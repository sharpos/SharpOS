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
	public class Timezone {
		public static ulong GetUTC (ulong localTimestamp)
		{
			return GetUTC (localTimestamp, Clock.Timezone);
		}

		public static ulong GetUTC (ulong localTimestamp, SByte tz)
		{
			SByte posTz = tz;

			if (tz < 0)
				posTz = (SByte) (0 - tz);

			ulong offset = (ulong)posTz * Timestamp.HourUnit;

			if (tz < 0)
				return localTimestamp + offset;
			else
				return localTimestamp - offset;
		}

		public static ulong Localize (ulong utcTimestamp)
		{
			return Localize (utcTimestamp, Clock.Timezone);
		}

		public static ulong Localize (ulong utcTimestamp, SByte tz)
		{
			SByte posTz = tz;

			if (tz < 0)
				posTz = (SByte) (0 - tz);

			ulong offset = (ulong)posTz * Timestamp.HourUnit;

			if (tz < 0)
				return utcTimestamp - offset;
			else
				return utcTimestamp + offset;
		}
	}
}

