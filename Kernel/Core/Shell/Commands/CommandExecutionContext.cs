// 
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
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
using SharpOS.Kernel.Foundation;
using System.Runtime.InteropServices;

namespace SharpOS.Kernel.Shell.Commands {
	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct CommandExecutionContext {
		public CString8* parameters;

		/// <summary>
		/// Allocates and returns an uninitialized CommandExecutionContext
		/// </summary>
		/// <param name="originalCommandLine"></param>
		/// <returns>A newly allocated CommandExecutionContext</returns>
		public static CommandExecutionContext* CREATE ()
		{
			CommandExecutionContext* result = (CommandExecutionContext*) ADC.MemoryManager.Allocate ((uint) sizeof (CommandExecutionContext));

			return result;
		}

		public static void DISPOSE (CommandExecutionContext* instance)
		{
			ADC.MemoryManager.Free ((void*) instance);
		}
	}
}
