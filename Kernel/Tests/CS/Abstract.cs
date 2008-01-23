//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Stanisław Pitucha <viraptor@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

//#define NO_ABSTRACT_SUPPORT
namespace SharpOS.Kernel.Tests.CS {
	public class Abstract {
#if !NO_ABSTRACT_SUPPORT
		private abstract class Base {
			protected int number;

			public Base ()
			{
				this.number = 0;
			}

			public Base (int number)
			{
				this.number = number;
			}

			public abstract int Number {
			        get;
			}

			public int GetNumber ()
			{
				return number;
			}

			public int Get37 ()
			{
				return 37;
			}

			public virtual int Get50override100 ()
			{
				return 50;
			}

			public abstract int GetAbstract42 ();
		}

		private class SubClass : Base {
			public SubClass ()
			{
			}
			public SubClass (int number) : base (number)
			{
			}

			public override int Number
			{
			        get { return number; }
			}

			public int GetInheritedNumber ()
			{
				return GetNumber ();
			}

			public override int Get50override100 ()
			{
				return 100;
			}

			public override int GetAbstract42 ()
			{
			        return 42;
			}
		}

		public static uint CMPGetAbstractProperty ()
		{
			if (new SubClass (37).Number == 37)
			        return 1;

			return 0;
		}

		public static uint CMPCallInherited ()
		{
			if (new SubClass (37).GetNumber () == 37)
				return 1;

			return 0;
		}

		public static uint CMPCallProxiedInherited ()
		{
			if (new SubClass (37).GetInheritedNumber () == 37)
				return 1;

			return 0;
		}

		public static uint CMPCallOverridden ()
		{
			if (new SubClass ().Get50override100 () == 100)
				return 1;

			return 0;
		}

		public static uint CMPCallAbstractMember ()
		{
			if (new SubClass ().GetAbstract42 () == 42)
			        return 1;

			return 0;
		}
#else
		public static uint CMPImplement ()
		{
			return 0;
		}
#endif
	}
}
