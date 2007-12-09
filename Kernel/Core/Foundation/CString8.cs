// 
// (C) 2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
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
				Diagnostics.Assert (index >= 0 && index < Length,
					"CString8.get_Indexer(): index out of bounds");
			}
			
			return Pointer [index];
		}
		
		public void SetChar (int index, byte value, bool boundsCheck)
		{
			if (boundsCheck) {
				Diagnostics.Assert (index >= 0 && index < Length,
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
			Diagnostics.Assert (from >= 0 && from < Length,
				"CString8.IndexOf(): argument `from' is out of range");
			Diagnostics.Assert (substr != null,
				"CString8.IndexOf(): argument `substr' is null");
			Diagnostics.Assert (offset >= 0 && offset < substrLen,
				"CString8.IndexOf(): argument `offset' is out of range");
			Diagnostics.Assert (count >= 0 && from + count < Length && from + count < substrLen,
				"CString8.IndexOf(): argument `count' is out of range");
				
			if (count == 0)
				count = Length - substrLen - offset;
			
			for (int x = from; x < from + count; ++x) {

				if (Compare (x, substr, offset, substrLen) == 0)
					return x;
			}

			return -1;
		}

		#endregion
		#region IndexOf() family

		public int IndexOf (int from, string substr, int offset, int count)
		{
			Diagnostics.Assert (from >= 0 && from < Length,
				"CString8.IndexOf(): argument `from' is out of range");
			Diagnostics.Assert (substr != null,
				"CString8.IndexOf(): argument `substr' is null");
			Diagnostics.Assert (offset >= 0 && offset < substr.Length,
				"CString8.IndexOf(): argument `offset' is out of range");
			Diagnostics.Assert (count >= 0 && from + count < Length && from + count < substr.Length,
				"CString8.IndexOf(): argument `count' is out of range");
				
			if (count == 0)
				count = Length - substr.Length - offset;
			
			for (int x = from; x < from + count; x++) {

				if (Compare (x, substr, offset, substr.Length) == 0)
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

        /// <summary>
        /// Generates a new CString8 instance that is identical to this one,
        /// minus any leading or trailing whitespace
        /// </summary>
        /// <returns>A pointer to a new CString8 instance</returns>
        public CString8* Trim()
        {
            byte* firstNonWhiteSpace = null;
            byte* lastNonWhiteSpace = null;

            byte* caret = this.Pointer;
            for (; (*caret) != '\0'; caret++)
            {
                if (firstNonWhiteSpace == null)
                {
                    if (!ASCII.IsWhiteSpace(*caret))
                        firstNonWhiteSpace = caret;
                    continue;
                }
                else
                {
                    if (ASCII.IsWhiteSpace(*caret)
                        && !ASCII.IsWhiteSpace(*(caret - 1)))
                        lastNonWhiteSpace = caret - 1;

                    if (lastNonWhiteSpace != null
                        && !ASCII.IsWhiteSpace(*caret))
                        lastNonWhiteSpace = null;
                }
            }
            if (lastNonWhiteSpace == null)
                lastNonWhiteSpace = caret - 1;

            if (firstNonWhiteSpace == null)
            {   //whole string needs to be filtered out...
                //So we generate an empty string and return it
                CString8* result = (CString8*)SharpOS.ADC.MemoryManager.Allocate(1);
                *((byte*)result) = (byte)'\0';
                return result;
            }
            else
            {   //we get to get part (which could be all) of the string...
                long length = (lastNonWhiteSpace - firstNonWhiteSpace) + 1;
                byte* result = (byte*)SharpOS.ADC.MemoryManager.Allocate((uint)length + 1);
                for (caret = firstNonWhiteSpace; caret <= lastNonWhiteSpace; caret++)
                {
                    result[caret - firstNonWhiteSpace] = *caret;
                }
                result[caret - firstNonWhiteSpace] = (byte)'\0';

                return (CString8*)result;
            }
        }

        public CString8* Substring(int index)
        {
            int l = this.Length;
            Diagnostics.Assert(index >= 0,
                "CString8.Substring(int): Parameter 'index' is outside of the valid range");
            Diagnostics.Assert(index < l,
                "CString8.Substring(int): Parameter 'index' is outside of the valid range");

            int count = l - index;

            return Substring_INTERNAL(index, count);
        }

        public CString8* Substring(int index, int count)
        {
            int l = this.Length;

            Diagnostics.Assert(index >= 0,
                "CString8.Substring(int,int): Parameter 'index' is outside of the valid range");
            Diagnostics.Assert(index < l,
                "CString8.Substring(int,int): Parameter 'index' is outside of the valid range");
            Diagnostics.Assert(count >= 0,
                "CString8.Substring(int,int): Parameter 'count' is outside of the valid range");
            Diagnostics.Assert((count + index) <= l,
                "CString8.Substring(int,int): Parameter 'count' is outside of the valid range");

            return Substring_INTERNAL(index, count);
        }

        private CString8* Substring_INTERNAL(int index, int count)
        {
            if (count == 0)
                return CString8.CreateEmpty();

            //TextMode.Write("Substring(int): [index,count]="); TextMode.Write(index, false); TextMode.Write(","); TextMode.Write((int)count, false); TextMode.WriteLine();

            byte* rslt = (byte*)SharpOS.ADC.MemoryManager.Allocate((uint)count + 1);
            byte* thisPtr = this.Pointer;
            Diagnostics.Assert(rslt != thisPtr, "CString8.Substring_INTERNAL(): Insane memory allocation detected!");
            
            for (int i = index; i < (index + count); i++)
            {
                rslt[i - index] = thisPtr[i];
            }
            rslt[count] = (byte)'\0';

            return (CString8*)rslt;
        }



        public static CString8* CreateEmpty()
        {
            byte* rslt = (byte*)SharpOS.ADC.MemoryManager.Allocate(1);
            rslt[0] = (byte)'\0';
            return (CString8*)rslt;
        }

        public static CString8* Copy(string str)
        {
            uint l = (uint)str.Length;

            byte* result = (byte*)ADC.MemoryManager.Allocate(l + 1);

            for (int i = 0; i < l; i++)
                result[i] = (byte)str[i];
            result[l] = (byte)'\0';

            return (CString8*)result;
        }

        public static CString8* Copy(byte* original)
        {
            return Copy((CString8*)original);
        }

        public static CString8* Copy(CString8* original)
        {
            uint l = (uint)original->Length;
            byte* originalPtr = original->Pointer;
            byte* result = (byte*)ADC.MemoryManager.Allocate(l + 1);

            for (int i = 0; i < l; i++)
                result[i] = originalPtr[i];
            result[l] = (byte)'\0';

            return (CString8*)result;
        }
    }
}