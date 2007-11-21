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
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using SharpOS.AOT.IR.Operators;
using Mono.Cecil;

namespace SharpOS.AOT.IR.Operands {
	[Serializable]
	public class Register : Identifier {
		/// <summary>
		/// Initializes a new instance of the <see cref="Register"/> class.
		/// </summary>
		/// <param name="i">The i.</param>
		public Register (int i)
			: base ("Reg" + i, i, null)
		{
		}

		protected Register ()
			: base ()
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
			Register register = new Register ();

			base.Clone (register);

			return register;
		}
	}
}