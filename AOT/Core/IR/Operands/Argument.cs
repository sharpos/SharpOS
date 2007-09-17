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
		protected Argument ()
			: base ()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Argument"/> class.
		/// </summary>
		/// <param name="i">The index.</param>
		/// <param name="typeName">Name of the type.</param>
		public Argument (int i, string typeName)
			: base ("Arg" + i, i, typeName)
		{
		}

		/// <summary>
		/// Visits the specified assignee.
		/// </summary>
		/// <param name="assignee">if set to <c>true</c> [assignee].</param>
		/// <param name="level">The level.</param>
		/// <param name="visitor">The visitor.</param>
		public override void Visit (bool assignee, int level, object parent, OperandVisitor visitor)
		{
			visitor (assignee, level, parent, this);

			base.Visit (assignee, level, this, visitor);
		}

		public override Operand Clone ()
		{
			Argument argument = new Argument ();

			base.Clone (argument);

			return argument;
		}
	}
}