//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT.Attributes;
using SharpOS.Kernel.ADC;

namespace InternalSystem {
	[TargetNamespace ("System")]
	public class String : InternalSystem.Object {
		private int length;
		private char firstChar;

		public int Length
		{
			get
			{
				return this.length;
			}
		}

		public char this [int index]
		{
			[Label ("System.String.get_Chars(System.Int32)")]
			get
			{
				return GetChar (index);
			}
		}

		private unsafe char GetChar (int index)
		{
			// TODO range checking

			fixed (char* p = &this.firstChar) {
				return p [index];
			}
		}

		public static void __RunTests ()
		{
			__Test1 ();
		}

		public static void __Test1 ()
		{
			string str = "Hello";
			string str2 = "US";
			string str3 = "Longer String Than Most";

			if ((str.Length == 5) && (str2.Length == 2) && (str3.Length == 23))
				TextMode.WriteLine ("System.String.Length: test passed");
			else
				TextMode.WriteLine ("System.String.Length: test FAILED");
		}
	}
}
