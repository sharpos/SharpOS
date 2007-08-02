// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.Text;

namespace SharpOS.AOT.Attributes {
	[AttributeUsage (AttributeTargets.Method)]
	public sealed class LabelAttribute : Attribute {
		public LabelAttribute (string label)
		{
		}
	}
}
