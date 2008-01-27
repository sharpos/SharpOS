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
	/// 
	/// </summary>
	public class Newobj : CallInstruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Newobj"/> class.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <param name="result">The result.</param>
		/// <param name="parameters">The parameters.</param>
		public Newobj (Method method, Register result, Operand [] parameters)
			: base (method, "Newobj " + method.MethodFullName + " ", result, parameters)
		{
			result.InternalType = InternalType.O;
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			if (this.method.Class.IsValueType) {
				this.def.InternalType = InternalType.ValueType;
				this.def.Type = this.method.Class;

			} else if (this.method.Class.IsArray)
				this.def.InternalType = InternalType.Array;

			else
				this.def.InternalType = InternalType.O;
		}
	}
}