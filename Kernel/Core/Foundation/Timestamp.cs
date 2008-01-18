//
// (C) 2006-2008 The SharpOS Project Team (http://www.sharpos.org)
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

	/// <summary>
	/// A structure representing an unsigned 64-bit timestamp counted in 100-nanosecond blocks,
	/// with an Epoch of January 1st, 0001.
	/// </summary>
	public struct Timestamp {
		public ulong Ticks;

		/// <summary>
		/// The maximum timestamp value.
		/// DOCFIXME: and what date is that?
		/// </summary>
		public const ulong MaxValue = 0xFFFFFFFFFFFFFFFFU;

		/// <summary>
		/// The minimum timestamp value, representing 00:00:00, January 1st, 0001.
		/// </summary>
		public const ulong MinValue = 0x0;

		/// <summary>
		/// This year is 1 AD (anno domini)
		/// </summary>
		public const uint EpochYear = 1;

		/// <summary>
		/// How many 100-nanosecond blocks in a millisecond.
		/// </summary>
		public const uint MillisecondUnit = 10000;

		/// <summary>
		/// How many 100-nanosecond blocks in a second.
		/// </summary>
		public const uint SecondUnit =   10000000;

		/// <summary>
		/// How many 100-nanosecond blocks in a minute.
		/// </summary>
		public const uint MinuteUnit =  600000000;

		/// <summary>
		/// How many 100-nanosecond blocks in an hour.
		/// </summary>
		public const ulong HourUnit = 36000000000;

		/// <summary>
		/// How many 100-nanosecond blocks in a day.
		/// </summary>
		public const ulong DayUnit = 864000000000;
	}
}

