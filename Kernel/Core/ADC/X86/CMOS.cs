//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
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
using SharpOS.AOT.X86;

namespace SharpOS.Kernel.ADC.X86 {
	public unsafe class CMOS {
		#region Address (enum)
		public enum Address 
		{	
			// RTC
			Seconds						= 0x00,		// Get seconds (00 to 59)
			SecondAlarm					= 0x01,
			Minutes						= 0x02,		// Get minutes (00 to 59)
			MinuteAlarm					= 0x03,
			Hour						= 0x04,		// Get hours
			HourAlarm					= 0x05,
			DayOfWeek					= 0x06,
			DayOfMonth					= 0x07,		// Get day of month (01 to 31)
			Month						= 0x08,		// Get month (01 to 12)
			Year						= 0x09,		// Get year (00 to 99)
			StatusRegisterA				= 0x0A,		
			// Bit 7     - (1) time update cycle in progress, data ouputs undefined
			//				   (bit 7 is read only)
			// Bit 6,5,4 - 22 stage divider. 010b - 32.768 Khz time base (default)
			// Bit 3-0   - Rate selection bits for interrupt.
			//				  0000b - none
			//				  0011b - 122 microseconds (minimum)
			//				  1111b - 500 milliseconds
			//				  0110b - 976.562 microseconds (default)

			StatusRegisterB				= 0x0B,		
			// Bit 7 - 1 enables cycle update, 0 disables
			// Bit 6 - 1 enables periodic interrupt
			// Bit 5 - 1 enables alarm interrupt
			// Bit 4 - 1 enables update-ended interrupt
			// Bit 3 - 1 enables square wave output
			// Bit 2 - Data Mode - 0: BCD, 1: Binary
			// Bit 1 - 24/12 hour selection - 1 enables 24 hour mode
			// Bit 0 - Daylight Savings Enable - 1 enables

			StatusRegisterC				= 0x0C,
			// Bit 7 - Interrupt request flag - 1 when any or all of bits 6-4 are
			//         1 and appropriate enables (Register B) are set to 1. Generates
			//         IRQ 8 when triggered.
			// Bit 6 - Periodic Interrupt flag
			// Bit 5 - Alarm Interrupt flag
			// Bit 4 - Update-Ended Interrupt Flag
			// Bit 3-0 reserved

			StatusRegisterD				= 0x0D,
			// Bit 7 - Valid RAM - 1 indicates batery power good, 0 if dead or
			//         disconnected.
			// Bit 6-0 reserved

			DiagnosticStatus			= 0x0E,
			// Bit 7 - When set (1) indicates clock has lost power
			// Bit 6 - (1) indicates incorrect checksum
			// Bit 5 - (1) indicates that equipment configuration is incorrect
			//             power-on check requires that atleast one floppy be installed
			// Bit 4 - (1) indicates error in memory size
			// Bit 3 - (1) indicates that controller or disk drive failed initialization
			// Bit 2 - (1) indicates that time is invalid
			// Bit 1 - (1) indicates installed adaptors do not match configuration
			// Bit 0 - (1) indicates a time-out while reading adaptor ID

			ShutdownStatus				= 0x0F,
			// 00		soft reset or unexpected shutdown
			// 01		shut down after memory size determination
			// 02		shut down after memory test
			// 03		shut down with memory error
			// 04		shut down with boot loader request
			// 05		JMP DWORD request with INT init
			// 06		protected mode test 7 passed
			// 07		protected mode test 7 failed
			// 08		protected mode test 1 failed
			// 09		block move shutdown request
			// 0A       JMP DWORD request without INT init
			// 0B       Used by 80386
			
			DiskDriveType				= 0x10,		// Installed floppy drive types
			// Bits 7-4 - First Floppy Disk Drive Type
			//			  0      No Drive
			//			  1      360 KB 5 1/4 Drive
			//			  2      1.2 MB 5 1/4 Drive - note: not listed in PS/2 technical manual
			//			  3      720 KB 3 1/2 Drive
			//			  4      1.44 MB 3 1/2 Drive
			//			  5h-Fh  unused (??? 5h: 2.88 Mb 3 1/2 Drive ???)
			//			  
			//			  Bits 3-0 Second Floppy Disk Drive Type (bit settings same as A)
			//			  
			//			  Hence a PC having a 5 1/4 1.2 Mb A: drive and a 1.44 Mb B: drive will
			//			  have a value of 24h in byte 10h. With a single 1.44 drive: 40h.
			
			SystemConfigurationSettings	= 0x11,
			// Bit 7 = Mouse support disable/enable 
			// Bit 6 = Memory test above 1MB disable/enable 
			// Bit 5 = Memory test tick sound disable/enable 
			// Bit 4 = Memory parity error check disable/enable 
			// Bit 3 = Setup utility trigger display disable/enable 
			// Bit 2 = Hard disk type 47 RAM area (0:300h or upper 1KB of DOS area) 
			// Bit 1 = Wait for <F1> if any error message disable/enable 
			// Bit 0 = System boot up with Numlock (off/on) 
			
			EquipmentList				= 0x14,
			// Bit 6-7 - number of diskette drives installed
			//			 00  1 diskette drive
			//			 01  2 diskette drives
			//			 10  reserved
			//			 11  reserved
			// Bit 4-5 - primary display
			//			 00  reserved
			//			 01  CGA 40 column color
			//			 10  CGA 80 column color
			//			 11  monochrome
			// Bit 3   - Display adapter installed/not installed
			// Bit 2   - Keyboard installed/not installed
			// Bit 1   - 1=math coprocessor installed, 0=none
			// Bit 0   - 1=diskette drives installed, 0=none

		}
		#endregion

		#region Read
		public static byte Read(Address address)
		{
			Asm.CLI();
			IO.Write8 (IO.Port.RTC_CommandPort, (byte)(0x80 | (byte)address));
			Asm.NOP();
			Asm.NOP();
			Asm.NOP();
			byte result = IO.Read8(IO.Port.RTC_DataPort);
			Asm.STI();
			return result;
		}
		#endregion
	}
}