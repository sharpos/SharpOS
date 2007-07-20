// created on 7/17/2007 at 4:16 PM
// created on 7/17/2007 at 4:10 PM
//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using SharpOS.AOT;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.ADC.X86 {

	public unsafe class Timer {
		public static EventRegisterStatus RegisterTimerEvent (uint func)
		{
			return PIT.RegisterTimerEvent (func);
		}
		
		public static ushort GetFrequency ()
		{
			return PIT.HZ;
		}
	}
}
