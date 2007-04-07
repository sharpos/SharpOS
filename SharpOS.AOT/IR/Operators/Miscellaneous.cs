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

namespace SharpOS.AOT.IR.Operators
{
    [Serializable]
    public class Miscellaneous : OperatorImplementation<Operator.MiscellaneousType>
    {
        public Miscellaneous(Operator.MiscellaneousType type)
            : base(type)
        {
        }
    }
}
