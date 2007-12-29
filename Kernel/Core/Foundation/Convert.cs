// 
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//  Bruce Markham <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.Kernel.Foundation;

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

        public unsafe static CString8* ToString(int value, bool hex)
        {
            byte* buffer = (byte*)ADC.MemoryManager.Allocate(64);
            
            int l = ToString(value, hex, buffer, 64, 0);
            CString8* result = ((CString8*)buffer)->Substring(0, l);

            ADC.MemoryManager.Free((void*)buffer);
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

			if (!hex && value < 0) {
				count++;

				uvalue = (uint) -value;
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
	}
}
