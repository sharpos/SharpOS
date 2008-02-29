//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS.Kernel.ADC.X86 {
	public static unsafe class Scheduler {
	
		const uint STACK_SIZE		= 8192;

		// Sigh.. we really need to get vtables & memory management working...
		private static IDT.ISRData* ThreadMemory		= null;
		private static IDT.Stack**	ThreadStack			= null;
		private static bool*		ThreadMemoryUsed	= null;

		public static unsafe void Setup ()
		{
			ThreadMemory		= (IDT.ISRData*) MemoryManager.Allocate ((uint) (IDT.ISRData.SizeOf * EntryModule.MaxThreads));
			ThreadMemoryUsed	= (bool*) MemoryManager.Allocate (1 * EntryModule.MaxThreads);
			ThreadStack			= (IDT.Stack**) MemoryManager.Allocate (4 * EntryModule.MaxThreads);

			for (int i = 0; i < EntryModule.MaxThreads; i++)
				ThreadMemoryUsed [i] = false;
		}

		public static unsafe void* CreateThread (uint function_address)
		{
			for (int i = 0; i < EntryModule.MaxThreads; i++) {
				
				if (ThreadMemoryUsed [i] == false) {
					ThreadMemoryUsed [i] = true;

					MemoryUtil.MemSet32 (0, (uint) (void*) &(ThreadMemory [i]), (uint) (sizeof (IDT.ISRData) / 4));


					if (ThreadStack[i] == null)
						ThreadStack[i] = (IDT.Stack*)MemoryManager.Allocate(STACK_SIZE);

					// ... temp code
					ThreadMemory [i].Stack = ThreadStack[i];
					ThreadMemory [i].Stack->FS = GDT.DataSelector;
					ThreadMemory [i].Stack->GS = GDT.DataSelector;
					ThreadMemory [i].Stack->ES = GDT.DataSelector;
					ThreadMemory [i].Stack->DS = GDT.DataSelector;
					ThreadMemory [i].Stack->SS = GDT.DataSelector;
					ThreadMemory [i].Stack->CS = GDT.CodeSelector;
					
					ThreadMemory [i].FrameIP =
					ThreadMemory [i].Stack->EIP = function_address;

					ThreadMemory [i].FrameBP = 0;

					ThreadMemory [i].Stack->UserESP =
					ThreadMemory [i].Stack->EBP = 
					ThreadMemory [i].Stack->ESP = (uint)ThreadStack[i];	// stack pointer

					ThreadMemory [i].Stack->EFlags = 0x00000202;

					// |------| start of stack memory
					// |      |
					// |      |  = ESP
					// | DATA |
					// | DATA |  = BSP
					// | DATA |
					// |------| end of stack memory
					
					return (void*) &ThreadMemory [i];
				}
			}
			return null;
		}
	}
}
