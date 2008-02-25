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
using System.Runtime.InteropServices;
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS.Kernel.DriverSystem {

	/// <summary>
	/// Low Pin Count bus
	///  The LPC bus was introduced by Intel in 1998 as a substitute for 
	///  the Industry Standard Architecture (ISA) bus.
	/// </summary>
	/// <TODO>LPCBusDriver should use implementations of LPCDevice/LPCDriver class internally</TODO>
	public class LPCBusDriver : GenericDriver	{

		// LPC devices
		//    FloppyDiskControllers
		//    FloppyDiskDrives
		//    Parallel ports
		//    Serial ports

		#region Initialize
		public override bool Initialize(IDevice device, IHardwareResourceManager manager)
		{
			return (isInitialized = false);
		}
		#endregion
		
		#region SubDevices
		public override bool HasSubDevices
		{
			get { return true; }
		}

		public override bool GetSubDevices(out IDevice[] subDevices)
		{
			subDevices = new IDevice[] {
			};
			return false;
		}
		#endregion
	}
}
