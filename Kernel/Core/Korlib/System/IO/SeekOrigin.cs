//
// (C) 2006-2008 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT.Attributes;

namespace InternalSystem.IO
{
	[TargetNamespace ("System.IO")]
	public enum SeekOrigin : int
	{
		Begin = 0,
		Current = 1,
		End = 2,
	}
}
