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
	public static unsafe class Barrier
	{
		#region Enter
		/// <summary>
		///		Starts the barrier, no other threads can run untill Exit is called
		/// </summary>
		/// <remarks>
		///		This function should be made "inline" by the AOT
		/// </remarks>
		[AOTAttr.ADCStub]
		public static void Enter()
		{
		}
		#endregion

		#region Exit
		/// <summary>
		///		Ends the barrier
		/// </summary>
		/// <remarks>
		///		This function should be made "inline" by the AOT
		/// </remarks>
		[AOTAttr.ADCStub]
		public static void Exit()
		{
		}
		#endregion
	}
}
