// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

//#define DISPLAY_GDT_SETUP_SUMMARY

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using ADC = SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.ADC.X86 {
	/// <summary>
	/// The Global Descriptor Table (GDT) is a data structure used by Intel x86-family 
	/// processors starting with the 80286 in order to define the characteristics of the
	/// various memory areas used during program execution, for example the base address, 
	/// the size and access privileges like executability and writability. 
	/// These memory areas are called segments in Intel terminology.
	/// </summary>
	public unsafe class GDT {
		private const ushort GDTEntries = 3;
		private const ushort SystemSelector = 0;
		public const ushort CodeSelector = 8;
		public const ushort DataSelector = 16;

		private static DTPointer* gdtPointer = (DTPointer*) Stubs.LabelledAlloc ("GDTPointer", DTPointer.SizeOf);
		private static Entry* gdt = (Entry*) Stubs.StaticAlloc (Entry.SizeOf * GDTEntries);

		[StructLayout (LayoutKind.Sequential)]
		public struct Entry {
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

		internal static void Setup ()
		{
			gdtPointer->Setup ((ushort) (sizeof (Entry) * GDTEntries - 1), (uint) gdt);

#if DISPLAY_GDT_SETUP_SUMMARY // TO TOGGLE, REFER TO TOP OF FILE
			ADC.TextMode.Write ("GDT Pointer: 0x");
			ADC.TextMode.Write ((int) gdtPointer->Address, true);
			ADC.TextMode.Write (" - 0x");
			ADC.TextMode.Write (gdtPointer->Size, true);
			ADC.TextMode.WriteLine ();
#endif

			gdt [SystemSelector >> 3].Setup (0, 0, 0);

			// Code Segment
			gdt [CodeSelector >> 3].Setup (0, 0xFFFFFFFF, (ushort) (
				Entry.Type.Granularity_4K |
				Entry.Type.OperandSize_32Bit |
				Entry.Type.Present |
				Entry.Type.Descriptor |
				Entry.Type.Executable |
				Entry.Type.Writable));

			// Data Segment
			gdt [DataSelector >> 3].Setup (0, 0xFFFFFFFF, (ushort) (
				Entry.Type.Granularity_4K |
				Entry.Type.OperandSize_32Bit |
				Entry.Type.Present |
				Entry.Type.Descriptor |
				Entry.Type.Writable));

			Asm.LGDT (new SharpOS.AOT.X86.Memory ("GDTPointer"));

			Asm.MOV (R16.AX, DataSelector);
			Asm.MOV (Seg.DS, R16.AX);
			Asm.MOV (Seg.ES, R16.AX);
			Asm.MOV (Seg.FS, R16.AX);
			Asm.MOV (Seg.GS, R16.AX);
			Asm.MOV (Seg.SS, R16.AX);

			Asm.JMP (CodeSelector, "Kernel_GDT_Entry_Point");
			Asm.LABEL ("Kernel_GDT_Entry_Point");
		}
	}
}

