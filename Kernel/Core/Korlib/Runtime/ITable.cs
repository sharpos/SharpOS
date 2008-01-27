//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Stanislaw Pitucha <viraptor@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System.Runtime.InteropServices;

namespace SharpOS.Korlib.Runtime {
	[SharpOS.AOT.Attributes.ITable]
	[StructLayout (LayoutKind.Sequential)]
	internal class ITable : InternalSystem.Object {
	}
}
