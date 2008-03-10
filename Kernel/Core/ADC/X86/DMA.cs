// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//  Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SharpOS.Kernel;
using SharpOS.Kernel.Memory;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS.Kernel.ADC.X86
{

	// DMA Mode = MOD1:MOD0:IDEC:AUTO:TRA1:TRA0:SEL1:SEL0

	public enum DMAMode : byte
	{
		ReadFromMemory = 0x08,	// TRN=10
		WriteToMemory = 0x04    // TRN=01
	}

	public enum DMATransferType : byte
	{
		OnDemand = 0x00,	// MOD=00
		Single = 0x40,		// MOD=01
		Block = 0x80,		// MOD=10
		CascadeMode = 0xC0	// MOD=11
	}

	public enum DMAChannel : byte
	{
		//Channel0 = 0, // reserved
		Channel1 = 1,
		Channel2 = 2,
		Channel3 = 3
	}

	public enum DMAAuto : byte
	{
		Auto = 0x10,
		NoAuto = 0x00,
	}

	public unsafe class DMA
	{
		protected static byte* dmaReserve;

		public static unsafe void Setup(byte* free)
		{
			// DMA 1) memory must be under 16M and 2) access must start on 64k boundary 

			// force align onto 64k boundary 
			// the trick here is add 64k to last allocation and then mask bit against (64k - 1) value
			dmaReserve = (byte*)(((uint)free + (64 * 1024)) & ((64 * 1024) - 1));

			if ((((uint)dmaReserve + (64 * 1024 * 3)) >> 24) != 0)
				Diagnostics.Panic("Can not allocated DMA memory under 16M");

			// Reserve 64K for each channel
			// DMA channel 0 is reserved - and not used on the x86 platform
			PageAllocator.ReservePageRange(dmaReserve, 64 * 1024 / ADC.Pager.AtomicPageSize, "dma #1");
			PageAllocator.ReservePageRange(dmaReserve + (64 * 1024), 64 * 1024 / ADC.Pager.AtomicPageSize, "dma #2");
			PageAllocator.ReservePageRange(dmaReserve + (2 * 64 * 1024), 64 * 1024 / ADC.Pager.AtomicPageSize, "dma #3");
		}

		private static void* GetDMATranserAddress(DMAChannel channel)
		{
			switch (channel) {
				case DMAChannel.Channel1:
					return dmaReserve;
				case DMAChannel.Channel2:
					return dmaReserve + (1024 * 64);
				case DMAChannel.Channel3:
					return dmaReserve + (1024 * 64 * 2);
				default:					
					return null;
			}
		}

		public static unsafe bool SetupChannel(DMAChannel channel, uint count, DMAMode mode, DMATransferType type, DMAAuto auto)
		{
			IO.Port dma_page;
			IO.Port dma_address;
			IO.Port dma_count;

			if (count > (1024 * 64))
				return false;

			switch (channel) {
				case DMAChannel.Channel1:
					dma_page = IO.Port.DMA_Channel1Page;
					dma_address = IO.Port.DMA_Channel1Address;
					dma_count = IO.Port.DMA_Channel1Count;
					break;
				case DMAChannel.Channel2:
					dma_page = IO.Port.DMA_Channel2Page;
					dma_address = IO.Port.DMA_Channel2Address;
					dma_count = IO.Port.DMA_Channel2Count;
					break;
				case DMAChannel.Channel3:
					dma_page = IO.Port.DMA_Channel3Page;
					dma_address = IO.Port.DMA_Channel3Address;
					dma_count = IO.Port.DMA_Channel3Count;
					break;
				default:
					// TODO: throw an exception when we have exception support
					return false;
			}

			uint address = (uint) GetDMATranserAddress(channel);

			Barrier.Enter();
			// Disable DMA Controller
			IO.WriteByte(IO.Port.DMA_ChannelMaskRegister, (byte)((byte)channel | 4));

			// Clear any current transfers
			IO.WriteByte(IO.Port.DMA_ClearBytePointerFlipFlop, (byte)0xFF);	// reset flip-flop

			// Set Address	
			IO.WriteByte(dma_address, (byte)(address & 0xFF)); // low byte
			IO.WriteByte(dma_address, (byte)((address >> 8) & 0xFF)); // high byte
			IO.WriteByte(dma_page, (byte)((address >> 16) & 0xFF)); // page

			// Clear any current transfers
			IO.WriteByte(IO.Port.DMA_ClearBytePointerFlipFlop, (byte)0xFF);	// reset flip-flop

			// Set Count
			IO.WriteByte(dma_count, (byte)((count - 1) & 0xFF)); // low
			IO.WriteByte(dma_count, (byte)(((count - 1) >> 8) & 0xFF)); // high

			// Set DMA_Channel to write
			IO.WriteByte(IO.Port.DMA_ModeRegister, (byte)((((byte)auto | (byte)mode | (byte)type) | (byte)channel)));

			// Enable DMA Controller
			IO.WriteByte(IO.Port.DMA_ChannelMaskRegister, (byte)(channel));

			Barrier.Exit();

			return true;
		}

		public static bool TransferOut(DMAChannel channel, void* destination, uint count)
		{
			if (count > (1024 * 64))
				return false;

			uint address = (uint)GetDMATranserAddress(channel);

			if (address == 0x00)
				return false;

			MemoryUtil.MemCopy(address, (uint)destination, count);

			return true;
		}

		public static bool TransferIn(DMAChannel channel, void* source, uint count)
		{
			if (count > (1024 * 64))
				return false;

			uint address = (uint)GetDMATranserAddress(channel);

			if (address == 0x00)
				return false;

			MemoryUtil.MemCopy((uint)source, address, count);

			return true;
		}


	}
}
