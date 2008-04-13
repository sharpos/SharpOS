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
	interface IFileSystemService {
		//IFileSystem MountDevice (char[] path);
		IFileSystem MountDevice (string path);
		IFileSystem Format();
	}
}
