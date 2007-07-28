//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

namespace SharpOS {
	public unsafe partial class KRNL {
		protected static void RunTests ()
		{
			if (SharpOS.Kernel.Tests.IL.Addition.CMP0 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.Addition.CMP0' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.Addition.CMP1 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.Addition.CMP1' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.ConditionChecking.CMP0 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.ConditionChecking.CMP0' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.ConstantLoading.CMP0 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.ConstantLoading.CMP0' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.ConstantLoading.CMP1 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.ConstantLoading.CMP1' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.ConversionOperations.CMP0 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.ConversionOperations.CMP0' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.MethodArguments.CMP0 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.MethodArguments.CMP0' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.MethodArguments.CMP1 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.MethodArguments.CMP1' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.MethodArguments.CMP2 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.MethodArguments.CMP2' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP0 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHR.CMP0' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP1 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHR.CMP1' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP2 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHR.CMP2' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP3 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHR.CMP3' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP4 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHR.CMP4' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP5 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHR.CMP5' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP6 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHR.CMP6' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP7 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHR.CMP7' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP8 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHR.CMP8' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP9 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHR.CMP9' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP10 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHR.CMP10' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP0 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHL.CMP0' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP1 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHL.CMP1' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP2 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHL.CMP2' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP3 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHL.CMP3' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP4 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHL.CMP4' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP5 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHL.CMP5' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP6 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHL.CMP6' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP7 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHL.CMP7' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP8 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHL.CMP8' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP9 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHL.CMP9' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP10 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHL.CMP10' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP11 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHL.CMP11' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP12 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHL.CMP12' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP13 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHL.CMP13' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP14 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHL.CMP14' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP15 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHL.CMP15' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP16 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHL.CMP16' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP17 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHL.CMP17' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.ADD.CMP0 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.ADD.CMP0' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.ADD.CMP1 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.ADD.CMP1' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.ADD.CMP2 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.ADD.CMP2' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.ADD.CMP3 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.ADD.CMP3' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.ADD.CMP4 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.ADD.CMP4' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.ADD.CMP5 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.ADD.CMP5' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP0 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP0' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP1 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP1' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP2 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP2' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP3 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP3' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP4 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP4' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP5 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP5' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP6 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP6' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP7 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP7' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP8 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP8' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP9 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP9' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP10 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP10' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP11 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP11' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.IL.SUB.CMP0 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.IL.SUB.CMP0' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.CS.Addition.CMP0 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.CS.Addition.CMP0' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.CS.Struct.CMPConstructor () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.CS.Struct.CMPConstructor' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.CS.Misc.CMPMisc1 () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.CS.Misc.CMPMisc1' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.CS.Misc.CMPMisc2a () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.CS.Misc.CMPMisc2a' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.CS.Misc.CMPMisc2b () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.CS.Misc.CMPMisc2b' failed."));
				return;
			}

			if (SharpOS.Kernel.Tests.CS.Misc.CMPMisc2c () != 1) {
				Screen.WriteLine (KRNL.String ("'SharpOS.Kernel.Tests.CS.Misc.CMPMisc2c' failed."));
				return;
			}

			Screen.WriteLine (KRNL.String ("All test cases have completed successfully!"));
		}
	}
}
