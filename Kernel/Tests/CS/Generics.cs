//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

//#define GENERICS_NOT_SUPPORTED

namespace SharpOS.Kernel.Tests.CS {
	public class Generics {
#if !GENERICS_NOT_SUPPORTED
		private class GenericType<Type> {
			public static int StaticValue = 0;

			public int FieldValue;

			public static Type StaticGenericType;

			private Type _value;

			public Type Value
			{
				get
				{
					return _value;
				}
				set
				{
					_value = value;
				}
			}
		}

		private class GenericType2<Type>
		{
			public GenericType<Type> test = new GenericType<Type>();
		}

		private static Type If<Type> (bool condition, Type first, Type second)
		{
			if (condition)
				return first;

			return second;
		}

		public unsafe static uint CMPGenericMethod ()
		{
			return Generics.If<uint> (false, 0, 1);
		}

		private static GenericType<uint> GetGenericTypeData ()
		{
			GenericType<uint> result = new GenericType<uint> ();

			result.Value = 1;

			return result;
		}

		public unsafe static uint CMPGenericType ()
		{
			return GetGenericTypeData ().Value;
		}

		/*
		private static GenericType2<uint> GetGenericType2Data ()
		{
			GenericType2<uint> result = new GenericType2<uint> ();

			result.test.Value = 1;

			return result;
		}

		public unsafe static uint CMPGenericType2 ()
		{
			return GetGenericType2Data ().test.Value;
		}*/


#else
		public static uint CMPGenerics ()
		{
			return 0;
		}
#endif
	}
}
