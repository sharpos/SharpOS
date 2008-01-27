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

		private class Class : Iface1, Iface2 {
			public Class ()
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

		public static uint CMPGetProperty ()
		{
			if (new Class ().Number == 42)
			        return 1;

			return 0;
		}

		public static uint CMPCallGet100 ()
		{
			if (new Class ().Get100() == 100)
			        return 1;

			return 0;
		}

		public static uint CMPCallChosenInterface1 ()
		{
			if ((new Class () as Iface1).GetNumber() == 42)
			        return 1;

			return 0;
		}

		public static uint CMPCallChosenInterface2 ()
		{
			if ((new Class () as Iface2).GetNumber() == 69)
			        return 1;

			return 0;
		}
	}
}
