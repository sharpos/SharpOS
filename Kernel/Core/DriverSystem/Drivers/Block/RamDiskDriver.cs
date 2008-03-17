// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.Kernel.DriverSystem;

namespace SharpOS.Kernel.DriverSystem.Drivers.Block
{
	public class RamDiskDriver : GenericDriver, IBlockDeviceController
	{
		private MemoryBlock ram;
		private uint blocks;
		private uint blockSize = 512;

		#region Initialize
		public override bool Initialize (IDriverContext context)
		{
			Diagnostics.Message("Ram Disk Controller");

			context.Initialize(DriverFlags.IOStream8Bit);	// ??

			// Question: what size? how is this passed in?
			blocks = 1024 * 2;
			ram.Allocate(blockSize * blocks);	// 1 Meg

			Diagnostics.Message("-->Creating Ram Disk Found");
			Diagnostics.Message("--->Size in KB: ", (int)blockSize / 2);

			DeviceResource resource = new DeviceResource("ramdisk1", DeviceResourceType.RamDisk, new GenericBlockDeviceAdapter(this, 0), DeviceResourceStatus.Online, 0, 0);
			DeviceResourceManager.Add(resource);

			return (isInitialized = false);
		}

		public int Open (uint drive)
		{
			return 0;
		}

		public int Release (uint drive)
		{
			return 0;
		}

		public uint GetBlockSize (uint drive)
		{
			return blockSize;
		}

		public uint GetTotalBlocks (uint drive)
		{
			return blocks;
		}

		public bool CanWrite (uint drive)
		{
			return true;
		}

		public unsafe bool ReadBlock (uint drive, uint lba, uint count, MemoryBlock memory)
		{
			if (lba + count > blocks)
				return false;

			memory.CopyFrom(ram.Offset(blockSize * lba), blockSize * count);
			return true;
		}

		public unsafe bool WriteBlock (uint drive, uint lba, uint count, MemoryBlock memory)
		{
			if (lba + count > blocks)
				return false;

			memory.CopyTo(ram.Offset(blockSize * lba), blockSize * count);
			return true;
		}

		public IDevice GetDeviceDriver ()
		{
			return null;
			//return (IDevice)this;
		}

		#endregion
	}
}
