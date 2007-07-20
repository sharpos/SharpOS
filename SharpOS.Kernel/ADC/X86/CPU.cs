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
using ADC = SharpOS.ADC;

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

			ADC.TextMode.Write (Kernel.String ("CPU Family: "));
			ADC.TextMode.SetAttributes (TextColor.LightCyan, TextColor.Black);
			ADC.TextMode.WriteByte ((byte) ((eax >> 8) & 0x0F));
			ADC.TextMode.RestoreAttributes ();
			ADC.TextMode.WriteLine ();

			ADC.TextMode.Write (Kernel.String ("CPU Model: "));
			ADC.TextMode.SetAttributes (TextColor.LightCyan, TextColor.Black);
			ADC.TextMode.WriteByte ((byte) ((eax >> 4) & 0x0F));
			ADC.TextMode.RestoreAttributes ();
			ADC.TextMode.WriteLine ();

			ADC.TextMode.Write (Kernel.String ("CPU Stepping: "));
			ADC.TextMode.SetAttributes (TextColor.LightCyan, TextColor.Black);
			ADC.TextMode.WriteByte ((byte) (eax & 0x0F));
			ADC.TextMode.RestoreAttributes ();
			ADC.TextMode.WriteLine ();

			ADC.TextMode.Write (Kernel.String ("CPU Flags: "));
			ADC.TextMode.SetAttributes (TextColor.LightCyan, TextColor.Black);

			if ((edx & 0x000000001) != 0)
				ADC.TextMode.Write (Kernel.String ("FPU "));

			if ((edx & 0x000000002) != 0)
				ADC.TextMode.Write (Kernel.String ("VME "));

			if ((edx & 0x000000004) != 0)
				ADC.TextMode.Write (Kernel.String ("DE "));

			if ((edx & 0x000000008) != 0)
				ADC.TextMode.Write (Kernel.String ("PSE "));

			if ((edx & 0x000000010) != 0)
				ADC.TextMode.Write (Kernel.String ("TSC "));

			if ((edx & 0x000000020) != 0)
				ADC.TextMode.Write (Kernel.String ("MSR "));

			if ((edx & 0x000000040) != 0)
				ADC.TextMode.Write (Kernel.String ("PAE "));

			if ((edx & 0x000000080) != 0)
				ADC.TextMode.Write (Kernel.String ("MCE "));

			if ((edx & 0x000000100) != 0)
				ADC.TextMode.Write (Kernel.String ("CX8 "));

			if ((edx & 0x000000200) != 0)
				ADC.TextMode.Write (Kernel.String ("APIC "));

			if ((edx & 0x000000800) != 0)
				ADC.TextMode.Write (Kernel.String ("SEP "));

			if ((edx & 0x000004000) != 0)
				ADC.TextMode.Write (Kernel.String ("MTRR "));

			if ((edx & 0x000002000) != 0)
				ADC.TextMode.Write (Kernel.String ("PGE "));

			if ((edx & 0x000004000) != 0)
				ADC.TextMode.Write (Kernel.String ("MCA "));

			if ((edx & 0x000008000) != 0)
				ADC.TextMode.Write (Kernel.String ("CMOV "));

			if ((edx & 0x000010000) != 0)
				ADC.TextMode.Write (Kernel.String ("PAT "));

			if ((edx & 0x000020000) != 0)
				ADC.TextMode.Write (Kernel.String ("PSE36 "));

			if ((edx & 0x000100000) != 0)
				ADC.TextMode.Write (Kernel.String ("NEPP "));

			if ((edx & 0x000400000) != 0)
				ADC.TextMode.Write (Kernel.String ("MMXEXT "));

			if ((edx & 0x000800000) != 0)
				ADC.TextMode.Write (Kernel.String ("MMX "));

			if ((edx & 0x001000000) != 0)
				ADC.TextMode.Write (Kernel.String ("FXSAVE "));

			if ((edx & 0x002000000) != 0)
				ADC.TextMode.Write (Kernel.String ("FFXSAVE "));

			if ((edx & 0x020000000) != 0)
				ADC.TextMode.Write (Kernel.String ("EM64T "));

			if ((edx & 0x040000000) != 0)
				ADC.TextMode.Write (Kernel.String ("3DNOWX "));

			if ((edx & 0x080000000) != 0)
				ADC.TextMode.Write (Kernel.String ("3DNOW "));

			ADC.TextMode.RestoreAttributes ();

			ADC.TextMode.WriteLine ();
		}

		public unsafe static void WriteNoCPUID ()
		{
			ADC.TextMode.SetAttributes (TextColor.LightRed, TextColor.Black);
			ADC.TextMode.WriteLine (Kernel.String ("No CPUID!"));
			ADC.TextMode.RestoreAttributes ();
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

			TextMode.Write (Kernel.String ("CPU Vendor: "));
			TextMode.SetAttributes (TextColor.LightCyan, TextColor.Black);
			TextMode.WriteString (ebx);
			TextMode.WriteString (edx);
			TextMode.WriteString (ecx);
			TextMode.RestoreAttributes ();
			TextMode.WriteNL ();
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

			TextMode.WriteString (eax);
			TextMode.WriteString (ebx);
			TextMode.WriteString (ecx);
			TextMode.WriteString (edx);
		}

		public unsafe static void WriteBrandName ()
		{
			// Brand Name
			TextMode.Write (Kernel.String ("CPU Brand: "));
			TextMode.SetAttributes (TextColor.LightCyan, TextColor.Black);

			for (uint i = 0x80000002; i <= 0x80000004; i++)
				WriteBrandName (i);

			TextMode.RestoreAttributes ();

			TextMode.WriteNL ();
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

