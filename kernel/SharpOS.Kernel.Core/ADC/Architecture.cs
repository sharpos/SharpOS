//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using SharpOS.AOT;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.ADC {
	public enum Architecture {
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
