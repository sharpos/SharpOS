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
using ADC = SharpOS.ADC;

namespace SharpOS.ADC.X86 
{
	// TODO: Create 'key' enum
	// TODO: Handle the more strange scancodes
	
	public unsafe class Keyboard
	{
		#region Global fields
		
		unsafe static uint *keyUpEvent = (uint*)Kernel.StaticAlloc (sizeof (uint) * Kernel.MaxEventHandlers);
		unsafe static uint *keyDownEvent = (uint*)Kernel.StaticAlloc (sizeof (uint) * Kernel.MaxEventHandlers);
		
		static bool leftShift;
		static bool rightShift;

		static bool leftAlt;
		static bool rightAlt;

		static bool leftControl;
		static bool rightControl;

		static bool scrollLock = false;
		static bool capsLock = false;
		static bool numLock = false;

		static bool scrollLockReleased = true;
		static bool capsLockReleased = true;
		static bool numLockReleased = true;
	
		unsafe static byte* defaultMap = null;
		unsafe static byte* shiftedMap = null;

		static int defaultMapLen = 0;
		static int shiftedMapLen = 0;
		static bool keymapInitialized = false;

		#endregion
		#region Constants

		const string KEYBOARD_HANDLER = "KEYBOARD_HANDLER";
		
		#endregion
		#region Enumerations

		enum KeyboardCommands
		{
			Set_Keyboard_LEDs			= 0xed,
			Echo					= 0xee,	// (Diagnostics)
			Select_Scancode_Set			= 0xf0,
			/*
			 0: return current set number: 1:'C', 2:'A', 3:'?'
			 1: set scancode set no 1
			 2: set scancode set no 2 -> standard
			 3: set scancode set no 3
			*/
			Identify_Keyboard			= 0xf2,
			Typematic_Rate				= 0xf3,
			/*
			 Send a second byte with:

			 bit 0 -> 4: rate. Timings:

			 0: 30 keys/sec		10: 10
			 1: 26.7			13: 9
			 2: 24				16: 7.5
			 4: 20				20: 5
			 8: 15				31: 2

			 bit 5 & 6: pause before repeat:

			 0: 250 ms
			 1: 500
			 2: 750
			 4: 1000

			 bit 7: Always 0
			 */
			Enable_Keyboard				= 0xf4,	// It clears its buffer and starts scanning.
			Disable_Scanning			= 0xf5,	// Reset keyboard
			Enable_Scanning				= 0xf6,	// Reset keyboard
			Resend_Last_Transmission		= 0xfe,
			Internal_Diagnostics			= 0xff,
		}

		enum KeyboardMessages
		{
			Too_Many_Keys			= 0x00,	// Too many keys are being pressed at once
			Basic_Assurance_Test		= 0xaa,
			Echo_Command_Result		= 0xee,
			Acknowledge			= 0xfa,	// Sent by every command, except eeh and feh
			BAT_Failed			= 0xfc,
			Request_Resend			= 0xfe,	// Resend your data please
			Keyboard_Error			= 0xff
		}

		#endregion
		#region Setup ()
		
		public static void Setup ()
		{
			IDT.RegisterIRQ (IDT.IRQ.Keyboard, Kernel.GetFunctionPointer (KEYBOARD_HANDLER));
		}

		#endregion
		#region Internal
		
		static void WaitUntilReady ()
		{
			while ((IO.In8 (IO.Ports.KB_controller_commands) & 0x02) != 0);
		}

		static void SendCommand (KeyboardCommands command)
		{
			KeyboardMessages message = KeyboardMessages.Acknowledge;

			do
			{
				IO.Out8 (IO.Ports.KB_data_port, (byte)command);
			
				// Wait for acknowledge and receieve it

				WaitUntilReady();

				message = (KeyboardMessages)IO.In8 (IO.Ports.KB_data_port);

				if (message == KeyboardMessages.Request_Resend) 
					continue; 
				else if (message == KeyboardMessages.Acknowledge)
					return;
				else {
					Kernel.Error ("ADC.X86.Keyboard.SendCommand(): unhandled message");
					return;
				}
				
			} while (message != KeyboardMessages.Acknowledge);
		}

		static void SendCommand (KeyboardCommands command, byte value)
		{
			WaitUntilReady ();

			SendCommand (KeyboardCommands.Set_Keyboard_LEDs);

			WaitUntilReady();

			IO.Out8(IO.Ports.KB_data_port, value);
		}

		[SharpOS.AOT.Attributes.Label (KEYBOARD_HANDLER)]
		static unsafe void KeyboardHandler (IDT.ISRData data)
		{
			// Read from the keyboard's data buffer
			byte input;
			uint scancode;
			bool pressed;

			input = IO.In8 (IO.Ports.KB_data_port);

			/* XXX: why is this commented out?
			
			if (input == KeyboardMessages.Too_Many_Keys ||
				input == KeyboardMessages.Keyboard_Error)
			{
				// TODO: do something usefull here..
				return;
			}

			*/
			
			if (input == 0xe0) {
				input		= IO.In8 (IO.Ports.KB_data_port);
				scancode	= (uint)((input & 0x7F) >> 8) | 0xe0;
				pressed		= (input & 0x80) == 0;
				
			} else if (input == 0xe1) {
				input		= IO.In8 (IO.Ports.KB_data_port);
				scancode	= (uint)(input & 0x7F);
				pressed		= (input & 0x80) == 0;
				return;
				
			} else {
				scancode	= (uint)(input & 0x7F);
				pressed		= (input & 0x80) == 0;
			}
			
			if (scancode == (uint)Keys.CapsLock) {					// CapsLock
				if (pressed)
				{
					if (capsLockReleased)
						capsLock = !capsLock;
					capsLockReleased = false;
					SetLEDs();
				} else
					capsLockReleased = true;
				return;
			} else if (scancode == (uint)Keys.NumLock) {			// NumLock
				if (pressed)
				{
					if (numLockReleased)
						numLock = !numLock;
					numLockReleased = false;
					SetLEDs();
				} else
					numLockReleased = true;
				return;
			} else if (scancode == (uint)Keys.ScrollLock) {			// ScrollLock
				if (pressed)
				{
					if (scrollLockReleased)
						scrollLock = !scrollLock;
					scrollLockReleased = false;
					SetLEDs();
				} else
					scrollLockReleased = true;
				return;
			} else if (scancode == (uint)Keys.LeftControl) {		 // left control
				leftControl = pressed;
				return;
			} else if (scancode == (uint)Keys.LeftShift) {	 // left shift
				leftShift = pressed;
				return;
			} else if (scancode == (uint)Keys.LeftAlt) {	 // left alt
				leftAlt = pressed;
				return;
			} else if (scancode == (uint)Keys.RightAlt) { // right alt
				rightAlt = pressed;
				return;
			} else if (scancode == (uint)Keys.RightControl) { // right control
				rightControl = pressed;
				return;
			} else if (scancode == (uint)Keys.RightShift) { // right shift
				rightShift = pressed;
				return;
			}

			if (pressed) {
				for (int x = 0; x < Kernel.MaxEventHandlers; ++x) {
					if (keyDownEvent [x] == 0)
						continue;
					
					Memory.Call (keyDownEvent [x], scancode);
				}
			} else {
				for (int x = 0; x < Kernel.MaxEventHandlers; ++x) {
					if (keyUpEvent [x] == 0)
						continue;

					Memory.Call(keyUpEvent[x], scancode);
				}
				
			}
		}

		#endregion
		#region ADC implementation
		
		public unsafe static void SetKeyMap (byte *defMap, int defLen, byte *shiftMap,
					      int shiftLen)
		{
			defaultMap = defMap;
			shiftedMap = shiftMap;
			defaultMapLen = defLen;
			shiftedMapLen = shiftLen;
			keymapInitialized = true;
		}
		
		public unsafe static EventRegisterStatus RegisterKeyUpEvent (uint address)
		{
			for (int x = 0; x < Kernel.MaxEventHandlers; ++x)
				if (keyUpEvent [x] == address)
					return EventRegisterStatus.AlreadySubscribed;
			
			for (int x = 0; x < Kernel.MaxEventHandlers; ++x) {
				if (keyUpEvent [x] == 0) {
					keyUpEvent [x] = address;
					
					return EventRegisterStatus.Success;
				}
			}
			
			return EventRegisterStatus.CapacityExceeded;
		}

		public unsafe static EventRegisterStatus RegisterKeyDownEvent (uint address)
		{
			for (int x = 0; x < Kernel.MaxEventHandlers; ++x)
				if (keyDownEvent [x] == address)
					return EventRegisterStatus.AlreadySubscribed;
			
			for (int x = 0; x < Kernel.MaxEventHandlers; ++x) {
				if (keyDownEvent [x] == 0) {
					keyDownEvent [x] = address;
					
					return EventRegisterStatus.Success;
				}
			}
			
			return EventRegisterStatus.CapacityExceeded;
		}

		public static void SetLEDs ()
		{
			byte leds = 0;

			if (scrollLock)
				leds |= (byte)1;
				
			if (numLock)
				leds |= (byte)2;
				
			if (capsLock)
				leds |= (byte)4;

			SendCommand (KeyboardCommands.Set_Keyboard_LEDs, leds);
		}

		public static bool LeftShift ()
		{
			return leftShift;
		}
		
		public static bool RightShift ()
		{
			return rightShift;
		}

		public static bool LeftAlt ()
		{
			return leftAlt;
		}
		
		public static bool RightAlt ()
		{
			return rightAlt;
		}

		public static bool LeftControl ()
		{
			return leftControl;
		}
		
		public static bool RightControl ()
		{
			return rightControl;
		}

		public static bool ScrollLock ()
		{
			return scrollLock;
		}
		
		public static bool CapsLock ()
		{
			return capsLock;
		}
		
		public static bool NumLock ()
		{
			return numLock;
		}
	
		public unsafe static byte *GetCurrentDefaultTable (int *ret_len)
		{
			*ret_len = defaultMapLen;
			return shiftedMap;
		}
		
		public unsafe static byte *GetCurrentShiftedTable (int *ret_len)
		{
			*ret_len = shiftedMapLen;
			return shiftedMap;
		}
		
		public unsafe static byte Translate (uint scancode, bool shifted)
		{
			if (!keymapInitialized)
			{
				Kernel.Assert(false, "Keymap not initialized!");
				return (byte)'?';
			}
			Kernel.Assert (shiftedMap != null, "No shifted map is available!");
			Kernel.Assert (defaultMap != null, "No default map is available!");

			if (shifted)
				return shiftedMap [(byte) scancode];
			else
				return defaultMap [(byte) scancode];
		}

		#endregion
	}
}

