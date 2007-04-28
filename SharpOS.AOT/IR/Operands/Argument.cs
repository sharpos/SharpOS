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
	public class Argument : Identifier {
		/// <summary>
		/// Initializes a new instance of the <see cref="Argument"/> class.
		/// </summary>
		/// <param name="i">The index.</param>
		/// <param name="typeName">Name of the type.</param>
		public Argument (int i, string typeName)
			: base ("Arg" + i, i)
		{
			this.typeName = typeName;
		}

		string typeName;

		/// <summary>
		/// Gets the name of the type.
		/// </summary>
		/// <value>The name of the type.</value>
		public string TypeName {
			get {
				return this.typeName;
			}
		}
	}
}