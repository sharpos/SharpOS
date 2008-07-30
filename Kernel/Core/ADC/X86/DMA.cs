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
using SharpOS.Kernel;
using SharpOS.Kernel.Memory;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using SharpOS.Kernel.HAL;

namespace SharpOS.Kernel.ADC.X86
{

	// DMA Mode = MOD1:MOD0:IDEC:AUTO:TRA1:TRA0:SEL1:SEL0

	internal struct DMAModeValue
	{
		internal const byte ReadFromMemory = 0x08;	// TRN=10
		internal const byte WriteToMemory = 0x04;   // TRN=01
	}

	internal struct DMATransferTypeValue
	{
		internal const byte OnDemand = 0x00;	// MOD=00
		internal const byte Single = 0x40;		// MOD=01
		internal const byte Block = 0x80;		// MOD=10
		internal const byte CascadeMode = 0xC0;	// MOD=11
	}

	internal struct DMAAutoValue
	{
		internal const byte Auto = 0x10;
		internal const byte NoAuto = 0x00;
	}

	public unsafe class DMA
	{
		protected static byte* dmaReserve;

		public static unsafe void Setup (byte* free)
		{
			// DMA 1) memory must be under 16M and 2) access must start on 64k boundary 

			// force align onto 64k boundary 
			// the trick here is add 64k to last allocation and then mask bit against (64k - 1) value
			dmaReserve = (byte*)(((uint)free + (64 * 1024)) & ((64 * 1024) - 1));

			if ((((uint)dmaReserve + (64 * 1024 * 3)) >> 24) != 0)
				Diagnostics.Panic ("Can not allocated DMA memory under 16M");

			// Reserve 64K for each channel
			// DMA channel 0 is reserved - and not used on the x86 platform
			PageAllocator.ReservePageRange (dmaReserve, 64 * 1024 / ADC.Pager.AtomicPageSize, "dma #1");
			PageAllocator.ReservePageRange (dmaReserve + (64 * 1024), 64 * 1024 / ADC.Pager.AtomicPageSize, "dma #2");
			PageAllocator.ReservePageRange (dmaReserve + (2 * 64 * 1024), 64 * 1024 / ADC.Pager.AtomicPageSize, "dma #3");
		}

		private static byte ToValue (DMATransferType transfertype)
		{
			switch (transfertype) {
				case DMATransferType.OnDemand: return DMATransferTypeValue.OnDemand;
				case DMATransferType.Block: return DMATransferTypeValue.Block;
				case DMATransferType.Single: return DMATransferTypeValue.Single;
				case DMATransferType.CascadeMode: return DMATransferTypeValue.CascadeMode;
				default: return 0;
			}
		}

		private static void* GetDMATranserAddress (byte channel)
		{
			return dmaReserve + (1024 * 64 * channel);
		}

		public static unsafe bool SetupChannel (byte channel, uint count, DMAMode mode, DMATransferType type, bool auto)
		{
			IO.Port dma_page;
			IO.Port dma_address;
			IO.Port dma_count;

			if (count > (1024 * 64))
				return false;

			switch (channel) {
				case 1:
					dma_page = IO.Port.DMA_Channel1Page;
					dma_address = IO.Port.DMA_Channel1Address;
					dma_count = IO.Port.DMA_Channel1Count;
					break;
				case 2:
					dma_page = IO.Port.DMA_Channel2Page;
					dma_address = IO.Port.DMA_Channel2Address;
					dma_count = IO.Port.DMA_Channel2Count;
					break;
				case 3:
					dma_page = IO.Port.DMA_Channel3Page;
					dma_address = IO.Port.DMA_Channel3Address;
					dma_count = IO.Port.DMA_Channel3Count;
					break;
				default:
					// TODO: throw an exception when we have exception support
					return false;
			}

			uint address = (uint)GetDMATranserAddress (channel);

			Barrier.Enter ();
			// Disable DMA Controller
			IO.WriteByte (IO.Port.DMA_ChannelMaskRegister, (byte)((byte)channel | 4));

			// Clear any current transfers
			IO.WriteByte (IO.Port.DMA_ClearBytePointerFlipFlop, (byte)0xFF);	// reset flip-flop

			// Set Address	
			IO.WriteByte (dma_address, (byte)(address & 0xFF)); // low byte
			IO.WriteByte (dma_address, (byte)((address >> 8) & 0xFF)); // high byte
			IO.WriteByte (dma_page, (byte)((address >> 16) & 0xFF)); // page

			// Clear any current transfers
			IO.WriteByte (IO.Port.DMA_ClearBytePointerFlipFlop, (byte)0xFF);	// reset flip-flop

			// Set Count
			IO.WriteByte (dma_count, (byte)((count - 1) & 0xFF)); // low
			IO.WriteByte (dma_count, (byte)(((count - 1) >> 8) & 0xFF)); // high

			byte value = (byte)(channel | (auto ? DMAAutoValue.Auto : DMAAutoValue.NoAuto) | ToValue (type));
			value = (byte)(value | (mode == DMAMode.ReadFromMemory ? DMAModeValue.ReadFromMemory : DMAModeValue.WriteToMemory));

			// Set DMA_Channel to write
			IO.WriteByte (IO.Port.DMA_ModeRegister, value);

			// Enable DMA Controller
			IO.WriteByte (IO.Port.DMA_ChannelMaskRegister, (byte)(channel));

			Barrier.Exit ();

			return true;
		}

		public static bool TransferOut (byte channel, void* destination, uint count)
		{
			if (count > (1024 * 64))
				return false;

			uint address = (uint)GetDMATranserAddress (channel);

			if (address == 0x00)
				return false;

			MemoryUtil.MemCopy (address, (uint)destination, count);

			return true;
		}

		public static bool TransferIn (byte channel, void* source, uint count)
		{
			if (count > (1024 * 64))
				return false;

			uint address = (uint)GetDMATranserAddress (channel);

			if (address == 0x00)
				return false;

			MemoryUtil.MemCopy ((uint)source, address, count);

			return true;
		}

		public static bool TransferOut (byte channel, uint count, byte[] destination, uint offset)
		{
			if (count > (1024 * 64))
				return false;

			uint address = (uint)GetDMATranserAddress (channel);

			if (address == 0x00)
				return false;

			for (uint i = 0; i < count; i++)
				destination[offset + i] = *(byte*)(address + i);

			return true;
		}

		public static bool TransferIn (byte channel, uint count, byte[] destination, uint offset)
		{
			if (count > (1024 * 64))
				return false;

			uint address = (uint)GetDMATranserAddress (channel);

			if (address == 0x00)
				return false;

			for (uint i = 0; i < count; i++)
				*(byte*)(address + i) = destination[offset + i];

			return true;
		}
	}
}
