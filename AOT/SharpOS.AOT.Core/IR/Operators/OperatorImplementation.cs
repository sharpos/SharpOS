// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.Collections.Generic;
using System.Text;

namespace SharpOS.AOT.IR.Operators {
	public class OperatorImplementation<TYPE> : Operator {
		/// <summary>
		/// Initializes a new instance of the <see cref="OperatorImplementation&lt;TYPE&gt;"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		public OperatorImplementation (TYPE type)
		{
			this.type = type;
		}

		private TYPE type;

		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <value>The type.</value>
		public TYPE Type {
			get {
				return type;
			}
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override string ToString ()
		{
			return this.type.ToString ();
		}
	}
}
