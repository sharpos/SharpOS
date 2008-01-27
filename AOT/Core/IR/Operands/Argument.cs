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
using Mono.Cecil;


namespace SharpOS.AOT.IR.Operands {
	/// <summary>
	/// Method Argument
	/// </summary>
	public class Argument : Identifier {
		/// <summary>
		/// Initializes a new instance of the <see cref="Argument"/> class.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="type">The type.</param>
		/// <param name="internalType">Type of the internal.</param>
		public Argument (int index, Class type, InternalType internalType)
			: base ("Arg", index)
		{
			this.type = type;
			this.internalType = internalType;
			this.forceSpill = true;
		}
	}
}