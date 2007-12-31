// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
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
	public unsafe class Architecture {
		/**
			<summary>
				Checks for compatibility with the current system, using 
				the most well-supported method possible. 
			</summary>
		*/
		public static bool CheckCompatibility ()
		{
			return true; // if we're running, we're at least 386.
		}

		public static void Setup ()
		{
			GDT.Setup ();	// Global Descriptor Table
			PIC.Setup ();	// Programmable Interrupt Controller
			IDT.Setup ();	// Interrupt Descriptor table
			RTC.Setup ();	// Real Time Clock
			PIT.Setup ();	// Periodic Interrupt Timer
			Serial.Setup (); // Setup serial I/O
		}

		/**
			<summary>
				Gets the ADC platform identifier.
			</summary>
		*/
		public static string GetCPU ()
		{
			return "x86";
		}

		public static string GetAuthor ()
		{
			return "The SharpOS Team";
		}

		public static string GetLayerName ()
		{
			return "SharpOS.ADC.X86";
		}

		public static int GetProcessorCount ()
		{
			return 0;
		}

		public static Processor* GetProcessors ()
		{
			return null; // TODO
		}

		public static EventRegisterStatus RegisterTimerEvent (uint func)
		{
			return PIT.RegisterTimerEvent (func);
		}

		/// <summary>
		///		Disable interrupts
		/// </summary>
		/// <remarks>
		/// This function should be made "inline" by the AOT
		/// </remarks>
		public static void DisableInterrupts ()
		{
			Asm.CLI ();
		}

		/// <summary>
		///		Enable interrupts
		/// </summary>
		/// <remarks>
		/// This function should be made "inline" by the AOT
		/// </remarks>
		public static void EnableInterrupts ()
		{
			Asm.STI ();
		}
	}
}
