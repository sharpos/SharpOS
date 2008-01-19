//
// (C) 2008 The SharpOS Project.
//
// Authors:
//	William Lahti <xfurious@gmail.com>

using SharpOS.AOT.Attributes;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.Shell.Commands.BuiltIn {
	public unsafe static class Timezone {
		private const string name = "timezone";
		private const string shortDescription = "Prints/sets the system timezone";
		private const string lblExecute = "COMMANDS.timezone.Execute";
		private const string lblGetHelp = "COMMANDS.timezone.GetHelp";

		[Label (lblExecute)]
		public static void Execute (CommandExecutionContext* context)
		{
			if (context->parameters->Compare ("--set", 0, 5) == 0) {
				CString8 *substr;
				int result;

				if (context->parameters->Length <= 6) {
					GetHelp (context);
					return;
				}

				substr = context->parameters->Substring (6);
				result = Convert.ToInt32 (substr);
				MemoryManager.Free (substr);

				TextMode.WriteLine ("Setting timezone to `", result, "'");
				Clock.Timezone = (System.SByte)result;

				return;
			}

			TextMode.Write ("Current timezone: ");
			TextMode.Write ((int)Clock.Timezone);
			TextMode.WriteLine ();
		}

		[Label (lblGetHelp)]
		public static void GetHelp (CommandExecutionContext* context)
		{
			TextMode.WriteLine ("Usage: timezone [--set ZONE]");
		}

		public static CommandTableEntry* CREATE ()
		{
			CommandTableEntry* entry = (CommandTableEntry*) MemoryManager.Allocate (
				(uint) sizeof (CommandTableEntry));

			entry->name = (CString8*) SharpOS.Kernel.Stubs.CString (name);
			entry->shortDescription = (CString8*) SharpOS.Kernel.Stubs.CString (shortDescription);
			entry->func_Execute = (void*) SharpOS.Kernel.Stubs.GetLabelAddress (lblExecute);
			entry->func_GetHelp = (void*) SharpOS.Kernel.Stubs.GetLabelAddress (lblGetHelp);

			return entry;
		}
	}
}
