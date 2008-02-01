// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.Kernel.ADC;
using System.Runtime.InteropServices;
using System;
using SharpOS.Kernel.Foundation;

namespace SharpOS.Kernel {
	public unsafe class Multiboot {
		
		#region Internal Structures

		#region Identifiers
		private enum Identifiers : uint {
			Header		= 0x1BADB002,
			BootLoader	= 0x2BADB002
		}
		#endregion
		
		#region HeaderFlags
		[Flags]
		private enum HeaderFlags : uint {
			AlignModulesOn4kb		= 1 << 0,
			RequireAvailableMemory	= 1 << 1,
			RequireVideoModeTable	= 1 << 2,
			UseHeaderAddress		= 1 << 16
		}
		#endregion
		
		#region VideoMode
		private enum VideoMode : uint {
			LinearGraphicsMode		= 0,
			EGAStandardTextMode		= 1
		}
		#endregion
		
		#region Header
		/// <summary>
		/// The Multiboot header
		/// </summary>
		[StructLayout (LayoutKind.Sequential)]
		private unsafe struct Header {
			public uint			Magic;
			public HeaderFlags	Flags;
			public uint			Checksum;
			
			// only set if UseHeaderAddress is set in Flags
			public uint			HeaderAddress;
			public uint			LoadAddress;
			public uint			LoadEndAddress;
			public uint			BSSEndAddress;
			public uint			EntryAddress;

			// only set if RequireVideoModeTable is set in Flags
			public VideoMode	VideoModeType;
			public uint			VideoModeWidth;
			public uint			VideoModeHeight;
			public uint			VideoModeDepth;
		}
		#endregion
		
		#region Module
		[StructLayout (LayoutKind.Sequential)]
		private unsafe struct Module {
			public uint ModuleStart;
			public uint ModuleEnd;
			public uint String;
			public uint Reserved;
		}
		#endregion
		
		#region MemoryMap
		/// <summary>
		/// Memory Map
		/// </summary>
		/// <remarks>
		/// Be careful that the offset 0 is base_addr_low
		/// but no size. 
		/// </remarks>
		[StructLayout (LayoutKind.Sequential)]
		private struct MemoryMap {
			public uint Size;
			public uint BaseAddressLow;
			public uint BaseAddressHigh;
			public uint LengthLow;
			public uint LengthHigh;
			public uint Type;
		}
		#endregion
		
		#region Drive
		[StructLayout (LayoutKind.Sequential)]
		private struct Drive {
			public uint		Size;
			public byte		DriveNumber;
			public byte		DriveMode;
			public ushort	DriveCylinders;
			public byte		DriveHeads;
			public byte		DriveSectors;
			public ushort	DrivePorts;	// more ports may follow, use size to find out how many
		}
		#endregion
		
		#region AOUTSymbolTable
		[StructLayout (LayoutKind.Sequential)]
		private unsafe struct AOUTSymbolTable {
			public uint TabSize;
			public uint StrSize;
			public uint Address;
			public uint Reserved;
		}
		#endregion
		
		#region ElfSectionHeaderTable
		[StructLayout (LayoutKind.Sequential)]
		private unsafe struct ElfSectionHeaderTable {
			public uint Num;
			public uint Size;
			public uint Address;
			public uint Shndx;
		}
		#endregion
		
		#region InfoFlags
		[Flags]
		private enum InfoFlags : uint {
			MemorySizeSet				= 1 << 0,
			BootDeviceSet				= 1 << 1,
			CommandLineSet				= 1 << 2,
			ModulesSet					= 1 << 3,
			AOUTKernelSymbolTableSet	= 1 << 4,
			ELFKernelSectionHeaderSet	= 1 << 5,
			MemoryMapSet				= 1 << 6,
			DrivesSet					= 1 << 7,
			ConfigTableSet				= 1 << 8,
			BootLoaderNameSet			= 1 << 9,
			APMTableSet					= 1 << 10,
			GraphicsTableSet			= 1 << 11,
		};	
		#endregion		
			
		#region InformationTable
		/// <summary>
		/// The Multiboot information
		/// </summary>
		[StructLayout (LayoutKind.Explicit)]
		private unsafe struct InformationTable {
			[FieldOffset ( 0)] public InfoFlags Flags;
			
			// values set when MemorySizeSet is set in Flags
			[FieldOffset ( 4)] public uint LowerMemorySize;
			[FieldOffset ( 8)] public uint UpperMemorySize;
			
			// values set when BootDeviceSet is set in Flags
			[FieldOffset (12)] public uint BootDevice;
			
			// value set when CommandLineSet is set in Flags
			[FieldOffset (16)] public uint _commandLine;

			// values set when ModulesSet is set in Flags
			[FieldOffset (20)] public uint ModulesCount;
			[FieldOffset (24)] public uint ModulesAddress;
			
			// values set when AOUTKernelSymbolTableSet is set in Flags
			[FieldOffset (28)] public AOUTSymbolTable AOUTSymbolTable;

			// values set when ELFKernelSectionHeaderSet is set in Flags
			[FieldOffset (28)] public ElfSectionHeaderTable ElfSectionHeader;
			
			// values set when MemoryMapSet is set in Flags
			[FieldOffset (44)] public uint MMapLength;
			[FieldOffset (48)] public uint MMapAddress;
			
			// values set when DrivesSet is set in Flags
			[FieldOffset (52)] public uint DrivesLength;
			[FieldOffset (56)] public uint DrivesAddress;
			
			// values set when ConfigTableSet is set in Flags
			[FieldOffset (60)] public uint ConfigTable;
			
			// values set when BootLoaderNameSet is set in Flags
			[FieldOffset (64)] public uint BootLoaderName;
			
			// values set when APMTableSet is set in Flags
			[FieldOffset (68)] public uint APMTable;
			
			// values set when GraphicsTableSet is set in Flags
			[FieldOffset (72)] public uint   VBEControlInfo;
			[FieldOffset (76)] public uint   VBEModeInfo;
			[FieldOffset (80)] public ushort VBEMode;
			[FieldOffset (82)] public ushort VBEInterfaceSegment;
			[FieldOffset (84)] public ushort VBEInterfaceOffset;
			[FieldOffset (86)] public ushort VBEInterfaceLength;
		}
		#endregion

		#endregion	
		
		#region IsMemorySizeSet
		public static bool IsMemorySizeSet { get { return ((Info->Flags & InfoFlags.MemorySizeSet) != 0); } }
		#endregion

		#region UpperMemorySize
		public static uint UpperMemorySize
		{
			get
			{
				if (IsMemorySizeSet)
					return Info->UpperMemorySize;
				else
					return 0;
			}
		}
		#endregion
			
		#region IsCommandLineSet
		public static bool IsCommandLineSet { get { return ((Info->Flags & InfoFlags.CommandLineSet) != 0); } }
		#endregion
		
		#region CommandLine
		public static CString8* CommandLine 
		{ 
			get 
			{
				if (IsCommandLineSet)
					return (CString8*)Info->_commandLine;
				else
					return null;
			} 
		}
		#endregion
		
		#region KernelAddress
		public static byte* KernelAddress { get { return (byte*)KernelStart; } }
		#endregion
		
		#region KernelSize
		public static uint KernelSize { get { return KernelEnd - KernelStart; } }
		#endregion
		
		#region IsMemoryMapSet
		public static bool IsMemoryMapSet { get { return ((Info->Flags & InfoFlags.MemoryMapSet) != 0); } }
		#endregion

		#region Setup
		private static Multiboot.InformationTable* Info = null;
		private static uint KernelStart;
		private static uint KernelEnd;

		public static bool Setup(uint magic, uint pointer, uint kernelStart, uint kernelEnd)
		{
			if (magic != (uint)SharpOS.Kernel.Multiboot.Identifiers.BootLoader) {
				Info = null;
				KernelStart = 0;
				KernelEnd = 0;
				return false;
			} else {
				Info = (Multiboot.InformationTable*) pointer;
				KernelStart = kernelStart;
				KernelEnd = kernelEnd;
				return true;
			}
		}
		#endregion

		#region WriteMultibootInfo
		public unsafe static void WriteMultibootInfo ()
		{
			TextMode.Write ("Kernel Image: 0x");
			TextMode.Write ((int) KernelStart, true, 4);
			TextMode.Write (" - 0x");
			TextMode.Write ((int) KernelEnd, true, 4);
			TextMode.WriteLine ();

			TextMode.Write ("Boot Drive: 0x");
			TextMode.Write ((int) Info->BootDevice, true, 4);
			TextMode.WriteLine ();

			TextMode.Write ("Flags: 0x");
			TextMode.Write ((int) Info->Flags, true, 4);
			TextMode.WriteLine ();

			TextMode.Write ("Command Line: ");
			TextMode.Write ((byte*) CommandLine);
			TextMode.WriteLine ();

			if (IsMemorySizeSet) {
				TextMode.Write ("Memory Lower: 0x");
				TextMode.Write ((int) Info->LowerMemorySize, true, 8);
				TextMode.WriteLine ();

				TextMode.Write ("Memory Upper: 0x");
				TextMode.Write ((int) Info->UpperMemorySize, true, 8);
				TextMode.WriteLine ();
			}

			if (IsMemoryMapSet) {
				TextMode.Write ("MMap Address: 0x");
				TextMode.Write ((int) Info->MMapAddress, true, 8);
				TextMode.WriteLine ();

				TextMode.Write ("MMap Length: 0x");
				TextMode.Write ((int) Info->MMapLength, true, 4);
				TextMode.WriteLine ();

				Multiboot.MemoryMap* mmap = (Multiboot.MemoryMap*) Info->MMapAddress;

				while ((uint) mmap < Info->MMapAddress + Info->MMapLength) {
					TextMode.Write ("Size = 0x");
					TextMode.Write ((int) mmap->Size, true, 4);
					TextMode.Write (", Base Address = 0x");
					if (mmap->BaseAddressHigh != 0)
						TextMode.Write ((int) mmap->BaseAddressHigh, true, 0);
					TextMode.Write ((int) mmap->BaseAddressLow, true, 8);
					TextMode.Write (", Length = 0x");
					if (mmap->LengthHigh != 0)
						TextMode.Write ((int) mmap->LengthHigh, true, 0);
					TextMode.Write ((int) mmap->LengthLow, true, 8);
					TextMode.Write (", Type = 0x");
					TextMode.Write ((int) mmap->Type, true, 2);
					TextMode.WriteLine ();

					// FIXME the 4 at the end is arch specific
					mmap = (Multiboot.MemoryMap*) ((uint) mmap + mmap->Size + 4);
				}
			}

			TextMode.WriteLine ();
		}
		#endregion
	}
}
