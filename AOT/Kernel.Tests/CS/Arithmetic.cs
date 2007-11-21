//
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

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

		public static uint CMPSimpleAdd ()
		{
			if (Add (1, 2) == 3)
				return 1;
				
			return 0;
		}

		public static uint CMPSimpleSubtract ()
		{
			if (Subtract (2, 1) == 1)
				return 1;

			return 0;
		}

		public static uint CMPSimpleMultiply ()
		{
			if (Multiply (4, 4) == 16)
				return 1;

			return 0;
		}

		public static uint CMPSimpleDivide ()
		{
			if (Divide (10, 5) == 2)
				return 1;

			return 0;
		}
	}
}
