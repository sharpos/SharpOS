/*
 * SharpOS.EIC/String.cs
 * N:SharpOS.EIC
 *
 * (C) 2007 William Lahti. This software is licensed under the terms of the GNU General
 * Public License.
 *
 */

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
		
		/**
			<summary>
				Concatenates <paramref name="count" /> bytes of 
				the string in buffer <paramref name="src" /> 
				to the end of the string in buffer 
				<paramref name="buffer" />.
			</summary>
			<remarks>
				
			</remarks>
		*/
		public static void Concat (byte *buffer, int size, byte *src, int count)
		{
			int c = count;
			int start = Length (buffer);
			
			if (c <= 0)
				c = Length (src);
			
			Kernel.Assert (*(buffer+size) == 0,
				Kernel.String ("Concat: warning, buffer may not have been allocated by ByteString"));
			
			Kernel.Assert (start + c < (size+1),
				Kernel.String ("Concat: buffer is too small"));
			
			Copy (buffer, size, src, start, c);
			*(buffer+start+c) = 0;
		}
		
		public static void Copy (byte *buffer, int size, byte *src, int index, int count)
		{
			Kernel.Assert (index + count < size+1,
				Kernel.String ("Copy: buffer is too small"));
			
			byte *ptr = buffer + index;
			byte *sptr = src;
			
			for (int x = index; x < index+count; ++x) {
				*ptr = *sptr;
				++ptr;
				++sptr;
			}
		}
		
		public static int Compare (byte *a, byte *b, int count)
		{
			int c = count;
			int al = Length (a), bl = Length (b);
			
			if (count == 0 && al != bl) {
				return al - bl;
			} else if (count != 0 && (count > al || count > bl)) {
				return al - bl;
			}
			
			if (c == 0)
				c = al;

			TextMode.Write (Kernel.String ("comparing: '"));
			TextMode.WriteSubstring (a, 0, count);
			TextMode.Write (Kernel.String ("' to '"));
			TextMode.WriteSubstring (b, 0, count);
			TextMode.WriteLine (Kernel.String ("'"));
			
			TextMode.WriteLine (Kernel.String ("count: "), c);
			
			for (int x = 0; x < c; ++x) {
			
				if (x >= c) {
					TextMode.WriteLine (Kernel.String ("here"));
					break;
				}

				//TextMode.WriteLine (Kernel.String ("x :"), x);
				TextMode.WriteLine (Kernel.String ("c :"), c);
				//TextMode.Write (Kernel.String ("comparing: '"));
				//TextMode.WriteSubstring (a, x, 1);
				//TextMode.Write (Kernel.String ("' to '"));
				//TextMode.WriteSubstring (b, x, 1);
				//TextMode.WriteLine (Kernel.String ("'"));
	
				if (a [x] != b [x]) {
					return a [x] - b [x];
				}
			}

			TextMode.WriteLine (Kernel.String ("done"));
			return 0;
		}

		public static void Test1 ()
		{
			byte *ptr1 = Kernel.String ("US"), ptr2 = Kernel.String ("SK");

			if (ByteString.Compare (ptr1, ptr2, 2) == 0)
				TextMode.WriteLine (Kernel.String ("ByteString.Compare(): test fail: 'US' != 'SK'"));
			else
				TextMode.WriteLine (Kernel.String ("ByteString.Compare(): test pass: 'US' != 'SK'"));

			if (ByteString.Compare (ptr1, ptr1, 2) == 0)
				TextMode.WriteLine (Kernel.String ("ByteString.Compare(): test pass: 'US' == 'US'"));
			else
				TextMode.WriteLine (Kernel.String ("ByteString.Compare(): test fail: 'US' == 'US'"));
		}
	}
}
