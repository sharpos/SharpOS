// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

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
	public class Method : IEnumerable<Block> {
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

		public MethodDefinition MethodDefinition {
			get {
				return this.methodDefinition;
			}
		}

		/// <summary>
		/// Dumps this instance.
		/// </summary>
		/// <returns></returns>
		public string Dump ()
		{
			return Dump (blocks);
		}

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

		/// <summary>
		/// Gets the argument.
		/// </summary>
		/// <param name="i">The i.</param>
		/// <returns></returns>
		public Argument GetArgument (int i)
		{
			Argument argument = new Argument (i);

			argument.SizeType = this.engine.GetInternalType (this.methodDefinition.Parameters[i - 1].ParameterType.FullName);
			return argument;
		}

		/// <summary>
		/// Gets the local.
		/// </summary>
		/// <param name="i">The i.</param>
		/// <returns></returns>
		public Local GetLocal (int i)
		{
			Local local = new Local (i);

			local.SizeType = this.engine.GetInternalType (this.methodDefinition.Body.Variables[i].VariableType.FullName);

			return local;
		}

		/// <summary>
		/// Dumps the specified list.
		/// </summary>
		/// <param name="list">The list.</param>
		/// <returns></returns>
		public string Dump (List<Block> list)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append ("===============================\n");
			stringBuilder.Append ("->" + this.methodDefinition.Name + "\n");
			stringBuilder.Append ("===============================\n");

			foreach (Block block in list) 
				block.Dump (string.Empty, stringBuilder);

			return stringBuilder.ToString();
		}

		/// <summary>
		/// Determines whether the specified instruction is branch.
		/// </summary>
		/// <param name="instruction">The instruction.</param>
		/// <param name="all">if set to <c>true</c> [all].</param>
		/// <returns>
		/// 	<c>true</c> if the specified instruction is branch; otherwise, <c>false</c>.
		/// </returns>
		public bool IsBranch (Mono.Cecil.Cil.Instruction instruction, bool all)
		{
			if (all && instruction.OpCode == OpCodes.Ret) 
				return true;

			if (instruction.OpCode == OpCodes.Br
					|| instruction.OpCode == OpCodes.Br_S
					|| instruction.OpCode == OpCodes.Brfalse
					|| instruction.OpCode == OpCodes.Brfalse_S
					|| instruction.OpCode == OpCodes.Brtrue
					|| instruction.OpCode == OpCodes.Brtrue_S
					|| instruction.OpCode == OpCodes.Beq
					|| instruction.OpCode == OpCodes.Beq_S
					|| instruction.OpCode == OpCodes.Bge
					|| instruction.OpCode == OpCodes.Bge_S
					|| instruction.OpCode == OpCodes.Bge_Un
					|| instruction.OpCode == OpCodes.Bge_Un_S
					|| instruction.OpCode == OpCodes.Bgt
					|| instruction.OpCode == OpCodes.Bgt_S
					|| instruction.OpCode == OpCodes.Bgt_Un
					|| instruction.OpCode == OpCodes.Bgt_Un_S
					|| instruction.OpCode == OpCodes.Ble
					|| instruction.OpCode == OpCodes.Ble_S
					|| instruction.OpCode == OpCodes.Ble_Un
					|| instruction.OpCode == OpCodes.Ble_Un_S
					|| instruction.OpCode == OpCodes.Blt
					|| instruction.OpCode == OpCodes.Blt_S
					|| instruction.OpCode == OpCodes.Blt_Un
					|| instruction.OpCode == OpCodes.Blt_Un_S
					|| instruction.OpCode == OpCodes.Bne_Un
					|| instruction.OpCode == OpCodes.Bne_Un_S
					|| instruction.OpCode == OpCodes.Switch
					|| instruction.OpCode == OpCodes.Leave
					|| instruction.OpCode == OpCodes.Leave_S
					|| instruction.OpCode == OpCodes.Endfinally
					|| instruction.OpCode == OpCodes.Throw
					|| instruction.OpCode == OpCodes.Rethrow) {
				return true;
			}

			return false;
		}

		/// <summary>
		/// Builds the blocks.
		/// </summary>
		private void BuildBlocks ()
		{
			blocks = new List<Block> ();

			Block currentBlock = new Block (this);
			blocks.Add (currentBlock);

			// 1st Step: Split the code in blocks that branch at the end

			for (int i = 0; i < this.methodDefinition.Body.Instructions.Count; i++) {
				Mono.Cecil.Cil.Instruction instruction = this.methodDefinition.Body.Instructions[i];

				currentBlock.CIL.Add (instruction);

				if (i < this.methodDefinition.Body.Instructions.Count - 1 && IsBranch (instruction, true)) {
					currentBlock = new Block (this);
					blocks.Add (currentBlock);
				}
			}

			// 2nd Step: Split the blocks if their code is referenced by other branches
			bool found;

			do {
				found = false;

				foreach (Block source in blocks) {
					if (this.IsBranch (source.CIL[source.CIL.Count - 1], false)
							&& (source.CIL[source.CIL.Count - 1].Operand is Mono.Cecil.Cil.Instruction
							    || source.CIL[source.CIL.Count - 1].Operand is Mono.Cecil.Cil.Instruction[])) {
						List<Mono.Cecil.Cil.Instruction> jumps = new List<Mono.Cecil.Cil.Instruction>();

						if (source.CIL[source.CIL.Count - 1].Operand is Mono.Cecil.Cil.Instruction) 
							jumps.Add (source.CIL[source.CIL.Count - 1].Operand as Mono.Cecil.Cil.Instruction);
						else 
							jumps = new List<Mono.Cecil.Cil.Instruction> (source.CIL[source.CIL.Count - 1].Operand as Mono.Cecil.Cil.Instruction[]);

						foreach (Mono.Cecil.Cil.Instruction jump in jumps) {

							if (jump == source.CIL[source.CIL.Count - 1]) 
								continue;

							for (int destinationIndex = 0; destinationIndex < blocks.Count; destinationIndex++) {
								Block destination = blocks[destinationIndex];
								Block newBlock = new Block (this);

								for (int i = 0; i < destination.CIL.Count; i++) {
									Mono.Cecil.Cil.Instruction instruction = destination.CIL[i];

									if (instruction == jump) {
										if (i == 0) 
											break;

										found = true;
									}

									if (found) 
										newBlock.CIL.Add (destination.CIL[i]);
									
								}

								if (found) {
									for (int i = 0; i < newBlock.CIL.Count; i++) 
										destination.CIL.Remove (newBlock.CIL[i]);

									blocks.Insert (destinationIndex + 1, newBlock);

									break;
								}
							}

							if (found) 
								break;
						}
					}

					if (found) 
						break;
				}

			} while (found);

			// 3rd step: split the try blocks in case they got mixed up with some other code
			do {
				found = false;

				foreach (ExceptionHandler exceptionHandler in this.methodDefinition.Body.ExceptionHandlers) {
					for (int i = 0; i < this.blocks.Count; i++) {
						Block block = this.blocks[i];

						if (exceptionHandler.TryStart.Offset > block.StartOffset
								&& exceptionHandler.TryStart.Offset <= block.EndOffset) {
							Block newBlock = new Block (this);

							for (int j = 0; j < block.CIL.Count; j++) {
								Mono.Cecil.Cil.Instruction instruction = block.CIL[j];

								if (instruction == exceptionHandler.TryStart) 
									found = true;

								if (found) 
									newBlock.CIL.Add (block.CIL[j]);
							}

							for (int j = 0; j < newBlock.CIL.Count; j++) 
								block.CIL.Remove (newBlock.CIL[j]);

							blocks.Insert (i + 1, newBlock);

							break;
						}

						if (block.StartOffset > exceptionHandler.TryStart.Offset) 
							break;
					}

					if (found) 
						break;
				}

			} while (found);

			return;
		}

		/// <summary>
		/// Fills the outs.
		/// </summary>
		/// <param name="destination">The destination.</param>
		/// <param name="instructions">The instructions.</param>
		private void FillOuts (Block destination, Mono.Cecil.Cil.Instruction[] instructions)
		{
			foreach (Mono.Cecil.Cil.Instruction instruction in instructions) {
				bool found = false;

				foreach (Block block in blocks) {
					if (block.CIL[0] == instruction) {
						found = true;

						destination.Outs.Add (block);

						break;
					}
				}

				if (!found) 
					throw new Exception ("Could not find the block for the instruction at offset '" + instruction.Offset + "'.");
			}

			return;
		}

		/// <summary>
		/// Classifies the and link blocks.
		/// </summary>
		private void ClassifyAndLinkBlocks ()
		{
			for (int i = 0; i < blocks.Count; i++) {
				Block block = blocks[i];

				if (block.CIL[block.CIL.Count - 1].OpCode == OpCodes.Ret) {
					block.Type = Block.BlockType.Return;

				} else if (block.CIL[block.CIL.Count - 1].OpCode == OpCodes.Switch) {
					block.Type = Block.BlockType.NWay;

					this.FillOuts (block, block.CIL[block.CIL.Count - 1].Operand as Mono.Cecil.Cil.Instruction[]);

				} else if (block.CIL[block.CIL.Count - 1].OpCode == OpCodes.Br
						|| block.CIL[block.CIL.Count - 1].OpCode == OpCodes.Br_S) {
					block.Type = Block.BlockType.OneWay;

					this.FillOuts (block, new Mono.Cecil.Cil.Instruction[] { block.CIL[block.CIL.Count - 1].Operand as Mono.Cecil.Cil.Instruction });

				} else if (block.CIL[block.CIL.Count - 1].OpCode == OpCodes.Endfinally) {
					block.Type = Block.BlockType.OneWay; // Block.BlockType.Finally;

				} else if (block.CIL[block.CIL.Count - 1].OpCode == OpCodes.Throw
						|| block.CIL[block.CIL.Count - 1].OpCode == OpCodes.Rethrow) {
					block.Type = Block.BlockType.Throw;

				} else if (block.CIL[block.CIL.Count - 1].OpCode == OpCodes.Leave
						|| block.CIL[block.CIL.Count - 1].OpCode == OpCodes.Leave_S) {
					Mono.Cecil.Cil.Instruction lastInstruction = block.CIL[block.CIL.Count - 1];

					this.FillOuts (block, new Mono.Cecil.Cil.Instruction[] { block.CIL[block.CIL.Count - 1].Operand as Mono.Cecil.Cil.Instruction });

					bool found = false;

					foreach (ExceptionHandler exceptionHandler in block.Method.MethodDefinition.Body.ExceptionHandlers) {
						if (exceptionHandler.TryEnd.Previous == lastInstruction) {
							found = true;
							block.Type = Block.BlockType.OneWay; // Block.BlockType.Try;

							break;

						} else if (exceptionHandler.HandlerEnd.Previous == lastInstruction) {
							found = true;
							block.Type = Block.BlockType.OneWay; // Block.BlockType.Catch;

							break;
						}
					}

					if (!found)
						throw new Exception ("Malformated Try/Catch block in '" + block.Method.MethodDefinition.Name + "'.");
						
				} else if (this.IsBranch (block.CIL[block.CIL.Count - 1], false)) {
					block.Type = Block.BlockType.TwoWay;

					this.FillOuts (block, new Mono.Cecil.Cil.Instruction[] { block.CIL[block.CIL.Count - 1].Operand as Mono.Cecil.Cil.Instruction });

					block.Outs.Add (blocks[i + 1]);

				} else {
					block.Type = Block.BlockType.Fall;
					block.Outs.Add (blocks[i + 1]);
				}
			}

			// Fill The Ins
			for (int i = 0; i < blocks.Count; i++) {
				for (int j = 0; j < blocks.Count; j++) {
					if (blocks[j].Outs.Contains (blocks[i]))
						blocks[i].Ins.Add (blocks[j]);
				}
			}

			return;
		}

		/// <summary>
		/// Blockses the optimization.
		/// </summary>
		public void BlocksOptimization ()
		{
			bool changed;

			do {
				changed = false;

				for (int i = 1; i < this.blocks.Count; i++) {
					if (this.blocks[i].Ins.Count == 1
							&& this.blocks[i - 1].Outs.Count == 1
							&& this.blocks[i].Ins[0] == this.blocks[i - 1]
							&& this.blocks[i - 1].Outs[0] == this.blocks[i]) {
						this.blocks[i - 1].Merge (this.blocks[i]);

						this.blocks.Remove (this.blocks[i]);

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
		/// Converts from CIL.
		/// </summary>
		private void ConvertFromCIL ()
		{
			foreach (Block block in blocks) 
				block.ConvertFromCIL (false);

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
		/// Dominatorses this instance.
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

			// Compute the Dominance Frontier
			foreach (Block block in blocks) {
				if (block.Ins.Count == 0)
					continue;
				
				foreach (Block predecessor in block.Ins) {
					Block runner = predecessor;

					while (runner != block.ImmediateDominator) {
						runner.DominanceFrontiers.Add (block);
						runner = runner.ImmediateDominator;
					}
				}
			}

			this.engine.WriteLine ("=======================================");
			this.engine.WriteLine ("Dominator");
			this.engine.WriteLine ("=======================================");

			foreach (Block block in this.blocks) {
				StringBuilder stringBuilder = new StringBuilder();

				if (block.ImmediateDominator == null)
					stringBuilder.Append ("<>");
				else
					stringBuilder.Append ("<" + block.ImmediateDominator.Index + ">");

				stringBuilder.Append (" " + block.Index + " -> [");

				foreach (Block dominator in block.Dominators) {
					if (dominator != block.Dominators[0])
						stringBuilder.Append (", ");

					stringBuilder.Append (dominator.Index);
				}

				stringBuilder.Append ("]");

				this.engine.WriteLine (stringBuilder.ToString());
			}

			this.engine.WriteLine ("=======================================");
			this.engine.WriteLine ("Dominator Tree");
			this.engine.WriteLine ("=======================================");

			foreach (Block parent in blocks) {
				if (parent.ImmediateDominatorOf.Count > 0) {
					StringBuilder stringBuilder = new StringBuilder();

					stringBuilder.Append (parent.Index + " -> [");

					foreach (Block child in parent.ImmediateDominatorOf) {
						if (child != parent.ImmediateDominatorOf[0])
							stringBuilder.Append (", ");

						stringBuilder.Append (child.Index);
					}

					stringBuilder.Append ("]");

					this.engine.WriteLine (stringBuilder.ToString());
				}
			}


			this.engine.WriteLine ("=======================================");
			this.engine.WriteLine ("Dominance Frontiers");
			this.engine.WriteLine ("=======================================");

			foreach (Block parent in blocks) {
				if (parent.DominanceFrontiers.Count > 0) {
					StringBuilder stringBuilder = new StringBuilder();

					stringBuilder.Append (parent.Index + " -> [");

					foreach (Block child in parent.DominanceFrontiers) {
						if (child != parent.DominanceFrontiers[0])
							stringBuilder.Append (", ");

						stringBuilder.Append (child.Index);
					}

					stringBuilder.Append ("]");

					this.engine.WriteLine (stringBuilder.ToString());
				}
			}

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
				/*if (identifier is SharpOS.AOT.IR.Operands.Register)
				{
				    return;
				}*/

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
		}

		/// <summary>
		/// Transformations to SSA.
		/// </summary>
		private void TransformationToSSA ()
		{
			IdentifierBlocks identifierList = new IdentifierBlocks();

			// Find out in which blocks every variable gets defined
			foreach (Block block in blocks) {
				foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in block) {
					if (!(instruction is Assign))
						continue;

					Assign assign = instruction as Assign;

					if (assign.Assignee is Field)
						continue;

					identifierList.AddVariable (assign.Assignee, block);
				}
			}

			// Insert PHI
			foreach (IdentifierBlocksItem item in identifierList) {
				this.engine.WriteLine (String.Format("PHI Identifier: {0}", item.key));

				List<Block> list = item.values;
				List<Block> everProcessed = new List<Block> ();

				foreach (Block block in list) 
					everProcessed.Add (block);

				do {
					Block block = list[0];
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
							Operand[] operands = new Operand[dominanceFrontier.Ins.Count];

							for (int i = 0; i < operands.Length; i++) 
								operands[i] = item.key.Clone();

							PHI phi = new PHI (item.key.Clone() as Identifier, new Operands.Miscellaneous (new Operators.Miscellaneous (Operator.MiscellaneousType.InternalList), operands));

							dominanceFrontier.InsertInstruction (0, phi);

							if (!everProcessed.Contains (dominanceFrontier)) {
								everProcessed.Add (dominanceFrontier);
								list.Add (dominanceFrontier);
							}
						}
					}

				} while (list.Count > 0);
			}

			// Rename the Variables
			foreach (Block block in blocks) {
				Dictionary<string, int> count = new Dictionary<string, int> ();
				Dictionary<string, Stack<int>> stack = new Dictionary<string, Stack<int>> ();

				foreach (IdentifierBlocksItem item in identifierList) {
					count[item.key.Value] = 0;
					stack[item.key.Value] = new Stack<int>();
					stack[item.key.Value].Push (0);
				}

				this.SSARename (this.blocks[0], count, stack);
			}

			return;
		}

		/// <summary>
		/// Gets the SSA stack value.
		/// </summary>
		/// <param name="stack">The stack.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		private int GetSSAStackValue (Dictionary < string, Stack < int >> stack, string name)
		{
			if (!stack.ContainsKey (name)) 
				return 0;

			return stack[name].Peek();
		}

		/// <summary>
		/// SSAs the rename.
		/// </summary>
		/// <param name="block">The block.</param>
		/// <param name="count">The count.</param>
		/// <param name="stack">The stack.</param>
		private void SSARename (Block block, Dictionary<string, int> count, Dictionary < string, Stack < int >> stack)
		{
			foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in block) {
				// Update the Operands of the instruction (A = B -> A = B5)
				if (!(instruction is PHI) && instruction.Value != null) {
					if (instruction.Value.Operands != null) {
						foreach (Operand operand in instruction.Value.Operands) {
							if (operand is Reference) {
								Identifier identifier = (operand as Reference).Value as Identifier;

								identifier.Version = GetSSAStackValue (stack, identifier.Value);

							} else if (operand is Field) {
								Identifier identifier = (operand as Field).Instance;

								if (identifier != null)
									identifier.Version = GetSSAStackValue (stack, identifier.Value);
							
							} else if (operand is Operands.Object) {
								Identifier identifier = (operand as Operands.Object).Address;

								identifier.Version = GetSSAStackValue (stack, identifier.Value);

							} else if (operand is Identifier) {
								Identifier identifier = operand as Identifier;

								identifier.Version = GetSSAStackValue (stack, identifier.Value);
							}
						}

					} else if (instruction.Value is Identifier) {
						Identifier identifier = instruction.Value as Identifier;

						identifier.Version = GetSSAStackValue (stack, identifier.Value);
					}
				}

				// Update the Definition of a variaable (e.g. A = ... -> A3 = ...)
				if (instruction is Assign) {
					Assign assign = instruction as Assign;

					if (assign.Assignee is Reference) {
						Identifier identifier = (assign.Assignee as Reference).Value;

						identifier.Version = GetSSAStackValue (stack, identifier.Value);

					} else if (assign.Assignee is Field) {
						Identifier identifier = (assign.Assignee as Field).Instance;

						if (identifier != null)
							identifier.Version = GetSSAStackValue (stack, identifier.Value);

					} else if (assign.Assignee is Operands.Object) {
						Identifier identifier = (assign.Assignee as Operands.Object).Address;
						
						identifier.Version = GetSSAStackValue (stack, identifier.Value);

					} else {
						string id = assign.Assignee.Value;

						count[id]++;
						stack[id].Push (count[id]);

						assign.Assignee.Version = count [id];
					}
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

					phi.Value.Operands[j].Version = stack[ (phi as Assign).Assignee.Value].Peek();
				}
			}

			// Descend in the Dominator Tree and do the "SSA Thing"
			foreach (Block child in block.ImmediateDominatorOf)
				this.SSARename (child, count, stack);

			// Pull from the stack the variable versions of the current block
			foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in block) {
				if (!(instruction is Assign))
					continue;

				Assign assign = instruction as Assign;

				if (!(assign.Assignee is Reference || assign.Assignee is Field || assign.Assignee is Operands.Object))
					stack [assign.Assignee.Value].Pop();
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
		}

		private class DefUseItem {
			/// <summary>
			/// Initializes a new instance of the <see cref="DefUseItem"/> class.
			/// </summary>
			/// <param name="key">The key.</param>
			/// <param name="values">The values.</param>
			public DefUseItem (string key, List<Instructions.Instruction> values)
			{
				this.key = key;
				this.values = values;
			}

			public string key;
			public List<Instructions.Instruction> values;
		}

		DefUse defuse;

		/// <summary>
		/// The first entry in every list of each variable is the definition instruction,
		/// the others are the instructions that use the variable.
		/// </summary>
		private void GetListOfDefUse ()
		{
			defuse = new DefUse();

			foreach (Block block in this.blocks) {
				foreach (Instructions.Instruction instruction in block) {
					if (instruction.Value != null && instruction.Value.Operands != null) {
						foreach (Operand operand in instruction.Value.Operands) {
							if (!(operand is Identifier))
								continue;

							string id = operand.ID;

							if (operand is Field
									&& (operand as Field).Instance != null)
								id = (operand as Field).Instance.ID;

							if (operand is Operands.Object)
								id = (operand as Operands.Object).Address.ID;

							if (!defuse.Contains (id)) {
								DefUseItem item = new DefUseItem (id, new List<SharpOS.AOT.IR.Instructions.Instruction>());
								item.values.Add (null);
								defuse.Add (item);

								if (operand.Version == 0) {
									defuse [id].values [0] = new Instructions.System (new SharpOS.AOT.IR.Operands.Miscellaneous (new Operators.Miscellaneous (Operator.MiscellaneousType.Argument)));
									defuse [id].values [0].Block = this.blocks [0];
								}
							}

							if (!defuse [id].values.Contains (instruction))
								defuse [id].values.Add (instruction);
						}
					}

					if (instruction is Assign) {
						Assign assign = instruction as Assign;

						string id = assign.Assignee.ID;

						if (assign.Assignee is Reference) {
							if (!defuse [id].values.Contains (instruction))
								defuse [id].values.Add (instruction);

						} else if (assign.Assignee is Field) {
							Field field = assign.Assignee as Field;

							if (field.Instance != null) {
								id = field.Instance.ID;

								if (!defuse [id].values.Contains (instruction))
									defuse [id].values.Add (instruction);
							}

						} else {
							if (!defuse.Contains (id)) {
								DefUseItem item = new DefUseItem (id, new List<SharpOS.AOT.IR.Instructions.Instruction> ());
								item.values.Add (instruction);
								defuse.Add (item);

							} else {
								if (defuse [id].values [0] != null)
									throw new Exception ("SSA variable '" + id + "' in '" + this.MethodFullName + "' defined a second time.");

								defuse [id].values [0] = instruction;
							}
						}
					}
				}
			}

			int stamp = 0;

			foreach (DefUseItem item in defuse) {
				string key = item.key;
				List<Instructions.Instruction> list = defuse [key].values;

				if (list [0] == null)
					throw new Exception ("Def statement for '" + key + "' in '" + this.MethodFullName + "' not found.");

				Assign definition = list [0] as Assign;

				if (definition == null)
					continue;

				definition.Assignee.Stamp = stamp++;

				for (int i = 1; i < list.Count; i++) {
					Instructions.Instruction instruction = list [i];

					if (instruction is Assign) {
						Assign assign = instruction as Assign;
						
						if (assign.Assignee is Reference) {
							Reference reference = assign.Assignee as Reference;

							if (reference.Value.ID.Equals (definition.Assignee.ID))
								reference.Value = definition.Assignee;

						} else if (assign.Assignee is Field) {
							Field field = (instruction as Assign).Assignee as Field;

							if (field.Instance != null
									&& field.Instance.ID.Equals (definition.Assignee.ID))
								field.Instance = definition.Assignee;
						}
					}

					if (instruction.Value == null)
						continue;

					for (int j = 0; j < instruction.Value.Operands.Length; j++) {
						Operand operand = instruction.Value.Operands [j];

						if (!(operand is Identifier))
							continue;

						string id = operand.ID;

						if (operand is Field
								&& (operand as Field).Instance != null)
							id = (operand as Field).Instance.ID;

						if (operand is Operands.Object)
							id = (operand as Operands.Object).Address.ID;

						if (!definition.Assignee.ID.Equals (id)) 
							continue;

						if (instruction.Value is Reference) {
							Reference reference = instruction.Value as Reference;

							reference.Value = definition.Assignee;

						} else if (instruction.Value is Field) {
							Field field = instruction.Value as Field;

							if (field.Instance != null)
								field.Instance = definition.Assignee;
						
						} else if (instruction.Value is Operands.Object) {
							Operands.Object _object = instruction.Value as Operands.Object;

							_object.Address = definition.Assignee;

						} else if (instruction.Value is Identifier)
							instruction.Value = definition.Assignee;

						else
							instruction.Value.Operands [j] = definition.Assignee;
					}
				}
			}

			this.engine.WriteLine ("=======================================");
			this.engine.WriteLine ("Def-Use");
			this.engine.WriteLine ("=======================================");

			foreach (DefUseItem item in defuse) {
				string key = item.key;
				List<Instructions.Instruction> list = defuse [key].values;

				this.engine.WriteLine (list [0].Block.Index + " : " + list [0].ToString ());

				for (int i = 1; i < list.Count; i++) {
					Instructions.Instruction instruction = list[i];

					this.engine.WriteLine ("\t" + instruction.Block.Index + " : " + instruction);
				}
			}

			return;
		}

		/// <summary>
		/// Deads the code elimination.
		/// </summary>
		private void DeadCodeElimination ()
		{
			List<string> keys = this.defuse.GetKeys ();

			this.engine.WriteLine ("=======================================");
			this.engine.WriteLine ("Dead Code Elimination");
			this.engine.WriteLine ("=======================================");

			while (keys.Count > 0) {
				string key = keys [0];
				keys.RemoveAt (0);

				List<Instructions.Instruction> list = this.defuse [key].values;

				// This variable is only defined but not used

				if (list.Count == 1
					&& !(list [0] is Assign
						&& (list [0] as Assign).Assignee is Field)) {
					// A = B + C;
					Instructions.Instruction definition = list [0];

					this.engine.WriteLine (definition.Block.Index + " : " + definition.ToString ());

					// Remove the instruction from the block that it is containing it
					definition.Block.RemoveInstruction (definition);

					// Remove the variable from the defuse list
					defuse.Remove (key);

					if (definition.Value != null
							&& !(definition is Instructions.System)) {
						// B & C used in "A = B + C"
						foreach (Operand operand in definition.Value.Operands) {
							if (!(operand is Identifier))
								continue;

							string id = operand.ID;

							// Remove "A = B + C" from B & C
							this.defuse [id].values.Remove (definition);

							// Add to the queue B & C to check them it they are used anywhere else
							if (!keys.Contains (id))
								keys.Add (id);
						}
					}
				}
			}

			return;
		}
		/// <summary>
		/// If there is an instruction like 'a = 1 + 2' is encountered then it gets replaced with 
		/// 'a = 3'.
		/// </summary>
		/// <returns>It returns true if one of the instructions got changed.</returns>
		private bool ConstantFolding ()
		{
			this.engine.WriteLine ("=======================================");
			this.engine.WriteLine ("Constant Folding");
			this.engine.WriteLine ("=======================================");

			bool changed = false;

			foreach (Block block in this.blocks) {
				foreach (Instructions.Instruction instruction in block) {
					if (!(instruction is Assign))
						continue;

					Assign assign = instruction as Assign;

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

					this.engine.WriteLine (assign.Block.Index + " : " + assign.ToString ());

					// TODO implement all the other operators
					if (binary.Type == Operator.BinaryType.Mul) {
						
						// TODO implement the other combinations
						if (constant1.SizeType == Operand.InternalSizeType.I4
							&& constant2.SizeType == Operand.InternalSizeType.I4) {

							changed = true;

							int value = Convert.ToInt32 (constant1.Value) * Convert.ToInt32 (constant2.Value);

							instruction.Value = new Constant (value);
							instruction.Value.SizeType = Operand.InternalSizeType.I4;
						}
					}

					this.engine.WriteLine ("\t" + assign.Block.Index + " : " + assign.ToString ());
				}
			}

			return changed;
		}

		/// <summary>
		/// It looks for instructions like 'a = 100; b = a;' and replaces them with 'b = 100;'
		/// </summary>
		private void ConstantPropagation()
		{
			List<string> keys = this.defuse.GetKeys ();

			this.engine.WriteLine ("=======================================");
			this.engine.WriteLine ("Constant Propagation");
			this.engine.WriteLine ("=======================================");

			keys.Sort ();

			while (keys.Count > 0) {
				string key = keys [0];
				keys.RemoveAt (0);

				List<Instructions.Instruction> list = this.defuse [key].values;

				Instructions.Instruction definition = list [0];

				// v2 = PHI(v1, v1)
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

					Assign assign = new Assign ( (definition as Assign).Assignee, sample);

					// Replace the PHI with a normal assignment
					definition.Block.RemoveInstruction (definition);
					definition.Block.InsertInstruction (0, assign);

					defuse[key].values[0] = assign;
				}

				// A = 100
				else if (definition is Assign
						&& (definition as Assign).Value is Constant
						&& !((definition as Assign).Assignee is Field)) {
					bool _break = false;

					// The first pass is to find out if the constant propagation can be done for current key
					// The second pass does the actual constant propagation
					for (int pass = 0; !_break && pass < 2; pass++) {
						// "X = A" becomes "X = 100"
						for (int i = 1; !_break && i < list.Count; i++) {
							Instructions.Instruction used = list[i];

							if (used.Value != null) {
								if (pass == 1) {
									this.engine.WriteLine (definition.Block.Index + " : " + definition.ToString ());
									this.engine.WriteLine ("\t >> " + definition.Block.Index + " : " + used.ToString ());
								}

								for (int j = 0; !_break && j < used.Value.Operands.Length; j++) {
									Operand operand = used.Value.Operands[j];

									// ref(local) can't be converted to ref(123)
									if (pass == 0 && operand is SharpOS.AOT.IR.Operands.Reference
											&& !((operand as SharpOS.AOT.IR.Operands.Reference).Value is SharpOS.AOT.IR.Operands.Register)) {
										_break = true;
										break;
									}

									// Replace A with 100
									if (pass == 1 && operand is Identifier
											&& operand.ID.Equals (key)) {
										
										if (used.Value is Identifier)
											used.Value = definition.Value;

										else
											used.Value.Operands[j] = definition.Value;
									}
								}

								if (pass == 1)
									this.engine.WriteLine ("\t << " + definition.Block.Index + " : " + used.ToString());
							}

							// Add X to the queue as "X = 100;"
							if (pass == 1 && used is Assign) {
								Identifier assignee = (used as Assign).Assignee;
								string id = assignee.ID;

								// Add to the queue
								if (!keys.Contains (id)
										&& !(assignee is Reference || assignee is Field))
									keys.Add (id);
							}
						}
					}

					if (!_break) {
						// Remove the instruction from the block that it is containing it
						definition.Block.RemoveInstruction (definition);

						// Remove the variable from the defuse list
						defuse.Remove (key);
					}
				}
			}

			return;
		}
		/// <summary>
		/// It looks for instructions like 'a = b; c = a;' and replaces them with 'c = b;'
		/// </summary>
		private void CopyPropagation ()
		{
			List<string> keys = this.defuse.GetKeys();

			this.engine.WriteLine ("=======================================");
			this.engine.WriteLine ("Copy Propagation");
			this.engine.WriteLine ("=======================================");

			while (keys.Count > 0) {
				string key = keys[0];
				keys.RemoveAt (0);

				List<Instructions.Instruction> list = this.defuse [key].values;

				Instructions.Instruction definition = list [0];

				// A = B
				if (!(definition is Assign) || list.Count == 1)
					continue;

				Assign assign = definition as Assign;

				if (assign.Value.ConvertTo != SharpOS.AOT.IR.Operands.Operand.ConvertType.NotSet
						/*|| assign.Value is Reference*/
						|| (assign.Value is Field && !this.engine.Assembly.IsRegister ((assign.Value as Identifier).Value))
						|| assign.Value is Arithmetic) {
					continue;

				} else if (assign.Assignee is Register
						&& assign.Value is Identifier
						&& this.engine.Assembly.IsRegister ( (assign.Value as Identifier).Value)) {

				} else if (assign.Assignee is Register
						&& assign.Value is Operands.Call
						&& this.engine.Assembly.IsInstruction ((assign.Value as Operands.Call).Method.DeclaringType.FullName)) {

				} else if (assign.Assignee is Identifier
						&& assign.Value is Identifier) {

				} else
					continue;

				this.engine.WriteLine (definition.Block.Index + " : " + definition.ToString ());

				bool _break = false;

				// The first pass is to find out if the copy propagation can be done for current key
				// The second pass does the actual copy propagation
				for (int pass = 0; !_break && pass < 2; pass++) {
					// "X = A" becomes "X = B"
					for (int i = 1; !_break && i < list.Count; i++) {
						Instructions.Instruction used = list [i];

						if (pass == 1 && used is Assign
								&& (used as Assign).Assignee is Reference) {
							Reference reference = (used as Assign).Assignee as Reference;

							if (reference.ID.Equals (key))
								reference.Value = definition.Value as Identifier;
						}

						if (used.Value != null && used.Value.Operands != null) {
							int replacements = 0;

							for (int j = 0; j < used.Value.Operands.Length; j++) {
								Operand operand = used.Value.Operands [j];

								// Replace A with B
								if (!(operand is Identifier
										&& operand.ID.Equals (key)))
									continue;

								if (pass == 1) {
									if (used.Value is Identifier)
										used.Value = definition.Value;

									else
										used.Value.Operands [j] = definition.Value;
								}

								replacements++;
							}

							if (pass == 0 && replacements == 0) {
								_break = true;
								break;
							}
							
							if (pass == 1)
								this.engine.WriteLine ("\t" + definition.Block.Index + " : " + used.ToString ());
						}

						if (pass == 1 && used is Assign) {
							Identifier assignee = (used as Assign).Assignee;
							string id = assignee.ID;

							// Add to the queue
							if (!keys.Contains (id)
									&& !(assignee is Reference || assignee is Field)) {
								this.engine.WriteLine (string.Format ("[*]Add Key: {0}", id));

								keys.Add (id);
							}
						}
					}

				}

				if (!_break) {
					// Remove the instruction from the block that it is containing it
					definition.Block.RemoveInstruction (definition);

					// Remove the variable from the defuse list
					defuse.Remove (key);
				}
			}

			return;
		}


		/// <summary>
		/// Gets the full name of the method.
		/// </summary>
		/// <value>The full name of the method.</value>
		public string MethodFullName {
			get {
				//return this.methodDefinition.DeclaringType.FullName + "." + this.methodDefinition.Name;
				return Method.GetLabel (this.methodDefinition);
			}
		}

		/// <summary>
		/// Gets the label.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		public static string GetLabel (MethodReference method)
		{
			StringBuilder result = new StringBuilder();

			result.Append (method.ReturnType.ReturnType.FullName + " ");
			result.Append (method.DeclaringType.FullName + "." + method.Name);

			foreach (ParameterReference parameter in method.Parameters)
				result.Append (" " + parameter.ParameterType.FullName);

			return result.ToString();
		}

		/// <summary>
		/// If a block that has many predecessors is linked to a block that has many successors
		/// then an empty edge is inserted. Its used later for the transformation out of SSA.
		/// </summary>
		public void EdgeSplit()
		{
			foreach (Block block in Preorder()) {
				if (block.Ins.Count > 1) {
					for (int i = 0; i < block.Ins.Count; i++) {
						Block predecessor = block.Ins[i];

						if (predecessor.Outs.Count > 1) {
							int position = 0;

							for (; position < predecessor.Outs.Count && predecessor.Outs[position] != block; position++) ;

							if (position == predecessor.Outs.Count) {
								throw new Exception ("In '" + this.MethodFullName + "' Block " + predecessor.Index + " is not linked to the Block " + block.Index + ".");
							}

							Block split = new Block();

							split.Index = this.blocks[this.blocks.Count - 1].Index + 1;
							split.Type = Block.BlockType.OneWay;
							split.InsertInstruction (0, new Jump());
							split.Ins.Add (predecessor);
							split.Outs.Add (block);

							predecessor.Outs[position] = split;
							block.Ins[i] = split;

							this.blocks.Add (split);
						}
					}
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

				foreach (Instructions.Instruction instruction in block) {
					if (!(instruction is PHI)) {
						break;
					}

					PHI phi = instruction as PHI;

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

						remove.Add (instruction);
					}
				}

				foreach (Instructions.Instruction instruction in remove)
					block.RemoveInstruction (instruction);
			}

			// Remove the empty blocks inserted by the SSA
			List<Block> removeBlocks = new List<Block> ();

			foreach (Block block in this.blocks) {
				if (block.Type == Block.BlockType.OneWay
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

		private class LiveRange : IComparable {
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

		/// <summary>
		/// Adds the line scan value.
		/// </summary>
		/// <param name="values">The values.</param>
		/// <param name="identifier">The identifier.</param>
		/// <param name="instruction">The instruction.</param>
		private void AddLineScanValue (Dictionary<string, LiveRange> values, Identifier identifier, Instructions.Instruction instruction)
		{
			if (this.engine.Assembly.IsRegister (identifier.Value))
				return;

			bool asmCall = instruction.Value is Operands.Call
				       && this.engine.Assembly.IsInstruction ( (instruction.Value as Operands.Call).Method.DeclaringType.FullName);

			if (identifier is Argument
					|| identifier is Field
					|| (identifier is Reference
						&& !(asmCall && !((identifier as Reference).Value is Argument))))
				return;

			if (asmCall) {
				identifier = (identifier as Reference).Value;
				identifier.ForceSpill = true;
			}

			string id = identifier.ID;

			if (!values.ContainsKey (id)) {
				LiveRange liveRange = new LiveRange (id, instruction);
				liveRange.Identifier = identifier;

				values[id] = liveRange;

			} else
				values[id].End = instruction;
		}

		private List<LiveRange> liveRanges;

		/// <summary>
		/// Computes the live ranges.
		/// </summary>
		private void ComputeLiveRanges ()
		{
			int index = 0;
			Dictionary<string, LiveRange> values = new Dictionary<string, LiveRange>();

			foreach (Block block in ReversePostorder()) {
				foreach (Instructions.Instruction instruction in block) {
					instruction.Index = index++;

					if (instruction.Value != null && instruction.Value.Operands != null) {
						foreach (Operand operand in instruction.Value.Operands) {
							if (operand is Identifier)
								AddLineScanValue (values, operand as Identifier, instruction);
						}
					}

					if (instruction is Assign)
						AddLineScanValue (values, (instruction as Assign).Assignee, instruction);
				}
			}

			this.liveRanges = new List<LiveRange> ();

			foreach (KeyValuePair<string, LiveRange> entry in values)
				this.liveRanges.Add (entry.Value);

			this.liveRanges.Sort (new LiveRange.SortByStart ());

			this.engine.WriteLine ("=======================================");
			this.engine.WriteLine ("Live Ranges");
			this.engine.WriteLine ("=======================================");

			foreach (LiveRange entry in this.liveRanges)
				this.engine.WriteLine (entry.ToString());

			return;
		}

		/// <summary>
		/// Sets the next stack position.
		/// </summary>
		/// <param name="operand">The operand.</param>
		private void SetNextStackPosition (Operand operand)
		{
			operand.Stack = this.stackSize;

			this.stackSize++;

			if (operand.SizeType == Operand.InternalSizeType.I8
					|| operand.SizeType == Operand.InternalSizeType.R8) 
				this.stackSize++;
		}

		/// <summary>
		/// Linears the scan register allocation.
		/// </summary>
		private void LinearScanRegisterAllocation ()
		{
			List<LiveRange> active = new List<LiveRange>();
			List<int> registers = new List<int>();
			int stackPosition = 0;

			for (int i = 0; i < this.engine.Assembly.AvailableRegistersCount; i++)
				registers.Add (i);

			for (int i = 0; i < this.liveRanges.Count; i++) {
				ExpireOldIntervals (active, registers, this.liveRanges[i]);

				if ( (this.liveRanges[i].Identifier as Identifier).ForceSpill
						|| this.engine.Assembly.Spill (this.liveRanges[i].Identifier.SizeType))
					SetNextStackPosition (this.liveRanges[i].Identifier);
				else {
					if (active.Count == this.engine.Assembly.AvailableRegistersCount)
						SpillAtInterval (active, registers, ref stackPosition, this.liveRanges[i]);
					else {
						int register = registers[0];
						registers.RemoveAt (0);

						this.liveRanges[i].Identifier.Register = register;

						active.Add (this.liveRanges[i]);
						active.Sort (new LiveRange.SortByEnd());
					}
				}
			}

			this.liveRanges.Sort (new LiveRange.SortByRegisterStack());

			this.engine.WriteLine ("=======================================");
			this.engine.WriteLine ("Linear Scan Register Allocation");
			this.engine.WriteLine ("=======================================");

			foreach (LiveRange entry in this.liveRanges)
				this.engine.WriteLine (entry.ToString ());

			return;
		}

		/// <summary>
		/// Expires the old intervals.
		/// </summary>
		/// <param name="active">The active.</param>
		/// <param name="registers">The registers.</param>
		/// <param name="liveRange">The live range.</param>
		private void ExpireOldIntervals (List<LiveRange> active, List<int> registers, LiveRange liveRange)
		{

			List<LiveRange> remove = new List<LiveRange>();

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
		/// <param name="stackPosition">The stack position.</param>
		/// <param name="liveRange">The live range.</param>
		private void SpillAtInterval (List<LiveRange> active, List<int> registers, ref int stackPosition, LiveRange liveRange)
		{
			LiveRange spill = active [active.Count - 1];

			if (spill.End.Index > liveRange.End.Index) {
				liveRange.Identifier.Register = spill.Identifier.Register;

				spill.Identifier.Register = int.MinValue;

				SetNextStackPosition (spill.Identifier);

				active.Remove (spill);

				active.Add (liveRange);
				active.Sort (new LiveRange.SortByEnd());

			} else
				SetNextStackPosition (liveRange.Identifier);
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
		/// Processes this instance.
		/// </summary>
		public void Process ()
		{
			if (this.methodDefinition.Body == null)
				return;

			this.BuildBlocks ();

			this.ClassifyAndLinkBlocks ();
			this.BlocksOptimization ();
			this.ConvertFromCIL ();
			this.Dominators ();

			this.engine.WriteLine (this.Dump ());

			this.TransformationToSSA ();
			this.EdgeSplit ();

			this.engine.WriteLine (this.Dump ());

			this.GetListOfDefUse ();

			this.engine.WriteLine (this.Dump ());

			this.DeadCodeElimination ();

			this.engine.WriteLine (this.Dump ());
	
			do {
				this.ConstantPropagation ();

				this.engine.WriteLine (this.Dump ());

				this.CopyPropagation ();
				
				this.engine.WriteLine (this.Dump ());

			} while (this.ConstantFolding ());

			this.TransformationOutOfSSA ();

			this.engine.WriteLine (this.Dump ());

			this.ComputeSizeType ();
			this.ComputeLiveRanges ();
			this.LinearScanRegisterAllocation ();

			this.engine.WriteLine (this.Dump ()); //ReversePostorder()));

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
	}
}
