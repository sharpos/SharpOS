// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
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

namespace SharpOS.Kernel.ADC.X86 {

	public enum DMAMode : byte
	{
		Read	= 0x48,
		Write	= 0x44
	}

	public enum DMAChannel : byte
	{
		Channel0	= 0,
		Channel1	= 1,
		Channel2	= 2,
		Channel3	= 3
	}

	public class DMA
	{
		public static bool SetupChannel (DMAChannel channel, byte page, ushort address, ushort count, DMAMode mode)
		{
			IO.Port dma_page;
			IO.Port dma_address;
			IO.Port dma_count;

			switch (channel)
			{
				case DMAChannel.Channel0:
					dma_page	= IO.Port.DMA_Channel0Page;
					dma_address = IO.Port.DMA_Channel0Address;
					dma_count	= IO.Port.DMA_Channel0Count;
					break;
				case DMAChannel.Channel1:
					dma_page	= IO.Port.DMA_Channel1Page;
					dma_address = IO.Port.DMA_Channel1Address;
					dma_count	= IO.Port.DMA_Channel1Count;
					break;
				case DMAChannel.Channel2:
					dma_page	= IO.Port.DMA_Channel2Page;
					dma_address = IO.Port.DMA_Channel2Address;
					dma_count	= IO.Port.DMA_Channel2Count;
					break;
				case DMAChannel.Channel3:
					dma_page	= IO.Port.DMA_Channel3Page;
					dma_address = IO.Port.DMA_Channel3Address;
					dma_count	= IO.Port.DMA_Channel3Count;
					break;
				default:
					// TODO: throw an exception when we have exception support
					return false;
			}

			Barrier.Enter();
			// Disable DMA Controller
			IO.WriteByte (IO.Port.DMA_ChannelMaskRegister, (byte)((byte)channel | 4));

			// Set DMA_Channel to write
			IO.WriteByte (IO.Port.DMA_ModeRegister, (byte)((byte)mode | (byte)channel));
			
			// Set Address	
			IO.WriteByte (dma_page, (byte)page);
			IO.WriteByte2 (dma_address, (ushort)address);

			// Set Count
			IO.WriteByte2 (dma_count, (ushort)(count - 1));
						
			// Enable DMA Controller
			IO.WriteByte (IO.Port.DMA_ChannelMaskRegister, (byte)(channel));
			Barrier.Exit();

			return true;
		}

	}
}
