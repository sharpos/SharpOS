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

			// Disable all IRQs (except IRQ2)
			IO.Out8 (MasterPICDataPort, 0xFB);
			IO.Delay ();

			IO.Out8 (SlavePICDataPort, 0xFF);
			IO.Delay ();
		}
	}
}

