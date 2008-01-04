//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using SharpOS.AOT;
using SharpOS.AOT.X86;
using System;

namespace SharpOS.Kernel.ADC.X86
{
	// FIXME: ...Causes lots of "not implemented" errors in the AOT
	public static class Interlocked	{
		/*
		#region Add
		public static unsafe uint Add(uint* location, uint value)
		{
			Asm.LOCK();
			Asm.ADD(location, value);
			return *location;
		}
		#endregion
		  
		#region CompareExchange
		public static unsafe uint CompareExchange(uint* location, uint value, uint comparand)
		{
			Asm.MOV(R32.ECX, location);
			Asm.MOV(R32.EDX, &value);
			Asm.MOV(R32.EAX, &comparand);
			Asm.LOCK();
			Asm.CMPXCHG(R32.ECX, R32.EDX);
			Asm.MOV(location, R32.EAX);
			return *location;
		}
		#endregion

		#region Decrement
		public static unsafe uint Decrement(uint* location)
		{
			Asm.LOCK();
			Asm.DEC(location);
			return *location;
		}
		#endregion

		#region Exchange
		public static unsafe uint Exchange(uint* location, uint value)
		{
			Asm.MOV(R32.ECX, location);
			Asm.MOV(R32.EDX, &value);
			Asm.LOCK();
			Asm.XCHG(R32.ECX, R32.EDX);
			Asm.MOV(location, R32.EDX); //? a guess
			return *location;
		}
		#endregion

		#region Increment
		public static unsafe uint Increment(uint* location)
		{
			Asm.LOCK();
			Asm.INC(location);
			return *location;
		}
		#endregion
		*/
	}
}
