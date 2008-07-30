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
using SharpOS.Kernel.FileSystem;

namespace SharpOS.Kernel.FileSystem.FATFileSystem
{
	public class VfsDirectory : DirectoryNode
	{
		protected uint directoryCluster;

		public VfsDirectory (IFileSystem fs, uint directoryCluster)
			: base (fs)
		{
			this.directoryCluster = directoryCluster;
		}

		public override IVfsNode Create (string name, VfsNodeType type, object settings)
		{
			return null;
		}

		public override IVfsNode Lookup (string name)
		{
			FAT.DirectoryEntryLocation location = (FileSystem as VfsFileSystem).FAT.FindEntry (new FAT.FatEntityComparer (name), this.directoryCluster);

			if (!location.Valid)
				return null;

			if (location.IsDirectory)
				return new VfsDirectory (FileSystem, location.Block);
			else
				return new VfsFile (FileSystem, location.Block, location.DirectorySector, location.DirectoryIndex);
		}

		public override object Open (System.IO.FileAccess access, System.IO.FileShare share)
		{
			return null;
		}

		public override void Delete (IVfsNode child, DirectoryEntry dentry)
		{
			FAT fs = this.FileSystem as FAT;

			uint childBlock = (child as VfsDirectory).directoryCluster;

			FAT.DirectoryEntryLocation location = fs.FindEntry (new FAT.FatMatchClusterComparer (childBlock), this.directoryCluster);

			if (!location.Valid)
				throw new System.ArgumentException (); //throw new IOException ("Unable to delete directory because it is not empty");

			fs.Delete (childBlock, directoryCluster, location.DirectoryIndex);
		}

	}
}