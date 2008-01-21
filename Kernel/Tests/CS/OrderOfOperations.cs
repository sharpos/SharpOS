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
	public class OrderOfOperations {

		public static uint CMPConstants ()
		{
//Unreachable code detected
#pragma warning disable 0162
			if (10 * 3 / 2 != 15)
				return 0;

			if (10 / 2 * 3 != 15)
				return 0;

			if (10 + 10 / 2 * 4 != 30)
				return 0;

			if (3 * 5 - 2 / 3 + 4 - 1 * 3 != 16)
				return 0;

			if ((4 + 10) * 2 != 28)
				return 0;

			if (2 * (4 + 10) == 80)
				return 0;

			return 1;
#pragma warning restore 0162
		}

		public static uint CMPValues ()
		{
			uint a, b, c, d, e, f, g;

			a = 10;
			b = 3;
			c = 2;

			if (a * b / c != 15)
				return 0;

			a = 10;
			b = 2;
			c = 3;

			if (a / b * c != 15)
				return 0;

			a = 10;
			b = 10;
			c = 2;
			d = 4;

			if (a + b / c * d != 30)
				return 0;

			a = 3;
			b = 5;
			c = 2;
			d = 3;
			e = 4;
			f = 1;
			g = 3;

			if (a * b - c / d + e - f * g != 16)
				return 0;

			a = 4;
			b = 10;
			c = 2;

			if ((a + b) * c != 28)
				return 0;

			a = 2;
			b = 4;
			c = 10;

			if (a * (b + c) == 80)
				return 0;

			return 1;
		}

		public static uint CMPValuesAndConstants ()
		{
			uint a, b, c, d, /*e,*/ f, g;

			a = 10;
			c = 2;

			if (a * 3 / c != 15)
				return 0;

			a = 10;
			b = 2;

			if (a / b * 3 != 15)
				return 0;

			a = 10;
			c = 2;
			d = 4;

			if (a + 10 / c * d != 30)
				return 0;

			a = 3;
			b = 5;
			d = 3;
			f = 1;
			g = 3;

			if (a * b - 2 / d + 4 - f * g != 16)
				return 0;

			b = 10;
			c = 2;

			if ((4 + b) * c != 28)
				return 0;

			a = 2;
			b = 4;

			if (a * (b + 10) == 80)
				return 0;

			return 1;
		}

	}
}
