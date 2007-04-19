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
	internal class DWordDataInstruction : DataInstruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="DWordDataInstruction"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="value">The value.</param>
		public DWordDataInstruction (string name, UInt32 value)
			: base (false, name, string.Empty, name, "DD " + string.Format ("0x{0:X8}", value), null, null, null, value, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DWordDataInstruction"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public DWordDataInstruction (UInt32 value)
			: base (false, string.Empty, string.Empty, string.Empty, "DD " + string.Format ("0x{0:X8}", value), null, null, null, value, null)
		{
		}
	}
}