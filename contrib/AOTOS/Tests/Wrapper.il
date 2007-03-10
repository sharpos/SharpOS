/**
 *  (C) 2006-2007 Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
 * 
 */

.assembly extern mscorlib
{
}

.class public ansi auto Wrapper extends [mscorlib]System.Object
{
	.method public hidebysig specialname rtspecialname instance void .ctor() cil managed
	{
		.maxstack 8
		ldarg.0 
		call instance void object::.ctor()
		ret
	}
	
	.method public static void Run() cil managed
	{
		.maxstack 3
		
		call int32 AOTTests.SHL::CMP0()
		ldc.i4 1
		ceq
		brtrue _0
		
		ldstr      "AOTTests.SHL::CMP0 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_0:
		OK:
		ldstr      "Ok"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		
		Exit:
		ret
	}
}
