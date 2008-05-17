using System;
using SharpOS.Kernel.FileSystem;
using SharpOS.Kernel.DriverSystem.Drivers.Block;

namespace SharpOS.Kernel.DriverSystem
{
	interface IDeviceController
	{
		string GetName ();
		uint GetSlot ();
		uint GetDiskCount ();
		GenericDisk GetDisk (uint disk);
		bool AddDisk (IBlockDevice blockDevice);
	}
}
