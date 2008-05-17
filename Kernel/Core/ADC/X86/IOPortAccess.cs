// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
//  Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS.Kernel.ADC.X86
{
	public static class IOPortAccess
	{

		#region ReadByte
		public unsafe static byte Read8 (uint port)
		{
			ushort uport = (ushort) port;
			byte value = 0;

			Asm.XOR (R32.EAX, R32.EAX);
			Asm.MOV (R16.DX, (ushort*)&uport);
			Asm.IN_AL__DX ();
			Asm.MOV (&value, R8.AL);

			return value;
		}
		#endregion


		#region ReadUInt16
		public unsafe static UInt16 Read16 (uint port)
		{
			ushort uport = (ushort) port;
			ushort value = 0;

			Asm.XOR (R32.EAX, R32.EAX);
			Asm.MOV (R16.DX, (ushort*)&uport);
			Asm.IN_AX__DX ();
			Asm.MOV (&value, R16.AX);

			return value;
		}
		#endregion

		#region ReadUInt32
		public unsafe static UInt32 Read32 (uint port)
		{
			ushort uport = (ushort) port;
			uint value = 0;

			Asm.XOR (R32.EAX, R32.EAX);
			Asm.MOV (R16.DX, (ushort*)&uport);
			Asm.IN_EAX__DX ();
			Asm.MOV (&value, R32.EAX);

			return value;
		}
		#endregion

		#region WriteByte
		public unsafe static void Write8 (uint port, byte value)
		{
			ushort uport = (ushort) port;
			Asm.MOV (R16.DX, (ushort*)&uport);
			Asm.MOV (R8.AL, &value);
			Asm.OUT_DX__AL ();
		}
		#endregion

		#region WriteUInt16
		public unsafe static void Write16 (uint port, UInt16 value)
		{
			ushort uport = (ushort) port;
			Asm.MOV (R16.DX, (ushort*)&uport);
			Asm.MOV (R16.AX, &value);
			Asm.OUT_DX__AX ();
		}
		#endregion

		#region Write32
		public unsafe static void Write32 (uint port, UInt32 value)
		{
			ushort uport = (ushort) port;
			Asm.MOV (R16.DX, (ushort*)&uport);
			Asm.MOV (R32.EAX, &value);
			Asm.OUT_DX__EAX ();
		}
		#endregion

		#region Delay
		public unsafe static void Delay ()
		{
			Asm.IN_AL (0x80);
			Asm.OUT__AL (0x80);
		}
		#endregion
	}
}
