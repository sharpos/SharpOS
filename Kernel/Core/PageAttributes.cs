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
using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.Memory {
	[System.Flags]
	public enum PageAttributes : uint {
		None = 0,

		ReadWrite = 1,
		User = 1 << 1,
		Present = 1 << 2,
	}
}