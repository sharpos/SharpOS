//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;

namespace SharpOS.Kernel.Tests.CS
{
	public class Equals
	{
		public static uint CMPInt32EqualsInt32()
		{
			Int32 test1 = 4;
			Int32 test2 = 4;
			Int32 test3 = 6;

			if (!test1.Equals(test2))
				return 0;
			
			if (test1.Equals(test3))
				return 0;

			return 1;
		}

		public static uint CMPInt32EqualsInt32Object()
		{
			Int32 test1 = 4;
			object test2 = 4;
			object test3 = 6;

			if (!test1.Equals(test2))
				return 0;
			
			if (test1.Equals(test3))
				return 0;

			return 1;
		}

		public static uint CMPInt32ObjectEqualsInt32Object()
		{
			Object test1 = 4;
			Object test2 = 4;
			Object test3 = 6;

			if (!test1.Equals(test2))
				return 0;
			
			if (test1.Equals(test3))
				return 0;

			return 1;
		}
	}
}
