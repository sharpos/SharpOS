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
		
		call int32 AOTTests.SHR::CMP0()
		ldc.i4 1
		ceq
		brtrue _0
		
		ldstr      "AOTTests.SHR::CMP0 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_0:
		call int32 AOTTests.SHR::CMP1()
		ldc.i4 1
		ceq
		brtrue _1
		
		ldstr      "AOTTests.SHR::CMP1 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_1:
		call int32 AOTTests.SHR::CMP2()
		ldc.i4 1
		ceq
		brtrue _2
		
		ldstr      "AOTTests.SHR::CMP2 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_2:
		call int32 AOTTests.SHR::CMP3()
		ldc.i4 1
		ceq
		brtrue _3
		
		ldstr      "AOTTests.SHR::CMP3 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_3:
		call int32 AOTTests.SHR::CMP4()
		ldc.i4 1
		ceq
		brtrue _4
		
		ldstr      "AOTTests.SHR::CMP4 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_4:
		call int32 AOTTests.SHR.UN::CMP0()
		ldc.i4 1
		ceq
		brtrue _5
		
		ldstr      "AOTTests.SHR.UN::CMP0 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_5:
		call int32 AOTTests.SHR.UN::CMP1()
		ldc.i4 1
		ceq
		brtrue _6
		
		ldstr      "AOTTests.SHR.UN::CMP1 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_6:
		call int32 AOTTests.SHR.UN::CMP2()
		ldc.i4 1
		ceq
		brtrue _7
		
		ldstr      "AOTTests.SHR.UN::CMP2 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_7:
		call int32 AOTTests.SHR.UN::CMP3()
		ldc.i4 1
		ceq
		brtrue _8
		
		ldstr      "AOTTests.SHR.UN::CMP3 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_8:
		call int32 AOTTests.SHR.UN::CMP4()
		ldc.i4 1
		ceq
		brtrue _9
		
		ldstr      "AOTTests.SHR.UN::CMP4 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_9:
		OK:
		ldstr      "Ok"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		
		Exit:
		ret
	}
}
