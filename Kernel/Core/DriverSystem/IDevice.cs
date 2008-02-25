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
	// TODO: need mechanism to reclaim/acquire resources when device is enabled/disabled
	// TODO: turn this into an interface eventually, untill then pretend it's an interface..
	public abstract class IDevice {

		/// <summary>Gets or sets the driver attached to the current device</summary>
		public abstract IDriver		Driver			{ get; set; }
		
		/// <summary>Enables or disables a device</summary>
		public abstract bool		Enabled			{ get; set; }

		// TODO: this should be put in attributes eventually:
		public abstract String		Signature		{ get; }
		public abstract String		Vendor			{ get; }
		public abstract String		Name			{ get; }
		public abstract String		Category		{ get; }
	}
}
