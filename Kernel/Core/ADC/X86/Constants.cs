// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT;

namespace SharpOS.Kernel.ADC.X86 {
	// TODO: ...shouldn't this be in SharpOS.AOT.X86.Asm?
	[System.Flags]
	internal enum CR0 : uint {
		PE = (1U << 0),		// protected mode
		MP = (1U << 1),		// math present
		EM = (1U << 2),		// emulate numeric extension
		TS = (1U << 3),		// task switched
		ET = (1U << 4),		// extension type
		NE = (1U << 5),		// numeric error enable

		WP = (1U << 16),	// write protect

		AM = (1U << 18),	// alignment mask

		NW = (1U << 29),	// not write-through
		CD = (1U << 30),	// cache disable
		PG = (1U << 31)		// paging enable
	}
}