// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	William Lahti <xfurious@gmail.com>
//	Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.Kernel.ADC;

// VERY EXPERIMENTAL!

namespace SharpOS.Kernel.DriverSystem.Character
{
	public class KeyboardDriver : GenericDriver, IIRQCallBack
	{
		public const uint KeyboardBufferSize = 1024;

		private IOPortStream ControllerCommands;
		private IOPortStream DataPort;
		private IRQHandler KeyboardIRQ;
		private MemoryBlock keyBuffer;
		private uint keysInBuffer;
		private SpinLock spinLock;

		#region Initialize
		public override bool Initialize (IDriverContext context)
		{
			context.Initialize();

			keyBuffer.Allocate(KeyboardBufferSize);
			keysInBuffer = 0;

			ControllerCommands = context.CreateIOPortStream((ushort)0x0064);//IO.Port.KB_controller_commands
			DataPort = context.CreateIOPortStream((ushort)0x0060);//IO.Port.KB_data_port

			// Disable so this doesn't interfere with existing Keyboard interface
			//KeyboardIRQ = context.CreateIRQHandler(1);
			//KeyboardIRQ.AssignCallBack(this);

			return (isInitialized = false);
		}
		#endregion

		#region Key State Variables

		private bool scrollLock = false;
		private bool capsLock = false;
		private bool numLock = false;

		private bool scrollLockReleased = true;
		private bool capsLockReleased = true;
		private bool numLockReleased = true;

		#endregion

		#region Enumerations

		enum KeyboardCommands
		{
			Set_Keyboard_LEDs = 0xed,
			Echo = 0xee,	// (Diagnostics)
			Select_Scancode_Set = 0xf0,
			/*
			 0: return current set number: 1:'C', 2:'A', 3:'?'
			 1: set scancode set no 1
			 2: set scancode set no 2 -> standard
			 3: set scancode set no 3
			*/
			Identify_Keyboard = 0xf2,
			Typematic_Rate = 0xf3,
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
			Enable_Keyboard = 0xF4,	// It clears its buffer and starts scanning.
			Disable_Scanning = 0xF5,// Reset keyboard
			Enable_Scanning = 0xF6,	// Reset keyboard
			Resend_Last_Transmission = 0xFE,
			Internal_Diagnostics = 0xFF,
		}

		enum KeyboardMessages
		{
			Too_Many_Keys = 0x00,	// Too many keys are being pressed at once
			Unknown1 = 0x3A, // This is causing our CAPS issue.
			Unknown2 = 0xBA, // This is causing our CAPS issue in qemu.
			Basic_Assurance_Test = 0xAA,
			Echo_Command_Result = 0xEE,
			Acknowledge = 0xFA,	// Sent by every command, except eeh and feh
			BAT_Failed = 0xFC,
			Request_Resend = 0xFE,	// Resend your data please
			Keyboard_Error = 0xFF
		}
		#endregion

		#region Internal

		private bool WaitUntilReady ()
		{
			// wait for register to be ready
			while (true) {
				uint status = ControllerCommands.Read8();

				if ((status & 0x20) == 0x00)
					return true;

				//TODO: add timeout check
			}

			//return false;
		}

		private void SendCommand (KeyboardCommands command)
		{
			KeyboardMessages message = KeyboardMessages.Acknowledge;

			do {
				DataPort.Write8((byte)command);

				// Wait for acknowledge and receieve it
				WaitUntilReady();

				message = (KeyboardMessages)DataPort.Read8();

				if (message == KeyboardMessages.Request_Resend)
					continue;
				else if (message == KeyboardMessages.Acknowledge)
					return;
				else if (message == KeyboardMessages.Unknown1) // This was the cause of the caps issue.
					return;
				else if (message == KeyboardMessages.Unknown2) // This was the cause of the caps issue in qemu.
                {
					capsLock = capsLock ^ capsLockReleased;
					capsLockReleased = true;
					return;
				}
				else {
					//Diagnostics.Error ("ADC.X86.Keyboard.SendCommand(): unhandled message");
					return;
				}

			} while (message != KeyboardMessages.Acknowledge);
		}

		private void SendCommand (KeyboardCommands command, byte value)
		{
			WaitUntilReady();
			SendCommand(KeyboardCommands.Set_Keyboard_LEDs);

			WaitUntilReady();
			DataPort.Write8(value);
		}

		public bool OnInterrupt (uint irq)
		{
			// Read from the keyboard's data buffer
			byte input;
			uint scancode;
			bool pressed;

			try {
				spinLock.Enter();

				input = DataPort.Read8();

				if (input == 0xE0) {
					input = DataPort.Read8();
					scancode = (uint)((input & 0x7F) >> 8) | 0xe0;
					pressed = (input & 0x80) == 0;

				}
				else if (input == 0xE1) {
					input = DataPort.Read8();
					scancode = (uint)(input & 0x7F);
					pressed = (input & 0x80) == 0;
					return true;

				}
				else {
					scancode = (uint)(input & 0x7F);
					pressed = (input & 0x80) == 0;
				}

				if (scancode == (uint)Keys.CapsLock) {	// CapsLock
					if (pressed) {
						if (capsLockReleased)
							capsLock = !capsLock;
						capsLockReleased = false;
						SetLEDs();
					}
					else
						capsLockReleased = true;
					return true;
				}
				else if (scancode == (uint)Keys.NumLock) {	// NumLock
					if (pressed) {
						if (numLockReleased)
							numLock = !numLock;
						numLockReleased = false;
						SetLEDs();
					}
					else
						numLockReleased = true;
					return true;
				}
				else if (scancode == (uint)Keys.ScrollLock) {	// ScrollLock
					if (pressed) {
						if (scrollLockReleased)
							scrollLock = !scrollLock;
						scrollLockReleased = false;
						SetLEDs();
					}
					else
						scrollLockReleased = true;
					return true;
				}

				if (((keysInBuffer + 1) * 4) > keyBuffer.length)
					return true; // ignoring keys

				keyBuffer.SetUInt(keysInBuffer * 4, scancode);
				keysInBuffer++;

				return true;
			}
			finally {
				spinLock.Exit();

				//TODO: pulse listeners?
			}

		}

		public uint GetScanCode ()
		{
			spinLock.Enter();

			uint scancode = keyBuffer.GetUInt(0);

			//TODO: this could be improved with a cyclic buffer
			for (uint i = 1; i < keysInBuffer; i++)
				keyBuffer.SetUInt((i - 1) * 4, keyBuffer.GetUInt(i * 4));

			keysInBuffer--;

			spinLock.Exit();

			return scancode;
		}

		public bool IsScrollLock
		{
			get
			{
				return scrollLock;
			}
		}

		public bool IsCapsLock
		{
			get
			{
				return capsLock;
			}
		}

		public bool IsNumLock
		{
			get
			{
				return numLock;
			}
		}

		private void SetLEDs ()
		{
			byte leds = 0;

			if (scrollLock)
				leds |= (byte)1;

			if (numLock)
				leds |= (byte)2;

			if (capsLock)
				leds |= (byte)4;

			SendCommand(KeyboardCommands.Set_Keyboard_LEDs, leds);
		}
		#endregion
	}

}
