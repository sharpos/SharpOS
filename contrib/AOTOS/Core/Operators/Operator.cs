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
}