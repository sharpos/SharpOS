// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Sander van Rossen <sander.vanrossen@gmail.com>
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
	// Periodic Interrupt Timer (PIT)
	public unsafe class PIT
	{
		public const string		TIMER_HANDLER			= "TIMER_HANDLER";

		private const byte		SquareWave			= 0x36;
		private const uint		PITFrequency			= 1193182;
		private const ushort		TimerCount			= (ushort)(PITFrequency / HZ);
		public const ushort		HZ				= 100;

		private static uint ticks = 0;

		public static void Setup()
		{
			IO.Out8(IO.Ports.PIT_mode_control_port, SquareWave);
			
			IO.Out8(IO.Ports.PIT_counter_0_counter_divisor, (TimerCount & 0xFF));
			IO.Out8(IO.Ports.PIT_counter_0_counter_divisor, ((TimerCount >> 8) & 0xFF));

			IDT.SetupIRQ(0, Kernel.GetFunctionPointer(TIMER_HANDLER));
		}

		#region TimerEvent
		public static uint timerEvent = 0;
		
		public static EventRegisterStatus RegisterTimerEvent (uint address)
		{
			if (timerEvent != 0)
				return EventRegisterStatus.CapacityExceeded;
			if (timerEvent == address)
				return EventRegisterStatus.AlreadySubscribed;
			
			timerEvent = address;
			
			return EventRegisterStatus.Success;
		}
		#endregion

		[SharpOS.AOT.Attributes.Label(TIMER_HANDLER)]
		private static unsafe void TimerHandler(IDT.ISRData data)
		{
			ticks++;
			
			if (timerEvent != 0)
				Memory.Call(timerEvent, ticks);
		}
	}
}

