//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

//#define DISABLE_SetAttributes

using System;
using System.Runtime.InteropServices;
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using SharpOS.Kernel.ADC.X86;
using ADC = SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.ADC.X86 {
	/// <summary>
	/// Handles setting up and working with CGA/EGA/MDA text modes.
	/// </summary>
	/// <todo>
	/// * FIXME: get row size from video card
	/// * Cleanup naming & usage of functions
	/// </todo>
	public unsafe class TextMode {
		#region Global state

		// Hardware
		static IO.Port CRT_index_register;
		static IO.Port CRT_data_register;
		static MemoryBlock videoMemory;
		static int bytePerChar = 1;
		static bool colorMode = false;
		static bool haveBuffer = false;
		static int height = 25;
		static int width = 80;
		static int bufferHeight = 25;
		static uint bufferSize = 0;
		static uint scanline = 80;

		static int x = 0;
		static int y = 0;
		static int readY = 0;
		static int writeY = 0;
		static TextColor foreground = TextColor.White;
		static TextColor background = TextColor.Black;

		static uint fill = 0;
		static byte attributes = (byte) ((byte) foreground | ((byte) background << 4));
		static byte* savedAttributes = Stubs.StaticAlloc (EntryModule.MaxTextAttributeSlots);

		#endregion
		#region Properties

		public static TextColor Foreground
		{
			get
			{
				return foreground;
			}
			set
			{
				SetAttributes (value, background);
			}
		}

		public static TextColor Background
		{
			get
			{
				return background;
			}
			set
			{
				SetAttributes (foreground, value);
			}
		}

		public static int X
		{
			get
			{
				return x;
			}
			set
			{
				x = value;
			}
		}

		public static int Y
		{
			get
			{
				return y;
			}
			set
			{
				y = value;
			}
		}

		#endregion
		#region Nested types

		private enum CRT_Indices : byte {
			horizontal_total = 0x00,
			horizontal_displayed = 0x01,
			horizontal_sync_position = 0x02,
			horizontal_sync_pulse_width = 0x03,

			vertical_total = 0x04,
			vertical_displayed = 0x05,
			vertical_sync_position = 0x06,
			vertical_sunc_pulse_width = 0x07,

			interlace_mode = 0x08,

			maximum_scan_lines = 0x09,

			cursor_start = 0x0A,
			cursor_end = 0x0B,

			start_address = 0x0C,
			start_address_high = 0x0C,
			start_address_low = 0x0D,

			cursor_location = 0x0E,
			cursor_location_high = 0x0E,
			cursor_location_low = 0x0F,

			light_pen = 0x10,
			light_pen_high = 0x10,
			light_pen_low = 0x11,
		}

		#endregion
		#region HardwareCommand

		private static int HardwareCommand (CRT_Indices index)
		{
			IO.WriteByte (CRT_index_register, (byte) index);
			return IO.ReadByte (CRT_data_register);
		}

		private static void HardwareCommand (CRT_Indices index, byte value)
		{
			IO.WriteByte (CRT_index_register, (byte) index);
			IO.WriteByte (CRT_data_register, (byte) value);
		}

		private static void HardwareCommand (CRT_Indices index, ushort value)
		{
			// high
			HardwareCommand (index, (byte) ((value >> 8) & 0xff));
			// low
			HardwareCommand ((CRT_Indices) (index + 1), (byte) (value & 0xff));
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
			// Find CRT controller addresses
			if ((IO.ReadByte (IO.Port.EGA_graphics_1_position_register) & 1) == 1) {
				// CGA/EGA color text mode

				CRT_index_register = IO.Port.CGA_CRT_index_register;
				CRT_data_register = IO.Port.CGA_CRT_data_register;
				
				// HACK!!
				videoMemory.address = (byte*)0xB8000;
				videoMemory.length = 0x4000;	// CGA has 16kb video memory
				//videoMemory = Architecture.ResourceManager.RequestMemoryMap(0xB8000, 0x4000);

				// ideally we'd poll how much video memory
				// we can use, but we can't so we're
				// being conservative and only use 16kb.
				bytePerChar = 2;
				colorMode = true;
				haveBuffer = true;
			} else {
				// MDA monochrome text mode

				CRT_index_register = IO.Port.MDA_CRT_index_register;
				CRT_data_register = IO.Port.MDA_CRT_data_register;
				
				// HACK!!
				videoMemory.address = (byte*)0xB0000;
				videoMemory.length = 0x1000;	// MDA has 16kb video memory
				//videoMemory = Architecture.ResourceManager.RequestMemoryMap(0xB0000, 0x1000);

				bytePerChar = 1;
				colorMode = false;
				haveBuffer = false;
			}

			// read the width
			width = HardwareCommand (CRT_Indices.horizontal_displayed) + 1;

			// this returns a funny number... what am i doing wrong here?
			//height = HardwareCommand(CRT_Indices.vertical_displayed) + 1;
			height = 25;

			scanline = (uint) (width * bytePerChar);

			if (haveBuffer)
				bufferHeight = (int) (
					videoMemory.Length
					/ scanline) - 1;
			else
				bufferHeight = height;

			bufferSize = (uint) bufferHeight * scanline;
			readY = 0;
			writeY = 0;

			for (int x = 0; x < EntryModule.MaxTextAttributeSlots; x++)
				savedAttributes [x] = 0xFF;
		}

		public static void SetCursorSize (byte _start, byte _end)
		{
			HardwareCommand (CRT_Indices.cursor_start, _start);
			HardwareCommand (CRT_Indices.cursor_end, _end);
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
			else if (y >= bufferHeight)
				y = (bufferHeight - 1);
		}

		public static void SetCursor (int _x, int _y)
		{
			MoveTo (_x, _y);

			ushort position = (ushort) (x + ((y + writeY) * width));
			HardwareCommand (CRT_Indices.cursor_location, position);
		}

		public static void GetCursor (out int ret_x, out int ret_y)
		{
			ret_x = x;
			ret_y = y;
		}

		public static void GetScreenSize (out int ret_w, out int ret_h)
		{
			ret_w = width;
			ret_h = height;
		}

		public static int GetBufferHeight ()
		{
			return bufferHeight;
		}

		public static int GetReadPosition ()
		{
			return readY;
		}

		public static void SetReadPos (int position)
		{
			if (!haveBuffer)
				return;

			if (position < 0)
				position = 0;

			if (position > (bufferHeight - height))
				position = (bufferHeight - height);

			readY = position;

			HardwareCommand (CRT_Indices.start_address, (ushort) (position * width));
		}

		public static int GetWritePosition ()
		{
			return writeY;
		}

		public static void SetWritePos (int position)
		{
			if (!haveBuffer)
				return;

			if (position < 0)
				position = 0;

			if (position > (bufferHeight - height))
				position = (bufferHeight - height);

			writeY = position;
			SetCursor (x, y);
		}

		public static void ClearScreen ()
		{
			x = 0;
			y = 0;
			videoMemory.Fill(fill);
			SetReadPos (0);
			writeY = 0;
			SetCursor (x, y);
		}

		/// <summary>
		/// Clear to end of line
		/// </summary>
		public unsafe static void ClearToEndOfLine ()
		{
			uint count	= (uint) (bytePerChar * (width - x));
			uint offset = (uint) (x + ((y + writeY) * scanline));
			videoMemory.Fill(fill, offset, count);
		}

		public static void MovePage (int value)
		{
			uint move_count, move_src, move_dst;
			uint fill_count, fill_dst;

			if (value > 0) {
				fill_count = (uint) (value * scanline);
				move_count = bufferSize - fill_count;
				move_src = fill_count;
				move_dst = 0;
				fill_dst = move_count;
			} else {
				fill_count = (uint) ((-value) * scanline);
				move_count = bufferSize - fill_count;
				move_src = 0;
				move_dst = fill_count;
				fill_dst = 0;
			}

      videoMemory.Move((uint)videoMemory.address + move_src, (uint)videoMemory.address + move_dst, move_count);
      videoMemory.Fill(fill, fill_dst, fill_count);
		}

		public static void ScrollPageWrite (int value)
		{
			int newPos = writeY + value;
			value = 0;
			if (newPos < 0) {
				value = -newPos;
				newPos = 0;
			} else
				if (newPos > (bufferHeight - height)) {
					value = newPos - (bufferHeight - height);
					newPos = (bufferHeight - height);
				}
			writeY = newPos;
			if (newPos != readY)
				SetReadPos (newPos);
			if (value != 0)
				MovePage (value);
			SetCursor (x, y);
		}

    /// <summary>
		/// Scrolls the text on the screen
		/// </summary>
		/// <param name="value">Positive value is down, negative is up</param>
		public static void ScrollPage (int value)
		{
			int newPos = readY + value;
			//value = 0;
			if (newPos < 0) {
				newPos = 0;
			} else
				if (newPos > writeY) {
					newPos = writeY;
				}
			if (newPos != readY)
				SetReadPos (newPos);
		}

		public static void WriteChar (byte value)
		{
			int index = 0;

			if (value != (byte) '\n') {
				index += (int) ((y + writeY) * scanline);

				if (colorMode) {
					index += x * 2;
					videoMemory[index  ] = value;
					videoMemory[index+1] = attributes;
				} else {
					index += x;
					videoMemory[index  ] = value;
				}

				x++;
			}

			if (x > (width - 1) || value == (byte) '\n') {
				x = 0;
				if (y != (height - 1)) {
					if (y == (height - 2))
						ScrollPageWrite (1);
					else
						y++;
				}

			}
		}

		#region Writing Hex values
		private const string hexValues = "0123456789ABCDEF";
		public static void WriteByte (byte value)
		{
			WriteChar ((byte) hexValues [((value >> 4) & 0x0F)]);
			WriteChar ((byte) hexValues [((value) & 0x0F)]);
		}

		public static void WriteHex (byte value)
		{
			if (value > 15)
				WriteChar ((byte) 'X');
			else
				WriteChar ((byte) hexValues [value]);
		}
		#endregion

		#region Attributes
		public static void SetAttributes (TextColor _foreground, TextColor _background)
		{
#if !DISABLE_SetAttributes
			foreground = _foreground;
			background = _background;

			attributes = (byte) ((byte) _foreground | ((byte) _background << 4));

			if (colorMode) {
				uint attr = attributes;	//FIXME: AOT bug, can't use attribute directly
				fill =
					((uint) 0x20) |
					(attr << 8) |
					((uint) 0x20 << 16) |
					(attr << 24);
			} else {
				fill =
					((uint) 0x20) |
					((uint) 0x20 << 8) |
					((uint) 0x20 << 16) |
					((uint) 0x20 << 24);
			}
#endif
		}

		public static bool SaveAttributes ()
		{
#if !DISABLE_SetAttributes
			for (int x = 0; x < EntryModule.MaxTextAttributeSlots; ++x) {
				if (savedAttributes [x] == 0xFF) {
					savedAttributes [x] = attributes;
					return true;
				}
			}
#endif
			return false;
		}

		public static bool RestoreAttributes ()
		{
#if !DISABLE_SetAttributes
			for (int x = EntryModule.MaxTextAttributeSlots - 1; x >= 0; --x) {
				if (savedAttributes [x] != 0xFF) {
					byte attr = savedAttributes [x];
					savedAttributes [x] = 0xFF;
					SetAttributes ((TextColor) (attr & 0x0F), (TextColor) ((attr >> 4) & 0x0F));
					return true;
				}
			}
#endif
			return false;
		}
		#endregion

		#endregion
	}
}
