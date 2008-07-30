//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//  Phil Garcia (aka tgiphil) <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.Kernel.Vfs;

namespace SharpOS.Kernel.FileSystem.FATFileSystem
{
	public class VfsFile : NodeBase
	{
		protected uint fileCluster;
		protected uint directorySector;
		protected uint directoryIndex;

		public VfsFile (IFileSystem fs, uint fileCluster, uint directorySector, uint directoryIndex)
			: base (fs, VfsNodeType.File)
		{
			this.fileCluster = fileCluster;
			this.directorySector = directorySector;
			this.directoryIndex = directoryIndex;
		}

		public override IVfsNode Create (string name, VfsNodeType type, object settings)
		{
			// TODO
			throw new System.NotSupportedException ("file write not supported");
		}

		public override IVfsNode Lookup (string name)
		{
			return null;	// Lookup() method doesn't make sense here
		}

		public override object Open (System.IO.FileAccess access, System.IO.FileShare sharing)
		{
			if (access == System.IO.FileAccess.Read)
				return new FATFileStream ((FileSystem as VfsFileSystem).FAT, fileCluster, directorySector, directoryIndex); 

			// TODO
			throw new System.NotSupportedException ("file write not supported");
		}

		public override void Delete (IVfsNode child, DirectoryEntry dentry)
		{
			throw new System.ArgumentException (); // Delete() method doesn't make sense here
		}
	}
}