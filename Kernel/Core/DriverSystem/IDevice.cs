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

namespace SharpOS.Kernel.DriverSystem
{
	// TODO: need mechanism to remember device/driver settings
	// TODO: need mechanism to reclaim/acquire resources when device is enabled/disabled
	public interface IDevice {

		/// <summary>Gets or sets the driver attached to the current device</summary>
		IDriver			Driver			{ get; set; }
		
		/// <summary>Enables or disables a device</summary>
		bool			Enabled			{ get; set; }

		// TODO: this should be put in attributes eventually:
		String			Signature		{ get; }
		String			Vendor			{ get; }
		String			Name			{ get; }
		String			Category		{ get; }
	}
}
