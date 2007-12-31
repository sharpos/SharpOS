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

namespace Ext2 {
	public class INode {
		public Block block;
		public uint index;

		public INode (Block block, uint index)
		{
			this.block = block;
			this.index = index;
		}

		public Block Block
		{
			get
			{
				return this.block;
			}
		}

		public const ushort EXT2_BAD_INO = 0x01;
		public const ushort EXT2_ROOT_INO = 0x02;
		public const ushort EXT2_ACL_IDX_INO = 0x03;
		public const ushort EXT2_ACL_DATA_INO = 0x04;
		public const ushort EXT2_BOOT_LOADER_INO = 0x05;
		public const ushort EXT2_UNDEL_DIR_INO = 0x06;
		public const ushort EXT2_S_IFMT = 0xF000;
		public const ushort EXT2_S_IFSOCK = 0xC000;
		public const ushort EXT2_S_IFLNK = 0xA000;
		public const ushort EXT2_S_IFREG = 0x8000;
		public const ushort EXT2_S_IFBLK = 0x6000;
		public const ushort EXT2_S_IFDIR = 0x4000;
		public const ushort EXT2_S_IFCHR = 0x2000;
		public const ushort EXT2_S_IFIFO = 0x1000;
		public const ushort EXT2_S_ISUID = 0x0800;
		public const ushort EXT2_S_ISGID = 0x0400;
		public const ushort EXT2_S_ISVTX = 0x0200;
		public const ushort EXT2_S_IRWXU = 0x01C0;
		public const ushort EXT2_S_IRUSR = 0x0100;
		public const ushort EXT2_S_IWUSR = 0x0080;
		public const ushort EXT2_S_IXUSR = 0x0040;
		public const ushort EXT2_S_IRWXG = 0x0038;
		public const ushort EXT2_S_IRGRP = 0x0020;
		public const ushort EXT2_S_IWGRP = 0x0010;
		public const ushort EXT2_S_IXGRP = 0x0008;
		public const ushort EXT2_S_IRWXO = 0x0007;
		public const ushort EXT2_S_IROTH = 0x0004;
		public const ushort EXT2_S_IWOTH = 0x0002;
		public const ushort EXT2_S_IXOTH = 0x0001;

		private static readonly uint ModeOffset = 0;
		private static readonly uint UserIDOffset = 2;
		private static readonly uint SizeOffset = 4;
		private static readonly uint AccessTimeOffset = 8;
		private static readonly uint CreatedTimeOffset = 12;
		private static readonly uint ModifiedTimeOffset = 16;
		private static readonly uint DeletedTimeOffset = 20;
		private static readonly uint GroupIDOffset = 24;
		private static readonly uint LinksCountOffset = 26;
		private static readonly uint BlocksOffset = 28;
		private static readonly uint FlagsOffset = 32;
		private static readonly uint FirstOSDependentValueOffset = 36;
		private static readonly uint BlockDataOffset = 40;
		private static readonly uint GenerationOffset = 100;
		private static readonly uint FileACLOffset = 104;
		private static readonly uint DirectoryACLOffset = 108;
		private static readonly uint LastFileFragmentOffset = 112;
		private static readonly uint SecondOSDependentValueOffset = 116;
		public static readonly uint INodeSize = 128;

		public ushort Mode
		{
			get
			{
				return this.block.GetUShort (this.index + ModeOffset);
			}
			set
			{
				this.block.SetUShort (this.index + ModeOffset, value);
			}
		}

		public ushort UserID
		{
			get
			{
				return this.block.GetUShort (this.index + UserIDOffset);
			}
			set
			{
				this.block.SetUShort (this.index + UserIDOffset, value);
			}
		}

		public uint Size
		{
			get
			{
				return this.block.GetUInt (this.index + SizeOffset);
			}
			set
			{
				this.block.SetUInt (this.index + SizeOffset, value);
			}
		}

		public uint AccessTime
		{
			get
			{
				return this.block.GetUInt (this.index + AccessTimeOffset);
			}
			set
			{
				this.block.SetUInt (this.index + AccessTimeOffset, value);
			}
		}

		public uint CreatedTime
		{
			get
			{
				return this.block.GetUInt (this.index + CreatedTimeOffset);
			}
			set
			{
				this.block.SetUInt (this.index + CreatedTimeOffset, value);
			}
		}

		public uint ModifiedTime
		{
			get
			{
				return this.block.GetUInt (this.index + ModifiedTimeOffset);
			}
			set
			{
				this.block.SetUInt (this.index + ModifiedTimeOffset, value);
			}
		}

		public uint DeletedTime
		{
			get
			{
				return this.block.GetUInt (this.index + DeletedTimeOffset);
			}
			set
			{
				this.block.SetUInt (this.index + DeletedTimeOffset, value);
			}
		}

		public ushort GroupID
		{
			get
			{
				return this.block.GetUShort (this.index + GroupIDOffset);
			}
			set
			{
				this.block.SetUShort (this.index + GroupIDOffset, value);
			}
		}

		public ushort LinksCount
		{
			get
			{
				return this.block.GetUShort (this.index + LinksCountOffset);
			}
			set
			{
				this.block.SetUShort (this.index + LinksCountOffset, value);
			}
		}

		public uint Blocks
		{
			get
			{
				return this.block.GetUInt (this.index + BlocksOffset);
			}
			set
			{
				this.block.SetUInt (this.index + BlocksOffset, value);
			}
		}

		public uint Flags
		{
			get
			{
				return this.block.GetUInt (this.index + FlagsOffset);
			}
			set
			{
				this.block.SetUInt (this.index + FlagsOffset, value);
			}
		}

		public uint FirstOSDependentValue
		{
			get
			{
				return this.block.GetUInt (this.index + FirstOSDependentValueOffset);
			}
			set
			{
				this.block.SetUInt (this.index + FirstOSDependentValueOffset, value);
			}
		}

		public uint [] BlockData
		{
			get
			{
				return this.block.GetUIntArray (15, this.index + BlockDataOffset);
			}
			set
			{
				this.block.SetUIntArray (15, this.index + BlockDataOffset, value);
			}
		}

		public uint Generation
		{
			get
			{
				return this.block.GetUInt (this.index + GenerationOffset);
			}
			set
			{
				this.block.SetUInt (this.index + GenerationOffset, value);
			}
		}

		public uint FileACL
		{
			get
			{
				return this.block.GetUInt (this.index + FileACLOffset);
			}
			set
			{
				this.block.SetUInt (this.index + FileACLOffset, value);
			}
		}

		public uint DirectoryACL
		{
			get
			{
				return this.block.GetUInt (this.index + DirectoryACLOffset);
			}
			set
			{
				this.block.SetUInt (this.index + DirectoryACLOffset, value);
			}
		}

		public uint LastFileFragment
		{
			get
			{
				return this.block.GetUInt (this.index + LastFileFragmentOffset);
			}
			set
			{
				this.block.SetUInt (this.index + LastFileFragmentOffset, value);
			}
		}

		public byte [] SecondOSDependentValue
		{
			get
			{
				return this.block.GetByteArray (12, this.index + SecondOSDependentValueOffset);
			}
			set
			{
				this.block.SetByteArray (12, this.index + SecondOSDependentValueOffset, value);
			}
		}
	}
}
