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
	///		- move up with pgup/pgdown
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

			// actually not correct because capslock does not behave like shift on all characters...
			bool	upperCase	= (Keyboard.LeftShift() || Keyboard.RightShift()) ^ Keyboard.CapsLock();

			TextMode.SetAttributes(TextColor.Yellow, TextColor.Black);

			switch ((ADC.Keys)scancode)
			{
				case Keys.Insert:
				{
					overwrite = !overwrite;
					SetOverwrite(overwrite);
					return;
				}
				case Keys.Delete:
				{
					return;
				}
				case Keys.Backspace:
				{
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
				}
				case Keys.LeftArrow:
				{
					int x, y, width, height;

					TextMode.GetScreenSize(&width, &height);
					TextMode.GetCursor(&x, &y);
					x = x - 1; if (x < 0) x = 0;
					TextMode.MoveTo(x, y);
					TextMode.RefreshCursor();
					return;
				}
				case Keys.RightArrow:
				{
					int x, y, width, height;

					TextMode.GetScreenSize(&width, &height);
					TextMode.GetCursor(&x, &y);
					x = x + 1; if (x >= width) x = width - 1;
					TextMode.MoveTo(x, y);
					TextMode.RefreshCursor();
					return;
				}
				case Keys.UpArrow:
				{
					int x, y, width, height;

					TextMode.GetScreenSize(&width, &height);
					TextMode.GetCursor(&x, &y);
					y = y - 1; if (y < 0) y = 0;
					TextMode.MoveTo(x, y);
					TextMode.RefreshCursor();
					return;
				}
				case Keys.DownArrow:
				{
					int x, y, width, height;

					TextMode.GetScreenSize(&width, &height);
					TextMode.GetCursor(&x, &y);
					y = y + 1; if (y >= height) y = height - 1;
					TextMode.MoveTo(x, y);
					TextMode.RefreshCursor();
					return;
				}
				case Keys.Enter:
				{
					TextMode.WriteLine();
					TextMode.ClearToEndOfLine();
					TextMode.RefreshCursor();
					return;
				}
			}

			byte key = Keyboard.Translate(scancode, upperCase);
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

