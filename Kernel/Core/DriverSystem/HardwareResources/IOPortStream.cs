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

namespace SharpOS.Kernel.DriverSystem {

	public interface IOPortStream {
		
		sbyte	ReadSByte();
		byte	ReadByte();
		Int16	ReadInt16();
		UInt16	ReadUInt16();
		Int32	ReadInt32();
		UInt32	ReadUInt32();
		Int64	ReadInt64();
		UInt64	ReadUInt64();

		void	Write(sbyte value);
		void	Write(byte value);
		void	Write(Int16 value);
		void	Write(UInt16 value);
		void	Write(Int32 value);
		void	Write(UInt32 value);
		void	Write(Int64 value);
		void	Write(UInt64 value);
		void	Write(byte[] buffer);

	}
}
