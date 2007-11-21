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
	public class Enum {
		enum IntEnum: int {
			A = 0,
			B = 1,
			C = 2,
			D = 3,
			E = 4
		}

		[System.Flags]
		enum IntAnonFlagsEnum: int {
			Zero = 0, A, B, C, D, E
		}
		
		[System.Flags]
		enum IntFlagsEnum: int {
			Zero = 0,
			A = 1,
			B = (1<<1),
			C = (1<<2),
			D = (1<<3),
			E = (1<<4),
		}
		
		public static uint CMPLiteralToInt ()
		{
			if ((int) IntEnum.E != 4 || (int) IntEnum.A != 0)
				return 0;

			return 1;
		}
		
		public static uint CMPIntToLiteral ()
		{
			if ((IntEnum)4 != IntEnum.E || (IntEnum)0 != IntEnum.A)
				return 0;

			return 1;
		}

		public static uint CMPConstantComparison ()
		{
			if (IntEnum.A != IntEnum.A)
				return 0;		// but really... come on.

			if (IntEnum.A == IntEnum.B)
				return 0;
				
			if (IntEnum.A == IntEnum.E)
				return 0;

			return 1;
		}

		public static uint CMPValueComparison ()
		{
			IntEnum a, b;

			/*a = IntEnum.A;
			b = IntEnum.B;
			
			if (a != b)
				return 0;		// but really... come on.
			*/

			a = IntEnum.A;
			b = IntEnum.B;
			
			if (a == b)
				return 0;

			a = IntEnum.A;
			b = IntEnum.E;
			
			if (a == b)
				return 0;

			return 1;
		}

		public static uint CMPSimpleFlags ()
		{
			IntEnum a;

			if ((int)(IntEnum.A | IntEnum.C) != 2)
				return 0;

			a = IntEnum.A | IntEnum.C | IntEnum.E;
			
			if ((a & IntEnum.A) != 0)
				return 0;
			
			if ((a & IntEnum.B) != 0)
				return 0;
			
			if ((a & IntEnum.C) == 0)
				return 0;
			
			if ((a & IntEnum.D) == 0)
				return 0;
			
			if ((a & IntEnum.E) == 0)
				return 0;

			return 1;
		}

		private static IntEnum ReturnA()
		{
			return IntEnum.A;
		}

		private static IntEnum ReturnB()
		{
			return IntEnum.B;
		}

		private static IntEnum ReturnC()
		{
			return IntEnum.C;
		}

		public static int CMPEnumReturn()
		{
			if (ReturnA() != IntEnum.A)
				return 0;

			if (ReturnB() != IntEnum.B)
				return 0;

			if (ReturnC() != IntEnum.C)
				return 0;

			return 1;
		}
	}
}
