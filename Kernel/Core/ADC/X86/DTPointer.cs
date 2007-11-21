// 
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Runtime.InteropServices;
using SharpOS;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS.ADC.X86 {
	[StructLayout (LayoutKind.Sequential)]
	public struct DTPointer {
		public const uint SizeOf = 6;

		public ushort Size;
		public uint Address;

		public void Setup (ushort size, uint address)
		{
			this.Size = size;
			this.Address = address;
		}
	}
}

