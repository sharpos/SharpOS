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
			string result = "";

			if (this.ignore)
				result += "\t";

			if (this.isSpecialCase)
				result += ">";

			result += this.name;

			result += "(";

			if (this.def != null)
				result += this.def.ToString () + " <= ";

			if (this.use != null) {
				for (int i = 0; i < this.use.Length; i++) {
					if (i > 0)
						result += ", ";

					if (this.use [i] != null)
						result += this.use [i].ToString ();
				}
			}

			result += ")";


			return result;
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

	/// <summary>
	/// 
	/// </summary>
	public class Add : Instruction {
		/// <summary>
		/// 
		/// </summary>
		public enum Type {
			/// <summary>
			/// 
			/// </summary>
			Add,
			/// <summary>
			/// 
			/// </summary>
			AddSignedWithOverflowCheck,
			/// <summary>
			/// 
			/// </summary>
			AddUnsignedWithOverflowCheck
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Add"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public Add (Type type, Register result, Register first, Register second)
			: base ("Add", result, new Operand [] { first, second })
		{
			this.type = type;
		}

		private Type type;

		/// <summary>
		/// Gets the type of the add.
		/// </summary>
		/// <value>The type of the add.</value>
		public Type AddType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = this.GetArithmeticalResultType (this.use [0], this.use [1], true, false, false, this.type != Type.Add);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Sub : Instruction {
		public enum Type {
			Sub,
			SubSignedWithOverflowCheck,
			SubUnsignedWithOverflowCheck
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Sub"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public Sub (Type type, Register result, Register first, Register second)
			: base ("Sub", result, new Operand [] { first, second })
		{
			this.type = type;
		}

		private Type type;

		public Type SubType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = this.GetArithmeticalResultType (this.use [0], this.use [1], false, true, false, this.type != Type.Sub);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Mul : Instruction {
		public enum Type {
			Mul,
			MulSignedWithOverflowCheck,
			MulUnsignedWithOverflowCheck
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Mul"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public Mul (Type type, Register result, Register first, Register second)
			: base ("Mul", result, new Operand [] { first, second })
		{
			this.type = type;
		}

		private Type type;

		public Type MulType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = this.GetArithmeticalResultType (this.use [0], this.use [1], false, false, false, this.type != Type.Mul);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Div : Instruction {
		public enum Type {
			Div,
			DivUnsigned
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Div"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public Div (Type type, Register result, Register first, Register second)
			: base ("Div", result, new Operand [] { first, second })
		{
			this.type = type;
		}

		private Type type;

		public Type DivType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = this.GetArithmeticalResultType (this.use [0], this.use [1], false, false, this.type == Type.Div, false);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Rem : Instruction {
		public enum Type {
			Remainder,
			RemainderUnsigned
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Rem"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public Rem (Type type, Register result, Register first, Register second)
			: base ("Rem", result, new Operand [] { first, second })
		{
			this.type = type;
		}

		private Type type;

		public Type RemType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = this.GetArithmeticalResultType (this.use [0], this.use [1], false, false, false, false);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Neg : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Neg"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Neg (Register result, Register value)
			: base ("Neg", result, new Operand [] { value })
		{
			result.InternalType = value.InternalType;
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			Register register = this.use [0] as Register;

			if (register.InternalType == InternalType.I4
					|| register.InternalType == InternalType.I8
					|| register.InternalType == InternalType.F
					|| register.InternalType == InternalType.I)
				this.def.InternalType = register.InternalType;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Shl : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Shl"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public Shl (Register result, Register first, Register second)
			: base ("Shl", result, new Operand [] { first, second })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = this.use [0].InternalType;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Shr : Instruction {
		public enum Type {
			SHR,
			SHRUnsigned
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Shr"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public Shr (Type type, Register result, Register first, Register second)
			: base ("Shr", result, new Operand [] { first, second })
		{
			this.type = type;
		}

		private Type type;

		public Type ShrType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = this.use [0].InternalType;
		}
	}

	public enum RelationalType {
		Equal,
		GreaterThan,
		GreaterThanUnsignedOrUnordered,
		GreaterThanOrEqual,
		GreaterThanOrEqualUnsignedOrUnordered,
		LessThan,
		LessThanUnsignedOrUnordered,
		LessThanOrEqual,
		LessThanOrEqualUnsignedOrUnordered,
		NotEqualOrUnordered
	}

	/// <summary>
	/// 
	/// </summary>
	public class Branch : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Branch"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public Branch (RelationalType type, Register first, Register second)
			: base ("Branch", null, new Operand [] { first, second })
		{
			this.type = type;
		}

		private RelationalType type;

		public RelationalType RelationalType
		{
			get
			{
				return this.type;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class SimpleBranch : Instruction {
		public enum Type {
			True,
			False
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleBranch"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="operand">The operand.</param>
		public SimpleBranch (Type type, Register operand)
			: base ("SimpleBranch " + type.ToString () + " ", null, new Operand [] { operand })
		{
			this.type = type;
		}

		private Type type;

		public Type SimpleBranchType
		{
			get
			{
				return this.type;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class ConditionCheck : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="ConditionCheck"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public ConditionCheck (RelationalType type, Register result, Register first, Register second)
			: base ("ConditionCheck", result, new Operand [] { first, second })
		{
			this.type = type;
		}

		private RelationalType type;

		public RelationalType RelationalType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			// TODO check the parameters

			this.def.InternalType = InternalType.I4;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Convert : Instruction {
		public enum Type {
			Conv_I,
			Conv_I1,
			Conv_I2,
			Conv_I4,
			Conv_I8,
			Conv_Ovf_I,
			Conv_Ovf_I_Un,
			Conv_Ovf_I1,
			Conv_Ovf_I1_Un,
			Conv_Ovf_I2,
			Conv_Ovf_I2_Un,
			Conv_Ovf_I4,
			Conv_Ovf_I4_Un,
			Conv_Ovf_I8,
			Conv_Ovf_I8_Un,
			Conv_Ovf_U,
			Conv_Ovf_U_Un,
			Conv_Ovf_U1,
			Conv_Ovf_U1_Un,
			Conv_Ovf_U2,
			Conv_Ovf_U2_Un,
			Conv_Ovf_U4,
			Conv_Ovf_U4_Un,
			Conv_Ovf_U8,
			Conv_Ovf_U8_Un,
			Conv_R_Un,
			Conv_R4,
			Conv_R8,
			Conv_U,
			Conv_U1,
			Conv_U2,
			Conv_U4,
			Conv_U8
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Convert"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Convert (Type type, Register result, Register value)
			: base (type.ToString (), result, new Operand [] { value })
		{
			this.type = type;

			result.InternalType = this.GetResultType (type);
		}

		private Type type;

		public Type ConvertType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public InternalType GetResultType (Type type)
		{
			switch (type) {

			case Type.Conv_I1:
			case Type.Conv_Ovf_I1:
			case Type.Conv_Ovf_I1_Un:
				return InternalType.I4; //I1;

			case Type.Conv_U1:
			case Type.Conv_Ovf_U1:
			case Type.Conv_Ovf_U1_Un:
				return InternalType.I4; //U1;

			case Type.Conv_I2:
			case Type.Conv_Ovf_I2:
			case Type.Conv_Ovf_I2_Un:
				return InternalType.I4; //I2;

			case Type.Conv_U2:
			case Type.Conv_Ovf_U2:
			case Type.Conv_Ovf_U2_Un:
				return InternalType.I4; //U2;

			case Type.Conv_I:
			case Type.Conv_Ovf_I:
			case Type.Conv_Ovf_I_Un:
				return InternalType.I;

			case Type.Conv_I4:
			case Type.Conv_Ovf_I4:
			case Type.Conv_Ovf_I4_Un:
				return InternalType.I4;

			case Type.Conv_U:
			case Type.Conv_Ovf_U:
			case Type.Conv_Ovf_U_Un:
				return InternalType.I; //U4;

			case Type.Conv_U4:
			case Type.Conv_Ovf_U4:
			case Type.Conv_Ovf_U4_Un:
				return InternalType.I4; //U4;

			case Type.Conv_I8:
			case Type.Conv_Ovf_I8:
			case Type.Conv_Ovf_I8_Un:
				return InternalType.I8;

			case Type.Conv_U8:
			case Type.Conv_Ovf_U8:
			case Type.Conv_Ovf_U8_Un:
				return InternalType.I8; //U8;

			case Type.Conv_R4:
			case Type.Conv_R_Un:
				return InternalType.F; //4;

			case Type.Conv_R8:
				return InternalType.F; //8;

			default:
				throw new NotImplementedEngineException ("'" + type + "' not supported.");
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Jump : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Jump"/> class.
		/// </summary>
		public Jump ()
			: base ("Jump", null, null)
		{
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Throw : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Throw"/> class.
		/// Call this in case it is a Rethrow
		/// </summary>
		public Throw ()
			: base ("Throw", null, null)
		{
		}

		public Throw (Register value)
			: base ("Throw", null, new Operand [] { value })
		{
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Return : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Return"/> class.
		/// Constructor for the case there is no return value
		/// </summary>
		public Return ()
			: base ("Return", null, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Return"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public Return (Register value)
			: base ("Return", null, new Operand [] { value })
		{
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Switch : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Switch"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="blocks">The blocks.</param>
		public Switch (Register value, Block [] blocks)
			: base ("Switch", null, new Operand [] { value })
		{
			this.blocks = blocks;
		}

		private Block [] blocks;

		public Block [] Blocks
		{
			get
			{
				return this.blocks;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Pop : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Pop"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public Pop (Register value)
			: base ("Pop", null, new Operand [] { value })
		{
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Dup : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Dup"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Dup (Register result, Register value)
			: base ("Dup", result, new Operand [] { value })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = this.use [0].InternalType;
			this.def.Type = this.use [0].Type;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Not : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Not"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Not (Register result, Register value)
			: base ("Not", result, new Operand [] { value })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = this.use [0].InternalType;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class And : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="And"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public And (Register result, Register first, Register second)
			: base ("And", result, new Operand [] { first, second })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = this.GetBitwiseResultType (this.use [0], this.use [1]);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Or : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Or"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public Or (Register result, Register first, Register second)
			: base ("Or", result, new Operand [] { first, second })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = this.GetBitwiseResultType (this.use [0], this.use [1]);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Xor : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Xor"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public Xor (Register result, Register first, Register second)
			: base ("Xor", result, new Operand [] { first, second })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = this.GetBitwiseResultType (this.use [0], this.use [1]);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Ldlen : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ldlen"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Ldlen (Register result, Register value)
			: base ("Ldlen", result, new Operand [] { value })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.U;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Ldnull : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ldnull"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		public Ldnull (Register result)
			: base ("Ldnull", result, new Operand [] { new NullConstant () })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.M;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Ldstr : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ldstr"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Ldstr (Register result, StringConstant value)
			: base ("Ldstr", result, new Operand [] { value })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.O;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Ldloc : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ldloc"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Ldloc (Register result, Local value)
			: base ("Ldloc", result, new Operand [] { value })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			base.LdProcess (method);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Stloc : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Stloc"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Stloc (Local result, Register value)
			: base ("Stloc", result, new Operand [] { value })
		{
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Ldloca : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ldloca"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Ldloca (Register result, Local value)
			: base ("Ldloca", result, new Operand [] { value })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.M;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Ldarg : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ldarg"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Ldarg (Register result, Argument value)
			: base ("Ldarg", result, new Operand [] { value })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			base.LdProcess (method);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Starg : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Starg"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Starg (Argument result, Register value)
			: base ("Starg", result, new Operand [] { value })
		{
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Ldelem : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ldelem"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public Ldelem (InternalType type, Register result, Register first, Register second)
			: base ("Ldelem", result, new Operand [] { first, second })
		{
			this.type = type;
			result.InternalType = this.AdjustRegisterInternalType (type);
		}

		InternalType type;

		/// <summary>
		/// Gets the type of the internal.
		/// </summary>
		/// <value>The type of the internal.</value>
		public InternalType InternalType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.Type = this.use [0].Type.SpecialTypeElement;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Stelem : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Stelem"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <param name="value">The value.</param>
		public Stelem (InternalType type, Register first, Register second, Register value)
			: base ("Stelem", null, new Operand [] { first, second, value })
		{
			this.type = type;
		}

		InternalType type;

		/// <summary>
		/// Gets the type of the internal.
		/// </summary>
		/// <value>The type of the internal.</value>
		public InternalType InternalType
		{
			get
			{
				return this.type;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Ldind : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ldind"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Ldind (InternalType type, Register result, Register value)
			: base ("Ldind", result, new Operand [] { value })
		{
			this.type = type;
			result.InternalType = this.AdjustRegisterInternalType (type);
		}

		InternalType type;

		/// <summary>
		/// Gets the type of the internal.
		/// </summary>
		/// <value>The type of the internal.</value>
		public InternalType InternalType
		{
			get
			{
				return this.type;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Stind : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Stind"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Stind (InternalType type, Register result, Register value)
			: base ("Stind", null, new Operand [] { result, value })
		{
			this.type = type;
		}

		InternalType type;

		/// <summary>
		/// Gets the type of the internal.
		/// </summary>
		/// <value>The type of the internal.</value>
		public InternalType InternalType
		{
			get
			{
				return this.type;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Localloc : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Localloc"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Localloc (Register result, Register value)
			: base ("Localloc", result, new Operand [] { value })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.M;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class SizeOf : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="SizeOf"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="typeReference">The type reference.</param>
		public SizeOf (Register result, TypeReference typeReference)
			: base ("SizeOf", result, null)
		{
			this.typeReference = typeReference;
		}

		private TypeReference typeReference;

		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <value>The type.</value>
		public TypeReference Type
		{
			get
			{
				return this.typeReference;
			}
		}

		/// <summary>
		/// Toes the string.
		/// </summary>
		/// <returns></returns>
		public override string ToString ()
		{
			return this.name + "(" + this.def.ToString () + "<=" + this.typeReference.ToString () + ")";
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.I4;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Ldtoken : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ldtoken"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Ldtoken (Register result, TokenConstant value)
			: base ("Ldtoken", result, new Operand [] { value })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.M;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Ldftn : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ldftn"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Ldftn (Register result, TokenConstant value)
			: base ("Ldftn", result, new Operand [] { value })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.M;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Ldfld : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ldfld"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Ldfld (Register result, FieldOperand value)
			: base ("Ldfld", result, new Operand [] { value })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			base.LdfldProcess (method);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Ldflda : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ldflda"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Ldflda (Register result, FieldOperand value)
			: base ("Ldflda", result, new Operand [] { value })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			base.LdfldProcess (method);

			this.def.InternalType = InternalType.M;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Ldsfld : Instruction {
		public Ldsfld (Register result, FieldOperand value)
			: base ("Ldsfld", result, new Operand [] { value })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			base.LdfldProcess (method);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Ldsflda : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ldsflda"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Ldsflda (Register result, FieldOperand value)
			: base ("Ldsflda", result, new Operand [] { value })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			base.LdfldProcess (method);

			this.def.InternalType = InternalType.M;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Stfld : Instruction {
		public Stfld (FieldOperand field, Register value)
			: base ("Stfld", null, new Operand [] { field, value })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			base.StfldProcess (method);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Stsfld : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Stsfld"/> class.
		/// </summary>
		/// <param name="field">The field.</param>
		/// <param name="value">The value.</param>
		public Stsfld (FieldOperand field, Register value)
			: base ("Stsfld", null, new Operand [] { field, value })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			base.StfldProcess (method);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Ldarga : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ldarga"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Ldarga (Register result, Argument value)
			: base ("Ldarga", result, new Operand [] { value })
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.M;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public abstract class CallInstruction : Instruction {
		public CallInstruction (Method method, string name, Operand def, Operand [] use)
			: base (name, def, use)
		{
			this.method = method;
		}

		protected Method method;

		public Method Method
		{
			get
			{
				return this.method;
			}
		}
	}

	// TODO add support for call/callvirt/calli/jmp
	/// <summary>
	/// 
	/// </summary>
	public class Call : CallInstruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Call"/> class.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <param name="result">The result.</param>
		/// <param name="parameters">The parameters.</param>
		public Call (Method method, Register result, Operand [] parameters)
			: base (method, "Call " + method.MethodFullName + " ", result, parameters)
		{
		}


		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			if (this.def != null) {
				this.def.InternalType = this.AdjustRegisterInternalType (method.Engine.GetInternalType (Class.GetTypeFullName (this.method.MethodDefinition.ReturnType.ReturnType)));

				if (this.def.InternalType == InternalType.ValueType)
					this.def.Type = method.Engine.GetClass (this.method.MethodDefinition.ReturnType.ReturnType);
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Callvirt : Call {
		/// <summary>
		/// Initializes a new instance of the <see cref="Callvirt"/> class.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <param name="result">The result.</param>
		/// <param name="parameters">The parameters.</param>
		public Callvirt (Method method, Register result, Operand [] parameters)
			: base (method, result, parameters)
		{
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Newobj : CallInstruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Newobj"/> class.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <param name="result">The result.</param>
		/// <param name="parameters">The parameters.</param>
		public Newobj (Method method, Register result, Operand [] parameters)
			: base (method, "Newobj " + method.MethodFullName + " ", result, parameters)
		{
			result.InternalType = InternalType.O;
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			if (this.method.Class.IsValueType) {
				this.def.InternalType = InternalType.ValueType;
				this.def.Type = this.method.Class;

			} else
				this.def.InternalType = InternalType.O;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Ldobj : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ldobj"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="type">The type.</param>
		/// <param name="instance">The instance.</param>
		public Ldobj (Register result, Class type, Register instance)
			: base ("Ldobj", result, new Operand [] { instance })
		{
			result.InternalType = InternalType.ValueType;
			result.Type = type;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Stobj : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Stobj"/> class.
		/// </summary>
		/// <param name="typeReference">The type reference.</param>
		/// <param name="instance">The instance.</param>
		/// <param name="value">The value.</param>
		public Stobj (TypeReference typeReference, Register instance, Register value)
			: base ("Stobj", null, new Operand [] { instance, value })
		{
			this.typeReference = typeReference;
		}

		TypeReference typeReference;

		public TypeReference Type
		{
			get
			{
				return this.typeReference;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Initobj : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Initobj"/> class.
		/// </summary>
		/// <param name="typeReference">The type reference.</param>
		/// <param name="instance">The instance.</param>
		public Initobj (TypeReference typeReference, Register instance)
			: base ("Initobj", null, new Operand [] { instance })
		{
			this.typeReference = typeReference;
		}

		TypeReference typeReference;

		public TypeReference Type
		{
			get
			{
				return this.typeReference;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Ldc : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ldc"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Ldc (Register result, Constant value)
			: base ("Ldc", result, new Operand [] { value })
		{
			result.InternalType = value.InternalType;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class PHI : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="PHI"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="operands">The operands.</param>
		public PHI (Register result, Operand [] operands)
			: base ("PHI", result, operands)
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			InternalType type = this.use [0].InternalType;
			int index = (this.def as Register).Index;

			for (int i = 0; i < this.use.Length; i++) {
				if (type != this.use [i].InternalType)
					throw new EngineException (string.Format ("The PHI operands have not the same type. ({0})", method.MethodFullName));

				if (index != (this.use [i] as Register).Index)
					throw new EngineException (string.Format ("The PHI operands have not the same index. ({0})", method.MethodFullName));
			}

			this.def.InternalType = type;
		}

		/// <summary>
		/// Attaches this instance.
		/// </summary>
		public void Attach ()
		{
			foreach (Register register in this.use)
				register.PHI = this.def as Register;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Initialize : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Initialize"/> class.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="typeReference">The type reference.</param>
		public Initialize (Local source, TypeReference typeReference)
			: base ("Initialize", null, new Operand [] { source })
		{
			this.typeReference = typeReference;
		}

		TypeReference typeReference;
	}

	/// <summary>
	/// 
	/// </summary>
	public class Box : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Box"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Box (Class type, Register result, Register value)
			: base ("Box", result, new Operand [] { value })
		{
			this.type = type;

			result.InternalType = InternalType.O;
		}

		Class type;

		public Class Type
		{
			get
			{
				return this.type;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Unbox : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Unbox"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="instance">The instance.</param>
		public Unbox (Class type, Register result, Register instance)
			: base ("Unbox", result, new Operand [] { instance })
		{
			this.type = type;

			result.InternalType = InternalType.I;
		}

		Class type;

		public Class Type
		{
			get
			{
				return this.type;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class UnboxAny : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="UnboxAny"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="instance">The instance.</param>
		public UnboxAny (Class type, Register result, Register instance)
			: base ("UnboxAny", result, new Operand [] { instance })
		{
			this.type = type;
		}

		Class type;

		public Class Type
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			if (this.type.ClassDefinition.IsValueType) {
				this.def.InternalType = method.Class.Engine.GetInternalType (this.type.TypeFullName);
				this.def.Type = this.type;

			} else
				throw new NotImplementedEngineException ();
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Newarr : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Newarr"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="instance">The instance.</param>
		public Newarr (Class type, Register result, Register instance)
			: base ("Newarr", result, new Operand [] { instance })
		{
			this.type = type;
		}

		Class type;

		public Class Type
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.SZArray;
			this.def.Type = this.type;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Isinst : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Isinst"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="instance">The instance.</param>
		public Isinst (Class type, Register result, Register instance)
			: base ("Isinst", result, new Operand [] { instance })
		{
			this.type = type;
		}

		Class type;

		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <value>The type.</value>
		public Class Type
		{
			get
			{
				return this.type;
			}
		}

		public override void Process (Method method)
		{
			if (this.type.IsClass) {
				this.def.InternalType = InternalType.O;
				this.def.Type = type;

			} else
				throw new NotImplementedEngineException ();
		}
	}
}