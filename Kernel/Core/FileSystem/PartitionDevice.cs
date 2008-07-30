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

namespace SharpOS.Kernel.FileSystem
{
	public class PartitionDevice : Device, IPartitionDevice
	{
		private IDiskDevice diskDevice;
		private uint start;
		private uint blockCount;
		private bool readOnly;

		public PartitionDevice (GenericPartition partition, IDiskDevice diskDevice, bool readOnly)
		{
			this.diskDevice = diskDevice;
			this.start = partition.StartLBA;
			this.blockCount = partition.TotalBlocks;
			this.readOnly = readOnly;

            base.parent = diskDevice as Device;
            base.name = base.parent.Name + "/Partition" + (partition.Index + 1).ToString();	// need to give it a unique name
			base.deviceStatus = DeviceStatus.Online;

			DeviceManager.Add (this);
		}

		public byte[] ReadBlock (uint block, uint count)
		{
			return diskDevice.ReadBlock (block + start, count);
		}

		public bool ReadBlock (uint block, uint count, byte[] data)
		{
			return diskDevice.ReadBlock (block + start, count, data);
		}

		public bool WriteBlock (uint block, uint count, byte[] data)
		{
			return diskDevice.WriteBlock (block + start, count, data);
		}

		public uint BlockCount
		{
			get
			{
				return blockCount;
			}
		}

		public uint BlockSize
		{
			get
			{
				return diskDevice.BlockSize;
			}
		}

		public bool CanWrite
		{
			get
			{
				return !readOnly;
			}
		}

	}

}
