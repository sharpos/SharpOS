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

namespace SharpOS.ADC.X86 {
	public unsafe class CPU {
		public unsafe static void WriteProcessorInfo ()
		{
			UInt32 eax = 0, ebx = 0, ecx = 0, edx = 0;

			// Processor Info & Feature Bits
			Asm.XOR (R32.EAX, R32.EAX);
			Asm.INC (R32.EAX);
			Asm.CPUID ();
			Asm.MOV (&eax, R32.EAX);
			Asm.MOV (&ebx, R32.EBX);
			Asm.MOV (&edx, R32.EDX);
			Asm.MOV (&ecx, R32.ECX);

			Screen.WriteMessage (Kernel.String ("CPU Family: "));
			Screen.SetAttributes (Screen.ColorTypes.LightCyan, Screen.ColorTypes.Black);
			Screen.WriteByte ((byte) ((eax >> 8) & 0x0F));
			Screen.RestoreAttributes ();
			Screen.WriteNL ();

			Screen.WriteMessage (Kernel.String ("CPU Model: "));
			Screen.SetAttributes (Screen.ColorTypes.LightCyan, Screen.ColorTypes.Black);
			Screen.WriteByte ((byte) ((eax >> 4) & 0x0F));
			Screen.RestoreAttributes ();
			Screen.WriteNL ();

			Screen.WriteMessage (Kernel.String ("CPU Stepping: "));
			Screen.SetAttributes (Screen.ColorTypes.LightCyan, Screen.ColorTypes.Black);
			Screen.WriteByte ((byte) (eax & 0x0F));
			Screen.RestoreAttributes ();
			Screen.WriteNL ();

			Screen.WriteMessage (Kernel.String ("CPU Flags: "));
			Screen.SetAttributes (Screen.ColorTypes.LightCyan, Screen.ColorTypes.Black);

			if ((edx & 0x000000001) != 0)
				Screen.WriteMessage (Kernel.String ("FPU "));

			if ((edx & 0x000000002) != 0)
				Screen.WriteMessage (Kernel.String ("VME "));

			if ((edx & 0x000000004) != 0)
				Screen.WriteMessage (Kernel.String ("DE "));

			if ((edx & 0x000000008) != 0)
				Screen.WriteMessage (Kernel.String ("PSE "));

			if ((edx & 0x000000010) != 0)
				Screen.WriteMessage (Kernel.String ("TSC "));

			if ((edx & 0x000000020) != 0)
				Screen.WriteMessage (Kernel.String ("MSR "));

			if ((edx & 0x000000040) != 0)
				Screen.WriteMessage (Kernel.String ("PAE "));

			if ((edx & 0x000000080) != 0)
				Screen.WriteMessage (Kernel.String ("MCE "));

			if ((edx & 0x000000100) != 0)
				Screen.WriteMessage (Kernel.String ("CX8 "));

			if ((edx & 0x000000200) != 0)
				Screen.WriteMessage (Kernel.String ("APIC "));

			if ((edx & 0x000000800) != 0)
				Screen.WriteMessage (Kernel.String ("SEP "));

			if ((edx & 0x000004000) != 0)
				Screen.WriteMessage (Kernel.String ("MTRR "));

			if ((edx & 0x000002000) != 0)
				Screen.WriteMessage (Kernel.String ("PGE "));

			if ((edx & 0x000004000) != 0)
				Screen.WriteMessage (Kernel.String ("MCA "));

			if ((edx & 0x000008000) != 0)
				Screen.WriteMessage (Kernel.String ("CMOV "));

			if ((edx & 0x000010000) != 0)
				Screen.WriteMessage (Kernel.String ("PAT "));

			if ((edx & 0x000020000) != 0)
				Screen.WriteMessage (Kernel.String ("PSE36 "));

			if ((edx & 0x000100000) != 0)
				Screen.WriteMessage (Kernel.String ("NEPP "));

			if ((edx & 0x000400000) != 0)
				Screen.WriteMessage (Kernel.String ("MMXEXT "));

			if ((edx & 0x000800000) != 0)
				Screen.WriteMessage (Kernel.String ("MMX "));

			if ((edx & 0x001000000) != 0)
				Screen.WriteMessage (Kernel.String ("FXSAVE "));

			if ((edx & 0x002000000) != 0)
				Screen.WriteMessage (Kernel.String ("FFXSAVE "));

			if ((edx & 0x020000000) != 0)
				Screen.WriteMessage (Kernel.String ("EM64T "));

			if ((edx & 0x040000000) != 0)
				Screen.WriteMessage (Kernel.String ("3DNOWX "));

			if ((edx & 0x080000000) != 0)
				Screen.WriteMessage (Kernel.String ("3DNOW "));

			Screen.RestoreAttributes ();

			Screen.WriteNL ();
		}

		public unsafe static void WriteNoCPUID ()
		{
			Screen.SetAttributes (Screen.ColorTypes.LightRed, Screen.ColorTypes.Black);
			Screen.WriteLine (Kernel.String ("No CPUID!"));
			Screen.RestoreAttributes ();
		}

		public unsafe static void WriteVendorName ()
		{
			UInt32 ebx = 0, ecx = 0, edx = 0;

			// Vendor Name
			Asm.XOR (R32.EAX, R32.EAX);
			Asm.CPUID ();
			Asm.MOV (&ebx, R32.EBX);
			Asm.MOV (&edx, R32.EDX);
			Asm.MOV (&ecx, R32.ECX);

			Screen.WriteMessage (Kernel.String ("CPU Vendor: "));
			Screen.SetAttributes (Screen.ColorTypes.LightCyan, Screen.ColorTypes.Black);
			Screen.WriteString (ebx);
			Screen.WriteString (edx);
			Screen.WriteString (ecx);
			Screen.RestoreAttributes ();
			Screen.WriteNL ();
		}

		public unsafe static void WriteBrandName (uint value)
		{
			UInt32 eax = 0, ebx = 0, ecx = 0, edx = 0;

			Asm.MOV (R32.EAX, &value);
			Asm.CPUID ();
			Asm.MOV (&eax, R32.EAX);
			Asm.MOV (&ebx, R32.EBX);
			Asm.MOV (&edx, R32.EDX);
			Asm.MOV (&ecx, R32.ECX);

			Screen.WriteString (eax);
			Screen.WriteString (ebx);
			Screen.WriteString (ecx);
			Screen.WriteString (edx);
		}

		public unsafe static void WriteBrandName ()
		{
			// Brand Name
			Screen.WriteMessage (Kernel.String ("CPU Brand: "));
			Screen.SetAttributes (Screen.ColorTypes.LightCyan, Screen.ColorTypes.Black);

			for (uint i = 0x80000002; i <= 0x80000004; i++)
				WriteBrandName (i);

			Screen.RestoreAttributes ();

			Screen.WriteNL ();
		}

		public unsafe static byte HasNoCPUID ()
		{
			byte result = 0;

			Asm.PUSHFD ();
			Asm.POP (R32.EAX);
			Asm.MOV (R32.ECX, R32.EAX);
			Asm.XOR (R32.EAX, 0x200000);
			Asm.PUSH (R32.EAX);
			Asm.POPFD ();
			Asm.PUSHFD ();
			Asm.POP (R32.EAX);
			Asm.CMP (R32.EAX, R32.ECX);
			Asm.SETZ (&result);

			return result;
		}

		public unsafe static void Setup ()
		{
			if (HasNoCPUID () == 1)
				WriteNoCPUID ();
			else {
				WriteVendorName ();
				WriteBrandName ();
				WriteProcessorInfo ();
			}

			return;
		}
	}
}

