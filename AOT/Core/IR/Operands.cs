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
using Mono.Cecil;


namespace SharpOS.AOT.IR.Operands {
	public enum InternalType {
		NotSet,
		I1,
		U1,

		I2,
		U2,

		I4,
		U4,
		I,
		U,

		I8,
		U8,

		R4,
		R8,
		F,

		ValueType,
		O,
		M,
		TypedReference
	}

	#region Operands

	/// <summary>
	/// Instruction operand.
	/// </summary>
	public abstract class Operand {

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Operand"/> class.
		/// </summary>
		public Operand ()
		{
		}

		#endregion

		protected InternalType internalType = InternalType.NotSet;

		public virtual InternalType InternalType
		{
			get
			{
				return this.internalType;
			}
			set
			{
				this.internalType = value;
			}
		}

		public static Operand GetNonRegister (Operand value, Type type)
		{
			Operand operand = value;

			while (operand is IR.Operands.Register) {
				IR.Operands.Register register = operand as IR.Operands.Register;

				if (register.Parent.Use.Length != 1)
					throw new EngineException (string.Format ("Could not propagate '{0}'.", value.ToString ()));

				operand = register.Parent.Use [0];
			}

			if (operand is NullConstant)
				return null;

			if (operand.GetType () != type)
				throw new EngineException (string.Format ("'{0}' expected but '{1}' found.", type.ToString (), operand.GetType ().ToString ()));

			return operand;
		}


	}

	#region Constants
	/// <summary>
	/// Constant Value
	/// </summary>
	public abstract class Constant : Operand {
	}

	public class NullConstant : Constant {
		public NullConstant ()
		{
			this.InternalType = InternalType.I;
		}

		public int Value
		{
			get
			{
				return 0;
			}
		}

		public override string ToString ()
		{
			return "null";
		}
	}

	public class IntConstant : Constant {
		public IntConstant (int value)
		{
			this.value = value;
			this.InternalType = InternalType.I4;
		}

		private int value;

		public int Value
		{
			get
			{
				return this.value;
			}
		}

		public override string ToString ()
		{
			return this.value.ToString ();
		}
	}

	public class LongConstant : Constant {
		public LongConstant (long value)
		{
			this.value = value;
			this.InternalType = InternalType.I8;
		}

		private long value;

		public long Value
		{
			get
			{
				return this.value;
			}
		}

		public override string ToString ()
		{
			return this.value.ToString ();
		}
	}

	public class FloatConstant : Constant {
		public FloatConstant (float value)
		{
			this.value = value;
			this.InternalType = InternalType.F;
		}

		private float value;

		public float Value
		{
			get
			{
				return this.value;
			}
		}

		public override string ToString ()
		{
			return this.value.ToString ();
		}
	}

	public class DoubleConstant : Constant {
		public DoubleConstant (double value)
		{
			this.value = value;
			this.InternalType = InternalType.F;
		}

		private double value;

		public double Value
		{
			get
			{
				return this.value;
			}
		}

		public override string ToString ()
		{
			return this.value.ToString ();
		}
	}

	public class StringConstant : Constant {
		public StringConstant (string value)
		{
			this.value = value;
			this.internalType = InternalType.O;
		}

		private string value;

		public string Value
		{
			get
			{
				return this.value;
			}
		}

		public override string ToString ()
		{
			return "\"" + this.value + "\"";
		}
	}

	public class TokenConstant : Constant {
		public TokenConstant (string value)
		{
			this.value = value;
		}

		private string value;

		public override string ToString ()
		{
			return "\"" + this.value + "\"";
		}
	}
	#endregion

	#region Identifiers
	/// <summary>
	/// Base class for Arguments, Local Variables...
	/// </summary>
	public abstract class Identifier : Operand {
		public Identifier (string typeName, int index)
		{
			this.index = index;
			this.typeName = typeName;
		}

		private string typeName = string.Empty;

		private int index = 0;

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

		private int version = 0;

		/// <summary>
		/// Gets or sets the SSA version.
		/// </summary>
		/// <value>The version.</value>
		public int Version
		{
			get
			{
				return version;
			}
			set
			{
				version = value;
			}
		}

		private bool forceSpill = false;

		/// <summary>
		/// If set, the Register Allocation doesn't allocate a register for this identifier.
		/// </summary>
		/// <value>Allow this identifier to get a register.</value>
		public bool ForceSpill
		{
			get
			{
				return forceSpill;
			}
			set
			{
				forceSpill = value;
			}
		}

		private int register = int.MinValue;

		/// <summary>
		/// Gets or sets the register.
		/// </summary>
		/// <value>The register.</value>
		public virtual int Register
		{
			get
			{
				return register;
			}
			set
			{
				register = value;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is register set.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is register set; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsRegisterSet
		{
			get
			{
				return register != int.MinValue;
			}
		}

		private int stack = int.MinValue;

		/// <summary>
		/// Gets or sets the stack.
		/// </summary>
		/// <value>The stack.</value>
		public virtual int Stack
		{
			get
			{
				return stack;
			}
			set
			{
				stack = value;
			}
		}

		public string ID
		{
			get
			{
				string result = this.typeName;

				result += this.index.ToString ();

				result += "_";

				result += this.version.ToString ();

				return result;
			}
		}

		public override string ToString ()
		{
			string result = this.ID;

			if (this.InternalType != InternalType.NotSet)
				result += "__" + this.InternalType.ToString ();

			return result;
		}

		protected MemberReference type = null;

		public MemberReference Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}
	}

	/// <summary>
	/// .NET Stack Register.
	/// </summary>
	public class Register : Identifier {
		public Register (int index)
			: base ("Reg", index)
		{
		}

		private Register phi = null;

		public Register PHI
		{
			get
			{
				return phi;
			}
			set
			{
				phi = value;
			}
		}

		Instructions.Instruction parent = null;

		public Instructions.Instruction Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		public override string ToString ()
		{
			string suffix = "";

			if (this.phi != null)
				suffix = "{" + this.phi + "}";

			return base.ToString () + suffix;
		}
	}

	/// <summary>
	/// Local Variable
	/// </summary>
	public class Local : Identifier {
		public Local (int index, TypeReference type)
			: base ("Loc", index)
		{
			this.type = type;
			this.ForceSpill = true;
		}
	}

	/// <summary>
	/// Method Argument
	/// </summary>
	public class Argument : Identifier {
		public Argument (int index, TypeReference type)
			: base ("Arg", index)
		{
			this.type = type;
			this.ForceSpill = true;
		}
	}

	public class Field : Operand {
		public Field (FieldReference type, Operand instance)
		{
			this.type = type;
			this.instance = instance;
		}

		public Field (FieldReference type)
		{
			this.type = type;
		}

		private FieldReference type = null;

		public FieldReference Type
		{
			get
			{
				return this.type;
			}
		}

		public string ShortFieldTypeName
		{
			get
			{
				return this.type.FieldType.Name;
			}
		}

		public override string ToString ()
		{
			string result = "";

			if (this.instance != null)
				result += this.instance.ToString () + "->";

			result += this.type.ToString ();

			return result;
		}

		Operand instance = null;

		public Operand Instance
		{
			get
			{
				return instance;
			}
			set
			{
				instance = value;
			}
		}
	}

	#endregion

	#endregion
}