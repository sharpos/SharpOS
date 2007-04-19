// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.Collections.Generic;
using System.Text;

namespace SharpOS.AOT.IR.Operators {
	[Serializable]
	public abstract class Operator {
		public enum UnaryType {
			Negation, 
			Not, 
			Unbox, 
			ArraySize
		}

		public enum MiscellaneousType {
			Throw, 
			NewArray, 
			InternalList, 
			Argument,
			Localloc
		}

		public enum BinaryType {
			Add, 
			AddSignedWithOverflowCheck, 
			AddUnsignedWithOverflowCheck, 
			Sub, 
			SubSignedWithOverflowCheck, 
			SubUnsignedWithOverflowCheck, 
			Mul, 
			MulSignedWithOverflowCheck, 
			MulUnsignedWithOverflowCheck, 
			Div, 
			DivUnsigned, 
			Remainder, 
			RemainderUnsigned, 
			SHL, 
			SHR, 
			SHRUnsigned, 
			And, 
			Or, 
			Xor
		}

		public enum BooleanType {
			True, 
			False, 
			And, 
			Or, 
			Conditional
		}

		public enum RelationalType {
			Equal, 
			GreaterThan, 
			GreaterThanUnsignedOrUnordered, 
			GreaterThanOrEqual, 
			GreaterThanOrEqualUnsignedOrUnordered, 
			LessThan, 
			LessThanUnsignedOrUnordered, 
			LessThanOrEqual, 
			LessThanOrEqualUnsignedOrUnordered, 
			NotEqualOrUnordered
		}
	}
}
