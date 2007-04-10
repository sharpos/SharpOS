/**
 *  (C) 2006-2007 The SharpOS Project Team - http://www.sharpos.org
 *
 *  Licensed under the terms of the GNU GPL License version 2.
 *
 *  Author: Mircea-Cristian Racasan <darx_kies@gmx.net>
 *
 */

using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using SharpOS.AOT.IR.Operands;

namespace SharpOS.AOT.IR.Instructions {
	[Serializable]
	public class Jump : SharpOS.AOT.IR.Instructions.Instruction {
		public Jump ()
			: base (null)
		{
		}

		public Jump (Operands.Boolean condition)
			: base (condition)
		{
		}

		public override void Dump (string prefix, StringBuilder stringBuilder)
		{
			/*stringBuilder.Append(prefix + this.FormatedIndex + "Jump [");

			for (int i = 0; i < this.Branches.Count; i++)
			{
			    if (i > 0)
			    {
			        stringBuilder.Append(", ");
			    }

			    stringBuilder.Append(this.Branches[i].FormatedIndex.Trim());
			}

			stringBuilder.Append("]");*/

			stringBuilder.Append (prefix + this.FormatedIndex + "Jmp");

			if (this.Value != null) 
				stringBuilder.Append (" " + this.Value);

			stringBuilder.Append ("\n");
		}
	}
}