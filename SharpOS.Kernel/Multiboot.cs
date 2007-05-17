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

		public unsafe static bool WriteMultibootInfo (uint magic, uint pointer, uint kernelStart, uint kernelEnd)
		{
			if (magic != (uint) SharpOS.Multiboot.Magic.BootLoader) {
				Screen.SetAttributes (Screen.ColorTypes.Red, Screen.ColorTypes.Black);
				Screen.WriteLine (Kernel.String ("Invalid magic number."));

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
				Screen.WriteMessage (Kernel.String ("Size = 0x"));
				Screen.WriteNumber (true, (int) mmap->Size);
				Screen.WriteMessage (Kernel.String (", Base Address = 0x"));
				Screen.WriteNumber (true, (int) mmap->BaseAddrHigh);
				Screen.WriteNumber (true, (int) mmap->BaseAddrLow);
				Screen.WriteMessage (Kernel.String (", Length = 0x"));
				Screen.WriteNumber (true, (int) mmap->LengthHigh);
				Screen.WriteNumber (true, (int) mmap->LengthLow);
				Screen.WriteMessage (Kernel.String (", Type = 0x"));
				Screen.WriteNumber (true, (int) mmap->Type);
				Screen.WriteNL ();

				// FIXME the 4 at the end is arch specific
				mmap = (Multiboot.MemoryMap*) ((uint) mmap + mmap->Size + 4);
			}
		}

		public unsafe static void WriteMultibootInfo (Multiboot.Info info, uint kernelStart, uint kernelEnd)
		{
			Screen.WriteMessage (Kernel.String ("Kernel Image: 0x"));
			Screen.WriteNumber (true, (int) kernelStart);
			Screen.WriteMessage (Kernel.String (" - 0x"));
			Screen.WriteNumber (true, (int) kernelEnd);
			Screen.WriteNL ();

			Screen.WriteMessage (Kernel.String ("Boot Drive: 0x"));
			Screen.WriteNumber (true, (int) info.BootDevice);
			Screen.WriteNL ();

			Screen.WriteMessage (Kernel.String ("Flags: 0x"));
			Screen.WriteNumber (true, (int) info.Flags);
			Screen.WriteNL ();

			Screen.WriteMessage (Kernel.String ("Command Line: "));
			Screen.WriteMessage ((byte*) info.CmdLine);
			Screen.WriteNL ();

			if ((info.Flags & 0x01) != 0) {
				Screen.WriteMessage (Kernel.String ("Memory Lower: 0x"));
				Screen.WriteNumber (true, (int) info.MemLower);
				Screen.WriteNL ();

				Screen.WriteMessage (Kernel.String ("Memory Upper: 0x"));
				Screen.WriteNumber (true, (int) info.MemUpper);
				Screen.WriteNL ();
			}

			if ((info.Flags & 0x40) != 0) {
				Screen.WriteMessage (Kernel.String ("MMap Address: 0x"));
				Screen.WriteNumber (true, (int) info.MMapAddr);
				Screen.WriteNL ();

				Screen.WriteMessage (Kernel.String ("MMap Length: 0x"));
				Screen.WriteNumber (true, (int) info.MMapLen);
				Screen.WriteNL ();

				WriteMultibootInfoMMap (info);
			}

			Screen.WriteNL ();
		}
	}
}
