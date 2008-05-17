using System;
using SharpOS.Kernel.FileSystem;
using SharpOS.Kernel.DriverSystem.Drivers.Block;

namespace SharpOS.Kernel.DriverSystem
{

	public class DeviceController : IDeviceController
	{
		public const uint MaxSupportedDisks = 256;

		protected string name;
		protected uint slot;
		protected GenericDisk[] genericDisks;
		protected uint diskCount;

		public DeviceController (string name, uint slot)
		{
			this.name = name;
			this.slot = slot;
			this.diskCount = 0;
			genericDisks = new GenericDisk[MaxSupportedDisks];
		}

		public string GetName ()
		{
			return name;
		}

		public uint GetSlot ()
		{
			return slot;
		}

		public bool AddDisk (IBlockDevice blockDevice)
		{
			if (diskCount >= MaxSupportedDisks) {
				//TODO: throw an exception
				return false;
			}

			genericDisks[diskCount] = new GenericDisk (blockDevice);
			diskCount++;

			return true;
		}

		public uint GetDiskCount ()
		{
			return diskCount;
		}

		public GenericDisk GetDisk (uint disk)
		{
			if (disk > diskCount) {
				//TODO: throw an exception
				return null;
			}

			return genericDisks[disk];
		}

	}
}
