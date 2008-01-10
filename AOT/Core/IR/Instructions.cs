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

		public virtual void Process (Method method)
		{
			return;
		}

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

			case InternalType.M:
				result = InternalType.M;
				break;
			}

			return result;
		}

		public void LdProcess (Method method)
		{
			this.def.InternalType = this.AdjustRegisterInternalType (this.use [0].InternalType);

			if (this.def.InternalType == InternalType.ValueType)
				(this.def as Identifier).Type = (this.use [0] as Identifier).Type;
		}

		public void LdfldProcess (Method method)
		{
			FieldOperand field = (this.use [0] as FieldOperand);

			string type = field.Field.Type.FieldType.ToString ();

			field.InternalType = method.Engine.GetInternalType (type);

			this.def.InternalType = this.AdjustRegisterInternalType (field.InternalType);

			if (this.def.InternalType == InternalType.ValueType)
				(this.def as Identifier).Type = field.Field.Type;
		}

		public void StfldProcess (Method method)
		{
			FieldOperand field = (this.use [0] as FieldOperand);

			string type = field.Field.Type.FieldType.ToString ();

			field.InternalType = method.Engine.GetInternalType (type);
		}
	}

	public class Add : Instruction {
		public enum Type {
			Add,
			AddSignedWithOverflowCheck,
			AddUnsignedWithOverflowCheck
		}

		public Add (Type type, Register result, Register first, Register second)
			: base ("Add", result, new Operand [] { first, second })
		{
			this.type = type;
		}

		private Type type;

		public Type AddType
		{
			get
			{
				return this.type;
			}
		}

		public override void Process (Method method)
		{
			this.def.InternalType = this.GetArithmeticalResultType (this.use [0], this.use [1], true, false, false, this.type != Type.Add);
		}
	}

	public class Sub : Instruction {
		public enum Type {
			Sub,
			SubSignedWithOverflowCheck,
			SubUnsignedWithOverflowCheck
		}

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

		public override void Process (Method method)
		{
			this.def.InternalType = this.GetArithmeticalResultType (this.use [0], this.use [1], false, true, false, this.type != Type.Sub);
		}
	}

	public class Mul : Instruction {
		public enum Type {
			Mul,
			MulSignedWithOverflowCheck,
			MulUnsignedWithOverflowCheck
		}

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

		public override void Process (Method method)
		{
			this.def.InternalType = this.GetArithmeticalResultType (this.use [0], this.use [1], false, false, false, this.type != Type.Mul);
		}
	}

	public class Div : Instruction {
		public enum Type {
			Div,
			DivUnsigned
		}

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

		public override void Process (Method method)
		{
			this.def.InternalType = this.GetArithmeticalResultType (this.use [0], this.use [1], false, false, this.type == Type.Div, false);
		}
	}

	public class Rem : Instruction {
		public enum Type {
			Remainder,
			RemainderUnsigned
		}

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

		public override void Process (Method method)
		{
			this.def.InternalType = this.GetArithmeticalResultType (this.use [0], this.use [1], false, false, false, false);
		}
	}

	public class Neg : Instruction {
		public Neg (Register result, Register value)
			: base ("Neg", result, new Operand [] { value })
		{
			result.InternalType = value.InternalType;
		}

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

	public class Shl : Instruction {
		public Shl (Register result, Register first, Register second)
			: base ("Shl", result, new Operand [] { first, second })
		{
		}

		public override void Process (Method method)
		{
			this.def.InternalType = this.use [0].InternalType;
		}
	}

	public class Shr : Instruction {
		public enum Type {
			SHR,
			SHRUnsigned
		}

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

	public class Branch : Instruction {
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

	public class SimpleBranch : Instruction {
		public enum Type {
			True,
			False
		}

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

	public class ConditionCheck : Instruction {
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

		public override void Process (Method method)
		{
			// TODO check the parameters

			this.def.InternalType = InternalType.I4;
		}
	}

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

	public class Jump : Instruction {
		public Jump ()
			: base ("Jump", null, null)
		{
		}
	}

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

	public class Return : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Return"/> class.
		/// Constructor for the case there is no return value
		/// </summary>
		public Return ()
			: base ("Return", null, null)
		{
		}

		public Return (Register value)
			: base ("Return", null, new Operand [] { value })
		{
		}
	}

	public class Switch : Instruction {
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

	public class Pop : Instruction {
		public Pop (Register value)
			: base ("Pop", null, new Operand [] { value })
		{
		}
	}

	public class Dup : Instruction {
		public Dup (Register result, Register value)
			: base ("Dup", result, new Operand [] { value })
		{
		}

		public override void Process (Method method)
		{
			this.def.InternalType = this.use [0].InternalType;
			(this.def as Register).Type = (this.use [0] as Register).Type;
		}
	}

	public class Not : Instruction {
		public Not (Register result, Register value)
			: base ("Not", result, new Operand [] { value })
		{
		}

		public override void Process (Method method)
		{
			this.def.InternalType = this.use [0].InternalType;
		}
	}

	public class And : Instruction {
		public And (Register result, Register first, Register second)
			: base ("And", result, new Operand [] { first, second })
		{
		}

		public override void Process (Method method)
		{
			this.def.InternalType = this.GetBitwiseResultType (this.use [0], this.use [1]);
		}
	}

	public class Or : Instruction {
		public Or (Register result, Register first, Register second)
			: base ("Or", result, new Operand [] { first, second })
		{
		}

		public override void Process (Method method)
		{
			this.def.InternalType = this.GetBitwiseResultType (this.use [0], this.use [1]);
		}
	}

	public class Xor : Instruction {
		public Xor (Register result, Register first, Register second)
			: base ("Xor", result, new Operand [] { first, second })
		{
		}

		public override void Process (Method method)
		{
			this.def.InternalType = this.GetBitwiseResultType (this.use [0], this.use [1]);
		}
	}

	public class Ldlen : Instruction {
		public Ldlen (Register result, Register value)
			: base ("Ldlen", result, new Operand [] { value })
		{
		}

		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.U;
		}
	}

	public class Ldnull : Instruction {
		public Ldnull (Register result)
			: base ("Ldnull", result, new Operand [] { new NullConstant () })
		{
		}

		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.M;
		}
	}

	public class Ldstr : Instruction {
		public Ldstr (Register result, StringConstant value)
			: base ("Ldstr", result, new Operand [] { value })
		{
		}

		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.O;
		}
	}

	public class Ldloc : Instruction {
		public Ldloc (Register result, Local value)
			: base ("Ldloc", result, new Operand [] { value })
		{
		}

		public override void Process (Method method)
		{
			base.LdProcess (method);
		}
	}

	public class Stloc : Instruction {
		public Stloc (Local result, Register value)
			: base ("Stloc", result, new Operand [] { value })
		{
		}
	}

	public class Ldloca : Instruction {
		public Ldloca (Register result, Local value)
			: base ("Ldloca", result, new Operand [] { value })
		{
		}

		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.M;
		}
	}

	public class Ldarg : Instruction {
		public Ldarg (Register result, Argument value)
			: base ("Ldarg", result, new Operand [] { value })
		{
		}

		public override void Process (Method method)
		{
			base.LdProcess (method);
		}
	}

	public class Starg : Instruction {
		public Starg (Argument result, Register value)
			: base ("Starg", result, new Operand [] { value })
		{
		}
	}

	public class Ldelem : Instruction {
		public Ldelem (InternalType type, Register result, Register first, Register second)
			: base ("Ldelem", result, new Operand [] { first, second })
		{
			this.type = type;
			result.InternalType = type;
		}

		InternalType type;

		public InternalType InternalType
		{
			get
			{
				return this.type;
			}
		}
	}

	public class Stelem : Instruction {
		public Stelem (InternalType type, Register first, Register second, Register value)
			: base ("Stelem", null, new Operand [] { first, second, value })
		{
			this.type = type;
		}

		InternalType type;

		public InternalType InternalType
		{
			get
			{
				return this.type;
			}
		}
	}

	public class Ldind : Instruction {
		public Ldind (InternalType type, Register result, Register value)
			: base ("Ldind", result, new Operand [] { value })
		{
			this.type = type;
			result.InternalType = this.AdjustRegisterInternalType (type);
		}

		InternalType type;

		public InternalType InternalType
		{
			get
			{
				return this.type;
			}
		}
	}

	public class Stind : Instruction {
		public Stind (InternalType type, Register result, Register value)
			: base ("Stind", null, new Operand [] { result, value })
		{
			this.type = type;
		}

		InternalType type;

		public InternalType InternalType
		{
			get
			{
				return this.type;
			}
		}
	}

	public class Localloc : Instruction {
		public Localloc (Register result, Register value)
			: base ("Localloc", result, new Operand [] { value })
		{
		}

		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.M;
		}
	}

	public class SizeOf : Instruction {
		public SizeOf (Register result, TypeReference typeReference)
			: base ("SizeOf", result, null)
		{
			this.typeReference = typeReference;
		}

		private TypeReference typeReference;

		public TypeReference Type
		{
			get
			{
				return this.typeReference;
			}
		}

		public override string ToString ()
		{
			return this.name + "(" + this.def.ToString () + "<=" + this.typeReference.ToString () + ")";
		}

		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.I4;
		}
	}

	public class Ldtoken : Instruction {
		public Ldtoken (Register result, TokenConstant value)
			: base ("Ldtoken", result, new Operand [] { value })
		{
		}

		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.M;
		}
	}

	public class Ldftn : Instruction {
		public Ldftn (Register result, TokenConstant value)
			: base ("Ldftn", result, new Operand [] { value })
		{
		}

		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.M;
		}
	}

	public class Ldfld : Instruction {
		public Ldfld (Register result, FieldOperand value)
			: base ("Ldfld", result, new Operand [] { value })
		{
		}

		public override void Process (Method method)
		{
			base.LdfldProcess (method);
		}
	}

	public class Ldflda : Instruction {
		public Ldflda (Register result, FieldOperand value)
			: base ("Ldflda", result, new Operand [] { value })
		{
		}

		public override void Process (Method method)
		{
			base.LdfldProcess (method);

			this.def.InternalType = InternalType.M;
		}
	}

	public class Ldsfld : Instruction {
		public Ldsfld (Register result, FieldOperand value)
			: base ("Ldsfld", result, new Operand [] { value })
		{
		}

		public override void Process (Method method)
		{
			base.LdfldProcess (method);
		}
	}

	public class Ldsflda : Instruction {
		public Ldsflda (Register result, FieldOperand value)
			: base ("Ldsflda", result, new Operand [] { value })
		{
		}

		public override void Process (Method method)
		{
			base.LdfldProcess (method);

			this.def.InternalType = InternalType.M;
		}
	}

	public class Stfld : Instruction {
		public Stfld (FieldOperand field, Register value)
			: base ("Stfld", null, new Operand [] { field, value })
		{
		}

		public override void Process (Method method)
		{
			base.StfldProcess (method);
		}
	}

	public class Stsfld : Instruction {
		public Stsfld (FieldOperand field, Register value)
			: base ("Stsfld", null, new Operand [] { field, value })
		{
		}

		public override void Process (Method method)
		{
			base.StfldProcess (method);
		}
	}

	public class Ldarga : Instruction {
		public Ldarga (Register result, Argument value)
			: base ("Ldarga", result, new Operand [] { value })
		{
		}

		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.M;
		}
	}

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
	public class Call : CallInstruction {
		public Call (Method method, Register result, Operand [] parameters)
			: base (method, "Call " + method.MethodDefinition.ToString () + " ", result, parameters)
		{
		}


		public override void Process (Method method)
		{
			if (this.def != null) {
				this.def.InternalType = this.AdjustRegisterInternalType (method.Engine.GetInternalType (Class.GetTypeFullName (this.method.MethodDefinition.ReturnType.ReturnType)));

				if (this.def.InternalType == InternalType.ValueType)
					(this.def as Register).Type = this.method.MethodDefinition.ReturnType.ReturnType;
			}
		}
	}

	public class Callvirt : Call {
		public Callvirt (Method method, Register result, Operand [] parameters)
			: base (method, result, parameters)
		{
		}
	}

	public class Newobj : CallInstruction {
		public Newobj (Method method, Register result, Operand [] parameters)
			: base (method, "Newobj " + method.MethodDefinition.ToString () + " ", result, parameters)
		{
			result.InternalType = InternalType.O;
		}

		public override void Process (Method method)
		{
			if (this.method.MethodDefinition.DeclaringType.IsValueType) {
				this.def.InternalType = InternalType.ValueType;
				(this.def as IR.Operands.Register).Type = this.method.MethodDefinition.DeclaringType;

			} else
				this.def.InternalType = InternalType.O;
		}
	}

	public class Ldobj : Instruction {
		public Ldobj (Register result, TypeReference typeReference, Register instance)
			: base ("Ldobj", result, new Operand [] { instance })
		{
			result.InternalType = InternalType.ValueType;
			result.Type = typeReference;
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

	public class Stobj : Instruction {
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

	public class Initobj : Instruction {
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

	public class Ldc : Instruction {
		public Ldc (Register result, Constant value)
			: base ("Ldc", result, new Operand [] { value })
		{
			result.InternalType = value.InternalType;
		}
	}

	public class PHI : Instruction {
		public PHI (Register result, Operand [] operands)
			: base ("PHI", result, operands)
		{
		}

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

		public void Attach ()
		{
			foreach (Register register in this.use)
				register.PHI = this.def as Register;
		}
	}

	public class Initialize : Instruction {
		public Initialize (Local source, TypeReference typeReference)
			: base ("Initialize", null, new Operand [] { source })
		{
			this.typeReference = typeReference;
		}

		TypeReference typeReference;
	}

	public class Box : Instruction {
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

	public class Unbox : Instruction {
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

	public class UnboxAny : Instruction {
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

		public override void Process (Method method)
		{
			if (this.type.ClassDefinition.IsValueType) {
				this.def.InternalType = method.Class.Engine.GetInternalType (this.type.TypeFullName);
				(this.def as Identifier).Type = this.type.ClassDefinition;

			} else
				throw new NotImplementedEngineException ();
		}
	}
}