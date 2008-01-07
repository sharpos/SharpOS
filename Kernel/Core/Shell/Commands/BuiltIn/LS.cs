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

namespace SharpOS.Kernel.Shell.Commands.BuiltIn
{
    public unsafe static class LS
    {
        public const string name = "ls";
        public const string shortDescription = "Retrieves file list in the file system";
        public const string lblExecute = "COMMANDS.LS.Execute";
        public const string lblGetHelp = "COMMANDS.LS.GetHelp";

        [Label(lblExecute)]
        public static void Execute(CommandExecutionContext* context)
        {
            SharpOS.Kernel.FileSystem.Ext2FS.ListFile();
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
