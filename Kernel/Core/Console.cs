//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	William Lahti <xfurious@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

#define FORBID_ARROW_KEYS

using System;
using SharpOS.Kernel;
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.Shell;

namespace SharpOS.Kernel {
	/// <summary>
	/// Provides basic console services
	/// </summary>
	/// <todo>
	/// TODO
	/// - typing in command
	///		- move left/right in command
	///		- insert overwrite
	/// - add capslock/numlock support
	///		just like we're now passing a 'shifted' boolean value to translate,
	///		should we also pass a 'numlock' boolean value to translate?
	///		maybe this needs to be generalized somehow (eventually)?
	///		what if a keyboard manifacturer has programmable keys/modes etc.?
	///		capslock is also not exactly the same as shift, because shift+1 is not the same as capslock+1
	/// </todo>
	public unsafe class Console {
		public const string CONSOLE_KEY_UP_HANDLER = "CONSOLE_KEY_UP_HANDLER";
		public const string CONSOLE_KEY_DOWN_HANDLER = "CONSOLE_KEY_DOWN_HANDLER";
		public const string CONSOLE_TIMER_HANDLER = "CONSOLE_TIMER_HANDLER";

		private static bool initialized = false;
		private static bool overwrite = false;

		private static StringBuilder* textBuffer;

		public static void Setup ()
		{
			textBuffer = StringBuilder.CREATE ((uint) 80);

			Keyboard.RegisterKeyUpEvent (Stubs.GetFunctionPointer (CONSOLE_KEY_UP_HANDLER));
			Keyboard.RegisterKeyDownEvent (Stubs.GetFunctionPointer (CONSOLE_KEY_DOWN_HANDLER));
			SharpOS.Kernel.ADC.Timer.RegisterTimerEvent (Stubs.GetFunctionPointer (CONSOLE_TIMER_HANDLER));

			initialized = true;
			TextMode.RefreshCursor ();
			SetOverwrite (overwrite);

		}

		public static unsafe void SetOverwrite (bool _overwrite)
		{
			overwrite = _overwrite;

			if (overwrite)
				TextMode.SetCursorSize (0, 15);
			else
				TextMode.SetCursorSize (13, 15);
		}

		[SharpOS.AOT.Attributes.Label (CONSOLE_KEY_UP_HANDLER)]
		public static unsafe void KeyUp (uint scancode)
		{
			if (!initialized)
				return;
		}

		public static uint pos = 0;

		[SharpOS.AOT.Attributes.Label (CONSOLE_KEY_DOWN_HANDLER)]
		public static unsafe void KeyDown (uint scancode)
		{
			if (!initialized)
				return;

			// actually not correct because capslock does not behave like shift on all characters...

			bool shifted = (Keyboard.LeftShift () || Keyboard.RightShift ());

			TextMode.SetAttributes (TextColor.Yellow, TextColor.Black);


			ADC.Keys key = (ADC.Keys) scancode;
			// switch statement doesn't for all cases somehow..
			//switch (key)
			//{
			//case Keys.Insert:
			if (key == Keys.Insert) {
				overwrite = !overwrite;
				SetOverwrite (overwrite);
				return;
			}
				//case Keys.Delete:
			else if (key == Keys.Delete) {
				return;
			}
				//case Keys.PageUp:
			else if (key == Keys.PageUp) {
				TextMode.ScrollPage (-1);
				return;
			}
				//case Keys.PageDown:
			else if (key == Keys.PageDown) {
				TextMode.ScrollPage (+1);
				return;
			}
				//case Keys.Backspace:
			else if (key == Keys.Backspace) {
				if (Console.textBuffer != null
				    && textBuffer->Length <= 0) {
					//Beep??
					return;
				}

				int x, y, width, height;

				TextMode.GetScreenSize (out width, out height);
				TextMode.GetCursor (out x, out y);
				x--;
				if (x < 0) {
					x = width - 1;
					y--;
					if (y < 0) {
						y = 0;
						return;
					}
				}
				TextMode.MoveTo (x, y);
				TextMode.WriteChar ((byte) ' ');

				textBuffer->RemoveAt (textBuffer->Length - 1, 1);

				TextMode.MoveTo (x, y);
				TextMode.RefreshCursor ();
				return;
			}
				//case Keys.LeftArrow:
			else if (key == Keys.LeftArrow) {
#if !FORBID_ARROW_KEYS
				int x, y, width, height;

				TextMode.GetScreenSize(out width, out height);
				TextMode.GetCursor(out x, out y);
				x = x - 1; if (x < 0) x = 0;
				TextMode.MoveTo(x, y);
				TextMode.RefreshCursor();
#else
				//TODO: Beep?
#endif
				return;
			}
				//case Keys.RightArrow:
			else if (key == Keys.RightArrow) {
#if !FORBID_ARROW_KEYS
				int x, y, width, height;

				TextMode.GetScreenSize(out width, out height);
				TextMode.GetCursor(out x, out y);
				x = x + 1; if (x >= width) x = width - 1;
				TextMode.MoveTo(x, y);
				TextMode.RefreshCursor();
#else
				//TODO: Beep?
#endif
				return;
			}
				//case Keys.UpArrow:
			else if (key == Keys.UpArrow) {
#if !FORBID_ARROW_KEYS
				int x, y, width, height;

				TextMode.GetScreenSize(out width, out height);
				TextMode.GetCursor(out x, out y);
				y = y - 1; if (y < 0) y = 0;
				TextMode.MoveTo(x, y);
				TextMode.RefreshCursor();
#else
				//TODO: Beep?
#endif
				return;
			}
				//case Keys.DownArrow:
			else if (key == Keys.DownArrow) {
#if !FORBID_ARROW_KEYS
				int x, y, width, height;

				TextMode.GetScreenSize(out width, out height);
				TextMode.GetCursor(out x, out y);
				y = y + 1; if (y >= height) y = height - 1;
				TextMode.MoveTo(x, y);
				TextMode.RefreshCursor();
#else
				//TODO: Beep?
#endif
				return;
			}
				//case Keys.Enter:
			else if (key == Keys.Enter) {
				TextMode.WriteLine ();
				TextMode.ClearToEndOfLine ();
				TextMode.RefreshCursor ();
				DispatchBuffer ();
				return;
			}
			//}

			// Code to fix keymap issue with caps.
			bool upperCase = shifted;
			if (
				(scancode >= 0x10 && scancode <= 0x26) ||
				(scancode == 0x1e || scancode == 0x1f) ||
				(scancode >= 0x2c && scancode <= 0x2f) ||
				(scancode >= 0x30 && scancode <= 0x32))
				upperCase ^= Keyboard.CapsLock ();

			byte character = Keyboard.Translate (scancode, upperCase);
			if (character == 0) {
				// just so that you can actually see that keyboard input works (& we simply don't know what character you just pressed)...
				TextMode.WriteChar ((byte) '?');
				TextMode.Write ((int) scancode);
				TextMode.RefreshCursor ();
				textBuffer->AppendChar ((byte) 255);
				return;
			}

			int read = TextMode.GetReadPosition ();
			int write = TextMode.GetWritePosition ();
			if (read != write)
				TextMode.SetReadPos (write);

			TextMode.WriteChar (character);
			textBuffer->AppendChar (character);
			TextMode.RefreshCursor ();
		}

		private static void DispatchBuffer ()
		{
			CString8* bufferCopy = CString8.Copy (textBuffer->buffer);
			Diagnostics.Assert (bufferCopy != null, "Prompter::DispatchBuffer(): INSANITY CHECK: CString8.Copy(byte*) returned NULL");
			Prompter.QueueLine (bufferCopy);
			CString8.DISPOSE (bufferCopy);
			textBuffer->Clear ();
		}

		[SharpOS.AOT.Attributes.Label (CONSOLE_TIMER_HANDLER)]
		public static unsafe void Timer (uint ticks)
		{
			if (ticks % SharpOS.Kernel.ADC.Timer.GetFrequency () == 0) {
				int x, y;
				TextMode.GetCursor (out x, out y);
				TextMode.SaveAttributes ();
				TextMode.MoveTo (0, 24);
				TextMode.SetAttributes (TextColor.Yellow, TextColor.Red);
				Clock.Write ();
				TextMode.RestoreAttributes ();
				TextMode.MoveTo (x, y);
			}

			Shell.Prompter.Pulse ();
		}
	}
}


