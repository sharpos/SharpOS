// 
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using SharpOS.AOT.IR.Operands;

namespace SharpOS.AOT.IR.Instructions {
	[Serializable]
	public class ConditionalJump : Jump {
		/// <summary>
		/// Initializes a new instance of the <see cref="ConditionalJump"/> class.
		/// </summary>
		/// <param name="condition">The condition.</param>
		public ConditionalJump (Operands.Boolean condition)
			: base (condition)
		{
		}
	}
}