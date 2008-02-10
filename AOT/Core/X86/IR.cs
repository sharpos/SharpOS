//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Stanislaw Pitucha <viraptor@gmail.com>
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
		private void Call (SharpOS.AOT.IR.Instructions.Call call)
		{
			if (call.IsSpecialCase) {
				if (this.assembly.IsInstruction (call.Method.Class.TypeFullName))
					this.HandleAssemblyStub (call);
				else
					this.HandleBuiltIns (call);

				return;
			}

			if (call.Method.Class.IsArray
					&& call.Method.SkipProcessing) {
				this.ArrayCalls (call);

				return;
			}

			// TODO add support for call/callvirt/calli/jmp and for tail.
			PushCallParameters (call);

			IR.Operands.Register assignee = call.Def as IR.Operands.Register;
			TypeDefinition returnType = call.Method.ReturnType.ReturnType as TypeDefinition;

			if (returnType != null && returnType.IsValueType) {
				this.assembly.LEA (R32.EAX, this.GetAddress (assignee));
				this.assembly.PUSH (R32.EAX);
			}

			if (call is Callvirt
					&& (call.Method.IsNewSlot
						|| call.Method.IsVirtual)) {
				IR.Operands.Register _this = call.Use [0] as IR.Operands.Register;

				if (_this.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (_this.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (_this)));

				if (call.Method.InterfaceMethodNumber == -1) {
					// Do a normal vtable call
					int address = this.assembly.Engine.VTableSize + this.assembly.IntSize * call.Method.VirtualSlot;

					// Get the Object's VTable
					this.assembly.MOV (R32.EAX, new DWordMemory (null, R32.EAX, null, 0));

					// Call virtual method using the table in the Object's VTable
					this.assembly.CALL (new DWordMemory (null, R32.EAX, null, 0, address));
				} else {
					// Do a IMT lookup call for interface method
					int address = this.assembly.IntSize * call.Method.InterfaceMethodKey + this.assembly.Engine.ObjectSize;

					// Get the Object's VTable
					this.assembly.MOV (R32.EAX, new DWordMemory (null, R32.EAX, null, 0));

					// Get the Object's ITable
					this.assembly.MOV (R32.EAX, new DWordMemory (null, R32.EAX, null, 0, this.assembly.IntSize * 4));

					// IMT key in case call hits a colision resolving stub
					this.assembly.MOV (R32.ECX, (uint) call.Method.InterfaceMethodNumber);

					// Call virtual method using the table in the Object's ITable
					this.assembly.CALL (new DWordMemory (null, R32.EAX, null, 0, address));
				}
			} else
				assembly.CALL (call.Method.AssemblyLabel);

			PopCallParameters (call);

			if (assignee != null) {
				switch (assignee.InternalType) {
				case InternalType.I:
				case InternalType.M:
				case InternalType.O:
				case InternalType.I4:
				case InternalType.SZArray:
				case InternalType.Array:
					if (assignee.IsRegisterSet)
						this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
					else
						this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

					break;

				case InternalType.I8:
					Memory assigneeMemory = this.GetAddress (assignee);
					DWordMemory low = new DWordMemory (assigneeMemory);
					this.assembly.MOV (low, R32.EAX);

					DWordMemory high = new DWordMemory (assigneeMemory);
					high.DisplacementDelta = 4;
					this.assembly.MOV (high, R32.EDX);

					break;

				case InternalType.ValueType:
					// It is already handled above
					break;

				default:
					throw new NotImplementedEngineException ("Call assignee handling.");
				}
			}
		}

		private void PushCallParameters (SharpOS.AOT.IR.Instructions.CallInstruction call)
		{
			for (int i = 0; i < call.Use.Length; i++) {
				Operand operand = call.Use [call.Use.Length - i - 1];

				this.PushOperand (operand);
			}
		}

		private void PopCallParameters (SharpOS.AOT.IR.Instructions.CallInstruction call)
		{
			uint result = 0;

			foreach (ParameterDefinition parameter in call.Method.Parameters)
				result += (uint) this.method.Engine.GetTypeSize (Class.GetTypeFullName (parameter.ParameterType), 4);

			if (call.Method.HasThis)
				result += 4;

			TypeDefinition returnType = call.Method.ReturnType.ReturnType as TypeDefinition;

			// In case the return type is a structure in that case the last parameter that is pushed on the stack
			// is the address to the memory where the result gets copied, and that address has to be pulled from
			// the stack.
			if (returnType != null && returnType.IsValueType)
				result += 4;

			if (result > 0)
				assembly.ADD (R32.ESP, result);
		}

		/// <summary>
		/// Determines whether the given call is marked with a StringAttribute.
		/// </summary>
		/// <param name="call">The call operand.</param>
		/// <returns>
		/// 	<c>true</c> if the method is a String stub; otherwise, <c>false</c>.
		/// </returns>
		private bool IsKernelString (SharpOS.AOT.IR.Instructions.Call call)
		{
			if (!call.Method.IsKernelString)
				return false;

			assembly.UTF7StringEncoding = true;

			StringConstant stringConstant = Operand.GetNonRegister (call.Use [0], typeof (StringConstant)) as StringConstant;

			IR.Operands.Register assignee = call.Def as IR.Operands.Register;

			string resource = assembly.AddString (stringConstant.Value);

			if (assignee.IsRegisterSet)
				this.assembly.MOV (Assembly.GetRegister (assignee.Register), resource);

			else {
				assembly.MOV (R32.EAX, resource);
				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);
			}

			assembly.UTF7StringEncoding = false;

			return true;
		}

		/// <summary>
		/// Determines whether the given call is marked with an AllocAttribute.
		/// </summary>
		/// <param name="call">The call.</param>
		/// <returns>
		/// 	<c>true</c> if the method being called is an Alloc stub; otherwise, <c>false</c>.
		/// </returns>
		private bool IsKernelAlloc (SharpOS.AOT.IR.Instructions.Call call)
		{
			if (!call.Method.IsKernelAlloc)
				return false;

			IntConstant constant = Operand.GetNonRegister (call.Use [0], typeof (IntConstant)) as IntConstant;

			UInt32 size = System.Convert.ToUInt32 (constant.Value);

			if (size == 0)
				throw new EngineException ("The parameter of the '" + typeof (SharpOS.AOT.Attributes.AllocAttribute).ToString () + "' method '" + call.Method.Class.TypeFullName + "." + call.Method.Name + "' is not valid.");

			IR.Operands.Register assignee = call.Def as IR.Operands.Register;

			if (assignee.IsRegisterSet)
				this.assembly.MOV (Assembly.GetRegister (assignee.Register), this.assembly.BSSAlloc (size));

			else {
				this.assembly.MOV (R32.EAX, this.assembly.BSSAlloc (size));

				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);
			}

			return true;
		}

		/// <summary>
		/// Determines if the method being called in <paramref name="call" /> is
		/// marked with a LabelledAllocAttribute.
		/// </summary>
		/// <param name="call">The call operand.</param>
		/// <returns>
		/// 	<c>true</c> if the call is a LabelledAlloc stub; otherwise, <c>false</c>.
		/// </returns>
		private bool IsKernelLabelledAlloc (SharpOS.AOT.IR.Instructions.Call call)
		{
			if (!call.Method.IsKernelLabeledAlloc)
				return false;

			StringConstant stringConstant = Operand.GetNonRegister (call.Use [0], typeof (StringConstant)) as StringConstant;
			IntConstant intConstant = Operand.GetNonRegister (call.Use [1], typeof (IntConstant)) as IntConstant;

			UInt32 size = System.Convert.ToUInt32 (intConstant.Value);

			if (stringConstant.Value.Length == 0
					&& size == 0)
				throw new EngineException ("The parameter of the '" + typeof (SharpOS.AOT.Attributes.LabelledAllocAttribute).ToString () + "' method '" + call.Method.Class.TypeFullName + "." + call.Method.Name + "' is not valid.");

			IR.Operands.Register assignee = call.Def as IR.Operands.Register;

			if (assignee.IsRegisterSet)
				this.assembly.MOV (Assembly.GetRegister (assignee.Register), this.assembly.BSSAlloc (stringConstant.Value, size));

			else {
				this.assembly.MOV (R32.EAX, this.assembly.BSSAlloc (stringConstant.Value, size));

				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);
			}

			return true;
		}

		/// <summary>
		/// Determines whether the method being called is marked with LabelAddressAttribute.
		/// </summary>
		/// <param name="call">The call.</param>
		/// <returns>
		/// 	<c>true</c> if the method is a LabelAddress stub; otherwise, <c>false</c>.
		/// </returns>
		private bool IsKernelLabelAddress (SharpOS.AOT.IR.Instructions.Call call)
		{
			if (!call.Method.IsKernelLabelAddress)
				return false;

			StringConstant stringConstant = Operand.GetNonRegister (call.Use [0], typeof (StringConstant)) as StringConstant;

			if (stringConstant.Value.Length == 0)
				throw new EngineException ("The parameter of the '" + typeof (SharpOS.AOT.Attributes.LabelAddressAttribute).ToString () + "' method '" + call.Method.Class.TypeFullName + "." + call.Method.Name + "' is not valid.");

			IR.Operands.Register assignee = call.Def as IR.Operands.Register;

			if (assignee.IsRegisterSet)
				this.assembly.MOV (Assembly.GetRegister (assignee.Register), stringConstant.Value);

			else {
				this.assembly.MOV (R32.EAX, stringConstant.Value);

				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);
			}

			return true;
		}

		/// <summary>
		/// Determines whether [is kernel object from pointer] [the specified call].
		/// </summary>
		/// <param name="call">The call.</param>
		/// <returns>
		/// 	<c>true</c> if [is kernel object from pointer] [the specified call]; otherwise, <c>false</c>.
		/// </returns>
		private bool IsKernelObjectConversion (SharpOS.AOT.IR.Instructions.Call call)
		{
			if (!call.Method.IsKernelObjectFromPointer && !call.Method.IsKernelPointerFromObject)
				return false;

			IR.Operands.Register value = call.Use [0] as IR.Operands.Register;
			IR.Operands.Register assignee = call.Def as IR.Operands.Register;

			if (value.IsRegisterSet)
				this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
			else
				this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

			if (assignee.IsRegisterSet)
				this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
			else
				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

			return true;
		}

		private void HandleBuiltIns (SharpOS.AOT.IR.Instructions.Call call)
		{
			if (!this.IsKernelString (call)
					&& !this.IsKernelAlloc (call)
					&& !this.IsKernelLabelledAlloc (call)
					&& !this.IsKernelLabelAddress (call)
					&& !this.IsKernelObjectConversion (call))
				throw new EngineException (string.Format ("Unknown Built-In '{0}'. ({1})", call.ToString (), this.method.MethodFullName));
		}

		private Memory GetAssemblyStubMemoryAddress (IR.Instructions.Newobj call)
		{
			if (call.Use.Length == 1) {
				string parameter = (Operand.GetNonRegister (call.Use [0], typeof (StringConstant)) as SharpOS.AOT.IR.Operands.StringConstant).Value;

				if (call.Method.Class.TypeFullName.EndsWith (".Memory")) {
					return new Memory (parameter);

				} else if (call.Method.Class.TypeFullName.EndsWith (".ByteMemory")) {
					return new ByteMemory (parameter);

				} else if (call.Method.Class.TypeFullName.EndsWith (".WordMemory")) {
					return new WordMemory (parameter);

				} else if (call.Method.Class.TypeFullName.EndsWith (".DWordMemory")) {
					return new DWordMemory (parameter);

				} else if (call.Method.Class.TypeFullName.EndsWith (".QWordMemory")) {
					return new QWordMemory (parameter);

				} else if (call.Method.Class.TypeFullName.EndsWith (".TWordMemory")) {
					return new TWordMemory (parameter);

				} else {
					throw new EngineException ("'" + call.Method.Class.TypeFullName + "' is not supported.");
				}

			} else if (call.Use.Length == 2) {
				SegType segment = Seg.GetByID ((Operand.GetNonRegister (call.Use [0], typeof (FieldOperand)) as FieldOperand).ShortFieldTypeName);
				string label = (Operand.GetNonRegister (call.Use [1], typeof (StringConstant)) as SharpOS.AOT.IR.Operands.StringConstant).Value;

				if (call.Method.Class.TypeFullName.EndsWith (".Memory")) {
					return new Memory (segment, label);

				} else if (call.Method.Class.TypeFullName.EndsWith (".ByteMemory")) {
					return new ByteMemory (segment, label);

				} else if (call.Method.Class.TypeFullName.EndsWith (".WordMemory")) {
					return new WordMemory (segment, label);

				} else if (call.Method.Class.TypeFullName.EndsWith (".DWordMemory")) {
					return new DWordMemory (segment, label);

				} else if (call.Method.Class.TypeFullName.EndsWith (".QWordMemory")) {
					return new QWordMemory (segment, label);

				} else if (call.Method.Class.TypeFullName.EndsWith (".TWordMemory")) {
					return new TWordMemory (segment, label);

				} else {
					throw new EngineException ("'" + call.Method.Class.TypeFullName + "' is not supported.");
				}

			} else if (call.Use.Length == 3) {
				SegType segment = Seg.GetByID (Operand.GetNonRegister (call.Use [0], typeof (FieldOperand)));
				R16Type _base = R16.GetByID (Operand.GetNonRegister (call.Use [1], typeof (FieldOperand)));
				R16Type index = R16.GetByID (Operand.GetNonRegister (call.Use [2], typeof (FieldOperand)));

				if (call.Method.Class.TypeFullName.EndsWith (".Memory")) {
					return new Memory (segment, _base, index);

				} else if (call.Method.Class.TypeFullName.EndsWith (".ByteMemory")) {
					return new ByteMemory (segment, _base, index);

				} else if (call.Method.Class.TypeFullName.EndsWith (".WordMemory")) {
					return new WordMemory (segment, _base, index);

				} else if (call.Method.Class.TypeFullName.EndsWith (".DWordMemory")) {
					return new DWordMemory (segment, _base, index);

				} else if (call.Method.Class.TypeFullName.EndsWith (".QWordMemory")) {
					return new QWordMemory (segment, _base, index);

				} else if (call.Method.Class.TypeFullName.EndsWith (".TWordMemory")) {
					return new TWordMemory (segment, _base, index);

				} else {
					throw new EngineException ("'" + call.Method.Class.TypeFullName + "' is not supported.");
				}

			} else if (call.Use.Length == 4) {
				if (call.Method.Parameters [1].ParameterType.FullName.IndexOf ("16") != -1) {
					SegType segment = Seg.GetByID (Operand.GetNonRegister (call.Use [0], typeof (FieldOperand)));
					R16Type _base = R16.GetByID (Operand.GetNonRegister (call.Use [1], typeof (FieldOperand)));
					R16Type index = R16.GetByID (Operand.GetNonRegister (call.Use [2], typeof (FieldOperand)));
					Int16 displacement = System.Convert.ToInt16 ((Operand.GetNonRegister (call.Use [3], typeof (IntConstant)) as IntConstant).Value);

					if (call.Method.Class.TypeFullName.EndsWith (".Memory")) {
						return new Memory (segment, _base, index, displacement);

					} else if (call.Method.Class.TypeFullName.EndsWith (".ByteMemory")) {
						return new ByteMemory (segment, _base, index, displacement);

					} else if (call.Method.Class.TypeFullName.EndsWith (".WordMemory")) {
						return new WordMemory (segment, _base, index, displacement);

					} else if (call.Method.Class.TypeFullName.EndsWith (".DWordMemory")) {
						return new DWordMemory (segment, _base, index, displacement);

					} else if (call.Method.Class.TypeFullName.EndsWith (".QWordMemory")) {
						return new QWordMemory (segment, _base, index, displacement);

					} else if (call.Method.Class.TypeFullName.EndsWith (".TWordMemory")) {
						return new TWordMemory (segment, _base, index, displacement);

					} else {
						throw new EngineException ("'" + call.Method.Class.TypeFullName + "' is not supported.");
					}

				} else {
					SegType segment = Seg.GetByID (Operand.GetNonRegister (call.Use [0], typeof (FieldOperand)));
					R32Type _base = R32.GetByID (Operand.GetNonRegister (call.Use [1], typeof (FieldOperand)));
					R32Type index = R32.GetByID (Operand.GetNonRegister (call.Use [2], typeof (FieldOperand)));
					Byte scale = System.Convert.ToByte ((Operand.GetNonRegister (call.Use [3], typeof (IntConstant)) as IntConstant).Value);

					if (call.Method.Class.TypeFullName.EndsWith (".Memory")) {
						return new Memory (segment, _base, index, scale);

					} else if (call.Method.Class.TypeFullName.EndsWith (".ByteMemory")) {
						return new ByteMemory (segment, _base, index, scale);

					} else if (call.Method.Class.TypeFullName.EndsWith (".WordMemory")) {
						return new WordMemory (segment, _base, index, scale);

					} else if (call.Method.Class.TypeFullName.EndsWith (".DWordMemory")) {
						return new DWordMemory (segment, _base, index, scale);

					} else if (call.Method.Class.TypeFullName.EndsWith (".QWordMemory")) {
						return new QWordMemory (segment, _base, index, scale);

					} else if (call.Method.Class.TypeFullName.EndsWith (".TWordMemory")) {
						return new TWordMemory (segment, _base, index, scale);

					} else {
						throw new EngineException ("'" + call.Method.Class.TypeFullName + "' is not supported.");
					}
				}

			} else if (call.Use.Length == 5) {
				SegType segment = Seg.GetByID (Operand.GetNonRegister (call.Use [0], typeof (FieldOperand)));
				R32Type _base = R32.GetByID (Operand.GetNonRegister (call.Use [1], typeof (FieldOperand)));
				R32Type index = R32.GetByID (Operand.GetNonRegister (call.Use [2], typeof (FieldOperand)));
				Byte scale = System.Convert.ToByte ((Operand.GetNonRegister (call.Use [3], typeof (IntConstant)) as IntConstant).Value);
				Int32 displacement = System.Convert.ToInt32 ((Operand.GetNonRegister (call.Use [4], typeof (IntConstant)) as IntConstant).Value);

				if (call.Method.Class.TypeFullName.EndsWith (".Memory")) {
					return new Memory (segment, _base, index, scale, displacement);

				} else if (call.Method.Class.TypeFullName.EndsWith (".ByteMemory")) {
					return new ByteMemory (segment, _base, index, scale, displacement);

				} else if (call.Method.Class.TypeFullName.EndsWith (".WordMemory")) {
					return new WordMemory (segment, _base, index, scale, displacement);

				} else if (call.Method.Class.TypeFullName.EndsWith (".DWordMemory")) {
					return new DWordMemory (segment, _base, index, scale, displacement);

				} else if (call.Method.Class.TypeFullName.EndsWith (".QWordMemory")) {
					return new QWordMemory (segment, _base, index, scale, displacement);

				} else if (call.Method.Class.TypeFullName.EndsWith (".TWordMemory")) {
					return new TWordMemory (segment, _base, index, scale, displacement);

				} else {
					throw new EngineException ("'" + call.Method.Class.TypeFullName + "' is not supported.");
				}

			} else
				throw new EngineException ("'" + call.Method.Name + "' has wrong parameters.");
		}

		/// <summary>
		/// This handles the Asm.XXX calls.
		/// </summary>
		/// <param name="block">The block.</param>
		/// <param name="instruction">The instruction.</param>
		private void HandleAssemblyStub (SharpOS.AOT.IR.Instructions.Call call)
		{
			string parameterTypes = string.Empty;
			object [] operands = new object [call.Method.Parameters.Count];

			for (int i = 0; i < call.Method.Parameters.Count; i++) {
				ParameterDefinition parameter = call.Method.Parameters [i];

				if (parameterTypes.Length > 0)
					parameterTypes += " ";

				IR.Operands.Operand operand = call.Use [i];

				while (operand is IR.Operands.Register
						&& !((operand as IR.Operands.Register).Parent is Newobj)) {
					IR.Operands.Register register = operand as IR.Operands.Register;

					if (register.Parent.Use.Length != 1)
						throw new EngineException (string.Format ("Could not process '{0}'. ({1})", register.Parent.ToString (), this.method.MethodFullName));

					operand = register.Parent.Use [0];
				}

				if (operand is IR.Operands.Register
						&& (operand as IR.Operands.Register).Parent is Newobj) {
					Memory memory = this.GetAssemblyStubMemoryAddress ((operand as IR.Operands.Register).Parent as Newobj);
					parameterTypes += memory.GetType ().Name;
					operands [i] = memory;

				} else if (parameter.ParameterType is PointerType) {
					if (operand is SharpOS.AOT.IR.Operands.Identifier) {
						IR.Operands.Identifier identifier = operand as SharpOS.AOT.IR.Operands.Identifier;

						if (identifier.IsRegisterSet) {
							Register register = Assembly.GetRegister (identifier.Register);
							parameterTypes += register.GetType ().Name;
							operands [i] = register;

						} else {
							Memory memory = this.GetMemory (identifier);
							parameterTypes += memory.GetType ().Name;
							operands [i] = memory;
						}
					}

				} else if (operand is FieldOperand) {
					FieldOperand field = operand as FieldOperand;
					MemberReference memberReference = field.Field.FieldDefinition;
					parameterTypes += field.ShortFieldTypeName;
					operands [i] = memberReference.Name;

				} else if (operand is Constant) {
					parameterTypes += parameter.ParameterType.Name;
					operands [i] = operand;

				} else
					throw new EngineException (string.Format ("Could not process '{0}'. ({1})", call.ToString (), this.method.MethodFullName));
			}

			parameterTypes = call.Method.Name + " " + parameterTypes;

			assembly.GetAssemblyInstruction (call, operands, parameterTypes.Trim ());
		}

		/// <summary>
		/// Handles the return.
		/// </summary>
		/// <param name="block">The block.</param>
		/// <param name="instruction">The instruction.</param>
		private void Return (SharpOS.AOT.IR.Instructions.Return instruction)
		{
			if (instruction.Use != null) {
				IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;

				switch (value.InternalType) {
				case InternalType.I:
				case InternalType.M:
				case InternalType.O:
				case InternalType.I4:
				case InternalType.SZArray:
				case InternalType.Array:
					if (value.IsRegisterSet)
						this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
					else
						this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

					break;

				case InternalType.I8:
					Memory assigneeMemory = this.GetAddress (value);
					DWordMemory low = new DWordMemory (assigneeMemory);
					this.assembly.MOV (R32.EAX, low);

					DWordMemory high = new DWordMemory (assigneeMemory);
					high.DisplacementDelta = 4;
					this.assembly.MOV (R32.EDX, high);

					break;

				case InternalType.ValueType:
					TypeDefinition returnType = instruction.Block.Method.MethodDefinition.ReturnType.ReturnType as TypeDefinition;

					int size = this.method.Engine.GetTypeSize (returnType.FullName, 4) / 4;

					this.assembly.PUSH (R32.ECX);
					this.assembly.PUSH (R32.ESI);
					this.assembly.PUSH (R32.EDI);

					if (value.IsRegisterSet)
						this.assembly.MOV (R32.ESI, Assembly.GetRegister (value.Register));
					else
						this.assembly.LEA (R32.ESI, new DWordMemory (this.GetAddress (value)));

					this.assembly.MOV (R32.EDI, new DWordMemory (null, R32.EBP, null, 0, 8));

					this.assembly.MOV (R32.ECX, (uint) size);

					this.assembly.CLD ();
					this.assembly.REP ();
					this.assembly.MOVSD ();

					this.assembly.POP (R32.EDI);
					this.assembly.POP (R32.ESI);
					this.assembly.POP (R32.ECX);

					break;

				default:
					throw new NotImplementedEngineException ("'" + instruction + "' is not supported.");
				}
			}

			assembly.JMP (method.MethodFullName + " exit");
		}

		private void Initialize (IR.Instructions.Initialize initialize)
		{
			this.Initialize (initialize.Use [0] as IR.Operands.Local);
		}

		private void Initialize (IR.Operands.Identifier identifier)
		{
			int size = this.method.Engine.GetTypeSize (identifier.TypeFullName, 4) / 4;

			if (size == 1) {
				this.assembly.XOR (R32.EAX, R32.EAX);

				if (identifier.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (identifier.Register), R32.EAX);
				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (identifier)), R32.EAX);

			} else {
				this.assembly.PUSH (R32.ECX);
				this.assembly.PUSH (R32.EDI);

				this.assembly.XOR (R32.EAX, R32.EAX);

				if (identifier.IsRegisterSet)
					this.assembly.MOV (R32.EDI, Assembly.GetRegister (identifier.Register));
				else
					this.assembly.LEA (R32.EDI, new DWordMemory (this.GetAddress (identifier)));

				this.assembly.MOV (R32.ECX, (uint) size);

				this.assembly.CLD ();
				this.assembly.REP ();
				this.assembly.STOSD ();

				this.assembly.POP (R32.EDI);
				this.assembly.POP (R32.ECX);
			}
		}

		private void Ldc (IR.Instructions.Ldc instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;

			switch (assignee.InternalType) {
			case InternalType.I4:
				uint intConstant = (uint) (instruction.Use [0] as IntConstant).Value;

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), intConstant);

				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), intConstant);

				break;

			case InternalType.I8:
				ulong longConstant = (ulong) (instruction.Use [0] as LongConstant).Value;
				uint hiValue = (uint) (longConstant >> 32);
				uint loValue = (uint) (longConstant & 0xFFFFFFFF);

				Memory address = this.GetAddress (assignee);
				address.DisplacementDelta = 4;

				this.assembly.MOV (new DWordMemory (address), hiValue);
				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), loValue);

				break;

			default:
				throw new NotImplementedEngineException ("LDC of " + assignee.InternalType + " not supported yet");
			}
		}

		private void Ldind (IR.Instructions.Ldind instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Register register = instruction.Use [0] as IR.Operands.Register;

			if (register.IsRegisterSet)
				this.assembly.MOV (R32.EAX, Assembly.GetRegister (register.Register));

			else
				this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (register)));

			switch (instruction.InternalType) {
			case InternalType.I1:
				this.assembly.MOVSX (R32.EAX, new ByteMemory (null, R32.EAX, null, 0));

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);

				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.U1:
				this.assembly.MOVZX (R32.EAX, new ByteMemory (null, R32.EAX, null, 0));

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);

				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.I2:
				this.assembly.MOVSX (R32.EAX, new WordMemory (null, R32.EAX, null, 0));

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);

				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.U2:
				this.assembly.MOVZX (R32.EAX, new WordMemory (null, R32.EAX, null, 0));

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);

				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.O:
			case InternalType.I:
			case InternalType.I4:
			case InternalType.U4:
			case InternalType.SZArray:
			case InternalType.Array:
				this.assembly.MOV (R32.EAX, new DWordMemory (null, R32.EAX, null, 0));

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);

				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.I8:
			case InternalType.U8:
				Memory address = this.GetAddress (assignee);
				address.DisplacementDelta = 4;

				this.assembly.MOV (R32.EDX, R32.EAX);
				this.assembly.ADD (R32.EDX, 4);

				this.assembly.MOV (R32.EAX, new DWordMemory (null, R32.EAX, null, 0));
				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				this.assembly.MOV (R32.EAX, new DWordMemory (null, R32.EDX, null, 0));
				this.assembly.MOV (new DWordMemory (address), R32.EAX);

				break;

			case InternalType.R4:
			case InternalType.R8:
			case InternalType.F:
			case InternalType.ValueType:
			case InternalType.M:
			case InternalType.U:
			case InternalType.TypedReference:
			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void Load (IR.Operands.Register assignee, InternalType sourceType, Memory memory)
		{
			switch (sourceType) {
			case InternalType.I1:
				this.assembly.MOVSX (R32.EAX, new ByteMemory (memory));

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);

				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.U1:
				this.assembly.MOVZX (R32.EAX, new ByteMemory (memory));

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);

				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.I2:
				this.assembly.MOVSX (R32.EAX, new WordMemory (memory));

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);

				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.U2:
				this.assembly.MOVZX (R32.EAX, new WordMemory (memory));

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);

				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.I4:
			case InternalType.U4:
			case InternalType.I:
			case InternalType.U:
			case InternalType.O:
			case InternalType.M:
			case InternalType.SZArray:
			case InternalType.Array:
				this.assembly.MOV (R32.EAX, new DWordMemory (memory));

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);

				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.I8:
			case InternalType.U8:
				DWordMemory source = new DWordMemory (memory);
				source.DisplacementDelta = 4;

				Memory destination = this.GetAddress (assignee);
				destination.DisplacementDelta = 4;

				this.assembly.MOV (R32.EAX, new DWordMemory (memory));
				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				this.assembly.MOV (R32.EAX, new DWordMemory (source));
				this.assembly.MOV (new DWordMemory (destination), R32.EAX);
				break;

			case InternalType.ValueType:
				uint size = (uint) assignee.Type.Size;

				if (size == 4) {
					this.assembly.MOV (R32.EAX, new DWordMemory (memory));

					if (assignee.IsRegisterSet)
						this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
					else
						this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				} else {
					this.assembly.PUSH (R32.ECX);
					this.assembly.PUSH (R32.ESI);
					this.assembly.PUSH (R32.EDI);

					this.assembly.LEA (R32.ESI, new DWordMemory (memory));

					if (assignee.IsRegisterSet)
						this.assembly.MOV (R32.EDI, Assembly.GetRegister (assignee.Register));
					else
						this.assembly.LEA (R32.EDI, new DWordMemory (this.GetAddress (assignee)));

					this.assembly.MOV (R32.ECX, size);

					this.assembly.CLD ();
					this.assembly.REP ();
					this.assembly.MOVSB ();

					this.assembly.POP (R32.EDI);
					this.assembly.POP (R32.ESI);
					this.assembly.POP (R32.ECX);
				}

				break;

			case InternalType.R4:
			case InternalType.R8:
			case InternalType.F:
			case InternalType.TypedReference:
			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void Save (string typeName, InternalType destinationType, Memory memory, IR.Operands.Register value)
		{
			switch (destinationType) {
			case InternalType.I1:
				if (value.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));

				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

				this.assembly.MOV (new ByteMemory (memory), R8.AL);

				break;

			case InternalType.U1:
				if (value.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));

				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

				this.assembly.MOV (new ByteMemory (memory), R8.AL);

				break;

			case InternalType.I2:
				if (value.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));

				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

				this.assembly.MOV (new WordMemory (memory), R16.AX);

				break;

			case InternalType.U2:
				if (value.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));

				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

				this.assembly.MOV (new WordMemory (memory), R16.AX);

				break;

			case InternalType.I4:
			case InternalType.U4:
			case InternalType.I:
			case InternalType.U:
			case InternalType.O:
			case InternalType.SZArray:
			case InternalType.Array:
				if (value.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));

				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

				this.assembly.MOV (new DWordMemory (memory), R32.EAX);

				break;

			case InternalType.I8:
			case InternalType.U8:
				Memory source = this.GetAddress (value);
				source.DisplacementDelta = 4;

				DWordMemory destination = new DWordMemory (memory);
				destination.DisplacementDelta = 4;

				this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));
				this.assembly.MOV (new DWordMemory (memory), R32.EAX);

				this.assembly.MOV (R32.EAX, new DWordMemory (source));
				this.assembly.MOV (new DWordMemory (destination), R32.EAX);
				break;

			case InternalType.ValueType:
				uint size = (uint) this.method.Engine.GetTypeSize (typeName, 4);

				if (size == 4) {
					if (value.IsRegisterSet)
						this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
					else
						this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

					this.assembly.MOV (new DWordMemory (memory), R32.EAX);

				} else {
					this.assembly.PUSH (R32.ECX);
					this.assembly.PUSH (R32.ESI);
					this.assembly.PUSH (R32.EDI);

					if (value.IsRegisterSet)
						this.assembly.MOV (R32.ESI, Assembly.GetRegister (value.Register));
					else
						this.assembly.LEA (R32.ESI, new DWordMemory (this.GetAddress (value)));

					this.assembly.LEA (R32.EDI, new DWordMemory (memory));

					this.assembly.MOV (R32.ECX, size);

					this.assembly.CLD ();
					this.assembly.REP ();
					this.assembly.MOVSB ();

					this.assembly.POP (R32.EDI);
					this.assembly.POP (R32.ESI);
					this.assembly.POP (R32.ECX);
				}
				break;

			case InternalType.R4:
			case InternalType.R8:
			case InternalType.F:
			case InternalType.M:
			case InternalType.TypedReference:
			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void Ldloc (IR.Instructions.Ldloc instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Local local = instruction.Use [0] as IR.Operands.Local;

			this.Load (assignee, local.InternalType, this.GetAddress (local));
		}

		private void Ldloca (IR.Instructions.Ldloca instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			Memory memory = new Memory (this.GetAddress (instruction.Use [0] as Local));

			if (assignee.IsRegisterSet) {
				this.assembly.LEA (Assembly.GetRegister (assignee.Register), memory);

			} else {
				this.assembly.LEA (R32.EAX, memory);

				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);
			}
		}

		private void Ldarg (IR.Instructions.Ldarg instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Argument argument = instruction.Use [0] as IR.Operands.Argument;

			this.Load (assignee, argument.InternalType, this.GetAddress (argument));
		}

		private void Ldarga (IR.Instructions.Ldarga instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			Memory memory = new Memory (this.GetAddress (instruction.Use [0] as Argument));

			if (assignee.IsRegisterSet) {
				this.assembly.LEA (Assembly.GetRegister (assignee.Register), memory);

			} else {
				this.assembly.LEA (R32.EAX, memory);

				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);
			}
		}

		private void Ldfld (IR.Instructions.Ldfld instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.FieldOperand field = instruction.Use [0] as IR.Operands.FieldOperand;

			this.Load (assignee, field.InternalType, this.GetAddress (field));
		}

		private void Ldsfld (IR.Instructions.Ldsfld instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.FieldOperand field = instruction.Use [0] as IR.Operands.FieldOperand;

			this.Load (assignee, field.InternalType, this.GetAddress (field));
		}

		private void Ldflda (IR.Instructions.Ldflda instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			Memory memory = new Memory (this.GetAddress (instruction.Use [0] as FieldOperand));

			if (assignee.IsRegisterSet) {
				this.assembly.LEA (Assembly.GetRegister (assignee.Register), memory);

			} else {
				this.assembly.LEA (R32.EAX, memory);

				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);
			}
		}

		private void Ldsflda (IR.Instructions.Ldsflda instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			Memory memory = new Memory (this.GetAddress (instruction.Use [0] as FieldOperand));

			if (assignee.IsRegisterSet) {
				this.assembly.LEA (Assembly.GetRegister (assignee.Register), memory);

			} else {
				this.assembly.LEA (R32.EAX, memory);

				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);
			}
		}

		private void Localloc (IR.Instructions.Localloc instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Register size = instruction.Use [0] as IR.Operands.Register;

			if (size.IsRegisterSet)
				this.assembly.MOV (R32.EAX, Assembly.GetRegister (size.Register));
			else
				this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (size)));

			// TODO: verify size
			this.assembly.SUB (R32.ESP, R32.EAX);

			if (assignee.IsRegisterSet)
				this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.ESP);
			else
				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.ESP);
		}

		private void Ldstr (IR.Instructions.Ldstr instruction)
		{
			// TODO it should create an object and copy the string data to that object

			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			StringConstant stringConstant = instruction.Use [0] as StringConstant;

			string resource = assembly.AddString (stringConstant.Value);

			if (assignee.IsRegisterSet)
				this.assembly.MOV (Assembly.GetRegister (assignee.Register), resource);

			else {
				assembly.MOV (R32.EAX, resource);
				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);
			}
		}

		private void Ldnull (IR.Instructions.Ldnull instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;

			if (assignee.IsRegisterSet)
				this.assembly.MOV (Assembly.GetRegister (assignee.Register), 0);

			else
				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), 0);
		}

		private void Stloc (IR.Instructions.Stloc instruction)
		{
			IR.Operands.Local assignee = instruction.Def as IR.Operands.Local;
			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;

			this.Save (assignee.Type.ToString (), assignee.InternalType, this.GetAddress (assignee), value);
		}

		private void Starg (IR.Instructions.Starg instruction)
		{
			IR.Operands.Argument assignee = instruction.Def as IR.Operands.Argument;
			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;

			this.Save (assignee.Type.ToString (), assignee.InternalType, this.GetAddress (assignee), value);
		}

		private void Stind (IR.Instructions.Stind instruction)
		{
			IR.Operands.Register address = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register value = instruction.Use [1] as IR.Operands.Register;

			if (address.IsRegisterSet)
				this.assembly.MOV (R32.EAX, Assembly.GetRegister (address.Register));

			else
				this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (address)));

			switch (instruction.InternalType) {
			case InternalType.I1:
				if (value.IsRegisterSet)
					this.assembly.MOV (R32.EDX, Assembly.GetRegister (value.Register));

				else
					this.assembly.MOV (R32.EDX, new DWordMemory (this.GetAddress (value)));

				this.assembly.MOV (new ByteMemory (null, R32.EAX, null, 0), R8.DL);

				break;

			case InternalType.I2:
				if (value.IsRegisterSet)
					this.assembly.MOV (R32.EDX, Assembly.GetRegister (value.Register));

				else
					this.assembly.MOV (R32.EDX, new DWordMemory (this.GetAddress (value)));

				this.assembly.MOV (new WordMemory (null, R32.EAX, null, 0), R16.DX);

				break;

			case InternalType.I:
			case InternalType.M:
			case InternalType.O:
			case InternalType.I4:
			case InternalType.SZArray:
			case InternalType.Array:
				if (value.IsRegisterSet)
					this.assembly.MOV (R32.EDX, Assembly.GetRegister (value.Register));

				else
					this.assembly.MOV (R32.EDX, new DWordMemory (this.GetAddress (value)));

				this.assembly.MOV (new DWordMemory (null, R32.EAX, null, 0), R32.EDX);

				break;

			case InternalType.I8:
				Memory source = this.GetAddress (value);
				source.DisplacementDelta = 4;

				this.assembly.MOV (R32.EDX, R32.EAX);
				this.assembly.ADD (R32.EDX, 4);

				this.assembly.MOV (R32.ECX, new DWordMemory (this.GetAddress (value)));
				this.assembly.MOV (new DWordMemory (null, R32.EAX, null, 0), R32.ECX);

				this.assembly.MOV (R32.ECX, new DWordMemory (source));
				this.assembly.MOV (new DWordMemory (null, R32.EDX, null, 0), R32.ECX);

				break;

			case InternalType.R4:
			case InternalType.R8:
			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void Stfld (IR.Instructions.Stfld instruction)
		{
			IR.Operands.FieldOperand assignee = instruction.Use [0] as IR.Operands.FieldOperand;
			IR.Operands.Register value = instruction.Use [1] as IR.Operands.Register;

			this.Save (assignee.Field.FieldDefinition.ToString (), assignee.InternalType, this.GetAddress (assignee), value);
		}

		private void Stsfld (IR.Instructions.Stsfld instruction)
		{
			IR.Operands.FieldOperand assignee = instruction.Use [0] as IR.Operands.FieldOperand;
			IR.Operands.Register value = instruction.Use [1] as IR.Operands.Register;

			this.Save (assignee.Field.FieldDefinition.ToString (), assignee.InternalType, this.GetAddress (assignee), value);
		}

		private void Convert (IR.Instructions.Convert instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;

			switch (value.InternalType) {
			case InternalType.M:
			case InternalType.I:
			case InternalType.U:
			case InternalType.I4:
				switch (instruction.ConvertType) {
				case SharpOS.AOT.IR.Instructions.Convert.Type.Conv_I1:
				case SharpOS.AOT.IR.Instructions.Convert.Type.Conv_U1:
					if (value.IsRegisterSet)
						this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
					else
						this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

					this.assembly.AND (R32.EAX, (uint) 0xFF);

					if (assignee.IsRegisterSet)
						this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
					else
						this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);
					break;

				case SharpOS.AOT.IR.Instructions.Convert.Type.Conv_I2:
				case SharpOS.AOT.IR.Instructions.Convert.Type.Conv_U2:
					if (value.IsRegisterSet)
						this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
					else
						this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

					this.assembly.AND (R32.EAX, (uint) 0xFFFF);

					if (assignee.IsRegisterSet)
						this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
					else
						this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);
					break;

				case SharpOS.AOT.IR.Instructions.Convert.Type.Conv_I:
				case SharpOS.AOT.IR.Instructions.Convert.Type.Conv_I4:
				case SharpOS.AOT.IR.Instructions.Convert.Type.Conv_U:
				case SharpOS.AOT.IR.Instructions.Convert.Type.Conv_U4:
					if (value.IsRegisterSet)
						this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
					else
						this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

					if (assignee.IsRegisterSet)
						this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
					else
						this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

					break;

				case SharpOS.AOT.IR.Instructions.Convert.Type.Conv_I8:
				case SharpOS.AOT.IR.Instructions.Convert.Type.Conv_U8:
					if (value.IsRegisterSet)
						this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
					else
						this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

					Memory memory = this.GetAddress (assignee);
					DWordMemory low = new DWordMemory (memory);
					this.assembly.MOV (low, R32.EAX);

					DWordMemory high = new DWordMemory (memory);
					high.DisplacementDelta = 4;
					this.assembly.XOR (R32.EAX, R32.EAX);
					this.assembly.MOV (high, R32.EAX);

					break;

				default:
					throw new NotImplementedEngineException ("The conversion from " + value.InternalType +
						" to " + instruction.ConvertType + " is not yet supported.");
				}

				break;

			case InternalType.I8:
				switch (instruction.ConvertType) {
				case SharpOS.AOT.IR.Instructions.Convert.Type.Conv_I:
				case SharpOS.AOT.IR.Instructions.Convert.Type.Conv_U:
				case SharpOS.AOT.IR.Instructions.Convert.Type.Conv_I4:
				case SharpOS.AOT.IR.Instructions.Convert.Type.Conv_U4:
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

					if (assignee.IsRegisterSet)
						this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
					else
						this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

					break;
				case SharpOS.AOT.IR.Instructions.Convert.Type.Conv_Ovf_I:
					string exceptLabel = this.assembly.GetCMPLabel;
					string okLabel = this.assembly.GetCMPLabel;
					DWordMemory upper =
						new DWordMemory (this.GetAddress (value));

					upper.DisplacementDelta = 4;

					// Check if it will overflow

					this.assembly.MOV (R32.EAX, upper);
					this.assembly.CMP (R32.EAX, 0);
					this.assembly.JNE (exceptLabel);

					// Handle the conversion

					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

					if (assignee.IsRegisterSet)
						this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
					else
						this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

					this.assembly.JMP (okLabel);

					// Conversion overflows, throw exception.

					this.assembly.LABEL (exceptLabel);
					this.assembly.CALL (this.assembly.Engine.OverflowHandler.AssemblyLabel);

					this.assembly.LABEL (okLabel);

					break;
				default:
					throw new NotImplementedEngineException ("The conversion from " + value.InternalType +
						" to " + instruction.ConvertType + " is not yet supported.");
				}

				break;

			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void Jump (IR.Instructions.Jump instruction)
		{
			assembly.JMP (this.GetLabel (instruction.Block.Outs [0]));
		}

		private void Branch (IR.Instructions.Branch instruction)
		{
			string okLabel = this.GetLabel (instruction.Block.Outs [0]);
			string errorLabel = this.GetLabel (instruction.Block.Outs [1]);

			IR.Operands.Register first = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register second = instruction.Use [1] as IR.Operands.Register;

			switch (first.InternalType) {
			case InternalType.I:
			case InternalType.O:
			case InternalType.M:
			case InternalType.I4:
				if (first.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (first.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (first)));

				if (second.IsRegisterSet)
					this.assembly.MOV (R32.EDX, Assembly.GetRegister (second.Register));
				else
					this.assembly.MOV (R32.EDX, new DWordMemory (this.GetAddress (second)));

				this.assembly.CMP (R32.EAX, R32.EDX);

				this.RelationalTypeCMP (instruction.RelationalType, okLabel);

				break;

			case InternalType.I8:
				DWordMemory firstAddress = new DWordMemory (this.GetAddress (first));
				firstAddress.DisplacementDelta = 4;

				DWordMemory secondAddress = new DWordMemory (this.GetAddress (second));
				secondAddress.DisplacementDelta = 4;

				this.assembly.MOV (R32.EAX, firstAddress);
				this.assembly.MOV (R32.EDX, secondAddress);
				this.assembly.CMP (R32.EAX, R32.EDX);

				RelationalTypeCMP (instruction.RelationalType, okLabel, errorLabel);

				this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (first)));
				this.assembly.MOV (R32.EDX, new DWordMemory (this.GetAddress (second)));
				this.assembly.CMP (R32.EAX, R32.EDX);

				RelationalTypeCMP (instruction.RelationalType, okLabel);

				break;

			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void SimpleBranch (IR.Instructions.SimpleBranch instruction)
		{
			string label = this.GetLabel (instruction.Block.Outs [0]);
			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;

			if (value.IsRegisterSet)
				this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));

			else
				this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

			this.assembly.TEST (R32.EAX, R32.EAX);

			if (instruction.SimpleBranchType == IR.Instructions.SimpleBranch.Type.True)
				this.assembly.JNE (label);

			else if (instruction.SimpleBranchType == IR.Instructions.SimpleBranch.Type.False)
				this.assembly.JE (label);

			else
				throw new NotImplementedEngineException ();
		}

		private void ConditionCheck (IR.Instructions.ConditionCheck instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Register first = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register second = instruction.Use [1] as IR.Operands.Register;

			string errorLabel = assembly.GetCMPLabel;
			string okLabel = assembly.GetCMPLabel;
			string endLabel = assembly.GetCMPLabel;

			switch (first.InternalType) {
			case InternalType.O:
			case InternalType.I:
			case InternalType.I4:
			case InternalType.SZArray:
			case InternalType.Array:
				if (first.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (first.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (first)));

				if (second.IsRegisterSet)
					this.assembly.MOV (R32.EDX, Assembly.GetRegister (second.Register));
				else
					this.assembly.MOV (R32.EDX, new DWordMemory (this.GetAddress (second)));

				this.assembly.CMP (R32.EAX, R32.EDX);

				RelationalTypeCMP (instruction.RelationalType, okLabel);

				break;

			case InternalType.I8:
				DWordMemory firstAddress = new DWordMemory (this.GetAddress (first));
				firstAddress.DisplacementDelta = 4;

				DWordMemory secondAddress = new DWordMemory (this.GetAddress (second));
				secondAddress.DisplacementDelta = 4;

				this.assembly.MOV (R32.EAX, firstAddress);
				this.assembly.MOV (R32.EDX, secondAddress);
				this.assembly.CMP (R32.EAX, R32.EDX);

				RelationalTypeCMP (instruction.RelationalType, okLabel, errorLabel);

				this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (first)));
				this.assembly.MOV (R32.EDX, new DWordMemory (this.GetAddress (second)));
				this.assembly.CMP (R32.EAX, R32.EDX);

				RelationalTypeCMP (instruction.RelationalType, okLabel);

				break;

			default:
				throw new NotImplementedEngineException ();
			}

			assembly.LABEL (errorLabel);
			this.assembly.XOR (R32.ECX, R32.ECX);
			this.assembly.JMP (endLabel);

			assembly.LABEL (okLabel);
			this.assembly.XOR (R32.ECX, R32.ECX);
			this.assembly.INC (R32.ECX);

			this.assembly.LABEL (endLabel);

			if (assignee.IsRegisterSet)
				this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.ECX);
			else
				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.ECX);
		}

		private void Dup (IR.Instructions.Dup instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;

			switch (assignee.InternalType) {
			case InternalType.I:
			case InternalType.O:
			case InternalType.M:
			case InternalType.I4:
			case InternalType.SZArray:
			case InternalType.Array:
				if (value.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.I8:
				Memory sourceMemory = this.GetAddress (value);
				DWordMemory source = new DWordMemory (sourceMemory);
				source.DisplacementDelta = 4;

				Memory destinationMemory = this.GetAddress (assignee);
				DWordMemory destination = new DWordMemory (destinationMemory);
				destination.DisplacementDelta = 4;

				this.assembly.MOV (R32.EAX, source);
				this.assembly.MOV (destination, R32.EAX);

				this.assembly.MOV (R32.EAX, new DWordMemory (sourceMemory));
				this.assembly.MOV (new DWordMemory (destinationMemory), R32.EAX);
				break;

			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void Add (IR.Instructions.Add instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Register first = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register second = instruction.Use [1] as IR.Operands.Register;

			switch (assignee.InternalType) {
			case InternalType.I:
			case InternalType.I4:
				if (first.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (first.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (first)));

				if (instruction.AddType == IR.Instructions.Add.Type.Add) {
					if (second.IsRegisterSet)
						this.assembly.ADD (R32.EAX, Assembly.GetRegister (second.Register));
					else
						this.assembly.ADD (R32.EAX, new DWordMemory (this.GetAddress (second)));
				} else
					throw new NotImplementedEngineException ();

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.I8:
				Memory firstMemory = this.GetAddress (first);
				this.assembly.MOV (R32.EAX, new DWordMemory (firstMemory));
				firstMemory.DisplacementDelta = 4;
				this.assembly.MOV (R32.EDX, new DWordMemory (firstMemory));

				if (instruction.AddType == IR.Instructions.Add.Type.Add) {
					Memory secondMemory = this.GetAddress (second);
					this.assembly.ADD (R32.EAX, new DWordMemory (secondMemory));
					secondMemory.DisplacementDelta = 4;
					this.assembly.ADC (R32.EDX, new DWordMemory (secondMemory));
				} else
					throw new NotImplementedEngineException ();

				Memory assigneeMemory = this.GetAddress (assignee);
				this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EAX);
				assigneeMemory.DisplacementDelta = 4;
				this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EDX);

				break;

			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void Sub (IR.Instructions.Sub instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Register first = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register second = instruction.Use [1] as IR.Operands.Register;

			switch (assignee.InternalType) {
			case InternalType.I:
			case InternalType.I4:
				if (first.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (first.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (first)));

				if (instruction.SubType == IR.Instructions.Sub.Type.Sub) {
					if (second.IsRegisterSet)
						this.assembly.SUB (R32.EAX, Assembly.GetRegister (second.Register));
					else
						this.assembly.SUB (R32.EAX, new DWordMemory (this.GetAddress (second)));
				} else
					throw new NotImplementedEngineException ();

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.I8:
				Memory firstMemory = this.GetAddress (first);
				this.assembly.MOV (R32.EAX, new DWordMemory (firstMemory));
				firstMemory.DisplacementDelta = 4;
				this.assembly.MOV (R32.EDX, new DWordMemory (firstMemory));

				if (instruction.SubType == IR.Instructions.Sub.Type.Sub) {
					Memory secondMemory = this.GetAddress (second);
					this.assembly.SUB (R32.EAX, new DWordMemory (secondMemory));
					secondMemory.DisplacementDelta = 4;
					this.assembly.SBB (R32.EDX, new DWordMemory (secondMemory));
				} else
					throw new NotImplementedEngineException ();

				Memory assigneeMemory = this.GetAddress (assignee);
				this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EAX);
				assigneeMemory.DisplacementDelta = 4;
				this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EDX);

				break;

			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void Mul (IR.Instructions.Mul instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Register first = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register second = instruction.Use [1] as IR.Operands.Register;

			switch (assignee.InternalType) {
			case InternalType.I:
			case InternalType.I4:
				if (first.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (first.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (first)));

				if (instruction.MulType == IR.Instructions.Mul.Type.Mul) {
					if (second.IsRegisterSet)
						this.assembly.IMUL (R32.EAX, Assembly.GetRegister (second.Register));
					else
						this.assembly.IMUL (R32.EAX, new DWordMemory (this.GetAddress (second)));
				} else
					throw new NotImplementedEngineException ();

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.I8:
				if (instruction.MulType == IR.Instructions.Mul.Type.Mul) {
					Memory firstMemory = this.GetAddress (first);
					DWordMemory firstHigh = new DWordMemory (firstMemory);
					firstHigh.DisplacementDelta = 4;
					DWordMemory firstLow = new DWordMemory (firstMemory);

					Memory secondMemory = this.GetAddress (second);
					DWordMemory secondHigh = new DWordMemory (secondMemory);
					secondHigh.DisplacementDelta = 4;
					DWordMemory secondLow = new DWordMemory (secondMemory);

					this.assembly.PUSH (secondHigh);
					this.assembly.PUSH (secondLow);
					this.assembly.PUSH (firstHigh);
					this.assembly.PUSH (firstLow);
					this.assembly.CALL (Assembly.HELPER_LMUL);
					this.assembly.ADD (R32.ESP, 16);

					Memory assigneeMemory = this.GetAddress (assignee);
					this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EAX);
					assigneeMemory.DisplacementDelta = 4;
					this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EDX);
				} else
					throw new NotImplementedEngineException ();

				break;

			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void Div (IR.Instructions.Div instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Register first = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register second = instruction.Use [1] as IR.Operands.Register;

			switch (assignee.InternalType) {
			case InternalType.I:
			case InternalType.I4:
				if (first.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (first.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (first)));


				if (second.IsRegisterSet)
					this.assembly.MOV (R32.ECX, Assembly.GetRegister (second.Register));
				else
					this.assembly.MOV (R32.ECX, new DWordMemory (this.GetAddress (second)));

				if (instruction.DivType == IR.Instructions.Div.Type.Div) {
					this.assembly.CDQ ();
					this.assembly.IDIV (R32.ECX);

				} else if (instruction.DivType == IR.Instructions.Div.Type.DivUnsigned) {
					this.assembly.XOR (R32.EDX, R32.EDX);
					this.assembly.DIV (R32.ECX);

				} else
					throw new NotImplementedEngineException ();

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void Rem (IR.Instructions.Rem instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Register first = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register second = instruction.Use [1] as IR.Operands.Register;

			switch (assignee.InternalType) {
			case InternalType.I:
			case InternalType.I4:
				if (first.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (first.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (first)));


				if (second.IsRegisterSet)
					this.assembly.MOV (R32.ECX, Assembly.GetRegister (second.Register));
				else
					this.assembly.MOV (R32.ECX, new DWordMemory (this.GetAddress (second)));

				if (instruction.RemType == IR.Instructions.Rem.Type.Remainder) {
					this.assembly.CDQ ();
					this.assembly.IDIV (R32.ECX);

				} else if (instruction.RemType == IR.Instructions.Rem.Type.RemainderUnsigned) {
					this.assembly.XOR (R32.EDX, R32.EDX);
					this.assembly.DIV (R32.ECX);

				} else
					throw new NotImplementedEngineException ();

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EDX);
				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EDX);

				break;

			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void Neg (IR.Instructions.Neg instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;

			switch (assignee.InternalType) {
			case InternalType.I:
			case InternalType.I4:
				if (value.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

				this.assembly.NEG (R32.EAX);

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.I8:
				Memory firstMemory = this.GetAddress (value);
				this.assembly.MOV (R32.EAX, new DWordMemory (firstMemory));
				firstMemory.DisplacementDelta = 4;
				this.assembly.MOV (R32.EDX, new DWordMemory (firstMemory));

				this.assembly.NOT (R32.EDX);
				this.assembly.NEG (R32.EAX);
				this.assembly.SBB (R32.EDX, 0xFFFFFFFF);

				Memory assigneeMemory = this.GetAddress (assignee);
				this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EAX);
				assigneeMemory.DisplacementDelta = 4;
				this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EDX);

				break;

			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void Shl (IR.Instructions.Shl instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Register first = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register second = instruction.Use [1] as IR.Operands.Register;

			switch (assignee.InternalType) {
			case InternalType.I:
			case InternalType.I4:
				if (first.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (first.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (first)));

				if (second.IsRegisterSet)
					this.assembly.MOV (R32.ECX, Assembly.GetRegister (second.Register));
				else
					this.assembly.MOV (R32.ECX, new DWordMemory (this.GetAddress (second)));

				this.assembly.AND (R32.ECX, (uint) 0xFF);
				this.assembly.SHL__CL (R32.EAX);

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.I8:
				Memory firstMemory = this.GetAddress (first);
				DWordMemory high = new DWordMemory (firstMemory);
				high.DisplacementDelta = 4;
				DWordMemory low = new DWordMemory (firstMemory);

				if (second.IsRegisterSet)
					this.assembly.PUSH (Assembly.GetRegister (second.Register));
				else
					this.assembly.PUSH (new DWordMemory (this.GetAddress (second)));

				this.assembly.PUSH (high);
				this.assembly.PUSH (low);
				this.assembly.CALL (Assembly.HELPER_LSHL);
				this.assembly.ADD (R32.ESP, 12);

				Memory assigneeMemory = this.GetAddress (assignee);
				this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EAX);
				assigneeMemory.DisplacementDelta = 4;
				this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EDX);

				break;

			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void Shr (IR.Instructions.Shr instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Register first = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register second = instruction.Use [1] as IR.Operands.Register;

			switch (assignee.InternalType) {
			case InternalType.I:
			case InternalType.I4:
				if (first.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (first.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (first)));

				if (second.IsRegisterSet)
					this.assembly.MOV (R32.ECX, Assembly.GetRegister (second.Register));
				else
					this.assembly.MOV (R32.ECX, new DWordMemory (this.GetAddress (second)));

				this.assembly.AND (R32.ECX, (uint) 0xFF);

				if (instruction.ShrType == IR.Instructions.Shr.Type.SHR)
					this.assembly.SAR__CL (R32.EAX);

				else if (instruction.ShrType == IR.Instructions.Shr.Type.SHRUnsigned)
					this.assembly.SHR__CL (R32.EAX);

				else
					throw new NotImplementedEngineException ();

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.I8:
				Memory firstMemory = this.GetAddress (first);
				DWordMemory high = new DWordMemory (firstMemory);
				high.DisplacementDelta = 4;
				DWordMemory low = new DWordMemory (firstMemory);

				if (second.IsRegisterSet)
					this.assembly.PUSH (Assembly.GetRegister (second.Register));
				else
					this.assembly.PUSH (new DWordMemory (this.GetAddress (second)));

				this.assembly.PUSH (high);
				this.assembly.PUSH (low);

				if (instruction.ShrType == IR.Instructions.Shr.Type.SHR)
					this.assembly.CALL (Assembly.HELPER_LSAR);

				else if (instruction.ShrType == IR.Instructions.Shr.Type.SHRUnsigned)
					this.assembly.CALL (Assembly.HELPER_LSHR);

				else
					throw new NotImplementedEngineException ();

				this.assembly.ADD (R32.ESP, 12);

				Memory assigneeMemory = this.GetAddress (assignee);
				this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EAX);
				assigneeMemory.DisplacementDelta = 4;
				this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EDX);

				break;

			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void And (IR.Instructions.And instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Register first = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register second = instruction.Use [1] as IR.Operands.Register;

			switch (assignee.InternalType) {
			case InternalType.I:
			case InternalType.I4:
				if (first.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (first.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (first)));

				if (second.IsRegisterSet)
					this.assembly.AND (R32.EAX, Assembly.GetRegister (second.Register));
				else
					this.assembly.AND (R32.EAX, new DWordMemory (this.GetAddress (second)));

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.I8:
				Memory firstMemory = this.GetAddress (first);
				this.assembly.MOV (R32.EAX, new DWordMemory (firstMemory));
				firstMemory.DisplacementDelta = 4;
				this.assembly.MOV (R32.EDX, new DWordMemory (firstMemory));

				Memory secondMemory = this.GetAddress (second);
				this.assembly.AND (R32.EAX, new DWordMemory (secondMemory));
				secondMemory.DisplacementDelta = 4;
				this.assembly.AND (R32.EDX, new DWordMemory (secondMemory));

				Memory assigneeMemory = this.GetAddress (assignee);
				this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EAX);
				assigneeMemory.DisplacementDelta = 4;
				this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EDX);

				break;

			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void Or (IR.Instructions.Or instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Register first = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register second = instruction.Use [1] as IR.Operands.Register;

			switch (assignee.InternalType) {
			case InternalType.I:
			case InternalType.I4:
				if (first.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (first.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (first)));

				if (second.IsRegisterSet)
					this.assembly.OR (R32.EAX, Assembly.GetRegister (second.Register));
				else
					this.assembly.OR (R32.EAX, new DWordMemory (this.GetAddress (second)));

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.I8:
				Memory firstMemory = this.GetAddress (first);
				this.assembly.MOV (R32.EAX, new DWordMemory (firstMemory));
				firstMemory.DisplacementDelta = 4;
				this.assembly.MOV (R32.EDX, new DWordMemory (firstMemory));

				Memory secondMemory = this.GetAddress (second);
				this.assembly.OR (R32.EAX, new DWordMemory (secondMemory));
				secondMemory.DisplacementDelta = 4;
				this.assembly.OR (R32.EDX, new DWordMemory (secondMemory));

				Memory assigneeMemory = this.GetAddress (assignee);
				this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EAX);
				assigneeMemory.DisplacementDelta = 4;
				this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EDX);

				break;

			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void Xor (IR.Instructions.Xor instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Register first = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register second = instruction.Use [1] as IR.Operands.Register;

			switch (assignee.InternalType) {
			case InternalType.I:
			case InternalType.I4:
				if (first.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (first.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (first)));

				if (second.IsRegisterSet)
					this.assembly.XOR (R32.EAX, Assembly.GetRegister (second.Register));
				else
					this.assembly.XOR (R32.EAX, new DWordMemory (this.GetAddress (second)));

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.I8:
				Memory firstMemory = this.GetAddress (first);
				this.assembly.MOV (R32.EAX, new DWordMemory (firstMemory));
				firstMemory.DisplacementDelta = 4;
				this.assembly.MOV (R32.EDX, new DWordMemory (firstMemory));

				Memory secondMemory = this.GetAddress (second);
				this.assembly.XOR (R32.EAX, new DWordMemory (secondMemory));
				secondMemory.DisplacementDelta = 4;
				this.assembly.XOR (R32.EDX, new DWordMemory (secondMemory));

				Memory assigneeMemory = this.GetAddress (assignee);
				this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EAX);
				assigneeMemory.DisplacementDelta = 4;
				this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EDX);

				break;

			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void Not (IR.Instructions.Not instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;

			switch (assignee.InternalType) {
			case InternalType.I:
			case InternalType.I4:
				if (value.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

				this.assembly.NOT (R32.EAX);

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				break;

			case InternalType.I8:
				Memory firstMemory = this.GetAddress (value);
				this.assembly.MOV (R32.EAX, new DWordMemory (firstMemory));
				firstMemory.DisplacementDelta = 4;
				this.assembly.MOV (R32.EDX, new DWordMemory (firstMemory));

				this.assembly.NOT (R32.EAX);
				this.assembly.NOT (R32.EDX);

				Memory assigneeMemory = this.GetAddress (assignee);
				this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EAX);
				assigneeMemory.DisplacementDelta = 4;
				this.assembly.MOV (new DWordMemory (assigneeMemory), R32.EDX);

				break;

			default:
				throw new NotImplementedEngineException ();
			}
		}

		private void Pop (IR.Instructions.Pop instruction)
		{
			// Do nothing.
		}

		private void Newobj (IR.Instructions.Newobj instruction)
		{
			if (instruction.Method.Class.IsValueType) {
				IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;

				this.Initialize (assignee);

				this.PushCallParameters (instruction);

				this.assembly.LEA (R32.EAX, new DWordMemory (this.GetAddress (assignee)));

				this.assembly.PUSH (R32.EAX);

				this.assembly.CALL (instruction.Method.AssemblyLabel);

				this.PopCallParameters (instruction);

			} else if (instruction.Method.Class.IsClass) {
				this.PushCallParameters (instruction);

				this.assembly.MOV (R32.EAX, this.assembly.GetVTableLabel (instruction.Method.Class.TypeFullName));
				this.assembly.PUSH (R32.EAX);
				this.assembly.CALL (this.assembly.Engine.AllocObject.AssemblyLabel);
				this.assembly.ADD (R32.ESP, 4);

				IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				this.assembly.PUSH (R32.EAX);
				this.assembly.CALL (instruction.Method.AssemblyLabel);
				this.PopCallParameters (instruction);

			} else if (instruction.Method.Class.IsArray) {
				this.ArrayMultidimensionalCtor (instruction);

			} else
				throw new NotImplementedEngineException ();
		}

		private void Stobj (IR.Instructions.Stobj instruction)
		{
			IR.Operands.Register assignee = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register value = instruction.Use [1] as IR.Operands.Register;

			this.assembly.PUSH (R32.ECX);
			this.assembly.PUSH (R32.ESI);
			this.assembly.PUSH (R32.EDI);

			if (value.IsRegisterSet)
				this.assembly.MOV (R32.ESI, Assembly.GetRegister (value.Register));
			else
				this.assembly.LEA (R32.ESI, new DWordMemory (this.GetAddress (value)));

			if (assignee.IsRegisterSet)
				this.assembly.MOV (R32.EDI, Assembly.GetRegister (assignee.Register));
			else
				this.assembly.MOV (R32.EDI, new DWordMemory (this.GetAddress (assignee)));

			string typeName = instruction.Type.ToString ();

			uint size = (uint) this.method.Engine.GetTypeSize (typeName, 4) / 4;

			this.assembly.MOV (R32.ECX, size);

			this.assembly.CLD ();
			this.assembly.REP ();
			this.assembly.MOVSD ();

			this.assembly.POP (R32.EDI);
			this.assembly.POP (R32.ESI);
			this.assembly.POP (R32.ECX);
		}

		private void Ldobj (IR.Instructions.Ldobj instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;
			string typeName = assignee.Type.ToString ();
			uint size = (uint) this.method.Engine.GetTypeSize (typeName, 4) / 4;

			this.assembly.PUSH (R32.ECX);
			this.assembly.PUSH (R32.ESI);
			this.assembly.PUSH (R32.EDI);

			if (value.IsRegisterSet)
				this.assembly.MOV (R32.ESI, Assembly.GetRegister (value.Register));
			else
				this.assembly.MOV (R32.ESI, new DWordMemory (this.GetAddress (value)));

			if (assignee.IsRegisterSet)
				this.assembly.MOV (R32.EDI, Assembly.GetRegister (assignee.Register));
			else
				this.assembly.LEA (R32.EDI, new DWordMemory (this.GetAddress (assignee)));

			this.assembly.MOV (R32.ECX, size);

			this.assembly.CLD ();
			this.assembly.REP ();
			this.assembly.MOVSD ();

			this.assembly.POP (R32.EDI);
			this.assembly.POP (R32.ESI);
			this.assembly.POP (R32.ECX);
		}

		private void Initobj (IR.Instructions.Initobj instruction)
		{
			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;
			string typeName = instruction.Type.ToString ();
			uint size = (uint) this.method.Engine.GetTypeSize (typeName);

			this.assembly.PUSH (R32.ECX);
			this.assembly.PUSH (R32.EDI);

			this.assembly.XOR (R32.EAX, R32.EAX);

			if (value.IsRegisterSet)
				this.assembly.MOV (R32.EDI, Assembly.GetRegister (value.Register));
			else
				this.assembly.MOV (R32.EDI, new DWordMemory (this.GetAddress (value)));

			this.assembly.MOV (R32.ECX, size);

			this.assembly.CLD ();
			this.assembly.REP ();
			this.assembly.STOSB ();

			this.assembly.POP (R32.EDI);
			this.assembly.POP (R32.ECX);
		}

		private void SizeOf (IR.Instructions.SizeOf instruction)
		{
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;

			uint size = (uint) this.method.Engine.GetTypeSize (instruction.Type.ToString ());

			if (assignee.IsRegisterSet)
				this.assembly.MOV (Assembly.GetRegister (assignee.Register), size);
			else
				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), size);
		}

		private void Switch (IR.Instructions.Switch instruction)
		{
			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;

			if (value.IsRegisterSet)
				this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
			else
				this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

			// The first block (0) is the one that is used to bail out if the switch tests are all false.
			for (byte i = 0; i < instruction.Blocks.Length; i++) {
				assembly.CMP (R32.EAX, i);
				assembly.JE (this.GetLabel (instruction.Blocks [i]));
			}

			assembly.JMP (this.GetLabel (instruction.Block.Outs [0]));
		}

		private void Box (IR.Instructions.Box instruction)
		{
			this.assembly.MOV (R32.EAX, this.assembly.GetVTableLabel (instruction.Type.TypeFullName));
			this.assembly.PUSH (R32.EAX);
			this.assembly.CALL (this.assembly.Engine.AllocObject.AssemblyLabel);
			assembly.ADD (R32.ESP, 4);

			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;

			if (assignee.IsRegisterSet)
				this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
			else
				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

			this.assembly.PUSH (R32.ECX);
			this.assembly.PUSH (R32.ESI);
			this.assembly.PUSH (R32.EDI);

			this.assembly.ADD (R32.EAX, (uint) this.assembly.Engine.ObjectSize);
			this.assembly.MOV (R32.EDI, R32.EAX);

			if (value.IsRegisterSet)
				this.assembly.MOV (R32.ESI, Assembly.GetRegister (value.Register));
			else
				this.assembly.LEA (R32.ESI, new DWordMemory (this.GetAddress (value)));

			this.assembly.MOV (R32.ECX, (uint) instruction.Type.Size);

			this.assembly.CLD ();
			this.assembly.REP ();
			this.assembly.MOVSB ();

			this.assembly.POP (R32.EDI);
			this.assembly.POP (R32.ESI);
			this.assembly.POP (R32.ECX);
		}

		private void Unbox (IR.Instructions.Unbox instruction)
		{
			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;

			if (value.IsRegisterSet)
				this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
			else
				this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

			this.assembly.ADD (R32.EAX, (uint) this.assembly.Engine.ObjectSize);

			if (assignee.IsRegisterSet)
				this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
			else
				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);
		}

		private void UnboxAny (IR.Instructions.UnboxAny instruction)
		{
			if (instruction.Type.ClassDefinition.IsValueType) {
				IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;
				IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;

				if (value.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

				this.assembly.ADD (R32.EAX, (uint) this.assembly.Engine.ObjectSize);

				this.assembly.PUSH (R32.ECX);
				this.assembly.PUSH (R32.ESI);
				this.assembly.PUSH (R32.EDI);

				this.assembly.MOV (R32.ESI, R32.EAX);

				if (assignee.IsRegisterSet)
					this.assembly.MOV (R32.EDI, Assembly.GetRegister (assignee.Register));
				else
					this.assembly.LEA (R32.EDI, new DWordMemory (this.GetAddress (assignee)));

				this.assembly.MOV (R32.ECX, (uint) instruction.Type.Size);

				this.assembly.CLD ();
				this.assembly.REP ();
				this.assembly.MOVSB ();

				this.assembly.POP (R32.EDI);
				this.assembly.POP (R32.ESI);
				this.assembly.POP (R32.ECX);

			} else {
				// TODO check the value if it is a reference or generic
				throw new NotImplementedEngineException ();
			}
		}

		#region ARRAY
		private const int ARRAY_RANK_OFFSET = 0;
		private const int ARRAY_BOUND_OFFSET = 4;
		private const int ARRAY_BOUND_LOWER_BOUND_OFFSET = 0;
		private const int ARRAY_BOUND_LENGTH_OFFSET = 4;
		private const int ARRAY_BOUND_SIZE = 8;
		private const int ARRAY_BASE_SIZE = ARRAY_BOUND_OFFSET + ARRAY_BOUND_SIZE;
		private const int ARRAY_FIRST_BOUND_LENGTH_OFFSET = ARRAY_BOUND_OFFSET + ARRAY_BOUND_LENGTH_OFFSET;

		private void Newarr (IR.Instructions.Newarr instruction)
		{
			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
			int objectSize = this.assembly.Engine.ObjectSize;

			this.assembly.MOV (R32.EAX, (uint) instruction.Type.SpecialTypeElement.ReferenceSize);

			if (value.IsRegisterSet)
				this.assembly.MOV (R32.ECX, Assembly.GetRegister (value.Register));
			else
				this.assembly.MOV (R32.ECX, new DWordMemory (this.GetAddress (value)));

			this.assembly.MUL (R32.ECX);
			this.assembly.ADD (R32.EAX, (uint) (objectSize + ARRAY_BASE_SIZE));

			this.assembly.PUSH (R32.EAX);

			this.assembly.MOV (R32.EAX, this.assembly.GetVTableLabel (instruction.Type.TypeFullName));
			this.assembly.PUSH (R32.EAX);

			this.assembly.CALL (this.assembly.Engine.AllocArray.AssemblyLabel);
			assembly.ADD (R32.ESP, 8);

			if (assignee.IsRegisterSet)
				this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
			else
				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

			this.assembly.MOV (new DWordMemory (null, R32.EAX, null, 0, objectSize + ARRAY_RANK_OFFSET), (uint) 1);
			this.assembly.MOV (new DWordMemory (null, R32.EAX, null, 0, objectSize + ARRAY_BOUND_OFFSET + ARRAY_BOUND_LOWER_BOUND_OFFSET), (uint) 0);

			if (value.IsRegisterSet)
				this.assembly.MOV (R32.ECX, Assembly.GetRegister (value.Register));
			else
				this.assembly.MOV (R32.ECX, new DWordMemory (this.GetAddress (value)));

			this.assembly.MOV (new DWordMemory (null, R32.EAX, null, 0, objectSize + ARRAY_FIRST_BOUND_LENGTH_OFFSET), R32.ECX);
		}

		private void Stelem (IR.Instructions.Stelem instruction)
		{
			string labelError = this.GetLabel (instruction.Block, instruction.Index, 0);
			string labelOk = this.GetLabel (instruction.Block, instruction.Index, 1);

			IR.Operands.Register value = instruction.Use [1] as IR.Operands.Register;
			IR.Operands.Register index = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register assignee = instruction.Use [2] as IR.Operands.Register;

			int objectSize = this.assembly.Engine.ObjectSize;
			int elementSize = assignee.Type.SpecialTypeElement.ReferenceSize;

			if (assignee.IsRegisterSet)
				this.assembly.MOV (R32.ECX, Assembly.GetRegister (assignee.Register));
			else
				this.assembly.MOV (R32.ECX, new DWordMemory (this.GetAddress (assignee)));

			if (index.IsRegisterSet)
				this.assembly.MOV (R32.EAX, Assembly.GetRegister (index.Register));
			else
				this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (index)));

			this.assembly.CMP (new DWordMemory (null, R32.ECX, null, 0, objectSize + ARRAY_FIRST_BOUND_LENGTH_OFFSET), R32.EAX);
			this.assembly.JNA (labelError);

			this.assembly.MOV (R32.EDX, (uint) elementSize);
			this.assembly.MUL (R32.EDX);
			this.assembly.LEA (R32.EDX, new DWordMemory (null, R32.ECX, R32.EAX, 0, objectSize + ARRAY_BASE_SIZE));

			this.Save (assignee.Type.SpecialTypeElement.TypeFullName, assignee.Type.SpecialTypeElement.InternalType, new DWordMemory (null, R32.EDX, null, 0), value);

			this.assembly.JMP (labelOk);
			this.assembly.LABEL (labelError);

			// TODO throw IndexOutOfRangeException

			this.assembly.LABEL (labelOk);
		}

		private void Ldelem (IR.Instructions.Ldelem instruction)
		{
			string labelError = this.GetLabel (instruction.Block, instruction.Index, 0);
			string labelOk = this.GetLabel (instruction.Block, instruction.Index, 1);

			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register index = instruction.Use [1] as IR.Operands.Register;
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;

			int objectSize = this.assembly.Engine.ObjectSize;
			int elementSize = value.Type.SpecialTypeElement.ReferenceSize;

			if (value.IsRegisterSet)
				this.assembly.MOV (R32.ECX, Assembly.GetRegister (value.Register));
			else
				this.assembly.MOV (R32.ECX, new DWordMemory (this.GetAddress (value)));

			if (index.IsRegisterSet)
				this.assembly.MOV (R32.EAX, Assembly.GetRegister (index.Register));
			else
				this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (index)));

			this.assembly.CMP (new DWordMemory (null, R32.ECX, null, 0, objectSize + ARRAY_FIRST_BOUND_LENGTH_OFFSET), R32.EAX);
			this.assembly.JNA (labelError);

			this.assembly.MOV (R32.EDX, (uint) elementSize);
			this.assembly.MUL (R32.EDX);
			this.assembly.LEA (R32.EDX, new DWordMemory (null, R32.ECX, R32.EAX, 0, objectSize + ARRAY_BASE_SIZE));

			this.Load (assignee, value.Type.SpecialTypeElement.InternalType, new DWordMemory (null, R32.EDX, null, 0));

			this.assembly.JMP (labelOk);
			this.assembly.LABEL (labelError);

			// TODO throw IndexOutOfRangeException

			this.assembly.LABEL (labelOk);
		}

		private void Ldlen (IR.Instructions.Ldlen instruction)
		{
			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;

			int objectSize = this.assembly.Engine.ObjectSize;

			if (value.IsRegisterSet)
				this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
			else
				this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

			this.assembly.MOV (R32.EAX, new DWordMemory (null, R32.EAX, null, 0, objectSize + ARRAY_FIRST_BOUND_LENGTH_OFFSET));

			if (assignee.IsRegisterSet)
				this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
			else
				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);
		}

		private void ArrayCalls (IR.Instructions.Call call)
		{
			if (call.Method.Name.Equals ("Set"))
				ArrayMultidimensionalSet (call);

			else if (call.Method.Name.Equals ("Get"))
				ArrayMultidimensionalGet (call);

			else
				throw new NotImplementedEngineException ();
		}

		private void ArrayMultidimensionalCtor (IR.Instructions.Newobj instruction)
		{
			string constructorType = "[" + "".PadLeft (instruction.Use.Length - 1, ',') + "]";

			if (instruction.Method.Class.TypeFullName.EndsWith (constructorType)) {
				IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;
				IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;
				int objectSize = this.assembly.Engine.ObjectSize;

				// Compute the count of entries in the array
				if (value.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

				for (int i = 1; i < instruction.Use.Length; i++) {
					value = instruction.Use [i] as IR.Operands.Register;

					if (value.IsRegisterSet)
						this.assembly.MOV (R32.ECX, Assembly.GetRegister (value.Register));
					else
						this.assembly.MOV (R32.ECX, new DWordMemory (this.GetAddress (value)));

					this.assembly.MUL (R32.ECX);
				}

				// Compute the amount of bytes of all entries in the array
				this.assembly.MOV (R32.ECX, (uint) instruction.Method.Class.SpecialTypeElement.ReferenceSize);
				this.assembly.MUL (R32.ECX);

				// Compute the whole size of the array including the overhead
				this.assembly.ADD (R32.EAX, (uint) (objectSize + ARRAY_BASE_SIZE + (instruction.Use.Length - 1)*ARRAY_BOUND_SIZE));

				this.assembly.PUSH (R32.EAX);

				this.assembly.MOV (R32.EAX, this.assembly.GetVTableLabel (instruction.Method.Class.TypeFullName));
				this.assembly.PUSH (R32.EAX);

				this.assembly.CALL (this.assembly.Engine.AllocArray.AssemblyLabel);
				assembly.ADD (R32.ESP, 8);

				if (assignee.IsRegisterSet)
					this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
				else
					this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);

				// Set the rank
				this.assembly.MOV (new DWordMemory (null, R32.EAX, null, 0, objectSize + ARRAY_RANK_OFFSET), (uint) instruction.Use.Length);

				// Set the lower boundary & length of every dimension
				for (int i = 0; i < instruction.Use.Length; i++) {
					int offset = objectSize + ARRAY_BOUND_OFFSET + i * ARRAY_BOUND_SIZE;

					value = instruction.Use [i] as IR.Operands.Register;

					if (value.IsRegisterSet)
						this.assembly.MOV (R32.ECX, Assembly.GetRegister (value.Register));
					else
						this.assembly.MOV (R32.ECX, new DWordMemory (this.GetAddress (value)));

					this.assembly.MOV (new DWordMemory (null, R32.EAX, null, 0, offset + ARRAY_BOUND_LOWER_BOUND_OFFSET), (uint) 0);
					this.assembly.MOV (new DWordMemory (null, R32.EAX, null, 0, offset + ARRAY_BOUND_LENGTH_OFFSET), R32.ECX);
				}

			} else
				throw new NotImplementedEngineException ("Constructor with Lower boundaries not supported yet.");

		}

		private void ArrayMultidimensionalSet (IR.Instructions.Call instruction)
		{
			string labelError = this.GetLabel (instruction.Block, instruction.Index, 0);
			string labelOk = this.GetLabel (instruction.Block, instruction.Index, 1);

			IR.Operands.Register value = instruction.Use [instruction.Use.Length - 1] as IR.Operands.Register;
			IR.Operands.Register assignee = instruction.Use [0] as IR.Operands.Register;

			int objectSize = this.assembly.Engine.ObjectSize;
			int elementSize = assignee.Type.SpecialTypeElement.ReferenceSize;

			if (assignee.IsRegisterSet)
				this.assembly.MOV (R32.ECX, Assembly.GetRegister (assignee.Register));
			else
				this.assembly.MOV (R32.ECX, new DWordMemory (this.GetAddress (assignee)));

			// Check if every index is valid
			for (int i = 1; i < instruction.Use.Length - 1; i++) {
				IR.Operands.Register index = instruction.Use [i] as IR.Operands.Register;

				if (index.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (index.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (index)));

				int offset = objectSize + ARRAY_BOUND_OFFSET + (i - 1) * ARRAY_BOUND_SIZE;

				this.assembly.SUB (R32.EAX, new DWordMemory (null, R32.ECX, null, 0, offset + ARRAY_BOUND_LOWER_BOUND_OFFSET));

				this.assembly.CMP (new DWordMemory (null, R32.ECX, null, 0, offset + ARRAY_BOUND_LENGTH_OFFSET), R32.EAX);
				this.assembly.JNA (labelError);
			}

			this.assembly.XOR (R32.EAX, R32.EAX);
			this.assembly.PUSH (R32.EAX);

			// Compute the position in the table using the indices
			for (int i = 1; i < instruction.Use.Length - 1; i++) {
				IR.Operands.Register index = instruction.Use [i] as IR.Operands.Register;

				if (index.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (index.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (index)));

				for (int j = i + 1; j < instruction.Use.Length - 1; j++) {
					int offset = objectSize + ARRAY_BOUND_OFFSET + (j - 1) * ARRAY_BOUND_SIZE;

					this.assembly.MOV (R32.EDX, new DWordMemory (null, R32.ECX, null, 0, offset + ARRAY_BOUND_LENGTH_OFFSET));
					this.assembly.MUL (R32.EDX);
				}

				this.assembly.ADD (new DWordMemory (null, R32.ESP, null, 0), R32.EAX);
			}

			this.assembly.POP (R32.EAX);

			this.assembly.MOV (R32.EDX, (uint) elementSize);
			this.assembly.MUL (R32.EDX);
			this.assembly.LEA (R32.EDX, new DWordMemory (null, R32.ECX, R32.EAX, 0, objectSize + ARRAY_BASE_SIZE + (instruction.Use.Length - 3)*ARRAY_BOUND_SIZE));

			this.Save (assignee.Type.SpecialTypeElement.TypeFullName, assignee.Type.SpecialTypeElement.InternalType, new DWordMemory (null, R32.EDX, null, 0), value);

			this.assembly.JMP (labelOk);
			this.assembly.LABEL (labelError);

			// TODO throw IndexOutOfRangeException

			this.assembly.LABEL (labelOk);
		}

		private void ArrayMultidimensionalGet (IR.Instructions.Call instruction)
		{
			string labelError = this.GetLabel (instruction.Block, instruction.Index, 0);
			string labelOk = this.GetLabel (instruction.Block, instruction.Index, 1);

			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;

			int objectSize = this.assembly.Engine.ObjectSize;
			int elementSize = value.Type.SpecialTypeElement.ReferenceSize;

			if (value.IsRegisterSet)
				this.assembly.MOV (R32.ECX, Assembly.GetRegister (value.Register));
			else
				this.assembly.MOV (R32.ECX, new DWordMemory (this.GetAddress (value)));

			// Check if every index is valid
			for (int i = 1; i < instruction.Use.Length; i++) {
				IR.Operands.Register index = instruction.Use [i] as IR.Operands.Register;

				if (index.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (index.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (index)));

				int offset = objectSize + ARRAY_BOUND_OFFSET + (i - 1) * ARRAY_BOUND_SIZE;

				this.assembly.SUB (R32.EAX, new DWordMemory (null, R32.ECX, null, 0, offset + ARRAY_BOUND_LOWER_BOUND_OFFSET));

				this.assembly.CMP (new DWordMemory (null, R32.ECX, null, 0, offset + ARRAY_BOUND_LENGTH_OFFSET), R32.EAX);
				this.assembly.JNA (labelError);
			}

			this.assembly.XOR (R32.EAX, R32.EAX);
			this.assembly.PUSH (R32.EAX);

			// Compute the position in the table using the indices
			for (int i = 1; i < instruction.Use.Length; i++) {
				IR.Operands.Register index = instruction.Use [i] as IR.Operands.Register;

				if (index.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (index.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (index)));

				for (int j = i + 1; j < instruction.Use.Length; j++) {
					int offset = objectSize + ARRAY_BOUND_OFFSET + (j - 1) * ARRAY_BOUND_SIZE;

					this.assembly.MOV (R32.EDX, new DWordMemory (null, R32.ECX, null, 0, offset + ARRAY_BOUND_LENGTH_OFFSET));
					this.assembly.MUL (R32.EDX);
				}

				this.assembly.ADD (new DWordMemory (null, R32.ESP, null, 0), R32.EAX);
			}

			this.assembly.POP (R32.EAX);

			this.assembly.MOV (R32.EDX, (uint) elementSize);
			this.assembly.MUL (R32.EDX);
			this.assembly.LEA (R32.EDX, new DWordMemory (null, R32.ECX, R32.EAX, 0, objectSize + ARRAY_BASE_SIZE + (instruction.Use.Length - 2) * ARRAY_BOUND_SIZE));

			this.Load (assignee, value.Type.SpecialTypeElement.InternalType, new DWordMemory (null, R32.EDX, null, 0));

			this.assembly.JMP (labelOk);
			this.assembly.LABEL (labelError);

			// TODO throw IndexOutOfRangeException

			this.assembly.LABEL (labelOk);
		}

		#endregion

		private void Isinst (IR.Instructions.Isinst instruction)
		{
			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;

			this.assembly.MOV (R32.EAX, this.assembly.GetTypeInfoLabel (instruction.Type.TypeFullName));
			this.assembly.PUSH (R32.EAX);

			if (value.IsRegisterSet)
				this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
			else
				this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

			this.assembly.PUSH (R32.EAX);
			this.assembly.CALL (this.assembly.Engine.IsBaseClassOf.AssemblyLabel);
			this.assembly.ADD (R32.ESP, 8);

			if (assignee.IsRegisterSet)
				this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
			else
				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);
		}

		private void Castclass (IR.Instructions.Castclass instruction)
		{
			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;
			IR.Operands.Register assignee = instruction.Def as IR.Operands.Register;

			this.assembly.MOV (R32.EAX, this.assembly.GetTypeInfoLabel (instruction.Type.TypeFullName));
			this.assembly.PUSH (R32.EAX);

			if (value.IsRegisterSet)
				this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
			else
				this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

			this.assembly.PUSH (R32.EAX);
			this.assembly.CALL (this.assembly.Engine.CastClass.AssemblyLabel);
			this.assembly.ADD (R32.ESP, 8);

			if (assignee.IsRegisterSet)
				this.assembly.MOV (Assembly.GetRegister (assignee.Register), R32.EAX);
			else
				this.assembly.MOV (new DWordMemory (this.GetAddress (assignee)), R32.EAX);
		}

		private void Leave (IR.Instructions.Leave instruction)
		{
			// This makes sure that the the finally handler gets called
			if (instruction.Block.IsTryLast)
				this.assembly.CALL (this.GetLabel (instruction.Block.Outs [1]));

			this.assembly.JMP (this.GetLabel (instruction.Block.Outs [0]));
		}

		private void Endfinally (IR.Instructions.Endfinally instruction)
		{
			this.assembly.MOV (R32.ESP, this.GetExceptionHandlingSPSlot);

			this.assembly.RET ();
		}

		private void Endfilter (IR.Instructions.Endfilter instruction)
		{
			IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;

			if (value.IsRegisterSet)
				this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
			else
				this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

			this.assembly.MOV (R32.ESP, this.GetExceptionHandlingSPSlot);

			this.assembly.RET ();
		}

		private void Break (IR.Instructions.Break instruction)
		{
			// Does nothing, perhaps emit a label?
		}

		private void Throw (IR.Instructions.Throw instruction)
		{
			if (instruction.Use.Length == 1) {
				IR.Operands.Register value = instruction.Use [0] as IR.Operands.Register;

				if (value.IsRegisterSet)
					this.assembly.MOV (R32.EAX, Assembly.GetRegister (value.Register));
				else
					this.assembly.MOV (R32.EAX, new DWordMemory (this.GetAddress (value)));

			} else 
				// This is for when rethrowing an exception
				this.assembly.MOV (R32.EAX, this.GetExceptionHandlingExceptionObjectSlot);

			this.assembly.PUSH (R32.EAX);
			this.assembly.CALL (this.assembly.Engine.Throw.AssemblyLabel);
			this.assembly.ADD (R32.ESP, 4);
		}
	}
}
