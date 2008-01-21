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
	public unsafe struct Time {
		public CalendarType Calendar;
		public int Year;
		public int Month;
		public byte *MonthString;
		public int Day;
		public int Hour;
		public int Minute;
		public int Second;
		public int Millisecond;
		public int Nanosecond;
		public int DayOfWeek;
		public byte *DayOfWeekString;
		public SByte CurrentTimezone;
		public byte *TimezoneString;

		/// <summary>
		/// Calculates the UTC timestamp of this Time structure, using the appropriate Calendar
		/// implementation.
		/// </summary>
		public ulong Ticks {
			get {
				switch (Calendar) {
					case CalendarType.Gregorian:
						fixed (Time *me = &this)
							return Timezone.GetUTC (
								GregorianCalendar.EncodeTimestamp (me));
					default:
						Diagnostics.Error ("Time::Ticks: Calendar not supported");
						break;
				}

				return 0;
			}
		}

		public ulong LocalTimestamp {
			get {
				switch (Calendar) {
					case CalendarType.Gregorian:
						fixed (Time *me = &this)
							return GregorianCalendar.EncodeTimestamp (me);
					default:
						Diagnostics.Error ("Time::Ticks: Calendar not supported");
						break;
				}

				return 0;
			}
		}

		public Time *Copy ()
		{
			return Copy (null);
		}

		/// <summary>
		/// Allocates a copy of the current Time structure using kernel memory management.
		/// </summary>
		public Time *Copy (Time *dest)
		{
			Time *time = dest;

			if (time == null)
				time = (Time*)MemoryManager.Allocate ((uint)sizeof (Time));

			time->Year = Year;
			time->Month = Month;
			time->Day = Day;
			time->Hour = Hour;
			time->Minute = Minute;
			time->Second = Second;
			time->Millisecond = Millisecond;
			time->Nanosecond = Nanosecond;
			time->MonthString = MonthString;
			time->DayOfWeekString = DayOfWeekString;
			time->CurrentTimezone = CurrentTimezone;
			time->Calendar = Calendar;

			return time;
		}

		/// <summary>
		/// Allocates and zeroes a Time structure using kernel memory management.
		/// </summary>
		public static Time *Allocate (Time *buf)
		{
			Time *time = buf;

			if (time == null)
				time = (Time*)MemoryManager.Allocate ((uint)sizeof (Time));

			time->Year = time->Month = time->Day = time->Hour = 1;
			time->Minute = time->Second = time->Millisecond = time->Nanosecond = 0;
			time->DayOfWeek = 1;
			time->Calendar = CalendarType.Gregorian;
			time->CurrentTimezone = Clock.Timezone;
			time->MonthString = null;
			time->DayOfWeekString = null;

			return time;
		}

		public static Time *Allocate ()
		{
			return Allocate (null);
		}

		/// <summary>
		/// Constructs a Time structure representing the given timestamp in the default (Gregorian)
		/// calendar.
		/// </summary>
		public static Time *Create (ulong timestamp)
		{
			return Create (timestamp, CalendarType.Gregorian);
		}

		/// <summary>
		/// Constructs a Time structure representing the given timestamp in the given
		/// calendar.
		/// </summary>
		public static Time *Create (ulong timestamp, CalendarType calendar)
		{
			Time *time = Allocate (null);

			timestamp = Timezone.Localize (timestamp);

			switch (calendar) {
				case CalendarType.Gregorian:
					GregorianCalendar.DecodeTimestamp (timestamp, time);
					break;
				default:
					Diagnostics.Error ("Time::Create(): Calendar not supported");
					MemoryManager.Free (time);
					time = null;
					break;
			}

			return time;
		}

		public void Write ()
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

		public unsafe void AddSeconds (int seconds)
		{
			fixed (Time *time = &this) {
				switch (Calendar) {
				case CalendarType.Gregorian:
					GregorianCalendar.AddSeconds (time, seconds);
					break;
				default:
					Diagnostics.Error ("Time.AddSeconds(): unsupported calendar");
					break;
				}
			}
		}

		public unsafe void Set (ulong timestamp)
		{
			fixed (Time *me = &this) {
				switch (Calendar) {
				case CalendarType.Gregorian:
					GregorianCalendar.DecodeTimestamp (timestamp, me);
					break;
				default:
					Diagnostics.Error ("Time.Set(): unsupported calendar");
					break;
				}
			}
		}

		public unsafe void Set (Time *other)
		{
			Year = other->Year;
			Month = other->Month;
			MonthString = other->MonthString;
			Day = other->Day;
			DayOfWeekString = other->DayOfWeekString;
			DayOfWeek = other->DayOfWeek;
			Hour = other->Hour;
			Minute = other->Minute;
			Second = other->Second;
			Millisecond = other->Millisecond;
			Nanosecond = other->Nanosecond;
			CurrentTimezone = other->CurrentTimezone;
		}

		public void ToString (PString8 *str)
		{
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

	}
}

