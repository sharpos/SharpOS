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

			public virtual int GetSum ()
			{
				return this.x + this.y;
			}
		}

		private class SuperPoint : Point {
			public SuperPoint (int x, int y)
				: base (x, y)
			{
			}

			public override int GetSum ()
			{
				return base.GetSum () * 2;
			}
		}

		public static uint CMPFieldNullReference ()
		{
			Point point = null;

			try {
				int y = point.x;
				return 3;
			} catch (System.NullReferenceException) {
				return 1;
			} catch {
				return 2;
			}
		}

		public static uint CMPMethodNullReference ()
		{
			SuperPoint point = null;

			try {
				point.GetSum ();
				return 3;
			} catch (System.NullReferenceException) {
				return 1;
			} catch {
				return 2;
			}
		}

		public static uint CMPCreateObject ()
		{
			Point point = new Point (100, 200);

			if (point.GetSum () == 300)
				return 1;

			return 0;
		}

		public static uint CMPOverrideObject ()
		{
			SuperPoint point = new SuperPoint (100, 200);

			if (point.GetSum () == 600)
				return 1;

			return 0;
		}

		public static uint CMPDefaultToString ()
		{
			if (new object().ToString() == "System.Object")
				return 1;

			return 0;
		}

		public static uint CMPIsOperator ()
		{
			object obj = new object();
			
			if (!(obj is object))
				return 0;

			if (obj is string)
				return 0;

			return 1;
		}

	}
}
