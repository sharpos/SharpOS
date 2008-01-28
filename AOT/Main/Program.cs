//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using SharpOS;
using SharpOS.AOT.IR;
using SharpOS.AOT.X86;
using GetOpts = Mono.GetOptions;
using Mono.Cecil;

namespace SharpOS.AOT {
	public class Options : GetOpts.Options {
		public Options (string [] args)
			:
			base (args)
		{
		}

		public string [] Assemblies = null;
		public bool TempOutput = false;
		public string ImageFilename = null;

		[GetOpts.Option ("The output file", 'o', "out")]
		public string OutputFilename = null;

		[GetOpts.Option ("The binary output (when -image is present)", "bin-out")]
		public string BinaryFilename = null;

		[GetOpts.Option ("Choose the platform to compile", 'c', "cpu")]
		public string CPU = "X86";

		[GetOpts.Option ("Verbose mode", 'v', "verbose")]
		public bool Verbose = false;

		[GetOpts.Option ("Verbosity (0 [silent] - 5)", 'm', "verbosity")]
		public int Verbosity = 0;

		[GetOpts.Option ("Specify a debug dump file ('-' for console)", 'd', "dump")]
		public string DumpFile = null;

		[GetOpts.Option ("Send dump to the console (as well as file)", "console-dump")]
		public bool ConsoleDump = false;

		[GetOpts.Option ("Text-mode dump (default is XML)", "text-dump")]
		public bool TextDump = false;

		[GetOpts.Option ("Dump the Assembler Code", "asm-dump")]
		public string AsmFile = null;

		[GetOpts.Option ("Specify the dump verbosity (1 - 3)", "dump-level")]
		public int DumpVerbosity = 1;

		[GetOpts.Option ("Dump method that contains the defined token", "dump-filter")]
		public string DumpFilter = string.Empty;

		[GetOpts.Option (-1, "Specify a resource to add to the output file", 'R', "res")]
		public string [] Resources = new string [0];

		[GetOpts.Option ("Disable metadata encoding", "no-metadata")]
		public bool NoMetadata = false;

		[GetOpts.Option ("Force recompile, even if all files are up to date", "force")]
		public bool ForceRecompile = false;

		public EngineOptions GetEngineOptions ()
		{
			EngineOptions eo = new EngineOptions ();

			eo.Assemblies = Assemblies;
			eo.OutputFilename = BinaryFilename;
			eo.CPU = CPU;
			eo.ConsoleDump = ConsoleDump;

			if (DumpFile == "-") {
				eo.DumpFile = null;
				eo.ConsoleDump = true;
			} else
				eo.DumpFile = DumpFile;

			eo.NoMetadata = NoMetadata;
			eo.AsmFile = AsmFile;
			eo.TextDump = TextDump;
			eo.DumpVerbosity = DumpVerbosity;
			eo.Verbosity = Verbosity;
			eo.ForceRecompile = ForceRecompile;

			if (Verbosity == 0 && Verbose)
				eo.Verbosity = 1;

			eo.DumpFilter = DumpFilter;

			foreach (string res in Resources) {
				byte [] contents;
				byte [] buffer = new byte [1024];
				int read = 0;
				int x = 0;
				Stream fs = null;
				string key, value;

				if (!res.Contains (":"))
					throw new Exception ("Invalid resource: '" +
						res + "': format is 'name:file'");

				key = res.Substring (0, res.IndexOf (':'));
				value = res.Substring (key.Length);
				fs = File.Open (value, FileMode.Open, FileAccess.Read);
				contents = new byte [fs.Length];

				while ((read = fs.Read (buffer, 0, buffer.Length)) != 0) {
					Array.Copy (buffer, 0, contents, x, read);
					x += read;
				}

				fs.Close ();

				eo.Resources.Add (key, contents);
			}

			return eo;
		}
	}

	public class Compiler {
		static int Main (string [] args)
		{
			Options opts = new Options (args);
			Engine engine = null;

			opts.ShowBanner ();

			if (opts.RemainingArguments.Length == 0) {
				Console.WriteLine ("Error: too few arguments");

				return 1;
			} else {
				bool stop = false;
				List<string> assemFiles = new List<string> ();

				foreach (string argStr in opts.RemainingArguments) {
					if (!File.Exists (argStr)) {
						Console.Error.WriteLine ("{0}: File not found",
									 argStr);
						stop = true;
					}

					assemFiles.Add (argStr);
				}

				if (stop)
					return 1;

				opts.Assemblies = assemFiles.ToArray ();
			}

			if (opts.OutputFilename == null) {
				string suffix = "bin";

				if (opts.Assemblies [0].EndsWith (".dll"))
					opts.OutputFilename = opts.Assemblies [0].Substring (0,
						opts.Assemblies [0].LastIndexOf ('.'))
						+ "." + suffix;
				else
					opts.OutputFilename = opts.Assemblies [0] + ".bin";
			}

			opts.BinaryFilename = opts.OutputFilename;

			bool debuggerAttached = false;

			// Prevent a Mono error if one occurs.
			try {
				debuggerAttached = System.Diagnostics.Debugger.IsAttached;
			} catch {
			}

			if (debuggerAttached) {
				engine = new Engine (opts.GetEngineOptions ());
				engine.Run ();
			} else
				try {
					engine = new Engine (opts.GetEngineOptions ());
					engine.Run ();
				} catch (Exception e) {
					AssemblyDefinition assembly;
					ModuleDefinition module;
					TypeReference type;
					MethodReference method;

					// Error handling

					engine.GetStatusInformation (out assembly, out module, out type, out method);

					if (e is EngineException) {
						Console.Error.WriteLine ("Error: {0}", e.ToString ());
					} else {
						Console.Error.WriteLine ("Caught exception: " + e);
					}

					switch (engine.CurrentStatus) {
					case Engine.Status.AssemblyLoading:
						Console.Error.WriteLine ("While loading assembly `{0}'", engine.ProcessingAssemblyFile);
						break;

					case Engine.Status.IRProcessing:
						Console.Error.WriteLine ();
						Console.Error.WriteLine ("While performing IR processing");
						break;

					case Engine.Status.IRGeneration:
						Console.Error.WriteLine ("While generating IR code for assembly `{0}'",
									engine.ProcessingAssemblyFile);
						break;

					case Engine.Status.Encoding:
						Console.Error.WriteLine ("While encoding the output assembly.");
						break;
					}

					if (method != null) {
						Console.Error.WriteLine ("Method:  {0}", (method == null ? "?" : method.ToString ()));
						Console.Error.WriteLine ("  in module:  {0}", (module == null ? "?" : module.ToString ()));
						Console.Error.WriteLine ("  of assembly:  {0}", (assembly == null ? "?" :
							assembly.ToString ()));
						Console.Error.WriteLine ("  loaded from:  {0}", (engine.ProcessingAssemblyFile == null ?
							"?" : engine.ProcessingAssemblyFile));
						Console.Error.WriteLine ();
					} else {
						Console.Error.WriteLine ("* Status information is not available.");
					}

					return 1;
				}

			return 0;
		}
	}
}
