using System;
using SharpOS.Kernel.DriverSystem;

namespace SharpOS.Kernel.DriverSystem.Drivers.Block
{
	class GenericBlockDeviceAdapter : IBlockDevice
	{
		protected IBlockDeviceController controller;
		protected uint drive;

		public GenericBlockDeviceAdapter (IBlockDeviceController controller, uint drive)
		{
			this.controller = controller;
			this.drive = drive;
		}

		public int Open ()
		{
			return 0;
		}

		public int Release ()
		{
			return 0;
		}

		public bool ReadBlock (uint sector, uint nsectors, MemoryBlock buff)
		{
			return controller.ReadBlock (drive, sector, nsectors, buff);
		}

		public bool WriteBlock (uint sector, uint nsectors, MemoryBlock buff)
		{
			return controller.WriteBlock (drive, sector, nsectors, buff);
		}

		public uint GetBlockSize ()
		{
			return controller.GetBlockSize (drive);
		}

		public uint GetTotalBlocks ()
		{
			return controller.GetTotalBlocks (drive);
		}

		public bool CanWrite ()
		{
			return controller.CanWrite (drive);
		}

		public IDevice GetDeviceDriver ()
		{
			return controller.GetDeviceDriver ();
		}
	}
}
