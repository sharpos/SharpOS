//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	William Lahti <xfurious@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
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

namespace SharpOS.Kernel {

	public unsafe class EntryModule {
		#region Global fields

		static bool stayInLoop = true;
		static KernelStage kernelStage = KernelStage.Init;
		static Multiboot.Info* multibootInfo = null;

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
			Multiboot.Info* multibootInfo = Multiboot.LoadMultibootInfo (magic, pointer, kernelStart, kernelEnd);
			if (multibootInfo == null) {
				StageError ("Error: multiboot loader required!");
				return;
			}

			StageMessage ("Commandline setup...");
			CommandLine.Setup (multibootInfo);

			StageMessage ("PageAllocator setup...");
			PageAllocator.Setup ((byte*) kernelStart, kernelEnd - kernelStart,
				multibootInfo->MemUpper + 1000);

			StageMessage ("MemoryManager setup...");
			ADC.MemoryManager.Setup ();

			StageMessage ("Clock setup...");
			Clock.Setup ();

			StageMessage ("PCIController setup...");
			PCIController.Setup ();

			StageMessage ("Keymap setup...");
			KeyMap.Setup ();

			StageMessage ("Keyboard setup...");
			Keyboard.Setup ();

			//StageMessage("Floppy Disk Controller setup...");
			//FloppyDiskController.Setup();

			StageMessage("Scheduler setup...");
			Scheduler.Setup();

			StageMessage ("Serial setup...");
			Serial.Setup ();

			StageMessage ("Console setup...");
			SharpOS.Kernel.Console.Setup ();

			//StageMessage("Ext2FS FileSystem setup...");
			//SharpOS.Kernel.FileSystem.Ext2FS.Setup();

			TextMode.SaveAttributes();
			TextMode.SetAttributes(TextColor.LightGreen, TextColor.Black);
			TextMode.WriteLine("");
			TextMode.WriteLine("Pinky: What are we gonna do tonight, Brain?");
			TextMode.WriteLine("The Brain: The same thing we do every night, Pinky - Try to take over the world!");
			TextMode.RestoreAttributes();

#if KERNEL_TESTS
			// Testcases
			TextMode.WriteLine ("Run tests");
			ByteString.__RunTests ();
			StringBuilder.__RunTests ();
			CString8.__RunTests ();
			PString8.__RunTests ();
			SharpOS.Kernel.Tests.Wrapper.Run ();
			InternalSystem.String.__RunTests ();
#endif

			StageMessage ("Shell setup...");
			SharpOS.Kernel.Shell.Prompter.Setup ();
			SharpOS.Kernel.Shell.Prompter.Start ();

			SetKernelStage (KernelStage.Diagnostics);

			while (stayInLoop);
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
		#region Object Support
		[SharpOS.AOT.Attributes.AllocObject]
		internal static unsafe InternalSystem.Object AllocObject (VTable vtable)
		{
			// TODO add GC support here

			/*TextMode.Write ("Alloc Object of Size: ");
			TextMode.Write ((int) vtable.Size);
			TextMode.Write (" Type: ");
			TextMode.Write (vtable.Type.Name);
			TextMode.WriteLine ();*/


			void* result = (void*) SharpOS.Kernel.ADC.MemoryManager.Allocate (vtable.Size);

			InternalSystem.Object _object = Stubs.GetObjectFromPointer (result);
			_object.VTable = vtable;

			return _object;
		}

		[SharpOS.AOT.Attributes.AllocArray]
		internal static unsafe InternalSystem.Object AllocArray (VTable vtable, int size)
		{
			// TODO add GC support here

			/*TextMode.Write ("Alloc Object of Size: ");
			TextMode.Write ((int) size);
			TextMode.Write (" Type: ");
			TextMode.Write (vtable.Type.Name);
			TextMode.WriteLine ();*/

			void* result = (void*) SharpOS.Kernel.ADC.MemoryManager.Allocate ((uint) size);

			InternalSystem.Object _object = Stubs.GetObjectFromPointer (result);
			_object.VTable = vtable;

			// TODO set the rank, rank data and initialize the data
			
			/*InternalSystem.Array _array = _object as InternalSystem.Array;
			_array.Rank = 1;
			_array.FirstEntry.LowerBound = 0;
			_array.FirstEntry.Length = count;*/

			return _object;
		}
		#endregion
	}
}


