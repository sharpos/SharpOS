// 
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	William Lahti <xfurious@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SharpOS.AOT.IR;
using SharpOS.AOT.IR.Operands;
using SharpOS.AOT.IR.Operators;
using SharpOS.AOT.IR.Instructions;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace SharpOS.AOT.IR {
	public class Block : IEnumerable<SharpOS.AOT.IR.Instructions.Instruction> {
		public enum BlockType {
			Return,
			Throw,
			OneWay,
			TwoWay,
			NWay,
			Fall
		}

		private int stack = 0;

		/// <summary>
		/// Gets the stack.
		/// </summary>
		/// <value>The stack.</value>
		public int Stack {
			get {
				return stack;
			}
		}

		private Method method = null;

		/// <summary>
		/// Gets the method.
		/// </summary>
		/// <value>The method.</value>
		public Method Method {
			get {
				return method;
			}
		}

		private bool ssaBlock = false;

		/// <summary>
		/// Gets or sets a value indicating whether this block has been added by the SSA.
		/// </summary>
		/// <value><c>true</c> if it has been added by the SSA; otherwise, <c>false</c>.</value>
		public bool SSABlock {
			get {
				return this.ssaBlock;
			}
			set {
				this.ssaBlock = value;
			}
		}

		private List<Mono.Cecil.Cil.Instruction> cil = new List<Mono.Cecil.Cil.Instruction> ();

		/// <summary>
		/// Gets or sets the CIL.
		/// </summary>
		/// <value>The CIL.</value>
		public List<Mono.Cecil.Cil.Instruction> CIL {
			get {
				return cil;
			}
			set {
				cil = value;
			}
		}

		bool converted = false;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Block"/> has been converted to IR.
		/// </summary>
		/// <value><c>true</c> if converted; otherwise, <c>false</c>.</value>
		public bool Converted {
			get {
				return converted;
			}
		}

		private List<SharpOS.AOT.IR.Instructions.Instruction> instructions = new List<SharpOS.AOT.IR.Instructions.Instruction> ();

		/// <summary>
		/// Gets the <see cref="SharpOS.AOT.IR.Instructions.Instruction"/> at the specified index.
		/// </summary>
		/// <value></value>
		public SharpOS.AOT.IR.Instructions.Instruction this [int index] {
			get {
				return this.instructions [index];
			}
		}


		/// <summary>
		/// Gets the instructions count.
		/// </summary>
		/// <value>The instructions count.</value>
		public int InstructionsCount {
			get {
				return this.instructions.Count;
			}
		}

		private List<Block> ins = new List<Block> ();

		/// <summary>
		/// Gets the ins.
		/// </summary>
		/// <value>The ins.</value>
		public List<Block> Ins {
			get {
				return ins;
			}
		}

		private List<Block> outs = new List<Block> ();

		/// <summary>
		/// Gets the outs.
		/// </summary>
		/// <value>The outs.</value>
		public List<Block> Outs {
			get {
				return outs;
			}
		}

		private List<Block> dominators = new List<Block> ();

		/// <summary>
		/// Gets or sets the dominators.
		/// </summary>
		/// <value>The dominators.</value>
		public List<Block> Dominators {
			get {
				return dominators;
			}
			set {
				dominators = value;
			}
		}

		private Block immediateDominator = null;

		/// <summary>
		/// Gets or sets the immediate dominator.
		/// </summary>
		/// <value>The immediate dominator.</value>
		public Block ImmediateDominator {
			get {
				return immediateDominator;
			}
			set {
				immediateDominator = value;
			}
		}

		private List<Block> immediateDominatorOf = new List<Block> ();

		/// <summary>
		/// Gets or sets the immediate dominator of.
		/// </summary>
		/// <value>The immediate dominator of.</value>
		public List<Block> ImmediateDominatorOf {
			get {
				return immediateDominatorOf;
			}
			set {
				immediateDominatorOf = value;
			}
		}

		private List<Block> dominanceFrontiers = new List<Block> ();

		/// <summary>
		/// Gets or sets the dominance frontiers.
		/// </summary>
		/// <value>The dominance frontiers.</value>
		public List<Block> DominanceFrontiers {
			get {
				return dominanceFrontiers;
			}
			set {
				dominanceFrontiers = value;
			}
		}

		private int index = 0;

		/// <summary>
		/// Gets or sets the index.
		/// </summary>
		/// <value>The index.</value>
		public int Index {
			get {
				return index;
			}
			set {
				index = value;
			}
		}

		/// <summary>
		/// Gets the start offset.
		/// </summary>
		/// <value>The start offset.</value>
		public long StartOffset {
			get {
				if (this.cil.Count > 0)
					return this.cil [0].Offset;

				if (this.InstructionsCount > 0)
					return this [0].StartOffset;

				return 0;
			}
		}

		/// <summary>
		/// Gets the end offset.
		/// </summary>
		/// <value>The end offset.</value>
		public long EndOffset {
			get {
				if (this.cil.Count > 0)
					return this.cil [this.cil.Count - 1].Offset;

				if (this.InstructionsCount > 0)
					return this [this.InstructionsCount - 1].EndOffset;

				return 0;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Block"/> class.
		/// </summary>
		/// <param name="method">The method.</param>
		public Block (Method method)
		{
			this.method = method;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Block"/> class.
		/// </summary>
		public Block ()
		{
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
		/// </returns>
		IEnumerator<SharpOS.AOT.IR.Instructions.Instruction> IEnumerable<SharpOS.AOT.IR.Instructions.Instruction>.GetEnumerator ()
		{
			foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in this.instructions) 
				yield return instruction;
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return ((IEnumerable<SharpOS.AOT.IR.Instructions.Instruction>) this).GetEnumerator ();
		}

		public delegate void BlockVisitor (Block block);

		/// <summary>
		/// Visits the specified visitor.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		public void Visit (BlockVisitor visitor)
		{
			foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in this.instructions) 
				instruction.VisitBlock (visitor);

			visitor (this);
		}

		/// <summary>
		/// Gets the stack delta.
		/// </summary>
		/// <param name="instruction">The instruction.</param>
		/// <returns></returns>
		private int GetStackDelta (Mono.Cecil.Cil.Instruction instruction)
		{
			int result = 0;

			switch (instruction.OpCode.StackBehaviourPop) {
				case StackBehaviour.PopAll:
					result = -this.stack;
					break;

				case StackBehaviour.Pop0:
					break;

				case StackBehaviour.Pop1:
				case StackBehaviour.Popi:
				case StackBehaviour.Popref:
					result--;
					break;

				case StackBehaviour.Pop1_pop1:
				case StackBehaviour.Popi_pop1:
				case StackBehaviour.Popi_popi:
				case StackBehaviour.Popi_popi8:
				case StackBehaviour.Popi_popr4:
				case StackBehaviour.Popi_popr8:
				case StackBehaviour.Popref_pop1:
				case StackBehaviour.Popref_popi:
					result -= 2;
					break;

				case StackBehaviour.Popi_popi_popi:
				case StackBehaviour.Popref_popi_popi:
				case StackBehaviour.Popref_popi_popi8:
				case StackBehaviour.Popref_popi_popr4:
				case StackBehaviour.Popref_popi_popr8:
				case StackBehaviour.Popref_popi_popref:
					result -= 3;
					break;

				case StackBehaviour.Varpop:
					if (instruction.OpCode.Value == OpCodes.Ret.Value) {
						if (this.method.MethodDefinition.ReturnType.ReturnType.FullName != Constants.Void)
							result--;

					} else if (instruction.OpCode.FlowControl == FlowControl.Call) {
						Mono.Cecil.MethodReference calledMethod = instruction.Operand as Mono.Cecil.MethodReference;

						result -= calledMethod.Parameters.Count;

						if (calledMethod.HasThis && instruction.OpCode.Value != OpCodes.Newobj.Value)
							result--;
					}
					break;

				default:
					throw new Exception ("Not implemented.");
			}

			switch (instruction.OpCode.StackBehaviourPush) {
				case StackBehaviour.Push0:
					break;

				case StackBehaviour.Push1:
				case StackBehaviour.Pushi:
				case StackBehaviour.Pushi8:
				case StackBehaviour.Pushr4:
				case StackBehaviour.Pushr8:
				case StackBehaviour.Pushref:
					result++;
					break;

				case StackBehaviour.Push1_push1:
					result += 2;
					break;

				case StackBehaviour.Varpush:
					if (instruction.OpCode.FlowControl == FlowControl.Call) {
						Mono.Cecil.MethodReference calledMethod = instruction.Operand as Mono.Cecil.MethodReference;

						if (calledMethod.ReturnType.ReturnType.FullName != Constants.Void)
							result++;
					}

					break;
				default:
					throw new Exception ("Not implemented.");
			}

			return result;
		}

		private Register SetRegister (int value)
		{
			if (value < 0)
				throw new Exception ("The register number may not be negative. ('" + this.method.MethodFullName + "')");

			return new Register (value);
		}

		private Register GetRegister (int value)
		{
			if (value < 0)
				throw new Exception ("The register number may not be negative. ('" + this.method.MethodFullName + "')");

			return new Register (value);
		}

		private Argument GetArgument (int value)
		{
			return this.Method.GetArgument (value);
		}

		private Argument SetArgument (int value)
		{
			return this.Method.GetArgument (value);
		}

		private Local GetLocal (int value)
		{
			return this.method.GetLocal (value);
		}

		private Local SetLocal (int value)
		{
			return this.method.GetLocal (value);
		}

		/// <summary>
		/// Converts from CIL.
		/// </summary>
		public void ConvertFromCIL ()
		{
			this.stack = 0;
			bool found = false;

			// We get the number of stack values from one of the blocks that has been processed and lead to the current one.
			foreach (Block _in in this.ins)
				if (_in.converted) {
					found = true;

					this.stack = _in.stack;
				}

			if (!found && this.ins.Count > 0)
				throw new Exception ("The conversion from CIL in '" + this.method.MethodFullName + "' for block #'" + this.index + "' failed.");

			this.instructions = new List <SharpOS.AOT.IR.Instructions.Instruction> ();

			// Catch
			if (this.method.MethodDefinition.Body.ExceptionHandlers.Count > 0 && this.cil.Count > 0) {
				foreach (ExceptionHandler exceptionHandler in this.method.MethodDefinition.Body.ExceptionHandlers) {
					if (exceptionHandler.CatchType != null
							&& exceptionHandler.HandlerStart == this.cil[0]) {
						this.AddInstruction (new Assign (this.SetRegister (stack++), new ExceptionValue()));
						break;
					}
				}
			}

			foreach (Mono.Cecil.Cil.Instruction cilInstruction in this.cil) {

				SharpOS.AOT.IR.Instructions.Instruction instruction = null;
#if true
#if true
				instruction = Block.ilDispatcher [(int) cilInstruction.OpCode.Code](this, cilInstruction);
#else
				switch (cilInstruction.OpCode.Code)
				{
					case Code.Nop:
						instruction = this.Nop(cilInstruction);
						break;

					case Code.Break:
						instruction = this.Break(cilInstruction);
						break;

					case Code.Ldarg_0:
						instruction = this.Ldarg_0(cilInstruction);
						break;

					case Code.Ldarg_1:
						instruction = this.Ldarg_1(cilInstruction);
						break;

					case Code.Ldarg_2:
						instruction = this.Ldarg_2(cilInstruction);
						break;

					case Code.Ldarg_3:
						instruction = this.Ldarg_3(cilInstruction);
						break;

					case Code.Ldloc_0:
						instruction = this.Ldloc_0(cilInstruction);
						break;

					case Code.Ldloc_1:
						instruction = this.Ldloc_1(cilInstruction);
						break;

					case Code.Ldloc_2:
						instruction = this.Ldloc_2(cilInstruction);
						break;

					case Code.Ldloc_3:
						instruction = this.Ldloc_3(cilInstruction);
						break;

					case Code.Stloc_0:
						instruction = this.Stloc_0(cilInstruction);
						break;

					case Code.Stloc_1:
						instruction = this.Stloc_1(cilInstruction);
						break;

					case Code.Stloc_2:
						instruction = this.Stloc_2(cilInstruction);
						break;

					case Code.Stloc_3:
						instruction = this.Stloc_3(cilInstruction);
						break;

					case Code.Ldarg_S:
						instruction = this.Ldarg_S(cilInstruction);
						break;

					case Code.Ldarga_S:
						instruction = this.Ldarga_S(cilInstruction);
						break;

					case Code.Starg_S:
						instruction = this.Starg_S(cilInstruction);
						break;

					case Code.Ldloc_S:
						instruction = this.Ldloc_S(cilInstruction);
						break;

					case Code.Ldloca_S:
						instruction = this.Ldloca_S(cilInstruction);
						break;

					case Code.Stloc_S:
						instruction = this.Stloc_S(cilInstruction);
						break;

					case Code.Ldnull:
						instruction = this.Ldnull(cilInstruction);
						break;

					case Code.Ldc_I4_M1:
						instruction = this.Ldc_I4_M1(cilInstruction);
						break;

					case Code.Ldc_I4_0:
						instruction = this.Ldc_I4_0(cilInstruction);
						break;

					case Code.Ldc_I4_1:
						instruction = this.Ldc_I4_1(cilInstruction);
						break;

					case Code.Ldc_I4_2:
						instruction = this.Ldc_I4_2(cilInstruction);
						break;

					case Code.Ldc_I4_3:
						instruction = this.Ldc_I4_3(cilInstruction);
						break;

					case Code.Ldc_I4_4:
						instruction = this.Ldc_I4_4(cilInstruction);
						break;

					case Code.Ldc_I4_5:
						instruction = this.Ldc_I4_5(cilInstruction);
						break;

					case Code.Ldc_I4_6:
						instruction = this.Ldc_I4_6(cilInstruction);
						break;

					case Code.Ldc_I4_7:
						instruction = this.Ldc_I4_7(cilInstruction);
						break;

					case Code.Ldc_I4_8:
						instruction = this.Ldc_I4_8(cilInstruction);
						break;

					case Code.Ldc_I4_S:
						instruction = this.Ldc_I4_S(cilInstruction);
						break;

					case Code.Ldc_I4:
						instruction = this.Ldc_I4(cilInstruction);
						break;

					case Code.Ldc_I8:
						instruction = this.Ldc_I8(cilInstruction);
						break;

					case Code.Ldc_R4:
						instruction = this.Ldc_R4(cilInstruction);
						break;

					case Code.Ldc_R8:
						instruction = this.Ldc_R8(cilInstruction);
						break;

					case Code.Dup:
						instruction = this.Dup(cilInstruction);
						break;

					case Code.Pop:
						instruction = this.Pop(cilInstruction);
						break;

					case Code.Jmp:
						instruction = this.Jmp(cilInstruction);
						break;

					case Code.Call:
						instruction = this.Call(cilInstruction);
						break;

					case Code.Calli:
						instruction = this.Calli(cilInstruction);
						break;

					case Code.Ret:
						instruction = this.Ret(cilInstruction);
						break;

					case Code.Br_S:
						instruction = this.Br_S(cilInstruction);
						break;

					case Code.Brfalse_S:
						instruction = this.Brfalse_S(cilInstruction);
						break;

					case Code.Brtrue_S:
						instruction = this.Brtrue_S(cilInstruction);
						break;

					case Code.Beq_S:
						instruction = this.Beq_S(cilInstruction);
						break;

					case Code.Bge_S:
						instruction = this.Bge_S(cilInstruction);
						break;

					case Code.Bgt_S:
						instruction = this.Bgt_S(cilInstruction);
						break;

					case Code.Ble_S:
						instruction = this.Ble_S(cilInstruction);
						break;

					case Code.Blt_S:
						instruction = this.Blt_S(cilInstruction);
						break;

					case Code.Bne_Un_S:
						instruction = this.Bne_Un_S(cilInstruction);
						break;

					case Code.Bge_Un_S:
						instruction = this.Bge_Un_S(cilInstruction);
						break;

					case Code.Bgt_Un_S:
						instruction = this.Bgt_Un_S(cilInstruction);
						break;

					case Code.Ble_Un_S:
						instruction = this.Ble_Un_S(cilInstruction);
						break;

					case Code.Blt_Un_S:
						instruction = this.Blt_Un_S(cilInstruction);
						break;

					case Code.Br:
						instruction = this.Br(cilInstruction);
						break;

					case Code.Brfalse:
						instruction = this.Brfalse(cilInstruction);
						break;

					case Code.Brtrue:
						instruction = this.Brtrue(cilInstruction);
						break;

					case Code.Beq:
						instruction = this.Beq(cilInstruction);
						break;

					case Code.Bge:
						instruction = this.Bge(cilInstruction);
						break;

					case Code.Bgt:
						instruction = this.Bgt(cilInstruction);
						break;

					case Code.Ble:
						instruction = this.Ble(cilInstruction);
						break;

					case Code.Blt:
						instruction = this.Blt(cilInstruction);
						break;

					case Code.Bne_Un:
						instruction = this.Bne_Un(cilInstruction);
						break;

					case Code.Bge_Un:
						instruction = this.Bge_Un(cilInstruction);
						break;

					case Code.Bgt_Un:
						instruction = this.Bgt_Un(cilInstruction);
						break;

					case Code.Ble_Un:
						instruction = this.Ble_Un(cilInstruction);
						break;

					case Code.Blt_Un:
						instruction = this.Blt_Un(cilInstruction);
						break;

					case Code.Switch:
						instruction = this.Switch(cilInstruction);
						break;

					case Code.Ldind_I1:
						instruction = this.Ldind_I1(cilInstruction);
						break;

					case Code.Ldind_U1:
						instruction = this.Ldind_U1(cilInstruction);
						break;

					case Code.Ldind_I2:
						instruction = this.Ldind_I2(cilInstruction);
						break;

					case Code.Ldind_U2:
						instruction = this.Ldind_U2(cilInstruction);
						break;

					case Code.Ldind_I4:
						instruction = this.Ldind_I4(cilInstruction);
						break;

					case Code.Ldind_U4:
						instruction = this.Ldind_U4(cilInstruction);
						break;

					case Code.Ldind_I8:
						instruction = this.Ldind_I8(cilInstruction);
						break;

					case Code.Ldind_I:
						instruction = this.Ldind_I(cilInstruction);
						break;

					case Code.Ldind_R4:
						instruction = this.Ldind_R4(cilInstruction);
						break;

					case Code.Ldind_R8:
						instruction = this.Ldind_R8(cilInstruction);
						break;

					case Code.Ldind_Ref:
						instruction = this.Ldind_Ref(cilInstruction);
						break;

					case Code.Stind_Ref:
						instruction = this.Stind_Ref(cilInstruction);
						break;

					case Code.Stind_I1:
						instruction = this.Stind_I1(cilInstruction);
						break;

					case Code.Stind_I2:
						instruction = this.Stind_I2(cilInstruction);
						break;

					case Code.Stind_I4:
						instruction = this.Stind_I4(cilInstruction);
						break;

					case Code.Stind_I8:
						instruction = this.Stind_I8(cilInstruction);
						break;

					case Code.Stind_R4:
						instruction = this.Stind_R4(cilInstruction);
						break;

					case Code.Stind_R8:
						instruction = this.Stind_R8(cilInstruction);
						break;

					case Code.Add:
						instruction = this.Add(cilInstruction);
						break;

					case Code.Sub:
						instruction = this.Sub(cilInstruction);
						break;

					case Code.Mul:
						instruction = this.Mul(cilInstruction);
						break;

					case Code.Div:
						instruction = this.Div(cilInstruction);
						break;

					case Code.Div_Un:
						instruction = this.Div_Un(cilInstruction);
						break;

					case Code.Rem:
						instruction = this.Rem(cilInstruction);
						break;

					case Code.Rem_Un:
						instruction = this.Rem_Un(cilInstruction);
						break;

					case Code.And:
						instruction = this.And(cilInstruction);
						break;

					case Code.Or:
						instruction = this.Or(cilInstruction);
						break;

					case Code.Xor:
						instruction = this.Xor(cilInstruction);
						break;

					case Code.Shl:
						instruction = this.Shl(cilInstruction);
						break;

					case Code.Shr:
						instruction = this.Shr(cilInstruction);
						break;

					case Code.Shr_Un:
						instruction = this.Shr_Un(cilInstruction);
						break;

					case Code.Neg:
						instruction = this.Neg(cilInstruction);
						break;

					case Code.Not:
						instruction = this.Not(cilInstruction);
						break;

					case Code.Conv_I1:
						instruction = this.Conv_I1(cilInstruction);
						break;

					case Code.Conv_I2:
						instruction = this.Conv_I2(cilInstruction);
						break;

					case Code.Conv_I4:
						instruction = this.Conv_I4(cilInstruction);
						break;

					case Code.Conv_I8:
						instruction = this.Conv_I8(cilInstruction);
						break;

					case Code.Conv_R4:
						instruction = this.Conv_R4(cilInstruction);
						break;

					case Code.Conv_R8:
						instruction = this.Conv_R8(cilInstruction);
						break;

					case Code.Conv_U4:
						instruction = this.Conv_U4(cilInstruction);
						break;

					case Code.Conv_U8:
						instruction = this.Conv_U8(cilInstruction);
						break;

					case Code.Callvirt:
						instruction = this.Callvirt(cilInstruction);
						break;

					case Code.Cpobj:
						instruction = this.Cpobj(cilInstruction);
						break;

					case Code.Ldobj:
						instruction = this.Ldobj(cilInstruction);
						break;

					case Code.Ldstr:
						instruction = this.Ldstr(cilInstruction);
						break;

					case Code.Newobj:
						instruction = this.Newobj(cilInstruction);
						break;

					case Code.Castclass:
						instruction = this.Castclass(cilInstruction);
						break;

					case Code.Isinst:
						instruction = this.Isinst(cilInstruction);
						break;

					case Code.Conv_R_Un:
						instruction = this.Conv_R_Un(cilInstruction);
						break;

					case Code.Unbox:
						instruction = this.Unbox(cilInstruction);
						break;

					case Code.Throw:
						instruction = this.Throw(cilInstruction);
						break;

					case Code.Ldfld:
						instruction = this.Ldfld(cilInstruction);
						break;

					case Code.Ldflda:
						instruction = this.Ldflda(cilInstruction);
						break;

					case Code.Stfld:
						instruction = this.Stfld(cilInstruction);
						break;

					case Code.Ldsfld:
						instruction = this.Ldsfld(cilInstruction);
						break;

					case Code.Ldsflda:
						instruction = this.Ldsflda(cilInstruction);
						break;

					case Code.Stsfld:
						instruction = this.Stsfld(cilInstruction);
						break;

					case Code.Stobj:
						instruction = this.Stobj(cilInstruction);
						break;

					case Code.Conv_Ovf_I1_Un:
						instruction = this.Conv_Ovf_I1_Un(cilInstruction);
						break;

					case Code.Conv_Ovf_I2_Un:
						instruction = this.Conv_Ovf_I2_Un(cilInstruction);
						break;

					case Code.Conv_Ovf_I4_Un:
						instruction = this.Conv_Ovf_I4_Un(cilInstruction);
						break;

					case Code.Conv_Ovf_I8_Un:
						instruction = this.Conv_Ovf_I8_Un(cilInstruction);
						break;

					case Code.Conv_Ovf_U1_Un:
						instruction = this.Conv_Ovf_U1_Un(cilInstruction);
						break;

					case Code.Conv_Ovf_U2_Un:
						instruction = this.Conv_Ovf_U2_Un(cilInstruction);
						break;

					case Code.Conv_Ovf_U4_Un:
						instruction = this.Conv_Ovf_U4_Un(cilInstruction);
						break;

					case Code.Conv_Ovf_U8_Un:
						instruction = this.Conv_Ovf_U8_Un(cilInstruction);
						break;

					case Code.Conv_Ovf_I_Un:
						instruction = this.Conv_Ovf_I_Un(cilInstruction);
						break;

					case Code.Conv_Ovf_U_Un:
						instruction = this.Conv_Ovf_U_Un(cilInstruction);
						break;

					case Code.Box:
						instruction = this.Box(cilInstruction);
						break;

					case Code.Newarr:
						instruction = this.Newarr(cilInstruction);
						break;

					case Code.Ldlen:
						instruction = this.Ldlen(cilInstruction);
						break;

					case Code.Ldelema:
						instruction = this.Ldelema(cilInstruction);
						break;

					case Code.Ldelem_I1:
						instruction = this.Ldelem_I1(cilInstruction);
						break;

					case Code.Ldelem_U1:
						instruction = this.Ldelem_U1(cilInstruction);
						break;

					case Code.Ldelem_I2:
						instruction = this.Ldelem_I2(cilInstruction);
						break;

					case Code.Ldelem_U2:
						instruction = this.Ldelem_U2(cilInstruction);
						break;

					case Code.Ldelem_I4:
						instruction = this.Ldelem_I4(cilInstruction);
						break;

					case Code.Ldelem_U4:
						instruction = this.Ldelem_U4(cilInstruction);
						break;

					case Code.Ldelem_I8:
						instruction = this.Ldelem_I8(cilInstruction);
						break;

					case Code.Ldelem_I:
						instruction = this.Ldelem_I(cilInstruction);
						break;

					case Code.Ldelem_R4:
						instruction = this.Ldelem_R4(cilInstruction);
						break;

					case Code.Ldelem_R8:
						instruction = this.Ldelem_R8(cilInstruction);
						break;

					case Code.Ldelem_Ref:
						instruction = this.Ldelem_Ref(cilInstruction);
						break;

					case Code.Stelem_I:
						instruction = this.Stelem_I(cilInstruction);
						break;

					case Code.Stelem_I1:
						instruction = this.Stelem_I1(cilInstruction);
						break;

					case Code.Stelem_I2:
						instruction = this.Stelem_I2(cilInstruction);
						break;

					case Code.Stelem_I4:
						instruction = this.Stelem_I4(cilInstruction);
						break;

					case Code.Stelem_I8:
						instruction = this.Stelem_I8(cilInstruction);
						break;

					case Code.Stelem_R4:
						instruction = this.Stelem_R4(cilInstruction);
						break;

					case Code.Stelem_R8:
						instruction = this.Stelem_R8(cilInstruction);
						break;

					case Code.Stelem_Ref:
						instruction = this.Stelem_Ref(cilInstruction);
						break;

					case Code.Ldelem_Any:
						instruction = this.Ldelem_Any(cilInstruction);
						break;

					case Code.Stelem_Any:
						instruction = this.Stelem_Any(cilInstruction);
						break;

					case Code.Unbox_Any:
						instruction = this.Unbox_Any(cilInstruction);
						break;

					case Code.Conv_Ovf_I1:
						instruction = this.Conv_Ovf_I1(cilInstruction);
						break;

					case Code.Conv_Ovf_U1:
						instruction = this.Conv_Ovf_U1(cilInstruction);
						break;

					case Code.Conv_Ovf_I2:
						instruction = this.Conv_Ovf_I2(cilInstruction);
						break;

					case Code.Conv_Ovf_U2:
						instruction = this.Conv_Ovf_U2(cilInstruction);
						break;

					case Code.Conv_Ovf_I4:
						instruction = this.Conv_Ovf_I4(cilInstruction);
						break;

					case Code.Conv_Ovf_U4:
						instruction = this.Conv_Ovf_U4(cilInstruction);
						break;

					case Code.Conv_Ovf_I8:
						instruction = this.Conv_Ovf_I8(cilInstruction);
						break;

					case Code.Conv_Ovf_U8:
						instruction = this.Conv_Ovf_U8(cilInstruction);
						break;

					case Code.Refanyval:
						instruction = this.Refanyval(cilInstruction);
						break;

					case Code.Ckfinite:
						instruction = this.Ckfinite(cilInstruction);
						break;

					case Code.Mkrefany:
						instruction = this.Mkrefany(cilInstruction);
						break;

					case Code.Ldtoken:
						instruction = this.Ldtoken(cilInstruction);
						break;

					case Code.Conv_U2:
						instruction = this.Conv_U2(cilInstruction);
						break;

					case Code.Conv_U1:
						instruction = this.Conv_U1(cilInstruction);
						break;

					case Code.Conv_I:
						instruction = this.Conv_I(cilInstruction);
						break;

					case Code.Conv_Ovf_I:
						instruction = this.Conv_Ovf_I(cilInstruction);
						break;

					case Code.Conv_Ovf_U:
						instruction = this.Conv_Ovf_U(cilInstruction);
						break;

					case Code.Add_Ovf:
						instruction = this.Add_Ovf(cilInstruction);
						break;

					case Code.Add_Ovf_Un:
						instruction = this.Add_Ovf_Un(cilInstruction);
						break;

					case Code.Mul_Ovf:
						instruction = this.Mul_Ovf(cilInstruction);
						break;

					case Code.Mul_Ovf_Un:
						instruction = this.Mul_Ovf_Un(cilInstruction);
						break;

					case Code.Sub_Ovf:
						instruction = this.Sub_Ovf(cilInstruction);
						break;

					case Code.Sub_Ovf_Un:
						instruction = this.Sub_Ovf_Un(cilInstruction);
						break;

					case Code.Endfinally:
						instruction = this.Endfinally(cilInstruction);
						break;

					case Code.Leave:
						instruction = this.Leave(cilInstruction);
						break;

					case Code.Leave_S:
						instruction = this.Leave_S(cilInstruction);
						break;

					case Code.Stind_I:
						instruction = this.Stind_I(cilInstruction);
						break;

					case Code.Conv_U:
						instruction = this.Conv_U(cilInstruction);
						break;

					case Code.Arglist:
						instruction = this.Arglist(cilInstruction);
						break;

					case Code.Ceq:
						instruction = this.Ceq(cilInstruction);
						break;

					case Code.Cgt:
						instruction = this.Cgt(cilInstruction);
						break;

					case Code.Cgt_Un:
						instruction = this.Cgt_Un(cilInstruction);
						break;

					case Code.Clt:
						instruction = this.Clt(cilInstruction);
						break;

					case Code.Clt_Un:
						instruction = this.Clt_Un(cilInstruction);
						break;

					case Code.Ldftn:
						instruction = this.Ldftn(cilInstruction);
						break;

					case Code.Ldvirtftn:
						instruction = this.Ldvirtftn(cilInstruction);
						break;

					case Code.Ldarg:
						instruction = this.Ldarg(cilInstruction);
						break;

					case Code.Ldarga:
						instruction = this.Ldarga(cilInstruction);
						break;

					case Code.Starg:
						instruction = this.Starg(cilInstruction);
						break;

					case Code.Ldloc:
						instruction = this.Ldloc(cilInstruction);
						break;

					case Code.Ldloca:
						instruction = this.Ldloca(cilInstruction);
						break;

					case Code.Stloc:
						instruction = this.Stloc(cilInstruction);
						break;

					case Code.Localloc:
						instruction = this.Localloc(cilInstruction);
						break;

					case Code.Endfilter:
						instruction = this.Endfilter(cilInstruction);
						break;

					case Code.Unaligned:
						instruction = this.Unaligned(cilInstruction);
						break;

					case Code.Volatile:
						instruction = this.Volatile(cilInstruction);
						break;

					case Code.Tail:
						instruction = this.Tail(cilInstruction);
						break;

					case Code.Initobj:
						instruction = this.Initobj(cilInstruction);
						break;

					case Code.Constrained:
						instruction = this.Constrained(cilInstruction);
						break;

					case Code.Cpblk:
						instruction = this.Cpblk(cilInstruction);
						break;

					case Code.Initblk:
						instruction = this.Initblk(cilInstruction);
						break;

					case Code.No:
						instruction = this.No(cilInstruction);
						break;

					case Code.Rethrow:
						instruction = this.Rethrow(cilInstruction);
						break;

					case Code.Sizeof:
						instruction = this.Sizeof(cilInstruction);
						break;

					case Code.Refanytype:
						instruction = this.Refanytype(cilInstruction);
						break;

					case Code.Readonly:
						instruction = this.Readonly(cilInstruction);
						break;

					default:
						throw new Exception ("Instruction '" + cilInstruction.OpCode.Name + "' is not implemented. (Found in '" + this.method.MethodFullName + "')");
				}
#endif
#else					
				if (cilInstruction.OpCode == OpCodes.Nop) {
					continue;
				} 
				
				// Convert
				else if (cilInstruction.OpCode == OpCodes.Conv_I) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_I);

				} else if (cilInstruction.OpCode == OpCodes.Conv_I1) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_I1);

				} else if (cilInstruction.OpCode == OpCodes.Conv_I2) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_I2);

				} else if (cilInstruction.OpCode == OpCodes.Conv_I4) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_I4);

				} else if (cilInstruction.OpCode == OpCodes.Conv_I8) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_I8);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I_Un) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I_Un);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I1) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I1);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I1_Un) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I1_Un);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I2) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I2);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I2_Un) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I2_Un);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I4) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I4);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I4_Un) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I4_Un);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I8) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I8);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I8_Un) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I8_Un);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U_Un) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U_Un);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U1) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U1);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U1_Un) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U1_Un);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U2) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U2);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U2_Un) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U2_Un);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U4) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U4);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U4_Un) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U4_Un);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U8) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U8);

				} else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U8_Un) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U8_Un);

				} else if (cilInstruction.OpCode == OpCodes.Conv_R_Un) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_R_Un);

				} else if (cilInstruction.OpCode == OpCodes.Conv_R4) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_R4);

				} else if (cilInstruction.OpCode == OpCodes.Conv_R8) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_R8);

				} else if (cilInstruction.OpCode == OpCodes.Conv_U) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_U);

				} else if (cilInstruction.OpCode == OpCodes.Conv_U1) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_U1);

				} else if (cilInstruction.OpCode == OpCodes.Conv_U2) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_U2);

				} else if (cilInstruction.OpCode == OpCodes.Conv_U4) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_U4);

				} else if (cilInstruction.OpCode == OpCodes.Conv_U8) {
					instruction = this.Convert (Operands.Operand.ConvertType.Conv_U8);
				}

				// Arithmetic
				else if (cilInstruction.OpCode == OpCodes.Add) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.Add), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Add_Ovf) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.AddSignedWithOverflowCheck), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Add_Ovf_Un) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.AddUnsignedWithOverflowCheck), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Sub) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.Sub), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Sub_Ovf) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.SubSignedWithOverflowCheck), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Sub_Ovf_Un) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.SubUnsignedWithOverflowCheck), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Mul) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.Mul), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Mul_Ovf) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.MulSignedWithOverflowCheck), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Mul_Ovf_Un) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.MulUnsignedWithOverflowCheck), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Div) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.Div), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Div_Un) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.DivUnsigned), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Rem) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.Remainder), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Rem_Un) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.RemainderUnsigned), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Neg) {
					instruction = new Assign (this.Register (stack - 1), new Arithmetic (new Unary (Operator.UnaryType.Negation), this.Register (stack - 1)));
				}

				// Bitwise
				else if (cilInstruction.OpCode == OpCodes.And) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.And), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Or) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.Or), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Xor) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.Xor), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Not) {
					instruction = new Assign (this.Register (stack - 1), new Arithmetic (new Unary (Operator.UnaryType.Not), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Shl) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.SHL), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Shr) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.SHR), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Shr_Un) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Binary (Operator.BinaryType.SHRUnsigned), this.Register (stack - 2), this.Register (stack - 1)));
				}

				// Branch
				else if (cilInstruction.OpCode == OpCodes.Beq || cilInstruction.OpCode == OpCodes.Beq_S) {
					instruction = new ConditionalJump (new SharpOS.AOT.IR.Operands.Boolean (new Relational (Operator.RelationalType.Equal), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Bge || cilInstruction.OpCode == OpCodes.Bge_S) {
					instruction = new ConditionalJump (new SharpOS.AOT.IR.Operands.Boolean (new Relational (Operator.RelationalType.GreaterThanOrEqual), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Bge_Un || cilInstruction.OpCode == OpCodes.Bge_Un_S) {
					instruction = new ConditionalJump (new SharpOS.AOT.IR.Operands.Boolean (new Relational (Operator.RelationalType.GreaterThanOrEqualUnsignedOrUnordered), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Bgt || cilInstruction.OpCode == OpCodes.Bgt_S) {
					instruction = new ConditionalJump (new SharpOS.AOT.IR.Operands.Boolean (new Relational (Operator.RelationalType.GreaterThan), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Bgt_Un || cilInstruction.OpCode == OpCodes.Bgt_Un_S) {
					instruction = new ConditionalJump (new SharpOS.AOT.IR.Operands.Boolean (new Relational (Operator.RelationalType.GreaterThanUnsignedOrUnordered), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Ble || cilInstruction.OpCode == OpCodes.Ble_S) {
					instruction = new ConditionalJump (new SharpOS.AOT.IR.Operands.Boolean (new Relational (Operator.RelationalType.LessThanOrEqual), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Ble_Un || cilInstruction.OpCode == OpCodes.Ble_Un_S) {
					instruction = new ConditionalJump (new SharpOS.AOT.IR.Operands.Boolean (new Relational (Operator.RelationalType.LessThanOrEqualUnsignedOrUnordered), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Blt || cilInstruction.OpCode == OpCodes.Blt_S) {
					instruction = new ConditionalJump (new SharpOS.AOT.IR.Operands.Boolean (new Relational (Operator.RelationalType.LessThan), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Blt_Un || cilInstruction.OpCode == OpCodes.Blt_Un_S) {
					instruction = new ConditionalJump (new SharpOS.AOT.IR.Operands.Boolean (new Relational (Operator.RelationalType.LessThanUnsignedOrUnordered), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Bne_Un || cilInstruction.OpCode == OpCodes.Bne_Un_S) {
					instruction = new ConditionalJump (new SharpOS.AOT.IR.Operands.Boolean (new Relational (Operator.RelationalType.NotEqualOrUnordered), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Brfalse || cilInstruction.OpCode == OpCodes.Brfalse_S) {
					instruction = new ConditionalJump (new SharpOS.AOT.IR.Operands.Boolean (new SharpOS.AOT.IR.Operators.Boolean (Operator.BooleanType.False), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Brtrue || cilInstruction.OpCode == OpCodes.Brtrue_S) {
					instruction = new ConditionalJump (new SharpOS.AOT.IR.Operands.Boolean (new SharpOS.AOT.IR.Operators.Boolean (Operator.BooleanType.True), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Br || cilInstruction.OpCode == OpCodes.Br_S) {
					instruction = new Jump();

				} else if (cilInstruction.OpCode == OpCodes.Leave
						|| cilInstruction.OpCode == OpCodes.Leave_S
						|| cilInstruction.OpCode == OpCodes.Endfinally) {
					instruction = new Jump();

				} else if (cilInstruction.OpCode == OpCodes.Throw) {
					instruction = new SharpOS.AOT.IR.Instructions.System (new SharpOS.AOT.IR.Operands.Miscellaneous (new SharpOS.AOT.IR.Operators.Miscellaneous (Operator.MiscellaneousType.Throw), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Rethrow) {
					instruction = new SharpOS.AOT.IR.Instructions.System (new SharpOS.AOT.IR.Operands.Miscellaneous (new SharpOS.AOT.IR.Operators.Miscellaneous (Operator.MiscellaneousType.Throw)));
				}

				// Misc
				else if (cilInstruction.OpCode == OpCodes.Ret) {
					if (this.method.MethodDefinition.ReturnType.ReturnType.FullName.Equals ("System.Void"))
						instruction = new Return();

					else if (stack > 0)
						instruction = new Return (this.Register (stack - 1));

					else
						instruction = new Return (this.Register (0));

				} else if (cilInstruction.OpCode == OpCodes.Switch) {
					instruction = new Switch (this.Register (stack - 1));

				} else if (cilInstruction.OpCode == OpCodes.Pop) {
					instruction = new Pop (this.Register (stack - 1));

				} else if (cilInstruction.OpCode == OpCodes.Dup) {
					instruction = new Assign (this.Register (stack), this.Register (stack - 1));

				} else if (cilInstruction.OpCode == OpCodes.Sizeof) {
					instruction = new Assign (this.Register (stack), new Constant (this.method.Engine.GetTypeSize (cilInstruction.Operand.ToString())));
					(instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;

				} else if (cilInstruction.OpCode == OpCodes.Localloc) {
					instruction = new Assign (this.Register (stack - 1), new SharpOS.AOT.IR.Operands.Miscellaneous (new Operators.Miscellaneous (Operator.MiscellaneousType.Localloc), this.Register (stack - 1)));
					(instruction as Assign).Assignee.SizeType = Operand.InternalSizeType.U;
				}

				// Check
				else if (cilInstruction.OpCode == OpCodes.Ceq) {
					instruction = new Assign (this.Register (stack - 2), new SharpOS.AOT.IR.Operands.Boolean (new SharpOS.AOT.IR.Operators.Relational (Operator.RelationalType.Equal), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Cgt) {
					instruction = new Assign (this.Register (stack - 2), new SharpOS.AOT.IR.Operands.Boolean (new SharpOS.AOT.IR.Operators.Relational (Operator.RelationalType.GreaterThan), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Cgt_Un) {
					instruction = new Assign (this.Register (stack - 2), new SharpOS.AOT.IR.Operands.Boolean (new SharpOS.AOT.IR.Operators.Relational (Operator.RelationalType.GreaterThanUnsignedOrUnordered), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Clt) {
					instruction = new Assign (this.Register (stack - 2), new SharpOS.AOT.IR.Operands.Boolean (new SharpOS.AOT.IR.Operators.Relational (Operator.RelationalType.LessThan), this.Register (stack - 2), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Clt_Un) {
					instruction = new Assign (this.Register (stack - 2), new SharpOS.AOT.IR.Operands.Boolean (new SharpOS.AOT.IR.Operators.Relational (Operator.RelationalType.LessThanUnsignedOrUnordered), this.Register (stack - 2), this.Register (stack - 1)));
				}

				// Load
				else if (cilInstruction.OpCode == OpCodes.Ldlen) {
					instruction = new Assign (this.Register (stack - 2), new Arithmetic (new Unary (Operator.UnaryType.ArraySize), this.Register (stack - 1)));

				} else if (cilInstruction.OpCode == OpCodes.Ldtoken) {
					instruction = new Assign (this.Register (stack), new Constant (cilInstruction.Operand));

				} else if (cilInstruction.OpCode == OpCodes.Ldnull) {
					// TODO change that to something else not "null"
					instruction = new Assign (this.Register (stack), new Constant ("null"));

				} else if (cilInstruction.OpCode == OpCodes.Ldstr) {
					//instruction = new Assign(this.Register(stack), new Constant("\"" + cilInstruction.Operand.ToString() + "\""));
					instruction = new Assign (this.Register (stack), new Constant (cilInstruction.Operand.ToString ()));

				} else if (cilInstruction.OpCode == OpCodes.Ldftn) {
					instruction = new Assign(this.Register(stack), new Operands.MethodReference(cilInstruction.Operand as Mono.Cecil.MethodReference));
				}

				// Load Constants
				else if (cilInstruction.OpCode == OpCodes.Ldc_I4_0) {
					instruction = new Assign (this.Register (stack), new Constant (0));
					(instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;

				} else if (cilInstruction.OpCode == OpCodes.Ldc_I4_1) {
					instruction = new Assign (this.Register (stack), new Constant (1));
					(instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;

				} else if (cilInstruction.OpCode == OpCodes.Ldc_I4_2) {
					instruction = new Assign (this.Register (stack), new Constant (2));
					(instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;

				} else if (cilInstruction.OpCode == OpCodes.Ldc_I4_3) {
					instruction = new Assign (this.Register (stack), new Constant (3));
					(instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;

				} else if (cilInstruction.OpCode == OpCodes.Ldc_I4_4) {
					instruction = new Assign (this.Register (stack), new Constant (4));
					(instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;

				} else if (cilInstruction.OpCode == OpCodes.Ldc_I4_5) {
					instruction = new Assign (this.Register (stack), new Constant (5));
					(instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;

				} else if (cilInstruction.OpCode == OpCodes.Ldc_I4_6) {
					instruction = new Assign (this.Register (stack), new Constant (6));
					(instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;

				} else if (cilInstruction.OpCode == OpCodes.Ldc_I4_7) {
					instruction = new Assign (this.Register (stack), new Constant (7));
					(instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;

				} else if (cilInstruction.OpCode == OpCodes.Ldc_I4_8) {
					instruction = new Assign (this.Register (stack), new Constant (8));
					(instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;

				} else if (cilInstruction.OpCode == OpCodes.Ldc_I4_M1) {
					instruction = new Assign (this.Register (stack), new Constant (-1));
					(instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;

				} else if (cilInstruction.OpCode == OpCodes.Ldc_I4_S) {
					instruction = new Assign (this.Register (stack), new Constant (cilInstruction.Operand));
					(instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;

				} else if (cilInstruction.OpCode == OpCodes.Ldc_I4) {
					instruction = new Assign (this.Register (stack), new Constant (cilInstruction.Operand));
					(instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;

				} else if (cilInstruction.OpCode == OpCodes.Ldc_I8) {
					instruction = new Assign (this.Register (stack), new Constant (cilInstruction.Operand));
					(instruction.Value as Constant).SizeType = Operand.InternalSizeType.I8;

				} else if (cilInstruction.OpCode == OpCodes.Ldc_R4) {
					instruction = new Assign (this.Register (stack), new Constant (cilInstruction.Operand));
					(instruction.Value as Constant).SizeType = Operand.InternalSizeType.R4;

				} else if (cilInstruction.OpCode == OpCodes.Ldc_R8) {
					instruction = new Assign (this.Register (stack), new Constant (cilInstruction.Operand));
					(instruction.Value as Constant).SizeType = Operand.InternalSizeType.R8;
				}

				// Indirect Load
				else if (cilInstruction.OpCode == OpCodes.Ldind_I) {
					Indirect indirect = new Indirect (this.Register (stack - 1));
					indirect.SizeType = Operand.InternalSizeType.I;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I;

					instruction = new Assign (register, indirect);

				} else if (cilInstruction.OpCode == OpCodes.Ldind_I1) {
					Indirect indirect = new Indirect (this.Register (stack - 1));
					indirect.SizeType = Operand.InternalSizeType.I1;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I1;

					instruction = new Assign (register, indirect);

				} else if (cilInstruction.OpCode == OpCodes.Ldind_I2) {
					Indirect indirect = new Indirect (this.Register (stack - 1));
					indirect.SizeType = Operand.InternalSizeType.I2;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I2;

					instruction = new Assign (register, indirect);

				} else if (cilInstruction.OpCode == OpCodes.Ldind_I4) {
					Indirect indirect = new Indirect (this.Register (stack - 1));
					indirect.SizeType = Operand.InternalSizeType.I4;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I4;

					instruction = new Assign (register, indirect);

				} else if (cilInstruction.OpCode == OpCodes.Ldind_I8) {
					Indirect indirect = new Indirect (this.Register (stack - 1));
					indirect.SizeType = Operand.InternalSizeType.I8;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I8;

					instruction = new Assign (register, indirect);

				} else if (cilInstruction.OpCode == OpCodes.Ldind_R4) {
					Indirect indirect = new Indirect (this.Register (stack - 1));
					indirect.SizeType = Operand.InternalSizeType.R4;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.R4;

					instruction = new Assign (register, indirect);

				} else if (cilInstruction.OpCode == OpCodes.Ldind_R8) {
					Indirect indirect = new Indirect (this.Register (stack - 1));
					indirect.SizeType = Operand.InternalSizeType.R8;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.R8;

					instruction = new Assign (register, indirect);

				} else if (cilInstruction.OpCode == OpCodes.Ldind_U1) {
					Indirect indirect = new Indirect (this.Register (stack - 1));
					indirect.SizeType = Operand.InternalSizeType.U1;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.U1;

					instruction = new Assign (register, indirect);

				} else if (cilInstruction.OpCode == OpCodes.Ldind_U2) {
					Indirect indirect = new Indirect (this.Register (stack - 1));
					indirect.SizeType = Operand.InternalSizeType.U2;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.U2;

					instruction = new Assign (register, indirect);

				} else if (cilInstruction.OpCode == OpCodes.Ldind_U4) {
					Indirect indirect = new Indirect (this.Register (stack - 1));
					indirect.SizeType = Operand.InternalSizeType.U4;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.U4;

					instruction = new Assign (register, indirect);
				}

				// Object
				else if (cilInstruction.OpCode == OpCodes.Newobj) {
					Mono.Cecil.MethodReference call = (cilInstruction.Operand as Mono.Cecil.MethodReference);

					Operand [] operands = new Operand [call.Parameters.Count];

					for (int i = 0; i < call.Parameters.Count; i++)
						operands [i] = this.Register (stack - call.Parameters.Count + i);

					Register register = this.Register (stack - call.Parameters.Count);
					register.SizeType = Operand.InternalSizeType.I;

					instruction = new Assign (register, new SharpOS.AOT.IR.Operands.Call (call, operands));

					stack -= call.Parameters.Count;

				} else if (cilInstruction.OpCode == OpCodes.Ldobj) {
					Operands.Object _object = new Operands.Object ((cilInstruction.Operand as TypeReference).FullName, this.Register (stack - 1));
					_object.SizeType = Operand.InternalSizeType.ValueType;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.ValueType;

					instruction = new Assign (register, _object);

				} else if (cilInstruction.OpCode == OpCodes.Stobj) {
					Operands.Object _object = new Operands.Object ((cilInstruction.Operand as TypeReference).FullName, this.Register (stack - 2));
					_object.SizeType = Operand.InternalSizeType.ValueType;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.ValueType;

					instruction = new Assign (_object, register);

				} else if (cilInstruction.OpCode == OpCodes.Initobj) {

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.ValueType;

					instruction = new Initialize (register, cilInstruction.Operand.ToString ());
				}
				
				// Load Locales
				else if (cilInstruction.OpCode == OpCodes.Ldloca || cilInstruction.OpCode == OpCodes.Ldloca_S) {
					Address address = new Address (this.GetLocal ((cilInstruction.Operand as VariableDefinition).Index));
					address.SizeType = Operand.InternalSizeType.I;

					instruction = new Assign (this.Register (stack), address);

				} else if (cilInstruction.OpCode == OpCodes.Ldloc || cilInstruction.OpCode == OpCodes.Ldloc_S) {
					instruction = new Assign (this.Register (stack), this.GetLocal ((cilInstruction.Operand as VariableDefinition).Index));

				} else if (cilInstruction.OpCode == OpCodes.Ldloc_0) {
					instruction = new Assign (this.Register (stack), this.GetLocal (0));

				} else if (cilInstruction.OpCode == OpCodes.Ldloc_1) {
					instruction = new Assign (this.Register (stack), this.GetLocal (1));

				} else if (cilInstruction.OpCode == OpCodes.Ldloc_2) {
					instruction = new Assign (this.Register (stack), this.GetLocal (2));

				} else if (cilInstruction.OpCode == OpCodes.Ldloc_3) {
					instruction = new Assign (this.Register (stack), this.GetLocal (3));
				}

				// Indirect Store
				else if (cilInstruction.OpCode == OpCodes.Stind_I) {
					Indirect indirect = new Indirect (this.Register (stack - 2));
					indirect.SizeType = Operand.InternalSizeType.I;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I;

					instruction = new Assign (reference, register);

				} else if (cilInstruction.OpCode == OpCodes.Stind_I1) {
					Indirect indirect = new Indirect (this.Register (stack - 2));
					indirect.SizeType = Operand.InternalSizeType.I1;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I1;

					instruction = new Assign (reference, register);

				} else if (cilInstruction.OpCode == OpCodes.Stind_I2) {
					Indirect indirect = new Indirect (this.Register (stack - 2));
					indirect.SizeType = Operand.InternalSizeType.I2;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I2;

					instruction = new Assign (reference, register);

				} else if (cilInstruction.OpCode == OpCodes.Stind_I4) {
					Indirect indirect = new Indirect (this.Register (stack - 2));
					indirect.SizeType = Operand.InternalSizeType.I4;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I4;

					instruction = new Assign (reference, register);

				} else if (cilInstruction.OpCode == OpCodes.Stind_I8) {
					Indirect indirect = new Indirect (this.Register (stack - 2));
					indirect.SizeType = Operand.InternalSizeType.I8;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I8;

					instruction = new Assign (reference, register);

				} else if (cilInstruction.OpCode == OpCodes.Stind_R4) {
					Indirect indirect = new Indirect (this.Register (stack - 2));
					indirect.SizeType = Operand.InternalSizeType.R4;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.R4;

					instruction = new Assign (reference, register);

				} else if (cilInstruction.OpCode == OpCodes.Stind_R8) {
					Indirect indirect = new Indirect (this.Register (stack - 2));
					indirect.SizeType = Operand.InternalSizeType.R8;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.R8;

					instruction = new Assign (reference, register);
				}

				// Store Locales
				else if (cilInstruction.OpCode == OpCodes.Stloc || cilInstruction.OpCode == OpCodes.Stloc_S) {
					instruction = new Assign (this.GetLocal ((cilInstruction.Operand as VariableDefinition).Index), this.Register (stack - 1));

				} else if (cilInstruction.OpCode == OpCodes.Stloc_0) {
					instruction = new Assign (this.GetLocal (0), this.Register (stack - 1));

				} else if (cilInstruction.OpCode == OpCodes.Stloc_1) {
					instruction = new Assign (this.GetLocal (1), this.Register (stack - 1));

				} else if (cilInstruction.OpCode == OpCodes.Stloc_2) {
					instruction = new Assign (this.GetLocal (2), this.Register (stack - 1));

				} else if (cilInstruction.OpCode == OpCodes.Stloc_3) {
					instruction = new Assign (this.GetLocal (3), this.Register (stack - 1));
				}

				// Arguments Load
				else if (cilInstruction.OpCode == OpCodes.Ldarg) {
					instruction = new Assign (this.Register (stack), this.GetArgument ((cilInstruction.Operand as ParameterDefinition).Sequence));

				} else if (cilInstruction.OpCode == OpCodes.Ldarg_S) {
					instruction = new Assign (this.Register (stack), this.GetArgument ((cilInstruction.Operand as ParameterDefinition).Sequence));

				} else if (cilInstruction.OpCode == OpCodes.Ldarga || cilInstruction.OpCode == OpCodes.Ldarga_S) {
					if (cilInstruction.Operand is ParameterDefinition) {
						Address address = new Address (this.GetArgument ((cilInstruction.Operand as ParameterDefinition).Sequence));
						address.SizeType = Operand.InternalSizeType.I;

						instruction = new Assign (this.Register (stack), address);

					} else {
						Address address = new Address (this.GetArgument ((int) cilInstruction.Operand));
						address.SizeType = Operand.InternalSizeType.I;

						instruction = new Assign (this.Register (stack), address);
					}

				} else if (cilInstruction.OpCode == OpCodes.Ldarg_0) {
					instruction = new Assign (this.Register (stack), this.GetArgument (1));

				} else if (cilInstruction.OpCode == OpCodes.Ldarg_1) {
					instruction = new Assign (this.Register (stack), this.GetArgument (2));

				} else if (cilInstruction.OpCode == OpCodes.Ldarg_2) {
					instruction = new Assign (this.Register (stack), this.GetArgument (3));

				} else if (cilInstruction.OpCode == OpCodes.Ldarg_3) {
					instruction = new Assign (this.Register (stack), this.GetArgument (4));
				}

				// Argument Store
				else if (cilInstruction.OpCode == OpCodes.Starg || cilInstruction.OpCode == OpCodes.Starg_S) {
					instruction = new Assign (this.GetArgument ((cilInstruction.Operand as ParameterDefinition).Sequence), this.Register (stack - 1));
				}

				// Call
				else if (cilInstruction.OpCode == OpCodes.Call
						|| cilInstruction.OpCode == OpCodes.Callvirt
						|| cilInstruction.OpCode == OpCodes.Jmp) {

					Mono.Cecil.MethodReference call = (cilInstruction.Operand as Mono.Cecil.MethodReference);
					MethodDefinition def = this.Method.Engine.GetCILDefinition (call);

					if (def != null) {
						foreach (CustomAttribute attr in def.CustomAttributes) {
							if (attr.Constructor.DeclaringType.FullName ==
									typeof (SharpOS.AOT.Attributes.ADCStubAttribute)
									.FullName) {
								// replace this call with an equivalent call
								// to the ADC layer
								call = this.Method.Engine.FixupADCMethod (call);
							}
						}
					}

					// TODO: if def == null, only allow stubs past this, fail gracefully
					// otherwise.
					
					Operand [] operands;

					// If it is not static include the register of the instance into the operands
					if (call.HasThis)
						operands = new Operand [call.Parameters.Count + 1];

					else
						operands = new Operand [call.Parameters.Count];

					for (int i = 0; i < operands.Length; i++)
						operands [i] = this.Register (stack - operands.Length + i);

					if (call.ReturnType.ReturnType.FullName.Equals ("System.Void")) {
						instruction = new SharpOS.AOT.IR.Instructions.Call (new SharpOS.AOT.IR.Operands.Call (call, operands));

						stack--;

					} else
						instruction = new Assign (this.Register (stack - operands.Length), new SharpOS.AOT.IR.Operands.Call (call, operands));

					stack -= operands.Length;
				}

				// Field
				else if (cilInstruction.OpCode == OpCodes.Ldfld) {
					MemberReference field = cilInstruction.Operand as MemberReference;
					string fieldName = field.DeclaringType.FullName + "::" + field.Name;

					if ((field as FieldDefinition).IsStatic)
						instruction = new Assign (this.Register (stack - 1), new Field (fieldName));

					else
						instruction = new Assign (this.Register (stack - 1), new Field (fieldName, this.Register (stack - 1)));

					(instruction.Value as Identifier).SizeType = this.method.Engine.GetInternalType (fieldName);

				} else if (cilInstruction.OpCode == OpCodes.Ldflda) {
					FieldReference field = cilInstruction.Operand as FieldReference;
					string fieldName = field.DeclaringType.FullName + "::" + field.Name;

					instruction = new Assign(this.Register(stack - 1), new Operands.Address (new Field((cilInstruction.Operand as FieldDefinition).DeclaringType.FullName + "::" + (cilInstruction.Operand as FieldDefinition).Name, this.Register(stack - 1))));
					(instruction.Value as Identifier).SizeType = Operand.InternalSizeType.U;
					(instruction.Value as Address).Value.SizeType = this.method.Engine.GetInternalType (field.FieldType.FullName);

				} else if (cilInstruction.OpCode == OpCodes.Ldsfld) {
					FieldReference field = cilInstruction.Operand as FieldReference;
					string fieldName = field.DeclaringType.FullName + "::" + field.Name;

					instruction = new Assign (this.Register (stack), new Field (fieldName));
					(instruction.Value as Identifier).SizeType = this.method.Engine.GetInternalType (field.FieldType.FullName);

				} else if (cilInstruction.OpCode == OpCodes.Ldsflda) {
					FieldReference field = cilInstruction.Operand as FieldReference;
					string fieldName = field.DeclaringType.FullName + "::" + field.Name;

					instruction = new Assign (this.Register (stack), new Operands.Address (new Field ((cilInstruction.Operand as FieldReference).DeclaringType.FullName + "::" + (cilInstruction.Operand as FieldReference).Name)));
					(instruction.Value as Identifier).SizeType = Operand.InternalSizeType.U;
					(instruction.Value as Address).Value.SizeType = this.method.Engine.GetInternalType (field.FieldType.FullName);

				} else if (cilInstruction.OpCode == OpCodes.Stfld) {
					MemberReference field = cilInstruction.Operand as MemberReference;
					string fieldName = field.DeclaringType.FullName + "::" + field.Name;

					if ((field as FieldDefinition).IsStatic)
						instruction = new Assign (new Field (fieldName), this.Register (stack - 1));

					else
						instruction = new Assign (new Field (fieldName, this.Register (stack - 2)), this.Register (stack - 1));

					(instruction as Assign).Assignee.SizeType = this.method.Engine.GetInternalType (fieldName);

				} else if (cilInstruction.OpCode == OpCodes.Stsfld) {
					FieldReference field = cilInstruction.Operand as FieldReference;
					string fieldName = field.DeclaringType.FullName + "::" + field.Name;

					instruction = new Assign (new Field (fieldName), this.Register (stack - 1));
					(instruction as Assign).Assignee.SizeType = this.method.Engine.GetInternalType (field.FieldType.FullName);
				}

				// Array
				else if (cilInstruction.OpCode == OpCodes.Newarr) {
					// TODO managed/unmanaged
					throw new Exception ("Not implemented yet.");
					//instruction = new Assign (this.Register (stack - 1), new SharpOS.AOT.IR.Operands.Miscellaneous (new SharpOS.AOT.IR.Operators.Miscellaneous (Operator.MiscellaneousType.NewArray), cilInstruction.Operand ));

				} else if (cilInstruction.OpCode == OpCodes.Stelem_Ref
						|| cilInstruction.OpCode == OpCodes.Stelem_Any
						|| cilInstruction.OpCode == OpCodes.Stelem_I
						|| cilInstruction.OpCode == OpCodes.Stelem_I1
						|| cilInstruction.OpCode == OpCodes.Stelem_I2
						|| cilInstruction.OpCode == OpCodes.Stelem_I4
						|| cilInstruction.OpCode == OpCodes.Stelem_I8
						|| cilInstruction.OpCode == OpCodes.Stelem_R4
						|| cilInstruction.OpCode == OpCodes.Stelem_R8) {
					instruction = new Assign (new ArrayElement (this.Register (stack - 3), this.Register (stack - 2)), this.Register (stack - 1));

				} else if (cilInstruction.OpCode == OpCodes.Ldelem_Ref
						|| cilInstruction.OpCode == OpCodes.Ldelem_I
						|| cilInstruction.OpCode == OpCodes.Ldelem_I1
						|| cilInstruction.OpCode == OpCodes.Ldelem_I2
						|| cilInstruction.OpCode == OpCodes.Ldelem_I4
						|| cilInstruction.OpCode == OpCodes.Ldelem_I8
						|| cilInstruction.OpCode == OpCodes.Ldelem_R4
						|| cilInstruction.OpCode == OpCodes.Ldelem_R8
						|| cilInstruction.OpCode == OpCodes.Ldelem_U1
						|| cilInstruction.OpCode == OpCodes.Ldelem_U2
						|| cilInstruction.OpCode == OpCodes.Ldelem_U4
						|| cilInstruction.OpCode == OpCodes.Ldelem_Any
						|| cilInstruction.OpCode == OpCodes.Ldelema) {
					// TODO Signed/Unsigned
					// TODO ldelema -> new Address()
					instruction = new Assign (this.Register (stack - 1), new ArrayElement (this.Register (stack - 2), this.Register (stack - 1)));

				} else
					throw new Exception ("Instruction '" + cilInstruction.OpCode.Name + "' is not implemented. (Found in '" + this.method.MethodFullName + "')");
#endif

				if (instruction != null)
					this.AddInstruction (instruction);

				this.stack += GetStackDelta (cilInstruction);

				if (this.stack < 0)
					throw new Exception (string.Format ("The stack is negative in '{0}'", this.method.MethodFullName));
			}

			this.converted = true;
		}

		/// <summary>
		/// Merges the specified block.
		/// </summary>
		/// <param name="block">The block.</param>
		public void Merge (Block block)
		{
			// Remove the jump that connects this block with the block parameter
			//if (this.type == BlockType.OneWay) 
			if (this.outs.Count == 1)
				this.cil.Remove (this.cil[this.cil.Count - 1]);

			this.outs = block.outs;

			// Add the instructions of the block parameter
			foreach (Mono.Cecil.Cil.Instruction instruction in block.cil) 
				this.cil.Add (instruction);

			foreach (Block _out in block.outs) {
				for (int i = 0; i < _out.ins.Count; i++) {
					if (_out.ins [i] == block)
						_out.ins [i] = this;
				}
			}
		}

		/// <summary>
		/// Inserts the instruction.
		/// </summary>
		/// <param name="position">The position.</param>
		/// <param name="instruction">The instruction.</param>
		public void InsertInstruction (int position, SharpOS.AOT.IR.Instructions.Instruction instruction)
		{
			instruction.Block = this;

			this.instructions.Insert (position, instruction);
		}

		/// <summary>
		/// Sets the instruction.
		/// </summary>
		/// <param name="position">The position.</param>
		/// <param name="instruction">The instruction.</param>
		public void SetInstruction (int position, SharpOS.AOT.IR.Instructions.Instruction instruction)
		{
			instruction.Block = this;

			for (int i = 0; i < this.instructions.Count; i++) {
				for (int j = 0; j < this.instructions[i].Branches.Count; j++) {
					if (this.instructions[i].Branches[j] == this.instructions[position]) 
						this.instructions[i].Branches[j] = instruction;
				}
			}

			this.instructions[position] = instruction;
		}

		/// <summary>
		/// Adds the instruction.
		/// </summary>
		/// <param name="instruction">The instruction.</param>
		public void AddInstruction (SharpOS.AOT.IR.Instructions.Instruction instruction)
		{
			instruction.Block = this;

			this.instructions.Add (instruction);
		}

		/// <summary>
		/// Removes the instruction.
		/// </summary>
		/// <param name="instruction">The instruction.</param>
		public void RemoveInstruction (SharpOS.AOT.IR.Instructions.Instruction instruction)
		{
			instruction.Removed = true;

			this.instructions.Remove (instruction);
		}

		/// <summary>
		/// Removes the instruction.
		/// </summary>
		/// <param name="position">The position.</param>
		public void RemoveInstruction (int position)
		{
			this.instructions [position].Removed = true;

			this.instructions.RemoveAt (position);
		}


		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override string ToString ()
		{
			DumpProcessor p = new DumpProcessor((int) DumpType.Buffer);
			
			Dump (p);

			return p.ToString ();
		}

		/// <summary>
		/// Updates the index.
		/// </summary>
		/*public void UpdateIndex ()
		{
			int index = 0;

			Instructions.Instruction.InstructionVisitor visitor = delegate (Instructions.Instruction instruction) {
				instruction.Index = index++;
			};

			foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in this) 
				instruction.VisitInstruction (visitor);
		}*/

		/// <summary>
		/// Dumps a representation of this block, including lists of blocks which
		/// lead to this block and blocks which are led to after this block is
		/// executed.
		/// </summary>
		/// <param name="p">The Dump Processor.</param>
		public void Dump (DumpProcessor p)
		{
			//this.UpdateIndex();
			
			List<int> ins = new List<int>();
			List<int> outs = new List<int>();

			for (int i = 0; i < this.ins.Count; i++)
				ins.Add(this.ins[i].Index);

			for (int i = 0; i < this.outs.Count; i++) 
				outs.Add(this.outs[i].Index);
			
			p.Element(this, ins.ToArray(), outs.ToArray());
			
			for (int i = 0; i < this.InstructionsCount; i++)
				this[i].Dump (p);
			
			p.PopElement();	// block

			#if false // TODO: convert to XML dump?
			
			foreach (Instruction instruction in this.cil) {
				string operand = string.Empty;

				if (instruction.Operand != null) {
					if (instruction.Operand is Instruction) {
						operand = (instruction.Operand as Instruction).Offset.ToString();

					} else if (instruction.Operand is Instruction[]) {
						foreach (Instruction instruction2 in (instruction.Operand as Instruction[])) {
							if (operand.Length > 0) 
								operand += " ";

							operand += instruction2.Offset.ToString();
						}

						operand = "(" + operand + ")";

					} else 
						operand = instruction.Operand.ToString();
				}

				Console.WriteLine ("{0}: {1} {2}", instruction.Offset, instruction.OpCode.Name, operand);
			}

			#endif
		}

		private SharpOS.AOT.IR.Instructions.Instruction Nop (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return null;
		}

		#region CONV

		/// <summary>
		/// Converts the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		private SharpOS.AOT.IR.Instructions.Instruction Convert (Operands.Operand.ConvertType type)
		{
			Register value = this.GetRegister (stack - 1);
			value.ConvertTo = type;

			Register assignee = this.SetRegister (stack - 1);

			return new Assign (assignee, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_I (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_I);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_I1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_I1);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_I2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_I2);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_I4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_I4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_I8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_I8);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I1);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I1_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I1_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I2);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I2_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I2_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I4_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I4_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I8);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I8_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_I8_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U1);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U1_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U1_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U2);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U2_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U2_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U4_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U4_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U8);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U8_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_Ovf_U8_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_R_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_R_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_R4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_R4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_R8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_R8);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_U (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_U);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_U1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_U1);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_U2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_U2);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_U4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_U4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_U8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Operands.Operand.ConvertType.Conv_U8);
		}

		#endregion

		#region ADD

		private SharpOS.AOT.IR.Instructions.Instruction Add (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return AddHandler (Operator.BinaryType.Add);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Add_Ovf (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return AddHandler (Operator.BinaryType.AddSignedWithOverflowCheck);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Add_Ovf_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return AddHandler (Operator.BinaryType.AddUnsignedWithOverflowCheck);
		}

		private SharpOS.AOT.IR.Instructions.Instruction AddHandler (Operator.BinaryType type)
		{
			Arithmetic value = new Arithmetic (new Binary (type), this.GetRegister (stack - 2), this.GetRegister (stack - 1));

			Register assignee = this.SetRegister (stack - 2);

			return new Assign (assignee, value);
		}

		#endregion

		#region SUB

		private SharpOS.AOT.IR.Instructions.Instruction Sub (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return SubHandler (Operator.BinaryType.Sub);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Sub_Ovf (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return SubHandler (Operator.BinaryType.SubSignedWithOverflowCheck);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Sub_Ovf_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return SubHandler (Operator.BinaryType.SubUnsignedWithOverflowCheck);
		}

		private SharpOS.AOT.IR.Instructions.Instruction SubHandler (Operator.BinaryType type)
		{
			Arithmetic value = new Arithmetic (new Binary (type), this.GetRegister (stack - 2), this.GetRegister (stack - 1));

			Register assignee = this.SetRegister (stack - 2);

			return new Assign (assignee, value);
		}

		#endregion

		#region MUL

		private SharpOS.AOT.IR.Instructions.Instruction Mul (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return MulHandler (Operator.BinaryType.Mul);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Mul_Ovf (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return MulHandler (Operator.BinaryType.MulSignedWithOverflowCheck);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Mul_Ovf_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return MulHandler (Operator.BinaryType.MulUnsignedWithOverflowCheck);
		}

		private SharpOS.AOT.IR.Instructions.Instruction MulHandler (Operator.BinaryType type)
		{
			Arithmetic value = new Arithmetic (new Binary (type), this.GetRegister (stack - 2), this.GetRegister (stack - 1));

			Register assignee = this.SetRegister (stack - 2);

			return new Assign (assignee, value);
		}

		#endregion

		#region DIV

		private SharpOS.AOT.IR.Instructions.Instruction Div (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return DivHandler (Operator.BinaryType.Div);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Div_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return DivHandler (Operator.BinaryType.DivUnsigned);
		}

		private SharpOS.AOT.IR.Instructions.Instruction DivHandler (Operator.BinaryType type)
		{
			Arithmetic value = new Arithmetic (new Binary (type), this.GetRegister (stack - 2), this.GetRegister (stack - 1));

			Register assignee = this.SetRegister (stack - 2);

			return new Assign (assignee, value);
		}

		#endregion

		#region REM

		private SharpOS.AOT.IR.Instructions.Instruction Rem (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return RemHandler (Operator.BinaryType.Remainder);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Rem_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return RemHandler (Operator.BinaryType.RemainderUnsigned);
		}


		private SharpOS.AOT.IR.Instructions.Instruction RemHandler (Operator.BinaryType type)
		{
			Arithmetic value = new Arithmetic (new Binary (type), this.GetRegister (stack - 2), this.GetRegister (stack - 1));

			Register assignee = this.SetRegister (stack - 2);

			return new Assign (assignee, value);
		}

		#endregion

		private SharpOS.AOT.IR.Instructions.Instruction Neg (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Arithmetic value = new Arithmetic (new Unary (Operator.UnaryType.Negation), this.GetRegister (stack - 1));

			Register assignee = this.SetRegister (stack - 1);

			return new Assign (assignee, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction And (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Arithmetic value = new Arithmetic (new Binary (Operator.BinaryType.And), this.GetRegister (stack - 2), this.GetRegister (stack - 1));

			Register assignee = this.SetRegister (stack - 2);

			return new Assign (assignee, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Or (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Arithmetic value = new Arithmetic (new Binary (Operator.BinaryType.Or), this.GetRegister (stack - 2), this.GetRegister (stack - 1));

			Register assignee = this.SetRegister (stack - 2);

			return new Assign (assignee, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Xor (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Arithmetic value = new Arithmetic (new Binary (Operator.BinaryType.Xor), this.GetRegister (stack - 2), this.GetRegister (stack - 1));

			Register assignee = this.SetRegister (stack - 2);

			return new Assign (assignee, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Not (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Arithmetic value = new Arithmetic (new Unary (Operator.UnaryType.Not), this.GetRegister (stack - 1));

			Register assignee = this.SetRegister (stack - 1);

			return new Assign (assignee, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Shl (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Arithmetic value = new Arithmetic (new Binary (Operator.BinaryType.SHL), this.GetRegister (stack - 2), this.GetRegister (stack - 1));

			Register assignee = this.SetRegister (stack - 2);

			return new Assign (assignee, value);
		}

		#region SHR

		private SharpOS.AOT.IR.Instructions.Instruction Shr_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return ShrHandler (Operator.BinaryType.SHRUnsigned);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Shr (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return ShrHandler (Operator.BinaryType.SHR);
		}

		private SharpOS.AOT.IR.Instructions.Instruction ShrHandler (Operator.BinaryType type)
		{
			Arithmetic value = new Arithmetic (new Binary (type), this.GetRegister (stack - 2), this.GetRegister (stack - 1));

			Register assignee = this.SetRegister (stack - 2);

			return new Assign (assignee, value);
		}

		#endregion

		#region Branch

		private SharpOS.AOT.IR.Instructions.Instruction Beq (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.Equal);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Beq_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.Equal);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bge (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.GreaterThanOrEqual);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bge_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.GreaterThanOrEqual);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bge_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.GreaterThanOrEqualUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bge_Un_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.GreaterThanOrEqualUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bgt (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.GreaterThan);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bgt_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.GreaterThan);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bgt_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.GreaterThanUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bgt_Un_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.GreaterThanUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ble (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.LessThanOrEqual);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ble_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.LessThanOrEqual);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ble_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.LessThanOrEqualUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ble_Un_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.LessThanOrEqualUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Blt (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.LessThan);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Blt_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.LessThan);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Blt_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.LessThanUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Blt_Un_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.LessThanUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bne_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.NotEqualOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bne_Un_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (Operator.RelationalType.NotEqualOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction BranchHandler (Operator.RelationalType type)
		{
			return new ConditionalJump (new SharpOS.AOT.IR.Operands.Boolean (new Relational (type), this.GetRegister (stack - 2), this.GetRegister (stack - 1)));
		}

		#endregion

		#region BR True/False

		private SharpOS.AOT.IR.Instructions.Instruction Brfalse (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BrTrueFalseHandler (Operator.BooleanType.False);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Brfalse_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BrTrueFalseHandler (Operator.BooleanType.False);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Brtrue (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BrTrueFalseHandler (Operator.BooleanType.True);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Brtrue_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BrTrueFalseHandler (Operator.BooleanType.True);
		}

		private SharpOS.AOT.IR.Instructions.Instruction BrTrueFalseHandler (Operator.BooleanType type)
		{
			return new ConditionalJump (new SharpOS.AOT.IR.Operands.Boolean (new SharpOS.AOT.IR.Operators.Boolean (type), this.GetRegister (stack - 1)));
		}

		#endregion

		private SharpOS.AOT.IR.Instructions.Instruction Br_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return new Jump ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Br (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return new Jump ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Leave (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return new Jump ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Leave_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return new Jump ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Endfinally (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return new Jump ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Throw (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return new SharpOS.AOT.IR.Instructions.System (new SharpOS.AOT.IR.Operands.Miscellaneous (new SharpOS.AOT.IR.Operators.Miscellaneous (Operator.MiscellaneousType.Throw), this.GetRegister (stack - 1)));
		}

		private SharpOS.AOT.IR.Instructions.Instruction Rethrow (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return new SharpOS.AOT.IR.Instructions.System (new SharpOS.AOT.IR.Operands.Miscellaneous (new SharpOS.AOT.IR.Operators.Miscellaneous (Operator.MiscellaneousType.Throw)));
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ret (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			if (this.method.MethodDefinition.ReturnType.ReturnType.FullName.Equals ("System.Void"))
				return new Return ();

			else if (stack > 0)
				return new Return (this.GetRegister (stack - 1));

			return new Return (this.GetRegister (0));
		}

		private SharpOS.AOT.IR.Instructions.Instruction Switch (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return new Switch (this.GetRegister (stack - 1));
		}

		private SharpOS.AOT.IR.Instructions.Instruction Pop (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return new Pop (this.GetRegister (stack - 1));
		}

		private SharpOS.AOT.IR.Instructions.Instruction Dup (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register value = this.GetRegister (stack - 1);

			Register assignee = this.SetRegister (stack);

			return new Assign (assignee, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Sizeof (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Constant constant = new Constant (this.method.Engine.GetTypeSize (cilInstruction.Operand.ToString ()));
			constant.SizeType = Operand.InternalSizeType.I4;

			Register assignee = this.SetRegister (stack);

			return new Assign (assignee, constant);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Localloc (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Operands.Miscellaneous value = new SharpOS.AOT.IR.Operands.Miscellaneous (new Operators.Miscellaneous (Operator.MiscellaneousType.Localloc), this.GetRegister (stack - 1));

			Register assignee = this.SetRegister (stack - 1);
			assignee.SizeType = Operand.InternalSizeType.U;

			return new Assign (assignee, value);
		}

		#region Conditional Check

		private SharpOS.AOT.IR.Instructions.Instruction Ceq (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return ConditionCheckHandler (Operator.RelationalType.Equal);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Cgt (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return ConditionCheckHandler (Operator.RelationalType.GreaterThan);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Cgt_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return ConditionCheckHandler (Operator.RelationalType.GreaterThanUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Clt (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return ConditionCheckHandler (Operator.RelationalType.LessThan);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Clt_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return ConditionCheckHandler (Operator.RelationalType.LessThanUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction ConditionCheckHandler (Operator.RelationalType type)
		{
			Operands.Boolean value = new SharpOS.AOT.IR.Operands.Boolean (new SharpOS.AOT.IR.Operators.Relational (type), this.GetRegister (stack - 2), this.GetRegister (stack - 1));

			Register assignee = this.SetRegister (stack - 2);

			return new Assign (assignee, value);
		}

		#endregion

		private SharpOS.AOT.IR.Instructions.Instruction Ldlen (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Arithmetic value = new Arithmetic (new Unary (Operator.UnaryType.ArraySize), this.GetRegister (stack - 1));

			Register assignee = this.SetRegister (stack - 2);

			return new Assign (assignee, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldtoken (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Constant value = new Constant (cilInstruction.Operand);
			
			Register assignee = this.SetRegister (stack);

			return new Assign (assignee, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldnull (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Constant value = new Constant (null);

			Register assignee = this.SetRegister (stack);
			assignee.SizeType = Operand.InternalSizeType.U;

			return new Assign (assignee, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldstr (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Constant value = new Constant (cilInstruction.Operand.ToString ());

			Register assignee = this.SetRegister (stack);

			return new Assign (assignee, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldftn (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Operands.MethodReference value = new Operands.MethodReference (cilInstruction.Operand as Mono.Cecil.MethodReference);

			Register assignee = this.SetRegister (stack);

			return new Assign (assignee, value);
		}

		#region Ldc
		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_0 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcHandler (Operand.InternalSizeType.I4, 0);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcHandler (Operand.InternalSizeType.I4, 1);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcHandler (Operand.InternalSizeType.I4, 2);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_3 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcHandler (Operand.InternalSizeType.I4, 3);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcHandler (Operand.InternalSizeType.I4, 4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_5 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcHandler (Operand.InternalSizeType.I4, 5);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_6 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcHandler (Operand.InternalSizeType.I4, 6);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_7 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcHandler (Operand.InternalSizeType.I4, 7);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcHandler (Operand.InternalSizeType.I4, 8);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_M1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcHandler (Operand.InternalSizeType.I4, -1);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcHandler (Operand.InternalSizeType.I4, cilInstruction.Operand);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcHandler (Operand.InternalSizeType.I4, cilInstruction.Operand);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcHandler (Operand.InternalSizeType.I8, cilInstruction.Operand);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_R4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcHandler (Operand.InternalSizeType.R4, cilInstruction.Operand);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_R8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcHandler (Operand.InternalSizeType.R8, cilInstruction.Operand);
		}

		private SharpOS.AOT.IR.Instructions.Instruction LdcHandler (Operand.InternalSizeType sizeType, object value)
		{
			Constant constant = new Constant (value);
			constant.SizeType = sizeType;

			Register assignee = this.SetRegister (stack);

			return new Assign (assignee, constant);
		}
		#endregion

		#region Ldind
		private SharpOS.AOT.IR.Instructions.Instruction Ldind_I (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (Operand.InternalSizeType.I, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_I1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (Operand.InternalSizeType.I1, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_I2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (Operand.InternalSizeType.I2, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_I4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (Operand.InternalSizeType.I4, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_I8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (Operand.InternalSizeType.I8, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_R4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (Operand.InternalSizeType.R4, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_R8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (Operand.InternalSizeType.R8, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_U1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (Operand.InternalSizeType.U1, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_U2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (Operand.InternalSizeType.U2, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_U4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (Operand.InternalSizeType.U4, cilInstruction);
		}
		
		private SharpOS.AOT.IR.Instructions.Instruction Ldind_Ref (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction LdindHandler (Operand.InternalSizeType sizeType, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Indirect indirect = new Indirect (this.GetRegister (stack - 1));
			indirect.SizeType = sizeType;

			Register register = this.SetRegister (stack - 1);
			register.SizeType = sizeType;

			return new Assign (register, indirect);
		}
		#endregion

		#region Obj
		private SharpOS.AOT.IR.Instructions.Instruction Newobj (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Mono.Cecil.MethodReference call = (cilInstruction.Operand as Mono.Cecil.MethodReference);

			Operand [] operands = new Operand [call.Parameters.Count];

			for (int i = 0; i < call.Parameters.Count; i++)
				operands [i] = this.GetRegister (stack - call.Parameters.Count + i);

			Register assignee = this.SetRegister (stack - call.Parameters.Count);
			assignee.SizeType = Operand.InternalSizeType.I;
			assignee.TypeName = call.DeclaringType.ToString ();

			Operands.Call callParameter = new SharpOS.AOT.IR.Operands.Call (call, operands);

			//stack -= call.Parameters.Count;

			return new Assign (assignee, callParameter);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldobj (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Operands.Object _object = new Operands.Object ((cilInstruction.Operand as TypeReference).FullName, this.GetRegister (stack - 1));
			_object.SizeType = Operand.InternalSizeType.ValueType;

			Register register = this.SetRegister (stack - 1);
			register.SizeType = Operand.InternalSizeType.ValueType;
			register.TypeName = (cilInstruction.Operand as TypeReference).FullName;

			return new Assign (register, _object);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stobj (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register register = this.GetRegister (stack - 1);
			register.SizeType = Operand.InternalSizeType.ValueType;

			Operands.Object _object = new Operands.Object ((cilInstruction.Operand as TypeReference).FullName, this.SetRegister (stack - 2));
			_object.SizeType = Operand.InternalSizeType.ValueType;

			return new Assign (_object, register);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Initobj (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register register = this.GetRegister (stack - 1);
			register.SizeType = Operand.InternalSizeType.ValueType;
			register.TypeName = cilInstruction.Operand.ToString ();

			return new Instructions.System (new Operands.Miscellaneous (new Operators.Miscellaneous (Operators.Miscellaneous.MiscellaneousType.InitObj), register));
		}
		#endregion

		#region Ldloca
		private SharpOS.AOT.IR.Instructions.Instruction Ldloca (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdlocaHandler (cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldloca_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdlocaHandler(cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction LdlocaHandler (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Address address = new Address (this.GetLocal ((cilInstruction.Operand as VariableDefinition).Index));

			Register assignee = this.SetRegister (stack);
			assignee.TypeName = (cilInstruction.Operand as VariableDefinition).VariableType.ToString ();

			return new Assign (assignee, address);
		}
		#endregion

		#region Ldloc
		private SharpOS.AOT.IR.Instructions.Instruction Ldloc (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdlocHandler (cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldloc_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdlocHandler (cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction LdlocHandler (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Local value = this.GetLocal ((cilInstruction.Operand as VariableDefinition).Index);

			Register assignee = this.SetRegister (stack);

			return new Assign (assignee, value);
		}
		#endregion

		#region Ldloc X
		private SharpOS.AOT.IR.Instructions.Instruction Ldloc_0 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdlocHandler (0, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldloc_1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdlocHandler (1, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldloc_2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdlocHandler (2, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldloc_3 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdlocHandler (3, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction LdlocHandler (int index, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Local value = this.GetLocal (index);

			Register assignee = this.SetRegister (stack);

			return new Assign (assignee, value);
		}
		#endregion

		#region Stind
		private SharpOS.AOT.IR.Instructions.Instruction Stind_I (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StindHandler (Operand.InternalSizeType.I, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stind_I1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StindHandler (Operand.InternalSizeType.I1, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stind_I2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StindHandler (Operand.InternalSizeType.I2, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stind_I4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StindHandler (Operand.InternalSizeType.I4, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stind_I8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StindHandler (Operand.InternalSizeType.I8, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stind_R4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StindHandler (Operand.InternalSizeType.R4, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stind_R8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StindHandler (Operand.InternalSizeType.R8, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stind_Ref (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction StindHandler (Operand.InternalSizeType sizeType, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register register = this.GetRegister (stack - 1);
			register.SizeType = sizeType;

			Indirect indirect = new Indirect (this.GetRegister (stack - 2));
			indirect.SizeType = sizeType;

			return new Assign (indirect, register);
		}
		#endregion

		#region Stloc
		private SharpOS.AOT.IR.Instructions.Instruction Stloc (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StlocHandler (cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stloc_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StlocHandler (cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction StlocHandler (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register value = this.GetRegister (stack - 1);

			Local assignee = this.SetLocal ((cilInstruction.Operand as VariableDefinition).Index);

			return new Assign (assignee, value);
		}
		#endregion

		#region Stloc X
		private SharpOS.AOT.IR.Instructions.Instruction Stloc_0 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StlocHandler (0, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stloc_1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StlocHandler (1, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stloc_2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StlocHandler (2, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stloc_3 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StlocHandler (3, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction StlocHandler (int index, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register value = this.GetRegister (stack - 1);

			Local assignee = this.SetLocal (index);

			return new Assign (assignee, value);
		}
		#endregion

		#region Ldarg
		private SharpOS.AOT.IR.Instructions.Instruction Ldarg (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdargHandler (cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldarg_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdargHandler (cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction LdargHandler (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Argument value = this.GetArgument ((cilInstruction.Operand as ParameterDefinition).Sequence);

			Register assignee = this.SetRegister (stack);

			return new Assign (assignee, value);
		}
		#endregion

		#region Starga
		private SharpOS.AOT.IR.Instructions.Instruction Ldarga (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdargaHandler (cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldarga_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdargaHandler(cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction LdargaHandler (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			if (cilInstruction.Operand is ParameterDefinition) {
				Address address = new Address (this.GetArgument ((cilInstruction.Operand as ParameterDefinition).Sequence));

				Register assignee = this.SetRegister (stack);

				return new Assign (assignee, address);

			} else {
				Address address = new Address (this.GetArgument ((int) cilInstruction.Operand));

				Register assignee = this.SetRegister (stack);

				return new Assign (assignee, address);
			}
		}
		#endregion

		#region Ldarg X
		private SharpOS.AOT.IR.Instructions.Instruction Ldarg_0 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdargHandler (1, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldarg_1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdargHandler (2, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldarg_2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdargHandler (3, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldarg_3 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdargHandler (4, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction LdargHandler (int i, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Argument value = this.GetArgument (i);

			Register assignee = this.SetRegister (stack);

			return new Assign (assignee, value);
		}
		#endregion

		#region Starg
		private SharpOS.AOT.IR.Instructions.Instruction Starg (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StargHandler (cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Starg_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StargHandler (cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction StargHandler (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register value = this.GetRegister (stack - 1);

			Argument assignee = this.SetArgument ((cilInstruction.Operand as ParameterDefinition).Sequence);

			return new Assign (assignee, value);
		}
		#endregion

		#region Call
		private SharpOS.AOT.IR.Instructions.Instruction Call (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return CallHandling (cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Callvirt (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return CallHandling (cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Jmp (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return CallHandling (cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction CallHandling (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Mono.Cecil.MethodReference call = (cilInstruction.Operand as Mono.Cecil.MethodReference);
			MethodDefinition def = this.Method.Engine.GetCILDefinition (call);

			if (def != null) {
				foreach (CustomAttribute attr in def.CustomAttributes) {
					if (attr.Constructor.DeclaringType.FullName ==
							typeof (SharpOS.AOT.Attributes.ADCStubAttribute).FullName) {

						call = this.Method.Engine.FixupADCMethod (call);
					}
				}
			}

			Operand [] operands;

			if (call.HasThis)
				operands = new Operand [call.Parameters.Count + 1];

			else
				operands = new Operand [call.Parameters.Count];

			for (int i = 0; i < operands.Length; i++)
				operands [i] = this.GetRegister (stack - operands.Length + i);

			if (call.ReturnType.ReturnType.FullName == Constants.Void) {
				//stack--;
				//stack -= operands.Length;

				return new SharpOS.AOT.IR.Instructions.Call (new SharpOS.AOT.IR.Operands.Call (call, operands));
			}

			Register assignee = this.SetRegister (stack - operands.Length);
			assignee.TypeName = call.ReturnType.ReturnType.FullName;

			//stack -= operands.Length;

			return new Assign (assignee, new SharpOS.AOT.IR.Operands.Call (call, operands));
		}
		#endregion

		#region Fld
		private SharpOS.AOT.IR.Instructions.Instruction Ldfld (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			MemberReference field = cilInstruction.Operand as MemberReference;
			string fieldName = field.DeclaringType.FullName + "::" + field.Name;

			Field fieldOperand;
			Register assignee;

			if ((field as FieldDefinition).IsStatic) {
				fieldOperand = new Field (fieldName);
				fieldOperand.SizeType = this.method.Engine.GetInternalType (fieldName);

				assignee = this.SetRegister (stack - 1);

				return new Assign (assignee, fieldOperand);
			}

			fieldOperand = new Field (fieldName, this.GetRegister (stack - 1));
			fieldOperand.SizeType = this.method.Engine.GetInternalType (fieldName);

			assignee = this.SetRegister (stack - 1);

			return new Assign (assignee, fieldOperand);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldflda (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			FieldReference field = cilInstruction.Operand as FieldReference;
			string fieldName = field.DeclaringType.FullName + "::" + field.Name;

			Operands.Address address = new Operands.Address (new Field ((cilInstruction.Operand as FieldDefinition).DeclaringType.FullName + "::" + (cilInstruction.Operand as FieldDefinition).Name, this.GetRegister (stack - 1)));
			address.Value.SizeType = this.method.Engine.GetInternalType (field.FieldType.FullName);

			Register assignee = this.SetRegister (stack - 1);

			return new Assign (assignee, address);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldsfld (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			FieldReference field = cilInstruction.Operand as FieldReference;
			string fieldName = field.DeclaringType.FullName + "::" + field.Name;

			Field fieldOperand = new Field (fieldName);
			fieldOperand.SizeType = this.method.Engine.GetInternalType (field.FieldType.FullName);

			Register assignee = this.SetRegister (stack);

			return new Assign (assignee, fieldOperand);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldsflda (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			FieldReference field = cilInstruction.Operand as FieldReference;
			string fieldName = field.DeclaringType.FullName + "::" + field.Name;

			Operands.Address address = new Operands.Address (new Field ((cilInstruction.Operand as FieldReference).DeclaringType.FullName + "::" + (cilInstruction.Operand as FieldReference).Name));
			address.Value.SizeType = this.method.Engine.GetInternalType (field.FieldType.FullName);

			Register assignee = this.SetRegister (stack);

			return new Assign (assignee, address);
		}


		private SharpOS.AOT.IR.Instructions.Instruction Stfld (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			MemberReference field = cilInstruction.Operand as MemberReference;
			string fieldName = field.DeclaringType.FullName + "::" + field.Name;

			Field fieldOperand;
			Register value;

			if ((field as FieldDefinition).IsStatic) {
				value = this.GetRegister (stack - 1);

				fieldOperand = new Field (fieldName);
				fieldOperand.SizeType = this.method.Engine.GetInternalType (fieldName);

				return new Assign (fieldOperand, value);
			}

			value = this.GetRegister (stack - 1);

			fieldOperand = new Field (fieldName, this.GetRegister (stack - 2));
			fieldOperand.SizeType = this.method.Engine.GetInternalType (fieldName);

			return new Assign (fieldOperand, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stsfld (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			FieldReference field = cilInstruction.Operand as FieldReference;
			string fieldName = field.DeclaringType.FullName + "::" + field.Name;

			Register value = this.GetRegister (stack - 1);

			Field fieldOperand = new Field (fieldName);
			fieldOperand.SizeType = this.method.Engine.GetInternalType (field.FieldType.FullName);

			return new Assign (fieldOperand, value);
		}
		#endregion

		#region Stelem
		private SharpOS.AOT.IR.Instructions.Instruction Stelem_Ref (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stelem_Any (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stelem_I (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StelemHandler (cilInstruction, Operand.InternalSizeType.I);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stelem_I1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StelemHandler (cilInstruction, Operand.InternalSizeType.I1);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stelem_I2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StelemHandler (cilInstruction, Operand.InternalSizeType.I2);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stelem_I4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StelemHandler (cilInstruction, Operand.InternalSizeType.I4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stelem_I8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StelemHandler (cilInstruction, Operand.InternalSizeType.I8);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stelem_R4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StelemHandler (cilInstruction, Operand.InternalSizeType.R4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stelem_R8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StelemHandler (cilInstruction, Operand.InternalSizeType.R8);
		}

		private SharpOS.AOT.IR.Instructions.Instruction StelemHandler (Mono.Cecil.Cil.Instruction cilInstruction, Operand.InternalSizeType sizeType)
		{
			Register value = this.GetRegister (stack - 1);
			value.SizeType = sizeType;

			ArrayElement arrayElement = new ArrayElement (this.GetRegister (stack - 3), this.GetRegister (stack - 2));
			arrayElement.SizeType = sizeType;

			return new Assign (arrayElement, value);
		}
		#endregion

		#region Ldelem
		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_Ref (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_I (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, Operand.InternalSizeType.I);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_I1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, Operand.InternalSizeType.I1);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_I2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, Operand.InternalSizeType.I2);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_I4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, Operand.InternalSizeType.I4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_I8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, Operand.InternalSizeType.I8);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_R4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, Operand.InternalSizeType.R4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_R8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, Operand.InternalSizeType.R8);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_U1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, Operand.InternalSizeType.U1);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_U2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, Operand.InternalSizeType.U2);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_U4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, Operand.InternalSizeType.U4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_Any (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelema (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction LdelemHandler (Mono.Cecil.Cil.Instruction cilInstruction, Operand.InternalSizeType sizeType)
		{
			Register assignee = this.SetRegister (stack - 1);
			assignee.SizeType = sizeType;

			ArrayElement arrayElement = new ArrayElement (this.GetRegister (stack - 2), this.GetRegister (stack - 1));
			arrayElement.SizeType = sizeType;

			return new Assign (assignee, arrayElement);
		}
		#endregion

		private SharpOS.AOT.IR.Instructions.Instruction Newarr (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Break (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}
		
		private SharpOS.AOT.IR.Instructions.Instruction Calli (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Cpobj (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Castclass (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Isinst (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Unbox (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Box (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Refanyval (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ckfinite (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Mkrefany (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Arglist (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldvirtftn (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Endfilter (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Unaligned (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Volatile (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Tail (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Constrained (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Cpblk (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Initblk (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction No (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Refanytype (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Unbox_Any (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		private SharpOS.AOT.IR.Instructions.Instruction Readonly (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new Exception ("Not implemented yet.");
		}

		#region Dispatcher
		private delegate Instructions.Instruction ILDispatcherDelegate (Block block, Mono.Cecil.Cil.Instruction cilInstruction);

		private static ILDispatcherDelegate [] ilDispatcher = new ILDispatcherDelegate [] {
			new ILDispatcherDelegate(Block.Nop),
			new ILDispatcherDelegate(Block.Break),
			new ILDispatcherDelegate(Block.Ldarg_0),
			new ILDispatcherDelegate(Block.Ldarg_1),
			new ILDispatcherDelegate(Block.Ldarg_2),
			new ILDispatcherDelegate(Block.Ldarg_3),
			new ILDispatcherDelegate(Block.Ldloc_0),
			new ILDispatcherDelegate(Block.Ldloc_1),
			new ILDispatcherDelegate(Block.Ldloc_2),
			new ILDispatcherDelegate(Block.Ldloc_3),
			new ILDispatcherDelegate(Block.Stloc_0),
			new ILDispatcherDelegate(Block.Stloc_1),
			new ILDispatcherDelegate(Block.Stloc_2),
			new ILDispatcherDelegate(Block.Stloc_3),
			new ILDispatcherDelegate(Block.Ldarg_S),
			new ILDispatcherDelegate(Block.Ldarga_S),
			new ILDispatcherDelegate(Block.Starg_S),
			new ILDispatcherDelegate(Block.Ldloc_S),
			new ILDispatcherDelegate(Block.Ldloca_S),
			new ILDispatcherDelegate(Block.Stloc_S),
			new ILDispatcherDelegate(Block.Ldnull),
			new ILDispatcherDelegate(Block.Ldc_I4_M1),
			new ILDispatcherDelegate(Block.Ldc_I4_0),
			new ILDispatcherDelegate(Block.Ldc_I4_1),
			new ILDispatcherDelegate(Block.Ldc_I4_2),
			new ILDispatcherDelegate(Block.Ldc_I4_3),
			new ILDispatcherDelegate(Block.Ldc_I4_4),
			new ILDispatcherDelegate(Block.Ldc_I4_5),
			new ILDispatcherDelegate(Block.Ldc_I4_6),
			new ILDispatcherDelegate(Block.Ldc_I4_7),
			new ILDispatcherDelegate(Block.Ldc_I4_8),
			new ILDispatcherDelegate(Block.Ldc_I4_S),
			new ILDispatcherDelegate(Block.Ldc_I4),
			new ILDispatcherDelegate(Block.Ldc_I8),
			new ILDispatcherDelegate(Block.Ldc_R4),
			new ILDispatcherDelegate(Block.Ldc_R8),
			new ILDispatcherDelegate(Block.Dup),
			new ILDispatcherDelegate(Block.Pop),
			new ILDispatcherDelegate(Block.Jmp),
			new ILDispatcherDelegate(Block.Call),
			new ILDispatcherDelegate(Block.Calli),
			new ILDispatcherDelegate(Block.Ret),
			new ILDispatcherDelegate(Block.Br_S),
			new ILDispatcherDelegate(Block.Brfalse_S),
			new ILDispatcherDelegate(Block.Brtrue_S),
			new ILDispatcherDelegate(Block.Beq_S),
			new ILDispatcherDelegate(Block.Bge_S),
			new ILDispatcherDelegate(Block.Bgt_S),
			new ILDispatcherDelegate(Block.Ble_S),
			new ILDispatcherDelegate(Block.Blt_S),
			new ILDispatcherDelegate(Block.Bne_Un_S),
			new ILDispatcherDelegate(Block.Bge_Un_S),
			new ILDispatcherDelegate(Block.Bgt_Un_S),
			new ILDispatcherDelegate(Block.Ble_Un_S),
			new ILDispatcherDelegate(Block.Blt_Un_S),
			new ILDispatcherDelegate(Block.Br),
			new ILDispatcherDelegate(Block.Brfalse),
			new ILDispatcherDelegate(Block.Brtrue),
			new ILDispatcherDelegate(Block.Beq),
			new ILDispatcherDelegate(Block.Bge),
			new ILDispatcherDelegate(Block.Bgt),
			new ILDispatcherDelegate(Block.Ble),
			new ILDispatcherDelegate(Block.Blt),
			new ILDispatcherDelegate(Block.Bne_Un),
			new ILDispatcherDelegate(Block.Bge_Un),
			new ILDispatcherDelegate(Block.Bgt_Un),
			new ILDispatcherDelegate(Block.Ble_Un),
			new ILDispatcherDelegate(Block.Blt_Un),
			new ILDispatcherDelegate(Block.Switch),
			new ILDispatcherDelegate(Block.Ldind_I1),
			new ILDispatcherDelegate(Block.Ldind_U1),
			new ILDispatcherDelegate(Block.Ldind_I2),
			new ILDispatcherDelegate(Block.Ldind_U2),
			new ILDispatcherDelegate(Block.Ldind_I4),
			new ILDispatcherDelegate(Block.Ldind_U4),
			new ILDispatcherDelegate(Block.Ldind_I8),
			new ILDispatcherDelegate(Block.Ldind_I),
			new ILDispatcherDelegate(Block.Ldind_R4),
			new ILDispatcherDelegate(Block.Ldind_R8),
			new ILDispatcherDelegate(Block.Ldind_Ref),
			new ILDispatcherDelegate(Block.Stind_Ref),
			new ILDispatcherDelegate(Block.Stind_I1),
			new ILDispatcherDelegate(Block.Stind_I2),
			new ILDispatcherDelegate(Block.Stind_I4),
			new ILDispatcherDelegate(Block.Stind_I8),
			new ILDispatcherDelegate(Block.Stind_R4),
			new ILDispatcherDelegate(Block.Stind_R8),
			new ILDispatcherDelegate(Block.Add),
			new ILDispatcherDelegate(Block.Sub),
			new ILDispatcherDelegate(Block.Mul),
			new ILDispatcherDelegate(Block.Div),
			new ILDispatcherDelegate(Block.Div_Un),
			new ILDispatcherDelegate(Block.Rem),
			new ILDispatcherDelegate(Block.Rem_Un),
			new ILDispatcherDelegate(Block.And),
			new ILDispatcherDelegate(Block.Or),
			new ILDispatcherDelegate(Block.Xor),
			new ILDispatcherDelegate(Block.Shl),
			new ILDispatcherDelegate(Block.Shr),
			new ILDispatcherDelegate(Block.Shr_Un),
			new ILDispatcherDelegate(Block.Neg),
			new ILDispatcherDelegate(Block.Not),
			new ILDispatcherDelegate(Block.Conv_I1),
			new ILDispatcherDelegate(Block.Conv_I2),
			new ILDispatcherDelegate(Block.Conv_I4),
			new ILDispatcherDelegate(Block.Conv_I8),
			new ILDispatcherDelegate(Block.Conv_R4),
			new ILDispatcherDelegate(Block.Conv_R8),
			new ILDispatcherDelegate(Block.Conv_U4),
			new ILDispatcherDelegate(Block.Conv_U8),
			new ILDispatcherDelegate(Block.Callvirt),
			new ILDispatcherDelegate(Block.Cpobj),
			new ILDispatcherDelegate(Block.Ldobj),
			new ILDispatcherDelegate(Block.Ldstr),
			new ILDispatcherDelegate(Block.Newobj),
			new ILDispatcherDelegate(Block.Castclass),
			new ILDispatcherDelegate(Block.Isinst),
			new ILDispatcherDelegate(Block.Conv_R_Un),
			new ILDispatcherDelegate(Block.Unbox),
			new ILDispatcherDelegate(Block.Throw),
			new ILDispatcherDelegate(Block.Ldfld),
			new ILDispatcherDelegate(Block.Ldflda),
			new ILDispatcherDelegate(Block.Stfld),
			new ILDispatcherDelegate(Block.Ldsfld),
			new ILDispatcherDelegate(Block.Ldsflda),
			new ILDispatcherDelegate(Block.Stsfld),
			new ILDispatcherDelegate(Block.Stobj),
			new ILDispatcherDelegate(Block.Conv_Ovf_I1_Un),
			new ILDispatcherDelegate(Block.Conv_Ovf_I2_Un),
			new ILDispatcherDelegate(Block.Conv_Ovf_I4_Un),
			new ILDispatcherDelegate(Block.Conv_Ovf_I8_Un),
			new ILDispatcherDelegate(Block.Conv_Ovf_U1_Un),
			new ILDispatcherDelegate(Block.Conv_Ovf_U2_Un),
			new ILDispatcherDelegate(Block.Conv_Ovf_U4_Un),
			new ILDispatcherDelegate(Block.Conv_Ovf_U8_Un),
			new ILDispatcherDelegate(Block.Conv_Ovf_I_Un),
			new ILDispatcherDelegate(Block.Conv_Ovf_U_Un),
			new ILDispatcherDelegate(Block.Box),
			new ILDispatcherDelegate(Block.Newarr),
			new ILDispatcherDelegate(Block.Ldlen),
			new ILDispatcherDelegate(Block.Ldelema),
			new ILDispatcherDelegate(Block.Ldelem_I1),
			new ILDispatcherDelegate(Block.Ldelem_U1),
			new ILDispatcherDelegate(Block.Ldelem_I2),
			new ILDispatcherDelegate(Block.Ldelem_U2),
			new ILDispatcherDelegate(Block.Ldelem_I4),
			new ILDispatcherDelegate(Block.Ldelem_U4),
			new ILDispatcherDelegate(Block.Ldelem_I8),
			new ILDispatcherDelegate(Block.Ldelem_I),
			new ILDispatcherDelegate(Block.Ldelem_R4),
			new ILDispatcherDelegate(Block.Ldelem_R8),
			new ILDispatcherDelegate(Block.Ldelem_Ref),
			new ILDispatcherDelegate(Block.Stelem_I),
			new ILDispatcherDelegate(Block.Stelem_I1),
			new ILDispatcherDelegate(Block.Stelem_I2),
			new ILDispatcherDelegate(Block.Stelem_I4),
			new ILDispatcherDelegate(Block.Stelem_I8),
			new ILDispatcherDelegate(Block.Stelem_R4),
			new ILDispatcherDelegate(Block.Stelem_R8),
			new ILDispatcherDelegate(Block.Stelem_Ref),
			new ILDispatcherDelegate(Block.Ldelem_Any),
			new ILDispatcherDelegate(Block.Stelem_Any),
			new ILDispatcherDelegate(Block.Unbox_Any),
			new ILDispatcherDelegate(Block.Conv_Ovf_I1),
			new ILDispatcherDelegate(Block.Conv_Ovf_U1),
			new ILDispatcherDelegate(Block.Conv_Ovf_I2),
			new ILDispatcherDelegate(Block.Conv_Ovf_U2),
			new ILDispatcherDelegate(Block.Conv_Ovf_I4),
			new ILDispatcherDelegate(Block.Conv_Ovf_U4),
			new ILDispatcherDelegate(Block.Conv_Ovf_I8),
			new ILDispatcherDelegate(Block.Conv_Ovf_U8),
			new ILDispatcherDelegate(Block.Refanyval),
			new ILDispatcherDelegate(Block.Ckfinite),
			new ILDispatcherDelegate(Block.Mkrefany),
			new ILDispatcherDelegate(Block.Ldtoken),
			new ILDispatcherDelegate(Block.Conv_U2),
			new ILDispatcherDelegate(Block.Conv_U1),
			new ILDispatcherDelegate(Block.Conv_I),
			new ILDispatcherDelegate(Block.Conv_Ovf_I),
			new ILDispatcherDelegate(Block.Conv_Ovf_U),
			new ILDispatcherDelegate(Block.Add_Ovf),
			new ILDispatcherDelegate(Block.Add_Ovf_Un),
			new ILDispatcherDelegate(Block.Mul_Ovf),
			new ILDispatcherDelegate(Block.Mul_Ovf_Un),
			new ILDispatcherDelegate(Block.Sub_Ovf),
			new ILDispatcherDelegate(Block.Sub_Ovf_Un),
			new ILDispatcherDelegate(Block.Endfinally),
			new ILDispatcherDelegate(Block.Leave),
			new ILDispatcherDelegate(Block.Leave_S),
			new ILDispatcherDelegate(Block.Stind_I),
			new ILDispatcherDelegate(Block.Conv_U),
			new ILDispatcherDelegate(Block.Arglist),
			new ILDispatcherDelegate(Block.Ceq),
			new ILDispatcherDelegate(Block.Cgt),
			new ILDispatcherDelegate(Block.Cgt_Un),
			new ILDispatcherDelegate(Block.Clt),
			new ILDispatcherDelegate(Block.Clt_Un),
			new ILDispatcherDelegate(Block.Ldftn),
			new ILDispatcherDelegate(Block.Ldvirtftn),
			new ILDispatcherDelegate(Block.Ldarg),
			new ILDispatcherDelegate(Block.Ldarga),
			new ILDispatcherDelegate(Block.Starg),
			new ILDispatcherDelegate(Block.Ldloc),
			new ILDispatcherDelegate(Block.Ldloca),
			new ILDispatcherDelegate(Block.Stloc),
			new ILDispatcherDelegate(Block.Localloc),
			new ILDispatcherDelegate(Block.Endfilter),
			new ILDispatcherDelegate(Block.Unaligned),
			new ILDispatcherDelegate(Block.Volatile),
			new ILDispatcherDelegate(Block.Tail),
			new ILDispatcherDelegate(Block.Initobj),
			new ILDispatcherDelegate(Block.Constrained),
			new ILDispatcherDelegate(Block.Cpblk),
			new ILDispatcherDelegate(Block.Initblk),
			new ILDispatcherDelegate(Block.No),
			new ILDispatcherDelegate(Block.Rethrow),
			new ILDispatcherDelegate(Block.Sizeof),
			new ILDispatcherDelegate(Block.Refanytype),
			new ILDispatcherDelegate(Block.Readonly),
		};

		private static Instructions.Instruction Nop(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Nop(cilInstruction); }
		private static Instructions.Instruction Break(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Break(cilInstruction); }
		private static Instructions.Instruction Ldarg_0(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldarg_0(cilInstruction); }
		private static Instructions.Instruction Ldarg_1(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldarg_1(cilInstruction); }
		private static Instructions.Instruction Ldarg_2(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldarg_2(cilInstruction); }
		private static Instructions.Instruction Ldarg_3(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldarg_3(cilInstruction); }
		private static Instructions.Instruction Ldloc_0(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldloc_0(cilInstruction); }
		private static Instructions.Instruction Ldloc_1(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldloc_1(cilInstruction); }
		private static Instructions.Instruction Ldloc_2(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldloc_2(cilInstruction); }
		private static Instructions.Instruction Ldloc_3(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldloc_3(cilInstruction); }
		private static Instructions.Instruction Stloc_0(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stloc_0(cilInstruction); }
		private static Instructions.Instruction Stloc_1(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stloc_1(cilInstruction); }
		private static Instructions.Instruction Stloc_2(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stloc_2(cilInstruction); }
		private static Instructions.Instruction Stloc_3(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stloc_3(cilInstruction); }
		private static Instructions.Instruction Ldarg_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldarg_S(cilInstruction); }
		private static Instructions.Instruction Ldarga_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldarga_S(cilInstruction); }
		private static Instructions.Instruction Starg_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Starg_S(cilInstruction); }
		private static Instructions.Instruction Ldloc_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldloc_S(cilInstruction); }
		private static Instructions.Instruction Ldloca_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldloca_S(cilInstruction); }
		private static Instructions.Instruction Stloc_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stloc_S(cilInstruction); }
		private static Instructions.Instruction Ldnull(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldnull(cilInstruction); }
		private static Instructions.Instruction Ldc_I4_M1(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldc_I4_M1(cilInstruction); }
		private static Instructions.Instruction Ldc_I4_0(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldc_I4_0(cilInstruction); }
		private static Instructions.Instruction Ldc_I4_1(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldc_I4_1(cilInstruction); }
		private static Instructions.Instruction Ldc_I4_2(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldc_I4_2(cilInstruction); }
		private static Instructions.Instruction Ldc_I4_3(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldc_I4_3(cilInstruction); }
		private static Instructions.Instruction Ldc_I4_4(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldc_I4_4(cilInstruction); }
		private static Instructions.Instruction Ldc_I4_5(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldc_I4_5(cilInstruction); }
		private static Instructions.Instruction Ldc_I4_6(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldc_I4_6(cilInstruction); }
		private static Instructions.Instruction Ldc_I4_7(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldc_I4_7(cilInstruction); }
		private static Instructions.Instruction Ldc_I4_8(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldc_I4_8(cilInstruction); }
		private static Instructions.Instruction Ldc_I4_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldc_I4_S(cilInstruction); }
		private static Instructions.Instruction Ldc_I4(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldc_I4(cilInstruction); }
		private static Instructions.Instruction Ldc_I8(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldc_I8(cilInstruction); }
		private static Instructions.Instruction Ldc_R4(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldc_R4(cilInstruction); }
		private static Instructions.Instruction Ldc_R8(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldc_R8(cilInstruction); }
		private static Instructions.Instruction Dup(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Dup(cilInstruction); }
		private static Instructions.Instruction Pop(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Pop(cilInstruction); }
		private static Instructions.Instruction Jmp(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Jmp(cilInstruction); }
		private static Instructions.Instruction Call(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Call(cilInstruction); }
		private static Instructions.Instruction Calli(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Calli(cilInstruction); }
		private static Instructions.Instruction Ret(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ret(cilInstruction); }
		private static Instructions.Instruction Br_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Br_S(cilInstruction); }
		private static Instructions.Instruction Brfalse_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Brfalse_S(cilInstruction); }
		private static Instructions.Instruction Brtrue_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Brtrue_S(cilInstruction); }
		private static Instructions.Instruction Beq_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Beq_S(cilInstruction); }
		private static Instructions.Instruction Bge_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Bge_S(cilInstruction); }
		private static Instructions.Instruction Bgt_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Bgt_S(cilInstruction); }
		private static Instructions.Instruction Ble_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ble_S(cilInstruction); }
		private static Instructions.Instruction Blt_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Blt_S(cilInstruction); }
		private static Instructions.Instruction Bne_Un_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Bne_Un_S(cilInstruction); }
		private static Instructions.Instruction Bge_Un_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Bge_Un_S(cilInstruction); }
		private static Instructions.Instruction Bgt_Un_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Bgt_Un_S(cilInstruction); }
		private static Instructions.Instruction Ble_Un_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ble_Un_S(cilInstruction); }
		private static Instructions.Instruction Blt_Un_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Blt_Un_S(cilInstruction); }
		private static Instructions.Instruction Br(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Br(cilInstruction); }
		private static Instructions.Instruction Brfalse(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Brfalse(cilInstruction); }
		private static Instructions.Instruction Brtrue(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Brtrue(cilInstruction); }
		private static Instructions.Instruction Beq(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Beq(cilInstruction); }
		private static Instructions.Instruction Bge(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Bge(cilInstruction); }
		private static Instructions.Instruction Bgt(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Bgt(cilInstruction); }
		private static Instructions.Instruction Ble(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ble(cilInstruction); }
		private static Instructions.Instruction Blt(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Blt(cilInstruction); }
		private static Instructions.Instruction Bne_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Bne_Un(cilInstruction); }
		private static Instructions.Instruction Bge_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Bge_Un(cilInstruction); }
		private static Instructions.Instruction Bgt_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Bgt_Un(cilInstruction); }
		private static Instructions.Instruction Ble_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ble_Un(cilInstruction); }
		private static Instructions.Instruction Blt_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Blt_Un(cilInstruction); }
		private static Instructions.Instruction Switch(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Switch(cilInstruction); }
		private static Instructions.Instruction Ldind_I1(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldind_I1(cilInstruction); }
		private static Instructions.Instruction Ldind_U1(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldind_U1(cilInstruction); }
		private static Instructions.Instruction Ldind_I2(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldind_I2(cilInstruction); }
		private static Instructions.Instruction Ldind_U2(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldind_U2(cilInstruction); }
		private static Instructions.Instruction Ldind_I4(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldind_I4(cilInstruction); }
		private static Instructions.Instruction Ldind_U4(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldind_U4(cilInstruction); }
		private static Instructions.Instruction Ldind_I8(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldind_I8(cilInstruction); }
		private static Instructions.Instruction Ldind_I(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldind_I(cilInstruction); }
		private static Instructions.Instruction Ldind_R4(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldind_R4(cilInstruction); }
		private static Instructions.Instruction Ldind_R8(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldind_R8(cilInstruction); }
		private static Instructions.Instruction Ldind_Ref(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldind_Ref(cilInstruction); }
		private static Instructions.Instruction Stind_Ref(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stind_Ref(cilInstruction); }
		private static Instructions.Instruction Stind_I1(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stind_I1(cilInstruction); }
		private static Instructions.Instruction Stind_I2(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stind_I2(cilInstruction); }
		private static Instructions.Instruction Stind_I4(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stind_I4(cilInstruction); }
		private static Instructions.Instruction Stind_I8(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stind_I8(cilInstruction); }
		private static Instructions.Instruction Stind_R4(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stind_R4(cilInstruction); }
		private static Instructions.Instruction Stind_R8(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stind_R8(cilInstruction); }
		private static Instructions.Instruction Add(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Add(cilInstruction); }
		private static Instructions.Instruction Sub(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Sub(cilInstruction); }
		private static Instructions.Instruction Mul(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Mul(cilInstruction); }
		private static Instructions.Instruction Div(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Div(cilInstruction); }
		private static Instructions.Instruction Div_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Div_Un(cilInstruction); }
		private static Instructions.Instruction Rem(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Rem(cilInstruction); }
		private static Instructions.Instruction Rem_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Rem_Un(cilInstruction); }
		private static Instructions.Instruction And(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.And(cilInstruction); }
		private static Instructions.Instruction Or(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Or(cilInstruction); }
		private static Instructions.Instruction Xor(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Xor(cilInstruction); }
		private static Instructions.Instruction Shl(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Shl(cilInstruction); }
		private static Instructions.Instruction Shr(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Shr(cilInstruction); }
		private static Instructions.Instruction Shr_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Shr_Un(cilInstruction); }
		private static Instructions.Instruction Neg(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Neg(cilInstruction); }
		private static Instructions.Instruction Not(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Not(cilInstruction); }
		private static Instructions.Instruction Conv_I1(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_I1(cilInstruction); }
		private static Instructions.Instruction Conv_I2(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_I2(cilInstruction); }
		private static Instructions.Instruction Conv_I4(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_I4(cilInstruction); }
		private static Instructions.Instruction Conv_I8(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_I8(cilInstruction); }
		private static Instructions.Instruction Conv_R4(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_R4(cilInstruction); }
		private static Instructions.Instruction Conv_R8(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_R8(cilInstruction); }
		private static Instructions.Instruction Conv_U4(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_U4(cilInstruction); }
		private static Instructions.Instruction Conv_U8(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_U8(cilInstruction); }
		private static Instructions.Instruction Callvirt(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Callvirt(cilInstruction); }
		private static Instructions.Instruction Cpobj(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Cpobj(cilInstruction); }
		private static Instructions.Instruction Ldobj(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldobj(cilInstruction); }
		private static Instructions.Instruction Ldstr(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldstr(cilInstruction); }
		private static Instructions.Instruction Newobj(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Newobj(cilInstruction); }
		private static Instructions.Instruction Castclass(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Castclass(cilInstruction); }
		private static Instructions.Instruction Isinst(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Isinst(cilInstruction); }
		private static Instructions.Instruction Conv_R_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_R_Un(cilInstruction); }
		private static Instructions.Instruction Unbox(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Unbox(cilInstruction); }
		private static Instructions.Instruction Throw(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Throw(cilInstruction); }
		private static Instructions.Instruction Ldfld(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldfld(cilInstruction); }
		private static Instructions.Instruction Ldflda(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldflda(cilInstruction); }
		private static Instructions.Instruction Stfld(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stfld(cilInstruction); }
		private static Instructions.Instruction Ldsfld(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldsfld(cilInstruction); }
		private static Instructions.Instruction Ldsflda(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldsflda(cilInstruction); }
		private static Instructions.Instruction Stsfld(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stsfld(cilInstruction); }
		private static Instructions.Instruction Stobj(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stobj(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_I1_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_I1_Un(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_I2_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_I2_Un(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_I4_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_I4_Un(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_I8_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_I8_Un(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_U1_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_U1_Un(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_U2_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_U2_Un(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_U4_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_U4_Un(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_U8_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_U8_Un(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_I_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_I_Un(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_U_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_U_Un(cilInstruction); }
		private static Instructions.Instruction Box(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Box(cilInstruction); }
		private static Instructions.Instruction Newarr(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Newarr(cilInstruction); }
		private static Instructions.Instruction Ldlen(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldlen(cilInstruction); }
		private static Instructions.Instruction Ldelema(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldelema(cilInstruction); }
		private static Instructions.Instruction Ldelem_I1(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldelem_I1(cilInstruction); }
		private static Instructions.Instruction Ldelem_U1(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldelem_U1(cilInstruction); }
		private static Instructions.Instruction Ldelem_I2(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldelem_I2(cilInstruction); }
		private static Instructions.Instruction Ldelem_U2(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldelem_U2(cilInstruction); }
		private static Instructions.Instruction Ldelem_I4(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldelem_I4(cilInstruction); }
		private static Instructions.Instruction Ldelem_U4(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldelem_U4(cilInstruction); }
		private static Instructions.Instruction Ldelem_I8(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldelem_I8(cilInstruction); }
		private static Instructions.Instruction Ldelem_I(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldelem_I(cilInstruction); }
		private static Instructions.Instruction Ldelem_R4(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldelem_R4(cilInstruction); }
		private static Instructions.Instruction Ldelem_R8(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldelem_R8(cilInstruction); }
		private static Instructions.Instruction Ldelem_Ref(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldelem_Ref(cilInstruction); }
		private static Instructions.Instruction Stelem_I(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stelem_I(cilInstruction); }
		private static Instructions.Instruction Stelem_I1(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stelem_I1(cilInstruction); }
		private static Instructions.Instruction Stelem_I2(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stelem_I2(cilInstruction); }
		private static Instructions.Instruction Stelem_I4(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stelem_I4(cilInstruction); }
		private static Instructions.Instruction Stelem_I8(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stelem_I8(cilInstruction); }
		private static Instructions.Instruction Stelem_R4(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stelem_R4(cilInstruction); }
		private static Instructions.Instruction Stelem_R8(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stelem_R8(cilInstruction); }
		private static Instructions.Instruction Stelem_Ref(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stelem_Ref(cilInstruction); }
		private static Instructions.Instruction Ldelem_Any(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldelem_Any(cilInstruction); }
		private static Instructions.Instruction Stelem_Any(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stelem_Any(cilInstruction); }
		private static Instructions.Instruction Unbox_Any(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Unbox_Any(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_I1(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_I1(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_U1(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_U1(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_I2(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_I2(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_U2(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_U2(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_I4(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_I4(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_U4(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_U4(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_I8(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_I8(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_U8(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_U8(cilInstruction); }
		private static Instructions.Instruction Refanyval(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Refanyval(cilInstruction); }
		private static Instructions.Instruction Ckfinite(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ckfinite(cilInstruction); }
		private static Instructions.Instruction Mkrefany(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Mkrefany(cilInstruction); }
		private static Instructions.Instruction Ldtoken(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldtoken(cilInstruction); }
		private static Instructions.Instruction Conv_U2(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_U2(cilInstruction); }
		private static Instructions.Instruction Conv_U1(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_U1(cilInstruction); }
		private static Instructions.Instruction Conv_I(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_I(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_I(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_I(cilInstruction); }
		private static Instructions.Instruction Conv_Ovf_U(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_Ovf_U(cilInstruction); }
		private static Instructions.Instruction Add_Ovf(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Add_Ovf(cilInstruction); }
		private static Instructions.Instruction Add_Ovf_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Add_Ovf_Un(cilInstruction); }
		private static Instructions.Instruction Mul_Ovf(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Mul_Ovf(cilInstruction); }
		private static Instructions.Instruction Mul_Ovf_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Mul_Ovf_Un(cilInstruction); }
		private static Instructions.Instruction Sub_Ovf(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Sub_Ovf(cilInstruction); }
		private static Instructions.Instruction Sub_Ovf_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Sub_Ovf_Un(cilInstruction); }
		private static Instructions.Instruction Endfinally(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Endfinally(cilInstruction); }
		private static Instructions.Instruction Leave(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Leave(cilInstruction); }
		private static Instructions.Instruction Leave_S(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Leave_S(cilInstruction); }
		private static Instructions.Instruction Stind_I(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stind_I(cilInstruction); }
		private static Instructions.Instruction Conv_U(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Conv_U(cilInstruction); }
		private static Instructions.Instruction Arglist(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Arglist(cilInstruction); }
		private static Instructions.Instruction Ceq(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ceq(cilInstruction); }
		private static Instructions.Instruction Cgt(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Cgt(cilInstruction); }
		private static Instructions.Instruction Cgt_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Cgt_Un(cilInstruction); }
		private static Instructions.Instruction Clt(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Clt(cilInstruction); }
		private static Instructions.Instruction Clt_Un(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Clt_Un(cilInstruction); }
		private static Instructions.Instruction Ldftn(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldftn(cilInstruction); }
		private static Instructions.Instruction Ldvirtftn(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldvirtftn(cilInstruction); }
		private static Instructions.Instruction Ldarg(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldarg(cilInstruction); }
		private static Instructions.Instruction Ldarga(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldarga(cilInstruction); }
		private static Instructions.Instruction Starg(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Starg(cilInstruction); }
		private static Instructions.Instruction Ldloc(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldloc(cilInstruction); }
		private static Instructions.Instruction Ldloca(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Ldloca(cilInstruction); }
		private static Instructions.Instruction Stloc(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Stloc(cilInstruction); }
		private static Instructions.Instruction Localloc(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Localloc(cilInstruction); }
		private static Instructions.Instruction Endfilter(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Endfilter(cilInstruction); }
		private static Instructions.Instruction Unaligned(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Unaligned(cilInstruction); }
		private static Instructions.Instruction Volatile(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Volatile(cilInstruction); }
		private static Instructions.Instruction Tail(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Tail(cilInstruction); }
		private static Instructions.Instruction Initobj(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Initobj(cilInstruction); }
		private static Instructions.Instruction Constrained(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Constrained(cilInstruction); }
		private static Instructions.Instruction Cpblk(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Cpblk(cilInstruction); }
		private static Instructions.Instruction Initblk(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Initblk(cilInstruction); }
		private static Instructions.Instruction No(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.No(cilInstruction); }
		private static Instructions.Instruction Rethrow(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Rethrow(cilInstruction); }
		private static Instructions.Instruction Sizeof(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Sizeof(cilInstruction); }
		private static Instructions.Instruction Refanytype(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Refanytype(cilInstruction); }
		private static Instructions.Instruction Readonly(Block block, Mono.Cecil.Cil.Instruction cilInstruction) { return block.Readonly(cilInstruction); }
		#endregion
	}
}
