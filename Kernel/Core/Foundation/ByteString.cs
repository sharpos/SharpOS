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

		public static int Length (byte* str)
		{
			int len = 0;

			while (str [len] != 0)
				++len;

			return len;
		}

		public static bool GetBytes (string src, byte* dst, int size)
		{
			Diagnostics.Assert (src != null, "ByteString.GetBytes (): argument `src' is null");
			Diagnostics.Assert (dst != null, "ByteString.GetBytes (): argument `dst' is null");
			Diagnostics.Assert (size > 0 && size < ByteString.Length (dst),
				"ByteString.GetBytes (): argument `size' is out of range");
			Diagnostics.Assert (size >= src.Length, "ByteString.GetBytes (): buffer is too small");

			if (src.Length > size)
				return false;

			for (int x = 0; x < size; ++x)
				dst [x] = (byte) src [x];

			return true;
		}

		#region Concat() family

		/// <summary>
		/// Concatenates <paramref name="count" /> bytes of
		/// the string in buffer <paramref name="src" />
		/// to the end of the string in buffer
		/// <paramref name="buffer" />.
		/// </summary>
		public static void Concat (byte* buffer, int size, byte* src, int count)
		{
			int c = count;
			int start = Length (buffer);

			if (c <= 0)
				c = Length (src);

			Diagnostics.Assert (*(buffer + size) == 0, "Concat: warning, buffer may not have been allocated by ByteString");

			Diagnostics.Assert (start + c < (size + 1), "Concat: buffer is too small");

			Copy (buffer, size, src, start, c);
			*(buffer + start + c) = 0;
		}

		#endregion
		#region Copy() family

		public static void Copy (byte* buffer, int size, byte* src, int index, int count)
		{
			Diagnostics.Assert (index + count < size + 1, "Copy: buffer is too small");

			byte* ptr = buffer + index;
			byte* sptr = src;

			for (int x = index; x < index + count; ++x) {
				*ptr = *sptr;
				++ptr;
				++sptr;
			}
		}

		#endregion
		#region Compare() family

		public static int Compare (byte* a, int aFrom, byte* b, int bFrom, int count)
		{
			int c = count;
			int aLength = ByteString.Length (a + aFrom), bLength = ByteString.Length (b + bFrom);

			if (count == 0 && aLength != bLength)
				return aLength - bLength;
			else if (count != 0 && (count > aLength || count > bLength))
				return aLength - bLength;

			if (c == 0) // aLen == bLen - filtered at first if
				c = aLength;

			for (int x = 0; x < c; ++x) {

				if (x >= c)
					break;
				if (a [aFrom + x] != b [bFrom + x])
					return a [aFrom + x] - b [bFrom + x];
			}

			return 0;
		}

		public static int Compare (byte* a, int aFrom, string b, int bFrom, int count)
		{
			int c = count;
			int aLength = ByteString.Length (a + aFrom), bLength = b.Length - bFrom;

			if (count == 0 && aLength != bLength)
				return aLength - bLength;
			else if (count != 0 && (count > aLength || count > bLength))
				return aLength - bLength;

			if (c == 0) // aLen == bLen - filtered at first if
				c = aLength;

			for (int x = 0; x < c; ++x) {

				if (x >= c) {
					break;
				}

				if (a [aFrom + x] != ((byte) (b [bFrom + x]))) {
					return ((int) a [aFrom + x] - ((int) (byte) b [bFrom + x]));
				}
			}

			return 0;
		}

		public static int Compare (byte* a, byte* b, int count)
		{
			return Compare (a, 0, b, 0, count);
		}

		public static int Compare (byte* a, byte* b)
		{
			return Compare (a, 0, b, 0, 0);
		}

		public static int Compare (byte* a, string b, int count)
		{
			return Compare (a, 0, b, 0, count);
		}

		public static int Compare (byte* a, string b)
		{
			return Compare (a, 0, b, 0, 0);
		}

		#endregion
		#region Testcases

		internal static void __RunTests ()
		{
			__Test1 ();
		}

		public static void __Test1 ()
		{
			byte* ptr1 = (byte*) Stubs.CString ("US"), ptr2 = (byte*) Stubs.CString ("SK");
			byte* longer = (byte*) Stubs.CString ("The US");

			//Test constant CString buffers
			Testcase.Test (ByteString.Compare (ptr1, ptr2) != 0,
				"ByteString.Compare()",
				"Comparing: 'US' != 'SK'");

			Testcase.Test (ByteString.Compare (ptr1, ptr1) == 0,
				"ByteString.Compare()",
				"Comparing: 'US' == 'US'");

			Testcase.Test (ByteString.Compare (ptr1, 1, ptr1, 1, 1) == 0,
				"ByteString.Compare()",
				"Comparing substrings: 'U[S]' == 'U[S]'");

			Testcase.Test (ByteString.Compare (longer, 4, ptr1, 0, 2) == 0,
				"ByteString.Compare()",
				"Comparing substrings: 'The [US]' == 'US'");

			Testcase.Test (ByteString.Compare (longer, 4, ptr1, 0, 0) == 0,
				"ByteString.Compare()",
				"Comparing substrings: 'The [US]' == 'US' (count=0)");

			//Test constant CString buffer with constant String type

			Testcase.Test (ByteString.Compare (ptr1, "SK") != 0,
				"ByteString.Compare()",
				"Comparing byte* and string constant: 'US' != const 'SK'");

			Testcase.Test (ByteString.Compare (ptr1, "US") == 0,
				"ByteString.Compare()",
				"Comparing byte* and string constant: 'US' == const 'US'");

			Testcase.Test (ByteString.Compare (longer, 4, "US", 0, 2) == 0,
				"ByteString.Compare()",
				"Comparing byte* substring and string constant: 'The [US]' == const 'US'");
			Testcase.Test (ByteString.Compare (longer, 4, "US", 0, 2) == 0,
				"ByteString.Compare()",
				"Comparing byte* substring and string constant: 'The [US]' == const 'US' (count=2)");

			Testcase.Test (ByteString.Compare (longer, 4, "US", 0, 0) == 0,
				"ByteString.Compare()",
				"Comparing byte* substring and string constant: 'The [US]' == const 'US' (count=0)");

			//Test that constant String is working properly
			const string str1 = "US";
			const string str2 = "SK";
			Testcase.Test ((byte) str1 [0] == (byte) 'U',
				"ByteString",
				"Testing string constants: (byte)\"US\" [0] == (byte)'U'");

			Testcase.Test ((byte) str1 [1] == (byte) 'S',
				"ByteString",
				"Testing string constants: (byte)\"US\" [1] == (byte)'S'");

			Testcase.Test (str1.Length == 2,
				"ByteString",
				"Testing string constant length: \"US\".Length == 2");

			Testcase.Test ((byte) str1 [1] == (byte) str2 [0],
				"ByteString",
				"Testing string constants: (byte)\"US\" [1] == (byte)\"SK\" [0]");

			// FIXME: This testcase does not test ByteString, but the string constants.... where should it go?

			Testcase.Test ("\n".Length == 1,
				"ByteString",
				"Testing string constants: Length of newline (\"\\n\".Length == 1");
		}

		#endregion
	}
}
