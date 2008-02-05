//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

//#define EXCEPTION_NOT_SUPPORTED

namespace SharpOS.Kernel.Tests.CS {
	public class Exceptions {
#if !EXCEPTION_NOT_SUPPORTED
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

		public static uint CMPCatch ()
		{
			uint result;

			try {
				result = 0;

				ThrowException ();

			} catch (System.Exception exception) {
				result = 1;
			}

			return result;			
		}

		private static void ThrowException ()
		{
			throw new System.Exception ();
		}
#else
		public static uint CMPExceptionHandling ()
		{
			return 0;
		}
#endif	
	}
}
