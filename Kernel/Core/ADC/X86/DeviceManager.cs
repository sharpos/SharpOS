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

namespace SharpOS.Kernel.ADC.X86 {

	// TODO: would be nice to be able to use List<IDevice> internally ...
	// TODO: eventually need events to notify subscribers when 
	//		devices have been changed/added/removed
	public class DeviceManager : IDeviceManager	{
		
		#region (Root) Devices
		private IDevice[]	rootDevices = new IDevice[]
		{
			new GenericDevice(
				"Keyboard",
				"Unknown",
				"PS/2 Keyboard",
				new KeyboardDriver()
			),
			
			/*
			new GenericDevice(
				"RTC",
				"Unknown",
				"Real Time Clock",
				new RTCDriver()
			),
			new GenericDevice(
				"UART",
				"Unknown",
				"UART Serial Port",
				new UARTDriver()
			),
			*/
			new GenericDevice(
				"LPC", 
				"Intel", 
				"Low Pin Count Bus", 
				new LPCBusDriver()),

			new GenericDevice(
				"PCI", 
				"PCI Special Interest Group", 
				"Peripheral Component Interconnect", 
				new PCIBusDriver()),

			/*
			new GenericDevice(
				"AGP", 
				"Intel", 
				"Accelerated Graphics Port", 
				new AGPBusDriver()),
			
			new GenericDevice(
				"USB", 
				"USB Implementers Forum", 
				"Universal Serial Bus", 
				new USBBusDriver()),
			
			new GenericDevice(
				"SCSI", 
				"Unknown", 
				"Small Computer System Interface", 
				new SCSIBusDriver()),
			
			new GenericDevice(
				"IEEE 1394", 
				"Institute of Electrical and Electronics Engineers", 
				"FireWire", 
				new FireWireBusDriver()),
			
			// formerly known as ATA
			new GenericDevice(
				"PATA", 
				"Western Digital", 
				"Parallel Advanced Technology Attachment", 
				new PATABusDriver()),
			
			new GenericDevice(
				"SATA", 
				"Serial ATA International Organization", 
				"Serial Advanced Technology Attachment", 
				new SATABusDriver()),
			*/
		};
		public override IDevice[]	Devices { get { return rootDevices; } }
		#endregion

		#region Setup
		public override void Setup()
		{
			InitializeDevices(rootDevices);
		}

		internal void InitializeDevices(IDevice[] devices)
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
					InitializeDevices(childDevices);
				
				//catch { .. disable device & reclaim driver resources (if any) .. }
			}
		}
		#endregion
	}
}
