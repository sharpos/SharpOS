// 
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Runtime.InteropServices;
using SharpOS;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS {
	public unsafe class Screen {
		public enum ColorTypes : byte {
			Black,
			Blue,
			Green,
			Cyan,
			Red,
			Magenta,
			Brown,
			White,
			DarkGray,
			LightBlue,
			LightGreen,
			LightCyan,
			LightRed,
			LightMagenta,
			Yellow,
			BrightWhite
		}

		static int x = 0;
		static int y = 0;
		static ColorTypes foreground = ColorTypes.Yellow;
		static ColorTypes background = ColorTypes.Black;
		static byte attributes = 0;
		static byte oldAttributes = 0;

		public unsafe static void GoTo (int _x, int _y)
		{
			x = _x;
			y = _y;
		}

		public unsafe static void CLRSCR ()
		{
			GoTo (0, 0);

			for (byte* video = (byte*) 0xB8000; (uint) video < 0xB8000 + 40 * 2 * 25;) {
				*video++ = 0x20;
				*video++ = 0x00;
			}
		}

		public unsafe static void WriteNumber (bool hex, int value)
		{
			byte* buffer = stackalloc byte [32];
			uint uvalue = (uint) value;
			ushort divisor = hex ? (ushort) 16 : (ushort) 10;
			int length = 0;

			if (!hex && value < 0) {
				buffer [length++] = (byte) '-';

				uvalue = (uint) -value;
			}

			do {
				uint remainder = uvalue % divisor;

				if (remainder < 10)
					buffer [length++] = (byte) ('0' + remainder);

				else
					buffer [length++] = (byte) ('A' + remainder - 10);

			} while ((uvalue /= divisor) != 0);

			while (length > 0)
				WriteChar ((char) buffer [--length]);
		}

		public unsafe static void WriteLine (string message)
		{
			WriteMessage (message);
			WriteNL ();
		}

		public unsafe static void WriteLine (string message, int value)
		{
			WriteMessage (message);
			WriteNumber (true, value);
			WriteNL ();
		}

		public unsafe static void WriteMessage (string message)
		{
			foreach (char value in message)
				WriteChar (value);
		}

		public unsafe static void WriteChar (char value)
		{
			byte* video = (byte*) 0xB8000;

			if (value == (byte) '\n') {
				x = 0;
				y++;

			} else {
				video += y * 160 + x * 2;

				*video++ = (byte) value;
				*video = attributes;

				x++;
			}
		}

		public unsafe static void WriteString (UInt32 value)
		{
			for (int i = 0; i < 4; i++) {
				WriteChar ((char) (value & 0xff));
				value >>= 8;
			}
		}
		public unsafe static void WriteNL ()
		{
			WriteChar ('\n');
		}

		public unsafe static void WriteByte (byte value)
		{
			WriteHex ((byte) (value >> 4));
			WriteHex ((byte) (value & 0x0F));
		}

		public unsafe static void WriteHex (byte value)
		{
			if (value <= 9)
				WriteChar ((char) (48 + value));

			else if (value == 10)
				WriteChar ('A');

			else if (value == 11)
				WriteChar ('B');

			else if (value == 12)
				WriteChar ('C');

			else if (value == 13)
				WriteChar ('D');

			else if (value == 14)
				WriteChar ('E');

			else if (value == 15)
				WriteChar ('F');

			else
				WriteChar ('X');
		}

		public static void SetAttributes (ColorTypes _foreground, ColorTypes _background)
		{
			foreground = _foreground;
			background = _background;

			oldAttributes = attributes;
			attributes = (byte) _foreground;
			attributes += (byte) ((byte) _background << 4);
		}

		public static void RestoreAttributes ()
		{
			attributes = oldAttributes;
		}
	}
}

