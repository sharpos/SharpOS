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
using SharpOS.Foundation;
using SharpOS.ADC;

namespace SharpOS.Shell.Commands.BuiltIn {
        public unsafe static class CpuId {
                public const string name = "cpuid";
                public const string shortDescription = "Displays info about the CPU and its capabilities";
                public const string lblExecute = "COMMANDS.cpu.Execute";
                public const string lblGetHelp = "COMMANDS.cpu.GetHelp";

                [Label (lblExecute)]
                public static void Execute(CommandExecutionContext* context) {
                        // ARCHDEPENDS: X86

                        SharpOS.ADC.X86.CPU.WriteBrandName ();
                        SharpOS.ADC.X86.CPU.WriteProcessorInfo ();
                }

                [Label (lblGetHelp)]
                public static void GetHelp(CommandExecutionContext* context) {
                        TextMode.WriteLine ("Syntax: ");
                        TextMode.WriteLine ("     cpuid");
                        TextMode.WriteLine ("");
                        TextMode.WriteLine ("Gets information about the CPU.");
                }

                public static CommandTableEntry* CREATE() {
                        CommandTableEntry* entry = (CommandTableEntry*) SharpOS.ADC.MemoryManager.Allocate ((uint) sizeof (CommandTableEntry));

                        entry->name = (CString8*) SharpOS.Stubs.CString (name);
                        entry->shortDescription = (CString8*) SharpOS.Stubs.CString (shortDescription);
                        entry->func_Execute = (void*) SharpOS.Stubs.GetLabelAddress (lblExecute);
                        entry->func_GetHelp = (void*) SharpOS.Stubs.GetLabelAddress (lblGetHelp);
                        entry->nextEntry = null;

                        return entry;
                }
        }
}