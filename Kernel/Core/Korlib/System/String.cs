//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//
// Some source code has been adapted from the Mono project under the following 
// copyright and license:
//
// (C) 2001 Ximian, Inc.  http://www.ximian.com
// Copyright (C) 2004-2005 Novell (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Runtime.InteropServices;
using SharpOS.AOT.Attributes;
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.Foundation;

namespace InternalSystem
{
	[StructLayout (LayoutKind.Sequential)]
	[TargetNamespace ("System")]
	public class String :
		InternalSystem.Object,
		System.Collections.IEnumerable
	{
		private int length;
		private char firstChar;

		public static readonly string Empty = "";

		internal unsafe char* _GetBuffer ()
		{
			fixed (char* p = &this.firstChar) {
				return p;
			}
		}

		public int Length
		{
			get
			{
				return this.length;
			}
		}

		public char this[int index]
		{
			[Label ("System.String.get_Chars(System.Int32)")]
			get
			{
				return GetChar (index);
			}
		}

		public override string ToString ()
		{
			object o = (object)this;
			return (string)o;
		}

		private unsafe char GetChar (int index)
		{
			if (index < 0)
				throw new System.ArgumentOutOfRangeException ("index is less than zero.");
			if (index >= this.length)
				throw new System.ArgumentOutOfRangeException ("index specifies a position that is not within this string.");

			fixed (char* p = &this.firstChar) {
				return p[index];
			}
		}

		private unsafe char FastGetChar (int index)
		{
			fixed (char* p = &this.firstChar) {
				return p[index];
			}
		}

		public static void __RunTests ()
		{
			__Test1 ();
		}

		public static void __Test1 ()
		{
			string str = "Hello";
			string str2 = "US";
			string str3 = "Longer String Than Most";

			Testcase.Test ((str.Length == 5) && (str2.Length == 2) && (str3.Length == 23),
				"System.String", "Length test");
		}

		private static unsafe bool CompareChars (InternalSystem.String a, InternalSystem.String b)
		{
			fixed (char* pa = &a.firstChar) {
				fixed (char* pb = &b.firstChar) {
					for (int i = 0; i < a.Length; ++i) {
						if (pa[i] != pb[i])
							return false;
					}
				}
			}

			return true;
		}

		[Label ("System.String.op_Equality(System.String,System.String)")]
		public static bool operator == (InternalSystem.String a, InternalSystem.String b)
		{
			if (a.length != b.length)
				return false;
			return String.CompareChars (a, b);
		}

		[Label ("System.String.op_Inequality(System.String,System.String)")]
		public static bool operator != (InternalSystem.String a, InternalSystem.String b)
		{
			return !(a == b);
		}

		public System.Collections.IEnumerator GetEnumerator ()
		{
			return new CharEnumerator (this);
		}

		public bool Equals (System.String i)
		{
			return ((InternalSystem.String)(object)i) == this;
		}

		public override bool Equals (object o)
		{
			//if (!(o is String))
			//	return false;

			String other = (String)o;
			return other == this;
		}

		[Label ("System.String.Concat(System.String,System.String)")]
		public static string Concat (InternalSystem.String a, InternalSystem.String b)
		{
			InternalSystem.String result = InternalAllocateStr (a.length + b.length);
			unsafe {
				char* ptrres = result._GetBuffer ();
				char* ptr = a._GetBuffer ();
				for (int i = 0; i < a.length; i++) {
					*ptrres = *ptr;
					ptrres++;
					ptr++;
				}
				ptr = b._GetBuffer ();
				for (int i = 0; i < b.length; i++) {
					*ptrres = *ptr;
					ptrres++;
					ptr++;
				}
			}
			return result as object as string;
		}

		[Label ("System.String.Concat(System.String,System.String,System.String)")]
		public static string Concat (InternalSystem.String a, InternalSystem.String b, InternalSystem.String c)
		{
			InternalSystem.String result = InternalAllocateStr (a.length + b.length + c.length);
			unsafe {
				char* ptrres = result._GetBuffer ();
				char* ptr = a._GetBuffer ();
				for (int i = 0; i < a.length; i++) {
					*ptrres = *ptr;
					ptrres++;
					ptr++;
				}
				ptr = b._GetBuffer ();
				for (int i = 0; i < b.length; i++) {
					*ptrres = *ptr;
					ptrres++;
					ptr++;
				}
				ptr = c._GetBuffer ();
				for (int i = 0; i < c.length; i++) {
					*ptrres = *ptr;
					ptrres++;
					ptr++;
				}
			}
			return result as object as string;
		}

		[Label ("System.String.Concat(System.String,System.String,System.String,System.String)")]
		public static string Concat (InternalSystem.String a, InternalSystem.String b, InternalSystem.String c, InternalSystem.String d)
		{
			InternalSystem.String result = InternalAllocateStr (a.length + b.length + c.length + d.length);
			unsafe {
				char* ptrres = result._GetBuffer ();
				char* ptr = a._GetBuffer ();
				for (int i = 0; i < a.length; i++) {
					*ptrres = *ptr;
					ptrres++;
					ptr++;
				}
				ptr = b._GetBuffer ();
				for (int i = 0; i < b.length; i++) {
					*ptrres = *ptr;
					ptrres++;
					ptr++;
				}
				ptr = c._GetBuffer ();
				for (int i = 0; i < c.length; i++) {
					*ptrres = *ptr;
					ptrres++;
					ptr++;
				}
				ptr = d._GetBuffer ();
				for (int i = 0; i < d.length; i++) {
					*ptrres = *ptr;
					ptrres++;
					ptr++;
				}
			}
			return result as object as string;
		}

		[Label ("System.String.Concat(System.String[])")]
		public static string Concat (InternalSystem.String[] strings)
		{
			int length = 0;

			foreach (String s in strings)
				length = length + s.Length;

			InternalSystem.String result = InternalAllocateStr (length);

			unsafe {
				char* ptrres = result._GetBuffer ();

				foreach (String s in strings) {
					char* ptr = s._GetBuffer ();
					for (int i = 0; i < s.length; i++) {
						*ptrres = *ptr;
						ptrres++;
						ptr++;
					}
				}
			}
			return result as object as string;
		}

		public string Substring (int startIndex)
		{
			if (startIndex == 0)
				return this as object as string;

			if (startIndex < 0 || startIndex > this.length)
				throw new System.ArgumentOutOfRangeException ("startIndex");

			int newlen = this.length - startIndex;
			InternalSystem.String result = InternalAllocateStr (newlen);

			unsafe {
				char* ptrres = result._GetBuffer ();
				char* ptr = this._GetBuffer ();

				ptr = ptr + startIndex;

				for (int i = 0; i < newlen; i++) {
					*ptrres = *ptr;
					ptrres++;
					ptr++;
				}
			}

			return result as object as string;
		}

		public string Substring (int startIndex, int length)
		{
			if (length < 0)
				throw new System.ArgumentOutOfRangeException ("length", "< 0");
			if (startIndex < 0)
				throw new System.ArgumentOutOfRangeException ("startIndex", "< 0");
			if (startIndex > this.length - length)
				throw new System.ArgumentOutOfRangeException ("startIndex + length > this.length");

			if (length == 0)
				return String.Empty;

			InternalSystem.String result = InternalAllocateStr (length);

			unsafe {
				char* ptrres = result._GetBuffer ();
				char* ptr = this._GetBuffer ();

				ptr = ptr + startIndex;

				for (int i = 0; i < length; i++) {
					*ptrres = *ptr;
					ptrres++;
					ptr++;
				}
			}

			return result as object as string;
		}

		public int IndexOf (char value)
		{
			if (this.length == 0)
				return -1;

			return IndexOfImpl (value, 0, this.length);
		}

		public int IndexOf (char value, int startIndex)
		{
			return IndexOf (value, startIndex, this.length - startIndex);
		}

		public int IndexOf (char value, int startIndex, int count)
		{
			if (startIndex < 0)
				throw new System.ArgumentOutOfRangeException ("startIndex", "< 0");
			if (count < 0)
				throw new System.ArgumentOutOfRangeException ("count", "< 0");
			if (startIndex > this.length - count)
				throw new System.ArgumentOutOfRangeException ("startIndex + count > this.length");

			if ((startIndex == 0 && this.length == 0) || (startIndex == this.length) || (count == 0))
				return -1;

			return IndexOfImpl (value, startIndex, count);
		}

		public int IndexOfAny (char[] anyOf)
		{
			if (anyOf == null)
				throw new System.ArgumentNullException ("anyOf");
			if (this.length == 0)
				return -1;

			return IndexOfAnyImpl (anyOf, 0, this.length);
		}

		public int IndexOfAny (char[] anyOf, int startIndex)
		{
			if (anyOf == null)
				throw new System.ArgumentNullException ("anyOf");
			if (startIndex < 0 || startIndex > this.length)
				throw new System.ArgumentOutOfRangeException ("startIndex");

			return IndexOfAnyImpl (anyOf, startIndex, this.length - startIndex);
		}

		public int IndexOfAny (char[] anyOf, int startIndex, int count)
		{
			if (anyOf == null)
				throw new System.ArgumentNullException ("anyOf");
			if (startIndex < 0)
				throw new System.ArgumentOutOfRangeException ("startIndex", "< 0");
			if (count < 0)
				throw new System.ArgumentOutOfRangeException ("count", "< 0");
			if (startIndex > this.length - count)
				throw new System.ArgumentOutOfRangeException ("startIndex + count > this.length");

			return IndexOfAnyImpl (anyOf, startIndex, count);
		}

		public int LastIndexOfAny (char[] anyOf)
		{
			if (anyOf == null)
				throw new System.ArgumentNullException ("anyOf");

			return InternalLastIndexOfAny (anyOf, this.length - 1, this.length);
		}

		public int LastIndexOfAny (char[] anyOf, int startIndex)
		{
			if (anyOf == null)
				throw new System.ArgumentNullException ("anyOf");

			if (startIndex < 0 || startIndex >= this.length)
				throw new System.ArgumentOutOfRangeException ();

			if (this.length == 0)
				return -1;

			return IndexOfAnyImpl (anyOf, startIndex, startIndex + 1);
		}

		public int LastIndexOfAny (char[] anyOf, int startIndex, int count)
		{
			if (anyOf == null)
				throw new System.ArgumentNullException ("anyOf");
			if ((startIndex < 0) || (startIndex >= this.Length))
				throw new System.ArgumentOutOfRangeException ("startIndex", "< 0 || > this.Length");
			if ((count < 0) || (count > this.Length))
				throw new System.ArgumentOutOfRangeException ("count", "< 0 || > this.Length");
			if (startIndex - count + 1 < 0)
				throw new System.ArgumentOutOfRangeException ("startIndex - count + 1 < 0");

			if (this.length == 0)
				return -1;

			return InternalLastIndexOfAny (anyOf, startIndex, count);
		}

		public int LastIndexOf (char value)
		{
			if (this.length == 0)
				return -1;

			return LastIndexOfImpl (value, this.length - 1, this.length);
		}

		public int LastIndexOf (char value, int startIndex)
		{
			return LastIndexOf (value, startIndex, startIndex + 1);
		}

		public int LastIndexOf (char value, int startIndex, int count)
		{
			if (startIndex == 0 && this.length == 0)
				return -1;
			if ((startIndex < 0) || (startIndex >= this.Length))
				throw new System.ArgumentOutOfRangeException ("startIndex", "< 0 || >= this.Length");
			if ((count < 0) || (count > this.Length))
				throw new System.ArgumentOutOfRangeException ("count", "< 0 || > this.Length");
			if (startIndex - count + 1 < 0)
				throw new System.ArgumentOutOfRangeException ("startIndex - count + 1 < 0");

			return LastIndexOfImpl (value, startIndex, count);
		}

		private int IndexOfImpl (char value, int startIndex, int count)
		{
			//"safe" implementation
			//for (int i = startIndex; i < count; i++)
			//    if (FastGetChar(i) == value)
			//        return i;

			unsafe {
				char* ptr = this._GetBuffer () + startIndex;
				for (int i = 0; i < count; i++) {
					if (*ptr == value)
						return startIndex + i;
					ptr++;
				}
			}

			return -1;
		}

		private int IndexOfAnyImpl (char[] anyOf, int startIndex, int count)
		{
			unsafe {
				char* ptr = this._GetBuffer () + startIndex;

				for (int i = 0; i < count; i++) {
					for (int loop = 0; loop != anyOf.Length; loop++)
						if (*ptr == anyOf[loop])
							return startIndex + i;
					ptr++;
				}
			}
			return -1;
		}

		private int LastIndexOfImpl (char value, int startIndex, int count)
		{
			unsafe {
				char* ptr = this._GetBuffer () + startIndex;

				for (int i = 0; i < count; i++) {
					if (*ptr == value)
						return startIndex - i;
					ptr--;
				}
			}
			return -1;
		}


		public static string CreateStringImpl (char[] val, int startIndex, int length)
		{
			if (val == null)
				throw new System.ArgumentNullException ("val");
			if (startIndex < 0)
				throw new System.ArgumentOutOfRangeException ("startIndex");
			if (length < 0)
				throw new System.ArgumentOutOfRangeException ("length");
			if (startIndex > val.Length - length)
				throw new System.ArgumentOutOfRangeException ("Out of range");
			if (length == 0)
				return string.Empty;

			InternalSystem.String result = InternalAllocateStr (length);

			unsafe {
				char* ptrres = result._GetBuffer ();

				for (int i = startIndex; i < length; i++) {
					*ptrres = val[i];
					ptrres++;
				}
			}

			return result as object as string;
		}

		public static string CreateStringImpl (char[] val)
		{
			if (val == null)
				return string.Empty;
			if (val.Length == 0)
				return string.Empty;

			InternalSystem.String result = InternalAllocateStr (val.Length);

			//"safe" implementation
			//for (int i = 0; i < val.Length; i++)
			//    result[i] = val[i];

			unsafe {
				char* ptrres = result._GetBuffer ();

				for (int i = 0; i < val.Length; i++) {
					*ptrres = val[i];
					ptrres++;
				}
			}

			return result as object as string;
		}

		public static string CreateStringImpl (uint value, bool signed, bool hex)
		{
			int offset = 0;

			uint uvalue = (uint)value;
			ushort divisor = hex ? (ushort)16 : (ushort)10;
			int length = 0;
			int count = 0;
			uint temp;
			bool negative = false;

			if (value < 0 && !hex && signed) {
				count++;
				uvalue = (uint)-value;
				negative = true;
			}

			temp = uvalue;

			do {
				temp /= divisor;
				count++;
			}
			while (temp != 0);

			length = count;
			InternalSystem.String result = InternalAllocateStr (length);

			unsafe {
				fixed (char* p = &result.firstChar) {

					if (negative) {
						p[offset++] = '-';
						count--;
					}

					for (int i = 0; i < count; i++) {
						uint remainder = uvalue % divisor;

						if (remainder < 10)
							p[offset + count - 1 - i] = (char)('0' + remainder);
						else
							p[offset + count - 1 - i] = (char)('A' + remainder - 10);

						uvalue /= divisor;
					}
				}
			}

			return result as object as string;
		}

		public unsafe static string CreateStringImpl (CString8* val)
		{
			if (val->Length == 0)
				return string.Empty;

			InternalSystem.String result = InternalAllocateStr (val->Length);

			unsafe {
				char* ptrres = result._GetBuffer ();

				for (int i = 0; i < val->Length; i++) {
					*ptrres = (char)(*(val->Pointer + i));
					ptrres++;
				}
			}

			return result as object as string;
		}

		public static bool IsNullOrEmpty (string value)
		{
			return (value == null) || (value.Length == 0);
		}

		// Stubs for Mono

		internal static String InternalAllocateStr (int length)
		{
			InternalSystem.String res = SharpOS.Korlib.Runtime.Runtime.AllocNewString (length);
			res.length = length;
			return res;
		}

		private int InternalLastIndexOfAny (char[] anyOf, int sIndex, int count)
		{
			unsafe {
				char* ptr = this._GetBuffer () + sIndex;

				for (int i = 0; i < count; i++) {
					for (int loop = 0; loop != anyOf.Length; loop++)
						if (*ptr == anyOf[loop])
							return sIndex - i;
					ptr--;
				}
			}
			return -1;
		}
	}
}

