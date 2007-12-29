// (C) 2006-2007 The SharpOS Project. This software is licensed under the
// terms of the GNU General Public License version 3.0 with the ClassPath
// linking exception.
//
// Authors:
// 	William Lahti <xfurious@gmail.com>
//

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.GetOptions;

public class CopOptions : Options {
	public CopOptions (string [] args):
		base (args)
	{
	}
}

