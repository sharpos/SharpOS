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
    public unsafe static class help
    {
        public const string name = "help";
        public const string shortDescription = "Retrieves help information about a command";
        public const string lblExecute = "COMMANDS.help.Execute";
        public const string lblGetHelp = "COMMANDS.help.GetHelp";

        [Label(lblExecute)]
        public static void Execute(CommandExecutionContext* context)
        {
            if(context->parameters != null)
                if (context->parameters->Length == 0)
                {
                    CommandExecutionContext* tempHelpContext = CommandExecutionContext.CREATE();
                    ADC.Memory.Call((void*)Stubs.GetFunctionPointer(lblGetHelp), tempHelpContext);
                    CommandExecutionContext.DISPOSE(tempHelpContext);
                    return;
                }

            int indexOfSpace = context->parameters->IndexOf(" ");
            CString8* commandName;
            if (indexOfSpace >= 0)
            {
                commandName = context->parameters->Substring(0, indexOfSpace);
            }
            else
            {
                commandName = CString8.Copy(context->parameters);
            }

            CommandTableEntry* command = Prompter.CommandTable->FindCommand(commandName);
            if (command == null)
            {
                TextMode.Write("No command '");
                TextMode.Write(commandName);
                TextMode.WriteLine("' is available to retrieve help for.");
                return;
            }

            CommandExecutionContext* helpContext = CommandExecutionContext.CREATE();
            if(indexOfSpace>=0)
            {
                CString8* tempParams = context->parameters->Substring(indexOfSpace);
                helpContext->parameters = tempParams->Trim();
                CString8.DISPOSE(tempParams);
            }
            else
            {
                helpContext->parameters = CString8.CreateEmpty();
            }

            ADC.Memory.Call(command->func_GetHelp, (void*)helpContext);

            CString8.DISPOSE(helpContext->parameters);
            CommandExecutionContext.DISPOSE(helpContext);
            CString8.DISPOSE(commandName);
        }

        [Label(lblGetHelp)]
        public static void GetHelp(CommandExecutionContext* context)
        {
            TextMode.WriteLine("Syntax: ");
            TextMode.WriteLine("     help <command>");
            TextMode.WriteLine("");
            TextMode.WriteLine("Prints help information about the given command");
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
