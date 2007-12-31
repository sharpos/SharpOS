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
	/// <summary>
	/// This class encapsulates machine code stubs, that when used by kernel code, 
	/// and then AOTed, are expressed as their respective machine language operations.
	/// </summary>
	public class Asm {

		/// <summary>
		/// AAA 
		/// </summary>
		public static void AAA ()
		{
		}

		/// <summary>
		/// AAD 
		/// </summary>
		public static void AAD ()
		{
		}

		/// <summary>
		/// AAD imm8
		/// </summary>
		public static void AAD (Byte target)
		{
		}

		/// <summary>
		/// AAM 
		/// </summary>
		public static void AAM ()
		{
		}

		/// <summary>
		/// AAM imm8
		/// </summary>
		public static void AAM (Byte target)
		{
		}

		/// <summary>
		/// AAS 
		/// </summary>
		public static void AAS ()
		{
		}

		/// <summary>
		/// ADC mem8,reg8
		/// </summary>
		public static void ADC (ByteMemory target, R8Type source)
		{
		}

		/// <summary>
		/// ADC mem8,reg8
		/// </summary>
		public unsafe static void ADC (byte* target, R8Type source)
		{
		}

		/// <summary>
		/// ADC mem16,reg16
		/// </summary>
		public static void ADC (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// ADC mem16,reg16
		/// </summary>
		public unsafe static void ADC (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// ADC mem32,reg32
		/// </summary>
		public static void ADC (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// ADC mem32,reg32
		/// </summary>
		public unsafe static void ADC (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// ADC reg8,mem8
		/// </summary>
		public static void ADC (R8Type target, ByteMemory source)
		{
		}

		/// <summary>
		/// ADC reg8,mem8
		/// </summary>
		public unsafe static void ADC (R8Type target, byte* source)
		{
		}

		/// <summary>
		/// ADC reg16,mem16
		/// </summary>
		public static void ADC (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// ADC reg16,mem16
		/// </summary>
		public unsafe static void ADC (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// ADC reg32,mem32
		/// </summary>
		public static void ADC (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// ADC reg32,mem32
		/// </summary>
		public unsafe static void ADC (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// ADC mem8,imm8
		/// </summary>
		public static void ADC (ByteMemory target, Byte source)
		{
		}

		/// <summary>
		/// ADC mem8,imm8
		/// </summary>
		public unsafe static void ADC (byte* target, Byte source)
		{
		}

		/// <summary>
		/// ADC mem16,imm16
		/// </summary>
		public static void ADC (WordMemory target, UInt16 source)
		{
		}

		/// <summary>
		/// ADC mem16,imm16
		/// </summary>
		public unsafe static void ADC (UInt16* target, UInt16 source)
		{
		}

		/// <summary>
		/// ADC mem32,imm32
		/// </summary>
		public static void ADC (DWordMemory target, UInt32 source)
		{
		}

		/// <summary>
		/// ADC mem32,imm32
		/// </summary>
		public unsafe static void ADC (UInt32* target, UInt32 source)
		{
		}

		/// <summary>
		/// ADC mem16,imm8
		/// </summary>
		public static void ADC (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// ADC mem16,imm8
		/// </summary>
		public unsafe static void ADC (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// ADC mem32,imm8
		/// </summary>
		public static void ADC (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// ADC mem32,imm8
		/// </summary>
		public unsafe static void ADC (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// ADC rmreg8,reg8
		/// </summary>
		public static void ADC (R8Type target, R8Type source)
		{
		}

		/// <summary>
		/// ADC rmreg16,reg16
		/// </summary>
		public static void ADC (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// ADC rmreg32,reg32
		/// </summary>
		public static void ADC (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// ADC rmreg8,imm8
		/// </summary>
		public static void ADC (R8Type target, Byte source)
		{
		}

		/// <summary>
		/// ADC rmreg16,imm16
		/// </summary>
		public static void ADC (R16Type target, UInt16 source)
		{
		}

		/// <summary>
		/// ADC rmreg32,imm32
		/// </summary>
		public static void ADC (R32Type target, UInt32 source)
		{
		}

		/// <summary>
		/// ADC rmreg16,imm8
		/// </summary>
		public static void ADC (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// ADC rmreg32,imm8
		/// </summary>
		public static void ADC (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// ADD mem8,reg8
		/// </summary>
		public static void ADD (ByteMemory target, R8Type source)
		{
		}

		/// <summary>
		/// ADD mem8,reg8
		/// </summary>
		public unsafe static void ADD (byte* target, R8Type source)
		{
		}

		/// <summary>
		/// ADD mem16,reg16
		/// </summary>
		public static void ADD (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// ADD mem16,reg16
		/// </summary>
		public unsafe static void ADD (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// ADD mem32,reg32
		/// </summary>
		public static void ADD (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// ADD mem32,reg32
		/// </summary>
		public unsafe static void ADD (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// ADD reg8,mem8
		/// </summary>
		public static void ADD (R8Type target, ByteMemory source)
		{
		}

		/// <summary>
		/// ADD reg8,mem8
		/// </summary>
		public unsafe static void ADD (R8Type target, byte* source)
		{
		}

		/// <summary>
		/// ADD reg16,mem16
		/// </summary>
		public static void ADD (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// ADD reg16,mem16
		/// </summary>
		public unsafe static void ADD (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// ADD reg32,mem32
		/// </summary>
		public static void ADD (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// ADD reg32,mem32
		/// </summary>
		public unsafe static void ADD (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// ADD mem8,imm8
		/// </summary>
		public static void ADD (ByteMemory target, Byte source)
		{
		}

		/// <summary>
		/// ADD mem8,imm8
		/// </summary>
		public unsafe static void ADD (byte* target, Byte source)
		{
		}

		/// <summary>
		/// ADD mem16,imm16
		/// </summary>
		public static void ADD (WordMemory target, UInt16 source)
		{
		}

		/// <summary>
		/// ADD mem16,imm16
		/// </summary>
		public unsafe static void ADD (UInt16* target, UInt16 source)
		{
		}

		/// <summary>
		/// ADD mem32,imm32
		/// </summary>
		public static void ADD (DWordMemory target, UInt32 source)
		{
		}

		/// <summary>
		/// ADD mem32,imm32
		/// </summary>
		public unsafe static void ADD (UInt32* target, UInt32 source)
		{
		}

		/// <summary>
		/// ADD mem16,imm8
		/// </summary>
		public static void ADD (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// ADD mem16,imm8
		/// </summary>
		public unsafe static void ADD (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// ADD mem32,imm8
		/// </summary>
		public static void ADD (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// ADD mem32,imm8
		/// </summary>
		public unsafe static void ADD (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// ADD rmreg8,reg8
		/// </summary>
		public static void ADD (R8Type target, R8Type source)
		{
		}

		/// <summary>
		/// ADD rmreg16,reg16
		/// </summary>
		public static void ADD (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// ADD rmreg32,reg32
		/// </summary>
		public static void ADD (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// ADD rmreg8,imm8
		/// </summary>
		public static void ADD (R8Type target, Byte source)
		{
		}

		/// <summary>
		/// ADD rmreg16,imm16
		/// </summary>
		public static void ADD (R16Type target, UInt16 source)
		{
		}

		/// <summary>
		/// ADD rmreg32,imm32
		/// </summary>
		public static void ADD (R32Type target, UInt32 source)
		{
		}

		/// <summary>
		/// ADD rmreg16,imm8
		/// </summary>
		public static void ADD (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// ADD rmreg32,imm8
		/// </summary>
		public static void ADD (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// ALIGN 
		/// </summary>
		public static void ALIGN (UInt32 value)
		{
		}

		/// <summary>
		/// AND mem8,reg8
		/// </summary>
		public static void AND (ByteMemory target, R8Type source)
		{
		}

		/// <summary>
		/// AND mem8,reg8
		/// </summary>
		public unsafe static void AND (byte* target, R8Type source)
		{
		}

		/// <summary>
		/// AND mem16,reg16
		/// </summary>
		public static void AND (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// AND mem16,reg16
		/// </summary>
		public unsafe static void AND (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// AND mem32,reg32
		/// </summary>
		public static void AND (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// AND mem32,reg32
		/// </summary>
		public unsafe static void AND (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// AND reg8,mem8
		/// </summary>
		public static void AND (R8Type target, ByteMemory source)
		{
		}

		/// <summary>
		/// AND reg8,mem8
		/// </summary>
		public unsafe static void AND (R8Type target, byte* source)
		{
		}

		/// <summary>
		/// AND reg16,mem16
		/// </summary>
		public static void AND (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// AND reg16,mem16
		/// </summary>
		public unsafe static void AND (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// AND reg32,mem32
		/// </summary>
		public static void AND (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// AND reg32,mem32
		/// </summary>
		public unsafe static void AND (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// AND mem8,imm8
		/// </summary>
		public static void AND (ByteMemory target, Byte source)
		{
		}

		/// <summary>
		/// AND mem8,imm8
		/// </summary>
		public unsafe static void AND (byte* target, Byte source)
		{
		}

		/// <summary>
		/// AND mem16,imm16
		/// </summary>
		public static void AND (WordMemory target, UInt16 source)
		{
		}

		/// <summary>
		/// AND mem16,imm16
		/// </summary>
		public unsafe static void AND (UInt16* target, UInt16 source)
		{
		}

		/// <summary>
		/// AND mem32,imm32
		/// </summary>
		public static void AND (DWordMemory target, UInt32 source)
		{
		}

		/// <summary>
		/// AND mem32,imm32
		/// </summary>
		public unsafe static void AND (UInt32* target, UInt32 source)
		{
		}

		/// <summary>
		/// AND mem16,imm8
		/// </summary>
		public static void AND (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// AND mem16,imm8
		/// </summary>
		public unsafe static void AND (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// AND mem32,imm8
		/// </summary>
		public static void AND (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// AND mem32,imm8
		/// </summary>
		public unsafe static void AND (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// AND rmreg8,reg8
		/// </summary>
		public static void AND (R8Type target, R8Type source)
		{
		}

		/// <summary>
		/// AND rmreg16,reg16
		/// </summary>
		public static void AND (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// AND rmreg32,reg32
		/// </summary>
		public static void AND (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// AND rmreg8,imm8
		/// </summary>
		public static void AND (R8Type target, Byte source)
		{
		}

		/// <summary>
		/// AND rmreg16,imm16
		/// </summary>
		public static void AND (R16Type target, UInt16 source)
		{
		}

		/// <summary>
		/// AND rmreg32,imm32
		/// </summary>
		public static void AND (R32Type target, UInt32 source)
		{
		}

		/// <summary>
		/// AND rmreg16,imm8
		/// </summary>
		public static void AND (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// AND rmreg32,imm8
		/// </summary>
		public static void AND (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// ARPL mem16,reg16
		/// </summary>
		public static void ARPL (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// ARPL mem16,reg16
		/// </summary>
		public unsafe static void ARPL (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// ARPL rmreg16,reg16
		/// </summary>
		public static void ARPL (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// BITS32 
		/// </summary>
		public static void BITS32 (bool value)
		{
		}

		/// <summary>
		/// BOUND reg16,mem
		/// </summary>
		public static void BOUND (R16Type target, Memory source)
		{
		}

		/// <summary>
		/// BOUND reg16,mem
		/// </summary>
		public unsafe static void BOUND (R16Type target, byte* source)
		{
		}

		/// <summary>
		/// BOUND reg32,mem
		/// </summary>
		public static void BOUND (R32Type target, Memory source)
		{
		}

		/// <summary>
		/// BOUND reg32,mem
		/// </summary>
		public unsafe static void BOUND (R32Type target, byte* source)
		{
		}

		/// <summary>
		/// BSF reg16,mem16
		/// </summary>
		public static void BSF (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// BSF reg16,mem16
		/// </summary>
		public unsafe static void BSF (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// BSF reg32,mem32
		/// </summary>
		public static void BSF (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// BSF reg32,mem32
		/// </summary>
		public unsafe static void BSF (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// BSF reg16,rmreg16
		/// </summary>
		public static void BSF (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// BSF reg32,rmreg32
		/// </summary>
		public static void BSF (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// BSR reg16,mem16
		/// </summary>
		public static void BSR (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// BSR reg16,mem16
		/// </summary>
		public unsafe static void BSR (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// BSR reg32,mem32
		/// </summary>
		public static void BSR (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// BSR reg32,mem32
		/// </summary>
		public unsafe static void BSR (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// BSR reg16,rmreg16
		/// </summary>
		public static void BSR (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// BSR reg32,rmreg32
		/// </summary>
		public static void BSR (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// BSWAP reg32
		/// </summary>
		public static void BSWAP (R32Type target)
		{
		}

		/// <summary>
		/// BT mem16,reg16
		/// </summary>
		public static void BT (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// BT mem16,reg16
		/// </summary>
		public unsafe static void BT (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// BT mem32,reg32
		/// </summary>
		public static void BT (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// BT mem32,reg32
		/// </summary>
		public unsafe static void BT (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// BT mem16,imm8
		/// </summary>
		public static void BT (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// BT mem16,imm8
		/// </summary>
		public unsafe static void BT (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// BT mem32,imm8
		/// </summary>
		public static void BT (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// BT mem32,imm8
		/// </summary>
		public unsafe static void BT (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// BT rmreg16,reg16
		/// </summary>
		public static void BT (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// BT rmreg32,reg32
		/// </summary>
		public static void BT (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// BT rmreg16,imm8
		/// </summary>
		public static void BT (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// BT rmreg32,imm8
		/// </summary>
		public static void BT (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// BTC mem16,reg16
		/// </summary>
		public static void BTC (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// BTC mem16,reg16
		/// </summary>
		public unsafe static void BTC (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// BTC mem32,reg32
		/// </summary>
		public static void BTC (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// BTC mem32,reg32
		/// </summary>
		public unsafe static void BTC (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// BTC mem16,imm8
		/// </summary>
		public static void BTC (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// BTC mem16,imm8
		/// </summary>
		public unsafe static void BTC (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// BTC mem32,imm8
		/// </summary>
		public static void BTC (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// BTC mem32,imm8
		/// </summary>
		public unsafe static void BTC (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// BTC rmreg16,reg16
		/// </summary>
		public static void BTC (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// BTC rmreg32,reg32
		/// </summary>
		public static void BTC (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// BTC rmreg16,imm8
		/// </summary>
		public static void BTC (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// BTC rmreg32,imm8
		/// </summary>
		public static void BTC (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// BTR mem16,reg16
		/// </summary>
		public static void BTR (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// BTR mem16,reg16
		/// </summary>
		public unsafe static void BTR (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// BTR mem32,reg32
		/// </summary>
		public static void BTR (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// BTR mem32,reg32
		/// </summary>
		public unsafe static void BTR (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// BTR mem16,imm8
		/// </summary>
		public static void BTR (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// BTR mem16,imm8
		/// </summary>
		public unsafe static void BTR (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// BTR mem32,imm8
		/// </summary>
		public static void BTR (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// BTR mem32,imm8
		/// </summary>
		public unsafe static void BTR (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// BTR rmreg16,reg16
		/// </summary>
		public static void BTR (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// BTR rmreg32,reg32
		/// </summary>
		public static void BTR (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// BTR rmreg16,imm8
		/// </summary>
		public static void BTR (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// BTR rmreg32,imm8
		/// </summary>
		public static void BTR (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// BTS mem16,reg16
		/// </summary>
		public static void BTS (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// BTS mem16,reg16
		/// </summary>
		public unsafe static void BTS (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// BTS mem32,reg32
		/// </summary>
		public static void BTS (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// BTS mem32,reg32
		/// </summary>
		public unsafe static void BTS (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// BTS mem16,imm8
		/// </summary>
		public static void BTS (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// BTS mem16,imm8
		/// </summary>
		public unsafe static void BTS (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// BTS mem32,imm8
		/// </summary>
		public static void BTS (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// BTS mem32,imm8
		/// </summary>
		public unsafe static void BTS (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// BTS rmreg16,reg16
		/// </summary>
		public static void BTS (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// BTS rmreg32,reg32
		/// </summary>
		public static void BTS (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// BTS rmreg16,imm8
		/// </summary>
		public static void BTS (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// BTS rmreg32,imm8
		/// </summary>
		public static void BTS (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// CALL imm
		/// </summary>
		public static void CALL (UInt32 target)
		{
		}

		/// <summary>
		/// CALL imm
		/// </summary>
		public static void CALL (string label)
		{
		}

		/// <summary>
		/// CALL imm16:imm16
		/// </summary>
		public static void CALL (UInt16 target, UInt16 source)
		{
		}

		/// <summary>
		/// CALL imm16:imm32
		/// </summary>
		public static void CALL (UInt16 target, UInt32 source)
		{
		}

		/// <summary>
		/// CALL FAR mem16
		/// </summary>
		public static void CALL_FAR (WordMemory target)
		{
		}

		/// <summary>
		/// CALL FAR mem16
		/// </summary>
		public unsafe static void CALL_FAR (UInt16* target)
		{
		}

		/// <summary>
		/// CALL FAR mem16
		/// </summary>
		public static void CALL_FAR (string label)
		{
		}

		/// <summary>
		/// CALL FAR mem32
		/// </summary>
		public static void CALL_FAR (DWordMemory target)
		{
		}

		/// <summary>
		/// CALL FAR mem32
		/// </summary>
		public unsafe static void CALL_FAR (UInt32* target)
		{
		}

		/// <summary>
		/// CALL mem16
		/// </summary>
		public static void CALL (WordMemory target)
		{
		}

		/// <summary>
		/// CALL mem16
		/// </summary>
		public unsafe static void CALL (UInt16* target)
		{
		}

		/// <summary>
		/// CALL mem32
		/// </summary>
		public static void CALL (DWordMemory target)
		{
		}

		/// <summary>
		/// CALL mem32
		/// </summary>
		public unsafe static void CALL (UInt32* target)
		{
		}

		/// <summary>
		/// CALL rmreg16
		/// </summary>
		public static void CALL (R16Type target)
		{
		}

		/// <summary>
		/// CALL rmreg32
		/// </summary>
		public static void CALL (R32Type target)
		{
		}

		/// <summary>
		/// CBW 
		/// </summary>
		public static void CBW ()
		{
		}

		/// <summary>
		/// CDQ 
		/// </summary>
		public static void CDQ ()
		{
		}

		/// <summary>
		/// CLC 
		/// </summary>
		public static void CLC ()
		{
		}

		/// <summary>
		/// CLD 
		/// </summary>
		public static void CLD ()
		{
		}

		/// <summary>
		/// CLFLUSH mem
		/// </summary>
		public static void CLFLUSH (Memory target)
		{
		}

		/// <summary>
		/// CLFLUSH mem
		/// </summary>
		public unsafe static void CLFLUSH (byte* target)
		{
		}

		/// <summary>
		/// CLI 
		/// </summary>
		public static void CLI ()
		{
		}

		/// <summary>
		/// CLTS 
		/// </summary>
		public static void CLTS ()
		{
		}

		/// <summary>
		/// CMC 
		/// </summary>
		public static void CMC ()
		{
		}

		/// <summary>
		/// CMOVA reg16,mem16
		/// </summary>
		public static void CMOVA (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVA reg16,mem16
		/// </summary>
		public unsafe static void CMOVA (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVA reg32,mem32
		/// </summary>
		public static void CMOVA (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVA reg32,mem32
		/// </summary>
		public unsafe static void CMOVA (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVA reg16,rmreg16
		/// </summary>
		public static void CMOVA (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVA reg32,rmreg32
		/// </summary>
		public static void CMOVA (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVAE reg16,mem16
		/// </summary>
		public static void CMOVAE (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVAE reg16,mem16
		/// </summary>
		public unsafe static void CMOVAE (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVAE reg32,mem32
		/// </summary>
		public static void CMOVAE (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVAE reg32,mem32
		/// </summary>
		public unsafe static void CMOVAE (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVAE reg16,rmreg16
		/// </summary>
		public static void CMOVAE (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVAE reg32,rmreg32
		/// </summary>
		public static void CMOVAE (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVB reg16,mem16
		/// </summary>
		public static void CMOVB (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVB reg16,mem16
		/// </summary>
		public unsafe static void CMOVB (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVB reg32,mem32
		/// </summary>
		public static void CMOVB (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVB reg32,mem32
		/// </summary>
		public unsafe static void CMOVB (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVB reg16,rmreg16
		/// </summary>
		public static void CMOVB (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVB reg32,rmreg32
		/// </summary>
		public static void CMOVB (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVBE reg16,mem16
		/// </summary>
		public static void CMOVBE (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVBE reg16,mem16
		/// </summary>
		public unsafe static void CMOVBE (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVBE reg32,mem32
		/// </summary>
		public static void CMOVBE (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVBE reg32,mem32
		/// </summary>
		public unsafe static void CMOVBE (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVBE reg16,rmreg16
		/// </summary>
		public static void CMOVBE (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVBE reg32,rmreg32
		/// </summary>
		public static void CMOVBE (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVC reg16,mem16
		/// </summary>
		public static void CMOVC (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVC reg16,mem16
		/// </summary>
		public unsafe static void CMOVC (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVC reg32,mem32
		/// </summary>
		public static void CMOVC (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVC reg32,mem32
		/// </summary>
		public unsafe static void CMOVC (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVC reg16,rmreg16
		/// </summary>
		public static void CMOVC (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVC reg32,rmreg32
		/// </summary>
		public static void CMOVC (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVE reg16,mem16
		/// </summary>
		public static void CMOVE (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVE reg16,mem16
		/// </summary>
		public unsafe static void CMOVE (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVE reg32,mem32
		/// </summary>
		public static void CMOVE (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVE reg32,mem32
		/// </summary>
		public unsafe static void CMOVE (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVE reg16,rmreg16
		/// </summary>
		public static void CMOVE (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVE reg32,rmreg32
		/// </summary>
		public static void CMOVE (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVG reg16,mem16
		/// </summary>
		public static void CMOVG (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVG reg16,mem16
		/// </summary>
		public unsafe static void CMOVG (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVG reg32,mem32
		/// </summary>
		public static void CMOVG (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVG reg32,mem32
		/// </summary>
		public unsafe static void CMOVG (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVG reg16,rmreg16
		/// </summary>
		public static void CMOVG (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVG reg32,rmreg32
		/// </summary>
		public static void CMOVG (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVGE reg16,mem16
		/// </summary>
		public static void CMOVGE (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVGE reg16,mem16
		/// </summary>
		public unsafe static void CMOVGE (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVGE reg32,mem32
		/// </summary>
		public static void CMOVGE (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVGE reg32,mem32
		/// </summary>
		public unsafe static void CMOVGE (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVGE reg16,rmreg16
		/// </summary>
		public static void CMOVGE (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVGE reg32,rmreg32
		/// </summary>
		public static void CMOVGE (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVL reg16,mem16
		/// </summary>
		public static void CMOVL (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVL reg16,mem16
		/// </summary>
		public unsafe static void CMOVL (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVL reg32,mem32
		/// </summary>
		public static void CMOVL (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVL reg32,mem32
		/// </summary>
		public unsafe static void CMOVL (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVL reg16,rmreg16
		/// </summary>
		public static void CMOVL (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVL reg32,rmreg32
		/// </summary>
		public static void CMOVL (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVLE reg16,mem16
		/// </summary>
		public static void CMOVLE (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVLE reg16,mem16
		/// </summary>
		public unsafe static void CMOVLE (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVLE reg32,mem32
		/// </summary>
		public static void CMOVLE (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVLE reg32,mem32
		/// </summary>
		public unsafe static void CMOVLE (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVLE reg16,rmreg16
		/// </summary>
		public static void CMOVLE (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVLE reg32,rmreg32
		/// </summary>
		public static void CMOVLE (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVNA reg16,mem16
		/// </summary>
		public static void CMOVNA (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVNA reg16,mem16
		/// </summary>
		public unsafe static void CMOVNA (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVNA reg32,mem32
		/// </summary>
		public static void CMOVNA (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVNA reg32,mem32
		/// </summary>
		public unsafe static void CMOVNA (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVNA reg16,rmreg16
		/// </summary>
		public static void CMOVNA (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVNA reg32,rmreg32
		/// </summary>
		public static void CMOVNA (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVNAE reg16,mem16
		/// </summary>
		public static void CMOVNAE (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVNAE reg16,mem16
		/// </summary>
		public unsafe static void CMOVNAE (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVNAE reg32,mem32
		/// </summary>
		public static void CMOVNAE (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVNAE reg32,mem32
		/// </summary>
		public unsafe static void CMOVNAE (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVNAE reg16,rmreg16
		/// </summary>
		public static void CMOVNAE (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVNAE reg32,rmreg32
		/// </summary>
		public static void CMOVNAE (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVNB reg16,mem16
		/// </summary>
		public static void CMOVNB (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVNB reg16,mem16
		/// </summary>
		public unsafe static void CMOVNB (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVNB reg32,mem32
		/// </summary>
		public static void CMOVNB (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVNB reg32,mem32
		/// </summary>
		public unsafe static void CMOVNB (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVNB reg16,rmreg16
		/// </summary>
		public static void CMOVNB (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVNB reg32,rmreg32
		/// </summary>
		public static void CMOVNB (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVNBE reg16,mem16
		/// </summary>
		public static void CMOVNBE (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVNBE reg16,mem16
		/// </summary>
		public unsafe static void CMOVNBE (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVNBE reg32,mem32
		/// </summary>
		public static void CMOVNBE (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVNBE reg32,mem32
		/// </summary>
		public unsafe static void CMOVNBE (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVNBE reg16,rmreg16
		/// </summary>
		public static void CMOVNBE (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVNBE reg32,rmreg32
		/// </summary>
		public static void CMOVNBE (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVNC reg16,mem16
		/// </summary>
		public static void CMOVNC (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVNC reg16,mem16
		/// </summary>
		public unsafe static void CMOVNC (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVNC reg32,mem32
		/// </summary>
		public static void CMOVNC (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVNC reg32,mem32
		/// </summary>
		public unsafe static void CMOVNC (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVNC reg16,rmreg16
		/// </summary>
		public static void CMOVNC (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVNC reg32,rmreg32
		/// </summary>
		public static void CMOVNC (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVNE reg16,mem16
		/// </summary>
		public static void CMOVNE (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVNE reg16,mem16
		/// </summary>
		public unsafe static void CMOVNE (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVNE reg32,mem32
		/// </summary>
		public static void CMOVNE (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVNE reg32,mem32
		/// </summary>
		public unsafe static void CMOVNE (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVNE reg16,rmreg16
		/// </summary>
		public static void CMOVNE (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVNE reg32,rmreg32
		/// </summary>
		public static void CMOVNE (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVNG reg16,mem16
		/// </summary>
		public static void CMOVNG (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVNG reg16,mem16
		/// </summary>
		public unsafe static void CMOVNG (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVNG reg32,mem32
		/// </summary>
		public static void CMOVNG (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVNG reg32,mem32
		/// </summary>
		public unsafe static void CMOVNG (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVNG reg16,rmreg16
		/// </summary>
		public static void CMOVNG (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVNG reg32,rmreg32
		/// </summary>
		public static void CMOVNG (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVNGE reg16,mem16
		/// </summary>
		public static void CMOVNGE (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVNGE reg16,mem16
		/// </summary>
		public unsafe static void CMOVNGE (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVNGE reg32,mem32
		/// </summary>
		public static void CMOVNGE (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVNGE reg32,mem32
		/// </summary>
		public unsafe static void CMOVNGE (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVNGE reg16,rmreg16
		/// </summary>
		public static void CMOVNGE (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVNGE reg32,rmreg32
		/// </summary>
		public static void CMOVNGE (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVNL reg16,mem16
		/// </summary>
		public static void CMOVNL (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVNL reg16,mem16
		/// </summary>
		public unsafe static void CMOVNL (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVNL reg32,mem32
		/// </summary>
		public static void CMOVNL (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVNL reg32,mem32
		/// </summary>
		public unsafe static void CMOVNL (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVNL reg16,rmreg16
		/// </summary>
		public static void CMOVNL (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVNL reg32,rmreg32
		/// </summary>
		public static void CMOVNL (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVNLE reg16,mem16
		/// </summary>
		public static void CMOVNLE (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVNLE reg16,mem16
		/// </summary>
		public unsafe static void CMOVNLE (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVNLE reg32,mem32
		/// </summary>
		public static void CMOVNLE (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVNLE reg32,mem32
		/// </summary>
		public unsafe static void CMOVNLE (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVNLE reg16,rmreg16
		/// </summary>
		public static void CMOVNLE (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVNLE reg32,rmreg32
		/// </summary>
		public static void CMOVNLE (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVNO reg16,mem16
		/// </summary>
		public static void CMOVNO (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVNO reg16,mem16
		/// </summary>
		public unsafe static void CMOVNO (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVNO reg32,mem32
		/// </summary>
		public static void CMOVNO (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVNO reg32,mem32
		/// </summary>
		public unsafe static void CMOVNO (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVNO reg16,rmreg16
		/// </summary>
		public static void CMOVNO (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVNO reg32,rmreg32
		/// </summary>
		public static void CMOVNO (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVNP reg16,mem16
		/// </summary>
		public static void CMOVNP (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVNP reg16,mem16
		/// </summary>
		public unsafe static void CMOVNP (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVNP reg32,mem32
		/// </summary>
		public static void CMOVNP (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVNP reg32,mem32
		/// </summary>
		public unsafe static void CMOVNP (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVNP reg16,rmreg16
		/// </summary>
		public static void CMOVNP (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVNP reg32,rmreg32
		/// </summary>
		public static void CMOVNP (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVNS reg16,mem16
		/// </summary>
		public static void CMOVNS (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVNS reg16,mem16
		/// </summary>
		public unsafe static void CMOVNS (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVNS reg32,mem32
		/// </summary>
		public static void CMOVNS (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVNS reg32,mem32
		/// </summary>
		public unsafe static void CMOVNS (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVNS reg16,rmreg16
		/// </summary>
		public static void CMOVNS (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVNS reg32,rmreg32
		/// </summary>
		public static void CMOVNS (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVNZ reg16,mem16
		/// </summary>
		public static void CMOVNZ (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVNZ reg16,mem16
		/// </summary>
		public unsafe static void CMOVNZ (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVNZ reg32,mem32
		/// </summary>
		public static void CMOVNZ (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVNZ reg32,mem32
		/// </summary>
		public unsafe static void CMOVNZ (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVNZ reg16,rmreg16
		/// </summary>
		public static void CMOVNZ (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVNZ reg32,rmreg32
		/// </summary>
		public static void CMOVNZ (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVO reg16,mem16
		/// </summary>
		public static void CMOVO (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVO reg16,mem16
		/// </summary>
		public unsafe static void CMOVO (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVO reg32,mem32
		/// </summary>
		public static void CMOVO (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVO reg32,mem32
		/// </summary>
		public unsafe static void CMOVO (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVO reg16,rmreg16
		/// </summary>
		public static void CMOVO (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVO reg32,rmreg32
		/// </summary>
		public static void CMOVO (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVP reg16,mem16
		/// </summary>
		public static void CMOVP (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVP reg16,mem16
		/// </summary>
		public unsafe static void CMOVP (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVP reg32,mem32
		/// </summary>
		public static void CMOVP (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVP reg32,mem32
		/// </summary>
		public unsafe static void CMOVP (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVP reg16,rmreg16
		/// </summary>
		public static void CMOVP (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVP reg32,rmreg32
		/// </summary>
		public static void CMOVP (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVPE reg16,mem16
		/// </summary>
		public static void CMOVPE (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVPE reg16,mem16
		/// </summary>
		public unsafe static void CMOVPE (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVPE reg32,mem32
		/// </summary>
		public static void CMOVPE (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVPE reg32,mem32
		/// </summary>
		public unsafe static void CMOVPE (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVPE reg16,rmreg16
		/// </summary>
		public static void CMOVPE (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVPE reg32,rmreg32
		/// </summary>
		public static void CMOVPE (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVPO reg16,mem16
		/// </summary>
		public static void CMOVPO (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVPO reg16,mem16
		/// </summary>
		public unsafe static void CMOVPO (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVPO reg32,mem32
		/// </summary>
		public static void CMOVPO (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVPO reg32,mem32
		/// </summary>
		public unsafe static void CMOVPO (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVPO reg16,rmreg16
		/// </summary>
		public static void CMOVPO (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVPO reg32,rmreg32
		/// </summary>
		public static void CMOVPO (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVS reg16,mem16
		/// </summary>
		public static void CMOVS (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVS reg16,mem16
		/// </summary>
		public unsafe static void CMOVS (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVS reg32,mem32
		/// </summary>
		public static void CMOVS (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVS reg32,mem32
		/// </summary>
		public unsafe static void CMOVS (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVS reg16,rmreg16
		/// </summary>
		public static void CMOVS (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVS reg32,rmreg32
		/// </summary>
		public static void CMOVS (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMOVZ reg16,mem16
		/// </summary>
		public static void CMOVZ (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMOVZ reg16,mem16
		/// </summary>
		public unsafe static void CMOVZ (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMOVZ reg32,mem32
		/// </summary>
		public static void CMOVZ (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMOVZ reg32,mem32
		/// </summary>
		public unsafe static void CMOVZ (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMOVZ reg16,rmreg16
		/// </summary>
		public static void CMOVZ (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMOVZ reg32,rmreg32
		/// </summary>
		public static void CMOVZ (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMP mem8,reg8
		/// </summary>
		public static void CMP (ByteMemory target, R8Type source)
		{
		}

		/// <summary>
		/// CMP mem8,reg8
		/// </summary>
		public unsafe static void CMP (byte* target, R8Type source)
		{
		}

		/// <summary>
		/// CMP mem16,reg16
		/// </summary>
		public static void CMP (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// CMP mem16,reg16
		/// </summary>
		public unsafe static void CMP (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// CMP mem32,reg32
		/// </summary>
		public static void CMP (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// CMP mem32,reg32
		/// </summary>
		public unsafe static void CMP (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// CMP reg8,mem8
		/// </summary>
		public static void CMP (R8Type target, ByteMemory source)
		{
		}

		/// <summary>
		/// CMP reg8,mem8
		/// </summary>
		public unsafe static void CMP (R8Type target, byte* source)
		{
		}

		/// <summary>
		/// CMP reg16,mem16
		/// </summary>
		public static void CMP (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// CMP reg16,mem16
		/// </summary>
		public unsafe static void CMP (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// CMP reg32,mem32
		/// </summary>
		public static void CMP (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// CMP reg32,mem32
		/// </summary>
		public unsafe static void CMP (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// CMP mem8,imm8
		/// </summary>
		public static void CMP (ByteMemory target, Byte source)
		{
		}

		/// <summary>
		/// CMP mem8,imm8
		/// </summary>
		public unsafe static void CMP (byte* target, Byte source)
		{
		}

		/// <summary>
		/// CMP mem16,imm16
		/// </summary>
		public static void CMP (WordMemory target, UInt16 source)
		{
		}

		/// <summary>
		/// CMP mem16,imm16
		/// </summary>
		public unsafe static void CMP (UInt16* target, UInt16 source)
		{
		}

		/// <summary>
		/// CMP mem32,imm32
		/// </summary>
		public static void CMP (DWordMemory target, UInt32 source)
		{
		}

		/// <summary>
		/// CMP mem32,imm32
		/// </summary>
		public unsafe static void CMP (UInt32* target, UInt32 source)
		{
		}

		/// <summary>
		/// CMP mem16,imm8
		/// </summary>
		public static void CMP (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// CMP mem16,imm8
		/// </summary>
		public unsafe static void CMP (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// CMP mem32,imm8
		/// </summary>
		public static void CMP (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// CMP mem32,imm8
		/// </summary>
		public unsafe static void CMP (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// CMP rmreg8,reg8
		/// </summary>
		public static void CMP (R8Type target, R8Type source)
		{
		}

		/// <summary>
		/// CMP rmreg16,reg16
		/// </summary>
		public static void CMP (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMP rmreg32,reg32
		/// </summary>
		public static void CMP (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMP rmreg8,imm8
		/// </summary>
		public static void CMP (R8Type target, Byte source)
		{
		}

		/// <summary>
		/// CMP rmreg16,imm16
		/// </summary>
		public static void CMP (R16Type target, UInt16 source)
		{
		}

		/// <summary>
		/// CMP rmreg32,imm32
		/// </summary>
		public static void CMP (R32Type target, UInt32 source)
		{
		}

		/// <summary>
		/// CMP rmreg16,imm8
		/// </summary>
		public static void CMP (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// CMP rmreg32,imm8
		/// </summary>
		public static void CMP (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// CMPSB 
		/// </summary>
		public static void CMPSB ()
		{
		}

		/// <summary>
		/// CMPSD 
		/// </summary>
		public static void CMPSD ()
		{
		}

		/// <summary>
		/// CMPSW 
		/// </summary>
		public static void CMPSW ()
		{
		}

		/// <summary>
		/// CMPXCHG mem8,reg8
		/// </summary>
		public static void CMPXCHG (ByteMemory target, R8Type source)
		{
		}

		/// <summary>
		/// CMPXCHG mem8,reg8
		/// </summary>
		public unsafe static void CMPXCHG (byte* target, R8Type source)
		{
		}

		/// <summary>
		/// CMPXCHG mem16,reg16
		/// </summary>
		public static void CMPXCHG (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// CMPXCHG mem16,reg16
		/// </summary>
		public unsafe static void CMPXCHG (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// CMPXCHG mem32,reg32
		/// </summary>
		public static void CMPXCHG (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// CMPXCHG mem32,reg32
		/// </summary>
		public unsafe static void CMPXCHG (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// CMPXCHG rmreg8,reg8
		/// </summary>
		public static void CMPXCHG (R8Type target, R8Type source)
		{
		}

		/// <summary>
		/// CMPXCHG rmreg16,reg16
		/// </summary>
		public static void CMPXCHG (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// CMPXCHG rmreg32,reg32
		/// </summary>
		public static void CMPXCHG (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// CMPXCHG8B mem
		/// </summary>
		public static void CMPXCHG8B (Memory target)
		{
		}

		/// <summary>
		/// CMPXCHG8B mem
		/// </summary>
		public unsafe static void CMPXCHG8B (byte* target)
		{
		}

		/// <summary>
		/// CPUID 
		/// </summary>
		public static void CPUID ()
		{
		}

		/// <summary>
		/// CWD 
		/// </summary>
		public static void CWD ()
		{
		}

		/// <summary>
		/// CWDE 
		/// </summary>
		public static void CWDE ()
		{
		}

		/// <summary>
		/// DAA 
		/// </summary>
		public static void DAA ()
		{
		}

		/// <summary>
		/// DAS 
		/// </summary>
		public static void DAS ()
		{
		}

		/// <summary>
		/// DATA 
		/// </summary>
		public static void DATA (string name, string values)
		{
		}

		/// <summary>
		/// DATA 
		/// </summary>
		public static void DATA (string name, byte value)
		{
		}

		/// <summary>
		/// DATA 
		/// </summary>
		public static void DATA (string name, UInt16 value)
		{
		}

		/// <summary>
		/// DATA 
		/// </summary>
		public static void DATA (string name, UInt32 value)
		{
		}

		/// <summary>
		/// DATA 
		/// </summary>
		public static void DATA (string values)
		{
		}

		/// <summary>
		/// DATA 
		/// </summary>
		public static void DATA (byte value)
		{
		}

		/// <summary>
		/// DATA 
		/// </summary>
		public static void DATA (UInt16 value)
		{
		}

		/// <summary>
		/// DATA 
		/// </summary>
		public static void DATA (UInt32 value)
		{
		}

		/// <summary>
		/// DEC reg16
		/// </summary>
		public static void DEC (R16Type target)
		{
		}

		/// <summary>
		/// DEC reg32
		/// </summary>
		public static void DEC (R32Type target)
		{
		}

		/// <summary>
		/// DEC mem8
		/// </summary>
		public static void DEC (ByteMemory target)
		{
		}

		/// <summary>
		/// DEC mem8
		/// </summary>
		public unsafe static void DEC (byte* target)
		{
		}

		/// <summary>
		/// DEC mem16
		/// </summary>
		public static void DEC (WordMemory target)
		{
		}

		/// <summary>
		/// DEC mem16
		/// </summary>
		public unsafe static void DEC (UInt16* target)
		{
		}

		/// <summary>
		/// DEC mem32
		/// </summary>
		public static void DEC (DWordMemory target)
		{
		}

		/// <summary>
		/// DEC mem32
		/// </summary>
		public unsafe static void DEC (UInt32* target)
		{
		}

		/// <summary>
		/// DEC rmreg8
		/// </summary>
		public static void DEC (R8Type target)
		{
		}

		/// <summary>
		/// DIV mem8
		/// </summary>
		public static void DIV (ByteMemory target)
		{
		}

		/// <summary>
		/// DIV mem8
		/// </summary>
		public unsafe static void DIV (byte* target)
		{
		}

		/// <summary>
		/// DIV mem16
		/// </summary>
		public static void DIV (WordMemory target)
		{
		}

		/// <summary>
		/// DIV mem16
		/// </summary>
		public unsafe static void DIV (UInt16* target)
		{
		}

		/// <summary>
		/// DIV mem32
		/// </summary>
		public static void DIV (DWordMemory target)
		{
		}

		/// <summary>
		/// DIV mem32
		/// </summary>
		public unsafe static void DIV (UInt32* target)
		{
		}

		/// <summary>
		/// DIV rmreg8
		/// </summary>
		public static void DIV (R8Type target)
		{
		}

		/// <summary>
		/// DIV rmreg16
		/// </summary>
		public static void DIV (R16Type target)
		{
		}

		/// <summary>
		/// DIV rmreg32
		/// </summary>
		public static void DIV (R32Type target)
		{
		}

		/// <summary>
		/// EMMS 
		/// </summary>
		public static void EMMS ()
		{
		}

		/// <summary>
		/// ENTER imm16,imm8
		/// </summary>
		public static void ENTER (UInt16 target, Byte source)
		{
		}

		/// <summary>
		/// F2XM1 
		/// </summary>
		public static void F2XM1 ()
		{
		}

		/// <summary>
		/// FABS 
		/// </summary>
		public static void FABS ()
		{
		}

		/// <summary>
		/// FADD mem32
		/// </summary>
		public static void FADD (DWordMemory target)
		{
		}

		/// <summary>
		/// FADD mem32
		/// </summary>
		public unsafe static void FADD (UInt32* target)
		{
		}

		/// <summary>
		/// FADD mem64
		/// </summary>
		public static void FADD (QWordMemory target)
		{
		}

		/// <summary>
		/// FADD fpureg
		/// </summary>
		public static void FADD (FPType target)
		{
		}

		/// <summary>
		/// FADD ST0,fpureg
		/// </summary>
		public static void FADD_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FADD fpureg,ST0
		/// </summary>
		public static void FADD__ST0 (FPType target)
		{
		}

		/// <summary>
		/// FADDP fpureg
		/// </summary>
		public static void FADDP (FPType target)
		{
		}

		/// <summary>
		/// FADDP fpureg,ST0
		/// </summary>
		public static void FADDP__ST0 (FPType target)
		{
		}

		/// <summary>
		/// FBLD mem80
		/// </summary>
		public static void FBLD (TWordMemory target)
		{
		}

		/// <summary>
		/// FBSTP mem80
		/// </summary>
		public static void FBSTP (TWordMemory target)
		{
		}

		/// <summary>
		/// FCHS 
		/// </summary>
		public static void FCHS ()
		{
		}

		/// <summary>
		/// FCLEX 
		/// </summary>
		public static void FCLEX ()
		{
		}

		/// <summary>
		/// FCMOVB fpureg
		/// </summary>
		public static void FCMOVB (FPType target)
		{
		}

		/// <summary>
		/// FCMOVB ST0,fpureg
		/// </summary>
		public static void FCMOVB_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FCMOVBE fpureg
		/// </summary>
		public static void FCMOVBE (FPType target)
		{
		}

		/// <summary>
		/// FCMOVBE ST0,fpureg
		/// </summary>
		public static void FCMOVBE_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FCMOVE fpureg
		/// </summary>
		public static void FCMOVE (FPType target)
		{
		}

		/// <summary>
		/// FCMOVE ST0,fpureg
		/// </summary>
		public static void FCMOVE_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FCMOVNB fpureg
		/// </summary>
		public static void FCMOVNB (FPType target)
		{
		}

		/// <summary>
		/// FCMOVNB ST0,fpureg
		/// </summary>
		public static void FCMOVNB_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FCMOVNBE fpureg
		/// </summary>
		public static void FCMOVNBE (FPType target)
		{
		}

		/// <summary>
		/// FCMOVNBE ST0,fpureg
		/// </summary>
		public static void FCMOVNBE_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FCMOVNE fpureg
		/// </summary>
		public static void FCMOVNE (FPType target)
		{
		}

		/// <summary>
		/// FCMOVNE ST0,fpureg
		/// </summary>
		public static void FCMOVNE_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FCMOVNU fpureg
		/// </summary>
		public static void FCMOVNU (FPType target)
		{
		}

		/// <summary>
		/// FCMOVNU ST0,fpureg
		/// </summary>
		public static void FCMOVNU_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FCMOVU fpureg
		/// </summary>
		public static void FCMOVU (FPType target)
		{
		}

		/// <summary>
		/// FCMOVU ST0,fpureg
		/// </summary>
		public static void FCMOVU_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FCOM mem32
		/// </summary>
		public static void FCOM (DWordMemory target)
		{
		}

		/// <summary>
		/// FCOM mem32
		/// </summary>
		public unsafe static void FCOM (UInt32* target)
		{
		}

		/// <summary>
		/// FCOM mem64
		/// </summary>
		public static void FCOM (QWordMemory target)
		{
		}

		/// <summary>
		/// FCOM fpureg
		/// </summary>
		public static void FCOM (FPType target)
		{
		}

		/// <summary>
		/// FCOM ST0,fpureg
		/// </summary>
		public static void FCOM_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FCOMI fpureg
		/// </summary>
		public static void FCOMI (FPType target)
		{
		}

		/// <summary>
		/// FCOMI ST0,fpureg
		/// </summary>
		public static void FCOMI_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FCOMIP fpureg
		/// </summary>
		public static void FCOMIP (FPType target)
		{
		}

		/// <summary>
		/// FCOMIP ST0,fpureg
		/// </summary>
		public static void FCOMIP_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FCOMP mem32
		/// </summary>
		public static void FCOMP (DWordMemory target)
		{
		}

		/// <summary>
		/// FCOMP mem32
		/// </summary>
		public unsafe static void FCOMP (UInt32* target)
		{
		}

		/// <summary>
		/// FCOMP mem64
		/// </summary>
		public static void FCOMP (QWordMemory target)
		{
		}

		/// <summary>
		/// FCOMP fpureg
		/// </summary>
		public static void FCOMP (FPType target)
		{
		}

		/// <summary>
		/// FCOMP ST0,fpureg
		/// </summary>
		public static void FCOMP_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FCOMPP 
		/// </summary>
		public static void FCOMPP ()
		{
		}

		/// <summary>
		/// FCOS 
		/// </summary>
		public static void FCOS ()
		{
		}

		/// <summary>
		/// FDECSTP 
		/// </summary>
		public static void FDECSTP ()
		{
		}

		/// <summary>
		/// FDISI 
		/// </summary>
		public static void FDISI ()
		{
		}

		/// <summary>
		/// FDIV mem32
		/// </summary>
		public static void FDIV (DWordMemory target)
		{
		}

		/// <summary>
		/// FDIV mem32
		/// </summary>
		public unsafe static void FDIV (UInt32* target)
		{
		}

		/// <summary>
		/// FDIV mem64
		/// </summary>
		public static void FDIV (QWordMemory target)
		{
		}

		/// <summary>
		/// FDIV fpureg
		/// </summary>
		public static void FDIV (FPType target)
		{
		}

		/// <summary>
		/// FDIV ST0,fpureg
		/// </summary>
		public static void FDIV_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FDIV fpureg,ST0
		/// </summary>
		public static void FDIV__ST0 (FPType target)
		{
		}

		/// <summary>
		/// FDIVP fpureg
		/// </summary>
		public static void FDIVP (FPType target)
		{
		}

		/// <summary>
		/// FDIVP fpureg,ST0
		/// </summary>
		public static void FDIVP__ST0 (FPType target)
		{
		}

		/// <summary>
		/// FDIVR mem32
		/// </summary>
		public static void FDIVR (DWordMemory target)
		{
		}

		/// <summary>
		/// FDIVR mem32
		/// </summary>
		public unsafe static void FDIVR (UInt32* target)
		{
		}

		/// <summary>
		/// FDIVR mem64
		/// </summary>
		public static void FDIVR (QWordMemory target)
		{
		}

		/// <summary>
		/// FDIVR fpureg
		/// </summary>
		public static void FDIVR (FPType target)
		{
		}

		/// <summary>
		/// FDIVR ST0,fpureg
		/// </summary>
		public static void FDIVR_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FDIVR fpureg,ST0
		/// </summary>
		public static void FDIVR__ST0 (FPType target)
		{
		}

		/// <summary>
		/// FDIVRP fpureg
		/// </summary>
		public static void FDIVRP (FPType target)
		{
		}

		/// <summary>
		/// FDIVRP fpureg,ST0
		/// </summary>
		public static void FDIVRP__ST0 (FPType target)
		{
		}

		/// <summary>
		/// FENI 
		/// </summary>
		public static void FENI ()
		{
		}

		/// <summary>
		/// FFREE fpureg
		/// </summary>
		public static void FFREE (FPType target)
		{
		}

		/// <summary>
		/// FFREEP fpureg
		/// </summary>
		public static void FFREEP (FPType target)
		{
		}

		/// <summary>
		/// FIADD mem16
		/// </summary>
		public static void FIADD (WordMemory target)
		{
		}

		/// <summary>
		/// FIADD mem16
		/// </summary>
		public unsafe static void FIADD (UInt16* target)
		{
		}

		/// <summary>
		/// FIADD mem32
		/// </summary>
		public static void FIADD (DWordMemory target)
		{
		}

		/// <summary>
		/// FIADD mem32
		/// </summary>
		public unsafe static void FIADD (UInt32* target)
		{
		}

		/// <summary>
		/// FICOM mem16
		/// </summary>
		public static void FICOM (WordMemory target)
		{
		}

		/// <summary>
		/// FICOM mem16
		/// </summary>
		public unsafe static void FICOM (UInt16* target)
		{
		}

		/// <summary>
		/// FICOM mem32
		/// </summary>
		public static void FICOM (DWordMemory target)
		{
		}

		/// <summary>
		/// FICOM mem32
		/// </summary>
		public unsafe static void FICOM (UInt32* target)
		{
		}

		/// <summary>
		/// FICOMP mem16
		/// </summary>
		public static void FICOMP (WordMemory target)
		{
		}

		/// <summary>
		/// FICOMP mem16
		/// </summary>
		public unsafe static void FICOMP (UInt16* target)
		{
		}

		/// <summary>
		/// FICOMP mem32
		/// </summary>
		public static void FICOMP (DWordMemory target)
		{
		}

		/// <summary>
		/// FICOMP mem32
		/// </summary>
		public unsafe static void FICOMP (UInt32* target)
		{
		}

		/// <summary>
		/// FIDIV mem16
		/// </summary>
		public static void FIDIV (WordMemory target)
		{
		}

		/// <summary>
		/// FIDIV mem16
		/// </summary>
		public unsafe static void FIDIV (UInt16* target)
		{
		}

		/// <summary>
		/// FIDIV mem32
		/// </summary>
		public static void FIDIV (DWordMemory target)
		{
		}

		/// <summary>
		/// FIDIV mem32
		/// </summary>
		public unsafe static void FIDIV (UInt32* target)
		{
		}

		/// <summary>
		/// FIDIVR mem16
		/// </summary>
		public static void FIDIVR (WordMemory target)
		{
		}

		/// <summary>
		/// FIDIVR mem16
		/// </summary>
		public unsafe static void FIDIVR (UInt16* target)
		{
		}

		/// <summary>
		/// FIDIVR mem32
		/// </summary>
		public static void FIDIVR (DWordMemory target)
		{
		}

		/// <summary>
		/// FIDIVR mem32
		/// </summary>
		public unsafe static void FIDIVR (UInt32* target)
		{
		}

		/// <summary>
		/// FILD mem16
		/// </summary>
		public static void FILD (WordMemory target)
		{
		}

		/// <summary>
		/// FILD mem16
		/// </summary>
		public unsafe static void FILD (UInt16* target)
		{
		}

		/// <summary>
		/// FILD mem32
		/// </summary>
		public static void FILD (DWordMemory target)
		{
		}

		/// <summary>
		/// FILD mem32
		/// </summary>
		public unsafe static void FILD (UInt32* target)
		{
		}

		/// <summary>
		/// FILD mem64
		/// </summary>
		public static void FILD (QWordMemory target)
		{
		}

		/// <summary>
		/// FIMUL mem16
		/// </summary>
		public static void FIMUL (WordMemory target)
		{
		}

		/// <summary>
		/// FIMUL mem16
		/// </summary>
		public unsafe static void FIMUL (UInt16* target)
		{
		}

		/// <summary>
		/// FIMUL mem32
		/// </summary>
		public static void FIMUL (DWordMemory target)
		{
		}

		/// <summary>
		/// FIMUL mem32
		/// </summary>
		public unsafe static void FIMUL (UInt32* target)
		{
		}

		/// <summary>
		/// FINCSTP 
		/// </summary>
		public static void FINCSTP ()
		{
		}

		/// <summary>
		/// FINIT 
		/// </summary>
		public static void FINIT ()
		{
		}

		/// <summary>
		/// FIST mem16
		/// </summary>
		public static void FIST (WordMemory target)
		{
		}

		/// <summary>
		/// FIST mem16
		/// </summary>
		public unsafe static void FIST (UInt16* target)
		{
		}

		/// <summary>
		/// FIST mem32
		/// </summary>
		public static void FIST (DWordMemory target)
		{
		}

		/// <summary>
		/// FIST mem32
		/// </summary>
		public unsafe static void FIST (UInt32* target)
		{
		}

		/// <summary>
		/// FISTP mem16
		/// </summary>
		public static void FISTP (WordMemory target)
		{
		}

		/// <summary>
		/// FISTP mem16
		/// </summary>
		public unsafe static void FISTP (UInt16* target)
		{
		}

		/// <summary>
		/// FISTP mem32
		/// </summary>
		public static void FISTP (DWordMemory target)
		{
		}

		/// <summary>
		/// FISTP mem32
		/// </summary>
		public unsafe static void FISTP (UInt32* target)
		{
		}

		/// <summary>
		/// FISTP mem64
		/// </summary>
		public static void FISTP (QWordMemory target)
		{
		}

		/// <summary>
		/// FISUB mem16
		/// </summary>
		public static void FISUB (WordMemory target)
		{
		}

		/// <summary>
		/// FISUB mem16
		/// </summary>
		public unsafe static void FISUB (UInt16* target)
		{
		}

		/// <summary>
		/// FISUB mem32
		/// </summary>
		public static void FISUB (DWordMemory target)
		{
		}

		/// <summary>
		/// FISUB mem32
		/// </summary>
		public unsafe static void FISUB (UInt32* target)
		{
		}

		/// <summary>
		/// FISUBR mem16
		/// </summary>
		public static void FISUBR (WordMemory target)
		{
		}

		/// <summary>
		/// FISUBR mem16
		/// </summary>
		public unsafe static void FISUBR (UInt16* target)
		{
		}

		/// <summary>
		/// FISUBR mem32
		/// </summary>
		public static void FISUBR (DWordMemory target)
		{
		}

		/// <summary>
		/// FISUBR mem32
		/// </summary>
		public unsafe static void FISUBR (UInt32* target)
		{
		}

		/// <summary>
		/// FLD mem32
		/// </summary>
		public static void FLD (DWordMemory target)
		{
		}

		/// <summary>
		/// FLD mem32
		/// </summary>
		public unsafe static void FLD (UInt32* target)
		{
		}

		/// <summary>
		/// FLD mem64
		/// </summary>
		public static void FLD (QWordMemory target)
		{
		}

		/// <summary>
		/// FLD mem80
		/// </summary>
		public static void FLD (TWordMemory target)
		{
		}

		/// <summary>
		/// FLD fpureg
		/// </summary>
		public static void FLD (FPType target)
		{
		}

		/// <summary>
		/// FLD1 
		/// </summary>
		public static void FLD1 ()
		{
		}

		/// <summary>
		/// FLDCW mem16
		/// </summary>
		public static void FLDCW (WordMemory target)
		{
		}

		/// <summary>
		/// FLDCW mem16
		/// </summary>
		public unsafe static void FLDCW (UInt16* target)
		{
		}

		/// <summary>
		/// FLDENV mem
		/// </summary>
		public static void FLDENV (Memory target)
		{
		}

		/// <summary>
		/// FLDENV mem
		/// </summary>
		public unsafe static void FLDENV (byte* target)
		{
		}

		/// <summary>
		/// FLDL2E 
		/// </summary>
		public static void FLDL2E ()
		{
		}

		/// <summary>
		/// FLDL2T 
		/// </summary>
		public static void FLDL2T ()
		{
		}

		/// <summary>
		/// FLDLG2 
		/// </summary>
		public static void FLDLG2 ()
		{
		}

		/// <summary>
		/// FLDLN2 
		/// </summary>
		public static void FLDLN2 ()
		{
		}

		/// <summary>
		/// FLDPI 
		/// </summary>
		public static void FLDPI ()
		{
		}

		/// <summary>
		/// FLDZ 
		/// </summary>
		public static void FLDZ ()
		{
		}

		/// <summary>
		/// FMUL mem32
		/// </summary>
		public static void FMUL (DWordMemory target)
		{
		}

		/// <summary>
		/// FMUL mem32
		/// </summary>
		public unsafe static void FMUL (UInt32* target)
		{
		}

		/// <summary>
		/// FMUL mem64
		/// </summary>
		public static void FMUL (QWordMemory target)
		{
		}

		/// <summary>
		/// FMUL fpureg
		/// </summary>
		public static void FMUL (FPType target)
		{
		}

		/// <summary>
		/// FMUL ST0,fpureg
		/// </summary>
		public static void FMUL_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FMUL fpureg,ST0
		/// </summary>
		public static void FMUL__ST0 (FPType target)
		{
		}

		/// <summary>
		/// FMULP fpureg
		/// </summary>
		public static void FMULP (FPType target)
		{
		}

		/// <summary>
		/// FMULP fpureg,ST0
		/// </summary>
		public static void FMULP__ST0 (FPType target)
		{
		}

		/// <summary>
		/// FNCLEX 
		/// </summary>
		public static void FNCLEX ()
		{
		}

		/// <summary>
		/// FNDISI 
		/// </summary>
		public static void FNDISI ()
		{
		}

		/// <summary>
		/// FNENI 
		/// </summary>
		public static void FNENI ()
		{
		}

		/// <summary>
		/// FNINIT 
		/// </summary>
		public static void FNINIT ()
		{
		}

		/// <summary>
		/// FNOP 
		/// </summary>
		public static void FNOP ()
		{
		}

		/// <summary>
		/// FNSAVE mem
		/// </summary>
		public static void FNSAVE (Memory target)
		{
		}

		/// <summary>
		/// FNSAVE mem
		/// </summary>
		public unsafe static void FNSAVE (byte* target)
		{
		}

		/// <summary>
		/// FNSTCW mem16
		/// </summary>
		public static void FNSTCW (WordMemory target)
		{
		}

		/// <summary>
		/// FNSTCW mem16
		/// </summary>
		public unsafe static void FNSTCW (UInt16* target)
		{
		}

		/// <summary>
		/// FNSTENV mem
		/// </summary>
		public static void FNSTENV (Memory target)
		{
		}

		/// <summary>
		/// FNSTENV mem
		/// </summary>
		public unsafe static void FNSTENV (byte* target)
		{
		}

		/// <summary>
		/// FNSTSW mem16
		/// </summary>
		public static void FNSTSW (WordMemory target)
		{
		}

		/// <summary>
		/// FNSTSW mem16
		/// </summary>
		public unsafe static void FNSTSW (UInt16* target)
		{
		}

		/// <summary>
		/// FNSTSW AX
		/// </summary>
		public static void FNSTSW_AX ()
		{
		}

		/// <summary>
		/// FPATAN 
		/// </summary>
		public static void FPATAN ()
		{
		}

		/// <summary>
		/// FPREM 
		/// </summary>
		public static void FPREM ()
		{
		}

		/// <summary>
		/// FPREM1 
		/// </summary>
		public static void FPREM1 ()
		{
		}

		/// <summary>
		/// FPTAN 
		/// </summary>
		public static void FPTAN ()
		{
		}

		/// <summary>
		/// FRNDINT 
		/// </summary>
		public static void FRNDINT ()
		{
		}

		/// <summary>
		/// FRSTOR mem
		/// </summary>
		public static void FRSTOR (Memory target)
		{
		}

		/// <summary>
		/// FRSTOR mem
		/// </summary>
		public unsafe static void FRSTOR (byte* target)
		{
		}

		/// <summary>
		/// FSAVE mem
		/// </summary>
		public static void FSAVE (Memory target)
		{
		}

		/// <summary>
		/// FSAVE mem
		/// </summary>
		public unsafe static void FSAVE (byte* target)
		{
		}

		/// <summary>
		/// FSCALE 
		/// </summary>
		public static void FSCALE ()
		{
		}

		/// <summary>
		/// FSETPM 
		/// </summary>
		public static void FSETPM ()
		{
		}

		/// <summary>
		/// FSIN 
		/// </summary>
		public static void FSIN ()
		{
		}

		/// <summary>
		/// FSINCOS 
		/// </summary>
		public static void FSINCOS ()
		{
		}

		/// <summary>
		/// FSQRT 
		/// </summary>
		public static void FSQRT ()
		{
		}

		/// <summary>
		/// FST mem32
		/// </summary>
		public static void FST (DWordMemory target)
		{
		}

		/// <summary>
		/// FST mem32
		/// </summary>
		public unsafe static void FST (UInt32* target)
		{
		}

		/// <summary>
		/// FST mem64
		/// </summary>
		public static void FST (QWordMemory target)
		{
		}

		/// <summary>
		/// FST fpureg
		/// </summary>
		public static void FST (FPType target)
		{
		}

		/// <summary>
		/// FSTCW mem16
		/// </summary>
		public static void FSTCW (WordMemory target)
		{
		}

		/// <summary>
		/// FSTCW mem16
		/// </summary>
		public unsafe static void FSTCW (UInt16* target)
		{
		}

		/// <summary>
		/// FSTENV mem
		/// </summary>
		public static void FSTENV (Memory target)
		{
		}

		/// <summary>
		/// FSTENV mem
		/// </summary>
		public unsafe static void FSTENV (byte* target)
		{
		}

		/// <summary>
		/// FSTP mem32
		/// </summary>
		public static void FSTP (DWordMemory target)
		{
		}

		/// <summary>
		/// FSTP mem32
		/// </summary>
		public unsafe static void FSTP (UInt32* target)
		{
		}

		/// <summary>
		/// FSTP mem64
		/// </summary>
		public static void FSTP (QWordMemory target)
		{
		}

		/// <summary>
		/// FSTP mem80
		/// </summary>
		public static void FSTP (TWordMemory target)
		{
		}

		/// <summary>
		/// FSTP fpureg
		/// </summary>
		public static void FSTP (FPType target)
		{
		}

		/// <summary>
		/// FSTSW mem16
		/// </summary>
		public static void FSTSW (WordMemory target)
		{
		}

		/// <summary>
		/// FSTSW mem16
		/// </summary>
		public unsafe static void FSTSW (UInt16* target)
		{
		}

		/// <summary>
		/// FSTSW AX
		/// </summary>
		public static void FSTSW_AX ()
		{
		}

		/// <summary>
		/// FSUB mem32
		/// </summary>
		public static void FSUB (DWordMemory target)
		{
		}

		/// <summary>
		/// FSUB mem32
		/// </summary>
		public unsafe static void FSUB (UInt32* target)
		{
		}

		/// <summary>
		/// FSUB mem64
		/// </summary>
		public static void FSUB (QWordMemory target)
		{
		}

		/// <summary>
		/// FSUB fpureg
		/// </summary>
		public static void FSUB (FPType target)
		{
		}

		/// <summary>
		/// FSUB ST0,fpureg
		/// </summary>
		public static void FSUB_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FSUB fpureg,ST0
		/// </summary>
		public static void FSUB__ST0 (FPType target)
		{
		}

		/// <summary>
		/// FSUBP fpureg
		/// </summary>
		public static void FSUBP (FPType target)
		{
		}

		/// <summary>
		/// FSUBP fpureg,ST0
		/// </summary>
		public static void FSUBP__ST0 (FPType target)
		{
		}

		/// <summary>
		/// FSUBR mem32
		/// </summary>
		public static void FSUBR (DWordMemory target)
		{
		}

		/// <summary>
		/// FSUBR mem32
		/// </summary>
		public unsafe static void FSUBR (UInt32* target)
		{
		}

		/// <summary>
		/// FSUBR mem64
		/// </summary>
		public static void FSUBR (QWordMemory target)
		{
		}

		/// <summary>
		/// FSUBR fpureg
		/// </summary>
		public static void FSUBR (FPType target)
		{
		}

		/// <summary>
		/// FSUBR ST0,fpureg
		/// </summary>
		public static void FSUBR_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FSUBR fpureg,ST0
		/// </summary>
		public static void FSUBR__ST0 (FPType target)
		{
		}

		/// <summary>
		/// FSUBRP fpureg
		/// </summary>
		public static void FSUBRP (FPType target)
		{
		}

		/// <summary>
		/// FSUBRP fpureg,ST0
		/// </summary>
		public static void FSUBRP__ST0 (FPType target)
		{
		}

		/// <summary>
		/// FTST 
		/// </summary>
		public static void FTST ()
		{
		}

		/// <summary>
		/// FUCOM fpureg
		/// </summary>
		public static void FUCOM (FPType target)
		{
		}

		/// <summary>
		/// FUCOM ST0,fpureg
		/// </summary>
		public static void FUCOM_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FUCOMI fpureg
		/// </summary>
		public static void FUCOMI (FPType target)
		{
		}

		/// <summary>
		/// FUCOMI ST0,fpureg
		/// </summary>
		public static void FUCOMI_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FUCOMIP fpureg
		/// </summary>
		public static void FUCOMIP (FPType target)
		{
		}

		/// <summary>
		/// FUCOMIP ST0,fpureg
		/// </summary>
		public static void FUCOMIP_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FUCOMP fpureg
		/// </summary>
		public static void FUCOMP (FPType target)
		{
		}

		/// <summary>
		/// FUCOMP ST0,fpureg
		/// </summary>
		public static void FUCOMP_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FUCOMPP 
		/// </summary>
		public static void FUCOMPP ()
		{
		}

		/// <summary>
		/// FWAIT 
		/// </summary>
		public static void FWAIT ()
		{
		}

		/// <summary>
		/// FXAM 
		/// </summary>
		public static void FXAM ()
		{
		}

		/// <summary>
		/// FXCH 
		/// </summary>
		public static void FXCH ()
		{
		}

		/// <summary>
		/// FXCH fpureg
		/// </summary>
		public static void FXCH (FPType target)
		{
		}

		/// <summary>
		/// FXCH fpureg,ST0
		/// </summary>
		public static void FXCH__ST0 (FPType target)
		{
		}

		/// <summary>
		/// FXCH ST0,fpureg
		/// </summary>
		public static void FXCH_ST0 (FPType source)
		{
		}

		/// <summary>
		/// FXRSTOR memory
		/// </summary>
		public static void FXRSTOR (Memory target)
		{
		}

		/// <summary>
		/// FXRSTOR memory
		/// </summary>
		public unsafe static void FXRSTOR (byte* target)
		{
		}

		/// <summary>
		/// FXSAVE memory
		/// </summary>
		public static void FXSAVE (Memory target)
		{
		}

		/// <summary>
		/// FXSAVE memory
		/// </summary>
		public unsafe static void FXSAVE (byte* target)
		{
		}

		/// <summary>
		/// FXTRACT 
		/// </summary>
		public static void FXTRACT ()
		{
		}

		/// <summary>
		/// FYL2X 
		/// </summary>
		public static void FYL2X ()
		{
		}

		/// <summary>
		/// FYL2XP1 
		/// </summary>
		public static void FYL2XP1 ()
		{
		}

		/// <summary>
		/// HLT 
		/// </summary>
		public static void HLT ()
		{
		}

		/// <summary>
		/// ICEBP 
		/// </summary>
		public static void ICEBP ()
		{
		}

		/// <summary>
		/// IDIV mem8
		/// </summary>
		public static void IDIV (ByteMemory target)
		{
		}

		/// <summary>
		/// IDIV mem8
		/// </summary>
		public unsafe static void IDIV (byte* target)
		{
		}

		/// <summary>
		/// IDIV mem16
		/// </summary>
		public static void IDIV (WordMemory target)
		{
		}

		/// <summary>
		/// IDIV mem16
		/// </summary>
		public unsafe static void IDIV (UInt16* target)
		{
		}

		/// <summary>
		/// IDIV mem32
		/// </summary>
		public static void IDIV (DWordMemory target)
		{
		}

		/// <summary>
		/// IDIV mem32
		/// </summary>
		public unsafe static void IDIV (UInt32* target)
		{
		}

		/// <summary>
		/// IDIV rmreg8
		/// </summary>
		public static void IDIV (R8Type target)
		{
		}

		/// <summary>
		/// IDIV rmreg16
		/// </summary>
		public static void IDIV (R16Type target)
		{
		}

		/// <summary>
		/// IDIV rmreg32
		/// </summary>
		public static void IDIV (R32Type target)
		{
		}

		/// <summary>
		/// IMUL mem8
		/// </summary>
		public static void IMUL (ByteMemory target)
		{
		}

		/// <summary>
		/// IMUL mem8
		/// </summary>
		public unsafe static void IMUL (byte* target)
		{
		}

		/// <summary>
		/// IMUL mem16
		/// </summary>
		public static void IMUL (WordMemory target)
		{
		}

		/// <summary>
		/// IMUL mem16
		/// </summary>
		public unsafe static void IMUL (UInt16* target)
		{
		}

		/// <summary>
		/// IMUL mem32
		/// </summary>
		public static void IMUL (DWordMemory target)
		{
		}

		/// <summary>
		/// IMUL mem32
		/// </summary>
		public unsafe static void IMUL (UInt32* target)
		{
		}

		/// <summary>
		/// IMUL reg16,mem16
		/// </summary>
		public static void IMUL (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// IMUL reg16,mem16
		/// </summary>
		public unsafe static void IMUL (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// IMUL reg32,mem32
		/// </summary>
		public static void IMUL (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// IMUL reg32,mem32
		/// </summary>
		public unsafe static void IMUL (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// IMUL reg16,imm8
		/// </summary>
		public static void IMUL (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// IMUL reg16,imm16
		/// </summary>
		public static void IMUL (R16Type target, UInt16 source)
		{
		}

		/// <summary>
		/// IMUL reg32,imm8
		/// </summary>
		public static void IMUL (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// IMUL reg32,imm32
		/// </summary>
		public static void IMUL (R32Type target, UInt32 source)
		{
		}

		/// <summary>
		/// IMUL reg16,mem16,imm8
		/// </summary>
		public static void IMUL (R16Type target, WordMemory source, Byte value)
		{
		}

		/// <summary>
		/// IMUL reg16,mem16,imm8
		/// </summary>
		public unsafe static void IMUL (R16Type target, UInt16* source, Byte value)
		{
		}

		/// <summary>
		/// IMUL reg16,mem16,imm16
		/// </summary>
		public static void IMUL (R16Type target, WordMemory source, UInt16 value)
		{
		}

		/// <summary>
		/// IMUL reg16,mem16,imm16
		/// </summary>
		public unsafe static void IMUL (R16Type target, UInt16* source, UInt16 value)
		{
		}

		/// <summary>
		/// IMUL reg32,mem32,imm8
		/// </summary>
		public static void IMUL (R32Type target, DWordMemory source, Byte value)
		{
		}

		/// <summary>
		/// IMUL reg32,mem32,imm8
		/// </summary>
		public unsafe static void IMUL (R32Type target, UInt32* source, Byte value)
		{
		}

		/// <summary>
		/// IMUL reg32,mem32,imm32
		/// </summary>
		public static void IMUL (R32Type target, DWordMemory source, UInt32 value)
		{
		}

		/// <summary>
		/// IMUL reg32,mem32,imm32
		/// </summary>
		public unsafe static void IMUL (R32Type target, UInt32* source, UInt32 value)
		{
		}

		/// <summary>
		/// IMUL rmreg8
		/// </summary>
		public static void IMUL (R8Type target)
		{
		}

		/// <summary>
		/// IMUL rmreg16
		/// </summary>
		public static void IMUL (R16Type target)
		{
		}

		/// <summary>
		/// IMUL rmreg32
		/// </summary>
		public static void IMUL (R32Type target)
		{
		}

		/// <summary>
		/// IMUL reg16,rmreg16
		/// </summary>
		public static void IMUL (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// IMUL reg32,rmreg32
		/// </summary>
		public static void IMUL (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// IMUL reg16,rmreg16,imm8
		/// </summary>
		public static void IMUL (R16Type target, R16Type source, Byte value)
		{
		}

		/// <summary>
		/// IMUL reg16,rmreg16,imm16
		/// </summary>
		public static void IMUL (R16Type target, R16Type source, UInt16 value)
		{
		}

		/// <summary>
		/// IMUL reg32,rmreg32,imm8
		/// </summary>
		public static void IMUL (R32Type target, R32Type source, Byte value)
		{
		}

		/// <summary>
		/// IMUL reg32,rmreg32,imm32
		/// </summary>
		public static void IMUL (R32Type target, R32Type source, UInt32 value)
		{
		}

		/// <summary>
		/// IN AL,imm8
		/// </summary>
		public static void IN_AL (Byte source)
		{
		}

		/// <summary>
		/// IN AX,imm8
		/// </summary>
		public static void IN_AX (Byte source)
		{
		}

		/// <summary>
		/// IN EAX,imm8
		/// </summary>
		public static void IN_EAX (Byte source)
		{
		}

		/// <summary>
		/// IN AL,DX
		/// </summary>
		public static void IN_AL__DX ()
		{
		}

		/// <summary>
		/// IN AX,DX
		/// </summary>
		public static void IN_AX__DX ()
		{
		}

		/// <summary>
		/// IN EAX,DX
		/// </summary>
		public static void IN_EAX__DX ()
		{
		}

		/// <summary>
		/// INC reg16
		/// </summary>
		public static void INC (R16Type target)
		{
		}

		/// <summary>
		/// INC reg32
		/// </summary>
		public static void INC (R32Type target)
		{
		}

		/// <summary>
		/// INC mem8
		/// </summary>
		public static void INC (ByteMemory target)
		{
		}

		/// <summary>
		/// INC mem8
		/// </summary>
		public unsafe static void INC (byte* target)
		{
		}

		/// <summary>
		/// INC mem16
		/// </summary>
		public static void INC (WordMemory target)
		{
		}

		/// <summary>
		/// INC mem16
		/// </summary>
		public unsafe static void INC (UInt16* target)
		{
		}

		/// <summary>
		/// INC mem32
		/// </summary>
		public static void INC (DWordMemory target)
		{
		}

		/// <summary>
		/// INC mem32
		/// </summary>
		public unsafe static void INC (UInt32* target)
		{
		}

		/// <summary>
		/// INC rmreg8
		/// </summary>
		public static void INC (R8Type target)
		{
		}

		/// <summary>
		/// INSB 
		/// </summary>
		public static void INSB ()
		{
		}

		/// <summary>
		/// INSD 
		/// </summary>
		public static void INSD ()
		{
		}

		/// <summary>
		/// INSW 
		/// </summary>
		public static void INSW ()
		{
		}

		/// <summary>
		/// INT imm8
		/// </summary>
		public static void INT (Byte target)
		{
		}

		/// <summary>
		/// INTO 
		/// </summary>
		public static void INTO ()
		{
		}

		/// <summary>
		/// INVD 
		/// </summary>
		public static void INVD ()
		{
		}

		/// <summary>
		/// INVLPG mem
		/// </summary>
		public static void INVLPG (Memory target)
		{
		}

		/// <summary>
		/// INVLPG mem
		/// </summary>
		public unsafe static void INVLPG (byte* target)
		{
		}

		/// <summary>
		/// IRET 
		/// </summary>
		public static void IRET ()
		{
		}

		/// <summary>
		/// IRETD 
		/// </summary>
		public static void IRETD ()
		{
		}

		/// <summary>
		/// IRETW 
		/// </summary>
		public static void IRETW ()
		{
		}

		/// <summary>
		/// JA imm8
		/// </summary>
		public static void JA (Byte target)
		{
		}

		/// <summary>
		/// JA imm8
		/// </summary>
		public static void JA (string label)
		{
		}

		/// <summary>
		/// JA NEAR imm
		/// </summary>
		public static void JA (UInt32 target)
		{
		}

		/// <summary>
		/// JAE imm8
		/// </summary>
		public static void JAE (Byte target)
		{
		}

		/// <summary>
		/// JAE imm8
		/// </summary>
		public static void JAE (string label)
		{
		}

		/// <summary>
		/// JAE NEAR imm
		/// </summary>
		public static void JAE (UInt32 target)
		{
		}

		/// <summary>
		/// JB imm8
		/// </summary>
		public static void JB (Byte target)
		{
		}

		/// <summary>
		/// JB imm8
		/// </summary>
		public static void JB (string label)
		{
		}

		/// <summary>
		/// JB NEAR imm
		/// </summary>
		public static void JB (UInt32 target)
		{
		}

		/// <summary>
		/// JBE imm8
		/// </summary>
		public static void JBE (Byte target)
		{
		}

		/// <summary>
		/// JBE imm8
		/// </summary>
		public static void JBE (string label)
		{
		}

		/// <summary>
		/// JBE NEAR imm
		/// </summary>
		public static void JBE (UInt32 target)
		{
		}

		/// <summary>
		/// JC imm8
		/// </summary>
		public static void JC (Byte target)
		{
		}

		/// <summary>
		/// JC imm8
		/// </summary>
		public static void JC (string label)
		{
		}

		/// <summary>
		/// JC NEAR imm
		/// </summary>
		public static void JC (UInt32 target)
		{
		}

		/// <summary>
		/// JCXZ imm8
		/// </summary>
		public static void JCXZ (Byte target)
		{
		}

		/// <summary>
		/// JCXZ imm8
		/// </summary>
		public static void JCXZ (string label)
		{
		}

		/// <summary>
		/// JE imm8
		/// </summary>
		public static void JE (Byte target)
		{
		}

		/// <summary>
		/// JE imm8
		/// </summary>
		public static void JE (string label)
		{
		}

		/// <summary>
		/// JE NEAR imm
		/// </summary>
		public static void JE (UInt32 target)
		{
		}

		/// <summary>
		/// JECXZ imm8
		/// </summary>
		public static void JECXZ (Byte target)
		{
		}

		/// <summary>
		/// JECXZ imm8
		/// </summary>
		public static void JECXZ (string label)
		{
		}

		/// <summary>
		/// JG imm8
		/// </summary>
		public static void JG (Byte target)
		{
		}

		/// <summary>
		/// JG imm8
		/// </summary>
		public static void JG (string label)
		{
		}

		/// <summary>
		/// JG NEAR imm
		/// </summary>
		public static void JG (UInt32 target)
		{
		}

		/// <summary>
		/// JGE imm8
		/// </summary>
		public static void JGE (Byte target)
		{
		}

		/// <summary>
		/// JGE imm8
		/// </summary>
		public static void JGE (string label)
		{
		}

		/// <summary>
		/// JGE NEAR imm
		/// </summary>
		public static void JGE (UInt32 target)
		{
		}

		/// <summary>
		/// JL imm8
		/// </summary>
		public static void JL (Byte target)
		{
		}

		/// <summary>
		/// JL imm8
		/// </summary>
		public static void JL (string label)
		{
		}

		/// <summary>
		/// JL NEAR imm
		/// </summary>
		public static void JL (UInt32 target)
		{
		}

		/// <summary>
		/// JLE imm8
		/// </summary>
		public static void JLE (Byte target)
		{
		}

		/// <summary>
		/// JLE imm8
		/// </summary>
		public static void JLE (string label)
		{
		}

		/// <summary>
		/// JLE NEAR imm
		/// </summary>
		public static void JLE (UInt32 target)
		{
		}

		/// <summary>
		/// JMP imm
		/// </summary>
		public static void JMP (UInt32 target)
		{
		}

		/// <summary>
		/// JMP imm
		/// </summary>
		public static void JMP (string label)
		{
		}

		/// <summary>
		/// JMP imm8
		/// </summary>
		public static void JMP (Byte target)
		{
		}

		/// <summary>
		/// JMP imm16:imm16
		/// </summary>
		public static void JMP (UInt16 target, UInt16 source)
		{
		}

		/// <summary>
		/// JMP imm16:imm32
		/// </summary>
		public static void JMP (UInt16 target, UInt32 source)
		{
		}

		/// <summary>
		/// JMP imm16:imm32
		/// </summary>
		public static void JMP (ushort target, string label)
		{
		}

		/// <summary>
		/// JMP FAR mem
		/// </summary>
		public static void JMP_FAR (Memory target)
		{
		}

		/// <summary>
		/// JMP FAR mem
		/// </summary>
		public unsafe static void JMP_FAR (byte* target)
		{
		}

		/// <summary>
		/// JMP FAR mem
		/// </summary>
		public static void JMP_FAR (string label)
		{
		}

		/// <summary>
		/// JMP FAR mem32
		/// </summary>
		public static void JMP_FAR (DWordMemory target)
		{
		}

		/// <summary>
		/// JMP FAR mem32
		/// </summary>
		public unsafe static void JMP_FAR (UInt32* target)
		{
		}

		/// <summary>
		/// JMP mem16
		/// </summary>
		public static void JMP (WordMemory target)
		{
		}

		/// <summary>
		/// JMP mem16
		/// </summary>
		public unsafe static void JMP (UInt16* target)
		{
		}

		/// <summary>
		/// JMP mem32
		/// </summary>
		public static void JMP (DWordMemory target)
		{
		}

		/// <summary>
		/// JMP mem32
		/// </summary>
		public unsafe static void JMP (UInt32* target)
		{
		}

		/// <summary>
		/// JMP rmreg16
		/// </summary>
		public static void JMP (R16Type target)
		{
		}

		/// <summary>
		/// JMP rmreg32
		/// </summary>
		public static void JMP (R32Type target)
		{
		}

		/// <summary>
		/// JNA imm8
		/// </summary>
		public static void JNA (Byte target)
		{
		}

		/// <summary>
		/// JNA imm8
		/// </summary>
		public static void JNA (string label)
		{
		}

		/// <summary>
		/// JNA NEAR imm
		/// </summary>
		public static void JNA (UInt32 target)
		{
		}

		/// <summary>
		/// JNAE imm8
		/// </summary>
		public static void JNAE (Byte target)
		{
		}

		/// <summary>
		/// JNAE imm8
		/// </summary>
		public static void JNAE (string label)
		{
		}

		/// <summary>
		/// JNAE NEAR imm
		/// </summary>
		public static void JNAE (UInt32 target)
		{
		}

		/// <summary>
		/// JNB imm8
		/// </summary>
		public static void JNB (Byte target)
		{
		}

		/// <summary>
		/// JNB imm8
		/// </summary>
		public static void JNB (string label)
		{
		}

		/// <summary>
		/// JNB NEAR imm
		/// </summary>
		public static void JNB (UInt32 target)
		{
		}

		/// <summary>
		/// JNBE imm8
		/// </summary>
		public static void JNBE (Byte target)
		{
		}

		/// <summary>
		/// JNBE imm8
		/// </summary>
		public static void JNBE (string label)
		{
		}

		/// <summary>
		/// JNBE NEAR imm
		/// </summary>
		public static void JNBE (UInt32 target)
		{
		}

		/// <summary>
		/// JNC imm8
		/// </summary>
		public static void JNC (Byte target)
		{
		}

		/// <summary>
		/// JNC imm8
		/// </summary>
		public static void JNC (string label)
		{
		}

		/// <summary>
		/// JNC NEAR imm
		/// </summary>
		public static void JNC (UInt32 target)
		{
		}

		/// <summary>
		/// JNE imm8
		/// </summary>
		public static void JNE (Byte target)
		{
		}

		/// <summary>
		/// JNE imm8
		/// </summary>
		public static void JNE (string label)
		{
		}

		/// <summary>
		/// JNE NEAR imm
		/// </summary>
		public static void JNE (UInt32 target)
		{
		}

		/// <summary>
		/// JNG imm8
		/// </summary>
		public static void JNG (Byte target)
		{
		}

		/// <summary>
		/// JNG imm8
		/// </summary>
		public static void JNG (string label)
		{
		}

		/// <summary>
		/// JNG NEAR imm
		/// </summary>
		public static void JNG (UInt32 target)
		{
		}

		/// <summary>
		/// JNGE imm8
		/// </summary>
		public static void JNGE (Byte target)
		{
		}

		/// <summary>
		/// JNGE imm8
		/// </summary>
		public static void JNGE (string label)
		{
		}

		/// <summary>
		/// JNGE NEAR imm
		/// </summary>
		public static void JNGE (UInt32 target)
		{
		}

		/// <summary>
		/// JNL imm8
		/// </summary>
		public static void JNL (Byte target)
		{
		}

		/// <summary>
		/// JNL imm8
		/// </summary>
		public static void JNL (string label)
		{
		}

		/// <summary>
		/// JNL NEAR imm
		/// </summary>
		public static void JNL (UInt32 target)
		{
		}

		/// <summary>
		/// JNLE imm8
		/// </summary>
		public static void JNLE (Byte target)
		{
		}

		/// <summary>
		/// JNLE imm8
		/// </summary>
		public static void JNLE (string label)
		{
		}

		/// <summary>
		/// JNLE NEAR imm
		/// </summary>
		public static void JNLE (UInt32 target)
		{
		}

		/// <summary>
		/// JNO imm8
		/// </summary>
		public static void JNO (Byte target)
		{
		}

		/// <summary>
		/// JNO imm8
		/// </summary>
		public static void JNO (string label)
		{
		}

		/// <summary>
		/// JNO NEAR imm
		/// </summary>
		public static void JNO (UInt32 target)
		{
		}

		/// <summary>
		/// JNP imm8
		/// </summary>
		public static void JNP (Byte target)
		{
		}

		/// <summary>
		/// JNP imm8
		/// </summary>
		public static void JNP (string label)
		{
		}

		/// <summary>
		/// JNP NEAR imm
		/// </summary>
		public static void JNP (UInt32 target)
		{
		}

		/// <summary>
		/// JNS imm8
		/// </summary>
		public static void JNS (Byte target)
		{
		}

		/// <summary>
		/// JNS imm8
		/// </summary>
		public static void JNS (string label)
		{
		}

		/// <summary>
		/// JNS NEAR imm
		/// </summary>
		public static void JNS (UInt32 target)
		{
		}

		/// <summary>
		/// JNZ imm8
		/// </summary>
		public static void JNZ (Byte target)
		{
		}

		/// <summary>
		/// JNZ imm8
		/// </summary>
		public static void JNZ (string label)
		{
		}

		/// <summary>
		/// JNZ NEAR imm
		/// </summary>
		public static void JNZ (UInt32 target)
		{
		}

		/// <summary>
		/// JO imm8
		/// </summary>
		public static void JO (Byte target)
		{
		}

		/// <summary>
		/// JO imm8
		/// </summary>
		public static void JO (string label)
		{
		}

		/// <summary>
		/// JO NEAR imm
		/// </summary>
		public static void JO (UInt32 target)
		{
		}

		/// <summary>
		/// JP imm8
		/// </summary>
		public static void JP (Byte target)
		{
		}

		/// <summary>
		/// JP imm8
		/// </summary>
		public static void JP (string label)
		{
		}

		/// <summary>
		/// JP NEAR imm
		/// </summary>
		public static void JP (UInt32 target)
		{
		}

		/// <summary>
		/// JPE imm8
		/// </summary>
		public static void JPE (Byte target)
		{
		}

		/// <summary>
		/// JPE imm8
		/// </summary>
		public static void JPE (string label)
		{
		}

		/// <summary>
		/// JPE NEAR imm
		/// </summary>
		public static void JPE (UInt32 target)
		{
		}

		/// <summary>
		/// JPO imm8
		/// </summary>
		public static void JPO (Byte target)
		{
		}

		/// <summary>
		/// JPO imm8
		/// </summary>
		public static void JPO (string label)
		{
		}

		/// <summary>
		/// JPO NEAR imm
		/// </summary>
		public static void JPO (UInt32 target)
		{
		}

		/// <summary>
		/// JS imm8
		/// </summary>
		public static void JS (Byte target)
		{
		}

		/// <summary>
		/// JS imm8
		/// </summary>
		public static void JS (string label)
		{
		}

		/// <summary>
		/// JS NEAR imm
		/// </summary>
		public static void JS (UInt32 target)
		{
		}

		/// <summary>
		/// JZ imm8
		/// </summary>
		public static void JZ (Byte target)
		{
		}

		/// <summary>
		/// JZ imm8
		/// </summary>
		public static void JZ (string label)
		{
		}

		/// <summary>
		/// JZ NEAR imm
		/// </summary>
		public static void JZ (UInt32 target)
		{
		}

		/// <summary>
		/// LABEL 
		/// </summary>
		public static void LABEL (string label)
		{
		}

		/// <summary>
		/// LAHF 
		/// </summary>
		public static void LAHF ()
		{
		}

		/// <summary>
		/// LAR reg16,mem16
		/// </summary>
		public static void LAR (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// LAR reg16,mem16
		/// </summary>
		public unsafe static void LAR (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// LAR reg32,mem32
		/// </summary>
		public static void LAR (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// LAR reg32,mem32
		/// </summary>
		public unsafe static void LAR (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// LAR reg16,rmreg16
		/// </summary>
		public static void LAR (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// LAR reg32,rmreg32
		/// </summary>
		public static void LAR (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// LDS reg16,mem
		/// </summary>
		public static void LDS (R16Type target, Memory source)
		{
		}

		/// <summary>
		/// LDS reg16,mem
		/// </summary>
		public unsafe static void LDS (R16Type target, byte* source)
		{
		}

		/// <summary>
		/// LDS reg32,mem
		/// </summary>
		public static void LDS (R32Type target, Memory source)
		{
		}

		/// <summary>
		/// LDS reg32,mem
		/// </summary>
		public unsafe static void LDS (R32Type target, byte* source)
		{
		}

		/// <summary>
		/// LEA reg16,mem
		/// </summary>
		public static void LEA (R16Type target, Memory source)
		{
		}

		/// <summary>
		/// LEA reg16,mem
		/// </summary>
		public unsafe static void LEA (R16Type target, byte* source)
		{
		}

		/// <summary>
		/// LEA reg32,mem
		/// </summary>
		public static void LEA (R32Type target, Memory source)
		{
		}

		/// <summary>
		/// LEA reg32,mem
		/// </summary>
		public unsafe static void LEA (R32Type target, byte* source)
		{
		}

		/// <summary>
		/// LEAVE 
		/// </summary>
		public static void LEAVE ()
		{
		}

		/// <summary>
		/// LES reg16,mem
		/// </summary>
		public static void LES (R16Type target, Memory source)
		{
		}

		/// <summary>
		/// LES reg16,mem
		/// </summary>
		public unsafe static void LES (R16Type target, byte* source)
		{
		}

		/// <summary>
		/// LES reg32,mem
		/// </summary>
		public static void LES (R32Type target, Memory source)
		{
		}

		/// <summary>
		/// LES reg32,mem
		/// </summary>
		public unsafe static void LES (R32Type target, byte* source)
		{
		}

		/// <summary>
		/// LFENCE 
		/// </summary>
		public static void LFENCE ()
		{
		}

		/// <summary>
		/// LFS reg16,mem
		/// </summary>
		public static void LFS (R16Type target, Memory source)
		{
		}

		/// <summary>
		/// LFS reg16,mem
		/// </summary>
		public unsafe static void LFS (R16Type target, byte* source)
		{
		}

		/// <summary>
		/// LFS reg32,mem
		/// </summary>
		public static void LFS (R32Type target, Memory source)
		{
		}

		/// <summary>
		/// LFS reg32,mem
		/// </summary>
		public unsafe static void LFS (R32Type target, byte* source)
		{
		}

		/// <summary>
		/// LGDT mem
		/// </summary>
		public static void LGDT (Memory target)
		{
		}

		/// <summary>
		/// LGDT mem
		/// </summary>
		public unsafe static void LGDT (byte* target)
		{
		}

		/// <summary>
		/// LGS reg16,mem
		/// </summary>
		public static void LGS (R16Type target, Memory source)
		{
		}

		/// <summary>
		/// LGS reg16,mem
		/// </summary>
		public unsafe static void LGS (R16Type target, byte* source)
		{
		}

		/// <summary>
		/// LGS reg32,mem
		/// </summary>
		public static void LGS (R32Type target, Memory source)
		{
		}

		/// <summary>
		/// LGS reg32,mem
		/// </summary>
		public unsafe static void LGS (R32Type target, byte* source)
		{
		}

		/// <summary>
		/// LIDT mem
		/// </summary>
		public static void LIDT (Memory target)
		{
		}

		/// <summary>
		/// LIDT mem
		/// </summary>
		public unsafe static void LIDT (byte* target)
		{
		}

		/// <summary>
		/// LLDT mem16
		/// </summary>
		public static void LLDT (WordMemory target)
		{
		}

		/// <summary>
		/// LLDT mem16
		/// </summary>
		public unsafe static void LLDT (UInt16* target)
		{
		}

		/// <summary>
		/// LLDT rmreg16
		/// </summary>
		public static void LLDT (R16Type target)
		{
		}

		/// <summary>
		/// LMSW mem16
		/// </summary>
		public static void LMSW (WordMemory target)
		{
		}

		/// <summary>
		/// LMSW mem16
		/// </summary>
		public unsafe static void LMSW (UInt16* target)
		{
		}

		/// <summary>
		/// LMSW rmreg16
		/// </summary>
		public static void LMSW (R16Type target)
		{
		}

		/// <summary>
		/// LOCK 
		/// </summary>
		public static void LOCK ()
		{
		}

		/// <summary>
		/// LODSB 
		/// </summary>
		public static void LODSB ()
		{
		}

		/// <summary>
		/// LODSD 
		/// </summary>
		public static void LODSD ()
		{
		}

		/// <summary>
		/// LODSW 
		/// </summary>
		public static void LODSW ()
		{
		}

		/// <summary>
		/// LOOP imm8
		/// </summary>
		public static void LOOP (Byte target)
		{
		}

		/// <summary>
		/// LOOPE imm8
		/// </summary>
		public static void LOOPE (Byte target)
		{
		}

		/// <summary>
		/// LOOPNE imm8
		/// </summary>
		public static void LOOPNE (Byte target)
		{
		}

		/// <summary>
		/// LOOPNZ imm8
		/// </summary>
		public static void LOOPNZ (Byte target)
		{
		}

		/// <summary>
		/// LOOPZ imm8
		/// </summary>
		public static void LOOPZ (Byte target)
		{
		}

		/// <summary>
		/// LSL reg16,mem16
		/// </summary>
		public static void LSL (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// LSL reg16,mem16
		/// </summary>
		public unsafe static void LSL (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// LSL reg32,mem32
		/// </summary>
		public static void LSL (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// LSL reg32,mem32
		/// </summary>
		public unsafe static void LSL (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// LSL reg16,rmreg16
		/// </summary>
		public static void LSL (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// LSL reg32,rmreg32
		/// </summary>
		public static void LSL (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// LSS reg16,mem
		/// </summary>
		public static void LSS (R16Type target, Memory source)
		{
		}

		/// <summary>
		/// LSS reg16,mem
		/// </summary>
		public unsafe static void LSS (R16Type target, byte* source)
		{
		}

		/// <summary>
		/// LSS reg32,mem
		/// </summary>
		public static void LSS (R32Type target, Memory source)
		{
		}

		/// <summary>
		/// LSS reg32,mem
		/// </summary>
		public unsafe static void LSS (R32Type target, byte* source)
		{
		}

		/// <summary>
		/// LTR mem16
		/// </summary>
		public static void LTR (WordMemory target)
		{
		}

		/// <summary>
		/// LTR mem16
		/// </summary>
		public unsafe static void LTR (UInt16* target)
		{
		}

		/// <summary>
		/// LTR rmreg16
		/// </summary>
		public static void LTR (R16Type target)
		{
		}

		/// <summary>
		/// MFENCE 
		/// </summary>
		public static void MFENCE ()
		{
		}

		/// <summary>
		/// MOV mem8,reg8
		/// </summary>
		public static void MOV (ByteMemory target, R8Type source)
		{
		}

		/// <summary>
		/// MOV mem8,reg8
		/// </summary>
		public unsafe static void MOV (byte* target, R8Type source)
		{
		}

		/// <summary>
		/// MOV mem16,reg16
		/// </summary>
		public static void MOV (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// MOV mem16,reg16
		/// </summary>
		public unsafe static void MOV (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// MOV mem32,reg32
		/// </summary>
		public static void MOV (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// MOV mem32,reg32
		/// </summary>
		public unsafe static void MOV (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// MOV reg8,mem8
		/// </summary>
		public static void MOV (R8Type target, ByteMemory source)
		{
		}

		/// <summary>
		/// MOV reg8,mem8
		/// </summary>
		public unsafe static void MOV (R8Type target, byte* source)
		{
		}

		/// <summary>
		/// MOV reg16,mem16
		/// </summary>
		public static void MOV (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// MOV reg16,mem16
		/// </summary>
		public unsafe static void MOV (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// MOV reg32,mem32
		/// </summary>
		public static void MOV (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// MOV reg32,mem32
		/// </summary>
		public unsafe static void MOV (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// MOV reg8,imm8
		/// </summary>
		public static void MOV (R8Type target, Byte source)
		{
		}

		/// <summary>
		/// MOV reg16,imm16
		/// </summary>
		public static void MOV (R16Type target, UInt16 source)
		{
		}

		/// <summary>
		/// MOV reg32,imm32
		/// </summary>
		public static void MOV (R32Type target, UInt32 source)
		{
		}

		/// <summary>
		/// MOV mem8,imm8
		/// </summary>
		public static void MOV (ByteMemory target, Byte source)
		{
		}

		/// <summary>
		/// MOV mem8,imm8
		/// </summary>
		public unsafe static void MOV (byte* target, Byte source)
		{
		}

		/// <summary>
		/// MOV mem16,imm16
		/// </summary>
		public static void MOV (WordMemory target, UInt16 source)
		{
		}

		/// <summary>
		/// MOV mem16,imm16
		/// </summary>
		public unsafe static void MOV (UInt16* target, UInt16 source)
		{
		}

		/// <summary>
		/// MOV mem32,imm32
		/// </summary>
		public static void MOV (DWordMemory target, UInt32 source)
		{
		}

		/// <summary>
		/// MOV mem32,imm32
		/// </summary>
		public unsafe static void MOV (UInt32* target, UInt32 source)
		{
		}

		/// <summary>
		/// MOV AL,memoffs8
		/// </summary>
		public static void MOV_AL (byte source)
		{
		}

		/// <summary>
		/// MOV AX,memoffs16
		/// </summary>
		public static void MOV_AX (UInt16 source)
		{
		}

		/// <summary>
		/// MOV EAX,memoffs32
		/// </summary>
		public static void MOV_EAX (UInt32 source)
		{
		}

		/// <summary>
		/// MOV memoffs8,AL
		/// </summary>
		public static void MOV__AL (byte target)
		{
		}

		/// <summary>
		/// MOV memoffs16,AX
		/// </summary>
		public static void MOV__AX (UInt16 target)
		{
		}

		/// <summary>
		/// MOV memoffs32,EAX
		/// </summary>
		public static void MOV__EAX (UInt32 target)
		{
		}

		/// <summary>
		/// MOV mem16,segreg
		/// </summary>
		public static void MOV (WordMemory target, SegType source)
		{
		}

		/// <summary>
		/// MOV mem16,segreg
		/// </summary>
		public unsafe static void MOV (UInt16* target, SegType source)
		{
		}

		/// <summary>
		/// MOV mem32,segreg
		/// </summary>
		public static void MOV (DWordMemory target, SegType source)
		{
		}

		/// <summary>
		/// MOV mem32,segreg
		/// </summary>
		public unsafe static void MOV (UInt32* target, SegType source)
		{
		}

		/// <summary>
		/// MOV segreg,mem16
		/// </summary>
		public static void MOV (SegType target, WordMemory source)
		{
		}

		/// <summary>
		/// MOV segreg,mem16
		/// </summary>
		public unsafe static void MOV (SegType target, UInt16* source)
		{
		}

		/// <summary>
		/// MOV segreg,mem32
		/// </summary>
		public static void MOV (SegType target, DWordMemory source)
		{
		}

		/// <summary>
		/// MOV segreg,mem32
		/// </summary>
		public unsafe static void MOV (SegType target, UInt32* source)
		{
		}

		/// <summary>
		/// MOV reg32,CR0/2/3/4
		/// </summary>
		public static void MOV (R32Type target, CRType source)
		{
		}

		/// <summary>
		/// MOV reg32,DR0/1/2/3/6/7
		/// </summary>
		public static void MOV (R32Type target, DRType source)
		{
		}

		/// <summary>
		/// MOV reg32,TR3/4/5/6/7
		/// </summary>
		public static void MOV (R32Type target, TRType source)
		{
		}

		/// <summary>
		/// MOV CR0/2/3/4,reg32
		/// </summary>
		public static void MOV (CRType target, R32Type source)
		{
		}

		/// <summary>
		/// MOV DR0/1/2/3/6/7,reg32
		/// </summary>
		public static void MOV (DRType target, R32Type source)
		{
		}

		/// <summary>
		/// MOV TR3/4/5/6/7,reg32
		/// </summary>
		public static void MOV (TRType target, R32Type source)
		{
		}

		/// <summary>
		/// MOV rmreg8,reg8
		/// </summary>
		public static void MOV (R8Type target, R8Type source)
		{
		}

		/// <summary>
		/// MOV rmreg16,reg16
		/// </summary>
		public static void MOV (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// MOV rmreg32,reg32
		/// </summary>
		public static void MOV (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// MOV rmreg16,segreg
		/// </summary>
		public static void MOV (R16Type target, SegType source)
		{
		}

		/// <summary>
		/// MOV rmreg32,segreg
		/// </summary>
		public static void MOV (R32Type target, SegType source)
		{
		}

		/// <summary>
		/// MOV segreg,rmreg16
		/// </summary>
		public static void MOV (SegType target, R16Type source)
		{
		}

		/// <summary>
		/// MOV segreg,rmreg32
		/// </summary>
		public static void MOV (SegType target, R32Type source)
		{
		}

		/// <summary>
		/// MOV 
		/// </summary>
		public static void MOV (R16Type target, string label)
		{
		}

		/// <summary>
		/// MOV 
		/// </summary>
		public static void MOV (R32Type target, string label)
		{
		}

		/// <summary>
		/// MOVSB 
		/// </summary>
		public static void MOVSB ()
		{
		}

		/// <summary>
		/// MOVSD 
		/// </summary>
		public static void MOVSD ()
		{
		}

		/// <summary>
		/// MOVSW 
		/// </summary>
		public static void MOVSW ()
		{
		}

		/// <summary>
		/// MOVSX reg16,mem8
		/// </summary>
		public static void MOVSX (R16Type target, ByteMemory source)
		{
		}

		/// <summary>
		/// MOVSX reg16,mem8
		/// </summary>
		public unsafe static void MOVSX (R16Type target, byte* source)
		{
		}

		/// <summary>
		/// MOVSX reg32,mem8
		/// </summary>
		public static void MOVSX (R32Type target, ByteMemory source)
		{
		}

		/// <summary>
		/// MOVSX reg32,mem8
		/// </summary>
		public unsafe static void MOVSX (R32Type target, byte* source)
		{
		}

		/// <summary>
		/// MOVSX reg32,mem16
		/// </summary>
		public static void MOVSX (R32Type target, WordMemory source)
		{
		}

		/// <summary>
		/// MOVSX reg32,mem16
		/// </summary>
		public unsafe static void MOVSX (R32Type target, UInt16* source)
		{
		}

		/// <summary>
		/// MOVSX reg16,rmreg8
		/// </summary>
		public static void MOVSX (R16Type target, R8Type source)
		{
		}

		/// <summary>
		/// MOVSX reg32,rmreg8
		/// </summary>
		public static void MOVSX (R32Type target, R8Type source)
		{
		}

		/// <summary>
		/// MOVSX reg32,rmreg16
		/// </summary>
		public static void MOVSX (R32Type target, R16Type source)
		{
		}

		/// <summary>
		/// MOVZX reg16,mem8
		/// </summary>
		public static void MOVZX (R16Type target, ByteMemory source)
		{
		}

		/// <summary>
		/// MOVZX reg16,mem8
		/// </summary>
		public unsafe static void MOVZX (R16Type target, byte* source)
		{
		}

		/// <summary>
		/// MOVZX reg32,mem8
		/// </summary>
		public static void MOVZX (R32Type target, ByteMemory source)
		{
		}

		/// <summary>
		/// MOVZX reg32,mem8
		/// </summary>
		public unsafe static void MOVZX (R32Type target, byte* source)
		{
		}

		/// <summary>
		/// MOVZX reg32,mem16
		/// </summary>
		public static void MOVZX (R32Type target, WordMemory source)
		{
		}

		/// <summary>
		/// MOVZX reg32,mem16
		/// </summary>
		public unsafe static void MOVZX (R32Type target, UInt16* source)
		{
		}

		/// <summary>
		/// MOVZX reg16,rmreg8
		/// </summary>
		public static void MOVZX (R16Type target, R8Type source)
		{
		}

		/// <summary>
		/// MOVZX reg32,rmreg8
		/// </summary>
		public static void MOVZX (R32Type target, R8Type source)
		{
		}

		/// <summary>
		/// MOVZX reg32,rmreg16
		/// </summary>
		public static void MOVZX (R32Type target, R16Type source)
		{
		}

		/// <summary>
		/// MUL mem8
		/// </summary>
		public static void MUL (ByteMemory target)
		{
		}

		/// <summary>
		/// MUL mem8
		/// </summary>
		public unsafe static void MUL (byte* target)
		{
		}

		/// <summary>
		/// MUL mem16
		/// </summary>
		public static void MUL (WordMemory target)
		{
		}

		/// <summary>
		/// MUL mem16
		/// </summary>
		public unsafe static void MUL (UInt16* target)
		{
		}

		/// <summary>
		/// MUL mem32
		/// </summary>
		public static void MUL (DWordMemory target)
		{
		}

		/// <summary>
		/// MUL mem32
		/// </summary>
		public unsafe static void MUL (UInt32* target)
		{
		}

		/// <summary>
		/// MUL rmreg8
		/// </summary>
		public static void MUL (R8Type target)
		{
		}

		/// <summary>
		/// MUL rmreg16
		/// </summary>
		public static void MUL (R16Type target)
		{
		}

		/// <summary>
		/// MUL rmreg32
		/// </summary>
		public static void MUL (R32Type target)
		{
		}

		/// <summary>
		/// NEG mem8
		/// </summary>
		public static void NEG (ByteMemory target)
		{
		}

		/// <summary>
		/// NEG mem8
		/// </summary>
		public unsafe static void NEG (byte* target)
		{
		}

		/// <summary>
		/// NEG mem16
		/// </summary>
		public static void NEG (WordMemory target)
		{
		}

		/// <summary>
		/// NEG mem16
		/// </summary>
		public unsafe static void NEG (UInt16* target)
		{
		}

		/// <summary>
		/// NEG mem32
		/// </summary>
		public static void NEG (DWordMemory target)
		{
		}

		/// <summary>
		/// NEG mem32
		/// </summary>
		public unsafe static void NEG (UInt32* target)
		{
		}

		/// <summary>
		/// NEG rmreg8
		/// </summary>
		public static void NEG (R8Type target)
		{
		}

		/// <summary>
		/// NEG rmreg16
		/// </summary>
		public static void NEG (R16Type target)
		{
		}

		/// <summary>
		/// NEG rmreg32
		/// </summary>
		public static void NEG (R32Type target)
		{
		}

		/// <summary>
		/// NOP 
		/// </summary>
		public static void NOP ()
		{
		}

		/// <summary>
		/// NOT mem8
		/// </summary>
		public static void NOT (ByteMemory target)
		{
		}

		/// <summary>
		/// NOT mem8
		/// </summary>
		public unsafe static void NOT (byte* target)
		{
		}

		/// <summary>
		/// NOT mem16
		/// </summary>
		public static void NOT (WordMemory target)
		{
		}

		/// <summary>
		/// NOT mem16
		/// </summary>
		public unsafe static void NOT (UInt16* target)
		{
		}

		/// <summary>
		/// NOT mem32
		/// </summary>
		public static void NOT (DWordMemory target)
		{
		}

		/// <summary>
		/// NOT mem32
		/// </summary>
		public unsafe static void NOT (UInt32* target)
		{
		}

		/// <summary>
		/// NOT rmreg8
		/// </summary>
		public static void NOT (R8Type target)
		{
		}

		/// <summary>
		/// NOT rmreg16
		/// </summary>
		public static void NOT (R16Type target)
		{
		}

		/// <summary>
		/// NOT rmreg32
		/// </summary>
		public static void NOT (R32Type target)
		{
		}

		/// <summary>
		/// OFFSET 
		/// </summary>
		public static void OFFSET (UInt32 value)
		{
		}

		/// <summary>
		/// OR mem8,reg8
		/// </summary>
		public static void OR (ByteMemory target, R8Type source)
		{
		}

		/// <summary>
		/// OR mem8,reg8
		/// </summary>
		public unsafe static void OR (byte* target, R8Type source)
		{
		}

		/// <summary>
		/// OR mem16,reg16
		/// </summary>
		public static void OR (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// OR mem16,reg16
		/// </summary>
		public unsafe static void OR (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// OR mem32,reg32
		/// </summary>
		public static void OR (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// OR mem32,reg32
		/// </summary>
		public unsafe static void OR (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// OR reg8,mem8
		/// </summary>
		public static void OR (R8Type target, ByteMemory source)
		{
		}

		/// <summary>
		/// OR reg8,mem8
		/// </summary>
		public unsafe static void OR (R8Type target, byte* source)
		{
		}

		/// <summary>
		/// OR reg16,mem16
		/// </summary>
		public static void OR (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// OR reg16,mem16
		/// </summary>
		public unsafe static void OR (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// OR reg32,mem32
		/// </summary>
		public static void OR (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// OR reg32,mem32
		/// </summary>
		public unsafe static void OR (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// OR mem8,imm8
		/// </summary>
		public static void OR (ByteMemory target, Byte source)
		{
		}

		/// <summary>
		/// OR mem8,imm8
		/// </summary>
		public unsafe static void OR (byte* target, Byte source)
		{
		}

		/// <summary>
		/// OR mem16,imm16
		/// </summary>
		public static void OR (WordMemory target, UInt16 source)
		{
		}

		/// <summary>
		/// OR mem16,imm16
		/// </summary>
		public unsafe static void OR (UInt16* target, UInt16 source)
		{
		}

		/// <summary>
		/// OR mem32,imm32
		/// </summary>
		public static void OR (DWordMemory target, UInt32 source)
		{
		}

		/// <summary>
		/// OR mem32,imm32
		/// </summary>
		public unsafe static void OR (UInt32* target, UInt32 source)
		{
		}

		/// <summary>
		/// OR mem16,imm8
		/// </summary>
		public static void OR (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// OR mem16,imm8
		/// </summary>
		public unsafe static void OR (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// OR mem32,imm8
		/// </summary>
		public static void OR (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// OR mem32,imm8
		/// </summary>
		public unsafe static void OR (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// OR rmreg8,reg8
		/// </summary>
		public static void OR (R8Type target, R8Type source)
		{
		}

		/// <summary>
		/// OR rmreg16,reg16
		/// </summary>
		public static void OR (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// OR rmreg32,reg32
		/// </summary>
		public static void OR (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// OR rmreg8,imm8
		/// </summary>
		public static void OR (R8Type target, Byte source)
		{
		}

		/// <summary>
		/// OR rmreg16,imm16
		/// </summary>
		public static void OR (R16Type target, UInt16 source)
		{
		}

		/// <summary>
		/// OR rmreg32,imm32
		/// </summary>
		public static void OR (R32Type target, UInt32 source)
		{
		}

		/// <summary>
		/// OR rmreg16,imm8
		/// </summary>
		public static void OR (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// OR rmreg32,imm8
		/// </summary>
		public static void OR (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// ORG 
		/// </summary>
		public static void ORG (UInt32 value)
		{
		}

		/// <summary>
		/// OUT imm8,AL
		/// </summary>
		public static void OUT__AL (Byte target)
		{
		}

		/// <summary>
		/// OUT imm8,AX
		/// </summary>
		public static void OUT__AX (Byte target)
		{
		}

		/// <summary>
		/// OUT imm8,EAX
		/// </summary>
		public static void OUT__EAX (Byte target)
		{
		}

		/// <summary>
		/// OUT DX,AL
		/// </summary>
		public static void OUT_DX__AL ()
		{
		}

		/// <summary>
		/// OUT DX,AX
		/// </summary>
		public static void OUT_DX__AX ()
		{
		}

		/// <summary>
		/// OUT DX,EAX
		/// </summary>
		public static void OUT_DX__EAX ()
		{
		}

		/// <summary>
		/// OUTSB 
		/// </summary>
		public static void OUTSB ()
		{
		}

		/// <summary>
		/// OUTSD 
		/// </summary>
		public static void OUTSD ()
		{
		}

		/// <summary>
		/// OUTSW 
		/// </summary>
		public static void OUTSW ()
		{
		}

		/// <summary>
		/// PAUSE 
		/// </summary>
		public static void PAUSE ()
		{
		}

		/// <summary>
		/// POP reg16
		/// </summary>
		public static void POP (R16Type target)
		{
		}

		/// <summary>
		/// POP reg32
		/// </summary>
		public static void POP (R32Type target)
		{
		}

		/// <summary>
		/// POP mem16
		/// </summary>
		public static void POP (WordMemory target)
		{
		}

		/// <summary>
		/// POP mem16
		/// </summary>
		public unsafe static void POP (UInt16* target)
		{
		}

		/// <summary>
		/// POP mem32
		/// </summary>
		public static void POP (DWordMemory target)
		{
		}

		/// <summary>
		/// POP mem32
		/// </summary>
		public unsafe static void POP (UInt32* target)
		{
		}

		/// <summary>
		/// POP segreg
		/// </summary>
		public static void POP (SegType target)
		{
		}

		/// <summary>
		/// POPA 
		/// </summary>
		public static void POPA ()
		{
		}

		/// <summary>
		/// POPAD 
		/// </summary>
		public static void POPAD ()
		{
		}

		/// <summary>
		/// POPAW 
		/// </summary>
		public static void POPAW ()
		{
		}

		/// <summary>
		/// POPF 
		/// </summary>
		public static void POPF ()
		{
		}

		/// <summary>
		/// POPFD 
		/// </summary>
		public static void POPFD ()
		{
		}

		/// <summary>
		/// POPFW 
		/// </summary>
		public static void POPFW ()
		{
		}

		/// <summary>
		/// PREFETCHNTA m8
		/// </summary>
		public static void PREFETCHNTA (Memory target)
		{
		}

		/// <summary>
		/// PREFETCHNTA m8
		/// </summary>
		public unsafe static void PREFETCHNTA (byte* target)
		{
		}

		/// <summary>
		/// PREFETCHT0 m8
		/// </summary>
		public static void PREFETCHT0 (Memory target)
		{
		}

		/// <summary>
		/// PREFETCHT0 m8
		/// </summary>
		public unsafe static void PREFETCHT0 (byte* target)
		{
		}

		/// <summary>
		/// PREFETCHT1 m8
		/// </summary>
		public static void PREFETCHT1 (Memory target)
		{
		}

		/// <summary>
		/// PREFETCHT1 m8
		/// </summary>
		public unsafe static void PREFETCHT1 (byte* target)
		{
		}

		/// <summary>
		/// PREFETCHT2 m8
		/// </summary>
		public static void PREFETCHT2 (Memory target)
		{
		}

		/// <summary>
		/// PREFETCHT2 m8
		/// </summary>
		public unsafe static void PREFETCHT2 (byte* target)
		{
		}

		/// <summary>
		/// PUSH reg16
		/// </summary>
		public static void PUSH (R16Type target)
		{
		}

		/// <summary>
		/// PUSH reg32
		/// </summary>
		public static void PUSH (R32Type target)
		{
		}

		/// <summary>
		/// PUSH mem16
		/// </summary>
		public static void PUSH (WordMemory target)
		{
		}

		/// <summary>
		/// PUSH mem16
		/// </summary>
		public unsafe static void PUSH (UInt16* target)
		{
		}

		/// <summary>
		/// PUSH mem32
		/// </summary>
		public static void PUSH (DWordMemory target)
		{
		}

		/// <summary>
		/// PUSH mem32
		/// </summary>
		public unsafe static void PUSH (UInt32* target)
		{
		}

		/// <summary>
		/// PUSH imm8
		/// </summary>
		public static void PUSH (Byte target)
		{
		}

		/// <summary>
		/// PUSH imm16
		/// </summary>
		public static void PUSH (UInt16 target)
		{
		}

		/// <summary>
		/// PUSH imm32
		/// </summary>
		public static void PUSH (UInt32 target)
		{
		}

		/// <summary>
		/// PUSH segreg
		/// </summary>
		public static void PUSH (SegType target)
		{
		}

		/// <summary>
		/// PUSHA 
		/// </summary>
		public static void PUSHA ()
		{
		}

		/// <summary>
		/// PUSHAD 
		/// </summary>
		public static void PUSHAD ()
		{
		}

		/// <summary>
		/// PUSHAW 
		/// </summary>
		public static void PUSHAW ()
		{
		}

		/// <summary>
		/// PUSHF 
		/// </summary>
		public static void PUSHF ()
		{
		}

		/// <summary>
		/// PUSHFD 
		/// </summary>
		public static void PUSHFD ()
		{
		}

		/// <summary>
		/// PUSHFW 
		/// </summary>
		public static void PUSHFW ()
		{
		}

		/// <summary>
		/// RCL mem8,CL
		/// </summary>
		public static void RCL__CL (ByteMemory target)
		{
		}

		/// <summary>
		/// RCL mem8,CL
		/// </summary>
		public unsafe static void RCL__CL (byte* target)
		{
		}

		/// <summary>
		/// RCL mem8,imm8
		/// </summary>
		public static void RCL (ByteMemory target, Byte source)
		{
		}

		/// <summary>
		/// RCL mem8,imm8
		/// </summary>
		public unsafe static void RCL (byte* target, Byte source)
		{
		}

		/// <summary>
		/// RCL mem16,CL
		/// </summary>
		public static void RCL__CL (WordMemory target)
		{
		}

		/// <summary>
		/// RCL mem16,CL
		/// </summary>
		public unsafe static void RCL__CL (UInt16* target)
		{
		}

		/// <summary>
		/// RCL mem16,imm8
		/// </summary>
		public static void RCL (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// RCL mem16,imm8
		/// </summary>
		public unsafe static void RCL (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// RCL mem32,CL
		/// </summary>
		public static void RCL__CL (DWordMemory target)
		{
		}

		/// <summary>
		/// RCL mem32,CL
		/// </summary>
		public unsafe static void RCL__CL (UInt32* target)
		{
		}

		/// <summary>
		/// RCL mem32,imm8
		/// </summary>
		public static void RCL (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// RCL mem32,imm8
		/// </summary>
		public unsafe static void RCL (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// RCL rmreg8,CL
		/// </summary>
		public static void RCL__CL (R8Type target)
		{
		}

		/// <summary>
		/// RCL rmreg8,imm8
		/// </summary>
		public static void RCL (R8Type target, Byte source)
		{
		}

		/// <summary>
		/// RCL rmreg16,CL
		/// </summary>
		public static void RCL__CL (R16Type target)
		{
		}

		/// <summary>
		/// RCL rmreg16,imm8
		/// </summary>
		public static void RCL (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// RCL rmreg32,CL
		/// </summary>
		public static void RCL__CL (R32Type target)
		{
		}

		/// <summary>
		/// RCL rmreg32,imm8
		/// </summary>
		public static void RCL (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// RCR mem8,CL
		/// </summary>
		public static void RCR__CL (ByteMemory target)
		{
		}

		/// <summary>
		/// RCR mem8,CL
		/// </summary>
		public unsafe static void RCR__CL (byte* target)
		{
		}

		/// <summary>
		/// RCR mem8,imm8
		/// </summary>
		public static void RCR (ByteMemory target, Byte source)
		{
		}

		/// <summary>
		/// RCR mem8,imm8
		/// </summary>
		public unsafe static void RCR (byte* target, Byte source)
		{
		}

		/// <summary>
		/// RCR mem16,CL
		/// </summary>
		public static void RCR__CL (WordMemory target)
		{
		}

		/// <summary>
		/// RCR mem16,CL
		/// </summary>
		public unsafe static void RCR__CL (UInt16* target)
		{
		}

		/// <summary>
		/// RCR mem16,imm8
		/// </summary>
		public static void RCR (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// RCR mem16,imm8
		/// </summary>
		public unsafe static void RCR (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// RCR mem32,CL
		/// </summary>
		public static void RCR__CL (DWordMemory target)
		{
		}

		/// <summary>
		/// RCR mem32,CL
		/// </summary>
		public unsafe static void RCR__CL (UInt32* target)
		{
		}

		/// <summary>
		/// RCR mem32,imm8
		/// </summary>
		public static void RCR (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// RCR mem32,imm8
		/// </summary>
		public unsafe static void RCR (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// RCR rmreg8,CL
		/// </summary>
		public static void RCR__CL (R8Type target)
		{
		}

		/// <summary>
		/// RCR rmreg8,imm8
		/// </summary>
		public static void RCR (R8Type target, Byte source)
		{
		}

		/// <summary>
		/// RCR rmreg16,CL
		/// </summary>
		public static void RCR__CL (R16Type target)
		{
		}

		/// <summary>
		/// RCR rmreg16,imm8
		/// </summary>
		public static void RCR (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// RCR rmreg32,CL
		/// </summary>
		public static void RCR__CL (R32Type target)
		{
		}

		/// <summary>
		/// RCR rmreg32,imm8
		/// </summary>
		public static void RCR (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// RDMSR 
		/// </summary>
		public static void RDMSR ()
		{
		}

		/// <summary>
		/// RDPMC 
		/// </summary>
		public static void RDPMC ()
		{
		}

		/// <summary>
		/// RDTSC 
		/// </summary>
		public static void RDTSC ()
		{
		}

		/// <summary>
		/// REP 
		/// </summary>
		public static void REP ()
		{
		}

		/// <summary>
		/// REPE 
		/// </summary>
		public static void REPE ()
		{
		}

		/// <summary>
		/// REPNE 
		/// </summary>
		public static void REPNE ()
		{
		}

		/// <summary>
		/// REPNZ 
		/// </summary>
		public static void REPNZ ()
		{
		}

		/// <summary>
		/// REPZ 
		/// </summary>
		public static void REPZ ()
		{
		}

		/// <summary>
		/// RET 
		/// </summary>
		public static void RET ()
		{
		}

		/// <summary>
		/// RET imm16
		/// </summary>
		public static void RET (UInt16 target)
		{
		}

		/// <summary>
		/// RETF 
		/// </summary>
		public static void RETF ()
		{
		}

		/// <summary>
		/// RETF imm16
		/// </summary>
		public static void RETF (UInt16 target)
		{
		}

		/// <summary>
		/// RETN 
		/// </summary>
		public static void RETN ()
		{
		}

		/// <summary>
		/// RETN imm16
		/// </summary>
		public static void RETN (UInt16 target)
		{
		}

		/// <summary>
		/// ROL mem8,CL
		/// </summary>
		public static void ROL__CL (ByteMemory target)
		{
		}

		/// <summary>
		/// ROL mem8,CL
		/// </summary>
		public unsafe static void ROL__CL (byte* target)
		{
		}

		/// <summary>
		/// ROL mem8,imm8
		/// </summary>
		public static void ROL (ByteMemory target, Byte source)
		{
		}

		/// <summary>
		/// ROL mem8,imm8
		/// </summary>
		public unsafe static void ROL (byte* target, Byte source)
		{
		}

		/// <summary>
		/// ROL mem16,CL
		/// </summary>
		public static void ROL__CL (WordMemory target)
		{
		}

		/// <summary>
		/// ROL mem16,CL
		/// </summary>
		public unsafe static void ROL__CL (UInt16* target)
		{
		}

		/// <summary>
		/// ROL mem16,imm8
		/// </summary>
		public static void ROL (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// ROL mem16,imm8
		/// </summary>
		public unsafe static void ROL (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// ROL mem32,CL
		/// </summary>
		public static void ROL__CL (DWordMemory target)
		{
		}

		/// <summary>
		/// ROL mem32,CL
		/// </summary>
		public unsafe static void ROL__CL (UInt32* target)
		{
		}

		/// <summary>
		/// ROL mem32,imm8
		/// </summary>
		public static void ROL (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// ROL mem32,imm8
		/// </summary>
		public unsafe static void ROL (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// ROL rmreg8,CL
		/// </summary>
		public static void ROL__CL (R8Type target)
		{
		}

		/// <summary>
		/// ROL rmreg8,imm8
		/// </summary>
		public static void ROL (R8Type target, Byte source)
		{
		}

		/// <summary>
		/// ROL rmreg16,CL
		/// </summary>
		public static void ROL__CL (R16Type target)
		{
		}

		/// <summary>
		/// ROL rmreg16,imm8
		/// </summary>
		public static void ROL (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// ROL rmreg32,CL
		/// </summary>
		public static void ROL__CL (R32Type target)
		{
		}

		/// <summary>
		/// ROL rmreg32,imm8
		/// </summary>
		public static void ROL (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// ROR mem8,CL
		/// </summary>
		public static void ROR__CL (ByteMemory target)
		{
		}

		/// <summary>
		/// ROR mem8,CL
		/// </summary>
		public unsafe static void ROR__CL (byte* target)
		{
		}

		/// <summary>
		/// ROR mem8,imm8
		/// </summary>
		public static void ROR (ByteMemory target, Byte source)
		{
		}

		/// <summary>
		/// ROR mem8,imm8
		/// </summary>
		public unsafe static void ROR (byte* target, Byte source)
		{
		}

		/// <summary>
		/// ROR mem16,CL
		/// </summary>
		public static void ROR__CL (WordMemory target)
		{
		}

		/// <summary>
		/// ROR mem16,CL
		/// </summary>
		public unsafe static void ROR__CL (UInt16* target)
		{
		}

		/// <summary>
		/// ROR mem16,imm8
		/// </summary>
		public static void ROR (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// ROR mem16,imm8
		/// </summary>
		public unsafe static void ROR (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// ROR mem32,CL
		/// </summary>
		public static void ROR__CL (DWordMemory target)
		{
		}

		/// <summary>
		/// ROR mem32,CL
		/// </summary>
		public unsafe static void ROR__CL (UInt32* target)
		{
		}

		/// <summary>
		/// ROR mem32,imm8
		/// </summary>
		public static void ROR (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// ROR mem32,imm8
		/// </summary>
		public unsafe static void ROR (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// ROR rmreg8,CL
		/// </summary>
		public static void ROR__CL (R8Type target)
		{
		}

		/// <summary>
		/// ROR rmreg8,imm8
		/// </summary>
		public static void ROR (R8Type target, Byte source)
		{
		}

		/// <summary>
		/// ROR rmreg16,CL
		/// </summary>
		public static void ROR__CL (R16Type target)
		{
		}

		/// <summary>
		/// ROR rmreg16,imm8
		/// </summary>
		public static void ROR (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// ROR rmreg32,CL
		/// </summary>
		public static void ROR__CL (R32Type target)
		{
		}

		/// <summary>
		/// ROR rmreg32,imm8
		/// </summary>
		public static void ROR (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// RSM 
		/// </summary>
		public static void RSM ()
		{
		}

		/// <summary>
		/// SAHF 
		/// </summary>
		public static void SAHF ()
		{
		}

		/// <summary>
		/// SAL mem8,CL
		/// </summary>
		public static void SAL__CL (ByteMemory target)
		{
		}

		/// <summary>
		/// SAL mem8,CL
		/// </summary>
		public unsafe static void SAL__CL (byte* target)
		{
		}

		/// <summary>
		/// SAL mem8,imm8
		/// </summary>
		public static void SAL (ByteMemory target, Byte source)
		{
		}

		/// <summary>
		/// SAL mem8,imm8
		/// </summary>
		public unsafe static void SAL (byte* target, Byte source)
		{
		}

		/// <summary>
		/// SAL mem16,CL
		/// </summary>
		public static void SAL__CL (WordMemory target)
		{
		}

		/// <summary>
		/// SAL mem16,CL
		/// </summary>
		public unsafe static void SAL__CL (UInt16* target)
		{
		}

		/// <summary>
		/// SAL mem16,imm8
		/// </summary>
		public static void SAL (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// SAL mem16,imm8
		/// </summary>
		public unsafe static void SAL (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// SAL mem32,CL
		/// </summary>
		public static void SAL__CL (DWordMemory target)
		{
		}

		/// <summary>
		/// SAL mem32,CL
		/// </summary>
		public unsafe static void SAL__CL (UInt32* target)
		{
		}

		/// <summary>
		/// SAL mem32,imm8
		/// </summary>
		public static void SAL (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// SAL mem32,imm8
		/// </summary>
		public unsafe static void SAL (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// SAL rmreg8,CL
		/// </summary>
		public static void SAL__CL (R8Type target)
		{
		}

		/// <summary>
		/// SAL rmreg8,imm8
		/// </summary>
		public static void SAL (R8Type target, Byte source)
		{
		}

		/// <summary>
		/// SAL rmreg16,CL
		/// </summary>
		public static void SAL__CL (R16Type target)
		{
		}

		/// <summary>
		/// SAL rmreg16,imm8
		/// </summary>
		public static void SAL (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// SAL rmreg32,CL
		/// </summary>
		public static void SAL__CL (R32Type target)
		{
		}

		/// <summary>
		/// SAL rmreg32,imm8
		/// </summary>
		public static void SAL (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// SALC 
		/// </summary>
		public static void SALC ()
		{
		}

		/// <summary>
		/// SAR mem8,CL
		/// </summary>
		public static void SAR__CL (ByteMemory target)
		{
		}

		/// <summary>
		/// SAR mem8,CL
		/// </summary>
		public unsafe static void SAR__CL (byte* target)
		{
		}

		/// <summary>
		/// SAR mem8,imm8
		/// </summary>
		public static void SAR (ByteMemory target, Byte source)
		{
		}

		/// <summary>
		/// SAR mem8,imm8
		/// </summary>
		public unsafe static void SAR (byte* target, Byte source)
		{
		}

		/// <summary>
		/// SAR mem16,CL
		/// </summary>
		public static void SAR__CL (WordMemory target)
		{
		}

		/// <summary>
		/// SAR mem16,CL
		/// </summary>
		public unsafe static void SAR__CL (UInt16* target)
		{
		}

		/// <summary>
		/// SAR mem16,imm8
		/// </summary>
		public static void SAR (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// SAR mem16,imm8
		/// </summary>
		public unsafe static void SAR (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// SAR mem32,CL
		/// </summary>
		public static void SAR__CL (DWordMemory target)
		{
		}

		/// <summary>
		/// SAR mem32,CL
		/// </summary>
		public unsafe static void SAR__CL (UInt32* target)
		{
		}

		/// <summary>
		/// SAR mem32,imm8
		/// </summary>
		public static void SAR (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// SAR mem32,imm8
		/// </summary>
		public unsafe static void SAR (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// SAR rmreg8,CL
		/// </summary>
		public static void SAR__CL (R8Type target)
		{
		}

		/// <summary>
		/// SAR rmreg8,imm8
		/// </summary>
		public static void SAR (R8Type target, Byte source)
		{
		}

		/// <summary>
		/// SAR rmreg16,CL
		/// </summary>
		public static void SAR__CL (R16Type target)
		{
		}

		/// <summary>
		/// SAR rmreg16,imm8
		/// </summary>
		public static void SAR (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// SAR rmreg32,CL
		/// </summary>
		public static void SAR__CL (R32Type target)
		{
		}

		/// <summary>
		/// SAR rmreg32,imm8
		/// </summary>
		public static void SAR (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// SBB mem8,reg8
		/// </summary>
		public static void SBB (ByteMemory target, R8Type source)
		{
		}

		/// <summary>
		/// SBB mem8,reg8
		/// </summary>
		public unsafe static void SBB (byte* target, R8Type source)
		{
		}

		/// <summary>
		/// SBB mem16,reg16
		/// </summary>
		public static void SBB (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// SBB mem16,reg16
		/// </summary>
		public unsafe static void SBB (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// SBB mem32,reg32
		/// </summary>
		public static void SBB (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// SBB mem32,reg32
		/// </summary>
		public unsafe static void SBB (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// SBB reg8,mem8
		/// </summary>
		public static void SBB (R8Type target, ByteMemory source)
		{
		}

		/// <summary>
		/// SBB reg8,mem8
		/// </summary>
		public unsafe static void SBB (R8Type target, byte* source)
		{
		}

		/// <summary>
		/// SBB reg16,mem16
		/// </summary>
		public static void SBB (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// SBB reg16,mem16
		/// </summary>
		public unsafe static void SBB (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// SBB reg32,mem32
		/// </summary>
		public static void SBB (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// SBB reg32,mem32
		/// </summary>
		public unsafe static void SBB (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// SBB mem8,imm8
		/// </summary>
		public static void SBB (ByteMemory target, Byte source)
		{
		}

		/// <summary>
		/// SBB mem8,imm8
		/// </summary>
		public unsafe static void SBB (byte* target, Byte source)
		{
		}

		/// <summary>
		/// SBB mem16,imm16
		/// </summary>
		public static void SBB (WordMemory target, UInt16 source)
		{
		}

		/// <summary>
		/// SBB mem16,imm16
		/// </summary>
		public unsafe static void SBB (UInt16* target, UInt16 source)
		{
		}

		/// <summary>
		/// SBB mem32,imm32
		/// </summary>
		public static void SBB (DWordMemory target, UInt32 source)
		{
		}

		/// <summary>
		/// SBB mem32,imm32
		/// </summary>
		public unsafe static void SBB (UInt32* target, UInt32 source)
		{
		}

		/// <summary>
		/// SBB mem16,imm8
		/// </summary>
		public static void SBB (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// SBB mem16,imm8
		/// </summary>
		public unsafe static void SBB (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// SBB mem32,imm8
		/// </summary>
		public static void SBB (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// SBB mem32,imm8
		/// </summary>
		public unsafe static void SBB (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// SBB rmreg8,reg8
		/// </summary>
		public static void SBB (R8Type target, R8Type source)
		{
		}

		/// <summary>
		/// SBB rmreg16,reg16
		/// </summary>
		public static void SBB (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// SBB rmreg32,reg32
		/// </summary>
		public static void SBB (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// SBB rmreg8,imm8
		/// </summary>
		public static void SBB (R8Type target, Byte source)
		{
		}

		/// <summary>
		/// SBB rmreg16,imm16
		/// </summary>
		public static void SBB (R16Type target, UInt16 source)
		{
		}

		/// <summary>
		/// SBB rmreg32,imm32
		/// </summary>
		public static void SBB (R32Type target, UInt32 source)
		{
		}

		/// <summary>
		/// SBB rmreg16,imm8
		/// </summary>
		public static void SBB (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// SBB rmreg32,imm8
		/// </summary>
		public static void SBB (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// SCASB 
		/// </summary>
		public static void SCASB ()
		{
		}

		/// <summary>
		/// SCASD 
		/// </summary>
		public static void SCASD ()
		{
		}

		/// <summary>
		/// SCASW 
		/// </summary>
		public static void SCASW ()
		{
		}

		/// <summary>
		/// SETA mem8
		/// </summary>
		public static void SETA (ByteMemory target)
		{
		}

		/// <summary>
		/// SETA mem8
		/// </summary>
		public unsafe static void SETA (byte* target)
		{
		}

		/// <summary>
		/// SETA rmreg8
		/// </summary>
		public static void SETA (R8Type target)
		{
		}

		/// <summary>
		/// SETAE mem8
		/// </summary>
		public static void SETAE (ByteMemory target)
		{
		}

		/// <summary>
		/// SETAE mem8
		/// </summary>
		public unsafe static void SETAE (byte* target)
		{
		}

		/// <summary>
		/// SETAE rmreg8
		/// </summary>
		public static void SETAE (R8Type target)
		{
		}

		/// <summary>
		/// SETB mem8
		/// </summary>
		public static void SETB (ByteMemory target)
		{
		}

		/// <summary>
		/// SETB mem8
		/// </summary>
		public unsafe static void SETB (byte* target)
		{
		}

		/// <summary>
		/// SETB rmreg8
		/// </summary>
		public static void SETB (R8Type target)
		{
		}

		/// <summary>
		/// SETBE mem8
		/// </summary>
		public static void SETBE (ByteMemory target)
		{
		}

		/// <summary>
		/// SETBE mem8
		/// </summary>
		public unsafe static void SETBE (byte* target)
		{
		}

		/// <summary>
		/// SETBE rmreg8
		/// </summary>
		public static void SETBE (R8Type target)
		{
		}

		/// <summary>
		/// SETC mem8
		/// </summary>
		public static void SETC (ByteMemory target)
		{
		}

		/// <summary>
		/// SETC mem8
		/// </summary>
		public unsafe static void SETC (byte* target)
		{
		}

		/// <summary>
		/// SETC rmreg8
		/// </summary>
		public static void SETC (R8Type target)
		{
		}

		/// <summary>
		/// SETE mem8
		/// </summary>
		public static void SETE (ByteMemory target)
		{
		}

		/// <summary>
		/// SETE mem8
		/// </summary>
		public unsafe static void SETE (byte* target)
		{
		}

		/// <summary>
		/// SETE rmreg8
		/// </summary>
		public static void SETE (R8Type target)
		{
		}

		/// <summary>
		/// SETG mem8
		/// </summary>
		public static void SETG (ByteMemory target)
		{
		}

		/// <summary>
		/// SETG mem8
		/// </summary>
		public unsafe static void SETG (byte* target)
		{
		}

		/// <summary>
		/// SETG rmreg8
		/// </summary>
		public static void SETG (R8Type target)
		{
		}

		/// <summary>
		/// SETGE mem8
		/// </summary>
		public static void SETGE (ByteMemory target)
		{
		}

		/// <summary>
		/// SETGE mem8
		/// </summary>
		public unsafe static void SETGE (byte* target)
		{
		}

		/// <summary>
		/// SETGE rmreg8
		/// </summary>
		public static void SETGE (R8Type target)
		{
		}

		/// <summary>
		/// SETL mem8
		/// </summary>
		public static void SETL (ByteMemory target)
		{
		}

		/// <summary>
		/// SETL mem8
		/// </summary>
		public unsafe static void SETL (byte* target)
		{
		}

		/// <summary>
		/// SETL rmreg8
		/// </summary>
		public static void SETL (R8Type target)
		{
		}

		/// <summary>
		/// SETLE mem8
		/// </summary>
		public static void SETLE (ByteMemory target)
		{
		}

		/// <summary>
		/// SETLE mem8
		/// </summary>
		public unsafe static void SETLE (byte* target)
		{
		}

		/// <summary>
		/// SETLE rmreg8
		/// </summary>
		public static void SETLE (R8Type target)
		{
		}

		/// <summary>
		/// SETNA mem8
		/// </summary>
		public static void SETNA (ByteMemory target)
		{
		}

		/// <summary>
		/// SETNA mem8
		/// </summary>
		public unsafe static void SETNA (byte* target)
		{
		}

		/// <summary>
		/// SETNA rmreg8
		/// </summary>
		public static void SETNA (R8Type target)
		{
		}

		/// <summary>
		/// SETNAE mem8
		/// </summary>
		public static void SETNAE (ByteMemory target)
		{
		}

		/// <summary>
		/// SETNAE mem8
		/// </summary>
		public unsafe static void SETNAE (byte* target)
		{
		}

		/// <summary>
		/// SETNAE rmreg8
		/// </summary>
		public static void SETNAE (R8Type target)
		{
		}

		/// <summary>
		/// SETNB mem8
		/// </summary>
		public static void SETNB (ByteMemory target)
		{
		}

		/// <summary>
		/// SETNB mem8
		/// </summary>
		public unsafe static void SETNB (byte* target)
		{
		}

		/// <summary>
		/// SETNB rmreg8
		/// </summary>
		public static void SETNB (R8Type target)
		{
		}

		/// <summary>
		/// SETNBE mem8
		/// </summary>
		public static void SETNBE (ByteMemory target)
		{
		}

		/// <summary>
		/// SETNBE mem8
		/// </summary>
		public unsafe static void SETNBE (byte* target)
		{
		}

		/// <summary>
		/// SETNBE rmreg8
		/// </summary>
		public static void SETNBE (R8Type target)
		{
		}

		/// <summary>
		/// SETNC mem8
		/// </summary>
		public static void SETNC (ByteMemory target)
		{
		}

		/// <summary>
		/// SETNC mem8
		/// </summary>
		public unsafe static void SETNC (byte* target)
		{
		}

		/// <summary>
		/// SETNC rmreg8
		/// </summary>
		public static void SETNC (R8Type target)
		{
		}

		/// <summary>
		/// SETNE mem8
		/// </summary>
		public static void SETNE (ByteMemory target)
		{
		}

		/// <summary>
		/// SETNE mem8
		/// </summary>
		public unsafe static void SETNE (byte* target)
		{
		}

		/// <summary>
		/// SETNE rmreg8
		/// </summary>
		public static void SETNE (R8Type target)
		{
		}

		/// <summary>
		/// SETNG mem8
		/// </summary>
		public static void SETNG (ByteMemory target)
		{
		}

		/// <summary>
		/// SETNG mem8
		/// </summary>
		public unsafe static void SETNG (byte* target)
		{
		}

		/// <summary>
		/// SETNG rmreg8
		/// </summary>
		public static void SETNG (R8Type target)
		{
		}

		/// <summary>
		/// SETNGE mem8
		/// </summary>
		public static void SETNGE (ByteMemory target)
		{
		}

		/// <summary>
		/// SETNGE mem8
		/// </summary>
		public unsafe static void SETNGE (byte* target)
		{
		}

		/// <summary>
		/// SETNGE rmreg8
		/// </summary>
		public static void SETNGE (R8Type target)
		{
		}

		/// <summary>
		/// SETNL mem8
		/// </summary>
		public static void SETNL (ByteMemory target)
		{
		}

		/// <summary>
		/// SETNL mem8
		/// </summary>
		public unsafe static void SETNL (byte* target)
		{
		}

		/// <summary>
		/// SETNL rmreg8
		/// </summary>
		public static void SETNL (R8Type target)
		{
		}

		/// <summary>
		/// SETNLE mem8
		/// </summary>
		public static void SETNLE (ByteMemory target)
		{
		}

		/// <summary>
		/// SETNLE mem8
		/// </summary>
		public unsafe static void SETNLE (byte* target)
		{
		}

		/// <summary>
		/// SETNLE rmreg8
		/// </summary>
		public static void SETNLE (R8Type target)
		{
		}

		/// <summary>
		/// SETNO mem8
		/// </summary>
		public static void SETNO (ByteMemory target)
		{
		}

		/// <summary>
		/// SETNO mem8
		/// </summary>
		public unsafe static void SETNO (byte* target)
		{
		}

		/// <summary>
		/// SETNO rmreg8
		/// </summary>
		public static void SETNO (R8Type target)
		{
		}

		/// <summary>
		/// SETNP mem8
		/// </summary>
		public static void SETNP (ByteMemory target)
		{
		}

		/// <summary>
		/// SETNP mem8
		/// </summary>
		public unsafe static void SETNP (byte* target)
		{
		}

		/// <summary>
		/// SETNP rmreg8
		/// </summary>
		public static void SETNP (R8Type target)
		{
		}

		/// <summary>
		/// SETNS mem8
		/// </summary>
		public static void SETNS (ByteMemory target)
		{
		}

		/// <summary>
		/// SETNS mem8
		/// </summary>
		public unsafe static void SETNS (byte* target)
		{
		}

		/// <summary>
		/// SETNS rmreg8
		/// </summary>
		public static void SETNS (R8Type target)
		{
		}

		/// <summary>
		/// SETNZ mem8
		/// </summary>
		public static void SETNZ (ByteMemory target)
		{
		}

		/// <summary>
		/// SETNZ mem8
		/// </summary>
		public unsafe static void SETNZ (byte* target)
		{
		}

		/// <summary>
		/// SETNZ rmreg8
		/// </summary>
		public static void SETNZ (R8Type target)
		{
		}

		/// <summary>
		/// SETO mem8
		/// </summary>
		public static void SETO (ByteMemory target)
		{
		}

		/// <summary>
		/// SETO mem8
		/// </summary>
		public unsafe static void SETO (byte* target)
		{
		}

		/// <summary>
		/// SETO rmreg8
		/// </summary>
		public static void SETO (R8Type target)
		{
		}

		/// <summary>
		/// SETP mem8
		/// </summary>
		public static void SETP (ByteMemory target)
		{
		}

		/// <summary>
		/// SETP mem8
		/// </summary>
		public unsafe static void SETP (byte* target)
		{
		}

		/// <summary>
		/// SETP rmreg8
		/// </summary>
		public static void SETP (R8Type target)
		{
		}

		/// <summary>
		/// SETPE mem8
		/// </summary>
		public static void SETPE (ByteMemory target)
		{
		}

		/// <summary>
		/// SETPE mem8
		/// </summary>
		public unsafe static void SETPE (byte* target)
		{
		}

		/// <summary>
		/// SETPE rmreg8
		/// </summary>
		public static void SETPE (R8Type target)
		{
		}

		/// <summary>
		/// SETPO mem8
		/// </summary>
		public static void SETPO (ByteMemory target)
		{
		}

		/// <summary>
		/// SETPO mem8
		/// </summary>
		public unsafe static void SETPO (byte* target)
		{
		}

		/// <summary>
		/// SETPO rmreg8
		/// </summary>
		public static void SETPO (R8Type target)
		{
		}

		/// <summary>
		/// SETS mem8
		/// </summary>
		public static void SETS (ByteMemory target)
		{
		}

		/// <summary>
		/// SETS mem8
		/// </summary>
		public unsafe static void SETS (byte* target)
		{
		}

		/// <summary>
		/// SETS rmreg8
		/// </summary>
		public static void SETS (R8Type target)
		{
		}

		/// <summary>
		/// SETZ mem8
		/// </summary>
		public static void SETZ (ByteMemory target)
		{
		}

		/// <summary>
		/// SETZ mem8
		/// </summary>
		public unsafe static void SETZ (byte* target)
		{
		}

		/// <summary>
		/// SETZ rmreg8
		/// </summary>
		public static void SETZ (R8Type target)
		{
		}

		/// <summary>
		/// SFENCE 
		/// </summary>
		public static void SFENCE ()
		{
		}

		/// <summary>
		/// SGDT mem
		/// </summary>
		public static void SGDT (Memory target)
		{
		}

		/// <summary>
		/// SGDT mem
		/// </summary>
		public unsafe static void SGDT (byte* target)
		{
		}

		/// <summary>
		/// SHL mem8,CL
		/// </summary>
		public static void SHL__CL (ByteMemory target)
		{
		}

		/// <summary>
		/// SHL mem8,CL
		/// </summary>
		public unsafe static void SHL__CL (byte* target)
		{
		}

		/// <summary>
		/// SHL mem8,imm8
		/// </summary>
		public static void SHL (ByteMemory target, Byte source)
		{
		}

		/// <summary>
		/// SHL mem8,imm8
		/// </summary>
		public unsafe static void SHL (byte* target, Byte source)
		{
		}

		/// <summary>
		/// SHL mem16,CL
		/// </summary>
		public static void SHL__CL (WordMemory target)
		{
		}

		/// <summary>
		/// SHL mem16,CL
		/// </summary>
		public unsafe static void SHL__CL (UInt16* target)
		{
		}

		/// <summary>
		/// SHL mem16,imm8
		/// </summary>
		public static void SHL (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// SHL mem16,imm8
		/// </summary>
		public unsafe static void SHL (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// SHL mem32,CL
		/// </summary>
		public static void SHL__CL (DWordMemory target)
		{
		}

		/// <summary>
		/// SHL mem32,CL
		/// </summary>
		public unsafe static void SHL__CL (UInt32* target)
		{
		}

		/// <summary>
		/// SHL mem32,imm8
		/// </summary>
		public static void SHL (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// SHL mem32,imm8
		/// </summary>
		public unsafe static void SHL (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// SHL rmreg8,CL
		/// </summary>
		public static void SHL__CL (R8Type target)
		{
		}

		/// <summary>
		/// SHL rmreg8,imm8
		/// </summary>
		public static void SHL (R8Type target, Byte source)
		{
		}

		/// <summary>
		/// SHL rmreg16,CL
		/// </summary>
		public static void SHL__CL (R16Type target)
		{
		}

		/// <summary>
		/// SHL rmreg16,imm8
		/// </summary>
		public static void SHL (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// SHL rmreg32,CL
		/// </summary>
		public static void SHL__CL (R32Type target)
		{
		}

		/// <summary>
		/// SHL rmreg32,imm8
		/// </summary>
		public static void SHL (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// SHLD mem16,reg16,imm8
		/// </summary>
		public static void SHLD (WordMemory target, R16Type source, Byte value)
		{
		}

		/// <summary>
		/// SHLD mem16,reg16,imm8
		/// </summary>
		public unsafe static void SHLD (UInt16* target, R16Type source, Byte value)
		{
		}

		/// <summary>
		/// SHLD mem32,reg32,imm8
		/// </summary>
		public static void SHLD (DWordMemory target, R32Type source, Byte value)
		{
		}

		/// <summary>
		/// SHLD mem32,reg32,imm8
		/// </summary>
		public unsafe static void SHLD (UInt32* target, R32Type source, Byte value)
		{
		}

		/// <summary>
		/// SHLD mem16,reg16,CL
		/// </summary>
		public static void SHLD___CL (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// SHLD mem16,reg16,CL
		/// </summary>
		public unsafe static void SHLD___CL (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// SHLD mem32,reg32,CL
		/// </summary>
		public static void SHLD___CL (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// SHLD mem32,reg32,CL
		/// </summary>
		public unsafe static void SHLD___CL (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// SHLD rmreg16,reg16,imm8
		/// </summary>
		public static void SHLD (R16Type target, R16Type source, Byte value)
		{
		}

		/// <summary>
		/// SHLD rmreg32,reg32,imm8
		/// </summary>
		public static void SHLD (R32Type target, R32Type source, Byte value)
		{
		}

		/// <summary>
		/// SHLD rmreg16,reg16,CL
		/// </summary>
		public static void SHLD___CL (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// SHLD rmreg32,reg32,CL
		/// </summary>
		public static void SHLD___CL (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// SHR mem8,CL
		/// </summary>
		public static void SHR__CL (ByteMemory target)
		{
		}

		/// <summary>
		/// SHR mem8,CL
		/// </summary>
		public unsafe static void SHR__CL (byte* target)
		{
		}

		/// <summary>
		/// SHR mem8,imm8
		/// </summary>
		public static void SHR (ByteMemory target, Byte source)
		{
		}

		/// <summary>
		/// SHR mem8,imm8
		/// </summary>
		public unsafe static void SHR (byte* target, Byte source)
		{
		}

		/// <summary>
		/// SHR mem16,CL
		/// </summary>
		public static void SHR__CL (WordMemory target)
		{
		}

		/// <summary>
		/// SHR mem16,CL
		/// </summary>
		public unsafe static void SHR__CL (UInt16* target)
		{
		}

		/// <summary>
		/// SHR mem16,imm8
		/// </summary>
		public static void SHR (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// SHR mem16,imm8
		/// </summary>
		public unsafe static void SHR (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// SHR mem32,CL
		/// </summary>
		public static void SHR__CL (DWordMemory target)
		{
		}

		/// <summary>
		/// SHR mem32,CL
		/// </summary>
		public unsafe static void SHR__CL (UInt32* target)
		{
		}

		/// <summary>
		/// SHR mem32,imm8
		/// </summary>
		public static void SHR (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// SHR mem32,imm8
		/// </summary>
		public unsafe static void SHR (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// SHR rmreg8,CL
		/// </summary>
		public static void SHR__CL (R8Type target)
		{
		}

		/// <summary>
		/// SHR rmreg8,imm8
		/// </summary>
		public static void SHR (R8Type target, Byte source)
		{
		}

		/// <summary>
		/// SHR rmreg16,CL
		/// </summary>
		public static void SHR__CL (R16Type target)
		{
		}

		/// <summary>
		/// SHR rmreg16,imm8
		/// </summary>
		public static void SHR (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// SHR rmreg32,CL
		/// </summary>
		public static void SHR__CL (R32Type target)
		{
		}

		/// <summary>
		/// SHR rmreg32,imm8
		/// </summary>
		public static void SHR (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// SHRD mem16,reg16,imm8
		/// </summary>
		public static void SHRD (WordMemory target, R16Type source, Byte value)
		{
		}

		/// <summary>
		/// SHRD mem16,reg16,imm8
		/// </summary>
		public unsafe static void SHRD (UInt16* target, R16Type source, Byte value)
		{
		}

		/// <summary>
		/// SHRD mem32,reg32,imm8
		/// </summary>
		public static void SHRD (DWordMemory target, R32Type source, Byte value)
		{
		}

		/// <summary>
		/// SHRD mem32,reg32,imm8
		/// </summary>
		public unsafe static void SHRD (UInt32* target, R32Type source, Byte value)
		{
		}

		/// <summary>
		/// SHRD mem16,reg16,CL
		/// </summary>
		public static void SHRD___CL (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// SHRD mem16,reg16,CL
		/// </summary>
		public unsafe static void SHRD___CL (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// SHRD mem32,reg32,CL
		/// </summary>
		public static void SHRD___CL (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// SHRD mem32,reg32,CL
		/// </summary>
		public unsafe static void SHRD___CL (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// SHRD rmreg16,reg16,imm8
		/// </summary>
		public static void SHRD (R16Type target, R16Type source, Byte value)
		{
		}

		/// <summary>
		/// SHRD rmreg32,reg32,imm8
		/// </summary>
		public static void SHRD (R32Type target, R32Type source, Byte value)
		{
		}

		/// <summary>
		/// SHRD rmreg16,reg16,CL
		/// </summary>
		public static void SHRD___CL (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// SHRD rmreg32,reg32,CL
		/// </summary>
		public static void SHRD___CL (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// SIDT mem
		/// </summary>
		public static void SIDT (Memory target)
		{
		}

		/// <summary>
		/// SIDT mem
		/// </summary>
		public unsafe static void SIDT (byte* target)
		{
		}

		/// <summary>
		/// SLDT mem16
		/// </summary>
		public static void SLDT (WordMemory target)
		{
		}

		/// <summary>
		/// SLDT mem16
		/// </summary>
		public unsafe static void SLDT (UInt16* target)
		{
		}

		/// <summary>
		/// SLDT rmreg16
		/// </summary>
		public static void SLDT (R16Type target)
		{
		}

		/// <summary>
		/// SMSW mem16
		/// </summary>
		public static void SMSW (WordMemory target)
		{
		}

		/// <summary>
		/// SMSW mem16
		/// </summary>
		public unsafe static void SMSW (UInt16* target)
		{
		}

		/// <summary>
		/// SMSW rmreg16
		/// </summary>
		public static void SMSW (R16Type target)
		{
		}

		/// <summary>
		/// STC 
		/// </summary>
		public static void STC ()
		{
		}

		/// <summary>
		/// STD 
		/// </summary>
		public static void STD ()
		{
		}

		/// <summary>
		/// STI 
		/// </summary>
		public static void STI ()
		{
		}

		/// <summary>
		/// STOSB 
		/// </summary>
		public static void STOSB ()
		{
		}

		/// <summary>
		/// STOSD 
		/// </summary>
		public static void STOSD ()
		{
		}

		/// <summary>
		/// STOSW 
		/// </summary>
		public static void STOSW ()
		{
		}

		/// <summary>
		/// STR mem16
		/// </summary>
		public static void STR (WordMemory target)
		{
		}

		/// <summary>
		/// STR mem16
		/// </summary>
		public unsafe static void STR (UInt16* target)
		{
		}

		/// <summary>
		/// STR rmreg16
		/// </summary>
		public static void STR (R16Type target)
		{
		}

		/// <summary>
		/// SUB mem8,reg8
		/// </summary>
		public static void SUB (ByteMemory target, R8Type source)
		{
		}

		/// <summary>
		/// SUB mem8,reg8
		/// </summary>
		public unsafe static void SUB (byte* target, R8Type source)
		{
		}

		/// <summary>
		/// SUB mem16,reg16
		/// </summary>
		public static void SUB (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// SUB mem16,reg16
		/// </summary>
		public unsafe static void SUB (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// SUB mem32,reg32
		/// </summary>
		public static void SUB (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// SUB mem32,reg32
		/// </summary>
		public unsafe static void SUB (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// SUB reg8,mem8
		/// </summary>
		public static void SUB (R8Type target, ByteMemory source)
		{
		}

		/// <summary>
		/// SUB reg8,mem8
		/// </summary>
		public unsafe static void SUB (R8Type target, byte* source)
		{
		}

		/// <summary>
		/// SUB reg16,mem16
		/// </summary>
		public static void SUB (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// SUB reg16,mem16
		/// </summary>
		public unsafe static void SUB (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// SUB reg32,mem32
		/// </summary>
		public static void SUB (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// SUB reg32,mem32
		/// </summary>
		public unsafe static void SUB (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// SUB mem8,imm8
		/// </summary>
		public static void SUB (ByteMemory target, Byte source)
		{
		}

		/// <summary>
		/// SUB mem8,imm8
		/// </summary>
		public unsafe static void SUB (byte* target, Byte source)
		{
		}

		/// <summary>
		/// SUB mem16,imm16
		/// </summary>
		public static void SUB (WordMemory target, UInt16 source)
		{
		}

		/// <summary>
		/// SUB mem16,imm16
		/// </summary>
		public unsafe static void SUB (UInt16* target, UInt16 source)
		{
		}

		/// <summary>
		/// SUB mem32,imm32
		/// </summary>
		public static void SUB (DWordMemory target, UInt32 source)
		{
		}

		/// <summary>
		/// SUB mem32,imm32
		/// </summary>
		public unsafe static void SUB (UInt32* target, UInt32 source)
		{
		}

		/// <summary>
		/// SUB mem16,imm8
		/// </summary>
		public static void SUB (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// SUB mem16,imm8
		/// </summary>
		public unsafe static void SUB (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// SUB mem32,imm8
		/// </summary>
		public static void SUB (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// SUB mem32,imm8
		/// </summary>
		public unsafe static void SUB (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// SUB rmreg8,reg8
		/// </summary>
		public static void SUB (R8Type target, R8Type source)
		{
		}

		/// <summary>
		/// SUB rmreg16,reg16
		/// </summary>
		public static void SUB (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// SUB rmreg32,reg32
		/// </summary>
		public static void SUB (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// SUB rmreg8,imm8
		/// </summary>
		public static void SUB (R8Type target, Byte source)
		{
		}

		/// <summary>
		/// SUB rmreg16,imm16
		/// </summary>
		public static void SUB (R16Type target, UInt16 source)
		{
		}

		/// <summary>
		/// SUB rmreg32,imm32
		/// </summary>
		public static void SUB (R32Type target, UInt32 source)
		{
		}

		/// <summary>
		/// SUB rmreg16,imm8
		/// </summary>
		public static void SUB (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// SUB rmreg32,imm8
		/// </summary>
		public static void SUB (R32Type target, Byte source)
		{
		}

		/// <summary>
		/// SYSCALL 
		/// </summary>
		public static void SYSCALL ()
		{
		}

		/// <summary>
		/// SYSENTER 
		/// </summary>
		public static void SYSENTER ()
		{
		}

		/// <summary>
		/// SYSEXIT 
		/// </summary>
		public static void SYSEXIT ()
		{
		}

		/// <summary>
		/// SYSRET 
		/// </summary>
		public static void SYSRET ()
		{
		}

		/// <summary>
		/// TEST mem8,reg8
		/// </summary>
		public static void TEST (ByteMemory target, R8Type source)
		{
		}

		/// <summary>
		/// TEST mem8,reg8
		/// </summary>
		public unsafe static void TEST (byte* target, R8Type source)
		{
		}

		/// <summary>
		/// TEST mem16,reg16
		/// </summary>
		public static void TEST (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// TEST mem16,reg16
		/// </summary>
		public unsafe static void TEST (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// TEST mem32,reg32
		/// </summary>
		public static void TEST (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// TEST mem32,reg32
		/// </summary>
		public unsafe static void TEST (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// TEST mem8,imm8
		/// </summary>
		public static void TEST (ByteMemory target, Byte source)
		{
		}

		/// <summary>
		/// TEST mem8,imm8
		/// </summary>
		public unsafe static void TEST (byte* target, Byte source)
		{
		}

		/// <summary>
		/// TEST mem16,imm16
		/// </summary>
		public static void TEST (WordMemory target, UInt16 source)
		{
		}

		/// <summary>
		/// TEST mem16,imm16
		/// </summary>
		public unsafe static void TEST (UInt16* target, UInt16 source)
		{
		}

		/// <summary>
		/// TEST mem32,imm32
		/// </summary>
		public static void TEST (DWordMemory target, UInt32 source)
		{
		}

		/// <summary>
		/// TEST mem32,imm32
		/// </summary>
		public unsafe static void TEST (UInt32* target, UInt32 source)
		{
		}

		/// <summary>
		/// TEST rmreg8,reg8
		/// </summary>
		public static void TEST (R8Type target, R8Type source)
		{
		}

		/// <summary>
		/// TEST rmreg16,reg16
		/// </summary>
		public static void TEST (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// TEST rmreg32,reg32
		/// </summary>
		public static void TEST (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// TEST rmreg8,imm8
		/// </summary>
		public static void TEST (R8Type target, Byte source)
		{
		}

		/// <summary>
		/// TEST rmreg16,imm16
		/// </summary>
		public static void TEST (R16Type target, UInt16 source)
		{
		}

		/// <summary>
		/// TEST rmreg32,imm32
		/// </summary>
		public static void TEST (R32Type target, UInt32 source)
		{
		}

		/// <summary>
		/// TIMES 
		/// </summary>
		public static void TIMES (UInt32 length, Byte value)
		{
		}

		/// <summary>
		/// UTF16 
		/// </summary>
		public static void UTF16 (string values)
		{
		}

		/// <summary>
		/// VERR mem16
		/// </summary>
		public static void VERR (WordMemory target)
		{
		}

		/// <summary>
		/// VERR mem16
		/// </summary>
		public unsafe static void VERR (UInt16* target)
		{
		}

		/// <summary>
		/// VERR rmreg16
		/// </summary>
		public static void VERR (R16Type target)
		{
		}

		/// <summary>
		/// VERW mem16
		/// </summary>
		public static void VERW (WordMemory target)
		{
		}

		/// <summary>
		/// VERW mem16
		/// </summary>
		public unsafe static void VERW (UInt16* target)
		{
		}

		/// <summary>
		/// VERW rmreg16
		/// </summary>
		public static void VERW (R16Type target)
		{
		}

		/// <summary>
		/// WAIT 
		/// </summary>
		public static void WAIT ()
		{
		}

		/// <summary>
		/// WBINVD 
		/// </summary>
		public static void WBINVD ()
		{
		}

		/// <summary>
		/// WRMSR 
		/// </summary>
		public static void WRMSR ()
		{
		}

		/// <summary>
		/// XADD mem8,reg8
		/// </summary>
		public static void XADD (ByteMemory target, R8Type source)
		{
		}

		/// <summary>
		/// XADD mem8,reg8
		/// </summary>
		public unsafe static void XADD (byte* target, R8Type source)
		{
		}

		/// <summary>
		/// XADD mem16,reg16
		/// </summary>
		public static void XADD (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// XADD mem16,reg16
		/// </summary>
		public unsafe static void XADD (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// XADD mem32,reg32
		/// </summary>
		public static void XADD (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// XADD mem32,reg32
		/// </summary>
		public unsafe static void XADD (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// XADD rmreg8,reg8
		/// </summary>
		public static void XADD (R8Type target, R8Type source)
		{
		}

		/// <summary>
		/// XADD rmreg16,reg16
		/// </summary>
		public static void XADD (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// XADD rmreg32,reg32
		/// </summary>
		public static void XADD (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// XCHG reg8,mem8
		/// </summary>
		public static void XCHG (R8Type target, ByteMemory source)
		{
		}

		/// <summary>
		/// XCHG reg8,mem8
		/// </summary>
		public unsafe static void XCHG (R8Type target, byte* source)
		{
		}

		/// <summary>
		/// XCHG reg16,mem16
		/// </summary>
		public static void XCHG (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// XCHG reg16,mem16
		/// </summary>
		public unsafe static void XCHG (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// XCHG reg32,mem32
		/// </summary>
		public static void XCHG (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// XCHG reg32,mem32
		/// </summary>
		public unsafe static void XCHG (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// XCHG mem8,reg8
		/// </summary>
		public static void XCHG (ByteMemory target, R8Type source)
		{
		}

		/// <summary>
		/// XCHG mem8,reg8
		/// </summary>
		public unsafe static void XCHG (byte* target, R8Type source)
		{
		}

		/// <summary>
		/// XCHG mem16,reg16
		/// </summary>
		public static void XCHG (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// XCHG mem16,reg16
		/// </summary>
		public unsafe static void XCHG (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// XCHG mem32,reg32
		/// </summary>
		public static void XCHG (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// XCHG mem32,reg32
		/// </summary>
		public unsafe static void XCHG (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// XCHG reg8,rmreg8
		/// </summary>
		public static void XCHG (R8Type target, R8Type source)
		{
		}

		/// <summary>
		/// XCHG reg16,rmreg16
		/// </summary>
		public static void XCHG (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// XCHG reg32,rmreg32
		/// </summary>
		public static void XCHG (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// XLAT 
		/// </summary>
		public static void XLAT ()
		{
		}

		/// <summary>
		/// XLATB 
		/// </summary>
		public static void XLATB ()
		{
		}

		/// <summary>
		/// XOR mem8,reg8
		/// </summary>
		public static void XOR (ByteMemory target, R8Type source)
		{
		}

		/// <summary>
		/// XOR mem8,reg8
		/// </summary>
		public unsafe static void XOR (byte* target, R8Type source)
		{
		}

		/// <summary>
		/// XOR mem16,reg16
		/// </summary>
		public static void XOR (WordMemory target, R16Type source)
		{
		}

		/// <summary>
		/// XOR mem16,reg16
		/// </summary>
		public unsafe static void XOR (UInt16* target, R16Type source)
		{
		}

		/// <summary>
		/// XOR mem32,reg32
		/// </summary>
		public static void XOR (DWordMemory target, R32Type source)
		{
		}

		/// <summary>
		/// XOR mem32,reg32
		/// </summary>
		public unsafe static void XOR (UInt32* target, R32Type source)
		{
		}

		/// <summary>
		/// XOR reg8,mem8
		/// </summary>
		public static void XOR (R8Type target, ByteMemory source)
		{
		}

		/// <summary>
		/// XOR reg8,mem8
		/// </summary>
		public unsafe static void XOR (R8Type target, byte* source)
		{
		}

		/// <summary>
		/// XOR reg16,mem16
		/// </summary>
		public static void XOR (R16Type target, WordMemory source)
		{
		}

		/// <summary>
		/// XOR reg16,mem16
		/// </summary>
		public unsafe static void XOR (R16Type target, UInt16* source)
		{
		}

		/// <summary>
		/// XOR reg32,mem32
		/// </summary>
		public static void XOR (R32Type target, DWordMemory source)
		{
		}

		/// <summary>
		/// XOR reg32,mem32
		/// </summary>
		public unsafe static void XOR (R32Type target, UInt32* source)
		{
		}

		/// <summary>
		/// XOR mem8,imm8
		/// </summary>
		public static void XOR (ByteMemory target, Byte source)
		{
		}

		/// <summary>
		/// XOR mem8,imm8
		/// </summary>
		public unsafe static void XOR (byte* target, Byte source)
		{
		}

		/// <summary>
		/// XOR mem16,imm16
		/// </summary>
		public static void XOR (WordMemory target, UInt16 source)
		{
		}

		/// <summary>
		/// XOR mem16,imm16
		/// </summary>
		public unsafe static void XOR (UInt16* target, UInt16 source)
		{
		}

		/// <summary>
		/// XOR mem32,imm32
		/// </summary>
		public static void XOR (DWordMemory target, UInt32 source)
		{
		}

		/// <summary>
		/// XOR mem32,imm32
		/// </summary>
		public unsafe static void XOR (UInt32* target, UInt32 source)
		{
		}

		/// <summary>
		/// XOR mem16,imm8
		/// </summary>
		public static void XOR (WordMemory target, Byte source)
		{
		}

		/// <summary>
		/// XOR mem16,imm8
		/// </summary>
		public unsafe static void XOR (UInt16* target, Byte source)
		{
		}

		/// <summary>
		/// XOR mem32,imm8
		/// </summary>
		public static void XOR (DWordMemory target, Byte source)
		{
		}

		/// <summary>
		/// XOR mem32,imm8
		/// </summary>
		public unsafe static void XOR (UInt32* target, Byte source)
		{
		}

		/// <summary>
		/// XOR rmreg8,reg8
		/// </summary>
		public static void XOR (R8Type target, R8Type source)
		{
		}

		/// <summary>
		/// XOR rmreg16,reg16
		/// </summary>
		public static void XOR (R16Type target, R16Type source)
		{
		}

		/// <summary>
		/// XOR rmreg32,reg32
		/// </summary>
		public static void XOR (R32Type target, R32Type source)
		{
		}

		/// <summary>
		/// XOR rmreg8,imm8
		/// </summary>
		public static void XOR (R8Type target, Byte source)
		{
		}

		/// <summary>
		/// XOR rmreg16,imm16
		/// </summary>
		public static void XOR (R16Type target, UInt16 source)
		{
		}

		/// <summary>
		/// XOR rmreg32,imm32
		/// </summary>
		public static void XOR (R32Type target, UInt32 source)
		{
		}

		/// <summary>
		/// XOR rmreg16,imm8
		/// </summary>
		public static void XOR (R16Type target, Byte source)
		{
		}

		/// <summary>
		/// XOR rmreg32,imm8
		/// </summary>
		public static void XOR (R32Type target, Byte source)
		{
		}
	}

}
