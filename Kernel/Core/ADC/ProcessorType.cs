//
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.ADC {
	public enum ProcessorType {
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
