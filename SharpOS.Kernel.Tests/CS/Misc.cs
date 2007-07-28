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
		
		public unsafe static uint Misc2 (uint granularity)
		{
			switch (granularity) {
				case 0:
					return 4096;
				case 1:
					return 131072;
				default:
					return 0xFFFFFFFF;
			}
		}

		public unsafe static uint CMPMisc2a ()
		{
			if (Misc2 (0) == 4096)
				return 1;
				
			return 0;
		}

		public unsafe static uint CMPMisc2b ()
		{
			if (Misc2 (1) == 131072)
				return 1;

			return 0;
		}

		public unsafe static uint CMPMisc2c ()
		{
			if (Misc2 (5) == 0xFFFFFFFF)
				return 1;

			return 0;
		}
	}
}
