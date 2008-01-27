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
		/// 
		/// </summary>
		NotSet,
		/// <summary>
		/// 
		/// </summary>
		I1,
		/// <summary>
		/// 
		/// </summary>
		U1,

		/// <summary>
		/// 
		/// </summary>
		I2,
		/// <summary>
		/// 
		/// </summary>
		U2,

		/// <summary>
		/// 
		/// </summary>
		I4,
		/// <summary>
		/// 
		/// </summary>
		U4,
		/// <summary>
		/// 
		/// </summary>
		I,
		/// <summary>
		/// 
		/// </summary>
		U,

		/// <summary>
		/// 
		/// </summary>
		I8,
		/// <summary>
		/// 
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
		/// 
		/// </summary>
		ValueType,
		/// <summary>
		/// 
		/// </summary>
		O,
		/// <summary>
		/// 
		/// </summary>
		M,
		/// <summary>
		/// 
		/// </summary>
		TypedReference,
		/// <summary>
		/// 
		/// </summary>
		Array,
		/// <summary>
		/// 
		/// </summary>
		SZArray
	}
}