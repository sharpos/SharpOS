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
	.class public auto ansi abstract sealed beforefieldinit ConditionChecking extends [mscorlib]System.Object
	{
		.method public static hidebysig default int32 CMP0 () cil managed
		{
			.maxstack 8

			ldc.i4 1
			ret
		}
	}
}
