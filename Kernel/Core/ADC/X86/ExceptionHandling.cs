//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using SharpOS.Korlib.Runtime;

namespace SharpOS.Kernel.ADC.X86 {
	public static class ExceptionHandling	{
		private const string GET_IP = "GET_IP";
		private const string DIVIDE_ERROR = "DIVIDE_ERROR";

		public static void Setup ()
		{
			IDT.RegisterIRQ (IDT.Interrupt.DivideError, Stubs.GetFunctionPointer (DIVIDE_ERROR));
		}

		[SharpOS.AOT.Attributes.Label (DIVIDE_ERROR)]
		static unsafe void KeyboardHandler (IDT.ISRData data)
		{
			throw new System.DivideByZeroException ();
		}

		internal unsafe static StackFrame [] GetCallingStack ()
		{
			uint bp, ip, count = 0;
			StackFrame [] stackFrame = null;

			// 1st step: count the stack frames
			// 2nd step: create the array and find the method boundary for every stack frame
			for (int step = 0; step < 2; step++) {
				// Get the current IP
				Asm.CALL (GET_IP);
				Asm.LABEL (GET_IP);
				Asm.POP (R32.EAX);
				Asm.MOV (&ip, R32.EAX);

				// Get the current BP
				Asm.MOV (&bp, R32.EBP);

				if (step == 1)
					stackFrame = new StackFrame [count];

				count = 0;

				do {
					if (step == 1) {
						MethodBoundary entry = null;

						for (int i = 0; i < Runtime.MethodBoundaries.Length; i++) {
							if (ip >= (uint) Runtime.MethodBoundaries [i].Begin
									&& ip < (uint) Runtime.MethodBoundaries [i].End) {
								entry = Runtime.MethodBoundaries [i];
								break;
							}
						}

						stackFrame [count] = new StackFrame ();
						stackFrame [count].IP = (void*) ip;
						stackFrame [count].BP = (void*) bp;
						stackFrame [count].MethodBoundary = entry;
					}

					count++;

					// Get the next IP
					Asm.MOV (R32.EBX, &bp);
					Asm.MOV (R32.EAX, new DWordMemory (null, R32.EBX, null, 0, 4));
					Asm.MOV (&ip, R32.EAX);

					// Get the next BP
					Asm.MOV (R32.EAX, new DWordMemory (null, R32.EBX, null, 0));
					Asm.MOV (&bp, R32.EAX);

				} while (bp != 0);
			}

			return stackFrame;
		}

		internal unsafe static void CallHandler (InternalSystem.Exception exception, SharpOS.Korlib.Runtime.ExceptionHandlingClause handler)
		{
			int calleeIndex = exception.CurrentStackFrame - 1;
			uint calleeBP = (uint) exception.CallingStack [calleeIndex].BP;
			uint targetIP = (uint) handler.HandlerBegin;
			uint exceptionAddress = (uint) Runtime.GetPointerFromObject (exception);

			// The address (EDX) will be then used in the handler
			Asm.MOV (R32.EDX, &exceptionAddress);

			// Set the address where it will jump to handle the exception
			Asm.MOV (R32.ECX, &targetIP);

			// This is very dependent of the way the AOT generates the prolog of the method
			Asm.MOV (R32.EAX, &calleeBP);
			Asm.SUB (R32.EAX, 12);
			Asm.MOV (R32.ESP, R32.EAX);
			Asm.POP (R32.EDI);
			Asm.POP (R32.ESI);
			Asm.POP (R32.EBX);
			Asm.POP (R32.EBP);

			// Just dump the address of the caller
			Asm.POP (R32.EAX);

			// We push the exception address on the stack
			Asm.PUSH (R32.EDX);

			// Push target address of the handler
			Asm.PUSH (R32.ECX);
			Asm.RET ();
		}
	}
}