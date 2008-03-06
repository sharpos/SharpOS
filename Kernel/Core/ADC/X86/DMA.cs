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
		Channel0 = 0, // reserved
		Channel1 = 1,
		Channel2 = 2,
		Channel3 = 3
	}

	public enum DMAAuto : byte
	{
		Auto = 0x10,
		NoAuto = 0x00,
	}
	public class DMA
	{
		public static unsafe bool SetupChannel(DMAChannel channel, void* address, ushort count, DMAMode mode, DMATransferType type, DMAAuto auto)
		{
			uint memaddress = (uint)address;

			if ((memaddress >> 24) != 0)
				return false; // all DMA must be under 16M			

			IO.Port dma_page;
			IO.Port dma_address;
			IO.Port dma_count;

			switch (channel) {
				case DMAChannel.Channel0:
					return false;	// can't use DMA Channel 0
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

			Barrier.Enter();
			// Disable DMA Controller
			IO.WriteByte(IO.Port.DMA_ChannelMaskRegister, (byte)((byte)channel | 4));

			// Clear any current transfers
			IO.WriteByte(IO.Port.DMA_ClearBytePointerFlipFlop, (byte)0xFF);	// reset flip-flop

			// Set Address	
			IO.WriteByte(dma_address, (byte)(memaddress & 0xFF)); // low byte
			IO.WriteByte(dma_address, (byte)((memaddress >> 8) & 0xFF)); // high byte
			IO.WriteByte(dma_page, (byte)((memaddress >> 16) & 0xFF)); // page

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

	}
}
