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
	public class Object : Identifier {
		/// <summary>
		/// Initializes a new instance of the <see cref="Object"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="operand">The operand.</param>
		public Object (string name, Operand operand)
			: base ("obj", new Operand[] { operand }, name)
		{
		}

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public Identifier Address
		{
			set {
				this.operands [0] = value;
			}
			get {
				return this.operands [0] as Identifier;
			}
		}

		public override int Register
		{
			get
			{
				return this.Address.Register;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is register set.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is register set; otherwise, <c>false</c>.
		/// </value>
		public override bool IsRegisterSet
		{
			get
			{
				return this.Address.Register != int.MinValue;
			}
		}

		/// <summary>
		/// Gets or sets the stack.
		/// </summary>
		/// <value>The stack.</value>
		public override int Stack
		{
			get
			{
				return this.Address.Stack;
			}
		}

		/// <summary>
		/// Toes the string.
		/// </summary>
		/// <returns></returns>
		public override string ToString ()
		{
			StringBuilder stringBuilder = new StringBuilder ();

			stringBuilder.Append ("(" + this.TypeName + ") " + this.operands [0].ToString ());

			return stringBuilder.ToString ();
		}
	}
}