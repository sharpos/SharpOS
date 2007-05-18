/*
	*  (C) 2007 Johann MacDonagh <johann [at] macdonaghs [dot] com>
	* 
	*  Licensed under the terms of the GNU GPL License version 2.
	*
*/

.assembly extern mscorlib
{
}

.namespace SharpOS.TestKernel.Tests
{
	.class public auto ansi abstract sealed beforefieldinit MethodArguments extends [mscorlib]System.Object
	{
		.method public static int32 CMP0() cil managed
		{
			.maxstack 8

			ldc.i4 3
			call int32 class SharpOS.TestKernel.Tests.MethodArguments::returnFirstArgDot0(int32)
			
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
			call int32 class SharpOS.TestKernel.Tests.MethodArguments::returnFirstArg(int32)
			
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
			call int32 class SharpOS.TestKernel.Tests.MethodArguments::returnSecondArg(int32, int32)
			
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
