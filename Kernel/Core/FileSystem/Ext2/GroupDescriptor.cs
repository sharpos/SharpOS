//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Jae Hyun Roh <wonbear@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;

namespace SharpOS.Kernel.FileSystem.Ext2
{
    public unsafe struct GroupDescriptor
    {
        public Block* block;
        public uint index;

        public void SetBlock(Block* block, uint index)
        {
            this.block = block;
            this.index = index;
        }

        public Block* Block
        {
            get
            {
                return this.block;
            }
        }


        private const uint BlockBitmapOffset = 0;
        private const uint INodeBitmapOffset = 4;
        private const uint INodeTableOffset = 8;
        private const uint FreeBlocksCountOffset = 12;
        private const uint FreeINodesCountOffset = 14;
        private const uint UsedDirsCountOffset = 16;
        private const uint PadOffset = 18;
        private const uint ReservedOffset = 20;
        public const uint GroupDescriptorSize = 32;

        public uint BlockBitmap
        {
            get
            {
                return this.block->GetUInt(this.index + BlockBitmapOffset);
            }
            set
            {
                this.block->SetUInt(this.index + BlockBitmapOffset, value);
            }
        }

        public uint INodeBitmap
        {
            get
            {
                return this.block->GetUInt(this.index + INodeBitmapOffset);
            }
            set
            {
                this.block->SetUInt(this.index + INodeBitmapOffset, value);
            }
        }

        public uint INodeTable
        {
            get
            {
                return this.block->GetUInt(this.index + INodeTableOffset);
            }
            set
            {
                this.block->SetUInt(this.index + INodeTableOffset, value);
            }
        }

        public ushort FreeBlocksCount
        {
            get
            {
                return this.block->GetUShort(this.index + FreeBlocksCountOffset);
            }
            set
            {
                this.block->SetUShort(this.index + FreeBlocksCountOffset, value);
            }
        }

        public ushort FreeINodesCount
        {
            get
            {
                return this.block->GetUShort(this.index + FreeINodesCountOffset);
            }
            set
            {
                this.block->SetUShort(this.index + FreeINodesCountOffset, value);
            }
        }

        public ushort UsedDirsCount
        {
            get
            {
                return this.block->GetUShort(this.index + UsedDirsCountOffset);
            }
            set
            {
                this.block->SetUShort(this.index + UsedDirsCountOffset, value);
            }
        }

        public ushort Pad
        {
            get
            {
                return this.block->GetUShort(this.index + PadOffset);
            }
            set
            {
                this.block->SetUShort(this.index + PadOffset, value);
            }
        }

        public byte* Reserved
        {
            get
            {
                return this.block->GetByteArray(12, this.index + ReservedOffset);
            }
            set
            {
                this.block->SetByteArray(12, this.index + ReservedOffset, value);
            }
        }
    }
}
