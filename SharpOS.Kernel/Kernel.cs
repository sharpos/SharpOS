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
using System.Runtime.InteropServices;
using SharpOS;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS {
	[StructLayout (LayoutKind.Sequential)]
	public struct DTPointer {
		public const uint SizeOf = 6;

		public ushort Size;
		public uint Address;

		public void Setup (ushort size, uint address)
		{
			this.Size = size;
			this.Address = address;
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct GDTEntry {
		public enum Type : ushort {
			Accessed = 1,
			Writable = 2,
			Expansion = 4,
			Executable = 8,
			Descriptor = 16,
			Privilege_Ring_0 = 0,
			Privilege_Ring_1 = 32,
			Privilege_Ring_2 = 64,
			Privilege_Ring_3 = 96,
			Present = 128,
			OperandSize_16Bit = 0,
			OperandSize_32Bit = 1024,
			Granularity_Byte = 0,
			Granularity_4K = 2048
		}

		public const uint SizeOf = 8;

		public ushort LimitLow;
		public ushort BaseLow;
		public byte BaseMiddle;
		public byte Access;
		public byte Granularity;
		public byte BaseHigh;

		public void Setup (uint _base, uint _limit, ushort flags)
		{
			this.BaseLow = (ushort) (_base & 0xFFFF);
			this.BaseMiddle = (byte) ((_base >> 16) & 0xFF);
			this.BaseHigh = (byte) ((_base >> 24) & 0xFF);

			// The limits
			this.LimitLow = (ushort) (_limit & 0xFFFF);
			this.Granularity = (byte) ((_limit >> 16) & 0x0F);

			// Granularity and Access
			this.Granularity |= (byte) ((flags >> 4) & 0xF0);
			this.Access = (byte) (flags & 0xFF);
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct IDTEntry {
		public enum Type : ushort {
			Call_Gate = 4,
			Task_Gate = 5,
			Interrupt_Gate = 6,
			Trap_Gate = 7,
			OperandSize_16Bit = 0,
			OperandSize_32Bit = 8,
			Privilege_Ring_0 = 0,
			Privilege_Ring_1 = 32,
			Privilege_Ring_2 = 64,
			Privilege_Ring_3 = 96,
			Present = 128,

		}

		public const uint SizeOf = 8;

		public ushort OffsetLow;
		public ushort Selector;
		public byte Unused;
		public byte Options;
		public ushort OffsetHigh;

		public void Setup (ushort selector, uint offset, byte options)
		{
			this.Selector = selector;
			this.OffsetLow = (ushort) (offset & 0xFFFF);
			this.OffsetHigh = (byte) ((offset >> 16) & 0xFFFF);
			this.Options = options;
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct ISRData {
		public uint SS;
		public uint FS;
		public uint GS;
		public uint ES;
		public uint DS;
		public uint EDI;
		public uint ESI;
		public uint EBP;
		public uint ESP;
		public uint EBX;
		public uint EDX;
		public uint ECX;
		public uint EAX;
		public uint Index;
		public uint EIP;
		public uint CS;
	}

	public unsafe partial class Kernel {
		#region GDT
		private const ushort GDTEntries = 3;
		private const ushort SystemSelector = 0;
		private const ushort CodeSelector = 8;
		private const ushort DataSelector = 16;

		private static DTPointer* gdtPointer = (DTPointer*) LabelledAlloc ("GDTPointer", DTPointer.SizeOf);
		private static GDTEntry* gdt = (GDTEntry*) Alloc (GDTEntry.SizeOf * GDTEntries);

		internal static void SetupGDT ()
		{
			gdtPointer->Setup ((ushort) (sizeof (GDTEntry) * GDTEntries - 1), (uint) gdt);

			WriteMessage (String ("GDT Pointer: 0x"));
			WriteNumber (true, (int) gdtPointer->Address);
			WriteMessage (String (" - 0x"));
			WriteNumber (true, gdtPointer->Size);
			WriteNL ();

			gdt [SystemSelector >> 3].Setup (0, 0, 0);

			// Code Segment
			gdt [CodeSelector >> 3].Setup (0, 0xFFFFFFFF, (ushort) (
				GDTEntry.Type.Granularity_4K |
				GDTEntry.Type.OperandSize_32Bit |
				GDTEntry.Type.Present |
				GDTEntry.Type.Descriptor |
				GDTEntry.Type.Executable |
				GDTEntry.Type.Writable)); 

			// Data Segment
			gdt [DataSelector >> 3].Setup (0, 0xFFFFFFFF, (ushort) (
				GDTEntry.Type.Granularity_4K |
				GDTEntry.Type.OperandSize_32Bit |
				GDTEntry.Type.Present |
				GDTEntry.Type.Descriptor |
				GDTEntry.Type.Writable));

			Asm.LGDT (new Memory ("GDTPointer"));

			Asm.MOV (R16.AX, DataSelector);
			Asm.MOV (Seg.DS, R16.AX);
			Asm.MOV (Seg.ES, R16.AX);
			Asm.MOV (Seg.FS, R16.AX);
			Asm.MOV (Seg.GS, R16.AX);
			Asm.MOV (Seg.SS, R16.AX);

			Asm.JMP (CodeSelector, "Kernel_GDT_Entry_Point");
			Asm.LABEL ("Kernel_GDT_Entry_Point");
		}
		#endregion

		#region IDT
		private const ushort IDTEntries = 256;

		private static DTPointer* idtPointer = (DTPointer*) LabelledAlloc ("IDTPointer", DTPointer.SizeOf);
		private static IDTEntry* idt = (IDTEntry*) Alloc (IDTEntry.SizeOf * IDTEntries);

		internal static void SetupIDT ()
		{
			idtPointer->Setup ((ushort) (sizeof (IDTEntry) * IDTEntries - 1), (uint) idt);

			WriteMessage (String ("IDT Pointer: 0x"));
			WriteNumber (true, (int) idtPointer->Address);
			WriteMessage (String (" - 0x"));
			WriteNumber (true, idtPointer->Size);
			WriteNL ();

			SetupISR ();

			Asm.LIDT (new Memory ("IDTPointer"));

			Asm.STI ();

			// Breakpoint - Testing the IDT
			//Asm.INT (3);
		}

		private static unsafe void ISRDispatcher (ISRData data)
		{
			x = 0;
			y = 0;

			WriteLine (String ("IDT 0x"), (int) data.Index);
			WriteLine (String ("EIP 0x"), (int) data.EIP);

			WriteLine (String ("EAX 0x"), (int) data.EAX);
			WriteLine (String ("ECX 0x"), (int) data.ECX);
			WriteLine (String ("EDX 0x"), (int) data.EDX);
			WriteLine (String ("EBX 0x"), (int) data.EBX);

			WriteLine (String ("ESP 0x"), (int) data.ESP);
			WriteLine (String ("EBP 0x"), (int) data.EBP);
			WriteLine (String ("ESI 0x"), (int) data.ESI);
			WriteLine (String ("EDI 0x"), (int) data.EDI);

			WriteLine (String ("DS 0x"), (int) data.DS);
			WriteLine (String ("ES 0x"), (int) data.ES);
			WriteLine (String ("FS 0x"), (int) data.FS);
			WriteLine (String ("GS 0x"), (int) data.GS);
			WriteLine (String ("SS 0x"), (int) data.SS);
			WriteLine (String ("CS 0x"), (int) data.CS);

			Asm.HLT ();
		}
		#endregion

		#region PIC
		private const byte MasterPICBase = 0x20;
		private const ushort MasterPICCommandPort = 0x20;
		private const ushort MasterPICDataPort = 0x21;

		private const byte SlavePICBase = 0x28;
		private const ushort SlavePICCommandPort = 0x70;
		private const ushort SlavePICDataPort = 0x71;

		public unsafe static byte inb (ushort port)
		{
			byte value = 0;

			Asm.XOR (R32.EAX, R32.EAX);
			Asm.MOV (R16.DX, &port);
			Asm.IN_AL__DX ();
			Asm.MOV (&value, R8.AL);

			return value;
		}

		public unsafe static ushort inw (ushort port)
		{
			ushort value = 0;

			Asm.XOR (R32.EAX, R32.EAX);
			Asm.MOV (R16.DX, &port);
			Asm.IN_AX__DX ();
			Asm.MOV (&value, R16.AX);

			return value;
		}

		public unsafe static uint inl (ushort port)
		{
			uint value = 0;

			Asm.XOR (R32.EAX, R32.EAX);
			Asm.MOV (R16.DX, &port);
			Asm.IN_EAX__DX ();
			Asm.MOV (&value, R32.EAX);

			return value;
		}

		public unsafe static void outb (ushort port, byte value)
		{
			Asm.MOV (R16.DX, &port);
			Asm.MOV (R8.AL, &value);
			Asm.OUT_DX__AL ();
		}

		public unsafe static void outw (ushort port, ushort value)
		{
			Asm.MOV (R16.DX, &port);
			Asm.MOV (R16.AX, &value);
			Asm.OUT_DX__AX ();
		}

		public unsafe static void outl (ushort port, uint value)
		{
			Asm.MOV (R16.DX, &port);
			Asm.MOV (R32.EAX, &value);
			Asm.OUT_DX__EAX ();
		}

		public unsafe static void iodelay ()
		{
			Asm.IN_AL (0x80);
			Asm.OUT__AL (0x80);
		}

		internal static void SetupPIC ()
		{
			byte masterMask = inb (MasterPICDataPort);
			byte slaveMask = inb (SlavePICDataPort);

			outb (MasterPICDataPort, masterMask);
			outb (SlavePICDataPort, slaveMask);
		}
		#endregion

		public unsafe static void BootEntry (uint magic, uint pointer, uint kernelStart, uint kernelEnd)
		{
			SetAttributes (ColorTypes.Yellow, ColorTypes.Black);

			WriteLine (String ("SharpOS v0.0.0.75 (http://www.sharpos.org)"));
			WriteNL ();

			SetupGDT ();
			SetupPIC ();
			SetupIDT ();

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
		static ColorTypes background = ColorTypes.Black;
		static byte attributes = 0;
		static byte oldAttributes = 0;

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

		public unsafe static void WriteLine (byte* message, int value)
		{
			WriteMessage (message);
			WriteNumber (true, value);
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

