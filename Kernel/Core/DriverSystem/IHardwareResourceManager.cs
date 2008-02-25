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

namespace SharpOS.Kernel.ADC {

	// NOTE: implemented in X86/HardwareResourceManager
	// TODO: turn this into an interface eventually, untill then pretend it's an interface..
	public abstract class IHardwareResourceManager {
		public abstract MemoryBlock		RequestMemoryBuffer	(uint address, uint length);
		public abstract	IOPortStream	Request8bitIOPort	(ushort port);
	}
}
