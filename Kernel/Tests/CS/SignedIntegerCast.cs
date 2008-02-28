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
	public class SignedIntegerCast {
		/// <summary>
		/// int -> sbyte
		/// </summary>
		public static uint CMPInt2SByte ()
		{
			int ivalue = -5;
			sbyte bvalue = 0;

			bvalue = (sbyte) ivalue;

			if (bvalue == -5)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// sbyte -> int
		/// </summary>
		public static uint CMPSByte2Int ()
		{
			sbyte bvalue = -100;
			int ivalue = 0;

			ivalue = (int) bvalue;

			if (ivalue == -100)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// sbyte -> short
		/// </summary>
		public static uint CMPSByte2Short ()
		{
			sbyte bvalue = -100;
			short svalue = 0;

			svalue = (short) bvalue;

			if (svalue == -100)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// short -> sbyte
		/// </summary>
		public static uint CMPShort2SByte ()
		{
			short svalue = -100;
			sbyte bvalue = 0;

			bvalue = (sbyte) svalue;

			if (bvalue == -100)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// short -> int
		/// </summary>
		public static uint CMPShort2Int ()
		{
			short svalue = -200;
			int ivalue = 0;

			ivalue = (int) svalue;

			if (ivalue == -200)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// int -> short
		/// </summary>
		public static uint CMPInt2Short ()
		{
			int ivalue = -200;
			short svalue = 0;

			svalue = (short) ivalue;

			if (svalue == -200)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// long -> sbyte
		/// </summary>
		public static uint CMPLong2SByte ()
		{
			long  lvalue = -100;
			sbyte bvalue = 0;

			bvalue = (sbyte) lvalue;

			if (bvalue == -100)
				return 1;
			else
				return 0;
		}

	}
}
