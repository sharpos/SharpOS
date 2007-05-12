// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	William Lahti <xfurious@gmail.com>
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

namespace SharpOS.AOT.IR.Instructions {
	[Serializable]
	public class Call : SharpOS.AOT.IR.Instructions.Instruction {
		public Call (SharpOS.AOT.IR.Operands.Call value)
			: base (value)
		{
		}

		public SharpOS.AOT.IR.Operands.Call Method {
			get {
				return this.Value as SharpOS.AOT.IR.Operands.Call;
			}
		}

		public override void Dump (DumpProcessor p)
		{
			p.AddElement ("call", this.Method.ToString (), true, true, false);
		}
	}
}