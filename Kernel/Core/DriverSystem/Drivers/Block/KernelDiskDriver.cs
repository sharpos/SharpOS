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

namespace SharpOS.Kernel.DriverSystem.Drivers.Block
{
	public class KernelDiskDriver : GenericDriver, IBlockDeviceController
	{
		private MemoryBlock ram;
		private uint blocks;
		private uint blockSize = 512;

		#region Initialize
		public unsafe override bool Initialize (IDriverContext context)
		{
			Diagnostics.Message ("Kernel Disk Controller");

			context.Initialize (DriverFlags.IOStream8Bit);	// ??

			// Question: what size? how is this passed in?
			void* fat = (void*)Stubs.GetLabelAddress ("SharpOS.Kernel/Resources/fat12.img");

			if (fat == null)
				return true;

			blocks = 1474560 / 2;
			ram = new MemoryBlock ((uint)fat, blocks * blockSize);

			Diagnostics.Message ("-->Embedded Kernel Disk Found");
			Diagnostics.Message ("--->Size in KB: ", (int)blocks / 2);

			DeviceResource resource = new DeviceResource ("embedded", DeviceResourceType.RamDisk, new GenericBlockDeviceAdapter (this, 0), DeviceResourceStatus.Online, 0, 0);
			DeviceResourceManager.Add (resource);

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

			memory.CopyFrom (ram.Offset (blockSize * lba), blockSize * count);
			return true;
		}

		public unsafe bool WriteBlock (uint drive, uint lba, uint count, MemoryBlock memory)
		{
			if (lba + count > blocks)
				return false;

			memory.CopyTo (ram.Offset (blockSize * lba), blockSize * count);
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
