// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
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
				return index;
			}
			set {
				index = value;
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
		/// Updates the index.
		/// </summary>
		/// <param name="index">The index.</param>
		public virtual void UpdateIndex (ref int index)
		{
			this.index = index++;
		}

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
		public void Replace (Dictionary<string, Operand> registerValues)
		{
			if (this.value == null) 
				return;

			if (this.value is Register && registerValues.ContainsKey (this.value.ToString())) 
				this.value = registerValues [this.value.ToString ()];
			else
				this.value.Replace (registerValues);
		}

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

		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override string ToString ()
		{
			StringBuilder stringBuilder = new StringBuilder();

			Dump (string.Empty, stringBuilder);

			string value = stringBuilder.ToString().Trim();

			int position = value.IndexOf (" ");

			/*if (position != -1)
			{
			    value = value.Substring(position + 1);
			}*/

			return value;
		}

		/// <summary>
		/// Dumps the specified prefix.
		/// </summary>
		/// <param name="prefix">The prefix.</param>
		/// <param name="stringBuilder">The string builder.</param>
		public virtual void Dump (string prefix, StringBuilder stringBuilder)
		{
			if (this.value != null) 
				stringBuilder.Append (prefix + this.FormatedIndex + this.value.ToString() + "\n");
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