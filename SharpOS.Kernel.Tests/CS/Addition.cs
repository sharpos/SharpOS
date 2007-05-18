//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

namespace SharpOS.Kernel.Tests.CS {
	public class Addition {
		public static uint Add (uint a, uint b)
		{
			return a + b;
		}
		
		public static uint CMP0 ()
		{
			if (Add (1, 2) == 3)
				return 1;
				
			return 0;
		}
	}
}
