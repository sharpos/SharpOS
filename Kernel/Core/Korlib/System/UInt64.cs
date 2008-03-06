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
	public struct UInt64/*:
		IComparable, 
		IFormattable, 
		IConvertible, 
		IComparable<ulong>, 
		IEquatable<ulong>*/ 
	{
#pragma warning disable 649
		internal ulong Value;
#pragma warning restore 649
		
		public bool Equals (System.UInt64 i)
		{
			return i == Value;
		}

		public override bool Equals (object o)
		{
			//if (!(o is UInt64))
			//	return false;

			UInt64 other = (UInt64)o;
			return other.Value == Value;
		}
	}
}
