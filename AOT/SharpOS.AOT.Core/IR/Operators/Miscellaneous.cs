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
	[Serializable]
	public class Miscellaneous : OperatorImplementation<Operator.MiscellaneousType> {
		/// <summary>
		/// Initializes a new instance of the <see cref="Miscellaneous"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		public Miscellaneous (Operator.MiscellaneousType type)
			: base (type)
		{
		}
	}
}
