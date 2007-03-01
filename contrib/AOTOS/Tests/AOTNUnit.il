/**
 *  (C) 2006-2007 Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
 * 
 */

.assembly extern mscorlib
{
}

.assembly extern nunit.framework
{
	.publickeytoken = (96 D0 9A 1E B7 F4 4A 77 )
	.ver 2:2:9:0
}

.module NUnit.Kernel.dll

.assembly NUnit.Kernel
{
	.ver 1:0:0:0
}

.class public ansi auto Tests extends [mscorlib]System.Object
{
	.custom instance void [nunit.framework]NUnit.Framework.TestFixtureAttribute::.ctor() = (1 0 0 0)
	
	.method public hidebysig specialname rtspecialname instance void .ctor() cil managed
	{
		.maxstack 8
		ldarg.0 
		call instance void object::.ctor()
		ret 
	}
	
	.method public void Run() cil managed
	{
		.custom instance void [nunit.framework]NUnit.Framework.TestAttribute::.ctor() = (1 0 0 0)
		.maxstack 3
		
		call int32 AOTTests.ADD::CMP0()
		ldstr "AOTTests.ADD::CMP0 failed."
		call void [nunit.framework]NUnit.Framework.Assert::IsTrue(bool, string)
		
		call int32 AOTTests.ADD::CMP1()
		ldstr "AOTTests.ADD::CMP1 failed."
		call void [nunit.framework]NUnit.Framework.Assert::IsTrue(bool, string)
		
		call int32 AOTTests.ADD::CMP2()
		ldstr "AOTTests.ADD::CMP2 failed."
		call void [nunit.framework]NUnit.Framework.Assert::IsTrue(bool, string)
		
		call int32 AOTTests.ADD::CMP3()
		ldstr "AOTTests.ADD::CMP3 failed."
		call void [nunit.framework]NUnit.Framework.Assert::IsTrue(bool, string)
		
		call int32 AOTTests.SHL::CMP0()
		ldstr "AOTTests.SHL::CMP0 failed."
		call void [nunit.framework]NUnit.Framework.Assert::IsTrue(bool, string)
		
		call int32 AOTTests.SHL::CMP1()
		ldstr "AOTTests.SHL::CMP1 failed."
		call void [nunit.framework]NUnit.Framework.Assert::IsTrue(bool, string)
		
		call int32 AOTTests.SHL::CMP2()
		ldstr "AOTTests.SHL::CMP2 failed."
		call void [nunit.framework]NUnit.Framework.Assert::IsTrue(bool, string)
		
		call int32 AOTTests.SHL::CMP3()
		ldstr "AOTTests.SHL::CMP3 failed."
		call void [nunit.framework]NUnit.Framework.Assert::IsTrue(bool, string)
		
		call int32 AOTTests.SHL::CMP4()
		ldstr "AOTTests.SHL::CMP4 failed."
		call void [nunit.framework]NUnit.Framework.Assert::IsTrue(bool, string)
		
		call int32 AOTTests.SHL::CMP5()
		ldstr "AOTTests.SHL::CMP5 failed."
		call void [nunit.framework]NUnit.Framework.Assert::IsTrue(bool, string)
		
		call int32 AOTTests.SHL::CMP6()
		ldstr "AOTTests.SHL::CMP6 failed."
		call void [nunit.framework]NUnit.Framework.Assert::IsTrue(bool, string)
		
		call int32 AOTTests.SHL::CMP7()
		ldstr "AOTTests.SHL::CMP7 failed."
		call void [nunit.framework]NUnit.Framework.Assert::IsTrue(bool, string)
		
		call int32 AOTTests.SUB::CMP0()
		ldstr "AOTTests.SUB::CMP0 failed."
		call void [nunit.framework]NUnit.Framework.Assert::IsTrue(bool, string)
		
		ret
	}
}
