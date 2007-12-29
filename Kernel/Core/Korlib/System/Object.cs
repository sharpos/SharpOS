//
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.Korlib.Runtime;

namespace Internal.System {
	public class Object {
		internal VTable VTable;
		internal uint Synchronisation;

		public Object ()
		{
		}
	}
}