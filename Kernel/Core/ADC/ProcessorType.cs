//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.ADC {
	public enum ProcessorType : byte {
		Unknown = 0,
		IA32,
		IA64,
		PowerPC,
		POWER,
		SPARC,
		ARM,
		RISC,
		CBEA,
	}
}
