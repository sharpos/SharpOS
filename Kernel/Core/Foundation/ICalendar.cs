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

	public unsafe interface ICalendar {
		/// <summary>
		/// Returns the string representing the given day of the week
		/// </summary>
		string GetDayOfWeekString (uint dayOfWeek);

		/// <summary>
		/// Gets the string representing the given month.
		/// </summary>
		string GetMonthString (uint month);

		/// <summary>
		/// Adds the correct day of week and month strings to the Time instance.
		/// </summary>
		void AddStrings (Time time);

		/// <summary>
		/// Adds the given amount of seconds to the Time instance.
		/// </summary>
		void AddSeconds (Time time, int seconds);

		/// <summary>
		/// Determines whether the year is a leap year
		/// </summary>
		bool IsLeapYear (uint year);

		/// <summary>
		/// Gets the amount of days in the given year.
		/// </summary>
		uint GetYearDays (uint year);

		/// <summary>
		/// Creates a 100-nanosecond granularity timestamp from the epoch (January 1st, 0001).
		/// </summary>
		ulong EncodeTimestamp (Time time);

		/// <summary>
		/// Gets the day of the week for the given time.
		/// </summary>
		int GetDayOfWeek (Time time);

		/// <summary>
		/// Decodes the timestamp into the given Time instance.
		/// </summary>
		void DecodeTimestamp (ulong timestamp, Time out_time);
	}
}
