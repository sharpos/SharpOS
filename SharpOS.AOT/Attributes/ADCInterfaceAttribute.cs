// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.Text;
using SharpOS.AOT;

namespace SharpOS.AOT.Attributes {

	/// <summary>
	/// Marks the namespace for a virtual ADC implementation contained
	/// in an assembly. This is used to support nested namespaces.
	/// </summary>
	[AttributeUsage (AttributeTargets.Assembly)]
	public class ADCInterfaceAttribute : Attribute {
		public ADCInterfaceAttribute (string ns)
		{
			Namespace = ns;
		}
		
		public string Namespace;
	}	
}
