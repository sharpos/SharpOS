// 
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

namespace SharpOS
{
	public enum KernelError: uint {
		Unknown = 0,
		
		Success,
		MultibootError,

		/// <summary>
		/// Scheduler Queue is empty and this was not expected
		/// </summary>
		SchedulerQueueEmpty
	}
}