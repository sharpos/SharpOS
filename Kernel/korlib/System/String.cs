//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using SharpOS.AOT.Attributes;

namespace System {
	public class String {
		private int capacity;
		private int length;
		private char firstChar;

		public int Length {
			get {
				return this.length;
			}
		}

		public char this [int index] {
			[Label ("System.String.get_Chars(System.Int32)")]
			get {
				return GetChar (index);
			}
		}

		private unsafe char GetChar (int index)
		{
			// TODO range checking

			fixed (char* p = &this.firstChar) {
				return p [index];
			}
		}
	}
}
