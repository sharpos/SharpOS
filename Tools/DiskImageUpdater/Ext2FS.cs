// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace Ext2 {
	public class Ext2FS {
		private void ForceWriteBlock (Block block)
		{
			block.Dirty = true;

			this.WriteBlock (block);
		}

		private void WriteBlock (Block block)
		{
			if (!block.Dirty)
				return;

			binaryReader.BaseStream.Seek (startOffset + block.Offset, SeekOrigin.Begin);
			binaryReader.BaseStream.Write (block.Buffer, 0, block.Buffer.Length);

			block.Dirty = false;
		}

		public int CHSToLBA (int cyl, int head, int sector)
		{
			return ((cyl * 10 + head) * 50) + sector - 1;
		}

		private uint startOffset = 0;

		private void ReadSuperBlock (bool hasMBR)
		{
			if (hasMBR) {
				byte [] mbr = binaryReader.ReadBytes (512);

				if (mbr [510] != 55 && mbr [511] != 0xaa)
					throw new Exception ("Invalid MBR Signature.");

				bool found = false;

				for (int i = 446; i < 510; i += 16) {
					if ((mbr [i] & 0x80) != 0) {
						found = true;

						if (mbr [i + 4] != 0x83)
							throw new Exception ("The bootable partition is not of type Ext2.");

						startOffset = mbr [i + 8];
						startOffset += (uint) (mbr [i + 9] << 8);
						startOffset += (uint) (mbr [i + 10] << 16);
						startOffset += (uint) (mbr [i + 11] << 24);

						startOffset *= 512;

						break;
					}
				}

				if (!found)
					throw new Exception ("No bootable partition found.");
			}

			binaryReader.BaseStream.Seek (startOffset + 1024, SeekOrigin.Begin);

			byte [] buffer = binaryReader.ReadBytes (1024);
			Block block = new Block (1024, buffer);

			SuperBlock superBlock = new SuperBlock (block);

			this.fileSystem = new FileSystem (superBlock);

			if (this.fileSystem.SuperBlock.Magic != SuperBlock.EXT2_MAGIC)
				throw new Exception ("Not an Ext2 partition.");

			if (this.fileSystem.SuperBlock.Errors != SuperBlock.EXT2_ERRORS_CONTINUE)
				throw new Exception ("Invalid Ext2 SuperBlock state.");

			if (this.fileSystem.SuperBlock.AlgorithmsBitmap != 0)
				throw new Exception ("Unsupported Bitmap Compression Algorithm.");
		}

		private void WriteSuperBlock ()
		{
			WriteBlock (this.fileSystem.SuperBlock.Block);
		}

		private void ReadGroupDescriptor ()
		{
			Block block = null;

			for (int i = 0; i < this.fileSystem.GroupsCount; i++) {
				if (i % this.fileSystem.GroupDescriptorsPerBlock == 0) {
					uint offset = fileSystem.SuperBlock.FirstDataDlock + 1;
					offset += (uint) (i / this.fileSystem.GroupDescriptorsPerBlock);
					offset *= this.fileSystem.BlockSize;

					binaryReader.BaseStream.Seek (startOffset + offset, SeekOrigin.Begin);

					byte [] buffer = binaryReader.ReadBytes ((int) this.fileSystem.BlockSize);
					block = new Block (offset, buffer);
				}

				this.fileSystem.GroupDescriptors [i] = new GroupDescriptor (block, (uint) ((i % this.fileSystem.GroupDescriptorsPerBlock) * GroupDescriptor.GroupDescriptorSize));
			}
		}

		private void WriteGroupDescriptor ()
		{
			for (int i = 0; i < this.fileSystem.GroupsCount; i++)
				WriteBlock (this.fileSystem.GroupDescriptors [i].Block);
		}

		private INode ReadINode (uint value)
		{
			value--;

			uint group = value / this.fileSystem.SuperBlock.INodesPerGroup;

			uint index = (value % this.fileSystem.SuperBlock.INodesPerGroup) * this.fileSystem.SuperBlock.INodeSize;

			uint offset = index % this.fileSystem.BlockSize;

			uint blockNumber = this.fileSystem.GroupDescriptors [group].INodeTable + (index / this.fileSystem.BlockSize);

			uint fileOffset = blockNumber * this.fileSystem.BlockSize;

			binaryReader.BaseStream.Seek (startOffset + fileOffset, SeekOrigin.Begin);

			byte [] buffer = binaryReader.ReadBytes ((int) this.fileSystem.BlockSize);

			Block block = new Block (fileOffset, buffer);

			INode result = new INode (block, offset);

			return result;
		}

		private void GetDataBlocks (INode inode, List<uint> blocks, List<bool> indirectBlocks)
		{
			uint count = (uint) Math.Ceiling ((double) inode.Size / (double) this.fileSystem.BlockSize);

			for (int i = 0; i < 12 && count > 0; i++, count--) {
				blocks.Add (inode.BlockData [i]);
				indirectBlocks.Add (false);
			}

			if (count == 0)
				return;

			this.GetIndirectDataBlocks (ref count, inode.BlockData [12], 0, blocks, indirectBlocks);

			if (count == 0)
				return;

			this.GetIndirectDataBlocks (ref count, inode.BlockData [13], 1, blocks, indirectBlocks);

			if (count == 0)
				return;

			this.GetIndirectDataBlocks (ref count, inode.BlockData [14], 2, blocks, indirectBlocks);

			if (count > 0)
				throw new Exception (string.Format ("File too big. (Inode #{0})", inode));
		}

		private void GetIndirectDataBlocks (ref uint count, uint block, int level, List<uint> blocks, List<bool> indirectBlocks)
		{
			blocks.Add (block);
			indirectBlocks.Add (true);

			this.binaryReader.BaseStream.Seek (startOffset + block * this.fileSystem.BlockSize, SeekOrigin.Begin);
			byte [] buffer = binaryReader.ReadBytes ((int) this.fileSystem.BlockSize);

			uint offset = 0;
			for (int i = 0; i < this.fileSystem.IndirectCount && count > 0; i++) {
				uint value = buffer [offset++];
				value += (uint) (buffer [offset++] << 8);
				value += (uint) (buffer [offset++] << 16);
				value += (uint) (buffer [offset++] << 24);

				if (value > 0) {
					if (level > 0) {
						blocks.Add (value);
						indirectBlocks.Add (true);
						this.GetIndirectDataBlocks (ref count, value, level - 1, blocks, indirectBlocks);
					} else {
						blocks.Add (value);
						indirectBlocks.Add (false);
						count--;
					}
				}
			}
		}

		private bool IsDirectory (INode inode)
		{
			return (inode.Mode & INode.EXT2_S_IFDIR) != 0;
		}

		private DirectoryFileFormat [] GetDirectoryEntries (INode inode)
		{
			List<DirectoryFileFormat> result = new List<DirectoryFileFormat> ();

			if (!this.IsDirectory (inode))
				return result.ToArray ();

			List<uint> blocks = new List<uint> ();
			List<bool> indirectBlocks = new List<bool> ();
			this.GetDataBlocks (inode, blocks, indirectBlocks);

			foreach (uint block in blocks) {
				this.binaryReader.BaseStream.Seek (startOffset + block * this.fileSystem.BlockSize, SeekOrigin.Begin);

				byte [] buffer = binaryReader.ReadBytes ((int) this.fileSystem.BlockSize);

				Block dataBlock = new Block (block * this.fileSystem.BlockSize, buffer);

				uint offset = 0;

				while (offset < this.fileSystem.BlockSize) {
					DirectoryFileFormat entry = new DirectoryFileFormat (dataBlock, offset);

					if (entry.INode != 0)
						result.Add (entry);

					offset += entry.RecordLength;
				}
			}

			return result.ToArray ();
		}

		private void DisplayTree (string path, INode parent)
		{
			DirectoryFileFormat [] entries = this.GetDirectoryEntries (parent);

			foreach (DirectoryFileFormat entry in entries) {
				INode inode = this.ReadINode (entry.INode);
				Console.WriteLine ("{2}{0} {1}", entry.Name, inode.Size, path);

				if (this.IsDirectory (inode)
						&& !entry.Name.Equals (".")
						&& !entry.Name.Equals (".."))
					this.DisplayTree (path + entry.Name + "/", inode);
			}
		}

		private INode GetINodeByName (string name)
		{
			string [] list = name.Split (new char [] { '/' });

			if (list.Length < 2)
				return this.root;

			return this.GetINodeByName (this.root, 1, list);
		}

		private INode GetINodeByName (INode parent, int index, string [] list)
		{
			DirectoryFileFormat [] entries = this.GetDirectoryEntries (parent);

			foreach (DirectoryFileFormat entry in entries) {
				INode inode = this.ReadINode (entry.INode);

				if (list [index] == entry.Name) {
					if (index == list.Length - 1)
						return inode;

					else if (this.IsDirectory (inode))
						return this.GetINodeByName (inode, index + 1, list);
				}
			}

			throw new Exception ("Not found");
		}

		private uint ComputeIndirectOfBlocksCount (uint blocks)
		{
			uint count = 0;
			if (blocks <= 12)
				return 0;

			blocks -= 12;

			this.ComputeIndirectOfBlocksCount (ref blocks, ref count, 0);

			if (blocks == 0)
				return count;

			this.ComputeIndirectOfBlocksCount (ref blocks, ref count, 1);

			if (blocks == 0)
				return count;

			this.ComputeIndirectOfBlocksCount (ref blocks, ref count, 2);

			if (blocks == 0)
				return count;

			throw new Exception ("Too big.");
		}

		private void ComputeIndirectOfBlocksCount (ref uint blocks, ref uint count, uint level)
		{
			count++;

			if (blocks > this.fileSystem.IndirectCount)
				blocks -= this.fileSystem.IndirectCount;
			else
				blocks = 0;

			if (level == 0)
				return;

			for (int i = 0; i < this.fileSystem.IndirectCount && blocks > 0; i++)
				this.ComputeIndirectOfBlocksCount (ref blocks, ref count, level - 1);
		}

		private void AllocateBlocks (List<uint> blocks, uint count)
		{
			// TOOD support for more than one descriptor
			// it has to pick the descriptor that contains the inode for
			// which these blocks are.
			int descriptor = 0;

			if (this.fileSystem.GroupDescriptors [descriptor].FreeBlocksCount < count - blocks.Count)
				throw new Exception ("Not enough free blocks.");

			this.binaryReader.BaseStream.Seek (startOffset + this.fileSystem.GroupDescriptors [descriptor].BlockBitmap * this.fileSystem.BlockSize, SeekOrigin.Begin);
			byte [] buffer = binaryReader.ReadBytes ((int) (this.fileSystem.BlockBitmapBlocksCount * this.fileSystem.BlockSize));

			// TODO better allocation algorithm
			for (int i = 0; i < buffer.Length && blocks.Count < count; i++) {
				if (buffer [i] == 0) {
					for (int j = 0; j < 8 && blocks.Count < count; j++) {
						buffer [i] |= (byte) (1 << j);

						this.fileSystem.GroupDescriptors [descriptor].FreeBlocksCount--;
						this.fileSystem.SuperBlock.FreeBlocksCount--;

						uint blockIndex = (uint) ((i * 8) + j) + 1;

						blocks.Add (blockIndex);
					}
				}
			}

			if (blocks.Count != count)
				throw new Exception ("Could not allocate enough blocks.");

			this.binaryReader.BaseStream.Seek (startOffset + this.fileSystem.GroupDescriptors [descriptor].BlockBitmap * this.fileSystem.BlockSize, SeekOrigin.Begin);
			binaryReader.BaseStream.Write (buffer, 0, (int) (this.fileSystem.BlockBitmapBlocksCount * this.fileSystem.BlockSize));
		}

		private void DeallocateBlocks (List<uint> blocks, uint count)
		{
			// TOOD support for more than one descriptor
			// it has to pick the descriptor that contains the inode for
			// which these blocks are.
			int descriptor = 0;

			this.binaryReader.BaseStream.Seek (startOffset + this.fileSystem.GroupDescriptors [descriptor].BlockBitmap * this.fileSystem.BlockSize, SeekOrigin.Begin);
			byte [] buffer = binaryReader.ReadBytes ((int) (this.fileSystem.BlockBitmapBlocksCount * this.fileSystem.BlockSize));

			while (blocks.Count > count) {
				uint value = blocks [blocks.Count - 1];
				blocks.Remove (value);

				value--;

				uint index = value / 8;
				byte bit = (byte) (value % 8);

				buffer [index] &= (byte) ~(1 << bit);

				this.fileSystem.GroupDescriptors [descriptor].FreeBlocksCount++;
				this.fileSystem.SuperBlock.FreeBlocksCount++;
			}

			this.binaryReader.BaseStream.Seek (startOffset + this.fileSystem.GroupDescriptors [descriptor].BlockBitmap * this.fileSystem.BlockSize, SeekOrigin.Begin);
			binaryReader.BaseStream.Write (buffer, 0, (int) (this.fileSystem.BlockBitmapBlocksCount * this.fileSystem.BlockSize));
		}

		private void WriteFile (INode inode, List<uint> blocks, BinaryReader binaryReader)
		{
			uint [] blockData = new uint [15];

			for (int i = 0; i < 15; i++)
				blockData [i] = 0;

			for (int i = 0; i < 12 && blocks.Count > 0; i++) {
				uint value = blocks [0];
				blocks.Remove (value);

				blockData [i] = value;

				uint offset = value * this.fileSystem.BlockSize;
				byte [] buffer = binaryReader.ReadBytes ((int) this.fileSystem.BlockSize);

				Block block = new Block (offset, buffer);

				this.ForceWriteBlock (block);
			}

			if (blocks.Count > 0)
				blockData [12] = this.WriteFileIndirect (0, blocks, binaryReader);

			if (blocks.Count > 0)
				blockData [13] = this.WriteFileIndirect (1, blocks, binaryReader);

			if (blocks.Count > 0)
				blockData [14] = this.WriteFileIndirect (2, blocks, binaryReader);

			inode.BlockData = blockData;
		}

		private uint WriteFileIndirect (uint level, List<uint> blocks, BinaryReader binaryReader)
		{
			Block block;
			byte [] buffer;
			uint offset = 0;

			uint blockValue = blocks [0];
			blocks.Remove (blockValue);

			uint [] blockData = new uint [this.fileSystem.BlockSize >> 2];
			for (int i = 0; i < blockData.Length; i++)
				blockData [i] = 0;


			for (int i = 0; i < blockData.Length && blocks.Count > 0; i++) {
				if (level == 0) {
					uint value = blocks [0];
					blocks.Remove (value);

					blockData [i] = value;

					offset = value * this.fileSystem.BlockSize;
					buffer = binaryReader.ReadBytes ((int) this.fileSystem.BlockSize);

					block = new Block (offset, buffer);

					this.ForceWriteBlock (block);

				} else
					blockData [i] = WriteFileIndirect (level - 1, blocks, binaryReader);
			}


			buffer = new byte [this.fileSystem.BlockSize];
			offset = 0;
			for (int i = 0; i < blockData.Length; i++) {
				buffer [offset++] = (byte) (blockData [i] & 0xFF);
				buffer [offset++] = (byte) ((blockData [i] >> 8) & 0xFF);
				buffer [offset++] = (byte) ((blockData [i] >> 16) & 0xFF);
				buffer [offset++] = (byte) ((blockData [i] >> 24) & 0xFF);
			}

			block = new Block (blockValue * this.fileSystem.BlockSize, buffer);

			this.ForceWriteBlock (block);

			return blockValue;
		}

		private FileSystem fileSystem;
		private System.IO.BinaryReader binaryReader;
		private INode root;

		public void UpdateKernel (string image, string kernel, bool hasMBR)
		{
			using (binaryReader = new BinaryReader (File.Open (image, FileMode.Open))) {
				this.ReadSuperBlock (hasMBR);

				///////////////////////////////////////////////////////////////////////
				this.ReadGroupDescriptor ();

				this.root = this.ReadINode (INode.EXT2_ROOT_INO);

				///////////////////////////////////////////////////////////////////////
				// Console.WriteLine ("------------------------------------------------------------");
				// this.DisplayTree ("/", this.root);

				///////////////////////////////////////////////////////////////////////
				INode inode = this.GetINodeByName ("/SharpOS.Kernel.bin");

				List<uint> blocks = new List<uint> ();
				List<bool> indirectBlocks = new List<bool> ();
				GetDataBlocks (inode, blocks, indirectBlocks);

				///////////////////////////////////////////////////////////////////////
				using (BinaryReader kernelBinaryReader = new BinaryReader (File.Open (kernel, FileMode.Open))) {
					// Console.WriteLine ("Length: {0}", kernelBinaryReader.BaseStream.Length);

					uint neededBlocks = (uint) Math.Ceiling ((double) kernelBinaryReader.BaseStream.Length / (double) this.fileSystem.BlockSize);
					uint neededIndirectBlocks = this.ComputeIndirectOfBlocksCount (neededBlocks);

					if (neededBlocks + neededIndirectBlocks > blocks.Count)
						this.AllocateBlocks (blocks, neededBlocks + neededIndirectBlocks);

					else if (neededBlocks + neededIndirectBlocks < blocks.Count)
						this.DeallocateBlocks (blocks, neededBlocks + neededIndirectBlocks);

					this.WriteSuperBlock ();
					this.WriteGroupDescriptor ();

					this.WriteFile (inode, blocks, kernelBinaryReader);

					inode.Size = (uint) kernelBinaryReader.BaseStream.Length;
					inode.Blocks = (uint) ((neededBlocks + neededIndirectBlocks) * this.fileSystem.BlockSize / 512);

					this.WriteBlock (inode.Block);
				}

			}
		}
	}
}

