//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	William Lahti <xfurious@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
//	Cédric Rousseau <cedrou@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.Kernel;
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.Memory;
using SharpOS.Korlib.Runtime;
using SharpOS.AOT.Metadata;

namespace SharpOS.Kernel {

	public unsafe class EntryModule {
		#region Global fields

		static bool stayInLoop = true;
		static KernelStage kernelStage = KernelStage.Init;

		// [SharpOS.AOT.Attributes.AddressOf ("SharpOS.Kernel.dll AssemblyRow#0")]
		// static AssemblyRow assemblyRow;

		#endregion
		#region Constants

		/// <summary>
		/// Defines the maximum event handler slots for each event.
		/// </summary>
		public const int MaxEventHandlers = 4;

		/// <summary>
		/// Defines the maximum length of key map names
		/// </summary>
		public const int MaxKeyMapNameLength = 40;

		/// <summary>
		/// Defines the amount of nested TextMode.SaveAttributes() are possible.
		/// </summary>
		public const int MaxTextAttributeSlots = 5;

		/// <summary>
		/// Defines the amount of threads that are possible.
		/// </summary>
		public const uint MaxThreads = 10;

		#endregion
		#region Entry point

		/// <summary>
		/// The kernel entry point. This function is called after static
		/// constructors and initialization are done.
		/// </summary>
		/// <param name="magic">
		/// Magic number of the multiboot loader.
		/// </param>
		/// <param name="pointer">
		/// Pointer to the Multiboot 'info' structure.
		/// </param>
		/// <param name="kernelStart">
		/// The address of the first byte reserved for the kernel.
		/// </param>
		/// <param name="kernelEnd">
		/// The address of the last byte reserved for the kernel.
		/// </param>
		[SharpOS.AOT.Attributes.KernelMain]
		public unsafe static void BootEntry (uint magic, uint pointer, uint kernelStart, uint kernelEnd)
		{
			// Initialize architecture-specific portion of the kernel
			Architecture.Setup ();

			// Set up text mode display
			TextMode.Setup ();

			TextMode.SetAttributes (TextColor.Yellow, TextColor.Black);
			TextMode.ClearScreen ();
			TextMode.SetCursor (0, 0);

			// Write the banner
			DisplayBanner ();

			StageMessage ("Multiboot setup...");			
			if (!Multiboot.Setup (magic, pointer, kernelStart, kernelEnd))
			{
				StageError ("Error: multiboot loader required!");
				return;
			}
			
			kernelStartLoc = (void*)kernelStart;
			kernelEndLoc = (void*)kernelEnd;

			StageMessage ("Commandline setup...");
			CommandLine.Setup ();

			StageMessage ("PageAllocator setup...");
			PageAllocator.Setup (
				Multiboot.KernelAddress, 
				Multiboot.KernelSize,
				Multiboot.UpperMemorySize + 1000);

			StageMessage ("MemoryManager setup...");
			ADC.MemoryManager.Setup ();

      // Must be done after MemoryManager setup.
			StageMessage ("Serial I/O setup...");
			Serial.Setup ();

      //StageMessage("Diagnostic Tool setup...");
      //DiagnosticTool.Server.Setup();

			StageMessage("Scheduler setup...");
			Scheduler.Setup();

			StageMessage ("Device setup...");
			ADC.Architecture.DeviceManager.Setup ();

			StageMessage ("Clock setup...");
			Clock.Setup ();
		
			//StageMessage ("Serial setup...");
			//Serial.Setup ();	// .. is also setup in Architecture?

			StageMessage ("Keymap setup...");
			KeyMap.Setup ();

			StageMessage ("Keyboard setup...");
			Keyboard.Setup ();

			StageMessage ("PCIController setup...");
			PCIController.Setup ();

			//StageMessage("Floppy Disk Controller setup...");
			//FloppyDiskController.Setup();
			
			//StageMessage("Ext2FS FileSystem setup...");
			//SharpOS.Kernel.FileSystem.Ext2FS.Setup();

			StageMessage ("Console setup...");
			SharpOS.Kernel.Console.Setup ();

			TextMode.SaveAttributes ();
			TextMode.SetAttributes (TextColor.LightGreen, TextColor.Black);
			TextMode.WriteLine ("");
			TextMode.WriteLine ("Pinky: What are we gonna do tonight, Brain?");
			TextMode.WriteLine ("The Brain: The same thing we do every night, Pinky - Try to take over the world!");
			TextMode.RestoreAttributes ();

#if KERNEL_TESTS
			// Testcases
			Serial.COM1.WriteLine ("Failed AOT Tests:");
			SharpOS.Kernel.Tests.Wrapper.Run ();
			Serial.COM1.WriteLine ();
			Serial.COM1.WriteLine ("Kernel Tests:");
			MemoryManager.__RunTests ();
			ByteString.__RunTests ();
			StringBuilder.__RunTests ();
			CString8.__RunTests ();
			PString8.__RunTests ();
			InternalSystem.String.__RunTests ();
			Runtime.__RunTests ();
#endif

			//Multiboot.WriteMultibootInfo();

			StageMessage ("Shell setup...");
			SharpOS.Kernel.Shell.Prompter.Setup ();
			SharpOS.Kernel.Shell.Prompter.Start ();

			SetKernelStage (KernelStage.Diagnostics);

			// Infinite loop used to halt the processors
			//FIXME We must know on each processor the current thread runs on. 
			//      Halt all other procs, then halt the current one.
			IProcessor[] procs = Architecture.GetProcessors ();
			int procCount = Architecture.GetProcessorCount();
			while (stayInLoop)
			{
				for (int i=0; i < procCount; i++)
				{
					procs [i].Halt ();
				}
			}
		}

		public static void DisplayBanner ()
		{
			TextMode.SaveAttributes ();
			TextMode.SetAttributes (TextColor.BrightWhite, TextColor.Black);
			TextMode.WriteLine ("SharpOS v0.0.1 Copyright (C) 2007 The SharpOS Team (http://www.sharpos.org/)");
			TextMode.WriteLine ();
			TextMode.RestoreAttributes ();
		}

		static unsafe void StageMessage (string message)
		{
			TextMode.SaveAttributes ();
			TextMode.SetAttributes (TextColor.Green, TextColor.Black);
			TextMode.WriteLine (message);
			TextMode.RestoreAttributes ();
		}

		static unsafe void StageError (string message)
		{
			TextMode.SaveAttributes ();
			TextMode.SetAttributes (TextColor.Red, TextColor.Black);
			TextMode.WriteLine (message);
			TextMode.RestoreAttributes ();
		}

		#endregion
		#region Kernel properties

		static void *kernelStartLoc = null;
		static void *kernelEndLoc = null;

		public unsafe static void GetKernelLocation (out void *start, out void *end)
		{
			start = kernelStartLoc;
			end = kernelEndLoc;
		}

		/// <summary>
		/// Sets the operational stage reported by the kernel.
		/// </summary>
		public unsafe static void SetKernelStage (KernelStage stage)
		{
			kernelStage = stage;
		}

		/// <summary>
		/// Gets the current operational stage of the kernel.
		/// </summary>
		public unsafe static KernelStage GetKernelStage ()
		{
			return kernelStage;
		}

		#endregion
		#region OS boot control

		/// <summary>
		/// Performs shutdown process, then calls ADC halt
		/// function.
		/// </summary>
		public unsafe static void Halt ()
		{
			SetKernelStage (KernelStage.Halt);
			BootControl.Freeze ();
		}

		/// <summary>
		/// Performs shutdown process, then calls ADC reboot
		/// function.
		/// </summary>
		public unsafe static void Reboot ()
		{
			SetKernelStage (KernelStage.Halt);
			BootControl.Reboot ();
		}

		#endregion
	}
}


