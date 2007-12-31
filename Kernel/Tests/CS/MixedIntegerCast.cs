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
	public class MixedIntegerCast {
		/// <summary>
		/// uint -> sbyte
		/// </summary>
		public static uint CMPUInt2SByte ()
		{
			uint ivalue = 5;
			sbyte bvalue = 0;

			bvalue = (sbyte) ivalue;

			if (bvalue == 5)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// int -> byte
		/// </summary>
		public static uint CMPInt2Byte ()
		{
			int ivalue = 5;
			byte bvalue = 0;

			bvalue = (byte) ivalue;

			if (bvalue == 5)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// uint -> byte
		/// </summary>
		public static uint CMPUInt2Byte ()
		{
			uint ivalue = 0x80;
			byte bvalue = 0;

			bvalue = (byte) ivalue;

			if (bvalue == 0x80)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// byte -> int
		/// </summary>
		public static uint CMPByte2Int ()
		{
			byte bvalue = 200;
			int ivalue = 0;

			ivalue = (int) bvalue;

			if (ivalue == 200)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// sbyte -> uint
		/// </summary>
		public static uint CMPSByte2UInt ()
		{
			sbyte bvalue = 100;
			uint ivalue = 0;

			ivalue = (uint) bvalue;

			if (ivalue == 100)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// byte -> short
		/// </summary>
		public static uint CMPByte2Short ()
		{
			byte bvalue = 200;
			short svalue = 0;

			svalue = (short) bvalue;

			if (svalue == 200)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// sbyte -> ushort
		/// </summary>
		public static uint CMPSByte2Short ()
		{
			sbyte bvalue = 100;
			ushort svalue = 0;

			svalue = (ushort) bvalue;

			if (svalue == 100)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// short -> byte
		/// </summary>
		public static uint CMPShort2Byte ()
		{
			short svalue = 200;
			byte bvalue = 0;

			bvalue = (byte) svalue;

			if (bvalue == 200)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// ushort -> sbyte
		/// </summary>
		public static uint CMPUShort2Byte ()
		{
			ushort svalue = 100;
			sbyte bvalue = 0;

			bvalue = (sbyte) svalue;

			if (bvalue == 100)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// ushort -> int
		/// </summary>
		public static uint CMPUShort2Int ()
		{
			ushort svalue = 200;
			int ivalue = 0;

			ivalue = (int) svalue;

			if (ivalue == 200)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// short -> uint
		/// </summary>
		public static uint CMPShort2UInt ()
		{
			short svalue = 200;
			uint ivalue = 0;

			ivalue = (uint) svalue;

			if (ivalue == 200)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// uint -> short
		/// </summary>
		public static uint CMPUInt2Short ()
		{
			uint ivalue = 200;
			short svalue = 0;

			svalue = (short) ivalue;

			if (svalue == 200)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// int -> ushort
		/// </summary>
		public static uint CMPInt2UShort ()
		{
			int ivalue = 200;
			ushort svalue = 0;

			svalue = (ushort) ivalue;

			if (svalue == 200)
				return 1;
			else
				return 0;
		}
	}
}
