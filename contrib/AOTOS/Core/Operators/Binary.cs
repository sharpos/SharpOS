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
    public class Binary : OperatorImplementation<Operator.BinaryType>
    {
        public Binary(Operator.BinaryType type)
            : base(type)
        {
        }
    }
}