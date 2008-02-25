// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections.Generic;
using System.Text;

namespace SharpOS.Kernel.ADC {

	public abstract class IOPortStream {
		
		public abstract sbyte	ReadSByte();
		public abstract byte	ReadByte();
		public abstract Int16	ReadInt16();
		public abstract UInt16	ReadUInt16();
		public abstract Int32	ReadInt32();
		public abstract UInt32	ReadUInt32();
		public abstract Int64	ReadInt64();
		public abstract UInt64	ReadUInt64();

		public abstract void Write(sbyte value);
		public abstract void Write(byte value);
		public abstract void Write(Int16 value);
		public abstract void Write(UInt16 value);
		public abstract void Write(Int32 value);
		public abstract void Write(UInt32 value);
		public abstract void Write(Int64 value);
		public abstract void Write(UInt64 value);
		public abstract void Write(byte[] buffer);

	}
}
