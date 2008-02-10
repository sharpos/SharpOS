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
	public class Endfilter : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Endfilter"/> class.
		/// </summary>
		public Endfilter (Register value)
			: base ("Endfilter", null, new Operand [] { value })
		{
		}
	}
}