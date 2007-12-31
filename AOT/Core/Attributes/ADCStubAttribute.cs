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
	/// Methods which carry this attribute act as stubs into
	/// the ADC layer in use at AOT compile-time.
	/// </summary>
	[AttributeUsage (AttributeTargets.Method)]
	public sealed class ADCStubAttribute : Attribute {
	}

}
