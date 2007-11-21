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
	public class Relational : OperatorImplementation<Operator.RelationalType> {
		/// <summary>
		/// Initializes a new instance of the <see cref="Relational"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		public Relational (Operator.RelationalType type)
			: base (type)
		{
		}

		/// <summary>
		/// Negates this instance.
		/// </summary>
		/// <returns></returns>
		public Relational Negate ()
		{
			Relational result = null;

			switch (this.Type) {

				case Operator.RelationalType.Equal:
					result = new Relational (Operator.RelationalType.NotEqualOrUnordered);

					break;

				case Operator.RelationalType.NotEqualOrUnordered:
					result = new Relational (Operator.RelationalType.Equal);

					break;

				case Operator.RelationalType.GreaterThan:
					result = new Relational (Operator.RelationalType.LessThanOrEqual);

					break;

				case Operator.RelationalType.GreaterThanUnsignedOrUnordered:
					result = new Relational (Operator.RelationalType.LessThanOrEqualUnsignedOrUnordered);

					break;

				case Operator.RelationalType.GreaterThanOrEqual:
					result = new Relational (Operator.RelationalType.LessThan);

					break;

				case Operator.RelationalType.GreaterThanOrEqualUnsignedOrUnordered:
					result = new Relational (Operator.RelationalType.LessThanUnsignedOrUnordered);

					break;

				case Operator.RelationalType.LessThan:
					result = new Relational (Operator.RelationalType.GreaterThanOrEqual);

					break;

				case Operator.RelationalType.LessThanUnsignedOrUnordered:
					result = new Relational (Operator.RelationalType.GreaterThanOrEqualUnsignedOrUnordered);

					break;

				case Operator.RelationalType.LessThanOrEqual:
					result = new Relational (Operator.RelationalType.GreaterThan);

					break;

				case Operator.RelationalType.LessThanOrEqualUnsignedOrUnordered:
					result = new Relational (Operator.RelationalType.GreaterThanUnsignedOrUnordered);

					break;
			}

			return result;
		}

	}
}