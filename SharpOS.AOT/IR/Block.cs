// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
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

		private BlockType type;

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
		public BlockType Type {
			get {
				return type;
			}
			set {
				type = value;
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

				default:
					break;
			}

			switch (instruction.OpCode.StackBehaviourPush) {

				case StackBehaviour.Push1:

				case StackBehaviour.Pushi:

				case StackBehaviour.Pushi8:

				case StackBehaviour.Pushr4:

				case StackBehaviour.Pushr8:

				case StackBehaviour.Pushref:

				case StackBehaviour.Varpush:
					result++;

					break;

				case StackBehaviour.Push1_push1:
					result += 2;

					break;

				default:
					break;
			}

			return result;
		}

		/// <summary>
		/// Converts the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		private SharpOS.AOT.IR.Instructions.Instruction Convert (Operands.Operand.ConvertType type)
		{
			//this.instructions[this.instructions.Count - 1].Value.ConvertTo = type;

			SharpOS.AOT.IR.Instructions.Instruction instruction = new Assign (this.Register (stack - 1), this.Register (stack - 1));
			instruction.Value.ConvertTo = type;

			return instruction;
		}

		private Register Register (int value)
		{
			if (value < 0)
				throw new Exception ("The register number may not be negative. ('" + this.method.MethodFullName + "')");

			return new Register (value);
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
						this.AddInstruction (new Assign (this.Register (stack++), new ExceptionValue()));
						break;
					}
				}
			}

			foreach (Mono.Cecil.Cil.Instruction cilInstruction in this.cil) {

				SharpOS.AOT.IR.Instructions.Instruction instruction = null;

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
					Reference reference = new Reference (this.Register (stack - 1));
					reference.SizeType = Operand.InternalSizeType.I;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I;

					instruction = new Assign (register, reference);

				} else if (cilInstruction.OpCode == OpCodes.Ldind_I1) {
					Reference reference = new Reference (this.Register (stack - 1));
					reference.SizeType = Operand.InternalSizeType.I1;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I1;

					instruction = new Assign (register, reference);

				} else if (cilInstruction.OpCode == OpCodes.Ldind_I2) {
					Reference reference = new Reference (this.Register (stack - 1));
					reference.SizeType = Operand.InternalSizeType.I2;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I2;

					instruction = new Assign (register, reference);

				} else if (cilInstruction.OpCode == OpCodes.Ldind_I4) {
					Reference reference = new Reference (this.Register (stack - 1));
					reference.SizeType = Operand.InternalSizeType.I4;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I4;

					instruction = new Assign (register, reference);

				} else if (cilInstruction.OpCode == OpCodes.Ldind_I8) {
					Reference reference = new Reference (this.Register (stack - 1));
					reference.SizeType = Operand.InternalSizeType.I8;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I8;

					instruction = new Assign (register, reference);

				} else if (cilInstruction.OpCode == OpCodes.Ldind_R4) {
					Reference reference = new Reference (this.Register (stack - 1));
					reference.SizeType = Operand.InternalSizeType.R4;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.R4;

					instruction = new Assign (register, reference);

				} else if (cilInstruction.OpCode == OpCodes.Ldind_R8) {
					Reference reference = new Reference (this.Register (stack - 1));
					reference.SizeType = Operand.InternalSizeType.R8;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.R8;

					instruction = new Assign (register, reference);

				} else if (cilInstruction.OpCode == OpCodes.Ldind_U1) {
					Reference reference = new Reference (this.Register (stack - 1));
					reference.SizeType = Operand.InternalSizeType.U1;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.U1;

					instruction = new Assign (register, reference);

				} else if (cilInstruction.OpCode == OpCodes.Ldind_U2) {
					Reference reference = new Reference (this.Register (stack - 1));
					reference.SizeType = Operand.InternalSizeType.U2;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.U2;

					instruction = new Assign (register, reference);

				} else if (cilInstruction.OpCode == OpCodes.Ldind_U4) {
					Reference reference = new Reference (this.Register (stack - 1));
					reference.SizeType = Operand.InternalSizeType.U4;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.U4;

					instruction = new Assign (register, reference);
				}

				// Object
				else if (cilInstruction.OpCode == OpCodes.Ldobj) {
					Operands.Object _object = new Operands.Object ((cilInstruction.Operand as TypeReference).FullName, this.Register (stack - 1));
					_object.SizeType = Operand.InternalSizeType.ValueType;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.ValueType;

					instruction = new Assign (register, _object);
				}
				
				// Load Locales
				else if (cilInstruction.OpCode == OpCodes.Ldloca || cilInstruction.OpCode == OpCodes.Ldloca_S) {
					// TODO use the Address() instead of Reference()?
					Reference reference = new Reference (this.Method.GetLocal ((cilInstruction.Operand as VariableDefinition).Index));
					reference.SizeType = Operand.InternalSizeType.I;

					instruction = new Assign (this.Register (stack), reference);

				} else if (cilInstruction.OpCode == OpCodes.Ldloc || cilInstruction.OpCode == OpCodes.Ldloc_S) {
					instruction = new Assign (this.Register (stack), this.Method.GetLocal ((cilInstruction.Operand as VariableDefinition).Index));

				} else if (cilInstruction.OpCode == OpCodes.Ldloc_0) {
					instruction = new Assign (this.Register (stack), this.Method.GetLocal (0));

				} else if (cilInstruction.OpCode == OpCodes.Ldloc_1) {
					instruction = new Assign (this.Register (stack), this.Method.GetLocal (1));

				} else if (cilInstruction.OpCode == OpCodes.Ldloc_2) {
					instruction = new Assign (this.Register (stack), this.Method.GetLocal (2));

				} else if (cilInstruction.OpCode == OpCodes.Ldloc_3) {
					instruction = new Assign (this.Register (stack), this.Method.GetLocal (3));
				}

				// Indirect Store
				else if (cilInstruction.OpCode == OpCodes.Stind_I) {
					Reference reference = new Reference (this.Register (stack - 2));
					reference.SizeType = Operand.InternalSizeType.I;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I;

					instruction = new Assign (reference, register);

				} else if (cilInstruction.OpCode == OpCodes.Stind_I1) {
					Reference reference = new Reference (this.Register (stack - 2));
					reference.SizeType = Operand.InternalSizeType.I1;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I1;

					instruction = new Assign (reference, register);

				} else if (cilInstruction.OpCode == OpCodes.Stind_I2) {
					Reference reference = new Reference (this.Register (stack - 2));
					reference.SizeType = Operand.InternalSizeType.I2;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I2;

					instruction = new Assign (reference, register);

				} else if (cilInstruction.OpCode == OpCodes.Stind_I4) {
					Reference reference = new Reference (this.Register (stack - 2));
					reference.SizeType = Operand.InternalSizeType.I4;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I4;

					instruction = new Assign (reference, register);

				} else if (cilInstruction.OpCode == OpCodes.Stind_I8) {
					Reference reference = new Reference (this.Register (stack - 2));
					reference.SizeType = Operand.InternalSizeType.I8;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.I8;

					instruction = new Assign (reference, register);

				} else if (cilInstruction.OpCode == OpCodes.Stind_R4) {
					Reference reference = new Reference (this.Register (stack - 2));
					reference.SizeType = Operand.InternalSizeType.R4;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.R4;

					instruction = new Assign (reference, register);

				} else if (cilInstruction.OpCode == OpCodes.Stind_R8) {
					Reference reference = new Reference (this.Register (stack - 2));
					reference.SizeType = Operand.InternalSizeType.R8;

					Register register = this.Register (stack - 1);
					register.SizeType = Operand.InternalSizeType.R8;

					instruction = new Assign (reference, register);
				}

				// Store Locales
				else if (cilInstruction.OpCode == OpCodes.Stloc || cilInstruction.OpCode == OpCodes.Stloc_S) {
					instruction = new Assign (this.Method.GetLocal ((cilInstruction.Operand as VariableDefinition).Index), this.Register (stack - 1));

				} else if (cilInstruction.OpCode == OpCodes.Stloc_0) {
					instruction = new Assign (this.Method.GetLocal (0), this.Register (stack - 1));

				} else if (cilInstruction.OpCode == OpCodes.Stloc_1) {
					instruction = new Assign (this.Method.GetLocal (1), this.Register (stack - 1));

				} else if (cilInstruction.OpCode == OpCodes.Stloc_2) {
					instruction = new Assign (this.Method.GetLocal (2), this.Register (stack - 1));

				} else if (cilInstruction.OpCode == OpCodes.Stloc_3) {
					instruction = new Assign (this.Method.GetLocal (3), this.Register (stack - 1));
				}

				// Arguments Load
				else if (cilInstruction.OpCode == OpCodes.Ldarg || cilInstruction.OpCode == OpCodes.Ldarg_S) {
					instruction = new Assign (this.Register (stack), this.Method.GetArgument ((cilInstruction.Operand as ParameterDefinition).Sequence));

				} else if (cilInstruction.OpCode == OpCodes.Ldarga || cilInstruction.OpCode == OpCodes.Ldarga_S) {
					if (cilInstruction.Operand is ParameterDefinition) {
						Address address = new Address (this.Method.GetArgument ((cilInstruction.Operand as ParameterDefinition).Sequence));
						address.SizeType = Operand.InternalSizeType.I;

						instruction = new Assign (this.Register (stack), address);

					} else {
						Address address = new Address (this.Method.GetArgument ((int) cilInstruction.Operand));
						address.SizeType = Operand.InternalSizeType.I;

						instruction = new Assign (this.Register (stack), address);
					}

				} else if (cilInstruction.OpCode == OpCodes.Ldarg_0) {
					instruction = new Assign (this.Register (stack), this.Method.GetArgument (1));

				} else if (cilInstruction.OpCode == OpCodes.Ldarg_1) {
					instruction = new Assign (this.Register (stack), this.Method.GetArgument (2));

				} else if (cilInstruction.OpCode == OpCodes.Ldarg_2) {
					instruction = new Assign (this.Register (stack), this.Method.GetArgument (3));

				} else if (cilInstruction.OpCode == OpCodes.Ldarg_3) {
					instruction = new Assign (this.Register (stack), this.Method.GetArgument (4));
				}

				// Argument Store
				else if (cilInstruction.OpCode == OpCodes.Starg || cilInstruction.OpCode == OpCodes.Starg_S) {
					instruction = new Assign (this.Method.GetArgument ((cilInstruction.Operand as ParameterDefinition).Sequence), this.Register (stack - 1));
				}

				// Call
				else if (cilInstruction.OpCode == OpCodes.Call
						|| cilInstruction.OpCode == OpCodes.Callvirt
						|| cilInstruction.OpCode == OpCodes.Jmp) {
					
					MethodReference call = (cilInstruction.Operand as MethodReference);
					MethodDefinition def = this.Method.Engine.GetCILDefinition (call);
					
					if (def != null) {
						foreach (CustomAttribute attr in def.CustomAttributes) {
							if (attr.Constructor.DeclaringType.FullName == 
									typeof(SharpOS.AOT.Attributes.ADCStubAttribute)
									.FullName) {
								// replace this call with an equivalent call
								// to the ADC layer
								
								this.Method.Engine.FixupADCMethod (call);
							}
						}
					} else {
						this.Method.Engine.Message(3, "Found a reference to undefined method `{0}'",
									   call.ToString());
					}
					
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

				} else if (cilInstruction.OpCode == OpCodes.Newobj) {
					MethodReference call = (cilInstruction.Operand as MethodReference);

					Operand [] operands = new Operand [call.Parameters.Count];

					for (int i = 0; i < call.Parameters.Count; i++)
						operands [i] = this.Register (stack - call.Parameters.Count + i);

					instruction = new Assign (this.Register (stack - call.Parameters.Count), new SharpOS.AOT.IR.Operands.Call (call, operands));

					stack -= call.Parameters.Count;
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
				}
				/*else if (cilInstruction.OpCode == OpCodes.Ldflda)
				{
					instruction = new Assign(this.Register(stack - 1), new Field((cilInstruction.Operand as FieldDefinition).DeclaringType.FullName + "::" + (cilInstruction.Operand as FieldDefinition).Name, this.Register(stack - 1)));
					(instruction.Value as Identifier).SizeType = Operand.InternalSizeType.U;
				}*/
				else if (cilInstruction.OpCode == OpCodes.Ldsfld) {
					FieldReference field = cilInstruction.Operand as FieldReference;
					string fieldName = field.DeclaringType.FullName + "::" + field.Name;

					instruction = new Assign (this.Register (stack), new Field (fieldName));
					(instruction.Value as Identifier).SizeType = this.method.Engine.GetInternalType (field.FieldType.FullName);
				}
				/*else if (cilInstruction.OpCode == OpCodes.Ldsflda)
				{
					instruction = new Assign(this.Register(stack), new Field((cilInstruction.Operand as FieldReference).DeclaringType.FullName + "::" + (cilInstruction.Operand as FieldReference).Name));
					(instruction.Value as Identifier).SizeType = Operand.InternalSizeType.U;
				}*/
				else if (cilInstruction.OpCode == OpCodes.Stfld) {
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
					instruction = new Assign (this.Register (stack - 2), new SharpOS.AOT.IR.Operands.Miscellaneous (new SharpOS.AOT.IR.Operators.Miscellaneous (Operator.MiscellaneousType.NewArray), this.Register (stack - 1)));

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

				if (instruction != null)
					this.AddInstruction (instruction);

				this.stack += GetStackDelta (cilInstruction);
			}

			this.converted = true;
		}

		/// <summary>
		/// Merges the specified block.
		/// </summary>
		/// <param name="block">The block.</param>
		public void Merge (Block block)
		{
			if (this.type == BlockType.OneWay) 
				this.cil.Remove (this.cil[this.cil.Count - 1]);

			this.type = block.type;

			this.outs = block.outs;

			foreach (Mono.Cecil.Cil.Instruction instruction in block.cil) 
				this.cil.Add (instruction);
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
			this.instructions.Remove (instruction);
		}

		/// <summary>
		/// Removes the instruction.
		/// </summary>
		/// <param name="position">The position.</param>
		public void RemoveInstruction (int position)
		{
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
			StringBuilder sb = new StringBuilder();
			DumpProcessor p = new DumpProcessor(DumpType.Text, sb);
			
			Dump (p);

			return sb.ToString ();
		}

		/// <summary>
		/// Updates the index.
		/// </summary>
		public void UpdateIndex ()
		{
			int index = 0;

			Instructions.Instruction.InstructionVisitor visitor = delegate (Instructions.Instruction instruction) {
				instruction.Index = index++;
			};

			foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in this) 
				instruction.VisitInstruction (visitor);
		}

		/// <summary>
		/// Dumps a representation of this block, including lists of blocks which
		/// lead to this block and blocks which are led to after this block is
		/// executed.
		/// </summary>
		/// <param name="p">The Dump Processor.</param>
		public void Dump (DumpProcessor p)
		{
			this.UpdateIndex();
			
			List<int> ins = new List<int>();
			List<int> outs = new List<int>();

			for (int i = 0; i < this.ins.Count; i++)
				ins.Add(this.ins[i].Index);

			for (int i = 0; i < this.outs.Count; i++) 
				outs.Add(this.outs[i].Index);
			
			p.Element(this, ins.ToArray(), outs.ToArray());
			
			for (int i = 0; i < this.InstructionsCount; i++)
				this[i].Dump (p);
			
			p.FinishElement();	// block

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
	}

}
