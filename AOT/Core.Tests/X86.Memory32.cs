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
public class X86Memory32
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
	public void Mov___0x12345678___EDX ()
	{
		// Mov [+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, null, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x15, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov___0xC___EDX ()
	{
		// Mov [-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, null, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x15, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS__0x12345678___EDX ()
	{
		// Mov [DS:+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, null, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x15, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS__0xC___EDX ()
	{
		// Mov [DS:-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, null, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x15, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX_1___EDX ()
	{
		// Mov [EAX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EAX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x10};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX_2___EDX ()
	{
		// Mov [EAX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EAX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX_4___EDX ()
	{
		// Mov [EAX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EAX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x85, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX_8___EDX ()
	{
		// Mov [EAX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EAX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xc5, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX_1_0x12345678___EDX ()
	{
		// Mov [EAX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EAX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x90, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX_2_0x12345678___EDX ()
	{
		// Mov [EAX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EAX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x0, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX_4_0x12345678___EDX ()
	{
		// Mov [EAX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EAX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x85, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX_8_0x12345678___EDX ()
	{
		// Mov [EAX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EAX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xc5, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX_1_0xC___EDX ()
	{
		// Mov [EAX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EAX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x50, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX_2_0xC___EDX ()
	{
		// Mov [EAX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EAX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x0, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX_4_0xC___EDX ()
	{
		// Mov [EAX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EAX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x85, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX_8_0xC___EDX ()
	{
		// Mov [EAX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EAX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xc5, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX_1___EDX ()
	{
		// Mov [DS:EAX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EAX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x10};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX_2___EDX ()
	{
		// Mov [DS:EAX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EAX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX_4___EDX ()
	{
		// Mov [DS:EAX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EAX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x85, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX_8___EDX ()
	{
		// Mov [DS:EAX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EAX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xc5, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX_1_0x12345678___EDX ()
	{
		// Mov [DS:EAX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EAX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x90, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX_2_0x12345678___EDX ()
	{
		// Mov [DS:EAX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EAX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x0, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX_4_0x12345678___EDX ()
	{
		// Mov [DS:EAX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EAX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x85, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX_8_0x12345678___EDX ()
	{
		// Mov [DS:EAX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EAX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xc5, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX_1_0xC___EDX ()
	{
		// Mov [DS:EAX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EAX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x50, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX_2_0xC___EDX ()
	{
		// Mov [DS:EAX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EAX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x0, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX_4_0xC___EDX ()
	{
		// Mov [DS:EAX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EAX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x85, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX_8_0xC___EDX ()
	{
		// Mov [DS:EAX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EAX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xc5, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX_1___EDX ()
	{
		// Mov [EBX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x13};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX_2___EDX ()
	{
		// Mov [EBX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x1b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX_4___EDX ()
	{
		// Mov [EBX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x9d, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX_8___EDX ()
	{
		// Mov [EBX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xdd, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX_1_0x12345678___EDX ()
	{
		// Mov [EBX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x93, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX_2_0x12345678___EDX ()
	{
		// Mov [EBX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x1b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX_4_0x12345678___EDX ()
	{
		// Mov [EBX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x9d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX_8_0x12345678___EDX ()
	{
		// Mov [EBX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xdd, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX_1_0xC___EDX ()
	{
		// Mov [EBX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x53, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX_2_0xC___EDX ()
	{
		// Mov [EBX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x1b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX_4_0xC___EDX ()
	{
		// Mov [EBX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x9d, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX_8_0xC___EDX ()
	{
		// Mov [EBX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xdd, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX_1___EDX ()
	{
		// Mov [DS:EBX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x13};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX_2___EDX ()
	{
		// Mov [DS:EBX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x1b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX_4___EDX ()
	{
		// Mov [DS:EBX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x9d, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX_8___EDX ()
	{
		// Mov [DS:EBX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xdd, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX_1_0x12345678___EDX ()
	{
		// Mov [DS:EBX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x93, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX_2_0x12345678___EDX ()
	{
		// Mov [DS:EBX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x1b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX_4_0x12345678___EDX ()
	{
		// Mov [DS:EBX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x9d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX_8_0x12345678___EDX ()
	{
		// Mov [DS:EBX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xdd, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX_1_0xC___EDX ()
	{
		// Mov [DS:EBX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x53, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX_2_0xC___EDX ()
	{
		// Mov [DS:EBX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x1b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX_4_0xC___EDX ()
	{
		// Mov [DS:EBX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x9d, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX_8_0xC___EDX ()
	{
		// Mov [DS:EBX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xdd, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX_1___EDX ()
	{
		// Mov [ECX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ECX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x11};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX_2___EDX ()
	{
		// Mov [ECX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ECX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x9};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX_4___EDX ()
	{
		// Mov [ECX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ECX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x8d, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX_8___EDX ()
	{
		// Mov [ECX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ECX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xcd, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX_1_0x12345678___EDX ()
	{
		// Mov [ECX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ECX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x91, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX_2_0x12345678___EDX ()
	{
		// Mov [ECX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ECX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x9, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX_4_0x12345678___EDX ()
	{
		// Mov [ECX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ECX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x8d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX_8_0x12345678___EDX ()
	{
		// Mov [ECX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ECX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xcd, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX_1_0xC___EDX ()
	{
		// Mov [ECX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ECX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x51, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX_2_0xC___EDX ()
	{
		// Mov [ECX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ECX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x9, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX_4_0xC___EDX ()
	{
		// Mov [ECX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ECX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x8d, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX_8_0xC___EDX ()
	{
		// Mov [ECX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ECX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xcd, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX_1___EDX ()
	{
		// Mov [DS:ECX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ECX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x11};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX_2___EDX ()
	{
		// Mov [DS:ECX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ECX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x9};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX_4___EDX ()
	{
		// Mov [DS:ECX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ECX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x8d, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX_8___EDX ()
	{
		// Mov [DS:ECX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ECX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xcd, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX_1_0x12345678___EDX ()
	{
		// Mov [DS:ECX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ECX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x91, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX_2_0x12345678___EDX ()
	{
		// Mov [DS:ECX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ECX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x9, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX_4_0x12345678___EDX ()
	{
		// Mov [DS:ECX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ECX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x8d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX_8_0x12345678___EDX ()
	{
		// Mov [DS:ECX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ECX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xcd, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX_1_0xC___EDX ()
	{
		// Mov [DS:ECX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ECX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x51, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX_2_0xC___EDX ()
	{
		// Mov [DS:ECX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ECX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x9, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX_4_0xC___EDX ()
	{
		// Mov [DS:ECX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ECX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x8d, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX_8_0xC___EDX ()
	{
		// Mov [DS:ECX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ECX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xcd, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX_1___EDX ()
	{
		// Mov [EDX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX_2___EDX ()
	{
		// Mov [EDX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX_4___EDX ()
	{
		// Mov [EDX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x95, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX_8___EDX ()
	{
		// Mov [EDX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xd5, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX_1_0x12345678___EDX ()
	{
		// Mov [EDX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x92, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX_2_0x12345678___EDX ()
	{
		// Mov [EDX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x12, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX_4_0x12345678___EDX ()
	{
		// Mov [EDX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x95, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX_8_0x12345678___EDX ()
	{
		// Mov [EDX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xd5, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX_1_0xC___EDX ()
	{
		// Mov [EDX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x52, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX_2_0xC___EDX ()
	{
		// Mov [EDX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x12, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX_4_0xC___EDX ()
	{
		// Mov [EDX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x95, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX_8_0xC___EDX ()
	{
		// Mov [EDX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xd5, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX_1___EDX ()
	{
		// Mov [DS:EDX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX_2___EDX ()
	{
		// Mov [DS:EDX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX_4___EDX ()
	{
		// Mov [DS:EDX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x95, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX_8___EDX ()
	{
		// Mov [DS:EDX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xd5, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX_1_0x12345678___EDX ()
	{
		// Mov [DS:EDX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x92, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX_2_0x12345678___EDX ()
	{
		// Mov [DS:EDX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x12, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX_4_0x12345678___EDX ()
	{
		// Mov [DS:EDX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x95, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX_8_0x12345678___EDX ()
	{
		// Mov [DS:EDX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xd5, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX_1_0xC___EDX ()
	{
		// Mov [DS:EDX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x52, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX_2_0xC___EDX ()
	{
		// Mov [DS:EDX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x12, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX_4_0xC___EDX ()
	{
		// Mov [DS:EDX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x95, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX_8_0xC___EDX ()
	{
		// Mov [DS:EDX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xd5, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP_1___EDX ()
	{
		// Mov [ESP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ESP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x24};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP_1_0x12345678___EDX ()
	{
		// Mov [ESP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ESP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x24, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP_1_0xC___EDX ()
	{
		// Mov [ESP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ESP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x24, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP_1___EDX ()
	{
		// Mov [DS:ESP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ESP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x24};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP_1_0x12345678___EDX ()
	{
		// Mov [DS:ESP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ESP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x24, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP_1_0xC___EDX ()
	{
		// Mov [DS:ESP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ESP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x24, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP_1___EDX ()
	{
		// Mov [EBP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x55, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP_2___EDX ()
	{
		// Mov [EBP*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBP, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x2d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP_4___EDX ()
	{
		// Mov [EBP*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBP, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xad, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP_8___EDX ()
	{
		// Mov [EBP*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBP, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xed, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP_1_0x12345678___EDX ()
	{
		// Mov [EBP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x95, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP_2_0x12345678___EDX ()
	{
		// Mov [EBP*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBP, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x2d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP_4_0x12345678___EDX ()
	{
		// Mov [EBP*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBP, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xad, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP_8_0x12345678___EDX ()
	{
		// Mov [EBP*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBP, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xed, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP_1_0xC___EDX ()
	{
		// Mov [EBP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x55, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP_2_0xC___EDX ()
	{
		// Mov [EBP*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBP, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x2d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP_4_0xC___EDX ()
	{
		// Mov [EBP*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBP, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xad, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP_8_0xC___EDX ()
	{
		// Mov [EBP*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EBP, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xed, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP_1___EDX ()
	{
		// Mov [DS:EBP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x55, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP_2___EDX ()
	{
		// Mov [DS:EBP*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBP, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x2d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP_4___EDX ()
	{
		// Mov [DS:EBP*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBP, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xad, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP_8___EDX ()
	{
		// Mov [DS:EBP*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBP, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xed, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP_1_0x12345678___EDX ()
	{
		// Mov [DS:EBP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x95, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP_2_0x12345678___EDX ()
	{
		// Mov [DS:EBP*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBP, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x2d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP_4_0x12345678___EDX ()
	{
		// Mov [DS:EBP*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBP, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xad, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP_8_0x12345678___EDX ()
	{
		// Mov [DS:EBP*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBP, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xed, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP_1_0xC___EDX ()
	{
		// Mov [DS:EBP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x55, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP_2_0xC___EDX ()
	{
		// Mov [DS:EBP*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBP, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x2d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP_4_0xC___EDX ()
	{
		// Mov [DS:EBP*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBP, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xad, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP_8_0xC___EDX ()
	{
		// Mov [DS:EBP*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EBP, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xed, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI_1___EDX ()
	{
		// Mov [EDI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x17};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI_2___EDX ()
	{
		// Mov [EDI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x3f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI_4___EDX ()
	{
		// Mov [EDI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xbd, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI_8___EDX ()
	{
		// Mov [EDI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xfd, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI_1_0x12345678___EDX ()
	{
		// Mov [EDI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x97, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI_2_0x12345678___EDX ()
	{
		// Mov [EDI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x3f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI_4_0x12345678___EDX ()
	{
		// Mov [EDI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xbd, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI_8_0x12345678___EDX ()
	{
		// Mov [EDI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xfd, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI_1_0xC___EDX ()
	{
		// Mov [EDI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x57, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI_2_0xC___EDX ()
	{
		// Mov [EDI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x3f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI_4_0xC___EDX ()
	{
		// Mov [EDI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xbd, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI_8_0xC___EDX ()
	{
		// Mov [EDI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.EDI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xfd, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI_1___EDX ()
	{
		// Mov [DS:EDI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x17};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI_2___EDX ()
	{
		// Mov [DS:EDI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x3f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI_4___EDX ()
	{
		// Mov [DS:EDI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xbd, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI_8___EDX ()
	{
		// Mov [DS:EDI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xfd, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI_1_0x12345678___EDX ()
	{
		// Mov [DS:EDI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x97, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI_2_0x12345678___EDX ()
	{
		// Mov [DS:EDI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x3f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI_4_0x12345678___EDX ()
	{
		// Mov [DS:EDI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xbd, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI_8_0x12345678___EDX ()
	{
		// Mov [DS:EDI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xfd, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI_1_0xC___EDX ()
	{
		// Mov [DS:EDI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x57, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI_2_0xC___EDX ()
	{
		// Mov [DS:EDI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x3f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI_4_0xC___EDX ()
	{
		// Mov [DS:EDI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xbd, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI_8_0xC___EDX ()
	{
		// Mov [DS:EDI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.EDI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xfd, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI_1___EDX ()
	{
		// Mov [ESI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ESI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x16};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI_2___EDX ()
	{
		// Mov [ESI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ESI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x36};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI_4___EDX ()
	{
		// Mov [ESI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ESI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xb5, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI_8___EDX ()
	{
		// Mov [ESI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ESI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xf5, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI_1_0x12345678___EDX ()
	{
		// Mov [ESI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ESI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x96, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI_2_0x12345678___EDX ()
	{
		// Mov [ESI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ESI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x36, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI_4_0x12345678___EDX ()
	{
		// Mov [ESI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ESI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xb5, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI_8_0x12345678___EDX ()
	{
		// Mov [ESI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ESI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xf5, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI_1_0xC___EDX ()
	{
		// Mov [ESI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ESI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x56, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI_2_0xC___EDX ()
	{
		// Mov [ESI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ESI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x36, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI_4_0xC___EDX ()
	{
		// Mov [ESI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ESI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xb5, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI_8_0xC___EDX ()
	{
		// Mov [ESI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, null, R32.ESI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xf5, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI_1___EDX ()
	{
		// Mov [DS:ESI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ESI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x16};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI_2___EDX ()
	{
		// Mov [DS:ESI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ESI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x36};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI_4___EDX ()
	{
		// Mov [DS:ESI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ESI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xb5, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI_8___EDX ()
	{
		// Mov [DS:ESI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ESI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xf5, 0x0, 0x0, 0x0, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI_1_0x12345678___EDX ()
	{
		// Mov [DS:ESI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ESI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x96, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI_2_0x12345678___EDX ()
	{
		// Mov [DS:ESI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ESI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x36, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI_4_0x12345678___EDX ()
	{
		// Mov [DS:ESI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ESI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xb5, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI_8_0x12345678___EDX ()
	{
		// Mov [DS:ESI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ESI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xf5, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI_1_0xC___EDX ()
	{
		// Mov [DS:ESI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ESI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x56, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI_2_0xC___EDX ()
	{
		// Mov [DS:ESI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ESI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x36, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI_4_0xC___EDX ()
	{
		// Mov [DS:ESI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ESI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xb5, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI_8_0xC___EDX ()
	{
		// Mov [DS:ESI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, null, R32.ESI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xf5, 0xf4, 0xff, 0xff, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDX ()
	{
		// Mov [EAX], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, null, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x10};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX_0x12345678___EDX ()
	{
		// Mov [EAX+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, null, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x90, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX_0xC___EDX ()
	{
		// Mov [EAX-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, null, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x50, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDX ()
	{
		// Mov [DS:EAX], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, null, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x10};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX_0x12345678___EDX ()
	{
		// Mov [DS:EAX+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, null, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x90, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX_0xC___EDX ()
	{
		// Mov [DS:EAX-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, null, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x50, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EAX_1___EDX ()
	{
		// Mov [EAX + EAX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EAX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EAX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EAX_2___EDX ()
	{
		// Mov [EAX + EAX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EAX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x40};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EAX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EAX_4___EDX ()
	{
		// Mov [EAX + EAX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EAX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x80};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EAX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EAX_8___EDX ()
	{
		// Mov [EAX + EAX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EAX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xc0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EAX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EAX_1_0x12345678___EDX ()
	{
		// Mov [EAX + EAX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EAX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x0, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EAX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EAX_2_0x12345678___EDX ()
	{
		// Mov [EAX + EAX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EAX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x40, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EAX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EAX_4_0x12345678___EDX ()
	{
		// Mov [EAX + EAX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EAX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x80, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EAX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EAX_8_0x12345678___EDX ()
	{
		// Mov [EAX + EAX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EAX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xc0, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EAX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EAX_1_0xC___EDX ()
	{
		// Mov [EAX + EAX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EAX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x0, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EAX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EAX_2_0xC___EDX ()
	{
		// Mov [EAX + EAX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EAX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x40, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EAX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EAX_4_0xC___EDX ()
	{
		// Mov [EAX + EAX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EAX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x80, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EAX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EAX_8_0xC___EDX ()
	{
		// Mov [EAX + EAX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EAX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xc0, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EAX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EAX_1___EDX ()
	{
		// Mov [DS:EAX + EAX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EAX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EAX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EAX_2___EDX ()
	{
		// Mov [DS:EAX + EAX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EAX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x40};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EAX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EAX_4___EDX ()
	{
		// Mov [DS:EAX + EAX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EAX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x80};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EAX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EAX_8___EDX ()
	{
		// Mov [DS:EAX + EAX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EAX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xc0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EAX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EAX_1_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EAX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EAX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x0, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EAX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EAX_2_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EAX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EAX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x40, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EAX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EAX_4_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EAX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EAX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x80, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EAX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EAX_8_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EAX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EAX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xc0, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EAX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EAX_1_0xC___EDX ()
	{
		// Mov [DS:EAX + EAX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EAX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x0, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EAX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EAX_2_0xC___EDX ()
	{
		// Mov [DS:EAX + EAX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EAX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x40, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EAX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EAX_4_0xC___EDX ()
	{
		// Mov [DS:EAX + EAX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EAX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x80, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EAX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EAX_8_0xC___EDX ()
	{
		// Mov [DS:EAX + EAX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EAX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xc0, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EAX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBX_1___EDX ()
	{
		// Mov [EAX + EBX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x18};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBX_2___EDX ()
	{
		// Mov [EAX + EBX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x58};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBX_4___EDX ()
	{
		// Mov [EAX + EBX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x98};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBX_8___EDX ()
	{
		// Mov [EAX + EBX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xd8};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBX_1_0x12345678___EDX ()
	{
		// Mov [EAX + EBX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x18, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBX_2_0x12345678___EDX ()
	{
		// Mov [EAX + EBX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x58, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBX_4_0x12345678___EDX ()
	{
		// Mov [EAX + EBX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x98, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBX_8_0x12345678___EDX ()
	{
		// Mov [EAX + EBX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xd8, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBX_1_0xC___EDX ()
	{
		// Mov [EAX + EBX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x18, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBX_2_0xC___EDX ()
	{
		// Mov [EAX + EBX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x58, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBX_4_0xC___EDX ()
	{
		// Mov [EAX + EBX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x98, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBX_8_0xC___EDX ()
	{
		// Mov [EAX + EBX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xd8, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBX_1___EDX ()
	{
		// Mov [DS:EAX + EBX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x18};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBX_2___EDX ()
	{
		// Mov [DS:EAX + EBX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x58};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBX_4___EDX ()
	{
		// Mov [DS:EAX + EBX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x98};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBX_8___EDX ()
	{
		// Mov [DS:EAX + EBX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xd8};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBX_1_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EBX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x18, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBX_2_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EBX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x58, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBX_4_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EBX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x98, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBX_8_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EBX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xd8, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBX_1_0xC___EDX ()
	{
		// Mov [DS:EAX + EBX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x18, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBX_2_0xC___EDX ()
	{
		// Mov [DS:EAX + EBX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x58, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBX_4_0xC___EDX ()
	{
		// Mov [DS:EAX + EBX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x98, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBX_8_0xC___EDX ()
	{
		// Mov [DS:EAX + EBX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xd8, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ECX_1___EDX ()
	{
		// Mov [EAX + ECX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ECX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x8};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ECX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ECX_2___EDX ()
	{
		// Mov [EAX + ECX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ECX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x48};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ECX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ECX_4___EDX ()
	{
		// Mov [EAX + ECX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ECX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x88};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ECX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ECX_8___EDX ()
	{
		// Mov [EAX + ECX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ECX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xc8};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ECX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ECX_1_0x12345678___EDX ()
	{
		// Mov [EAX + ECX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ECX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x8, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ECX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ECX_2_0x12345678___EDX ()
	{
		// Mov [EAX + ECX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ECX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x48, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ECX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ECX_4_0x12345678___EDX ()
	{
		// Mov [EAX + ECX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ECX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x88, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ECX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ECX_8_0x12345678___EDX ()
	{
		// Mov [EAX + ECX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ECX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xc8, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ECX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ECX_1_0xC___EDX ()
	{
		// Mov [EAX + ECX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ECX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x8, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ECX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ECX_2_0xC___EDX ()
	{
		// Mov [EAX + ECX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ECX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x48, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ECX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ECX_4_0xC___EDX ()
	{
		// Mov [EAX + ECX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ECX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x88, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ECX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ECX_8_0xC___EDX ()
	{
		// Mov [EAX + ECX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ECX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xc8, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ECX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ECX_1___EDX ()
	{
		// Mov [DS:EAX + ECX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ECX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x8};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ECX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ECX_2___EDX ()
	{
		// Mov [DS:EAX + ECX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ECX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x48};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ECX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ECX_4___EDX ()
	{
		// Mov [DS:EAX + ECX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ECX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x88};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ECX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ECX_8___EDX ()
	{
		// Mov [DS:EAX + ECX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ECX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xc8};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ECX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ECX_1_0x12345678___EDX ()
	{
		// Mov [DS:EAX + ECX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ECX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x8, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ECX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ECX_2_0x12345678___EDX ()
	{
		// Mov [DS:EAX + ECX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ECX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x48, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ECX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ECX_4_0x12345678___EDX ()
	{
		// Mov [DS:EAX + ECX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ECX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x88, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ECX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ECX_8_0x12345678___EDX ()
	{
		// Mov [DS:EAX + ECX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ECX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xc8, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ECX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ECX_1_0xC___EDX ()
	{
		// Mov [DS:EAX + ECX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ECX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x8, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ECX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ECX_2_0xC___EDX ()
	{
		// Mov [DS:EAX + ECX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ECX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x48, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ECX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ECX_4_0xC___EDX ()
	{
		// Mov [DS:EAX + ECX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ECX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x88, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ECX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ECX_8_0xC___EDX ()
	{
		// Mov [DS:EAX + ECX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ECX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xc8, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ECX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDX_1___EDX ()
	{
		// Mov [EAX + EDX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x10};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDX_2___EDX ()
	{
		// Mov [EAX + EDX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x50};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDX_4___EDX ()
	{
		// Mov [EAX + EDX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x90};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDX_8___EDX ()
	{
		// Mov [EAX + EDX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xd0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDX_1_0x12345678___EDX ()
	{
		// Mov [EAX + EDX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x10, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDX_2_0x12345678___EDX ()
	{
		// Mov [EAX + EDX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x50, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDX_4_0x12345678___EDX ()
	{
		// Mov [EAX + EDX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x90, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDX_8_0x12345678___EDX ()
	{
		// Mov [EAX + EDX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xd0, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDX_1_0xC___EDX ()
	{
		// Mov [EAX + EDX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x10, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDX_2_0xC___EDX ()
	{
		// Mov [EAX + EDX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x50, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDX_4_0xC___EDX ()
	{
		// Mov [EAX + EDX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x90, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDX_8_0xC___EDX ()
	{
		// Mov [EAX + EDX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xd0, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDX_1___EDX ()
	{
		// Mov [DS:EAX + EDX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x10};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDX_2___EDX ()
	{
		// Mov [DS:EAX + EDX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x50};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDX_4___EDX ()
	{
		// Mov [DS:EAX + EDX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x90};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDX_8___EDX ()
	{
		// Mov [DS:EAX + EDX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xd0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDX_1_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EDX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x10, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDX_2_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EDX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x50, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDX_4_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EDX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x90, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDX_8_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EDX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xd0, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDX_1_0xC___EDX ()
	{
		// Mov [DS:EAX + EDX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x10, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDX_2_0xC___EDX ()
	{
		// Mov [DS:EAX + EDX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x50, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDX_4_0xC___EDX ()
	{
		// Mov [DS:EAX + EDX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x90, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDX_8_0xC___EDX ()
	{
		// Mov [DS:EAX + EDX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xd0, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ESP_1___EDX ()
	{
		// Mov [EAX + ESP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ESP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ESP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ESP_1_0x12345678___EDX ()
	{
		// Mov [EAX + ESP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ESP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x4, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ESP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ESP_1_0xC___EDX ()
	{
		// Mov [EAX + ESP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ESP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x4, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ESP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ESP_1___EDX ()
	{
		// Mov [DS:EAX + ESP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ESP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ESP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ESP_1_0x12345678___EDX ()
	{
		// Mov [DS:EAX + ESP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ESP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x4, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ESP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ESP_1_0xC___EDX ()
	{
		// Mov [DS:EAX + ESP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ESP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x4, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ESP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBP_1___EDX ()
	{
		// Mov [EAX + EBP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x28};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBP_2___EDX ()
	{
		// Mov [EAX + EBP*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBP, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x68};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBP*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBP_4___EDX ()
	{
		// Mov [EAX + EBP*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBP, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xa8};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBP*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBP_8___EDX ()
	{
		// Mov [EAX + EBP*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBP, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xe8};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBP*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBP_1_0x12345678___EDX ()
	{
		// Mov [EAX + EBP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x28, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBP_2_0x12345678___EDX ()
	{
		// Mov [EAX + EBP*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBP, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x68, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBP*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBP_4_0x12345678___EDX ()
	{
		// Mov [EAX + EBP*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBP, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xa8, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBP*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBP_8_0x12345678___EDX ()
	{
		// Mov [EAX + EBP*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBP, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xe8, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBP*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBP_1_0xC___EDX ()
	{
		// Mov [EAX + EBP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x28, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBP_2_0xC___EDX ()
	{
		// Mov [EAX + EBP*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBP, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x68, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBP*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBP_4_0xC___EDX ()
	{
		// Mov [EAX + EBP*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBP, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xa8, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBP*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EBP_8_0xC___EDX ()
	{
		// Mov [EAX + EBP*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EBP, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xe8, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EBP*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBP_1___EDX ()
	{
		// Mov [DS:EAX + EBP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x28};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBP_2___EDX ()
	{
		// Mov [DS:EAX + EBP*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBP, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x68};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBP*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBP_4___EDX ()
	{
		// Mov [DS:EAX + EBP*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBP, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xa8};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBP*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBP_8___EDX ()
	{
		// Mov [DS:EAX + EBP*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBP, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xe8};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBP*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBP_1_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EBP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x28, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBP_2_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EBP*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBP, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x68, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBP*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBP_4_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EBP*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBP, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xa8, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBP*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBP_8_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EBP*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBP, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xe8, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBP*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBP_1_0xC___EDX ()
	{
		// Mov [DS:EAX + EBP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x28, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBP_2_0xC___EDX ()
	{
		// Mov [DS:EAX + EBP*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBP, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x68, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBP*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBP_4_0xC___EDX ()
	{
		// Mov [DS:EAX + EBP*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBP, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xa8, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBP*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EBP_8_0xC___EDX ()
	{
		// Mov [DS:EAX + EBP*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EBP, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xe8, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EBP*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDI_1___EDX ()
	{
		// Mov [EAX + EDI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x38};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDI_2___EDX ()
	{
		// Mov [EAX + EDI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x78};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDI_4___EDX ()
	{
		// Mov [EAX + EDI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xb8};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDI_8___EDX ()
	{
		// Mov [EAX + EDI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xf8};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDI_1_0x12345678___EDX ()
	{
		// Mov [EAX + EDI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x38, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDI_2_0x12345678___EDX ()
	{
		// Mov [EAX + EDI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x78, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDI_4_0x12345678___EDX ()
	{
		// Mov [EAX + EDI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xb8, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDI_8_0x12345678___EDX ()
	{
		// Mov [EAX + EDI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xf8, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDI_1_0xC___EDX ()
	{
		// Mov [EAX + EDI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x38, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDI_2_0xC___EDX ()
	{
		// Mov [EAX + EDI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x78, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDI_4_0xC___EDX ()
	{
		// Mov [EAX + EDI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xb8, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___EDI_8_0xC___EDX ()
	{
		// Mov [EAX + EDI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.EDI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xf8, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + EDI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDI_1___EDX ()
	{
		// Mov [DS:EAX + EDI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x38};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDI_2___EDX ()
	{
		// Mov [DS:EAX + EDI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x78};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDI_4___EDX ()
	{
		// Mov [DS:EAX + EDI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xb8};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDI_8___EDX ()
	{
		// Mov [DS:EAX + EDI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xf8};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDI_1_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EDI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x38, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDI_2_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EDI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x78, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDI_4_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EDI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xb8, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDI_8_0x12345678___EDX ()
	{
		// Mov [DS:EAX + EDI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xf8, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDI_1_0xC___EDX ()
	{
		// Mov [DS:EAX + EDI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x38, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDI_2_0xC___EDX ()
	{
		// Mov [DS:EAX + EDI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x78, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDI_4_0xC___EDX ()
	{
		// Mov [DS:EAX + EDI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xb8, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___EDI_8_0xC___EDX ()
	{
		// Mov [DS:EAX + EDI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.EDI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xf8, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + EDI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ESI_1___EDX ()
	{
		// Mov [EAX + ESI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ESI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x30};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ESI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ESI_2___EDX ()
	{
		// Mov [EAX + ESI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ESI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x70};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ESI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ESI_4___EDX ()
	{
		// Mov [EAX + ESI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ESI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xb0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ESI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ESI_8___EDX ()
	{
		// Mov [EAX + ESI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ESI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xf0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ESI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ESI_1_0x12345678___EDX ()
	{
		// Mov [EAX + ESI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ESI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x30, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ESI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ESI_2_0x12345678___EDX ()
	{
		// Mov [EAX + ESI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ESI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x70, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ESI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ESI_4_0x12345678___EDX ()
	{
		// Mov [EAX + ESI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ESI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xb0, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ESI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ESI_8_0x12345678___EDX ()
	{
		// Mov [EAX + ESI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ESI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xf0, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ESI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ESI_1_0xC___EDX ()
	{
		// Mov [EAX + ESI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ESI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x30, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ESI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ESI_2_0xC___EDX ()
	{
		// Mov [EAX + ESI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ESI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x70, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ESI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ESI_4_0xC___EDX ()
	{
		// Mov [EAX + ESI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ESI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xb0, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ESI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EAX___ESI_8_0xC___EDX ()
	{
		// Mov [EAX + ESI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EAX, R32.ESI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xf0, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EAX + ESI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ESI_1___EDX ()
	{
		// Mov [DS:EAX + ESI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ESI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x30};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ESI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ESI_2___EDX ()
	{
		// Mov [DS:EAX + ESI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ESI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x70};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ESI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ESI_4___EDX ()
	{
		// Mov [DS:EAX + ESI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ESI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xb0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ESI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ESI_8___EDX ()
	{
		// Mov [DS:EAX + ESI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ESI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xf0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ESI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ESI_1_0x12345678___EDX ()
	{
		// Mov [DS:EAX + ESI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ESI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x30, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ESI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ESI_2_0x12345678___EDX ()
	{
		// Mov [DS:EAX + ESI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ESI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x70, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ESI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ESI_4_0x12345678___EDX ()
	{
		// Mov [DS:EAX + ESI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ESI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xb0, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ESI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ESI_8_0x12345678___EDX ()
	{
		// Mov [DS:EAX + ESI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ESI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xf0, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ESI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ESI_1_0xC___EDX ()
	{
		// Mov [DS:EAX + ESI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ESI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x30, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ESI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ESI_2_0xC___EDX ()
	{
		// Mov [DS:EAX + ESI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ESI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x70, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ESI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ESI_4_0xC___EDX ()
	{
		// Mov [DS:EAX + ESI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ESI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xb0, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ESI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EAX___ESI_8_0xC___EDX ()
	{
		// Mov [DS:EAX + ESI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EAX, R32.ESI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xf0, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EAX + ESI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDX ()
	{
		// Mov [EBX], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, null, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x13};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX_0x12345678___EDX ()
	{
		// Mov [EBX+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, null, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x93, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX_0xC___EDX ()
	{
		// Mov [EBX-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, null, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x53, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDX ()
	{
		// Mov [DS:EBX], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, null, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x13};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX_0x12345678___EDX ()
	{
		// Mov [DS:EBX+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, null, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x93, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX_0xC___EDX ()
	{
		// Mov [DS:EBX-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, null, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x53, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EAX_1___EDX ()
	{
		// Mov [EBX + EAX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EAX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x3};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EAX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EAX_2___EDX ()
	{
		// Mov [EBX + EAX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EAX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x43};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EAX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EAX_4___EDX ()
	{
		// Mov [EBX + EAX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EAX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x83};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EAX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EAX_8___EDX ()
	{
		// Mov [EBX + EAX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EAX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xc3};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EAX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EAX_1_0x12345678___EDX ()
	{
		// Mov [EBX + EAX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EAX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x3, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EAX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EAX_2_0x12345678___EDX ()
	{
		// Mov [EBX + EAX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EAX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x43, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EAX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EAX_4_0x12345678___EDX ()
	{
		// Mov [EBX + EAX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EAX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x83, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EAX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EAX_8_0x12345678___EDX ()
	{
		// Mov [EBX + EAX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EAX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xc3, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EAX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EAX_1_0xC___EDX ()
	{
		// Mov [EBX + EAX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EAX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x3, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EAX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EAX_2_0xC___EDX ()
	{
		// Mov [EBX + EAX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EAX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x43, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EAX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EAX_4_0xC___EDX ()
	{
		// Mov [EBX + EAX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EAX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x83, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EAX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EAX_8_0xC___EDX ()
	{
		// Mov [EBX + EAX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EAX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xc3, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EAX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EAX_1___EDX ()
	{
		// Mov [DS:EBX + EAX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EAX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x3};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EAX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EAX_2___EDX ()
	{
		// Mov [DS:EBX + EAX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EAX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x43};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EAX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EAX_4___EDX ()
	{
		// Mov [DS:EBX + EAX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EAX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x83};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EAX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EAX_8___EDX ()
	{
		// Mov [DS:EBX + EAX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EAX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xc3};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EAX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EAX_1_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EAX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EAX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x3, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EAX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EAX_2_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EAX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EAX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x43, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EAX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EAX_4_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EAX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EAX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x83, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EAX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EAX_8_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EAX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EAX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xc3, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EAX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EAX_1_0xC___EDX ()
	{
		// Mov [DS:EBX + EAX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EAX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x3, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EAX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EAX_2_0xC___EDX ()
	{
		// Mov [DS:EBX + EAX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EAX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x43, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EAX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EAX_4_0xC___EDX ()
	{
		// Mov [DS:EBX + EAX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EAX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x83, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EAX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EAX_8_0xC___EDX ()
	{
		// Mov [DS:EBX + EAX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EAX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xc3, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EAX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBX_1___EDX ()
	{
		// Mov [EBX + EBX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x1b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBX_2___EDX ()
	{
		// Mov [EBX + EBX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x5b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBX_4___EDX ()
	{
		// Mov [EBX + EBX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x9b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBX_8___EDX ()
	{
		// Mov [EBX + EBX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xdb};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBX_1_0x12345678___EDX ()
	{
		// Mov [EBX + EBX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x1b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBX_2_0x12345678___EDX ()
	{
		// Mov [EBX + EBX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x5b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBX_4_0x12345678___EDX ()
	{
		// Mov [EBX + EBX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x9b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBX_8_0x12345678___EDX ()
	{
		// Mov [EBX + EBX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xdb, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBX_1_0xC___EDX ()
	{
		// Mov [EBX + EBX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x1b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBX_2_0xC___EDX ()
	{
		// Mov [EBX + EBX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x5b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBX_4_0xC___EDX ()
	{
		// Mov [EBX + EBX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x9b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBX_8_0xC___EDX ()
	{
		// Mov [EBX + EBX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xdb, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBX_1___EDX ()
	{
		// Mov [DS:EBX + EBX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x1b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBX_2___EDX ()
	{
		// Mov [DS:EBX + EBX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x5b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBX_4___EDX ()
	{
		// Mov [DS:EBX + EBX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x9b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBX_8___EDX ()
	{
		// Mov [DS:EBX + EBX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xdb};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBX_1_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EBX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x1b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBX_2_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EBX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x5b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBX_4_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EBX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x9b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBX_8_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EBX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xdb, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBX_1_0xC___EDX ()
	{
		// Mov [DS:EBX + EBX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x1b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBX_2_0xC___EDX ()
	{
		// Mov [DS:EBX + EBX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x5b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBX_4_0xC___EDX ()
	{
		// Mov [DS:EBX + EBX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x9b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBX_8_0xC___EDX ()
	{
		// Mov [DS:EBX + EBX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xdb, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ECX_1___EDX ()
	{
		// Mov [EBX + ECX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ECX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xb};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ECX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ECX_2___EDX ()
	{
		// Mov [EBX + ECX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ECX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x4b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ECX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ECX_4___EDX ()
	{
		// Mov [EBX + ECX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ECX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x8b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ECX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ECX_8___EDX ()
	{
		// Mov [EBX + ECX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ECX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xcb};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ECX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ECX_1_0x12345678___EDX ()
	{
		// Mov [EBX + ECX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ECX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xb, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ECX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ECX_2_0x12345678___EDX ()
	{
		// Mov [EBX + ECX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ECX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x4b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ECX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ECX_4_0x12345678___EDX ()
	{
		// Mov [EBX + ECX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ECX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x8b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ECX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ECX_8_0x12345678___EDX ()
	{
		// Mov [EBX + ECX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ECX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xcb, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ECX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ECX_1_0xC___EDX ()
	{
		// Mov [EBX + ECX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ECX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xb, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ECX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ECX_2_0xC___EDX ()
	{
		// Mov [EBX + ECX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ECX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x4b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ECX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ECX_4_0xC___EDX ()
	{
		// Mov [EBX + ECX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ECX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x8b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ECX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ECX_8_0xC___EDX ()
	{
		// Mov [EBX + ECX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ECX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xcb, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ECX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ECX_1___EDX ()
	{
		// Mov [DS:EBX + ECX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ECX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xb};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ECX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ECX_2___EDX ()
	{
		// Mov [DS:EBX + ECX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ECX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x4b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ECX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ECX_4___EDX ()
	{
		// Mov [DS:EBX + ECX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ECX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x8b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ECX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ECX_8___EDX ()
	{
		// Mov [DS:EBX + ECX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ECX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xcb};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ECX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ECX_1_0x12345678___EDX ()
	{
		// Mov [DS:EBX + ECX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ECX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xb, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ECX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ECX_2_0x12345678___EDX ()
	{
		// Mov [DS:EBX + ECX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ECX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x4b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ECX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ECX_4_0x12345678___EDX ()
	{
		// Mov [DS:EBX + ECX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ECX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x8b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ECX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ECX_8_0x12345678___EDX ()
	{
		// Mov [DS:EBX + ECX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ECX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xcb, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ECX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ECX_1_0xC___EDX ()
	{
		// Mov [DS:EBX + ECX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ECX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xb, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ECX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ECX_2_0xC___EDX ()
	{
		// Mov [DS:EBX + ECX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ECX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x4b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ECX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ECX_4_0xC___EDX ()
	{
		// Mov [DS:EBX + ECX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ECX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x8b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ECX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ECX_8_0xC___EDX ()
	{
		// Mov [DS:EBX + ECX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ECX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xcb, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ECX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDX_1___EDX ()
	{
		// Mov [EBX + EDX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x13};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDX_2___EDX ()
	{
		// Mov [EBX + EDX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x53};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDX_4___EDX ()
	{
		// Mov [EBX + EDX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x93};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDX_8___EDX ()
	{
		// Mov [EBX + EDX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xd3};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDX_1_0x12345678___EDX ()
	{
		// Mov [EBX + EDX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x13, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDX_2_0x12345678___EDX ()
	{
		// Mov [EBX + EDX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x53, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDX_4_0x12345678___EDX ()
	{
		// Mov [EBX + EDX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x93, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDX_8_0x12345678___EDX ()
	{
		// Mov [EBX + EDX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xd3, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDX_1_0xC___EDX ()
	{
		// Mov [EBX + EDX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x13, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDX_2_0xC___EDX ()
	{
		// Mov [EBX + EDX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x53, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDX_4_0xC___EDX ()
	{
		// Mov [EBX + EDX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x93, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDX_8_0xC___EDX ()
	{
		// Mov [EBX + EDX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xd3, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDX_1___EDX ()
	{
		// Mov [DS:EBX + EDX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x13};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDX_2___EDX ()
	{
		// Mov [DS:EBX + EDX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x53};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDX_4___EDX ()
	{
		// Mov [DS:EBX + EDX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x93};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDX_8___EDX ()
	{
		// Mov [DS:EBX + EDX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xd3};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDX_1_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EDX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x13, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDX_2_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EDX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x53, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDX_4_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EDX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x93, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDX_8_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EDX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xd3, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDX_1_0xC___EDX ()
	{
		// Mov [DS:EBX + EDX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x13, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDX_2_0xC___EDX ()
	{
		// Mov [DS:EBX + EDX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x53, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDX_4_0xC___EDX ()
	{
		// Mov [DS:EBX + EDX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x93, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDX_8_0xC___EDX ()
	{
		// Mov [DS:EBX + EDX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xd3, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ESP_1___EDX ()
	{
		// Mov [EBX + ESP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ESP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x1c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ESP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ESP_1_0x12345678___EDX ()
	{
		// Mov [EBX + ESP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ESP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x1c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ESP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ESP_1_0xC___EDX ()
	{
		// Mov [EBX + ESP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ESP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x1c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ESP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ESP_1___EDX ()
	{
		// Mov [DS:EBX + ESP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ESP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x1c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ESP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ESP_1_0x12345678___EDX ()
	{
		// Mov [DS:EBX + ESP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ESP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x1c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ESP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ESP_1_0xC___EDX ()
	{
		// Mov [DS:EBX + ESP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ESP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x1c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ESP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBP_1___EDX ()
	{
		// Mov [EBX + EBP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x2b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBP_2___EDX ()
	{
		// Mov [EBX + EBP*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBP, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x6b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBP*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBP_4___EDX ()
	{
		// Mov [EBX + EBP*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBP, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xab};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBP*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBP_8___EDX ()
	{
		// Mov [EBX + EBP*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBP, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xeb};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBP*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBP_1_0x12345678___EDX ()
	{
		// Mov [EBX + EBP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x2b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBP_2_0x12345678___EDX ()
	{
		// Mov [EBX + EBP*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBP, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x6b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBP*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBP_4_0x12345678___EDX ()
	{
		// Mov [EBX + EBP*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBP, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xab, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBP*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBP_8_0x12345678___EDX ()
	{
		// Mov [EBX + EBP*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBP, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xeb, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBP*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBP_1_0xC___EDX ()
	{
		// Mov [EBX + EBP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x2b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBP_2_0xC___EDX ()
	{
		// Mov [EBX + EBP*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBP, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x6b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBP*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBP_4_0xC___EDX ()
	{
		// Mov [EBX + EBP*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBP, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xab, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBP*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EBP_8_0xC___EDX ()
	{
		// Mov [EBX + EBP*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EBP, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xeb, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EBP*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBP_1___EDX ()
	{
		// Mov [DS:EBX + EBP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x2b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBP_2___EDX ()
	{
		// Mov [DS:EBX + EBP*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBP, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x6b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBP*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBP_4___EDX ()
	{
		// Mov [DS:EBX + EBP*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBP, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xab};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBP*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBP_8___EDX ()
	{
		// Mov [DS:EBX + EBP*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBP, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xeb};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBP*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBP_1_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EBP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x2b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBP_2_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EBP*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBP, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x6b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBP*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBP_4_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EBP*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBP, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xab, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBP*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBP_8_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EBP*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBP, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xeb, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBP*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBP_1_0xC___EDX ()
	{
		// Mov [DS:EBX + EBP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x2b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBP_2_0xC___EDX ()
	{
		// Mov [DS:EBX + EBP*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBP, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x6b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBP*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBP_4_0xC___EDX ()
	{
		// Mov [DS:EBX + EBP*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBP, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xab, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBP*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EBP_8_0xC___EDX ()
	{
		// Mov [DS:EBX + EBP*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EBP, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xeb, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EBP*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDI_1___EDX ()
	{
		// Mov [EBX + EDI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x3b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDI_2___EDX ()
	{
		// Mov [EBX + EDI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x7b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDI_4___EDX ()
	{
		// Mov [EBX + EDI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xbb};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDI_8___EDX ()
	{
		// Mov [EBX + EDI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xfb};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDI_1_0x12345678___EDX ()
	{
		// Mov [EBX + EDI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x3b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDI_2_0x12345678___EDX ()
	{
		// Mov [EBX + EDI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x7b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDI_4_0x12345678___EDX ()
	{
		// Mov [EBX + EDI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xbb, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDI_8_0x12345678___EDX ()
	{
		// Mov [EBX + EDI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xfb, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDI_1_0xC___EDX ()
	{
		// Mov [EBX + EDI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x3b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDI_2_0xC___EDX ()
	{
		// Mov [EBX + EDI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x7b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDI_4_0xC___EDX ()
	{
		// Mov [EBX + EDI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xbb, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___EDI_8_0xC___EDX ()
	{
		// Mov [EBX + EDI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.EDI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xfb, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + EDI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDI_1___EDX ()
	{
		// Mov [DS:EBX + EDI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x3b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDI_2___EDX ()
	{
		// Mov [DS:EBX + EDI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x7b};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDI_4___EDX ()
	{
		// Mov [DS:EBX + EDI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xbb};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDI_8___EDX ()
	{
		// Mov [DS:EBX + EDI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xfb};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDI_1_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EDI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x3b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDI_2_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EDI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x7b, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDI_4_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EDI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xbb, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDI_8_0x12345678___EDX ()
	{
		// Mov [DS:EBX + EDI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xfb, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDI_1_0xC___EDX ()
	{
		// Mov [DS:EBX + EDI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x3b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDI_2_0xC___EDX ()
	{
		// Mov [DS:EBX + EDI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x7b, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDI_4_0xC___EDX ()
	{
		// Mov [DS:EBX + EDI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xbb, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___EDI_8_0xC___EDX ()
	{
		// Mov [DS:EBX + EDI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.EDI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xfb, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + EDI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ESI_1___EDX ()
	{
		// Mov [EBX + ESI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ESI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x33};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ESI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ESI_2___EDX ()
	{
		// Mov [EBX + ESI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ESI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x73};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ESI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ESI_4___EDX ()
	{
		// Mov [EBX + ESI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ESI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xb3};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ESI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ESI_8___EDX ()
	{
		// Mov [EBX + ESI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ESI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xf3};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ESI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ESI_1_0x12345678___EDX ()
	{
		// Mov [EBX + ESI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ESI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x33, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ESI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ESI_2_0x12345678___EDX ()
	{
		// Mov [EBX + ESI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ESI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x73, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ESI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ESI_4_0x12345678___EDX ()
	{
		// Mov [EBX + ESI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ESI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xb3, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ESI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ESI_8_0x12345678___EDX ()
	{
		// Mov [EBX + ESI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ESI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xf3, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ESI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ESI_1_0xC___EDX ()
	{
		// Mov [EBX + ESI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ESI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x33, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ESI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ESI_2_0xC___EDX ()
	{
		// Mov [EBX + ESI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ESI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x73, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ESI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ESI_4_0xC___EDX ()
	{
		// Mov [EBX + ESI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ESI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xb3, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ESI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBX___ESI_8_0xC___EDX ()
	{
		// Mov [EBX + ESI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBX, R32.ESI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xf3, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBX + ESI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ESI_1___EDX ()
	{
		// Mov [DS:EBX + ESI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ESI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x33};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ESI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ESI_2___EDX ()
	{
		// Mov [DS:EBX + ESI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ESI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x73};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ESI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ESI_4___EDX ()
	{
		// Mov [DS:EBX + ESI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ESI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xb3};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ESI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ESI_8___EDX ()
	{
		// Mov [DS:EBX + ESI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ESI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xf3};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ESI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ESI_1_0x12345678___EDX ()
	{
		// Mov [DS:EBX + ESI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ESI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x33, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ESI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ESI_2_0x12345678___EDX ()
	{
		// Mov [DS:EBX + ESI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ESI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x73, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ESI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ESI_4_0x12345678___EDX ()
	{
		// Mov [DS:EBX + ESI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ESI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xb3, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ESI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ESI_8_0x12345678___EDX ()
	{
		// Mov [DS:EBX + ESI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ESI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xf3, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ESI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ESI_1_0xC___EDX ()
	{
		// Mov [DS:EBX + ESI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ESI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x33, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ESI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ESI_2_0xC___EDX ()
	{
		// Mov [DS:EBX + ESI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ESI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x73, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ESI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ESI_4_0xC___EDX ()
	{
		// Mov [DS:EBX + ESI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ESI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xb3, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ESI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBX___ESI_8_0xC___EDX ()
	{
		// Mov [DS:EBX + ESI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBX, R32.ESI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xf3, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBX + ESI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDX ()
	{
		// Mov [ECX], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, null, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x11};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX_0x12345678___EDX ()
	{
		// Mov [ECX+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, null, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x91, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX_0xC___EDX ()
	{
		// Mov [ECX-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, null, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x51, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDX ()
	{
		// Mov [DS:ECX], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, null, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x11};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX_0x12345678___EDX ()
	{
		// Mov [DS:ECX+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, null, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x91, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX_0xC___EDX ()
	{
		// Mov [DS:ECX-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, null, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x51, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EAX_1___EDX ()
	{
		// Mov [ECX + EAX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EAX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x1};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EAX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EAX_2___EDX ()
	{
		// Mov [ECX + EAX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EAX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x41};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EAX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EAX_4___EDX ()
	{
		// Mov [ECX + EAX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EAX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x81};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EAX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EAX_8___EDX ()
	{
		// Mov [ECX + EAX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EAX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xc1};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EAX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EAX_1_0x12345678___EDX ()
	{
		// Mov [ECX + EAX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EAX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x1, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EAX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EAX_2_0x12345678___EDX ()
	{
		// Mov [ECX + EAX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EAX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x41, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EAX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EAX_4_0x12345678___EDX ()
	{
		// Mov [ECX + EAX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EAX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x81, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EAX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EAX_8_0x12345678___EDX ()
	{
		// Mov [ECX + EAX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EAX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xc1, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EAX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EAX_1_0xC___EDX ()
	{
		// Mov [ECX + EAX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EAX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x1, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EAX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EAX_2_0xC___EDX ()
	{
		// Mov [ECX + EAX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EAX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x41, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EAX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EAX_4_0xC___EDX ()
	{
		// Mov [ECX + EAX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EAX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x81, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EAX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EAX_8_0xC___EDX ()
	{
		// Mov [ECX + EAX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EAX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xc1, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EAX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EAX_1___EDX ()
	{
		// Mov [DS:ECX + EAX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EAX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x1};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EAX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EAX_2___EDX ()
	{
		// Mov [DS:ECX + EAX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EAX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x41};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EAX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EAX_4___EDX ()
	{
		// Mov [DS:ECX + EAX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EAX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x81};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EAX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EAX_8___EDX ()
	{
		// Mov [DS:ECX + EAX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EAX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xc1};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EAX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EAX_1_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EAX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EAX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x1, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EAX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EAX_2_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EAX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EAX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x41, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EAX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EAX_4_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EAX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EAX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x81, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EAX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EAX_8_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EAX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EAX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xc1, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EAX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EAX_1_0xC___EDX ()
	{
		// Mov [DS:ECX + EAX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EAX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x1, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EAX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EAX_2_0xC___EDX ()
	{
		// Mov [DS:ECX + EAX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EAX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x41, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EAX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EAX_4_0xC___EDX ()
	{
		// Mov [DS:ECX + EAX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EAX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x81, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EAX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EAX_8_0xC___EDX ()
	{
		// Mov [DS:ECX + EAX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EAX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xc1, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EAX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBX_1___EDX ()
	{
		// Mov [ECX + EBX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x19};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBX_2___EDX ()
	{
		// Mov [ECX + EBX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x59};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBX_4___EDX ()
	{
		// Mov [ECX + EBX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x99};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBX_8___EDX ()
	{
		// Mov [ECX + EBX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xd9};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBX_1_0x12345678___EDX ()
	{
		// Mov [ECX + EBX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x19, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBX_2_0x12345678___EDX ()
	{
		// Mov [ECX + EBX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x59, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBX_4_0x12345678___EDX ()
	{
		// Mov [ECX + EBX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x99, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBX_8_0x12345678___EDX ()
	{
		// Mov [ECX + EBX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xd9, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBX_1_0xC___EDX ()
	{
		// Mov [ECX + EBX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x19, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBX_2_0xC___EDX ()
	{
		// Mov [ECX + EBX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x59, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBX_4_0xC___EDX ()
	{
		// Mov [ECX + EBX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x99, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBX_8_0xC___EDX ()
	{
		// Mov [ECX + EBX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xd9, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBX_1___EDX ()
	{
		// Mov [DS:ECX + EBX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x19};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBX_2___EDX ()
	{
		// Mov [DS:ECX + EBX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x59};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBX_4___EDX ()
	{
		// Mov [DS:ECX + EBX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x99};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBX_8___EDX ()
	{
		// Mov [DS:ECX + EBX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xd9};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBX_1_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EBX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x19, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBX_2_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EBX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x59, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBX_4_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EBX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x99, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBX_8_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EBX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xd9, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBX_1_0xC___EDX ()
	{
		// Mov [DS:ECX + EBX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x19, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBX_2_0xC___EDX ()
	{
		// Mov [DS:ECX + EBX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x59, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBX_4_0xC___EDX ()
	{
		// Mov [DS:ECX + EBX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x99, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBX_8_0xC___EDX ()
	{
		// Mov [DS:ECX + EBX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xd9, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ECX_1___EDX ()
	{
		// Mov [ECX + ECX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ECX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x9};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ECX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ECX_2___EDX ()
	{
		// Mov [ECX + ECX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ECX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x49};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ECX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ECX_4___EDX ()
	{
		// Mov [ECX + ECX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ECX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x89};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ECX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ECX_8___EDX ()
	{
		// Mov [ECX + ECX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ECX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xc9};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ECX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ECX_1_0x12345678___EDX ()
	{
		// Mov [ECX + ECX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ECX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x9, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ECX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ECX_2_0x12345678___EDX ()
	{
		// Mov [ECX + ECX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ECX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x49, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ECX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ECX_4_0x12345678___EDX ()
	{
		// Mov [ECX + ECX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ECX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x89, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ECX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ECX_8_0x12345678___EDX ()
	{
		// Mov [ECX + ECX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ECX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xc9, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ECX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ECX_1_0xC___EDX ()
	{
		// Mov [ECX + ECX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ECX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x9, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ECX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ECX_2_0xC___EDX ()
	{
		// Mov [ECX + ECX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ECX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x49, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ECX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ECX_4_0xC___EDX ()
	{
		// Mov [ECX + ECX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ECX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x89, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ECX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ECX_8_0xC___EDX ()
	{
		// Mov [ECX + ECX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ECX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xc9, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ECX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ECX_1___EDX ()
	{
		// Mov [DS:ECX + ECX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ECX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x9};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ECX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ECX_2___EDX ()
	{
		// Mov [DS:ECX + ECX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ECX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x49};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ECX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ECX_4___EDX ()
	{
		// Mov [DS:ECX + ECX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ECX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x89};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ECX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ECX_8___EDX ()
	{
		// Mov [DS:ECX + ECX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ECX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xc9};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ECX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ECX_1_0x12345678___EDX ()
	{
		// Mov [DS:ECX + ECX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ECX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x9, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ECX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ECX_2_0x12345678___EDX ()
	{
		// Mov [DS:ECX + ECX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ECX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x49, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ECX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ECX_4_0x12345678___EDX ()
	{
		// Mov [DS:ECX + ECX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ECX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x89, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ECX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ECX_8_0x12345678___EDX ()
	{
		// Mov [DS:ECX + ECX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ECX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xc9, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ECX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ECX_1_0xC___EDX ()
	{
		// Mov [DS:ECX + ECX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ECX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x9, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ECX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ECX_2_0xC___EDX ()
	{
		// Mov [DS:ECX + ECX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ECX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x49, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ECX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ECX_4_0xC___EDX ()
	{
		// Mov [DS:ECX + ECX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ECX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x89, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ECX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ECX_8_0xC___EDX ()
	{
		// Mov [DS:ECX + ECX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ECX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xc9, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ECX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDX_1___EDX ()
	{
		// Mov [ECX + EDX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x11};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDX_2___EDX ()
	{
		// Mov [ECX + EDX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x51};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDX_4___EDX ()
	{
		// Mov [ECX + EDX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x91};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDX_8___EDX ()
	{
		// Mov [ECX + EDX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xd1};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDX_1_0x12345678___EDX ()
	{
		// Mov [ECX + EDX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x11, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDX_2_0x12345678___EDX ()
	{
		// Mov [ECX + EDX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x51, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDX_4_0x12345678___EDX ()
	{
		// Mov [ECX + EDX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x91, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDX_8_0x12345678___EDX ()
	{
		// Mov [ECX + EDX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xd1, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDX_1_0xC___EDX ()
	{
		// Mov [ECX + EDX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x11, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDX_2_0xC___EDX ()
	{
		// Mov [ECX + EDX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x51, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDX_4_0xC___EDX ()
	{
		// Mov [ECX + EDX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x91, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDX_8_0xC___EDX ()
	{
		// Mov [ECX + EDX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xd1, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDX_1___EDX ()
	{
		// Mov [DS:ECX + EDX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x11};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDX_2___EDX ()
	{
		// Mov [DS:ECX + EDX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x51};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDX_4___EDX ()
	{
		// Mov [DS:ECX + EDX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x91};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDX_8___EDX ()
	{
		// Mov [DS:ECX + EDX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xd1};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDX_1_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EDX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x11, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDX_2_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EDX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x51, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDX_4_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EDX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x91, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDX_8_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EDX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xd1, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDX_1_0xC___EDX ()
	{
		// Mov [DS:ECX + EDX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x11, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDX_2_0xC___EDX ()
	{
		// Mov [DS:ECX + EDX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x51, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDX_4_0xC___EDX ()
	{
		// Mov [DS:ECX + EDX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x91, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDX_8_0xC___EDX ()
	{
		// Mov [DS:ECX + EDX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xd1, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ESP_1___EDX ()
	{
		// Mov [ECX + ESP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ESP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xc};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ESP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ESP_1_0x12345678___EDX ()
	{
		// Mov [ECX + ESP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ESP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xc, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ESP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ESP_1_0xC___EDX ()
	{
		// Mov [ECX + ESP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ESP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xc, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ESP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ESP_1___EDX ()
	{
		// Mov [DS:ECX + ESP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ESP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xc};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ESP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ESP_1_0x12345678___EDX ()
	{
		// Mov [DS:ECX + ESP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ESP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xc, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ESP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ESP_1_0xC___EDX ()
	{
		// Mov [DS:ECX + ESP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ESP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xc, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ESP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBP_1___EDX ()
	{
		// Mov [ECX + EBP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x29};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBP_2___EDX ()
	{
		// Mov [ECX + EBP*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBP, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x69};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBP*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBP_4___EDX ()
	{
		// Mov [ECX + EBP*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBP, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xa9};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBP*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBP_8___EDX ()
	{
		// Mov [ECX + EBP*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBP, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xe9};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBP*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBP_1_0x12345678___EDX ()
	{
		// Mov [ECX + EBP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x29, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBP_2_0x12345678___EDX ()
	{
		// Mov [ECX + EBP*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBP, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x69, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBP*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBP_4_0x12345678___EDX ()
	{
		// Mov [ECX + EBP*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBP, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xa9, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBP*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBP_8_0x12345678___EDX ()
	{
		// Mov [ECX + EBP*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBP, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xe9, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBP*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBP_1_0xC___EDX ()
	{
		// Mov [ECX + EBP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x29, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBP_2_0xC___EDX ()
	{
		// Mov [ECX + EBP*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBP, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x69, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBP*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBP_4_0xC___EDX ()
	{
		// Mov [ECX + EBP*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBP, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xa9, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBP*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EBP_8_0xC___EDX ()
	{
		// Mov [ECX + EBP*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EBP, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xe9, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EBP*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBP_1___EDX ()
	{
		// Mov [DS:ECX + EBP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x29};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBP_2___EDX ()
	{
		// Mov [DS:ECX + EBP*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBP, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x69};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBP*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBP_4___EDX ()
	{
		// Mov [DS:ECX + EBP*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBP, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xa9};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBP*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBP_8___EDX ()
	{
		// Mov [DS:ECX + EBP*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBP, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xe9};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBP*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBP_1_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EBP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x29, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBP_2_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EBP*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBP, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x69, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBP*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBP_4_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EBP*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBP, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xa9, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBP*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBP_8_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EBP*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBP, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xe9, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBP*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBP_1_0xC___EDX ()
	{
		// Mov [DS:ECX + EBP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x29, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBP_2_0xC___EDX ()
	{
		// Mov [DS:ECX + EBP*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBP, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x69, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBP*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBP_4_0xC___EDX ()
	{
		// Mov [DS:ECX + EBP*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBP, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xa9, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBP*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EBP_8_0xC___EDX ()
	{
		// Mov [DS:ECX + EBP*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EBP, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xe9, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EBP*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDI_1___EDX ()
	{
		// Mov [ECX + EDI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x39};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDI_2___EDX ()
	{
		// Mov [ECX + EDI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x79};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDI_4___EDX ()
	{
		// Mov [ECX + EDI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xb9};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDI_8___EDX ()
	{
		// Mov [ECX + EDI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xf9};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDI_1_0x12345678___EDX ()
	{
		// Mov [ECX + EDI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x39, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDI_2_0x12345678___EDX ()
	{
		// Mov [ECX + EDI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x79, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDI_4_0x12345678___EDX ()
	{
		// Mov [ECX + EDI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xb9, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDI_8_0x12345678___EDX ()
	{
		// Mov [ECX + EDI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xf9, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDI_1_0xC___EDX ()
	{
		// Mov [ECX + EDI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x39, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDI_2_0xC___EDX ()
	{
		// Mov [ECX + EDI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x79, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDI_4_0xC___EDX ()
	{
		// Mov [ECX + EDI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xb9, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___EDI_8_0xC___EDX ()
	{
		// Mov [ECX + EDI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.EDI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xf9, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + EDI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDI_1___EDX ()
	{
		// Mov [DS:ECX + EDI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x39};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDI_2___EDX ()
	{
		// Mov [DS:ECX + EDI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x79};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDI_4___EDX ()
	{
		// Mov [DS:ECX + EDI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xb9};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDI_8___EDX ()
	{
		// Mov [DS:ECX + EDI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xf9};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDI_1_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EDI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x39, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDI_2_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EDI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x79, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDI_4_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EDI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xb9, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDI_8_0x12345678___EDX ()
	{
		// Mov [DS:ECX + EDI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xf9, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDI_1_0xC___EDX ()
	{
		// Mov [DS:ECX + EDI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x39, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDI_2_0xC___EDX ()
	{
		// Mov [DS:ECX + EDI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x79, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDI_4_0xC___EDX ()
	{
		// Mov [DS:ECX + EDI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xb9, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___EDI_8_0xC___EDX ()
	{
		// Mov [DS:ECX + EDI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.EDI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xf9, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + EDI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ESI_1___EDX ()
	{
		// Mov [ECX + ESI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ESI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x31};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ESI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ESI_2___EDX ()
	{
		// Mov [ECX + ESI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ESI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x71};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ESI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ESI_4___EDX ()
	{
		// Mov [ECX + ESI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ESI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xb1};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ESI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ESI_8___EDX ()
	{
		// Mov [ECX + ESI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ESI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xf1};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ESI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ESI_1_0x12345678___EDX ()
	{
		// Mov [ECX + ESI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ESI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x31, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ESI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ESI_2_0x12345678___EDX ()
	{
		// Mov [ECX + ESI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ESI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x71, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ESI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ESI_4_0x12345678___EDX ()
	{
		// Mov [ECX + ESI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ESI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xb1, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ESI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ESI_8_0x12345678___EDX ()
	{
		// Mov [ECX + ESI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ESI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xf1, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ESI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ESI_1_0xC___EDX ()
	{
		// Mov [ECX + ESI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ESI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x31, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ESI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ESI_2_0xC___EDX ()
	{
		// Mov [ECX + ESI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ESI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x71, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ESI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ESI_4_0xC___EDX ()
	{
		// Mov [ECX + ESI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ESI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xb1, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ESI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ECX___ESI_8_0xC___EDX ()
	{
		// Mov [ECX + ESI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ECX, R32.ESI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xf1, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ECX + ESI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ESI_1___EDX ()
	{
		// Mov [DS:ECX + ESI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ESI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x31};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ESI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ESI_2___EDX ()
	{
		// Mov [DS:ECX + ESI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ESI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x71};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ESI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ESI_4___EDX ()
	{
		// Mov [DS:ECX + ESI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ESI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xb1};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ESI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ESI_8___EDX ()
	{
		// Mov [DS:ECX + ESI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ESI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xf1};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ESI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ESI_1_0x12345678___EDX ()
	{
		// Mov [DS:ECX + ESI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ESI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x31, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ESI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ESI_2_0x12345678___EDX ()
	{
		// Mov [DS:ECX + ESI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ESI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x71, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ESI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ESI_4_0x12345678___EDX ()
	{
		// Mov [DS:ECX + ESI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ESI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xb1, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ESI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ESI_8_0x12345678___EDX ()
	{
		// Mov [DS:ECX + ESI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ESI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xf1, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ESI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ESI_1_0xC___EDX ()
	{
		// Mov [DS:ECX + ESI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ESI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x31, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ESI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ESI_2_0xC___EDX ()
	{
		// Mov [DS:ECX + ESI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ESI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x71, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ESI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ESI_4_0xC___EDX ()
	{
		// Mov [DS:ECX + ESI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ESI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xb1, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ESI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ECX___ESI_8_0xC___EDX ()
	{
		// Mov [DS:ECX + ESI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ECX, R32.ESI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xf1, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ECX + ESI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDX ()
	{
		// Mov [EDX], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, null, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX_0x12345678___EDX ()
	{
		// Mov [EDX+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, null, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x92, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX_0xC___EDX ()
	{
		// Mov [EDX-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, null, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x52, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDX ()
	{
		// Mov [DS:EDX], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, null, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX_0x12345678___EDX ()
	{
		// Mov [DS:EDX+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, null, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x92, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX_0xC___EDX ()
	{
		// Mov [DS:EDX-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, null, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x52, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EAX_1___EDX ()
	{
		// Mov [EDX + EAX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EAX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x2};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EAX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EAX_2___EDX ()
	{
		// Mov [EDX + EAX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EAX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x42};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EAX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EAX_4___EDX ()
	{
		// Mov [EDX + EAX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EAX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x82};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EAX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EAX_8___EDX ()
	{
		// Mov [EDX + EAX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EAX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xc2};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EAX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EAX_1_0x12345678___EDX ()
	{
		// Mov [EDX + EAX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EAX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x2, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EAX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EAX_2_0x12345678___EDX ()
	{
		// Mov [EDX + EAX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EAX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x42, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EAX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EAX_4_0x12345678___EDX ()
	{
		// Mov [EDX + EAX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EAX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x82, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EAX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EAX_8_0x12345678___EDX ()
	{
		// Mov [EDX + EAX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EAX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xc2, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EAX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EAX_1_0xC___EDX ()
	{
		// Mov [EDX + EAX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EAX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x2, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EAX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EAX_2_0xC___EDX ()
	{
		// Mov [EDX + EAX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EAX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x42, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EAX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EAX_4_0xC___EDX ()
	{
		// Mov [EDX + EAX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EAX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x82, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EAX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EAX_8_0xC___EDX ()
	{
		// Mov [EDX + EAX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EAX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xc2, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EAX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EAX_1___EDX ()
	{
		// Mov [DS:EDX + EAX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EAX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x2};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EAX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EAX_2___EDX ()
	{
		// Mov [DS:EDX + EAX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EAX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x42};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EAX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EAX_4___EDX ()
	{
		// Mov [DS:EDX + EAX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EAX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x82};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EAX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EAX_8___EDX ()
	{
		// Mov [DS:EDX + EAX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EAX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xc2};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EAX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EAX_1_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EAX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EAX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x2, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EAX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EAX_2_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EAX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EAX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x42, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EAX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EAX_4_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EAX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EAX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x82, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EAX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EAX_8_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EAX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EAX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xc2, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EAX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EAX_1_0xC___EDX ()
	{
		// Mov [DS:EDX + EAX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EAX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x2, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EAX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EAX_2_0xC___EDX ()
	{
		// Mov [DS:EDX + EAX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EAX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x42, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EAX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EAX_4_0xC___EDX ()
	{
		// Mov [DS:EDX + EAX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EAX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x82, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EAX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EAX_8_0xC___EDX ()
	{
		// Mov [DS:EDX + EAX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EAX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xc2, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EAX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBX_1___EDX ()
	{
		// Mov [EDX + EBX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x1a};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBX_2___EDX ()
	{
		// Mov [EDX + EBX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x5a};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBX_4___EDX ()
	{
		// Mov [EDX + EBX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x9a};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBX_8___EDX ()
	{
		// Mov [EDX + EBX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xda};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBX_1_0x12345678___EDX ()
	{
		// Mov [EDX + EBX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x1a, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBX_2_0x12345678___EDX ()
	{
		// Mov [EDX + EBX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x5a, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBX_4_0x12345678___EDX ()
	{
		// Mov [EDX + EBX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x9a, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBX_8_0x12345678___EDX ()
	{
		// Mov [EDX + EBX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xda, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBX_1_0xC___EDX ()
	{
		// Mov [EDX + EBX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x1a, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBX_2_0xC___EDX ()
	{
		// Mov [EDX + EBX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x5a, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBX_4_0xC___EDX ()
	{
		// Mov [EDX + EBX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x9a, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBX_8_0xC___EDX ()
	{
		// Mov [EDX + EBX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xda, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBX_1___EDX ()
	{
		// Mov [DS:EDX + EBX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x1a};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBX_2___EDX ()
	{
		// Mov [DS:EDX + EBX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x5a};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBX_4___EDX ()
	{
		// Mov [DS:EDX + EBX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x9a};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBX_8___EDX ()
	{
		// Mov [DS:EDX + EBX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xda};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBX_1_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EBX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x1a, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBX_2_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EBX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x5a, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBX_4_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EBX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x9a, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBX_8_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EBX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xda, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBX_1_0xC___EDX ()
	{
		// Mov [DS:EDX + EBX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x1a, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBX_2_0xC___EDX ()
	{
		// Mov [DS:EDX + EBX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x5a, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBX_4_0xC___EDX ()
	{
		// Mov [DS:EDX + EBX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x9a, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBX_8_0xC___EDX ()
	{
		// Mov [DS:EDX + EBX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xda, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ECX_1___EDX ()
	{
		// Mov [EDX + ECX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ECX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xa};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ECX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ECX_2___EDX ()
	{
		// Mov [EDX + ECX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ECX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x4a};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ECX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ECX_4___EDX ()
	{
		// Mov [EDX + ECX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ECX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x8a};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ECX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ECX_8___EDX ()
	{
		// Mov [EDX + ECX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ECX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xca};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ECX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ECX_1_0x12345678___EDX ()
	{
		// Mov [EDX + ECX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ECX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xa, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ECX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ECX_2_0x12345678___EDX ()
	{
		// Mov [EDX + ECX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ECX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x4a, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ECX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ECX_4_0x12345678___EDX ()
	{
		// Mov [EDX + ECX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ECX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x8a, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ECX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ECX_8_0x12345678___EDX ()
	{
		// Mov [EDX + ECX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ECX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xca, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ECX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ECX_1_0xC___EDX ()
	{
		// Mov [EDX + ECX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ECX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xa, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ECX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ECX_2_0xC___EDX ()
	{
		// Mov [EDX + ECX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ECX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x4a, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ECX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ECX_4_0xC___EDX ()
	{
		// Mov [EDX + ECX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ECX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x8a, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ECX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ECX_8_0xC___EDX ()
	{
		// Mov [EDX + ECX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ECX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xca, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ECX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ECX_1___EDX ()
	{
		// Mov [DS:EDX + ECX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ECX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xa};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ECX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ECX_2___EDX ()
	{
		// Mov [DS:EDX + ECX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ECX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x4a};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ECX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ECX_4___EDX ()
	{
		// Mov [DS:EDX + ECX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ECX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x8a};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ECX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ECX_8___EDX ()
	{
		// Mov [DS:EDX + ECX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ECX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xca};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ECX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ECX_1_0x12345678___EDX ()
	{
		// Mov [DS:EDX + ECX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ECX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xa, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ECX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ECX_2_0x12345678___EDX ()
	{
		// Mov [DS:EDX + ECX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ECX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x4a, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ECX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ECX_4_0x12345678___EDX ()
	{
		// Mov [DS:EDX + ECX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ECX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x8a, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ECX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ECX_8_0x12345678___EDX ()
	{
		// Mov [DS:EDX + ECX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ECX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xca, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ECX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ECX_1_0xC___EDX ()
	{
		// Mov [DS:EDX + ECX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ECX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xa, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ECX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ECX_2_0xC___EDX ()
	{
		// Mov [DS:EDX + ECX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ECX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x4a, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ECX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ECX_4_0xC___EDX ()
	{
		// Mov [DS:EDX + ECX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ECX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x8a, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ECX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ECX_8_0xC___EDX ()
	{
		// Mov [DS:EDX + ECX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ECX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xca, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ECX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDX_1___EDX ()
	{
		// Mov [EDX + EDX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDX_2___EDX ()
	{
		// Mov [EDX + EDX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x52};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDX_4___EDX ()
	{
		// Mov [EDX + EDX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x92};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDX_8___EDX ()
	{
		// Mov [EDX + EDX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xd2};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDX_1_0x12345678___EDX ()
	{
		// Mov [EDX + EDX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x12, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDX_2_0x12345678___EDX ()
	{
		// Mov [EDX + EDX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x52, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDX_4_0x12345678___EDX ()
	{
		// Mov [EDX + EDX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x92, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDX_8_0x12345678___EDX ()
	{
		// Mov [EDX + EDX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xd2, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDX_1_0xC___EDX ()
	{
		// Mov [EDX + EDX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x12, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDX_2_0xC___EDX ()
	{
		// Mov [EDX + EDX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x52, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDX_4_0xC___EDX ()
	{
		// Mov [EDX + EDX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x92, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDX_8_0xC___EDX ()
	{
		// Mov [EDX + EDX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xd2, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDX_1___EDX ()
	{
		// Mov [DS:EDX + EDX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDX_2___EDX ()
	{
		// Mov [DS:EDX + EDX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x52};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDX_4___EDX ()
	{
		// Mov [DS:EDX + EDX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x92};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDX_8___EDX ()
	{
		// Mov [DS:EDX + EDX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xd2};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDX_1_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EDX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x12, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDX_2_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EDX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x52, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDX_4_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EDX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x92, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDX_8_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EDX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xd2, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDX_1_0xC___EDX ()
	{
		// Mov [DS:EDX + EDX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x12, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDX_2_0xC___EDX ()
	{
		// Mov [DS:EDX + EDX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x52, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDX_4_0xC___EDX ()
	{
		// Mov [DS:EDX + EDX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x92, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDX_8_0xC___EDX ()
	{
		// Mov [DS:EDX + EDX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xd2, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ESP_1___EDX ()
	{
		// Mov [EDX + ESP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ESP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x14};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ESP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ESP_1_0x12345678___EDX ()
	{
		// Mov [EDX + ESP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ESP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x14, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ESP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ESP_1_0xC___EDX ()
	{
		// Mov [EDX + ESP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ESP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x14, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ESP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ESP_1___EDX ()
	{
		// Mov [DS:EDX + ESP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ESP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x14};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ESP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ESP_1_0x12345678___EDX ()
	{
		// Mov [DS:EDX + ESP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ESP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x14, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ESP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ESP_1_0xC___EDX ()
	{
		// Mov [DS:EDX + ESP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ESP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x14, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ESP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBP_1___EDX ()
	{
		// Mov [EDX + EBP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x2a};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBP_2___EDX ()
	{
		// Mov [EDX + EBP*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBP, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x6a};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBP*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBP_4___EDX ()
	{
		// Mov [EDX + EBP*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBP, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xaa};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBP*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBP_8___EDX ()
	{
		// Mov [EDX + EBP*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBP, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xea};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBP*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBP_1_0x12345678___EDX ()
	{
		// Mov [EDX + EBP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x2a, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBP_2_0x12345678___EDX ()
	{
		// Mov [EDX + EBP*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBP, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x6a, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBP*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBP_4_0x12345678___EDX ()
	{
		// Mov [EDX + EBP*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBP, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xaa, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBP*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBP_8_0x12345678___EDX ()
	{
		// Mov [EDX + EBP*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBP, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xea, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBP*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBP_1_0xC___EDX ()
	{
		// Mov [EDX + EBP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x2a, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBP_2_0xC___EDX ()
	{
		// Mov [EDX + EBP*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBP, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x6a, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBP*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBP_4_0xC___EDX ()
	{
		// Mov [EDX + EBP*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBP, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xaa, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBP*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EBP_8_0xC___EDX ()
	{
		// Mov [EDX + EBP*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EBP, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xea, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EBP*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBP_1___EDX ()
	{
		// Mov [DS:EDX + EBP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x2a};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBP_2___EDX ()
	{
		// Mov [DS:EDX + EBP*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBP, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x6a};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBP*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBP_4___EDX ()
	{
		// Mov [DS:EDX + EBP*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBP, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xaa};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBP*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBP_8___EDX ()
	{
		// Mov [DS:EDX + EBP*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBP, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xea};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBP*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBP_1_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EBP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x2a, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBP_2_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EBP*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBP, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x6a, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBP*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBP_4_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EBP*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBP, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xaa, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBP*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBP_8_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EBP*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBP, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xea, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBP*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBP_1_0xC___EDX ()
	{
		// Mov [DS:EDX + EBP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x2a, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBP_2_0xC___EDX ()
	{
		// Mov [DS:EDX + EBP*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBP, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x6a, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBP*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBP_4_0xC___EDX ()
	{
		// Mov [DS:EDX + EBP*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBP, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xaa, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBP*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EBP_8_0xC___EDX ()
	{
		// Mov [DS:EDX + EBP*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EBP, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xea, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EBP*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDI_1___EDX ()
	{
		// Mov [EDX + EDI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x3a};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDI_2___EDX ()
	{
		// Mov [EDX + EDI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x7a};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDI_4___EDX ()
	{
		// Mov [EDX + EDI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xba};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDI_8___EDX ()
	{
		// Mov [EDX + EDI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xfa};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDI_1_0x12345678___EDX ()
	{
		// Mov [EDX + EDI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x3a, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDI_2_0x12345678___EDX ()
	{
		// Mov [EDX + EDI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x7a, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDI_4_0x12345678___EDX ()
	{
		// Mov [EDX + EDI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xba, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDI_8_0x12345678___EDX ()
	{
		// Mov [EDX + EDI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xfa, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDI_1_0xC___EDX ()
	{
		// Mov [EDX + EDI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x3a, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDI_2_0xC___EDX ()
	{
		// Mov [EDX + EDI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x7a, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDI_4_0xC___EDX ()
	{
		// Mov [EDX + EDI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xba, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___EDI_8_0xC___EDX ()
	{
		// Mov [EDX + EDI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.EDI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xfa, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + EDI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDI_1___EDX ()
	{
		// Mov [DS:EDX + EDI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x3a};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDI_2___EDX ()
	{
		// Mov [DS:EDX + EDI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x7a};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDI_4___EDX ()
	{
		// Mov [DS:EDX + EDI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xba};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDI_8___EDX ()
	{
		// Mov [DS:EDX + EDI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xfa};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDI_1_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EDI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x3a, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDI_2_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EDI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x7a, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDI_4_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EDI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xba, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDI_8_0x12345678___EDX ()
	{
		// Mov [DS:EDX + EDI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xfa, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDI_1_0xC___EDX ()
	{
		// Mov [DS:EDX + EDI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x3a, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDI_2_0xC___EDX ()
	{
		// Mov [DS:EDX + EDI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x7a, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDI_4_0xC___EDX ()
	{
		// Mov [DS:EDX + EDI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xba, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___EDI_8_0xC___EDX ()
	{
		// Mov [DS:EDX + EDI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.EDI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xfa, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + EDI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ESI_1___EDX ()
	{
		// Mov [EDX + ESI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ESI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x32};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ESI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ESI_2___EDX ()
	{
		// Mov [EDX + ESI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ESI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x72};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ESI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ESI_4___EDX ()
	{
		// Mov [EDX + ESI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ESI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xb2};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ESI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ESI_8___EDX ()
	{
		// Mov [EDX + ESI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ESI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xf2};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ESI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ESI_1_0x12345678___EDX ()
	{
		// Mov [EDX + ESI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ESI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x32, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ESI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ESI_2_0x12345678___EDX ()
	{
		// Mov [EDX + ESI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ESI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x72, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ESI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ESI_4_0x12345678___EDX ()
	{
		// Mov [EDX + ESI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ESI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xb2, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ESI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ESI_8_0x12345678___EDX ()
	{
		// Mov [EDX + ESI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ESI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xf2, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ESI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ESI_1_0xC___EDX ()
	{
		// Mov [EDX + ESI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ESI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x32, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ESI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ESI_2_0xC___EDX ()
	{
		// Mov [EDX + ESI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ESI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x72, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ESI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ESI_4_0xC___EDX ()
	{
		// Mov [EDX + ESI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ESI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xb2, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ESI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDX___ESI_8_0xC___EDX ()
	{
		// Mov [EDX + ESI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDX, R32.ESI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xf2, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDX + ESI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ESI_1___EDX ()
	{
		// Mov [DS:EDX + ESI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ESI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x32};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ESI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ESI_2___EDX ()
	{
		// Mov [DS:EDX + ESI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ESI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x72};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ESI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ESI_4___EDX ()
	{
		// Mov [DS:EDX + ESI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ESI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xb2};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ESI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ESI_8___EDX ()
	{
		// Mov [DS:EDX + ESI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ESI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xf2};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ESI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ESI_1_0x12345678___EDX ()
	{
		// Mov [DS:EDX + ESI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ESI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x32, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ESI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ESI_2_0x12345678___EDX ()
	{
		// Mov [DS:EDX + ESI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ESI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x72, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ESI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ESI_4_0x12345678___EDX ()
	{
		// Mov [DS:EDX + ESI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ESI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xb2, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ESI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ESI_8_0x12345678___EDX ()
	{
		// Mov [DS:EDX + ESI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ESI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xf2, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ESI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ESI_1_0xC___EDX ()
	{
		// Mov [DS:EDX + ESI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ESI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x32, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ESI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ESI_2_0xC___EDX ()
	{
		// Mov [DS:EDX + ESI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ESI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x72, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ESI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ESI_4_0xC___EDX ()
	{
		// Mov [DS:EDX + ESI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ESI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xb2, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ESI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDX___ESI_8_0xC___EDX ()
	{
		// Mov [DS:EDX + ESI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDX, R32.ESI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xf2, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDX + ESI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDX ()
	{
		// Mov [ESP], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, null, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x24};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP_0x12345678___EDX ()
	{
		// Mov [ESP+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, null, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x24, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP_0xC___EDX ()
	{
		// Mov [ESP-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, null, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x24, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDX ()
	{
		// Mov [DS:ESP], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, null, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x24};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP_0x12345678___EDX ()
	{
		// Mov [DS:ESP+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, null, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x24, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP_0xC___EDX ()
	{
		// Mov [DS:ESP-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, null, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x24, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EAX_1___EDX ()
	{
		// Mov [ESP + EAX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EAX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EAX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EAX_2___EDX ()
	{
		// Mov [ESP + EAX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EAX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x44};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EAX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EAX_4___EDX ()
	{
		// Mov [ESP + EAX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EAX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x84};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EAX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EAX_8___EDX ()
	{
		// Mov [ESP + EAX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EAX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xc4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EAX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EAX_1_0x12345678___EDX ()
	{
		// Mov [ESP + EAX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EAX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x4, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EAX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EAX_2_0x12345678___EDX ()
	{
		// Mov [ESP + EAX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EAX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x44, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EAX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EAX_4_0x12345678___EDX ()
	{
		// Mov [ESP + EAX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EAX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x84, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EAX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EAX_8_0x12345678___EDX ()
	{
		// Mov [ESP + EAX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EAX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xc4, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EAX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EAX_1_0xC___EDX ()
	{
		// Mov [ESP + EAX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EAX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x4, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EAX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EAX_2_0xC___EDX ()
	{
		// Mov [ESP + EAX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EAX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x44, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EAX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EAX_4_0xC___EDX ()
	{
		// Mov [ESP + EAX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EAX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x84, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EAX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EAX_8_0xC___EDX ()
	{
		// Mov [ESP + EAX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EAX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xc4, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EAX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EAX_1___EDX ()
	{
		// Mov [DS:ESP + EAX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EAX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EAX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EAX_2___EDX ()
	{
		// Mov [DS:ESP + EAX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EAX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x44};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EAX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EAX_4___EDX ()
	{
		// Mov [DS:ESP + EAX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EAX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x84};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EAX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EAX_8___EDX ()
	{
		// Mov [DS:ESP + EAX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EAX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xc4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EAX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EAX_1_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EAX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EAX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x4, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EAX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EAX_2_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EAX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EAX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x44, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EAX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EAX_4_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EAX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EAX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x84, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EAX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EAX_8_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EAX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EAX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xc4, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EAX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EAX_1_0xC___EDX ()
	{
		// Mov [DS:ESP + EAX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EAX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x4, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EAX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EAX_2_0xC___EDX ()
	{
		// Mov [DS:ESP + EAX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EAX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x44, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EAX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EAX_4_0xC___EDX ()
	{
		// Mov [DS:ESP + EAX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EAX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x84, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EAX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EAX_8_0xC___EDX ()
	{
		// Mov [DS:ESP + EAX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EAX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xc4, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EAX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBX_1___EDX ()
	{
		// Mov [ESP + EBX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x1c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBX_2___EDX ()
	{
		// Mov [ESP + EBX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x5c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBX_4___EDX ()
	{
		// Mov [ESP + EBX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x9c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBX_8___EDX ()
	{
		// Mov [ESP + EBX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xdc};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBX_1_0x12345678___EDX ()
	{
		// Mov [ESP + EBX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x1c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBX_2_0x12345678___EDX ()
	{
		// Mov [ESP + EBX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x5c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBX_4_0x12345678___EDX ()
	{
		// Mov [ESP + EBX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x9c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBX_8_0x12345678___EDX ()
	{
		// Mov [ESP + EBX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xdc, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBX_1_0xC___EDX ()
	{
		// Mov [ESP + EBX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x1c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBX_2_0xC___EDX ()
	{
		// Mov [ESP + EBX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x5c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBX_4_0xC___EDX ()
	{
		// Mov [ESP + EBX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x9c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBX_8_0xC___EDX ()
	{
		// Mov [ESP + EBX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xdc, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBX_1___EDX ()
	{
		// Mov [DS:ESP + EBX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x1c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBX_2___EDX ()
	{
		// Mov [DS:ESP + EBX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x5c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBX_4___EDX ()
	{
		// Mov [DS:ESP + EBX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x9c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBX_8___EDX ()
	{
		// Mov [DS:ESP + EBX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xdc};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBX_1_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EBX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x1c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBX_2_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EBX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x5c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBX_4_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EBX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x9c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBX_8_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EBX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xdc, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBX_1_0xC___EDX ()
	{
		// Mov [DS:ESP + EBX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x1c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBX_2_0xC___EDX ()
	{
		// Mov [DS:ESP + EBX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x5c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBX_4_0xC___EDX ()
	{
		// Mov [DS:ESP + EBX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x9c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBX_8_0xC___EDX ()
	{
		// Mov [DS:ESP + EBX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xdc, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ECX_1___EDX ()
	{
		// Mov [ESP + ECX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ECX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xc};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ECX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ECX_2___EDX ()
	{
		// Mov [ESP + ECX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ECX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x4c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ECX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ECX_4___EDX ()
	{
		// Mov [ESP + ECX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ECX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x8c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ECX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ECX_8___EDX ()
	{
		// Mov [ESP + ECX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ECX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xcc};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ECX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ECX_1_0x12345678___EDX ()
	{
		// Mov [ESP + ECX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ECX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xc, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ECX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ECX_2_0x12345678___EDX ()
	{
		// Mov [ESP + ECX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ECX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x4c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ECX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ECX_4_0x12345678___EDX ()
	{
		// Mov [ESP + ECX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ECX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x8c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ECX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ECX_8_0x12345678___EDX ()
	{
		// Mov [ESP + ECX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ECX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xcc, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ECX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ECX_1_0xC___EDX ()
	{
		// Mov [ESP + ECX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ECX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xc, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ECX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ECX_2_0xC___EDX ()
	{
		// Mov [ESP + ECX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ECX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x4c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ECX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ECX_4_0xC___EDX ()
	{
		// Mov [ESP + ECX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ECX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x8c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ECX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ECX_8_0xC___EDX ()
	{
		// Mov [ESP + ECX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ECX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xcc, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ECX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ECX_1___EDX ()
	{
		// Mov [DS:ESP + ECX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ECX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xc};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ECX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ECX_2___EDX ()
	{
		// Mov [DS:ESP + ECX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ECX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x4c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ECX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ECX_4___EDX ()
	{
		// Mov [DS:ESP + ECX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ECX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x8c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ECX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ECX_8___EDX ()
	{
		// Mov [DS:ESP + ECX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ECX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xcc};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ECX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ECX_1_0x12345678___EDX ()
	{
		// Mov [DS:ESP + ECX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ECX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xc, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ECX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ECX_2_0x12345678___EDX ()
	{
		// Mov [DS:ESP + ECX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ECX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x4c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ECX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ECX_4_0x12345678___EDX ()
	{
		// Mov [DS:ESP + ECX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ECX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x8c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ECX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ECX_8_0x12345678___EDX ()
	{
		// Mov [DS:ESP + ECX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ECX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xcc, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ECX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ECX_1_0xC___EDX ()
	{
		// Mov [DS:ESP + ECX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ECX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xc, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ECX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ECX_2_0xC___EDX ()
	{
		// Mov [DS:ESP + ECX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ECX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x4c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ECX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ECX_4_0xC___EDX ()
	{
		// Mov [DS:ESP + ECX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ECX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x8c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ECX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ECX_8_0xC___EDX ()
	{
		// Mov [DS:ESP + ECX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ECX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xcc, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ECX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDX_1___EDX ()
	{
		// Mov [ESP + EDX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x14};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDX_2___EDX ()
	{
		// Mov [ESP + EDX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x54};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDX_4___EDX ()
	{
		// Mov [ESP + EDX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x94};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDX_8___EDX ()
	{
		// Mov [ESP + EDX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xd4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDX_1_0x12345678___EDX ()
	{
		// Mov [ESP + EDX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x14, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDX_2_0x12345678___EDX ()
	{
		// Mov [ESP + EDX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x54, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDX_4_0x12345678___EDX ()
	{
		// Mov [ESP + EDX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x94, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDX_8_0x12345678___EDX ()
	{
		// Mov [ESP + EDX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xd4, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDX_1_0xC___EDX ()
	{
		// Mov [ESP + EDX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x14, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDX_2_0xC___EDX ()
	{
		// Mov [ESP + EDX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x54, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDX_4_0xC___EDX ()
	{
		// Mov [ESP + EDX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x94, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDX_8_0xC___EDX ()
	{
		// Mov [ESP + EDX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xd4, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDX_1___EDX ()
	{
		// Mov [DS:ESP + EDX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x14};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDX_2___EDX ()
	{
		// Mov [DS:ESP + EDX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x54};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDX_4___EDX ()
	{
		// Mov [DS:ESP + EDX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x94};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDX_8___EDX ()
	{
		// Mov [DS:ESP + EDX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xd4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDX_1_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EDX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x14, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDX_2_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EDX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x54, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDX_4_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EDX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x94, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDX_8_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EDX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xd4, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDX_1_0xC___EDX ()
	{
		// Mov [DS:ESP + EDX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x14, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDX_2_0xC___EDX ()
	{
		// Mov [DS:ESP + EDX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x54, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDX_4_0xC___EDX ()
	{
		// Mov [DS:ESP + EDX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x94, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDX_8_0xC___EDX ()
	{
		// Mov [DS:ESP + EDX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xd4, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBP_1___EDX ()
	{
		// Mov [ESP + EBP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x2c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBP_2___EDX ()
	{
		// Mov [ESP + EBP*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBP, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x6c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBP*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBP_4___EDX ()
	{
		// Mov [ESP + EBP*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBP, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xac};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBP*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBP_8___EDX ()
	{
		// Mov [ESP + EBP*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBP, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xec};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBP*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBP_1_0x12345678___EDX ()
	{
		// Mov [ESP + EBP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x2c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBP_2_0x12345678___EDX ()
	{
		// Mov [ESP + EBP*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBP, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x6c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBP*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBP_4_0x12345678___EDX ()
	{
		// Mov [ESP + EBP*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBP, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xac, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBP*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBP_8_0x12345678___EDX ()
	{
		// Mov [ESP + EBP*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBP, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xec, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBP*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBP_1_0xC___EDX ()
	{
		// Mov [ESP + EBP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x2c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBP_2_0xC___EDX ()
	{
		// Mov [ESP + EBP*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBP, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x6c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBP*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBP_4_0xC___EDX ()
	{
		// Mov [ESP + EBP*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBP, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xac, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBP*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EBP_8_0xC___EDX ()
	{
		// Mov [ESP + EBP*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EBP, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xec, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EBP*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBP_1___EDX ()
	{
		// Mov [DS:ESP + EBP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x2c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBP_2___EDX ()
	{
		// Mov [DS:ESP + EBP*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBP, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x6c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBP*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBP_4___EDX ()
	{
		// Mov [DS:ESP + EBP*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBP, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xac};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBP*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBP_8___EDX ()
	{
		// Mov [DS:ESP + EBP*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBP, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xec};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBP*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBP_1_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EBP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x2c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBP_2_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EBP*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBP, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x6c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBP*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBP_4_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EBP*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBP, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xac, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBP*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBP_8_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EBP*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBP, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xec, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBP*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBP_1_0xC___EDX ()
	{
		// Mov [DS:ESP + EBP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x2c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBP_2_0xC___EDX ()
	{
		// Mov [DS:ESP + EBP*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBP, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x6c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBP*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBP_4_0xC___EDX ()
	{
		// Mov [DS:ESP + EBP*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBP, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xac, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBP*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EBP_8_0xC___EDX ()
	{
		// Mov [DS:ESP + EBP*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EBP, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xec, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EBP*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDI_1___EDX ()
	{
		// Mov [ESP + EDI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x3c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDI_2___EDX ()
	{
		// Mov [ESP + EDI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x7c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDI_4___EDX ()
	{
		// Mov [ESP + EDI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xbc};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDI_8___EDX ()
	{
		// Mov [ESP + EDI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xfc};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDI_1_0x12345678___EDX ()
	{
		// Mov [ESP + EDI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x3c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDI_2_0x12345678___EDX ()
	{
		// Mov [ESP + EDI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x7c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDI_4_0x12345678___EDX ()
	{
		// Mov [ESP + EDI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xbc, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDI_8_0x12345678___EDX ()
	{
		// Mov [ESP + EDI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xfc, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDI_1_0xC___EDX ()
	{
		// Mov [ESP + EDI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x3c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDI_2_0xC___EDX ()
	{
		// Mov [ESP + EDI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x7c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDI_4_0xC___EDX ()
	{
		// Mov [ESP + EDI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xbc, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___EDI_8_0xC___EDX ()
	{
		// Mov [ESP + EDI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.EDI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xfc, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + EDI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDI_1___EDX ()
	{
		// Mov [DS:ESP + EDI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x3c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDI_2___EDX ()
	{
		// Mov [DS:ESP + EDI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x7c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDI_4___EDX ()
	{
		// Mov [DS:ESP + EDI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xbc};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDI_8___EDX ()
	{
		// Mov [DS:ESP + EDI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xfc};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDI_1_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EDI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x3c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDI_2_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EDI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x7c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDI_4_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EDI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xbc, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDI_8_0x12345678___EDX ()
	{
		// Mov [DS:ESP + EDI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xfc, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDI_1_0xC___EDX ()
	{
		// Mov [DS:ESP + EDI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x3c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDI_2_0xC___EDX ()
	{
		// Mov [DS:ESP + EDI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x7c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDI_4_0xC___EDX ()
	{
		// Mov [DS:ESP + EDI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xbc, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___EDI_8_0xC___EDX ()
	{
		// Mov [DS:ESP + EDI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.EDI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xfc, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + EDI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ESI_1___EDX ()
	{
		// Mov [ESP + ESI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ESI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x34};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ESI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ESI_2___EDX ()
	{
		// Mov [ESP + ESI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ESI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x74};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ESI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ESI_4___EDX ()
	{
		// Mov [ESP + ESI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ESI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xb4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ESI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ESI_8___EDX ()
	{
		// Mov [ESP + ESI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ESI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ESI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ESI_1_0x12345678___EDX ()
	{
		// Mov [ESP + ESI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ESI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x34, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ESI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ESI_2_0x12345678___EDX ()
	{
		// Mov [ESP + ESI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ESI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x74, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ESI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ESI_4_0x12345678___EDX ()
	{
		// Mov [ESP + ESI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ESI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xb4, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ESI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ESI_8_0x12345678___EDX ()
	{
		// Mov [ESP + ESI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ESI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xf4, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ESI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ESI_1_0xC___EDX ()
	{
		// Mov [ESP + ESI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ESI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x34, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ESI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ESI_2_0xC___EDX ()
	{
		// Mov [ESP + ESI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ESI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x74, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ESI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ESI_4_0xC___EDX ()
	{
		// Mov [ESP + ESI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ESI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xb4, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ESI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESP___ESI_8_0xC___EDX ()
	{
		// Mov [ESP + ESI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESP, R32.ESI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xf4, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESP + ESI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ESI_1___EDX ()
	{
		// Mov [DS:ESP + ESI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ESI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x34};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ESI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ESI_2___EDX ()
	{
		// Mov [DS:ESP + ESI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ESI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x74};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ESI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ESI_4___EDX ()
	{
		// Mov [DS:ESP + ESI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ESI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xb4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ESI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ESI_8___EDX ()
	{
		// Mov [DS:ESP + ESI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ESI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ESI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ESI_1_0x12345678___EDX ()
	{
		// Mov [DS:ESP + ESI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ESI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x34, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ESI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ESI_2_0x12345678___EDX ()
	{
		// Mov [DS:ESP + ESI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ESI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x74, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ESI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ESI_4_0x12345678___EDX ()
	{
		// Mov [DS:ESP + ESI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ESI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xb4, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ESI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ESI_8_0x12345678___EDX ()
	{
		// Mov [DS:ESP + ESI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ESI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xf4, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ESI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ESI_1_0xC___EDX ()
	{
		// Mov [DS:ESP + ESI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ESI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x34, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ESI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ESI_2_0xC___EDX ()
	{
		// Mov [DS:ESP + ESI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ESI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x74, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ESI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ESI_4_0xC___EDX ()
	{
		// Mov [DS:ESP + ESI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ESI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xb4, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ESI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESP___ESI_8_0xC___EDX ()
	{
		// Mov [DS:ESP + ESI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESP, R32.ESI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xf4, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESP + ESI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDX ()
	{
		// Mov [EBP], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, null, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x55, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP_0x12345678___EDX ()
	{
		// Mov [EBP+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, null, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x95, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP_0xC___EDX ()
	{
		// Mov [EBP-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, null, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x55, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDX ()
	{
		// Mov [DS:EBP], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, null, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x55, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP_0x12345678___EDX ()
	{
		// Mov [DS:EBP+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, null, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x95, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP_0xC___EDX ()
	{
		// Mov [DS:EBP-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, null, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x55, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EAX_1___EDX ()
	{
		// Mov [EBP + EAX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EAX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x5, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EAX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EAX_2___EDX ()
	{
		// Mov [EBP + EAX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EAX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x45, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EAX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EAX_4___EDX ()
	{
		// Mov [EBP + EAX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EAX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x85, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EAX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EAX_8___EDX ()
	{
		// Mov [EBP + EAX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EAX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xc5, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EAX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EAX_1_0x12345678___EDX ()
	{
		// Mov [EBP + EAX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EAX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x5, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EAX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EAX_2_0x12345678___EDX ()
	{
		// Mov [EBP + EAX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EAX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x45, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EAX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EAX_4_0x12345678___EDX ()
	{
		// Mov [EBP + EAX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EAX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x85, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EAX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EAX_8_0x12345678___EDX ()
	{
		// Mov [EBP + EAX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EAX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xc5, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EAX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EAX_1_0xC___EDX ()
	{
		// Mov [EBP + EAX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EAX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x5, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EAX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EAX_2_0xC___EDX ()
	{
		// Mov [EBP + EAX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EAX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x45, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EAX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EAX_4_0xC___EDX ()
	{
		// Mov [EBP + EAX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EAX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x85, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EAX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EAX_8_0xC___EDX ()
	{
		// Mov [EBP + EAX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EAX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xc5, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EAX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EAX_1___EDX ()
	{
		// Mov [DS:EBP + EAX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EAX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x5, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EAX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EAX_2___EDX ()
	{
		// Mov [DS:EBP + EAX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EAX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x45, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EAX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EAX_4___EDX ()
	{
		// Mov [DS:EBP + EAX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EAX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x85, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EAX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EAX_8___EDX ()
	{
		// Mov [DS:EBP + EAX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EAX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xc5, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EAX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EAX_1_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EAX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EAX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x5, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EAX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EAX_2_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EAX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EAX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x45, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EAX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EAX_4_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EAX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EAX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x85, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EAX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EAX_8_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EAX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EAX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xc5, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EAX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EAX_1_0xC___EDX ()
	{
		// Mov [DS:EBP + EAX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EAX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x5, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EAX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EAX_2_0xC___EDX ()
	{
		// Mov [DS:EBP + EAX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EAX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x45, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EAX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EAX_4_0xC___EDX ()
	{
		// Mov [DS:EBP + EAX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EAX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x85, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EAX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EAX_8_0xC___EDX ()
	{
		// Mov [DS:EBP + EAX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EAX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xc5, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EAX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBX_1___EDX ()
	{
		// Mov [EBP + EBX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x1d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBX_2___EDX ()
	{
		// Mov [EBP + EBX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x5d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBX_4___EDX ()
	{
		// Mov [EBP + EBX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x9d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBX_8___EDX ()
	{
		// Mov [EBP + EBX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xdd, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBX_1_0x12345678___EDX ()
	{
		// Mov [EBP + EBX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x1d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBX_2_0x12345678___EDX ()
	{
		// Mov [EBP + EBX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x5d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBX_4_0x12345678___EDX ()
	{
		// Mov [EBP + EBX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x9d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBX_8_0x12345678___EDX ()
	{
		// Mov [EBP + EBX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xdd, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBX_1_0xC___EDX ()
	{
		// Mov [EBP + EBX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x1d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBX_2_0xC___EDX ()
	{
		// Mov [EBP + EBX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x5d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBX_4_0xC___EDX ()
	{
		// Mov [EBP + EBX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x9d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBX_8_0xC___EDX ()
	{
		// Mov [EBP + EBX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xdd, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBX_1___EDX ()
	{
		// Mov [DS:EBP + EBX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x1d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBX_2___EDX ()
	{
		// Mov [DS:EBP + EBX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x5d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBX_4___EDX ()
	{
		// Mov [DS:EBP + EBX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x9d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBX_8___EDX ()
	{
		// Mov [DS:EBP + EBX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xdd, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBX_1_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EBX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x1d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBX_2_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EBX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x5d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBX_4_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EBX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x9d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBX_8_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EBX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xdd, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBX_1_0xC___EDX ()
	{
		// Mov [DS:EBP + EBX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x1d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBX_2_0xC___EDX ()
	{
		// Mov [DS:EBP + EBX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x5d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBX_4_0xC___EDX ()
	{
		// Mov [DS:EBP + EBX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x9d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBX_8_0xC___EDX ()
	{
		// Mov [DS:EBP + EBX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xdd, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ECX_1___EDX ()
	{
		// Mov [EBP + ECX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ECX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xd, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ECX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ECX_2___EDX ()
	{
		// Mov [EBP + ECX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ECX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x4d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ECX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ECX_4___EDX ()
	{
		// Mov [EBP + ECX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ECX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x8d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ECX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ECX_8___EDX ()
	{
		// Mov [EBP + ECX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ECX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xcd, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ECX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ECX_1_0x12345678___EDX ()
	{
		// Mov [EBP + ECX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ECX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xd, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ECX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ECX_2_0x12345678___EDX ()
	{
		// Mov [EBP + ECX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ECX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x4d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ECX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ECX_4_0x12345678___EDX ()
	{
		// Mov [EBP + ECX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ECX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x8d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ECX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ECX_8_0x12345678___EDX ()
	{
		// Mov [EBP + ECX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ECX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xcd, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ECX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ECX_1_0xC___EDX ()
	{
		// Mov [EBP + ECX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ECX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xd, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ECX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ECX_2_0xC___EDX ()
	{
		// Mov [EBP + ECX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ECX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x4d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ECX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ECX_4_0xC___EDX ()
	{
		// Mov [EBP + ECX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ECX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x8d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ECX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ECX_8_0xC___EDX ()
	{
		// Mov [EBP + ECX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ECX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xcd, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ECX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ECX_1___EDX ()
	{
		// Mov [DS:EBP + ECX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ECX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xd, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ECX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ECX_2___EDX ()
	{
		// Mov [DS:EBP + ECX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ECX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x4d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ECX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ECX_4___EDX ()
	{
		// Mov [DS:EBP + ECX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ECX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x8d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ECX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ECX_8___EDX ()
	{
		// Mov [DS:EBP + ECX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ECX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xcd, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ECX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ECX_1_0x12345678___EDX ()
	{
		// Mov [DS:EBP + ECX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ECX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xd, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ECX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ECX_2_0x12345678___EDX ()
	{
		// Mov [DS:EBP + ECX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ECX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x4d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ECX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ECX_4_0x12345678___EDX ()
	{
		// Mov [DS:EBP + ECX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ECX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x8d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ECX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ECX_8_0x12345678___EDX ()
	{
		// Mov [DS:EBP + ECX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ECX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xcd, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ECX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ECX_1_0xC___EDX ()
	{
		// Mov [DS:EBP + ECX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ECX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xd, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ECX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ECX_2_0xC___EDX ()
	{
		// Mov [DS:EBP + ECX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ECX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x4d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ECX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ECX_4_0xC___EDX ()
	{
		// Mov [DS:EBP + ECX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ECX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x8d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ECX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ECX_8_0xC___EDX ()
	{
		// Mov [DS:EBP + ECX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ECX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xcd, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ECX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDX_1___EDX ()
	{
		// Mov [EBP + EDX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x15, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDX_2___EDX ()
	{
		// Mov [EBP + EDX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x55, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDX_4___EDX ()
	{
		// Mov [EBP + EDX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x95, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDX_8___EDX ()
	{
		// Mov [EBP + EDX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xd5, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDX_1_0x12345678___EDX ()
	{
		// Mov [EBP + EDX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x15, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDX_2_0x12345678___EDX ()
	{
		// Mov [EBP + EDX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x55, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDX_4_0x12345678___EDX ()
	{
		// Mov [EBP + EDX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x95, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDX_8_0x12345678___EDX ()
	{
		// Mov [EBP + EDX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xd5, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDX_1_0xC___EDX ()
	{
		// Mov [EBP + EDX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x15, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDX_2_0xC___EDX ()
	{
		// Mov [EBP + EDX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x55, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDX_4_0xC___EDX ()
	{
		// Mov [EBP + EDX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x95, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDX_8_0xC___EDX ()
	{
		// Mov [EBP + EDX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xd5, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDX_1___EDX ()
	{
		// Mov [DS:EBP + EDX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x15, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDX_2___EDX ()
	{
		// Mov [DS:EBP + EDX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x55, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDX_4___EDX ()
	{
		// Mov [DS:EBP + EDX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x95, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDX_8___EDX ()
	{
		// Mov [DS:EBP + EDX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xd5, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDX_1_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EDX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x15, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDX_2_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EDX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x55, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDX_4_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EDX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x95, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDX_8_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EDX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xd5, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDX_1_0xC___EDX ()
	{
		// Mov [DS:EBP + EDX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x15, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDX_2_0xC___EDX ()
	{
		// Mov [DS:EBP + EDX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x55, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDX_4_0xC___EDX ()
	{
		// Mov [DS:EBP + EDX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x95, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDX_8_0xC___EDX ()
	{
		// Mov [DS:EBP + EDX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xd5, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ESP_1___EDX ()
	{
		// Mov [EBP + ESP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ESP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x2c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ESP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ESP_1_0x12345678___EDX ()
	{
		// Mov [EBP + ESP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ESP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x2c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ESP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ESP_1_0xC___EDX ()
	{
		// Mov [EBP + ESP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ESP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x2c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ESP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ESP_1___EDX ()
	{
		// Mov [DS:EBP + ESP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ESP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x2c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ESP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ESP_1_0x12345678___EDX ()
	{
		// Mov [DS:EBP + ESP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ESP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x2c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ESP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ESP_1_0xC___EDX ()
	{
		// Mov [DS:EBP + ESP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ESP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x2c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ESP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBP_1___EDX ()
	{
		// Mov [EBP + EBP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x2d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBP_2___EDX ()
	{
		// Mov [EBP + EBP*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBP, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x6d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBP*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBP_4___EDX ()
	{
		// Mov [EBP + EBP*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBP, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xad, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBP*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBP_8___EDX ()
	{
		// Mov [EBP + EBP*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBP, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xed, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBP*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBP_1_0x12345678___EDX ()
	{
		// Mov [EBP + EBP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x2d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBP_2_0x12345678___EDX ()
	{
		// Mov [EBP + EBP*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBP, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x6d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBP*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBP_4_0x12345678___EDX ()
	{
		// Mov [EBP + EBP*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBP, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xad, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBP*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBP_8_0x12345678___EDX ()
	{
		// Mov [EBP + EBP*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBP, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xed, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBP*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBP_1_0xC___EDX ()
	{
		// Mov [EBP + EBP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x2d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBP_2_0xC___EDX ()
	{
		// Mov [EBP + EBP*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBP, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x6d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBP*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBP_4_0xC___EDX ()
	{
		// Mov [EBP + EBP*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBP, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xad, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBP*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EBP_8_0xC___EDX ()
	{
		// Mov [EBP + EBP*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EBP, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xed, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EBP*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBP_1___EDX ()
	{
		// Mov [DS:EBP + EBP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x2d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBP_2___EDX ()
	{
		// Mov [DS:EBP + EBP*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBP, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x6d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBP*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBP_4___EDX ()
	{
		// Mov [DS:EBP + EBP*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBP, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xad, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBP*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBP_8___EDX ()
	{
		// Mov [DS:EBP + EBP*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBP, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xed, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBP*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBP_1_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EBP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x2d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBP_2_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EBP*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBP, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x6d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBP*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBP_4_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EBP*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBP, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xad, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBP*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBP_8_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EBP*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBP, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xed, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBP*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBP_1_0xC___EDX ()
	{
		// Mov [DS:EBP + EBP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x2d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBP_2_0xC___EDX ()
	{
		// Mov [DS:EBP + EBP*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBP, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x6d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBP*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBP_4_0xC___EDX ()
	{
		// Mov [DS:EBP + EBP*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBP, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xad, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBP*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EBP_8_0xC___EDX ()
	{
		// Mov [DS:EBP + EBP*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EBP, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xed, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EBP*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDI_1___EDX ()
	{
		// Mov [EBP + EDI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x3d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDI_2___EDX ()
	{
		// Mov [EBP + EDI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x7d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDI_4___EDX ()
	{
		// Mov [EBP + EDI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xbd, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDI_8___EDX ()
	{
		// Mov [EBP + EDI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xfd, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDI_1_0x12345678___EDX ()
	{
		// Mov [EBP + EDI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x3d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDI_2_0x12345678___EDX ()
	{
		// Mov [EBP + EDI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x7d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDI_4_0x12345678___EDX ()
	{
		// Mov [EBP + EDI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xbd, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDI_8_0x12345678___EDX ()
	{
		// Mov [EBP + EDI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xfd, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDI_1_0xC___EDX ()
	{
		// Mov [EBP + EDI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x3d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDI_2_0xC___EDX ()
	{
		// Mov [EBP + EDI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x7d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDI_4_0xC___EDX ()
	{
		// Mov [EBP + EDI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xbd, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___EDI_8_0xC___EDX ()
	{
		// Mov [EBP + EDI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.EDI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xfd, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + EDI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDI_1___EDX ()
	{
		// Mov [DS:EBP + EDI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x3d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDI_2___EDX ()
	{
		// Mov [DS:EBP + EDI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x7d, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDI_4___EDX ()
	{
		// Mov [DS:EBP + EDI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xbd, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDI_8___EDX ()
	{
		// Mov [DS:EBP + EDI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xfd, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDI_1_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EDI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x3d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDI_2_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EDI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x7d, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDI_4_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EDI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xbd, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDI_8_0x12345678___EDX ()
	{
		// Mov [DS:EBP + EDI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xfd, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDI_1_0xC___EDX ()
	{
		// Mov [DS:EBP + EDI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x3d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDI_2_0xC___EDX ()
	{
		// Mov [DS:EBP + EDI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x7d, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDI_4_0xC___EDX ()
	{
		// Mov [DS:EBP + EDI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xbd, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___EDI_8_0xC___EDX ()
	{
		// Mov [DS:EBP + EDI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.EDI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xfd, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + EDI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ESI_1___EDX ()
	{
		// Mov [EBP + ESI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ESI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x35, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ESI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ESI_2___EDX ()
	{
		// Mov [EBP + ESI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ESI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x75, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ESI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ESI_4___EDX ()
	{
		// Mov [EBP + ESI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ESI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xb5, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ESI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ESI_8___EDX ()
	{
		// Mov [EBP + ESI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ESI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xf5, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ESI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ESI_1_0x12345678___EDX ()
	{
		// Mov [EBP + ESI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ESI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x35, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ESI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ESI_2_0x12345678___EDX ()
	{
		// Mov [EBP + ESI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ESI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x75, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ESI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ESI_4_0x12345678___EDX ()
	{
		// Mov [EBP + ESI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ESI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xb5, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ESI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ESI_8_0x12345678___EDX ()
	{
		// Mov [EBP + ESI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ESI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xf5, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ESI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ESI_1_0xC___EDX ()
	{
		// Mov [EBP + ESI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ESI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x35, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ESI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ESI_2_0xC___EDX ()
	{
		// Mov [EBP + ESI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ESI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x75, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ESI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ESI_4_0xC___EDX ()
	{
		// Mov [EBP + ESI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ESI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xb5, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ESI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EBP___ESI_8_0xC___EDX ()
	{
		// Mov [EBP + ESI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EBP, R32.ESI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xf5, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EBP + ESI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ESI_1___EDX ()
	{
		// Mov [DS:EBP + ESI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ESI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x35, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ESI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ESI_2___EDX ()
	{
		// Mov [DS:EBP + ESI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ESI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x75, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ESI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ESI_4___EDX ()
	{
		// Mov [DS:EBP + ESI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ESI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xb5, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ESI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ESI_8___EDX ()
	{
		// Mov [DS:EBP + ESI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ESI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xf5, 0x0};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ESI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ESI_1_0x12345678___EDX ()
	{
		// Mov [DS:EBP + ESI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ESI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x35, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ESI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ESI_2_0x12345678___EDX ()
	{
		// Mov [DS:EBP + ESI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ESI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x75, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ESI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ESI_4_0x12345678___EDX ()
	{
		// Mov [DS:EBP + ESI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ESI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xb5, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ESI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ESI_8_0x12345678___EDX ()
	{
		// Mov [DS:EBP + ESI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ESI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xf5, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ESI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ESI_1_0xC___EDX ()
	{
		// Mov [DS:EBP + ESI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ESI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x35, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ESI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ESI_2_0xC___EDX ()
	{
		// Mov [DS:EBP + ESI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ESI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x75, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ESI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ESI_4_0xC___EDX ()
	{
		// Mov [DS:EBP + ESI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ESI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xb5, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ESI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EBP___ESI_8_0xC___EDX ()
	{
		// Mov [DS:EBP + ESI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EBP, R32.ESI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xf5, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EBP + ESI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDX ()
	{
		// Mov [EDI], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, null, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x17};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI_0x12345678___EDX ()
	{
		// Mov [EDI+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, null, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x97, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI_0xC___EDX ()
	{
		// Mov [EDI-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, null, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x57, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDX ()
	{
		// Mov [DS:EDI], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, null, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x17};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI_0x12345678___EDX ()
	{
		// Mov [DS:EDI+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, null, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x97, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI_0xC___EDX ()
	{
		// Mov [DS:EDI-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, null, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x57, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EAX_1___EDX ()
	{
		// Mov [EDI + EAX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EAX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x7};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EAX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EAX_2___EDX ()
	{
		// Mov [EDI + EAX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EAX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x47};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EAX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EAX_4___EDX ()
	{
		// Mov [EDI + EAX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EAX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x87};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EAX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EAX_8___EDX ()
	{
		// Mov [EDI + EAX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EAX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xc7};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EAX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EAX_1_0x12345678___EDX ()
	{
		// Mov [EDI + EAX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EAX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x7, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EAX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EAX_2_0x12345678___EDX ()
	{
		// Mov [EDI + EAX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EAX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x47, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EAX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EAX_4_0x12345678___EDX ()
	{
		// Mov [EDI + EAX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EAX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x87, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EAX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EAX_8_0x12345678___EDX ()
	{
		// Mov [EDI + EAX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EAX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xc7, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EAX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EAX_1_0xC___EDX ()
	{
		// Mov [EDI + EAX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EAX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x7, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EAX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EAX_2_0xC___EDX ()
	{
		// Mov [EDI + EAX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EAX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x47, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EAX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EAX_4_0xC___EDX ()
	{
		// Mov [EDI + EAX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EAX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x87, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EAX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EAX_8_0xC___EDX ()
	{
		// Mov [EDI + EAX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EAX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xc7, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EAX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EAX_1___EDX ()
	{
		// Mov [DS:EDI + EAX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EAX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x7};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EAX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EAX_2___EDX ()
	{
		// Mov [DS:EDI + EAX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EAX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x47};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EAX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EAX_4___EDX ()
	{
		// Mov [DS:EDI + EAX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EAX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x87};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EAX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EAX_8___EDX ()
	{
		// Mov [DS:EDI + EAX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EAX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xc7};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EAX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EAX_1_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EAX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EAX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x7, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EAX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EAX_2_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EAX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EAX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x47, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EAX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EAX_4_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EAX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EAX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x87, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EAX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EAX_8_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EAX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EAX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xc7, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EAX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EAX_1_0xC___EDX ()
	{
		// Mov [DS:EDI + EAX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EAX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x7, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EAX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EAX_2_0xC___EDX ()
	{
		// Mov [DS:EDI + EAX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EAX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x47, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EAX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EAX_4_0xC___EDX ()
	{
		// Mov [DS:EDI + EAX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EAX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x87, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EAX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EAX_8_0xC___EDX ()
	{
		// Mov [DS:EDI + EAX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EAX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xc7, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EAX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBX_1___EDX ()
	{
		// Mov [EDI + EBX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x1f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBX_2___EDX ()
	{
		// Mov [EDI + EBX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x5f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBX_4___EDX ()
	{
		// Mov [EDI + EBX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x9f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBX_8___EDX ()
	{
		// Mov [EDI + EBX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xdf};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBX_1_0x12345678___EDX ()
	{
		// Mov [EDI + EBX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x1f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBX_2_0x12345678___EDX ()
	{
		// Mov [EDI + EBX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x5f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBX_4_0x12345678___EDX ()
	{
		// Mov [EDI + EBX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x9f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBX_8_0x12345678___EDX ()
	{
		// Mov [EDI + EBX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xdf, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBX_1_0xC___EDX ()
	{
		// Mov [EDI + EBX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x1f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBX_2_0xC___EDX ()
	{
		// Mov [EDI + EBX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x5f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBX_4_0xC___EDX ()
	{
		// Mov [EDI + EBX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x9f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBX_8_0xC___EDX ()
	{
		// Mov [EDI + EBX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xdf, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBX_1___EDX ()
	{
		// Mov [DS:EDI + EBX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x1f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBX_2___EDX ()
	{
		// Mov [DS:EDI + EBX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x5f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBX_4___EDX ()
	{
		// Mov [DS:EDI + EBX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x9f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBX_8___EDX ()
	{
		// Mov [DS:EDI + EBX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xdf};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBX_1_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EBX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x1f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBX_2_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EBX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x5f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBX_4_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EBX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x9f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBX_8_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EBX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xdf, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBX_1_0xC___EDX ()
	{
		// Mov [DS:EDI + EBX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x1f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBX_2_0xC___EDX ()
	{
		// Mov [DS:EDI + EBX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x5f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBX_4_0xC___EDX ()
	{
		// Mov [DS:EDI + EBX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x9f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBX_8_0xC___EDX ()
	{
		// Mov [DS:EDI + EBX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xdf, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ECX_1___EDX ()
	{
		// Mov [EDI + ECX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ECX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xf};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ECX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ECX_2___EDX ()
	{
		// Mov [EDI + ECX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ECX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x4f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ECX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ECX_4___EDX ()
	{
		// Mov [EDI + ECX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ECX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x8f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ECX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ECX_8___EDX ()
	{
		// Mov [EDI + ECX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ECX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xcf};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ECX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ECX_1_0x12345678___EDX ()
	{
		// Mov [EDI + ECX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ECX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xf, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ECX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ECX_2_0x12345678___EDX ()
	{
		// Mov [EDI + ECX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ECX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x4f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ECX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ECX_4_0x12345678___EDX ()
	{
		// Mov [EDI + ECX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ECX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x8f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ECX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ECX_8_0x12345678___EDX ()
	{
		// Mov [EDI + ECX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ECX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xcf, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ECX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ECX_1_0xC___EDX ()
	{
		// Mov [EDI + ECX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ECX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xf, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ECX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ECX_2_0xC___EDX ()
	{
		// Mov [EDI + ECX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ECX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x4f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ECX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ECX_4_0xC___EDX ()
	{
		// Mov [EDI + ECX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ECX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x8f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ECX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ECX_8_0xC___EDX ()
	{
		// Mov [EDI + ECX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ECX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xcf, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ECX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ECX_1___EDX ()
	{
		// Mov [DS:EDI + ECX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ECX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xf};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ECX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ECX_2___EDX ()
	{
		// Mov [DS:EDI + ECX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ECX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x4f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ECX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ECX_4___EDX ()
	{
		// Mov [DS:EDI + ECX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ECX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x8f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ECX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ECX_8___EDX ()
	{
		// Mov [DS:EDI + ECX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ECX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xcf};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ECX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ECX_1_0x12345678___EDX ()
	{
		// Mov [DS:EDI + ECX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ECX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xf, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ECX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ECX_2_0x12345678___EDX ()
	{
		// Mov [DS:EDI + ECX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ECX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x4f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ECX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ECX_4_0x12345678___EDX ()
	{
		// Mov [DS:EDI + ECX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ECX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x8f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ECX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ECX_8_0x12345678___EDX ()
	{
		// Mov [DS:EDI + ECX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ECX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xcf, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ECX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ECX_1_0xC___EDX ()
	{
		// Mov [DS:EDI + ECX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ECX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xf, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ECX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ECX_2_0xC___EDX ()
	{
		// Mov [DS:EDI + ECX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ECX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x4f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ECX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ECX_4_0xC___EDX ()
	{
		// Mov [DS:EDI + ECX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ECX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x8f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ECX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ECX_8_0xC___EDX ()
	{
		// Mov [DS:EDI + ECX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ECX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xcf, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ECX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDX_1___EDX ()
	{
		// Mov [EDI + EDX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x17};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDX_2___EDX ()
	{
		// Mov [EDI + EDX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x57};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDX_4___EDX ()
	{
		// Mov [EDI + EDX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x97};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDX_8___EDX ()
	{
		// Mov [EDI + EDX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xd7};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDX_1_0x12345678___EDX ()
	{
		// Mov [EDI + EDX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x17, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDX_2_0x12345678___EDX ()
	{
		// Mov [EDI + EDX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x57, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDX_4_0x12345678___EDX ()
	{
		// Mov [EDI + EDX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x97, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDX_8_0x12345678___EDX ()
	{
		// Mov [EDI + EDX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xd7, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDX_1_0xC___EDX ()
	{
		// Mov [EDI + EDX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x17, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDX_2_0xC___EDX ()
	{
		// Mov [EDI + EDX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x57, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDX_4_0xC___EDX ()
	{
		// Mov [EDI + EDX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x97, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDX_8_0xC___EDX ()
	{
		// Mov [EDI + EDX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xd7, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDX_1___EDX ()
	{
		// Mov [DS:EDI + EDX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x17};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDX_2___EDX ()
	{
		// Mov [DS:EDI + EDX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x57};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDX_4___EDX ()
	{
		// Mov [DS:EDI + EDX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x97};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDX_8___EDX ()
	{
		// Mov [DS:EDI + EDX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xd7};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDX_1_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EDX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x17, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDX_2_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EDX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x57, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDX_4_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EDX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x97, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDX_8_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EDX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xd7, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDX_1_0xC___EDX ()
	{
		// Mov [DS:EDI + EDX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x17, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDX_2_0xC___EDX ()
	{
		// Mov [DS:EDI + EDX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x57, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDX_4_0xC___EDX ()
	{
		// Mov [DS:EDI + EDX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x97, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDX_8_0xC___EDX ()
	{
		// Mov [DS:EDI + EDX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xd7, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ESP_1___EDX ()
	{
		// Mov [EDI + ESP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ESP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x3c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ESP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ESP_1_0x12345678___EDX ()
	{
		// Mov [EDI + ESP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ESP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x3c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ESP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ESP_1_0xC___EDX ()
	{
		// Mov [EDI + ESP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ESP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x3c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ESP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ESP_1___EDX ()
	{
		// Mov [DS:EDI + ESP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ESP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x3c};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ESP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ESP_1_0x12345678___EDX ()
	{
		// Mov [DS:EDI + ESP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ESP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x3c, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ESP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ESP_1_0xC___EDX ()
	{
		// Mov [DS:EDI + ESP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ESP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x3c, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ESP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBP_1___EDX ()
	{
		// Mov [EDI + EBP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x2f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBP_2___EDX ()
	{
		// Mov [EDI + EBP*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBP, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x6f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBP*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBP_4___EDX ()
	{
		// Mov [EDI + EBP*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBP, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xaf};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBP*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBP_8___EDX ()
	{
		// Mov [EDI + EBP*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBP, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xef};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBP*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBP_1_0x12345678___EDX ()
	{
		// Mov [EDI + EBP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x2f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBP_2_0x12345678___EDX ()
	{
		// Mov [EDI + EBP*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBP, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x6f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBP*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBP_4_0x12345678___EDX ()
	{
		// Mov [EDI + EBP*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBP, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xaf, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBP*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBP_8_0x12345678___EDX ()
	{
		// Mov [EDI + EBP*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBP, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xef, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBP*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBP_1_0xC___EDX ()
	{
		// Mov [EDI + EBP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x2f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBP_2_0xC___EDX ()
	{
		// Mov [EDI + EBP*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBP, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x6f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBP*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBP_4_0xC___EDX ()
	{
		// Mov [EDI + EBP*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBP, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xaf, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBP*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EBP_8_0xC___EDX ()
	{
		// Mov [EDI + EBP*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EBP, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xef, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EBP*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBP_1___EDX ()
	{
		// Mov [DS:EDI + EBP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x2f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBP_2___EDX ()
	{
		// Mov [DS:EDI + EBP*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBP, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x6f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBP*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBP_4___EDX ()
	{
		// Mov [DS:EDI + EBP*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBP, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xaf};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBP*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBP_8___EDX ()
	{
		// Mov [DS:EDI + EBP*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBP, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xef};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBP*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBP_1_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EBP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x2f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBP_2_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EBP*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBP, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x6f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBP*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBP_4_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EBP*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBP, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xaf, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBP*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBP_8_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EBP*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBP, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xef, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBP*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBP_1_0xC___EDX ()
	{
		// Mov [DS:EDI + EBP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x2f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBP_2_0xC___EDX ()
	{
		// Mov [DS:EDI + EBP*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBP, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x6f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBP*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBP_4_0xC___EDX ()
	{
		// Mov [DS:EDI + EBP*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBP, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xaf, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBP*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EBP_8_0xC___EDX ()
	{
		// Mov [DS:EDI + EBP*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EBP, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xef, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EBP*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDI_1___EDX ()
	{
		// Mov [EDI + EDI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x3f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDI_2___EDX ()
	{
		// Mov [EDI + EDI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x7f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDI_4___EDX ()
	{
		// Mov [EDI + EDI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xbf};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDI_8___EDX ()
	{
		// Mov [EDI + EDI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDI_1_0x12345678___EDX ()
	{
		// Mov [EDI + EDI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x3f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDI_2_0x12345678___EDX ()
	{
		// Mov [EDI + EDI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x7f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDI_4_0x12345678___EDX ()
	{
		// Mov [EDI + EDI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xbf, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDI_8_0x12345678___EDX ()
	{
		// Mov [EDI + EDI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xff, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDI_1_0xC___EDX ()
	{
		// Mov [EDI + EDI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x3f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDI_2_0xC___EDX ()
	{
		// Mov [EDI + EDI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x7f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDI_4_0xC___EDX ()
	{
		// Mov [EDI + EDI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xbf, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___EDI_8_0xC___EDX ()
	{
		// Mov [EDI + EDI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.EDI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xff, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + EDI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDI_1___EDX ()
	{
		// Mov [DS:EDI + EDI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x3f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDI_2___EDX ()
	{
		// Mov [DS:EDI + EDI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x7f};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDI_4___EDX ()
	{
		// Mov [DS:EDI + EDI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xbf};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDI_8___EDX ()
	{
		// Mov [DS:EDI + EDI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xff};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDI_1_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EDI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x3f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDI_2_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EDI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x7f, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDI_4_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EDI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xbf, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDI_8_0x12345678___EDX ()
	{
		// Mov [DS:EDI + EDI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xff, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDI_1_0xC___EDX ()
	{
		// Mov [DS:EDI + EDI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x3f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDI_2_0xC___EDX ()
	{
		// Mov [DS:EDI + EDI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x7f, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDI_4_0xC___EDX ()
	{
		// Mov [DS:EDI + EDI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xbf, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___EDI_8_0xC___EDX ()
	{
		// Mov [DS:EDI + EDI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.EDI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xff, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + EDI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ESI_1___EDX ()
	{
		// Mov [EDI + ESI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ESI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x37};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ESI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ESI_2___EDX ()
	{
		// Mov [EDI + ESI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ESI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x77};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ESI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ESI_4___EDX ()
	{
		// Mov [EDI + ESI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ESI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xb7};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ESI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ESI_8___EDX ()
	{
		// Mov [EDI + ESI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ESI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xf7};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ESI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ESI_1_0x12345678___EDX ()
	{
		// Mov [EDI + ESI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ESI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x37, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ESI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ESI_2_0x12345678___EDX ()
	{
		// Mov [EDI + ESI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ESI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x77, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ESI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ESI_4_0x12345678___EDX ()
	{
		// Mov [EDI + ESI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ESI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xb7, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ESI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ESI_8_0x12345678___EDX ()
	{
		// Mov [EDI + ESI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ESI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xf7, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ESI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ESI_1_0xC___EDX ()
	{
		// Mov [EDI + ESI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ESI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x37, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ESI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ESI_2_0xC___EDX ()
	{
		// Mov [EDI + ESI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ESI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x77, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ESI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ESI_4_0xC___EDX ()
	{
		// Mov [EDI + ESI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ESI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xb7, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ESI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__EDI___ESI_8_0xC___EDX ()
	{
		// Mov [EDI + ESI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.EDI, R32.ESI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xf7, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [EDI + ESI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ESI_1___EDX ()
	{
		// Mov [DS:EDI + ESI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ESI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x37};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ESI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ESI_2___EDX ()
	{
		// Mov [DS:EDI + ESI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ESI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x77};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ESI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ESI_4___EDX ()
	{
		// Mov [DS:EDI + ESI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ESI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xb7};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ESI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ESI_8___EDX ()
	{
		// Mov [DS:EDI + ESI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ESI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xf7};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ESI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ESI_1_0x12345678___EDX ()
	{
		// Mov [DS:EDI + ESI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ESI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x37, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ESI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ESI_2_0x12345678___EDX ()
	{
		// Mov [DS:EDI + ESI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ESI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x77, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ESI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ESI_4_0x12345678___EDX ()
	{
		// Mov [DS:EDI + ESI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ESI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xb7, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ESI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ESI_8_0x12345678___EDX ()
	{
		// Mov [DS:EDI + ESI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ESI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xf7, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ESI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ESI_1_0xC___EDX ()
	{
		// Mov [DS:EDI + ESI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ESI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x37, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ESI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ESI_2_0xC___EDX ()
	{
		// Mov [DS:EDI + ESI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ESI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x77, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ESI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ESI_4_0xC___EDX ()
	{
		// Mov [DS:EDI + ESI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ESI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xb7, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ESI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_EDI___ESI_8_0xC___EDX ()
	{
		// Mov [DS:EDI + ESI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.EDI, R32.ESI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xf7, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:EDI + ESI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDX ()
	{
		// Mov [ESI], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, null, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x16};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI_0x12345678___EDX ()
	{
		// Mov [ESI+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, null, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x96, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI_0xC___EDX ()
	{
		// Mov [ESI-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, null, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x56, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDX ()
	{
		// Mov [DS:ESI], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, null, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x16};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI_0x12345678___EDX ()
	{
		// Mov [DS:ESI+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, null, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x96, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI_0xC___EDX ()
	{
		// Mov [DS:ESI-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, null, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x56, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EAX_1___EDX ()
	{
		// Mov [ESI + EAX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EAX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x6};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EAX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EAX_2___EDX ()
	{
		// Mov [ESI + EAX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EAX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x46};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EAX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EAX_4___EDX ()
	{
		// Mov [ESI + EAX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EAX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x86};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EAX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EAX_8___EDX ()
	{
		// Mov [ESI + EAX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EAX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xc6};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EAX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EAX_1_0x12345678___EDX ()
	{
		// Mov [ESI + EAX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EAX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x6, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EAX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EAX_2_0x12345678___EDX ()
	{
		// Mov [ESI + EAX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EAX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x46, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EAX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EAX_4_0x12345678___EDX ()
	{
		// Mov [ESI + EAX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EAX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x86, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EAX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EAX_8_0x12345678___EDX ()
	{
		// Mov [ESI + EAX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EAX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xc6, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EAX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EAX_1_0xC___EDX ()
	{
		// Mov [ESI + EAX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EAX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x6, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EAX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EAX_2_0xC___EDX ()
	{
		// Mov [ESI + EAX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EAX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x46, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EAX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EAX_4_0xC___EDX ()
	{
		// Mov [ESI + EAX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EAX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x86, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EAX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EAX_8_0xC___EDX ()
	{
		// Mov [ESI + EAX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EAX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xc6, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EAX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EAX_1___EDX ()
	{
		// Mov [DS:ESI + EAX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EAX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x6};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EAX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EAX_2___EDX ()
	{
		// Mov [DS:ESI + EAX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EAX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x46};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EAX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EAX_4___EDX ()
	{
		// Mov [DS:ESI + EAX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EAX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x86};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EAX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EAX_8___EDX ()
	{
		// Mov [DS:ESI + EAX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EAX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xc6};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EAX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EAX_1_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EAX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EAX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x6, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EAX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EAX_2_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EAX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EAX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x46, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EAX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EAX_4_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EAX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EAX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x86, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EAX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EAX_8_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EAX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EAX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xc6, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EAX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EAX_1_0xC___EDX ()
	{
		// Mov [DS:ESI + EAX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EAX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x6, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EAX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EAX_2_0xC___EDX ()
	{
		// Mov [DS:ESI + EAX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EAX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x46, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EAX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EAX_4_0xC___EDX ()
	{
		// Mov [DS:ESI + EAX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EAX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x86, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EAX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EAX_8_0xC___EDX ()
	{
		// Mov [DS:ESI + EAX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EAX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xc6, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EAX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBX_1___EDX ()
	{
		// Mov [ESI + EBX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x1e};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBX_2___EDX ()
	{
		// Mov [ESI + EBX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x5e};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBX_4___EDX ()
	{
		// Mov [ESI + EBX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x9e};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBX_8___EDX ()
	{
		// Mov [ESI + EBX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xde};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBX_1_0x12345678___EDX ()
	{
		// Mov [ESI + EBX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x1e, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBX_2_0x12345678___EDX ()
	{
		// Mov [ESI + EBX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x5e, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBX_4_0x12345678___EDX ()
	{
		// Mov [ESI + EBX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x9e, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBX_8_0x12345678___EDX ()
	{
		// Mov [ESI + EBX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xde, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBX_1_0xC___EDX ()
	{
		// Mov [ESI + EBX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x1e, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBX_2_0xC___EDX ()
	{
		// Mov [ESI + EBX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x5e, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBX_4_0xC___EDX ()
	{
		// Mov [ESI + EBX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x9e, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBX_8_0xC___EDX ()
	{
		// Mov [ESI + EBX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xde, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBX_1___EDX ()
	{
		// Mov [DS:ESI + EBX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x1e};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBX_2___EDX ()
	{
		// Mov [DS:ESI + EBX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x5e};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBX_4___EDX ()
	{
		// Mov [DS:ESI + EBX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x9e};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBX_8___EDX ()
	{
		// Mov [DS:ESI + EBX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xde};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBX_1_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EBX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x1e, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBX_2_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EBX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x5e, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBX_4_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EBX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x9e, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBX_8_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EBX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xde, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBX_1_0xC___EDX ()
	{
		// Mov [DS:ESI + EBX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x1e, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBX_2_0xC___EDX ()
	{
		// Mov [DS:ESI + EBX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x5e, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBX_4_0xC___EDX ()
	{
		// Mov [DS:ESI + EBX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x9e, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBX_8_0xC___EDX ()
	{
		// Mov [DS:ESI + EBX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xde, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ECX_1___EDX ()
	{
		// Mov [ESI + ECX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ECX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xe};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ECX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ECX_2___EDX ()
	{
		// Mov [ESI + ECX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ECX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x4e};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ECX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ECX_4___EDX ()
	{
		// Mov [ESI + ECX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ECX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x8e};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ECX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ECX_8___EDX ()
	{
		// Mov [ESI + ECX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ECX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xce};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ECX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ECX_1_0x12345678___EDX ()
	{
		// Mov [ESI + ECX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ECX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xe, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ECX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ECX_2_0x12345678___EDX ()
	{
		// Mov [ESI + ECX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ECX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x4e, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ECX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ECX_4_0x12345678___EDX ()
	{
		// Mov [ESI + ECX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ECX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x8e, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ECX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ECX_8_0x12345678___EDX ()
	{
		// Mov [ESI + ECX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ECX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xce, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ECX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ECX_1_0xC___EDX ()
	{
		// Mov [ESI + ECX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ECX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xe, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ECX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ECX_2_0xC___EDX ()
	{
		// Mov [ESI + ECX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ECX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x4e, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ECX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ECX_4_0xC___EDX ()
	{
		// Mov [ESI + ECX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ECX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x8e, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ECX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ECX_8_0xC___EDX ()
	{
		// Mov [ESI + ECX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ECX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xce, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ECX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ECX_1___EDX ()
	{
		// Mov [DS:ESI + ECX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ECX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xe};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ECX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ECX_2___EDX ()
	{
		// Mov [DS:ESI + ECX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ECX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x4e};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ECX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ECX_4___EDX ()
	{
		// Mov [DS:ESI + ECX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ECX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x8e};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ECX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ECX_8___EDX ()
	{
		// Mov [DS:ESI + ECX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ECX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xce};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ECX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ECX_1_0x12345678___EDX ()
	{
		// Mov [DS:ESI + ECX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ECX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xe, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ECX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ECX_2_0x12345678___EDX ()
	{
		// Mov [DS:ESI + ECX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ECX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x4e, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ECX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ECX_4_0x12345678___EDX ()
	{
		// Mov [DS:ESI + ECX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ECX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x8e, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ECX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ECX_8_0x12345678___EDX ()
	{
		// Mov [DS:ESI + ECX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ECX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xce, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ECX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ECX_1_0xC___EDX ()
	{
		// Mov [DS:ESI + ECX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ECX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xe, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ECX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ECX_2_0xC___EDX ()
	{
		// Mov [DS:ESI + ECX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ECX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x4e, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ECX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ECX_4_0xC___EDX ()
	{
		// Mov [DS:ESI + ECX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ECX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x8e, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ECX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ECX_8_0xC___EDX ()
	{
		// Mov [DS:ESI + ECX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ECX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xce, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ECX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDX_1___EDX ()
	{
		// Mov [ESI + EDX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x16};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDX_2___EDX ()
	{
		// Mov [ESI + EDX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x56};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDX_4___EDX ()
	{
		// Mov [ESI + EDX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x96};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDX_8___EDX ()
	{
		// Mov [ESI + EDX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xd6};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDX_1_0x12345678___EDX ()
	{
		// Mov [ESI + EDX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x16, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDX_2_0x12345678___EDX ()
	{
		// Mov [ESI + EDX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x56, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDX_4_0x12345678___EDX ()
	{
		// Mov [ESI + EDX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x96, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDX_8_0x12345678___EDX ()
	{
		// Mov [ESI + EDX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xd6, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDX_1_0xC___EDX ()
	{
		// Mov [ESI + EDX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x16, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDX_2_0xC___EDX ()
	{
		// Mov [ESI + EDX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x56, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDX_4_0xC___EDX ()
	{
		// Mov [ESI + EDX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x96, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDX_8_0xC___EDX ()
	{
		// Mov [ESI + EDX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xd6, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDX_1___EDX ()
	{
		// Mov [DS:ESI + EDX*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDX, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x16};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDX*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDX_2___EDX ()
	{
		// Mov [DS:ESI + EDX*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDX, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x56};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDX*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDX_4___EDX ()
	{
		// Mov [DS:ESI + EDX*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDX, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x96};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDX*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDX_8___EDX ()
	{
		// Mov [DS:ESI + EDX*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDX, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xd6};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDX*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDX_1_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EDX*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDX, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x16, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDX*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDX_2_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EDX*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDX, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x56, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDX*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDX_4_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EDX*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDX, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x96, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDX*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDX_8_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EDX*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDX, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xd6, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDX*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDX_1_0xC___EDX ()
	{
		// Mov [DS:ESI + EDX*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDX, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x16, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDX*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDX_2_0xC___EDX ()
	{
		// Mov [DS:ESI + EDX*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDX, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x56, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDX*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDX_4_0xC___EDX ()
	{
		// Mov [DS:ESI + EDX*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDX, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x96, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDX*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDX_8_0xC___EDX ()
	{
		// Mov [DS:ESI + EDX*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDX, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xd6, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDX*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ESP_1___EDX ()
	{
		// Mov [ESI + ESP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ESP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x34};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ESP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ESP_1_0x12345678___EDX ()
	{
		// Mov [ESI + ESP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ESP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x34, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ESP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ESP_1_0xC___EDX ()
	{
		// Mov [ESI + ESP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ESP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x34, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ESP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ESP_1___EDX ()
	{
		// Mov [DS:ESI + ESP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ESP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x34};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ESP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ESP_1_0x12345678___EDX ()
	{
		// Mov [DS:ESI + ESP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ESP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x34, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ESP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ESP_1_0xC___EDX ()
	{
		// Mov [DS:ESI + ESP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ESP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x34, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ESP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBP_1___EDX ()
	{
		// Mov [ESI + EBP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x2e};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBP_2___EDX ()
	{
		// Mov [ESI + EBP*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBP, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x6e};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBP*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBP_4___EDX ()
	{
		// Mov [ESI + EBP*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBP, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xae};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBP*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBP_8___EDX ()
	{
		// Mov [ESI + EBP*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBP, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xee};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBP*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBP_1_0x12345678___EDX ()
	{
		// Mov [ESI + EBP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x2e, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBP_2_0x12345678___EDX ()
	{
		// Mov [ESI + EBP*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBP, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x6e, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBP*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBP_4_0x12345678___EDX ()
	{
		// Mov [ESI + EBP*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBP, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xae, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBP*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBP_8_0x12345678___EDX ()
	{
		// Mov [ESI + EBP*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBP, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xee, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBP*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBP_1_0xC___EDX ()
	{
		// Mov [ESI + EBP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x2e, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBP_2_0xC___EDX ()
	{
		// Mov [ESI + EBP*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBP, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x6e, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBP*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBP_4_0xC___EDX ()
	{
		// Mov [ESI + EBP*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBP, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xae, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBP*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EBP_8_0xC___EDX ()
	{
		// Mov [ESI + EBP*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EBP, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xee, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EBP*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBP_1___EDX ()
	{
		// Mov [DS:ESI + EBP*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBP, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x2e};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBP*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBP_2___EDX ()
	{
		// Mov [DS:ESI + EBP*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBP, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x6e};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBP*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBP_4___EDX ()
	{
		// Mov [DS:ESI + EBP*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBP, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xae};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBP*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBP_8___EDX ()
	{
		// Mov [DS:ESI + EBP*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBP, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xee};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBP*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBP_1_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EBP*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBP, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x2e, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBP*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBP_2_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EBP*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBP, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x6e, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBP*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBP_4_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EBP*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBP, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xae, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBP*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBP_8_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EBP*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBP, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xee, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBP*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBP_1_0xC___EDX ()
	{
		// Mov [DS:ESI + EBP*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBP, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x2e, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBP*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBP_2_0xC___EDX ()
	{
		// Mov [DS:ESI + EBP*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBP, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x6e, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBP*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBP_4_0xC___EDX ()
	{
		// Mov [DS:ESI + EBP*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBP, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xae, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBP*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EBP_8_0xC___EDX ()
	{
		// Mov [DS:ESI + EBP*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EBP, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xee, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EBP*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDI_1___EDX ()
	{
		// Mov [ESI + EDI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x3e};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDI_2___EDX ()
	{
		// Mov [ESI + EDI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x7e};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDI_4___EDX ()
	{
		// Mov [ESI + EDI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xbe};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDI_8___EDX ()
	{
		// Mov [ESI + EDI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xfe};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDI_1_0x12345678___EDX ()
	{
		// Mov [ESI + EDI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x3e, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDI_2_0x12345678___EDX ()
	{
		// Mov [ESI + EDI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x7e, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDI_4_0x12345678___EDX ()
	{
		// Mov [ESI + EDI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xbe, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDI_8_0x12345678___EDX ()
	{
		// Mov [ESI + EDI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xfe, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDI_1_0xC___EDX ()
	{
		// Mov [ESI + EDI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x3e, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDI_2_0xC___EDX ()
	{
		// Mov [ESI + EDI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x7e, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDI_4_0xC___EDX ()
	{
		// Mov [ESI + EDI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xbe, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___EDI_8_0xC___EDX ()
	{
		// Mov [ESI + EDI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.EDI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xfe, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + EDI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDI_1___EDX ()
	{
		// Mov [DS:ESI + EDI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x3e};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDI_2___EDX ()
	{
		// Mov [DS:ESI + EDI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x7e};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDI_4___EDX ()
	{
		// Mov [DS:ESI + EDI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xbe};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDI_8___EDX ()
	{
		// Mov [DS:ESI + EDI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xfe};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDI_1_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EDI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x3e, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDI_2_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EDI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x7e, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDI_4_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EDI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xbe, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDI_8_0x12345678___EDX ()
	{
		// Mov [DS:ESI + EDI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xfe, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDI_1_0xC___EDX ()
	{
		// Mov [DS:ESI + EDI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x3e, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDI_2_0xC___EDX ()
	{
		// Mov [DS:ESI + EDI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x7e, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDI_4_0xC___EDX ()
	{
		// Mov [DS:ESI + EDI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xbe, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___EDI_8_0xC___EDX ()
	{
		// Mov [DS:ESI + EDI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.EDI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xfe, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + EDI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ESI_1___EDX ()
	{
		// Mov [ESI + ESI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ESI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x36};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ESI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ESI_2___EDX ()
	{
		// Mov [ESI + ESI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ESI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0x76};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ESI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ESI_4___EDX ()
	{
		// Mov [ESI + ESI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ESI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xb6};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ESI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ESI_8___EDX ()
	{
		// Mov [ESI + ESI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ESI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x14, 0xf6};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ESI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ESI_1_0x12345678___EDX ()
	{
		// Mov [ESI + ESI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ESI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x36, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ESI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ESI_2_0x12345678___EDX ()
	{
		// Mov [ESI + ESI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ESI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0x76, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ESI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ESI_4_0x12345678___EDX ()
	{
		// Mov [ESI + ESI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ESI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xb6, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ESI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ESI_8_0x12345678___EDX ()
	{
		// Mov [ESI + ESI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ESI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x94, 0xf6, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ESI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ESI_1_0xC___EDX ()
	{
		// Mov [ESI + ESI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ESI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x36, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ESI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ESI_2_0xC___EDX ()
	{
		// Mov [ESI + ESI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ESI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0x76, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ESI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ESI_4_0xC___EDX ()
	{
		// Mov [ESI + ESI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ESI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xb6, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ESI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__ESI___ESI_8_0xC___EDX ()
	{
		// Mov [ESI + ESI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(null, R32.ESI, R32.ESI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x89, 0x54, 0xf6, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [ESI + ESI*8-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ESI_1___EDX ()
	{
		// Mov [DS:ESI + ESI*1], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ESI, 0),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x36};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ESI*1], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ESI_2___EDX ()
	{
		// Mov [DS:ESI + ESI*2], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ESI, 1),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0x76};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ESI*2], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ESI_4___EDX ()
	{
		// Mov [DS:ESI + ESI*4], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ESI, 2),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xb6};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ESI*4], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ESI_8___EDX ()
	{
		// Mov [DS:ESI + ESI*8], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ESI, 3),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x14, 0xf6};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ESI*8], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ESI_1_0x12345678___EDX ()
	{
		// Mov [DS:ESI + ESI*1+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ESI, 0, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x36, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ESI*1+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ESI_2_0x12345678___EDX ()
	{
		// Mov [DS:ESI + ESI*2+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ESI, 1, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0x76, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ESI*2+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ESI_4_0x12345678___EDX ()
	{
		// Mov [DS:ESI + ESI*4+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ESI, 2, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xb6, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ESI*4+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ESI_8_0x12345678___EDX ()
	{
		// Mov [DS:ESI + ESI*8+0x12345678], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ESI, 3, +0x12345678),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x94, 0xf6, 0x78, 0x56, 0x34, 0x12};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ESI*8+0x12345678], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ESI_1_0xC___EDX ()
	{
		// Mov [DS:ESI + ESI*1-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ESI, 0, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x36, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ESI*1-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ESI_2_0xC___EDX ()
	{
		// Mov [DS:ESI + ESI*2-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ESI, 1, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0x76, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ESI*2-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ESI_4_0xC___EDX ()
	{
		// Mov [DS:ESI + ESI*4-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ESI, 2, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xb6, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ESI*4-0xC], EDX' failed.");
	}
	
	[Test]
	public void Mov__DS_ESI___ESI_8_0xC___EDX ()
	{
		// Mov [DS:ESI + ESI*8-0xC], EDX
		MemoryStream memoryStream = new MemoryStream();
		Assembly asm = new Assembly();
		asm.MOV (new DWordMemory(Seg.DS, R32.ESI, R32.ESI, 3, -0xC),  R32.EDX);
		asm.Encode(memoryStream);
		byte[] target = new byte[] {0x3e, 0x89, 0x54, 0xf6, 0xf4};
		Assert.IsTrue(CompareData(memoryStream, target), "'Mov [DS:ESI + ESI*8-0xC], EDX' failed.");
	}
}
*/