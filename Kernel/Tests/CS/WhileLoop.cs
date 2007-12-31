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
	public class WhileLoop {
		public static uint CMP0 ()
		{
			int x, c;

			c = 10;
			x = 0;

			while (x < c) {
				if (x >= c)
					return 0;

				++x;
			}

			return 1;
		}
	}
}
