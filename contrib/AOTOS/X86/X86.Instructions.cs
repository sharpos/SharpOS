/**
 *  (C) 2006-2007 Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
 * 
 */

using System;

namespace SharpOS.AOT.X86
{
	public class Asm
	{
		
		// AAA 
		public static void AAA () {}
		
		// AAD 
		public static void AAD () {}
		
		// AAD imm8
		public static void AAD (Byte target) {}
		
		// AAM 
		public static void AAM () {}
		
		// AAM imm8
		public static void AAM (Byte target) {}
		
		// AAS 
		public static void AAS () {}
		
		// ADC mem8,reg8
		public static void ADC (ByteMemory target, R8Type source) {}
		
		// ADC mem16,reg16
		public static void ADC (WordMemory target, R16Type source) {}
		
		// ADC mem32,reg32
		public static void ADC (DWordMemory target, R32Type source) {}
		
		// ADC reg8,mem8
		public static void ADC (R8Type target, ByteMemory source) {}
		
		// ADC reg16,mem16
		public static void ADC (R16Type target, WordMemory source) {}
		
		// ADC reg32,mem32
		public static void ADC (R32Type target, DWordMemory source) {}
		
		// ADC mem8,imm8
		public static void ADC (ByteMemory target, Byte source) {}
		
		// ADC mem16,imm16
		public static void ADC (WordMemory target, UInt16 source) {}
		
		// ADC mem32,imm32
		public static void ADC (DWordMemory target, UInt32 source) {}
		
		// ADC mem16,imm8
		public static void ADC (WordMemory target, Byte source) {}
		
		// ADC mem32,imm8
		public static void ADC (DWordMemory target, Byte source) {}
		
		// ADC rmreg8,reg8
		public static void ADC (R8Type target, R8Type source) {}
		
		// ADC rmreg16,reg16
		public static void ADC (R16Type target, R16Type source) {}
		
		// ADC rmreg32,reg32
		public static void ADC (R32Type target, R32Type source) {}
		
		// ADC rmreg8,imm8
		public static void ADC (R8Type target, Byte source) {}
		
		// ADC rmreg16,imm16
		public static void ADC (R16Type target, UInt16 source) {}
		
		// ADC rmreg32,imm32
		public static void ADC (R32Type target, UInt32 source) {}
		
		// ADC rmreg16,imm8
		public static void ADC (R16Type target, Byte source) {}
		
		// ADC rmreg32,imm8
		public static void ADC (R32Type target, Byte source) {}
		
		// ADD mem8,reg8
		public static void ADD (ByteMemory target, R8Type source) {}
		
		// ADD mem16,reg16
		public static void ADD (WordMemory target, R16Type source) {}
		
		// ADD mem32,reg32
		public static void ADD (DWordMemory target, R32Type source) {}
		
		// ADD reg8,mem8
		public static void ADD (R8Type target, ByteMemory source) {}
		
		// ADD reg16,mem16
		public static void ADD (R16Type target, WordMemory source) {}
		
		// ADD reg32,mem32
		public static void ADD (R32Type target, DWordMemory source) {}
		
		// ADD mem8,imm8
		public static void ADD (ByteMemory target, Byte source) {}
		
		// ADD mem16,imm16
		public static void ADD (WordMemory target, UInt16 source) {}
		
		// ADD mem32,imm32
		public static void ADD (DWordMemory target, UInt32 source) {}
		
		// ADD mem16,imm8
		public static void ADD (WordMemory target, Byte source) {}
		
		// ADD mem32,imm8
		public static void ADD (DWordMemory target, Byte source) {}
		
		// ADD rmreg8,reg8
		public static void ADD (R8Type target, R8Type source) {}
		
		// ADD rmreg16,reg16
		public static void ADD (R16Type target, R16Type source) {}
		
		// ADD rmreg32,reg32
		public static void ADD (R32Type target, R32Type source) {}
		
		// ADD rmreg8,imm8
		public static void ADD (R8Type target, Byte source) {}
		
		// ADD rmreg16,imm16
		public static void ADD (R16Type target, UInt16 source) {}
		
		// ADD rmreg32,imm32
		public static void ADD (R32Type target, UInt32 source) {}
		
		// ADD rmreg16,imm8
		public static void ADD (R16Type target, Byte source) {}
		
		// ADD rmreg32,imm8
		public static void ADD (R32Type target, Byte source) {}
		
		// AND mem8,reg8
		public static void AND (ByteMemory target, R8Type source) {}
		
		// AND mem16,reg16
		public static void AND (WordMemory target, R16Type source) {}
		
		// AND mem32,reg32
		public static void AND (DWordMemory target, R32Type source) {}
		
		// AND reg8,mem8
		public static void AND (R8Type target, ByteMemory source) {}
		
		// AND reg16,mem16
		public static void AND (R16Type target, WordMemory source) {}
		
		// AND reg32,mem32
		public static void AND (R32Type target, DWordMemory source) {}
		
		// AND mem8,imm8
		public static void AND (ByteMemory target, Byte source) {}
		
		// AND mem16,imm16
		public static void AND (WordMemory target, UInt16 source) {}
		
		// AND mem32,imm32
		public static void AND (DWordMemory target, UInt32 source) {}
		
		// AND mem16,imm8
		public static void AND (WordMemory target, Byte source) {}
		
		// AND mem32,imm8
		public static void AND (DWordMemory target, Byte source) {}
		
		// AND rmreg8,reg8
		public static void AND (R8Type target, R8Type source) {}
		
		// AND rmreg16,reg16
		public static void AND (R16Type target, R16Type source) {}
		
		// AND rmreg32,reg32
		public static void AND (R32Type target, R32Type source) {}
		
		// AND rmreg8,imm8
		public static void AND (R8Type target, Byte source) {}
		
		// AND rmreg16,imm16
		public static void AND (R16Type target, UInt16 source) {}
		
		// AND rmreg32,imm32
		public static void AND (R32Type target, UInt32 source) {}
		
		// AND rmreg16,imm8
		public static void AND (R16Type target, Byte source) {}
		
		// AND rmreg32,imm8
		public static void AND (R32Type target, Byte source) {}
		
		// ARPL mem16,reg16
		public static void ARPL (WordMemory target, R16Type source) {}
		
		// ARPL rmreg16,reg16
		public static void ARPL (R16Type target, R16Type source) {}
		
		// BITS32 
		public static void BITS32 (bool value) {}
		
		// BOUND reg16,mem
		public static void BOUND (R16Type target, Memory source) {}
		
		// BOUND reg32,mem
		public static void BOUND (R32Type target, Memory source) {}
		
		// BSF reg16,mem16
		public static void BSF (R16Type target, WordMemory source) {}
		
		// BSF reg32,mem32
		public static void BSF (R32Type target, DWordMemory source) {}
		
		// BSF reg16,rmreg16
		public static void BSF (R16Type target, R16Type source) {}
		
		// BSF reg32,rmreg32
		public static void BSF (R32Type target, R32Type source) {}
		
		// BSR reg16,mem16
		public static void BSR (R16Type target, WordMemory source) {}
		
		// BSR reg32,mem32
		public static void BSR (R32Type target, DWordMemory source) {}
		
		// BSR reg16,rmreg16
		public static void BSR (R16Type target, R16Type source) {}
		
		// BSR reg32,rmreg32
		public static void BSR (R32Type target, R32Type source) {}
		
		// BSWAP reg32
		public static void BSWAP (R32Type target) {}
		
		// BT mem16,reg16
		public static void BT (WordMemory target, R16Type source) {}
		
		// BT mem32,reg32
		public static void BT (DWordMemory target, R32Type source) {}
		
		// BT mem16,imm8
		public static void BT (WordMemory target, Byte source) {}
		
		// BT mem32,imm8
		public static void BT (DWordMemory target, Byte source) {}
		
		// BT rmreg16,reg16
		public static void BT (R16Type target, R16Type source) {}
		
		// BT rmreg32,reg32
		public static void BT (R32Type target, R32Type source) {}
		
		// BT rmreg16,imm8
		public static void BT (R16Type target, Byte source) {}
		
		// BT rmreg32,imm8
		public static void BT (R32Type target, Byte source) {}
		
		// BTC mem16,reg16
		public static void BTC (WordMemory target, R16Type source) {}
		
		// BTC mem32,reg32
		public static void BTC (DWordMemory target, R32Type source) {}
		
		// BTC mem16,imm8
		public static void BTC (WordMemory target, Byte source) {}
		
		// BTC mem32,imm8
		public static void BTC (DWordMemory target, Byte source) {}
		
		// BTC rmreg16,reg16
		public static void BTC (R16Type target, R16Type source) {}
		
		// BTC rmreg32,reg32
		public static void BTC (R32Type target, R32Type source) {}
		
		// BTC rmreg16,imm8
		public static void BTC (R16Type target, Byte source) {}
		
		// BTC rmreg32,imm8
		public static void BTC (R32Type target, Byte source) {}
		
		// BTR mem16,reg16
		public static void BTR (WordMemory target, R16Type source) {}
		
		// BTR mem32,reg32
		public static void BTR (DWordMemory target, R32Type source) {}
		
		// BTR mem16,imm8
		public static void BTR (WordMemory target, Byte source) {}
		
		// BTR mem32,imm8
		public static void BTR (DWordMemory target, Byte source) {}
		
		// BTR rmreg16,reg16
		public static void BTR (R16Type target, R16Type source) {}
		
		// BTR rmreg32,reg32
		public static void BTR (R32Type target, R32Type source) {}
		
		// BTR rmreg16,imm8
		public static void BTR (R16Type target, Byte source) {}
		
		// BTR rmreg32,imm8
		public static void BTR (R32Type target, Byte source) {}
		
		// BTS mem16,reg16
		public static void BTS (WordMemory target, R16Type source) {}
		
		// BTS mem32,reg32
		public static void BTS (DWordMemory target, R32Type source) {}
		
		// BTS mem16,imm8
		public static void BTS (WordMemory target, Byte source) {}
		
		// BTS mem32,imm8
		public static void BTS (DWordMemory target, Byte source) {}
		
		// BTS rmreg16,reg16
		public static void BTS (R16Type target, R16Type source) {}
		
		// BTS rmreg32,reg32
		public static void BTS (R32Type target, R32Type source) {}
		
		// BTS rmreg16,imm8
		public static void BTS (R16Type target, Byte source) {}
		
		// BTS rmreg32,imm8
		public static void BTS (R32Type target, Byte source) {}
		
		// CALL imm
		public static void CALL (UInt32 target) {}
		
		public static void CALL (string label) {}
		
		// CALL imm16:imm16
		public static void CALL (UInt16 target, UInt16 source) {}
		
		// CALL imm16:imm32
		public static void CALL (UInt16 target, UInt32 source) {}
		
		// CALL FAR mem16
		public static void CALL_FAR (WordMemory target) {}
		
		public static void CALL_FAR (string label) {}
		
		// CALL FAR mem32
		public static void CALL_FAR (DWordMemory target) {}
		
		// CALL mem16
		public static void CALL (WordMemory target) {}
		
		// CALL mem32
		public static void CALL (DWordMemory target) {}
		
		// CALL rmreg16
		public static void CALL (R16Type target) {}
		
		// CALL rmreg32
		public static void CALL (R32Type target) {}
		
		// CBW 
		public static void CBW () {}
		
		// CDQ 
		public static void CDQ () {}
		
		// CLC 
		public static void CLC () {}
		
		// CLD 
		public static void CLD () {}
		
		// CLFLUSH mem
		public static void CLFLUSH (Memory target) {}
		
		// CLI 
		public static void CLI () {}
		
		// CLTS 
		public static void CLTS () {}
		
		// CMC 
		public static void CMC () {}
		
		// CMOVA reg16,mem16
		public static void CMOVA (R16Type target, WordMemory source) {}
		
		// CMOVA reg32,mem32
		public static void CMOVA (R32Type target, DWordMemory source) {}
		
		// CMOVA reg16,rmreg16
		public static void CMOVA (R16Type target, R16Type source) {}
		
		// CMOVA reg32,rmreg32
		public static void CMOVA (R32Type target, R32Type source) {}
		
		// CMOVAE reg16,mem16
		public static void CMOVAE (R16Type target, WordMemory source) {}
		
		// CMOVAE reg32,mem32
		public static void CMOVAE (R32Type target, DWordMemory source) {}
		
		// CMOVAE reg16,rmreg16
		public static void CMOVAE (R16Type target, R16Type source) {}
		
		// CMOVAE reg32,rmreg32
		public static void CMOVAE (R32Type target, R32Type source) {}
		
		// CMOVB reg16,mem16
		public static void CMOVB (R16Type target, WordMemory source) {}
		
		// CMOVB reg32,mem32
		public static void CMOVB (R32Type target, DWordMemory source) {}
		
		// CMOVB reg16,rmreg16
		public static void CMOVB (R16Type target, R16Type source) {}
		
		// CMOVB reg32,rmreg32
		public static void CMOVB (R32Type target, R32Type source) {}
		
		// CMOVBE reg16,mem16
		public static void CMOVBE (R16Type target, WordMemory source) {}
		
		// CMOVBE reg32,mem32
		public static void CMOVBE (R32Type target, DWordMemory source) {}
		
		// CMOVBE reg16,rmreg16
		public static void CMOVBE (R16Type target, R16Type source) {}
		
		// CMOVBE reg32,rmreg32
		public static void CMOVBE (R32Type target, R32Type source) {}
		
		// CMOVC reg16,mem16
		public static void CMOVC (R16Type target, WordMemory source) {}
		
		// CMOVC reg32,mem32
		public static void CMOVC (R32Type target, DWordMemory source) {}
		
		// CMOVC reg16,rmreg16
		public static void CMOVC (R16Type target, R16Type source) {}
		
		// CMOVC reg32,rmreg32
		public static void CMOVC (R32Type target, R32Type source) {}
		
		// CMOVE reg16,mem16
		public static void CMOVE (R16Type target, WordMemory source) {}
		
		// CMOVE reg32,mem32
		public static void CMOVE (R32Type target, DWordMemory source) {}
		
		// CMOVE reg16,rmreg16
		public static void CMOVE (R16Type target, R16Type source) {}
		
		// CMOVE reg32,rmreg32
		public static void CMOVE (R32Type target, R32Type source) {}
		
		// CMOVG reg16,mem16
		public static void CMOVG (R16Type target, WordMemory source) {}
		
		// CMOVG reg32,mem32
		public static void CMOVG (R32Type target, DWordMemory source) {}
		
		// CMOVG reg16,rmreg16
		public static void CMOVG (R16Type target, R16Type source) {}
		
		// CMOVG reg32,rmreg32
		public static void CMOVG (R32Type target, R32Type source) {}
		
		// CMOVGE reg16,mem16
		public static void CMOVGE (R16Type target, WordMemory source) {}
		
		// CMOVGE reg32,mem32
		public static void CMOVGE (R32Type target, DWordMemory source) {}
		
		// CMOVGE reg16,rmreg16
		public static void CMOVGE (R16Type target, R16Type source) {}
		
		// CMOVGE reg32,rmreg32
		public static void CMOVGE (R32Type target, R32Type source) {}
		
		// CMOVL reg16,mem16
		public static void CMOVL (R16Type target, WordMemory source) {}
		
		// CMOVL reg32,mem32
		public static void CMOVL (R32Type target, DWordMemory source) {}
		
		// CMOVL reg16,rmreg16
		public static void CMOVL (R16Type target, R16Type source) {}
		
		// CMOVL reg32,rmreg32
		public static void CMOVL (R32Type target, R32Type source) {}
		
		// CMOVLE reg16,mem16
		public static void CMOVLE (R16Type target, WordMemory source) {}
		
		// CMOVLE reg32,mem32
		public static void CMOVLE (R32Type target, DWordMemory source) {}
		
		// CMOVLE reg16,rmreg16
		public static void CMOVLE (R16Type target, R16Type source) {}
		
		// CMOVLE reg32,rmreg32
		public static void CMOVLE (R32Type target, R32Type source) {}
		
		// CMOVNA reg16,mem16
		public static void CMOVNA (R16Type target, WordMemory source) {}
		
		// CMOVNA reg32,mem32
		public static void CMOVNA (R32Type target, DWordMemory source) {}
		
		// CMOVNA reg16,rmreg16
		public static void CMOVNA (R16Type target, R16Type source) {}
		
		// CMOVNA reg32,rmreg32
		public static void CMOVNA (R32Type target, R32Type source) {}
		
		// CMOVNAE reg16,mem16
		public static void CMOVNAE (R16Type target, WordMemory source) {}
		
		// CMOVNAE reg32,mem32
		public static void CMOVNAE (R32Type target, DWordMemory source) {}
		
		// CMOVNAE reg16,rmreg16
		public static void CMOVNAE (R16Type target, R16Type source) {}
		
		// CMOVNAE reg32,rmreg32
		public static void CMOVNAE (R32Type target, R32Type source) {}
		
		// CMOVNB reg16,mem16
		public static void CMOVNB (R16Type target, WordMemory source) {}
		
		// CMOVNB reg32,mem32
		public static void CMOVNB (R32Type target, DWordMemory source) {}
		
		// CMOVNB reg16,rmreg16
		public static void CMOVNB (R16Type target, R16Type source) {}
		
		// CMOVNB reg32,rmreg32
		public static void CMOVNB (R32Type target, R32Type source) {}
		
		// CMOVNBE reg16,mem16
		public static void CMOVNBE (R16Type target, WordMemory source) {}
		
		// CMOVNBE reg32,mem32
		public static void CMOVNBE (R32Type target, DWordMemory source) {}
		
		// CMOVNBE reg16,rmreg16
		public static void CMOVNBE (R16Type target, R16Type source) {}
		
		// CMOVNBE reg32,rmreg32
		public static void CMOVNBE (R32Type target, R32Type source) {}
		
		// CMOVNC reg16,mem16
		public static void CMOVNC (R16Type target, WordMemory source) {}
		
		// CMOVNC reg32,mem32
		public static void CMOVNC (R32Type target, DWordMemory source) {}
		
		// CMOVNC reg16,rmreg16
		public static void CMOVNC (R16Type target, R16Type source) {}
		
		// CMOVNC reg32,rmreg32
		public static void CMOVNC (R32Type target, R32Type source) {}
		
		// CMOVNE reg16,mem16
		public static void CMOVNE (R16Type target, WordMemory source) {}
		
		// CMOVNE reg32,mem32
		public static void CMOVNE (R32Type target, DWordMemory source) {}
		
		// CMOVNE reg16,rmreg16
		public static void CMOVNE (R16Type target, R16Type source) {}
		
		// CMOVNE reg32,rmreg32
		public static void CMOVNE (R32Type target, R32Type source) {}
		
		// CMOVNG reg16,mem16
		public static void CMOVNG (R16Type target, WordMemory source) {}
		
		// CMOVNG reg32,mem32
		public static void CMOVNG (R32Type target, DWordMemory source) {}
		
		// CMOVNG reg16,rmreg16
		public static void CMOVNG (R16Type target, R16Type source) {}
		
		// CMOVNG reg32,rmreg32
		public static void CMOVNG (R32Type target, R32Type source) {}
		
		// CMOVNGE reg16,mem16
		public static void CMOVNGE (R16Type target, WordMemory source) {}
		
		// CMOVNGE reg32,mem32
		public static void CMOVNGE (R32Type target, DWordMemory source) {}
		
		// CMOVNGE reg16,rmreg16
		public static void CMOVNGE (R16Type target, R16Type source) {}
		
		// CMOVNGE reg32,rmreg32
		public static void CMOVNGE (R32Type target, R32Type source) {}
		
		// CMOVNL reg16,mem16
		public static void CMOVNL (R16Type target, WordMemory source) {}
		
		// CMOVNL reg32,mem32
		public static void CMOVNL (R32Type target, DWordMemory source) {}
		
		// CMOVNL reg16,rmreg16
		public static void CMOVNL (R16Type target, R16Type source) {}
		
		// CMOVNL reg32,rmreg32
		public static void CMOVNL (R32Type target, R32Type source) {}
		
		// CMOVNLE reg16,mem16
		public static void CMOVNLE (R16Type target, WordMemory source) {}
		
		// CMOVNLE reg32,mem32
		public static void CMOVNLE (R32Type target, DWordMemory source) {}
		
		// CMOVNLE reg16,rmreg16
		public static void CMOVNLE (R16Type target, R16Type source) {}
		
		// CMOVNLE reg32,rmreg32
		public static void CMOVNLE (R32Type target, R32Type source) {}
		
		// CMOVNO reg16,mem16
		public static void CMOVNO (R16Type target, WordMemory source) {}
		
		// CMOVNO reg32,mem32
		public static void CMOVNO (R32Type target, DWordMemory source) {}
		
		// CMOVNO reg16,rmreg16
		public static void CMOVNO (R16Type target, R16Type source) {}
		
		// CMOVNO reg32,rmreg32
		public static void CMOVNO (R32Type target, R32Type source) {}
		
		// CMOVNP reg16,mem16
		public static void CMOVNP (R16Type target, WordMemory source) {}
		
		// CMOVNP reg32,mem32
		public static void CMOVNP (R32Type target, DWordMemory source) {}
		
		// CMOVNP reg16,rmreg16
		public static void CMOVNP (R16Type target, R16Type source) {}
		
		// CMOVNP reg32,rmreg32
		public static void CMOVNP (R32Type target, R32Type source) {}
		
		// CMOVNS reg16,mem16
		public static void CMOVNS (R16Type target, WordMemory source) {}
		
		// CMOVNS reg32,mem32
		public static void CMOVNS (R32Type target, DWordMemory source) {}
		
		// CMOVNS reg16,rmreg16
		public static void CMOVNS (R16Type target, R16Type source) {}
		
		// CMOVNS reg32,rmreg32
		public static void CMOVNS (R32Type target, R32Type source) {}
		
		// CMOVNZ reg16,mem16
		public static void CMOVNZ (R16Type target, WordMemory source) {}
		
		// CMOVNZ reg32,mem32
		public static void CMOVNZ (R32Type target, DWordMemory source) {}
		
		// CMOVNZ reg16,rmreg16
		public static void CMOVNZ (R16Type target, R16Type source) {}
		
		// CMOVNZ reg32,rmreg32
		public static void CMOVNZ (R32Type target, R32Type source) {}
		
		// CMOVO reg16,mem16
		public static void CMOVO (R16Type target, WordMemory source) {}
		
		// CMOVO reg32,mem32
		public static void CMOVO (R32Type target, DWordMemory source) {}
		
		// CMOVO reg16,rmreg16
		public static void CMOVO (R16Type target, R16Type source) {}
		
		// CMOVO reg32,rmreg32
		public static void CMOVO (R32Type target, R32Type source) {}
		
		// CMOVP reg16,mem16
		public static void CMOVP (R16Type target, WordMemory source) {}
		
		// CMOVP reg32,mem32
		public static void CMOVP (R32Type target, DWordMemory source) {}
		
		// CMOVP reg16,rmreg16
		public static void CMOVP (R16Type target, R16Type source) {}
		
		// CMOVP reg32,rmreg32
		public static void CMOVP (R32Type target, R32Type source) {}
		
		// CMOVPE reg16,mem16
		public static void CMOVPE (R16Type target, WordMemory source) {}
		
		// CMOVPE reg32,mem32
		public static void CMOVPE (R32Type target, DWordMemory source) {}
		
		// CMOVPE reg16,rmreg16
		public static void CMOVPE (R16Type target, R16Type source) {}
		
		// CMOVPE reg32,rmreg32
		public static void CMOVPE (R32Type target, R32Type source) {}
		
		// CMOVPO reg16,mem16
		public static void CMOVPO (R16Type target, WordMemory source) {}
		
		// CMOVPO reg32,mem32
		public static void CMOVPO (R32Type target, DWordMemory source) {}
		
		// CMOVPO reg16,rmreg16
		public static void CMOVPO (R16Type target, R16Type source) {}
		
		// CMOVPO reg32,rmreg32
		public static void CMOVPO (R32Type target, R32Type source) {}
		
		// CMOVS reg16,mem16
		public static void CMOVS (R16Type target, WordMemory source) {}
		
		// CMOVS reg32,mem32
		public static void CMOVS (R32Type target, DWordMemory source) {}
		
		// CMOVS reg16,rmreg16
		public static void CMOVS (R16Type target, R16Type source) {}
		
		// CMOVS reg32,rmreg32
		public static void CMOVS (R32Type target, R32Type source) {}
		
		// CMOVZ reg16,mem16
		public static void CMOVZ (R16Type target, WordMemory source) {}
		
		// CMOVZ reg32,mem32
		public static void CMOVZ (R32Type target, DWordMemory source) {}
		
		// CMOVZ reg16,rmreg16
		public static void CMOVZ (R16Type target, R16Type source) {}
		
		// CMOVZ reg32,rmreg32
		public static void CMOVZ (R32Type target, R32Type source) {}
		
		// CMP mem8,reg8
		public static void CMP (ByteMemory target, R8Type source) {}
		
		// CMP mem16,reg16
		public static void CMP (WordMemory target, R16Type source) {}
		
		// CMP mem32,reg32
		public static void CMP (DWordMemory target, R32Type source) {}
		
		// CMP reg8,mem8
		public static void CMP (R8Type target, ByteMemory source) {}
		
		// CMP reg16,mem16
		public static void CMP (R16Type target, WordMemory source) {}
		
		// CMP reg32,mem32
		public static void CMP (R32Type target, DWordMemory source) {}
		
		// CMP mem8,imm8
		public static void CMP (ByteMemory target, Byte source) {}
		
		// CMP mem16,imm16
		public static void CMP (WordMemory target, UInt16 source) {}
		
		// CMP mem32,imm32
		public static void CMP (DWordMemory target, UInt32 source) {}
		
		// CMP mem16,imm8
		public static void CMP (WordMemory target, Byte source) {}
		
		// CMP mem32,imm8
		public static void CMP (DWordMemory target, Byte source) {}
		
		// CMP rmreg8,reg8
		public static void CMP (R8Type target, R8Type source) {}
		
		// CMP rmreg16,reg16
		public static void CMP (R16Type target, R16Type source) {}
		
		// CMP rmreg32,reg32
		public static void CMP (R32Type target, R32Type source) {}
		
		// CMP rmreg8,imm8
		public static void CMP (R8Type target, Byte source) {}
		
		// CMP rmreg16,imm16
		public static void CMP (R16Type target, UInt16 source) {}
		
		// CMP rmreg32,imm32
		public static void CMP (R32Type target, UInt32 source) {}
		
		// CMP rmreg16,imm8
		public static void CMP (R16Type target, Byte source) {}
		
		// CMP rmreg32,imm8
		public static void CMP (R32Type target, Byte source) {}
		
		// CMPSB 
		public static void CMPSB () {}
		
		// CMPSD 
		public static void CMPSD () {}
		
		// CMPSW 
		public static void CMPSW () {}
		
		// CMPXCHG mem8,reg8
		public static void CMPXCHG (ByteMemory target, R8Type source) {}
		
		// CMPXCHG mem16,reg16
		public static void CMPXCHG (WordMemory target, R16Type source) {}
		
		// CMPXCHG mem32,reg32
		public static void CMPXCHG (DWordMemory target, R32Type source) {}
		
		// CMPXCHG rmreg8,reg8
		public static void CMPXCHG (R8Type target, R8Type source) {}
		
		// CMPXCHG rmreg16,reg16
		public static void CMPXCHG (R16Type target, R16Type source) {}
		
		// CMPXCHG rmreg32,reg32
		public static void CMPXCHG (R32Type target, R32Type source) {}
		
		// CMPXCHG8B mem
		public static void CMPXCHG8B (Memory target) {}
		
		// CPUID 
		public static void CPUID () {}
		
		// CWD 
		public static void CWD () {}
		
		// CWDE 
		public static void CWDE () {}
		
		// DAA 
		public static void DAA () {}
		
		// DAS 
		public static void DAS () {}
		
		// DATA 
		public static void DATA (string name, string values) {}
		
		// DATA 
		public static void DATA (string name, byte value) {}
		
		// DATA 
		public static void DATA (string name, UInt16 value) {}
		
		// DATA 
		public static void DATA (string name, UInt32 value) {}
		
		// DATA 
		public static void DATA (string values) {}
		
		// DATA 
		public static void DATA (byte value) {}
		
		// DATA 
		public static void DATA (UInt16 value) {}
		
		// DATA 
		public static void DATA (UInt32 value) {}
		
		// DEC reg16
		public static void DEC (R16Type target) {}
		
		// DEC reg32
		public static void DEC (R32Type target) {}
		
		// DEC mem8
		public static void DEC (ByteMemory target) {}
		
		// DEC mem16
		public static void DEC (WordMemory target) {}
		
		// DEC mem32
		public static void DEC (DWordMemory target) {}
		
		// DEC rmreg8
		public static void DEC (R8Type target) {}
		
		// DIV mem8
		public static void DIV (ByteMemory target) {}
		
		// DIV mem16
		public static void DIV (WordMemory target) {}
		
		// DIV mem32
		public static void DIV (DWordMemory target) {}
		
		// DIV rmreg8
		public static void DIV (R8Type target) {}
		
		// DIV rmreg16
		public static void DIV (R16Type target) {}
		
		// DIV rmreg32
		public static void DIV (R32Type target) {}
		
		// EMMS 
		public static void EMMS () {}
		
		// ENTER imm16,imm8
		public static void ENTER (UInt16 target, Byte source) {}
		
		// F2XM1 
		public static void F2XM1 () {}
		
		// FABS 
		public static void FABS () {}
		
		// FADD mem32
		public static void FADD (DWordMemory target) {}
		
		// FADD mem64
		public static void FADD (QWordMemory target) {}
		
		// FADD fpureg
		public static void FADD (FPType target) {}
		
		// FADD ST0,fpureg
		public static void FADD_ST0 (FPType source) {}
		
		// FADD fpureg,ST0
		public static void FADD__ST0 (FPType target) {}
		
		// FADDP fpureg
		public static void FADDP (FPType target) {}
		
		// FADDP fpureg,ST0
		public static void FADDP__ST0 (FPType target) {}
		
		// FBLD mem80
		public static void FBLD (TWordMemory target) {}
		
		// FBSTP mem80
		public static void FBSTP (TWordMemory target) {}
		
		// FCHS 
		public static void FCHS () {}
		
		// FCLEX 
		public static void FCLEX () {}
		
		// FCMOVB fpureg
		public static void FCMOVB (FPType target) {}
		
		// FCMOVB ST0,fpureg
		public static void FCMOVB_ST0 (FPType source) {}
		
		// FCMOVBE fpureg
		public static void FCMOVBE (FPType target) {}
		
		// FCMOVBE ST0,fpureg
		public static void FCMOVBE_ST0 (FPType source) {}
		
		// FCMOVE fpureg
		public static void FCMOVE (FPType target) {}
		
		// FCMOVE ST0,fpureg
		public static void FCMOVE_ST0 (FPType source) {}
		
		// FCMOVNB fpureg
		public static void FCMOVNB (FPType target) {}
		
		// FCMOVNB ST0,fpureg
		public static void FCMOVNB_ST0 (FPType source) {}
		
		// FCMOVNBE fpureg
		public static void FCMOVNBE (FPType target) {}
		
		// FCMOVNBE ST0,fpureg
		public static void FCMOVNBE_ST0 (FPType source) {}
		
		// FCMOVNE fpureg
		public static void FCMOVNE (FPType target) {}
		
		// FCMOVNE ST0,fpureg
		public static void FCMOVNE_ST0 (FPType source) {}
		
		// FCMOVNU fpureg
		public static void FCMOVNU (FPType target) {}
		
		// FCMOVNU ST0,fpureg
		public static void FCMOVNU_ST0 (FPType source) {}
		
		// FCMOVU fpureg
		public static void FCMOVU (FPType target) {}
		
		// FCMOVU ST0,fpureg
		public static void FCMOVU_ST0 (FPType source) {}
		
		// FCOM mem32
		public static void FCOM (DWordMemory target) {}
		
		// FCOM mem64
		public static void FCOM (QWordMemory target) {}
		
		// FCOM fpureg
		public static void FCOM (FPType target) {}
		
		// FCOM ST0,fpureg
		public static void FCOM_ST0 (FPType source) {}
		
		// FCOMI fpureg
		public static void FCOMI (FPType target) {}
		
		// FCOMI ST0,fpureg
		public static void FCOMI_ST0 (FPType source) {}
		
		// FCOMIP fpureg
		public static void FCOMIP (FPType target) {}
		
		// FCOMIP ST0,fpureg
		public static void FCOMIP_ST0 (FPType source) {}
		
		// FCOMP mem32
		public static void FCOMP (DWordMemory target) {}
		
		// FCOMP mem64
		public static void FCOMP (QWordMemory target) {}
		
		// FCOMP fpureg
		public static void FCOMP (FPType target) {}
		
		// FCOMP ST0,fpureg
		public static void FCOMP_ST0 (FPType source) {}
		
		// FCOMPP 
		public static void FCOMPP () {}
		
		// FCOS 
		public static void FCOS () {}
		
		// FDECSTP 
		public static void FDECSTP () {}
		
		// FDISI 
		public static void FDISI () {}
		
		// FDIV mem32
		public static void FDIV (DWordMemory target) {}
		
		// FDIV mem64
		public static void FDIV (QWordMemory target) {}
		
		// FDIV fpureg
		public static void FDIV (FPType target) {}
		
		// FDIV ST0,fpureg
		public static void FDIV_ST0 (FPType source) {}
		
		// FDIV fpureg,ST0
		public static void FDIV__ST0 (FPType target) {}
		
		// FDIVP fpureg
		public static void FDIVP (FPType target) {}
		
		// FDIVP fpureg,ST0
		public static void FDIVP__ST0 (FPType target) {}
		
		// FDIVR mem32
		public static void FDIVR (DWordMemory target) {}
		
		// FDIVR mem64
		public static void FDIVR (QWordMemory target) {}
		
		// FDIVR fpureg
		public static void FDIVR (FPType target) {}
		
		// FDIVR ST0,fpureg
		public static void FDIVR_ST0 (FPType source) {}
		
		// FDIVR fpureg,ST0
		public static void FDIVR__ST0 (FPType target) {}
		
		// FDIVRP fpureg
		public static void FDIVRP (FPType target) {}
		
		// FDIVRP fpureg,ST0
		public static void FDIVRP__ST0 (FPType target) {}
		
		// FENI 
		public static void FENI () {}
		
		// FFREE fpureg
		public static void FFREE (FPType target) {}
		
		// FFREEP fpureg
		public static void FFREEP (FPType target) {}
		
		// FIADD mem16
		public static void FIADD (WordMemory target) {}
		
		// FIADD mem32
		public static void FIADD (DWordMemory target) {}
		
		// FICOM mem16
		public static void FICOM (WordMemory target) {}
		
		// FICOM mem32
		public static void FICOM (DWordMemory target) {}
		
		// FICOMP mem16
		public static void FICOMP (WordMemory target) {}
		
		// FICOMP mem32
		public static void FICOMP (DWordMemory target) {}
		
		// FIDIV mem16
		public static void FIDIV (WordMemory target) {}
		
		// FIDIV mem32
		public static void FIDIV (DWordMemory target) {}
		
		// FIDIVR mem16
		public static void FIDIVR (WordMemory target) {}
		
		// FIDIVR mem32
		public static void FIDIVR (DWordMemory target) {}
		
		// FILD mem16
		public static void FILD (WordMemory target) {}
		
		// FILD mem32
		public static void FILD (DWordMemory target) {}
		
		// FILD mem64
		public static void FILD (QWordMemory target) {}
		
		// FIMUL mem16
		public static void FIMUL (WordMemory target) {}
		
		// FIMUL mem32
		public static void FIMUL (DWordMemory target) {}
		
		// FINCSTP 
		public static void FINCSTP () {}
		
		// FINIT 
		public static void FINIT () {}
		
		// FIST mem16
		public static void FIST (WordMemory target) {}
		
		// FIST mem32
		public static void FIST (DWordMemory target) {}
		
		// FISTP mem16
		public static void FISTP (WordMemory target) {}
		
		// FISTP mem32
		public static void FISTP (DWordMemory target) {}
		
		// FISTP mem64
		public static void FISTP (QWordMemory target) {}
		
		// FISUB mem16
		public static void FISUB (WordMemory target) {}
		
		// FISUB mem32
		public static void FISUB (DWordMemory target) {}
		
		// FISUBR mem16
		public static void FISUBR (WordMemory target) {}
		
		// FISUBR mem32
		public static void FISUBR (DWordMemory target) {}
		
		// FLD mem32
		public static void FLD (DWordMemory target) {}
		
		// FLD mem64
		public static void FLD (QWordMemory target) {}
		
		// FLD mem80
		public static void FLD (TWordMemory target) {}
		
		// FLD fpureg
		public static void FLD (FPType target) {}
		
		// FLD1 
		public static void FLD1 () {}
		
		// FLDCW mem16
		public static void FLDCW (WordMemory target) {}
		
		// FLDENV mem
		public static void FLDENV (Memory target) {}
		
		// FLDL2E 
		public static void FLDL2E () {}
		
		// FLDL2T 
		public static void FLDL2T () {}
		
		// FLDLG2 
		public static void FLDLG2 () {}
		
		// FLDLN2 
		public static void FLDLN2 () {}
		
		// FLDPI 
		public static void FLDPI () {}
		
		// FLDZ 
		public static void FLDZ () {}
		
		// FMUL mem32
		public static void FMUL (DWordMemory target) {}
		
		// FMUL mem64
		public static void FMUL (QWordMemory target) {}
		
		// FMUL fpureg
		public static void FMUL (FPType target) {}
		
		// FMUL ST0,fpureg
		public static void FMUL_ST0 (FPType source) {}
		
		// FMUL fpureg,ST0
		public static void FMUL__ST0 (FPType target) {}
		
		// FMULP fpureg
		public static void FMULP (FPType target) {}
		
		// FMULP fpureg,ST0
		public static void FMULP__ST0 (FPType target) {}
		
		// FNCLEX 
		public static void FNCLEX () {}
		
		// FNDISI 
		public static void FNDISI () {}
		
		// FNENI 
		public static void FNENI () {}
		
		// FNINIT 
		public static void FNINIT () {}
		
		// FNOP 
		public static void FNOP () {}
		
		// FNSAVE mem
		public static void FNSAVE (Memory target) {}
		
		// FNSTCW mem16
		public static void FNSTCW (WordMemory target) {}
		
		// FNSTENV mem
		public static void FNSTENV (Memory target) {}
		
		// FNSTSW mem16
		public static void FNSTSW (WordMemory target) {}
		
		// FNSTSW AX
		public static void FNSTSW_AX () {}
		
		// FPATAN 
		public static void FPATAN () {}
		
		// FPREM 
		public static void FPREM () {}
		
		// FPREM1 
		public static void FPREM1 () {}
		
		// FPTAN 
		public static void FPTAN () {}
		
		// FRNDINT 
		public static void FRNDINT () {}
		
		// FRSTOR mem
		public static void FRSTOR (Memory target) {}
		
		// FSAVE mem
		public static void FSAVE (Memory target) {}
		
		// FSCALE 
		public static void FSCALE () {}
		
		// FSETPM 
		public static void FSETPM () {}
		
		// FSIN 
		public static void FSIN () {}
		
		// FSINCOS 
		public static void FSINCOS () {}
		
		// FSQRT 
		public static void FSQRT () {}
		
		// FST mem32
		public static void FST (DWordMemory target) {}
		
		// FST mem64
		public static void FST (QWordMemory target) {}
		
		// FST fpureg
		public static void FST (FPType target) {}
		
		// FSTCW mem16
		public static void FSTCW (WordMemory target) {}
		
		// FSTENV mem
		public static void FSTENV (Memory target) {}
		
		// FSTP mem32
		public static void FSTP (DWordMemory target) {}
		
		// FSTP mem64
		public static void FSTP (QWordMemory target) {}
		
		// FSTP mem80
		public static void FSTP (TWordMemory target) {}
		
		// FSTP fpureg
		public static void FSTP (FPType target) {}
		
		// FSTSW mem16
		public static void FSTSW (WordMemory target) {}
		
		// FSTSW AX
		public static void FSTSW_AX () {}
		
		// FSUB mem32
		public static void FSUB (DWordMemory target) {}
		
		// FSUB mem64
		public static void FSUB (QWordMemory target) {}
		
		// FSUB fpureg
		public static void FSUB (FPType target) {}
		
		// FSUB ST0,fpureg
		public static void FSUB_ST0 (FPType source) {}
		
		// FSUB fpureg,ST0
		public static void FSUB__ST0 (FPType target) {}
		
		// FSUBP fpureg
		public static void FSUBP (FPType target) {}
		
		// FSUBP fpureg,ST0
		public static void FSUBP__ST0 (FPType target) {}
		
		// FSUBR mem32
		public static void FSUBR (DWordMemory target) {}
		
		// FSUBR mem64
		public static void FSUBR (QWordMemory target) {}
		
		// FSUBR fpureg
		public static void FSUBR (FPType target) {}
		
		// FSUBR ST0,fpureg
		public static void FSUBR_ST0 (FPType source) {}
		
		// FSUBR fpureg,ST0
		public static void FSUBR__ST0 (FPType target) {}
		
		// FSUBRP fpureg
		public static void FSUBRP (FPType target) {}
		
		// FSUBRP fpureg,ST0
		public static void FSUBRP__ST0 (FPType target) {}
		
		// FTST 
		public static void FTST () {}
		
		// FUCOM fpureg
		public static void FUCOM (FPType target) {}
		
		// FUCOM ST0,fpureg
		public static void FUCOM_ST0 (FPType source) {}
		
		// FUCOMI fpureg
		public static void FUCOMI (FPType target) {}
		
		// FUCOMI ST0,fpureg
		public static void FUCOMI_ST0 (FPType source) {}
		
		// FUCOMIP fpureg
		public static void FUCOMIP (FPType target) {}
		
		// FUCOMIP ST0,fpureg
		public static void FUCOMIP_ST0 (FPType source) {}
		
		// FUCOMP fpureg
		public static void FUCOMP (FPType target) {}
		
		// FUCOMP ST0,fpureg
		public static void FUCOMP_ST0 (FPType source) {}
		
		// FUCOMPP 
		public static void FUCOMPP () {}
		
		// FWAIT 
		public static void FWAIT () {}
		
		// FXAM 
		public static void FXAM () {}
		
		// FXCH 
		public static void FXCH () {}
		
		// FXCH fpureg
		public static void FXCH (FPType target) {}
		
		// FXCH fpureg,ST0
		public static void FXCH__ST0 (FPType target) {}
		
		// FXCH ST0,fpureg
		public static void FXCH_ST0 (FPType source) {}
		
		// FXRSTOR memory
		public static void FXRSTOR (Memory target) {}
		
		// FXSAVE memory
		public static void FXSAVE (Memory target) {}
		
		// FXTRACT 
		public static void FXTRACT () {}
		
		// FYL2X 
		public static void FYL2X () {}
		
		// FYL2XP1 
		public static void FYL2XP1 () {}
		
		// HLT 
		public static void HLT () {}
		
		// ICEBP 
		public static void ICEBP () {}
		
		// IDIV mem8
		public static void IDIV (ByteMemory target) {}
		
		// IDIV mem16
		public static void IDIV (WordMemory target) {}
		
		// IDIV mem32
		public static void IDIV (DWordMemory target) {}
		
		// IDIV rmreg8
		public static void IDIV (R8Type target) {}
		
		// IDIV rmreg16
		public static void IDIV (R16Type target) {}
		
		// IDIV rmreg32
		public static void IDIV (R32Type target) {}
		
		// IMUL mem8
		public static void IMUL (ByteMemory target) {}
		
		// IMUL mem16
		public static void IMUL (WordMemory target) {}
		
		// IMUL mem32
		public static void IMUL (DWordMemory target) {}
		
		// IMUL reg16,mem16
		public static void IMUL (R16Type target, WordMemory source) {}
		
		// IMUL reg32,mem32
		public static void IMUL (R32Type target, DWordMemory source) {}
		
		// IMUL reg16,imm8
		public static void IMUL (R16Type target, Byte source) {}
		
		// IMUL reg16,imm16
		public static void IMUL (R16Type target, UInt16 source) {}
		
		// IMUL reg32,imm8
		public static void IMUL (R32Type target, Byte source) {}
		
		// IMUL reg32,imm32
		public static void IMUL (R32Type target, UInt32 source) {}
		
		// IMUL reg16,mem16,imm8
		public static void IMUL (R16Type target, WordMemory source, Byte value) {}
		
		// IMUL reg16,mem16,imm16
		public static void IMUL (R16Type target, WordMemory source, UInt16 value) {}
		
		// IMUL reg32,mem32,imm8
		public static void IMUL (R32Type target, DWordMemory source, Byte value) {}
		
		// IMUL reg32,mem32,imm32
		public static void IMUL (R32Type target, DWordMemory source, UInt32 value) {}
		
		// IMUL rmreg8
		public static void IMUL (R8Type target) {}
		
		// IMUL rmreg16
		public static void IMUL (R16Type target) {}
		
		// IMUL rmreg32
		public static void IMUL (R32Type target) {}
		
		// IMUL reg16,rmreg16
		public static void IMUL (R16Type target, R16Type source) {}
		
		// IMUL reg32,rmreg32
		public static void IMUL (R32Type target, R32Type source) {}
		
		// IMUL reg16,rmreg16,imm8
		public static void IMUL (R16Type target, R16Type source, Byte value) {}
		
		// IMUL reg16,rmreg16,imm16
		public static void IMUL (R16Type target, R16Type source, UInt16 value) {}
		
		// IMUL reg32,rmreg32,imm8
		public static void IMUL (R32Type target, R32Type source, Byte value) {}
		
		// IMUL reg32,rmreg32,imm32
		public static void IMUL (R32Type target, R32Type source, UInt32 value) {}
		
		// IN AL,imm8
		public static void IN_AL (Byte source) {}
		
		// IN AX,imm8
		public static void IN_AX (Byte source) {}
		
		// IN EAX,imm8
		public static void IN_EAX (Byte source) {}
		
		// IN AL,DX
		public static void IN_AL__DX () {}
		
		// IN AX,DX
		public static void IN_AX__DX () {}
		
		// IN EAX,DX
		public static void IN_EAX__DX () {}
		
		// INC reg16
		public static void INC (R16Type target) {}
		
		// INC reg32
		public static void INC (R32Type target) {}
		
		// INC mem8
		public static void INC (ByteMemory target) {}
		
		// INC mem16
		public static void INC (WordMemory target) {}
		
		// INC mem32
		public static void INC (DWordMemory target) {}
		
		// INC rmreg8
		public static void INC (R8Type target) {}
		
		// INSB 
		public static void INSB () {}
		
		// INSD 
		public static void INSD () {}
		
		// INSW 
		public static void INSW () {}
		
		// INT imm8
		public static void INT (Byte target) {}
		
		// INTO 
		public static void INTO () {}
		
		// INVD 
		public static void INVD () {}
		
		// INVLPG mem
		public static void INVLPG (Memory target) {}
		
		// IRET 
		public static void IRET () {}
		
		// IRETD 
		public static void IRETD () {}
		
		// IRETW 
		public static void IRETW () {}
		
		// JA imm8
		public static void JA (Byte target) {}
		
		public static void JA (string label) {}
		
		// JA NEAR imm
		public static void JA (UInt32 target) {}
		
		// JAE imm8
		public static void JAE (Byte target) {}
		
		public static void JAE (string label) {}
		
		// JAE NEAR imm
		public static void JAE (UInt32 target) {}
		
		// JB imm8
		public static void JB (Byte target) {}
		
		public static void JB (string label) {}
		
		// JB NEAR imm
		public static void JB (UInt32 target) {}
		
		// JBE imm8
		public static void JBE (Byte target) {}
		
		public static void JBE (string label) {}
		
		// JBE NEAR imm
		public static void JBE (UInt32 target) {}
		
		// JC imm8
		public static void JC (Byte target) {}
		
		public static void JC (string label) {}
		
		// JC NEAR imm
		public static void JC (UInt32 target) {}
		
		// JCXZ imm8
		public static void JCXZ (Byte target) {}
		
		public static void JCXZ (string label) {}
		
		// JE imm8
		public static void JE (Byte target) {}
		
		public static void JE (string label) {}
		
		// JE NEAR imm
		public static void JE (UInt32 target) {}
		
		// JECXZ imm8
		public static void JECXZ (Byte target) {}
		
		public static void JECXZ (string label) {}
		
		// JG imm8
		public static void JG (Byte target) {}
		
		public static void JG (string label) {}
		
		// JG NEAR imm
		public static void JG (UInt32 target) {}
		
		// JGE imm8
		public static void JGE (Byte target) {}
		
		public static void JGE (string label) {}
		
		// JGE NEAR imm
		public static void JGE (UInt32 target) {}
		
		// JL imm8
		public static void JL (Byte target) {}
		
		public static void JL (string label) {}
		
		// JL NEAR imm
		public static void JL (UInt32 target) {}
		
		// JLE imm8
		public static void JLE (Byte target) {}
		
		public static void JLE (string label) {}
		
		// JLE NEAR imm
		public static void JLE (UInt32 target) {}
		
		// JMP imm
		public static void JMP (UInt32 target) {}
		
		public static void JMP (string label) {}
		
		// JMP imm8
		public static void JMP (Byte target) {}
		
		// JMP imm16:imm16
		public static void JMP (UInt16 target, UInt16 source) {}
		
		// JMP imm16:imm32
		public static void JMP (UInt16 target, UInt32 source) {}
		
		// JMP FAR mem
		public static void JMP_FAR (Memory target) {}
		
		public static void JMP_FAR (string label) {}
		
		// JMP FAR mem32
		public static void JMP_FAR (DWordMemory target) {}
		
		// JMP mem16
		public static void JMP (WordMemory target) {}
		
		// JMP mem32
		public static void JMP (DWordMemory target) {}
		
		// JMP rmreg16
		public static void JMP (R16Type target) {}
		
		// JMP rmreg32
		public static void JMP (R32Type target) {}
		
		// JNA imm8
		public static void JNA (Byte target) {}
		
		public static void JNA (string label) {}
		
		// JNA NEAR imm
		public static void JNA (UInt32 target) {}
		
		// JNAE imm8
		public static void JNAE (Byte target) {}
		
		public static void JNAE (string label) {}
		
		// JNAE NEAR imm
		public static void JNAE (UInt32 target) {}
		
		// JNB imm8
		public static void JNB (Byte target) {}
		
		public static void JNB (string label) {}
		
		// JNB NEAR imm
		public static void JNB (UInt32 target) {}
		
		// JNBE imm8
		public static void JNBE (Byte target) {}
		
		public static void JNBE (string label) {}
		
		// JNBE NEAR imm
		public static void JNBE (UInt32 target) {}
		
		// JNC imm8
		public static void JNC (Byte target) {}
		
		public static void JNC (string label) {}
		
		// JNC NEAR imm
		public static void JNC (UInt32 target) {}
		
		// JNE imm8
		public static void JNE (Byte target) {}
		
		public static void JNE (string label) {}
		
		// JNE NEAR imm
		public static void JNE (UInt32 target) {}
		
		// JNG imm8
		public static void JNG (Byte target) {}
		
		public static void JNG (string label) {}
		
		// JNG NEAR imm
		public static void JNG (UInt32 target) {}
		
		// JNGE imm8
		public static void JNGE (Byte target) {}
		
		public static void JNGE (string label) {}
		
		// JNGE NEAR imm
		public static void JNGE (UInt32 target) {}
		
		// JNL imm8
		public static void JNL (Byte target) {}
		
		public static void JNL (string label) {}
		
		// JNL NEAR imm
		public static void JNL (UInt32 target) {}
		
		// JNLE imm8
		public static void JNLE (Byte target) {}
		
		public static void JNLE (string label) {}
		
		// JNLE NEAR imm
		public static void JNLE (UInt32 target) {}
		
		// JNO imm8
		public static void JNO (Byte target) {}
		
		public static void JNO (string label) {}
		
		// JNO NEAR imm
		public static void JNO (UInt32 target) {}
		
		// JNP imm8
		public static void JNP (Byte target) {}
		
		public static void JNP (string label) {}
		
		// JNP NEAR imm
		public static void JNP (UInt32 target) {}
		
		// JNS imm8
		public static void JNS (Byte target) {}
		
		public static void JNS (string label) {}
		
		// JNS NEAR imm
		public static void JNS (UInt32 target) {}
		
		// JNZ imm8
		public static void JNZ (Byte target) {}
		
		public static void JNZ (string label) {}
		
		// JNZ NEAR imm
		public static void JNZ (UInt32 target) {}
		
		// JO imm8
		public static void JO (Byte target) {}
		
		public static void JO (string label) {}
		
		// JO NEAR imm
		public static void JO (UInt32 target) {}
		
		// JP imm8
		public static void JP (Byte target) {}
		
		public static void JP (string label) {}
		
		// JP NEAR imm
		public static void JP (UInt32 target) {}
		
		// JPE imm8
		public static void JPE (Byte target) {}
		
		public static void JPE (string label) {}
		
		// JPE NEAR imm
		public static void JPE (UInt32 target) {}
		
		// JPO imm8
		public static void JPO (Byte target) {}
		
		public static void JPO (string label) {}
		
		// JPO NEAR imm
		public static void JPO (UInt32 target) {}
		
		// JS imm8
		public static void JS (Byte target) {}
		
		public static void JS (string label) {}
		
		// JS NEAR imm
		public static void JS (UInt32 target) {}
		
		// JZ imm8
		public static void JZ (Byte target) {}
		
		public static void JZ (string label) {}
		
		// JZ NEAR imm
		public static void JZ (UInt32 target) {}
		
		// LABEL 
		public static void LABEL (string label) {}
		
		// LAHF 
		public static void LAHF () {}
		
		// LAR reg16,mem16
		public static void LAR (R16Type target, WordMemory source) {}
		
		// LAR reg32,mem32
		public static void LAR (R32Type target, DWordMemory source) {}
		
		// LAR reg16,rmreg16
		public static void LAR (R16Type target, R16Type source) {}
		
		// LAR reg32,rmreg32
		public static void LAR (R32Type target, R32Type source) {}
		
		// LDS reg16,mem
		public static void LDS (R16Type target, Memory source) {}
		
		// LDS reg32,mem
		public static void LDS (R32Type target, Memory source) {}
		
		// LEA reg16,mem
		public static void LEA (R16Type target, Memory source) {}
		
		// LEA reg32,mem
		public static void LEA (R32Type target, Memory source) {}
		
		// LEAVE 
		public static void LEAVE () {}
		
		// LES reg16,mem
		public static void LES (R16Type target, Memory source) {}
		
		// LES reg32,mem
		public static void LES (R32Type target, Memory source) {}
		
		// LFENCE 
		public static void LFENCE () {}
		
		// LFS reg16,mem
		public static void LFS (R16Type target, Memory source) {}
		
		// LFS reg32,mem
		public static void LFS (R32Type target, Memory source) {}
		
		// LGDT mem
		public static void LGDT (Memory target) {}
		
		// LGS reg16,mem
		public static void LGS (R16Type target, Memory source) {}
		
		// LGS reg32,mem
		public static void LGS (R32Type target, Memory source) {}
		
		// LIDT mem
		public static void LIDT (Memory target) {}
		
		// LLDT mem16
		public static void LLDT (WordMemory target) {}
		
		// LLDT rmreg16
		public static void LLDT (R16Type target) {}
		
		// LMSW mem16
		public static void LMSW (WordMemory target) {}
		
		// LMSW rmreg16
		public static void LMSW (R16Type target) {}
		
		// LODSB 
		public static void LODSB () {}
		
		// LODSD 
		public static void LODSD () {}
		
		// LODSW 
		public static void LODSW () {}
		
		// LOOP imm8
		public static void LOOP (Byte target) {}
		
		// LOOPE imm8
		public static void LOOPE (Byte target) {}
		
		// LOOPNE imm8
		public static void LOOPNE (Byte target) {}
		
		// LOOPNZ imm8
		public static void LOOPNZ (Byte target) {}
		
		// LOOPZ imm8
		public static void LOOPZ (Byte target) {}
		
		// LSL reg16,mem16
		public static void LSL (R16Type target, WordMemory source) {}
		
		// LSL reg32,mem32
		public static void LSL (R32Type target, DWordMemory source) {}
		
		// LSL reg16,rmreg16
		public static void LSL (R16Type target, R16Type source) {}
		
		// LSL reg32,rmreg32
		public static void LSL (R32Type target, R32Type source) {}
		
		// LSS reg16,mem
		public static void LSS (R16Type target, Memory source) {}
		
		// LSS reg32,mem
		public static void LSS (R32Type target, Memory source) {}
		
		// LTR mem16
		public static void LTR (WordMemory target) {}
		
		// LTR rmreg16
		public static void LTR (R16Type target) {}
		
		// MFENCE 
		public static void MFENCE () {}
		
		// MOV mem8,reg8
		public static void MOV (ByteMemory target, R8Type source) {}
		
		// MOV mem16,reg16
		public static void MOV (WordMemory target, R16Type source) {}
		
		// MOV mem32,reg32
		public static void MOV (DWordMemory target, R32Type source) {}
		
		// MOV reg8,mem8
		public static void MOV (R8Type target, ByteMemory source) {}
		
		// MOV reg16,mem16
		public static void MOV (R16Type target, WordMemory source) {}
		
		// MOV reg32,mem32
		public static void MOV (R32Type target, DWordMemory source) {}
		
		// MOV reg8,imm8
		public static void MOV (R8Type target, Byte source) {}
		
		// MOV reg16,imm16
		public static void MOV (R16Type target, UInt16 source) {}
		
		// MOV reg32,imm32
		public static void MOV (R32Type target, UInt32 source) {}
		
		// MOV mem8,imm8
		public static void MOV (ByteMemory target, Byte source) {}
		
		// MOV mem16,imm16
		public static void MOV (WordMemory target, UInt16 source) {}
		
		// MOV mem32,imm32
		public static void MOV (DWordMemory target, UInt32 source) {}
		
		// MOV AL,memoffs8
		public static void MOV_AL (byte source) {}
		
		// MOV AX,memoffs16
		public static void MOV_AX (UInt16 source) {}
		
		// MOV EAX,memoffs32
		public static void MOV_EAX (UInt32 source) {}
		
		// MOV memoffs8,AL
		public static void MOV__AL (byte target) {}
		
		// MOV memoffs16,AX
		public static void MOV__AX (UInt16 target) {}
		
		// MOV memoffs32,EAX
		public static void MOV__EAX (UInt32 target) {}
		
		// MOV mem16,segreg
		public static void MOV (WordMemory target, SegType source) {}
		
		// MOV mem32,segreg
		public static void MOV (DWordMemory target, SegType source) {}
		
		// MOV segreg,mem16
		public static void MOV (SegType target, WordMemory source) {}
		
		// MOV segreg,mem32
		public static void MOV (SegType target, DWordMemory source) {}
		
		// MOV reg32,CR0/2/3/4
		public static void MOV (R32Type target, CRType source) {}
		
		// MOV reg32,DR0/1/2/3/6/7
		public static void MOV (R32Type target, DRType source) {}
		
		// MOV reg32,TR3/4/5/6/7
		public static void MOV (R32Type target, TRType source) {}
		
		// MOV CR0/2/3/4,reg32
		public static void MOV (CRType target, R32Type source) {}
		
		// MOV DR0/1/2/3/6/7,reg32
		public static void MOV (DRType target, R32Type source) {}
		
		// MOV TR3/4/5/6/7,reg32
		public static void MOV (TRType target, R32Type source) {}
		
		// MOV rmreg8,reg8
		public static void MOV (R8Type target, R8Type source) {}
		
		// MOV rmreg16,reg16
		public static void MOV (R16Type target, R16Type source) {}
		
		// MOV rmreg32,reg32
		public static void MOV (R32Type target, R32Type source) {}
		
		// MOV rmreg16,segreg
		public static void MOV (R16Type target, SegType source) {}
		
		// MOV rmreg32,segreg
		public static void MOV (R32Type target, SegType source) {}
		
		// MOV segreg,rmreg16
		public static void MOV (SegType target, R16Type source) {}
		
		// MOV segreg,rmreg32
		public static void MOV (SegType target, R32Type source) {}
		
		// MOV 
		public static void MOV (R16Type target, string label) {}
		
		// MOV 
		public static void MOV (R32Type target, string label) {}
		
		// MOVSB 
		public static void MOVSB () {}
		
		// MOVSD 
		public static void MOVSD () {}
		
		// MOVSW 
		public static void MOVSW () {}
		
		// MOVSX reg16,mem8
		public static void MOVSX (R16Type target, ByteMemory source) {}
		
		// MOVSX reg32,mem8
		public static void MOVSX (R32Type target, ByteMemory source) {}
		
		// MOVSX reg32,mem16
		public static void MOVSX (R32Type target, WordMemory source) {}
		
		// MOVSX reg16,rmreg8
		public static void MOVSX (R16Type target, R8Type source) {}
		
		// MOVSX reg32,rmreg8
		public static void MOVSX (R32Type target, R8Type source) {}
		
		// MOVSX reg32,rmreg16
		public static void MOVSX (R32Type target, R16Type source) {}
		
		// MOVZX reg16,mem8
		public static void MOVZX (R16Type target, ByteMemory source) {}
		
		// MOVZX reg32,mem8
		public static void MOVZX (R32Type target, ByteMemory source) {}
		
		// MOVZX reg32,mem16
		public static void MOVZX (R32Type target, WordMemory source) {}
		
		// MOVZX reg16,rmreg8
		public static void MOVZX (R16Type target, R8Type source) {}
		
		// MOVZX reg32,rmreg8
		public static void MOVZX (R32Type target, R8Type source) {}
		
		// MOVZX reg32,rmreg16
		public static void MOVZX (R32Type target, R16Type source) {}
		
		// MUL mem8
		public static void MUL (ByteMemory target) {}
		
		// MUL mem16
		public static void MUL (WordMemory target) {}
		
		// MUL mem32
		public static void MUL (DWordMemory target) {}
		
		// MUL rmreg8
		public static void MUL (R8Type target) {}
		
		// MUL rmreg16
		public static void MUL (R16Type target) {}
		
		// MUL rmreg32
		public static void MUL (R32Type target) {}
		
		// NEG mem8
		public static void NEG (ByteMemory target) {}
		
		// NEG mem16
		public static void NEG (WordMemory target) {}
		
		// NEG mem32
		public static void NEG (DWordMemory target) {}
		
		// NEG rmreg8
		public static void NEG (R8Type target) {}
		
		// NEG rmreg16
		public static void NEG (R16Type target) {}
		
		// NEG rmreg32
		public static void NEG (R32Type target) {}
		
		// NOP 
		public static void NOP () {}
		
		// NOT mem8
		public static void NOT (ByteMemory target) {}
		
		// NOT mem16
		public static void NOT (WordMemory target) {}
		
		// NOT mem32
		public static void NOT (DWordMemory target) {}
		
		// NOT rmreg8
		public static void NOT (R8Type target) {}
		
		// NOT rmreg16
		public static void NOT (R16Type target) {}
		
		// NOT rmreg32
		public static void NOT (R32Type target) {}
		
		// OFFSET 
		public static void OFFSET (UInt32 value) {}
		
		// OR mem8,reg8
		public static void OR (ByteMemory target, R8Type source) {}
		
		// OR mem16,reg16
		public static void OR (WordMemory target, R16Type source) {}
		
		// OR mem32,reg32
		public static void OR (DWordMemory target, R32Type source) {}
		
		// OR reg8,mem8
		public static void OR (R8Type target, ByteMemory source) {}
		
		// OR reg16,mem16
		public static void OR (R16Type target, WordMemory source) {}
		
		// OR reg32,mem32
		public static void OR (R32Type target, DWordMemory source) {}
		
		// OR mem8,imm8
		public static void OR (ByteMemory target, Byte source) {}
		
		// OR mem16,imm16
		public static void OR (WordMemory target, UInt16 source) {}
		
		// OR mem32,imm32
		public static void OR (DWordMemory target, UInt32 source) {}
		
		// OR mem16,imm8
		public static void OR (WordMemory target, Byte source) {}
		
		// OR mem32,imm8
		public static void OR (DWordMemory target, Byte source) {}
		
		// OR rmreg8,reg8
		public static void OR (R8Type target, R8Type source) {}
		
		// OR rmreg16,reg16
		public static void OR (R16Type target, R16Type source) {}
		
		// OR rmreg32,reg32
		public static void OR (R32Type target, R32Type source) {}
		
		// OR rmreg8,imm8
		public static void OR (R8Type target, Byte source) {}
		
		// OR rmreg16,imm16
		public static void OR (R16Type target, UInt16 source) {}
		
		// OR rmreg32,imm32
		public static void OR (R32Type target, UInt32 source) {}
		
		// OR rmreg16,imm8
		public static void OR (R16Type target, Byte source) {}
		
		// OR rmreg32,imm8
		public static void OR (R32Type target, Byte source) {}
		
		// ORG 
		public static void ORG (UInt32 value) {}
		
		// OUT imm8,AL
		public static void OUT__AL (Byte target) {}
		
		// OUT imm8,AX
		public static void OUT__AX (Byte target) {}
		
		// OUT imm8,EAX
		public static void OUT__EAX (Byte target) {}
		
		// OUT DX,AL
		public static void OUT_DX__AL () {}
		
		// OUT DX,AX
		public static void OUT_DX__AX () {}
		
		// OUT DX,EAX
		public static void OUT_DX__EAX () {}
		
		// OUTSB 
		public static void OUTSB () {}
		
		// OUTSD 
		public static void OUTSD () {}
		
		// OUTSW 
		public static void OUTSW () {}
		
		// PAUSE 
		public static void PAUSE () {}
		
		// POP reg16
		public static void POP (R16Type target) {}
		
		// POP reg32
		public static void POP (R32Type target) {}
		
		// POP mem16
		public static void POP (WordMemory target) {}
		
		// POP mem32
		public static void POP (DWordMemory target) {}
		
		// POP segreg
		public static void POP (SegType target) {}
		
		// POPA 
		public static void POPA () {}
		
		// POPAD 
		public static void POPAD () {}
		
		// POPAW 
		public static void POPAW () {}
		
		// POPF 
		public static void POPF () {}
		
		// POPFD 
		public static void POPFD () {}
		
		// POPFW 
		public static void POPFW () {}
		
		// PREFETCHNTA m8
		public static void PREFETCHNTA (Memory target) {}
		
		// PREFETCHT0 m8
		public static void PREFETCHT0 (Memory target) {}
		
		// PREFETCHT1 m8
		public static void PREFETCHT1 (Memory target) {}
		
		// PREFETCHT2 m8
		public static void PREFETCHT2 (Memory target) {}
		
		// PUSH reg16
		public static void PUSH (R16Type target) {}
		
		// PUSH reg32
		public static void PUSH (R32Type target) {}
		
		// PUSH mem16
		public static void PUSH (WordMemory target) {}
		
		// PUSH mem32
		public static void PUSH (DWordMemory target) {}
		
		// PUSH imm8
		public static void PUSH (Byte target) {}
		
		// PUSH imm16
		public static void PUSH (UInt16 target) {}
		
		// PUSH imm32
		public static void PUSH (UInt32 target) {}
		
		// PUSH segreg
		public static void PUSH (SegType target) {}
		
		// PUSHA 
		public static void PUSHA () {}
		
		// PUSHAD 
		public static void PUSHAD () {}
		
		// PUSHAW 
		public static void PUSHAW () {}
		
		// PUSHF 
		public static void PUSHF () {}
		
		// PUSHFD 
		public static void PUSHFD () {}
		
		// PUSHFW 
		public static void PUSHFW () {}
		
		// RCL mem8,CL
		public static void RCL__CL (ByteMemory target) {}
		
		// RCL mem8,imm8
		public static void RCL (ByteMemory target, Byte source) {}
		
		// RCL mem16,CL
		public static void RCL__CL (WordMemory target) {}
		
		// RCL mem16,imm8
		public static void RCL (WordMemory target, Byte source) {}
		
		// RCL mem32,CL
		public static void RCL__CL (DWordMemory target) {}
		
		// RCL mem32,imm8
		public static void RCL (DWordMemory target, Byte source) {}
		
		// RCL rmreg8,CL
		public static void RCL__CL (R8Type target) {}
		
		// RCL rmreg8,imm8
		public static void RCL (R8Type target, Byte source) {}
		
		// RCL rmreg16,CL
		public static void RCL__CL (R16Type target) {}
		
		// RCL rmreg16,imm8
		public static void RCL (R16Type target, Byte source) {}
		
		// RCL rmreg32,CL
		public static void RCL__CL (R32Type target) {}
		
		// RCL rmreg32,imm8
		public static void RCL (R32Type target, Byte source) {}
		
		// RCR mem8,CL
		public static void RCR__CL (ByteMemory target) {}
		
		// RCR mem8,imm8
		public static void RCR (ByteMemory target, Byte source) {}
		
		// RCR mem16,CL
		public static void RCR__CL (WordMemory target) {}
		
		// RCR mem16,imm8
		public static void RCR (WordMemory target, Byte source) {}
		
		// RCR mem32,CL
		public static void RCR__CL (DWordMemory target) {}
		
		// RCR mem32,imm8
		public static void RCR (DWordMemory target, Byte source) {}
		
		// RCR rmreg8,CL
		public static void RCR__CL (R8Type target) {}
		
		// RCR rmreg8,imm8
		public static void RCR (R8Type target, Byte source) {}
		
		// RCR rmreg16,CL
		public static void RCR__CL (R16Type target) {}
		
		// RCR rmreg16,imm8
		public static void RCR (R16Type target, Byte source) {}
		
		// RCR rmreg32,CL
		public static void RCR__CL (R32Type target) {}
		
		// RCR rmreg32,imm8
		public static void RCR (R32Type target, Byte source) {}
		
		// RDMSR 
		public static void RDMSR () {}
		
		// RDPMC 
		public static void RDPMC () {}
		
		// RDTSC 
		public static void RDTSC () {}
		
		// RET 
		public static void RET () {}
		
		// RET imm16
		public static void RET (UInt16 target) {}
		
		// RETF 
		public static void RETF () {}
		
		// RETF imm16
		public static void RETF (UInt16 target) {}
		
		// RETN 
		public static void RETN () {}
		
		// RETN imm16
		public static void RETN (UInt16 target) {}
		
		// ROL mem8,CL
		public static void ROL__CL (ByteMemory target) {}
		
		// ROL mem8,imm8
		public static void ROL (ByteMemory target, Byte source) {}
		
		// ROL mem16,CL
		public static void ROL__CL (WordMemory target) {}
		
		// ROL mem16,imm8
		public static void ROL (WordMemory target, Byte source) {}
		
		// ROL mem32,CL
		public static void ROL__CL (DWordMemory target) {}
		
		// ROL mem32,imm8
		public static void ROL (DWordMemory target, Byte source) {}
		
		// ROL rmreg8,CL
		public static void ROL__CL (R8Type target) {}
		
		// ROL rmreg8,imm8
		public static void ROL (R8Type target, Byte source) {}
		
		// ROL rmreg16,CL
		public static void ROL__CL (R16Type target) {}
		
		// ROL rmreg16,imm8
		public static void ROL (R16Type target, Byte source) {}
		
		// ROL rmreg32,CL
		public static void ROL__CL (R32Type target) {}
		
		// ROL rmreg32,imm8
		public static void ROL (R32Type target, Byte source) {}
		
		// ROR mem8,CL
		public static void ROR__CL (ByteMemory target) {}
		
		// ROR mem8,imm8
		public static void ROR (ByteMemory target, Byte source) {}
		
		// ROR mem16,CL
		public static void ROR__CL (WordMemory target) {}
		
		// ROR mem16,imm8
		public static void ROR (WordMemory target, Byte source) {}
		
		// ROR mem32,CL
		public static void ROR__CL (DWordMemory target) {}
		
		// ROR mem32,imm8
		public static void ROR (DWordMemory target, Byte source) {}
		
		// ROR rmreg8,CL
		public static void ROR__CL (R8Type target) {}
		
		// ROR rmreg8,imm8
		public static void ROR (R8Type target, Byte source) {}
		
		// ROR rmreg16,CL
		public static void ROR__CL (R16Type target) {}
		
		// ROR rmreg16,imm8
		public static void ROR (R16Type target, Byte source) {}
		
		// ROR rmreg32,CL
		public static void ROR__CL (R32Type target) {}
		
		// ROR rmreg32,imm8
		public static void ROR (R32Type target, Byte source) {}
		
		// RSM 
		public static void RSM () {}
		
		// SAHF 
		public static void SAHF () {}
		
		// SAL mem8,CL
		public static void SAL__CL (ByteMemory target) {}
		
		// SAL mem8,imm8
		public static void SAL (ByteMemory target, Byte source) {}
		
		// SAL mem16,CL
		public static void SAL__CL (WordMemory target) {}
		
		// SAL mem16,imm8
		public static void SAL (WordMemory target, Byte source) {}
		
		// SAL mem32,CL
		public static void SAL__CL (DWordMemory target) {}
		
		// SAL mem32,imm8
		public static void SAL (DWordMemory target, Byte source) {}
		
		// SAL rmreg8,CL
		public static void SAL__CL (R8Type target) {}
		
		// SAL rmreg8,imm8
		public static void SAL (R8Type target, Byte source) {}
		
		// SAL rmreg16,CL
		public static void SAL__CL (R16Type target) {}
		
		// SAL rmreg16,imm8
		public static void SAL (R16Type target, Byte source) {}
		
		// SAL rmreg32,CL
		public static void SAL__CL (R32Type target) {}
		
		// SAL rmreg32,imm8
		public static void SAL (R32Type target, Byte source) {}
		
		// SALC 
		public static void SALC () {}
		
		// SAR mem8,CL
		public static void SAR__CL (ByteMemory target) {}
		
		// SAR mem8,imm8
		public static void SAR (ByteMemory target, Byte source) {}
		
		// SAR mem16,CL
		public static void SAR__CL (WordMemory target) {}
		
		// SAR mem16,imm8
		public static void SAR (WordMemory target, Byte source) {}
		
		// SAR mem32,CL
		public static void SAR__CL (DWordMemory target) {}
		
		// SAR mem32,imm8
		public static void SAR (DWordMemory target, Byte source) {}
		
		// SAR rmreg8,CL
		public static void SAR__CL (R8Type target) {}
		
		// SAR rmreg8,imm8
		public static void SAR (R8Type target, Byte source) {}
		
		// SAR rmreg16,CL
		public static void SAR__CL (R16Type target) {}
		
		// SAR rmreg16,imm8
		public static void SAR (R16Type target, Byte source) {}
		
		// SAR rmreg32,CL
		public static void SAR__CL (R32Type target) {}
		
		// SAR rmreg32,imm8
		public static void SAR (R32Type target, Byte source) {}
		
		// SBB mem8,reg8
		public static void SBB (ByteMemory target, R8Type source) {}
		
		// SBB mem16,reg16
		public static void SBB (WordMemory target, R16Type source) {}
		
		// SBB mem32,reg32
		public static void SBB (DWordMemory target, R32Type source) {}
		
		// SBB reg8,mem8
		public static void SBB (R8Type target, ByteMemory source) {}
		
		// SBB reg16,mem16
		public static void SBB (R16Type target, WordMemory source) {}
		
		// SBB reg32,mem32
		public static void SBB (R32Type target, DWordMemory source) {}
		
		// SBB mem8,imm8
		public static void SBB (ByteMemory target, Byte source) {}
		
		// SBB mem16,imm16
		public static void SBB (WordMemory target, UInt16 source) {}
		
		// SBB mem32,imm32
		public static void SBB (DWordMemory target, UInt32 source) {}
		
		// SBB mem16,imm8
		public static void SBB (WordMemory target, Byte source) {}
		
		// SBB mem32,imm8
		public static void SBB (DWordMemory target, Byte source) {}
		
		// SBB rmreg8,reg8
		public static void SBB (R8Type target, R8Type source) {}
		
		// SBB rmreg16,reg16
		public static void SBB (R16Type target, R16Type source) {}
		
		// SBB rmreg32,reg32
		public static void SBB (R32Type target, R32Type source) {}
		
		// SBB rmreg8,imm8
		public static void SBB (R8Type target, Byte source) {}
		
		// SBB rmreg16,imm16
		public static void SBB (R16Type target, UInt16 source) {}
		
		// SBB rmreg32,imm32
		public static void SBB (R32Type target, UInt32 source) {}
		
		// SBB rmreg16,imm8
		public static void SBB (R16Type target, Byte source) {}
		
		// SBB rmreg32,imm8
		public static void SBB (R32Type target, Byte source) {}
		
		// SCASB 
		public static void SCASB () {}
		
		// SCASD 
		public static void SCASD () {}
		
		// SCASW 
		public static void SCASW () {}
		
		// SETA mem8
		public static void SETA (ByteMemory target) {}
		
		// SETA rmreg8
		public static void SETA (R8Type target) {}
		
		// SETAE mem8
		public static void SETAE (ByteMemory target) {}
		
		// SETAE rmreg8
		public static void SETAE (R8Type target) {}
		
		// SETB mem8
		public static void SETB (ByteMemory target) {}
		
		// SETB rmreg8
		public static void SETB (R8Type target) {}
		
		// SETBE mem8
		public static void SETBE (ByteMemory target) {}
		
		// SETBE rmreg8
		public static void SETBE (R8Type target) {}
		
		// SETC mem8
		public static void SETC (ByteMemory target) {}
		
		// SETC rmreg8
		public static void SETC (R8Type target) {}
		
		// SETE mem8
		public static void SETE (ByteMemory target) {}
		
		// SETE rmreg8
		public static void SETE (R8Type target) {}
		
		// SETG mem8
		public static void SETG (ByteMemory target) {}
		
		// SETG rmreg8
		public static void SETG (R8Type target) {}
		
		// SETGE mem8
		public static void SETGE (ByteMemory target) {}
		
		// SETGE rmreg8
		public static void SETGE (R8Type target) {}
		
		// SETL mem8
		public static void SETL (ByteMemory target) {}
		
		// SETL rmreg8
		public static void SETL (R8Type target) {}
		
		// SETLE mem8
		public static void SETLE (ByteMemory target) {}
		
		// SETLE rmreg8
		public static void SETLE (R8Type target) {}
		
		// SETNA mem8
		public static void SETNA (ByteMemory target) {}
		
		// SETNA rmreg8
		public static void SETNA (R8Type target) {}
		
		// SETNAE mem8
		public static void SETNAE (ByteMemory target) {}
		
		// SETNAE rmreg8
		public static void SETNAE (R8Type target) {}
		
		// SETNB mem8
		public static void SETNB (ByteMemory target) {}
		
		// SETNB rmreg8
		public static void SETNB (R8Type target) {}
		
		// SETNBE mem8
		public static void SETNBE (ByteMemory target) {}
		
		// SETNBE rmreg8
		public static void SETNBE (R8Type target) {}
		
		// SETNC mem8
		public static void SETNC (ByteMemory target) {}
		
		// SETNC rmreg8
		public static void SETNC (R8Type target) {}
		
		// SETNE mem8
		public static void SETNE (ByteMemory target) {}
		
		// SETNE rmreg8
		public static void SETNE (R8Type target) {}
		
		// SETNG mem8
		public static void SETNG (ByteMemory target) {}
		
		// SETNG rmreg8
		public static void SETNG (R8Type target) {}
		
		// SETNGE mem8
		public static void SETNGE (ByteMemory target) {}
		
		// SETNGE rmreg8
		public static void SETNGE (R8Type target) {}
		
		// SETNL mem8
		public static void SETNL (ByteMemory target) {}
		
		// SETNL rmreg8
		public static void SETNL (R8Type target) {}
		
		// SETNLE mem8
		public static void SETNLE (ByteMemory target) {}
		
		// SETNLE rmreg8
		public static void SETNLE (R8Type target) {}
		
		// SETNO mem8
		public static void SETNO (ByteMemory target) {}
		
		// SETNO rmreg8
		public static void SETNO (R8Type target) {}
		
		// SETNP mem8
		public static void SETNP (ByteMemory target) {}
		
		// SETNP rmreg8
		public static void SETNP (R8Type target) {}
		
		// SETNS mem8
		public static void SETNS (ByteMemory target) {}
		
		// SETNS rmreg8
		public static void SETNS (R8Type target) {}
		
		// SETNZ mem8
		public static void SETNZ (ByteMemory target) {}
		
		// SETNZ rmreg8
		public static void SETNZ (R8Type target) {}
		
		// SETO mem8
		public static void SETO (ByteMemory target) {}
		
		// SETO rmreg8
		public static void SETO (R8Type target) {}
		
		// SETP mem8
		public static void SETP (ByteMemory target) {}
		
		// SETP rmreg8
		public static void SETP (R8Type target) {}
		
		// SETPE mem8
		public static void SETPE (ByteMemory target) {}
		
		// SETPE rmreg8
		public static void SETPE (R8Type target) {}
		
		// SETPO mem8
		public static void SETPO (ByteMemory target) {}
		
		// SETPO rmreg8
		public static void SETPO (R8Type target) {}
		
		// SETS mem8
		public static void SETS (ByteMemory target) {}
		
		// SETS rmreg8
		public static void SETS (R8Type target) {}
		
		// SETZ mem8
		public static void SETZ (ByteMemory target) {}
		
		// SETZ rmreg8
		public static void SETZ (R8Type target) {}
		
		// SFENCE 
		public static void SFENCE () {}
		
		// SGDT mem
		public static void SGDT (Memory target) {}
		
		// SHL mem8,CL
		public static void SHL__CL (ByteMemory target) {}
		
		// SHL mem8,imm8
		public static void SHL (ByteMemory target, Byte source) {}
		
		// SHL mem16,CL
		public static void SHL__CL (WordMemory target) {}
		
		// SHL mem16,imm8
		public static void SHL (WordMemory target, Byte source) {}
		
		// SHL mem32,CL
		public static void SHL__CL (DWordMemory target) {}
		
		// SHL mem32,imm8
		public static void SHL (DWordMemory target, Byte source) {}
		
		// SHL rmreg8,CL
		public static void SHL__CL (R8Type target) {}
		
		// SHL rmreg8,imm8
		public static void SHL (R8Type target, Byte source) {}
		
		// SHL rmreg16,CL
		public static void SHL__CL (R16Type target) {}
		
		// SHL rmreg16,imm8
		public static void SHL (R16Type target, Byte source) {}
		
		// SHL rmreg32,CL
		public static void SHL__CL (R32Type target) {}
		
		// SHL rmreg32,imm8
		public static void SHL (R32Type target, Byte source) {}
		
		// SHLD mem16,reg16,imm8
		public static void SHLD (WordMemory target, R16Type source, Byte value) {}
		
		// SHLD mem32,reg32,imm8
		public static void SHLD (DWordMemory target, R32Type source, Byte value) {}
		
		// SHLD mem16,reg16,CL
		public static void SHLD___CL (WordMemory target, R16Type source) {}
		
		// SHLD mem32,reg32,CL
		public static void SHLD___CL (DWordMemory target, R32Type source) {}
		
		// SHLD rmreg16,reg16,imm8
		public static void SHLD (R16Type target, R16Type source, Byte value) {}
		
		// SHLD rmreg32,reg32,imm8
		public static void SHLD (R32Type target, R32Type source, Byte value) {}
		
		// SHLD rmreg16,reg16,CL
		public static void SHLD___CL (R16Type target, R16Type source) {}
		
		// SHLD rmreg32,reg32,CL
		public static void SHLD___CL (R32Type target, R32Type source) {}
		
		// SHR mem8,CL
		public static void SHR__CL (ByteMemory target) {}
		
		// SHR mem8,imm8
		public static void SHR (ByteMemory target, Byte source) {}
		
		// SHR mem16,CL
		public static void SHR__CL (WordMemory target) {}
		
		// SHR mem16,imm8
		public static void SHR (WordMemory target, Byte source) {}
		
		// SHR mem32,CL
		public static void SHR__CL (DWordMemory target) {}
		
		// SHR mem32,imm8
		public static void SHR (DWordMemory target, Byte source) {}
		
		// SHR rmreg8,CL
		public static void SHR__CL (R8Type target) {}
		
		// SHR rmreg8,imm8
		public static void SHR (R8Type target, Byte source) {}
		
		// SHR rmreg16,CL
		public static void SHR__CL (R16Type target) {}
		
		// SHR rmreg16,imm8
		public static void SHR (R16Type target, Byte source) {}
		
		// SHR rmreg32,CL
		public static void SHR__CL (R32Type target) {}
		
		// SHR rmreg32,imm8
		public static void SHR (R32Type target, Byte source) {}
		
		// SHRD mem16,reg16,imm8
		public static void SHRD (WordMemory target, R16Type source, Byte value) {}
		
		// SHRD mem32,reg32,imm8
		public static void SHRD (DWordMemory target, R32Type source, Byte value) {}
		
		// SHRD mem16,reg16,CL
		public static void SHRD___CL (WordMemory target, R16Type source) {}
		
		// SHRD mem32,reg32,CL
		public static void SHRD___CL (DWordMemory target, R32Type source) {}
		
		// SHRD rmreg16,reg16,imm8
		public static void SHRD (R16Type target, R16Type source, Byte value) {}
		
		// SHRD rmreg32,reg32,imm8
		public static void SHRD (R32Type target, R32Type source, Byte value) {}
		
		// SHRD rmreg16,reg16,CL
		public static void SHRD___CL (R16Type target, R16Type source) {}
		
		// SHRD rmreg32,reg32,CL
		public static void SHRD___CL (R32Type target, R32Type source) {}
		
		// SIDT mem
		public static void SIDT (Memory target) {}
		
		// SLDT mem16
		public static void SLDT (WordMemory target) {}
		
		// SLDT rmreg16
		public static void SLDT (R16Type target) {}
		
		// SMSW mem16
		public static void SMSW (WordMemory target) {}
		
		// SMSW rmreg16
		public static void SMSW (R16Type target) {}
		
		// STC 
		public static void STC () {}
		
		// STD 
		public static void STD () {}
		
		// STI 
		public static void STI () {}
		
		// STOSB 
		public static void STOSB () {}
		
		// STOSD 
		public static void STOSD () {}
		
		// STOSW 
		public static void STOSW () {}
		
		// STR mem16
		public static void STR (WordMemory target) {}
		
		// STR rmreg16
		public static void STR (R16Type target) {}
		
		// SUB mem8,reg8
		public static void SUB (ByteMemory target, R8Type source) {}
		
		// SUB mem16,reg16
		public static void SUB (WordMemory target, R16Type source) {}
		
		// SUB mem32,reg32
		public static void SUB (DWordMemory target, R32Type source) {}
		
		// SUB reg8,mem8
		public static void SUB (R8Type target, ByteMemory source) {}
		
		// SUB reg16,mem16
		public static void SUB (R16Type target, WordMemory source) {}
		
		// SUB reg32,mem32
		public static void SUB (R32Type target, DWordMemory source) {}
		
		// SUB mem8,imm8
		public static void SUB (ByteMemory target, Byte source) {}
		
		// SUB mem16,imm16
		public static void SUB (WordMemory target, UInt16 source) {}
		
		// SUB mem32,imm32
		public static void SUB (DWordMemory target, UInt32 source) {}
		
		// SUB mem16,imm8
		public static void SUB (WordMemory target, Byte source) {}
		
		// SUB mem32,imm8
		public static void SUB (DWordMemory target, Byte source) {}
		
		// SUB rmreg8,reg8
		public static void SUB (R8Type target, R8Type source) {}
		
		// SUB rmreg16,reg16
		public static void SUB (R16Type target, R16Type source) {}
		
		// SUB rmreg32,reg32
		public static void SUB (R32Type target, R32Type source) {}
		
		// SUB rmreg8,imm8
		public static void SUB (R8Type target, Byte source) {}
		
		// SUB rmreg16,imm16
		public static void SUB (R16Type target, UInt16 source) {}
		
		// SUB rmreg32,imm32
		public static void SUB (R32Type target, UInt32 source) {}
		
		// SUB rmreg16,imm8
		public static void SUB (R16Type target, Byte source) {}
		
		// SUB rmreg32,imm8
		public static void SUB (R32Type target, Byte source) {}
		
		// SYSCALL 
		public static void SYSCALL () {}
		
		// SYSENTER 
		public static void SYSENTER () {}
		
		// SYSEXIT 
		public static void SYSEXIT () {}
		
		// SYSRET 
		public static void SYSRET () {}
		
		// TEST mem8,reg8
		public static void TEST (ByteMemory target, R8Type source) {}
		
		// TEST mem16,reg16
		public static void TEST (WordMemory target, R16Type source) {}
		
		// TEST mem32,reg32
		public static void TEST (DWordMemory target, R32Type source) {}
		
		// TEST mem8,imm8
		public static void TEST (ByteMemory target, Byte source) {}
		
		// TEST mem16,imm16
		public static void TEST (WordMemory target, UInt16 source) {}
		
		// TEST mem32,imm32
		public static void TEST (DWordMemory target, UInt32 source) {}
		
		// TEST rmreg8,reg8
		public static void TEST (R8Type target, R8Type source) {}
		
		// TEST rmreg16,reg16
		public static void TEST (R16Type target, R16Type source) {}
		
		// TEST rmreg32,reg32
		public static void TEST (R32Type target, R32Type source) {}
		
		// TEST rmreg8,imm8
		public static void TEST (R8Type target, Byte source) {}
		
		// TEST rmreg16,imm16
		public static void TEST (R16Type target, UInt16 source) {}
		
		// TEST rmreg32,imm32
		public static void TEST (R32Type target, UInt32 source) {}
		
		// VERR mem16
		public static void VERR (WordMemory target) {}
		
		// VERR rmreg16
		public static void VERR (R16Type target) {}
		
		// VERW mem16
		public static void VERW (WordMemory target) {}
		
		// VERW rmreg16
		public static void VERW (R16Type target) {}
		
		// WAIT 
		public static void WAIT () {}
		
		// WBINVD 
		public static void WBINVD () {}
		
		// WRMSR 
		public static void WRMSR () {}
		
		// XADD mem8,reg8
		public static void XADD (ByteMemory target, R8Type source) {}
		
		// XADD mem16,reg16
		public static void XADD (WordMemory target, R16Type source) {}
		
		// XADD mem32,reg32
		public static void XADD (DWordMemory target, R32Type source) {}
		
		// XADD rmreg8,reg8
		public static void XADD (R8Type target, R8Type source) {}
		
		// XADD rmreg16,reg16
		public static void XADD (R16Type target, R16Type source) {}
		
		// XADD rmreg32,reg32
		public static void XADD (R32Type target, R32Type source) {}
		
		// XCHG reg8,mem8
		public static void XCHG (R8Type target, ByteMemory source) {}
		
		// XCHG reg16,mem16
		public static void XCHG (R16Type target, WordMemory source) {}
		
		// XCHG reg32,mem32
		public static void XCHG (R32Type target, DWordMemory source) {}
		
		// XCHG mem8,reg8
		public static void XCHG (ByteMemory target, R8Type source) {}
		
		// XCHG mem16,reg16
		public static void XCHG (WordMemory target, R16Type source) {}
		
		// XCHG mem32,reg32
		public static void XCHG (DWordMemory target, R32Type source) {}
		
		// XCHG reg8,rmreg8
		public static void XCHG (R8Type target, R8Type source) {}
		
		// XCHG reg16,rmreg16
		public static void XCHG (R16Type target, R16Type source) {}
		
		// XCHG reg32,rmreg32
		public static void XCHG (R32Type target, R32Type source) {}
		
		// XLAT 
		public static void XLAT () {}
		
		// XLATB 
		public static void XLATB () {}
		
		// XOR mem8,reg8
		public static void XOR (ByteMemory target, R8Type source) {}
		
		// XOR mem16,reg16
		public static void XOR (WordMemory target, R16Type source) {}
		
		// XOR mem32,reg32
		public static void XOR (DWordMemory target, R32Type source) {}
		
		// XOR reg8,mem8
		public static void XOR (R8Type target, ByteMemory source) {}
		
		// XOR reg16,mem16
		public static void XOR (R16Type target, WordMemory source) {}
		
		// XOR reg32,mem32
		public static void XOR (R32Type target, DWordMemory source) {}
		
		// XOR mem8,imm8
		public static void XOR (ByteMemory target, Byte source) {}
		
		// XOR mem16,imm16
		public static void XOR (WordMemory target, UInt16 source) {}
		
		// XOR mem32,imm32
		public static void XOR (DWordMemory target, UInt32 source) {}
		
		// XOR mem16,imm8
		public static void XOR (WordMemory target, Byte source) {}
		
		// XOR mem32,imm8
		public static void XOR (DWordMemory target, Byte source) {}
		
		// XOR rmreg8,reg8
		public static void XOR (R8Type target, R8Type source) {}
		
		// XOR rmreg16,reg16
		public static void XOR (R16Type target, R16Type source) {}
		
		// XOR rmreg32,reg32
		public static void XOR (R32Type target, R32Type source) {}
		
		// XOR rmreg8,imm8
		public static void XOR (R8Type target, Byte source) {}
		
		// XOR rmreg16,imm16
		public static void XOR (R16Type target, UInt16 source) {}
		
		// XOR rmreg32,imm32
		public static void XOR (R32Type target, UInt32 source) {}
		
		// XOR rmreg16,imm8
		public static void XOR (R16Type target, Byte source) {}
		
		// XOR rmreg32,imm8
		public static void XOR (R32Type target, Byte source) {}
	}
	
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
	
	public partial class Assembly
	{
		private void GetAssemblyInstruction(SharpOS.AOT.IR.Operands.Call method, string parameterTypes)
		{
			switch(method.Method.Name)
			{
				case "AAA":
					switch(parameterTypes)
					{
						case "AAA":
							this.AAA();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "AAD":
					switch(parameterTypes)
					{
						case "AAD":
							this.AAD();
							break;
						
						case "AAD Byte":
							this.AAD(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "AAM":
					switch(parameterTypes)
					{
						case "AAM":
							this.AAM();
							break;
						
						case "AAM Byte":
							this.AAM(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "AAS":
					switch(parameterTypes)
					{
						case "AAS":
							this.AAS();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "ADC":
					switch(parameterTypes)
					{
						case "ADC ByteMemory Byte":
							this.ADC(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ADC ByteMemory R8Type":
							this.ADC(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ADC DWordMemory Byte":
							this.ADC(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ADC DWordMemory R32Type":
							this.ADC(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ADC DWordMemory UInt32":
							this.ADC(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ADC R16Type Byte":
							this.ADC(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ADC R16Type R16Type":
							this.ADC(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ADC R16Type UInt16":
							this.ADC(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ADC R16Type WordMemory":
							this.ADC(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "ADC R32Type Byte":
							this.ADC(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ADC R32Type DWordMemory":
							this.ADC(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "ADC R32Type R32Type":
							this.ADC(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ADC R32Type UInt32":
							this.ADC(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ADC R8Type Byte":
							this.ADC(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ADC R8Type ByteMemory":
							this.ADC(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetByteMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "ADC R8Type R8Type":
							this.ADC(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ADC WordMemory Byte":
							this.ADC(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ADC WordMemory R16Type":
							this.ADC(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ADC WordMemory UInt16":
							this.ADC(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "ADD":
					switch(parameterTypes)
					{
						case "ADD ByteMemory Byte":
							this.ADD(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ADD ByteMemory R8Type":
							this.ADD(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ADD DWordMemory Byte":
							this.ADD(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ADD DWordMemory R32Type":
							this.ADD(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ADD DWordMemory UInt32":
							this.ADD(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ADD R16Type Byte":
							this.ADD(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ADD R16Type R16Type":
							this.ADD(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ADD R16Type UInt16":
							this.ADD(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ADD R16Type WordMemory":
							this.ADD(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "ADD R32Type Byte":
							this.ADD(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ADD R32Type DWordMemory":
							this.ADD(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "ADD R32Type R32Type":
							this.ADD(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ADD R32Type UInt32":
							this.ADD(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ADD R8Type Byte":
							this.ADD(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ADD R8Type ByteMemory":
							this.ADD(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetByteMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "ADD R8Type R8Type":
							this.ADD(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ADD WordMemory Byte":
							this.ADD(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ADD WordMemory R16Type":
							this.ADD(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ADD WordMemory UInt16":
							this.ADD(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "AND":
					switch(parameterTypes)
					{
						case "AND ByteMemory Byte":
							this.AND(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "AND ByteMemory R8Type":
							this.AND(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "AND DWordMemory Byte":
							this.AND(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "AND DWordMemory R32Type":
							this.AND(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "AND DWordMemory UInt32":
							this.AND(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "AND R16Type Byte":
							this.AND(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "AND R16Type R16Type":
							this.AND(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "AND R16Type UInt16":
							this.AND(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "AND R16Type WordMemory":
							this.AND(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "AND R32Type Byte":
							this.AND(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "AND R32Type DWordMemory":
							this.AND(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "AND R32Type R32Type":
							this.AND(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "AND R32Type UInt32":
							this.AND(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "AND R8Type Byte":
							this.AND(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "AND R8Type ByteMemory":
							this.AND(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetByteMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "AND R8Type R8Type":
							this.AND(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "AND WordMemory Byte":
							this.AND(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "AND WordMemory R16Type":
							this.AND(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "AND WordMemory UInt16":
							this.AND(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "ARPL":
					switch(parameterTypes)
					{
						case "ARPL R16Type R16Type":
							this.ARPL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ARPL WordMemory R16Type":
							this.ARPL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "BITS32":
					switch(parameterTypes)
					{
						case "BITS32 Boolean":
							this.BITS32(Convert.ToBoolean((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "BOUND":
					switch(parameterTypes)
					{
						case "BOUND R16Type Memory":
							this.BOUND(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "BOUND R32Type Memory":
							this.BOUND(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "BSF":
					switch(parameterTypes)
					{
						case "BSF R16Type R16Type":
							this.BSF(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "BSF R16Type WordMemory":
							this.BSF(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "BSF R32Type DWordMemory":
							this.BSF(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "BSF R32Type R32Type":
							this.BSF(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "BSR":
					switch(parameterTypes)
					{
						case "BSR R16Type R16Type":
							this.BSR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "BSR R16Type WordMemory":
							this.BSR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "BSR R32Type DWordMemory":
							this.BSR(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "BSR R32Type R32Type":
							this.BSR(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "BSWAP":
					switch(parameterTypes)
					{
						case "BSWAP R32Type":
							this.BSWAP(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "BT":
					switch(parameterTypes)
					{
						case "BT DWordMemory Byte":
							this.BT(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "BT DWordMemory R32Type":
							this.BT(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "BT R16Type Byte":
							this.BT(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "BT R16Type R16Type":
							this.BT(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "BT R32Type Byte":
							this.BT(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "BT R32Type R32Type":
							this.BT(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "BT WordMemory Byte":
							this.BT(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "BT WordMemory R16Type":
							this.BT(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "BTC":
					switch(parameterTypes)
					{
						case "BTC DWordMemory Byte":
							this.BTC(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "BTC DWordMemory R32Type":
							this.BTC(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "BTC R16Type Byte":
							this.BTC(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "BTC R16Type R16Type":
							this.BTC(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "BTC R32Type Byte":
							this.BTC(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "BTC R32Type R32Type":
							this.BTC(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "BTC WordMemory Byte":
							this.BTC(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "BTC WordMemory R16Type":
							this.BTC(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "BTR":
					switch(parameterTypes)
					{
						case "BTR DWordMemory Byte":
							this.BTR(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "BTR DWordMemory R32Type":
							this.BTR(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "BTR R16Type Byte":
							this.BTR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "BTR R16Type R16Type":
							this.BTR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "BTR R32Type Byte":
							this.BTR(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "BTR R32Type R32Type":
							this.BTR(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "BTR WordMemory Byte":
							this.BTR(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "BTR WordMemory R16Type":
							this.BTR(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "BTS":
					switch(parameterTypes)
					{
						case "BTS DWordMemory Byte":
							this.BTS(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "BTS DWordMemory R32Type":
							this.BTS(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "BTS R16Type Byte":
							this.BTS(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "BTS R16Type R16Type":
							this.BTS(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "BTS R32Type Byte":
							this.BTS(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "BTS R32Type R32Type":
							this.BTS(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "BTS WordMemory Byte":
							this.BTS(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "BTS WordMemory R16Type":
							this.BTS(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CALL":
					switch(parameterTypes)
					{
						case "CALL DWordMemory":
							this.CALL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CALL R16Type":
							this.CALL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CALL R32Type":
							this.CALL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CALL String":
							this.CALL((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "CALL UInt16 UInt16":
							this.CALL(Convert.ToUInt16((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "CALL UInt16 UInt32":
							this.CALL(Convert.ToUInt16((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "CALL UInt32":
							this.CALL(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "CALL WordMemory":
							this.CALL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CALL_FAR":
					switch(parameterTypes)
					{
						case "CALL_FAR DWordMemory":
							this.CALL_FAR(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CALL_FAR WordMemory":
							this.CALL_FAR(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CBW":
					switch(parameterTypes)
					{
						case "CBW":
							this.CBW();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CDQ":
					switch(parameterTypes)
					{
						case "CDQ":
							this.CDQ();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CLC":
					switch(parameterTypes)
					{
						case "CLC":
							this.CLC();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CLD":
					switch(parameterTypes)
					{
						case "CLD":
							this.CLD();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CLFLUSH":
					switch(parameterTypes)
					{
						case "CLFLUSH Memory":
							this.CLFLUSH(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CLI":
					switch(parameterTypes)
					{
						case "CLI":
							this.CLI();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CLTS":
					switch(parameterTypes)
					{
						case "CLTS":
							this.CLTS();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMC":
					switch(parameterTypes)
					{
						case "CMC":
							this.CMC();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVA":
					switch(parameterTypes)
					{
						case "CMOVA R16Type R16Type":
							this.CMOVA(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVA R16Type WordMemory":
							this.CMOVA(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVA R32Type DWordMemory":
							this.CMOVA(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVA R32Type R32Type":
							this.CMOVA(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVAE":
					switch(parameterTypes)
					{
						case "CMOVAE R16Type R16Type":
							this.CMOVAE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVAE R16Type WordMemory":
							this.CMOVAE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVAE R32Type DWordMemory":
							this.CMOVAE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVAE R32Type R32Type":
							this.CMOVAE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVB":
					switch(parameterTypes)
					{
						case "CMOVB R16Type R16Type":
							this.CMOVB(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVB R16Type WordMemory":
							this.CMOVB(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVB R32Type DWordMemory":
							this.CMOVB(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVB R32Type R32Type":
							this.CMOVB(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVBE":
					switch(parameterTypes)
					{
						case "CMOVBE R16Type R16Type":
							this.CMOVBE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVBE R16Type WordMemory":
							this.CMOVBE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVBE R32Type DWordMemory":
							this.CMOVBE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVBE R32Type R32Type":
							this.CMOVBE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVC":
					switch(parameterTypes)
					{
						case "CMOVC R16Type R16Type":
							this.CMOVC(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVC R16Type WordMemory":
							this.CMOVC(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVC R32Type DWordMemory":
							this.CMOVC(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVC R32Type R32Type":
							this.CMOVC(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVE":
					switch(parameterTypes)
					{
						case "CMOVE R16Type R16Type":
							this.CMOVE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVE R16Type WordMemory":
							this.CMOVE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVE R32Type DWordMemory":
							this.CMOVE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVE R32Type R32Type":
							this.CMOVE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVG":
					switch(parameterTypes)
					{
						case "CMOVG R16Type R16Type":
							this.CMOVG(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVG R16Type WordMemory":
							this.CMOVG(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVG R32Type DWordMemory":
							this.CMOVG(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVG R32Type R32Type":
							this.CMOVG(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVGE":
					switch(parameterTypes)
					{
						case "CMOVGE R16Type R16Type":
							this.CMOVGE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVGE R16Type WordMemory":
							this.CMOVGE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVGE R32Type DWordMemory":
							this.CMOVGE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVGE R32Type R32Type":
							this.CMOVGE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVL":
					switch(parameterTypes)
					{
						case "CMOVL R16Type R16Type":
							this.CMOVL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVL R16Type WordMemory":
							this.CMOVL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVL R32Type DWordMemory":
							this.CMOVL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVL R32Type R32Type":
							this.CMOVL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVLE":
					switch(parameterTypes)
					{
						case "CMOVLE R16Type R16Type":
							this.CMOVLE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVLE R16Type WordMemory":
							this.CMOVLE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVLE R32Type DWordMemory":
							this.CMOVLE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVLE R32Type R32Type":
							this.CMOVLE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVNA":
					switch(parameterTypes)
					{
						case "CMOVNA R16Type R16Type":
							this.CMOVNA(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVNA R16Type WordMemory":
							this.CMOVNA(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNA R32Type DWordMemory":
							this.CMOVNA(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNA R32Type R32Type":
							this.CMOVNA(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVNAE":
					switch(parameterTypes)
					{
						case "CMOVNAE R16Type R16Type":
							this.CMOVNAE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVNAE R16Type WordMemory":
							this.CMOVNAE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNAE R32Type DWordMemory":
							this.CMOVNAE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNAE R32Type R32Type":
							this.CMOVNAE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVNB":
					switch(parameterTypes)
					{
						case "CMOVNB R16Type R16Type":
							this.CMOVNB(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVNB R16Type WordMemory":
							this.CMOVNB(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNB R32Type DWordMemory":
							this.CMOVNB(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNB R32Type R32Type":
							this.CMOVNB(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVNBE":
					switch(parameterTypes)
					{
						case "CMOVNBE R16Type R16Type":
							this.CMOVNBE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVNBE R16Type WordMemory":
							this.CMOVNBE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNBE R32Type DWordMemory":
							this.CMOVNBE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNBE R32Type R32Type":
							this.CMOVNBE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVNC":
					switch(parameterTypes)
					{
						case "CMOVNC R16Type R16Type":
							this.CMOVNC(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVNC R16Type WordMemory":
							this.CMOVNC(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNC R32Type DWordMemory":
							this.CMOVNC(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNC R32Type R32Type":
							this.CMOVNC(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVNE":
					switch(parameterTypes)
					{
						case "CMOVNE R16Type R16Type":
							this.CMOVNE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVNE R16Type WordMemory":
							this.CMOVNE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNE R32Type DWordMemory":
							this.CMOVNE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNE R32Type R32Type":
							this.CMOVNE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVNG":
					switch(parameterTypes)
					{
						case "CMOVNG R16Type R16Type":
							this.CMOVNG(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVNG R16Type WordMemory":
							this.CMOVNG(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNG R32Type DWordMemory":
							this.CMOVNG(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNG R32Type R32Type":
							this.CMOVNG(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVNGE":
					switch(parameterTypes)
					{
						case "CMOVNGE R16Type R16Type":
							this.CMOVNGE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVNGE R16Type WordMemory":
							this.CMOVNGE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNGE R32Type DWordMemory":
							this.CMOVNGE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNGE R32Type R32Type":
							this.CMOVNGE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVNL":
					switch(parameterTypes)
					{
						case "CMOVNL R16Type R16Type":
							this.CMOVNL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVNL R16Type WordMemory":
							this.CMOVNL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNL R32Type DWordMemory":
							this.CMOVNL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNL R32Type R32Type":
							this.CMOVNL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVNLE":
					switch(parameterTypes)
					{
						case "CMOVNLE R16Type R16Type":
							this.CMOVNLE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVNLE R16Type WordMemory":
							this.CMOVNLE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNLE R32Type DWordMemory":
							this.CMOVNLE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNLE R32Type R32Type":
							this.CMOVNLE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVNO":
					switch(parameterTypes)
					{
						case "CMOVNO R16Type R16Type":
							this.CMOVNO(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVNO R16Type WordMemory":
							this.CMOVNO(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNO R32Type DWordMemory":
							this.CMOVNO(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNO R32Type R32Type":
							this.CMOVNO(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVNP":
					switch(parameterTypes)
					{
						case "CMOVNP R16Type R16Type":
							this.CMOVNP(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVNP R16Type WordMemory":
							this.CMOVNP(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNP R32Type DWordMemory":
							this.CMOVNP(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNP R32Type R32Type":
							this.CMOVNP(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVNS":
					switch(parameterTypes)
					{
						case "CMOVNS R16Type R16Type":
							this.CMOVNS(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVNS R16Type WordMemory":
							this.CMOVNS(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNS R32Type DWordMemory":
							this.CMOVNS(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNS R32Type R32Type":
							this.CMOVNS(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVNZ":
					switch(parameterTypes)
					{
						case "CMOVNZ R16Type R16Type":
							this.CMOVNZ(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVNZ R16Type WordMemory":
							this.CMOVNZ(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNZ R32Type DWordMemory":
							this.CMOVNZ(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVNZ R32Type R32Type":
							this.CMOVNZ(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVO":
					switch(parameterTypes)
					{
						case "CMOVO R16Type R16Type":
							this.CMOVO(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVO R16Type WordMemory":
							this.CMOVO(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVO R32Type DWordMemory":
							this.CMOVO(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVO R32Type R32Type":
							this.CMOVO(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVP":
					switch(parameterTypes)
					{
						case "CMOVP R16Type R16Type":
							this.CMOVP(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVP R16Type WordMemory":
							this.CMOVP(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVP R32Type DWordMemory":
							this.CMOVP(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVP R32Type R32Type":
							this.CMOVP(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVPE":
					switch(parameterTypes)
					{
						case "CMOVPE R16Type R16Type":
							this.CMOVPE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVPE R16Type WordMemory":
							this.CMOVPE(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVPE R32Type DWordMemory":
							this.CMOVPE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVPE R32Type R32Type":
							this.CMOVPE(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVPO":
					switch(parameterTypes)
					{
						case "CMOVPO R16Type R16Type":
							this.CMOVPO(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVPO R16Type WordMemory":
							this.CMOVPO(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVPO R32Type DWordMemory":
							this.CMOVPO(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVPO R32Type R32Type":
							this.CMOVPO(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVS":
					switch(parameterTypes)
					{
						case "CMOVS R16Type R16Type":
							this.CMOVS(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVS R16Type WordMemory":
							this.CMOVS(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVS R32Type DWordMemory":
							this.CMOVS(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVS R32Type R32Type":
							this.CMOVS(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMOVZ":
					switch(parameterTypes)
					{
						case "CMOVZ R16Type R16Type":
							this.CMOVZ(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMOVZ R16Type WordMemory":
							this.CMOVZ(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVZ R32Type DWordMemory":
							this.CMOVZ(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMOVZ R32Type R32Type":
							this.CMOVZ(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMP":
					switch(parameterTypes)
					{
						case "CMP ByteMemory Byte":
							this.CMP(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "CMP ByteMemory R8Type":
							this.CMP(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMP DWordMemory Byte":
							this.CMP(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "CMP DWordMemory R32Type":
							this.CMP(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMP DWordMemory UInt32":
							this.CMP(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "CMP R16Type Byte":
							this.CMP(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "CMP R16Type R16Type":
							this.CMP(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMP R16Type UInt16":
							this.CMP(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "CMP R16Type WordMemory":
							this.CMP(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMP R32Type Byte":
							this.CMP(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "CMP R32Type DWordMemory":
							this.CMP(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMP R32Type R32Type":
							this.CMP(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMP R32Type UInt32":
							this.CMP(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "CMP R8Type Byte":
							this.CMP(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "CMP R8Type ByteMemory":
							this.CMP(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetByteMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "CMP R8Type R8Type":
							this.CMP(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMP WordMemory Byte":
							this.CMP(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "CMP WordMemory R16Type":
							this.CMP(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMP WordMemory UInt16":
							this.CMP(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMPSB":
					switch(parameterTypes)
					{
						case "CMPSB":
							this.CMPSB();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMPSD":
					switch(parameterTypes)
					{
						case "CMPSD":
							this.CMPSD();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMPSW":
					switch(parameterTypes)
					{
						case "CMPSW":
							this.CMPSW();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMPXCHG":
					switch(parameterTypes)
					{
						case "CMPXCHG ByteMemory R8Type":
							this.CMPXCHG(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMPXCHG DWordMemory R32Type":
							this.CMPXCHG(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMPXCHG R16Type R16Type":
							this.CMPXCHG(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMPXCHG R32Type R32Type":
							this.CMPXCHG(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMPXCHG R8Type R8Type":
							this.CMPXCHG(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "CMPXCHG WordMemory R16Type":
							this.CMPXCHG(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CMPXCHG8B":
					switch(parameterTypes)
					{
						case "CMPXCHG8B Memory":
							this.CMPXCHG8B(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CPUID":
					switch(parameterTypes)
					{
						case "CPUID":
							this.CPUID();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CWD":
					switch(parameterTypes)
					{
						case "CWD":
							this.CWD();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "CWDE":
					switch(parameterTypes)
					{
						case "CWDE":
							this.CWDE();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "DAA":
					switch(parameterTypes)
					{
						case "DAA":
							this.DAA();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "DAS":
					switch(parameterTypes)
					{
						case "DAS":
							this.DAS();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "DATA":
					switch(parameterTypes)
					{
						case "DATA Byte":
							this.DATA(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "DATA String":
							this.DATA((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "DATA String Byte":
							this.DATA((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString(), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "DATA String String":
							this.DATA((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString(), (method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "DATA String UInt16":
							this.DATA((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString(), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "DATA String UInt32":
							this.DATA((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString(), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "DATA UInt16":
							this.DATA(Convert.ToUInt16((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "DATA UInt32":
							this.DATA(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "DEC":
					switch(parameterTypes)
					{
						case "DEC ByteMemory":
							this.DEC(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "DEC DWordMemory":
							this.DEC(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "DEC R16Type":
							this.DEC(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "DEC R32Type":
							this.DEC(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "DEC R8Type":
							this.DEC(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "DEC WordMemory":
							this.DEC(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "DIV":
					switch(parameterTypes)
					{
						case "DIV ByteMemory":
							this.DIV(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "DIV DWordMemory":
							this.DIV(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "DIV R16Type":
							this.DIV(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "DIV R32Type":
							this.DIV(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "DIV R8Type":
							this.DIV(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "DIV WordMemory":
							this.DIV(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "EMMS":
					switch(parameterTypes)
					{
						case "EMMS":
							this.EMMS();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "ENTER":
					switch(parameterTypes)
					{
						case "ENTER UInt16 Byte":
							this.ENTER(Convert.ToUInt16((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "F2XM1":
					switch(parameterTypes)
					{
						case "F2XM1":
							this.F2XM1();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FABS":
					switch(parameterTypes)
					{
						case "FABS":
							this.FABS();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FADD":
					switch(parameterTypes)
					{
						case "FADD DWordMemory":
							this.FADD(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FADD FPType":
							this.FADD(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "FADD QWordMemory":
							this.FADD(GetQWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FADDP":
					switch(parameterTypes)
					{
						case "FADDP FPType":
							this.FADDP(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FADDP__ST0":
					switch(parameterTypes)
					{
						case "FADDP__ST0 FPType":
							this.FADDP__ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FADD_ST0":
					switch(parameterTypes)
					{
						case "FADD_ST0 FPType":
							this.FADD_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FADD__ST0":
					switch(parameterTypes)
					{
						case "FADD__ST0 FPType":
							this.FADD__ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FBLD":
					switch(parameterTypes)
					{
						case "FBLD TWordMemory":
							this.FBLD(GetTWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FBSTP":
					switch(parameterTypes)
					{
						case "FBSTP TWordMemory":
							this.FBSTP(GetTWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCHS":
					switch(parameterTypes)
					{
						case "FCHS":
							this.FCHS();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCLEX":
					switch(parameterTypes)
					{
						case "FCLEX":
							this.FCLEX();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCMOVB":
					switch(parameterTypes)
					{
						case "FCMOVB FPType":
							this.FCMOVB(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCMOVBE":
					switch(parameterTypes)
					{
						case "FCMOVBE FPType":
							this.FCMOVBE(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCMOVBE_ST0":
					switch(parameterTypes)
					{
						case "FCMOVBE_ST0 FPType":
							this.FCMOVBE_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCMOVB_ST0":
					switch(parameterTypes)
					{
						case "FCMOVB_ST0 FPType":
							this.FCMOVB_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCMOVE":
					switch(parameterTypes)
					{
						case "FCMOVE FPType":
							this.FCMOVE(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCMOVE_ST0":
					switch(parameterTypes)
					{
						case "FCMOVE_ST0 FPType":
							this.FCMOVE_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCMOVNB":
					switch(parameterTypes)
					{
						case "FCMOVNB FPType":
							this.FCMOVNB(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCMOVNBE":
					switch(parameterTypes)
					{
						case "FCMOVNBE FPType":
							this.FCMOVNBE(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCMOVNBE_ST0":
					switch(parameterTypes)
					{
						case "FCMOVNBE_ST0 FPType":
							this.FCMOVNBE_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCMOVNB_ST0":
					switch(parameterTypes)
					{
						case "FCMOVNB_ST0 FPType":
							this.FCMOVNB_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCMOVNE":
					switch(parameterTypes)
					{
						case "FCMOVNE FPType":
							this.FCMOVNE(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCMOVNE_ST0":
					switch(parameterTypes)
					{
						case "FCMOVNE_ST0 FPType":
							this.FCMOVNE_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCMOVNU":
					switch(parameterTypes)
					{
						case "FCMOVNU FPType":
							this.FCMOVNU(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCMOVNU_ST0":
					switch(parameterTypes)
					{
						case "FCMOVNU_ST0 FPType":
							this.FCMOVNU_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCMOVU":
					switch(parameterTypes)
					{
						case "FCMOVU FPType":
							this.FCMOVU(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCMOVU_ST0":
					switch(parameterTypes)
					{
						case "FCMOVU_ST0 FPType":
							this.FCMOVU_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCOM":
					switch(parameterTypes)
					{
						case "FCOM DWordMemory":
							this.FCOM(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FCOM FPType":
							this.FCOM(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "FCOM QWordMemory":
							this.FCOM(GetQWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCOMI":
					switch(parameterTypes)
					{
						case "FCOMI FPType":
							this.FCOMI(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCOMIP":
					switch(parameterTypes)
					{
						case "FCOMIP FPType":
							this.FCOMIP(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCOMIP_ST0":
					switch(parameterTypes)
					{
						case "FCOMIP_ST0 FPType":
							this.FCOMIP_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCOMI_ST0":
					switch(parameterTypes)
					{
						case "FCOMI_ST0 FPType":
							this.FCOMI_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCOMP":
					switch(parameterTypes)
					{
						case "FCOMP DWordMemory":
							this.FCOMP(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FCOMP FPType":
							this.FCOMP(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "FCOMP QWordMemory":
							this.FCOMP(GetQWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCOMPP":
					switch(parameterTypes)
					{
						case "FCOMPP":
							this.FCOMPP();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCOMP_ST0":
					switch(parameterTypes)
					{
						case "FCOMP_ST0 FPType":
							this.FCOMP_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCOM_ST0":
					switch(parameterTypes)
					{
						case "FCOM_ST0 FPType":
							this.FCOM_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FCOS":
					switch(parameterTypes)
					{
						case "FCOS":
							this.FCOS();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FDECSTP":
					switch(parameterTypes)
					{
						case "FDECSTP":
							this.FDECSTP();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FDISI":
					switch(parameterTypes)
					{
						case "FDISI":
							this.FDISI();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FDIV":
					switch(parameterTypes)
					{
						case "FDIV DWordMemory":
							this.FDIV(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FDIV FPType":
							this.FDIV(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "FDIV QWordMemory":
							this.FDIV(GetQWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FDIVP":
					switch(parameterTypes)
					{
						case "FDIVP FPType":
							this.FDIVP(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FDIVP__ST0":
					switch(parameterTypes)
					{
						case "FDIVP__ST0 FPType":
							this.FDIVP__ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FDIVR":
					switch(parameterTypes)
					{
						case "FDIVR DWordMemory":
							this.FDIVR(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FDIVR FPType":
							this.FDIVR(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "FDIVR QWordMemory":
							this.FDIVR(GetQWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FDIVRP":
					switch(parameterTypes)
					{
						case "FDIVRP FPType":
							this.FDIVRP(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FDIVRP__ST0":
					switch(parameterTypes)
					{
						case "FDIVRP__ST0 FPType":
							this.FDIVRP__ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FDIVR_ST0":
					switch(parameterTypes)
					{
						case "FDIVR_ST0 FPType":
							this.FDIVR_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FDIVR__ST0":
					switch(parameterTypes)
					{
						case "FDIVR__ST0 FPType":
							this.FDIVR__ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FDIV_ST0":
					switch(parameterTypes)
					{
						case "FDIV_ST0 FPType":
							this.FDIV_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FDIV__ST0":
					switch(parameterTypes)
					{
						case "FDIV__ST0 FPType":
							this.FDIV__ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FENI":
					switch(parameterTypes)
					{
						case "FENI":
							this.FENI();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FFREE":
					switch(parameterTypes)
					{
						case "FFREE FPType":
							this.FFREE(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FFREEP":
					switch(parameterTypes)
					{
						case "FFREEP FPType":
							this.FFREEP(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FIADD":
					switch(parameterTypes)
					{
						case "FIADD DWordMemory":
							this.FIADD(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FIADD WordMemory":
							this.FIADD(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FICOM":
					switch(parameterTypes)
					{
						case "FICOM DWordMemory":
							this.FICOM(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FICOM WordMemory":
							this.FICOM(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FICOMP":
					switch(parameterTypes)
					{
						case "FICOMP DWordMemory":
							this.FICOMP(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FICOMP WordMemory":
							this.FICOMP(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FIDIV":
					switch(parameterTypes)
					{
						case "FIDIV DWordMemory":
							this.FIDIV(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FIDIV WordMemory":
							this.FIDIV(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FIDIVR":
					switch(parameterTypes)
					{
						case "FIDIVR DWordMemory":
							this.FIDIVR(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FIDIVR WordMemory":
							this.FIDIVR(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FILD":
					switch(parameterTypes)
					{
						case "FILD DWordMemory":
							this.FILD(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FILD QWordMemory":
							this.FILD(GetQWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FILD WordMemory":
							this.FILD(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FIMUL":
					switch(parameterTypes)
					{
						case "FIMUL DWordMemory":
							this.FIMUL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FIMUL WordMemory":
							this.FIMUL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FINCSTP":
					switch(parameterTypes)
					{
						case "FINCSTP":
							this.FINCSTP();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FINIT":
					switch(parameterTypes)
					{
						case "FINIT":
							this.FINIT();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FIST":
					switch(parameterTypes)
					{
						case "FIST DWordMemory":
							this.FIST(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FIST WordMemory":
							this.FIST(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FISTP":
					switch(parameterTypes)
					{
						case "FISTP DWordMemory":
							this.FISTP(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FISTP QWordMemory":
							this.FISTP(GetQWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FISTP WordMemory":
							this.FISTP(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FISUB":
					switch(parameterTypes)
					{
						case "FISUB DWordMemory":
							this.FISUB(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FISUB WordMemory":
							this.FISUB(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FISUBR":
					switch(parameterTypes)
					{
						case "FISUBR DWordMemory":
							this.FISUBR(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FISUBR WordMemory":
							this.FISUBR(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FLD":
					switch(parameterTypes)
					{
						case "FLD DWordMemory":
							this.FLD(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FLD FPType":
							this.FLD(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "FLD QWordMemory":
							this.FLD(GetQWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FLD TWordMemory":
							this.FLD(GetTWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FLD1":
					switch(parameterTypes)
					{
						case "FLD1":
							this.FLD1();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FLDCW":
					switch(parameterTypes)
					{
						case "FLDCW WordMemory":
							this.FLDCW(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FLDENV":
					switch(parameterTypes)
					{
						case "FLDENV Memory":
							this.FLDENV(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FLDL2E":
					switch(parameterTypes)
					{
						case "FLDL2E":
							this.FLDL2E();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FLDL2T":
					switch(parameterTypes)
					{
						case "FLDL2T":
							this.FLDL2T();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FLDLG2":
					switch(parameterTypes)
					{
						case "FLDLG2":
							this.FLDLG2();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FLDLN2":
					switch(parameterTypes)
					{
						case "FLDLN2":
							this.FLDLN2();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FLDPI":
					switch(parameterTypes)
					{
						case "FLDPI":
							this.FLDPI();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FLDZ":
					switch(parameterTypes)
					{
						case "FLDZ":
							this.FLDZ();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FMUL":
					switch(parameterTypes)
					{
						case "FMUL DWordMemory":
							this.FMUL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FMUL FPType":
							this.FMUL(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "FMUL QWordMemory":
							this.FMUL(GetQWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FMULP":
					switch(parameterTypes)
					{
						case "FMULP FPType":
							this.FMULP(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FMULP__ST0":
					switch(parameterTypes)
					{
						case "FMULP__ST0 FPType":
							this.FMULP__ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FMUL_ST0":
					switch(parameterTypes)
					{
						case "FMUL_ST0 FPType":
							this.FMUL_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FMUL__ST0":
					switch(parameterTypes)
					{
						case "FMUL__ST0 FPType":
							this.FMUL__ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FNCLEX":
					switch(parameterTypes)
					{
						case "FNCLEX":
							this.FNCLEX();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FNDISI":
					switch(parameterTypes)
					{
						case "FNDISI":
							this.FNDISI();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FNENI":
					switch(parameterTypes)
					{
						case "FNENI":
							this.FNENI();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FNINIT":
					switch(parameterTypes)
					{
						case "FNINIT":
							this.FNINIT();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FNOP":
					switch(parameterTypes)
					{
						case "FNOP":
							this.FNOP();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FNSAVE":
					switch(parameterTypes)
					{
						case "FNSAVE Memory":
							this.FNSAVE(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FNSTCW":
					switch(parameterTypes)
					{
						case "FNSTCW WordMemory":
							this.FNSTCW(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FNSTENV":
					switch(parameterTypes)
					{
						case "FNSTENV Memory":
							this.FNSTENV(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FNSTSW":
					switch(parameterTypes)
					{
						case "FNSTSW WordMemory":
							this.FNSTSW(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FNSTSW_AX":
					switch(parameterTypes)
					{
						case "FNSTSW_AX":
							this.FNSTSW_AX();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FPATAN":
					switch(parameterTypes)
					{
						case "FPATAN":
							this.FPATAN();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FPREM":
					switch(parameterTypes)
					{
						case "FPREM":
							this.FPREM();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FPREM1":
					switch(parameterTypes)
					{
						case "FPREM1":
							this.FPREM1();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FPTAN":
					switch(parameterTypes)
					{
						case "FPTAN":
							this.FPTAN();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FRNDINT":
					switch(parameterTypes)
					{
						case "FRNDINT":
							this.FRNDINT();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FRSTOR":
					switch(parameterTypes)
					{
						case "FRSTOR Memory":
							this.FRSTOR(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSAVE":
					switch(parameterTypes)
					{
						case "FSAVE Memory":
							this.FSAVE(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSCALE":
					switch(parameterTypes)
					{
						case "FSCALE":
							this.FSCALE();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSETPM":
					switch(parameterTypes)
					{
						case "FSETPM":
							this.FSETPM();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSIN":
					switch(parameterTypes)
					{
						case "FSIN":
							this.FSIN();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSINCOS":
					switch(parameterTypes)
					{
						case "FSINCOS":
							this.FSINCOS();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSQRT":
					switch(parameterTypes)
					{
						case "FSQRT":
							this.FSQRT();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FST":
					switch(parameterTypes)
					{
						case "FST DWordMemory":
							this.FST(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FST FPType":
							this.FST(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "FST QWordMemory":
							this.FST(GetQWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSTCW":
					switch(parameterTypes)
					{
						case "FSTCW WordMemory":
							this.FSTCW(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSTENV":
					switch(parameterTypes)
					{
						case "FSTENV Memory":
							this.FSTENV(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSTP":
					switch(parameterTypes)
					{
						case "FSTP DWordMemory":
							this.FSTP(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FSTP FPType":
							this.FSTP(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "FSTP QWordMemory":
							this.FSTP(GetQWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FSTP TWordMemory":
							this.FSTP(GetTWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSTSW":
					switch(parameterTypes)
					{
						case "FSTSW WordMemory":
							this.FSTSW(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSTSW_AX":
					switch(parameterTypes)
					{
						case "FSTSW_AX":
							this.FSTSW_AX();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSUB":
					switch(parameterTypes)
					{
						case "FSUB DWordMemory":
							this.FSUB(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FSUB FPType":
							this.FSUB(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "FSUB QWordMemory":
							this.FSUB(GetQWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSUBP":
					switch(parameterTypes)
					{
						case "FSUBP FPType":
							this.FSUBP(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSUBP__ST0":
					switch(parameterTypes)
					{
						case "FSUBP__ST0 FPType":
							this.FSUBP__ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSUBR":
					switch(parameterTypes)
					{
						case "FSUBR DWordMemory":
							this.FSUBR(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "FSUBR FPType":
							this.FSUBR(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "FSUBR QWordMemory":
							this.FSUBR(GetQWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSUBRP":
					switch(parameterTypes)
					{
						case "FSUBRP FPType":
							this.FSUBRP(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSUBRP__ST0":
					switch(parameterTypes)
					{
						case "FSUBRP__ST0 FPType":
							this.FSUBRP__ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSUBR_ST0":
					switch(parameterTypes)
					{
						case "FSUBR_ST0 FPType":
							this.FSUBR_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSUBR__ST0":
					switch(parameterTypes)
					{
						case "FSUBR__ST0 FPType":
							this.FSUBR__ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSUB_ST0":
					switch(parameterTypes)
					{
						case "FSUB_ST0 FPType":
							this.FSUB_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FSUB__ST0":
					switch(parameterTypes)
					{
						case "FSUB__ST0 FPType":
							this.FSUB__ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FTST":
					switch(parameterTypes)
					{
						case "FTST":
							this.FTST();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FUCOM":
					switch(parameterTypes)
					{
						case "FUCOM FPType":
							this.FUCOM(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FUCOMI":
					switch(parameterTypes)
					{
						case "FUCOMI FPType":
							this.FUCOMI(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FUCOMIP":
					switch(parameterTypes)
					{
						case "FUCOMIP FPType":
							this.FUCOMIP(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FUCOMIP_ST0":
					switch(parameterTypes)
					{
						case "FUCOMIP_ST0 FPType":
							this.FUCOMIP_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FUCOMI_ST0":
					switch(parameterTypes)
					{
						case "FUCOMI_ST0 FPType":
							this.FUCOMI_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FUCOMP":
					switch(parameterTypes)
					{
						case "FUCOMP FPType":
							this.FUCOMP(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FUCOMPP":
					switch(parameterTypes)
					{
						case "FUCOMPP":
							this.FUCOMPP();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FUCOMP_ST0":
					switch(parameterTypes)
					{
						case "FUCOMP_ST0 FPType":
							this.FUCOMP_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FUCOM_ST0":
					switch(parameterTypes)
					{
						case "FUCOM_ST0 FPType":
							this.FUCOM_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FWAIT":
					switch(parameterTypes)
					{
						case "FWAIT":
							this.FWAIT();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FXAM":
					switch(parameterTypes)
					{
						case "FXAM":
							this.FXAM();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FXCH":
					switch(parameterTypes)
					{
						case "FXCH":
							this.FXCH();
							break;
						
						case "FXCH FPType":
							this.FXCH(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FXCH_ST0":
					switch(parameterTypes)
					{
						case "FXCH_ST0 FPType":
							this.FXCH_ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FXCH__ST0":
					switch(parameterTypes)
					{
						case "FXCH__ST0 FPType":
							this.FXCH__ST0(FP.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FXRSTOR":
					switch(parameterTypes)
					{
						case "FXRSTOR Memory":
							this.FXRSTOR(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FXSAVE":
					switch(parameterTypes)
					{
						case "FXSAVE Memory":
							this.FXSAVE(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FXTRACT":
					switch(parameterTypes)
					{
						case "FXTRACT":
							this.FXTRACT();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FYL2X":
					switch(parameterTypes)
					{
						case "FYL2X":
							this.FYL2X();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "FYL2XP1":
					switch(parameterTypes)
					{
						case "FYL2XP1":
							this.FYL2XP1();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "HLT":
					switch(parameterTypes)
					{
						case "HLT":
							this.HLT();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "ICEBP":
					switch(parameterTypes)
					{
						case "ICEBP":
							this.ICEBP();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "IDIV":
					switch(parameterTypes)
					{
						case "IDIV ByteMemory":
							this.IDIV(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "IDIV DWordMemory":
							this.IDIV(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "IDIV R16Type":
							this.IDIV(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "IDIV R32Type":
							this.IDIV(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "IDIV R8Type":
							this.IDIV(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "IDIV WordMemory":
							this.IDIV(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "IMUL":
					switch(parameterTypes)
					{
						case "IMUL ByteMemory":
							this.IMUL(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "IMUL DWordMemory":
							this.IMUL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "IMUL R16Type":
							this.IMUL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "IMUL R16Type Byte":
							this.IMUL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "IMUL R16Type R16Type":
							this.IMUL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "IMUL R16Type R16Type Byte":
							this.IMUL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "IMUL R16Type R16Type UInt16":
							this.IMUL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt16((method.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "IMUL R16Type UInt16":
							this.IMUL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "IMUL R16Type WordMemory":
							this.IMUL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "IMUL R16Type WordMemory Byte":
							this.IMUL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "IMUL R16Type WordMemory UInt16":
							this.IMUL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt16((method.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "IMUL R32Type":
							this.IMUL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "IMUL R32Type Byte":
							this.IMUL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "IMUL R32Type DWordMemory":
							this.IMUL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "IMUL R32Type DWordMemory Byte":
							this.IMUL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "IMUL R32Type DWordMemory UInt32":
							this.IMUL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt32((method.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "IMUL R32Type R32Type":
							this.IMUL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "IMUL R32Type R32Type Byte":
							this.IMUL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "IMUL R32Type R32Type UInt32":
							this.IMUL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt32((method.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "IMUL R32Type UInt32":
							this.IMUL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "IMUL R8Type":
							this.IMUL(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "IMUL WordMemory":
							this.IMUL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "INC":
					switch(parameterTypes)
					{
						case "INC ByteMemory":
							this.INC(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "INC DWordMemory":
							this.INC(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "INC R16Type":
							this.INC(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "INC R32Type":
							this.INC(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "INC R8Type":
							this.INC(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "INC WordMemory":
							this.INC(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "INSB":
					switch(parameterTypes)
					{
						case "INSB":
							this.INSB();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "INSD":
					switch(parameterTypes)
					{
						case "INSD":
							this.INSD();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "INSW":
					switch(parameterTypes)
					{
						case "INSW":
							this.INSW();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "INT":
					switch(parameterTypes)
					{
						case "INT Byte":
							this.INT(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "INTO":
					switch(parameterTypes)
					{
						case "INTO":
							this.INTO();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "INVD":
					switch(parameterTypes)
					{
						case "INVD":
							this.INVD();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "INVLPG":
					switch(parameterTypes)
					{
						case "INVLPG Memory":
							this.INVLPG(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "IN_AL":
					switch(parameterTypes)
					{
						case "IN_AL Byte":
							this.IN_AL(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "IN_AL__DX":
					switch(parameterTypes)
					{
						case "IN_AL__DX":
							this.IN_AL__DX();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "IN_AX":
					switch(parameterTypes)
					{
						case "IN_AX Byte":
							this.IN_AX(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "IN_AX__DX":
					switch(parameterTypes)
					{
						case "IN_AX__DX":
							this.IN_AX__DX();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "IN_EAX":
					switch(parameterTypes)
					{
						case "IN_EAX Byte":
							this.IN_EAX(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "IN_EAX__DX":
					switch(parameterTypes)
					{
						case "IN_EAX__DX":
							this.IN_EAX__DX();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "IRET":
					switch(parameterTypes)
					{
						case "IRET":
							this.IRET();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "IRETD":
					switch(parameterTypes)
					{
						case "IRETD":
							this.IRETD();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "IRETW":
					switch(parameterTypes)
					{
						case "IRETW":
							this.IRETW();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JA":
					switch(parameterTypes)
					{
						case "JA Byte":
							this.JA(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JA String":
							this.JA((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JA UInt32":
							this.JA(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JAE":
					switch(parameterTypes)
					{
						case "JAE Byte":
							this.JAE(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JAE String":
							this.JAE((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JAE UInt32":
							this.JAE(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JB":
					switch(parameterTypes)
					{
						case "JB Byte":
							this.JB(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JB String":
							this.JB((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JB UInt32":
							this.JB(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JBE":
					switch(parameterTypes)
					{
						case "JBE Byte":
							this.JBE(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JBE String":
							this.JBE((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JBE UInt32":
							this.JBE(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JC":
					switch(parameterTypes)
					{
						case "JC Byte":
							this.JC(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JC String":
							this.JC((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JC UInt32":
							this.JC(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JCXZ":
					switch(parameterTypes)
					{
						case "JCXZ Byte":
							this.JCXZ(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JE":
					switch(parameterTypes)
					{
						case "JE Byte":
							this.JE(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JE String":
							this.JE((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JE UInt32":
							this.JE(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JECXZ":
					switch(parameterTypes)
					{
						case "JECXZ Byte":
							this.JECXZ(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JG":
					switch(parameterTypes)
					{
						case "JG Byte":
							this.JG(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JG String":
							this.JG((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JG UInt32":
							this.JG(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JGE":
					switch(parameterTypes)
					{
						case "JGE Byte":
							this.JGE(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JGE String":
							this.JGE((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JGE UInt32":
							this.JGE(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JL":
					switch(parameterTypes)
					{
						case "JL Byte":
							this.JL(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JL String":
							this.JL((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JL UInt32":
							this.JL(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JLE":
					switch(parameterTypes)
					{
						case "JLE Byte":
							this.JLE(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JLE String":
							this.JLE((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JLE UInt32":
							this.JLE(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JMP":
					switch(parameterTypes)
					{
						case "JMP Byte":
							this.JMP(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JMP DWordMemory":
							this.JMP(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "JMP R16Type":
							this.JMP(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "JMP R32Type":
							this.JMP(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "JMP String":
							this.JMP((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JMP UInt16 UInt16":
							this.JMP(Convert.ToUInt16((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JMP UInt16 UInt32":
							this.JMP(Convert.ToUInt16((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JMP UInt32":
							this.JMP(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JMP WordMemory":
							this.JMP(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JMP_FAR":
					switch(parameterTypes)
					{
						case "JMP_FAR DWordMemory":
							this.JMP_FAR(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "JMP_FAR Memory":
							this.JMP_FAR(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JNA":
					switch(parameterTypes)
					{
						case "JNA Byte":
							this.JNA(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JNA String":
							this.JNA((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JNA UInt32":
							this.JNA(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JNAE":
					switch(parameterTypes)
					{
						case "JNAE Byte":
							this.JNAE(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JNAE String":
							this.JNAE((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JNAE UInt32":
							this.JNAE(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JNB":
					switch(parameterTypes)
					{
						case "JNB Byte":
							this.JNB(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JNB String":
							this.JNB((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JNB UInt32":
							this.JNB(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JNBE":
					switch(parameterTypes)
					{
						case "JNBE Byte":
							this.JNBE(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JNBE String":
							this.JNBE((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JNBE UInt32":
							this.JNBE(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JNC":
					switch(parameterTypes)
					{
						case "JNC Byte":
							this.JNC(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JNC String":
							this.JNC((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JNC UInt32":
							this.JNC(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JNE":
					switch(parameterTypes)
					{
						case "JNE Byte":
							this.JNE(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JNE String":
							this.JNE((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JNE UInt32":
							this.JNE(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JNG":
					switch(parameterTypes)
					{
						case "JNG Byte":
							this.JNG(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JNG String":
							this.JNG((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JNG UInt32":
							this.JNG(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JNGE":
					switch(parameterTypes)
					{
						case "JNGE Byte":
							this.JNGE(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JNGE String":
							this.JNGE((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JNGE UInt32":
							this.JNGE(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JNL":
					switch(parameterTypes)
					{
						case "JNL Byte":
							this.JNL(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JNL String":
							this.JNL((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JNL UInt32":
							this.JNL(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JNLE":
					switch(parameterTypes)
					{
						case "JNLE Byte":
							this.JNLE(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JNLE String":
							this.JNLE((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JNLE UInt32":
							this.JNLE(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JNO":
					switch(parameterTypes)
					{
						case "JNO Byte":
							this.JNO(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JNO String":
							this.JNO((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JNO UInt32":
							this.JNO(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JNP":
					switch(parameterTypes)
					{
						case "JNP Byte":
							this.JNP(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JNP String":
							this.JNP((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JNP UInt32":
							this.JNP(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JNS":
					switch(parameterTypes)
					{
						case "JNS Byte":
							this.JNS(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JNS String":
							this.JNS((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JNS UInt32":
							this.JNS(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JNZ":
					switch(parameterTypes)
					{
						case "JNZ Byte":
							this.JNZ(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JNZ String":
							this.JNZ((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JNZ UInt32":
							this.JNZ(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JO":
					switch(parameterTypes)
					{
						case "JO Byte":
							this.JO(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JO String":
							this.JO((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JO UInt32":
							this.JO(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JP":
					switch(parameterTypes)
					{
						case "JP Byte":
							this.JP(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JP String":
							this.JP((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JP UInt32":
							this.JP(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JPE":
					switch(parameterTypes)
					{
						case "JPE Byte":
							this.JPE(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JPE String":
							this.JPE((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JPE UInt32":
							this.JPE(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JPO":
					switch(parameterTypes)
					{
						case "JPO Byte":
							this.JPO(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JPO String":
							this.JPO((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JPO UInt32":
							this.JPO(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JS":
					switch(parameterTypes)
					{
						case "JS Byte":
							this.JS(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JS String":
							this.JS((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JS UInt32":
							this.JS(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "JZ":
					switch(parameterTypes)
					{
						case "JZ Byte":
							this.JZ(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "JZ String":
							this.JZ((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "JZ UInt32":
							this.JZ(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LABEL":
					switch(parameterTypes)
					{
						case "LABEL String":
							this.LABEL((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LAHF":
					switch(parameterTypes)
					{
						case "LAHF":
							this.LAHF();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LAR":
					switch(parameterTypes)
					{
						case "LAR R16Type R16Type":
							this.LAR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "LAR R16Type WordMemory":
							this.LAR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "LAR R32Type DWordMemory":
							this.LAR(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "LAR R32Type R32Type":
							this.LAR(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LDS":
					switch(parameterTypes)
					{
						case "LDS R16Type Memory":
							this.LDS(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "LDS R32Type Memory":
							this.LDS(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LEA":
					switch(parameterTypes)
					{
						case "LEA R16Type Memory":
							this.LEA(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "LEA R32Type Memory":
							this.LEA(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LEAVE":
					switch(parameterTypes)
					{
						case "LEAVE":
							this.LEAVE();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LES":
					switch(parameterTypes)
					{
						case "LES R16Type Memory":
							this.LES(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "LES R32Type Memory":
							this.LES(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LFENCE":
					switch(parameterTypes)
					{
						case "LFENCE":
							this.LFENCE();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LFS":
					switch(parameterTypes)
					{
						case "LFS R16Type Memory":
							this.LFS(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "LFS R32Type Memory":
							this.LFS(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LGDT":
					switch(parameterTypes)
					{
						case "LGDT Memory":
							this.LGDT(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LGS":
					switch(parameterTypes)
					{
						case "LGS R16Type Memory":
							this.LGS(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "LGS R32Type Memory":
							this.LGS(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LIDT":
					switch(parameterTypes)
					{
						case "LIDT Memory":
							this.LIDT(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LLDT":
					switch(parameterTypes)
					{
						case "LLDT R16Type":
							this.LLDT(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "LLDT WordMemory":
							this.LLDT(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LMSW":
					switch(parameterTypes)
					{
						case "LMSW R16Type":
							this.LMSW(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "LMSW WordMemory":
							this.LMSW(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LODSB":
					switch(parameterTypes)
					{
						case "LODSB":
							this.LODSB();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LODSD":
					switch(parameterTypes)
					{
						case "LODSD":
							this.LODSD();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LODSW":
					switch(parameterTypes)
					{
						case "LODSW":
							this.LODSW();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LOOP":
					switch(parameterTypes)
					{
						case "LOOP Byte":
							this.LOOP(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LOOPE":
					switch(parameterTypes)
					{
						case "LOOPE Byte":
							this.LOOPE(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LOOPNE":
					switch(parameterTypes)
					{
						case "LOOPNE Byte":
							this.LOOPNE(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LOOPNZ":
					switch(parameterTypes)
					{
						case "LOOPNZ Byte":
							this.LOOPNZ(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LOOPZ":
					switch(parameterTypes)
					{
						case "LOOPZ Byte":
							this.LOOPZ(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LSL":
					switch(parameterTypes)
					{
						case "LSL R16Type R16Type":
							this.LSL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "LSL R16Type WordMemory":
							this.LSL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "LSL R32Type DWordMemory":
							this.LSL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "LSL R32Type R32Type":
							this.LSL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LSS":
					switch(parameterTypes)
					{
						case "LSS R16Type Memory":
							this.LSS(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "LSS R32Type Memory":
							this.LSS(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "LTR":
					switch(parameterTypes)
					{
						case "LTR R16Type":
							this.LTR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "LTR WordMemory":
							this.LTR(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "MFENCE":
					switch(parameterTypes)
					{
						case "MFENCE":
							this.MFENCE();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "MOV":
					switch(parameterTypes)
					{
						case "MOV ByteMemory Byte":
							this.MOV(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "MOV ByteMemory R8Type":
							this.MOV(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOV CRType R32Type":
							this.MOV(CR.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOV DRType R32Type":
							this.MOV(DR.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOV DWordMemory R32Type":
							this.MOV(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOV DWordMemory SegType":
							this.MOV(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Seg.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOV DWordMemory UInt32":
							this.MOV(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "MOV R16Type R16Type":
							this.MOV(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOV R16Type SegType":
							this.MOV(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Seg.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOV R16Type String":
							this.MOV(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), (method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "MOV R16Type UInt16":
							this.MOV(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "MOV R16Type WordMemory":
							this.MOV(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "MOV R32Type CRType":
							this.MOV(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), CR.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOV R32Type DRType":
							this.MOV(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), DR.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOV R32Type DWordMemory":
							this.MOV(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "MOV R32Type R32Type":
							this.MOV(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOV R32Type SegType":
							this.MOV(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Seg.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOV R32Type String":
							this.MOV(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), (method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
							break;
						
						case "MOV R32Type TRType":
							this.MOV(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), TR.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOV R32Type UInt32":
							this.MOV(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "MOV R8Type Byte":
							this.MOV(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "MOV R8Type ByteMemory":
							this.MOV(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetByteMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "MOV R8Type R8Type":
							this.MOV(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOV SegType DWordMemory":
							this.MOV(Seg.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "MOV SegType R16Type":
							this.MOV(Seg.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOV SegType R32Type":
							this.MOV(Seg.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOV SegType WordMemory":
							this.MOV(Seg.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "MOV TRType R32Type":
							this.MOV(TR.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOV WordMemory R16Type":
							this.MOV(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOV WordMemory SegType":
							this.MOV(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Seg.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOV WordMemory UInt16":
							this.MOV(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "MOVSB":
					switch(parameterTypes)
					{
						case "MOVSB":
							this.MOVSB();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "MOVSD":
					switch(parameterTypes)
					{
						case "MOVSD":
							this.MOVSD();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "MOVSW":
					switch(parameterTypes)
					{
						case "MOVSW":
							this.MOVSW();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "MOVSX":
					switch(parameterTypes)
					{
						case "MOVSX R16Type ByteMemory":
							this.MOVSX(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetByteMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "MOVSX R16Type R8Type":
							this.MOVSX(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOVSX R32Type ByteMemory":
							this.MOVSX(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetByteMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "MOVSX R32Type R16Type":
							this.MOVSX(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOVSX R32Type R8Type":
							this.MOVSX(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOVSX R32Type WordMemory":
							this.MOVSX(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "MOVZX":
					switch(parameterTypes)
					{
						case "MOVZX R16Type ByteMemory":
							this.MOVZX(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetByteMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "MOVZX R16Type R8Type":
							this.MOVZX(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOVZX R32Type ByteMemory":
							this.MOVZX(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetByteMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "MOVZX R32Type R16Type":
							this.MOVZX(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOVZX R32Type R8Type":
							this.MOVZX(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MOVZX R32Type WordMemory":
							this.MOVZX(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "MOV_AL":
					switch(parameterTypes)
					{
						case "MOV_AL Byte":
							this.MOV_AL(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "MOV_AX":
					switch(parameterTypes)
					{
						case "MOV_AX UInt16":
							this.MOV_AX(Convert.ToUInt16((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "MOV_EAX":
					switch(parameterTypes)
					{
						case "MOV_EAX UInt32":
							this.MOV_EAX(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "MOV__AL":
					switch(parameterTypes)
					{
						case "MOV__AL Byte":
							this.MOV__AL(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "MOV__AX":
					switch(parameterTypes)
					{
						case "MOV__AX UInt16":
							this.MOV__AX(Convert.ToUInt16((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "MOV__EAX":
					switch(parameterTypes)
					{
						case "MOV__EAX UInt32":
							this.MOV__EAX(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "MUL":
					switch(parameterTypes)
					{
						case "MUL ByteMemory":
							this.MUL(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "MUL DWordMemory":
							this.MUL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "MUL R16Type":
							this.MUL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MUL R32Type":
							this.MUL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MUL R8Type":
							this.MUL(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "MUL WordMemory":
							this.MUL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "NEG":
					switch(parameterTypes)
					{
						case "NEG ByteMemory":
							this.NEG(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "NEG DWordMemory":
							this.NEG(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "NEG R16Type":
							this.NEG(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "NEG R32Type":
							this.NEG(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "NEG R8Type":
							this.NEG(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "NEG WordMemory":
							this.NEG(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "NOP":
					switch(parameterTypes)
					{
						case "NOP":
							this.NOP();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "NOT":
					switch(parameterTypes)
					{
						case "NOT ByteMemory":
							this.NOT(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "NOT DWordMemory":
							this.NOT(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "NOT R16Type":
							this.NOT(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "NOT R32Type":
							this.NOT(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "NOT R8Type":
							this.NOT(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "NOT WordMemory":
							this.NOT(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "OFFSET":
					switch(parameterTypes)
					{
						case "OFFSET UInt32":
							this.OFFSET(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "OR":
					switch(parameterTypes)
					{
						case "OR ByteMemory Byte":
							this.OR(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "OR ByteMemory R8Type":
							this.OR(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "OR DWordMemory Byte":
							this.OR(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "OR DWordMemory R32Type":
							this.OR(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "OR DWordMemory UInt32":
							this.OR(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "OR R16Type Byte":
							this.OR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "OR R16Type R16Type":
							this.OR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "OR R16Type UInt16":
							this.OR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "OR R16Type WordMemory":
							this.OR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "OR R32Type Byte":
							this.OR(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "OR R32Type DWordMemory":
							this.OR(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "OR R32Type R32Type":
							this.OR(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "OR R32Type UInt32":
							this.OR(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "OR R8Type Byte":
							this.OR(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "OR R8Type ByteMemory":
							this.OR(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetByteMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "OR R8Type R8Type":
							this.OR(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "OR WordMemory Byte":
							this.OR(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "OR WordMemory R16Type":
							this.OR(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "OR WordMemory UInt16":
							this.OR(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "ORG":
					switch(parameterTypes)
					{
						case "ORG UInt32":
							this.ORG(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "OUTSB":
					switch(parameterTypes)
					{
						case "OUTSB":
							this.OUTSB();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "OUTSD":
					switch(parameterTypes)
					{
						case "OUTSD":
							this.OUTSD();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "OUTSW":
					switch(parameterTypes)
					{
						case "OUTSW":
							this.OUTSW();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "OUT_DX__AL":
					switch(parameterTypes)
					{
						case "OUT_DX__AL":
							this.OUT_DX__AL();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "OUT_DX__AX":
					switch(parameterTypes)
					{
						case "OUT_DX__AX":
							this.OUT_DX__AX();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "OUT_DX__EAX":
					switch(parameterTypes)
					{
						case "OUT_DX__EAX":
							this.OUT_DX__EAX();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "OUT__AL":
					switch(parameterTypes)
					{
						case "OUT__AL Byte":
							this.OUT__AL(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "OUT__AX":
					switch(parameterTypes)
					{
						case "OUT__AX Byte":
							this.OUT__AX(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "OUT__EAX":
					switch(parameterTypes)
					{
						case "OUT__EAX Byte":
							this.OUT__EAX(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "PAUSE":
					switch(parameterTypes)
					{
						case "PAUSE":
							this.PAUSE();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "POP":
					switch(parameterTypes)
					{
						case "POP DWordMemory":
							this.POP(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "POP R16Type":
							this.POP(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "POP R32Type":
							this.POP(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "POP SegType":
							this.POP(Seg.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "POP WordMemory":
							this.POP(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "POPA":
					switch(parameterTypes)
					{
						case "POPA":
							this.POPA();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "POPAD":
					switch(parameterTypes)
					{
						case "POPAD":
							this.POPAD();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "POPAW":
					switch(parameterTypes)
					{
						case "POPAW":
							this.POPAW();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "POPF":
					switch(parameterTypes)
					{
						case "POPF":
							this.POPF();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "POPFD":
					switch(parameterTypes)
					{
						case "POPFD":
							this.POPFD();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "POPFW":
					switch(parameterTypes)
					{
						case "POPFW":
							this.POPFW();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "PREFETCHNTA":
					switch(parameterTypes)
					{
						case "PREFETCHNTA Memory":
							this.PREFETCHNTA(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "PREFETCHT0":
					switch(parameterTypes)
					{
						case "PREFETCHT0 Memory":
							this.PREFETCHT0(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "PREFETCHT1":
					switch(parameterTypes)
					{
						case "PREFETCHT1 Memory":
							this.PREFETCHT1(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "PREFETCHT2":
					switch(parameterTypes)
					{
						case "PREFETCHT2 Memory":
							this.PREFETCHT2(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "PUSH":
					switch(parameterTypes)
					{
						case "PUSH Byte":
							this.PUSH(Convert.ToByte((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "PUSH DWordMemory":
							this.PUSH(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "PUSH R16Type":
							this.PUSH(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "PUSH R32Type":
							this.PUSH(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "PUSH SegType":
							this.PUSH(Seg.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "PUSH UInt16":
							this.PUSH(Convert.ToUInt16((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "PUSH UInt32":
							this.PUSH(Convert.ToUInt32((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "PUSH WordMemory":
							this.PUSH(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "PUSHA":
					switch(parameterTypes)
					{
						case "PUSHA":
							this.PUSHA();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "PUSHAD":
					switch(parameterTypes)
					{
						case "PUSHAD":
							this.PUSHAD();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "PUSHAW":
					switch(parameterTypes)
					{
						case "PUSHAW":
							this.PUSHAW();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "PUSHF":
					switch(parameterTypes)
					{
						case "PUSHF":
							this.PUSHF();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "PUSHFD":
					switch(parameterTypes)
					{
						case "PUSHFD":
							this.PUSHFD();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "PUSHFW":
					switch(parameterTypes)
					{
						case "PUSHFW":
							this.PUSHFW();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "RCL":
					switch(parameterTypes)
					{
						case "RCL ByteMemory Byte":
							this.RCL(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "RCL DWordMemory Byte":
							this.RCL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "RCL R16Type Byte":
							this.RCL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "RCL R32Type Byte":
							this.RCL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "RCL R8Type Byte":
							this.RCL(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "RCL WordMemory Byte":
							this.RCL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "RCL__CL":
					switch(parameterTypes)
					{
						case "RCL__CL ByteMemory":
							this.RCL__CL(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "RCL__CL DWordMemory":
							this.RCL__CL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "RCL__CL R16Type":
							this.RCL__CL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "RCL__CL R32Type":
							this.RCL__CL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "RCL__CL R8Type":
							this.RCL__CL(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "RCL__CL WordMemory":
							this.RCL__CL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "RCR":
					switch(parameterTypes)
					{
						case "RCR ByteMemory Byte":
							this.RCR(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "RCR DWordMemory Byte":
							this.RCR(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "RCR R16Type Byte":
							this.RCR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "RCR R32Type Byte":
							this.RCR(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "RCR R8Type Byte":
							this.RCR(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "RCR WordMemory Byte":
							this.RCR(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "RCR__CL":
					switch(parameterTypes)
					{
						case "RCR__CL ByteMemory":
							this.RCR__CL(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "RCR__CL DWordMemory":
							this.RCR__CL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "RCR__CL R16Type":
							this.RCR__CL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "RCR__CL R32Type":
							this.RCR__CL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "RCR__CL R8Type":
							this.RCR__CL(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "RCR__CL WordMemory":
							this.RCR__CL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "RDMSR":
					switch(parameterTypes)
					{
						case "RDMSR":
							this.RDMSR();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "RDPMC":
					switch(parameterTypes)
					{
						case "RDPMC":
							this.RDPMC();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "RDTSC":
					switch(parameterTypes)
					{
						case "RDTSC":
							this.RDTSC();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "RET":
					switch(parameterTypes)
					{
						case "RET":
							this.RET();
							break;
						
						case "RET UInt16":
							this.RET(Convert.ToUInt16((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "RETF":
					switch(parameterTypes)
					{
						case "RETF":
							this.RETF();
							break;
						
						case "RETF UInt16":
							this.RETF(Convert.ToUInt16((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "RETN":
					switch(parameterTypes)
					{
						case "RETN":
							this.RETN();
							break;
						
						case "RETN UInt16":
							this.RETN(Convert.ToUInt16((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "ROL":
					switch(parameterTypes)
					{
						case "ROL ByteMemory Byte":
							this.ROL(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ROL DWordMemory Byte":
							this.ROL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ROL R16Type Byte":
							this.ROL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ROL R32Type Byte":
							this.ROL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ROL R8Type Byte":
							this.ROL(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ROL WordMemory Byte":
							this.ROL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "ROL__CL":
					switch(parameterTypes)
					{
						case "ROL__CL ByteMemory":
							this.ROL__CL(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "ROL__CL DWordMemory":
							this.ROL__CL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "ROL__CL R16Type":
							this.ROL__CL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ROL__CL R32Type":
							this.ROL__CL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ROL__CL R8Type":
							this.ROL__CL(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ROL__CL WordMemory":
							this.ROL__CL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "ROR":
					switch(parameterTypes)
					{
						case "ROR ByteMemory Byte":
							this.ROR(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ROR DWordMemory Byte":
							this.ROR(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ROR R16Type Byte":
							this.ROR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ROR R32Type Byte":
							this.ROR(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ROR R8Type Byte":
							this.ROR(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "ROR WordMemory Byte":
							this.ROR(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "ROR__CL":
					switch(parameterTypes)
					{
						case "ROR__CL ByteMemory":
							this.ROR__CL(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "ROR__CL DWordMemory":
							this.ROR__CL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "ROR__CL R16Type":
							this.ROR__CL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ROR__CL R32Type":
							this.ROR__CL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ROR__CL R8Type":
							this.ROR__CL(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "ROR__CL WordMemory":
							this.ROR__CL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "RSM":
					switch(parameterTypes)
					{
						case "RSM":
							this.RSM();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SAHF":
					switch(parameterTypes)
					{
						case "SAHF":
							this.SAHF();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SAL":
					switch(parameterTypes)
					{
						case "SAL ByteMemory Byte":
							this.SAL(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SAL DWordMemory Byte":
							this.SAL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SAL R16Type Byte":
							this.SAL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SAL R32Type Byte":
							this.SAL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SAL R8Type Byte":
							this.SAL(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SAL WordMemory Byte":
							this.SAL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SALC":
					switch(parameterTypes)
					{
						case "SALC":
							this.SALC();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SAL__CL":
					switch(parameterTypes)
					{
						case "SAL__CL ByteMemory":
							this.SAL__CL(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SAL__CL DWordMemory":
							this.SAL__CL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SAL__CL R16Type":
							this.SAL__CL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SAL__CL R32Type":
							this.SAL__CL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SAL__CL R8Type":
							this.SAL__CL(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SAL__CL WordMemory":
							this.SAL__CL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SAR":
					switch(parameterTypes)
					{
						case "SAR ByteMemory Byte":
							this.SAR(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SAR DWordMemory Byte":
							this.SAR(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SAR R16Type Byte":
							this.SAR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SAR R32Type Byte":
							this.SAR(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SAR R8Type Byte":
							this.SAR(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SAR WordMemory Byte":
							this.SAR(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SAR__CL":
					switch(parameterTypes)
					{
						case "SAR__CL ByteMemory":
							this.SAR__CL(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SAR__CL DWordMemory":
							this.SAR__CL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SAR__CL R16Type":
							this.SAR__CL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SAR__CL R32Type":
							this.SAR__CL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SAR__CL R8Type":
							this.SAR__CL(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SAR__CL WordMemory":
							this.SAR__CL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SBB":
					switch(parameterTypes)
					{
						case "SBB ByteMemory Byte":
							this.SBB(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SBB ByteMemory R8Type":
							this.SBB(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SBB DWordMemory Byte":
							this.SBB(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SBB DWordMemory R32Type":
							this.SBB(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SBB DWordMemory UInt32":
							this.SBB(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SBB R16Type Byte":
							this.SBB(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SBB R16Type R16Type":
							this.SBB(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SBB R16Type UInt16":
							this.SBB(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SBB R16Type WordMemory":
							this.SBB(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SBB R32Type Byte":
							this.SBB(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SBB R32Type DWordMemory":
							this.SBB(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SBB R32Type R32Type":
							this.SBB(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SBB R32Type UInt32":
							this.SBB(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SBB R8Type Byte":
							this.SBB(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SBB R8Type ByteMemory":
							this.SBB(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetByteMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SBB R8Type R8Type":
							this.SBB(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SBB WordMemory Byte":
							this.SBB(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SBB WordMemory R16Type":
							this.SBB(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SBB WordMemory UInt16":
							this.SBB(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SCASB":
					switch(parameterTypes)
					{
						case "SCASB":
							this.SCASB();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SCASD":
					switch(parameterTypes)
					{
						case "SCASD":
							this.SCASD();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SCASW":
					switch(parameterTypes)
					{
						case "SCASW":
							this.SCASW();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETA":
					switch(parameterTypes)
					{
						case "SETA ByteMemory":
							this.SETA(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETA R8Type":
							this.SETA(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETAE":
					switch(parameterTypes)
					{
						case "SETAE ByteMemory":
							this.SETAE(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETAE R8Type":
							this.SETAE(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETB":
					switch(parameterTypes)
					{
						case "SETB ByteMemory":
							this.SETB(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETB R8Type":
							this.SETB(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETBE":
					switch(parameterTypes)
					{
						case "SETBE ByteMemory":
							this.SETBE(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETBE R8Type":
							this.SETBE(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETC":
					switch(parameterTypes)
					{
						case "SETC ByteMemory":
							this.SETC(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETC R8Type":
							this.SETC(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETE":
					switch(parameterTypes)
					{
						case "SETE ByteMemory":
							this.SETE(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETE R8Type":
							this.SETE(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETG":
					switch(parameterTypes)
					{
						case "SETG ByteMemory":
							this.SETG(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETG R8Type":
							this.SETG(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETGE":
					switch(parameterTypes)
					{
						case "SETGE ByteMemory":
							this.SETGE(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETGE R8Type":
							this.SETGE(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETL":
					switch(parameterTypes)
					{
						case "SETL ByteMemory":
							this.SETL(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETL R8Type":
							this.SETL(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETLE":
					switch(parameterTypes)
					{
						case "SETLE ByteMemory":
							this.SETLE(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETLE R8Type":
							this.SETLE(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETNA":
					switch(parameterTypes)
					{
						case "SETNA ByteMemory":
							this.SETNA(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETNA R8Type":
							this.SETNA(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETNAE":
					switch(parameterTypes)
					{
						case "SETNAE ByteMemory":
							this.SETNAE(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETNAE R8Type":
							this.SETNAE(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETNB":
					switch(parameterTypes)
					{
						case "SETNB ByteMemory":
							this.SETNB(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETNB R8Type":
							this.SETNB(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETNBE":
					switch(parameterTypes)
					{
						case "SETNBE ByteMemory":
							this.SETNBE(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETNBE R8Type":
							this.SETNBE(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETNC":
					switch(parameterTypes)
					{
						case "SETNC ByteMemory":
							this.SETNC(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETNC R8Type":
							this.SETNC(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETNE":
					switch(parameterTypes)
					{
						case "SETNE ByteMemory":
							this.SETNE(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETNE R8Type":
							this.SETNE(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETNG":
					switch(parameterTypes)
					{
						case "SETNG ByteMemory":
							this.SETNG(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETNG R8Type":
							this.SETNG(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETNGE":
					switch(parameterTypes)
					{
						case "SETNGE ByteMemory":
							this.SETNGE(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETNGE R8Type":
							this.SETNGE(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETNL":
					switch(parameterTypes)
					{
						case "SETNL ByteMemory":
							this.SETNL(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETNL R8Type":
							this.SETNL(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETNLE":
					switch(parameterTypes)
					{
						case "SETNLE ByteMemory":
							this.SETNLE(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETNLE R8Type":
							this.SETNLE(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETNO":
					switch(parameterTypes)
					{
						case "SETNO ByteMemory":
							this.SETNO(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETNO R8Type":
							this.SETNO(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETNP":
					switch(parameterTypes)
					{
						case "SETNP ByteMemory":
							this.SETNP(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETNP R8Type":
							this.SETNP(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETNS":
					switch(parameterTypes)
					{
						case "SETNS ByteMemory":
							this.SETNS(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETNS R8Type":
							this.SETNS(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETNZ":
					switch(parameterTypes)
					{
						case "SETNZ ByteMemory":
							this.SETNZ(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETNZ R8Type":
							this.SETNZ(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETO":
					switch(parameterTypes)
					{
						case "SETO ByteMemory":
							this.SETO(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETO R8Type":
							this.SETO(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETP":
					switch(parameterTypes)
					{
						case "SETP ByteMemory":
							this.SETP(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETP R8Type":
							this.SETP(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETPE":
					switch(parameterTypes)
					{
						case "SETPE ByteMemory":
							this.SETPE(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETPE R8Type":
							this.SETPE(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETPO":
					switch(parameterTypes)
					{
						case "SETPO ByteMemory":
							this.SETPO(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETPO R8Type":
							this.SETPO(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETS":
					switch(parameterTypes)
					{
						case "SETS ByteMemory":
							this.SETS(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETS R8Type":
							this.SETS(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SETZ":
					switch(parameterTypes)
					{
						case "SETZ ByteMemory":
							this.SETZ(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SETZ R8Type":
							this.SETZ(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SFENCE":
					switch(parameterTypes)
					{
						case "SFENCE":
							this.SFENCE();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SGDT":
					switch(parameterTypes)
					{
						case "SGDT Memory":
							this.SGDT(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SHL":
					switch(parameterTypes)
					{
						case "SHL ByteMemory Byte":
							this.SHL(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SHL DWordMemory Byte":
							this.SHL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SHL R16Type Byte":
							this.SHL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SHL R32Type Byte":
							this.SHL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SHL R8Type Byte":
							this.SHL(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SHL WordMemory Byte":
							this.SHL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SHLD":
					switch(parameterTypes)
					{
						case "SHLD DWordMemory R32Type Byte":
							this.SHLD(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SHLD R16Type R16Type Byte":
							this.SHLD(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SHLD R32Type R32Type Byte":
							this.SHLD(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SHLD WordMemory R16Type Byte":
							this.SHLD(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SHLD___CL":
					switch(parameterTypes)
					{
						case "SHLD___CL DWordMemory R32Type":
							this.SHLD___CL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SHLD___CL R16Type R16Type":
							this.SHLD___CL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SHLD___CL R32Type R32Type":
							this.SHLD___CL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SHLD___CL WordMemory R16Type":
							this.SHLD___CL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SHL__CL":
					switch(parameterTypes)
					{
						case "SHL__CL ByteMemory":
							this.SHL__CL(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SHL__CL DWordMemory":
							this.SHL__CL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SHL__CL R16Type":
							this.SHL__CL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SHL__CL R32Type":
							this.SHL__CL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SHL__CL R8Type":
							this.SHL__CL(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SHL__CL WordMemory":
							this.SHL__CL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SHR":
					switch(parameterTypes)
					{
						case "SHR ByteMemory Byte":
							this.SHR(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SHR DWordMemory Byte":
							this.SHR(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SHR R16Type Byte":
							this.SHR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SHR R32Type Byte":
							this.SHR(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SHR R8Type Byte":
							this.SHR(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SHR WordMemory Byte":
							this.SHR(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SHRD":
					switch(parameterTypes)
					{
						case "SHRD DWordMemory R32Type Byte":
							this.SHRD(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SHRD R16Type R16Type Byte":
							this.SHRD(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SHRD R32Type R32Type Byte":
							this.SHRD(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SHRD WordMemory R16Type Byte":
							this.SHRD(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SHRD___CL":
					switch(parameterTypes)
					{
						case "SHRD___CL DWordMemory R32Type":
							this.SHRD___CL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SHRD___CL R16Type R16Type":
							this.SHRD___CL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SHRD___CL R32Type R32Type":
							this.SHRD___CL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SHRD___CL WordMemory R16Type":
							this.SHRD___CL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SHR__CL":
					switch(parameterTypes)
					{
						case "SHR__CL ByteMemory":
							this.SHR__CL(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SHR__CL DWordMemory":
							this.SHR__CL(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SHR__CL R16Type":
							this.SHR__CL(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SHR__CL R32Type":
							this.SHR__CL(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SHR__CL R8Type":
							this.SHR__CL(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SHR__CL WordMemory":
							this.SHR__CL(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SIDT":
					switch(parameterTypes)
					{
						case "SIDT Memory":
							this.SIDT(GetMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SLDT":
					switch(parameterTypes)
					{
						case "SLDT R16Type":
							this.SLDT(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SLDT WordMemory":
							this.SLDT(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SMSW":
					switch(parameterTypes)
					{
						case "SMSW R16Type":
							this.SMSW(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SMSW WordMemory":
							this.SMSW(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "STC":
					switch(parameterTypes)
					{
						case "STC":
							this.STC();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "STD":
					switch(parameterTypes)
					{
						case "STD":
							this.STD();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "STI":
					switch(parameterTypes)
					{
						case "STI":
							this.STI();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "STOSB":
					switch(parameterTypes)
					{
						case "STOSB":
							this.STOSB();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "STOSD":
					switch(parameterTypes)
					{
						case "STOSD":
							this.STOSD();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "STOSW":
					switch(parameterTypes)
					{
						case "STOSW":
							this.STOSW();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "STR":
					switch(parameterTypes)
					{
						case "STR R16Type":
							this.STR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "STR WordMemory":
							this.STR(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SUB":
					switch(parameterTypes)
					{
						case "SUB ByteMemory Byte":
							this.SUB(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SUB ByteMemory R8Type":
							this.SUB(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SUB DWordMemory Byte":
							this.SUB(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SUB DWordMemory R32Type":
							this.SUB(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SUB DWordMemory UInt32":
							this.SUB(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SUB R16Type Byte":
							this.SUB(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SUB R16Type R16Type":
							this.SUB(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SUB R16Type UInt16":
							this.SUB(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SUB R16Type WordMemory":
							this.SUB(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SUB R32Type Byte":
							this.SUB(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SUB R32Type DWordMemory":
							this.SUB(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SUB R32Type R32Type":
							this.SUB(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SUB R32Type UInt32":
							this.SUB(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SUB R8Type Byte":
							this.SUB(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SUB R8Type ByteMemory":
							this.SUB(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetByteMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "SUB R8Type R8Type":
							this.SUB(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SUB WordMemory Byte":
							this.SUB(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "SUB WordMemory R16Type":
							this.SUB(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "SUB WordMemory UInt16":
							this.SUB(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SYSCALL":
					switch(parameterTypes)
					{
						case "SYSCALL":
							this.SYSCALL();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SYSENTER":
					switch(parameterTypes)
					{
						case "SYSENTER":
							this.SYSENTER();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SYSEXIT":
					switch(parameterTypes)
					{
						case "SYSEXIT":
							this.SYSEXIT();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "SYSRET":
					switch(parameterTypes)
					{
						case "SYSRET":
							this.SYSRET();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "TEST":
					switch(parameterTypes)
					{
						case "TEST ByteMemory Byte":
							this.TEST(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "TEST ByteMemory R8Type":
							this.TEST(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "TEST DWordMemory R32Type":
							this.TEST(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "TEST DWordMemory UInt32":
							this.TEST(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "TEST R16Type R16Type":
							this.TEST(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "TEST R16Type UInt16":
							this.TEST(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "TEST R32Type R32Type":
							this.TEST(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "TEST R32Type UInt32":
							this.TEST(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "TEST R8Type Byte":
							this.TEST(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "TEST R8Type R8Type":
							this.TEST(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "TEST WordMemory R16Type":
							this.TEST(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "TEST WordMemory UInt16":
							this.TEST(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "VERR":
					switch(parameterTypes)
					{
						case "VERR R16Type":
							this.VERR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "VERR WordMemory":
							this.VERR(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "VERW":
					switch(parameterTypes)
					{
						case "VERW R16Type":
							this.VERW(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "VERW WordMemory":
							this.VERW(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "WAIT":
					switch(parameterTypes)
					{
						case "WAIT":
							this.WAIT();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "WBINVD":
					switch(parameterTypes)
					{
						case "WBINVD":
							this.WBINVD();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "WRMSR":
					switch(parameterTypes)
					{
						case "WRMSR":
							this.WRMSR();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "XADD":
					switch(parameterTypes)
					{
						case "XADD ByteMemory R8Type":
							this.XADD(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "XADD DWordMemory R32Type":
							this.XADD(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "XADD R16Type R16Type":
							this.XADD(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "XADD R32Type R32Type":
							this.XADD(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "XADD R8Type R8Type":
							this.XADD(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "XADD WordMemory R16Type":
							this.XADD(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "XCHG":
					switch(parameterTypes)
					{
						case "XCHG ByteMemory R8Type":
							this.XCHG(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "XCHG DWordMemory R32Type":
							this.XCHG(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "XCHG R16Type R16Type":
							this.XCHG(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "XCHG R16Type WordMemory":
							this.XCHG(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "XCHG R32Type DWordMemory":
							this.XCHG(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "XCHG R32Type R32Type":
							this.XCHG(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "XCHG R8Type ByteMemory":
							this.XCHG(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetByteMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "XCHG R8Type R8Type":
							this.XCHG(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "XCHG WordMemory R16Type":
							this.XCHG(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "XLAT":
					switch(parameterTypes)
					{
						case "XLAT":
							this.XLAT();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "XLATB":
					switch(parameterTypes)
					{
						case "XLATB":
							this.XLATB();
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				case "XOR":
					switch(parameterTypes)
					{
						case "XOR ByteMemory Byte":
							this.XOR(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "XOR ByteMemory R8Type":
							this.XOR(GetByteMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "XOR DWordMemory Byte":
							this.XOR(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "XOR DWordMemory R32Type":
							this.XOR(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "XOR DWordMemory UInt32":
							this.XOR(GetDWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "XOR R16Type Byte":
							this.XOR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "XOR R16Type R16Type":
							this.XOR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "XOR R16Type UInt16":
							this.XOR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "XOR R16Type WordMemory":
							this.XOR(R16.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "XOR R32Type Byte":
							this.XOR(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "XOR R32Type DWordMemory":
							this.XOR(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetDWordMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "XOR R32Type R32Type":
							this.XOR(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R32.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "XOR R32Type UInt32":
							this.XOR(R32.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToUInt32((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "XOR R8Type Byte":
							this.XOR(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "XOR R8Type ByteMemory":
							this.XOR(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), GetByteMemory(method.Operands[1] as SharpOS.AOT.IR.Operands.Call));
							break;
						
						case "XOR R8Type R8Type":
							this.XOR(R8.GetByID((method.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), R8.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "XOR WordMemory Byte":
							this.XOR(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToByte((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						case "XOR WordMemory R16Type":
							this.XOR(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), R16.GetByID((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
							break;
						
						case "XOR WordMemory UInt16":
							this.XOR(GetWordMemory(method.Operands[0] as SharpOS.AOT.IR.Operands.Call), Convert.ToUInt16((method.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value));
							break;
						
						default:
							throw new Exception("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
					}
					break;
				
				default:
					throw new Exception("'" + method.Method.Name + "' is not supported.");
			}
		}
	}
}
