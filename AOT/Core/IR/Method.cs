// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	William Lahti <xfurious@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
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
using SharpOS.AOT.IR.Instructions;
using SharpOS.AOT.IR.Operands;
using SharpOS.AOT.IR.Operators;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;


namespace SharpOS.AOT.IR {
	/// <summary>
	/// Represents a method in the AOT's intermediate representation. 
	/// </summary>
	public class Method : IEnumerable<Block> {
		private int stackSize = 0;

		/// <summary>
		/// Gets or sets the size of the stack.
		/// </summary>
		/// <value>The size of the stack.</value>
		public int StackSize {
			get {
				return stackSize;
			}
			set {
				stackSize = value;
			}
		}

		private Engine engine = null;
		
		/// <summary>
		/// Gets the engine.
		/// </summary>
		/// <value>The engine.</value>
		public Engine Engine {
			get {
				return this.engine;
			}
		}

		private MethodDefinition methodDefinition = null;

		/// <summary>
		/// Gets the method definition.
		/// </summary>
		/// <value>The method definition.</value>
		public MethodDefinition MethodDefinition {
			get {
				return this.methodDefinition;
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
			this.defuse.Dump (this.engine.Dump);
		}

		/// <summary>
		/// Gets an Argument object that represents the numbered method 
		/// argument named by <paramref name="i"/>. 
		/// </summary>
		/// <param name="i">The i.</param>
		/// <returns></returns>
		public Argument GetArgument (int i)
		{
			Argument argument;

			if (this.methodDefinition.HasThis) {
				if (i == 1) {
					argument = new Argument (i, this.methodDefinition.DeclaringType.FullName);
					argument.SizeType = Operand.InternalSizeType.U;

				} else {
					argument = new Argument (i, this.methodDefinition.Parameters [i - 2].ParameterType.FullName);
					argument.SizeType = this.engine.GetInternalType (argument.TypeName);
				}

			} else {
				argument = new Argument (i, this.methodDefinition.Parameters [i - 1].ParameterType.FullName);
				argument.SizeType = this.engine.GetInternalType (argument.TypeName);
			}

			return argument;
		}

		/// <summary>
		/// Gets a local variable with the index given by <paramref name="i" />.
		/// </summary>
		/// <param name="i">The index of the local variable.</param>
		/// <returns></returns>
		public Local GetLocal (int i)
		{
			Local local = new Local (i, this.methodDefinition.Body.Variables [i].VariableType.FullName);
			local.SizeType = this.engine.GetInternalType (this.methodDefinition.Body.Variables [i].VariableType.FullName);

			return local;
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

		private void LinkBlocks (Dictionary<int, Block> starts, Block current, Mono.Cecil.Cil.Instruction instruction)
		{
			if (instruction == null)
				return;

			int offset = instruction.Offset;

			if (!starts.ContainsKey (offset))
				throw new Exception (string.Format ("No block found starting at {1} in '{0}'.", this.MethodFullName, offset));

			Block link = starts [offset];

			if (!current.Outs.Contains (link))
				current.Outs.Add (link);

			if (!link.Ins.Contains (current))
				link.Ins.Add (current);
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
			Dictionary<int, Block> starts = new Dictionary<int, Block> ();
			int index = 0;
			Block current = null;
			foreach (Mono.Cecil.Cil.Instruction instruction in instructions) {
				if (index < offsets.Count && instruction.Offset == offsets [index]) {
					current = new Block (this);
					this.blocks.Add (current);

					starts [instruction.Offset] = current;

					index++;
				}

				current.CIL.Add (instruction);
			}

			// Link the blocks
			foreach (Block block in this.blocks) {
				Mono.Cecil.Cil.Instruction instruction = block.CIL [block.CIL.Count - 1];

				// this is for the conditional jump
				LinkBlocks (starts, block, instruction.Operand as Mono.Cecil.Cil.Instruction);

				if (instruction.OpCode.FlowControl != FlowControl.Branch)
					LinkBlocks (starts, block, instruction.Next);

				// This is for a 'switch', which can have lots of targets
				Mono.Cecil.Cil.Instruction [] targets = instruction.Operand as Mono.Cecil.Cil.Instruction [];

				if (targets != null) {
					foreach (Mono.Cecil.Cil.Instruction value in targets)
						LinkBlocks (starts, block, value);
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
				this.blocks[i].Index = i;

			return;
		}

		/// <summary>
		/// Converts from CIL to IR.
		/// </summary>
		private void ConvertFromCIL ()
		{
			foreach (Block block in this.Preorder ()) 
				block.ConvertFromCIL ();

			if (blocks.Count > 0
					&& this.methodDefinition.Body.Variables.Count > 0) {
				for (int i = 0; i < this.methodDefinition.Body.Variables.Count; i++) {
					VariableDefinition variableDefinition = this.methodDefinition.Body.Variables [this.methodDefinition.Body.Variables.Count - i - 1];
					blocks [0].InsertInstruction (0, new Initialize (this.GetLocal (this.methodDefinition.Body.Variables.Count - i - 1), variableDefinition.VariableType.ToString ()));
				}
			}

			return;
		}

		/// <summary>
		/// Preorders this instance.
		/// </summary>
		/// <returns></returns>
		private List<Block> Preorder ()
		{
			List<Block> list = new List<Block>();
			List<Block> visited = new List<Block>();

			Preorder (visited, list, this.blocks[0]);

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
					Preorder (visited, list, current.Outs[i]);
			}

			return;
		}

		/// <summary>
		/// Postorders this instance.
		/// </summary>
		/// <returns></returns>
		private List<Block> Postorder ()
		{
			List<Block> list = new List<Block>();
			List<Block> visited = new List<Block>();

			Postorder (visited, list, this.blocks[0]);

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
					Postorder (visited, list, current.Outs[i]);

				list.Add (current);
			}

			return;
		}

		/// <summary>
		/// Reverses the postorder.
		/// </summary>
		/// <returns></returns>
		private List<Block> ReversePostorder()
		{
			List<Block> list = new List<Block>();
			List<Block> visited = new List<Block>();
			List<Block> active = new List<Block>();

			visited.Add (this.blocks[0]);
			list.Add (this.blocks[0]);

			ReversePostorder (visited, active, list, this.blocks[0]);

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
					this.blocks[i].Dominators.Add (block);
				}
			}

			List<Block> list = Preorder();

			bool changed = true;

			while (changed) {
				changed = false;

				for (int i = 0; i < list.Count; i++) {
					List<Block> predecessorDoms = new List<Block>();
					List<Block> doms = new List<Block>();

					Block block = list[list.Count - 1 - i];

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
				engine.Dump.Dominance(this.blocks);
			
			return;
		}

		private class IdentifierBlocks : KeyedCollection<SharpOS.AOT.IR.Operands.Identifier, IdentifierBlocksItem> {
			/// <summary>
			/// Initializes a new instance of the <see cref="IdentifierBlocks"/> class.
			/// </summary>
			internal IdentifierBlocks () 
				: base ()
			{
			}

			/// <summary>
			/// When implemented in a derived class, extracts the key from the specified element.
			/// </summary>
			/// <param name="item">The element from which to extract the key.</param>
			/// <returns>The key for the specified element.</returns>
			protected override SharpOS.AOT.IR.Operands.Identifier GetKeyForItem (IdentifierBlocksItem item)
			{
				return item.key;
			}

			/// <summary>
			/// Gets the keys.
			/// </summary>
			/// <returns></returns>
			internal List<SharpOS.AOT.IR.Operands.Identifier> GetKeys ()
			{
				List<SharpOS.AOT.IR.Operands.Identifier> values = new List<SharpOS.AOT.IR.Operands.Identifier>();

				foreach (IdentifierBlocksItem item in this)
					values.Add (item.key);

				return values;
			}

			/// <summary>
			/// Adds the variable.
			/// </summary>
			/// <param name="identifier">The identifier.</param>
			/// <param name="block">The block.</param>
			internal void AddVariable (Identifier identifier, Block block)
			{
				foreach (IdentifierBlocksItem item in this) {
					if (item.key.ID.Equals (identifier.ID)) {
						if (!item.values.Contains (block)) 
							item.values.Add (block);

						return;
					}
				}

				List<Block> list = new List<Block> ();
				list.Add (block);

				IdentifierBlocksItem value = new IdentifierBlocksItem (identifier.Clone() as Identifier, list);
				this.Add (value);

				return;
			}
		}

		private class IdentifierBlocksItem {
			/// <summary>
			/// Initializes a new instance of the <see cref="IdentifierBlocksItem"/> class.
			/// </summary>
			/// <param name="key">The key.</param>
			/// <param name="values">The values.</param>
			public IdentifierBlocksItem (SharpOS.AOT.IR.Operands.Identifier key, List<Block> values)
			{
				this.key = key;
				this.values = values;
			}

			public SharpOS.AOT.IR.Operands.Identifier key;
			public List<Block> values;

			public override string ToString ()
			{
				return key.ToString ();
			}
		}

		/// <summary>
		/// Transformations to SSA.
		/// </summary>
		private void TransformationToSSA ()
		{
			IdentifierBlocks identifierList = new IdentifierBlocks ();

			// Find out in which blocks every variable gets defined
			foreach (Block block in blocks) {
				foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in block) {
					if (!(instruction is Assign))
						continue;

					Assign assign = instruction as Assign;

					List<Operand> usage = new List<Operand> ();
					Operand definition = instruction.GetDefinitionAndUsage (usage);

					if (definition != null)
						identifierList.AddVariable (definition as Identifier, block);
				}
			}

			this.engine.Dump.Section (DumpSection.SSATransform);

			// Insert PHI
			foreach (IdentifierBlocksItem item in identifierList) {
				this.engine.Dump.PHI (item.key.ToString ());

				List<Block> list = item.values;
				List<Block> everProcessed = new List<Block> ();

				foreach (Block block in list)
					everProcessed.Add (block);

				do {
					Block block = list [0];
					list.RemoveAt (0);

					foreach (Block dominanceFrontier in block.DominanceFrontiers) {
						bool found = false;

						// Is the PHI for the current variable already in the block?
						foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in dominanceFrontier) {
							if (!(instruction is PHI))
								break;

							Assign phi = instruction as Assign;

							string id = phi.Assignee.Value;

							if (id.Equals (item.key.Value)) {
								found = true;
								break;
							}
						}

						if (!found) {
							Operand [] operands = new Operand [dominanceFrontier.Ins.Count];

							for (int i = 0; i < operands.Length; i++)
								operands [i] = item.key.Clone ();

							PHI phi = new PHI (item.key.Clone () as Identifier, new Operands.Miscellaneous (new Operators.Miscellaneous (Operator.MiscellaneousType.InternalList), operands));

							dominanceFrontier.InsertInstruction (0, phi);

							if (!everProcessed.Contains (dominanceFrontier)) {
								everProcessed.Add (dominanceFrontier);
								list.Add (dominanceFrontier);
							}
						}
					}

				} while (list.Count > 0);
			}

			this.engine.Dump.PopElement ();

			// Rename the Variables
			foreach (Block block in blocks) {
				Dictionary<string, int> count = new Dictionary<string, int> ();
				Dictionary<string, Stack<int>> stack = new Dictionary<string, Stack<int>> ();

				foreach (IdentifierBlocksItem item in identifierList) {
					count [item.key.Value] = 0;
					stack [item.key.Value] = new Stack<int> ();
					stack [item.key.Value].Push (0);
				}

				this.SSARename (this.blocks [0], count, stack);
			}

			return;
		}

		/// <summary>
		/// Sets the identifier's version.
		/// </summary>
		/// <param name="stack">The stack.</param>
		/// <param name="identifier">The identifier.</param>
		private static void SetVersion (Dictionary < string, Stack < int >> stack, Identifier identifier)
		{
			if (!stack.ContainsKey (identifier.Value))
				identifier.Version = 0;
			else
				identifier.Version = stack [identifier.Value].Peek ();
		}

		/// <summary>
		/// SSAs the rename.
		/// </summary>
		/// <param name="block">The block.</param>
		/// <param name="count">The count.</param>
		/// <param name="stack">The stack.</param>
		private void SSARename (Block block, Dictionary<string, int> count, Dictionary<string, Stack<int>> stack)
		{
			foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in block) {
				List<Operand> usage = new List<Operand> ();
				Operand definition = instruction.GetDefinitionAndUsage (usage);

				// Update the Operands of the instruction (A = B -> A = B5)
				if (!(instruction is PHI)) {
					foreach (Operand operand in usage)
						SetVersion (stack, operand as Identifier);
				}

				// Update the Definition of a variaable (e.g. A = ... -> A3 = ...)
				if (definition != null) {
					string id = (definition as Identifier).Value;

					count [id]++;
					stack [id].Push (count [id]);

					definition.Version = count [id];
				}
			}

			// Now update the PHI of the successors
			foreach (Block successor in block.Outs) {
				int j = 0;
				bool found = false;

				// Find the position of the link to the successor in the successor itself
				foreach (Block predecessor in successor.Ins) {
					if (predecessor == block) {
						found = true;
						break;
					}

					j++;
				}

				if (!found)
					throw new Exception ("Could not find the successor position.");

				// The update the PHI Values
				foreach (Instructions.Instruction instruction in successor) {
					if (!(instruction is PHI))
						break;

					PHI phi = instruction as PHI;

					phi.Value.Operands [j].Version = stack [(phi as Assign).Assignee.Value].Peek ();
				}
			}

			// Descend in the Dominator Tree and do the "SSA Thing"
			foreach (Block child in block.ImmediateDominatorOf)
				this.SSARename (child, count, stack);

			// Pull from the stack the variable versions of the current block
			foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in block) {
				if (!(instruction is Assign))
					continue;

				List<Operand> usage = new List<Operand> ();
				Operand definition = instruction.GetDefinitionAndUsage (usage);

				if (definition != null)
					stack [(definition as Identifier).Value].Pop ();
			}

			return;
		}

		private class DefUse : KeyedCollection<string, DefUseItem> {
			/// <summary>
			/// Initializes a new instance of the <see cref="DefUse"/> class.
			/// </summary>
			public DefUse () 
				: base ()
			{
			}

			/// <summary>
			/// When implemented in a derived class, extracts the key from the specified element.
			/// </summary>
			/// <param name="item">The element from which to extract the key.</param>
			/// <returns>The key for the specified element.</returns>
			protected override string GetKeyForItem (DefUseItem item)
			{
				return item.key;
			}

			/// <summary>
			/// Gets the keys.
			/// </summary>
			/// <returns></returns>
			public List<string> GetKeys ()
			{
				List<string> values = new List<string>();

				foreach (DefUseItem item in this)
					values.Add (item.key);

				return values;
			}

			/// <summary>
			/// Gets the item.
			/// </summary>
			/// <param name="key">The key.</param>
			/// <returns></returns>
			private DefUseItem GetItem (string key)
			{
				if (!this.Contains (key))
					this.Add (new DefUseItem (key));

				return this [key];
			}

			/// <summary>
			/// Sets the definition.
			/// </summary>
			/// <param name="key">The key.</param>
			/// <param name="value">The value.</param>
			public void SetDefinition (string key, Instructions.Instruction value)
			{
				this.SetDefinition (key, value, false);
			}

			/// <summary>
			/// Sets the definition.
			/// </summary>
			/// <param name="key">The key.</param>
			/// <param name="value">The value.</param>
			/// <param name="force">if set to <c>true</c> no checks are performed.</param>
			public void SetDefinition (string key, Instructions.Instruction value, bool force)
			{
				this.GetItem (key).SetDefinition (value, force);
			}

			/// <summary>
			/// Adds the usage.
			/// </summary>
			/// <param name="key">The key.</param>
			/// <param name="value">The value.</param>
			public void AddUsage (string key, Instructions.Instruction value)
			{
				this.GetItem (key).AddUsage (value);
			}

			/// <summary>
			/// Removes the usage.
			/// </summary>
			/// <param name="key">The key.</param>
			/// <param name="value">The value.</param>
			public void RemoveUsage (string key, Instructions.Instruction value)
			{
				this.GetItem (key).RemoveUsage (value);
			}

			/// <summary>
			/// Dumps the specified dump processor.
			/// </summary>
			/// <param name="dumpProcessor">The dump processor.</param>
			public void Dump (DumpProcessor dumpProcessor)
			{
				dumpProcessor.Section (DumpSection.DefineUse);

				foreach (DefUseItem item in this)
					dumpProcessor.Element (item);

				dumpProcessor.PopElement ();
			}

			/// <summary>
			/// Validates this instance.
			/// </summary>
			public void Validate ()
			{
				foreach (DefUseItem item in this)
					item.Validate ();
			}
		}

		public class DefUseItem : IEnumerable<SharpOS.AOT.IR.Instructions.Instruction> {
			public string key;
			Instructions.Instruction definition = null;
			List<Instructions.Instruction> usage = new List<Instructions.Instruction> ();

			/// <summary>
			/// Gets the definition of the current key.
			/// </summary>
			/// <value>The definition.</value>
			public Instructions.Instruction Definition {
				get {
					return this.definition;
				}
			}

			/// <summary>
			/// Gets the number of instructions where the current key is being used.
			/// </summary>
			/// <value>The count.</value>
			public int Count {
				get {
					return this.usage.Count;
				}
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="DefUseItem"/> class.
			/// </summary>
			/// <param name="key">The key.</param>
			/// <param name="values">The values.</param>
			public DefUseItem (string key)
			{
				this.key = key;
			}

			/// <summary>
			/// Sets the definition.
			/// </summary>
			/// <param name="value">The value.</param>
			public void SetDefinition (Instructions.Instruction value)
			{
				this.SetDefinition (value, false);
			}

			/// <summary>
			/// Sets the definition.
			/// </summary>
			/// <param name="value">The value.</param>
			/// <param name="force">if set to <c>true</c> no checks are performed.</param>
			public void SetDefinition (Instructions.Instruction value, bool force)
			{
				if (force)
					this.definition = value;
				else if (this.definition == null)
					this.definition = value;

				else if (!(value is Instructions.System))
					throw new Exception ("'" + value + "' is defined again.");
			}

			/// <summary>
			/// Adds the instruction.
			/// </summary>
			/// <param name="instruction">The instruction.</param>
			public void AddUsage (Instructions.Instruction instruction)
			{
				if (!this.usage.Contains (instruction))
					this.usage.Add (instruction);
				
				// TODO else log?
			}

			/// <summary>
			/// Removes the instruction.
			/// </summary>
			/// <param name="instruction">The instruction.</param>
			public void RemoveUsage (Instructions.Instruction instruction)
			{
				if (this.usage.Contains (instruction))
					this.usage.Remove (instruction);

				// TODO else log?
			}

			/// <summary>
			/// Returns an enumerator that iterates through the collection.
			/// </summary>
			/// <returns>
			/// A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
			/// </returns>
			IEnumerator<SharpOS.AOT.IR.Instructions.Instruction> IEnumerable<SharpOS.AOT.IR.Instructions.Instruction>.GetEnumerator ()
			{
				foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in this.usage)
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

			/// <summary>
			/// Validates this instance.
			/// </summary>
			public void Validate ()
			{
				if (this.definition.Removed)
					throw new Exception ("The definition '" + this.definition.ToString () + "' should not be in the def-use list anymore.");

				foreach (Instructions.Instruction instruction in this.usage)
					if (this.definition.Removed)
						throw new Exception ("The instruction '" + this.definition.ToString () + "' should not be in the usage list anymore.");
			}

			public bool SkipCopyPropagation (Engine engine) 
			{
				// ... if it is no assign instruction
				if (!(definition is Assign))
					return true;

				// ... if it is an initialization of a varible
				if (definition is Initialize)
					return true;

				// ... if it is not used by any other instruction
				if (this.Count == 0)
					return true;


				Assign assign = definition as Assign;

				// ... if it is a conversion
				if (assign.Value.ConvertTo != SharpOS.AOT.IR.Operands.Operand.ConvertType.NotSet)
					return true;

				// ... if it is a reference or address
				if (assign.Value is Indirect)
					return true;

				// ... if it is a field
				if (assign.Value is Field)
					return true;
				
				// ... else 
				if (assign.Assignee is Identifier
						&& assign.Value is Identifier)
					return false;

				return true;
			}

			public override string ToString ()
			{
				return this.definition.ToString ();
			}
		}

		DefUse defuse;

		/// <summary>
		/// The first entry in every list of each variable is the definition instruction,
		/// the others are the instructions that use the variable.
		/// </summary>
		private void GetListOfDefUse ()
		{
			defuse = new DefUse ();

			foreach (Block block in this.blocks) {
				foreach (Instructions.Instruction instruction in block) {
					List<Operand> usage = new List<Operand> ();

					Operand definition = instruction.GetDefinitionAndUsage (usage);

					if (definition != null)
						defuse.SetDefinition (definition.ID, instruction);

					foreach (Operand operand in usage) {
						if (operand.Version == 0) {
							Instructions.System argument = new Instructions.System (new SharpOS.AOT.IR.Operands.Miscellaneous (new Operators.Miscellaneous (Operator.MiscellaneousType.Argument)));

							argument.Block = this.blocks [0];

							defuse.SetDefinition (operand.ID, argument);
						}

						defuse.AddUsage (operand.ID, instruction);
					}
				}
			}

			int stamp = 0;

			foreach (DefUseItem item in defuse) {
				string key = item.key;

				if (item.Definition == null)
					throw new Exception ("Def statement for '" + key + "' in '" + this.MethodFullName + "' not found.");

				Assign definition = item.Definition as Assign;

				if (definition == null)
					continue;

				definition.Assignee.Stamp = stamp++;

				foreach (Instructions.Instruction instruction in item)
					instruction.ReplaceOperand (definition.Assignee.ID, definition.Assignee, null);
			}

			if (this.engine.Options.Dump)
				DumpDefUse ();

			return;
		}

		private void InternalPropagationLogic (List<Instructions.Instruction> remove, Instructions.Instruction call, Operands.Call operand)
		{
			for (int i = 0; i < operand.Operands.Length; i++) {
				Operand parameter = operand.Operands [i];
				Assign assign = null;
				Instructions.Instruction instruction = call;

				DefUseItem item = null;

				do {
					if (!this.defuse.Contains (parameter.ID))
						throw new Exception (string.Format ("Could not find the defuse key '{0}'.", parameter.ID));

					item = this.defuse [parameter.ID];

					// Remove it from the usage list
					item.RemoveUsage (instruction);

					if (item.Count > 0) {
						// Removing any references in a PHI
						foreach (Instructions.Instruction entry in item) {
							if (!(entry is PHI))
								throw new Exception ("Propagation Core failed.");
						
							PHI phi = entry as PHI;

							foreach (Operand phiOperand in phi.Value.Operands) {
								if (phiOperand.ID == parameter.ID)
									continue;

								this.defuse.Remove (phiOperand.ID);
							}

							remove.Add (entry);
						}
					}

					this.defuse.Remove (parameter.ID);

					instruction = item.Definition;

					remove.Add (instruction);

					assign = instruction as Assign;

					parameter = assign.Value;
				}
				while (parameter is Register);

				operand.Operands [i] = parameter;

				if (parameter is Operands.Address) {
					Operands.Address address = parameter as Operands.Address;

					if (!this.defuse.Contains (address.Value.ID))
						throw new Exception (string.Format ("Could not find the defuse key '{0}'.", address.Value.ID));

					item = this.defuse [address.Value.ID];

					// Add it to the new item's usage list
					item.AddUsage (call);
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
						Operands.Call operand = call.Value as Operands.Call;

						if (engine.HasSharpOSAttribute (operand))
							this.InternalPropagationLogic (remove, instruction, operand);

						else if (engine.Assembly.IsInstruction (call.Method.Method.DeclaringType.FullName))
							this.InternalPropagationLogic (remove, instruction, operand);

					} else if (instruction is Instructions.Assign) {
						Instructions.Assign assign = instruction as Instructions.Assign;

						if (!(assign.Value is Operands.Call))
							continue;

						Operands.Call operand = assign.Value as Operands.Call;
						
						if (engine.HasSharpOSAttribute (operand))
							this.InternalPropagationLogic (remove, instruction, operand);

						else if (engine.Assembly.IsInstruction (operand.Method.DeclaringType.FullName))
							this.InternalPropagationLogic (remove, instruction, operand);

					}
				}
			}

			foreach (Instructions.Instruction instruction in remove)
				instruction.Block.RemoveInstruction (instruction);

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
			List<string> keys = this.defuse.GetKeys ();

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
						if (constant1.SizeType == Operand.InternalSizeType.I4
								&& constant2.SizeType == Operand.InternalSizeType.I4) {

							changed = true;

							int value = Convert.ToInt32 (constant1.Value) * Convert.ToInt32 (constant2.Value);

							definition.Value = new Constant (value);
							definition.Value.SizeType = Operand.InternalSizeType.I4;
						}
					} else if (binary.Type == Operator.BinaryType.Sub) {
						// TODO implement the other combinations
						if (constant1.SizeType == Operand.InternalSizeType.I4
								&& constant2.SizeType == Operand.InternalSizeType.I4) {

							changed = true;

							int value = Convert.ToInt32 (constant1.Value) - Convert.ToInt32 (constant2.Value);

							definition.Value = new Constant (value);
							definition.Value.SizeType = Operand.InternalSizeType.I4;
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

			return;
		}

		/// <summary>
		/// Gets the full name of the method.
		/// </summary>
		/// <value>The full name of the method.</value>
		public string MethodFullName {
			get {
				return Method.GetLabel (this.methodDefinition);
			}
		}

		private const string INTERNAL = "Internal.";

		/// <summary>
		/// Gets the label.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		public static string GetLabel (Mono.Cecil.MethodReference method)
		{
			StringBuilder result = new StringBuilder ();

			string value = method.DeclaringType.FullName;
				
			if (value.StartsWith (INTERNAL))
				value = value.Substring (INTERNAL.Length);

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
		/// If a block that has many predecessors is linked to a block that has many successors
		/// then an empty edge is inserted. Its used later for the transformation out of SSA.
		/// </summary>
		public void EdgeSplit ()
		{
			foreach (Block block in Preorder ()) {
				if (block.Ins.Count <= 1)
					continue;

				for (int i = 0; i < block.Ins.Count; i++) {
					Block predecessor = block.Ins [i];

					if (predecessor.Outs.Count <= 1)
						continue;

					int position = 0;

					for (; position < predecessor.Outs.Count && predecessor.Outs [position] != block; position++)
						;

					if (position == predecessor.Outs.Count)
						throw new Exception ("In '" + this.MethodFullName + "' Block " + predecessor.Index + " is not linked to the Block " + block.Index + ".");

					Block split = new Block ();

					split.SSABlock = true;
					split.Index = this.blocks [this.blocks.Count - 1].Index + 1;
					split.InsertInstruction (0, new Jump ());
					split.Ins.Add (predecessor);
					split.Outs.Add (block);

					predecessor.Outs [position] = split;
					block.Ins [i] = split;

					this.blocks.Add (split);
				}
			}
		}

		/// <summary>
		/// Transformation out of SSA
		/// </summary>
		private void TransformationOutOfSSA()
		{
			foreach (Block block in this.blocks) {

				List<Instructions.Instruction> remove = new List<SharpOS.AOT.IR.Instructions.Instruction>();

				List<Instructions.PHI> phiList = new List<SharpOS.AOT.IR.Instructions.PHI> ();

				foreach (Instructions.Instruction instruction in block) {
					if (!(instruction is PHI))
						break;

					PHI phi = instruction as PHI;

					phiList.Add (phi);
				}

				foreach (Instructions.PHI phi in phiList) {
					for (int i = 0; i < block.Ins.Count; i++) {
						Block predecessor = block.Ins[i];

						// Skip uninitilized register assignments (Reg1_5=Reg2_0)
						if (phi.Value.Operands[i] is Register
								&& (phi.Value.Operands[i] as Register).Version == 0) {
							continue;
						}

						Assign assign = new Assign (phi.Assignee, phi.Value.Operands[i]);

						int position = predecessor.InstructionsCount;

						if (predecessor.InstructionsCount > 0
								&& predecessor[predecessor.InstructionsCount - 1] is Jump) {
							position--;
						}

						predecessor.InsertInstruction (position, assign);

						remove.Add (phi);
					}
				}

				foreach (Instructions.Instruction instruction in remove)
					block.RemoveInstruction (instruction);
			}

			// Remove the empty blocks inserted by the SSA
			List<Block> removeBlocks = new List<Block> ();

			foreach (Block block in this.blocks) {
				if (block.SSABlock
						&& block.Ins.Count == 1
						&& block.InstructionsCount == 1) {

					removeBlocks.Add (block);

					Block _in = block.Ins [0];
					Block _out = block.Outs [0];

					bool found = false;

					for (int i = 0; i < _in.Outs.Count; i++) {
						if (_in.Outs [i] == block) {
							found = true;
							_in.Outs [i] = _out;
							break;
						}
					}

					if (!found)
						throw new Exception ("Missing In-Block in '" + this.MethodFullName + "'.");

					found = false;

					for (int i = 0; i < _out.Ins.Count; i++) {
						if (_out.Ins [i] == block) {
							found = true;
							_out.Ins [i] = _in;
							break;
						}
					}

					if (!found)
						throw new Exception ("Missing Out-Block in '" + this.MethodFullName + "'.");
				}
			}

			foreach (Block block in removeBlocks)
				this.blocks.Remove (block);
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
			public string ID {
				get {
					return id;
				}
				set {
					id = value;
				}
			}

			private Instructions.Instruction start = null;

			/// <summary>
			/// Gets or sets the start.
			/// </summary>
			/// <value>The start.</value>
			public Instructions.Instruction Start {
				get {
					return start;
				}
				set {
					start = value;
				}
			}

			private Instructions.Instruction end = null;

			/// <summary>
			/// Gets or sets the end.
			/// </summary>
			/// <value>The end.</value>
			public Instructions.Instruction End {
				get {
					return end;
				}
				set {
					end = value;
				}
			}

			private Operand identifier = null;

			/// <summary>
			/// Gets or sets the identifier.
			/// </summary>
			/// <value>The identifier.</value>
			public Operand Identifier {
				get {
					return identifier;
				}
				set {
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

					Operand.OperandVisitor visitor = delegate (bool assignee, int level, object parent, Operand operand) {
						// The argument needs no register as it is on the stack
						if (operand is Argument)
							return;

						Identifier identifier = (operand as Identifier);

						if (parent is Address)
							identifier.ForceSpill = true;

						string id = identifier.ID;

						if (!values.ContainsKey (id)) {
							LiveRange liveRange = new LiveRange (id, instruction);
							liveRange.Identifier = identifier;

							values [id] = liveRange;

						} else
							values [id].End = instruction;

					};

					instruction.VisitOperand (visitor);
				}
			}

			this.liveRanges = new List<LiveRange> ();

			foreach (KeyValuePair<string, LiveRange> entry in values)
				this.liveRanges.Add (entry.Value);

			this.liveRanges.Sort (new LiveRange.SortByStart ());

			#region Dump
			if (engine.Options.Dump) {
				engine.Dump.Section(DumpSection.LiveRanges);
				
				foreach (LiveRange entry in this.liveRanges)
					engine.Dump.Element(entry);
					
				engine.Dump.PopElement();
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
			if (identifier.SizeType == Operand.InternalSizeType.ValueType)
				this.stackSize += this.engine.GetTypeSize (identifier.TypeName, 4) >> 2;

			else
				this.stackSize += this.engine.GetTypeSize (identifier.SizeType, 4) >> 2;

			identifier.Stack = this.stackSize;
		}

		/// <summary>
		/// Linears the scan register allocation.
		/// </summary>
		private void LinearScanRegisterAllocation ()
		{
			List<LiveRange> active = new List<LiveRange>();
			List<int> registers = new List<int>();

			for (int i = 0; i < this.engine.Assembly.AvailableRegistersCount; i++)
				registers.Add (i);

			for (int i = 0; i < this.liveRanges.Count; i++) {
				ExpireOldIntervals (active, registers, this.liveRanges [i]);

				if ((this.liveRanges [i].Identifier as Identifier).ForceSpill
						|| this.engine.Assembly.Spill (this.liveRanges [i].Identifier.SizeType))
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
				active.Sort (new LiveRange.SortByEnd());

			} else
				SetNextStackPosition (liveRange.Identifier as Identifier);
		}

		/// <summary>
		/// Computes the type of the size.
		/// </summary>
		private void ComputeSizeType ()
		{
			int unsolvedCounter = 0;
			int lastUnsolvedCounter = 0;

			do {
				lastUnsolvedCounter = unsolvedCounter;
				unsolvedCounter = 0;

				foreach (Block block in this.ReversePostorder()) {
					foreach (Instructions.Instruction instruction in block) {
						if (!(instruction is Assign)) 
							continue;

						Assign assign = instruction as Assign;

						if (assign.Assignee.SizeType != Operand.InternalSizeType.NotSet)
							continue;

						bool found = false;

						if (assign.Value.ConvertTo != Operand.ConvertType.NotSet) {
							found = true;
							assign.Assignee.SizeType = assign.Value.ConvertSizeType;

						} else if (assign.Value.SizeType != Operand.InternalSizeType.NotSet) {
							found = true;
							assign.Assignee.SizeType = assign.Value.SizeType;

						} else if (assign.Value is Operands.Call) {
							found = true;
							Operands.Call call = assign.Value as Operands.Call;
							assign.Assignee.SizeType = this.engine.GetInternalType (call.Method.ReturnType.ReturnType.FullName);

						} else if (assign.Value is Operands.Boolean) {
							found = true;
							assign.Assignee.SizeType = Operand.InternalSizeType.I;

						} else if (assign.Value is Operands.Field) {
							found = true;
							Field field = assign.Value as Operands.Field;
							assign.Assignee.SizeType = this.engine.GetInternalType (field.ID);

						} else if (assign.Value is Operands.Constant) {
							found = true;
							
							if ((assign.Value as Operands.Constant).Value is string)
								assign.Assignee.SizeType = Operand.InternalSizeType.S;

						} else if (assign.Value.Operands.Length > 0) {
							foreach (Operand operand in assign.Value.Operands) {
								if (operand.ConvertTo != Operand.ConvertType.NotSet) {
									found = true;
									assign.Assignee.SizeType = operand.ConvertSizeType;
									break;

								} else if (operand.SizeType != Operand.InternalSizeType.NotSet) {
									found = true;
									assign.Assignee.SizeType = operand.SizeType;
									break;
								}
							}
						}

						if (assign.Assignee is Identifier
								&& (assign.Assignee as Identifier).TypeName == null
								&& assign.Value is Identifier
								&& (assign.Value as Identifier).TypeName != null)
							(assign.Assignee as Identifier).TypeName = (assign.Value as Identifier).TypeName;

						if (!found)
							unsolvedCounter++;
					}
				}

				if (unsolvedCounter != 0 && lastUnsolvedCounter == unsolvedCounter)
					throw new Exception ("Could not compute variable sizes in '" + this.MethodFullName + "'.");

			} while (unsolvedCounter != 0);

			return;
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
		
		/// <summary>
		/// Processes this instance.
		/// </summary>
		public void Process ()
		{
			if (this.engine.Options.Dump)
				this.engine.Dump.Element(this.methodDefinition);
			
			if (this.methodDefinition.Body == null)
				return;

			this.BuildBlocks ();

			this.BlocksOptimization ();
			this.ConvertFromCIL ();
			
			if (this.engine.Options.DumpVerbosity >= 3)
				this.DumpBlocks ();
			
			this.Dominators ();

			if (this.engine.Options.DumpVerbosity >= 3)
				this.DumpBlocks ();

			this.TransformationToSSA ();
			this.EdgeSplit ();

			if (this.engine.Options.DumpVerbosity >= 3)
				this.DumpBlocks ();

			this.GetListOfDefUse ();

			if (this.engine.Options.DumpVerbosity >= 3)
				this.DumpBlocks ();

			this.InternalPropagation ();

			if (this.engine.Options.DumpVerbosity >= 4)
				this.DumpDefUse ();

			if (this.engine.Options.DumpVerbosity >= 3)
				this.DumpBlocks ();

			/*this.Optimizations ();
				
			if (this.engine.Options.DumpVerbosity >= 4)
				DumpDefUse ();

			if (this.engine.Options.DumpVerbosity >= 3)
				DumpBlocks ();

			this.GetListOfDefUse ();*/

			this.TransformationOutOfSSA ();

			if (this.engine.Options.DumpVerbosity >= 3)
				this.DumpBlocks ();
			
			this.ComputeSizeType ();
			this.ComputeLiveRanges ();
			this.LinearScanRegisterAllocation ();

			this.DumpBlocks();

			if (this.engine.Options.Dump)
				this.engine.Dump.PopElement();	// method
				
			return;
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
			return ((IEnumerable<Block>) this).GetEnumerator();
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
	}
}
