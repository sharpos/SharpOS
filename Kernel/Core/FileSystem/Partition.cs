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
using SharpOS.Kernel.BlockDevice;

namespace SharpOS.Kernel.FileSystem
{
	public class Partition
	{
		private GenericDisk genericdisk;
		private ushort partitionnbr;
		private uint startsector;
		private uint totalsectors;
		private byte type;
		private bool bootable;
		private uint[] guid;	// for Guid Partition Table (GPT)

		public Partition(GenericDisk genericdisk, ushort partitionnbr, uint startsector, uint totalsectors, byte type, bool bootable)
		{
			this.genericdisk = genericdisk;
			this.partitionnbr = partitionnbr;
			this.startsector = startsector;
			this.totalsectors = totalsectors;
			this.type = type;
			this.bootable = bootable;
			this.guid = new uint[4];
		}
	}
}
