// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using SharpOS.AOT;
using SharpOS.AOT.X86;

namespace SharpOS.ADC.X86 {
	public unsafe class BootControl {
		/**
			<summary>
				Checks for compatibility with the current system, using 
				the most well-supported method possible. 
			</summary>
		*/
		public static void Halt()
		{
			// TODO: 'halt' is a bit vague, should we be trying to turn off the system here?

			// Disable interrupts
			Asm.CLI();

			// Halt the system
			Asm.HLT();
		}


		/// <summary>
		/// We can reset the system, oddly enough, through the keyboard IO port
		/// </summary>
		public static void Reboot()
		{
			// Disable interrupts
			Asm.CLI();
					
			// Clear all keyboard buffers (output and command buffers)
			byte temp;
			do
			{
				temp = IO.In8(IO.Port.KB_controller_commands);
				if ((temp & 0x01) != 0)
					IO.In8(IO.Port.KB_data_port); // Empty keyboard buffer
			} while ((temp & 0x02) != 0);

			// Reset the system
			IO.Out8(IO.Port.KB_controller_commands, 0xFE);

			// Halt the system (in case reset didn't work)
			Asm.HLT();
		}
	}
}
