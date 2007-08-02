//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//
using SharpOS.AOT.X86;

namespace SharpOS.Kernel.Tests.CS {
	public class Misc {
		public unsafe static void Misc1 (uint value)
		{
			Asm.PUSH(&value);
			Asm.POP(R32.EAX);
		}
	
		public static uint CMPMisc1 ()
		{
			Misc1 (100);
				
			return 1;
		}
	}
}
