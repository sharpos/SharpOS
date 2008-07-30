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

namespace InternalSystem
{
	[TargetNamespace ("System")]
	public struct Int32 /*:
		IComparable, 
		IFormattable, 
		IConvertible, 
		IComparable<int>, 
		IEquatable<int>*/
	{
#pragma warning disable 649
		internal int Value;
#pragma warning restore 649

		public bool Equals (System.Int32 i)
		{
			return i == Value;
		}

		public override bool Equals (object o)
		{
			//if (!(o is Int32))
			//	return false;

			Int32 other = (Int32)o;
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
