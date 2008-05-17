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
	class GenericBlockDevicePartition : IBlockDevice
	{
		protected IBlockDevice blockDevice;
		protected uint sectors;
		protected uint offset;

		public GenericBlockDevicePartition (IBlockDevice blockdevice, uint offset, uint sectors)
		{
			this.blockDevice = blockdevice;
			this.offset = offset;
			this.sectors = sectors;
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

			return blockDevice.ReadBlock (sector + offset, nsectors, buff);
		}

		public bool WriteBlock (uint sector, uint nsectors, MemoryBlock buff)
		{
			if (sector + nsectors >= sectors)
				return false;

			return blockDevice.WriteBlock (sector + offset, nsectors, buff);
		}

		public uint GetSectorSize ()
		{
			return blockDevice.GetSectorSize ();
		}

		public uint GetTotalSectors ()
		{
			return sectors;
		}

		public bool CanWrite ()
		{
			return blockDevice.CanWrite ();
		}

		public IDevice GetDeviceDriver ()
		{
			return blockDevice.GetDeviceDriver ();
		}
	}
}

