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
using System.Collections.Generic;
using System.Text;

namespace SharpOS.Kernel.ADC
{
	// TODO: need mechanism to remember device/driver settings
	// TODO: need mechanism to reclaim resources when driver is unloaded / crashes
	// TODO: need mechanism to reclaim/acquire resources when device is enabled/disabled
	// TODO: need mechanism to tell the system if devices have been removed or added
	// TODO: turn this into an interface eventually, untill then pretend it's an interface..
	public abstract class IDriver {

		public abstract bool		Initialize		(IDevice device, IHardwareResourceManager manager);
		public abstract bool		IsInitialized	{ get; }
				
		public abstract bool		HasSubDevices	{ get; }
		
		/// <summary>
		/// Returns an array with all devices attached to current device.
		/// </summary>
		/// <param name="subDevices">All devices attached to the current device</param>
		public abstract bool		GetSubDevices	(out IDevice[] subDevices);
	}
}
