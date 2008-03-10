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
using SharpOS.Kernel.Foundation;

namespace SharpOS.Kernel.BlockDevice
{
	public class FloppyDisk : IBlockDevice
	{
		protected uint sectors = 2880; // standard 1.44M Floppy
		protected byte drive = 0;

		public FloppyDisk()
		{
		}

		public int Open()
		{
			//TODO: determine # of sectors & hardwaresectorsize
			return 0;
		}

		public int Release()
		{
			return 0;
		}

		public uint GetBlockSize()
		{
			return 512;		// 512 bytes per block (sector)
		}

		public uint GetTotalBlocks()
		{
			return sectors;	
		}

		public bool CanWrite()
		{
			return true;
		}

		public unsafe int Request(IORequestType request, uint sector, uint count, ByteBuffer buff)
		{
			if (buff.data == null)
				return -1;

			if (sector + count > sectors)
				return -1;

			if (request == IORequestType.Read) {
				FloppyDiskController.ReadBlock(drive, sector, count, buff.data);
			}
			else if (request == IORequestType.Write)
				FloppyDiskController.WriteBlock(drive, sector, count, buff.data);

			return 0;
		}

	}
}
