// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
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
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace SharpOS.AOT.X86 {
	internal class AlignInstruction : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="AlignInstruction"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public AlignInstruction (UInt32 value)
			: base (true, string.Empty, string.Empty, "ALIGN", value.ToString (), null, null, null, value, null)
		{
		}
	}
}