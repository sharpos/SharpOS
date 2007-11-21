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
	[Serializable]
	public class Address : Indirect {
		/// <summary>
		/// Initializes a new instance of the <see cref="Address"/> class.
		/// </summary>
		/// <param name="operand">The operand.</param>
		public Address (Operand operand)
			: base ("adr", operand)
		{
			this.SizeType = InternalSizeType.U;
		}

		/// <summary>
		/// Gets the ID.
		/// </summary>
		/// <value>The ID.</value>
		public override string ID {
			get {
				return this.ToString ();
			}
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public new Identifier Value {
			get {
				return this.operands[0] as Identifier;
			}
			set {
				this.operands[0] = value;
			}
		}

		/// <summary>
		/// Toes the string.
		/// </summary>
		/// <returns></returns>
		public override string ToString ()
		{
			return "adr(" + this.Value.ToString() + ")";
		}
	}
}