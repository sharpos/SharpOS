using System;
using System.Collections.Generic;
using System.Text;

namespace SharpOS.Kernel.DeviceSystem.PCI
{
	public interface IPCIController
	{
		uint ReadConfig (uint bus, uint slot, uint function, uint register);
		void WriteConfig (uint bus, uint slot, uint function, uint register, uint value);
	}
}
