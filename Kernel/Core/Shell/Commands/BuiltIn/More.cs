// 
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//  Phil Garcia (aka tgiphil) <phil@thinkedge.com>
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

namespace SharpOS.Kernel.Shell.Commands.BuiltIn
{
	public unsafe static class More
	{
		public const string name = "more";
		public const string shortDescription = "Display a file";
		public const string lblExecute = "COMMANDS.more.Execute";
		public const string lblGetHelp = "COMMANDS.more.GetHelp";

		[Label (lblExecute)]
		public static void Execute (CommandExecutionContext* context)
		{
			try {
				TextMode.WriteLine ("Reading..");

				string filename = Foundation.Convert.ToString (context->parameters);

				// for testing...
				if (string.IsNullOrEmpty (filename))
					filename = "/embedded/TEST0.TXT";

				TextMode.Write ("File:");
				TextMode.WriteLine (filename);

				TextMode.WriteLine ("More.Execute.1");
				System.IO.Stream filestream = (System.IO.Stream) Vfs.VirtualFileSystem.Open (filename, System.IO.FileAccess.Read, System.IO.FileShare.Read);
				TextMode.WriteLine ("More.Execute.2");
				
			}
			catch (Exception e) {
				TextMode.Write ("Exception: ");
				TextMode.Write (e.ToString ());
				TextMode.WriteLine ();
			}
		}

		[Label (lblGetHelp)]
		public static void GetHelp (CommandExecutionContext* context)
		{
			TextMode.WriteLine ("Syntax: ");
			TextMode.WriteLine ("     more <filename>");
			TextMode.WriteLine ();
			TextMode.WriteLine ("Displays the contents of a file");
		}

		public static CommandTableEntry* CREATE ()
		{
			CommandTableEntry* entry = (CommandTableEntry*)SharpOS.Kernel.ADC.MemoryManager.Allocate ((uint)sizeof (CommandTableEntry));

			entry->name = (CString8*)SharpOS.Kernel.Stubs.CString (name);
			entry->shortDescription = (CString8*)SharpOS.Kernel.Stubs.CString (shortDescription);
			entry->func_Execute = (void*)SharpOS.Kernel.Stubs.GetLabelAddress (lblExecute);
			entry->func_GetHelp = (void*)SharpOS.Kernel.Stubs.GetLabelAddress (lblGetHelp);
			entry->nextEntry = null;

			return entry;
		}
	}
}
