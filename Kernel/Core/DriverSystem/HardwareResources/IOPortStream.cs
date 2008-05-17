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
using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.DriverSystem
{
	//public interface IOPortReadStream
	//{
	//    byte Read8 ();
	//    UInt16 Read16 ();
	//    UInt32 Read32 ();
	//}

	//public interface IOPortWriteStream
	//{
	//    void Write8 (byte value);
	//    void Write16 (UInt16 value);
	//    void Write32 (UInt32 value);
	//}

	//public interface IOPortReadWriteStream : IOPortReadStream, IOPortWriteStream
	//{
	//}

	public class IOPortStream : IOPort
	{
		public IOPortStream (uint port)
			: base (port)
		{
		}
	}
}
