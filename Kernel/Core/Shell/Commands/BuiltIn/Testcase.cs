//
// (C) 2008 The SharpOS Project.
//
// Authors:
//	William Lahti <xfurious@gmail.com>

using SharpOS.AOT.Attributes;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.Shell.Commands.BuiltIn {
	public unsafe static class Testcase {
		private const string name = "testcase";
		private const string shortDescription = "Prints test cases which have run";
		private const string lblExecute = "COMMANDS.testcase.Execute";
		private const string lblGetHelp = "COMMANDS.testcase.GetHelp";

		[Label (lblExecute)]
		public static void Execute (CommandExecutionContext* context)
		{
			bool onlyFail = false;
			bool onlyPass = false;
			bool showTest = false;
			CString8 *source = null;
			//int sourceColumn = 32;
			int idColumn = 6;
			int id = 0;

			SharpOS.Kernel.Foundation.Testcase.TestRecord *rec = null;

			rec = SharpOS.Kernel.Foundation.Testcase.GetFirstTest ();

			if (context->parameters->Compare ("--fail") == 0) {
				onlyFail = true;
			} else if (context->parameters->Compare ("--pass") == 0) {
				onlyPass = true;
			} else if (context->parameters->Compare ("--source ", 0, 9) == 0) {
				source = context->parameters->Substring (9);
			} else if (context->parameters->Length > 0) {
				showTest = true;
				id = Convert.ToInt32 (context->parameters);

				if (id < 0 || id > SharpOS.Kernel.Foundation.Testcase.GetTestCount ()) {
					Diagnostics.Error ("testcase: no such test");
					return;
				}
			}

			while ((rec = SharpOS.Kernel.Foundation.Testcase.GetTest (id)) != null) {
				TextMode.SaveAttributes ();

				if (onlyFail && rec->Result != false) {
					++id;
					continue;
				}

				if (onlyPass && rec->Result == false) {
					++id;
					continue;
				}

				if (source != null && rec->Source->Compare (source) != 0) {
					++id;
					continue;
				}

				if (showTest) {
					TextMode.Write ("Test #");
					TextMode.Write (id);
					TextMode.Write (": ");
					TextMode.WriteLine (rec->Name);

					TextMode.Write ("  Source : ");
					TextMode.WriteLine (rec->Source);
					TextMode.Write ("  Result : ");
					TextMode.SaveAttributes ();
					if (rec->Result) {
						TextMode.Foreground = TextColor.Green;
						TextMode.Write ("PASS");
					} else {
						TextMode.Foreground = TextColor.Red;
						TextMode.Write ("FAIL");
					}
					TextMode.RestoreAttributes ();
					TextMode.WriteLine ();


				}

				TextMode.Write ("#");
				TextMode.Write (id);
				TextMode.Write (" ");

				TextMode.CursorLeft = idColumn;

				if (rec->Result) {
					TextMode.Foreground = TextColor.Green;
					TextMode.Write ("PASS : ");
				} else {
					TextMode.Foreground = TextColor.Red;
					TextMode.Write ("FAIL : ");
				}

				TextMode.RestoreAttributes ();

				TextMode.Write (rec->Name);
				TextMode.WriteLine ();

				if (showTest)
					break;

				++id;
			}

			if (source != null)
				MemoryManager.Free (source);
		}

		[Label (lblGetHelp)]
		public static void GetHelp (CommandExecutionContext* context)
		{
			TextMode.WriteLine ("Usage: testcase [ID|--fail|--pass|--source SOURCE]");
			TextMode.WriteLine ("Prints kernel testcases which have been run.");
		}

		public static CommandTableEntry* CREATE ()
		{
			CommandTableEntry* entry = (CommandTableEntry*) MemoryManager.Allocate (
				(uint) sizeof (CommandTableEntry));

			entry->name = (CString8*) SharpOS.Kernel.Stubs.CString (name);
			entry->shortDescription = (CString8*) SharpOS.Kernel.Stubs.CString (shortDescription);
			entry->func_Execute = (void*) SharpOS.Kernel.Stubs.GetLabelAddress (lblExecute);
			entry->func_GetHelp = (void*) SharpOS.Kernel.Stubs.GetLabelAddress (lblGetHelp);

			return entry;
		}
	}
}
