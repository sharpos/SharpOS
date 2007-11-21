//
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

.assembly extern mscorlib
{
}

.namespace SharpOS.Kernel.Tests.IL
{
	.class public ansi auto SHR extends [mscorlib]System.Object
	{
		.method public static int32 Testi8i4i8(int64 p0, int32 p1, int64 p2) cil managed
		{
			.maxstack 3
			
			ldarg p0
			ldarg p1
			shr
			
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
			
			ldc.i8 2
			ldc.i4 1
			ldc.i8 1
			call int32 SharpOS.Kernel.Tests.IL.SHR::Testi8i4i8(int64, int32, int64)
			
			ret
		}
		.method public static int32 CMP1() cil managed
		{
			.maxstack 3
			
			ldc.i8 0x8000000000000000
			ldc.i4 0x3F
			ldc.i8 0xFFFFFFFFFFFFFFFF
			call int32 SharpOS.Kernel.Tests.IL.SHR::Testi8i4i8(int64, int32, int64)
			
			ret
		}
		.method public static int32 CMP2() cil managed
		{
			.maxstack 3
			
			ldc.i8 0x7FFFFFFFFFFFFFFF
			ldc.i4 0x3F
			ldc.i8 0
			call int32 SharpOS.Kernel.Tests.IL.SHR::Testi8i4i8(int64, int32, int64)
			
			ret
		}
		.method public static int32 CMP3() cil managed
		{
			.maxstack 3
			
			ldc.i8 0x7FFFFFFFFFFFFFFF
			ldc.i4 0x20
			ldc.i8 0x7FFFFFFF
			call int32 SharpOS.Kernel.Tests.IL.SHR::Testi8i4i8(int64, int32, int64)
			
			ret
		}
		.method public static int32 CMP4() cil managed
		{
			.maxstack 3
			
			ldc.i8 0x7FFFFFFFFFFFFFFF
			ldc.i4 0x1F
			ldc.i8 0xFFFFFFFF
			call int32 SharpOS.Kernel.Tests.IL.SHR::Testi8i4i8(int64, int32, int64)
			
			ret
		}
		.method public static int32 CMP5() cil managed
		{
			.maxstack 3
			
			ldc.i8 0x8000000000000000
			ldc.i4 0x21
			ldc.i8 0xFFFFFFFFC0000000
			call int32 SharpOS.Kernel.Tests.IL.SHR::Testi8i4i8(int64, int32, int64)
			
			ret
		}
		.method public static int32 Testi4i4i4(int32 p0, int32 p1, int32 p2) cil managed
		{
			.maxstack 3
			
			ldarg p0
			ldarg p1
			shr
			
			ldarg p2
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
			
			ldc.i4 2
			ldc.i4 1
			ldc.i4 1
			call int32 SharpOS.Kernel.Tests.IL.SHR::Testi4i4i4(int32, int32, int32)
			
			ret
		}
		.method public static int32 CMP7() cil managed
		{
			.maxstack 3
			
			ldc.i4 8
			ldc.i4 3
			ldc.i4 1
			call int32 SharpOS.Kernel.Tests.IL.SHR::Testi4i4i4(int32, int32, int32)
			
			ret
		}
		.method public static int32 CMP8() cil managed
		{
			.maxstack 3
			
			ldc.i4 0x80000000
			ldc.i4 0x20
			ldc.i4 0x80000000
			call int32 SharpOS.Kernel.Tests.IL.SHR::Testi4i4i4(int32, int32, int32)
			
			ret
		}
		.method public static int32 CMP9() cil managed
		{
			.maxstack 3
			
			ldc.i4 0x80000000
			ldc.i4 0x1F
			ldc.i4 0xFFFFFFFF
			call int32 SharpOS.Kernel.Tests.IL.SHR::Testi4i4i4(int32, int32, int32)
			
			ret
		}
		.method public static int32 CMP10() cil managed
		{
			.maxstack 3
			
			ldc.i4 0x80000000
			ldc.i4 1
			ldc.i4 0xC0000000
			call int32 SharpOS.Kernel.Tests.IL.SHR::Testi4i4i4(int32, int32, int32)
			
			ret
		}
	}
	.class public ansi auto SHL extends [mscorlib]System.Object
	{
		.method public static int32 Testi4i4i4(int32 p0, int32 p1, int32 p2) cil managed
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
			
			ldc.i4 1
			ldc.i4 0x1F
			ldc.i4 0x80000000
			call int32 SharpOS.Kernel.Tests.IL.SHL::Testi4i4i4(int32, int32, int32)
			
			ret
		}
		.method public static int32 CMP1() cil managed
		{
			.maxstack 3
			
			ldc.i4 0xFFFFFFFF
			ldc.i4 0x0000003F
			ldc.i4 0x80000000
			call int32 SharpOS.Kernel.Tests.IL.SHL::Testi4i4i4(int32, int32, int32)
			
			ret
		}
		.method public static int32 CMP2() cil managed
		{
			.maxstack 3
			
			ldc.i4 1
			ldc.i4 0x20
			ldc.i4 1
			call int32 SharpOS.Kernel.Tests.IL.SHL::Testi4i4i4(int32, int32, int32)
			
			ret
		}
		.method public static int32 CMP3() cil managed
		{
			.maxstack 3
			
			ldc.i4 1
			ldc.i4 0x21
			ldc.i4 2
			call int32 SharpOS.Kernel.Tests.IL.SHL::Testi4i4i4(int32, int32, int32)
			
			ret
		}
		.method public static int32 CMP4() cil managed
		{
			.maxstack 3
			
			ldc.i4 1
			ldc.i4 1
			ldc.i4 2
			call int32 SharpOS.Kernel.Tests.IL.SHL::Testi4i4i4(int32, int32, int32)
			
			ret
		}
		.method public static int32 CMP5() cil managed
		{
			.maxstack 3
			
			ldc.i4 2
			ldc.i4 1
			ldc.i4 4
			call int32 SharpOS.Kernel.Tests.IL.SHL::Testi4i4i4(int32, int32, int32)
			
			ret
		}
		.method public static int32 CMP6() cil managed
		{
			.maxstack 3
			
			ldc.i4 4
			ldc.i4 1
			ldc.i4 8
			call int32 SharpOS.Kernel.Tests.IL.SHL::Testi4i4i4(int32, int32, int32)
			
			ret
		}
		.method public static int32 CMP7() cil managed
		{
			.maxstack 3
			
			ldc.i4 1
			ldc.i4 2
			ldc.i4 4
			call int32 SharpOS.Kernel.Tests.IL.SHL::Testi4i4i4(int32, int32, int32)
			
			ret
		}
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
		.method public static int32 CMP8() cil managed
		{
			.maxstack 3
			
			ldc.i8 0xFFFFFFFFFFFFFFFF
			ldc.i4 0x3F
			ldc.i8 0x8000000000000000
			call int32 SharpOS.Kernel.Tests.IL.SHL::Testu8u4u8(uint64, uint32, uint64)
			
			ret
		}
		.method public static int32 CMP9() cil managed
		{
			.maxstack 3
			
			ldc.i8 0xFFFFFFFFFFFFFFFF
			ldc.i4 0x20
			ldc.i8 0xFFFFFFFF00000000
			call int32 SharpOS.Kernel.Tests.IL.SHL::Testu8u4u8(uint64, uint32, uint64)
			
			ret
		}
		.method public static int32 CMP10() cil managed
		{
			.maxstack 3
			
			ldc.i8 0xFFFFFFFFFFFFFFFF
			ldc.i4 0x21
			ldc.i8 0xFFFFFFFE00000000
			call int32 SharpOS.Kernel.Tests.IL.SHL::Testu8u4u8(uint64, uint32, uint64)
			
			ret
		}
		.method public static int32 CMP11() cil managed
		{
			.maxstack 3
			
			ldc.i8 0xFFFFFFFFFFFFFFFF
			ldc.i4 0x7F
			ldc.i8 0x8000000000000000
			call int32 SharpOS.Kernel.Tests.IL.SHL::Testu8u4u8(uint64, uint32, uint64)
			
			ret
		}
		.method public static int32 CMP12() cil managed
		{
			.maxstack 3
			
			ldc.i8 0xFFFFFFFFFFFFFFFF
			ldc.i4 0xFF
			ldc.i8 0x8000000000000000
			call int32 SharpOS.Kernel.Tests.IL.SHL::Testu8u4u8(uint64, uint32, uint64)
			
			ret
		}
		.method public static int32 CMP13() cil managed
		{
			.maxstack 3
			
			ldc.i8 0xFFFFFFFFFFFFFFFF
			ldc.i4 0x3F
			ldc.i8 0x8000000000000000
			call int32 SharpOS.Kernel.Tests.IL.SHL::Testu8u4u8(uint64, uint32, uint64)
			
			ret
		}
		.method public static int32 CMP14() cil managed
		{
			.maxstack 3
			
			ldc.i8 0xFFFFFFFFFFFFFFFF
			ldc.i4 0x1F
			ldc.i8 0xFFFFFFFF80000000
			call int32 SharpOS.Kernel.Tests.IL.SHL::Testu8u4u8(uint64, uint32, uint64)
			
			ret
		}
		.method public static int32 CMP15() cil managed
		{
			.maxstack 3
			
			ldc.i8 0xFFFFFFFFFFFFFFFF
			ldc.i4 0x20
			ldc.i8 0xFFFFFFFF00000000
			call int32 SharpOS.Kernel.Tests.IL.SHL::Testu8u4u8(uint64, uint32, uint64)
			
			ret
		}
		.method public static int32 CMP16() cil managed
		{
			.maxstack 3
			
			ldc.i8 0xFFFFFFFFFFFFFFFF
			ldc.i4 0x24
			ldc.i8 0xFFFFFFF000000000
			call int32 SharpOS.Kernel.Tests.IL.SHL::Testu8u4u8(uint64, uint32, uint64)
			
			ret
		}
		.method public static int32 CMP17() cil managed
		{
			.maxstack 3
			
			ldc.i8 1
			ldc.i4 1
			ldc.i8 2
			call int32 SharpOS.Kernel.Tests.IL.SHL::Testu8u4u8(uint64, uint32, uint64)
			
			ret
		}
	}
	.class public ansi auto ADD extends [mscorlib]System.Object
	{
		.method public static int32 Testi4i4i4(int32 p0, int32 p1, int32 p2) cil managed
		{
			.maxstack 3
			
			ldarg p0
			ldarg p1
			add
			
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
			
			ldc.i4 0x80000000
			ldc.i4 0x80000000
			ldc.i4 0
			call int32 SharpOS.Kernel.Tests.IL.ADD::Testi4i4i4(int32, int32, int32)
			
			ret
		}
		.method public static int32 CMP1() cil managed
		{
			.maxstack 3
			
			ldc.i4 1
			ldc.i4 2
			ldc.i4 3
			call int32 SharpOS.Kernel.Tests.IL.ADD::Testi4i4i4(int32, int32, int32)
			
			ret
		}
		.method public static int32 CMP2() cil managed
		{
			.maxstack 3
			
			ldc.i4 -1
			ldc.i4 1
			ldc.i4 0
			call int32 SharpOS.Kernel.Tests.IL.ADD::Testi4i4i4(int32, int32, int32)
			
			ret
		}
		.method public static int32 Testi8i8i8(int64 p0, int64 p1, int64 p2) cil managed
		{
			.maxstack 3
			
			ldarg p0
			ldarg p1
			add
			
			ldarg p2
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
			ldc.i8 3
			call int32 SharpOS.Kernel.Tests.IL.ADD::Testi8i8i8(int64, int64, int64)
			
			ret
		}
		.method public static int32 CMP4() cil managed
		{
			.maxstack 3
			
			ldc.i8 0x8000000000000000
			ldc.i8 0x7FFFFFFFFFFFFFFF
			ldc.i8 0xFFFFFFFFFFFFFFFF
			call int32 SharpOS.Kernel.Tests.IL.ADD::Testi8i8i8(int64, int64, int64)
			
			ret
		}
		.method public static int32 CMP5() cil managed
		{
			.maxstack 3
			
			ldc.i8 0x8000000000000000
			ldc.i8 0xFFFFFFFFFFFFFFFF
			ldc.i8 0x7FFFFFFFFFFFFFFF
			call int32 SharpOS.Kernel.Tests.IL.ADD::Testi8i8i8(int64, int64, int64)
			
			ret
		}
	}
	.class public ansi auto SHRUN extends [mscorlib]System.Object
	{
		.method public static int32 Testi8i4i8(int64 p0, int32 p1, int64 p2) cil managed
		{
			.maxstack 3
			
			ldarg p0
			ldarg p1
			shr.un
			
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
			ldc.i8 1
			call int32 SharpOS.Kernel.Tests.IL.SHRUN::Testi8i4i8(int64, int32, int64)
			
			ret
		}
		.method public static int32 CMP1() cil managed
		{
			.maxstack 3
			
			ldc.i8 0xFFFFFFFFFFFFFFFF
			ldc.i4 1
			ldc.i8 0x7FFFFFFFFFFFFFFF
			call int32 SharpOS.Kernel.Tests.IL.SHRUN::Testi8i4i8(int64, int32, int64)
			
			ret
		}
		.method public static int32 CMP2() cil managed
		{
			.maxstack 3
			
			ldc.i8 0xFFFFFFFFFFFFFFFF
			ldc.i4 0x21
			ldc.i8 0x7FFFFFFF
			call int32 SharpOS.Kernel.Tests.IL.SHRUN::Testi8i4i8(int64, int32, int64)
			
			ret
		}
		.method public static int32 CMP3() cil managed
		{
			.maxstack 3
			
			ldc.i8 0x8000000000000000
			ldc.i4 1
			ldc.i8 0x4000000000000000
			call int32 SharpOS.Kernel.Tests.IL.SHRUN::Testi8i4i8(int64, int32, int64)
			
			ret
		}
		.method public static int32 CMP4() cil managed
		{
			.maxstack 3
			
			ldc.i8 0x8000000000000000
			ldc.i4 0x3F
			ldc.i8 1
			call int32 SharpOS.Kernel.Tests.IL.SHRUN::Testi8i4i8(int64, int32, int64)
			
			ret
		}
		.method public static int32 CMP5() cil managed
		{
			.maxstack 3
			
			ldc.i8 0x80000000
			ldc.i4 0x1F
			ldc.i8 1
			call int32 SharpOS.Kernel.Tests.IL.SHRUN::Testi8i4i8(int64, int32, int64)
			
			ret
		}
		.method public static int32 CMP6() cil managed
		{
			.maxstack 3
			
			ldc.i8 0x80000000
			ldc.i4 1
			ldc.i8 0x40000000
			call int32 SharpOS.Kernel.Tests.IL.SHRUN::Testi8i4i8(int64, int32, int64)
			
			ret
		}
		.method public static int32 CMP7() cil managed
		{
			.maxstack 3
			
			ldc.i8 2
			ldc.i4 1
			ldc.i8 1
			call int32 SharpOS.Kernel.Tests.IL.SHRUN::Testi8i4i8(int64, int32, int64)
			
			ret
		}
		.method public static int32 CMP8() cil managed
		{
			.maxstack 3
			
			ldc.i8 8
			ldc.i4 3
			ldc.i8 1
			call int32 SharpOS.Kernel.Tests.IL.SHRUN::Testi8i4i8(int64, int32, int64)
			
			ret
		}
		.method public static int32 Testi4i4i4(int32 p0, int32 p1, int32 p2) cil managed
		{
			.maxstack 3
			
			ldarg p0
			ldarg p1
			shr.un
			
			ldarg p2
			ceq
			brtrue ok
			
			ldc.i4 0
			ret
			
			ok:
			ldc.i4 1
			ret
		}
		.method public static int32 CMP9() cil managed
		{
			.maxstack 3
			
			ldc.i4 2
			ldc.i4 1
			ldc.i4 1
			call int32 SharpOS.Kernel.Tests.IL.SHRUN::Testi4i4i4(int32, int32, int32)
			
			ret
		}
		.method public static int32 CMP10() cil managed
		{
			.maxstack 3
			
			ldc.i4 0x80000000
			ldc.i4 0x1F
			ldc.i4 1
			call int32 SharpOS.Kernel.Tests.IL.SHRUN::Testi4i4i4(int32, int32, int32)
			
			ret
		}
		.method public static int32 CMP11() cil managed
		{
			.maxstack 3
			
			ldc.i4 0x80000000
			ldc.i4 1
			ldc.i4 0x40000000
			call int32 SharpOS.Kernel.Tests.IL.SHRUN::Testi4i4i4(int32, int32, int32)
			
			ret
		}
	}
	.class public ansi auto SUB extends [mscorlib]System.Object
	{
		.method public static int32 Testi4i4i4(int32 p0, int32 p1, int32 p2) cil managed
		{
			.maxstack 3
			
			ldarg p0
			ldarg p1
			sub
			
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
			
			ldc.i4 1
			ldc.i4 1
			ldc.i4 0
			call int32 SharpOS.Kernel.Tests.IL.SUB::Testi4i4i4(int32, int32, int32)
			
			ret
		}
	}
}
