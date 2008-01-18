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
	/// <summary>
	/// 
	/// </summary>
	public enum InternalType {
		/// <summary>
		/// 
		/// </summary>
		NotSet,
		/// <summary>
		/// 
		/// </summary>
		I1,
		/// <summary>
		/// 
		/// </summary>
		U1,

		/// <summary>
		/// 
		/// </summary>
		I2,
		/// <summary>
		/// 
		/// </summary>
		U2,

		/// <summary>
		/// 
		/// </summary>
		I4,
		/// <summary>
		/// 
		/// </summary>
		U4,
		/// <summary>
		/// 
		/// </summary>
		I,
		/// <summary>
		/// 
		/// </summary>
		U,

		/// <summary>
		/// 
		/// </summary>
		I8,
		/// <summary>
		/// 
		/// </summary>
		U8,

		/// <summary>
		/// 
		/// </summary>
		R4,
		/// <summary>
		/// 
		/// </summary>
		R8,
		/// <summary>
		/// 
		/// </summary>
		F,

		/// <summary>
		/// 
		/// </summary>
		ValueType,
		/// <summary>
		/// 
		/// </summary>
		O,
		/// <summary>
		/// 
		/// </summary>
		M,
		/// <summary>
		/// 
		/// </summary>
		TypedReference,
		/// <summary>
		/// 
		/// </summary>
		Array,
		/// <summary>
		/// 
		/// </summary>
		SZArray
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

		/// <summary>
		/// Gets or sets the type of the internal.
		/// </summary>
		/// <value>The type of the internal.</value>
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

		protected Class type = null;

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
		public Class Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}
		
		/// <summary>
		/// Gets the non register.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="type">The type.</param>
		/// <returns></returns>
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

	/// <summary>
	/// 
	/// </summary>
	public class NullConstant : Constant {
		/// <summary>
		/// Initializes a new instance of the <see cref="NullConstant"/> class.
		/// </summary>
		public NullConstant ()
		{
			this.InternalType = InternalType.I;
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>The value.</value>
		public int Value
		{
			get
			{
				return 0;
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
			return "null";
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class IntConstant : Constant {
		/// <summary>
		/// Initializes a new instance of the <see cref="IntConstant"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
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

	/// <summary>
	/// 
	/// </summary>
	public class LongConstant : Constant {
		/// <summary>
		/// Initializes a new instance of the <see cref="LongConstant"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
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

	/// <summary>
	/// 
	/// </summary>
	public class FloatConstant : Constant {
		/// <summary>
		/// Initializes a new instance of the <see cref="FloatConstant"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
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

	/// <summary>
	/// 
	/// </summary>
	public class DoubleConstant : Constant {
		/// <summary>
		/// Initializes a new instance of the <see cref="DoubleConstant"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
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

	/// <summary>
	/// 
	/// </summary>
	public class StringConstant : Constant {
		/// <summary>
		/// Initializes a new instance of the <see cref="StringConstant"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
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

	/// <summary>
	/// 
	/// </summary>
	public class TokenConstant : Constant {
		/// <summary>
		/// Initializes a new instance of the <see cref="TokenConstant"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
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
		/// <summary>
		/// Initializes a new instance of the <see cref="Identifier"/> class.
		/// </summary>
		/// <param name="typeName">Name of the type.</param>
		/// <param name="index">The index.</param>
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

		protected bool forceSpill = false;

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

		/// <summary>
		/// Gets the ID.
		/// </summary>
		/// <value>The ID.</value>
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

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		public override string ToString ()
		{
			string result = this.ID;

			if (this.InternalType != InternalType.NotSet)
				result += "__" + this.InternalType.ToString ();

			return result;
		}

		/// <summary>
		/// Gets the full name of the type.
		/// </summary>
		/// <value>The full name of the type.</value>
		public string TypeFullName {
			get {
				return this.Type.TypeFullName;
			}
		}
	}

	/// <summary>
	/// .NET Stack Register.
	/// </summary>
	public class Register : Identifier {
		/// <summary>
		/// Initializes a new instance of the <see cref="Register"/> class.
		/// </summary>
		/// <param name="index">The index.</param>
		public Register (int index)
			: base ("Reg", index)
		{
		}

		private Register phi = null;

		/// <summary>
		/// Gets or sets the PHI.
		/// </summary>
		/// <value>The PHI.</value>
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

		/// <summary>
		/// Gets or sets the parent.
		/// </summary>
		/// <value>The parent.</value>
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

		/// <summary>
		/// Toes the string.
		/// </summary>
		/// <returns></returns>
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
		/// <summary>
		/// Initializes a new instance of the <see cref="Local"/> class.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="type">The type.</param>
		/// <param name="internalType">Type of the internal.</param>
		public Local (int index, Class type, InternalType internalType)
			: base ("Loc", index)
		{
			this.type = type;
			this.internalType = internalType;
			this.forceSpill = true;
		}
	}

	/// <summary>
	/// Method Argument
	/// </summary>
	public class Argument : Identifier {
		/// <summary>
		/// Initializes a new instance of the <see cref="Argument"/> class.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="type">The type.</param>
		/// <param name="internalType">Type of the internal.</param>
		public Argument (int index, Class type, InternalType internalType)
			: base ("Arg", index)
		{
			this.type = type;
			this.internalType = internalType;
			this.forceSpill = true;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Field: Operand {
		/// <summary>
		/// Initializes a new instance of the <see cref="Field"/> class.
		/// </summary>
		/// <param name="fieldDefinition">The field definition.</param>
		public Field (FieldDefinition fieldDefinition, Class type, InternalType internalType)
		{
			this.type = type;
			this.internalType = internalType;
			this.fieldDefinition = fieldDefinition;
		}

		FieldDefinition fieldDefinition;

		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <value>The type.</value>
		public FieldDefinition FieldDefinition
		{
			get
			{
				return this.fieldDefinition;
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
				return this.fieldDefinition.Name;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is static.
		/// </summary>
		/// <value><c>true</c> if this instance is static; otherwise, <c>false</c>.</value>
		public bool IsStatic
		{
			get
			{
				return this.fieldDefinition.IsStatic;
			}
		}

		/// <summary>
		/// Gets the offset.
		/// </summary>
		/// <value>The offset.</value>
		public uint Offset
		{
			get
			{
				return this.fieldDefinition.Offset;
			}
		}

		/// <summary>
		/// Gets or sets the type of the internal.
		/// </summary>
		/// <value>The type of the internal.</value>
		public override InternalType InternalType
		{
			get
			{
				return this.internalType;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class FieldOperand : Operand {
		/// <summary>
		/// Initializes a new instance of the <see cref="FieldOperand"/> class.
		/// </summary>
		/// <param name="field">The field.</param>
		/// <param name="instance">The instance.</param>
		public FieldOperand (Field field, Operand instance)
		{
			this.field = field;
			this.instance = instance;
		}

		public FieldOperand (Field field)
		{
			this.field = field;
		}

		private Field field = null;

		/// <summary>
		/// Gets the field.
		/// </summary>
		/// <value>The field.</value>
		public Field Field
		{
			get
			{
				return this.field;
			}
		}

		/// <summary>
		/// Gets the short name of the field type.
		/// </summary>
		/// <value>The short name of the field type.</value>
		public string ShortFieldTypeName
		{
			get
			{
				return this.field.FieldDefinition.FieldType.Name;
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

			if (this.instance != null)
				result += this.instance.ToString () + "->";

			result += this.field.FieldDefinition.ToString ();

			return result;
		}

		Operand instance = null;

		/// <summary>
		/// Gets or sets the instance.
		/// </summary>
		/// <value>The instance.</value>
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

		/// <summary>
		/// Gets the full name of the type.
		/// </summary>
		/// <value>The full name of the type.</value>
		public string TypeFullName
		{
			get
			{
				return Class.GetTypeFullName (this.field.FieldDefinition);
			}
		}

		public override InternalType InternalType
		{
			get
			{
				return this.field.InternalType;
			}
		}
	}

	#endregion

	#endregion
}