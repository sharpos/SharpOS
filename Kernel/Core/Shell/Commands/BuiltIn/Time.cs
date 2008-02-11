//
// (C) 2008 The SharpOS Project.
//
// Authors:
//	William Lahti <xfurious@gmail.com>

using System;
using System.Runtime.InteropServices;
using SharpOS.AOT.Attributes;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.Shell.Commands.BuiltIn {
	public unsafe static class Time {
		private const string name = "time";
		private const string shortDescription = "Prints the system time";
		private const string lblExecute = "COMMANDS.time.Execute";
		private const string lblGetHelp = "COMMANDS.time.GetHelp";

		[Label (lblExecute)]
		public static void Execute (CommandExecutionContext* context)
		{
			SharpOS.Kernel.Foundation.Time time = new SharpOS.Kernel.Foundation.Time ();
			byte *rawbuf = stackalloc byte [50];
			PString8 *pstr = PString8.Wrap (rawbuf, 50);

			if (context->parameters->Compare ("--hw") == 0)
				Clock.GetHardwareTime (time);
			else
				Clock.GetCurrentTime (time);

			time.ToString (pstr);
			TextMode.WriteLine (pstr);
		}

		[Label (lblGetHelp)]
		public static void GetHelp (CommandExecutionContext* context)
		{
			TextMode.WriteLine ("Usage: time");
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
