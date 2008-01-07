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
	public class Boxing {
		private struct Point {
			public int x, y;

			public Point (int x, int y)
			{
				this.x = x;
				this.y = y;
			}

			public int GetSum ()
			{
				return this.x + this.y;
			}
		}

		private static object GetPoint (int a, int b)
		{
			return new Point (a, b);
		}

		private static int Add (int a, int b)
		{
			Point point = (Point) GetPoint (a, b);

			return point.x + point.y;
		}

		public static uint CMPBoxUnbox ()
		{
			if (Add (100, 200) == 300)
				return 1;

			return 0;
		}

		public static uint CMP2 ()
		{
			object o = 0xbeef;

			if ((int) o == 0xbeef)
				return 1;

			return 0;
		}
	}
}