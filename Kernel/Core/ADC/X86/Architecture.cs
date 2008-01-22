//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.Kernel;
using SharpOS.AOT;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using ADC = SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.ADC.X86 {

	// ..rename to enviroment?
	public unsafe class Architecture {

		public static void Setup ()
		{
			GDT.Setup ();	// Global Descriptor Table
			PIC.Setup ();	// Programmable Interrupt Controller
			IDT.Setup ();	// Interrupt Descriptor table
			PIT.Setup ();	// Periodic Interrupt Timer
			Serial.Setup (); // Setup serial I/O			
		}

		/**
			<summary>
				Checks for compatibility with the current system, using 
				the most well-supported method possible. 
			</summary>
		*/
		public static bool CheckCompatibility()
		{
			return true; // if we're running, we're at least 386.
		}

		/**
			<summary>
				Gets the ADC platform identifier.
			</summary>
		*/
		public static string GetPlatform()
		{
			return "X86";
		}

		public static string GetAuthor ()
		{
			return "The SharpOS Team";
		}

		public static string GetLayerName ()
		{
			return "SharpOS.ADC.X86";
		}
		
		// must do it here because memory management doesn't work yet in Setup... :(
		private static void InitializeProcessor()
		{
			processors = new IProcessor[1];
			for (int i = 0; i < processors.Length; i++)
			{
				processors[i] = new Processor();
				processors[i].Setup();
			}
		}

		public static int GetProcessorCount ()
		{
			if (processors == null)
				InitializeProcessor();

			return processors.Length;
		}

		static private IProcessor[] processors = null;
		public static IProcessor[] GetProcessors ()
		{
			if (processors == null)
				InitializeProcessor();

			return processors; 
		}
	}
}
