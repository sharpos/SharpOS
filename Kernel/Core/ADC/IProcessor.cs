//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	Cédric Rousseau <cedrou@gmail.com>
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

	public unsafe interface IProcessor {
		uint			ID				{ get; }

		uint			Index			{ get; }
		
		ProcessorType	ArchType		{ get; }
		
		CString8*		VendorName		{ get; }
		CString8*		BrandName		{ get; }
		CString8*		FamilyName		{ get; }
		CString8*		ModelName		{ get; }
		
		ProcessorFeature[] Features		{ get; }

		void			Halt			();
	}
}
