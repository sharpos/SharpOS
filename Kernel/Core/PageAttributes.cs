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
using SharpOS.ADC;

namespace SharpOS.Memory {
	[System.Flags]
	public enum PageAttributes: uint {
		None = 0,
		
		ReadWrite	= 1,
		User		= 1<<1,
		Present		= 1<<2,
	}
}