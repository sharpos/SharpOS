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
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.DriverSystem.Drivers.Block;

namespace SharpOS.Kernel.FileSystem
{
	public static class VolumeManager
	{

		public struct Volume
		{
			public uint VolumeID;
			public IBlockDevice Device;
			public IFileSystem Filesystem;
			public char DriveLetter;

			public Volume (IBlockDevice device, IFileSystem filesystem)
			{
				this.VolumeID = VolumeManager.GetUniqueVolumeID ();
				this.Device = device;
				this.Filesystem = filesystem;
				this.DriveLetter = ' ';
			}

			public Volume (IBlockDevice device, IFileSystem filesystem, char driveletter)
			{
				this.VolumeID = VolumeManager.GetUniqueVolumeID ();
				this.Device = device;
				this.Filesystem = filesystem;
				this.DriveLetter = driveletter;
			}
		}

		private const uint MaxVolumes = 256;

		private static Volume[] volumes;
		private static uint volumeCount = 0;
		private static uint volumeID = 0;

		private static Volume NullVolume;

		private static SpinLock spinLock;

		public static void Initialize ()
		{
			volumes = new Volume[MaxVolumes];
			volumeCount = 0;
			NullVolume.VolumeID = 0;
			NullVolume.DriveLetter = ' ';
		}

		public static uint GetUniqueVolumeID ()
		{
			try {
				spinLock.Enter ();

				return ++volumeID;
			}
			finally {
				spinLock.Exit ();
			}
		}

		public static int Mount (IFileSystem filesystem, IBlockDevice device)
		{
			return Mount (filesystem, device, ' ');
		}

		public static int Mount (IFileSystem filesystem, IBlockDevice device, char driveletter)
		{
			spinLock.Enter ();

			try {
				if (volumeCount >= MaxVolumes)
					return -1;

				driveletter = NormalizeDriveLetter (driveletter);

				if (GetVolumeSlotByDriveLetter (driveletter) >= 0)
					return -2;

				volumes[volumeCount] = new Volume (device, filesystem, driveletter);
				volumeCount++;

				return (int)(volumeCount - 1);
			}
			finally {
				spinLock.Exit ();
			}
		}

		public static int Unmount (int volumeid)
		{
			spinLock.Enter ();

			try {
				for (int slot = 0; slot < volumeCount; slot++)
					if (volumes[slot].VolumeID == volumeid) {
						// if able to unmount safetly
						//TODO: add check here.

						volumeCount--;
						volumes[slot] = volumes[volumeCount];

						return 0;
					}

				return -1;
			}
			finally {
				spinLock.Exit ();
			}
		}

		public static Volume GetByDriveLetter (char driveletter)
		{
			driveletter = NormalizeDriveLetter (driveletter);

			for (int slot = 0; slot < volumeCount; slot++)
				if (volumes[slot].DriveLetter == driveletter)
					return volumes[slot];

			return NullVolume;
		}

		public static char NormalizeDriveLetter (char driveletter)
		{
			if ((driveletter >= 'A') || (driveletter >= 'Z'))
				return driveletter;

			if ((driveletter >= 'a') || (driveletter >= 'z'))
				return (char) ('A' + driveletter - 'A');

			return ' ';
		}

		private static int GetVolumeSlotByDriveLetter (char driveletter)
		{
			driveletter = NormalizeDriveLetter (driveletter);

			if (driveletter == ' ')
				return -1;

			for (int slot = 0; slot < volumeCount; slot++)
				if (volumes[slot].DriveLetter == driveletter)
					return slot;

			return -1;
		}
	}
}
