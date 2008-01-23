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

namespace SharpOS.AOT.Attributes {
	/// <summary>
	/// 
	/// </summary>
	[AttributeUsage (AttributeTargets.Method)]
	public sealed class LabelAttribute : Attribute {
		/// <summary>
		/// Initializes a new instance of the <see cref="LabelAttribute"/> class.
		/// </summary>
		/// <param name="label">The label.</param>
		public LabelAttribute (string label)
		{
		}
	}
}
