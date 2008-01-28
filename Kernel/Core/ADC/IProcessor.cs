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
	public unsafe abstract class IProcessor {
		public abstract uint			ID				{ get; }
		
		public abstract ProcessorType	ArchType		{ get; }
		
		public abstract CString8*		VendorName		{ get; }
		public abstract CString8*		BrandName		{ get; }
		public abstract CString8*		FamilyName		{ get; }
		public abstract CString8*		ModelName		{ get; }
		
		public abstract ProcessorFeature[] Features		{ get; }
	}
}
