//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System.Runtime.InteropServices;
using SharpOS.AOT.Attributes;
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.Foundation;

namespace InternalSystem {
	[TargetNamespace ("System")]
	public sealed class CharEnumerator : 
		System.ICloneable, 
		//System.Collections.Generic.IEnumerator<char>, 
		System.Collections.IEnumerator
	{
		private InternalSystem.String	internalString;
		private int						index;

		internal CharEnumerator (InternalSystem.String _string)
		{
			internalString = _string;
			index = -1;
		}

		public void Reset ()
		{
			index = -1;
		}

		public bool MoveNext ()
		{
			index ++;
			return (index >= internalString.Length);
		}

		public char Current 
		{
			get 
			{
				if (index == -1 || 
					index >= internalString.Length)
					throw new System.InvalidOperationException(
						"The position is invalid.");
				return internalString [index];
			}
		}

		object System.Collections.IEnumerator.Current  { get { return Current; } }

		public void Dispose() { }

		public object Clone ()
		{
			CharEnumerator enumerator = new CharEnumerator(internalString);
			enumerator.index = index;
			return enumerator;
		}
	}
}
