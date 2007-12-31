//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT.X86;

namespace SharpOS.Kernel.Tests.CS {
	public class X86 {
		public unsafe static uint PushArgument (uint value)
		{
			uint result;

			Asm.PUSH (&value);
			Asm.POP (R32.EAX);
			Asm.MOV (&result, R32.EAX);

			return result;
		}

		public static uint CMPPushArgument ()
		{
			if (PushArgument (123) == 123)
				return 1;

			return 0;
		}

		public unsafe static uint ReadArgument (uint value)
		{
			uint result;

			Asm.MOV (R32.EAX, new DWordMemory (null, R32.EBP, null, 0, 8));
			Asm.MOV (&result, R32.EAX);

			return result;
		}

		public static uint CMPReadArgument ()
		{
			if (ReadArgument (123) == 123)
				return 1;

			return 0;
		}

		private const string X86_Test_LABEL = "X86_Test_LABEL";

		public unsafe static void X86TestLabel (uint value)
		{
			Asm.LABEL (X86_Test_LABEL);
			Asm.DATA ((uint) 0);
		}

		public unsafe static uint LabelHandling (uint value)
		{
			uint result;

			Asm.MOV (R32.EAX, &value);
			Asm.MOV (new DWordMemory (X86_Test_LABEL), R32.EAX);


			Asm.MOV (R32.EDX, new DWordMemory (X86_Test_LABEL));
			Asm.SHL (R32.EDX, 1);
			Asm.MOV (&result, R32.EDX);

			return result;
		}

		public static uint CMPLabelHandling ()
		{
			if (LabelHandling (123) == 246)
				return 1;

			return 0;
		}
	}
}
