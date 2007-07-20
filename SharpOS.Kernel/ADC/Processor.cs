//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using SharpOS.AOT;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.ADC {

	public unsafe struct Processor {
		uint ID;
		Architecture ArchType;
		byte *VendorName;
		uint VendorID;
		byte *FamilyName;
		uint FamilyID;
		byte *ModelName;
		uint ModelID;
		uint ClockSpeed;
		uint CacheSize;
		void *Flags;
	}
}
