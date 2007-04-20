// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
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
	public class Field : Identifier {
		/// <summary>
		/// Initializes a new instance of the <see cref="Field"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="operand">The operand.</param>
		public Field (string name, Operand operand)
			: base (name, new Operand[] { operand })
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Field"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		public Field (string name)
			: base (name, 0)
		{
		}

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public Identifier Instance
		{
			set {
				if (this.operands == null || this.operands.Length == 0)
					this.operands = new Operand [1];

				this.operands [0] = value;
			}
			get {
				if (this.operands != null && this.operands.Length > 0)
					return this.operands [0] as Identifier;

				return null;
			}
		}

		/// <summary>
		/// Toes the string.
		/// </summary>
		/// <returns></returns>
		public override string ToString ()
		{
			StringBuilder stringBuilder = new StringBuilder ();

			if (this.operands != null && this.operands.Length != 0)
				stringBuilder.Append (this.operands [0].ToString () + "->");

			stringBuilder.Append (base.ToString ());

			return stringBuilder.ToString ();
		}
	}
}