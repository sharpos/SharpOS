// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System.Runtime.InteropServices;
using SharpOS.ADC;

namespace SharpOS {
	public unsafe class Multiboot {
		public enum Magic : uint {
			Header = 0x1BADB002,
			BootLoader = 0x2BADB002
		}
		
		[StructLayout (LayoutKind.Sequential)]
		public unsafe struct Header {
			public uint Magic;
			public uint Flags;
			public uint Checksum;
			public uint HeaderAddr;
			public uint LoadAddr;
			public uint LoadEndAddr;
			public uint BSSEndAddr;
			public uint EntryAddr;
		}

		[StructLayout (LayoutKind.Sequential)]
		public unsafe struct AOUTSymbolTable
		{
			public uint TabSize;
			public uint StrSize;
			public uint Addr;
			public uint Reserved;
		}

		[StructLayout (LayoutKind.Sequential)]
		public unsafe struct ElfSectionHeaderTable
		{
			public uint Num;
			public uint Size;
			public uint Addr;
			public uint Shndx;
		}
		
		/* The Multiboot information. */
		[StructLayout (LayoutKind.Explicit)]
		public unsafe struct Info
		{
			[FieldOffset(0)]
			public uint Flags;
			[FieldOffset(4)]
			public uint MemLower;
			[FieldOffset(8)]
			public uint MemUpper;
			[FieldOffset(12)]
			public uint BootDevice;
			[FieldOffset(16)]
			public uint CmdLine;
			[FieldOffset(20)]
			public uint ModsCount;
			[FieldOffset(24)]
			public uint ModsAddr;
			
			[FieldOffset(28)]
			public AOUTSymbolTable AOUTSym;
			[FieldOffset(28)]
			public ElfSectionHeaderTable ElfSec;
				
			[FieldOffset(44)]
			public uint MMapLen;
			[FieldOffset(48)]
			public uint MMapAddr;
		}

		[StructLayout (LayoutKind.Sequential)]
		public unsafe struct Module
		{
			public uint ModStart;
			public uint ModEnd;
			public uint String;
			public uint Reserved;
		}
		
		///
		/// <remarks>
		/// Be careful that the offset 0 is base_addr_low
		/// but no size. 
		/// </remarks>
		[StructLayout (LayoutKind.Sequential)]
		public struct MemoryMap
		{
			public uint Size;
			public uint BaseAddrLow;
			public uint BaseAddrHigh;
			public uint LengthLow;
			public uint LengthHigh;
			public uint Type;
		}

        public unsafe static Multiboot.Info* LoadMultibootInfo(uint magic, uint pointer, uint kernelStart, uint kernelEnd)
        {
            if (magic != (uint)SharpOS.Multiboot.Magic.BootLoader)
            {
                TextMode.SetAttributes(TextColor.Red, TextColor.Black);
                TextMode.WriteLine("LoadMultibootInfo() - invalid magic number");

                return null;
            }
            else
            {
                return (Multiboot.Info*)pointer;
            }
        }

		public unsafe static void WriteMultibootInfoMMap (Multiboot.Info* info)
		{
			Multiboot.MemoryMap* mmap = (Multiboot.MemoryMap*) info->MMapAddr;

			while ((uint) mmap < info->MMapAddr + info->MMapLen) {
				TextMode.Write ("Size = 0x");
				TextMode.Write ((int) mmap->Size, true);
				TextMode.Write (", Base Address = 0x");
				TextMode.Write ((int) mmap->BaseAddrHigh, true);
				TextMode.Write ((int) mmap->BaseAddrLow, true);
				TextMode.Write (", Length = 0x");
				TextMode.Write ((int) mmap->LengthHigh, true);
				TextMode.Write ((int) mmap->LengthLow, true);
				TextMode.Write (", Type = 0x");
				TextMode.Write ((int) mmap->Type, true);
				TextMode.WriteLine ();

				// FIXME the 4 at the end is arch specific
				mmap = (Multiboot.MemoryMap*) ((uint) mmap + mmap->Size + 4);
			}
		}

		public unsafe static void WriteMultibootInfo (Multiboot.Info* info, uint kernelStart, uint kernelEnd)
		{
			TextMode.Write ("Kernel Image: 0x");
			TextMode.Write ((int) kernelStart, true);
			TextMode.Write (" - 0x");
			TextMode.Write ((int) kernelEnd, true);
			TextMode.WriteLine ();

			TextMode.Write ("Boot Drive: 0x");
			TextMode.Write ((int) info->BootDevice, true);
			TextMode.WriteLine ();

			TextMode.Write ("Flags: 0x");
			TextMode.Write ((int) info->Flags, true);
			TextMode.WriteLine ();

			TextMode.Write ("Command Line: ");
			TextMode.Write ((byte*) info->CmdLine);
			TextMode.WriteLine ();

			if ((info->Flags & 0x01) != 0) {
				TextMode.Write ("Memory Lower: 0x");
				TextMode.Write ((int) info->MemLower, true);
				TextMode.WriteLine ();

				TextMode.Write ("Memory Upper: 0x");
				TextMode.Write ((int) info->MemUpper, true);
				TextMode.WriteLine ();
			}

			if ((info->Flags & 0x40) != 0) {
				TextMode.Write ("MMap Address: 0x");
				TextMode.Write ((int) info->MMapAddr, true);
				TextMode.WriteLine ();

				TextMode.Write ("MMap Length: 0x");
				TextMode.Write ((int) info->MMapLen, true);
				TextMode.WriteLine ();

				WriteMultibootInfoMMap (info);
			}

			TextMode.WriteLine ();
		}
	}
}
