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
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.DriverSystem;

namespace SharpOS.Kernel.DriverSystem.Drivers.Block
{
	public class RamDiskDriver : GenericDriver, IBlockDeviceController
	{
		protected const uint MaxRamDisks = 9;

		protected uint blockSize = 512;

		protected struct RamDisk
		{
			public MemoryBlock ram;
			public uint blocks;
		}

		protected RamDisk[] ramDisks;
		protected uint diskCount;

		#region Initialize
		public override bool Initialize (IDriverContext context)
		{
			//TextMode.WriteLine ("ramdisk: Ram Disk Controller");

			ramDisks = new RamDisk[MaxRamDisks];
			diskCount = 0;

			DeviceController deviceController = new DeviceController ("ramdisk", 0);

			context.Initialize ();
			isInitialized = true;

			TextMode.WriteLine ("ramdisk: driver installed, max: ", (int)MaxRamDisks);

			CreateDisk (deviceController, 1024 * 2);

			return true;
		}

		protected bool CreateDisk (DeviceController deviceController, uint blocks)
		{
			if (diskCount >= MaxRamDisks)
				return false;

			ramDisks[diskCount].blocks = blocks;
			ramDisks[diskCount].ram.Allocate (blockSize * blocks);

			//TextMode.Write (ramDiskNames[diskCount]);
			TextMode.Write ("Disk #");
			TextMode.Write ((int)diskCount);
			TextMode.Write (" - ", (int)(ramDisks[diskCount].blocks * 2));
			TextMode.Write ("KB");
			TextMode.WriteLine ();

			deviceController.AddDisk (new GenericBlockDeviceAdapter (this, diskCount));

			//DeviceResource resource = new DeviceResource (ramDiskNames[diskCount], DeviceResourceType.RamDisk, new GenericBlockDeviceAdapter (this, 0), DeviceResourceStatus.Online, 0, 0);
			//DeviceResourceManager.Add (resource);

			DeviceControllers.Add (deviceController);

			diskCount++;
			return true;
		}

		public int Open (uint drive)
		{
			return 0;
		}

		public int Release (uint drive)
		{
			return 0;
		}

		public uint GetSectorSize (uint drive)
		{
			return blockSize;
		}

		public uint GetTotalSectors (uint drive)
		{
			return ramDisks[diskCount].blocks;
		}

		public bool CanWrite (uint drive)
		{
			return true;
		}

		public unsafe bool ReadBlock (uint drive, uint lba, uint count, MemoryBlock memory)
		{
			if (lba + count > ramDisks[diskCount].blocks)
				return false;

			ramDisks[drive].ram.CopyFrom (ramDisks[drive].ram.Offset (blockSize * lba), blockSize * count);
			return true;
		}

		public unsafe bool WriteBlock (uint drive, uint lba, uint count, MemoryBlock memory)
		{
			if (lba + count > ramDisks[diskCount].blocks)
				return false;

			ramDisks[drive].ram.CopyTo (ramDisks[drive].ram.Offset (blockSize * lba), blockSize * count);
			return true;
		}

		public IDevice GetDeviceDriver ()
		{
			return (IDevice)this;
		}

		#endregion
	}
}
