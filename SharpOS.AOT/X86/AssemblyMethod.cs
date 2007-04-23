// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Bruce <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using SharpOS.AOT.IR;
using SharpOS.AOT.IR.Instructions;
using SharpOS.AOT.IR.Operands;
using SharpOS.AOT.IR.Operators;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace SharpOS.AOT.X86 {
	internal class AssemblyMethod {
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
			string fullname = method.MethodFullName;

			assembly.LABEL (fullname);
			assembly.PUSH (R32.EBP);
			assembly.MOV (R32.EBP, R32.ESP);
			assembly.PUSH (R32.EBX);
			assembly.PUSH (R32.ESI);
			assembly.PUSH (R32.EDI);

			if (method.StackSize > 0)
				assembly.SUB (R32.ESP, (UInt32) (method.StackSize * 4));

			foreach (Block block in method) {

				assembly.LABEL (fullname + "_" + block.Index.ToString()); //StartOffset.ToString());

				foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in block) {
					if (instruction is SharpOS.AOT.IR.Instructions.Call
							&& assembly.IsAssemblyStub ( (instruction as SharpOS.AOT.IR.Instructions.Call).Method.Method.DeclaringType.FullName)) {
						this.HandleAssemblyStub (block, instruction);

					} else if (instruction is SharpOS.AOT.IR.Instructions.Call) {
						this.HandleCall (block, (instruction as SharpOS.AOT.IR.Instructions.Call).Method as SharpOS.AOT.IR.Operands.Call);

					} else if (instruction is SharpOS.AOT.IR.Instructions.Assign) {
						this.HandleAssign (block, instruction);

					} else if (instruction is SharpOS.AOT.IR.Instructions.ConditionalJump) {
						this.HandleConditionalJump (block, instruction);

					} else if (instruction is SharpOS.AOT.IR.Instructions.Jump) {
						this.HandleJump (block, instruction);

					} else if (instruction is SharpOS.AOT.IR.Instructions.Return) {
						this.HandleReturn (block, instruction as SharpOS.AOT.IR.Instructions.Return);

					} else if (instruction is SharpOS.AOT.IR.Instructions.Pop) {
						// Nothing to do

					} else {
						throw new Exception ("'" + instruction + "' is not supported.");
					}
				}
			}

			assembly.LABEL (fullname + "_exit");

			assembly.LEA (R32.ESP, new DWordMemory (null, R32.EBP, null, 0, -12));
			assembly.POP (R32.EDI);
			assembly.POP (R32.ESI);
			assembly.POP (R32.EBX);
			assembly.POP (R32.EBP);
			assembly.RET();

			return true;
		}

		/// <summary>
		/// Handles the conditional jump.
		/// </summary>
		/// <param name="block">The block.</param>
		/// <param name="instruction">The instruction.</param>
		private void HandleConditionalJump (Block block, SharpOS.AOT.IR.Instructions.Instruction instruction)
		{
			SharpOS.AOT.IR.Instructions.ConditionalJump jump = instruction as SharpOS.AOT.IR.Instructions.ConditionalJump;

			string label = method.MethodFullName + "_" + block.Outs[0].Index.ToString(); //.StartOffset.ToString();

			if (jump.Value is SharpOS.AOT.IR.Operands.Boolean) {
				SharpOS.AOT.IR.Operands.Boolean expression = jump.Value as SharpOS.AOT.IR.Operands.Boolean;

				if (expression.Operator is SharpOS.AOT.IR.Operators.Relational) {
					if (this.IsFourBytes (expression.Operands[0])
							&& this.IsFourBytes (expression.Operands[1])) {
						R32Type spare1 = assembly.GetSpareRegister();
						R32Type spare2 = assembly.GetSpareRegister();

						this.MovRegisterOperand (spare1, expression.Operands[0]);
						this.MovRegisterOperand (spare2, expression.Operands[1]);

						assembly.CMP (spare1, spare2);

						assembly.FreeSpareRegister (spare1);
						assembly.FreeSpareRegister (spare2);

						SharpOS.AOT.IR.Operators.Relational relational = expression.Operator as SharpOS.AOT.IR.Operators.Relational;

						switch (relational.Type) {

							case Operator.RelationalType.Equal:
								assembly.JE (label);

								break;

							case Operator.RelationalType.NotEqualOrUnordered:
								assembly.JNE (label);

								break;

							case Operator.RelationalType.LessThan:
								assembly.JL (label);

								break;

							case Operator.RelationalType.LessThanOrEqual:
								assembly.JLE (label);

								break;

							case Operator.RelationalType.GreaterThan:
								assembly.JG (label);

								break;

							case Operator.RelationalType.GreaterThanOrEqual:
								assembly.JGE (label);

								break;

							case Operator.RelationalType.LessThanUnsignedOrUnordered:
								assembly.JB (label);

								break;

							case Operator.RelationalType.LessThanOrEqualUnsignedOrUnordered:
								assembly.JBE (label);

								break;

							case Operator.RelationalType.GreaterThanUnsignedOrUnordered:
								assembly.JA (label);

								break;

							case Operator.RelationalType.GreaterThanOrEqualUnsignedOrUnordered:
								assembly.JAE (label);

								break;

							default:
								throw new Exception ("'" + relational.Type + "' is not supported.");
						}

					} else {
						SharpOS.AOT.IR.Operators.Boolean.RelationalType type = (expression.Operator as SharpOS.AOT.IR.Operators.Relational).Type;
						string errorLabel = assembly.GetCMPLabel;

						this.CMP (type, expression.Operands[0], expression.Operands[1], label, errorLabel, errorLabel);

						assembly.LABEL (errorLabel);
					}

				} else if (expression.Operator is SharpOS.AOT.IR.Operators.Boolean) {
					// TODO i4, i8, r4, r8 check?

					SharpOS.AOT.IR.Operators.Boolean boolean = expression.Operator as SharpOS.AOT.IR.Operators.Boolean;

					if (expression.Operands[0].IsRegisterSet) {
						R32Type register = assembly.GetRegister (expression.Operands[0].Register);

						assembly.TEST (register, register);

					} else {
						R32Type register = assembly.GetSpareRegister();

						this.MovRegisterMemory (register, expression.Operands[0] as Identifier);

						assembly.TEST (register, register);

						assembly.FreeSpareRegister (register);
					}

					switch (boolean.Type) {

						case Operator.BooleanType.True:
							assembly.JNE (label);

							break;

						case Operator.BooleanType.False:
							assembly.JE (label);

							break;

						default:
							throw new Exception ("'" + expression.Operator.GetType() + "' is not supported.");
					}

				} else {
					throw new Exception ("'" + expression.Operator.GetType() + "' is not supported.");
				}

			} else {
				throw new Exception ("'" + jump.Value.GetType() + "' is not supported.");
			}
		}

		/// <summary>
		/// Handles the jump.
		/// </summary>
		/// <param name="block">The block.</param>
		/// <param name="instruction">The instruction.</param>
		private void HandleJump (Block block, SharpOS.AOT.IR.Instructions.Instruction instruction)
		{
			SharpOS.AOT.IR.Instructions.Jump jump = instruction as SharpOS.AOT.IR.Instructions.Jump;

			assembly.JMP (method.MethodFullName + "_" + block.Outs[0].Index.ToString()); //.StartOffset.ToString());
		}

		/// <summary>
		/// Handles the assembly stub.
		/// </summary>
		/// <param name="block">The block.</param>
		/// <param name="instruction">The instruction.</param>
		private void HandleAssemblyStub (Block block, SharpOS.AOT.IR.Instructions.Instruction instruction)
		{
			SharpOS.AOT.IR.Instructions.Call call = instruction as SharpOS.AOT.IR.Instructions.Call;

			string parameterTypes = string.Empty;
			object[] operands = new object[call.Method.Method.Parameters.Count];

			for (int i = 0; i < call.Method.Method.Parameters.Count; i++) {
				ParameterDefinition parameter = call.Method.Method.Parameters[i];

				if (parameterTypes.Length > 0) {
					parameterTypes += " ";
				}

				if (call.Value.Operands[i] is SharpOS.AOT.IR.Operands.Reference) {
					Operand operand = (call.Value.Operands[i] as SharpOS.AOT.IR.Operands.Reference).Value;

					if (operand.IsRegisterSet) {
						Register register = assembly.GetRegister (operand.Register);
						parameterTypes += register.GetType().Name;
						operands[i] = register;

					} else {
						Memory memory = this.GetMemory (operand as Identifier);
						parameterTypes += memory.GetType().Name;
						operands[i] = memory;
					}

				} else {
					parameterTypes += parameter.ParameterType.Name;

					operands[i] = call.Value.Operands[i];
				}
			}

			parameterTypes = call.Method.Method.Name + " " + parameterTypes;

			parameterTypes = parameterTypes.Trim();

			assembly.GetAssemblyInstruction (call.Method, operands, parameterTypes);
		}

		/// <summary>
		/// Handles the call.
		/// </summary>
		/// <param name="block">The block.</param>
		/// <param name="call">The call.</param>
		private void HandleCall (Block block, SharpOS.AOT.IR.Operands.Call call)
		{
			for (int i = 0; i < call.Operands.Length; i++) {
				Operand operand = call.Operands [call.Operands.Length - i - 1];

				if (operand is Constant) {
					if (this.IsFourBytes (operand)) {
						Int32 value = Convert.ToInt32 ((operand as Constant).Value);

						assembly.PUSH ((UInt32) value);

					} else if (this.IsEightBytes (operand)) {
						Int64 value = Convert.ToInt64 ((operand as Constant).Value);

						assembly.PUSH ((UInt32) (value >> 32));
						assembly.PUSH ((UInt32) (value & 0xFFFFFFFF));

					} else
						throw new Exception ("'" + operand + "' is not supported.");

				} else if (operand is Argument
						|| operand is SharpOS.AOT.IR.Operands.Register
						|| operand is Reference
						|| operand is Local) {
					if (this.IsFourBytes (operand)) {
						if (operand.IsRegisterSet)
							assembly.PUSH (assembly.GetRegister (operand.Register));

						else {
							this.MovRegisterMemory (R32.EAX, operand as Identifier);
							assembly.PUSH (R32.EAX);
						}

					} else
						throw new Exception ("'" + operand + "' is not supported.");

				} else if (operand is SharpOS.AOT.IR.Operands.Object) {
					SharpOS.AOT.IR.Operands.Object _object = operand as SharpOS.AOT.IR.Operands.Object;

					int size = this.method.Engine.GetTypeSize (_object.Value);
					
					this.assembly.SUB (R32.ESP, (uint) size);

					this.assembly.PUSH (R32.ESI);
					this.assembly.PUSH (R32.EDI);
					this.assembly.PUSH (R32.ECX);

					this.assembly.MOV (R32.ECX, (uint) (size / 4));
					this.assembly.CLD ();
					this.assembly.MOVSD ();

					this.assembly.POP (R32.ECX);
					this.assembly.POP (R32.EDI);
					this.assembly.POP (R32.ESI);

				} else
					throw new Exception ("'" + call.Operands [i].GetType () + "' is not supported.");
			}

			assembly.CALL (call.AssemblyLabel);

			int result = call.Method.Parameters.Count;

			foreach (ParameterDefinition parameter in call.Method.Parameters) {
				Operand.InternalSizeType sizeType = this.method.Engine.GetInternalType (parameter.ParameterType.ToString ());

				if (sizeType == Operand.InternalSizeType.I8
						|| sizeType == Operand.InternalSizeType.U8
						|| sizeType == Operand.InternalSizeType.R8)
					result++;
			}

			assembly.ADD (R32.ESP, (UInt32) (4 * result));
		}

		/// <summary>
		/// Movs the register operand.
		/// </summary>
		/// <param name="register">The register.</param>
		/// <param name="operand">The operand.</param>
		private void MovRegisterOperand (R32Type register, Operand operand)
		{
			if (operand is Constant) {
				this.MovRegisterConstant (register, operand as Constant);

			} else if (operand.IsRegisterSet) {
				this.MovRegisterRegister (register, assembly.GetRegister (operand.Register));

			} else
				this.MovRegisterMemory (register, operand as Identifier);
		}

		/// <summary>
		/// Movs the register operand.
		/// </summary>
		/// <param name="loRegister">The lo register.</param>
		/// <param name="hiRegister">The hi register.</param>
		/// <param name="operand">The operand.</param>
		private void MovRegisterOperand (R32Type loRegister, R32Type hiRegister, Operand operand)
		{
			if (operand is Constant) {
				Int64 value = Convert.ToInt64 ((operand as Constant).Value);

				this.MovRegisterConstant (loRegister, hiRegister, (UInt64) value);

			} else if (!operand.IsRegisterSet) {
				this.MovRegisterMemory (loRegister, hiRegister, operand as Identifier);

			} else
				throw new Exception ("'" + operand + "' not supported.");
		}

		/// <summary>
		/// Movs the operand register.
		/// </summary>
		/// <param name="operand">The operand.</param>
		/// <param name="register">The register.</param>
		private void MovOperandRegister (Operand operand, R32Type register)
		{
			if (operand.IsRegisterSet)
				this.MovRegisterRegister (assembly.GetRegister (operand.Register), register);

			else
				this.MovMemoryRegister (operand as Identifier, register);
		}

		/// <summary>
		/// Movs the register memory.
		/// </summary>
		/// <param name="register">The register.</param>
		/// <param name="identifier">The identifier.</param>
		private void MovRegisterMemory (R32Type register, Identifier identifier)
		{
			Memory memory = this.GetMemory (identifier);

			if (identifier.SizeType == Operand.InternalSizeType.Object)
				assembly.LEA (register, memory);

			else if (memory is DWordMemory)
				assembly.MOV (register, memory as DWordMemory);

			else if (memory is WordMemory) {
				if (this.IsSigned (identifier))
					assembly.MOVSX (register, memory as WordMemory);

				else
					assembly.MOVZX (register, memory as WordMemory);

			} else if (memory is ByteMemory) {
				if (this.IsSigned (identifier))
					assembly.MOVSX (register, memory as ByteMemory);

				else
					assembly.MOVZX (register, memory as ByteMemory);

			} else
				throw new Exception ("'" + memory.ToString () + "' is not supported.");
		}

		/// <summary>
		/// Movs the register memory.
		/// </summary>
		/// <param name="loRegister">The lo register.</param>
		/// <param name="hiRegister">The hi register.</param>
		/// <param name="identifier">The identifier.</param>
		private void MovRegisterMemory (R32Type loRegister, R32Type hiRegister, Identifier identifier)
		{
			DWordMemory memory = this.GetMemoryType (identifier) as DWordMemory;

			assembly.MOV (loRegister, memory);

			memory = new DWordMemory (memory);
			memory.DisplacementDelta = 4;

			assembly.MOV (hiRegister, memory);
		}

		/// <summary>
		/// Movs the memory register.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		/// <param name="loRegister">The lo register.</param>
		/// <param name="hiRegister">The hi register.</param>
		private void MovMemoryRegister (Identifier identifier, R32Type loRegister, R32Type hiRegister)
		{
			DWordMemory memory = this.GetMemoryType (identifier) as DWordMemory;

			assembly.MOV (memory, loRegister);

			memory = new DWordMemory (memory);
			memory.DisplacementDelta = 4;

			assembly.MOV (memory, hiRegister);
		}

		/// <summary>
		/// Movs the memory register.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		/// <param name="register">The register.</param>
		private void MovMemoryRegister (Identifier identifier, R32Type register)
		{
			Memory memory = this.GetMemoryType (identifier);

			if (memory is ByteMemory) {
				R32Type spare = assembly.GetSpareRegister();
				ByteMemory byteMemory = this.GetMemory (identifier) as ByteMemory;

				this.MovRegisterRegister (spare, register);
				assembly.MOV (byteMemory, assembly.Get8BitRegister (spare));

				assembly.FreeSpareRegister (spare);

			} else if (memory is WordMemory) {
				R32Type spare = assembly.GetSpareRegister();
				WordMemory wordMemory = this.GetMemory (identifier) as WordMemory;

				this.MovRegisterRegister (spare, register);
				assembly.MOV (wordMemory, assembly.Get16BitRegister (spare));

				assembly.FreeSpareRegister (spare);

			} else if (memory is DWordMemory) {
				assembly.MOV (memory as DWordMemory, register);

			} else
				throw new Exception ("'" + memory.ToString() + "' is not supported.");
		}

		/// <summary>
		/// CMPs the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <param name="okLabel">The ok label.</param>
		/// <param name="errorLabel">The error label.</param>
		/// <param name="endLabel">The end label.</param>
		private void CMP (SharpOS.AOT.IR.Operators.Boolean.RelationalType type, Operand first, Operand second, string okLabel, string errorLabel, string endLabel)
		{
			this.MovRegisterOperand (R32.EAX, R32.EDX, first);

			if (second is Constant) {
				Int64 constant = Convert.ToInt64 ( (second as Constant).Value);

				assembly.CMP (R32.EDX, (UInt32) (constant >> 32));

			} else {
				DWordMemory memory = this.GetMemory (second as Identifier) as DWordMemory;

				memory.DisplacementDelta = 4;

				assembly.CMP (R32.EDX, memory);
			}

			switch (type) {

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.Equal:
					assembly.JNE (errorLabel);

					break;

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.NotEqualOrUnordered:
					assembly.JNE (okLabel);

					break;

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.LessThan:
					assembly.JG (errorLabel);

					assembly.JL (okLabel);

					break;

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.LessThanUnsignedOrUnordered:
					assembly.JA (errorLabel);

					assembly.JB (okLabel);

					break;

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.LessThanOrEqual:
					assembly.JG (errorLabel);

					assembly.JL (okLabel);

					break;

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.LessThanOrEqualUnsignedOrUnordered:
					assembly.JA (errorLabel);

					assembly.JB (okLabel);

					break;

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.GreaterThan:
					assembly.JL (errorLabel);

					assembly.JG (okLabel);

					break;

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.GreaterThanUnsignedOrUnordered:
					assembly.JB (errorLabel);

					assembly.JA (okLabel);

					break;

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.GreaterThanOrEqual:
					assembly.JL (errorLabel);

					assembly.JG (okLabel);

					break;

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.GreaterThanOrEqualUnsignedOrUnordered:
					assembly.JB (errorLabel);

					assembly.JA (okLabel);

					break;

				default:
					throw new Exception ("'" + type + "' is not supported.");
			}

			if (second is Constant) {
				Int64 constant = Convert.ToInt64 ( (second as Constant).Value);

				assembly.CMP (R32.EAX, (UInt32) (constant & 0xFFFFFFFF));

			} else {
				Memory memory = this.GetMemory (second as Identifier);

				assembly.CMP (R32.EAX, memory as DWordMemory);
			}

			switch (type) {

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.Equal:
					assembly.JE (okLabel);

					break;

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.NotEqualOrUnordered:
					assembly.JNE (okLabel);

					break;

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.LessThan:
					assembly.JB (okLabel);

					break;

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.LessThanUnsignedOrUnordered:
					assembly.JB (okLabel);

					break;

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.LessThanOrEqual:
					assembly.JBE (okLabel);

					break;

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.LessThanOrEqualUnsignedOrUnordered:
					assembly.JBE (okLabel);

					break;

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.GreaterThan:
					assembly.JA (okLabel);

					break;

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.GreaterThanUnsignedOrUnordered:
					assembly.JA (okLabel);

					break;

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.GreaterThanOrEqual:
					assembly.JAE (okLabel);

					break;

				case SharpOS.AOT.IR.Operators.Boolean.RelationalType.GreaterThanOrEqualUnsignedOrUnordered:
					assembly.JAE (okLabel);

					break;

				default:
					throw new Exception ("'" + type + "' is not supported.");
			}
		}

		/// <summary>
		/// Movs the register boolean.
		/// </summary>
		/// <param name="register">The register.</param>
		/// <param name="operand">The operand.</param>
		private void MovRegisterBoolean (R32Type register, SharpOS.AOT.IR.Operands.Boolean operand)
		{
			SharpOS.AOT.IR.Operators.Boolean.RelationalType type = (operand.Operator as SharpOS.AOT.IR.Operators.Relational).Type;

			Operand first = operand.Operands[0];
			Operand second = operand.Operands[1];

			if (first is Constant) {
				Operand temp = first;
				first = second;
				second = temp;
			}

			if (this.IsEightBytes (first)
					|| this.IsEightBytes (second)) {
				string errorLabel = assembly.GetCMPLabel;
				string okLabel = assembly.GetCMPLabel;
				string endLabel = assembly.GetCMPLabel;

				this.CMP (type, first, second, okLabel, errorLabel, endLabel);

				assembly.LABEL (errorLabel);
				this.MovRegisterConstant (register, 0);
				assembly.JMP (endLabel);

				assembly.LABEL (okLabel);
				this.MovRegisterConstant (register, 1);

				assembly.LABEL (endLabel);

			} else {
				this.MovRegisterOperand (register, first);

				if (second is Constant) {
					Int32 value = Convert.ToInt32 ( (second as Constant).Value);

					assembly.CMP (register, (UInt32) value);

				} else if (second.IsRegisterSet) {
					assembly.CMP (register, assembly.GetRegister (second.Register));

				} else {
					Memory memory = this.GetMemory (second as Identifier);

					assembly.CMP (register, memory as DWordMemory);
				}

				switch (type) {

					case SharpOS.AOT.IR.Operators.Boolean.RelationalType.Equal:
						assembly.SETE (R8.AL);

						break;

					case SharpOS.AOT.IR.Operators.Boolean.RelationalType.GreaterThan:
						assembly.SETG (R8.AL);

						break;

					case SharpOS.AOT.IR.Operators.Boolean.RelationalType.GreaterThanUnsignedOrUnordered:
						assembly.SETA (R8.AL);

						break;

					case SharpOS.AOT.IR.Operators.Boolean.RelationalType.LessThan:
						assembly.SETL (R8.AL);

						break;

					case SharpOS.AOT.IR.Operators.Boolean.RelationalType.LessThanUnsignedOrUnordered:
						assembly.SETB (R8.AL);

						break;

					default:
						throw new Exception ("'" + operand.Operator + "' is not supported.");
				}

				assembly.MOVZX (register, R8.AL);
			}
		}

		/// <summary>
		/// Movs the register arithmetic.
		/// </summary>
		/// <param name="loRegister">The lo register.</param>
		/// <param name="hiRegister">The hi register.</param>
		/// <param name="operand">The operand.</param>
		private void MovRegisterArithmetic (R32Type loRegister, R32Type hiRegister, Arithmetic operand)
		{
			if (operand.Operator is Unary) {
				Unary.UnaryType type = (operand.Operator as Unary).Type;
				Operand first = operand.Operands[0];

				this.MovRegisterOperand (loRegister, hiRegister, first);

				if (type == Operator.UnaryType.Negation) {
					assembly.NEG (loRegister);
					assembly.NEG (hiRegister);

				} else if (type == Operator.UnaryType.Not) {
					assembly.NOT (loRegister);
					assembly.NOT (hiRegister);

				} else {
					throw new Exception ("'" + type + "' is not supported.");
				}

			} else if (operand.Operator is Binary) {
				Binary.BinaryType type = (operand.Operator as Binary).Type;
				Operand first = operand.Operands[0];
				Operand second = operand.Operands[1];

				this.MovRegisterOperand (loRegister, hiRegister, first);

				// TODO validate the parameter types int32 & int32, int64 & int64.....
				if (type == Binary.BinaryType.Add) {
					if (second is Constant) {
						Int64 value = Convert.ToInt64 ( (second as Constant).Value);

						UInt32 loConstant = (UInt32) (value & 0xFFFFFFFF);
						UInt32 hiConstant = (UInt32) (value >> 32);

						assembly.ADD (loRegister, loConstant);
						assembly.ADC (hiRegister, hiConstant);

					} else if (!second.IsRegisterSet) {
						DWordMemory memory = this.GetMemoryType (second as Identifier) as DWordMemory;

						assembly.ADD (loRegister, memory);

						memory = new DWordMemory (memory);
						memory.DisplacementDelta = 4;

						assembly.ADC (hiRegister, memory);

					} else
						throw new Exception ("'" + second + "' is not supported.");

				} else if (type == Operator.BinaryType.Sub) {
					if (second is Constant) {
						Int64 value = Convert.ToInt64 ( (second as Constant).Value);

						UInt32 loConstant = (UInt32) (value & 0xFFFFFFFF);
						UInt32 hiConstant = (UInt32) (value >> 32);

						assembly.SUB (loRegister, loConstant);
						assembly.SBB (hiRegister, hiConstant);

					} else if (!second.IsRegisterSet) {
						DWordMemory memory = this.GetMemoryType (second as Identifier) as DWordMemory;

						assembly.SUB (loRegister, memory);

						memory = new DWordMemory (memory);
						memory.DisplacementDelta = 4;

						assembly.SBB (hiRegister, memory);

					} else
						throw new Exception ("'" + second + "' is not supported.");

				} else if (type == Operator.BinaryType.And) {
					if (second is Constant) {
						Int64 value = Convert.ToInt64 ( (second as Constant).Value);

						UInt32 loConstant = (UInt32) (value & 0xFFFFFFFF);
						UInt32 hiConstant = (UInt32) (value >> 32);

						assembly.AND (loRegister, loConstant);
						assembly.AND (hiRegister, hiConstant);

					} else if (!second.IsRegisterSet) {
						DWordMemory memory = this.GetMemoryType (second as Identifier) as DWordMemory;

						assembly.AND (loRegister, memory);

						memory = new DWordMemory (memory);
						memory.DisplacementDelta = 4;

						assembly.AND (hiRegister, memory);

					} else
						throw new Exception ("'" + second + "' is not supported.");

				} else if (type == Operator.BinaryType.Or) {
					if (second is Constant) {
						Int64 value = Convert.ToInt64 ( (second as Constant).Value);

						UInt32 loConstant = (UInt32) (value & 0xFFFFFFFF);
						UInt32 hiConstant = (UInt32) (value >> 32);

						assembly.OR (loRegister, loConstant);
						assembly.OR (hiRegister, hiConstant);

					} else if (!second.IsRegisterSet) {
						DWordMemory memory = this.GetMemoryType (second as Identifier) as DWordMemory;

						assembly.OR (loRegister, memory);

						memory = new DWordMemory (memory);
						memory.DisplacementDelta = 4;

						assembly.OR (hiRegister, memory);

					} else
						throw new Exception ("'" + second + "' is not supported.");

				} else if (type == Operator.BinaryType.SHL
						|| type == Operator.BinaryType.SHR
						|| type == Operator.BinaryType.SHRUnsigned) {
					
					// Only the lower 32-bit are needed for the second shift parameter
					if (second is Constant) {
						Int64 value = Convert.ToInt64 ( (second as Constant).Value);

						UInt32 loConstant = (UInt32) (value & 0xFFFFFFFF);

						assembly.PUSH (loConstant);

					} else if (!second.IsRegisterSet) {
						DWordMemory memory = this.GetMemoryType (second as Identifier) as DWordMemory;

						assembly.PUSH (memory);

					} else
						throw new Exception ("'" + second + "' is not supported.");

					assembly.PUSH (loRegister);

					assembly.PUSH (hiRegister);

					if (type == Operator.BinaryType.SHL) {
						assembly.CALL (Assembly.HELPER_LSHL);

					} else if (type == Operator.BinaryType.SHR) {
						assembly.CALL (Assembly.HELPER_LSAR);

					} else if (type == Operator.BinaryType.SHRUnsigned) {
						assembly.CALL (Assembly.HELPER_LSHR);

					} else {
						throw new Exception ("'" + type + "' not supported.");
					}

					assembly.ADD (R32.ESP, 12);

					this.MovRegisterRegister (loRegister, R32.EAX);
					this.MovRegisterRegister (hiRegister, R32.EDX);

				} else
					throw new Exception ("'" + type + "' is not supported.");

			} else
				throw new Exception ("'" + operand.Operator + "' is not supported.");
		}

		/// <summary>
		/// Movs the register arithmetic.
		/// </summary>
		/// <param name="register">The register.</param>
		/// <param name="operand">The operand.</param>
		private void MovRegisterArithmetic (R32Type register, Arithmetic operand)
		{
			if (operand.Operator is Unary) {
				Unary.UnaryType type = (operand.Operator as Unary).Type;
				Operand first = operand.Operands[0];

				this.MovRegisterOperand (register, first);

				if (type == Operator.UnaryType.Negation) {
					assembly.NEG (register);

				} else if (type == Operator.UnaryType.Not) {
					assembly.NOT (register);

				} else {
					throw new Exception ("'" + type + "' is not supported.");
				}

			} else if (operand.Operator is Binary) {
				Binary.BinaryType type = (operand.Operator as Binary).Type;
				Operand first = operand.Operands[0];
				Operand second = operand.Operands[1];

				this.MovRegisterOperand (register, first);

				// TODO validate the parameter types int32 & int32, int64 & int64.....
				if (type == Binary.BinaryType.Add) {
					if (second is Constant) {
						Int32 value = Convert.ToInt32 ( (second as Constant).Value);

						assembly.ADD (register, (UInt32) value);

					} else if (second.IsRegisterSet) {
						assembly.ADD (register, assembly.GetRegister (second.Register));

					} else {
						//Memory memory = this.GetMemory(second as Identifier);
						this.MovRegisterMemory (R32.EAX, second as Identifier);
						assembly.ADD (register, R32.EAX);
					}

				} else if (type == Binary.BinaryType.Sub) {
					if (second is Constant) {
						UInt32 value = Convert.ToUInt32 (Convert.ToInt32 ( (second as Constant).Value));

						assembly.SUB (register, value);

					} else if (second.IsRegisterSet) {
						assembly.SUB (register, assembly.GetRegister (second.Register));

					} else {
						Memory memory = this.GetMemory (second as Identifier);

						assembly.SUB (register, memory as DWordMemory);
					}

				} else if (type == Binary.BinaryType.Mul) {
					if (second is Constant) {
						UInt32 value = Convert.ToUInt32 (Convert.ToInt32 ( (second as Constant).Value));

						assembly.IMUL (register, value);

					} else if (second.IsRegisterSet) {
						assembly.IMUL (register, assembly.GetRegister (second.Register));

					} else {
						Memory memory = this.GetMemory (second as Identifier);

						assembly.IMUL (register, memory as DWordMemory);
					}

				} else if (type == Binary.BinaryType.Div) {
					this.MovRegisterOperand (R32.EAX, first);
					this.MovRegisterOperand (R32.ECX, second);

					assembly.CDQ();
					assembly.IDIV (R32.ECX);

					assembly.MOV (register, R32.EAX);

				} else if (type == Binary.BinaryType.And) {
					if (second is Constant) {
						UInt32 value = (UInt32) Convert.ToInt32 ( (second as Constant).Value);

						assembly.AND (register, value);

					} else if (second.IsRegisterSet) {
						assembly.AND (register, assembly.GetRegister (second.Register));

					} else {
						Memory memory = this.GetMemory (second as Identifier);

						assembly.AND (register, memory as DWordMemory);
					}

				} else if (type == Binary.BinaryType.Or) {
					if (second is Constant) {
						UInt32 value = Convert.ToUInt32 (Convert.ToInt32 ( (second as Constant).Value));

						assembly.OR (register, value);

					} else if (second.IsRegisterSet) {
						assembly.OR (register, assembly.GetRegister (second.Register));

					} else {
						Memory memory = this.GetMemory (second as Identifier);

						assembly.OR (register, memory as DWordMemory);
					}

				} else if (type == Binary.BinaryType.SHL) {
					this.MovRegisterOperand (R32.ECX, second);

					assembly.SHL__CL (register);

				} else if (type == Binary.BinaryType.SHR) {
					this.MovRegisterOperand (R32.ECX, second);

					assembly.SAR__CL (register);

				} else if (type == Binary.BinaryType.SHRUnsigned) {
					this.MovRegisterOperand (R32.ECX, second);

					assembly.SHR__CL (register);

				} else {
					throw new Exception ("'" + type + "' is not supported.");
				}

			} else {
				throw new Exception ("'" + operand.Operator + "' is not supported.");
			}
		}

		/// <summary>
		/// Movs the register constant.
		/// </summary>
		/// <param name="register">The register.</param>
		/// <param name="constant">The constant.</param>
		private void MovRegisterConstant (R32Type register, UInt32 constant)
		{
			if (constant == 0)
				assembly.XOR (register, register);
			else
				assembly.MOV (register, constant);
		}

		/// <summary>
		/// Movs the register constant.
		/// </summary>
		/// <param name="loRegister">The lo register.</param>
		/// <param name="hiRegister">The hi register.</param>
		/// <param name="constant">The constant.</param>
		private void MovRegisterConstant (R32Type loRegister, R32Type hiRegister, UInt64 constant)
		{
			if (constant == 0) {
				assembly.XOR (loRegister, loRegister);
				assembly.XOR (hiRegister, hiRegister);

			} else {
				assembly.MOV (loRegister, (UInt32) (constant & 0xFFFFFFFF));
				assembly.MOV (hiRegister, (UInt32) (constant >> 32));
			}
		}

		/// <summary>
		/// Handles the assign.
		/// </summary>
		/// <param name="block">The block.</param>
		/// <param name="instruction">The instruction.</param>
		private void HandleAssign (Block block, SharpOS.AOT.IR.Instructions.Instruction instruction)
		{
			SharpOS.AOT.IR.Instructions.Assign assign = (instruction as SharpOS.AOT.IR.Instructions.Assign);

			if (assign.Value is Constant) {
				if (this.IsFourBytes (assign.Assignee)
						|| this.IsEightBytes (assign.Assignee)) {
					if (assign.Assignee.IsRegisterSet)
						this.MovRegisterConstant (assign);

					else
						this.MovMemoryConstant (assign);

				} else
					throw new Exception ("'" + instruction + "' is not supported.");

			} else if (assign.Value is Identifier) {
				if (assign.Assignee.IsRegisterSet
						&& assign.Value.IsRegisterSet) {
					this.MovRegisterRegister (assign);

				} else if (!assign.Assignee.IsRegisterSet
						&& !assign.Value.IsRegisterSet) {
					if (this.IsFourBytes (assign.Assignee)
							|| this.IsEightBytes (assign.Assignee)) {
						this.MovMemoryMemory (assign);

					} else
						throw new Exception ("'" + instruction + "' is not supported.");

				} else if (!assign.Assignee.IsRegisterSet
						&& assign.Value.IsRegisterSet) {
					if (this.IsFourBytes (assign.Assignee)) {
						this.MovMemoryRegister (assign);

					} else 
						throw new Exception ("'" + instruction + "' is not supported.");

				} else if (assign.Assignee.IsRegisterSet
						&& !assign.Value.IsRegisterSet) {
					if (this.IsFourBytes (assign.Assignee))
						this.MovRegisterMemory (assign);

					else
						throw new Exception ("'" + instruction + "' is not supported.");

				} else
					// Just in case....
					throw new Exception ("'" + instruction + "' is not supported.");

			} else if (assign.Value is Arithmetic) {
				if (this.IsFourBytes (assign.Assignee)) {
					if (assign.Assignee.IsRegisterSet)
						this.MovRegisterArithmetic (assembly.GetRegister (assign.Assignee.Register), assign.Value as Arithmetic);

					else {
						R32Type register = assembly.GetSpareRegister();

						this.MovRegisterArithmetic (register, assign.Value as Arithmetic);

						this.MovMemoryRegister (assign.Assignee, register);

						assembly.FreeSpareRegister (register);
					}

				} else if (this.IsEightBytes (assign.Assignee)) {
					this.MovRegisterArithmetic (R32.EAX, R32.EDX, assign.Value as Arithmetic);

					this.MovMemoryRegister (assign.Assignee, R32.EAX, R32.EDX);

				} else
					throw new Exception ("'" + instruction + "' is not supported.");

			} else if (assign.Value is SharpOS.AOT.IR.Operands.Boolean) {
				if (this.IsFourBytes (assign.Assignee)) {
					if (assign.Assignee.IsRegisterSet) {
						this.MovRegisterBoolean (assembly.GetRegister (assign.Assignee.Register), assign.Value as SharpOS.AOT.IR.Operands.Boolean);

					} else {
						R32Type register = assembly.GetSpareRegister();

						this.MovRegisterBoolean (register, assign.Value as SharpOS.AOT.IR.Operands.Boolean);

						this.MovMemoryRegister (assign.Assignee, register);

						assembly.FreeSpareRegister (register);
					}

				} else
					throw new Exception ("'" + instruction + "' is not supported.");

			} else if (assign.Value is SharpOS.AOT.IR.Operands.Call) {
				SharpOS.AOT.IR.Operands.Call call = assign.Value as SharpOS.AOT.IR.Operands.Call;

				string name = call.Method.DeclaringType.FullName + "." + call.Method.Name;

				if (assembly.IsKernelString (name)) {
					this.HandleAssign (block, new Assign (assign.Assignee, call.Operands[0]));

				} else {
					this.HandleCall (block, call);

					if (this.IsFourBytes (assign.Assignee)) {
						this.MovOperandRegister (assign.Assignee, R32.EAX);

					} else if (this.IsEightBytes (assign.Assignee)) {
						this.MovMemoryRegister (assign.Assignee, R32.EAX, R32.EDX);

					} else
						throw new Exception ("'" + instruction + "' is not supported.");
				}

			} else if (assign.Value is SharpOS.AOT.IR.Operands.Miscellaneous) {
				SharpOS.AOT.IR.Operands.Miscellaneous miscellaneous = assign.Value as SharpOS.AOT.IR.Operands.Miscellaneous;

				if (miscellaneous.Operator is SharpOS.AOT.IR.Operators.Miscellaneous
						&& (miscellaneous.Operator as SharpOS.AOT.IR.Operators.Miscellaneous).Type == Operator.MiscellaneousType.Localloc) {

					int size = Convert.ToInt32 ((miscellaneous.Operands [0] as Constant).Value);

					if (size < 1)
						throw new Exception ("'" + instruction + "' has an invalid size value.");

					if (size > 4096)
						throw new Exception ("'" + instruction + "' has an invalid size value. (Bigger than 4096 bytes)");

					if (size % 4 != 0)
						size = ((size / 4) + 1) * 4;

					this.assembly.SUB (R32.ESP, (uint) size);
					this.MovOperandRegister (assign.Assignee, R32.ESP);

				} else
					throw new Exception ("'" + instruction + "' is not supported.");

			} else
				throw new Exception ("'" + instruction + "' is not supported.");
		}

		/// <summary>
		/// Handles the return.
		/// </summary>
		/// <param name="block">The block.</param>
		/// <param name="instruction">The instruction.</param>
		private void HandleReturn (Block block, SharpOS.AOT.IR.Instructions.Return instruction)
		{
			if (instruction.Value != null) {
				if (this.IsFourBytes (instruction.Value)) {
					this.MovRegisterOperand (R32.EAX, instruction.Value);

				} else if (this.IsEightBytes (instruction.Value)) {
					this.MovRegisterMemory (R32.EAX, R32.EDX, instruction.Value as Identifier);

				} else
					throw new Exception ("'" + instruction + "' is not supported.");
			}

			assembly.JMP (method.MethodFullName + "_exit");
		}

		/// <summary>
		/// Movs the register constant.
		/// </summary>
		/// <param name="register">The register.</param>
		/// <param name="operand">The operand.</param>
		private void MovRegisterConstant (R32Type register, Constant operand)
		{
			if (operand.Value.GetType().ToString().Equals ("System.String")) {
				assembly.MOV (register, assembly.AddString (operand.Value.ToString()));

			} else {
				Int32 value = Convert.ToInt32 (operand.Value);
				this.MovRegisterConstant (register, (UInt32) value);
			}
		}

		/// <summary>
		/// Movs the register constant.
		/// </summary>
		/// <param name="assign">The assign.</param>
		private void MovRegisterConstant (Assign assign)
		{
			this.MovRegisterConstant (assembly.GetRegister (assign.Assignee.Register), assign.Value as Constant);
		}

		/// <summary>
		/// Movs the memory constant.
		/// </summary>
		/// <param name="assign">The assign.</param>
		private void MovMemoryConstant (Assign assign)
		{
			if (this.IsFourBytes (assign.Assignee)) {
				Memory memory = this.GetMemory (assign.Assignee);

				Int32 value = Convert.ToInt32 ( (assign.Value as Constant).Value);

				this.MovMemoryConstant (memory, (UInt32) value);

			} else if (this.IsEightBytes (assign.Assignee)) {
				DWordMemory memory = this.GetMemory (assign.Assignee) as DWordMemory;

				Int64 value = Convert.ToInt64 ( (assign.Value as Constant).Value);

				UInt32 loValue = (UInt32) (value & 0xFFFFFFFF);
				UInt32 hiValue = (UInt32) (value >> 32);

				this.MovMemoryConstant (memory, (UInt32) loValue);

				memory = new DWordMemory (memory);
				memory.DisplacementDelta = 4;

				this.MovMemoryConstant (memory, (UInt32) hiValue);

			} else {
				throw new Exception ("'" + assign.ToString() + "' not supported.");
			}
		}

		/// <summary>
		/// Gets the memory.
		/// </summary>
		/// <param name="sizeType">Type of the size.</param>
		/// <param name="_base">The _base.</param>
		/// <param name="scale">The scale.</param>
		/// <param name="displacement">The displacement.</param>
		/// <returns></returns>
		private Memory GetMemory (SharpOS.AOT.IR.Operands.Operand.InternalSizeType sizeType, R32Type _base, byte scale, int displacement)
		{
			return GetMemory (sizeType, _base, scale, displacement, string.Empty);
		}

		/// <summary>
		/// Gets the memory.
		/// </summary>
		/// <param name="sizeType">Type of the size.</param>
		/// <param name="_base">The _base.</param>
		/// <param name="scale">The scale.</param>
		/// <param name="displacement">The displacement.</param>
		/// <param name="label">The label.</param>
		/// <returns></returns>
		private Memory GetMemory (SharpOS.AOT.IR.Operands.Operand.InternalSizeType sizeType, R32Type _base, byte scale, int displacement, string label)
		{
			Memory address = null;

			if (sizeType == Operand.InternalSizeType.I1
					|| sizeType == Operand.InternalSizeType.U1) {
				if (label.Length > 0)
					address = new ByteMemory (label);

				else {
					if (displacement == 0)
						address = new ByteMemory (null, _base, null, scale);

					else
						address = new ByteMemory (null, _base, null, scale, displacement);
				}

			} else if (sizeType == Operand.InternalSizeType.I2
					|| sizeType == Operand.InternalSizeType.U2) {
				if (label.Length > 0) {
					address = new WordMemory (label);

				} else {
					if (displacement == 0)
						address = new WordMemory (null, _base, null, scale);

					else
						address = new WordMemory (null, _base, null, scale, displacement);
				}

			} else if (sizeType == Operand.InternalSizeType.I4
					|| sizeType == Operand.InternalSizeType.U4
					|| sizeType == Operand.InternalSizeType.I
					|| sizeType == Operand.InternalSizeType.U
					|| sizeType == Operand.InternalSizeType.Object) {
				if (label.Length > 0)
					address = new DWordMemory (label);

				else {
					if (displacement == 0)
						address = new DWordMemory (null, _base, null, scale);

					else
						address = new DWordMemory (null, _base, null, scale, displacement);
				}

			} else if (sizeType == Operand.InternalSizeType.I8
					|| sizeType == Operand.InternalSizeType.U8) {
				if (label.Length > 0)
					address = new DWordMemory (label);

				else {
					if (displacement == 0)
						address = new DWordMemory (null, _base, null, scale);

					else
						address = new DWordMemory (null, _base, null, scale, displacement);
				}

			} else
				throw new Exception ("'" + sizeType + "' not supported.");

			return address;
		}

		/// <summary>
		/// Gets the type of the memory.
		/// </summary>
		/// <param name="operand">The operand.</param>
		/// <returns></returns>
		private Memory GetMemoryType (Identifier operand)
		{
			return this.GetMemory (operand, false);
		}

		/// <summary>
		/// Gets the memory.
		/// </summary>
		/// <param name="operand">The operand.</param>
		/// <returns></returns>
		private Memory GetMemory (Identifier operand)
		{
			return this.GetMemory (operand, true);
		}

		/// <summary>
		/// Gets the memory.
		/// </summary>
		/// <param name="operand">The operand.</param>
		/// <param name="emit">if set to <c>true</c> [emit].</param>
		/// <returns></returns>
		private Memory GetMemory (Identifier operand, bool emit)
		{
			Memory address = null;

			if (operand is Field) {
				Field field = operand as Field;

				if (field.Instance != null) {
					this.MovRegisterOperand (R32.ECX, field.Instance);

					address = this.GetMemory (operand.SizeType, R32.ECX, 0, this.assembly.GetFieldOffset (field.Value));

				} else
					address = this.GetMemory (operand.SizeType, null, 0, 0, operand.Value);

			} else if (operand is Reference) {
				Identifier identifier = (operand as Reference).Value as Identifier;
				R32Type register;

				if (identifier.IsRegisterSet)
					register = assembly.GetRegister (identifier.Register);

				else {
					register = assembly.GetSpareRegister();

					if (emit) 
						this.MovRegisterMemory (register, identifier);
				}

				address = this.GetMemory (operand.SizeType, register, 0, 0);

				assembly.FreeSpareRegister (register);

			} else if (operand is Argument) {
				int index = (operand as SharpOS.AOT.IR.Operands.Argument).Index;

				address = this.GetMemory (operand.SizeType, R32.EBP, 0, this.GetArgumentOffset (index) * 4);

			} else if (operand.Stack != int.MinValue) {
				address = this.GetMemory (operand.SizeType, R32.EBP, 0, - ( (3 + operand.Stack) * 4));

			} else
				throw new Exception ("Wrong '" + operand.ToString() + "' Operand.");

			return address;
		}

		/// <summary>
		/// Gets the argument offset.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		private int GetArgumentOffset (int index)
		{
			int i = 0;
			int result = 2;

			foreach (ParameterDefinition parameter in this.method.MethodDefinition.Parameters) {
				Operand.InternalSizeType sizeType = this.method.Engine.GetInternalType (parameter.ParameterType.ToString());

				if (++i == index)
					break;

				result++;

				if (sizeType == Operand.InternalSizeType.I8
						|| sizeType == Operand.InternalSizeType.U8
						|| sizeType == Operand.InternalSizeType.R8)
					result++;
			}

			return result;
		}

		/// <summary>
		/// Movs the memory register.
		/// </summary>
		/// <param name="assign">The assign.</param>
		private void MovMemoryRegister (Assign assign)
		{
			this.MovMemoryRegister (assign.Assignee, assembly.GetRegister (assign.Value.Register));
		}

		/// <summary>
		/// Movs the register memory.
		/// </summary>
		/// <param name="assign">The assign.</param>
		private void MovRegisterMemory (Assign assign)
		{
			this.MovRegisterMemory (assembly.GetRegister (assign.Assignee.Register), assign.Value as Identifier);
		}

		/// <summary>
		/// Movs the memory memory.
		/// </summary>
		/// <param name="assign">The assign.</param>
		private void MovMemoryMemory (Assign assign)
		{
			if (this.IsFourBytes (assign.Assignee)) {
				R32Type register = assembly.GetSpareRegister();

				this.MovRegisterMemory (register, assign.Value as Identifier);

				this.MovMemoryRegister (assign.Assignee, register);

				assembly.FreeSpareRegister (register);

			} else if (this.IsEightBytes (assign.Assignee)) {
				this.MovRegisterMemory (R32.EAX, R32.EDX, assign.Value as Identifier);

				this.MovMemoryRegister (assign.Assignee, R32.EAX, R32.EDX);

			} else
				throw new Exception ("'" + assign + "' not supported.");
		}

		/// <summary>
		/// Movs the memory constant.
		/// </summary>
		/// <param name="memory">The memory.</param>
		/// <param name="constant">The constant.</param>
		private void MovMemoryConstant (Memory memory, UInt32 constant)
		{
			if (constant == 0) {
				R32Type register = assembly.GetSpareRegister();

				assembly.XOR (register, register);

				if (memory is ByteMemory) {
					assembly.MOV (memory as ByteMemory, assembly.Get8BitRegister (register));

				} else if (memory is WordMemory) {
					assembly.MOV (memory as WordMemory, assembly.Get16BitRegister (register));

				} else if (memory is DWordMemory) {
					assembly.MOV (memory as DWordMemory, register);

				} else
					throw new Exception ("'" + memory + "' not supported.");

				assembly.FreeSpareRegister (register);

			} else if (memory is ByteMemory) {
				assembly.MOV (memory as ByteMemory, Convert.ToByte (constant));

			} else if (memory is WordMemory) {
				assembly.MOV (memory as WordMemory, Convert.ToUInt16 (constant));

			} else if (memory is DWordMemory) {
				assembly.MOV (memory as DWordMemory, constant);

			} else
				throw new Exception ("'" + memory + "' not supported.");
		}

		/// <summary>
		/// Movs the register register.
		/// </summary>
		/// <param name="assign">The assign.</param>
		private void MovRegisterRegister (Assign assign)
		{
			this.MovRegisterRegister (assembly.GetRegister (assign.Assignee.Register), assembly.GetRegister (assign.Value.Register));
		}

		/// <summary>
		/// Movs the register register.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="source">The source.</param>
		private void MovRegisterRegister (R32Type target, R32Type source)
		{
			if (target != source)
				assembly.MOV (target, source);
		}

		/// <summary>
		/// Determines whether [is four bytes] [the specified operand].
		/// </summary>
		/// <param name="operand">The operand.</param>
		/// <returns>
		/// 	<c>true</c> if [is four bytes] [the specified operand]; otherwise, <c>false</c>.
		/// </returns>
		private bool IsFourBytes (Operand operand)
		{
			if (operand.SizeType == Operand.InternalSizeType.I
					|| operand.SizeType == Operand.InternalSizeType.U
					|| operand.SizeType == Operand.InternalSizeType.I1
					|| operand.SizeType == Operand.InternalSizeType.U1
					|| operand.SizeType == Operand.InternalSizeType.I2
					|| operand.SizeType == Operand.InternalSizeType.U2
					|| operand.SizeType == Operand.InternalSizeType.I4
					|| operand.SizeType == Operand.InternalSizeType.U4) {
				return true;
			}

			return false;
		}

		/// <summary>
		/// Determines whether [is eight bytes] [the specified operand].
		/// </summary>
		/// <param name="operand">The operand.</param>
		/// <returns>
		/// 	<c>true</c> if [is eight bytes] [the specified operand]; otherwise, <c>false</c>.
		/// </returns>
		private bool IsEightBytes (Operand operand)
		{
			if (operand.SizeType == Operand.InternalSizeType.I8
					|| operand.SizeType == Operand.InternalSizeType.U8)
				return true;

			return false;
		}

		/// <summary>
		/// Determines whether the specified operand is signed.
		/// </summary>
		/// <param name="operand">The operand.</param>
		/// <returns>
		/// 	<c>true</c> if the specified operand is signed; otherwise, <c>false</c>.
		/// </returns>
		private bool IsSigned (Operand operand)
		{
			if (operand.SizeType == Operand.InternalSizeType.I
					|| operand.SizeType == Operand.InternalSizeType.I1
					|| operand.SizeType == Operand.InternalSizeType.I2
					|| operand.SizeType == Operand.InternalSizeType.I4
					|| operand.SizeType == Operand.InternalSizeType.I8)
				return true;

			return false;
		}
	}
}
