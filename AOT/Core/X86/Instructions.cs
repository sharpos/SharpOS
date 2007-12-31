//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.AOT.IR;

namespace SharpOS.AOT.X86 {
	public partial class Assembly {

		/// <summary>
		/// AAA 
		/// </summary>
		public void AAA ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AAA", "", null, null, null, null, new string [] { "37" }));
		}

		/// <summary>
		/// AAD 
		/// </summary>
		public void AAD ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AAD", "", null, null, null, null, new string [] { "D5", "0A" }));
		}

		/// <summary>
		/// AAD imm8
		/// </summary>
		public void AAD (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AAD", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "D5", "ib" }));
		}

		/// <summary>
		/// AAM 
		/// </summary>
		public void AAM ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AAM", "", null, null, null, null, new string [] { "D4", "0A" }));
		}

		/// <summary>
		/// AAM imm8
		/// </summary>
		public void AAM (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AAM", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "D4", "ib" }));
		}

		/// <summary>
		/// AAS 
		/// </summary>
		public void AAS ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AAS", "", null, null, null, null, new string [] { "3F" }));
		}

		/// <summary>
		/// ADC mem8,reg8
		/// </summary>
		public void ADC (ByteMemory target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "10", "/r" }));
		}

		/// <summary>
		/// ADC mem16,reg16
		/// </summary>
		public void ADC (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o16", "11", "/r" }));
		}

		/// <summary>
		/// ADC mem32,reg32
		/// </summary>
		public void ADC (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o32", "11", "/r" }));
		}

		/// <summary>
		/// ADC reg8,mem8
		/// </summary>
		public void ADC (R8Type target, ByteMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "12", "/r" }));
		}

		/// <summary>
		/// ADC reg16,mem16
		/// </summary>
		public void ADC (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "13", "/r" }));
		}

		/// <summary>
		/// ADC reg32,mem32
		/// </summary>
		public void ADC (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "13", "/r" }));
		}

		/// <summary>
		/// ADC mem8,imm8
		/// </summary>
		public void ADC (ByteMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "80", "/2", "ib" }));
		}

		/// <summary>
		/// ADC mem16,imm16
		/// </summary>
		public void ADC (WordMemory target, UInt16 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "81", "/2", "iw" }));
		}

		/// <summary>
		/// ADC mem32,imm32
		/// </summary>
		public void ADC (DWordMemory target, UInt32 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "81", "/2", "id" }));
		}

		/// <summary>
		/// ADC mem16,imm8
		/// </summary>
		public void ADC (WordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "83", "/2", "ib" }));
		}

		/// <summary>
		/// ADC mem32,imm8
		/// </summary>
		public void ADC (DWordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "83", "/2", "ib" }));
		}

		/// <summary>
		/// ADC rmreg8,reg8
		/// </summary>
		public void ADC (R8Type target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "10", "/r" }));
		}

		/// <summary>
		/// ADC rmreg16,reg16
		/// </summary>
		public void ADC (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o16", "11", "/r" }));
		}

		/// <summary>
		/// ADC rmreg32,reg32
		/// </summary>
		public void ADC (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o32", "11", "/r" }));
		}

		/// <summary>
		/// ADC rmreg8,imm8
		/// </summary>
		public void ADC (R8Type target, Byte source)
		{
			if (target == R8.AL)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC_AL", "AL" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "14", "ib" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "80", "/2", "ib" }));
			}
		}

		/// <summary>
		/// ADC rmreg16,imm16
		/// </summary>
		public void ADC (R16Type target, UInt16 source)
		{
			if (target == R16.AX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC_AX", "AX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o16", "15", "iw" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "81", "/2", "iw" }));
			}
		}

		/// <summary>
		/// ADC rmreg32,imm32
		/// </summary>
		public void ADC (R32Type target, UInt32 source)
		{
			if (target == R32.EAX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC_EAX", "EAX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o32", "15", "id" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "81", "/2", "id" }));
			}
		}

		/// <summary>
		/// ADC rmreg16,imm8
		/// </summary>
		public void ADC (R16Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "83", "/2", "ib" }));
		}

		/// <summary>
		/// ADC rmreg32,imm8
		/// </summary>
		public void ADC (R32Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADC", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "83", "/2", "ib" }));
		}

		/// <summary>
		/// ADD mem8,reg8
		/// </summary>
		public void ADD (ByteMemory target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "00", "/r" }));
		}

		/// <summary>
		/// ADD mem16,reg16
		/// </summary>
		public void ADD (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o16", "01", "/r" }));
		}

		/// <summary>
		/// ADD mem32,reg32
		/// </summary>
		public void ADD (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o32", "01", "/r" }));
		}

		/// <summary>
		/// ADD reg8,mem8
		/// </summary>
		public void ADD (R8Type target, ByteMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "02", "/r" }));
		}

		/// <summary>
		/// ADD reg16,mem16
		/// </summary>
		public void ADD (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "03", "/r" }));
		}

		/// <summary>
		/// ADD reg32,mem32
		/// </summary>
		public void ADD (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "03", "/r" }));
		}

		/// <summary>
		/// ADD mem8,imm8
		/// </summary>
		public void ADD (ByteMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "80", "/0", "ib" }));
		}

		/// <summary>
		/// ADD mem16,imm16
		/// </summary>
		public void ADD (WordMemory target, UInt16 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "81", "/0", "iw" }));
		}

		/// <summary>
		/// ADD mem32,imm32
		/// </summary>
		public void ADD (DWordMemory target, UInt32 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "81", "/0", "id" }));
		}

		/// <summary>
		/// ADD mem16,imm8
		/// </summary>
		public void ADD (WordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "83", "/0", "ib" }));
		}

		/// <summary>
		/// ADD mem32,imm8
		/// </summary>
		public void ADD (DWordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "83", "/0", "ib" }));
		}

		/// <summary>
		/// ADD rmreg8,reg8
		/// </summary>
		public void ADD (R8Type target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "00", "/r" }));
		}

		/// <summary>
		/// ADD rmreg16,reg16
		/// </summary>
		public void ADD (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o16", "01", "/r" }));
		}

		/// <summary>
		/// ADD rmreg32,reg32
		/// </summary>
		public void ADD (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o32", "01", "/r" }));
		}

		/// <summary>
		/// ADD rmreg8,imm8
		/// </summary>
		public void ADD (R8Type target, Byte source)
		{
			if (target == R8.AL)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD_AL", "AL" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "04", "ib" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "80", "/0", "ib" }));
			}
		}

		/// <summary>
		/// ADD rmreg16,imm16
		/// </summary>
		public void ADD (R16Type target, UInt16 source)
		{
			if (target == R16.AX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD_AX", "AX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o16", "05", "iw" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "81", "/0", "iw" }));
			}
		}

		/// <summary>
		/// ADD rmreg32,imm32
		/// </summary>
		public void ADD (R32Type target, UInt32 source)
		{
			if (target == R32.EAX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD_EAX", "EAX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o32", "05", "id" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "81", "/0", "id" }));
			}
		}

		/// <summary>
		/// ADD rmreg16,imm8
		/// </summary>
		public void ADD (R16Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "83", "/0", "ib" }));
		}

		/// <summary>
		/// ADD rmreg32,imm8
		/// </summary>
		public void ADD (R32Type target, Byte source)
		{
			if (source == 0) {
			} else if (source == 1) {
				this.INC (target);
			} else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ADD", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "83", "/0", "ib" }));
			}
		}

		/// <summary>
		/// AND mem8,reg8
		/// </summary>
		public void AND (ByteMemory target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "20", "/r" }));
		}

		/// <summary>
		/// AND mem16,reg16
		/// </summary>
		public void AND (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o16", "21", "/r" }));
		}

		/// <summary>
		/// AND mem32,reg32
		/// </summary>
		public void AND (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o32", "21", "/r" }));
		}

		/// <summary>
		/// AND reg8,mem8
		/// </summary>
		public void AND (R8Type target, ByteMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "22", "/r" }));
		}

		/// <summary>
		/// AND reg16,mem16
		/// </summary>
		public void AND (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "23", "/r" }));
		}

		/// <summary>
		/// AND reg32,mem32
		/// </summary>
		public void AND (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "23", "/r" }));
		}

		/// <summary>
		/// AND mem8,imm8
		/// </summary>
		public void AND (ByteMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "80", "/4", "ib" }));
		}

		/// <summary>
		/// AND mem16,imm16
		/// </summary>
		public void AND (WordMemory target, UInt16 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "81", "/4", "iw" }));
		}

		/// <summary>
		/// AND mem32,imm32
		/// </summary>
		public void AND (DWordMemory target, UInt32 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "81", "/4", "id" }));
		}

		/// <summary>
		/// AND mem16,imm8
		/// </summary>
		public void AND (WordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "83", "/4", "ib" }));
		}

		/// <summary>
		/// AND mem32,imm8
		/// </summary>
		public void AND (DWordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "83", "/4", "ib" }));
		}

		/// <summary>
		/// AND rmreg8,reg8
		/// </summary>
		public void AND (R8Type target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "20", "/r" }));
		}

		/// <summary>
		/// AND rmreg16,reg16
		/// </summary>
		public void AND (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o16", "21", "/r" }));
		}

		/// <summary>
		/// AND rmreg32,reg32
		/// </summary>
		public void AND (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o32", "21", "/r" }));
		}

		/// <summary>
		/// AND rmreg8,imm8
		/// </summary>
		public void AND (R8Type target, Byte source)
		{
			if (target == R8.AL)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND_AL", "AL" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "24", "ib" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "80", "/4", "ib" }));
			}
		}

		/// <summary>
		/// AND rmreg16,imm16
		/// </summary>
		public void AND (R16Type target, UInt16 source)
		{
			if (target == R16.AX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND_AX", "AX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o16", "25", "iw" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "81", "/4", "iw" }));
			}
		}

		/// <summary>
		/// AND rmreg32,imm32
		/// </summary>
		public void AND (R32Type target, UInt32 source)
		{
			if (target == R32.EAX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND_EAX", "EAX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o32", "25", "id" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "81", "/4", "id" }));
			}
		}

		/// <summary>
		/// AND rmreg16,imm8
		/// </summary>
		public void AND (R16Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "83", "/4", "ib" }));
		}

		/// <summary>
		/// AND rmreg32,imm8
		/// </summary>
		public void AND (R32Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "AND", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "83", "/4", "ib" }));
		}

		/// <summary>
		/// ARPL mem16,reg16
		/// </summary>
		public void ARPL (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ARPL", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "63", "/r" }));
		}

		/// <summary>
		/// ARPL rmreg16,reg16
		/// </summary>
		public void ARPL (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ARPL", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "63", "/r" }));
		}

		/// <summary>
		/// BOUND reg16,mem
		/// </summary>
		public void BOUND (R16Type target, Memory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BOUND", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "62", "/r" }));
		}

		/// <summary>
		/// BOUND reg32,mem
		/// </summary>
		public void BOUND (R32Type target, Memory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BOUND", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "62", "/r" }));
		}

		/// <summary>
		/// BSF reg16,mem16
		/// </summary>
		public void BSF (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BSF", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "BC", "/r" }));
		}

		/// <summary>
		/// BSF reg32,mem32
		/// </summary>
		public void BSF (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BSF", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "BC", "/r" }));
		}

		/// <summary>
		/// BSF reg16,rmreg16
		/// </summary>
		public void BSF (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BSF", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "BC", "/r" }));
		}

		/// <summary>
		/// BSF reg32,rmreg32
		/// </summary>
		public void BSF (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BSF", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "BC", "/r" }));
		}

		/// <summary>
		/// BSR reg16,mem16
		/// </summary>
		public void BSR (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BSR", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "BD", "/r" }));
		}

		/// <summary>
		/// BSR reg32,mem32
		/// </summary>
		public void BSR (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BSR", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "BD", "/r" }));
		}

		/// <summary>
		/// BSR reg16,rmreg16
		/// </summary>
		public void BSR (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BSR", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "BD", "/r" }));
		}

		/// <summary>
		/// BSR reg32,rmreg32
		/// </summary>
		public void BSR (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BSR", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "BD", "/r" }));
		}

		/// <summary>
		/// BSWAP reg32
		/// </summary>
		public void BSWAP (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BSWAP", target.ToString (), null, null, target, null, new string [] { "o32", "0F", "C8+r" }));
		}

		/// <summary>
		/// BT mem16,reg16
		/// </summary>
		public void BT (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BT", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o16", "0F", "A3", "/r" }));
		}

		/// <summary>
		/// BT mem32,reg32
		/// </summary>
		public void BT (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BT", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o32", "0F", "A3", "/r" }));
		}

		/// <summary>
		/// BT mem16,imm8
		/// </summary>
		public void BT (WordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BT", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "0F", "BA", "/4", "ib" }));
		}

		/// <summary>
		/// BT mem32,imm8
		/// </summary>
		public void BT (DWordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BT", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "0F", "BA", "/4", "ib" }));
		}

		/// <summary>
		/// BT rmreg16,reg16
		/// </summary>
		public void BT (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BT", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o16", "0F", "A3", "/r" }));
		}

		/// <summary>
		/// BT rmreg32,reg32
		/// </summary>
		public void BT (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BT", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o32", "0F", "A3", "/r" }));
		}

		/// <summary>
		/// BT rmreg16,imm8
		/// </summary>
		public void BT (R16Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BT", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "0F", "BA", "/4", "ib" }));
		}

		/// <summary>
		/// BT rmreg32,imm8
		/// </summary>
		public void BT (R32Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BT", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "0F", "BA", "/4", "ib" }));
		}

		/// <summary>
		/// BTC mem16,reg16
		/// </summary>
		public void BTC (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTC", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o16", "0F", "BB", "/r" }));
		}

		/// <summary>
		/// BTC mem32,reg32
		/// </summary>
		public void BTC (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTC", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o32", "0F", "BB", "/r" }));
		}

		/// <summary>
		/// BTC mem16,imm8
		/// </summary>
		public void BTC (WordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTC", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "0F", "BA", "/7", "ib" }));
		}

		/// <summary>
		/// BTC mem32,imm8
		/// </summary>
		public void BTC (DWordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTC", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "0F", "BA", "/7", "ib" }));
		}

		/// <summary>
		/// BTC rmreg16,reg16
		/// </summary>
		public void BTC (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTC", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o16", "0F", "BB", "/r" }));
		}

		/// <summary>
		/// BTC rmreg32,reg32
		/// </summary>
		public void BTC (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTC", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o32", "0F", "BB", "/r" }));
		}

		/// <summary>
		/// BTC rmreg16,imm8
		/// </summary>
		public void BTC (R16Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTC", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "0F", "BA", "/7", "ib" }));
		}

		/// <summary>
		/// BTC rmreg32,imm8
		/// </summary>
		public void BTC (R32Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTC", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "0F", "BA", "/7", "ib" }));
		}

		/// <summary>
		/// BTR mem16,reg16
		/// </summary>
		public void BTR (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTR", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o16", "0F", "B3", "/r" }));
		}

		/// <summary>
		/// BTR mem32,reg32
		/// </summary>
		public void BTR (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTR", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o32", "0F", "B3", "/r" }));
		}

		/// <summary>
		/// BTR mem16,imm8
		/// </summary>
		public void BTR (WordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "0F", "BA", "/6", "ib" }));
		}

		/// <summary>
		/// BTR mem32,imm8
		/// </summary>
		public void BTR (DWordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "0F", "BA", "/6", "ib" }));
		}

		/// <summary>
		/// BTR rmreg16,reg16
		/// </summary>
		public void BTR (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTR", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o16", "0F", "B3", "/r" }));
		}

		/// <summary>
		/// BTR rmreg32,reg32
		/// </summary>
		public void BTR (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTR", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o32", "0F", "B3", "/r" }));
		}

		/// <summary>
		/// BTR rmreg16,imm8
		/// </summary>
		public void BTR (R16Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "0F", "BA", "/6", "ib" }));
		}

		/// <summary>
		/// BTR rmreg32,imm8
		/// </summary>
		public void BTR (R32Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "0F", "BA", "/6", "ib" }));
		}

		/// <summary>
		/// BTS mem16,reg16
		/// </summary>
		public void BTS (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTS", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o16", "0F", "AB", "/r" }));
		}

		/// <summary>
		/// BTS mem32,reg32
		/// </summary>
		public void BTS (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTS", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o32", "0F", "AB", "/r" }));
		}

		/// <summary>
		/// BTS mem16,imm8
		/// </summary>
		public void BTS (WordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTS", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "0F", "BA", "/5", "ib" }));
		}

		/// <summary>
		/// BTS mem32,imm8
		/// </summary>
		public void BTS (DWordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTS", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "0F", "BA", "/5", "ib" }));
		}

		/// <summary>
		/// BTS rmreg16,reg16
		/// </summary>
		public void BTS (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTS", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o16", "0F", "AB", "/r" }));
		}

		/// <summary>
		/// BTS rmreg32,reg32
		/// </summary>
		public void BTS (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTS", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o32", "0F", "AB", "/r" }));
		}

		/// <summary>
		/// BTS rmreg16,imm8
		/// </summary>
		public void BTS (R16Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTS", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "0F", "BA", "/5", "ib" }));
		}

		/// <summary>
		/// BTS rmreg32,imm8
		/// </summary>
		public void BTS (R32Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "BTS", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "0F", "BA", "/5", "ib" }));
		}

		/// <summary>
		/// CALL imm
		/// </summary>
		public void CALL (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CALL", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "E8", "rw/rd" }));
		}

		/// <summary>
		/// CALL imm
		/// </summary>
		public void CALL (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "CALL", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "E8", "rw/rd" }));
		}

		/// <summary>
		/// CALL imm16:imm16
		/// </summary>
		public void CALL (UInt16 target, UInt16 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CALL", string.Format ("0x{0:x}", target) + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source, target }, new string [] { "o16", "9A", "iw", "iw" }));
		}

		/// <summary>
		/// CALL imm16:imm32
		/// </summary>
		public void CALL (UInt16 target, UInt32 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CALL", string.Format ("0x{0:x}", target) + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source, target }, new string [] { "o32", "9A", "id", "iw" }));
		}

		/// <summary>
		/// CALL FAR mem16
		/// </summary>
		public void CALL_FAR (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CALL_FAR", target.ToString (), target, null, null, null, new string [] { "o16", "FF", "/3" }));
		}

		/// <summary>
		/// CALL FAR mem32
		/// </summary>
		public void CALL_FAR (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CALL_FAR", target.ToString (), target, null, null, null, new string [] { "o32", "FF", "/3" }));
		}

		/// <summary>
		/// CALL mem16
		/// </summary>
		public void CALL (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CALL", target.ToString (), target, null, null, null, new string [] { "o16", "FF", "/2" }));
		}

		/// <summary>
		/// CALL mem32
		/// </summary>
		public void CALL (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CALL", target.ToString (), target, null, null, null, new string [] { "o32", "FF", "/2" }));
		}

		/// <summary>
		/// CALL rmreg16
		/// </summary>
		public void CALL (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CALL", target.ToString (), null, target, null, null, new string [] { "o16", "FF", "/2" }));
		}

		/// <summary>
		/// CALL rmreg32
		/// </summary>
		public void CALL (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CALL", target.ToString (), null, target, null, null, new string [] { "o32", "FF", "/2" }));
		}

		/// <summary>
		/// CBW 
		/// </summary>
		public void CBW ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CBW", "", null, null, null, null, new string [] { "o16", "98" }));
		}

		/// <summary>
		/// CDQ 
		/// </summary>
		public void CDQ ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CDQ", "", null, null, null, null, new string [] { "o32", "99" }));
		}

		/// <summary>
		/// CLC 
		/// </summary>
		public void CLC ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CLC", "", null, null, null, null, new string [] { "F8" }));
		}

		/// <summary>
		/// CLD 
		/// </summary>
		public void CLD ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CLD", "", null, null, null, null, new string [] { "FC" }));
		}

		/// <summary>
		/// CLFLUSH mem
		/// </summary>
		public void CLFLUSH (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CLFLUSH", target.ToString (), target, null, null, null, new string [] { "0F", "AE", "/7" }));
		}

		/// <summary>
		/// CLI 
		/// </summary>
		public void CLI ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CLI", "", null, null, null, null, new string [] { "FA" }));
		}

		/// <summary>
		/// CLTS 
		/// </summary>
		public void CLTS ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CLTS", "", null, null, null, null, new string [] { "0F", "06" }));
		}

		/// <summary>
		/// CMC 
		/// </summary>
		public void CMC ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMC", "", null, null, null, null, new string [] { "F5" }));
		}

		/// <summary>
		/// CMOVA reg16,mem16
		/// </summary>
		public void CMOVA (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVA", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "47", "/r" }));
		}

		/// <summary>
		/// CMOVA reg32,mem32
		/// </summary>
		public void CMOVA (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVA", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "47", "/r" }));
		}

		/// <summary>
		/// CMOVA reg16,rmreg16
		/// </summary>
		public void CMOVA (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVA", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "47", "/r" }));
		}

		/// <summary>
		/// CMOVA reg32,rmreg32
		/// </summary>
		public void CMOVA (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVA", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "47", "/r" }));
		}

		/// <summary>
		/// CMOVAE reg16,mem16
		/// </summary>
		public void CMOVAE (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVAE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "43", "/r" }));
		}

		/// <summary>
		/// CMOVAE reg32,mem32
		/// </summary>
		public void CMOVAE (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVAE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "43", "/r" }));
		}

		/// <summary>
		/// CMOVAE reg16,rmreg16
		/// </summary>
		public void CMOVAE (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVAE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "43", "/r" }));
		}

		/// <summary>
		/// CMOVAE reg32,rmreg32
		/// </summary>
		public void CMOVAE (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVAE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "43", "/r" }));
		}

		/// <summary>
		/// CMOVB reg16,mem16
		/// </summary>
		public void CMOVB (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVB", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "42", "/r" }));
		}

		/// <summary>
		/// CMOVB reg32,mem32
		/// </summary>
		public void CMOVB (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVB", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "42", "/r" }));
		}

		/// <summary>
		/// CMOVB reg16,rmreg16
		/// </summary>
		public void CMOVB (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVB", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "42", "/r" }));
		}

		/// <summary>
		/// CMOVB reg32,rmreg32
		/// </summary>
		public void CMOVB (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVB", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "42", "/r" }));
		}

		/// <summary>
		/// CMOVBE reg16,mem16
		/// </summary>
		public void CMOVBE (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVBE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "46", "/r" }));
		}

		/// <summary>
		/// CMOVBE reg32,mem32
		/// </summary>
		public void CMOVBE (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVBE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "46", "/r" }));
		}

		/// <summary>
		/// CMOVBE reg16,rmreg16
		/// </summary>
		public void CMOVBE (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVBE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "46", "/r" }));
		}

		/// <summary>
		/// CMOVBE reg32,rmreg32
		/// </summary>
		public void CMOVBE (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVBE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "46", "/r" }));
		}

		/// <summary>
		/// CMOVC reg16,mem16
		/// </summary>
		public void CMOVC (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVC", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "42", "/r" }));
		}

		/// <summary>
		/// CMOVC reg32,mem32
		/// </summary>
		public void CMOVC (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVC", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "42", "/r" }));
		}

		/// <summary>
		/// CMOVC reg16,rmreg16
		/// </summary>
		public void CMOVC (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVC", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "42", "/r" }));
		}

		/// <summary>
		/// CMOVC reg32,rmreg32
		/// </summary>
		public void CMOVC (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVC", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "42", "/r" }));
		}

		/// <summary>
		/// CMOVE reg16,mem16
		/// </summary>
		public void CMOVE (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "44", "/r" }));
		}

		/// <summary>
		/// CMOVE reg32,mem32
		/// </summary>
		public void CMOVE (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "44", "/r" }));
		}

		/// <summary>
		/// CMOVE reg16,rmreg16
		/// </summary>
		public void CMOVE (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "44", "/r" }));
		}

		/// <summary>
		/// CMOVE reg32,rmreg32
		/// </summary>
		public void CMOVE (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "44", "/r" }));
		}

		/// <summary>
		/// CMOVG reg16,mem16
		/// </summary>
		public void CMOVG (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVG", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "4F", "/r" }));
		}

		/// <summary>
		/// CMOVG reg32,mem32
		/// </summary>
		public void CMOVG (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVG", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "4F", "/r" }));
		}

		/// <summary>
		/// CMOVG reg16,rmreg16
		/// </summary>
		public void CMOVG (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVG", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "4F", "/r" }));
		}

		/// <summary>
		/// CMOVG reg32,rmreg32
		/// </summary>
		public void CMOVG (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVG", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "4F", "/r" }));
		}

		/// <summary>
		/// CMOVGE reg16,mem16
		/// </summary>
		public void CMOVGE (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVGE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "4D", "/r" }));
		}

		/// <summary>
		/// CMOVGE reg32,mem32
		/// </summary>
		public void CMOVGE (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVGE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "4D", "/r" }));
		}

		/// <summary>
		/// CMOVGE reg16,rmreg16
		/// </summary>
		public void CMOVGE (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVGE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "4D", "/r" }));
		}

		/// <summary>
		/// CMOVGE reg32,rmreg32
		/// </summary>
		public void CMOVGE (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVGE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "4D", "/r" }));
		}

		/// <summary>
		/// CMOVL reg16,mem16
		/// </summary>
		public void CMOVL (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVL", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "4C", "/r" }));
		}

		/// <summary>
		/// CMOVL reg32,mem32
		/// </summary>
		public void CMOVL (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVL", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "4C", "/r" }));
		}

		/// <summary>
		/// CMOVL reg16,rmreg16
		/// </summary>
		public void CMOVL (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVL", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "4C", "/r" }));
		}

		/// <summary>
		/// CMOVL reg32,rmreg32
		/// </summary>
		public void CMOVL (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVL", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "4C", "/r" }));
		}

		/// <summary>
		/// CMOVLE reg16,mem16
		/// </summary>
		public void CMOVLE (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVLE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "4E", "/r" }));
		}

		/// <summary>
		/// CMOVLE reg32,mem32
		/// </summary>
		public void CMOVLE (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVLE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "4E", "/r" }));
		}

		/// <summary>
		/// CMOVLE reg16,rmreg16
		/// </summary>
		public void CMOVLE (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVLE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "4E", "/r" }));
		}

		/// <summary>
		/// CMOVLE reg32,rmreg32
		/// </summary>
		public void CMOVLE (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVLE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "4E", "/r" }));
		}

		/// <summary>
		/// CMOVNA reg16,mem16
		/// </summary>
		public void CMOVNA (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNA", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "46", "/r" }));
		}

		/// <summary>
		/// CMOVNA reg32,mem32
		/// </summary>
		public void CMOVNA (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNA", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "46", "/r" }));
		}

		/// <summary>
		/// CMOVNA reg16,rmreg16
		/// </summary>
		public void CMOVNA (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNA", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "46", "/r" }));
		}

		/// <summary>
		/// CMOVNA reg32,rmreg32
		/// </summary>
		public void CMOVNA (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNA", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "46", "/r" }));
		}

		/// <summary>
		/// CMOVNAE reg16,mem16
		/// </summary>
		public void CMOVNAE (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNAE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "42", "/r" }));
		}

		/// <summary>
		/// CMOVNAE reg32,mem32
		/// </summary>
		public void CMOVNAE (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNAE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "42", "/r" }));
		}

		/// <summary>
		/// CMOVNAE reg16,rmreg16
		/// </summary>
		public void CMOVNAE (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNAE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "42", "/r" }));
		}

		/// <summary>
		/// CMOVNAE reg32,rmreg32
		/// </summary>
		public void CMOVNAE (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNAE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "42", "/r" }));
		}

		/// <summary>
		/// CMOVNB reg16,mem16
		/// </summary>
		public void CMOVNB (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNB", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "43", "/r" }));
		}

		/// <summary>
		/// CMOVNB reg32,mem32
		/// </summary>
		public void CMOVNB (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNB", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "43", "/r" }));
		}

		/// <summary>
		/// CMOVNB reg16,rmreg16
		/// </summary>
		public void CMOVNB (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNB", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "43", "/r" }));
		}

		/// <summary>
		/// CMOVNB reg32,rmreg32
		/// </summary>
		public void CMOVNB (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNB", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "43", "/r" }));
		}

		/// <summary>
		/// CMOVNBE reg16,mem16
		/// </summary>
		public void CMOVNBE (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNBE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "47", "/r" }));
		}

		/// <summary>
		/// CMOVNBE reg32,mem32
		/// </summary>
		public void CMOVNBE (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNBE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "47", "/r" }));
		}

		/// <summary>
		/// CMOVNBE reg16,rmreg16
		/// </summary>
		public void CMOVNBE (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNBE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "47", "/r" }));
		}

		/// <summary>
		/// CMOVNBE reg32,rmreg32
		/// </summary>
		public void CMOVNBE (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNBE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "47", "/r" }));
		}

		/// <summary>
		/// CMOVNC reg16,mem16
		/// </summary>
		public void CMOVNC (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNC", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "43", "/r" }));
		}

		/// <summary>
		/// CMOVNC reg32,mem32
		/// </summary>
		public void CMOVNC (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNC", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "43", "/r" }));
		}

		/// <summary>
		/// CMOVNC reg16,rmreg16
		/// </summary>
		public void CMOVNC (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNC", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "43", "/r" }));
		}

		/// <summary>
		/// CMOVNC reg32,rmreg32
		/// </summary>
		public void CMOVNC (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNC", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "43", "/r" }));
		}

		/// <summary>
		/// CMOVNE reg16,mem16
		/// </summary>
		public void CMOVNE (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "45", "/r" }));
		}

		/// <summary>
		/// CMOVNE reg32,mem32
		/// </summary>
		public void CMOVNE (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "45", "/r" }));
		}

		/// <summary>
		/// CMOVNE reg16,rmreg16
		/// </summary>
		public void CMOVNE (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "45", "/r" }));
		}

		/// <summary>
		/// CMOVNE reg32,rmreg32
		/// </summary>
		public void CMOVNE (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "45", "/r" }));
		}

		/// <summary>
		/// CMOVNG reg16,mem16
		/// </summary>
		public void CMOVNG (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNG", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "4E", "/r" }));
		}

		/// <summary>
		/// CMOVNG reg32,mem32
		/// </summary>
		public void CMOVNG (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNG", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "4E", "/r" }));
		}

		/// <summary>
		/// CMOVNG reg16,rmreg16
		/// </summary>
		public void CMOVNG (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNG", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "4E", "/r" }));
		}

		/// <summary>
		/// CMOVNG reg32,rmreg32
		/// </summary>
		public void CMOVNG (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNG", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "4E", "/r" }));
		}

		/// <summary>
		/// CMOVNGE reg16,mem16
		/// </summary>
		public void CMOVNGE (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNGE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "4C", "/r" }));
		}

		/// <summary>
		/// CMOVNGE reg32,mem32
		/// </summary>
		public void CMOVNGE (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNGE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "4C", "/r" }));
		}

		/// <summary>
		/// CMOVNGE reg16,rmreg16
		/// </summary>
		public void CMOVNGE (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNGE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "4C", "/r" }));
		}

		/// <summary>
		/// CMOVNGE reg32,rmreg32
		/// </summary>
		public void CMOVNGE (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNGE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "4C", "/r" }));
		}

		/// <summary>
		/// CMOVNL reg16,mem16
		/// </summary>
		public void CMOVNL (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNL", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "4D", "/r" }));
		}

		/// <summary>
		/// CMOVNL reg32,mem32
		/// </summary>
		public void CMOVNL (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNL", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "4D", "/r" }));
		}

		/// <summary>
		/// CMOVNL reg16,rmreg16
		/// </summary>
		public void CMOVNL (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNL", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "4D", "/r" }));
		}

		/// <summary>
		/// CMOVNL reg32,rmreg32
		/// </summary>
		public void CMOVNL (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNL", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "4D", "/r" }));
		}

		/// <summary>
		/// CMOVNLE reg16,mem16
		/// </summary>
		public void CMOVNLE (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNLE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "4F", "/r" }));
		}

		/// <summary>
		/// CMOVNLE reg32,mem32
		/// </summary>
		public void CMOVNLE (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNLE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "4F", "/r" }));
		}

		/// <summary>
		/// CMOVNLE reg16,rmreg16
		/// </summary>
		public void CMOVNLE (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNLE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "4F", "/r" }));
		}

		/// <summary>
		/// CMOVNLE reg32,rmreg32
		/// </summary>
		public void CMOVNLE (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNLE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "4F", "/r" }));
		}

		/// <summary>
		/// CMOVNO reg16,mem16
		/// </summary>
		public void CMOVNO (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNO", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "41", "/r" }));
		}

		/// <summary>
		/// CMOVNO reg32,mem32
		/// </summary>
		public void CMOVNO (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNO", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "41", "/r" }));
		}

		/// <summary>
		/// CMOVNO reg16,rmreg16
		/// </summary>
		public void CMOVNO (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNO", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "41", "/r" }));
		}

		/// <summary>
		/// CMOVNO reg32,rmreg32
		/// </summary>
		public void CMOVNO (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNO", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "41", "/r" }));
		}

		/// <summary>
		/// CMOVNP reg16,mem16
		/// </summary>
		public void CMOVNP (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNP", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "4B", "/r" }));
		}

		/// <summary>
		/// CMOVNP reg32,mem32
		/// </summary>
		public void CMOVNP (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNP", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "4B", "/r" }));
		}

		/// <summary>
		/// CMOVNP reg16,rmreg16
		/// </summary>
		public void CMOVNP (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNP", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "4B", "/r" }));
		}

		/// <summary>
		/// CMOVNP reg32,rmreg32
		/// </summary>
		public void CMOVNP (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNP", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "4B", "/r" }));
		}

		/// <summary>
		/// CMOVNS reg16,mem16
		/// </summary>
		public void CMOVNS (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNS", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "49", "/r" }));
		}

		/// <summary>
		/// CMOVNS reg32,mem32
		/// </summary>
		public void CMOVNS (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNS", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "49", "/r" }));
		}

		/// <summary>
		/// CMOVNS reg16,rmreg16
		/// </summary>
		public void CMOVNS (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNS", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "49", "/r" }));
		}

		/// <summary>
		/// CMOVNS reg32,rmreg32
		/// </summary>
		public void CMOVNS (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNS", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "49", "/r" }));
		}

		/// <summary>
		/// CMOVNZ reg16,mem16
		/// </summary>
		public void CMOVNZ (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNZ", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "45", "/r" }));
		}

		/// <summary>
		/// CMOVNZ reg32,mem32
		/// </summary>
		public void CMOVNZ (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNZ", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "45", "/r" }));
		}

		/// <summary>
		/// CMOVNZ reg16,rmreg16
		/// </summary>
		public void CMOVNZ (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNZ", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "45", "/r" }));
		}

		/// <summary>
		/// CMOVNZ reg32,rmreg32
		/// </summary>
		public void CMOVNZ (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVNZ", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "45", "/r" }));
		}

		/// <summary>
		/// CMOVO reg16,mem16
		/// </summary>
		public void CMOVO (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVO", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "40", "/r" }));
		}

		/// <summary>
		/// CMOVO reg32,mem32
		/// </summary>
		public void CMOVO (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVO", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "40", "/r" }));
		}

		/// <summary>
		/// CMOVO reg16,rmreg16
		/// </summary>
		public void CMOVO (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVO", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "40", "/r" }));
		}

		/// <summary>
		/// CMOVO reg32,rmreg32
		/// </summary>
		public void CMOVO (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVO", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "40", "/r" }));
		}

		/// <summary>
		/// CMOVP reg16,mem16
		/// </summary>
		public void CMOVP (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVP", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "4A", "/r" }));
		}

		/// <summary>
		/// CMOVP reg32,mem32
		/// </summary>
		public void CMOVP (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVP", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "4A", "/r" }));
		}

		/// <summary>
		/// CMOVP reg16,rmreg16
		/// </summary>
		public void CMOVP (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVP", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "4A", "/r" }));
		}

		/// <summary>
		/// CMOVP reg32,rmreg32
		/// </summary>
		public void CMOVP (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVP", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "4A", "/r" }));
		}

		/// <summary>
		/// CMOVPE reg16,mem16
		/// </summary>
		public void CMOVPE (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVPE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "4A", "/r" }));
		}

		/// <summary>
		/// CMOVPE reg32,mem32
		/// </summary>
		public void CMOVPE (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVPE", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "4A", "/r" }));
		}

		/// <summary>
		/// CMOVPE reg16,rmreg16
		/// </summary>
		public void CMOVPE (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVPE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "4A", "/r" }));
		}

		/// <summary>
		/// CMOVPE reg32,rmreg32
		/// </summary>
		public void CMOVPE (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVPE", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "4A", "/r" }));
		}

		/// <summary>
		/// CMOVPO reg16,mem16
		/// </summary>
		public void CMOVPO (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVPO", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "4B", "/r" }));
		}

		/// <summary>
		/// CMOVPO reg32,mem32
		/// </summary>
		public void CMOVPO (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVPO", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "4B", "/r" }));
		}

		/// <summary>
		/// CMOVPO reg16,rmreg16
		/// </summary>
		public void CMOVPO (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVPO", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "4B", "/r" }));
		}

		/// <summary>
		/// CMOVPO reg32,rmreg32
		/// </summary>
		public void CMOVPO (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVPO", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "4B", "/r" }));
		}

		/// <summary>
		/// CMOVS reg16,mem16
		/// </summary>
		public void CMOVS (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVS", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "48", "/r" }));
		}

		/// <summary>
		/// CMOVS reg32,mem32
		/// </summary>
		public void CMOVS (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVS", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "48", "/r" }));
		}

		/// <summary>
		/// CMOVS reg16,rmreg16
		/// </summary>
		public void CMOVS (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVS", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "48", "/r" }));
		}

		/// <summary>
		/// CMOVS reg32,rmreg32
		/// </summary>
		public void CMOVS (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVS", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "48", "/r" }));
		}

		/// <summary>
		/// CMOVZ reg16,mem16
		/// </summary>
		public void CMOVZ (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVZ", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "44", "/r" }));
		}

		/// <summary>
		/// CMOVZ reg32,mem32
		/// </summary>
		public void CMOVZ (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVZ", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "44", "/r" }));
		}

		/// <summary>
		/// CMOVZ reg16,rmreg16
		/// </summary>
		public void CMOVZ (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVZ", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "44", "/r" }));
		}

		/// <summary>
		/// CMOVZ reg32,rmreg32
		/// </summary>
		public void CMOVZ (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMOVZ", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "44", "/r" }));
		}

		/// <summary>
		/// CMP mem8,reg8
		/// </summary>
		public void CMP (ByteMemory target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "38", "/r" }));
		}

		/// <summary>
		/// CMP mem16,reg16
		/// </summary>
		public void CMP (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o16", "39", "/r" }));
		}

		/// <summary>
		/// CMP mem32,reg32
		/// </summary>
		public void CMP (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o32", "39", "/r" }));
		}

		/// <summary>
		/// CMP reg8,mem8
		/// </summary>
		public void CMP (R8Type target, ByteMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "3A", "/r" }));
		}

		/// <summary>
		/// CMP reg16,mem16
		/// </summary>
		public void CMP (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "3B", "/r" }));
		}

		/// <summary>
		/// CMP reg32,mem32
		/// </summary>
		public void CMP (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "3B", "/r" }));
		}

		/// <summary>
		/// CMP mem8,imm8
		/// </summary>
		public void CMP (ByteMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "80", "/7", "ib" }));
		}

		/// <summary>
		/// CMP mem16,imm16
		/// </summary>
		public void CMP (WordMemory target, UInt16 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "81", "/7", "iw" }));
		}

		/// <summary>
		/// CMP mem32,imm32
		/// </summary>
		public void CMP (DWordMemory target, UInt32 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "81", "/7", "id" }));
		}

		/// <summary>
		/// CMP mem16,imm8
		/// </summary>
		public void CMP (WordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "83", "/7", "ib" }));
		}

		/// <summary>
		/// CMP mem32,imm8
		/// </summary>
		public void CMP (DWordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "83", "/7", "ib" }));
		}

		/// <summary>
		/// CMP rmreg8,reg8
		/// </summary>
		public void CMP (R8Type target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "38", "/r" }));
		}

		/// <summary>
		/// CMP rmreg16,reg16
		/// </summary>
		public void CMP (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o16", "39", "/r" }));
		}

		/// <summary>
		/// CMP rmreg32,reg32
		/// </summary>
		public void CMP (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o32", "39", "/r" }));
		}

		/// <summary>
		/// CMP rmreg8,imm8
		/// </summary>
		public void CMP (R8Type target, Byte source)
		{
			if (target == R8.AL)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP_AL", "AL" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "3C", "ib" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "80", "/7", "ib" }));
			}
		}

		/// <summary>
		/// CMP rmreg16,imm16
		/// </summary>
		public void CMP (R16Type target, UInt16 source)
		{
			if (target == R16.AX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP_AX", "AX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o16", "3D", "iw" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "81", "/7", "iw" }));
			}
		}

		/// <summary>
		/// CMP rmreg32,imm32
		/// </summary>
		public void CMP (R32Type target, UInt32 source)
		{
			if (target == R32.EAX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP_EAX", "EAX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o32", "3D", "id" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "81", "/7", "id" }));
			}
		}

		/// <summary>
		/// CMP rmreg16,imm8
		/// </summary>
		public void CMP (R16Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "83", "/7", "ib" }));
		}

		/// <summary>
		/// CMP rmreg32,imm8
		/// </summary>
		public void CMP (R32Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMP", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "83", "/7", "ib" }));
		}

		/// <summary>
		/// CMPSB 
		/// </summary>
		public void CMPSB ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMPSB", "", null, null, null, null, new string [] { "A6" }));
		}

		/// <summary>
		/// CMPSD 
		/// </summary>
		public void CMPSD ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMPSD", "", null, null, null, null, new string [] { "o32", "A7" }));
		}

		/// <summary>
		/// CMPSW 
		/// </summary>
		public void CMPSW ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMPSW", "", null, null, null, null, new string [] { "o16", "A7" }));
		}

		/// <summary>
		/// CMPXCHG mem8,reg8
		/// </summary>
		public void CMPXCHG (ByteMemory target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMPXCHG", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "0F", "B0", "/r" }));
		}

		/// <summary>
		/// CMPXCHG mem16,reg16
		/// </summary>
		public void CMPXCHG (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMPXCHG", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o16", "0F", "B1", "/r" }));
		}

		/// <summary>
		/// CMPXCHG mem32,reg32
		/// </summary>
		public void CMPXCHG (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMPXCHG", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o32", "0F", "B1", "/r" }));
		}

		/// <summary>
		/// CMPXCHG rmreg8,reg8
		/// </summary>
		public void CMPXCHG (R8Type target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMPXCHG", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "0F", "B0", "/r" }));
		}

		/// <summary>
		/// CMPXCHG rmreg16,reg16
		/// </summary>
		public void CMPXCHG (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMPXCHG", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o16", "0F", "B1", "/r" }));
		}

		/// <summary>
		/// CMPXCHG rmreg32,reg32
		/// </summary>
		public void CMPXCHG (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMPXCHG", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o32", "0F", "B1", "/r" }));
		}

		/// <summary>
		/// CMPXCHG8B mem
		/// </summary>
		public void CMPXCHG8B (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CMPXCHG8B", target.ToString (), target, null, null, null, new string [] { "0F", "C7", "/1" }));
		}

		/// <summary>
		/// CPUID 
		/// </summary>
		public void CPUID ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CPUID", "", null, null, null, null, new string [] { "0F", "A2" }));
		}

		/// <summary>
		/// CWD 
		/// </summary>
		public void CWD ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CWD", "", null, null, null, null, new string [] { "o16", "99" }));
		}

		/// <summary>
		/// CWDE 
		/// </summary>
		public void CWDE ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "CWDE", "", null, null, null, null, new string [] { "o32", "98" }));
		}

		/// <summary>
		/// DAA 
		/// </summary>
		public void DAA ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "DAA", "", null, null, null, null, new string [] { "27" }));
		}

		/// <summary>
		/// DAS 
		/// </summary>
		public void DAS ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "DAS", "", null, null, null, null, new string [] { "2F" }));
		}

		/// <summary>
		/// DEC reg16
		/// </summary>
		public void DEC (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "DEC", target.ToString (), null, null, target, null, new string [] { "o16", "48+r" }));
		}

		/// <summary>
		/// DEC reg32
		/// </summary>
		public void DEC (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "DEC", target.ToString (), null, null, target, null, new string [] { "o32", "48+r" }));
		}

		/// <summary>
		/// DEC mem8
		/// </summary>
		public void DEC (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "DEC", target.ToString (), target, null, null, null, new string [] { "FE", "/1" }));
		}

		/// <summary>
		/// DEC mem16
		/// </summary>
		public void DEC (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "DEC", target.ToString (), target, null, null, null, new string [] { "o16", "FF", "/1" }));
		}

		/// <summary>
		/// DEC mem32
		/// </summary>
		public void DEC (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "DEC", target.ToString (), target, null, null, null, new string [] { "o32", "FF", "/1" }));
		}

		/// <summary>
		/// DEC rmreg8
		/// </summary>
		public void DEC (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "DEC", target.ToString (), null, target, null, null, new string [] { "FE", "/1" }));
		}

		/// <summary>
		/// DIV mem8
		/// </summary>
		public void DIV (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "DIV", target.ToString (), target, null, null, null, new string [] { "F6", "/6" }));
		}

		/// <summary>
		/// DIV mem16
		/// </summary>
		public void DIV (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "DIV", target.ToString (), target, null, null, null, new string [] { "o16", "F7", "/6" }));
		}

		/// <summary>
		/// DIV mem32
		/// </summary>
		public void DIV (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "DIV", target.ToString (), target, null, null, null, new string [] { "o32", "F7", "/6" }));
		}

		/// <summary>
		/// DIV rmreg8
		/// </summary>
		public void DIV (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "DIV", target.ToString (), null, target, null, null, new string [] { "F6", "/6" }));
		}

		/// <summary>
		/// DIV rmreg16
		/// </summary>
		public void DIV (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "DIV", target.ToString (), null, target, null, null, new string [] { "o16", "F7", "/6" }));
		}

		/// <summary>
		/// DIV rmreg32
		/// </summary>
		public void DIV (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "DIV", target.ToString (), null, target, null, null, new string [] { "o32", "F7", "/6" }));
		}

		/// <summary>
		/// EMMS 
		/// </summary>
		public void EMMS ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "EMMS", "", null, null, null, null, new string [] { "0F", "77" }));
		}

		/// <summary>
		/// ENTER imm16,imm8
		/// </summary>
		public void ENTER (UInt16 target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ENTER", string.Format ("0x{0:x}", target) + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { target, source }, new string [] { "C8", "iw", "ib" }));
		}

		/// <summary>
		/// F2XM1 
		/// </summary>
		public void F2XM1 ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "F2XM1", "", null, null, null, null, new string [] { "D9", "F0" }));
		}

		/// <summary>
		/// FABS 
		/// </summary>
		public void FABS ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FABS", "", null, null, null, null, new string [] { "D9", "E1" }));
		}

		/// <summary>
		/// FADD mem32
		/// </summary>
		public void FADD (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FADD", target.ToString (), target, null, null, null, new string [] { "D8", "/0" }));
		}

		/// <summary>
		/// FADD mem64
		/// </summary>
		public void FADD (QWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FADD", target.ToString (), target, null, null, null, new string [] { "DC", "/0" }));
		}

		/// <summary>
		/// FADD fpureg
		/// </summary>
		public void FADD (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FADD", target.ToString (), null, null, target, null, new string [] { "D8", "C0+r" }));
		}

		/// <summary>
		/// FADD ST0,fpureg
		/// </summary>
		public void FADD_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FADD_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "D8", "C0+r" }));
		}

		/// <summary>
		/// FADD fpureg,ST0
		/// </summary>
		public void FADD__ST0 (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FADD__ST0", target.ToString () + ", " + "ST0", null, null, target, null, new string [] { "DC", "C0+r" }));
		}

		/// <summary>
		/// FADDP fpureg
		/// </summary>
		public void FADDP (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FADDP", target.ToString (), null, null, target, null, new string [] { "DE", "C0+r" }));
		}

		/// <summary>
		/// FADDP fpureg,ST0
		/// </summary>
		public void FADDP__ST0 (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FADDP__ST0", target.ToString () + ", " + "ST0", null, null, target, null, new string [] { "DE", "C0+r" }));
		}

		/// <summary>
		/// FBLD mem80
		/// </summary>
		public void FBLD (TWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FBLD", target.ToString (), target, null, null, null, new string [] { "DF", "/4" }));
		}

		/// <summary>
		/// FBSTP mem80
		/// </summary>
		public void FBSTP (TWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FBSTP", target.ToString (), target, null, null, null, new string [] { "DF", "/6" }));
		}

		/// <summary>
		/// FCHS 
		/// </summary>
		public void FCHS ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCHS", "", null, null, null, null, new string [] { "D9", "E0" }));
		}

		/// <summary>
		/// FCLEX 
		/// </summary>
		public void FCLEX ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCLEX", "", null, null, null, null, new string [] { "9B", "DB", "E2" }));
		}

		/// <summary>
		/// FCMOVB fpureg
		/// </summary>
		public void FCMOVB (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCMOVB", target.ToString (), null, null, target, null, new string [] { "DA", "C0+r" }));
		}

		/// <summary>
		/// FCMOVB ST0,fpureg
		/// </summary>
		public void FCMOVB_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCMOVB_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "DA", "C0+r" }));
		}

		/// <summary>
		/// FCMOVBE fpureg
		/// </summary>
		public void FCMOVBE (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCMOVBE", target.ToString (), null, null, target, null, new string [] { "DA", "D0+r" }));
		}

		/// <summary>
		/// FCMOVBE ST0,fpureg
		/// </summary>
		public void FCMOVBE_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCMOVBE_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "DA", "D0+r" }));
		}

		/// <summary>
		/// FCMOVE fpureg
		/// </summary>
		public void FCMOVE (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCMOVE", target.ToString (), null, null, target, null, new string [] { "DA", "C8+r" }));
		}

		/// <summary>
		/// FCMOVE ST0,fpureg
		/// </summary>
		public void FCMOVE_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCMOVE_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "DA", "C8+r" }));
		}

		/// <summary>
		/// FCMOVNB fpureg
		/// </summary>
		public void FCMOVNB (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCMOVNB", target.ToString (), null, null, target, null, new string [] { "DB", "C0+r" }));
		}

		/// <summary>
		/// FCMOVNB ST0,fpureg
		/// </summary>
		public void FCMOVNB_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCMOVNB_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "DB", "C0+r" }));
		}

		/// <summary>
		/// FCMOVNBE fpureg
		/// </summary>
		public void FCMOVNBE (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCMOVNBE", target.ToString (), null, null, target, null, new string [] { "DB", "D0+r" }));
		}

		/// <summary>
		/// FCMOVNBE ST0,fpureg
		/// </summary>
		public void FCMOVNBE_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCMOVNBE_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "DB", "D0+r" }));
		}

		/// <summary>
		/// FCMOVNE fpureg
		/// </summary>
		public void FCMOVNE (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCMOVNE", target.ToString (), null, null, target, null, new string [] { "DB", "C8+r" }));
		}

		/// <summary>
		/// FCMOVNE ST0,fpureg
		/// </summary>
		public void FCMOVNE_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCMOVNE_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "DB", "C8+r" }));
		}

		/// <summary>
		/// FCMOVNU fpureg
		/// </summary>
		public void FCMOVNU (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCMOVNU", target.ToString (), null, null, target, null, new string [] { "DB", "D8+r" }));
		}

		/// <summary>
		/// FCMOVNU ST0,fpureg
		/// </summary>
		public void FCMOVNU_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCMOVNU_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "DB", "D8+r" }));
		}

		/// <summary>
		/// FCMOVU fpureg
		/// </summary>
		public void FCMOVU (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCMOVU", target.ToString (), null, null, target, null, new string [] { "DA", "D8+r" }));
		}

		/// <summary>
		/// FCMOVU ST0,fpureg
		/// </summary>
		public void FCMOVU_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCMOVU_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "DA", "D8+r" }));
		}

		/// <summary>
		/// FCOM mem32
		/// </summary>
		public void FCOM (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCOM", target.ToString (), target, null, null, null, new string [] { "D8", "/2" }));
		}

		/// <summary>
		/// FCOM mem64
		/// </summary>
		public void FCOM (QWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCOM", target.ToString (), target, null, null, null, new string [] { "DC", "/2" }));
		}

		/// <summary>
		/// FCOM fpureg
		/// </summary>
		public void FCOM (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCOM", target.ToString (), null, null, target, null, new string [] { "D8", "D0+r" }));
		}

		/// <summary>
		/// FCOM ST0,fpureg
		/// </summary>
		public void FCOM_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCOM_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "D8", "D0+r" }));
		}

		/// <summary>
		/// FCOMI fpureg
		/// </summary>
		public void FCOMI (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCOMI", target.ToString (), null, null, target, null, new string [] { "DB", "F0+r" }));
		}

		/// <summary>
		/// FCOMI ST0,fpureg
		/// </summary>
		public void FCOMI_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCOMI_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "DB", "F0+r" }));
		}

		/// <summary>
		/// FCOMIP fpureg
		/// </summary>
		public void FCOMIP (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCOMIP", target.ToString (), null, null, target, null, new string [] { "DF", "F0+r" }));
		}

		/// <summary>
		/// FCOMIP ST0,fpureg
		/// </summary>
		public void FCOMIP_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCOMIP_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "DF", "F0+r" }));
		}

		/// <summary>
		/// FCOMP mem32
		/// </summary>
		public void FCOMP (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCOMP", target.ToString (), target, null, null, null, new string [] { "D8", "/3" }));
		}

		/// <summary>
		/// FCOMP mem64
		/// </summary>
		public void FCOMP (QWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCOMP", target.ToString (), target, null, null, null, new string [] { "DC", "/3" }));
		}

		/// <summary>
		/// FCOMP fpureg
		/// </summary>
		public void FCOMP (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCOMP", target.ToString (), null, null, target, null, new string [] { "D8", "D8+r" }));
		}

		/// <summary>
		/// FCOMP ST0,fpureg
		/// </summary>
		public void FCOMP_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCOMP_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "D8", "D8+r" }));
		}

		/// <summary>
		/// FCOMPP 
		/// </summary>
		public void FCOMPP ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCOMPP", "", null, null, null, null, new string [] { "DE", "D9" }));
		}

		/// <summary>
		/// FCOS 
		/// </summary>
		public void FCOS ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FCOS", "", null, null, null, null, new string [] { "D9", "FF" }));
		}

		/// <summary>
		/// FDECSTP 
		/// </summary>
		public void FDECSTP ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FDECSTP", "", null, null, null, null, new string [] { "D9", "F6" }));
		}

		/// <summary>
		/// FDISI 
		/// </summary>
		public void FDISI ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FDISI", "", null, null, null, null, new string [] { "9B", "DB", "E1" }));
		}

		/// <summary>
		/// FDIV mem32
		/// </summary>
		public void FDIV (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FDIV", target.ToString (), target, null, null, null, new string [] { "D8", "/6" }));
		}

		/// <summary>
		/// FDIV mem64
		/// </summary>
		public void FDIV (QWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FDIV", target.ToString (), target, null, null, null, new string [] { "DC", "/6" }));
		}

		/// <summary>
		/// FDIV fpureg
		/// </summary>
		public void FDIV (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FDIV", target.ToString (), null, null, target, null, new string [] { "D8", "F0+r" }));
		}

		/// <summary>
		/// FDIV ST0,fpureg
		/// </summary>
		public void FDIV_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FDIV_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "D8", "F0+r" }));
		}

		/// <summary>
		/// FDIV fpureg,ST0
		/// </summary>
		public void FDIV__ST0 (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FDIV__ST0", target.ToString () + ", " + "ST0", null, null, target, null, new string [] { "DC", "F8+r" }));
		}

		/// <summary>
		/// FDIVP fpureg
		/// </summary>
		public void FDIVP (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FDIVP", target.ToString (), null, null, target, null, new string [] { "DE", "F8+r" }));
		}

		/// <summary>
		/// FDIVP fpureg,ST0
		/// </summary>
		public void FDIVP__ST0 (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FDIVP__ST0", target.ToString () + ", " + "ST0", null, null, target, null, new string [] { "DE", "F8+r" }));
		}

		/// <summary>
		/// FDIVR mem32
		/// </summary>
		public void FDIVR (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FDIVR", target.ToString (), target, null, null, null, new string [] { "D8", "/7" }));
		}

		/// <summary>
		/// FDIVR mem64
		/// </summary>
		public void FDIVR (QWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FDIVR", target.ToString (), target, null, null, null, new string [] { "DC", "/7" }));
		}

		/// <summary>
		/// FDIVR fpureg
		/// </summary>
		public void FDIVR (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FDIVR", target.ToString (), null, null, target, null, new string [] { "D8", "F8+r" }));
		}

		/// <summary>
		/// FDIVR ST0,fpureg
		/// </summary>
		public void FDIVR_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FDIVR_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "D8", "F8+r" }));
		}

		/// <summary>
		/// FDIVR fpureg,ST0
		/// </summary>
		public void FDIVR__ST0 (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FDIVR__ST0", target.ToString () + ", " + "ST0", null, null, target, null, new string [] { "DC", "F0+r" }));
		}

		/// <summary>
		/// FDIVRP fpureg
		/// </summary>
		public void FDIVRP (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FDIVRP", target.ToString (), null, null, target, null, new string [] { "DE", "F0+r" }));
		}

		/// <summary>
		/// FDIVRP fpureg,ST0
		/// </summary>
		public void FDIVRP__ST0 (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FDIVRP__ST0", target.ToString () + ", " + "ST0", null, null, target, null, new string [] { "DE", "F0+r" }));
		}

		/// <summary>
		/// FENI 
		/// </summary>
		public void FENI ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FENI", "", null, null, null, null, new string [] { "9B", "DB", "E0" }));
		}

		/// <summary>
		/// FFREE fpureg
		/// </summary>
		public void FFREE (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FFREE", target.ToString (), null, null, target, null, new string [] { "DD", "C0+r" }));
		}

		/// <summary>
		/// FFREEP fpureg
		/// </summary>
		public void FFREEP (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FFREEP", target.ToString (), null, null, target, null, new string [] { "DF", "C0+r" }));
		}

		/// <summary>
		/// FIADD mem16
		/// </summary>
		public void FIADD (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FIADD", target.ToString (), target, null, null, null, new string [] { "DE", "/0" }));
		}

		/// <summary>
		/// FIADD mem32
		/// </summary>
		public void FIADD (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FIADD", target.ToString (), target, null, null, null, new string [] { "DA", "/0" }));
		}

		/// <summary>
		/// FICOM mem16
		/// </summary>
		public void FICOM (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FICOM", target.ToString (), target, null, null, null, new string [] { "DE", "/2" }));
		}

		/// <summary>
		/// FICOM mem32
		/// </summary>
		public void FICOM (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FICOM", target.ToString (), target, null, null, null, new string [] { "DA", "/2" }));
		}

		/// <summary>
		/// FICOMP mem16
		/// </summary>
		public void FICOMP (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FICOMP", target.ToString (), target, null, null, null, new string [] { "DE", "/3" }));
		}

		/// <summary>
		/// FICOMP mem32
		/// </summary>
		public void FICOMP (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FICOMP", target.ToString (), target, null, null, null, new string [] { "DA", "/3" }));
		}

		/// <summary>
		/// FIDIV mem16
		/// </summary>
		public void FIDIV (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FIDIV", target.ToString (), target, null, null, null, new string [] { "DE", "/6" }));
		}

		/// <summary>
		/// FIDIV mem32
		/// </summary>
		public void FIDIV (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FIDIV", target.ToString (), target, null, null, null, new string [] { "DA", "/6" }));
		}

		/// <summary>
		/// FIDIVR mem16
		/// </summary>
		public void FIDIVR (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FIDIVR", target.ToString (), target, null, null, null, new string [] { "DE", "/7" }));
		}

		/// <summary>
		/// FIDIVR mem32
		/// </summary>
		public void FIDIVR (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FIDIVR", target.ToString (), target, null, null, null, new string [] { "DA", "/7" }));
		}

		/// <summary>
		/// FILD mem16
		/// </summary>
		public void FILD (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FILD", target.ToString (), target, null, null, null, new string [] { "DF", "/0" }));
		}

		/// <summary>
		/// FILD mem32
		/// </summary>
		public void FILD (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FILD", target.ToString (), target, null, null, null, new string [] { "DB", "/0" }));
		}

		/// <summary>
		/// FILD mem64
		/// </summary>
		public void FILD (QWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FILD", target.ToString (), target, null, null, null, new string [] { "DF", "/5" }));
		}

		/// <summary>
		/// FIMUL mem16
		/// </summary>
		public void FIMUL (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FIMUL", target.ToString (), target, null, null, null, new string [] { "DE", "/1" }));
		}

		/// <summary>
		/// FIMUL mem32
		/// </summary>
		public void FIMUL (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FIMUL", target.ToString (), target, null, null, null, new string [] { "DA", "/1" }));
		}

		/// <summary>
		/// FINCSTP 
		/// </summary>
		public void FINCSTP ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FINCSTP", "", null, null, null, null, new string [] { "D9", "F7" }));
		}

		/// <summary>
		/// FINIT 
		/// </summary>
		public void FINIT ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FINIT", "", null, null, null, null, new string [] { "9B", "DB", "E3" }));
		}

		/// <summary>
		/// FIST mem16
		/// </summary>
		public void FIST (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FIST", target.ToString (), target, null, null, null, new string [] { "DF", "/2" }));
		}

		/// <summary>
		/// FIST mem32
		/// </summary>
		public void FIST (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FIST", target.ToString (), target, null, null, null, new string [] { "DB", "/2" }));
		}

		/// <summary>
		/// FISTP mem16
		/// </summary>
		public void FISTP (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FISTP", target.ToString (), target, null, null, null, new string [] { "DF", "/3" }));
		}

		/// <summary>
		/// FISTP mem32
		/// </summary>
		public void FISTP (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FISTP", target.ToString (), target, null, null, null, new string [] { "DB", "/3" }));
		}

		/// <summary>
		/// FISTP mem64
		/// </summary>
		public void FISTP (QWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FISTP", target.ToString (), target, null, null, null, new string [] { "DF", "/7" }));
		}

		/// <summary>
		/// FISUB mem16
		/// </summary>
		public void FISUB (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FISUB", target.ToString (), target, null, null, null, new string [] { "DE", "/4" }));
		}

		/// <summary>
		/// FISUB mem32
		/// </summary>
		public void FISUB (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FISUB", target.ToString (), target, null, null, null, new string [] { "DA", "/4" }));
		}

		/// <summary>
		/// FISUBR mem16
		/// </summary>
		public void FISUBR (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FISUBR", target.ToString (), target, null, null, null, new string [] { "DE", "/5" }));
		}

		/// <summary>
		/// FISUBR mem32
		/// </summary>
		public void FISUBR (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FISUBR", target.ToString (), target, null, null, null, new string [] { "DA", "/5" }));
		}

		/// <summary>
		/// FLD mem32
		/// </summary>
		public void FLD (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FLD", target.ToString (), target, null, null, null, new string [] { "D9", "/0" }));
		}

		/// <summary>
		/// FLD mem64
		/// </summary>
		public void FLD (QWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FLD", target.ToString (), target, null, null, null, new string [] { "DD", "/0" }));
		}

		/// <summary>
		/// FLD mem80
		/// </summary>
		public void FLD (TWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FLD", target.ToString (), target, null, null, null, new string [] { "DB", "/5" }));
		}

		/// <summary>
		/// FLD fpureg
		/// </summary>
		public void FLD (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FLD", target.ToString (), null, null, target, null, new string [] { "D9", "C0+r" }));
		}

		/// <summary>
		/// FLD1 
		/// </summary>
		public void FLD1 ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FLD1", "", null, null, null, null, new string [] { "D9", "E8" }));
		}

		/// <summary>
		/// FLDCW mem16
		/// </summary>
		public void FLDCW (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FLDCW", target.ToString (), target, null, null, null, new string [] { "D9", "/5" }));
		}

		/// <summary>
		/// FLDENV mem
		/// </summary>
		public void FLDENV (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FLDENV", target.ToString (), target, null, null, null, new string [] { "D9", "/4" }));
		}

		/// <summary>
		/// FLDL2E 
		/// </summary>
		public void FLDL2E ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FLDL2E", "", null, null, null, null, new string [] { "D9", "EA" }));
		}

		/// <summary>
		/// FLDL2T 
		/// </summary>
		public void FLDL2T ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FLDL2T", "", null, null, null, null, new string [] { "D9", "E9" }));
		}

		/// <summary>
		/// FLDLG2 
		/// </summary>
		public void FLDLG2 ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FLDLG2", "", null, null, null, null, new string [] { "D9", "EC" }));
		}

		/// <summary>
		/// FLDLN2 
		/// </summary>
		public void FLDLN2 ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FLDLN2", "", null, null, null, null, new string [] { "D9", "ED" }));
		}

		/// <summary>
		/// FLDPI 
		/// </summary>
		public void FLDPI ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FLDPI", "", null, null, null, null, new string [] { "D9", "EB" }));
		}

		/// <summary>
		/// FLDZ 
		/// </summary>
		public void FLDZ ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FLDZ", "", null, null, null, null, new string [] { "D9", "EE" }));
		}

		/// <summary>
		/// FMUL mem32
		/// </summary>
		public void FMUL (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FMUL", target.ToString (), target, null, null, null, new string [] { "D8", "/1" }));
		}

		/// <summary>
		/// FMUL mem64
		/// </summary>
		public void FMUL (QWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FMUL", target.ToString (), target, null, null, null, new string [] { "DC", "/1" }));
		}

		/// <summary>
		/// FMUL fpureg
		/// </summary>
		public void FMUL (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FMUL", target.ToString (), null, null, target, null, new string [] { "D8", "C8+r" }));
		}

		/// <summary>
		/// FMUL ST0,fpureg
		/// </summary>
		public void FMUL_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FMUL_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "D8", "C8+r" }));
		}

		/// <summary>
		/// FMUL fpureg,ST0
		/// </summary>
		public void FMUL__ST0 (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FMUL__ST0", target.ToString () + ", " + "ST0", null, null, target, null, new string [] { "DC", "C8+r" }));
		}

		/// <summary>
		/// FMULP fpureg
		/// </summary>
		public void FMULP (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FMULP", target.ToString (), null, null, target, null, new string [] { "DE", "C8+r" }));
		}

		/// <summary>
		/// FMULP fpureg,ST0
		/// </summary>
		public void FMULP__ST0 (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FMULP__ST0", target.ToString () + ", " + "ST0", null, null, target, null, new string [] { "DE", "C8+r" }));
		}

		/// <summary>
		/// FNCLEX 
		/// </summary>
		public void FNCLEX ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FNCLEX", "", null, null, null, null, new string [] { "DB", "E2" }));
		}

		/// <summary>
		/// FNDISI 
		/// </summary>
		public void FNDISI ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FNDISI", "", null, null, null, null, new string [] { "DB", "E1" }));
		}

		/// <summary>
		/// FNENI 
		/// </summary>
		public void FNENI ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FNENI", "", null, null, null, null, new string [] { "DB", "E0" }));
		}

		/// <summary>
		/// FNINIT 
		/// </summary>
		public void FNINIT ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FNINIT", "", null, null, null, null, new string [] { "DB", "E3" }));
		}

		/// <summary>
		/// FNOP 
		/// </summary>
		public void FNOP ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FNOP", "", null, null, null, null, new string [] { "D9", "D0" }));
		}

		/// <summary>
		/// FNSAVE mem
		/// </summary>
		public void FNSAVE (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FNSAVE", target.ToString (), target, null, null, null, new string [] { "DD", "/6" }));
		}

		/// <summary>
		/// FNSTCW mem16
		/// </summary>
		public void FNSTCW (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FNSTCW", target.ToString (), target, null, null, null, new string [] { "D9", "/7" }));
		}

		/// <summary>
		/// FNSTENV mem
		/// </summary>
		public void FNSTENV (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FNSTENV", target.ToString (), target, null, null, null, new string [] { "D9", "/6" }));
		}

		/// <summary>
		/// FNSTSW mem16
		/// </summary>
		public void FNSTSW (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FNSTSW", target.ToString (), target, null, null, null, new string [] { "DD", "/7" }));
		}

		/// <summary>
		/// FNSTSW AX
		/// </summary>
		public void FNSTSW_AX ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FNSTSW_AX", "AX", null, null, null, null, new string [] { "DF", "E0" }));
		}

		/// <summary>
		/// FPATAN 
		/// </summary>
		public void FPATAN ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FPATAN", "", null, null, null, null, new string [] { "D9", "F3" }));
		}

		/// <summary>
		/// FPREM 
		/// </summary>
		public void FPREM ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FPREM", "", null, null, null, null, new string [] { "D9", "F8" }));
		}

		/// <summary>
		/// FPREM1 
		/// </summary>
		public void FPREM1 ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FPREM1", "", null, null, null, null, new string [] { "D9", "F5" }));
		}

		/// <summary>
		/// FPTAN 
		/// </summary>
		public void FPTAN ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FPTAN", "", null, null, null, null, new string [] { "D9", "F2" }));
		}

		/// <summary>
		/// FRNDINT 
		/// </summary>
		public void FRNDINT ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FRNDINT", "", null, null, null, null, new string [] { "D9", "FC" }));
		}

		/// <summary>
		/// FRSTOR mem
		/// </summary>
		public void FRSTOR (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FRSTOR", target.ToString (), target, null, null, null, new string [] { "DD", "/4" }));
		}

		/// <summary>
		/// FSAVE mem
		/// </summary>
		public void FSAVE (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSAVE", target.ToString (), target, null, null, null, new string [] { "9B", "DD", "/6" }));
		}

		/// <summary>
		/// FSCALE 
		/// </summary>
		public void FSCALE ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSCALE", "", null, null, null, null, new string [] { "D9", "FD" }));
		}

		/// <summary>
		/// FSETPM 
		/// </summary>
		public void FSETPM ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSETPM", "", null, null, null, null, new string [] { "DB", "E4" }));
		}

		/// <summary>
		/// FSIN 
		/// </summary>
		public void FSIN ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSIN", "", null, null, null, null, new string [] { "D9", "FE" }));
		}

		/// <summary>
		/// FSINCOS 
		/// </summary>
		public void FSINCOS ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSINCOS", "", null, null, null, null, new string [] { "D9", "FB" }));
		}

		/// <summary>
		/// FSQRT 
		/// </summary>
		public void FSQRT ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSQRT", "", null, null, null, null, new string [] { "D9", "FA" }));
		}

		/// <summary>
		/// FST mem32
		/// </summary>
		public void FST (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FST", target.ToString (), target, null, null, null, new string [] { "D9", "/2" }));
		}

		/// <summary>
		/// FST mem64
		/// </summary>
		public void FST (QWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FST", target.ToString (), target, null, null, null, new string [] { "DD", "/2" }));
		}

		/// <summary>
		/// FST fpureg
		/// </summary>
		public void FST (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FST", target.ToString (), null, null, target, null, new string [] { "DD", "D0+r" }));
		}

		/// <summary>
		/// FSTCW mem16
		/// </summary>
		public void FSTCW (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSTCW", target.ToString (), target, null, null, null, new string [] { "9B", "D9", "/7" }));
		}

		/// <summary>
		/// FSTENV mem
		/// </summary>
		public void FSTENV (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSTENV", target.ToString (), target, null, null, null, new string [] { "9B", "D9", "/6" }));
		}

		/// <summary>
		/// FSTP mem32
		/// </summary>
		public void FSTP (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSTP", target.ToString (), target, null, null, null, new string [] { "D9", "/3" }));
		}

		/// <summary>
		/// FSTP mem64
		/// </summary>
		public void FSTP (QWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSTP", target.ToString (), target, null, null, null, new string [] { "DD", "/3" }));
		}

		/// <summary>
		/// FSTP mem80
		/// </summary>
		public void FSTP (TWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSTP", target.ToString (), target, null, null, null, new string [] { "DB", "/7" }));
		}

		/// <summary>
		/// FSTP fpureg
		/// </summary>
		public void FSTP (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSTP", target.ToString (), null, null, target, null, new string [] { "DD", "D8+r" }));
		}

		/// <summary>
		/// FSTSW mem16
		/// </summary>
		public void FSTSW (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSTSW", target.ToString (), target, null, null, null, new string [] { "9B", "DD", "/7" }));
		}

		/// <summary>
		/// FSTSW AX
		/// </summary>
		public void FSTSW_AX ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSTSW_AX", "AX", null, null, null, null, new string [] { "9B", "DF", "E0" }));
		}

		/// <summary>
		/// FSUB mem32
		/// </summary>
		public void FSUB (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSUB", target.ToString (), target, null, null, null, new string [] { "D8", "/4" }));
		}

		/// <summary>
		/// FSUB mem64
		/// </summary>
		public void FSUB (QWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSUB", target.ToString (), target, null, null, null, new string [] { "DC", "/4" }));
		}

		/// <summary>
		/// FSUB fpureg
		/// </summary>
		public void FSUB (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSUB", target.ToString (), null, null, target, null, new string [] { "D8", "E0+r" }));
		}

		/// <summary>
		/// FSUB ST0,fpureg
		/// </summary>
		public void FSUB_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSUB_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "D8", "E0+r" }));
		}

		/// <summary>
		/// FSUB fpureg,ST0
		/// </summary>
		public void FSUB__ST0 (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSUB__ST0", target.ToString () + ", " + "ST0", null, null, target, null, new string [] { "DC", "E8+r" }));
		}

		/// <summary>
		/// FSUBP fpureg
		/// </summary>
		public void FSUBP (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSUBP", target.ToString (), null, null, target, null, new string [] { "DE", "E8+r" }));
		}

		/// <summary>
		/// FSUBP fpureg,ST0
		/// </summary>
		public void FSUBP__ST0 (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSUBP__ST0", target.ToString () + ", " + "ST0", null, null, target, null, new string [] { "DE", "E8+r" }));
		}

		/// <summary>
		/// FSUBR mem32
		/// </summary>
		public void FSUBR (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSUBR", target.ToString (), target, null, null, null, new string [] { "D8", "/5" }));
		}

		/// <summary>
		/// FSUBR mem64
		/// </summary>
		public void FSUBR (QWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSUBR", target.ToString (), target, null, null, null, new string [] { "DC", "/5" }));
		}

		/// <summary>
		/// FSUBR fpureg
		/// </summary>
		public void FSUBR (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSUBR", target.ToString (), null, null, target, null, new string [] { "D8", "E8+r" }));
		}

		/// <summary>
		/// FSUBR ST0,fpureg
		/// </summary>
		public void FSUBR_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSUBR_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "D8", "E8+r" }));
		}

		/// <summary>
		/// FSUBR fpureg,ST0
		/// </summary>
		public void FSUBR__ST0 (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSUBR__ST0", target.ToString () + ", " + "ST0", null, null, target, null, new string [] { "DC", "E0+r" }));
		}

		/// <summary>
		/// FSUBRP fpureg
		/// </summary>
		public void FSUBRP (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSUBRP", target.ToString (), null, null, target, null, new string [] { "DE", "E0+r" }));
		}

		/// <summary>
		/// FSUBRP fpureg,ST0
		/// </summary>
		public void FSUBRP__ST0 (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FSUBRP__ST0", target.ToString () + ", " + "ST0", null, null, target, null, new string [] { "DE", "E0+r" }));
		}

		/// <summary>
		/// FTST 
		/// </summary>
		public void FTST ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FTST", "", null, null, null, null, new string [] { "D9", "E4" }));
		}

		/// <summary>
		/// FUCOM fpureg
		/// </summary>
		public void FUCOM (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FUCOM", target.ToString (), null, null, target, null, new string [] { "DD", "E0+r" }));
		}

		/// <summary>
		/// FUCOM ST0,fpureg
		/// </summary>
		public void FUCOM_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FUCOM_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "DD", "E0+r" }));
		}

		/// <summary>
		/// FUCOMI fpureg
		/// </summary>
		public void FUCOMI (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FUCOMI", target.ToString (), null, null, target, null, new string [] { "DB", "E8+r" }));
		}

		/// <summary>
		/// FUCOMI ST0,fpureg
		/// </summary>
		public void FUCOMI_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FUCOMI_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "DB", "E8+r" }));
		}

		/// <summary>
		/// FUCOMIP fpureg
		/// </summary>
		public void FUCOMIP (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FUCOMIP", target.ToString (), null, null, target, null, new string [] { "DF", "E8+r" }));
		}

		/// <summary>
		/// FUCOMIP ST0,fpureg
		/// </summary>
		public void FUCOMIP_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FUCOMIP_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "DF", "E8+r" }));
		}

		/// <summary>
		/// FUCOMP fpureg
		/// </summary>
		public void FUCOMP (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FUCOMP", target.ToString (), null, null, target, null, new string [] { "DD", "E8+r" }));
		}

		/// <summary>
		/// FUCOMP ST0,fpureg
		/// </summary>
		public void FUCOMP_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FUCOMP_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "DD", "E8+r" }));
		}

		/// <summary>
		/// FUCOMPP 
		/// </summary>
		public void FUCOMPP ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FUCOMPP", "", null, null, null, null, new string [] { "DA", "E9" }));
		}

		/// <summary>
		/// FWAIT 
		/// </summary>
		public void FWAIT ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FWAIT", "", null, null, null, null, new string [] { "9B" }));
		}

		/// <summary>
		/// FXAM 
		/// </summary>
		public void FXAM ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FXAM", "", null, null, null, null, new string [] { "D9", "E5" }));
		}

		/// <summary>
		/// FXCH 
		/// </summary>
		public void FXCH ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FXCH", "", null, null, null, null, new string [] { "D9", "C9" }));
		}

		/// <summary>
		/// FXCH fpureg
		/// </summary>
		public void FXCH (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FXCH", target.ToString (), null, null, target, null, new string [] { "D9", "C8+r" }));
		}

		/// <summary>
		/// FXCH fpureg,ST0
		/// </summary>
		public void FXCH__ST0 (FPType target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FXCH__ST0", target.ToString () + ", " + "ST0", null, null, target, null, new string [] { "D9", "C8+r" }));
		}

		/// <summary>
		/// FXCH ST0,fpureg
		/// </summary>
		public void FXCH_ST0 (FPType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FXCH_ST0", "ST0" + ", " + source.ToString (), null, null, source, null, new string [] { "D9", "C8+r" }));
		}

		/// <summary>
		/// FXRSTOR memory
		/// </summary>
		public void FXRSTOR (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FXRSTOR", target.ToString (), target, null, null, null, new string [] { "0F", "AE", "/1" }));
		}

		/// <summary>
		/// FXSAVE memory
		/// </summary>
		public void FXSAVE (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FXSAVE", target.ToString (), target, null, null, null, new string [] { "0F", "AE", "/0" }));
		}

		/// <summary>
		/// FXTRACT 
		/// </summary>
		public void FXTRACT ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FXTRACT", "", null, null, null, null, new string [] { "D9", "F4" }));
		}

		/// <summary>
		/// FYL2X 
		/// </summary>
		public void FYL2X ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FYL2X", "", null, null, null, null, new string [] { "D9", "F1" }));
		}

		/// <summary>
		/// FYL2XP1 
		/// </summary>
		public void FYL2XP1 ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "FYL2XP1", "", null, null, null, null, new string [] { "D9", "F9" }));
		}

		/// <summary>
		/// HLT 
		/// </summary>
		public void HLT ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "HLT", "", null, null, null, null, new string [] { "F4" }));
		}

		/// <summary>
		/// ICEBP 
		/// </summary>
		public void ICEBP ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ICEBP", "", null, null, null, null, new string [] { "F1" }));
		}

		/// <summary>
		/// IDIV mem8
		/// </summary>
		public void IDIV (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IDIV", target.ToString (), target, null, null, null, new string [] { "F6", "/7" }));
		}

		/// <summary>
		/// IDIV mem16
		/// </summary>
		public void IDIV (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IDIV", target.ToString (), target, null, null, null, new string [] { "o16", "F7", "/7" }));
		}

		/// <summary>
		/// IDIV mem32
		/// </summary>
		public void IDIV (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IDIV", target.ToString (), target, null, null, null, new string [] { "o32", "F7", "/7" }));
		}

		/// <summary>
		/// IDIV rmreg8
		/// </summary>
		public void IDIV (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IDIV", target.ToString (), null, target, null, null, new string [] { "F6", "/7" }));
		}

		/// <summary>
		/// IDIV rmreg16
		/// </summary>
		public void IDIV (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IDIV", target.ToString (), null, target, null, null, new string [] { "o16", "F7", "/7" }));
		}

		/// <summary>
		/// IDIV rmreg32
		/// </summary>
		public void IDIV (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IDIV", target.ToString (), null, target, null, null, new string [] { "o32", "F7", "/7" }));
		}

		/// <summary>
		/// IMUL mem8
		/// </summary>
		public void IMUL (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString (), target, null, null, null, new string [] { "F6", "/5" }));
		}

		/// <summary>
		/// IMUL mem16
		/// </summary>
		public void IMUL (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString (), target, null, null, null, new string [] { "o16", "F7", "/5" }));
		}

		/// <summary>
		/// IMUL mem32
		/// </summary>
		public void IMUL (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString (), target, null, null, null, new string [] { "o32", "F7", "/5" }));
		}

		/// <summary>
		/// IMUL reg16,mem16
		/// </summary>
		public void IMUL (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "AF", "/r" }));
		}

		/// <summary>
		/// IMUL reg32,mem32
		/// </summary>
		public void IMUL (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "AF", "/r" }));
		}

		/// <summary>
		/// IMUL reg16,imm8
		/// </summary>
		public void IMUL (R16Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, null, target, new UInt32 [] { source }, new string [] { "o16", "6B", "/r", "ib" }));
		}

		/// <summary>
		/// IMUL reg16,imm16
		/// </summary>
		public void IMUL (R16Type target, UInt16 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, null, target, new UInt32 [] { source }, new string [] { "o16", "69", "/r", "iw" }));
		}

		/// <summary>
		/// IMUL reg32,imm8
		/// </summary>
		public void IMUL (R32Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, null, target, new UInt32 [] { source }, new string [] { "o32", "6B", "/r", "ib" }));
		}

		/// <summary>
		/// IMUL reg32,imm32
		/// </summary>
		public void IMUL (R32Type target, UInt32 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, null, target, new UInt32 [] { source }, new string [] { "o32", "69", "/r", "id" }));
		}

		/// <summary>
		/// IMUL reg16,mem16,imm8
		/// </summary>
		public void IMUL (R16Type target, WordMemory source, Byte value)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString () + ", " + source.ToString () + ", " + string.Format ("0x{0:x}", value), source, null, target, new UInt32 [] { value }, new string [] { "o16", "6B", "/r", "ib" }));
		}

		/// <summary>
		/// IMUL reg16,mem16,imm16
		/// </summary>
		public void IMUL (R16Type target, WordMemory source, UInt16 value)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString () + ", " + source.ToString () + ", " + string.Format ("0x{0:x}", value), source, null, target, new UInt32 [] { value }, new string [] { "o16", "69", "/r", "iw" }));
		}

		/// <summary>
		/// IMUL reg32,mem32,imm8
		/// </summary>
		public void IMUL (R32Type target, DWordMemory source, Byte value)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString () + ", " + source.ToString () + ", " + string.Format ("0x{0:x}", value), source, null, target, new UInt32 [] { value }, new string [] { "o32", "6B", "/r", "ib" }));
		}

		/// <summary>
		/// IMUL reg32,mem32,imm32
		/// </summary>
		public void IMUL (R32Type target, DWordMemory source, UInt32 value)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString () + ", " + source.ToString () + ", " + string.Format ("0x{0:x}", value), source, null, target, new UInt32 [] { value }, new string [] { "o32", "69", "/r", "id" }));
		}

		/// <summary>
		/// IMUL rmreg8
		/// </summary>
		public void IMUL (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString (), null, target, null, null, new string [] { "F6", "/5" }));
		}

		/// <summary>
		/// IMUL rmreg16
		/// </summary>
		public void IMUL (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString (), null, target, null, null, new string [] { "o16", "F7", "/5" }));
		}

		/// <summary>
		/// IMUL rmreg32
		/// </summary>
		public void IMUL (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString (), null, target, null, null, new string [] { "o32", "F7", "/5" }));
		}

		/// <summary>
		/// IMUL reg16,rmreg16
		/// </summary>
		public void IMUL (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "AF", "/r" }));
		}

		/// <summary>
		/// IMUL reg32,rmreg32
		/// </summary>
		public void IMUL (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "AF", "/r" }));
		}

		/// <summary>
		/// IMUL reg16,rmreg16,imm8
		/// </summary>
		public void IMUL (R16Type target, R16Type source, Byte value)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString () + ", " + source.ToString () + ", " + string.Format ("0x{0:x}", value), null, source, target, new UInt32 [] { value }, new string [] { "o16", "6B", "/r", "ib" }));
		}

		/// <summary>
		/// IMUL reg16,rmreg16,imm16
		/// </summary>
		public void IMUL (R16Type target, R16Type source, UInt16 value)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString () + ", " + source.ToString () + ", " + string.Format ("0x{0:x}", value), null, source, target, new UInt32 [] { value }, new string [] { "o16", "69", "/r", "iw" }));
		}

		/// <summary>
		/// IMUL reg32,rmreg32,imm8
		/// </summary>
		public void IMUL (R32Type target, R32Type source, Byte value)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString () + ", " + source.ToString () + ", " + string.Format ("0x{0:x}", value), null, source, target, new UInt32 [] { value }, new string [] { "o32", "6B", "/r", "ib" }));
		}

		/// <summary>
		/// IMUL reg32,rmreg32,imm32
		/// </summary>
		public void IMUL (R32Type target, R32Type source, UInt32 value)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IMUL", target.ToString () + ", " + source.ToString () + ", " + string.Format ("0x{0:x}", value), null, source, target, new UInt32 [] { value }, new string [] { "o32", "69", "/r", "id" }));
		}

		/// <summary>
		/// IN AL,imm8
		/// </summary>
		public void IN_AL (Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IN_AL", "AL" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "E4", "ib" }));
		}

		/// <summary>
		/// IN AX,imm8
		/// </summary>
		public void IN_AX (Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IN_AX", "AX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o16", "E5", "ib" }));
		}

		/// <summary>
		/// IN EAX,imm8
		/// </summary>
		public void IN_EAX (Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IN_EAX", "EAX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o32", "E5", "ib" }));
		}

		/// <summary>
		/// IN AL,DX
		/// </summary>
		public void IN_AL__DX ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IN_AL__DX", "AL" + ", " + "DX", null, null, null, null, new string [] { "EC" }));
		}

		/// <summary>
		/// IN AX,DX
		/// </summary>
		public void IN_AX__DX ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IN_AX__DX", "AX" + ", " + "DX", null, null, null, null, new string [] { "o16", "ED" }));
		}

		/// <summary>
		/// IN EAX,DX
		/// </summary>
		public void IN_EAX__DX ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IN_EAX__DX", "EAX" + ", " + "DX", null, null, null, null, new string [] { "o32", "ED" }));
		}

		/// <summary>
		/// INC reg16
		/// </summary>
		public void INC (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "INC", target.ToString (), null, null, target, null, new string [] { "o16", "40+r" }));
		}

		/// <summary>
		/// INC reg32
		/// </summary>
		public void INC (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "INC", target.ToString (), null, null, target, null, new string [] { "o32", "40+r" }));
		}

		/// <summary>
		/// INC mem8
		/// </summary>
		public void INC (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "INC", target.ToString (), target, null, null, null, new string [] { "FE", "/0" }));
		}

		/// <summary>
		/// INC mem16
		/// </summary>
		public void INC (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "INC", target.ToString (), target, null, null, null, new string [] { "o16", "FF", "/0" }));
		}

		/// <summary>
		/// INC mem32
		/// </summary>
		public void INC (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "INC", target.ToString (), target, null, null, null, new string [] { "o32", "FF", "/0" }));
		}

		/// <summary>
		/// INC rmreg8
		/// </summary>
		public void INC (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "INC", target.ToString (), null, target, null, null, new string [] { "FE", "/0" }));
		}

		/// <summary>
		/// INSB 
		/// </summary>
		public void INSB ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "INSB", "", null, null, null, null, new string [] { "6C" }));
		}

		/// <summary>
		/// INSD 
		/// </summary>
		public void INSD ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "INSD", "", null, null, null, null, new string [] { "o32", "6D" }));
		}

		/// <summary>
		/// INSW 
		/// </summary>
		public void INSW ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "INSW", "", null, null, null, null, new string [] { "o16", "6D" }));
		}

		/// <summary>
		/// INT imm8
		/// </summary>
		public void INT (Byte target)
		{
			if (target == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "INT_1", "1", null, null, null, null, new string [] { "F1" }));
			else if (target == 3)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "INT_3", "3", null, null, null, null, new string [] { "CC" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "INT", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "CD", "ib" }));
			}
		}

		/// <summary>
		/// INTO 
		/// </summary>
		public void INTO ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "INTO", "", null, null, null, null, new string [] { "CE" }));
		}

		/// <summary>
		/// INVD 
		/// </summary>
		public void INVD ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "INVD", "", null, null, null, null, new string [] { "0F", "08" }));
		}

		/// <summary>
		/// INVLPG mem
		/// </summary>
		public void INVLPG (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "INVLPG", target.ToString (), target, null, null, null, new string [] { "0F", "01", "/7" }));
		}

		/// <summary>
		/// IRET 
		/// </summary>
		public void IRET ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IRET", "", null, null, null, null, new string [] { "CF" }));
		}

		/// <summary>
		/// IRETD 
		/// </summary>
		public void IRETD ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IRETD", "", null, null, null, null, new string [] { "o32", "CF" }));
		}

		/// <summary>
		/// IRETW 
		/// </summary>
		public void IRETW ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "IRETW", "", null, null, null, null, new string [] { "o16", "CF" }));
		}

		/// <summary>
		/// JA imm8
		/// </summary>
		public void JA (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JA", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "77", "rb" }));
		}

		/// <summary>
		/// JA NEAR imm
		/// </summary>
		public void JA (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JA", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "87", "rw/rd" }));
		}

		/// <summary>
		/// JA NEAR imm
		/// </summary>
		public void JA (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JA", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "87", "rw/rd" }));
		}

		/// <summary>
		/// JAE imm8
		/// </summary>
		public void JAE (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JAE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "73", "rb" }));
		}

		/// <summary>
		/// JAE NEAR imm
		/// </summary>
		public void JAE (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JAE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "83", "rw/rd" }));
		}

		/// <summary>
		/// JAE NEAR imm
		/// </summary>
		public void JAE (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JAE", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "83", "rw/rd" }));
		}

		/// <summary>
		/// JB imm8
		/// </summary>
		public void JB (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JB", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "72", "rb" }));
		}

		/// <summary>
		/// JB NEAR imm
		/// </summary>
		public void JB (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JB", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "82", "rw/rd" }));
		}

		/// <summary>
		/// JB NEAR imm
		/// </summary>
		public void JB (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JB", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "82", "rw/rd" }));
		}

		/// <summary>
		/// JBE imm8
		/// </summary>
		public void JBE (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JBE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "76", "rb" }));
		}

		/// <summary>
		/// JBE NEAR imm
		/// </summary>
		public void JBE (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JBE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "86", "rw/rd" }));
		}

		/// <summary>
		/// JBE NEAR imm
		/// </summary>
		public void JBE (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JBE", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "86", "rw/rd" }));
		}

		/// <summary>
		/// JC imm8
		/// </summary>
		public void JC (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JC", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "72", "rb" }));
		}

		/// <summary>
		/// JC NEAR imm
		/// </summary>
		public void JC (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JC", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "82", "rw/rd" }));
		}

		/// <summary>
		/// JC NEAR imm
		/// </summary>
		public void JC (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JC", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "82", "rw/rd" }));
		}

		/// <summary>
		/// JCXZ imm8
		/// </summary>
		public void JCXZ (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JCXZ", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "a16", "E3", "rb" }));
		}

		/// <summary>
		/// JE imm8
		/// </summary>
		public void JE (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "74", "rb" }));
		}

		/// <summary>
		/// JE NEAR imm
		/// </summary>
		public void JE (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "84", "rw/rd" }));
		}

		/// <summary>
		/// JE NEAR imm
		/// </summary>
		public void JE (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JE", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "84", "rw/rd" }));
		}

		/// <summary>
		/// JECXZ imm8
		/// </summary>
		public void JECXZ (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JECXZ", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "a32", "E3", "rb" }));
		}

		/// <summary>
		/// JG imm8
		/// </summary>
		public void JG (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JG", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "7F", "rb" }));
		}

		/// <summary>
		/// JG NEAR imm
		/// </summary>
		public void JG (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JG", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "8F", "rw/rd" }));
		}

		/// <summary>
		/// JG NEAR imm
		/// </summary>
		public void JG (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JG", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "8F", "rw/rd" }));
		}

		/// <summary>
		/// JGE imm8
		/// </summary>
		public void JGE (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JGE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "7D", "rb" }));
		}

		/// <summary>
		/// JGE NEAR imm
		/// </summary>
		public void JGE (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JGE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "8D", "rw/rd" }));
		}

		/// <summary>
		/// JGE NEAR imm
		/// </summary>
		public void JGE (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JGE", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "8D", "rw/rd" }));
		}

		/// <summary>
		/// JL imm8
		/// </summary>
		public void JL (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JL", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "7C", "rb" }));
		}

		/// <summary>
		/// JL NEAR imm
		/// </summary>
		public void JL (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JL", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "8C", "rw/rd" }));
		}

		/// <summary>
		/// JL NEAR imm
		/// </summary>
		public void JL (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JL", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "8C", "rw/rd" }));
		}

		/// <summary>
		/// JLE imm8
		/// </summary>
		public void JLE (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JLE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "7E", "rb" }));
		}

		/// <summary>
		/// JLE NEAR imm
		/// </summary>
		public void JLE (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JLE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "8E", "rw/rd" }));
		}

		/// <summary>
		/// JLE NEAR imm
		/// </summary>
		public void JLE (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JLE", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "8E", "rw/rd" }));
		}

		/// <summary>
		/// JMP imm
		/// </summary>
		public void JMP (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JMP", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "E9", "rw/rd" }));
		}

		/// <summary>
		/// JMP imm
		/// </summary>
		public void JMP (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JMP", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "E9", "rw/rd" }));
		}

		/// <summary>
		/// JMP imm8
		/// </summary>
		public void JMP (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JMP", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "EB", "rb" }));
		}

		/// <summary>
		/// JMP imm16:imm16
		/// </summary>
		public void JMP (UInt16 target, UInt16 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JMP", string.Format ("0x{0:x}", target) + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source, target }, new string [] { "o16", "EA", "iw", "iw" }));
		}

		/// <summary>
		/// JMP imm16:imm32
		/// </summary>
		public void JMP (UInt16 target, UInt32 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JMP", string.Format ("0x{0:x}", target) + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source, target }, new string [] { "o32", "EA", "id", "iw" }));
		}

		/// <summary>
		/// JMP imm16:imm32
		/// </summary>
		public void JMP (ushort target, string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JMP", string.Format ("0x{0:x}", target) + ":" + Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0, target }, new string [] { "o32", "EA", "id", "iw" }));
		}

		/// <summary>
		/// JMP FAR mem
		/// </summary>
		public void JMP_FAR (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JMP_FAR", target.ToString (), target, null, null, null, new string [] { "o16", "FF", "/5" }));
		}

		/// <summary>
		/// JMP FAR mem32
		/// </summary>
		public void JMP_FAR (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JMP_FAR", target.ToString (), target, null, null, null, new string [] { "o32", "FF", "/5" }));
		}

		/// <summary>
		/// JMP mem16
		/// </summary>
		public void JMP (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JMP", target.ToString (), target, null, null, null, new string [] { "o16", "FF", "/4" }));
		}

		/// <summary>
		/// JMP mem32
		/// </summary>
		public void JMP (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JMP", target.ToString (), target, null, null, null, new string [] { "o32", "FF", "/4" }));
		}

		/// <summary>
		/// JMP rmreg16
		/// </summary>
		public void JMP (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JMP", target.ToString (), null, target, null, null, new string [] { "o16", "FF", "/4" }));
		}

		/// <summary>
		/// JMP rmreg32
		/// </summary>
		public void JMP (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JMP", target.ToString (), null, target, null, null, new string [] { "o32", "FF", "/4" }));
		}

		/// <summary>
		/// JNA imm8
		/// </summary>
		public void JNA (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNA", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "76", "rb" }));
		}

		/// <summary>
		/// JNA NEAR imm
		/// </summary>
		public void JNA (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNA", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "86", "rw/rd" }));
		}

		/// <summary>
		/// JNA NEAR imm
		/// </summary>
		public void JNA (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JNA", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "86", "rw/rd" }));
		}

		/// <summary>
		/// JNAE imm8
		/// </summary>
		public void JNAE (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNAE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "72", "rb" }));
		}

		/// <summary>
		/// JNAE NEAR imm
		/// </summary>
		public void JNAE (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNAE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "82", "rw/rd" }));
		}

		/// <summary>
		/// JNAE NEAR imm
		/// </summary>
		public void JNAE (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JNAE", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "82", "rw/rd" }));
		}

		/// <summary>
		/// JNB imm8
		/// </summary>
		public void JNB (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNB", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "73", "rb" }));
		}

		/// <summary>
		/// JNB NEAR imm
		/// </summary>
		public void JNB (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNB", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "83", "rw/rd" }));
		}

		/// <summary>
		/// JNB NEAR imm
		/// </summary>
		public void JNB (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JNB", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "83", "rw/rd" }));
		}

		/// <summary>
		/// JNBE imm8
		/// </summary>
		public void JNBE (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNBE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "77", "rb" }));
		}

		/// <summary>
		/// JNBE NEAR imm
		/// </summary>
		public void JNBE (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNBE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "87", "rw/rd" }));
		}

		/// <summary>
		/// JNBE NEAR imm
		/// </summary>
		public void JNBE (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JNBE", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "87", "rw/rd" }));
		}

		/// <summary>
		/// JNC imm8
		/// </summary>
		public void JNC (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNC", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "73", "rb" }));
		}

		/// <summary>
		/// JNC NEAR imm
		/// </summary>
		public void JNC (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNC", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "83", "rw/rd" }));
		}

		/// <summary>
		/// JNC NEAR imm
		/// </summary>
		public void JNC (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JNC", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "83", "rw/rd" }));
		}

		/// <summary>
		/// JNE imm8
		/// </summary>
		public void JNE (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "75", "rb" }));
		}

		/// <summary>
		/// JNE NEAR imm
		/// </summary>
		public void JNE (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "85", "rw/rd" }));
		}

		/// <summary>
		/// JNE NEAR imm
		/// </summary>
		public void JNE (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JNE", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "85", "rw/rd" }));
		}

		/// <summary>
		/// JNG imm8
		/// </summary>
		public void JNG (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNG", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "7E", "rb" }));
		}

		/// <summary>
		/// JNG NEAR imm
		/// </summary>
		public void JNG (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNG", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "8E", "rw/rd" }));
		}

		/// <summary>
		/// JNG NEAR imm
		/// </summary>
		public void JNG (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JNG", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "8E", "rw/rd" }));
		}

		/// <summary>
		/// JNGE imm8
		/// </summary>
		public void JNGE (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNGE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "7C", "rb" }));
		}

		/// <summary>
		/// JNGE NEAR imm
		/// </summary>
		public void JNGE (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNGE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "8C", "rw/rd" }));
		}

		/// <summary>
		/// JNGE NEAR imm
		/// </summary>
		public void JNGE (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JNGE", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "8C", "rw/rd" }));
		}

		/// <summary>
		/// JNL imm8
		/// </summary>
		public void JNL (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNL", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "7D", "rb" }));
		}

		/// <summary>
		/// JNL NEAR imm
		/// </summary>
		public void JNL (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNL", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "8D", "rw/rd" }));
		}

		/// <summary>
		/// JNL NEAR imm
		/// </summary>
		public void JNL (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JNL", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "8D", "rw/rd" }));
		}

		/// <summary>
		/// JNLE imm8
		/// </summary>
		public void JNLE (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNLE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "7F", "rb" }));
		}

		/// <summary>
		/// JNLE NEAR imm
		/// </summary>
		public void JNLE (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNLE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "8F", "rw/rd" }));
		}

		/// <summary>
		/// JNLE NEAR imm
		/// </summary>
		public void JNLE (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JNLE", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "8F", "rw/rd" }));
		}

		/// <summary>
		/// JNO imm8
		/// </summary>
		public void JNO (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNO", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "71", "rb" }));
		}

		/// <summary>
		/// JNO NEAR imm
		/// </summary>
		public void JNO (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNO", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "81", "rw/rd" }));
		}

		/// <summary>
		/// JNO NEAR imm
		/// </summary>
		public void JNO (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JNO", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "81", "rw/rd" }));
		}

		/// <summary>
		/// JNP imm8
		/// </summary>
		public void JNP (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNP", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "7B", "rb" }));
		}

		/// <summary>
		/// JNP NEAR imm
		/// </summary>
		public void JNP (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNP", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "8B", "rw/rd" }));
		}

		/// <summary>
		/// JNP NEAR imm
		/// </summary>
		public void JNP (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JNP", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "8B", "rw/rd" }));
		}

		/// <summary>
		/// JNS imm8
		/// </summary>
		public void JNS (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNS", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "79", "rb" }));
		}

		/// <summary>
		/// JNS NEAR imm
		/// </summary>
		public void JNS (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNS", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "89", "rw/rd" }));
		}

		/// <summary>
		/// JNS NEAR imm
		/// </summary>
		public void JNS (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JNS", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "89", "rw/rd" }));
		}

		/// <summary>
		/// JNZ imm8
		/// </summary>
		public void JNZ (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNZ", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "75", "rb" }));
		}

		/// <summary>
		/// JNZ NEAR imm
		/// </summary>
		public void JNZ (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JNZ", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "85", "rw/rd" }));
		}

		/// <summary>
		/// JNZ NEAR imm
		/// </summary>
		public void JNZ (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JNZ", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "85", "rw/rd" }));
		}

		/// <summary>
		/// JO imm8
		/// </summary>
		public void JO (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JO", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "70", "rb" }));
		}

		/// <summary>
		/// JO NEAR imm
		/// </summary>
		public void JO (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JO", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "80", "rw/rd" }));
		}

		/// <summary>
		/// JO NEAR imm
		/// </summary>
		public void JO (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JO", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "80", "rw/rd" }));
		}

		/// <summary>
		/// JP imm8
		/// </summary>
		public void JP (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JP", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "7A", "rb" }));
		}

		/// <summary>
		/// JP NEAR imm
		/// </summary>
		public void JP (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JP", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "8A", "rw/rd" }));
		}

		/// <summary>
		/// JP NEAR imm
		/// </summary>
		public void JP (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JP", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "8A", "rw/rd" }));
		}

		/// <summary>
		/// JPE imm8
		/// </summary>
		public void JPE (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JPE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "7A", "rb" }));
		}

		/// <summary>
		/// JPE NEAR imm
		/// </summary>
		public void JPE (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JPE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "8A", "rw/rd" }));
		}

		/// <summary>
		/// JPE NEAR imm
		/// </summary>
		public void JPE (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JPE", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "8A", "rw/rd" }));
		}

		/// <summary>
		/// JPO imm8
		/// </summary>
		public void JPO (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JPO", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "7B", "rb" }));
		}

		/// <summary>
		/// JPO NEAR imm
		/// </summary>
		public void JPO (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JPO", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "8B", "rw/rd" }));
		}

		/// <summary>
		/// JPO NEAR imm
		/// </summary>
		public void JPO (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JPO", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "8B", "rw/rd" }));
		}

		/// <summary>
		/// JS imm8
		/// </summary>
		public void JS (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JS", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "78", "rb" }));
		}

		/// <summary>
		/// JS NEAR imm
		/// </summary>
		public void JS (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JS", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "88", "rw/rd" }));
		}

		/// <summary>
		/// JS NEAR imm
		/// </summary>
		public void JS (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JS", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "88", "rw/rd" }));
		}

		/// <summary>
		/// JZ imm8
		/// </summary>
		public void JZ (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JZ", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "74", "rb" }));
		}

		/// <summary>
		/// JZ NEAR imm
		/// </summary>
		public void JZ (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "JZ", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "0F", "84", "rw/rd" }));
		}

		/// <summary>
		/// JZ NEAR imm
		/// </summary>
		public void JZ (string label)
		{
			this.instructions.Add (new Instruction (true, string.Empty, Assembly.FormatLabelName (label), "JZ", Assembly.FormatLabelName (label), null, null, null, new UInt32 [] { 0 }, new string [] { "0F", "84", "rw/rd" }));
		}

		/// <summary>
		/// LAHF 
		/// </summary>
		public void LAHF ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LAHF", "", null, null, null, null, new string [] { "9F" }));
		}

		/// <summary>
		/// LAR reg16,mem16
		/// </summary>
		public void LAR (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LAR", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "02", "/r" }));
		}

		/// <summary>
		/// LAR reg32,mem32
		/// </summary>
		public void LAR (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LAR", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "02", "/r" }));
		}

		/// <summary>
		/// LAR reg16,rmreg16
		/// </summary>
		public void LAR (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LAR", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "02", "/r" }));
		}

		/// <summary>
		/// LAR reg32,rmreg32
		/// </summary>
		public void LAR (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LAR", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "02", "/r" }));
		}

		/// <summary>
		/// LDS reg16,mem
		/// </summary>
		public void LDS (R16Type target, Memory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LDS", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "C5", "/r" }));
		}

		/// <summary>
		/// LDS reg32,mem
		/// </summary>
		public void LDS (R32Type target, Memory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LDS", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "C5", "/r" }));
		}

		/// <summary>
		/// LEA reg16,mem
		/// </summary>
		public void LEA (R16Type target, Memory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LEA", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "8D", "/r" }));
		}

		/// <summary>
		/// LEA reg32,mem
		/// </summary>
		public void LEA (R32Type target, Memory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LEA", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "8D", "/r" }));
		}

		/// <summary>
		/// LEAVE 
		/// </summary>
		public void LEAVE ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LEAVE", "", null, null, null, null, new string [] { "C9" }));
		}

		/// <summary>
		/// LES reg16,mem
		/// </summary>
		public void LES (R16Type target, Memory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LES", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "C4", "/r" }));
		}

		/// <summary>
		/// LES reg32,mem
		/// </summary>
		public void LES (R32Type target, Memory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LES", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "C4", "/r" }));
		}

		/// <summary>
		/// LFENCE 
		/// </summary>
		public void LFENCE ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LFENCE", "", null, null, null, null, new string [] { "0F", "AE", "/5" }));
		}

		/// <summary>
		/// LFS reg16,mem
		/// </summary>
		public void LFS (R16Type target, Memory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LFS", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "B4", "/r" }));
		}

		/// <summary>
		/// LFS reg32,mem
		/// </summary>
		public void LFS (R32Type target, Memory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LFS", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "B4", "/r" }));
		}

		/// <summary>
		/// LGDT mem
		/// </summary>
		public void LGDT (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LGDT", target.ToString (), target, null, null, null, new string [] { "0F", "01", "/2" }));
		}

		/// <summary>
		/// LGS reg16,mem
		/// </summary>
		public void LGS (R16Type target, Memory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LGS", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "B5", "/r" }));
		}

		/// <summary>
		/// LGS reg32,mem
		/// </summary>
		public void LGS (R32Type target, Memory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LGS", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "B5", "/r" }));
		}

		/// <summary>
		/// LIDT mem
		/// </summary>
		public void LIDT (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LIDT", target.ToString (), target, null, null, null, new string [] { "0F", "01", "/3" }));
		}

		/// <summary>
		/// LLDT mem16
		/// </summary>
		public void LLDT (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LLDT", target.ToString (), target, null, null, null, new string [] { "0F", "00", "/2" }));
		}

		/// <summary>
		/// LLDT rmreg16
		/// </summary>
		public void LLDT (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LLDT", target.ToString (), null, target, null, null, new string [] { "0F", "00", "/2" }));
		}

		/// <summary>
		/// LMSW mem16
		/// </summary>
		public void LMSW (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LMSW", target.ToString (), target, null, null, null, new string [] { "0F", "01", "/6" }));
		}

		/// <summary>
		/// LMSW rmreg16
		/// </summary>
		public void LMSW (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LMSW", target.ToString (), null, target, null, null, new string [] { "0F", "01", "/6" }));
		}

		/// <summary>
		/// LOCK 
		/// </summary>
		public void LOCK ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LOCK", "", null, null, null, null, new string [] { "F0" }));
		}

		/// <summary>
		/// LODSB 
		/// </summary>
		public void LODSB ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LODSB", "", null, null, null, null, new string [] { "AC" }));
		}

		/// <summary>
		/// LODSD 
		/// </summary>
		public void LODSD ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LODSD", "", null, null, null, null, new string [] { "o32", "AD" }));
		}

		/// <summary>
		/// LODSW 
		/// </summary>
		public void LODSW ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LODSW", "", null, null, null, null, new string [] { "o16", "AD" }));
		}

		/// <summary>
		/// LOOP imm8
		/// </summary>
		public void LOOP (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LOOP", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "E2", "rb" }));
		}

		/// <summary>
		/// LOOPE imm8
		/// </summary>
		public void LOOPE (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LOOPE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "E1", "rb" }));
		}

		/// <summary>
		/// LOOPNE imm8
		/// </summary>
		public void LOOPNE (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LOOPNE", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "E0", "rb" }));
		}

		/// <summary>
		/// LOOPNZ imm8
		/// </summary>
		public void LOOPNZ (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LOOPNZ", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "E0", "rb" }));
		}

		/// <summary>
		/// LOOPZ imm8
		/// </summary>
		public void LOOPZ (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LOOPZ", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "E1", "rb" }));
		}

		/// <summary>
		/// LSL reg16,mem16
		/// </summary>
		public void LSL (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LSL", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "03", "/r" }));
		}

		/// <summary>
		/// LSL reg32,mem32
		/// </summary>
		public void LSL (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LSL", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "03", "/r" }));
		}

		/// <summary>
		/// LSL reg16,rmreg16
		/// </summary>
		public void LSL (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LSL", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "03", "/r" }));
		}

		/// <summary>
		/// LSL reg32,rmreg32
		/// </summary>
		public void LSL (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LSL", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "03", "/r" }));
		}

		/// <summary>
		/// LSS reg16,mem
		/// </summary>
		public void LSS (R16Type target, Memory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LSS", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "B2", "/r" }));
		}

		/// <summary>
		/// LSS reg32,mem
		/// </summary>
		public void LSS (R32Type target, Memory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LSS", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "B2", "/r" }));
		}

		/// <summary>
		/// LTR mem16
		/// </summary>
		public void LTR (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LTR", target.ToString (), target, null, null, null, new string [] { "0F", "00", "/3" }));
		}

		/// <summary>
		/// LTR rmreg16
		/// </summary>
		public void LTR (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "LTR", target.ToString (), null, target, null, null, new string [] { "0F", "00", "/3" }));
		}

		/// <summary>
		/// MFENCE 
		/// </summary>
		public void MFENCE ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MFENCE", "", null, null, null, null, new string [] { "0F", "AE", "/6" }));
		}

		/// <summary>
		/// MOV mem8,reg8
		/// </summary>
		public void MOV (ByteMemory target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "88", "/r" }));
		}

		/// <summary>
		/// MOV mem16,reg16
		/// </summary>
		public void MOV (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o16", "89", "/r" }));
		}

		/// <summary>
		/// MOV mem32,reg32
		/// </summary>
		public void MOV (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o32", "89", "/r" }));
		}

		/// <summary>
		/// MOV reg8,mem8
		/// </summary>
		public void MOV (R8Type target, ByteMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "8A", "/r" }));
		}

		/// <summary>
		/// MOV reg16,mem16
		/// </summary>
		public void MOV (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "8B", "/r" }));
		}

		/// <summary>
		/// MOV reg32,mem32
		/// </summary>
		public void MOV (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "8B", "/r" }));
		}

		/// <summary>
		/// MOV reg8,imm8
		/// </summary>
		public void MOV (R8Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, null, target, new UInt32 [] { source }, new string [] { "B0+r", "ib" }));
		}

		/// <summary>
		/// MOV reg16,imm16
		/// </summary>
		public void MOV (R16Type target, UInt16 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, null, target, new UInt32 [] { source }, new string [] { "o16", "B8+r", "iw" }));
		}

		/// <summary>
		/// MOV reg32,imm32
		/// </summary>
		public void MOV (R32Type target, UInt32 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, null, target, new UInt32 [] { source }, new string [] { "o32", "B8+r", "id" }));
		}

		/// <summary>
		/// MOV mem8,imm8
		/// </summary>
		public void MOV (ByteMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "C6", "/0", "ib" }));
		}

		/// <summary>
		/// MOV mem16,imm16
		/// </summary>
		public void MOV (WordMemory target, UInt16 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "C7", "/0", "iw" }));
		}

		/// <summary>
		/// MOV mem32,imm32
		/// </summary>
		public void MOV (DWordMemory target, UInt32 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "C7", "/0", "id" }));
		}

		/// <summary>
		/// MOV AL,memoffs8
		/// </summary>
		public void MOV_AL (byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV_AL", "AL" + ", " + source.ToString (), null, null, null, new UInt32 [] { source }, new string [] { "A0", "ow/od" }));
		}

		/// <summary>
		/// MOV AX,memoffs16
		/// </summary>
		public void MOV_AX (UInt16 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV_AX", "AX" + ", " + source.ToString (), null, null, null, new UInt32 [] { source }, new string [] { "o16", "A1", "ow/od" }));
		}

		/// <summary>
		/// MOV EAX,memoffs32
		/// </summary>
		public void MOV_EAX (UInt32 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV_EAX", "EAX" + ", " + source.ToString (), null, null, null, new UInt32 [] { source }, new string [] { "o32", "A1", "ow/od" }));
		}

		/// <summary>
		/// MOV memoffs8,AL
		/// </summary>
		public void MOV__AL (byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV__AL", target.ToString () + ", " + "AL", null, null, null, new UInt32 [] { target }, new string [] { "A2", "ow/od" }));
		}

		/// <summary>
		/// MOV memoffs16,AX
		/// </summary>
		public void MOV__AX (UInt16 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV__AX", target.ToString () + ", " + "AX", null, null, null, new UInt32 [] { target }, new string [] { "o16", "A3", "ow/od" }));
		}

		/// <summary>
		/// MOV memoffs32,EAX
		/// </summary>
		public void MOV__EAX (UInt32 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV__EAX", target.ToString () + ", " + "EAX", null, null, null, new UInt32 [] { target }, new string [] { "o32", "A3", "ow/od" }));
		}

		/// <summary>
		/// MOV mem16,segreg
		/// </summary>
		public void MOV (WordMemory target, SegType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "8C", "/r" }));
		}

		/// <summary>
		/// MOV mem32,segreg
		/// </summary>
		public void MOV (DWordMemory target, SegType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o32", "8C", "/r" }));
		}

		/// <summary>
		/// MOV segreg,mem16
		/// </summary>
		public void MOV (SegType target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "8E", "/r" }));
		}

		/// <summary>
		/// MOV segreg,mem32
		/// </summary>
		public void MOV (SegType target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "8E", "/r" }));
		}

		/// <summary>
		/// MOV reg32,CR0/2/3/4
		/// </summary>
		public void MOV (R32Type target, CRType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "0F", "20", "/r" }));
		}

		/// <summary>
		/// MOV reg32,DR0/1/2/3/6/7
		/// </summary>
		public void MOV (R32Type target, DRType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "0F", "21", "/r" }));
		}

		/// <summary>
		/// MOV reg32,TR3/4/5/6/7
		/// </summary>
		public void MOV (R32Type target, TRType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "0F", "24", "/r" }));
		}

		/// <summary>
		/// MOV CR0/2/3/4,reg32
		/// </summary>
		public void MOV (CRType target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "0F", "22", "/r" }));
		}

		/// <summary>
		/// MOV DR0/1/2/3/6/7,reg32
		/// </summary>
		public void MOV (DRType target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "0F", "23", "/r" }));
		}

		/// <summary>
		/// MOV TR3/4/5/6/7,reg32
		/// </summary>
		public void MOV (TRType target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "0F", "26", "/r" }));
		}

		/// <summary>
		/// MOV rmreg8,reg8
		/// </summary>
		public void MOV (R8Type target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "88", "/r" }));
		}

		/// <summary>
		/// MOV rmreg16,reg16
		/// </summary>
		public void MOV (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o16", "89", "/r" }));
		}

		/// <summary>
		/// MOV rmreg32,reg32
		/// </summary>
		public void MOV (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o32", "89", "/r" }));
		}

		/// <summary>
		/// MOV rmreg16,segreg
		/// </summary>
		public void MOV (R16Type target, SegType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o16", "8C", "/r" }));
		}

		/// <summary>
		/// MOV rmreg32,segreg
		/// </summary>
		public void MOV (R32Type target, SegType source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o32", "8C", "/r" }));
		}

		/// <summary>
		/// MOV segreg,rmreg16
		/// </summary>
		public void MOV (SegType target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "8E", "/r" }));
		}

		/// <summary>
		/// MOV segreg,rmreg32
		/// </summary>
		public void MOV (SegType target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOV", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "8E", "/r" }));
		}

		/// <summary>
		/// MOVSB 
		/// </summary>
		public void MOVSB ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOVSB", "", null, null, null, null, new string [] { "A4" }));
		}

		/// <summary>
		/// MOVSD 
		/// </summary>
		public void MOVSD ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOVSD", "", null, null, null, null, new string [] { "o32", "A5" }));
		}

		/// <summary>
		/// MOVSW 
		/// </summary>
		public void MOVSW ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOVSW", "", null, null, null, null, new string [] { "o16", "A5" }));
		}

		/// <summary>
		/// MOVSX reg16,mem8
		/// </summary>
		public void MOVSX (R16Type target, ByteMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOVSX", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "BE", "/r" }));
		}

		/// <summary>
		/// MOVSX reg32,mem8
		/// </summary>
		public void MOVSX (R32Type target, ByteMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOVSX", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "BE", "/r" }));
		}

		/// <summary>
		/// MOVSX reg32,mem16
		/// </summary>
		public void MOVSX (R32Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOVSX", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "BF", "/r" }));
		}

		/// <summary>
		/// MOVSX reg16,rmreg8
		/// </summary>
		public void MOVSX (R16Type target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOVSX", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "BE", "/r" }));
		}

		/// <summary>
		/// MOVSX reg32,rmreg8
		/// </summary>
		public void MOVSX (R32Type target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOVSX", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "BE", "/r" }));
		}

		/// <summary>
		/// MOVSX reg32,rmreg16
		/// </summary>
		public void MOVSX (R32Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOVSX", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "BF", "/r" }));
		}

		/// <summary>
		/// MOVZX reg16,mem8
		/// </summary>
		public void MOVZX (R16Type target, ByteMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOVZX", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0F", "B6", "/r" }));
		}

		/// <summary>
		/// MOVZX reg32,mem8
		/// </summary>
		public void MOVZX (R32Type target, ByteMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOVZX", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "B6", "/r" }));
		}

		/// <summary>
		/// MOVZX reg32,mem16
		/// </summary>
		public void MOVZX (R32Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOVZX", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0F", "B7", "/r" }));
		}

		/// <summary>
		/// MOVZX reg16,rmreg8
		/// </summary>
		public void MOVZX (R16Type target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOVZX", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "0F", "B6", "/r" }));
		}

		/// <summary>
		/// MOVZX reg32,rmreg8
		/// </summary>
		public void MOVZX (R32Type target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOVZX", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "B6", "/r" }));
		}

		/// <summary>
		/// MOVZX reg32,rmreg16
		/// </summary>
		public void MOVZX (R32Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MOVZX", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "0F", "B7", "/r" }));
		}

		/// <summary>
		/// MUL mem8
		/// </summary>
		public void MUL (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MUL", target.ToString (), target, null, null, null, new string [] { "F6", "/4" }));
		}

		/// <summary>
		/// MUL mem16
		/// </summary>
		public void MUL (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MUL", target.ToString (), target, null, null, null, new string [] { "o16", "F7", "/4" }));
		}

		/// <summary>
		/// MUL mem32
		/// </summary>
		public void MUL (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MUL", target.ToString (), target, null, null, null, new string [] { "o32", "F7", "/4" }));
		}

		/// <summary>
		/// MUL rmreg8
		/// </summary>
		public void MUL (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MUL", target.ToString (), null, target, null, null, new string [] { "F6", "/4" }));
		}

		/// <summary>
		/// MUL rmreg16
		/// </summary>
		public void MUL (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MUL", target.ToString (), null, target, null, null, new string [] { "o16", "F7", "/4" }));
		}

		/// <summary>
		/// MUL rmreg32
		/// </summary>
		public void MUL (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "MUL", target.ToString (), null, target, null, null, new string [] { "o32", "F7", "/4" }));
		}

		/// <summary>
		/// NEG mem8
		/// </summary>
		public void NEG (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "NEG", target.ToString (), target, null, null, null, new string [] { "F6", "/3" }));
		}

		/// <summary>
		/// NEG mem16
		/// </summary>
		public void NEG (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "NEG", target.ToString (), target, null, null, null, new string [] { "o16", "F7", "/3" }));
		}

		/// <summary>
		/// NEG mem32
		/// </summary>
		public void NEG (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "NEG", target.ToString (), target, null, null, null, new string [] { "o32", "F7", "/3" }));
		}

		/// <summary>
		/// NEG rmreg8
		/// </summary>
		public void NEG (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "NEG", target.ToString (), null, target, null, null, new string [] { "F6", "/3" }));
		}

		/// <summary>
		/// NEG rmreg16
		/// </summary>
		public void NEG (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "NEG", target.ToString (), null, target, null, null, new string [] { "o16", "F7", "/3" }));
		}

		/// <summary>
		/// NEG rmreg32
		/// </summary>
		public void NEG (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "NEG", target.ToString (), null, target, null, null, new string [] { "o32", "F7", "/3" }));
		}

		/// <summary>
		/// NOP 
		/// </summary>
		public void NOP ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "NOP", "", null, null, null, null, new string [] { "90" }));
		}

		/// <summary>
		/// NOT mem8
		/// </summary>
		public void NOT (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "NOT", target.ToString (), target, null, null, null, new string [] { "F6", "/2" }));
		}

		/// <summary>
		/// NOT mem16
		/// </summary>
		public void NOT (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "NOT", target.ToString (), target, null, null, null, new string [] { "o16", "F7", "/2" }));
		}

		/// <summary>
		/// NOT mem32
		/// </summary>
		public void NOT (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "NOT", target.ToString (), target, null, null, null, new string [] { "o32", "F7", "/2" }));
		}

		/// <summary>
		/// NOT rmreg8
		/// </summary>
		public void NOT (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "NOT", target.ToString (), null, target, null, null, new string [] { "F6", "/2" }));
		}

		/// <summary>
		/// NOT rmreg16
		/// </summary>
		public void NOT (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "NOT", target.ToString (), null, target, null, null, new string [] { "o16", "F7", "/2" }));
		}

		/// <summary>
		/// NOT rmreg32
		/// </summary>
		public void NOT (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "NOT", target.ToString (), null, target, null, null, new string [] { "o32", "F7", "/2" }));
		}

		/// <summary>
		/// OR mem8,reg8
		/// </summary>
		public void OR (ByteMemory target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "08", "/r" }));
		}

		/// <summary>
		/// OR mem16,reg16
		/// </summary>
		public void OR (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o16", "09", "/r" }));
		}

		/// <summary>
		/// OR mem32,reg32
		/// </summary>
		public void OR (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o32", "09", "/r" }));
		}

		/// <summary>
		/// OR reg8,mem8
		/// </summary>
		public void OR (R8Type target, ByteMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "0A", "/r" }));
		}

		/// <summary>
		/// OR reg16,mem16
		/// </summary>
		public void OR (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "0B", "/r" }));
		}

		/// <summary>
		/// OR reg32,mem32
		/// </summary>
		public void OR (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "0B", "/r" }));
		}

		/// <summary>
		/// OR mem8,imm8
		/// </summary>
		public void OR (ByteMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "80", "/1", "ib" }));
		}

		/// <summary>
		/// OR mem16,imm16
		/// </summary>
		public void OR (WordMemory target, UInt16 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "81", "/1", "iw" }));
		}

		/// <summary>
		/// OR mem32,imm32
		/// </summary>
		public void OR (DWordMemory target, UInt32 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "81", "/1", "id" }));
		}

		/// <summary>
		/// OR mem16,imm8
		/// </summary>
		public void OR (WordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "83", "/1", "ib" }));
		}

		/// <summary>
		/// OR mem32,imm8
		/// </summary>
		public void OR (DWordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "83", "/1", "ib" }));
		}

		/// <summary>
		/// OR rmreg8,reg8
		/// </summary>
		public void OR (R8Type target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "08", "/r" }));
		}

		/// <summary>
		/// OR rmreg16,reg16
		/// </summary>
		public void OR (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o16", "09", "/r" }));
		}

		/// <summary>
		/// OR rmreg32,reg32
		/// </summary>
		public void OR (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o32", "09", "/r" }));
		}

		/// <summary>
		/// OR rmreg8,imm8
		/// </summary>
		public void OR (R8Type target, Byte source)
		{
			if (target == R8.AL)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR_AL", "AL" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "0C", "ib" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "80", "/1", "ib" }));
			}
		}

		/// <summary>
		/// OR rmreg16,imm16
		/// </summary>
		public void OR (R16Type target, UInt16 source)
		{
			if (target == R16.AX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR_AX", "AX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o16", "0D", "iw" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "81", "/1", "iw" }));
			}
		}

		/// <summary>
		/// OR rmreg32,imm32
		/// </summary>
		public void OR (R32Type target, UInt32 source)
		{
			if (target == R32.EAX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR_EAX", "EAX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o32", "0D", "id" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "81", "/1", "id" }));
			}
		}

		/// <summary>
		/// OR rmreg16,imm8
		/// </summary>
		public void OR (R16Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "83", "/1", "ib" }));
		}

		/// <summary>
		/// OR rmreg32,imm8
		/// </summary>
		public void OR (R32Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "83", "/1", "ib" }));
		}

		/// <summary>
		/// OUT imm8,AL
		/// </summary>
		public void OUT__AL (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OUT__AL", string.Format ("0x{0:x}", target) + ", " + "AL", null, null, null, new UInt32 [] { target }, new string [] { "E6", "ib" }));
		}

		/// <summary>
		/// OUT imm8,AX
		/// </summary>
		public void OUT__AX (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OUT__AX", string.Format ("0x{0:x}", target) + ", " + "AX", null, null, null, new UInt32 [] { target }, new string [] { "o16", "E7", "ib" }));
		}

		/// <summary>
		/// OUT imm8,EAX
		/// </summary>
		public void OUT__EAX (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OUT__EAX", string.Format ("0x{0:x}", target) + ", " + "EAX", null, null, null, new UInt32 [] { target }, new string [] { "o32", "E7", "ib" }));
		}

		/// <summary>
		/// OUT DX,AL
		/// </summary>
		public void OUT_DX__AL ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OUT_DX__AL", "DX" + ", " + "AL", null, null, null, null, new string [] { "EE" }));
		}

		/// <summary>
		/// OUT DX,AX
		/// </summary>
		public void OUT_DX__AX ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OUT_DX__AX", "DX" + ", " + "AX", null, null, null, null, new string [] { "o16", "EF" }));
		}

		/// <summary>
		/// OUT DX,EAX
		/// </summary>
		public void OUT_DX__EAX ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OUT_DX__EAX", "DX" + ", " + "EAX", null, null, null, null, new string [] { "o32", "EF" }));
		}

		/// <summary>
		/// OUTSB 
		/// </summary>
		public void OUTSB ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OUTSB", "", null, null, null, null, new string [] { "6E" }));
		}

		/// <summary>
		/// OUTSD 
		/// </summary>
		public void OUTSD ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OUTSD", "", null, null, null, null, new string [] { "o32", "6F" }));
		}

		/// <summary>
		/// OUTSW 
		/// </summary>
		public void OUTSW ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "OUTSW", "", null, null, null, null, new string [] { "o16", "6F" }));
		}

		/// <summary>
		/// PAUSE 
		/// </summary>
		public void PAUSE ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PAUSE", "", null, null, null, null, new string [] { "F3", "90" }));
		}

		/// <summary>
		/// POP reg16
		/// </summary>
		public void POP (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "POP", target.ToString (), null, null, target, null, new string [] { "o16", "58+r" }));
		}

		/// <summary>
		/// POP reg32
		/// </summary>
		public void POP (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "POP", target.ToString (), null, null, target, null, new string [] { "o32", "58+r" }));
		}

		/// <summary>
		/// POP mem16
		/// </summary>
		public void POP (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "POP", target.ToString (), target, null, null, null, new string [] { "o16", "8F", "/0" }));
		}

		/// <summary>
		/// POP mem32
		/// </summary>
		public void POP (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "POP", target.ToString (), target, null, null, null, new string [] { "o32", "8F", "/0" }));
		}

		/// <summary>
		/// POP segreg
		/// </summary>
		public void POP (SegType target)
		{
			if (target == Seg.GS)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "POP_GS", "GS", null, null, null, null, new string [] { "0F", "A9" }));
			else if (target == Seg.FS)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "POP_FS", "FS", null, null, null, null, new string [] { "0F", "A1" }));
			else if (target == Seg.ES)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "POP_ES", "ES", null, null, null, null, new string [] { "07" }));
			else if (target == Seg.DS)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "POP_DS", "DS", null, null, null, null, new string [] { "1F" }));
			else if (target == Seg.SS)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "POP_SS", "SS", null, null, null, null, new string [] { "17" }));
			else {
				throw new EngineException ("Parameters not supported.");
			}
		}

		/// <summary>
		/// POPA 
		/// </summary>
		public void POPA ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "POPA", "", null, null, null, null, new string [] { "61" }));
		}

		/// <summary>
		/// POPAD 
		/// </summary>
		public void POPAD ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "POPAD", "", null, null, null, null, new string [] { "o32", "61" }));
		}

		/// <summary>
		/// POPAW 
		/// </summary>
		public void POPAW ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "POPAW", "", null, null, null, null, new string [] { "o16", "61" }));
		}

		/// <summary>
		/// POPF 
		/// </summary>
		public void POPF ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "POPF", "", null, null, null, null, new string [] { "9D" }));
		}

		/// <summary>
		/// POPFD 
		/// </summary>
		public void POPFD ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "POPFD", "", null, null, null, null, new string [] { "o32", "9D" }));
		}

		/// <summary>
		/// POPFW 
		/// </summary>
		public void POPFW ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "POPFW", "", null, null, null, null, new string [] { "o16", "9D" }));
		}

		/// <summary>
		/// PREFETCHNTA m8
		/// </summary>
		public void PREFETCHNTA (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PREFETCHNTA", target.ToString (), target, null, null, null, new string [] { "0F", "18", "/0" }));
		}

		/// <summary>
		/// PREFETCHT0 m8
		/// </summary>
		public void PREFETCHT0 (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PREFETCHT0", target.ToString (), target, null, null, null, new string [] { "0F", "18", "/1" }));
		}

		/// <summary>
		/// PREFETCHT1 m8
		/// </summary>
		public void PREFETCHT1 (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PREFETCHT1", target.ToString (), target, null, null, null, new string [] { "0F", "18", "/2" }));
		}

		/// <summary>
		/// PREFETCHT2 m8
		/// </summary>
		public void PREFETCHT2 (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PREFETCHT2", target.ToString (), target, null, null, null, new string [] { "0F", "18", "/3" }));
		}

		/// <summary>
		/// PUSH reg16
		/// </summary>
		public void PUSH (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSH", target.ToString (), null, null, target, null, new string [] { "o16", "50+r" }));
		}

		/// <summary>
		/// PUSH reg32
		/// </summary>
		public void PUSH (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSH", target.ToString (), null, null, target, null, new string [] { "o32", "50+r" }));
		}

		/// <summary>
		/// PUSH mem16
		/// </summary>
		public void PUSH (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSH", target.ToString (), target, null, null, null, new string [] { "o16", "FF", "/6" }));
		}

		/// <summary>
		/// PUSH mem32
		/// </summary>
		public void PUSH (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSH", target.ToString (), target, null, null, null, new string [] { "o32", "FF", "/6" }));
		}

		/// <summary>
		/// PUSH imm8
		/// </summary>
		public void PUSH (Byte target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSH", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "6A", "ib" }));
		}

		/// <summary>
		/// PUSH imm16
		/// </summary>
		public void PUSH (UInt16 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSH", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "o16", "68", "iw" }));
		}

		/// <summary>
		/// PUSH imm32
		/// </summary>
		public void PUSH (UInt32 target)
		{
			if ((Int32) target >= -128 && (Int32) target <= 127) {
				this.PUSH ((byte) target);
			} else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSH", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "o32", "68", "id" }));
			}
		}

		/// <summary>
		/// PUSH segreg
		/// </summary>
		public void PUSH (SegType target)
		{
			if (target == Seg.CS)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSH_CS", "CS", null, null, null, null, new string [] { "0E" }));
			else if (target == Seg.GS)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSH_GS", "GS", null, null, null, null, new string [] { "0F", "A8" }));
			else if (target == Seg.ES)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSH_ES", "ES", null, null, null, null, new string [] { "06" }));
			else if (target == Seg.DS)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSH_DS", "DS", null, null, null, null, new string [] { "1E" }));
			else if (target == Seg.SS)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSH_SS", "SS", null, null, null, null, new string [] { "16" }));
			else if (target == Seg.FS)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSH_FS", "FS", null, null, null, null, new string [] { "0F", "A0" }));
			else {
				throw new EngineException ("Parameters not supported.");
			}
		}

		/// <summary>
		/// PUSHA 
		/// </summary>
		public void PUSHA ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSHA", "", null, null, null, null, new string [] { "60" }));
		}

		/// <summary>
		/// PUSHAD 
		/// </summary>
		public void PUSHAD ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSHAD", "", null, null, null, null, new string [] { "o32", "60" }));
		}

		/// <summary>
		/// PUSHAW 
		/// </summary>
		public void PUSHAW ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSHAW", "", null, null, null, null, new string [] { "o16", "60" }));
		}

		/// <summary>
		/// PUSHF 
		/// </summary>
		public void PUSHF ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSHF", "", null, null, null, null, new string [] { "9C" }));
		}

		/// <summary>
		/// PUSHFD 
		/// </summary>
		public void PUSHFD ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSHFD", "", null, null, null, null, new string [] { "o32", "9C" }));
		}

		/// <summary>
		/// PUSHFW 
		/// </summary>
		public void PUSHFW ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "PUSHFW", "", null, null, null, null, new string [] { "o16", "9C" }));
		}

		/// <summary>
		/// RCL mem8,CL
		/// </summary>
		public void RCL__CL (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCL__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "D2", "/2" }));
		}

		/// <summary>
		/// RCL mem8,imm8
		/// </summary>
		public void RCL (ByteMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCL__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "D0", "/2" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCL", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "C0", "/2", "ib" }));
			}
		}

		/// <summary>
		/// RCL mem16,CL
		/// </summary>
		public void RCL__CL (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCL__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "o16", "D3", "/2" }));
		}

		/// <summary>
		/// RCL mem16,imm8
		/// </summary>
		public void RCL (WordMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCL__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "o16", "D1", "/2" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCL", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "C1", "/2", "ib" }));
			}
		}

		/// <summary>
		/// RCL mem32,CL
		/// </summary>
		public void RCL__CL (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCL__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "o32", "D3", "/2" }));
		}

		/// <summary>
		/// RCL mem32,imm8
		/// </summary>
		public void RCL (DWordMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCL__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "o32", "D1", "/2" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCL", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "C1", "/2", "ib" }));
			}
		}

		/// <summary>
		/// RCL rmreg8,CL
		/// </summary>
		public void RCL__CL (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCL__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "D2", "/2" }));
		}

		/// <summary>
		/// RCL rmreg8,imm8
		/// </summary>
		public void RCL (R8Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCL__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "D0", "/2" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCL", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "C0", "/2", "ib" }));
			}
		}

		/// <summary>
		/// RCL rmreg16,CL
		/// </summary>
		public void RCL__CL (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCL__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "o16", "D3", "/2" }));
		}

		/// <summary>
		/// RCL rmreg16,imm8
		/// </summary>
		public void RCL (R16Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCL__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "o16", "D1", "/2" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCL", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "C1", "/2", "ib" }));
			}
		}

		/// <summary>
		/// RCL rmreg32,CL
		/// </summary>
		public void RCL__CL (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCL__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "o32", "D3", "/2" }));
		}

		/// <summary>
		/// RCL rmreg32,imm8
		/// </summary>
		public void RCL (R32Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCL__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "o32", "D1", "/2" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCL", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "C1", "/2", "ib" }));
			}
		}

		/// <summary>
		/// RCR mem8,CL
		/// </summary>
		public void RCR__CL (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCR__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "D2", "/3" }));
		}

		/// <summary>
		/// RCR mem8,imm8
		/// </summary>
		public void RCR (ByteMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCR__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "D0", "/3" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "C0", "/3", "ib" }));
			}
		}

		/// <summary>
		/// RCR mem16,CL
		/// </summary>
		public void RCR__CL (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCR__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "o16", "D3", "/3" }));
		}

		/// <summary>
		/// RCR mem16,imm8
		/// </summary>
		public void RCR (WordMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCR__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "o16", "D1", "/3" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "C1", "/3", "ib" }));
			}
		}

		/// <summary>
		/// RCR mem32,CL
		/// </summary>
		public void RCR__CL (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCR__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "o32", "D3", "/3" }));
		}

		/// <summary>
		/// RCR mem32,imm8
		/// </summary>
		public void RCR (DWordMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCR__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "o32", "D1", "/3" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "C1", "/3", "ib" }));
			}
		}

		/// <summary>
		/// RCR rmreg8,CL
		/// </summary>
		public void RCR__CL (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCR__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "D2", "/3" }));
		}

		/// <summary>
		/// RCR rmreg8,imm8
		/// </summary>
		public void RCR (R8Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCR__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "D0", "/3" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "C0", "/3", "ib" }));
			}
		}

		/// <summary>
		/// RCR rmreg16,CL
		/// </summary>
		public void RCR__CL (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCR__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "o16", "D3", "/3" }));
		}

		/// <summary>
		/// RCR rmreg16,imm8
		/// </summary>
		public void RCR (R16Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCR__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "o16", "D1", "/3" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "C1", "/3", "ib" }));
			}
		}

		/// <summary>
		/// RCR rmreg32,CL
		/// </summary>
		public void RCR__CL (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCR__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "o32", "D3", "/3" }));
		}

		/// <summary>
		/// RCR rmreg32,imm8
		/// </summary>
		public void RCR (R32Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCR__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "o32", "D1", "/3" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RCR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "C1", "/3", "ib" }));
			}
		}

		/// <summary>
		/// RDMSR 
		/// </summary>
		public void RDMSR ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RDMSR", "", null, null, null, null, new string [] { "0F", "32" }));
		}

		/// <summary>
		/// RDPMC 
		/// </summary>
		public void RDPMC ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RDPMC", "", null, null, null, null, new string [] { "0F", "33" }));
		}

		/// <summary>
		/// RDTSC 
		/// </summary>
		public void RDTSC ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RDTSC", "", null, null, null, null, new string [] { "0F", "31" }));
		}

		/// <summary>
		/// REP 
		/// </summary>
		public void REP ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "REP", "", null, null, null, null, new string [] { "F3" }));
		}

		/// <summary>
		/// REPE 
		/// </summary>
		public void REPE ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "REPE", "", null, null, null, null, new string [] { "F3" }));
		}

		/// <summary>
		/// REPNE 
		/// </summary>
		public void REPNE ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "REPNE", "", null, null, null, null, new string [] { "F2" }));
		}

		/// <summary>
		/// REPNZ 
		/// </summary>
		public void REPNZ ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "REPNZ", "", null, null, null, null, new string [] { "F2" }));
		}

		/// <summary>
		/// REPZ 
		/// </summary>
		public void REPZ ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "REPZ", "", null, null, null, null, new string [] { "F3" }));
		}

		/// <summary>
		/// RET 
		/// </summary>
		public void RET ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RET", "", null, null, null, null, new string [] { "C3" }));
		}

		/// <summary>
		/// RET imm16
		/// </summary>
		public void RET (UInt16 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RET", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "C2", "iw" }));
		}

		/// <summary>
		/// RETF 
		/// </summary>
		public void RETF ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RETF", "", null, null, null, null, new string [] { "CB" }));
		}

		/// <summary>
		/// RETF imm16
		/// </summary>
		public void RETF (UInt16 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RETF", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "CA", "iw" }));
		}

		/// <summary>
		/// RETN 
		/// </summary>
		public void RETN ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RETN", "", null, null, null, null, new string [] { "C3" }));
		}

		/// <summary>
		/// RETN imm16
		/// </summary>
		public void RETN (UInt16 target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RETN", string.Format ("0x{0:x}", target), null, null, null, new UInt32 [] { target }, new string [] { "C2", "iw" }));
		}

		/// <summary>
		/// ROL mem8,CL
		/// </summary>
		public void ROL__CL (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROL__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "D2", "/0" }));
		}

		/// <summary>
		/// ROL mem8,imm8
		/// </summary>
		public void ROL (ByteMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROL__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "D0", "/0" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROL", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "C0", "/0", "ib" }));
			}
		}

		/// <summary>
		/// ROL mem16,CL
		/// </summary>
		public void ROL__CL (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROL__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "o16", "D3", "/0" }));
		}

		/// <summary>
		/// ROL mem16,imm8
		/// </summary>
		public void ROL (WordMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROL__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "o16", "D1", "/0" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROL", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "C1", "/0", "ib" }));
			}
		}

		/// <summary>
		/// ROL mem32,CL
		/// </summary>
		public void ROL__CL (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROL__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "o32", "D3", "/0" }));
		}

		/// <summary>
		/// ROL mem32,imm8
		/// </summary>
		public void ROL (DWordMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROL__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "o32", "D1", "/0" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROL", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "C1", "/0", "ib" }));
			}
		}

		/// <summary>
		/// ROL rmreg8,CL
		/// </summary>
		public void ROL__CL (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROL__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "D2", "/0" }));
		}

		/// <summary>
		/// ROL rmreg8,imm8
		/// </summary>
		public void ROL (R8Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROL__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "D0", "/0" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROL", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "C0", "/0", "ib" }));
			}
		}

		/// <summary>
		/// ROL rmreg16,CL
		/// </summary>
		public void ROL__CL (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROL__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "o16", "D3", "/0" }));
		}

		/// <summary>
		/// ROL rmreg16,imm8
		/// </summary>
		public void ROL (R16Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROL__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "o16", "D1", "/0" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROL", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "C1", "/0", "ib" }));
			}
		}

		/// <summary>
		/// ROL rmreg32,CL
		/// </summary>
		public void ROL__CL (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROL__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "o32", "D3", "/0" }));
		}

		/// <summary>
		/// ROL rmreg32,imm8
		/// </summary>
		public void ROL (R32Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROL__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "o32", "D1", "/0" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROL", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "C1", "/0", "ib" }));
			}
		}

		/// <summary>
		/// ROR mem8,CL
		/// </summary>
		public void ROR__CL (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROR__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "D2", "/1" }));
		}

		/// <summary>
		/// ROR mem8,imm8
		/// </summary>
		public void ROR (ByteMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROR__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "D0", "/1" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "C0", "/1", "ib" }));
			}
		}

		/// <summary>
		/// ROR mem16,CL
		/// </summary>
		public void ROR__CL (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROR__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "o16", "D3", "/1" }));
		}

		/// <summary>
		/// ROR mem16,imm8
		/// </summary>
		public void ROR (WordMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROR__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "o16", "D1", "/1" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "C1", "/1", "ib" }));
			}
		}

		/// <summary>
		/// ROR mem32,CL
		/// </summary>
		public void ROR__CL (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROR__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "o32", "D3", "/1" }));
		}

		/// <summary>
		/// ROR mem32,imm8
		/// </summary>
		public void ROR (DWordMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROR__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "o32", "D1", "/1" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "C1", "/1", "ib" }));
			}
		}

		/// <summary>
		/// ROR rmreg8,CL
		/// </summary>
		public void ROR__CL (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROR__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "D2", "/1" }));
		}

		/// <summary>
		/// ROR rmreg8,imm8
		/// </summary>
		public void ROR (R8Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROR__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "D0", "/1" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "C0", "/1", "ib" }));
			}
		}

		/// <summary>
		/// ROR rmreg16,CL
		/// </summary>
		public void ROR__CL (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROR__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "o16", "D3", "/1" }));
		}

		/// <summary>
		/// ROR rmreg16,imm8
		/// </summary>
		public void ROR (R16Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROR__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "o16", "D1", "/1" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "C1", "/1", "ib" }));
			}
		}

		/// <summary>
		/// ROR rmreg32,CL
		/// </summary>
		public void ROR__CL (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROR__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "o32", "D3", "/1" }));
		}

		/// <summary>
		/// ROR rmreg32,imm8
		/// </summary>
		public void ROR (R32Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROR__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "o32", "D1", "/1" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "ROR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "C1", "/1", "ib" }));
			}
		}

		/// <summary>
		/// RSM 
		/// </summary>
		public void RSM ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "RSM", "", null, null, null, null, new string [] { "0F", "AA" }));
		}

		/// <summary>
		/// SAHF 
		/// </summary>
		public void SAHF ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAHF", "", null, null, null, null, new string [] { "9E" }));
		}

		/// <summary>
		/// SAL mem8,CL
		/// </summary>
		public void SAL__CL (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAL__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "D2", "/4" }));
		}

		/// <summary>
		/// SAL mem8,imm8
		/// </summary>
		public void SAL (ByteMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAL__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "D0", "/4" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAL", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "C0", "/4", "ib" }));
			}
		}

		/// <summary>
		/// SAL mem16,CL
		/// </summary>
		public void SAL__CL (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAL__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "o16", "D3", "/4" }));
		}

		/// <summary>
		/// SAL mem16,imm8
		/// </summary>
		public void SAL (WordMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAL__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "o16", "D1", "/4" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAL", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "C1", "/4", "ib" }));
			}
		}

		/// <summary>
		/// SAL mem32,CL
		/// </summary>
		public void SAL__CL (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAL__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "o32", "D3", "/4" }));
		}

		/// <summary>
		/// SAL mem32,imm8
		/// </summary>
		public void SAL (DWordMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAL__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "o32", "D1", "/4" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAL", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "C1", "/4", "ib" }));
			}
		}

		/// <summary>
		/// SAL rmreg8,CL
		/// </summary>
		public void SAL__CL (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAL__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "D2", "/4" }));
		}

		/// <summary>
		/// SAL rmreg8,imm8
		/// </summary>
		public void SAL (R8Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAL__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "D0", "/4" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAL", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "C0", "/4", "ib" }));
			}
		}

		/// <summary>
		/// SAL rmreg16,CL
		/// </summary>
		public void SAL__CL (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAL__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "o16", "D3", "/4" }));
		}

		/// <summary>
		/// SAL rmreg16,imm8
		/// </summary>
		public void SAL (R16Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAL__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "o16", "D1", "/4" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAL", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "C1", "/4", "ib" }));
			}
		}

		/// <summary>
		/// SAL rmreg32,CL
		/// </summary>
		public void SAL__CL (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAL__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "o32", "D3", "/4" }));
		}

		/// <summary>
		/// SAL rmreg32,imm8
		/// </summary>
		public void SAL (R32Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAL__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "o32", "D1", "/4" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAL", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "C1", "/4", "ib" }));
			}
		}

		/// <summary>
		/// SALC 
		/// </summary>
		public void SALC ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SALC", "", null, null, null, null, new string [] { "D6" }));
		}

		/// <summary>
		/// SAR mem8,CL
		/// </summary>
		public void SAR__CL (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAR__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "D2", "/7" }));
		}

		/// <summary>
		/// SAR mem8,imm8
		/// </summary>
		public void SAR (ByteMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAR__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "D0", "/7" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "C0", "/7", "ib" }));
			}
		}

		/// <summary>
		/// SAR mem16,CL
		/// </summary>
		public void SAR__CL (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAR__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "o16", "D3", "/7" }));
		}

		/// <summary>
		/// SAR mem16,imm8
		/// </summary>
		public void SAR (WordMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAR__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "o16", "D1", "/7" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "C1", "/7", "ib" }));
			}
		}

		/// <summary>
		/// SAR mem32,CL
		/// </summary>
		public void SAR__CL (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAR__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "o32", "D3", "/7" }));
		}

		/// <summary>
		/// SAR mem32,imm8
		/// </summary>
		public void SAR (DWordMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAR__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "o32", "D1", "/7" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "C1", "/7", "ib" }));
			}
		}

		/// <summary>
		/// SAR rmreg8,CL
		/// </summary>
		public void SAR__CL (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAR__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "D2", "/7" }));
		}

		/// <summary>
		/// SAR rmreg8,imm8
		/// </summary>
		public void SAR (R8Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAR__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "D0", "/7" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "C0", "/7", "ib" }));
			}
		}

		/// <summary>
		/// SAR rmreg16,CL
		/// </summary>
		public void SAR__CL (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAR__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "o16", "D3", "/7" }));
		}

		/// <summary>
		/// SAR rmreg16,imm8
		/// </summary>
		public void SAR (R16Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAR__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "o16", "D1", "/7" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "C1", "/7", "ib" }));
			}
		}

		/// <summary>
		/// SAR rmreg32,CL
		/// </summary>
		public void SAR__CL (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAR__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "o32", "D3", "/7" }));
		}

		/// <summary>
		/// SAR rmreg32,imm8
		/// </summary>
		public void SAR (R32Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAR__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "o32", "D1", "/7" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SAR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "C1", "/7", "ib" }));
			}
		}

		/// <summary>
		/// SBB mem8,reg8
		/// </summary>
		public void SBB (ByteMemory target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "18", "/r" }));
		}

		/// <summary>
		/// SBB mem16,reg16
		/// </summary>
		public void SBB (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o16", "19", "/r" }));
		}

		/// <summary>
		/// SBB mem32,reg32
		/// </summary>
		public void SBB (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o32", "19", "/r" }));
		}

		/// <summary>
		/// SBB reg8,mem8
		/// </summary>
		public void SBB (R8Type target, ByteMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "1A", "/r" }));
		}

		/// <summary>
		/// SBB reg16,mem16
		/// </summary>
		public void SBB (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "1B", "/r" }));
		}

		/// <summary>
		/// SBB reg32,mem32
		/// </summary>
		public void SBB (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "1B", "/r" }));
		}

		/// <summary>
		/// SBB mem8,imm8
		/// </summary>
		public void SBB (ByteMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "80", "/3", "ib" }));
		}

		/// <summary>
		/// SBB mem16,imm16
		/// </summary>
		public void SBB (WordMemory target, UInt16 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "81", "/3", "iw" }));
		}

		/// <summary>
		/// SBB mem32,imm32
		/// </summary>
		public void SBB (DWordMemory target, UInt32 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "81", "/3", "id" }));
		}

		/// <summary>
		/// SBB mem16,imm8
		/// </summary>
		public void SBB (WordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "83", "/3", "ib" }));
		}

		/// <summary>
		/// SBB mem32,imm8
		/// </summary>
		public void SBB (DWordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "83", "/3", "ib" }));
		}

		/// <summary>
		/// SBB rmreg8,reg8
		/// </summary>
		public void SBB (R8Type target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "18", "/r" }));
		}

		/// <summary>
		/// SBB rmreg16,reg16
		/// </summary>
		public void SBB (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o16", "19", "/r" }));
		}

		/// <summary>
		/// SBB rmreg32,reg32
		/// </summary>
		public void SBB (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o32", "19", "/r" }));
		}

		/// <summary>
		/// SBB rmreg8,imm8
		/// </summary>
		public void SBB (R8Type target, Byte source)
		{
			if (target == R8.AL)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB_AL", "AL" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "1C", "ib" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "80", "/3", "ib" }));
			}
		}

		/// <summary>
		/// SBB rmreg16,imm16
		/// </summary>
		public void SBB (R16Type target, UInt16 source)
		{
			if (target == R16.AX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB_AX", "AX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o16", "1D", "iw" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "81", "/3", "iw" }));
			}
		}

		/// <summary>
		/// SBB rmreg32,imm32
		/// </summary>
		public void SBB (R32Type target, UInt32 source)
		{
			if (target == R32.EAX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB_EAX", "EAX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o32", "1D", "id" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "81", "/3", "id" }));
			}
		}

		/// <summary>
		/// SBB rmreg16,imm8
		/// </summary>
		public void SBB (R16Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "83", "/3", "ib" }));
		}

		/// <summary>
		/// SBB rmreg32,imm8
		/// </summary>
		public void SBB (R32Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SBB", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "83", "/3", "ib" }));
		}

		/// <summary>
		/// SCASB 
		/// </summary>
		public void SCASB ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SCASB", "", null, null, null, null, new string [] { "AE" }));
		}

		/// <summary>
		/// SCASD 
		/// </summary>
		public void SCASD ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SCASD", "", null, null, null, null, new string [] { "o32", "AF" }));
		}

		/// <summary>
		/// SCASW 
		/// </summary>
		public void SCASW ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SCASW", "", null, null, null, null, new string [] { "o16", "AF" }));
		}

		/// <summary>
		/// SETA mem8
		/// </summary>
		public void SETA (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETA", target.ToString (), target, null, null, null, new string [] { "0F", "97", "/0" }));
		}

		/// <summary>
		/// SETA rmreg8
		/// </summary>
		public void SETA (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETA", target.ToString (), null, target, null, null, new string [] { "0F", "97", "/0" }));
		}

		/// <summary>
		/// SETAE mem8
		/// </summary>
		public void SETAE (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETAE", target.ToString (), target, null, null, null, new string [] { "0F", "93", "/0" }));
		}

		/// <summary>
		/// SETAE rmreg8
		/// </summary>
		public void SETAE (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETAE", target.ToString (), null, target, null, null, new string [] { "0F", "93", "/0" }));
		}

		/// <summary>
		/// SETB mem8
		/// </summary>
		public void SETB (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETB", target.ToString (), target, null, null, null, new string [] { "0F", "92", "/0" }));
		}

		/// <summary>
		/// SETB rmreg8
		/// </summary>
		public void SETB (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETB", target.ToString (), null, target, null, null, new string [] { "0F", "92", "/0" }));
		}

		/// <summary>
		/// SETBE mem8
		/// </summary>
		public void SETBE (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETBE", target.ToString (), target, null, null, null, new string [] { "0F", "96", "/0" }));
		}

		/// <summary>
		/// SETBE rmreg8
		/// </summary>
		public void SETBE (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETBE", target.ToString (), null, target, null, null, new string [] { "0F", "96", "/0" }));
		}

		/// <summary>
		/// SETC mem8
		/// </summary>
		public void SETC (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETC", target.ToString (), target, null, null, null, new string [] { "0F", "92", "/0" }));
		}

		/// <summary>
		/// SETC rmreg8
		/// </summary>
		public void SETC (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETC", target.ToString (), null, target, null, null, new string [] { "0F", "92", "/0" }));
		}

		/// <summary>
		/// SETE mem8
		/// </summary>
		public void SETE (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETE", target.ToString (), target, null, null, null, new string [] { "0F", "94", "/0" }));
		}

		/// <summary>
		/// SETE rmreg8
		/// </summary>
		public void SETE (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETE", target.ToString (), null, target, null, null, new string [] { "0F", "94", "/0" }));
		}

		/// <summary>
		/// SETG mem8
		/// </summary>
		public void SETG (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETG", target.ToString (), target, null, null, null, new string [] { "0F", "9F", "/0" }));
		}

		/// <summary>
		/// SETG rmreg8
		/// </summary>
		public void SETG (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETG", target.ToString (), null, target, null, null, new string [] { "0F", "9F", "/0" }));
		}

		/// <summary>
		/// SETGE mem8
		/// </summary>
		public void SETGE (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETGE", target.ToString (), target, null, null, null, new string [] { "0F", "9D", "/0" }));
		}

		/// <summary>
		/// SETGE rmreg8
		/// </summary>
		public void SETGE (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETGE", target.ToString (), null, target, null, null, new string [] { "0F", "9D", "/0" }));
		}

		/// <summary>
		/// SETL mem8
		/// </summary>
		public void SETL (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETL", target.ToString (), target, null, null, null, new string [] { "0F", "9C", "/0" }));
		}

		/// <summary>
		/// SETL rmreg8
		/// </summary>
		public void SETL (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETL", target.ToString (), null, target, null, null, new string [] { "0F", "9C", "/0" }));
		}

		/// <summary>
		/// SETLE mem8
		/// </summary>
		public void SETLE (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETLE", target.ToString (), target, null, null, null, new string [] { "0F", "9E", "/0" }));
		}

		/// <summary>
		/// SETLE rmreg8
		/// </summary>
		public void SETLE (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETLE", target.ToString (), null, target, null, null, new string [] { "0F", "9E", "/0" }));
		}

		/// <summary>
		/// SETNA mem8
		/// </summary>
		public void SETNA (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNA", target.ToString (), target, null, null, null, new string [] { "0F", "96", "/0" }));
		}

		/// <summary>
		/// SETNA rmreg8
		/// </summary>
		public void SETNA (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNA", target.ToString (), null, target, null, null, new string [] { "0F", "96", "/0" }));
		}

		/// <summary>
		/// SETNAE mem8
		/// </summary>
		public void SETNAE (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNAE", target.ToString (), target, null, null, null, new string [] { "0F", "92", "/0" }));
		}

		/// <summary>
		/// SETNAE rmreg8
		/// </summary>
		public void SETNAE (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNAE", target.ToString (), null, target, null, null, new string [] { "0F", "92", "/0" }));
		}

		/// <summary>
		/// SETNB mem8
		/// </summary>
		public void SETNB (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNB", target.ToString (), target, null, null, null, new string [] { "0F", "93", "/0" }));
		}

		/// <summary>
		/// SETNB rmreg8
		/// </summary>
		public void SETNB (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNB", target.ToString (), null, target, null, null, new string [] { "0F", "93", "/0" }));
		}

		/// <summary>
		/// SETNBE mem8
		/// </summary>
		public void SETNBE (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNBE", target.ToString (), target, null, null, null, new string [] { "0F", "97", "/0" }));
		}

		/// <summary>
		/// SETNBE rmreg8
		/// </summary>
		public void SETNBE (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNBE", target.ToString (), null, target, null, null, new string [] { "0F", "97", "/0" }));
		}

		/// <summary>
		/// SETNC mem8
		/// </summary>
		public void SETNC (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNC", target.ToString (), target, null, null, null, new string [] { "0F", "93", "/0" }));
		}

		/// <summary>
		/// SETNC rmreg8
		/// </summary>
		public void SETNC (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNC", target.ToString (), null, target, null, null, new string [] { "0F", "93", "/0" }));
		}

		/// <summary>
		/// SETNE mem8
		/// </summary>
		public void SETNE (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNE", target.ToString (), target, null, null, null, new string [] { "0F", "95", "/0" }));
		}

		/// <summary>
		/// SETNE rmreg8
		/// </summary>
		public void SETNE (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNE", target.ToString (), null, target, null, null, new string [] { "0F", "95", "/0" }));
		}

		/// <summary>
		/// SETNG mem8
		/// </summary>
		public void SETNG (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNG", target.ToString (), target, null, null, null, new string [] { "0F", "9E", "/0" }));
		}

		/// <summary>
		/// SETNG rmreg8
		/// </summary>
		public void SETNG (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNG", target.ToString (), null, target, null, null, new string [] { "0F", "9E", "/0" }));
		}

		/// <summary>
		/// SETNGE mem8
		/// </summary>
		public void SETNGE (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNGE", target.ToString (), target, null, null, null, new string [] { "0F", "9C", "/0" }));
		}

		/// <summary>
		/// SETNGE rmreg8
		/// </summary>
		public void SETNGE (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNGE", target.ToString (), null, target, null, null, new string [] { "0F", "9C", "/0" }));
		}

		/// <summary>
		/// SETNL mem8
		/// </summary>
		public void SETNL (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNL", target.ToString (), target, null, null, null, new string [] { "0F", "9D", "/0" }));
		}

		/// <summary>
		/// SETNL rmreg8
		/// </summary>
		public void SETNL (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNL", target.ToString (), null, target, null, null, new string [] { "0F", "9D", "/0" }));
		}

		/// <summary>
		/// SETNLE mem8
		/// </summary>
		public void SETNLE (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNLE", target.ToString (), target, null, null, null, new string [] { "0F", "9F", "/0" }));
		}

		/// <summary>
		/// SETNLE rmreg8
		/// </summary>
		public void SETNLE (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNLE", target.ToString (), null, target, null, null, new string [] { "0F", "9F", "/0" }));
		}

		/// <summary>
		/// SETNO mem8
		/// </summary>
		public void SETNO (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNO", target.ToString (), target, null, null, null, new string [] { "0F", "91", "/0" }));
		}

		/// <summary>
		/// SETNO rmreg8
		/// </summary>
		public void SETNO (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNO", target.ToString (), null, target, null, null, new string [] { "0F", "91", "/0" }));
		}

		/// <summary>
		/// SETNP mem8
		/// </summary>
		public void SETNP (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNP", target.ToString (), target, null, null, null, new string [] { "0F", "9B", "/0" }));
		}

		/// <summary>
		/// SETNP rmreg8
		/// </summary>
		public void SETNP (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNP", target.ToString (), null, target, null, null, new string [] { "0F", "9B", "/0" }));
		}

		/// <summary>
		/// SETNS mem8
		/// </summary>
		public void SETNS (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNS", target.ToString (), target, null, null, null, new string [] { "0F", "99", "/0" }));
		}

		/// <summary>
		/// SETNS rmreg8
		/// </summary>
		public void SETNS (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNS", target.ToString (), null, target, null, null, new string [] { "0F", "99", "/0" }));
		}

		/// <summary>
		/// SETNZ mem8
		/// </summary>
		public void SETNZ (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNZ", target.ToString (), target, null, null, null, new string [] { "0F", "95", "/0" }));
		}

		/// <summary>
		/// SETNZ rmreg8
		/// </summary>
		public void SETNZ (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETNZ", target.ToString (), null, target, null, null, new string [] { "0F", "95", "/0" }));
		}

		/// <summary>
		/// SETO mem8
		/// </summary>
		public void SETO (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETO", target.ToString (), target, null, null, null, new string [] { "0F", "90", "/0" }));
		}

		/// <summary>
		/// SETO rmreg8
		/// </summary>
		public void SETO (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETO", target.ToString (), null, target, null, null, new string [] { "0F", "90", "/0" }));
		}

		/// <summary>
		/// SETP mem8
		/// </summary>
		public void SETP (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETP", target.ToString (), target, null, null, null, new string [] { "0F", "9A", "/0" }));
		}

		/// <summary>
		/// SETP rmreg8
		/// </summary>
		public void SETP (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETP", target.ToString (), null, target, null, null, new string [] { "0F", "9A", "/0" }));
		}

		/// <summary>
		/// SETPE mem8
		/// </summary>
		public void SETPE (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETPE", target.ToString (), target, null, null, null, new string [] { "0F", "9A", "/0" }));
		}

		/// <summary>
		/// SETPE rmreg8
		/// </summary>
		public void SETPE (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETPE", target.ToString (), null, target, null, null, new string [] { "0F", "9A", "/0" }));
		}

		/// <summary>
		/// SETPO mem8
		/// </summary>
		public void SETPO (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETPO", target.ToString (), target, null, null, null, new string [] { "0F", "9B", "/0" }));
		}

		/// <summary>
		/// SETPO rmreg8
		/// </summary>
		public void SETPO (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETPO", target.ToString (), null, target, null, null, new string [] { "0F", "9B", "/0" }));
		}

		/// <summary>
		/// SETS mem8
		/// </summary>
		public void SETS (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETS", target.ToString (), target, null, null, null, new string [] { "0F", "98", "/0" }));
		}

		/// <summary>
		/// SETS rmreg8
		/// </summary>
		public void SETS (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETS", target.ToString (), null, target, null, null, new string [] { "0F", "98", "/0" }));
		}

		/// <summary>
		/// SETZ mem8
		/// </summary>
		public void SETZ (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETZ", target.ToString (), target, null, null, null, new string [] { "0F", "94", "/0" }));
		}

		/// <summary>
		/// SETZ rmreg8
		/// </summary>
		public void SETZ (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SETZ", target.ToString (), null, target, null, null, new string [] { "0F", "94", "/0" }));
		}

		/// <summary>
		/// SFENCE 
		/// </summary>
		public void SFENCE ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SFENCE", "", null, null, null, null, new string [] { "0F", "AE", "/7" }));
		}

		/// <summary>
		/// SGDT mem
		/// </summary>
		public void SGDT (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SGDT", target.ToString (), target, null, null, null, new string [] { "0F", "01", "/0" }));
		}

		/// <summary>
		/// SHL mem8,CL
		/// </summary>
		public void SHL__CL (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHL__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "D2", "/4" }));
		}

		/// <summary>
		/// SHL mem8,imm8
		/// </summary>
		public void SHL (ByteMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHL__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "D0", "/4" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHL", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "C0", "/4", "ib" }));
			}
		}

		/// <summary>
		/// SHL mem16,CL
		/// </summary>
		public void SHL__CL (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHL__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "o16", "D3", "/4" }));
		}

		/// <summary>
		/// SHL mem16,imm8
		/// </summary>
		public void SHL (WordMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHL__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "o16", "D1", "/4" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHL", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "C1", "/4", "ib" }));
			}
		}

		/// <summary>
		/// SHL mem32,CL
		/// </summary>
		public void SHL__CL (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHL__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "o32", "D3", "/4" }));
		}

		/// <summary>
		/// SHL mem32,imm8
		/// </summary>
		public void SHL (DWordMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHL__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "o32", "D1", "/4" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHL", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "C1", "/4", "ib" }));
			}
		}

		/// <summary>
		/// SHL rmreg8,CL
		/// </summary>
		public void SHL__CL (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHL__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "D2", "/4" }));
		}

		/// <summary>
		/// SHL rmreg8,imm8
		/// </summary>
		public void SHL (R8Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHL__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "D0", "/4" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHL", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "C0", "/4", "ib" }));
			}
		}

		/// <summary>
		/// SHL rmreg16,CL
		/// </summary>
		public void SHL__CL (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHL__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "o16", "D3", "/4" }));
		}

		/// <summary>
		/// SHL rmreg16,imm8
		/// </summary>
		public void SHL (R16Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHL__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "o16", "D1", "/4" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHL", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "C1", "/4", "ib" }));
			}
		}

		/// <summary>
		/// SHL rmreg32,CL
		/// </summary>
		public void SHL__CL (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHL__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "o32", "D3", "/4" }));
		}

		/// <summary>
		/// SHL rmreg32,imm8
		/// </summary>
		public void SHL (R32Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHL__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "o32", "D1", "/4" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHL", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "C1", "/4", "ib" }));
			}
		}

		/// <summary>
		/// SHLD mem16,reg16,imm8
		/// </summary>
		public void SHLD (WordMemory target, R16Type source, Byte value)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHLD", target.ToString () + ", " + source.ToString () + ", " + string.Format ("0x{0:x}", value), target, null, source, new UInt32 [] { value }, new string [] { "o16", "0F", "A4", "/r", "ib" }));
		}

		/// <summary>
		/// SHLD mem32,reg32,imm8
		/// </summary>
		public void SHLD (DWordMemory target, R32Type source, Byte value)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHLD", target.ToString () + ", " + source.ToString () + ", " + string.Format ("0x{0:x}", value), target, null, source, new UInt32 [] { value }, new string [] { "o32", "0F", "A4", "/r", "ib" }));
		}

		/// <summary>
		/// SHLD mem16,reg16,CL
		/// </summary>
		public void SHLD___CL (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHLD___CL", target.ToString () + ", " + source.ToString () + ", " + "CL", target, null, source, null, new string [] { "o16", "0F", "A5", "/r" }));
		}

		/// <summary>
		/// SHLD mem32,reg32,CL
		/// </summary>
		public void SHLD___CL (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHLD___CL", target.ToString () + ", " + source.ToString () + ", " + "CL", target, null, source, null, new string [] { "o32", "0F", "A5", "/r" }));
		}

		/// <summary>
		/// SHLD rmreg16,reg16,imm8
		/// </summary>
		public void SHLD (R16Type target, R16Type source, Byte value)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHLD", target.ToString () + ", " + source.ToString () + ", " + string.Format ("0x{0:x}", value), null, target, source, new UInt32 [] { value }, new string [] { "o16", "0F", "A4", "/r", "ib" }));
		}

		/// <summary>
		/// SHLD rmreg32,reg32,imm8
		/// </summary>
		public void SHLD (R32Type target, R32Type source, Byte value)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHLD", target.ToString () + ", " + source.ToString () + ", " + string.Format ("0x{0:x}", value), null, target, source, new UInt32 [] { value }, new string [] { "o32", "0F", "A4", "/r", "ib" }));
		}

		/// <summary>
		/// SHLD rmreg16,reg16,CL
		/// </summary>
		public void SHLD___CL (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHLD___CL", target.ToString () + ", " + source.ToString () + ", " + "CL", null, target, source, null, new string [] { "o16", "0F", "A5", "/r" }));
		}

		/// <summary>
		/// SHLD rmreg32,reg32,CL
		/// </summary>
		public void SHLD___CL (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHLD___CL", target.ToString () + ", " + source.ToString () + ", " + "CL", null, target, source, null, new string [] { "o32", "0F", "A5", "/r" }));
		}

		/// <summary>
		/// SHR mem8,CL
		/// </summary>
		public void SHR__CL (ByteMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHR__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "D2", "/5" }));
		}

		/// <summary>
		/// SHR mem8,imm8
		/// </summary>
		public void SHR (ByteMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHR__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "D0", "/5" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "C0", "/5", "ib" }));
			}
		}

		/// <summary>
		/// SHR mem16,CL
		/// </summary>
		public void SHR__CL (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHR__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "o16", "D3", "/5" }));
		}

		/// <summary>
		/// SHR mem16,imm8
		/// </summary>
		public void SHR (WordMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHR__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "o16", "D1", "/5" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "C1", "/5", "ib" }));
			}
		}

		/// <summary>
		/// SHR mem32,CL
		/// </summary>
		public void SHR__CL (DWordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHR__CL", target.ToString () + ", " + "CL", target, null, null, null, new string [] { "o32", "D3", "/5" }));
		}

		/// <summary>
		/// SHR mem32,imm8
		/// </summary>
		public void SHR (DWordMemory target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHR__1", target.ToString () + ", " + "1", target, null, null, null, new string [] { "o32", "D1", "/5" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "C1", "/5", "ib" }));
			}
		}

		/// <summary>
		/// SHR rmreg8,CL
		/// </summary>
		public void SHR__CL (R8Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHR__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "D2", "/5" }));
		}

		/// <summary>
		/// SHR rmreg8,imm8
		/// </summary>
		public void SHR (R8Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHR__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "D0", "/5" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "C0", "/5", "ib" }));
			}
		}

		/// <summary>
		/// SHR rmreg16,CL
		/// </summary>
		public void SHR__CL (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHR__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "o16", "D3", "/5" }));
		}

		/// <summary>
		/// SHR rmreg16,imm8
		/// </summary>
		public void SHR (R16Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHR__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "o16", "D1", "/5" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "C1", "/5", "ib" }));
			}
		}

		/// <summary>
		/// SHR rmreg32,CL
		/// </summary>
		public void SHR__CL (R32Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHR__CL", target.ToString () + ", " + "CL", null, target, null, null, new string [] { "o32", "D3", "/5" }));
		}

		/// <summary>
		/// SHR rmreg32,imm8
		/// </summary>
		public void SHR (R32Type target, Byte source)
		{
			if (source == 1)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHR__1", target.ToString () + ", " + "1", null, target, null, null, new string [] { "o32", "D1", "/5" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "C1", "/5", "ib" }));
			}
		}

		/// <summary>
		/// SHRD mem16,reg16,imm8
		/// </summary>
		public void SHRD (WordMemory target, R16Type source, Byte value)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHRD", target.ToString () + ", " + source.ToString () + ", " + string.Format ("0x{0:x}", value), target, null, source, new UInt32 [] { value }, new string [] { "o16", "0F", "AC", "/r", "ib" }));
		}

		/// <summary>
		/// SHRD mem32,reg32,imm8
		/// </summary>
		public void SHRD (DWordMemory target, R32Type source, Byte value)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHRD", target.ToString () + ", " + source.ToString () + ", " + string.Format ("0x{0:x}", value), target, null, source, new UInt32 [] { value }, new string [] { "o32", "0F", "AC", "/r", "ib" }));
		}

		/// <summary>
		/// SHRD mem16,reg16,CL
		/// </summary>
		public void SHRD___CL (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHRD___CL", target.ToString () + ", " + source.ToString () + ", " + "CL", target, null, source, null, new string [] { "o16", "0F", "AD", "/r" }));
		}

		/// <summary>
		/// SHRD mem32,reg32,CL
		/// </summary>
		public void SHRD___CL (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHRD___CL", target.ToString () + ", " + source.ToString () + ", " + "CL", target, null, source, null, new string [] { "o32", "0F", "AD", "/r" }));
		}

		/// <summary>
		/// SHRD rmreg16,reg16,imm8
		/// </summary>
		public void SHRD (R16Type target, R16Type source, Byte value)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHRD", target.ToString () + ", " + source.ToString () + ", " + string.Format ("0x{0:x}", value), null, target, source, new UInt32 [] { value }, new string [] { "o16", "0F", "AC", "/r", "ib" }));
		}

		/// <summary>
		/// SHRD rmreg32,reg32,imm8
		/// </summary>
		public void SHRD (R32Type target, R32Type source, Byte value)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHRD", target.ToString () + ", " + source.ToString () + ", " + string.Format ("0x{0:x}", value), null, target, source, new UInt32 [] { value }, new string [] { "o32", "0F", "AC", "/r", "ib" }));
		}

		/// <summary>
		/// SHRD rmreg16,reg16,CL
		/// </summary>
		public void SHRD___CL (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHRD___CL", target.ToString () + ", " + source.ToString () + ", " + "CL", null, target, source, null, new string [] { "o16", "0F", "AD", "/r" }));
		}

		/// <summary>
		/// SHRD rmreg32,reg32,CL
		/// </summary>
		public void SHRD___CL (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SHRD___CL", target.ToString () + ", " + source.ToString () + ", " + "CL", null, target, source, null, new string [] { "o32", "0F", "AD", "/r" }));
		}

		/// <summary>
		/// SIDT mem
		/// </summary>
		public void SIDT (Memory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SIDT", target.ToString (), target, null, null, null, new string [] { "0F", "01", "/1" }));
		}

		/// <summary>
		/// SLDT mem16
		/// </summary>
		public void SLDT (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SLDT", target.ToString (), target, null, null, null, new string [] { "0F", "00", "/0" }));
		}

		/// <summary>
		/// SLDT rmreg16
		/// </summary>
		public void SLDT (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SLDT", target.ToString (), null, target, null, null, new string [] { "o16", "0F", "00", "/0" }));
		}

		/// <summary>
		/// SMSW mem16
		/// </summary>
		public void SMSW (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SMSW", target.ToString (), target, null, null, null, new string [] { "0F", "01", "/4" }));
		}

		/// <summary>
		/// SMSW rmreg16
		/// </summary>
		public void SMSW (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SMSW", target.ToString (), null, target, null, null, new string [] { "o16", "0F", "01", "/4" }));
		}

		/// <summary>
		/// STC 
		/// </summary>
		public void STC ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "STC", "", null, null, null, null, new string [] { "F9" }));
		}

		/// <summary>
		/// STD 
		/// </summary>
		public void STD ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "STD", "", null, null, null, null, new string [] { "FD" }));
		}

		/// <summary>
		/// STI 
		/// </summary>
		public void STI ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "STI", "", null, null, null, null, new string [] { "FB" }));
		}

		/// <summary>
		/// STOSB 
		/// </summary>
		public void STOSB ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "STOSB", "", null, null, null, null, new string [] { "AA" }));
		}

		/// <summary>
		/// STOSD 
		/// </summary>
		public void STOSD ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "STOSD", "", null, null, null, null, new string [] { "o32", "AB" }));
		}

		/// <summary>
		/// STOSW 
		/// </summary>
		public void STOSW ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "STOSW", "", null, null, null, null, new string [] { "o16", "AB" }));
		}

		/// <summary>
		/// STR mem16
		/// </summary>
		public void STR (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "STR", target.ToString (), target, null, null, null, new string [] { "0F", "00", "/1" }));
		}

		/// <summary>
		/// STR rmreg16
		/// </summary>
		public void STR (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "STR", target.ToString (), null, target, null, null, new string [] { "o16", "0F", "00", "/1" }));
		}

		/// <summary>
		/// SUB mem8,reg8
		/// </summary>
		public void SUB (ByteMemory target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "28", "/r" }));
		}

		/// <summary>
		/// SUB mem16,reg16
		/// </summary>
		public void SUB (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o16", "29", "/r" }));
		}

		/// <summary>
		/// SUB mem32,reg32
		/// </summary>
		public void SUB (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o32", "29", "/r" }));
		}

		/// <summary>
		/// SUB reg8,mem8
		/// </summary>
		public void SUB (R8Type target, ByteMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "2A", "/r" }));
		}

		/// <summary>
		/// SUB reg16,mem16
		/// </summary>
		public void SUB (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "2B", "/r" }));
		}

		/// <summary>
		/// SUB reg32,mem32
		/// </summary>
		public void SUB (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "2B", "/r" }));
		}

		/// <summary>
		/// SUB mem8,imm8
		/// </summary>
		public void SUB (ByteMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "80", "/5", "ib" }));
		}

		/// <summary>
		/// SUB mem16,imm16
		/// </summary>
		public void SUB (WordMemory target, UInt16 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "81", "/5", "iw" }));
		}

		/// <summary>
		/// SUB mem32,imm32
		/// </summary>
		public void SUB (DWordMemory target, UInt32 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "81", "/5", "id" }));
		}

		/// <summary>
		/// SUB mem16,imm8
		/// </summary>
		public void SUB (WordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "83", "/5", "ib" }));
		}

		/// <summary>
		/// SUB mem32,imm8
		/// </summary>
		public void SUB (DWordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "83", "/5", "ib" }));
		}

		/// <summary>
		/// SUB rmreg8,reg8
		/// </summary>
		public void SUB (R8Type target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "28", "/r" }));
		}

		/// <summary>
		/// SUB rmreg16,reg16
		/// </summary>
		public void SUB (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o16", "29", "/r" }));
		}

		/// <summary>
		/// SUB rmreg32,reg32
		/// </summary>
		public void SUB (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o32", "29", "/r" }));
		}

		/// <summary>
		/// SUB rmreg8,imm8
		/// </summary>
		public void SUB (R8Type target, Byte source)
		{
			if (target == R8.AL)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB_AL", "AL" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "2C", "ib" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "80", "/5", "ib" }));
			}
		}

		/// <summary>
		/// SUB rmreg16,imm16
		/// </summary>
		public void SUB (R16Type target, UInt16 source)
		{
			if (target == R16.AX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB_AX", "AX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o16", "2D", "iw" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "81", "/5", "iw" }));
			}
		}

		/// <summary>
		/// SUB rmreg32,imm32
		/// </summary>
		public void SUB (R32Type target, UInt32 source)
		{
			if (target == R32.EAX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB_EAX", "EAX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o32", "2D", "id" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "81", "/5", "id" }));
			}
		}

		/// <summary>
		/// SUB rmreg16,imm8
		/// </summary>
		public void SUB (R16Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "83", "/5", "ib" }));
		}

		/// <summary>
		/// SUB rmreg32,imm8
		/// </summary>
		public void SUB (R32Type target, Byte source)
		{
			if (source == 0) {
			} else if (source == 1) {
				this.DEC (target);
			} else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SUB", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "83", "/5", "ib" }));
			}
		}

		/// <summary>
		/// SYSCALL 
		/// </summary>
		public void SYSCALL ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SYSCALL", "", null, null, null, null, new string [] { "0F", "05" }));
		}

		/// <summary>
		/// SYSENTER 
		/// </summary>
		public void SYSENTER ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SYSENTER", "", null, null, null, null, new string [] { "0F", "34" }));
		}

		/// <summary>
		/// SYSEXIT 
		/// </summary>
		public void SYSEXIT ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SYSEXIT", "", null, null, null, null, new string [] { "0F", "35" }));
		}

		/// <summary>
		/// SYSRET 
		/// </summary>
		public void SYSRET ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "SYSRET", "", null, null, null, null, new string [] { "0F", "07" }));
		}

		/// <summary>
		/// TEST mem8,reg8
		/// </summary>
		public void TEST (ByteMemory target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "TEST", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "84", "/r" }));
		}

		/// <summary>
		/// TEST mem16,reg16
		/// </summary>
		public void TEST (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "TEST", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o16", "85", "/r" }));
		}

		/// <summary>
		/// TEST mem32,reg32
		/// </summary>
		public void TEST (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "TEST", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o32", "85", "/r" }));
		}

		/// <summary>
		/// TEST mem8,imm8
		/// </summary>
		public void TEST (ByteMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "TEST", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "F6", "/0", "ib" }));
		}

		/// <summary>
		/// TEST mem16,imm16
		/// </summary>
		public void TEST (WordMemory target, UInt16 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "TEST", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "F7", "/0", "iw" }));
		}

		/// <summary>
		/// TEST mem32,imm32
		/// </summary>
		public void TEST (DWordMemory target, UInt32 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "TEST", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "F7", "/0", "id" }));
		}

		/// <summary>
		/// TEST rmreg8,reg8
		/// </summary>
		public void TEST (R8Type target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "TEST", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "84", "/r" }));
		}

		/// <summary>
		/// TEST rmreg16,reg16
		/// </summary>
		public void TEST (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "TEST", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o16", "85", "/r" }));
		}

		/// <summary>
		/// TEST rmreg32,reg32
		/// </summary>
		public void TEST (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "TEST", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o32", "85", "/r" }));
		}

		/// <summary>
		/// TEST rmreg8,imm8
		/// </summary>
		public void TEST (R8Type target, Byte source)
		{
			if (target == R8.AL)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "TEST_AL", "AL" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "A8", "ib" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "TEST", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "F6", "/0", "ib" }));
			}
		}

		/// <summary>
		/// TEST rmreg16,imm16
		/// </summary>
		public void TEST (R16Type target, UInt16 source)
		{
			if (target == R16.AX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "TEST_AX", "AX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o16", "A9", "iw" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "TEST", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "F7", "/0", "iw" }));
			}
		}

		/// <summary>
		/// TEST rmreg32,imm32
		/// </summary>
		public void TEST (R32Type target, UInt32 source)
		{
			if (target == R32.EAX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "TEST_EAX", "EAX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o32", "A9", "id" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "TEST", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "F7", "/0", "id" }));
			}
		}

		/// <summary>
		/// VERR mem16
		/// </summary>
		public void VERR (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "VERR", target.ToString (), target, null, null, null, new string [] { "0F", "00", "/4" }));
		}

		/// <summary>
		/// VERR rmreg16
		/// </summary>
		public void VERR (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "VERR", target.ToString (), null, target, null, null, new string [] { "0F", "00", "/4" }));
		}

		/// <summary>
		/// VERW mem16
		/// </summary>
		public void VERW (WordMemory target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "VERW", target.ToString (), target, null, null, null, new string [] { "0F", "00", "/5" }));
		}

		/// <summary>
		/// VERW rmreg16
		/// </summary>
		public void VERW (R16Type target)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "VERW", target.ToString (), null, target, null, null, new string [] { "0F", "00", "/5" }));
		}

		/// <summary>
		/// WAIT 
		/// </summary>
		public void WAIT ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "WAIT", "", null, null, null, null, new string [] { "9B" }));
		}

		/// <summary>
		/// WBINVD 
		/// </summary>
		public void WBINVD ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "WBINVD", "", null, null, null, null, new string [] { "0F", "09" }));
		}

		/// <summary>
		/// WRMSR 
		/// </summary>
		public void WRMSR ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "WRMSR", "", null, null, null, null, new string [] { "0F", "30" }));
		}

		/// <summary>
		/// XADD mem8,reg8
		/// </summary>
		public void XADD (ByteMemory target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XADD", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "0F", "C0", "/r" }));
		}

		/// <summary>
		/// XADD mem16,reg16
		/// </summary>
		public void XADD (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XADD", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o16", "0F", "C1", "/r" }));
		}

		/// <summary>
		/// XADD mem32,reg32
		/// </summary>
		public void XADD (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XADD", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o32", "0F", "C1", "/r" }));
		}

		/// <summary>
		/// XADD rmreg8,reg8
		/// </summary>
		public void XADD (R8Type target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XADD", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "0F", "C0", "/r" }));
		}

		/// <summary>
		/// XADD rmreg16,reg16
		/// </summary>
		public void XADD (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XADD", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o16", "0F", "C1", "/r" }));
		}

		/// <summary>
		/// XADD rmreg32,reg32
		/// </summary>
		public void XADD (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XADD", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o32", "0F", "C1", "/r" }));
		}

		/// <summary>
		/// XCHG reg8,mem8
		/// </summary>
		public void XCHG (R8Type target, ByteMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XCHG", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "86", "/r" }));
		}

		/// <summary>
		/// XCHG reg16,mem16
		/// </summary>
		public void XCHG (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XCHG", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "87", "/r" }));
		}

		/// <summary>
		/// XCHG reg32,mem32
		/// </summary>
		public void XCHG (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XCHG", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "87", "/r" }));
		}

		/// <summary>
		/// XCHG mem8,reg8
		/// </summary>
		public void XCHG (ByteMemory target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XCHG", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "86", "/r" }));
		}

		/// <summary>
		/// XCHG mem16,reg16
		/// </summary>
		public void XCHG (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XCHG", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o16", "87", "/r" }));
		}

		/// <summary>
		/// XCHG mem32,reg32
		/// </summary>
		public void XCHG (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XCHG", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o32", "87", "/r" }));
		}

		/// <summary>
		/// XCHG reg8,rmreg8
		/// </summary>
		public void XCHG (R8Type target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XCHG", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "86", "/r" }));
		}

		/// <summary>
		/// XCHG reg16,rmreg16
		/// </summary>
		public void XCHG (R16Type target, R16Type source)
		{
			if (target == R16.AX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XCHG_AX", "AX" + ", " + source.ToString (), null, null, source, null, new string [] { "o16", "90+r" }));
			else if (source == R16.AX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XCHG__AX", target.ToString () + ", " + "AX", null, null, target, null, new string [] { "o16", "90+r" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XCHG", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o16", "87", "/r" }));
			}
		}

		/// <summary>
		/// XCHG reg32,rmreg32
		/// </summary>
		public void XCHG (R32Type target, R32Type source)
		{
			if (target == R32.EAX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XCHG_EAX", "EAX" + ", " + source.ToString (), null, null, source, null, new string [] { "o32", "90+r" }));
			else if (source == R32.EAX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XCHG__EAX", target.ToString () + ", " + "EAX", null, null, target, null, new string [] { "o32", "90+r" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XCHG", target.ToString () + ", " + source.ToString (), null, source, target, null, new string [] { "o32", "87", "/r" }));
			}
		}

		/// <summary>
		/// XLAT 
		/// </summary>
		public void XLAT ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XLAT", "", null, null, null, null, new string [] { "D7" }));
		}

		/// <summary>
		/// XLATB 
		/// </summary>
		public void XLATB ()
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XLATB", "", null, null, null, null, new string [] { "D7" }));
		}

		/// <summary>
		/// XOR mem8,reg8
		/// </summary>
		public void XOR (ByteMemory target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "30", "/r" }));
		}

		/// <summary>
		/// XOR mem16,reg16
		/// </summary>
		public void XOR (WordMemory target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o16", "31", "/r" }));
		}

		/// <summary>
		/// XOR mem32,reg32
		/// </summary>
		public void XOR (DWordMemory target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + source.ToString (), target, null, source, null, new string [] { "o32", "31", "/r" }));
		}

		/// <summary>
		/// XOR reg8,mem8
		/// </summary>
		public void XOR (R8Type target, ByteMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "32", "/r" }));
		}

		/// <summary>
		/// XOR reg16,mem16
		/// </summary>
		public void XOR (R16Type target, WordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o16", "33", "/r" }));
		}

		/// <summary>
		/// XOR reg32,mem32
		/// </summary>
		public void XOR (R32Type target, DWordMemory source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + source.ToString (), source, null, target, null, new string [] { "o32", "33", "/r" }));
		}

		/// <summary>
		/// XOR mem8,imm8
		/// </summary>
		public void XOR (ByteMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "80", "/6", "ib" }));
		}

		/// <summary>
		/// XOR mem16,imm16
		/// </summary>
		public void XOR (WordMemory target, UInt16 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "81", "/6", "iw" }));
		}

		/// <summary>
		/// XOR mem32,imm32
		/// </summary>
		public void XOR (DWordMemory target, UInt32 source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "81", "/6", "id" }));
		}

		/// <summary>
		/// XOR mem16,imm8
		/// </summary>
		public void XOR (WordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o16", "83", "/6", "ib" }));
		}

		/// <summary>
		/// XOR mem32,imm8
		/// </summary>
		public void XOR (DWordMemory target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + string.Format ("0x{0:x}", source), target, null, null, new UInt32 [] { source }, new string [] { "o32", "83", "/6", "ib" }));
		}

		/// <summary>
		/// XOR rmreg8,reg8
		/// </summary>
		public void XOR (R8Type target, R8Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "30", "/r" }));
		}

		/// <summary>
		/// XOR rmreg16,reg16
		/// </summary>
		public void XOR (R16Type target, R16Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o16", "31", "/r" }));
		}

		/// <summary>
		/// XOR rmreg32,reg32
		/// </summary>
		public void XOR (R32Type target, R32Type source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + source.ToString (), null, target, source, null, new string [] { "o32", "31", "/r" }));
		}

		/// <summary>
		/// XOR rmreg8,imm8
		/// </summary>
		public void XOR (R8Type target, Byte source)
		{
			if (target == R8.AL)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR_AL", "AL" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "34", "ib" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "80", "/6", "ib" }));
			}
		}

		/// <summary>
		/// XOR rmreg16,imm16
		/// </summary>
		public void XOR (R16Type target, UInt16 source)
		{
			if (target == R16.AX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR_AX", "AX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o16", "35", "iw" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "81", "/6", "iw" }));
			}
		}

		/// <summary>
		/// XOR rmreg32,imm32
		/// </summary>
		public void XOR (R32Type target, UInt32 source)
		{
			if (target == R32.EAX)
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR_EAX", "EAX" + ", " + string.Format ("0x{0:x}", source), null, null, null, new UInt32 [] { source }, new string [] { "o32", "35", "id" }));
			else {
				this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "81", "/6", "id" }));
			}
		}

		/// <summary>
		/// XOR rmreg16,imm8
		/// </summary>
		public void XOR (R16Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o16", "83", "/6", "ib" }));
		}

		/// <summary>
		/// XOR rmreg32,imm8
		/// </summary>
		public void XOR (R32Type target, Byte source)
		{
			this.instructions.Add (new Instruction (true, string.Empty, string.Empty, "XOR", target.ToString () + ", " + string.Format ("0x{0:x}", source), null, target, null, new UInt32 [] { source }, new string [] { "o32", "83", "/6", "ib" }));
		}
	}

}
