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
	public static unsafe class KernelDisk
	{
		private static void* fat = null; // (void*)Stubs.GetLabelAddress("SharpOS.Kernel/Resources/fat12.img");

		public static ContinuousRamDisk RamDisk = null;

		public static void Setup()
		{
			if (fat == null)
				return;

			RamDisk = new ContinuousRamDisk((byte*)fat, 1474560);	//TODO: dynamically determine resource size (instead of hard coding 1.44Mb)			
		}

	}
}
