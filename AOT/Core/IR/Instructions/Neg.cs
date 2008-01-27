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
	public class Neg : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Neg"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Neg (Register result, Register value)
			: base ("Neg", result, new Operand [] { value })
		{
			result.InternalType = value.InternalType;
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			Register register = this.use [0] as Register;

			if (register.InternalType == InternalType.I4
					|| register.InternalType == InternalType.I8
					|| register.InternalType == InternalType.F
					|| register.InternalType == InternalType.I)
				this.def.InternalType = register.InternalType;
		}
	}
}