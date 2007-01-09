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
    public class Relational : OperatorImplementation<Operator.RelationalType>
    {
        public Relational(Operator.RelationalType type)
            : base(type)
        {
        }

        public Relational Negate()
        {
            Relational result = null;

            switch (this.Type)
            {
                case Operator.RelationalType.Equal:
                    result = new Relational(Operator.RelationalType.NotEqualOrUnordered);
                    break;

                case Operator.RelationalType.NotEqualOrUnordered:
                    result = new Relational(Operator.RelationalType.Equal);
                    break;

                case Operator.RelationalType.GreaterThan:
                    result = new Relational(Operator.RelationalType.LessThanOrEqual);
                    break;

                case Operator.RelationalType.GreaterThanUnsignedOrUnordered:
                    result = new Relational(Operator.RelationalType.LessThanOrEqualUnsignedOrUnordered);
                    break;

                case Operator.RelationalType.GreaterThanOrEqual:
                    result = new Relational(Operator.RelationalType.LessThan);
                    break;

                case Operator.RelationalType.GreaterThanOrEqualUnsignedOrUnordered:
                    result = new Relational(Operator.RelationalType.LessThanUnsignedOrUnordered);
                    break;

                case Operator.RelationalType.LessThan:
                    result = new Relational(Operator.RelationalType.GreaterThanOrEqual);
                    break;

                case Operator.RelationalType.LessThanUnsignedOrUnordered:
                    result = new Relational(Operator.RelationalType.GreaterThanOrEqualUnsignedOrUnordered);
                    break;

                case Operator.RelationalType.LessThanOrEqual:
                    result = new Relational(Operator.RelationalType.GreaterThan);
                    break;

                case Operator.RelationalType.LessThanOrEqualUnsignedOrUnordered:
                    result = new Relational(Operator.RelationalType.GreaterThanUnsignedOrUnordered);
                    break;
            }

            return result;
        }

    }
}