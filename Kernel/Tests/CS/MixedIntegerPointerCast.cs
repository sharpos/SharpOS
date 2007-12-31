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
	public unsafe class MixedIntegerPointerCast {

		/// <summary>
		/// void* -> byte
		/// </summary>
		public static uint CMPVoidP2Byte ()
		{
			byte data = 100;
			void* ptr = &data;
			byte result = *(byte*) ptr;

			if (result == 100)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// void* -> short
		/// </summary>
		public static uint CMPVoidP2Short ()
		{
			short data = 100;
			void* ptr = &data;
			short result = *(short*) ptr;

			if (result == 100)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// void* -> int
		/// </summary>
		public static uint CMPVoidP2Int ()
		{
			int data = 100;
			void* ptr = &data;
			int result = *(int*) ptr;

			if (result == 100)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// void* -> long
		/// </summary>
		public static uint CMPVoidP2Long ()
		{
			long data = 100;
			void* ptr = &data;
			long result = *(long*) ptr;

			if (result == 100)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// void* -> sbyte
		/// </summary>
		public static uint CMPVoidP2SByte ()
		{
			sbyte data = 100;
			void* ptr = &data;
			sbyte result = *(sbyte*) ptr;

			if (result == 100)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// void* -> ushort
		/// </summary>
		public static uint CMPVoidP2UShort ()
		{
			ushort data = 100;
			void* ptr = &data;
			ushort result = *(ushort*) ptr;

			if (result == 100)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// void* -> uint
		/// </summary>
		public static uint CMPVoidP2UInt ()
		{
			uint data = 100;
			void* ptr = &data;
			uint result = *(uint*) ptr;

			if (result == 100)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// void* -> ulong
		/// </summary>
		public static uint CMPVoidP2ULong ()
		{
			ulong data = 100;
			void* ptr = &data;
			ulong result = *(ulong*) ptr;

			if (result == 100)
				return 1;
			else
				return 0;
		}
	}
}
