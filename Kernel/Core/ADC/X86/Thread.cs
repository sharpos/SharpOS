//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT;
using AOTAttr = SharpOS.AOT.Attributes;
using SharpOS.Kernel.Foundation;
using System;
using SharpOS.AOT.X86;

namespace SharpOS.Kernel.ADC.X86 {
	public sealed unsafe class Thread
	{
		public static void		Yield() 
		{
			// TODO: implement thread yielding
			SpinWait(1);
		}
		
		public static void		SpinWait(int iterations) 
		{
			if (iterations <= 0)
				return;

			Asm.MOV(R32.EAX, (uint*)&iterations);
			Asm.LABEL("SpinWait");
			Asm.PAUSE();
			Asm.DEC(R32.EAX);
			Asm.JNC("SpinWait");
		}
		
		public static Int32		VolatileRead(ref Int32 address) 
		{
			// NOTE: assuming single core system here!
			return address;
		}
		
		public static void		BeginCriticalRegion()
		{
			// TODO: implement
		}
		
		public static void		EndCriticalRegion()
		{
			// TODO: implement
		}
	}
}
