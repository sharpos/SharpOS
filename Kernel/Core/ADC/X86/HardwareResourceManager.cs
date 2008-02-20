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
	/// How to abstract away all the different types of hardware resources yet
	/// keep as much as possible platform independent.. 
	/// maybe have an interface for each different type of hardware resource?
	/// </todo>
	internal unsafe class HardwareResourceManager : IHardwareResourceManager {
		
		public void Setup()
		{
		}

		public static MemoryBlock RequestMemoryBuffer(uint address, uint length)
		{
			return new MemoryBlock(address, length);
		}

		public static IOPortStream Request8bitIOStream(IO.Port port)
		{
			return new IOPortStream8bit(port);
		}
	}
}