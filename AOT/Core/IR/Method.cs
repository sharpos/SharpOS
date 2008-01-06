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

#define NEW_BLOCK_HANDLING

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using SharpOS.AOT.IR;
using SharpOS.AOT.IR.Operands;
using SharpOS.AOT.IR.Instructions;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;


namespace SharpOS.AOT.IR {
	/// <summary>
	/// Represents a method in the AOT's intermediate representation. 
	/// </summary>
	public class Method : IEnumerable<Block> {
		private bool skipProcessing = false;

		public bool SkipProcessing
		{
			get
			{
				return this.skipProcessing;
			}
			set
			{
				this.skipProcessing = value;
			}
		}

		private bool entryPoint = false;

		/// <summary>
		/// Gets or sets a value indicating whether this method should be called when booting.
		/// </summary>
		/// <value><c>true</c> if [entry point]; otherwise, <c>false</c>.</value>
		public bool EntryPoint
		{
			get
			{
				return this.entryPoint;
			}
			set
			{
				this.entryPoint = value;
			}
		}

		private int stackSize = 0;

		/// <summary>
		/// Gets or sets the size of the stack.
		/// </summary>
		/// <value>The size of the stack.</value>
		public int StackSize
		{
			get
			{
				return stackSize;
			}
			set
			{
				stackSize = value;
			}
		}

		private Engine engine = null;

		/// <summary>
		/// Gets the engine.
		/// </summary>
		/// <value>The engine.</value>
		public Engine Engine
		{
			get
			{
				return this.engine;
			}
		}

		private MethodDefinition methodDefinition = null;

		/// <summary>
		/// Gets the method definition.
		/// </summary>
		/// <value>The method definition.</value>
		public MethodDefinition MethodDefinition
		{
			get
			{
				return this.methodDefinition;
			}
		}

		public string Name
		{
			get
			{
				return this.methodDefinition.Name;
			}
		}

		public TypeReference DeclaringType
		{
			get
			{
				return this.methodDefinition.DeclaringType;
			}
		}

		public MethodReturnType ReturnType
		{
			get
			{
				return this.methodDefinition.ReturnType;
			}
		}

		public ParameterDefinitionCollection Parameters
		{
			get
			{
				return this.methodDefinition.Parameters;
			}
		}

		public bool HasThis
		{
			get
			{
				return this.methodDefinition.HasThis;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Method"/> class.
		/// </summary>
		/// <param name="engine">The engine.</param>
		/// <param name="methodDefinition">The method definition.</param>
		public Method (Engine engine, MethodDefinition methodDefinition)
		{
			this.engine = engine;
			this.methodDefinition = methodDefinition;
		}

		/// <summary>
		/// Dumps a representation of the blocks that comprise this method
		/// </summary>
		/// <returns></returns>
		public void DumpBlocks ()
		{
			DumpBlocks (blocks, this.engine.Dump);
		}

		/// <summary>
		/// Dumps the def use.
		/// </summary>
		public void DumpDefUse ()
		{
			//			this.defuse.Dump (this.engine.Dump);
		}

		List<Argument> arguments = new List<Argument> ();

		/// <summary>
		/// Gets an Argument object that represents the numbered method 
		/// argument named by <paramref name="i"/>. 
		/// </summary>
		/// <param name="i">The i.</param>
		/// <returns></returns>
		public Argument GetArgument (int i, bool sequence)
		{
			if (this.methodDefinition.HasThis && sequence)
				i++;

			if (i < 0)
				throw new EngineException (string.Format ("Argument Index may not be negative. ({0})", this.MethodFullName));

			if (i >= this.arguments.Count)
				throw new EngineException (string.Format ("Argument Index out of range. ({0})", this.MethodFullName));

			return this.arguments [i];
		}

		List<Local> locals = new List<Local> ();

		/// <summary>
		/// Gets a local variable with the index given by <paramref name="i" />.
		/// </summary>
		/// <param name="i">The index of the local variable.</param>
		/// <returns></returns>
		public Local GetLocal (int i)
		{
			if (i < 0)
				throw new EngineException (string.Format ("Local Index may not be negative. ({0})", this.MethodFullName));

			if (i >= this.locals.Count)
				throw new EngineException (string.Format ("Local Index out of range. ({0})", this.MethodFullName));

			return this.locals [i];
		}

		/// <summary>
		/// Determines whether the specified instruction is a branch.
		/// </summary>
		/// <param name="instruction">The instruction to check.</param>
		/// <param name="all">if set to <c>true</c> [all]. FIXME</param>
		/// <returns>
		/// 	<c>true</c> if the specified instruction is branch; otherwise, <c>false</c>.
		/// </returns>
		public bool IsBranch (Mono.Cecil.Cil.Instruction instruction)
		{
			FlowControl flowControl = instruction.OpCode.FlowControl;

			if (flowControl == FlowControl.Break
					|| flowControl == FlowControl.Branch
					|| flowControl == FlowControl.Return
					|| flowControl == FlowControl.Cond_Branch
					|| flowControl == FlowControl.Throw)
				return true;

			return false;
		}

		private void AddInstructionOffset (List<int> offsets, Mono.Cecil.Cil.Instruction instruction)
		{
			if (instruction != null && !offsets.Contains (instruction.Offset))
				offsets.Add (instruction.Offset);
		}

		private void LinkBlocks (Block current, Mono.Cecil.Cil.Instruction instruction)
		{
			if (instruction == null)
				return;

			int offset = instruction.Offset;

			Block link = GetBlockByOffset (offset);

			if (!current.Outs.Contains (link))
				current.Outs.Add (link);

			if (!link.Ins.Contains (current))
				link.Ins.Add (current);
		}

		Dictionary<int, Block> offsetToBlock = new Dictionary<int, Block> ();

		public Block GetBlockByOffset (int i)
		{
			if (!offsetToBlock.ContainsKey (i))
				throw new EngineException (string.Format ("No block found starting at {1} in '{0}'.", this.MethodFullName, i));

			return this.offsetToBlock [i];
		}

		/// <summary>
		/// Builds the blocks.
		/// </summary>
		private void BuildBlocks ()
		{
			this.blocks = new List<Block> ();

			InstructionCollection instructions = this.methodDefinition.Body.Instructions;
			List<int> offsets = new List<int> ();

			// Add all the offsets of the instructions that start a block
			if (instructions.Count > 0)
				offsets.Add (instructions [0].Offset);

			foreach (Mono.Cecil.Cil.Instruction instruction in instructions) {
				if (IsBranch (instruction)) {
					// this is for the conditional jump
					this.AddInstructionOffset (offsets, instruction.Operand as Mono.Cecil.Cil.Instruction);

					// This is for a 'switch', which can have lots of targets
					Mono.Cecil.Cil.Instruction [] targets = instruction.Operand as Mono.Cecil.Cil.Instruction [];

					if (targets != null) {
						foreach (Mono.Cecil.Cil.Instruction value in targets)
							this.AddInstructionOffset (offsets, value);
					}

					this.AddInstructionOffset (offsets, instruction.Next);
				}
			}

			offsets.Sort ();

			// Build the blocks
			int index = 0;
			Block current = null;
			foreach (Mono.Cecil.Cil.Instruction instruction in instructions) {
				if (index < offsets.Count && instruction.Offset == offsets [index]) {
					current = new Block (this);
					this.blocks.Add (current);

					offsetToBlock [instruction.Offset] = current;

					index++;
				}

				current.CIL.Add (instruction);
			}

			// Link the blocks
			foreach (Block block in this.blocks) {
				Mono.Cecil.Cil.Instruction instruction = block.CIL [block.CIL.Count - 1];

				// this is for the conditional jump
				LinkBlocks (block, instruction.Operand as Mono.Cecil.Cil.Instruction);

				if (instruction.OpCode.FlowControl != FlowControl.Branch)
					LinkBlocks (block, instruction.Next);

				// This is for a 'switch', which can have lots of targets
				Mono.Cecil.Cil.Instruction [] targets = instruction.Operand as Mono.Cecil.Cil.Instruction [];

				if (targets != null) {
					foreach (Mono.Cecil.Cil.Instruction value in targets)
						LinkBlocks (block, value);
				}
			}
		}

		/// <summary>
		/// Merges two blocks that are next to each other and are linked only to each other.
		/// </summary>
		public void BlocksOptimization ()
		{
			bool changed;

			do {
				changed = false;

				for (int i = 1; i < this.blocks.Count; i++) {
					if (this.blocks [i].Ins.Count == 1
							&& this.blocks [i - 1].Outs.Count == 1
							&& this.blocks [i].Ins [0] == this.blocks [i - 1]
							&& this.blocks [i - 1].Outs [0] == this.blocks [i]) {
						this.blocks [i - 1].Merge (this.blocks [i]);

						this.blocks.Remove (this.blocks [i]);

						changed = true;
						break;
					}
				}

			} while (changed);

			for (int i = 0; i < this.blocks.Count; i++)
				this.blocks [i].Index = i;

			return;
		}

		List<int> registerVersions = new List<int> ();

		/// <summary>
		/// Holds the register versions used mainly by the SSA.
		/// </summary>
		/// <value>The register versions.</value>
		public List<int> RegisterVersions
		{
			get
			{
				return registerVersions;
			}
		}

		/// <summary>
		/// Converts from CIL to IR.
		/// </summary>
		private void ConvertFromCIL ()
		{
			this.registerVersions = new List<int> ();

			for (int i = 0; i < this.methodDefinition.Body.MaxStack; i++)
				this.registerVersions.Add (0);

			foreach (Block block in this.Preorder ())
				block.ConvertFromCIL ();

			// Insert Initialize instructions to initialize the local variables
			if (blocks.Count > 0
					&& this.methodDefinition.Body.Variables.Count > 0) {
				for (int i = 0; i < this.methodDefinition.Body.Variables.Count; i++) {
					VariableDefinition variableDefinition = this.methodDefinition.Body.Variables [this.methodDefinition.Body.Variables.Count - i - 1];

					Instructions.Instruction instruction = new Initialize (this.GetLocal (this.methodDefinition.Body.Variables.Count - i - 1), variableDefinition.VariableType);

					blocks [0].InsertInstruction (0, instruction);
				}
			}

			foreach (Block block in this.blocks) {
				// If the current block is connected to more than one block
				// and they have not the same number of entries on the stack 
				// then throw an exception.
				if (block.Ins.Count > 1) {
					int stackSize = block.Ins [0].Stack.Count;

					for (int i = 1; i < block.Ins.Count; i++)
						if (block.Ins [i].Stack.Count != stackSize)
							throw new EngineException (string.Format ("The branches of the block #{0} have not the same number of stack entries. ({1})", block.Index, this.MethodFullName));
				}

				block.EndCILConversion ();
			}

			return;
		}

		/// <summary>
		/// Preorders this instance.
		/// </summary>
		/// <returns></returns>
		private List<Block> Preorder ()
		{
			List<Block> list = new List<Block> ();
			List<Block> visited = new List<Block> ();

			Preorder (visited, list, this.blocks [0]);

			return list;
		}

		/// <summary>
		/// Preorders the specified visited.
		/// </summary>
		/// <param name="visited">The visited.</param>
		/// <param name="list">The list.</param>
		/// <param name="current">The current.</param>
		private void Preorder (List<Block> visited, List<Block> list, Block current)
		{
			if (!visited.Contains (current)) {
				visited.Add (current);

				list.Add (current);

				for (int i = 0; i < current.Outs.Count; i++)
					Preorder (visited, list, current.Outs [i]);
			}

			return;
		}

		/// <summary>
		/// Postorders this instance.
		/// </summary>
		/// <returns></returns>
		private List<Block> Postorder ()
		{
			List<Block> list = new List<Block> ();
			List<Block> visited = new List<Block> ();

			Postorder (visited, list, this.blocks [0]);

			return list;
		}

		/// <summary>
		/// Postorders the specified visited.
		/// </summary>
		/// <param name="visited">The visited.</param>
		/// <param name="list">The list.</param>
		/// <param name="current">The current.</param>
		private void Postorder (List<Block> visited, List<Block> list, Block current)
		{
			if (!visited.Contains (current)) {
				visited.Add (current);

				for (int i = 0; i < current.Outs.Count; i++)
					Postorder (visited, list, current.Outs [i]);

				list.Add (current);
			}

			return;
		}

		/// <summary>
		/// Reverses the postorder.
		/// </summary>
		/// <returns></returns>
		private List<Block> ReversePostorder ()
		{
			List<Block> list = new List<Block> ();
			List<Block> visited = new List<Block> ();
			List<Block> active = new List<Block> ();

			visited.Add (this.blocks [0]);
			list.Add (this.blocks [0]);

			ReversePostorder (visited, active, list, this.blocks [0]);

			return list;
		}

		/// <summary>
		/// Reverses the postorder.
		/// </summary>
		/// <param name="visited">The visited.</param>
		/// <param name="active">The active.</param>
		/// <param name="list">The list.</param>
		/// <param name="current">The current.</param>
		private void ReversePostorder (List<Block> visited, List<Block> active, List<Block> list, Block current)
		{
			if (this.blocks.Count == list.Count)
				return;

			if (active.Contains (current))
				return;

			active.Add (current);

			for (int i = 0; i < current.Outs.Count; i++) {
				if (!visited.Contains (current.Outs [i])) {
					visited.Add (current.Outs [i]);
					list.Add (current.Outs [i]);
				}
			}

			for (int i = 0; i < current.Outs.Count; i++)
				ReversePostorder (visited, active, list, current.Outs [i]);

			active.Remove (current);

			return;
		}

		/// <summary>
		/// Computes the block dominators.
		/// </summary>
		private void Dominators ()
		{
			for (int i = 0; i < this.blocks.Count; i++) {
				foreach (Block block in blocks) {
					this.blocks [i].Dominators.Add (block);
				}
			}

			List<Block> list = Preorder ();

			bool changed = true;

			while (changed) {
				changed = false;

				for (int i = 0; i < list.Count; i++) {
					List<Block> predecessorDoms = new List<Block> ();
					List<Block> doms = new List<Block> ();

					Block block = list [list.Count - 1 - i];

					// Add the dominator blocks of the predecessors
					foreach (Block predecessor in block.Ins) {
						foreach (Block dom in predecessor.Dominators) {
							if (!predecessorDoms.Contains (dom))
								predecessorDoms.Add (dom);
						}
					}

					// For each block in the predecessors' dominators build the intersection
					foreach (Block predecessorDom in predecessorDoms) {
						bool include = true;

						foreach (Block predecessor in block.Ins) {
							if (!predecessor.Dominators.Contains (predecessorDom)) {
								include = false;
								break;
							}
						}

						if (include)
							doms.Add (predecessorDom);
					}

					// Add the block itself to the dominators
					doms.Add (block);

					// Set the new dominators if there are any differences
					if (block.Dominators.Count != doms.Count) {
						block.Dominators = doms;
						changed = true;

					} else {
						foreach (Block dom in doms) {
							if (!block.Dominators.Contains (dom)) {
								block.Dominators = doms;
								changed = true;
								break;
							}
						}
					}
				}
			}

			// Compute the Immediate Dominator of each Block
			foreach (Block block in blocks) {
				foreach (Block immediateDominator in block.Dominators) {
					// An Immediate Dominator can't be the block itself
					if (immediateDominator == block)
						continue;

					bool found = false;

					foreach (Block dominator in block.Dominators) {
						if (dominator == immediateDominator || dominator == block)
							continue;

						// An Immediate Dominator can't dominate another Dominator only the block itself
						if (dominator.Dominators.Contains (immediateDominator)) {
							found = true;
							break;
						}
					}

					// We found the Immediate Dominator that does not dominate any other dominator but the block itself
					if (!found) {
						block.ImmediateDominator = immediateDominator;
						break;
					}
				}
			}

			// Build the Dominator Tree. The Parent of a Node is the Immediate Dominator of that block.
			foreach (Block parent in blocks) {
				foreach (Block possibleChild in blocks) {
					if (parent == possibleChild.ImmediateDominator)
						parent.ImmediateDominatorOf.Add (possibleChild);
				}
			}

			if (engine.Options.Dump)
				engine.Dump.Dominance (this.blocks);

			// Compute the Dominance Frontier
			foreach (Block block in blocks) {
				if (block.Ins.Count == 0)
					continue;

				foreach (Block predecessor in block.Ins) {
					Block runner = predecessor;

					while (runner != block.ImmediateDominator) {
						if (!runner.DominanceFrontiers.Contains (block))
							runner.DominanceFrontiers.Add (block);

						runner = runner.ImmediateDominator;
					}
				}
			}

			if (engine.Options.Dump)
				engine.Dump.Dominance (this.blocks);

			return;
		}

		private void InternalPropagationLogic (Instructions.Instruction instruction)
		{
			if (instruction.Use == null)
				return;

			for (int i = 0; i < instruction.Use.Length; i++) {
				Operand operand = instruction.Use [i];

				Register register = operand as Register;

				if (register != null) {
					if (register.Parent == null)
						throw new EngineException (string.Format ("Instruction '{0}' can't be processed by the Internal Propagation. ({1})", instruction, this.MethodFullName));

					register.Parent.Ignore = true;

					InternalPropagationLogic (register.Parent);
				}
			}
		}

		/// <summary>
		/// It is a Constant and Copy Propagation for SharpOS custom attributes. (e.g. SharpOS.AOT.Attribues.ADCLayerAttribute)
		/// and a Constant and Copy Propagation for Assembly calls. (e.g. Asm.MOV...)
		/// </summary>
		private void InternalPropagation ()
		{
			List<Instructions.Instruction> remove = new List<SharpOS.AOT.IR.Instructions.Instruction> ();

			foreach (Block block in this.blocks) {
				foreach (Instructions.Instruction instruction in block) {
					if (instruction is Instructions.Call) {
						Instructions.Call call = (instruction as Instructions.Call);

						if (!engine.HasSharpOSAttribute (call)
								&& !engine.Assembly.IsInstruction (call.Method.MethodDefinition.DeclaringType.FullName))
							continue;

						instruction.IsSpecialCase = true;

						this.InternalPropagationLogic (instruction);
					}
				}
			}

			return;
		}


		/// <summary>
		/// Copy Propagation
		/// Constant Propagation
		/// Constant Folding
		/// Dead Code Elimination
		/// </summary>
		private void Optimizations ()
		{
			/*			List<string> keys = this.defuse.GetKeys ();

						this.engine.Dump.Section (DumpSection.Optimizations);

						keys.Sort ();

						while (keys.Count > 0) {
							string key = keys [0];
							keys.RemoveAt (0);

							DefUseItem item = this.defuse [key];

							Instructions.Instruction definition = item.Definition;

							// If the PHI values are all the same then PHI is replaced with the first value. 
							// (e.g. v2 = PHI(v1, v1) -> v2 = v1)
							if (definition is PHI) {
								Operand sample = definition.Value.Operands [0];

								bool equal = true;

								for (int i = 1; i < definition.Value.Operands.Length; i++) {
									if (!sample.ID.Equals (definition.Value.Operands [i].ID)) {
										equal = false;
										break;
									}
								}

								if (!equal)
									continue;

								Assign assign = new Assign ((definition as Assign).Assignee, sample);

								// Replace the PHI with a normal assignment
								definition.Block.RemoveInstruction (definition);
								definition.Block.InsertInstruction (0, assign);

								this.defuse.SetDefinition (key, assign, true);
							}

							/// It looks for instructions like 'a = 100; b = a;' and replaces them with 'b = 100;'
							else if (definition is Assign
									&& (definition as Assign).Value is Constant
									&& ((definition as Assign).Value as Constant).Value.GetType () != typeof (string)
									&& ((definition as Assign).Assignee is Local
										|| (definition as Assign).Assignee is Register)) {
								bool remove = true;

								// "X = A" becomes "X = 100"
								foreach (Instructions.Instruction used in item) {
									List<Operand> usage = new List<Operand> ();
									Operand definitionOperand = used.GetDefinitionAndUsage (usage);

									if (this.engine.Options.Dump) {
										this.engine.Dump.Item ();
										this.engine.Dump.Element (definition);
										this.engine.Dump.PushElement ("before", true, false, false);
										this.engine.Dump.Element (used);
										this.engine.Dump.PopElement ();
									}

									Operand.OperandReplaceVisitor visitor = delegate (object parent, Operand old) {
										if (parent is Indirect
												|| parent is Address)
											return false;

										return true;
									};

									int replacements = used.ReplaceOperand (key, definition.Value, visitor);

									if (replacements == 0)
										remove = false;

									if (this.engine.Options.Dump) {
										this.engine.Dump.PushElement ("after", true, false, false);
										this.engine.Dump.Element (used);
										this.engine.Dump.PopElement ();
										this.engine.Dump.PopElement ();
									}

									// Add X to the queue as "X = 100;"
									if (definitionOperand != null) {
										string id = definitionOperand.ID;
							
										if (!keys.Contains (id))
											keys.Add (id);
									}
								}

								if (remove) { 
									// Remove the instruction from the block that it is containing it
									definition.Block.RemoveInstruction (definition);

									// Remove the variable from the defuse list
									defuse.Remove (key);
								}
							}

							// Copy Propagation
							// It looks for instructions like 'a = b; c = a;' and replaces them with 'c = b;'
							else if (!item.SkipCopyPropagation (this.engine)) {
								Assign assign = definition as Assign;

								// A = B
								this.engine.Dump.Item();
								this.engine.Dump.Element(definition);

								//bool _break = false;
								bool remove = true;

								// "X = A" becomes "X = B"
								foreach (Instructions.Instruction used in item) {
									List<Operand> usage = new List<Operand> ();
									Operand definitionOperand = used.GetDefinitionAndUsage (usage);

									if (this.engine.Options.Dump) {
										this.engine.Dump.PushElement ("before", true, false, false);
										this.engine.Dump.Element (used);
										this.engine.Dump.PopElement ();
									}

									int replacements = used.ReplaceOperand (key, definition.Value as Identifier, null);
							
									if (replacements == 0)
										remove = false;

									if (definitionOperand != null) {
										string id = definitionOperand.ID;

										if (!keys.Contains (id))
											keys.Add (id);
									}

									if (this.engine.Options.Dump) {
										this.engine.Dump.PushElement ("after", true, false, false);
										this.engine.Dump.Element (used);
										this.engine.Dump.PopElement ();
									}

								}

								if (remove) {
									// If A = B and B is still in the queue we remove A = B from the B usage list
									// and add all the "usage" items from A to the B "usage" list.
									if (this.defuse.GetKeys ().Contains (assign.Value.ID) == true) {
										this.defuse.RemoveUsage (assign.Value.ID, assign);

										foreach (Instructions.Instruction usage in this.defuse [key])
											this.defuse.AddUsage (assign.Value.ID, usage);
									}

									// Remove the instruction from the block that it is containing it
									definition.Block.RemoveInstruction (definition);

									// Remove the variable from the defuse list
									this.defuse.Remove (key);
								}
					
								this.engine.Dump.PopElement();	// item
							} 
				
							// Constant Folding
							else if (definition is Assign
								   && (definition as Assign).Value is Operands.Arithmetic) {

								bool changed = false;

								Assign assign = definition as Assign;

								if (!(assign.Value is Operands.Arithmetic))
									continue;

								Arithmetic arithmetic = assign.Value as Operands.Arithmetic;

								if (!(arithmetic.Operator is Binary))
									continue;

								if (!(arithmetic.Operands [0] is Constant
									&& arithmetic.Operands [1] is Constant))
									continue;

								Binary binary = arithmetic.Operator as Binary;
								Constant constant1 = arithmetic.Operands [0] as Constant;
								Constant constant2 = arithmetic.Operands [1] as Constant;

								if (this.engine.Options.Dump) {
									this.engine.Dump.Item ();
									this.engine.Dump.PushElement ("before", true, false, false);
									this.engine.Dump.Element (assign);
									this.engine.Dump.PopElement ();
								}

								// TODO implement all the other operators
								if (binary.Type == Operator.BinaryType.Mul) {
									// TODO implement the other combinations
									if (constant1.SizeType == InternalType.I4
											&& constant2.SizeType == InternalType.I4) {

										changed = true;

										int value = System.Convert.ToInt32 (constant1.Value) * System.Convert.ToInt32 (constant2.Value);

										definition.Value = new Constant (value);
										definition.Value.SizeType = InternalType.I4;
									}
								} else if (binary.Type == Operator.BinaryType.Sub) {
									// TODO implement the other combinations
									if (constant1.SizeType == InternalType.I4
											&& constant2.SizeType == InternalType.I4) {

										changed = true;

										int value = System.Convert.ToInt32 (constant1.Value) - System.Convert.ToInt32 (constant2.Value);

										definition.Value = new Constant (value);
										definition.Value.SizeType = InternalType.I4;
									}
								}

								if (this.engine.Options.Dump) {
									this.engine.Dump.PushElement ("after", true, false, false);
									this.engine.Dump.Element (assign);
									this.engine.Dump.PopElement ();
									this.engine.Dump.PopElement ();
								}

								if (changed)
									keys.Add (key);
							}

							// Dead Code Elimination
							else if (item.Count == 0) {
								// Remove the instruction from the block that it is containing it
								definition.Block.RemoveInstruction (definition);

								// Remove the variable from the defuse list
								defuse.Remove (key);

								List<Operand> usage = new List<Operand> ();
								definition.GetDefinitionAndUsage (usage);

								// B & C used in "A = B + C"
								foreach (Operand _entry in usage) {
									string id = _entry.ID;

									// Remove "A = B + C" from B & C
									this.defuse.RemoveUsage (id, definition);

									// Add to the queue B & C to check them it they are used anywhere else
									if (!keys.Contains (id))
										keys.Add (id);
								}
							}
						}

						this.engine.Dump.PopElement ();	// section: const-propagation
						*/
			return;
		}

		/// <summary>
		/// Gets the full name of the method.
		/// </summary>
		/// <value>The full name of the method.</value>
		public string MethodFullName
		{
			get
			{
				return Method.GetLabel (this.methodDefinition);
			}
		}

		/// <summary>
		/// Gets the label.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		public static string GetLabel (Mono.Cecil.MethodReference method)
		{
			StringBuilder result = new StringBuilder ();

			string value = method.DeclaringType.FullName;

			foreach (CustomAttribute attribute in method.DeclaringType.CustomAttributes) {
				if (!attribute.Constructor.DeclaringType.FullName.Equals (typeof (SharpOS.AOT.Attributes.TargetNamespaceAttribute).ToString ()))
					continue;

				value = attribute.ConstructorParameters [0].ToString () + "." + method.DeclaringType.Name;
			}

			result.Append (value + "." + method.Name);

			result.Append ("(");

			for (int i = 0; i < method.Parameters.Count; i++) {
				if (i != 0)
					result.Append (",");

				result.Append (method.Parameters [i].ParameterType.FullName);
			}


			result.Append (")");

			return result.ToString ();
		}

		/// <summary>
		/// Transformation out of SSA
		/// </summary>
		private void TransformationOutOfSSA ()
		{
			foreach (Block block in this.blocks)
				block.TransformationOutOfSSA ();
		}

		public class LiveRange : IComparable {
			/// <summary>
			/// Initializes a new instance of the <see cref="LiveRange"/> class.
			/// </summary>
			/// <param name="id">The id.</param>
			/// <param name="start">The start.</param>
			public LiveRange (string id, Instructions.Instruction start)
			{
				this.id = id;
				this.start = start;
				this.end = start;
			}

			private string id = string.Empty;

			/// <summary>
			/// Gets or sets the ID.
			/// </summary>
			/// <value>The ID.</value>
			public string ID
			{
				get
				{
					return id;
				}
				set
				{
					id = value;
				}
			}

			private Instructions.Instruction start = null;

			/// <summary>
			/// Gets or sets the start.
			/// </summary>
			/// <value>The start.</value>
			public Instructions.Instruction Start
			{
				get
				{
					return start;
				}
				set
				{
					start = value;
				}
			}

			private Instructions.Instruction end = null;

			/// <summary>
			/// Gets or sets the end.
			/// </summary>
			/// <value>The end.</value>
			public Instructions.Instruction End
			{
				get
				{
					return end;
				}
				set
				{
					end = value;
				}
			}

			private Identifier identifier = null;

			/// <summary>
			/// Gets or sets the identifier.
			/// </summary>
			/// <value>The identifier.</value>
			public Identifier Identifier
			{
				get
				{
					return identifier;
				}
				set
				{
					identifier = value;
				}
			}

			/// <summary>
			/// Compares the current instance with another object of the same type.
			/// </summary>
			/// <param name="obj">An object to compare with this instance.</param>
			/// <returns>
			/// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than obj. Zero This instance is equal to obj. Greater than zero This instance is greater than obj.
			/// </returns>
			/// <exception cref="T:System.ArgumentException">obj is not the same type as this instance. </exception>
			int IComparable.CompareTo (object obj)
			{
				LiveRange liveRange = (LiveRange) obj;

				return this.id.CompareTo (liveRange.id);
			}

			/// <summary>
			/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
			/// </summary>
			/// <returns>
			/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
			/// </returns>
			public override string ToString ()
			{
				string register = string.Empty;

				if (this.Identifier.Register != int.MinValue)
					register = "R" + this.Identifier.Register;

				else if (this.Identifier.Stack != int.MinValue)
					register = "M" + this.Identifier.Stack;

				return this.identifier + " : " + register + " : " + this.Start.Index + " <-> " + this.End.Index;
			}

			public class SortByStart : IComparer<LiveRange> {
				/// <summary>
				/// Compares the specified live range1.
				/// </summary>
				/// <param name="liveRange1">The live range1.</param>
				/// <param name="liveRange2">The live range2.</param>
				/// <returns></returns>
				int IComparer<LiveRange>.Compare (LiveRange liveRange1, LiveRange liveRange2)
				{
					if (liveRange1.Start.Index > liveRange2.Start.Index)
						return 1;

					if (liveRange1.Start.Index < liveRange2.Start.Index)
						return -1;

					if (liveRange1.End.Index > liveRange2.End.Index)
						return 1;

					if (liveRange1.End.Index < liveRange2.End.Index)
						return -1;

					return 0;
				}
			}

			public class SortByEnd : IComparer<LiveRange> {
				/// <summary>
				/// Compares the specified live range1.
				/// </summary>
				/// <param name="liveRange1">The live range1.</param>
				/// <param name="liveRange2">The live range2.</param>
				/// <returns></returns>
				int IComparer<LiveRange>.Compare (LiveRange liveRange1, LiveRange liveRange2)
				{
					if (liveRange1.End.Index > liveRange2.End.Index)
						return 1;

					if (liveRange1.End.Index < liveRange2.End.Index)
						return -1;

					return 0;
				}
			}

			public class SortByRegisterStack : IComparer<LiveRange> {
				/// <summary>
				/// Compares the specified live range1.
				/// </summary>
				/// <param name="liveRange1">The live range1.</param>
				/// <param name="liveRange2">The live range2.</param>
				/// <returns></returns>
				int IComparer<LiveRange>.Compare (LiveRange liveRange1, LiveRange liveRange2)
				{
					if (liveRange1.Identifier.Register != int.MinValue
							&& liveRange2.Identifier.Stack != int.MinValue)
						return -1;

					if (liveRange1.Identifier.Stack != int.MinValue
							&& liveRange2.Identifier.Register != int.MinValue)
						return 1;

					if (liveRange1.Identifier.Register != int.MinValue
							&& liveRange2.Identifier.Register != int.MinValue) {
						if (liveRange1.Identifier.Register > liveRange2.Identifier.Register)
							return 1;

						if (liveRange1.Identifier.Register < liveRange2.Identifier.Register)
							return -1;

						return 0;
					}

					if (liveRange1.Identifier.Stack > liveRange2.Identifier.Stack)
						return 1;

					if (liveRange1.Identifier.Stack < liveRange2.Identifier.Stack)
						return -1;

					return 0;
				}
			}
		}

		private List<LiveRange> liveRanges;

		private void AddKeyLiveRange (Dictionary<string, LiveRange> values, Instructions.Instruction instruction, Operand operand)
		{
			if (operand is Register) {
				Register register = operand as Register;

				if (register.Parent.Ignore)
					return;
			}

			if (operand is Argument)
				return;

			Identifier identifier = operand as Identifier;

			if (identifier == null)
				return;

			if (identifier is Register
					&& (identifier as Register).PHI != null) {
				do {
					identifier = (identifier as Register).PHI;
				} while ((identifier as Register).PHI != null);
			}

			string id = identifier.ID;

			if (!values.ContainsKey (id)) {
				LiveRange liveRange = new LiveRange (id, instruction);
				liveRange.Identifier = identifier;

				values [id] = liveRange;

			} else
				values [id].End = instruction;
		}

		/// <summary>
		/// Computes the live ranges.
		/// </summary>
		private void ComputeLiveRanges ()
		{
			int index = 0;
			Dictionary<string, LiveRange> values = new Dictionary<string, LiveRange> ();

			List<Block> reversePostorder = ReversePostorder ();

			foreach (Block block in reversePostorder) {
				foreach (Instructions.Instruction instruction in block) {
					instruction.Index = index++;

					if (!instruction.Ignore)
						AddKeyLiveRange (values, instruction, instruction.Def);

					if (instruction.Use != null) {
						foreach (Operand operand in instruction.Use)
							AddKeyLiveRange (values, instruction, operand);
					}
				}
			}

			this.liveRanges = new List<LiveRange> ();

			foreach (KeyValuePair<string, LiveRange> entry in values)
				this.liveRanges.Add (entry.Value);

			this.liveRanges.Sort (new LiveRange.SortByStart ());

			#region Dump
			if (engine.Options.Dump) {
				engine.Dump.Section (DumpSection.LiveRanges);

				foreach (LiveRange entry in this.liveRanges)
					engine.Dump.Element (entry);

				engine.Dump.PopElement ();
			}
			#endregion

			// No identifier gets a register allocated only a position on the stack.
			foreach (LiveRange entry in this.liveRanges)
				(entry.Identifier as Identifier).ForceSpill = true;
		}

		/// <summary>
		/// Sets the next stack position.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		private void SetNextStackPosition (Identifier identifier)
		{
			if (identifier.InternalType == InternalType.ValueType)
				this.stackSize += this.engine.GetTypeSize (identifier.TypeFullName, 4) >> 2;

			else
				this.stackSize += this.engine.GetTypeSize (identifier.InternalType, 4) >> 2;

			identifier.Stack = this.stackSize;
		}

		/// <summary>
		/// Linears the scan register allocation.
		/// </summary>
		private void LinearScanRegisterAllocation ()
		{
			List<LiveRange> active = new List<LiveRange> ();
			List<int> registers = new List<int> ();

			for (int i = 0; i < this.engine.Assembly.AvailableRegistersCount; i++)
				registers.Add (i);

			for (int i = 0; i < this.liveRanges.Count; i++) {
				ExpireOldIntervals (active, registers, this.liveRanges [i]);

				if ((this.liveRanges [i].Identifier as Identifier).ForceSpill
						|| this.engine.Assembly.Spill (this.liveRanges [i].Identifier.InternalType))
					SetNextStackPosition (this.liveRanges [i].Identifier as Identifier);

				else {
					if (active.Count == this.engine.Assembly.AvailableRegistersCount)
						SpillAtInterval (active, registers, this.liveRanges [i]);

					else {
						int register = registers [0];
						registers.RemoveAt (0);

						this.liveRanges [i].Identifier.Register = register;

						active.Add (this.liveRanges [i]);
						active.Sort (new LiveRange.SortByEnd ());
					}
				}
			}

			this.liveRanges.Sort (new LiveRange.SortByRegisterStack ());

			#region Dump
			if (this.engine.Options.Dump) {
				this.engine.Dump.Section (DumpSection.RegisterAllocation);

				foreach (LiveRange entry in this.liveRanges)
					this.engine.Dump.Element (entry);

				this.engine.Dump.PopElement ();
			}
			#endregion

			foreach (Block block in this.blocks)
				block.UpdateRegisterAndStackValues ();

			return;
		}

		/// <summary>
		/// Expires the old intervals.
		/// </summary>
		/// <param name="active">The active.</param>
		/// <param name="registers">The registers.</param>
		/// <param name="liveRange">The live range.</param>
		private static void ExpireOldIntervals (List<LiveRange> active, List<int> registers, LiveRange liveRange)
		{
			List<LiveRange> remove = new List<LiveRange> ();

			foreach (LiveRange value in active) {
				if (value.End.Index >= liveRange.Start.Index)
					break;

				remove.Add (value);
			}

			foreach (LiveRange value in remove) {
				registers.Add (value.Identifier.Register);

				active.Remove (value);
			}

		}

		/// <summary>
		/// Spills at interval.
		/// </summary>
		/// <param name="active">The active.</param>
		/// <param name="registers">The registers.</param>
		/// <param name="liveRange">The live range.</param>
		private void SpillAtInterval (List<LiveRange> active, List<int> registers, LiveRange liveRange)
		{
			LiveRange spill = active [active.Count - 1];

			if (spill.End.Index > liveRange.End.Index) {
				liveRange.Identifier.Register = spill.Identifier.Register;

				spill.Identifier.Register = int.MinValue;

				SetNextStackPosition (spill.Identifier as Identifier);

				active.Remove (spill);

				active.Add (liveRange);
				active.Sort (new LiveRange.SortByEnd ());

			} else
				SetNextStackPosition (liveRange.Identifier as Identifier);

		}


		/// <summary>
		/// Dumps a representation of the blocks that comprise this method
		/// </summary>
		/// <returns></returns>
		public void DumpBlocks (DumpProcessor p)
		{
			DumpBlocks (blocks, p);
		}

		/// <summary>
		/// Dumps a representation of the blocks that comprise this method
		/// </summary>
		/// <returns></returns>
		private void DumpBlocks (List<Block> list, DumpProcessor p)
		{
			if (engine.Options.Dump) {
				p.Section (DumpSection.MethodBlocks);

				foreach (Block block in list)
					block.Dump (p);

				p.PopElement ();
			}
		}

		private void PreProcess ()
		{
			for (int i = 0; i < this.methodDefinition.Body.Variables.Count; i++) {
				TypeReference typeReference = this.methodDefinition.Body.Variables [i].VariableType;

				Local local = new Local (i, typeReference);
				local.InternalType = this.Engine.GetInternalType (local.TypeFullName);

				this.locals.Add (local);
			}

			if (this.methodDefinition.HasThis) {
				TypeReference typeReference = this.methodDefinition.DeclaringType;

				Argument argument = new Argument (0, typeReference);
				argument.InternalType = InternalType.M; // this.engine.GetInternalType (typeReference.ToString ());

				this.arguments.Add (argument);
			}

			int delta = this.arguments.Count;

			for (int i = 0; i < this.methodDefinition.Parameters.Count; i++) {
				TypeReference typeReference = this.methodDefinition.Parameters [i].ParameterType;

				Argument argument = new Argument (delta + i, typeReference);
				argument.InternalType = this.engine.GetInternalType (argument.TypeFullName);

				this.arguments.Add (argument);
			}
		}

		/// <summary>
		/// Processes this instance.
		/// </summary>
		public void Process ()
		{
			if (this.skipProcessing)
				return;

			if (this.engine.Options.Dump)
				this.engine.Dump.Element (this.methodDefinition);

			if (this.methodDefinition.Body == null)
				return;

			this.PreProcess ();

			this.BuildBlocks ();

			this.BlocksOptimization ();

			this.ConvertFromCIL ();

			if (this.engine.Options.DumpVerbosity >= 3)
				this.DumpBlocks ();

			/*this.Dominators ();

			if (this.engine.Options.DumpVerbosity >= 3)
				this.DumpBlocks ();
			*/

			this.InternalPropagation ();

			if (this.engine.Options.DumpVerbosity >= 3)
				this.DumpBlocks ();

			/*this.Optimizations ();
				
			if (this.engine.Options.DumpVerbosity >= 3)
				DumpBlocks ();
			*/

			this.TransformationOutOfSSA ();

			if (this.engine.Options.DumpVerbosity >= 3)
				this.DumpBlocks ();


			this.ComputeLiveRanges ();

			this.LinearScanRegisterAllocation ();

			if (this.engine.Options.DumpVerbosity >= 3)
				this.DumpBlocks ();

			if (this.engine.Options.Dump)
				this.engine.Dump.PopElement ();	// method
		}

		private List<Block> blocks;

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
		/// </returns>
		IEnumerator<Block> IEnumerable<Block>.GetEnumerator ()
		{
			foreach (Block block in this.blocks)
				yield return block;
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return ((IEnumerable<Block>) this).GetEnumerator ();
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override string ToString ()
		{
			if (this.methodDefinition != null)
				return this.MethodFullName;

			return base.ToString ();
		}

		public bool IsAllocObject
		{
			get
			{
				foreach (CustomAttribute customAttribute in this.methodDefinition.CustomAttributes) {
					if (customAttribute.Constructor.DeclaringType.FullName != typeof (SharpOS.AOT.Attributes.AllocObjectAttribute).ToString ())
						continue;

					if (Class.GetTypeFullName (methodDefinition.ReturnType.ReturnType) != "System.Object"
							|| methodDefinition.Parameters.Count != 1
							|| methodDefinition.Parameters [0].ParameterType.FullName != this.engine.VTableClass.TypeFullName)
						throw new EngineException (string.Format ("'{0}' is not a valid AllocObject method", this.methodDefinition.ToString ()));

					return true;
				}

				return false;
			}
		}
	}
}
