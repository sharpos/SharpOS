//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//  Jae Hyun Roh <wonbear@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;

namespace SharpOS.Kernel.FileSystem
{
	public unsafe struct DirectoryFileFormat {
		public Block* block;
		public uint index;
		
		public void SetBlock(Block* block, uint index)
		{
			this.block = block;
			this.index = index;
		}
		
		public Block* Block {
			get {
				return this.block;
			}
		}
		
		public const byte EXT2_FT_UNKNOWN = 0;
		public const byte EXT2_FT_REG_FILE = 1;
		public const byte EXT2_FT_DIR = 2;
		public const byte EXT2_FT_CHRDEV = 3;
		public const byte EXT2_FT_BLKDEV = 4;
		public const byte EXT2_FT_FIFO = 5;
		public const byte EXT2_FT_SOCK = 6;
		public const byte EXT2_FT_SYMLINK = 7;
		public const byte EXT2_FT_MAX = 8;
		
		private const uint INodeOffset = 0;
		private const uint RecordLengthOffset = 4;
		private const uint NameLengthOffset = 6;
		private const uint FileTypeOffset = 7;
		private const uint NameOffset = 8;
		public const uint DirectoryFileFormatSize = 9;
		
		public uint INode {
			get {
				return this.block->GetUInt (this.index + INodeOffset);
			}
			set {
				this.block->SetUInt (this.index + INodeOffset, value);
			}
		}
		
		public ushort RecordLength {
			get {
				return this.block->GetUShort (this.index + RecordLengthOffset);
			}
			set {
				this.block->SetUShort (this.index + RecordLengthOffset, value);
			}
		}
		
		public byte NameLength {
			get {
				return this.block->GetByte (this.index + NameLengthOffset);
			}
			set {
				this.block->SetByte (this.index + NameLengthOffset, value);
			}
		}
		
		public byte FileType {
			get {
				return this.block->GetByte (this.index + FileTypeOffset);
			}
			set {
				this.block->SetByte (this.index + FileTypeOffset, value);
			}
		}

        public SharpOS.Kernel.Foundation.CString8* Name {
			get {
				return this.block->GetString (this.NameLength, this.index + NameOffset);
			}
		}
	}
}
