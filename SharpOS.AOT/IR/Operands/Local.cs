// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using SharpOS.AOT.IR.Operators;
using Mono.Cecil;

namespace SharpOS.AOT.IR.Operands {
	[Serializable]
	public class Local : Identifier {
		/// <summary>
		/// Initializes a new instance of the <see cref="Local"/> class.
		/// </summary>
		/// <param name="i">The i.</param>
		public Local (int i)
			: base ("Loc" + i, i)
		{
		}
	}
}