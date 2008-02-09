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
	public class TestException: System.Exception {
		public TestException (uint result)
		{
			this.result = result;
		}

		private uint result;

		public uint Result
		{
			get
			{
				return this.result;
			}
		}
	}

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

		public static uint CMPCatch2 ()
		{
			uint result;

			try {
				result = 0;

				ThrowTestException ();

			} catch (TestException exception) {
				result = exception.Result;

			} catch (System.Exception exception) {
				result = 2;
			}

			return result;
		}

		public static uint CMPCatch3 ()
		{
			uint result;

			try {
				result = 0;

				ThrowException ();

			} catch {
				result = 1;
			}

			return result;
		}

		public static uint CMPCatchDivideError ()
		{
			uint result;

			try {
				result = 0;

				DivideByZero (0);

			} catch (System.DivideByZeroException exception) {
				result = 1;

			} catch {
				result = 2;
			}

			return result;
		}

		private static void ThrowException ()
		{
			throw new System.Exception ();
		}

		private static void ThrowTestException ()
		{
			throw new TestException (1);
		}

		private static int DivideByZero (int value)
		{
			return 1 / value;
		}
#else
		public static uint CMPExceptionHandling ()
		{
			return 0;
		}
#endif	
	}
}
