// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
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
	public class System : SharpOS.AOT.IR.Instructions.Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="System"/> class.
		/// </summary>
		/// <param name="_operator">The _operator.</param>
		public System (Miscellaneous _operator)
			: base (_operator)
		{
		}
	}
}