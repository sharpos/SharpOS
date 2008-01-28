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
			Serial.Setup ();	// Setup serial I/O			
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


		#region Processors
		// must do it here because memory management doesn't work yet in Setup... :(
		static private IProcessor[] processors = null;
		private static void InitializeProcessor()
		{
			processors = new IProcessor[1];
			for (int i = 0; i < processors.Length; i++)
			{
				Processor processor = new Processor();
				processor.Setup();
				processors[i] = processor;
			}
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

				
		#region Devices
		private static DeviceManager	deviceManager = null;
		public static IDeviceManager	DeviceManager 
		{
			get 
			{
				if (deviceManager == null)
				{
					deviceManager = new DeviceManager();
					deviceManager.AddRootDevices();
				}
				return deviceManager; 
			}
		}
		#endregion
		

		// WARNING: ..only visible internally in current assembly for security reasons!!
		#region ResourceManager
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
	}
}
