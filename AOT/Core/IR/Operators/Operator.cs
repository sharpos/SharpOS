// 
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
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
			Localloc,
			InitObj
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
