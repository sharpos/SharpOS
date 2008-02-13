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
	internal class LabelInstruction : Instruction {
		/// <summary>
		/// <see cref="LabelInstruction"/> represents a label in the output encoding, which may be used to represent
		/// a particular offset in the resulting image file.
		/// </summary>
		/// <param name="label">The label.</param>
		public LabelInstruction (string label)
			: base (false, label, string.Empty, Assembly.FormatLabelName (label) + ":", string.Empty, null, null, null, Assembly.FormatLabelName (label), null)
		{
		}
	}
}