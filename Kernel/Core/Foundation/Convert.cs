// 
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.Foundation {
	public unsafe class Convert {


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

		public unsafe static int ToString (int value, bool hex, byte* buffer,
						    int bufferLen, int offset)
		{
			uint uvalue = (uint) value;
			ushort divisor = hex ? (ushort) 16 : (ushort) 10;
			int length = 0;
			int count = 0;
			uint temp;
			bool negative = false;

			if (value < 0) {
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
	}
}
