//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

namespace SharpOS.Kernel.Tests.CS {
	public unsafe class PointerCast {
		/// <summary>
		/// void* -> byte*
		/// </summary>
		public static uint CMPVoidP2ByteP ()
		{
			void* vp = (void*) 100;
			byte* bp = null;

			bp = (byte*) vp;

			if (bp == (byte*) 100)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// void* -> short*
		/// </summary>
		public static uint CMPVoidP2ShortP ()
		{
			void* vp = (void*) 10000;
			short* sp = null;

			sp = (short*) vp;

			if (sp == (short*) 10000)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// void* -> int*
		/// </summary>
		public static uint CMPVoidP2IntP ()
		{
			void* vp = (void*) 0x10000;
			int* ip = null;

			ip = (int*) vp;

			if (ip == (int*) 0x10000)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// void* -> long*
		/// </summary>
		public static uint CMPVoidP2LongP ()
		{
			void* vp = (void*) 0x10000;
			long* lp = null;

			lp = (long*) vp;

			if (lp == (long*) 0x10000)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// byte* -> void*
		/// </summary>
		public static uint CMPByteP2VoidP ()
		{
			byte* bp = (byte*) 0x10000;
			void* vp = null;

			vp = (void*) bp;

			if (vp == (void*) 0x10000)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// short* -> void*
		/// </summary>
		public static uint CMPShortP2VoidP ()
		{
			short* sp = (short*) 0x10000;
			void* vp = null;

			vp = (void*) sp;

			if (vp == (void*) 0x10000)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// int* -> void*
		/// </summary>
		public static uint CMPIntP2VoidP ()
		{
			int* ip = (int*) 0x10000;
			void* vp = null;

			vp = (void*) ip;

			if (vp == (void*) 0x10000)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// long* -> void*
		/// </summary>
		public static uint CMPLongP2VoidP ()
		{
			long* lp = (long*) 0x10000;
			void* vp = null;

			vp = (void*) lp;

			if (vp == (void*) 0x10000)
				return 1;
			else
				return 0;
		}
	}
}
