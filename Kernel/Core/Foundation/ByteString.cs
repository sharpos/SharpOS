//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.Kernel;
using SharpOS.Kernel.Memory;
using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.Foundation {

	public unsafe class ByteString {

		public static int Length (byte *str)
		{
			int len = 0;

			while (str [len] != 0)
				++len;

			return len;
		}

		public static bool GetBytes (string src, byte *dst, int size)
		{
			Diagnostics.Assert (src != null, "ByteString.GetBytes (): argument `src' is null");
			Diagnostics.Assert (dst != null, "ByteString.GetBytes (): argument `dst' is null");
			Diagnostics.Assert (size > 0 && size < ByteString.Length (dst),
				"ByteString.GetBytes (): argument `size' is out of range");
			Diagnostics.Assert (size >= src.Length, "ByteString.GetBytes (): buffer is too small");

			if (src.Length > size)
				return false;

			for (int x = 0; x < size; ++x)
				dst [x] = (byte)src[x];

			return true;
		}

		#region Concat() family

		/// <summary>
		/// Concatenates <paramref name="count" /> bytes of
		/// the string in buffer <paramref name="src" />
		/// to the end of the string in buffer
		/// <paramref name="buffer" />.
		/// </summary>
		public static void Concat (byte *buffer, int size, byte *src, int count)
		{
			int c = count;
			int start = Length (buffer);

			if (c <= 0)
				c = Length (src);

			Diagnostics.Assert (*(buffer+size) == 0, "Concat: warning, buffer may not have been allocated by ByteString");

			Diagnostics.Assert (start + c < (size+1), "Concat: buffer is too small");

			Copy (buffer, size, src, start, c);
			*(buffer+start+c) = 0;
		}

		#endregion
		#region Copy() family

		public static void Copy (byte *buffer, int size, byte *src, int index, int count)
		{
			Diagnostics.Assert (index + count < size+1, "Copy: buffer is too small");

			byte *ptr = buffer + index;
			byte *sptr = src;

			for (int x = index; x < index+count; ++x) {
				*ptr = *sptr;
				++ptr;
				++sptr;
			}
		}

		#endregion
		#region Compare() family

		public static int Compare (byte *a, int aFrom, byte *b, int bFrom, int count)
		{
			int c = count;
			int aLength = ByteString.Length (a), bLength = ByteString.Length (b);

			if (count == 0 && aLength != bLength)
				return aLength - bLength;
			else if (count != 0 && (aFrom + count > aLength || bFrom + count > bLength))
				return aLength - bLength;

			if (c == 0)
				c = aLength;

			for (int x = 0; x < c; ++x) {

				if (x >= c)
					break;
				if (a [aFrom + x] != b [bFrom + x])
					return a [aFrom + x] - b [bFrom + x];
			}

			return 0;
		}

		public static int Compare (byte *a, int aFrom, string b, int bFrom, int count)
		{
			int c = count;
			int aLength = ByteString.Length (a);

			if (count == 0 && aLength != b.Length) {
				return aLength - b.Length;
			} else if (count != 0 && (aFrom + count > aLength || bFrom + count > b.Length)) {
				return aLength - b.Length;
			}

			if (c == 0)
				c = aLength;

			for (int x = 0; x < c; ++x) {

				if (x >= c) {
					break;
				}

				if (a [aFrom + x] != ((byte) (b[bFrom + x]))) {
					return ((int)a[aFrom + x] - ((int)(byte)b[bFrom + x]));
				}
			}

			return 0;
		}

		public static int Compare (byte *a, byte *b, int count)
		{
			return Compare (a, 0, b, 0, count);
		}

		public static int Compare (byte *a, byte *b)
		{
			return Compare (a, 0, b, 0, 0);
		}

		public static int Compare (byte *a, string b, int count)
		{
			return Compare (a, 0, b, 0, count);
		}

		public static int Compare (byte *a, string b)
		{
			return Compare (a, 0, b, 0, 0);
		}

		#endregion
		#region Testcases

		internal static void __RunTests ()
		{
			__Test1();
		}

		public static void __Test1 ()
		{
			byte *ptr1 = (byte*)Stubs.CString ("US"), ptr2 = (byte*)Stubs.CString ("SK");
			byte *longer = (byte*)Stubs.CString ("The US");

			//Test constant CString buffers
			if (ByteString.Compare (ptr1, ptr2) == 0)
				TextMode.WriteLine ("ByteString.Compare(): test FAIL: 'US' != 'SK'");
			else
				TextMode.WriteLine ("ByteString.Compare(): test pass: 'US' != 'SK'");

			if (ByteString.Compare (ptr1, ptr1) == 0)
				TextMode.WriteLine ("ByteString.Compare(): test pass: 'US' == 'US'");
			else
				TextMode.WriteLine ("ByteString.Compare(): test FAIL: 'US' == 'US'");

			if (ByteString.Compare (ptr1, 1, ptr1, 1, 1) == 0)
				TextMode.WriteLine ("ByteString.Compare(): test pass: 'U[S]' == 'U[S]'");
			else
				TextMode.WriteLine ("ByteString.Compare(): test FAIL: 'U[S]' == 'U[S]'");

			if (ByteString.Compare (longer, 4, ptr1, 0, 2) == 0)
				TextMode.WriteLine ("ByteString.Compare(): test pass: 'The [US]' == 'US'");
			else
				TextMode.WriteLine ("ByteString.Compare(): test FAIL: 'The [US]' == 'US'");

			//Test constant CString buffer with constant String type

			if (ByteString.Compare (ptr1, "SK") == 0)
				TextMode.WriteLine ("ByteString.Compare(): test FAIL: 'US' != const 'SK'");
			else
				TextMode.WriteLine ("ByteString.Compare(): test pass: 'US' != const 'SK'");

			if (ByteString.Compare (ptr1, "US") == 0)
				TextMode.WriteLine ("ByteString.Compare(): test pass: 'US' == const 'US'");
			else
				TextMode.WriteLine ("ByteString.Compare(): test FAIL: 'US' == const 'US'");

			if (ByteString.Compare (longer, 4, "US", 0, 2) == 0)
				TextMode.WriteLine ("ByteString.Compare(): test pass: 'The [US]' == const 'US'");
			else
				TextMode.WriteLine ("ByteString.Compare(): test FAIL: 'The [US]' == const 'US'");

			//Test that constant String is working properly
			const string str1 = "US";
			const string str2 = "SK";
			if ((byte)str1[0] == (byte)'U')
				TextMode.WriteLine("ByteString : test pass: (byte)\"US\"[0]==(byte)'U'");
			else
				TextMode.WriteLine("ByteString : test FAIL: (byte)\"US\"[0]==(byte)'U'");

			if ((byte)str1[1] == (byte)'S')
				TextMode.WriteLine("ByteString : test pass: (byte)\"US\"[1]==(byte)'S'");
			else
				TextMode.WriteLine("ByteString : test FAIL: (byte)\"US\"[1]==(byte)'S'");

			if (str1.Length == 2)
				TextMode.WriteLine("ByteString : test pass: \"US\".Length==2");
			else
				TextMode.WriteLine("ByteString : test FAIL: \"US\".Length==2");

			if ((byte)str1[1] == (byte)str2[0])
				TextMode.WriteLine("ByteString : test pass: (byte)\"US\"[1]==(byte)\"SK\"[0]");
			else
				TextMode.WriteLine("ByteString : test FAIL: (byte)\"US\"[1]==(byte)\"SK\"[0]");
		}

		#endregion
	}
}
