// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Cédric Rousseau <cedrou@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.ADC
{
	/// <summary>
	/// This class describe an hardware device on a PCI bus.
	/// </summary>
	public unsafe struct PCIDevice
	{
		#region Private fields

		private uint bus;
		private uint slot;
		private uint function;
		private ushort vendorID;
		private ushort deviceID;
		private byte revisionID;
		private ClassCodes classCode;
		private byte progIF;

		#endregion

		#region Construction

		/// <summary>
		/// Create a new PCIDevice instance at the selected PCI address
		/// </summary>
		/// <param name="bus"></param>
		/// <param name="slot"></param>
		/// <param name="fun"></param>
		/// <returns>A pointer to the newly created structure</returns>
		public static PCIDevice* CREATE (uint bus, uint slot, uint fun)
		{
			PCIDevice* instance = (PCIDevice*)MemoryManager.Allocate ((uint)sizeof (PCIDevice));

			instance->bus = bus;
			instance->slot = slot;
			instance->function = fun;

			uint data = 0;

			data = PCIController.ReadConfig (bus, slot, fun, 0);
			instance->vendorID = (ushort)(data & 0xFFFF);
			instance->deviceID = (ushort)((data >> 16) & 0xFFFF);

      data = PCIController.ReadConfig(bus, slot, fun, 8);
			instance->revisionID = (byte)(data & 0xFF);
			instance->progIF = (byte)((data >> 8) & 0xFF);
			instance->classCode = (ClassCodes)((data >> 16) & 0xFFFF);

			return instance;
		}

		#endregion

		#region Public properties

		public uint Bus { get { return bus; } }
		public uint Slot { get { return slot; } }
		public uint Function { get { return function; } }
		public ushort VendorID { get { return vendorID; } }
		public ushort DeviceID { get { return deviceID; } }
		public byte RevisionID { get { return revisionID; } }
		public ClassCodes ClassCode { get { return classCode; } }
		public byte ProgIF { get { return progIF; } }

		#endregion

		#region Enumerations

		// http://www.acm.uiuc.edu/sigops/roll_your_own/7.c.1.html
		public enum ClassCodes : ushort
		{ 
			//Devices built before class codes (i.e. pre PCI 2.0)
			NotVGA = 0x0000, // All devices other than VGA
			VGADevice = 0x0001, // VGA device

			//Mass storage controller
			SCSIController = 0x0100,
			IDEController = 0x0101, // The Prog I/F byte is defined as follows:
															//bits 	Desc
															//0 	Operating mode (primary)
															//1 	Programmable indicator (primary)
															//2 	Operating mode (secondary)
															//3 	Programmable indicator (secondary)
															//6..4 	Reserved (zero)
															//7 	Master IDE device
			FloppyDiskController = 0x0102,
			IPIController = 0x0103,
			RAIDController = 0x0104,
			MassStorageController = 0x0180,
			
			//Network controller
			EthernetController = 0x0200,
			TokenRing = 0x0201,
			FDDIController = 0x0202,
			ATMController = 0x0203,
			NetworkController = 0x0280,

			//Display controller
			VGAController = 0x0300, // VGA compatible controller. 
			                        // Has mapping for 0xA0000..0xBFFFF and io addresses 0x3B0..0x3BB.
								              // Prog I/F 0x01: 8514 compatible
			XGAController = 0x0301,
			DisplayController = 0x0380, // Prog I/F = 0x80

			//Multimedia device
			VideoDevice = 0x0400,
			AudioDevice = 0x0401,
			MultimediaDevice = 0x0480,

			//Memory Controller
			RAMController = 0x0500,
			FlashMemoryController = 0x0501,
			MemoryController = 0x0580,

			//Bridge Device
			HostPCIBridge = 0x0600,
			PCIISABridge = 0x0601,
			PCIEISABridge = 0x0602,
			PCIMicroChannelBridge = 0x0603,
			PCIPCIBridge = 0x0604,
			PCIPCMCIABridge = 0x0605,
			PCINuBusBridge = 0x0606,
			PCICardBusBridge = 0x0607,
			BridgeDevice = 0x0680,

			//Simple communications controllers
			GenericXTCompatibleSerialController = 0x0700, // 0x00 Generic XT compatable serial controller
																										// 0x01	16450 compatable serial controller
																										// 0x02	16550 compatable serial controller
			ParallelPort = 0x0701,	// 0x00 Parallel port
															// 0x01 Bi-directional parallel port
															// 0x02 ECP 1.X parallel port
			CommunicationController = 0x0780,

			//Base system peripherals
			ProgrammableInterruptController = 0x0800, // 0x00  Generic 8259 programmable interrupt controller (PIC)
																								// 0x01 	ISA PIC
																								// 0x02 	EISA PIC
			DMAController = 0x0801,	// 0x00 	Generic 8237 DMA controller
															// 0x01 	ISA DMA controller
															// 0x02 	EISA DMA controller
			SystemTimer = 0x0802,	// 0x00 	Generic 8254 timer
														// 0x01 	ISA system timer
														// 0x02 	EISA system timer
			RTCController = 0x0803, // 0x00 	Generic RTC controller
															// 0x01 	ISA RTC controller
			SystemPeripheral = 0x0880,

			//Input devices
			KeyboardController = 0x0900,
			PenController = 0x0901,
			MouseController = 0x0902,
			InputController = 0x0980,

			//Docking Stations
			GenericDockingStation = 0x0A00,
			DockingStation = 0x0A80,

			//Processors
			Processor386 = 0x0B00,
			Processor486 = 0x0B01,
			ProcessorPentium = 0x0B02,
			ProcessorAlpha = 0x0B10,
			ProcessorPowerPC = 0x0B20,
			CoProcessor = 0x0B40,

			//Serial bus controllers
			FirewireController = 0x0C00,
			AccessBusController = 0x0C00,
			SSAController = 0x0C00,
			USBController = 0x0C00,

			//Reserved = 0x0D-0xFE 	
			//Misc = 0xFF
		}
		#endregion
	}

	/// <summary>
	/// This presents methods and accessors to manage a PCI bus.
	/// </summary>
	public unsafe class PCIController
  {
		/// <summary>
		/// Setup the PCI controller
		/// </summary>
		/// <remarks>
		/// This function initialize the controller, scan the PCI bus and 
		/// create an array with all the found devices.
		/// </remarks>
    [AOTAttr.ADCStub]
    public static void Setup()
    {
    }

		/// <summary>
		/// Check PCI Bus availability
		/// </summary>
		/// <returns>True if a PCI bus was found and setup. False otherwise</returns>
		public static bool IsAvailable
    {
      [AOTAttr.ADCStub]
      get { return false; }
    }

		/// <summary>
		/// Return the configuration data from PCI address
		/// </summary>
		/// <param name="bus"></param>
		/// <param name="slot"></param>
		/// <param name="function"></param>
		/// <param name="register"></param>
		/// <returns>The 32 bits value of the pointed register</returns>
    [AOTAttr.ADCStub]
    public unsafe static uint ReadConfig(uint bus, uint slot, uint function, uint register)
    {
      return 0;
    }

		/// <summary>
		/// Get a pointer to the specified PCIDevice structure.
		/// </summary>
		/// <param name="index">The index of the device</param>
		/// <returns>The address of the instance</returns>
    [AOTAttr.ADCStub]
    public unsafe static uint Devices(int index) // returns PCIDevice*
    { 
      return 0;
    }

		/// <summary>
		/// Get the number of devices found
		/// </summary>
		/// <returns></returns>
    public unsafe static int DeviceCount
    {
      [AOTAttr.ADCStub]
      get { return 0; }
    }


  }
}
