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
using SharpOS.Kernel.DriverSystem;

namespace SharpOS.Kernel.ADC.X86 {

	/// <summary>
	/// This class is used for drivers to acquire (safe) access to and reserve 
	/// access to system wide hardware resources.
	/// </summary>
	/// <todo>
	/// Eventually we want to pass an attribute annotated interface to the
	/// manager and it'll generate a class which implements that interface and
	/// returns it.. untill we can implement something like that we'll 
	/// need to work around it
	/// </todo>
	/// <todo>
	/// We need to have some sort of context so that when a driver is cleaned up, 
	/// we can find back the resources it requested.
	/// </todo>	
	/// <TODO> add support for dma </TODO>
	/// <TODO> add support for interrupts </TODO>
	internal class HardwareResourceManager : IHardwareResourceManager {
		
		public void Setup()
		{
		}

		public MemoryBlock RequestMemoryBuffer(uint address, uint length)
		{
			return new MemoryBlock(address, length);
		}

		public IOPortStream Request8bitIOPort(ushort port)
		{
			return new IOPortStream8bit((IO.Port)port);
		}
	}
}