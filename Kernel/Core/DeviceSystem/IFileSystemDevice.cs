using System;
using SharpOS.Kernel.FileSystem;

namespace SharpOS.Kernel.DeviceSystem
{
	public interface IFileSystemDevice
	{
        GenericFileSystem Create(IPartitionDevice partition);
	}
}
