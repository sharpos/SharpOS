// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
//  Phil Garcia (aka tgiphil) <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SharpOS.Kernel;
using SharpOS.AOT.IR;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.ADC
{
	public static class IO
	{

		#region Read8
		[AOTAttr.ADCStub]
		public unsafe static byte Read8 (uint port)
		{
			return 0;
		}
		#endregion

		#region ReadByte
		[AOTAttr.ADCStub]
		public unsafe static byte ReadByte (uint port)
		{
			return Read8 (port);
		}
		#endregion

		#region Read16
		[AOTAttr.ADCStub]
		public unsafe static UInt16 Read16 (uint port)
		{
			return 0;
		}
		#endregion

		#region Read32
		[AOTAttr.ADCStub]
		public unsafe static UInt32 Read32 (uint port)
		{
			return 0;
		}
		#endregion

		#region Write8
		[AOTAttr.ADCStub]
		public unsafe static void Write8 (uint port, byte value)
		{
		}
		#endregion

		#region WriteByte
		[AOTAttr.ADCStub]
		public unsafe static void WriteByte (uint port, byte value)
		{
			Write8 (port, value);
		}
		#endregion

		#region Write16
		[AOTAttr.ADCStub]
		public unsafe static void Write16 (uint port, UInt16 value)
		{
		}
		#endregion

		#region Write32
		[AOTAttr.ADCStub]
		public unsafe static void Write32 (uint port, UInt32 value)
		{
		}
		#endregion

		#region Delay
		[AOTAttr.ADCStub]
		public unsafe static void Delay ()
		{
		}
		#endregion
	}
}
