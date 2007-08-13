// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	William Lahti <xfurious@gmail.com>
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
	public class Assign : SharpOS.AOT.IR.Instructions.Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Assign"/> class.
		/// </summary>
		/// <param name="assignee">The assignee.</param>
		/// <param name="value">The value.</param>
		public Assign (Identifier assignee, Operand value)
			: base (value)
		{
			this.assignee = assignee;
		}

		private Identifier assignee;

		/// <summary>
		/// Gets the assignee.
		/// </summary>
		/// <value>The assignee.</value>
		public Identifier Assignee {
			get {
				return assignee;
			}
		}

		/// <summary>
		/// Dumps the specified prefix.
		/// </summary>
		/// <param name="p">The DumpProcessor.</param>
		public override void Dump (DumpProcessor p)
		{
			p.PushElement ("assign", true, true, false);
			p.AddElement ("index", this.FormatedIndex, false, true, false);
			p.AddElement ("assignee", this.assignee.ToString (), false, true, false);
			p.AddElement ("operation", " = ", false, true, false);
			p.AddElement ("value", this.Value.ToString (), false, true, false);
			p.PopElement ();
		}
	}
}