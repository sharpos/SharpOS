// 
// (C) 2008 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Stanislaw Pitucha <viraptor@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT.Attributes;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.Shell.Commands.BuiltIn {
	public unsafe static class LsPci {
		public const string name = "lspci";
		public const string shortDescription = "Displays info about PCI and inserted cards";
		public const string lblExecute = "COMMANDS.lspci.Execute";
		public const string lblGetHelp = "COMMANDS.lspci.GetHelp";

		[Label (lblExecute)]
		public static void Execute (CommandExecutionContext* context)
		{
			//if (!PCIController.IsAvailable) {
			//    ADC.TextMode.WriteLine ("Pci not found");
			//}

			//StringBuilder* sb = SharpOS.Kernel.Foundation.StringBuilder.CREATE ();

			//sb->AppendNumber (PCIController.DeviceCount);
			//sb->Append (" PCI devices were detected.");
			//ADC.TextMode.WriteLine (sb->buffer);

			//for (int index = 0; index < PCIController.DeviceCount; index++)
			//{
			//    PCIDevice* pciDev = (PCIDevice*)PCIController.Devices (index);
			//    if ((int)pciDev != 0)
			//    {
			//        sb->Clear ();
			//        sb->Append ("PCI");
			//        sb->AppendNumber (index);
			//        sb->Append (":");
			//        sb->AppendNumber ((int)pciDev->Bus);
			//        sb->Append (":");
			//        sb->AppendNumber ((int)pciDev->Slot);
			//        sb->Append (".");
			//        sb->AppendNumber ((int)pciDev->Function);
			//        sb->Append (" Vendor:0x");
			//        sb->AppendNumber (pciDev->VendorID, true);
			//        sb->Append (" Device:0x");
			//        sb->AppendNumber (pciDev->DeviceID, true);
			//        sb->Append (" Rev:0x");
			//        sb->AppendNumber (pciDev->RevisionID, true);
			//        sb->Append (" Class:0x");
			//        sb->AppendNumber ((int)((uint)pciDev->ClassCode << 8 | pciDev->ProgIF), true);
			//        //[cedrou] not yet !
			//        //sb->Append (pciDev->ClassCode.ToString());
			//        ADC.TextMode.WriteLine (sb->buffer);
			//    }
			//}

			//MemoryManager.Free ((void*)sb->buffer);
			//MemoryManager.Free ((void*)sb);
		}

		[Label (lblGetHelp)]
		public static void GetHelp (CommandExecutionContext* context)
		{
			TextMode.WriteLine ("Syntax: ");
			TextMode.WriteLine ("     lspci");
			TextMode.WriteLine ("");
			TextMode.WriteLine ("Gets information about PCI.");
		}

		public static CommandTableEntry* CREATE ()
		{
			CommandTableEntry* entry = (CommandTableEntry*) SharpOS.Kernel.ADC.MemoryManager.Allocate ((uint) sizeof (CommandTableEntry));

			entry->name = (CString8*) SharpOS.Kernel.Stubs.CString (name);
			entry->shortDescription = (CString8*) SharpOS.Kernel.Stubs.CString (shortDescription);
			entry->func_Execute = (void*) SharpOS.Kernel.Stubs.GetLabelAddress (lblExecute);
			entry->func_GetHelp = (void*) SharpOS.Kernel.Stubs.GetLabelAddress (lblGetHelp);
			entry->nextEntry = null;

			return entry;
		}
	}
}
