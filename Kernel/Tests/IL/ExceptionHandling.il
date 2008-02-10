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
	.class public auto ansi TestException extends [mscorlib]System.Exception
	{
		.field public int32 Error

		.method public instance void .ctor(int32)
		{
			ldarg.0
			call instance void [mscorlib]System.Exception::.ctor()
			ldarg.0
			ldarg.1
			stfld int32 SharpOS.Kernel.Tests.IL.TestException::Error
			ret
		}
	}
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

		.method public hidebysig static uint32 CMPDontRunFilter() cil managed
		{
			.maxstack 2
			.locals init (uint32 result)
			
			.try
			{
				ldc.i4.1 
				stloc.0 
				leave.s exit
			}
			filter
			{
				ldc.i4.1
				endfilter
			}
			{
				ldc.i4.0
				stloc.0 
				leave.s exit
			}
			
			exit:
			ldloc.0
			ret 
		} 

		.method public hidebysig static uint32 CMPFilter() cil managed
		{
			.maxstack 2
			.locals init (uint32 result)
			
			.try
			{
				ldc.i4.0 
				stloc.0 
				
				call void SharpOS.Kernel.Tests.IL.ExceptionHandling::ThrowException()
				
				leave.s exit
			}
			filter
			{
				ldc.i4.1
				endfilter
			}
			{
				ldc.i4.1
				stloc.0 
				leave.s exit
			}
			
			exit:
			ldloc.0
			ret 
		} 

		.method public static void ThrowException()
		{
			ldc.i4.1
			newobj instance void SharpOS.Kernel.Tests.IL.TestException::.ctor(int32)
			throw
		}
	}
}

