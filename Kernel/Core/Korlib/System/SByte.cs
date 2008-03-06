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
	public struct SByte/*:
		IComparable, 
		IFormattable, 
		IConvertible, 
		IComparable<sbyte>, 
		IEquatable<sbyte>*/ 
	{
#pragma warning disable 649
		internal sbyte Value;
#pragma warning restore 649
		
		public bool Equals (System.SByte i)
		{
			return i == Value;
		}

		public override bool Equals (object o)
		{
			//if (!(o is SByte))
			//	return false;

			SByte other = (SByte)o;
			return other.Value == Value;
		}
	}
}
