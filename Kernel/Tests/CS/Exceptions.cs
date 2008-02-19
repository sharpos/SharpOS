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

		public static uint CMPFinally2 ()
		{
			uint result = 0;

			try {
				ThrowException2 (ref result);
			} catch {
				++result;
			}

			if (result == 2)
				result = 1;
			else
				result = 0;

			return result;
		}

		public static uint CMPCatch ()
		{
			uint result;

			try {
				result = 0;

				ThrowException ();

			} catch (System.Exception) {
				result = 1;
			}

			return result;
		}

		public static uint CMPCatch2 ()
		{
			uint result = 0;

			/*try {
				ThrowTestException ();

			} catch (TestException exception) {
				result = exception.Result;

			} catch (System.Exception exception) {
				result = 2;
			}*/

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

			} catch (System.DivideByZeroException) {
				result = 1;

			} catch {
				result = 2;
			}

			return result;
		}

		public static uint CMPTryCatchFinally1 ()
		{
			uint result = 0;

			try {
				ThrowException ();
			} catch {
				++result;
			} finally {
				++result;
			}

			if (result == 2)
				result = 1;
			else
				result = 0;

			return result;
		}

		public static uint CMPTryCatchFinally2 ()
		{
			uint result = 0;

			try {
				result = 1;
			} catch {
				--result;
			} finally {
				++result;
			}

			if (result == 2)
				result = 1;
			else
				result = 0;

			return result;
		}

		public static uint CMPTryCatchFinally3 ()
		{
			uint result = 0;

			try {
				ThrowTestException ();
			} catch (TestException) {
				++result;
			} finally {
				++result;
			}

			if (result == 2)
				result = 1;
			else
				result = 0;

			return result;
		}

		public static uint CMPTryCatchFinally4 ()
		{
			uint result = 0;

			try {
				ThrowTestException ();
			} catch (TestException) {
				++result;
			} finally {
				++result;
			}

			if (result == 2)
				result = 1;
			else
				result = 0;

			return result;
		}

		public static uint CMPTryCatchFinally5 ()
		{
			uint result = 0;

			try {
				ThrowException2 (ref result);
			} catch {
				++result;
			} finally {
				++result;
			}

			if (result == 3)
				result = 1;
			else
				result = 0;

			return result;
		}

		public static uint CMPRethrow ()
		{
			uint result = 0;

			try {
				try {
					result++;

					ThrowTestException ();

				} catch {
					result++;

					throw;
				}
			} catch {
				result++;
			}

			if (result == 3)
				result = 1;
			else
				result = 0;

			return result;

		}

		private static void ThrowException ()
		{
			throw new System.Exception ();
		}

		private static void ThrowException2 (ref uint result)
		{
			try {
				ThrowException ();
			} finally {
				++result;
			}
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
