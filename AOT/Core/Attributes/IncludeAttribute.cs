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
	/// Used to mark the classes in the SharpOS.AOT.Core that will be needed by the Kernel.
	/// </summary>
	[AttributeUsage (AttributeTargets.Class | AttributeTargets.Enum)]
	public sealed class IncludeAttribute : Attribute {
	}
}
