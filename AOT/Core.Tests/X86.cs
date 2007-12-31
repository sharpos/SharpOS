/**
 *  (C) 2006-2007 Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 * Licensed under the terms of the GNU GPL v3,
 *  with Classpath Linking Exception for Libraries
 * 
 */

using System;
using System.IO;
using SharpOS;
using SharpOS.AOT.X86;
using NUnit.Framework;

[TestFixture]
public class X86 {
	public bool CompareData (MemoryStream memoryStream, byte [] target)
	{
		byte [] source = new byte [memoryStream.Length];
		memoryStream.Seek (0, SeekOrigin.Begin);
		memoryStream.Read (source, 0, source.Length);

		if (memoryStream.Length != target.Length) {
			return false;
		}

		for (int i = 0; i < source.Length; i++) {
			if (source [i] != target [i]) {
				return false;
			}
		}

		return true;
	}

	// AAA 
	[Test]
	public void AAA ()
	{
		// AAA
		// AAA ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AAA ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x37 };
		Assert.IsTrue (CompareData (memoryStream, target), "'AAA' failed.");
	}

	// AAD 
	[Test]
	public void AAD ()
	{
		// AAD
		// AAD ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AAD ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd5, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'AAD' failed.");
	}

	// AAD imm8
	[Test]
	public void AAD_imm8 ()
	{
		// AAD 0xd
		// AAD (0xd)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AAD (0xd);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd5, 0xd };
		Assert.IsTrue (CompareData (memoryStream, target), "'AAD 0xd' failed.");
	}

	// AAM 
	[Test]
	public void AAM ()
	{
		// AAM
		// AAM ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AAM ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd4, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'AAM' failed.");
	}

	// AAM imm8
	[Test]
	public void AAM_imm8 ()
	{
		// AAM 0xb
		// AAM (0xb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AAM (0xb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd4, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'AAM 0xb' failed.");
	}

	// AAS 
	[Test]
	public void AAS ()
	{
		// AAS
		// AAS ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AAS ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3f };
		Assert.IsTrue (CompareData (memoryStream, target), "'AAS' failed.");
	}

	// ADC mem8,reg8
	[Test]
	public void ADC_mem8_reg8 ()
	{
		// ADC [ES:EDX], DL
		// ADC (new ByteMemory(Seg.ES, R32.EDX, null, 0), R8.DL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (new ByteMemory (Seg.ES, R32.EDX, null, 0), R8.DL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0x10, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC [ES:EDX], DL' failed.");
	}

	// ADC mem16,reg16
	[Test]
	public void ADC_mem16_reg16 ()
	{
		// ADC [0x12345678], CX
		// ADC (new WordMemory(null, null, null, 0, 0x12345678), R16.CX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (new WordMemory (null, null, null, 0, 0x12345678), R16.CX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x11, 0xd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC [0x12345678], CX' failed.");
	}

	// ADC mem32,reg32
	[Test]
	public void ADC_mem32_reg32 ()
	{
		// ADC [ESI + EBP*1], EBX
		// ADC (new DWordMemory(null, R32.ESI, R32.EBP, 0), R32.EBX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (new DWordMemory (null, R32.ESI, R32.EBP, 0), R32.EBX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x11, 0x1c, 0x2e };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC [ESI + EBP*1], EBX' failed.");
	}

	// ADC reg8,mem8
	[Test]
	public void ADC_reg8_mem8 ()
	{
		// ADC BH, [0x12345678]
		// ADC (R8.BH, new ByteMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (R8.BH, new ByteMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x12, 0x3d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC BH, [0x12345678]' failed.");
	}

	// ADC reg16,mem16
	[Test]
	public void ADC_reg16_mem16 ()
	{
		// ADC BX, [DS:ESP]
		// ADC (R16.BX, new WordMemory(Seg.DS, R32.ESP, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (R16.BX, new WordMemory (Seg.DS, R32.ESP, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x66, 0x13, 0x1c, 0x24 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC BX, [DS:ESP]' failed.");
	}

	// ADC reg32,mem32
	[Test]
	public void ADC_reg32_mem32 ()
	{
		// ADC EBX, [EBX]
		// ADC (R32.EBX, new DWordMemory(null, R32.EBX, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (R32.EBX, new DWordMemory (null, R32.EBX, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x13, 0x1b };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC EBX, [EBX]' failed.");
	}

	// ADC mem8,imm8
	[Test]
	public void ADC_mem8_imm8 ()
	{
		// ADC Byte [0x12345678], 0xd
		// ADC (new ByteMemory(null, null, null, 0, 0x12345678), 0xd)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (new ByteMemory (null, null, null, 0, 0x12345678), 0xd);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x80, 0x15, 0x78, 0x56, 0x34, 0x12, 0xd };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC Byte [0x12345678], 0xd' failed.");
	}

	// ADC mem16,imm16
	[Test]
	public void ADC_mem16_imm16 ()
	{
		// ADC Word [0x12345678], 0x3c4
		// ADC (new WordMemory(null, null, null, 0, 0x12345678), 0x3c4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (new WordMemory (null, null, null, 0, 0x12345678), 0x3c4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x81, 0x15, 0x78, 0x56, 0x34, 0x12, 0xc4, 0x3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC Word [0x12345678], 0x3c4' failed.");
	}

	// ADC mem32,imm32
	[Test]
	public void ADC_mem32_imm32 ()
	{
		// ADC DWord [EDI + EAX*4 + 0x12345678], 0x3e5b6ea
		// ADC (new DWordMemory(null, R32.EDI, R32.EAX, 2, 0x12345678), 0x3e5b6ea)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (new DWordMemory (null, R32.EDI, R32.EAX, 2, 0x12345678), 0x3e5b6ea);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x81, 0x94, 0x87, 0x78, 0x56, 0x34, 0x12, 0xea, 0xb6, 0xe5, 0x3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC DWord [EDI + EAX*4 + 0x12345678], 0x3e5b6ea' failed.");
	}

	// ADC mem16,imm8
	[Test]
	public void ADC_mem16_imm8 ()
	{
		// ADC Word [FS:EBX + 0x12345678], 0x0
		// ADC (new WordMemory(Seg.FS, R32.EBX, null, 0, 0x12345678), 0x0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (new WordMemory (Seg.FS, R32.EBX, null, 0, 0x12345678), 0x0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0x66, 0x83, 0x93, 0x78, 0x56, 0x34, 0x12, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC Word [FS:EBX + 0x12345678], 0x0' failed.");
	}

	// ADC mem32,imm8
	[Test]
	public void ADC_mem32_imm8 ()
	{
		// ADC DWord [ES:EDX], 0xb
		// ADC (new DWordMemory(Seg.ES, R32.EDX, null, 0), 0xb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (new DWordMemory (Seg.ES, R32.EDX, null, 0), 0xb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0x83, 0x12, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC DWord [ES:EDX], 0xb' failed.");
	}

	// ADC rmreg8,reg8
	[Test]
	public void ADC_rmreg8_reg8 ()
	{
		// ADC DL, BH
		// ADC (R8.DL, R8.BH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (R8.DL, R8.BH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x10, 0xfa };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC DL, BH' failed.");
	}

	// ADC rmreg16,reg16
	[Test]
	public void ADC_rmreg16_reg16 ()
	{
		// ADC DI, SI
		// ADC (R16.DI, R16.SI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (R16.DI, R16.SI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x11, 0xf7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC DI, SI' failed.");
	}

	// ADC rmreg32,reg32
	[Test]
	public void ADC_rmreg32_reg32 ()
	{
		// ADC ESI, EBX
		// ADC (R32.ESI, R32.EBX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (R32.ESI, R32.EBX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x11, 0xde };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC ESI, EBX' failed.");
	}

	// ADC rmreg8,imm8
	[Test]
	public void ADC_rmreg8_imm8 ()
	{
		// ADC AH, 0xb
		// ADC (R8.AH, 0xb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (R8.AH, 0xb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x80, 0xd4, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC AH, 0xb' failed.");
	}

	// ADC rmreg16,imm16
	[Test]
	public void ADC_rmreg16_imm16 ()
	{
		// ADC SP, 0x2c4
		// ADC (R16.SP, 0x2c4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (R16.SP, 0x2c4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x81, 0xd4, 0xc4, 0x2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC SP, 0x2c4' failed.");
	}

	// ADC rmreg32,imm32
	[Test]
	public void ADC_rmreg32_imm32 ()
	{
		// ADC ESP, 0x7683f00
		// ADC (R32.ESP, 0x7683f00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (R32.ESP, 0x7683f00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x81, 0xd4, 0x0, 0x3f, 0x68, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC ESP, 0x7683f00' failed.");
	}

	// ADC rmreg16,imm8
	[Test]
	public void ADC_rmreg16_imm8 ()
	{
		// ADC DX, 0x3
		// ADC (R16.DX, 0x3)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (R16.DX, 0x3);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x83, 0xd2, 0x3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC DX, 0x3' failed.");
	}

	// ADC rmreg32,imm8
	[Test]
	public void ADC_rmreg32_imm8 ()
	{
		// ADC EBP, 0x7
		// ADC (R32.EBP, 0x7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADC (R32.EBP, 0x7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x83, 0xd5, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADC EBP, 0x7' failed.");
	}

	// ADD mem8,reg8
	[Test]
	public void ADD_mem8_reg8 ()
	{
		// ADD [DS:EAX], CL
		// ADD (new ByteMemory(Seg.DS, R32.EAX, null, 0), R8.CL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (new ByteMemory (Seg.DS, R32.EAX, null, 0), R8.CL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x0, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD [DS:EAX], CL' failed.");
	}

	// ADD mem16,reg16
	[Test]
	public void ADD_mem16_reg16 ()
	{
		// ADD [CS:EDX], BP
		// ADD (new WordMemory(Seg.CS, R32.EDX, null, 0), R16.BP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (new WordMemory (Seg.CS, R32.EDX, null, 0), R16.BP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0x66, 0x1, 0x2a };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD [CS:EDX], BP' failed.");
	}

	// ADD mem32,reg32
	[Test]
	public void ADD_mem32_reg32 ()
	{
		// ADD [EAX + EDX*1], EBX
		// ADD (new DWordMemory(null, R32.EAX, R32.EDX, 0), R32.EBX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (new DWordMemory (null, R32.EAX, R32.EDX, 0), R32.EBX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x1, 0x1c, 0x10 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD [EAX + EDX*1], EBX' failed.");
	}

	// ADD reg8,mem8
	[Test]
	public void ADD_reg8_mem8 ()
	{
		// ADD AL, [EDI]
		// ADD (R8.AL, new ByteMemory(null, R32.EDI, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (R8.AL, new ByteMemory (null, R32.EDI, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD AL, [EDI]' failed.");
	}

	// ADD reg16,mem16
	[Test]
	public void ADD_reg16_mem16 ()
	{
		// ADD CX, [GS:EBX + 0x12345678]
		// ADD (R16.CX, new WordMemory(Seg.GS, R32.EBX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (R16.CX, new WordMemory (Seg.GS, R32.EBX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0x66, 0x3, 0x8b, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD CX, [GS:EBX + 0x12345678]' failed.");
	}

	// ADD reg32,mem32
	[Test]
	public void ADD_reg32_mem32 ()
	{
		// ADD ESI, [ESI*2]
		// ADD (R32.ESI, new DWordMemory(null, null, R32.ESI, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (R32.ESI, new DWordMemory (null, null, R32.ESI, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3, 0x34, 0x36 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD ESI, [ESI*2]' failed.");
	}

	// ADD mem8,imm8
	[Test]
	public void ADD_mem8_imm8 ()
	{
		// ADD Byte [ESP + EBX*2 + 0x12345678], 0x8
		// ADD (new ByteMemory(null, R32.ESP, R32.EBX, 1, 0x12345678), 0x8)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (new ByteMemory (null, R32.ESP, R32.EBX, 1, 0x12345678), 0x8);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x80, 0x84, 0x5c, 0x78, 0x56, 0x34, 0x12, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD Byte [ESP + EBX*2 + 0x12345678], 0x8' failed.");
	}

	// ADD mem16,imm16
	[Test]
	public void ADD_mem16_imm16 ()
	{
		// ADD Word [0x12345678], 0x77b
		// ADD (new WordMemory(null, null, null, 0, 0x12345678), 0x77b)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (new WordMemory (null, null, null, 0, 0x12345678), 0x77b);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x81, 0x5, 0x78, 0x56, 0x34, 0x12, 0x7b, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD Word [0x12345678], 0x77b' failed.");
	}

	// ADD mem32,imm32
	[Test]
	public void ADD_mem32_imm32 ()
	{
		// ADD DWord [ES:0x12345678], 0x55afc7d
		// ADD (new DWordMemory(Seg.ES, null, null, 0, 0x12345678), 0x55afc7d)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (new DWordMemory (Seg.ES, null, null, 0, 0x12345678), 0x55afc7d);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0x81, 0x5, 0x78, 0x56, 0x34, 0x12, 0x7d, 0xfc, 0x5a, 0x5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD DWord [ES:0x12345678], 0x55afc7d' failed.");
	}

	// ADD mem16,imm8
	[Test]
	public void ADD_mem16_imm8 ()
	{
		// ADD Word [CS:0x12345678], 0x6
		// ADD (new WordMemory(Seg.CS, null, null, 0, 0x12345678), 0x6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (new WordMemory (Seg.CS, null, null, 0, 0x12345678), 0x6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0x66, 0x83, 0x5, 0x78, 0x56, 0x34, 0x12, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD Word [CS:0x12345678], 0x6' failed.");
	}

	// ADD mem32,imm8
	[Test]
	public void ADD_mem32_imm8 ()
	{
		// ADD DWord [CS:EBX + 0x12345678], 0xc
		// ADD (new DWordMemory(Seg.CS, R32.EBX, null, 0, 0x12345678), 0xc)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (new DWordMemory (Seg.CS, R32.EBX, null, 0, 0x12345678), 0xc);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0x83, 0x83, 0x78, 0x56, 0x34, 0x12, 0xc };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD DWord [CS:EBX + 0x12345678], 0xc' failed.");
	}

	// ADD rmreg8,reg8
	[Test]
	public void ADD_rmreg8_reg8 ()
	{
		// ADD BH, DH
		// ADD (R8.BH, R8.DH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (R8.BH, R8.DH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x0, 0xf7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD BH, DH' failed.");
	}

	// ADD rmreg16,reg16
	[Test]
	public void ADD_rmreg16_reg16 ()
	{
		// ADD CX, AX
		// ADD (R16.CX, R16.AX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (R16.CX, R16.AX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x1, 0xc1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD CX, AX' failed.");
	}

	// ADD rmreg32,reg32
	[Test]
	public void ADD_rmreg32_reg32 ()
	{
		// ADD EBP, EBP
		// ADD (R32.EBP, R32.EBP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (R32.EBP, R32.EBP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x1, 0xed };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD EBP, EBP' failed.");
	}

	// ADD rmreg8,imm8
	[Test]
	public void ADD_rmreg8_imm8 ()
	{
		// ADD BH, 0x4
		// ADD (R8.BH, 0x4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (R8.BH, 0x4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x80, 0xc7, 0x4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD BH, 0x4' failed.");
	}

	// ADD rmreg16,imm16
	[Test]
	public void ADD_rmreg16_imm16 ()
	{
		// ADD BP, 0xd5c
		// ADD (R16.BP, 0xd5c)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (R16.BP, 0xd5c);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x81, 0xc5, 0x5c, 0xd };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD BP, 0xd5c' failed.");
	}

	// ADD rmreg32,imm32
	[Test]
	public void ADD_rmreg32_imm32 ()
	{
		// ADD ESP, 0x54b85b5
		// ADD (R32.ESP, 0x54b85b5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (R32.ESP, 0x54b85b5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x81, 0xc4, 0xb5, 0x85, 0x4b, 0x5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD ESP, 0x54b85b5' failed.");
	}

	// ADD rmreg16,imm8
	[Test]
	public void ADD_rmreg16_imm8 ()
	{
		// ADD SP, 0x9
		// ADD (R16.SP, 0x9)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (R16.SP, 0x9);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x83, 0xc4, 0x9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD SP, 0x9' failed.");
	}

	// ADD rmreg32,imm8
	[Test]
	public void ADD_rmreg32_imm8 ()
	{
		// ADD ESI, 0x9
		// ADD (R32.ESI, 0x9)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ADD (R32.ESI, 0x9);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x83, 0xc6, 0x9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ADD ESI, 0x9' failed.");
	}

	// AND mem8,reg8
	[Test]
	public void AND_mem8_reg8 ()
	{
		// AND [EDI*4 + 0x12345678], CH
		// AND (new ByteMemory(null, null, R32.EDI, 2, 0x12345678), R8.CH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (new ByteMemory (null, null, R32.EDI, 2, 0x12345678), R8.CH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x20, 0x2c, 0xbd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND [EDI*4 + 0x12345678], CH' failed.");
	}

	// AND mem16,reg16
	[Test]
	public void AND_mem16_reg16 ()
	{
		// AND [EBX + ESI*4 + 0x12345678], AX
		// AND (new WordMemory(null, R32.EBX, R32.ESI, 2, 0x12345678), R16.AX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (new WordMemory (null, R32.EBX, R32.ESI, 2, 0x12345678), R16.AX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x21, 0x84, 0xb3, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND [EBX + ESI*4 + 0x12345678], AX' failed.");
	}

	// AND mem32,reg32
	[Test]
	public void AND_mem32_reg32 ()
	{
		// AND [0x12345678], EBX
		// AND (new DWordMemory(null, null, null, 0, 0x12345678), R32.EBX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (new DWordMemory (null, null, null, 0, 0x12345678), R32.EBX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x21, 0x1d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND [0x12345678], EBX' failed.");
	}

	// AND reg8,mem8
	[Test]
	public void AND_reg8_mem8 ()
	{
		// AND CL, [ES:EDX + ESI*1 + 0x12345678]
		// AND (R8.CL, new ByteMemory(Seg.ES, R32.EDX, R32.ESI, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (R8.CL, new ByteMemory (Seg.ES, R32.EDX, R32.ESI, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0x22, 0x8c, 0x32, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND CL, [ES:EDX + ESI*1 + 0x12345678]' failed.");
	}

	// AND reg16,mem16
	[Test]
	public void AND_reg16_mem16 ()
	{
		// AND SI, [EBP]
		// AND (R16.SI, new WordMemory(null, R32.EBP, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (R16.SI, new WordMemory (null, R32.EBP, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x23, 0x75, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND SI, [EBP]' failed.");
	}

	// AND reg32,mem32
	[Test]
	public void AND_reg32_mem32 ()
	{
		// AND EDX, [CS:0x12345678]
		// AND (R32.EDX, new DWordMemory(Seg.CS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (R32.EDX, new DWordMemory (Seg.CS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0x23, 0x15, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND EDX, [CS:0x12345678]' failed.");
	}

	// AND mem8,imm8
	[Test]
	public void AND_mem8_imm8 ()
	{
		// AND Byte [0x12345678], 0x4
		// AND (new ByteMemory(null, null, null, 0, 0x12345678), 0x4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (new ByteMemory (null, null, null, 0, 0x12345678), 0x4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x80, 0x25, 0x78, 0x56, 0x34, 0x12, 0x4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND Byte [0x12345678], 0x4' failed.");
	}

	// AND mem16,imm16
	[Test]
	public void AND_mem16_imm16 ()
	{
		// AND Word [EDX], 0x72e
		// AND (new WordMemory(null, R32.EDX, null, 0), 0x72e)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (new WordMemory (null, R32.EDX, null, 0), 0x72e);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x81, 0x22, 0x2e, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND Word [EDX], 0x72e' failed.");
	}

	// AND mem32,imm32
	[Test]
	public void AND_mem32_imm32 ()
	{
		// AND DWord [CS:0x12345678], 0x53735c3
		// AND (new DWordMemory(Seg.CS, null, null, 0, 0x12345678), 0x53735c3)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (new DWordMemory (Seg.CS, null, null, 0, 0x12345678), 0x53735c3);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0x81, 0x25, 0x78, 0x56, 0x34, 0x12, 0xc3, 0x35, 0x37, 0x5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND DWord [CS:0x12345678], 0x53735c3' failed.");
	}

	// AND mem16,imm8
	[Test]
	public void AND_mem16_imm8 ()
	{
		// AND Word [ECX + ECX*8 + 0x12345678], 0x0
		// AND (new WordMemory(null, R32.ECX, R32.ECX, 3, 0x12345678), 0x0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (new WordMemory (null, R32.ECX, R32.ECX, 3, 0x12345678), 0x0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x83, 0xa4, 0xc9, 0x78, 0x56, 0x34, 0x12, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND Word [ECX + ECX*8 + 0x12345678], 0x0' failed.");
	}

	// AND mem32,imm8
	[Test]
	public void AND_mem32_imm8 ()
	{
		// AND DWord [EAX + ECX*8 + 0x12345678], 0xc
		// AND (new DWordMemory(null, R32.EAX, R32.ECX, 3, 0x12345678), 0xc)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (new DWordMemory (null, R32.EAX, R32.ECX, 3, 0x12345678), 0xc);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x83, 0xa4, 0xc8, 0x78, 0x56, 0x34, 0x12, 0xc };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND DWord [EAX + ECX*8 + 0x12345678], 0xc' failed.");
	}

	// AND rmreg8,reg8
	[Test]
	public void AND_rmreg8_reg8 ()
	{
		// AND CH, CH
		// AND (R8.CH, R8.CH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (R8.CH, R8.CH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x20, 0xed };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND CH, CH' failed.");
	}

	// AND rmreg16,reg16
	[Test]
	public void AND_rmreg16_reg16 ()
	{
		// AND SP, DX
		// AND (R16.SP, R16.DX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (R16.SP, R16.DX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x21, 0xd4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND SP, DX' failed.");
	}

	// AND rmreg32,reg32
	[Test]
	public void AND_rmreg32_reg32 ()
	{
		// AND EAX, EAX
		// AND (R32.EAX, R32.EAX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (R32.EAX, R32.EAX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x21, 0xc0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND EAX, EAX' failed.");
	}

	// AND rmreg8,imm8
	[Test]
	public void AND_rmreg8_imm8 ()
	{
		// AND CH, 0x8
		// AND (R8.CH, 0x8)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (R8.CH, 0x8);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x80, 0xe5, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND CH, 0x8' failed.");
	}

	// AND rmreg16,imm16
	[Test]
	public void AND_rmreg16_imm16 ()
	{
		// AND BP, 0x216
		// AND (R16.BP, 0x216)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (R16.BP, 0x216);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x81, 0xe5, 0x16, 0x2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND BP, 0x216' failed.");
	}

	// AND rmreg32,imm32
	[Test]
	public void AND_rmreg32_imm32 ()
	{
		// AND EBP, 0x3a93244
		// AND (R32.EBP, 0x3a93244)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (R32.EBP, 0x3a93244);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x81, 0xe5, 0x44, 0x32, 0xa9, 0x3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND EBP, 0x3a93244' failed.");
	}

	// AND rmreg16,imm8
	[Test]
	public void AND_rmreg16_imm8 ()
	{
		// AND DX, 0x9
		// AND (R16.DX, 0x9)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (R16.DX, 0x9);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x83, 0xe2, 0x9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND DX, 0x9' failed.");
	}

	// AND rmreg32,imm8
	[Test]
	public void AND_rmreg32_imm8 ()
	{
		// AND EDX, 0xa
		// AND (R32.EDX, 0xa)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.AND (R32.EDX, 0xa);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x83, 0xe2, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'AND EDX, 0xa' failed.");
	}

	// ARPL mem16,reg16
	[Test]
	public void ARPL_mem16_reg16 ()
	{
		// ARPL [GS:EBX*2 + 0x12345678], DI
		// ARPL (new WordMemory(Seg.GS, null, R32.EBX, 1, 0x12345678), R16.DI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ARPL (new WordMemory (Seg.GS, null, R32.EBX, 1, 0x12345678), R16.DI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0x63, 0xbc, 0x1b, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ARPL [GS:EBX*2 + 0x12345678], DI' failed.");
	}

	// ARPL rmreg16,reg16
	[Test]
	public void ARPL_rmreg16_reg16 ()
	{
		// ARPL SP, AX
		// ARPL (R16.SP, R16.AX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ARPL (R16.SP, R16.AX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x63, 0xc4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ARPL SP, AX' failed.");
	}

	// BOUND reg16,mem
	[Test]
	public void BOUND_reg16_mem ()
	{
		// BOUND AX, [ES:ESI*4 + 0x12345678]
		// BOUND (R16.AX, new DWordMemory(Seg.ES, null, R32.ESI, 2, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BOUND (R16.AX, new DWordMemory (Seg.ES, null, R32.ESI, 2, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0x66, 0x62, 0x4, 0xb5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BOUND AX, [ES:ESI*4 + 0x12345678]' failed.");
	}

	// BOUND reg32,mem
	[Test]
	public void BOUND_reg32_mem ()
	{
		// BOUND EBP, [EBP + EBP*2]
		// BOUND (R32.EBP, new DWordMemory(null, R32.EBP, R32.EBP, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BOUND (R32.EBP, new DWordMemory (null, R32.EBP, R32.EBP, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x62, 0x6c, 0x6d, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BOUND EBP, [EBP + EBP*2]' failed.");
	}

	// BSF reg16,mem16
	[Test]
	public void BSF_reg16_mem16 ()
	{
		// BSF SI, [ESI + 0x12345678]
		// BSF (R16.SI, new WordMemory(null, R32.ESI, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BSF (R16.SI, new WordMemory (null, R32.ESI, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xbc, 0xb6, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BSF SI, [ESI + 0x12345678]' failed.");
	}

	// BSF reg32,mem32
	[Test]
	public void BSF_reg32_mem32 ()
	{
		// BSF ESI, [ES:0x12345678]
		// BSF (R32.ESI, new DWordMemory(Seg.ES, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BSF (R32.ESI, new DWordMemory (Seg.ES, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xf, 0xbc, 0x35, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BSF ESI, [ES:0x12345678]' failed.");
	}

	// BSF reg16,rmreg16
	[Test]
	public void BSF_reg16_rmreg16 ()
	{
		// BSF SP, BX
		// BSF (R16.SP, R16.BX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BSF (R16.SP, R16.BX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xbc, 0xe3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BSF SP, BX' failed.");
	}

	// BSF reg32,rmreg32
	[Test]
	public void BSF_reg32_rmreg32 ()
	{
		// BSF EBX, ESI
		// BSF (R32.EBX, R32.ESI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BSF (R32.EBX, R32.ESI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xbc, 0xde };
		Assert.IsTrue (CompareData (memoryStream, target), "'BSF EBX, ESI' failed.");
	}

	// BSR reg16,mem16
	[Test]
	public void BSR_reg16_mem16 ()
	{
		// BSR DX, [FS:ECX + 0x12345678]
		// BSR (R16.DX, new WordMemory(Seg.FS, R32.ECX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BSR (R16.DX, new WordMemory (Seg.FS, R32.ECX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0x66, 0xf, 0xbd, 0x91, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BSR DX, [FS:ECX + 0x12345678]' failed.");
	}

	// BSR reg32,mem32
	[Test]
	public void BSR_reg32_mem32 ()
	{
		// BSR ESI, [EBX*4]
		// BSR (R32.ESI, new DWordMemory(null, null, R32.EBX, 2))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BSR (R32.ESI, new DWordMemory (null, null, R32.EBX, 2));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xbd, 0x34, 0x9d, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BSR ESI, [EBX*4]' failed.");
	}

	// BSR reg16,rmreg16
	[Test]
	public void BSR_reg16_rmreg16 ()
	{
		// BSR SI, AX
		// BSR (R16.SI, R16.AX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BSR (R16.SI, R16.AX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xbd, 0xf0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BSR SI, AX' failed.");
	}

	// BSR reg32,rmreg32
	[Test]
	public void BSR_reg32_rmreg32 ()
	{
		// BSR EDX, EDI
		// BSR (R32.EDX, R32.EDI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BSR (R32.EDX, R32.EDI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xbd, 0xd7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BSR EDX, EDI' failed.");
	}

	// BSWAP reg32
	[Test]
	public void BSWAP_reg32 ()
	{
		// BSWAP EBX
		// BSWAP (R32.EBX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BSWAP (R32.EBX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xcb };
		Assert.IsTrue (CompareData (memoryStream, target), "'BSWAP EBX' failed.");
	}

	// BT mem16,reg16
	[Test]
	public void BT_mem16_reg16 ()
	{
		// BT [ESP + EBP*8], AX
		// BT (new WordMemory(null, R32.ESP, R32.EBP, 3), R16.AX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BT (new WordMemory (null, R32.ESP, R32.EBP, 3), R16.AX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xa3, 0x4, 0xec };
		Assert.IsTrue (CompareData (memoryStream, target), "'BT [ESP + EBP*8], AX' failed.");
	}

	// BT mem32,reg32
	[Test]
	public void BT_mem32_reg32 ()
	{
		// BT [GS:EAX + 0x12345678], ECX
		// BT (new DWordMemory(Seg.GS, R32.EAX, null, 0, 0x12345678), R32.ECX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BT (new DWordMemory (Seg.GS, R32.EAX, null, 0, 0x12345678), R32.ECX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xf, 0xa3, 0x88, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BT [GS:EAX + 0x12345678], ECX' failed.");
	}

	// BT mem16,imm8
	[Test]
	public void BT_mem16_imm8 ()
	{
		// BT Word [0x12345678], 0x6
		// BT (new WordMemory(null, null, null, 0, 0x12345678), 0x6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BT (new WordMemory (null, null, null, 0, 0x12345678), 0x6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xba, 0x25, 0x78, 0x56, 0x34, 0x12, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BT Word [0x12345678], 0x6' failed.");
	}

	// BT mem32,imm8
	[Test]
	public void BT_mem32_imm8 ()
	{
		// BT DWord [ESP + ECX*2 + 0x12345678], 0x0
		// BT (new DWordMemory(null, R32.ESP, R32.ECX, 1, 0x12345678), 0x0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BT (new DWordMemory (null, R32.ESP, R32.ECX, 1, 0x12345678), 0x0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xba, 0xa4, 0x4c, 0x78, 0x56, 0x34, 0x12, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BT DWord [ESP + ECX*2 + 0x12345678], 0x0' failed.");
	}

	// BT rmreg16,reg16
	[Test]
	public void BT_rmreg16_reg16 ()
	{
		// BT DI, CX
		// BT (R16.DI, R16.CX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BT (R16.DI, R16.CX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xa3, 0xcf };
		Assert.IsTrue (CompareData (memoryStream, target), "'BT DI, CX' failed.");
	}

	// BT rmreg32,reg32
	[Test]
	public void BT_rmreg32_reg32 ()
	{
		// BT EBX, EDX
		// BT (R32.EBX, R32.EDX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BT (R32.EBX, R32.EDX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xa3, 0xd3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BT EBX, EDX' failed.");
	}

	// BT rmreg16,imm8
	[Test]
	public void BT_rmreg16_imm8 ()
	{
		// BT SI, 0xf
		// BT (R16.SI, 0xf)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BT (R16.SI, 0xf);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xba, 0xe6, 0xf };
		Assert.IsTrue (CompareData (memoryStream, target), "'BT SI, 0xf' failed.");
	}

	// BT rmreg32,imm8
	[Test]
	public void BT_rmreg32_imm8 ()
	{
		// BT EAX, 0xd
		// BT (R32.EAX, 0xd)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BT (R32.EAX, 0xd);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xba, 0xe0, 0xd };
		Assert.IsTrue (CompareData (memoryStream, target), "'BT EAX, 0xd' failed.");
	}

	// BTC mem16,reg16
	[Test]
	public void BTC_mem16_reg16 ()
	{
		// BTC [EBP + EDX*4 + 0x12345678], DI
		// BTC (new WordMemory(null, R32.EBP, R32.EDX, 2, 0x12345678), R16.DI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTC (new WordMemory (null, R32.EBP, R32.EDX, 2, 0x12345678), R16.DI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xbb, 0xbc, 0x95, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTC [EBP + EDX*4 + 0x12345678], DI' failed.");
	}

	// BTC mem32,reg32
	[Test]
	public void BTC_mem32_reg32 ()
	{
		// BTC [EDX + ESI*2 + 0x12345678], EBP
		// BTC (new DWordMemory(null, R32.EDX, R32.ESI, 1, 0x12345678), R32.EBP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTC (new DWordMemory (null, R32.EDX, R32.ESI, 1, 0x12345678), R32.EBP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xbb, 0xac, 0x72, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTC [EDX + ESI*2 + 0x12345678], EBP' failed.");
	}

	// BTC mem16,imm8
	[Test]
	public void BTC_mem16_imm8 ()
	{
		// BTC Word [EAX + EBX*1], 0xb
		// BTC (new WordMemory(null, R32.EAX, R32.EBX, 0), 0xb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTC (new WordMemory (null, R32.EAX, R32.EBX, 0), 0xb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xba, 0x3c, 0x18, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTC Word [EAX + EBX*1], 0xb' failed.");
	}

	// BTC mem32,imm8
	[Test]
	public void BTC_mem32_imm8 ()
	{
		// BTC DWord [FS:EDX*1 + 0x12345678], 0xf
		// BTC (new DWordMemory(Seg.FS, null, R32.EDX, 0, 0x12345678), 0xf)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTC (new DWordMemory (Seg.FS, null, R32.EDX, 0, 0x12345678), 0xf);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xf, 0xba, 0xba, 0x78, 0x56, 0x34, 0x12, 0xf };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTC DWord [FS:EDX*1 + 0x12345678], 0xf' failed.");
	}

	// BTC rmreg16,reg16
	[Test]
	public void BTC_rmreg16_reg16 ()
	{
		// BTC DX, BP
		// BTC (R16.DX, R16.BP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTC (R16.DX, R16.BP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xbb, 0xea };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTC DX, BP' failed.");
	}

	// BTC rmreg32,reg32
	[Test]
	public void BTC_rmreg32_reg32 ()
	{
		// BTC EBP, EBX
		// BTC (R32.EBP, R32.EBX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTC (R32.EBP, R32.EBX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xbb, 0xdd };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTC EBP, EBX' failed.");
	}

	// BTC rmreg16,imm8
	[Test]
	public void BTC_rmreg16_imm8 ()
	{
		// BTC CX, 0x0
		// BTC (R16.CX, 0x0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTC (R16.CX, 0x0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xba, 0xf9, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTC CX, 0x0' failed.");
	}

	// BTC rmreg32,imm8
	[Test]
	public void BTC_rmreg32_imm8 ()
	{
		// BTC EBX, 0x7
		// BTC (R32.EBX, 0x7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTC (R32.EBX, 0x7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xba, 0xfb, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTC EBX, 0x7' failed.");
	}

	// BTR mem16,reg16
	[Test]
	public void BTR_mem16_reg16 ()
	{
		// BTR [SS:EAX + 0x12345678], CX
		// BTR (new WordMemory(Seg.SS, R32.EAX, null, 0, 0x12345678), R16.CX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTR (new WordMemory (Seg.SS, R32.EAX, null, 0, 0x12345678), R16.CX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0x66, 0xf, 0xb3, 0x88, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTR [SS:EAX + 0x12345678], CX' failed.");
	}

	// BTR mem32,reg32
	[Test]
	public void BTR_mem32_reg32 ()
	{
		// BTR [EBX + 0x12345678], EDX
		// BTR (new DWordMemory(null, R32.EBX, null, 0, 0x12345678), R32.EDX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTR (new DWordMemory (null, R32.EBX, null, 0, 0x12345678), R32.EDX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xb3, 0x93, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTR [EBX + 0x12345678], EDX' failed.");
	}

	// BTR mem16,imm8
	[Test]
	public void BTR_mem16_imm8 ()
	{
		// BTR Word [EDI*4 + 0x12345678], 0xf
		// BTR (new WordMemory(null, null, R32.EDI, 2, 0x12345678), 0xf)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTR (new WordMemory (null, null, R32.EDI, 2, 0x12345678), 0xf);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xba, 0x34, 0xbd, 0x78, 0x56, 0x34, 0x12, 0xf };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTR Word [EDI*4 + 0x12345678], 0xf' failed.");
	}

	// BTR mem32,imm8
	[Test]
	public void BTR_mem32_imm8 ()
	{
		// BTR DWord [SS:EBX + ECX*1 + 0x12345678], 0x0
		// BTR (new DWordMemory(Seg.SS, R32.EBX, R32.ECX, 0, 0x12345678), 0x0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTR (new DWordMemory (Seg.SS, R32.EBX, R32.ECX, 0, 0x12345678), 0x0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0xf, 0xba, 0xb4, 0xb, 0x78, 0x56, 0x34, 0x12, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTR DWord [SS:EBX + ECX*1 + 0x12345678], 0x0' failed.");
	}

	// BTR rmreg16,reg16
	[Test]
	public void BTR_rmreg16_reg16 ()
	{
		// BTR BP, SI
		// BTR (R16.BP, R16.SI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTR (R16.BP, R16.SI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xb3, 0xf5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTR BP, SI' failed.");
	}

	// BTR rmreg32,reg32
	[Test]
	public void BTR_rmreg32_reg32 ()
	{
		// BTR EBX, ECX
		// BTR (R32.EBX, R32.ECX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTR (R32.EBX, R32.ECX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xb3, 0xcb };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTR EBX, ECX' failed.");
	}

	// BTR rmreg16,imm8
	[Test]
	public void BTR_rmreg16_imm8 ()
	{
		// BTR SP, 0xb
		// BTR (R16.SP, 0xb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTR (R16.SP, 0xb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xba, 0xf4, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTR SP, 0xb' failed.");
	}

	// BTR rmreg32,imm8
	[Test]
	public void BTR_rmreg32_imm8 ()
	{
		// BTR EDI, 0xb
		// BTR (R32.EDI, 0xb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTR (R32.EDI, 0xb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xba, 0xf7, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTR EDI, 0xb' failed.");
	}

	// BTS mem16,reg16
	[Test]
	public void BTS_mem16_reg16 ()
	{
		// BTS [GS:0x12345678], BX
		// BTS (new WordMemory(Seg.GS, null, null, 0, 0x12345678), R16.BX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTS (new WordMemory (Seg.GS, null, null, 0, 0x12345678), R16.BX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0x66, 0xf, 0xab, 0x1d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTS [GS:0x12345678], BX' failed.");
	}

	// BTS mem32,reg32
	[Test]
	public void BTS_mem32_reg32 ()
	{
		// BTS [ES:0x12345678], ESP
		// BTS (new DWordMemory(Seg.ES, null, null, 0, 0x12345678), R32.ESP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTS (new DWordMemory (Seg.ES, null, null, 0, 0x12345678), R32.ESP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xf, 0xab, 0x25, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTS [ES:0x12345678], ESP' failed.");
	}

	// BTS mem16,imm8
	[Test]
	public void BTS_mem16_imm8 ()
	{
		// BTS Word [EDX + EDX*4 + 0x12345678], 0xe
		// BTS (new WordMemory(null, R32.EDX, R32.EDX, 2, 0x12345678), 0xe)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTS (new WordMemory (null, R32.EDX, R32.EDX, 2, 0x12345678), 0xe);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xba, 0xac, 0x92, 0x78, 0x56, 0x34, 0x12, 0xe };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTS Word [EDX + EDX*4 + 0x12345678], 0xe' failed.");
	}

	// BTS mem32,imm8
	[Test]
	public void BTS_mem32_imm8 ()
	{
		// BTS DWord [GS:0x12345678], 0x4
		// BTS (new DWordMemory(Seg.GS, null, null, 0, 0x12345678), 0x4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTS (new DWordMemory (Seg.GS, null, null, 0, 0x12345678), 0x4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xf, 0xba, 0x2d, 0x78, 0x56, 0x34, 0x12, 0x4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTS DWord [GS:0x12345678], 0x4' failed.");
	}

	// BTS rmreg16,reg16
	[Test]
	public void BTS_rmreg16_reg16 ()
	{
		// BTS DI, SP
		// BTS (R16.DI, R16.SP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTS (R16.DI, R16.SP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xab, 0xe7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTS DI, SP' failed.");
	}

	// BTS rmreg32,reg32
	[Test]
	public void BTS_rmreg32_reg32 ()
	{
		// BTS EDI, ECX
		// BTS (R32.EDI, R32.ECX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTS (R32.EDI, R32.ECX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xab, 0xcf };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTS EDI, ECX' failed.");
	}

	// BTS rmreg16,imm8
	[Test]
	public void BTS_rmreg16_imm8 ()
	{
		// BTS AX, 0x6
		// BTS (R16.AX, 0x6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTS (R16.AX, 0x6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xba, 0xe8, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTS AX, 0x6' failed.");
	}

	// BTS rmreg32,imm8
	[Test]
	public void BTS_rmreg32_imm8 ()
	{
		// BTS ECX, 0xb
		// BTS (R32.ECX, 0xb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.BTS (R32.ECX, 0xb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xba, 0xe9, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'BTS ECX, 0xb' failed.");
	}

	// CALL imm
	[Test]
	public void CALL_imm ()
	{
		// CALL 0xab1a16f
		// CALL (0xab1a16f)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CALL (0xab1a16f);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xe8, 0x6a, 0xa1, 0xb1, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'CALL 0xab1a16f' failed.");
	}

	// CALL imm16:imm16
	[Test]
	public void CALL_imm16_imm16 ()
	{
		// CALL WORD 0x736: 0xcff
		// CALL (0x736, 0xcff)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CALL (0x736, 0xcff);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x9a, 0xff, 0xc, 0x36, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CALL WORD 0x736: 0xcff' failed.");
	}

	// CALL imm16:imm32
	[Test]
	public void CALL_imm16_imm32 ()
	{
		// CALL 0x8e2: 0xbeeca0e
		// CALL (0x8e2, 0xbeeca0e)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CALL (0x8e2, 0xbeeca0e);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x9a, 0xe, 0xca, 0xee, 0xb, 0xe2, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CALL 0x8e2: 0xbeeca0e' failed.");
	}

	// CALL FAR mem16
	[Test]
	public void CALL_FAR_mem16 ()
	{
		// CALL FAR Word [EBP + ECX*1]
		// CALL_FAR (new WordMemory(null, R32.EBP, R32.ECX, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CALL_FAR (new WordMemory (null, R32.EBP, R32.ECX, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xff, 0x5c, 0xd, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CALL FAR Word [EBP + ECX*1]' failed.");
	}

	// CALL FAR mem32
	[Test]
	public void CALL_FAR_mem32 ()
	{
		// CALL FAR DWord [EDI*4 + 0x12345678]
		// CALL_FAR (new DWordMemory(null, null, R32.EDI, 2, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CALL_FAR (new DWordMemory (null, null, R32.EDI, 2, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xff, 0x1c, 0xbd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CALL FAR DWord [EDI*4 + 0x12345678]' failed.");
	}

	// CALL mem16
	[Test]
	public void CALL_mem16 ()
	{
		// CALL Word [CS:0x12345678]
		// CALL (new WordMemory(Seg.CS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CALL (new WordMemory (Seg.CS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0x66, 0xff, 0x15, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CALL Word [CS:0x12345678]' failed.");
	}

	// CALL mem32
	[Test]
	public void CALL_mem32 ()
	{
		// CALL DWord [SS:0x12345678]
		// CALL (new DWordMemory(Seg.SS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CALL (new DWordMemory (Seg.SS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0xff, 0x15, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CALL DWord [SS:0x12345678]' failed.");
	}

	// CALL rmreg16
	[Test]
	public void CALL_rmreg16 ()
	{
		// CALL BX
		// CALL (R16.BX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CALL (R16.BX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xff, 0xd3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CALL BX' failed.");
	}

	// CALL rmreg32
	[Test]
	public void CALL_rmreg32 ()
	{
		// CALL ECX
		// CALL (R32.ECX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CALL (R32.ECX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xff, 0xd1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CALL ECX' failed.");
	}

	// CBW 
	[Test]
	public void CBW ()
	{
		// CBW
		// CBW ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CBW ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x98 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CBW' failed.");
	}

	// CDQ 
	[Test]
	public void CDQ ()
	{
		// CDQ
		// CDQ ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CDQ ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x99 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CDQ' failed.");
	}

	// CLC 
	[Test]
	public void CLC ()
	{
		// CLC
		// CLC ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CLC ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CLC' failed.");
	}

	// CLD 
	[Test]
	public void CLD ()
	{
		// CLD
		// CLD ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CLD ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xfc };
		Assert.IsTrue (CompareData (memoryStream, target), "'CLD' failed.");
	}

	// CLFLUSH mem
	[Test]
	public void CLFLUSH_mem ()
	{
		// CLFLUSH [EDI + EBP*1]
		// CLFLUSH (new DWordMemory(null, R32.EDI, R32.EBP, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CLFLUSH (new DWordMemory (null, R32.EDI, R32.EBP, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xae, 0x3c, 0x2f };
		Assert.IsTrue (CompareData (memoryStream, target), "'CLFLUSH [EDI + EBP*1]' failed.");
	}

	// CLI 
	[Test]
	public void CLI ()
	{
		// CLI
		// CLI ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CLI ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xfa };
		Assert.IsTrue (CompareData (memoryStream, target), "'CLI' failed.");
	}

	// CLTS 
	[Test]
	public void CLTS ()
	{
		// CLTS
		// CLTS ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CLTS ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CLTS' failed.");
	}

	// CMC 
	[Test]
	public void CMC ()
	{
		// CMC
		// CMC ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMC ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMC' failed.");
	}

	// CMOVA reg16,mem16
	[Test]
	public void CMOVA_reg16_mem16 ()
	{
		// CMOVA BP, [EDI*8]
		// CMOVA (R16.BP, new WordMemory(null, null, R32.EDI, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVA (R16.BP, new WordMemory (null, null, R32.EDI, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x47, 0x2c, 0xfd, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVA BP, [EDI*8]' failed.");
	}

	// CMOVA reg32,mem32
	[Test]
	public void CMOVA_reg32_mem32 ()
	{
		// CMOVA EBP, [0x12345678]
		// CMOVA (R32.EBP, new DWordMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVA (R32.EBP, new DWordMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x47, 0x2d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVA EBP, [0x12345678]' failed.");
	}

	// CMOVA reg16,rmreg16
	[Test]
	public void CMOVA_reg16_rmreg16 ()
	{
		// CMOVA DI, SP
		// CMOVA (R16.DI, R16.SP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVA (R16.DI, R16.SP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x47, 0xfc };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVA DI, SP' failed.");
	}

	// CMOVA reg32,rmreg32
	[Test]
	public void CMOVA_reg32_rmreg32 ()
	{
		// CMOVA EBP, EAX
		// CMOVA (R32.EBP, R32.EAX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVA (R32.EBP, R32.EAX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x47, 0xe8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVA EBP, EAX' failed.");
	}

	// CMOVAE reg16,mem16
	[Test]
	public void CMOVAE_reg16_mem16 ()
	{
		// CMOVAE DI, [FS:0x12345678]
		// CMOVAE (R16.DI, new WordMemory(Seg.FS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVAE (R16.DI, new WordMemory (Seg.FS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0x66, 0xf, 0x43, 0x3d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVAE DI, [FS:0x12345678]' failed.");
	}

	// CMOVAE reg32,mem32
	[Test]
	public void CMOVAE_reg32_mem32 ()
	{
		// CMOVAE EDX, [EDX*4 + 0x12345678]
		// CMOVAE (R32.EDX, new DWordMemory(null, null, R32.EDX, 2, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVAE (R32.EDX, new DWordMemory (null, null, R32.EDX, 2, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x43, 0x14, 0x95, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVAE EDX, [EDX*4 + 0x12345678]' failed.");
	}

	// CMOVAE reg16,rmreg16
	[Test]
	public void CMOVAE_reg16_rmreg16 ()
	{
		// CMOVAE DX, SI
		// CMOVAE (R16.DX, R16.SI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVAE (R16.DX, R16.SI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x43, 0xd6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVAE DX, SI' failed.");
	}

	// CMOVAE reg32,rmreg32
	[Test]
	public void CMOVAE_reg32_rmreg32 ()
	{
		// CMOVAE EAX, EDI
		// CMOVAE (R32.EAX, R32.EDI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVAE (R32.EAX, R32.EDI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x43, 0xc7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVAE EAX, EDI' failed.");
	}

	// CMOVB reg16,mem16
	[Test]
	public void CMOVB_reg16_mem16 ()
	{
		// CMOVB SP, [EBX + 0x12345678]
		// CMOVB (R16.SP, new WordMemory(null, R32.EBX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVB (R16.SP, new WordMemory (null, R32.EBX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x42, 0xa3, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVB SP, [EBX + 0x12345678]' failed.");
	}

	// CMOVB reg32,mem32
	[Test]
	public void CMOVB_reg32_mem32 ()
	{
		// CMOVB EAX, [CS:EDI + 0x12345678]
		// CMOVB (R32.EAX, new DWordMemory(Seg.CS, R32.EDI, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVB (R32.EAX, new DWordMemory (Seg.CS, R32.EDI, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xf, 0x42, 0x87, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVB EAX, [CS:EDI + 0x12345678]' failed.");
	}

	// CMOVB reg16,rmreg16
	[Test]
	public void CMOVB_reg16_rmreg16 ()
	{
		// CMOVB DX, SI
		// CMOVB (R16.DX, R16.SI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVB (R16.DX, R16.SI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x42, 0xd6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVB DX, SI' failed.");
	}

	// CMOVB reg32,rmreg32
	[Test]
	public void CMOVB_reg32_rmreg32 ()
	{
		// CMOVB EAX, EBP
		// CMOVB (R32.EAX, R32.EBP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVB (R32.EAX, R32.EBP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x42, 0xc5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVB EAX, EBP' failed.");
	}

	// CMOVBE reg16,mem16
	[Test]
	public void CMOVBE_reg16_mem16 ()
	{
		// CMOVBE DI, [EDI + ECX*2]
		// CMOVBE (R16.DI, new WordMemory(null, R32.EDI, R32.ECX, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVBE (R16.DI, new WordMemory (null, R32.EDI, R32.ECX, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x46, 0x3c, 0x4f };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVBE DI, [EDI + ECX*2]' failed.");
	}

	// CMOVBE reg32,mem32
	[Test]
	public void CMOVBE_reg32_mem32 ()
	{
		// CMOVBE EAX, [DS:EBX + EBX*8 + 0x12345678]
		// CMOVBE (R32.EAX, new DWordMemory(Seg.DS, R32.EBX, R32.EBX, 3, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVBE (R32.EAX, new DWordMemory (Seg.DS, R32.EBX, R32.EBX, 3, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xf, 0x46, 0x84, 0xdb, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVBE EAX, [DS:EBX + EBX*8 + 0x12345678]' failed.");
	}

	// CMOVBE reg16,rmreg16
	[Test]
	public void CMOVBE_reg16_rmreg16 ()
	{
		// CMOVBE SI, BX
		// CMOVBE (R16.SI, R16.BX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVBE (R16.SI, R16.BX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x46, 0xf3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVBE SI, BX' failed.");
	}

	// CMOVBE reg32,rmreg32
	[Test]
	public void CMOVBE_reg32_rmreg32 ()
	{
		// CMOVBE ECX, EBP
		// CMOVBE (R32.ECX, R32.EBP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVBE (R32.ECX, R32.EBP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x46, 0xcd };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVBE ECX, EBP' failed.");
	}

	// CMOVC reg16,mem16
	[Test]
	public void CMOVC_reg16_mem16 ()
	{
		// CMOVC SI, [EDX + 0x12345678]
		// CMOVC (R16.SI, new WordMemory(null, R32.EDX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVC (R16.SI, new WordMemory (null, R32.EDX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x42, 0xb2, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVC SI, [EDX + 0x12345678]' failed.");
	}

	// CMOVC reg32,mem32
	[Test]
	public void CMOVC_reg32_mem32 ()
	{
		// CMOVC EBX, [EDX*2 + 0x12345678]
		// CMOVC (R32.EBX, new DWordMemory(null, null, R32.EDX, 1, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVC (R32.EBX, new DWordMemory (null, null, R32.EDX, 1, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x42, 0x9c, 0x12, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVC EBX, [EDX*2 + 0x12345678]' failed.");
	}

	// CMOVC reg16,rmreg16
	[Test]
	public void CMOVC_reg16_rmreg16 ()
	{
		// CMOVC CX, SP
		// CMOVC (R16.CX, R16.SP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVC (R16.CX, R16.SP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x42, 0xcc };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVC CX, SP' failed.");
	}

	// CMOVC reg32,rmreg32
	[Test]
	public void CMOVC_reg32_rmreg32 ()
	{
		// CMOVC EDX, ESI
		// CMOVC (R32.EDX, R32.ESI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVC (R32.EDX, R32.ESI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x42, 0xd6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVC EDX, ESI' failed.");
	}

	// CMOVE reg16,mem16
	[Test]
	public void CMOVE_reg16_mem16 ()
	{
		// CMOVE BP, [SS:0x12345678]
		// CMOVE (R16.BP, new WordMemory(Seg.SS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVE (R16.BP, new WordMemory (Seg.SS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0x66, 0xf, 0x44, 0x2d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVE BP, [SS:0x12345678]' failed.");
	}

	// CMOVE reg32,mem32
	[Test]
	public void CMOVE_reg32_mem32 ()
	{
		// CMOVE ESI, [EDI + ECX*4 + 0x12345678]
		// CMOVE (R32.ESI, new DWordMemory(null, R32.EDI, R32.ECX, 2, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVE (R32.ESI, new DWordMemory (null, R32.EDI, R32.ECX, 2, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x44, 0xb4, 0x8f, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVE ESI, [EDI + ECX*4 + 0x12345678]' failed.");
	}

	// CMOVE reg16,rmreg16
	[Test]
	public void CMOVE_reg16_rmreg16 ()
	{
		// CMOVE AX, AX
		// CMOVE (R16.AX, R16.AX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVE (R16.AX, R16.AX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x44, 0xc0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVE AX, AX' failed.");
	}

	// CMOVE reg32,rmreg32
	[Test]
	public void CMOVE_reg32_rmreg32 ()
	{
		// CMOVE EBX, EAX
		// CMOVE (R32.EBX, R32.EAX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVE (R32.EBX, R32.EAX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x44, 0xd8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVE EBX, EAX' failed.");
	}

	// CMOVG reg16,mem16
	[Test]
	public void CMOVG_reg16_mem16 ()
	{
		// CMOVG DI, [SS:0x12345678]
		// CMOVG (R16.DI, new WordMemory(Seg.SS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVG (R16.DI, new WordMemory (Seg.SS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0x66, 0xf, 0x4f, 0x3d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVG DI, [SS:0x12345678]' failed.");
	}

	// CMOVG reg32,mem32
	[Test]
	public void CMOVG_reg32_mem32 ()
	{
		// CMOVG EDX, [FS:ESP + EDX*2 + 0x12345678]
		// CMOVG (R32.EDX, new DWordMemory(Seg.FS, R32.ESP, R32.EDX, 1, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVG (R32.EDX, new DWordMemory (Seg.FS, R32.ESP, R32.EDX, 1, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xf, 0x4f, 0x94, 0x54, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVG EDX, [FS:ESP + EDX*2 + 0x12345678]' failed.");
	}

	// CMOVG reg16,rmreg16
	[Test]
	public void CMOVG_reg16_rmreg16 ()
	{
		// CMOVG BX, AX
		// CMOVG (R16.BX, R16.AX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVG (R16.BX, R16.AX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x4f, 0xd8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVG BX, AX' failed.");
	}

	// CMOVG reg32,rmreg32
	[Test]
	public void CMOVG_reg32_rmreg32 ()
	{
		// CMOVG EDX, ECX
		// CMOVG (R32.EDX, R32.ECX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVG (R32.EDX, R32.ECX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x4f, 0xd1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVG EDX, ECX' failed.");
	}

	// CMOVGE reg16,mem16
	[Test]
	public void CMOVGE_reg16_mem16 ()
	{
		// CMOVGE BP, [SS:EDX + EAX*2]
		// CMOVGE (R16.BP, new WordMemory(Seg.SS, R32.EDX, R32.EAX, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVGE (R16.BP, new WordMemory (Seg.SS, R32.EDX, R32.EAX, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0x66, 0xf, 0x4d, 0x2c, 0x42 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVGE BP, [SS:EDX + EAX*2]' failed.");
	}

	// CMOVGE reg32,mem32
	[Test]
	public void CMOVGE_reg32_mem32 ()
	{
		// CMOVGE EDX, [ES:EDI + EBP*2 + 0x12345678]
		// CMOVGE (R32.EDX, new DWordMemory(Seg.ES, R32.EDI, R32.EBP, 1, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVGE (R32.EDX, new DWordMemory (Seg.ES, R32.EDI, R32.EBP, 1, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xf, 0x4d, 0x94, 0x6f, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVGE EDX, [ES:EDI + EBP*2 + 0x12345678]' failed.");
	}

	// CMOVGE reg16,rmreg16
	[Test]
	public void CMOVGE_reg16_rmreg16 ()
	{
		// CMOVGE CX, SI
		// CMOVGE (R16.CX, R16.SI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVGE (R16.CX, R16.SI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x4d, 0xce };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVGE CX, SI' failed.");
	}

	// CMOVGE reg32,rmreg32
	[Test]
	public void CMOVGE_reg32_rmreg32 ()
	{
		// CMOVGE EBX, EDX
		// CMOVGE (R32.EBX, R32.EDX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVGE (R32.EBX, R32.EDX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x4d, 0xda };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVGE EBX, EDX' failed.");
	}

	// CMOVL reg16,mem16
	[Test]
	public void CMOVL_reg16_mem16 ()
	{
		// CMOVL DX, [ECX + EDX*8]
		// CMOVL (R16.DX, new WordMemory(null, R32.ECX, R32.EDX, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVL (R16.DX, new WordMemory (null, R32.ECX, R32.EDX, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x4c, 0x14, 0xd1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVL DX, [ECX + EDX*8]' failed.");
	}

	// CMOVL reg32,mem32
	[Test]
	public void CMOVL_reg32_mem32 ()
	{
		// CMOVL EDX, [0x12345678]
		// CMOVL (R32.EDX, new DWordMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVL (R32.EDX, new DWordMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x4c, 0x15, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVL EDX, [0x12345678]' failed.");
	}

	// CMOVL reg16,rmreg16
	[Test]
	public void CMOVL_reg16_rmreg16 ()
	{
		// CMOVL BX, BP
		// CMOVL (R16.BX, R16.BP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVL (R16.BX, R16.BP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x4c, 0xdd };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVL BX, BP' failed.");
	}

	// CMOVL reg32,rmreg32
	[Test]
	public void CMOVL_reg32_rmreg32 ()
	{
		// CMOVL EDX, EAX
		// CMOVL (R32.EDX, R32.EAX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVL (R32.EDX, R32.EAX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x4c, 0xd0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVL EDX, EAX' failed.");
	}

	// CMOVLE reg16,mem16
	[Test]
	public void CMOVLE_reg16_mem16 ()
	{
		// CMOVLE AX, [DS:0x12345678]
		// CMOVLE (R16.AX, new WordMemory(Seg.DS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVLE (R16.AX, new WordMemory (Seg.DS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x66, 0xf, 0x4e, 0x5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVLE AX, [DS:0x12345678]' failed.");
	}

	// CMOVLE reg32,mem32
	[Test]
	public void CMOVLE_reg32_mem32 ()
	{
		// CMOVLE EDX, [EBP + 0x12345678]
		// CMOVLE (R32.EDX, new DWordMemory(null, R32.EBP, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVLE (R32.EDX, new DWordMemory (null, R32.EBP, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x4e, 0x95, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVLE EDX, [EBP + 0x12345678]' failed.");
	}

	// CMOVLE reg16,rmreg16
	[Test]
	public void CMOVLE_reg16_rmreg16 ()
	{
		// CMOVLE CX, BP
		// CMOVLE (R16.CX, R16.BP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVLE (R16.CX, R16.BP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x4e, 0xcd };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVLE CX, BP' failed.");
	}

	// CMOVLE reg32,rmreg32
	[Test]
	public void CMOVLE_reg32_rmreg32 ()
	{
		// CMOVLE ESP, EAX
		// CMOVLE (R32.ESP, R32.EAX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVLE (R32.ESP, R32.EAX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x4e, 0xe0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVLE ESP, EAX' failed.");
	}

	// CMOVNA reg16,mem16
	[Test]
	public void CMOVNA_reg16_mem16 ()
	{
		// CMOVNA CX, [EDX*8 + 0x12345678]
		// CMOVNA (R16.CX, new WordMemory(null, null, R32.EDX, 3, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNA (R16.CX, new WordMemory (null, null, R32.EDX, 3, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x46, 0xc, 0xd5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNA CX, [EDX*8 + 0x12345678]' failed.");
	}

	// CMOVNA reg32,mem32
	[Test]
	public void CMOVNA_reg32_mem32 ()
	{
		// CMOVNA EBX, [CS:EBP + EDX*1 + 0x12345678]
		// CMOVNA (R32.EBX, new DWordMemory(Seg.CS, R32.EBP, R32.EDX, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNA (R32.EBX, new DWordMemory (Seg.CS, R32.EBP, R32.EDX, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xf, 0x46, 0x9c, 0x15, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNA EBX, [CS:EBP + EDX*1 + 0x12345678]' failed.");
	}

	// CMOVNA reg16,rmreg16
	[Test]
	public void CMOVNA_reg16_rmreg16 ()
	{
		// CMOVNA CX, BX
		// CMOVNA (R16.CX, R16.BX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNA (R16.CX, R16.BX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x46, 0xcb };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNA CX, BX' failed.");
	}

	// CMOVNA reg32,rmreg32
	[Test]
	public void CMOVNA_reg32_rmreg32 ()
	{
		// CMOVNA EAX, ESI
		// CMOVNA (R32.EAX, R32.ESI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNA (R32.EAX, R32.ESI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x46, 0xc6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNA EAX, ESI' failed.");
	}

	// CMOVNAE reg16,mem16
	[Test]
	public void CMOVNAE_reg16_mem16 ()
	{
		// CMOVNAE CX, [0x12345678]
		// CMOVNAE (R16.CX, new WordMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNAE (R16.CX, new WordMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x42, 0xd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNAE CX, [0x12345678]' failed.");
	}

	// CMOVNAE reg32,mem32
	[Test]
	public void CMOVNAE_reg32_mem32 ()
	{
		// CMOVNAE EDX, [ES:EDI + ESI*2]
		// CMOVNAE (R32.EDX, new DWordMemory(Seg.ES, R32.EDI, R32.ESI, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNAE (R32.EDX, new DWordMemory (Seg.ES, R32.EDI, R32.ESI, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xf, 0x42, 0x14, 0x77 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNAE EDX, [ES:EDI + ESI*2]' failed.");
	}

	// CMOVNAE reg16,rmreg16
	[Test]
	public void CMOVNAE_reg16_rmreg16 ()
	{
		// CMOVNAE SI, BP
		// CMOVNAE (R16.SI, R16.BP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNAE (R16.SI, R16.BP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x42, 0xf5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNAE SI, BP' failed.");
	}

	// CMOVNAE reg32,rmreg32
	[Test]
	public void CMOVNAE_reg32_rmreg32 ()
	{
		// CMOVNAE EBX, EBX
		// CMOVNAE (R32.EBX, R32.EBX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNAE (R32.EBX, R32.EBX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x42, 0xdb };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNAE EBX, EBX' failed.");
	}

	// CMOVNB reg16,mem16
	[Test]
	public void CMOVNB_reg16_mem16 ()
	{
		// CMOVNB BX, [EDX*1]
		// CMOVNB (R16.BX, new WordMemory(null, null, R32.EDX, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNB (R16.BX, new WordMemory (null, null, R32.EDX, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x43, 0x1a };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNB BX, [EDX*1]' failed.");
	}

	// CMOVNB reg32,mem32
	[Test]
	public void CMOVNB_reg32_mem32 ()
	{
		// CMOVNB EAX, [CS:EBX]
		// CMOVNB (R32.EAX, new DWordMemory(Seg.CS, R32.EBX, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNB (R32.EAX, new DWordMemory (Seg.CS, R32.EBX, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xf, 0x43, 0x3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNB EAX, [CS:EBX]' failed.");
	}

	// CMOVNB reg16,rmreg16
	[Test]
	public void CMOVNB_reg16_rmreg16 ()
	{
		// CMOVNB DX, CX
		// CMOVNB (R16.DX, R16.CX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNB (R16.DX, R16.CX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x43, 0xd1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNB DX, CX' failed.");
	}

	// CMOVNB reg32,rmreg32
	[Test]
	public void CMOVNB_reg32_rmreg32 ()
	{
		// CMOVNB EDX, ECX
		// CMOVNB (R32.EDX, R32.ECX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNB (R32.EDX, R32.ECX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x43, 0xd1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNB EDX, ECX' failed.");
	}

	// CMOVNBE reg16,mem16
	[Test]
	public void CMOVNBE_reg16_mem16 ()
	{
		// CMOVNBE DI, [GS:0x12345678]
		// CMOVNBE (R16.DI, new WordMemory(Seg.GS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNBE (R16.DI, new WordMemory (Seg.GS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0x66, 0xf, 0x47, 0x3d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNBE DI, [GS:0x12345678]' failed.");
	}

	// CMOVNBE reg32,mem32
	[Test]
	public void CMOVNBE_reg32_mem32 ()
	{
		// CMOVNBE ESP, [SS:0x12345678]
		// CMOVNBE (R32.ESP, new DWordMemory(Seg.SS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNBE (R32.ESP, new DWordMemory (Seg.SS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0xf, 0x47, 0x25, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNBE ESP, [SS:0x12345678]' failed.");
	}

	// CMOVNBE reg16,rmreg16
	[Test]
	public void CMOVNBE_reg16_rmreg16 ()
	{
		// CMOVNBE BP, SI
		// CMOVNBE (R16.BP, R16.SI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNBE (R16.BP, R16.SI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x47, 0xee };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNBE BP, SI' failed.");
	}

	// CMOVNBE reg32,rmreg32
	[Test]
	public void CMOVNBE_reg32_rmreg32 ()
	{
		// CMOVNBE EAX, ESP
		// CMOVNBE (R32.EAX, R32.ESP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNBE (R32.EAX, R32.ESP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x47, 0xc4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNBE EAX, ESP' failed.");
	}

	// CMOVNC reg16,mem16
	[Test]
	public void CMOVNC_reg16_mem16 ()
	{
		// CMOVNC SI, [EDI + 0x12345678]
		// CMOVNC (R16.SI, new WordMemory(null, R32.EDI, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNC (R16.SI, new WordMemory (null, R32.EDI, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x43, 0xb7, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNC SI, [EDI + 0x12345678]' failed.");
	}

	// CMOVNC reg32,mem32
	[Test]
	public void CMOVNC_reg32_mem32 ()
	{
		// CMOVNC EAX, [EBP]
		// CMOVNC (R32.EAX, new DWordMemory(null, R32.EBP, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNC (R32.EAX, new DWordMemory (null, R32.EBP, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x43, 0x45, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNC EAX, [EBP]' failed.");
	}

	// CMOVNC reg16,rmreg16
	[Test]
	public void CMOVNC_reg16_rmreg16 ()
	{
		// CMOVNC BX, DX
		// CMOVNC (R16.BX, R16.DX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNC (R16.BX, R16.DX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x43, 0xda };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNC BX, DX' failed.");
	}

	// CMOVNC reg32,rmreg32
	[Test]
	public void CMOVNC_reg32_rmreg32 ()
	{
		// CMOVNC ESI, ECX
		// CMOVNC (R32.ESI, R32.ECX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNC (R32.ESI, R32.ECX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x43, 0xf1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNC ESI, ECX' failed.");
	}

	// CMOVNE reg16,mem16
	[Test]
	public void CMOVNE_reg16_mem16 ()
	{
		// CMOVNE AX, [ESP]
		// CMOVNE (R16.AX, new WordMemory(null, R32.ESP, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNE (R16.AX, new WordMemory (null, R32.ESP, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x45, 0x4, 0x24 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNE AX, [ESP]' failed.");
	}

	// CMOVNE reg32,mem32
	[Test]
	public void CMOVNE_reg32_mem32 ()
	{
		// CMOVNE EDX, [DS:ECX*8]
		// CMOVNE (R32.EDX, new DWordMemory(Seg.DS, null, R32.ECX, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNE (R32.EDX, new DWordMemory (Seg.DS, null, R32.ECX, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xf, 0x45, 0x14, 0xcd, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNE EDX, [DS:ECX*8]' failed.");
	}

	// CMOVNE reg16,rmreg16
	[Test]
	public void CMOVNE_reg16_rmreg16 ()
	{
		// CMOVNE DI, DX
		// CMOVNE (R16.DI, R16.DX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNE (R16.DI, R16.DX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x45, 0xfa };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNE DI, DX' failed.");
	}

	// CMOVNE reg32,rmreg32
	[Test]
	public void CMOVNE_reg32_rmreg32 ()
	{
		// CMOVNE ECX, EDI
		// CMOVNE (R32.ECX, R32.EDI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNE (R32.ECX, R32.EDI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x45, 0xcf };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNE ECX, EDI' failed.");
	}

	// CMOVNG reg16,mem16
	[Test]
	public void CMOVNG_reg16_mem16 ()
	{
		// CMOVNG DI, [DS:EDX*2]
		// CMOVNG (R16.DI, new WordMemory(Seg.DS, null, R32.EDX, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNG (R16.DI, new WordMemory (Seg.DS, null, R32.EDX, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x66, 0xf, 0x4e, 0x3c, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNG DI, [DS:EDX*2]' failed.");
	}

	// CMOVNG reg32,mem32
	[Test]
	public void CMOVNG_reg32_mem32 ()
	{
		// CMOVNG ECX, [SS:EBX*4]
		// CMOVNG (R32.ECX, new DWordMemory(Seg.SS, null, R32.EBX, 2))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNG (R32.ECX, new DWordMemory (Seg.SS, null, R32.EBX, 2));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0xf, 0x4e, 0xc, 0x9d, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNG ECX, [SS:EBX*4]' failed.");
	}

	// CMOVNG reg16,rmreg16
	[Test]
	public void CMOVNG_reg16_rmreg16 ()
	{
		// CMOVNG DX, CX
		// CMOVNG (R16.DX, R16.CX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNG (R16.DX, R16.CX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x4e, 0xd1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNG DX, CX' failed.");
	}

	// CMOVNG reg32,rmreg32
	[Test]
	public void CMOVNG_reg32_rmreg32 ()
	{
		// CMOVNG EDI, ESP
		// CMOVNG (R32.EDI, R32.ESP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNG (R32.EDI, R32.ESP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x4e, 0xfc };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNG EDI, ESP' failed.");
	}

	// CMOVNGE reg16,mem16
	[Test]
	public void CMOVNGE_reg16_mem16 ()
	{
		// CMOVNGE SI, [FS:EBP*2]
		// CMOVNGE (R16.SI, new WordMemory(Seg.FS, null, R32.EBP, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNGE (R16.SI, new WordMemory (Seg.FS, null, R32.EBP, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0x66, 0xf, 0x4c, 0x74, 0x2d, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNGE SI, [FS:EBP*2]' failed.");
	}

	// CMOVNGE reg32,mem32
	[Test]
	public void CMOVNGE_reg32_mem32 ()
	{
		// CMOVNGE EDI, [ESP]
		// CMOVNGE (R32.EDI, new DWordMemory(null, R32.ESP, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNGE (R32.EDI, new DWordMemory (null, R32.ESP, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x4c, 0x3c, 0x24 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNGE EDI, [ESP]' failed.");
	}

	// CMOVNGE reg16,rmreg16
	[Test]
	public void CMOVNGE_reg16_rmreg16 ()
	{
		// CMOVNGE BP, SP
		// CMOVNGE (R16.BP, R16.SP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNGE (R16.BP, R16.SP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x4c, 0xec };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNGE BP, SP' failed.");
	}

	// CMOVNGE reg32,rmreg32
	[Test]
	public void CMOVNGE_reg32_rmreg32 ()
	{
		// CMOVNGE EDX, ECX
		// CMOVNGE (R32.EDX, R32.ECX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNGE (R32.EDX, R32.ECX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x4c, 0xd1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNGE EDX, ECX' failed.");
	}

	// CMOVNL reg16,mem16
	[Test]
	public void CMOVNL_reg16_mem16 ()
	{
		// CMOVNL DI, [CS:EBP*8]
		// CMOVNL (R16.DI, new WordMemory(Seg.CS, null, R32.EBP, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNL (R16.DI, new WordMemory (Seg.CS, null, R32.EBP, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0x66, 0xf, 0x4d, 0x3c, 0xed, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNL DI, [CS:EBP*8]' failed.");
	}

	// CMOVNL reg32,mem32
	[Test]
	public void CMOVNL_reg32_mem32 ()
	{
		// CMOVNL ECX, [0x12345678]
		// CMOVNL (R32.ECX, new DWordMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNL (R32.ECX, new DWordMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x4d, 0xd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNL ECX, [0x12345678]' failed.");
	}

	// CMOVNL reg16,rmreg16
	[Test]
	public void CMOVNL_reg16_rmreg16 ()
	{
		// CMOVNL BX, BP
		// CMOVNL (R16.BX, R16.BP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNL (R16.BX, R16.BP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x4d, 0xdd };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNL BX, BP' failed.");
	}

	// CMOVNL reg32,rmreg32
	[Test]
	public void CMOVNL_reg32_rmreg32 ()
	{
		// CMOVNL ESI, EAX
		// CMOVNL (R32.ESI, R32.EAX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNL (R32.ESI, R32.EAX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x4d, 0xf0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNL ESI, EAX' failed.");
	}

	// CMOVNLE reg16,mem16
	[Test]
	public void CMOVNLE_reg16_mem16 ()
	{
		// CMOVNLE BP, [EDI + ESI*2 + 0x12345678]
		// CMOVNLE (R16.BP, new WordMemory(null, R32.EDI, R32.ESI, 1, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNLE (R16.BP, new WordMemory (null, R32.EDI, R32.ESI, 1, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x4f, 0xac, 0x77, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNLE BP, [EDI + ESI*2 + 0x12345678]' failed.");
	}

	// CMOVNLE reg32,mem32
	[Test]
	public void CMOVNLE_reg32_mem32 ()
	{
		// CMOVNLE EBP, [ECX]
		// CMOVNLE (R32.EBP, new DWordMemory(null, R32.ECX, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNLE (R32.EBP, new DWordMemory (null, R32.ECX, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x4f, 0x29 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNLE EBP, [ECX]' failed.");
	}

	// CMOVNLE reg16,rmreg16
	[Test]
	public void CMOVNLE_reg16_rmreg16 ()
	{
		// CMOVNLE DI, SI
		// CMOVNLE (R16.DI, R16.SI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNLE (R16.DI, R16.SI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x4f, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNLE DI, SI' failed.");
	}

	// CMOVNLE reg32,rmreg32
	[Test]
	public void CMOVNLE_reg32_rmreg32 ()
	{
		// CMOVNLE EAX, EDI
		// CMOVNLE (R32.EAX, R32.EDI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNLE (R32.EAX, R32.EDI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x4f, 0xc7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNLE EAX, EDI' failed.");
	}

	// CMOVNO reg16,mem16
	[Test]
	public void CMOVNO_reg16_mem16 ()
	{
		// CMOVNO SI, [EBP + EAX*1]
		// CMOVNO (R16.SI, new WordMemory(null, R32.EBP, R32.EAX, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNO (R16.SI, new WordMemory (null, R32.EBP, R32.EAX, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x41, 0x74, 0x5, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNO SI, [EBP + EAX*1]' failed.");
	}

	// CMOVNO reg32,mem32
	[Test]
	public void CMOVNO_reg32_mem32 ()
	{
		// CMOVNO ESI, [CS:EBX*1 + 0x12345678]
		// CMOVNO (R32.ESI, new DWordMemory(Seg.CS, null, R32.EBX, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNO (R32.ESI, new DWordMemory (Seg.CS, null, R32.EBX, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xf, 0x41, 0xb3, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNO ESI, [CS:EBX*1 + 0x12345678]' failed.");
	}

	// CMOVNO reg16,rmreg16
	[Test]
	public void CMOVNO_reg16_rmreg16 ()
	{
		// CMOVNO DX, AX
		// CMOVNO (R16.DX, R16.AX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNO (R16.DX, R16.AX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x41, 0xd0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNO DX, AX' failed.");
	}

	// CMOVNO reg32,rmreg32
	[Test]
	public void CMOVNO_reg32_rmreg32 ()
	{
		// CMOVNO ESI, ESP
		// CMOVNO (R32.ESI, R32.ESP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNO (R32.ESI, R32.ESP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x41, 0xf4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNO ESI, ESP' failed.");
	}

	// CMOVNP reg16,mem16
	[Test]
	public void CMOVNP_reg16_mem16 ()
	{
		// CMOVNP DX, [0x12345678]
		// CMOVNP (R16.DX, new WordMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNP (R16.DX, new WordMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x4b, 0x15, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNP DX, [0x12345678]' failed.");
	}

	// CMOVNP reg32,mem32
	[Test]
	public void CMOVNP_reg32_mem32 ()
	{
		// CMOVNP ECX, [CS:EBP]
		// CMOVNP (R32.ECX, new DWordMemory(Seg.CS, R32.EBP, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNP (R32.ECX, new DWordMemory (Seg.CS, R32.EBP, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xf, 0x4b, 0x4d, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNP ECX, [CS:EBP]' failed.");
	}

	// CMOVNP reg16,rmreg16
	[Test]
	public void CMOVNP_reg16_rmreg16 ()
	{
		// CMOVNP SP, DX
		// CMOVNP (R16.SP, R16.DX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNP (R16.SP, R16.DX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x4b, 0xe2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNP SP, DX' failed.");
	}

	// CMOVNP reg32,rmreg32
	[Test]
	public void CMOVNP_reg32_rmreg32 ()
	{
		// CMOVNP ESI, ECX
		// CMOVNP (R32.ESI, R32.ECX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNP (R32.ESI, R32.ECX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x4b, 0xf1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNP ESI, ECX' failed.");
	}

	// CMOVNS reg16,mem16
	[Test]
	public void CMOVNS_reg16_mem16 ()
	{
		// CMOVNS BX, [GS:EDX + 0x12345678]
		// CMOVNS (R16.BX, new WordMemory(Seg.GS, R32.EDX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNS (R16.BX, new WordMemory (Seg.GS, R32.EDX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0x66, 0xf, 0x49, 0x9a, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNS BX, [GS:EDX + 0x12345678]' failed.");
	}

	// CMOVNS reg32,mem32
	[Test]
	public void CMOVNS_reg32_mem32 ()
	{
		// CMOVNS ESP, [EBX + EBP*1 + 0x12345678]
		// CMOVNS (R32.ESP, new DWordMemory(null, R32.EBX, R32.EBP, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNS (R32.ESP, new DWordMemory (null, R32.EBX, R32.EBP, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x49, 0xa4, 0x2b, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNS ESP, [EBX + EBP*1 + 0x12345678]' failed.");
	}

	// CMOVNS reg16,rmreg16
	[Test]
	public void CMOVNS_reg16_rmreg16 ()
	{
		// CMOVNS SP, BX
		// CMOVNS (R16.SP, R16.BX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNS (R16.SP, R16.BX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x49, 0xe3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNS SP, BX' failed.");
	}

	// CMOVNS reg32,rmreg32
	[Test]
	public void CMOVNS_reg32_rmreg32 ()
	{
		// CMOVNS EBP, EBP
		// CMOVNS (R32.EBP, R32.EBP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNS (R32.EBP, R32.EBP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x49, 0xed };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNS EBP, EBP' failed.");
	}

	// CMOVNZ reg16,mem16
	[Test]
	public void CMOVNZ_reg16_mem16 ()
	{
		// CMOVNZ SP, [ES:EDX*8]
		// CMOVNZ (R16.SP, new WordMemory(Seg.ES, null, R32.EDX, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNZ (R16.SP, new WordMemory (Seg.ES, null, R32.EDX, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0x66, 0xf, 0x45, 0x24, 0xd5, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNZ SP, [ES:EDX*8]' failed.");
	}

	// CMOVNZ reg32,mem32
	[Test]
	public void CMOVNZ_reg32_mem32 ()
	{
		// CMOVNZ ESI, [ECX + ECX*8]
		// CMOVNZ (R32.ESI, new DWordMemory(null, R32.ECX, R32.ECX, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNZ (R32.ESI, new DWordMemory (null, R32.ECX, R32.ECX, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x45, 0x34, 0xc9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNZ ESI, [ECX + ECX*8]' failed.");
	}

	// CMOVNZ reg16,rmreg16
	[Test]
	public void CMOVNZ_reg16_rmreg16 ()
	{
		// CMOVNZ AX, BP
		// CMOVNZ (R16.AX, R16.BP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNZ (R16.AX, R16.BP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x45, 0xc5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNZ AX, BP' failed.");
	}

	// CMOVNZ reg32,rmreg32
	[Test]
	public void CMOVNZ_reg32_rmreg32 ()
	{
		// CMOVNZ ESP, EDX
		// CMOVNZ (R32.ESP, R32.EDX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVNZ (R32.ESP, R32.EDX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x45, 0xe2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVNZ ESP, EDX' failed.");
	}

	// CMOVO reg16,mem16
	[Test]
	public void CMOVO_reg16_mem16 ()
	{
		// CMOVO CX, [ECX + 0x12345678]
		// CMOVO (R16.CX, new WordMemory(null, R32.ECX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVO (R16.CX, new WordMemory (null, R32.ECX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x40, 0x89, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVO CX, [ECX + 0x12345678]' failed.");
	}

	// CMOVO reg32,mem32
	[Test]
	public void CMOVO_reg32_mem32 ()
	{
		// CMOVO ESI, [EAX + EDX*4 + 0x12345678]
		// CMOVO (R32.ESI, new DWordMemory(null, R32.EAX, R32.EDX, 2, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVO (R32.ESI, new DWordMemory (null, R32.EAX, R32.EDX, 2, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x40, 0xb4, 0x90, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVO ESI, [EAX + EDX*4 + 0x12345678]' failed.");
	}

	// CMOVO reg16,rmreg16
	[Test]
	public void CMOVO_reg16_rmreg16 ()
	{
		// CMOVO DI, CX
		// CMOVO (R16.DI, R16.CX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVO (R16.DI, R16.CX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x40, 0xf9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVO DI, CX' failed.");
	}

	// CMOVO reg32,rmreg32
	[Test]
	public void CMOVO_reg32_rmreg32 ()
	{
		// CMOVO ECX, EBP
		// CMOVO (R32.ECX, R32.EBP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVO (R32.ECX, R32.EBP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x40, 0xcd };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVO ECX, EBP' failed.");
	}

	// CMOVP reg16,mem16
	[Test]
	public void CMOVP_reg16_mem16 ()
	{
		// CMOVP AX, [ESP]
		// CMOVP (R16.AX, new WordMemory(null, R32.ESP, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVP (R16.AX, new WordMemory (null, R32.ESP, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x4a, 0x4, 0x24 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVP AX, [ESP]' failed.");
	}

	// CMOVP reg32,mem32
	[Test]
	public void CMOVP_reg32_mem32 ()
	{
		// CMOVP EAX, [0x12345678]
		// CMOVP (R32.EAX, new DWordMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVP (R32.EAX, new DWordMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x4a, 0x5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVP EAX, [0x12345678]' failed.");
	}

	// CMOVP reg16,rmreg16
	[Test]
	public void CMOVP_reg16_rmreg16 ()
	{
		// CMOVP AX, SI
		// CMOVP (R16.AX, R16.SI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVP (R16.AX, R16.SI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x4a, 0xc6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVP AX, SI' failed.");
	}

	// CMOVP reg32,rmreg32
	[Test]
	public void CMOVP_reg32_rmreg32 ()
	{
		// CMOVP EBP, ESI
		// CMOVP (R32.EBP, R32.ESI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVP (R32.EBP, R32.ESI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x4a, 0xee };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVP EBP, ESI' failed.");
	}

	// CMOVPE reg16,mem16
	[Test]
	public void CMOVPE_reg16_mem16 ()
	{
		// CMOVPE AX, [EBX + 0x12345678]
		// CMOVPE (R16.AX, new WordMemory(null, R32.EBX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVPE (R16.AX, new WordMemory (null, R32.EBX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x4a, 0x83, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVPE AX, [EBX + 0x12345678]' failed.");
	}

	// CMOVPE reg32,mem32
	[Test]
	public void CMOVPE_reg32_mem32 ()
	{
		// CMOVPE EBP, [DS:ESP + ESI*2]
		// CMOVPE (R32.EBP, new DWordMemory(Seg.DS, R32.ESP, R32.ESI, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVPE (R32.EBP, new DWordMemory (Seg.DS, R32.ESP, R32.ESI, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xf, 0x4a, 0x2c, 0x74 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVPE EBP, [DS:ESP + ESI*2]' failed.");
	}

	// CMOVPE reg16,rmreg16
	[Test]
	public void CMOVPE_reg16_rmreg16 ()
	{
		// CMOVPE CX, SP
		// CMOVPE (R16.CX, R16.SP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVPE (R16.CX, R16.SP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x4a, 0xcc };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVPE CX, SP' failed.");
	}

	// CMOVPE reg32,rmreg32
	[Test]
	public void CMOVPE_reg32_rmreg32 ()
	{
		// CMOVPE EAX, EDI
		// CMOVPE (R32.EAX, R32.EDI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVPE (R32.EAX, R32.EDI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x4a, 0xc7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVPE EAX, EDI' failed.");
	}

	// CMOVPO reg16,mem16
	[Test]
	public void CMOVPO_reg16_mem16 ()
	{
		// CMOVPO BX, [EBX*2]
		// CMOVPO (R16.BX, new WordMemory(null, null, R32.EBX, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVPO (R16.BX, new WordMemory (null, null, R32.EBX, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x4b, 0x1c, 0x1b };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVPO BX, [EBX*2]' failed.");
	}

	// CMOVPO reg32,mem32
	[Test]
	public void CMOVPO_reg32_mem32 ()
	{
		// CMOVPO EAX, [CS:EBX + 0x12345678]
		// CMOVPO (R32.EAX, new DWordMemory(Seg.CS, R32.EBX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVPO (R32.EAX, new DWordMemory (Seg.CS, R32.EBX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xf, 0x4b, 0x83, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVPO EAX, [CS:EBX + 0x12345678]' failed.");
	}

	// CMOVPO reg16,rmreg16
	[Test]
	public void CMOVPO_reg16_rmreg16 ()
	{
		// CMOVPO CX, DX
		// CMOVPO (R16.CX, R16.DX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVPO (R16.CX, R16.DX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x4b, 0xca };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVPO CX, DX' failed.");
	}

	// CMOVPO reg32,rmreg32
	[Test]
	public void CMOVPO_reg32_rmreg32 ()
	{
		// CMOVPO EDX, EBP
		// CMOVPO (R32.EDX, R32.EBP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVPO (R32.EDX, R32.EBP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x4b, 0xd5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVPO EDX, EBP' failed.");
	}

	// CMOVS reg16,mem16
	[Test]
	public void CMOVS_reg16_mem16 ()
	{
		// CMOVS SI, [EBP + 0x12345678]
		// CMOVS (R16.SI, new WordMemory(null, R32.EBP, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVS (R16.SI, new WordMemory (null, R32.EBP, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x48, 0xb5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVS SI, [EBP + 0x12345678]' failed.");
	}

	// CMOVS reg32,mem32
	[Test]
	public void CMOVS_reg32_mem32 ()
	{
		// CMOVS ECX, [CS:EBP]
		// CMOVS (R32.ECX, new DWordMemory(Seg.CS, R32.EBP, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVS (R32.ECX, new DWordMemory (Seg.CS, R32.EBP, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xf, 0x48, 0x4d, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVS ECX, [CS:EBP]' failed.");
	}

	// CMOVS reg16,rmreg16
	[Test]
	public void CMOVS_reg16_rmreg16 ()
	{
		// CMOVS AX, DI
		// CMOVS (R16.AX, R16.DI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVS (R16.AX, R16.DI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x48, 0xc7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVS AX, DI' failed.");
	}

	// CMOVS reg32,rmreg32
	[Test]
	public void CMOVS_reg32_rmreg32 ()
	{
		// CMOVS EBX, ECX
		// CMOVS (R32.EBX, R32.ECX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVS (R32.EBX, R32.ECX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x48, 0xd9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVS EBX, ECX' failed.");
	}

	// CMOVZ reg16,mem16
	[Test]
	public void CMOVZ_reg16_mem16 ()
	{
		// CMOVZ DX, [ESI + ESI*4 + 0x12345678]
		// CMOVZ (R16.DX, new WordMemory(null, R32.ESI, R32.ESI, 2, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVZ (R16.DX, new WordMemory (null, R32.ESI, R32.ESI, 2, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x44, 0x94, 0xb6, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVZ DX, [ESI + ESI*4 + 0x12345678]' failed.");
	}

	// CMOVZ reg32,mem32
	[Test]
	public void CMOVZ_reg32_mem32 ()
	{
		// CMOVZ EDX, [SS:EDI + EAX*8]
		// CMOVZ (R32.EDX, new DWordMemory(Seg.SS, R32.EDI, R32.EAX, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVZ (R32.EDX, new DWordMemory (Seg.SS, R32.EDI, R32.EAX, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0xf, 0x44, 0x14, 0xc7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVZ EDX, [SS:EDI + EAX*8]' failed.");
	}

	// CMOVZ reg16,rmreg16
	[Test]
	public void CMOVZ_reg16_rmreg16 ()
	{
		// CMOVZ CX, SI
		// CMOVZ (R16.CX, R16.SI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVZ (R16.CX, R16.SI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x44, 0xce };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVZ CX, SI' failed.");
	}

	// CMOVZ reg32,rmreg32
	[Test]
	public void CMOVZ_reg32_rmreg32 ()
	{
		// CMOVZ EDI, EDX
		// CMOVZ (R32.EDI, R32.EDX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMOVZ (R32.EDI, R32.EDX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x44, 0xfa };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMOVZ EDI, EDX' failed.");
	}

	// CMP mem8,reg8
	[Test]
	public void CMP_mem8_reg8 ()
	{
		// CMP [ES:EBX + 0x12345678], DL
		// CMP (new ByteMemory(Seg.ES, R32.EBX, null, 0, 0x12345678), R8.DL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (new ByteMemory (Seg.ES, R32.EBX, null, 0, 0x12345678), R8.DL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0x38, 0x93, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP [ES:EBX + 0x12345678], DL' failed.");
	}

	// CMP mem16,reg16
	[Test]
	public void CMP_mem16_reg16 ()
	{
		// CMP [0x12345678], DI
		// CMP (new WordMemory(null, null, null, 0, 0x12345678), R16.DI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (new WordMemory (null, null, null, 0, 0x12345678), R16.DI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x39, 0x3d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP [0x12345678], DI' failed.");
	}

	// CMP mem32,reg32
	[Test]
	public void CMP_mem32_reg32 ()
	{
		// CMP [ECX], EDX
		// CMP (new DWordMemory(null, R32.ECX, null, 0), R32.EDX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (new DWordMemory (null, R32.ECX, null, 0), R32.EDX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x39, 0x11 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP [ECX], EDX' failed.");
	}

	// CMP reg8,mem8
	[Test]
	public void CMP_reg8_mem8 ()
	{
		// CMP BL, [DS:0x12345678]
		// CMP (R8.BL, new ByteMemory(Seg.DS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (R8.BL, new ByteMemory (Seg.DS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x3a, 0x1d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP BL, [DS:0x12345678]' failed.");
	}

	// CMP reg16,mem16
	[Test]
	public void CMP_reg16_mem16 ()
	{
		// CMP SI, [0x12345678]
		// CMP (R16.SI, new WordMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (R16.SI, new WordMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x3b, 0x35, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP SI, [0x12345678]' failed.");
	}

	// CMP reg32,mem32
	[Test]
	public void CMP_reg32_mem32 ()
	{
		// CMP EDI, [ESI*8]
		// CMP (R32.EDI, new DWordMemory(null, null, R32.ESI, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (R32.EDI, new DWordMemory (null, null, R32.ESI, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3b, 0x3c, 0xf5, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP EDI, [ESI*8]' failed.");
	}

	// CMP mem8,imm8
	[Test]
	public void CMP_mem8_imm8 ()
	{
		// CMP Byte [DS:0x12345678], 0x7
		// CMP (new ByteMemory(Seg.DS, null, null, 0, 0x12345678), 0x7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (new ByteMemory (Seg.DS, null, null, 0, 0x12345678), 0x7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x80, 0x3d, 0x78, 0x56, 0x34, 0x12, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP Byte [DS:0x12345678], 0x7' failed.");
	}

	// CMP mem16,imm16
	[Test]
	public void CMP_mem16_imm16 ()
	{
		// CMP Word [ES:EBP], 0x8ad
		// CMP (new WordMemory(Seg.ES, R32.EBP, null, 0), 0x8ad)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (new WordMemory (Seg.ES, R32.EBP, null, 0), 0x8ad);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0x66, 0x81, 0x7d, 0x0, 0xad, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP Word [ES:EBP], 0x8ad' failed.");
	}

	// CMP mem32,imm32
	[Test]
	public void CMP_mem32_imm32 ()
	{
		// CMP DWord [ES:0x12345678], 0x6abbff6
		// CMP (new DWordMemory(Seg.ES, null, null, 0, 0x12345678), 0x6abbff6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (new DWordMemory (Seg.ES, null, null, 0, 0x12345678), 0x6abbff6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0x81, 0x3d, 0x78, 0x56, 0x34, 0x12, 0xf6, 0xbf, 0xab, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP DWord [ES:0x12345678], 0x6abbff6' failed.");
	}

	// CMP mem16,imm8
	[Test]
	public void CMP_mem16_imm8 ()
	{
		// CMP Word [SS:0x12345678], 0xa
		// CMP (new WordMemory(Seg.SS, null, null, 0, 0x12345678), 0xa)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (new WordMemory (Seg.SS, null, null, 0, 0x12345678), 0xa);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0x66, 0x83, 0x3d, 0x78, 0x56, 0x34, 0x12, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP Word [SS:0x12345678], 0xa' failed.");
	}

	// CMP mem32,imm8
	[Test]
	public void CMP_mem32_imm8 ()
	{
		// CMP DWord [EDX + EBP*1 + 0x12345678], 0xb
		// CMP (new DWordMemory(null, R32.EDX, R32.EBP, 0, 0x12345678), 0xb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (new DWordMemory (null, R32.EDX, R32.EBP, 0, 0x12345678), 0xb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x83, 0xbc, 0x2a, 0x78, 0x56, 0x34, 0x12, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP DWord [EDX + EBP*1 + 0x12345678], 0xb' failed.");
	}

	// CMP rmreg8,reg8
	[Test]
	public void CMP_rmreg8_reg8 ()
	{
		// CMP CH, CL
		// CMP (R8.CH, R8.CL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (R8.CH, R8.CL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x38, 0xcd };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP CH, CL' failed.");
	}

	// CMP rmreg16,reg16
	[Test]
	public void CMP_rmreg16_reg16 ()
	{
		// CMP SI, AX
		// CMP (R16.SI, R16.AX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (R16.SI, R16.AX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x39, 0xc6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP SI, AX' failed.");
	}

	// CMP rmreg32,reg32
	[Test]
	public void CMP_rmreg32_reg32 ()
	{
		// CMP EBP, EBP
		// CMP (R32.EBP, R32.EBP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (R32.EBP, R32.EBP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x39, 0xed };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP EBP, EBP' failed.");
	}

	// CMP rmreg8,imm8
	[Test]
	public void CMP_rmreg8_imm8 ()
	{
		// CMP CH, 0x3
		// CMP (R8.CH, 0x3)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (R8.CH, 0x3);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x80, 0xfd, 0x3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP CH, 0x3' failed.");
	}

	// CMP rmreg16,imm16
	[Test]
	public void CMP_rmreg16_imm16 ()
	{
		// CMP AX, 0xa82
		// CMP (R16.AX, 0xa82)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (R16.AX, 0xa82);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x3d, 0x82, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP AX, 0xa82' failed.");
	}

	// CMP rmreg32,imm32
	[Test]
	public void CMP_rmreg32_imm32 ()
	{
		// CMP EBP, 0xea3f51f
		// CMP (R32.EBP, 0xea3f51f)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (R32.EBP, 0xea3f51f);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x81, 0xfd, 0x1f, 0xf5, 0xa3, 0xe };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP EBP, 0xea3f51f' failed.");
	}

	// CMP rmreg16,imm8
	[Test]
	public void CMP_rmreg16_imm8 ()
	{
		// CMP DI, 0xa
		// CMP (R16.DI, 0xa)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (R16.DI, 0xa);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x83, 0xff, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP DI, 0xa' failed.");
	}

	// CMP rmreg32,imm8
	[Test]
	public void CMP_rmreg32_imm8 ()
	{
		// CMP EDI, 0xa
		// CMP (R32.EDI, 0xa)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMP (R32.EDI, 0xa);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x83, 0xff, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMP EDI, 0xa' failed.");
	}

	// CMPSB 
	[Test]
	public void CMPSB ()
	{
		// CMPSB
		// CMPSB ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMPSB ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xa6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMPSB' failed.");
	}

	// CMPSD 
	[Test]
	public void CMPSD ()
	{
		// CMPSD
		// CMPSD ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMPSD ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xa7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMPSD' failed.");
	}

	// CMPSW 
	[Test]
	public void CMPSW ()
	{
		// CMPSW
		// CMPSW ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMPSW ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xa7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMPSW' failed.");
	}

	// CMPXCHG mem8,reg8
	[Test]
	public void CMPXCHG_mem8_reg8 ()
	{
		// CMPXCHG [DS:EDX + 0x12345678], DH
		// CMPXCHG (new ByteMemory(Seg.DS, R32.EDX, null, 0, 0x12345678), R8.DH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMPXCHG (new ByteMemory (Seg.DS, R32.EDX, null, 0, 0x12345678), R8.DH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xf, 0xb0, 0xb2, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMPXCHG [DS:EDX + 0x12345678], DH' failed.");
	}

	// CMPXCHG mem16,reg16
	[Test]
	public void CMPXCHG_mem16_reg16 ()
	{
		// CMPXCHG [DS:0x12345678], SI
		// CMPXCHG (new WordMemory(Seg.DS, null, null, 0, 0x12345678), R16.SI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMPXCHG (new WordMemory (Seg.DS, null, null, 0, 0x12345678), R16.SI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x66, 0xf, 0xb1, 0x35, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMPXCHG [DS:0x12345678], SI' failed.");
	}

	// CMPXCHG mem32,reg32
	[Test]
	public void CMPXCHG_mem32_reg32 ()
	{
		// CMPXCHG [EBP + EBP*4 + 0x12345678], EAX
		// CMPXCHG (new DWordMemory(null, R32.EBP, R32.EBP, 2, 0x12345678), R32.EAX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMPXCHG (new DWordMemory (null, R32.EBP, R32.EBP, 2, 0x12345678), R32.EAX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xb1, 0x84, 0xad, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMPXCHG [EBP + EBP*4 + 0x12345678], EAX' failed.");
	}

	// CMPXCHG rmreg8,reg8
	[Test]
	public void CMPXCHG_rmreg8_reg8 ()
	{
		// CMPXCHG DH, AL
		// CMPXCHG (R8.DH, R8.AL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMPXCHG (R8.DH, R8.AL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xb0, 0xc6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMPXCHG DH, AL' failed.");
	}

	// CMPXCHG rmreg16,reg16
	[Test]
	public void CMPXCHG_rmreg16_reg16 ()
	{
		// CMPXCHG SP, DI
		// CMPXCHG (R16.SP, R16.DI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMPXCHG (R16.SP, R16.DI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xb1, 0xfc };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMPXCHG SP, DI' failed.");
	}

	// CMPXCHG rmreg32,reg32
	[Test]
	public void CMPXCHG_rmreg32_reg32 ()
	{
		// CMPXCHG ECX, ESP
		// CMPXCHG (R32.ECX, R32.ESP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMPXCHG (R32.ECX, R32.ESP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xb1, 0xe1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMPXCHG ECX, ESP' failed.");
	}

	// CMPXCHG8B mem
	[Test]
	public void CMPXCHG8B_mem ()
	{
		// CMPXCHG8B [FS:EBP + EBX*2 + 0x12345678]
		// CMPXCHG8B (new DWordMemory(Seg.FS, R32.EBP, R32.EBX, 1, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CMPXCHG8B (new DWordMemory (Seg.FS, R32.EBP, R32.EBX, 1, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xf, 0xc7, 0x8c, 0x5d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CMPXCHG8B [FS:EBP + EBX*2 + 0x12345678]' failed.");
	}

	// CPUID 
	[Test]
	public void CPUID ()
	{
		// CPUID
		// CPUID ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CPUID ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xa2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CPUID' failed.");
	}

	// CWD 
	[Test]
	public void CWD ()
	{
		// CWD
		// CWD ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CWD ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x99 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CWD' failed.");
	}

	// CWDE 
	[Test]
	public void CWDE ()
	{
		// CWDE
		// CWDE ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.CWDE ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x98 };
		Assert.IsTrue (CompareData (memoryStream, target), "'CWDE' failed.");
	}

	// DAA 
	[Test]
	public void DAA ()
	{
		// DAA
		// DAA ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.DAA ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x27 };
		Assert.IsTrue (CompareData (memoryStream, target), "'DAA' failed.");
	}

	// DAS 
	[Test]
	public void DAS ()
	{
		// DAS
		// DAS ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.DAS ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2f };
		Assert.IsTrue (CompareData (memoryStream, target), "'DAS' failed.");
	}

	// DEC reg16
	[Test]
	public void DEC_reg16 ()
	{
		// DEC BX
		// DEC (R16.BX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.DEC (R16.BX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x4b };
		Assert.IsTrue (CompareData (memoryStream, target), "'DEC BX' failed.");
	}

	// DEC reg32
	[Test]
	public void DEC_reg32 ()
	{
		// DEC EBP
		// DEC (R32.EBP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.DEC (R32.EBP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x4d };
		Assert.IsTrue (CompareData (memoryStream, target), "'DEC EBP' failed.");
	}

	// DEC mem8
	[Test]
	public void DEC_mem8 ()
	{
		// DEC Byte [SS:0x12345678]
		// DEC (new ByteMemory(Seg.SS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.DEC (new ByteMemory (Seg.SS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0xfe, 0xd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'DEC Byte [SS:0x12345678]' failed.");
	}

	// DEC mem16
	[Test]
	public void DEC_mem16 ()
	{
		// DEC Word [ECX + EBP*1]
		// DEC (new WordMemory(null, R32.ECX, R32.EBP, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.DEC (new WordMemory (null, R32.ECX, R32.EBP, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xff, 0xc, 0x29 };
		Assert.IsTrue (CompareData (memoryStream, target), "'DEC Word [ECX + EBP*1]' failed.");
	}

	// DEC mem32
	[Test]
	public void DEC_mem32 ()
	{
		// DEC DWord [GS:EDX*8 + 0x12345678]
		// DEC (new DWordMemory(Seg.GS, null, R32.EDX, 3, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.DEC (new DWordMemory (Seg.GS, null, R32.EDX, 3, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xff, 0xc, 0xd5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'DEC DWord [GS:EDX*8 + 0x12345678]' failed.");
	}

	// DEC rmreg8
	[Test]
	public void DEC_rmreg8 ()
	{
		// DEC AH
		// DEC (R8.AH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.DEC (R8.AH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xfe, 0xcc };
		Assert.IsTrue (CompareData (memoryStream, target), "'DEC AH' failed.");
	}

	// DIV mem8
	[Test]
	public void DIV_mem8 ()
	{
		// DIV Byte [DS:0x12345678]
		// DIV (new ByteMemory(Seg.DS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.DIV (new ByteMemory (Seg.DS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xf6, 0x35, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'DIV Byte [DS:0x12345678]' failed.");
	}

	// DIV mem16
	[Test]
	public void DIV_mem16 ()
	{
		// DIV Word [0x12345678]
		// DIV (new WordMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.DIV (new WordMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf7, 0x35, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'DIV Word [0x12345678]' failed.");
	}

	// DIV mem32
	[Test]
	public void DIV_mem32 ()
	{
		// DIV DWord [ESP]
		// DIV (new DWordMemory(null, R32.ESP, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.DIV (new DWordMemory (null, R32.ESP, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf7, 0x34, 0x24 };
		Assert.IsTrue (CompareData (memoryStream, target), "'DIV DWord [ESP]' failed.");
	}

	// DIV rmreg8
	[Test]
	public void DIV_rmreg8 ()
	{
		// DIV DL
		// DIV (R8.DL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.DIV (R8.DL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf6, 0xf2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'DIV DL' failed.");
	}

	// DIV rmreg16
	[Test]
	public void DIV_rmreg16 ()
	{
		// DIV SI
		// DIV (R16.SI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.DIV (R16.SI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf7, 0xf6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'DIV SI' failed.");
	}

	// DIV rmreg32
	[Test]
	public void DIV_rmreg32 ()
	{
		// DIV EBP
		// DIV (R32.EBP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.DIV (R32.EBP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf7, 0xf5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'DIV EBP' failed.");
	}

	// EMMS 
	[Test]
	public void EMMS ()
	{
		// EMMS
		// EMMS ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.EMMS ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x77 };
		Assert.IsTrue (CompareData (memoryStream, target), "'EMMS' failed.");
	}

	// ENTER imm16,imm8
	[Test]
	public void ENTER_imm16_imm8 ()
	{
		// ENTER 0x3ec, 0x0
		// ENTER (0x3ec, 0x0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ENTER (0x3ec, 0x0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc8, 0xec, 0x3, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ENTER 0x3ec, 0x0' failed.");
	}

	// F2XM1 
	[Test]
	public void F2XM1 ()
	{
		// F2XM1
		// F2XM1 ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.F2XM1 ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xf0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'F2XM1' failed.");
	}

	// FABS 
	[Test]
	public void FABS ()
	{
		// FABS
		// FABS ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FABS ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xe1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FABS' failed.");
	}

	// FADD mem32
	[Test]
	public void FADD_mem32 ()
	{
		// FADD DWord [0x12345678]
		// FADD (new DWordMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FADD (new DWordMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd8, 0x5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FADD DWord [0x12345678]' failed.");
	}

	// FADD mem64
	[Test]
	public void FADD_mem64 ()
	{
		// FADD QWord [0x12345678]
		// FADD (new QWordMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FADD (new QWordMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdc, 0x5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FADD QWord [0x12345678]' failed.");
	}

	// FADD fpureg
	[Test]
	public void FADD_fpureg ()
	{
		// FADD ST5
		// FADD (FP.ST5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FADD (FP.ST5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd8, 0xc5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FADD ST5' failed.");
	}

	// FADD ST0,fpureg
	[Test]
	public void FADD_ST0_fpureg ()
	{
		// FADD ST0, ST4
		// FADD_ST0 (FP.ST4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FADD_ST0 (FP.ST4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd8, 0xc4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FADD ST0, ST4' failed.");
	}

	// FADD fpureg,ST0
	[Test]
	public void FADD_fpureg_ST0 ()
	{
		// FADD ST7, ST0
		// FADD__ST0 (FP.ST7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FADD__ST0 (FP.ST7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdc, 0xc7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FADD ST7, ST0' failed.");
	}

	// FADDP fpureg
	[Test]
	public void FADDP_fpureg ()
	{
		// FADDP ST6
		// FADDP (FP.ST6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FADDP (FP.ST6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xde, 0xc6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FADDP ST6' failed.");
	}

	// FADDP fpureg,ST0
	[Test]
	public void FADDP_fpureg_ST0 ()
	{
		// FADDP ST6, ST0
		// FADDP__ST0 (FP.ST6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FADDP__ST0 (FP.ST6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xde, 0xc6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FADDP ST6, ST0' failed.");
	}

	// FBLD mem80
	[Test]
	public void FBLD_mem80 ()
	{
		// FBLD TWord [GS:0x12345678]
		// FBLD (new TWordMemory(Seg.GS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FBLD (new TWordMemory (Seg.GS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xdf, 0x25, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FBLD TWord [GS:0x12345678]' failed.");
	}

	// FBSTP mem80
	[Test]
	public void FBSTP_mem80 ()
	{
		// FBSTP TWord [CS:EDX*8]
		// FBSTP (new TWordMemory(Seg.CS, null, R32.EDX, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FBSTP (new TWordMemory (Seg.CS, null, R32.EDX, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xdf, 0x34, 0xd5, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FBSTP TWord [CS:EDX*8]' failed.");
	}

	// FCHS 
	[Test]
	public void FCHS ()
	{
		// FCHS
		// FCHS ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCHS ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xe0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCHS' failed.");
	}

	// FCLEX 
	[Test]
	public void FCLEX ()
	{
		// FCLEX
		// FCLEX ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCLEX ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x9b, 0xdb, 0xe2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCLEX' failed.");
	}

	// FCMOVB fpureg
	[Test]
	public void FCMOVB_fpureg ()
	{
		// FCMOVB ST4
		// FCMOVB (FP.ST4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCMOVB (FP.ST4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xda, 0xc4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCMOVB ST4' failed.");
	}

	// FCMOVB ST0,fpureg
	[Test]
	public void FCMOVB_ST0_fpureg ()
	{
		// FCMOVB ST0, ST1
		// FCMOVB_ST0 (FP.ST1)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCMOVB_ST0 (FP.ST1);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xda, 0xc1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCMOVB ST0, ST1' failed.");
	}

	// FCMOVBE fpureg
	[Test]
	public void FCMOVBE_fpureg ()
	{
		// FCMOVBE ST2
		// FCMOVBE (FP.ST2)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCMOVBE (FP.ST2);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xda, 0xd2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCMOVBE ST2' failed.");
	}

	// FCMOVBE ST0,fpureg
	[Test]
	public void FCMOVBE_ST0_fpureg ()
	{
		// FCMOVBE ST0, ST7
		// FCMOVBE_ST0 (FP.ST7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCMOVBE_ST0 (FP.ST7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xda, 0xd7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCMOVBE ST0, ST7' failed.");
	}

	// FCMOVE fpureg
	[Test]
	public void FCMOVE_fpureg ()
	{
		// FCMOVE ST5
		// FCMOVE (FP.ST5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCMOVE (FP.ST5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xda, 0xcd };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCMOVE ST5' failed.");
	}

	// FCMOVE ST0,fpureg
	[Test]
	public void FCMOVE_ST0_fpureg ()
	{
		// FCMOVE ST0, ST5
		// FCMOVE_ST0 (FP.ST5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCMOVE_ST0 (FP.ST5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xda, 0xcd };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCMOVE ST0, ST5' failed.");
	}

	// FCMOVNB fpureg
	[Test]
	public void FCMOVNB_fpureg ()
	{
		// FCMOVNB ST7
		// FCMOVNB (FP.ST7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCMOVNB (FP.ST7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0xc7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCMOVNB ST7' failed.");
	}

	// FCMOVNB ST0,fpureg
	[Test]
	public void FCMOVNB_ST0_fpureg ()
	{
		// FCMOVNB ST0, ST1
		// FCMOVNB_ST0 (FP.ST1)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCMOVNB_ST0 (FP.ST1);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0xc1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCMOVNB ST0, ST1' failed.");
	}

	// FCMOVNBE fpureg
	[Test]
	public void FCMOVNBE_fpureg ()
	{
		// FCMOVNBE ST2
		// FCMOVNBE (FP.ST2)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCMOVNBE (FP.ST2);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0xd2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCMOVNBE ST2' failed.");
	}

	// FCMOVNBE ST0,fpureg
	[Test]
	public void FCMOVNBE_ST0_fpureg ()
	{
		// FCMOVNBE ST0, ST1
		// FCMOVNBE_ST0 (FP.ST1)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCMOVNBE_ST0 (FP.ST1);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0xd1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCMOVNBE ST0, ST1' failed.");
	}

	// FCMOVNE fpureg
	[Test]
	public void FCMOVNE_fpureg ()
	{
		// FCMOVNE ST4
		// FCMOVNE (FP.ST4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCMOVNE (FP.ST4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0xcc };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCMOVNE ST4' failed.");
	}

	// FCMOVNE ST0,fpureg
	[Test]
	public void FCMOVNE_ST0_fpureg ()
	{
		// FCMOVNE ST0, ST2
		// FCMOVNE_ST0 (FP.ST2)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCMOVNE_ST0 (FP.ST2);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0xca };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCMOVNE ST0, ST2' failed.");
	}

	// FCMOVNU fpureg
	[Test]
	public void FCMOVNU_fpureg ()
	{
		// FCMOVNU ST5
		// FCMOVNU (FP.ST5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCMOVNU (FP.ST5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0xdd };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCMOVNU ST5' failed.");
	}

	// FCMOVNU ST0,fpureg
	[Test]
	public void FCMOVNU_ST0_fpureg ()
	{
		// FCMOVNU ST0, ST4
		// FCMOVNU_ST0 (FP.ST4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCMOVNU_ST0 (FP.ST4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0xdc };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCMOVNU ST0, ST4' failed.");
	}

	// FCMOVU fpureg
	[Test]
	public void FCMOVU_fpureg ()
	{
		// FCMOVU ST0
		// FCMOVU (FP.ST0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCMOVU (FP.ST0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xda, 0xd8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCMOVU ST0' failed.");
	}

	// FCMOVU ST0,fpureg
	[Test]
	public void FCMOVU_ST0_fpureg ()
	{
		// FCMOVU ST0, ST2
		// FCMOVU_ST0 (FP.ST2)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCMOVU_ST0 (FP.ST2);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xda, 0xda };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCMOVU ST0, ST2' failed.");
	}

	// FCOM mem32
	[Test]
	public void FCOM_mem32 ()
	{
		// FCOM DWord [0x12345678]
		// FCOM (new DWordMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCOM (new DWordMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd8, 0x15, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCOM DWord [0x12345678]' failed.");
	}

	// FCOM mem64
	[Test]
	public void FCOM_mem64 ()
	{
		// FCOM QWord [ES:EBP*4]
		// FCOM (new QWordMemory(Seg.ES, null, R32.EBP, 2))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCOM (new QWordMemory (Seg.ES, null, R32.EBP, 2));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xdc, 0x14, 0xad, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCOM QWord [ES:EBP*4]' failed.");
	}

	// FCOM fpureg
	[Test]
	public void FCOM_fpureg ()
	{
		// FCOM ST5
		// FCOM (FP.ST5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCOM (FP.ST5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd8, 0xd5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCOM ST5' failed.");
	}

	// FCOM ST0,fpureg
	[Test]
	public void FCOM_ST0_fpureg ()
	{
		// FCOM ST0, ST5
		// FCOM_ST0 (FP.ST5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCOM_ST0 (FP.ST5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd8, 0xd5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCOM ST0, ST5' failed.");
	}

	// FCOMI fpureg
	[Test]
	public void FCOMI_fpureg ()
	{
		// FCOMI ST7
		// FCOMI (FP.ST7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCOMI (FP.ST7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0xf7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCOMI ST7' failed.");
	}

	// FCOMI ST0,fpureg
	[Test]
	public void FCOMI_ST0_fpureg ()
	{
		// FCOMI ST0, ST7
		// FCOMI_ST0 (FP.ST7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCOMI_ST0 (FP.ST7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0xf7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCOMI ST0, ST7' failed.");
	}

	// FCOMIP fpureg
	[Test]
	public void FCOMIP_fpureg ()
	{
		// FCOMIP ST3
		// FCOMIP (FP.ST3)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCOMIP (FP.ST3);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdf, 0xf3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCOMIP ST3' failed.");
	}

	// FCOMIP ST0,fpureg
	[Test]
	public void FCOMIP_ST0_fpureg ()
	{
		// FCOMIP ST0, ST0
		// FCOMIP_ST0 (FP.ST0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCOMIP_ST0 (FP.ST0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdf, 0xf0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCOMIP ST0, ST0' failed.");
	}

	// FCOMP mem32
	[Test]
	public void FCOMP_mem32 ()
	{
		// FCOMP DWord [ES:0x12345678]
		// FCOMP (new DWordMemory(Seg.ES, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCOMP (new DWordMemory (Seg.ES, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xd8, 0x1d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCOMP DWord [ES:0x12345678]' failed.");
	}

	// FCOMP mem64
	[Test]
	public void FCOMP_mem64 ()
	{
		// FCOMP QWord [FS:0x12345678]
		// FCOMP (new QWordMemory(Seg.FS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCOMP (new QWordMemory (Seg.FS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xdc, 0x1d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCOMP QWord [FS:0x12345678]' failed.");
	}

	// FCOMP fpureg
	[Test]
	public void FCOMP_fpureg ()
	{
		// FCOMP ST5
		// FCOMP (FP.ST5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCOMP (FP.ST5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd8, 0xdd };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCOMP ST5' failed.");
	}

	// FCOMP ST0,fpureg
	[Test]
	public void FCOMP_ST0_fpureg ()
	{
		// FCOMP ST0, ST7
		// FCOMP_ST0 (FP.ST7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCOMP_ST0 (FP.ST7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd8, 0xdf };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCOMP ST0, ST7' failed.");
	}

	// FCOMPP 
	[Test]
	public void FCOMPP ()
	{
		// FCOMPP
		// FCOMPP ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCOMPP ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xde, 0xd9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCOMPP' failed.");
	}

	// FCOS 
	[Test]
	public void FCOS ()
	{
		// FCOS
		// FCOS ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FCOS ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xff };
		Assert.IsTrue (CompareData (memoryStream, target), "'FCOS' failed.");
	}

	// FDECSTP 
	[Test]
	public void FDECSTP ()
	{
		// FDECSTP
		// FDECSTP ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FDECSTP ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xf6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FDECSTP' failed.");
	}

	// FDISI 
	[Test]
	public void FDISI ()
	{
		// FDISI
		// FDISI ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FDISI ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x9b, 0xdb, 0xe1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FDISI' failed.");
	}

	// FDIV mem32
	[Test]
	public void FDIV_mem32 ()
	{
		// FDIV DWord [DS:0x12345678]
		// FDIV (new DWordMemory(Seg.DS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FDIV (new DWordMemory (Seg.DS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xd8, 0x35, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FDIV DWord [DS:0x12345678]' failed.");
	}

	// FDIV mem64
	[Test]
	public void FDIV_mem64 ()
	{
		// FDIV QWord [ESI*1 + 0x12345678]
		// FDIV (new QWordMemory(null, null, R32.ESI, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FDIV (new QWordMemory (null, null, R32.ESI, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdc, 0xb6, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FDIV QWord [ESI*1 + 0x12345678]' failed.");
	}

	// FDIV fpureg
	[Test]
	public void FDIV_fpureg ()
	{
		// FDIV ST1
		// FDIV (FP.ST1)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FDIV (FP.ST1);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd8, 0xf1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FDIV ST1' failed.");
	}

	// FDIV ST0,fpureg
	[Test]
	public void FDIV_ST0_fpureg ()
	{
		// FDIV ST0, ST3
		// FDIV_ST0 (FP.ST3)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FDIV_ST0 (FP.ST3);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd8, 0xf3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FDIV ST0, ST3' failed.");
	}

	// FDIV fpureg,ST0
	[Test]
	public void FDIV_fpureg_ST0 ()
	{
		// FDIV ST0, ST0
		// FDIV__ST0 (FP.ST0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FDIV__ST0 (FP.ST0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdc, 0xf8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FDIV ST0, ST0' failed.");
	}

	// FDIVP fpureg
	[Test]
	public void FDIVP_fpureg ()
	{
		// FDIVP ST2
		// FDIVP (FP.ST2)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FDIVP (FP.ST2);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xde, 0xfa };
		Assert.IsTrue (CompareData (memoryStream, target), "'FDIVP ST2' failed.");
	}

	// FDIVP fpureg,ST0
	[Test]
	public void FDIVP_fpureg_ST0 ()
	{
		// FDIVP ST6, ST0
		// FDIVP__ST0 (FP.ST6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FDIVP__ST0 (FP.ST6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xde, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'FDIVP ST6, ST0' failed.");
	}

	// FDIVR mem32
	[Test]
	public void FDIVR_mem32 ()
	{
		// FDIVR DWord [ES:0x12345678]
		// FDIVR (new DWordMemory(Seg.ES, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FDIVR (new DWordMemory (Seg.ES, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xd8, 0x3d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FDIVR DWord [ES:0x12345678]' failed.");
	}

	// FDIVR mem64
	[Test]
	public void FDIVR_mem64 ()
	{
		// FDIVR QWord [ECX + 0x12345678]
		// FDIVR (new QWordMemory(null, R32.ECX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FDIVR (new QWordMemory (null, R32.ECX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdc, 0xb9, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FDIVR QWord [ECX + 0x12345678]' failed.");
	}

	// FDIVR fpureg
	[Test]
	public void FDIVR_fpureg ()
	{
		// FDIVR ST4
		// FDIVR (FP.ST4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FDIVR (FP.ST4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd8, 0xfc };
		Assert.IsTrue (CompareData (memoryStream, target), "'FDIVR ST4' failed.");
	}

	// FDIVR ST0,fpureg
	[Test]
	public void FDIVR_ST0_fpureg ()
	{
		// FDIVR ST0, ST4
		// FDIVR_ST0 (FP.ST4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FDIVR_ST0 (FP.ST4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd8, 0xfc };
		Assert.IsTrue (CompareData (memoryStream, target), "'FDIVR ST0, ST4' failed.");
	}

	// FDIVR fpureg,ST0
	[Test]
	public void FDIVR_fpureg_ST0 ()
	{
		// FDIVR ST7, ST0
		// FDIVR__ST0 (FP.ST7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FDIVR__ST0 (FP.ST7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdc, 0xf7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FDIVR ST7, ST0' failed.");
	}

	// FDIVRP fpureg
	[Test]
	public void FDIVRP_fpureg ()
	{
		// FDIVRP ST1
		// FDIVRP (FP.ST1)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FDIVRP (FP.ST1);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xde, 0xf1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FDIVRP ST1' failed.");
	}

	// FDIVRP fpureg,ST0
	[Test]
	public void FDIVRP_fpureg_ST0 ()
	{
		// FDIVRP ST4, ST0
		// FDIVRP__ST0 (FP.ST4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FDIVRP__ST0 (FP.ST4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xde, 0xf4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FDIVRP ST4, ST0' failed.");
	}

	// FENI 
	[Test]
	public void FENI ()
	{
		// FENI
		// FENI ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FENI ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x9b, 0xdb, 0xe0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FENI' failed.");
	}

	// FFREE fpureg
	[Test]
	public void FFREE_fpureg ()
	{
		// FFREE ST4
		// FFREE (FP.ST4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FFREE (FP.ST4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdd, 0xc4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FFREE ST4' failed.");
	}

	// FFREEP fpureg
	[Test]
	public void FFREEP_fpureg ()
	{
		// FFREEP ST1
		// FFREEP (FP.ST1)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FFREEP (FP.ST1);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdf, 0xc1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FFREEP ST1' failed.");
	}

	// FIADD mem16
	[Test]
	public void FIADD_mem16 ()
	{
		// FIADD Word [ECX]
		// FIADD (new WordMemory(null, R32.ECX, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FIADD (new WordMemory (null, R32.ECX, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xde, 0x1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FIADD Word [ECX]' failed.");
	}

	// FIADD mem32
	[Test]
	public void FIADD_mem32 ()
	{
		// FIADD DWord [EBP + 0x12345678]
		// FIADD (new DWordMemory(null, R32.EBP, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FIADD (new DWordMemory (null, R32.EBP, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xda, 0x85, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FIADD DWord [EBP + 0x12345678]' failed.");
	}

	// FICOM mem16
	[Test]
	public void FICOM_mem16 ()
	{
		// FICOM Word [ESI + ECX*2]
		// FICOM (new WordMemory(null, R32.ESI, R32.ECX, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FICOM (new WordMemory (null, R32.ESI, R32.ECX, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xde, 0x14, 0x4e };
		Assert.IsTrue (CompareData (memoryStream, target), "'FICOM Word [ESI + ECX*2]' failed.");
	}

	// FICOM mem32
	[Test]
	public void FICOM_mem32 ()
	{
		// FICOM DWord [ESI + ESI*2]
		// FICOM (new DWordMemory(null, R32.ESI, R32.ESI, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FICOM (new DWordMemory (null, R32.ESI, R32.ESI, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xda, 0x14, 0x76 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FICOM DWord [ESI + ESI*2]' failed.");
	}

	// FICOMP mem16
	[Test]
	public void FICOMP_mem16 ()
	{
		// FICOMP Word [ES:ECX + EBP*2]
		// FICOMP (new WordMemory(Seg.ES, R32.ECX, R32.EBP, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FICOMP (new WordMemory (Seg.ES, R32.ECX, R32.EBP, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xde, 0x1c, 0x69 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FICOMP Word [ES:ECX + EBP*2]' failed.");
	}

	// FICOMP mem32
	[Test]
	public void FICOMP_mem32 ()
	{
		// FICOMP DWord [DS:EBX*2]
		// FICOMP (new DWordMemory(Seg.DS, null, R32.EBX, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FICOMP (new DWordMemory (Seg.DS, null, R32.EBX, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xda, 0x1c, 0x1b };
		Assert.IsTrue (CompareData (memoryStream, target), "'FICOMP DWord [DS:EBX*2]' failed.");
	}

	// FIDIV mem16
	[Test]
	public void FIDIV_mem16 ()
	{
		// FIDIV Word [CS:0x12345678]
		// FIDIV (new WordMemory(Seg.CS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FIDIV (new WordMemory (Seg.CS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xde, 0x35, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FIDIV Word [CS:0x12345678]' failed.");
	}

	// FIDIV mem32
	[Test]
	public void FIDIV_mem32 ()
	{
		// FIDIV DWord [FS:EAX*1]
		// FIDIV (new DWordMemory(Seg.FS, null, R32.EAX, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FIDIV (new DWordMemory (Seg.FS, null, R32.EAX, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xda, 0x30 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FIDIV DWord [FS:EAX*1]' failed.");
	}

	// FIDIVR mem16
	[Test]
	public void FIDIVR_mem16 ()
	{
		// FIDIVR Word [EBP*1 + 0x12345678]
		// FIDIVR (new WordMemory(null, null, R32.EBP, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FIDIVR (new WordMemory (null, null, R32.EBP, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xde, 0xbd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FIDIVR Word [EBP*1 + 0x12345678]' failed.");
	}

	// FIDIVR mem32
	[Test]
	public void FIDIVR_mem32 ()
	{
		// FIDIVR DWord [SS:EBP*2 + 0x12345678]
		// FIDIVR (new DWordMemory(Seg.SS, null, R32.EBP, 1, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FIDIVR (new DWordMemory (Seg.SS, null, R32.EBP, 1, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0xda, 0xbc, 0x2d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FIDIVR DWord [SS:EBP*2 + 0x12345678]' failed.");
	}

	// FILD mem16
	[Test]
	public void FILD_mem16 ()
	{
		// FILD Word [ESI + 0x12345678]
		// FILD (new WordMemory(null, R32.ESI, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FILD (new WordMemory (null, R32.ESI, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdf, 0x86, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FILD Word [ESI + 0x12345678]' failed.");
	}

	// FILD mem32
	[Test]
	public void FILD_mem32 ()
	{
		// FILD DWord [GS:ECX]
		// FILD (new DWordMemory(Seg.GS, R32.ECX, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FILD (new DWordMemory (Seg.GS, R32.ECX, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xdb, 0x1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FILD DWord [GS:ECX]' failed.");
	}

	// FILD mem64
	[Test]
	public void FILD_mem64 ()
	{
		// FILD QWord [EAX + ECX*8 + 0x12345678]
		// FILD (new QWordMemory(null, R32.EAX, R32.ECX, 3, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FILD (new QWordMemory (null, R32.EAX, R32.ECX, 3, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdf, 0xac, 0xc8, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FILD QWord [EAX + ECX*8 + 0x12345678]' failed.");
	}

	// FIMUL mem16
	[Test]
	public void FIMUL_mem16 ()
	{
		// FIMUL Word [0x12345678]
		// FIMUL (new WordMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FIMUL (new WordMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xde, 0xd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FIMUL Word [0x12345678]' failed.");
	}

	// FIMUL mem32
	[Test]
	public void FIMUL_mem32 ()
	{
		// FIMUL DWord [CS:0x12345678]
		// FIMUL (new DWordMemory(Seg.CS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FIMUL (new DWordMemory (Seg.CS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xda, 0xd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FIMUL DWord [CS:0x12345678]' failed.");
	}

	// FINCSTP 
	[Test]
	public void FINCSTP ()
	{
		// FINCSTP
		// FINCSTP ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FINCSTP ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xf7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FINCSTP' failed.");
	}

	// FINIT 
	[Test]
	public void FINIT ()
	{
		// FINIT
		// FINIT ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FINIT ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x9b, 0xdb, 0xe3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FINIT' failed.");
	}

	// FIST mem16
	[Test]
	public void FIST_mem16 ()
	{
		// FIST Word [ESP + EDI*1]
		// FIST (new WordMemory(null, R32.ESP, R32.EDI, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FIST (new WordMemory (null, R32.ESP, R32.EDI, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdf, 0x14, 0x3c };
		Assert.IsTrue (CompareData (memoryStream, target), "'FIST Word [ESP + EDI*1]' failed.");
	}

	// FIST mem32
	[Test]
	public void FIST_mem32 ()
	{
		// FIST DWord [CS:EBP*4 + 0x12345678]
		// FIST (new DWordMemory(Seg.CS, null, R32.EBP, 2, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FIST (new DWordMemory (Seg.CS, null, R32.EBP, 2, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xdb, 0x14, 0xad, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FIST DWord [CS:EBP*4 + 0x12345678]' failed.");
	}

	// FISTP mem16
	[Test]
	public void FISTP_mem16 ()
	{
		// FISTP Word [CS:EAX*1 + 0x12345678]
		// FISTP (new WordMemory(Seg.CS, null, R32.EAX, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FISTP (new WordMemory (Seg.CS, null, R32.EAX, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xdf, 0x98, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FISTP Word [CS:EAX*1 + 0x12345678]' failed.");
	}

	// FISTP mem32
	[Test]
	public void FISTP_mem32 ()
	{
		// FISTP DWord [0x12345678]
		// FISTP (new DWordMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FISTP (new DWordMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0x1d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FISTP DWord [0x12345678]' failed.");
	}

	// FISTP mem64
	[Test]
	public void FISTP_mem64 ()
	{
		// FISTP QWord [GS:ESP + EDX*8]
		// FISTP (new QWordMemory(Seg.GS, R32.ESP, R32.EDX, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FISTP (new QWordMemory (Seg.GS, R32.ESP, R32.EDX, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xdf, 0x3c, 0xd4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FISTP QWord [GS:ESP + EDX*8]' failed.");
	}

	// FISUB mem16
	[Test]
	public void FISUB_mem16 ()
	{
		// FISUB Word [EAX + 0x12345678]
		// FISUB (new WordMemory(null, R32.EAX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FISUB (new WordMemory (null, R32.EAX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xde, 0xa0, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FISUB Word [EAX + 0x12345678]' failed.");
	}

	// FISUB mem32
	[Test]
	public void FISUB_mem32 ()
	{
		// FISUB DWord [GS:EBP + 0x12345678]
		// FISUB (new DWordMemory(Seg.GS, R32.EBP, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FISUB (new DWordMemory (Seg.GS, R32.EBP, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xda, 0xa5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FISUB DWord [GS:EBP + 0x12345678]' failed.");
	}

	// FISUBR mem16
	[Test]
	public void FISUBR_mem16 ()
	{
		// FISUBR Word [GS:EDX*1]
		// FISUBR (new WordMemory(Seg.GS, null, R32.EDX, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FISUBR (new WordMemory (Seg.GS, null, R32.EDX, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xde, 0x2a };
		Assert.IsTrue (CompareData (memoryStream, target), "'FISUBR Word [GS:EDX*1]' failed.");
	}

	// FISUBR mem32
	[Test]
	public void FISUBR_mem32 ()
	{
		// FISUBR DWord [ESI + 0x12345678]
		// FISUBR (new DWordMemory(null, R32.ESI, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FISUBR (new DWordMemory (null, R32.ESI, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xda, 0xae, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FISUBR DWord [ESI + 0x12345678]' failed.");
	}

	// FLD mem32
	[Test]
	public void FLD_mem32 ()
	{
		// FLD DWord [FS:ECX]
		// FLD (new DWordMemory(Seg.FS, R32.ECX, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FLD (new DWordMemory (Seg.FS, R32.ECX, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xd9, 0x1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FLD DWord [FS:ECX]' failed.");
	}

	// FLD mem64
	[Test]
	public void FLD_mem64 ()
	{
		// FLD QWord [GS:0x12345678]
		// FLD (new QWordMemory(Seg.GS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FLD (new QWordMemory (Seg.GS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xdd, 0x5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FLD QWord [GS:0x12345678]' failed.");
	}

	// FLD mem80
	[Test]
	public void FLD_mem80 ()
	{
		// FLD TWord [DS:ECX]
		// FLD (new TWordMemory(Seg.DS, R32.ECX, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FLD (new TWordMemory (Seg.DS, R32.ECX, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xdb, 0x29 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FLD TWord [DS:ECX]' failed.");
	}

	// FLD fpureg
	[Test]
	public void FLD_fpureg ()
	{
		// FLD ST3
		// FLD (FP.ST3)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FLD (FP.ST3);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xc3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FLD ST3' failed.");
	}

	// FLD1 
	[Test]
	public void FLD1 ()
	{
		// FLD1
		// FLD1 ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FLD1 ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xe8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FLD1' failed.");
	}

	// FLDCW mem16
	[Test]
	public void FLDCW_mem16 ()
	{
		// FLDCW Word [ESP + 0x12345678]
		// FLDCW (new WordMemory(null, R32.ESP, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FLDCW (new WordMemory (null, R32.ESP, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xac, 0x24, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FLDCW Word [ESP + 0x12345678]' failed.");
	}

	// FLDENV mem
	[Test]
	public void FLDENV_mem ()
	{
		// FLDENV [GS:EDI + 0x12345678]
		// FLDENV (new DWordMemory(Seg.GS, R32.EDI, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FLDENV (new DWordMemory (Seg.GS, R32.EDI, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xd9, 0xa7, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FLDENV [GS:EDI + 0x12345678]' failed.");
	}

	// FLDL2E 
	[Test]
	public void FLDL2E ()
	{
		// FLDL2E
		// FLDL2E ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FLDL2E ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xea };
		Assert.IsTrue (CompareData (memoryStream, target), "'FLDL2E' failed.");
	}

	// FLDL2T 
	[Test]
	public void FLDL2T ()
	{
		// FLDL2T
		// FLDL2T ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FLDL2T ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xe9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FLDL2T' failed.");
	}

	// FLDLG2 
	[Test]
	public void FLDLG2 ()
	{
		// FLDLG2
		// FLDLG2 ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FLDLG2 ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xec };
		Assert.IsTrue (CompareData (memoryStream, target), "'FLDLG2' failed.");
	}

	// FLDLN2 
	[Test]
	public void FLDLN2 ()
	{
		// FLDLN2
		// FLDLN2 ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FLDLN2 ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xed };
		Assert.IsTrue (CompareData (memoryStream, target), "'FLDLN2' failed.");
	}

	// FLDPI 
	[Test]
	public void FLDPI ()
	{
		// FLDPI
		// FLDPI ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FLDPI ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xeb };
		Assert.IsTrue (CompareData (memoryStream, target), "'FLDPI' failed.");
	}

	// FLDZ 
	[Test]
	public void FLDZ ()
	{
		// FLDZ
		// FLDZ ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FLDZ ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xee };
		Assert.IsTrue (CompareData (memoryStream, target), "'FLDZ' failed.");
	}

	// FMUL mem32
	[Test]
	public void FMUL_mem32 ()
	{
		// FMUL DWord [GS:0x12345678]
		// FMUL (new DWordMemory(Seg.GS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FMUL (new DWordMemory (Seg.GS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xd8, 0xd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FMUL DWord [GS:0x12345678]' failed.");
	}

	// FMUL mem64
	[Test]
	public void FMUL_mem64 ()
	{
		// FMUL QWord [EBP + EBP*8]
		// FMUL (new QWordMemory(null, R32.EBP, R32.EBP, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FMUL (new QWordMemory (null, R32.EBP, R32.EBP, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdc, 0x4c, 0xed, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FMUL QWord [EBP + EBP*8]' failed.");
	}

	// FMUL fpureg
	[Test]
	public void FMUL_fpureg ()
	{
		// FMUL ST5
		// FMUL (FP.ST5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FMUL (FP.ST5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd8, 0xcd };
		Assert.IsTrue (CompareData (memoryStream, target), "'FMUL ST5' failed.");
	}

	// FMUL ST0,fpureg
	[Test]
	public void FMUL_ST0_fpureg ()
	{
		// FMUL ST0, ST0
		// FMUL_ST0 (FP.ST0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FMUL_ST0 (FP.ST0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd8, 0xc8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FMUL ST0, ST0' failed.");
	}

	// FMUL fpureg,ST0
	[Test]
	public void FMUL_fpureg_ST0 ()
	{
		// FMUL ST4, ST0
		// FMUL__ST0 (FP.ST4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FMUL__ST0 (FP.ST4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdc, 0xcc };
		Assert.IsTrue (CompareData (memoryStream, target), "'FMUL ST4, ST0' failed.");
	}

	// FMULP fpureg
	[Test]
	public void FMULP_fpureg ()
	{
		// FMULP ST1
		// FMULP (FP.ST1)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FMULP (FP.ST1);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xde, 0xc9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FMULP ST1' failed.");
	}

	// FMULP fpureg,ST0
	[Test]
	public void FMULP_fpureg_ST0 ()
	{
		// FMULP ST6, ST0
		// FMULP__ST0 (FP.ST6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FMULP__ST0 (FP.ST6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xde, 0xce };
		Assert.IsTrue (CompareData (memoryStream, target), "'FMULP ST6, ST0' failed.");
	}

	// FNCLEX 
	[Test]
	public void FNCLEX ()
	{
		// FNCLEX
		// FNCLEX ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FNCLEX ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0xe2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FNCLEX' failed.");
	}

	// FNDISI 
	[Test]
	public void FNDISI ()
	{
		// FNDISI
		// FNDISI ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FNDISI ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0xe1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FNDISI' failed.");
	}

	// FNENI 
	[Test]
	public void FNENI ()
	{
		// FNENI
		// FNENI ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FNENI ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0xe0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FNENI' failed.");
	}

	// FNINIT 
	[Test]
	public void FNINIT ()
	{
		// FNINIT
		// FNINIT ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FNINIT ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0xe3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FNINIT' failed.");
	}

	// FNOP 
	[Test]
	public void FNOP ()
	{
		// FNOP
		// FNOP ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FNOP ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xd0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FNOP' failed.");
	}

	// FNSAVE mem
	[Test]
	public void FNSAVE_mem ()
	{
		// FNSAVE [EDX + 0x12345678]
		// FNSAVE (new DWordMemory(null, R32.EDX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FNSAVE (new DWordMemory (null, R32.EDX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdd, 0xb2, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FNSAVE [EDX + 0x12345678]' failed.");
	}

	// FNSTCW mem16
	[Test]
	public void FNSTCW_mem16 ()
	{
		// FNSTCW Word [DS:EBX + 0x12345678]
		// FNSTCW (new WordMemory(Seg.DS, R32.EBX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FNSTCW (new WordMemory (Seg.DS, R32.EBX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xd9, 0xbb, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FNSTCW Word [DS:EBX + 0x12345678]' failed.");
	}

	// FNSTENV mem
	[Test]
	public void FNSTENV_mem ()
	{
		// FNSTENV [DS:ESI + EBX*1]
		// FNSTENV (new DWordMemory(Seg.DS, R32.ESI, R32.EBX, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FNSTENV (new DWordMemory (Seg.DS, R32.ESI, R32.EBX, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xd9, 0x34, 0x1e };
		Assert.IsTrue (CompareData (memoryStream, target), "'FNSTENV [DS:ESI + EBX*1]' failed.");
	}

	// FNSTSW mem16
	[Test]
	public void FNSTSW_mem16 ()
	{
		// FNSTSW Word [ES:EDI + 0x12345678]
		// FNSTSW (new WordMemory(Seg.ES, R32.EDI, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FNSTSW (new WordMemory (Seg.ES, R32.EDI, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xdd, 0xbf, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FNSTSW Word [ES:EDI + 0x12345678]' failed.");
	}

	// FNSTSW AX
	[Test]
	public void FNSTSW_AX ()
	{
		// FNSTSW AX
		// FNSTSW_AX ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FNSTSW_AX ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdf, 0xe0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FNSTSW AX' failed.");
	}

	// FPATAN 
	[Test]
	public void FPATAN ()
	{
		// FPATAN
		// FPATAN ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FPATAN ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xf3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FPATAN' failed.");
	}

	// FPREM 
	[Test]
	public void FPREM ()
	{
		// FPREM
		// FPREM ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FPREM ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xf8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FPREM' failed.");
	}

	// FPREM1 
	[Test]
	public void FPREM1 ()
	{
		// FPREM1
		// FPREM1 ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FPREM1 ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xf5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FPREM1' failed.");
	}

	// FPTAN 
	[Test]
	public void FPTAN ()
	{
		// FPTAN
		// FPTAN ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FPTAN ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xf2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FPTAN' failed.");
	}

	// FRNDINT 
	[Test]
	public void FRNDINT ()
	{
		// FRNDINT
		// FRNDINT ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FRNDINT ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xfc };
		Assert.IsTrue (CompareData (memoryStream, target), "'FRNDINT' failed.");
	}

	// FRSTOR mem
	[Test]
	public void FRSTOR_mem ()
	{
		// FRSTOR [GS:0x12345678]
		// FRSTOR (new DWordMemory(Seg.GS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FRSTOR (new DWordMemory (Seg.GS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xdd, 0x25, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FRSTOR [GS:0x12345678]' failed.");
	}

	// FSAVE mem
	[Test]
	public void FSAVE_mem ()
	{
		// FSAVE [EBP*8 + 0x12345678]
		// FSAVE (new DWordMemory(null, null, R32.EBP, 3, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSAVE (new DWordMemory (null, null, R32.EBP, 3, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x9b, 0xdd, 0x34, 0xed, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSAVE [EBP*8 + 0x12345678]' failed.");
	}

	// FSCALE 
	[Test]
	public void FSCALE ()
	{
		// FSCALE
		// FSCALE ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSCALE ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xfd };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSCALE' failed.");
	}

	// FSETPM 
	[Test]
	public void FSETPM ()
	{
		// FSETPM
		// FSETPM ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSETPM ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0xe4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSETPM' failed.");
	}

	// FSIN 
	[Test]
	public void FSIN ()
	{
		// FSIN
		// FSIN ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSIN ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSIN' failed.");
	}

	// FSINCOS 
	[Test]
	public void FSINCOS ()
	{
		// FSINCOS
		// FSINCOS ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSINCOS ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xfb };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSINCOS' failed.");
	}

	// FSQRT 
	[Test]
	public void FSQRT ()
	{
		// FSQRT
		// FSQRT ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSQRT ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xfa };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSQRT' failed.");
	}

	// FST mem32
	[Test]
	public void FST_mem32 ()
	{
		// FST DWord [EAX*2]
		// FST (new DWordMemory(null, null, R32.EAX, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FST (new DWordMemory (null, null, R32.EAX, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0x14, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FST DWord [EAX*2]' failed.");
	}

	// FST mem64
	[Test]
	public void FST_mem64 ()
	{
		// FST QWord [ES:0x12345678]
		// FST (new QWordMemory(Seg.ES, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FST (new QWordMemory (Seg.ES, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xdd, 0x15, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FST QWord [ES:0x12345678]' failed.");
	}

	// FST fpureg
	[Test]
	public void FST_fpureg ()
	{
		// FST ST1
		// FST (FP.ST1)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FST (FP.ST1);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdd, 0xd1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FST ST1' failed.");
	}

	// FSTCW mem16
	[Test]
	public void FSTCW_mem16 ()
	{
		// FSTCW Word [EBP*4 + 0x12345678]
		// FSTCW (new WordMemory(null, null, R32.EBP, 2, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSTCW (new WordMemory (null, null, R32.EBP, 2, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x9b, 0xd9, 0x3c, 0xad, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSTCW Word [EBP*4 + 0x12345678]' failed.");
	}

	// FSTENV mem
	[Test]
	public void FSTENV_mem ()
	{
		// FSTENV [ES:0x12345678]
		// FSTENV (new DWordMemory(Seg.ES, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSTENV (new DWordMemory (Seg.ES, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0x9b, 0xd9, 0x35, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSTENV [ES:0x12345678]' failed.");
	}

	// FSTP mem32
	[Test]
	public void FSTP_mem32 ()
	{
		// FSTP DWord [DS:ECX + 0x12345678]
		// FSTP (new DWordMemory(Seg.DS, R32.ECX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSTP (new DWordMemory (Seg.DS, R32.ECX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xd9, 0x99, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSTP DWord [DS:ECX + 0x12345678]' failed.");
	}

	// FSTP mem64
	[Test]
	public void FSTP_mem64 ()
	{
		// FSTP QWord [ES:ESP + EDX*2]
		// FSTP (new QWordMemory(Seg.ES, R32.ESP, R32.EDX, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSTP (new QWordMemory (Seg.ES, R32.ESP, R32.EDX, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xdd, 0x1c, 0x54 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSTP QWord [ES:ESP + EDX*2]' failed.");
	}

	// FSTP mem80
	[Test]
	public void FSTP_mem80 ()
	{
		// FSTP TWord [EBX + 0x12345678]
		// FSTP (new TWordMemory(null, R32.EBX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSTP (new TWordMemory (null, R32.EBX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0xbb, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSTP TWord [EBX + 0x12345678]' failed.");
	}

	// FSTP fpureg
	[Test]
	public void FSTP_fpureg ()
	{
		// FSTP ST0
		// FSTP (FP.ST0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSTP (FP.ST0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdd, 0xd8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSTP ST0' failed.");
	}

	// FSTSW mem16
	[Test]
	public void FSTSW_mem16 ()
	{
		// FSTSW Word [EBX + 0x12345678]
		// FSTSW (new WordMemory(null, R32.EBX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSTSW (new WordMemory (null, R32.EBX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x9b, 0xdd, 0xbb, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSTSW Word [EBX + 0x12345678]' failed.");
	}

	// FSTSW AX
	[Test]
	public void FSTSW_AX ()
	{
		// FSTSW AX
		// FSTSW_AX ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSTSW_AX ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x9b, 0xdf, 0xe0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSTSW AX' failed.");
	}

	// FSUB mem32
	[Test]
	public void FSUB_mem32 ()
	{
		// FSUB DWord [CS:0x12345678]
		// FSUB (new DWordMemory(Seg.CS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSUB (new DWordMemory (Seg.CS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xd8, 0x25, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSUB DWord [CS:0x12345678]' failed.");
	}

	// FSUB mem64
	[Test]
	public void FSUB_mem64 ()
	{
		// FSUB QWord [EDX*2 + 0x12345678]
		// FSUB (new QWordMemory(null, null, R32.EDX, 1, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSUB (new QWordMemory (null, null, R32.EDX, 1, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdc, 0xa4, 0x12, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSUB QWord [EDX*2 + 0x12345678]' failed.");
	}

	// FSUB fpureg
	[Test]
	public void FSUB_fpureg ()
	{
		// FSUB ST6
		// FSUB (FP.ST6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSUB (FP.ST6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd8, 0xe6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSUB ST6' failed.");
	}

	// FSUB ST0,fpureg
	[Test]
	public void FSUB_ST0_fpureg ()
	{
		// FSUB ST0, ST7
		// FSUB_ST0 (FP.ST7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSUB_ST0 (FP.ST7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd8, 0xe7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSUB ST0, ST7' failed.");
	}

	// FSUB fpureg,ST0
	[Test]
	public void FSUB_fpureg_ST0 ()
	{
		// FSUB ST6, ST0
		// FSUB__ST0 (FP.ST6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSUB__ST0 (FP.ST6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdc, 0xee };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSUB ST6, ST0' failed.");
	}

	// FSUBP fpureg
	[Test]
	public void FSUBP_fpureg ()
	{
		// FSUBP ST1
		// FSUBP (FP.ST1)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSUBP (FP.ST1);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xde, 0xe9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSUBP ST1' failed.");
	}

	// FSUBP fpureg,ST0
	[Test]
	public void FSUBP_fpureg_ST0 ()
	{
		// FSUBP ST7, ST0
		// FSUBP__ST0 (FP.ST7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSUBP__ST0 (FP.ST7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xde, 0xef };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSUBP ST7, ST0' failed.");
	}

	// FSUBR mem32
	[Test]
	public void FSUBR_mem32 ()
	{
		// FSUBR DWord [FS:EAX + 0x12345678]
		// FSUBR (new DWordMemory(Seg.FS, R32.EAX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSUBR (new DWordMemory (Seg.FS, R32.EAX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xd8, 0xa8, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSUBR DWord [FS:EAX + 0x12345678]' failed.");
	}

	// FSUBR mem64
	[Test]
	public void FSUBR_mem64 ()
	{
		// FSUBR QWord [SS:0x12345678]
		// FSUBR (new QWordMemory(Seg.SS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSUBR (new QWordMemory (Seg.SS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0xdc, 0x2d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSUBR QWord [SS:0x12345678]' failed.");
	}

	// FSUBR fpureg
	[Test]
	public void FSUBR_fpureg ()
	{
		// FSUBR ST0
		// FSUBR (FP.ST0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSUBR (FP.ST0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd8, 0xe8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSUBR ST0' failed.");
	}

	// FSUBR ST0,fpureg
	[Test]
	public void FSUBR_ST0_fpureg ()
	{
		// FSUBR ST0, ST1
		// FSUBR_ST0 (FP.ST1)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSUBR_ST0 (FP.ST1);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd8, 0xe9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSUBR ST0, ST1' failed.");
	}

	// FSUBR fpureg,ST0
	[Test]
	public void FSUBR_fpureg_ST0 ()
	{
		// FSUBR ST7, ST0
		// FSUBR__ST0 (FP.ST7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSUBR__ST0 (FP.ST7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdc, 0xe7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSUBR ST7, ST0' failed.");
	}

	// FSUBRP fpureg
	[Test]
	public void FSUBRP_fpureg ()
	{
		// FSUBRP ST5
		// FSUBRP (FP.ST5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSUBRP (FP.ST5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xde, 0xe5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSUBRP ST5' failed.");
	}

	// FSUBRP fpureg,ST0
	[Test]
	public void FSUBRP_fpureg_ST0 ()
	{
		// FSUBRP ST5, ST0
		// FSUBRP__ST0 (FP.ST5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FSUBRP__ST0 (FP.ST5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xde, 0xe5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FSUBRP ST5, ST0' failed.");
	}

	// FTST 
	[Test]
	public void FTST ()
	{
		// FTST
		// FTST ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FTST ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xe4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FTST' failed.");
	}

	// FUCOM fpureg
	[Test]
	public void FUCOM_fpureg ()
	{
		// FUCOM ST3
		// FUCOM (FP.ST3)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FUCOM (FP.ST3);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdd, 0xe3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FUCOM ST3' failed.");
	}

	// FUCOM ST0,fpureg
	[Test]
	public void FUCOM_ST0_fpureg ()
	{
		// FUCOM ST0, ST3
		// FUCOM_ST0 (FP.ST3)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FUCOM_ST0 (FP.ST3);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdd, 0xe3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FUCOM ST0, ST3' failed.");
	}

	// FUCOMI fpureg
	[Test]
	public void FUCOMI_fpureg ()
	{
		// FUCOMI ST6
		// FUCOMI (FP.ST6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FUCOMI (FP.ST6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0xee };
		Assert.IsTrue (CompareData (memoryStream, target), "'FUCOMI ST6' failed.");
	}

	// FUCOMI ST0,fpureg
	[Test]
	public void FUCOMI_ST0_fpureg ()
	{
		// FUCOMI ST0, ST5
		// FUCOMI_ST0 (FP.ST5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FUCOMI_ST0 (FP.ST5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdb, 0xed };
		Assert.IsTrue (CompareData (memoryStream, target), "'FUCOMI ST0, ST5' failed.");
	}

	// FUCOMIP fpureg
	[Test]
	public void FUCOMIP_fpureg ()
	{
		// FUCOMIP ST5
		// FUCOMIP (FP.ST5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FUCOMIP (FP.ST5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdf, 0xed };
		Assert.IsTrue (CompareData (memoryStream, target), "'FUCOMIP ST5' failed.");
	}

	// FUCOMIP ST0,fpureg
	[Test]
	public void FUCOMIP_ST0_fpureg ()
	{
		// FUCOMIP ST0, ST5
		// FUCOMIP_ST0 (FP.ST5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FUCOMIP_ST0 (FP.ST5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdf, 0xed };
		Assert.IsTrue (CompareData (memoryStream, target), "'FUCOMIP ST0, ST5' failed.");
	}

	// FUCOMP fpureg
	[Test]
	public void FUCOMP_fpureg ()
	{
		// FUCOMP ST6
		// FUCOMP (FP.ST6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FUCOMP (FP.ST6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdd, 0xee };
		Assert.IsTrue (CompareData (memoryStream, target), "'FUCOMP ST6' failed.");
	}

	// FUCOMP ST0,fpureg
	[Test]
	public void FUCOMP_ST0_fpureg ()
	{
		// FUCOMP ST0, ST5
		// FUCOMP_ST0 (FP.ST5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FUCOMP_ST0 (FP.ST5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xdd, 0xed };
		Assert.IsTrue (CompareData (memoryStream, target), "'FUCOMP ST0, ST5' failed.");
	}

	// FUCOMPP 
	[Test]
	public void FUCOMPP ()
	{
		// FUCOMPP
		// FUCOMPP ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FUCOMPP ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xda, 0xe9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FUCOMPP' failed.");
	}

	// FWAIT 
	[Test]
	public void FWAIT ()
	{
		// FWAIT
		// FWAIT ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FWAIT ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x9b };
		Assert.IsTrue (CompareData (memoryStream, target), "'FWAIT' failed.");
	}

	// FXAM 
	[Test]
	public void FXAM ()
	{
		// FXAM
		// FXAM ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FXAM ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xe5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FXAM' failed.");
	}

	// FXCH 
	[Test]
	public void FXCH ()
	{
		// FXCH
		// FXCH ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FXCH ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xc9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FXCH' failed.");
	}

	// FXCH fpureg
	[Test]
	public void FXCH_fpureg ()
	{
		// FXCH ST0
		// FXCH (FP.ST0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FXCH (FP.ST0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xc8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FXCH ST0' failed.");
	}

	// FXCH fpureg,ST0
	[Test]
	public void FXCH_fpureg_ST0 ()
	{
		// FXCH ST3, ST0
		// FXCH__ST0 (FP.ST3)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FXCH__ST0 (FP.ST3);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xcb };
		Assert.IsTrue (CompareData (memoryStream, target), "'FXCH ST3, ST0' failed.");
	}

	// FXCH ST0,fpureg
	[Test]
	public void FXCH_ST0_fpureg ()
	{
		// FXCH ST0, ST2
		// FXCH_ST0 (FP.ST2)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FXCH_ST0 (FP.ST2);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xca };
		Assert.IsTrue (CompareData (memoryStream, target), "'FXCH ST0, ST2' failed.");
	}

	// FXRSTOR memory
	[Test]
	public void FXRSTOR_memory ()
	{
		// FXRSTOR [FS:0x12345678]
		// FXRSTOR (new ByteMemory(Seg.FS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FXRSTOR (new ByteMemory (Seg.FS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xf, 0xae, 0xd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FXRSTOR [FS:0x12345678]' failed.");
	}

	// FXSAVE memory
	[Test]
	public void FXSAVE_memory ()
	{
		// FXSAVE [DS:0x12345678]
		// FXSAVE (new ByteMemory(Seg.DS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FXSAVE (new ByteMemory (Seg.DS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xf, 0xae, 0x5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FXSAVE [DS:0x12345678]' failed.");
	}

	// FXTRACT 
	[Test]
	public void FXTRACT ()
	{
		// FXTRACT
		// FXTRACT ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FXTRACT ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xf4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FXTRACT' failed.");
	}

	// FYL2X 
	[Test]
	public void FYL2X ()
	{
		// FYL2X
		// FYL2X ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FYL2X ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xf1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FYL2X' failed.");
	}

	// FYL2XP1 
	[Test]
	public void FYL2XP1 ()
	{
		// FYL2XP1
		// FYL2XP1 ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.FYL2XP1 ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd9, 0xf9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'FYL2XP1' failed.");
	}

	// HLT 
	[Test]
	public void HLT ()
	{
		// HLT
		// HLT ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.HLT ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'HLT' failed.");
	}

	// ICEBP 
	[Test]
	public void ICEBP ()
	{
		// ICEBP
		// ICEBP ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ICEBP ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ICEBP' failed.");
	}

	// IDIV mem8
	[Test]
	public void IDIV_mem8 ()
	{
		// IDIV Byte [CS:0x12345678]
		// IDIV (new ByteMemory(Seg.CS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IDIV (new ByteMemory (Seg.CS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xf6, 0x3d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IDIV Byte [CS:0x12345678]' failed.");
	}

	// IDIV mem16
	[Test]
	public void IDIV_mem16 ()
	{
		// IDIV Word [DS:0x12345678]
		// IDIV (new WordMemory(Seg.DS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IDIV (new WordMemory (Seg.DS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x66, 0xf7, 0x3d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IDIV Word [DS:0x12345678]' failed.");
	}

	// IDIV mem32
	[Test]
	public void IDIV_mem32 ()
	{
		// IDIV DWord [GS:0x12345678]
		// IDIV (new DWordMemory(Seg.GS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IDIV (new DWordMemory (Seg.GS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xf7, 0x3d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IDIV DWord [GS:0x12345678]' failed.");
	}

	// IDIV rmreg8
	[Test]
	public void IDIV_rmreg8 ()
	{
		// IDIV BL
		// IDIV (R8.BL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IDIV (R8.BL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf6, 0xfb };
		Assert.IsTrue (CompareData (memoryStream, target), "'IDIV BL' failed.");
	}

	// IDIV rmreg16
	[Test]
	public void IDIV_rmreg16 ()
	{
		// IDIV SI
		// IDIV (R16.SI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IDIV (R16.SI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf7, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'IDIV SI' failed.");
	}

	// IDIV rmreg32
	[Test]
	public void IDIV_rmreg32 ()
	{
		// IDIV ESP
		// IDIV (R32.ESP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IDIV (R32.ESP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf7, 0xfc };
		Assert.IsTrue (CompareData (memoryStream, target), "'IDIV ESP' failed.");
	}

	// IMUL mem8
	[Test]
	public void IMUL_mem8 ()
	{
		// IMUL Byte [0x12345678]
		// IMUL (new ByteMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (new ByteMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf6, 0x2d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL Byte [0x12345678]' failed.");
	}

	// IMUL mem16
	[Test]
	public void IMUL_mem16 ()
	{
		// IMUL Word [EBP + 0x12345678]
		// IMUL (new WordMemory(null, R32.EBP, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (new WordMemory (null, R32.EBP, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf7, 0xad, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL Word [EBP + 0x12345678]' failed.");
	}

	// IMUL mem32
	[Test]
	public void IMUL_mem32 ()
	{
		// IMUL DWord [ESI*1 + 0x12345678]
		// IMUL (new DWordMemory(null, null, R32.ESI, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (new DWordMemory (null, null, R32.ESI, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf7, 0xae, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL DWord [ESI*1 + 0x12345678]' failed.");
	}

	// IMUL reg16,mem16
	[Test]
	public void IMUL_reg16_mem16 ()
	{
		// IMUL AX, [EAX + 0x12345678]
		// IMUL (R16.AX, new WordMemory(null, R32.EAX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R16.AX, new WordMemory (null, R32.EAX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xaf, 0x80, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL AX, [EAX + 0x12345678]' failed.");
	}

	// IMUL reg32,mem32
	[Test]
	public void IMUL_reg32_mem32 ()
	{
		// IMUL EDX, [ES:ECX + EBP*4]
		// IMUL (R32.EDX, new DWordMemory(Seg.ES, R32.ECX, R32.EBP, 2))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R32.EDX, new DWordMemory (Seg.ES, R32.ECX, R32.EBP, 2));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xf, 0xaf, 0x14, 0xa9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL EDX, [ES:ECX + EBP*4]' failed.");
	}

	// IMUL reg16,imm8
	[Test]
	public void IMUL_reg16_imm8 ()
	{
		// IMUL BP, 0xc
		// IMUL (R16.BP, 0xc)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R16.BP, 0xc);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x6b, 0xed, 0xc };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL BP, 0xc' failed.");
	}

	// IMUL reg16,imm16
	[Test]
	public void IMUL_reg16_imm16 ()
	{
		// IMUL BX, 0x916
		// IMUL (R16.BX, 0x916)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R16.BX, 0x916);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x69, 0xdb, 0x16, 0x9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL BX, 0x916' failed.");
	}

	// IMUL reg32,imm8
	[Test]
	public void IMUL_reg32_imm8 ()
	{
		// IMUL EBP, 0xd
		// IMUL (R32.EBP, 0xd)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R32.EBP, 0xd);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x6b, 0xed, 0xd };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL EBP, 0xd' failed.");
	}

	// IMUL reg32,imm32
	[Test]
	public void IMUL_reg32_imm32 ()
	{
		// IMUL ESP, 0xb4f535a
		// IMUL (R32.ESP, 0xb4f535a)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R32.ESP, 0xb4f535a);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x69, 0xe4, 0x5a, 0x53, 0x4f, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL ESP, 0xb4f535a' failed.");
	}

	// IMUL reg16,mem16,imm8
	[Test]
	public void IMUL_reg16_mem16_imm8 ()
	{
		// IMUL AX, [GS:EAX*4 + 0x12345678], 0x7
		// IMUL (R16.AX, new WordMemory(Seg.GS, null, R32.EAX, 2, 0x12345678), 0x7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R16.AX, new WordMemory (Seg.GS, null, R32.EAX, 2, 0x12345678), 0x7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0x66, 0x6b, 0x4, 0x85, 0x78, 0x56, 0x34, 0x12, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL AX, [GS:EAX*4 + 0x12345678], 0x7' failed.");
	}

	// IMUL reg16,mem16,imm16
	[Test]
	public void IMUL_reg16_mem16_imm16 ()
	{
		// IMUL AX, [DS:EAX*2], 0xd98
		// IMUL (R16.AX, new WordMemory(Seg.DS, null, R32.EAX, 1), 0xd98)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R16.AX, new WordMemory (Seg.DS, null, R32.EAX, 1), 0xd98);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x66, 0x69, 0x4, 0x0, 0x98, 0xd };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL AX, [DS:EAX*2], 0xd98' failed.");
	}

	// IMUL reg32,mem32,imm8
	[Test]
	public void IMUL_reg32_mem32_imm8 ()
	{
		// IMUL ECX, [EDI*4], 0x1
		// IMUL (R32.ECX, new DWordMemory(null, null, R32.EDI, 2), 0x1)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R32.ECX, new DWordMemory (null, null, R32.EDI, 2), 0x1);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x6b, 0xc, 0xbd, 0x0, 0x0, 0x0, 0x0, 0x1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL ECX, [EDI*4], 0x1' failed.");
	}

	// IMUL reg32,mem32,imm32
	[Test]
	public void IMUL_reg32_mem32_imm32 ()
	{
		// IMUL ESP, [ESI], 0x53495ce
		// IMUL (R32.ESP, new DWordMemory(null, R32.ESI, null, 0), 0x53495ce)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R32.ESP, new DWordMemory (null, R32.ESI, null, 0), 0x53495ce);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x69, 0x26, 0xce, 0x95, 0x34, 0x5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL ESP, [ESI], 0x53495ce' failed.");
	}

	// IMUL rmreg8
	[Test]
	public void IMUL_rmreg8 ()
	{
		// IMUL AL
		// IMUL (R8.AL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R8.AL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf6, 0xe8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL AL' failed.");
	}

	// IMUL rmreg16
	[Test]
	public void IMUL_rmreg16 ()
	{
		// IMUL DI
		// IMUL (R16.DI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R16.DI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf7, 0xef };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL DI' failed.");
	}

	// IMUL rmreg32
	[Test]
	public void IMUL_rmreg32 ()
	{
		// IMUL EBX
		// IMUL (R32.EBX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R32.EBX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf7, 0xeb };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL EBX' failed.");
	}

	// IMUL reg16,rmreg16
	[Test]
	public void IMUL_reg16_rmreg16 ()
	{
		// IMUL BX, DX
		// IMUL (R16.BX, R16.DX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R16.BX, R16.DX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xaf, 0xda };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL BX, DX' failed.");
	}

	// IMUL reg32,rmreg32
	[Test]
	public void IMUL_reg32_rmreg32 ()
	{
		// IMUL ESI, EDX
		// IMUL (R32.ESI, R32.EDX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R32.ESI, R32.EDX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xaf, 0xf2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL ESI, EDX' failed.");
	}

	// IMUL reg16,rmreg16,imm8
	[Test]
	public void IMUL_reg16_rmreg16_imm8 ()
	{
		// IMUL BX, SI, 0x8
		// IMUL (R16.BX, R16.SI, 0x8)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R16.BX, R16.SI, 0x8);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x6b, 0xde, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL BX, SI, 0x8' failed.");
	}

	// IMUL reg16,rmreg16,imm16
	[Test]
	public void IMUL_reg16_rmreg16_imm16 ()
	{
		// IMUL CX, SI, 0x690
		// IMUL (R16.CX, R16.SI, 0x690)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R16.CX, R16.SI, 0x690);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x69, 0xce, 0x90, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL CX, SI, 0x690' failed.");
	}

	// IMUL reg32,rmreg32,imm8
	[Test]
	public void IMUL_reg32_rmreg32_imm8 ()
	{
		// IMUL ESP, ECX, 0x7
		// IMUL (R32.ESP, R32.ECX, 0x7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R32.ESP, R32.ECX, 0x7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x6b, 0xe1, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL ESP, ECX, 0x7' failed.");
	}

	// IMUL reg32,rmreg32,imm32
	[Test]
	public void IMUL_reg32_rmreg32_imm32 ()
	{
		// IMUL ECX, EDX, 0xc09d7fb
		// IMUL (R32.ECX, R32.EDX, 0xc09d7fb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IMUL (R32.ECX, R32.EDX, 0xc09d7fb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x69, 0xca, 0xfb, 0xd7, 0x9, 0xc };
		Assert.IsTrue (CompareData (memoryStream, target), "'IMUL ECX, EDX, 0xc09d7fb' failed.");
	}

	// IN AL,imm8
	[Test]
	public void IN_AL_imm8 ()
	{
		// IN AL, 0x6
		// IN_AL (0x6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IN_AL (0x6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xe4, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IN AL, 0x6' failed.");
	}

	// IN AX,imm8
	[Test]
	public void IN_AX_imm8 ()
	{
		// IN AX, 0x1
		// IN_AX (0x1)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IN_AX (0x1);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xe5, 0x1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IN AX, 0x1' failed.");
	}

	// IN EAX,imm8
	[Test]
	public void IN_EAX_imm8 ()
	{
		// IN EAX, 0x0
		// IN_EAX (0x0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IN_EAX (0x0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xe5, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'IN EAX, 0x0' failed.");
	}

	// IN AL,DX
	[Test]
	public void IN_AL_DX ()
	{
		// IN AL, DX
		// IN_AL__DX ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IN_AL__DX ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xec };
		Assert.IsTrue (CompareData (memoryStream, target), "'IN AL, DX' failed.");
	}

	// IN AX,DX
	[Test]
	public void IN_AX_DX ()
	{
		// IN AX, DX
		// IN_AX__DX ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IN_AX__DX ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xed };
		Assert.IsTrue (CompareData (memoryStream, target), "'IN AX, DX' failed.");
	}

	// IN EAX,DX
	[Test]
	public void IN_EAX_DX ()
	{
		// IN EAX, DX
		// IN_EAX__DX ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IN_EAX__DX ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xed };
		Assert.IsTrue (CompareData (memoryStream, target), "'IN EAX, DX' failed.");
	}

	// INC reg16
	[Test]
	public void INC_reg16 ()
	{
		// INC AX
		// INC (R16.AX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.INC (R16.AX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x40 };
		Assert.IsTrue (CompareData (memoryStream, target), "'INC AX' failed.");
	}

	// INC reg32
	[Test]
	public void INC_reg32 ()
	{
		// INC EBX
		// INC (R32.EBX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.INC (R32.EBX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x43 };
		Assert.IsTrue (CompareData (memoryStream, target), "'INC EBX' failed.");
	}

	// INC mem8
	[Test]
	public void INC_mem8 ()
	{
		// INC Byte [0x12345678]
		// INC (new ByteMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.INC (new ByteMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xfe, 0x5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'INC Byte [0x12345678]' failed.");
	}

	// INC mem16
	[Test]
	public void INC_mem16 ()
	{
		// INC Word [SS:EDI]
		// INC (new WordMemory(Seg.SS, R32.EDI, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.INC (new WordMemory (Seg.SS, R32.EDI, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0x66, 0xff, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'INC Word [SS:EDI]' failed.");
	}

	// INC mem32
	[Test]
	public void INC_mem32 ()
	{
		// INC DWord [ES:EAX*2 + 0x12345678]
		// INC (new DWordMemory(Seg.ES, null, R32.EAX, 1, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.INC (new DWordMemory (Seg.ES, null, R32.EAX, 1, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xff, 0x84, 0x0, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'INC DWord [ES:EAX*2 + 0x12345678]' failed.");
	}

	// INC rmreg8
	[Test]
	public void INC_rmreg8 ()
	{
		// INC BL
		// INC (R8.BL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.INC (R8.BL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xfe, 0xc3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'INC BL' failed.");
	}

	// INSB 
	[Test]
	public void INSB ()
	{
		// INSB
		// INSB ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.INSB ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x6c };
		Assert.IsTrue (CompareData (memoryStream, target), "'INSB' failed.");
	}

	// INSD 
	[Test]
	public void INSD ()
	{
		// INSD
		// INSD ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.INSD ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x6d };
		Assert.IsTrue (CompareData (memoryStream, target), "'INSD' failed.");
	}

	// INSW 
	[Test]
	public void INSW ()
	{
		// INSW
		// INSW ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.INSW ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x6d };
		Assert.IsTrue (CompareData (memoryStream, target), "'INSW' failed.");
	}

	// INT imm8
	[Test]
	public void INT_imm8 ()
	{
		// INT 0xc
		// INT (0xc)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.INT (0xc);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xcd, 0xc };
		Assert.IsTrue (CompareData (memoryStream, target), "'INT 0xc' failed.");
	}

	// INTO 
	[Test]
	public void INTO ()
	{
		// INTO
		// INTO ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.INTO ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xce };
		Assert.IsTrue (CompareData (memoryStream, target), "'INTO' failed.");
	}

	// INVD 
	[Test]
	public void INVD ()
	{
		// INVD
		// INVD ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.INVD ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'INVD' failed.");
	}

	// INVLPG mem
	[Test]
	public void INVLPG_mem ()
	{
		// INVLPG [FS:EAX*4 + 0x12345678]
		// INVLPG (new DWordMemory(Seg.FS, null, R32.EAX, 2, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.INVLPG (new DWordMemory (Seg.FS, null, R32.EAX, 2, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xf, 0x1, 0x3c, 0x85, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'INVLPG [FS:EAX*4 + 0x12345678]' failed.");
	}

	// IRET 
	[Test]
	public void IRET ()
	{
		// IRET
		// IRET ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IRET ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xcf };
		Assert.IsTrue (CompareData (memoryStream, target), "'IRET' failed.");
	}

	// IRETD 
	[Test]
	public void IRETD ()
	{
		// IRETD
		// IRETD ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IRETD ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xcf };
		Assert.IsTrue (CompareData (memoryStream, target), "'IRETD' failed.");
	}

	// IRETW 
	[Test]
	public void IRETW ()
	{
		// IRETW
		// IRETW ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.IRETW ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xcf };
		Assert.IsTrue (CompareData (memoryStream, target), "'IRETW' failed.");
	}

	// JA imm8
	[Test]
	public void JA_imm8 ()
	{
		// JA_imm8: JA SHORT JA_imm8
		// JA (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JA (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x77, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JA_imm8: JA SHORT JA_imm8' failed.");
	}

	// JA NEAR imm
	[Test]
	public void JA_NEAR_imm ()
	{
		// JA 0xba87a5a
		// JA (0xba87a5a)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JA (0xba87a5a);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x87, 0x54, 0x7a, 0xa8, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'JA 0xba87a5a' failed.");
	}

	// JAE imm8
	[Test]
	public void JAE_imm8 ()
	{
		// JAE_imm8: JAE SHORT JAE_imm8
		// JAE (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JAE (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x73, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JAE_imm8: JAE SHORT JAE_imm8' failed.");
	}

	// JAE NEAR imm
	[Test]
	public void JAE_NEAR_imm ()
	{
		// JAE 0x9f38666
		// JAE (0x9f38666)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JAE (0x9f38666);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x83, 0x60, 0x86, 0xf3, 0x9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JAE 0x9f38666' failed.");
	}

	// JB imm8
	[Test]
	public void JB_imm8 ()
	{
		// JB_imm8: JB SHORT JB_imm8
		// JB (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JB (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x72, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JB_imm8: JB SHORT JB_imm8' failed.");
	}

	// JB NEAR imm
	[Test]
	public void JB_NEAR_imm ()
	{
		// JB 0x33b8cd5
		// JB (0x33b8cd5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JB (0x33b8cd5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x82, 0xcf, 0x8c, 0x3b, 0x3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JB 0x33b8cd5' failed.");
	}

	// JBE imm8
	[Test]
	public void JBE_imm8 ()
	{
		// JBE_imm8: JBE SHORT JBE_imm8
		// JBE (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JBE (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x76, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JBE_imm8: JBE SHORT JBE_imm8' failed.");
	}

	// JBE NEAR imm
	[Test]
	public void JBE_NEAR_imm ()
	{
		// JBE 0xccd34a0
		// JBE (0xccd34a0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JBE (0xccd34a0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x86, 0x9a, 0x34, 0xcd, 0xc };
		Assert.IsTrue (CompareData (memoryStream, target), "'JBE 0xccd34a0' failed.");
	}

	// JC imm8
	[Test]
	public void JC_imm8 ()
	{
		// JC_imm8: JC SHORT JC_imm8
		// JC (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JC (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x72, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JC_imm8: JC SHORT JC_imm8' failed.");
	}

	// JC NEAR imm
	[Test]
	public void JC_NEAR_imm ()
	{
		// JC 0xbecba02
		// JC (0xbecba02)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JC (0xbecba02);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x82, 0xfc, 0xb9, 0xec, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'JC 0xbecba02' failed.");
	}

	// JCXZ imm8
	[Test]
	public void JCXZ_imm8 ()
	{
		// JCXZ_imm8: JCXZ JCXZ_imm8
		// JCXZ (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JCXZ (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x67, 0xe3, 0xfd };
		Assert.IsTrue (CompareData (memoryStream, target), "'JCXZ_imm8: JCXZ JCXZ_imm8' failed.");
	}

	// JE imm8
	[Test]
	public void JE_imm8 ()
	{
		// JE_imm8: JE SHORT JE_imm8
		// JE (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JE (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x74, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JE_imm8: JE SHORT JE_imm8' failed.");
	}

	// JE NEAR imm
	[Test]
	public void JE_NEAR_imm ()
	{
		// JE 0xa1f593f
		// JE (0xa1f593f)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JE (0xa1f593f);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x84, 0x39, 0x59, 0x1f, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'JE 0xa1f593f' failed.");
	}

	// JECXZ imm8
	[Test]
	public void JECXZ_imm8 ()
	{
		// JECXZ_imm8: JECXZ JECXZ_imm8
		// JECXZ (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JECXZ (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xe3, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JECXZ_imm8: JECXZ JECXZ_imm8' failed.");
	}

	// JG imm8
	[Test]
	public void JG_imm8 ()
	{
		// JG_imm8: JG SHORT JG_imm8
		// JG (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JG (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x7f, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JG_imm8: JG SHORT JG_imm8' failed.");
	}

	// JG NEAR imm
	[Test]
	public void JG_NEAR_imm ()
	{
		// JG 0x9539af3
		// JG (0x9539af3)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JG (0x9539af3);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x8f, 0xed, 0x9a, 0x53, 0x9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JG 0x9539af3' failed.");
	}

	// JGE imm8
	[Test]
	public void JGE_imm8 ()
	{
		// JGE_imm8: JGE SHORT JGE_imm8
		// JGE (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JGE (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x7d, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JGE_imm8: JGE SHORT JGE_imm8' failed.");
	}

	// JGE NEAR imm
	[Test]
	public void JGE_NEAR_imm ()
	{
		// JGE 0x8fea856
		// JGE (0x8fea856)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JGE (0x8fea856);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x8d, 0x50, 0xa8, 0xfe, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JGE 0x8fea856' failed.");
	}

	// JL imm8
	[Test]
	public void JL_imm8 ()
	{
		// JL_imm8: JL SHORT JL_imm8
		// JL (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JL (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x7c, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JL_imm8: JL SHORT JL_imm8' failed.");
	}

	// JL NEAR imm
	[Test]
	public void JL_NEAR_imm ()
	{
		// JL 0x4e423ce
		// JL (0x4e423ce)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JL (0x4e423ce);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x8c, 0xc8, 0x23, 0xe4, 0x4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JL 0x4e423ce' failed.");
	}

	// JLE imm8
	[Test]
	public void JLE_imm8 ()
	{
		// JLE_imm8: JLE SHORT JLE_imm8
		// JLE (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JLE (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x7e, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JLE_imm8: JLE SHORT JLE_imm8' failed.");
	}

	// JLE NEAR imm
	[Test]
	public void JLE_NEAR_imm ()
	{
		// JLE 0xcc0fe17
		// JLE (0xcc0fe17)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JLE (0xcc0fe17);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x8e, 0x11, 0xfe, 0xc0, 0xc };
		Assert.IsTrue (CompareData (memoryStream, target), "'JLE 0xcc0fe17' failed.");
	}

	// JMP imm
	[Test]
	public void JMP_imm ()
	{
		// JMP 0x50b4d29
		// JMP (0x50b4d29)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JMP (0x50b4d29);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xe9, 0x24, 0x4d, 0xb, 0x5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JMP 0x50b4d29' failed.");
	}

	// JMP imm8
	[Test]
	public void JMP_imm8 ()
	{
		// JMP_imm8: JMP SHORT JMP_imm8
		// JMP (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JMP (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xeb, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JMP_imm8: JMP SHORT JMP_imm8' failed.");
	}

	// JMP imm16:imm16
	[Test]
	public void JMP_imm16_imm16 ()
	{
		// JMP WORD 0x319: 0xceb
		// JMP (0x319, 0xceb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JMP (0x319, 0xceb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xea, 0xeb, 0xc, 0x19, 0x3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JMP WORD 0x319: 0xceb' failed.");
	}

	// JMP imm16:imm32
	[Test]
	public void JMP_imm16_imm32 ()
	{
		// JMP 0xaed: 0xb2bdff6
		// JMP (0xaed, 0xb2bdff6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JMP (0xaed, 0xb2bdff6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xea, 0xf6, 0xdf, 0x2b, 0xb, 0xed, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'JMP 0xaed: 0xb2bdff6' failed.");
	}

	// JMP FAR mem
	[Test]
	public void JMP_FAR_mem ()
	{
		// JMP FAR [EBX]
		// JMP_FAR (new DWordMemory(null, R32.EBX, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JMP_FAR (new DWordMemory (null, R32.EBX, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xff, 0x2b };
		Assert.IsTrue (CompareData (memoryStream, target), "'JMP FAR [EBX]' failed.");
	}

	// JMP FAR mem32
	[Test]
	public void JMP_FAR_mem32 ()
	{
		// JMP FAR DWord [ES:0x12345678]
		// JMP_FAR (new DWordMemory(Seg.ES, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JMP_FAR (new DWordMemory (Seg.ES, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xff, 0x2d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JMP FAR DWord [ES:0x12345678]' failed.");
	}

	// JMP mem16
	[Test]
	public void JMP_mem16 ()
	{
		// JMP Word [EAX + EDX*1]
		// JMP (new WordMemory(null, R32.EAX, R32.EDX, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JMP (new WordMemory (null, R32.EAX, R32.EDX, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xff, 0x24, 0x10 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JMP Word [EAX + EDX*1]' failed.");
	}

	// JMP mem32
	[Test]
	public void JMP_mem32 ()
	{
		// JMP DWord [FS:ECX*2]
		// JMP (new DWordMemory(Seg.FS, null, R32.ECX, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JMP (new DWordMemory (Seg.FS, null, R32.ECX, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xff, 0x24, 0x9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JMP DWord [FS:ECX*2]' failed.");
	}

	// JMP rmreg16
	[Test]
	public void JMP_rmreg16 ()
	{
		// JMP SI
		// JMP (R16.SI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JMP (R16.SI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xff, 0xe6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JMP SI' failed.");
	}

	// JMP rmreg32
	[Test]
	public void JMP_rmreg32 ()
	{
		// JMP ESP
		// JMP (R32.ESP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JMP (R32.ESP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xff, 0xe4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JMP ESP' failed.");
	}

	// JNA imm8
	[Test]
	public void JNA_imm8 ()
	{
		// JNA_imm8: JNA SHORT JNA_imm8
		// JNA (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNA (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x76, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNA_imm8: JNA SHORT JNA_imm8' failed.");
	}

	// JNA NEAR imm
	[Test]
	public void JNA_NEAR_imm ()
	{
		// JNA 0x537e20a
		// JNA (0x537e20a)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNA (0x537e20a);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x86, 0x4, 0xe2, 0x37, 0x5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNA 0x537e20a' failed.");
	}

	// JNAE imm8
	[Test]
	public void JNAE_imm8 ()
	{
		// JNAE_imm8: JNAE SHORT JNAE_imm8
		// JNAE (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNAE (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x72, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNAE_imm8: JNAE SHORT JNAE_imm8' failed.");
	}

	// JNAE NEAR imm
	[Test]
	public void JNAE_NEAR_imm ()
	{
		// JNAE 0xb8c3021
		// JNAE (0xb8c3021)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNAE (0xb8c3021);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x82, 0x1b, 0x30, 0x8c, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNAE 0xb8c3021' failed.");
	}

	// JNB imm8
	[Test]
	public void JNB_imm8 ()
	{
		// JNB_imm8: JNB SHORT JNB_imm8
		// JNB (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNB (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x73, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNB_imm8: JNB SHORT JNB_imm8' failed.");
	}

	// JNB NEAR imm
	[Test]
	public void JNB_NEAR_imm ()
	{
		// JNB 0xdcb7338
		// JNB (0xdcb7338)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNB (0xdcb7338);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x83, 0x32, 0x73, 0xcb, 0xd };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNB 0xdcb7338' failed.");
	}

	// JNBE imm8
	[Test]
	public void JNBE_imm8 ()
	{
		// JNBE_imm8: JNBE SHORT JNBE_imm8
		// JNBE (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNBE (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x77, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNBE_imm8: JNBE SHORT JNBE_imm8' failed.");
	}

	// JNBE NEAR imm
	[Test]
	public void JNBE_NEAR_imm ()
	{
		// JNBE 0x97bd1a2
		// JNBE (0x97bd1a2)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNBE (0x97bd1a2);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x87, 0x9c, 0xd1, 0x7b, 0x9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNBE 0x97bd1a2' failed.");
	}

	// JNC imm8
	[Test]
	public void JNC_imm8 ()
	{
		// JNC_imm8: JNC SHORT JNC_imm8
		// JNC (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNC (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x73, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNC_imm8: JNC SHORT JNC_imm8' failed.");
	}

	// JNC NEAR imm
	[Test]
	public void JNC_NEAR_imm ()
	{
		// JNC 0x36f63c8
		// JNC (0x36f63c8)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNC (0x36f63c8);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x83, 0xc2, 0x63, 0x6f, 0x3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNC 0x36f63c8' failed.");
	}

	// JNE imm8
	[Test]
	public void JNE_imm8 ()
	{
		// JNE_imm8: JNE SHORT JNE_imm8
		// JNE (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNE (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x75, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNE_imm8: JNE SHORT JNE_imm8' failed.");
	}

	// JNE NEAR imm
	[Test]
	public void JNE_NEAR_imm ()
	{
		// JNE 0xbcc2f28
		// JNE (0xbcc2f28)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNE (0xbcc2f28);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x85, 0x22, 0x2f, 0xcc, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNE 0xbcc2f28' failed.");
	}

	// JNG imm8
	[Test]
	public void JNG_imm8 ()
	{
		// JNG_imm8: JNG SHORT JNG_imm8
		// JNG (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNG (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x7e, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNG_imm8: JNG SHORT JNG_imm8' failed.");
	}

	// JNG NEAR imm
	[Test]
	public void JNG_NEAR_imm ()
	{
		// JNG 0xef38105
		// JNG (0xef38105)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNG (0xef38105);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x8e, 0xff, 0x80, 0xf3, 0xe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNG 0xef38105' failed.");
	}

	// JNGE imm8
	[Test]
	public void JNGE_imm8 ()
	{
		// JNGE_imm8: JNGE SHORT JNGE_imm8
		// JNGE (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNGE (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x7c, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNGE_imm8: JNGE SHORT JNGE_imm8' failed.");
	}

	// JNGE NEAR imm
	[Test]
	public void JNGE_NEAR_imm ()
	{
		// JNGE 0xca86f70
		// JNGE (0xca86f70)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNGE (0xca86f70);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x8c, 0x6a, 0x6f, 0xa8, 0xc };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNGE 0xca86f70' failed.");
	}

	// JNL imm8
	[Test]
	public void JNL_imm8 ()
	{
		// JNL_imm8: JNL SHORT JNL_imm8
		// JNL (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNL (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x7d, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNL_imm8: JNL SHORT JNL_imm8' failed.");
	}

	// JNL NEAR imm
	[Test]
	public void JNL_NEAR_imm ()
	{
		// JNL 0x62a9dec
		// JNL (0x62a9dec)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNL (0x62a9dec);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x8d, 0xe6, 0x9d, 0x2a, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNL 0x62a9dec' failed.");
	}

	// JNLE imm8
	[Test]
	public void JNLE_imm8 ()
	{
		// JNLE_imm8: JNLE SHORT JNLE_imm8
		// JNLE (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNLE (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x7f, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNLE_imm8: JNLE SHORT JNLE_imm8' failed.");
	}

	// JNLE NEAR imm
	[Test]
	public void JNLE_NEAR_imm ()
	{
		// JNLE 0xdce43a8
		// JNLE (0xdce43a8)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNLE (0xdce43a8);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x8f, 0xa2, 0x43, 0xce, 0xd };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNLE 0xdce43a8' failed.");
	}

	// JNO imm8
	[Test]
	public void JNO_imm8 ()
	{
		// JNO_imm8: JNO SHORT JNO_imm8
		// JNO (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNO (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x71, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNO_imm8: JNO SHORT JNO_imm8' failed.");
	}

	// JNO NEAR imm
	[Test]
	public void JNO_NEAR_imm ()
	{
		// JNO 0x2853a23
		// JNO (0x2853a23)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNO (0x2853a23);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x81, 0x1d, 0x3a, 0x85, 0x2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNO 0x2853a23' failed.");
	}

	// JNP imm8
	[Test]
	public void JNP_imm8 ()
	{
		// JNP_imm8: JNP SHORT JNP_imm8
		// JNP (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNP (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x7b, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNP_imm8: JNP SHORT JNP_imm8' failed.");
	}

	// JNP NEAR imm
	[Test]
	public void JNP_NEAR_imm ()
	{
		// JNP 0xf92c2c0
		// JNP (0xf92c2c0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNP (0xf92c2c0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x8b, 0xba, 0xc2, 0x92, 0xf };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNP 0xf92c2c0' failed.");
	}

	// JNS imm8
	[Test]
	public void JNS_imm8 ()
	{
		// JNS_imm8: JNS SHORT JNS_imm8
		// JNS (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNS (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x79, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNS_imm8: JNS SHORT JNS_imm8' failed.");
	}

	// JNS NEAR imm
	[Test]
	public void JNS_NEAR_imm ()
	{
		// JNS 0x8e3e2f9
		// JNS (0x8e3e2f9)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNS (0x8e3e2f9);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x89, 0xf3, 0xe2, 0xe3, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNS 0x8e3e2f9' failed.");
	}

	// JNZ imm8
	[Test]
	public void JNZ_imm8 ()
	{
		// JNZ_imm8: JNZ SHORT JNZ_imm8
		// JNZ (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNZ (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x75, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNZ_imm8: JNZ SHORT JNZ_imm8' failed.");
	}

	// JNZ NEAR imm
	[Test]
	public void JNZ_NEAR_imm ()
	{
		// JNZ 0x8217208
		// JNZ (0x8217208)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JNZ (0x8217208);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x85, 0x2, 0x72, 0x21, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JNZ 0x8217208' failed.");
	}

	// JO imm8
	[Test]
	public void JO_imm8 ()
	{
		// JO_imm8: JO SHORT JO_imm8
		// JO (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JO (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x70, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JO_imm8: JO SHORT JO_imm8' failed.");
	}

	// JO NEAR imm
	[Test]
	public void JO_NEAR_imm ()
	{
		// JO 0xa101973
		// JO (0xa101973)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JO (0xa101973);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x80, 0x6d, 0x19, 0x10, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'JO 0xa101973' failed.");
	}

	// JP imm8
	[Test]
	public void JP_imm8 ()
	{
		// JP_imm8: JP SHORT JP_imm8
		// JP (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JP (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x7a, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JP_imm8: JP SHORT JP_imm8' failed.");
	}

	// JP NEAR imm
	[Test]
	public void JP_NEAR_imm ()
	{
		// JP 0x4cf60f3
		// JP (0x4cf60f3)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JP (0x4cf60f3);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x8a, 0xed, 0x60, 0xcf, 0x4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JP 0x4cf60f3' failed.");
	}

	// JPE imm8
	[Test]
	public void JPE_imm8 ()
	{
		// JPE_imm8: JPE SHORT JPE_imm8
		// JPE (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JPE (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x7a, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JPE_imm8: JPE SHORT JPE_imm8' failed.");
	}

	// JPE NEAR imm
	[Test]
	public void JPE_NEAR_imm ()
	{
		// JPE 0x6c2a4b7
		// JPE (0x6c2a4b7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JPE (0x6c2a4b7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x8a, 0xb1, 0xa4, 0xc2, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'JPE 0x6c2a4b7' failed.");
	}

	// JPO imm8
	[Test]
	public void JPO_imm8 ()
	{
		// JPO_imm8: JPO SHORT JPO_imm8
		// JPO (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JPO (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x7b, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JPO_imm8: JPO SHORT JPO_imm8' failed.");
	}

	// JPO NEAR imm
	[Test]
	public void JPO_NEAR_imm ()
	{
		// JPO 0xda4771c
		// JPO (0xda4771c)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JPO (0xda4771c);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x8b, 0x16, 0x77, 0xa4, 0xd };
		Assert.IsTrue (CompareData (memoryStream, target), "'JPO 0xda4771c' failed.");
	}

	// JS imm8
	[Test]
	public void JS_imm8 ()
	{
		// JS_imm8: JS SHORT JS_imm8
		// JS (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JS (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x78, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JS_imm8: JS SHORT JS_imm8' failed.");
	}

	// JS NEAR imm
	[Test]
	public void JS_NEAR_imm ()
	{
		// JS 0xdba8cae
		// JS (0xdba8cae)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JS (0xdba8cae);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x88, 0xa8, 0x8c, 0xba, 0xd };
		Assert.IsTrue (CompareData (memoryStream, target), "'JS 0xdba8cae' failed.");
	}

	// JZ imm8
	[Test]
	public void JZ_imm8 ()
	{
		// JZ_imm8: JZ SHORT JZ_imm8
		// JZ (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JZ (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x74, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'JZ_imm8: JZ SHORT JZ_imm8' failed.");
	}

	// JZ NEAR imm
	[Test]
	public void JZ_NEAR_imm ()
	{
		// JZ 0xd319228
		// JZ (0xd319228)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.JZ (0xd319228);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x84, 0x22, 0x92, 0x31, 0xd };
		Assert.IsTrue (CompareData (memoryStream, target), "'JZ 0xd319228' failed.");
	}

	// LAHF 
	[Test]
	public void LAHF ()
	{
		// LAHF
		// LAHF ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LAHF ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x9f };
		Assert.IsTrue (CompareData (memoryStream, target), "'LAHF' failed.");
	}

	// LAR reg16,mem16
	[Test]
	public void LAR_reg16_mem16 ()
	{
		// LAR BP, [SS:EBP + 0x12345678]
		// LAR (R16.BP, new WordMemory(Seg.SS, R32.EBP, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LAR (R16.BP, new WordMemory (Seg.SS, R32.EBP, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0x66, 0xf, 0x2, 0xad, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LAR BP, [SS:EBP + 0x12345678]' failed.");
	}

	// LAR reg32,mem32
	[Test]
	public void LAR_reg32_mem32 ()
	{
		// LAR EBP, [0x12345678]
		// LAR (R32.EBP, new DWordMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LAR (R32.EBP, new DWordMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x2, 0x2d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LAR EBP, [0x12345678]' failed.");
	}

	// LAR reg16,rmreg16
	[Test]
	public void LAR_reg16_rmreg16 ()
	{
		// LAR DX, BX
		// LAR (R16.DX, R16.BX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LAR (R16.DX, R16.BX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x2, 0xd3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LAR DX, BX' failed.");
	}

	// LAR reg32,rmreg32
	[Test]
	public void LAR_reg32_rmreg32 ()
	{
		// LAR EBX, ESP
		// LAR (R32.EBX, R32.ESP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LAR (R32.EBX, R32.ESP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x2, 0xdc };
		Assert.IsTrue (CompareData (memoryStream, target), "'LAR EBX, ESP' failed.");
	}

	// LDS reg16,mem
	[Test]
	public void LDS_reg16_mem ()
	{
		// LDS BP, [ECX + 0x12345678]
		// LDS (R16.BP, new DWordMemory(null, R32.ECX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LDS (R16.BP, new DWordMemory (null, R32.ECX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xc5, 0xa9, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LDS BP, [ECX + 0x12345678]' failed.");
	}

	// LDS reg32,mem
	[Test]
	public void LDS_reg32_mem ()
	{
		// LDS EDI, [CS:EBP]
		// LDS (R32.EDI, new DWordMemory(Seg.CS, R32.EBP, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LDS (R32.EDI, new DWordMemory (Seg.CS, R32.EBP, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xc5, 0x7d, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LDS EDI, [CS:EBP]' failed.");
	}

	// LEA reg16,mem
	[Test]
	public void LEA_reg16_mem ()
	{
		// LEA DX, [CS:EBX + 0x12345678]
		// LEA (R16.DX, new DWordMemory(Seg.CS, R32.EBX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LEA (R16.DX, new DWordMemory (Seg.CS, R32.EBX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0x66, 0x8d, 0x93, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LEA DX, [CS:EBX + 0x12345678]' failed.");
	}

	// LEA reg32,mem
	[Test]
	public void LEA_reg32_mem ()
	{
		// LEA EDI, [FS:ESP + EDX*2]
		// LEA (R32.EDI, new DWordMemory(Seg.FS, R32.ESP, R32.EDX, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LEA (R32.EDI, new DWordMemory (Seg.FS, R32.ESP, R32.EDX, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0x8d, 0x3c, 0x54 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LEA EDI, [FS:ESP + EDX*2]' failed.");
	}

	// LEAVE 
	[Test]
	public void LEAVE ()
	{
		// LEAVE
		// LEAVE ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LEAVE ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LEAVE' failed.");
	}

	// LES reg16,mem
	[Test]
	public void LES_reg16_mem ()
	{
		// LES SP, [ES:EDX + 0x12345678]
		// LES (R16.SP, new DWordMemory(Seg.ES, R32.EDX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LES (R16.SP, new DWordMemory (Seg.ES, R32.EDX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0x66, 0xc4, 0xa2, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LES SP, [ES:EDX + 0x12345678]' failed.");
	}

	// LES reg32,mem
	[Test]
	public void LES_reg32_mem ()
	{
		// LES EAX, [FS:EBP]
		// LES (R32.EAX, new DWordMemory(Seg.FS, R32.EBP, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LES (R32.EAX, new DWordMemory (Seg.FS, R32.EBP, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xc4, 0x45, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LES EAX, [FS:EBP]' failed.");
	}

	// LFENCE 
	[Test]
	public void LFENCE ()
	{
		// LFENCE
		// LFENCE ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LFENCE ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xae, 0xe8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LFENCE' failed.");
	}

	// LFS reg16,mem
	[Test]
	public void LFS_reg16_mem ()
	{
		// LFS DI, [0x12345678]
		// LFS (R16.DI, new DWordMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LFS (R16.DI, new DWordMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xb4, 0x3d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LFS DI, [0x12345678]' failed.");
	}

	// LFS reg32,mem
	[Test]
	public void LFS_reg32_mem ()
	{
		// LFS ECX, [GS:EBX*1 + 0x12345678]
		// LFS (R32.ECX, new DWordMemory(Seg.GS, null, R32.EBX, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LFS (R32.ECX, new DWordMemory (Seg.GS, null, R32.EBX, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xf, 0xb4, 0x8b, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LFS ECX, [GS:EBX*1 + 0x12345678]' failed.");
	}

	// LGDT mem
	[Test]
	public void LGDT_mem ()
	{
		// LGDT [ECX + EBP*2]
		// LGDT (new DWordMemory(null, R32.ECX, R32.EBP, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LGDT (new DWordMemory (null, R32.ECX, R32.EBP, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x1, 0x14, 0x69 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LGDT [ECX + EBP*2]' failed.");
	}

	// LGS reg16,mem
	[Test]
	public void LGS_reg16_mem ()
	{
		// LGS DI, [FS:0x12345678]
		// LGS (R16.DI, new DWordMemory(Seg.FS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LGS (R16.DI, new DWordMemory (Seg.FS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0x66, 0xf, 0xb5, 0x3d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LGS DI, [FS:0x12345678]' failed.");
	}

	// LGS reg32,mem
	[Test]
	public void LGS_reg32_mem ()
	{
		// LGS EBX, [EBX + 0x12345678]
		// LGS (R32.EBX, new DWordMemory(null, R32.EBX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LGS (R32.EBX, new DWordMemory (null, R32.EBX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xb5, 0x9b, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LGS EBX, [EBX + 0x12345678]' failed.");
	}

	// LIDT mem
	[Test]
	public void LIDT_mem ()
	{
		// LIDT [FS:ESI + 0x12345678]
		// LIDT (new DWordMemory(Seg.FS, R32.ESI, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LIDT (new DWordMemory (Seg.FS, R32.ESI, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xf, 0x1, 0x9e, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LIDT [FS:ESI + 0x12345678]' failed.");
	}

	// LLDT mem16
	[Test]
	public void LLDT_mem16 ()
	{
		// LLDT Word [CS:EBX + EBX*2 + 0x12345678]
		// LLDT (new WordMemory(Seg.CS, R32.EBX, R32.EBX, 1, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LLDT (new WordMemory (Seg.CS, R32.EBX, R32.EBX, 1, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xf, 0x0, 0x94, 0x5b, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LLDT Word [CS:EBX + EBX*2 + 0x12345678]' failed.");
	}

	// LLDT rmreg16
	[Test]
	public void LLDT_rmreg16 ()
	{
		// LLDT SP
		// LLDT (R16.SP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LLDT (R16.SP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x0, 0xd4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LLDT SP' failed.");
	}

	// LMSW mem16
	[Test]
	public void LMSW_mem16 ()
	{
		// LMSW Word [DS:EDI + 0x12345678]
		// LMSW (new WordMemory(Seg.DS, R32.EDI, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LMSW (new WordMemory (Seg.DS, R32.EDI, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xf, 0x1, 0xb7, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LMSW Word [DS:EDI + 0x12345678]' failed.");
	}

	// LMSW rmreg16
	[Test]
	public void LMSW_rmreg16 ()
	{
		// LMSW SP
		// LMSW (R16.SP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LMSW (R16.SP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x1, 0xf4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LMSW SP' failed.");
	}

	// LODSB 
	[Test]
	public void LODSB ()
	{
		// LODSB
		// LODSB ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LODSB ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xac };
		Assert.IsTrue (CompareData (memoryStream, target), "'LODSB' failed.");
	}

	// LODSD 
	[Test]
	public void LODSD ()
	{
		// LODSD
		// LODSD ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LODSD ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xad };
		Assert.IsTrue (CompareData (memoryStream, target), "'LODSD' failed.");
	}

	// LODSW 
	[Test]
	public void LODSW ()
	{
		// LODSW
		// LODSW ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LODSW ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xad };
		Assert.IsTrue (CompareData (memoryStream, target), "'LODSW' failed.");
	}

	// LOOP imm8
	[Test]
	public void LOOP_imm8 ()
	{
		// LOOP_imm8: LOOP LOOP_imm8
		// LOOP (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LOOP (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xe2, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'LOOP_imm8: LOOP LOOP_imm8' failed.");
	}

	// LOOPE imm8
	[Test]
	public void LOOPE_imm8 ()
	{
		// LOOPE_imm8: LOOPE LOOPE_imm8
		// LOOPE (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LOOPE (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xe1, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'LOOPE_imm8: LOOPE LOOPE_imm8' failed.");
	}

	// LOOPNE imm8
	[Test]
	public void LOOPNE_imm8 ()
	{
		// LOOPNE_imm8: LOOPNE LOOPNE_imm8
		// LOOPNE (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LOOPNE (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xe0, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'LOOPNE_imm8: LOOPNE LOOPNE_imm8' failed.");
	}

	// LOOPNZ imm8
	[Test]
	public void LOOPNZ_imm8 ()
	{
		// LOOPNZ_imm8: LOOPNZ LOOPNZ_imm8
		// LOOPNZ (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LOOPNZ (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xe0, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'LOOPNZ_imm8: LOOPNZ LOOPNZ_imm8' failed.");
	}

	// LOOPZ imm8
	[Test]
	public void LOOPZ_imm8 ()
	{
		// LOOPZ_imm8: LOOPZ LOOPZ_imm8
		// LOOPZ (0x00)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LOOPZ (0x00);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xe1, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'LOOPZ_imm8: LOOPZ LOOPZ_imm8' failed.");
	}

	// LSL reg16,mem16
	[Test]
	public void LSL_reg16_mem16 ()
	{
		// LSL SP, [FS:EBX + 0x12345678]
		// LSL (R16.SP, new WordMemory(Seg.FS, R32.EBX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LSL (R16.SP, new WordMemory (Seg.FS, R32.EBX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0x66, 0xf, 0x3, 0xa3, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LSL SP, [FS:EBX + 0x12345678]' failed.");
	}

	// LSL reg32,mem32
	[Test]
	public void LSL_reg32_mem32 ()
	{
		// LSL ESP, [CS:EDI]
		// LSL (R32.ESP, new DWordMemory(Seg.CS, R32.EDI, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LSL (R32.ESP, new DWordMemory (Seg.CS, R32.EDI, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xf, 0x3, 0x27 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LSL ESP, [CS:EDI]' failed.");
	}

	// LSL reg16,rmreg16
	[Test]
	public void LSL_reg16_rmreg16 ()
	{
		// LSL BX, DX
		// LSL (R16.BX, R16.DX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LSL (R16.BX, R16.DX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x3, 0xda };
		Assert.IsTrue (CompareData (memoryStream, target), "'LSL BX, DX' failed.");
	}

	// LSL reg32,rmreg32
	[Test]
	public void LSL_reg32_rmreg32 ()
	{
		// LSL ECX, EBP
		// LSL (R32.ECX, R32.EBP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LSL (R32.ECX, R32.EBP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x3, 0xcd };
		Assert.IsTrue (CompareData (memoryStream, target), "'LSL ECX, EBP' failed.");
	}

	// LSS reg16,mem
	[Test]
	public void LSS_reg16_mem ()
	{
		// LSS BX, [EDI + 0x12345678]
		// LSS (R16.BX, new DWordMemory(null, R32.EDI, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LSS (R16.BX, new DWordMemory (null, R32.EDI, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xb2, 0x9f, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LSS BX, [EDI + 0x12345678]' failed.");
	}

	// LSS reg32,mem
	[Test]
	public void LSS_reg32_mem ()
	{
		// LSS EDX, [ECX + EAX*1 + 0x12345678]
		// LSS (R32.EDX, new DWordMemory(null, R32.ECX, R32.EAX, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LSS (R32.EDX, new DWordMemory (null, R32.ECX, R32.EAX, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xb2, 0x94, 0x1, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LSS EDX, [ECX + EAX*1 + 0x12345678]' failed.");
	}

	// LTR mem16
	[Test]
	public void LTR_mem16 ()
	{
		// LTR Word [EAX + 0x12345678]
		// LTR (new WordMemory(null, R32.EAX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LTR (new WordMemory (null, R32.EAX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x0, 0x98, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'LTR Word [EAX + 0x12345678]' failed.");
	}

	// LTR rmreg16
	[Test]
	public void LTR_rmreg16 ()
	{
		// LTR BX
		// LTR (R16.BX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.LTR (R16.BX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x0, 0xdb };
		Assert.IsTrue (CompareData (memoryStream, target), "'LTR BX' failed.");
	}

	// MFENCE 
	[Test]
	public void MFENCE ()
	{
		// MFENCE
		// MFENCE ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MFENCE ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xae, 0xf0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MFENCE' failed.");
	}

	// MOV mem8,reg8
	[Test]
	public void MOV_mem8_reg8 ()
	{
		// MOV [EDI + 0x12345678], CL
		// MOV (new ByteMemory(null, R32.EDI, null, 0, 0x12345678), R8.CL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (new ByteMemory (null, R32.EDI, null, 0, 0x12345678), R8.CL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x88, 0x8f, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV [EDI + 0x12345678], CL' failed.");
	}

	// MOV mem16,reg16
	[Test]
	public void MOV_mem16_reg16 ()
	{
		// MOV [CS:ESP + EBX*8], AX
		// MOV (new WordMemory(Seg.CS, R32.ESP, R32.EBX, 3), R16.AX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (new WordMemory (Seg.CS, R32.ESP, R32.EBX, 3), R16.AX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0x66, 0x89, 0x4, 0xdc };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV [CS:ESP + EBX*8], AX' failed.");
	}

	// MOV mem32,reg32
	[Test]
	public void MOV_mem32_reg32 ()
	{
		// MOV [EDI*2], ECX
		// MOV (new DWordMemory(null, null, R32.EDI, 1), R32.ECX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (new DWordMemory (null, null, R32.EDI, 1), R32.ECX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x89, 0xc, 0x3f };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV [EDI*2], ECX' failed.");
	}

	// MOV reg8,mem8
	[Test]
	public void MOV_reg8_mem8 ()
	{
		// MOV BH, [EDX*8]
		// MOV (R8.BH, new ByteMemory(null, null, R32.EDX, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (R8.BH, new ByteMemory (null, null, R32.EDX, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x8a, 0x3c, 0xd5, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV BH, [EDX*8]' failed.");
	}

	// MOV reg16,mem16
	[Test]
	public void MOV_reg16_mem16 ()
	{
		// MOV CX, [EBP]
		// MOV (R16.CX, new WordMemory(null, R32.EBP, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (R16.CX, new WordMemory (null, R32.EBP, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x8b, 0x4d, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV CX, [EBP]' failed.");
	}

	// MOV reg32,mem32
	[Test]
	public void MOV_reg32_mem32 ()
	{
		// MOV ESI, [EBP*2]
		// MOV (R32.ESI, new DWordMemory(null, null, R32.EBP, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (R32.ESI, new DWordMemory (null, null, R32.EBP, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x8b, 0x74, 0x2d, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV ESI, [EBP*2]' failed.");
	}

	// MOV reg8,imm8
	[Test]
	public void MOV_reg8_imm8 ()
	{
		// MOV BL, 0x2
		// MOV (R8.BL, 0x2)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (R8.BL, 0x2);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xb3, 0x2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV BL, 0x2' failed.");
	}

	// MOV reg16,imm16
	[Test]
	public void MOV_reg16_imm16 ()
	{
		// MOV BX, 0x35d
		// MOV (R16.BX, 0x35d)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (R16.BX, 0x35d);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xbb, 0x5d, 0x3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV BX, 0x35d' failed.");
	}

	// MOV reg32,imm32
	[Test]
	public void MOV_reg32_imm32 ()
	{
		// MOV EDI, 0x256dd83
		// MOV (R32.EDI, 0x256dd83)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (R32.EDI, 0x256dd83);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xbf, 0x83, 0xdd, 0x56, 0x2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV EDI, 0x256dd83' failed.");
	}

	// MOV mem8,imm8
	[Test]
	public void MOV_mem8_imm8 ()
	{
		// MOV Byte [CS:ESI], 0x7
		// MOV (new ByteMemory(Seg.CS, R32.ESI, null, 0), 0x7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (new ByteMemory (Seg.CS, R32.ESI, null, 0), 0x7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xc6, 0x6, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV Byte [CS:ESI], 0x7' failed.");
	}

	// MOV mem16,imm16
	[Test]
	public void MOV_mem16_imm16 ()
	{
		// MOV Word [ESI], 0xb89
		// MOV (new WordMemory(null, R32.ESI, null, 0), 0xb89)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (new WordMemory (null, R32.ESI, null, 0), 0xb89);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xc7, 0x6, 0x89, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV Word [ESI], 0xb89' failed.");
	}

	// MOV mem32,imm32
	[Test]
	public void MOV_mem32_imm32 ()
	{
		// MOV DWord [EDX*1 + 0x12345678], 0xc06f46b
		// MOV (new DWordMemory(null, null, R32.EDX, 0, 0x12345678), 0xc06f46b)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (new DWordMemory (null, null, R32.EDX, 0, 0x12345678), 0xc06f46b);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc7, 0x82, 0x78, 0x56, 0x34, 0x12, 0x6b, 0xf4, 0x6, 0xc };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV DWord [EDX*1 + 0x12345678], 0xc06f46b' failed.");
	}

	// MOV AL,memoffs8
	[Test]
	public void MOV_AL_memoffs8 ()
	{
		// MOV AL, [0xa]
		// MOV_AL (0xa)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV_AL (0xa);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xa0, 0xa, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV AL, [0xa]' failed.");
	}

	// MOV AX,memoffs16
	[Test]
	public void MOV_AX_memoffs16 ()
	{
		// MOV AX, [0xd02]
		// MOV_AX (0xd02)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV_AX (0xd02);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xa1, 0x2, 0xd, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV AX, [0xd02]' failed.");
	}

	// MOV EAX,memoffs32
	[Test]
	public void MOV_EAX_memoffs32 ()
	{
		// MOV EAX, [0x5f19da8]
		// MOV_EAX (0x5f19da8)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV_EAX (0x5f19da8);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xa1, 0xa8, 0x9d, 0xf1, 0x5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV EAX, [0x5f19da8]' failed.");
	}

	// MOV memoffs8,AL
	[Test]
	public void MOV_memoffs8_AL ()
	{
		// MOV Byte [0xe], AL
		// MOV__AL (0xe)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV__AL (0xe);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xa2, 0xe, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV Byte [0xe], AL' failed.");
	}

	// MOV memoffs16,AX
	[Test]
	public void MOV_memoffs16_AX ()
	{
		// MOV Word [0x551], AX
		// MOV__AX (0x551)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV__AX (0x551);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xa3, 0x51, 0x5, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV Word [0x551], AX' failed.");
	}

	// MOV memoffs32,EAX
	[Test]
	public void MOV_memoffs32_EAX ()
	{
		// MOV DWord [0xfab5d54], EAX
		// MOV__EAX (0xfab5d54)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV__EAX (0xfab5d54);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xa3, 0x54, 0x5d, 0xab, 0xf };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV DWord [0xfab5d54], EAX' failed.");
	}

	// MOV mem16,segreg
	[Test]
	public void MOV_mem16_segreg ()
	{
		// MOV [GS:EBP + ECX*1], DS
		// MOV (new WordMemory(Seg.GS, R32.EBP, R32.ECX, 0), Seg.DS)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (new WordMemory (Seg.GS, R32.EBP, R32.ECX, 0), Seg.DS);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0x8c, 0x5c, 0xd, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV [GS:EBP + ECX*1], DS' failed.");
	}

	// MOV mem32,segreg
	[Test]
	public void MOV_mem32_segreg ()
	{
		// MOV [ES:0x12345678], GS
		// MOV (new DWordMemory(Seg.ES, null, null, 0, 0x12345678), Seg.GS)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (new DWordMemory (Seg.ES, null, null, 0, 0x12345678), Seg.GS);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0x8c, 0x2d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV [ES:0x12345678], GS' failed.");
	}

	// MOV segreg,mem16
	[Test]
	public void MOV_segreg_mem16 ()
	{
		// MOV ES, [DS:0x12345678]
		// MOV (Seg.ES, new WordMemory(Seg.DS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (Seg.ES, new WordMemory (Seg.DS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x8e, 0x5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV ES, [DS:0x12345678]' failed.");
	}

	// MOV segreg,mem32
	[Test]
	public void MOV_segreg_mem32 ()
	{
		// MOV FS, [SS:EDX + EDI*8 + 0x12345678]
		// MOV (Seg.FS, new DWordMemory(Seg.SS, R32.EDX, R32.EDI, 3, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (Seg.FS, new DWordMemory (Seg.SS, R32.EDX, R32.EDI, 3, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0x8e, 0xa4, 0xfa, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV FS, [SS:EDX + EDI*8 + 0x12345678]' failed.");
	}

	// MOV reg32,CR0/2/3/4
	[Test]
	public void MOV_reg32_CR0234 ()
	{
		// MOV EAX, CR2
		// MOV (R32.EAX, CR.CR2)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (R32.EAX, CR.CR2);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x20, 0xd0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV EAX, CR2' failed.");
	}

	// MOV reg32,DR0/1/2/3/6/7
	[Test]
	public void MOV_reg32_DR012367 ()
	{
		// MOV ECX, DR4
		// MOV (R32.ECX, DR.DR4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (R32.ECX, DR.DR4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x21, 0xe1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV ECX, DR4' failed.");
	}

	// MOV reg32,TR3/4/5/6/7
	[Test]
	public void MOV_reg32_TR34567 ()
	{
		// MOV ESP, TR3
		// MOV (R32.ESP, TR.TR3)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (R32.ESP, TR.TR3);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x24, 0xdc };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV ESP, TR3' failed.");
	}

	// MOV CR0/2/3/4,reg32
	[Test]
	public void MOV_CR0234_reg32 ()
	{
		// MOV CR0, ECX
		// MOV (CR.CR0, R32.ECX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (CR.CR0, R32.ECX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x22, 0xc1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV CR0, ECX' failed.");
	}

	// MOV DR0/1/2/3/6/7,reg32
	[Test]
	public void MOV_DR012367_reg32 ()
	{
		// MOV DR4, EDI
		// MOV (DR.DR4, R32.EDI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (DR.DR4, R32.EDI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x23, 0xe7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV DR4, EDI' failed.");
	}

	// MOV TR3/4/5/6/7,reg32
	[Test]
	public void MOV_TR34567_reg32 ()
	{
		// MOV TR5, EBP
		// MOV (TR.TR5, R32.EBP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (TR.TR5, R32.EBP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x26, 0xed };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV TR5, EBP' failed.");
	}

	// MOV rmreg8,reg8
	[Test]
	public void MOV_rmreg8_reg8 ()
	{
		// MOV BH, AH
		// MOV (R8.BH, R8.AH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (R8.BH, R8.AH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x88, 0xe7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV BH, AH' failed.");
	}

	// MOV rmreg16,reg16
	[Test]
	public void MOV_rmreg16_reg16 ()
	{
		// MOV SP, SP
		// MOV (R16.SP, R16.SP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (R16.SP, R16.SP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x89, 0xe4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV SP, SP' failed.");
	}

	// MOV rmreg32,reg32
	[Test]
	public void MOV_rmreg32_reg32 ()
	{
		// MOV EDX, EAX
		// MOV (R32.EDX, R32.EAX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (R32.EDX, R32.EAX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x89, 0xc2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV EDX, EAX' failed.");
	}

	// MOV rmreg16,segreg
	[Test]
	public void MOV_rmreg16_segreg ()
	{
		// MOV DX, GS
		// MOV (R16.DX, Seg.GS)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (R16.DX, Seg.GS);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x8c, 0xea };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV DX, GS' failed.");
	}

	// MOV rmreg32,segreg
	[Test]
	public void MOV_rmreg32_segreg ()
	{
		// MOV EBP, SS
		// MOV (R32.EBP, Seg.SS)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (R32.EBP, Seg.SS);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x8c, 0xd5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV EBP, SS' failed.");
	}

	// MOV segreg,rmreg16
	[Test]
	public void MOV_segreg_rmreg16 ()
	{
		// MOV DS, DI
		// MOV (Seg.DS, R16.DI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (Seg.DS, R16.DI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x8e, 0xdf };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV DS, DI' failed.");
	}

	// MOV segreg,rmreg32
	[Test]
	public void MOV_segreg_rmreg32 ()
	{
		// MOV DS, EAX
		// MOV (Seg.DS, R32.EAX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOV (Seg.DS, R32.EAX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x8e, 0xd8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOV DS, EAX' failed.");
	}

	// MOVSB 
	[Test]
	public void MOVSB ()
	{
		// MOVSB
		// MOVSB ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOVSB ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xa4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOVSB' failed.");
	}

	// MOVSD 
	[Test]
	public void MOVSD ()
	{
		// MOVSD
		// MOVSD ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOVSD ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xa5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOVSD' failed.");
	}

	// MOVSW 
	[Test]
	public void MOVSW ()
	{
		// MOVSW
		// MOVSW ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOVSW ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xa5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOVSW' failed.");
	}

	// MOVSX reg16,mem8
	[Test]
	public void MOVSX_reg16_mem8 ()
	{
		// MOVSX BX, Byte [CS:0x12345678]
		// MOVSX (R16.BX, new ByteMemory(Seg.CS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOVSX (R16.BX, new ByteMemory (Seg.CS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0x66, 0xf, 0xbe, 0x1d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOVSX BX, Byte [CS:0x12345678]' failed.");
	}

	// MOVSX reg32,mem8
	[Test]
	public void MOVSX_reg32_mem8 ()
	{
		// MOVSX EBX, Byte [CS:EBP*4]
		// MOVSX (R32.EBX, new ByteMemory(Seg.CS, null, R32.EBP, 2))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOVSX (R32.EBX, new ByteMemory (Seg.CS, null, R32.EBP, 2));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xf, 0xbe, 0x1c, 0xad, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOVSX EBX, Byte [CS:EBP*4]' failed.");
	}

	// MOVSX reg32,mem16
	[Test]
	public void MOVSX_reg32_mem16 ()
	{
		// MOVSX EDX, Word [CS:EAX*8]
		// MOVSX (R32.EDX, new WordMemory(Seg.CS, null, R32.EAX, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOVSX (R32.EDX, new WordMemory (Seg.CS, null, R32.EAX, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xf, 0xbf, 0x14, 0xc5, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOVSX EDX, Word [CS:EAX*8]' failed.");
	}

	// MOVSX reg16,rmreg8
	[Test]
	public void MOVSX_reg16_rmreg8 ()
	{
		// MOVSX BX, DH
		// MOVSX (R16.BX, R8.DH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOVSX (R16.BX, R8.DH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xbe, 0xde };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOVSX BX, DH' failed.");
	}

	// MOVSX reg32,rmreg8
	[Test]
	public void MOVSX_reg32_rmreg8 ()
	{
		// MOVSX EBP, AL
		// MOVSX (R32.EBP, R8.AL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOVSX (R32.EBP, R8.AL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xbe, 0xe8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOVSX EBP, AL' failed.");
	}

	// MOVSX reg32,rmreg16
	[Test]
	public void MOVSX_reg32_rmreg16 ()
	{
		// MOVSX EBP, BP
		// MOVSX (R32.EBP, R16.BP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOVSX (R32.EBP, R16.BP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xbf, 0xed };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOVSX EBP, BP' failed.");
	}

	// MOVZX reg16,mem8
	[Test]
	public void MOVZX_reg16_mem8 ()
	{
		// MOVZX CX, Byte [ECX*1]
		// MOVZX (R16.CX, new ByteMemory(null, null, R32.ECX, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOVZX (R16.CX, new ByteMemory (null, null, R32.ECX, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xb6, 0x9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOVZX CX, Byte [ECX*1]' failed.");
	}

	// MOVZX reg32,mem8
	[Test]
	public void MOVZX_reg32_mem8 ()
	{
		// MOVZX ECX, Byte [ES:EDI + EAX*8]
		// MOVZX (R32.ECX, new ByteMemory(Seg.ES, R32.EDI, R32.EAX, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOVZX (R32.ECX, new ByteMemory (Seg.ES, R32.EDI, R32.EAX, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xf, 0xb6, 0xc, 0xc7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOVZX ECX, Byte [ES:EDI + EAX*8]' failed.");
	}

	// MOVZX reg32,mem16
	[Test]
	public void MOVZX_reg32_mem16 ()
	{
		// MOVZX ECX, Word [EAX + EDX*8]
		// MOVZX (R32.ECX, new WordMemory(null, R32.EAX, R32.EDX, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOVZX (R32.ECX, new WordMemory (null, R32.EAX, R32.EDX, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xb7, 0xc, 0xd0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOVZX ECX, Word [EAX + EDX*8]' failed.");
	}

	// MOVZX reg16,rmreg8
	[Test]
	public void MOVZX_reg16_rmreg8 ()
	{
		// MOVZX CX, CH
		// MOVZX (R16.CX, R8.CH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOVZX (R16.CX, R8.CH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xb6, 0xcd };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOVZX CX, CH' failed.");
	}

	// MOVZX reg32,rmreg8
	[Test]
	public void MOVZX_reg32_rmreg8 ()
	{
		// MOVZX EAX, CH
		// MOVZX (R32.EAX, R8.CH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOVZX (R32.EAX, R8.CH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xb6, 0xc5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOVZX EAX, CH' failed.");
	}

	// MOVZX reg32,rmreg16
	[Test]
	public void MOVZX_reg32_rmreg16 ()
	{
		// MOVZX EDI, SI
		// MOVZX (R32.EDI, R16.SI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MOVZX (R32.EDI, R16.SI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xb7, 0xfe };
		Assert.IsTrue (CompareData (memoryStream, target), "'MOVZX EDI, SI' failed.");
	}

	// MUL mem8
	[Test]
	public void MUL_mem8 ()
	{
		// MUL Byte [SS:EDX]
		// MUL (new ByteMemory(Seg.SS, R32.EDX, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MUL (new ByteMemory (Seg.SS, R32.EDX, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0xf6, 0x22 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MUL Byte [SS:EDX]' failed.");
	}

	// MUL mem16
	[Test]
	public void MUL_mem16 ()
	{
		// MUL Word [GS:EAX]
		// MUL (new WordMemory(Seg.GS, R32.EAX, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MUL (new WordMemory (Seg.GS, R32.EAX, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0x66, 0xf7, 0x20 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MUL Word [GS:EAX]' failed.");
	}

	// MUL mem32
	[Test]
	public void MUL_mem32 ()
	{
		// MUL DWord [FS:ESI + ESI*8]
		// MUL (new DWordMemory(Seg.FS, R32.ESI, R32.ESI, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MUL (new DWordMemory (Seg.FS, R32.ESI, R32.ESI, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xf7, 0x24, 0xf6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MUL DWord [FS:ESI + ESI*8]' failed.");
	}

	// MUL rmreg8
	[Test]
	public void MUL_rmreg8 ()
	{
		// MUL AH
		// MUL (R8.AH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MUL (R8.AH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf6, 0xe4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MUL AH' failed.");
	}

	// MUL rmreg16
	[Test]
	public void MUL_rmreg16 ()
	{
		// MUL SI
		// MUL (R16.SI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MUL (R16.SI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf7, 0xe6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MUL SI' failed.");
	}

	// MUL rmreg32
	[Test]
	public void MUL_rmreg32 ()
	{
		// MUL ECX
		// MUL (R32.ECX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.MUL (R32.ECX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf7, 0xe1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'MUL ECX' failed.");
	}

	// NEG mem8
	[Test]
	public void NEG_mem8 ()
	{
		// NEG Byte [ES:0x12345678]
		// NEG (new ByteMemory(Seg.ES, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.NEG (new ByteMemory (Seg.ES, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xf6, 0x1d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'NEG Byte [ES:0x12345678]' failed.");
	}

	// NEG mem16
	[Test]
	public void NEG_mem16 ()
	{
		// NEG Word [EDX + EAX*4 + 0x12345678]
		// NEG (new WordMemory(null, R32.EDX, R32.EAX, 2, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.NEG (new WordMemory (null, R32.EDX, R32.EAX, 2, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf7, 0x9c, 0x82, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'NEG Word [EDX + EAX*4 + 0x12345678]' failed.");
	}

	// NEG mem32
	[Test]
	public void NEG_mem32 ()
	{
		// NEG DWord [CS:0x12345678]
		// NEG (new DWordMemory(Seg.CS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.NEG (new DWordMemory (Seg.CS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xf7, 0x1d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'NEG DWord [CS:0x12345678]' failed.");
	}

	// NEG rmreg8
	[Test]
	public void NEG_rmreg8 ()
	{
		// NEG AH
		// NEG (R8.AH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.NEG (R8.AH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf6, 0xdc };
		Assert.IsTrue (CompareData (memoryStream, target), "'NEG AH' failed.");
	}

	// NEG rmreg16
	[Test]
	public void NEG_rmreg16 ()
	{
		// NEG SP
		// NEG (R16.SP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.NEG (R16.SP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf7, 0xdc };
		Assert.IsTrue (CompareData (memoryStream, target), "'NEG SP' failed.");
	}

	// NEG rmreg32
	[Test]
	public void NEG_rmreg32 ()
	{
		// NEG ESI
		// NEG (R32.ESI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.NEG (R32.ESI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf7, 0xde };
		Assert.IsTrue (CompareData (memoryStream, target), "'NEG ESI' failed.");
	}

	// NOP 
	[Test]
	public void NOP ()
	{
		// NOP
		// NOP ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.NOP ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x90 };
		Assert.IsTrue (CompareData (memoryStream, target), "'NOP' failed.");
	}

	// NOT mem8
	[Test]
	public void NOT_mem8 ()
	{
		// NOT Byte [ESP + EBP*8]
		// NOT (new ByteMemory(null, R32.ESP, R32.EBP, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.NOT (new ByteMemory (null, R32.ESP, R32.EBP, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf6, 0x14, 0xec };
		Assert.IsTrue (CompareData (memoryStream, target), "'NOT Byte [ESP + EBP*8]' failed.");
	}

	// NOT mem16
	[Test]
	public void NOT_mem16 ()
	{
		// NOT Word [DS:EDX*4]
		// NOT (new WordMemory(Seg.DS, null, R32.EDX, 2))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.NOT (new WordMemory (Seg.DS, null, R32.EDX, 2));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x66, 0xf7, 0x14, 0x95, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'NOT Word [DS:EDX*4]' failed.");
	}

	// NOT mem32
	[Test]
	public void NOT_mem32 ()
	{
		// NOT DWord [FS:EDI*1]
		// NOT (new DWordMemory(Seg.FS, null, R32.EDI, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.NOT (new DWordMemory (Seg.FS, null, R32.EDI, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xf7, 0x17 };
		Assert.IsTrue (CompareData (memoryStream, target), "'NOT DWord [FS:EDI*1]' failed.");
	}

	// NOT rmreg8
	[Test]
	public void NOT_rmreg8 ()
	{
		// NOT CL
		// NOT (R8.CL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.NOT (R8.CL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf6, 0xd1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'NOT CL' failed.");
	}

	// NOT rmreg16
	[Test]
	public void NOT_rmreg16 ()
	{
		// NOT BX
		// NOT (R16.BX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.NOT (R16.BX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf7, 0xd3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'NOT BX' failed.");
	}

	// NOT rmreg32
	[Test]
	public void NOT_rmreg32 ()
	{
		// NOT EDX
		// NOT (R32.EDX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.NOT (R32.EDX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf7, 0xd2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'NOT EDX' failed.");
	}

	// OR mem8,reg8
	[Test]
	public void OR_mem8_reg8 ()
	{
		// OR [ES:ESI + EDX*1], CL
		// OR (new ByteMemory(Seg.ES, R32.ESI, R32.EDX, 0), R8.CL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (new ByteMemory (Seg.ES, R32.ESI, R32.EDX, 0), R8.CL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0x8, 0xc, 0x16 };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR [ES:ESI + EDX*1], CL' failed.");
	}

	// OR mem16,reg16
	[Test]
	public void OR_mem16_reg16 ()
	{
		// OR [ES:EDI*2], DX
		// OR (new WordMemory(Seg.ES, null, R32.EDI, 1), R16.DX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (new WordMemory (Seg.ES, null, R32.EDI, 1), R16.DX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0x66, 0x9, 0x14, 0x3f };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR [ES:EDI*2], DX' failed.");
	}

	// OR mem32,reg32
	[Test]
	public void OR_mem32_reg32 ()
	{
		// OR [CS:EAX + EBX*2], EDI
		// OR (new DWordMemory(Seg.CS, R32.EAX, R32.EBX, 1), R32.EDI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (new DWordMemory (Seg.CS, R32.EAX, R32.EBX, 1), R32.EDI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0x9, 0x3c, 0x58 };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR [CS:EAX + EBX*2], EDI' failed.");
	}

	// OR reg8,mem8
	[Test]
	public void OR_reg8_mem8 ()
	{
		// OR AL, [GS:0x12345678]
		// OR (R8.AL, new ByteMemory(Seg.GS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (R8.AL, new ByteMemory (Seg.GS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xa, 0x5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR AL, [GS:0x12345678]' failed.");
	}

	// OR reg16,mem16
	[Test]
	public void OR_reg16_mem16 ()
	{
		// OR DX, [EDI + 0x12345678]
		// OR (R16.DX, new WordMemory(null, R32.EDI, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (R16.DX, new WordMemory (null, R32.EDI, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xb, 0x97, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR DX, [EDI + 0x12345678]' failed.");
	}

	// OR reg32,mem32
	[Test]
	public void OR_reg32_mem32 ()
	{
		// OR EBX, [0x12345678]
		// OR (R32.EBX, new DWordMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (R32.EBX, new DWordMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xb, 0x1d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR EBX, [0x12345678]' failed.");
	}

	// OR mem8,imm8
	[Test]
	public void OR_mem8_imm8 ()
	{
		// OR Byte [SS:0x12345678], 0x7
		// OR (new ByteMemory(Seg.SS, null, null, 0, 0x12345678), 0x7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (new ByteMemory (Seg.SS, null, null, 0, 0x12345678), 0x7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0x80, 0xd, 0x78, 0x56, 0x34, 0x12, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR Byte [SS:0x12345678], 0x7' failed.");
	}

	// OR mem16,imm16
	[Test]
	public void OR_mem16_imm16 ()
	{
		// OR Word [CS:0x12345678], 0xe6a
		// OR (new WordMemory(Seg.CS, null, null, 0, 0x12345678), 0xe6a)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (new WordMemory (Seg.CS, null, null, 0, 0x12345678), 0xe6a);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0x66, 0x81, 0xd, 0x78, 0x56, 0x34, 0x12, 0x6a, 0xe };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR Word [CS:0x12345678], 0xe6a' failed.");
	}

	// OR mem32,imm32
	[Test]
	public void OR_mem32_imm32 ()
	{
		// OR DWord [CS:EBP + EAX*8], 0xbeab706
		// OR (new DWordMemory(Seg.CS, R32.EBP, R32.EAX, 3), 0xbeab706)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (new DWordMemory (Seg.CS, R32.EBP, R32.EAX, 3), 0xbeab706);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0x81, 0x4c, 0xc5, 0x0, 0x6, 0xb7, 0xea, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR DWord [CS:EBP + EAX*8], 0xbeab706' failed.");
	}

	// OR mem16,imm8
	[Test]
	public void OR_mem16_imm8 ()
	{
		// OR Word [EBX + EDI*2], 0x5
		// OR (new WordMemory(null, R32.EBX, R32.EDI, 1), 0x5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (new WordMemory (null, R32.EBX, R32.EDI, 1), 0x5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x83, 0xc, 0x7b, 0x5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR Word [EBX + EDI*2], 0x5' failed.");
	}

	// OR mem32,imm8
	[Test]
	public void OR_mem32_imm8 ()
	{
		// OR DWord [EBP*1 + 0x12345678], 0x9
		// OR (new DWordMemory(null, null, R32.EBP, 0, 0x12345678), 0x9)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (new DWordMemory (null, null, R32.EBP, 0, 0x12345678), 0x9);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x83, 0x8d, 0x78, 0x56, 0x34, 0x12, 0x9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR DWord [EBP*1 + 0x12345678], 0x9' failed.");
	}

	// OR rmreg8,reg8
	[Test]
	public void OR_rmreg8_reg8 ()
	{
		// OR CH, CH
		// OR (R8.CH, R8.CH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (R8.CH, R8.CH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x8, 0xed };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR CH, CH' failed.");
	}

	// OR rmreg16,reg16
	[Test]
	public void OR_rmreg16_reg16 ()
	{
		// OR SI, SI
		// OR (R16.SI, R16.SI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (R16.SI, R16.SI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x9, 0xf6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR SI, SI' failed.");
	}

	// OR rmreg32,reg32
	[Test]
	public void OR_rmreg32_reg32 ()
	{
		// OR EBX, ESP
		// OR (R32.EBX, R32.ESP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (R32.EBX, R32.ESP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x9, 0xe3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR EBX, ESP' failed.");
	}

	// OR rmreg8,imm8
	[Test]
	public void OR_rmreg8_imm8 ()
	{
		// OR CH, 0xb
		// OR (R8.CH, 0xb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (R8.CH, 0xb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x80, 0xcd, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR CH, 0xb' failed.");
	}

	// OR rmreg16,imm16
	[Test]
	public void OR_rmreg16_imm16 ()
	{
		// OR SI, 0xee1
		// OR (R16.SI, 0xee1)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (R16.SI, 0xee1);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x81, 0xce, 0xe1, 0xe };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR SI, 0xee1' failed.");
	}

	// OR rmreg32,imm32
	[Test]
	public void OR_rmreg32_imm32 ()
	{
		// OR EBP, 0x260018b
		// OR (R32.EBP, 0x260018b)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (R32.EBP, 0x260018b);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x81, 0xcd, 0x8b, 0x1, 0x60, 0x2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR EBP, 0x260018b' failed.");
	}

	// OR rmreg16,imm8
	[Test]
	public void OR_rmreg16_imm8 ()
	{
		// OR AX, 0x1
		// OR (R16.AX, 0x1)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (R16.AX, 0x1);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x83, 0xc8, 0x1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR AX, 0x1' failed.");
	}

	// OR rmreg32,imm8
	[Test]
	public void OR_rmreg32_imm8 ()
	{
		// OR EDX, 0x8
		// OR (R32.EDX, 0x8)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OR (R32.EDX, 0x8);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x83, 0xca, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'OR EDX, 0x8' failed.");
	}

	// OUT imm8,AL
	[Test]
	public void OUT_imm8_AL ()
	{
		// OUT 0xf, AL
		// OUT__AL (0xf)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OUT__AL (0xf);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xe6, 0xf };
		Assert.IsTrue (CompareData (memoryStream, target), "'OUT 0xf, AL' failed.");
	}

	// OUT imm8,AX
	[Test]
	public void OUT_imm8_AX ()
	{
		// OUT 0x3, AX
		// OUT__AX (0x3)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OUT__AX (0x3);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xe7, 0x3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'OUT 0x3, AX' failed.");
	}

	// OUT imm8,EAX
	[Test]
	public void OUT_imm8_EAX ()
	{
		// OUT 0xe, EAX
		// OUT__EAX (0xe)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OUT__EAX (0xe);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xe7, 0xe };
		Assert.IsTrue (CompareData (memoryStream, target), "'OUT 0xe, EAX' failed.");
	}

	// OUT DX,AL
	[Test]
	public void OUT_DX_AL ()
	{
		// OUT DX, AL
		// OUT_DX__AL ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OUT_DX__AL ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xee };
		Assert.IsTrue (CompareData (memoryStream, target), "'OUT DX, AL' failed.");
	}

	// OUT DX,AX
	[Test]
	public void OUT_DX_AX ()
	{
		// OUT DX, AX
		// OUT_DX__AX ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OUT_DX__AX ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xef };
		Assert.IsTrue (CompareData (memoryStream, target), "'OUT DX, AX' failed.");
	}

	// OUT DX,EAX
	[Test]
	public void OUT_DX_EAX ()
	{
		// OUT DX, EAX
		// OUT_DX__EAX ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OUT_DX__EAX ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xef };
		Assert.IsTrue (CompareData (memoryStream, target), "'OUT DX, EAX' failed.");
	}

	// OUTSB 
	[Test]
	public void OUTSB ()
	{
		// OUTSB
		// OUTSB ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OUTSB ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x6e };
		Assert.IsTrue (CompareData (memoryStream, target), "'OUTSB' failed.");
	}

	// OUTSD 
	[Test]
	public void OUTSD ()
	{
		// OUTSD
		// OUTSD ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OUTSD ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x6f };
		Assert.IsTrue (CompareData (memoryStream, target), "'OUTSD' failed.");
	}

	// OUTSW 
	[Test]
	public void OUTSW ()
	{
		// OUTSW
		// OUTSW ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.OUTSW ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x6f };
		Assert.IsTrue (CompareData (memoryStream, target), "'OUTSW' failed.");
	}

	// PAUSE 
	[Test]
	public void PAUSE ()
	{
		// PAUSE
		// PAUSE ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PAUSE ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf3, 0x90 };
		Assert.IsTrue (CompareData (memoryStream, target), "'PAUSE' failed.");
	}

	// POP reg16
	[Test]
	public void POP_reg16 ()
	{
		// POP DI
		// POP (R16.DI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.POP (R16.DI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x5f };
		Assert.IsTrue (CompareData (memoryStream, target), "'POP DI' failed.");
	}

	// POP reg32
	[Test]
	public void POP_reg32 ()
	{
		// POP EBP
		// POP (R32.EBP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.POP (R32.EBP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x5d };
		Assert.IsTrue (CompareData (memoryStream, target), "'POP EBP' failed.");
	}

	// POP mem16
	[Test]
	public void POP_mem16 ()
	{
		// POP Word [EDX + EDX*1]
		// POP (new WordMemory(null, R32.EDX, R32.EDX, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.POP (new WordMemory (null, R32.EDX, R32.EDX, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x8f, 0x4, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'POP Word [EDX + EDX*1]' failed.");
	}

	// POP mem32
	[Test]
	public void POP_mem32 ()
	{
		// POP DWord [DS:EAX + EDX*2]
		// POP (new DWordMemory(Seg.DS, R32.EAX, R32.EDX, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.POP (new DWordMemory (Seg.DS, R32.EAX, R32.EDX, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x8f, 0x4, 0x50 };
		Assert.IsTrue (CompareData (memoryStream, target), "'POP DWord [DS:EAX + EDX*2]' failed.");
	}

	// POP segreg
	[Test]
	public void POP_segreg ()
	{
		// POP FS
		// POP (Seg.FS)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.POP (Seg.FS);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xa1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'POP FS' failed.");
	}

	// POPA 
	[Test]
	public void POPA ()
	{
		// POPA
		// POPA ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.POPA ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x61 };
		Assert.IsTrue (CompareData (memoryStream, target), "'POPA' failed.");
	}

	// POPAD 
	[Test]
	public void POPAD ()
	{
		// POPAD
		// POPAD ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.POPAD ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x61 };
		Assert.IsTrue (CompareData (memoryStream, target), "'POPAD' failed.");
	}

	// POPAW 
	[Test]
	public void POPAW ()
	{
		// POPAW
		// POPAW ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.POPAW ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x61 };
		Assert.IsTrue (CompareData (memoryStream, target), "'POPAW' failed.");
	}

	// POPF 
	[Test]
	public void POPF ()
	{
		// POPF
		// POPF ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.POPF ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x9d };
		Assert.IsTrue (CompareData (memoryStream, target), "'POPF' failed.");
	}

	// POPFD 
	[Test]
	public void POPFD ()
	{
		// POPFD
		// POPFD ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.POPFD ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x9d };
		Assert.IsTrue (CompareData (memoryStream, target), "'POPFD' failed.");
	}

	// POPFW 
	[Test]
	public void POPFW ()
	{
		// POPFW
		// POPFW ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.POPFW ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x9d };
		Assert.IsTrue (CompareData (memoryStream, target), "'POPFW' failed.");
	}

	// PREFETCHNTA m8
	[Test]
	public void PREFETCHNTA_m8 ()
	{
		// PREFETCHNTA [EDI + 0x12345678]
		// PREFETCHNTA (new ByteMemory(null, R32.EDI, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PREFETCHNTA (new ByteMemory (null, R32.EDI, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x18, 0x87, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'PREFETCHNTA [EDI + 0x12345678]' failed.");
	}

	// PREFETCHT0 m8
	[Test]
	public void PREFETCHT0_m8 ()
	{
		// PREFETCHT0 [GS:EBP*1 + 0x12345678]
		// PREFETCHT0 (new ByteMemory(Seg.GS, null, R32.EBP, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PREFETCHT0 (new ByteMemory (Seg.GS, null, R32.EBP, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xf, 0x18, 0x8d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'PREFETCHT0 [GS:EBP*1 + 0x12345678]' failed.");
	}

	// PREFETCHT1 m8
	[Test]
	public void PREFETCHT1_m8 ()
	{
		// PREFETCHT1 [FS:0x12345678]
		// PREFETCHT1 (new ByteMemory(Seg.FS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PREFETCHT1 (new ByteMemory (Seg.FS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xf, 0x18, 0x15, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'PREFETCHT1 [FS:0x12345678]' failed.");
	}

	// PREFETCHT2 m8
	[Test]
	public void PREFETCHT2_m8 ()
	{
		// PREFETCHT2 [CS:EDX + EBX*2]
		// PREFETCHT2 (new ByteMemory(Seg.CS, R32.EDX, R32.EBX, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PREFETCHT2 (new ByteMemory (Seg.CS, R32.EDX, R32.EBX, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xf, 0x18, 0x1c, 0x5a };
		Assert.IsTrue (CompareData (memoryStream, target), "'PREFETCHT2 [CS:EDX + EBX*2]' failed.");
	}

	// PUSH reg16
	[Test]
	public void PUSH_reg16 ()
	{
		// PUSH AX
		// PUSH (R16.AX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PUSH (R16.AX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x50 };
		Assert.IsTrue (CompareData (memoryStream, target), "'PUSH AX' failed.");
	}

	// PUSH reg32
	[Test]
	public void PUSH_reg32 ()
	{
		// PUSH EBP
		// PUSH (R32.EBP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PUSH (R32.EBP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x55 };
		Assert.IsTrue (CompareData (memoryStream, target), "'PUSH EBP' failed.");
	}

	// PUSH mem16
	[Test]
	public void PUSH_mem16 ()
	{
		// PUSH Word [GS:EBP + EDX*8 + 0x12345678]
		// PUSH (new WordMemory(Seg.GS, R32.EBP, R32.EDX, 3, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PUSH (new WordMemory (Seg.GS, R32.EBP, R32.EDX, 3, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0x66, 0xff, 0xb4, 0xd5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'PUSH Word [GS:EBP + EDX*8 + 0x12345678]' failed.");
	}

	// PUSH mem32
	[Test]
	public void PUSH_mem32 ()
	{
		// PUSH DWord [SS:0x12345678]
		// PUSH (new DWordMemory(Seg.SS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PUSH (new DWordMemory (Seg.SS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0xff, 0x35, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'PUSH DWord [SS:0x12345678]' failed.");
	}

	// PUSH imm8
	[Test]
	public void PUSH_imm8 ()
	{
		// PUSH 0x7
		// PUSH (0x7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PUSH (0x7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x6a, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'PUSH 0x7' failed.");
	}

	// PUSH imm16
	[Test]
	public void PUSH_imm16 ()
	{
		// PUSH WORD 0xd49
		// PUSH (0xd49)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PUSH (0xd49);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x68, 0x49, 0xd };
		Assert.IsTrue (CompareData (memoryStream, target), "'PUSH WORD 0xd49' failed.");
	}

	// PUSH imm32
	[Test]
	public void PUSH_imm32 ()
	{
		// PUSH 0x6dab45b
		// PUSH (0x6dab45b)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PUSH (0x6dab45b);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x68, 0x5b, 0xb4, 0xda, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'PUSH 0x6dab45b' failed.");
	}

	// PUSH segreg
	[Test]
	public void PUSH_segreg ()
	{
		// PUSH SS
		// PUSH (Seg.SS)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PUSH (Seg.SS);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x16 };
		Assert.IsTrue (CompareData (memoryStream, target), "'PUSH SS' failed.");
	}

	// PUSHA 
	[Test]
	public void PUSHA ()
	{
		// PUSHA
		// PUSHA ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PUSHA ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x60 };
		Assert.IsTrue (CompareData (memoryStream, target), "'PUSHA' failed.");
	}

	// PUSHAD 
	[Test]
	public void PUSHAD ()
	{
		// PUSHAD
		// PUSHAD ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PUSHAD ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x60 };
		Assert.IsTrue (CompareData (memoryStream, target), "'PUSHAD' failed.");
	}

	// PUSHAW 
	[Test]
	public void PUSHAW ()
	{
		// PUSHAW
		// PUSHAW ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PUSHAW ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x60 };
		Assert.IsTrue (CompareData (memoryStream, target), "'PUSHAW' failed.");
	}

	// PUSHF 
	[Test]
	public void PUSHF ()
	{
		// PUSHF
		// PUSHF ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PUSHF ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x9c };
		Assert.IsTrue (CompareData (memoryStream, target), "'PUSHF' failed.");
	}

	// PUSHFD 
	[Test]
	public void PUSHFD ()
	{
		// PUSHFD
		// PUSHFD ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PUSHFD ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x9c };
		Assert.IsTrue (CompareData (memoryStream, target), "'PUSHFD' failed.");
	}

	// PUSHFW 
	[Test]
	public void PUSHFW ()
	{
		// PUSHFW
		// PUSHFW ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.PUSHFW ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x9c };
		Assert.IsTrue (CompareData (memoryStream, target), "'PUSHFW' failed.");
	}

	// RCL mem8,CL
	[Test]
	public void RCL_mem8_CL ()
	{
		// RCL Byte [EAX*8 + 0x12345678], CL
		// RCL__CL (new ByteMemory(null, null, R32.EAX, 3, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCL__CL (new ByteMemory (null, null, R32.EAX, 3, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd2, 0x14, 0xc5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCL Byte [EAX*8 + 0x12345678], CL' failed.");
	}

	// RCL mem8,imm8
	[Test]
	public void RCL_mem8_imm8 ()
	{
		// RCL Byte [GS:ECX*4], 0x3
		// RCL (new ByteMemory(Seg.GS, null, R32.ECX, 2), 0x3)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCL (new ByteMemory (Seg.GS, null, R32.ECX, 2), 0x3);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xc0, 0x14, 0x8d, 0x0, 0x0, 0x0, 0x0, 0x3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCL Byte [GS:ECX*4], 0x3' failed.");
	}

	// RCL mem16,CL
	[Test]
	public void RCL_mem16_CL ()
	{
		// RCL Word [ES:EBP + EAX*8], CL
		// RCL__CL (new WordMemory(Seg.ES, R32.EBP, R32.EAX, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCL__CL (new WordMemory (Seg.ES, R32.EBP, R32.EAX, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0x66, 0xd3, 0x54, 0xc5, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCL Word [ES:EBP + EAX*8], CL' failed.");
	}

	// RCL mem16,imm8
	[Test]
	public void RCL_mem16_imm8 ()
	{
		// RCL Word [SS:EAX*2], 0x0
		// RCL (new WordMemory(Seg.SS, null, R32.EAX, 1), 0x0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCL (new WordMemory (Seg.SS, null, R32.EAX, 1), 0x0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0x66, 0xc1, 0x14, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCL Word [SS:EAX*2], 0x0' failed.");
	}

	// RCL mem32,CL
	[Test]
	public void RCL_mem32_CL ()
	{
		// RCL DWord [FS:ECX + EBX*8 + 0x12345678], CL
		// RCL__CL (new DWordMemory(Seg.FS, R32.ECX, R32.EBX, 3, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCL__CL (new DWordMemory (Seg.FS, R32.ECX, R32.EBX, 3, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xd3, 0x94, 0xd9, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCL DWord [FS:ECX + EBX*8 + 0x12345678], CL' failed.");
	}

	// RCL mem32,imm8
	[Test]
	public void RCL_mem32_imm8 ()
	{
		// RCL DWord [EBX + ECX*2 + 0x12345678], 0xe
		// RCL (new DWordMemory(null, R32.EBX, R32.ECX, 1, 0x12345678), 0xe)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCL (new DWordMemory (null, R32.EBX, R32.ECX, 1, 0x12345678), 0xe);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc1, 0x94, 0x4b, 0x78, 0x56, 0x34, 0x12, 0xe };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCL DWord [EBX + ECX*2 + 0x12345678], 0xe' failed.");
	}

	// RCL rmreg8,CL
	[Test]
	public void RCL_rmreg8_CL ()
	{
		// RCL AH, CL
		// RCL__CL (R8.AH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCL__CL (R8.AH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd2, 0xd4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCL AH, CL' failed.");
	}

	// RCL rmreg8,imm8
	[Test]
	public void RCL_rmreg8_imm8 ()
	{
		// RCL AL, 0x5
		// RCL (R8.AL, 0x5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCL (R8.AL, 0x5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc0, 0xd0, 0x5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCL AL, 0x5' failed.");
	}

	// RCL rmreg16,CL
	[Test]
	public void RCL_rmreg16_CL ()
	{
		// RCL BX, CL
		// RCL__CL (R16.BX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCL__CL (R16.BX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xd3, 0xd3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCL BX, CL' failed.");
	}

	// RCL rmreg16,imm8
	[Test]
	public void RCL_rmreg16_imm8 ()
	{
		// RCL AX, 0xd
		// RCL (R16.AX, 0xd)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCL (R16.AX, 0xd);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xc1, 0xd0, 0xd };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCL AX, 0xd' failed.");
	}

	// RCL rmreg32,CL
	[Test]
	public void RCL_rmreg32_CL ()
	{
		// RCL EDI, CL
		// RCL__CL (R32.EDI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCL__CL (R32.EDI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd3, 0xd7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCL EDI, CL' failed.");
	}

	// RCL rmreg32,imm8
	[Test]
	public void RCL_rmreg32_imm8 ()
	{
		// RCL ESI, 0xb
		// RCL (R32.ESI, 0xb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCL (R32.ESI, 0xb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc1, 0xd6, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCL ESI, 0xb' failed.");
	}

	// RCR mem8,CL
	[Test]
	public void RCR_mem8_CL ()
	{
		// RCR Byte [ESP + EAX*2 + 0x12345678], CL
		// RCR__CL (new ByteMemory(null, R32.ESP, R32.EAX, 1, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCR__CL (new ByteMemory (null, R32.ESP, R32.EAX, 1, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd2, 0x9c, 0x44, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCR Byte [ESP + EAX*2 + 0x12345678], CL' failed.");
	}

	// RCR mem8,imm8
	[Test]
	public void RCR_mem8_imm8 ()
	{
		// RCR Byte [SS:0x12345678], 0x3
		// RCR (new ByteMemory(Seg.SS, null, null, 0, 0x12345678), 0x3)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCR (new ByteMemory (Seg.SS, null, null, 0, 0x12345678), 0x3);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0xc0, 0x1d, 0x78, 0x56, 0x34, 0x12, 0x3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCR Byte [SS:0x12345678], 0x3' failed.");
	}

	// RCR mem16,CL
	[Test]
	public void RCR_mem16_CL ()
	{
		// RCR Word [FS:0x12345678], CL
		// RCR__CL (new WordMemory(Seg.FS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCR__CL (new WordMemory (Seg.FS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0x66, 0xd3, 0x1d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCR Word [FS:0x12345678], CL' failed.");
	}

	// RCR mem16,imm8
	[Test]
	public void RCR_mem16_imm8 ()
	{
		// RCR Word [FS:0x12345678], 0x4
		// RCR (new WordMemory(Seg.FS, null, null, 0, 0x12345678), 0x4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCR (new WordMemory (Seg.FS, null, null, 0, 0x12345678), 0x4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0x66, 0xc1, 0x1d, 0x78, 0x56, 0x34, 0x12, 0x4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCR Word [FS:0x12345678], 0x4' failed.");
	}

	// RCR mem32,CL
	[Test]
	public void RCR_mem32_CL ()
	{
		// RCR DWord [EAX], CL
		// RCR__CL (new DWordMemory(null, R32.EAX, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCR__CL (new DWordMemory (null, R32.EAX, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd3, 0x18 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCR DWord [EAX], CL' failed.");
	}

	// RCR mem32,imm8
	[Test]
	public void RCR_mem32_imm8 ()
	{
		// RCR DWord [GS:0x12345678], 0x9
		// RCR (new DWordMemory(Seg.GS, null, null, 0, 0x12345678), 0x9)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCR (new DWordMemory (Seg.GS, null, null, 0, 0x12345678), 0x9);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xc1, 0x1d, 0x78, 0x56, 0x34, 0x12, 0x9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCR DWord [GS:0x12345678], 0x9' failed.");
	}

	// RCR rmreg8,CL
	[Test]
	public void RCR_rmreg8_CL ()
	{
		// RCR DH, CL
		// RCR__CL (R8.DH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCR__CL (R8.DH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd2, 0xde };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCR DH, CL' failed.");
	}

	// RCR rmreg8,imm8
	[Test]
	public void RCR_rmreg8_imm8 ()
	{
		// RCR DL, 0x6
		// RCR (R8.DL, 0x6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCR (R8.DL, 0x6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc0, 0xda, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCR DL, 0x6' failed.");
	}

	// RCR rmreg16,CL
	[Test]
	public void RCR_rmreg16_CL ()
	{
		// RCR BP, CL
		// RCR__CL (R16.BP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCR__CL (R16.BP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xd3, 0xdd };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCR BP, CL' failed.");
	}

	// RCR rmreg16,imm8
	[Test]
	public void RCR_rmreg16_imm8 ()
	{
		// RCR CX, 0x8
		// RCR (R16.CX, 0x8)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCR (R16.CX, 0x8);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xc1, 0xd9, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCR CX, 0x8' failed.");
	}

	// RCR rmreg32,CL
	[Test]
	public void RCR_rmreg32_CL ()
	{
		// RCR ESP, CL
		// RCR__CL (R32.ESP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCR__CL (R32.ESP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd3, 0xdc };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCR ESP, CL' failed.");
	}

	// RCR rmreg32,imm8
	[Test]
	public void RCR_rmreg32_imm8 ()
	{
		// RCR EAX, 0xa
		// RCR (R32.EAX, 0xa)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RCR (R32.EAX, 0xa);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc1, 0xd8, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'RCR EAX, 0xa' failed.");
	}

	// RDMSR 
	[Test]
	public void RDMSR ()
	{
		// RDMSR
		// RDMSR ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RDMSR ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x32 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RDMSR' failed.");
	}

	// RDPMC 
	[Test]
	public void RDPMC ()
	{
		// RDPMC
		// RDPMC ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RDPMC ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x33 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RDPMC' failed.");
	}

	// RDTSC 
	[Test]
	public void RDTSC ()
	{
		// RDTSC
		// RDTSC ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RDTSC ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x31 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RDTSC' failed.");
	}

	// RET 
	[Test]
	public void RET ()
	{
		// RET
		// RET ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RET ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RET' failed.");
	}

	// RET imm16
	[Test]
	public void RET_imm16 ()
	{
		// RET 0x469
		// RET (0x469)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RET (0x469);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc2, 0x69, 0x4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RET 0x469' failed.");
	}

	// RETF 
	[Test]
	public void RETF ()
	{
		// RETF
		// RETF ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RETF ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xcb };
		Assert.IsTrue (CompareData (memoryStream, target), "'RETF' failed.");
	}

	// RETF imm16
	[Test]
	public void RETF_imm16 ()
	{
		// RETF 0xba8
		// RETF (0xba8)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RETF (0xba8);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xca, 0xa8, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'RETF 0xba8' failed.");
	}

	// RETN 
	[Test]
	public void RETN ()
	{
		// RETN
		// RETN ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RETN ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RETN' failed.");
	}

	// RETN imm16
	[Test]
	public void RETN_imm16 ()
	{
		// RETN 0x749
		// RETN (0x749)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RETN (0x749);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc2, 0x49, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'RETN 0x749' failed.");
	}

	// ROL mem8,CL
	[Test]
	public void ROL_mem8_CL ()
	{
		// ROL Byte [EBP + EDX*8], CL
		// ROL__CL (new ByteMemory(null, R32.EBP, R32.EDX, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROL__CL (new ByteMemory (null, R32.EBP, R32.EDX, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd2, 0x44, 0xd5, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROL Byte [EBP + EDX*8], CL' failed.");
	}

	// ROL mem8,imm8
	[Test]
	public void ROL_mem8_imm8 ()
	{
		// ROL Byte [GS:EBP + EBX*2], 0xd
		// ROL (new ByteMemory(Seg.GS, R32.EBP, R32.EBX, 1), 0xd)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROL (new ByteMemory (Seg.GS, R32.EBP, R32.EBX, 1), 0xd);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xc0, 0x44, 0x5d, 0x0, 0xd };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROL Byte [GS:EBP + EBX*2], 0xd' failed.");
	}

	// ROL mem16,CL
	[Test]
	public void ROL_mem16_CL ()
	{
		// ROL Word [EBP*2], CL
		// ROL__CL (new WordMemory(null, null, R32.EBP, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROL__CL (new WordMemory (null, null, R32.EBP, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xd3, 0x44, 0x2d, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROL Word [EBP*2], CL' failed.");
	}

	// ROL mem16,imm8
	[Test]
	public void ROL_mem16_imm8 ()
	{
		// ROL Word [EDI], 0xc
		// ROL (new WordMemory(null, R32.EDI, null, 0), 0xc)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROL (new WordMemory (null, R32.EDI, null, 0), 0xc);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xc1, 0x7, 0xc };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROL Word [EDI], 0xc' failed.");
	}

	// ROL mem32,CL
	[Test]
	public void ROL_mem32_CL ()
	{
		// ROL DWord [EAX*8], CL
		// ROL__CL (new DWordMemory(null, null, R32.EAX, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROL__CL (new DWordMemory (null, null, R32.EAX, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd3, 0x4, 0xc5, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROL DWord [EAX*8], CL' failed.");
	}

	// ROL mem32,imm8
	[Test]
	public void ROL_mem32_imm8 ()
	{
		// ROL DWord [CS:EDX + EDI*2 + 0x12345678], 0xf
		// ROL (new DWordMemory(Seg.CS, R32.EDX, R32.EDI, 1, 0x12345678), 0xf)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROL (new DWordMemory (Seg.CS, R32.EDX, R32.EDI, 1, 0x12345678), 0xf);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xc1, 0x84, 0x7a, 0x78, 0x56, 0x34, 0x12, 0xf };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROL DWord [CS:EDX + EDI*2 + 0x12345678], 0xf' failed.");
	}

	// ROL rmreg8,CL
	[Test]
	public void ROL_rmreg8_CL ()
	{
		// ROL AL, CL
		// ROL__CL (R8.AL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROL__CL (R8.AL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd2, 0xc0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROL AL, CL' failed.");
	}

	// ROL rmreg8,imm8
	[Test]
	public void ROL_rmreg8_imm8 ()
	{
		// ROL DL, 0x4
		// ROL (R8.DL, 0x4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROL (R8.DL, 0x4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc0, 0xc2, 0x4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROL DL, 0x4' failed.");
	}

	// ROL rmreg16,CL
	[Test]
	public void ROL_rmreg16_CL ()
	{
		// ROL BP, CL
		// ROL__CL (R16.BP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROL__CL (R16.BP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xd3, 0xc5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROL BP, CL' failed.");
	}

	// ROL rmreg16,imm8
	[Test]
	public void ROL_rmreg16_imm8 ()
	{
		// ROL DX, 0x6
		// ROL (R16.DX, 0x6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROL (R16.DX, 0x6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xc1, 0xc2, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROL DX, 0x6' failed.");
	}

	// ROL rmreg32,CL
	[Test]
	public void ROL_rmreg32_CL ()
	{
		// ROL ESI, CL
		// ROL__CL (R32.ESI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROL__CL (R32.ESI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd3, 0xc6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROL ESI, CL' failed.");
	}

	// ROL rmreg32,imm8
	[Test]
	public void ROL_rmreg32_imm8 ()
	{
		// ROL EBX, 0x5
		// ROL (R32.EBX, 0x5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROL (R32.EBX, 0x5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc1, 0xc3, 0x5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROL EBX, 0x5' failed.");
	}

	// ROR mem8,CL
	[Test]
	public void ROR_mem8_CL ()
	{
		// ROR Byte [EDI + EBX*1], CL
		// ROR__CL (new ByteMemory(null, R32.EDI, R32.EBX, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROR__CL (new ByteMemory (null, R32.EDI, R32.EBX, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd2, 0xc, 0x1f };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROR Byte [EDI + EBX*1], CL' failed.");
	}

	// ROR mem8,imm8
	[Test]
	public void ROR_mem8_imm8 ()
	{
		// ROR Byte [0x12345678], 0x1
		// ROR (new ByteMemory(null, null, null, 0, 0x12345678), 0x1)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROR (new ByteMemory (null, null, null, 0, 0x12345678), 0x1);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd0, 0xd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROR Byte [0x12345678], 0x1' failed.");
	}

	// ROR mem16,CL
	[Test]
	public void ROR_mem16_CL ()
	{
		// ROR Word [ES:0x12345678], CL
		// ROR__CL (new WordMemory(Seg.ES, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROR__CL (new WordMemory (Seg.ES, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0x66, 0xd3, 0xd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROR Word [ES:0x12345678], CL' failed.");
	}

	// ROR mem16,imm8
	[Test]
	public void ROR_mem16_imm8 ()
	{
		// ROR Word [0x12345678], 0xc
		// ROR (new WordMemory(null, null, null, 0, 0x12345678), 0xc)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROR (new WordMemory (null, null, null, 0, 0x12345678), 0xc);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xc1, 0xd, 0x78, 0x56, 0x34, 0x12, 0xc };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROR Word [0x12345678], 0xc' failed.");
	}

	// ROR mem32,CL
	[Test]
	public void ROR_mem32_CL ()
	{
		// ROR DWord [EDX + EBX*2 + 0x12345678], CL
		// ROR__CL (new DWordMemory(null, R32.EDX, R32.EBX, 1, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROR__CL (new DWordMemory (null, R32.EDX, R32.EBX, 1, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd3, 0x8c, 0x5a, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROR DWord [EDX + EBX*2 + 0x12345678], CL' failed.");
	}

	// ROR mem32,imm8
	[Test]
	public void ROR_mem32_imm8 ()
	{
		// ROR DWord [ESI + ESI*1], 0x3
		// ROR (new DWordMemory(null, R32.ESI, R32.ESI, 0), 0x3)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROR (new DWordMemory (null, R32.ESI, R32.ESI, 0), 0x3);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc1, 0xc, 0x36, 0x3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROR DWord [ESI + ESI*1], 0x3' failed.");
	}

	// ROR rmreg8,CL
	[Test]
	public void ROR_rmreg8_CL ()
	{
		// ROR AH, CL
		// ROR__CL (R8.AH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROR__CL (R8.AH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd2, 0xcc };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROR AH, CL' failed.");
	}

	// ROR rmreg8,imm8
	[Test]
	public void ROR_rmreg8_imm8 ()
	{
		// ROR AL, 0x8
		// ROR (R8.AL, 0x8)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROR (R8.AL, 0x8);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc0, 0xc8, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROR AL, 0x8' failed.");
	}

	// ROR rmreg16,CL
	[Test]
	public void ROR_rmreg16_CL ()
	{
		// ROR DX, CL
		// ROR__CL (R16.DX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROR__CL (R16.DX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xd3, 0xca };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROR DX, CL' failed.");
	}

	// ROR rmreg16,imm8
	[Test]
	public void ROR_rmreg16_imm8 ()
	{
		// ROR BP, 0xe
		// ROR (R16.BP, 0xe)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROR (R16.BP, 0xe);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xc1, 0xcd, 0xe };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROR BP, 0xe' failed.");
	}

	// ROR rmreg32,CL
	[Test]
	public void ROR_rmreg32_CL ()
	{
		// ROR EDX, CL
		// ROR__CL (R32.EDX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROR__CL (R32.EDX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd3, 0xca };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROR EDX, CL' failed.");
	}

	// ROR rmreg32,imm8
	[Test]
	public void ROR_rmreg32_imm8 ()
	{
		// ROR EBP, 0xa
		// ROR (R32.EBP, 0xa)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.ROR (R32.EBP, 0xa);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc1, 0xcd, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'ROR EBP, 0xa' failed.");
	}

	// RSM 
	[Test]
	public void RSM ()
	{
		// RSM
		// RSM ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.RSM ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xaa };
		Assert.IsTrue (CompareData (memoryStream, target), "'RSM' failed.");
	}

	// SAHF 
	[Test]
	public void SAHF ()
	{
		// SAHF
		// SAHF ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAHF ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x9e };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAHF' failed.");
	}

	// SAL mem8,CL
	[Test]
	public void SAL_mem8_CL ()
	{
		// SAL Byte [0x12345678], CL
		// SAL__CL (new ByteMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAL__CL (new ByteMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd2, 0x25, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAL Byte [0x12345678], CL' failed.");
	}

	// SAL mem8,imm8
	[Test]
	public void SAL_mem8_imm8 ()
	{
		// SAL Byte [ECX + EAX*8 + 0x12345678], 0x9
		// SAL (new ByteMemory(null, R32.ECX, R32.EAX, 3, 0x12345678), 0x9)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAL (new ByteMemory (null, R32.ECX, R32.EAX, 3, 0x12345678), 0x9);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc0, 0xa4, 0xc1, 0x78, 0x56, 0x34, 0x12, 0x9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAL Byte [ECX + EAX*8 + 0x12345678], 0x9' failed.");
	}

	// SAL mem16,CL
	[Test]
	public void SAL_mem16_CL ()
	{
		// SAL Word [EBX*2], CL
		// SAL__CL (new WordMemory(null, null, R32.EBX, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAL__CL (new WordMemory (null, null, R32.EBX, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xd3, 0x24, 0x1b };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAL Word [EBX*2], CL' failed.");
	}

	// SAL mem16,imm8
	[Test]
	public void SAL_mem16_imm8 ()
	{
		// SAL Word [GS:0x12345678], 0xb
		// SAL (new WordMemory(Seg.GS, null, null, 0, 0x12345678), 0xb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAL (new WordMemory (Seg.GS, null, null, 0, 0x12345678), 0xb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0x66, 0xc1, 0x25, 0x78, 0x56, 0x34, 0x12, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAL Word [GS:0x12345678], 0xb' failed.");
	}

	// SAL mem32,CL
	[Test]
	public void SAL_mem32_CL ()
	{
		// SAL DWord [ES:EBX*2], CL
		// SAL__CL (new DWordMemory(Seg.ES, null, R32.EBX, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAL__CL (new DWordMemory (Seg.ES, null, R32.EBX, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xd3, 0x24, 0x1b };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAL DWord [ES:EBX*2], CL' failed.");
	}

	// SAL mem32,imm8
	[Test]
	public void SAL_mem32_imm8 ()
	{
		// SAL DWord [EDI + ECX*2], 0xf
		// SAL (new DWordMemory(null, R32.EDI, R32.ECX, 1), 0xf)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAL (new DWordMemory (null, R32.EDI, R32.ECX, 1), 0xf);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc1, 0x24, 0x4f, 0xf };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAL DWord [EDI + ECX*2], 0xf' failed.");
	}

	// SAL rmreg8,CL
	[Test]
	public void SAL_rmreg8_CL ()
	{
		// SAL AL, CL
		// SAL__CL (R8.AL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAL__CL (R8.AL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd2, 0xe0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAL AL, CL' failed.");
	}

	// SAL rmreg8,imm8
	[Test]
	public void SAL_rmreg8_imm8 ()
	{
		// SAL BH, 0x8
		// SAL (R8.BH, 0x8)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAL (R8.BH, 0x8);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc0, 0xe7, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAL BH, 0x8' failed.");
	}

	// SAL rmreg16,CL
	[Test]
	public void SAL_rmreg16_CL ()
	{
		// SAL AX, CL
		// SAL__CL (R16.AX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAL__CL (R16.AX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xd3, 0xe0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAL AX, CL' failed.");
	}

	// SAL rmreg16,imm8
	[Test]
	public void SAL_rmreg16_imm8 ()
	{
		// SAL DX, 0x2
		// SAL (R16.DX, 0x2)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAL (R16.DX, 0x2);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xc1, 0xe2, 0x2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAL DX, 0x2' failed.");
	}

	// SAL rmreg32,CL
	[Test]
	public void SAL_rmreg32_CL ()
	{
		// SAL ESP, CL
		// SAL__CL (R32.ESP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAL__CL (R32.ESP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd3, 0xe4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAL ESP, CL' failed.");
	}

	// SAL rmreg32,imm8
	[Test]
	public void SAL_rmreg32_imm8 ()
	{
		// SAL EAX, 0xe
		// SAL (R32.EAX, 0xe)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAL (R32.EAX, 0xe);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc1, 0xe0, 0xe };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAL EAX, 0xe' failed.");
	}

	// SALC 
	[Test]
	public void SALC ()
	{
		// SALC
		// SALC ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SALC ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SALC' failed.");
	}

	// SAR mem8,CL
	[Test]
	public void SAR_mem8_CL ()
	{
		// SAR Byte [DS:EBX + ECX*8], CL
		// SAR__CL (new ByteMemory(Seg.DS, R32.EBX, R32.ECX, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAR__CL (new ByteMemory (Seg.DS, R32.EBX, R32.ECX, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xd2, 0x3c, 0xcb };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAR Byte [DS:EBX + ECX*8], CL' failed.");
	}

	// SAR mem8,imm8
	[Test]
	public void SAR_mem8_imm8 ()
	{
		// SAR Byte [SS:EAX*2], 0x4
		// SAR (new ByteMemory(Seg.SS, null, R32.EAX, 1), 0x4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAR (new ByteMemory (Seg.SS, null, R32.EAX, 1), 0x4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0xc0, 0x3c, 0x0, 0x4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAR Byte [SS:EAX*2], 0x4' failed.");
	}

	// SAR mem16,CL
	[Test]
	public void SAR_mem16_CL ()
	{
		// SAR Word [0x12345678], CL
		// SAR__CL (new WordMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAR__CL (new WordMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xd3, 0x3d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAR Word [0x12345678], CL' failed.");
	}

	// SAR mem16,imm8
	[Test]
	public void SAR_mem16_imm8 ()
	{
		// SAR Word [GS:EDX + EBX*2], 0xf
		// SAR (new WordMemory(Seg.GS, R32.EDX, R32.EBX, 1), 0xf)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAR (new WordMemory (Seg.GS, R32.EDX, R32.EBX, 1), 0xf);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0x66, 0xc1, 0x3c, 0x5a, 0xf };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAR Word [GS:EDX + EBX*2], 0xf' failed.");
	}

	// SAR mem32,CL
	[Test]
	public void SAR_mem32_CL ()
	{
		// SAR DWord [ESI*1], CL
		// SAR__CL (new DWordMemory(null, null, R32.ESI, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAR__CL (new DWordMemory (null, null, R32.ESI, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd3, 0x3e };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAR DWord [ESI*1], CL' failed.");
	}

	// SAR mem32,imm8
	[Test]
	public void SAR_mem32_imm8 ()
	{
		// SAR DWord [EAX*2 + 0x12345678], 0x8
		// SAR (new DWordMemory(null, null, R32.EAX, 1, 0x12345678), 0x8)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAR (new DWordMemory (null, null, R32.EAX, 1, 0x12345678), 0x8);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc1, 0xbc, 0x0, 0x78, 0x56, 0x34, 0x12, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAR DWord [EAX*2 + 0x12345678], 0x8' failed.");
	}

	// SAR rmreg8,CL
	[Test]
	public void SAR_rmreg8_CL ()
	{
		// SAR DL, CL
		// SAR__CL (R8.DL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAR__CL (R8.DL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd2, 0xfa };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAR DL, CL' failed.");
	}

	// SAR rmreg8,imm8
	[Test]
	public void SAR_rmreg8_imm8 ()
	{
		// SAR BH, 0xd
		// SAR (R8.BH, 0xd)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAR (R8.BH, 0xd);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc0, 0xff, 0xd };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAR BH, 0xd' failed.");
	}

	// SAR rmreg16,CL
	[Test]
	public void SAR_rmreg16_CL ()
	{
		// SAR BX, CL
		// SAR__CL (R16.BX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAR__CL (R16.BX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xd3, 0xfb };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAR BX, CL' failed.");
	}

	// SAR rmreg16,imm8
	[Test]
	public void SAR_rmreg16_imm8 ()
	{
		// SAR SP, 0xa
		// SAR (R16.SP, 0xa)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAR (R16.SP, 0xa);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xc1, 0xfc, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAR SP, 0xa' failed.");
	}

	// SAR rmreg32,CL
	[Test]
	public void SAR_rmreg32_CL ()
	{
		// SAR EBX, CL
		// SAR__CL (R32.EBX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAR__CL (R32.EBX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd3, 0xfb };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAR EBX, CL' failed.");
	}

	// SAR rmreg32,imm8
	[Test]
	public void SAR_rmreg32_imm8 ()
	{
		// SAR EAX, 0x6
		// SAR (R32.EAX, 0x6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SAR (R32.EAX, 0x6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc1, 0xf8, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SAR EAX, 0x6' failed.");
	}

	// SBB mem8,reg8
	[Test]
	public void SBB_mem8_reg8 ()
	{
		// SBB [0x12345678], AH
		// SBB (new ByteMemory(null, null, null, 0, 0x12345678), R8.AH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (new ByteMemory (null, null, null, 0, 0x12345678), R8.AH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x18, 0x25, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB [0x12345678], AH' failed.");
	}

	// SBB mem16,reg16
	[Test]
	public void SBB_mem16_reg16 ()
	{
		// SBB [ESI*1 + 0x12345678], SP
		// SBB (new WordMemory(null, null, R32.ESI, 0, 0x12345678), R16.SP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (new WordMemory (null, null, R32.ESI, 0, 0x12345678), R16.SP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x19, 0xa6, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB [ESI*1 + 0x12345678], SP' failed.");
	}

	// SBB mem32,reg32
	[Test]
	public void SBB_mem32_reg32 ()
	{
		// SBB [ESI + EBX*8 + 0x12345678], EBP
		// SBB (new DWordMemory(null, R32.ESI, R32.EBX, 3, 0x12345678), R32.EBP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (new DWordMemory (null, R32.ESI, R32.EBX, 3, 0x12345678), R32.EBP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x19, 0xac, 0xde, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB [ESI + EBX*8 + 0x12345678], EBP' failed.");
	}

	// SBB reg8,mem8
	[Test]
	public void SBB_reg8_mem8 ()
	{
		// SBB BH, [EDX + EDI*8 + 0x12345678]
		// SBB (R8.BH, new ByteMemory(null, R32.EDX, R32.EDI, 3, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (R8.BH, new ByteMemory (null, R32.EDX, R32.EDI, 3, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x1a, 0xbc, 0xfa, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB BH, [EDX + EDI*8 + 0x12345678]' failed.");
	}

	// SBB reg16,mem16
	[Test]
	public void SBB_reg16_mem16 ()
	{
		// SBB BX, [ES:0x12345678]
		// SBB (R16.BX, new WordMemory(Seg.ES, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (R16.BX, new WordMemory (Seg.ES, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0x66, 0x1b, 0x1d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB BX, [ES:0x12345678]' failed.");
	}

	// SBB reg32,mem32
	[Test]
	public void SBB_reg32_mem32 ()
	{
		// SBB ESP, [DS:0x12345678]
		// SBB (R32.ESP, new DWordMemory(Seg.DS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (R32.ESP, new DWordMemory (Seg.DS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x1b, 0x25, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB ESP, [DS:0x12345678]' failed.");
	}

	// SBB mem8,imm8
	[Test]
	public void SBB_mem8_imm8 ()
	{
		// SBB Byte [0x12345678], 0xb
		// SBB (new ByteMemory(null, null, null, 0, 0x12345678), 0xb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (new ByteMemory (null, null, null, 0, 0x12345678), 0xb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x80, 0x1d, 0x78, 0x56, 0x34, 0x12, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB Byte [0x12345678], 0xb' failed.");
	}

	// SBB mem16,imm16
	[Test]
	public void SBB_mem16_imm16 ()
	{
		// SBB Word [ESP + EBX*2], 0xc71
		// SBB (new WordMemory(null, R32.ESP, R32.EBX, 1), 0xc71)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (new WordMemory (null, R32.ESP, R32.EBX, 1), 0xc71);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x81, 0x1c, 0x5c, 0x71, 0xc };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB Word [ESP + EBX*2], 0xc71' failed.");
	}

	// SBB mem32,imm32
	[Test]
	public void SBB_mem32_imm32 ()
	{
		// SBB DWord [0x12345678], 0x7072650
		// SBB (new DWordMemory(null, null, null, 0, 0x12345678), 0x7072650)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (new DWordMemory (null, null, null, 0, 0x12345678), 0x7072650);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x81, 0x1d, 0x78, 0x56, 0x34, 0x12, 0x50, 0x26, 0x7, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB DWord [0x12345678], 0x7072650' failed.");
	}

	// SBB mem16,imm8
	[Test]
	public void SBB_mem16_imm8 ()
	{
		// SBB Word [FS:EDX*4], 0xa
		// SBB (new WordMemory(Seg.FS, null, R32.EDX, 2), 0xa)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (new WordMemory (Seg.FS, null, R32.EDX, 2), 0xa);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0x66, 0x83, 0x1c, 0x95, 0x0, 0x0, 0x0, 0x0, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB Word [FS:EDX*4], 0xa' failed.");
	}

	// SBB mem32,imm8
	[Test]
	public void SBB_mem32_imm8 ()
	{
		// SBB DWord [EDX + EAX*4 + 0x12345678], 0xe
		// SBB (new DWordMemory(null, R32.EDX, R32.EAX, 2, 0x12345678), 0xe)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (new DWordMemory (null, R32.EDX, R32.EAX, 2, 0x12345678), 0xe);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x83, 0x9c, 0x82, 0x78, 0x56, 0x34, 0x12, 0xe };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB DWord [EDX + EAX*4 + 0x12345678], 0xe' failed.");
	}

	// SBB rmreg8,reg8
	[Test]
	public void SBB_rmreg8_reg8 ()
	{
		// SBB BH, BH
		// SBB (R8.BH, R8.BH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (R8.BH, R8.BH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x18, 0xff };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB BH, BH' failed.");
	}

	// SBB rmreg16,reg16
	[Test]
	public void SBB_rmreg16_reg16 ()
	{
		// SBB DX, BX
		// SBB (R16.DX, R16.BX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (R16.DX, R16.BX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x19, 0xda };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB DX, BX' failed.");
	}

	// SBB rmreg32,reg32
	[Test]
	public void SBB_rmreg32_reg32 ()
	{
		// SBB EDI, EAX
		// SBB (R32.EDI, R32.EAX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (R32.EDI, R32.EAX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x19, 0xc7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB EDI, EAX' failed.");
	}

	// SBB rmreg8,imm8
	[Test]
	public void SBB_rmreg8_imm8 ()
	{
		// SBB DH, 0x0
		// SBB (R8.DH, 0x0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (R8.DH, 0x0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x80, 0xde, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB DH, 0x0' failed.");
	}

	// SBB rmreg16,imm16
	[Test]
	public void SBB_rmreg16_imm16 ()
	{
		// SBB CX, 0x219
		// SBB (R16.CX, 0x219)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (R16.CX, 0x219);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x81, 0xd9, 0x19, 0x2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB CX, 0x219' failed.");
	}

	// SBB rmreg32,imm32
	[Test]
	public void SBB_rmreg32_imm32 ()
	{
		// SBB EBX, 0xbf38241
		// SBB (R32.EBX, 0xbf38241)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (R32.EBX, 0xbf38241);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x81, 0xdb, 0x41, 0x82, 0xf3, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB EBX, 0xbf38241' failed.");
	}

	// SBB rmreg16,imm8
	[Test]
	public void SBB_rmreg16_imm8 ()
	{
		// SBB CX, 0x6
		// SBB (R16.CX, 0x6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (R16.CX, 0x6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x83, 0xd9, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB CX, 0x6' failed.");
	}

	// SBB rmreg32,imm8
	[Test]
	public void SBB_rmreg32_imm8 ()
	{
		// SBB EAX, 0xd
		// SBB (R32.EAX, 0xd)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SBB (R32.EAX, 0xd);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x83, 0xd8, 0xd };
		Assert.IsTrue (CompareData (memoryStream, target), "'SBB EAX, 0xd' failed.");
	}

	// SCASB 
	[Test]
	public void SCASB ()
	{
		// SCASB
		// SCASB ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SCASB ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xae };
		Assert.IsTrue (CompareData (memoryStream, target), "'SCASB' failed.");
	}

	// SCASD 
	[Test]
	public void SCASD ()
	{
		// SCASD
		// SCASD ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SCASD ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xaf };
		Assert.IsTrue (CompareData (memoryStream, target), "'SCASD' failed.");
	}

	// SCASW 
	[Test]
	public void SCASW ()
	{
		// SCASW
		// SCASW ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SCASW ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xaf };
		Assert.IsTrue (CompareData (memoryStream, target), "'SCASW' failed.");
	}

	// SETA mem8
	[Test]
	public void SETA_mem8 ()
	{
		// SETA Byte [CS:EDI*4 + 0x12345678]
		// SETA (new ByteMemory(Seg.CS, null, R32.EDI, 2, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETA (new ByteMemory (Seg.CS, null, R32.EDI, 2, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xf, 0x97, 0x4, 0xbd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETA Byte [CS:EDI*4 + 0x12345678]' failed.");
	}

	// SETA rmreg8
	[Test]
	public void SETA_rmreg8 ()
	{
		// SETA CL
		// SETA (R8.CL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETA (R8.CL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x97, 0xc1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETA CL' failed.");
	}

	// SETAE mem8
	[Test]
	public void SETAE_mem8 ()
	{
		// SETAE Byte [DS:ECX + EAX*4]
		// SETAE (new ByteMemory(Seg.DS, R32.ECX, R32.EAX, 2))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETAE (new ByteMemory (Seg.DS, R32.ECX, R32.EAX, 2));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xf, 0x93, 0x4, 0x81 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETAE Byte [DS:ECX + EAX*4]' failed.");
	}

	// SETAE rmreg8
	[Test]
	public void SETAE_rmreg8 ()
	{
		// SETAE BL
		// SETAE (R8.BL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETAE (R8.BL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x93, 0xc3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETAE BL' failed.");
	}

	// SETB mem8
	[Test]
	public void SETB_mem8 ()
	{
		// SETB Byte [EBX + 0x12345678]
		// SETB (new ByteMemory(null, R32.EBX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETB (new ByteMemory (null, R32.EBX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x92, 0x83, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETB Byte [EBX + 0x12345678]' failed.");
	}

	// SETB rmreg8
	[Test]
	public void SETB_rmreg8 ()
	{
		// SETB DL
		// SETB (R8.DL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETB (R8.DL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x92, 0xc2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETB DL' failed.");
	}

	// SETBE mem8
	[Test]
	public void SETBE_mem8 ()
	{
		// SETBE Byte [DS:ESI*2 + 0x12345678]
		// SETBE (new ByteMemory(Seg.DS, null, R32.ESI, 1, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETBE (new ByteMemory (Seg.DS, null, R32.ESI, 1, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xf, 0x96, 0x84, 0x36, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETBE Byte [DS:ESI*2 + 0x12345678]' failed.");
	}

	// SETBE rmreg8
	[Test]
	public void SETBE_rmreg8 ()
	{
		// SETBE BL
		// SETBE (R8.BL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETBE (R8.BL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x96, 0xc3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETBE BL' failed.");
	}

	// SETC mem8
	[Test]
	public void SETC_mem8 ()
	{
		// SETC Byte [DS:ESP + 0x12345678]
		// SETC (new ByteMemory(Seg.DS, R32.ESP, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETC (new ByteMemory (Seg.DS, R32.ESP, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xf, 0x92, 0x84, 0x24, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETC Byte [DS:ESP + 0x12345678]' failed.");
	}

	// SETC rmreg8
	[Test]
	public void SETC_rmreg8 ()
	{
		// SETC AH
		// SETC (R8.AH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETC (R8.AH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x92, 0xc4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETC AH' failed.");
	}

	// SETE mem8
	[Test]
	public void SETE_mem8 ()
	{
		// SETE Byte [EDX + EDX*4 + 0x12345678]
		// SETE (new ByteMemory(null, R32.EDX, R32.EDX, 2, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETE (new ByteMemory (null, R32.EDX, R32.EDX, 2, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x94, 0x84, 0x92, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETE Byte [EDX + EDX*4 + 0x12345678]' failed.");
	}

	// SETE rmreg8
	[Test]
	public void SETE_rmreg8 ()
	{
		// SETE DL
		// SETE (R8.DL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETE (R8.DL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x94, 0xc2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETE DL' failed.");
	}

	// SETG mem8
	[Test]
	public void SETG_mem8 ()
	{
		// SETG Byte [GS:EBP]
		// SETG (new ByteMemory(Seg.GS, R32.EBP, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETG (new ByteMemory (Seg.GS, R32.EBP, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xf, 0x9f, 0x45, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETG Byte [GS:EBP]' failed.");
	}

	// SETG rmreg8
	[Test]
	public void SETG_rmreg8 ()
	{
		// SETG CL
		// SETG (R8.CL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETG (R8.CL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9f, 0xc1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETG CL' failed.");
	}

	// SETGE mem8
	[Test]
	public void SETGE_mem8 ()
	{
		// SETGE Byte [EBX + ECX*1]
		// SETGE (new ByteMemory(null, R32.EBX, R32.ECX, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETGE (new ByteMemory (null, R32.EBX, R32.ECX, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9d, 0x4, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETGE Byte [EBX + ECX*1]' failed.");
	}

	// SETGE rmreg8
	[Test]
	public void SETGE_rmreg8 ()
	{
		// SETGE CL
		// SETGE (R8.CL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETGE (R8.CL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9d, 0xc1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETGE CL' failed.");
	}

	// SETL mem8
	[Test]
	public void SETL_mem8 ()
	{
		// SETL Byte [EDX + 0x12345678]
		// SETL (new ByteMemory(null, R32.EDX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETL (new ByteMemory (null, R32.EDX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9c, 0x82, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETL Byte [EDX + 0x12345678]' failed.");
	}

	// SETL rmreg8
	[Test]
	public void SETL_rmreg8 ()
	{
		// SETL CH
		// SETL (R8.CH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETL (R8.CH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9c, 0xc5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETL CH' failed.");
	}

	// SETLE mem8
	[Test]
	public void SETLE_mem8 ()
	{
		// SETLE Byte [ES:EBP*2]
		// SETLE (new ByteMemory(Seg.ES, null, R32.EBP, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETLE (new ByteMemory (Seg.ES, null, R32.EBP, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xf, 0x9e, 0x44, 0x2d, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETLE Byte [ES:EBP*2]' failed.");
	}

	// SETLE rmreg8
	[Test]
	public void SETLE_rmreg8 ()
	{
		// SETLE CL
		// SETLE (R8.CL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETLE (R8.CL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9e, 0xc1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETLE CL' failed.");
	}

	// SETNA mem8
	[Test]
	public void SETNA_mem8 ()
	{
		// SETNA Byte [FS:ESP + ECX*1 + 0x12345678]
		// SETNA (new ByteMemory(Seg.FS, R32.ESP, R32.ECX, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNA (new ByteMemory (Seg.FS, R32.ESP, R32.ECX, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xf, 0x96, 0x84, 0xc, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNA Byte [FS:ESP + ECX*1 + 0x12345678]' failed.");
	}

	// SETNA rmreg8
	[Test]
	public void SETNA_rmreg8 ()
	{
		// SETNA BL
		// SETNA (R8.BL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNA (R8.BL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x96, 0xc3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNA BL' failed.");
	}

	// SETNAE mem8
	[Test]
	public void SETNAE_mem8 ()
	{
		// SETNAE Byte [GS:EDX*8]
		// SETNAE (new ByteMemory(Seg.GS, null, R32.EDX, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNAE (new ByteMemory (Seg.GS, null, R32.EDX, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xf, 0x92, 0x4, 0xd5, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNAE Byte [GS:EDX*8]' failed.");
	}

	// SETNAE rmreg8
	[Test]
	public void SETNAE_rmreg8 ()
	{
		// SETNAE CH
		// SETNAE (R8.CH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNAE (R8.CH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x92, 0xc5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNAE CH' failed.");
	}

	// SETNB mem8
	[Test]
	public void SETNB_mem8 ()
	{
		// SETNB Byte [0x12345678]
		// SETNB (new ByteMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNB (new ByteMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x93, 0x5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNB Byte [0x12345678]' failed.");
	}

	// SETNB rmreg8
	[Test]
	public void SETNB_rmreg8 ()
	{
		// SETNB DH
		// SETNB (R8.DH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNB (R8.DH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x93, 0xc6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNB DH' failed.");
	}

	// SETNBE mem8
	[Test]
	public void SETNBE_mem8 ()
	{
		// SETNBE Byte [SS:EDI*1 + 0x12345678]
		// SETNBE (new ByteMemory(Seg.SS, null, R32.EDI, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNBE (new ByteMemory (Seg.SS, null, R32.EDI, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0xf, 0x97, 0x87, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNBE Byte [SS:EDI*1 + 0x12345678]' failed.");
	}

	// SETNBE rmreg8
	[Test]
	public void SETNBE_rmreg8 ()
	{
		// SETNBE AL
		// SETNBE (R8.AL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNBE (R8.AL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x97, 0xc0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNBE AL' failed.");
	}

	// SETNC mem8
	[Test]
	public void SETNC_mem8 ()
	{
		// SETNC Byte [EBP + EDI*2]
		// SETNC (new ByteMemory(null, R32.EBP, R32.EDI, 1))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNC (new ByteMemory (null, R32.EBP, R32.EDI, 1));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x93, 0x44, 0x7d, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNC Byte [EBP + EDI*2]' failed.");
	}

	// SETNC rmreg8
	[Test]
	public void SETNC_rmreg8 ()
	{
		// SETNC CH
		// SETNC (R8.CH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNC (R8.CH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x93, 0xc5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNC CH' failed.");
	}

	// SETNE mem8
	[Test]
	public void SETNE_mem8 ()
	{
		// SETNE Byte [DS:0x12345678]
		// SETNE (new ByteMemory(Seg.DS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNE (new ByteMemory (Seg.DS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xf, 0x95, 0x5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNE Byte [DS:0x12345678]' failed.");
	}

	// SETNE rmreg8
	[Test]
	public void SETNE_rmreg8 ()
	{
		// SETNE DH
		// SETNE (R8.DH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNE (R8.DH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x95, 0xc6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNE DH' failed.");
	}

	// SETNG mem8
	[Test]
	public void SETNG_mem8 ()
	{
		// SETNG Byte [FS:ESI]
		// SETNG (new ByteMemory(Seg.FS, R32.ESI, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNG (new ByteMemory (Seg.FS, R32.ESI, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xf, 0x9e, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNG Byte [FS:ESI]' failed.");
	}

	// SETNG rmreg8
	[Test]
	public void SETNG_rmreg8 ()
	{
		// SETNG CH
		// SETNG (R8.CH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNG (R8.CH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9e, 0xc5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNG CH' failed.");
	}

	// SETNGE mem8
	[Test]
	public void SETNGE_mem8 ()
	{
		// SETNGE Byte [ECX*2 + 0x12345678]
		// SETNGE (new ByteMemory(null, null, R32.ECX, 1, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNGE (new ByteMemory (null, null, R32.ECX, 1, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9c, 0x84, 0x9, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNGE Byte [ECX*2 + 0x12345678]' failed.");
	}

	// SETNGE rmreg8
	[Test]
	public void SETNGE_rmreg8 ()
	{
		// SETNGE CL
		// SETNGE (R8.CL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNGE (R8.CL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9c, 0xc1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNGE CL' failed.");
	}

	// SETNL mem8
	[Test]
	public void SETNL_mem8 ()
	{
		// SETNL Byte [ESI*4]
		// SETNL (new ByteMemory(null, null, R32.ESI, 2))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNL (new ByteMemory (null, null, R32.ESI, 2));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9d, 0x4, 0xb5, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNL Byte [ESI*4]' failed.");
	}

	// SETNL rmreg8
	[Test]
	public void SETNL_rmreg8 ()
	{
		// SETNL BH
		// SETNL (R8.BH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNL (R8.BH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9d, 0xc7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNL BH' failed.");
	}

	// SETNLE mem8
	[Test]
	public void SETNLE_mem8 ()
	{
		// SETNLE Byte [SS:EBP]
		// SETNLE (new ByteMemory(Seg.SS, R32.EBP, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNLE (new ByteMemory (Seg.SS, R32.EBP, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0xf, 0x9f, 0x45, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNLE Byte [SS:EBP]' failed.");
	}

	// SETNLE rmreg8
	[Test]
	public void SETNLE_rmreg8 ()
	{
		// SETNLE CH
		// SETNLE (R8.CH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNLE (R8.CH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9f, 0xc5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNLE CH' failed.");
	}

	// SETNO mem8
	[Test]
	public void SETNO_mem8 ()
	{
		// SETNO Byte [ESI*8 + 0x12345678]
		// SETNO (new ByteMemory(null, null, R32.ESI, 3, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNO (new ByteMemory (null, null, R32.ESI, 3, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x91, 0x4, 0xf5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNO Byte [ESI*8 + 0x12345678]' failed.");
	}

	// SETNO rmreg8
	[Test]
	public void SETNO_rmreg8 ()
	{
		// SETNO AH
		// SETNO (R8.AH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNO (R8.AH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x91, 0xc4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNO AH' failed.");
	}

	// SETNP mem8
	[Test]
	public void SETNP_mem8 ()
	{
		// SETNP Byte [ES:EBP*1 + 0x12345678]
		// SETNP (new ByteMemory(Seg.ES, null, R32.EBP, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNP (new ByteMemory (Seg.ES, null, R32.EBP, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xf, 0x9b, 0x85, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNP Byte [ES:EBP*1 + 0x12345678]' failed.");
	}

	// SETNP rmreg8
	[Test]
	public void SETNP_rmreg8 ()
	{
		// SETNP BH
		// SETNP (R8.BH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNP (R8.BH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9b, 0xc7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNP BH' failed.");
	}

	// SETNS mem8
	[Test]
	public void SETNS_mem8 ()
	{
		// SETNS Byte [ECX + 0x12345678]
		// SETNS (new ByteMemory(null, R32.ECX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNS (new ByteMemory (null, R32.ECX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x99, 0x81, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNS Byte [ECX + 0x12345678]' failed.");
	}

	// SETNS rmreg8
	[Test]
	public void SETNS_rmreg8 ()
	{
		// SETNS CH
		// SETNS (R8.CH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNS (R8.CH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x99, 0xc5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNS CH' failed.");
	}

	// SETNZ mem8
	[Test]
	public void SETNZ_mem8 ()
	{
		// SETNZ Byte [ESI + 0x12345678]
		// SETNZ (new ByteMemory(null, R32.ESI, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNZ (new ByteMemory (null, R32.ESI, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x95, 0x86, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNZ Byte [ESI + 0x12345678]' failed.");
	}

	// SETNZ rmreg8
	[Test]
	public void SETNZ_rmreg8 ()
	{
		// SETNZ DH
		// SETNZ (R8.DH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETNZ (R8.DH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x95, 0xc6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETNZ DH' failed.");
	}

	// SETO mem8
	[Test]
	public void SETO_mem8 ()
	{
		// SETO Byte [EAX*4 + 0x12345678]
		// SETO (new ByteMemory(null, null, R32.EAX, 2, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETO (new ByteMemory (null, null, R32.EAX, 2, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x90, 0x4, 0x85, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETO Byte [EAX*4 + 0x12345678]' failed.");
	}

	// SETO rmreg8
	[Test]
	public void SETO_rmreg8 ()
	{
		// SETO CH
		// SETO (R8.CH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETO (R8.CH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x90, 0xc5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETO CH' failed.");
	}

	// SETP mem8
	[Test]
	public void SETP_mem8 ()
	{
		// SETP Byte [CS:ESI*8]
		// SETP (new ByteMemory(Seg.CS, null, R32.ESI, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETP (new ByteMemory (Seg.CS, null, R32.ESI, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xf, 0x9a, 0x4, 0xf5, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETP Byte [CS:ESI*8]' failed.");
	}

	// SETP rmreg8
	[Test]
	public void SETP_rmreg8 ()
	{
		// SETP CH
		// SETP (R8.CH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETP (R8.CH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9a, 0xc5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETP CH' failed.");
	}

	// SETPE mem8
	[Test]
	public void SETPE_mem8 ()
	{
		// SETPE Byte [EBP + ECX*1]
		// SETPE (new ByteMemory(null, R32.EBP, R32.ECX, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETPE (new ByteMemory (null, R32.EBP, R32.ECX, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9a, 0x44, 0xd, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETPE Byte [EBP + ECX*1]' failed.");
	}

	// SETPE rmreg8
	[Test]
	public void SETPE_rmreg8 ()
	{
		// SETPE AH
		// SETPE (R8.AH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETPE (R8.AH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9a, 0xc4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETPE AH' failed.");
	}

	// SETPO mem8
	[Test]
	public void SETPO_mem8 ()
	{
		// SETPO Byte [EAX + EDI*1 + 0x12345678]
		// SETPO (new ByteMemory(null, R32.EAX, R32.EDI, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETPO (new ByteMemory (null, R32.EAX, R32.EDI, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9b, 0x84, 0x38, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETPO Byte [EAX + EDI*1 + 0x12345678]' failed.");
	}

	// SETPO rmreg8
	[Test]
	public void SETPO_rmreg8 ()
	{
		// SETPO CH
		// SETPO (R8.CH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETPO (R8.CH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9b, 0xc5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETPO CH' failed.");
	}

	// SETS mem8
	[Test]
	public void SETS_mem8 ()
	{
		// SETS Byte [EBP*8]
		// SETS (new ByteMemory(null, null, R32.EBP, 3))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETS (new ByteMemory (null, null, R32.EBP, 3));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x98, 0x4, 0xed, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETS Byte [EBP*8]' failed.");
	}

	// SETS rmreg8
	[Test]
	public void SETS_rmreg8 ()
	{
		// SETS BH
		// SETS (R8.BH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETS (R8.BH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x98, 0xc7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETS BH' failed.");
	}

	// SETZ mem8
	[Test]
	public void SETZ_mem8 ()
	{
		// SETZ Byte [0x12345678]
		// SETZ (new ByteMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETZ (new ByteMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x94, 0x5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETZ Byte [0x12345678]' failed.");
	}

	// SETZ rmreg8
	[Test]
	public void SETZ_rmreg8 ()
	{
		// SETZ CH
		// SETZ (R8.CH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SETZ (R8.CH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x94, 0xc5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SETZ CH' failed.");
	}

	// SFENCE 
	[Test]
	public void SFENCE ()
	{
		// SFENCE
		// SFENCE ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SFENCE ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xae, 0xf8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SFENCE' failed.");
	}

	// SGDT mem
	[Test]
	public void SGDT_mem ()
	{
		// SGDT [EAX]
		// SGDT (new DWordMemory(null, R32.EAX, null, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SGDT (new DWordMemory (null, R32.EAX, null, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x1, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SGDT [EAX]' failed.");
	}

	// SHL mem8,CL
	[Test]
	public void SHL_mem8_CL ()
	{
		// SHL Byte [CS:EDI + 0x12345678], CL
		// SHL__CL (new ByteMemory(Seg.CS, R32.EDI, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHL__CL (new ByteMemory (Seg.CS, R32.EDI, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0xd2, 0xa7, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHL Byte [CS:EDI + 0x12345678], CL' failed.");
	}

	// SHL mem8,imm8
	[Test]
	public void SHL_mem8_imm8 ()
	{
		// SHL Byte [EBP*4 + 0x12345678], 0x2
		// SHL (new ByteMemory(null, null, R32.EBP, 2, 0x12345678), 0x2)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHL (new ByteMemory (null, null, R32.EBP, 2, 0x12345678), 0x2);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc0, 0x24, 0xad, 0x78, 0x56, 0x34, 0x12, 0x2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHL Byte [EBP*4 + 0x12345678], 0x2' failed.");
	}

	// SHL mem16,CL
	[Test]
	public void SHL_mem16_CL ()
	{
		// SHL Word [GS:EAX + ECX*1], CL
		// SHL__CL (new WordMemory(Seg.GS, R32.EAX, R32.ECX, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHL__CL (new WordMemory (Seg.GS, R32.EAX, R32.ECX, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0x66, 0xd3, 0x24, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHL Word [GS:EAX + ECX*1], CL' failed.");
	}

	// SHL mem16,imm8
	[Test]
	public void SHL_mem16_imm8 ()
	{
		// SHL Word [0x12345678], 0x3
		// SHL (new WordMemory(null, null, null, 0, 0x12345678), 0x3)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHL (new WordMemory (null, null, null, 0, 0x12345678), 0x3);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xc1, 0x25, 0x78, 0x56, 0x34, 0x12, 0x3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHL Word [0x12345678], 0x3' failed.");
	}

	// SHL mem32,CL
	[Test]
	public void SHL_mem32_CL ()
	{
		// SHL DWord [SS:EBP + EDX*4], CL
		// SHL__CL (new DWordMemory(Seg.SS, R32.EBP, R32.EDX, 2))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHL__CL (new DWordMemory (Seg.SS, R32.EBP, R32.EDX, 2));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0xd3, 0x64, 0x95, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHL DWord [SS:EBP + EDX*4], CL' failed.");
	}

	// SHL mem32,imm8
	[Test]
	public void SHL_mem32_imm8 ()
	{
		// SHL DWord [FS:EDI], 0x6
		// SHL (new DWordMemory(Seg.FS, R32.EDI, null, 0), 0x6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHL (new DWordMemory (Seg.FS, R32.EDI, null, 0), 0x6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xc1, 0x27, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHL DWord [FS:EDI], 0x6' failed.");
	}

	// SHL rmreg8,CL
	[Test]
	public void SHL_rmreg8_CL ()
	{
		// SHL CL, CL
		// SHL__CL (R8.CL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHL__CL (R8.CL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd2, 0xe1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHL CL, CL' failed.");
	}

	// SHL rmreg8,imm8
	[Test]
	public void SHL_rmreg8_imm8 ()
	{
		// SHL BH, 0xb
		// SHL (R8.BH, 0xb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHL (R8.BH, 0xb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc0, 0xe7, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHL BH, 0xb' failed.");
	}

	// SHL rmreg16,CL
	[Test]
	public void SHL_rmreg16_CL ()
	{
		// SHL DI, CL
		// SHL__CL (R16.DI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHL__CL (R16.DI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xd3, 0xe7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHL DI, CL' failed.");
	}

	// SHL rmreg16,imm8
	[Test]
	public void SHL_rmreg16_imm8 ()
	{
		// SHL BX, 0xe
		// SHL (R16.BX, 0xe)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHL (R16.BX, 0xe);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xc1, 0xe3, 0xe };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHL BX, 0xe' failed.");
	}

	// SHL rmreg32,CL
	[Test]
	public void SHL_rmreg32_CL ()
	{
		// SHL ESI, CL
		// SHL__CL (R32.ESI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHL__CL (R32.ESI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd3, 0xe6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHL ESI, CL' failed.");
	}

	// SHL rmreg32,imm8
	[Test]
	public void SHL_rmreg32_imm8 ()
	{
		// SHL ESI, 0xb
		// SHL (R32.ESI, 0xb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHL (R32.ESI, 0xb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc1, 0xe6, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHL ESI, 0xb' failed.");
	}

	// SHLD mem16,reg16,imm8
	[Test]
	public void SHLD_mem16_reg16_imm8 ()
	{
		// SHLD [ECX + EDI*4 + 0x12345678], DX, 0x4
		// SHLD (new WordMemory(null, R32.ECX, R32.EDI, 2, 0x12345678), R16.DX, 0x4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHLD (new WordMemory (null, R32.ECX, R32.EDI, 2, 0x12345678), R16.DX, 0x4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xa4, 0x94, 0xb9, 0x78, 0x56, 0x34, 0x12, 0x4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHLD [ECX + EDI*4 + 0x12345678], DX, 0x4' failed.");
	}

	// SHLD mem32,reg32,imm8
	[Test]
	public void SHLD_mem32_reg32_imm8 ()
	{
		// SHLD [0x12345678], EDI, 0x8
		// SHLD (new DWordMemory(null, null, null, 0, 0x12345678), R32.EDI, 0x8)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHLD (new DWordMemory (null, null, null, 0, 0x12345678), R32.EDI, 0x8);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xa4, 0x3d, 0x78, 0x56, 0x34, 0x12, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHLD [0x12345678], EDI, 0x8' failed.");
	}

	// SHLD mem16,reg16,CL
	[Test]
	public void SHLD_mem16_reg16_CL ()
	{
		// SHLD [EBX + EDI*4 + 0x12345678], SP, CL
		// SHLD___CL (new WordMemory(null, R32.EBX, R32.EDI, 2, 0x12345678), R16.SP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHLD___CL (new WordMemory (null, R32.EBX, R32.EDI, 2, 0x12345678), R16.SP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xa5, 0xa4, 0xbb, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHLD [EBX + EDI*4 + 0x12345678], SP, CL' failed.");
	}

	// SHLD mem32,reg32,CL
	[Test]
	public void SHLD_mem32_reg32_CL ()
	{
		// SHLD [FS:0x12345678], EDI, CL
		// SHLD___CL (new DWordMemory(Seg.FS, null, null, 0, 0x12345678), R32.EDI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHLD___CL (new DWordMemory (Seg.FS, null, null, 0, 0x12345678), R32.EDI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xf, 0xa5, 0x3d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHLD [FS:0x12345678], EDI, CL' failed.");
	}

	// SHLD rmreg16,reg16,imm8
	[Test]
	public void SHLD_rmreg16_reg16_imm8 ()
	{
		// SHLD BX, BP, 0xa
		// SHLD (R16.BX, R16.BP, 0xa)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHLD (R16.BX, R16.BP, 0xa);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xa4, 0xeb, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHLD BX, BP, 0xa' failed.");
	}

	// SHLD rmreg32,reg32,imm8
	[Test]
	public void SHLD_rmreg32_reg32_imm8 ()
	{
		// SHLD ESP, EBP, 0xb
		// SHLD (R32.ESP, R32.EBP, 0xb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHLD (R32.ESP, R32.EBP, 0xb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xa4, 0xec, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHLD ESP, EBP, 0xb' failed.");
	}

	// SHLD rmreg16,reg16,CL
	[Test]
	public void SHLD_rmreg16_reg16_CL ()
	{
		// SHLD SP, CX, CL
		// SHLD___CL (R16.SP, R16.CX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHLD___CL (R16.SP, R16.CX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xa5, 0xcc };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHLD SP, CX, CL' failed.");
	}

	// SHLD rmreg32,reg32,CL
	[Test]
	public void SHLD_rmreg32_reg32_CL ()
	{
		// SHLD EDI, EDI, CL
		// SHLD___CL (R32.EDI, R32.EDI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHLD___CL (R32.EDI, R32.EDI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xa5, 0xff };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHLD EDI, EDI, CL' failed.");
	}

	// SHR mem8,CL
	[Test]
	public void SHR_mem8_CL ()
	{
		// SHR Byte [EAX*1], CL
		// SHR__CL (new ByteMemory(null, null, R32.EAX, 0))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHR__CL (new ByteMemory (null, null, R32.EAX, 0));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd2, 0x28 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHR Byte [EAX*1], CL' failed.");
	}

	// SHR mem8,imm8
	[Test]
	public void SHR_mem8_imm8 ()
	{
		// SHR Byte [EDI*8 + 0x12345678], 0x8
		// SHR (new ByteMemory(null, null, R32.EDI, 3, 0x12345678), 0x8)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHR (new ByteMemory (null, null, R32.EDI, 3, 0x12345678), 0x8);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc0, 0x2c, 0xfd, 0x78, 0x56, 0x34, 0x12, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHR Byte [EDI*8 + 0x12345678], 0x8' failed.");
	}

	// SHR mem16,CL
	[Test]
	public void SHR_mem16_CL ()
	{
		// SHR Word [ECX + EBP*8 + 0x12345678], CL
		// SHR__CL (new WordMemory(null, R32.ECX, R32.EBP, 3, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHR__CL (new WordMemory (null, R32.ECX, R32.EBP, 3, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xd3, 0xac, 0xe9, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHR Word [ECX + EBP*8 + 0x12345678], CL' failed.");
	}

	// SHR mem16,imm8
	[Test]
	public void SHR_mem16_imm8 ()
	{
		// SHR Word [ESI], 0x0
		// SHR (new WordMemory(null, R32.ESI, null, 0), 0x0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHR (new WordMemory (null, R32.ESI, null, 0), 0x0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xc1, 0x2e, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHR Word [ESI], 0x0' failed.");
	}

	// SHR mem32,CL
	[Test]
	public void SHR_mem32_CL ()
	{
		// SHR DWord [EDI + EDI*2 + 0x12345678], CL
		// SHR__CL (new DWordMemory(null, R32.EDI, R32.EDI, 1, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHR__CL (new DWordMemory (null, R32.EDI, R32.EDI, 1, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd3, 0xac, 0x7f, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHR DWord [EDI + EDI*2 + 0x12345678], CL' failed.");
	}

	// SHR mem32,imm8
	[Test]
	public void SHR_mem32_imm8 ()
	{
		// SHR DWord [EDI + EDX*4 + 0x12345678], 0x2
		// SHR (new DWordMemory(null, R32.EDI, R32.EDX, 2, 0x12345678), 0x2)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHR (new DWordMemory (null, R32.EDI, R32.EDX, 2, 0x12345678), 0x2);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc1, 0xac, 0x97, 0x78, 0x56, 0x34, 0x12, 0x2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHR DWord [EDI + EDX*4 + 0x12345678], 0x2' failed.");
	}

	// SHR rmreg8,CL
	[Test]
	public void SHR_rmreg8_CL ()
	{
		// SHR AH, CL
		// SHR__CL (R8.AH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHR__CL (R8.AH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd2, 0xec };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHR AH, CL' failed.");
	}

	// SHR rmreg8,imm8
	[Test]
	public void SHR_rmreg8_imm8 ()
	{
		// SHR BH, 0x6
		// SHR (R8.BH, 0x6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHR (R8.BH, 0x6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc0, 0xef, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHR BH, 0x6' failed.");
	}

	// SHR rmreg16,CL
	[Test]
	public void SHR_rmreg16_CL ()
	{
		// SHR DX, CL
		// SHR__CL (R16.DX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHR__CL (R16.DX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xd3, 0xea };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHR DX, CL' failed.");
	}

	// SHR rmreg16,imm8
	[Test]
	public void SHR_rmreg16_imm8 ()
	{
		// SHR BX, 0x1
		// SHR (R16.BX, 0x1)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHR (R16.BX, 0x1);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xd1, 0xeb };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHR BX, 0x1' failed.");
	}

	// SHR rmreg32,CL
	[Test]
	public void SHR_rmreg32_CL ()
	{
		// SHR ESI, CL
		// SHR__CL (R32.ESI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHR__CL (R32.ESI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd3, 0xee };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHR ESI, CL' failed.");
	}

	// SHR rmreg32,imm8
	[Test]
	public void SHR_rmreg32_imm8 ()
	{
		// SHR ESP, 0x0
		// SHR (R32.ESP, 0x0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHR (R32.ESP, 0x0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xc1, 0xec, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHR ESP, 0x0' failed.");
	}

	// SHRD mem16,reg16,imm8
	[Test]
	public void SHRD_mem16_reg16_imm8 ()
	{
		// SHRD [ESI*4], BP, 0xb
		// SHRD (new WordMemory(null, null, R32.ESI, 2), R16.BP, 0xb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHRD (new WordMemory (null, null, R32.ESI, 2), R16.BP, 0xb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xac, 0x2c, 0xb5, 0x0, 0x0, 0x0, 0x0, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHRD [ESI*4], BP, 0xb' failed.");
	}

	// SHRD mem32,reg32,imm8
	[Test]
	public void SHRD_mem32_reg32_imm8 ()
	{
		// SHRD [FS:0x12345678], EBP, 0x4
		// SHRD (new DWordMemory(Seg.FS, null, null, 0, 0x12345678), R32.EBP, 0x4)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHRD (new DWordMemory (Seg.FS, null, null, 0, 0x12345678), R32.EBP, 0x4);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xf, 0xac, 0x2d, 0x78, 0x56, 0x34, 0x12, 0x4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHRD [FS:0x12345678], EBP, 0x4' failed.");
	}

	// SHRD mem16,reg16,CL
	[Test]
	public void SHRD_mem16_reg16_CL ()
	{
		// SHRD [CS:ESP], DI, CL
		// SHRD___CL (new WordMemory(Seg.CS, R32.ESP, null, 0), R16.DI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHRD___CL (new WordMemory (Seg.CS, R32.ESP, null, 0), R16.DI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0x66, 0xf, 0xad, 0x3c, 0x24 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHRD [CS:ESP], DI, CL' failed.");
	}

	// SHRD mem32,reg32,CL
	[Test]
	public void SHRD_mem32_reg32_CL ()
	{
		// SHRD [EBX*2], ESI, CL
		// SHRD___CL (new DWordMemory(null, null, R32.EBX, 1), R32.ESI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHRD___CL (new DWordMemory (null, null, R32.EBX, 1), R32.ESI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xad, 0x34, 0x1b };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHRD [EBX*2], ESI, CL' failed.");
	}

	// SHRD rmreg16,reg16,imm8
	[Test]
	public void SHRD_rmreg16_reg16_imm8 ()
	{
		// SHRD SI, DX, 0x6
		// SHRD (R16.SI, R16.DX, 0x6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHRD (R16.SI, R16.DX, 0x6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xac, 0xd6, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHRD SI, DX, 0x6' failed.");
	}

	// SHRD rmreg32,reg32,imm8
	[Test]
	public void SHRD_rmreg32_reg32_imm8 ()
	{
		// SHRD EBX, ECX, 0x7
		// SHRD (R32.EBX, R32.ECX, 0x7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHRD (R32.EBX, R32.ECX, 0x7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xac, 0xcb, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHRD EBX, ECX, 0x7' failed.");
	}

	// SHRD rmreg16,reg16,CL
	[Test]
	public void SHRD_rmreg16_reg16_CL ()
	{
		// SHRD SP, SI, CL
		// SHRD___CL (R16.SP, R16.SI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHRD___CL (R16.SP, R16.SI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xad, 0xf4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHRD SP, SI, CL' failed.");
	}

	// SHRD rmreg32,reg32,CL
	[Test]
	public void SHRD_rmreg32_reg32_CL ()
	{
		// SHRD ECX, EAX, CL
		// SHRD___CL (R32.ECX, R32.EAX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SHRD___CL (R32.ECX, R32.EAX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xad, 0xc1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SHRD ECX, EAX, CL' failed.");
	}

	// SIDT mem
	[Test]
	public void SIDT_mem ()
	{
		// SIDT [DS:ESI + 0x12345678]
		// SIDT (new DWordMemory(Seg.DS, R32.ESI, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SIDT (new DWordMemory (Seg.DS, R32.ESI, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xf, 0x1, 0x8e, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SIDT [DS:ESI + 0x12345678]' failed.");
	}

	// SLDT mem16
	[Test]
	public void SLDT_mem16 ()
	{
		// SLDT Word [FS:0x12345678]
		// SLDT (new WordMemory(Seg.FS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SLDT (new WordMemory (Seg.FS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xf, 0x0, 0x5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SLDT Word [FS:0x12345678]' failed.");
	}

	// SLDT rmreg16
	[Test]
	public void SLDT_rmreg16 ()
	{
		// SLDT CX
		// SLDT (R16.CX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SLDT (R16.CX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x0, 0xc1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SLDT CX' failed.");
	}

	// SMSW mem16
	[Test]
	public void SMSW_mem16 ()
	{
		// SMSW Word [0x12345678]
		// SMSW (new WordMemory(null, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SMSW (new WordMemory (null, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x1, 0x25, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SMSW Word [0x12345678]' failed.");
	}

	// SMSW rmreg16
	[Test]
	public void SMSW_rmreg16 ()
	{
		// SMSW AX
		// SMSW (R16.AX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SMSW (R16.AX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x1, 0xe0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SMSW AX' failed.");
	}

	// STC 
	[Test]
	public void STC ()
	{
		// STC
		// STC ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.STC ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'STC' failed.");
	}

	// STD 
	[Test]
	public void STD ()
	{
		// STD
		// STD ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.STD ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xfd };
		Assert.IsTrue (CompareData (memoryStream, target), "'STD' failed.");
	}

	// STI 
	[Test]
	public void STI ()
	{
		// STI
		// STI ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.STI ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xfb };
		Assert.IsTrue (CompareData (memoryStream, target), "'STI' failed.");
	}

	// STOSB 
	[Test]
	public void STOSB ()
	{
		// STOSB
		// STOSB ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.STOSB ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xaa };
		Assert.IsTrue (CompareData (memoryStream, target), "'STOSB' failed.");
	}

	// STOSD 
	[Test]
	public void STOSD ()
	{
		// STOSD
		// STOSD ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.STOSD ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xab };
		Assert.IsTrue (CompareData (memoryStream, target), "'STOSD' failed.");
	}

	// STOSW 
	[Test]
	public void STOSW ()
	{
		// STOSW
		// STOSW ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.STOSW ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xab };
		Assert.IsTrue (CompareData (memoryStream, target), "'STOSW' failed.");
	}

	// STR mem16
	[Test]
	public void STR_mem16 ()
	{
		// STR Word [FS:EBP + 0x12345678]
		// STR (new WordMemory(Seg.FS, R32.EBP, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.STR (new WordMemory (Seg.FS, R32.EBP, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xf, 0x0, 0x8d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'STR Word [FS:EBP + 0x12345678]' failed.");
	}

	// STR rmreg16
	[Test]
	public void STR_rmreg16 ()
	{
		// STR DX
		// STR (R16.DX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.STR (R16.DX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0x0, 0xca };
		Assert.IsTrue (CompareData (memoryStream, target), "'STR DX' failed.");
	}

	// SUB mem8,reg8
	[Test]
	public void SUB_mem8_reg8 ()
	{
		// SUB [FS:EDI*4 + 0x12345678], BL
		// SUB (new ByteMemory(Seg.FS, null, R32.EDI, 2, 0x12345678), R8.BL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (new ByteMemory (Seg.FS, null, R32.EDI, 2, 0x12345678), R8.BL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0x28, 0x1c, 0xbd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB [FS:EDI*4 + 0x12345678], BL' failed.");
	}

	// SUB mem16,reg16
	[Test]
	public void SUB_mem16_reg16 ()
	{
		// SUB [0x12345678], DX
		// SUB (new WordMemory(null, null, null, 0, 0x12345678), R16.DX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (new WordMemory (null, null, null, 0, 0x12345678), R16.DX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x29, 0x15, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB [0x12345678], DX' failed.");
	}

	// SUB mem32,reg32
	[Test]
	public void SUB_mem32_reg32 ()
	{
		// SUB [ESP + EDX*2 + 0x12345678], ESP
		// SUB (new DWordMemory(null, R32.ESP, R32.EDX, 1, 0x12345678), R32.ESP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (new DWordMemory (null, R32.ESP, R32.EDX, 1, 0x12345678), R32.ESP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x29, 0xa4, 0x54, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB [ESP + EDX*2 + 0x12345678], ESP' failed.");
	}

	// SUB reg8,mem8
	[Test]
	public void SUB_reg8_mem8 ()
	{
		// SUB BH, [CS:ECX + 0x12345678]
		// SUB (R8.BH, new ByteMemory(Seg.CS, R32.ECX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (R8.BH, new ByteMemory (Seg.CS, R32.ECX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0x2a, 0xb9, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB BH, [CS:ECX + 0x12345678]' failed.");
	}

	// SUB reg16,mem16
	[Test]
	public void SUB_reg16_mem16 ()
	{
		// SUB CX, [CS:0x12345678]
		// SUB (R16.CX, new WordMemory(Seg.CS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (R16.CX, new WordMemory (Seg.CS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0x66, 0x2b, 0xd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB CX, [CS:0x12345678]' failed.");
	}

	// SUB reg32,mem32
	[Test]
	public void SUB_reg32_mem32 ()
	{
		// SUB ECX, [DS:ECX + 0x12345678]
		// SUB (R32.ECX, new DWordMemory(Seg.DS, R32.ECX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (R32.ECX, new DWordMemory (Seg.DS, R32.ECX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x2b, 0x89, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB ECX, [DS:ECX + 0x12345678]' failed.");
	}

	// SUB mem8,imm8
	[Test]
	public void SUB_mem8_imm8 ()
	{
		// SUB Byte [ECX*1 + 0x12345678], 0x2
		// SUB (new ByteMemory(null, null, R32.ECX, 0, 0x12345678), 0x2)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (new ByteMemory (null, null, R32.ECX, 0, 0x12345678), 0x2);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x80, 0xa9, 0x78, 0x56, 0x34, 0x12, 0x2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB Byte [ECX*1 + 0x12345678], 0x2' failed.");
	}

	// SUB mem16,imm16
	[Test]
	public void SUB_mem16_imm16 ()
	{
		// SUB Word [DS:ESI*4 + 0x12345678], 0x396
		// SUB (new WordMemory(Seg.DS, null, R32.ESI, 2, 0x12345678), 0x396)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (new WordMemory (Seg.DS, null, R32.ESI, 2, 0x12345678), 0x396);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x66, 0x81, 0x2c, 0xb5, 0x78, 0x56, 0x34, 0x12, 0x96, 0x3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB Word [DS:ESI*4 + 0x12345678], 0x396' failed.");
	}

	// SUB mem32,imm32
	[Test]
	public void SUB_mem32_imm32 ()
	{
		// SUB DWord [EDX + EDI*4 + 0x12345678], 0x435fcbf
		// SUB (new DWordMemory(null, R32.EDX, R32.EDI, 2, 0x12345678), 0x435fcbf)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (new DWordMemory (null, R32.EDX, R32.EDI, 2, 0x12345678), 0x435fcbf);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x81, 0xac, 0xba, 0x78, 0x56, 0x34, 0x12, 0xbf, 0xfc, 0x35, 0x4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB DWord [EDX + EDI*4 + 0x12345678], 0x435fcbf' failed.");
	}

	// SUB mem16,imm8
	[Test]
	public void SUB_mem16_imm8 ()
	{
		// SUB Word [DS:EAX*8], 0x7
		// SUB (new WordMemory(Seg.DS, null, R32.EAX, 3), 0x7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (new WordMemory (Seg.DS, null, R32.EAX, 3), 0x7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x66, 0x83, 0x2c, 0xc5, 0x0, 0x0, 0x0, 0x0, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB Word [DS:EAX*8], 0x7' failed.");
	}

	// SUB mem32,imm8
	[Test]
	public void SUB_mem32_imm8 ()
	{
		// SUB DWord [ESP + ESI*2 + 0x12345678], 0x3
		// SUB (new DWordMemory(null, R32.ESP, R32.ESI, 1, 0x12345678), 0x3)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (new DWordMemory (null, R32.ESP, R32.ESI, 1, 0x12345678), 0x3);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x83, 0xac, 0x74, 0x78, 0x56, 0x34, 0x12, 0x3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB DWord [ESP + ESI*2 + 0x12345678], 0x3' failed.");
	}

	// SUB rmreg8,reg8
	[Test]
	public void SUB_rmreg8_reg8 ()
	{
		// SUB AH, AL
		// SUB (R8.AH, R8.AL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (R8.AH, R8.AL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x28, 0xc4 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB AH, AL' failed.");
	}

	// SUB rmreg16,reg16
	[Test]
	public void SUB_rmreg16_reg16 ()
	{
		// SUB BX, SP
		// SUB (R16.BX, R16.SP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (R16.BX, R16.SP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x29, 0xe3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB BX, SP' failed.");
	}

	// SUB rmreg32,reg32
	[Test]
	public void SUB_rmreg32_reg32 ()
	{
		// SUB EDX, ESI
		// SUB (R32.EDX, R32.ESI)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (R32.EDX, R32.ESI);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x29, 0xf2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB EDX, ESI' failed.");
	}

	// SUB rmreg8,imm8
	[Test]
	public void SUB_rmreg8_imm8 ()
	{
		// SUB DH, 0x6
		// SUB (R8.DH, 0x6)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (R8.DH, 0x6);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x80, 0xee, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB DH, 0x6' failed.");
	}

	// SUB rmreg16,imm16
	[Test]
	public void SUB_rmreg16_imm16 ()
	{
		// SUB CX, 0xa8a
		// SUB (R16.CX, 0xa8a)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (R16.CX, 0xa8a);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x81, 0xe9, 0x8a, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB CX, 0xa8a' failed.");
	}

	// SUB rmreg32,imm32
	[Test]
	public void SUB_rmreg32_imm32 ()
	{
		// SUB ESI, 0xbfdafd2
		// SUB (R32.ESI, 0xbfdafd2)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (R32.ESI, 0xbfdafd2);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x81, 0xee, 0xd2, 0xaf, 0xfd, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB ESI, 0xbfdafd2' failed.");
	}

	// SUB rmreg16,imm8
	[Test]
	public void SUB_rmreg16_imm8 ()
	{
		// SUB SP, 0x8
		// SUB (R16.SP, 0x8)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (R16.SP, 0x8);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x83, 0xec, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB SP, 0x8' failed.");
	}

	// SUB rmreg32,imm8
	[Test]
	public void SUB_rmreg32_imm8 ()
	{
		// SUB ECX, 0xb
		// SUB (R32.ECX, 0xb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SUB (R32.ECX, 0xb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x83, 0xe9, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'SUB ECX, 0xb' failed.");
	}

	// SYSCALL 
	[Test]
	public void SYSCALL ()
	{
		// SYSCALL
		// SYSCALL ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SYSCALL ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SYSCALL' failed.");
	}

	// SYSENTER 
	[Test]
	public void SYSENTER ()
	{
		// SYSENTER
		// SYSENTER ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SYSENTER ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x34 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SYSENTER' failed.");
	}

	// SYSEXIT 
	[Test]
	public void SYSEXIT ()
	{
		// SYSEXIT
		// SYSEXIT ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SYSEXIT ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x35 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SYSEXIT' failed.");
	}

	// SYSRET 
	[Test]
	public void SYSRET ()
	{
		// SYSRET
		// SYSRET ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.SYSRET ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'SYSRET' failed.");
	}

	// TEST mem8,reg8
	[Test]
	public void TEST_mem8_reg8 ()
	{
		// TEST [DS:0x12345678], CL
		// TEST (new ByteMemory(Seg.DS, null, null, 0, 0x12345678), R8.CL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.TEST (new ByteMemory (Seg.DS, null, null, 0, 0x12345678), R8.CL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x84, 0xd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'TEST [DS:0x12345678], CL' failed.");
	}

	// TEST mem16,reg16
	[Test]
	public void TEST_mem16_reg16 ()
	{
		// TEST [FS:EBP*2 + 0x12345678], BP
		// TEST (new WordMemory(Seg.FS, null, R32.EBP, 1, 0x12345678), R16.BP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.TEST (new WordMemory (Seg.FS, null, R32.EBP, 1, 0x12345678), R16.BP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0x66, 0x85, 0xac, 0x2d, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'TEST [FS:EBP*2 + 0x12345678], BP' failed.");
	}

	// TEST mem32,reg32
	[Test]
	public void TEST_mem32_reg32 ()
	{
		// TEST [ES:0x12345678], EAX
		// TEST (new DWordMemory(Seg.ES, null, null, 0, 0x12345678), R32.EAX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.TEST (new DWordMemory (Seg.ES, null, null, 0, 0x12345678), R32.EAX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0x85, 0x5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'TEST [ES:0x12345678], EAX' failed.");
	}

	// TEST mem8,imm8
	[Test]
	public void TEST_mem8_imm8 ()
	{
		// TEST Byte [EBP*8 + 0x12345678], 0xa
		// TEST (new ByteMemory(null, null, R32.EBP, 3, 0x12345678), 0xa)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.TEST (new ByteMemory (null, null, R32.EBP, 3, 0x12345678), 0xa);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf6, 0x4, 0xed, 0x78, 0x56, 0x34, 0x12, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'TEST Byte [EBP*8 + 0x12345678], 0xa' failed.");
	}

	// TEST mem16,imm16
	[Test]
	public void TEST_mem16_imm16 ()
	{
		// TEST Word [DS:EDX*1], 0xeee
		// TEST (new WordMemory(Seg.DS, null, R32.EDX, 0), 0xeee)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.TEST (new WordMemory (Seg.DS, null, R32.EDX, 0), 0xeee);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x66, 0xf7, 0x2, 0xee, 0xe };
		Assert.IsTrue (CompareData (memoryStream, target), "'TEST Word [DS:EDX*1], 0xeee' failed.");
	}

	// TEST mem32,imm32
	[Test]
	public void TEST_mem32_imm32 ()
	{
		// TEST DWord [ECX*2 + 0x12345678], 0xdb3e2b7
		// TEST (new DWordMemory(null, null, R32.ECX, 1, 0x12345678), 0xdb3e2b7)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.TEST (new DWordMemory (null, null, R32.ECX, 1, 0x12345678), 0xdb3e2b7);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf7, 0x84, 0x9, 0x78, 0x56, 0x34, 0x12, 0xb7, 0xe2, 0xb3, 0xd };
		Assert.IsTrue (CompareData (memoryStream, target), "'TEST DWord [ECX*2 + 0x12345678], 0xdb3e2b7' failed.");
	}

	// TEST rmreg8,reg8
	[Test]
	public void TEST_rmreg8_reg8 ()
	{
		// TEST CL, BH
		// TEST (R8.CL, R8.BH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.TEST (R8.CL, R8.BH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x84, 0xf9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'TEST CL, BH' failed.");
	}

	// TEST rmreg16,reg16
	[Test]
	public void TEST_rmreg16_reg16 ()
	{
		// TEST DI, CX
		// TEST (R16.DI, R16.CX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.TEST (R16.DI, R16.CX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x85, 0xcf };
		Assert.IsTrue (CompareData (memoryStream, target), "'TEST DI, CX' failed.");
	}

	// TEST rmreg32,reg32
	[Test]
	public void TEST_rmreg32_reg32 ()
	{
		// TEST ESI, EBX
		// TEST (R32.ESI, R32.EBX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.TEST (R32.ESI, R32.EBX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x85, 0xde };
		Assert.IsTrue (CompareData (memoryStream, target), "'TEST ESI, EBX' failed.");
	}

	// TEST rmreg8,imm8
	[Test]
	public void TEST_rmreg8_imm8 ()
	{
		// TEST CL, 0x5
		// TEST (R8.CL, 0x5)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.TEST (R8.CL, 0x5);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf6, 0xc1, 0x5 };
		Assert.IsTrue (CompareData (memoryStream, target), "'TEST CL, 0x5' failed.");
	}

	// TEST rmreg16,imm16
	[Test]
	public void TEST_rmreg16_imm16 ()
	{
		// TEST DI, 0xb4d
		// TEST (R16.DI, 0xb4d)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.TEST (R16.DI, 0xb4d);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf7, 0xc7, 0x4d, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'TEST DI, 0xb4d' failed.");
	}

	// TEST rmreg32,imm32
	[Test]
	public void TEST_rmreg32_imm32 ()
	{
		// TEST ESP, 0xe698429
		// TEST (R32.ESP, 0xe698429)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.TEST (R32.ESP, 0xe698429);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf7, 0xc4, 0x29, 0x84, 0x69, 0xe };
		Assert.IsTrue (CompareData (memoryStream, target), "'TEST ESP, 0xe698429' failed.");
	}

	// VERR mem16
	[Test]
	public void VERR_mem16 ()
	{
		// VERR Word [FS:0x12345678]
		// VERR (new WordMemory(Seg.FS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.VERR (new WordMemory (Seg.FS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0xf, 0x0, 0x25, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'VERR Word [FS:0x12345678]' failed.");
	}

	// VERR rmreg16
	[Test]
	public void VERR_rmreg16 ()
	{
		// VERR DX
		// VERR (R16.DX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.VERR (R16.DX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x0, 0xe2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'VERR DX' failed.");
	}

	// VERW mem16
	[Test]
	public void VERW_mem16 ()
	{
		// VERW Word [GS:EDX*4]
		// VERW (new WordMemory(Seg.GS, null, R32.EDX, 2))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.VERW (new WordMemory (Seg.GS, null, R32.EDX, 2));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0xf, 0x0, 0x2c, 0x95, 0x0, 0x0, 0x0, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'VERW Word [GS:EDX*4]' failed.");
	}

	// VERW rmreg16
	[Test]
	public void VERW_rmreg16 ()
	{
		// VERW BP
		// VERW (R16.BP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.VERW (R16.BP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x0, 0xed };
		Assert.IsTrue (CompareData (memoryStream, target), "'VERW BP' failed.");
	}

	// WAIT 
	[Test]
	public void WAIT ()
	{
		// WAIT
		// WAIT ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.WAIT ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x9b };
		Assert.IsTrue (CompareData (memoryStream, target), "'WAIT' failed.");
	}

	// WBINVD 
	[Test]
	public void WBINVD ()
	{
		// WBINVD
		// WBINVD ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.WBINVD ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'WBINVD' failed.");
	}

	// WRMSR 
	[Test]
	public void WRMSR ()
	{
		// WRMSR
		// WRMSR ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.WRMSR ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0x30 };
		Assert.IsTrue (CompareData (memoryStream, target), "'WRMSR' failed.");
	}

	// XADD mem8,reg8
	[Test]
	public void XADD_mem8_reg8 ()
	{
		// XADD [ES:EAX*2], AL
		// XADD (new ByteMemory(Seg.ES, null, R32.EAX, 1), R8.AL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XADD (new ByteMemory (Seg.ES, null, R32.EAX, 1), R8.AL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x26, 0xf, 0xc0, 0x4, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XADD [ES:EAX*2], AL' failed.");
	}

	// XADD mem16,reg16
	[Test]
	public void XADD_mem16_reg16 ()
	{
		// XADD [ESI], BP
		// XADD (new WordMemory(null, R32.ESI, null, 0), R16.BP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XADD (new WordMemory (null, R32.ESI, null, 0), R16.BP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xc1, 0x2e };
		Assert.IsTrue (CompareData (memoryStream, target), "'XADD [ESI], BP' failed.");
	}

	// XADD mem32,reg32
	[Test]
	public void XADD_mem32_reg32 ()
	{
		// XADD [DS:0x12345678], ECX
		// XADD (new DWordMemory(Seg.DS, null, null, 0, 0x12345678), R32.ECX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XADD (new DWordMemory (Seg.DS, null, null, 0, 0x12345678), R32.ECX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0xf, 0xc1, 0xd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XADD [DS:0x12345678], ECX' failed.");
	}

	// XADD rmreg8,reg8
	[Test]
	public void XADD_rmreg8_reg8 ()
	{
		// XADD AL, CH
		// XADD (R8.AL, R8.CH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XADD (R8.AL, R8.CH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xc0, 0xe8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XADD AL, CH' failed.");
	}

	// XADD rmreg16,reg16
	[Test]
	public void XADD_rmreg16_reg16 ()
	{
		// XADD SI, BP
		// XADD (R16.SI, R16.BP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XADD (R16.SI, R16.BP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0xf, 0xc1, 0xee };
		Assert.IsTrue (CompareData (memoryStream, target), "'XADD SI, BP' failed.");
	}

	// XADD rmreg32,reg32
	[Test]
	public void XADD_rmreg32_reg32 ()
	{
		// XADD ESI, EBP
		// XADD (R32.ESI, R32.EBP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XADD (R32.ESI, R32.EBP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xf, 0xc1, 0xee };
		Assert.IsTrue (CompareData (memoryStream, target), "'XADD ESI, EBP' failed.");
	}

	// XCHG reg8,mem8
	[Test]
	public void XCHG_reg8_mem8 ()
	{
		// XCHG AH, [EBX + 0x12345678]
		// XCHG (R8.AH, new ByteMemory(null, R32.EBX, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XCHG (R8.AH, new ByteMemory (null, R32.EBX, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x86, 0xa3, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XCHG AH, [EBX + 0x12345678]' failed.");
	}

	// XCHG reg16,mem16
	[Test]
	public void XCHG_reg16_mem16 ()
	{
		// XCHG DI, [EBX + ECX*4]
		// XCHG (R16.DI, new WordMemory(null, R32.EBX, R32.ECX, 2))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XCHG (R16.DI, new WordMemory (null, R32.EBX, R32.ECX, 2));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x87, 0x3c, 0x8b };
		Assert.IsTrue (CompareData (memoryStream, target), "'XCHG DI, [EBX + ECX*4]' failed.");
	}

	// XCHG reg32,mem32
	[Test]
	public void XCHG_reg32_mem32 ()
	{
		// XCHG EDI, [EBP + EDX*8 + 0x12345678]
		// XCHG (R32.EDI, new DWordMemory(null, R32.EBP, R32.EDX, 3, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XCHG (R32.EDI, new DWordMemory (null, R32.EBP, R32.EDX, 3, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x87, 0xbc, 0xd5, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XCHG EDI, [EBP + EDX*8 + 0x12345678]' failed.");
	}

	// XCHG mem8,reg8
	[Test]
	public void XCHG_mem8_reg8 ()
	{
		// XCHG [FS:ECX*8 + 0x12345678], AL
		// XCHG (new ByteMemory(Seg.FS, null, R32.ECX, 3, 0x12345678), R8.AL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XCHG (new ByteMemory (Seg.FS, null, R32.ECX, 3, 0x12345678), R8.AL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0x86, 0x4, 0xcd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XCHG [FS:ECX*8 + 0x12345678], AL' failed.");
	}

	// XCHG mem16,reg16
	[Test]
	public void XCHG_mem16_reg16 ()
	{
		// XCHG [EBX + EDI*8 + 0x12345678], DX
		// XCHG (new WordMemory(null, R32.EBX, R32.EDI, 3, 0x12345678), R16.DX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XCHG (new WordMemory (null, R32.EBX, R32.EDI, 3, 0x12345678), R16.DX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x87, 0x94, 0xfb, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XCHG [EBX + EDI*8 + 0x12345678], DX' failed.");
	}

	// XCHG mem32,reg32
	[Test]
	public void XCHG_mem32_reg32 ()
	{
		// XCHG [GS:ESP], ESP
		// XCHG (new DWordMemory(Seg.GS, R32.ESP, null, 0), R32.ESP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XCHG (new DWordMemory (Seg.GS, R32.ESP, null, 0), R32.ESP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0x87, 0x24, 0x24 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XCHG [GS:ESP], ESP' failed.");
	}

	// XCHG reg8,rmreg8
	[Test]
	public void XCHG_reg8_rmreg8 ()
	{
		// XCHG DH, BL
		// XCHG (R8.DH, R8.BL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XCHG (R8.DH, R8.BL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x86, 0xf3 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XCHG DH, BL' failed.");
	}

	// XCHG reg16,rmreg16
	[Test]
	public void XCHG_reg16_rmreg16 ()
	{
		// XCHG BP, SP
		// XCHG (R16.BP, R16.SP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XCHG (R16.BP, R16.SP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x87, 0xec };
		Assert.IsTrue (CompareData (memoryStream, target), "'XCHG BP, SP' failed.");
	}

	// XCHG reg32,rmreg32
	[Test]
	public void XCHG_reg32_rmreg32 ()
	{
		// XCHG EDX, ECX
		// XCHG (R32.EDX, R32.ECX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XCHG (R32.EDX, R32.ECX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x87, 0xd1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XCHG EDX, ECX' failed.");
	}

	// XLAT 
	[Test]
	public void XLAT ()
	{
		// XLAT
		// XLAT ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XLAT ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XLAT' failed.");
	}

	// XLATB 
	[Test]
	public void XLATB ()
	{
		// XLATB
		// XLATB ()
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XLATB ();
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0xd7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XLATB' failed.");
	}

	// XOR mem8,reg8
	[Test]
	public void XOR_mem8_reg8 ()
	{
		// XOR [FS:ESI*1], AH
		// XOR (new ByteMemory(Seg.FS, null, R32.ESI, 0), R8.AH)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (new ByteMemory (Seg.FS, null, R32.ESI, 0), R8.AH);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x64, 0x30, 0x26 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR [FS:ESI*1], AH' failed.");
	}

	// XOR mem16,reg16
	[Test]
	public void XOR_mem16_reg16 ()
	{
		// XOR [DS:EDX], BP
		// XOR (new WordMemory(Seg.DS, R32.EDX, null, 0), R16.BP)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (new WordMemory (Seg.DS, R32.EDX, null, 0), R16.BP);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x66, 0x31, 0x2a };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR [DS:EDX], BP' failed.");
	}

	// XOR mem32,reg32
	[Test]
	public void XOR_mem32_reg32 ()
	{
		// XOR [SS:ECX*8 + 0x12345678], EAX
		// XOR (new DWordMemory(Seg.SS, null, R32.ECX, 3, 0x12345678), R32.EAX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (new DWordMemory (Seg.SS, null, R32.ECX, 3, 0x12345678), R32.EAX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x36, 0x31, 0x4, 0xcd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR [SS:ECX*8 + 0x12345678], EAX' failed.");
	}

	// XOR reg8,mem8
	[Test]
	public void XOR_reg8_mem8 ()
	{
		// XOR DH, [CS:ESP + EBP*2 + 0x12345678]
		// XOR (R8.DH, new ByteMemory(Seg.CS, R32.ESP, R32.EBP, 1, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (R8.DH, new ByteMemory (Seg.CS, R32.ESP, R32.EBP, 1, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x2e, 0x32, 0xb4, 0x6c, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR DH, [CS:ESP + EBP*2 + 0x12345678]' failed.");
	}

	// XOR reg16,mem16
	[Test]
	public void XOR_reg16_mem16 ()
	{
		// XOR CX, [DS:0x12345678]
		// XOR (R16.CX, new WordMemory(Seg.DS, null, null, 0, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (R16.CX, new WordMemory (Seg.DS, null, null, 0, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x3e, 0x66, 0x33, 0xd, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR CX, [DS:0x12345678]' failed.");
	}

	// XOR reg32,mem32
	[Test]
	public void XOR_reg32_mem32 ()
	{
		// XOR ESI, [GS:EAX*2 + 0x12345678]
		// XOR (R32.ESI, new DWordMemory(Seg.GS, null, R32.EAX, 1, 0x12345678))
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (R32.ESI, new DWordMemory (Seg.GS, null, R32.EAX, 1, 0x12345678));
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0x33, 0xb4, 0x0, 0x78, 0x56, 0x34, 0x12 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR ESI, [GS:EAX*2 + 0x12345678]' failed.");
	}

	// XOR mem8,imm8
	[Test]
	public void XOR_mem8_imm8 ()
	{
		// XOR Byte [ESP + EBP*2], 0x1
		// XOR (new ByteMemory(null, R32.ESP, R32.EBP, 1), 0x1)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (new ByteMemory (null, R32.ESP, R32.EBP, 1), 0x1);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x80, 0x34, 0x6c, 0x1 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR Byte [ESP + EBP*2], 0x1' failed.");
	}

	// XOR mem16,imm16
	[Test]
	public void XOR_mem16_imm16 ()
	{
		// XOR Word [EBX*1 + 0x12345678], 0x617
		// XOR (new WordMemory(null, null, R32.EBX, 0, 0x12345678), 0x617)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (new WordMemory (null, null, R32.EBX, 0, 0x12345678), 0x617);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x81, 0xb3, 0x78, 0x56, 0x34, 0x12, 0x17, 0x6 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR Word [EBX*1 + 0x12345678], 0x617' failed.");
	}

	// XOR mem32,imm32
	[Test]
	public void XOR_mem32_imm32 ()
	{
		// XOR DWord [0x12345678], 0x969210a
		// XOR (new DWordMemory(null, null, null, 0, 0x12345678), 0x969210a)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (new DWordMemory (null, null, null, 0, 0x12345678), 0x969210a);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x81, 0x35, 0x78, 0x56, 0x34, 0x12, 0xa, 0x21, 0x69, 0x9 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR DWord [0x12345678], 0x969210a' failed.");
	}

	// XOR mem16,imm8
	[Test]
	public void XOR_mem16_imm8 ()
	{
		// XOR Word [GS:ECX], 0x0
		// XOR (new WordMemory(Seg.GS, R32.ECX, null, 0), 0x0)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (new WordMemory (Seg.GS, R32.ECX, null, 0), 0x0);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0x66, 0x83, 0x31, 0x0 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR Word [GS:ECX], 0x0' failed.");
	}

	// XOR mem32,imm8
	[Test]
	public void XOR_mem32_imm8 ()
	{
		// XOR DWord [GS:ESP + ECX*2], 0xb
		// XOR (new DWordMemory(Seg.GS, R32.ESP, R32.ECX, 1), 0xb)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (new DWordMemory (Seg.GS, R32.ESP, R32.ECX, 1), 0xb);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x65, 0x83, 0x34, 0x4c, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR DWord [GS:ESP + ECX*2], 0xb' failed.");
	}

	// XOR rmreg8,reg8
	[Test]
	public void XOR_rmreg8_reg8 ()
	{
		// XOR DL, DL
		// XOR (R8.DL, R8.DL)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (R8.DL, R8.DL);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x30, 0xd2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR DL, DL' failed.");
	}

	// XOR rmreg16,reg16
	[Test]
	public void XOR_rmreg16_reg16 ()
	{
		// XOR DX, DX
		// XOR (R16.DX, R16.DX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (R16.DX, R16.DX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x31, 0xd2 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR DX, DX' failed.");
	}

	// XOR rmreg32,reg32
	[Test]
	public void XOR_rmreg32_reg32 ()
	{
		// XOR EDI, EDX
		// XOR (R32.EDI, R32.EDX)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (R32.EDI, R32.EDX);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x31, 0xd7 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR EDI, EDX' failed.");
	}

	// XOR rmreg8,imm8
	[Test]
	public void XOR_rmreg8_imm8 ()
	{
		// XOR BH, 0xa
		// XOR (R8.BH, 0xa)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (R8.BH, 0xa);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x80, 0xf7, 0xa };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR BH, 0xa' failed.");
	}

	// XOR rmreg16,imm16
	[Test]
	public void XOR_rmreg16_imm16 ()
	{
		// XOR DX, 0xe42
		// XOR (R16.DX, 0xe42)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (R16.DX, 0xe42);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x81, 0xf2, 0x42, 0xe };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR DX, 0xe42' failed.");
	}

	// XOR rmreg32,imm32
	[Test]
	public void XOR_rmreg32_imm32 ()
	{
		// XOR EDI, 0xb40e972
		// XOR (R32.EDI, 0xb40e972)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (R32.EDI, 0xb40e972);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x81, 0xf7, 0x72, 0xe9, 0x40, 0xb };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR EDI, 0xb40e972' failed.");
	}

	// XOR rmreg16,imm8
	[Test]
	public void XOR_rmreg16_imm8 ()
	{
		// XOR SI, 0xd
		// XOR (R16.SI, 0xd)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (R16.SI, 0xd);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x66, 0x83, 0xf6, 0xd };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR SI, 0xd' failed.");
	}

	// XOR rmreg32,imm8
	[Test]
	public void XOR_rmreg32_imm8 ()
	{
		// XOR ESI, 0x8
		// XOR (R32.ESI, 0x8)
		MemoryStream memoryStream = new MemoryStream ();
		Assembly asm = new Assembly ();
		asm.XOR (R32.ESI, 0x8);
		asm.Encode (memoryStream);
		byte [] target = new byte [] { 0x83, 0xf6, 0x8 };
		Assert.IsTrue (CompareData (memoryStream, target), "'XOR ESI, 0x8' failed.");
	}
}
