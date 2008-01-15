// 
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Bruce Markham <illuminus86@gmail.com>
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
	public unsafe static class Help {
		public const string name = "help";
		public const string shortDescription = "Retrieves help information about a command";
		public const string lblExecute = "COMMANDS.help.Execute";
		public const string lblGetHelp = "COMMANDS.help.GetHelp";

		[Label (lblExecute)]
		public static void Execute (CommandExecutionContext* context)
		{
			CommandExecutionAttemptResult result = Prompter.CommandTable->HandleLine (context->parameters, false, true);

			if (result == CommandExecutionAttemptResult.NotFound) {
				int indexOfSpace = context->parameters->IndexOf (" ");
				CString8* tempStr;
				if (indexOfSpace >= 0)
					tempStr = context->parameters->Substring (0, indexOfSpace);
				else
					tempStr = CString8.Copy (context->parameters);

				TextMode.Write ("No command '");
				TextMode.Write (tempStr);
				TextMode.WriteLine ("' is available to retrieve help for.");
				TextMode.WriteLine (CommandTableHeader.inform_USE_HELP_COMMANDS);

				CString8.DISPOSE (tempStr);
				return;
			}
			if (result == CommandExecutionAttemptResult.BlankEntry) {
				ADC.MemoryUtil.Call ((void*) Stubs.GetFunctionPointer (lblGetHelp), (void*) context);
			}
		}

		[Label (lblGetHelp)]
		public static void GetHelp (CommandExecutionContext* context)
		{
			TextMode.WriteLine ("Syntax: ");
			TextMode.WriteLine ("     help <command>");
			TextMode.WriteLine ("");
			TextMode.WriteLine ("Prints help information about the given command.");
			TextMode.WriteLine (CommandTableHeader.inform_USE_HELP_COMMANDS);
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