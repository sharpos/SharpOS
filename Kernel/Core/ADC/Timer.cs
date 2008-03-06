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

namespace SharpOS.Kernel.ADC {

	public unsafe class Timer {
		[AOTAttr.ADCStub]
		public static EventRegisterStatus RegisterTimerEvent (uint func)
		{
			return EventRegisterStatus.NotSupported;
		}

		public static EventRegisterStatus UnregisterTimerEvent (uint func)
		{
			return EventRegisterStatus.NotSupported;
		}

		[AOTAttr.ADCStub]
		public static ushort GetFrequency ()
		{
			return 0;
		}

		[AOTAttr.ADCStub]
		public static void Delay(uint milliseconds)
		{ 
		}
	}
}
