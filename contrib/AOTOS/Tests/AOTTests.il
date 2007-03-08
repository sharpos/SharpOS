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
	.class public ansi auto SHR extends [mscorlib]System.Object
	{
		.method public static int32 CMP0() cil managed
		{
			.maxstack 3
			
			ldc.i4 2
			ldc.i4 1
			shr
			
			ldc.i4 1
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
			
			ldc.i4 8
			ldc.i4 3
			shr
			
			ldc.i4 1
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
			
			ldc.i4 0x80000000
			ldc.i4 0x20
			shr
			
			ldc.i4 0x80000000
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
			
			ldc.i4 0x80000000
			ldc.i4 0x1f
			shr
			
			ldc.i4 0xFFFFFFFF
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
			
			ldc.i4 0x80000000
			ldc.i4 1
			shr
			
			ldc.i4 0xC0000000
			ceq
			brtrue ok
			
			ldc.i4 0
			ret
			
			ok:
			ldc.i4 1
			ret
		}
	}
	.class public ansi auto SHR.UN extends [mscorlib]System.Object
	{
		.method public static int32 CMP0() cil managed
		{
			.maxstack 3
			
			ldc.i4 2
			ldc.i4 1
			shr.un
			
			ldc.i4 1
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
			
			ldc.i4 0x80000000
			ldc.i4 0x1f
			shr.un
			
			ldc.i4 1
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
			
			ldc.i4 0x80000000
			ldc.i4 1
			shr.un
			
			ldc.i4 0x40000000
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
			
			ldc.i8 2
			ldc.i4 1
			shr.un
			
			ldc.i8 1
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
			
			ldc.i8 8
			ldc.i4 3
			shr.un
			
			ldc.i8 1
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
