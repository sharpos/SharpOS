// 
// (C) 2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
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
    public unsafe static class commands
    {
        public const string name = "commands";
        public const string shortDescription = "Displays a list of all the commands";
        public const string lblExecute = "COMMANDS.commands.Execute";
        public const string lblGetHelp = "COMMANDS.commands.GetHelp";

        [Label(lblExecute)]
        public static void Execute(CommandExecutionContext* context)
        {
            Prompter.DisplayCommandList();
        }

        [Label(lblGetHelp)]
        public static void GetHelp(CommandExecutionContext* context)
        {
            TextMode.WriteLine("Syntax: ");
            TextMode.WriteLine("     commands");
            TextMode.WriteLine("");
            TextMode.WriteLine("Displays a list of all commands, and a short description of each.");
        }

        public static CommandTableEntry* CREATE()
        {
            CommandTableEntry* entry = (CommandTableEntry*)SharpOS.ADC.MemoryManager.Allocate((uint)sizeof(CommandTableEntry));

            entry->name = (CString8*)SharpOS.Stubs.CString(name);
            entry->shortDescription = (CString8*)SharpOS.Stubs.CString(shortDescription);
            entry->func_Execute = (void*)SharpOS.Stubs.GetLabelAddress(lblExecute);
            entry->func_GetHelp = (void*)SharpOS.Stubs.GetLabelAddress(lblGetHelp);
            entry->nextEntry = null;

            return entry;
        }
    }
}