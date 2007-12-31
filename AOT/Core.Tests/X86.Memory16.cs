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
/*
[TestFixture]
public class X86Memory16
{
	public bool CompareData(MemoryStream memoryStream, byte[] target)
	{
		byte[] source = new byte[memoryStream.Length];
		memoryStream.Seek(0, SeekOrigin.Begin);
		memoryStream.Read(source, 0, source.Length);
		
		if (memoryStream.Length != target.Length)
		{
			return false;
		}
		
		for (int i = 0; i < source.Length; i++)
		{
			if (source[i] != target[i])
			{
				return false;
			}
		}
		
		return true;
	}
	
	[Test]
	public void Mov__BX___DX ()
	{
		// Mov [BX], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.BX, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x17};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [BX], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_BX___DX ()
	{
		// Mov [DS:BX], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.BX, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x17};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:BX], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_BX___DX ()
	{
		// Mov [ES:BX], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.BX, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x17};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:BX], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_BX___DX ()
	{
		// Mov [CS:BX], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.BX, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x17};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:BX], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_BX___DX ()
	{
		// Mov [SS:BX], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.BX, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x17};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:BX], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_BX___DX ()
	{
		// Mov [FS:BX], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.BX, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x17};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:BX], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_BX___DX ()
	{
		// Mov [GS:BX], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.BX, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x17};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:BX], DX' failed.");
	}
	
	[Test]
	public void Mov__BX_0x1234___DX ()
	{
		// Mov [BX+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.BX, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x97, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [BX+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_BX_0x1234___DX ()
	{
		// Mov [DS:BX+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.BX, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x97, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:BX+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_BX_0x1234___DX ()
	{
		// Mov [ES:BX+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.BX, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x97, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:BX+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_BX_0x1234___DX ()
	{
		// Mov [CS:BX+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.BX, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x97, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:BX+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_BX_0x1234___DX ()
	{
		// Mov [SS:BX+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.BX, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x97, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:BX+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_BX_0x1234___DX ()
	{
		// Mov [FS:BX+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.BX, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x97, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:BX+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_BX_0x1234___DX ()
	{
		// Mov [GS:BX+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.BX, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x97, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:BX+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__BX_0xC___DX ()
	{
		// Mov [BX-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.BX, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x57, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [BX-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_BX_0xC___DX ()
	{
		// Mov [DS:BX-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.BX, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x57, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:BX-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_BX_0xC___DX ()
	{
		// Mov [ES:BX-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.BX, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x57, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:BX-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_BX_0xC___DX ()
	{
		// Mov [CS:BX-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.BX, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x57, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:BX-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_BX_0xC___DX ()
	{
		// Mov [SS:BX-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.BX, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x57, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:BX-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_BX_0xC___DX ()
	{
		// Mov [FS:BX-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.BX, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x57, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:BX-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_BX_0xC___DX ()
	{
		// Mov [GS:BX-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.BX, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x57, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:BX-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__BX___SI___DX ()
	{
		// Mov [BX + SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.BX, R16.SI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x10};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [BX + SI], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_BX___SI___DX ()
	{
		// Mov [DS:BX + SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.BX, R16.SI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x10};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:BX + SI], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_BX___SI___DX ()
	{
		// Mov [ES:BX + SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.BX, R16.SI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x10};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:BX + SI], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_BX___SI___DX ()
	{
		// Mov [CS:BX + SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.BX, R16.SI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x10};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:BX + SI], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_BX___SI___DX ()
	{
		// Mov [SS:BX + SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.BX, R16.SI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x10};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:BX + SI], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_BX___SI___DX ()
	{
		// Mov [FS:BX + SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.BX, R16.SI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x10};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:BX + SI], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_BX___SI___DX ()
	{
		// Mov [GS:BX + SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.BX, R16.SI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x10};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:BX + SI], DX' failed.");
	}
	
	[Test]
	public void Mov__BX___SI_0x1234___DX ()
	{
		// Mov [BX + SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.BX, R16.SI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x90, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [BX + SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_BX___SI_0x1234___DX ()
	{
		// Mov [DS:BX + SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.BX, R16.SI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x90, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:BX + SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_BX___SI_0x1234___DX ()
	{
		// Mov [ES:BX + SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.BX, R16.SI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x90, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:BX + SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_BX___SI_0x1234___DX ()
	{
		// Mov [CS:BX + SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.BX, R16.SI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x90, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:BX + SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_BX___SI_0x1234___DX ()
	{
		// Mov [SS:BX + SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.BX, R16.SI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x90, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:BX + SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_BX___SI_0x1234___DX ()
	{
		// Mov [FS:BX + SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.BX, R16.SI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x90, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:BX + SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_BX___SI_0x1234___DX ()
	{
		// Mov [GS:BX + SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.BX, R16.SI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x90, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:BX + SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__BX___SI_0xC___DX ()
	{
		// Mov [BX + SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.BX, R16.SI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x50, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [BX + SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_BX___SI_0xC___DX ()
	{
		// Mov [DS:BX + SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.BX, R16.SI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x50, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:BX + SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_BX___SI_0xC___DX ()
	{
		// Mov [ES:BX + SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.BX, R16.SI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x50, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:BX + SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_BX___SI_0xC___DX ()
	{
		// Mov [CS:BX + SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.BX, R16.SI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x50, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:BX + SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_BX___SI_0xC___DX ()
	{
		// Mov [SS:BX + SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.BX, R16.SI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x50, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:BX + SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_BX___SI_0xC___DX ()
	{
		// Mov [FS:BX + SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.BX, R16.SI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x50, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:BX + SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_BX___SI_0xC___DX ()
	{
		// Mov [GS:BX + SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.BX, R16.SI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x50, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:BX + SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__BX___DI___DX ()
	{
		// Mov [BX + DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.BX, R16.DI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x11};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [BX + DI], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_BX___DI___DX ()
	{
		// Mov [DS:BX + DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.BX, R16.DI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x11};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:BX + DI], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_BX___DI___DX ()
	{
		// Mov [ES:BX + DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.BX, R16.DI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x11};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:BX + DI], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_BX___DI___DX ()
	{
		// Mov [CS:BX + DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.BX, R16.DI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x11};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:BX + DI], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_BX___DI___DX ()
	{
		// Mov [SS:BX + DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.BX, R16.DI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x11};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:BX + DI], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_BX___DI___DX ()
	{
		// Mov [FS:BX + DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.BX, R16.DI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x11};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:BX + DI], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_BX___DI___DX ()
	{
		// Mov [GS:BX + DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.BX, R16.DI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x11};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:BX + DI], DX' failed.");
	}
	
	[Test]
	public void Mov__BX___DI_0x1234___DX ()
	{
		// Mov [BX + DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.BX, R16.DI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x91, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [BX + DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_BX___DI_0x1234___DX ()
	{
		// Mov [DS:BX + DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.BX, R16.DI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x91, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:BX + DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_BX___DI_0x1234___DX ()
	{
		// Mov [ES:BX + DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.BX, R16.DI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x91, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:BX + DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_BX___DI_0x1234___DX ()
	{
		// Mov [CS:BX + DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.BX, R16.DI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x91, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:BX + DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_BX___DI_0x1234___DX ()
	{
		// Mov [SS:BX + DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.BX, R16.DI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x91, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:BX + DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_BX___DI_0x1234___DX ()
	{
		// Mov [FS:BX + DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.BX, R16.DI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x91, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:BX + DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_BX___DI_0x1234___DX ()
	{
		// Mov [GS:BX + DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.BX, R16.DI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x91, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:BX + DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__BX___DI_0xC___DX ()
	{
		// Mov [BX + DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.BX, R16.DI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x51, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [BX + DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_BX___DI_0xC___DX ()
	{
		// Mov [DS:BX + DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.BX, R16.DI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x51, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:BX + DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_BX___DI_0xC___DX ()
	{
		// Mov [ES:BX + DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.BX, R16.DI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x51, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:BX + DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_BX___DI_0xC___DX ()
	{
		// Mov [CS:BX + DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.BX, R16.DI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x51, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:BX + DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_BX___DI_0xC___DX ()
	{
		// Mov [SS:BX + DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.BX, R16.DI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x51, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:BX + DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_BX___DI_0xC___DX ()
	{
		// Mov [FS:BX + DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.BX, R16.DI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x51, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:BX + DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_BX___DI_0xC___DX ()
	{
		// Mov [GS:BX + DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.BX, R16.DI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x51, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:BX + DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__BP___DX ()
	{
		// Mov [BP], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.BP, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x56, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [BP], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_BP___DX ()
	{
		// Mov [DS:BP], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.BP, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x56, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:BP], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_BP___DX ()
	{
		// Mov [ES:BP], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.BP, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x56, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:BP], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_BP___DX ()
	{
		// Mov [CS:BP], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.BP, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x56, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:BP], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_BP___DX ()
	{
		// Mov [SS:BP], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.BP, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x56, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:BP], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_BP___DX ()
	{
		// Mov [FS:BP], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.BP, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x56, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:BP], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_BP___DX ()
	{
		// Mov [GS:BP], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.BP, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x56, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:BP], DX' failed.");
	}
	
	[Test]
	public void Mov__BP_0x1234___DX ()
	{
		// Mov [BP+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.BP, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x96, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [BP+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_BP_0x1234___DX ()
	{
		// Mov [DS:BP+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.BP, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x96, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:BP+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_BP_0x1234___DX ()
	{
		// Mov [ES:BP+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.BP, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x96, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:BP+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_BP_0x1234___DX ()
	{
		// Mov [CS:BP+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.BP, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x96, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:BP+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_BP_0x1234___DX ()
	{
		// Mov [SS:BP+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.BP, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x96, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:BP+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_BP_0x1234___DX ()
	{
		// Mov [FS:BP+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.BP, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x96, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:BP+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_BP_0x1234___DX ()
	{
		// Mov [GS:BP+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.BP, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x96, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:BP+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__BP_0xC___DX ()
	{
		// Mov [BP-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.BP, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x56, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [BP-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_BP_0xC___DX ()
	{
		// Mov [DS:BP-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.BP, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x56, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:BP-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_BP_0xC___DX ()
	{
		// Mov [ES:BP-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.BP, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x56, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:BP-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_BP_0xC___DX ()
	{
		// Mov [CS:BP-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.BP, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x56, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:BP-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_BP_0xC___DX ()
	{
		// Mov [SS:BP-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.BP, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x56, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:BP-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_BP_0xC___DX ()
	{
		// Mov [FS:BP-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.BP, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x56, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:BP-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_BP_0xC___DX ()
	{
		// Mov [GS:BP-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.BP, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x56, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:BP-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__BP___SI___DX ()
	{
		// Mov [BP + SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.BP, R16.SI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [BP + SI], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_BP___SI___DX ()
	{
		// Mov [DS:BP + SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.BP, R16.SI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:BP + SI], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_BP___SI___DX ()
	{
		// Mov [ES:BP + SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.BP, R16.SI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:BP + SI], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_BP___SI___DX ()
	{
		// Mov [CS:BP + SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.BP, R16.SI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:BP + SI], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_BP___SI___DX ()
	{
		// Mov [SS:BP + SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.BP, R16.SI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:BP + SI], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_BP___SI___DX ()
	{
		// Mov [FS:BP + SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.BP, R16.SI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:BP + SI], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_BP___SI___DX ()
	{
		// Mov [GS:BP + SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.BP, R16.SI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:BP + SI], DX' failed.");
	}
	
	[Test]
	public void Mov__BP___SI_0x1234___DX ()
	{
		// Mov [BP + SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.BP, R16.SI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x92, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [BP + SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_BP___SI_0x1234___DX ()
	{
		// Mov [DS:BP + SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.BP, R16.SI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x92, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:BP + SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_BP___SI_0x1234___DX ()
	{
		// Mov [ES:BP + SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.BP, R16.SI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x92, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:BP + SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_BP___SI_0x1234___DX ()
	{
		// Mov [CS:BP + SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.BP, R16.SI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x92, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:BP + SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_BP___SI_0x1234___DX ()
	{
		// Mov [SS:BP + SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.BP, R16.SI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x92, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:BP + SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_BP___SI_0x1234___DX ()
	{
		// Mov [FS:BP + SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.BP, R16.SI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x92, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:BP + SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_BP___SI_0x1234___DX ()
	{
		// Mov [GS:BP + SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.BP, R16.SI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x92, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:BP + SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__BP___SI_0xC___DX ()
	{
		// Mov [BP + SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.BP, R16.SI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x52, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [BP + SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_BP___SI_0xC___DX ()
	{
		// Mov [DS:BP + SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.BP, R16.SI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x52, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:BP + SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_BP___SI_0xC___DX ()
	{
		// Mov [ES:BP + SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.BP, R16.SI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x52, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:BP + SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_BP___SI_0xC___DX ()
	{
		// Mov [CS:BP + SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.BP, R16.SI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x52, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:BP + SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_BP___SI_0xC___DX ()
	{
		// Mov [SS:BP + SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.BP, R16.SI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x52, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:BP + SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_BP___SI_0xC___DX ()
	{
		// Mov [FS:BP + SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.BP, R16.SI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x52, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:BP + SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_BP___SI_0xC___DX ()
	{
		// Mov [GS:BP + SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.BP, R16.SI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x52, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:BP + SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__BP___DI___DX ()
	{
		// Mov [BP + DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.BP, R16.DI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x13};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [BP + DI], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_BP___DI___DX ()
	{
		// Mov [DS:BP + DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.BP, R16.DI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x13};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:BP + DI], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_BP___DI___DX ()
	{
		// Mov [ES:BP + DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.BP, R16.DI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x13};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:BP + DI], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_BP___DI___DX ()
	{
		// Mov [CS:BP + DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.BP, R16.DI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x13};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:BP + DI], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_BP___DI___DX ()
	{
		// Mov [SS:BP + DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.BP, R16.DI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x13};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:BP + DI], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_BP___DI___DX ()
	{
		// Mov [FS:BP + DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.BP, R16.DI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x13};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:BP + DI], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_BP___DI___DX ()
	{
		// Mov [GS:BP + DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.BP, R16.DI),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x13};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:BP + DI], DX' failed.");
	}
	
	[Test]
	public void Mov__BP___DI_0x1234___DX ()
	{
		// Mov [BP + DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.BP, R16.DI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x93, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [BP + DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_BP___DI_0x1234___DX ()
	{
		// Mov [DS:BP + DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.BP, R16.DI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x93, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:BP + DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_BP___DI_0x1234___DX ()
	{
		// Mov [ES:BP + DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.BP, R16.DI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x93, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:BP + DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_BP___DI_0x1234___DX ()
	{
		// Mov [CS:BP + DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.BP, R16.DI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x93, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:BP + DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_BP___DI_0x1234___DX ()
	{
		// Mov [SS:BP + DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.BP, R16.DI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x93, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:BP + DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_BP___DI_0x1234___DX ()
	{
		// Mov [FS:BP + DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.BP, R16.DI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x93, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:BP + DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_BP___DI_0x1234___DX ()
	{
		// Mov [GS:BP + DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.BP, R16.DI, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x93, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:BP + DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__BP___DI_0xC___DX ()
	{
		// Mov [BP + DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.BP, R16.DI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x53, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [BP + DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_BP___DI_0xC___DX ()
	{
		// Mov [DS:BP + DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.BP, R16.DI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x53, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:BP + DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_BP___DI_0xC___DX ()
	{
		// Mov [ES:BP + DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.BP, R16.DI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x53, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:BP + DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_BP___DI_0xC___DX ()
	{
		// Mov [CS:BP + DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.BP, R16.DI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x53, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:BP + DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_BP___DI_0xC___DX ()
	{
		// Mov [SS:BP + DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.BP, R16.DI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x53, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:BP + DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_BP___DI_0xC___DX ()
	{
		// Mov [FS:BP + DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.BP, R16.DI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x53, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:BP + DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_BP___DI_0xC___DX ()
	{
		// Mov [GS:BP + DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.BP, R16.DI, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x53, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:BP + DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__SI___DX ()
	{
		// Mov [SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.SI, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SI], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_SI___DX ()
	{
		// Mov [DS:SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.SI, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:SI], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_SI___DX ()
	{
		// Mov [ES:SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.SI, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x14};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:SI], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_SI___DX ()
	{
		// Mov [CS:SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.SI, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x14};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:SI], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_SI___DX ()
	{
		// Mov [SS:SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.SI, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x14};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:SI], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_SI___DX ()
	{
		// Mov [FS:SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.SI, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x14};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:SI], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_SI___DX ()
	{
		// Mov [GS:SI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.SI, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x14};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:SI], DX' failed.");
	}
	
	[Test]
	public void Mov__SI_0x1234___DX ()
	{
		// Mov [SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.SI, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_SI_0x1234___DX ()
	{
		// Mov [DS:SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.SI, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_SI_0x1234___DX ()
	{
		// Mov [ES:SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.SI, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x94, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_SI_0x1234___DX ()
	{
		// Mov [CS:SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.SI, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x94, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_SI_0x1234___DX ()
	{
		// Mov [SS:SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.SI, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x94, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_SI_0x1234___DX ()
	{
		// Mov [FS:SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.SI, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x94, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_SI_0x1234___DX ()
	{
		// Mov [GS:SI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.SI, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x94, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:SI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__SI_0xC___DX ()
	{
		// Mov [SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.SI, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_SI_0xC___DX ()
	{
		// Mov [DS:SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.SI, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_SI_0xC___DX ()
	{
		// Mov [ES:SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.SI, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x54, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_SI_0xC___DX ()
	{
		// Mov [CS:SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.SI, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x54, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_SI_0xC___DX ()
	{
		// Mov [SS:SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.SI, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x54, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_SI_0xC___DX ()
	{
		// Mov [FS:SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.SI, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x54, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_SI_0xC___DX ()
	{
		// Mov [GS:SI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.SI, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x54, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:SI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__DI___DX ()
	{
		// Mov [DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.DI, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x15};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DI], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_DI___DX ()
	{
		// Mov [DS:DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.DI, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x15};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:DI], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_DI___DX ()
	{
		// Mov [ES:DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.DI, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x15};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:DI], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_DI___DX ()
	{
		// Mov [CS:DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.DI, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x15};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:DI], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_DI___DX ()
	{
		// Mov [SS:DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.DI, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x15};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:DI], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_DI___DX ()
	{
		// Mov [FS:DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.DI, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x15};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:DI], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_DI___DX ()
	{
		// Mov [GS:DI], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.DI, null),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x15};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:DI], DX' failed.");
	}
	
	[Test]
	public void Mov__DI_0x1234___DX ()
	{
		// Mov [DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.DI, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x95, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_DI_0x1234___DX ()
	{
		// Mov [DS:DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.DI, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x95, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_DI_0x1234___DX ()
	{
		// Mov [ES:DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.DI, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x95, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_DI_0x1234___DX ()
	{
		// Mov [CS:DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.DI, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x95, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_DI_0x1234___DX ()
	{
		// Mov [SS:DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.DI, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x95, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_DI_0x1234___DX ()
	{
		// Mov [FS:DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.DI, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x95, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_DI_0x1234___DX ()
	{
		// Mov [GS:DI+0x1234], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.DI, null, +0x1234),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x95, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:DI+0x1234], DX' failed.");
	}
	
	[Test]
	public void Mov__DI_0xC___DX ()
	{
		// Mov [DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(null, R16.DI, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x55, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__DS_DI_0xC___DX ()
	{
		// Mov [DS:DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.DS, R16.DI, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x55, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__ES_DI_0xC___DX ()
	{
		// Mov [ES:DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.ES, R16.DI, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x26, 0x89, 0x55, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ES:DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__CS_DI_0xC___DX ()
	{
		// Mov [CS:DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.CS, R16.DI, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x2e, 0x89, 0x55, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [CS:DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__SS_DI_0xC___DX ()
	{
		// Mov [SS:DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.SS, R16.DI, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x36, 0x89, 0x55, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [SS:DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__FS_DI_0xC___DX ()
	{
		// Mov [FS:DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.FS, R16.DI, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x64, 0x89, 0x55, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [FS:DI-0xC], DX' failed.");
	}
	
	[Test]
	public void Mov__GS_DI_0xC___DX ()
	{
		// Mov [GS:DI-0xC], DX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly(false);
		asm.MOV (new WordMemory(Seg.GS, R16.DI, null, -0xC),  R16.DX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x65, 0x89, 0x55, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [GS:DI-0xC], DX' failed.");
	}
}
*/