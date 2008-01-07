// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

#define PE

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using SharpOS.AOT.IR;
using SharpOS.AOT.IR.Operands;
using SharpOS.AOT.IR.Instructions;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace SharpOS.AOT.X86 {
	public partial class Assembly : IAssembly {
		internal const string KERNEL_MAIN = "KERNEL_MAIN";

		const string START_CODE = "START_CODE";
		const string END_CODE = "END_CODE";
		const string START_DATA = "START_DATA";
		const string END_DATA = "END_DATA";
		const string START_COFF_SYMBOLS = "START_COFF_SYMBOLS";
		const string START_BSS = "START_BSS";
		const string END_BSS = "END_BSS";
		const string END_STACK = "END_STACK";
		const string THE_END = "THE_END";
		const string KERNEL_ENTRY_POINT = "KERNEL_ENTRY_POINT";

		const string MULTIBOOT_HEADER_ADDRESS = "MULTIBOOT_HEADER_ADDRESS";
		const string MULTIBOOT_LOAD_END_ADDRESS = "MULTIBOOT_LOAD_END_ADDRESS";
		const string MULTIBOOT_BSS_END_ADDRESS = "MULTIBOOT_BSS_END_ADDRESS";
		const string MULTIBOOT_ENTRY_POINT = "MULTIBOOT_ENTRY_POINT";

#if PE
		const string DOS_MESSAGE = "DOS_MESSAGE";
		const string PE_ADRESS_OFFSET = "PE_ADRESS_OFFSET";
		const string PE_HEADER = "PE_HEADER";
		const string PE_POINTER_TO_SYMBOL_TABLE = "PE_POINTER_TO_SYMBOL_TABLE";
		const string PE_NUMBER_OF_SYMBOLS = "PE_NUMBER_OF_SYMBOLS";
		const string PE_SIZE_OF_OPTIONAL_HEADER = "PE_SIZE_OF_OPTIONAL_HEADER";
		const string PE_ADDRESS_OF_ENTRY_POINT = "PE_ADDRESS_OF_ENTRY_POINT";
		const string PE_CODE = ".text";
		const string PE_DATA = ".data";
		const string PE_BSS = ".bss";
		const string PE_VIRTUAL_SIZE = "VirtualSize";
		const string PE_VIRTUAL_ADDRESS = "VirtualAddress";
		const string PE_SIZE_OF_RAW_DATA = "SizeOfRawData";
		const string PE_POINTER_TO_RAW_DATA = "PointerToRawData";
		const ushort PE_CODE_SECTION = 1;
		const ushort PE_DATA_SECTION = 2;
#endif

		const uint BASE_ADDRESS = 0x00100000;
		internal const uint ALIGNMENT = 16;
		internal const byte OBJECT_ALIGNMENT_SHIFT = 3;
		internal const uint OBJECT_ALIGNMENT = 1 << OBJECT_ALIGNMENT_SHIFT;
		const uint STACK_SIZE = 64 * 1024;

		internal const string HELPER_LSHL = "LSHL";
		internal const string HELPER_LSHR = "LSHR";
		internal const string HELPER_LSAR = "LSAR";
		internal const string HELPER_LMUL = "LMUL";

		#region RUNTIME
		const string VTABLE_LABEL = "{0} VTable";
		#endregion

		Engine engine;

		public Engine Engine
		{
			get
			{
				return engine;
			}
		}

		Assembly data;
		Assembly bss;
		Dictionary<string, Instruction> labels;
		List<COFF.Symbol> symbols;

		uint multibootBSSEndAddress = 0;
		uint multibootLoadEndAddress = 0;

		/// <summary>
		/// Initializes a new instance of the <see cref="Assembly"/> class.
		/// </summary>
		public Assembly ()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Assembly"/> class.
		/// </summary>
		/// <param name="bits32">if set to <c>true</c> [bits32].</param>
		public Assembly (bool bits32)
		{
			this.bits32 = bits32;
		}

		private bool bits32 = true;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Assembly"/> is bits32.
		/// </summary>
		/// <value><c>true</c> if bits32; otherwise, <c>false</c>.</value>
		public bool Bits32
		{
			get
			{
				return bits32;
			}
		}

		List<Instruction> instructions = new List<Instruction> ();

		/// <summary>
		/// Gets the <see cref="SharpOS.AOT.X86.Instruction"/> at the specified index.
		/// </summary>
		/// <value></value>
		public Instruction this [int index]
		{
			get
			{
				return this.instructions [index];
			}
		}

		#region COFF Symbols
		/// <summary>
		/// Adds a COFF Symbol.
		/// </summary>
		/// <param name="symbol">The symbol.</param>
		internal void AddSymbol (COFF.Symbol symbol)
		{
			this.symbols.Add (symbol);
		}

		/// <summary>
		/// Adds the encoded COFF symbol data.
		/// </summary>
		private void AddSymbols ()
		{
			this.ALIGN (ALIGNMENT);
			this.LABEL (START_COFF_SYMBOLS);

			uint stringTableOffset = 4;

			for (int i = 0; i < this.symbols.Count; i++) {
				if (this.symbols [i] is COFF.Label) {
					// To tell the COFF Loader that the name is in the String Table
					this.DATA ((uint) 0);

					// The offset in the string table containing the name
					this.DATA (stringTableOffset);

					// We remember the index for the next instruction that needs to be updated
					this.symbols [i].Index = this.instructions.Count;

					// The offset to the code that the label points to in the binary
					this.DATA ((uint) 0);

#if PE
					// The section
					if (this.symbols [i] is COFF.Static)
						this.DATA (PE_DATA_SECTION);
					else
						this.DATA (PE_CODE_SECTION);
#endif

					// The type
					if (this.symbols [i] is COFF.Function)
						this.DATA ((ushort) 0x20);

					else if (this.symbols [i] is COFF.Static)
						this.DATA ((ushort) 0x04);

					else
						this.DATA ((ushort) 0x00);


					// The storage class
					if (this.symbols [i] is COFF.Function)
						this.DATA ((byte) COFF.Symbol.StorageClassType.C_EXT);

					else if (this.symbols [i] is COFF.Static)
						this.DATA ((byte) COFF.Symbol.StorageClassType.C_STAT);

					else
						this.DATA ((byte) COFF.Symbol.StorageClassType.C_LABEL);

					// Number of Auxiliary Records
					this.DATA ((byte) 0);

					stringTableOffset += (uint) ((this.symbols [i] as COFF.Label).Name.Length + 1);
				} else
					throw new EngineException ("COFF Symbol '" + this.symbols [i].GetType () + "' is not supported.");
			}

			// Write String Table Length
			this.DATA ((uint) stringTableOffset);

			for (int i = 0; i < this.symbols.Count; i++) {
				if (this.symbols [i] is COFF.Label) {
					this.DATA ((this.symbols [i] as COFF.Label).Name);
					this.DATA ((byte) 0);

				} else
					throw new EngineException ("COFF Symbol '" + this.symbols [i].GetType () + "' is not supported.");
			}
		}

		private void PatchSymbolOffsets ()
		{
			uint startCode = this.instructions [this.GetLabelIndex (START_CODE)].Offset;
			uint startData = this.instructions [this.GetLabelIndex (START_DATA)].Offset;
			for (int i = 0; i < this.symbols.Count; i++) {
				if (this.symbols [i] is COFF.Static) {
					uint offset = this.instructions [this.GetLabelIndex ((this.symbols [i] as COFF.Label).Name)].Offset - startData;
					this.instructions [this.symbols [i].Index].Value = offset;

				} else if (this.symbols [i] is COFF.Label) {
					uint offset = this.instructions [this.GetLabelIndex ((this.symbols [i] as COFF.Label).Name)].Offset - startCode;
					this.instructions [this.symbols [i].Index].Value = offset;

				} else
					throw new EngineException ("COFF Symbol '" + this.symbols [i].GetType () + "' is not supported.");
			}
		}
		#endregion

		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override string ToString ()
		{
			StringBuilder stringBuilder = new StringBuilder ();

			foreach (Instruction instruction in this.instructions)
				stringBuilder.Append (instruction.ToString () + "\n");

			return stringBuilder.ToString ();
		}

		/// <summary>
		/// Gets the type of the register size.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public InternalType GetRegisterSizeType (string value)
		{
			if (value.Equals ("SharpOS.AOT.X86.R8Type"))
				return InternalType.U1;

			else if (value.StartsWith ("SharpOS.AOT.X86.R16Type"))
				return InternalType.U2;

			else if (value.StartsWith ("SharpOS.AOT.X86.SegType"))
				return InternalType.U2;

			else if (value.StartsWith ("SharpOS.AOT.X86.R32Type"))
				return InternalType.U4;

			else if (value.StartsWith ("SharpOS.AOT.X86.CRType"))
				return InternalType.U4;

			else if (value.StartsWith ("SharpOS.AOT.X86.DRType"))
				return InternalType.U4;
			
			else if (value.StartsWith ("SharpOS.AOT.X86.TRType"))
				return InternalType.U4;

			else if (value.StartsWith ("SharpOS.AOT.X86.FPType"))
				return InternalType.F;

			else
				throw new EngineException ("'" + value + "' is not supported.");
		}

		/// <summary>
		/// Determines whether the specified value is instruction.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// 	<c>true</c> if the specified value is instruction; otherwise, <c>false</c>.
		/// </returns>
		public bool IsInstruction (string value)
		{
			return value.StartsWith ("SharpOS.AOT.X86.Asm");
		}


		/// <summary>
		/// Determines whether the specified value is register.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// 	<c>true</c> if the specified value is register; otherwise, <c>false</c>.
		/// </returns>
		public bool IsRegister (string value)
		{
			return value.StartsWith ("SharpOS.AOT.X86.Register")
				|| value.StartsWith ("SharpOS.AOT.X86.R8")
				|| value.StartsWith ("SharpOS.AOT.X86.R16")
				|| value.StartsWith ("SharpOS.AOT.X86.R32")
				|| value.StartsWith ("SharpOS.AOT.X86.Seg")
				|| value.StartsWith ("SharpOS.AOT.X86.DR")
				|| value.StartsWith ("SharpOS.AOT.X86.CR")
				|| value.StartsWith ("SharpOS.AOT.X86.FP")
				|| value.StartsWith ("SharpOS.AOT.X86.TR");
		}


		/// <summary>
		/// Determines whether the specified value is a memory address.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// 	<c>true</c> if the specified value is a memory address; otherwise, <c>false</c>.
		/// </returns>
		public bool IsMemoryAddress (string value)
		{
			return value.StartsWith ("SharpOS.AOT.X86.Memory")
				|| value.StartsWith ("SharpOS.AOT.X86.ByteMemory")
				|| value.StartsWith ("SharpOS.AOT.X86.WordMemory")
				|| value.StartsWith ("SharpOS.AOT.X86.DWordMemory")
				|| value.StartsWith ("SharpOS.AOT.X86.QWordMemory")
				|| value.StartsWith ("SharpOS.AOT.X86.TWordMemory");
		}

		/// <summary>
		/// BITs the S32.
		/// </summary>
		/// <param name="value">if set to <c>true</c> [value].</param>
		public void BITS32 (bool value)
		{
			this.instructions.Add (new Bits32Instruction (value));
		}

		/// <summary>
		/// Address of the specified label.
		/// </summary>
		/// <param name="label">The label.</param>
		public void ADDRESSOF (string label)
		{
			this.instructions.Add (new AddressOf (label));
		}

		/// <summary>
		/// DATAs the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="values">The values.</param>
		public void DATA (string name, string values)
		{
			this.instructions.Add (new LabelInstruction (name));
			this.instructions.Add (new ByteDataInstruction (values));
		}

		/// <summary>
		/// DATAs the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="value">The value.</param>
		public void DATA (string name, byte value)
		{
			this.instructions.Add (new LabelInstruction (name));
			this.instructions.Add (new ByteDataInstruction (value));
		}

		/// <summary>
		/// DATAs the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="value">The value.</param>
		public void DATA (string name, UInt16 value)
		{
			this.instructions.Add (new LabelInstruction (name));
			this.instructions.Add (new WordDataInstruction (value));
		}

		/// <summary>
		/// DATAs the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="value">The value.</param>
		public void DATA (string name, UInt32 value)
		{
			this.instructions.Add (new LabelInstruction (name));
			this.instructions.Add (new DWordDataInstruction (value));
		}

		/// <summary>
		/// Adds an UTF7 string for storage.
		/// </summary>
		/// <param name="value">The value.</param>
		public void DATA (string value)
		{
			this.instructions.Add (new ByteDataInstruction (value));
		}

		/// <summary>
		/// Adds an UTF16 string for storage.
		/// </summary>
		/// <param name="value">The value.</param>
		public void UTF16 (string value)
		{
			this.instructions.Add (new WordDataInstruction (value));
		}

		/// <summary>
		/// DATAs the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void DATA (byte value)
		{
			this.instructions.Add (new ByteDataInstruction (value));
		}

		/// <summary>
		/// DATAs the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void DATA (UInt16 value)
		{
			this.instructions.Add (new WordDataInstruction (value));
		}

		/// <summary>
		/// DATAs the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void DATA (UInt32 value)
		{
			this.instructions.Add (new DWordDataInstruction (value));
		}

		/// <summary>
		/// OFFSETs the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void OFFSET (UInt32 value)
		{
			this.instructions.Add (new OffsetInstruction (value));
		}

		/// <summary>
		/// ORGs the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void ORG (UInt32 value)
		{
			this.instructions.Add (new OrgInstruction (value));
		}

		/// <summary>
		/// ALIGNs the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void ALIGN (UInt32 value)
		{
			this.instructions.Add (new AlignInstruction (value));
		}

		/// <summary>
		/// TIMESs the specified length.
		/// </summary>
		/// <param name="length">The length.</param>
		/// <param name="value">The value.</param>
		public void TIMES (UInt32 length, Byte value)
		{
			this.instructions.Add (new TimesInstruction (length, value));
		}

		/// <summary>
		/// LABELs the specified label.
		/// </summary>
		/// <param name="label">The label.</param>
		public void LABEL (string label)
		{
			this.instructions.Add (new LabelInstruction (label));
		}

		/// <summary>
		/// LABELs the specified label.
		/// </summary>
		/// <param name="value">The value.</param>
		public void COMMENT (string value)
		{
			this.instructions.Add (new CommentInstruction (value));
		}

		/// <summary>
		/// MOVs the specified target.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="label">The label.</param>
		public void MOV (R16Type target, string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, label, "MOV", target.ToString () + ", " + Assembly.FormatLabelName (label), null, null, target, new UInt32 [] { 0 }, new string [] { "o16", "B8+r", "iw" }));
		}

		/// <summary>
		/// MOVs the specified target.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="label">The label.</param>
		public void MOV (R32Type target, string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, label, "MOV", target.ToString () + ", " + Assembly.FormatLabelName (label), null, null, target, new UInt32 [] { 0 }, new string [] { "o32", "B8+r", "id" }));
		}

		/// <summary>
		/// Gets the index of the given data label.
		/// </summary>
		/// <param name="label">The label.</param>
		/// <returns></returns>
		private int GetLabelIndex (string label)
		{
			label = Assembly.FormatLabelName (label);

			if (this.labels.ContainsKey (label) == false)
				throw new EngineException ("Label '" + label + "' has not been found.");

			return this.labels [label].Index;
		}

		/// <summary>
		/// Gets the address of the given data label.
		/// </summary>
		/// <param name="label">The label.</param>
		/// <returns></returns>
		private UInt32 GetLabelAddress (string label)
		{
			label = Assembly.FormatLabelName (label);

			if (this.labels.ContainsKey (label) == false)
				throw new EngineException ("Label '" + label + "' has not been found.");

			return this.labels [label].Offset;
		}

		private bool utf7StringEncoding;

		internal bool UTF7StringEncoding
		{
			get
			{
				return this.utf7StringEncoding;
			}
			set
			{
				this.utf7StringEncoding = value;
			}
		}

		/// <summary>
		/// Patches the specified memory stream.
		/// </summary>
		private void Patch ()
		{
			int index = this.GetLabelIndex (MULTIBOOT_ENTRY_POINT);
			this.instructions [index + 1].Value = BASE_ADDRESS + this.instructions [this.GetLabelIndex (KERNEL_ENTRY_POINT)].Offset;

			index = this.GetLabelIndex (MULTIBOOT_HEADER_ADDRESS);
			this.instructions [index + 1].Value = BASE_ADDRESS + this.instructions [index].Offset - 0x0C;

			index = this.GetLabelIndex (MULTIBOOT_LOAD_END_ADDRESS);
			this.instructions [index + 1].Value = this.multibootLoadEndAddress;

			index = this.GetLabelIndex (MULTIBOOT_BSS_END_ADDRESS);
			this.instructions [index + 1].Value = this.multibootBSSEndAddress;

#if PE
			index = this.GetLabelIndex (PE_ADRESS_OFFSET);
			this.instructions [index + 1].Value = this.instructions [this.GetLabelIndex (PE_HEADER)].Offset;

			this.PatchPE ();
#endif
		}

		#region Portable Executable
#if PE
		private void AddPEHeader ()
		{
			////////////////////////////////////////////////////////////////////// 
			// DOS Header
			// Magic number ('MZ')
			this.DATA ((ushort) 0x5A4D);

			// Bytes on last page of file
			this.DATA ((ushort) 0x0090);

			// Pages in file
			this.DATA ((ushort) 0x0003);

			// Relocations
			this.DATA ((ushort) 0x0000);

			// Size of header in paragraphs
			this.DATA ((ushort) 0x0004);

			// Minimum extra paragraphs needed
			this.DATA ((ushort) 0x0000);

			// Maximum extra paragraphs needed
			this.DATA ((ushort) 0xFFFF);

			// Initial (relative) SS value
			this.DATA ((ushort) 0x0000);

			// Initial SP value
			this.DATA ((ushort) 0x00B8);

			// Checksum
			this.DATA ((ushort) 0x0000);

			// Initial IP value
			this.DATA ((ushort) 0x0000);

			// Initial (relative) CS value
			this.DATA ((ushort) 0x0000);

			// File address of relocation table
			this.DATA ((ushort) 0x0040);

			// Overlay number
			this.DATA ((ushort) 0x0000);

			// Reserved words
			this.DATA ((ushort) 0x0000);
			this.DATA ((ushort) 0x0000);
			this.DATA ((ushort) 0x0000);
			this.DATA ((ushort) 0x0000);

			// OEM identifier
			this.DATA ((ushort) 0x0000);

			// OEM information
			this.DATA ((ushort) 0x0000);

			// Reserved words
			this.DATA ((ushort) 0x0000);
			this.DATA ((ushort) 0x0000);
			this.DATA ((ushort) 0x0000);
			this.DATA ((ushort) 0x0000);
			this.DATA ((ushort) 0x0000);
			this.DATA ((ushort) 0x0000);
			this.DATA ((ushort) 0x0000);
			this.DATA ((ushort) 0x0000);
			this.DATA ((ushort) 0x0000);
			this.DATA ((ushort) 0x0000);

			// File address of the new exe header
			this.LABEL (PE_ADRESS_OFFSET);
			this.DATA ((uint) 0);

			//////////////////////////////////////////////////////////////////////
			// DOS Code 
			this.BITS32 (false);
			this.ORG (0);

			this.PUSH (Seg.CS);
			this.POP (Seg.DS);

			this.MOV (R16.DX, DOS_MESSAGE);
			this.MOV (R8.AH, 0x09);
			this.INT (0x21);
			this.MOV (R16.AX, 0x4c01);
			this.INT (0x21);

			this.LABEL (DOS_MESSAGE);
			this.DATA ("This is SharpOS speaking: Please don't panic!$");

			this.BITS32 (true);

			this.AddMultibootHeader ();

			//////////////////////////////////////////////////////////////////////
			// PE Header
			this.ALIGN (ALIGNMENT);

			this.LABEL (PE_HEADER);

			// PE\0\0 (PE Signature)
			this.DATA ((uint) 0x00004550);

			// Machine (Intel 386)
			this.DATA ((ushort) 0x014C);

			// Number of Sections
			this.DATA ((ushort) 0x0003);

			// Time Date Stamp
			this.DATA ((uint) 0x4634F185);

			// Pointer to Symbol Table
			this.LABEL (PE_POINTER_TO_SYMBOL_TABLE);
			this.DATA ((uint) 0);

			// Number of Symbols
			this.LABEL (PE_NUMBER_OF_SYMBOLS);
			this.DATA ((uint) 0);

			// Size of Optional Header
			this.DATA ((ushort) 0x00E0);

			// Characteristics
			this.DATA ((ushort) 0x1305);

			//////////////////////////////////////////////////////////////////////
			// Optional Header
			// MagicNumber 	
			this.DATA ((ushort) 0x010B);

			// MajorLinkerVersion
			this.DATA ((byte) 0x07);

			// MinorLinkerVersion
			this.DATA ((byte) 0x00);

			// SizeOfCode
			this.DATA ((uint) 0x00000000); // FIXME ?

			// SizeOfInitializedData
			this.DATA ((uint) 0x00000000); // FIXME ?

			// SizeOfUninitializedData
			this.DATA ((uint) 0x00000000); // FIXME ?

			// AddressOfEntryPoint
			this.LABEL (PE_ADDRESS_OF_ENTRY_POINT);
			this.DATA ((uint) 0x00000000);

			// BaseOfCode
			this.DATA ((uint) 0x00000000);

			// BaseOfData
			this.DATA ((uint) 0x00000000);

			// ImageBase
			this.DATA ((uint) BASE_ADDRESS);

			// SectionAlignment
			this.DATA ((uint) ALIGNMENT);

			// FileAlignment
			this.DATA ((uint) ALIGNMENT);

			// MajorOSVersion
			this.DATA ((ushort) 0x0005);

			// MinorOSVersion
			this.DATA ((ushort) 0x0001);

			// MajorImageVersion
			this.DATA ((ushort) 0x0005);

			// MinorImageVersion
			this.DATA ((ushort) 0x0001);

			// MajorSubsystemVersion
			this.DATA ((ushort) 0x0004);

			// MinorSubsystemVersion
			this.DATA ((ushort) 0x0000);

			// Reserved
			this.DATA ((uint) 0x00000000);

			// SizeOfImage
			this.DATA ((uint) 0x00000000);

			// SizeOfHeaders
			this.DATA ((uint) 0x00000000);

			// CheckSum
			this.DATA ((uint) 0x00000000);

			// Subsystem
			this.DATA ((ushort) 0x0001);

			// DLLCharacteristics
			this.DATA ((ushort) 0x8000);

			// SizeOfStackReserve
			this.DATA ((uint) 0x00040000);

			// SizeOfStackCommit
			this.DATA ((uint) 0x00001000);

			// SizeOfHeapReserve
			this.DATA ((uint) 0x00100000);

			// SizeOfHeapCommit
			this.DATA ((uint) 0x00001000);

			// LoaderFlags
			this.DATA ((uint) 0x00000000);

			// NumberOfRvaAndSizes
			this.DATA ((uint) 0x00000010);

			// Export Table
			this.DATA ((uint) 0x00000000);
			this.DATA ((uint) 0x00000000);

			// Import Table
			this.DATA ((uint) 0x00000000);
			this.DATA ((uint) 0x00000000);

			// Resource Table
			this.DATA ((uint) 0x00000000);
			this.DATA ((uint) 0x00000000);

			// Exception Table
			this.DATA ((uint) 0x00000000);
			this.DATA ((uint) 0x00000000);

			// Certificate File
			this.DATA ((uint) 0x00000000);
			this.DATA ((uint) 0x00000000);

			// Relocation Table
			this.DATA ((uint) 0x00000000);
			this.DATA ((uint) 0x00000000);

			// Debug Data
			this.DATA ((uint) 0x00000000);
			this.DATA ((uint) 0x00000000);

			// Architecture Data
			this.DATA ((uint) 0x00000000);
			this.DATA ((uint) 0x00000000);

			// Global Ptr
			this.DATA ((uint) 0x00000000);
			this.DATA ((uint) 0x00000000);

			// TLS Table
			this.DATA ((uint) 0x00000000);
			this.DATA ((uint) 0x00000000);

			// Load Config Table
			this.DATA ((uint) 0x00000000);
			this.DATA ((uint) 0x00000000);

			// Bound Import Table
			this.DATA ((uint) 0x00000000);
			this.DATA ((uint) 0x00000000);

			// Import Address Table
			this.DATA ((uint) 0x00000000);
			this.DATA ((uint) 0x00000000);

			// Delay Import Descriptor
			this.DATA ((uint) 0x00000000);
			this.DATA ((uint) 0x00000000);

			// COM+ Runtime Header
			this.DATA ((uint) 0x00000000);
			this.DATA ((uint) 0x00000000);

			// Reserved
			this.DATA ((uint) 0x00000000);
			this.DATA ((uint) 0x00000000);

			//////////////////////////////////////////////////////////////////////
			// Sections
			this.AddPESection (PE_CODE, 0x60000060);

			this.AddPESection (PE_DATA, 0xC0000040);

			this.AddPESection (PE_BSS, 0xC0000080);
		}

		private void PatchPE ()
		{
			uint start = this.instructions [this.GetLabelIndex (START_CODE)].Offset;
			int index = this.GetLabelIndex (PE_ADDRESS_OF_ENTRY_POINT);
			this.instructions [index + 1].Value = start;

			index = this.GetLabelIndex (GetPESectionLabel (PE_CODE, PE_VIRTUAL_ADDRESS));
			this.instructions [index + 1].Value = start;

			index = this.GetLabelIndex (GetPESectionLabel (PE_CODE, PE_POINTER_TO_RAW_DATA));
			this.instructions [index + 1].Value = start;


			////////////////////////////////////////////////////////////////////////////////////////
			start = this.instructions [this.GetLabelIndex (END_CODE)].Offset - start;
			index = this.GetLabelIndex (GetPESectionLabel (PE_CODE, PE_VIRTUAL_SIZE));
			this.instructions [index + 1].Value = start;

			index = this.GetLabelIndex (GetPESectionLabel (PE_CODE, PE_SIZE_OF_RAW_DATA));
			this.instructions [index + 1].Value = start;


			////////////////////////////////////////////////////////////////////////////////////////
			start = this.instructions [this.GetLabelIndex (START_DATA)].Offset;
			index = this.GetLabelIndex (GetPESectionLabel (PE_DATA, PE_VIRTUAL_ADDRESS));
			this.instructions [index + 1].Value = start;

			index = this.GetLabelIndex (GetPESectionLabel (PE_DATA, PE_POINTER_TO_RAW_DATA));
			this.instructions [index + 1].Value = start;


			////////////////////////////////////////////////////////////////////////////////////////
			start = this.instructions [this.GetLabelIndex (END_DATA)].Offset - start;
			index = this.GetLabelIndex (GetPESectionLabel (PE_DATA, PE_VIRTUAL_SIZE));
			this.instructions [index + 1].Value = start;

			index = this.GetLabelIndex (GetPESectionLabel (PE_DATA, PE_SIZE_OF_RAW_DATA));
			this.instructions [index + 1].Value = start;


			////////////////////////////////////////////////////////////////////////////////////////
			start = this.instructions [this.GetLabelIndex (START_BSS)].Offset;
			index = this.GetLabelIndex (GetPESectionLabel (PE_BSS, PE_VIRTUAL_ADDRESS));
			this.instructions [index + 1].Value = start;


			////////////////////////////////////////////////////////////////////////////////////////
			start = this.instructions [this.GetLabelIndex (END_BSS)].Offset - start;
			index = this.GetLabelIndex (GetPESectionLabel (PE_BSS, PE_VIRTUAL_SIZE));
			this.instructions [index + 1].Value = start;


			////////////////////////////////////////////////////////////////////////////////////////
			start = this.instructions [this.GetLabelIndex (START_COFF_SYMBOLS)].Offset;
			index = this.GetLabelIndex (PE_POINTER_TO_SYMBOL_TABLE);
			this.instructions [index + 1].Value = (uint) start;

			index = this.GetLabelIndex (PE_NUMBER_OF_SYMBOLS);
			this.instructions [index + 1].Value = (uint) this.symbols.Count;
		}

		private static string GetPESectionLabel (string prefix, string type)
		{
			return "PE_Section_" + prefix + "_" + type;
		}

		private void AddPESection (string id, uint characteristics)
		{
			string name = id;

			while (name.Length < 8)
				name += "\0";

			this.DATA (name);

			// Misc/Virtual Size
			this.LABEL (GetPESectionLabel (id, PE_VIRTUAL_SIZE));
			this.DATA ((uint) 0x00000000);

			// Virtual Address
			this.LABEL (GetPESectionLabel (id, PE_VIRTUAL_ADDRESS));
			this.DATA ((uint) 0x00000000);

			// Size of Raw Data
			this.LABEL (GetPESectionLabel (id, PE_SIZE_OF_RAW_DATA));
			this.DATA ((uint) 0x00000000);

			// Pointer to Raw Data
			this.LABEL (GetPESectionLabel (id, PE_POINTER_TO_RAW_DATA));
			this.DATA ((uint) 0x00000000);

			// Pointer to Relocations
			this.DATA ((uint) 0x00000000);

			// Pointer to Linenumbers
			this.DATA ((uint) 0x00000000);

			// Number of Relocations
			this.DATA ((ushort) 0x0000);

			// Number of Line Numbers
			this.DATA ((ushort) 0x0000);

			// Characteristics
			this.DATA (characteristics);
		}
#endif
		#endregion

		/// <summary>
		/// Adds the multiboot header.
		/// </summary>
		private void AddMultibootHeader ()
		{
			uint magic = 0x1BADB002;
			uint flags = 0x00010003; //Extra info following and retrieve memory and video modes infos
			uint checksum = (uint) ((int) ((-(magic + flags))));

			this.ALIGN (4);

			this.DATA (magic);
			this.DATA (flags);
			this.DATA (checksum);

			// Header Address
			this.LABEL (MULTIBOOT_HEADER_ADDRESS);
			this.DATA ((uint) 0);

			// Load Address
			this.DATA (BASE_ADDRESS);

			// Load End Address
			this.LABEL (MULTIBOOT_LOAD_END_ADDRESS);
			this.DATA ((uint) 0);

			// BSS End Address
			this.LABEL (MULTIBOOT_BSS_END_ADDRESS);
			this.DATA ((uint) 0);

			// Entry Address (It will get patched later)
			this.LABEL (MULTIBOOT_ENTRY_POINT);
			this.DATA ((uint) 0);
		}

		/// <summary>
		/// Adds the entry point.
		/// </summary>
		private void AddEntryPoint ()
		{
			this.ORG (0x00100000);

			this.AddSymbol (new COFF.Label (KERNEL_ENTRY_POINT));

			this.LABEL (KERNEL_ENTRY_POINT);

			this.MOV (R32.ESP, END_STACK);

			this.PUSH (0);
			this.POPF ();

			// The kernel End
			this.MOV (R32.ECX, THE_END);
			this.PUSH (R32.ECX);

			// The kernel Start
			this.PUSH (BASE_ADDRESS);

			// Pointer to the Multiboot Info 
			this.PUSH (R32.EBX);

			// The magic value
			this.PUSH (R32.EAX);

			foreach (Class _class in engine) {
				foreach (Method method in _class) {
					if (method.MethodFullName.IndexOf (".cctor") == -1)
						continue;

					this.CALL (method.MethodFullName);
				}
			}

			this.CALL (KERNEL_MAIN);

			// Just hang
			this.HLT ();
		}

		/// <summary>
		/// Gets the V table label.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public string GetVTableLabel (string value)
		{
			return string.Format (VTABLE_LABEL, value);
		}

		/// <summary>
		/// Adds the object fields.
		/// </summary>
		/// <param name="_class">The _class.</param>
		private void AddObjectFields (string _class)
		{
			// VTable
			this.ADDRESSOF (this.GetVTableLabel (_class));

			// Synchronisation
			this.DATA ((uint) 0);
		}

		/// <summary>
		/// Adds the data.
		/// </summary>
		private void AddData ()
		{
			this.engine.Dump.Section (DumpSection.DataEncode);

			this.ALIGN (ALIGNMENT);
			this.LABEL (START_DATA);

			foreach (Class _class in engine) {
				if (_class.IsInternal)
					continue;

				if (_class.ClassDefinition.IsEnum)
					continue;

				this.ALIGN (OBJECT_ALIGNMENT);

				// Writing the Runtime VTable instances
				string label = this.GetVTableLabel (_class.TypeFullName);
				this.AddSymbol (new COFF.Label (label));
				this.LABEL (label);

				this.AddObjectFields (this.engine.VTableClass.TypeFullName);

				// VTable Size Field
				this.DATA ((uint) _class.Size);

				if (_class.ClassDefinition.IsValueType)
					continue;

				foreach (Field field in _class.Fields.Values) {
					string fullname = field.Type.ToString ();
					//field.DeclaringType.FullName + "::" + field.Name;

					if (!field.Type.IsStatic) {
						this.engine.Dump.IgnoreMember (fullname,
								"Non-static field");

						continue;
					}

					this.AddSymbol (new COFF.Static (fullname));
					this.LABEL (fullname);

					// TODO refactor this
					switch (engine.GetInternalType (field.Type.FieldType.FullName)) {
						case InternalType.I1:
						case InternalType.U1:
							this.DATA ((byte) 0);
							break;

						case InternalType.I2:
						case InternalType.U2:
							this.DATA ((ushort) 0);
							break;

						case InternalType.O:
						case InternalType.I:
						case InternalType.U:
						case InternalType.I4:
						case InternalType.U4:
						case InternalType.R4:
							this.DATA ((uint) 0);
							break;

						case InternalType.I8:
						case InternalType.U8:
						case InternalType.R8:
						case InternalType.F:
							this.DATA ((uint) 0);
							this.DATA ((uint) 0);
							break;

						default:
							throw new NotImplementedEngineException ("'" + field.Type.FieldType + "' is not supported.");
					}
				}
			}

			this.engine.Dump.PopElement ();	// section: DataEncode

			foreach (Instruction instruction in data.instructions)
				this.instructions.Add (instruction);

			AddResources ();

			this.ALIGN (ALIGNMENT);
			this.LABEL (END_DATA);
		}

		/// <summary>
		/// Adds the stack.
		/// </summary>
		private void AddBSS ()
		{
			this.ALIGN (ALIGNMENT);
			this.LABEL (START_BSS);

			foreach (Instruction instruction in bss.instructions)
				this.instructions.Add (instruction);

			this.TIMES (STACK_SIZE, 0);
			this.LABEL (END_STACK);

			this.ALIGN (ALIGNMENT);
			this.LABEL (END_BSS);
		}

		private void AddResources ()
		{
			foreach (KeyValuePair<string, byte []> kvp in this.engine.Resources) {
				this.engine.Message (3, "Encoding resource `{0}'...",
					kvp.Key);

				if (!kvp.Key.Contains ("/Resources/"))
					throw new EngineException ("Bad label for resource: " +
						kvp.Key);

				this.LABEL (kvp.Key);

				for (int x = 0; x < kvp.Value.Length; ++x)
					this.DATA (kvp.Value [x]);
			}
		}

		/// <summary>
		/// Encodes the specified engine.
		/// </summary>
		/// <param name="engine">The engine.</param>
		/// <param name="target">The target.</param>
		/// <returns></returns>
		public bool Encode (Engine engine, string target)
		{
			this.symbols = new List<SharpOS.AOT.COFF.Symbol> ();
			this.data = new Assembly ();
			this.bss = new Assembly ();
			this.engine = engine;

			this.engine.Dump.Section (DumpSection.Encoding);

#if PE
			this.AddPEHeader ();
#else
			this.AddMultibootHeader ();
#endif

			this.ALIGN (ALIGNMENT);
			this.LABEL (START_CODE);

			this.AddEntryPoint ();

			this.engine.Dump.Section (DumpSection.MethodEncode);

			foreach (Class _class in engine) {
				foreach (Method method in _class) {
					this.engine.Dump.MethodEncode (method);

					engine.SetStatusInformation (_class.ClassDefinition.Module.Assembly,
						_class.ClassDefinition.Module, _class.ClassDefinition,
						method.MethodDefinition);

					new AssemblyMethod (this, method).GetAssemblyCode ();

					engine.ClearStatusInformation ();
				}
			}

			this.engine.Dump.PopElement ();

			this.AddHelperFunctions ();

			this.ALIGN (ALIGNMENT);
			this.LABEL (END_CODE);

			this.AddData ();
			this.AddSymbols ();
			this.AddBSS ();

			this.ALIGN (ALIGNMENT);
			this.LABEL (THE_END);

			this.Save (target);

			return true;
		}

		/// <summary>
		/// Saves the specified target.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <returns></returns>
		private bool Save (string target)
		{
			MemoryStream memoryStream = new MemoryStream ();

			this.Encode (memoryStream);

			using (FileStream fileStream = new FileStream (target, FileMode.Create))
				memoryStream.WriteTo (fileStream);

			if (this.engine.Options.AsmDump) {
				try {
					using (StreamWriter streamWriter = new StreamWriter (this.engine.Options.AsmFile)) {
						foreach (Instruction instruction in this.instructions) {
							if (instruction is LabelInstruction)
								streamWriter.WriteLine ();

							streamWriter.WriteLine (instruction.ToString ());
						}
					}
				} catch {
					Console.WriteLine ("Could not generate the Asm Dump file. ('" + this.engine.Options.AsmFile + "')");
				}
			}

			this.engine.Dump.PopElement ();

			return true;
		}

		/// <summary>
		/// Encodes the specified memory stream.
		/// </summary>
		/// <param name="memoryStream">The memory stream.</param>
		/// <returns></returns>
		public bool Encode (MemoryStream memoryStream)
		{
			int org = 0;

			this.labels = new Dictionary<string, Instruction> ();
			BinaryWriter binaryWriter = new BinaryWriter (memoryStream);

			// The first pass does the computation/optimization
			// The second pass writes the content
			for (int pass = 0; pass < 2; pass++) {
				bool changed;

				if (pass == 1)
					this.Patch ();

				if (pass == 1)
					this.PatchSymbolOffsets ();

				do {
					changed = false;
					UInt32 offset = 0;
					bool bss = false;

					for (int i = 0; i < this.instructions.Count; i++) {
						Instruction instruction = this.instructions [i];

						if (pass == 0)
							instruction.Index = i;

						if (instruction is OrgInstruction) {
							if ((UInt32) instruction.Value < offset)
								org = -((int) offset);

							else
								org = System.Convert.ToInt32 (instruction.Value);

						} else if (instruction is Bits32Instruction)
							this.bits32 = (bool) instruction.Value;

						else if (instruction is OffsetInstruction) {
							offset = (UInt32) instruction.Value;

							if (offset < binaryWriter.BaseStream.Length)
								throw new EngineException ("Wrong offset '" + offset.ToString () + "'.");

							while (pass == 1 && !bss && binaryWriter.BaseStream.Length < instruction.Offset)
								binaryWriter.Write ((byte) 0);

						} else if (instruction is AlignInstruction) {
							if (offset % (UInt32) instruction.Value != 0)
								offset += ((UInt32) instruction.Value - offset % (UInt32) instruction.Value);

							while (pass == 1 && !bss && binaryWriter.BaseStream.Length < instruction.Offset)
								binaryWriter.Write ((byte) 0);

						} else if (instruction is TimesInstruction) {
							TimesInstruction times = instruction as TimesInstruction;

							offset += times.Length;

							while (pass == 1 && !bss && binaryWriter.BaseStream.Length < instruction.Offset)
								binaryWriter.Write ((byte) times.Value);
						}

						if (instruction.Label.Equals (Assembly.FormatLabelName (START_BSS)))
							bss = true;

						if (pass == 0) {
							instruction.Offset = offset;

							if (instruction.Label.Length > 0) {
								if (this.labels.ContainsKey (instruction.Label))
									throw new EngineException (string.Format (
										"The label '{0}' has been defined more than once. Definition 1 = '{1}', Definition 2 = '{2}'",
										instruction.Label, this.labels [instruction.Label], instruction));

								this.labels.Add (instruction.Label, instruction);
							}
						}

						if (pass == 1) {
							if (instruction is AddressOf) {
								AddressOf addressOf = instruction as AddressOf;
								addressOf.Value = (uint) (org + this.GetLabelAddress (addressOf.AddressOfLabel));
							}

							if (instruction.Reference.Length > 0) {
								((UInt32 []) instruction.Value) [0] = this.GetLabelAddress (instruction.Reference);

								if (!instruction.Relative)
									((UInt32 []) instruction.Value) [0] = (UInt32) (org + ((UInt32 []) instruction.Value) [0]);
							}

							if (instruction.RMMemory != null && instruction.RMMemory.Reference.Length > 0)
								instruction.RMMemory.Displacement = (int) (org + this.GetLabelAddress (instruction.RMMemory.Reference) + instruction.RMMemory.DisplacementDelta);
						}

						if (pass == 0) {
							// Load End Address
							if (instruction.Label.Equals (Assembly.FormatLabelName (END_DATA)))
								this.multibootLoadEndAddress = (UInt32) (org + instruction.Offset);

							// BSS End Address
							if (instruction.Label.Equals (Assembly.FormatLabelName (END_BSS)))
								this.multibootBSSEndAddress = (UInt32) (org + instruction.Offset);
						}

						if (pass == 1 && !bss)
							instruction.Encode (this.bits32, binaryWriter);

						if (pass == 0)
							offset += instruction.Size (this.bits32);
					}

				} while (changed);
			}

			return true;
		}

		/// <summary>
		/// Adds the LSHL.
		/// </summary>
		private void AddLSHL ()
		{
			string end = HELPER_LSHL + " EXIT";
			string hiShift = HELPER_LSHL + " HI_SHIFT";

			this.AddSymbol (new COFF.Label (HELPER_LSHL));

			this.LABEL (HELPER_LSHL);
			this.MOV (R32.ECX, new DWordMemory (null, R32.ESP, null, 0, 12));
			this.MOV (R32.EDX, new DWordMemory (null, R32.ESP, null, 0, 8));
			this.MOV (R32.EAX, new DWordMemory (null, R32.ESP, null, 0, 4));

			this.AND (R32.ECX, 63);

			this.TEST (R32.ECX, R32.ECX);
			this.JZ (end);

			this.CMP (R32.ECX, 32);
			this.JAE (hiShift);

			this.PUSH (R32.EBX);
			this.PUSH (R32.ESI);

			this.MOV (R32.ESI, R32.EAX);
			this.SHL__CL (R32.ESI);

			this.SHL__CL (R32.EDX);

			this.MOV (R32.EBX, 32);
			this.SUB (R32.EBX, R32.ECX);
			this.MOV (R32.ECX, R32.EBX);

			this.SHR__CL (R32.EAX);

			this.OR (R32.EDX, R32.EAX);

			this.MOV (R32.EAX, R32.ESI);

			this.POP (R32.ESI);
			this.POP (R32.EBX);

			this.JMP (end);
			this.LABEL (hiShift);

			this.MOV (R32.EDX, R32.EAX);
			this.XOR (R32.EAX, R32.EAX);
			this.SUB (R32.ECX, 32);
			this.SHL__CL (R32.EDX);

			this.LABEL (end);
			this.RET ();
		}

		/// <summary>
		/// Adds the LSHR.
		/// </summary>
		private void AddLSHR ()
		{
			string end = HELPER_LSHR + " EXIT";
			string hiShift = HELPER_LSHR + " HI SHIFT";

			this.AddSymbol (new COFF.Label (HELPER_LSHR));

			this.LABEL (HELPER_LSHR);
			this.MOV (R32.ECX, new DWordMemory (null, R32.ESP, null, 0, 12));
			this.MOV (R32.EDX, new DWordMemory (null, R32.ESP, null, 0, 8));
			this.MOV (R32.EAX, new DWordMemory (null, R32.ESP, null, 0, 4));

			this.AND (R32.ECX, 63);

			this.TEST (R32.ECX, R32.ECX);
			this.JZ (end);

			this.CMP (R32.ECX, 32);
			this.JAE (hiShift);

			this.PUSH (R32.EBX);
			this.PUSH (R32.ESI);

			this.MOV (R32.ESI, R32.EDX);
			this.SHR__CL (R32.ESI);

			this.SHR__CL (R32.EAX);

			this.MOV (R32.EBX, 32);
			this.SUB (R32.EBX, R32.ECX);
			this.MOV (R32.ECX, R32.EBX);

			this.SHL__CL (R32.EDX);

			this.OR (R32.EAX, R32.EDX);

			this.MOV (R32.EDX, R32.ESI);

			this.POP (R32.ESI);
			this.POP (R32.EBX);

			this.JMP (end);
			this.LABEL (hiShift);

			this.MOV (R32.EAX, R32.EDX);
			this.XOR (R32.EDX, R32.EDX);
			this.SUB (R32.ECX, 32);
			this.SHR__CL (R32.EAX);

			this.LABEL (end);
			this.RET ();
		}

		/// <summary>
		/// Adds the LSAR.
		/// </summary>
		private void AddLSAR ()
		{
			string end = HELPER_LSAR + " EXIT";
			string hiShift = HELPER_LSAR + " HI SHIFT";

			this.AddSymbol (new COFF.Label (HELPER_LSAR));

			this.LABEL (HELPER_LSAR);
			this.MOV (R32.ECX, new DWordMemory (null, R32.ESP, null, 0, 12));
			this.MOV (R32.EDX, new DWordMemory (null, R32.ESP, null, 0, 8));
			this.MOV (R32.EAX, new DWordMemory (null, R32.ESP, null, 0, 4));

			this.AND (R32.ECX, 63);

			this.TEST (R32.ECX, R32.ECX);
			this.JZ (end);

			this.CMP (R32.ECX, 32);
			this.JAE (hiShift);

			this.PUSH (R32.EBX);
			this.PUSH (R32.ESI);

			this.MOV (R32.ESI, R32.EDX);
			this.SAR__CL (R32.ESI);

			this.SAR__CL (R32.EAX);

			this.MOV (R32.EBX, 32);
			this.SUB (R32.EBX, R32.ECX);
			this.MOV (R32.ECX, R32.EBX);

			this.SHL__CL (R32.EDX);

			this.OR (R32.EAX, R32.EDX);

			this.MOV (R32.EDX, R32.ESI);

			this.POP (R32.ESI);
			this.POP (R32.EBX);

			this.JMP (end);
			this.LABEL (hiShift);

			this.MOV (R32.EAX, R32.EDX);
			this.SUB (R32.ECX, 32);
			this.SAR__CL (R32.EAX);

			this.SAR (R32.EDX, 31);

			this.LABEL (end);
			this.RET ();
		}

		private void AddLMUL ()
		{
			string firstNotSigned = HELPER_LMUL + " FIRST NOT SIGNED";
			string secondNotSigned = HELPER_LMUL + " SECOND NOT SIGNED";
			string dontSignResult = HELPER_LMUL + " DONT SIGN RESULT";

			this.AddSymbol (new COFF.Label (HELPER_LMUL));

			this.LABEL (HELPER_LMUL);

			this.MOV (R32.ECX, 0);

			this.CMP (new DWordMemory (null, R32.ESP, null, 0, 8), 0);
			this.JNS (firstNotSigned);
			this.NOT (new DWordMemory (null, R32.ESP, null, 0, 4));
			this.NOT (new DWordMemory (null, R32.ESP, null, 0, 8));
			this.ADD (new DWordMemory (null, R32.ESP, null, 0, 4), (byte) 1);
			this.ADC (new DWordMemory (null, R32.ESP, null, 0, 8), (byte) 0);
			this.MOV (R32.ECX, (byte) 1);
			this.LABEL (firstNotSigned);


			this.CMP (new DWordMemory (null, R32.ESP, null, 0, 16), 0);
			this.JNS (secondNotSigned);
			this.NOT (new DWordMemory (null, R32.ESP, null, 0, 12));
			this.NOT (new DWordMemory (null, R32.ESP, null, 0, 16));
			this.ADD (new DWordMemory (null, R32.ESP, null, 0, 12), (byte) 1);
			this.ADC (new DWordMemory (null, R32.ESP, null, 0, 16), (byte) 0);
			this.XOR (R32.ECX, (byte) 1);
			this.LABEL (secondNotSigned);


			this.MOV (R32.EAX, new DWordMemory (null, R32.ESP, null, 0, 4));
			this.MUL (new DWordMemory (null, R32.ESP, null, 0, 12));
			this.MOV (new DWordMemory (null, R32.ESP, null, 0, -4), R32.EAX);
			this.MOV (new DWordMemory (null, R32.ESP, null, 0, -8), R32.EDX);

			this.MOV (R32.EAX, new DWordMemory (null, R32.ESP, null, 0, 8));
			this.MUL (new DWordMemory (null, R32.ESP, null, 0, 12));
			this.ADD (new DWordMemory (null, R32.ESP, null, 0, -8), R32.EAX);

			this.MOV (R32.EAX, new DWordMemory (null, R32.ESP, null, 0, 4));
			this.MUL (new DWordMemory (null, R32.ESP, null, 0, 16));
			this.ADD (new DWordMemory (null, R32.ESP, null, 0, -8), R32.EAX);

			this.TEST (R32.ECX, 1);
			this.JZ (dontSignResult);
			this.NOT (new DWordMemory (null, R32.ESP, null, 0, -4));
			this.NOT (new DWordMemory (null, R32.ESP, null, 0, -8));
			this.ADD (new DWordMemory (null, R32.ESP, null, 0, -4), (byte) 1);
			this.ADC (new DWordMemory (null, R32.ESP, null, 0, -8), (byte) 0);
			this.LABEL (dontSignResult);

			this.MOV (R32.EAX, new DWordMemory (null, R32.ESP, null, 0, -4));
			this.MOV (R32.EDX, new DWordMemory (null, R32.ESP, null, 0, -8));

			this.RET ();
		}

		/// <summary>
		/// Adds the helper functions.
		/// </summary>
		private void AddHelperFunctions ()
		{
			this.AddLSHL ();
			this.AddLSHR ();
			this.AddLSAR ();
			this.AddLMUL ();
		}

		/// <summary>
		/// Gets the memory internal.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		private static Memory GetMemoryInternal (object value)
		{
			if (value is Memory)
				return value as Memory;

			throw new EngineException ("'" + value.ToString () + "' is not supported.");
		}

		/// <summary>
		/// Gets the memory.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public Memory GetMemory (object value)
		{
			return GetMemoryInternal (value);
		}

		/// <summary>
		/// Gets the byte memory.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public ByteMemory GetByteMemory (object value)
		{
			return GetMemoryInternal (value) as ByteMemory;
		}

		/// <summary>
		/// Gets the word memory.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public WordMemory GetWordMemory (object value)
		{
			return GetMemoryInternal (value) as WordMemory;
		}

		/// <summary>
		/// Gets the D word memory.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public DWordMemory GetDWordMemory (object value)
		{
			return GetMemoryInternal (value) as DWordMemory;
		}

		/// <summary>
		/// Gets the Q word memory.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public QWordMemory GetQWordMemory (object value)
		{
			return GetMemoryInternal (value) as QWordMemory;
		}

		/// <summary>
		/// Gets the T word memory.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public TWordMemory GetTWordMemory (object value)
		{
			return GetMemoryInternal (value) as TWordMemory;
		}

		private bool EAX = false, ECX = false, EDX = false;

		/// <summary>
		/// Gets the spare register.
		/// </summary>
		/// <returns></returns>
		internal SharpOS.AOT.X86.R32Type GetSpareRegister ()
		{
			if (!EAX) {
				EAX = true;

				return R32.EAX;

			} else if (!ECX) {
				ECX = true;

				return R32.ECX;

			} else if (!EDX) {
				EDX = true;

				return R32.EDX;

			} else
				throw new EngineException ("No spare registers.");
		}

		/// <summary>
		/// Frees the spare register.
		/// </summary>
		/// <param name="register">The register.</param>
		internal void FreeSpareRegister (SharpOS.AOT.X86.R32Type register)
		{
			if (register == R32.EAX) {
				if (EAX)
					EAX = false;
				else
					throw new EngineException ("EAX is already free.");

			} else if (register == R32.ECX) {
				if (ECX)
					ECX = false;
				else
					throw new EngineException ("ECX is already free.");

			} else if (register == R32.EDX) {
				if (EDX)
					EDX = false;
				else
					throw new EngineException ("EDX is already free.");
			}
		}

		/// <summary>
		/// Get8s the bit register.
		/// </summary>
		/// <param name="register">The register.</param>
		/// <returns></returns>
		internal static R8Type Get8BitRegister (SharpOS.AOT.X86.R32Type register)
		{
			if (register == R32.EAX)
				return R8.AL;

			else if (register == R32.ECX)
				return R8.CL;

			else if (register == R32.EDX)
				return R8.DL;

			else if (register == R32.EBX)
				return R8.BL;

			else
				throw new EngineException ("'" + register + "' has no 8-Bit register.");
		}

		/// <summary>
		/// Get16s the bit register.
		/// </summary>
		/// <param name="register">The register.</param>
		/// <returns></returns>
		internal static R16Type Get16BitRegister (SharpOS.AOT.X86.R32Type register)
		{
			if (register == R32.EAX)
				return R16.AX;

			else if (register == R32.ECX)
				return R16.CX;

			else if (register == R32.EDX)
				return R16.DX;

			else if (register == R32.EBX)
				return R16.BX;

			else
				throw new EngineException ("'" + register + "' has no 16-Bit register.");
		}

		enum Registers : int {
			EBX = 0,
			ESI,
			EDI,

			EAX,
			EDX,
			ECX,

			AX,
			DX,
			CX,

			AH,
			DH,
			CH,

			AL,
			DL,
			CL
		}

		/// <summary>
		/// Gets the register.
		/// </summary>
		/// <param name="i">The i.</param>
		/// <returns></returns>
		internal static SharpOS.AOT.X86.R32Type GetRegister (int i)
		{
			switch ((Registers) i) {

			case Registers.EBX:
				return R32.EBX;

			case Registers.ESI:
				return R32.ESI;

			case Registers.EDI:
				return R32.EDI;

			case Registers.EAX:
				return R32.EAX;

			case Registers.EDX:
				return R32.EDX;

			case Registers.ECX:
				return R32.ECX;

			default:
				throw new EngineException ("'" + i.ToString () + "' is no valid register.");
			}
		}

		/// <summary>
		/// Gets the available registers count.
		/// </summary>
		/// <value>The available registers count.</value>
		public int AvailableRegistersCount
		{
			get
			{
				return 3;
			}
		}

		/// <summary>
		/// Gets the size of the int.
		/// </summary>
		/// <value>The size of the int.</value>
		public int IntSize
		{
			get
			{
				return 4;
			}
		}

		/// <summary>
		/// Spills the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public bool Spill (InternalType type)
		{
			if (type == InternalType.NotSet)
				throw new EngineException ("Size Type not set.");

			if (type == InternalType.I8
					|| type == InternalType.U8
					|| type == InternalType.R4
					|| type == InternalType.R8
					|| type == InternalType.ValueType)
				return true;

			return false;
		}

		Dictionary<string, string> strings = new Dictionary<string, string> ();
		Dictionary<string, string> utf7Strings = new Dictionary<string, string> ();

		/// <summary>
		/// Adds the string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		internal string AddString (string value)
		{
			string label;

			if (this.UTF7StringEncoding) {
				if (utf7Strings.ContainsKey (value))
					return utf7Strings [value];

				label = this.GetFreeResourceLabel;

				utf7Strings.Add (value, label);

				this.AddSymbol (new COFF.Static (label));

				data.LABEL (label);

				data.DATA (value);

				data.DATA ((byte) 0);

			} else {
				if (strings.ContainsKey (value))
					return strings [value];

				label = this.GetFreeResourceLabel;

				strings.Add (value, label);

				this.AddSymbol (new COFF.Static (label));

				data.LABEL (label);

				data.AddObjectFields (Mono.Cecil.Constants.String);

				// Length
				data.DATA ((uint) value.Length);

				// The string value
				data.UTF16 (value);

				// Trailing zero
				data.DATA ((ushort) 0);
			}

			return label;
		}

		/// <summary>
		/// Allocates of memory in the BSS Section.
		/// </summary>
		/// <param name="size">The size.</param>
		/// <returns>The label of the memory chunk.</returns>
		internal string BSSAlloc (uint size)
		{
			string label = this.GetFreeResourceLabel;

			return BSSAlloc (label, size);
		}

		internal string BSSAlloc (string label, uint size)
		{
			bss.ALIGN (ALIGNMENT);

			bss.LABEL (label);

			bss.TIMES (size, 0);

			return label;
		}

		private int resourceCounter = 0;

		/// <summary>
		/// Gets the get free resource label.
		/// </summary>
		/// <value>The get free resource label.</value>
		internal string GetFreeResourceLabel
		{
			get
			{
				return "Resource " + this.resourceCounter++;
			}
		}

		private int cmpCounter = 0;

		/// <summary>
		/// Gets the get CMP label.
		/// </summary>
		/// <value>The get CMP label.</value>
		internal string GetCMPLabel
		{
			get
			{
				return "CMP " + this.cmpCounter++;
			}
		}

		/// <summary>
		/// Formats the name of the label.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		internal static string FormatLabelName (string value)
		{
			return value;
		}
	}
}
