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
		private static IDT.ISRData*	ThreadMemory		= (IDT.ISRData*)Kernel.StaticAlloc((uint)(/*sizeof(IDT.ISRData)*/(19 * 4) * Kernel.MaxThreads));
		private static bool*		ThreadMemoryUsed	= (bool*)Kernel.StaticAlloc(1 * Kernel.MaxThreads);

		public static unsafe void Setup()
		{
			for (int i = 0; i < Kernel.MaxThreads; i++)
				ThreadMemoryUsed[i] = false;
		}

		public static unsafe void* CreateThread(uint function_address)
		{
			Asm.CLI();
			for (int i = 0; i < Kernel.MaxThreads; i++)
			{
				if (ThreadMemoryUsed[i] == false)
				{
					ThreadMemoryUsed[i] = true;

					Memory.MemSet32(0, (uint)(void*)&(ThreadMemory[i]), (uint)(sizeof(IDT.ISRData) / 4));

					// ... temp code
					ushort ds, cs, es, fs, gs, ss;
					Asm.MOV(R16.AX, Seg.DS); Asm.MOV(&ds, R16.AX);
					Asm.MOV(R16.AX, Seg.CS); Asm.MOV(&cs, R16.AX);
					Asm.MOV(R16.AX, Seg.ES); Asm.MOV(&es, R16.AX);
					Asm.MOV(R16.AX, Seg.FS); Asm.MOV(&fs, R16.AX);
					Asm.MOV(R16.AX, Seg.GS); Asm.MOV(&gs, R16.AX);
					Asm.MOV(R16.AX, Seg.SS); Asm.MOV(&ss, R16.AX);

					ThreadMemory[i].FS = (uint)fs;
					ThreadMemory[i].GS = (uint)gs;
					ThreadMemory[i].ES = (uint)es;
					ThreadMemory[i].DS = (uint)ds;
					ThreadMemory[i].CS = (uint)cs;
					ThreadMemory[i].SS = (uint)ss;

					ThreadMemory[i].EFlags	= 0x0200;
					ThreadMemory[i].EIP		= function_address;

					Asm.STI();
					return (void*)&ThreadMemory[i];
				}
			}
			Asm.STI();
			return null;
		}
	}
}
