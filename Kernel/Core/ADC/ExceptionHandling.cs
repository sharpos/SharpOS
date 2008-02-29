//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using AOTAttr = SharpOS.AOT.Attributes;
using SharpOS.Korlib.Runtime;

namespace SharpOS.Kernel.ADC {
#pragma warning disable 649
	internal unsafe class StackFrame {
		public void* IP;
		public void* BP;
		public bool [] IgnoreMethodBoundaryClause;
		public MethodBoundary MethodBoundary;

		public StackFrame (void* ip, void* bp, MethodBoundary methodBoundary)
		{
			this.IP = ip;
			this.BP = bp;
			this.MethodBoundary = methodBoundary;

			if (methodBoundary.ExceptionHandlingClauses != null) {
				this.IgnoreMethodBoundaryClause = new bool [methodBoundary.ExceptionHandlingClauses.Length];

				for (int i = 0; i < this.IgnoreMethodBoundaryClause.Length; i++)
					this.IgnoreMethodBoundaryClause [i] = false;
			} else
				this.IgnoreMethodBoundaryClause = new bool [0];
		}
	}
#pragma warning restore 649

	public static class ExceptionHandling {
		[AOTAttr.ADCStub]
		public static void Setup ()
		{
		}

		[AOTAttr.ADCStub]
		internal unsafe static StackFrame [] GetCallingStack ()
		{
			Diagnostics.Error ("Unimplemented - ExceptionHandling.GetCallingStack");

			return null;
		}
		
		[AOTAttr.ADCStub]
		internal unsafe static void DumpCallingStack ()
		{
			Diagnostics.Error ("Unimplemented - ExceptionHandling.DumpCallingStack");
		}

		[AOTAttr.ADCStub]
		internal unsafe static void CallHandler (InternalSystem.Exception exception, ExceptionHandlingClause handler, void* callerBP)
		{
			Diagnostics.Error ("Unimplemented - ExceptionHandling.CallHandler");
		}
		
		[AOTAttr.ADCStub]
		internal unsafe static void CallFinallyFault (InternalSystem.Exception exception, ExceptionHandlingClause handler, void* callerBP)
		{
			Diagnostics.Error ("Unimplemented - ExceptionHandling.CallFinallyFault");
		}
		
		[AOTAttr.ADCStub]
		internal unsafe static uint CallFilter (InternalSystem.Exception exception, ExceptionHandlingClause handler, void* callerBP)
		{
			Diagnostics.Error ("Unimplemented - ExceptionHandling.CallFilter");

			return 0;
		}
	}
}
