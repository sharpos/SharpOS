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
	/// Used to mark a method as the runtime handling of
	/// overflow exceptions.
	/// </summary>
	[AttributeUsage (AttributeTargets.Method)]
	public sealed class OverflowHandlerAttribute : Attribute {
	}
}
