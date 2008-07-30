// 
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Text;
using SharpOS.AOT.Attributes;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.DeviceSystem;
using SharpOS.Kernel.DeviceSystem.PCI;

namespace SharpOS.Kernel.Shell.Commands.BuiltIn
{
	public unsafe static class ListResources
	{
		private const string name = "listresources";
		private const string shortDescription = "Displays available device resources";
		private const string lblExecute = "COMMANDS.listresources.Execute";
		private const string lblGetHelp = "COMMANDS.listresources.GetHelp";

		[Label (lblExecute)]
		public static void Execute (CommandExecutionContext* context)
		{
			TextMode.WriteLine ("Device Resources:");

			Device[] devices = DeviceManager.GetDevices ();

			foreach (Device device in devices) {

				if (!(device is PCIDevice))
					continue;

				TextMode.Write ("Resource: ");
				TextMode.Write (device.Name);

				if (device.Parent != null) {
					TextMode.Write (" - Parent: ");
					TextMode.Write (device.Parent.Name);
				}
				TextMode.WriteLine ();

				if (device is PCIDevice) {
					PCIDevice pciDevice = (device as PCIDevice);

					TextMode.Write ("  Vendor:0x");
					TextMode.Write (pciDevice.VendorID.ToString ("X"));
					TextMode.Write (" Device:0x");
					TextMode.Write (pciDevice.DeviceID.ToString ("X"));
					TextMode.Write (" Class:0x");
					TextMode.Write (pciDevice.ClassCode.ToString ("X"));
					TextMode.Write (" Rev:0x");
					TextMode.Write (pciDevice.RevisionID.ToString ("X"));
					TextMode.WriteLine ();

					foreach (PCIBaseAddress address in pciDevice.BaseAddresses) {
						if (address.Address != 0) {
							TextMode.Write ("    ");
							//TextMode.WriteLine (address.ToString ());

							if (address.Region == AddressRegion.IO)
								TextMode.Write ("I/O Port at 0x");
							else
								TextMode.Write ("Memory at 0x");

							TextMode.Write (address.Address.ToString ("X"));

							TextMode.Write (" [size=");

							if ((address.Size & 0xFFFFF) == 0) {
								TextMode.Write ((address.Size >> 20).ToString ());
								TextMode.Write ("M");
							}
							else if ((address.Size & 0x3FF) == 0) {
								TextMode.Write ((address.Size >> 10).ToString ());
								TextMode.Write ("K");
							}
							else
								TextMode.Write (address.Size.ToString ());

							TextMode.Write ("]");

							if (address.Prefetchable)
								TextMode.Write ("(prefetchable)");

							TextMode.WriteLine ();
						}
					}

					if (pciDevice.IRQ != 0) {
						TextMode.Write ("    ");
						TextMode.Write ("IRQ at ");
						TextMode.Write (pciDevice.IRQ.ToString ());
						TextMode.WriteLine ();
					}
				}
			}
		}

		[Label (lblGetHelp)]
		public static void GetHelp (CommandExecutionContext* context)
		{
			TextMode.WriteLine ("Syntax: ");
			TextMode.WriteLine ("     listresources");
			TextMode.WriteLine ("");
			TextMode.WriteLine ("Prints available devoce resources.");
		}

		public static CommandTableEntry* CREATE ()
		{
			CommandTableEntry* entry = (CommandTableEntry*)SharpOS.Kernel.ADC.MemoryManager.Allocate ((uint)sizeof (CommandTableEntry));

			entry->name = (CString8*)SharpOS.Kernel.Stubs.CString (name);
			entry->shortDescription = (CString8*)SharpOS.Kernel.Stubs.CString (shortDescription);
			entry->func_Execute = (void*)SharpOS.Kernel.Stubs.GetLabelAddress (lblExecute);
			entry->func_GetHelp = (void*)SharpOS.Kernel.Stubs.GetLabelAddress (lblGetHelp);

			return entry;
		}
	}
}