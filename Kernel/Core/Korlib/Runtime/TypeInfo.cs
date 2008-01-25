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
using SharpOS.AOT.Metadata;

namespace SharpOS.Korlib.Runtime {
	[SharpOS.AOT.Attributes.TypeInfo]
	[StructLayout (LayoutKind.Sequential)]
	internal class TypeInfo : InternalSystem.Object {
		internal string Name;
		internal TypeInfo Base;
		internal AssemblyMetadata Assembly;
		internal uint MetadataToken;
	}
}
