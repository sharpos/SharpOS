//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Text;

namespace Ext2 {
	public class Block {
		public Block (uint offset, byte [] buffer)
		{
			this._offset = offset;
			this.buffer = buffer;
			this.dirty = false;
		}

		private uint _offset;
		public byte [] buffer;
		private bool dirty;

		public bool Dirty
		{
			get
			{
				return this.dirty;
			}
			set
			{
				this.dirty = value;
			}
		}

		public uint Offset
		{
			get
			{
				return this._offset;
			}
		}

		public byte [] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		public uint GetUInt (uint offset)
		{
			uint value = this.buffer [offset++];
			value += (uint) (this.buffer [offset++] << 8);
			value += (uint) (this.buffer [offset++] << 16);
			value += (uint) (this.buffer [offset++] << 24);

			return value;
		}

		public void SetUInt (uint offset, uint value)
		{
			this.dirty = true;

			this.buffer [offset++] = (byte) (value & 0xFF);
			this.buffer [offset++] = (byte) ((value >> 8) & 0xFF);
			this.buffer [offset++] = (byte) ((value >> 16) & 0xFF);
			this.buffer [offset++] = (byte) ((value >> 24) & 0xFF);
		}

		public ushort GetUShort (uint offset)
		{
			ushort value = this.buffer [offset++];
			value += (ushort) (this.buffer [offset++] << 8);

			return value;
		}

		public void SetUShort (uint offset, ushort value)
		{
			this.dirty = true;

			this.buffer [offset++] = (byte) (value & 0xFF);
			this.buffer [offset++] = (byte) ((value >> 8) & 0xFF);
		}

		public int GetInt (uint offset)
		{
			return (int) this.GetUInt (offset);
		}

		public void SetInt (uint offset, int value)
		{
			this.SetUInt (offset, (uint) value);
		}

		public byte GetByte (uint offset)
		{
			return this.buffer [offset];
		}

		public void SetByte (uint offset, byte value)
		{
			this.dirty = true;

			this.buffer [offset] = value;
		}

		public string GetString (uint length, uint offset)
		{
			return Encoding.ASCII.GetString (this.buffer, (int) offset, (int) length);
		}

		public byte [] GetByteArray (uint count, uint offset)
		{
			byte [] result = new byte [count];

			Array.Copy (this.buffer, offset, result, 0, count);

			return result;
		}

		public void SetByteArray (uint count, uint offset, byte [] values)
		{
			this.dirty = true;

			for (int i = 0; i < count; i++)
				this.buffer [offset + i] = values [i];
		}

		public uint [] GetUIntArray (uint count, uint offset)
		{
			uint [] result = new uint [count];

			for (int i = 0; i < count; i++) {
				uint value = this.buffer [offset++];
				value += (uint) (this.buffer [offset++] << 8);
				value += (uint) (this.buffer [offset++] << 16);
				value += (uint) (this.buffer [offset++] << 24);

				result [i] = value;
			}

			return result;
		}

		public void SetUIntArray (uint count, uint offset, uint [] values)
		{
			this.dirty = true;

			for (int i = 0; i < count; i++) {
				uint value = values [i];

				this.buffer [offset++] = (byte) (value & 0xFF);
				this.buffer [offset++] = (byte) ((value >> 8) & 0xFF);
				this.buffer [offset++] = (byte) ((value >> 16) & 0xFF);
				this.buffer [offset++] = (byte) ((value >> 24) & 0xFF);
			}
		}
	}
}
