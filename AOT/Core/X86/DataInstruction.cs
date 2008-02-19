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
using SharpOS.AOT.IR;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace SharpOS.AOT.X86 {
	internal class DataInstruction : Instruction {
		/// <summary>
		/// <see cref="DataInstruction"/> represents a data value encoded into the output image.
		/// </summary>
		/// <param name="indent">if set to <c>true</c>, the data instruction is indented in the ASM dump</param>
		/// <param name="label">The label.</param>
		/// <param name="reference">The reference.</param>
		/// <param name="name">The name.</param>
		/// <param name="parameters">The parameters.</param>
		/// <param name="rmMemory">The rm memory.</param>
		/// <param name="rmRegister">The rm register.</param>
		/// <param name="register">The register.</param>
		/// <param name="value">The value.</param>
		/// <param name="encoding">The encoding.</param>
		public DataInstruction (bool indent, string label, string reference, string name, string parameters, Memory rmMemory, Register rmRegister, Register register, object value, string [] encoding)
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
			// all calls to Encode are handled by specialized methods overriding this one
			throw new EngineException ("Wrong data type.");
		}
	}
}
