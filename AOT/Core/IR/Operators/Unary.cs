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
using System.Collections.Generic;
using System.Text;

namespace SharpOS.AOT.IR.Operators {
	[Serializable]
	public class Unary : OperatorImplementation<Operator.UnaryType> {
		/// <summary>
		/// Initializes a new instance of the <see cref="Unary"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		public Unary (Operator.UnaryType type)
			: base (type)
		{
		}
	}
}