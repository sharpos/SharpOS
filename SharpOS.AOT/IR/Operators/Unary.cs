/**
 *  (C) 2006-2007 Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace SharpOS.AOT.IR.Operators
{
    [Serializable]
    public class Unary : OperatorImplementation<Operator.UnaryType>
    {
        public Unary(Operator.UnaryType type)
            : base(type)
        {
        }
    }
}