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
	public class ConditionCheck : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="ConditionCheck"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public ConditionCheck (RelationalType type, Register result, Register first, Register second)
			: base ("ConditionCheck", result, new Operand [] { first, second })
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

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			// TODO check the parameters

			this.def.InternalType = InternalType.I4;
		}
	}
}