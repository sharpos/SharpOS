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
	/// Marks an assembly as containing an architecture-
	/// dependent layer for the given CPU. Classes in the
	/// layer must match the layout and interface provided
	/// by the SharpOS.ADC framework, and exist in the specified
	/// namespace.
	/// </summary>
	[AttributeUsage (AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class ADCLayerAttribute : Attribute {
		public ADCLayerAttribute (string cpu, string ns)
		{
			CPU = cpu;
			Namespace = ns;
		}

		public string CPU;
		public string Namespace;
	}
}
