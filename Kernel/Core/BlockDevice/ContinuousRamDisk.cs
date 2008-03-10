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
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.Foundation;

namespace SharpOS.Kernel.BlockDevice
{
	public unsafe class ContinuousRamDisk : IBlockDevice
	{
		protected byte* mem = null;
		protected uint sectors;
		protected bool free;
		protected uint hardwaresectorsize = GenericBlockDevice.KernelSectorSize;

		public ContinuousRamDisk(uint sectors)
		{
			this.sectors = sectors;
			mem = (byte*)MemoryManager.Allocate(hardwaresectorsize * sectors);
			free = true;
		}

		public ContinuousRamDisk(byte* memory, uint size)
		{
			mem = memory;
			sectors = size / hardwaresectorsize;
			free = false;
		}

		public int Open()
		{
			return 0;
		}

		public int Release()
		{
			if ((free) && (mem != null))
				MemoryManager.Free(mem);

			mem = null;

			return 0;
		}

		public uint GetBlockSize()
		{
			return hardwaresectorsize;
		}

		public uint GetTotalBlocks()
		{
			return hardwaresectorsize * sectors;
		}

		public bool CanWrite()
		{
			return true;
		}

		public unsafe int Request(IORequestType request, uint sector, uint nsectors, ByteBuffer buff)
		{
			if ((buff.data == null) || (sector + nsectors > sectors))
				return -1;

			if (request == IORequestType.Read)
				MemoryUtil.MemCopy((uint)mem + (hardwaresectorsize * sector), (uint)buff.data, hardwaresectorsize * nsectors);
			else if (request == IORequestType.Write)
				MemoryUtil.MemCopy((uint)buff.data, (uint)mem + (hardwaresectorsize * sector), hardwaresectorsize * nsectors);

			return 0;
		}
	}
}
