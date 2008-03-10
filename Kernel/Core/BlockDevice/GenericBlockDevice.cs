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
using System.Collections.Generic;
using System.Text;

namespace SharpOS.Kernel.BlockDevice
{
	public enum IORequestType : byte
	{
		Read = 0,
		Write = 1,
	}

	public static class GenericBlockDevice
	{
		public const uint KernelSectorSize = 512; //TODO: move to global area

		public static int DoRequest(IBlockDevice device, IORequestType request, uint sector, uint nsectors, ByteBuffer buff)
		{
			uint blocksize = device.GetBlockSize();

			if (KernelSectorSize == blocksize)
				return device.Request(request, sector, nsectors, buff);

			uint multiple = blocksize / KernelSectorSize;
			uint modulus = blocksize % KernelSectorSize;

			if (modulus == 0) {
				return device.Request(request, KernelSectorSize, nsectors * multiple, buff);
			}

			// only devices with block sizes which are multiples of the kernel sector size are supported
			//TODO: raise exception

			return 0;
		}

	}


}
