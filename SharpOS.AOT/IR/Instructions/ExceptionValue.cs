/**
 *  (C) 2006-2007 The SharpOS Project Team - http://www.sharpos.org
 *
 *  Licensed under the terms of the GNU GPL License version 2.
 *
 *  Author: Mircea-Cristian Racasan <darx_kies@gmx.net>
 *
 */

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using SharpOS.AOT.IR.Operands;
using SharpOS.AOT.IR.Operators;
using Mono.Cecil;

namespace SharpOS.AOT.IR.Instructions {
	[Serializable]
	public class ExceptionValue : Identifier {
		public ExceptionValue ()
			: base ("ExceptionValue", 0)
		{
		}
	}
}
