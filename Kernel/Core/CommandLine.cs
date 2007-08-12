// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SharpOS;
using SharpOS.ADC;
using SharpOS.Foundation;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS
{
	public unsafe class CommandLine
	{
		public static void Setup (Multiboot.Info *mbInfo)
		{
			if ((mbInfo->Flags & (1<<2)) != 0) {
				
				TextMode.Write ("good cmdline: 0x");
				TextMode.WriteNumber ((int)mbInfo->CmdLine, true);
				TextMode.WriteLine ();
				
				commandLine = (byte*) mbInfo->CmdLine;
				TextMode.WriteLine ("getting length");
				length = ByteString.Length (commandLine);
				TextMode.WriteLine ("done");
			} else
				TextMode.WriteLine ("No command line available");
		}

		static byte *commandLine = null;
		static int length = 0;

		public unsafe static byte *Get ()
		{
			return commandLine;
		}

		public unsafe static byte *GetOption (byte *option)
		{
			int optionLen = 0;

			if (commandLine == null)
				return null;
			
			optionLen = ByteString.Length (option);
			
			for (int x = 0; x < length - optionLen; ++x) {
				if (ByteString.Compare (&commandLine [x], option, optionLen) == 0)
					return &commandLine [x];
			}

			return null;
		}
		
		public unsafe static bool IsOptionPresent (byte *option)
		{
			return GetOption (option) != null;
		}

		public static byte *GetArgument (byte *option, int *len)
		{
			int optionLen = 0;
			byte *place = null;

			if (commandLine == null)
				return null;
			
			optionLen = ByteString.Length (option);
			place = GetOption (option);
			
			Kernel.Assert (len != null, Kernel.String (
				"GetArgument (byte *, int *): argument `len' is null"));
			
			for (byte *ptr = GetOption (option) + optionLen; ptr < (commandLine + length); ++ptr) {
				if (*ptr != (byte)' ') {
					bool found = false;
					
					for (byte *px = ptr; px < (commandLine + length); ++px) {
						if (*px == (byte)' ') {
							*len = (int) (px - ptr);
							found = true;
							break;
						}
					}

					if (!found)
						*len = (int)(length - (ptr - commandLine));
					
					return ptr;
				}
			}

			return null;
		}
	}
}

