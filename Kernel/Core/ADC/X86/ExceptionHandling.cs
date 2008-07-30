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
		private const string GET_IP_DUMP = "GET_IP_DUMP";
		private const string DIVIDE_ERROR = "DIVIDE_ERROR";

		public static void Setup ()
		{
			IDT.RegisterIRQ (IDT.Interrupt.DivideError, Stubs.GetFunctionPointer (DIVIDE_ERROR));
		}

		[SharpOS.AOT.Attributes.Label (DIVIDE_ERROR)]
		static unsafe void DivideByZeroHandler (IDT.ISRData data)
		{
			SharpOS.Korlib.Runtime.Runtime.Throw (new InternalSystem.DivideByZeroException (), 4);
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

						stackFrame [count] = new StackFrame ((void*) ip, (void*) bp, entry);
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
		
		internal unsafe static void DumpCallingStack ()
		{
			//if (!Serial.Initialized)
			//    return;
			bool found;
			uint bp, ip;

			// Get the current IP
			Asm.CALL (GET_IP_DUMP);
			Asm.LABEL (GET_IP_DUMP);
			Asm.POP (R32.EAX);
			Asm.MOV (&ip, R32.EAX);

			// Get the current BP
			Asm.MOV (&bp, R32.EBP);

			do {

				// Get the next IP
				Asm.MOV (R32.EBX, &bp);
				Asm.MOV (R32.EAX, new DWordMemory (null, R32.EBX, null, 0, 4));
				Asm.MOV (&ip, R32.EAX);

				// Get the next BP
				Asm.MOV (R32.EAX, new DWordMemory (null, R32.EBX, null, 0));
				Asm.MOV (&bp, R32.EAX);

				found = false;
				for (int i = 0; i < Runtime.MethodBoundaries.Length; i++) {
					if (ip >= (uint) Runtime.MethodBoundaries [i].Begin
							&& ip < (uint) Runtime.MethodBoundaries [i].End) {
						
						Debug.COM1.Write (Runtime.MethodBoundaries [i].Name);
						Debug.COM1.Write (" [IP=0x");
						Debug.COM1.WriteNumber ((int) ip, true);
						Debug.COM1.Write (", BP=0x");
						Debug.COM1.WriteNumber ((int) bp, true);
						Debug.COM1.WriteLine ("]");
						found = true;
						break;
					}
				}

				if (!found)
				{
					Debug.COM1.Write ("(unknown)");
					Debug.COM1.Write (" [IP=0x");
					Debug.COM1.WriteNumber ((int) ip, true);
					Debug.COM1.Write (", BP=0x");
					Debug.COM1.WriteNumber ((int) bp, true);
					Debug.COM1.WriteLine ("]");
				}

			} while (bp != 0);
		}

		internal unsafe static void CallHandler (InternalSystem.Exception exception, SharpOS.Korlib.Runtime.ExceptionHandlingClause handler, void* callerBP)
		{
			uint _callerBP = (uint) callerBP;
			uint targetIP = (uint) handler.HandlerBegin;
			uint exceptionAddress = (uint) Runtime.GetPointerFromObject (exception);

			// The address (EDX) will be then used in the handler
			Asm.MOV (R32.EDX, &exceptionAddress);

			// Set the address where it will jump to handle the exception
			Asm.MOV (R32.ECX, &targetIP);

			// This is very dependent of the way the AOT generates the prolog of the method
			Asm.MOV (R32.EAX, &_callerBP);
			Asm.SUB (R32.EAX, 12);
			Asm.MOV (R32.ESP, R32.EAX);
			Asm.POP (R32.EDI);
			Asm.POP (R32.ESI);
			Asm.POP (R32.EBX);
			Asm.POP (R32.EBP);

			// Just dump the address of the caller
			Asm.POP (R32.EAX);

			// Assign EAX the address of the exception object
			Asm.MOV (R32.EAX, R32.EDX);

			// Push target address of the handler
			Asm.PUSH (R32.ECX);
			Asm.RET ();
		}

		internal unsafe static void CallFinallyFault (InternalSystem.Exception exception, SharpOS.Korlib.Runtime.ExceptionHandlingClause handler, void* callerBP)
		{
			uint _callerBP = (uint) callerBP;
			uint targetIP = (uint) handler.HandlerBegin;

			// Set the address where it will jump to handle the exception
			Asm.MOV (R32.ECX, &targetIP);

			// This is very dependent of the way the AOT generates the prolog of the method
			Asm.MOV (R32.EAX, &_callerBP);

			Asm.PUSH (R32.EBP);
			Asm.PUSH (R32.EBX);
			Asm.PUSH (R32.ESI);
			Asm.PUSH (R32.EDI);

			Asm.MOV (R32.EBP, new DWordMemory (null, R32.EAX, null, 0, 0));
			Asm.MOV (R32.EBX, new DWordMemory (null, R32.EAX, null, 0, -4));
			Asm.MOV (R32.ESI, new DWordMemory (null, R32.EAX, null, 0, -8));
			Asm.MOV (R32.EDI, new DWordMemory (null, R32.EAX, null, 0, -12));

			// Push target address of the handler
			Asm.CALL (R32.ECX);

			Asm.POP (R32.EDI);
			Asm.POP (R32.ESI);
			Asm.POP (R32.EBX);
			Asm.POP (R32.EBP);
		}

		internal unsafe static uint CallFilter (InternalSystem.Exception exception, SharpOS.Korlib.Runtime.ExceptionHandlingClause handler, void* callerBP)
		{
			uint _callerBP = (uint) callerBP;
			uint targetIP = (uint) handler.FilterBegin;
			uint exceptionAddress = (uint) Runtime.GetPointerFromObject (exception);
			uint result = 0;

			// The address (EDX) will be then used in the handler
			Asm.MOV (R32.EDX, &exceptionAddress);

			// Set the address where it will jump to handle the exception
			Asm.MOV (R32.ECX, &targetIP);

			// This is very dependent of the way the AOT generates the prolog of the method
			Asm.MOV (R32.EAX, &_callerBP);

			Asm.PUSH (R32.EBP);
			Asm.PUSH (R32.EBX);
			Asm.PUSH (R32.ESI);
			Asm.PUSH (R32.EDI);

			Asm.MOV (R32.EBP, new DWordMemory (null, R32.EAX, null, 0, 0));
			Asm.MOV (R32.EBX, new DWordMemory (null, R32.EAX, null, 0, -4));
			Asm.MOV (R32.ESI, new DWordMemory (null, R32.EAX, null, 0, -8));
			Asm.MOV (R32.EDI, new DWordMemory (null, R32.EAX, null, 0, -12));

			// Assign EAX the address of the exception object
			Asm.MOV (R32.EAX, R32.EDX);

			// Push target address of the handler
			Asm.CALL (R32.ECX);

			Asm.POP (R32.EDI);
			Asm.POP (R32.ESI);
			Asm.POP (R32.EBX);
			Asm.POP (R32.EBP);

			// Get the result of the filter
			Asm.MOV (&result, R32.EAX);

			return result;
		}
	}
}