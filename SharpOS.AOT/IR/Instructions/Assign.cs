/**
 *  (C) 2006-2007 The SharpOS Project Team - http://www.sharpos.org
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
 * 
 *  Author: Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 */

using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using SharpOS.AOT.IR.Operands;

namespace SharpOS.AOT.IR.Instructions
{
    [Serializable]
    public class Assign : SharpOS.AOT.IR.Instructions.Instruction
    {
        public Assign(Identifier asignee, Operand value)
            : base(value)
        {
            this.asignee = asignee;
        }

        private Identifier asignee;

        public Identifier Asignee
        {
            get { return asignee; }
        }

        public override void Dump(string prefix, StringBuilder stringBuilder)
        {
            stringBuilder.Append(prefix + this.FormatedIndex + this.asignee + " = " + this.Value + "\n");
        }
    }
}