// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//  Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries

using System;
using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.HAL
{
	public interface IReadOnlyIOPort
	{
		uint Port { get; }
		byte Read8 ();
		UInt16 Read16 ();
		UInt32 Read32 ();
	}

	public interface IWriteOnlyIOPort
	{
		uint Port { get; }
		void Write8 (byte value);
		void Write16 (UInt16 value);
		void Write32 (UInt32 value);
	}

	public interface IReadWriteIOPort : IReadOnlyIOPort, IWriteOnlyIOPort
	{

	}

	public class IOPort : IReadOnlyIOPort, IWriteOnlyIOPort, IReadWriteIOPort
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
			return IO.Read8 (port);
		}

		public UInt16 Read16 ()
		{
			return IO.Read16 (port);
		}

		public UInt32 Read32 ()
		{
			return IO.Read32 (port);
		}

		#endregion

		#region Write

		public void Write8 (byte value)
		{
			IO.Write8 (port, value);
		}

		public void Write16 (UInt16 value)
		{
			IO.Write16 (port, value);
		}

		public void Write32 (UInt32 value)
		{
			IO.Write32 (port, value);
		}

		#endregion

	}
}
