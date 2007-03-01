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
	.class public ansi auto ADD extends [mscorlib]System.Object
	{
		.method public static int32 CMP0() cil managed
		{
			.maxstack 3
			
			ldc.i4 0x80000000
			ldc.i4 0x80000000
			add
			
			ldc.i4 0
			ceq
			brtrue ok
			
			ldc.i4 0
			ret
			
			ok:
			ldc.i4 1
			ret
		}
		.method public static int32 CMP1() cil managed
		{
			.maxstack 3
			
			ldc.i4 1
			ldc.i4 2
			add
			
			ldc.i4 3
			ceq
			brtrue ok
			
			ldc.i4 0
			ret
			
			ok:
			ldc.i4 1
			ret
		}
		.method public static int32 CMP2() cil managed
		{
			.maxstack 3
			
			ldc.i4 -1
			ldc.i4 1
			add
			
			ldc.i4 0
			ceq
			brtrue ok
			
			ldc.i4 0
			ret
			
			ok:
			ldc.i4 1
			ret
		}
		.method public static int32 CMP3() cil managed
		{
			.maxstack 3
			
			ldc.i8 1
			ldc.i8 2
			add
			
			ldc.i8 3
			ceq
			brtrue ok
			
			ldc.i4 0
			ret
			
			ok:
			ldc.i4 1
			ret
		}
	}
	.class public ansi auto SHL extends [mscorlib]System.Object
	{
		.method public static int32 CMP0() cil managed
		{
			.maxstack 3
			
			ldc.i4 1
			ldc.i4 0x1F
			shl
			
			ldc.i4 0x80000000
			ceq
			brtrue ok
			
			ldc.i4 0
			ret
			
			ok:
			ldc.i4 1
			ret
		}
		.method public static int32 CMP1() cil managed
		{
			.maxstack 3
			
			ldc.i4 0xFFFFFFFF
			ldc.i4 0x0000003F
			shl
			
			ldc.i4 0x80000000
			ceq
			brtrue ok
			
			ldc.i4 0
			ret
			
			ok:
			ldc.i4 1
			ret
		}
		.method public static int32 CMP2() cil managed
		{
			.maxstack 3
			
			ldc.i4 1
			ldc.i4 0x20
			shl
			
			ldc.i4 1
			ceq
			brtrue ok
			
			ldc.i4 0
			ret
			
			ok:
			ldc.i4 1
			ret
		}
		.method public static int32 CMP3() cil managed
		{
			.maxstack 3
			
			ldc.i4 1
			ldc.i4 0x21
			shl
			
			ldc.i4 2
			ceq
			brtrue ok
			
			ldc.i4 0
			ret
			
			ok:
			ldc.i4 1
			ret
		}
		.method public static int32 CMP4() cil managed
		{
			.maxstack 3
			
			ldc.i4 1
			ldc.i4 1
			shl
			
			ldc.i4 2
			ceq
			brtrue ok
			
			ldc.i4 0
			ret
			
			ok:
			ldc.i4 1
			ret
		}
		.method public static int32 CMP5() cil managed
		{
			.maxstack 3
			
			ldc.i4 2
			ldc.i4 1
			shl
			
			ldc.i4 4
			ceq
			brtrue ok
			
			ldc.i4 0
			ret
			
			ok:
			ldc.i4 1
			ret
		}
		.method public static int32 CMP6() cil managed
		{
			.maxstack 3
			
			ldc.i4 4
			ldc.i4 1
			shl
			
			ldc.i4 8
			ceq
			brtrue ok
			
			ldc.i4 0
			ret
			
			ok:
			ldc.i4 1
			ret
		}
		.method public static int32 CMP7() cil managed
		{
			.maxstack 3
			
			ldc.i4 1
			ldc.i4 2
			shl
			
			ldc.i4 4
			ceq
			brtrue ok
			
			ldc.i4 0
			ret
			
			ok:
			ldc.i4 1
			ret
		}
	}
	.class public ansi auto SUB extends [mscorlib]System.Object
	{
		.method public static int32 CMP0() cil managed
		{
			.maxstack 3
			
			ldc.i4 1
			ldc.i4 1
			sub
			
			ldc.i4 0
			ceq
			brtrue ok
			
			ldc.i4 0
			ret
			
			ok:
			ldc.i4 1
			ret
		}
	}
}
