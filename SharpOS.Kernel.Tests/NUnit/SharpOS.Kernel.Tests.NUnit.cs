//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using NUnit.Framework;

[TestFixture]
public class KernelTests {
	[Test]
	public void SharpOS_Kernel_Tests_IL_Addition_CMP0 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.Addition.CMP0 () == 1, "'SharpOS.Kernel.Tests.IL.Addition.CMP0' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_Addition_CMP1 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.Addition.CMP1 () == 1, "'SharpOS.Kernel.Tests.IL.Addition.CMP1' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_ConditionChecking_CMP0 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.ConditionChecking.CMP0 () == 1, "'SharpOS.Kernel.Tests.IL.ConditionChecking.CMP0' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_ConstantLoading_CMP0 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.ConstantLoading.CMP0 () == 1, "'SharpOS.Kernel.Tests.IL.ConstantLoading.CMP0' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_ConstantLoading_CMP1 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.ConstantLoading.CMP1 () == 1, "'SharpOS.Kernel.Tests.IL.ConstantLoading.CMP1' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_ConversionOperations_CMP0 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.ConversionOperations.CMP0 () == 1, "'SharpOS.Kernel.Tests.IL.ConversionOperations.CMP0' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_MethodArguments_CMP0 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.MethodArguments.CMP0 () == 1, "'SharpOS.Kernel.Tests.IL.MethodArguments.CMP0' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_MethodArguments_CMP1 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.MethodArguments.CMP1 () == 1, "'SharpOS.Kernel.Tests.IL.MethodArguments.CMP1' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_MethodArguments_CMP2 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.MethodArguments.CMP2 () == 1, "'SharpOS.Kernel.Tests.IL.MethodArguments.CMP2' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHR_CMP0 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHR.CMP0 () == 1, "'SharpOS.Kernel.Tests.IL.SHR.CMP0' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHR_CMP1 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHR.CMP1 () == 1, "'SharpOS.Kernel.Tests.IL.SHR.CMP1' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHR_CMP2 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHR.CMP2 () == 1, "'SharpOS.Kernel.Tests.IL.SHR.CMP2' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHR_CMP3 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHR.CMP3 () == 1, "'SharpOS.Kernel.Tests.IL.SHR.CMP3' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHR_CMP4 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHR.CMP4 () == 1, "'SharpOS.Kernel.Tests.IL.SHR.CMP4' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHR_CMP5 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHR.CMP5 () == 1, "'SharpOS.Kernel.Tests.IL.SHR.CMP5' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHR_CMP6 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHR.CMP6 () == 1, "'SharpOS.Kernel.Tests.IL.SHR.CMP6' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHR_CMP7 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHR.CMP7 () == 1, "'SharpOS.Kernel.Tests.IL.SHR.CMP7' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHR_CMP8 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHR.CMP8 () == 1, "'SharpOS.Kernel.Tests.IL.SHR.CMP8' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHR_CMP9 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHR.CMP9 () == 1, "'SharpOS.Kernel.Tests.IL.SHR.CMP9' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHR_CMP10 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHR.CMP10 () == 1, "'SharpOS.Kernel.Tests.IL.SHR.CMP10' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHL_CMP0 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHL.CMP0 () == 1, "'SharpOS.Kernel.Tests.IL.SHL.CMP0' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHL_CMP1 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHL.CMP1 () == 1, "'SharpOS.Kernel.Tests.IL.SHL.CMP1' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHL_CMP2 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHL.CMP2 () == 1, "'SharpOS.Kernel.Tests.IL.SHL.CMP2' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHL_CMP3 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHL.CMP3 () == 1, "'SharpOS.Kernel.Tests.IL.SHL.CMP3' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHL_CMP4 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHL.CMP4 () == 1, "'SharpOS.Kernel.Tests.IL.SHL.CMP4' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHL_CMP5 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHL.CMP5 () == 1, "'SharpOS.Kernel.Tests.IL.SHL.CMP5' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHL_CMP6 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHL.CMP6 () == 1, "'SharpOS.Kernel.Tests.IL.SHL.CMP6' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHL_CMP7 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHL.CMP7 () == 1, "'SharpOS.Kernel.Tests.IL.SHL.CMP7' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHL_CMP8 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHL.CMP8 () == 1, "'SharpOS.Kernel.Tests.IL.SHL.CMP8' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHL_CMP9 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHL.CMP9 () == 1, "'SharpOS.Kernel.Tests.IL.SHL.CMP9' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHL_CMP10 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHL.CMP10 () == 1, "'SharpOS.Kernel.Tests.IL.SHL.CMP10' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHL_CMP11 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHL.CMP11 () == 1, "'SharpOS.Kernel.Tests.IL.SHL.CMP11' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHL_CMP12 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHL.CMP12 () == 1, "'SharpOS.Kernel.Tests.IL.SHL.CMP12' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHL_CMP13 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHL.CMP13 () == 1, "'SharpOS.Kernel.Tests.IL.SHL.CMP13' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHL_CMP14 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHL.CMP14 () == 1, "'SharpOS.Kernel.Tests.IL.SHL.CMP14' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHL_CMP15 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHL.CMP15 () == 1, "'SharpOS.Kernel.Tests.IL.SHL.CMP15' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHL_CMP16 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHL.CMP16 () == 1, "'SharpOS.Kernel.Tests.IL.SHL.CMP16' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHL_CMP17 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHL.CMP17 () == 1, "'SharpOS.Kernel.Tests.IL.SHL.CMP17' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_ADD_CMP0 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.ADD.CMP0 () == 1, "'SharpOS.Kernel.Tests.IL.ADD.CMP0' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_ADD_CMP1 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.ADD.CMP1 () == 1, "'SharpOS.Kernel.Tests.IL.ADD.CMP1' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_ADD_CMP2 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.ADD.CMP2 () == 1, "'SharpOS.Kernel.Tests.IL.ADD.CMP2' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_ADD_CMP3 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.ADD.CMP3 () == 1, "'SharpOS.Kernel.Tests.IL.ADD.CMP3' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_ADD_CMP4 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.ADD.CMP4 () == 1, "'SharpOS.Kernel.Tests.IL.ADD.CMP4' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_ADD_CMP5 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.ADD.CMP5 () == 1, "'SharpOS.Kernel.Tests.IL.ADD.CMP5' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHRUN_CMP0 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHRUN.CMP0 () == 1, "'SharpOS.Kernel.Tests.IL.SHRUN.CMP0' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHRUN_CMP1 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHRUN.CMP1 () == 1, "'SharpOS.Kernel.Tests.IL.SHRUN.CMP1' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHRUN_CMP2 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHRUN.CMP2 () == 1, "'SharpOS.Kernel.Tests.IL.SHRUN.CMP2' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHRUN_CMP3 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHRUN.CMP3 () == 1, "'SharpOS.Kernel.Tests.IL.SHRUN.CMP3' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHRUN_CMP4 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHRUN.CMP4 () == 1, "'SharpOS.Kernel.Tests.IL.SHRUN.CMP4' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHRUN_CMP5 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHRUN.CMP5 () == 1, "'SharpOS.Kernel.Tests.IL.SHRUN.CMP5' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHRUN_CMP6 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHRUN.CMP6 () == 1, "'SharpOS.Kernel.Tests.IL.SHRUN.CMP6' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHRUN_CMP7 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHRUN.CMP7 () == 1, "'SharpOS.Kernel.Tests.IL.SHRUN.CMP7' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHRUN_CMP8 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHRUN.CMP8 () == 1, "'SharpOS.Kernel.Tests.IL.SHRUN.CMP8' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHRUN_CMP9 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHRUN.CMP9 () == 1, "'SharpOS.Kernel.Tests.IL.SHRUN.CMP9' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHRUN_CMP10 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHRUN.CMP10 () == 1, "'SharpOS.Kernel.Tests.IL.SHRUN.CMP10' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SHRUN_CMP11 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SHRUN.CMP11 () == 1, "'SharpOS.Kernel.Tests.IL.SHRUN.CMP11' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_IL_SUB_CMP0 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.IL.SUB.CMP0 () == 1, "'SharpOS.Kernel.Tests.IL.SUB.CMP0' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_CS_Addition_CMP0 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.CS.Addition.CMP0 () == 1, "'SharpOS.Kernel.Tests.CS.Addition.CMP0' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_CS_Struct_CMPConstructor ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.CS.Struct.CMPConstructor () == 1, "'SharpOS.Kernel.Tests.CS.Struct.CMPConstructor' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_CS_Misc_CMPMisc1 ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.CS.Misc.CMPMisc1 () == 1, "'SharpOS.Kernel.Tests.CS.Misc.CMPMisc1' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_CS_Misc_CMPMisc2a ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.CS.Misc.CMPMisc2a () == 1, "'SharpOS.Kernel.Tests.CS.Misc.CMPMisc2a' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_CS_Misc_CMPMisc2b ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.CS.Misc.CMPMisc2b () == 1, "'SharpOS.Kernel.Tests.CS.Misc.CMPMisc2b' failed.");
		}
	[Test]
	public void SharpOS_Kernel_Tests_CS_Misc_CMPMisc2c ()
		{
			Assert.IsTrue (SharpOS.Kernel.Tests.CS.Misc.CMPMisc2c () == 1, "'SharpOS.Kernel.Tests.CS.Misc.CMPMisc2c' failed.");
		}
}
