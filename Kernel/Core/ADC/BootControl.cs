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
	public unsafe class BootControl {
		/// <summary>
		/// Powers down the system.
		/// </summary>
		[AOTAttr.ADCStub]
		public static void PowerOff ()
		{
		}

		/// <summary>
		/// Freezes the system. 
		/// Usually used after a crash to display information after which the user 
		/// can turn of the machine.
		/// </summary>
		[AOTAttr.ADCStub]
		public static void Freeze ()
		{
		}

		/// <summary>
		/// Puts the system into sleep mode.
		/// </summary>
		[AOTAttr.ADCStub]
		public static void Sleep ()
		{
		}

		/// <summary>
		/// Reboot the system.
		/// </summary>
		[AOTAttr.ADCStub]
		public static void Reboot ()
		{
		}
	}
}
