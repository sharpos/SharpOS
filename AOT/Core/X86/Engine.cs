//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.AOT.IR;

namespace SharpOS.AOT.X86 {
	public partial class Assembly {
		/// <summary>
		/// Emits an x86 instruction (represented by an SharpOS.X86.Instruction object) for the given
		/// 'Asm' stub call instruction.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <param name="operands">The operands.</param>
		/// <param name="parameterTypes">The parameter types.</param>
		internal void GetAssemblyInstruction (SharpOS.AOT.IR.Instructions.Call method, object [] operands, string parameterTypes)
		{
			switch (method.Method.Name) {
			case "AAA":
				switch (parameterTypes) {
				case "AAA":
					this.AAA ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "AAD":
				switch (parameterTypes) {
				case "AAD":
					this.AAD ();
					break;

				case "AAD Byte":
					this.AAD ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "AAM":
				switch (parameterTypes) {
				case "AAM":
					this.AAM ();
					break;

				case "AAM Byte":
					this.AAM ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "AAS":
				switch (parameterTypes) {
				case "AAS":
					this.AAS ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "ADC":
				switch (parameterTypes) {
				case "ADC ByteMemory Byte":
					this.ADC (GetByteMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ADC ByteMemory R8Type":
					this.ADC (GetByteMemory (operands [0]), R8.GetByID (operands [1]));
					break;

				case "ADC DWordMemory Byte":
					this.ADC (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ADC DWordMemory R32Type":
					this.ADC (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "ADC DWordMemory UInt32":
					this.ADC (GetDWordMemory (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ADC R16Type Byte":
					this.ADC (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ADC R16Type R16Type":
					this.ADC (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "ADC R16Type UInt16":
					this.ADC (R16.GetByID (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ADC R16Type WordMemory":
					this.ADC (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "ADC R32Type Byte":
					this.ADC (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ADC R32Type DWordMemory":
					this.ADC (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "ADC R32Type R32Type":
					this.ADC (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "ADC R32Type UInt32":
					this.ADC (R32.GetByID (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ADC R8Type Byte":
					this.ADC (R8.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ADC R8Type ByteMemory":
					this.ADC (R8.GetByID (operands [0]), GetByteMemory (operands [1]));
					break;

				case "ADC R8Type R8Type":
					this.ADC (R8.GetByID (operands [0]), R8.GetByID (operands [1]));
					break;

				case "ADC WordMemory Byte":
					this.ADC (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ADC WordMemory R16Type":
					this.ADC (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				case "ADC WordMemory UInt16":
					this.ADC (GetWordMemory (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "ADD":
				switch (parameterTypes) {
				case "ADD ByteMemory Byte":
					this.ADD (GetByteMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ADD ByteMemory R8Type":
					this.ADD (GetByteMemory (operands [0]), R8.GetByID (operands [1]));
					break;

				case "ADD DWordMemory Byte":
					this.ADD (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ADD DWordMemory R32Type":
					this.ADD (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "ADD DWordMemory UInt32":
					this.ADD (GetDWordMemory (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ADD R16Type Byte":
					this.ADD (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ADD R16Type R16Type":
					this.ADD (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "ADD R16Type UInt16":
					this.ADD (R16.GetByID (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ADD R16Type WordMemory":
					this.ADD (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "ADD R32Type Byte":
					this.ADD (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ADD R32Type DWordMemory":
					this.ADD (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "ADD R32Type R32Type":
					this.ADD (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "ADD R32Type UInt32":
					this.ADD (R32.GetByID (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ADD R8Type Byte":
					this.ADD (R8.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ADD R8Type ByteMemory":
					this.ADD (R8.GetByID (operands [0]), GetByteMemory (operands [1]));
					break;

				case "ADD R8Type R8Type":
					this.ADD (R8.GetByID (operands [0]), R8.GetByID (operands [1]));
					break;

				case "ADD WordMemory Byte":
					this.ADD (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ADD WordMemory R16Type":
					this.ADD (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				case "ADD WordMemory UInt16":
					this.ADD (GetWordMemory (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "ALIGN":
				switch (parameterTypes) {
				case "ALIGN UInt32":
					this.ALIGN ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "AND":
				switch (parameterTypes) {
				case "AND ByteMemory Byte":
					this.AND (GetByteMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "AND ByteMemory R8Type":
					this.AND (GetByteMemory (operands [0]), R8.GetByID (operands [1]));
					break;

				case "AND DWordMemory Byte":
					this.AND (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "AND DWordMemory R32Type":
					this.AND (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "AND DWordMemory UInt32":
					this.AND (GetDWordMemory (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "AND R16Type Byte":
					this.AND (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "AND R16Type R16Type":
					this.AND (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "AND R16Type UInt16":
					this.AND (R16.GetByID (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "AND R16Type WordMemory":
					this.AND (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "AND R32Type Byte":
					this.AND (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "AND R32Type DWordMemory":
					this.AND (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "AND R32Type R32Type":
					this.AND (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "AND R32Type UInt32":
					this.AND (R32.GetByID (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "AND R8Type Byte":
					this.AND (R8.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "AND R8Type ByteMemory":
					this.AND (R8.GetByID (operands [0]), GetByteMemory (operands [1]));
					break;

				case "AND R8Type R8Type":
					this.AND (R8.GetByID (operands [0]), R8.GetByID (operands [1]));
					break;

				case "AND WordMemory Byte":
					this.AND (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "AND WordMemory R16Type":
					this.AND (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				case "AND WordMemory UInt16":
					this.AND (GetWordMemory (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "ARPL":
				switch (parameterTypes) {
				case "ARPL R16Type R16Type":
					this.ARPL (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "ARPL WordMemory R16Type":
					this.ARPL (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "BITS32":
				switch (parameterTypes) {
				case "BITS32 Boolean":
					this.BITS32 (Convert.ToBoolean ((operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "BOUND":
				switch (parameterTypes) {
				case "BOUND R16Type Memory":
					this.BOUND (R16.GetByID (operands [0]), GetMemory (operands [1]));
					break;

				case "BOUND R32Type Memory":
					this.BOUND (R32.GetByID (operands [0]), GetMemory (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "BSF":
				switch (parameterTypes) {
				case "BSF R16Type R16Type":
					this.BSF (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "BSF R16Type WordMemory":
					this.BSF (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "BSF R32Type DWordMemory":
					this.BSF (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "BSF R32Type R32Type":
					this.BSF (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "BSR":
				switch (parameterTypes) {
				case "BSR R16Type R16Type":
					this.BSR (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "BSR R16Type WordMemory":
					this.BSR (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "BSR R32Type DWordMemory":
					this.BSR (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "BSR R32Type R32Type":
					this.BSR (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "BSWAP":
				switch (parameterTypes) {
				case "BSWAP R32Type":
					this.BSWAP (R32.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "BT":
				switch (parameterTypes) {
				case "BT DWordMemory Byte":
					this.BT (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "BT DWordMemory R32Type":
					this.BT (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "BT R16Type Byte":
					this.BT (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "BT R16Type R16Type":
					this.BT (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "BT R32Type Byte":
					this.BT (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "BT R32Type R32Type":
					this.BT (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "BT WordMemory Byte":
					this.BT (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "BT WordMemory R16Type":
					this.BT (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "BTC":
				switch (parameterTypes) {
				case "BTC DWordMemory Byte":
					this.BTC (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "BTC DWordMemory R32Type":
					this.BTC (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "BTC R16Type Byte":
					this.BTC (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "BTC R16Type R16Type":
					this.BTC (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "BTC R32Type Byte":
					this.BTC (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "BTC R32Type R32Type":
					this.BTC (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "BTC WordMemory Byte":
					this.BTC (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "BTC WordMemory R16Type":
					this.BTC (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "BTR":
				switch (parameterTypes) {
				case "BTR DWordMemory Byte":
					this.BTR (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "BTR DWordMemory R32Type":
					this.BTR (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "BTR R16Type Byte":
					this.BTR (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "BTR R16Type R16Type":
					this.BTR (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "BTR R32Type Byte":
					this.BTR (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "BTR R32Type R32Type":
					this.BTR (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "BTR WordMemory Byte":
					this.BTR (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "BTR WordMemory R16Type":
					this.BTR (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "BTS":
				switch (parameterTypes) {
				case "BTS DWordMemory Byte":
					this.BTS (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "BTS DWordMemory R32Type":
					this.BTS (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "BTS R16Type Byte":
					this.BTS (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "BTS R16Type R16Type":
					this.BTS (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "BTS R32Type Byte":
					this.BTS (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "BTS R32Type R32Type":
					this.BTS (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "BTS WordMemory Byte":
					this.BTS (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "BTS WordMemory R16Type":
					this.BTS (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CALL":
				switch (parameterTypes) {
				case "CALL DWordMemory":
					this.CALL (GetDWordMemory (operands [0]));
					break;

				case "CALL R16Type":
					this.CALL (R16.GetByID (operands [0]));
					break;

				case "CALL R32Type":
					this.CALL (R32.GetByID (operands [0]));
					break;

				case "CALL String":
					this.CALL ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "CALL UInt16 UInt16":
					this.CALL ((UInt16) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value, (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "CALL UInt16 UInt32":
					this.CALL ((UInt16) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value, (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "CALL UInt32":
					this.CALL ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "CALL WordMemory":
					this.CALL (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CALL_FAR":
				switch (parameterTypes) {
				case "CALL_FAR DWordMemory":
					this.CALL_FAR (GetDWordMemory (operands [0]));
					break;

				case "CALL_FAR WordMemory":
					this.CALL_FAR (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CBW":
				switch (parameterTypes) {
				case "CBW":
					this.CBW ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CDQ":
				switch (parameterTypes) {
				case "CDQ":
					this.CDQ ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CLC":
				switch (parameterTypes) {
				case "CLC":
					this.CLC ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CLD":
				switch (parameterTypes) {
				case "CLD":
					this.CLD ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CLFLUSH":
				switch (parameterTypes) {
				case "CLFLUSH Memory":
					this.CLFLUSH (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CLI":
				switch (parameterTypes) {
				case "CLI":
					this.CLI ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CLTS":
				switch (parameterTypes) {
				case "CLTS":
					this.CLTS ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMC":
				switch (parameterTypes) {
				case "CMC":
					this.CMC ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVA":
				switch (parameterTypes) {
				case "CMOVA R16Type R16Type":
					this.CMOVA (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVA R16Type WordMemory":
					this.CMOVA (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVA R32Type DWordMemory":
					this.CMOVA (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVA R32Type R32Type":
					this.CMOVA (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVAE":
				switch (parameterTypes) {
				case "CMOVAE R16Type R16Type":
					this.CMOVAE (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVAE R16Type WordMemory":
					this.CMOVAE (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVAE R32Type DWordMemory":
					this.CMOVAE (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVAE R32Type R32Type":
					this.CMOVAE (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVB":
				switch (parameterTypes) {
				case "CMOVB R16Type R16Type":
					this.CMOVB (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVB R16Type WordMemory":
					this.CMOVB (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVB R32Type DWordMemory":
					this.CMOVB (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVB R32Type R32Type":
					this.CMOVB (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVBE":
				switch (parameterTypes) {
				case "CMOVBE R16Type R16Type":
					this.CMOVBE (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVBE R16Type WordMemory":
					this.CMOVBE (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVBE R32Type DWordMemory":
					this.CMOVBE (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVBE R32Type R32Type":
					this.CMOVBE (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVC":
				switch (parameterTypes) {
				case "CMOVC R16Type R16Type":
					this.CMOVC (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVC R16Type WordMemory":
					this.CMOVC (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVC R32Type DWordMemory":
					this.CMOVC (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVC R32Type R32Type":
					this.CMOVC (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVE":
				switch (parameterTypes) {
				case "CMOVE R16Type R16Type":
					this.CMOVE (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVE R16Type WordMemory":
					this.CMOVE (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVE R32Type DWordMemory":
					this.CMOVE (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVE R32Type R32Type":
					this.CMOVE (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVG":
				switch (parameterTypes) {
				case "CMOVG R16Type R16Type":
					this.CMOVG (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVG R16Type WordMemory":
					this.CMOVG (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVG R32Type DWordMemory":
					this.CMOVG (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVG R32Type R32Type":
					this.CMOVG (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVGE":
				switch (parameterTypes) {
				case "CMOVGE R16Type R16Type":
					this.CMOVGE (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVGE R16Type WordMemory":
					this.CMOVGE (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVGE R32Type DWordMemory":
					this.CMOVGE (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVGE R32Type R32Type":
					this.CMOVGE (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVL":
				switch (parameterTypes) {
				case "CMOVL R16Type R16Type":
					this.CMOVL (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVL R16Type WordMemory":
					this.CMOVL (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVL R32Type DWordMemory":
					this.CMOVL (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVL R32Type R32Type":
					this.CMOVL (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVLE":
				switch (parameterTypes) {
				case "CMOVLE R16Type R16Type":
					this.CMOVLE (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVLE R16Type WordMemory":
					this.CMOVLE (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVLE R32Type DWordMemory":
					this.CMOVLE (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVLE R32Type R32Type":
					this.CMOVLE (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVNA":
				switch (parameterTypes) {
				case "CMOVNA R16Type R16Type":
					this.CMOVNA (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVNA R16Type WordMemory":
					this.CMOVNA (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVNA R32Type DWordMemory":
					this.CMOVNA (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVNA R32Type R32Type":
					this.CMOVNA (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVNAE":
				switch (parameterTypes) {
				case "CMOVNAE R16Type R16Type":
					this.CMOVNAE (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVNAE R16Type WordMemory":
					this.CMOVNAE (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVNAE R32Type DWordMemory":
					this.CMOVNAE (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVNAE R32Type R32Type":
					this.CMOVNAE (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVNB":
				switch (parameterTypes) {
				case "CMOVNB R16Type R16Type":
					this.CMOVNB (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVNB R16Type WordMemory":
					this.CMOVNB (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVNB R32Type DWordMemory":
					this.CMOVNB (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVNB R32Type R32Type":
					this.CMOVNB (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVNBE":
				switch (parameterTypes) {
				case "CMOVNBE R16Type R16Type":
					this.CMOVNBE (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVNBE R16Type WordMemory":
					this.CMOVNBE (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVNBE R32Type DWordMemory":
					this.CMOVNBE (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVNBE R32Type R32Type":
					this.CMOVNBE (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVNC":
				switch (parameterTypes) {
				case "CMOVNC R16Type R16Type":
					this.CMOVNC (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVNC R16Type WordMemory":
					this.CMOVNC (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVNC R32Type DWordMemory":
					this.CMOVNC (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVNC R32Type R32Type":
					this.CMOVNC (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVNE":
				switch (parameterTypes) {
				case "CMOVNE R16Type R16Type":
					this.CMOVNE (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVNE R16Type WordMemory":
					this.CMOVNE (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVNE R32Type DWordMemory":
					this.CMOVNE (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVNE R32Type R32Type":
					this.CMOVNE (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVNG":
				switch (parameterTypes) {
				case "CMOVNG R16Type R16Type":
					this.CMOVNG (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVNG R16Type WordMemory":
					this.CMOVNG (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVNG R32Type DWordMemory":
					this.CMOVNG (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVNG R32Type R32Type":
					this.CMOVNG (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVNGE":
				switch (parameterTypes) {
				case "CMOVNGE R16Type R16Type":
					this.CMOVNGE (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVNGE R16Type WordMemory":
					this.CMOVNGE (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVNGE R32Type DWordMemory":
					this.CMOVNGE (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVNGE R32Type R32Type":
					this.CMOVNGE (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVNL":
				switch (parameterTypes) {
				case "CMOVNL R16Type R16Type":
					this.CMOVNL (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVNL R16Type WordMemory":
					this.CMOVNL (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVNL R32Type DWordMemory":
					this.CMOVNL (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVNL R32Type R32Type":
					this.CMOVNL (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVNLE":
				switch (parameterTypes) {
				case "CMOVNLE R16Type R16Type":
					this.CMOVNLE (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVNLE R16Type WordMemory":
					this.CMOVNLE (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVNLE R32Type DWordMemory":
					this.CMOVNLE (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVNLE R32Type R32Type":
					this.CMOVNLE (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVNO":
				switch (parameterTypes) {
				case "CMOVNO R16Type R16Type":
					this.CMOVNO (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVNO R16Type WordMemory":
					this.CMOVNO (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVNO R32Type DWordMemory":
					this.CMOVNO (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVNO R32Type R32Type":
					this.CMOVNO (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVNP":
				switch (parameterTypes) {
				case "CMOVNP R16Type R16Type":
					this.CMOVNP (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVNP R16Type WordMemory":
					this.CMOVNP (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVNP R32Type DWordMemory":
					this.CMOVNP (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVNP R32Type R32Type":
					this.CMOVNP (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVNS":
				switch (parameterTypes) {
				case "CMOVNS R16Type R16Type":
					this.CMOVNS (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVNS R16Type WordMemory":
					this.CMOVNS (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVNS R32Type DWordMemory":
					this.CMOVNS (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVNS R32Type R32Type":
					this.CMOVNS (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVNZ":
				switch (parameterTypes) {
				case "CMOVNZ R16Type R16Type":
					this.CMOVNZ (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVNZ R16Type WordMemory":
					this.CMOVNZ (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVNZ R32Type DWordMemory":
					this.CMOVNZ (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVNZ R32Type R32Type":
					this.CMOVNZ (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVO":
				switch (parameterTypes) {
				case "CMOVO R16Type R16Type":
					this.CMOVO (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVO R16Type WordMemory":
					this.CMOVO (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVO R32Type DWordMemory":
					this.CMOVO (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVO R32Type R32Type":
					this.CMOVO (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVP":
				switch (parameterTypes) {
				case "CMOVP R16Type R16Type":
					this.CMOVP (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVP R16Type WordMemory":
					this.CMOVP (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVP R32Type DWordMemory":
					this.CMOVP (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVP R32Type R32Type":
					this.CMOVP (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVPE":
				switch (parameterTypes) {
				case "CMOVPE R16Type R16Type":
					this.CMOVPE (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVPE R16Type WordMemory":
					this.CMOVPE (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVPE R32Type DWordMemory":
					this.CMOVPE (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVPE R32Type R32Type":
					this.CMOVPE (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVPO":
				switch (parameterTypes) {
				case "CMOVPO R16Type R16Type":
					this.CMOVPO (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVPO R16Type WordMemory":
					this.CMOVPO (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVPO R32Type DWordMemory":
					this.CMOVPO (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVPO R32Type R32Type":
					this.CMOVPO (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVS":
				switch (parameterTypes) {
				case "CMOVS R16Type R16Type":
					this.CMOVS (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVS R16Type WordMemory":
					this.CMOVS (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVS R32Type DWordMemory":
					this.CMOVS (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVS R32Type R32Type":
					this.CMOVS (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMOVZ":
				switch (parameterTypes) {
				case "CMOVZ R16Type R16Type":
					this.CMOVZ (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMOVZ R16Type WordMemory":
					this.CMOVZ (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMOVZ R32Type DWordMemory":
					this.CMOVZ (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMOVZ R32Type R32Type":
					this.CMOVZ (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMP":
				switch (parameterTypes) {
				case "CMP ByteMemory Byte":
					this.CMP (GetByteMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "CMP ByteMemory R8Type":
					this.CMP (GetByteMemory (operands [0]), R8.GetByID (operands [1]));
					break;

				case "CMP DWordMemory Byte":
					this.CMP (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "CMP DWordMemory R32Type":
					this.CMP (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "CMP DWordMemory UInt32":
					this.CMP (GetDWordMemory (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "CMP R16Type Byte":
					this.CMP (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "CMP R16Type R16Type":
					this.CMP (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMP R16Type UInt16":
					this.CMP (R16.GetByID (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "CMP R16Type WordMemory":
					this.CMP (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "CMP R32Type Byte":
					this.CMP (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "CMP R32Type DWordMemory":
					this.CMP (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "CMP R32Type R32Type":
					this.CMP (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "CMP R32Type UInt32":
					this.CMP (R32.GetByID (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "CMP R8Type Byte":
					this.CMP (R8.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "CMP R8Type ByteMemory":
					this.CMP (R8.GetByID (operands [0]), GetByteMemory (operands [1]));
					break;

				case "CMP R8Type R8Type":
					this.CMP (R8.GetByID (operands [0]), R8.GetByID (operands [1]));
					break;

				case "CMP WordMemory Byte":
					this.CMP (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "CMP WordMemory R16Type":
					this.CMP (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMP WordMemory UInt16":
					this.CMP (GetWordMemory (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMPSB":
				switch (parameterTypes) {
				case "CMPSB":
					this.CMPSB ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMPSD":
				switch (parameterTypes) {
				case "CMPSD":
					this.CMPSD ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMPSW":
				switch (parameterTypes) {
				case "CMPSW":
					this.CMPSW ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMPXCHG":
				switch (parameterTypes) {
				case "CMPXCHG ByteMemory R8Type":
					this.CMPXCHG (GetByteMemory (operands [0]), R8.GetByID (operands [1]));
					break;

				case "CMPXCHG DWordMemory R32Type":
					this.CMPXCHG (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "CMPXCHG R16Type R16Type":
					this.CMPXCHG (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "CMPXCHG R32Type R32Type":
					this.CMPXCHG (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "CMPXCHG R8Type R8Type":
					this.CMPXCHG (R8.GetByID (operands [0]), R8.GetByID (operands [1]));
					break;

				case "CMPXCHG WordMemory R16Type":
					this.CMPXCHG (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CMPXCHG8B":
				switch (parameterTypes) {
				case "CMPXCHG8B Memory":
					this.CMPXCHG8B (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CPUID":
				switch (parameterTypes) {
				case "CPUID":
					this.CPUID ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CWD":
				switch (parameterTypes) {
				case "CWD":
					this.CWD ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "CWDE":
				switch (parameterTypes) {
				case "CWDE":
					this.CWDE ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "DAA":
				switch (parameterTypes) {
				case "DAA":
					this.DAA ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "DAS":
				switch (parameterTypes) {
				case "DAS":
					this.DAS ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "DATA":
				switch (parameterTypes) {
				case "DATA Byte":
					this.DATA ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "DATA String":
					this.DATA ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "DATA String Byte":
					this.DATA ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString (), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "DATA String String":
					this.DATA ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString (), (operands [1] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "DATA String UInt16":
					this.DATA ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString (), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "DATA String UInt32":
					this.DATA ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString (), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "DATA UInt16":
					this.DATA ((UInt16) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "DATA UInt32":
					this.DATA ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "DEC":
				switch (parameterTypes) {
				case "DEC ByteMemory":
					this.DEC (GetByteMemory (operands [0]));
					break;

				case "DEC DWordMemory":
					this.DEC (GetDWordMemory (operands [0]));
					break;

				case "DEC R16Type":
					this.DEC (R16.GetByID (operands [0]));
					break;

				case "DEC R32Type":
					this.DEC (R32.GetByID (operands [0]));
					break;

				case "DEC R8Type":
					this.DEC (R8.GetByID (operands [0]));
					break;

				case "DEC WordMemory":
					this.DEC (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "DIV":
				switch (parameterTypes) {
				case "DIV ByteMemory":
					this.DIV (GetByteMemory (operands [0]));
					break;

				case "DIV DWordMemory":
					this.DIV (GetDWordMemory (operands [0]));
					break;

				case "DIV R16Type":
					this.DIV (R16.GetByID (operands [0]));
					break;

				case "DIV R32Type":
					this.DIV (R32.GetByID (operands [0]));
					break;

				case "DIV R8Type":
					this.DIV (R8.GetByID (operands [0]));
					break;

				case "DIV WordMemory":
					this.DIV (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "EMMS":
				switch (parameterTypes) {
				case "EMMS":
					this.EMMS ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "ENTER":
				switch (parameterTypes) {
				case "ENTER UInt16 Byte":
					this.ENTER ((UInt16) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value, (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "F2XM1":
				switch (parameterTypes) {
				case "F2XM1":
					this.F2XM1 ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FABS":
				switch (parameterTypes) {
				case "FABS":
					this.FABS ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FADD":
				switch (parameterTypes) {
				case "FADD DWordMemory":
					this.FADD (GetDWordMemory (operands [0]));
					break;

				case "FADD FPType":
					this.FADD (FP.GetByID (operands [0]));
					break;

				case "FADD QWordMemory":
					this.FADD (GetQWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FADDP":
				switch (parameterTypes) {
				case "FADDP FPType":
					this.FADDP (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FADDP__ST0":
				switch (parameterTypes) {
				case "FADDP__ST0 FPType":
					this.FADDP__ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FADD_ST0":
				switch (parameterTypes) {
				case "FADD_ST0 FPType":
					this.FADD_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FADD__ST0":
				switch (parameterTypes) {
				case "FADD__ST0 FPType":
					this.FADD__ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FBLD":
				switch (parameterTypes) {
				case "FBLD TWordMemory":
					this.FBLD (GetTWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FBSTP":
				switch (parameterTypes) {
				case "FBSTP TWordMemory":
					this.FBSTP (GetTWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCHS":
				switch (parameterTypes) {
				case "FCHS":
					this.FCHS ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCLEX":
				switch (parameterTypes) {
				case "FCLEX":
					this.FCLEX ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCMOVB":
				switch (parameterTypes) {
				case "FCMOVB FPType":
					this.FCMOVB (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCMOVBE":
				switch (parameterTypes) {
				case "FCMOVBE FPType":
					this.FCMOVBE (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCMOVBE_ST0":
				switch (parameterTypes) {
				case "FCMOVBE_ST0 FPType":
					this.FCMOVBE_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCMOVB_ST0":
				switch (parameterTypes) {
				case "FCMOVB_ST0 FPType":
					this.FCMOVB_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCMOVE":
				switch (parameterTypes) {
				case "FCMOVE FPType":
					this.FCMOVE (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCMOVE_ST0":
				switch (parameterTypes) {
				case "FCMOVE_ST0 FPType":
					this.FCMOVE_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCMOVNB":
				switch (parameterTypes) {
				case "FCMOVNB FPType":
					this.FCMOVNB (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCMOVNBE":
				switch (parameterTypes) {
				case "FCMOVNBE FPType":
					this.FCMOVNBE (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCMOVNBE_ST0":
				switch (parameterTypes) {
				case "FCMOVNBE_ST0 FPType":
					this.FCMOVNBE_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCMOVNB_ST0":
				switch (parameterTypes) {
				case "FCMOVNB_ST0 FPType":
					this.FCMOVNB_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCMOVNE":
				switch (parameterTypes) {
				case "FCMOVNE FPType":
					this.FCMOVNE (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCMOVNE_ST0":
				switch (parameterTypes) {
				case "FCMOVNE_ST0 FPType":
					this.FCMOVNE_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCMOVNU":
				switch (parameterTypes) {
				case "FCMOVNU FPType":
					this.FCMOVNU (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCMOVNU_ST0":
				switch (parameterTypes) {
				case "FCMOVNU_ST0 FPType":
					this.FCMOVNU_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCMOVU":
				switch (parameterTypes) {
				case "FCMOVU FPType":
					this.FCMOVU (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCMOVU_ST0":
				switch (parameterTypes) {
				case "FCMOVU_ST0 FPType":
					this.FCMOVU_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCOM":
				switch (parameterTypes) {
				case "FCOM DWordMemory":
					this.FCOM (GetDWordMemory (operands [0]));
					break;

				case "FCOM FPType":
					this.FCOM (FP.GetByID (operands [0]));
					break;

				case "FCOM QWordMemory":
					this.FCOM (GetQWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCOMI":
				switch (parameterTypes) {
				case "FCOMI FPType":
					this.FCOMI (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCOMIP":
				switch (parameterTypes) {
				case "FCOMIP FPType":
					this.FCOMIP (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCOMIP_ST0":
				switch (parameterTypes) {
				case "FCOMIP_ST0 FPType":
					this.FCOMIP_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCOMI_ST0":
				switch (parameterTypes) {
				case "FCOMI_ST0 FPType":
					this.FCOMI_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCOMP":
				switch (parameterTypes) {
				case "FCOMP DWordMemory":
					this.FCOMP (GetDWordMemory (operands [0]));
					break;

				case "FCOMP FPType":
					this.FCOMP (FP.GetByID (operands [0]));
					break;

				case "FCOMP QWordMemory":
					this.FCOMP (GetQWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCOMPP":
				switch (parameterTypes) {
				case "FCOMPP":
					this.FCOMPP ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCOMP_ST0":
				switch (parameterTypes) {
				case "FCOMP_ST0 FPType":
					this.FCOMP_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCOM_ST0":
				switch (parameterTypes) {
				case "FCOM_ST0 FPType":
					this.FCOM_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FCOS":
				switch (parameterTypes) {
				case "FCOS":
					this.FCOS ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FDECSTP":
				switch (parameterTypes) {
				case "FDECSTP":
					this.FDECSTP ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FDISI":
				switch (parameterTypes) {
				case "FDISI":
					this.FDISI ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FDIV":
				switch (parameterTypes) {
				case "FDIV DWordMemory":
					this.FDIV (GetDWordMemory (operands [0]));
					break;

				case "FDIV FPType":
					this.FDIV (FP.GetByID (operands [0]));
					break;

				case "FDIV QWordMemory":
					this.FDIV (GetQWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FDIVP":
				switch (parameterTypes) {
				case "FDIVP FPType":
					this.FDIVP (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FDIVP__ST0":
				switch (parameterTypes) {
				case "FDIVP__ST0 FPType":
					this.FDIVP__ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FDIVR":
				switch (parameterTypes) {
				case "FDIVR DWordMemory":
					this.FDIVR (GetDWordMemory (operands [0]));
					break;

				case "FDIVR FPType":
					this.FDIVR (FP.GetByID (operands [0]));
					break;

				case "FDIVR QWordMemory":
					this.FDIVR (GetQWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FDIVRP":
				switch (parameterTypes) {
				case "FDIVRP FPType":
					this.FDIVRP (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FDIVRP__ST0":
				switch (parameterTypes) {
				case "FDIVRP__ST0 FPType":
					this.FDIVRP__ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FDIVR_ST0":
				switch (parameterTypes) {
				case "FDIVR_ST0 FPType":
					this.FDIVR_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FDIVR__ST0":
				switch (parameterTypes) {
				case "FDIVR__ST0 FPType":
					this.FDIVR__ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FDIV_ST0":
				switch (parameterTypes) {
				case "FDIV_ST0 FPType":
					this.FDIV_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FDIV__ST0":
				switch (parameterTypes) {
				case "FDIV__ST0 FPType":
					this.FDIV__ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FENI":
				switch (parameterTypes) {
				case "FENI":
					this.FENI ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FFREE":
				switch (parameterTypes) {
				case "FFREE FPType":
					this.FFREE (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FFREEP":
				switch (parameterTypes) {
				case "FFREEP FPType":
					this.FFREEP (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FIADD":
				switch (parameterTypes) {
				case "FIADD DWordMemory":
					this.FIADD (GetDWordMemory (operands [0]));
					break;

				case "FIADD WordMemory":
					this.FIADD (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FICOM":
				switch (parameterTypes) {
				case "FICOM DWordMemory":
					this.FICOM (GetDWordMemory (operands [0]));
					break;

				case "FICOM WordMemory":
					this.FICOM (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FICOMP":
				switch (parameterTypes) {
				case "FICOMP DWordMemory":
					this.FICOMP (GetDWordMemory (operands [0]));
					break;

				case "FICOMP WordMemory":
					this.FICOMP (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FIDIV":
				switch (parameterTypes) {
				case "FIDIV DWordMemory":
					this.FIDIV (GetDWordMemory (operands [0]));
					break;

				case "FIDIV WordMemory":
					this.FIDIV (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FIDIVR":
				switch (parameterTypes) {
				case "FIDIVR DWordMemory":
					this.FIDIVR (GetDWordMemory (operands [0]));
					break;

				case "FIDIVR WordMemory":
					this.FIDIVR (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FILD":
				switch (parameterTypes) {
				case "FILD DWordMemory":
					this.FILD (GetDWordMemory (operands [0]));
					break;

				case "FILD QWordMemory":
					this.FILD (GetQWordMemory (operands [0]));
					break;

				case "FILD WordMemory":
					this.FILD (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FIMUL":
				switch (parameterTypes) {
				case "FIMUL DWordMemory":
					this.FIMUL (GetDWordMemory (operands [0]));
					break;

				case "FIMUL WordMemory":
					this.FIMUL (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FINCSTP":
				switch (parameterTypes) {
				case "FINCSTP":
					this.FINCSTP ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FINIT":
				switch (parameterTypes) {
				case "FINIT":
					this.FINIT ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FIST":
				switch (parameterTypes) {
				case "FIST DWordMemory":
					this.FIST (GetDWordMemory (operands [0]));
					break;

				case "FIST WordMemory":
					this.FIST (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FISTP":
				switch (parameterTypes) {
				case "FISTP DWordMemory":
					this.FISTP (GetDWordMemory (operands [0]));
					break;

				case "FISTP QWordMemory":
					this.FISTP (GetQWordMemory (operands [0]));
					break;

				case "FISTP WordMemory":
					this.FISTP (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FISUB":
				switch (parameterTypes) {
				case "FISUB DWordMemory":
					this.FISUB (GetDWordMemory (operands [0]));
					break;

				case "FISUB WordMemory":
					this.FISUB (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FISUBR":
				switch (parameterTypes) {
				case "FISUBR DWordMemory":
					this.FISUBR (GetDWordMemory (operands [0]));
					break;

				case "FISUBR WordMemory":
					this.FISUBR (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FLD":
				switch (parameterTypes) {
				case "FLD DWordMemory":
					this.FLD (GetDWordMemory (operands [0]));
					break;

				case "FLD FPType":
					this.FLD (FP.GetByID (operands [0]));
					break;

				case "FLD QWordMemory":
					this.FLD (GetQWordMemory (operands [0]));
					break;

				case "FLD TWordMemory":
					this.FLD (GetTWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FLD1":
				switch (parameterTypes) {
				case "FLD1":
					this.FLD1 ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FLDCW":
				switch (parameterTypes) {
				case "FLDCW WordMemory":
					this.FLDCW (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FLDENV":
				switch (parameterTypes) {
				case "FLDENV Memory":
					this.FLDENV (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FLDL2E":
				switch (parameterTypes) {
				case "FLDL2E":
					this.FLDL2E ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FLDL2T":
				switch (parameterTypes) {
				case "FLDL2T":
					this.FLDL2T ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FLDLG2":
				switch (parameterTypes) {
				case "FLDLG2":
					this.FLDLG2 ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FLDLN2":
				switch (parameterTypes) {
				case "FLDLN2":
					this.FLDLN2 ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FLDPI":
				switch (parameterTypes) {
				case "FLDPI":
					this.FLDPI ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FLDZ":
				switch (parameterTypes) {
				case "FLDZ":
					this.FLDZ ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FMUL":
				switch (parameterTypes) {
				case "FMUL DWordMemory":
					this.FMUL (GetDWordMemory (operands [0]));
					break;

				case "FMUL FPType":
					this.FMUL (FP.GetByID (operands [0]));
					break;

				case "FMUL QWordMemory":
					this.FMUL (GetQWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FMULP":
				switch (parameterTypes) {
				case "FMULP FPType":
					this.FMULP (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FMULP__ST0":
				switch (parameterTypes) {
				case "FMULP__ST0 FPType":
					this.FMULP__ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FMUL_ST0":
				switch (parameterTypes) {
				case "FMUL_ST0 FPType":
					this.FMUL_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FMUL__ST0":
				switch (parameterTypes) {
				case "FMUL__ST0 FPType":
					this.FMUL__ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FNCLEX":
				switch (parameterTypes) {
				case "FNCLEX":
					this.FNCLEX ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FNDISI":
				switch (parameterTypes) {
				case "FNDISI":
					this.FNDISI ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FNENI":
				switch (parameterTypes) {
				case "FNENI":
					this.FNENI ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FNINIT":
				switch (parameterTypes) {
				case "FNINIT":
					this.FNINIT ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FNOP":
				switch (parameterTypes) {
				case "FNOP":
					this.FNOP ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FNSAVE":
				switch (parameterTypes) {
				case "FNSAVE Memory":
					this.FNSAVE (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FNSTCW":
				switch (parameterTypes) {
				case "FNSTCW WordMemory":
					this.FNSTCW (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FNSTENV":
				switch (parameterTypes) {
				case "FNSTENV Memory":
					this.FNSTENV (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FNSTSW":
				switch (parameterTypes) {
				case "FNSTSW WordMemory":
					this.FNSTSW (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FNSTSW_AX":
				switch (parameterTypes) {
				case "FNSTSW_AX":
					this.FNSTSW_AX ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FPATAN":
				switch (parameterTypes) {
				case "FPATAN":
					this.FPATAN ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FPREM":
				switch (parameterTypes) {
				case "FPREM":
					this.FPREM ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FPREM1":
				switch (parameterTypes) {
				case "FPREM1":
					this.FPREM1 ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FPTAN":
				switch (parameterTypes) {
				case "FPTAN":
					this.FPTAN ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FRNDINT":
				switch (parameterTypes) {
				case "FRNDINT":
					this.FRNDINT ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FRSTOR":
				switch (parameterTypes) {
				case "FRSTOR Memory":
					this.FRSTOR (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSAVE":
				switch (parameterTypes) {
				case "FSAVE Memory":
					this.FSAVE (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSCALE":
				switch (parameterTypes) {
				case "FSCALE":
					this.FSCALE ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSETPM":
				switch (parameterTypes) {
				case "FSETPM":
					this.FSETPM ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSIN":
				switch (parameterTypes) {
				case "FSIN":
					this.FSIN ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSINCOS":
				switch (parameterTypes) {
				case "FSINCOS":
					this.FSINCOS ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSQRT":
				switch (parameterTypes) {
				case "FSQRT":
					this.FSQRT ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FST":
				switch (parameterTypes) {
				case "FST DWordMemory":
					this.FST (GetDWordMemory (operands [0]));
					break;

				case "FST FPType":
					this.FST (FP.GetByID (operands [0]));
					break;

				case "FST QWordMemory":
					this.FST (GetQWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSTCW":
				switch (parameterTypes) {
				case "FSTCW WordMemory":
					this.FSTCW (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSTENV":
				switch (parameterTypes) {
				case "FSTENV Memory":
					this.FSTENV (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSTP":
				switch (parameterTypes) {
				case "FSTP DWordMemory":
					this.FSTP (GetDWordMemory (operands [0]));
					break;

				case "FSTP FPType":
					this.FSTP (FP.GetByID (operands [0]));
					break;

				case "FSTP QWordMemory":
					this.FSTP (GetQWordMemory (operands [0]));
					break;

				case "FSTP TWordMemory":
					this.FSTP (GetTWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSTSW":
				switch (parameterTypes) {
				case "FSTSW WordMemory":
					this.FSTSW (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSTSW_AX":
				switch (parameterTypes) {
				case "FSTSW_AX":
					this.FSTSW_AX ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSUB":
				switch (parameterTypes) {
				case "FSUB DWordMemory":
					this.FSUB (GetDWordMemory (operands [0]));
					break;

				case "FSUB FPType":
					this.FSUB (FP.GetByID (operands [0]));
					break;

				case "FSUB QWordMemory":
					this.FSUB (GetQWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSUBP":
				switch (parameterTypes) {
				case "FSUBP FPType":
					this.FSUBP (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSUBP__ST0":
				switch (parameterTypes) {
				case "FSUBP__ST0 FPType":
					this.FSUBP__ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSUBR":
				switch (parameterTypes) {
				case "FSUBR DWordMemory":
					this.FSUBR (GetDWordMemory (operands [0]));
					break;

				case "FSUBR FPType":
					this.FSUBR (FP.GetByID (operands [0]));
					break;

				case "FSUBR QWordMemory":
					this.FSUBR (GetQWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSUBRP":
				switch (parameterTypes) {
				case "FSUBRP FPType":
					this.FSUBRP (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSUBRP__ST0":
				switch (parameterTypes) {
				case "FSUBRP__ST0 FPType":
					this.FSUBRP__ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSUBR_ST0":
				switch (parameterTypes) {
				case "FSUBR_ST0 FPType":
					this.FSUBR_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSUBR__ST0":
				switch (parameterTypes) {
				case "FSUBR__ST0 FPType":
					this.FSUBR__ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSUB_ST0":
				switch (parameterTypes) {
				case "FSUB_ST0 FPType":
					this.FSUB_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FSUB__ST0":
				switch (parameterTypes) {
				case "FSUB__ST0 FPType":
					this.FSUB__ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FTST":
				switch (parameterTypes) {
				case "FTST":
					this.FTST ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FUCOM":
				switch (parameterTypes) {
				case "FUCOM FPType":
					this.FUCOM (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FUCOMI":
				switch (parameterTypes) {
				case "FUCOMI FPType":
					this.FUCOMI (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FUCOMIP":
				switch (parameterTypes) {
				case "FUCOMIP FPType":
					this.FUCOMIP (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FUCOMIP_ST0":
				switch (parameterTypes) {
				case "FUCOMIP_ST0 FPType":
					this.FUCOMIP_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FUCOMI_ST0":
				switch (parameterTypes) {
				case "FUCOMI_ST0 FPType":
					this.FUCOMI_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FUCOMP":
				switch (parameterTypes) {
				case "FUCOMP FPType":
					this.FUCOMP (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FUCOMPP":
				switch (parameterTypes) {
				case "FUCOMPP":
					this.FUCOMPP ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FUCOMP_ST0":
				switch (parameterTypes) {
				case "FUCOMP_ST0 FPType":
					this.FUCOMP_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FUCOM_ST0":
				switch (parameterTypes) {
				case "FUCOM_ST0 FPType":
					this.FUCOM_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FWAIT":
				switch (parameterTypes) {
				case "FWAIT":
					this.FWAIT ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FXAM":
				switch (parameterTypes) {
				case "FXAM":
					this.FXAM ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FXCH":
				switch (parameterTypes) {
				case "FXCH":
					this.FXCH ();
					break;

				case "FXCH FPType":
					this.FXCH (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FXCH_ST0":
				switch (parameterTypes) {
				case "FXCH_ST0 FPType":
					this.FXCH_ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FXCH__ST0":
				switch (parameterTypes) {
				case "FXCH__ST0 FPType":
					this.FXCH__ST0 (FP.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FXRSTOR":
				switch (parameterTypes) {
				case "FXRSTOR Memory":
					this.FXRSTOR (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FXSAVE":
				switch (parameterTypes) {
				case "FXSAVE Memory":
					this.FXSAVE (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FXTRACT":
				switch (parameterTypes) {
				case "FXTRACT":
					this.FXTRACT ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FYL2X":
				switch (parameterTypes) {
				case "FYL2X":
					this.FYL2X ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "FYL2XP1":
				switch (parameterTypes) {
				case "FYL2XP1":
					this.FYL2XP1 ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "HLT":
				switch (parameterTypes) {
				case "HLT":
					this.HLT ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "ICEBP":
				switch (parameterTypes) {
				case "ICEBP":
					this.ICEBP ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "IDIV":
				switch (parameterTypes) {
				case "IDIV ByteMemory":
					this.IDIV (GetByteMemory (operands [0]));
					break;

				case "IDIV DWordMemory":
					this.IDIV (GetDWordMemory (operands [0]));
					break;

				case "IDIV R16Type":
					this.IDIV (R16.GetByID (operands [0]));
					break;

				case "IDIV R32Type":
					this.IDIV (R32.GetByID (operands [0]));
					break;

				case "IDIV R8Type":
					this.IDIV (R8.GetByID (operands [0]));
					break;

				case "IDIV WordMemory":
					this.IDIV (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "IMUL":
				switch (parameterTypes) {
				case "IMUL ByteMemory":
					this.IMUL (GetByteMemory (operands [0]));
					break;

				case "IMUL DWordMemory":
					this.IMUL (GetDWordMemory (operands [0]));
					break;

				case "IMUL R16Type":
					this.IMUL (R16.GetByID (operands [0]));
					break;

				case "IMUL R16Type Byte":
					this.IMUL (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "IMUL R16Type R16Type":
					this.IMUL (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "IMUL R16Type R16Type Byte":
					this.IMUL (R16.GetByID (operands [0]), R16.GetByID (operands [1]), (byte) (operands [2] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "IMUL R16Type R16Type UInt16":
					this.IMUL (R16.GetByID (operands [0]), R16.GetByID (operands [1]), (UInt16) (operands [2] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "IMUL R16Type UInt16":
					this.IMUL (R16.GetByID (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "IMUL R16Type WordMemory":
					this.IMUL (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "IMUL R16Type WordMemory Byte":
					this.IMUL (R16.GetByID (operands [0]), GetWordMemory (operands [1]), (byte) (operands [2] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "IMUL R16Type WordMemory UInt16":
					this.IMUL (R16.GetByID (operands [0]), GetWordMemory (operands [1]), (UInt16) (operands [2] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "IMUL R32Type":
					this.IMUL (R32.GetByID (operands [0]));
					break;

				case "IMUL R32Type Byte":
					this.IMUL (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "IMUL R32Type DWordMemory":
					this.IMUL (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "IMUL R32Type DWordMemory Byte":
					this.IMUL (R32.GetByID (operands [0]), GetDWordMemory (operands [1]), (byte) (operands [2] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "IMUL R32Type DWordMemory UInt32":
					this.IMUL (R32.GetByID (operands [0]), GetDWordMemory (operands [1]), (UInt32) (operands [2] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "IMUL R32Type R32Type":
					this.IMUL (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "IMUL R32Type R32Type Byte":
					this.IMUL (R32.GetByID (operands [0]), R32.GetByID (operands [1]), (byte) (operands [2] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "IMUL R32Type R32Type UInt32":
					this.IMUL (R32.GetByID (operands [0]), R32.GetByID (operands [1]), (UInt32) (operands [2] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "IMUL R32Type UInt32":
					this.IMUL (R32.GetByID (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "IMUL R8Type":
					this.IMUL (R8.GetByID (operands [0]));
					break;

				case "IMUL WordMemory":
					this.IMUL (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "INC":
				switch (parameterTypes) {
				case "INC ByteMemory":
					this.INC (GetByteMemory (operands [0]));
					break;

				case "INC DWordMemory":
					this.INC (GetDWordMemory (operands [0]));
					break;

				case "INC R16Type":
					this.INC (R16.GetByID (operands [0]));
					break;

				case "INC R32Type":
					this.INC (R32.GetByID (operands [0]));
					break;

				case "INC R8Type":
					this.INC (R8.GetByID (operands [0]));
					break;

				case "INC WordMemory":
					this.INC (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "INSB":
				switch (parameterTypes) {
				case "INSB":
					this.INSB ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "INSD":
				switch (parameterTypes) {
				case "INSD":
					this.INSD ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "INSW":
				switch (parameterTypes) {
				case "INSW":
					this.INSW ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "INT":
				switch (parameterTypes) {
				case "INT Byte":
					this.INT ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "INTO":
				switch (parameterTypes) {
				case "INTO":
					this.INTO ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "INVD":
				switch (parameterTypes) {
				case "INVD":
					this.INVD ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "INVLPG":
				switch (parameterTypes) {
				case "INVLPG Memory":
					this.INVLPG (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "IN_AL":
				switch (parameterTypes) {
				case "IN_AL Byte":
					this.IN_AL ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "IN_AL__DX":
				switch (parameterTypes) {
				case "IN_AL__DX":
					this.IN_AL__DX ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "IN_AX":
				switch (parameterTypes) {
				case "IN_AX Byte":
					this.IN_AX ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "IN_AX__DX":
				switch (parameterTypes) {
				case "IN_AX__DX":
					this.IN_AX__DX ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "IN_EAX":
				switch (parameterTypes) {
				case "IN_EAX Byte":
					this.IN_EAX ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "IN_EAX__DX":
				switch (parameterTypes) {
				case "IN_EAX__DX":
					this.IN_EAX__DX ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "IRET":
				switch (parameterTypes) {
				case "IRET":
					this.IRET ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "IRETD":
				switch (parameterTypes) {
				case "IRETD":
					this.IRETD ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "IRETW":
				switch (parameterTypes) {
				case "IRETW":
					this.IRETW ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JA":
				switch (parameterTypes) {
				case "JA Byte":
					this.JA ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JA String":
					this.JA ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JA UInt32":
					this.JA ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JAE":
				switch (parameterTypes) {
				case "JAE Byte":
					this.JAE ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JAE String":
					this.JAE ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JAE UInt32":
					this.JAE ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JB":
				switch (parameterTypes) {
				case "JB Byte":
					this.JB ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JB String":
					this.JB ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JB UInt32":
					this.JB ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JBE":
				switch (parameterTypes) {
				case "JBE Byte":
					this.JBE ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JBE String":
					this.JBE ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JBE UInt32":
					this.JBE ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JC":
				switch (parameterTypes) {
				case "JC Byte":
					this.JC ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JC String":
					this.JC ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JC UInt32":
					this.JC ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JCXZ":
				switch (parameterTypes) {
				case "JCXZ Byte":
					this.JCXZ ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JE":
				switch (parameterTypes) {
				case "JE Byte":
					this.JE ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JE String":
					this.JE ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JE UInt32":
					this.JE ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JECXZ":
				switch (parameterTypes) {
				case "JECXZ Byte":
					this.JECXZ ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JG":
				switch (parameterTypes) {
				case "JG Byte":
					this.JG ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JG String":
					this.JG ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JG UInt32":
					this.JG ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JGE":
				switch (parameterTypes) {
				case "JGE Byte":
					this.JGE ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JGE String":
					this.JGE ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JGE UInt32":
					this.JGE ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JL":
				switch (parameterTypes) {
				case "JL Byte":
					this.JL ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JL String":
					this.JL ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JL UInt32":
					this.JL ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JLE":
				switch (parameterTypes) {
				case "JLE Byte":
					this.JLE ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JLE String":
					this.JLE ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JLE UInt32":
					this.JLE ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JMP":
				switch (parameterTypes) {
				case "JMP Byte":
					this.JMP ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JMP DWordMemory":
					this.JMP (GetDWordMemory (operands [0]));
					break;

				case "JMP R16Type":
					this.JMP (R16.GetByID (operands [0]));
					break;

				case "JMP R32Type":
					this.JMP (R32.GetByID (operands [0]));
					break;

				case "JMP String":
					this.JMP ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JMP UInt16 String":
					this.JMP ((UInt16) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value, (operands [1] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JMP UInt16 UInt16":
					this.JMP ((UInt16) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value, (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JMP UInt16 UInt32":
					this.JMP ((UInt16) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value, (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JMP UInt32":
					this.JMP ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JMP WordMemory":
					this.JMP (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JMP_FAR":
				switch (parameterTypes) {
				case "JMP_FAR DWordMemory":
					this.JMP_FAR (GetDWordMemory (operands [0]));
					break;

				case "JMP_FAR Memory":
					this.JMP_FAR (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JNA":
				switch (parameterTypes) {
				case "JNA Byte":
					this.JNA ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JNA String":
					this.JNA ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JNA UInt32":
					this.JNA ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JNAE":
				switch (parameterTypes) {
				case "JNAE Byte":
					this.JNAE ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JNAE String":
					this.JNAE ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JNAE UInt32":
					this.JNAE ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JNB":
				switch (parameterTypes) {
				case "JNB Byte":
					this.JNB ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JNB String":
					this.JNB ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JNB UInt32":
					this.JNB ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JNBE":
				switch (parameterTypes) {
				case "JNBE Byte":
					this.JNBE ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JNBE String":
					this.JNBE ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JNBE UInt32":
					this.JNBE ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JNC":
				switch (parameterTypes) {
				case "JNC Byte":
					this.JNC ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JNC String":
					this.JNC ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JNC UInt32":
					this.JNC ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JNE":
				switch (parameterTypes) {
				case "JNE Byte":
					this.JNE ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JNE String":
					this.JNE ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JNE UInt32":
					this.JNE ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JNG":
				switch (parameterTypes) {
				case "JNG Byte":
					this.JNG ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JNG String":
					this.JNG ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JNG UInt32":
					this.JNG ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JNGE":
				switch (parameterTypes) {
				case "JNGE Byte":
					this.JNGE ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JNGE String":
					this.JNGE ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JNGE UInt32":
					this.JNGE ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JNL":
				switch (parameterTypes) {
				case "JNL Byte":
					this.JNL ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JNL String":
					this.JNL ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JNL UInt32":
					this.JNL ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JNLE":
				switch (parameterTypes) {
				case "JNLE Byte":
					this.JNLE ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JNLE String":
					this.JNLE ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JNLE UInt32":
					this.JNLE ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JNO":
				switch (parameterTypes) {
				case "JNO Byte":
					this.JNO ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JNO String":
					this.JNO ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JNO UInt32":
					this.JNO ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JNP":
				switch (parameterTypes) {
				case "JNP Byte":
					this.JNP ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JNP String":
					this.JNP ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JNP UInt32":
					this.JNP ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JNS":
				switch (parameterTypes) {
				case "JNS Byte":
					this.JNS ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JNS String":
					this.JNS ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JNS UInt32":
					this.JNS ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JNZ":
				switch (parameterTypes) {
				case "JNZ Byte":
					this.JNZ ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JNZ String":
					this.JNZ ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JNZ UInt32":
					this.JNZ ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JO":
				switch (parameterTypes) {
				case "JO Byte":
					this.JO ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JO String":
					this.JO ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JO UInt32":
					this.JO ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JP":
				switch (parameterTypes) {
				case "JP Byte":
					this.JP ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JP String":
					this.JP ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JP UInt32":
					this.JP ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JPE":
				switch (parameterTypes) {
				case "JPE Byte":
					this.JPE ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JPE String":
					this.JPE ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JPE UInt32":
					this.JPE ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JPO":
				switch (parameterTypes) {
				case "JPO Byte":
					this.JPO ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JPO String":
					this.JPO ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JPO UInt32":
					this.JPO ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JS":
				switch (parameterTypes) {
				case "JS Byte":
					this.JS ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JS String":
					this.JS ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JS UInt32":
					this.JS ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "JZ":
				switch (parameterTypes) {
				case "JZ Byte":
					this.JZ ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "JZ String":
					this.JZ ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "JZ UInt32":
					this.JZ ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LABEL":
				switch (parameterTypes) {
				case "LABEL String":
					this.LABEL ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LAHF":
				switch (parameterTypes) {
				case "LAHF":
					this.LAHF ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LAR":
				switch (parameterTypes) {
				case "LAR R16Type R16Type":
					this.LAR (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "LAR R16Type WordMemory":
					this.LAR (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "LAR R32Type DWordMemory":
					this.LAR (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "LAR R32Type R32Type":
					this.LAR (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LDS":
				switch (parameterTypes) {
				case "LDS R16Type Memory":
					this.LDS (R16.GetByID (operands [0]), GetMemory (operands [1]));
					break;

				case "LDS R32Type Memory":
					this.LDS (R32.GetByID (operands [0]), GetMemory (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LEA":
				switch (parameterTypes) {
				case "LEA R16Type Memory":
					this.LEA (R16.GetByID (operands [0]), GetMemory (operands [1]));
					break;

				case "LEA R32Type Memory":
					this.LEA (R32.GetByID (operands [0]), GetMemory (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LEAVE":
				switch (parameterTypes) {
				case "LEAVE":
					this.LEAVE ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LES":
				switch (parameterTypes) {
				case "LES R16Type Memory":
					this.LES (R16.GetByID (operands [0]), GetMemory (operands [1]));
					break;

				case "LES R32Type Memory":
					this.LES (R32.GetByID (operands [0]), GetMemory (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LFENCE":
				switch (parameterTypes) {
				case "LFENCE":
					this.LFENCE ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LFS":
				switch (parameterTypes) {
				case "LFS R16Type Memory":
					this.LFS (R16.GetByID (operands [0]), GetMemory (operands [1]));
					break;

				case "LFS R32Type Memory":
					this.LFS (R32.GetByID (operands [0]), GetMemory (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LGDT":
				switch (parameterTypes) {
				case "LGDT Memory":
					this.LGDT (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LGS":
				switch (parameterTypes) {
				case "LGS R16Type Memory":
					this.LGS (R16.GetByID (operands [0]), GetMemory (operands [1]));
					break;

				case "LGS R32Type Memory":
					this.LGS (R32.GetByID (operands [0]), GetMemory (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LIDT":
				switch (parameterTypes) {
				case "LIDT Memory":
					this.LIDT (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LLDT":
				switch (parameterTypes) {
				case "LLDT R16Type":
					this.LLDT (R16.GetByID (operands [0]));
					break;

				case "LLDT WordMemory":
					this.LLDT (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LMSW":
				switch (parameterTypes) {
				case "LMSW R16Type":
					this.LMSW (R16.GetByID (operands [0]));
					break;

				case "LMSW WordMemory":
					this.LMSW (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LOCK":
				switch (parameterTypes) {
				case "LOCK":
					this.LOCK ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LODSB":
				switch (parameterTypes) {
				case "LODSB":
					this.LODSB ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LODSD":
				switch (parameterTypes) {
				case "LODSD":
					this.LODSD ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LODSW":
				switch (parameterTypes) {
				case "LODSW":
					this.LODSW ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LOOP":
				switch (parameterTypes) {
				case "LOOP Byte":
					this.LOOP ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LOOPE":
				switch (parameterTypes) {
				case "LOOPE Byte":
					this.LOOPE ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LOOPNE":
				switch (parameterTypes) {
				case "LOOPNE Byte":
					this.LOOPNE ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LOOPNZ":
				switch (parameterTypes) {
				case "LOOPNZ Byte":
					this.LOOPNZ ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LOOPZ":
				switch (parameterTypes) {
				case "LOOPZ Byte":
					this.LOOPZ ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LSL":
				switch (parameterTypes) {
				case "LSL R16Type R16Type":
					this.LSL (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "LSL R16Type WordMemory":
					this.LSL (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "LSL R32Type DWordMemory":
					this.LSL (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "LSL R32Type R32Type":
					this.LSL (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LSS":
				switch (parameterTypes) {
				case "LSS R16Type Memory":
					this.LSS (R16.GetByID (operands [0]), GetMemory (operands [1]));
					break;

				case "LSS R32Type Memory":
					this.LSS (R32.GetByID (operands [0]), GetMemory (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "LTR":
				switch (parameterTypes) {
				case "LTR R16Type":
					this.LTR (R16.GetByID (operands [0]));
					break;

				case "LTR WordMemory":
					this.LTR (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "MFENCE":
				switch (parameterTypes) {
				case "MFENCE":
					this.MFENCE ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "MOV":
				switch (parameterTypes) {
				case "MOV ByteMemory Byte":
					this.MOV (GetByteMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "MOV ByteMemory R8Type":
					this.MOV (GetByteMemory (operands [0]), R8.GetByID (operands [1]));
					break;

				case "MOV CRType R32Type":
					this.MOV (CR.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "MOV DRType R32Type":
					this.MOV (DR.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "MOV DWordMemory R32Type":
					this.MOV (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "MOV DWordMemory SegType":
					this.MOV (GetDWordMemory (operands [0]), Seg.GetByID (operands [1]));
					break;

				case "MOV DWordMemory UInt32":
					this.MOV (GetDWordMemory (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "MOV R16Type R16Type":
					this.MOV (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "MOV R16Type SegType":
					this.MOV (R16.GetByID (operands [0]), Seg.GetByID (operands [1]));
					break;

				case "MOV R16Type String":
					this.MOV (R16.GetByID (operands [0]), (operands [1] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "MOV R16Type UInt16":
					this.MOV (R16.GetByID (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "MOV R16Type WordMemory":
					this.MOV (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "MOV R32Type CRType":
					this.MOV (R32.GetByID (operands [0]), CR.GetByID (operands [1]));
					break;

				case "MOV R32Type DRType":
					this.MOV (R32.GetByID (operands [0]), DR.GetByID (operands [1]));
					break;

				case "MOV R32Type DWordMemory":
					this.MOV (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "MOV R32Type R32Type":
					this.MOV (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "MOV R32Type SegType":
					this.MOV (R32.GetByID (operands [0]), Seg.GetByID (operands [1]));
					break;

				case "MOV R32Type String":
					this.MOV (R32.GetByID (operands [0]), (operands [1] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				case "MOV R32Type TRType":
					this.MOV (R32.GetByID (operands [0]), TR.GetByID (operands [1]));
					break;

				case "MOV R32Type UInt32":
					this.MOV (R32.GetByID (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "MOV R8Type Byte":
					this.MOV (R8.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "MOV R8Type ByteMemory":
					this.MOV (R8.GetByID (operands [0]), GetByteMemory (operands [1]));
					break;

				case "MOV R8Type R8Type":
					this.MOV (R8.GetByID (operands [0]), R8.GetByID (operands [1]));
					break;

				case "MOV SegType DWordMemory":
					this.MOV (Seg.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "MOV SegType R16Type":
					this.MOV (Seg.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "MOV SegType R32Type":
					this.MOV (Seg.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "MOV SegType WordMemory":
					this.MOV (Seg.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "MOV TRType R32Type":
					this.MOV (TR.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "MOV WordMemory R16Type":
					this.MOV (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				case "MOV WordMemory SegType":
					this.MOV (GetWordMemory (operands [0]), Seg.GetByID (operands [1]));
					break;

				case "MOV WordMemory UInt16":
					this.MOV (GetWordMemory (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "MOVSB":
				switch (parameterTypes) {
				case "MOVSB":
					this.MOVSB ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "MOVSD":
				switch (parameterTypes) {
				case "MOVSD":
					this.MOVSD ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "MOVSW":
				switch (parameterTypes) {
				case "MOVSW":
					this.MOVSW ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "MOVSX":
				switch (parameterTypes) {
				case "MOVSX R16Type ByteMemory":
					this.MOVSX (R16.GetByID (operands [0]), GetByteMemory (operands [1]));
					break;

				case "MOVSX R16Type R8Type":
					this.MOVSX (R16.GetByID (operands [0]), R8.GetByID (operands [1]));
					break;

				case "MOVSX R32Type ByteMemory":
					this.MOVSX (R32.GetByID (operands [0]), GetByteMemory (operands [1]));
					break;

				case "MOVSX R32Type R16Type":
					this.MOVSX (R32.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "MOVSX R32Type R8Type":
					this.MOVSX (R32.GetByID (operands [0]), R8.GetByID (operands [1]));
					break;

				case "MOVSX R32Type WordMemory":
					this.MOVSX (R32.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "MOVZX":
				switch (parameterTypes) {
				case "MOVZX R16Type ByteMemory":
					this.MOVZX (R16.GetByID (operands [0]), GetByteMemory (operands [1]));
					break;

				case "MOVZX R16Type R8Type":
					this.MOVZX (R16.GetByID (operands [0]), R8.GetByID (operands [1]));
					break;

				case "MOVZX R32Type ByteMemory":
					this.MOVZX (R32.GetByID (operands [0]), GetByteMemory (operands [1]));
					break;

				case "MOVZX R32Type R16Type":
					this.MOVZX (R32.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "MOVZX R32Type R8Type":
					this.MOVZX (R32.GetByID (operands [0]), R8.GetByID (operands [1]));
					break;

				case "MOVZX R32Type WordMemory":
					this.MOVZX (R32.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "MOV_AL":
				switch (parameterTypes) {
				case "MOV_AL Byte":
					this.MOV_AL ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "MOV_AX":
				switch (parameterTypes) {
				case "MOV_AX UInt16":
					this.MOV_AX ((UInt16) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "MOV_EAX":
				switch (parameterTypes) {
				case "MOV_EAX UInt32":
					this.MOV_EAX ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "MOV__AL":
				switch (parameterTypes) {
				case "MOV__AL Byte":
					this.MOV__AL ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "MOV__AX":
				switch (parameterTypes) {
				case "MOV__AX UInt16":
					this.MOV__AX ((UInt16) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "MOV__EAX":
				switch (parameterTypes) {
				case "MOV__EAX UInt32":
					this.MOV__EAX ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "MUL":
				switch (parameterTypes) {
				case "MUL ByteMemory":
					this.MUL (GetByteMemory (operands [0]));
					break;

				case "MUL DWordMemory":
					this.MUL (GetDWordMemory (operands [0]));
					break;

				case "MUL R16Type":
					this.MUL (R16.GetByID (operands [0]));
					break;

				case "MUL R32Type":
					this.MUL (R32.GetByID (operands [0]));
					break;

				case "MUL R8Type":
					this.MUL (R8.GetByID (operands [0]));
					break;

				case "MUL WordMemory":
					this.MUL (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "NEG":
				switch (parameterTypes) {
				case "NEG ByteMemory":
					this.NEG (GetByteMemory (operands [0]));
					break;

				case "NEG DWordMemory":
					this.NEG (GetDWordMemory (operands [0]));
					break;

				case "NEG R16Type":
					this.NEG (R16.GetByID (operands [0]));
					break;

				case "NEG R32Type":
					this.NEG (R32.GetByID (operands [0]));
					break;

				case "NEG R8Type":
					this.NEG (R8.GetByID (operands [0]));
					break;

				case "NEG WordMemory":
					this.NEG (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "NOP":
				switch (parameterTypes) {
				case "NOP":
					this.NOP ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "NOT":
				switch (parameterTypes) {
				case "NOT ByteMemory":
					this.NOT (GetByteMemory (operands [0]));
					break;

				case "NOT DWordMemory":
					this.NOT (GetDWordMemory (operands [0]));
					break;

				case "NOT R16Type":
					this.NOT (R16.GetByID (operands [0]));
					break;

				case "NOT R32Type":
					this.NOT (R32.GetByID (operands [0]));
					break;

				case "NOT R8Type":
					this.NOT (R8.GetByID (operands [0]));
					break;

				case "NOT WordMemory":
					this.NOT (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "OFFSET":
				switch (parameterTypes) {
				case "OFFSET UInt32":
					this.OFFSET ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "OR":
				switch (parameterTypes) {
				case "OR ByteMemory Byte":
					this.OR (GetByteMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "OR ByteMemory R8Type":
					this.OR (GetByteMemory (operands [0]), R8.GetByID (operands [1]));
					break;

				case "OR DWordMemory Byte":
					this.OR (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "OR DWordMemory R32Type":
					this.OR (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "OR DWordMemory UInt32":
					this.OR (GetDWordMemory (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "OR R16Type Byte":
					this.OR (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "OR R16Type R16Type":
					this.OR (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "OR R16Type UInt16":
					this.OR (R16.GetByID (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "OR R16Type WordMemory":
					this.OR (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "OR R32Type Byte":
					this.OR (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "OR R32Type DWordMemory":
					this.OR (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "OR R32Type R32Type":
					this.OR (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "OR R32Type UInt32":
					this.OR (R32.GetByID (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "OR R8Type Byte":
					this.OR (R8.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "OR R8Type ByteMemory":
					this.OR (R8.GetByID (operands [0]), GetByteMemory (operands [1]));
					break;

				case "OR R8Type R8Type":
					this.OR (R8.GetByID (operands [0]), R8.GetByID (operands [1]));
					break;

				case "OR WordMemory Byte":
					this.OR (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "OR WordMemory R16Type":
					this.OR (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				case "OR WordMemory UInt16":
					this.OR (GetWordMemory (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "ORG":
				switch (parameterTypes) {
				case "ORG UInt32":
					this.ORG ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "OUTSB":
				switch (parameterTypes) {
				case "OUTSB":
					this.OUTSB ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "OUTSD":
				switch (parameterTypes) {
				case "OUTSD":
					this.OUTSD ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "OUTSW":
				switch (parameterTypes) {
				case "OUTSW":
					this.OUTSW ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "OUT_DX__AL":
				switch (parameterTypes) {
				case "OUT_DX__AL":
					this.OUT_DX__AL ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "OUT_DX__AX":
				switch (parameterTypes) {
				case "OUT_DX__AX":
					this.OUT_DX__AX ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "OUT_DX__EAX":
				switch (parameterTypes) {
				case "OUT_DX__EAX":
					this.OUT_DX__EAX ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "OUT__AL":
				switch (parameterTypes) {
				case "OUT__AL Byte":
					this.OUT__AL ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "OUT__AX":
				switch (parameterTypes) {
				case "OUT__AX Byte":
					this.OUT__AX ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "OUT__EAX":
				switch (parameterTypes) {
				case "OUT__EAX Byte":
					this.OUT__EAX ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "PAUSE":
				switch (parameterTypes) {
				case "PAUSE":
					this.PAUSE ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "POP":
				switch (parameterTypes) {
				case "POP DWordMemory":
					this.POP (GetDWordMemory (operands [0]));
					break;

				case "POP R16Type":
					this.POP (R16.GetByID (operands [0]));
					break;

				case "POP R32Type":
					this.POP (R32.GetByID (operands [0]));
					break;

				case "POP SegType":
					this.POP (Seg.GetByID (operands [0]));
					break;

				case "POP WordMemory":
					this.POP (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "POPA":
				switch (parameterTypes) {
				case "POPA":
					this.POPA ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "POPAD":
				switch (parameterTypes) {
				case "POPAD":
					this.POPAD ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "POPAW":
				switch (parameterTypes) {
				case "POPAW":
					this.POPAW ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "POPF":
				switch (parameterTypes) {
				case "POPF":
					this.POPF ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "POPFD":
				switch (parameterTypes) {
				case "POPFD":
					this.POPFD ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "POPFW":
				switch (parameterTypes) {
				case "POPFW":
					this.POPFW ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "PREFETCHNTA":
				switch (parameterTypes) {
				case "PREFETCHNTA Memory":
					this.PREFETCHNTA (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "PREFETCHT0":
				switch (parameterTypes) {
				case "PREFETCHT0 Memory":
					this.PREFETCHT0 (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "PREFETCHT1":
				switch (parameterTypes) {
				case "PREFETCHT1 Memory":
					this.PREFETCHT1 (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "PREFETCHT2":
				switch (parameterTypes) {
				case "PREFETCHT2 Memory":
					this.PREFETCHT2 (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "PUSH":
				switch (parameterTypes) {
				case "PUSH Byte":
					this.PUSH ((byte) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "PUSH DWordMemory":
					this.PUSH (GetDWordMemory (operands [0]));
					break;

				case "PUSH R16Type":
					this.PUSH (R16.GetByID (operands [0]));
					break;

				case "PUSH R32Type":
					this.PUSH (R32.GetByID (operands [0]));
					break;

				case "PUSH SegType":
					this.PUSH (Seg.GetByID (operands [0]));
					break;

				case "PUSH UInt16":
					this.PUSH ((UInt16) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "PUSH UInt32":
					this.PUSH ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "PUSH WordMemory":
					this.PUSH (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "PUSHA":
				switch (parameterTypes) {
				case "PUSHA":
					this.PUSHA ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "PUSHAD":
				switch (parameterTypes) {
				case "PUSHAD":
					this.PUSHAD ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "PUSHAW":
				switch (parameterTypes) {
				case "PUSHAW":
					this.PUSHAW ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "PUSHF":
				switch (parameterTypes) {
				case "PUSHF":
					this.PUSHF ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "PUSHFD":
				switch (parameterTypes) {
				case "PUSHFD":
					this.PUSHFD ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "PUSHFW":
				switch (parameterTypes) {
				case "PUSHFW":
					this.PUSHFW ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "RCL":
				switch (parameterTypes) {
				case "RCL ByteMemory Byte":
					this.RCL (GetByteMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "RCL DWordMemory Byte":
					this.RCL (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "RCL R16Type Byte":
					this.RCL (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "RCL R32Type Byte":
					this.RCL (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "RCL R8Type Byte":
					this.RCL (R8.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "RCL WordMemory Byte":
					this.RCL (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "RCL__CL":
				switch (parameterTypes) {
				case "RCL__CL ByteMemory":
					this.RCL__CL (GetByteMemory (operands [0]));
					break;

				case "RCL__CL DWordMemory":
					this.RCL__CL (GetDWordMemory (operands [0]));
					break;

				case "RCL__CL R16Type":
					this.RCL__CL (R16.GetByID (operands [0]));
					break;

				case "RCL__CL R32Type":
					this.RCL__CL (R32.GetByID (operands [0]));
					break;

				case "RCL__CL R8Type":
					this.RCL__CL (R8.GetByID (operands [0]));
					break;

				case "RCL__CL WordMemory":
					this.RCL__CL (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "RCR":
				switch (parameterTypes) {
				case "RCR ByteMemory Byte":
					this.RCR (GetByteMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "RCR DWordMemory Byte":
					this.RCR (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "RCR R16Type Byte":
					this.RCR (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "RCR R32Type Byte":
					this.RCR (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "RCR R8Type Byte":
					this.RCR (R8.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "RCR WordMemory Byte":
					this.RCR (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "RCR__CL":
				switch (parameterTypes) {
				case "RCR__CL ByteMemory":
					this.RCR__CL (GetByteMemory (operands [0]));
					break;

				case "RCR__CL DWordMemory":
					this.RCR__CL (GetDWordMemory (operands [0]));
					break;

				case "RCR__CL R16Type":
					this.RCR__CL (R16.GetByID (operands [0]));
					break;

				case "RCR__CL R32Type":
					this.RCR__CL (R32.GetByID (operands [0]));
					break;

				case "RCR__CL R8Type":
					this.RCR__CL (R8.GetByID (operands [0]));
					break;

				case "RCR__CL WordMemory":
					this.RCR__CL (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "RDMSR":
				switch (parameterTypes) {
				case "RDMSR":
					this.RDMSR ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "RDPMC":
				switch (parameterTypes) {
				case "RDPMC":
					this.RDPMC ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "RDTSC":
				switch (parameterTypes) {
				case "RDTSC":
					this.RDTSC ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "REP":
				switch (parameterTypes) {
				case "REP":
					this.REP ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "REPE":
				switch (parameterTypes) {
				case "REPE":
					this.REPE ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "REPNE":
				switch (parameterTypes) {
				case "REPNE":
					this.REPNE ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "REPNZ":
				switch (parameterTypes) {
				case "REPNZ":
					this.REPNZ ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "REPZ":
				switch (parameterTypes) {
				case "REPZ":
					this.REPZ ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "RET":
				switch (parameterTypes) {
				case "RET":
					this.RET ();
					break;

				case "RET UInt16":
					this.RET ((UInt16) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "RETF":
				switch (parameterTypes) {
				case "RETF":
					this.RETF ();
					break;

				case "RETF UInt16":
					this.RETF ((UInt16) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "RETN":
				switch (parameterTypes) {
				case "RETN":
					this.RETN ();
					break;

				case "RETN UInt16":
					this.RETN ((UInt16) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "ROL":
				switch (parameterTypes) {
				case "ROL ByteMemory Byte":
					this.ROL (GetByteMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ROL DWordMemory Byte":
					this.ROL (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ROL R16Type Byte":
					this.ROL (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ROL R32Type Byte":
					this.ROL (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ROL R8Type Byte":
					this.ROL (R8.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ROL WordMemory Byte":
					this.ROL (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "ROL__CL":
				switch (parameterTypes) {
				case "ROL__CL ByteMemory":
					this.ROL__CL (GetByteMemory (operands [0]));
					break;

				case "ROL__CL DWordMemory":
					this.ROL__CL (GetDWordMemory (operands [0]));
					break;

				case "ROL__CL R16Type":
					this.ROL__CL (R16.GetByID (operands [0]));
					break;

				case "ROL__CL R32Type":
					this.ROL__CL (R32.GetByID (operands [0]));
					break;

				case "ROL__CL R8Type":
					this.ROL__CL (R8.GetByID (operands [0]));
					break;

				case "ROL__CL WordMemory":
					this.ROL__CL (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "ROR":
				switch (parameterTypes) {
				case "ROR ByteMemory Byte":
					this.ROR (GetByteMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ROR DWordMemory Byte":
					this.ROR (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ROR R16Type Byte":
					this.ROR (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ROR R32Type Byte":
					this.ROR (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ROR R8Type Byte":
					this.ROR (R8.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "ROR WordMemory Byte":
					this.ROR (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "ROR__CL":
				switch (parameterTypes) {
				case "ROR__CL ByteMemory":
					this.ROR__CL (GetByteMemory (operands [0]));
					break;

				case "ROR__CL DWordMemory":
					this.ROR__CL (GetDWordMemory (operands [0]));
					break;

				case "ROR__CL R16Type":
					this.ROR__CL (R16.GetByID (operands [0]));
					break;

				case "ROR__CL R32Type":
					this.ROR__CL (R32.GetByID (operands [0]));
					break;

				case "ROR__CL R8Type":
					this.ROR__CL (R8.GetByID (operands [0]));
					break;

				case "ROR__CL WordMemory":
					this.ROR__CL (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "RSM":
				switch (parameterTypes) {
				case "RSM":
					this.RSM ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SAHF":
				switch (parameterTypes) {
				case "SAHF":
					this.SAHF ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SAL":
				switch (parameterTypes) {
				case "SAL ByteMemory Byte":
					this.SAL (GetByteMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SAL DWordMemory Byte":
					this.SAL (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SAL R16Type Byte":
					this.SAL (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SAL R32Type Byte":
					this.SAL (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SAL R8Type Byte":
					this.SAL (R8.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SAL WordMemory Byte":
					this.SAL (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SALC":
				switch (parameterTypes) {
				case "SALC":
					this.SALC ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SAL__CL":
				switch (parameterTypes) {
				case "SAL__CL ByteMemory":
					this.SAL__CL (GetByteMemory (operands [0]));
					break;

				case "SAL__CL DWordMemory":
					this.SAL__CL (GetDWordMemory (operands [0]));
					break;

				case "SAL__CL R16Type":
					this.SAL__CL (R16.GetByID (operands [0]));
					break;

				case "SAL__CL R32Type":
					this.SAL__CL (R32.GetByID (operands [0]));
					break;

				case "SAL__CL R8Type":
					this.SAL__CL (R8.GetByID (operands [0]));
					break;

				case "SAL__CL WordMemory":
					this.SAL__CL (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SAR":
				switch (parameterTypes) {
				case "SAR ByteMemory Byte":
					this.SAR (GetByteMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SAR DWordMemory Byte":
					this.SAR (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SAR R16Type Byte":
					this.SAR (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SAR R32Type Byte":
					this.SAR (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SAR R8Type Byte":
					this.SAR (R8.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SAR WordMemory Byte":
					this.SAR (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SAR__CL":
				switch (parameterTypes) {
				case "SAR__CL ByteMemory":
					this.SAR__CL (GetByteMemory (operands [0]));
					break;

				case "SAR__CL DWordMemory":
					this.SAR__CL (GetDWordMemory (operands [0]));
					break;

				case "SAR__CL R16Type":
					this.SAR__CL (R16.GetByID (operands [0]));
					break;

				case "SAR__CL R32Type":
					this.SAR__CL (R32.GetByID (operands [0]));
					break;

				case "SAR__CL R8Type":
					this.SAR__CL (R8.GetByID (operands [0]));
					break;

				case "SAR__CL WordMemory":
					this.SAR__CL (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SBB":
				switch (parameterTypes) {
				case "SBB ByteMemory Byte":
					this.SBB (GetByteMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SBB ByteMemory R8Type":
					this.SBB (GetByteMemory (operands [0]), R8.GetByID (operands [1]));
					break;

				case "SBB DWordMemory Byte":
					this.SBB (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SBB DWordMemory R32Type":
					this.SBB (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "SBB DWordMemory UInt32":
					this.SBB (GetDWordMemory (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SBB R16Type Byte":
					this.SBB (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SBB R16Type R16Type":
					this.SBB (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "SBB R16Type UInt16":
					this.SBB (R16.GetByID (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SBB R16Type WordMemory":
					this.SBB (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "SBB R32Type Byte":
					this.SBB (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SBB R32Type DWordMemory":
					this.SBB (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "SBB R32Type R32Type":
					this.SBB (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "SBB R32Type UInt32":
					this.SBB (R32.GetByID (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SBB R8Type Byte":
					this.SBB (R8.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SBB R8Type ByteMemory":
					this.SBB (R8.GetByID (operands [0]), GetByteMemory (operands [1]));
					break;

				case "SBB R8Type R8Type":
					this.SBB (R8.GetByID (operands [0]), R8.GetByID (operands [1]));
					break;

				case "SBB WordMemory Byte":
					this.SBB (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SBB WordMemory R16Type":
					this.SBB (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				case "SBB WordMemory UInt16":
					this.SBB (GetWordMemory (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SCASB":
				switch (parameterTypes) {
				case "SCASB":
					this.SCASB ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SCASD":
				switch (parameterTypes) {
				case "SCASD":
					this.SCASD ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SCASW":
				switch (parameterTypes) {
				case "SCASW":
					this.SCASW ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETA":
				switch (parameterTypes) {
				case "SETA ByteMemory":
					this.SETA (GetByteMemory (operands [0]));
					break;

				case "SETA R8Type":
					this.SETA (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETAE":
				switch (parameterTypes) {
				case "SETAE ByteMemory":
					this.SETAE (GetByteMemory (operands [0]));
					break;

				case "SETAE R8Type":
					this.SETAE (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETB":
				switch (parameterTypes) {
				case "SETB ByteMemory":
					this.SETB (GetByteMemory (operands [0]));
					break;

				case "SETB R8Type":
					this.SETB (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETBE":
				switch (parameterTypes) {
				case "SETBE ByteMemory":
					this.SETBE (GetByteMemory (operands [0]));
					break;

				case "SETBE R8Type":
					this.SETBE (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETC":
				switch (parameterTypes) {
				case "SETC ByteMemory":
					this.SETC (GetByteMemory (operands [0]));
					break;

				case "SETC R8Type":
					this.SETC (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETE":
				switch (parameterTypes) {
				case "SETE ByteMemory":
					this.SETE (GetByteMemory (operands [0]));
					break;

				case "SETE R8Type":
					this.SETE (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETG":
				switch (parameterTypes) {
				case "SETG ByteMemory":
					this.SETG (GetByteMemory (operands [0]));
					break;

				case "SETG R8Type":
					this.SETG (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETGE":
				switch (parameterTypes) {
				case "SETGE ByteMemory":
					this.SETGE (GetByteMemory (operands [0]));
					break;

				case "SETGE R8Type":
					this.SETGE (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETL":
				switch (parameterTypes) {
				case "SETL ByteMemory":
					this.SETL (GetByteMemory (operands [0]));
					break;

				case "SETL R8Type":
					this.SETL (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETLE":
				switch (parameterTypes) {
				case "SETLE ByteMemory":
					this.SETLE (GetByteMemory (operands [0]));
					break;

				case "SETLE R8Type":
					this.SETLE (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETNA":
				switch (parameterTypes) {
				case "SETNA ByteMemory":
					this.SETNA (GetByteMemory (operands [0]));
					break;

				case "SETNA R8Type":
					this.SETNA (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETNAE":
				switch (parameterTypes) {
				case "SETNAE ByteMemory":
					this.SETNAE (GetByteMemory (operands [0]));
					break;

				case "SETNAE R8Type":
					this.SETNAE (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETNB":
				switch (parameterTypes) {
				case "SETNB ByteMemory":
					this.SETNB (GetByteMemory (operands [0]));
					break;

				case "SETNB R8Type":
					this.SETNB (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETNBE":
				switch (parameterTypes) {
				case "SETNBE ByteMemory":
					this.SETNBE (GetByteMemory (operands [0]));
					break;

				case "SETNBE R8Type":
					this.SETNBE (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETNC":
				switch (parameterTypes) {
				case "SETNC ByteMemory":
					this.SETNC (GetByteMemory (operands [0]));
					break;

				case "SETNC R8Type":
					this.SETNC (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETNE":
				switch (parameterTypes) {
				case "SETNE ByteMemory":
					this.SETNE (GetByteMemory (operands [0]));
					break;

				case "SETNE R8Type":
					this.SETNE (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETNG":
				switch (parameterTypes) {
				case "SETNG ByteMemory":
					this.SETNG (GetByteMemory (operands [0]));
					break;

				case "SETNG R8Type":
					this.SETNG (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETNGE":
				switch (parameterTypes) {
				case "SETNGE ByteMemory":
					this.SETNGE (GetByteMemory (operands [0]));
					break;

				case "SETNGE R8Type":
					this.SETNGE (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETNL":
				switch (parameterTypes) {
				case "SETNL ByteMemory":
					this.SETNL (GetByteMemory (operands [0]));
					break;

				case "SETNL R8Type":
					this.SETNL (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETNLE":
				switch (parameterTypes) {
				case "SETNLE ByteMemory":
					this.SETNLE (GetByteMemory (operands [0]));
					break;

				case "SETNLE R8Type":
					this.SETNLE (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETNO":
				switch (parameterTypes) {
				case "SETNO ByteMemory":
					this.SETNO (GetByteMemory (operands [0]));
					break;

				case "SETNO R8Type":
					this.SETNO (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETNP":
				switch (parameterTypes) {
				case "SETNP ByteMemory":
					this.SETNP (GetByteMemory (operands [0]));
					break;

				case "SETNP R8Type":
					this.SETNP (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETNS":
				switch (parameterTypes) {
				case "SETNS ByteMemory":
					this.SETNS (GetByteMemory (operands [0]));
					break;

				case "SETNS R8Type":
					this.SETNS (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETNZ":
				switch (parameterTypes) {
				case "SETNZ ByteMemory":
					this.SETNZ (GetByteMemory (operands [0]));
					break;

				case "SETNZ R8Type":
					this.SETNZ (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETO":
				switch (parameterTypes) {
				case "SETO ByteMemory":
					this.SETO (GetByteMemory (operands [0]));
					break;

				case "SETO R8Type":
					this.SETO (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETP":
				switch (parameterTypes) {
				case "SETP ByteMemory":
					this.SETP (GetByteMemory (operands [0]));
					break;

				case "SETP R8Type":
					this.SETP (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETPE":
				switch (parameterTypes) {
				case "SETPE ByteMemory":
					this.SETPE (GetByteMemory (operands [0]));
					break;

				case "SETPE R8Type":
					this.SETPE (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETPO":
				switch (parameterTypes) {
				case "SETPO ByteMemory":
					this.SETPO (GetByteMemory (operands [0]));
					break;

				case "SETPO R8Type":
					this.SETPO (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETS":
				switch (parameterTypes) {
				case "SETS ByteMemory":
					this.SETS (GetByteMemory (operands [0]));
					break;

				case "SETS R8Type":
					this.SETS (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SETZ":
				switch (parameterTypes) {
				case "SETZ ByteMemory":
					this.SETZ (GetByteMemory (operands [0]));
					break;

				case "SETZ R8Type":
					this.SETZ (R8.GetByID (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SFENCE":
				switch (parameterTypes) {
				case "SFENCE":
					this.SFENCE ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SGDT":
				switch (parameterTypes) {
				case "SGDT Memory":
					this.SGDT (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SHL":
				switch (parameterTypes) {
				case "SHL ByteMemory Byte":
					this.SHL (GetByteMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SHL DWordMemory Byte":
					this.SHL (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SHL R16Type Byte":
					this.SHL (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SHL R32Type Byte":
					this.SHL (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SHL R8Type Byte":
					this.SHL (R8.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SHL WordMemory Byte":
					this.SHL (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SHLD":
				switch (parameterTypes) {
				case "SHLD DWordMemory R32Type Byte":
					this.SHLD (GetDWordMemory (operands [0]), R32.GetByID (operands [1]), (byte) (operands [2] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SHLD R16Type R16Type Byte":
					this.SHLD (R16.GetByID (operands [0]), R16.GetByID (operands [1]), (byte) (operands [2] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SHLD R32Type R32Type Byte":
					this.SHLD (R32.GetByID (operands [0]), R32.GetByID (operands [1]), (byte) (operands [2] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SHLD WordMemory R16Type Byte":
					this.SHLD (GetWordMemory (operands [0]), R16.GetByID (operands [1]), (byte) (operands [2] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SHLD___CL":
				switch (parameterTypes) {
				case "SHLD___CL DWordMemory R32Type":
					this.SHLD___CL (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "SHLD___CL R16Type R16Type":
					this.SHLD___CL (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "SHLD___CL R32Type R32Type":
					this.SHLD___CL (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "SHLD___CL WordMemory R16Type":
					this.SHLD___CL (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SHL__CL":
				switch (parameterTypes) {
				case "SHL__CL ByteMemory":
					this.SHL__CL (GetByteMemory (operands [0]));
					break;

				case "SHL__CL DWordMemory":
					this.SHL__CL (GetDWordMemory (operands [0]));
					break;

				case "SHL__CL R16Type":
					this.SHL__CL (R16.GetByID (operands [0]));
					break;

				case "SHL__CL R32Type":
					this.SHL__CL (R32.GetByID (operands [0]));
					break;

				case "SHL__CL R8Type":
					this.SHL__CL (R8.GetByID (operands [0]));
					break;

				case "SHL__CL WordMemory":
					this.SHL__CL (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SHR":
				switch (parameterTypes) {
				case "SHR ByteMemory Byte":
					this.SHR (GetByteMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SHR DWordMemory Byte":
					this.SHR (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SHR R16Type Byte":
					this.SHR (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SHR R32Type Byte":
					this.SHR (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SHR R8Type Byte":
					this.SHR (R8.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SHR WordMemory Byte":
					this.SHR (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SHRD":
				switch (parameterTypes) {
				case "SHRD DWordMemory R32Type Byte":
					this.SHRD (GetDWordMemory (operands [0]), R32.GetByID (operands [1]), (byte) (operands [2] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SHRD R16Type R16Type Byte":
					this.SHRD (R16.GetByID (operands [0]), R16.GetByID (operands [1]), (byte) (operands [2] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SHRD R32Type R32Type Byte":
					this.SHRD (R32.GetByID (operands [0]), R32.GetByID (operands [1]), (byte) (operands [2] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SHRD WordMemory R16Type Byte":
					this.SHRD (GetWordMemory (operands [0]), R16.GetByID (operands [1]), (byte) (operands [2] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SHRD___CL":
				switch (parameterTypes) {
				case "SHRD___CL DWordMemory R32Type":
					this.SHRD___CL (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "SHRD___CL R16Type R16Type":
					this.SHRD___CL (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "SHRD___CL R32Type R32Type":
					this.SHRD___CL (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "SHRD___CL WordMemory R16Type":
					this.SHRD___CL (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SHR__CL":
				switch (parameterTypes) {
				case "SHR__CL ByteMemory":
					this.SHR__CL (GetByteMemory (operands [0]));
					break;

				case "SHR__CL DWordMemory":
					this.SHR__CL (GetDWordMemory (operands [0]));
					break;

				case "SHR__CL R16Type":
					this.SHR__CL (R16.GetByID (operands [0]));
					break;

				case "SHR__CL R32Type":
					this.SHR__CL (R32.GetByID (operands [0]));
					break;

				case "SHR__CL R8Type":
					this.SHR__CL (R8.GetByID (operands [0]));
					break;

				case "SHR__CL WordMemory":
					this.SHR__CL (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SIDT":
				switch (parameterTypes) {
				case "SIDT Memory":
					this.SIDT (GetMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SLDT":
				switch (parameterTypes) {
				case "SLDT R16Type":
					this.SLDT (R16.GetByID (operands [0]));
					break;

				case "SLDT WordMemory":
					this.SLDT (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SMSW":
				switch (parameterTypes) {
				case "SMSW R16Type":
					this.SMSW (R16.GetByID (operands [0]));
					break;

				case "SMSW WordMemory":
					this.SMSW (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "STC":
				switch (parameterTypes) {
				case "STC":
					this.STC ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "STD":
				switch (parameterTypes) {
				case "STD":
					this.STD ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "STI":
				switch (parameterTypes) {
				case "STI":
					this.STI ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "STOSB":
				switch (parameterTypes) {
				case "STOSB":
					this.STOSB ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "STOSD":
				switch (parameterTypes) {
				case "STOSD":
					this.STOSD ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "STOSW":
				switch (parameterTypes) {
				case "STOSW":
					this.STOSW ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "STR":
				switch (parameterTypes) {
				case "STR R16Type":
					this.STR (R16.GetByID (operands [0]));
					break;

				case "STR WordMemory":
					this.STR (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SUB":
				switch (parameterTypes) {
				case "SUB ByteMemory Byte":
					this.SUB (GetByteMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SUB ByteMemory R8Type":
					this.SUB (GetByteMemory (operands [0]), R8.GetByID (operands [1]));
					break;

				case "SUB DWordMemory Byte":
					this.SUB (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SUB DWordMemory R32Type":
					this.SUB (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "SUB DWordMemory UInt32":
					this.SUB (GetDWordMemory (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SUB R16Type Byte":
					this.SUB (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SUB R16Type R16Type":
					this.SUB (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "SUB R16Type UInt16":
					this.SUB (R16.GetByID (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SUB R16Type WordMemory":
					this.SUB (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "SUB R32Type Byte":
					this.SUB (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SUB R32Type DWordMemory":
					this.SUB (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "SUB R32Type R32Type":
					this.SUB (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "SUB R32Type UInt32":
					this.SUB (R32.GetByID (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SUB R8Type Byte":
					this.SUB (R8.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SUB R8Type ByteMemory":
					this.SUB (R8.GetByID (operands [0]), GetByteMemory (operands [1]));
					break;

				case "SUB R8Type R8Type":
					this.SUB (R8.GetByID (operands [0]), R8.GetByID (operands [1]));
					break;

				case "SUB WordMemory Byte":
					this.SUB (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "SUB WordMemory R16Type":
					this.SUB (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				case "SUB WordMemory UInt16":
					this.SUB (GetWordMemory (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SYSCALL":
				switch (parameterTypes) {
				case "SYSCALL":
					this.SYSCALL ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SYSENTER":
				switch (parameterTypes) {
				case "SYSENTER":
					this.SYSENTER ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SYSEXIT":
				switch (parameterTypes) {
				case "SYSEXIT":
					this.SYSEXIT ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "SYSRET":
				switch (parameterTypes) {
				case "SYSRET":
					this.SYSRET ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "TEST":
				switch (parameterTypes) {
				case "TEST ByteMemory Byte":
					this.TEST (GetByteMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "TEST ByteMemory R8Type":
					this.TEST (GetByteMemory (operands [0]), R8.GetByID (operands [1]));
					break;

				case "TEST DWordMemory R32Type":
					this.TEST (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "TEST DWordMemory UInt32":
					this.TEST (GetDWordMemory (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "TEST R16Type R16Type":
					this.TEST (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "TEST R16Type UInt16":
					this.TEST (R16.GetByID (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "TEST R32Type R32Type":
					this.TEST (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "TEST R32Type UInt32":
					this.TEST (R32.GetByID (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "TEST R8Type Byte":
					this.TEST (R8.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "TEST R8Type R8Type":
					this.TEST (R8.GetByID (operands [0]), R8.GetByID (operands [1]));
					break;

				case "TEST WordMemory R16Type":
					this.TEST (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				case "TEST WordMemory UInt16":
					this.TEST (GetWordMemory (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "TIMES":
				switch (parameterTypes) {
				case "TIMES UInt32 Byte":
					this.TIMES ((UInt32) (operands [0] as SharpOS.AOT.IR.Operands.IntConstant).Value, (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "UTF16":
				switch (parameterTypes) {
				case "UTF16 String":
					this.UTF16 ((operands [0] as SharpOS.AOT.IR.Operands.StringConstant).Value.ToString ());
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "VERR":
				switch (parameterTypes) {
				case "VERR R16Type":
					this.VERR (R16.GetByID (operands [0]));
					break;

				case "VERR WordMemory":
					this.VERR (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "VERW":
				switch (parameterTypes) {
				case "VERW R16Type":
					this.VERW (R16.GetByID (operands [0]));
					break;

				case "VERW WordMemory":
					this.VERW (GetWordMemory (operands [0]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "WAIT":
				switch (parameterTypes) {
				case "WAIT":
					this.WAIT ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "WBINVD":
				switch (parameterTypes) {
				case "WBINVD":
					this.WBINVD ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "WRMSR":
				switch (parameterTypes) {
				case "WRMSR":
					this.WRMSR ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "XADD":
				switch (parameterTypes) {
				case "XADD ByteMemory R8Type":
					this.XADD (GetByteMemory (operands [0]), R8.GetByID (operands [1]));
					break;

				case "XADD DWordMemory R32Type":
					this.XADD (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "XADD R16Type R16Type":
					this.XADD (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "XADD R32Type R32Type":
					this.XADD (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "XADD R8Type R8Type":
					this.XADD (R8.GetByID (operands [0]), R8.GetByID (operands [1]));
					break;

				case "XADD WordMemory R16Type":
					this.XADD (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "XCHG":
				switch (parameterTypes) {
				case "XCHG ByteMemory R8Type":
					this.XCHG (GetByteMemory (operands [0]), R8.GetByID (operands [1]));
					break;

				case "XCHG DWordMemory R32Type":
					this.XCHG (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "XCHG R16Type R16Type":
					this.XCHG (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "XCHG R16Type WordMemory":
					this.XCHG (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "XCHG R32Type DWordMemory":
					this.XCHG (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "XCHG R32Type R32Type":
					this.XCHG (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "XCHG R8Type ByteMemory":
					this.XCHG (R8.GetByID (operands [0]), GetByteMemory (operands [1]));
					break;

				case "XCHG R8Type R8Type":
					this.XCHG (R8.GetByID (operands [0]), R8.GetByID (operands [1]));
					break;

				case "XCHG WordMemory R16Type":
					this.XCHG (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "XLAT":
				switch (parameterTypes) {
				case "XLAT":
					this.XLAT ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "XLATB":
				switch (parameterTypes) {
				case "XLATB":
					this.XLATB ();
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			case "XOR":
				switch (parameterTypes) {
				case "XOR ByteMemory Byte":
					this.XOR (GetByteMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "XOR ByteMemory R8Type":
					this.XOR (GetByteMemory (operands [0]), R8.GetByID (operands [1]));
					break;

				case "XOR DWordMemory Byte":
					this.XOR (GetDWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "XOR DWordMemory R32Type":
					this.XOR (GetDWordMemory (operands [0]), R32.GetByID (operands [1]));
					break;

				case "XOR DWordMemory UInt32":
					this.XOR (GetDWordMemory (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "XOR R16Type Byte":
					this.XOR (R16.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "XOR R16Type R16Type":
					this.XOR (R16.GetByID (operands [0]), R16.GetByID (operands [1]));
					break;

				case "XOR R16Type UInt16":
					this.XOR (R16.GetByID (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "XOR R16Type WordMemory":
					this.XOR (R16.GetByID (operands [0]), GetWordMemory (operands [1]));
					break;

				case "XOR R32Type Byte":
					this.XOR (R32.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "XOR R32Type DWordMemory":
					this.XOR (R32.GetByID (operands [0]), GetDWordMemory (operands [1]));
					break;

				case "XOR R32Type R32Type":
					this.XOR (R32.GetByID (operands [0]), R32.GetByID (operands [1]));
					break;

				case "XOR R32Type UInt32":
					this.XOR (R32.GetByID (operands [0]), (UInt32) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "XOR R8Type Byte":
					this.XOR (R8.GetByID (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "XOR R8Type ByteMemory":
					this.XOR (R8.GetByID (operands [0]), GetByteMemory (operands [1]));
					break;

				case "XOR R8Type R8Type":
					this.XOR (R8.GetByID (operands [0]), R8.GetByID (operands [1]));
					break;

				case "XOR WordMemory Byte":
					this.XOR (GetWordMemory (operands [0]), (byte) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				case "XOR WordMemory R16Type":
					this.XOR (GetWordMemory (operands [0]), R16.GetByID (operands [1]));
					break;

				case "XOR WordMemory UInt16":
					this.XOR (GetWordMemory (operands [0]), (UInt16) (operands [1] as SharpOS.AOT.IR.Operands.IntConstant).Value);
					break;

				default:
					throw new EngineException ("'" + method.Method.Name + "(" + parameterTypes + ")' is not supported.");
				}
				break;

			default:
				throw new EngineException ("'" + method.Method.Name + "' is not supported.");
			}
		}
	}
}
