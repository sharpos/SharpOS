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
    public class OperatorImplementation<TYPE> : Operator
    {
        public OperatorImplementation(TYPE type)
        {
            this.type = type;
        }

        private TYPE type;

        public TYPE Type
        {
            get { return type; }
        }

        public override string ToString()
        {
            return this.type.ToString();
        }
    }
}