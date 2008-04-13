//
// (C) 2006-2008 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Michael Ruck (aka grover) <sharpos@michaelruck.de>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

namespace SharpOS.Kernel.Vfs {
	/// <summary>
	/// Specifies the type of the node in the virtual file system.
	/// </summary>
    public enum VfsNodeType {

		/// <summary>
		/// An unknown node type.
		/// </summary>
		Unknown,

		/// <summary>
		/// Represents a classic file (represented by a System.IO.Stream or derived class.
		/// </summary>
        File,

		/// <summary>
		/// Represents a folder (aka directory) in the file system.
		/// </summary>
        Directory,

		/// <summary>
		/// A symbolic link in the (virtual) filesystem.
		/// </summary>
		SymbolicLink,

		/// <summary>
		/// Represents a basic device node.
		/// </summary>
        Device
    }
}
