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
	public class Miscellaneous : Operand {
		/// <summary>
		/// Initializes a new instance of the <see cref="Miscellaneous"/> class.
		/// </summary>
		/// <param name="_operator">The _operator.</param>
		public Miscellaneous (Operator _operator)
			: base (_operator, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Miscellaneous"/> class.
		/// </summary>
		/// <param name="_operator">The _operator.</param>
		/// <param name="operand">The operand.</param>
		public Miscellaneous (Operator _operator, Operand operand)
			: base (_operator, new Operand[] { operand })
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Miscellaneous"/> class.
		/// </summary>
		/// <param name="_operator">The _operator.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		public Miscellaneous (Operator _operator, Operand first, Operand second)
			: base (_operator, new Operand[] { first, second })
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Miscellaneous"/> class.
		/// </summary>
		/// <param name="_operator">The _operator.</param>
		/// <param name="operands">The operands.</param>
		public Miscellaneous (Operator _operator, Operand[] operands)
			: base (_operator, operands)
		{
		}
	}
}