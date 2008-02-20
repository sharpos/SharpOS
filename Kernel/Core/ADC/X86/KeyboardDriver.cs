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

namespace SharpOS.Kernel.ADC.X86
{
	public class KeyboardDriver : GenericDriver {

		private IOPortStream ControllerCommands;
		private IOPortStream DataPort;

		#region Initialize
		public override bool Initialize(IDevice device, IHardwareResourceManager manager)
		{
			ControllerCommands	= manager.Request8bitIOPort((ushort)IO.Port.KB_controller_commands);
			DataPort			= manager.Request8bitIOPort((ushort)IO.Port.KB_data_port);
			
			return (isInitialized = false);
		}
		#endregion
	}
}
