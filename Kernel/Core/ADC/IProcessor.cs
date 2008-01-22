//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT;
using AOTAttr = SharpOS.AOT.Attributes;
using SharpOS.Kernel.Foundation;
using System;

namespace SharpOS.Kernel.ADC {

	// TODO: eventually do something more interesting besides just holding a string...
	public unsafe class ProcessorFeature {
		public string FeatureName;
	}

	// TODO: turn this into an interface eventually, untill then pretend it's an interface..
	public unsafe class IProcessor {
		public virtual void				Setup()			{ }
		
		public virtual uint				ID				{ get { return 0; } }
		
		public virtual ProcessorType	ArchType		{ get { return ProcessorType.Unknown; } }
		
		public virtual CString8*		VendorName		{ get { return CString8.Copy (""); } }
		public virtual CString8*		BrandName		{ get { return CString8.Copy (""); } }
		public virtual CString8*		FamilyName		{ get { return CString8.Copy (""); } }
		public virtual CString8*		ModelName		{ get { return CString8.Copy (""); } }
		
		public virtual ProcessorFeature[] Features	{ get { return null; } }
	}
}
