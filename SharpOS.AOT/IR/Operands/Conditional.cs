// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using SharpOS.AOT.IR.Operators;
using Mono.Cecil;

namespace SharpOS.AOT.IR.Operands {
	[Serializable]
	public class Conditional : Boolean {
		/// <summary>
		/// Initializes a new instance of the <see cref="Conditional"/> class.
		/// </summary>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <param name="third">The third.</param>
		public Conditional (Boolean first, Operand second, Operand third)
			: base (new SharpOS.AOT.IR.Operators.Boolean (Operator.BooleanType.Conditional), first, second, third)
		{
		}
	}
}