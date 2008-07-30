//
// (C) 2006-2008 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Michael Ruck (aka grover) <sharpos@michaelruck.de>
//  Phil Garcia (aka tgiphil) <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System.IO;
using SharpOS.Kernel.FileSystem;
using SharpOS.Kernel.DeviceSystem;

namespace SharpOS.Kernel.Vfs
{
	public sealed class FileSystemFactory
	{
		/// <summary>
		/// This function iterates all running file system drivers, which have registered themselves 
		/// beneath the /system/filesystems and ask them if they can mount this path.			 
		/// </summary>
		/// <param name="path">The path to the partition</param>
		/// <returns></returns>
		public static IFileSystem CreateFileSystem (string path)
		{
			string fullPath = path.Substring (1);

			// since we don't have /system/devices yet, just look for the partition devices in the DeviceManager
			//Device[] partitions = DeviceManager.GetDevicesOf (typeof (IPartitionDevice), fullPath);

			IPartitionDevice partition = null;

			foreach (Device device in DeviceManager.GetOnlineDevicesWithName (fullPath))
				if (device is IPartitionDevice) {
					partition = (IPartitionDevice)device;
					break;
				}

			if (partition == null)
				return null;

			foreach (Device device in DeviceManager.GetOnlineDevices ()) {
				if (device is IFileSystemService) {
					GenericFileSystem fs = (device as IFileSystemDevice).Create (partition);

					if (fs.Valid)
						return fs.CreateVFSMount ();
				}
			}

			return null;
		}
	}
}
