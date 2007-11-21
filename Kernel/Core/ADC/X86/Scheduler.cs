//
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
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

namespace SharpOS.ADC.X86
{
	public static unsafe class Scheduler
	{
		// Sigh.. we really need to get vtables & memory management working...
		private static IDT.ISRData*	ThreadMemory		= (IDT.ISRData*)Kernel.StaticAlloc((uint)(/*sizeof(IDT.ISRData)*/(19 * 4) * Kernel.MaxThreads));
		private static bool*		ThreadMemoryUsed	= (bool*)Kernel.StaticAlloc(1 * Kernel.MaxThreads);

		public static unsafe void Setup()
		{
			for (int i = 0; i < Kernel.MaxThreads; i++)
				ThreadMemoryUsed[i] = false;
		}

		public static unsafe void* CreateThread(uint function_address)
		{
			for (int i = 0; i < Kernel.MaxThreads; i++)
			{
				if (ThreadMemoryUsed[i] == false)
				{
					ThreadMemoryUsed[i] = true;

					Memory.MemSet32(0, (uint)(void*)&(ThreadMemory[i]), (uint)(sizeof(IDT.ISRData) / 4));

					// ... temp code
					ThreadMemory[i].FS		= GDT.DataSelector;
					ThreadMemory[i].GS		= GDT.DataSelector;
					ThreadMemory[i].ES		= GDT.DataSelector;
					ThreadMemory[i].DS		= GDT.DataSelector;
					ThreadMemory[i].SS		= GDT.DataSelector;
					ThreadMemory[i].CS		= GDT.CodeSelector;
					ThreadMemory[i].EIP		= function_address;

					ThreadMemory[i].EFlags	= 0x00000202;
					// set up stack etc.
					//ThreadMemory[i].SS	= (uint)ss;				// stack segment
					//ThreadMemory[i].ESP	= stack + stacksize;	// stack pointer
					//ThreadMemory[i].EBP	= ...;					// no need to set?

					//ThreadMemory[i].CS	= (uint)cs;				// code segment
					//ThreadMemory[i].EIP	= function_address;		// instruction pointer
					
					// |------| start of stack memory
					// |      |
					// |      |  = ESP
					// | DATA |
					// | DATA |  = BSP
					// | DATA |
					// |------| end of stack memory

					return (void*)&ThreadMemory[i];
				}
			}
			return null;
		}
	}
}
