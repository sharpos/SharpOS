//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

#define DEBUG_EXCEPTION_HANDLING

using System.Runtime.InteropServices;
using SharpOS.AOT.Metadata;
using SharpOS.AOT.Attributes;
using SharpOS.Kernel;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;

namespace SharpOS.Korlib.Runtime {

	/// <summary>
	/// Base class for aggregated metadata information.
	/// NOTE: this has nothing to do with the metadata stored
	/// by the AOT, but is only the base class of objects
	/// which are constructed by iterating through that
	/// information.
	/// </summary>
	public abstract class Metadata {
		public Metadata (AssemblyMetadata assembly)
		{
			this.assembly = assembly;
		}

		AssemblyMetadata assembly;

		public AssemblyMetadata Assembly {
			get { return this.assembly; }
			protected set { this.assembly = value; }
		}

		public abstract void Free ();
	}
}
