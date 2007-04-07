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
    public class Boolean : OperatorImplementation<Operator.BooleanType>
    {
        public Boolean(Operator.BooleanType type)
            : base(type)
        {
        }

        public Boolean Negate()
        {
            Boolean result = null;

            switch (this.Type)
            {
                case Operator.BooleanType.True:
                    result = new Boolean(BooleanType.False);
                    break;

                case Operator.BooleanType.False:
                    result = new Boolean(BooleanType.True);
                    break;

                case Operator.BooleanType.And:
                    result = new Boolean(BooleanType.Or);
                    break;

                case Operator.BooleanType.Or:
                    result = new Boolean(BooleanType.And);
                    break;

                case Operator.BooleanType.Conditional:
                    result = new Boolean(BooleanType.Conditional);
                    break;
            }

            return result;
        }
    }
}