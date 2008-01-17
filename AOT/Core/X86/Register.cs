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
		public string Name
		{
			get
			{
				return name;
			}
		}

		private byte index = 0;

		/// <summary>
		/// An implementation-specific value used to aid in the encoding of the reference to this register
		/// to a stream or file.
		/// </summary>
		public byte Index
		{
			get
			{
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

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string GetName (object value)
		{
			if (value is IR.Operands.FieldOperand)
				return (value as IR.Operands.FieldOperand).Field.Name;

			if (value is System.String)
				return value as System.String;

			throw new EngineException (string.Format ("Could not get the register name from '{0}'", value.ToString ()));
		}
	}
}