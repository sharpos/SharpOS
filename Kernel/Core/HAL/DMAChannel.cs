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
using SharpOS.Kernel.ADC.X86;

namespace SharpOS.Kernel.HAL
{

	public enum DMAMode : byte
	{
		ReadFromMemory,
		WriteToMemory
	}

	public enum DMATransferType : byte
	{
		OnDemand,
		Single,
		Block,
		CascadeMode
	}

	public unsafe interface IDMAChannel
	{
		void SetupChannel (DMAMode mode, DMATransferType type, bool auto, uint count);
		bool TransferOut (uint count, byte[] data, uint offset);
		bool TransferIn (uint count, byte[] data, uint offset);
	}

	public class DMAChannel : IDMAChannel
	{

		internal DMAChannel (byte channel)
		{
			this.channel = channel;
		}

		protected byte channel;

		#region Methods

		public void SetupChannel (DMAMode mode, DMATransferType type, bool auto, uint count)
		{
			DMA.SetupChannel (channel, count, mode, type, auto);
		}

		public bool TransferOut (uint count, byte[] data, uint offset)
		{
			return DMA.TransferOut (channel, count, data, offset);
		}

		public bool TransferIn (uint count, byte[] data, uint offset)
		{
			return DMA.TransferIn (channel, count, data, offset);
		}

		#endregion
	}
}
