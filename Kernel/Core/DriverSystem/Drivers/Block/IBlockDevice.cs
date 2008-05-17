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
using SharpOS.Kernel.DriverSystem;

namespace SharpOS.Kernel.DriverSystem.Drivers.Block
{
	public interface IBlockDevice
	{
		// block device initialization 
		int Open ();
		int Release ();

		bool ReadBlock (uint sector, uint nsectors, MemoryBlock buff);
		bool WriteBlock (uint sector, uint nsectors, MemoryBlock buff);

		uint GetSectorSize ();
		uint GetTotalSectors ();
		bool CanWrite ();

		IDevice GetDeviceDriver ();
	}
}
