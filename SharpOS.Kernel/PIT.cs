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
	public unsafe class PIT {
		public const string TIMER_HANDLER = "TIMER_HANDLER";

		private const byte TimerModeControlPort = 0x43;
		private const byte TimerChannel0Port = 0x40;
		private const byte TimerChannel2Port = 0x42;
		private const byte SquareWave = 0x36;
		private const uint PITFrequency = 1193182; 
		private const ushort TimerCount = (ushort) (PITFrequency / HZ);
		private const ushort HZ = 100;

		private static uint ticks = 0;

		public static void Setup ()
		{
			IO.Out8 (TimerModeControlPort, SquareWave);
			IO.Out8 (TimerChannel0Port, (TimerCount & 0xFF));
			IO.Out8 (TimerChannel0Port, ((TimerCount >> 8) & 0xFF));

			IDT.SetupIRQ (0, Kernel.GetFunctionPointer (TIMER_HANDLER));
		}

		[SharpOS.AOT.Attributes.Label (TIMER_HANDLER)]
		private static unsafe void TimerHandler (IDT.ISRData data)
		{
			ticks++;

			if (ticks % HZ == 0) {
				Screen.GoTo (50, 0);
				Screen.SetAttributes (Screen.ColorTypes.Yellow, Screen.ColorTypes.Red);
				Screen.WriteLine (Kernel.String ("Timer ticks: "), (int) ticks);
			}
		}
	}
}

