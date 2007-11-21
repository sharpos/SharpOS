// 
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	William Lahti <xfurious@gmail.com>
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
		/// Dumps the instruction.
		/// </summary>
		/// <param name="dumpProcessor">The dump processor.</param>
		public override void Dump (DumpProcessor dumpProcessor)
		{
			dumpProcessor.AddElement ("switch", this.FormatedIndex + "Switch " + this.Value.ToString (), true, true, false);
		}
	}
}