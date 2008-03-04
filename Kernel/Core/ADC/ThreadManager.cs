//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.ADC {
	public static unsafe class ThreadManager {		
		[AOTAttr.ADCStub]
		public static unsafe void Setup ()
		{
			Diagnostics.Error ("Unimplemented - Setup");
		}

		[AOTAttr.ADCStub]
		public static SharpOS.Kernel.ADC.Thread CreateThread (uint function_address)
		{
			Diagnostics.Error ("Unimplemented - CreateThread");
			return null;
		}
	}
}
