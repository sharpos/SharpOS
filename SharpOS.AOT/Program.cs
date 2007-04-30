// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using SharpOS;
using SharpOS.AOT.IR;
using SharpOS.AOT.X86;
using GetOpts = Mono.GetOptions;

namespace SharpOS.AOT {
	public class Options: GetOpts.Options {
		public Options(string[] args):
			base(args)
		{ }
		
		public string[] Assemblies = null;
		public bool TempOutput = false;
		public string BinaryFilename = null;
		public string ImageFilename = null;
		
		[GetOpts.Option("Specify the output file", 'o', "out")]
			public string OutputFilename = null;
		
		[GetOpts.Option("Output a bootable image", 'i', "image")]
			public bool CreateImage = false;
		
		[GetOpts.Option("Choose the platform to compile", 'c', "cpu")]
			public string CPU = "X86";
		
		[GetOpts.Option("Verbose mode", 'v', "verbose")]
			public bool Verbose = false;
			
		[GetOpts.Option("Dump mode (super-verbose output)", 'd', "dump")]
			public string DumpFile = null;
			
		[GetOpts.Option("Text-mode dump (default is XML)", "text-dump")]
			public bool TextDump = false;
			
		[GetOpts.Option("Specify the dump verbosity (1 - 3)", "dump-level")]
			public int DumpVerbosity = 1;
		
		public EngineOptions GetEngineOptions()
		{
			EngineOptions eo = new EngineOptions();
			
			eo.Assemblies = Assemblies;
			eo.OutputFilename = OutputFilename;
			eo.CPU = CPU;
			eo.Verbose = Verbose;
			eo.DumpFile = DumpFile;
			eo.TextDump = TextDump;
			eo.DumpVerbosity = DumpVerbosity;
			return eo;
		}
	}
	
	public class Compiler {
		static int Main (string[] args) 
		{
			Options opts = new Options(args);
			Engine engine = null;
			
			opts.ShowBanner();
			
			if (opts.RemainingArguments.Length == 0) {
				Console.WriteLine("Error: too few arguments");
				
				return 1;
			} else {
				bool stop = false;
				List<string> assemFiles = new List<string>();
				
				foreach (string argStr in opts.RemainingArguments) {
					if (!File.Exists(argStr)) {
						Console.Error.WriteLine("{0}: File not found");
						stop = true;
					}
					
					assemFiles.Add(argStr);
				}
				
				if (stop)
					return 1;
					
				opts.Assemblies = assemFiles.ToArray();
			}
			
			if (opts.OutputFilename == null) {
				if (opts.Assemblies[0].EndsWith(".dll"))
					opts.OutputFilename = opts.Assemblies[0].Substring(0,
							opts.Assemblies[0].LastIndexOf('.')) + ".bin";
				else
					opts.OutputFilename = opts.Assemblies[0] + ".bin";
			}
			
			opts.BinaryFilename = opts.OutputFilename;
			
			engine = new Engine(opts.GetEngineOptions());
			engine.Run ();
			
			return 0;
		}
	}
}
