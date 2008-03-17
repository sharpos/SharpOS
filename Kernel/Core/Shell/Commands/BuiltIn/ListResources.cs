// 
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Text;
using SharpOS.AOT.Attributes;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.DriverSystem;

namespace SharpOS.Kernel.Shell.Commands.BuiltIn
{
	public unsafe static class ListResources
	{
		private const string name = "listresources";
		private const string shortDescription = "Displays available device resources";
		private const string lblExecute = "COMMANDS.listresources.Execute";
		private const string lblGetHelp = "COMMANDS.listresources.GetHelp";

		[Label (lblExecute)]
		public static void Execute (CommandExecutionContext* context)
		{
			TextMode.WriteLine ("Device Resources:");

			for (uint slot = 0; ; slot++) {
				DeviceResource resource = DeviceResourceManager.GetBySlot (slot);

				if (resource.Status == DeviceResourceStatus.UnableToLocated)
					break;

				if (resource.Status == DeviceResourceStatus.None)
					continue;

				TextMode.Write ((int)slot);
				TextMode.Write (": /devices/");
				TextMode.Write (resource.Name);
				TextMode.Write (" - ");
				TextMode.Write ((int)resource.BlockDevice.GetTotalBlocks ());
				TextMode.Write ("/");
				TextMode.Write ((int)resource.BlockDevice.GetBlockSize ());
				TextMode.WriteLine ();
			}
		}

		[Label (lblGetHelp)]
		public static void GetHelp (CommandExecutionContext* context)
		{
			TextMode.WriteLine ("Syntax: ");
			TextMode.WriteLine ("     listresources");
			TextMode.WriteLine ("");
			TextMode.WriteLine ("Prints available devoce resources.");
		}

		public static CommandTableEntry* CREATE ()
		{
			CommandTableEntry* entry = (CommandTableEntry*)SharpOS.Kernel.ADC.MemoryManager.Allocate ((uint)sizeof (CommandTableEntry));

			entry->name = (CString8*)SharpOS.Kernel.Stubs.CString (name);
			entry->shortDescription = (CString8*)SharpOS.Kernel.Stubs.CString (shortDescription);
			entry->func_Execute = (void*)SharpOS.Kernel.Stubs.GetLabelAddress (lblExecute);
			entry->func_GetHelp = (void*)SharpOS.Kernel.Stubs.GetLabelAddress (lblGetHelp);

			return entry;
		}
	}
}