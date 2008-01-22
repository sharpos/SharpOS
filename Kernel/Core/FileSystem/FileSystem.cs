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
using System.Text;

namespace SharpOS.Kernel.FileSystem
{
    public unsafe struct FileSystem
    {
        public bool SetSuperBlock(SuperBlock* superBlock)
        {
			this.superBlock = superBlock;

            ///////////////////////////////////////////////////////////////////////
            this.blockSize = (uint)(1024 << (int)this.superBlock->BlockSize);

			
			if (this.superBlock->FragmentSize < 0)
                this.fragmentSize = (uint)(1024 >> -superBlock->FragmentSize);
            else
                this.fragmentSize = (uint)(1024 << superBlock->FragmentSize);

			if (superBlock->INodesPerGroup == 0)
			{
				ADC.TextMode.WriteLine("superBlock->INodesPerGroup == 0");
				return false;
			}

            this.groupsCount = (uint)(superBlock->INodesCount / superBlock->INodesPerGroup);

			if (this.fragmentSize == 0)
			{
				ADC.TextMode.WriteLine("this.fragmentSize == 0");
				return false;
			}

            this.fragmentsPerBlock = (uint)(this.blockSize / this.fragmentSize);

			if (GroupDescriptor.GroupDescriptorSize == 0)
			{
				ADC.TextMode.WriteLine("GroupDescriptor.GroupDescriptorSize == 0");
				return false;
			}

            this.groupDescriptorsPerBlock = (uint)(this.blockSize / GroupDescriptor.GroupDescriptorSize); //Marshal.SizeOf (typeof (GroupDescriptor)));
            this.indirectCount = this.blockSize >> 2;
			
            if (this.superBlock->RevisionLevel == 1)
                this.inodeSize = this.superBlock->INodeSize;
            else
                this.inodeSize = 128;
			
			if (this.inodeSize == 0)
				return false;
			
			if (this.inodeSize == 0)
			{
				ADC.TextMode.WriteLine("this.inodeSize == 0");
				return false;
			}

            this.inodesPerBlock = (uint)(this.blockSize / this.inodeSize);


			if (this.blockSize == 0)
			{
				ADC.TextMode.WriteLine("this.blockSize == 0");
				return false;
			}

            this.inodeTableBlocksCount = ((this.superBlock->INodesPerGroup * this.inodeSize) / this.blockSize);

            ///////////////////////////////////////////////////////////////////////
            uint bitmapSize = 0;
			
			bitmapSize = this.superBlock->BlocksPerGroup / 8;
            this.blockBitmapBlocksCount = (uint)(bitmapSize / this.blockSize);
            this.groupDescriptors = (GroupDescriptor*)SharpOS.Kernel.ADC.MemoryManager.Allocate((uint)(sizeof(GroupDescriptor) * this.groupsCount));

			return true;
        }

        private uint inodeSize;

        public uint INodeSize
        {
            get
            {
                return inodeSize;
            }
        }

        private uint blockSize;

        public uint BlockSize
        {
            get
            {
                return blockSize;
            }
        }

        private uint fragmentSize;

        public uint FragmentSize
        {
            get
            {
                return fragmentSize;
            }
        }

        private uint groupsCount;

        public uint GroupsCount
        {
            get
            {
                return groupsCount;
            }
        }

        private uint groupDescriptorsPerBlock;

        public uint GroupDescriptorsPerBlock
        {
            get
            {
                return groupDescriptorsPerBlock;
            }
        }

        private uint inodesPerBlock;

        public uint INodesPerBlock
        {
            get
            {
                return inodesPerBlock;
            }
        }

        private uint inodeTableBlocksCount;

        public uint INodeTableBlocksCount
        {
            get
            {
                return inodeTableBlocksCount;
            }
        }

        private uint fragmentsPerBlock;

        public uint FragmentsPerBlock
        {
            get
            {
                return fragmentsPerBlock;
            }
        }

        private uint indirectCount;

        public uint IndirectCount
        {
            get
            {
                return indirectCount;
            }
        }

        private uint blockBitmapBlocksCount;

        public uint BlockBitmapBlocksCount
        {
            get
            {
                return blockBitmapBlocksCount;
            }
        }

        private SuperBlock* superBlock;

        public SuperBlock* SuperBlock
        {
            get
            {
                return this.superBlock;
            }
        }

        private GroupDescriptor* groupDescriptors;

        public GroupDescriptor* GroupDescriptors
        {
            get
            {
                return groupDescriptors;
            }
        }
    }
}
