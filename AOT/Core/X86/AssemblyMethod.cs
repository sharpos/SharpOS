// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Bruce Markham <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using SharpOS.AOT.IR;
using SharpOS.AOT.IR.Operands;
using SharpOS.AOT.IR.Instructions;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace SharpOS.AOT.X86 {
	internal partial class AssemblyMethod {
		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyMethod"/> class.
		/// </summary>
		/// <param name="assembly">The assembly.</param>
		/// <param name="method">The method.</param>
		public AssemblyMethod (Assembly assembly, Method method)
		{
			this.assembly = assembly;
			this.method = method;
		}

		protected Method method = null;
		protected Assembly assembly = null;

		/// <summary>
		/// Gets the assembly code.
		/// </summary>
		/// <returns></returns>
		public bool GetAssemblyCode ()
		{
			if (this.method.SkipProcessing)
				return true;

			string fullname = method.MethodFullName;

			assembly.ALIGN (Assembly.ALIGNMENT);

			foreach (CustomAttribute attribute in method.MethodDefinition.CustomAttributes) {
				if (attribute.Constructor.DeclaringType.FullName.Equals (typeof (SharpOS.AOT.Attributes.LabelAttribute).ToString ())) {
					assembly.LABEL (attribute.ConstructorParameters [0].ToString ());

					this.assembly.AddSymbol (new COFF.Label (attribute.ConstructorParameters [0].ToString ()));
				}
			}

			if (this.method.EntryPoint) {
				assembly.LABEL (Assembly.KERNEL_MAIN);

				this.assembly.AddSymbol (new COFF.Label (Assembly.KERNEL_MAIN));
			}

			this.assembly.AddSymbol (new COFF.Function (fullname));

			assembly.LABEL (fullname);
			assembly.PUSH (R32.EBP);
			assembly.MOV (R32.EBP, R32.ESP);
			assembly.PUSH (R32.EBX);
			assembly.PUSH (R32.ESI);
			assembly.PUSH (R32.EDI);

			if (method.StackSize > 0)
				assembly.SUB (R32.ESP, (UInt32) (method.StackSize * 4));

			foreach (Block block in method) {
				assembly.LABEL (fullname + " " + block.Index.ToString ());

				foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in block) {
					assembly.COMMENT (instruction.ToString ());

					if (instruction.Ignore)
						continue;

					if (instruction is IR.Instructions.Call)
						this.Call (instruction as SharpOS.AOT.IR.Instructions.Call);

					else if (instruction is IR.Instructions.Return)
						this.Return (instruction as IR.Instructions.Return);

					else if (instruction is IR.Instructions.Initialize)
						this.Initialize (instruction as IR.Instructions.Initialize);

					else if (instruction is IR.Instructions.Ldc)
						this.Ldc (instruction as IR.Instructions.Ldc);

					else if (instruction is IR.Instructions.Ldind)
						this.Ldind (instruction as IR.Instructions.Ldind);

					else if (instruction is IR.Instructions.Ldloc)
						this.Ldloc (instruction as IR.Instructions.Ldloc);

					else if (instruction is IR.Instructions.Ldloca)
						this.Ldloca (instruction as IR.Instructions.Ldloca);

					else if (instruction is IR.Instructions.Ldarg)
						this.Ldarg (instruction as IR.Instructions.Ldarg);

					else if (instruction is IR.Instructions.Ldarga)
						this.Ldarga (instruction as IR.Instructions.Ldarga);

					else if (instruction is IR.Instructions.Ldfld)
						this.Ldfld (instruction as IR.Instructions.Ldfld);

					else if (instruction is IR.Instructions.Ldsfld)
						this.Ldsfld (instruction as IR.Instructions.Ldsfld);

					else if (instruction is IR.Instructions.Ldflda)
						this.Ldflda (instruction as IR.Instructions.Ldflda);

					else if (instruction is IR.Instructions.Localloc)
						this.Localloc (instruction as IR.Instructions.Localloc);

					else if (instruction is IR.Instructions.Ldstr)
						this.Ldstr (instruction as IR.Instructions.Ldstr);

					else if (instruction is IR.Instructions.Ldnull)
						this.Ldnull (instruction as IR.Instructions.Ldnull);

					else if (instruction is IR.Instructions.Stloc)
						this.Stloc (instruction as IR.Instructions.Stloc);

					else if (instruction is IR.Instructions.Starg)
						this.Starg (instruction as IR.Instructions.Starg);

					else if (instruction is IR.Instructions.Stind)
						this.Stind (instruction as IR.Instructions.Stind);

					else if (instruction is IR.Instructions.Stfld)
						this.Stfld (instruction as IR.Instructions.Stfld);

					else if (instruction is IR.Instructions.Stsfld)
						this.Stsfld (instruction as IR.Instructions.Stsfld);

					else if (instruction is IR.Instructions.Convert)
						this.Convert (instruction as IR.Instructions.Convert);

					else if (instruction is IR.Instructions.Dup)
						this.Dup (instruction as IR.Instructions.Dup);

					else if (instruction is IR.Instructions.Jump)
						this.Jump (instruction as IR.Instructions.Jump);

					else if (instruction is IR.Instructions.Branch)
						this.Branch (instruction as IR.Instructions.Branch);

					else if (instruction is IR.Instructions.SimpleBranch)
						this.SimpleBranch (instruction as IR.Instructions.SimpleBranch);

					else if (instruction is IR.Instructions.ConditionCheck)
						this.ConditionCheck (instruction as IR.Instructions.ConditionCheck);

					else if (instruction is IR.Instructions.Add)
						this.Add (instruction as IR.Instructions.Add);

					else if (instruction is IR.Instructions.Sub)
						this.Sub (instruction as IR.Instructions.Sub);

					else if (instruction is IR.Instructions.Mul)
						this.Mul (instruction as IR.Instructions.Mul);

					else if (instruction is IR.Instructions.Div)
						this.Div (instruction as IR.Instructions.Div);

					else if (instruction is IR.Instructions.Rem)
						this.Rem (instruction as IR.Instructions.Rem);

					else if (instruction is IR.Instructions.Neg)
						this.Neg (instruction as IR.Instructions.Neg);

					else if (instruction is IR.Instructions.Shl)
						this.Shl (instruction as IR.Instructions.Shl);

					else if (instruction is IR.Instructions.Shr)
						this.Shr (instruction as IR.Instructions.Shr);

					else if (instruction is IR.Instructions.And)
						this.And (instruction as IR.Instructions.And);

					else if (instruction is IR.Instructions.Or)
						this.Or (instruction as IR.Instructions.Or);

					else if (instruction is IR.Instructions.Xor)
						this.Xor (instruction as IR.Instructions.Xor);

					else if (instruction is IR.Instructions.Not)
						this.Not (instruction as IR.Instructions.Not);

					else if (instruction is IR.Instructions.Pop)
						this.Pop (instruction as IR.Instructions.Pop);

					else if (instruction is IR.Instructions.Newobj)
						this.Newobj (instruction as IR.Instructions.Newobj);

					else if (instruction is IR.Instructions.Stobj)
						this.Stobj (instruction as IR.Instructions.Stobj);

					else if (instruction is IR.Instructions.Ldobj)
						this.Ldobj (instruction as IR.Instructions.Ldobj);

					else if (instruction is IR.Instructions.Initobj)
						this.Initobj (instruction as IR.Instructions.Initobj);

					else if (instruction is IR.Instructions.SizeOf)
						this.SizeOf (instruction as IR.Instructions.SizeOf);

					else if (instruction is IR.Instructions.Switch)
						this.Switch (instruction as IR.Instructions.Switch);

					else if (instruction is IR.Instructions.Box)
						this.Box (instruction as IR.Instructions.Box);

					else if (instruction is IR.Instructions.Unbox)
						this.Unbox (instruction as IR.Instructions.Unbox);

					else if (instruction is IR.Instructions.UnboxAny)
						this.UnboxAny (instruction as IR.Instructions.UnboxAny);

					else if (instruction is IR.Instructions.Newarr)
						this.Newarr (instruction as IR.Instructions.Newarr);

					else if (instruction is IR.Instructions.Stelem)
						this.Stelem (instruction as IR.Instructions.Stelem);

					else if (instruction is IR.Instructions.Ldelem)
						this.Ldelem (instruction as IR.Instructions.Ldelem);

					else if (instruction is IR.Instructions.Ldlen)
						this.Ldlen (instruction as IR.Instructions.Ldlen);

					else
						throw new EngineException ("'" + instruction + "' is not supported.");
				}
			}

			assembly.LABEL (fullname + " exit");

			assembly.LEA (R32.ESP, new DWordMemory (null, R32.EBP, null, 0, -12));
			assembly.POP (R32.EDI);
			assembly.POP (R32.ESI);
			assembly.POP (R32.EBX);
			assembly.POP (R32.EBP);
			assembly.RET ();

			return true;
		}

		#region Helper
		private string GetLabel (Block block)
		{
			return method.MethodFullName + " " + block.Index.ToString ();
		}

		private void RelationalTypeCMP (RelationalType type, string okLabel, string errorLabel)
		{
			switch (type) {

			case RelationalType.Equal:
				assembly.JNE (errorLabel);

				break;

			case RelationalType.NotEqualOrUnordered:
				assembly.JNE (okLabel);

				break;

			case RelationalType.LessThan:
				assembly.JG (errorLabel);

				assembly.JL (okLabel);

				break;

			case RelationalType.LessThanUnsignedOrUnordered:
				assembly.JA (errorLabel);

				assembly.JB (okLabel);

				break;

			case RelationalType.LessThanOrEqual:
				assembly.JG (errorLabel);

				assembly.JL (okLabel);

				break;

			case RelationalType.LessThanOrEqualUnsignedOrUnordered:
				assembly.JA (errorLabel);

				assembly.JB (okLabel);

				break;

			case RelationalType.GreaterThan:
				assembly.JL (errorLabel);

				assembly.JG (okLabel);

				break;

			case RelationalType.GreaterThanUnsignedOrUnordered:
				assembly.JB (errorLabel);

				assembly.JA (okLabel);

				break;

			case RelationalType.GreaterThanOrEqual:
				assembly.JL (errorLabel);

				assembly.JG (okLabel);

				break;

			case RelationalType.GreaterThanOrEqualUnsignedOrUnordered:
				assembly.JB (errorLabel);

				assembly.JA (okLabel);

				break;

			default:
				throw new NotImplementedEngineException ("'" + type + "' is not supported.");
			}
		}

		public void RelationalTypeCMP (RelationalType type, string label)
		{
			switch (type) {
			case RelationalType.Equal:
				assembly.JE (label);

				break;

			case RelationalType.NotEqualOrUnordered:
				assembly.JNE (label);

				break;

			case RelationalType.LessThan:
				assembly.JL (label);

				break;

			case RelationalType.LessThanOrEqual:
				assembly.JLE (label);

				break;

			case RelationalType.GreaterThan:
				assembly.JG (label);

				break;

			case RelationalType.GreaterThanOrEqual:
				assembly.JGE (label);

				break;

			case RelationalType.LessThanUnsignedOrUnordered:
				assembly.JB (label);

				break;

			case RelationalType.LessThanOrEqualUnsignedOrUnordered:
				assembly.JBE (label);

				break;

			case RelationalType.GreaterThanUnsignedOrUnordered:
				assembly.JA (label);

				break;

			case RelationalType.GreaterThanOrEqualUnsignedOrUnordered:
				assembly.JAE (label);

				break;

			default:
				throw new NotImplementedEngineException ("'" + type + "' is not supported.");
			}
		}

		private void PushOperand (Operand operand)
		{
			if (operand is IntConstant) {
				Int32 value = System.Convert.ToInt32 ((operand as IntConstant).Value);

				assembly.PUSH ((UInt32) value);

			} else if (operand is LongConstant) {
				Int64 value = System.Convert.ToInt64 ((operand as LongConstant).Value);

				assembly.PUSH ((UInt32) (value >> 32));
				assembly.PUSH ((UInt32) (value & 0xFFFFFFFF));

			} else if (operand is IR.Operands.Register) {
				IR.Operands.Register identifier = operand as IR.Operands.Register;

				switch (identifier.InternalType) {
				case InternalType.SZArray:
				case InternalType.Array:
				case InternalType.I:
				case InternalType.O:
				case InternalType.M:
				case InternalType.I4:
					if (identifier.IsRegisterSet)
						this.assembly.PUSH (Assembly.GetRegister (identifier.Register));

					else
						this.assembly.PUSH (new DWordMemory (this.GetAddress (identifier)));

					break;

				case InternalType.I8:
					Memory memory = this.GetAddress (identifier);
					
					DWordMemory high = new DWordMemory (memory);
					high.DisplacementDelta = 4;
					assembly.PUSH (new DWordMemory (high));

					DWordMemory low = new DWordMemory (memory);
					assembly.PUSH (low);

					break;

				case InternalType.ValueType:
					int size = this.method.Engine.GetTypeSize (identifier.Type.ToString ());

					uint pushSize = (uint) size;

					if (pushSize % 4 != 0)
						pushSize = ((pushSize / 4) + 1) * 4;

					this.assembly.SUB (R32.ESP, pushSize);

					this.assembly.PUSH (R32.ESI);
					this.assembly.PUSH (R32.EDI);
					this.assembly.PUSH (R32.ECX);

					this.assembly.LEA (R32.ESI, new DWordMemory (this.GetAddress (identifier)));

					// The 3 push above changed the ESP so we need a LEA = ESP + 12
					this.assembly.LEA (R32.EDI, new Memory (null, R32.ESP, null, 0, 12));
					this.assembly.MOV (R32.ECX, (uint) size);

					this.assembly.CLD ();
					this.assembly.REP ();
					this.assembly.MOVSB ();

					this.assembly.POP (R32.ECX);
					this.assembly.POP (R32.EDI);
					this.assembly.POP (R32.ESI);
					break;

				default:
					throw new NotImplementedEngineException ("'" + operand + "' is not supported.");
				}

			} else
				throw new NotImplementedEngineException ("'" + operand + "' is not supported.");
		}

		#endregion

		/// <summary>
		/// Gets the memory.
		/// </summary>
		/// <param name="operand">The operand.</param>
		/// <param name="emit">if set to <c>true</c> [emit].</param>
		/// <returns></returns>
		private Memory GetMemory (Operand operand)
		{
			Memory address = null;

			if (operand is FieldOperand) {
				address = this.GetAddress (operand as FieldOperand);

			} else if (operand is Argument) {
				address = this.GetAddress (operand as Argument);

			} else if (operand is Local) {
				address = this.GetAddress (operand as Local);

			} else if (operand is IR.Operands.Register) {
				address = this.GetAddress (operand as IR.Operands.Register);

			} else
				throw new NotImplementedEngineException ("Wrong '" + operand.ToString () + "' Operand.");

			if (operand.InternalType == InternalType.I1
					|| operand.InternalType == InternalType.U1) {
				address = new ByteMemory (address);

			} else if (operand.InternalType == InternalType.I2
					|| operand.InternalType == InternalType.U2) {
				address = new WordMemory (address);

			} else if (operand.InternalType == InternalType.I4
					|| operand.InternalType == InternalType.U4
					|| operand.InternalType == InternalType.I
					|| operand.InternalType == InternalType.U
					|| operand.InternalType == InternalType.ValueType
					|| operand.InternalType == InternalType.O
					|| operand.InternalType == InternalType.M
					|| operand.InternalType == InternalType.SZArray
					|| operand.InternalType == InternalType.Array) {
				address = new DWordMemory (address);

			} else if (operand.InternalType == InternalType.I8
					|| operand.InternalType == InternalType.U8) {
				address = new DWordMemory (address);

			} else
				throw new NotImplementedEngineException ("'" + operand.InternalType + "' not supported.");

			return address;
		}

		private Memory GetAddress (IR.Operands.Argument argument)
		{
			return new Memory (null, R32.EBP, null, 0, this.GetArgumentOffset (argument.Index) * 4);
		}

		private Memory GetAddress (IR.Operands.Local local)
		{
			return new Memory (null, R32.EBP, null, 0, this.GetIdentifierDisplacement (local));
		}

		private Memory GetAddress (IR.Operands.Register register)
		{
			return new Memory (null, R32.EBP, null, 0, this.GetIdentifierDisplacement (register));
		}

		private Memory GetAddress (IR.Operands.FieldOperand field)
		{
			if (field.Instance != null) {
				IR.Operands.Register identifier = field.Instance as IR.Operands.Register;
				R32Type register;

				if (identifier.IsRegisterSet)
					register = Assembly.GetRegister (identifier.Register);

				else {
					register = R32.EDX;

					this.assembly.MOV (register, new DWordMemory (this.GetAddress (identifier)));
				}

				return new Memory (null, register, null, 0, this.assembly.Engine.GetFieldOffset (field));
			}

			return new Memory (field.Field.FieldDefinition.ToString ());
		}

		private Memory GetAddress (IR.Operands.Identifier identifier)
		{
			if (identifier is Argument)
				return this.GetAddress (identifier as Argument);

			else if (identifier is Local)
				return this.GetAddress (identifier as Local);

			else if (identifier is IR.Operands.Register)
				return this.GetAddress (identifier as IR.Operands.Register);

			throw new NotImplementedEngineException ();
		}

		private int GetIdentifierDisplacement (IR.Operands.Identifier identifier)
		{
			return -((3 + identifier.Stack) * 4);
		}

		/// <summary>
		/// Gets the argument offset.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		private int GetArgumentOffset (int index)
		{
			int i = 0;
			int result = 2; // EIP (of the caller) + EBP

			// If the return type is a value type then it will get the address for the buffer where the result is saved
			if (this.method.MethodDefinition.ReturnType.ReturnType is TypeDefinition
					&& (this.method.MethodDefinition.ReturnType.ReturnType as TypeDefinition).IsValueType)
				result++;

			foreach (ParameterDefinition parameter in this.method.MethodDefinition.Parameters) {
				InternalType InternalType = this.method.Engine.GetInternalType (parameter.ParameterType.ToString ());

				if (i++ == index)
					break;

				result += this.method.Engine.GetTypeSize (parameter.ParameterType.ToString (), 4) >> 2;
			}

			return result;
		}

		/// <summary>
		/// Determines whether the specified operand is signed.
		/// </summary>
		/// <param name="operand">The operand.</param>
		/// <returns>
		/// 	<c>true</c> if the specified operand is signed; otherwise, <c>false</c>.
		/// </returns>
		private static bool IsSigned (Operand operand)
		{
			if (operand.InternalType == InternalType.I
					|| operand.InternalType == InternalType.I1
					|| operand.InternalType == InternalType.I2
					|| operand.InternalType == InternalType.I4
					|| operand.InternalType == InternalType.I8)
				return true;

			return false;
		}
	}
}
