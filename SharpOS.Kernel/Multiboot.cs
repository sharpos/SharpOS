// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using Interop = System.Runtime.InteropServices;

namespace SharpOS.Multiboot {
	public enum Magic : uint {
		Header = 0x1BADB002,
		BootLoader = 0x2BADB002
	}
	
	[Interop.StructLayout(Interop.LayoutKind.Sequential)]
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
	
	public unsafe struct AOUTSymbolTable
	{
		public uint TabSize;
		public uint StrSize;
		public uint Addr;
		public uint Reserved;
	}
	
	public unsafe struct ElfSectionHeaderTable
	{
		public uint Num;
		public uint Size;
		public uint Addr;
		public uint Shndx;
	}
	
	/* The Multiboot information. */
	[Interop.StructLayout(Interop.LayoutKind.Explicit)]
	public unsafe struct Info
	{
		[Interop.FieldOffset(0)]
		public uint Flags;
		[Interop.FieldOffset(4)]
		public uint MemLower;
		[Interop.FieldOffset(8)]
		public uint MemUpper;
		[Interop.FieldOffset(12)]
		public uint BootDevice;
		[Interop.FieldOffset(16)]
		public uint CmdLine;
		[Interop.FieldOffset(20)]
		public uint ModsCount;
		[Interop.FieldOffset(24)]
		public uint ModsAddr;
		
		[Interop.FieldOffset(32)]
		public AOUTSymbolTable AOUTSym;
		/*[Interop.FieldOffset(32)]
		public ElfSectionHeaderTable ElfSec;*/
			
		[Interop.FieldOffset(36)]
		public uint MMapLen;
		[Interop.FieldOffset(40)]
		public uint MMapAddr;
	}
	
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
