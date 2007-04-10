/**
 *  (C) 2006-2007 The SharpOS Project Team - http://www.sharpos.org
 *
 *  Licensed under the terms of the GNU GPL License version 2.
 *
 *  Author: Mircea-Cristian Racasan <darx_kies@gmx.net>
 *
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace SharpOS.AOT.IR.Operators {
	public class OperatorImplementation<TYPE> : Operator {
		public OperatorImplementation (TYPE type)
		{
			this.type = type;
		}

		private TYPE type;

		public TYPE Type {
			get {
				return type;
			}
		}

		public override string ToString()
		{
			return this.type.ToString();
		}
	}
}
