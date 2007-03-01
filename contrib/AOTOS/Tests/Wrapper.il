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
		
		call int32 AOTTests.ADD::CMP0()
		ldc.i4 1
		ceq
		brtrue _0
		
		ldstr      "AOTTests.ADD::CMP0 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_0:
		call int32 AOTTests.ADD::CMP1()
		ldc.i4 1
		ceq
		brtrue _1
		
		ldstr      "AOTTests.ADD::CMP1 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_1:
		call int32 AOTTests.ADD::CMP2()
		ldc.i4 1
		ceq
		brtrue _2
		
		ldstr      "AOTTests.ADD::CMP2 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_2:
		call int32 AOTTests.ADD::CMP3()
		ldc.i4 1
		ceq
		brtrue _3
		
		ldstr      "AOTTests.ADD::CMP3 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_3:
		call int32 AOTTests.SHL::CMP0()
		ldc.i4 1
		ceq
		brtrue _4
		
		ldstr      "AOTTests.SHL::CMP0 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_4:
		call int32 AOTTests.SHL::CMP1()
		ldc.i4 1
		ceq
		brtrue _5
		
		ldstr      "AOTTests.SHL::CMP1 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_5:
		call int32 AOTTests.SHL::CMP2()
		ldc.i4 1
		ceq
		brtrue _6
		
		ldstr      "AOTTests.SHL::CMP2 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_6:
		call int32 AOTTests.SHL::CMP3()
		ldc.i4 1
		ceq
		brtrue _7
		
		ldstr      "AOTTests.SHL::CMP3 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_7:
		call int32 AOTTests.SHL::CMP4()
		ldc.i4 1
		ceq
		brtrue _8
		
		ldstr      "AOTTests.SHL::CMP4 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_8:
		call int32 AOTTests.SHL::CMP5()
		ldc.i4 1
		ceq
		brtrue _9
		
		ldstr      "AOTTests.SHL::CMP5 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_9:
		call int32 AOTTests.SHL::CMP6()
		ldc.i4 1
		ceq
		brtrue _10
		
		ldstr      "AOTTests.SHL::CMP6 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_10:
		call int32 AOTTests.SHL::CMP7()
		ldc.i4 1
		ceq
		brtrue _11
		
		ldstr      "AOTTests.SHL::CMP7 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_11:
		call int32 AOTTests.SUB::CMP0()
		ldc.i4 1
		ceq
		brtrue _12
		
		ldstr      "AOTTests.SUB::CMP0 Failed"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		br Exit
		
		_12:
		OK:
		ldstr      "Ok"
		call       uint8* SharpOS.Kernel::String(string)
		call       void SharpOS.Kernel::WriteMessage(uint8*)
		
		Exit:
		ret
	}
}
