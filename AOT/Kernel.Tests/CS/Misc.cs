//
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT.X86;

namespace SharpOS.Kernel.Tests.CS {
	public class Misc {
		public unsafe static uint CMP0 ()
		{
			byte *buf = stackalloc byte [3];
			byte *outbuf = stackalloc byte [32];
			int size = 0;
			
			buf [0] = 2;
			buf [1] = (byte) 'U';
			buf [2] = (byte) 'S';

			size = ReadPrefixedString (buf, outbuf, 32);

			if (size == -2)
				return 0;
			else 
				return 1;
		}
		
		public unsafe static int Read7BitInt (void *ptr, int *ret_len)
		{
			// Originally from Mono: mcs/class/corlib/System.IO/BinaryReader.cs
			// Copyright (C) 2004 Novell
			
			int ret = 0;
			int shift = 0;
			byte *bp = (byte*)ptr;
			byte b;
			
			do {
				b = *bp;
				++bp;

				if (ret_len != null)
					(*ret_len)++;
				
				ret = ret | (((int)(b & 0x7f)) << shift);
				shift += 7;
			} while ((b & 0x80) == 0x80);

			return ret;
		}

		public unsafe static int ReadPrefixedString (void *ptr, byte *buffer, int bufferLen)
		{
			int ilen = 0;
			int size = 0;
			int x = 0;
			byte *bp = (byte*)ptr;

			size = Read7BitInt (ptr, &ilen);
			bp += ilen;

			buffer [bufferLen-1] = 0;
			
			for (x = 0; x < size && x < bufferLen-1; ++x)
				buffer [x] = bp [x];
			
			buffer [x] = 0;

			if (size != 2)
				return -2;

			if (ilen != 1)
				return -2;
				
			return ilen + size;
		}
	}
}
