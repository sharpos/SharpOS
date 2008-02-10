//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
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

		private Method method = null;

		/// <summary>
		/// Gets the method.
		/// </summary>
		/// <value>The method.</value>
		public Method Method
		{
			get
			{
				return method;
			}
		}

		private bool ssaBlock = false;

		/// <summary>
		/// Gets or sets a value indicating whether this block has been added by the SSA.
		/// </summary>
		/// <value><c>true</c> if it has been added by the SSA; otherwise, <c>false</c>.</value>
		public bool SSABlock
		{
			get
			{
				return this.ssaBlock;
			}
			set
			{
				this.ssaBlock = value;
			}
		}

		private List<Mono.Cecil.Cil.Instruction> cil = new List<Mono.Cecil.Cil.Instruction> ();

		/// <summary>
		/// Gets or sets the CIL.
		/// </summary>
		/// <value>The CIL.</value>
		public List<Mono.Cecil.Cil.Instruction> CIL
		{
			get
			{
				return cil;
			}
			set
			{
				cil = value;
			}
		}

		bool converted = false;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Block"/> has been converted to IR.
		/// </summary>
		/// <value><c>true</c> if converted; otherwise, <c>false</c>.</value>
		public bool Converted
		{
			get
			{
				return converted;
			}
		}

		private List<SharpOS.AOT.IR.Instructions.Instruction> instructions = new List<SharpOS.AOT.IR.Instructions.Instruction> ();

		/// <summary>
		/// Gets the <see cref="SharpOS.AOT.IR.Instructions.Instruction"/> at the specified index.
		/// </summary>
		/// <value></value>
		public SharpOS.AOT.IR.Instructions.Instruction this [int index]
		{
			get
			{
				return this.instructions [index];
			}
		}


		/// <summary>
		/// Gets the instructions count.
		/// </summary>
		/// <value>The instructions count.</value>
		public int InstructionsCount
		{
			get
			{
				return this.instructions.Count;
			}
		}

		private List<Block> ins = new List<Block> ();

		/// <summary>
		/// Gets the ins.
		/// </summary>
		/// <value>The ins.</value>
		public List<Block> Ins
		{
			get
			{
				return ins;
			}
		}

		private List<Block> outs = new List<Block> ();

		/// <summary>
		/// Gets the outs.
		/// </summary>
		/// <value>The outs.</value>
		public List<Block> Outs
		{
			get
			{
				return outs;
			}
		}

		private List<Block> dominators = new List<Block> ();

		/// <summary>
		/// Gets or sets the dominators.
		/// </summary>
		/// <value>The dominators.</value>
		public List<Block> Dominators
		{
			get
			{
				return dominators;
			}
			set
			{
				dominators = value;
			}
		}

		private Block immediateDominator = null;

		/// <summary>
		/// Gets or sets the immediate dominator.
		/// </summary>
		/// <value>The immediate dominator.</value>
		public Block ImmediateDominator
		{
			get
			{
				return immediateDominator;
			}
			set
			{
				immediateDominator = value;
			}
		}

		private List<Block> immediateDominatorOf = new List<Block> ();

		/// <summary>
		/// Gets or sets the immediate dominator of.
		/// </summary>
		/// <value>The immediate dominator of.</value>
		public List<Block> ImmediateDominatorOf
		{
			get
			{
				return immediateDominatorOf;
			}
			set
			{
				immediateDominatorOf = value;
			}
		}

		private List<Block> dominanceFrontiers = new List<Block> ();

		/// <summary>
		/// Gets or sets the dominance frontiers.
		/// </summary>
		/// <value>The dominance frontiers.</value>
		public List<Block> DominanceFrontiers
		{
			get
			{
				return dominanceFrontiers;
			}
			set
			{
				dominanceFrontiers = value;
			}
		}

		private int index = 0;

		/// <summary>
		/// Gets or sets the index.
		/// </summary>
		/// <value>The index.</value>
		public int Index
		{
			get
			{
				return index;
			}
			set
			{
				index = value;
			}
		}

		/// <summary>
		/// Gets the start offset.
		/// </summary>
		/// <value>The start offset.</value>
		public long StartOffset
		{
			get
			{
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
		public long EndOffset
		{
			get
			{
				if (this.cil.Count > 0)
					return this.cil [this.cil.Count - 1].Offset;

				if (this.InstructionsCount > 0)
					return this [this.InstructionsCount - 1].EndOffset;

				return 0;
			}
		}

		bool isTryLast = false;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is try end.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is try end; otherwise, <c>false</c>.
		/// </value>
		public bool IsTryLast
		{
			get
			{
				return isTryLast;
			}
			set
			{
				isTryLast = value;
			}
		}

		bool isFinallyFilterFaultStart = false;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is finally filter fault start.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is finally filter fault start; otherwise, <c>false</c>.
		/// </value>
		public bool IsFinallyFilterFaultStart
		{
			get
			{
				return isFinallyFilterFaultStart;
			}
			set
			{
				isFinallyFilterFaultStart = value;
			}
		}

		bool isFilterLast = false;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is filter end.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is filter end; otherwise, <c>false</c>.
		/// </value>
		public bool IsFilterLast
		{
			get
			{
				return isFilterLast;
			}
			set
			{
				isFilterLast = value;
			}
		}

		bool isCatchBegin = false;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is catch begin.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is catch begin; otherwise, <c>false</c>.
		/// </value>
		public bool IsCatchBegin
		{
			get
			{
				return isCatchBegin;
			}
			set
			{
				isCatchBegin = value;
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
			// TODO
			/*foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in this.instructions)
				instruction.VisitBlock (visitor);

			visitor (this);*/
		}

		Stack<Register> stack = new Stack<Register> ();

		/// <summary>
		/// Gets the stack.
		/// </summary>
		/// <value>The stack.</value>
		public Stack<Register> Stack
		{
			get
			{
				return stack;
			}
		}

		private Register SetRegister ()
		{
			if (this.stack.Count > this.method.MaxStack)
				throw new EngineException ("Stack overflow. ('" + this.method.MethodFullName + "')");

			Register register = new Register (this.stack.Count);

			register.Version = ++this.method.RegisterVersions [this.stack.Count];

			this.stack.Push (register);

			return register;
		}

		private Register GetRegister ()
		{
			if (this.stack.Count == 0)
				throw new EngineException ("Stack underflow. ('" + this.method.MethodFullName + "')");

			return this.stack.Pop ();
		}

		private Argument GetArgument (int value, bool sequence)
		{
			return this.Method.GetArgument (value, sequence);
		}

		private Argument SetArgument (int value, bool sequence)
		{
			return this.Method.GetArgument (value, sequence);
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
			this.instructions = new List<SharpOS.AOT.IR.Instructions.Instruction> ();

			bool found = false;

			// We get the number of stack values from one of the blocks that has been processed and lead to the current one.
			foreach (Block _in in this.ins)
				if (_in.converted) {
					found = true;

					for (int i = 0; i < _in.stack.Count; i++) {
						Register [] operands = new Register [this.ins.Count];

						Register result = this.SetRegister ();

						PHI phi = new PHI (result, operands);

						this.instructions.Add (phi);
					}
				}

			// Throw an error if none of the blocks that lead to this one has been previously processed
			if (!found && this.ins.Count > 0 && this.index > 0)
				throw new EngineException ("The conversion from CIL in '" + this.method.MethodFullName + "' for block #'" + this.index + "' failed.");

			if (this.IsCatchBegin) {
				Register exception = this.SetRegister ();
				exception.InternalType = InternalType.O;
				exception.Parent = new Instructions.Ldnull (exception);
			}

			foreach (Mono.Cecil.Cil.Instruction cilInstruction in this.cil) {
				Instructions.Instruction instruction = Block.ilDispatcher [(int) cilInstruction.OpCode.Code] (this, cilInstruction);

				// Prevent System.Object::.ctor from calling itself
				if (instruction is Call
						&& this.method.IsConstructor
						&& this.method.Class.TypeFullName == Mono.Cecil.Constants.Object
						&& (instruction as Call).Method.Class.TypeFullName == Mono.Cecil.Constants.Object)
					instruction = null;

				if (instruction != null)
					this.AddInstruction (instruction);
			}

			this.converted = true;

			if (this.outs.Count == 0 && this.stack.Count > 0)
				throw new EngineException (string.Format ("The stack is not empty when leaving in '{0}'", this.method.MethodFullName));
		}

		public void EndCILConversion ()
		{
			foreach (Instructions.Instruction instruction in this.instructions) {
				if (instruction is PHI) {
					PHI phi = instruction as PHI;

					int index = (phi.Def as Register).Index;

					int i = 0;
					foreach (Block block in this.ins) {
						if (index >= block.stack.Count)
							throw new EngineException (string.Format ("Inconsistent stack. ({0})", this.method.MethodFullName));

						phi.Use [i] = block.stack.ToArray () [index];

						i++;
					}
				}

				instruction.Process (this.method);

				if (instruction.Def != null
						&& instruction.Def is Register)
					(instruction.Def as Register).Parent = instruction;

				// TODO Check the type of all operands if they are set especially the def's
			}
		}

		private List<PHI> phiList = new List<PHI> ();

		public void TransformationOutOfSSA ()
		{
			foreach (Instructions.Instruction instruction in this.instructions) {
				if (!(instruction is PHI))
					break;

				PHI phi = instruction as PHI;

				this.phiList.Add (phi);
			}

			foreach (PHI phi in this.phiList) {
				phi.Attach ();

				this.instructions.Remove (phi);
			}
		}

		public void UpdateRegisterAndStackValues ()
		{
			foreach (IR.Instructions.Instruction instruction in this.instructions)
				if (instruction.Def != null
						&& instruction.Def is IR.Operands.Register
						&& (instruction.Def as IR.Operands.Register).PHI != null) {
					IR.Operands.Register parent = (instruction.Def as IR.Operands.Register);
					IR.Operands.Register identifier = parent;

					do {
						identifier = identifier.PHI as IR.Operands.Register;
					} while (identifier.PHI != null);

					parent.Register = identifier.Register;
					parent.Stack = identifier.Stack;
				}
		}

		/// <summary>
		/// Merges the specified block.
		/// </summary>
		/// <param name="block">The block.</param>
		public void Merge (Block block)
		{
			// Remove the jump that connects this block with the block parameter
			if (this.outs.Count == 1)
				this.cil.Remove (this.cil [this.cil.Count - 1]);

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
				for (int j = 0; j < this.instructions [i].Branches.Count; j++) {
					if (this.instructions [i].Branches [j] == this.instructions [position])
						this.instructions [i].Branches [j] = instruction;
				}
			}

			this.instructions [position] = instruction;
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
			DumpProcessor p = new DumpProcessor ((int) DumpType.Buffer);

			Dump (p);

			return p.ToString ();
		}

		/*
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

			List<int> ins = new List<int> ();
			List<int> outs = new List<int> ();

			for (int i = 0; i < this.ins.Count; i++)
				ins.Add (this.ins [i].Index);

			for (int i = 0; i < this.outs.Count; i++)
				outs.Add (this.outs [i].Index);

			p.Element (this, ins.ToArray (), outs.ToArray ());

			for (int i = 0; i < this.InstructionsCount; i++)
				this [i].Dump (p);

			p.PopElement ();	// block

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

		private SharpOS.AOT.IR.Instructions.Instruction Convert (Instructions.Convert.Type type)
		{
			Register value = this.GetRegister ();

			Register assignee = this.SetRegister ();

			return new Instructions.Convert (type, assignee, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_I (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_I);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_I1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_I1);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_I2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_I2);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_I4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_I4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_I8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_I8);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_I);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_I_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_I1);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I1_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_I1_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_I2);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I2_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_I2_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_I4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I4_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_I4_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_I8);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_I8_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_I8_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_U);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_U_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_U1);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U1_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_U1_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_U2);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U2_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_U2_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_U4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U4_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_U4_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_U8);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_Ovf_U8_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_Ovf_U8_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_R_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_R_Un);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_R4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_R4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_R8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_R8);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_U (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_U);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_U1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_U1);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_U2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_U2);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_U4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_U4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Conv_U8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return this.Convert (Instructions.Convert.Type.Conv_U8);
		}

		#endregion

		#region ADD

		private SharpOS.AOT.IR.Instructions.Instruction Add (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return AddHandler (Instructions.Add.Type.Add);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Add_Ovf (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return AddHandler (Instructions.Add.Type.AddSignedWithOverflowCheck);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Add_Ovf_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return AddHandler (Instructions.Add.Type.AddUnsignedWithOverflowCheck);
		}

		private SharpOS.AOT.IR.Instructions.Instruction AddHandler (SharpOS.AOT.IR.Instructions.Add.Type type)
		{
			Register first = this.GetRegister ();
			Register second = this.GetRegister ();

			Register assignee = this.SetRegister ();

			return new SharpOS.AOT.IR.Instructions.Add (type, assignee, second, first);
		}

		#endregion

		#region SUB

		private SharpOS.AOT.IR.Instructions.Instruction Sub (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return SubHandler (Instructions.Sub.Type.Sub);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Sub_Ovf (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return SubHandler (Instructions.Sub.Type.SubSignedWithOverflowCheck);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Sub_Ovf_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return SubHandler (Instructions.Sub.Type.SubUnsignedWithOverflowCheck);
		}

		private SharpOS.AOT.IR.Instructions.Instruction SubHandler (SharpOS.AOT.IR.Instructions.Sub.Type type)
		{
			Register first = this.GetRegister ();
			Register second = this.GetRegister ();

			Register assignee = this.SetRegister ();

			return new SharpOS.AOT.IR.Instructions.Sub (type, assignee, second, first);
		}

		#endregion

		#region MUL

		private SharpOS.AOT.IR.Instructions.Instruction Mul (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return MulHandler (Instructions.Mul.Type.Mul);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Mul_Ovf (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return MulHandler (Instructions.Mul.Type.MulSignedWithOverflowCheck);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Mul_Ovf_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return MulHandler (Instructions.Mul.Type.MulUnsignedWithOverflowCheck);
		}

		private SharpOS.AOT.IR.Instructions.Instruction MulHandler (SharpOS.AOT.IR.Instructions.Mul.Type type)
		{
			Register first = this.GetRegister ();
			Register second = this.GetRegister ();

			Register assignee = this.SetRegister ();

			return new SharpOS.AOT.IR.Instructions.Mul (type, assignee, second, first);
		}

		#endregion

		#region DIV

		private SharpOS.AOT.IR.Instructions.Instruction Div (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return DivHandler (Instructions.Div.Type.Div);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Div_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return DivHandler (Instructions.Div.Type.DivUnsigned);
		}

		private SharpOS.AOT.IR.Instructions.Instruction DivHandler (SharpOS.AOT.IR.Instructions.Div.Type type)
		{
			Register first = this.GetRegister ();
			Register second = this.GetRegister ();

			Register assignee = this.SetRegister ();

			return new SharpOS.AOT.IR.Instructions.Div (type, assignee, second, first);
		}

		#endregion

		#region REM

		private SharpOS.AOT.IR.Instructions.Instruction Rem (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return RemHandler (Instructions.Rem.Type.Remainder);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Rem_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return RemHandler (Instructions.Rem.Type.RemainderUnsigned);
		}

		private SharpOS.AOT.IR.Instructions.Instruction RemHandler (Rem.Type type)
		{
			Register first = this.GetRegister ();
			Register second = this.GetRegister ();

			Register assignee = this.SetRegister ();

			return new SharpOS.AOT.IR.Instructions.Rem (type, assignee, second, first);
		}

		#endregion

		private SharpOS.AOT.IR.Instructions.Instruction Neg (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register value = this.GetRegister ();

			Register assignee = this.SetRegister ();

			return new Neg (assignee, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction And (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register first = this.GetRegister ();
			Register second = this.GetRegister ();

			Register assignee = this.SetRegister ();

			return new And (assignee, second, first);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Or (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register first = this.GetRegister ();
			Register second = this.GetRegister ();

			Register assignee = this.SetRegister ();

			return new Or (assignee, second, first);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Xor (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register first = this.GetRegister ();
			Register second = this.GetRegister ();

			Register assignee = this.SetRegister ();

			return new Xor (assignee, second, first);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Not (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register value = this.GetRegister ();

			Register assignee = this.SetRegister ();

			return new Not (assignee, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Shl (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register first = this.GetRegister ();
			Register second = this.GetRegister ();

			Register assignee = this.SetRegister ();

			return new Shl (assignee, second, first);
		}

		#region SHR

		private SharpOS.AOT.IR.Instructions.Instruction Shr_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return ShrHandler (Instructions.Shr.Type.SHRUnsigned);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Shr (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return ShrHandler (Instructions.Shr.Type.SHR);
		}

		private SharpOS.AOT.IR.Instructions.Instruction ShrHandler (Instructions.Shr.Type type)
		{
			Register first = this.GetRegister ();
			Register second = this.GetRegister ();

			Register assignee = this.SetRegister ();

			return new SharpOS.AOT.IR.Instructions.Shr (type, assignee, second, first);
		}

		#endregion

		#region Branch

		private SharpOS.AOT.IR.Instructions.Instruction Beq (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.Equal);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Beq_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.Equal);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bge (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.GreaterThanOrEqual);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bge_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.GreaterThanOrEqual);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bge_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.GreaterThanOrEqualUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bge_Un_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.GreaterThanOrEqualUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bgt (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.GreaterThan);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bgt_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.GreaterThan);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bgt_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.GreaterThanUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bgt_Un_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.GreaterThanUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ble (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.LessThanOrEqual);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ble_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.LessThanOrEqual);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ble_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.LessThanOrEqualUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ble_Un_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.LessThanOrEqualUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Blt (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.LessThan);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Blt_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.LessThan);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Blt_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.LessThanUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Blt_Un_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.LessThanUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bne_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.NotEqualOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Bne_Un_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BranchHandler (RelationalType.NotEqualOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction BranchHandler (RelationalType type)
		{
			Register first = this.GetRegister ();
			Register second = this.GetRegister ();

			return new SharpOS.AOT.IR.Instructions.Branch (type, second, first);
		}

		#endregion

		#region BR True/False

		private SharpOS.AOT.IR.Instructions.Instruction Brfalse (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BrTrueFalseHandler (Instructions.SimpleBranch.Type.False);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Brfalse_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BrTrueFalseHandler (Instructions.SimpleBranch.Type.False);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Brtrue (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BrTrueFalseHandler (Instructions.SimpleBranch.Type.True);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Brtrue_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return BrTrueFalseHandler (Instructions.SimpleBranch.Type.True);
		}

		private SharpOS.AOT.IR.Instructions.Instruction BrTrueFalseHandler (SharpOS.AOT.IR.Instructions.SimpleBranch.Type type)
		{
			Register register = this.GetRegister ();

			return new SharpOS.AOT.IR.Instructions.SimpleBranch (type, register);
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
			return new Leave ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Leave_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return new Leave ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Endfinally (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return new Endfinally ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Endfilter (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register value = this.GetRegister ();

			return new Endfilter (value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Throw (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return new Throw (this.GetRegister ());
		}

		private SharpOS.AOT.IR.Instructions.Instruction Rethrow (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return new Throw ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ret (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			if (this.method.MethodDefinition.ReturnType.ReturnType.FullName.Equals (Mono.Cecil.Constants.Void))
				return new Return ();

			return new Return (this.GetRegister ());
		}

		private SharpOS.AOT.IR.Instructions.Instruction Switch (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Mono.Cecil.Cil.Instruction [] instructions = (cilInstruction.Operand as Mono.Cecil.Cil.Instruction []);

			Block [] blocks = new Block [instructions.Length];

			for (int i = 0; i < instructions.Length; i++)
				blocks [i] = this.method.GetBlockByOffset (instructions [i].Offset);

			return new Switch (this.GetRegister (), blocks);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Pop (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return new Pop (this.GetRegister ());
		}

		private SharpOS.AOT.IR.Instructions.Instruction Dup (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register value = this.stack.Peek ();

			Register assignee = this.SetRegister ();

			return new Dup (assignee, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Sizeof (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register assignee = this.SetRegister ();

			return new SizeOf (assignee, cilInstruction.Operand as TypeReference);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Localloc (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register value = this.GetRegister ();

			Register assignee = this.SetRegister ();

			return new Localloc (assignee, value);
		}

		#region Conditional Check

		private SharpOS.AOT.IR.Instructions.Instruction Ceq (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return ConditionCheckHandler (RelationalType.Equal);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Cgt (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return ConditionCheckHandler (RelationalType.GreaterThan);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Cgt_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return ConditionCheckHandler (RelationalType.GreaterThanUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Clt (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return ConditionCheckHandler (RelationalType.LessThan);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Clt_Un (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return ConditionCheckHandler (RelationalType.LessThanUnsignedOrUnordered);
		}

		private SharpOS.AOT.IR.Instructions.Instruction ConditionCheckHandler (RelationalType type)
		{
			Register first = this.GetRegister ();
			Register second = this.GetRegister ();

			Register assignee = this.SetRegister ();

			return new SharpOS.AOT.IR.Instructions.ConditionCheck (type, assignee, second, first);
		}

		#endregion

		private SharpOS.AOT.IR.Instructions.Instruction Ldlen (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register value = this.GetRegister ();

			Register assignee = this.SetRegister ();

			return new Ldlen (assignee, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldtoken (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			TokenConstant value = new TokenConstant (cilInstruction.Operand.ToString ());

			Register assignee = this.SetRegister ();

			return new Ldtoken (assignee, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldnull (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return new Ldnull (this.SetRegister ());
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldstr (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			StringConstant value = new StringConstant (cilInstruction.Operand.ToString ());

			Register assignee = this.SetRegister ();

			return new Ldstr (assignee, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldftn (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			TokenConstant value = new TokenConstant ((cilInstruction.Operand as Mono.Cecil.MethodReference).ToString ());

			Register assignee = this.SetRegister ();

			return new Ldftn (assignee, value);
		}

		#region Ldc
		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_0 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcIntHandler (0);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcIntHandler (1);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcIntHandler (2);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_3 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcIntHandler (3);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcIntHandler (4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_5 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcIntHandler (5);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_6 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcIntHandler (6);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_7 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcIntHandler (7);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcIntHandler (8);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_M1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcIntHandler (-1);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcIntHandler (System.Convert.ToInt32 (cilInstruction.Operand));
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcIntHandler (System.Convert.ToInt32 (cilInstruction.Operand));
		}

		private SharpOS.AOT.IR.Instructions.Instruction LdcIntHandler (int value)
		{
			return LdcHandler (new IntConstant (value));
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_I8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcHandler (new LongConstant (System.Convert.ToInt64 (cilInstruction.Operand)));
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_R4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcHandler (new FloatConstant (System.Convert.ToSingle (cilInstruction.Operand)));
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldc_R8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdcHandler (new DoubleConstant (System.Convert.ToDouble (cilInstruction.Operand)));
		}

		private SharpOS.AOT.IR.Instructions.Instruction LdcHandler (Constant value)
		{
			Register assignee = this.SetRegister ();

			return new Ldc (assignee, value);
		}
		#endregion

		#region Ldind
		private SharpOS.AOT.IR.Instructions.Instruction Ldind_I (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (InternalType.I, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_I1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (InternalType.I1, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_I2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (InternalType.I2, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_I4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (InternalType.I4, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_I8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (InternalType.I8, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_R4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (InternalType.R4, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_R8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (InternalType.R8, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_U1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (InternalType.U1, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_U2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (InternalType.U2, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_U4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (InternalType.U4, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldind_Ref (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdindHandler (InternalType.O, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction LdindHandler (InternalType sizeType, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register indirect = this.GetRegister ();
			Register assignee = this.SetRegister ();

			return new Ldind (sizeType, assignee, indirect);
		}
		#endregion

		#region Obj
		private SharpOS.AOT.IR.Instructions.Instruction Newobj (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Mono.Cecil.MethodReference call = (cilInstruction.Operand as Mono.Cecil.MethodReference);

			Operand [] operands = new Operand [call.Parameters.Count];

			for (int i = 0; i < call.Parameters.Count; i++)
				operands [call.Parameters.Count - 1 - i] = this.GetRegister ();

			Register assignee = this.SetRegister ();

			return new Newobj (this.method.Engine.GetMethod (call), assignee, operands);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldobj (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			TypeReference typeReference = cilInstruction.Operand as TypeReference;

			Register instance = this.GetRegister ();

			Register register = this.SetRegister ();

			return new Ldobj (register, this.method.Engine.GetClass (typeReference), instance);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stobj (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			TypeReference typeReference = cilInstruction.Operand as TypeReference;

			Register register = this.GetRegister ();

			Register instance = this.GetRegister ();

			return new Stobj (typeReference, instance, register);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Initobj (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			TypeReference typeReference = cilInstruction.Operand as TypeReference;

			Register instance = this.GetRegister ();

			return new Initobj (typeReference, instance);
		}
		#endregion

		#region Ldloca
		private SharpOS.AOT.IR.Instructions.Instruction Ldloca (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdlocaHandler (cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldloca_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdlocaHandler (cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction LdlocaHandler (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Local value = this.GetLocal ((cilInstruction.Operand as VariableDefinition).Index);

			Register assignee = this.SetRegister ();

			return new Ldloca (assignee, value);
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
			return LdlocHandler ((cilInstruction.Operand as VariableDefinition).Index, cilInstruction);
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

			Register assignee = this.SetRegister ();

			return new Ldloc (assignee, value);
		}
		#endregion

		#region Stind
		private SharpOS.AOT.IR.Instructions.Instruction Stind_I (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StindHandler (InternalType.I, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stind_I1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StindHandler (InternalType.I1, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stind_I2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StindHandler (InternalType.I2, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stind_I4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StindHandler (InternalType.I4, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stind_I8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StindHandler (InternalType.I8, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stind_R4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StindHandler (InternalType.R4, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stind_R8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StindHandler (InternalType.R8, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stind_Ref (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StindHandler (InternalType.O, cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction StindHandler (InternalType sizeType, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Register register = this.GetRegister ();
			Register assignee = this.GetRegister ();

			return new Stind (sizeType, assignee, register);
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
			return StlocHandler ((cilInstruction.Operand as VariableDefinition).Index, cilInstruction);
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
			Register value = this.GetRegister ();

			Local assignee = this.SetLocal (index);

			return new Stloc (assignee, value);
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
			return LdargHandler ((cilInstruction.Operand as ParameterDefinition).Sequence - 1, cilInstruction, true);
		}
		#endregion

		#region Ldarga
		private SharpOS.AOT.IR.Instructions.Instruction Ldarga (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdargaHandler (cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldarga_S (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdargaHandler (cilInstruction);
		}

		private SharpOS.AOT.IR.Instructions.Instruction LdargaHandler (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Argument value;

			if (cilInstruction.Operand is ParameterDefinition)
				value = this.GetArgument ((cilInstruction.Operand as ParameterDefinition).Sequence - 1, true);
			else
				value = this.GetArgument ((int) cilInstruction.Operand, false);

			Register assignee = this.SetRegister ();

			return new Ldarga (assignee, value);
		}
		#endregion

		#region Ldarg X
		private SharpOS.AOT.IR.Instructions.Instruction Ldarg_0 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdargHandler (0, cilInstruction, false);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldarg_1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdargHandler (1, cilInstruction, false);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldarg_2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdargHandler (2, cilInstruction, false);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldarg_3 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdargHandler (3, cilInstruction, false);
		}

		private SharpOS.AOT.IR.Instructions.Instruction LdargHandler (int i, Mono.Cecil.Cil.Instruction cilInstruction, bool sequence)
		{
			Argument value = this.GetArgument (i, sequence);

			Register assignee = this.SetRegister ();

			return new Ldarg (assignee, value);

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
			Register value = this.GetRegister ();

			Argument assignee = this.SetArgument ((cilInstruction.Operand as ParameterDefinition).Sequence - 1, true);

			return new Starg (assignee, value);
		}
		#endregion

		#region Call
		private SharpOS.AOT.IR.Instructions.Instruction Call (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Method method;
			Register result;
			Operand [] parameters;

			CallHandling (cilInstruction, out method, out result, out parameters);

			return new Call (method, result, parameters);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Calli (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Method method;
			Register result;
			Operand [] parameters;

			CallHandling (cilInstruction, out method, out result, out parameters);

			return new Call (method, result, parameters);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Callvirt (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Method method;
			Register result;
			Operand [] parameters;

			CallHandling (cilInstruction, out method, out result, out parameters);

			return new Callvirt (method, result, parameters);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Jmp (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			Method method;
			Register result;
			Operand [] parameters;

			CallHandling (cilInstruction, out method, out result, out parameters);

			return new Call (method, result, parameters);
		}

		private void CallHandling (Mono.Cecil.Cil.Instruction cilInstruction, out Method method, out Register result, out Operand [] operands)
		{
			Mono.Cecil.MethodReference call = (cilInstruction.Operand as Mono.Cecil.MethodReference);
			MethodDefinition def = this.Method.Engine.GetCILDefinition (call);
			result = null;

			if (def != null) {
				foreach (CustomAttribute attr in def.CustomAttributes) {
					if (attr.Constructor.DeclaringType.FullName ==
							typeof (SharpOS.AOT.Attributes.ADCStubAttribute).FullName) {

						call = this.Method.Engine.FixupADCMethod (call);
					}
				}
			}

			method = this.method.Engine.GetMethod (call);

			if (call.HasThis)
				operands = new Operand [call.Parameters.Count + 1];

			else
				operands = new Operand [call.Parameters.Count];

			for (int i = 0; i < operands.Length; i++)
				operands [operands.Length - 1 - i] = this.GetRegister ();

			if (call.ReturnType.ReturnType.FullName != Constants.Void)
				result = this.SetRegister ();
		}

		#endregion

		#region Fld
		private SharpOS.AOT.IR.Instructions.Instruction Ldfld (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			FieldReference fieldReference = cilInstruction.Operand as FieldReference;

			Register instance = this.GetRegister ();

			FieldOperand field = new FieldOperand (this.method.Engine.GetField (fieldReference), instance);

			Register assignee = this.SetRegister ();

			return new Ldfld (assignee, field);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldflda (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			FieldReference fieldReference = cilInstruction.Operand as FieldReference;

			Register instance = this.GetRegister ();

			FieldOperand field = new FieldOperand (this.method.Engine.GetField (fieldReference), instance);

			Register assignee = this.SetRegister ();

			return new Ldflda (assignee, field);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldsfld (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			FieldReference fieldReference = cilInstruction.Operand as FieldReference;

			FieldOperand field = new FieldOperand (this.method.Engine.GetField (fieldReference));

			Register assignee = this.SetRegister ();

			return new Ldsfld (assignee, field);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldsflda (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			FieldReference fieldReference = cilInstruction.Operand as FieldReference;

			FieldOperand field = new FieldOperand (this.method.Engine.GetField (fieldReference));

			Register assignee = this.SetRegister ();

			return new Ldsflda (assignee, field);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stfld (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			FieldReference fieldReference = cilInstruction.Operand as FieldReference;

			Register value = this.GetRegister ();

			Register instance = this.GetRegister ();

			FieldOperand field = new FieldOperand (this.method.Engine.GetField (fieldReference), instance);

			return new Stfld (field, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stsfld (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			FieldReference fieldReference = cilInstruction.Operand as FieldReference;

			Register value = this.GetRegister ();

			FieldOperand field = new FieldOperand (this.method.Engine.GetField (fieldReference));

			return new Stsfld (field, value);
		}
		#endregion

		#region Stelem
		private SharpOS.AOT.IR.Instructions.Instruction Stelem_Ref (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StelemHandler (cilInstruction, InternalType.O);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stelem_I (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StelemHandler (cilInstruction, InternalType.I);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stelem_I1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StelemHandler (cilInstruction, InternalType.I1);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stelem_I2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StelemHandler (cilInstruction, InternalType.I2);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stelem_I4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StelemHandler (cilInstruction, InternalType.I4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stelem_I8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StelemHandler (cilInstruction, InternalType.I8);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stelem_R4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StelemHandler (cilInstruction, InternalType.R4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stelem_R8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return StelemHandler (cilInstruction, InternalType.R8);
		}

		private SharpOS.AOT.IR.Instructions.Instruction StelemHandler (Mono.Cecil.Cil.Instruction cilInstruction, InternalType sizeType)
		{
			Register first = this.GetRegister ();
			Register second = this.GetRegister ();
			Register value = this.GetRegister ();

			return new Stelem (sizeType, second, first, value);
		}
		#endregion

		#region Ldelem
		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_Ref (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, InternalType.O);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_I (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, InternalType.I);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_I1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, InternalType.I1);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_I2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, InternalType.I2);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_I4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, InternalType.I4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_I8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, InternalType.I8);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_R4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, InternalType.R4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_R8 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, InternalType.R8);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_U1 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, InternalType.U1);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_U2 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, InternalType.U2);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_U4 (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return LdelemHandler (cilInstruction, InternalType.U4);
		}

		private SharpOS.AOT.IR.Instructions.Instruction LdelemHandler (Mono.Cecil.Cil.Instruction cilInstruction, InternalType sizeType)
		{
			Register first = this.GetRegister ();
			Register second = this.GetRegister ();

			Register assignee = this.SetRegister ();

			return new Ldelem (sizeType, assignee, second, first);
		}
		#endregion

		#region Boxing
		private SharpOS.AOT.IR.Instructions.Instruction Box (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			TypeReference typeReference = cilInstruction.Operand as TypeReference;

			Register value = this.GetRegister ();

			Register result = this.SetRegister ();

			return new Box (this.method.Engine.GetClass (typeReference), result, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Unbox (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			TypeReference typeReference = cilInstruction.Operand as TypeReference;

			Register value = this.GetRegister ();

			Register result = this.SetRegister ();

			return new Unbox (this.method.Engine.GetClass (typeReference), result, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Unbox_Any (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			TypeReference typeReference = cilInstruction.Operand as TypeReference;

			Register value = this.GetRegister ();

			Register result = this.SetRegister ();

			return new UnboxAny (this.method.Engine.GetClass (typeReference), result, value);
		}
		#endregion

		private SharpOS.AOT.IR.Instructions.Instruction Newarr (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			TypeReference typeReference = cilInstruction.Operand as TypeReference;

			Register value = this.GetRegister ();

			Register result = this.SetRegister ();

			return new Newarr (this.method.Engine.GetClassByName (typeReference.FullName + "[]"), result, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Isinst (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			TypeReference typeReference = cilInstruction.Operand as TypeReference;

			Register value = this.GetRegister ();

			Register result = this.SetRegister ();

			return new Isinst (this.method.Engine.GetClass (typeReference), result, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Castclass (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			TypeReference typeReference = cilInstruction.Operand as TypeReference;

			Register value = this.GetRegister ();
			Register result = this.SetRegister ();

			return new Castclass (this.method.Engine.GetClass (typeReference), result, value);
		}

		private SharpOS.AOT.IR.Instructions.Instruction Break (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return new Break ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelem_Any (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new NotImplementedEngineException ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Stelem_Any (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new NotImplementedEngineException ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldelema (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new NotImplementedEngineException ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Cpobj (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new NotImplementedEngineException ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Refanyval (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new NotImplementedEngineException ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ckfinite (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new NotImplementedEngineException ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Mkrefany (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new NotImplementedEngineException ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Arglist (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new NotImplementedEngineException ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Ldvirtftn (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new NotImplementedEngineException ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Unaligned (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new NotImplementedEngineException ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Volatile (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new NotImplementedEngineException ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Tail (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new NotImplementedEngineException ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Constrained (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new NotImplementedEngineException ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Cpblk (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new NotImplementedEngineException ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Initblk (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new NotImplementedEngineException ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction No (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new NotImplementedEngineException ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Refanytype (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new NotImplementedEngineException ();
		}

		private SharpOS.AOT.IR.Instructions.Instruction Readonly (Mono.Cecil.Cil.Instruction cilInstruction)
		{
			throw new NotImplementedEngineException ();
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

		private static Instructions.Instruction Nop (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Nop (cilInstruction);
		}
		private static Instructions.Instruction Break (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Break (cilInstruction);
		}
		private static Instructions.Instruction Ldarg_0 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldarg_0 (cilInstruction);
		}
		private static Instructions.Instruction Ldarg_1 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldarg_1 (cilInstruction);
		}
		private static Instructions.Instruction Ldarg_2 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldarg_2 (cilInstruction);
		}
		private static Instructions.Instruction Ldarg_3 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldarg_3 (cilInstruction);
		}
		private static Instructions.Instruction Ldloc_0 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldloc_0 (cilInstruction);
		}
		private static Instructions.Instruction Ldloc_1 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldloc_1 (cilInstruction);
		}
		private static Instructions.Instruction Ldloc_2 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldloc_2 (cilInstruction);
		}
		private static Instructions.Instruction Ldloc_3 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldloc_3 (cilInstruction);
		}
		private static Instructions.Instruction Stloc_0 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stloc_0 (cilInstruction);
		}
		private static Instructions.Instruction Stloc_1 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stloc_1 (cilInstruction);
		}
		private static Instructions.Instruction Stloc_2 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stloc_2 (cilInstruction);
		}
		private static Instructions.Instruction Stloc_3 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stloc_3 (cilInstruction);
		}
		private static Instructions.Instruction Ldarg_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldarg_S (cilInstruction);
		}
		private static Instructions.Instruction Ldarga_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldarga_S (cilInstruction);
		}
		private static Instructions.Instruction Starg_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Starg_S (cilInstruction);
		}
		private static Instructions.Instruction Ldloc_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldloc_S (cilInstruction);
		}
		private static Instructions.Instruction Ldloca_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldloca_S (cilInstruction);
		}
		private static Instructions.Instruction Stloc_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stloc_S (cilInstruction);
		}
		private static Instructions.Instruction Ldnull (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldnull (cilInstruction);
		}
		private static Instructions.Instruction Ldc_I4_M1 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldc_I4_M1 (cilInstruction);
		}
		private static Instructions.Instruction Ldc_I4_0 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldc_I4_0 (cilInstruction);
		}
		private static Instructions.Instruction Ldc_I4_1 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldc_I4_1 (cilInstruction);
		}
		private static Instructions.Instruction Ldc_I4_2 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldc_I4_2 (cilInstruction);
		}
		private static Instructions.Instruction Ldc_I4_3 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldc_I4_3 (cilInstruction);
		}
		private static Instructions.Instruction Ldc_I4_4 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldc_I4_4 (cilInstruction);
		}
		private static Instructions.Instruction Ldc_I4_5 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldc_I4_5 (cilInstruction);
		}
		private static Instructions.Instruction Ldc_I4_6 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldc_I4_6 (cilInstruction);
		}
		private static Instructions.Instruction Ldc_I4_7 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldc_I4_7 (cilInstruction);
		}
		private static Instructions.Instruction Ldc_I4_8 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldc_I4_8 (cilInstruction);
		}
		private static Instructions.Instruction Ldc_I4_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldc_I4_S (cilInstruction);
		}
		private static Instructions.Instruction Ldc_I4 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldc_I4 (cilInstruction);
		}
		private static Instructions.Instruction Ldc_I8 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldc_I8 (cilInstruction);
		}
		private static Instructions.Instruction Ldc_R4 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldc_R4 (cilInstruction);
		}
		private static Instructions.Instruction Ldc_R8 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldc_R8 (cilInstruction);
		}
		private static Instructions.Instruction Dup (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Dup (cilInstruction);
		}
		private static Instructions.Instruction Pop (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Pop (cilInstruction);
		}
		private static Instructions.Instruction Jmp (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Jmp (cilInstruction);
		}
		private static Instructions.Instruction Call (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Call (cilInstruction);
		}
		private static Instructions.Instruction Calli (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Calli (cilInstruction);
		}
		private static Instructions.Instruction Ret (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ret (cilInstruction);
		}
		private static Instructions.Instruction Br_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Br_S (cilInstruction);
		}
		private static Instructions.Instruction Brfalse_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Brfalse_S (cilInstruction);
		}
		private static Instructions.Instruction Brtrue_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Brtrue_S (cilInstruction);
		}
		private static Instructions.Instruction Beq_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Beq_S (cilInstruction);
		}
		private static Instructions.Instruction Bge_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Bge_S (cilInstruction);
		}
		private static Instructions.Instruction Bgt_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Bgt_S (cilInstruction);
		}
		private static Instructions.Instruction Ble_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ble_S (cilInstruction);
		}
		private static Instructions.Instruction Blt_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Blt_S (cilInstruction);
		}
		private static Instructions.Instruction Bne_Un_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Bne_Un_S (cilInstruction);
		}
		private static Instructions.Instruction Bge_Un_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Bge_Un_S (cilInstruction);
		}
		private static Instructions.Instruction Bgt_Un_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Bgt_Un_S (cilInstruction);
		}
		private static Instructions.Instruction Ble_Un_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ble_Un_S (cilInstruction);
		}
		private static Instructions.Instruction Blt_Un_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Blt_Un_S (cilInstruction);
		}
		private static Instructions.Instruction Br (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Br (cilInstruction);
		}
		private static Instructions.Instruction Brfalse (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Brfalse (cilInstruction);
		}
		private static Instructions.Instruction Brtrue (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Brtrue (cilInstruction);
		}
		private static Instructions.Instruction Beq (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Beq (cilInstruction);
		}
		private static Instructions.Instruction Bge (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Bge (cilInstruction);
		}
		private static Instructions.Instruction Bgt (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Bgt (cilInstruction);
		}
		private static Instructions.Instruction Ble (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ble (cilInstruction);
		}
		private static Instructions.Instruction Blt (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Blt (cilInstruction);
		}
		private static Instructions.Instruction Bne_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Bne_Un (cilInstruction);
		}
		private static Instructions.Instruction Bge_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Bge_Un (cilInstruction);
		}
		private static Instructions.Instruction Bgt_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Bgt_Un (cilInstruction);
		}
		private static Instructions.Instruction Ble_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ble_Un (cilInstruction);
		}
		private static Instructions.Instruction Blt_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Blt_Un (cilInstruction);
		}
		private static Instructions.Instruction Switch (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Switch (cilInstruction);
		}
		private static Instructions.Instruction Ldind_I1 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldind_I1 (cilInstruction);
		}
		private static Instructions.Instruction Ldind_U1 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldind_U1 (cilInstruction);
		}
		private static Instructions.Instruction Ldind_I2 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldind_I2 (cilInstruction);
		}
		private static Instructions.Instruction Ldind_U2 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldind_U2 (cilInstruction);
		}
		private static Instructions.Instruction Ldind_I4 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldind_I4 (cilInstruction);
		}
		private static Instructions.Instruction Ldind_U4 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldind_U4 (cilInstruction);
		}
		private static Instructions.Instruction Ldind_I8 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldind_I8 (cilInstruction);
		}
		private static Instructions.Instruction Ldind_I (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldind_I (cilInstruction);
		}
		private static Instructions.Instruction Ldind_R4 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldind_R4 (cilInstruction);
		}
		private static Instructions.Instruction Ldind_R8 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldind_R8 (cilInstruction);
		}
		private static Instructions.Instruction Ldind_Ref (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldind_Ref (cilInstruction);
		}
		private static Instructions.Instruction Stind_Ref (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stind_Ref (cilInstruction);
		}
		private static Instructions.Instruction Stind_I1 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stind_I1 (cilInstruction);
		}
		private static Instructions.Instruction Stind_I2 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stind_I2 (cilInstruction);
		}
		private static Instructions.Instruction Stind_I4 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stind_I4 (cilInstruction);
		}
		private static Instructions.Instruction Stind_I8 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stind_I8 (cilInstruction);
		}
		private static Instructions.Instruction Stind_R4 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stind_R4 (cilInstruction);
		}
		private static Instructions.Instruction Stind_R8 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stind_R8 (cilInstruction);
		}
		private static Instructions.Instruction Add (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Add (cilInstruction);
		}
		private static Instructions.Instruction Sub (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Sub (cilInstruction);
		}
		private static Instructions.Instruction Mul (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Mul (cilInstruction);
		}
		private static Instructions.Instruction Div (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Div (cilInstruction);
		}
		private static Instructions.Instruction Div_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Div_Un (cilInstruction);
		}
		private static Instructions.Instruction Rem (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Rem (cilInstruction);
		}
		private static Instructions.Instruction Rem_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Rem_Un (cilInstruction);
		}
		private static Instructions.Instruction And (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.And (cilInstruction);
		}
		private static Instructions.Instruction Or (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Or (cilInstruction);
		}
		private static Instructions.Instruction Xor (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Xor (cilInstruction);
		}
		private static Instructions.Instruction Shl (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Shl (cilInstruction);
		}
		private static Instructions.Instruction Shr (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Shr (cilInstruction);
		}
		private static Instructions.Instruction Shr_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Shr_Un (cilInstruction);
		}
		private static Instructions.Instruction Neg (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Neg (cilInstruction);
		}
		private static Instructions.Instruction Not (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Not (cilInstruction);
		}
		private static Instructions.Instruction Conv_I1 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_I1 (cilInstruction);
		}
		private static Instructions.Instruction Conv_I2 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_I2 (cilInstruction);
		}
		private static Instructions.Instruction Conv_I4 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_I4 (cilInstruction);
		}
		private static Instructions.Instruction Conv_I8 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_I8 (cilInstruction);
		}
		private static Instructions.Instruction Conv_R4 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_R4 (cilInstruction);
		}
		private static Instructions.Instruction Conv_R8 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_R8 (cilInstruction);
		}
		private static Instructions.Instruction Conv_U4 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_U4 (cilInstruction);
		}
		private static Instructions.Instruction Conv_U8 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_U8 (cilInstruction);
		}
		private static Instructions.Instruction Callvirt (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Callvirt (cilInstruction);
		}
		private static Instructions.Instruction Cpobj (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Cpobj (cilInstruction);
		}
		private static Instructions.Instruction Ldobj (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldobj (cilInstruction);
		}
		private static Instructions.Instruction Ldstr (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldstr (cilInstruction);
		}
		private static Instructions.Instruction Newobj (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Newobj (cilInstruction);
		}
		private static Instructions.Instruction Castclass (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Castclass (cilInstruction);
		}
		private static Instructions.Instruction Isinst (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Isinst (cilInstruction);
		}
		private static Instructions.Instruction Conv_R_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_R_Un (cilInstruction);
		}
		private static Instructions.Instruction Unbox (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Unbox (cilInstruction);
		}
		private static Instructions.Instruction Throw (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Throw (cilInstruction);
		}
		private static Instructions.Instruction Ldfld (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldfld (cilInstruction);
		}
		private static Instructions.Instruction Ldflda (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldflda (cilInstruction);
		}
		private static Instructions.Instruction Stfld (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stfld (cilInstruction);
		}
		private static Instructions.Instruction Ldsfld (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldsfld (cilInstruction);
		}
		private static Instructions.Instruction Ldsflda (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldsflda (cilInstruction);
		}
		private static Instructions.Instruction Stsfld (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stsfld (cilInstruction);
		}
		private static Instructions.Instruction Stobj (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stobj (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_I1_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_I1_Un (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_I2_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_I2_Un (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_I4_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_I4_Un (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_I8_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_I8_Un (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_U1_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_U1_Un (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_U2_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_U2_Un (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_U4_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_U4_Un (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_U8_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_U8_Un (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_I_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_I_Un (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_U_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_U_Un (cilInstruction);
		}
		private static Instructions.Instruction Box (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Box (cilInstruction);
		}
		private static Instructions.Instruction Newarr (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Newarr (cilInstruction);
		}
		private static Instructions.Instruction Ldlen (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldlen (cilInstruction);
		}
		private static Instructions.Instruction Ldelema (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldelema (cilInstruction);
		}
		private static Instructions.Instruction Ldelem_I1 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldelem_I1 (cilInstruction);
		}
		private static Instructions.Instruction Ldelem_U1 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldelem_U1 (cilInstruction);
		}
		private static Instructions.Instruction Ldelem_I2 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldelem_I2 (cilInstruction);
		}
		private static Instructions.Instruction Ldelem_U2 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldelem_U2 (cilInstruction);
		}
		private static Instructions.Instruction Ldelem_I4 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldelem_I4 (cilInstruction);
		}
		private static Instructions.Instruction Ldelem_U4 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldelem_U4 (cilInstruction);
		}
		private static Instructions.Instruction Ldelem_I8 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldelem_I8 (cilInstruction);
		}
		private static Instructions.Instruction Ldelem_I (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldelem_I (cilInstruction);
		}
		private static Instructions.Instruction Ldelem_R4 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldelem_R4 (cilInstruction);
		}
		private static Instructions.Instruction Ldelem_R8 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldelem_R8 (cilInstruction);
		}
		private static Instructions.Instruction Ldelem_Ref (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldelem_Ref (cilInstruction);
		}
		private static Instructions.Instruction Stelem_I (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stelem_I (cilInstruction);
		}
		private static Instructions.Instruction Stelem_I1 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stelem_I1 (cilInstruction);
		}
		private static Instructions.Instruction Stelem_I2 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stelem_I2 (cilInstruction);
		}
		private static Instructions.Instruction Stelem_I4 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stelem_I4 (cilInstruction);
		}
		private static Instructions.Instruction Stelem_I8 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stelem_I8 (cilInstruction);
		}
		private static Instructions.Instruction Stelem_R4 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stelem_R4 (cilInstruction);
		}
		private static Instructions.Instruction Stelem_R8 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stelem_R8 (cilInstruction);
		}
		private static Instructions.Instruction Stelem_Ref (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stelem_Ref (cilInstruction);
		}
		private static Instructions.Instruction Ldelem_Any (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldelem_Any (cilInstruction);
		}
		private static Instructions.Instruction Stelem_Any (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stelem_Any (cilInstruction);
		}
		private static Instructions.Instruction Unbox_Any (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Unbox_Any (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_I1 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_I1 (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_U1 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_U1 (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_I2 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_I2 (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_U2 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_U2 (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_I4 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_I4 (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_U4 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_U4 (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_I8 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_I8 (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_U8 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_U8 (cilInstruction);
		}
		private static Instructions.Instruction Refanyval (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Refanyval (cilInstruction);
		}
		private static Instructions.Instruction Ckfinite (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ckfinite (cilInstruction);
		}
		private static Instructions.Instruction Mkrefany (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Mkrefany (cilInstruction);
		}
		private static Instructions.Instruction Ldtoken (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldtoken (cilInstruction);
		}
		private static Instructions.Instruction Conv_U2 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_U2 (cilInstruction);
		}
		private static Instructions.Instruction Conv_U1 (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_U1 (cilInstruction);
		}
		private static Instructions.Instruction Conv_I (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_I (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_I (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_I (cilInstruction);
		}
		private static Instructions.Instruction Conv_Ovf_U (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_Ovf_U (cilInstruction);
		}
		private static Instructions.Instruction Add_Ovf (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Add_Ovf (cilInstruction);
		}
		private static Instructions.Instruction Add_Ovf_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Add_Ovf_Un (cilInstruction);
		}
		private static Instructions.Instruction Mul_Ovf (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Mul_Ovf (cilInstruction);
		}
		private static Instructions.Instruction Mul_Ovf_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Mul_Ovf_Un (cilInstruction);
		}
		private static Instructions.Instruction Sub_Ovf (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Sub_Ovf (cilInstruction);
		}
		private static Instructions.Instruction Sub_Ovf_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Sub_Ovf_Un (cilInstruction);
		}
		private static Instructions.Instruction Endfinally (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Endfinally (cilInstruction);
		}
		private static Instructions.Instruction Leave (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Leave (cilInstruction);
		}
		private static Instructions.Instruction Leave_S (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Leave_S (cilInstruction);
		}
		private static Instructions.Instruction Stind_I (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stind_I (cilInstruction);
		}
		private static Instructions.Instruction Conv_U (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Conv_U (cilInstruction);
		}
		private static Instructions.Instruction Arglist (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Arglist (cilInstruction);
		}
		private static Instructions.Instruction Ceq (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ceq (cilInstruction);
		}
		private static Instructions.Instruction Cgt (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Cgt (cilInstruction);
		}
		private static Instructions.Instruction Cgt_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Cgt_Un (cilInstruction);
		}
		private static Instructions.Instruction Clt (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Clt (cilInstruction);
		}
		private static Instructions.Instruction Clt_Un (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Clt_Un (cilInstruction);
		}
		private static Instructions.Instruction Ldftn (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldftn (cilInstruction);
		}
		private static Instructions.Instruction Ldvirtftn (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldvirtftn (cilInstruction);
		}
		private static Instructions.Instruction Ldarg (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldarg (cilInstruction);
		}
		private static Instructions.Instruction Ldarga (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldarga (cilInstruction);
		}
		private static Instructions.Instruction Starg (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Starg (cilInstruction);
		}
		private static Instructions.Instruction Ldloc (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldloc (cilInstruction);
		}
		private static Instructions.Instruction Ldloca (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Ldloca (cilInstruction);
		}
		private static Instructions.Instruction Stloc (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Stloc (cilInstruction);
		}
		private static Instructions.Instruction Localloc (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Localloc (cilInstruction);
		}
		private static Instructions.Instruction Endfilter (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Endfilter (cilInstruction);
		}
		private static Instructions.Instruction Unaligned (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Unaligned (cilInstruction);
		}
		private static Instructions.Instruction Volatile (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Volatile (cilInstruction);
		}
		private static Instructions.Instruction Tail (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Tail (cilInstruction);
		}
		private static Instructions.Instruction Initobj (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Initobj (cilInstruction);
		}
		private static Instructions.Instruction Constrained (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Constrained (cilInstruction);
		}
		private static Instructions.Instruction Cpblk (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Cpblk (cilInstruction);
		}
		private static Instructions.Instruction Initblk (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Initblk (cilInstruction);
		}
		private static Instructions.Instruction No (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.No (cilInstruction);
		}
		private static Instructions.Instruction Rethrow (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Rethrow (cilInstruction);
		}
		private static Instructions.Instruction Sizeof (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Sizeof (cilInstruction);
		}
		private static Instructions.Instruction Refanytype (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Refanytype (cilInstruction);
		}
		private static Instructions.Instruction Readonly (Block block, Mono.Cecil.Cil.Instruction cilInstruction)
		{
			return block.Readonly (cilInstruction);
		}
		#endregion
	}
}
