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
	public class Shr : Instruction {
		public enum Type {
			SHR,
			SHRUnsigned
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Shr"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public Shr (Type type, Register result, Register first, Register second)
			: base ("Shr", result, new Operand [] { first, second })
		{
			this.type = type;
		}

		private Type type;

		public Type ShrType
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
			this.def.InternalType = this.use [0].InternalType;
		}
	}
}