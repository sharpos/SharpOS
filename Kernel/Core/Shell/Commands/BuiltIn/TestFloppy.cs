// 
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Jae Hyun Roh <wonbear@gmail.com>
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
using SharpOS.Kernel.FileSystem;
using SharpOS.Kernel.FileSystem.FAT;
using SharpOS.Kernel.BlockDevice;

namespace SharpOS.Kernel.Shell.Commands.BuiltIn
{
	public unsafe static class TestFloppy
	{
		public const string name = "testfloppy";
		public const string shortDescription = "Test the floppy controller";
		public const string lblExecute = "COMMANDS.TESTFLOPPY.Execute";
		public const string lblGetHelp = "COMMANDS.TESTFLOPPY.GetHelp";

		[Label(lblExecute)]
		public static void Execute(CommandExecutionContext* context)
		{
			try {
				TextMode.WriteLine("Floppy Disk Controller - Test Start");
				//FloppyDiskController.ReadTrack(0, 0, 0);
				TextMode.WriteLine("Floppy Disk Controller - Test End");
			}
			catch (Exception e) {
				TextMode.WriteLine("Exception caught.");
				TextMode.WriteLine(e.ToString());
			}
		}

		public static CommandTableEntry* CREATE()
		{
			CommandTableEntry* entry = (CommandTableEntry*)SharpOS.Kernel.ADC.MemoryManager.Allocate((uint)sizeof(CommandTableEntry));

			entry->name = (CString8*)SharpOS.Kernel.Stubs.CString(name);
			entry->shortDescription = (CString8*)SharpOS.Kernel.Stubs.CString(shortDescription);
			entry->func_Execute = (void*)SharpOS.Kernel.Stubs.GetLabelAddress(lblExecute);

			return entry;
		}
	}
}
