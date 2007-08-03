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
using ADC = SharpOS.ADC;

namespace SharpOS.ADC.X86 
{
	
	public unsafe class TextMode
	{
		//FIXME: get row size from video card
	
		static int			x					= 0;
		static int			y					= 0;
		static TextColor	foreground			= TextColor.White;
		static TextColor	background			= TextColor.Black;
		static byte			attributes			= 0;
		static byte			oldAttributes		= 0;

		static IO.Ports		CRT_index_register;
		static IO.Ports		CRT_data_register;
		static int			width				= 80;
		static int			scanline			= 80;
		static int			height				= 25;
		static uint			address				= 0xB0000;
		static bool			colorMode			= false;

		internal static void Setup()
		{
			// Find CRT controller addresses
			bool position = (IO.In8 (IO.Ports.EGA_graphics_1_position_register) & 1) == 1;
			if (position)
			{
				CRT_index_register = IO.Ports.CGA_CRT_index_register;
				CRT_data_register = IO.Ports.CGA_CRT_data_register;
				address = 0xB8000;
				scanline = 160;
				colorMode = true;
			} else
			{
				CRT_index_register = IO.Ports.MDA_CRT_index_register;
				CRT_data_register = IO.Ports.MDA_CRT_data_register;
				address = 0xB0000;
				scanline = 80;
				colorMode = false;
			}

			IO.Out8(CRT_index_register, (byte)CRT_Indices.horizontal_displayed);
			width = (IO.In8(CRT_data_register) + 1);

			// this returns a funny number... what am i doing wrong here?
			//IO.Out8(CRT_index_register, (byte)CRT_Indices.vertical_displayed);
			//height = (IO.In8(CRT_data_register) + 1);
			ADC.TextMode.WriteNumber (IO.In8(CRT_data_register) + 1);
			
			SetAttributes (TextColor.Yellow, TextColor.Black);
			ClearScreen ();
			
			TextMode.GoTo (20, height - 1);
			ADC.TextMode.WriteNumber (width);
			TextMode.GoTo (25, height - 1);
			ADC.TextMode.WriteNumber (height);
			TextMode.GoTo(0, 0);
		}

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

		public unsafe static void SetCursorSize(byte _start, byte _end)
		{
			IO.Out8(CRT_index_register, (byte)CRT_Indices.cursor_start);
			IO.Out8(CRT_data_register, (byte)_start);

			IO.Out8(CRT_index_register, (byte)CRT_Indices.cursor_end);
			IO.Out8(CRT_data_register, (byte)_end);
		}

		public unsafe static void SetCursor(int _x, int _y)
		{
			if (_x < 0)
				_x = 0;
			if (_x >= width)
				_x = width - 1;
			if (_y < 0)
				_y = 0;
			if (_y >= height)
				_y = height - 1;

			ushort position = (ushort)(_x + (_y * width));

			// cursor LOW port to vga INDEX register
			IO.Out8(CRT_index_register, (byte)CRT_Indices.cursor_location_low);
			IO.Out8(CRT_data_register, (byte)(position & 0xFF));
		   
			// cursor HIGH port to vga INDEX register
			IO.Out8(CRT_index_register, (byte)CRT_Indices.cursor_location_high);
			IO.Out8(CRT_data_register, (byte)((position >> 8) & 0xFF));
		}

		public unsafe static int GetX()
		{
			return x;
		}

		public unsafe static int GetY()
		{
			return y;
		}

		public unsafe static void GoTo (int _x, int _y)
		{
			x = _x; if (x < 0) x = 0; else if (x >= width) x = width - 1;
			y = _y; if (y < 0) y = 0; else if (y >= height) y = height - 1;
		}

		public unsafe static void ClearScreen ()
		{
			x = 0; y = 0;

			uint fill;
			uint count = (uint)(scanline * height);
			if (colorMode)
			{
				fill =
					((uint)0x20) |
					((uint)attributes << 8) |
					((uint)0x20 << 16) |
					((uint)attributes << 24);
			} else
			{
				fill =
					((uint)0x20) |
					((uint)0x20 << 8) |
					((uint)0x20 << 16) |
					((uint)0x20 << 24);
			}

			MemUtils.MemSet32(fill, address, count / 4);

			// ...the code below sometimes causes a crash now and then?
			/*
			for (byte* video = (byte*)address; (uint)video < address + width * height; )
			{
				*video++ = 0x20;
				*video++ = 0x00;
			}*/
		}

		public unsafe static void Write (byte* message)
		{
			for (int i = 0; message [i] != 0; i++)
				WriteChar (message [i]);
		}

		public unsafe static void WriteChar (byte value)
		{
			byte* video = (byte*)address;

			if (value != (byte) '\n') 
			{
				video += (uint)(y * scanline);
				if (colorMode)
				{
					video += x * 2;
					*video++ = value;
					*video = attributes;
				} else
				{
					video += x;
					*video = value;
				}
				x++;
			}

			if (x > (width - 1) || value == (byte)'\n')
			{
				x = 0;
				if (y != (height - 1))
				{
					if (y == (height - 2))
						ScrollPage(1);
					else
						y++;
				}

			}
		}

		public unsafe static void ScrollPage(int value)
		{
			if (value <= 0)
				// scrolldown not implemented
				return;

			int		lines	= height - (value + 1);
			int		count	= lines * scanline;
			int		count2	= value * scanline;

			uint	src		= address + (uint)count2;
			uint	dst		= address;

			MemUtils.MemCopy32(src, dst, (uint)(count / 4));

			dst = address + (uint)count;

			uint fill;
			if (colorMode)
			{
				fill =
					((uint)0x20) |
					((uint)attributes << 8) |
					((uint)0x20 << 16) |
					((uint)attributes << 24);
			} else
			{
				fill =
					((uint)0x20) |
					((uint)0x20 << 8) |
					((uint)0x20 << 16) |
					((uint)0x20 << 24);
			}
			MemUtils.MemSet32(fill, dst, (uint)(count2 / 4));
		}

		public unsafe static void WriteString (UInt32 value)
		{
			for (int i = 0; i < 4; i++) 
			{
				WriteChar ((byte) (value & 0xff));
				value >>= 8;
			}
		}

		public unsafe static void WriteByte (byte value)
		{
			WriteHex ((byte) (value >> 4));
			WriteHex ((byte) (value & 0x0F));
		}

		public unsafe static void WriteHex (byte value)
		{
			if (value <= 9)
				WriteChar ((byte) (48 + value));

			else if (value == 10)
				WriteChar ((byte) 'A');

			else if (value == 11)
				WriteChar ((byte) 'B');

			else if (value == 12)
				WriteChar ((byte) 'C');

			else if (value == 13)
				WriteChar ((byte) 'D');

			else if (value == 14)
				WriteChar ((byte) 'E');

			else if (value == 15)
				WriteChar ((byte) 'F');

			else
				WriteChar ((byte) 'X');
		}

		public static void SetAttributes (TextColor _foreground, TextColor _background)
		{
			foreground = _foreground;
			background = _background;

			oldAttributes = attributes;
			attributes = (byte)((byte)_foreground | ((byte)_background << 4));
		}

		public static void RestoreAttributes ()
		{
			attributes = oldAttributes;
		}
	}
}

