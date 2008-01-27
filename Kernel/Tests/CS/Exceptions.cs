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
	public class Exceptions {
		public static uint CMPFinally ()
		{
			uint result;

			try {
				result = 0;
			} finally {
				result = 1;
			}

			return result;
		}
	}
}
