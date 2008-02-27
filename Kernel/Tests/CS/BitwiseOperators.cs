//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

namespace SharpOS.Kernel.Tests.CS {
	public class BitwiseOperators {
		public static uint Or (uint a, uint b)
		{
			return a | b;
		}

		public static uint And (uint a, uint b)
		{
			return a & b;
		}

		public static uint Xor (uint a, uint b)
		{
			return a ^ b;
		}

		public static uint Not (uint a)
		{
			return ~a;
		}

		public static uint ShiftLeft (uint a, int shift)
		{
			return a << shift;
		}

		public static uint ShiftRight (uint a, int shift)
		{
			return a >> shift;
		}

		public static uint CMPSimpleAND ()
		{
			if (And (0xFFF0, 0x0FFF) == 0x0FF0)
				return 1;

			return 0;
		}

		public static uint CMPSimpleOR ()
		{
			if (Or (0xFF00, 0x00FF) == 0xFFFF)
				return 1;

			return 0;
		}

		public static uint CMPSimpleXOR ()
		{
			if (Xor (0xFF0F, 0xFFFF) == 0x00F0)
				return 1;

			return 0;
		}

		public static uint CMPSimpleNot ()
		{
			if (Not (1) == 0xfffffffeL)
				return 1;

			return 0;
		}

		public static uint CMPSimpleShiftLeft ()
		{
			if (ShiftLeft (2, 1) == 4)
				return 1;

			return 0;
		}

		public static uint CMPUnsignedShiftLeft ()
		{
			if (ShiftLeft (1, 31) == 2147483648U)
				return 1;

			return 0;
		}

		public static uint CMPSimpleShiftRight ()
		{
			if (ShiftRight (2, 1) == 1)
				return 1;

			return 0;
		}
	}
}
