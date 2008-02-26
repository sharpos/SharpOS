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

namespace SharpOS.Kernel.DriverSystem {

	///<summary>
	/// Finds a driver for a given signature, currently unimplemented, 
	/// eventually will look for a driver in kernel resources or on disk.
	/// </summary>
	public static class DriverManager {
		public static IDriver[] Find(IDevice device)
		{
			return new IDriver[] { };
		}
	}
}
