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
using SharpOS.Kernel.DriverSystem.Drivers.Block;

namespace SharpOS.Kernel.FileSystem
{
	public static class DiskManager
	{
		private static GenericDisk[] disks;

		public static void Initialize()
		{
			disks = new GenericDisk[256]; // upto 256 disks
		}

		public static bool Register(GenericDisk disk)
		{
			for (int index = 0; index < disks.Length; index++)
				if (disks[index] == null) {
					disks[index] = disk;
					return true;
				}

			return false;
		}

		public static bool Unregister(GenericDisk disk)
		{
			for (int index = 0; index < disks.Length; index++)
				if (disks[index] == disk) {
					//TODO: check if active, otherwise abort + plus other stuff
					disks[index] = null;
					return true;
				}

			return false;
		}
	}
}
