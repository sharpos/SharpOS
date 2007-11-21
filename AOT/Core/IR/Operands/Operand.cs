// 
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using SharpOS.AOT.IR.Operators;
using Mono.Cecil;

namespace SharpOS.AOT.IR.Operands {
	/// <summary>
	/// </summary>
	[Serializable]	
	public abstract class Operand {
		/// <summary>
		/// </summary>
		public enum ConvertType {
			NotSet, 
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
		/// </summary>
		public enum InternalSizeType {
			NotSet, 
			I, 
			U, 
			I1, 
			U1, 
			I2, 
			U2, 
			I4, 
			U4, 
			I8, 
			U8, 
			R4, 
			R8, 
			ValueType,
			S
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Operand"/> class.
		/// </summary>
		public Operand ()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Operand"/> class.
		/// </summary>
		/// <param name="_operator">The _operator.</param>
		/// <param name="operands">The operands.</param>
		public Operand (Operator _operator, Operand [] operands)
		{
			this._operator = _operator;
			this.operands = operands;
		}

		private int register = int.MinValue;

		/// <summary>
		/// Gets or sets the register.
		/// </summary>
		/// <value>The register.</value>
		public virtual int Register {
			get {
				return register;
			}
			set {
				register = value;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is register set.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is register set; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsRegisterSet {
			get {
				return register != int.MinValue;
			}
		}

		private int stack = int.MinValue;

		/// <summary>
		/// Gets or sets the stack.
		/// </summary>
		/// <value>The stack.</value>
		public virtual int Stack {
			get {
				return stack;
			}
			set {
				stack = value;
			}
		}

		private ConvertType convertTo = ConvertType.NotSet;

		/// <summary>
		/// Gets or sets the convert to.
		/// </summary>
		/// <value>The convert to.</value>
		public ConvertType ConvertTo {
			get {
				return convertTo;
			}
			set {
				convertTo = value;
			}
		}

		private InternalSizeType sizeType = InternalSizeType.NotSet;

		/// <summary>
		/// Gets or sets the type of the size.
		/// </summary>
		/// <value>The type of the size.</value>
		public InternalSizeType SizeType {
			get {
				return sizeType;
			}
			set {
				sizeType = value;
			}
		}

		/// <summary>
		/// Gets the type of the convert size.
		/// </summary>
		/// <value>The type of the convert size.</value>
		public InternalSizeType ConvertSizeType {
			get {
				return Operand.GetType (this.convertTo);
			}
		}


		private Operator _operator = null;

		/// <summary>
		/// Gets the operator.
		/// </summary>
		/// <value>The operator.</value>
		public Operator Operator {
			get {
				return _operator;
			}
		}

		protected Operand[] operands = null;

		/// <summary>
		/// Gets or sets the operands.
		/// </summary>
		/// <value>The operands.</value>
		public virtual Operand[] Operands {
			get {
				return operands;
			}
			set {
				this.operands = value;
			}
		}

		private int stamp = int.MinValue;

		/// <summary>
		/// Gets or sets the stamp.
		/// </summary>
		/// <value>The stamp.</value>
		public int Stamp {
			get {
				return stamp;
			}
			set {
				stamp = value;
			}
		}


		private int version = 0;

		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>The version.</value>
		public int Version {
			get {
				return version;
			}
			set {
				version = value;
			}
		}

		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static InternalSizeType GetType (ConvertType type)
		{
			switch (type) {

				case Operand.ConvertType.Conv_I1:

				case Operand.ConvertType.Conv_Ovf_I1:

				case Operand.ConvertType.Conv_Ovf_I1_Un:
					return InternalSizeType.I1;

				case Operand.ConvertType.Conv_U1:

				case Operand.ConvertType.Conv_Ovf_U1:

				case Operand.ConvertType.Conv_Ovf_U1_Un:
					return InternalSizeType.U1;

				case Operand.ConvertType.Conv_I2:

				case Operand.ConvertType.Conv_Ovf_I2:

				case Operand.ConvertType.Conv_Ovf_I2_Un:
					return InternalSizeType.I2;

				case Operand.ConvertType.Conv_U2:

				case Operand.ConvertType.Conv_Ovf_U2:

				case Operand.ConvertType.Conv_Ovf_U2_Un:
					return InternalSizeType.U2;

				case Operand.ConvertType.Conv_I:

				case Operand.ConvertType.Conv_Ovf_I:

				case Operand.ConvertType.Conv_Ovf_I_Un:

				case Operand.ConvertType.Conv_I4:

				case Operand.ConvertType.Conv_Ovf_I4:

				case Operand.ConvertType.Conv_Ovf_I4_Un:
					return InternalSizeType.I4;

				case Operand.ConvertType.Conv_U:

				case Operand.ConvertType.Conv_Ovf_U:

				case Operand.ConvertType.Conv_Ovf_U_Un:

				case Operand.ConvertType.Conv_U4:

				case Operand.ConvertType.Conv_Ovf_U4:

				case Operand.ConvertType.Conv_Ovf_U4_Un:
					return InternalSizeType.U4;

				case Operand.ConvertType.Conv_I8:

				case Operand.ConvertType.Conv_Ovf_I8:

				case Operand.ConvertType.Conv_Ovf_I8_Un:
					return InternalSizeType.I8;

				case Operand.ConvertType.Conv_U8:

				case Operand.ConvertType.Conv_Ovf_U8:

				case Operand.ConvertType.Conv_Ovf_U8_Un:
					return InternalSizeType.U8;

				case Operand.ConvertType.Conv_R4:

				case Operand.ConvertType.Conv_R_Un:
					return InternalSizeType.R4;

				case Operand.ConvertType.Conv_R8:
					return InternalSizeType.R8;

				default:
					throw new Exception ("'" + type + "' not supported.");
			}
		}

		/// <summary>
		/// Replaces the specified register values.
		/// </summary>
		/// <param name="registerValues">The register values.</param>
		/*public void Replace (Dictionary<string, Operand> registerValues)
		{
			if (this.operands == null) 
				return;

			for (int i = 0; i < this.operands.Length; i++) {
				Operand operand = this.operands[i];

				operand.Replace (registerValues);

				if (operand is Register && registerValues.ContainsKey (operand.ToString())) 
					this.operands[i] = registerValues[operand.ToString() ];
			}
		}*/

		/// <summary>
		/// Clones this instance.
		/// </summary>
		/// <returns></returns>
		public virtual SharpOS.AOT.IR.Operands.Operand Clone ()
		{
			/*BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();

			binaryFormatter.Serialize (memoryStream, this);
			memoryStream.Seek (0, SeekOrigin.Begin);

			SharpOS.AOT.IR.Operands.Operand operand = (SharpOS.AOT.IR.Operands.Operand) binaryFormatter.Deserialize (memoryStream);

			return operand;*/

			throw new EngineException ("Not implemented.");
		}

		protected virtual void Clone (SharpOS.AOT.IR.Operands.Operand operand)
		{
			operand.register = this.register;
			operand.stack = this.stack;
			operand.sizeType = this.sizeType;
			operand.stamp = this.stamp;
			operand.version = this.version;
			operand.convertTo = this.convertTo;
			operand._operator = this._operator;

			if (this.operands != null && this.operands.Length > 0)
				throw new EngineException ("Not implemented.");
		}

		/// <summary>
		/// Gets the ID.
		/// </summary>
		/// <value>The ID.</value>
		public virtual string ID {
			get {
				return this.ToString ();
			}
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override string ToString () {
			StringBuilder stringBuilder = new StringBuilder ();
			string operatorValue = string.Empty;

			/*if (this._operator != null)
			{
			    stringBuilder.Append(this._operator.ToString() + " ");
			}

			if (this.operands != null && this.operands.Length > 0)
			{
			    foreach (Operand operand in operands)
			    {
			        if (operand != operands[0])
			        {
			            stringBuilder.Append(", ");
			        }

			        stringBuilder.Append(operand.ToString());
			    }

			}*/

			if (this._operator != null) 
				operatorValue = this._operator.ToString();
			
			if (this.operands != null && this.operands.Length > 0) {
				if (this.operands.Length == 1) {
					stringBuilder.Append (operatorValue + " (" + this.operands[0].ToString() + ")");

				} else if (this.operands.Length == 2) {
					stringBuilder.Append ("(" + this.operands[0].ToString() + ") " + operatorValue + " (" + this.operands[1].ToString() + ")");

				} else {
					stringBuilder.Append (operatorValue + " (");

					foreach (Operand expression in operands) {
						if (expression != operands[0]) 
							stringBuilder.Append (", ");

						stringBuilder.Append (expression.ToString());
					}

					stringBuilder.Append (")");
				}

			} else
				stringBuilder.Append (operatorValue);

			return stringBuilder.ToString ().Trim ();
		}

		public delegate void OperandVisitor (bool assignee, int level, object parent, Operand operand);

		public virtual void Visit (bool assignee, int level, object parent, OperandVisitor visitor)
		{
			if (this.operands != null)
				foreach (Operand operand in this.operands)
					operand.Visit (assignee, level + 1, this, visitor);
		}


		public delegate bool OperandReplaceVisitor (object parent, Operand oldValue);

		public virtual int ReplaceOperand (string id, Operand operand, OperandReplaceVisitor visitor)
		{
			int replacements = 0;

			if (this.operands != null) {
				for (int i = 0; i < this.operands.Length; i++) {
					if (this.operands [i].ID == id) {
						if (visitor == null || visitor (this, this.operands [i])) {
							this.operands [i] = operand;
							replacements++;
						}
					} else
						replacements += this.operands [i].ReplaceOperand (id, operand, visitor);
				}
			}

			return replacements;
		}
	}
}