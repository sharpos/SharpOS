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
			StringBuilder stringBuilder = new StringBuilder ();

			if (this.instance != null) {
				stringBuilder.Append (this.instance.ToString ());
				stringBuilder.Append ("->");
			}

			stringBuilder.Append (this.field.ToString ());

			return stringBuilder.ToString ();
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

		/// <summary>
		/// Gets or sets the type of the internal.
		/// </summary>
		/// <value>The type of the internal.</value>
		public override InternalType InternalType
		{
			get
			{
				return this.field.InternalType;
			}
		}
	}
}
