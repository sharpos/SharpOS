//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Cédric Rousseau <cedrou@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Windows.Forms;
using Mono.GetOptions;

namespace SharpOS.Tools.DiagnosticTool {

	public class ProgramOptions : Options {
		public ProgramOptions (string[] args):
			base (args)
		{
		}

		[Option ("Save pipe filename to this file", 's', "save-filename")]
		public string SaveFilename = null;
	}

	public static class Program
	{
		public static ProgramOptions Options;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main (string [] args)
		{
			Options = new ProgramOptions (args);
			Application.EnableVisualStyles ();
			Application.SetCompatibleTextRenderingDefault (false);
			Application.Run (new MainWindow ());
		}
	}
}
