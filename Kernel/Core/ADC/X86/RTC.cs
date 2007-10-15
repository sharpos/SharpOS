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
			int		seconds;
			int		minutes;
			int		hour;
			int		day;
			int		month;
			int		year;
			
			// wait while update is in progress
			//IO.Out8(IO.Port.RTC_CommandPort, 0x10);
			//while ((IO.In8(IO.Port.RTC_DataPort) & 0x80) == 0);

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

