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
	public class Ldelem : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ldelem"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public Ldelem (InternalType type, Register result, Register first, Register second)
			: base ("Ldelem", result, new Operand [] { first, second })
		{
			this.type = type;
			result.InternalType = this.AdjustRegisterInternalType (type);
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

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.Type = this.use [0].Type.SpecialTypeElement;
		}
	}
}