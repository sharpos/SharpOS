//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.ADC.X86 {

	public unsafe class Timer {
		public static EventRegisterStatus RegisterTimerEvent (uint func)
		{
			return PIT.RegisterTimerEvent (func);
		}

		public static EventRegisterStatus UnregisterTimerEvent (uint func)
		{
			return PIT.UnregisterTimerEvent (func);
		}

		public static ushort GetFrequency ()
		{
			return PIT.HZ;
		}

		public static void Delay(uint milliseconds)
		{
			// resolution is only 10 milliseconds
			uint end = PIT.GetTickCount() + (milliseconds / 10);

			while (PIT.GetTickCount() < end)
				;
		}
	}
}
