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

namespace SharpOS.Kernel.DriverSystem {

	[Flags]
	public enum DriverFlags : uint
	{
		IOStream8Bit	= 0,
		IOStream16Bit	= 1,
		IOStream32Bit	= 2,
		IOStream64Bit	= 3,	// ... is this even possible?
		IOStreamMask	= 3

	}
}
