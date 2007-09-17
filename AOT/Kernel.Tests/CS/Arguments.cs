//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

namespace SharpOS.Kernel.Tests.CS {
	public class Arguments {
		public static void Arguments1 (uint a, uint b, ref uint c)
		{
			c = a + b;
			
			return;
		}

		public static void Arguments2 (uint a, uint b, out uint c)
		{
			c = a + b;
			
			return;
		}

		public static uint CMPArguments1 ()
		{
			uint c = 0;

			Arguments1 (1, 2, ref c);
			
			if (c == 3)
				return 1;
				
			return 0;
		}

		public static uint CMPArguments2 ()
		{
			uint c;

			Arguments2 (1, 2, out c);
			
			if (c == 3)
				return 1;
				
			return 0;
		}
	}
}
