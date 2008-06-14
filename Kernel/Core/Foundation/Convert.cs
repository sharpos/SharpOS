//
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
//  James Starr <jamesstarr@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.Foundation {
	public unsafe class Convert {
		public const byte NonAsciiCharacter = 0x7f;

		public unsafe static int ToInt32 (CString8 *str)
		{
			return ToInt32 (str, 0, str->Length);
		}

		public unsafe static int ToInt32 (CString8 *str, int offset, int length)
		{
			return ToInt32 (str->Pointer, offset, length, str->Length);
		}

		public unsafe static int ToInt32 (PString8 *str, int offset, int length)
		{
			return ToInt32 (str->Pointer, offset, length, str->Length);
		}

		public unsafe static int ToInt32 (PString8 *str)
		{
			return ToInt32 (str->Pointer, 0, str->Length, str->Length);
		}

		public unsafe static int ToInt32 (byte *buffer, int offset, int length, int capacity)
		{
			bool started = false;
			int result, place;

			result = 0;
			place = 1;

			Diagnostics.Assert (offset + length >= capacity,
				"Convert.ToInt32(): offset + length >= capacity");

			for (int x = offset + length; x >= offset; --x) {
				int digit = 0;

				if (!started && buffer [x] == '\0')
					continue;

				started = false;

				digit = buffer [x] - (byte)'0';

				if (x == 0 && buffer [x] == '-') {
					result = 0 - result;
					continue;
				}

				if (!ASCII.IsNumeric (buffer [x])) {
					Diagnostics.Error ("Convert.ToInt32(): Contains non-digit");
					return 0;
				}

				result += place * digit;
				place *= 10;
			}

			return result;
		}

		public unsafe static int ToByteString (string str, byte* buffer, int bufferLen, int offset)
		{
			if (offset + str.Length + 1 > bufferLen)
				return -1;

			for (int x = 0; x < str.Length; ++x)
				buffer [offset + x] = (byte) str [x];

			return 0;
		}

		public unsafe static CString8* ToString (int value, bool hex)
		{
			byte* buffer = (byte*) ADC.MemoryManager.Allocate (64);

			int l = ToString (value, hex, buffer, 64, 0);
			CString8* result = ((CString8*) buffer)->Substring (0, l);

			ADC.MemoryManager.Free ((void*) buffer);
			return result;
		}
		
		public unsafe static int ToString(uint value, bool hex, byte* buffer,
							int bufferLen, int offset)
		{
			return ToString((int)value, hex, false, buffer, bufferLen, offset);
		}

		public unsafe static int ToString(int value, bool hex, byte* buffer,
							int bufferLen, int offset)
		{
			return ToString(value, hex, true, buffer, bufferLen, offset);
		}

		private unsafe static int ToString (int value, bool hex, bool signed, byte* buffer,
						    int bufferLen, int offset)
		{
			uint uvalue = (uint) value;
			ushort divisor = hex ? (ushort) 16 : (ushort) 10;
			int length = 0;
			int count = 0;
			uint temp;
			bool negative = false;

			if (value < 0 && !hex && signed) {
				count++;
				uvalue = (uint) -value;
				negative = true;
			}

			temp = uvalue;

			do {
				temp /= divisor;
				count++;
			}
			while (temp != 0);

			Diagnostics.Assert (offset + count < bufferLen, "Convert.ToString: buffer too small.");

			length = count;

			if (negative) {
				buffer [offset++] = (byte) '-';
				count--;
			}

			for (int i = 0; i < count; i++) {
				uint remainder = uvalue % divisor;

				if (remainder < 10)
					buffer [offset + count - 1 - i] = (byte) ('0' + remainder);

				else
					buffer [offset + count - 1 - i] = (byte) ('A' + remainder - 10);

				uvalue /= divisor;
			}

			return length;
		}

#if LONG_ENABLED
                public unsafe static int ToString (long value, bool hex, byte* buffer,
                                                    int bufferLen, int offset)
                {
                        ulong uvalue = (ulong) value;
                        ushort divisor = hex ? (ushort) 16 : (ushort) 10;
                        int length = 0;
                        int count = 0;
                        ulong temp;
                        bool negative = false;

                        if (value < 0) {
                                count++;
                                uvalue = (ulong) -value;
                                negative = true;
                        }

                        temp = uvalue;

                        do {
                                temp /= divisor;
                                count++;
                        }
                        while (temp != 0);

                        Diagnostics.Assert (offset + count < bufferLen, "Convert.ToString: buffer too small.");

                        length = count;

                        if (negative) {
                                buffer [offset++] = (byte) '-';
                                count--;
                        }

                        for (int i = 0; i < count; i++) {
                                ulong remainder = uvalue % divisor;

                                if (remainder < 10)
                                        buffer [offset + count - 1 - i] = (byte) ('0' + remainder);

                                else
                                        buffer [offset + count - 1 - i] = (byte) ('A' + remainder - 10);

                                uvalue /= divisor;
                        }

                        return length;
                }

                /// <summary>
                /// Runs tests on the long to string conversions.
                /// </summary>
                public unsafe static void __Test2 ()
                {
                        long t1 = 105L;
                        long t2 = -1805L;
                        long t3 = 0x6FL;
                        long t4 = -0x96AL;
                        CString8* p1 = (CString8*)Stubs.CString("105"), p2 = (CString8*)Stubs.CString("-1805");
                        CString8* p3 = (CString8*)Stubs.CString("6F"), p4 = (CString8*)Stubs.CString("-96A");


                        if (!__StringComp(Convert.ToString(t1, false), p1))
                                TextMode.WriteLine("Convert.ToString(long, bool) Failed Test:  105");

                        if (!__StringComp(Convert.ToString(t2, false), p2))
                                TextMode.WriteLine("Convert.ToString(long, bool) Failed Test:  -1805");

                        if (!__StringComp(Convert.ToString(t3, true), p3))
                                TextMode.WriteLine("Convert.ToString(long, bool) Failed Test:  0x6F");

                        if (!__StringComp(Convert.ToString(t4, true), p4))
                                TextMode.WriteLine("Convert.ToString(long, bool) Failed Test:  -0x96A");

                }

                public unsafe static CString8* ToString (long value, bool hex)
                {
                        byte* buffer = (byte*) ADC.MemoryManager.Allocate (128);

                        int l = ToString (value, hex, buffer, 128, 0);
                        CString8* result = ((CString8*)buffer)->Substring (0, l);

                        ADC.MemoryManager.Free ((void*) buffer);
                        return result;
                }
#else
		public unsafe static void __Test2 ()
		{
			TextMode.WriteLine ("Convert.ToString(long, bool) Warning:  Long Is Not Enabled");
		}
#endif

		internal unsafe static void __RunTests ()
		{
			__Test1 ();
			__Test2 ();
			__Test3 ();

		}

		/// <summary>
		/// Runs tests on the int to string conversions.
		/// </summary>
		public unsafe static void __Test1 ()
		{
			int t1 = 105;
			int t2 = -1805;
			int t3 = 0x6F;
			int t4 = -0x96A;
			CString8* p1 = (CString8*)Stubs.CString("105");
			CString8* p2 = (CString8*)Stubs.CString("-1805");
			CString8* p3 = (CString8*) Stubs.CString ("6F");
			CString8* p4 = (CString8*)Stubs.CString("-96A");


			if (!__StringComp(Convert.ToString(t1, false), p1))
				TextMode.WriteLine("Convert.ToString(int, bool) Failed Test:  105");

			if (!__StringComp(Convert.ToString(t2, false), p2))
				TextMode.WriteLine("Convert.ToString(int, bool) Failed Test:  -1805");

			if (!__StringComp(Convert.ToString(t3, true), p3))
				TextMode.WriteLine("Convert.ToString(int, bool) Failed Test:  0x6F");

			if (!__StringComp(Convert.ToString(t4, true), p4))
				TextMode.WriteLine("Convert.ToString(int, bool) Failed Test:  -0x96A");
		}


		public static void __Test3 ()
		{
			char aCharacter = 'a';
			string emptyString = "";
			string otherString = "Û<=>";
			char [] otherArray = new char [3];
			otherArray [0] = 'x';
			otherArray [1] = 'y';
			otherArray [2] = 'z';

			Testcase.Test (ToAscii (aCharacter) == 0x61,
				     "Convert.ToAscii()",
				     "char 'a' to Ascii");

			byte [] currentArray = ToAscii (emptyString);

			Testcase.Test (currentArray.Length == 0,
				     "Convert.ToAscii()",
				     "string \"\" to Ascii");

			currentArray = ToAscii (otherString);

			Testcase.Test (currentArray.Length == 4,
				     "Convert.ToAscii()",
				     "string \"Û<=>\" to Ascii(length)");
			Testcase.Test (currentArray [0] == NonAsciiCharacter,
				     "Convert.ToAscii()",
				     "string \"Û<=>\" to Ascii[0]");
			Testcase.Test (currentArray [1] == 0x3C,
				     "Convert.ToAscii()",
				     "string \"Û<=>\" to Ascii[1]");
			Testcase.Test (currentArray [2] == 0x3D,
				     "Convert.ToAscii()",
				     "string \"Û<=>\" to Ascii[2]");
			Testcase.Test (currentArray [3] == 0x3E,
				     "Convert.ToAscii()",
				     "string \"Û<=>\" to Ascii[3]");

			currentArray = ToAscii (otherArray);
			Testcase.Test (currentArray.Length == 3,
				     "Convert.ToAscii()",
				     "string \"xyz\" to Ascii(length)");
			Testcase.Test (currentArray [0] == 0x78,
				     "Convert.ToAscii()",
				     "char[] \"xyz\" to Ascii[0]");
			Testcase.Test (currentArray [1] == 0x79,
				     "Convert.ToAscii()",
				     "char[] \"xyz\" to Ascii[1]");
			Testcase.Test (currentArray [2] == 0x7a,
				     "Convert.ToAscii()",
				     "char[] \"xyz\" to Ascii[2]");
		}    


		private unsafe static bool __StringComp (CString8* str1, CString8* str2)
		{
			byte* s1 = (byte*) str1;
			byte* s2 = (byte*) str2;
			while (*s1 == *s2 && *s1 != 0 && *s2 != 0) {
				++s1;
				++s2;
			}

			return (*s1 == 0 & *s2 == 0);
		}

		/// <summary>
		/// Converts character to ascii representation.  If unicode 
		/// character is outside the range of an Ascii character then 
		/// Convert.NonAsciiCharacter is returned.
		/// </summary>
		/// <param name="value">Character to be converted</param>
		/// <returns>Ascii representation of the character</returns>
		public static byte ToAscii (char value)
		{
			unsafe {
				byte* uniPointer = (byte*)&value;
				//read in first word(little Endian)
				uint firstWord = *uniPointer;
				uniPointer++;
				firstWord = firstWord
					   + ((uint)(*uniPointer) << 8);
				if (firstWord <= 0x7F) {
					return (byte)firstWord;
				}
				return NonAsciiCharacter;
			}
		}

		/// <summary>
		/// Converts the array of characters to ascii representation.  
		/// If unicode character is outside the range of an Ascii 
		/// character then Convert.NonAsciiCharacter is returned in its 
		/// place.
		/// </summary>
		/// <param name="value">Array of Characters to Converted
		/// </param>
		/// <returns>Array of byte containing Ascii representation
		/// </returns>
		public static byte [] ToAscii (char [] value)
		{
			byte [] asciiEncoding = new byte [value.Length];
			for (int x = 0; x < value.Length; x++) {
				asciiEncoding [x] = ToAscii (value [x]);
			}
			return asciiEncoding;
		}

		/// <summary>
		/// Converts string to its ascii representation.  If unicode 
		/// character is outside the range of an Ascii character then 
		/// Convert.NonAsciiCharacter is returned in its place.
		/// </summary>
		/// <param name="value">string to be converted</param>
		/// <returns>Array of byte containing Ascii representation
		/// </returns>
		public static byte [] ToAscii (string value)
		{
			byte [] asciiEncoding = new byte [value.Length];
			for (int x = 0; x < value.Length; x++) {
				asciiEncoding [x] = ToAscii (value [x]);
			}
			return asciiEncoding;
		}

		/// <summary>
		/// Converts character to an ascii c like string representation.
		/// If unicode character is outside the range of an Ascii
		/// character then Convert.NonAsciiCharacter is returned.
		/// </summary>
		/// <param name="value">char to be converted</param>
		/// <returns></returns>
		public unsafe static CString8* ToCString8 (char value)
		{
			CString8* str1 = CString8.Create (1);
			str1->SetChar (0, ToAscii (value));
			return str1;
		}

		/// <summary>
		/// Converts the array of characters to an ascii c like string 
		/// representation.  If unicode character is outside the range	
		/// of an Ascii character then Convert.NonAsciiCharacter is 
		/// returned in its place.
		/// </summary>
		/// <param name="value">Array of Characters to Converted
		/// </param>
		/// <returns></returns>
		public unsafe static CString8* ToCString8 (char [] value)
		{
			CString8* str1 = CString8.Create (value.Length);
			for (int x = 0; x < value.Length; x++) {
				str1->SetChar (x, ToAscii (value [x]));
			}
			return str1;
		}

		/// <summary>
		/// Converts string to its ascii representation terminating with
		/// null.  If unicode character is outside the range of an Ascii
		/// character then Convert.NonAsciiCharacter is returned in its 
		/// place.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public unsafe static CString8* ToCString8 (string value)
		{
			CString8* str1 = CString8.Create (value.Length);
			for (int x = 0; x < value.Length; x++) {
				str1->SetChar (x, ToAscii (value [x]));
			}
			return str1;
		}

		static public string ToString (char[] val, int startIndex, int length)
		{
			return InternalSystem.String.CreateStringImpl (val, startIndex, length);
		}

		static public string ToString (char[] val)
		{
			return InternalSystem.String.CreateStringImpl (val);
		}

		static public string ToString (CString8* cstring)
		{
			return InternalSystem.String.CreateStringImpl (cstring);
		}
	}
}
