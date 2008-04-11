//
// (C) 2006-2008 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Michael Ruck (aka grover) <sharpos@michaelruck.de>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT.Attributes;

namespace InternalSystem.IO {
	[TargetNamespace("System.IO")]
	public enum FileAccess {
		Read,
		Write,
		ReadWrite
	}
}
