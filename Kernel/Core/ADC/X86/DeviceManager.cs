using System;
using System.Collections.Generic;
using System.Text;

namespace SharpOS.Kernel.ADC.X86
{
	public class DeviceManager : IDeviceManager	{

		#region AddRootDevices
		internal void AddRootDevices()
		{
			GenericDevice	lpcDevice = new GenericDevice();
			lpcDevice.Setup(
				"LPC", 
				"Intel", 
				"Low Pin Count Bus", 
				"",
				new LPCDriver());

			GenericDevice	pciDevice = new GenericDevice();
			pciDevice.Setup(
				"PCI", 
				"PCI Special Interest Group", 
				"Peripheral Component Interconnect", 
				new PCIDriver());


			rootDevices		= new IDevice[2];
			rootDevices[0]	= lpcDevice;
			rootDevices[1]	= pciDevice;
		}
		#endregion
		
		#region Setup
		public override void Setup()
		{
			InitializeDriver(rootDevices);
		}

		internal void InitializeDriver(IDevice[] devices)
		{
			// ..initialize root devices
			for (int i = 0; i < devices.Length; i++)
			{
				//try {
								
				if (devices[i] == null || // just in case...
					devices[i].Driver == null)
					continue;

				devices[i].Driver.Initialize(devices[i], Architecture.ResourceManager);

				//catch { .. disable device & reclaim driver resources (if any) .. }
			}

			// ..initialize child devices
			for (int i = 0; i < devices.Length; i++)
			{ 
				//try {
								
				if (devices[i] == null || // just in case...
					devices[i].Driver == null ||
					!devices[i].Driver.IsInitialized ||
					!devices[i].Driver.HasSubDevices)
					continue;

				IDevice[] childDevices;
				if (devices[i].Driver.GetSubDevices(out childDevices))
					InitializeDriver(childDevices);
				
				//catch { .. disable device & reclaim driver resources (if any) .. }
			}
		}
		#endregion
		
		#region Devices
		private IDevice[]			rootDevices;
		public override IDevice[]	Devices { get { return rootDevices; } }
		#endregion
	}
}
