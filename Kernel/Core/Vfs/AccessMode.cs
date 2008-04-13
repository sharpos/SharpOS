//
// (C) 2006-2008 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Michael Ruck (aka grover) <sharpos@michaelruck.de>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;

namespace SharpOS.Kernel.Vfs {
	/// <summary>
	/// The access mode enumeration is used to determine accessibility of the file by the immediate caller. <see cref="SharpOS.Kernel.Vfs.VirtualFileSystem.Access(System.String,SharpOS.Kernel.Vfs.AccessMode)"/> for more information.
	/// </summary>
	[ Flags ]
	public enum AccessMode {

		/// <summary>
		/// Check if the caller has the right to traverse the resource.
		/// </summary>
		Traverse,

		/// <summary>
		/// Check if the caller has read access to the resource.
		/// </summary>
		Read,

		/// <summary>
		/// Check if the caller has write access to the resource.
		/// </summary>
		Write,

		/// <summary>
		/// Check if the caller has delete access to the resoure.
		/// </summary>
		Delete,

		/// <summary>
		/// Check if the caller has execute permissions for the resource.
		/// </summary>
		Execute,

		/// <summary>
		/// Check if the named resource is available.
		/// </summary>
		Exists
	}
}