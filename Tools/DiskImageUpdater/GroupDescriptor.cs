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
	public class GroupDescriptor {
		public Block block;
		public uint index;

		public GroupDescriptor (Block block, uint index)
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


		private static readonly uint BlockBitmapOffset = 0;
		private static readonly uint INodeBitmapOffset = 4;
		private static readonly uint INodeTableOffset = 8;
		private static readonly uint FreeBlocksCountOffset = 12;
		private static readonly uint FreeINodesCountOffset = 14;
		private static readonly uint UsedDirsCountOffset = 16;
		private static readonly uint PadOffset = 18;
		private static readonly uint ReservedOffset = 20;
		public static readonly uint GroupDescriptorSize = 32;

		public uint BlockBitmap
		{
			get
			{
				return this.block.GetUInt (this.index + BlockBitmapOffset);
			}
			set
			{
				this.block.SetUInt (this.index + BlockBitmapOffset, value);
			}
		}

		public uint INodeBitmap
		{
			get
			{
				return this.block.GetUInt (this.index + INodeBitmapOffset);
			}
			set
			{
				this.block.SetUInt (this.index + INodeBitmapOffset, value);
			}
		}

		public uint INodeTable
		{
			get
			{
				return this.block.GetUInt (this.index + INodeTableOffset);
			}
			set
			{
				this.block.SetUInt (this.index + INodeTableOffset, value);
			}
		}

		public ushort FreeBlocksCount
		{
			get
			{
				return this.block.GetUShort (this.index + FreeBlocksCountOffset);
			}
			set
			{
				this.block.SetUShort (this.index + FreeBlocksCountOffset, value);
			}
		}

		public ushort FreeINodesCount
		{
			get
			{
				return this.block.GetUShort (this.index + FreeINodesCountOffset);
			}
			set
			{
				this.block.SetUShort (this.index + FreeINodesCountOffset, value);
			}
		}

		public ushort UsedDirsCount
		{
			get
			{
				return this.block.GetUShort (this.index + UsedDirsCountOffset);
			}
			set
			{
				this.block.SetUShort (this.index + UsedDirsCountOffset, value);
			}
		}

		public ushort Pad
		{
			get
			{
				return this.block.GetUShort (this.index + PadOffset);
			}
			set
			{
				this.block.SetUShort (this.index + PadOffset, value);
			}
		}

		public byte [] Reserved
		{
			get
			{
				return this.block.GetByteArray (12, this.index + ReservedOffset);
			}
			set
			{
				this.block.SetByteArray (12, this.index + ReservedOffset, value);
			}
		}
	}
}
