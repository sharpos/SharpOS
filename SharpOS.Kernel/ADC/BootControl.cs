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

namespace SharpOS.ADC {
	public unsafe class BootControl {
		/// <summary>
		/// Halt the system.
		/// </summary>
		[AOTAttr.ADCStub]
		public static void Halt()
		{
		}
		
		/// <summary>
		/// Reboot the system.
		/// </summary>
		[AOTAttr.ADCStub]
		public static void Reboot()
		{
		}
	}
}
