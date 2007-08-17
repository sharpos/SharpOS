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
		public uint ID;
		public Architecture ArchType;
		public byte *VendorName;
		public uint VendorID;
		public byte *FamilyName;
		public uint FamilyID;
		public byte *ModelName;
		public uint ModelID;
		public uint ClockSpeed;
		public uint CacheSize;
		public void *Flags;
	}
}
