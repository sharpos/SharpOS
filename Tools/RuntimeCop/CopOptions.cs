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
using System.IO;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.GetOptions;

public class CopOptions : Options {
	public CopOptions (string [] args)
		:
		base (args)
	{
	}
}

