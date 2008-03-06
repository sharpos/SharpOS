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
	public struct Char/*:
		IComparable, 
		IFormattable, 
		IConvertible, 
		IComparable<char>, 
		IEquatable<char>*/ 
	{
#pragma warning disable 649
		internal char Value;
#pragma warning restore 649
		
		public bool Equals (System.Char i)
		{
			return i == Value;
		}

		public override bool Equals (object o)
		{
			//if (!(o is Char))
			//	return false;

			Char other = (Char)o;
			return other.Value == Value;
		}
	}
}
