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
using SharpOS.AOT.IR;
using SharpOS.AOT.IR.Instructions;
using SharpOS.AOT.IR.Operands;
using SharpOS.AOT.IR.Operators;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace SharpOS.AOT.X86 {
	internal class LabelInstruction : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="LabelInstruction"/> class.
		/// </summary>
		/// <param name="label">The label.</param>
		public LabelInstruction (string label)
			: base (false, label, string.Empty, Assembly.FormatLabelName (label) + ":", string.Empty, null, null, null, Assembly.FormatLabelName (label), null)
		{
		}
	}
}