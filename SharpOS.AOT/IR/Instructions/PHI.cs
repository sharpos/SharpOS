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
	public class PHI : SharpOS.AOT.IR.Instructions.Assign {
		/// <summary>
		/// Initializes a new instance of the <see cref="PHI"/> class.
		/// </summary>
		/// <param name="asignee">The asignee.</param>
		/// <param name="identifiers">The identifiers.</param>
		public PHI (Identifier asignee, SharpOS.AOT.IR.Operands.Miscellaneous identifiers)
			: base (asignee, identifiers)
		{
		}

		/// <summary>
		/// Dumps the specified prefix.
		/// </summary>
		/// <param name="prefix">The prefix.</param>
		/// <param name="stringBuilder">The string builder.</param>
		public override void Dump (string prefix, StringBuilder stringBuilder) 
		{
			stringBuilder.Append (prefix + this.FormatedIndex);

			stringBuilder.Append (this.Assignee.ToString());

			stringBuilder.Append (" = {");

			for (int i = 0; i < this.Value.Operands.Length; i++) {
				if (i > 0) 
					stringBuilder.Append (", ");

				stringBuilder.Append (this.Value.Operands[i].ToString());
			}

			stringBuilder.Append ("}\n");
		}
	}
}
