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
	.class public auto ansi abstract sealed beforefieldinit ConstantLoading extends [mscorlib]System.Object
	{
		.method public static hidebysig default int32 CMP0 () cil managed
		{
			.maxstack 8

			ldc.i4 1
			ret
		}
		
		.method public static hidebysig default int32 CMP1 () cil managed
		{
			.maxstack 8

			ldc.i4 1
			ldc.i4 0
			pop
			ret
		}
	}
}

