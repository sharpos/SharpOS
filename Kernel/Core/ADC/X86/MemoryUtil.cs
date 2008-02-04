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
using SharpOS.Korlib.Runtime;

namespace SharpOS.Kernel.ADC.X86 {
	public static class MemoryUtil	{

		#region MemSet
		public static unsafe void MemSet (uint value, uint dst, uint count)
		{
			Asm.CLD ();
			Asm.MOV (R32.ECX, &count);
			Asm.MOV (R32.EAX, &value);
			Asm.MOV (R32.EDI, &dst);

			Asm.SHR (R32.ECX, 1);		// divide count by 2 (8bit -> 16bit blocks)
			Asm.JNC ("Set16");			// if carry flag has not been set, skip next instruction
			Asm.STOSB ();				// move a byte (8bit) first

			Asm.LABEL ("Set16");
			Asm.SHR (R32.ECX, 1);		// divide count by 2 (16bit -> 32bit blocks)
			Asm.JNC ("Set32");			// if carry flag has not been set, skip next instruction
			Asm.STOSW ();				// move short (16bit) first

			Asm.LABEL ("Set32");
			Asm.REP ();
			Asm.STOSD ();
		}
		#endregion
		
		#region MemSet32
		public static unsafe void MemSet32 (uint value, uint dst, uint count)
		{
			Asm.CLD ();
			Asm.MOV (R32.ECX, &count);
			Asm.MOV (R32.EAX, &value);
			Asm.MOV (R32.EDI, &dst);
			Asm.REP ();
			Asm.STOSD ();
		}
		#endregion
		
		#region BitCount
		public static uint BitCount (byte value)
		{
			uint count = 0;    
			while (value > 0)
			{
				count += (value & 1u);
				value >>= 1 ;
			}
			return (uint)count;
		}
		
		public static uint BitCount (ushort value)
		{
			uint count = 0;    
			while (value > 0)
			{
				count += (value & 1u);
				value >>= 1 ;
			}
			return (uint)count;
		}
		
		public static uint BitCount (uint value)
		{
			uint count = 0;    
			while (value > 0)
			{
				count += (value & 1u);
				value >>= 1 ;
			}
			return (uint)count;
		}

		public static uint BitCount (ulong value)
		{
			ulong count = 0;    
			while (value > 0)
			{
				count += (value & 1ul);
				value >>= 1 ;
			}
			return (uint)count;
		}
		#endregion
	
		#region MemCopy
		public static unsafe void MemCopy (uint src, uint dst, uint count)
		{
			Asm.CLD ();
			Asm.MOV (R32.ECX, &count);
			Asm.MOV (R32.ESI, &src);
			Asm.MOV (R32.EDI, &dst);
			Asm.CMP (R32.ESI, R32.EDI);
			Asm.JA ("FMove8");
			Asm.STD ();
			Asm.ADD (R32.ESI, R32.ECX);
			Asm.ADD (R32.EDI, R32.ECX);
			Asm.LABEL ("BMove8");
			Asm.SHR (R32.ECX, 1);		// divide count by 2 (8bit -> 16bit blocks)
			Asm.JNC ("BMove16");			// if carry flag has not been set, skip next instruction
			Asm.MOVSB ();				// move a byte (8bit) first

			Asm.LABEL ("BMove16");
			Asm.SHR (R32.ECX, 1);		// divide count by 2 (16bit -> 32bit blocks)
			Asm.JNC ("BMove32");			// if carry flag has not been set, skip next instruction
			Asm.MOVSW ();				// move short (16bit) first

			Asm.LABEL ("BMove32");
			Asm.INC (R32.ECX);
			Asm.REP ();
			Asm.MOVSD ();		// move everything else in 32bit blocks
			Asm.JMP ("EndMove");

			Asm.LABEL ("FMove8");
			Asm.SHR (R32.ECX, 1);		// divide count by 2 (8bit -> 16bit blocks)
			Asm.JNC ("FMove16");			// if carry flag has not been set, skip next instruction
			Asm.MOVSB ();				// move a byte (8bit) first

			Asm.LABEL ("FMove16");
			Asm.SHR (R32.ECX, 1);		// divide count by 2 (16bit -> 32bit blocks)
			Asm.JNC ("FMove32");			// if carry flag has not been set, skip next instruction
			Asm.MOVSW ();				// move short (16bit) first

			Asm.LABEL ("FMove32");
			Asm.REP ();
			Asm.MOVSD ();		// move everything else in 32bit blocks
			Asm.LABEL ("EndMove");
		}
		#endregion
	
		#region MemCopy32
		public static unsafe void MemCopy32 (uint src, uint dst, uint count)
		{
			Asm.CLD ();
			Asm.MOV (R32.ECX, &count);
			Asm.MOV (R32.ESI, &src);
			Asm.MOV (R32.EDI, &dst);
			Asm.REP ();
			Asm.MOVSD ();
		}
		#endregion
	
		#region Call
		public unsafe static void Call (uint address, uint value)
		{
			Asm.PUSH (&value);
			Asm.CALL (&address);
		}

		public unsafe static void Call (void* address, uint value)
		{
			uint _address = (uint) address;

			Asm.PUSH (&value);
			Asm.CALL (&_address);
		}

		public unsafe static void Call (void* functionPointer, void* pointeredParameter)
		{
			uint _functionPointer = (uint) functionPointer;
			uint _pointeredParameter = (uint) pointeredParameter;

			Asm.PUSH (&_pointeredParameter);
			Asm.CALL (&_functionPointer);
		}

		/*		// sigh.. one can only dream...
				public delegate void call_function(uint value);
				public unsafe static void Call(call_function function, uint value)
				{
					Asm.PUSH(&value);
					Asm.CALL(function);
				}
		*/

		private const string GET_IP = "GET_IP";

		internal unsafe static MethodBoundary [] GetCallingStack ()
		{
			uint bp, ip, count = 0;
			MethodBoundary [] methodBoundaries = null;

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
					methodBoundaries = new MethodBoundary [count];

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

						methodBoundaries [count] = entry;
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

			return methodBoundaries;
		}
		#endregion
	}
}
