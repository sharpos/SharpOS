// 
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Jonathan Dickinson <jonathand.za@gmail.com>
//	Sander van Rossen <sander.vanrossen@gmail.com>
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
	public unsafe static class CpuId {
		public const string name = "cpuid";
		public const string shortDescription = "Displays info about the CPU and its capabilities";
		public const string lblExecute = "COMMANDS.cpu.Execute";
		public const string lblGetHelp = "COMMANDS.cpu.GetHelp";
		
		public static void RenderItemTitle(string title)
		{
			TextMode.SetAttributes(TextColor.LightMagenta, TextColor.Black);
			TextMode.Write(title);
			TextMode.SetAttributes(TextColor.LightCyan, TextColor.Black);
		}

		public static void RenderItem(string title, CString8* value)
		{
			RenderItemTitle(title);

			TextMode.WriteLine(value);
		}

		public static void RenderItem(string title, string value)
		{
			RenderItemTitle(title);

			TextMode.WriteLine(value);
		}

		public static void RenderItem(string title, uint value)
		{
			RenderItemTitle(title);

			TextMode.Write((int)value);
			TextMode.WriteLine();
		}
		
		[Label (lblExecute)]
		public static void Execute (CommandExecutionContext* context)
		{
			// ARCHDEPENDS: X86
			IProcessor[] cpus = Architecture.GetProcessors();

			TextMode.SaveAttributes();

			for (int i = 0; i < cpus.Length; i++)
			{
				ProcessorType type = cpus[i].ArchType;
				
				if (type == ProcessorType.IA32)
					RenderItem("Architecture: ", "IA32");
				if (type == ProcessorType.IA64)
					RenderItem("Architecture: ", "IA64");
				if (type == ProcessorType.Unknown)
					RenderItem("Architecture: ", "Unknown");

				RenderItem("CPU Vendor: ", cpus[i].VendorName);
				RenderItem("CPU Brand: ", cpus[i].BrandName);
				RenderItem("CPU Family: ", cpus[i].FamilyName);
				RenderItem("CPU Model: ", cpus[i].ModelName);
				//RenderItem("CPU ClockSpeed: ", cpus[i].ClockSpeed);
				//RenderItem("CPU CacheSize: ", cpus[i].CacheSize);

				RenderItemTitle("CPU Flags: ");

				ProcessorFeature[] features = cpus[i].Features;
				for (int f = 0; f < features.Length; f++)
				{
					if (features[f] == null)
					{
						TextMode.Write("? ");
						continue;
					}
					TextMode.Write(features[f].FeatureName);
					TextMode.Write(" ");
				}
				TextMode.WriteLine();

				TextMode.RestoreAttributes();
			}
		}

		[Label (lblGetHelp)]
		public static void GetHelp (CommandExecutionContext* context)
		{
			TextMode.WriteLine ("Syntax: ");
			TextMode.WriteLine ("     cpuid");
			TextMode.WriteLine ("");
			TextMode.WriteLine ("Gets information about the CPU.");
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
