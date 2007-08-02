// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.Runtime.InteropServices;
using SharpOS;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using SharpOS.ADC;

namespace SharpOS 
{
	public unsafe partial class Kernel 
	{
		public static bool stayInLoop = true;
		static KernelStage kernelStage = KernelStage.Init;
		
		[SharpOS.AOT.Attributes.KernelMain]
		public unsafe static void BootEntry (uint magic, uint pointer, uint kernelStart, uint kernelEnd)
		{
			TextMode.Setup();

			TextMode.SetAttributes (TextColor.Yellow, TextColor.Black);

			TextMode.WriteLine (Kernel.String ("SharpOS v0.0.0.75 (http://www.sharpos.org)"));
			TextMode.WriteLine ();

			Arch.Setup ();
			Keyboard.Setup();

			if (!Multiboot.WriteMultibootInfo (magic, pointer, kernelStart, kernelEnd)) {
				TextMode.WriteLine (Kernel.String ("Error: multiboot loader required!"));
				return;
			}
			
			//CPU.Setup ();

			TextMode.GoTo (0, 23);
			TextMode.SetAttributes (TextColor.LightGreen, TextColor.Black);
			TextMode.WriteLine (String ("Pinky: What are we gonna do tonight, Brain?"));
			TextMode.WriteLine (String ("The Brain: The same thing we do every night, Pinky - Try to take over the world!"));
			TextMode.RestoreAttributes ();

			SharpOS.Console.Setup();

			//FIXME: this currently crashes the aot-compiler - LogicalError
			//while (true);
			
			//this works;
			byte n = 0;
			while (n < 100) { }
		}

		public unsafe static void SetKernelStage (KernelStage stage)
		{
			kernelStage = stage;
		}
		
		public unsafe static KernelStage GetKernelStage ()
		{
			return kernelStage;
		}
		
		public unsafe static void Halt ()
		{
			SetKernelStage (KernelStage.Halt);
			BootControl.Halt ();
		}
		
		public unsafe static void Reboot ()
		{
			SetKernelStage (KernelStage.Halt);
			BootControl.Reboot ();
		}
		
		#region Call
		public unsafe static void Call (uint address, uint value)
		{
			if (address == 0)
				return;

			Asm.PUSH(&value);
			Asm.CALL(&address);
		}
		#endregion

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

