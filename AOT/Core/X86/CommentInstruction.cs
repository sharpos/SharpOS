// 
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
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
	internal class CommentInstruction : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="CommentInstruction"/> class.
		/// </summary>
		/// <param name="value">The Comment.</param>
		public CommentInstruction (string value)
			: base (true, string.Empty, string.Empty, "; " + value.Trim (), string.Empty, null, null, null, null, null)
		{
		}

		public override string ToString ()
		{
			return "\n" + string.Empty.PadRight (36) + this.Name;
		}
	}
}