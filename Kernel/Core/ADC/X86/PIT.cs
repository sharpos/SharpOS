// 
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
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
using SharpOS;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS.ADC.X86
{
	/// <summary>
	/// Periodic Interrupt Timer (PIT)
	/// </summary>
	public unsafe class PIT
	{
		public const string 	TIMER_HANDLER		= "TIMER_HANDLER";

		private const byte  	SquareWave   		= 0x36;
		private const uint  	PITFrequency 		= 1193182;
		public static ushort	HZ           		= 100;

		private static uint 	ticks        		= 0;
		public static uint		timerEvent			= 0;

		/*
		// sigh.. one can only dream
		public delegate void somefunction(uint value);
		unsafe static somefunction[] timerEvents = (somefunction[])Kernel.StaticAlloc<somefunction>(Kernel.MaxEventHandlers);		
		public event somefunction function
		{
			add 
			{
				for (int x = 0; x < Kernel.MaxEventHandlers; ++x)
					if (timerEvents[x] == value)
						return;
			
				for (int x = 0; x < Kernel.MaxEventHandlers; ++x)
					if (timerEvents[x] == null)
						timerEvents[x] = value;
			}
			remove
			{
				for (int x = 0; x < Kernel.MaxEventHandlers; ++x)
					if (timerEvents[x] == value)
						timerEvents[x] = null;
			}
		}
		*/

		#region Setup
		public static void Setup()
		{
			SetTimerFrequency(HZ);

			IDT.RegisterIRQ(IDT.Interrupt.SystemTimer, Kernel.GetFunctionPointer(TIMER_HANDLER));
		}
		#endregion

		#region SetTimerFrequency
		public static void SetTimerFrequency(ushort Hz)
		{
			ushort TimerCount = (ushort)(PITFrequency / Hz);
			IO.Out8(IO.Port.PIT_mode_control_port, SquareWave);

			IO.Out8(IO.Port.PIT_counter_0_counter_divisor, (byte)(TimerCount & 0xFF));
			IO.Out8(IO.Port.PIT_counter_0_counter_divisor, (byte)((TimerCount >> 8) & 0xFF));
		}
		#endregion

		#region TimerEvent
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

		#region TimerHandler
		[SharpOS.AOT.Attributes.Label(TIMER_HANDLER)]
		private static unsafe void TimerHandler(IDT.ISRData data)
		{
			ticks++;			
			if (timerEvent != 0)
				Memory.Call(timerEvent, ticks);

			// run scheduler here..
			data = *((IDT.ISRData*)ADC.Scheduler.GetNextThread((void*)&data));
		}
		#endregion
	}
}

