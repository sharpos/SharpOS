//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using SharpOS.AOT;
using SharpOS.AOT.X86;
using System;

namespace SharpOS.Kernel.ADC.X86
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
		public static void Enter()
		{
			Asm.CLI ();
		}
		#endregion

		#region Exit
		/// <summary>
		///		Ends the barrier
		/// </summary>
		/// <remarks>
		///		This function should be made "inline" by the AOT
		/// </remarks>
		public static void Exit()
		{
			Asm.STI ();
		}
		#endregion
	}
}
