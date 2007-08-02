// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Bruce Markham <illuminus86@gmail.com>
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
	public class Call : Operand {
		/// <summary>
		/// Initializes a new instance of the <see cref="Call"/> class.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <param name="operands">The operands.</param>
		public Call (Mono.Cecil.MethodReference method, Operand[] operands)
			: base (null, operands)
		{
			this.method = method;
		}

		private Mono.Cecil.MethodReference method;

		/// <summary>
		/// Gets the method.
		/// </summary>
		/// <value>The method.</value>
		public Mono.Cecil.MethodReference Method {
			get {
				return method;
			}
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override string ToString ()
		{
			StringBuilder stringBuilder = new StringBuilder();

			stringBuilder.Append ("Call " + this.Method.ReturnType.ReturnType.Name);
			stringBuilder.Append (" " + this.Method.DeclaringType.FullName + "." + this.method.Name);
			stringBuilder.Append ("(");

			foreach (Operand operand in this.Operands) {
				if (operand != this.Operands[0]) 
					stringBuilder.Append (", ");

				stringBuilder.Append (operand.ToString());
			}

			stringBuilder.Append (")");

			return stringBuilder.ToString();
		}

		/// <summary>
		/// It returns the unique name of this call. (e.g. "void namespace.class.method UInt32 UInt16")
		/// </summary>
		public string AssemblyLabel {
			get {
				return IR.Method.GetLabel (this.Method);
			}
		}
	}
}
