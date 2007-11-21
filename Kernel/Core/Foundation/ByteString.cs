// 
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS;
using SharpOS.Memory;
using SharpOS.ADC;

namespace SharpOS.Foundation {

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
			Kernel.Assert (src != null, "ByteString.GetBytes (): argument `src' is null");
			Kernel.Assert (dst != null, "ByteString.GetBytes (): argument `dst' is null");
			Kernel.Assert (size > 0 && size < ByteString.Length (dst),
				"ByteString.GetBytes (): argument `size' is out of range");
			Kernel.Assert (size >= src.Length, "ByteString.GetBytes (): buffer is too small");
			
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
			
			Kernel.Assert (*(buffer+size) == 0, "Concat: warning, buffer may not have been allocated by ByteString");
			
			Kernel.Assert (start + c < (size+1), "Concat: buffer is too small");
			
			Copy (buffer, size, src, start, c);
			*(buffer+start+c) = 0;
		}

		#endregion
		#region Copy() family
		
		public static void Copy (byte *buffer, int size, byte *src, int index, int count)
		{
			Kernel.Assert (index + count < size+1, "Copy: buffer is too small");
			
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
			int al = Length (a), bl = Length (b);
			
			if (count == 0 && al != bl)
				return al - bl;
			else if (count != 0 && (aFrom + count > al || bFrom + count > bl))
				return al - bl;
			
			if (c == 0)
				c = al;

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
			int al = Length (a);
			
			if (count == 0 && al != b.Length) {
				return al - b.Length;
			} else if (count != 0 && (count > al || count > b.Length)) {
				return al - b.Length;
			}
			
			if (c == 0)
				c = al;

			for (int x = 0; x < c; ++x) {
			
				if (x >= c) {
					break;
				}
	
				if (a [aFrom + x] != (byte) b [bFrom + x]) {
					return (int)a [aFrom + x] - b [bFrom + x];
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

		public static void __RunTests ()
		{
			__Test1 ();
		}
		
		public static void __Test1 ()
		{
			byte *ptr1 = (byte*)Kernel.CString ("US"), ptr2 = (byte*)Kernel.CString ("SK");

			if (ByteString.Compare (ptr1, ptr2, 2) == 0)
				TextMode.WriteLine ("ByteString.Compare(): test fail: 'US' != 'SK'");
			else
				TextMode.WriteLine ("ByteString.Compare(): test pass: 'US' != 'SK'");

			if (ByteString.Compare (ptr1, ptr1, 2) == 0)
				TextMode.WriteLine ("ByteString.Compare(): test pass: 'US' == 'US'");
			else
				TextMode.WriteLine ("ByteString.Compare(): test fail: 'US' == 'US'");
		}

		#endregion
	}
}
