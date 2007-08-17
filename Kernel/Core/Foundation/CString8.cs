// 
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using SharpOS;
using SharpOS.Memory;
using SharpOS.ADC;

namespace SharpOS.Foundation {

	/// <summary>
	/// Represents a C-style (null-terminated) string.
	/// </summary>
	public unsafe struct CString8 {

		public byte firstChar;

		#region Properties
		
		/// <summary>
		/// Gets the length of the string.
		/// </summary>
		public int Length {
			get {
				return ByteString.Length (Pointer);
			}
		}

		/// <summary>
		/// Gets the null-terminated byte pointer for the string.
		/// </summary>
		public byte *Pointer {
			get {
				byte *ptr;
				
				// HACK

				fixed (byte *tp = &firstChar)
					ptr = tp;
				
				return ptr;
			}
		}
		
		/// <summary>
		/// If <paramref name="boundsCheck" /> is true, makes sure
		/// <paramref name="index" /> is within bounds, then gets the
		/// character at <paramref name="index" /> from the string..
		/// </summary>
		public byte GetChar (int index, bool boundsCheck)
		{
			if (boundsCheck) {
				Kernel.Assert (index >= 0 && index < Length,
					"CString8.get_Indexer(): index out of bounds");
			}
			
			return Pointer [index];
		}
		
		public void SetChar (int index, byte value, bool boundsCheck)
		{
			if (boundsCheck) {
				Kernel.Assert (index >= 0 && index < Length,
					"CString8.get_Indexer(): index out of bounds");
			}
			
			Pointer [index] = value;
		}

		public byte GetChar (int index)
		{
			return GetChar (index, true);
		}
		
		public void SetChar (int index, byte value)
		{
			SetChar (index, value, true);
		}
		
		#endregion
		#region Internal

		int IndexOf (int from, byte *substr, int substrLen, int offset, int count)
		{
			Kernel.Assert (from >= 0 && from < Length,
				"CString8.IndexOf(): argument `from' is out of range");
			Kernel.Assert (substr != null,
				"CString8.IndexOf(): argument `substr' is null");
			Kernel.Assert (offset >= 0 && offset < substrLen,
				"CString8.IndexOf(): argument `offset' is out of range");
			Kernel.Assert (count >= 0 && from + count < Length && from + count < substrLen,
				"CString8.IndexOf(): argument `count' is out of range");
				
			if (count == 0)
				count = Length - substrLen - offset;
			
			for (int x = from; x < from + count; ++x) {
				
				if (Compare (x, substr, offset, count) == 0)
					return x;
			}

			return -1;
		}

		#endregion
		#region IndexOf() family

		public int IndexOf (int from, string substr, int offset, int count)
		{
			Kernel.Assert (from >= 0 && from < Length,
				"CString8.IndexOf(): argument `from' is out of range");
			Kernel.Assert (substr != null,
				"CString8.IndexOf(): argument `substr' is null");
			Kernel.Assert (offset >= 0 && offset < substr.Length,
				"CString8.IndexOf(): argument `offset' is out of range");
			Kernel.Assert (count >= 0 && from + count < Length && from + count < substr.Length,
				"CString8.IndexOf(): argument `count' is out of range");
				
			if (count == 0)
				count = Length - substr.Length - offset;
			
			for (int x = from; x < from + count; ++x) {
				
				if (Compare (x, substr, offset, count) == 0)
					return x;
			}

			return -1;
		}
		
		public int IndexOf (int from, CString8 *substr, int offset, int count)
		{
			return IndexOf (from, substr->Pointer, substr->Length, offset, count);
		}

		public int IndexOf (int from, PString8 *substr, int offset, int count)
		{
			return IndexOf (from, substr->Pointer, substr->Length, offset, count);
		}
		
		public int IndexOf (int from, byte *substr, int offset, int count)
		{
			return IndexOf (from, substr, ByteString.Length (substr), offset, count);
		}

		public int IndexOf (int from, string substr, int count)
		{
			return IndexOf (from, substr, 0, count);
		}
		
		public int IndexOf (int from, CString8 *substr, int count)
		{
			return IndexOf (from, substr, 0, count);
		}

		public int IndexOf (int from, PString8 *substr, int count)
		{
			return IndexOf (from, substr, 0, count);
		}
		
		public int IndexOf (int from, byte *substr, int count)
		{
			return IndexOf (from, substr, 0, count);
		}
		
		public int IndexOf (int from, string substr)
		{
			return IndexOf (from, substr, 0, 0);
		}
		
		public int IndexOf (int from, CString8 *substr)
		{
			return IndexOf (from, substr, 0, 0);
		}

		public int IndexOf (int from, PString8 *substr)
		{
			return IndexOf (from, substr, 0, 0);
		}
		
		public int IndexOf (int from, byte *substr)
		{
			return IndexOf (from, substr, 0, 0);
		}
		
		public int IndexOf (string substr)
		{
			return IndexOf (0, substr, 0, 0);
		}
		
		public int IndexOf (CString8 *substr)
		{
			return IndexOf (0, substr, 0, 0);
		}

		public int IndexOf (PString8 *substr)
		{
			return IndexOf (0, substr, 0, 0);
		}
		
		public int IndexOf (byte *substr)
		{
			return IndexOf (0, substr, 0, 0);
		}
		
		#endregion
		#region Compare() family
		
		/// <summary>
		/// Compares <paramref name="count" /> characters of the
		/// string against <paramref name="str" />.
		/// </summary>
		public int Compare (int from, CString8 *str, int offset, int count)
		{
			return ByteString.Compare (Pointer, from, str->Pointer, offset, count);
		}

		/// <summary>
		/// Compares <paramref name="count" /> characters of the
		/// string against <paramref name="str" />.
		/// </summary>
		public int Compare (int from, PString8 *str, int offset, int count)
		{
			return ByteString.Compare (Pointer, from, str->Pointer, offset, count);
		}

		/// <summary>
		/// Compares <paramref name="count" /> characters of the
		/// string against <paramref name="str" />.
		/// </summary>
		public int Compare (int from, string str, int offset, int count)
		{
			return ByteString.Compare (Pointer, from, str, offset, count);
		}

		/// <summary>
		/// Compares <paramref name="count" /> characters of the
		/// string against <paramref name="str" />.
		/// </summary>
		public int Compare (int from, byte *str, int offset, int count)
		{
			return ByteString.Compare (Pointer, from, str, offset, count);
		}

		/// <summary>
		/// Compares <paramref name="count" /> characters of the
		/// string against <paramref name="str" />.
		/// </summary>
		public int Compare (CString8 *str, int offset, int count)
		{
			return ByteString.Compare (Pointer, 0, str->Pointer, offset, count);
		}

		/// <summary>
		/// Compares <paramref name="count" /> characters of the
		/// string against <paramref name="str" />.
		/// </summary>
		public int Compare (PString8 *str, int offset, int count)
		{
			return ByteString.Compare (Pointer, 0, str->Pointer, offset, count);
		}

		/// <summary>
		/// Compares <paramref name="count" /> characters of the
		/// string against <paramref name="str" />.
		/// </summary>
		public int Compare (string str, int offset, int count)
		{
			return ByteString.Compare (Pointer, 0, str, offset, count);
		}

		/// <summary>
		/// Compares <paramref name="count" /> characters of the
		/// string against <paramref name="str" />.
		/// </summary>
		public int Compare (byte *str, int offset, int count)
		{
			return ByteString.Compare (Pointer, 0, str, offset, count);
		}

		/// <summary>
		/// Compares <paramref name="count" /> characters of the
		/// string against <paramref name="str" />.
		/// </summary>
		public int Compare (CString8 *str, int count)
		{
			return ByteString.Compare (Pointer, 0, str->Pointer, 0, count);
		}

		/// <summary>
		/// Compares <paramref name="count" /> characters of the
		/// string against <paramref name="str" />.
		/// </summary>
		public int Compare (PString8 *str, int count)
		{
			return ByteString.Compare (Pointer, 0, str->Pointer, 0, count);
		}

		/// <summary>
		/// Compares <paramref name="count" /> characters of the
		/// string against <paramref name="str" />.
		/// </summary>
		public int Compare (string str, int count)
		{
			return ByteString.Compare (Pointer, 0, str, 0, count);
		}

		/// <summary>
		/// Compares <paramref name="count" /> characters of the
		/// string against <paramref name="str" />.
		/// </summary>
		public int Compare (byte *str, int count)
		{
			return ByteString.Compare (Pointer, 0, str, 0, count);
		}

		/// <summary>
		/// Compares the string against <paramref name="str" />.
		/// </summary>
		public int Compare (CString8 *str)
		{
			return Compare (str, 0);
		}
		
		/// <summary>
		/// Compares the string against <paramref name="str" />.
		/// </summary>
		public int Compare (PString8 *str)
		{
			return Compare (str, 0);
		}
		
		/// <summary>
		/// Compares the string against <paramref name="str" />.
		/// </summary>
		public int Compare (string str)
		{
			return Compare (str, 0);
		}

		/// <summary>
		/// Compares the string against <paramref name="str" />.
		/// </summary>
		public int Compare (byte *str)
		{
			return Compare (str, 0);
		}

		#endregion
	}
}
