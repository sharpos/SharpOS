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
using SharpOS.Kernel.DriverSystem.Drivers.Block;

namespace SharpOS.Kernel.FileSystem
{
	public struct PartitionDescription
	{
		public ushort partitionnbr;
		public uint startsector;
		public uint totalsectors;
		public byte type;
		public bool bootable;
		public uint[] guid;	// for Guid Partition Table (GPT)

		public PartitionDescription (ushort partitionnbr, uint startsector, uint totalsectors, byte type, bool bootable)
		{
			this.partitionnbr = partitionnbr;
			this.startsector = startsector;
			this.totalsectors = totalsectors;
			this.type = type;
			this.bootable = bootable;
			this.guid = null;
		}
	}

	public class Partition
	{
		private GenericDisk genericdisk;
		private PartitionDescription partition;

		public Partition (GenericDisk genericdisk, PartitionDescription partition)
		{
			this.genericdisk = genericdisk;
			this.partition = partition;
		}

	}
}
