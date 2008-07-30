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
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.DeviceSystem;
using SharpOS.Kernel.DeviceSystem.DiskController;

namespace SharpOS.Kernel.FileSystem
{

	public class DiskDevice : Device, IDiskDevice
	{
		private IDiskControllerDevice diskController;
		private uint driveNbr;
		private uint totalSectors;
		private MasterBootBlock mbr;
		private bool readOnly;

		public bool ReadOnly
		{
			get
			{
				return readOnly;
			}
		}

		public bool CanWrite
		{
			get
			{
				return !readOnly;
			}
		}

		public DiskDevice (IDiskControllerDevice diskController, uint driveNbr, bool readOnly)
		{
			base.parent = (diskController as Device);
			base.name = base.parent.Name + "/Disk" + driveNbr.ToString ();
			base.deviceStatus = DeviceStatus.Online;
			this.totalSectors = diskController.GetTotalSectors (driveNbr);
			this.diskController = diskController;
			this.driveNbr = driveNbr;

			if (readOnly)
				this.readOnly = false;
			else
				this.readOnly = this.diskController.CanWrite (driveNbr);

			mbr = new MasterBootBlock (this);

			if (mbr.Valid)
				for (uint i = 0; i < MasterBootBlock.MaxMBRPartitions; i++)
					if (mbr[i].PartitionType != PartitionTypes.Empty)
						new PartitionDevice (mbr[i], this, readOnly);
		}

		public byte[] ReadBlock (uint block, uint count)
		{
			byte[] data = new byte[count * BlockSize];

			diskController.ReadBlock (driveNbr, block, count, data);

			return data;
		}

		public bool ReadBlock (uint block, uint count, byte[] data)
		{
			return diskController.ReadBlock (driveNbr, block, count, data);
		}

		public bool WriteBlock (uint block, uint count, byte[] data)
		{
			return diskController.WriteBlock (driveNbr, block, count, data);
		}

		public uint TotalBlocks
		{
			get
			{
				return totalSectors;
			}
		}

		public uint BlockSize
		{
			get
			{
				return diskController.GetSectorSize (driveNbr);
			}
		}

		public GenericPartition this[uint partitionNbr]
		{
			get
			{
				return mbr[partitionNbr];
			}
		}

	}
}
