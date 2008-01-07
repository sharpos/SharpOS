namespace SharpOS.Kernel.Tests.CS
{
	public class Inheritance
	{
		private class Base
		{
			int number;

			public Base ()
			{
				this.number = 0;
			}

			public Base (int number)
			{
				this.number = number;
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

			public int Get58shadowWith69 ()
			{
				return 58;
			}
		}

		private class SubClass : Base
		{
			public SubClass () { }
			public SubClass (int number) : base (number) { }

			public int GetInheritedNumber ()
			{
				return GetNumber ();
			}

			public override int Get50override100 ()
			{
				return 100;
			}

			public new int Get58shadowWith69 ()
			{
				return 69;
			}
		}

		public static uint CMPCallInherited ()
		{
			if (new SubClass (37).GetNumber () == 37)
				return 1;

			return 0;
		}

		public static uint CMPCallProxiedInherited ()
		{
			if (new SubClass (37).GetInheritedNumber() == 37)
				return 1;

			return 0;
		}

		public static uint CMPCallOverridden ()
		{
			if (new SubClass ().Get50override100() == 100)
				return 1;

			return 0;
		}

		public static uint CMPCallShadowedMember ()
		{
			if (new SubClass ().Get58shadowWith69() == 69)
				return 1;

			return 0;
		}
	}
}
