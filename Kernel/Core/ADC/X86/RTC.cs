// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
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
	/// Real Time Clock
	/// </summary>
	public unsafe class RTC {
		#region Setup
		public static void Setup ()
		{
			int seconds;
			int minutes;
			int hour;
			int day;
			int month;
			int year;

			// wait while update is in progress
			//IO.Out8(IO.Port.RTC_CommandPort, 0x10);
			//while ((IO.In8(IO.Port.RTC_DataPort) & 0x80) == 0);

			do {
				seconds		= CMOS.Read (CMOS.Address.Seconds);			// Get seconds (00 to 59)
				minutes		= CMOS.Read (CMOS.Address.Minutes);			// Get minutes (00 to 59)
				hour		= CMOS.Read (CMOS.Address.Hour);				// Get hours
				day			= CMOS.Read (CMOS.Address.Month);			// Get day of month (01 to 31)
				month		= CMOS.Read (CMOS.Address.DayOfMonth);		// Get month (01 to 12)
				year		= 2000 + CMOS.Read (CMOS.Address.Year);		// Get year (00 to 99)
			} while (seconds != CMOS.Read (CMOS.Address.Seconds));

			long ticks = ToTicks (year, month, day, hour, minutes, seconds, 0);
		}
		#endregion

		#region ToTicks
		internal static long ToTicks (int year, int month, int day, int hours, int minutes, int seconds, int milliseconds)
		{
			return 0;
			//return CalculateTicks(AbsoluteDays(year, month, day), hours, minutes, seconds, milliseconds);
		}
		#endregion
	}
}

