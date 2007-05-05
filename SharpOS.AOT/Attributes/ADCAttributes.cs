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
	/// Marks an assembly as containing an architecture-
	/// dependent layer for the given CPU. Classes in the
	/// layer must match the layout and interface provided
	/// by the SharpOS.ADC framework, and exist in the specified
	/// namespace.
	/// </summary>
	[AttributeUsage (AttributeTargets.Assembly, AllowMultiple = true)]
	public class ADCLayerAttribute : Attribute {
		public ADCLayerAttribute (string cpu, string ns)
		{
			CPU = cpu;
			Namespace = ns;
		}
		
		public string CPU;
		public string Namespace;
	}
	
	/// <summary>
	/// Methods which carry this attribute act as stubs into
	/// the ADC layer in use at AOT compile-time.
	/// </summary>
	[AttributeUsage (AttributeTargets.Method)]
	public class ADCStubAttribute : Attribute {
	}
}
