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
using System.Collections.Generic;
using System.Text;
using SharpOS.AOT.IR.Operands;
using Mono.Cecil;


namespace SharpOS.AOT.IR.Instructions {
	/// <summary>
	/// 
	/// </summary>
	public class SizeOf : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="SizeOf"/> class.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="typeReference">The type reference.</param>
		public SizeOf (Register result, TypeReference typeReference)
			: base ("SizeOf", result, null)
		{
			this.typeReference = typeReference;
		}

		private TypeReference typeReference;

		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <value>The type.</value>
		public TypeReference Type
		{
			get
			{
				return this.typeReference;
			}
		}

		/// <summary>
		/// Toes the string.
		/// </summary>
		/// <returns></returns>
		public override string ToString ()
		{
			return this.name + "(" + this.def.ToString () + "<=" + this.typeReference.ToString () + ")";
		}

		/// <summary>
		/// Processes the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public override void Process (Method method)
		{
			this.def.InternalType = InternalType.I4;
		}
	}
}