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
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SharpOS;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
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
	/// </todo>
	public unsafe class Console
	{
		public const string CONSOLE_KEY_UP_HANDLER = "CONSOLE_KEY_UP_HANDLER";
		public const string CONSOLE_KEY_DOWN_HANDLER = "CONSOLE_KEY_DOWN_HANDLER";
		public const string CONSOLE_TIMER_HANDLER = "CONSOLE_TIMER_HANDLER";

		private static bool initialized = false;
		private static bool overwrite	= false;
		private static bool capslock	= false;
		private static bool numlock		= false;
		private static bool scrolllock	= false;

		public static void Setup ()
		{
			TextMode.WriteLine ();
			
			Keyboard.RegisterKeyUpEvent (Kernel.GetFunctionPointer(CONSOLE_KEY_UP_HANDLER));
			Keyboard.RegisterKeyDownEvent (Kernel.GetFunctionPointer(CONSOLE_KEY_DOWN_HANDLER));
			Arch.RegisterTimerEvent(Kernel.GetFunctionPointer(CONSOLE_TIMER_HANDLER));
			
			initialized = true;
			TextMode.RefreshCursor ();
			SetOverwrite (true);

			capslock	= false;
			numlock		= true;
			scrolllock	= true;
			
			Keyboard.SetLEDs (capslock, numlock, scrolllock);
		}
		
		public static unsafe void SetOverwrite (bool _overwrite)
		{
			overwrite = _overwrite;
			
			if (overwrite)
				TextMode.SetCursorSize(0, 15);
			else
				TextMode.SetCursorSize(13, 15);
		}

		public static unsafe void SetCapsLock (bool _capslock)
		{
			capslock = _capslock;
			Keyboard.SetLEDs (capslock, numlock, scrolllock);
		}

		public static unsafe void SetNumLock (bool _numlock)
		{
			numlock = _numlock;
			Keyboard.SetLEDs (capslock, numlock, scrolllock);
		}

		public static unsafe void SetScrollLock (bool _scrolllock)
		{
			scrolllock = _scrolllock;
			Keyboard.SetLEDs (capslock, numlock, scrolllock);
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

			byte key = 0;
			
			if (Keyboard.LeftShift () || Keyboard.RightShift ())
				key = Keyboard.Translate (scancode, true);
			else
				key = Keyboard.Translate (scancode, false);

			
			if (key == 0)
				return;

			key = ASCII.ToUpper (key);

			TextMode.SetAttributes (TextColor.Yellow, TextColor.Black);
			TextMode.WriteChar (key);
			TextMode.RefreshCursor ();
		}

		[SharpOS.AOT.Attributes.Label(CONSOLE_TIMER_HANDLER)]
		public static unsafe void Timer (uint ticks)
		{
			if (ticks % SharpOS.ADC.Timer.GetFrequency () == 0)
			{
				int x, y;
				
				TextMode.GetCursor (&x, &y);
				TextMode.MoveTo (0, 24);
				TextMode.SetAttributes (TextColor.Yellow, TextColor.Red);
				TextMode.WriteLine ("Timer ticks: ", (int)ticks);
				TextMode.MoveTo (x, y);
			}
		}
	}
}

