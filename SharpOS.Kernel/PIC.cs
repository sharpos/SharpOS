// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
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

namespace SharpOS.ADC.X86 {
	public unsafe class PIC {
		private static byte MasterIRQMask = 0xFB;
		private static byte SlaveIRQMask = 0xFF;

		public const byte EndOfInterrupt = 0x20;

		public const byte MasterIRQBase = 0x20;
		private const ushort MasterPICCommandPort = 0x20;
		private const ushort MasterPICDataPort = 0x21;
		private const byte MasterICW1 = 0x11;
		private const byte MasterICW3 = 0x04; // Connect IRQ2 to the Slave
		private const byte MasterICW4 = 0x01;

		public const byte SlaveIRQBase = 0x28;
		private const ushort SlavePICCommandPort = 0x70;
		private const ushort SlavePICDataPort = 0x71;
		private const byte SlaveICW1 = 0x11;
		private const byte SlaveICW3 = 0x02; // Connect IRQ2 to the Slave
		private const byte SlaveICW4 = 0x01;

		public static void Setup ()
		{
			// Remap the IRQ
			IO.Out8 (MasterPICCommandPort, MasterICW1);
			IO.Delay ();

			IO.Out8 (MasterPICDataPort, MasterIRQBase);
			IO.Delay ();

			IO.Out8 (MasterPICDataPort, MasterICW3);
			IO.Delay ();

			IO.Out8 (MasterPICDataPort, MasterICW4);
			IO.Delay ();


			IO.Out8 (SlavePICCommandPort, SlaveICW1);
			IO.Delay ();

			IO.Out8 (SlavePICDataPort, SlaveIRQBase);
			IO.Delay ();

			IO.Out8 (SlavePICDataPort, SlaveICW3);
			IO.Delay ();

			IO.Out8 (SlavePICDataPort, SlaveICW4);
			IO.Delay ();


			DisableAllIRQs ();
		}

		public static void SendMasterEndOfInterrupt ()
		{
			IO.Out8 (MasterPICCommandPort, EndOfInterrupt);
		}

		public static void SendSlaveEndOfInterrupt ()
		{
			IO.Out8 (SlavePICCommandPort, EndOfInterrupt);
		}

		public static void DisableAllIRQs ()
		{
			MasterIRQMask = (byte) 0xFF;

			IO.Out8 (MasterPICDataPort, MasterIRQMask);
			IO.Delay ();

			SlaveIRQMask = (byte) 0xFF;

			IO.Out8 (SlavePICDataPort, SlaveIRQMask);
			IO.Delay ();
		}

		public static void EnableMasterIRQ (byte value)
		{
			value &= 7;

			if (value == 2)
				return;

			MasterIRQMask &= (byte) ~(1 << value);

			IO.Out8 (MasterPICDataPort, MasterIRQMask);
		}

		public static void EnableSlaveIRQ (byte value)
		{
			value &= 7;

			SlaveIRQMask &= (byte) ~(1 << value);

			IO.Out8 (SlavePICDataPort, SlaveIRQMask);
		}

		public static void EnableIRQ (byte value)
		{
			if (value < 8)
				EnableMasterIRQ (value);

			else if (value < 16)
				EnableSlaveIRQ ((byte) (value - 8));
		}

		public static void DisableMasterIRQ (byte value)
		{
			value &= 7;

			if (value == 2)
				return;

			MasterIRQMask |= (byte) (1 << value);

			IO.Out8 (MasterPICDataPort, MasterIRQMask);
		}

		public static void DisableSlaveIRQ (byte value)
		{
			value &= 7;

			SlaveIRQMask |= (byte) (1 << value);

			IO.Out8 (SlavePICDataPort, SlaveIRQMask);
		}

		public static void DisableIRQ (byte value)
		{
			if (value < 8)
				DisableMasterIRQ (value);

			else if (value < 16)
				DisableSlaveIRQ ((byte) (value - 8));
		}
	}
}

