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
using System.Text;
using SharpOS.AOT;

namespace SharpOS.AOT.Attributes {
	/// <summary>
	/// Used to mark a method a Method as being naked,
	/// that means no prolog and no epilog.
	/// </summary>
	[AttributeUsage (AttributeTargets.Method)]
	public sealed class NakedAttribute : Attribute {
	}
}
