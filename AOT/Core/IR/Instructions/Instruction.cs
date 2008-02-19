// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections.Generic;
using System.Text;
using SharpOS.AOT.IR.Operands;
using Mono.Cecil;


namespace SharpOS.AOT.IR.Instructions {
	/// <summary>
	/// Base class for all instructions
	/// </summary>
	public abstract class Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Instruction"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="def">The def.</param>
		/// <param name="use">The use.</param>
		public Instruction (string name, Operand def, Operand [] use)
		{
			this.name = name;
			this.def = def;
			this.use = use;
		}

		private bool ignore = false;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Instruction"/> is gets ignored.
		/// </summary>
		/// <value><c>true</c> if ignore; otherwise, <c>false</c>.</value>
		public bool Ignore
		{
			get
			{
				return ignore;
			}
			set
			{
				ignore = value;
			}
		}

		private bool isSpecialCase = false;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is special case.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is special case; otherwise, <c>false</c>.
		/// </value>
		public bool IsSpecialCase
		{
			get
			{
				return isSpecialCase;
			}
			set
			{
				isSpecialCase = value;
			}
		}

		private Block block = null;

		/// <summary>
		/// Gets or sets the block.
		/// </summary>
		/// <value>The block.</value>
		public Block Block
		{
			get
			{
				return this.block;
			}
			set
			{
				this.block = value;
			}
		}

		private int index = -1;

		/// <summary>
		/// Gets or sets the index.
		/// </summary>
		/// <value>The index.</value>
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		private long startOffset = 0;

		/// <summary>
		/// Gets or sets the start offset.
		/// </summary>
		/// <value>The start offset.</value>
		public virtual long StartOffset
		{
			get
			{
				return startOffset;
			}
			set
			{
				startOffset = value;
			}
		}

		private long endOffset = 0;

		/// <summary>
		/// Gets or sets the end offset.
		/// </summary>
		/// <value>The end offset.</value>
		public virtual long EndOffset
		{
			get
			{
				return endOffset;
			}
			set
			{
				endOffset = value;
			}
		}

		private List<SharpOS.AOT.IR.Instructions.Instruction> branches = new List<SharpOS.AOT.IR.Instructions.Instruction> ();

		/// <summary>
		/// Gets or sets the branches.
		/// </summary>
		/// <value>The branches.</value>
		public List<SharpOS.AOT.IR.Instructions.Instruction> Branches
		{
			get
			{
				return branches;
			}
			set
			{
				branches = value;
			}
		}

		private bool removed = false;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Instruction"/> is removed.
		/// It is used only for debugging purposes.
		/// </summary>
		/// <value><c>true</c> if removed; otherwise, <c>false</c>.</value>
		public bool Removed
		{
			get
			{
				return removed;
			}
			set
			{
				removed = value;
			}
		}

		/// <summary>
		/// The name of the instruction
		/// </summary>
		protected string name = string.Empty;

		protected Operand [] use = null;

		/// <summary>
		/// List of operands that are in use by this instruction
		/// </summary>
		public Operand [] Use
		{
			get
			{
				return this.use;
			}
		}

		protected Operand def = null;

		/// <summary>
		/// Operand that is defined by this instruction
		/// </summary>
		public Operand Def
		{
			set
			{
				this.def = value;
			}
			get
			{
				return this.def;
			}
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		public override string ToString ()
		{
			StringBuilder result = new StringBuilder();

			if (this.ignore)
				result.Append ('\t');

			if (this.isSpecialCase)
				result.Append ('>');

			result.Append (this.name);

			result.Append ('(');

			if (this.def != null) {
				result.Append (this.def.ToString ());
				result.Append (" <= ");
			}

			if (this.use != null) {
				for (int i = 0; i < this.use.Length; i++) {
					if (i > 0)
						result.Append (", ");

					if (this.use [i] != null)
						result.Append (this.use [i].ToString ());
				}
			}

			result.Append (')');


			return result.ToString ();
		}

		/// <summary>
		/// Gets the index of the formated.
		/// </summary>
		/// <value>The index of the formated.</value>
		public string FormatedIndex
		{
			get
			{
				if (this.index == -1)
					return string.Empty;

				return String.Format ("{0} ", this.index).PadLeft (4);
			}
		}

		/// <summary>
		/// Dumps the instruction.
		/// </summary>
		/// <param name="dumpProcessor">The dump processor.</param>
		public virtual void Dump (DumpProcessor dumpProcessor)
		{
			dumpProcessor.AddElement ("code", this.FormatedIndex + this.ToString (), true, true, false);
		}

		/// <summary>
		/// Gets the type of the bitwise result.
		/// </summary>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns></returns>
		protected InternalType GetBitwiseResultType (Operand first, Operand second)
		{
			if (first.InternalType == InternalType.I4
					&& second.InternalType == InternalType.I4)
				return InternalType.I4;

			if (first.InternalType == InternalType.I8
					&& second.InternalType == InternalType.I8)
				return InternalType.I8;

			if (first.InternalType == InternalType.I4
					&& second.InternalType == InternalType.I)
				return InternalType.I;

			if (first.InternalType == InternalType.I
					&& second.InternalType == InternalType.I4)
				return InternalType.I;

			return InternalType.NotSet;
		}

		/// <summary>
		/// Gets the type of the arithmetical result.
		/// </summary>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <param name="add">if set to <c>true</c> [add].</param>
		/// <param name="sub">if set to <c>true</c> [sub].</param>
		/// <param name="unsignedDiv">if set to <c>true</c> [unsigned div].</param>
		/// <param name="overflow">if set to <c>true</c> [overflow].</param>
		/// <returns></returns>
		protected InternalType GetArithmeticalResultType (Operand first, Operand second, bool add, bool sub, bool unsignedDiv, bool overflow)
		{
			if (first.InternalType == InternalType.I4) {
				if (second.InternalType == InternalType.I4)
					return InternalType.I4;

				if (second.InternalType == InternalType.I)
					return InternalType.I;

				if (second.InternalType == InternalType.M
						&& add)
					return InternalType.M;
			}

			if (first.InternalType == InternalType.I8
					&& second.InternalType == InternalType.I8)
				return InternalType.I8;

			if (first.InternalType == InternalType.I) {
				if (second.InternalType == InternalType.I4)
					return InternalType.I;

				if (second.InternalType == InternalType.I)
					return InternalType.I;

				if (second.InternalType == InternalType.M
						&& add)
					return InternalType.M;
			}

			if (first.InternalType == InternalType.F
					&& second.InternalType == InternalType.F
					&& !unsignedDiv
					&& !overflow)
				return InternalType.F;

			if (first.InternalType == InternalType.M) {
				if (second.InternalType == InternalType.I4
						&& (add || sub))
					return InternalType.M;

				if (second.InternalType == InternalType.I
						&& (add || sub))
					return InternalType.M;

				if (second.InternalType == InternalType.M
						&& sub)
					return InternalType.I;
			}

			return InternalType.NotSet;
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public virtual void Process (Method method)
		{
			return;
		}

		/// <summary>
		/// Adjusts the type of the register internal.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public InternalType AdjustRegisterInternalType (InternalType type)
		{
			InternalType result = InternalType.NotSet;

			switch (type) {
			case InternalType.I:
				result = InternalType.I;
				break;

			case InternalType.U:
				result = InternalType.I;
				break;

			case InternalType.I1:
			case InternalType.U1:
			case InternalType.I2:
			case InternalType.U2:
			case InternalType.I4:
			case InternalType.U4:
				result = InternalType.I4;
				break;

			case InternalType.I8:
			case InternalType.U8:
				result = InternalType.I8;
				break;

			case InternalType.R4:
			case InternalType.R8:
			case InternalType.F:
				result = InternalType.F;
				break;

			case InternalType.ValueType:
				result = InternalType.ValueType;
				break;

			case InternalType.O:
				result = InternalType.O;
				break;
			
			case InternalType.SZArray:
				result = InternalType.SZArray;
				break;
			
			case InternalType.Array:
				result = InternalType.Array;
				break;

			case InternalType.M:
				result = InternalType.M;
				break;
			}

			return result;
		}

		/// <summary>
		/// Lds the process.
		/// </summary>
		/// <param name="method">The method.</param>
		public void LdProcess (Method method)
		{
			this.def.InternalType = this.AdjustRegisterInternalType (this.use [0].InternalType);
			this.def.Type = this.use [0].Type;
		}

		/// <summary>
		/// LDFLDs the process.
		/// </summary>
		/// <param name="method">The method.</param>
		public void LdfldProcess (Method method)
		{
			FieldOperand field = (this.use [0] as FieldOperand);

			this.def.InternalType = this.AdjustRegisterInternalType (field.InternalType);
			this.def.Type = field.Field.Type;
		}

		/// <summary>
		/// STFLDs the process.
		/// </summary>
		/// <param name="method">The method.</param>
		public void StfldProcess (Method method)
		{
		}
	}
}