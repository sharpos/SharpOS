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
	internal class WordDataInstruction : DataInstruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="WordDataInstruction"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public WordDataInstruction (UInt16 value)
			: base (false, string.Empty, string.Empty, string.Empty, "DW " + string.Format ("0x{0:X4}", value), null, null, null, value, null)
		{
		}

		public WordDataInstruction (string values)
			: base (false, string.Empty, string.Empty, string.Empty, "DW \"" + values + "\"", null, null, null, values, null)
		{
		}

		public override string Parameters
		{
			get
			{
				if (this.value.GetType () == typeof (UInt16))
					return "DW " + string.Format ("0x{0:X4}", this.value);

				return base.Parameters;
			}
		}

		public override bool Encode (bool bits32, BinaryWriter binaryWriter)
		{
			if (this.Value is string) {
				string value = (string) this.Value;

				binaryWriter.Write (System.Text.Encoding.Unicode.GetBytes (value));

			} else if (this.value is UInt16) {
				binaryWriter.Write ((UInt16) this.Value);
			} else
				base.Encode (bits32, binaryWriter);

			return true;
		}

	}
}