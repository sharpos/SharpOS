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
	// TODO add support for call/callvirt/calli/jmp
	/// <summary>
	/// 
	/// </summary>
	public class Call : CallInstruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Call"/> class.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <param name="result">The result.</param>
		/// <param name="parameters">The parameters.</param>
		public Call (Method method, Register result, Operand [] parameters)
			: base (method, "Call " + method.MethodFullName + " ", result, parameters)
		{
		}


		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			if (this.def != null) {
				this.def.InternalType = this.AdjustRegisterInternalType (this.method.ReturnType.InternalType);
				this.def.Type = this.method.ReturnType;
			}
		}
	}
}