.assembly extern mscorlib
{
}

.assembly AOTTests
{
    .ver 1:0:0:0
}

.module AOTTests.dll

.namespace AOTTests
{
	.class public ansi auto Add extends [mscorlib]System.Object
	{
		.method public static int64 AddI8R4(int64, float32) cil managed
		{
			.maxstack 3
			
			ldarg.0
			ldarg.1
			add
			conv.i8
			ret
		}
		
		.method public static int64 Test1() cil managed
		{
			.maxstack 3
			
			ldc.i8 0x5555555555555555
			ldc.r4 1234567
			call int64 AOTTests.Add::AddI8R4(int64, float32)
			ret
		}
		
		.method public static int32 CMP1() cil managed
		{
			.maxstack 3

			call int64 AOTTests.Add::Test1()
			ldc.i8 0xAAAAAAAA9EEC098D
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