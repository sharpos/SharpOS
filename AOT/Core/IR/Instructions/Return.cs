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
	public class Return : SharpOS.AOT.IR.Instructions.Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Return"/> class.
		/// </summary>
		public Return ()
			: base (null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Return"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public Return (Operand value)
			: base (value)
		{
		}

		/// <summary>
		/// Dumps the instruction.
		/// </summary>
		/// <param name="dumpProcessor">The dump processor.</param>
		public override void Dump (DumpProcessor dumpProcessor)
		{
			if (this.Value != null)
				dumpProcessor.AddElement ("return", this.FormatedIndex + "Ret " + this.Value.ToString (), true, true, false);

			else
				dumpProcessor.AddElement ("return", this.FormatedIndex + "Ret", true, true, false);
		}
	}
}