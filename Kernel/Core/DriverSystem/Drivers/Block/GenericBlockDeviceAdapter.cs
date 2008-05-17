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
	class GenericBlockDeviceAdapter : IBlockDevice
	{
		protected IBlockDeviceController controller;
		protected uint drive;
		protected uint offset;
		protected uint sectors;

		public GenericBlockDeviceAdapter (IBlockDeviceController controller, uint drive)
		{
			this.controller = controller;
			this.drive = drive;
			this.offset = 0;
			this.sectors = controller.GetTotalSectors (drive);
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
			if (sector + nsectors >= sectors)
				return false;

			return controller.ReadBlock (drive, sector + offset, nsectors, buff);
		}

		public bool WriteBlock (uint sector, uint nsectors, MemoryBlock buff)
		{
			if (sector + nsectors >= sectors)
				return false;

			return controller.WriteBlock (drive, sector + offset, nsectors, buff);
		}

		public uint GetSectorSize ()
		{
			return controller.GetSectorSize (drive);
		}

		public uint GetTotalSectors ()
		{
			return sectors;
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
