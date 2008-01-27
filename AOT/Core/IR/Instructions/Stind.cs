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
	public class Stind : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Stind"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Stind (InternalType type, Register result, Register value)
			: base ("Stind", null, new Operand [] { result, value })
		{
			this.type = type;
		}

		InternalType type;

		/// <summary>
		/// Gets the type of the internal.
		/// </summary>
		/// <value>The type of the internal.</value>
		public InternalType InternalType
		{
			get
			{
				return this.type;
			}
		}
	}
}