//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//	Sam Wilson <tecywiz121@hotmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

//#define ARRAYS_NOT_SUPPORTED
//#define MULTIDIMENSIONAL_ARRAYS_NOT_SUPPORTED
//#define JAGGED_ARRAYS_NOT_SUPPORTED

namespace SharpOS.Kernel.Tests.CS {
#if !ARRAYS_NOT_SUPPORTED
	public unsafe class Array {

		class SomeObject {
			public SomeObject (int value)
			{
				this.value = value;
			}

			public int value = 0;
		}

		struct SomeStruct {
			public SomeStruct (int value)
			{
				this.value = value;
			}

			public int value;
			
			public int Value {
				get {
					return value;
				}
			}
		}

		/// <summary>
		/// SomeStruct[] call on element
		/// </summary>
		public static uint CMPStructArrayCall ()
		{
			SomeStruct [] arr = new SomeStruct [1];
			int i = 0;

			arr [0] = new SomeStruct (42);

			if (arr [i].Value != 42)
				return 0;

			return 1;
		}

		/// <summary>
		/// int[] read/write
		/// </summary>
		public static uint CMPObjectArray ()
		{
			SomeObject [] arr = new SomeObject [3];

			arr [0] = new SomeObject (7);
			arr [1] = new SomeObject (9);
			arr [2] = new SomeObject (44);

			if (arr [0].value != 7)
				return 0;

			if (arr [1].value != 9)
				return 0;

			if (arr [2].value != 44)
				return 0;

			return 1;
		}

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
		/// Long[] read/write
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
			ushort [] arr = new ushort [3];

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
#endif

#if !JAGGED_ARRAYS_NOT_SUPPORTED
		/// <summary>
		/// int[][] read/write
		/// </summary>
		public static uint CMPIntJaggedMultidimensionalArray ()
		{
			int [,] [,,] arr = new int [2,3] [,,];

			arr [1, 0] = new int [2, 2, 5];
			arr [1, 1] = new int [2, 2, 3];
			arr [1, 2] = new int [2, 2, 2];

			arr [1, 0] [1, 1, 0] = 39;
			arr [1, 0] [1, 1, 1] = 7;
			arr [1, 0] [1, 1, 2] = 1009;
			arr [1, 0] [1, 1, 3] = -22;
			arr [1, 0] [1, 1, 4] = 1;

			arr [1, 1] [1, 1, 0] = 88887;
			arr [1, 1] [1, 1, 1] = -987788;
			arr [1, 1] [1, 1, 2] = 0;

			arr [1, 2] [1, 1, 0] = 6;
			arr [1, 2] [1, 1, 1] = arr [1, 0] [1, 1, 0] * arr [1, 2] [1, 1, 0];

			if (arr [1, 0] [1, 1, 0] != 39)
				return 0;

			if (arr [1, 0] [1, 1, 1] != 7)
				return 0;

			if (arr [1, 0] [1, 1, 2] != 1009)
				return 0;

			if (arr [1, 0] [1, 1, 3] != -22)
				return 0;

			if (arr [1, 0] [1, 1, 4] != 1)
				return 0;



			if (arr [1, 1] [1, 1, 0] != 88887)
				return 0;

			if (arr [1, 1] [1, 1, 1] != -987788)
				return 0;

			if (arr [1, 1] [1, 1, 2] != 0)
				return 0;


			if (arr [1, 2] [1, 1, 0] != 6)
				return 0;

			if (arr [1, 2] [1, 1, 1] != 39 * 6)
				return 0;

			return 1;
		}

		/// <summary>
		/// int[][] read/write
		/// </summary>
		public static uint CMPIntJaggedArray ()
		{
			int [] [] arr = new int [3] [];

			arr [0] = new int [5];
			arr [1] = new int [3];
			arr [2] = new int [2];

			arr [0] [0] = 39;
			arr [0] [1] = 7;
			arr [0] [2] = 1009;
			arr [0] [3] = -22;
			arr [0] [4] = 1;

			arr [1] [0] = 88887;
			arr [1] [1] = -987788;
			arr [1] [2] = 0;

			arr [2] [0] = 6;
			arr [2] [1] = arr [0] [0] * arr [2] [0];

			if (arr [0] [0] != 39)
				return 0;

			if (arr [0] [1] != 7)
				return 0;

			if (arr [0] [2] != 1009)
				return 0;

			if (arr [0] [3] != -22)
				return 0;

			if (arr [0] [4] != 1)
				return 0;



			if (arr [1] [0] != 88887)
				return 0;

			if (arr [1] [1] != -987788)
				return 0;

			if (arr [1] [2] != 0)
				return 0;


			if (arr [2] [0] != 6)
				return 0;

			if (arr [2] [1] != 39 * 6)
				return 0;

			return 1;
		}

		/// <summary>
		/// Short[][] read/write
		/// </summary>
		public static uint CMPShortJaggedArray ()
		{
			short [] [] arr = new short [3] [];

			arr [0] = new short [5];
			arr [1] = new short [3];
			arr [2] = new short [2];

			arr [0] [0] = 39;
			arr [0] [1] = 7;
			arr [0] [2] = 1009;
			arr [0] [3] = -22;
			arr [0] [4] = 1;

			arr [1] [0] = 667;
			arr [1] [1] = -876;
			arr [1] [2] = 0;

			arr [2] [0] = 6;
			arr [2] [1] = 39 * 6;

			if (arr [0] [0] != 39)
				return 0;

			if (arr [0] [1] != 7)
				return 0;

			if (arr [0] [2] != 1009)
				return 0;

			if (arr [0] [3] != -22)
				return 0;

			if (arr [0] [4] != 1)
				return 0;



			if (arr [1] [0] != 667)
				return 0;

			if (arr [1] [1] != -876)
				return 0;

			if (arr [1] [2] != 0)
				return 0;


			if (arr [2] [0] != 6)
				return 0;

			if (arr [2] [1] != 39 * 6)
				return 0;

			return 1;
		}

		/// <summary>
		/// Byte[][] read/write
		/// </summary>
		public static uint CMPByteJaggedArray ()
		{
			byte [] [] arr = new byte [3] [];

			arr [0] = new byte [5];
			arr [1] = new byte [3];
			arr [2] = new byte [2];

			arr [0] [0] = 39;
			arr [0] [1] = 7;
			arr [0] [2] = 76;
			arr [0] [3] = 22;
			arr [0] [4] = 1;

			arr [1] [0] = 99;
			arr [1] [1] = 87;
			arr [1] [2] = 0;

			arr [2] [0] = 6;
			arr [2] [1] = (byte) (arr [0] [0] * arr [2] [0]);

			if (arr [0] [0] != 39)
				return 0;

			if (arr [0] [1] != 7)
				return 0;

			if (arr [0] [2] != 76)
				return 0;

			if (arr [0] [3] != 22)
				return 0;

			if (arr [0] [4] != 1)
				return 0;



			if (arr [1] [0] != 99)
				return 0;

			if (arr [1] [1] != 87)
				return 0;

			if (arr [1] [2] != 0)
				return 0;


			if (arr [2] [0] != 6)
				return 0;

			if (arr [2] [1] != 39 * 6)
				return 0;

			return 1;
		}

		/// <summary>
		/// long[][] read/write
		/// </summary>
		public static uint CMPLongJaggedArray ()
		{
			long [] [] arr = new long [3] [];

			arr [0] = new long [5];
			arr [1] = new long [3];
			arr [2] = new long [2];

			arr [0] [0] = 39;
			arr [0] [1] = 7;
			arr [0] [2] = 1009;
			arr [0] [3] = -22;
			arr [0] [4] = 1;

			arr [1] [0] = 88887;
			arr [1] [1] = -987788;
			arr [1] [2] = 0;

			arr [2] [0] = 6;
			arr [2] [1] = arr [0] [0] * arr [2] [0];

			if (arr [0] [0] != 39)
				return 0;

			if (arr [0] [1] != 7)
				return 0;

			if (arr [0] [2] != 1009)
				return 0;

			if (arr [0] [3] != -22)
				return 0;

			if (arr [0] [4] != 1)
				return 0;



			if (arr [1] [0] != 88887)
				return 0;

			if (arr [1] [1] != -987788)
				return 0;

			if (arr [1] [2] != 0)
				return 0;


			if (arr [2] [0] != 6)
				return 0;

			if (arr [2] [1] != 39 * 6)
				return 0;

			return 1;
		}
		
		/// <summary>
		/// ulong[][] read/write
		/// </summary>
		public static uint CMPULongJaggedArray ()
		{
			ulong [] [] arr = new ulong [3] [];

			arr [0] = new ulong [5];
			arr [1] = new ulong [3];
			arr [2] = new ulong [2];

			arr [0] [0] = 39;
			arr [0] [1] = 7;
			arr [0] [2] = 76;
			arr [0] [3] = 22;
			arr [0] [4] = 4294967294;

			arr [1] [0] = 99;
			arr [1] [1] = 87;
			arr [1] [2] = 0;

			arr [2] [0] = 6;
			arr [2] [1] = arr [0] [0] * arr [2] [0];

			if (arr [0] [0] != 39)
				return 0;

			if (arr [0] [1] != 7)
				return 0;

			if (arr [0] [2] != 76)
				return 0;

			if (arr [0] [3] != 22)
				return 0;

			if (arr [0] [4] != 4294967294)
				return 0;



			if (arr [1] [0] != 99)
				return 0;

			if (arr [1] [1] != 87)
				return 0;

			if (arr [1] [2] != 0)
				return 0;


			if (arr [2] [0] != 6)
				return 0;

			if (arr [2] [1] != 39 * 6)
				return 0;

			return 1;
		}
		/// <summary>
		/// sbyte[][] read/write
		/// </summary>
		public static uint CMPSByteJaggedArray ()
		{
			sbyte [] [] arr = new sbyte [3] [];

			arr [0] = new sbyte [5];
			arr [1] = new sbyte [3];
			arr [2] = new sbyte [2];

			arr [0] [0] = 39;
			arr [0] [1] = 7;
			arr [0] [2] = 76;
			arr [0] [3] = 22;
			arr [0] [4] = 1;

			arr [1] [0] = 99;
			arr [1] [1] = 87;
			arr [1] [2] = 0;

			arr [2] [0] = 2;
			arr [2] [1] = (sbyte) (arr [0] [0] * arr [2] [0]);

			if (arr [0] [0] != 39)
				return 0;

			if (arr [0] [1] != 7)
				return 0;

			if (arr [0] [2] != 76)
				return 0;

			if (arr [0] [3] != 22)
				return 0;

			if (arr [0] [4] != 1)
				return 0;



			if (arr [1] [0] != 99)
				return 0;

			if (arr [1] [1] != 87)
				return 0;

			if (arr [1] [2] != 0)
				return 0;


			if (arr [2] [0] != 2)
				return 0;

			if (arr [2] [1] != 39 * 2)
				return 0;

			return 1;
		}
		/// <summary>
		/// ushort[][] read/write
		/// </summary>
		public static uint CMPUShortJaggedArray ()
		{
			ushort [] [] arr = new ushort [3] [];

			arr [0] = new ushort [5];
			arr [1] = new ushort [3];
			arr [2] = new ushort [2];

			arr [0] [0] = 39;
			arr [0] [1] = 7;
			arr [0] [2] = 76;
			arr [0] [3] = 22;
			arr [0] [4] = 1;

			arr [1] [0] = 99;
			arr [1] [1] = 87;
			arr [1] [2] = 0;

			arr [2] [0] = 6;
			arr [2] [1] = (ushort) (arr [0] [0] * arr [2] [0]);

			if (arr [0] [0] != 39)
				return 0;

			if (arr [0] [1] != 7)
				return 0;

			if (arr [0] [2] != 76)
				return 0;

			if (arr [0] [3] != 22)
				return 0;

			if (arr [0] [4] != 1)
				return 0;



			if (arr [1] [0] != 99)
				return 0;

			if (arr [1] [1] != 87)
				return 0;

			if (arr [1] [2] != 0)
				return 0;


			if (arr [2] [0] != 6)
				return 0;

			if (arr [2] [1] != 39 * 6)
				return 0;

			return 1;
		}
		/// <summary>
		/// uint[][] read/write
		/// </summary>
		public static uint CMPUIntJaggedArray ()
		{
			uint [] [] arr = new uint [3] [];

			arr [0] = new uint [5];
			arr [1] = new uint [3];
			arr [2] = new uint [2];

			arr [0] [0] = 39;
			arr [0] [1] = 7;
			arr [0] [2] = 76;
			arr [0] [3] = 22;
			arr [0] [4] = 4294967294;

			arr [1] [0] = 99;
			arr [1] [1] = 87;
			arr [1] [2] = 0;

			arr [2] [0] = 6;
			arr [2] [1] = arr [0] [0] * arr [2] [0];

			if (arr [0] [0] != 39)
				return 0;

			if (arr [0] [1] != 7)
				return 0;

			if (arr [0] [2] != 76)
				return 0;

			if (arr [0] [3] != 22)
				return 0;

			if (arr [0] [4] != 4294967294)
				return 0;



			if (arr [1] [0] != 99)
				return 0;

			if (arr [1] [1] != 87)
				return 0;

			if (arr [1] [2] != 0)
				return 0;


			if (arr [2] [0] != 6)
				return 0;

			if (arr [2] [1] != 39 * 6)
				return 0;

			return 1;
		}
#endif

#if !MULTIDIMENSIONAL_ARRAYS_NOT_SUPPORTED

		/// <summary>
		/// int[,] read/write
		/// </summary>
		public static uint CMPIntMultidimensionalArray ()
		{
			int [,] arr = new int [3, 2];

			arr [0, 0] = 39;
			arr [0, 1] = 7;

			arr [1, 0] = 88887;
			arr [1, 1] = -987788;

			arr [2, 0] = 6;
			arr [2, 1] = arr [0, 0] * arr [2, 0];

			if (arr [0, 0] != 39)
				return 0;

			if (arr [0, 1] != 7)
				return 0;



			if (arr [1, 0] != 88887)
				return 0;

			if (arr [1, 1] != -987788)
				return 0;



			if (arr [2, 0] != 6)
				return 0;

			if (arr [2, 1] != 39 * 6)
				return 0;

			return 1;
		}

		/// <summary>
		/// short[,] read/write
		/// </summary>
		public static uint CMPShortMultidimensionalArray ()
		{
			short [,] arr = new short [3, 2];

			arr [0, 0] = 39;
			arr [0, 1] = 7;

			arr [1, 0] = 667;
			arr [1, 1] = -876;

			arr [2, 0] = 6;
			arr [2, 1] = (short) (arr [0, 0] * arr [2, 0]);

			if (arr [0, 0] != 39)
				return 0;

			if (arr [0, 1] != 7)
				return 0;



			if (arr [1, 0] != 667)
				return 0;

			if (arr [1, 1] != -876)
				return 0;



			if (arr [2, 0] != 6)
				return 0;

			if (arr [2, 1] != 39 * 6)
				return 0;

			return 1;
		}

		/// <summary>
		/// byte[,] read/write
		/// </summary>
		public static uint CMPByteMultidimensionalArray ()
		{
			byte [,] arr = new byte [3, 2];

			arr [0, 0] = 39;
			arr [0, 1] = 7;

			arr [1, 0] = 123;
			arr [1, 1] = 24;

			arr [2, 0] = 6;
			arr [2, 1] = (byte) (arr [0, 0] * arr [2, 0]);

			if (arr [0, 0] != 39)
				return 0;

			if (arr [0, 1] != 7)
				return 0;



			if (arr [1, 0] != 123)
				return 0;

			if (arr [1, 1] != 24)
				return 0;



			if (arr [2, 0] != 6)
				return 0;

			if (arr [2, 1] != 39 * 6)
				return 0;

			return 1;
		}
		/// <summary>
		/// long[,] read/write
		/// </summary>
		public static uint CMPLongMultidimensionalArray ()
		{
			long [,] arr = new long [3, 2];

			arr [0, 0] = 39;
			arr [0, 1] = 7;

			arr [1, 0] = 88887;
			arr [1, 1] = -987788;

			arr [2, 0] = 6;
			arr [2, 1] = arr [0, 0] * arr [2, 0];

			if (arr [0, 0] != 39)
				return 0;

			if (arr [0, 1] != 7)
				return 0;



			if (arr [1, 0] != 88887)
				return 0;

			if (arr [1, 1] != -987788)
				return 0;



			if (arr [2, 0] != 6)
				return 0;

			if (arr [2, 1] != 39 * 6)
				return 0;

			return 1;
		}
		
		/// <summary>
		/// uint[,] read/write
		/// </summary>
		public static uint CMPUIntMultidimensionalArray ()
		{
			uint [,] arr = new uint [3, 2];

			arr [0, 0] = 39;
			arr [0, 1] = 7;

			arr [1, 0] = 123;
			arr [1, 1] = 24;

			arr [2, 0] = 6;
			arr [2, 1] = arr [0, 0] * arr [2, 0];

			if (arr [0, 0] != 39)
				return 0;

			if (arr [0, 1] != 7)
				return 0;



			if (arr [1, 0] != 123)
				return 0;

			if (arr [1, 1] != 24)
				return 0;



			if (arr [2, 0] != 6)
				return 0;

			if (arr [2, 1] != 39 * 6)
				return 0;

			return 1;
		}

		/// <summary>
		/// ushort[,] read/write
		/// </summary>
		public static uint CMPUShortMultidimensionalArray ()
		{
			ushort [,] arr = new ushort [3, 2];

			arr [0, 0] = 39;
			arr [0, 1] = 7;

			arr [1, 0] = 123;
			arr [1, 1] = 24;

			arr [2, 0] = 6;
			arr [2, 1] = (ushort) (arr [0, 0] * arr [2, 0]);

			if (arr [0, 0] != 39)
				return 0;

			if (arr [0, 1] != 7)
				return 0;



			if (arr [1, 0] != 123)
				return 0;

			if (arr [1, 1] != 24)
				return 0;



			if (arr [2, 0] != 6)
				return 0;

			if (arr [2, 1] != 39 * 6)
				return 0;

			return 1;
		}

		/// <summary>
		/// sbyte[,] read/write
		/// </summary>
		public static uint CMPSByteMultidimensionalArray ()
		{
			sbyte [,] arr = new sbyte [3, 2];

			arr [0, 0] = 39;
			arr [0, 1] = 7;

			arr [1, 0] = 123;
			arr [1, 1] = 24;

			arr [2, 0] = 2;
			arr [2, 1] = (sbyte) (arr [0, 0] * arr [2, 0]);

			if (arr [0, 0] != 39)
				return 0;

			if (arr [0, 1] != 7)
				return 0;



			if (arr [1, 0] != 123)
				return 0;

			if (arr [1, 1] != 24)
				return 0;



			if (arr [2, 0] != 2)
				return 0;

			if (arr [2, 1] != 39 * 2)
				return 0;

			return 1;
		}

		/// <summary>
		/// ulong[,] read/write
		/// </summary>
		public static uint CMPULongMultidimensionalArray ()
		{
			ulong [,] arr = new ulong [3, 2];

			arr [0, 0] = 39;
			arr [0, 1] = 7;

			arr [1, 0] = 123;
			arr [1, 1] = 24;

			arr [2, 0] = 6;
			arr [2, 1] = arr [0, 0] * arr [2, 0];

			if (arr [0, 0] != 39)
				return 0;

			if (arr [0, 1] != 7)
				return 0;



			if (arr [1, 0] != 123)
				return 0;

			if (arr [1, 1] != 24)
				return 0;



			if (arr [2, 0] != 6)
				return 0;

			if (arr [2, 1] != 39 * 6)
				return 0;

			return 1;
		}

		public static uint CMPIntMultidimensionalArrayLength ()
		{
			int [,] arr = new int [3, 2];

			if (arr.Length != 6)
				return 0;

			return 1;
		}
#endif
	}
}
