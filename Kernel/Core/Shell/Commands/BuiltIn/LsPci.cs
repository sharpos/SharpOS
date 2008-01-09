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
using System.Collections.Generic;
using System.Text;
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
			// ARCHDEPENDS: X86

			if (SharpOS.Kernel.ADC.X86.PCI.IsAvailable ()) {
				ADC.TextMode.WriteLine ("Pci found");
				ADC.X86.PCI.ReportConfig ();
			} else
				ADC.TextMode.WriteLine ("Pci not found");
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
