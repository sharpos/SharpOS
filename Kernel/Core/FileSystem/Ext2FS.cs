// 
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Jae Hyun Roh <wonbear@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

using SharpOS.AOT.Attributes;
using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.FileSystem {
	public unsafe static class Ext2FS {
		static byte* floppyDiskData = null; 
		static FileSystem* fileSystem = null;
		static INode* root = null;
		static DirectoryFileFormat* format = null;

		static UInt16 readCount = 512 * 18 * 7;

		public static void Setup ()
		{
			floppyDiskData = (byte*)MemoryManager.Allocate(readCount);

			FloppyDiskController.Read(floppyDiskData, 0, readCount);

			if (!ReadSuperBlock())
				return;

			ReadGroupDescriptor ();	
			
			root = ReadINode (INode.EXT2_ROOT_INO);	
			
			format = (DirectoryFileFormat*) MemoryManager.Allocate ((uint) (sizeof (DirectoryFileFormat) * 20));
		}

		public static void ListFile ()
		{
			GetDirectoryEntries (root, format);
		}

		private static bool IsDirectory (INode* inode)
		{
			return (inode->Mode & INode.EXT2_S_IFDIR) != 0;
		}

		private static void GetDataBlocks (INode* inode, uint* blocks, bool* indirectBlocks, ref int blockCount)
		{
			uint count = 0;

			count = (uint) (inode->Size / fileSystem->BlockSize);

			blockCount = (int) count;

			uint* blockDatas = inode->BlockData;

			for (int i = 0; i < 12 && count > 0; i++, count--) {
				blocks [i] = blockDatas [i];
				indirectBlocks [i] = false;
			}

			if (count == 0)
				return;

			//this.GetIndirectDataBlocks(ref count, inode.BlockData[12], 0, blocks, indirectBlocks);

			//if (count == 0)
			//    return;

			//this.GetIndirectDataBlocks(ref count, inode.BlockData[13], 1, blocks, indirectBlocks);

			//if (count == 0)
			//    return;

			//this.GetIndirectDataBlocks(ref count, inode.BlockData[14], 2, blocks, indirectBlocks);

			//if (count > 0)
			//    throw new Exception(string.Format("File too big. (Inode #{0})", inode));
		}

		private static void GetDirectoryEntries (INode* inode, DirectoryFileFormat* format)
		{
			if (!IsDirectory (inode))
				return;

			int blockCount = 0;

			uint* blocks = (uint*) MemoryManager.Allocate (sizeof (uint) * 100);
			bool* indirectBlocks = (bool*) MemoryManager.Allocate (sizeof (bool) * 100);
			GetDataBlocks (inode, blocks, indirectBlocks, ref blockCount);

			int formatCount = 0;

			for (int i = 0; i < blockCount; ++i) {
				byte* source = floppyDiskData;
				byte* buffer = (byte*) (source + (blocks [i] * fileSystem->BlockSize));

				Block* dataBlock = (Block*) MemoryManager.Allocate ((uint) sizeof (Block));
				dataBlock->SetBlock (blocks [i] * fileSystem->BlockSize, buffer);

				uint offset = 0;

				while (offset < fileSystem->BlockSize) {
					format [formatCount].SetBlock (dataBlock, offset);
					if (format [formatCount].INode != 0) {
						//TextMode.WriteLine("INode Exist : ", (int)format[formatCount].INode);
					}

					offset += format [formatCount].RecordLength;
					TextMode.WriteLine (format [formatCount].Name);

					formatCount++;
				}
			}
		}

		private static bool ReadSuperBlock ()
		{
			byte* source = floppyDiskData;
			byte* buffer = (byte*) (source + 1024);

			uint sizeOfBlock = (uint) sizeof (Block);
			Block* block = (Block*) SharpOS.Kernel.ADC.MemoryManager.Allocate (sizeOfBlock);//block.Setup(1024, buffer);

			block->SetBlock (1024, buffer);			
			SuperBlock* superBlock = (SuperBlock*) SharpOS.Kernel.ADC.MemoryManager.Allocate ((uint) sizeof (SuperBlock));
			superBlock->SetBlock (block);

			fileSystem = (FileSystem*) MemoryManager.Allocate ((uint) sizeof (FileSystem));
			if (!fileSystem->SetSuperBlock(superBlock))
			{
				TextMode.WriteLine ("Not an Ext2 partition.");
				return false;
			}

			if (fileSystem->SuperBlock->Magic != SuperBlock.EXT2_MAGIC)
			{
				TextMode.WriteLine("Not an Ext2 partition.");
				return false;
			}

			if (fileSystem->SuperBlock->Errors != SuperBlock.EXT2_ERRORS_CONTINUE)
			{
				TextMode.WriteLine("Invalid Ext2 SuperBlock state.");
				return false;
			}

			if (fileSystem->SuperBlock->AlgorithmsBitmap != 0)
			{
				TextMode.WriteLine("Unsupported Bitmap Compression Algorithm.");
				return false;
			}
			return true;
		}

		private static void ReadGroupDescriptor ()
		{
			Block* block = null;

			for (uint i = 0; i < fileSystem->GroupsCount; i++) {
				if (i % fileSystem->GroupDescriptorsPerBlock == 0) {
					uint offset = fileSystem->SuperBlock->FirstDataDlock + 1;
					offset += (uint) (i / fileSystem->GroupDescriptorsPerBlock);
					offset *= fileSystem->BlockSize;

					uint sizeOfBlock = (uint) sizeof (Block);
					block = (Block*) SharpOS.Kernel.ADC.MemoryManager.Allocate (sizeOfBlock);

					byte* source = floppyDiskData;
					byte* buffer = (byte*) (source + offset);

					block->SetBlock (offset, buffer);
				}

				fileSystem->GroupDescriptors [i].SetBlock (block, (uint) ((i % fileSystem->GroupDescriptorsPerBlock) * GroupDescriptor.GroupDescriptorSize));
			}
		}

		private static INode* ReadINode (uint value)
		{
			value--;

			uint group = value / fileSystem->SuperBlock->INodesPerGroup;

			uint index = (value % fileSystem->SuperBlock->INodesPerGroup) * fileSystem->SuperBlock->INodeSize;

			uint offset = index % fileSystem->BlockSize;

			uint blockNumber = fileSystem->GroupDescriptors [group].INodeTable + (index / fileSystem->BlockSize);

			uint fileOffset = blockNumber * fileSystem->BlockSize;

			byte* source = floppyDiskData;
			byte* buffer = (byte*) (source + fileOffset);

			uint sizeOfBlock = (uint) sizeof (Block);
			Block* block = (Block*) SharpOS.Kernel.ADC.MemoryManager.Allocate (sizeOfBlock);
			block->SetBlock (fileOffset, buffer);

			INode* result = (INode*) MemoryManager.Allocate ((uint) sizeof (INode));
			result->SetBlock (block, offset);

			return result;
		}
	}
}
