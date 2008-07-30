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
using SharpOS.Kernel.DeviceSystem;
using SharpOS.Kernel.Vfs;

namespace SharpOS.Kernel.FileSystem
{
	public abstract class GenericFileSystem
	{
		protected IPartitionDevice partition;
		protected uint blockSize;
		protected bool valid;
		protected string volumeLabel;
		protected byte[] serialNbr;

		public GenericFileSystem (IPartitionDevice partition)
		{
			this.partition = partition;
			this.blockSize = partition.BlockSize;
			this.valid = false;
			this.volumeLabel = string.Empty;
			this.serialNbr = new byte[0];
		}

		abstract public IFileSystem CreateVFSMount ();

		public bool Valid
		{
			get
			{
				return valid;
			}
		}

		public string VolumeLabel
		{
			get
			{
				return volumeLabel;
			}
		}
		public byte[] SerialNbr
		{
			get
			{
				return serialNbr;
			}
		}

	}
}
