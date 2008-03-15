// 
// (C) 2008-2008 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Stanislaw Pitucha <viraptor@gmail.com>
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
	public class Ldelema : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Ldflda"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="index">Array's index.</param>
		/// <param name="arrayRef">Array reference.</param>
		public Ldelema (Class type, Register result, Register index, Register arrayRef)
			: base ("Ldelema", result, new Operand [] { arrayRef, index })
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
			if (this.type.IsClass || this.type.IsValueType) {
				this.def.InternalType = InternalType.M;
				this.def.Type = type;

			} else
				throw new NotImplementedEngineException ("Type " + this.Type + " can't be handled by Ldelema");
		}
	}
}
