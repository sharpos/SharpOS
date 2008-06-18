//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Stanisław Pitucha <viraptor@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

namespace SharpOS.Kernel.Tests.CS {
	public class Interface {
		private interface Iface1 {
			int GetNumber ();
			int Get100 ();
			int Number
			{
				get;
			}
			void Key1_3();
			void Key1_4();
			void Key2_0();
		}

		private interface Iface2 {
			int GetNumber ();
		}

		private unsafe interface Iface3 {
			byte *GetPointer ();
		}

		private interface IfaceOverrides {
			int Add (int num1, int num2);
			int Add (int num1, int num2, int num3);
			int Add (int num1, short num2);
		}

		private class ClassOverrides : IfaceOverrides {
			public ClassOverrides ()
			{
			}

			public int Add (int num1, int num2)
			{
				return num1 + num2;
			}

			public int Add (int num1, int num2, int num3)
			{
				return num1 + num2 + num3;
			}

			public int Add (int num1, short num2)
			{
				return num1 + (int)num2;
			}
		}

		private class Class1 : Iface2 {
			public Class1 ()
			{
			}

			public int GetNumber ()
			{
				return 69;
			}
		}

		private class Class2 : Iface1, Iface2 {
			public Class2 ()
			{
			}

			public int Number
			{
				get
				{
					return 42;
				}
			}

			public int Get100 ()
			{
				return 100;
			}

			int Iface1.GetNumber ()
			{
				return 42;
			}

			int Iface2.GetNumber ()
			{
				return 69;
			}

			public void Key1_3() {}
			public void Key1_4() {}
			public void Key2_0() {}
		}

		private unsafe class Class3 : Iface3 {
			public Class3 ()
			{
			}

			public byte *GetPointer ()
			{
				return (byte*) 0x42;
			}
		}

		private class Class4 : Iface2 {
			public Class4 ()
			{
			}

			public int GetNumber ()
			{
				return 72;
			}

			int Iface2.GetNumber ()
			{
				return 69;
			}
		}

		public interface BaseInterface {
			int TestFunction ();
		}

		public class BaseInterfaceImplementation : BaseInterface {
			public int TestFunction () { return 10; }
		}
		
		public class InheritedClass : BaseInterfaceImplementation {}
		
		public static uint CMPInterfaceImplementationInherited ()
		{
			InheritedClass instance = new InheritedClass ();

			if (instance.TestFunction () == 10)
				return 1;

			return 0;
		}

		public static uint CMPBasic ()
		{
			Iface2 c = new Class1 ();

			if (c.GetNumber () == 69)
				return 1;

			return 0;
		}

		public static uint CMPExplicitImplementation ()
		{
			Iface2 c = new Class4 ();

			if (c.GetNumber () == 69)
				return 1;

			return 0;
		}

		public static uint CMPExplicitImplementation2 ()
		{
			Class4 c = new Class4 ();

			if (c.GetNumber () == 72)
				return 1;

			return 0;
		}

		public unsafe static uint CMPPointerMethod ()
		{
			Iface3 c = new Class3 ();

			if ((uint) c.GetPointer() == 0x42)
				return 1;

			return 0;
		}

		public static uint CMPOverrides1 ()
		{
			IfaceOverrides ov = new ClassOverrides ();

			if (ov.Add (30, 12) == 42)
				return 1;

			return 0;
		}

		public static uint CMPOverrides2 ()
		{
			IfaceOverrides ov = new ClassOverrides ();

			if (ov.Add (30, 10, 2) == 42)
				return 1;

			return 0;
		}

		public static uint CMPOverrides3 ()
		{
			IfaceOverrides ov = new ClassOverrides ();
			short num2 = 11;

			if (ov.Add (31, num2) == 42)
				return 1;

			return 0;
		}

		public static uint CMPGetProperty ()
		{
			if (new Class2 ().Number == 42)
			        return 1;

			return 0;
		}

		public static uint CMPCallGet100 ()
		{
			if (new Class2 ().Get100() == 100)
			        return 1;

			return 0;
		}

		public static uint CMPCallChosenInterface1 ()
		{
			if ((new Class2 () as Iface1).GetNumber() == 42)
			        return 1;

			return 0;
		}

		public static uint CMPCallChosenInterface2 ()
		{
			if ((new Class2 () as Iface2).GetNumber() == 69)
			        return 1;

			return 0;
		}

		public static uint CMPIsOperator ()
		{
			object obj = new object();

			if (obj is Iface2)
				return 0;


			return 1;
		}

		public static uint CMPIsOperator2 ()
		{
			Class1 cl = new Class1();

			if (!(cl is Class1))
				return 0;

			return 1;
		}
	}
}
