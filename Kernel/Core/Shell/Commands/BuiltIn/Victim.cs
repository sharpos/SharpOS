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
        public unsafe static class Victim {
                private const string name = "victim";
                private const string shortDescription = "This is a placeholder for a bug";
                private const string lblExecute = "COMMANDS.victim.Execute";
                private const string lblGetHelp = "COMMANDS.victim.GetHelp";

                [Label (lblExecute)]
                public static void Execute(CommandExecutionContext* context) {
                        TextMode.WriteLine ("IT WORKS");
                }

                [Label (lblGetHelp)]
                public static void GetHelp(CommandExecutionContext* context) {

                }

                public static CommandTableEntry* CREATE() {
                        CommandTableEntry* entry = (CommandTableEntry*) SharpOS.ADC.MemoryManager.Allocate ((uint) sizeof (CommandTableEntry));

                        entry->name = (CString8*) SharpOS.Stubs.CString (name);
                        entry->shortDescription = (CString8*) SharpOS.Stubs.CString (shortDescription);
                        entry->func_Execute = (void*) SharpOS.Stubs.GetLabelAddress (lblExecute);
                        entry->func_GetHelp = (void*) SharpOS.Stubs.GetLabelAddress (lblGetHelp);

                        return entry;
                }
        }
}