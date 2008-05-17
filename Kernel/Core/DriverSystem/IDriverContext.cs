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

namespace SharpOS.Kernel.DriverSystem
{

	public interface IDriverContext
	{
		DriverFlags Flags { get; }

		void Release();
		bool IsReleased { get; }

		// TODO: this should eventually be done trough attributes
		void Initialize();

		MemoryBlock CreateMemoryBuffer(uint address, uint length);
		
		IOPortStream CreateIOPortStream(uint port);
		IOPortStream CreateIOPortStream (uint port, uint offset);
		//IOPortReadStream CreateIOPortReadStream (uint port);
		//IOPortWriteStream CreateIOPortWriteStream (uint port);
	
		DMAChannel CreateDMAChannel(byte channel);
		IRQHandler CreateIRQHandler (byte irq);
	}
}

