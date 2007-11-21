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
	/// <summary>
	/// This abstract class represents a register for the x86 architecture
	/// </summary>
	public abstract class Register {
		/// <summary>
		/// Primary constructor for derived classes to overload when instanciating a reference to
		/// an x86 register.
		/// </summary>
		/// <param name="name">The name of the register that is being referred to</param>
		/// <param name="index">An implementation-specific number used to encode this register to stream</param>
		public Register (string name, byte index)
		{
			this.name = name;
			this.index = index;
		}

		private string name = string.Empty;

		/// <summary>
		/// The name of this register
		/// </summary>
		public string Name {
			get {
				return name;
			}
		}

		private byte index = 0;

		/// <summary>
		/// An implementation-specific value used to aid in the encoding of the reference to this register
		/// to a stream or file.
		/// </summary>
		public byte Index {
			get {
				return index;
			}
		}

		/// <summary>
		/// Returns the name of this register as a String
		/// </summary>
		/// <returns>A string containing the name of this register</returns>
		public override string ToString ()
		{
			return this.name;
		}

		public static string GetName (object value)
		{
			string result;

			if (value is System.String)
				result = value as System.String;

			else if (value is SharpOS.AOT.IR.Operands.Identifier)
				result = (value as SharpOS.AOT.IR.Operands.Identifier).Value.ToString();
	
			else
				throw new Exception ("'" + value.ToString() + "' is not supported.");

			if (result.IndexOf ("::") != -1)
				result = result.Substring (result.IndexOf ("::") + 2);

			if (result.ToLower ().Equals ("null"))
				return null;

			return result;
		}
	}
}