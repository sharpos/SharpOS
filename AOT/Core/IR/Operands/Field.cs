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
	public class Field: Operand {
		/// <summary>
		/// Initializes a new instance of the <see cref="Field"/> class.
		/// </summary>
		/// <param name="fieldDefinition">The field definition.</param>
		public Field (FieldDefinition fieldDefinition, Class type, Class _class, InternalType internalType)
		{
			this.type = type;
			this.internalType = internalType;
			this.fieldDefinition = fieldDefinition;
			this._class = _class;
		}

		Class _class;

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

		public string AddressOf
		{
			get
			{
				foreach (CustomAttribute customAttribute in this.fieldDefinition.CustomAttributes) {
					if (customAttribute.Constructor.DeclaringType.FullName !=
							typeof (SharpOS.AOT.Attributes.AddressOfAttribute).FullName)
						continue;

					return customAttribute.ConstructorParameters [0].ToString ();
				}

				return string.Empty;
			}
		}

		public override string ToString ()
		{
			StringBuilder stringBuilder = new StringBuilder ();

			stringBuilder.Append (this.type.TypeFullName);
			stringBuilder.Append (" ");
			stringBuilder.Append (this._class.TypeFullName);
			stringBuilder.Append ("::");
			stringBuilder.Append (this.fieldDefinition.Name);

			return stringBuilder.ToString ();
		}
	}
}