// 
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Cedric Rousseau <cedrou@gmail.com>
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

namespace SharpOS.Kernel.Shell.Commands.BuiltIn {
	public unsafe static class MemView {
		public const string name = "memview";
		public const string shortDescription = "Display memory";
		public const string lblExecute = "COMMANDS.memview.Execute";
		public const string lblGetHelp = "COMMANDS.memview.GetHelp";

		private static bool IsHexDigit (char c)
		{
			return (c >= '0' && c <= '9')
					|| (c >= 'A' && c <= 'F')
					|| (c >= 'a' && c <= 'f');
		}

		[Label (lblExecute)]
		public static void Execute (CommandExecutionContext* context)
		{
			byte* buffer = context->parameters->Pointer;
			int len = context->parameters->Length;

			// scan parameter and convert string to hex int
			int start = 0;
			while (start < len && !IsHexDigit ((char)buffer [start]))
				start++;
			int end = start;
			while (end < len && IsHexDigit ((char)buffer [end]))
				end++;
			len = end - start;

			int offset = 0;
			for (int index = start; index < end; index++)
			{
				offset <<= 4;

				byte current = 0;
				char digit = (char)buffer [index];

				if (digit >= '0' && digit <= '9') {
					current = (byte)(digit - '0');
				}
				else if (digit >= 'A' && digit <= 'F') {
					current = (byte)(10 + digit - 'A');
				}
				else if (digit >= 'a' && digit <= 'f') {
					current = (byte)(10 + digit - 'a');
				}
				offset |= current;
			}

			TextMode.Write(Diagnostics.FormatDump ((byte*)offset, 256, 16));
		}

		[Label (lblGetHelp)]
		public static void GetHelp (CommandExecutionContext* context)
		{
			TextMode.WriteLine ("Syntax: ");
			TextMode.WriteLine ("     memview <hex-address>");
			TextMode.WriteLine ("");
			TextMode.WriteLine ("Displays 256 bytes of memory at the requested address.");
		}

		public static CommandTableEntry* CREATE ()
		{
			CommandTableEntry* entry = (CommandTableEntry*)SharpOS.Kernel.ADC.MemoryManager.Allocate ((uint)sizeof (CommandTableEntry));

			entry->name = (CString8*)SharpOS.Kernel.Stubs.CString (name);
			entry->shortDescription = (CString8*)SharpOS.Kernel.Stubs.CString (shortDescription);
			entry->func_Execute = (void*)SharpOS.Kernel.Stubs.GetLabelAddress (lblExecute);
			entry->func_GetHelp = (void*)SharpOS.Kernel.Stubs.GetLabelAddress (lblGetHelp);
			entry->nextEntry = null;

			return entry;
		}
	}
}
