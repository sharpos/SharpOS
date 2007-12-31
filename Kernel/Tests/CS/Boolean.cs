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
	public class Boolean {
		public static bool And (bool a, bool b)
		{
			return a && b;
		}

		public static bool Or (bool a, bool b)
		{
			return a || b;
		}

		public static bool Not (bool a)
		{
			return !a;
		}

		public static uint CMPSimpleAnd ()
		{
			if (And (true, false) != false)
				return 0;

			if (And (true, true) != true)
				return 0;

			if (And (false, false) != false)
				return 0;

			return 1;
		}

		public static uint CMPSimpleOr ()
		{
			if (Or (true, false) != true)
				return 0;

			if (Or (true, true) != true)
				return 0;

			if (Or (false, false) != false)
				return 0;

			return 1;
		}

		public static uint CMPSimpleNot ()
		{
			if (Not (false) != true)
				return 0;

			if (Not (true) != false)
				return 0;

			return 1;
		}
	}
}
