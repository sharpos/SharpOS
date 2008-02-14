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

	public class FieldMetadata : Metadata {
		public FieldMetadata (AssemblyMetadata assembly, FieldRow row):
			base (assembly)
		{
			this.fieldRow = row; //fieldRow;
			this.signature = new FieldSignature (assembly, row);
		}

		FieldRow fieldRow;
		FieldSignature signature;

		public override void Free ()
		{
			signature.Free ();
			Runtime.Free (this);
		}
	}
}
