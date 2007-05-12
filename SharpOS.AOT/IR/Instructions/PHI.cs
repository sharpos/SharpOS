// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	William Lahti <xfurious@gmail.com>
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
		/// <param name="p"></param>
		public override void Dump (DumpProcessor p) 
		{
			Dictionary<string,string> attr = new Dictionary<string,string>();
			string v = "";
		
			p.AddElement ("assignee", this.Assignee.ToString(), false, true, false);
			p.AddElement ("operation", this.Assignee.ToString (), false, true, false);
		
			for (int i = 0; i < this.Value.Operands.Length; i++) {
				if (i > 0)
					v += ", ";
		
				v += this.Value.Operands[i].ToString();
			}

			v = "{" + v + "}";

			p.AddElement ("value", v, false, true, false);
		}
	}
}
