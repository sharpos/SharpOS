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
	public class Enum {
		public 
		enum IntEnum : int {
			A = 0,
			B = 1,
			C = 2,
			D = 3,
			E = 4
		}

		[System.Flags]
		enum IntAnonFlagsEnum : int {
			Zero = 0,
			A,
			B,
			C,
			D,
			E
		}

		[System.Flags]
		enum IntFlagsEnum : int {
			Zero = 0,
			A = 1,
			B = (1 << 1),
			C = (1 << 2),
			D = (1 << 3),
			E = (1 << 4),
		}

		public static uint CMPLiteralToInt ()
		{
//Unreachable code detected
#pragma warning disable 0162
			if ((int) IntEnum.E != 4 || (int) IntEnum.A != 0)
				return 0;

			return 1;
#pragma warning restore 0162
		}

		public static uint CMPIntToLiteral ()
		{
//Unreachable code detected
#pragma warning disable 0162
			if ((IntEnum) 4 != IntEnum.E || (IntEnum) 0 != IntEnum.A)
				return 0;

			return 1;
#pragma warning restore 0162
		}

		public static uint CMPIntConstantComparison ()
		{
//Unreachable code detected
#pragma warning disable 0162
			if (IntEnum.A != IntEnum.A)
				return 0;		// but really... come on.

			if (IntEnum.A == IntEnum.B)
				return 0;

			if (IntEnum.A == IntEnum.E)
				return 0;

			return 1;
#pragma warning restore 0162
		}

		public static uint CMPIntValueComparison ()
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

		public static uint CMPSimpleIntFlags ()
		{
//Unreachable code detected
#pragma warning disable 0162
			IntEnum a;

			if ((int) (IntEnum.A | IntEnum.C) != 2)
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
#pragma warning restore 0162
		}

		private static IntEnum ReturnIntA ()
		{
			return IntEnum.A;
		}

		private static IntEnum ReturnIntB ()
		{
			return IntEnum.B;
		}

		private static IntEnum ReturnIntC ()
		{
			return IntEnum.C;
		}

		public static int CMPIntEnumReturn ()
		{
			if (ReturnIntA () != IntEnum.A)
				return 0;

			if (ReturnIntB () != IntEnum.B)
				return 0;

			if (ReturnIntC () != IntEnum.C)
				return 0;

			return 1;
		}

		enum ULongEnum : ulong {
			A = 0,
			B = 1,
			C = 2,
			D = 3,
			E = 4
		}

		[System.Flags]
		enum ULongAnonFlagsEnum : ulong {
			Zero = 0,
			A,
			B,
			C,
			D,
			E
		}

		[System.Flags]
		enum ULongFlagsEnum : ulong {
			Zero = 0,
			A = 1,
			B = (1 << 1),
			C = (1 << 2),
			D = (1 << 3),
			E = (1 << 4),
		}

		public static ulong CMPLiteralToULong ()
		{
//Unreachable code detected
#pragma warning disable 0162
			if ((ulong) ULongEnum.E != 4 || (ulong) ULongEnum.A != 0)
				return 0;

			return 1;
#pragma warning restore 0162
		}

		public static ulong CMPULongToLiteral ()
		{
//Unreachable code detected
#pragma warning disable 0162
			if ((ULongEnum) 4 != ULongEnum.E || (ULongEnum) 0 != ULongEnum.A)
				return 0;

			return 1;
#pragma warning restore 0162
		}

		public static ulong CMPULongConstantComparison ()
		{
//Unreachable code detected
#pragma warning disable 0162
			if (ULongEnum.A != ULongEnum.A)
				return 0;		// but really... come on.

			if (ULongEnum.A == ULongEnum.B)
				return 0;

			if (ULongEnum.A == ULongEnum.E)
				return 0;

			return 1;
#pragma warning restore 0162
		}

		public static ulong CMPULongValueComparison ()
		{
			ULongEnum a, b;

			/*a = ULongEnum.A;
			b = ULongEnum.B;
			
			if (a != b)
				return 0;		// but really... come on.
			*/

			a = ULongEnum.A;
			b = ULongEnum.B;

			if (a == b)
				return 0;

			a = ULongEnum.A;
			b = ULongEnum.E;

			if (a == b)
				return 0;

			return 1;
		}

		public static ulong CMPSimpleULongFlags ()
		{
#pragma warning disable 0162
			ULongEnum a;

			if ((long) (ULongEnum.A | ULongEnum.C) != 2)
				return 0;

			a = ULongEnum.A | ULongEnum.C | ULongEnum.E;

			if ((a & ULongEnum.A) != 0)
				return 0;

			if ((a & ULongEnum.B) != 0)
				return 0;

			if ((a & ULongEnum.C) == 0)
				return 0;

			if ((a & ULongEnum.D) == 0)
				return 0;

			if ((a & ULongEnum.E) == 0)
				return 0;

			return 1;
#pragma warning restore 0162
		}

		private static ULongEnum ReturnULongA ()
		{
			return ULongEnum.A;
		}

		private static ULongEnum ReturnULongB ()
		{
			return ULongEnum.B;
		}

		private static ULongEnum ReturnULongC ()
		{
			return ULongEnum.C;
		}

		public static ulong CMPULongEnumReturn ()
		{
//Unreachable code detected
#pragma warning disable 0162
			if (ReturnULongA () != ULongEnum.A)
				return 0;

			if (ReturnULongB () != ULongEnum.B)
				return 0;

			if (ReturnULongC () != ULongEnum.C)
				return 0;

			return 1;
#pragma warning restore 0162
		}
	}
}
