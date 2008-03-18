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
	public class Struct {
		private struct Point {
			private static readonly int SOME_CONSTANT = 1;
			public int x, y;

			public Point (int x, int y)
			{
				this.x = x;
				this.y = y;
			}

			public int GetSum ()
			{
				return this.x + this.y;
			}
			
			static public int GetConstant ()
			{
				return Point.SOME_CONSTANT;
			}
		}

		private static int Constructor (int a, int b)
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

		private unsafe static int StructPointer (void* pointer)
		{
			Point* point = (Point*) pointer;

			return point->x + point->y;
		}

		public unsafe static uint CMPStructPointer ()
		{
			Point point = new Point (100, 200);

			if (StructPointer (((Point*) &point)) == 300)
				return 1;

			return 0;
		}

		private unsafe static int StructPointer2Helper (Point point)
		{
			return point.GetSum ();
		}

		private unsafe static int StructPointer2 (void* pointer)
		{
			Point* point = (Point*) pointer;

			return StructPointer2Helper (*point);
		}

		public unsafe static uint CMPStructPointer2 ()
		{
			Point point = new Point (100, 200);

			if (StructPointer2 (((Point*) &point)) == 300)
				return 1;

			return 0;
		}

		public unsafe static uint CMPEmptyStruct ()
		{
			Point point = new Point ();

			if (point.y == 0 && point.x == 0)
				return 1;

			return 0;
		}

		private static int StructParameter (int result, Point point)
		{
			return point.GetSum () == result ? 1 : 0;
		}

		public static int CMPStructParameter ()
		{
			Point point = new Point (100, 200);

			return StructParameter (300, point);
		}

		private static void NoChanges (Point point)
		{
			point.x *= 2;
			point.y *= 3;
		}

		public static int CMPNoChanges ()
		{
			Point point = new Point (100, 200);

			NoChanges (point);

			return point.GetSum () == 300 ? 1 : 0;
		}

		private static int Copy (Point point)
		{
			Point local = point;

			return local.GetSum ();
		}

		public static int CMPCopy ()
		{
			Point point = new Point (100, 200);

			return Copy (point) == 300 ? 1 : 0;
		}

		private static Point Return (int x, int y)
		{
			Point point = new Point (x, y);

			return point;
		}

		public static int CMPReturn ()
		{
			Return (200, 300);

			return Return (100, 200).GetSum () == 300 ? 1 : 0;
		}

		public unsafe static int CMPSizeof1 ()
		{
			int size = sizeof (Point);

			if (size != 8)
				return 0;

			return 1;
		}

		unsafe struct Header {
//Unreachable code detected
#pragma warning disable 0169
			Header*	Next;
			Header*	Prev;
			byte	test;
#pragma warning restore 0169
		}

		public unsafe static int CMPSizeof2 ()
		{
			uint ptr = 0x1000000;
			byte* testPtr = (byte*) ptr;

			byte* test = testPtr + sizeof (Header);

			if ((ptr + 9) != (uint) test)
				return 0;

			return 1;
		}
		
		public static int CMPPrivateStatic ()
		{
			return Point.GetConstant ();
		}
	}
}