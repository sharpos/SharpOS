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
		private interface IInterface {
			void CallMe ();
		}

		private class BaseClass {
			public uint CallMeToo ()
			{
				return 0xdeadbeef;
			}
		}

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

			public virtual uint DoSomething (Type value)
			{
				return 0;	
			}

			public void CallMe ()
			{

			}
		}

		private class GenericTypeTwo<Type>: GenericType<Type> {
			public override uint DoSomething (Type value)
			{
				return 1;
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

		private static GenericTypeTwo<uint> GetGenericTypeTwoData ()
		{
			GenericTypeTwo<uint> result = new GenericTypeTwo<uint> ();

			result.Value = 1;

			return result;
		}

		public unsafe static uint CMPGenericTypeTwo ()
		{
			return GetGenericTypeTwoData ().Value;
		}


		public unsafe static uint CMPGenericTypeTwoVirtualMethods ()
		{
			/*GenericTypeTwo<uint> result = new GenericTypeTwo<uint> ();

			return result.DoSomething (6);*/

			return 0;
		}

		private class SubGenericType<Type>
		{
			public GenericType<Type> test = new GenericType<Type>();
		}

		
		private static SubGenericType<uint> GetSubGenericTypeData ()
		{
			SubGenericType<uint> result = new SubGenericType<uint> ();

			result.test.Value = 1;

			return result;
		}
		
		public unsafe static uint CMPSubGenericType ()
		{
			return GetSubGenericTypeData ().test.Value;
		}


#else
		public static uint CMPGenerics ()
		{
			return 0;
		}
#endif
	}
}
