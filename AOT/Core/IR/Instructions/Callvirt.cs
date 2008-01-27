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
	public class Callvirt : Call {
		/// <summary>
		/// Initializes a new instance of the <see cref="Callvirt"/> class.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <param name="result">The result.</param>
		/// <param name="parameters">The parameters.</param>
		public Callvirt (Method method, Register result, Operand [] parameters)
			: base (method, result, parameters)
		{
		}
	}
}