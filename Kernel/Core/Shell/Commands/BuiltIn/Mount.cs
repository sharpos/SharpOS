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
	public unsafe static class Mount
	{
		public const string name = "mount";
		public const string shortDescription = "Mount file system";
		public const string lblExecute = "COMMANDS.mount.Execute";
		public const string lblGetHelp = "COMMANDS.mount.GetHelp";

		[Label (lblExecute)]
		public static void Execute (CommandExecutionContext* context)
		{
			try {
				TextMode.WriteLine ("Mounting");

				string parameters = Foundation.Convert.ToString (context->parameters);

				// for testing...
				if (string.IsNullOrEmpty (parameters))
					parameters = "/IDE_496/Disk0/Partition1 /Disk1";

				int space = parameters.IndexOf (' ');

				if (space <= 0) {
					TextMode.WriteLine ("Incorrect syntax");
					return;
				}

				string source = parameters.Substring (0, space);
				string target = parameters.Substring (space + 1, parameters.Length - 1 - space);

				TextMode.Write ("Source:");
				TextMode.WriteLine (source);
				TextMode.Write ("Target:");
				TextMode.WriteLine (target);

				Vfs.VirtualFileSystem.Mount (source, target);
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
			TextMode.WriteLine ("     mount <target> <source>");
			TextMode.WriteLine ();
			TextMode.WriteLine ("Mounts a file system at <target> from block device at <source>.");
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
