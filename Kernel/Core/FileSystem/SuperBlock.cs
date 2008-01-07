//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Jae Hyun Roh <wonbear@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;

namespace SharpOS.Kernel.FileSystem
{
	public unsafe struct SuperBlock {
		public Block* block;
		
		public void SetBlock(Block* block)
		{
			this.block = block;
		}
		
		public Block* Block {
			get {
				return this.block;
			}
		}
		
		public const ushort EXT2_MAGIC = 0xEF53;
		public const ushort EXT2_ERRORS_CONTINUE = 1;
		public const ushort EXT2_ERRORS_RO = 2;
		public const ushort EXT2_ERRORS_PANIC = 3;

        private const uint INodesCountOffset = 0;
        private const uint BlocksCountOffset = 4;
        private const uint ReservedBlocksCountOffset = 8;
        private const uint FreeBlocksCountOffset = 12;
        private const uint FreeINodesCountOffset = 16;
        private const uint FirstDataDlockOffset = 20;
        private const uint BlockSizeOffset = 24;
        private const uint FragmentSizeOffset = 28;
        private const uint BlocksPerGroupOffset = 32;
        private const uint FragsPerGroupOffset = 36;
        private const uint INodesPerGroupOffset = 40;
        private const uint LastMountTimeOffset = 44;
        private const uint LastWriteTimeOffset = 48;
        private const uint MountCountOffset = 52;
        private const uint MaxMountCountOffset = 54;
        private const uint MagicOffset = 56;
        private const uint StateOffset = 58;
        private const uint ErrorsOffset = 60;
        private const uint MinorRevisionLevelOffset = 62;
        private const uint LastCheckOffset = 64;
        private const uint CheckIntervalOffset = 68;
        private const uint CreatorOSOffset = 72;
        private const uint RevisionLevelOffset = 76;
        private const uint DefaultReservedUserIDOffset = 80;
        private const uint DefaultReservedGroupIDOffset = 82;
        private const uint FirstINodeOffset = 84;
        private const uint INodeSizeOffset = 88;
        private const uint BlockGroupNROffset = 90;
        private const uint FeaturesCompatibleOffset = 92;
        private const uint FeaturesIncompatibleOffset = 96;
        private const uint FeaturesReadOnlyCompatibleOffset = 100;
        private const uint UUIDOffset = 104;
        private const uint VolumeNameOffset = 120;
        private const uint LastMountedOffset = 136;
        private const uint AlgorithmsBitmapOffset = 200;
        public const uint SuperBlockSize = 204;
		
        public uint INodesCount {
            get {
                return this.block->GetUInt (INodesCountOffset);
            }
            set {
                this.block->SetUInt (INodesCountOffset, value);
            }
        }

        public uint BlocksCount
        {
            get
            {
                return this.block->GetUInt(BlocksCountOffset);
            }
            set
            {
                this.block->SetUInt(BlocksCountOffset, value);
            }
        }

        public uint ReservedBlocksCount
        {
            get
            {
                return this.block->GetUInt(ReservedBlocksCountOffset);
            }
            set
            {
                this.block->SetUInt(ReservedBlocksCountOffset, value);
            }
        }

        public uint FreeBlocksCount
        {
            get
            {
                return this.block->GetUInt(FreeBlocksCountOffset);
            }
            set
            {
                this.block->SetUInt(FreeBlocksCountOffset, value);
            }
        }

        public uint FreeINodesCount
        {
            get
            {
                return this.block->GetUInt(FreeINodesCountOffset);
            }
            set
            {
                this.block->SetUInt(FreeINodesCountOffset, value);
            }
        }

        public uint FirstDataDlock
        {
            get
            {
                return this.block->GetUInt(FirstDataDlockOffset);
            }
            set
            {
                this.block->SetUInt(FirstDataDlockOffset, value);
            }
        }

        public uint BlockSize
        {
            get
            {
                return this.block->GetUInt(BlockSizeOffset);
            }
            set
            {
                this.block->SetUInt(BlockSizeOffset, value);
            }
        }

        public int FragmentSize
        {
            get
            {
                return this.block->GetInt(FragmentSizeOffset);
            }
            set
            {
                this.block->SetInt(FragmentSizeOffset, value);
            }
        }

        public uint BlocksPerGroup
        {
            get
            {
                return this.block->GetUInt(BlocksPerGroupOffset);
            }
            set
            {
                this.block->SetUInt(BlocksPerGroupOffset, value);
            }
        }

        public uint FragsPerGroup
        {
            get
            {
                return this.block->GetUInt(FragsPerGroupOffset);
            }
            set
            {
                this.block->SetUInt(FragsPerGroupOffset, value);
            }
        }

        public uint INodesPerGroup
        {
            get
            {
                return this.block->GetUInt(INodesPerGroupOffset);
            }
            set
            {
                this.block->SetUInt(INodesPerGroupOffset, value);
            }
        }

        public uint LastMountTime
        {
            get
            {
                return this.block->GetUInt(LastMountTimeOffset);
            }
            set
            {
                this.block->SetUInt(LastMountTimeOffset, value);
            }
        }

        public uint LastWriteTime
        {
            get
            {
                return this.block->GetUInt(LastWriteTimeOffset);
            }
            set
            {
                this.block->SetUInt(LastWriteTimeOffset, value);
            }
        }

        public ushort MountCount
        {
            get
            {
                return this.block->GetUShort(MountCountOffset);
            }
            set
            {
                this.block->SetUShort(MountCountOffset, value);
            }
        }

        public ushort MaxMountCount
        {
            get
            {
                return this.block->GetUShort(MaxMountCountOffset);
            }
            set
            {
                this.block->SetUShort(MaxMountCountOffset, value);
            }
        }

        public ushort Magic
        {
            get
            {
                return this.block->GetUShort(MagicOffset);
            }
            set
            {
                this.block->SetUShort(MagicOffset, value);
            }
        }

        public ushort State
        {
            get
            {
                return this.block->GetUShort(StateOffset);
            }
            set
            {
                this.block->SetUShort(StateOffset, value);
            }
        }

        public ushort Errors
        {
            get
            {
                return this.block->GetUShort(ErrorsOffset);
            }
            set
            {
                this.block->SetUShort(ErrorsOffset, value);
            }
        }

        public ushort MinorRevisionLevel
        {
            get
            {
                return this.block->GetUShort(MinorRevisionLevelOffset);
            }
            set
            {
                this.block->SetUShort(MinorRevisionLevelOffset, value);
            }
        }

        public uint LastCheck
        {
            get
            {
                return this.block->GetUInt(LastCheckOffset);
            }
            set
            {
                this.block->SetUInt(LastCheckOffset, value);
            }
        }

        public uint CheckInterval
        {
            get
            {
                return this.block->GetUInt(CheckIntervalOffset);
            }
            set
            {
                this.block->SetUInt(CheckIntervalOffset, value);
            }
        }

        public uint CreatorOS
        {
            get
            {
                return this.block->GetUInt(CreatorOSOffset);
            }
            set
            {
                this.block->SetUInt(CreatorOSOffset, value);
            }
        }

        public uint RevisionLevel
        {
            get
            {
                return this.block->GetUInt(RevisionLevelOffset);
            }
            set
            {
                this.block->SetUInt(RevisionLevelOffset, value);
            }
        }

        public ushort DefaultReservedUserID
        {
            get
            {
                return this.block->GetUShort(DefaultReservedUserIDOffset);
            }
            set
            {
                this.block->SetUShort(DefaultReservedUserIDOffset, value);
            }
        }

        public ushort DefaultReservedGroupID
        {
            get
            {
                return this.block->GetUShort(DefaultReservedGroupIDOffset);
            }
            set
            {
                this.block->SetUShort(DefaultReservedGroupIDOffset, value);
            }
        }

        public uint FirstINode
        {
            get
            {
                return this.block->GetUInt(FirstINodeOffset);
            }
            set
            {
                this.block->SetUInt(FirstINodeOffset, value);
            }
        }

        public ushort INodeSize
        {
            get
            {
                return this.block->GetUShort(INodeSizeOffset);
            }
            set
            {
                this.block->SetUShort(INodeSizeOffset, value);
            }
        }

        public ushort BlockGroupNR
        {
            get
            {
                return this.block->GetUShort(BlockGroupNROffset);
            }
            set
            {
                this.block->SetUShort(BlockGroupNROffset, value);
            }
        }

        public uint FeaturesCompatible
        {
            get
            {
                return this.block->GetUInt(FeaturesCompatibleOffset);
            }
            set
            {
                this.block->SetUInt(FeaturesCompatibleOffset, value);
            }
        }

        public uint FeaturesIncompatible
        {
            get
            {
                return this.block->GetUInt(FeaturesIncompatibleOffset);
            }
            set
            {
                this.block->SetUInt(FeaturesIncompatibleOffset, value);
            }
        }

        public uint FeaturesReadOnlyCompatible
        {
            get
            {
                return this.block->GetUInt(FeaturesReadOnlyCompatibleOffset);
            }
            set
            {
                this.block->SetUInt(FeaturesReadOnlyCompatibleOffset, value);
            }
        }

        public byte* UUID
        {
            get
            {
                return this.block->GetByteArray(16, UUIDOffset);
            }
            set
            {
                this.block->SetByteArray(16, UUIDOffset, value);
            }
        }

        public byte* VolumeName
        {
            get
            {
                return this.block->GetByteArray(16, VolumeNameOffset);
            }
            set
            {
                this.block->SetByteArray(16, VolumeNameOffset, value);
            }
        }

        public byte* LastMounted
        {
            get
            {
                return this.block->GetByteArray(64, LastMountedOffset);
            }
            set
            {
                this.block->SetByteArray(64, LastMountedOffset, value);
            }
        }

        public uint AlgorithmsBitmap
        {
            get
            {
                return this.block->GetUInt(AlgorithmsBitmapOffset);
            }
            set
            {
                this.block->SetUInt(AlgorithmsBitmapOffset, value);
            }
        }
	}
}
