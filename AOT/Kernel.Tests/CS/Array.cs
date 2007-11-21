//
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

#define ARRAYS_NOT_SUPPORTED

namespace SharpOS.Kernel.Tests.CS {
#if !ARRAYS_NOT_SUPPORTED
	public unsafe class Array {

		/// <summary>
		/// int[] read/write
		/// </summary>
		public static uint CMPIntArray ()
		{
			int [] arr = new int [3];

			arr [0] = 7;
			arr [1] = 9;
			arr [2] = 44;
			
			if (arr [0] != 7)
				return 0;

			if (arr [1] != 9)
				return 0;

			if (arr [2] != 44)
				return 0;

			return 1;
		}
		
		/// <summary>
		/// short[] read/write
		/// </summary>
		public static uint CMPShortArray ()
		{
			short [] arr = new short [3];

			arr [0] = 7;
			arr [1] = 9;
			arr [2] = 44;
			
			if (arr [0] != 7)
				return 0;

			if (arr [1] != 9)
				return 0;

			if (arr [2] != 44)
				return 0;

			return 1;
		}
		
		/// <summary>
		/// byte[] read/write
		/// </summary>
		public static uint CMPByteArray ()
		{
			byte [] arr = new byte [3];

			arr [0] = 7;
			arr [1] = 9;
			arr [2] = 44;
			
			if (arr [0] != 7)
				return 0;

			if (arr [1] != 9)
				return 0;

			if (arr [2] != 44)
				return 0;

			return 1;
		}

		/// <summary>
		/// int[] read/write
		/// </summary>
		public static uint CMPLongArray ()
		{
			long [] arr = new long [3];

			arr [0] = 7;
			arr [1] = 9;
			arr [2] = 44;
			
			if (arr [0] != 7)
				return 0;

			if (arr [1] != 9)
				return 0;

			if (arr [2] != 44)
				return 0;

			return 1;
		}
		///////////////////

		/// <summary>
		/// uint[] read/write
		/// </summary>
		public static uint CMPUIntArray ()
		{
			uint [] arr = new uint [3];

			arr [0] = 7;
			arr [1] = 9;
			arr [2] = 44;
			
			if (arr [0] != 7)
				return 0;

			if (arr [1] != 9)
				return 0;

			if (arr [2] != 44)
				return 0;

			return 1;
		}
		
		/// <summary>
		/// ushort[] read/write
		/// </summary>
		public static uint CMPUShortArray ()
		{
			short [] arr = new short [3];

			arr [0] = 7;
			arr [1] = 9;
			arr [2] = 44;
			
			if (arr [0] != 7)
				return 0;

			if (arr [1] != 9)
				return 0;

			if (arr [2] != 44)
				return 0;

			return 1;
		}
		
		/// <summary>
		/// sbyte[] read/write
		/// </summary>
		public static uint CMPSByteArray ()
		{
			sbyte [] arr = new sbyte [3];

			arr [0] = 7;
			arr [1] = 9;
			arr [2] = 44;
			
			if (arr [0] != 7)
				return 0;

			if (arr [1] != 9)
				return 0;

			if (arr [2] != 44)
				return 0;

			return 1;
		}

		/// <summary>
		/// ulong[] read/write
		/// </summary>
		public static uint CMPULongArray ()
		{
			ulong [] arr = new ulong [3];

			arr [0] = 7;
			arr [1] = 9;
			arr [2] = 44;
			
			if (arr [0] != 7)
				return 0;

			if (arr [1] != 9)
				return 0;

			if (arr [2] != 44)
				return 0;

			return 1;
		}
		
		/// <summary>
		/// int[].Length
		/// </summary>
		public static uint CMPIntArrayLength ()
		{
			int [] arrA = new int [10];
			int [] arrB = new int [4];

			if (arrA.Length != 10)
				return 0;
			if (arrB.Length != 4)
				return 0;

			return 1;
		}
		
		/// <summary>
		/// int[].Length
		/// </summary>
		public static uint CMPIntArrayZeroLength ()
		{
			int [] arr = new int [0];

			if (arr.Length != 0)
				return 0;

			return 1;
		}
	}
#endif
}
