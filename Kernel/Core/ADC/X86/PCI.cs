// 
// (C) 2008 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Stanislaw Pitucha <viraptor@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using ADC = SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.ADC.X86 {
	public unsafe class PCI {
		const uint BASE_VALUE = 0x80000000;
		
		// Read configuration DWORD from PCI address
		public unsafe static uint ReadConfig32(uint bus, uint dev, uint fun, uint register) {
			uint address = (uint)(BASE_VALUE | (bus << 16) | (dev << 11) | (fun << 8) | (register & ~3));
			IO.Out32(IO.Port.PCI_Config_Address, address);
			return IO.In32(IO.Port.PCI_Config_Data);
		}
		
		// check PCI bus availability by probing IO
		public unsafe static bool IsAvailable() {
			IO.Out32(IO.Port.PCI_Config_Address, BASE_VALUE);
			if(IO.In32(IO.Port.PCI_Config_Address) != BASE_VALUE)
				return false;
			else
				return true;
		}
		
		// iterate over bus/device/function to list all devices
		public unsafe static void ReportConfig() {
			uint id, bus=0, dev=0, fun=0;
			while(true) {
				id = ReadConfig32(bus, dev, fun, 0);
				if(id != 0xffffffff) {
					ADC.TextMode.Write("bus:dev:fun -> ");
					ADC.TextMode.WriteNumber((int)bus);
					ADC.TextMode.Write(":");
					ADC.TextMode.WriteNumber((int)dev);
					ADC.TextMode.Write(":");
					ADC.TextMode.WriteNumber((int)fun);
					ADC.TextMode.Write(" device ");
					ADC.TextMode.WriteNumber((int)(id&0xffff), true);
					ADC.TextMode.Write(":");
					ADC.TextMode.WriteNumber((int)((id>>16)&0xffff), true);
					ADC.TextMode.WriteLine();
				}
				
				// maximum of 8 functions per device
				if(fun<7)
					fun++;
				else {
					// maximum 32 devices
					if(dev<31)
						dev++;
					else {
						// maximum 8 buses
						if(bus<7)
							bus++;
						else
							break;
						dev=0;
					}
					fun=0;
				}
			}
			ADC.TextMode.WriteLine("End of listing.");
		}
	}
}
