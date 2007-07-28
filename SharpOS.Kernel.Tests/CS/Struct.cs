//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

namespace SharpOS.Kernel.Tests.CS {
	public class Struct {
		private struct Point {
			public int x, y;

			public Point (int x, int y)
			{
				this.x = x;
				this.y = y;
			}
		}
		
		public static int Constructor (int a, int b)
		{
			Point point = new Point (a, b);

			return point.x + point.y;
		}
		
		public static uint CMPConstructor ()
		{
			if (Constructor (100, 200) == 300)
				return 1;
				
			return 0;
		}
	}
}
