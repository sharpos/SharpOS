//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

namespace SharpOS.Kernel.Tests.CS {
	public class UnsignedIntegerCast {
		/// <summary>
		/// uint -> byte
		/// </summary>
		public static uint CMPUInt2Byte ()
		{
			uint ivalue = 5;
			byte bvalue = 0;

			bvalue = (byte) ivalue;

			if (bvalue == 5)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// byte -> uint
		/// </summary>
		public static uint CMPByte2UInt ()
		{
			byte bvalue = 200;
			uint ivalue = 0;

			ivalue = (uint) bvalue;

			if (ivalue == 200)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// byte -> ushort
		/// </summary>
		public static uint CMPByte2UShort ()
		{
			byte bvalue = 200;
			ushort svalue = 0;

			svalue = (ushort) bvalue;

			if (svalue == 200)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// ushort -> byte
		/// </summary>
		public static uint CMPUShort2Byte ()
		{
			ushort svalue = 200;
			byte bvalue = 0;

			bvalue = (byte) svalue;

			if (bvalue == 200)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// ushort -> uint
		/// </summary>
		public static uint CMPUShort2UInt ()
		{
			ushort svalue = 200;
			uint ivalue = 0;

			ivalue = (uint) svalue;

			if (ivalue == 200)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// uint -> ushort
		/// </summary>
		public static uint CMPUInt2UShort ()
		{
			uint ivalue = 200;
			ushort svalue = 0;

			svalue = (ushort) ivalue;

			if (svalue == 200)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// ulong -> byte
		/// </summary>
		public static uint CMPULong2Byte ()
		{
			ulong lvalue = 100;
			byte  bvalue = 0;

			bvalue = (byte) lvalue;

			if (bvalue == 100)
				return 1;
			else
				return 0;
		}

	}
}
