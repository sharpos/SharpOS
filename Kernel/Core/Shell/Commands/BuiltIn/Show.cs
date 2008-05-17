//
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Jonathan Dickinson <jonathand.za@gmail.com>
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
	public unsafe static class Show {
		public const string name = "show";
		public const string shortDescription = "Shows information about SharpOS";
		public const string lblExecute = "COMMANDS.show.Execute";
		public const string lblGetHelp = "COMMANDS.show.GetHelp";

		[Label (lblExecute)]
		public static void Execute (CommandExecutionContext* context)
		{
			if (context->parameters->Compare ("w") == 0)
				ShowWarranty ();
			else if (context->parameters->Compare ("c") == 0)
				ShowCopyright ();
			else if (context->parameters->Compare ("d") == 0)
				ShowDevelopers ();
			else
				GetHelp (context);
		}

		public static void ShowWarranty ()
		{
			TextMode.WriteLine ("The SharpOS Team can accept no liability what-so-ever arising from the usage of this software and/or the source code for this software.");
		}

		public static void ShowCopyright ()
		{
			TextMode.WriteLine ("SharpOS Copyright (C) 2007 The SharpOS Team");
			TextMode.WriteLine ("This software and it's source code is protected by international Copyright Law.");
			TextMode.WriteLine ("By using this software you are agreeing to the terms laid out in the GNU General Public License Version 3 with the Classpath Linking Exception.");
		}

		public static void ShowDevelopers ()
		{
			TextMode.WriteLine ("Developers:");
			TextMode.WriteLine ("xfury - William Lahti - xfurious /at/ gmail /dot/ com");
			TextMode.WriteLine ("illuminus - Bruce Markham - illuminus86 /at/ gmail /dot/ com");
			TextMode.WriteLine ("darxkies - Mircea-Cristian Racasan - darx_kies /at/ gmx /dot/ net");
			TextMode.WriteLine ("moitoius - Jonathan Dickinson - jonathand /dot/ za /at/ gmail /dot/ com");
			TextMode.WriteLine ("Logical_Error - Sander van Rossen - sander/dot/ vanrossen /at/ gmail /dot/ com");
			TextMode.WriteLine ("tgiphil - Phil Garcia - phil /at/ thinkedge /dot/ com");
			TextMode.WriteLine ("lsmithmier");
			TextMode.WriteLine ("jmacdonagh");
			TextMode.WriteLine ("msieradzki");
			TextMode.WriteLine ("asgeirh");
			TextMode.WriteLine ("darkdonno");
		}

		[Label (lblGetHelp)]
		public static void GetHelp (CommandExecutionContext* context)
		{
			TextMode.WriteLine ("Syntax: ");
			TextMode.WriteLine ("     show w : shows the warranty.");
			TextMode.WriteLine ("     show c : shows the copyright.");
			TextMode.WriteLine ("     show d : shows the developers.");
			TextMode.WriteLine (CommandTableHeader.inform_USE_HELP_COMMANDS);
		}

		public static CommandTableEntry* CREATE ()
		{
			CommandTableEntry* entry = (CommandTableEntry*) SharpOS.Kernel.ADC.MemoryManager.Allocate ((uint) sizeof (CommandTableEntry));

			entry->name = (CString8*) SharpOS.Kernel.Stubs.CString (name);
			entry->shortDescription = (CString8*) SharpOS.Kernel.Stubs.CString (shortDescription);
			entry->func_Execute = (void*) SharpOS.Kernel.Stubs.GetLabelAddress (lblExecute);
			entry->func_GetHelp = (void*) SharpOS.Kernel.Stubs.GetLabelAddress (lblGetHelp);

			return entry;
		}
	}
}
