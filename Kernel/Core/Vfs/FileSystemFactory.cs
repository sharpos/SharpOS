//
// (C) 2006-2008 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Michael Ruck (aka grover) <sharpos@michaelruck.de>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System.IO;

namespace SharpOS.Kernel.Vfs {
	public sealed class FileSystemFactory {
		public static IFileSystem CreateFileSystem(string path)
		{
			/*
			 * Algorithm:
			 * 
			 * This function iterates all running file system drivers, which have registered themselves
			 * beneath the /proc/filesystems and ask them if they can mount this path.
			 * 
			 */

			IFileSystem result = null;

			//System.IO.DirectoryInfo fsFolder = (System.IO.DirectoryInfo)VirtualFileSystem.Open("/proc/filesystems", FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
			//FileInfo[] files = fsFolder.GetFiles();
			//foreach (FileInfo entry in files)
			//{
			//    IFileSystemService fss = VirtualFileSystem.Open(entry.FullName, FileAccess.Read, FileShare.Delete | FileShare.ReadWrite) as IFileSystemService;
			//    if (null != fss)
			//    {
			//        result = fss.MountDevice(path);
			//        if (null != result)
			//        {
			//            break;
			//        }
			//    }
			//}
			return result;
		}
	}
}
