// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;

namespace SharpOS.Kernel.DriverSystem
{
	public abstract class GenericDriver : IDriver {
		#region IsInitialized
		protected bool isInitialized = false;
		public override bool IsInitialized
		{
			get { return isInitialized; }
		}
		#endregion

		//public abstract bool Initialize(IDevice device, IHardwareResourceManager manager);

		#region HasSubDevices
		public override bool HasSubDevices
		{
			get { return false; }
		}
		#endregion

		#region GetSubDevices
		public override bool GetSubDevices(out IDevice[] subDevices)
		{
			subDevices = null;
			return false;
		}
		#endregion
	}
}
