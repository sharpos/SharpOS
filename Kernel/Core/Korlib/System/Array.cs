//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System.Runtime.InteropServices;
using SharpOS.AOT.Attributes;
using SharpOS.Kernel.ADC;

namespace InternalSystem {
	[StructLayout (LayoutKind.Sequential)]
	[TargetNamespace ("System")]
	public abstract class Array : InternalSystem.Object
		/*
		ICloneable, 
		IList, 
		ICollection, 
		IEnumerable
		*/
	{
		[StructLayout (LayoutKind.Sequential)]
		internal struct BoundEntry {
			internal int LowerBound;
			internal int Length;
		}

		internal int Rank;
		internal BoundEntry FirstEntry;

		public unsafe int InternalLength
		{
			get
			{
				int result = this.FirstEntry.Length;

				fixed (BoundEntry* entries = &this.FirstEntry) {
					for (int i = 1; i < this.Rank; i++)
						result *= entries [i].Length;
				}

				return result;
			}
		}

		public int Length
		{
			get
			{
				return this.InternalLength;
			}
		}
	}
}
