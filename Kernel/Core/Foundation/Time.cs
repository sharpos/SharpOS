//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
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
using SharpOS.Kernel.ADC;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using SharpOS.Kernel.Foundation;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.Foundation {

	/// <summary>
	/// A universal calendar entry. This can describe a date in any
	/// supported calendar type.
	/// </summary>
	public class Time {
		public Time (ulong timestamp):
			this (timestamp, CalendarManager.GetCalendar (CalendarType.Gregorian))
		{
		}

		public Time (ulong timestamp, ICalendar calendar)
		{
			if (calendar == null)
				throw new ArgumentNullException ("calendar");

			this.Calendar = calendar;
			this.Calendar.DecodeTimestamp (timestamp, this);
		}

		public Time ():
			this (0)
		{
		}

		public ICalendar Calendar = null;
		public int Year = 0;
		public int Month = 0;
		public string MonthString = null;
		public int Day = 0;
		public int Hour = 0;
		public int Minute = 0;
		public int Second = 0;
		public int Millisecond = 0;
		public int Nanosecond = 0;
		public int DayOfWeek = 0;
		public string DayOfWeekString = null;
		public SByte CurrentTimezone = 0;
		public string TimezoneString = null;

		/// <summary>
		/// Calculates the UTC timestamp of this Time structure, using the appropriate Calendar
		/// implementation.
		/// </summary>
		public ulong Ticks {
			get {
				if (this.Calendar == null)
					throw new Exception ("Calendar is null");

				return Timezone.GetUTC (Calendar.EncodeTimestamp (this));
			}
		}

		public ulong LocalTimestamp {
			get {
				Diagnostics.Assert (this.Calendar != null,
					"Time.LocalTimestamp: Calendar is null");

				return Calendar.EncodeTimestamp (this);
			}
		}

		public unsafe Time Clone ()
		{
			Time newTime = new Time ();

			newTime.Year = this.Year;
			newTime.Month = this.Month;
			newTime.MonthString = this.MonthString;
			newTime.Day = this.Day;
			newTime.Hour = this.Hour;
			newTime.Minute = this.Minute;
			newTime.Second = this.Second;
			newTime.Millisecond = this.Millisecond;
			newTime.Nanosecond = this.Nanosecond;
			newTime.DayOfWeek = this.DayOfWeek;
			newTime.DayOfWeekString = this.DayOfWeekString;
			newTime.CurrentTimezone = this.CurrentTimezone;
			newTime.TimezoneString = this.TimezoneString;

			return newTime;
		}

		public unsafe void Write ()
		{
			if (DayOfWeekString != null) {
				TextMode.Write (DayOfWeekString);
				TextMode.Write (" ");
			}

			if (MonthString != null) {
				// January 1, 2008
				TextMode.Write (MonthString);
				TextMode.Write (" ");
				TextMode.Write (Day);
				TextMode.Write (", ");
				TextMode.Write (Year);
			} else {
				// 2008/1/1
				TextMode.Write (Year);
				TextMode.Write ("/");
				TextMode.Write (Month);
				TextMode.Write ("/");
				TextMode.Write (Day);
			}

			TextMode.Write (" ");
			TextMode.Write (Hour);
			TextMode.Write (":");

			if (Minute < 10)
				TextMode.Write ("0");

			TextMode.Write (Minute);

			TextMode.Write (":");

			if (Second < 10)
				TextMode.Write ("0");

			TextMode.Write (Second);
			TextMode.Write (".");
			TextMode.Write (Millisecond);

			if (CurrentTimezone != 0) {
				TextMode.Write (" ");

				if (TimezoneString != null)
					TextMode.Write (TimezoneString);
				else {
					TextMode.Write ("UTC");
					if (CurrentTimezone < 0)
						TextMode.Write ("+");
					TextMode.Write (CurrentTimezone);
				}
			} else {
				TextMode.Write (" UTC");
			}
		}

		public void AddSeconds (int seconds)
		{
			this.Calendar.AddSeconds (this, seconds);
		}

		public unsafe void Set (ulong timestamp)
		{
			Calendar.DecodeTimestamp (timestamp, this);
		}

		public unsafe void Set (Time other)
		{
			if (other == null)
				throw new ArgumentNullException ("other");

			this.Year = other.Year;
			this.Month = other.Month;
			this.MonthString = other.MonthString;
			this.Day = other.Day;
			this.DayOfWeekString = other.DayOfWeekString;
			this.DayOfWeek = other.DayOfWeek;
			this.Hour = other.Hour;
			this.Minute = other.Minute;
			this.Second = other.Second;
			this.Millisecond = other.Millisecond;
			this.Nanosecond = other.Nanosecond;
			this.CurrentTimezone = other.CurrentTimezone;
		}

		public unsafe void ToString (PString8 *str)
		{
			if (str == null)
				throw new ArgumentNullException ("str");

			if (DayOfWeekString != null) {
				str->Concat (DayOfWeekString);
				str->Concat (" ");
			}

			if (MonthString != null) {
				// January 1, 2008
				str->Concat (MonthString);
				str->Concat (" ");
				str->Concat (Day);
				str->Concat (", ");
				str->Concat (Year);
			} else {
				// 2008/1/1
				str->Concat (Year);
				str->Concat ("/");
				str->Concat (Month);
				str->Concat ("/");
				str->Concat (Day);
			}

			str->Concat (" ");
			str->Concat (Hour);
			str->Concat (":");

			if (Minute < 10)
				str->Concat ("0");

			str->Concat (Minute);

			str->Concat (":");

			if (Second < 10)
				str->Concat ("0");

			str->Concat (Second);
			str->Concat (".");
			str->Concat (Millisecond);

			if (CurrentTimezone != 0) {
				str->Concat (" ");

				if (TimezoneString != null)
					str->Concat (TimezoneString);
				else {
					str->Concat ("UTC");
					if (CurrentTimezone < 0)
						str->Concat ("+");
					str->Concat (CurrentTimezone);
				}
			} else {
				str->Concat (" UTC");
			}
		}

		public unsafe CString8 *GetTimeString ()
		{
			Foundation.StringBuilder* sb = Foundation.StringBuilder.CREATE ();

			sb->AppendNumber (this.Hour);
			sb->Append (":");
			sb->AppendNumber (this.Minute);
			sb->Append (":");
			sb->AppendNumber (this.Second);

			try {
				return sb->buffer;
			} finally {
				MemoryManager.Free (sb);
			}
		}
	}
}

