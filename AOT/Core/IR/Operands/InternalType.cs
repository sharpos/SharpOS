//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
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
using Mono.Cecil;


namespace SharpOS.AOT.IR.Operands {
	/// <summary>
	///
	/// </summary>
	public enum InternalType {
		/// <summary>
		/// The internal type of this operand has not yet been set.
		/// </summary>
		NotSet,
		/// <summary>
		/// A one byte signed integer
		/// </summary>
		I1,
		/// <summary>
		/// A one byte unsigned integer
		/// </summary>
		U1,

		/// <summary>
		/// A two byte signed integer
		/// </summary>
		I2,
		/// <summary>
		/// A two byte unsigned integer
		/// </summary>
		U2,

		/// <summary>
		/// A four byte signed integer
		/// </summary>
		I4,
		/// <summary>
		/// A four byte unsigned integer
		/// </summary>
		U4,
		/// <summary>
		/// Signed integer of native size (as determined by architecture)
		/// </summary>
		I,
		/// <summary>
		/// Unsigned integer of native size (as determined by architecture)
		/// </summary>
		U,

		/// <summary>
		/// Eight byte signed integer
		/// </summary>
		I8,
		/// <summary>
		/// Eight byte unsigned integer
		/// </summary>
		U8,

		/// <summary>
		///
		/// </summary>
		R4,
		/// <summary>
		///
		/// </summary>
		R8,
		/// <summary>
		///
		/// </summary>
		F,

		/// <summary>
		/// A value passed "by-value".
		/// </summary>
		ValueType,
		/// <summary>
		/// An object
		/// </summary>
		O,
		/// <summary>
		/// A managed pointer
		/// </summary>
		M,
		/// <summary>
		/// A typed reference spawned by the mkrefany instruction. Semantics defined by ECMA-335.
		/// </summary>
		TypedReference,
		/// <summary>
		/// Any array value which is not a single-dimension, zero-bound one.
		/// </summary>
		Array,
		/// <summary>
		/// A single-dimension zero-bound array
		/// </summary>
		SZArray
	}
}