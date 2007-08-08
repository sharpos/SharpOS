// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.Runtime.InteropServices;
using SharpOS;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS {
	public unsafe partial class KRNL {
		[SharpOS.AOT.Attributes.KernelMain]
		public unsafe static void BootEntry (uint magic, uint pointer, uint kernelStart, uint kernelEnd)
		{
			Screen.SetAttributes (Screen.ColorTypes.Yellow, Screen.ColorTypes.Black);

			Screen.WriteLine (String ("SharpOS Unit Tests (http://www.sharpos.org)"));
			Screen.WriteNL ();

			RunTests ();
		}

		#region Stubs
		[SharpOS.AOT.Attributes.String]
		public unsafe static byte* String (string value)
		{
			return null;
		}

		[SharpOS.AOT.Attributes.Alloc]
		public unsafe static byte* Alloc (uint value)
		{
			return null;
		}

		[SharpOS.AOT.Attributes.LabelledAlloc]
		public unsafe static byte* LabelledAlloc (string label, uint value)
		{
			return null;
		}

		[SharpOS.AOT.Attributes.LabelAddress]
		public unsafe static uint GetFunctionPointer (string label)
		{
			return 0;
		}
		#endregion
	}
}

