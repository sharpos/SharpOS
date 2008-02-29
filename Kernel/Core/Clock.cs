//
// (C) 2008 The SharpOS Project Team (http://www.sharpos.org)
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
using SharpOS.AOT.Attributes;
using SharpOS.Kernel.Foundation;
using SharpOS.Korlib.Runtime;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.Foundation {
	public unsafe class Clock {
		private const string CLOCK_HANDLER = "CLOCK_HANDLER";
		static ulong bootTime = 0;
		static Time currentTime = null;
		//static int secondChunks = 0;
		static ulong nanoSeconds = 0;
		static int sinceHardwareSync = 0;
		static CalendarType Calendar = CalendarType.Gregorian;
		static bool hardwareIsUTC = true;

		/// <summary>
		/// Tracks how much of the current second has passed so far.
		/// This is a count of timer fires, so when it becomes
		/// equal to Timer.GetFrequency (), a second has passed.
		/// </summary>
		static int secondChunk = 0;
		static SByte systemTimezone = 0;
		static Time internalTime;

		public static bool HardwareIsUTC {
			get {
				return hardwareIsUTC;
			} set {
				hardwareIsUTC = value;
			}
		}

		public static SByte Timezone {
			get {
				return systemTimezone;
			} set {
				ulong time = currentTime.Ticks;

				if (systemTimezone != 0)
					time = SharpOS.Kernel.Foundation.Timezone.GetUTC (time);

				if (value != 0)
					time = SharpOS.Kernel.Foundation.Timezone.Localize (time, value);

				currentTime.Set (time);
				currentTime.CurrentTimezone = value;
				systemTimezone = value;
			}
		}

		/// <summary>
		/// The interval of time (in 100-nanosecond ticks) between synchronizations from the
		/// hardware clock to the software clock. (default 600000000 ticks is 1 minute).
		/// </summary>
		public static int HardwareSyncTicks = 60;

		/// <summary>
		/// Retrieves the current hardware time. If this is not possible,
		/// this method returns false.
		/// </summary>
		public static bool GetHardwareTime (Time time)
		{
			bool result = false;
			ulong ct = 0;

			if (time == null)
				throw new ArgumentNullException ("time");

			time.Calendar = CalendarManager.GetCalendar (Calendar);

			result = RTC.Read (out time.Year, out time.Month, out time.Day, out time.Hour,
					out time.Minute, out time.Second);
			time.DayOfWeek = time.Calendar.GetDayOfWeek (time);
			time.Calendar.AddStrings (time);

			ct = time.Ticks;

			if (HardwareIsUTC && Timezone != 0) {
				ct = SharpOS.Kernel.Foundation.Timezone.Localize (ct, Timezone);
				time.Set (ct);
			}

			return result;
		}

		/// <summary>
		/// Retrieves the current hardware time as a timestamp. If this is not possible,
		/// this method returns 0.
		/// </summary>
		public static unsafe ulong GetHardwareTimestamp ()
		{
			Time time = new Time ();

			try {
				return (GetHardwareTime (time) ? time.Ticks : 0);
			} finally {
				Runtime.Free (time);
			}
		}

		/// <summary>
		/// Initializes the system clock. First the hardware time is read
		/// and stored as the boot time and current time. The times are
		/// stored as 64-bit unsigned integers with 100-nanosecond ticks
		/// since the epoch (January 1, 0001, 00:00:00). A handler is added
		/// to the system timer event which updates the current time and
		/// handles periodic synchronization with the hardware time. The
		/// amount added to the current time per timer fire is relevant
		/// to the frequency reportedly used by the system timer.
		/// </summary>
		public static unsafe void Setup ()
		{
			Time time = null;
			EventRegisterStatus st = EventRegisterStatus.Success;

			// Read the hardware time

			currentTime = new Time ();
			GetHardwareTime (currentTime);
			bootTime = GetCurrentTimestamp ();

			// Install the clock handler

			st = Timer.RegisterTimerEvent (Stubs.GetFunctionPointer (CLOCK_HANDLER));

			if (st != EventRegisterStatus.Success)
				Diagnostics.Error ("Failed to register clock handler");

			// Debug

			time = currentTime;

			TextMode.Write ("Current time: ");
			TextMode.Write (time.Year);
			TextMode.Write ("/");
			TextMode.Write (time.Month);
			TextMode.Write ("/");
			TextMode.Write (time.Day);
			TextMode.Write (" ");
			TextMode.Write (time.Hour);
			TextMode.Write (":");
			TextMode.Write (time.Minute);
			TextMode.Write (":");
			TextMode.Write (time.Second);
			TextMode.WriteLine ();

			internalTime = new Time ();
		}

		/// <summary>
		/// The clock maintenance handler, which is run per system timer fire.
		/// </summary>
		[Label (CLOCK_HANDLER)]
		static unsafe void UpdateClock (uint ticks)
		{
			if (secondChunk == Timer.GetFrequency ()) {
				currentTime.AddSeconds (1);
				secondChunk = 0;
				sinceHardwareSync += 1;

				// Synchronize from the hardware clock

				if (sinceHardwareSync >= HardwareSyncTicks) {
					GetHardwareTime (currentTime);
					sinceHardwareSync = 0;
				}
			}

			secondChunk += 1;

			// FIXME:
			//	- There seems to be a problem with adding 64-bit numbers, still,
			//	  perhaps related to the storage of the number in a static spot
			//	  (off the stack).
			//	- 64-bit division is not supported yet, when it is, use this first
			//	  variation which takes into account the speed of the timer
			//
			//nanoSeconds += 1000000000 / (ulong)Timer.GetFrequency ();
			nanoSeconds = nanoSeconds + 10000000L;
		}

		public unsafe static Time GetCurrentTime ()
		{
			Time time = new Time ();
			GetCurrentTime (time);
			return time;
		}

		public unsafe static void Write ()
		{
			if (internalTime == null)
				return;

			GetCurrentTime(internalTime);
			internalTime.Write();
			//Time time = null;
			//time = Clock.GetCurrentTime ();
			//time.Write ();
			//Runtime.Free (time);
		}

		/// <summary>
		/// Retrieves the current system clock  time.
		/// </summary>
		public unsafe static void GetCurrentTime (Time time)
		{
			if (currentTime == null)
				throw new Exception ("Clock has not been setup yet");
			if (time == null)
				throw new ArgumentNullException ("time");

			time.Set (currentTime);
		}

		public unsafe static ulong GetCurrentTimestamp ()
		{
			Time time;

			if (currentTime == null)
				throw new Exception ("Clock has not been setup yet");

			time = new Time ();
			GetCurrentTime (time);

			try {
				return time.Ticks;
			} finally {
				Runtime.Free (time);
			}
		}

		/// <summary>
		/// Retrieves the system boot time.
		/// </summary>
		public unsafe static void GetBootTime (Time time)
		{
			if (currentTime == null)
				throw new Exception ("Clock has not been setup yet");
			if (time == null)
				throw new ArgumentNullException ("time");

			time.Set (bootTime);
		}


		/// <summary>
		/// Resets the nanostamp counter.
		/// </summary>
		public static void ResetNanostamp ()
		{
			nanoSeconds = 0;
		}

		/// <summary>
		/// Returns the amount of nanoseconds since the
		/// last ResetNanostamp() call. This value is limited
		/// in precision to the frequency of the system timer.
		/// </summary>
		public static ulong GetNanostamp ()
		{
			return nanoSeconds;
		}
	}
}

