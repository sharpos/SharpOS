// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

namespace SharpOS
{
	public enum KernelStage: uint {
		Init = 0,
		RuntimeInit,
		UserInit,
		Active,
		SingleUser,
		Stopping,
		Stop,
		Halt,
		
		Unknown = 0xFFFFFFFF
	}
}