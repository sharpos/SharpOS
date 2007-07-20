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

namespace SharpOS.ADC.X86 
{
	// TODO: Create 'key' enum
	// TODO: Handle the more strange scancodes
	
	public unsafe class Keyboard
	{
		public const string		KEYBOARD_HANDLER	= "KEYBOARD_HANDLER";

		public static uint keyUpEventCount = 0;
		public static uint *keyUpEvent = null;
		public static uint keyDownEventCount = 0;
		public static uint *keyDownEvent = null;
		
		static bool leftShift;
		static bool rightShift;

		static bool leftAlt;
		static bool rightAlt;

		static bool leftControl;
		static bool rightControl;

		static bool scrollLock;
		static bool capsLock;
		static bool numLock;
	
		static byte* keymap = null;
		
		public static void Setup ()
		{
			keyUpEvent = (uint*) Kernel.Alloc (sizeof (uint) * 4);
			keyUpEventCount = 4;
			keyDownEvent = (uint*) Kernel.Alloc (sizeof (uint) * 4);
			keyDownEventCount = 4;
			
			IDT.SetupIRQ (1, Kernel.GetFunctionPointer (KEYBOARD_HANDLER));
			
			keymap = Kernel.Alloc (300);
			KeyboardLayouts.US (keymap);
		}

		#region KeyUpEvent
		public static EventRegisterStatus RegisterKeyUpEvent(uint address)
		{
			for (int x = 0; x < keyUpEventCount; ++x)
				if (keyUpEvent [x] == address)
					return EventRegisterStatus.AlreadySubscribed;
			
			for (int x = 0; x < keyUpEventCount; ++x) {
				if (keyUpEvent [x] == 0) {
					keyUpEvent [x] = address;
					
					return EventRegisterStatus.Success;
				}
			}
			
			return EventRegisterStatus.CapacityExceeded;
		}
		#endregion

		#region KeyDownEvent
		public static EventRegisterStatus RegisterKeyDownEvent(uint address)
		{
			for (int x = 0; x < keyDownEventCount; ++x)
				if (keyDownEvent [x] == address)
					return EventRegisterStatus.AlreadySubscribed;
			
			for (int x = 0; x < keyDownEventCount; ++x) {
				if (keyDownEvent [x] == 0) {
					keyDownEvent [x] = address;
					
					return EventRegisterStatus.Success;
				}
			}
			
			return EventRegisterStatus.CapacityExceeded;
		}
		#endregion

		#region KeyboardCommands
		enum KeyboardCommands
		{
			Set_Keyboard_LEDs			= 0xed,
			Echo						= 0xee,	// (Diagnostics)
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
			Resend_Last_Transmission	= 0xfe,
			Internal_Diagnostics		= 0xff,
		}
		#endregion

		#region KeyboardMessages
		enum KeyboardMessages
		{
			Too_Many_Keys			= 0x00,	// Too many keys are being pressed at once
			Basic_Assurance_Test	= 0xaa,
			Echo_Command_Result		= 0xee,
			Acknowledge				= 0xfa,	// Sent by every command, except eeh and feh
			BAT_Failed				= 0xfc,
			Request_Resend			= 0xfe,	// Resend your data please
			Keyboard_Error			= 0xff
		}
		#endregion

		#region SendCommand
		private static unsafe void WaitUntillReady()
		{
			while ((IO.In8(IO.Ports.KB_controller_commands) & 0x02) != 0)
				;
		}

		private static unsafe void SendCommand(KeyboardCommands command)
		{
			KeyboardMessages message = KeyboardMessages.Acknowledge;
			do
			{
				IO.Out8(IO.Ports.KB_data_port, (byte)command);
			
				// Wait for acknowledge...
				while ((IO.In8(IO.Ports.KB_controller_commands) & 0x01) != 0)
					;
			
				// receive acknowledge
				message = (KeyboardMessages)IO.In8(IO.Ports.KB_data_port);
				if (message == KeyboardMessages.Request_Resend) 
					continue; 
				else 
				if (message == KeyboardMessages.Acknowledge)
					return;
				else
					//FIXME: We should throw an exception here (or something)
					return;
			} while (message != KeyboardMessages.Acknowledge);
		}

		private static unsafe void SendCommand(KeyboardCommands command, byte value)
		{
			WaitUntillReady ();

			SendCommand (KeyboardCommands.Set_Keyboard_LEDs);

			WaitUntillReady ();

			IO.Out8 (IO.Ports.KB_data_port, value);
		}
		#endregion

		#region SetLEDs
		public static unsafe void SetLEDs (bool capslock, bool numlock, bool scrolllock)
		{
			byte leds = 0;
			if (capslock)		leds |= (byte)1;
			if (numlock)		leds |= (byte)2;
			if (scrolllock)		leds |= (byte)4;

			SendCommand (KeyboardCommands.Set_Keyboard_LEDs, leds);
		}
		#endregion

		[SharpOS.AOT.Attributes.Label(KEYBOARD_HANDLER)]
		private static unsafe void KeyboardHandler (IDT.ISRData data)
		{
			// Read from the keyboard's data buffer
			byte	input		= IO.In8 (IO.Ports.KB_data_port);
			/*
			if (input == KeyboardMessages.Too_Many_Keys ||
				input == KeyboardMessages.Keyboard_Error)
			{
				//TODO: do something usefull here..
				return;
			}*/

			uint	scancode;
			bool	pressed;

			if (input == 0xe0)
			{
				input		= IO.In8 (IO.Ports.KB_data_port);

				scancode	= (uint)((input & 0x7F) >> 8) | 0xe0;
				pressed		= (input & 0x80) == 0;
			} else
			if (input == 0xe1)
			{
				input		= IO.In8 (IO.Ports.KB_data_port);

				scancode	= (uint)(input & 0x7F);
				pressed		= (input & 0x80) == 0;
				return;
			} else
			{
				scancode	= (uint)(input & 0x7F);
				pressed		= (input & 0x80) == 0;
			}

			if (scancode == 0x1d)	{ leftControl	= pressed;	return; } else
			if (scancode == 0x2a)	{ leftShift		= pressed;	return; } else
			if (scancode == 0x38)	{ leftAlt		= pressed;	return; } else
			if (scancode == 0x3a)	{ capsLock		= pressed;	return; } else
			if (scancode == 0x45)	{ numLock		= pressed;	return; } else
			if (scancode == 0x46)	{ scrollLock	= pressed;	return; } else
			if (scancode == 0xe038) { rightAlt		= pressed;	return; } else
			if (scancode == 0xe01d) { rightControl	= pressed;	return; } else
			if (scancode == 0xe036) { rightShift	= pressed;	return; }

			if (pressed)
			{
				for (int x = 0; x < keyDownEventCount; ++x) {
					if (keyDownEvent [x] == 0)
						continue;
					
					Kernel.Call (keyDownEvent [x], scancode);
				}
			} else
			{
				for (int x = 0; x < keyUpEventCount; ++x) {
					if (keyUpEvent [x] == 0)
						continue;
					
					Kernel.Call (keyUpEvent [x], scancode);
				}
				
			}
		}

		public static unsafe bool LeftShift ()
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
	
		public static byte *GetKeymap ()
		{
			return keymap;
		}
		
		public static byte Translate (uint scancode)
		{
			Kernel.Assert (keymap != null, Kernel.String ("No keymap is available!"));
			
			if (scancode > 0x80)
				return (byte)'\0';
			
			return keymap [(byte) scancode];
		}
	}
}

