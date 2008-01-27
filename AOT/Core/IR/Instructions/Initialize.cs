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
	public class Initialize : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Initialize"/> class.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="typeReference">The type reference.</param>
		public Initialize (Local source, TypeReference typeReference)
			: base ("Initialize", null, new Operand [] { source })
		{
			this.typeReference = typeReference;
		}

		TypeReference typeReference;
	}
}