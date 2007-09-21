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

namespace SharpOS.ADC.X86
{
	/// <summary>
	/// Programmable Interrupt Controller (PIC)
	/// </summary>
	public unsafe class PIC
	{
		private static byte MasterIRQMask = 0xFB;
		private static byte SlaveIRQMask = 0xFF;

		public const byte EndOfInterrupt = 0x20;

		public const byte MasterIRQBase = 0x20;
		private const byte MasterICW1 = 0x11;
		private const byte MasterICW3 = 0x04; // Connect IRQ2 to the Slave
		private const byte MasterICW4 = 0x01;

		public const byte SlaveIRQBase = 0x28;
		private const byte SlaveICW1 = 0x11;
		private const byte SlaveICW3 = 0x02; // Connect IRQ2 to the Slave
		private const byte SlaveICW4 = 0x01;

		public static void Setup()
		{
			// Remap the IRQ
			IO.Out8(IO.Port.PIC_CommandPort, MasterICW1);
			IO.Delay();

			IO.Out8(IO.Port.PIC_DataPort, MasterIRQBase);
			IO.Delay();

			IO.Out8(IO.Port.PIC_DataPort, MasterICW3);
			IO.Delay();

			IO.Out8(IO.Port.PIC_DataPort, MasterICW4);
			IO.Delay();


			IO.Out8(IO.Port.RTC_CommandPort, SlaveICW1);
			IO.Delay();

			IO.Out8(IO.Port.RTC_DataPort, SlaveIRQBase);
			IO.Delay();

			IO.Out8(IO.Port.RTC_DataPort, SlaveICW3);
			IO.Delay();

			IO.Out8(IO.Port.RTC_DataPort, SlaveICW4);
			IO.Delay();


			DisableAllIRQs();
		}

		public static void SendMasterEndOfInterrupt()
		{
			IO.Out8(IO.Port.PIC_CommandPort, EndOfInterrupt);
		}

		public static void SendSlaveEndOfInterrupt()
		{
			IO.Out8(IO.Port.RTC_CommandPort, EndOfInterrupt);
		}

		public static void DisableAllIRQs()
		{
			MasterIRQMask = (byte)0xFF;

			IO.Out8(IO.Port.PIC_DataPort, MasterIRQMask);
			IO.Delay();

			SlaveIRQMask = (byte)0xFF;

			IO.Out8(IO.Port.RTC_DataPort, SlaveIRQMask);
			IO.Delay();
		}

		public static void EnableMasterIRQ(byte value)
		{
			value &= 7;

			if (value == 2)
				return;

			MasterIRQMask &= (byte)~(1 << value);

			IO.Out8(IO.Port.PIC_DataPort, MasterIRQMask);
		}

		public static void EnableSlaveIRQ(byte value)
		{
			value &= 7;

			SlaveIRQMask &= (byte)~(1 << value);

			IO.Out8(IO.Port.RTC_DataPort, SlaveIRQMask);
		}

		public static void EnableIRQ(byte value)
		{
			if (value < 8)
				EnableMasterIRQ(value);

			else if (value < 16)
				EnableSlaveIRQ((byte)(value - 8));
		}

		public static void DisableMasterIRQ(byte value)
		{
			value &= 7;

			if (value == 2)
				return;

			MasterIRQMask |= (byte)(1 << value);

			IO.Out8(IO.Port.PIC_DataPort, MasterIRQMask);
		}

		public static void DisableSlaveIRQ(byte value)
		{
			value &= 7;

			SlaveIRQMask |= (byte)(1 << value);

			IO.Out8(IO.Port.RTC_DataPort, SlaveIRQMask);
		}

		public static void DisableIRQ(byte value)
		{
			if (value < 8)
				DisableMasterIRQ(value);

			else if (value < 16)
				DisableSlaveIRQ((byte)(value - 8));
		}
	}
}

