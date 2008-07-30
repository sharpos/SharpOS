//
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Bruce Markham <illuminus86@gmail.com>
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System.Collections.Generic;
using System.Text;
using SharpOS.Kernel.Foundation;
using System.Runtime.InteropServices;
using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.Shell.Commands
{
	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct CommandTableHeader
	{
		public const string inform_USE_HELP_COMMANDS = "Use 'help commands' to get a list of commands.";

		public int count;
		public CommandTableEntry* firstEntry;

		public CommandTableEntry* FindCommand (CString8* name)
		{
			Diagnostics.Assert (name != null, "CommandTableHeader::FindCommand(CString8*): Parameter 'name' is null");
			Diagnostics.Assert (firstEntry != null, "CommandTableHeader::FindCommand(CString8*): Cannot search through commands; command list is empty");
			CommandTableEntry* currentEntry;
			for (currentEntry = firstEntry;
					currentEntry != null;
					currentEntry = currentEntry->nextEntry) {
				//FIXME: This needs to be case insensitive!
				if (ByteString.Compare (name->Pointer, currentEntry->name->Pointer) == 0)
					return currentEntry;
				//else
				//{
				//    ADC.TextMode.Write("CommandTableHeader::FindCommand(CString8*): Entered '");
				//    ADC.TextMode.Write(name);
				//    ADC.TextMode.Write("' != '");
				//    ADC.TextMode.Write(currentEntry->name);
				//    ADC.TextMode.WriteLine("'");
				//}
			}

			return null;
		}

		internal static void DisplayCommandEntryDebug (CommandTableEntry* entry)
		{
			const int tempBufferSIZE = 30;
			byte* tempBuffer = stackalloc byte[tempBufferSIZE];

			ADC.TextMode.WriteLine ("Dumping CommandTableEntry:");
			if (entry == null) {
				ADC.TextMode.Write (" Pointer Address: ");
				Convert.ToString ((int)entry, true, tempBuffer, tempBufferSIZE, 0);
				ADC.TextMode.Write (tempBuffer);
				ADC.TextMode.WriteLine (" (NULL!)");
			}
			else {
				ADC.TextMode.Write (" Pointer Address: ");
				Convert.ToString ((int)entry, true, tempBuffer, tempBufferSIZE, 0);
				ADC.TextMode.WriteLine (tempBuffer);
			}

			ADC.TextMode.Write (" Name: \"");
			ADC.TextMode.Write (entry->name);
			ADC.TextMode.Write ("\" @ ");
			Convert.ToString ((int)entry->name, true, tempBuffer, tempBufferSIZE, 0);
			ADC.TextMode.WriteLine (tempBuffer);

			ADC.TextMode.Write (" Description: \"");
			ADC.TextMode.Write (entry->shortDescription);
			ADC.TextMode.Write ("\" @ ");
			Convert.ToString ((int)entry->shortDescription, true, tempBuffer, tempBufferSIZE, 0);
			ADC.TextMode.WriteLine (tempBuffer);

			ADC.TextMode.Write (" Execute() @ ");
			Convert.ToString ((int)entry->func_Execute, true, tempBuffer, tempBufferSIZE, 0);
			ADC.TextMode.Write (tempBuffer);
			ADC.TextMode.Write ("; GetHelp() @ ");
			Convert.ToString ((int)entry->func_GetHelp, true, tempBuffer, tempBufferSIZE, 0);
			ADC.TextMode.WriteLine (tempBuffer);

			ADC.TextMode.Write (" nextEntry @ ");
			Convert.ToString ((int)entry->nextEntry, true, tempBuffer, tempBufferSIZE, 0);
			ADC.TextMode.WriteLine (tempBuffer);
		}

		public void AddEntry (CommandTableEntry* entry)
		{
			if (entry == null)
				Diagnostics.Message ("CommandTableHeader::AddEntry(CommandTableEntry*): Parameter 'entry' is null");

			if (firstEntry == null) {
				firstEntry = entry;

				entry->nextEntry = null;
				return;

			}
			else {
				CommandTableEntry* currentEntry = null;
				for (currentEntry = firstEntry;
					currentEntry->nextEntry != null;
					currentEntry = currentEntry->nextEntry) {
				}
				currentEntry->nextEntry = entry;
				entry->nextEntry = null;

				//CommandTableEntry* currentEntry = this.firstEntry;
				//for ( ;
				//    currentEntry->nextEntry != null || currentEntry->nextEntry->name->Compare(currentEntry->name) > 0;
				//    currentEntry = currentEntry->nextEntry)
				//{
				//}
				//if (currentEntry->nextEntry == null)
				//{
				//    currentEntry->nextEntry = entry;
				//    entry->nextEntry = null;
				//}
				//else
				//{
				//    CommandTableEntry* nextEntry = currentEntry->nextEntry;
				//    currentEntry->nextEntry = entry;
				//    entry->nextEntry = nextEntry;
				//}

				return;
			}
		}

		public void DISPOSE ()
		{
			CommandTableEntry* curEntry = this.firstEntry;
			while (curEntry != null) {
				CommandTableEntry* nEntry = curEntry->nextEntry;

				curEntry->DISPOSE ();
				SharpOS.Kernel.ADC.MemoryManager.Free (curEntry);

				curEntry = nEntry;
			}
		}

		internal CommandExecutionAttemptResult HandleLine (CString8* input, bool verboseFailure, bool useHelp)
		{
			Diagnostics.Assert (input != null, "Prompter::HandleLine(CString8*): Parameter 'input' is null");
#if Prompter_DebuggingVerbosity
			Diagnostics.Message("Prompter::HandleLine(CString8*): Function started");
#endif

			if (input->Length == 0) {
#if Prompter_DebuggingVerbosity
			Diagnostics.Message("Prompter::HandleLine(CString8*): Raw input is blank");
#endif
				if (verboseFailure)
					HandleEmptyCommandEntry ();

#if Prompter_DebuggingVerbosity
			Diagnostics.Message("Prompter::HandleLine(CString8*): RET");
#endif
				return CommandExecutionAttemptResult.BlankEntry;
			}
			CString8* trimmedInput = input->Trim ();
			if (trimmedInput->Length == 0) {
#if Prompter_DebuggingVerbosity
			Diagnostics.Message("Prompter::HandleLine(CString8*): Trimmed input is blank");
#endif
				CString8.DISPOSE (trimmedInput);

				if (verboseFailure)
					HandleEmptyCommandEntry ();

#if Prompter_DebuggingVerbosity
			Diagnostics.Message("Prompter::HandleLine(CString8*): RET");
#endif
				return CommandExecutionAttemptResult.BlankEntry;
			}

			int firstSpace = trimmedInput->IndexOf (" ");
			CString8* commandName;
			CString8* parameters;
			if (firstSpace < 0) {
				commandName = trimmedInput;
				parameters = CString8.CreateEmpty ();
			}
			else {
				commandName = trimmedInput->Substring (0, firstSpace);
				parameters = trimmedInput->Substring (firstSpace + 1);
			}

			CommandTableEntry* command = this.FindCommand (commandName);
			if (command == null) {
#if Prompter_DebuggingVerbosity
				Diagnostics.Message("Prompter::HandleLine(CString8*): Command not found");
#endif
				if (verboseFailure)
					HandleUnrecognizedCommandEntry (commandName);

#if Prompter_DebuggingVerbosity
				Diagnostics.Message("Prompter::HandleLine(CString8*): Freeing contextual stuff");
#endif
				//Free up what we used
				if (commandName != trimmedInput)
					CString8.DISPOSE (commandName);
				CString8.DISPOSE (trimmedInput);
				CString8.DISPOSE (parameters);
#if Prompter_DebuggingVerbosity
				Diagnostics.Message("Prompter::HandleLine(CString8*): RET");
#endif
				return CommandExecutionAttemptResult.NotFound;
			}
			CommandExecutionContext* commandExecutionContext = CommandExecutionContext.CREATE ();
			commandExecutionContext->parameters = parameters;

#if Prompter_DebuggingVerbosity
			Diagnostics.Message("Prompter::HandleLine(CString8*): Getting ready to call command");
#endif
			if (!useHelp)
				ADC.MemoryUtil.Call (command->func_Execute, (void*)commandExecutionContext);
			else
				ADC.MemoryUtil.Call (command->func_GetHelp, (void*)commandExecutionContext);
#if Prompter_DebuggingVerbosity
			Diagnostics.Message("Prompter::HandleLine(CString8*): Done calling command");
#endif

			//Free up what we used
#if Prompter_DebuggingVerbosity
			Diagnostics.Message("Prompter::HandleLine(CString8*): Freeing contextual stuff");
#endif
			if (commandName != trimmedInput)
				CString8.DISPOSE (commandName);
			CString8.DISPOSE (trimmedInput);
			CString8.DISPOSE (parameters);
			CommandExecutionContext.DISPOSE (commandExecutionContext);
#if Prompter_DebuggingVerbosity
			Diagnostics.Message("Prompter::HandleLine(CString8*): RET");
#endif
			return CommandExecutionAttemptResult.Success;
		}

		private static void HandleUnrecognizedCommandEntry (CString8* commandName)
		{
			TextMode.Write ("Unrecognized command: \"");
			TextMode.Write (commandName);
			TextMode.WriteLine ("\"");
		}

		private static void HandleEmptyCommandEntry ()
		{
			TextMode.WriteLine (inform_USE_HELP_COMMANDS);
		}

		public static CommandTableHeader* GenerateDefaultRoot ()
		{
			CommandTableHeader* header = (CommandTableHeader*)SharpOS.Kernel.ADC.MemoryManager.Allocate ((uint)sizeof (CommandTableHeader));

			header->firstEntry = null;

			header->AddEntry (BuiltIn.Cls.CREATE ());
			header->AddEntry (BuiltIn.Commands.CREATE ());
			header->AddEntry (BuiltIn.CpuId.CREATE ());
			header->AddEntry (BuiltIn.Egg.CREATE ());
			header->AddEntry (BuiltIn.Halt.CREATE ());
			header->AddEntry (BuiltIn.Help.CREATE ());
			header->AddEntry (BuiltIn.Keymap.CREATE ());
			header->AddEntry (BuiltIn.LS.CREATE ());
			header->AddEntry (BuiltIn.LsPci.CREATE ());
			header->AddEntry (BuiltIn.MemDump.CREATE ());
			header->AddEntry (BuiltIn.MemView.CREATE ());
			header->AddEntry (BuiltIn.Panic.CREATE ());
			header->AddEntry (BuiltIn.Reboot.CREATE ());
			header->AddEntry (BuiltIn.Show.CREATE ());
			header->AddEntry (BuiltIn.Snake.CREATE ());
			header->AddEntry (BuiltIn.Stage.CREATE ());
			header->AddEntry (BuiltIn.Version.CREATE ());
			header->AddEntry (BuiltIn.Time.CREATE ());
			header->AddEntry (BuiltIn.Timezone.CREATE ());
			header->AddEntry (BuiltIn.Testcase.CREATE ());
			header->AddEntry (BuiltIn.ListResources.CREATE ());
			header->AddEntry (BuiltIn.Mount.CREATE ());
			header->AddEntry (BuiltIn.More.CREATE ());

			return header;
		}
	}
}
