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

namespace SharpOS.Kernel.DriverSystem
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

	public unsafe interface DMAChannel
	{
		void SetupChannel (DMAMode mode, DMATransferType type, bool auto, uint count);
		bool TransferOut (MemoryBlock memoryblock, uint count);
		bool TransferIn (MemoryBlock memoryblock, uint count);
	}
}
