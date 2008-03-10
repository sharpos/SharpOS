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
using SharpOS.Kernel.BlockDevice;

namespace SharpOS.Kernel.FileSystem
{
	public static class VolumeManager
	{
		private const uint InitialBlockDeviceAllocation = 256;
		private static IBlockDevice[] devices;
		private static IFileSystem[] filesystems;
		private static int[] driveletters;

		private static uint volumecount = 0;

		private static object sync;

		public static void Initialize()
		{
			devices = new IBlockDevice[InitialBlockDeviceAllocation];
			filesystems = new IFileSystem[InitialBlockDeviceAllocation];
			driveletters = new int[26];	// temp. until VFS implemented
			sync = new object();
		}

		public static int Mount(IFileSystem filesystem, IBlockDevice device)
		{
			return Mount(filesystem, device, ' ');
		}

		public static int Mount(IFileSystem filesystem, IBlockDevice device, char driveletter)
		{
			//lock (sync)
			{
				if (volumecount >= devices.Length)
					return -1;

				if (driveletter != ' ') {
					int slot = DriveLetterToSlot(driveletter);

					if (driveletters[slot] == 0)			// using trick
						return -2; // already used 

					driveletters[slot] = (int)volumecount + 1;	// trick, add one
				}

				devices[volumecount] = device;
				filesystems[volumecount] = filesystem;

				return (int)++volumecount;
			}
		}

		public static int Unmount(int volumeid)
		{
			//lock (sync)
			{
				if (devices[volumeid] == null)
					return 0;

				// todo lock, flush, and if volume then unmount.
				devices[volumeid] = null;
				filesystems[volumeid] = null;

				return 0;
			}
		}

		public static int DriveLetterToSlot(char letter)
		{
			if (letter == ' ')
				return -1;
			else if (letter >= 'A' || letter <= 'Z')
				return letter - 'A';
			else if (letter >= 'a' || letter <= 'z')
				return letter - 'a';
			else
				return -1;
		}

		public static IFileSystem GetFileSystemByDriveLetter(char driveletter)
		{
			int slot = DriveLetterToSlot(driveletter);

			if (slot < 0)
				return null;

			int id = driveletters[slot];

			if (id <= 0)
				return null;

			return filesystems[slot - 1];	// reverse trick
		}
	}
}
