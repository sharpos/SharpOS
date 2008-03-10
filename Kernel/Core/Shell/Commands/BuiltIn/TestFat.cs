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
	public unsafe static class TestFat
	{
		public const string name = "testfat";
		public const string shortDescription = "Retrieves a file from embedded FAT12 file system";
		public const string lblExecute = "COMMANDS.TESTFAT.Execute";
		public const string lblGetHelp = "COMMANDS.TESTFAT.GetHelp";

		[Label(lblExecute)]
		public static void Execute(CommandExecutionContext* context)
		{
			try {
				TextMode.WriteLine("Step 1 - Initializing RamDisk");

				KernelDisk.Setup();

				ContinuousRamDisk ram = KernelDisk.RamDisk;

				TextMode.WriteLine("Step 2 - Mounting FAT");

				FAT fat = new FAT(ram);

				if (fat == null) {
					TextMode.WriteLine("Step 2 - Failed");
					return;
				}

				TextMode.WriteLine("Step 3 - Reading Boot Sector");

				if (!fat.ReadBootSector()) {
					TextMode.WriteLine("Step 3 - Failed");
					return;
				}

				TextMode.WriteLine("Step 4 - Finding File");

				OpenFile file = fat.FindFile(@"DIR2\GPL-3_0.TXT");

				TextMode.WriteLine("Step 5 - Loading File");

				ByteBuffer buf = fat.LoadFile(file);

				TextMode.WriteLine("Step 6 - Displaying File");

				TextMode.WriteLine(buf.data);
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
