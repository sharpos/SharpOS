/**
 *  (C) 2006-2007 Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
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
    public class ArrayElement : Identifier
    {
        public ArrayElement(Operand array, Operand index)
            : base(array.ToString(), new Operand[] { array, index })
        {
        }

        public Operand Array
        {
            get
            {
                return this.operands[0];
            }
        }

        public Operand IDX
        {
            get
            {
                return this.operands[1];
            }
        }

        public override string ToString()
        {
            return this.Array + "[" + this.IDX + "]";
        }
    }
}