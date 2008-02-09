//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

.assembly extern mscorlib
{
}

.namespace SharpOS.Kernel.Tests.IL
{
	.class public auto ansi abstract sealed beforefieldinit ExceptionHandling extends [mscorlib]System.Object
	{	
		.method public hidebysig static uint32 CMPFinally() cil managed
		{
			.maxstack 1
			.locals init (uint32 result)
			
			.try
			{
				ldc.i4.0 
				stloc.0 
				leave.s exit
			}
			finally
			{
				ldc.i4.1 
				stloc.0 
				endfinally 
			}
			
			exit:
			ldloc.0
			ret 
		}

 

 

	}
}

