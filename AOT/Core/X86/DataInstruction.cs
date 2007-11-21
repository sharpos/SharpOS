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
	internal class DataInstruction : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="DataInstruction"/> class.
		/// </summary>
		/// <param name="indent">if set to <c>true</c> [indent].</param>
		/// <param name="label">The label.</param>
		/// <param name="reference">The reference.</param>
		/// <param name="name">The name.</param>
		/// <param name="parameters">The parameters.</param>
		/// <param name="rmMemory">The rm memory.</param>
		/// <param name="rmRegister">The rm register.</param>
		/// <param name="register">The register.</param>
		/// <param name="value">The value.</param>
		/// <param name="encoding">The encoding.</param>
		public DataInstruction (bool indent, string label, string reference, string name, string parameters, Memory rmMemory, Register rmRegister, Register register, object value, string[] encoding)
			: base (indent, label, reference, name, parameters, rmMemory, rmRegister, register, value, encoding)
		{
		}

		/// <summary>
		/// Encodes the specified bits32.
		/// </summary>
		/// <param name="bits32">if set to <c>true</c> [bits32].</param>
		/// <param name="binaryWriter">The binary writer.</param>
		/// <returns></returns>
		public override bool Encode (bool bits32, BinaryWriter binaryWriter)
		{
			if (this.Value is string) {
				string value = (string) this.Value;

				binaryWriter.Write (value.ToCharArray ());

			} else if (this.Value is byte) {
				binaryWriter.Write ((byte) this.Value);

			} else if (this.Value is UInt16) {
				binaryWriter.Write ((UInt16) this.Value);

			} else if (this.Value is UInt32) {
				binaryWriter.Write ((UInt32) this.Value);

			} else
				throw new Exception ("Wrong data type.");

			return true;
		}
	}
}