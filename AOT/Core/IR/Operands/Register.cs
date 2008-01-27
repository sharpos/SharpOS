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
using Mono.Cecil;


namespace SharpOS.AOT.IR.Operands {
	/// <summary>
	/// .NET Stack Register.
	/// </summary>
	public class Register : Identifier {
		/// <summary>
		/// Initializes a new instance of the <see cref="Register"/> class.
		/// </summary>
		/// <param name="index">The index.</param>
		public Register (int index)
			: base ("Reg", index)
		{
		}

		private Register phi = null;

		/// <summary>
		/// Gets or sets the PHI.
		/// </summary>
		/// <value>The PHI.</value>
		public Register PHI
		{
			get
			{
				return phi;
			}
			set
			{
				phi = value;
			}
		}

		Instructions.Instruction parent = null;

		/// <summary>
		/// Gets or sets the parent.
		/// </summary>
		/// <value>The parent.</value>
		public Instructions.Instruction Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		/// <summary>
		/// Toes the string.
		/// </summary>
		/// <returns></returns>
		public override string ToString ()
		{
			string suffix = "";

			if (this.phi != null)
				suffix = "{" + this.phi + "}";

			return base.ToString () + suffix;
		}
	}
}