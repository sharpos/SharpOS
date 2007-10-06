// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using SharpOS;
using SharpOS.ADC;
using SharpOS.Foundation;
using SharpOS.Memory;

namespace SharpOS {

	public unsafe class Kernel {
		#region Global fields
		
		static bool stayInLoop = true;
		static KernelStage kernelStage = KernelStage.Init;
		static Multiboot.Info *multibootInfo = null;
		static byte *intermediateStringBuffer = StaticAlloc (MaxMessageLength);
		
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
		/// Defines the maximum allowed length of diagnostic messages
		/// </summary>
		public const int MaxMessageLength = 60;
				
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
			Architecture.Setup();

			// Set up text mode display
			TextMode.Setup();

			TextMode.SetAttributes (TextColor.Yellow, TextColor.Black);
			TextMode.ClearScreen ();
			TextMode.SetCursor (0, 0);
			
			// Write the banner
			TextMode.SaveAttributes();
			TextMode.SetAttributes (TextColor.BrightWhite, TextColor.Black);
			TextMode.WriteLine ("SharpOS v0.0.0.75 (http://sharpos.sourceforge.net/)");
			TextMode.WriteLine ();
			TextMode.RestoreAttributes();

			StageMessage("Multiboot setup.");
			Multiboot.Info* multibootInfo = Multiboot.LoadMultibootInfo(magic, pointer, kernelStart, kernelEnd);
			if (multibootInfo == null)
			{
				StageError("Error: multiboot loader required!");
				return;
			} else
			{
				Multiboot.WriteMultibootInfo(multibootInfo, kernelStart, kernelEnd);
			}

			StageMessage("Commandline setup.");
			CommandLine.Setup(multibootInfo);

			StageMessage("Keymap setup.");
			KeyMap.Setup();
			
			StageMessage("Keyboard setup.");
			Keyboard.Setup();

			//StageMessage("CPU setup.");
			//CPU.Setup ();

			StageMessage("PageAllocator setup.");
			PageAllocator.Setup ((byte*)kernelStart, kernelEnd - kernelStart,
				multibootInfo->MemUpper + 1000);

			StageMessage("MemoryManager setup.");
			//ADC.MemoryManager.Setup();
			//Memory.MemoryManager.Setup ();

			StageMessage("Scheduler setup.");
			Scheduler.Setup();

			//scheduler testing code:
			/*
			// once a thread starts, it seems as if interrupts don't fire anymore, 
			// so the scheduler never gets the chance to schedule the next thread..
			void* testThread1 = Scheduler.CreateThread(Kernel.GetFunctionPointer(KERNEL_TEST1));
			void* testThread2 = Scheduler.CreateThread(Kernel.GetFunctionPointer(KERNEL_TEST2));
			TextMode.WriteLine((testThread1 == null) ? "testThread1 failed" : "testThread1 created");
			TextMode.WriteLine((testThread2 == null) ? "testThread2 failed" : "testThread2 created");
			Scheduler.ScheduleThread(testThread1);
			Scheduler.ScheduleThread(testThread2);
			
			Scheduler.DumpThreads();
			*/
			
			StageMessage("Console setup.");
			SharpOS.Console.Setup();
			
			
			// memory testing code
			/*
			void* data;
			void* data2;
			void* data3;
			data = ADC.MemoryManager.Allocate(100);
			data2 = ADC.MemoryManager.Allocate(200);
			data3 = ADC.MemoryManager.Allocate(400);

			ADC.MemoryManager.Dump();

			ADC.MemoryManager.Free(data);
			ADC.MemoryManager.Free(data3);
			ADC.MemoryManager.Free(data2);

			ADC.MemoryManager.Dump();

			data = ADC.MemoryManager.Allocate(400);
			data2 = ADC.MemoryManager.Allocate(200);
			data3 = ADC.MemoryManager.Allocate(100);
			
			ADC.MemoryManager.Dump();
			*/
			TextMode.SaveAttributes();
			TextMode.SetAttributes(TextColor.LightGreen, TextColor.Black);
			TextMode.WriteLine("");
			TextMode.WriteLine("Pinky: What are we gonna do tonight, Brain?");
			TextMode.WriteLine("The Brain: The same thing we do every night, Pinky - Try to take over the world!");
			TextMode.RestoreAttributes();
			
#if KERNEL_TESTS
			// Testcases
			
			ByteString.Test1 ();
#endif
			
			while (stayInLoop);
		}

		const string KERNEL_TEST1 = "KERNEL_TEST1";		
		[SharpOS.AOT.Attributes.Label(KERNEL_TEST1)]
		static unsafe void TestThread()
		{
			int i = 0;
			while (stayInLoop)
			{
				if (i < 100)
					TextMode.Write("a");
				i++;
			}
		}

		const string KERNEL_TEST2 = "KERNEL_TEST2";
		[SharpOS.AOT.Attributes.Label(KERNEL_TEST2)]
		static unsafe void TestThread2()
		{
			int i = 0;
			while (stayInLoop)
			{
				if (i < 100)
					TextMode.Write("b");
				i++;
			}
		}

		static unsafe void StageMessage(string message)
		{
			TextMode.SaveAttributes();
			TextMode.SetAttributes(TextColor.Green, TextColor.Black);
			TextMode.WriteLine(message);
			TextMode.RestoreAttributes();
		}

		static unsafe void StageError(string message)
		{
			TextMode.SaveAttributes();
			TextMode.SetAttributes(TextColor.Red, TextColor.Black);
			TextMode.WriteLine(message);
			TextMode.RestoreAttributes();
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
			BootControl.Halt ();
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
		#region Diagnostics

		public static void SetErrorTextAttributes ()
		{
			TextMode.SetAttributes (TextColor.Red, TextColor.Black);
		}

		public static void SetWarningTextAttributes ()
		{
			TextMode.SetAttributes (TextColor.Brown, TextColor.Black);
		}
		
		/// <summary>
		/// Induce a kernel panic. Prints the meessage, stage, and error code
		/// then halts the computer.
		/// <summary>
		public static void Panic (string msg, KernelStage stage, KernelError code)
		{
			PString8 *buf = PString8.Wrap (intermediateStringBuffer, Kernel.MaxMessageLength);

			
			TextMode.SetAttributes (TextColor.Red, TextColor.Black);

			buf->Concat ("Panic! -- ");
			buf->Concat (msg);
			buf->ConcatLine ();
			
			buf->Concat ("  Stage: ");
			buf->Concat ((int)stage, false);
			buf->ConcatLine ();
			
			buf->Concat ("  Error: ");
			buf->Concat ((int)code, false);
			buf->ConcatLine ();

			TextMode.SaveAttributes ();
			SetErrorTextAttributes ();
			TextMode.Write (buf);
			TextMode.RestoreAttributes ();
			
			Halt ();
		}
		
		public static void Panic (string msg)
		{
			Panic (msg, KernelStage.Unknown, KernelError.Unknown);
		}
		
		public static void Assert (bool cond, string msg)
		{
			if (!cond)
			{
				TextMode.Write ("Assertion Failed: ");
				TextMode.WriteLine (msg);

				Halt();
			}
		}
		
		public static void AssertFalse (bool cond, string msg)
		{
			Assert (!cond, msg);
		}

		public static void AssertZero (uint err, string msg)
		{
			if (err != 0) {
				TextMode.Write ("Error: ");
				TextMode.Write ((int)err);

				Assert (false, msg);
			}
		}
		
		public static void AssertNonZero (uint err, string msg)
		{
			AssertZero (err == 0 ? 1U : 0U, msg);
		}
		
		public static void Warning (string msg)
		{
			TextMode.SaveAttributes();
			PString8* buf = PString8.Wrap(intermediateStringBuffer, Kernel.MaxMessageLength);

			SetWarningTextAttributes();

			buf->Concat("Warning: ");
			buf->Concat(msg);
			TextMode.WriteLine(buf);
			
			TextMode.RestoreAttributes();
		}
		
		public static void Message (string msg)
		{
			TextMode.WriteLine (msg);
		}
		
		public static void Error (string msg)
		{
			TextMode.SaveAttributes ();
			SetErrorTextAttributes ();
			TextMode.WriteLine (msg);
			TextMode.RestoreAttributes ();
		}

		public static void Error (PString8 *msg)
		{
			TextMode.SaveAttributes ();
			SetErrorTextAttributes ();
			TextMode.WriteLine (msg);
			TextMode.RestoreAttributes ();
		}

		#endregion
		#region Stubs
		
		/// <summary>
		/// Used to statically allocate and initialize a CString8* string.
		/// </summary>
		[SharpOS.AOT.Attributes.String]
		public unsafe static byte *CString (string value)
		{
			return null;
		}

		/// <summary>
		/// Statically allocates a range of bytes.
		/// </summary>
		[SharpOS.AOT.Attributes.Alloc]
		public unsafe static byte* StaticAlloc (uint value)
		{
			return null;
		}

		/// <summary>
		/// Statically allocates a range of bytes and gives it
		/// the specified label.
		/// </summary>
		[SharpOS.AOT.Attributes.LabelledAlloc]
		public unsafe static byte* LabelledAlloc (string label, uint value)
		{
			return null;
		}

		/// <summary>
		/// Gets the function pointer of the given label. This
		/// is a synonym for GetLabelAddress().
		/// </summary>
		[SharpOS.AOT.Attributes.LabelAddress]
		public unsafe static uint GetFunctionPointer (string label)
		{
			return 0;
		}
		
		/// <summary>
		/// Gets the pointer associated with the given label.
		/// </summary>
		[SharpOS.AOT.Attributes.LabelAddress]
		public unsafe static uint GetLabelAddress (string label)
		{
			return 0;
		}
		#endregion
	}
}

