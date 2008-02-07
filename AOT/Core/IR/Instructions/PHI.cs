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
	public class PHI : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="PHI"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="operands">The operands.</param>
		public PHI (Register result, Operand [] operands)
			: base ("PHI", result, operands)
		{
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			InternalType type = this.use [0].InternalType;
			int index = (this.use [0] as Register).Index;

			for (int i = 1; i < this.use.Length; i++) {
				if (type != this.use [i].InternalType)
					throw new EngineException (string.Format ("The PHI operands have not the same type. ({0})", method.MethodFullName));

				if (index != (this.use [i] as Register).Index)
					throw new EngineException (string.Format ("The PHI operands have not the same index. ({0})", method.MethodFullName));
			}

			this.def.InternalType = type;
		}

		/// <summary>
		/// Attaches this instance.
		/// </summary>
		public void Attach ()
		{
			foreach (Register register in this.use)
				register.PHI = this.def as Register;
		}
	}
}