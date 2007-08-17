// 
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using SharpOS.Memory;

namespace SharpOS.Foundation {
	public unsafe class Convert {
		public unsafe static int ToByteString (string str, byte *buffer, int bufferLen, int offset)
		{
			if (offset + str.Length + 1 > bufferLen)
				return -1;

			for (int x = 0; x < str.Length; ++x)
				buffer [offset + x] = (byte)str [x];

			return 0;
		}
		
		public unsafe static int ToString (int value, bool hex, byte *buffer,
						    int bufferLen, int offset)
		{
			uint uvalue = (uint) value;
			ushort divisor = hex ? (ushort) 16 : (ushort) 10;
			int length = 0;

			if (!hex && value < 0) {
				buffer [length++] = (byte) '-';

				uvalue = (uint) -value;
			}

			do {
				uint remainder = uvalue % divisor;

				if (offset + length >= bufferLen)
					return length;
				
				if (remainder < 10)
					buffer [offset + (length++)] =
						(byte) ('0' + remainder);

				else
					buffer [offset + (length++)] =
						(byte) ('A' + remainder - 10);

			} while ((uvalue /= divisor) != 0);

			return length;
		}
	}
}
