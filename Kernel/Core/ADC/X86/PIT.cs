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
	/// Periodic Interrupt Timer (PIT)
	/// </summary>
	public unsafe class PIT {
		public const string TIMER_HANDLER = "TIMER_HANDLER";

		private const byte SquareWave = 0x36;
		private const uint PITFrequency = 1193182;
		public static ushort HZ = 100;

		private static uint ticks = 0;
		unsafe static uint* timerEvent =
			(uint*)Stubs.StaticAlloc(sizeof(uint) * EntryModule.MaxEventHandlers);

		/*
		// sigh.. one can only dream
		public delegate void somefunction(uint value);
		unsafe static somefunction[] timerEvents = (somefunction[])Stubs.StaticAlloc<somefunction>(Kernel.MaxEventHandlers);
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
		public static void Setup ()
		{
			SetTimerFrequency (HZ);

			IDT.RegisterIRQ (IDT.Interrupt.SystemTimer, Stubs.GetFunctionPointer (TIMER_HANDLER));
		}
		#endregion

		#region SetTimerFrequency
		public static void SetTimerFrequency (ushort Hz)
		{
			ushort TimerCount = (ushort) (PITFrequency / Hz);
			IO.WriteByte (IO.Port.PIT_mode_control_port, SquareWave);

			IO.WriteByte (IO.Port.PIT_counter_0_counter_divisor, (byte) (TimerCount & 0xFF));
			IO.WriteByte (IO.Port.PIT_counter_0_counter_divisor, (byte) ((TimerCount >> 8) & 0xFF));
		}
		#endregion

		#region TimerEvent
		public static EventRegisterStatus RegisterTimerEvent (uint address)
		{
			for (int x = 0; x < EntryModule.MaxEventHandlers; ++x) {
				if (timerEvent[x] == address)
					return EventRegisterStatus.AlreadySubscribed;
			}

			for (int x = 0; x < EntryModule.MaxEventHandlers; ++x)
			{
				if (timerEvent[x] == 0) {
					timerEvent[x] = address;
					return EventRegisterStatus.Success;
				}
			}

			return EventRegisterStatus.CapacityExceeded;
		}

		public static EventRegisterStatus UnregisterTimerEvent (uint address)
		{
			for (int x = 0; x < EntryModule.MaxEventHandlers; ++x) {
				if (timerEvent[x] == address) {
					timerEvent[x] = 0;
					return EventRegisterStatus.Success;
				}
			}

			return EventRegisterStatus.NotSubscribed;
		}

		#endregion

		#region TimerHandler
		[SharpOS.AOT.Attributes.Label (TIMER_HANDLER)]
		private static unsafe void TimerHandler (IDT.ISRData data)
		{
			ticks++;

			for (int x = 0; x < EntryModule.MaxEventHandlers; ++x) {
				if (timerEvent[x] == 0)
					continue;

				MemoryUtil.Call(timerEvent[x], ticks);
			}

	
			SharpOS.Kernel.ADC.Thread thread = Dispatcher.Dispatch((void*) data.Stack);
			if (thread != null)
			{
				IDT.Stack*	newStack = (IDT.Stack*) thread.StackPointer;
				newStack->IrqIndex = data.Stack->IrqIndex;
				data.Stack		= newStack;
			}
		}
		#endregion

		public static uint GetTickCount()
		{
			return ticks;
		}


	}
}

