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

namespace SharpOS.Multiboot {
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
}
