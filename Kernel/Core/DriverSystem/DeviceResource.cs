using System;
using System.Text;
using SharpOS.Kernel.DriverSystem.Drivers.Block;

namespace SharpOS.Kernel.DriverSystem
{
	enum DeviceResourceType : byte
	{
		None = 0x00,	// Nothing at all

		// Alias to another device resource (example: /devices/cdrom to /device/idecd01)
		Alias,

		// Block Devices
		RamDisk,
		FloppyDisk,
		HardDrive,
		RemovableMedia,	// CDs & DVDs (maybe USB drives too)
		DiskPartition,
		Tape,

		// Character Devices
		Keyboard,
		Mouse,
		// Bi-Directional
		Serial,
		Parallel,
	}

	enum DeviceResourceStatus : byte
	{
		None = 0x00,		// None (doesn't exists)
		Online,
		Offline,
		Reserved,
		Error,				// resource is out of order
		MaxedOut,			// for internal errors
		UnableToLocated,	// for internal errors
	}

	struct DeviceResource
	{
		public uint ID;
		public String Name;
		public DeviceResourceType Type;
		public IBlockDevice BlockDevice;	// may be null
		public DeviceResourceStatus Status;
		public uint Major;			// not the same as in unix/linux
		public uint Minor;			// not the same as in unix/linux
		public uint ParentID;

		public DeviceResource (String name, DeviceResourceType type, IBlockDevice blockDevice, DeviceResourceStatus status, uint major, uint minor)
		{
			this.ID = DeviceResourceManager.GetUniqueResourceID ();
			this.ParentID = 0;
			this.Name = name;
			this.Type = type;
			this.BlockDevice = blockDevice;
			this.Status = status;
			this.Major = major;
			this.Minor = minor;
		}

		public DeviceResource (uint parentID, String name, DeviceResourceType type, IBlockDevice blockDevice, DeviceResourceStatus status, uint major, uint minor)
		{
			this.ID = DeviceResourceManager.GetUniqueResourceID ();
			this.ParentID = parentID;
			this.Name = name;
			this.Type = type;
			this.BlockDevice = blockDevice;
			this.Status = status;
			this.Major = major;
			this.Minor = minor;
		}

	}
}
