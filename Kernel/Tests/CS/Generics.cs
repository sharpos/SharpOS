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
#else
		public static uint CMPGenerics ()
		{
			return 0;
		}
#endif
	}
}
