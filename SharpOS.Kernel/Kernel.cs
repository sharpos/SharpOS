/**
 *  (C) 2006-2007 The SharpOS Project Team - http://www.sharpos.org
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
 * 
 *  Author: Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using SharpOS;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS
{
    public unsafe class Kernel {
        public enum ColorTypes : byte {
		Black,
		Blue,
		Green,
		Cyan,
		Red,
		Magenta,
		Brown,
		White,
		DarkGray,
		LightBlue,
		LightGreen,
		LightCyan,
		LightRed,
		LightMagenta,
		Yellow,
		BrightWhite
        }

	static int x = 0;
	static int y = 0;
	static ColorTypes foreground = ColorTypes.Yellow;
	static ColorTypes background = ColorTypes.Yellow;
	static byte attributes = 0;
	static byte oldAttributes = 0;

	public unsafe static void BootEntry ()
	{
		SetAttributes (ColorTypes.Black, ColorTypes.Green);

		WriteLine (Intro);
		WriteNL ();
		WriteCPUIDInfo ();

		x = 0;
		y = 24;

		SetAttributes (ColorTypes.Magenta, ColorTypes.Black);
		WriteLine (Footer);
		RestoreAttributes ();
	}

	public unsafe static void WriteLine (byte* message)
	{
		WriteMessage (message);
		WriteNL ();
	}

	public unsafe static void WriteMessage(byte* message)
	{
		for (int i = 0; message [i] != 0; i++)
			WriteChar (message [i]);
	}

	public unsafe static void WriteChar(byte value)
	{
		byte* video = (byte*)0xB8000;

		if (value == (byte)'\n') {
			x = 0;
			y++;
		} else {
			video += y * 160 + x * 2;

			*video++ = value;
			*video = attributes;

			x++;
		}
	}
	
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

		WriteMessage (CPU_Family);
		SetAttributes (ColorTypes.LightCyan, ColorTypes.Black);
		WriteByte ((byte)((eax >> 8) & 0x0F));
		RestoreAttributes ();
		WriteNL ();

		WriteMessage (CPU_Model);
		SetAttributes (ColorTypes.LightCyan, ColorTypes.Black);
		WriteByte ((byte)((eax >> 4) & 0x0F));
		RestoreAttributes ();
		WriteNL ();

		WriteMessage (CPU_Stepping);
		SetAttributes (ColorTypes.LightCyan, ColorTypes.Black);
		WriteByte ((byte)(eax & 0x0F));
		RestoreAttributes ();
		WriteNL ();

		WriteMessage (CPU_Flags);
		SetAttributes (ColorTypes.LightCyan, ColorTypes.Black);

		if ((edx & 0x000000001) != 0) WriteMessage (CPU_FLAGS_FPU); 
		if ((edx & 0x000000002) != 0) WriteMessage (CPU_FLAGS_VME); 
		if ((edx & 0x000000004) != 0) WriteMessage (CPU_FLAGS_DE); 
		if ((edx & 0x000000008) != 0) WriteMessage (CPU_FLAGS_PSE); 
		if ((edx & 0x000000010) != 0) WriteMessage (CPU_FLAGS_TSC); 
		if ((edx & 0x000000020) != 0) WriteMessage (CPU_FLAGS_MSR); 
		if ((edx & 0x000000040) != 0) WriteMessage (CPU_FLAGS_PAE); 
		if ((edx & 0x000000080) != 0) WriteMessage (CPU_FLAGS_MCE); 
		if ((edx & 0x000000100) != 0) WriteMessage (CPU_FLAGS_CX8); 
		if ((edx & 0x000000200) != 0) WriteMessage (CPU_FLAGS_APIC); 
		if ((edx & 0x000000800) != 0) WriteMessage (CPU_FLAGS_SEP); 
		if ((edx & 0x000004000) != 0) WriteMessage (CPU_FLAGS_MTRR); 
		if ((edx & 0x000002000) != 0) WriteMessage (CPU_FLAGS_PGE); 
		if ((edx & 0x000004000) != 0) WriteMessage (CPU_FLAGS_MCA); 
		if ((edx & 0x000008000) != 0) WriteMessage (CPU_FLAGS_CMOV); 
		if ((edx & 0x000010000) != 0) WriteMessage (CPU_FLAGS_PAT); 
		if ((edx & 0x000020000) != 0) WriteMessage (CPU_FLAGS_PSE36); 
		if ((edx & 0x000100000) != 0) WriteMessage (CPU_FLAGS_NEPP); 
		if ((edx & 0x000400000) != 0) WriteMessage (CPU_FLAGS_MMXEXT); 
		if ((edx & 0x000800000) != 0) WriteMessage (CPU_FLAGS_MMX); 
		if ((edx & 0x001000000) != 0) WriteMessage (CPU_FLAGS_FXSAVE); 
		if ((edx & 0x002000000) != 0) WriteMessage (CPU_FLAGS_FFXSAVE); 
		if ((edx & 0x020000000) != 0) WriteMessage (CPU_FLAGS_EM64T); 
		if ((edx & 0x040000000) != 0) WriteMessage (CPU_FLAGS_3DNOWX); 
		if ((edx & 0x080000000) != 0) WriteMessage (CPU_FLAGS_3DNOW); 

		RestoreAttributes ();
		WriteNL ();
        }

        public unsafe static void WriteNoCPUID ()
        {
		SetAttributes (ColorTypes.LightRed, ColorTypes.Black);
		WriteLine (NO_CPUID);
		RestoreAttributes ();
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

		WriteMessage (CPU_Vendor);
		SetAttributes (ColorTypes.LightCyan, ColorTypes.Black);
		WriteString (ebx);
		WriteString (edx);
		WriteString (ecx);
		RestoreAttributes ();
		WriteNL ();
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

		WriteString (eax);
		WriteString (ebx);
		WriteString (ecx);
		WriteString (edx);
        }

        public unsafe static void WriteBrandName ()
        {
		// Brand Name
		WriteMessage (CPU_Brand);
		SetAttributes (ColorTypes.LightCyan, ColorTypes.Black);
                
		for (uint i = 0x80000002; i <= 0x80000004; i++)
			WriteBrandName (i);
            
		RestoreAttributes ();
		WriteNL ();
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

        public unsafe static void WriteCPUIDInfo ()
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

        public unsafe static void WriteString (UInt32 value)
        {
		for (int i = 0; i < 4; i++) {
			WriteChar ((byte)(value & 0xff));
			value >>= 8;
		}
        }

        public unsafe static byte* String (string value)
        {
		return null;
        }

        public unsafe static byte* Intro = String("SharpOS v0.0.0.0.99 (http://www.sharpos.org)");
        public unsafe static byte* Footer = String("and the mandatory \"Hello World\"");
        public unsafe static byte* NO_CPUID = String("No CPUID!");
        public unsafe static byte* CPU_Vendor = String("CPU Vendor: ");
        public unsafe static byte* CPU_Brand = String("CPU Brand: ");
        public unsafe static byte* CPU_Family = String("CPU Family: ");
        public unsafe static byte* CPU_Model = String("CPU Model: ");
        public unsafe static byte* CPU_Stepping = String("CPU Stepping: ");
        public unsafe static byte* CPU_Flags = String("CPU Flags: ");
        public unsafe static byte* CPU_FLAGS_FPU = String("FPU ");
        public unsafe static byte* CPU_FLAGS_VME = String("VME ");
        public unsafe static byte* CPU_FLAGS_DE = String("DE ");
        public unsafe static byte* CPU_FLAGS_PSE = String("PSE ");
        public unsafe static byte* CPU_FLAGS_TSC = String("TSC ");
        public unsafe static byte* CPU_FLAGS_MSR = String("MSR ");
        public unsafe static byte* CPU_FLAGS_PAE = String("PAE ");
        public unsafe static byte* CPU_FLAGS_MCE = String("MCE ");
        public unsafe static byte* CPU_FLAGS_CX8 = String("CX8 ");
        public unsafe static byte* CPU_FLAGS_APIC = String("APIC ");
        public unsafe static byte* CPU_FLAGS_SEP = String("SEP ");
        public unsafe static byte* CPU_FLAGS_MTRR = String("MTRR ");
        public unsafe static byte* CPU_FLAGS_PGE = String("PGE ");
        public unsafe static byte* CPU_FLAGS_MCA = String("MCA ");
        public unsafe static byte* CPU_FLAGS_CMOV = String("CMOV ");
        public unsafe static byte* CPU_FLAGS_PAT = String("PAT ");
        public unsafe static byte* CPU_FLAGS_PSE36 = String("PSE36 ");
        public unsafe static byte* CPU_FLAGS_NEPP = String("NEPP ");
        public unsafe static byte* CPU_FLAGS_MMXEXT = String("MMXEXT ");
        public unsafe static byte* CPU_FLAGS_MMX = String("MMX ");
        public unsafe static byte* CPU_FLAGS_FXSAVE = String("FXSAVE ");
        public unsafe static byte* CPU_FLAGS_FFXSAVE = String("FFXSAVE ");
        public unsafe static byte* CPU_FLAGS_EM64T = String("EM64T ");
        public unsafe static byte* CPU_FLAGS_3DNOWX = String("3DNOWX ");
        public unsafe static byte* CPU_FLAGS_3DNOW = String("3DNOW ");

        public unsafe static void WriteNL ()
        {
		WriteChar ((byte)'\n');
        }

        public unsafe static void WriteByte (byte value)
        {
		WriteHex ((byte)(value >> 4));
		WriteHex ((byte)(value & 0x0F));
        }

	public unsafe static void WriteHex (byte value)
        {
		if (value <= 9)
			WriteChar ((byte) (48 + value));
			
		else if (value == 10)
	                WriteChar ((byte)'A');
	                
		else if (value == 11)
			WriteChar ((byte)'B');
			
		else if (value == 12)
			WriteChar ((byte)'C');
			
		else if (value == 13)
			WriteChar ((byte)'D');
			
		else if (value == 14)
	                WriteChar ((byte)'E');
	                
		else if (value == 15)
			WriteChar ((byte)'F');
			
		else
			WriteChar ((byte)'X');
        }

        public static void SetAttributes (ColorTypes _foreground, ColorTypes _background)
        {
		foreground = _foreground;
		background = _background;

		oldAttributes = attributes;
		attributes = (byte) _foreground;
		attributes += (byte) ((byte) _background << 4);
        }
        
        public static void RestoreAttributes ()
        {
		attributes = oldAttributes;
        }
    }
}

