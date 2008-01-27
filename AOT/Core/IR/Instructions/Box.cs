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
	public class Box : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Box"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Box (Class type, Register result, Register value)
			: base ("Box", result, new Operand [] { value })
		{
			this.type = type;

			result.InternalType = InternalType.O;
		}

		Class type;

		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <value>The type.</value>
		public Class Type
		{
			get
			{
				return this.type;
			}
		}
	}
}