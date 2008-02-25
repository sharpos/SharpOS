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

namespace SharpOS.Kernel.ADC.X86
{
	public class RTCDriver : GenericDriver {

		#region Initialize
		public override bool  Initialize(IDevice device, IHardwareResourceManager manager)
		{
			return (isInitialized = false);
		}
		#endregion
	}
}
