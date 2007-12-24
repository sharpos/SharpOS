// 
// (C) 2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Bruce Markham <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

//#define Prompter_DebuggingVerbosity

using System;
using SharpOS.Shell.Commands;
using SharpOS.AOT.Attributes;
using SharpOS.ADC;
using SharpOS.Foundation;

namespace SharpOS.Shell
{
    public unsafe static class Prompter
    {
        private static bool initialized = false;
        private static bool running = false;
        private static StringBuilder* lineBuffer;
        private const string LineSeperator = "\n";

        public static CommandTableHeader* CommandTable;
        public static void Setup()
        {
            if (initialized)
                return;

            CommandTable = CommandTableHeader.GenerateDefault();
            lineBuffer = StringBuilder.CREATE(80 * 5);

            initialized = true;
        }

        public static void Start()
        {
            if (initialized == false)
                Setup();

            TextMode.WriteLine();
            WritePrompt();
            
            running = true;
        }

        public static void WritePrompt()
        {
            TextMode.Write("#OS> ");
        }

        public static void Pulse()
        {
            if (running == false)
                return;

            CString8* line = DequeueLine();
            if (line != null)
            {
                HandleLine(line);
                CString8.DISPOSE(line);
            }
        }

        private static void HandleLine(CString8* input)
        {
            Diagnostics.Assert(input!=null,"Prompter::HandleLine(CString8*): Parameter 'input' is null");
#if Prompter_DebuggingVerbosity
            Diagnostics.Message("Prompter::HandleLine(CString8*): Function started");
#endif

            if (input->Length == 0)
            {
#if Prompter_DebuggingVerbosity
                Diagnostics.Message("Prompter::HandleLine(CString8*): Raw input is blank");
#endif
                HandleEmptyCommandEntry();
#if Prompter_DebuggingVerbosity
                Diagnostics.Message("Prompter::HandleLine(CString8*): RET");
#endif
                return;
            }
            CString8* trimmedInput = input->Trim();
            if (trimmedInput->Length == 0)
            {
#if Prompter_DebuggingVerbosity
                Diagnostics.Message("Prompter::HandleLine(CString8*): Trimmed input is blank");
#endif
                CString8.DISPOSE(trimmedInput);
                HandleEmptyCommandEntry();
#if Prompter_DebuggingVerbosity
                Diagnostics.Message("Prompter::HandleLine(CString8*): RET");
#endif
                return;
            }
            
            int firstSpace = trimmedInput->IndexOf(" ");
            CString8* commandName;
            CString8* parameters;
            if (firstSpace < 0)
            {
                commandName = trimmedInput;
                parameters = CString8.CreateEmpty();
            }
            else
            {
                commandName = trimmedInput->Substring(0, firstSpace);
                parameters = trimmedInput->Substring(firstSpace + 1);
            }

            CommandTableEntry* command = CommandTable->FindCommand(commandName);
            if (command == null)
            {
#if Prompter_DebuggingVerbosity
                Diagnostics.Message("Prompter::HandleLine(CString8*): Command not found");
#endif
                HandleUnrecognizedCommandEntry(commandName);

#if Prompter_DebuggingVerbosity
                Diagnostics.Message("Prompter::HandleLine(CString8*): Freeing contextual stuff");
#endif
                //Free up what we used
                if (commandName != trimmedInput)
                    CString8.DISPOSE(commandName);
                CString8.DISPOSE(trimmedInput);
                CString8.DISPOSE(parameters);
#if Prompter_DebuggingVerbosity
                Diagnostics.Message("Prompter::HandleLine(CString8*): RET");
#endif
                return;
            }
            CommandExecutionContext* commandExecutionContext = CommandExecutionContext.CREATE();
            commandExecutionContext->parameters = parameters;

#if Prompter_DebuggingVerbosity
            Diagnostics.Message("Prompter::HandleLine(CString8*): Getting ready to call command");
#endif
            ADC.Memory.Call(command->func_Execute, (void*)commandExecutionContext);
#if Prompter_DebuggingVerbosity
            Diagnostics.Message("Prompter::HandleLine(CString8*): Done calling command");
#endif

            TextMode.WriteLine();
            WritePrompt();

            //Free up what we used
#if Prompter_DebuggingVerbosity
            Diagnostics.Message("Prompter::HandleLine(CString8*): Freeing contextual stuff");
#endif
            if (commandName != trimmedInput)
                CString8.DISPOSE(commandName);
            CString8.DISPOSE(trimmedInput);
            CString8.DISPOSE(parameters);
            CommandExecutionContext.DISPOSE(commandExecutionContext);
#if Prompter_DebuggingVerbosity
            Diagnostics.Message("Prompter::HandleLine(CString8*): RET");
#endif
        }

        private static void HandleUnrecognizedCommandEntry(CString8* commandName)
        {
            TextMode.Write("Unrecognized command: \""); TextMode.Write(commandName); TextMode.WriteLine("\"");
            TextMode.WriteLine();
            WritePrompt();
        }

        private static void HandleEmptyCommandEntry()
        {
            TextMode.WriteLine("Blank command entries don't do anything...");
            TextMode.WriteLine();
            WritePrompt();
        }

        public static void QueueLine(CString8* input)
        {
            if (initialized == false)
                Setup();
            
            Diagnostics.Assert(input != null, "Prompter::QueueLine(CString8*): Parameter 'input' is null");

            if (lineBuffer->Length > 0)
                lineBuffer->Append(LineSeperator);
            if (input->Length == 0)
            {
                lineBuffer->Append(" ");
            }
            else
                lineBuffer->Append(input);
        }

        private static CString8* DequeueLine()
        {
            Diagnostics.Assert(initialized, "Prompter::DequeueLine(): Prompter is not initialized");

            if (lineBuffer->Length == 0)
                return null;

            int indexOfSeperator = lineBuffer->buffer->IndexOf(LineSeperator);

            if (indexOfSeperator >= 0)
            {
                CString8* result = lineBuffer->buffer->Substring(0, indexOfSeperator);
                lineBuffer->RemoveAt(0, indexOfSeperator + LineSeperator.Length);
                return result;
            }
            else // (lineBuffer->Length > 0) but no seperator is present
            {
                CString8* result = lineBuffer->buffer->Substring(0);
                lineBuffer->Clear();
                return result;
            }
        }

        public static void DisplayCommandList()
        {
            DisplayCommandList(CommandTable);
        }
        internal static void DisplayCommandList(CommandTableHeader* commandTable)
        {
            Diagnostics.Assert(commandTable != null, "Prompter::DisplayCommandList(CommandTableHeader*): Parameter 'commandTable' is null");

            if (commandTable->firstEntry == null)
            {
                ADC.TextMode.WriteLine("No commands to display; the commands list is empty.");
                return;
            }
            else
            {
                const int firstColWidth = 22;
                
                string colALabel = "  NAME";
                string colBLabel = "DESCRIPTION";
                TextMode.Write(colALabel);
                for (int spaces = firstColWidth - colALabel.Length;
                    spaces > 0;
                    spaces--)
                    ADC.TextMode.Write(" ");
                TextMode.WriteLine(colBLabel);

                CommandTableEntry* currentEntry;
                for (currentEntry = commandTable->firstEntry;
                    currentEntry != null;
                    currentEntry = currentEntry->nextEntry)
                {
                    ADC.TextMode.Write("[");
                    ADC.TextMode.Write(currentEntry->name);
                    ADC.TextMode.Write("]");
                    int spaces = firstColWidth - (currentEntry->name->Length) - 2;
                    if (spaces < 0)
                        spaces = 0;
                    for (; spaces > 0; spaces--)
                        ADC.TextMode.Write(" ");
                    ADC.TextMode.WriteLine(currentEntry->shortDescription);
                }
            }
        }
    }
}