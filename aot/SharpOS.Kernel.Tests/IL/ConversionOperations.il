/*
	*  (C) 2007 Johann MacDonagh <johann [at] macdonaghs [dot] com>
	* 
	*  Licensed under the terms of the GNU GPL License version 2.
	*
*/

.assembly extern mscorlib
{
}

.namespace SharpOS.Kernel.Tests.IL
{
	.class public auto ansi abstract sealed beforefieldinit ConversionOperations extends [mscorlib]System.Object
	{
		.method public static int32 CMP0() cil managed
		{
			.maxstack 8

			ldc.i4 1
			conv.i2

			ldc.i4 1
			conv.i2
			ceq
			brtrue ok
			
			ldc.i4 0
			ret
			
			ok:
			ldc.i4 1
			ret
		}
	}
}
