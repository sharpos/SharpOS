// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

namespace SharpOS.Kernel {
	public enum KernelStage : uint {
		Init = 0,
		RuntimeInit,
		UserInit,
		Active,
		SingleUser,
		Stopping,
		Stop,
		Halt,
		Diagnostics,

		Unknown = 0xFFFFFFFF
	}
}
