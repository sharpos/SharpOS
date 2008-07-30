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
	public struct UInt16/*:
		IComparable, 
		IFormattable, 
		IConvertible, 
		IComparable<ushort>, 
		IEquatable<ushort>*/ 
	{
#pragma warning disable 649
		internal ushort Value;
#pragma warning restore 649
		
		public bool Equals (System.UInt16 i)
		{
			return i == Value;
		}

		public override bool Equals (object o)
		{
			//if (!(o is UInt16))
			//	return false;

			UInt16 other = (UInt16)o;
			return other.Value == Value;
		}

		public string ToString ()
		{
			return InternalSystem.String.CreateStringImpl (Value, false, false);
		}

		public string ToString (string format)
		{			
			if (format == "X")
				return InternalSystem.String.CreateStringImpl (Value, false, true);
			else
				return ToString ();
		}
	}
}
