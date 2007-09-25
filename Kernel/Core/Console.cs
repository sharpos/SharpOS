// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using SharpOS;
using SharpOS.ADC;

namespace SharpOS 
{
	/// <summary>
	/// Provides basic console services
	/// </summary>
	/// <todo>
	/// TODO
	/// - text history
	/// - ability to move cursor left / right
	/// - use modifier keys
	/// - add numlock support
	///		just like we're now passing a 'shifted' boolean value to translate, 
	///		should we also pass a 'numlock' boolean value to translate?
	///		maybe this needs to be generalized somehow (eventually)?
	///		what if a keyboard manifacturer has programmable keys/modes etc.?
	/// - create keycode-enum for human readable keycodes
	/// </todo>
	public unsafe class Console
	{
		public const string CONSOLE_KEY_UP_HANDLER		= "CONSOLE_KEY_UP_HANDLER";
		public const string CONSOLE_KEY_DOWN_HANDLER	= "CONSOLE_KEY_DOWN_HANDLER";
		public const string	CONSOLE_TIMER_HANDLER		= "CONSOLE_TIMER_HANDLER";

		private static bool initialized = false;
		private static bool overwrite	= false;

		public static void Setup ()
		{
			Keyboard.RegisterKeyUpEvent (Kernel.GetFunctionPointer(CONSOLE_KEY_UP_HANDLER));
			Keyboard.RegisterKeyDownEvent (Kernel.GetFunctionPointer(CONSOLE_KEY_DOWN_HANDLER));
			Architecture.RegisterTimerEvent(Kernel.GetFunctionPointer(CONSOLE_TIMER_HANDLER));
			
			initialized = true;
			TextMode.RefreshCursor ();
			SetOverwrite(overwrite);
		}
		
		public static unsafe void SetOverwrite (bool _overwrite)
		{
			overwrite = _overwrite;
			
			if (overwrite)
				TextMode.SetCursorSize(0, 15);
			else
				TextMode.SetCursorSize(13, 15);
		}

		[SharpOS.AOT.Attributes.Label (CONSOLE_KEY_UP_HANDLER)]
		public static unsafe void KeyUp (uint scancode)
		{
			if (!initialized)
				return;
		}

		[SharpOS.AOT.Attributes.Label (CONSOLE_KEY_DOWN_HANDLER)]
		public static unsafe void KeyDown (uint scancode)
		{
			if (!initialized)
				return;

			byte	key = 0;

			bool upperCase = (Keyboard.LeftShift() || Keyboard.RightShift()) ^ Keyboard.CapsLock();

			TextMode.SetAttributes(TextColor.Yellow, TextColor.Black);

			switch ((ADC.Keys)scancode)
			{
				case Keys.Insert: 
					overwrite = !overwrite;
					SetOverwrite(overwrite);
					return;
				case Keys.Delete:
					break;

				case Keys.Backspace:
					int x, y, width, height;

					TextMode.GetScreenSize(&width, &height);
					TextMode.GetCursor(&x, &y); 
					x--;
					if (x < 0)
					{
						x = width - 1;
						y--;
						if (y < 0)
						{
							y = 0;
							return;
						}
					}
					TextMode.MoveTo(x, y);
					TextMode.WriteChar((byte)' ');
					TextMode.MoveTo(x, y);
					TextMode.RefreshCursor();
					return;

				case Keys.LeftArrow:
					return;
				case Keys.RightArrow:
					return;

				case Keys.UpArrow:
					return;
				case Keys.DownArrow:
					return;

				case Keys.Enter:
					TextMode.WriteLine();
					TextMode.RefreshCursor();
					return;
			}

			key = Keyboard.Translate(scancode, upperCase);

			if (key == 0)
			{
				// just so that you can actually see that keyboard input works (& we simply don't know what character you just pressed)...
				TextMode.WriteChar((byte)'?');
				TextMode.RefreshCursor();
				return;
			}

			TextMode.WriteChar (key);
			TextMode.RefreshCursor ();
		}

		[SharpOS.AOT.Attributes.Label(CONSOLE_TIMER_HANDLER)]
		public static unsafe void Timer (uint ticks)
		{
			if (ticks % SharpOS.ADC.Timer.GetFrequency () == 0)
			{
				int x, y;

				TextMode.GetCursor(&x, &y);
				TextMode.SaveAttributes();
				TextMode.MoveTo (0, 24);
				TextMode.SetAttributes (TextColor.Yellow, TextColor.Red);
				TextMode.WriteLine("Timer ticks: ", (int)ticks);
				TextMode.RestoreAttributes();
				TextMode.MoveTo(x, y);
			}
		}
	}
}

