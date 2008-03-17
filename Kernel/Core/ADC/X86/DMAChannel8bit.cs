// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//  Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.Kernel.DriverSystem;

namespace SharpOS.Kernel.ADC.X86
{

	public class DMAChannel8bit : DMAChannel
	{

		#region Constructor
		internal DMAChannel8bit (byte channel)
		{
			this.channel = channel;
		}
		#endregion

		#region Channel
		protected byte channel;
		#endregion

		#region Methods
		public void SetupChannel (DMAMode mode, DMATransferType type, bool auto, uint count)
		{
			DMA.SetupChannel(channel, count, mode, type, auto);
		}

		public unsafe bool TransferOut (MemoryBlock source, uint count)
		{
			return DMA.TransferOut(channel, source.address, count);
		}

		public unsafe bool TransferIn (MemoryBlock destination, uint count)
		{
			return DMA.TransferIn(channel, destination.address, count);
		}
		#endregion
	}
}
