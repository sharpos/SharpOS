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
	
	.method public void Run0() cil managed
	{
		.custom instance void [nunit.framework]NUnit.Framework.TestAttribute::.ctor() = (1 0 0 0)
		.maxstack 3
		
		call int32 AOTTests.SHL::CMP0()
		ldstr "AOTTests.SHL::CMP0 failed."
		call void [nunit.framework]NUnit.Framework.Assert::IsTrue(bool, string)
		
		ret
	}
}
