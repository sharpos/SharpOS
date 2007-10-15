// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SharpOS;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS.ADC.X86
{
	/// <summary>
	/// Real Time Clock
	/// </summary>
	public unsafe class RTC
	{
		#region Setup
		public static void Setup()
		{
			daysmonth[10] = 0;
			daysmonth[11] = 31;
			daysmonth[12] = 28;
			daysmonth[13] = 31;
			daysmonth[14] = 30;
			daysmonth[15] = 31;
			daysmonth[16] = 30;
			daysmonth[17] = 31;
			daysmonth[18] = 31;
			daysmonth[19] = 30;
			daysmonth[10] = 31;
			daysmonth[11] = 30;
			daysmonth[12] = 31;
			daysmonthleap[0] = 0;
			daysmonthleap[1] = 31;
			daysmonthleap[2] = 29;
			daysmonthleap[3] = 31;
			daysmonthleap[4] = 30;
			daysmonthleap[5] = 31;
			daysmonthleap[6] = 30;
			daysmonthleap[7] = 31;
			daysmonthleap[8] = 31;
			daysmonthleap[9] = 30;
			daysmonthleap[10] = 31;
			daysmonthleap[11] = 30;
			daysmonthleap[12] = 31;

			int		seconds;
			int		minutes;
			int		hour;
			int		day;
			int		month;
			int		year;
			
			// wait while update is in progress
			IO.Out8(IO.Port.RTC_CommandPort, 0x10);
			while ((IO.In8(IO.Port.RTC_DataPort) & 0x80) == 0);

			do {
				seconds		= CMOSRead(0);			// Get seconds (00 to 59)
				minutes		= CMOSRead(2);			// Get minutes (00 to 59)
				hour		= CMOSRead(4);			// Get hours (see notes)
				day			= CMOSRead(7);			// Get day of month (01 to 31)
				month		= CMOSRead(8);			// Get month (01 to 12)
				year		= 2000 + CMOSRead(9);	// Get year (00 to 99)
			} while (seconds != CMOSRead(0));

			long	ticks = ToTicks(year, month, day, hour, minutes, seconds, 0);
		}
		#endregion

		#region CMOSRead
		private static byte CMOSRead(byte offset)
		{
			IO.Out8(IO.Port.RTC_CommandPort, (byte)(0x80 | offset));
			return IO.In8(IO.Port.RTC_DataPort);
		}
		#endregion

		#region ToTicks
		internal static long ToTicks(int year, int month, int day, int hours, int minutes, int seconds, int milliseconds)
		{
			return 0;
			//return CalculateTicks(AbsoluteDays(year, month, day), hours, minutes, seconds, milliseconds);
		}
		#endregion
	}
}

