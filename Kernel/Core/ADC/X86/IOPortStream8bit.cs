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

namespace SharpOS.Kernel.ADC.X86 {

	public class IOPortStream8bit : IOPortStream {

		#region Constructor
		internal IOPortStream8bit(IO.Port _port) 
			: base(_port)
		{
		}
		#endregion

		#region Read

		#region ReadSByte
		public override sbyte	ReadSByte()
		{
			return IO.ReadSByte(port);
		}
		#endregion
		
		#region ReadByte
		public override byte	ReadByte()
		{
			return IO.ReadByte(port);
		}
		#endregion
		
		#region ReadInt16
		public override Int16	ReadInt16()
		{
			byte	value0 = IO.ReadByte(port);
			byte	value1 = IO.ReadByte(port);

			unchecked
			{
				return  (Int16)(
						((UInt16)value0 << 0) +
						((UInt16)value1 << 8));
			}
		}
		#endregion
		
		#region ReadUInt16
		public override UInt16	ReadUInt16()
		{
			byte	value0 = IO.ReadByte(port);
			byte	value1 = IO.ReadByte(port);

			return  (UInt16)(
					((UInt16)value0 << 0) +
					((UInt16)value1 << 8));
		}
		#endregion
		
		#region ReadInt32
		public override Int32	ReadInt32()
		{
			byte	value0 = IO.ReadByte(port);
			byte	value1 = IO.ReadByte(port);
			byte	value2 = IO.ReadByte(port);
			byte	value3 = IO.ReadByte(port);

			unchecked
			{
				return  (Int32)(
						((UInt32)value0 <<  0) +
						((UInt32)value1 <<  8) +
						((UInt32)value2 << 16) +
						((UInt32)value3 << 24));
			}
		}
		#endregion
		
		#region ReadUInt32
		public override UInt32	ReadUInt32()
		{
			byte	value0 = IO.ReadByte(port);
			byte	value1 = IO.ReadByte(port);
			byte	value2 = IO.ReadByte(port);
			byte	value3 = IO.ReadByte(port);
			
			return  (UInt32)(
					((UInt32)value0 <<  0) +
					((UInt32)value1 <<  8) +
					((UInt32)value2 << 16) +
					((UInt32)value3 << 24));
		}
		#endregion
		
		#region ReadInt64
		public override Int64	ReadInt64()
		{
			byte	value0 = IO.ReadByte(port);
			byte	value1 = IO.ReadByte(port);
			byte	value2 = IO.ReadByte(port);
			byte	value3 = IO.ReadByte(port);
			byte	value4 = IO.ReadByte(port);
			byte	value5 = IO.ReadByte(port);
			byte	value6 = IO.ReadByte(port);
			byte	value7 = IO.ReadByte(port);

			unchecked
			{
				return  (Int64)(
						((UInt64)value0 <<  0) +
						((UInt64)value1 <<  8) +
						((UInt64)value2 << 16) +
						((UInt64)value3 << 24) +
						((UInt64)value4 << 32) +
						((UInt64)value5 << 40) +
						((UInt64)value6 << 48) +
						((UInt64)value7 << 56));
			}
		}
		#endregion
		
		#region ReadUInt64
		public override UInt64	ReadUInt64()
		{
			byte	value0 = IO.ReadByte(port);
			byte	value1 = IO.ReadByte(port);
			byte	value2 = IO.ReadByte(port);
			byte	value3 = IO.ReadByte(port);
			byte	value4 = IO.ReadByte(port);
			byte	value5 = IO.ReadByte(port);
			byte	value6 = IO.ReadByte(port);
			byte	value7 = IO.ReadByte(port);
			
			return  (UInt64)(
					((UInt64)value0 <<  0) +
					((UInt64)value1 <<  8) +
					((UInt64)value2 << 16) +
					((UInt64)value3 << 24) +
					((UInt64)value4 << 32) +
					((UInt64)value5 << 40) +
					((UInt64)value6 << 48) +
					((UInt64)value7 << 56));
		}
		#endregion

		#endregion
				
		#region Write
		
		#region Write SByte
		public override void Write(sbyte value)
		{
			IO.WriteSByte(port, value);
		}
		#endregion
		
		#region Write Byte
		public override void Write(byte value)
		{
			IO.WriteByte(port, value);
		}
		#endregion
		
		#region Write Int16
		public override void Write(Int16 value)
		{
			unchecked
			{
				IO.WriteByte(port, (byte)(((UInt16)value      ) & 255));
				IO.WriteByte(port, (byte)(((UInt16)value >>  8) & 255));
			}
		}
		#endregion
		
		#region Write UInt16
		public override void Write(UInt16 value)
		{
			IO.WriteByte(port, (byte)((value      ) & 255));
			IO.WriteByte(port, (byte)((value >>  8) & 255));
		}
		#endregion
		
		#region Write Int32
		public override void Write(Int32 value)
		{
			unchecked
			{
				IO.WriteByte(port, (byte)(((UInt32)value      ) & 255));
				IO.WriteByte(port, (byte)(((UInt32)value >>  8) & 255));
				IO.WriteByte(port, (byte)(((UInt32)value >> 16) & 255));
				IO.WriteByte(port, (byte)(((UInt32)value >> 24) & 255));
			}
		}
		#endregion
		
		#region Write UInt32
		public override void Write(UInt32 value)
		{
			IO.WriteByte(port, (byte)((value      ) & 255));
			IO.WriteByte(port, (byte)((value >>  8) & 255));
			IO.WriteByte(port, (byte)((value >> 16) & 255));
			IO.WriteByte(port, (byte)((value >> 24) & 255));
		}
		#endregion
		
		#region Write Int64
		public override void Write(Int64 value)
		{
			unchecked
			{
				//TODO: remove double cast (byte)(int) when direct case from ulong to byte works
				IO.WriteByte(port, (byte)(int)(((UInt64)value      ) & 255));
				IO.WriteByte(port, (byte)(int)(((UInt64)value >>  8) & 255));
				IO.WriteByte(port, (byte)(int)(((UInt64)value >> 16) & 255));
				IO.WriteByte(port, (byte)(int)(((UInt64)value >> 24) & 255));
				IO.WriteByte(port, (byte)(int)(((UInt64)value >> 32) & 255));
				IO.WriteByte(port, (byte)(int)(((UInt64)value >> 40) & 255));
				IO.WriteByte(port, (byte)(int)(((UInt64)value >> 48) & 255));
				IO.WriteByte(port, (byte)(int)(((UInt64)value >> 56) & 255));
			}
		}
		#endregion
		
		#region Write UInt64
		public override void Write(UInt64 value)
		{
			//TODO: remove double cast (byte)(int) when direct case from ulong to byte works
			IO.WriteByte(port, (byte)(int)((value      ) & 255));
			IO.WriteByte(port, (byte)(int)((value >>  8) & 255));
			IO.WriteByte(port, (byte)(int)((value >> 16) & 255));
			IO.WriteByte(port, (byte)(int)((value >> 24) & 255));
			IO.WriteByte(port, (byte)(int)((value >> 32) & 255));
			IO.WriteByte(port, (byte)(int)((value >> 40) & 255));
			IO.WriteByte(port, (byte)(int)((value >> 48) & 255));
			IO.WriteByte(port, (byte)(int)((value >> 56) & 255));
		}
		#endregion
		
		#region Write byte[]
		public override void Write(byte[] buffer)
		{
			foreach(Byte value in buffer)
				IO.WriteByte(port, value);
		}
		#endregion
		
		#endregion
	}
}
