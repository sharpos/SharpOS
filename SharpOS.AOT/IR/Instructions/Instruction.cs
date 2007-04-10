/**
 *  (C) 2006-2007 The SharpOS Project Team - http://www.sharpos.org
 *
 *  Licensed under the terms of the GNU GPL License version 2.
 *
 *  Author: Mircea-Cristian Racasan <darx_kies@gmx.net>
 *
 */

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
		public Instruction (Operand value)
		{
			this.value = value;
		}

		private Block block = null;

		public Block Block {
			get {
				return block;
			}
			set {
				block = value;
			}
		}

		private Operand value;

		public Operand Value {
			get {
				return value;
			}
			set {
				this.value = value;
			}
		}

		private int index = -1;

		public int Index {
			get {
				return index;
			}
			set {
				index = value;
			}
		}

		public delegate void InstructionVisitor (SharpOS.AOT.IR.Instructions.Instruction instruction);

		public virtual void VisitInstruction (InstructionVisitor visitor)
		{
			visitor (this);
		}

		public virtual void VisitBlock (Block.BlockVisitor visitor)
		{}

		public virtual void UpdateIndex (ref int index)
		{
			this.index = index++;
		}

		public string FormatedIndex {
			get {
				if (this.index == -1) 
					return string.Empty;

				return String.Format ("{0} ", this.index).PadLeft (4);
			}
		}

		public void Replace (Dictionary<string, Operand> registerValues)
		{
			if (this.value == null) 
				return;

			if (this.value is Register && registerValues.ContainsKey (this.value.ToString()) == true) 
				this.value = registerValues [this.value.ToString ()];
			else
				this.value.Replace (registerValues);
		}

		private long startOffset = 0;

		public virtual long StartOffset {
			get {
				return startOffset;
			}
			set {
				startOffset = value;
			}
		}

		private long endOffset = 0;

		public virtual long EndOffset {
			get {
				return endOffset;
			}
			set {
				endOffset = value;
			}
		}

		private List<SharpOS.AOT.IR.Instructions.Instruction> branches = new List<SharpOS.AOT.IR.Instructions.Instruction>();

		public List<SharpOS.AOT.IR.Instructions.Instruction> Branches {
			get {
				return branches;
			}
			set {
				branches = value;
			}
		}

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

		public virtual void Dump (string prefix, StringBuilder stringBuilder)
		{
			if (this.value != null) 
				stringBuilder.Append (prefix + this.FormatedIndex + this.value.ToString() + "\n");
		}

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