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

			Screen.WriteLine (KRNL.String ("All test cases have completed successfully!"));
		}
	}
}
