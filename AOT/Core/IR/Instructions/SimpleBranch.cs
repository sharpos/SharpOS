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
	public class SimpleBranch : Instruction {
		public enum Type {
			True,
			False
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleBranch"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="operand">The operand.</param>
		public SimpleBranch (Type type, Register operand)
			: base ("SimpleBranch " + type.ToString () + " ", null, new Operand [] { operand })
		{
			this.type = type;
		}

		private Type type;

		/// <summary>
		/// Gets the type of the simple branch.
		/// </summary>
		/// <value>The type of the simple branch.</value>
		public Type SimpleBranchType
		{
			get
			{
				return this.type;
			}
		}
	}
}