//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Text;
using SharpOS.AOT;

namespace SharpOS.AOT.Attributes {
	/// <summary>
	/// Used to mark the method that is responsable for handling null reference exceptions.
	/// </summary>
	[AttributeUsage (AttributeTargets.Method)]
	public sealed class NullReferenceHandlerAttribute : Attribute {
	}
}
