//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS.ADC.X86
{
	public static unsafe class Scheduler
	{
		// Sigh.. we really need to get vtables & memory management working...
		private static IDT.ISRData*	ThreadMemory		= (IDT.ISRData*)Kernel.StaticAlloc((uint)(sizeof(IDT.ISRData) * Kernel.MaxThreads));
		private static bool*		ThreadMemoryUsed	= (bool*)Kernel.StaticAlloc(sizeof(bool) * Kernel.MaxThreads);

		public static unsafe void Setup()
		{
			for (int i = 0; i < Kernel.MaxThreads; i++)
				ThreadMemoryUsed[i] = false;
		}

		public static unsafe void* CreateThread(uint address)
		{
			//UNTESTED! does this work?
			Asm.CLI();
			for (int i = 0; i < Kernel.MaxThreads; i++)
			{
				if (ThreadMemoryUsed[i] == false)
				{
					ThreadMemoryUsed[i] = true;

					// memset has not been tested yet:
					Memory.MemSet32(0, (uint)(void*)&(ThreadMemory[i]), (uint)(sizeof(IDT.ISRData) / 4));
					
					ThreadMemory[i].CS = address;	// ...would this work?
					// ... data segment needs to be set etc.

					Asm.STI();
					return (void*)&ThreadMemory[i];
				}
			}
			Asm.STI();
			return null;
		}
	}
}
