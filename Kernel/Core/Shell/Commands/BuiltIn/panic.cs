using System;
using System.Collections.Generic;
using System.Text;
using SharpOS.AOT.Attributes;
using SharpOS.Foundation;
using SharpOS.ADC;

namespace SharpOS.Shell.Commands.BuiltIn {
	public unsafe static class panic {
		private const string name = "panic";
		private const string shortDescription = "This tests the panic mode";
		private const string lblExecute = "COMMANDS.panic.Execute";
		private const string lblGetHelp = "COMMANDS.panic.GetHelp";

		[Label (lblExecute)]
		public static void Execute (CommandExecutionContext* context)
		{
			Diagnostics.Panic ("You tested the panic.");
		}

		[Label (lblGetHelp)]
		public static void GetHelp (CommandExecutionContext* context)
		{
			TextMode.WriteLine ("Syntax: ");
			TextMode.WriteLine ("     panic");
			TextMode.WriteLine ("");
			TextMode.WriteLine ("Causes a RSOD (Red Screen Of Death).");
		}

		public static CommandTableEntry* CREATE ()
		{
			CommandTableEntry* entry = (CommandTableEntry*) SharpOS.ADC.MemoryManager.Allocate ((uint) sizeof (CommandTableEntry));

			entry->name = (CString8*) SharpOS.Stubs.CString (name);
			entry->shortDescription = (CString8*) SharpOS.Stubs.CString (shortDescription);
			entry->func_Execute = (void*) SharpOS.Stubs.GetLabelAddress (lblExecute);
			entry->func_GetHelp = (void*) SharpOS.Stubs.GetLabelAddress (lblGetHelp);

			return entry;
		}
	}
}
