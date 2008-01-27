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
	public class Throw : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Throw"/> class.
		/// Call this in case it is a Rethrow
		/// </summary>
		public Throw ()
			: base ("Throw", null, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Throw"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public Throw (Register value)
			: base ("Throw", null, new Operand [] { value })
		{
		}
	}
}