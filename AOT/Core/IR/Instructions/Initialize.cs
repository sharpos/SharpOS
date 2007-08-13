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

using Mono.Cecil.Cil;

namespace SharpOS.AOT.IR.Instructions {
	[Serializable]
	public class Initialize : SharpOS.AOT.IR.Instructions.Assign {
		/// <summary>
		/// Initializes a new instance of the <see cref="Initialize"/> class.
		/// </summary>
		/// <param name="assignee">The assignee.</param>
		public Initialize (Identifier assignee, VariableDefinition variableDefinition)
			: base (assignee, null)
		{
			this.variableDefinition = variableDefinition;
		}

		private VariableDefinition variableDefinition;

		public VariableDefinition Type {
			get {
				return this.variableDefinition;
			}
		}

		/// <summary>
		/// Dumps the specified prefix.
		/// </summary>
		/// <param name="p">The DumpProcessor.</param>
		public override void Dump (DumpProcessor p)
		{
			p.PushElement ("initialize", true, true, false);
			p.AddElement ("index", this.FormatedIndex, false, true, false);
			p.AddElement ("operation", "INITIALIZE ", false, true, false);
			p.AddElement ("assignee", this.Assignee.ToString (), false, true, false);
			p.PopElement ();
		}
	}
}