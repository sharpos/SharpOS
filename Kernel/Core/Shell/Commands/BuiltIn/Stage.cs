// 
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Bruce Markham <illuminus86@gmail.com>
//	William Lahti <xfurious@gmail.com>
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
	public unsafe static class Stage {
		public const string name = "stage";
		public const string shortDescription = "Prints the current kernel stage";
		public const string lblExecute = "COMMANDS.stage.Execute";
		public const string lblGetHelp = "COMMANDS.stage.GetHelp";

		[Label (lblExecute)]
		public static void Execute (CommandExecutionContext* context)
		{
			TextMode.Write ("Current kernel stage: ");

			switch (EntryModule.GetKernelStage ()) {
			case KernelStage.Init:
				TextMode.WriteLine ("(0) init");
				break;
			case KernelStage.RuntimeInit:
				TextMode.WriteLine ("(1) runtime-init");
				break;
			case KernelStage.UserInit:
				TextMode.WriteLine ("(2) user-init");
				break;
			case KernelStage.Active:
				TextMode.WriteLine ("(3) active");
				break;
			case KernelStage.SingleUser:
				TextMode.WriteLine ("(4) single-user");
				break;
			case KernelStage.Stopping:
				TextMode.WriteLine ("(5) stopping");
				break;
			case KernelStage.Stop:
				TextMode.WriteLine ("(6) stop");
				break;
			case KernelStage.Halt:
				TextMode.WriteLine ("(7) halt");
				break;
			case KernelStage.Diagnostics:
				TextMode.WriteLine ("(8) diagnostics");
				break;
			case KernelStage.Unknown:
				TextMode.WriteLine ("(?) unknown");
				break;
			}
		}

		[Label (lblGetHelp)]
		public static void GetHelp (CommandExecutionContext* context)
		{
			TextMode.WriteLine ("Syntax: ");
			TextMode.WriteLine ("     stage");
			TextMode.WriteLine ("");
			TextMode.WriteLine ("Prints the current kernel stage.");
		}

		public static CommandTableEntry* CREATE ()
		{
			CommandTableEntry* entry = (CommandTableEntry*) SharpOS.Kernel.ADC.MemoryManager.Allocate ((uint) sizeof (CommandTableEntry));

			entry->name = (CString8*) SharpOS.Kernel.Stubs.CString (name);
			entry->shortDescription = (CString8*) SharpOS.Kernel.Stubs.CString (shortDescription);
			entry->func_Execute = (void*) SharpOS.Kernel.Stubs.GetLabelAddress (lblExecute);
			entry->func_GetHelp = (void*) SharpOS.Kernel.Stubs.GetLabelAddress (lblGetHelp);

			return entry;
		}
	}
}
