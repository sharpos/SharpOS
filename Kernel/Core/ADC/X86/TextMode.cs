// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.Runtime.InteropServices;
using SharpOS;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using SharpOS.ADC.X86;
using ADC = SharpOS.ADC;

namespace SharpOS.ADC.X86 
{
	/// <summary>
	/// Handles setting up and working with CGA/EGA/MDA text modes.
	/// </summary>
	/// <todo>
	/// * FIXME: get row size from video card
	/// </todo>
	public unsafe class TextMode
	{
		#region Global state
		
		static int			x = 0;
		static int			y = 0;
		static TextColor	foreground		= TextColor.White;
		static TextColor	background		= TextColor.Black;
		static byte			attributes		= (byte)((byte)foreground | ((byte)background << 4));
		static byte *		savedAttributes = Kernel.StaticAlloc (Kernel.MaxTextAttributeSlots);

		static IO.Port		CRT_index_register;
		static IO.Port		CRT_data_register;

		static int			width		= 80;
		static uint			scanline	= 80;
		static int			bytePerChar = 1;
		static int			height		= 25;
		static uint			address		= 0xB0000;
		static uint			screenSize	= 80 * 25;
		static uint			maxLines	= Kernel.MaxTextBufferLines;
		static bool			colorMode	= false;

		#endregion
		#region Nested types
		
		private enum CRT_Indices : byte
		{
			horizontal_total			= 0x00,
			horizontal_displayed		= 0x01,
			horizontal_sync_position	= 0x02,
			horizontal_sync_pulse_width	= 0x03,
			
			vertical_total				= 0x04,
			vertical_displayed			= 0x05,
			vertical_sync_position		= 0x06,
			vertical_sunc_pulse_width	= 0x07,
			
			interlace_mode				= 0x08,
			
			maximum_scan_lines			= 0x09,
			
			cursor_start				= 0x0A,
			cursor_end					= 0x0B,
			
			start_address_high			= 0x0C,
			start_address_low			= 0x0D,
			
			cursor_location_high		= 0x0E,
			cursor_location_low			= 0x0F,

			light_pen_high				= 0x10,
			light_pen_low				= 0x11,
		}

		#endregion
		#region ADC implementation
		
		/// <summary>
		/// ADC portion of the TextMode class setup. For X86, this function
		/// first locates the addresses of CRT controllers using color (EGA/CGA)
		/// or alternatively monochrome (MDA) display standards. Finally it
		/// determines the width and height of the text buffer.
		/// </summary>
		/// <reference>http://www.cknow.com/refs/VideoDisplayStandards.html</reference>
		public static void Setup ()
		{
			for (int x = 0; x < Kernel.MaxTextAttributeSlots; x++)
				savedAttributes[x] = 0xFF;

			// Find CRT controller addresses			
			if ((IO.In8 (IO.Port.EGA_graphics_1_position_register) & 1) == 1) {
				// CGA/EGA color text mode
				
				CRT_index_register = IO.Port.CGA_CRT_index_register;
				CRT_data_register = IO.Port.CGA_CRT_data_register;
				address = 0xB8000;
				bytePerChar = 2;
				colorMode = true;
			} else {
				// MDA monochrome text mode
				
				CRT_index_register = IO.Port.MDA_CRT_index_register;
				CRT_data_register = IO.Port.MDA_CRT_data_register;
				address = 0xB0000;
				bytePerChar = 1;
				colorMode = false;
			}
			maxLines = (uint)(Kernel.MaxTextBufferLines / bytePerChar);

			// read the width
			
			IO.Out8(CRT_index_register, (byte)CRT_Indices.horizontal_displayed);
			width = (IO.In8(CRT_data_register) + 1);

			// this returns a funny number... what am i doing wrong here?
			/*
			IO.Out8(CRT_index_register, (byte)CRT_Indices.vertical_displayed);
			height = (IO.In8(CRT_data_register) + 1);

			ADC.TextMode.Write("height: ");
			ADC.TextMode.Write(height);
			ADC.TextMode.WriteLine();*/
			height = 25;

			scanline = (uint)(width * bytePerChar);
			screenSize = (uint)(scanline * height);
		}

		public static TextColor Foreground {
			get { return foreground; }
			set { foreground = value; }
		}
		
		public static TextColor Background {
			get { return background; }
			set { background = value; }
		}
		
		public static void SetCursorSize (byte _start, byte _end)
		{
			IO.Out8(CRT_index_register, (byte)CRT_Indices.cursor_start);
			IO.Out8(CRT_data_register, (byte)_start);

			IO.Out8(CRT_index_register, (byte)CRT_Indices.cursor_end);
			IO.Out8(CRT_data_register, (byte)_end);
		}

		public static void MoveTo (int _x, int _y)
		{
			x = _x; 
			y = _y;

			if (x < 0)
				x = 0;
			else if (x >= width)
				x = width - 1;
			
			if (y < 0)
				y = 0;
			else if (y >= height)
				y = height - 1;
		}

		public static void SetCursor (int _x, int _y)
		{
			MoveTo (_x, _y);
			
			ushort position = (ushort)(_x + (_y * width));

			// cursor LOW port to vga INDEX register
			IO.Out8(CRT_index_register, (byte)CRT_Indices.cursor_location_low);
			IO.Out8(CRT_data_register, (byte)(position & 0xFF));
		   
			// cursor HIGH port to vga INDEX register
			IO.Out8(CRT_index_register, (byte)CRT_Indices.cursor_location_high);
			IO.Out8(CRT_data_register, (byte)((position >> 8) & 0xFF));
		}

		public static void GetCursor (int *ret_x, int *ret_y)
		{
			*ret_x = x;
			*ret_y = y;
		}
		
		public static void GetScreenSize (int *ret_w, int *ret_h)
		{
			*ret_w = width;
			*ret_h = height;
		}
		
		public static void ClearScreen ()
		{
			x = 0; y = 0;

			uint fill;

			if (colorMode) {
				uint attr = attributes;
				fill =
					((uint)0x20) |
					(attr << 8) |
					((uint)0x20 << 16) |
					(attr << 24);
			} else {
				fill =
					((uint)0x20) |
					((uint)0x20 << 8) |
					((uint)0x20 << 16) |
					((uint)0x20 << 24);
			}

			Memory.MemSet(fill, address, screenSize);
		}

		/// <summary>
		/// Clear to end of line
		/// </summary>
		public unsafe static void ClearToEndOfLine()
		{
			uint fill;
			uint count = (uint)(bytePerChar * (width - x));

			if (colorMode)
			{
				uint attr = attributes;
				fill =
					((uint)0x20) |
					(attr << 8) |
					((uint)0x20 << 16) |
					(attr << 24);
			}
			else
			{
				fill =
					((uint)0x20) |
					((uint)0x20 << 8) |
					((uint)0x20 << 16) |
					((uint)0x20 << 24);
			}

			byte* video = (byte*)address;
			video += (uint)(x + (y * scanline));
			Memory.MemSet(fill, (uint)video, count);
		}

		public static void WriteChar (byte value)
		{
			byte* video = (byte*)address;

			if (value != (byte) '\n') {
				video += (uint)(y * scanline);
				
				if (colorMode) {
					video += x * 2;
					*video++ = value;
					*video = attributes;
				} else {
					video += x;
					*video = value;
				}
				
				x++;
			}

			if (x > (width - 1) || value == (byte)'\n') {
				x = 0;
				if (y != (height - 1)) {
					if (y == (height - 2))
						ScrollPage (1);
					else
						y++;
				}

			}
		}

		public static void ScrollPage (int value)
		{
			if (value <= 0)
				return;		// scrolldown not implemented

			uint lines, count, count2;
			uint src, dst, fill;
			
			lines = (uint)(height - (value + 1));
			count = lines * scanline;
			count2 = (uint)(value * scanline);

			src = address + (uint)count2;
			dst = address;

			Memory.MemCopy32(src, dst, (uint)(count / 4));

			dst = address + (uint)count;

			if (colorMode) {
				uint attr = attributes;
				fill =
					((uint)0x20) |
					(attr << 8) |
					((uint)0x20 << 16) |
					(attr << 24);
			} else {
				fill =
					((uint)0x20) |
					((uint)0x20 << 8) |
					((uint)0x20 << 16) |
					((uint)0x20 << 24);
			}

			Memory.MemSet32(fill, dst, (uint)(count2 / 4));
		}

		private const string hexValues = "0123456789ABCDEF";
		public static void WriteByte (byte value)
		{
			WriteChar((byte)hexValues[((value >> 4) & 0x0F)]);
			WriteChar((byte)hexValues[((value     ) & 0x0F)]);
		}

		public static void WriteHex (byte value)
		{
			if (value > 15) WriteChar((byte)'X');
			else			WriteChar((byte)hexValues[value]);
		}

		public static void SetAttributes (TextColor _foreground, TextColor _background)
		{
			foreground = _foreground;
			background = _background;

			attributes = (byte)((byte)_foreground | ((byte)_background << 4));
		}

		public static bool SaveAttributes ()
		{
			for (int x = 0; x < Kernel.MaxTextAttributeSlots; ++x) {
				if (savedAttributes [x] == 0xFF) {
					savedAttributes [x] = attributes;
					return true;
				}
			}
			return false;
		}
		
		public static bool RestoreAttributes ()
		{
			for (int x = Kernel.MaxTextAttributeSlots - 1; x >= 0; --x) {
				if (savedAttributes [x] != 0xFF) {
					attributes = savedAttributes [x];
					savedAttributes [x] = 0xFF;		
					return true;
				}
			}
			return false;
		}

		#endregion
	}
}
