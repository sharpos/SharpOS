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
using SharpOS.AOT.IR.Operators;
using Mono.Cecil;

namespace SharpOS.AOT.IR.Operands
{
    [Serializable]
    public class Reference : Identifier
    {
        public Reference(Operand operand)
            : base("ref", new Operand[] { operand })
        {

        }

        public override string ID
        {
            get
            {
                return this.operands[0].ID;
            }
        }

        public new Identifier Value
        {
            get
            {
                return this.operands[0] as Identifier;
            }

            set
            {
                this.operands[0] = value;
            }
        }

        public override string ToString()
        {
            return "ref(" + this.Value.ToString() + ")";
        }
    }
}