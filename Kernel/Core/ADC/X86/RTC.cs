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
using SharpOS.Kernel.Foundation;

namespace SharpOS.Kernel.ADC.X86 {
	/// <summary>
	/// Real Time Clock
	/// </summary>
	public unsafe class RTC {

		/// <summary>
		/// Reads the time from CMOS
		/// </summary>
		public static bool Read (out int year, out int month, out int day, out int hour,
			out int minutes, out int seconds)
		{
			// wait while update is in progress
			while ((CMOS.Read(CMOS.Address.StatusRegisterA) & 0x80) != 0)
				;

			do {
				seconds = CMOS.Read (CMOS.Address.Seconds);	// Get seconds (00 to 59)
				minutes = CMOS.Read (CMOS.Address.Minutes);	// Get minutes (00 to 59)
				hour = CMOS.Read (CMOS.Address.Hour);		// Get hours
				day = CMOS.Read (CMOS.Address.DayOfMonth);	// Get day of month (01 to 31)
				month = CMOS.Read (CMOS.Address.Month);		// Get month (01 to 12)
				year = CMOS.Read (CMOS.Address.Year);	// Get year (00 to 99)
			} while (seconds != CMOS.Read (CMOS.Address.Seconds));

			if ((CMOS.Read (CMOS.Address.StatusRegisterB) & (1<<2)) == 0) {
				seconds = ConvertBCDToValue(seconds);
				minutes = ConvertBCDToValue(minutes);
				hour	= ConvertBCDToValue(hour);
				day	= ConvertBCDToValue(day);
				month	= ConvertBCDToValue(month);
				year	= ConvertBCDToValue(year);
			}

			year += 2000;

			return true;
		}

		#region BCD conversion functions

		private static int ConvertBCDToValue (int value)
		{
			return (((value) & 0x0f) + ((value)>>4)*10);
		}

		private static int ConvertValueToBCD (int value)
		{
			return ((((value) / 10) << 4) + (value) % 10);
		}

		#endregion
	}
}


