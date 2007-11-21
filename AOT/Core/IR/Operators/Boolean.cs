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
using System.Collections.Generic;
using System.Text;

namespace SharpOS.AOT.IR.Operators {
	[Serializable]
	public class Boolean : OperatorImplementation<Operator.BooleanType> {
		/// <summary>
		/// Initializes a new instance of the <see cref="Boolean"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		public Boolean (Operator.BooleanType type)
			: base (type)
		{
		}

		/// <summary>
		/// Negates this instance.
		/// </summary>
		/// <returns></returns>
		public Boolean Negate ()
		{
			Boolean result = null;

			switch (this.Type) {

				case Operator.BooleanType.True:
					result = new Boolean (BooleanType.False);

					break;

				case Operator.BooleanType.False:
					result = new Boolean (BooleanType.True);

					break;

				case Operator.BooleanType.And:
					result = new Boolean (BooleanType.Or);

					break;

				case Operator.BooleanType.Or:
					result = new Boolean (BooleanType.And);

					break;

				case Operator.BooleanType.Conditional:
					result = new Boolean (BooleanType.Conditional);

					break;
			}

			return result;
		}
	}
}
