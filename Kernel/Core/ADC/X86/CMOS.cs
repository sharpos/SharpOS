//
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.AOT;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.ADC.X86 {
	public unsafe class CMOS {
		#region Address (enum)
		public enum Address 
		{	
			// RTC
			Seconds						= 0x00,		// Get seconds (00 to 59)
			Minutes						= 0x02,		// Get minutes (00 to 59)
			MinuteAlarm					= 0x03,
			Hour						= 0x04,		// Get hours (see notes)
			HourAlarm					= 0x05,
			DayOfWeek					= 0x06,
			DayOfMonth					= 0x07,		// Get day of month (01 to 31)
			Month						= 0x08,		// Get month (01 to 12)
			Year						= 0x09,		// Get year (00 to 99)
			StatusRegisterA				= 0x0A,
			StatusRegisterB				= 0x0B,
			StatusRegisterC				= 0x0C,
			StatusRegisterD				= 0x0D,

			// IBM		
			DiskDriveType				= 0x10,		// Installed floppy drive types			

		}
		#endregion

		#region Read
		public static byte Read(Address address)
		{
			IO.Out8 (IO.Port.RTC_CommandPort, (byte)(0x80 | (byte)address));
			return IO.In8 (IO.Port.RTC_DataPort);
		}
		#endregion
	}
}