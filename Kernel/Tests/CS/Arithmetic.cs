//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//
// TODO:
// - Create testcases for 64-bit division when implemented

namespace SharpOS.Kernel.Tests.CS {
	public class Arithmetic {
		public static uint Add (uint a, uint b)
		{
			return a + b;
		}

		public static uint Subtract (uint a, uint b)
		{
			return a - b;
		}

		public static uint Multiply (uint a, uint b)
		{
			return a * b;
		}

		public static uint Divide (uint a, uint b)
		{
			return a / b;
		}

		public static ulong Add (ulong a, ulong b)
		{
			return a + b;
		}

		public static ulong Subtract (ulong a, ulong b)
		{
			return a - b;
		}

		public static ulong Multiply (ulong a, ulong b)
		{
			return a * b;
		}

		public static ulong Divide (ulong a, ulong b)
		{
			// FIXME: 64-bit division not implemented yet
			// return a / b;
			return a;
		}

		public static uint CMPSimpleAdd32 ()
		{
			if (Add (1, 2) == 3)
				return 1;

			return 0;
		}

		public static uint CMPSimpleSubtract32 ()
		{
			if (Subtract (2, 1) == 1)
				return 1;

			return 0;
		}

		public static uint CMPSimpleMultiply32 ()
		{
			if (Multiply (4, 4) == 16)
				return 1;

			return 0;
		}

		public static uint CMPSimpleDivide32 ()
		{
			if (Divide (10, 5) == 2)
				return 1;

			return 0;
		}

		public static uint CMPSimpleAdd64 ()
		{
			if (Add (1L, 2L) == 3L)
				return 1;

			return 0;
		}

		public static uint CMPSimpleDivide64 ()
		{
			if (Divide (10L, 5L) == 2L)
				return 1;

			return 0;
		}

		public static uint CMPSimpleSubtract64 ()
		{
			if (Subtract (2L, 1L) == 1L)
				return 1;

			return 0;
		}

		public static uint CMPSimpleMultiply64 ()
		{
			if (Multiply (4L, 4L) == 16L)
				return 1;

			return 0;
		}

		public static uint CMPOverflowAdd64 ()
		{
			if (Add (1000000000000000L, 10000000000000L) ==
					1010000000000000L)
				return 1;

			return 0;
		}

		public static uint CMPOverflowSubtract64 ()
		{
			if (Subtract (1000000000000000L, 10000000000000L) ==
					990000000000000L)
				return 1;

			return 0;
		}

		public static uint CMPOverflowMultiply64 ()
		{
			if (Multiply (1000000000000000L, 100L) ==
					100000000000000000L)
				return 1;

			return 0;
		}
	}
}
