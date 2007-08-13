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
	public class Jump : SharpOS.AOT.IR.Instructions.Instruction {
		public Jump ()
			: base (null)
		{
		}

		public Jump (Operands.Boolean condition)
			: base (condition)
		{
		}

		/* FIXME: convert to XML dump or kill (effectively the same as base Dump())
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

			stringBuilder.Append("]");

			stringBuilder.Append (prefix + this.FormatedIndex + "Jmp");

			if (this.Value != null) 
				stringBuilder.Append (" " + this.Value);

			stringBuilder.Append ("\n");
		}*/

		/// <summary>
		/// Dumps the instruction.
		/// </summary>
		/// <param name="dumpProcessor">The dump processor.</param>
		public override void Dump (DumpProcessor dumpProcessor)
		{
			if (this.Value != null)
				dumpProcessor.AddElement ("jump", this.FormatedIndex + "Jump " + this.Value.ToString (), true, true, false);
			
			else
				dumpProcessor.AddElement ("jump", this.FormatedIndex + "Jump", true, true, false);
		}
	}
}