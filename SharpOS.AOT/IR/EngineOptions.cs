// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

namespace SharpOS.AOT.IR {

	/// <summary>
	/// An instance of this class must be passed to the 
	/// <see cref="SharpOS.AOT.IR.Engine" /> constructor.
	/// The contained values defined the options used during
	/// AOT compilation.
	/// </summary>
	public class EngineOptions {
		public string[] Assemblies = null;
		public string OutputFilename = "SharpOS.Kernel.bin";
		public string CPU = "X86";
		public string DumpFile = null;
		public bool TextDump = false;
		public int DumpVerbosity = 1;
		public int Verbosity = 0;
		public bool ConsoleDump = false;
		public string AsmFile = null;
		
		public bool Dump {
			get {
				return DumpFile != null;
			}
		}

		public bool AsmDump {
			get {
				return AsmFile != null;
			}
		}
	}
}