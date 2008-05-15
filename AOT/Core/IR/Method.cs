//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	William Lahti <xfurious@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
//	Stanislaw Pitucha <viraptor@gmail.com>
//  Adam Stevenson <a.l.stevenson@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

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
	/// Represents a method in the AOT's intermediate representation (IR).
	/// </summary>
	public class Method : IEnumerable<Block>
    {
		List<ExceptionHandlingClause> exceptions = new List<ExceptionHandlingClause> ();

		/// <summary>
		/// Gets the exceptions.
		/// </summary>
		/// <value>The exceptions.</value>
		public List<ExceptionHandlingClause> Exceptions
		{
			get
			{
				return exceptions;
			}
		}

		private bool skipProcessing = false;

		/// <summary>
		/// Gets or sets a value indicating whether [skip processing].
		/// </summary>
		/// <value><c>true</c> if [skip processing]; otherwise, <c>false</c>.</value>
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

        private GenericInstanceMethod genericInstanceMethod = null;

		private MethodReference methodDefinition = null;

        

		/// <summary>
		/// Gets the method definition.
		/// </summary>
		/// <value>The method definition.</value>
		public MethodReference MethodDefinition
		{
			get
			{
				return this.methodDefinition;
			}
		}

		

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get
			{
				return this.methodDefinition.Name;
			}
		}

		private Class returnType = null;

		/// <summary>
		/// Gets the type of the return.
		/// </summary>
		/// <value>The type of the return.</value>
		public Class ReturnType
		{
			get
			{
				if (returnType == null)
					this.returnType = this.GetClass (this.methodDefinition.ReturnType.ReturnType);

				return this.returnType;
			}
		}


		/// <summary>
		/// Gets a value indicating whether this instance is return type big value type.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is return type big value type; otherwise, <c>false</c>.
		/// </value>
		public bool IsReturnTypeBigValueType
		{
			get
			{
				TypeDefinition returnType = this.methodDefinition.ReturnType.ReturnType as TypeDefinition;

				return returnType != null && returnType.IsValueType;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance has this.
		/// </summary>
		/// <value><c>true</c> if this instance has this; otherwise, <c>false</c>.</value>
		public bool HasThis
		{
			get
			{
				return this.methodDefinition.HasThis;
			}
		}

		private Class _class;

		/// <summary>
		/// Gets the class.
		/// </summary>
		/// <value>The class.</value>
		public Class Class
		{
			get
			{
				return this._class;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Method"/> class.
		/// </summary>
		/// <param name="engine">The engine.</param>
		/// <param name="_class">The _class.</param>
		/// <param name="methodDefinition">The method definition.</param>
		public Method (Engine engine, Class _class, MethodReference methodDefinition)
		{
			this.engine = engine;
			this._class = _class;
			this.methodDefinition = methodDefinition;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Method"/> class.
		/// </summary>
		/// <param name="engine">The engine.</param>
		/// <param name="_class">The _class.</param>
		/// <param name="methodDefinition">The method definition.</param>
		/// <param name="genericInstanceMethod">The generic instance method.</param>
        /// <refactorNote>
        ///    <summary>There are two possible sources of information that might overlap.
        ///    Looking at reducing that overlap.</summary>
        ///    
        ///    <note>After looking into cecil, they made a design decision to just extend MethodSpecification by adding
        ///    the class GenericInstanceMethod and having it inherit MethodSpecification.  This prevents extra fields
        ///    from being exposed, by MethodSpecification but at the same time, prevents a single class from
        ///    representing all of the method information present. But becuase they did it this way they are causing
        ///    branching statements in code that has to deal with both situations, which is thus increasing the code
        ///    complexity and making this code base less stable.
        /// 
        ///    In the future, a tool needs to be created that combines the information from both 
        ///    Mono.Cecil.MethodSpecification and Mono.Cecil.GenericInstanceMethod, and then
        ///    using that in this code base.   This would help prevent branching statements from arising
        ///    in the future.
        /// 
        ///    </note>
        ///    <remarks>This constructor is only being referenced once in the code base as of 4-19-08.</remarks>
        ///    <references>
        ///         <reference>Class.cs::GetMethodByName(), line 448</reference>
        ///    </references>
        /// </refactorNote>
		public Method (Engine engine, Class _class, MethodReference methodDefinition, GenericInstanceMethod genericInstanceMethod)
		{
			this.engine = engine;
			this._class = _class;
			this.methodDefinition = methodDefinition;
			this.genericInstanceMethod = genericInstanceMethod;
            
		}

		bool setup = false;

		/// <summary>
		/// Setups this instance.
		/// </summary>
		public void Setup ()
		{
			if (this.setup)
				return;

			this.PreProcess ();

			this.setup = true;
		}

		/// <summary>
		///
		/// </summary>
		public const byte IMTSize = 5;

		/// <summary>
		/// Assigns the interface method number.
		/// </summary>
		public void AssignInterfaceMethodNumber() {
			if (interfaceMethodNumber != -1)
				throw new EngineException("Interface number already assigned to " + this.MethodFullName);

			interfaceMethodNumber = interfaceMethodCount;
			interfaceMethodCount++;
		}

		static private int interfaceMethodCount = 0;
		private int interfaceMethodNumber = -1;

		/// <summary>
		/// Gets or sets the interface method number.
		/// </summary>
		/// <value>The interface method number.</value>
		public int InterfaceMethodNumber
		{
			get { return interfaceMethodNumber; }
			set { interfaceMethodNumber = value; }
		}

		/// <summary>
		/// Gets the interface method key.
		/// </summary>
		/// <value>The interface method key.</value>
		public int InterfaceMethodKey
		{
			get { return interfaceMethodNumber % IMTSize; }
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

		public List<Argument> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		/// <summary>
		/// Gets an Argument object that represents the numbered method
		/// argument named by <paramref name="i"/>.
		/// </summary>
		/// <param name="i">The i.</param>
		/// <param name="sequence">if set to <c>true</c> [sequence].</param>
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

		/// <summary>
		/// Adds the instruction offset.
		/// </summary>
		/// <param name="offsets">The offsets.</param>
		/// <param name="instruction">The instruction.</param>
		private void AddInstructionOffset (List<int> offsets, Mono.Cecil.Cil.Instruction instruction)
		{
			if (instruction != null && !offsets.Contains (instruction.Offset))
				offsets.Add (instruction.Offset);
		}

		/// <summary>
		/// Links the blocks.
		/// </summary>
		/// <param name="current">The current.</param>
		/// <param name="instruction">The instruction.</param>
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

		/// <summary>
		/// Gets the block by offset.
		/// </summary>
		/// <param name="i">The i.</param>
		/// <returns></returns>
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

			InstructionCollection instructions = this.CIL.Instructions;
			List<int> offsets = new List<int> ();

			GetOffsets(instructions, offsets);

			BuildBlocks(instructions, offsets);

			LinkBlocks ();

			MarkBlocks ();
		}

		private void MarkBlocks ()
		{
			MethodDefinition definition = this.methodDefinition as MethodDefinition;

			if (definition == null || definition.Body == null)
				return;

			foreach (ExceptionHandler exceptionHandler in definition.Body.ExceptionHandlers) {
				ExceptionHandlingClause clause = new ExceptionHandlingClause ();
				clause.Type = (SharpOS.AOT.Metadata.ExceptionHandlerType) exceptionHandler.Type;

				Block tryBegin = null;
				Block tryLast = null;
				Block tryEnd = null;
				Block filterBegin = null;
				Block filterLast = null;
				Block filterEnd = null;
				Block handlerBegin = null;
				Block handlerLast = null;
				Block handlerEnd = null;

				foreach (Block block in this.blocks) {
					if (block.CIL.Count == 0)
						continue;

					SetEHStartEnd (ref tryBegin, ref tryLast, ref tryEnd, exceptionHandler.TryStart, exceptionHandler.TryEnd, block);
					SetEHStartEnd (ref filterBegin, ref filterLast, ref filterEnd, exceptionHandler.FilterStart, exceptionHandler.HandlerStart /*FilterEnd*/, block);
					SetEHStartEnd (ref handlerBegin, ref handlerLast, ref handlerEnd, exceptionHandler.HandlerStart, exceptionHandler.HandlerEnd, block);
				}


				if (tryLast != null && exceptionHandler.Type == ExceptionHandlerType.Finally)
					tryLast.IsTryLast = true;

				if (handlerLast != null)
					handlerLast.IsCatchLast = true;


				if (handlerBegin != null) {
					if (exceptionHandler.Type == ExceptionHandlerType.Catch)
						handlerBegin.IsCatchBegin = true;

					else if (exceptionHandler.Type == ExceptionHandlerType.Filter)
						handlerBegin.IsCatchBegin = true;

					else if (exceptionHandler.Type == ExceptionHandlerType.Fault)
						handlerBegin.IsFaultBegin = true;

					else if (exceptionHandler.Type == ExceptionHandlerType.Finally)
						handlerBegin.IsFinallyBegin = true;
				}


				if (filterBegin != null)
					filterBegin.IsFilterBegin = true;


				clause.TryBegin = tryBegin;
				clause.TryEnd = tryEnd;
				clause.FilterBegin = filterBegin;
				clause.FilterEnd = filterEnd;
				clause.HandlerBegin = handlerBegin;
				clause.HandlerEnd = handlerEnd;

				if (exceptionHandler.CatchType != null)
					clause.Class = this.engine.GetClass (exceptionHandler.CatchType);

				this.exceptions.Add (clause);
			}
		}

		/// <summary>
		/// Sets the EH start end.
		/// </summary>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <param name="exceptionStart">The exception start.</param>
		/// <param name="exceptionEnd">The exception end.</param>
		/// <param name="block">The block.</param>
		private void SetEHStartEnd (ref Block start, ref Block last, ref Block end, Mono.Cecil.Cil.Instruction exceptionStart, Mono.Cecil.Cil.Instruction exceptionEnd, Block block)
		{
			if (exceptionStart == null || exceptionEnd == null)
				return;

			if (block.StartOffset >= exceptionStart.Offset
					&& block.EndOffset < exceptionEnd.Offset) {
				if (start == null)
					start = block;

				last = block;
			}

			if (start != null && end == null && block.StartOffset >= exceptionEnd.Offset)
				end = block;

			if ((block.EndOffset >= exceptionEnd.Offset
						&& block.StartOffset < exceptionEnd.Offset)
					|| (block.EndOffset >= exceptionStart.Offset
						&& block.StartOffset < exceptionStart.Offset))
				throw new EngineException (string.Format ("The blocks have not been built correctly in '{0}'.", this.MethodFullName));
		}

		/// <summary>
		/// Gets the offsets.
		/// </summary>
		/// <param name="instructions">The instructions.</param>
		/// <param name="offsets">The offsets.</param>
		private void GetOffsets (InstructionCollection instructions, List<int> offsets)
		{
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

			MethodDefinition definition = this.methodDefinition as MethodDefinition;

			foreach (ExceptionHandler exceptionHandler in definition.Body.ExceptionHandlers) {
				this.AddInstructionOffset (offsets, exceptionHandler.TryStart);
				this.AddInstructionOffset (offsets, exceptionHandler.TryEnd);
				this.AddInstructionOffset (offsets, exceptionHandler.FilterStart);
				this.AddInstructionOffset (offsets, exceptionHandler.HandlerStart);
				this.AddInstructionOffset (offsets, exceptionHandler.HandlerEnd);
			}

			offsets.Sort ();
		}

		/// <summary>
		/// Builds the blocks.
		/// </summary>
		/// <param name="instructions">The instructions.</param>
		/// <param name="offsets">The offsets.</param>
		private void BuildBlocks (InstructionCollection instructions, List<int> offsets)
		{
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
		}

		/// <summary>
		/// Links the blocks.
		/// </summary>
		private void LinkBlocks ()
		{
			// Link the blocks
			foreach (Block block in this.blocks) {
				Mono.Cecil.Cil.Instruction instruction = block.CIL [block.CIL.Count - 1];

				// this is for the conditional jump
				LinkBlocks (block, instruction.Operand as Mono.Cecil.Cil.Instruction);

				if (instruction.OpCode.FlowControl != FlowControl.Branch
						|| (instruction.OpCode.FlowControl == FlowControl.Branch
							&& (instruction.OpCode.Code == Code.Leave
								|| instruction.OpCode.Code == Code.Leave_S)))
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
			/*bool changed;

			do {
				changed = false;

				for (int i = 1; i < this.blocks.Count; i++) {
					if (this.blocks [i].Ins.Count == 1
							&& this.blocks [i - 1].Outs.Count == 1
							&& this.blocks [i].Ins [0] == this.blocks [i - 1]
							&& this.blocks [i - 1].Outs [0] == this.blocks [i]) {

						if (this.blocks [i - 1].CIL.Count > 0
								&& this.blocks [i - 1].CIL [this.blocks [i - 1].CIL.Count - 1].OpCode.FlowControl == FlowControl.Cond_Branch)
							continue;

						this.blocks [i - 1].Merge (this.blocks [i]);

						this.blocks.Remove (this.blocks [i]);

						changed = true;
						break;
					}
				}

			} while (changed);*/

			return;
		}

		/// <summary>
		/// Sets the index of the block.
		/// </summary>
		private void SetBlockIndex ()
		{
			for (int i = 0; i < this.blocks.Count; i++)
				this.blocks [i].Index = i;
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

			for (int i = 0; i < this.MaxStack; i++)
				this.registerVersions.Add (0);

			foreach (Block block in this.Preorder ())
				block.ConvertFromCIL ();

			// Insert Initialize instructions to initialize the local variables
			if (blocks.Count > 0
					&& this.CIL.Variables.Count > 0) {
				for (int i = 0; i < this.CIL.Variables.Count; i++) {
					VariableDefinition variableDefinition = this.CIL.Variables [this.CIL.Variables.Count - i - 1];

					Instructions.Instruction instruction = new Initialize (this.GetLocal (this.CIL.Variables.Count - i - 1), variableDefinition.VariableType);

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
		/// Produces list of traversed blocks from current instance, preorder style
		/// </summary>
		/// <returns>Ordered list of blocks</returns>
		private List<Block> Preorder ()
		{
			List<Block> list = new List<Block> (this.blocks.Count);

			Preorder (list, this.blocks [0]);

			return list;
		}

		/// <summary>
		/// Recursively visit graph (preorder style), starting at current block.
		/// </summary>
		/// <param name="list">List used to write down order.</param>
		/// <param name="current">Currently visited node.</param>
		private void Preorder (List<Block> list, Block current)
		{
			list.Add (current);

			for (int i = 0; i < current.Outs.Count; i++)
				if (!list.Contains (current.Outs [i]))
					Preorder (list, current.Outs [i]);

			return;
		}

		/// <summary>
		/// Produces list of traversed blocks from current instance, postorder style
		/// </summary>
		/// <returns>Ordered list of blocks</returns>
		private List<Block> Postorder ()
		{
			List<Block> list = new List<Block> (this.blocks.Count);
			List<Block> visited = new List<Block> (this.blocks.Count);

			Postorder (visited, list, this.blocks [0]);

			return list;
		}

		/// <summary>
		/// Recursively visit graph (postorder style), starting at current block.
		/// </summary>
		/// <param name="visited">Already visited blocks list.</param>
		/// <param name="list">List used to write down order.</param>
		/// <param name="current">Currently visited node.</param>
		private void Postorder (List<Block> visited, List<Block> list, Block current)
		{
			visited.Add (current);

			for (int i = 0; i < current.Outs.Count; i++)
				if (!visited.Contains (current.Outs [i]))
					Postorder (visited, list, current.Outs [i]);

			list.Add (current);

			return;
		}

		/// <summary>
		/// Produces list of traversed blocks from current instance, reverse postorder style
		/// </summary>
		/// <returns>Ordered list of blocks</returns>
		private List<Block> ReversePostorder ()
		{
			List<Block> list = new List<Block> (this.blocks.Count);

			list.Add (this.blocks [0]);

			for (int i = 0; i < list.Count; i++) {
				List<Block> outs = list[i].Outs;
				for (int o = 0; o < outs.Count; o++)
					if (!list.Contains (outs [o]))
						list.Insert(i+1, outs [o]);
			}

			return list;
		}

		/* previous version left - just in case
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
		*/

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

		/// <summary>
		/// Internals the propagation logic.
		/// </summary>
		/// <param name="instruction">The instruction.</param>
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
			foreach (Block block in this.blocks) {
				foreach (Instructions.Instruction instruction in block) {
					if (instruction is Instructions.Call) {
						Instructions.Call call = (instruction as Instructions.Call);

						if (!engine.HasSharpOSAttribute (call)
								&& !engine.Assembly.IsInstruction (call.Method.Class.TypeFullName))
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
                /// Why do we have two variables representing seemingly the same information?
                /// 
                /// Answer: Seems to be an issue with Mono.Cecil.  We need to represent information that is present in either the 
                /// genericInstanceMethod or in methodDefinition, or both.  Becuase of this, we have to include both references and check where the 
                /// information is coming from to pass the correct reference.
                if (this.genericInstanceMethod != null)
                {
                    return Method.GetLabel(this._class, this.genericInstanceMethod);
                }
                else
                {
                    return Method.GetLabel(this._class, this.methodDefinition);
                }
			}
		}

		// TODO Move it to Class.cs as a non-static method  there are five references to this method that are going to need to be investigated
        /// changed.  Oh and why can't this be just incorporated as a member method in this class (Method.cs), or better, into a class framework that
        /// supports AOT as well as CIL reflection.
		/// <summary>
		/// Gets the label.
		/// </summary>
		/// <param name="_class">The _class.</param>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		public static string GetLabel (Class _class, Mono.Cecil.MethodReference method)
		{

			

            /// The goal of this method is to generate a name like "ClassName.MethodName".  Right now there are two sources
            /// for the class name the class object and a compiled method.  
            /// 

            StringBuilder result = new StringBuilder();

            /// Stores the name of the class in which the method resides.  This name can come from two sources (as of right now)  See above comments.
            string className;

            if (_class != null)
            {
                /// Get the class name from the passed class object.
                className = _class.TypeFullName;
            }
            else
            {
                /// Get the class name from a compiled source using the method reference passed from cecil.
                className = method.DeclaringType.FullName;


                foreach (CustomAttribute attribute in method.DeclaringType.CustomAttributes)
                {
                    /// In an effor to keep the size of the kernel as small as possible (at least this what is
                    /// persumed), the SharpOS.AOT.Attributes.TargetNamespaceAttribute does not declare a property 
                    /// called name.  Instead it just has a constructor.  Since the argument of the constructor
                    /// is static for each instance of the attribute, that argument can be retrieved, which is the
                    /// namespace that should be used in building the target class.
                    /// 
                    /// If the attribute is not there, then 

                    if (!attribute.Constructor.DeclaringType.FullName.Equals(typeof(SharpOS.AOT.Attributes.TargetNamespaceAttribute).ToString()))
                        continue;

                    /// Combine the namespace with the string.  
                    /// 
                    /// Note the reason for calling this variable className and not FullName is because namespaces are not technically recognized
                    /// in IL.  Instead, they are considered part of the namespace itself.  

                    className = attribute.ConstructorParameters[0].ToString() + "." + method.DeclaringType.Name;

                    /// TODO: 1) Should there not be a break statement here, since there should only be one TargetNamespaceAttribute per class?
                }
            }

            /// Combine the class name with the method name.
            result.Append(className + "." + method.Name);

            /// If the method is a generic method, then append the fullname of each argument to the current result.
			if (method is GenericInstanceMethod)
            {
				GenericInstanceMethod genericInstanceMethod = method as GenericInstanceMethod;

				result.Append ("<");

				for (int i = 0; i < genericInstanceMethod.GenericArguments.Count; i++)
                {
                    if (i > 0)
                    {
                        result.Append(",");
                    }

					result.Append (genericInstanceMethod.GenericArguments [i].FullName);
				}

				result.Append (">");
			}

			result.Append ("(");

            /// Need to attach the parameter types, as method names can be overlaoded.  

			for (int i = 0; i < method.Parameters.Count; i++) {
				if (i != 0)
					result.Append (",");

				result.Append (method.Parameters [i].ParameterType.FullName);
			}


			result.Append (")");

			return result.ToString ();
		}

		/// <summary>
		/// Gets the ID.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		public static string GetID (Mono.Cecil.MethodReference method)
		{
			StringBuilder result = new StringBuilder ();

			result.Append (method.Name);

			if (method is GenericInstanceMethod) {
				GenericInstanceMethod genericInstanceMethod = method as GenericInstanceMethod;

				result.Append ("<");

				for (int i = 0; i < genericInstanceMethod.GenericArguments.Count; i++) {
					if (i > 0)
						result.Append (",");

					result.Append (genericInstanceMethod.GenericArguments [i].FullName);
				}

				result.Append (">");
			}

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

		/// <summary>
		///
		/// </summary>
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

			/// <summary>
			///
			/// </summary>
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

			/// <summary>
			///
			/// </summary>
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

			/// <summary>
			///
			/// </summary>
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
		/// Adds the key live range.
		/// </summary>
		/// <param name="values">The values.</param>
		/// <param name="instruction">The instruction.</param>
		/// <param name="operand">The operand.</param>
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

		/// <summary>
		/// Gets the class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		internal Class GetClass (TypeReference type)
		{
			if (type is GenericParameter
					&& this.genericInstanceMethod != null) {
				GenericParameter genericParameter = type as GenericParameter;

				int i = 0;
				for (; i < genericParameter.Owner.GenericParameters.Count; i++) {
					if (genericParameter.Owner.GenericParameters [i].FullName == genericParameter.FullName)
						break;
				}

				if (i >= genericParameter.Owner.GenericParameters.Count)
					throw new EngineException (string.Format ("Type '{0}' was not found in the method '{1}'.", type.ToString (), this.MethodFullName));

				type = this.genericInstanceMethod.GenericArguments [i];

			}

			return this._class.GetClass (type);
		}

		/// <summary>
		/// Pres the process.
		/// </summary>
		private void PreProcess ()
		{
			if (this.IsGenericType)
				return;

			if (this.methodDefinition.HasThis) {
				TypeReference typeReference = this.methodDefinition.DeclaringType;

				Class _class = this.GetClass (typeReference);
				InternalType internalType = InternalType.M;

				Argument argument = new Argument (0, _class, internalType);

				this.arguments.Add (argument);
			}

			int delta = this.arguments.Count;

			for (int i = 0; i < this.methodDefinition.Parameters.Count; i++) {
				TypeReference typeReference = this.methodDefinition.Parameters [i].ParameterType;

				Class _class = this.GetClass (typeReference);
				InternalType internalType = this.engine.GetInternalType (_class.TypeFullName);

				Argument argument = new Argument (delta + i, _class, internalType);

				this.arguments.Add (argument);
			}
			
			MethodBody cil = this.CIL;
			
			if (cil == null)
				return;

			for (int i = 0; i < cil.Variables.Count; i++) {
				TypeReference typeReference = cil.Variables [i].VariableType;

				Class _class = this.GetClass (typeReference);
				InternalType internalType = this.Engine.GetInternalType (_class.TypeFullName);

				Local local = new Local (i, _class, internalType);

				this.locals.Add (local);
			}
		}

		public bool processed = false;

		/// <summary>
		/// Processes this instance.
		/// </summary>
		public void Process ()
		{
			if (this.processed)
				return;

			if (this.IsGenericType)
				return;

			if (this.skipProcessing)
				return;

			if (this.engine.Options.Dump)
				this.engine.Dump.Element (this.methodDefinition);

			if (this.CIL == null)
				return;

			//this.PreProcess ();

			this.BuildBlocks ();

			//this.BlocksOptimization ();

			this.SetBlockIndex ();

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

			this.processed = true;
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

		/// <summary>
		/// It returns the unique name of this call. (e.g. "void namespace.class.method UInt32 UInt16")
		/// </summary>
		public string AssemblyLabel
		{
			get
			{
				return this.MethodFullName;
			}
		}

		/// <summary>
		/// Gets the ID.
		/// </summary>
		/// <value>The ID.</value>
		public string ID
		{
			get
			{
				if (this.genericInstanceMethod != null)
					return IR.Method.GetID (this.genericInstanceMethod);

				return IR.Method.GetID (this.methodDefinition);
			}
		}

		private int virtualSlot = int.MinValue;

		/// <summary>
		/// Gets or sets the virtual slot.
		/// </summary>
		/// <value>The virtual slot.</value>
		public int VirtualSlot
		{
			get
			{
				return virtualSlot;
			}
			set
			{
				virtualSlot = value;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is constructor.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is constructor; otherwise, <c>false</c>.
		/// </value>
		public bool IsConstructor
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition != null)
					return definition.IsConstructor;

				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is virtual.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is virtual; otherwise, <c>false</c>.
		/// </value>
		public bool IsVirtual
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition != null)
					return definition.IsVirtual;

				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is abstract.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is abstract; otherwise, <c>false</c>.
		/// </value>
		public bool IsAbstract
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition != null)
					return definition.IsAbstract;

				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is new slot.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is new slot; otherwise, <c>false</c>.
		/// </value>
		public bool IsNewSlot
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition != null)
					return definition.IsNewSlot;

				return false;
			}
		}

		/// <summary>
		/// Gets the max stack.
		/// </summary>
		/// <value>The max stack.</value>
		public int MaxStack
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition != null
						&& definition.Body != null)
					return definition.Body.MaxStack;

				return 0;
			}
		}

		/// <summary>
		/// Gets the CIL instructions count.
		/// </summary>
		/// <value>The CIL instructions count.</value>
		public int CILInstructionsCount
		{
			get
			{
				MethodBody methodBody = this.CIL;

				if (methodBody != null)
					return methodBody.Instructions.Count;

				return 0;
			}
		}

		/// <summary>
		/// Gets the CIL.
		/// </summary>
		/// <value>The CIL.</value>
		public MethodBody CIL
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition != null)
					return definition.Body;

				return null;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is simple main.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is simple main; otherwise, <c>false</c>.
		/// </value>
		public bool IsSimpleMain
		{
			get
			{
				return this.Name.Equals ("Main")
						&& this.methodDefinition.Parameters.Count == 0
						&& (this.ReturnType.TypeFullName.Equals (Mono.Cecil.Constants.Int32)
							|| this.ReturnType.TypeFullName.Equals (Mono.Cecil.Constants.Void));
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is marked main.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is marked main; otherwise, <c>false</c>.
		/// </value>
		public bool IsMarkedMain
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition == null)
					return false;

				foreach (CustomAttribute attribute in definition.CustomAttributes) {
					if (!attribute.Constructor.DeclaringType.FullName.Equals (typeof (SharpOS.AOT.Attributes.KernelMainAttribute).ToString ()))
						continue;

					return true;
				}

				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is kernel object from pointer.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is kernel object from pointer; otherwise, <c>false</c>.
		/// </value>
		public bool IsKernelObjectFromPointer
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition == null
						|| definition.CustomAttributes.Count == 0)
					return false;

				foreach (CustomAttribute attribute in definition.CustomAttributes) {
					if (!attribute.Constructor.DeclaringType.FullName.Equals (typeof (SharpOS.AOT.Attributes.PointerToObjectAttribute).ToString ()))
						continue;

					if (!(this.ReturnType.TypeFullName.Equals ("System.Object")
							&& definition.Parameters.Count == 1
							&& definition.Parameters [0].ParameterType.FullName.Equals ("System.Void*")))
						throw new EngineException ("'" + this._class.TypeFullName + "." + this.Name + "' is no '" + typeof (SharpOS.AOT.Attributes.PointerToObjectAttribute).ToString () + "' method.");


					return true;
				}

				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is kernel object from pointer.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is kernel object from pointer; otherwise, <c>false</c>.
		/// </value>
		public bool IsKernelPointerFromObject
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition == null
						|| definition.CustomAttributes.Count == 0)
					return false;

				foreach (CustomAttribute attribute in definition.CustomAttributes) {
					if (!attribute.Constructor.DeclaringType.FullName.Equals (typeof (SharpOS.AOT.Attributes.ObjectToPointerAttribute).ToString ()))
						continue;

					if (!(this.ReturnType.TypeFullName.Equals ("System.Void*")
							&& definition.Parameters.Count == 1
							&& definition.Parameters [0].ParameterType.FullName.Equals ("System.Object")))
						throw new EngineException ("'" + this._class.TypeFullName + "." + this.Name + "' is no '" + typeof (SharpOS.AOT.Attributes.ObjectToPointerAttribute).ToString () + "' method.");


					return true;
				}

				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is kernel label address.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is kernel label address; otherwise, <c>false</c>.
		/// </value>
		public bool IsKernelLabelAddress
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition == null
						|| definition.CustomAttributes.Count == 0)
					return false;

				foreach (CustomAttribute attribute in definition.CustomAttributes) {
					if (!attribute.Constructor.DeclaringType.FullName.Equals (typeof (SharpOS.AOT.Attributes.LabelAddressAttribute).ToString ()))
						continue;

					if (!(this.ReturnType.TypeFullName.Equals ("System.UInt32")
							&& definition.Parameters.Count == 1
							&& definition.Parameters [0].ParameterType.FullName.Equals ("System.String")))
						throw new EngineException ("'" + this._class.TypeFullName + "." + this.Name + "' is no '" + typeof (SharpOS.AOT.Attributes.LabelAddressAttribute).ToString () + "' method.");

					return true;
				}

				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is kernel labeled alloc.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is kernel labeled alloc; otherwise, <c>false</c>.
		/// </value>
		public bool IsKernelLabeledAlloc
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition == null
						|| definition.CustomAttributes.Count == 0)
					return false;

				foreach (CustomAttribute attribute in definition.CustomAttributes) {
					if (!attribute.Constructor.DeclaringType.FullName.Equals (typeof (SharpOS.AOT.Attributes.LabelledAllocAttribute).ToString ()))
						continue;

					if (!(this.ReturnType.TypeFullName.Equals ("System.Byte*")
							&& definition.Parameters.Count == 2
							&& definition.Parameters [0].ParameterType.FullName.Equals ("System.String")
							&& definition.Parameters [1].ParameterType.FullName.Equals ("System.UInt32")))
						throw new EngineException ("'" + this._class.TypeFullName + "." + this.Name + "' is no '" + typeof (SharpOS.AOT.Attributes.LabelledAllocAttribute).ToString () + "' method.");

					return true;
				}

				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is kernel alloc.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is kernel alloc; otherwise, <c>false</c>.
		/// </value>
		public bool IsKernelAlloc
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition == null
						|| definition.CustomAttributes.Count == 0)
					return false;

				foreach (CustomAttribute attribute in definition.CustomAttributes) {
					if (!attribute.Constructor.DeclaringType.FullName.Equals (typeof (SharpOS.AOT.Attributes.AllocAttribute).ToString ()))
						continue;

					if (!(this.ReturnType.TypeFullName.Equals ("System.Byte*")
							&& definition.Parameters.Count == 1
							&& definition.Parameters [0].ParameterType.FullName.Equals ("System.UInt32")))
						throw new EngineException ("'" + this._class.TypeFullName + "." + this.Name + "' is no '" + typeof (SharpOS.AOT.Attributes.AllocAttribute).ToString () + "' method.");

					return true;
				}

				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is kernel string.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is kernel string; otherwise, <c>false</c>.
		/// </value>
		public bool IsKernelString
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition == null
						|| definition.CustomAttributes.Count == 0)
					return false;

				foreach (CustomAttribute attribute in definition.CustomAttributes) {
					if (!attribute.Constructor.DeclaringType.FullName.Equals (typeof (SharpOS.AOT.Attributes.StringAttribute).ToString ()))
						continue;

					if (!(this.ReturnType.TypeFullName.Equals ("System.Byte*")
							&& definition.Parameters.Count == 1
							&& definition.Parameters [0].ParameterType.FullName.Equals ("System.String")))
						throw new EngineException ("'" + this._class.TypeFullName + "." + this.Name + "' is no 'String' method.");

					return true;
				}

				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is alloc object.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is alloc object; otherwise, <c>false</c>.
		/// </value>
		public bool IsAllocObject
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition == null
						|| definition.CustomAttributes.Count == 0)
					return false;

				foreach (CustomAttribute customAttribute in definition.CustomAttributes) {
					if (customAttribute.Constructor.DeclaringType.FullName != typeof (SharpOS.AOT.Attributes.AllocObjectAttribute).ToString ())
						continue;

					if (this.ReturnType.TypeFullName != Mono.Cecil.Constants.Object
							|| !definition.IsStatic
							|| definition.Parameters.Count != 1
							|| definition.Parameters [0].ParameterType.FullName != this.engine.VTableClass.TypeFullName)
						throw new EngineException (string.Format ("'{0}' is not a valid AllocObject method", this.methodDefinition.ToString ()));

					return true;
				}

				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is throw.
		/// </summary>
		/// <value><c>true</c> if this instance is throw; otherwise, <c>false</c>.</value>
		public bool IsThrow
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition == null
						|| definition.CustomAttributes.Count == 0)
					return false;

				foreach (CustomAttribute customAttribute in definition.CustomAttributes) {
					if (customAttribute.Constructor.DeclaringType.FullName != typeof (SharpOS.AOT.Attributes.ThrowAttribute).ToString ())
						continue;

					if (this.ReturnType.TypeFullName != Mono.Cecil.Constants.Void
							|| !definition.IsStatic
							|| definition.Parameters.Count != 1
							|| Class.GetTypeFullName (definition.Parameters [0].ParameterType) != "System.Exception")
						throw new EngineException (string.Format ("'{0}' is not a valid Throw method", this.methodDefinition.ToString ()));

					return true;
				}

				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is the implementation of `isinst'.
		/// </summary>
		/// <value><c>true</c> if this instance is isinst; otherwise, <c>false</c>.</value>
		public bool IsIsInst
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition == null
						|| definition.CustomAttributes.Count == 0)
					return false;

				foreach (CustomAttribute customAttribute in definition.CustomAttributes) {
					if (customAttribute.Constructor.DeclaringType.FullName != typeof (SharpOS.AOT.Attributes.IsInstAttribute).ToString ())
						continue;

					if (this.ReturnType.TypeFullName != Mono.Cecil.Constants.Object
							|| !definition.IsStatic
							|| definition.Parameters.Count != 2
							|| Class.GetTypeFullName (definition.Parameters [0].ParameterType) != Mono.Cecil.Constants.Object
							|| Class.GetTypeFullName (definition.Parameters [1].ParameterType) != this.engine.TypeInfoClass.TypeFullName)
						throw new EngineException (string.Format ("'{0}' is not a valid IsInst method", this.methodDefinition.ToString ()));

					return true;
				}

				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is the implementation of `castclass'.
		/// </summary>
		/// <value><c>true</c> if this instance is isinst; otherwise, <c>false</c>.</value>
		public bool IsCastClass
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition == null
						|| definition.CustomAttributes.Count == 0)
					return false;

				foreach (CustomAttribute customAttribute in definition.CustomAttributes) {
					if (customAttribute.Constructor.DeclaringType.FullName != typeof (SharpOS.AOT.Attributes.CastClassAttribute).ToString ())
						continue;

					if (this.ReturnType.TypeFullName != Mono.Cecil.Constants.Object
							|| !definition.IsStatic
							|| definition.Parameters.Count != 2
							|| Class.GetTypeFullName (definition.Parameters [0].ParameterType) != Mono.Cecil.Constants.Object
							|| Class.GetTypeFullName (definition.Parameters [1].ParameterType) != this.engine.TypeInfoClass.TypeFullName)
						throw new EngineException (string.Format ("'{0}' is not a valid CastClass method", this.methodDefinition.ToString ()));

					return true;
				}

				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is the runtime implementation
		/// of overflow handling.
		/// </summary>
		/// <value><c>true</c> if this instance is isinst; otherwise, <c>false</c>.</value>
		public bool IsOverflowHandler
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition == null
						|| definition.CustomAttributes.Count == 0)
					return false;

				foreach (CustomAttribute customAttribute in definition.CustomAttributes) {
					if (customAttribute.Constructor.DeclaringType.FullName != typeof (SharpOS.AOT.Attributes.OverflowHandlerAttribute).ToString ())
						continue;

					if (this.ReturnType.TypeFullName != Mono.Cecil.Constants.Void
							|| !definition.IsStatic
							|| definition.Parameters.Count != 0)
						throw new EngineException (string.Format ("'{0}' is not a valid OverflowHandler method", this.methodDefinition.ToString ()));

					return true;
				}

				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is the runtime implementation
		/// of overflow handling.
		/// </summary>
		/// <value><c>true</c> if this instance is isinst; otherwise, <c>false</c>.</value>
		public bool IsNullReferenceHandler
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition == null
						|| definition.CustomAttributes.Count == 0)
					return false;

				foreach (CustomAttribute customAttribute in definition.CustomAttributes) {
					if (customAttribute.Constructor.DeclaringType.FullName != typeof (SharpOS.AOT.Attributes.NullReferenceHandlerAttribute).ToString ())
						continue;

					if (this.ReturnType.TypeFullName != Mono.Cecil.Constants.Void
							|| !definition.IsStatic
							|| definition.Parameters.Count != 0)
						throw new EngineException (string.Format ("'{0}' is not a valid NullReferenceHandler method", this.methodDefinition.ToString ()));

					return true;
				}

				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is alloc SZ array.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is alloc SZ array; otherwise, <c>false</c>.
		/// </value>
		public bool IsAllocArray
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition == null
						|| definition.CustomAttributes.Count == 0)
					return false;

				foreach (CustomAttribute customAttribute in definition.CustomAttributes) {
					if (customAttribute.Constructor.DeclaringType.FullName != typeof (SharpOS.AOT.Attributes.AllocArrayAttribute).ToString ())
						continue;

					if (this.ReturnType.TypeFullName != Mono.Cecil.Constants.Object
							|| !definition.IsStatic
							|| definition.Parameters.Count != 2
							|| definition.Parameters [0].ParameterType.FullName != this.engine.VTableClass.TypeFullName
							|| definition.Parameters [1].ParameterType.FullName != Mono.Cecil.Constants.Int32)
						throw new EngineException (string.Format ("'{0}' is not a valid AllocArray method", this.methodDefinition.ToString ()));

					return true;
				}

				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this method is naked (Sexy baby... grrrrr).
		/// </summary>
		/// <value><c>true</c> if this instance is naked; otherwise, <c>false</c>.</value>
		public bool IsNaked
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition == null
						|| definition.CustomAttributes.Count == 0)
					return false;

				foreach (CustomAttribute customAttribute in definition.CustomAttributes) {
					if (customAttribute.Constructor.DeclaringType.FullName != typeof (SharpOS.AOT.Attributes.NakedAttribute).ToString ())
						continue;

					if (this.arguments.Count != 0)
						throw new EngineException (string.Format ("'{0}' is not a valid Naked method. It should have no parameters.", this.methodDefinition.ToString ()));

					return true;
				}

				return false;
			}
		}

		/// <summary>
		/// Gets the labels.
		/// </summary>
		/// <value>The labels.</value>
		public List<string> Labels
		{
			get
			{
				List<string> labels = new List<string> ();

				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition == null
						|| definition.CustomAttributes.Count == 0)
					return labels;

				foreach (CustomAttribute customAttribute in definition.CustomAttributes) {
					if (!customAttribute.Constructor.DeclaringType.FullName.Equals (typeof (SharpOS.AOT.Attributes.LabelAttribute).ToString ()))
						continue;

					string name = customAttribute.ConstructorParameters [0].ToString ();

					labels.Add (name);
				}

				return labels;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance has exception handling.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has exception handling; otherwise, <c>false</c>.
		/// </value>
		public bool HasExceptionHandling
		{
			get
			{
				MethodDefinition definition = this.methodDefinition as MethodDefinition;

				if (definition == null)
					return false;

				return definition.Body.ExceptionHandlers.Count != 0;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is CCTOR.
		/// </summary>
		/// <value><c>true</c> if this instance is CCTOR; otherwise, <c>false</c>.</value>
		public bool IsCCTOR
		{
			get
			{
				return this.MethodFullName.IndexOf (".cctor") != -1;
			}
		}

		public bool IsGenericType
		{
			get
			{
				return this.methodDefinition.GenericParameters != null
						&& this.methodDefinition.GenericParameters.Count > 0
						&& this.genericInstanceMethod == null;
			}
		}
	}
}
