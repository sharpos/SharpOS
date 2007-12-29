// 
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Bruce Markham <illuminus86@gmail.com>
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

namespace SharpOS.Shell.Commands.BuiltIn
{
    public unsafe static class halt
    {
        public const string name = "halt";
        public const string shortDescription = "Halts the system";
        public const string lblExecute = "COMMANDS.halt.Execute";
        public const string lblGetHelp = "COMMANDS.halt.GetHelp";

        [Label(lblExecute)]
        public static void Execute(CommandExecutionContext* context)
        {
            Kernel.Halt();
            ADC.BootControl.PowerOff();
            ADC.BootControl.Freeze();
        }

        [Label(lblGetHelp)]
        public static void GetHelp(CommandExecutionContext* context)
        {
            TextMode.WriteLine("Syntax: ");
            TextMode.WriteLine("     halt");
            TextMode.WriteLine("");
            TextMode.WriteLine("Halts the system.");
        }

        public static CommandTableEntry* CREATE()
        {
            CommandTableEntry* entry = (CommandTableEntry*)SharpOS.ADC.MemoryManager.Allocate((uint)sizeof(CommandTableEntry));

            entry->name = (CString8*)SharpOS.Stubs.CString(name);
            entry->shortDescription = (CString8*)SharpOS.Stubs.CString(shortDescription);
            entry->func_Execute = (void*)SharpOS.Stubs.GetLabelAddress(lblExecute);
            entry->func_GetHelp = (void*)SharpOS.Stubs.GetLabelAddress(lblGetHelp);

            return entry;
        }
    }
}