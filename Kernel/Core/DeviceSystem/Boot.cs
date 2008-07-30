using System;
using SharpOS.Kernel.DeviceSystem;

namespace SharpOS.Kernel.DeviceSystem
{
	public static class Boot
	{
		public static void Start ()
		{
			DeviceManager.Initalize ();
			SharpOS.Kernel.ADC.TextMode.WriteLine ("Starting Controllers");
			DeviceManager.Add (new PCI.PCIController ());

			DeviceManager.Add (new DiskController.IDEDiskDriver (DiskController.IDEDiskDriver.PrimaryIOBase));
			//DeviceManager.Add (new DiskController.IDEDiskDriver (DiskController.IDEDiskDriver.SecondaryIOBase));

			DeviceManager.Add (new DiskController.FloppyDiskDriver (DiskController.FloppyDiskDriver.PrimaryIOBase));

			DeviceManager.Add (new SerialDevice(SerialDevice.Serial1Port,SerialDevice.Serial1IRQ));
			DeviceManager.Add (new SerialDevice(SerialDevice.Serial2Port,SerialDevice.Serial2IRQ));
		}
	}
}
