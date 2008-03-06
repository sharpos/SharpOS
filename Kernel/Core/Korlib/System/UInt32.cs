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
	public struct UInt32/*:
		IComparable, 
		IFormattable, 
		IConvertible, 
		IComparable<uint>, 
		IEquatable<uint>*/ 
	{
#pragma warning disable 649
		internal uint Value;
#pragma warning restore 649
		
		public unsafe bool Equals (System.UInt32 i)
		{
			return i == Value;
		}

		public override unsafe bool Equals (object o)
		{
			//if (!(o is UInt32))
			//	return false;

			UInt32 other = (UInt32)o;
			return other.Value == Value;
		}
	}
}
