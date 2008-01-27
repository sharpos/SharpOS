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
	public class Add : Instruction {
		/// <summary>
		/// 
		/// </summary>
		public enum Type {
			/// <summary>
			/// 
			/// </summary>
			Add,
			/// <summary>
			/// 
			/// </summary>
			AddSignedWithOverflowCheck,
			/// <summary>
			/// 
			/// </summary>
			AddUnsignedWithOverflowCheck
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Add"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public Add (Type type, Register result, Register first, Register second)
			: base ("Add", result, new Operand [] { first, second })
		{
			this.type = type;
		}

		private Type type;

		/// <summary>
		/// Gets the type of the add.
		/// </summary>
		/// <value>The type of the add.</value>
		public Type AddType
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
			this.def.InternalType = this.GetArithmeticalResultType (this.use [0], this.use [1], true, false, false, this.type != Type.Add);
		}
	}
}