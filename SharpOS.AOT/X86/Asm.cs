/**
 *  (C) 2006-2007 The SharpOS Project Team - http://www.sharpos.org
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
 * 
 *  Author: Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 */

using System;

namespace SharpOS.AOT.X86
{
    /// <summary>
    /// This class encapsulates machine code stubs, that when used by kernel code, 
    /// and then AOTed, are expressed as their respective machine language operations.
    /// </summary>
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
		public unsafe static void ADC (byte *target, R8Type source) {}
		
		// ADC mem16,reg16
		public static void ADC (WordMemory target, R16Type source) {}
		public unsafe static void ADC (UInt16 *target, R16Type source) {}
		
		// ADC mem32,reg32
		public static void ADC (DWordMemory target, R32Type source) {}
		public unsafe static void ADC (UInt32 *target, R32Type source) {}
		
		// ADC reg8,mem8
		public static void ADC (R8Type target, ByteMemory source) {}
		public unsafe static void ADC (R8Type target, byte *source) {}
		
		// ADC reg16,mem16
		public static void ADC (R16Type target, WordMemory source) {}
		public unsafe static void ADC (R16Type target, UInt16 *source) {}
		
		// ADC reg32,mem32
		public static void ADC (R32Type target, DWordMemory source) {}
		public unsafe static void ADC (R32Type target, UInt32 *source) {}
		
		// ADC mem8,imm8
		public static void ADC (ByteMemory target, Byte source) {}
		public unsafe static void ADC (byte *target, Byte source) {}
		
		// ADC mem16,imm16
		public static void ADC (WordMemory target, UInt16 source) {}
		public unsafe static void ADC (UInt16 *target, UInt16 source) {}
		
		// ADC mem32,imm32
		public static void ADC (DWordMemory target, UInt32 source) {}
		public unsafe static void ADC (UInt32 *target, UInt32 source) {}
		
		// ADC mem16,imm8
		public static void ADC (WordMemory target, Byte source) {}
		public unsafe static void ADC (UInt16 *target, Byte source) {}
		
		// ADC mem32,imm8
		public static void ADC (DWordMemory target, Byte source) {}
		public unsafe static void ADC (UInt32 *target, Byte source) {}
		
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
		public unsafe static void ADD (byte *target, R8Type source) {}
		
		// ADD mem16,reg16
		public static void ADD (WordMemory target, R16Type source) {}
		public unsafe static void ADD (UInt16 *target, R16Type source) {}
		
		// ADD mem32,reg32
		public static void ADD (DWordMemory target, R32Type source) {}
		public unsafe static void ADD (UInt32 *target, R32Type source) {}
		
		// ADD reg8,mem8
		public static void ADD (R8Type target, ByteMemory source) {}
		public unsafe static void ADD (R8Type target, byte *source) {}
		
		// ADD reg16,mem16
		public static void ADD (R16Type target, WordMemory source) {}
		public unsafe static void ADD (R16Type target, UInt16 *source) {}
		
		// ADD reg32,mem32
		public static void ADD (R32Type target, DWordMemory source) {}
		public unsafe static void ADD (R32Type target, UInt32 *source) {}
		
		// ADD mem8,imm8
		public static void ADD (ByteMemory target, Byte source) {}
		public unsafe static void ADD (byte *target, Byte source) {}
		
		// ADD mem16,imm16
		public static void ADD (WordMemory target, UInt16 source) {}
		public unsafe static void ADD (UInt16 *target, UInt16 source) {}
		
		// ADD mem32,imm32
		public static void ADD (DWordMemory target, UInt32 source) {}
		public unsafe static void ADD (UInt32 *target, UInt32 source) {}
		
		// ADD mem16,imm8
		public static void ADD (WordMemory target, Byte source) {}
		public unsafe static void ADD (UInt16 *target, Byte source) {}
		
		// ADD mem32,imm8
		public static void ADD (DWordMemory target, Byte source) {}
		public unsafe static void ADD (UInt32 *target, Byte source) {}
		
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
		
		// ALIGN 
		public static void ALIGN (UInt32 value) {}
		
		// AND mem8,reg8
		public static void AND (ByteMemory target, R8Type source) {}
		public unsafe static void AND (byte *target, R8Type source) {}
		
		// AND mem16,reg16
		public static void AND (WordMemory target, R16Type source) {}
		public unsafe static void AND (UInt16 *target, R16Type source) {}
		
		// AND mem32,reg32
		public static void AND (DWordMemory target, R32Type source) {}
		public unsafe static void AND (UInt32 *target, R32Type source) {}
		
		// AND reg8,mem8
		public static void AND (R8Type target, ByteMemory source) {}
		public unsafe static void AND (R8Type target, byte *source) {}
		
		// AND reg16,mem16
		public static void AND (R16Type target, WordMemory source) {}
		public unsafe static void AND (R16Type target, UInt16 *source) {}
		
		// AND reg32,mem32
		public static void AND (R32Type target, DWordMemory source) {}
		public unsafe static void AND (R32Type target, UInt32 *source) {}
		
		// AND mem8,imm8
		public static void AND (ByteMemory target, Byte source) {}
		public unsafe static void AND (byte *target, Byte source) {}
		
		// AND mem16,imm16
		public static void AND (WordMemory target, UInt16 source) {}
		public unsafe static void AND (UInt16 *target, UInt16 source) {}
		
		// AND mem32,imm32
		public static void AND (DWordMemory target, UInt32 source) {}
		public unsafe static void AND (UInt32 *target, UInt32 source) {}
		
		// AND mem16,imm8
		public static void AND (WordMemory target, Byte source) {}
		public unsafe static void AND (UInt16 *target, Byte source) {}
		
		// AND mem32,imm8
		public static void AND (DWordMemory target, Byte source) {}
		public unsafe static void AND (UInt32 *target, Byte source) {}
		
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
		public unsafe static void ARPL (UInt16 *target, R16Type source) {}
		
		// ARPL rmreg16,reg16
		public static void ARPL (R16Type target, R16Type source) {}
		
		// BITS32 
		public static void BITS32 (bool value) {}
		
		// BOUND reg16,mem
		public static void BOUND (R16Type target, Memory source) {}
		public unsafe static void BOUND (R16Type target, byte *source) {}
		
		// BOUND reg32,mem
		public static void BOUND (R32Type target, Memory source) {}
		public unsafe static void BOUND (R32Type target, byte *source) {}
		
		// BSF reg16,mem16
		public static void BSF (R16Type target, WordMemory source) {}
		public unsafe static void BSF (R16Type target, UInt16 *source) {}
		
		// BSF reg32,mem32
		public static void BSF (R32Type target, DWordMemory source) {}
		public unsafe static void BSF (R32Type target, UInt32 *source) {}
		
		// BSF reg16,rmreg16
		public static void BSF (R16Type target, R16Type source) {}
		
		// BSF reg32,rmreg32
		public static void BSF (R32Type target, R32Type source) {}
		
		// BSR reg16,mem16
		public static void BSR (R16Type target, WordMemory source) {}
		public unsafe static void BSR (R16Type target, UInt16 *source) {}
		
		// BSR reg32,mem32
		public static void BSR (R32Type target, DWordMemory source) {}
		public unsafe static void BSR (R32Type target, UInt32 *source) {}
		
		// BSR reg16,rmreg16
		public static void BSR (R16Type target, R16Type source) {}
		
		// BSR reg32,rmreg32
		public static void BSR (R32Type target, R32Type source) {}
		
		// BSWAP reg32
		public static void BSWAP (R32Type target) {}
		
		// BT mem16,reg16
		public static void BT (WordMemory target, R16Type source) {}
		public unsafe static void BT (UInt16 *target, R16Type source) {}
		
		// BT mem32,reg32
		public static void BT (DWordMemory target, R32Type source) {}
		public unsafe static void BT (UInt32 *target, R32Type source) {}
		
		// BT mem16,imm8
		public static void BT (WordMemory target, Byte source) {}
		public unsafe static void BT (UInt16 *target, Byte source) {}
		
		// BT mem32,imm8
		public static void BT (DWordMemory target, Byte source) {}
		public unsafe static void BT (UInt32 *target, Byte source) {}
		
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
		public unsafe static void BTC (UInt16 *target, R16Type source) {}
		
		// BTC mem32,reg32
		public static void BTC (DWordMemory target, R32Type source) {}
		public unsafe static void BTC (UInt32 *target, R32Type source) {}
		
		// BTC mem16,imm8
		public static void BTC (WordMemory target, Byte source) {}
		public unsafe static void BTC (UInt16 *target, Byte source) {}
		
		// BTC mem32,imm8
		public static void BTC (DWordMemory target, Byte source) {}
		public unsafe static void BTC (UInt32 *target, Byte source) {}
		
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
		public unsafe static void BTR (UInt16 *target, R16Type source) {}
		
		// BTR mem32,reg32
		public static void BTR (DWordMemory target, R32Type source) {}
		public unsafe static void BTR (UInt32 *target, R32Type source) {}
		
		// BTR mem16,imm8
		public static void BTR (WordMemory target, Byte source) {}
		public unsafe static void BTR (UInt16 *target, Byte source) {}
		
		// BTR mem32,imm8
		public static void BTR (DWordMemory target, Byte source) {}
		public unsafe static void BTR (UInt32 *target, Byte source) {}
		
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
		public unsafe static void BTS (UInt16 *target, R16Type source) {}
		
		// BTS mem32,reg32
		public static void BTS (DWordMemory target, R32Type source) {}
		public unsafe static void BTS (UInt32 *target, R32Type source) {}
		
		// BTS mem16,imm8
		public static void BTS (WordMemory target, Byte source) {}
		public unsafe static void BTS (UInt16 *target, Byte source) {}
		
		// BTS mem32,imm8
		public static void BTS (DWordMemory target, Byte source) {}
		public unsafe static void BTS (UInt32 *target, Byte source) {}
		
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
		public unsafe static void CALL_FAR (UInt16 *target) {}
		
		public static void CALL_FAR (string label) {}
		
		// CALL FAR mem32
		public static void CALL_FAR (DWordMemory target) {}
		public unsafe static void CALL_FAR (UInt32 *target) {}
		
		// CALL mem16
		public static void CALL (WordMemory target) {}
		public unsafe static void CALL (UInt16 *target) {}
		
		// CALL mem32
		public static void CALL (DWordMemory target) {}
		public unsafe static void CALL (UInt32 *target) {}
		
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
		public unsafe static void CLFLUSH (byte *target) {}
		
		// CLI 
		public static void CLI () {}
		
		// CLTS 
		public static void CLTS () {}
		
		// CMC 
		public static void CMC () {}
		
		// CMOVA reg16,mem16
		public static void CMOVA (R16Type target, WordMemory source) {}
		public unsafe static void CMOVA (R16Type target, UInt16 *source) {}
		
		// CMOVA reg32,mem32
		public static void CMOVA (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVA (R32Type target, UInt32 *source) {}
		
		// CMOVA reg16,rmreg16
		public static void CMOVA (R16Type target, R16Type source) {}
		
		// CMOVA reg32,rmreg32
		public static void CMOVA (R32Type target, R32Type source) {}
		
		// CMOVAE reg16,mem16
		public static void CMOVAE (R16Type target, WordMemory source) {}
		public unsafe static void CMOVAE (R16Type target, UInt16 *source) {}
		
		// CMOVAE reg32,mem32
		public static void CMOVAE (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVAE (R32Type target, UInt32 *source) {}
		
		// CMOVAE reg16,rmreg16
		public static void CMOVAE (R16Type target, R16Type source) {}
		
		// CMOVAE reg32,rmreg32
		public static void CMOVAE (R32Type target, R32Type source) {}
		
		// CMOVB reg16,mem16
		public static void CMOVB (R16Type target, WordMemory source) {}
		public unsafe static void CMOVB (R16Type target, UInt16 *source) {}
		
		// CMOVB reg32,mem32
		public static void CMOVB (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVB (R32Type target, UInt32 *source) {}
		
		// CMOVB reg16,rmreg16
		public static void CMOVB (R16Type target, R16Type source) {}
		
		// CMOVB reg32,rmreg32
		public static void CMOVB (R32Type target, R32Type source) {}
		
		// CMOVBE reg16,mem16
		public static void CMOVBE (R16Type target, WordMemory source) {}
		public unsafe static void CMOVBE (R16Type target, UInt16 *source) {}
		
		// CMOVBE reg32,mem32
		public static void CMOVBE (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVBE (R32Type target, UInt32 *source) {}
		
		// CMOVBE reg16,rmreg16
		public static void CMOVBE (R16Type target, R16Type source) {}
		
		// CMOVBE reg32,rmreg32
		public static void CMOVBE (R32Type target, R32Type source) {}
		
		// CMOVC reg16,mem16
		public static void CMOVC (R16Type target, WordMemory source) {}
		public unsafe static void CMOVC (R16Type target, UInt16 *source) {}
		
		// CMOVC reg32,mem32
		public static void CMOVC (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVC (R32Type target, UInt32 *source) {}
		
		// CMOVC reg16,rmreg16
		public static void CMOVC (R16Type target, R16Type source) {}
		
		// CMOVC reg32,rmreg32
		public static void CMOVC (R32Type target, R32Type source) {}
		
		// CMOVE reg16,mem16
		public static void CMOVE (R16Type target, WordMemory source) {}
		public unsafe static void CMOVE (R16Type target, UInt16 *source) {}
		
		// CMOVE reg32,mem32
		public static void CMOVE (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVE (R32Type target, UInt32 *source) {}
		
		// CMOVE reg16,rmreg16
		public static void CMOVE (R16Type target, R16Type source) {}
		
		// CMOVE reg32,rmreg32
		public static void CMOVE (R32Type target, R32Type source) {}
		
		// CMOVG reg16,mem16
		public static void CMOVG (R16Type target, WordMemory source) {}
		public unsafe static void CMOVG (R16Type target, UInt16 *source) {}
		
		// CMOVG reg32,mem32
		public static void CMOVG (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVG (R32Type target, UInt32 *source) {}
		
		// CMOVG reg16,rmreg16
		public static void CMOVG (R16Type target, R16Type source) {}
		
		// CMOVG reg32,rmreg32
		public static void CMOVG (R32Type target, R32Type source) {}
		
		// CMOVGE reg16,mem16
		public static void CMOVGE (R16Type target, WordMemory source) {}
		public unsafe static void CMOVGE (R16Type target, UInt16 *source) {}
		
		// CMOVGE reg32,mem32
		public static void CMOVGE (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVGE (R32Type target, UInt32 *source) {}
		
		// CMOVGE reg16,rmreg16
		public static void CMOVGE (R16Type target, R16Type source) {}
		
		// CMOVGE reg32,rmreg32
		public static void CMOVGE (R32Type target, R32Type source) {}
		
		// CMOVL reg16,mem16
		public static void CMOVL (R16Type target, WordMemory source) {}
		public unsafe static void CMOVL (R16Type target, UInt16 *source) {}
		
		// CMOVL reg32,mem32
		public static void CMOVL (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVL (R32Type target, UInt32 *source) {}
		
		// CMOVL reg16,rmreg16
		public static void CMOVL (R16Type target, R16Type source) {}
		
		// CMOVL reg32,rmreg32
		public static void CMOVL (R32Type target, R32Type source) {}
		
		// CMOVLE reg16,mem16
		public static void CMOVLE (R16Type target, WordMemory source) {}
		public unsafe static void CMOVLE (R16Type target, UInt16 *source) {}
		
		// CMOVLE reg32,mem32
		public static void CMOVLE (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVLE (R32Type target, UInt32 *source) {}
		
		// CMOVLE reg16,rmreg16
		public static void CMOVLE (R16Type target, R16Type source) {}
		
		// CMOVLE reg32,rmreg32
		public static void CMOVLE (R32Type target, R32Type source) {}
		
		// CMOVNA reg16,mem16
		public static void CMOVNA (R16Type target, WordMemory source) {}
		public unsafe static void CMOVNA (R16Type target, UInt16 *source) {}
		
		// CMOVNA reg32,mem32
		public static void CMOVNA (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVNA (R32Type target, UInt32 *source) {}
		
		// CMOVNA reg16,rmreg16
		public static void CMOVNA (R16Type target, R16Type source) {}
		
		// CMOVNA reg32,rmreg32
		public static void CMOVNA (R32Type target, R32Type source) {}
		
		// CMOVNAE reg16,mem16
		public static void CMOVNAE (R16Type target, WordMemory source) {}
		public unsafe static void CMOVNAE (R16Type target, UInt16 *source) {}
		
		// CMOVNAE reg32,mem32
		public static void CMOVNAE (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVNAE (R32Type target, UInt32 *source) {}
		
		// CMOVNAE reg16,rmreg16
		public static void CMOVNAE (R16Type target, R16Type source) {}
		
		// CMOVNAE reg32,rmreg32
		public static void CMOVNAE (R32Type target, R32Type source) {}
		
		// CMOVNB reg16,mem16
		public static void CMOVNB (R16Type target, WordMemory source) {}
		public unsafe static void CMOVNB (R16Type target, UInt16 *source) {}
		
		// CMOVNB reg32,mem32
		public static void CMOVNB (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVNB (R32Type target, UInt32 *source) {}
		
		// CMOVNB reg16,rmreg16
		public static void CMOVNB (R16Type target, R16Type source) {}
		
		// CMOVNB reg32,rmreg32
		public static void CMOVNB (R32Type target, R32Type source) {}
		
		// CMOVNBE reg16,mem16
		public static void CMOVNBE (R16Type target, WordMemory source) {}
		public unsafe static void CMOVNBE (R16Type target, UInt16 *source) {}
		
		// CMOVNBE reg32,mem32
		public static void CMOVNBE (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVNBE (R32Type target, UInt32 *source) {}
		
		// CMOVNBE reg16,rmreg16
		public static void CMOVNBE (R16Type target, R16Type source) {}
		
		// CMOVNBE reg32,rmreg32
		public static void CMOVNBE (R32Type target, R32Type source) {}
		
		// CMOVNC reg16,mem16
		public static void CMOVNC (R16Type target, WordMemory source) {}
		public unsafe static void CMOVNC (R16Type target, UInt16 *source) {}
		
		// CMOVNC reg32,mem32
		public static void CMOVNC (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVNC (R32Type target, UInt32 *source) {}
		
		// CMOVNC reg16,rmreg16
		public static void CMOVNC (R16Type target, R16Type source) {}
		
		// CMOVNC reg32,rmreg32
		public static void CMOVNC (R32Type target, R32Type source) {}
		
		// CMOVNE reg16,mem16
		public static void CMOVNE (R16Type target, WordMemory source) {}
		public unsafe static void CMOVNE (R16Type target, UInt16 *source) {}
		
		// CMOVNE reg32,mem32
		public static void CMOVNE (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVNE (R32Type target, UInt32 *source) {}
		
		// CMOVNE reg16,rmreg16
		public static void CMOVNE (R16Type target, R16Type source) {}
		
		// CMOVNE reg32,rmreg32
		public static void CMOVNE (R32Type target, R32Type source) {}
		
		// CMOVNG reg16,mem16
		public static void CMOVNG (R16Type target, WordMemory source) {}
		public unsafe static void CMOVNG (R16Type target, UInt16 *source) {}
		
		// CMOVNG reg32,mem32
		public static void CMOVNG (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVNG (R32Type target, UInt32 *source) {}
		
		// CMOVNG reg16,rmreg16
		public static void CMOVNG (R16Type target, R16Type source) {}
		
		// CMOVNG reg32,rmreg32
		public static void CMOVNG (R32Type target, R32Type source) {}
		
		// CMOVNGE reg16,mem16
		public static void CMOVNGE (R16Type target, WordMemory source) {}
		public unsafe static void CMOVNGE (R16Type target, UInt16 *source) {}
		
		// CMOVNGE reg32,mem32
		public static void CMOVNGE (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVNGE (R32Type target, UInt32 *source) {}
		
		// CMOVNGE reg16,rmreg16
		public static void CMOVNGE (R16Type target, R16Type source) {}
		
		// CMOVNGE reg32,rmreg32
		public static void CMOVNGE (R32Type target, R32Type source) {}
		
		// CMOVNL reg16,mem16
		public static void CMOVNL (R16Type target, WordMemory source) {}
		public unsafe static void CMOVNL (R16Type target, UInt16 *source) {}
		
		// CMOVNL reg32,mem32
		public static void CMOVNL (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVNL (R32Type target, UInt32 *source) {}
		
		// CMOVNL reg16,rmreg16
		public static void CMOVNL (R16Type target, R16Type source) {}
		
		// CMOVNL reg32,rmreg32
		public static void CMOVNL (R32Type target, R32Type source) {}
		
		// CMOVNLE reg16,mem16
		public static void CMOVNLE (R16Type target, WordMemory source) {}
		public unsafe static void CMOVNLE (R16Type target, UInt16 *source) {}
		
		// CMOVNLE reg32,mem32
		public static void CMOVNLE (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVNLE (R32Type target, UInt32 *source) {}
		
		// CMOVNLE reg16,rmreg16
		public static void CMOVNLE (R16Type target, R16Type source) {}
		
		// CMOVNLE reg32,rmreg32
		public static void CMOVNLE (R32Type target, R32Type source) {}
		
		// CMOVNO reg16,mem16
		public static void CMOVNO (R16Type target, WordMemory source) {}
		public unsafe static void CMOVNO (R16Type target, UInt16 *source) {}
		
		// CMOVNO reg32,mem32
		public static void CMOVNO (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVNO (R32Type target, UInt32 *source) {}
		
		// CMOVNO reg16,rmreg16
		public static void CMOVNO (R16Type target, R16Type source) {}
		
		// CMOVNO reg32,rmreg32
		public static void CMOVNO (R32Type target, R32Type source) {}
		
		// CMOVNP reg16,mem16
		public static void CMOVNP (R16Type target, WordMemory source) {}
		public unsafe static void CMOVNP (R16Type target, UInt16 *source) {}
		
		// CMOVNP reg32,mem32
		public static void CMOVNP (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVNP (R32Type target, UInt32 *source) {}
		
		// CMOVNP reg16,rmreg16
		public static void CMOVNP (R16Type target, R16Type source) {}
		
		// CMOVNP reg32,rmreg32
		public static void CMOVNP (R32Type target, R32Type source) {}
		
		// CMOVNS reg16,mem16
		public static void CMOVNS (R16Type target, WordMemory source) {}
		public unsafe static void CMOVNS (R16Type target, UInt16 *source) {}
		
		// CMOVNS reg32,mem32
		public static void CMOVNS (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVNS (R32Type target, UInt32 *source) {}
		
		// CMOVNS reg16,rmreg16
		public static void CMOVNS (R16Type target, R16Type source) {}
		
		// CMOVNS reg32,rmreg32
		public static void CMOVNS (R32Type target, R32Type source) {}
		
		// CMOVNZ reg16,mem16
		public static void CMOVNZ (R16Type target, WordMemory source) {}
		public unsafe static void CMOVNZ (R16Type target, UInt16 *source) {}
		
		// CMOVNZ reg32,mem32
		public static void CMOVNZ (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVNZ (R32Type target, UInt32 *source) {}
		
		// CMOVNZ reg16,rmreg16
		public static void CMOVNZ (R16Type target, R16Type source) {}
		
		// CMOVNZ reg32,rmreg32
		public static void CMOVNZ (R32Type target, R32Type source) {}
		
		// CMOVO reg16,mem16
		public static void CMOVO (R16Type target, WordMemory source) {}
		public unsafe static void CMOVO (R16Type target, UInt16 *source) {}
		
		// CMOVO reg32,mem32
		public static void CMOVO (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVO (R32Type target, UInt32 *source) {}
		
		// CMOVO reg16,rmreg16
		public static void CMOVO (R16Type target, R16Type source) {}
		
		// CMOVO reg32,rmreg32
		public static void CMOVO (R32Type target, R32Type source) {}
		
		// CMOVP reg16,mem16
		public static void CMOVP (R16Type target, WordMemory source) {}
		public unsafe static void CMOVP (R16Type target, UInt16 *source) {}
		
		// CMOVP reg32,mem32
		public static void CMOVP (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVP (R32Type target, UInt32 *source) {}
		
		// CMOVP reg16,rmreg16
		public static void CMOVP (R16Type target, R16Type source) {}
		
		// CMOVP reg32,rmreg32
		public static void CMOVP (R32Type target, R32Type source) {}
		
		// CMOVPE reg16,mem16
		public static void CMOVPE (R16Type target, WordMemory source) {}
		public unsafe static void CMOVPE (R16Type target, UInt16 *source) {}
		
		// CMOVPE reg32,mem32
		public static void CMOVPE (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVPE (R32Type target, UInt32 *source) {}
		
		// CMOVPE reg16,rmreg16
		public static void CMOVPE (R16Type target, R16Type source) {}
		
		// CMOVPE reg32,rmreg32
		public static void CMOVPE (R32Type target, R32Type source) {}
		
		// CMOVPO reg16,mem16
		public static void CMOVPO (R16Type target, WordMemory source) {}
		public unsafe static void CMOVPO (R16Type target, UInt16 *source) {}
		
		// CMOVPO reg32,mem32
		public static void CMOVPO (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVPO (R32Type target, UInt32 *source) {}
		
		// CMOVPO reg16,rmreg16
		public static void CMOVPO (R16Type target, R16Type source) {}
		
		// CMOVPO reg32,rmreg32
		public static void CMOVPO (R32Type target, R32Type source) {}
		
		// CMOVS reg16,mem16
		public static void CMOVS (R16Type target, WordMemory source) {}
		public unsafe static void CMOVS (R16Type target, UInt16 *source) {}
		
		// CMOVS reg32,mem32
		public static void CMOVS (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVS (R32Type target, UInt32 *source) {}
		
		// CMOVS reg16,rmreg16
		public static void CMOVS (R16Type target, R16Type source) {}
		
		// CMOVS reg32,rmreg32
		public static void CMOVS (R32Type target, R32Type source) {}
		
		// CMOVZ reg16,mem16
		public static void CMOVZ (R16Type target, WordMemory source) {}
		public unsafe static void CMOVZ (R16Type target, UInt16 *source) {}
		
		// CMOVZ reg32,mem32
		public static void CMOVZ (R32Type target, DWordMemory source) {}
		public unsafe static void CMOVZ (R32Type target, UInt32 *source) {}
		
		// CMOVZ reg16,rmreg16
		public static void CMOVZ (R16Type target, R16Type source) {}
		
		// CMOVZ reg32,rmreg32
		public static void CMOVZ (R32Type target, R32Type source) {}
		
		// CMP mem8,reg8
		public static void CMP (ByteMemory target, R8Type source) {}
		public unsafe static void CMP (byte *target, R8Type source) {}
		
		// CMP mem16,reg16
		public static void CMP (WordMemory target, R16Type source) {}
		public unsafe static void CMP (UInt16 *target, R16Type source) {}
		
		// CMP mem32,reg32
		public static void CMP (DWordMemory target, R32Type source) {}
		public unsafe static void CMP (UInt32 *target, R32Type source) {}
		
		// CMP reg8,mem8
		public static void CMP (R8Type target, ByteMemory source) {}
		public unsafe static void CMP (R8Type target, byte *source) {}
		
		// CMP reg16,mem16
		public static void CMP (R16Type target, WordMemory source) {}
		public unsafe static void CMP (R16Type target, UInt16 *source) {}
		
		// CMP reg32,mem32
		public static void CMP (R32Type target, DWordMemory source) {}
		public unsafe static void CMP (R32Type target, UInt32 *source) {}
		
		// CMP mem8,imm8
		public static void CMP (ByteMemory target, Byte source) {}
		public unsafe static void CMP (byte *target, Byte source) {}
		
		// CMP mem16,imm16
		public static void CMP (WordMemory target, UInt16 source) {}
		public unsafe static void CMP (UInt16 *target, UInt16 source) {}
		
		// CMP mem32,imm32
		public static void CMP (DWordMemory target, UInt32 source) {}
		public unsafe static void CMP (UInt32 *target, UInt32 source) {}
		
		// CMP mem16,imm8
		public static void CMP (WordMemory target, Byte source) {}
		public unsafe static void CMP (UInt16 *target, Byte source) {}
		
		// CMP mem32,imm8
		public static void CMP (DWordMemory target, Byte source) {}
		public unsafe static void CMP (UInt32 *target, Byte source) {}
		
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
		public unsafe static void CMPXCHG (byte *target, R8Type source) {}
		
		// CMPXCHG mem16,reg16
		public static void CMPXCHG (WordMemory target, R16Type source) {}
		public unsafe static void CMPXCHG (UInt16 *target, R16Type source) {}
		
		// CMPXCHG mem32,reg32
		public static void CMPXCHG (DWordMemory target, R32Type source) {}
		public unsafe static void CMPXCHG (UInt32 *target, R32Type source) {}
		
		// CMPXCHG rmreg8,reg8
		public static void CMPXCHG (R8Type target, R8Type source) {}
		
		// CMPXCHG rmreg16,reg16
		public static void CMPXCHG (R16Type target, R16Type source) {}
		
		// CMPXCHG rmreg32,reg32
		public static void CMPXCHG (R32Type target, R32Type source) {}
		
		// CMPXCHG8B mem
		public static void CMPXCHG8B (Memory target) {}
		public unsafe static void CMPXCHG8B (byte *target) {}
		
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
		public unsafe static void DEC (byte *target) {}
		
		// DEC mem16
		public static void DEC (WordMemory target) {}
		public unsafe static void DEC (UInt16 *target) {}
		
		// DEC mem32
		public static void DEC (DWordMemory target) {}
		public unsafe static void DEC (UInt32 *target) {}
		
		// DEC rmreg8
		public static void DEC (R8Type target) {}
		
		// DIV mem8
		public static void DIV (ByteMemory target) {}
		public unsafe static void DIV (byte *target) {}
		
		// DIV mem16
		public static void DIV (WordMemory target) {}
		public unsafe static void DIV (UInt16 *target) {}
		
		// DIV mem32
		public static void DIV (DWordMemory target) {}
		public unsafe static void DIV (UInt32 *target) {}
		
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
		public unsafe static void FADD (UInt32 *target) {}
		
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
		public unsafe static void FCOM (UInt32 *target) {}
		
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
		public unsafe static void FCOMP (UInt32 *target) {}
		
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
		public unsafe static void FDIV (UInt32 *target) {}
		
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
		public unsafe static void FDIVR (UInt32 *target) {}
		
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
		public unsafe static void FIADD (UInt16 *target) {}
		
		// FIADD mem32
		public static void FIADD (DWordMemory target) {}
		public unsafe static void FIADD (UInt32 *target) {}
		
		// FICOM mem16
		public static void FICOM (WordMemory target) {}
		public unsafe static void FICOM (UInt16 *target) {}
		
		// FICOM mem32
		public static void FICOM (DWordMemory target) {}
		public unsafe static void FICOM (UInt32 *target) {}
		
		// FICOMP mem16
		public static void FICOMP (WordMemory target) {}
		public unsafe static void FICOMP (UInt16 *target) {}
		
		// FICOMP mem32
		public static void FICOMP (DWordMemory target) {}
		public unsafe static void FICOMP (UInt32 *target) {}
		
		// FIDIV mem16
		public static void FIDIV (WordMemory target) {}
		public unsafe static void FIDIV (UInt16 *target) {}
		
		// FIDIV mem32
		public static void FIDIV (DWordMemory target) {}
		public unsafe static void FIDIV (UInt32 *target) {}
		
		// FIDIVR mem16
		public static void FIDIVR (WordMemory target) {}
		public unsafe static void FIDIVR (UInt16 *target) {}
		
		// FIDIVR mem32
		public static void FIDIVR (DWordMemory target) {}
		public unsafe static void FIDIVR (UInt32 *target) {}
		
		// FILD mem16
		public static void FILD (WordMemory target) {}
		public unsafe static void FILD (UInt16 *target) {}
		
		// FILD mem32
		public static void FILD (DWordMemory target) {}
		public unsafe static void FILD (UInt32 *target) {}
		
		// FILD mem64
		public static void FILD (QWordMemory target) {}
		
		// FIMUL mem16
		public static void FIMUL (WordMemory target) {}
		public unsafe static void FIMUL (UInt16 *target) {}
		
		// FIMUL mem32
		public static void FIMUL (DWordMemory target) {}
		public unsafe static void FIMUL (UInt32 *target) {}
		
		// FINCSTP 
		public static void FINCSTP () {}
		
		// FINIT 
		public static void FINIT () {}
		
		// FIST mem16
		public static void FIST (WordMemory target) {}
		public unsafe static void FIST (UInt16 *target) {}
		
		// FIST mem32
		public static void FIST (DWordMemory target) {}
		public unsafe static void FIST (UInt32 *target) {}
		
		// FISTP mem16
		public static void FISTP (WordMemory target) {}
		public unsafe static void FISTP (UInt16 *target) {}
		
		// FISTP mem32
		public static void FISTP (DWordMemory target) {}
		public unsafe static void FISTP (UInt32 *target) {}
		
		// FISTP mem64
		public static void FISTP (QWordMemory target) {}
		
		// FISUB mem16
		public static void FISUB (WordMemory target) {}
		public unsafe static void FISUB (UInt16 *target) {}
		
		// FISUB mem32
		public static void FISUB (DWordMemory target) {}
		public unsafe static void FISUB (UInt32 *target) {}
		
		// FISUBR mem16
		public static void FISUBR (WordMemory target) {}
		public unsafe static void FISUBR (UInt16 *target) {}
		
		// FISUBR mem32
		public static void FISUBR (DWordMemory target) {}
		public unsafe static void FISUBR (UInt32 *target) {}
		
		// FLD mem32
		public static void FLD (DWordMemory target) {}
		public unsafe static void FLD (UInt32 *target) {}
		
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
		public unsafe static void FLDCW (UInt16 *target) {}
		
		// FLDENV mem
		public static void FLDENV (Memory target) {}
		public unsafe static void FLDENV (byte *target) {}
		
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
		public unsafe static void FMUL (UInt32 *target) {}
		
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
		public unsafe static void FNSAVE (byte *target) {}
		
		// FNSTCW mem16
		public static void FNSTCW (WordMemory target) {}
		public unsafe static void FNSTCW (UInt16 *target) {}
		
		// FNSTENV mem
		public static void FNSTENV (Memory target) {}
		public unsafe static void FNSTENV (byte *target) {}
		
		// FNSTSW mem16
		public static void FNSTSW (WordMemory target) {}
		public unsafe static void FNSTSW (UInt16 *target) {}
		
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
		public unsafe static void FRSTOR (byte *target) {}
		
		// FSAVE mem
		public static void FSAVE (Memory target) {}
		public unsafe static void FSAVE (byte *target) {}
		
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
		public unsafe static void FST (UInt32 *target) {}
		
		// FST mem64
		public static void FST (QWordMemory target) {}
		
		// FST fpureg
		public static void FST (FPType target) {}
		
		// FSTCW mem16
		public static void FSTCW (WordMemory target) {}
		public unsafe static void FSTCW (UInt16 *target) {}
		
		// FSTENV mem
		public static void FSTENV (Memory target) {}
		public unsafe static void FSTENV (byte *target) {}
		
		// FSTP mem32
		public static void FSTP (DWordMemory target) {}
		public unsafe static void FSTP (UInt32 *target) {}
		
		// FSTP mem64
		public static void FSTP (QWordMemory target) {}
		
		// FSTP mem80
		public static void FSTP (TWordMemory target) {}
		
		// FSTP fpureg
		public static void FSTP (FPType target) {}
		
		// FSTSW mem16
		public static void FSTSW (WordMemory target) {}
		public unsafe static void FSTSW (UInt16 *target) {}
		
		// FSTSW AX
		public static void FSTSW_AX () {}
		
		// FSUB mem32
		public static void FSUB (DWordMemory target) {}
		public unsafe static void FSUB (UInt32 *target) {}
		
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
		public unsafe static void FSUBR (UInt32 *target) {}
		
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
		public unsafe static void FXRSTOR (byte *target) {}
		
		// FXSAVE memory
		public static void FXSAVE (Memory target) {}
		public unsafe static void FXSAVE (byte *target) {}
		
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
		public unsafe static void IDIV (byte *target) {}
		
		// IDIV mem16
		public static void IDIV (WordMemory target) {}
		public unsafe static void IDIV (UInt16 *target) {}
		
		// IDIV mem32
		public static void IDIV (DWordMemory target) {}
		public unsafe static void IDIV (UInt32 *target) {}
		
		// IDIV rmreg8
		public static void IDIV (R8Type target) {}
		
		// IDIV rmreg16
		public static void IDIV (R16Type target) {}
		
		// IDIV rmreg32
		public static void IDIV (R32Type target) {}
		
		// IMUL mem8
		public static void IMUL (ByteMemory target) {}
		public unsafe static void IMUL (byte *target) {}
		
		// IMUL mem16
		public static void IMUL (WordMemory target) {}
		public unsafe static void IMUL (UInt16 *target) {}
		
		// IMUL mem32
		public static void IMUL (DWordMemory target) {}
		public unsafe static void IMUL (UInt32 *target) {}
		
		// IMUL reg16,mem16
		public static void IMUL (R16Type target, WordMemory source) {}
		public unsafe static void IMUL (R16Type target, UInt16 *source) {}
		
		// IMUL reg32,mem32
		public static void IMUL (R32Type target, DWordMemory source) {}
		public unsafe static void IMUL (R32Type target, UInt32 *source) {}
		
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
		public unsafe static void IMUL (R16Type target, UInt16 *source, Byte value) {}
		
		// IMUL reg16,mem16,imm16
		public static void IMUL (R16Type target, WordMemory source, UInt16 value) {}
		public unsafe static void IMUL (R16Type target, UInt16 *source, UInt16 value) {}
		
		// IMUL reg32,mem32,imm8
		public static void IMUL (R32Type target, DWordMemory source, Byte value) {}
		public unsafe static void IMUL (R32Type target, UInt32 *source, Byte value) {}
		
		// IMUL reg32,mem32,imm32
		public static void IMUL (R32Type target, DWordMemory source, UInt32 value) {}
		public unsafe static void IMUL (R32Type target, UInt32 *source, UInt32 value) {}
		
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
		public unsafe static void INC (byte *target) {}
		
		// INC mem16
		public static void INC (WordMemory target) {}
		public unsafe static void INC (UInt16 *target) {}
		
		// INC mem32
		public static void INC (DWordMemory target) {}
		public unsafe static void INC (UInt32 *target) {}
		
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
		public unsafe static void INVLPG (byte *target) {}
		
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
		public unsafe static void JMP_FAR (byte *target) {}
		
		public static void JMP_FAR (string label) {}
		
		// JMP FAR mem32
		public static void JMP_FAR (DWordMemory target) {}
		public unsafe static void JMP_FAR (UInt32 *target) {}
		
		// JMP mem16
		public static void JMP (WordMemory target) {}
		public unsafe static void JMP (UInt16 *target) {}
		
		// JMP mem32
		public static void JMP (DWordMemory target) {}
		public unsafe static void JMP (UInt32 *target) {}
		
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
		public unsafe static void LAR (R16Type target, UInt16 *source) {}
		
		// LAR reg32,mem32
		public static void LAR (R32Type target, DWordMemory source) {}
		public unsafe static void LAR (R32Type target, UInt32 *source) {}
		
		// LAR reg16,rmreg16
		public static void LAR (R16Type target, R16Type source) {}
		
		// LAR reg32,rmreg32
		public static void LAR (R32Type target, R32Type source) {}
		
		// LDS reg16,mem
		public static void LDS (R16Type target, Memory source) {}
		public unsafe static void LDS (R16Type target, byte *source) {}
		
		// LDS reg32,mem
		public static void LDS (R32Type target, Memory source) {}
		public unsafe static void LDS (R32Type target, byte *source) {}
		
		// LEA reg16,mem
		public static void LEA (R16Type target, Memory source) {}
		public unsafe static void LEA (R16Type target, byte *source) {}
		
		// LEA reg32,mem
		public static void LEA (R32Type target, Memory source) {}
		public unsafe static void LEA (R32Type target, byte *source) {}
		
		// LEAVE 
		public static void LEAVE () {}
		
		// LES reg16,mem
		public static void LES (R16Type target, Memory source) {}
		public unsafe static void LES (R16Type target, byte *source) {}
		
		// LES reg32,mem
		public static void LES (R32Type target, Memory source) {}
		public unsafe static void LES (R32Type target, byte *source) {}
		
		// LFENCE 
		public static void LFENCE () {}
		
		// LFS reg16,mem
		public static void LFS (R16Type target, Memory source) {}
		public unsafe static void LFS (R16Type target, byte *source) {}
		
		// LFS reg32,mem
		public static void LFS (R32Type target, Memory source) {}
		public unsafe static void LFS (R32Type target, byte *source) {}
		
		// LGDT mem
		public static void LGDT (Memory target) {}
		public unsafe static void LGDT (byte *target) {}
		
		// LGS reg16,mem
		public static void LGS (R16Type target, Memory source) {}
		public unsafe static void LGS (R16Type target, byte *source) {}
		
		// LGS reg32,mem
		public static void LGS (R32Type target, Memory source) {}
		public unsafe static void LGS (R32Type target, byte *source) {}
		
		// LIDT mem
		public static void LIDT (Memory target) {}
		public unsafe static void LIDT (byte *target) {}
		
		// LLDT mem16
		public static void LLDT (WordMemory target) {}
		public unsafe static void LLDT (UInt16 *target) {}
		
		// LLDT rmreg16
		public static void LLDT (R16Type target) {}
		
		// LMSW mem16
		public static void LMSW (WordMemory target) {}
		public unsafe static void LMSW (UInt16 *target) {}
		
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
		public unsafe static void LSL (R16Type target, UInt16 *source) {}
		
		// LSL reg32,mem32
		public static void LSL (R32Type target, DWordMemory source) {}
		public unsafe static void LSL (R32Type target, UInt32 *source) {}
		
		// LSL reg16,rmreg16
		public static void LSL (R16Type target, R16Type source) {}
		
		// LSL reg32,rmreg32
		public static void LSL (R32Type target, R32Type source) {}
		
		// LSS reg16,mem
		public static void LSS (R16Type target, Memory source) {}
		public unsafe static void LSS (R16Type target, byte *source) {}
		
		// LSS reg32,mem
		public static void LSS (R32Type target, Memory source) {}
		public unsafe static void LSS (R32Type target, byte *source) {}
		
		// LTR mem16
		public static void LTR (WordMemory target) {}
		public unsafe static void LTR (UInt16 *target) {}
		
		// LTR rmreg16
		public static void LTR (R16Type target) {}
		
		// MFENCE 
		public static void MFENCE () {}
		
		// MOV mem8,reg8
		public static void MOV (ByteMemory target, R8Type source) {}
		public unsafe static void MOV (byte *target, R8Type source) {}
		
		// MOV mem16,reg16
		public static void MOV (WordMemory target, R16Type source) {}
		public unsafe static void MOV (UInt16 *target, R16Type source) {}
		
		// MOV mem32,reg32
		public static void MOV (DWordMemory target, R32Type source) {}
		public unsafe static void MOV (UInt32 *target, R32Type source) {}
		
		// MOV reg8,mem8
		public static void MOV (R8Type target, ByteMemory source) {}
		public unsafe static void MOV (R8Type target, byte *source) {}
		
		// MOV reg16,mem16
		public static void MOV (R16Type target, WordMemory source) {}
		public unsafe static void MOV (R16Type target, UInt16 *source) {}
		
		// MOV reg32,mem32
		public static void MOV (R32Type target, DWordMemory source) {}
		public unsafe static void MOV (R32Type target, UInt32 *source) {}
		
		// MOV reg8,imm8
		public static void MOV (R8Type target, Byte source) {}
		
		// MOV reg16,imm16
		public static void MOV (R16Type target, UInt16 source) {}
		
		// MOV reg32,imm32
		public static void MOV (R32Type target, UInt32 source) {}
		
		// MOV mem8,imm8
		public static void MOV (ByteMemory target, Byte source) {}
		public unsafe static void MOV (byte *target, Byte source) {}
		
		// MOV mem16,imm16
		public static void MOV (WordMemory target, UInt16 source) {}
		public unsafe static void MOV (UInt16 *target, UInt16 source) {}
		
		// MOV mem32,imm32
		public static void MOV (DWordMemory target, UInt32 source) {}
		public unsafe static void MOV (UInt32 *target, UInt32 source) {}
		
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
		public unsafe static void MOV (UInt16 *target, SegType source) {}
		
		// MOV mem32,segreg
		public static void MOV (DWordMemory target, SegType source) {}
		public unsafe static void MOV (UInt32 *target, SegType source) {}
		
		// MOV segreg,mem16
		public static void MOV (SegType target, WordMemory source) {}
		public unsafe static void MOV (SegType target, UInt16 *source) {}
		
		// MOV segreg,mem32
		public static void MOV (SegType target, DWordMemory source) {}
		public unsafe static void MOV (SegType target, UInt32 *source) {}
		
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
		public unsafe static void MOVSX (R16Type target, byte *source) {}
		
		// MOVSX reg32,mem8
		public static void MOVSX (R32Type target, ByteMemory source) {}
		public unsafe static void MOVSX (R32Type target, byte *source) {}
		
		// MOVSX reg32,mem16
		public static void MOVSX (R32Type target, WordMemory source) {}
		public unsafe static void MOVSX (R32Type target, UInt16 *source) {}
		
		// MOVSX reg16,rmreg8
		public static void MOVSX (R16Type target, R8Type source) {}
		
		// MOVSX reg32,rmreg8
		public static void MOVSX (R32Type target, R8Type source) {}
		
		// MOVSX reg32,rmreg16
		public static void MOVSX (R32Type target, R16Type source) {}
		
		// MOVZX reg16,mem8
		public static void MOVZX (R16Type target, ByteMemory source) {}
		public unsafe static void MOVZX (R16Type target, byte *source) {}
		
		// MOVZX reg32,mem8
		public static void MOVZX (R32Type target, ByteMemory source) {}
		public unsafe static void MOVZX (R32Type target, byte *source) {}
		
		// MOVZX reg32,mem16
		public static void MOVZX (R32Type target, WordMemory source) {}
		public unsafe static void MOVZX (R32Type target, UInt16 *source) {}
		
		// MOVZX reg16,rmreg8
		public static void MOVZX (R16Type target, R8Type source) {}
		
		// MOVZX reg32,rmreg8
		public static void MOVZX (R32Type target, R8Type source) {}
		
		// MOVZX reg32,rmreg16
		public static void MOVZX (R32Type target, R16Type source) {}
		
		// MUL mem8
		public static void MUL (ByteMemory target) {}
		public unsafe static void MUL (byte *target) {}
		
		// MUL mem16
		public static void MUL (WordMemory target) {}
		public unsafe static void MUL (UInt16 *target) {}
		
		// MUL mem32
		public static void MUL (DWordMemory target) {}
		public unsafe static void MUL (UInt32 *target) {}
		
		// MUL rmreg8
		public static void MUL (R8Type target) {}
		
		// MUL rmreg16
		public static void MUL (R16Type target) {}
		
		// MUL rmreg32
		public static void MUL (R32Type target) {}
		
		// NEG mem8
		public static void NEG (ByteMemory target) {}
		public unsafe static void NEG (byte *target) {}
		
		// NEG mem16
		public static void NEG (WordMemory target) {}
		public unsafe static void NEG (UInt16 *target) {}
		
		// NEG mem32
		public static void NEG (DWordMemory target) {}
		public unsafe static void NEG (UInt32 *target) {}
		
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
		public unsafe static void NOT (byte *target) {}
		
		// NOT mem16
		public static void NOT (WordMemory target) {}
		public unsafe static void NOT (UInt16 *target) {}
		
		// NOT mem32
		public static void NOT (DWordMemory target) {}
		public unsafe static void NOT (UInt32 *target) {}
		
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
		public unsafe static void OR (byte *target, R8Type source) {}
		
		// OR mem16,reg16
		public static void OR (WordMemory target, R16Type source) {}
		public unsafe static void OR (UInt16 *target, R16Type source) {}
		
		// OR mem32,reg32
		public static void OR (DWordMemory target, R32Type source) {}
		public unsafe static void OR (UInt32 *target, R32Type source) {}
		
		// OR reg8,mem8
		public static void OR (R8Type target, ByteMemory source) {}
		public unsafe static void OR (R8Type target, byte *source) {}
		
		// OR reg16,mem16
		public static void OR (R16Type target, WordMemory source) {}
		public unsafe static void OR (R16Type target, UInt16 *source) {}
		
		// OR reg32,mem32
		public static void OR (R32Type target, DWordMemory source) {}
		public unsafe static void OR (R32Type target, UInt32 *source) {}
		
		// OR mem8,imm8
		public static void OR (ByteMemory target, Byte source) {}
		public unsafe static void OR (byte *target, Byte source) {}
		
		// OR mem16,imm16
		public static void OR (WordMemory target, UInt16 source) {}
		public unsafe static void OR (UInt16 *target, UInt16 source) {}
		
		// OR mem32,imm32
		public static void OR (DWordMemory target, UInt32 source) {}
		public unsafe static void OR (UInt32 *target, UInt32 source) {}
		
		// OR mem16,imm8
		public static void OR (WordMemory target, Byte source) {}
		public unsafe static void OR (UInt16 *target, Byte source) {}
		
		// OR mem32,imm8
		public static void OR (DWordMemory target, Byte source) {}
		public unsafe static void OR (UInt32 *target, Byte source) {}
		
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
		public unsafe static void POP (UInt16 *target) {}
		
		// POP mem32
		public static void POP (DWordMemory target) {}
		public unsafe static void POP (UInt32 *target) {}
		
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
		public unsafe static void PREFETCHNTA (byte *target) {}
		
		// PREFETCHT0 m8
		public static void PREFETCHT0 (Memory target) {}
		public unsafe static void PREFETCHT0 (byte *target) {}
		
		// PREFETCHT1 m8
		public static void PREFETCHT1 (Memory target) {}
		public unsafe static void PREFETCHT1 (byte *target) {}
		
		// PREFETCHT2 m8
		public static void PREFETCHT2 (Memory target) {}
		public unsafe static void PREFETCHT2 (byte *target) {}
		
		// PUSH reg16
		public static void PUSH (R16Type target) {}
		
		// PUSH reg32
		public static void PUSH (R32Type target) {}
		
		// PUSH mem16
		public static void PUSH (WordMemory target) {}
		public unsafe static void PUSH (UInt16 *target) {}
		
		// PUSH mem32
		public static void PUSH (DWordMemory target) {}
		public unsafe static void PUSH (UInt32 *target) {}
		
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
		public unsafe static void RCL__CL (byte *target) {}
		
		// RCL mem8,imm8
		public static void RCL (ByteMemory target, Byte source) {}
		public unsafe static void RCL (byte *target, Byte source) {}
		
		// RCL mem16,CL
		public static void RCL__CL (WordMemory target) {}
		public unsafe static void RCL__CL (UInt16 *target) {}
		
		// RCL mem16,imm8
		public static void RCL (WordMemory target, Byte source) {}
		public unsafe static void RCL (UInt16 *target, Byte source) {}
		
		// RCL mem32,CL
		public static void RCL__CL (DWordMemory target) {}
		public unsafe static void RCL__CL (UInt32 *target) {}
		
		// RCL mem32,imm8
		public static void RCL (DWordMemory target, Byte source) {}
		public unsafe static void RCL (UInt32 *target, Byte source) {}
		
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
		public unsafe static void RCR__CL (byte *target) {}
		
		// RCR mem8,imm8
		public static void RCR (ByteMemory target, Byte source) {}
		public unsafe static void RCR (byte *target, Byte source) {}
		
		// RCR mem16,CL
		public static void RCR__CL (WordMemory target) {}
		public unsafe static void RCR__CL (UInt16 *target) {}
		
		// RCR mem16,imm8
		public static void RCR (WordMemory target, Byte source) {}
		public unsafe static void RCR (UInt16 *target, Byte source) {}
		
		// RCR mem32,CL
		public static void RCR__CL (DWordMemory target) {}
		public unsafe static void RCR__CL (UInt32 *target) {}
		
		// RCR mem32,imm8
		public static void RCR (DWordMemory target, Byte source) {}
		public unsafe static void RCR (UInt32 *target, Byte source) {}
		
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
		public unsafe static void ROL__CL (byte *target) {}
		
		// ROL mem8,imm8
		public static void ROL (ByteMemory target, Byte source) {}
		public unsafe static void ROL (byte *target, Byte source) {}
		
		// ROL mem16,CL
		public static void ROL__CL (WordMemory target) {}
		public unsafe static void ROL__CL (UInt16 *target) {}
		
		// ROL mem16,imm8
		public static void ROL (WordMemory target, Byte source) {}
		public unsafe static void ROL (UInt16 *target, Byte source) {}
		
		// ROL mem32,CL
		public static void ROL__CL (DWordMemory target) {}
		public unsafe static void ROL__CL (UInt32 *target) {}
		
		// ROL mem32,imm8
		public static void ROL (DWordMemory target, Byte source) {}
		public unsafe static void ROL (UInt32 *target, Byte source) {}
		
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
		public unsafe static void ROR__CL (byte *target) {}
		
		// ROR mem8,imm8
		public static void ROR (ByteMemory target, Byte source) {}
		public unsafe static void ROR (byte *target, Byte source) {}
		
		// ROR mem16,CL
		public static void ROR__CL (WordMemory target) {}
		public unsafe static void ROR__CL (UInt16 *target) {}
		
		// ROR mem16,imm8
		public static void ROR (WordMemory target, Byte source) {}
		public unsafe static void ROR (UInt16 *target, Byte source) {}
		
		// ROR mem32,CL
		public static void ROR__CL (DWordMemory target) {}
		public unsafe static void ROR__CL (UInt32 *target) {}
		
		// ROR mem32,imm8
		public static void ROR (DWordMemory target, Byte source) {}
		public unsafe static void ROR (UInt32 *target, Byte source) {}
		
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
		public unsafe static void SAL__CL (byte *target) {}
		
		// SAL mem8,imm8
		public static void SAL (ByteMemory target, Byte source) {}
		public unsafe static void SAL (byte *target, Byte source) {}
		
		// SAL mem16,CL
		public static void SAL__CL (WordMemory target) {}
		public unsafe static void SAL__CL (UInt16 *target) {}
		
		// SAL mem16,imm8
		public static void SAL (WordMemory target, Byte source) {}
		public unsafe static void SAL (UInt16 *target, Byte source) {}
		
		// SAL mem32,CL
		public static void SAL__CL (DWordMemory target) {}
		public unsafe static void SAL__CL (UInt32 *target) {}
		
		// SAL mem32,imm8
		public static void SAL (DWordMemory target, Byte source) {}
		public unsafe static void SAL (UInt32 *target, Byte source) {}
		
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
		public unsafe static void SAR__CL (byte *target) {}
		
		// SAR mem8,imm8
		public static void SAR (ByteMemory target, Byte source) {}
		public unsafe static void SAR (byte *target, Byte source) {}
		
		// SAR mem16,CL
		public static void SAR__CL (WordMemory target) {}
		public unsafe static void SAR__CL (UInt16 *target) {}
		
		// SAR mem16,imm8
		public static void SAR (WordMemory target, Byte source) {}
		public unsafe static void SAR (UInt16 *target, Byte source) {}
		
		// SAR mem32,CL
		public static void SAR__CL (DWordMemory target) {}
		public unsafe static void SAR__CL (UInt32 *target) {}
		
		// SAR mem32,imm8
		public static void SAR (DWordMemory target, Byte source) {}
		public unsafe static void SAR (UInt32 *target, Byte source) {}
		
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
		public unsafe static void SBB (byte *target, R8Type source) {}
		
		// SBB mem16,reg16
		public static void SBB (WordMemory target, R16Type source) {}
		public unsafe static void SBB (UInt16 *target, R16Type source) {}
		
		// SBB mem32,reg32
		public static void SBB (DWordMemory target, R32Type source) {}
		public unsafe static void SBB (UInt32 *target, R32Type source) {}
		
		// SBB reg8,mem8
		public static void SBB (R8Type target, ByteMemory source) {}
		public unsafe static void SBB (R8Type target, byte *source) {}
		
		// SBB reg16,mem16
		public static void SBB (R16Type target, WordMemory source) {}
		public unsafe static void SBB (R16Type target, UInt16 *source) {}
		
		// SBB reg32,mem32
		public static void SBB (R32Type target, DWordMemory source) {}
		public unsafe static void SBB (R32Type target, UInt32 *source) {}
		
		// SBB mem8,imm8
		public static void SBB (ByteMemory target, Byte source) {}
		public unsafe static void SBB (byte *target, Byte source) {}
		
		// SBB mem16,imm16
		public static void SBB (WordMemory target, UInt16 source) {}
		public unsafe static void SBB (UInt16 *target, UInt16 source) {}
		
		// SBB mem32,imm32
		public static void SBB (DWordMemory target, UInt32 source) {}
		public unsafe static void SBB (UInt32 *target, UInt32 source) {}
		
		// SBB mem16,imm8
		public static void SBB (WordMemory target, Byte source) {}
		public unsafe static void SBB (UInt16 *target, Byte source) {}
		
		// SBB mem32,imm8
		public static void SBB (DWordMemory target, Byte source) {}
		public unsafe static void SBB (UInt32 *target, Byte source) {}
		
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
		public unsafe static void SETA (byte *target) {}
		
		// SETA rmreg8
		public static void SETA (R8Type target) {}
		
		// SETAE mem8
		public static void SETAE (ByteMemory target) {}
		public unsafe static void SETAE (byte *target) {}
		
		// SETAE rmreg8
		public static void SETAE (R8Type target) {}
		
		// SETB mem8
		public static void SETB (ByteMemory target) {}
		public unsafe static void SETB (byte *target) {}
		
		// SETB rmreg8
		public static void SETB (R8Type target) {}
		
		// SETBE mem8
		public static void SETBE (ByteMemory target) {}
		public unsafe static void SETBE (byte *target) {}
		
		// SETBE rmreg8
		public static void SETBE (R8Type target) {}
		
		// SETC mem8
		public static void SETC (ByteMemory target) {}
		public unsafe static void SETC (byte *target) {}
		
		// SETC rmreg8
		public static void SETC (R8Type target) {}
		
		// SETE mem8
		public static void SETE (ByteMemory target) {}
		public unsafe static void SETE (byte *target) {}
		
		// SETE rmreg8
		public static void SETE (R8Type target) {}
		
		// SETG mem8
		public static void SETG (ByteMemory target) {}
		public unsafe static void SETG (byte *target) {}
		
		// SETG rmreg8
		public static void SETG (R8Type target) {}
		
		// SETGE mem8
		public static void SETGE (ByteMemory target) {}
		public unsafe static void SETGE (byte *target) {}
		
		// SETGE rmreg8
		public static void SETGE (R8Type target) {}
		
		// SETL mem8
		public static void SETL (ByteMemory target) {}
		public unsafe static void SETL (byte *target) {}
		
		// SETL rmreg8
		public static void SETL (R8Type target) {}
		
		// SETLE mem8
		public static void SETLE (ByteMemory target) {}
		public unsafe static void SETLE (byte *target) {}
		
		// SETLE rmreg8
		public static void SETLE (R8Type target) {}
		
		// SETNA mem8
		public static void SETNA (ByteMemory target) {}
		public unsafe static void SETNA (byte *target) {}
		
		// SETNA rmreg8
		public static void SETNA (R8Type target) {}
		
		// SETNAE mem8
		public static void SETNAE (ByteMemory target) {}
		public unsafe static void SETNAE (byte *target) {}
		
		// SETNAE rmreg8
		public static void SETNAE (R8Type target) {}
		
		// SETNB mem8
		public static void SETNB (ByteMemory target) {}
		public unsafe static void SETNB (byte *target) {}
		
		// SETNB rmreg8
		public static void SETNB (R8Type target) {}
		
		// SETNBE mem8
		public static void SETNBE (ByteMemory target) {}
		public unsafe static void SETNBE (byte *target) {}
		
		// SETNBE rmreg8
		public static void SETNBE (R8Type target) {}
		
		// SETNC mem8
		public static void SETNC (ByteMemory target) {}
		public unsafe static void SETNC (byte *target) {}
		
		// SETNC rmreg8
		public static void SETNC (R8Type target) {}
		
		// SETNE mem8
		public static void SETNE (ByteMemory target) {}
		public unsafe static void SETNE (byte *target) {}
		
		// SETNE rmreg8
		public static void SETNE (R8Type target) {}
		
		// SETNG mem8
		public static void SETNG (ByteMemory target) {}
		public unsafe static void SETNG (byte *target) {}
		
		// SETNG rmreg8
		public static void SETNG (R8Type target) {}
		
		// SETNGE mem8
		public static void SETNGE (ByteMemory target) {}
		public unsafe static void SETNGE (byte *target) {}
		
		// SETNGE rmreg8
		public static void SETNGE (R8Type target) {}
		
		// SETNL mem8
		public static void SETNL (ByteMemory target) {}
		public unsafe static void SETNL (byte *target) {}
		
		// SETNL rmreg8
		public static void SETNL (R8Type target) {}
		
		// SETNLE mem8
		public static void SETNLE (ByteMemory target) {}
		public unsafe static void SETNLE (byte *target) {}
		
		// SETNLE rmreg8
		public static void SETNLE (R8Type target) {}
		
		// SETNO mem8
		public static void SETNO (ByteMemory target) {}
		public unsafe static void SETNO (byte *target) {}
		
		// SETNO rmreg8
		public static void SETNO (R8Type target) {}
		
		// SETNP mem8
		public static void SETNP (ByteMemory target) {}
		public unsafe static void SETNP (byte *target) {}
		
		// SETNP rmreg8
		public static void SETNP (R8Type target) {}
		
		// SETNS mem8
		public static void SETNS (ByteMemory target) {}
		public unsafe static void SETNS (byte *target) {}
		
		// SETNS rmreg8
		public static void SETNS (R8Type target) {}
		
		// SETNZ mem8
		public static void SETNZ (ByteMemory target) {}
		public unsafe static void SETNZ (byte *target) {}
		
		// SETNZ rmreg8
		public static void SETNZ (R8Type target) {}
		
		// SETO mem8
		public static void SETO (ByteMemory target) {}
		public unsafe static void SETO (byte *target) {}
		
		// SETO rmreg8
		public static void SETO (R8Type target) {}
		
		// SETP mem8
		public static void SETP (ByteMemory target) {}
		public unsafe static void SETP (byte *target) {}
		
		// SETP rmreg8
		public static void SETP (R8Type target) {}
		
		// SETPE mem8
		public static void SETPE (ByteMemory target) {}
		public unsafe static void SETPE (byte *target) {}
		
		// SETPE rmreg8
		public static void SETPE (R8Type target) {}
		
		// SETPO mem8
		public static void SETPO (ByteMemory target) {}
		public unsafe static void SETPO (byte *target) {}
		
		// SETPO rmreg8
		public static void SETPO (R8Type target) {}
		
		// SETS mem8
		public static void SETS (ByteMemory target) {}
		public unsafe static void SETS (byte *target) {}
		
		// SETS rmreg8
		public static void SETS (R8Type target) {}
		
		// SETZ mem8
		public static void SETZ (ByteMemory target) {}
		public unsafe static void SETZ (byte *target) {}
		
		// SETZ rmreg8
		public static void SETZ (R8Type target) {}
		
		// SFENCE 
		public static void SFENCE () {}
		
		// SGDT mem
		public static void SGDT (Memory target) {}
		public unsafe static void SGDT (byte *target) {}
		
		// SHL mem8,CL
		public static void SHL__CL (ByteMemory target) {}
		public unsafe static void SHL__CL (byte *target) {}
		
		// SHL mem8,imm8
		public static void SHL (ByteMemory target, Byte source) {}
		public unsafe static void SHL (byte *target, Byte source) {}
		
		// SHL mem16,CL
		public static void SHL__CL (WordMemory target) {}
		public unsafe static void SHL__CL (UInt16 *target) {}
		
		// SHL mem16,imm8
		public static void SHL (WordMemory target, Byte source) {}
		public unsafe static void SHL (UInt16 *target, Byte source) {}
		
		// SHL mem32,CL
		public static void SHL__CL (DWordMemory target) {}
		public unsafe static void SHL__CL (UInt32 *target) {}
		
		// SHL mem32,imm8
		public static void SHL (DWordMemory target, Byte source) {}
		public unsafe static void SHL (UInt32 *target, Byte source) {}
		
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
		public unsafe static void SHLD (UInt16 *target, R16Type source, Byte value) {}
		
		// SHLD mem32,reg32,imm8
		public static void SHLD (DWordMemory target, R32Type source, Byte value) {}
		public unsafe static void SHLD (UInt32 *target, R32Type source, Byte value) {}
		
		// SHLD mem16,reg16,CL
		public static void SHLD___CL (WordMemory target, R16Type source) {}
		public unsafe static void SHLD___CL (UInt16 *target, R16Type source) {}
		
		// SHLD mem32,reg32,CL
		public static void SHLD___CL (DWordMemory target, R32Type source) {}
		public unsafe static void SHLD___CL (UInt32 *target, R32Type source) {}
		
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
		public unsafe static void SHR__CL (byte *target) {}
		
		// SHR mem8,imm8
		public static void SHR (ByteMemory target, Byte source) {}
		public unsafe static void SHR (byte *target, Byte source) {}
		
		// SHR mem16,CL
		public static void SHR__CL (WordMemory target) {}
		public unsafe static void SHR__CL (UInt16 *target) {}
		
		// SHR mem16,imm8
		public static void SHR (WordMemory target, Byte source) {}
		public unsafe static void SHR (UInt16 *target, Byte source) {}
		
		// SHR mem32,CL
		public static void SHR__CL (DWordMemory target) {}
		public unsafe static void SHR__CL (UInt32 *target) {}
		
		// SHR mem32,imm8
		public static void SHR (DWordMemory target, Byte source) {}
		public unsafe static void SHR (UInt32 *target, Byte source) {}
		
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
		public unsafe static void SHRD (UInt16 *target, R16Type source, Byte value) {}
		
		// SHRD mem32,reg32,imm8
		public static void SHRD (DWordMemory target, R32Type source, Byte value) {}
		public unsafe static void SHRD (UInt32 *target, R32Type source, Byte value) {}
		
		// SHRD mem16,reg16,CL
		public static void SHRD___CL (WordMemory target, R16Type source) {}
		public unsafe static void SHRD___CL (UInt16 *target, R16Type source) {}
		
		// SHRD mem32,reg32,CL
		public static void SHRD___CL (DWordMemory target, R32Type source) {}
		public unsafe static void SHRD___CL (UInt32 *target, R32Type source) {}
		
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
		public unsafe static void SIDT (byte *target) {}
		
		// SLDT mem16
		public static void SLDT (WordMemory target) {}
		public unsafe static void SLDT (UInt16 *target) {}
		
		// SLDT rmreg16
		public static void SLDT (R16Type target) {}
		
		// SMSW mem16
		public static void SMSW (WordMemory target) {}
		public unsafe static void SMSW (UInt16 *target) {}
		
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
		public unsafe static void STR (UInt16 *target) {}
		
		// STR rmreg16
		public static void STR (R16Type target) {}
		
		// SUB mem8,reg8
		public static void SUB (ByteMemory target, R8Type source) {}
		public unsafe static void SUB (byte *target, R8Type source) {}
		
		// SUB mem16,reg16
		public static void SUB (WordMemory target, R16Type source) {}
		public unsafe static void SUB (UInt16 *target, R16Type source) {}
		
		// SUB mem32,reg32
		public static void SUB (DWordMemory target, R32Type source) {}
		public unsafe static void SUB (UInt32 *target, R32Type source) {}
		
		// SUB reg8,mem8
		public static void SUB (R8Type target, ByteMemory source) {}
		public unsafe static void SUB (R8Type target, byte *source) {}
		
		// SUB reg16,mem16
		public static void SUB (R16Type target, WordMemory source) {}
		public unsafe static void SUB (R16Type target, UInt16 *source) {}
		
		// SUB reg32,mem32
		public static void SUB (R32Type target, DWordMemory source) {}
		public unsafe static void SUB (R32Type target, UInt32 *source) {}
		
		// SUB mem8,imm8
		public static void SUB (ByteMemory target, Byte source) {}
		public unsafe static void SUB (byte *target, Byte source) {}
		
		// SUB mem16,imm16
		public static void SUB (WordMemory target, UInt16 source) {}
		public unsafe static void SUB (UInt16 *target, UInt16 source) {}
		
		// SUB mem32,imm32
		public static void SUB (DWordMemory target, UInt32 source) {}
		public unsafe static void SUB (UInt32 *target, UInt32 source) {}
		
		// SUB mem16,imm8
		public static void SUB (WordMemory target, Byte source) {}
		public unsafe static void SUB (UInt16 *target, Byte source) {}
		
		// SUB mem32,imm8
		public static void SUB (DWordMemory target, Byte source) {}
		public unsafe static void SUB (UInt32 *target, Byte source) {}
		
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
		public unsafe static void TEST (byte *target, R8Type source) {}
		
		// TEST mem16,reg16
		public static void TEST (WordMemory target, R16Type source) {}
		public unsafe static void TEST (UInt16 *target, R16Type source) {}
		
		// TEST mem32,reg32
		public static void TEST (DWordMemory target, R32Type source) {}
		public unsafe static void TEST (UInt32 *target, R32Type source) {}
		
		// TEST mem8,imm8
		public static void TEST (ByteMemory target, Byte source) {}
		public unsafe static void TEST (byte *target, Byte source) {}
		
		// TEST mem16,imm16
		public static void TEST (WordMemory target, UInt16 source) {}
		public unsafe static void TEST (UInt16 *target, UInt16 source) {}
		
		// TEST mem32,imm32
		public static void TEST (DWordMemory target, UInt32 source) {}
		public unsafe static void TEST (UInt32 *target, UInt32 source) {}
		
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
		
		// TIMES 
		public static void TIMES (UInt32 length, Byte value) {}
		
		// VERR mem16
		public static void VERR (WordMemory target) {}
		public unsafe static void VERR (UInt16 *target) {}
		
		// VERR rmreg16
		public static void VERR (R16Type target) {}
		
		// VERW mem16
		public static void VERW (WordMemory target) {}
		public unsafe static void VERW (UInt16 *target) {}
		
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
		public unsafe static void XADD (byte *target, R8Type source) {}
		
		// XADD mem16,reg16
		public static void XADD (WordMemory target, R16Type source) {}
		public unsafe static void XADD (UInt16 *target, R16Type source) {}
		
		// XADD mem32,reg32
		public static void XADD (DWordMemory target, R32Type source) {}
		public unsafe static void XADD (UInt32 *target, R32Type source) {}
		
		// XADD rmreg8,reg8
		public static void XADD (R8Type target, R8Type source) {}
		
		// XADD rmreg16,reg16
		public static void XADD (R16Type target, R16Type source) {}
		
		// XADD rmreg32,reg32
		public static void XADD (R32Type target, R32Type source) {}
		
		// XCHG reg8,mem8
		public static void XCHG (R8Type target, ByteMemory source) {}
		public unsafe static void XCHG (R8Type target, byte *source) {}
		
		// XCHG reg16,mem16
		public static void XCHG (R16Type target, WordMemory source) {}
		public unsafe static void XCHG (R16Type target, UInt16 *source) {}
		
		// XCHG reg32,mem32
		public static void XCHG (R32Type target, DWordMemory source) {}
		public unsafe static void XCHG (R32Type target, UInt32 *source) {}
		
		// XCHG mem8,reg8
		public static void XCHG (ByteMemory target, R8Type source) {}
		public unsafe static void XCHG (byte *target, R8Type source) {}
		
		// XCHG mem16,reg16
		public static void XCHG (WordMemory target, R16Type source) {}
		public unsafe static void XCHG (UInt16 *target, R16Type source) {}
		
		// XCHG mem32,reg32
		public static void XCHG (DWordMemory target, R32Type source) {}
		public unsafe static void XCHG (UInt32 *target, R32Type source) {}
		
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
		public unsafe static void XOR (byte *target, R8Type source) {}
		
		// XOR mem16,reg16
		public static void XOR (WordMemory target, R16Type source) {}
		public unsafe static void XOR (UInt16 *target, R16Type source) {}
		
		// XOR mem32,reg32
		public static void XOR (DWordMemory target, R32Type source) {}
		public unsafe static void XOR (UInt32 *target, R32Type source) {}
		
		// XOR reg8,mem8
		public static void XOR (R8Type target, ByteMemory source) {}
		public unsafe static void XOR (R8Type target, byte *source) {}
		
		// XOR reg16,mem16
		public static void XOR (R16Type target, WordMemory source) {}
		public unsafe static void XOR (R16Type target, UInt16 *source) {}
		
		// XOR reg32,mem32
		public static void XOR (R32Type target, DWordMemory source) {}
		public unsafe static void XOR (R32Type target, UInt32 *source) {}
		
		// XOR mem8,imm8
		public static void XOR (ByteMemory target, Byte source) {}
		public unsafe static void XOR (byte *target, Byte source) {}
		
		// XOR mem16,imm16
		public static void XOR (WordMemory target, UInt16 source) {}
		public unsafe static void XOR (UInt16 *target, UInt16 source) {}
		
		// XOR mem32,imm32
		public static void XOR (DWordMemory target, UInt32 source) {}
		public unsafe static void XOR (UInt32 *target, UInt32 source) {}
		
		// XOR mem16,imm8
		public static void XOR (WordMemory target, Byte source) {}
		public unsafe static void XOR (UInt16 *target, Byte source) {}
		
		// XOR mem32,imm8
		public static void XOR (DWordMemory target, Byte source) {}
		public unsafe static void XOR (UInt32 *target, Byte source) {}
		
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
	
}
