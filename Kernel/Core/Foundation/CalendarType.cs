//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

//#define VERBOSE_BinaryTool

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SharpOS.Kernel;
using SharpOS.Kernel.ADC;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using SharpOS.Kernel.Foundation;
using AOTAttr = SharpOS.AOT.Attributes;

//#define VERBOSE_BinaryTool

namespace SharpOS.Kernel.Foundation {
	public enum CalendarType : short {
		Unknown = 0,
		Gregorian,
		Islamic,
		Julian,
		Astronomical,
		ISO,
	}
}

