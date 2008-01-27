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
	/// 
	/// </summary>
	public class IntConstant : Constant {
		/// <summary>
		/// Initializes a new instance of the <see cref="IntConstant"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public IntConstant (int value)
		{
			this.value = value;
			this.InternalType = InternalType.I4;
		}

		private int value;

		public int Value
		{
			get
			{
				return this.value;
			}
		}

		public override string ToString ()
		{
			return this.value.ToString ();
		}
	}
}