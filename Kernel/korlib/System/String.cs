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

namespace Internal.System {
	public class String: Internal.System.Object {
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
