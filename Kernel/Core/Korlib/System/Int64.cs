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
	public struct Int64/*:
		IComparable, 
		IFormattable, 
		IConvertible, 
		IComparable<long>, 
		IEquatable<long>*/ 
	{
#pragma warning disable 649
		internal long Value;
#pragma warning restore 649
		
		public bool Equals (System.Int64 i)
		{
			return i == Value;
		}

		public override bool Equals (object o)
		{
			//if (!(o is Int64))
			//	return false;

			Int64 other = (Int64)o;
			return other.Value == Value;
		}
	}
}
