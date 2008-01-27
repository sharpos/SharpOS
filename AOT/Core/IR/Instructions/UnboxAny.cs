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
	public class UnboxAny : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="UnboxAny"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="instance">The instance.</param>
		public UnboxAny (Class type, Register result, Register instance)
			: base ("UnboxAny", result, new Operand [] { instance })
		{
			this.type = type;
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

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			if (this.type.ClassDefinition.IsValueType) {
				this.def.InternalType = method.Class.Engine.GetInternalType (this.type.TypeFullName);
				this.def.Type = this.type;

			} else
				throw new NotImplementedEngineException ();
		}
	}
}