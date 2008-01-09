//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.Tests {
	public unsafe class Wrapper {
		public static void Run ()
		{
#if KERNEL_TESTS
			int failures = 0;
			if (SharpOS.Kernel.Tests.IL.ADD.CMP0 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.ADD.CMP0' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.ADD.CMP3 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.ADD.CMP3' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.ADD.CMP1 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.ADD.CMP1' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.ADD.CMP4 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.ADD.CMP4' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.ADD.CMP2 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.ADD.CMP2' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.ADD.CMP5 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.ADD.CMP5' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.Addition.CMP0 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.Addition.CMP0' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.Addition.CMP1 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.Addition.CMP1' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.ConditionChecking.CMP0 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.ConditionChecking.CMP0' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.ConstantLoading.CMP0 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.ConstantLoading.CMP0' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.ConstantLoading.CMP1 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.ConstantLoading.CMP1' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.ConversionOperations.CMP0 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.ConversionOperations.CMP0' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.MethodArguments.CMP0 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.MethodArguments.CMP0' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.MethodArguments.CMP1 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.MethodArguments.CMP1' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.MethodArguments.CMP2 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.MethodArguments.CMP2' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP4 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHL.CMP4' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP3 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHL.CMP3' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP2 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHL.CMP2' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP1 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHL.CMP1' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP0 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHL.CMP0' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP17 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHL.CMP17' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP12 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHL.CMP12' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP15 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHL.CMP15' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP14 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHL.CMP14' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP13 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHL.CMP13' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP11 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHL.CMP11' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP10 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHL.CMP10' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP16 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHL.CMP16' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP9 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHL.CMP9' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP8 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHL.CMP8' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP7 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHL.CMP7' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP6 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHL.CMP6' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHL.CMP5 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHL.CMP5' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP4 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHR.CMP4' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP3 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHR.CMP3' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP2 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHR.CMP2' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP1 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHR.CMP1' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP0 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHR.CMP0' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP10 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHR.CMP10' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP9 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHR.CMP9' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP8 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHR.CMP8' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP7 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHR.CMP7' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP6 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHR.CMP6' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHR.CMP5 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHR.CMP5' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP4 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP4' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP3 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP3' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP2 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP2' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP1 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP1' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP0 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP0' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP11 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP11' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP10 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP10' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP9 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP9' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP8 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP8' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP7 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP7' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP6 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP6' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SHRUN.CMP5 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SHRUN.CMP5' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.IL.SUB.CMP0 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.IL.SUB.CMP0' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Arguments.CMPArguments1 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Arguments.CMPArguments1' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Arguments.CMPArguments2 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Arguments.CMPArguments2' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.PointerCast.CMPVoidP2ByteP () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.PointerCast.CMPVoidP2ByteP' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.PointerCast.CMPVoidP2ShortP () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.PointerCast.CMPVoidP2ShortP' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.PointerCast.CMPVoidP2IntP () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.PointerCast.CMPVoidP2IntP' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.PointerCast.CMPVoidP2LongP () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.PointerCast.CMPVoidP2LongP' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.PointerCast.CMPByteP2VoidP () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.PointerCast.CMPByteP2VoidP' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.PointerCast.CMPShortP2VoidP () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.PointerCast.CMPShortP2VoidP' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.PointerCast.CMPIntP2VoidP () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.PointerCast.CMPIntP2VoidP' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.PointerCast.CMPLongP2VoidP () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.PointerCast.CMPLongP2VoidP' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Enum.CMPLiteralToInt () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Enum.CMPLiteralToInt' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Enum.CMPIntToLiteral () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Enum.CMPIntToLiteral' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Enum.CMPConstantComparison () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Enum.CMPConstantComparison' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Enum.CMPValueComparison () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Enum.CMPValueComparison' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Enum.CMPSimpleFlags () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Enum.CMPSimpleFlags' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Enum.CMPEnumReturn () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Enum.CMPEnumReturn' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.WhileLoop.CMP0 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.WhileLoop.CMP0' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Misc.CMP1 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Misc.CMP1' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Misc.CMP2 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Misc.CMP2' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Misc.CMP0 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Misc.CMP0' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPUInt2SByte () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPUInt2SByte' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPInt2Byte () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPInt2Byte' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPUInt2Byte () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPUInt2Byte' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPByte2Int () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPByte2Int' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPSByte2UInt () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPSByte2UInt' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPByte2Short () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPByte2Short' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPSByte2Short () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPSByte2Short' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPShort2Byte () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPShort2Byte' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPUShort2Byte () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPUShort2Byte' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPUShort2Int () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPUShort2Int' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPShort2UInt () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPShort2UInt' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPUInt2Short () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPUInt2Short' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPInt2UShort () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerCast.CMPInt2UShort' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.ByteString.CMP0 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.ByteString.CMP0' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.X86.CMPPushArgument () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.X86.CMPPushArgument' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.X86.CMPReadArgument () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.X86.CMPReadArgument' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.X86.CMPLabelHandling () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.X86.CMPLabelHandling' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Struct.CMPConstructor () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Struct.CMPConstructor' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Struct.CMPStructPointer () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Struct.CMPStructPointer' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Struct.CMPStructPointer2 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Struct.CMPStructPointer2' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Struct.CMPEmptyStruct () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Struct.CMPEmptyStruct' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Struct.CMPStructParameter () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Struct.CMPStructParameter' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Struct.CMPNoChanges () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Struct.CMPNoChanges' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Struct.CMPCopy () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Struct.CMPCopy' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Struct.CMPReturn () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Struct.CMPReturn' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Struct.CMPSizeof1 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Struct.CMPSizeof1' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Struct.CMPSizeof2 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Struct.CMPSizeof2' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Objects.CMPCreateObject () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Objects.CMPCreateObject' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Objects.CMPOverrideObject () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Objects.CMPOverrideObject' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Boxing.CMPBoxUnbox () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Boxing.CMPBoxUnbox' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Boxing.CMP2 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Boxing.CMP2' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Boolean.CMPSimpleAnd () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Boolean.CMPSimpleAnd' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Boolean.CMPSimpleOr () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Boolean.CMPSimpleOr' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Boolean.CMPSimpleNot () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Boolean.CMPSimpleNot' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Inheritance.CMPCallInherited () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Inheritance.CMPCallInherited' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Inheritance.CMPCallProxiedInherited () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Inheritance.CMPCallProxiedInherited' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Inheritance.CMPCallOverridden () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Inheritance.CMPCallOverridden' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Inheritance.CMPCallShadowedMember () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Inheritance.CMPCallShadowedMember' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Arithmetic.CMPSimpleAdd () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Arithmetic.CMPSimpleAdd' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Arithmetic.CMPSimpleSubtract () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Arithmetic.CMPSimpleSubtract' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Arithmetic.CMPSimpleMultiply () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Arithmetic.CMPSimpleMultiply' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Arithmetic.CMPSimpleDivide () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Arithmetic.CMPSimpleDivide' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.String.CMPGetLength () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.String.CMPGetLength' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.String.CMPGetChars () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.String.CMPGetChars' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.String.CMPGetChars2 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.String.CMPGetChars2' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.String.CMPBumperIndexing1 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.String.CMPBumperIndexing1' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.String.CMPBumperIndexing2 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.String.CMPBumperIndexing2' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.String.CMPBumperLength () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.String.CMPBumperLength' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.String.CMPCStringStub1 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.String.CMPCStringStub1' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.String.CMPConstIndexing3 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.String.CMPConstIndexing3' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.BooleanOrderOfOperations.CMPConstants () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.BooleanOrderOfOperations.CMPConstants' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.BooleanOrderOfOperations.CMPValues () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.BooleanOrderOfOperations.CMPValues' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.BooleanOrderOfOperations.CMPValuesAndConstants () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.BooleanOrderOfOperations.CMPValuesAndConstants' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerPointerCast.CMPVoidP2Byte () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerPointerCast.CMPVoidP2Byte' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerPointerCast.CMPVoidP2Short () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerPointerCast.CMPVoidP2Short' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerPointerCast.CMPVoidP2Int () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerPointerCast.CMPVoidP2Int' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerPointerCast.CMPVoidP2Long () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerPointerCast.CMPVoidP2Long' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerPointerCast.CMPVoidP2SByte () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerPointerCast.CMPVoidP2SByte' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerPointerCast.CMPVoidP2UShort () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerPointerCast.CMPVoidP2UShort' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerPointerCast.CMPVoidP2UInt () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerPointerCast.CMPVoidP2UInt' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.MixedIntegerPointerCast.CMPVoidP2ULong () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.MixedIntegerPointerCast.CMPVoidP2ULong' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.StaticConstructor.CMPStaticConstructor () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.StaticConstructor.CMPStaticConstructor' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.OrderOfOperations.CMPConstants () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.OrderOfOperations.CMPConstants' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.OrderOfOperations.CMPValues () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.OrderOfOperations.CMPValues' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.OrderOfOperations.CMPValuesAndConstants () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.OrderOfOperations.CMPValuesAndConstants' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.BitwiseOperators.CMPSimpleAND () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.BitwiseOperators.CMPSimpleAND' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.BitwiseOperators.CMPSimpleOR () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.BitwiseOperators.CMPSimpleOR' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.BitwiseOperators.CMPSimpleXOR () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.BitwiseOperators.CMPSimpleXOR' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.BitwiseOperators.CMPSimpleShiftLeft () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.BitwiseOperators.CMPSimpleShiftLeft' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.BitwiseOperators.CMPUnsignedShiftLeft () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.BitwiseOperators.CMPUnsignedShiftLeft' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.BitwiseOperators.CMPSimpleShiftRight () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.BitwiseOperators.CMPSimpleShiftRight' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.UnsignedIntegerCast.CMPUInt2Byte () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.UnsignedIntegerCast.CMPUInt2Byte' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.UnsignedIntegerCast.CMPByte2UInt () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.UnsignedIntegerCast.CMPByte2UInt' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.UnsignedIntegerCast.CMPByte2UShort () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.UnsignedIntegerCast.CMPByte2UShort' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.UnsignedIntegerCast.CMPUShort2Byte () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.UnsignedIntegerCast.CMPUShort2Byte' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.UnsignedIntegerCast.CMPUShort2UInt () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.UnsignedIntegerCast.CMPUShort2UInt' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.UnsignedIntegerCast.CMPUInt2UShort () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.UnsignedIntegerCast.CMPUInt2UShort' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Switch.CMP0 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Switch.CMP0' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Switch.CMP1 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Switch.CMP1' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Switch.CMP2 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Switch.CMP2' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Switch.CMP3 () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Switch.CMP3' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Switch.CMPMisc2a () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Switch.CMPMisc2a' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Switch.CMPMisc2b () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Switch.CMPMisc2b' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.Switch.CMPMisc2c () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.Switch.CMPMisc2c' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.SignedIntegerCast.CMPInt2SByte () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.SignedIntegerCast.CMPInt2SByte' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.SignedIntegerCast.CMPSByte2Int () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.SignedIntegerCast.CMPSByte2Int' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.SignedIntegerCast.CMPSByte2Short () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.SignedIntegerCast.CMPSByte2Short' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.SignedIntegerCast.CMPShort2SByte () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.SignedIntegerCast.CMPShort2SByte' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.SignedIntegerCast.CMPShort2Int () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.SignedIntegerCast.CMPShort2Int' failed.");
				failures++;
			}

			if (SharpOS.Kernel.Tests.CS.SignedIntegerCast.CMPInt2Short () != 1) {
				TextMode.WriteLine ("'SharpOS.Kernel.Tests.CS.SignedIntegerCast.CMPInt2Short' failed.");
				failures++;
			}

			if (failures > 0)
				TextMode.WriteLine ("Not all tests passed!");
			else
				TextMode.WriteLine ("All test cases have completed successfully!");
#endif
		}
	}
}
