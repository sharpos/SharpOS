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
using SharpOS.ADC.X86;

namespace SharpOS {
	public unsafe partial class Kernel {
		[SharpOS.AOT.Attributes.KernelMain]
		public unsafe static void BootEntry (uint magic, uint pointer, uint kernelStart, uint kernelEnd)
		{
			Screen.SetAttributes (Screen.ColorTypes.Yellow, Screen.ColorTypes.Black);

			Screen.WriteLine (Kernel.String ("SharpOS v0.0.0.75 (http://www.sharpos.org)"));
			Screen.WriteNL ();

			GDT.Setup ();
			PIC.Setup ();
			IDT.Setup ();
			PIT.Setup ();

			if (!Multiboot.WriteMultibootInfo (magic, pointer, kernelStart, kernelEnd))
				return;
			
			CPU.Setup ();

			Screen.GoTo (0, 23);
			Screen.SetAttributes (Screen.ColorTypes.LightGreen, Screen.ColorTypes.Black);
			Screen.WriteLine (String ("Pinky: What are we gonna do tonight, Brain?"));
			Screen.WriteLine (String ("The Brain: The same thing we do every night, Pinky - Try to take over the world!"));
			Screen.RestoreAttributes ();

			//FIXME: this currently crashes the aot-compiler - LogicalError
			//while (true);
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

