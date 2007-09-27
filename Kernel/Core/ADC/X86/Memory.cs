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
	public static class Memory
	{
		public static unsafe void MemSet(uint value, uint dst, uint count)
		{
									Asm.MOV(R32.ECX, &count);
									Asm.MOV(R32.EAX, &value);
									Asm.MOV(R32.EDI, &dst);

									Asm.SHR(R32.ECX, 1);		// divide count by 2 (8bit -> 16bit blocks)
									Asm.JNC("Set16");			// if carry flag has not been set, skip next instruction
							    	Asm.STOSB();				// move a byte (8bit) first

			Asm.LABEL("Set16");		Asm.SHR(R32.ECX, 1);		// divide count by 2 (16bit -> 32bit blocks)
							    	Asm.JNC("Set32");			// if carry flag has not been set, skip next instruction
									Asm.STOSW();				// move short (16bit) first

			Asm.LABEL("Set32");		Asm.REP(); Asm.STOSD();
		}

		public static unsafe void MemSet32(uint value, uint dst, uint count)
		{
			Asm.MOV(R32.ECX, &count);
			Asm.MOV(R32.EAX, &value);
			Asm.MOV(R32.EDI, &dst);
			Asm.REP();
			Asm.STOSD();
		}

		public static unsafe void MemCopy(uint src, uint dst, uint count)
		{
							    	Asm.MOV(R32.ECX, &count);
							    	Asm.MOV(R32.ESI, &src);
							    	Asm.MOV(R32.EDI, &dst);

							    	Asm.SHR(R32.ECX, 1);		// divide count by 2 (8bit -> 16bit blocks)
									Asm.JNC("Move16");			// if carry flag has not been set, skip next instruction
							    	Asm.MOVSB();				// move a byte (8bit) first

			Asm.LABEL("Move16");	Asm.SHR(R32.ECX, 1);		// divide count by 2 (16bit -> 32bit blocks)
							    	Asm.JNC("Move32");			// if carry flag has not been set, skip next instruction
									Asm.MOVSW();				// move short (16bit) first

			Asm.LABEL("Move32");	Asm.REP(); Asm.MOVSD();		// move everything else in 32bit blocks
		}

		public static unsafe void MemCopy32(uint src, uint dst, uint count)
		{
			Asm.MOV(R32.ECX, &count);
			Asm.MOV(R32.ESI, &src);
			Asm.MOV(R32.EDI, &dst);
			Asm.REP();
			Asm.MOVSD();
		}

		public unsafe static void Call(uint address, uint value)
		{
			Asm.PUSH(&value);
			Asm.CALL(&address);
		}

		public unsafe static void Call(void* address, uint value)
		{
			uint _address = (uint) address;

			Asm.PUSH(&value);
			Asm.CALL(&_address);
		}

/*		// sigh.. one can only dream...
		public delegate void call_function(uint value);
		public unsafe static void Call(call_function function, uint value)
		{
			Asm.PUSH(&value);
			Asm.CALL(function);
		}
*/
	}
}
