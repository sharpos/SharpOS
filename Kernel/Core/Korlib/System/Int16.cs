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
	public struct Int16/*:
		IComparable, 
		IFormattable, 
		IConvertible, 
		IComparable<short>, 
		IEquatable<short>*/ 
	{
#pragma warning disable 649
		internal short Value;
#pragma warning restore 649
		
		public bool Equals (System.Int16 i)
		{
			return i == Value;
		}

		public override bool Equals (object o)
		{
			//if (!(o is Int16))
			//	return false;

			Int16 other = (Int16)o;
			return other.Value == Value;
		}

		public string ToString ()
		{
			return InternalSystem.String.CreateStringImpl ((uint)Value, true, false);
		}

		public string ToString (string format)
		{	
			if (format == "X")
				return InternalSystem.String.CreateStringImpl ((uint)Value, true, true);
			else
				return ToString ();
		}
	}
}
