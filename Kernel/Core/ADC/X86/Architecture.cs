//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.Kernel;
using SharpOS.AOT;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using SharpOS.Kernel.DriverSystem;
using SharpOS.Kernel.DriverSystem.Drivers.Block;
//using SharpOS.Kernel.DriverSystem.Character;
using ADC = SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.ADC.X86 {

	// TODO: ..rename to enviroment?
	// TODO: should we initialize memory management here?
	// TODO: PIT/serial are devices, should be initialized trough the devicemanager
	public unsafe class Architecture {

		#region Setup
		public static void Setup ()
		{
			GDT.Setup ();		// Global Descriptor Table
			PIC.Setup ();		// Programmable Interrupt Controller
			IDT.Setup ();		// Interrupt Descriptor table
			PIT.Setup ();		// Periodic Interrupt Timer
			// Disabled because it needs MemoryManager [cedrou]
			//Serial.Setup ();	// Setup serial I/O			
		}
		#endregion

		// TODO: How usefull is this?
		#region CheckCompatibility
		/**
			<summary>
				Checks for compatibility with the current system, using 
				the most well-supported method possible. 
			</summary>
		*/
		public static bool CheckCompatibility()
		{
			return true; // if we're running, we're at least 386.
		}
		#endregion

		// TODO: should be put in attributes / seperate class?
		#region Implementation Information
		/**
			<summary>
				Gets the ADC platform identifier.
			</summary>
		*/
		public static string GetPlatform()
		{
			return "X86";
		}

		public static string GetAuthor ()
		{
			return "The SharpOS Team";
		}

		public static string GetLayerName ()
		{
			return "SharpOS.ADC.X86";
		}
		#endregion

		#region Processors (public)
		// must do it here because memory management doesn't work yet in Setup... :(
		static private IProcessor[] processors = null;
		private static void InitializeProcessor()
		{
			processors = new IProcessor[]
			{
				new Processor(0)
			};
		}

		public static int GetProcessorCount ()
		{
			if (processors == null)
				InitializeProcessor();

			return processors.Length;
		}

		public static IProcessor[] GetProcessors ()
		{
			if (processors == null)
				InitializeProcessor();

			return processors;
		}
		#endregion
				
		#region DeviceManager (public)
		private static DeviceManager	deviceManager = null;
		public static IDeviceManager	DeviceManager 
		{
			get 
			{
				if (deviceManager == null)
					deviceManager = new DeviceManager(ResourceManager);
				return deviceManager; 
			}
		}
		#endregion

		// WARNING: ..only visible _internally_ in current assembly.
		//			(for security reasons)
		#region ResourceManager (internal)
		private static HardwareResourceManager	resourceManager = null;
		internal static HardwareResourceManager ResourceManager
		{
			get
			{
				if (resourceManager == null)
				{
					resourceManager = new HardwareResourceManager();
					resourceManager.Setup();
				}
				return resourceManager;
			}
		}
		#endregion


		// .. temporarily put here untill we have a beter mechanism to determine 
		// which root devices are available (=platform specific)
		#region RootDevices
		private static IDevice[] rootDevices = null;

		public static IDevice[] RootDevices
		{
			get
			{
				if (rootDevices == null)
				{
					// oh kill me now..
					rootDevices = new IDevice[]
					{
						//new GenericDevice(
						//    "Keyboard",
						//    "Unknown",
						//    "PS/2 Keyboard",
						//    new KeyboardDriver()
						//),
						
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

						new GenericDevice(
						    "Kernel Disk",
						    "Unknown",
						    "Kernel Disk",
						    new KernelDiskDriver()
						),

						new GenericDevice(
						    "Ramdisk",
						    "Unknown",
						    "Ramdisk Controller",
						    new RamDiskDriver()
						),

						new GenericDevice(
							"Primary FDC",
							"Unknown",
							"Primary Floppy Disk Controller",
							new FloppyDiskDriver()
						),
						
						new GenericDevice(
							"Primary IDE",
							"Unknown",
							"Primary IDE Controller",
							new IDEDiskDriver()
						),

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
				}
				return rootDevices;
			}
		}
		#endregion
	}
}
