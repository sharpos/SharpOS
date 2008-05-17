// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//  Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries

using System;

namespace SharpOS.Kernel.ADC
{
	public class IOPort
	{
		private uint port;

		public IOPort (uint port)
		{
			this.port = port;
		}

		public uint Port
		{
			get
			{
				return port;
			}
		}
		
		#region Read

		public byte Read8 ()
		{
			return IOPortAccess.Read8 (port);
		}

		public UInt16 Read16 ()
		{
			return IOPortAccess.Read16 (port);
		}

		public UInt32 Read32 ()
		{
			return IOPortAccess.Read32 (port);
		}

		#endregion

		#region Write

		public void Write8 (byte value)
		{
			IOPortAccess.Write8 (port, value);
		}

		public void Write16 (UInt16 value)
		{
			IOPortAccess.Write16 (port, value);
		}

		public void Write32 (UInt32 value)
		{
			IOPortAccess.Write32 (port, value);
		}

		#endregion

	}
}
