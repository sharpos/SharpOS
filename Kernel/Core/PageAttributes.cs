// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
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