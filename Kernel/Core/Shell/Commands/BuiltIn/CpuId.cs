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
			IProcessor cpu = Architecture.GetProcessors();
			cpu.Setup();

			TextMode.SaveAttributes();
			
			ProcessorType type = cpu.ArchType;
			if (type == ProcessorType.IA32)
				RenderItem("Architecture: ", "IA32");
			if (type == ProcessorType.IA64)
				RenderItem("Architecture: ", "IA64");
			if (type == ProcessorType.Unknown)
				RenderItem("Architecture: ", "Unknown");

			RenderItem("CPU Vendor: ", cpu.VendorName);
			RenderItem("CPU Brand: ", cpu.BrandName);
			RenderItem("CPU Family: ", cpu.FamilyName);
			RenderItem("CPU Model: ", cpu.ModelName);
			//RenderItem("CPU ClockSpeed: ", cpu.ClockSpeed);
			//RenderItem("CPU CacheSize: ", cpu.CacheSize);

			RenderItemTitle("CPU Flags: ");
			//FeatureFlags flags = cpu.FeatureFlags;
			UInt64 flags = cpu.Features;

			if (0 != (flags & (UInt64)FeatureFlags.FPU   )) TextMode.Write("FPU ");
			if (0 != (flags & (UInt64)FeatureFlags.VME   )) TextMode.Write("VME ");
			if (0 != (flags & (UInt64)FeatureFlags.DE    )) TextMode.Write("DE ");
			if (0 != (flags & (UInt64)FeatureFlags.PSE   )) TextMode.Write("PSE ");
			if (0 != (flags & (UInt64)FeatureFlags.TSC   )) TextMode.Write("TSC ");
			if (0 != (flags & (UInt64)FeatureFlags.MSR   )) TextMode.Write("MSR ");
			if (0 != (flags & (UInt64)FeatureFlags.PAE   )) TextMode.Write("PAE ");
			if (0 != (flags & (UInt64)FeatureFlags.MCE   )) TextMode.Write("MCE ");
			if (0 != (flags & (UInt64)FeatureFlags.CX8   )) TextMode.Write("CX8 ");
			if (0 != (flags & (UInt64)FeatureFlags.APIC  )) TextMode.Write("APIC ");
			if (0 != (flags & (UInt64)FeatureFlags.SEP   )) TextMode.Write("SEP ");
			if (0 != (flags & (UInt64)FeatureFlags.MTRR  )) TextMode.Write("MTRR ");
			if (0 != (flags & (UInt64)FeatureFlags.PGE   )) TextMode.Write("PGE ");
			if (0 != (flags & (UInt64)FeatureFlags.MCA   )) TextMode.Write("MCA ");
			if (0 != (flags & (UInt64)FeatureFlags.CMOV  )) TextMode.Write("CMOV ");
			if (0 != (flags & (UInt64)FeatureFlags.PAT   )) TextMode.Write("PAT ");
			if (0 != (flags & (UInt64)FeatureFlags.PSE36 )) TextMode.Write("PSE36 ");
			if (0 != (flags & (UInt64)FeatureFlags.PSN   )) TextMode.Write("PSN ");
			if (0 != (flags & (UInt64)FeatureFlags.CLFSH )) TextMode.Write("CLFSH ");
			if (0 != (flags & (UInt64)FeatureFlags.DTES  )) TextMode.Write("DTES ");
			if (0 != (flags & (UInt64)FeatureFlags.ACPI  )) TextMode.Write("ACPI ");
			if (0 != (flags & (UInt64)FeatureFlags.MMX   )) TextMode.Write("MMX ");
			if (0 != (flags & (UInt64)FeatureFlags.FXSR  )) TextMode.Write("FXSR ");
			if (0 != (flags & (UInt64)FeatureFlags.SSE   )) TextMode.Write("SSE ");
			if (0 != (flags & (UInt64)FeatureFlags.SSE2  )) TextMode.Write("SSE2 ");
			if (0 != (flags & (UInt64)FeatureFlags.SS    )) TextMode.Write("SS ");
			if (0 != (flags & (UInt64)FeatureFlags.HTT   )) TextMode.Write("HTT ");
			if (0 != (flags & (UInt64)FeatureFlags.TM1   )) TextMode.Write("TM1 ");
			if (0 != (flags & (UInt64)FeatureFlags.IA64  )) TextMode.Write("IA64 ");
			if (0 != (flags & (UInt64)FeatureFlags.PBE   )) TextMode.Write("PBE ");
			if (0 != (flags & (UInt64)FeatureFlags.SSE3  )) TextMode.Write("SSE3 ");
			if (0 != (flags & (UInt64)FeatureFlags.DTSE64)) TextMode.Write("DTSE64 ");
			if (0 != (flags & (UInt64)FeatureFlags.MON   )) TextMode.Write("MON ");
			if (0 != (flags & (UInt64)FeatureFlags.DSCPL )) TextMode.Write("DSCPL ");
			if (0 != (flags & (UInt64)FeatureFlags.VMX   )) TextMode.Write("VMX ");
			if (0 != (flags & (UInt64)FeatureFlags.SMX   )) TextMode.Write("SMX ");
			if (0 != (flags & (UInt64)FeatureFlags.EST   )) TextMode.Write("EST ");
			if (0 != (flags & (UInt64)FeatureFlags.TM2   )) TextMode.Write("TM2 ");
			if (0 != (flags & (UInt64)FeatureFlags.SSSE3 )) TextMode.Write("SSSE3 ");
			if (0 != (flags & (UInt64)FeatureFlags.CID   )) TextMode.Write("CID ");
			if (0 != (flags & (UInt64)FeatureFlags.CX16  )) TextMode.Write("CX16 ");
			if (0 != (flags & (UInt64)FeatureFlags.ETPRD )) TextMode.Write("ETPRD ");
			if (0 != (flags & (UInt64)FeatureFlags.PDCM  )) TextMode.Write("PDCM ");
			if (0 != (flags & (UInt64)FeatureFlags.DCA   )) TextMode.Write("DCA ");
			if (0 != (flags & (UInt64)FeatureFlags.SSE4_1)) TextMode.Write("SSE4.1 ");
			if (0 != (flags & (UInt64)FeatureFlags.SSE4_2)) TextMode.Write("SSE4.2 ");
			if (0 != (flags & (UInt64)FeatureFlags.X2APIC)) TextMode.Write("X2APIC ");
			if (0 != (flags & (UInt64)FeatureFlags.POPCNT)) TextMode.Write("POPCNT ");
			TextMode.WriteLine();
			
			TextMode.RestoreAttributes();
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
