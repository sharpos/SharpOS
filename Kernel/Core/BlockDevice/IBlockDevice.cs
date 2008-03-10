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
	public interface IBlockDevice
	{
		// block device initialization 
		int Open(); //TODO: pass in some parameters
		int Release();

		// non-request-queue IO
		int Request(IORequestType request, uint sector, uint nsectors, ByteBuffer buff);

		uint GetBlockSize();
		uint GetTotalBlocks();
		bool CanWrite();
	}
}
