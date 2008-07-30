//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//  Phil Garcia (aka tgiphil) <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT.Attributes;
using SharpOS.Korlib.Runtime;

namespace InternalSystem
{
	[TargetNamespace ("System")]
	public unsafe struct IntPtr
	{
		private void *value;

		public static readonly IntPtr Zero;

		public override bool Equals (object o)
		{
			if (!(o is System.IntPtr))
				return false;

			return ((IntPtr)o).value == value;
		}

		public override int GetHashCode ()
		{
			return (int)value;
		}

		public static bool operator == (IntPtr a, IntPtr b)
		{
			return (a.value == b.value);
		}

		public static bool operator != (IntPtr a, IntPtr b)
		{
			return (a.value != b.value);
		}

	}
}
