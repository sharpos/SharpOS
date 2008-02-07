//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
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
		public unsafe static uint CMP1 ()
		{
			byte foreground = 0xA;
			byte background = 0x5;

			byte attributes = (byte) ((byte) foreground | ((byte) background << 4));

			uint attr = attributes;	//FIXME: AOT bug, can't use attribute directly
			uint fill =
				((uint) 0x20) |
				(attr << 8) |
				((uint) 0x20 << 16) |
				(attr << 24);

			if (fill == 0x5a205a20)
				return 1;

			return 0;
		}

		public unsafe static uint CMP2 ()
		{
			uint fill =
					((uint) 0x20) |
					((uint) 0x20 << 8) |
					((uint) 0x20 << 16) |
					((uint) 0x20 << 24);

			if (fill == 0x20202020)
				return 1;

			return 0;
		}

		public unsafe static uint CMP0 ()
		{
			byte* buf = stackalloc byte [3];
			byte* outbuf = stackalloc byte [32];
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

		public unsafe static int Read7BitInt (void* ptr, int* ret_len)
		{
			// Originally from Mono: mcs/class/corlib/System.IO/BinaryReader.cs
			// Copyright (C) 2004 Novell

			int ret = 0;
			int shift = 0;
			byte* bp = (byte*) ptr;
			byte b;

			do {
				b = *bp;
				++bp;

				if (ret_len != null)
					(*ret_len)++;

				ret = ret | (((int) (b & 0x7f)) << shift);
				shift += 7;
			} while ((b & 0x80) == 0x80);

			return ret;
		}

		public unsafe static int ReadPrefixedString (void* ptr, byte* buffer, int bufferLen)
		{
			int ilen = 0;
			int size = 0;
			int x = 0;
			byte* bp = (byte*) ptr;

			size = Read7BitInt (ptr, &ilen);
			bp += ilen;

			buffer [bufferLen - 1] = 0;

			for (x = 0; x < size && x < bufferLen - 1; ++x)
				buffer [x] = bp [x];

			buffer [x] = 0;

			if (size != 2)
				return -2;

			if (ilen != 1)
				return -2;

			return ilen + size;
		}

		public static int Value2 (int i)
		{
			return i;
		}

		public static int Value (int i, int cmp, int first, int second)
		{
			return Value2 (i == cmp ? first : second);
		}

		public static uint CMP3 ()
		{
			if (Value (1, 1, 300, 0) == 300)
				return 1;

			return 0;
		}

		public static int Value4 (int i)
		{
			int result;

			if (i == 1)
				result = 301;

			else if (i == 2)
				result = 0;

			else
				result = 300;

			return result;
		}

		public static uint CMP4 ()
		{
			if (Value4 (5) == 300)
				return 1;

			return 0;
		}

		// bug regression test - PHI operants index problem
		// uncomment after it's fixed / for testing
		private class C
		{
			public void c5test(int i) {}
			public static C c;
		}
		
		public static void c5helper (bool a)
		{
			C.c.c5test(a?1:2);
		}

		public static uint CMPPhiIndexBug ()
		{
			C.c = new C();
			c5helper(true);
			C.c = null;
			return 1;
		}
	}
}
