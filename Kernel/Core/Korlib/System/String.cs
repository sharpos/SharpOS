//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System.Runtime.InteropServices;
using SharpOS.AOT.Attributes;
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.Foundation;

namespace InternalSystem {
	[StructLayout (LayoutKind.Sequential)]
	[TargetNamespace ("System")]
	public class String : 
		InternalSystem.Object, 
		System.Collections.IEnumerable 
	{
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
			if (index < 0)
				throw new System.ArgumentOutOfRangeException("index is less than zero.");
			if (index >= this.length)
				throw new System.ArgumentOutOfRangeException("index specifies a position that is not within this string.");

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

			Testcase.Test ((str.Length == 5) && (str2.Length == 2) && (str3.Length == 23),
				"System.String", "Length test");
		}

		private static unsafe bool CompareChars (InternalSystem.String a, InternalSystem.String b)
		{
			fixed (char* pa = &a.firstChar) {
				fixed (char* pb = &b.firstChar) {
					for (int i = 0; i < a.Length; ++i) {
						if (pa [i] != pb [i])
							return false;
					}
				}
			}

			return true;
		}

		[Label ("System.String.op_Equality(System.String,System.String)")]
		public static bool operator == (InternalSystem.String a, InternalSystem.String b)
		{
			if (a.length != b.length)
				return false;
			return String.CompareChars (a, b);
		}

		[Label ("System.String.op_Inequality(System.String,System.String)")]
		public static bool operator != (InternalSystem.String a, InternalSystem.String b)
		{
			return !(a == b);
		}

		public System.Collections.IEnumerator GetEnumerator()
		{
			return new CharEnumerator(this);
		}
		
		public unsafe bool Equals (System.String i)
		{
			return ((InternalSystem.String)(object)i) == this;
		}

		public override unsafe bool Equals (object o)
		{
			//if (!(o is String))
			//	return false;

			String other = (String)o;
			return other == this;
		}
	}
}
