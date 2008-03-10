// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;

namespace SharpOS.Kernel.DriverSystem
{
	public class FloppyDiskDriver : GenericDriver
	{
		private IOPortStream ControllerCommands;
		private IOPortStream DataPort;
		private IOPortStream ConfigPort;
		private IOPortStream StatusPort;

		#region Initialize
		public override bool Initialize(IDriverContext context)
		{
			context.Initialize(DriverFlags.IOStream8Bit);

			ushort iobase = 0x03F2; // Secondary is at 0x0372

			ControllerCommands = context.CreateIOPortStream((ushort)iobase);//IO.Port.FDC_DORPort
			StatusPort = context.CreateIOPortStream((ushort)(iobase + 2));//IO.Port.FDC_StatusPort
			DataPort = context.CreateIOPortStream((ushort)(iobase + 3));	//IO.Port.FDC_DataPort
			ConfigPort = context.CreateIOPortStream((ushort)(iobase + 5));//IO.Port.FDC_Config

			return (isInitialized = false);
		}
		#endregion
	}
}
