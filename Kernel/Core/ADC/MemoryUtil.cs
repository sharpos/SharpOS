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
using SharpOS.Korlib.Runtime;

namespace SharpOS.Kernel.ADC {
	public static class MemoryUtil {
		[AOTAttr.ADCStub]
		public unsafe static void Call (void* functionPointer, void* pointeredParameter)
		{
			Diagnostics.Error ("Unimplemented - MemoryUtil.Call");
		}
	}
}
