//
// (C) 2006-2008 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	William Lahti <xfurious@gmail.com>
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
using SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.ADC {
	/// <summary>
	/// Real Time Clock
	/// </summary>
	public unsafe class RTC {

		/// <summary>
		/// Reads the time from CMOS
		/// </summary>
		[ADCStub]
		public static bool Read (out int year, out int month, out int day, out int hour,
			out int minutes, out int seconds)
		{
			year = month = day = hour = minutes = seconds = 0;
			return false;
		}
	}
}
