/**
 *  (C) 2006-2007 Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
 * 
 */

.assembly extern mscorlib
{
}

.namespace AOTTests
{
	.class public ansi auto SHL extends [mscorlib]System.Object
	{
		.method public static int32 Testu8u4u8(uint64 p0, uint32 p1, uint64 p2) cil managed
		{
			.maxstack 3
			
			ldarg p0
			ldarg p1
			shl
			
			ldarg p2
			ceq
			brtrue ok
			
			ldc.i4 0
			ret
			
			ok:
			ldc.i4 1
			ret
		}
		.method public static int32 CMP0() cil managed
		{
			.maxstack 3
			
			ldc.i8 0xFFFFFFFFFFFFFFFF
			ldc.i4 0x3F
			ldc.i8 0x8000000000000000
			call int32 AOTTests.SHL::Testu8u4u8(uint64, uint32, uint64)
			
			ret
		}
	}
}
