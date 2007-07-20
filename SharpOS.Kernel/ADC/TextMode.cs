// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.Runtime.InteropServices;
using SharpOS;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using SharpOS.ADC.X86;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.ADC {
	public unsafe class TextMode
	{		[AOTAttr.ADCStub]
		public static void Setup()
		{
		}

		[AOTAttr.ADCStub]
		public unsafe static void SetCursorSize (byte _start, byte _end)
		{
		}

		[AOTAttr.ADCStub]
		public unsafe static void SetCursor(int _x, int _y)
		{
		}

		[AOTAttr.ADCStub]
		public unsafe static int GetX()
		{
			return 0;
		}
		
		[AOTAttr.ADCStub]
		public unsafe static int GetY()
		{
			return 0;
		}

		[AOTAttr.ADCStub]
		public unsafe static void GoTo (int _x, int _y)
		{
		}

		[AOTAttr.ADCStub]
		public unsafe static void ClearScreen ()
		{
		}

		[AOTAttr.ADCStub]
		public unsafe static void WriteNumber (int value, bool hex)
		{
		}

		public unsafe static void WriteNumber (int value)
		{
			WriteNumber (value, false);
		}
		
		[Obsolete ("Use WriteNumber (int value, bool hex) instead")]
		public unsafe static void WriteNumber (bool hex, int value)
		{
		
		}
		
		public unsafe static void WriteLine (byte* message)
		{
			Write (message);
			WriteLine ();
		}

		public unsafe static void WriteLine (byte* message, int value)
		{
			Write (message);
			WriteNumber (value, true);
			WriteLine ();
		}

		public unsafe static void Write (byte* message)
		{
			for (int i = 0; message [i] != 0; i++)
				WriteChar (message [i]);
		}

		[AOTAttr.ADCStub]
		public unsafe static void WriteChar (byte value)
		{
		}

		[AOTAttr.ADCStub]
		public unsafe static void ScrollPage(int value)
		{
		}

		[AOTAttr.ADCStub]
		public unsafe static void WriteString (UInt32 value)
		{
		}

		public unsafe static void WriteLine ()
		{
			WriteChar ((byte) '\n');
		}

		[AOTAttr.ADCStub]
		public unsafe static void WriteByte (byte value)
		{
		}

		[AOTAttr.ADCStub]
		public unsafe static void WriteHex (byte value)
		{
		}

		[AOTAttr.ADCStub]
		public static void SetAttributes (TextColor _foreground, TextColor _background)
		{
		}

		[AOTAttr.ADCStub]
		public static void RestoreAttributes ()
		{
		}
	}
}

