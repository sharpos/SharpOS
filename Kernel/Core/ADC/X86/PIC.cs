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

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS.Kernel.ADC.X86 {
	/// <summary>
	/// Programmable Interrupt Controller (PIC)
	/// </summary>
	public unsafe class PIC {
		#region Constants
		private static byte MasterIRQMask = 0xFB;
		private static byte SlaveIRQMask = 0xFF;

		private const byte EndOfInterrupt = 0x20;

		private const byte MasterIRQBase = ((byte) IDT.Interrupt.LastException + 1);
		private const byte MasterICW1 = 0x11;
		private const byte MasterICW3 = 0x04; // Connect IRQ2 to the Slave
		private const byte MasterICW4 = 0x01;

		private const byte SlaveIRQBase = MasterIRQBase + 0x8;
		private const byte SlaveICW1 = 0x11;
		private const byte SlaveICW3 = 0x02; // Connect IRQ2 to the Slave
		private const byte SlaveICW4 = 0x01;

		public const byte LastSlaveIRQ = SlaveIRQBase + 0x8;
		#endregion

		#region Setup
		public static void Setup ()
		{
			byte mask1, mask2;

			// save masks
			mask1 = IO.ReadByte (IO.Port.Master_PIC_DataPort);
			mask2 = IO.ReadByte (IO.Port.Slave_PIC_DataPort);


			// Remap the IRQ
			IO.WriteByte (IO.Port.Master_PIC_CommandPort, MasterICW1);
			IO.Delay ();

			IO.WriteByte (IO.Port.Master_PIC_DataPort, MasterIRQBase);
			IO.Delay ();

			IO.WriteByte (IO.Port.Master_PIC_DataPort, MasterICW3);
			IO.Delay ();

			IO.WriteByte (IO.Port.Master_PIC_DataPort, MasterICW4);
			IO.Delay ();


			IO.WriteByte (IO.Port.Slave_PIC_CommandPort, SlaveICW1);
			IO.Delay ();

			IO.WriteByte (IO.Port.Slave_PIC_DataPort, SlaveIRQBase);
			IO.Delay ();

			IO.WriteByte (IO.Port.Slave_PIC_DataPort, SlaveICW3);
			IO.Delay ();

			IO.WriteByte (IO.Port.Slave_PIC_DataPort, SlaveICW4);
			IO.Delay ();


			// restore saved masks.
			IO.WriteByte (IO.Port.Master_PIC_DataPort, mask1);
			IO.WriteByte (IO.Port.Slave_PIC_DataPort, mask2);

			DisableAllIRQs ();
		}
		#endregion

		#region SendEndOfInterrupt
		public static void SendEndOfInterrupt (byte value)
		{
			// Check if this is a hardware interrupt
			if (value < MasterIRQBase ||
				value > LastSlaveIRQ)
				return;

			if (value >= SlaveIRQBase)
				IO.WriteByte (IO.Port.Slave_PIC_CommandPort, EndOfInterrupt);
			IO.WriteByte (IO.Port.Master_PIC_CommandPort, EndOfInterrupt);
		}
		#endregion

		#region DisableAllIRQs
		public static void DisableAllIRQs ()
		{
			MasterIRQMask = (byte) 0xFF;

			IO.WriteByte (IO.Port.Master_PIC_DataPort, MasterIRQMask);
			IO.Delay ();

			SlaveIRQMask = (byte) 0xFF;

			IO.WriteByte (IO.Port.Slave_PIC_DataPort, SlaveIRQMask);
			IO.Delay ();
		}
		#endregion

		#region EnableIRQ
		private static void EnableMasterIRQ (byte value)
		{
			value &= 7;

			if (value == 2)
				return;

			MasterIRQMask &= (byte) ~(1 << value);

			IO.WriteByte (IO.Port.Master_PIC_DataPort, MasterIRQMask);
		}

		private static void EnableSlaveIRQ (byte value)
		{
			value &= 7;

			SlaveIRQMask &= (byte) ~(1 << value);

			IO.WriteByte (IO.Port.Slave_PIC_DataPort, SlaveIRQMask);
		}

		public static void EnableIRQ (byte value)
		{
			// Check if this is a hardware interrupt
			if (value < MasterIRQBase ||
				value > LastSlaveIRQ)
				// TODO: ... what to do with all the other interrupts??
				return;
			if (value < SlaveIRQBase)
				EnableMasterIRQ ((byte) (value - MasterIRQBase));
			else
				EnableSlaveIRQ ((byte) (value - SlaveIRQBase));
		}
		#endregion

		#region DisableIRQ
		private static void DisableMasterIRQ (byte value)
		{
			value &= 7;

			if (value == 2)
				return;

			MasterIRQMask |= (byte) (1 << value);

			IO.WriteByte (IO.Port.Master_PIC_DataPort, MasterIRQMask);
		}

		private static void DisableSlaveIRQ (byte value)
		{
			value &= 7;

			SlaveIRQMask |= (byte) (1 << value);

			IO.WriteByte (IO.Port.Slave_PIC_DataPort, SlaveIRQMask);
		}

		public static void DisableIRQ (byte value)
		{
			// Check if this is a hardware interrupt
			if (value < MasterIRQBase ||
				value > LastSlaveIRQ)
				// TODO: ... what to do with all the other interrupts??
				return;
			if (value < SlaveIRQBase)
				DisableMasterIRQ ((byte) (value - MasterIRQBase));
			else
				DisableSlaveIRQ ((byte) (value - SlaveIRQBase));
		}
		#endregion
	}
}

