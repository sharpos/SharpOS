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
// (**) Please see the ADC conventions and documentation at Kernel/Core/Documents/ADC.txt
//

using System.Runtime.InteropServices;
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using SharpOS.Kernel.ADC.X86;
using SharpOS.Kernel.Foundation;

namespace SharpOS.Kernel.ADC {
	public class TextMode {
		#region ADC Interface

		/// <summary>
		/// Performs architecture-specific setup.
		/// </summary>
		[SharpOS.AOT.Attributes.ADCStub]
		public unsafe static void Setup ()
		{
		}

		/// <summary>
		/// Writes the ASCII character <paramref name="value" /> to the screen.
		/// </summary>
		[SharpOS.AOT.Attributes.ADCStub]
		public unsafe static void WriteChar (byte value)
		{
		}

		/// <summary>
		/// Move the internal cursor to the specified position,
		/// but do not update the cursor position displayed on
		/// the screen. This saves time and reduces flickering
		/// of the display while doing complex screen changes.
		/// </summary>
		[SharpOS.AOT.Attributes.ADCStub]
		public unsafe static void MoveTo (int _x, int _y)
		{
		}

		/// <summary>
		/// Change the hardware cursor size.
		/// </summary>
		[SharpOS.AOT.Attributes.ADCStub]
		public unsafe static void SetCursorSize (byte _start, byte _end)
		{
		}

		/// <summary>
		/// Change the internal cursor position (like MoveTo()), then
		/// update the cursor displayed on the screen.
		/// </summary>
		[SharpOS.AOT.Attributes.ADCStub]
		public unsafe static void SetCursor (int _x, int _y)
		{
		}

		/// <summary>
		/// Retrieve the position of the cursor and the size of the screen simultaneously.
		/// </summary>
		[SharpOS.AOT.Attributes.ADCStub]
		public static void GetCursor (out int ret_x, out int ret_y)
		{
			ret_x = ret_y = 0;
		}

		[SharpOS.AOT.Attributes.ADCStub]
		public static int GetReadPosition ()
		{
			return 0;
		}

		[SharpOS.AOT.Attributes.ADCStub]
		public static int GetWritePosition ()
		{
			return 0;
		}

		[SharpOS.AOT.Attributes.ADCStub]
		public static void SetReadPos (int position)
		{
		}

		[SharpOS.AOT.Attributes.ADCStub]
		public static int GetBufferHeight ()
		{
			return 0;
		}

		[SharpOS.AOT.Attributes.ADCStub]
		public unsafe static void GetScreenSize (out int ret_w, out int ret_h)
		{
			ret_w = ret_h = 0;
		}

		/// <summary>
		/// Clear the screen.
		/// </summary>
		[SharpOS.AOT.Attributes.ADCStub]
		public unsafe static void ClearScreen ()
		{
		}

		/// <summary>
		/// Clear to end of line
		/// </summary>
		[SharpOS.AOT.Attributes.ADCStub]
		public unsafe static void ClearToEndOfLine ()
		{
		}

		/// <summary>
		/// Scroll the screen by <paramref name="value" /> lines.
		/// </summary>
		[SharpOS.AOT.Attributes.ADCStub]
		public unsafe static void ScrollPage (int value)
		{
		}

		/// <summary>
		/// Write <paramref name="value" /> as a decimal value.
		/// </summary>
		[SharpOS.AOT.Attributes.ADCStub]
		public unsafe static void WriteByte (byte value)
		{
		}

		/// <summary>
		/// Write <paramref name="value" /> as a hexadecimal value
		/// </summary>
		[SharpOS.AOT.Attributes.ADCStub]
		public unsafe static void WriteHex (byte value)
		{
		}

		/// <summary>
		/// Change the attributes used for subsequent Write() calls.
		/// </summary>
		[SharpOS.AOT.Attributes.ADCStub]
		public static void SetAttributes (TextColor _foreground, TextColor _background)
		{
		}

		/// <summary>
		/// Restores the last set of screen attributes
		/// </summary>
		[SharpOS.AOT.Attributes.ADCStub]
		public static bool RestoreAttributes ()
		{
			return false;
		}

		/// <summary>
		/// Saves the current set of screen attributes
		/// </summary>
		[SharpOS.AOT.Attributes.ADCStub]
		public static bool SaveAttributes ()
		{
			return false;
		}

		public static TextColor Foreground
		{

			[SharpOS.AOT.Attributes.ADCStub]
			get
			{
				return TextColor.Black;
			}
			[SharpOS.AOT.Attributes.ADCStub]
			set
			{
			}
		}

		public static TextColor Background
		{
			[SharpOS.AOT.Attributes.ADCStub]
			get
			{
				return TextColor.Black;
			}
			[SharpOS.AOT.Attributes.ADCStub]
			set
			{
			}
		}

		public static int CursorTop {
			get {
				int x, y;

				GetCursor (out x, out y);
				return y;
			} set {
				int x, y;

				GetCursor (out x, out y);
				y = value;
				SetCursor (x, y);
			}
		}

		public static int CursorLeft {
			get {
				int x, y;

				GetCursor (out x, out y);
				return x;
			} set {
				int x, y;

				GetCursor (out x, out y);
				x = value;
				SetCursor (x, y);
			}
		}

		#endregion
		#region Internal

		/// <summary>
		/// Common Write() implementation. Serves Write(CString8*), Write(PString8*), and
		/// Write(byte*).
		/// </summary>
		public unsafe static void Write (byte* str, int len)
		{
			for (int i = 0; i < len; i++)
				WriteChar (str [i]);
		}

		public unsafe static void Write (byte* str, int strLen, int offset, int len)
		{
			Diagnostics.Assert (len > strLen, "TextMode.Write(): len > strLen");

			for (int i = offset; i < strLen && (i - offset < len); ++i)
				WriteChar (str [i]);
		}

		#endregion
		#region Cursors

		/// <summary>
		/// Makes sure the cursor displayed on the screen is
		/// in sync with the internal cursor (used to position
		/// new Write() data).
		/// </summary>
		public static void RefreshCursor ()
		{
			int x, y;

			GetCursor (out x, out y);
			SetCursor (x, y);
		}

		#endregion
		#region Write() family

		/// <summary>
		/// Writes a 16-bit string to the screen.
		/// </summary>
		public static void Write (string message)
		{
			for (int i = 0; i < message.Length; i++)
				WriteChar ((byte) message [i]);
		}
		public static void Write (string message, int value)
		{
			Write (message);
			Write (value);
		}

		public unsafe static void Write (CString8* str)
		{
			Write (str->Pointer, str->Length);
		}

		public unsafe static void Write (PString8* str)
		{
			Write (str->Pointer, str->Length);
		}

		public unsafe static void Write (byte* str)
		{
			Write (str, ByteString.Length (str));
		}

		/// <summary>
		/// Writes an Int32 to the screen in decimal format.
		/// </summary>
		public static void Write (int value)
		{
			Write (value, false);
		}

		/// <summary>
		/// Writes an Int32 to the screen, either in decimal or
		/// hexadecimal format.
		/// </summary>
		public unsafe static void Write (int value, bool hex)
		{
			byte* buffer = stackalloc byte [32];
			int length;

			length = Convert.ToString (value, hex, buffer, 32, 0);

			for (int x = 0; x < length; ++x)
				WriteChar (buffer [x]);
		}

		/// <summary>
		/// Writes an Int32 to the screen, either in decimal or
		/// hexadecimal format.
		/// </summary>
		public unsafe static void Write (int value, bool hex, int minSize)
		{
			byte* buffer = stackalloc byte [32];
			int length;

			length = Convert.ToString (value, hex, buffer, 32, 0);

			if (length < minSize)
				for (int x = 0; x < minSize - length; ++x)
					WriteChar ((byte)('0'));

			for (int x = 0; x < length; ++x)
				WriteChar (buffer [x]);
		}

		/// <summary>
		/// Writes an bool to the screen
		/// <param name="value">boolean value to write</param>
		/// </summary>
		public unsafe static void Write (bool value)
		{
			if (value) {
				Write ("True");
			} else {
				Write ("False");
			}
		}

		#endregion
		#region WriteLine() family

		/// <summary>
		/// Writes a newline to the screen.
		/// </summary>
		public static void WriteLine ()
		{
			WriteChar ((byte) '\n');
		}

		/// <summary>
		/// Writes a string to the screen, followed by a newline.
		/// </summary>
		public static void WriteLine (string message)
		{
			Write (message);
			WriteLine ();
		}

		/// <summary>
		/// Writes a CString8* to the screen, followed by a newline.
		/// </summary>
		public unsafe static void WriteLine (CString8* message)
		{
			Write (message);
			WriteLine ();
		}

		/// <summary>
		/// Writes a CString8* to the screen, followed by a newline.
		/// </summary>
		public unsafe static void WriteLine (PString8* message)
		{
			Write (message);
			WriteLine ();
		}

		/// <summary>
		/// Writes a CString8* to the screen, followed by a newline.
		/// </summary>
		public unsafe static void WriteLine (byte* message)
		{
			Write (message);
			WriteLine ();
		}

		/// <summary>
		/// Writes the string <paramref name="message" /> to the screen,
		/// then the Int32 <paramref name="value" />, followed by a newline.
		/// </summary>
		public static void WriteLine (string message, int value, bool hex)
		{
			Write (message);
			Write (value, hex);
			WriteLine ();
		}

		/// <summary>
		/// Writes the string <paramref name="message" /> to the screen,
		/// then the Int32 <paramref name="value" />, followed by a newline.
		/// </summary>
		public unsafe static void WriteLine (CString8* message, int value, bool hex)
		{
			Write (message);
			Write (value, hex);
			WriteLine ();
		}

		/// <summary>
		/// Writes the string <paramref name="message" /> to the screen,
		/// then the Int32 <paramref name="value" />, followed by a newline.
		/// </summary>
		public unsafe static void WriteLine (PString8* message, int value, bool hex)
		{
			Write (message);
			Write (value, hex);
			WriteLine ();
		}

		/// <summary>
		/// Writes the string <paramref name="message" /> to the screen,
		/// then the Int32 <paramref name="value" />, followed by a newline.
		/// </summary>
		public unsafe static void WriteLine (byte* message, int value, bool hex)
		{
			Write (message);
			Write (value, hex);
			WriteLine ();
		}

		public static void WriteLine (string message, int value)
		{
			Write (message);
			Write (value);
			WriteLine ();
		}

		public unsafe static void WriteLine (CString8* message, int value)
		{
			Write (message);
			Write (value);
			WriteLine ();
		}

		public unsafe static void WriteLine (PString8* message, int value)
		{
			Write (message);
			Write (value);
			WriteLine ();
		}

		public unsafe static void WriteLine (byte* message, int value)
		{
			Write (message);
			Write (value);
			WriteLine ();
		}

		#endregion
		#region WriteLine() convenient overloads

		public static void WriteLine (string message, int value, bool hex, string message2)
		{
			Write (message);
			Write (value, hex);
			Write (message2);
		}

		public static void WriteLine (string message, int value, string message2)
		{
			Write (message);
			Write (value);
			Write (message2);
		}

		#endregion
		#region WriteSubstring() family

		/// <summary>
		/// Writes <paramref name="len" /> characters of the string
		/// <paramref name="message" /> to the screen, starting with
		/// the character at index <paramref name="offset" />.
		/// </summary>
		public static void WriteSubstring (string message, int offset, int len)
		{
			for (int i = offset; i < message.Length && i < offset + len; ++i)
				WriteChar ((byte) message [i]);
		}

		/// <summary>
		/// Writes <paramref name="len" /> characters of the string
		/// <paramref name="message" /> to the screen, starting with
		/// the character at index <paramref name="offset" />.
		/// </summary>
		public unsafe static void WriteSubstring (CString8* message, int offset, int len)
		{
			for (int i = offset; message->GetChar (i) != 0 && i < offset + len; ++i)
				WriteChar (message->GetChar (i));
		}

		/// <summary>
		/// Writes <paramref name="len" /> characters of the string
		/// <paramref name="message" /> to the screen, starting with
		/// the character at index <paramref name="offset" />.
		/// </summary>
		public unsafe static void WriteSubstring (PString8* message, int offset, int len)
		{
			for (int i = offset; i < message->Length && i < offset + len; ++i)
				WriteChar (message->GetChar (i));
		}

		/// <summary>
		/// Writes <paramref name="len" /> characters of the string
		/// <paramref name="message" /> to the screen, starting with
		/// the character at index <paramref name="offset" />.
		/// </summary>
		public unsafe static void WriteSubstring (byte* message, int offset, int len)
		{
			for (int i = offset; message [i] != 0 && i < (offset + len); ++i)
				WriteChar (message [i]);
		}

		#endregion
		#region Obsolete
		// TODO: remove all references to these and axe them!

		/// <summary>
		/// Writes a number to the screen in decimal format.
		/// </summary>
		[System.Obsolete ("Use Write (int value)")]
		public static void WriteNumber (int value)
		{
			WriteNumber (value, false);
		}

		/// <summary>
		/// Writes a number to the screen, either in decimal or
		/// hexadecimal format.
		/// </summary>
		[System.Obsolete ("Use Write (int value, bool hex)")]
		public static void WriteNumber (int value, bool hex)
		{
			Write (value, hex);
		}

// TODO: remove me!
//
//		/// <summary>
//		/// Writes a number to the screen, either in decimal or
//		/// hexadecimal format.
//		/// </summary>
// 		[System.Obsolete ("Use WriteNumber (int value, bool hex) instead")]
// 		public static void WriteNumber (bool hex, int value)
// 		{
// 			WriteNumber (value, hex);
// 		}

		#endregion
	}
}

