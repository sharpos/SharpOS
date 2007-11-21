// 
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
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
	public class Options: GetOpts.Options {
		public Options(string[] args):
			base(args)
		{ 
		}
		
		public string[] Assemblies = null;
		public bool TempOutput = false;
		public string ImageFilename = null;
		
		[GetOpts.Option("The output file", 'o', "out")]
		public string OutputFilename = null;
		
		[GetOpts.Option("The binary output (when -image is present)", "bin-out")]
		public string BinaryFilename = null;
		
		[GetOpts.Option("Output a bootable image", 'i', "image")]
		public bool CreateImage = false;
		
		[GetOpts.Option("Choose the platform to compile", 'c', "cpu")]
		public string CPU = "X86";
		
		[GetOpts.Option("Verbose mode", 'v', "verbose")]
		public bool Verbose = false;
			
		[GetOpts.Option("Verbosity (0 [silent] - 5)", 'm', "verbosity")]
		public int Verbosity = 0;
			
		[GetOpts.Option("Specify a debug dump file ('-' for console)", 'd', "dump")]
		public string DumpFile = null;
			
		[GetOpts.Option("Send dump to the console (as well as file)", "console-dump")]
		public bool ConsoleDump = false;
			
		[GetOpts.Option("Text-mode dump (default is XML)", "text-dump")]
		public bool TextDump = false;
		
		[GetOpts.Option ("Dump the Assembler Code", "asm-dump")]
		public string AsmFile = null;
			
		[GetOpts.Option("Specify the dump verbosity (1 - 3)", "dump-level")]
		public int DumpVerbosity = 1;

		[GetOpts.Option ("Dump method that contains the defined token", "dump-filter")]
		public string DumpFilter = string.Empty;
		
		[GetOpts.Option (-1, "Specify a resource to add to the output file", 'R', "res")]
		public string [] Resources = new string [0];
	
		public EngineOptions GetEngineOptions()
		{
			EngineOptions eo = new EngineOptions();
			
			eo.Assemblies = Assemblies;
			eo.OutputFilename = BinaryFilename;
			eo.CPU = CPU;
			eo.ConsoleDump = ConsoleDump;
			
			if (DumpFile == "-") {
				eo.DumpFile = null;
				eo.ConsoleDump = true;
			} else
				eo.DumpFile = DumpFile;

			eo.AsmFile = AsmFile;
			eo.TextDump = TextDump;
			eo.DumpVerbosity = DumpVerbosity;
			eo.Verbosity = Verbosity;
			
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
		static void CreateImage (Engine engine, Options opts)
		{
			string tmpScript = System.IO.Path.GetTempFileName();
			string interp = null;
			Stream stm = null;
			
			engine.Message(1, "Creating filesystem image `{0}'...",
				opts.ImageFilename);
			
			if (Environment.OSVersion.Platform == PlatformID.Unix) {
				// Linux/UNIX style system
				engine.Message(1, " - Chose UNIX/Linux filesystem creator");
				
				File.Move(tmpScript, tmpScript + ".sh");
				tmpScript += ".sh";
				interp = "bash";
				stm = System.Reflection.Assembly.GetCallingAssembly().
					GetManifestResourceStream("ImageBuilder.sh");
			} else {
				// Assume Windows for the time being. (UNTESTED)
				engine.Message(1, " - Chose Win32 filesystem creator");
				
				File.Move(tmpScript, tmpScript + ".bat");
				tmpScript += ".bat";
				interp = tmpScript;
				stm = System.Reflection.Assembly.GetCallingAssembly().
					GetManifestResourceStream("ImageBuilder.bat");	
			}
			
			using (StreamReader sr = new StreamReader(stm)) {
				using (StreamWriter sw = new StreamWriter(tmpScript)) {
					sw.Write(sr.ReadToEnd ());
				}
			}
			
			{
				// Write the template image from resources to our output image.
				
				Stream templImg = System.Reflection.Assembly.GetCallingAssembly().
					GetManifestResourceStream("Template.img");
				Stream imageFile = File.Open(opts.ImageFilename, FileMode.Create,
								FileAccess.Write);
				byte[] buffer = new byte[4096];
				int len = 0;
				
				while ((len = templImg.Read(buffer, 0, 4096)) != 0)
					imageFile.Write(buffer, 0, len);
				
				templImg.Close();
				imageFile.Close();
			}
			
			stm.Close ();
			
			// TODO: set executable status before running

			string param;
			if (tmpScript != interp) 
				param = string.Format("\"{0}\" \"{1}\" \"{2}\"",
					tmpScript, opts.BinaryFilename,
					opts.ImageFilename);
			else
				param = string.Format("\"{0}\" \"{1}\"",
					opts.BinaryFilename,
					opts.ImageFilename);

			engine.Message(1, "Start process (" + interp + " " + param + ")");
			Process p = Process.Start(interp, param);

			p.WaitForExit ();

			if (p.ExitCode != 0)
				throw new EngineException (string.Format (
							   "Failed to generate image `{0}'",
							   opts.ImageFilename));

			File.Delete (tmpScript);

			if (opts.TempOutput)
				File.Delete (opts.BinaryFilename);
		}

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

				if (opts.CreateImage)
					suffix = "img";
				
				if (opts.Assemblies [0].EndsWith (".dll"))
					opts.OutputFilename = opts.Assemblies [0].Substring (0,
						opts.Assemblies [0].LastIndexOf ('.'))
						+ "." + suffix;
				else
					opts.OutputFilename = opts.Assemblies [0] + ".bin";
			}

			if (opts.CreateImage) {
				opts.ImageFilename = opts.OutputFilename;
				if (opts.BinaryFilename == null) {
					opts.BinaryFilename = Path.GetTempFileName ();
					opts.TempOutput = true;
				}
			} else
				opts.BinaryFilename = opts.OutputFilename;

#if !BYPASS_CATCH
			try {
#endif
				engine = new Engine (opts.GetEngineOptions ());
				engine.Run ();

				if (opts.CreateImage)
					CreateImage (engine, opts);

#if !BYPASS_CATCH				
			} catch (Exception e) {
				AssemblyDefinition assembly;
				ModuleDefinition module;
				TypeDefinition type;
				MethodDefinition method;

				// Error handling
				
				engine.GetStatusInformation (out assembly, out module, out type, out method);

				if (e is EngineException) {
					Console.Error.WriteLine ("Error: {0}", e.Message);
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
#endif

			return 0;
		}
	}
}
