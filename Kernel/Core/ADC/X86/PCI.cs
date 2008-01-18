// 
// (C) 2008 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Stanislaw Pitucha <viraptor@gmail.com>
//	Cédric Rousseau <cedrou@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

namespace SharpOS.Kernel.ADC.X86 {
  public unsafe class PCIController	{
		#region Constants

		private static readonly uint MAX_DEVICES = 16;
		private static readonly uint BASE_VALUE = 0x80000000;

		#endregion
		
		#region Private fields

    private static bool isAvailable;
		private static PCIDevice** deviceList;
    private static int deviceCount;

		#endregion

		#region ADC implementation

		public unsafe static uint Devices(int index) 
    {
      if (index >= 0 && index < MAX_DEVICES) 
        return (uint)deviceList[index]; 
      return 0; 
    }

    public static int DeviceCount
		{
      get { return deviceCount; }
		}

    public static uint ReadConfig(uint bus, uint slot, uint function, uint register)
    {
      uint address = 0x80000000
                   | ((bus & 0xFF) << 16)
                   | ((slot & 0x0F) << 11)
                   | ((function & 0x07) << 8)
                   | (register & 0xFC);

      IO.Out32(IO.Port.PCI_Config_Address, address);
      return IO.In32(IO.Port.PCI_Config_Data);
    }

		// check for the presence of a device at the specific PCI address
    public static bool ProbeDevice(uint bus, uint slot, uint fun)
    {
      uint data = ReadConfig(bus, slot, fun, 0);
      return (data != 0xFFFFFFFF);
    }

		// check PCI bus availability by probing IO
		public static bool IsAvailable 
		{
      get { return isAvailable; }
		}
		

    public unsafe static void Setup()
    {
			IO.Out32(IO.Port.PCI_Config_Address, BASE_VALUE);
      isAvailable = (IO.In32 (IO.Port.PCI_Config_Address) == BASE_VALUE);

			//CR- Don't know why but the following static allocation code cannot be AOTted.
			//CR- The same error occurs when the allocation is done with the declaration of deviceList.
			//CR-     EXEC : error : SharpOS.AOT.IR.EngineException: Could not propagate 'Reg0_4__I4'.
      //deviceList = (PCIDevice**)Stubs.StaticAlloc((uint)(sizeof(PCIDevice*) * MAX_DEVICES));
			deviceList = (PCIDevice**)MemoryManager.Allocate ((uint)sizeof (PCIDevice*) * MAX_DEVICES);
			for (int index = 0; index < MAX_DEVICES; index++)
      {
        deviceList[index] = (PCIDevice*)0;
      }
      deviceCount = 0;

			// iterate over bus/device/function to list all devices
			for (uint bus = 0; bus < 256 && deviceCount < MAX_DEVICES; bus++) {
        for (uint slot = 0; slot < 16 && deviceCount < MAX_DEVICES; slot++) {
          for (uint fun = 0; fun < 7 && deviceCount < MAX_DEVICES; fun++) {
            if (ProbeDevice(bus, slot, fun)) {
              deviceList[deviceCount++] = PCIDevice.CREATE(bus, slot, fun);
            }
          }
        }
      }
    }

		//[cedrou] is this member really useful ???
    public unsafe static void Destroy()
    {
      for (int index = 0; index < MAX_DEVICES; index++) {
        if ((int)deviceList[index] != 0) {
          MemoryManager.Free((void*)deviceList[index]);
        }
      }
			MemoryManager.Free ((void*)deviceList);
		}

		#endregion
	}
}
