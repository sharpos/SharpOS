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

namespace SharpOS.Kernel.ADC {

	public enum EventRegisterStatus {
		Success,
		NotSupported,
		CapacityExceeded,
		AlreadySubscribed,
		NotSubscribed,
	}
}
