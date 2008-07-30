//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//  Phil Garcia (aka tgiphil) <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.AOT.Attributes;
using SharpOS.Korlib.Runtime;

namespace InternalSystem
{
	[TargetNamespace ("System")]
	public struct RuntimeTypeHandle
	{
		IntPtr value;

		internal RuntimeTypeHandle (IntPtr val)
		{
			value = val;
		}

		public IntPtr Value
		{
			get
			{
				return value;
			}
		}

	}
}
