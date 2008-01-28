using System;
using System.Collections.Generic;
using System.Text;

namespace SharpOS.Kernel.ADC
{
	// TODO: turn this into an interface eventually, untill then pretend it's an interface..
	public abstract class IDeviceManager {		
		public abstract IDevice[]	Devices { get; }

		public abstract void Setup();
	}
}
