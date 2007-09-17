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
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using SharpOS.AOT.IR.Operands;

namespace SharpOS.AOT.IR.Instructions {
	[Serializable]
	public abstract class Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Instruction"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public Instruction (Operand value)
		{
			this.value = value;
		}

		private Block block = null;

		/// <summary>
		/// Gets or sets the block.
		/// </summary>
		/// <value>The block.</value>
		public Block Block {
			get {
				return block;
			}
			set {
				block = value;
			}
		}

		private Operand value;

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public Operand Value {
			get {
				return value;
			}
			set {
				this.value = value;
			}
		}

		private int index = -1;

		/// <summary>
		/// Gets or sets the index.
		/// </summary>
		/// <value>The index.</value>
		public int Index {
			get {
				return this.index;
			}
			set {
				this.index = value;
			}
		}

		public delegate void InstructionVisitor (SharpOS.AOT.IR.Instructions.Instruction instruction);

		/// <summary>
		/// Visits the instruction.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		public virtual void VisitInstruction (InstructionVisitor visitor)
		{
			visitor (this);
		}

		/// <summary>
		/// Visits the block.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		public virtual void VisitBlock (Block.BlockVisitor visitor)
		{
		}

		/// <summary>
		/// Visits the operands.
		/// </summary>
		/// <param name="level">The level.</param>
		/// <param name="visitor">The visitor.</param>
		public virtual void VisitOperand (Operand.OperandVisitor visitor)
		{
			if (this.value != null)
				this.value.Visit (false, 0, this, visitor);
		}

		/// <summary>
		/// Gets the definition and usage (e.g. A = B + C -> definition: A & usage: [B, C]).
		/// </summary>
		/// <param name="usage">The Usage.</param>
		/// <returns>The Definition</returns>
		public Operand GetDefinitionAndUsage (List<Operand> usage)
		{
			Operand definition = null;

			Operand.OperandVisitor visitor = delegate (bool assignee, int level, object parent, Operand operand) {
				if (assignee && level == 0)
					definition = operand;
				else
					usage.Add (operand);
			};

			this.VisitOperand (visitor);

			return definition;
		}

		public virtual int ReplaceOperand (string id, Operand operand, Operand.OperandReplaceVisitor visitor)
		{
			int replacements = 0;

			if (this.value != null) {
				if (this.value.ID == id) {
					if (visitor == null || visitor (this, this.value)) {
						this.value = operand;
						replacements++;
					}
				} else
					replacements += this.value.ReplaceOperand (id, operand, visitor);
			}

			return replacements;
		}

		/*/// <summary>
		/// Updates the index.
		/// </summary>
		/// <param name="index">The index.</param>
		public virtual void UpdateIndex (ref int index)
		{
			this.index = index++;
		}*/

		/// <summary>
		/// Gets the index of the formated.
		/// </summary>
		/// <value>The index of the formated.</value>
		public string FormatedIndex {
			get {
				if (this.index == -1) 
					return string.Empty;

				return String.Format ("{0} ", this.index).PadLeft (4);
			}
		}

		/// <summary>
		/// Replaces the specified register values.
		/// </summary>
		/// <param name="registerValues">The register values.</param>
		/*public void Replace (Dictionary<string, Operand> registerValues)
		{
			if (this.value == null) 
				return;

			if (this.value is Register && registerValues.ContainsKey (this.value.ToString())) 
				this.value = registerValues [this.value.ToString ()];
			else
				this.value.Replace (registerValues);
		}*/

		private long startOffset = 0;

		/// <summary>
		/// Gets or sets the start offset.
		/// </summary>
		/// <value>The start offset.</value>
		public virtual long StartOffset {
			get {
				return startOffset;
			}
			set {
				startOffset = value;
			}
		}

		private long endOffset = 0;

		/// <summary>
		/// Gets or sets the end offset.
		/// </summary>
		/// <value>The end offset.</value>
		public virtual long EndOffset {
			get {
				return endOffset;
			}
			set {
				endOffset = value;
			}
		}

		private List<SharpOS.AOT.IR.Instructions.Instruction> branches = new List<SharpOS.AOT.IR.Instructions.Instruction>();

		/// <summary>
		/// Gets or sets the branches.
		/// </summary>
		/// <value>The branches.</value>
		public List<SharpOS.AOT.IR.Instructions.Instruction> Branches {
			get {
				return branches;
			}
			set {
				branches = value;
			}
		}

		private bool removed = false;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Instruction"/> is removed.
		/// It is used only for debugging purposes.
		/// </summary>
		/// <value><c>true</c> if removed; otherwise, <c>false</c>.</value>
		public bool Removed {
			get {
				return removed;
			}
			set {
				removed = value;
			}
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

			return p.ToString ().Replace ("\n", "");
		}

		/// <summary>
		/// Dumps the instruction.
		/// </summary>
		/// <param name="dumpProcessor">The dump processor.</param>
		public virtual void Dump (DumpProcessor dumpProcessor)
		{
			dumpProcessor.AddElement ("code", this.FormatedIndex + this.value.ToString (), true, true, false);
		}

		/// <summary>
		/// Clones this instance.
		/// </summary>
		/// <returns></returns>
		public SharpOS.AOT.IR.Instructions.Instruction Clone ()
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();

			binaryFormatter.Serialize (memoryStream, this);
			memoryStream.Seek (0, SeekOrigin.Begin);

			SharpOS.AOT.IR.Instructions.Instruction instruction = (SharpOS.AOT.IR.Instructions.Instruction) binaryFormatter.Deserialize (memoryStream);

			return instruction;
		}
	}
}