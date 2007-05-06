// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.Collections.Generic;
using System.Text;
using SharpOS;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS {
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

		#region Structs
		/*public struct TestStruct {
			public uint FirstValue;
			public byte SecondValue;

			public int TestStructMethod (int value)
			{
				return (int) (this.FirstValue + this.SecondValue + value);
			}
		}

		public unsafe static int TestStructProc (TestStruct value)
		{
			return (int) (value.FirstValue + value.SecondValue);
		}

		public unsafe static int NewTestStruct ()
		{
			TestStruct* values = stackalloc TestStruct [10];
			values [3].FirstValue = 1;
			values [3].SecondValue = 2;

			int result = TestStructProc (values [3]);

			result += values [3].TestStructMethod (2);

			return result;
		}*/
		#endregion

		private static byte* gdt = Alloc (1024);
		private static byte* idt = Alloc (256);


		public unsafe static void BootEntry (uint magic, uint pointer, uint kernelStart, uint kernelEnd)
		{
			//x = NewTestStruct ();

			SetAttributes (ColorTypes.Yellow, ColorTypes.Black);

			WriteLine (String ("SharpOS v0.0.0.75 (http://www.sharpos.org)"));
			WriteNL ();

			if (!WriteMultibootInfo (magic, pointer, kernelStart, kernelEnd))
				return;
			
			WriteCPUIDInfo ();

			x = 0;
			y = 23;

			SetAttributes (ColorTypes.LightGreen, ColorTypes.Black);
			WriteLine (String ("Pinky: What are we gonna do tonight, Brain?"));
			WriteLine (String ("The Brain: The same thing we do every night, Pinky - Try to take over the world!"));
			RestoreAttributes ();
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
		#endregion

		#region Multiboot
		public unsafe static bool WriteMultibootInfo (uint magic, uint pointer, uint kernelStart, uint kernelEnd)
		{
			if (magic != (uint) SharpOS.Multiboot.Magic.BootLoader) {
				SetAttributes (ColorTypes.Red, ColorTypes.Black);
				WriteLine (String ("Invalid magic number."));

				return false;
			}

			Multiboot.Info* multibootInfo = (Multiboot.Info*) pointer;

			WriteMultibootInfo (*multibootInfo, kernelStart, kernelEnd);

			return true;
		}

		public unsafe static void WriteMultibootInfoMMap (Multiboot.Info info)
		{
			Multiboot.MemoryMap* mmap = (Multiboot.MemoryMap*) info.MMapAddr;

			while ((uint) mmap < info.MMapAddr + info.MMapLen) {
				WriteMessage (String ("Size = 0x"));
				WriteNumber (true, (int) mmap->Size);
				WriteMessage (String (", Base Address = 0x"));
				WriteNumber (true, (int) mmap->BaseAddrHigh);
				WriteNumber (true, (int) mmap->BaseAddrLow);
				WriteMessage (String (", Length = 0x"));
				WriteNumber (true, (int) mmap->LengthHigh);
				WriteNumber (true, (int) mmap->LengthLow);
				WriteMessage (String (", Type = 0x"));
				WriteNumber (true, (int) mmap->Type);
				WriteNL ();

				// FIXME the 4 at the end is arch specific
				mmap = (Multiboot.MemoryMap*) ((uint) mmap + mmap->Size + 4);
			}
		}

		public unsafe static void WriteMultibootInfo (Multiboot.Info info, uint kernelStart, uint kernelEnd)
		{
			WriteMessage (String ("Kernel Image: 0x"));
			WriteNumber (true, (int) kernelStart);
			WriteMessage (String (" - 0x"));
			WriteNumber (true, (int) kernelEnd);
			WriteNL ();

			WriteMessage (String ("Boot Drive: 0x"));
			WriteNumber (true, (int) info.BootDevice);
			WriteNL ();

			WriteMessage (String ("Flags: 0x"));
			WriteNumber (true, (int) info.Flags);
			WriteNL ();

			WriteMessage (String ("Command Line: "));
			WriteMessage ((byte*) info.CmdLine);
			WriteNL ();

			if ((info.Flags & 0x01) != 0) {
				WriteMessage (String ("Memory Lower: 0x"));
				WriteNumber (true, (int) info.MemLower);
				WriteNL ();

				WriteMessage (String ("Memory Upper: 0x"));
				WriteNumber (true, (int) info.MemUpper);
				WriteNL ();
			}

			if ((info.Flags & 0x40) != 0) {
				WriteMessage (String ("MMap Address: 0x"));
				WriteNumber (true, (int) info.MMapAddr);
				WriteNL ();

				WriteMessage (String ("MMap Length: 0x"));
				WriteNumber (true, (int) info.MMapLen);
				WriteNL ();

				WriteMultibootInfoMMap (info);
			}

			WriteNL ();
		}
		#endregion

		#region CPUID
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

			WriteMessage (String ("CPU Family: "));
			SetAttributes (ColorTypes.LightCyan, ColorTypes.Black);
			WriteByte ((byte) ((eax >> 8) & 0x0F));
			RestoreAttributes ();
			WriteNL ();

			WriteMessage (String ("CPU Model: "));
			SetAttributes (ColorTypes.LightCyan, ColorTypes.Black);
			WriteByte ((byte) ((eax >> 4) & 0x0F));
			RestoreAttributes ();
			WriteNL ();

			WriteMessage (String ("CPU Stepping: "));
			SetAttributes (ColorTypes.LightCyan, ColorTypes.Black);
			WriteByte ((byte) (eax & 0x0F));
			RestoreAttributes ();
			WriteNL ();

			WriteMessage (String ("CPU Flags: "));
			SetAttributes (ColorTypes.LightCyan, ColorTypes.Black);

			if ((edx & 0x000000001) != 0)
				WriteMessage (String ("FPU "));

			if ((edx & 0x000000002) != 0)
				WriteMessage (String ("VME "));

			if ((edx & 0x000000004) != 0)
				WriteMessage (String ("DE "));

			if ((edx & 0x000000008) != 0)
				WriteMessage (String ("PSE "));

			if ((edx & 0x000000010) != 0)
				WriteMessage (String ("TSC "));

			if ((edx & 0x000000020) != 0)
				WriteMessage (String ("MSR "));

			if ((edx & 0x000000040) != 0)
				WriteMessage (String ("PAE "));

			if ((edx & 0x000000080) != 0)
				WriteMessage (String ("MCE "));

			if ((edx & 0x000000100) != 0)
				WriteMessage (String ("CX8 "));

			if ((edx & 0x000000200) != 0)
				WriteMessage (String ("APIC "));

			if ((edx & 0x000000800) != 0)
				WriteMessage (String ("SEP "));

			if ((edx & 0x000004000) != 0)
				WriteMessage (String ("MTRR "));

			if ((edx & 0x000002000) != 0)
				WriteMessage (String ("PGE "));

			if ((edx & 0x000004000) != 0)
				WriteMessage (String ("MCA "));

			if ((edx & 0x000008000) != 0)
				WriteMessage (String ("CMOV "));

			if ((edx & 0x000010000) != 0)
				WriteMessage (String ("PAT "));

			if ((edx & 0x000020000) != 0)
				WriteMessage (String ("PSE36 "));

			if ((edx & 0x000100000) != 0)
				WriteMessage (String ("NEPP "));

			if ((edx & 0x000400000) != 0)
				WriteMessage (String ("MMXEXT "));

			if ((edx & 0x000800000) != 0)
				WriteMessage (String ("MMX "));

			if ((edx & 0x001000000) != 0)
				WriteMessage (String ("FXSAVE "));

			if ((edx & 0x002000000) != 0)
				WriteMessage (String ("FFXSAVE "));

			if ((edx & 0x020000000) != 0)
				WriteMessage (String ("EM64T "));

			if ((edx & 0x040000000) != 0)
				WriteMessage (String ("3DNOWX "));

			if ((edx & 0x080000000) != 0)
				WriteMessage (String ("3DNOW "));

			RestoreAttributes ();

			WriteNL ();
		}

		public unsafe static void WriteNoCPUID ()
		{
			SetAttributes (ColorTypes.LightRed, ColorTypes.Black);
			WriteLine (String ("No CPUID!"));
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

			WriteMessage (String ("CPU Vendor: "));
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
			WriteMessage (String ("CPU Brand: "));
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
		#endregion

		#region Display Routines
		public unsafe static void WriteNumber (bool hex, int value)
		{
			byte* buffer = stackalloc byte [32];
			uint uvalue = (uint) value;
			ushort divisor = hex ? (ushort) 16 : (ushort) 10;
			int length = 0;

			if (!hex && value < 0) {
				buffer [length++] = (byte) '-';

				uvalue = (uint) -value;
			}

			do {
				uint remainder = uvalue % divisor;

				if (remainder < 10)
					buffer [length++] = (byte) ('0' + remainder);

				else
					buffer [length++] = (byte) ('A' + remainder - 10);

			} while ((uvalue /= divisor) != 0);

			while (length > 0)
				WriteChar (buffer [--length]);
		}

		public unsafe static void WriteLine (byte* message)
		{
			WriteMessage (message);
			WriteNL ();
		}

		public unsafe static void WriteMessage (byte* message)
		{
			for (int i = 0; message [i] != 0; i++)
				WriteChar (message [i]);
		}

		public unsafe static void WriteChar (byte value)
		{
			byte* video = (byte*) 0xB8000;

			if (value == (byte) '\n') {
				x = 0;
				y++;

			} else {
				video += y * 160 + x * 2;

				*video++ = value;
				*video = attributes;

				x++;
			}
		}
		public unsafe static void WriteString (UInt32 value)
		{
			for (int i = 0; i < 4; i++) {
				WriteChar ((byte) (value & 0xff));
				value >>= 8;
			}
		}
		public unsafe static void WriteNL ()
		{
			WriteChar ((byte) '\n');
		}

		public unsafe static void WriteByte (byte value)
		{
			WriteHex ((byte) (value >> 4));
			WriteHex ((byte) (value & 0x0F));
		}

		public unsafe static void WriteHex (byte value)
		{
			if (value <= 9)
				WriteChar ((byte) (48 + value));

			else if (value == 10)
				WriteChar ((byte) 'A');

			else if (value == 11)
				WriteChar ((byte) 'B');

			else if (value == 12)
				WriteChar ((byte) 'C');

			else if (value == 13)
				WriteChar ((byte) 'D');

			else if (value == 14)
				WriteChar ((byte) 'E');

			else if (value == 15)
				WriteChar ((byte) 'F');

			else
				WriteChar ((byte) 'X');
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
		#endregion
	}
}

