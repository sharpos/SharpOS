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
	internal class DWordDataInstruction : DataInstruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="DWordDataInstruction"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public DWordDataInstruction (UInt32 value)
			: base (false, string.Empty, string.Empty, string.Empty, "DD " + string.Format ("0x{0:X8}", value), null, null, null, value, null)
		{
		}

		public override bool Encode (bool bits32, BinaryWriter binaryWriter)
		{
			binaryWriter.Write ((UInt32) this.Value);
			return true;
		}

		public override string Parameters
		{
			get
			{
				if (this.value.GetType () == typeof (UInt32))
					return "DD " + string.Format ("0x{0:X8}", this.value);

				return base.Parameters;
			}
		}
	}
}