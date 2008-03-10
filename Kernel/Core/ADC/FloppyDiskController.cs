// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Jae Hyun Roh <wonbear@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Text;
using System.Runtime.InteropServices;
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.ADC
{
	public unsafe class FloppyDiskController
	{
		[AOTAttr.ADCStub]
		public static void Setup()
		{
		}

		[AOTAttr.ADCStub]
		public static bool ReadBlock(uint drive, uint lba, uint count, byte* memory)
		{
			return false;
		}

		[AOTAttr.ADCStub]
		public static bool WriteBlock(uint drive, uint lba, uint count, byte* memory)
		{
			return false;
		}

	}
}
