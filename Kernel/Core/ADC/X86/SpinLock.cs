//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using SharpOS.AOT;
using SharpOS.AOT.X86;
using System;

namespace SharpOS.Kernel.ADC.X86
{
	public static class SpinLock
	{

		#region Lock
		public static unsafe void Lock(uint* location)
		{
			Asm.MOV(R32.EDX, 1);

			// loop:
			Asm.LABEL("Kernel_SpinLock_Entry");

			Asm.XCHG(R32.EAX, location);
			Asm.TEST(R32.EAX, R32.EAX);
			Asm.JNZ("Kernel_SpinLock_Entry");

			Asm.RET();
		}
		#endregion

		#region Release
		public static unsafe void Release(uint* location)
		{
			Asm.MOV(R32.EDX, 0);
			Asm.XCHG(R32.EAX, location);
			Asm.RET();
		}
		#endregion

	}
}
