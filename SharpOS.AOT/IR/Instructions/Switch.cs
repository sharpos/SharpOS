// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
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
	public class Switch : SharpOS.AOT.IR.Instructions.Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Switch"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public Switch (Operand value)
			: base (value)
		{
		}

		/// <summary>
		/// Dumps the specified prefix.
		/// </summary>
		/// <param name="prefix">The prefix.</param>
		/// <param name="stringBuilder">The string builder.</param>
		public override void Dump (string prefix, StringBuilder stringBuilder)
		{
			stringBuilder.Append (prefix + this.FormatedIndex + "Switch " + this.Value + "\n");
		}
	}
}