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
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using SharpOS.AOT.IR.Operands;

namespace SharpOS.AOT.IR.Instructions {
	[Serializable]
	public class End : SharpOS.AOT.IR.Instructions.Instruction {
		public static readonly End END = new End ();

		/// <summary>
		/// Initializes a new instance of the <see cref="End"/> class.
		/// </summary>
		private End ()
			: base (null)
		{
			this.Index = int.MaxValue;
		}
	}
}