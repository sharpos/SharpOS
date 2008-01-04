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
using SharpOS;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.ADC
{
	// FIXME: ...Causes lots of "not implemented" errors in the AOT
	public static unsafe class Interlocked {
		/*
		#region Add
		[AOTAttr.ADCStub]
		public static unsafe uint Add(uint* location, uint value) { throw new Exception("not implemented"); }
		#endregion

		#region CompareExchange
		[AOTAttr.ADCStub]
		public static unsafe uint CompareExchange(uint* location, uint value, uint comparand) { throw new Exception("not implemented"); }
		#endregion

		#region Decrement
		[AOTAttr.ADCStub]
		public static unsafe uint Decrement(uint* location) { throw new Exception("not implemented"); }
		#endregion

		#region Exchange
		[AOTAttr.ADCStub]
		public static unsafe uint Exchange(uint* location, uint value) { throw new Exception("not implemented"); }
		#endregion

		#region Increment
		[AOTAttr.ADCStub]
		public static unsafe uint Increment(ref uint location) { throw new Exception("not implemented"); }
		#endregion
		*/
	}
}
