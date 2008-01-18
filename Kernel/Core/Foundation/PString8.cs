//
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
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

	/// <summary>
	/// Represents a length-prefixed C-style string (null-terminated). This type
	/// can be used as a string buffer,
	/// </summary>
	public unsafe struct PString8 {

		int length;
		int capacity;
		byte firstChar;

		#region Wrap

		/// <summary>
		/// Wraps a data buffer into a PString8 pointer and initializes
		/// the Capacity field to <paramref name="bufferSize" /> minus
		/// the size of the PString8 structure. <paramref name="bufferSize" />
		/// should be the entire size of the allocation.
		/// </summary>
		public static PString8* Wrap (void* buffer, int bufferSize)
		{
			PString8* wrapped;

			if (bufferSize < sizeof (PString8))
				return null;

			wrapped = (PString8*) buffer;
			wrapped->capacity = bufferSize - sizeof (PString8) + 1;
			wrapped->length = 0;
			wrapped->firstChar = 0;

			return wrapped;
		}

		#endregion
		#region Internal

		/// <summary>
		/// Internal common implementation of Concat(). The <paramref name="strLen" />
		/// parameter is the length of the string in buffer <paramref name="str" />,
		/// determined using the fastest possible method for the string type. This
		/// causes slightly faster performance for string types that store the length
		/// of the string (as opposed to null termination).
		/// </summary>
		int Concat (byte* str, int strLen, int offset, int len)
		{
			if (len == 0)
				len = strLen - offset;

			Diagnostics.Assert (offset + len <= strLen, "PString8.Concat(): offset + len <= strLen");

			for (int x = offset; x < strLen && (x - offset) < len; ++x)
				Concat (str [x]);

			return len;
		}

		#endregion
		#region Properties

		/// <summary>
		/// Gets the length of the string.
		/// </summary>
		public int Length
		{
			get
			{
				return length;
			}
		}

		/// <summary>
		/// Gets the capacity of the string.
		/// </summary>
		public int Capacity
		{
			get
			{
				return capacity;
			}
		}

		/// <summary>
		/// If <paramref name="boundsCheck" /> is true, makes sure
		/// <paramref name="index" /> is in bounds, then gets the
		/// character <paramref name="index" /> from the string.
		/// </summary>
		public byte GetChar (int index, bool boundsCheck)
		{
			if (boundsCheck) {
				Diagnostics.Assert (index >= 0 && index < Length,
					"PString8.get_Indexer(): index out of bounds");
			}

			return Pointer [index];
		}

		public void SetChar (int index, byte value, bool boundsCheck)
		{
			if (boundsCheck) {
				Diagnostics.Assert (index >= 0 && index < Length,
					"PString8.set_Indexer(): index out of bounds");

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

		/// <summary>
		/// Gets the null-terminated byte* pointer.
		/// </summary>
		public byte* Pointer
		{
			get
			{
				byte* ptr;

				// HACK

				fixed (byte* tp = &firstChar)
					ptr = tp;

				return ptr;
			}
		}

		#endregion
		#region Clear() family

		public void Clear ()
		{
			firstChar = 0;
			this.length = 0;
		}

		#endregion
		#region Concat() family

		/// <summary>
		///
		/// </summary>
		public void ConcatLine ()
		{
			Concat ('\n');
		}

		/// <summary>
		/// Concatenates from <paramref name="str" /> the
		/// characters at indices <paramref name="offset" /> to
		/// <paramref name="offset" /> + <paramref name="len" />.
		/// </summary>
		public int Concat (PString8* str, int offset, int len)
		{
			return Concat (str->Pointer, str->Length, offset, len);
		}

		/// <summary>
		/// Concatenates from <paramref name="str" /> the
		/// characters at indices <paramref name="offset" /> to
		/// <paramref name="offset" /> + <paramref name="len" />.
		/// </summary>
		public int Concat (CString8* str, int offset, int len)
		{
			return Concat (str->Pointer, str->Length, offset, len);
		}

		/// <summary>
		/// Concatenates from <paramref name="str" /> the
		/// characters at indices <paramref name="offset" /> to
		/// <paramref name="offset" /> + <paramref name="len" />.
		/// </summary>
		public int Concat (string str, int offset, int len)
		{
			if (len == 0)
				len = str.Length - offset;

			if (len + offset > str.Length)
				return -1;

			for (int x = 0; x < str.Length; ++x)
				Concat ((byte) str [x]);

			return len;
		}

		public int Concat (byte* str, int len)
		{
			return Concat (str, len, 0, len);
		}

		public int Concat (byte* str)
		{
			return Concat (str, ByteString.Length (str), 0, ByteString.Length (str));
		}

		/// <summary>
		/// Concatenates <paramref name="str" />.
		/// </summary>
		public int Concat (PString8* str)
		{
			return Concat (str, 0, 0);
		}

		/// <summary>
		/// Concatenates <paramref name="str" />.
		/// </summary>
		public int Concat (CString8* str)
		{
			return Concat (str, 0, 0);
		}

		/// <summary>
		/// Concatenates <paramref name="str" />.
		/// </summary>
		public int Concat (string str)
		{
			return Concat (str, 0, 0);
		}

		/// <summary>
		/// Concatenates <paramref name="character" />
		/// </summary>
		public bool Concat (byte character)
		{
			if (length >= capacity)
				return false;

			Pointer [length++] = character;

			return true;
		}

		public int Concat (int number, bool hex)
		{
			int ret = 0;
			CString8 *str = null;

			str = Convert.ToString (number, hex);
			ret = Concat (str);
			MemoryManager.Free(str);

			return ret;
		}

		public int Concat (int number)
		{
			return Concat (number, false);
		}

		#endregion
		#region Compare() family

		/// <summary>
		/// Compares <paramref name="count" /> characters of the
		/// string against <paramref name="str" />.
		/// </summary>
		public int Compare (byte* str, int count)
		{
			return ByteString.Compare (Pointer, str, count);
		}

		/// <summary>
		/// Compares <paramref name="count" /> characters of the
		/// string against <paramref name="str" />.
		/// </summary>
		public int Compare (PString8* str, int count)
		{
			return ByteString.Compare (Pointer, str->Pointer, count);
		}

		/// <summary>
		/// Compares <paramref name="count" /> characters of the
		/// string against <paramref name="str" />.
		/// </summary>
		public int Compare (CString8* str, int count)
		{
			return ByteString.Compare (Pointer, str->Pointer, count);
		}

		/// <summary>
		/// Compares <paramref name="count" /> characters of the
		/// string against <paramref name="str" />.
		/// </summary>
		public int Compare (string str, int count)
		{
			return ByteString.Compare (Pointer, str, count);
		}

		/// <summary>
		/// Compares the string against <paramref name="str" />.
		/// </summary>
		public int Compare (CString8* str)
		{
			return Compare (str, 0);
		}

		/// <summary>
		/// Compares the string against <paramref name="str" />.
		/// </summary>
		public int Compare (PString8* str)
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

		#endregion

		#region Testcases

		internal static void __RunTests ()
		{
			__Test1 ();
		}

		public static void __Test1 ()
		{
		}

		#endregion
	}
}
