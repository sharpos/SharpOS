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
	public static unsafe class ThreadManager {

		public static unsafe void Setup ()
		{
		}

		const uint STACK_SIZE		= 64 * 1024;
		
		internal static unsafe void* CreateStack(uint function_address)
		{
			IDT.Stack*	stack	= (IDT.Stack*) 
				(((byte*)MemoryManager.Allocate(STACK_SIZE)) 
					+ (STACK_SIZE - (uint) sizeof(IDT.Stack)));

			if (stack == null)
				return null;

			// ... temp code
			stack->FS = GDT.DataSelector;
			stack->GS = GDT.DataSelector;
			stack->ES = GDT.DataSelector;
			stack->DS = GDT.DataSelector;
			stack->SS = GDT.DataSelector;
			stack->CS = GDT.CodeSelector;
			
			stack->EIP = function_address;

			stack->UserESP =
			stack->EBP = 
			stack->ESP = (uint) stack;	// stack pointer

			stack->EFlags = 0x00000202;

			// |------| start of stack memory
			// |      |
			// |      |  = ESP
			// | DATA |
			// | DATA |  = EBP
			// | DATA |
			// |------| end of stack memory
			
			return (void*) stack;
		}

		public static SharpOS.Kernel.ADC.Thread CreateThread(uint function_address)
		{
			void* stack = CreateStack(function_address);
			if (stack == null)
				return null;

			SharpOS.Kernel.ADC.Thread newThread = new SharpOS.Kernel.ADC.Thread();
			newThread.StackPointer		= stack;
			newThread.StackAddress		= (uint)stack;
			newThread.FunctionAddress	= function_address;
			return newThread;
		}
	}
}
