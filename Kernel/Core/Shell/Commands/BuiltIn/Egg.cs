// 
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Jonathan Dickinson <jonathand.za@gmail.com>
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
	public unsafe static class Egg {
		public const string name = "egg";
		public const string shortDescription = "The mandatory easter egg";
		public const string lblExecute = "COMMANDS.egg.Execute";
		public const string lblGetHelp = "COMMANDS.egg.GetHelp";

		[Label (lblExecute)]
		public static void Execute (CommandExecutionContext* context)
		{
			TextMode.WriteLine ();

			TextMode.WriteLine ("              .=\"\"=.");
			TextMode.WriteLine ("             / _  _ \\");
			TextMode.WriteLine ("            |  d  b  |");
			TextMode.WriteLine ("            \\   /\\   /             ,");
			TextMode.WriteLine ("           ,/'-=\\/=-'\\,    |\\   /\\/ \\/|   ,_");
			TextMode.WriteLine ("          / /        \\ \\   ; \\/`     '; , \\_',");
			TextMode.WriteLine ("         | /          \\ |   \\        / ");
			TextMode.WriteLine ("         \\/ \\        / \\/    '.    .'    /`.");
			TextMode.WriteLine ("             '.    .'          `~~` , /\\ `");
			TextMode.WriteLine ("     jgs     _|`~~`|_              .  `");
			TextMode.WriteLine ("             /|\\  /|\\");
			TextMode.WriteLine ();
			TextMode.WriteLine ("What is the egg?");
			TextMode.WriteLine ("The egg has you.");
		}

		[Label (lblGetHelp)]
		public static void GetHelp (CommandExecutionContext* context)
		{
			TextMode.WriteLine ("Syntax: ");
			TextMode.WriteLine ("     egg");
			TextMode.WriteLine ("");
			TextMode.WriteLine ("And we a proud of it!");
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