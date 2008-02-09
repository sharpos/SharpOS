//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Johann MacDonagh <johann [at] macdonaghs [dot] com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

.assembly extern mscorlib
{
}

.namespace SharpOS.Kernel.Tests.IL
{
	.class public auto ansi abstract sealed beforefieldinit MethodArguments extends [mscorlib]System.Object
	{
		.method public static int32 CMP0() cil managed
		{
			.maxstack 8

			ldc.i4 3
			call int32 class SharpOS.Kernel.Tests.IL.MethodArguments::returnFirstArgDot0(int32)
			
			ldc.i4 3
			ceq
			brtrue ok
			
			ldc.i4 0
			ret

			ok:
			ldc.i4 1
			ret
		}

		.method public static int32 CMP1() cil managed
		{
			.maxstack 8

			ldc.i4 5
			call int32 class SharpOS.Kernel.Tests.IL.MethodArguments::returnFirstArg(int32)
			
			ldc.i4 5
			ceq
			brtrue ok
			
			ldc.i4 0
			ret

			ok:
			ldc.i4 1
			ret
		}
		
		.method public static int32 CMP2() cil managed
		{
			.maxstack 8

			ldc.i4 3
			ldc.i4 9
			call int32 class SharpOS.Kernel.Tests.IL.MethodArguments::returnSecondArg(int32, int32)
			
			ldc.i4 9
			ceq
			brtrue ok
			
			ldc.i4 0
			ret

			ok:
			ldc.i4 1
			ret
		}

		.method private static int32 returnFirstArgDot0(int32) cil managed
		{
			.maxstack 8

			ldarg.0
			ret
		}
		
		.method private static int32 returnFirstArg(int32) cil managed
		{
			.maxstack 8

			ldarg 0
			ret
		}
		
		.method private static int32 returnSecondArg(int32, int32) cil managed
		{
			.maxstack 8

			ldarg 1
			ret
		}
	}
}
