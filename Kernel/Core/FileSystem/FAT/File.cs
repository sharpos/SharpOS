//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.Kernel.Vfs;

namespace SharpOS.Kernel.FileSystem.Fat
{
	public class File : NodeBase
	{
		protected uint cluster;

		public File (IFileSystem fs, uint cluster)
			: base (fs, VfsNodeType.File)
		{
			this.cluster = cluster;
		}

		public override IVfsNode Create (string name, VfsNodeType type, object settings)
		{
			return null;
		}

		public override IVfsNode Lookup (string name)
		{
			return null;	// Lookup() method doesn't make sense here
		}

		public override object Open (System.IO.FileAccess access, System.IO.FileShare sharing)
		{
			return null;	// TODO
		}

		public override void Delete (IVfsNode child, DirectoryEntry dentry)
		{
			throw new System.ArgumentException (); // Delete() method doesn't make sense here
		}
	}
}