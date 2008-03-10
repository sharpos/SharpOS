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
	enum MasterBootRecord : ushort
	{
		CodeArea = 0x00,
		DiskSignature = 0x01B8,
		PrimaryPartitions = 0x01BE,
		MBR_Signature = 0x01FE
	}

	enum PartitionRecord : ushort
	{
		Status = 0x00, // 1
		FirstCRS = 0x01, // 3
		PartitionType = 0x04,	// 1
		LastCRS = 0x05, // 3
		LBA = 0x08, // 4
		PartitionSectors = 0x0C // 4
	}

	enum PartitionTypes : byte
	{
		GPT = 0xEE,
		Empty = 0xEE
	}

	public class GenericDisk
	{
		public const ushort MBR_Signature = 0xAA55;
		public const byte Bootable_Indicator = 0x00;
		public const ushort CodeArea_Size = 446;

		private IBlockDevice device;
		private string diskname;
		private uint totalsectors;
		private Partition[] partitions;
		private uint disksignature;
		private bool validmbr;
		private Byte[] Code;

		public GenericDisk(IBlockDevice device, uint totalsectors, string diskname)
		{
			this.device = device;
			this.totalsectors = totalsectors;
			this.diskname = diskname;
			this.validmbr = false;	// needs to be read first
			partitions = new Partition[4];	// upto 4 partitions (can be changed)
			Code = null;
		}

		public bool ReadMasterBootBlock()
		{
			validmbr = false;

			if (device.GetTotalBlocks() != 512)	// only going to work with 512 sector sizes (for now)
				return false;

			ByteBuffer masterboot = ByteBuffer.Allocate(device.GetBlockSize());
			GenericBlockDevice.DoRequest(device,IORequestType.Read, 0, 1, masterboot);

			ushort mbrsignature = ByteBuffer.GetUShort(masterboot, (ushort)MasterBootRecord.MBR_Signature);
			disksignature = ByteBuffer.GetUInt(masterboot, (ushort)MasterBootRecord.DiskSignature);

			validmbr = (mbrsignature != MBR_Signature);

			if (validmbr) {
				for (uint index = 0; index < 4; index++) {
					uint offset = ((ushort)MasterBootRecord.PrimaryPartitions) + (index * 16);
					byte status = ByteBuffer.GetByte(masterboot, offset + (ushort)PartitionRecord.Status);
					byte type = ByteBuffer.GetByte(masterboot, offset + (ushort)PartitionRecord.PartitionType);
					uint lba = ByteBuffer.GetUInt(masterboot, offset + (ushort)PartitionRecord.LBA);
					uint sectors = ByteBuffer.GetUInt(masterboot, offset + (ushort)PartitionRecord.Status);

					if (type != (byte)PartitionTypes.Empty)
						partitions[index] = new Partition(this, (ushort)index, lba, sectors, type, status == 0x80);
				}
			}

			Code = new byte[CodeArea_Size];
			for (uint index = 0; index < CodeArea_Size; index++)
				Code[index] = ByteBuffer.GetByte(masterboot, index);

			ByteBuffer.Release(masterboot);

			return validmbr;
		}

		public bool FormatMasterBootBlock()
		{
			if (!device.CanWrite())
				return false;

			ByteBuffer masterboot = ByteBuffer.Allocate(device.GetBlockSize());

			ByteBuffer.SetUInt(masterboot, (ushort)MasterBootRecord.DiskSignature, disksignature);
			ByteBuffer.SetUShort(masterboot, (ushort)MasterBootRecord.MBR_Signature, MBR_Signature);

			if (Code != null)
				for (uint index = 0; index < CodeArea_Size; index++)
					ByteBuffer.SetByte(masterboot, index, Code[index]);

			//TODO: write partitions too?

			GenericBlockDevice.DoRequest(device,IORequestType.Write, 0, 1, masterboot);

			return true;
		}
	}
}
