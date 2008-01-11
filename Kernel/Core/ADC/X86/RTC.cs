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

			do {
				// wait while update is in progress
				while ((CMOS.Read(CMOS.Address.StatusRegisterA) & 0x80) != 0)
					;

				seconds		= CMOS.Read(CMOS.Address.Seconds);			// Get seconds (00 to 59)
				minutes		= CMOS.Read (CMOS.Address.Minutes);			// Get minutes (00 to 59)
				hour		= CMOS.Read (CMOS.Address.Hour);			// Get hours
				day			= CMOS.Read (CMOS.Address.DayOfMonth);		// Get day of month (01 to 31)
				month		= CMOS.Read (CMOS.Address.Month);			// Get month (01 to 12)
				year		= CMOS.Read (CMOS.Address.Year);			// Get year (00 to 99)
			} while (seconds != CMOS.Read (CMOS.Address.Seconds));

			if ((CMOS.Read (CMOS.Address.StatusRegisterB) & (1<<2)) == 0) {
				seconds = ConvertBCDToValue(seconds);
				minutes = ConvertBCDToValue(minutes);
				hour	= ConvertBCDToValue(hour);
				day		= ConvertBCDToValue(day);
				month	= ConvertBCDToValue(month);
				year	= ConvertBCDToValue(year);
			}

			year += 2000;

			long ticks = ToTicks (year, month, day, hour, minutes, seconds, 0);
		}
		#endregion

		#region BCD conversion functions
		private static int ConvertBCDToValue(int value)    
		{
			return (((value) & 0x0f) + ((value)>>4)*10);
		}

		private static int ConvertValueToBCD(int value)    
		{
			return ((((value) / 10) << 4) + (value) % 10);
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

