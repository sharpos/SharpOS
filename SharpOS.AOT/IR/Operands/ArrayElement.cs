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
	public class ArrayElement : Identifier {
		/// <summary>
		/// Initializes a new instance of the <see cref="ArrayElement"/> class.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <param name="index">The index.</param>
		public ArrayElement (Operand array, Operand index)
			: base (array.ToString(), new Operand[] { array, index })
		{
		}

		/// <summary>
		/// Gets the array.
		/// </summary>
		/// <value>The array.</value>
		public Operand Array {
			get {
				return this.operands[0];
			}
		}

		/// <summary>
		/// Gets the IDX.
		/// </summary>
		/// <value>The IDX.</value>
		public Operand IDX {
			get {
				return this.operands[1];
			}
		}

		/// <summary>
		/// Toes the string.
		/// </summary>
		/// <returns></returns>
		public override string ToString ()
		{
			return this.Array + "[" + this.IDX + "]";
		}
	}
}