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
	public class Stelem : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Stelem"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <param name="value">The value.</param>
		public Stelem (InternalType type, Register first, Register second, Register value)
			: base ("Stelem", null, new Operand [] { first, second, value })
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