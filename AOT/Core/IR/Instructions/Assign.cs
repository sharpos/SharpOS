// 
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	William Lahti <xfurious@gmail.com>
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

		public override void VisitOperand (Operand.OperandVisitor visitor)
		{
			if (this.assignee != null)
				this.assignee.Visit (true, 0, this, visitor);

			base.VisitOperand (visitor);
		}

		public override int ReplaceOperand (string id, Operand operand, Operand.OperandReplaceVisitor visitor)
		{
			int replacements = 0;

			if (this.assignee != null) {
				if (this.assignee.ID == id) {
					if (visitor == null || visitor (this, this.assignee)) {
						this.assignee = operand as Identifier;
						replacements++;
					}
				} else
					replacements += this.assignee.ReplaceOperand (id, operand, visitor);
			}

			replacements += base.ReplaceOperand (id, operand, visitor);

			return replacements;
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