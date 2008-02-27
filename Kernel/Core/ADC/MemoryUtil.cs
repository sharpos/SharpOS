//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using AOTAttr = SharpOS.AOT.Attributes;
using SharpOS.Korlib.Runtime;

namespace SharpOS.Kernel.ADC {
	public static class MemoryUtil {
		[AOTAttr.ADCStub]
		public unsafe static void Call (void* functionPointer, void* pointeredParameter)
		{
		}

		[AOTAttr.ADCStub]
		public unsafe static void MemSet(uint value, uint dst, uint count)
		{
		}

		[AOTAttr.ADCStub]
		public unsafe static void MemSet32(uint value, uint dst, uint count)
		{
		}

		[AOTAttr.ADCStub]
		public static uint BitCount(byte value)
		{
			return 0;
		}

		[AOTAttr.ADCStub]
		public static uint BitCount(ushort value)
		{
			return 0;
		}

		[AOTAttr.ADCStub]
		public static uint BitCount(uint value)
		{
			return 0;
		}

		[AOTAttr.ADCStub]
		public static uint BitCount(ulong value)
		{
			return 0;
		}
		
		[AOTAttr.ADCStub]
		public static uint NextPowerOf2(uint x)
		{
			return 0;
		}

		[AOTAttr.ADCStub]
		public unsafe static void MemCopy(uint src, uint dst, uint count)
		{
		}

		[AOTAttr.ADCStub]
		public unsafe static void MemCopy32 (uint src, uint dst, uint count)
		{
		}

		[AOTAttr.ADCStub]
		public unsafe static void Call (uint address, uint value)
		{
		}
	}
}
