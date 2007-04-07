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
    public class Arithmetic : Operand
    {
        public Arithmetic(Unary _operator, Operand operand)
            : base(_operator, new Operand[] { operand })
        {

        }

        public Arithmetic(Binary _operator, Operand first, Operand second)
            : base(_operator, new Operand[] { first, second })
        {

        }
    }
}