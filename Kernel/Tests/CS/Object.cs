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
	public class Objects {
		private class Point {
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

		public static uint CMPCreateObject ()
		{
			Point point = new Point (100, 200);

			if (point.GetSum () == 300)
				return 1;

			return 0;
		}
	}
}