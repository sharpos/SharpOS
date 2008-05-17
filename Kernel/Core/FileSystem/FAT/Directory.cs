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
	public class Directory : DirectoryNode
	{
		protected uint cluster;

		public Directory (IFileSystem fs, uint cluster)
			: base (fs)
		{
			this.cluster = cluster;
		}

		public override IVfsNode Create (string name, VfsNodeType type, object settings)
		{
			return null;
		}

		public override IVfsNode Lookup (string name)
		{
			FileSystem fs = ((FileSystem)this.FileSystem);

			DirectoryEntryLocation location = fs.FindEntry (new FatEntityComparer (name), this.cluster);

			if (!location.Valid)
				return null;

			if (location.Directory)
				return new Directory ((IFileSystem)fs, location.Block);
			else
				return new File ((IFileSystem)fs, location.Block);
		}

		public override object Open (System.IO.FileAccess access, System.IO.FileShare share)
		{
			return null;
		}

		public override void Delete (IVfsNode child, DirectoryEntry dentry)
		{
			FileSystem fs = ((FileSystem)this.FileSystem);
			uint childBlock = ((Directory)child).cluster;

			DirectoryEntryLocation location = fs.FindEntry (new FatMatchClusterComparer (childBlock), this.cluster);

			if (!location.Valid)
				throw new System.ArgumentException (); //throw new IOException ("Unable to delete directory because it is not empty");

			((FileSystem)this.FileSystem).Delete (childBlock, cluster, location.Index);
		}

	}
}