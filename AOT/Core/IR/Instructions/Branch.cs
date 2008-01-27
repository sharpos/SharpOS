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
	public class Branch : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Branch"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public Branch (RelationalType type, Register first, Register second)
			: base ("Branch", null, new Operand [] { first, second })
		{
			this.type = type;
		}

		private RelationalType type;

		/// <summary>
		/// Gets the type of the relational.
		/// </summary>
		/// <value>The type of the relational.</value>
		public RelationalType RelationalType
		{
			get
			{
				return this.type;
			}
		}
	}
}