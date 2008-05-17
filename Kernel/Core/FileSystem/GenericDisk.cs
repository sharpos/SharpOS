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
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.DriverSystem;
using SharpOS.Kernel.DriverSystem.Drivers.Block;

namespace SharpOS.Kernel.FileSystem
{
	internal struct MasterBootRecord
	{
		internal const uint CodeArea = 0x00;
		internal const uint DiskSignature = 0x01B8;
		internal const uint PrimaryPartitions = 0x01BE;
		internal const uint MBRSignature = 0x01FE;
	}

	internal struct MasterBootConstants
	{
		internal const ushort MBRSignature = 0xAA55;
		internal const byte BootableIndicator = 0x00;
		internal const ushort CodeAreaSize = 446;
	}

	internal struct PartitionRecord
	{
		internal const uint Status = 0x00; // 1
		internal const uint FirstCRS = 0x01; // 3
		internal const uint PartitionType = 0x04;	// 1
		internal const uint LastCRS = 0x05; // 3
		internal const uint LBA = 0x08; // 4
		internal const uint PartitionSectors = 0x0C; // 4
	}

	internal struct PartitionTypes
	{
		internal const byte GPT = 0xEE;
		internal const byte Empty = 0x00;
		internal const byte ExtendedPartition = 0x0F;
		internal const byte OldExtendedPartition = 0x05; // limited to disks under 8.4Gb
	}

	public class GenericDisk
	{
		private IBlockDevice device;
		//private string diskname;
		private uint totalsectors;
		private Partition[] partitions;
		private uint disksignature;
		private bool validmbr;
		private Byte[] Code;

		public const uint MaxSupportedParitions = 256;
		public const uint MaxMBRParitions = 4;

		public GenericDisk (IBlockDevice device)
		{
			this.device = device;
			this.totalsectors = device.GetTotalSectors ();
			//			this.diskname = diskname;
			this.validmbr = false;	// needs to be read first
			partitions = new Partition[MaxSupportedParitions];
			Code = null;
		}

		public bool ReadMasterBootBlock ()
		{
			validmbr = false;

			if (device.GetSectorSize () != 512)	// only going to work with 512 sector sizes
				return false;

			MemoryBlock masterboot = new MemoryBlock (512);

			device.ReadBlock (0, 1, masterboot);

			ushort mbrsignature = masterboot.GetUShort (MasterBootRecord.MBRSignature);
			disksignature = masterboot.GetUInt (MasterBootRecord.DiskSignature);

			validmbr = (mbrsignature == MasterBootConstants.MBRSignature);

			if (validmbr) {
				for (uint index = 0; index < MaxMBRParitions; index++) {

					uint offset = MasterBootRecord.PrimaryPartitions + (index * 16);
					byte status = masterboot.GetByte (offset + PartitionRecord.Status);
					byte type = masterboot.GetByte (offset + PartitionRecord.PartitionType);
					uint startinglba = masterboot.GetUInt (offset + PartitionRecord.LBA);
					uint sectors = masterboot.GetUInt (offset + PartitionRecord.Status);

					PartitionDescription partition = new PartitionDescription ((ushort)index, startinglba, sectors, type, status == 0x80);

					if (type != PartitionTypes.Empty) {
						partitions[index] = new Partition (this, partition);

						TextMode.Write ("partition #", (int)index);
						TextMode.Write (" found, lba=", (int)startinglba);
						TextMode.Write (", count=", (int)sectors);
						TextMode.Write (", type=", (int)type);

						// for testing
						IBlockDevice block = new GenericBlockDevicePartition (device, startinglba, sectors);
						if (Fat.FileSystem.IsFat (block))
							TextMode.Write (" (fat)");

						TextMode.WriteLine ();

					}
				}
				//TODO: Follow Extended Partitions
			}

			Code = new byte[MasterBootConstants.CodeAreaSize];
			for (uint index = 0; index < MasterBootConstants.CodeAreaSize; index++)
				Code[index] = masterboot.GetByte (index);

			masterboot.Release ();

			return validmbr;
		}

		public bool FormatMasterBootBlock ()
		{
			if (!device.CanWrite ())
				return false;

			MemoryBlock masterboot = new MemoryBlock (512);

			masterboot.SetUInt (MasterBootRecord.DiskSignature, disksignature);
			masterboot.SetUShort (MasterBootRecord.MBRSignature, MasterBootConstants.MBRSignature);

			if (Code != null)
				for (uint index = 0; index < MasterBootConstants.CodeAreaSize; index++)
					masterboot.SetByte (index, Code[index]);

			//TODO: write partitions too?

			device.WriteBlock (0, 1, masterboot);

			return true;
		}
	}
}
