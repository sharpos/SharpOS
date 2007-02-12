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
		.method public static int32 AddI4I4(int32, int32) cil managed
		{
			.maxstack 3
			
			ldarg.0
			ldarg.1
			add
			ret
		}
		
		.method public static int32 Test0() cil managed
		{
			.maxstack 3
			
			ldc.i4 1
			ldc.i4 2
			call int32 AOTTests.Add::AddI4I4(int32, int32)
			ret
		}
		
		.method public static int32 CMP0() cil managed
		{
			.maxstack 3

			call int32 AOTTests.Add::Test0()
			ldc.i4 3
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