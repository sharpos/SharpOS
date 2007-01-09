/**
 *  (C) 2006-2007 Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
 * 
 */

using System;

namespace SharpOS.AOT.X86
{
	public partial class Assembly
	{
		
		// AAA 
		public void AAA ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AAA", "", null, null, null, null, new string[] {"37"}));
		}
		
		// AAD 
		public void AAD ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AAD", "", null, null, null, null, new string[] {"D5", "0A"}));
		}
		
		// AAD imm8
		public void AAD (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AAD", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"D5", "ib"}));
		}
		
		// AAM 
		public void AAM ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AAM", "", null, null, null, null, new string[] {"D4", "0A"}));
		}
		
		// AAM imm8
		public void AAM (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AAM", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"D4", "ib"}));
		}
		
		// AAS 
		public void AAS ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AAS", "", null, null, null, null, new string[] {"3F"}));
		}
		
		// ADC mem8,reg8
		public void ADC (ByteMemory target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"10", "/r"}));
		}
		
		// ADC mem16,reg16
		public void ADC (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o16", "11", "/r"}));
		}
		
		// ADC mem32,reg32
		public void ADC (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o32", "11", "/r"}));
		}
		
		// ADC reg8,mem8
		public void ADC (R8Type target, ByteMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"12", "/r"}));
		}
		
		// ADC reg16,mem16
		public void ADC (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "13", "/r"}));
		}
		
		// ADC reg32,mem32
		public void ADC (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "13", "/r"}));
		}
		
		// ADC mem8,imm8
		public void ADC (ByteMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"80", "/2", "ib"}));
		}
		
		// ADC mem16,imm16
		public void ADC (WordMemory target, UInt16 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "81", "/2", "iw"}));
		}
		
		// ADC mem32,imm32
		public void ADC (DWordMemory target, UInt32 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "81", "/2", "id"}));
		}
		
		// ADC mem16,imm8
		public void ADC (WordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "83", "/2", "ib"}));
		}
		
		// ADC mem32,imm8
		public void ADC (DWordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "83", "/2", "ib"}));
		}
		
		// ADC rmreg8,reg8
		public void ADC (R8Type target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"10", "/r"}));
		}
		
		// ADC rmreg16,reg16
		public void ADC (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o16", "11", "/r"}));
		}
		
		// ADC rmreg32,reg32
		public void ADC (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o32", "11", "/r"}));
		}
		
		// ADC rmreg8,imm8
		public void ADC (R8Type target, Byte source)
		{
			if (target == R8.AL)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC_AL", "AL" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"14", "ib"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"80", "/2", "ib"}));
			}
		}
		
		// ADC rmreg16,imm16
		public void ADC (R16Type target, UInt16 source)
		{
			if (target == R16.AX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC_AX", "AX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o16", "15", "iw"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "81", "/2", "iw"}));
			}
		}
		
		// ADC rmreg32,imm32
		public void ADC (R32Type target, UInt32 source)
		{
			if (target == R32.EAX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC_EAX", "EAX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o32", "15", "id"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "81", "/2", "id"}));
			}
		}
		
		// ADC rmreg16,imm8
		public void ADC (R16Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "83", "/2", "ib"}));
		}
		
		// ADC rmreg32,imm8
		public void ADC (R32Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADC", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "83", "/2", "ib"}));
		}
		
		// ADD mem8,reg8
		public void ADD (ByteMemory target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"00", "/r"}));
		}
		
		// ADD mem16,reg16
		public void ADD (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o16", "01", "/r"}));
		}
		
		// ADD mem32,reg32
		public void ADD (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o32", "01", "/r"}));
		}
		
		// ADD reg8,mem8
		public void ADD (R8Type target, ByteMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"02", "/r"}));
		}
		
		// ADD reg16,mem16
		public void ADD (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "03", "/r"}));
		}
		
		// ADD reg32,mem32
		public void ADD (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "03", "/r"}));
		}
		
		// ADD mem8,imm8
		public void ADD (ByteMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"80", "/0", "ib"}));
		}
		
		// ADD mem16,imm16
		public void ADD (WordMemory target, UInt16 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "81", "/0", "iw"}));
		}
		
		// ADD mem32,imm32
		public void ADD (DWordMemory target, UInt32 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "81", "/0", "id"}));
		}
		
		// ADD mem16,imm8
		public void ADD (WordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "83", "/0", "ib"}));
		}
		
		// ADD mem32,imm8
		public void ADD (DWordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "83", "/0", "ib"}));
		}
		
		// ADD rmreg8,reg8
		public void ADD (R8Type target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"00", "/r"}));
		}
		
		// ADD rmreg16,reg16
		public void ADD (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o16", "01", "/r"}));
		}
		
		// ADD rmreg32,reg32
		public void ADD (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o32", "01", "/r"}));
		}
		
		// ADD rmreg8,imm8
		public void ADD (R8Type target, Byte source)
		{
			if (target == R8.AL)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD_AL", "AL" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"04", "ib"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"80", "/0", "ib"}));
			}
		}
		
		// ADD rmreg16,imm16
		public void ADD (R16Type target, UInt16 source)
		{
			if (target == R16.AX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD_AX", "AX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o16", "05", "iw"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "81", "/0", "iw"}));
			}
		}
		
		// ADD rmreg32,imm32
		public void ADD (R32Type target, UInt32 source)
		{
			if (target == R32.EAX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD_EAX", "EAX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o32", "05", "id"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "81", "/0", "id"}));
			}
		}
		
		// ADD rmreg16,imm8
		public void ADD (R16Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "83", "/0", "ib"}));
		}
		
		// ADD rmreg32,imm8
		public void ADD (R32Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ADD", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "83", "/0", "ib"}));
		}
		
		// AND mem8,reg8
		public void AND (ByteMemory target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"20", "/r"}));
		}
		
		// AND mem16,reg16
		public void AND (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o16", "21", "/r"}));
		}
		
		// AND mem32,reg32
		public void AND (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o32", "21", "/r"}));
		}
		
		// AND reg8,mem8
		public void AND (R8Type target, ByteMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"22", "/r"}));
		}
		
		// AND reg16,mem16
		public void AND (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "23", "/r"}));
		}
		
		// AND reg32,mem32
		public void AND (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "23", "/r"}));
		}
		
		// AND mem8,imm8
		public void AND (ByteMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"80", "/4", "ib"}));
		}
		
		// AND mem16,imm16
		public void AND (WordMemory target, UInt16 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "81", "/4", "iw"}));
		}
		
		// AND mem32,imm32
		public void AND (DWordMemory target, UInt32 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "81", "/4", "id"}));
		}
		
		// AND mem16,imm8
		public void AND (WordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "83", "/4", "ib"}));
		}
		
		// AND mem32,imm8
		public void AND (DWordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "83", "/4", "ib"}));
		}
		
		// AND rmreg8,reg8
		public void AND (R8Type target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"20", "/r"}));
		}
		
		// AND rmreg16,reg16
		public void AND (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o16", "21", "/r"}));
		}
		
		// AND rmreg32,reg32
		public void AND (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o32", "21", "/r"}));
		}
		
		// AND rmreg8,imm8
		public void AND (R8Type target, Byte source)
		{
			if (target == R8.AL)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND_AL", "AL" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"24", "ib"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"80", "/4", "ib"}));
			}
		}
		
		// AND rmreg16,imm16
		public void AND (R16Type target, UInt16 source)
		{
			if (target == R16.AX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND_AX", "AX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o16", "25", "iw"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "81", "/4", "iw"}));
			}
		}
		
		// AND rmreg32,imm32
		public void AND (R32Type target, UInt32 source)
		{
			if (target == R32.EAX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND_EAX", "EAX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o32", "25", "id"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "81", "/4", "id"}));
			}
		}
		
		// AND rmreg16,imm8
		public void AND (R16Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "83", "/4", "ib"}));
		}
		
		// AND rmreg32,imm8
		public void AND (R32Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "AND", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "83", "/4", "ib"}));
		}
		
		// ARPL mem16,reg16
		public void ARPL (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ARPL", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"63", "/r"}));
		}
		
		// ARPL rmreg16,reg16
		public void ARPL (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ARPL", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"63", "/r"}));
		}
		
		// BOUND reg16,mem
		public void BOUND (R16Type target, Memory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BOUND", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "62", "/r"}));
		}
		
		// BOUND reg32,mem
		public void BOUND (R32Type target, Memory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BOUND", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "62", "/r"}));
		}
		
		// BSF reg16,mem16
		public void BSF (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BSF", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "BC", "/r"}));
		}
		
		// BSF reg32,mem32
		public void BSF (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BSF", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "BC", "/r"}));
		}
		
		// BSF reg16,rmreg16
		public void BSF (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BSF", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "BC", "/r"}));
		}
		
		// BSF reg32,rmreg32
		public void BSF (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BSF", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "BC", "/r"}));
		}
		
		// BSR reg16,mem16
		public void BSR (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BSR", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "BD", "/r"}));
		}
		
		// BSR reg32,mem32
		public void BSR (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BSR", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "BD", "/r"}));
		}
		
		// BSR reg16,rmreg16
		public void BSR (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BSR", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "BD", "/r"}));
		}
		
		// BSR reg32,rmreg32
		public void BSR (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BSR", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "BD", "/r"}));
		}
		
		// BSWAP reg32
		public void BSWAP (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BSWAP", target.ToString(), null, null, target, null, new string[] {"o32", "0F", "C8+r"}));
		}
		
		// BT mem16,reg16
		public void BT (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BT", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o16", "0F", "A3", "/r"}));
		}
		
		// BT mem32,reg32
		public void BT (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BT", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o32", "0F", "A3", "/r"}));
		}
		
		// BT mem16,imm8
		public void BT (WordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BT", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "0F", "BA", "/4", "ib"}));
		}
		
		// BT mem32,imm8
		public void BT (DWordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BT", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "0F", "BA", "/4", "ib"}));
		}
		
		// BT rmreg16,reg16
		public void BT (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BT", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o16", "0F", "A3", "/r"}));
		}
		
		// BT rmreg32,reg32
		public void BT (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BT", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o32", "0F", "A3", "/r"}));
		}
		
		// BT rmreg16,imm8
		public void BT (R16Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BT", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "0F", "BA", "/4", "ib"}));
		}
		
		// BT rmreg32,imm8
		public void BT (R32Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BT", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "0F", "BA", "/4", "ib"}));
		}
		
		// BTC mem16,reg16
		public void BTC (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTC", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o16", "0F", "BB", "/r"}));
		}
		
		// BTC mem32,reg32
		public void BTC (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTC", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o32", "0F", "BB", "/r"}));
		}
		
		// BTC mem16,imm8
		public void BTC (WordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTC", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "0F", "BA", "/7", "ib"}));
		}
		
		// BTC mem32,imm8
		public void BTC (DWordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTC", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "0F", "BA", "/7", "ib"}));
		}
		
		// BTC rmreg16,reg16
		public void BTC (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTC", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o16", "0F", "BB", "/r"}));
		}
		
		// BTC rmreg32,reg32
		public void BTC (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTC", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o32", "0F", "BB", "/r"}));
		}
		
		// BTC rmreg16,imm8
		public void BTC (R16Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTC", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "0F", "BA", "/7", "ib"}));
		}
		
		// BTC rmreg32,imm8
		public void BTC (R32Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTC", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "0F", "BA", "/7", "ib"}));
		}
		
		// BTR mem16,reg16
		public void BTR (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTR", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o16", "0F", "B3", "/r"}));
		}
		
		// BTR mem32,reg32
		public void BTR (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTR", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o32", "0F", "B3", "/r"}));
		}
		
		// BTR mem16,imm8
		public void BTR (WordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "0F", "BA", "/6", "ib"}));
		}
		
		// BTR mem32,imm8
		public void BTR (DWordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "0F", "BA", "/6", "ib"}));
		}
		
		// BTR rmreg16,reg16
		public void BTR (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTR", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o16", "0F", "B3", "/r"}));
		}
		
		// BTR rmreg32,reg32
		public void BTR (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTR", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o32", "0F", "B3", "/r"}));
		}
		
		// BTR rmreg16,imm8
		public void BTR (R16Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "0F", "BA", "/6", "ib"}));
		}
		
		// BTR rmreg32,imm8
		public void BTR (R32Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "0F", "BA", "/6", "ib"}));
		}
		
		// BTS mem16,reg16
		public void BTS (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTS", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o16", "0F", "AB", "/r"}));
		}
		
		// BTS mem32,reg32
		public void BTS (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTS", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o32", "0F", "AB", "/r"}));
		}
		
		// BTS mem16,imm8
		public void BTS (WordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTS", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "0F", "BA", "/5", "ib"}));
		}
		
		// BTS mem32,imm8
		public void BTS (DWordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTS", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "0F", "BA", "/5", "ib"}));
		}
		
		// BTS rmreg16,reg16
		public void BTS (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTS", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o16", "0F", "AB", "/r"}));
		}
		
		// BTS rmreg32,reg32
		public void BTS (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTS", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o32", "0F", "AB", "/r"}));
		}
		
		// BTS rmreg16,imm8
		public void BTS (R16Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTS", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "0F", "BA", "/5", "ib"}));
		}
		
		// BTS rmreg32,imm8
		public void BTS (R32Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "BTS", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "0F", "BA", "/5", "ib"}));
		}
		
		// CALL imm
		public void CALL (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CALL", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"E8", "rw/rd"}));
		}
		
		public void CALL (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "CALL", label, null, null, null, new UInt32[] {0}, new string[] {"E8", "rw/rd"}));
		}
		
		// CALL imm16:imm16
		public void CALL (UInt16 target, UInt16 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CALL", string.Format("0x{0:x}", target) + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source, target}, new string[] {"o16", "9A", "iw", "iw"}));
		}
		
		// CALL imm16:imm32
		public void CALL (UInt16 target, UInt32 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CALL", string.Format("0x{0:x}", target) + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source, target}, new string[] {"o32", "9A", "id", "iw"}));
		}
		
		// CALL FAR mem16
		public void CALL_FAR (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CALL_FAR", target.ToString(), target, null, null, null, new string[] {"o16", "FF", "/3"}));
		}
		
		// CALL FAR mem32
		public void CALL_FAR (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CALL_FAR", target.ToString(), target, null, null, null, new string[] {"o32", "FF", "/3"}));
		}
		
		// CALL mem16
		public void CALL (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CALL", target.ToString(), target, null, null, null, new string[] {"o16", "FF", "/2"}));
		}
		
		// CALL mem32
		public void CALL (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CALL", target.ToString(), target, null, null, null, new string[] {"o32", "FF", "/2"}));
		}
		
		// CALL rmreg16
		public void CALL (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CALL", target.ToString(), null, target, null, null, new string[] {"o16", "FF", "/2"}));
		}
		
		// CALL rmreg32
		public void CALL (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CALL", target.ToString(), null, target, null, null, new string[] {"o32", "FF", "/2"}));
		}
		
		// CBW 
		public void CBW ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CBW", "", null, null, null, null, new string[] {"o16", "98"}));
		}
		
		// CDQ 
		public void CDQ ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CDQ", "", null, null, null, null, new string[] {"o32", "99"}));
		}
		
		// CLC 
		public void CLC ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CLC", "", null, null, null, null, new string[] {"F8"}));
		}
		
		// CLD 
		public void CLD ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CLD", "", null, null, null, null, new string[] {"FC"}));
		}
		
		// CLFLUSH mem
		public void CLFLUSH (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CLFLUSH", target.ToString(), target, null, null, null, new string[] {"0F", "AE", "/7"}));
		}
		
		// CLI 
		public void CLI ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CLI", "", null, null, null, null, new string[] {"FA"}));
		}
		
		// CLTS 
		public void CLTS ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CLTS", "", null, null, null, null, new string[] {"0F", "06"}));
		}
		
		// CMC 
		public void CMC ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMC", "", null, null, null, null, new string[] {"F5"}));
		}
		
		// CMOVA reg16,mem16
		public void CMOVA (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVA", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "47", "/r"}));
		}
		
		// CMOVA reg32,mem32
		public void CMOVA (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVA", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "47", "/r"}));
		}
		
		// CMOVA reg16,rmreg16
		public void CMOVA (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVA", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "47", "/r"}));
		}
		
		// CMOVA reg32,rmreg32
		public void CMOVA (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVA", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "47", "/r"}));
		}
		
		// CMOVAE reg16,mem16
		public void CMOVAE (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVAE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "43", "/r"}));
		}
		
		// CMOVAE reg32,mem32
		public void CMOVAE (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVAE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "43", "/r"}));
		}
		
		// CMOVAE reg16,rmreg16
		public void CMOVAE (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVAE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "43", "/r"}));
		}
		
		// CMOVAE reg32,rmreg32
		public void CMOVAE (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVAE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "43", "/r"}));
		}
		
		// CMOVB reg16,mem16
		public void CMOVB (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVB", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "42", "/r"}));
		}
		
		// CMOVB reg32,mem32
		public void CMOVB (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVB", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "42", "/r"}));
		}
		
		// CMOVB reg16,rmreg16
		public void CMOVB (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVB", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "42", "/r"}));
		}
		
		// CMOVB reg32,rmreg32
		public void CMOVB (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVB", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "42", "/r"}));
		}
		
		// CMOVBE reg16,mem16
		public void CMOVBE (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVBE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "46", "/r"}));
		}
		
		// CMOVBE reg32,mem32
		public void CMOVBE (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVBE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "46", "/r"}));
		}
		
		// CMOVBE reg16,rmreg16
		public void CMOVBE (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVBE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "46", "/r"}));
		}
		
		// CMOVBE reg32,rmreg32
		public void CMOVBE (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVBE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "46", "/r"}));
		}
		
		// CMOVC reg16,mem16
		public void CMOVC (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVC", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "42", "/r"}));
		}
		
		// CMOVC reg32,mem32
		public void CMOVC (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVC", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "42", "/r"}));
		}
		
		// CMOVC reg16,rmreg16
		public void CMOVC (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVC", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "42", "/r"}));
		}
		
		// CMOVC reg32,rmreg32
		public void CMOVC (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVC", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "42", "/r"}));
		}
		
		// CMOVE reg16,mem16
		public void CMOVE (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "44", "/r"}));
		}
		
		// CMOVE reg32,mem32
		public void CMOVE (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "44", "/r"}));
		}
		
		// CMOVE reg16,rmreg16
		public void CMOVE (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "44", "/r"}));
		}
		
		// CMOVE reg32,rmreg32
		public void CMOVE (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "44", "/r"}));
		}
		
		// CMOVG reg16,mem16
		public void CMOVG (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVG", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "4F", "/r"}));
		}
		
		// CMOVG reg32,mem32
		public void CMOVG (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVG", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "4F", "/r"}));
		}
		
		// CMOVG reg16,rmreg16
		public void CMOVG (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVG", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "4F", "/r"}));
		}
		
		// CMOVG reg32,rmreg32
		public void CMOVG (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVG", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "4F", "/r"}));
		}
		
		// CMOVGE reg16,mem16
		public void CMOVGE (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVGE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "4D", "/r"}));
		}
		
		// CMOVGE reg32,mem32
		public void CMOVGE (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVGE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "4D", "/r"}));
		}
		
		// CMOVGE reg16,rmreg16
		public void CMOVGE (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVGE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "4D", "/r"}));
		}
		
		// CMOVGE reg32,rmreg32
		public void CMOVGE (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVGE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "4D", "/r"}));
		}
		
		// CMOVL reg16,mem16
		public void CMOVL (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVL", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "4C", "/r"}));
		}
		
		// CMOVL reg32,mem32
		public void CMOVL (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVL", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "4C", "/r"}));
		}
		
		// CMOVL reg16,rmreg16
		public void CMOVL (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVL", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "4C", "/r"}));
		}
		
		// CMOVL reg32,rmreg32
		public void CMOVL (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVL", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "4C", "/r"}));
		}
		
		// CMOVLE reg16,mem16
		public void CMOVLE (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVLE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "4E", "/r"}));
		}
		
		// CMOVLE reg32,mem32
		public void CMOVLE (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVLE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "4E", "/r"}));
		}
		
		// CMOVLE reg16,rmreg16
		public void CMOVLE (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVLE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "4E", "/r"}));
		}
		
		// CMOVLE reg32,rmreg32
		public void CMOVLE (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVLE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "4E", "/r"}));
		}
		
		// CMOVNA reg16,mem16
		public void CMOVNA (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNA", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "46", "/r"}));
		}
		
		// CMOVNA reg32,mem32
		public void CMOVNA (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNA", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "46", "/r"}));
		}
		
		// CMOVNA reg16,rmreg16
		public void CMOVNA (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNA", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "46", "/r"}));
		}
		
		// CMOVNA reg32,rmreg32
		public void CMOVNA (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNA", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "46", "/r"}));
		}
		
		// CMOVNAE reg16,mem16
		public void CMOVNAE (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNAE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "42", "/r"}));
		}
		
		// CMOVNAE reg32,mem32
		public void CMOVNAE (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNAE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "42", "/r"}));
		}
		
		// CMOVNAE reg16,rmreg16
		public void CMOVNAE (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNAE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "42", "/r"}));
		}
		
		// CMOVNAE reg32,rmreg32
		public void CMOVNAE (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNAE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "42", "/r"}));
		}
		
		// CMOVNB reg16,mem16
		public void CMOVNB (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNB", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "43", "/r"}));
		}
		
		// CMOVNB reg32,mem32
		public void CMOVNB (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNB", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "43", "/r"}));
		}
		
		// CMOVNB reg16,rmreg16
		public void CMOVNB (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNB", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "43", "/r"}));
		}
		
		// CMOVNB reg32,rmreg32
		public void CMOVNB (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNB", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "43", "/r"}));
		}
		
		// CMOVNBE reg16,mem16
		public void CMOVNBE (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNBE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "47", "/r"}));
		}
		
		// CMOVNBE reg32,mem32
		public void CMOVNBE (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNBE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "47", "/r"}));
		}
		
		// CMOVNBE reg16,rmreg16
		public void CMOVNBE (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNBE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "47", "/r"}));
		}
		
		// CMOVNBE reg32,rmreg32
		public void CMOVNBE (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNBE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "47", "/r"}));
		}
		
		// CMOVNC reg16,mem16
		public void CMOVNC (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNC", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "43", "/r"}));
		}
		
		// CMOVNC reg32,mem32
		public void CMOVNC (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNC", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "43", "/r"}));
		}
		
		// CMOVNC reg16,rmreg16
		public void CMOVNC (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNC", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "43", "/r"}));
		}
		
		// CMOVNC reg32,rmreg32
		public void CMOVNC (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNC", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "43", "/r"}));
		}
		
		// CMOVNE reg16,mem16
		public void CMOVNE (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "45", "/r"}));
		}
		
		// CMOVNE reg32,mem32
		public void CMOVNE (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "45", "/r"}));
		}
		
		// CMOVNE reg16,rmreg16
		public void CMOVNE (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "45", "/r"}));
		}
		
		// CMOVNE reg32,rmreg32
		public void CMOVNE (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "45", "/r"}));
		}
		
		// CMOVNG reg16,mem16
		public void CMOVNG (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNG", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "4E", "/r"}));
		}
		
		// CMOVNG reg32,mem32
		public void CMOVNG (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNG", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "4E", "/r"}));
		}
		
		// CMOVNG reg16,rmreg16
		public void CMOVNG (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNG", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "4E", "/r"}));
		}
		
		// CMOVNG reg32,rmreg32
		public void CMOVNG (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNG", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "4E", "/r"}));
		}
		
		// CMOVNGE reg16,mem16
		public void CMOVNGE (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNGE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "4C", "/r"}));
		}
		
		// CMOVNGE reg32,mem32
		public void CMOVNGE (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNGE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "4C", "/r"}));
		}
		
		// CMOVNGE reg16,rmreg16
		public void CMOVNGE (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNGE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "4C", "/r"}));
		}
		
		// CMOVNGE reg32,rmreg32
		public void CMOVNGE (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNGE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "4C", "/r"}));
		}
		
		// CMOVNL reg16,mem16
		public void CMOVNL (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNL", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "4D", "/r"}));
		}
		
		// CMOVNL reg32,mem32
		public void CMOVNL (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNL", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "4D", "/r"}));
		}
		
		// CMOVNL reg16,rmreg16
		public void CMOVNL (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNL", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "4D", "/r"}));
		}
		
		// CMOVNL reg32,rmreg32
		public void CMOVNL (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNL", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "4D", "/r"}));
		}
		
		// CMOVNLE reg16,mem16
		public void CMOVNLE (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNLE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "4F", "/r"}));
		}
		
		// CMOVNLE reg32,mem32
		public void CMOVNLE (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNLE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "4F", "/r"}));
		}
		
		// CMOVNLE reg16,rmreg16
		public void CMOVNLE (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNLE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "4F", "/r"}));
		}
		
		// CMOVNLE reg32,rmreg32
		public void CMOVNLE (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNLE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "4F", "/r"}));
		}
		
		// CMOVNO reg16,mem16
		public void CMOVNO (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNO", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "41", "/r"}));
		}
		
		// CMOVNO reg32,mem32
		public void CMOVNO (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNO", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "41", "/r"}));
		}
		
		// CMOVNO reg16,rmreg16
		public void CMOVNO (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNO", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "41", "/r"}));
		}
		
		// CMOVNO reg32,rmreg32
		public void CMOVNO (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNO", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "41", "/r"}));
		}
		
		// CMOVNP reg16,mem16
		public void CMOVNP (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNP", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "4B", "/r"}));
		}
		
		// CMOVNP reg32,mem32
		public void CMOVNP (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNP", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "4B", "/r"}));
		}
		
		// CMOVNP reg16,rmreg16
		public void CMOVNP (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNP", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "4B", "/r"}));
		}
		
		// CMOVNP reg32,rmreg32
		public void CMOVNP (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNP", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "4B", "/r"}));
		}
		
		// CMOVNS reg16,mem16
		public void CMOVNS (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNS", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "49", "/r"}));
		}
		
		// CMOVNS reg32,mem32
		public void CMOVNS (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNS", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "49", "/r"}));
		}
		
		// CMOVNS reg16,rmreg16
		public void CMOVNS (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNS", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "49", "/r"}));
		}
		
		// CMOVNS reg32,rmreg32
		public void CMOVNS (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNS", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "49", "/r"}));
		}
		
		// CMOVNZ reg16,mem16
		public void CMOVNZ (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNZ", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "45", "/r"}));
		}
		
		// CMOVNZ reg32,mem32
		public void CMOVNZ (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNZ", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "45", "/r"}));
		}
		
		// CMOVNZ reg16,rmreg16
		public void CMOVNZ (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNZ", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "45", "/r"}));
		}
		
		// CMOVNZ reg32,rmreg32
		public void CMOVNZ (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVNZ", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "45", "/r"}));
		}
		
		// CMOVO reg16,mem16
		public void CMOVO (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVO", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "40", "/r"}));
		}
		
		// CMOVO reg32,mem32
		public void CMOVO (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVO", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "40", "/r"}));
		}
		
		// CMOVO reg16,rmreg16
		public void CMOVO (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVO", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "40", "/r"}));
		}
		
		// CMOVO reg32,rmreg32
		public void CMOVO (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVO", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "40", "/r"}));
		}
		
		// CMOVP reg16,mem16
		public void CMOVP (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVP", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "4A", "/r"}));
		}
		
		// CMOVP reg32,mem32
		public void CMOVP (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVP", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "4A", "/r"}));
		}
		
		// CMOVP reg16,rmreg16
		public void CMOVP (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVP", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "4A", "/r"}));
		}
		
		// CMOVP reg32,rmreg32
		public void CMOVP (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVP", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "4A", "/r"}));
		}
		
		// CMOVPE reg16,mem16
		public void CMOVPE (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVPE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "4A", "/r"}));
		}
		
		// CMOVPE reg32,mem32
		public void CMOVPE (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVPE", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "4A", "/r"}));
		}
		
		// CMOVPE reg16,rmreg16
		public void CMOVPE (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVPE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "4A", "/r"}));
		}
		
		// CMOVPE reg32,rmreg32
		public void CMOVPE (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVPE", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "4A", "/r"}));
		}
		
		// CMOVPO reg16,mem16
		public void CMOVPO (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVPO", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "4B", "/r"}));
		}
		
		// CMOVPO reg32,mem32
		public void CMOVPO (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVPO", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "4B", "/r"}));
		}
		
		// CMOVPO reg16,rmreg16
		public void CMOVPO (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVPO", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "4B", "/r"}));
		}
		
		// CMOVPO reg32,rmreg32
		public void CMOVPO (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVPO", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "4B", "/r"}));
		}
		
		// CMOVS reg16,mem16
		public void CMOVS (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVS", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "48", "/r"}));
		}
		
		// CMOVS reg32,mem32
		public void CMOVS (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVS", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "48", "/r"}));
		}
		
		// CMOVS reg16,rmreg16
		public void CMOVS (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVS", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "48", "/r"}));
		}
		
		// CMOVS reg32,rmreg32
		public void CMOVS (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVS", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "48", "/r"}));
		}
		
		// CMOVZ reg16,mem16
		public void CMOVZ (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVZ", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "44", "/r"}));
		}
		
		// CMOVZ reg32,mem32
		public void CMOVZ (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVZ", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "44", "/r"}));
		}
		
		// CMOVZ reg16,rmreg16
		public void CMOVZ (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVZ", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "44", "/r"}));
		}
		
		// CMOVZ reg32,rmreg32
		public void CMOVZ (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMOVZ", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "44", "/r"}));
		}
		
		// CMP mem8,reg8
		public void CMP (ByteMemory target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"38", "/r"}));
		}
		
		// CMP mem16,reg16
		public void CMP (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o16", "39", "/r"}));
		}
		
		// CMP mem32,reg32
		public void CMP (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o32", "39", "/r"}));
		}
		
		// CMP reg8,mem8
		public void CMP (R8Type target, ByteMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"3A", "/r"}));
		}
		
		// CMP reg16,mem16
		public void CMP (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "3B", "/r"}));
		}
		
		// CMP reg32,mem32
		public void CMP (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "3B", "/r"}));
		}
		
		// CMP mem8,imm8
		public void CMP (ByteMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"80", "/7", "ib"}));
		}
		
		// CMP mem16,imm16
		public void CMP (WordMemory target, UInt16 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "81", "/7", "iw"}));
		}
		
		// CMP mem32,imm32
		public void CMP (DWordMemory target, UInt32 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "81", "/7", "id"}));
		}
		
		// CMP mem16,imm8
		public void CMP (WordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "83", "/7", "ib"}));
		}
		
		// CMP mem32,imm8
		public void CMP (DWordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "83", "/7", "ib"}));
		}
		
		// CMP rmreg8,reg8
		public void CMP (R8Type target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"38", "/r"}));
		}
		
		// CMP rmreg16,reg16
		public void CMP (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o16", "39", "/r"}));
		}
		
		// CMP rmreg32,reg32
		public void CMP (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o32", "39", "/r"}));
		}
		
		// CMP rmreg8,imm8
		public void CMP (R8Type target, Byte source)
		{
			if (target == R8.AL)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP_AL", "AL" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"3C", "ib"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"80", "/7", "ib"}));
			}
		}
		
		// CMP rmreg16,imm16
		public void CMP (R16Type target, UInt16 source)
		{
			if (target == R16.AX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP_AX", "AX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o16", "3D", "iw"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "81", "/7", "iw"}));
			}
		}
		
		// CMP rmreg32,imm32
		public void CMP (R32Type target, UInt32 source)
		{
			if (target == R32.EAX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP_EAX", "EAX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o32", "3D", "id"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "81", "/7", "id"}));
			}
		}
		
		// CMP rmreg16,imm8
		public void CMP (R16Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "83", "/7", "ib"}));
		}
		
		// CMP rmreg32,imm8
		public void CMP (R32Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMP", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "83", "/7", "ib"}));
		}
		
		// CMPSB 
		public void CMPSB ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMPSB", "", null, null, null, null, new string[] {"A6"}));
		}
		
		// CMPSD 
		public void CMPSD ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMPSD", "", null, null, null, null, new string[] {"o32", "A7"}));
		}
		
		// CMPSW 
		public void CMPSW ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMPSW", "", null, null, null, null, new string[] {"o16", "A7"}));
		}
		
		// CMPXCHG mem8,reg8
		public void CMPXCHG (ByteMemory target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMPXCHG", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"0F", "B0", "/r"}));
		}
		
		// CMPXCHG mem16,reg16
		public void CMPXCHG (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMPXCHG", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o16", "0F", "B1", "/r"}));
		}
		
		// CMPXCHG mem32,reg32
		public void CMPXCHG (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMPXCHG", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o32", "0F", "B1", "/r"}));
		}
		
		// CMPXCHG rmreg8,reg8
		public void CMPXCHG (R8Type target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMPXCHG", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"0F", "B0", "/r"}));
		}
		
		// CMPXCHG rmreg16,reg16
		public void CMPXCHG (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMPXCHG", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o16", "0F", "B1", "/r"}));
		}
		
		// CMPXCHG rmreg32,reg32
		public void CMPXCHG (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMPXCHG", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o32", "0F", "B1", "/r"}));
		}
		
		// CMPXCHG8B mem
		public void CMPXCHG8B (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CMPXCHG8B", target.ToString(), target, null, null, null, new string[] {"0F", "C7", "/1"}));
		}
		
		// CPUID 
		public void CPUID ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CPUID", "", null, null, null, null, new string[] {"0F", "A2"}));
		}
		
		// CWD 
		public void CWD ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CWD", "", null, null, null, null, new string[] {"o16", "99"}));
		}
		
		// CWDE 
		public void CWDE ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "CWDE", "", null, null, null, null, new string[] {"o32", "98"}));
		}
		
		// DAA 
		public void DAA ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "DAA", "", null, null, null, null, new string[] {"27"}));
		}
		
		// DAS 
		public void DAS ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "DAS", "", null, null, null, null, new string[] {"2F"}));
		}
		
		// DEC reg16
		public void DEC (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "DEC", target.ToString(), null, null, target, null, new string[] {"o16", "48+r"}));
		}
		
		// DEC reg32
		public void DEC (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "DEC", target.ToString(), null, null, target, null, new string[] {"o32", "48+r"}));
		}
		
		// DEC mem8
		public void DEC (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "DEC", target.ToString(), target, null, null, null, new string[] {"FE", "/1"}));
		}
		
		// DEC mem16
		public void DEC (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "DEC", target.ToString(), target, null, null, null, new string[] {"o16", "FF", "/1"}));
		}
		
		// DEC mem32
		public void DEC (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "DEC", target.ToString(), target, null, null, null, new string[] {"o32", "FF", "/1"}));
		}
		
		// DEC rmreg8
		public void DEC (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "DEC", target.ToString(), null, target, null, null, new string[] {"FE", "/1"}));
		}
		
		// DIV mem8
		public void DIV (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "DIV", target.ToString(), target, null, null, null, new string[] {"F6", "/6"}));
		}
		
		// DIV mem16
		public void DIV (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "DIV", target.ToString(), target, null, null, null, new string[] {"o16", "F7", "/6"}));
		}
		
		// DIV mem32
		public void DIV (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "DIV", target.ToString(), target, null, null, null, new string[] {"o32", "F7", "/6"}));
		}
		
		// DIV rmreg8
		public void DIV (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "DIV", target.ToString(), null, target, null, null, new string[] {"F6", "/6"}));
		}
		
		// DIV rmreg16
		public void DIV (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "DIV", target.ToString(), null, target, null, null, new string[] {"o16", "F7", "/6"}));
		}
		
		// DIV rmreg32
		public void DIV (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "DIV", target.ToString(), null, target, null, null, new string[] {"o32", "F7", "/6"}));
		}
		
		// EMMS 
		public void EMMS ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "EMMS", "", null, null, null, null, new string[] {"0F", "77"}));
		}
		
		// ENTER imm16,imm8
		public void ENTER (UInt16 target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ENTER", string.Format("0x{0:x}", target) + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {target, source}, new string[] {"C8", "iw", "ib"}));
		}
		
		// F2XM1 
		public void F2XM1 ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "F2XM1", "", null, null, null, null, new string[] {"D9", "F0"}));
		}
		
		// FABS 
		public void FABS ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FABS", "", null, null, null, null, new string[] {"D9", "E1"}));
		}
		
		// FADD mem32
		public void FADD (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FADD", target.ToString(), target, null, null, null, new string[] {"D8", "/0"}));
		}
		
		// FADD mem64
		public void FADD (QWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FADD", target.ToString(), target, null, null, null, new string[] {"DC", "/0"}));
		}
		
		// FADD fpureg
		public void FADD (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FADD", target.ToString(), null, null, target, null, new string[] {"D8", "C0+r"}));
		}
		
		// FADD ST0,fpureg
		public void FADD_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FADD_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"D8", "C0+r"}));
		}
		
		// FADD fpureg,ST0
		public void FADD__ST0 (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FADD__ST0", target.ToString() + ", " + "ST0", null, null, target, null, new string[] {"DC", "C0+r"}));
		}
		
		// FADDP fpureg
		public void FADDP (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FADDP", target.ToString(), null, null, target, null, new string[] {"DE", "C0+r"}));
		}
		
		// FADDP fpureg,ST0
		public void FADDP__ST0 (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FADDP__ST0", target.ToString() + ", " + "ST0", null, null, target, null, new string[] {"DE", "C0+r"}));
		}
		
		// FBLD mem80
		public void FBLD (TWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FBLD", target.ToString(), target, null, null, null, new string[] {"DF", "/4"}));
		}
		
		// FBSTP mem80
		public void FBSTP (TWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FBSTP", target.ToString(), target, null, null, null, new string[] {"DF", "/6"}));
		}
		
		// FCHS 
		public void FCHS ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCHS", "", null, null, null, null, new string[] {"D9", "E0"}));
		}
		
		// FCLEX 
		public void FCLEX ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCLEX", "", null, null, null, null, new string[] {"9B", "DB", "E2"}));
		}
		
		// FCMOVB fpureg
		public void FCMOVB (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCMOVB", target.ToString(), null, null, target, null, new string[] {"DA", "C0+r"}));
		}
		
		// FCMOVB ST0,fpureg
		public void FCMOVB_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCMOVB_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"DA", "C0+r"}));
		}
		
		// FCMOVBE fpureg
		public void FCMOVBE (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCMOVBE", target.ToString(), null, null, target, null, new string[] {"DA", "D0+r"}));
		}
		
		// FCMOVBE ST0,fpureg
		public void FCMOVBE_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCMOVBE_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"DA", "D0+r"}));
		}
		
		// FCMOVE fpureg
		public void FCMOVE (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCMOVE", target.ToString(), null, null, target, null, new string[] {"DA", "C8+r"}));
		}
		
		// FCMOVE ST0,fpureg
		public void FCMOVE_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCMOVE_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"DA", "C8+r"}));
		}
		
		// FCMOVNB fpureg
		public void FCMOVNB (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCMOVNB", target.ToString(), null, null, target, null, new string[] {"DB", "C0+r"}));
		}
		
		// FCMOVNB ST0,fpureg
		public void FCMOVNB_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCMOVNB_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"DB", "C0+r"}));
		}
		
		// FCMOVNBE fpureg
		public void FCMOVNBE (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCMOVNBE", target.ToString(), null, null, target, null, new string[] {"DB", "D0+r"}));
		}
		
		// FCMOVNBE ST0,fpureg
		public void FCMOVNBE_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCMOVNBE_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"DB", "D0+r"}));
		}
		
		// FCMOVNE fpureg
		public void FCMOVNE (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCMOVNE", target.ToString(), null, null, target, null, new string[] {"DB", "C8+r"}));
		}
		
		// FCMOVNE ST0,fpureg
		public void FCMOVNE_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCMOVNE_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"DB", "C8+r"}));
		}
		
		// FCMOVNU fpureg
		public void FCMOVNU (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCMOVNU", target.ToString(), null, null, target, null, new string[] {"DB", "D8+r"}));
		}
		
		// FCMOVNU ST0,fpureg
		public void FCMOVNU_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCMOVNU_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"DB", "D8+r"}));
		}
		
		// FCMOVU fpureg
		public void FCMOVU (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCMOVU", target.ToString(), null, null, target, null, new string[] {"DA", "D8+r"}));
		}
		
		// FCMOVU ST0,fpureg
		public void FCMOVU_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCMOVU_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"DA", "D8+r"}));
		}
		
		// FCOM mem32
		public void FCOM (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCOM", target.ToString(), target, null, null, null, new string[] {"D8", "/2"}));
		}
		
		// FCOM mem64
		public void FCOM (QWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCOM", target.ToString(), target, null, null, null, new string[] {"DC", "/2"}));
		}
		
		// FCOM fpureg
		public void FCOM (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCOM", target.ToString(), null, null, target, null, new string[] {"D8", "D0+r"}));
		}
		
		// FCOM ST0,fpureg
		public void FCOM_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCOM_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"D8", "D0+r"}));
		}
		
		// FCOMI fpureg
		public void FCOMI (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCOMI", target.ToString(), null, null, target, null, new string[] {"DB", "F0+r"}));
		}
		
		// FCOMI ST0,fpureg
		public void FCOMI_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCOMI_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"DB", "F0+r"}));
		}
		
		// FCOMIP fpureg
		public void FCOMIP (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCOMIP", target.ToString(), null, null, target, null, new string[] {"DF", "F0+r"}));
		}
		
		// FCOMIP ST0,fpureg
		public void FCOMIP_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCOMIP_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"DF", "F0+r"}));
		}
		
		// FCOMP mem32
		public void FCOMP (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCOMP", target.ToString(), target, null, null, null, new string[] {"D8", "/3"}));
		}
		
		// FCOMP mem64
		public void FCOMP (QWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCOMP", target.ToString(), target, null, null, null, new string[] {"DC", "/3"}));
		}
		
		// FCOMP fpureg
		public void FCOMP (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCOMP", target.ToString(), null, null, target, null, new string[] {"D8", "D8+r"}));
		}
		
		// FCOMP ST0,fpureg
		public void FCOMP_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCOMP_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"D8", "D8+r"}));
		}
		
		// FCOMPP 
		public void FCOMPP ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCOMPP", "", null, null, null, null, new string[] {"DE", "D9"}));
		}
		
		// FCOS 
		public void FCOS ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FCOS", "", null, null, null, null, new string[] {"D9", "FF"}));
		}
		
		// FDECSTP 
		public void FDECSTP ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FDECSTP", "", null, null, null, null, new string[] {"D9", "F6"}));
		}
		
		// FDISI 
		public void FDISI ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FDISI", "", null, null, null, null, new string[] {"9B", "DB", "E1"}));
		}
		
		// FDIV mem32
		public void FDIV (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FDIV", target.ToString(), target, null, null, null, new string[] {"D8", "/6"}));
		}
		
		// FDIV mem64
		public void FDIV (QWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FDIV", target.ToString(), target, null, null, null, new string[] {"DC", "/6"}));
		}
		
		// FDIV fpureg
		public void FDIV (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FDIV", target.ToString(), null, null, target, null, new string[] {"D8", "F0+r"}));
		}
		
		// FDIV ST0,fpureg
		public void FDIV_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FDIV_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"D8", "F0+r"}));
		}
		
		// FDIV fpureg,ST0
		public void FDIV__ST0 (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FDIV__ST0", target.ToString() + ", " + "ST0", null, null, target, null, new string[] {"DC", "F8+r"}));
		}
		
		// FDIVP fpureg
		public void FDIVP (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FDIVP", target.ToString(), null, null, target, null, new string[] {"DE", "F8+r"}));
		}
		
		// FDIVP fpureg,ST0
		public void FDIVP__ST0 (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FDIVP__ST0", target.ToString() + ", " + "ST0", null, null, target, null, new string[] {"DE", "F8+r"}));
		}
		
		// FDIVR mem32
		public void FDIVR (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FDIVR", target.ToString(), target, null, null, null, new string[] {"D8", "/7"}));
		}
		
		// FDIVR mem64
		public void FDIVR (QWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FDIVR", target.ToString(), target, null, null, null, new string[] {"DC", "/7"}));
		}
		
		// FDIVR fpureg
		public void FDIVR (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FDIVR", target.ToString(), null, null, target, null, new string[] {"D8", "F8+r"}));
		}
		
		// FDIVR ST0,fpureg
		public void FDIVR_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FDIVR_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"D8", "F8+r"}));
		}
		
		// FDIVR fpureg,ST0
		public void FDIVR__ST0 (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FDIVR__ST0", target.ToString() + ", " + "ST0", null, null, target, null, new string[] {"DC", "F0+r"}));
		}
		
		// FDIVRP fpureg
		public void FDIVRP (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FDIVRP", target.ToString(), null, null, target, null, new string[] {"DE", "F0+r"}));
		}
		
		// FDIVRP fpureg,ST0
		public void FDIVRP__ST0 (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FDIVRP__ST0", target.ToString() + ", " + "ST0", null, null, target, null, new string[] {"DE", "F0+r"}));
		}
		
		// FENI 
		public void FENI ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FENI", "", null, null, null, null, new string[] {"9B", "DB", "E0"}));
		}
		
		// FFREE fpureg
		public void FFREE (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FFREE", target.ToString(), null, null, target, null, new string[] {"DD", "C0+r"}));
		}
		
		// FFREEP fpureg
		public void FFREEP (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FFREEP", target.ToString(), null, null, target, null, new string[] {"DF", "C0+r"}));
		}
		
		// FIADD mem16
		public void FIADD (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FIADD", target.ToString(), target, null, null, null, new string[] {"DE", "/0"}));
		}
		
		// FIADD mem32
		public void FIADD (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FIADD", target.ToString(), target, null, null, null, new string[] {"DA", "/0"}));
		}
		
		// FICOM mem16
		public void FICOM (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FICOM", target.ToString(), target, null, null, null, new string[] {"DE", "/2"}));
		}
		
		// FICOM mem32
		public void FICOM (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FICOM", target.ToString(), target, null, null, null, new string[] {"DA", "/2"}));
		}
		
		// FICOMP mem16
		public void FICOMP (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FICOMP", target.ToString(), target, null, null, null, new string[] {"DE", "/3"}));
		}
		
		// FICOMP mem32
		public void FICOMP (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FICOMP", target.ToString(), target, null, null, null, new string[] {"DA", "/3"}));
		}
		
		// FIDIV mem16
		public void FIDIV (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FIDIV", target.ToString(), target, null, null, null, new string[] {"DE", "/6"}));
		}
		
		// FIDIV mem32
		public void FIDIV (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FIDIV", target.ToString(), target, null, null, null, new string[] {"DA", "/6"}));
		}
		
		// FIDIVR mem16
		public void FIDIVR (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FIDIVR", target.ToString(), target, null, null, null, new string[] {"DE", "/7"}));
		}
		
		// FIDIVR mem32
		public void FIDIVR (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FIDIVR", target.ToString(), target, null, null, null, new string[] {"DA", "/7"}));
		}
		
		// FILD mem16
		public void FILD (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FILD", target.ToString(), target, null, null, null, new string[] {"DF", "/0"}));
		}
		
		// FILD mem32
		public void FILD (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FILD", target.ToString(), target, null, null, null, new string[] {"DB", "/0"}));
		}
		
		// FILD mem64
		public void FILD (QWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FILD", target.ToString(), target, null, null, null, new string[] {"DF", "/5"}));
		}
		
		// FIMUL mem16
		public void FIMUL (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FIMUL", target.ToString(), target, null, null, null, new string[] {"DE", "/1"}));
		}
		
		// FIMUL mem32
		public void FIMUL (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FIMUL", target.ToString(), target, null, null, null, new string[] {"DA", "/1"}));
		}
		
		// FINCSTP 
		public void FINCSTP ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FINCSTP", "", null, null, null, null, new string[] {"D9", "F7"}));
		}
		
		// FINIT 
		public void FINIT ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FINIT", "", null, null, null, null, new string[] {"9B", "DB", "E3"}));
		}
		
		// FIST mem16
		public void FIST (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FIST", target.ToString(), target, null, null, null, new string[] {"DF", "/2"}));
		}
		
		// FIST mem32
		public void FIST (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FIST", target.ToString(), target, null, null, null, new string[] {"DB", "/2"}));
		}
		
		// FISTP mem16
		public void FISTP (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FISTP", target.ToString(), target, null, null, null, new string[] {"DF", "/3"}));
		}
		
		// FISTP mem32
		public void FISTP (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FISTP", target.ToString(), target, null, null, null, new string[] {"DB", "/3"}));
		}
		
		// FISTP mem64
		public void FISTP (QWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FISTP", target.ToString(), target, null, null, null, new string[] {"DF", "/7"}));
		}
		
		// FISUB mem16
		public void FISUB (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FISUB", target.ToString(), target, null, null, null, new string[] {"DE", "/4"}));
		}
		
		// FISUB mem32
		public void FISUB (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FISUB", target.ToString(), target, null, null, null, new string[] {"DA", "/4"}));
		}
		
		// FISUBR mem16
		public void FISUBR (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FISUBR", target.ToString(), target, null, null, null, new string[] {"DE", "/5"}));
		}
		
		// FISUBR mem32
		public void FISUBR (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FISUBR", target.ToString(), target, null, null, null, new string[] {"DA", "/5"}));
		}
		
		// FLD mem32
		public void FLD (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FLD", target.ToString(), target, null, null, null, new string[] {"D9", "/0"}));
		}
		
		// FLD mem64
		public void FLD (QWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FLD", target.ToString(), target, null, null, null, new string[] {"DD", "/0"}));
		}
		
		// FLD mem80
		public void FLD (TWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FLD", target.ToString(), target, null, null, null, new string[] {"DB", "/5"}));
		}
		
		// FLD fpureg
		public void FLD (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FLD", target.ToString(), null, null, target, null, new string[] {"D9", "C0+r"}));
		}
		
		// FLD1 
		public void FLD1 ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FLD1", "", null, null, null, null, new string[] {"D9", "E8"}));
		}
		
		// FLDCW mem16
		public void FLDCW (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FLDCW", target.ToString(), target, null, null, null, new string[] {"D9", "/5"}));
		}
		
		// FLDENV mem
		public void FLDENV (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FLDENV", target.ToString(), target, null, null, null, new string[] {"D9", "/4"}));
		}
		
		// FLDL2E 
		public void FLDL2E ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FLDL2E", "", null, null, null, null, new string[] {"D9", "EA"}));
		}
		
		// FLDL2T 
		public void FLDL2T ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FLDL2T", "", null, null, null, null, new string[] {"D9", "E9"}));
		}
		
		// FLDLG2 
		public void FLDLG2 ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FLDLG2", "", null, null, null, null, new string[] {"D9", "EC"}));
		}
		
		// FLDLN2 
		public void FLDLN2 ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FLDLN2", "", null, null, null, null, new string[] {"D9", "ED"}));
		}
		
		// FLDPI 
		public void FLDPI ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FLDPI", "", null, null, null, null, new string[] {"D9", "EB"}));
		}
		
		// FLDZ 
		public void FLDZ ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FLDZ", "", null, null, null, null, new string[] {"D9", "EE"}));
		}
		
		// FMUL mem32
		public void FMUL (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FMUL", target.ToString(), target, null, null, null, new string[] {"D8", "/1"}));
		}
		
		// FMUL mem64
		public void FMUL (QWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FMUL", target.ToString(), target, null, null, null, new string[] {"DC", "/1"}));
		}
		
		// FMUL fpureg
		public void FMUL (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FMUL", target.ToString(), null, null, target, null, new string[] {"D8", "C8+r"}));
		}
		
		// FMUL ST0,fpureg
		public void FMUL_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FMUL_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"D8", "C8+r"}));
		}
		
		// FMUL fpureg,ST0
		public void FMUL__ST0 (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FMUL__ST0", target.ToString() + ", " + "ST0", null, null, target, null, new string[] {"DC", "C8+r"}));
		}
		
		// FMULP fpureg
		public void FMULP (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FMULP", target.ToString(), null, null, target, null, new string[] {"DE", "C8+r"}));
		}
		
		// FMULP fpureg,ST0
		public void FMULP__ST0 (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FMULP__ST0", target.ToString() + ", " + "ST0", null, null, target, null, new string[] {"DE", "C8+r"}));
		}
		
		// FNCLEX 
		public void FNCLEX ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FNCLEX", "", null, null, null, null, new string[] {"DB", "E2"}));
		}
		
		// FNDISI 
		public void FNDISI ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FNDISI", "", null, null, null, null, new string[] {"DB", "E1"}));
		}
		
		// FNENI 
		public void FNENI ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FNENI", "", null, null, null, null, new string[] {"DB", "E0"}));
		}
		
		// FNINIT 
		public void FNINIT ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FNINIT", "", null, null, null, null, new string[] {"DB", "E3"}));
		}
		
		// FNOP 
		public void FNOP ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FNOP", "", null, null, null, null, new string[] {"D9", "D0"}));
		}
		
		// FNSAVE mem
		public void FNSAVE (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FNSAVE", target.ToString(), target, null, null, null, new string[] {"DD", "/6"}));
		}
		
		// FNSTCW mem16
		public void FNSTCW (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FNSTCW", target.ToString(), target, null, null, null, new string[] {"D9", "/7"}));
		}
		
		// FNSTENV mem
		public void FNSTENV (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FNSTENV", target.ToString(), target, null, null, null, new string[] {"D9", "/6"}));
		}
		
		// FNSTSW mem16
		public void FNSTSW (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FNSTSW", target.ToString(), target, null, null, null, new string[] {"DD", "/7"}));
		}
		
		// FNSTSW AX
		public void FNSTSW_AX ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FNSTSW_AX", "AX", null, null, null, null, new string[] {"DF", "E0"}));
		}
		
		// FPATAN 
		public void FPATAN ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FPATAN", "", null, null, null, null, new string[] {"D9", "F3"}));
		}
		
		// FPREM 
		public void FPREM ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FPREM", "", null, null, null, null, new string[] {"D9", "F8"}));
		}
		
		// FPREM1 
		public void FPREM1 ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FPREM1", "", null, null, null, null, new string[] {"D9", "F5"}));
		}
		
		// FPTAN 
		public void FPTAN ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FPTAN", "", null, null, null, null, new string[] {"D9", "F2"}));
		}
		
		// FRNDINT 
		public void FRNDINT ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FRNDINT", "", null, null, null, null, new string[] {"D9", "FC"}));
		}
		
		// FRSTOR mem
		public void FRSTOR (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FRSTOR", target.ToString(), target, null, null, null, new string[] {"DD", "/4"}));
		}
		
		// FSAVE mem
		public void FSAVE (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSAVE", target.ToString(), target, null, null, null, new string[] {"9B", "DD", "/6"}));
		}
		
		// FSCALE 
		public void FSCALE ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSCALE", "", null, null, null, null, new string[] {"D9", "FD"}));
		}
		
		// FSETPM 
		public void FSETPM ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSETPM", "", null, null, null, null, new string[] {"DB", "E4"}));
		}
		
		// FSIN 
		public void FSIN ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSIN", "", null, null, null, null, new string[] {"D9", "FE"}));
		}
		
		// FSINCOS 
		public void FSINCOS ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSINCOS", "", null, null, null, null, new string[] {"D9", "FB"}));
		}
		
		// FSQRT 
		public void FSQRT ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSQRT", "", null, null, null, null, new string[] {"D9", "FA"}));
		}
		
		// FST mem32
		public void FST (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FST", target.ToString(), target, null, null, null, new string[] {"D9", "/2"}));
		}
		
		// FST mem64
		public void FST (QWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FST", target.ToString(), target, null, null, null, new string[] {"DD", "/2"}));
		}
		
		// FST fpureg
		public void FST (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FST", target.ToString(), null, null, target, null, new string[] {"DD", "D0+r"}));
		}
		
		// FSTCW mem16
		public void FSTCW (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSTCW", target.ToString(), target, null, null, null, new string[] {"9B", "D9", "/7"}));
		}
		
		// FSTENV mem
		public void FSTENV (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSTENV", target.ToString(), target, null, null, null, new string[] {"9B", "D9", "/6"}));
		}
		
		// FSTP mem32
		public void FSTP (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSTP", target.ToString(), target, null, null, null, new string[] {"D9", "/3"}));
		}
		
		// FSTP mem64
		public void FSTP (QWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSTP", target.ToString(), target, null, null, null, new string[] {"DD", "/3"}));
		}
		
		// FSTP mem80
		public void FSTP (TWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSTP", target.ToString(), target, null, null, null, new string[] {"DB", "/7"}));
		}
		
		// FSTP fpureg
		public void FSTP (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSTP", target.ToString(), null, null, target, null, new string[] {"DD", "D8+r"}));
		}
		
		// FSTSW mem16
		public void FSTSW (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSTSW", target.ToString(), target, null, null, null, new string[] {"9B", "DD", "/7"}));
		}
		
		// FSTSW AX
		public void FSTSW_AX ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSTSW_AX", "AX", null, null, null, null, new string[] {"9B", "DF", "E0"}));
		}
		
		// FSUB mem32
		public void FSUB (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSUB", target.ToString(), target, null, null, null, new string[] {"D8", "/4"}));
		}
		
		// FSUB mem64
		public void FSUB (QWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSUB", target.ToString(), target, null, null, null, new string[] {"DC", "/4"}));
		}
		
		// FSUB fpureg
		public void FSUB (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSUB", target.ToString(), null, null, target, null, new string[] {"D8", "E0+r"}));
		}
		
		// FSUB ST0,fpureg
		public void FSUB_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSUB_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"D8", "E0+r"}));
		}
		
		// FSUB fpureg,ST0
		public void FSUB__ST0 (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSUB__ST0", target.ToString() + ", " + "ST0", null, null, target, null, new string[] {"DC", "E8+r"}));
		}
		
		// FSUBP fpureg
		public void FSUBP (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSUBP", target.ToString(), null, null, target, null, new string[] {"DE", "E8+r"}));
		}
		
		// FSUBP fpureg,ST0
		public void FSUBP__ST0 (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSUBP__ST0", target.ToString() + ", " + "ST0", null, null, target, null, new string[] {"DE", "E8+r"}));
		}
		
		// FSUBR mem32
		public void FSUBR (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSUBR", target.ToString(), target, null, null, null, new string[] {"D8", "/5"}));
		}
		
		// FSUBR mem64
		public void FSUBR (QWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSUBR", target.ToString(), target, null, null, null, new string[] {"DC", "/5"}));
		}
		
		// FSUBR fpureg
		public void FSUBR (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSUBR", target.ToString(), null, null, target, null, new string[] {"D8", "E8+r"}));
		}
		
		// FSUBR ST0,fpureg
		public void FSUBR_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSUBR_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"D8", "E8+r"}));
		}
		
		// FSUBR fpureg,ST0
		public void FSUBR__ST0 (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSUBR__ST0", target.ToString() + ", " + "ST0", null, null, target, null, new string[] {"DC", "E0+r"}));
		}
		
		// FSUBRP fpureg
		public void FSUBRP (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSUBRP", target.ToString(), null, null, target, null, new string[] {"DE", "E0+r"}));
		}
		
		// FSUBRP fpureg,ST0
		public void FSUBRP__ST0 (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FSUBRP__ST0", target.ToString() + ", " + "ST0", null, null, target, null, new string[] {"DE", "E0+r"}));
		}
		
		// FTST 
		public void FTST ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FTST", "", null, null, null, null, new string[] {"D9", "E4"}));
		}
		
		// FUCOM fpureg
		public void FUCOM (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FUCOM", target.ToString(), null, null, target, null, new string[] {"DD", "E0+r"}));
		}
		
		// FUCOM ST0,fpureg
		public void FUCOM_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FUCOM_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"DD", "E0+r"}));
		}
		
		// FUCOMI fpureg
		public void FUCOMI (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FUCOMI", target.ToString(), null, null, target, null, new string[] {"DB", "E8+r"}));
		}
		
		// FUCOMI ST0,fpureg
		public void FUCOMI_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FUCOMI_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"DB", "E8+r"}));
		}
		
		// FUCOMIP fpureg
		public void FUCOMIP (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FUCOMIP", target.ToString(), null, null, target, null, new string[] {"DF", "E8+r"}));
		}
		
		// FUCOMIP ST0,fpureg
		public void FUCOMIP_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FUCOMIP_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"DF", "E8+r"}));
		}
		
		// FUCOMP fpureg
		public void FUCOMP (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FUCOMP", target.ToString(), null, null, target, null, new string[] {"DD", "E8+r"}));
		}
		
		// FUCOMP ST0,fpureg
		public void FUCOMP_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FUCOMP_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"DD", "E8+r"}));
		}
		
		// FUCOMPP 
		public void FUCOMPP ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FUCOMPP", "", null, null, null, null, new string[] {"DA", "E9"}));
		}
		
		// FWAIT 
		public void FWAIT ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FWAIT", "", null, null, null, null, new string[] {"9B"}));
		}
		
		// FXAM 
		public void FXAM ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FXAM", "", null, null, null, null, new string[] {"D9", "E5"}));
		}
		
		// FXCH 
		public void FXCH ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FXCH", "", null, null, null, null, new string[] {"D9", "C9"}));
		}
		
		// FXCH fpureg
		public void FXCH (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FXCH", target.ToString(), null, null, target, null, new string[] {"D9", "C8+r"}));
		}
		
		// FXCH fpureg,ST0
		public void FXCH__ST0 (FPType target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FXCH__ST0", target.ToString() + ", " + "ST0", null, null, target, null, new string[] {"D9", "C8+r"}));
		}
		
		// FXCH ST0,fpureg
		public void FXCH_ST0 (FPType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FXCH_ST0", "ST0" + ", " + source.ToString(), null, null, source, null, new string[] {"D9", "C8+r"}));
		}
		
		// FXRSTOR memory
		public void FXRSTOR (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FXRSTOR", target.ToString(), target, null, null, null, new string[] {"0F", "AE", "/1"}));
		}
		
		// FXSAVE memory
		public void FXSAVE (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FXSAVE", target.ToString(), target, null, null, null, new string[] {"0F", "AE", "/0"}));
		}
		
		// FXTRACT 
		public void FXTRACT ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FXTRACT", "", null, null, null, null, new string[] {"D9", "F4"}));
		}
		
		// FYL2X 
		public void FYL2X ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FYL2X", "", null, null, null, null, new string[] {"D9", "F1"}));
		}
		
		// FYL2XP1 
		public void FYL2XP1 ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "FYL2XP1", "", null, null, null, null, new string[] {"D9", "F9"}));
		}
		
		// HLT 
		public void HLT ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "HLT", "", null, null, null, null, new string[] {"F4"}));
		}
		
		// ICEBP 
		public void ICEBP ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ICEBP", "", null, null, null, null, new string[] {"F1"}));
		}
		
		// IDIV mem8
		public void IDIV (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IDIV", target.ToString(), target, null, null, null, new string[] {"F6", "/7"}));
		}
		
		// IDIV mem16
		public void IDIV (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IDIV", target.ToString(), target, null, null, null, new string[] {"o16", "F7", "/7"}));
		}
		
		// IDIV mem32
		public void IDIV (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IDIV", target.ToString(), target, null, null, null, new string[] {"o32", "F7", "/7"}));
		}
		
		// IDIV rmreg8
		public void IDIV (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IDIV", target.ToString(), null, target, null, null, new string[] {"F6", "/7"}));
		}
		
		// IDIV rmreg16
		public void IDIV (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IDIV", target.ToString(), null, target, null, null, new string[] {"o16", "F7", "/7"}));
		}
		
		// IDIV rmreg32
		public void IDIV (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IDIV", target.ToString(), null, target, null, null, new string[] {"o32", "F7", "/7"}));
		}
		
		// IMUL mem8
		public void IMUL (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString(), target, null, null, null, new string[] {"F6", "/5"}));
		}
		
		// IMUL mem16
		public void IMUL (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString(), target, null, null, null, new string[] {"o16", "F7", "/5"}));
		}
		
		// IMUL mem32
		public void IMUL (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString(), target, null, null, null, new string[] {"o32", "F7", "/5"}));
		}
		
		// IMUL reg16,mem16
		public void IMUL (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "AF", "/r"}));
		}
		
		// IMUL reg32,mem32
		public void IMUL (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "AF", "/r"}));
		}
		
		// IMUL reg16,imm8
		public void IMUL (R16Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString() + ", " + string.Format("0x{0:x}", source), null, null, target, new UInt32[] {source}, new string[] {"o16", "6B", "/r", "ib"}));
		}
		
		// IMUL reg16,imm16
		public void IMUL (R16Type target, UInt16 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString() + ", " + string.Format("0x{0:x}", source), null, null, target, new UInt32[] {source}, new string[] {"o16", "69", "/r", "iw"}));
		}
		
		// IMUL reg32,imm8
		public void IMUL (R32Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString() + ", " + string.Format("0x{0:x}", source), null, null, target, new UInt32[] {source}, new string[] {"o32", "6B", "/r", "ib"}));
		}
		
		// IMUL reg32,imm32
		public void IMUL (R32Type target, UInt32 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString() + ", " + string.Format("0x{0:x}", source), null, null, target, new UInt32[] {source}, new string[] {"o32", "69", "/r", "id"}));
		}
		
		// IMUL reg16,mem16,imm8
		public void IMUL (R16Type target, WordMemory source, Byte value)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString() + ", " + source.ToString() + ", " + string.Format("0x{0:x}", value), source, null, target, new UInt32[] {value}, new string[] {"o16", "6B", "/r", "ib"}));
		}
		
		// IMUL reg16,mem16,imm16
		public void IMUL (R16Type target, WordMemory source, UInt16 value)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString() + ", " + source.ToString() + ", " + string.Format("0x{0:x}", value), source, null, target, new UInt32[] {value}, new string[] {"o16", "69", "/r", "iw"}));
		}
		
		// IMUL reg32,mem32,imm8
		public void IMUL (R32Type target, DWordMemory source, Byte value)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString() + ", " + source.ToString() + ", " + string.Format("0x{0:x}", value), source, null, target, new UInt32[] {value}, new string[] {"o32", "6B", "/r", "ib"}));
		}
		
		// IMUL reg32,mem32,imm32
		public void IMUL (R32Type target, DWordMemory source, UInt32 value)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString() + ", " + source.ToString() + ", " + string.Format("0x{0:x}", value), source, null, target, new UInt32[] {value}, new string[] {"o32", "69", "/r", "id"}));
		}
		
		// IMUL rmreg8
		public void IMUL (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString(), null, target, null, null, new string[] {"F6", "/5"}));
		}
		
		// IMUL rmreg16
		public void IMUL (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString(), null, target, null, null, new string[] {"o16", "F7", "/5"}));
		}
		
		// IMUL rmreg32
		public void IMUL (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString(), null, target, null, null, new string[] {"o32", "F7", "/5"}));
		}
		
		// IMUL reg16,rmreg16
		public void IMUL (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "AF", "/r"}));
		}
		
		// IMUL reg32,rmreg32
		public void IMUL (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "AF", "/r"}));
		}
		
		// IMUL reg16,rmreg16,imm8
		public void IMUL (R16Type target, R16Type source, Byte value)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString() + ", " + source.ToString() + ", " + string.Format("0x{0:x}", value), null, source, target, new UInt32[] {value}, new string[] {"o16", "6B", "/r", "ib"}));
		}
		
		// IMUL reg16,rmreg16,imm16
		public void IMUL (R16Type target, R16Type source, UInt16 value)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString() + ", " + source.ToString() + ", " + string.Format("0x{0:x}", value), null, source, target, new UInt32[] {value}, new string[] {"o16", "69", "/r", "iw"}));
		}
		
		// IMUL reg32,rmreg32,imm8
		public void IMUL (R32Type target, R32Type source, Byte value)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString() + ", " + source.ToString() + ", " + string.Format("0x{0:x}", value), null, source, target, new UInt32[] {value}, new string[] {"o32", "6B", "/r", "ib"}));
		}
		
		// IMUL reg32,rmreg32,imm32
		public void IMUL (R32Type target, R32Type source, UInt32 value)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IMUL", target.ToString() + ", " + source.ToString() + ", " + string.Format("0x{0:x}", value), null, source, target, new UInt32[] {value}, new string[] {"o32", "69", "/r", "id"}));
		}
		
		// IN AL,imm8
		public void IN_AL (Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IN_AL", "AL" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"E4", "ib"}));
		}
		
		// IN AX,imm8
		public void IN_AX (Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IN_AX", "AX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o16", "E5", "ib"}));
		}
		
		// IN EAX,imm8
		public void IN_EAX (Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IN_EAX", "EAX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o32", "E5", "ib"}));
		}
		
		// IN AL,DX
		public void IN_AL__DX ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IN_AL__DX", "AL" + ", " + "DX", null, null, null, null, new string[] {"EC"}));
		}
		
		// IN AX,DX
		public void IN_AX__DX ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IN_AX__DX", "AX" + ", " + "DX", null, null, null, null, new string[] {"o16", "ED"}));
		}
		
		// IN EAX,DX
		public void IN_EAX__DX ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IN_EAX__DX", "EAX" + ", " + "DX", null, null, null, null, new string[] {"o32", "ED"}));
		}
		
		// INC reg16
		public void INC (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "INC", target.ToString(), null, null, target, null, new string[] {"o16", "40+r"}));
		}
		
		// INC reg32
		public void INC (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "INC", target.ToString(), null, null, target, null, new string[] {"o32", "40+r"}));
		}
		
		// INC mem8
		public void INC (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "INC", target.ToString(), target, null, null, null, new string[] {"FE", "/0"}));
		}
		
		// INC mem16
		public void INC (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "INC", target.ToString(), target, null, null, null, new string[] {"o16", "FF", "/0"}));
		}
		
		// INC mem32
		public void INC (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "INC", target.ToString(), target, null, null, null, new string[] {"o32", "FF", "/0"}));
		}
		
		// INC rmreg8
		public void INC (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "INC", target.ToString(), null, target, null, null, new string[] {"FE", "/0"}));
		}
		
		// INSB 
		public void INSB ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "INSB", "", null, null, null, null, new string[] {"6C"}));
		}
		
		// INSD 
		public void INSD ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "INSD", "", null, null, null, null, new string[] {"o32", "6D"}));
		}
		
		// INSW 
		public void INSW ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "INSW", "", null, null, null, null, new string[] {"o16", "6D"}));
		}
		
		// INT imm8
		public void INT (Byte target)
		{
			if (target == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "INT_1", "1", null, null, null, null, new string[] {"F1"}));
			}
			else if (target == 3)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "INT_3", "3", null, null, null, null, new string[] {"CC"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "INT", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"CD", "ib"}));
			}
		}
		
		// INTO 
		public void INTO ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "INTO", "", null, null, null, null, new string[] {"CE"}));
		}
		
		// INVD 
		public void INVD ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "INVD", "", null, null, null, null, new string[] {"0F", "08"}));
		}
		
		// INVLPG mem
		public void INVLPG (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "INVLPG", target.ToString(), target, null, null, null, new string[] {"0F", "01", "/7"}));
		}
		
		// IRET 
		public void IRET ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IRET", "", null, null, null, null, new string[] {"CF"}));
		}
		
		// IRETD 
		public void IRETD ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IRETD", "", null, null, null, null, new string[] {"o32", "CF"}));
		}
		
		// IRETW 
		public void IRETW ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "IRETW", "", null, null, null, null, new string[] {"o16", "CF"}));
		}
		
		// JA imm8
		public void JA (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JA", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"77", "rb"}));
		}
		
		// JA NEAR imm
		public void JA (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JA", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "87", "rw/rd"}));
		}
		
		public void JA (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JA", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "87", "rw/rd"}));
		}
		
		// JAE imm8
		public void JAE (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JAE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"73", "rb"}));
		}
		
		// JAE NEAR imm
		public void JAE (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JAE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "83", "rw/rd"}));
		}
		
		public void JAE (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JAE", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "83", "rw/rd"}));
		}
		
		// JB imm8
		public void JB (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JB", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"72", "rb"}));
		}
		
		// JB NEAR imm
		public void JB (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JB", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "82", "rw/rd"}));
		}
		
		public void JB (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JB", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "82", "rw/rd"}));
		}
		
		// JBE imm8
		public void JBE (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JBE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"76", "rb"}));
		}
		
		// JBE NEAR imm
		public void JBE (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JBE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "86", "rw/rd"}));
		}
		
		public void JBE (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JBE", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "86", "rw/rd"}));
		}
		
		// JC imm8
		public void JC (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JC", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"72", "rb"}));
		}
		
		// JC NEAR imm
		public void JC (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JC", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "82", "rw/rd"}));
		}
		
		public void JC (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JC", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "82", "rw/rd"}));
		}
		
		// JCXZ imm8
		public void JCXZ (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JCXZ", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"a16", "E3", "rb"}));
		}
		
		// JE imm8
		public void JE (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"74", "rb"}));
		}
		
		// JE NEAR imm
		public void JE (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "84", "rw/rd"}));
		}
		
		public void JE (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JE", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "84", "rw/rd"}));
		}
		
		// JECXZ imm8
		public void JECXZ (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JECXZ", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"a32", "E3", "rb"}));
		}
		
		// JG imm8
		public void JG (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JG", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"7F", "rb"}));
		}
		
		// JG NEAR imm
		public void JG (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JG", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "8F", "rw/rd"}));
		}
		
		public void JG (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JG", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "8F", "rw/rd"}));
		}
		
		// JGE imm8
		public void JGE (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JGE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"7D", "rb"}));
		}
		
		// JGE NEAR imm
		public void JGE (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JGE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "8D", "rw/rd"}));
		}
		
		public void JGE (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JGE", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "8D", "rw/rd"}));
		}
		
		// JL imm8
		public void JL (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JL", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"7C", "rb"}));
		}
		
		// JL NEAR imm
		public void JL (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JL", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "8C", "rw/rd"}));
		}
		
		public void JL (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JL", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "8C", "rw/rd"}));
		}
		
		// JLE imm8
		public void JLE (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JLE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"7E", "rb"}));
		}
		
		// JLE NEAR imm
		public void JLE (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JLE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "8E", "rw/rd"}));
		}
		
		public void JLE (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JLE", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "8E", "rw/rd"}));
		}
		
		// JMP imm
		public void JMP (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JMP", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"E9", "rw/rd"}));
		}
		
		public void JMP (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JMP", label, null, null, null, new UInt32[] {0}, new string[] {"E9", "rw/rd"}));
		}
		
		// JMP imm8
		public void JMP (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JMP", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"EB", "rb"}));
		}
		
		// JMP imm16:imm16
		public void JMP (UInt16 target, UInt16 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JMP", string.Format("0x{0:x}", target) + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source, target}, new string[] {"o16", "EA", "iw", "iw"}));
		}
		
		// JMP imm16:imm32
		public void JMP (UInt16 target, UInt32 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JMP", string.Format("0x{0:x}", target) + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source, target}, new string[] {"o32", "EA", "id", "iw"}));
		}
		
		// JMP FAR mem
		public void JMP_FAR (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JMP_FAR", target.ToString(), target, null, null, null, new string[] {"o16", "FF", "/5"}));
		}
		
		// JMP FAR mem32
		public void JMP_FAR (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JMP_FAR", target.ToString(), target, null, null, null, new string[] {"o32", "FF", "/5"}));
		}
		
		// JMP mem16
		public void JMP (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JMP", target.ToString(), target, null, null, null, new string[] {"o16", "FF", "/4"}));
		}
		
		// JMP mem32
		public void JMP (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JMP", target.ToString(), target, null, null, null, new string[] {"o32", "FF", "/4"}));
		}
		
		// JMP rmreg16
		public void JMP (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JMP", target.ToString(), null, target, null, null, new string[] {"o16", "FF", "/4"}));
		}
		
		// JMP rmreg32
		public void JMP (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JMP", target.ToString(), null, target, null, null, new string[] {"o32", "FF", "/4"}));
		}
		
		// JNA imm8
		public void JNA (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNA", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"76", "rb"}));
		}
		
		// JNA NEAR imm
		public void JNA (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNA", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "86", "rw/rd"}));
		}
		
		public void JNA (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JNA", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "86", "rw/rd"}));
		}
		
		// JNAE imm8
		public void JNAE (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNAE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"72", "rb"}));
		}
		
		// JNAE NEAR imm
		public void JNAE (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNAE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "82", "rw/rd"}));
		}
		
		public void JNAE (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JNAE", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "82", "rw/rd"}));
		}
		
		// JNB imm8
		public void JNB (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNB", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"73", "rb"}));
		}
		
		// JNB NEAR imm
		public void JNB (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNB", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "83", "rw/rd"}));
		}
		
		public void JNB (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JNB", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "83", "rw/rd"}));
		}
		
		// JNBE imm8
		public void JNBE (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNBE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"77", "rb"}));
		}
		
		// JNBE NEAR imm
		public void JNBE (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNBE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "87", "rw/rd"}));
		}
		
		public void JNBE (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JNBE", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "87", "rw/rd"}));
		}
		
		// JNC imm8
		public void JNC (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNC", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"73", "rb"}));
		}
		
		// JNC NEAR imm
		public void JNC (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNC", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "83", "rw/rd"}));
		}
		
		public void JNC (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JNC", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "83", "rw/rd"}));
		}
		
		// JNE imm8
		public void JNE (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"75", "rb"}));
		}
		
		// JNE NEAR imm
		public void JNE (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "85", "rw/rd"}));
		}
		
		public void JNE (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JNE", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "85", "rw/rd"}));
		}
		
		// JNG imm8
		public void JNG (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNG", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"7E", "rb"}));
		}
		
		// JNG NEAR imm
		public void JNG (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNG", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "8E", "rw/rd"}));
		}
		
		public void JNG (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JNG", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "8E", "rw/rd"}));
		}
		
		// JNGE imm8
		public void JNGE (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNGE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"7C", "rb"}));
		}
		
		// JNGE NEAR imm
		public void JNGE (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNGE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "8C", "rw/rd"}));
		}
		
		public void JNGE (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JNGE", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "8C", "rw/rd"}));
		}
		
		// JNL imm8
		public void JNL (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNL", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"7D", "rb"}));
		}
		
		// JNL NEAR imm
		public void JNL (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNL", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "8D", "rw/rd"}));
		}
		
		public void JNL (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JNL", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "8D", "rw/rd"}));
		}
		
		// JNLE imm8
		public void JNLE (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNLE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"7F", "rb"}));
		}
		
		// JNLE NEAR imm
		public void JNLE (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNLE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "8F", "rw/rd"}));
		}
		
		public void JNLE (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JNLE", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "8F", "rw/rd"}));
		}
		
		// JNO imm8
		public void JNO (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNO", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"71", "rb"}));
		}
		
		// JNO NEAR imm
		public void JNO (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNO", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "81", "rw/rd"}));
		}
		
		public void JNO (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JNO", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "81", "rw/rd"}));
		}
		
		// JNP imm8
		public void JNP (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNP", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"7B", "rb"}));
		}
		
		// JNP NEAR imm
		public void JNP (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNP", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "8B", "rw/rd"}));
		}
		
		public void JNP (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JNP", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "8B", "rw/rd"}));
		}
		
		// JNS imm8
		public void JNS (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNS", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"79", "rb"}));
		}
		
		// JNS NEAR imm
		public void JNS (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNS", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "89", "rw/rd"}));
		}
		
		public void JNS (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JNS", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "89", "rw/rd"}));
		}
		
		// JNZ imm8
		public void JNZ (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNZ", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"75", "rb"}));
		}
		
		// JNZ NEAR imm
		public void JNZ (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JNZ", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "85", "rw/rd"}));
		}
		
		public void JNZ (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JNZ", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "85", "rw/rd"}));
		}
		
		// JO imm8
		public void JO (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JO", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"70", "rb"}));
		}
		
		// JO NEAR imm
		public void JO (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JO", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "80", "rw/rd"}));
		}
		
		public void JO (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JO", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "80", "rw/rd"}));
		}
		
		// JP imm8
		public void JP (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JP", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"7A", "rb"}));
		}
		
		// JP NEAR imm
		public void JP (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JP", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "8A", "rw/rd"}));
		}
		
		public void JP (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JP", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "8A", "rw/rd"}));
		}
		
		// JPE imm8
		public void JPE (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JPE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"7A", "rb"}));
		}
		
		// JPE NEAR imm
		public void JPE (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JPE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "8A", "rw/rd"}));
		}
		
		public void JPE (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JPE", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "8A", "rw/rd"}));
		}
		
		// JPO imm8
		public void JPO (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JPO", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"7B", "rb"}));
		}
		
		// JPO NEAR imm
		public void JPO (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JPO", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "8B", "rw/rd"}));
		}
		
		public void JPO (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JPO", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "8B", "rw/rd"}));
		}
		
		// JS imm8
		public void JS (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JS", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"78", "rb"}));
		}
		
		// JS NEAR imm
		public void JS (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JS", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "88", "rw/rd"}));
		}
		
		public void JS (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JS", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "88", "rw/rd"}));
		}
		
		// JZ imm8
		public void JZ (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JZ", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"74", "rb"}));
		}
		
		// JZ NEAR imm
		public void JZ (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "JZ", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"0F", "84", "rw/rd"}));
		}
		
		public void JZ (string label)
		{
			this.instructions.Add(new Instruction(true, string.Empty, label, "JZ", label, null, null, null, new UInt32[] {0}, new string[] {"0F", "84", "rw/rd"}));
		}
		
		// LAHF 
		public void LAHF ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LAHF", "", null, null, null, null, new string[] {"9F"}));
		}
		
		// LAR reg16,mem16
		public void LAR (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LAR", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "02", "/r"}));
		}
		
		// LAR reg32,mem32
		public void LAR (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LAR", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "02", "/r"}));
		}
		
		// LAR reg16,rmreg16
		public void LAR (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LAR", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "02", "/r"}));
		}
		
		// LAR reg32,rmreg32
		public void LAR (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LAR", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "02", "/r"}));
		}
		
		// LDS reg16,mem
		public void LDS (R16Type target, Memory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LDS", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "C5", "/r"}));
		}
		
		// LDS reg32,mem
		public void LDS (R32Type target, Memory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LDS", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "C5", "/r"}));
		}
		
		// LEA reg16,mem
		public void LEA (R16Type target, Memory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LEA", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "8D", "/r"}));
		}
		
		// LEA reg32,mem
		public void LEA (R32Type target, Memory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LEA", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "8D", "/r"}));
		}
		
		// LEAVE 
		public void LEAVE ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LEAVE", "", null, null, null, null, new string[] {"C9"}));
		}
		
		// LES reg16,mem
		public void LES (R16Type target, Memory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LES", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "C4", "/r"}));
		}
		
		// LES reg32,mem
		public void LES (R32Type target, Memory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LES", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "C4", "/r"}));
		}
		
		// LFENCE 
		public void LFENCE ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LFENCE", "", null, null, null, null, new string[] {"0F", "AE", "/5"}));
		}
		
		// LFS reg16,mem
		public void LFS (R16Type target, Memory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LFS", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "B4", "/r"}));
		}
		
		// LFS reg32,mem
		public void LFS (R32Type target, Memory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LFS", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "B4", "/r"}));
		}
		
		// LGDT mem
		public void LGDT (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LGDT", target.ToString(), target, null, null, null, new string[] {"0F", "01", "/2"}));
		}
		
		// LGS reg16,mem
		public void LGS (R16Type target, Memory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LGS", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "B5", "/r"}));
		}
		
		// LGS reg32,mem
		public void LGS (R32Type target, Memory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LGS", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "B5", "/r"}));
		}
		
		// LIDT mem
		public void LIDT (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LIDT", target.ToString(), target, null, null, null, new string[] {"0F", "01", "/3"}));
		}
		
		// LLDT mem16
		public void LLDT (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LLDT", target.ToString(), target, null, null, null, new string[] {"0F", "00", "/2"}));
		}
		
		// LLDT rmreg16
		public void LLDT (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LLDT", target.ToString(), null, target, null, null, new string[] {"0F", "00", "/2"}));
		}
		
		// LMSW mem16
		public void LMSW (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LMSW", target.ToString(), target, null, null, null, new string[] {"0F", "01", "/6"}));
		}
		
		// LMSW rmreg16
		public void LMSW (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LMSW", target.ToString(), null, target, null, null, new string[] {"0F", "01", "/6"}));
		}
		
		// LODSB 
		public void LODSB ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LODSB", "", null, null, null, null, new string[] {"AC"}));
		}
		
		// LODSD 
		public void LODSD ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LODSD", "", null, null, null, null, new string[] {"o32", "AD"}));
		}
		
		// LODSW 
		public void LODSW ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LODSW", "", null, null, null, null, new string[] {"o16", "AD"}));
		}
		
		// LOOP imm8
		public void LOOP (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LOOP", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"E2", "rb"}));
		}
		
		// LOOPE imm8
		public void LOOPE (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LOOPE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"E1", "rb"}));
		}
		
		// LOOPNE imm8
		public void LOOPNE (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LOOPNE", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"E0", "rb"}));
		}
		
		// LOOPNZ imm8
		public void LOOPNZ (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LOOPNZ", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"E0", "rb"}));
		}
		
		// LOOPZ imm8
		public void LOOPZ (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LOOPZ", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"E1", "rb"}));
		}
		
		// LSL reg16,mem16
		public void LSL (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LSL", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "03", "/r"}));
		}
		
		// LSL reg32,mem32
		public void LSL (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LSL", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "03", "/r"}));
		}
		
		// LSL reg16,rmreg16
		public void LSL (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LSL", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "03", "/r"}));
		}
		
		// LSL reg32,rmreg32
		public void LSL (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LSL", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "03", "/r"}));
		}
		
		// LSS reg16,mem
		public void LSS (R16Type target, Memory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LSS", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "B2", "/r"}));
		}
		
		// LSS reg32,mem
		public void LSS (R32Type target, Memory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LSS", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "B2", "/r"}));
		}
		
		// LTR mem16
		public void LTR (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LTR", target.ToString(), target, null, null, null, new string[] {"0F", "00", "/3"}));
		}
		
		// LTR rmreg16
		public void LTR (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "LTR", target.ToString(), null, target, null, null, new string[] {"0F", "00", "/3"}));
		}
		
		// MFENCE 
		public void MFENCE ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MFENCE", "", null, null, null, null, new string[] {"0F", "AE", "/6"}));
		}
		
		// MOV mem8,reg8
		public void MOV (ByteMemory target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"88", "/r"}));
		}
		
		// MOV mem16,reg16
		public void MOV (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o16", "89", "/r"}));
		}
		
		// MOV mem32,reg32
		public void MOV (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o32", "89", "/r"}));
		}
		
		// MOV reg8,mem8
		public void MOV (R8Type target, ByteMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"8A", "/r"}));
		}
		
		// MOV reg16,mem16
		public void MOV (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "8B", "/r"}));
		}
		
		// MOV reg32,mem32
		public void MOV (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "8B", "/r"}));
		}
		
		// MOV reg8,imm8
		public void MOV (R8Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + string.Format("0x{0:x}", source), null, null, target, new UInt32[] {source}, new string[] {"B0+r", "ib"}));
		}
		
		// MOV reg16,imm16
		public void MOV (R16Type target, UInt16 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + string.Format("0x{0:x}", source), null, null, target, new UInt32[] {source}, new string[] {"o16", "B8+r", "iw"}));
		}
		
		// MOV reg32,imm32
		public void MOV (R32Type target, UInt32 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + string.Format("0x{0:x}", source), null, null, target, new UInt32[] {source}, new string[] {"o32", "B8+r", "id"}));
		}
		
		// MOV mem8,imm8
		public void MOV (ByteMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"C6", "/0", "ib"}));
		}
		
		// MOV mem16,imm16
		public void MOV (WordMemory target, UInt16 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "C7", "/0", "iw"}));
		}
		
		// MOV mem32,imm32
		public void MOV (DWordMemory target, UInt32 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "C7", "/0", "id"}));
		}
		
		// MOV AL,memoffs8
		public void MOV_AL (byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV_AL", "AL" + ", " + source.ToString(), null, null, null, new UInt32[] {source}, new string[] {"A0", "ow/od"}));
		}
		
		// MOV AX,memoffs16
		public void MOV_AX (UInt16 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV_AX", "AX" + ", " + source.ToString(), null, null, null, new UInt32[] {source}, new string[] {"o16", "A1", "ow/od"}));
		}
		
		// MOV EAX,memoffs32
		public void MOV_EAX (UInt32 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV_EAX", "EAX" + ", " + source.ToString(), null, null, null, new UInt32[] {source}, new string[] {"o32", "A1", "ow/od"}));
		}
		
		// MOV memoffs8,AL
		public void MOV__AL (byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV__AL", target.ToString() + ", " + "AL", null, null, null, new UInt32[] {target}, new string[] {"A2", "ow/od"}));
		}
		
		// MOV memoffs16,AX
		public void MOV__AX (UInt16 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV__AX", target.ToString() + ", " + "AX", null, null, null, new UInt32[] {target}, new string[] {"o16", "A3", "ow/od"}));
		}
		
		// MOV memoffs32,EAX
		public void MOV__EAX (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV__EAX", target.ToString() + ", " + "EAX", null, null, null, new UInt32[] {target}, new string[] {"o32", "A3", "ow/od"}));
		}
		
		// MOV mem16,segreg
		public void MOV (WordMemory target, SegType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"8C", "/r"}));
		}
		
		// MOV mem32,segreg
		public void MOV (DWordMemory target, SegType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o32", "8C", "/r"}));
		}
		
		// MOV segreg,mem16
		public void MOV (SegType target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"8E", "/r"}));
		}
		
		// MOV segreg,mem32
		public void MOV (SegType target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "8E", "/r"}));
		}
		
		// MOV reg32,CR0/2/3/4
		public void MOV (R32Type target, CRType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"0F", "20", "/r"}));
		}
		
		// MOV reg32,DR0/1/2/3/6/7
		public void MOV (R32Type target, DRType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"0F", "21", "/r"}));
		}
		
		// MOV reg32,TR3/4/5/6/7
		public void MOV (R32Type target, TRType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"0F", "24", "/r"}));
		}
		
		// MOV CR0/2/3/4,reg32
		public void MOV (CRType target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"0F", "22", "/r"}));
		}
		
		// MOV DR0/1/2/3/6/7,reg32
		public void MOV (DRType target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"0F", "23", "/r"}));
		}
		
		// MOV TR3/4/5/6/7,reg32
		public void MOV (TRType target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"0F", "26", "/r"}));
		}
		
		// MOV rmreg8,reg8
		public void MOV (R8Type target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"88", "/r"}));
		}
		
		// MOV rmreg16,reg16
		public void MOV (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o16", "89", "/r"}));
		}
		
		// MOV rmreg32,reg32
		public void MOV (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o32", "89", "/r"}));
		}
		
		// MOV rmreg16,segreg
		public void MOV (R16Type target, SegType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o16", "8C", "/r"}));
		}
		
		// MOV rmreg32,segreg
		public void MOV (R32Type target, SegType source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o32", "8C", "/r"}));
		}
		
		// MOV segreg,rmreg16
		public void MOV (SegType target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"8E", "/r"}));
		}
		
		// MOV segreg,rmreg32
		public void MOV (SegType target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOV", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "8E", "/r"}));
		}
		
		// MOVSB 
		public void MOVSB ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOVSB", "", null, null, null, null, new string[] {"A4"}));
		}
		
		// MOVSD 
		public void MOVSD ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOVSD", "", null, null, null, null, new string[] {"o32", "A5"}));
		}
		
		// MOVSW 
		public void MOVSW ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOVSW", "", null, null, null, null, new string[] {"o16", "A5"}));
		}
		
		// MOVSX reg16,mem8
		public void MOVSX (R16Type target, ByteMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOVSX", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "BE", "/r"}));
		}
		
		// MOVSX reg32,mem8
		public void MOVSX (R32Type target, ByteMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOVSX", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "BE", "/r"}));
		}
		
		// MOVSX reg32,mem16
		public void MOVSX (R32Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOVSX", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "BF", "/r"}));
		}
		
		// MOVSX reg16,rmreg8
		public void MOVSX (R16Type target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOVSX", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "BE", "/r"}));
		}
		
		// MOVSX reg32,rmreg8
		public void MOVSX (R32Type target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOVSX", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "BE", "/r"}));
		}
		
		// MOVSX reg32,rmreg16
		public void MOVSX (R32Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOVSX", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "BF", "/r"}));
		}
		
		// MOVZX reg16,mem8
		public void MOVZX (R16Type target, ByteMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOVZX", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0F", "B6", "/r"}));
		}
		
		// MOVZX reg32,mem8
		public void MOVZX (R32Type target, ByteMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOVZX", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "B6", "/r"}));
		}
		
		// MOVZX reg32,mem16
		public void MOVZX (R32Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOVZX", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0F", "B7", "/r"}));
		}
		
		// MOVZX reg16,rmreg8
		public void MOVZX (R16Type target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOVZX", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "0F", "B6", "/r"}));
		}
		
		// MOVZX reg32,rmreg8
		public void MOVZX (R32Type target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOVZX", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "B6", "/r"}));
		}
		
		// MOVZX reg32,rmreg16
		public void MOVZX (R32Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MOVZX", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "0F", "B7", "/r"}));
		}
		
		// MUL mem8
		public void MUL (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MUL", target.ToString(), target, null, null, null, new string[] {"F6", "/4"}));
		}
		
		// MUL mem16
		public void MUL (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MUL", target.ToString(), target, null, null, null, new string[] {"o16", "F7", "/4"}));
		}
		
		// MUL mem32
		public void MUL (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MUL", target.ToString(), target, null, null, null, new string[] {"o32", "F7", "/4"}));
		}
		
		// MUL rmreg8
		public void MUL (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MUL", target.ToString(), null, target, null, null, new string[] {"F6", "/4"}));
		}
		
		// MUL rmreg16
		public void MUL (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MUL", target.ToString(), null, target, null, null, new string[] {"o16", "F7", "/4"}));
		}
		
		// MUL rmreg32
		public void MUL (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "MUL", target.ToString(), null, target, null, null, new string[] {"o32", "F7", "/4"}));
		}
		
		// NEG mem8
		public void NEG (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "NEG", target.ToString(), target, null, null, null, new string[] {"F6", "/3"}));
		}
		
		// NEG mem16
		public void NEG (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "NEG", target.ToString(), target, null, null, null, new string[] {"o16", "F7", "/3"}));
		}
		
		// NEG mem32
		public void NEG (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "NEG", target.ToString(), target, null, null, null, new string[] {"o32", "F7", "/3"}));
		}
		
		// NEG rmreg8
		public void NEG (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "NEG", target.ToString(), null, target, null, null, new string[] {"F6", "/3"}));
		}
		
		// NEG rmreg16
		public void NEG (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "NEG", target.ToString(), null, target, null, null, new string[] {"o16", "F7", "/3"}));
		}
		
		// NEG rmreg32
		public void NEG (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "NEG", target.ToString(), null, target, null, null, new string[] {"o32", "F7", "/3"}));
		}
		
		// NOP 
		public void NOP ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "NOP", "", null, null, null, null, new string[] {"90"}));
		}
		
		// NOT mem8
		public void NOT (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "NOT", target.ToString(), target, null, null, null, new string[] {"F6", "/2"}));
		}
		
		// NOT mem16
		public void NOT (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "NOT", target.ToString(), target, null, null, null, new string[] {"o16", "F7", "/2"}));
		}
		
		// NOT mem32
		public void NOT (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "NOT", target.ToString(), target, null, null, null, new string[] {"o32", "F7", "/2"}));
		}
		
		// NOT rmreg8
		public void NOT (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "NOT", target.ToString(), null, target, null, null, new string[] {"F6", "/2"}));
		}
		
		// NOT rmreg16
		public void NOT (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "NOT", target.ToString(), null, target, null, null, new string[] {"o16", "F7", "/2"}));
		}
		
		// NOT rmreg32
		public void NOT (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "NOT", target.ToString(), null, target, null, null, new string[] {"o32", "F7", "/2"}));
		}
		
		// OR mem8,reg8
		public void OR (ByteMemory target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"08", "/r"}));
		}
		
		// OR mem16,reg16
		public void OR (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o16", "09", "/r"}));
		}
		
		// OR mem32,reg32
		public void OR (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o32", "09", "/r"}));
		}
		
		// OR reg8,mem8
		public void OR (R8Type target, ByteMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"0A", "/r"}));
		}
		
		// OR reg16,mem16
		public void OR (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "0B", "/r"}));
		}
		
		// OR reg32,mem32
		public void OR (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "0B", "/r"}));
		}
		
		// OR mem8,imm8
		public void OR (ByteMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"80", "/1", "ib"}));
		}
		
		// OR mem16,imm16
		public void OR (WordMemory target, UInt16 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "81", "/1", "iw"}));
		}
		
		// OR mem32,imm32
		public void OR (DWordMemory target, UInt32 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "81", "/1", "id"}));
		}
		
		// OR mem16,imm8
		public void OR (WordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "83", "/1", "ib"}));
		}
		
		// OR mem32,imm8
		public void OR (DWordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "83", "/1", "ib"}));
		}
		
		// OR rmreg8,reg8
		public void OR (R8Type target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"08", "/r"}));
		}
		
		// OR rmreg16,reg16
		public void OR (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o16", "09", "/r"}));
		}
		
		// OR rmreg32,reg32
		public void OR (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o32", "09", "/r"}));
		}
		
		// OR rmreg8,imm8
		public void OR (R8Type target, Byte source)
		{
			if (target == R8.AL)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR_AL", "AL" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"0C", "ib"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"80", "/1", "ib"}));
			}
		}
		
		// OR rmreg16,imm16
		public void OR (R16Type target, UInt16 source)
		{
			if (target == R16.AX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR_AX", "AX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o16", "0D", "iw"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "81", "/1", "iw"}));
			}
		}
		
		// OR rmreg32,imm32
		public void OR (R32Type target, UInt32 source)
		{
			if (target == R32.EAX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR_EAX", "EAX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o32", "0D", "id"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "81", "/1", "id"}));
			}
		}
		
		// OR rmreg16,imm8
		public void OR (R16Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "83", "/1", "ib"}));
		}
		
		// OR rmreg32,imm8
		public void OR (R32Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "83", "/1", "ib"}));
		}
		
		// OUT imm8,AL
		public void OUT__AL (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OUT__AL", string.Format("0x{0:x}", target) + ", " + "AL", null, null, null, new UInt32[] {target}, new string[] {"E6", "ib"}));
		}
		
		// OUT imm8,AX
		public void OUT__AX (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OUT__AX", string.Format("0x{0:x}", target) + ", " + "AX", null, null, null, new UInt32[] {target}, new string[] {"o16", "E7", "ib"}));
		}
		
		// OUT imm8,EAX
		public void OUT__EAX (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OUT__EAX", string.Format("0x{0:x}", target) + ", " + "EAX", null, null, null, new UInt32[] {target}, new string[] {"o32", "E7", "ib"}));
		}
		
		// OUT DX,AL
		public void OUT_DX__AL ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OUT_DX__AL", "DX" + ", " + "AL", null, null, null, null, new string[] {"EE"}));
		}
		
		// OUT DX,AX
		public void OUT_DX__AX ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OUT_DX__AX", "DX" + ", " + "AX", null, null, null, null, new string[] {"o16", "EF"}));
		}
		
		// OUT DX,EAX
		public void OUT_DX__EAX ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OUT_DX__EAX", "DX" + ", " + "EAX", null, null, null, null, new string[] {"o32", "EF"}));
		}
		
		// OUTSB 
		public void OUTSB ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OUTSB", "", null, null, null, null, new string[] {"6E"}));
		}
		
		// OUTSD 
		public void OUTSD ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OUTSD", "", null, null, null, null, new string[] {"o32", "6F"}));
		}
		
		// OUTSW 
		public void OUTSW ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "OUTSW", "", null, null, null, null, new string[] {"o16", "6F"}));
		}
		
		// PAUSE 
		public void PAUSE ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PAUSE", "", null, null, null, null, new string[] {"F3", "90"}));
		}
		
		// POP reg16
		public void POP (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "POP", target.ToString(), null, null, target, null, new string[] {"o16", "58+r"}));
		}
		
		// POP reg32
		public void POP (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "POP", target.ToString(), null, null, target, null, new string[] {"o32", "58+r"}));
		}
		
		// POP mem16
		public void POP (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "POP", target.ToString(), target, null, null, null, new string[] {"o16", "8F", "/0"}));
		}
		
		// POP mem32
		public void POP (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "POP", target.ToString(), target, null, null, null, new string[] {"o32", "8F", "/0"}));
		}
		
		// POP segreg
		public void POP (SegType target)
		{
			if (target == Seg.GS)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "POP_GS", "GS", null, null, null, null, new string[] {"0F", "A9"}));
			}
			else if (target == Seg.FS)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "POP_FS", "FS", null, null, null, null, new string[] {"0F", "A1"}));
			}
			else if (target == Seg.ES)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "POP_ES", "ES", null, null, null, null, new string[] {"07"}));
			}
			else if (target == Seg.DS)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "POP_DS", "DS", null, null, null, null, new string[] {"1F"}));
			}
			else if (target == Seg.SS)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "POP_SS", "SS", null, null, null, null, new string[] {"17"}));
			}
			else
			{
				throw new Exception("Parameters not supported.");
			}
		}
		
		// POPA 
		public void POPA ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "POPA", "", null, null, null, null, new string[] {"61"}));
		}
		
		// POPAD 
		public void POPAD ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "POPAD", "", null, null, null, null, new string[] {"o32", "61"}));
		}
		
		// POPAW 
		public void POPAW ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "POPAW", "", null, null, null, null, new string[] {"o16", "61"}));
		}
		
		// POPF 
		public void POPF ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "POPF", "", null, null, null, null, new string[] {"9D"}));
		}
		
		// POPFD 
		public void POPFD ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "POPFD", "", null, null, null, null, new string[] {"o32", "9D"}));
		}
		
		// POPFW 
		public void POPFW ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "POPFW", "", null, null, null, null, new string[] {"o16", "9D"}));
		}
		
		// PREFETCHNTA m8
		public void PREFETCHNTA (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PREFETCHNTA", target.ToString(), target, null, null, null, new string[] {"0F", "18", "/0"}));
		}
		
		// PREFETCHT0 m8
		public void PREFETCHT0 (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PREFETCHT0", target.ToString(), target, null, null, null, new string[] {"0F", "18", "/1"}));
		}
		
		// PREFETCHT1 m8
		public void PREFETCHT1 (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PREFETCHT1", target.ToString(), target, null, null, null, new string[] {"0F", "18", "/2"}));
		}
		
		// PREFETCHT2 m8
		public void PREFETCHT2 (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PREFETCHT2", target.ToString(), target, null, null, null, new string[] {"0F", "18", "/3"}));
		}
		
		// PUSH reg16
		public void PUSH (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSH", target.ToString(), null, null, target, null, new string[] {"o16", "50+r"}));
		}
		
		// PUSH reg32
		public void PUSH (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSH", target.ToString(), null, null, target, null, new string[] {"o32", "50+r"}));
		}
		
		// PUSH mem16
		public void PUSH (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSH", target.ToString(), target, null, null, null, new string[] {"o16", "FF", "/6"}));
		}
		
		// PUSH mem32
		public void PUSH (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSH", target.ToString(), target, null, null, null, new string[] {"o32", "FF", "/6"}));
		}
		
		// PUSH imm8
		public void PUSH (Byte target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSH", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"6A", "ib"}));
		}
		
		// PUSH imm16
		public void PUSH (UInt16 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSH", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"o16", "68", "iw"}));
		}
		
		// PUSH imm32
		public void PUSH (UInt32 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSH", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"o32", "68", "id"}));
		}
		
		// PUSH segreg
		public void PUSH (SegType target)
		{
			if (target == Seg.CS)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSH_CS", "CS", null, null, null, null, new string[] {"0E"}));
			}
			else if (target == Seg.GS)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSH_GS", "GS", null, null, null, null, new string[] {"0F", "A8"}));
			}
			else if (target == Seg.ES)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSH_ES", "ES", null, null, null, null, new string[] {"06"}));
			}
			else if (target == Seg.DS)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSH_DS", "DS", null, null, null, null, new string[] {"1E"}));
			}
			else if (target == Seg.SS)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSH_SS", "SS", null, null, null, null, new string[] {"16"}));
			}
			else if (target == Seg.FS)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSH_FS", "FS", null, null, null, null, new string[] {"0F", "A0"}));
			}
			else
			{
				throw new Exception("Parameters not supported.");
			}
		}
		
		// PUSHA 
		public void PUSHA ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSHA", "", null, null, null, null, new string[] {"60"}));
		}
		
		// PUSHAD 
		public void PUSHAD ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSHAD", "", null, null, null, null, new string[] {"o32", "60"}));
		}
		
		// PUSHAW 
		public void PUSHAW ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSHAW", "", null, null, null, null, new string[] {"o16", "60"}));
		}
		
		// PUSHF 
		public void PUSHF ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSHF", "", null, null, null, null, new string[] {"9C"}));
		}
		
		// PUSHFD 
		public void PUSHFD ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSHFD", "", null, null, null, null, new string[] {"o32", "9C"}));
		}
		
		// PUSHFW 
		public void PUSHFW ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "PUSHFW", "", null, null, null, null, new string[] {"o16", "9C"}));
		}
		
		// RCL mem8,CL
		public void RCL__CL (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCL__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"D2", "/2"}));
		}
		
		// RCL mem8,imm8
		public void RCL (ByteMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCL__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"D0", "/2"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCL", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"C0", "/2", "ib"}));
			}
		}
		
		// RCL mem16,CL
		public void RCL__CL (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCL__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"o16", "D3", "/2"}));
		}
		
		// RCL mem16,imm8
		public void RCL (WordMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCL__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"o16", "D1", "/2"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCL", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "C1", "/2", "ib"}));
			}
		}
		
		// RCL mem32,CL
		public void RCL__CL (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCL__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"o32", "D3", "/2"}));
		}
		
		// RCL mem32,imm8
		public void RCL (DWordMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCL__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"o32", "D1", "/2"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCL", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "C1", "/2", "ib"}));
			}
		}
		
		// RCL rmreg8,CL
		public void RCL__CL (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCL__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"D2", "/2"}));
		}
		
		// RCL rmreg8,imm8
		public void RCL (R8Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCL__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"D0", "/2"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCL", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"C0", "/2", "ib"}));
			}
		}
		
		// RCL rmreg16,CL
		public void RCL__CL (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCL__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"o16", "D3", "/2"}));
		}
		
		// RCL rmreg16,imm8
		public void RCL (R16Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCL__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"o16", "D1", "/2"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCL", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "C1", "/2", "ib"}));
			}
		}
		
		// RCL rmreg32,CL
		public void RCL__CL (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCL__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"o32", "D3", "/2"}));
		}
		
		// RCL rmreg32,imm8
		public void RCL (R32Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCL__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"o32", "D1", "/2"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCL", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "C1", "/2", "ib"}));
			}
		}
		
		// RCR mem8,CL
		public void RCR__CL (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCR__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"D2", "/3"}));
		}
		
		// RCR mem8,imm8
		public void RCR (ByteMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCR__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"D0", "/3"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"C0", "/3", "ib"}));
			}
		}
		
		// RCR mem16,CL
		public void RCR__CL (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCR__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"o16", "D3", "/3"}));
		}
		
		// RCR mem16,imm8
		public void RCR (WordMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCR__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"o16", "D1", "/3"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "C1", "/3", "ib"}));
			}
		}
		
		// RCR mem32,CL
		public void RCR__CL (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCR__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"o32", "D3", "/3"}));
		}
		
		// RCR mem32,imm8
		public void RCR (DWordMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCR__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"o32", "D1", "/3"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "C1", "/3", "ib"}));
			}
		}
		
		// RCR rmreg8,CL
		public void RCR__CL (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCR__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"D2", "/3"}));
		}
		
		// RCR rmreg8,imm8
		public void RCR (R8Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCR__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"D0", "/3"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"C0", "/3", "ib"}));
			}
		}
		
		// RCR rmreg16,CL
		public void RCR__CL (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCR__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"o16", "D3", "/3"}));
		}
		
		// RCR rmreg16,imm8
		public void RCR (R16Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCR__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"o16", "D1", "/3"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "C1", "/3", "ib"}));
			}
		}
		
		// RCR rmreg32,CL
		public void RCR__CL (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCR__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"o32", "D3", "/3"}));
		}
		
		// RCR rmreg32,imm8
		public void RCR (R32Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCR__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"o32", "D1", "/3"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RCR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "C1", "/3", "ib"}));
			}
		}
		
		// RDMSR 
		public void RDMSR ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RDMSR", "", null, null, null, null, new string[] {"0F", "32"}));
		}
		
		// RDPMC 
		public void RDPMC ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RDPMC", "", null, null, null, null, new string[] {"0F", "33"}));
		}
		
		// RDTSC 
		public void RDTSC ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RDTSC", "", null, null, null, null, new string[] {"0F", "31"}));
		}
		
		// RET 
		public void RET ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RET", "", null, null, null, null, new string[] {"C3"}));
		}
		
		// RET imm16
		public void RET (UInt16 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RET", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"C2", "iw"}));
		}
		
		// RETF 
		public void RETF ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RETF", "", null, null, null, null, new string[] {"CB"}));
		}
		
		// RETF imm16
		public void RETF (UInt16 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RETF", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"CA", "iw"}));
		}
		
		// RETN 
		public void RETN ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RETN", "", null, null, null, null, new string[] {"C3"}));
		}
		
		// RETN imm16
		public void RETN (UInt16 target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RETN", string.Format("0x{0:x}", target), null, null, null, new UInt32[] {target}, new string[] {"C2", "iw"}));
		}
		
		// ROL mem8,CL
		public void ROL__CL (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROL__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"D2", "/0"}));
		}
		
		// ROL mem8,imm8
		public void ROL (ByteMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROL__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"D0", "/0"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROL", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"C0", "/0", "ib"}));
			}
		}
		
		// ROL mem16,CL
		public void ROL__CL (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROL__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"o16", "D3", "/0"}));
		}
		
		// ROL mem16,imm8
		public void ROL (WordMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROL__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"o16", "D1", "/0"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROL", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "C1", "/0", "ib"}));
			}
		}
		
		// ROL mem32,CL
		public void ROL__CL (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROL__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"o32", "D3", "/0"}));
		}
		
		// ROL mem32,imm8
		public void ROL (DWordMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROL__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"o32", "D1", "/0"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROL", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "C1", "/0", "ib"}));
			}
		}
		
		// ROL rmreg8,CL
		public void ROL__CL (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROL__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"D2", "/0"}));
		}
		
		// ROL rmreg8,imm8
		public void ROL (R8Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROL__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"D0", "/0"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROL", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"C0", "/0", "ib"}));
			}
		}
		
		// ROL rmreg16,CL
		public void ROL__CL (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROL__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"o16", "D3", "/0"}));
		}
		
		// ROL rmreg16,imm8
		public void ROL (R16Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROL__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"o16", "D1", "/0"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROL", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "C1", "/0", "ib"}));
			}
		}
		
		// ROL rmreg32,CL
		public void ROL__CL (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROL__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"o32", "D3", "/0"}));
		}
		
		// ROL rmreg32,imm8
		public void ROL (R32Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROL__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"o32", "D1", "/0"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROL", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "C1", "/0", "ib"}));
			}
		}
		
		// ROR mem8,CL
		public void ROR__CL (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROR__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"D2", "/1"}));
		}
		
		// ROR mem8,imm8
		public void ROR (ByteMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROR__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"D0", "/1"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"C0", "/1", "ib"}));
			}
		}
		
		// ROR mem16,CL
		public void ROR__CL (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROR__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"o16", "D3", "/1"}));
		}
		
		// ROR mem16,imm8
		public void ROR (WordMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROR__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"o16", "D1", "/1"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "C1", "/1", "ib"}));
			}
		}
		
		// ROR mem32,CL
		public void ROR__CL (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROR__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"o32", "D3", "/1"}));
		}
		
		// ROR mem32,imm8
		public void ROR (DWordMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROR__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"o32", "D1", "/1"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "C1", "/1", "ib"}));
			}
		}
		
		// ROR rmreg8,CL
		public void ROR__CL (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROR__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"D2", "/1"}));
		}
		
		// ROR rmreg8,imm8
		public void ROR (R8Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROR__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"D0", "/1"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"C0", "/1", "ib"}));
			}
		}
		
		// ROR rmreg16,CL
		public void ROR__CL (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROR__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"o16", "D3", "/1"}));
		}
		
		// ROR rmreg16,imm8
		public void ROR (R16Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROR__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"o16", "D1", "/1"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "C1", "/1", "ib"}));
			}
		}
		
		// ROR rmreg32,CL
		public void ROR__CL (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROR__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"o32", "D3", "/1"}));
		}
		
		// ROR rmreg32,imm8
		public void ROR (R32Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROR__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"o32", "D1", "/1"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "ROR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "C1", "/1", "ib"}));
			}
		}
		
		// RSM 
		public void RSM ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "RSM", "", null, null, null, null, new string[] {"0F", "AA"}));
		}
		
		// SAHF 
		public void SAHF ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAHF", "", null, null, null, null, new string[] {"9E"}));
		}
		
		// SAL mem8,CL
		public void SAL__CL (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAL__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"D2", "/4"}));
		}
		
		// SAL mem8,imm8
		public void SAL (ByteMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAL__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"D0", "/4"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAL", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"C0", "/4", "ib"}));
			}
		}
		
		// SAL mem16,CL
		public void SAL__CL (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAL__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"o16", "D3", "/4"}));
		}
		
		// SAL mem16,imm8
		public void SAL (WordMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAL__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"o16", "D1", "/4"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAL", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "C1", "/4", "ib"}));
			}
		}
		
		// SAL mem32,CL
		public void SAL__CL (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAL__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"o32", "D3", "/4"}));
		}
		
		// SAL mem32,imm8
		public void SAL (DWordMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAL__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"o32", "D1", "/4"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAL", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "C1", "/4", "ib"}));
			}
		}
		
		// SAL rmreg8,CL
		public void SAL__CL (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAL__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"D2", "/4"}));
		}
		
		// SAL rmreg8,imm8
		public void SAL (R8Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAL__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"D0", "/4"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAL", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"C0", "/4", "ib"}));
			}
		}
		
		// SAL rmreg16,CL
		public void SAL__CL (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAL__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"o16", "D3", "/4"}));
		}
		
		// SAL rmreg16,imm8
		public void SAL (R16Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAL__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"o16", "D1", "/4"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAL", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "C1", "/4", "ib"}));
			}
		}
		
		// SAL rmreg32,CL
		public void SAL__CL (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAL__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"o32", "D3", "/4"}));
		}
		
		// SAL rmreg32,imm8
		public void SAL (R32Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAL__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"o32", "D1", "/4"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAL", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "C1", "/4", "ib"}));
			}
		}
		
		// SALC 
		public void SALC ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SALC", "", null, null, null, null, new string[] {"D6"}));
		}
		
		// SAR mem8,CL
		public void SAR__CL (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAR__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"D2", "/7"}));
		}
		
		// SAR mem8,imm8
		public void SAR (ByteMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAR__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"D0", "/7"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"C0", "/7", "ib"}));
			}
		}
		
		// SAR mem16,CL
		public void SAR__CL (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAR__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"o16", "D3", "/7"}));
		}
		
		// SAR mem16,imm8
		public void SAR (WordMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAR__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"o16", "D1", "/7"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "C1", "/7", "ib"}));
			}
		}
		
		// SAR mem32,CL
		public void SAR__CL (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAR__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"o32", "D3", "/7"}));
		}
		
		// SAR mem32,imm8
		public void SAR (DWordMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAR__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"o32", "D1", "/7"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "C1", "/7", "ib"}));
			}
		}
		
		// SAR rmreg8,CL
		public void SAR__CL (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAR__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"D2", "/7"}));
		}
		
		// SAR rmreg8,imm8
		public void SAR (R8Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAR__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"D0", "/7"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"C0", "/7", "ib"}));
			}
		}
		
		// SAR rmreg16,CL
		public void SAR__CL (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAR__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"o16", "D3", "/7"}));
		}
		
		// SAR rmreg16,imm8
		public void SAR (R16Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAR__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"o16", "D1", "/7"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "C1", "/7", "ib"}));
			}
		}
		
		// SAR rmreg32,CL
		public void SAR__CL (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAR__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"o32", "D3", "/7"}));
		}
		
		// SAR rmreg32,imm8
		public void SAR (R32Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAR__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"o32", "D1", "/7"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SAR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "C1", "/7", "ib"}));
			}
		}
		
		// SBB mem8,reg8
		public void SBB (ByteMemory target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"18", "/r"}));
		}
		
		// SBB mem16,reg16
		public void SBB (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o16", "19", "/r"}));
		}
		
		// SBB mem32,reg32
		public void SBB (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o32", "19", "/r"}));
		}
		
		// SBB reg8,mem8
		public void SBB (R8Type target, ByteMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"1A", "/r"}));
		}
		
		// SBB reg16,mem16
		public void SBB (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "1B", "/r"}));
		}
		
		// SBB reg32,mem32
		public void SBB (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "1B", "/r"}));
		}
		
		// SBB mem8,imm8
		public void SBB (ByteMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"80", "/3", "ib"}));
		}
		
		// SBB mem16,imm16
		public void SBB (WordMemory target, UInt16 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "81", "/3", "iw"}));
		}
		
		// SBB mem32,imm32
		public void SBB (DWordMemory target, UInt32 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "81", "/3", "id"}));
		}
		
		// SBB mem16,imm8
		public void SBB (WordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "83", "/3", "ib"}));
		}
		
		// SBB mem32,imm8
		public void SBB (DWordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "83", "/3", "ib"}));
		}
		
		// SBB rmreg8,reg8
		public void SBB (R8Type target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"18", "/r"}));
		}
		
		// SBB rmreg16,reg16
		public void SBB (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o16", "19", "/r"}));
		}
		
		// SBB rmreg32,reg32
		public void SBB (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o32", "19", "/r"}));
		}
		
		// SBB rmreg8,imm8
		public void SBB (R8Type target, Byte source)
		{
			if (target == R8.AL)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB_AL", "AL" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"1C", "ib"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"80", "/3", "ib"}));
			}
		}
		
		// SBB rmreg16,imm16
		public void SBB (R16Type target, UInt16 source)
		{
			if (target == R16.AX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB_AX", "AX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o16", "1D", "iw"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "81", "/3", "iw"}));
			}
		}
		
		// SBB rmreg32,imm32
		public void SBB (R32Type target, UInt32 source)
		{
			if (target == R32.EAX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB_EAX", "EAX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o32", "1D", "id"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "81", "/3", "id"}));
			}
		}
		
		// SBB rmreg16,imm8
		public void SBB (R16Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "83", "/3", "ib"}));
		}
		
		// SBB rmreg32,imm8
		public void SBB (R32Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SBB", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "83", "/3", "ib"}));
		}
		
		// SCASB 
		public void SCASB ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SCASB", "", null, null, null, null, new string[] {"AE"}));
		}
		
		// SCASD 
		public void SCASD ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SCASD", "", null, null, null, null, new string[] {"o32", "AF"}));
		}
		
		// SCASW 
		public void SCASW ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SCASW", "", null, null, null, null, new string[] {"o16", "AF"}));
		}
		
		// SETA mem8
		public void SETA (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETA", target.ToString(), target, null, null, null, new string[] {"0F", "97", "/0"}));
		}
		
		// SETA rmreg8
		public void SETA (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETA", target.ToString(), null, target, null, null, new string[] {"0F", "97", "/0"}));
		}
		
		// SETAE mem8
		public void SETAE (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETAE", target.ToString(), target, null, null, null, new string[] {"0F", "93", "/0"}));
		}
		
		// SETAE rmreg8
		public void SETAE (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETAE", target.ToString(), null, target, null, null, new string[] {"0F", "93", "/0"}));
		}
		
		// SETB mem8
		public void SETB (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETB", target.ToString(), target, null, null, null, new string[] {"0F", "92", "/0"}));
		}
		
		// SETB rmreg8
		public void SETB (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETB", target.ToString(), null, target, null, null, new string[] {"0F", "92", "/0"}));
		}
		
		// SETBE mem8
		public void SETBE (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETBE", target.ToString(), target, null, null, null, new string[] {"0F", "96", "/0"}));
		}
		
		// SETBE rmreg8
		public void SETBE (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETBE", target.ToString(), null, target, null, null, new string[] {"0F", "96", "/0"}));
		}
		
		// SETC mem8
		public void SETC (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETC", target.ToString(), target, null, null, null, new string[] {"0F", "92", "/0"}));
		}
		
		// SETC rmreg8
		public void SETC (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETC", target.ToString(), null, target, null, null, new string[] {"0F", "92", "/0"}));
		}
		
		// SETE mem8
		public void SETE (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETE", target.ToString(), target, null, null, null, new string[] {"0F", "94", "/0"}));
		}
		
		// SETE rmreg8
		public void SETE (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETE", target.ToString(), null, target, null, null, new string[] {"0F", "94", "/0"}));
		}
		
		// SETG mem8
		public void SETG (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETG", target.ToString(), target, null, null, null, new string[] {"0F", "9F", "/0"}));
		}
		
		// SETG rmreg8
		public void SETG (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETG", target.ToString(), null, target, null, null, new string[] {"0F", "9F", "/0"}));
		}
		
		// SETGE mem8
		public void SETGE (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETGE", target.ToString(), target, null, null, null, new string[] {"0F", "9D", "/0"}));
		}
		
		// SETGE rmreg8
		public void SETGE (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETGE", target.ToString(), null, target, null, null, new string[] {"0F", "9D", "/0"}));
		}
		
		// SETL mem8
		public void SETL (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETL", target.ToString(), target, null, null, null, new string[] {"0F", "9C", "/0"}));
		}
		
		// SETL rmreg8
		public void SETL (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETL", target.ToString(), null, target, null, null, new string[] {"0F", "9C", "/0"}));
		}
		
		// SETLE mem8
		public void SETLE (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETLE", target.ToString(), target, null, null, null, new string[] {"0F", "9E", "/0"}));
		}
		
		// SETLE rmreg8
		public void SETLE (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETLE", target.ToString(), null, target, null, null, new string[] {"0F", "9E", "/0"}));
		}
		
		// SETNA mem8
		public void SETNA (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNA", target.ToString(), target, null, null, null, new string[] {"0F", "96", "/0"}));
		}
		
		// SETNA rmreg8
		public void SETNA (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNA", target.ToString(), null, target, null, null, new string[] {"0F", "96", "/0"}));
		}
		
		// SETNAE mem8
		public void SETNAE (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNAE", target.ToString(), target, null, null, null, new string[] {"0F", "92", "/0"}));
		}
		
		// SETNAE rmreg8
		public void SETNAE (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNAE", target.ToString(), null, target, null, null, new string[] {"0F", "92", "/0"}));
		}
		
		// SETNB mem8
		public void SETNB (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNB", target.ToString(), target, null, null, null, new string[] {"0F", "93", "/0"}));
		}
		
		// SETNB rmreg8
		public void SETNB (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNB", target.ToString(), null, target, null, null, new string[] {"0F", "93", "/0"}));
		}
		
		// SETNBE mem8
		public void SETNBE (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNBE", target.ToString(), target, null, null, null, new string[] {"0F", "97", "/0"}));
		}
		
		// SETNBE rmreg8
		public void SETNBE (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNBE", target.ToString(), null, target, null, null, new string[] {"0F", "97", "/0"}));
		}
		
		// SETNC mem8
		public void SETNC (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNC", target.ToString(), target, null, null, null, new string[] {"0F", "93", "/0"}));
		}
		
		// SETNC rmreg8
		public void SETNC (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNC", target.ToString(), null, target, null, null, new string[] {"0F", "93", "/0"}));
		}
		
		// SETNE mem8
		public void SETNE (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNE", target.ToString(), target, null, null, null, new string[] {"0F", "95", "/0"}));
		}
		
		// SETNE rmreg8
		public void SETNE (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNE", target.ToString(), null, target, null, null, new string[] {"0F", "95", "/0"}));
		}
		
		// SETNG mem8
		public void SETNG (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNG", target.ToString(), target, null, null, null, new string[] {"0F", "9E", "/0"}));
		}
		
		// SETNG rmreg8
		public void SETNG (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNG", target.ToString(), null, target, null, null, new string[] {"0F", "9E", "/0"}));
		}
		
		// SETNGE mem8
		public void SETNGE (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNGE", target.ToString(), target, null, null, null, new string[] {"0F", "9C", "/0"}));
		}
		
		// SETNGE rmreg8
		public void SETNGE (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNGE", target.ToString(), null, target, null, null, new string[] {"0F", "9C", "/0"}));
		}
		
		// SETNL mem8
		public void SETNL (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNL", target.ToString(), target, null, null, null, new string[] {"0F", "9D", "/0"}));
		}
		
		// SETNL rmreg8
		public void SETNL (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNL", target.ToString(), null, target, null, null, new string[] {"0F", "9D", "/0"}));
		}
		
		// SETNLE mem8
		public void SETNLE (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNLE", target.ToString(), target, null, null, null, new string[] {"0F", "9F", "/0"}));
		}
		
		// SETNLE rmreg8
		public void SETNLE (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNLE", target.ToString(), null, target, null, null, new string[] {"0F", "9F", "/0"}));
		}
		
		// SETNO mem8
		public void SETNO (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNO", target.ToString(), target, null, null, null, new string[] {"0F", "91", "/0"}));
		}
		
		// SETNO rmreg8
		public void SETNO (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNO", target.ToString(), null, target, null, null, new string[] {"0F", "91", "/0"}));
		}
		
		// SETNP mem8
		public void SETNP (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNP", target.ToString(), target, null, null, null, new string[] {"0F", "9B", "/0"}));
		}
		
		// SETNP rmreg8
		public void SETNP (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNP", target.ToString(), null, target, null, null, new string[] {"0F", "9B", "/0"}));
		}
		
		// SETNS mem8
		public void SETNS (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNS", target.ToString(), target, null, null, null, new string[] {"0F", "99", "/0"}));
		}
		
		// SETNS rmreg8
		public void SETNS (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNS", target.ToString(), null, target, null, null, new string[] {"0F", "99", "/0"}));
		}
		
		// SETNZ mem8
		public void SETNZ (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNZ", target.ToString(), target, null, null, null, new string[] {"0F", "95", "/0"}));
		}
		
		// SETNZ rmreg8
		public void SETNZ (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETNZ", target.ToString(), null, target, null, null, new string[] {"0F", "95", "/0"}));
		}
		
		// SETO mem8
		public void SETO (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETO", target.ToString(), target, null, null, null, new string[] {"0F", "90", "/0"}));
		}
		
		// SETO rmreg8
		public void SETO (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETO", target.ToString(), null, target, null, null, new string[] {"0F", "90", "/0"}));
		}
		
		// SETP mem8
		public void SETP (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETP", target.ToString(), target, null, null, null, new string[] {"0F", "9A", "/0"}));
		}
		
		// SETP rmreg8
		public void SETP (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETP", target.ToString(), null, target, null, null, new string[] {"0F", "9A", "/0"}));
		}
		
		// SETPE mem8
		public void SETPE (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETPE", target.ToString(), target, null, null, null, new string[] {"0F", "9A", "/0"}));
		}
		
		// SETPE rmreg8
		public void SETPE (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETPE", target.ToString(), null, target, null, null, new string[] {"0F", "9A", "/0"}));
		}
		
		// SETPO mem8
		public void SETPO (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETPO", target.ToString(), target, null, null, null, new string[] {"0F", "9B", "/0"}));
		}
		
		// SETPO rmreg8
		public void SETPO (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETPO", target.ToString(), null, target, null, null, new string[] {"0F", "9B", "/0"}));
		}
		
		// SETS mem8
		public void SETS (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETS", target.ToString(), target, null, null, null, new string[] {"0F", "98", "/0"}));
		}
		
		// SETS rmreg8
		public void SETS (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETS", target.ToString(), null, target, null, null, new string[] {"0F", "98", "/0"}));
		}
		
		// SETZ mem8
		public void SETZ (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETZ", target.ToString(), target, null, null, null, new string[] {"0F", "94", "/0"}));
		}
		
		// SETZ rmreg8
		public void SETZ (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SETZ", target.ToString(), null, target, null, null, new string[] {"0F", "94", "/0"}));
		}
		
		// SFENCE 
		public void SFENCE ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SFENCE", "", null, null, null, null, new string[] {"0F", "AE", "/7"}));
		}
		
		// SGDT mem
		public void SGDT (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SGDT", target.ToString(), target, null, null, null, new string[] {"0F", "01", "/0"}));
		}
		
		// SHL mem8,CL
		public void SHL__CL (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHL__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"D2", "/4"}));
		}
		
		// SHL mem8,imm8
		public void SHL (ByteMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHL__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"D0", "/4"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHL", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"C0", "/4", "ib"}));
			}
		}
		
		// SHL mem16,CL
		public void SHL__CL (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHL__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"o16", "D3", "/4"}));
		}
		
		// SHL mem16,imm8
		public void SHL (WordMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHL__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"o16", "D1", "/4"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHL", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "C1", "/4", "ib"}));
			}
		}
		
		// SHL mem32,CL
		public void SHL__CL (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHL__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"o32", "D3", "/4"}));
		}
		
		// SHL mem32,imm8
		public void SHL (DWordMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHL__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"o32", "D1", "/4"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHL", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "C1", "/4", "ib"}));
			}
		}
		
		// SHL rmreg8,CL
		public void SHL__CL (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHL__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"D2", "/4"}));
		}
		
		// SHL rmreg8,imm8
		public void SHL (R8Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHL__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"D0", "/4"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHL", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"C0", "/4", "ib"}));
			}
		}
		
		// SHL rmreg16,CL
		public void SHL__CL (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHL__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"o16", "D3", "/4"}));
		}
		
		// SHL rmreg16,imm8
		public void SHL (R16Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHL__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"o16", "D1", "/4"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHL", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "C1", "/4", "ib"}));
			}
		}
		
		// SHL rmreg32,CL
		public void SHL__CL (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHL__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"o32", "D3", "/4"}));
		}
		
		// SHL rmreg32,imm8
		public void SHL (R32Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHL__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"o32", "D1", "/4"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHL", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "C1", "/4", "ib"}));
			}
		}
		
		// SHLD mem16,reg16,imm8
		public void SHLD (WordMemory target, R16Type source, Byte value)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHLD", target.ToString() + ", " + source.ToString() + ", " + string.Format("0x{0:x}", value), target, null, source, new UInt32[] {value}, new string[] {"o16", "0F", "A4", "/r", "ib"}));
		}
		
		// SHLD mem32,reg32,imm8
		public void SHLD (DWordMemory target, R32Type source, Byte value)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHLD", target.ToString() + ", " + source.ToString() + ", " + string.Format("0x{0:x}", value), target, null, source, new UInt32[] {value}, new string[] {"o32", "0F", "A4", "/r", "ib"}));
		}
		
		// SHLD mem16,reg16,CL
		public void SHLD___CL (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHLD___CL", target.ToString() + ", " + source.ToString() + ", " + "CL", target, null, source, null, new string[] {"o16", "0F", "A5", "/r"}));
		}
		
		// SHLD mem32,reg32,CL
		public void SHLD___CL (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHLD___CL", target.ToString() + ", " + source.ToString() + ", " + "CL", target, null, source, null, new string[] {"o32", "0F", "A5", "/r"}));
		}
		
		// SHLD rmreg16,reg16,imm8
		public void SHLD (R16Type target, R16Type source, Byte value)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHLD", target.ToString() + ", " + source.ToString() + ", " + string.Format("0x{0:x}", value), null, target, source, new UInt32[] {value}, new string[] {"o16", "0F", "A4", "/r", "ib"}));
		}
		
		// SHLD rmreg32,reg32,imm8
		public void SHLD (R32Type target, R32Type source, Byte value)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHLD", target.ToString() + ", " + source.ToString() + ", " + string.Format("0x{0:x}", value), null, target, source, new UInt32[] {value}, new string[] {"o32", "0F", "A4", "/r", "ib"}));
		}
		
		// SHLD rmreg16,reg16,CL
		public void SHLD___CL (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHLD___CL", target.ToString() + ", " + source.ToString() + ", " + "CL", null, target, source, null, new string[] {"o16", "0F", "A5", "/r"}));
		}
		
		// SHLD rmreg32,reg32,CL
		public void SHLD___CL (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHLD___CL", target.ToString() + ", " + source.ToString() + ", " + "CL", null, target, source, null, new string[] {"o32", "0F", "A5", "/r"}));
		}
		
		// SHR mem8,CL
		public void SHR__CL (ByteMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHR__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"D2", "/5"}));
		}
		
		// SHR mem8,imm8
		public void SHR (ByteMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHR__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"D0", "/5"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"C0", "/5", "ib"}));
			}
		}
		
		// SHR mem16,CL
		public void SHR__CL (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHR__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"o16", "D3", "/5"}));
		}
		
		// SHR mem16,imm8
		public void SHR (WordMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHR__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"o16", "D1", "/5"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "C1", "/5", "ib"}));
			}
		}
		
		// SHR mem32,CL
		public void SHR__CL (DWordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHR__CL", target.ToString() + ", " + "CL", target, null, null, null, new string[] {"o32", "D3", "/5"}));
		}
		
		// SHR mem32,imm8
		public void SHR (DWordMemory target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHR__1", target.ToString() + ", " + "1", target, null, null, null, new string[] {"o32", "D1", "/5"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "C1", "/5", "ib"}));
			}
		}
		
		// SHR rmreg8,CL
		public void SHR__CL (R8Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHR__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"D2", "/5"}));
		}
		
		// SHR rmreg8,imm8
		public void SHR (R8Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHR__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"D0", "/5"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"C0", "/5", "ib"}));
			}
		}
		
		// SHR rmreg16,CL
		public void SHR__CL (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHR__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"o16", "D3", "/5"}));
		}
		
		// SHR rmreg16,imm8
		public void SHR (R16Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHR__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"o16", "D1", "/5"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "C1", "/5", "ib"}));
			}
		}
		
		// SHR rmreg32,CL
		public void SHR__CL (R32Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHR__CL", target.ToString() + ", " + "CL", null, target, null, null, new string[] {"o32", "D3", "/5"}));
		}
		
		// SHR rmreg32,imm8
		public void SHR (R32Type target, Byte source)
		{
			if (source == 1)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHR__1", target.ToString() + ", " + "1", null, target, null, null, new string[] {"o32", "D1", "/5"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "C1", "/5", "ib"}));
			}
		}
		
		// SHRD mem16,reg16,imm8
		public void SHRD (WordMemory target, R16Type source, Byte value)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHRD", target.ToString() + ", " + source.ToString() + ", " + string.Format("0x{0:x}", value), target, null, source, new UInt32[] {value}, new string[] {"o16", "0F", "AC", "/r", "ib"}));
		}
		
		// SHRD mem32,reg32,imm8
		public void SHRD (DWordMemory target, R32Type source, Byte value)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHRD", target.ToString() + ", " + source.ToString() + ", " + string.Format("0x{0:x}", value), target, null, source, new UInt32[] {value}, new string[] {"o32", "0F", "AC", "/r", "ib"}));
		}
		
		// SHRD mem16,reg16,CL
		public void SHRD___CL (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHRD___CL", target.ToString() + ", " + source.ToString() + ", " + "CL", target, null, source, null, new string[] {"o16", "0F", "AD", "/r"}));
		}
		
		// SHRD mem32,reg32,CL
		public void SHRD___CL (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHRD___CL", target.ToString() + ", " + source.ToString() + ", " + "CL", target, null, source, null, new string[] {"o32", "0F", "AD", "/r"}));
		}
		
		// SHRD rmreg16,reg16,imm8
		public void SHRD (R16Type target, R16Type source, Byte value)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHRD", target.ToString() + ", " + source.ToString() + ", " + string.Format("0x{0:x}", value), null, target, source, new UInt32[] {value}, new string[] {"o16", "0F", "AC", "/r", "ib"}));
		}
		
		// SHRD rmreg32,reg32,imm8
		public void SHRD (R32Type target, R32Type source, Byte value)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHRD", target.ToString() + ", " + source.ToString() + ", " + string.Format("0x{0:x}", value), null, target, source, new UInt32[] {value}, new string[] {"o32", "0F", "AC", "/r", "ib"}));
		}
		
		// SHRD rmreg16,reg16,CL
		public void SHRD___CL (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHRD___CL", target.ToString() + ", " + source.ToString() + ", " + "CL", null, target, source, null, new string[] {"o16", "0F", "AD", "/r"}));
		}
		
		// SHRD rmreg32,reg32,CL
		public void SHRD___CL (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SHRD___CL", target.ToString() + ", " + source.ToString() + ", " + "CL", null, target, source, null, new string[] {"o32", "0F", "AD", "/r"}));
		}
		
		// SIDT mem
		public void SIDT (Memory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SIDT", target.ToString(), target, null, null, null, new string[] {"0F", "01", "/1"}));
		}
		
		// SLDT mem16
		public void SLDT (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SLDT", target.ToString(), target, null, null, null, new string[] {"0F", "00", "/0"}));
		}
		
		// SLDT rmreg16
		public void SLDT (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SLDT", target.ToString(), null, target, null, null, new string[] {"o16", "0F", "00", "/0"}));
		}
		
		// SMSW mem16
		public void SMSW (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SMSW", target.ToString(), target, null, null, null, new string[] {"0F", "01", "/4"}));
		}
		
		// SMSW rmreg16
		public void SMSW (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SMSW", target.ToString(), null, target, null, null, new string[] {"o16", "0F", "01", "/4"}));
		}
		
		// STC 
		public void STC ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "STC", "", null, null, null, null, new string[] {"F9"}));
		}
		
		// STD 
		public void STD ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "STD", "", null, null, null, null, new string[] {"FD"}));
		}
		
		// STI 
		public void STI ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "STI", "", null, null, null, null, new string[] {"FB"}));
		}
		
		// STOSB 
		public void STOSB ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "STOSB", "", null, null, null, null, new string[] {"AA"}));
		}
		
		// STOSD 
		public void STOSD ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "STOSD", "", null, null, null, null, new string[] {"o32", "AB"}));
		}
		
		// STOSW 
		public void STOSW ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "STOSW", "", null, null, null, null, new string[] {"o16", "AB"}));
		}
		
		// STR mem16
		public void STR (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "STR", target.ToString(), target, null, null, null, new string[] {"0F", "00", "/1"}));
		}
		
		// STR rmreg16
		public void STR (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "STR", target.ToString(), null, target, null, null, new string[] {"o16", "0F", "00", "/1"}));
		}
		
		// SUB mem8,reg8
		public void SUB (ByteMemory target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"28", "/r"}));
		}
		
		// SUB mem16,reg16
		public void SUB (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o16", "29", "/r"}));
		}
		
		// SUB mem32,reg32
		public void SUB (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o32", "29", "/r"}));
		}
		
		// SUB reg8,mem8
		public void SUB (R8Type target, ByteMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"2A", "/r"}));
		}
		
		// SUB reg16,mem16
		public void SUB (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "2B", "/r"}));
		}
		
		// SUB reg32,mem32
		public void SUB (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "2B", "/r"}));
		}
		
		// SUB mem8,imm8
		public void SUB (ByteMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"80", "/5", "ib"}));
		}
		
		// SUB mem16,imm16
		public void SUB (WordMemory target, UInt16 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "81", "/5", "iw"}));
		}
		
		// SUB mem32,imm32
		public void SUB (DWordMemory target, UInt32 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "81", "/5", "id"}));
		}
		
		// SUB mem16,imm8
		public void SUB (WordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "83", "/5", "ib"}));
		}
		
		// SUB mem32,imm8
		public void SUB (DWordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "83", "/5", "ib"}));
		}
		
		// SUB rmreg8,reg8
		public void SUB (R8Type target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"28", "/r"}));
		}
		
		// SUB rmreg16,reg16
		public void SUB (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o16", "29", "/r"}));
		}
		
		// SUB rmreg32,reg32
		public void SUB (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o32", "29", "/r"}));
		}
		
		// SUB rmreg8,imm8
		public void SUB (R8Type target, Byte source)
		{
			if (target == R8.AL)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB_AL", "AL" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"2C", "ib"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"80", "/5", "ib"}));
			}
		}
		
		// SUB rmreg16,imm16
		public void SUB (R16Type target, UInt16 source)
		{
			if (target == R16.AX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB_AX", "AX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o16", "2D", "iw"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "81", "/5", "iw"}));
			}
		}
		
		// SUB rmreg32,imm32
		public void SUB (R32Type target, UInt32 source)
		{
			if (target == R32.EAX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB_EAX", "EAX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o32", "2D", "id"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "81", "/5", "id"}));
			}
		}
		
		// SUB rmreg16,imm8
		public void SUB (R16Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "83", "/5", "ib"}));
		}
		
		// SUB rmreg32,imm8
		public void SUB (R32Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SUB", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "83", "/5", "ib"}));
		}
		
		// SYSCALL 
		public void SYSCALL ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SYSCALL", "", null, null, null, null, new string[] {"0F", "05"}));
		}
		
		// SYSENTER 
		public void SYSENTER ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SYSENTER", "", null, null, null, null, new string[] {"0F", "34"}));
		}
		
		// SYSEXIT 
		public void SYSEXIT ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SYSEXIT", "", null, null, null, null, new string[] {"0F", "35"}));
		}
		
		// SYSRET 
		public void SYSRET ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "SYSRET", "", null, null, null, null, new string[] {"0F", "07"}));
		}
		
		// TEST mem8,reg8
		public void TEST (ByteMemory target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "TEST", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"84", "/r"}));
		}
		
		// TEST mem16,reg16
		public void TEST (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "TEST", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o16", "85", "/r"}));
		}
		
		// TEST mem32,reg32
		public void TEST (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "TEST", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o32", "85", "/r"}));
		}
		
		// TEST mem8,imm8
		public void TEST (ByteMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "TEST", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"F6", "/0", "ib"}));
		}
		
		// TEST mem16,imm16
		public void TEST (WordMemory target, UInt16 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "TEST", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "F7", "/0", "iw"}));
		}
		
		// TEST mem32,imm32
		public void TEST (DWordMemory target, UInt32 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "TEST", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "F7", "/0", "id"}));
		}
		
		// TEST rmreg8,reg8
		public void TEST (R8Type target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "TEST", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"84", "/r"}));
		}
		
		// TEST rmreg16,reg16
		public void TEST (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "TEST", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o16", "85", "/r"}));
		}
		
		// TEST rmreg32,reg32
		public void TEST (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "TEST", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o32", "85", "/r"}));
		}
		
		// TEST rmreg8,imm8
		public void TEST (R8Type target, Byte source)
		{
			if (target == R8.AL)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "TEST_AL", "AL" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"A8", "ib"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "TEST", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"F6", "/0", "ib"}));
			}
		}
		
		// TEST rmreg16,imm16
		public void TEST (R16Type target, UInt16 source)
		{
			if (target == R16.AX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "TEST_AX", "AX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o16", "A9", "iw"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "TEST", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "F7", "/0", "iw"}));
			}
		}
		
		// TEST rmreg32,imm32
		public void TEST (R32Type target, UInt32 source)
		{
			if (target == R32.EAX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "TEST_EAX", "EAX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o32", "A9", "id"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "TEST", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "F7", "/0", "id"}));
			}
		}
		
		// VERR mem16
		public void VERR (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "VERR", target.ToString(), target, null, null, null, new string[] {"0F", "00", "/4"}));
		}
		
		// VERR rmreg16
		public void VERR (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "VERR", target.ToString(), null, target, null, null, new string[] {"0F", "00", "/4"}));
		}
		
		// VERW mem16
		public void VERW (WordMemory target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "VERW", target.ToString(), target, null, null, null, new string[] {"0F", "00", "/5"}));
		}
		
		// VERW rmreg16
		public void VERW (R16Type target)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "VERW", target.ToString(), null, target, null, null, new string[] {"0F", "00", "/5"}));
		}
		
		// WAIT 
		public void WAIT ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "WAIT", "", null, null, null, null, new string[] {"9B"}));
		}
		
		// WBINVD 
		public void WBINVD ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "WBINVD", "", null, null, null, null, new string[] {"0F", "09"}));
		}
		
		// WRMSR 
		public void WRMSR ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "WRMSR", "", null, null, null, null, new string[] {"0F", "30"}));
		}
		
		// XADD mem8,reg8
		public void XADD (ByteMemory target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XADD", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"0F", "C0", "/r"}));
		}
		
		// XADD mem16,reg16
		public void XADD (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XADD", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o16", "0F", "C1", "/r"}));
		}
		
		// XADD mem32,reg32
		public void XADD (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XADD", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o32", "0F", "C1", "/r"}));
		}
		
		// XADD rmreg8,reg8
		public void XADD (R8Type target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XADD", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"0F", "C0", "/r"}));
		}
		
		// XADD rmreg16,reg16
		public void XADD (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XADD", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o16", "0F", "C1", "/r"}));
		}
		
		// XADD rmreg32,reg32
		public void XADD (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XADD", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o32", "0F", "C1", "/r"}));
		}
		
		// XCHG reg8,mem8
		public void XCHG (R8Type target, ByteMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XCHG", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"86", "/r"}));
		}
		
		// XCHG reg16,mem16
		public void XCHG (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XCHG", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "87", "/r"}));
		}
		
		// XCHG reg32,mem32
		public void XCHG (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XCHG", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "87", "/r"}));
		}
		
		// XCHG mem8,reg8
		public void XCHG (ByteMemory target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XCHG", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"86", "/r"}));
		}
		
		// XCHG mem16,reg16
		public void XCHG (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XCHG", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o16", "87", "/r"}));
		}
		
		// XCHG mem32,reg32
		public void XCHG (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XCHG", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o32", "87", "/r"}));
		}
		
		// XCHG reg8,rmreg8
		public void XCHG (R8Type target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XCHG", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"86", "/r"}));
		}
		
		// XCHG reg16,rmreg16
		public void XCHG (R16Type target, R16Type source)
		{
			if (target == R16.AX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XCHG_AX", "AX" + ", " + source.ToString(), null, null, source, null, new string[] {"o16", "90+r"}));
			}
			else if (source == R16.AX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XCHG__AX", target.ToString() + ", " + "AX", null, null, target, null, new string[] {"o16", "90+r"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XCHG", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o16", "87", "/r"}));
			}
		}
		
		// XCHG reg32,rmreg32
		public void XCHG (R32Type target, R32Type source)
		{
			if (target == R32.EAX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XCHG_EAX", "EAX" + ", " + source.ToString(), null, null, source, null, new string[] {"o32", "90+r"}));
			}
			else if (source == R32.EAX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XCHG__EAX", target.ToString() + ", " + "EAX", null, null, target, null, new string[] {"o32", "90+r"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XCHG", target.ToString() + ", " + source.ToString(), null, source, target, null, new string[] {"o32", "87", "/r"}));
			}
		}
		
		// XLAT 
		public void XLAT ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XLAT", "", null, null, null, null, new string[] {"D7"}));
		}
		
		// XLATB 
		public void XLATB ()
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XLATB", "", null, null, null, null, new string[] {"D7"}));
		}
		
		// XOR mem8,reg8
		public void XOR (ByteMemory target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"30", "/r"}));
		}
		
		// XOR mem16,reg16
		public void XOR (WordMemory target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o16", "31", "/r"}));
		}
		
		// XOR mem32,reg32
		public void XOR (DWordMemory target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + source.ToString(), target, null, source, null, new string[] {"o32", "31", "/r"}));
		}
		
		// XOR reg8,mem8
		public void XOR (R8Type target, ByteMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"32", "/r"}));
		}
		
		// XOR reg16,mem16
		public void XOR (R16Type target, WordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o16", "33", "/r"}));
		}
		
		// XOR reg32,mem32
		public void XOR (R32Type target, DWordMemory source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + source.ToString(), source, null, target, null, new string[] {"o32", "33", "/r"}));
		}
		
		// XOR mem8,imm8
		public void XOR (ByteMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"80", "/6", "ib"}));
		}
		
		// XOR mem16,imm16
		public void XOR (WordMemory target, UInt16 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "81", "/6", "iw"}));
		}
		
		// XOR mem32,imm32
		public void XOR (DWordMemory target, UInt32 source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "81", "/6", "id"}));
		}
		
		// XOR mem16,imm8
		public void XOR (WordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o16", "83", "/6", "ib"}));
		}
		
		// XOR mem32,imm8
		public void XOR (DWordMemory target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + string.Format("0x{0:x}", source), target, null, null, new UInt32[] {source}, new string[] {"o32", "83", "/6", "ib"}));
		}
		
		// XOR rmreg8,reg8
		public void XOR (R8Type target, R8Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"30", "/r"}));
		}
		
		// XOR rmreg16,reg16
		public void XOR (R16Type target, R16Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o16", "31", "/r"}));
		}
		
		// XOR rmreg32,reg32
		public void XOR (R32Type target, R32Type source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + source.ToString(), null, target, source, null, new string[] {"o32", "31", "/r"}));
		}
		
		// XOR rmreg8,imm8
		public void XOR (R8Type target, Byte source)
		{
			if (target == R8.AL)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR_AL", "AL" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"34", "ib"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"80", "/6", "ib"}));
			}
		}
		
		// XOR rmreg16,imm16
		public void XOR (R16Type target, UInt16 source)
		{
			if (target == R16.AX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR_AX", "AX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o16", "35", "iw"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "81", "/6", "iw"}));
			}
		}
		
		// XOR rmreg32,imm32
		public void XOR (R32Type target, UInt32 source)
		{
			if (target == R32.EAX)
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR_EAX", "EAX" + ", " + string.Format("0x{0:x}", source), null, null, null, new UInt32[] {source}, new string[] {"o32", "35", "id"}));
			}
			else
			{
				this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "81", "/6", "id"}));
			}
		}
		
		// XOR rmreg16,imm8
		public void XOR (R16Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o16", "83", "/6", "ib"}));
		}
		
		// XOR rmreg32,imm8
		public void XOR (R32Type target, Byte source)
		{
			this.instructions.Add(new Instruction(true, string.Empty, string.Empty, "XOR", target.ToString() + ", " + string.Format("0x{0:x}", source), null, target, null, new UInt32[] {source}, new string[] {"o32", "83", "/6", "ib"}));
		}
	}
	
}
