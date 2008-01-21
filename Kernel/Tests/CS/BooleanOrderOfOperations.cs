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
	public class BooleanOrderOfOperations {

		public static uint CMPConstants ()
		{
//Unreachable code detected
#pragma warning disable 0162
			if ((true && false || true) != true)
				return 0;

			if ((true || false && false) != true)
				return 0;

			if ((true && false || true && false) != false)
				return 0;

			if ((true && false || true && true) != true)
				return 0;

			return 1;
#pragma warning restore 0162
		}

		public static uint CMPValues ()
		{
			bool a, b, c, d;

			a = true;
			b = false;
			c = true;

			if ((a && b || c) != true)
				return 0;

			a = true;
			b = false;
			c = false;

			if ((a || b && c) != true)
				return 0;

			a = true;
			b = false;
			c = true;
			d = false;
			if ((a && b || c && d) != false)
				return 0;

			a = true;
			b = false;
			c = true;
			d = true;

			if ((a && b || c && d) != true)
				return 0;

			return 1;
		}

		public static uint CMPValuesAndConstants ()
		{
			bool a, b, c, d;

			a = true;
			b = false;

			if ((a && b || true) != true)
				return 0;

			a = true;
			c = false;

			if ((a || false && c) != true)
				return 0;

			a = true;
			b = false;
			c = true;

			if ((a && b || c && false) != false)
				return 0;

			b = false;
			c = true;
			d = true;

			if ((true && b || c && d) != true)
				return 0;

			return 1;
		}
	}
}
