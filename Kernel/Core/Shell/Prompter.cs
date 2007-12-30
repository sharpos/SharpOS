// 
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Bruce Markham <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

//#define Prompter_DebuggingVerbosity

using System;
using SharpOS.Kernel.Shell.Commands;
using SharpOS.AOT.Attributes;
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.Foundation;

namespace SharpOS.Kernel.Shell {
	public unsafe static class Prompter {
		private static bool initialized = false;
		private static bool running = false;
		private static StringBuilder* lineBuffer;
		private const string LineSeperator = "\n";

		public static CommandTableHeader* CommandTable;
		public static void Setup ()
		{
			if (initialized)
				return;

			CommandTable = CommandTableHeader.GenerateDefaultRoot ();
			lineBuffer = StringBuilder.CREATE (80 * 5);

			initialized = true;
		}

		public static void Start ()
		{
			if (initialized == false)
				Setup ();

                        TextMode.WriteLine ("SharpOS  Copyright (C) 2007  The SharpOS Team");
                        TextMode.WriteLine ();
                        TextMode.WriteLine ("This program comes with ABSOLUTELY NO WARRANTY; for details type 'show w'.");
                        TextMode.WriteLine ("This is free software, and you are welcome to redistribute it");
                        TextMode.WriteLine ("under certain conditions; type 'show c' for details.");

			TextMode.WriteLine ();
			WritePrompt ();

			running = true;
		}

		public static void WritePrompt ()
		{
			bool changingColor = TextMode.SaveAttributes ();
			if (changingColor)
				TextMode.SetAttributes (TextColor.LightGreen, TextMode.Background);
			TextMode.Write ("#");
			if (changingColor)
				TextMode.SetAttributes (TextColor.LightCyan, TextMode.Background);
			TextMode.Write ("OS");
			if (changingColor)
				TextMode.SetAttributes (TextColor.White, TextMode.Background);
			TextMode.Write (">");
			if (changingColor)
				TextMode.RestoreAttributes ();
			TextMode.Write (" ");
			TextMode.RefreshCursor ();
		}

		public static void Pulse ()
		{
			if (running == false)
				return;

			CString8* line = DequeueLine ();
			if (line != null) {
				HandleLine (line);
				CString8.DISPOSE (line);
			}
		}

		private static void HandleLine (CString8* input)
		{
			CommandExecutionAttemptResult executionResult;
			executionResult = CommandTable->HandleLine (input, true, false);

			ADC.TextMode.WriteLine ();
			WritePrompt ();
		}

		public static void QueueLine (CString8* input)
		{
			if (initialized == false)
				Setup ();

			Diagnostics.Assert (input != null, "Prompter::QueueLine(CString8*): Parameter 'input' is null");

			if (lineBuffer->Length > 0)
				lineBuffer->Append (LineSeperator);
			if (input->Length == 0) {
				lineBuffer->Append (" ");
			} else
				lineBuffer->Append (input);
		}

		private static CString8* DequeueLine ()
		{
			Diagnostics.Assert (initialized, "Prompter::DequeueLine(): Prompter is not initialized");

			if (lineBuffer->Length == 0)
				return null;

			int indexOfSeperator = lineBuffer->buffer->IndexOf (LineSeperator);

			if (indexOfSeperator >= 0) {
				CString8* result = lineBuffer->buffer->Substring (0, indexOfSeperator);
				lineBuffer->RemoveAt (0, indexOfSeperator + LineSeperator.Length);
				return result;
			} else // (lineBuffer->Length > 0) but no seperator is present
            {
				CString8* result = lineBuffer->buffer->Substring (0);
				lineBuffer->Clear ();
				return result;
			}
		}

		public static void DisplayCommandList ()
		{
			DisplayCommandList (CommandTable);
		}

		internal static void DisplayCommandList (CommandTableHeader* commandTable)
		{
			Diagnostics.Assert (commandTable != null, "Prompter::DisplayCommandList(CommandTableHeader*): Parameter 'commandTable' is null");

			if (commandTable->firstEntry == null) {
				ADC.TextMode.WriteLine ("No commands to display; the commands list is empty.");
				return;

			} else {
				const int firstColWidth = 22;

				string colALabel = "  NAME";
				string colBLabel = "DESCRIPTION";
				
				TextMode.Write (colALabel);

				for (int spaces = firstColWidth - colALabel.Length;
						spaces > 0;
						spaces--)
					ADC.TextMode.Write (" ");
				
				TextMode.WriteLine (colBLabel);

				CommandTableEntry* currentEntry;
				//HACK: Was originally: for (currentEntry = commandTable->firstEntry;
				for (currentEntry = commandTable->firstEntry;
						currentEntry != null;
						currentEntry = currentEntry->nextEntry) {
					ADC.TextMode.Write ("[");
					ADC.TextMode.Write (currentEntry->name);
					ADC.TextMode.Write ("]");
					
					int spaces = firstColWidth - (currentEntry->name->Length) - 2;
					
					if (spaces < 0)
						spaces = 0;

					for (; spaces > 0; spaces--)
						ADC.TextMode.Write (" ");

					ADC.TextMode.WriteLine (currentEntry->shortDescription);
				}
			}
		}
	}
}