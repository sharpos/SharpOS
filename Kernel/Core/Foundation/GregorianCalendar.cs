//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//	Sebastian Öberg <sebastian.oberg@gmail.com>
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
	/// An implementation of the Gregorian calender that can translate between
	/// SharpOS timestamps and Gregorian dates, stored in the universal Time
	/// structure.
	/// </summary>
	public class GregorianCalendar {
		public static unsafe byte *GetDayOfWeekString (uint dayOfWeek)
		{
			switch (dayOfWeek) {
				case 1: return Stubs.CString ("Sunday");
				case 2: return Stubs.CString ("Monday");
				case 3: return Stubs.CString ("Tuesday");
				case 4: return Stubs.CString ("Wednesday");
				case 5: return Stubs.CString ("Thursday");
				case 6: return Stubs.CString ("Friday");
				case 7: return Stubs.CString ("Saturday");
			}

			return null;
		}

		public static unsafe byte *GetMonthString (uint month)
		{
			switch (month) {
				case 1: return Stubs.CString ("January");
				case 2: return Stubs.CString ("February");
				case 3: return Stubs.CString ("March");
				case 4: return Stubs.CString ("April");
				case 5: return Stubs.CString ("May");
				case 6: return Stubs.CString ("June");
				case 7: return Stubs.CString ("July");
				case 8: return Stubs.CString ("August");
				case 9: return Stubs.CString ("September");
				case 10: return Stubs.CString ("October");
				case 11: return Stubs.CString ("November");
				case 12: return Stubs.CString ("December");
			}

			return null;
		}

		public unsafe static void AddSeconds (Time *time, int seconds)
		{
			time->Second += seconds;

			if (time->Second > 59) {
				time->Minute += time->Second / 60;
				time->Second = time->Second % 60;
			}

			if (time->Minute > 59) {
				time->Hour += time->Minute / 60;
				time->Minute = time->Minute % 60;
			}

			if (time->Hour > 23) {
				time->Day += time->Hour / 24;
				time->Hour = time->Hour % 24;
			}

			if (time->Day >= GetMonthDays ((uint)time->Month, (uint)time->Year)) {
				int days = time->Day - (int)GetMonthDays ((uint)time->Month, (uint)time->Year);

				while (days >= GetMonthDays ((uint)time->Month, (uint)time->Year)) {
					time->Month++;
					days -= (int)GetMonthDays ((uint)time->Month, (uint)time->Year);
				}
			}

			if (time->Month > 12) {
				time->Year += time->Month / 12;
				time->Month = time->Month % 12;
			}

			time->MonthString = GetMonthString ((uint)time->Month);
			time->DayOfWeek = GetDayOfWeek (time);
			time->DayOfWeekString = GetDayOfWeekString ((uint)time->DayOfWeek);
		}

		static unsafe uint GetMonthDays (uint month)
		{
			return GetMonthDays (month, 0);
		}

		static unsafe uint GetMonthDays (uint month, uint year)
		{
			switch (month) {
				case 1: return 31;
				case 2:
					if (year > 0 && IsLeapYear (year))
						return 29;
					else
						return 28;
				case 3: return 31;
				case 4: return 30;
				case 5: return 31;
				case 6: return 30;
				case 7: return 31;
				case 8: return 31;
				case 9: return 30;
				case 10: return 31;
				case 11: return 30;
				case 12: return 31;
			}

			return 0;
		}

		public static bool IsLeapYear (uint year)
		{
			if (year % 400 == 0)
				return true;
			else if (year % 100 != 0 && year % 4 == 0)
				return true;

			return false;
		}

		public unsafe static uint GetYearDays (uint year)
		{
			if (IsLeapYear (year))
				return 366;
			else
				return 365;
		}

		// Mon Jan  7 04:01:28 EST 2008

		public unsafe static ulong EncodeTimestamp (Time *time)
		{
			ulong timestamp = 0;

			// encode years

			for (uint x = 1; x < time->Year; ++x) {
				timestamp += GetYearDays (x) * Timestamp.DayUnit;
			}

			// encode months

			for (uint x = 1; x < time->Month; ++x)
				timestamp += GetMonthDays (x, (uint)time->Year) * Timestamp.DayUnit;

			// encode days

			timestamp += (ulong) time->Day * Timestamp.DayUnit;
			timestamp += (ulong) time->Hour * Timestamp.HourUnit;
			timestamp += (ulong) time->Minute * Timestamp.MinuteUnit;
			timestamp += (ulong) time->Second * Timestamp.SecondUnit;
			timestamp += (ulong) time->Millisecond * Timestamp.MillisecondUnit;
			timestamp += (ulong) time->Nanosecond;

			return timestamp;
		}

		public static bool debug = false;

		public unsafe static int GetDayOfWeek(Time* time)
		{
			int dayOfWeek = time->Year + (time->Year / 4);
			dayOfWeek -= time->Year / 100;
			dayOfWeek += time->Year / 400;
			dayOfWeek += time->Day;
			dayOfWeek += GetMonthPseudoNumber((uint)time->Month, (uint)time->Year);
			dayOfWeek = dayOfWeek % 7;

			return dayOfWeek;
		}

		public unsafe static int GetMonthPseudoNumber(uint month, uint year)
		{
			switch (month) {
				case 1: if (IsLeapYear(year))
						return 6;
					else
						return 0;
				case 2: if (IsLeapYear(year))
						return 2;
					else
						return 3;
				case 3: return 3;
				case 4: return 6;
				case 5: return 1;
				case 6: return 4;
				case 7: return 6;
				case 8: return 2;
				case 9: return 5;
				case 10: return 0;
				case 11: return 3;
				case 12: return 5;
			}

			return 0;
		}

		public unsafe static void DecodeTimestamp (ulong timestamp, Time *out_time)
		{
			ulong remaining = timestamp;

			Diagnostics.Assert (out_time != null,
				"Timestamp::Decode(): parameter `time' is null");

			// init

			out_time->Year = (int) Timestamp.EpochYear;
			out_time->Day = out_time->Month = 1;
			out_time->Hour = out_time->Minute =
				out_time->Second = out_time->Millisecond = out_time->Nanosecond =
				out_time->DayOfWeek = 0;
			out_time->CurrentTimezone = 0;

			while (remaining > 0) {
				uint yearDays = GetYearDays ((uint)out_time->Year);
				uint monthDays = GetMonthDays ((uint)out_time->Month);

				// if this is a leap year and we are in February, add a day

				if (IsLeapYear ((uint)out_time->Year) && out_time->Month == 1)
					monthDays += 1;

				// take a chunk out of `remaining'

				if (remaining > (yearDays * Timestamp.DayUnit)) {
					// add a year
					if (debug)
						TextMode.WriteLine ("adding a year");
					out_time->Year++;
					remaining -= (yearDays * Timestamp.DayUnit);
					continue;
				} else if (remaining > (monthDays * Timestamp.DayUnit)) {
					// add a month

					out_time->Month++;
					remaining -= (monthDays * Timestamp.DayUnit);
				} else if (remaining >= Timestamp.DayUnit) {
					out_time->Day++;
					remaining -= Timestamp.DayUnit;
				} else if (remaining >= Timestamp.HourUnit) {
					out_time->Hour++;
					remaining -= Timestamp.HourUnit;
				} else if (remaining >= Timestamp.MinuteUnit) {
					out_time->Minute++;
					remaining -= Timestamp.MinuteUnit;
				} else if (remaining >= Timestamp.SecondUnit) {
					out_time->Second++;
					remaining -= Timestamp.SecondUnit;
				} else if (remaining >= Timestamp.MillisecondUnit) {
					out_time->Millisecond++;
					remaining -= Timestamp.MillisecondUnit;
				} else {
					out_time->Nanosecond = (int) remaining;
					remaining = 0;
				}
			}

			out_time->MonthString = GetMonthString ((uint)out_time->Month);

			// FIXME:
			//	The simplest implementation of this requires floating point
			//	types to be supported by the AOT.

			out_time->DayOfWeek = GetDayOfWeek (out_time);
			out_time->DayOfWeekString = GetDayOfWeekString ((uint)out_time->DayOfWeek);
		}
	}
}
