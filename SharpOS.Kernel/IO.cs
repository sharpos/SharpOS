// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SharpOS;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS.ADC.X86 {
	public class IO {
		public unsafe static byte In8 (ushort port)
		{
			byte value = 0;

			Asm.XOR (R32.EAX, R32.EAX);
			Asm.MOV (R16.DX, &port);
			Asm.IN_AL__DX ();
			Asm.MOV (&value, R8.AL);

			return value;
		}

		public unsafe static ushort In16 (ushort port)
		{
			ushort value = 0;

			Asm.XOR (R32.EAX, R32.EAX);
			Asm.MOV (R16.DX, &port);
			Asm.IN_AX__DX ();
			Asm.MOV (&value, R16.AX);

			return value;
		}

		public unsafe static uint In32 (ushort port)
		{
			uint value = 0;

			Asm.XOR (R32.EAX, R32.EAX);
			Asm.MOV (R16.DX, &port);
			Asm.IN_EAX__DX ();
			Asm.MOV (&value, R32.EAX);

			return value;
		}

		public unsafe static void Out8 (ushort port, byte value)
		{
			Asm.MOV (R16.DX, &port);
			Asm.MOV (R8.AL, &value);
			Asm.OUT_DX__AL ();
		}

		public unsafe static void Out16 (ushort port, ushort value)
		{
			Asm.MOV (R16.DX, &port);
			Asm.MOV (R16.AX, &value);
			Asm.OUT_DX__AX ();
		}

		public unsafe static void Out32 (ushort port, uint value)
		{
			Asm.MOV (R16.DX, &port);
			Asm.MOV (R32.EAX, &value);
			Asm.OUT_DX__EAX ();
		}

		public unsafe static void Delay ()
		{
			Asm.IN_AL (0x80);
			Asm.OUT__AL (0x80);
		}
	}
}

