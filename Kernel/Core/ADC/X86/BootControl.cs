// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT;
using SharpOS.AOT.X86;

namespace SharpOS.Kernel.ADC.X86 {
	/// <summary>
	/// Contains all system power functionality 
	/// </summary>
	/// <remarks>
	/// Change the name to reflect something more than just booting?
	/// (after all, sleeping/resuming etc. fit well together with the rest of the functions)
	/// </remarks>
	/// <todo>
	/// We probably need "Advanced Configuration and Power Interface (ACPI)" support..
	/// ...eventually anyway
	/// </todo>	
	public unsafe class BootControl {
		/// <summary>
		/// Powers down the system.
		/// </summary>
		public static void PowerOff ()
		{
			// TODO: turn off system here..

			// Disable interrupts
			Asm.CLI ();

			// Halt the system
			Asm.HLT ();
		}

		/// <summary>
		/// Freezes the system. 
		/// Usually used after a crash to display information after which the user 
		/// can turn of the machine.
		/// </summary>
		public static void Freeze ()
		{
			// Disable interrupts
			Asm.CLI ();

			// Halt the system
			Asm.HLT ();
		}

		/// <summary>
		/// Puts the system into sleep mode.
		/// </summary>
		public static void Sleep ()
		{
			// TODO: eventually implement this..

			// Disable interrupts
			Asm.CLI ();

			// Halt the system
			Asm.HLT ();
		}

		/// <summary>
		/// Reboot the system.
		/// We can reset the system, oddly enough, through the keyboard IO port
		/// </summary>
		public static void Reboot ()
		{
			// Disable interrupts
			Asm.CLI ();

			// Clear all keyboard buffers (output and command buffers)
			byte temp;
			do {
				temp = IO.ReadByte (IO.Port.KB_controller_commands);
				if ((temp & 0x01) != 0)
					IO.ReadByte (IO.Port.KB_data_port); // Empty keyboard buffer
			} while ((temp & 0x02) != 0);

			// Reset the system
			IO.WriteByte (IO.Port.KB_controller_commands, 0xFE);

			// Halt the system (in case reset didn't work)
			Asm.HLT ();
		}
	}
}
