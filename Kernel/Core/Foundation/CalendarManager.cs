//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
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
using SharpOS.Kernel.ADC;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using SharpOS.Kernel.Foundation;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.Foundation {

	public unsafe class CalendarManager {
		static ICalendar Gregorian = null;

		public static ICalendar GetCalendar (CalendarType type)
		{
			// Initialize the calendars here

			if (Gregorian == null)
				Gregorian = new GregorianCalendar ();

			// List the calendars here

			switch (type) {
			case CalendarType.Gregorian:
				return Gregorian;
			default:
				return null;
			}
		}
	}
}
