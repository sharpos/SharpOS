using System;
using SharpOS.Kernel.DeviceSystem;
using SharpOS.Kernel.FileSystem;

namespace SharpOS.Kernel.FileSystem.FATFileSystem
{
	public class FATDevice : Device, IFileSystemDevice
	{
		public FATDevice ()
		{
			this.name = "FAT";			
			this.parent = null;
			this.deviceStatus = DeviceStatus.Online;
		}

		public static void Register ()
		{
			DeviceManager.Add (new FATDevice());
		}

        public GenericFileSystem Create(IPartitionDevice partition)
		{
			return new FAT (partition);
		}

	}
}
