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
    public abstract class Operator
    {
        public enum UnaryType
        {
            Negation
            , Not
            , Unbox
            , ArraySize
        }

        public enum MiscellaneousType
        {
            Throw
            , NewArray
            , InternalList
            , Argument
        }

        public enum BinaryType
        {
            Add
            , AddSignedWithOverflowCheck
            , AddUnsignedWithOverflowCheck
            , And
            , Div
            , DivUnsigned
            , Mul
            , MulSignedWithOverflowCheck
            , MulUnsignedWithOverflowCheck
            , Remainder
            , RemainderUnsigned
            , SHL
            , SHR
            , SHRUnsigned
            , Sub
            , SubSignedWithOverflowCheck
            , SubUnsignedWithOverflowCheck
            , Or
            , Xor
        }

        public enum BooleanType
        {
            True
            , False
            , And
            , Or
            , Conditional
        }
        public enum RelationalType
        {
            Equal
            , GreaterThan
            , GreaterThanUnsignedOrUnordered
            , GreaterThanOrEqual
            , GreaterThanOrEqualUnsignedOrUnordered
            , LessThan
            , LessThanUnsignedOrUnordered
            , LessThanOrEqual
            , LessThanOrEqualUnsignedOrUnordered
            , NotEqualOrUnordered
        }
    }

    public class OperatorImplementation<TYPE>: Operator
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


    [Serializable]
    public class Unary : OperatorImplementation<Operator.UnaryType>
    {
        public Unary(Operator.UnaryType type)
            : base(type)
        {
        }
    }

    [Serializable]
    public class Binary : OperatorImplementation<Operator.BinaryType>
    {
        public Binary(Operator.BinaryType type)
            : base(type)
        {
        }
    }

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

    [Serializable]
    public class Miscellaneous : OperatorImplementation<Operator.MiscellaneousType>
    {
        public Miscellaneous(Operator.MiscellaneousType type)
            : base(type)
        {
        }
    }
}
