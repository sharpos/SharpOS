//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.Kernel;
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.Foundation;

namespace SharpOS.Kernel {
	public unsafe class CommandLine {
		public static void Setup ()
		{
			commandLine = Multiboot.CommandLine;
			if (commandLine == null)
				length = 0;
			else
				length = commandLine->Length;
		}

		static CString8* commandLine = null;
		static int length = 0;

		public unsafe static CString8* GetString ()
		{
			return commandLine;
		}

		public unsafe static int IndexOfOption (string option)
		{
			int index = 0;

			if (commandLine == null)
				return -1;

			while (index < commandLine->Length - option.Length) {
				index = commandLine->IndexOf (index, option);

				if (index == -1)
					return -1;

				if (index + option.Length + 1 < commandLine->Length) {
					if (commandLine->GetChar (index + option.Length) != ' ') {
						++index;
						continue;
					} else {
						return index;
					}
				} else {
					return index;
				}
			}

			return -1;
		}

		public unsafe static bool ContainsOption (string option)
		{
			return IndexOfOption (option) != -1;
		}

		public static bool GetArgument (string option, PString8* buf)
		{
			int argumentIndex;
			int argumentLen;

			Diagnostics.Assert (option != null, "CommandLine.GetArgument(): argument `option' is null");
			Diagnostics.Assert (buf != null, "CommandLine.GetArgument(): argument `buf' is null");

			if (commandLine == null)
				return false;

			argumentIndex = IndexOfArgument (option);

			if (argumentIndex < 0)
				return false;

			argumentLen = GetArgumentLength (argumentIndex);

			buf->Concat (commandLine, argumentIndex, argumentLen);

			return true;
		}

		/// <summary>
		/// Returns the index in the command line string containing the
		/// first character of the argument to <paramref name="option" />.
		///
		/// </summary>
		/// <returns>
		/// If the option was not found, this function returns -2.
		/// If the option exists but it does not have an argument, this
		/// function returns -1. Otherwise, returns the index of the first
		/// character of the option's argument string.
		/// </summary>
		public static int IndexOfArgument (string option)
		{
			int optionIndex;
			int argumentIndex;

			Diagnostics.Assert (option != null, "CommandLine.GetArgument(): argument `option' is null");

			if (commandLine == null)
				return -1;

			optionIndex = IndexOfOption (option);
			argumentIndex = optionIndex + 1;

			if (optionIndex == -1)
				return -2;

			if (optionIndex + 1 >= commandLine->Length)
				return -1;

			while (argumentIndex < commandLine->Length) {
				if (commandLine->GetChar (argumentIndex) == ' ')
					return argumentIndex+1;

				++argumentIndex;
			}

			return -1;
		}

		public static int GetArgumentLength (int argumentIndex)
		{
			int x = argumentIndex;

			Diagnostics.Assert (argumentIndex >= 0 && argumentIndex < commandLine->Length,
				"CommandLine.GetArgumentLength(): argument `argumentIndex' is out of range");

			while (x < commandLine->Length && commandLine->GetChar (x) != ' ')
				++x;

			return x - argumentIndex;
		}
	}
}


