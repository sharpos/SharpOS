//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections.Generic;
using System.Text;
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.Foundation;
using SharpOS.Korlib.Runtime;

namespace SharpOS.Kernel.BlockDevice
{
	public unsafe struct ByteBuffer
	{
		public byte* data;

		public static ByteBuffer Allocate(uint size)
		{
			ByteBuffer wrapper;
			if (size > 0)
				wrapper.data = (byte*)MemoryManager.Allocate(size);
			else
				wrapper.data = null;
			return wrapper;
		}

		public static void Allocate(ByteBuffer wrapper, uint size)
		{
			wrapper.data = (byte*)MemoryManager.Allocate(size);
		}

		public static void Release(ByteBuffer wrapper)
		{
			if (wrapper.data != null)
				MemoryManager.Free(wrapper.data);
		}

		public static void Copy(ByteBuffer src, ByteBuffer dest, uint srcoffset, uint destoffset, uint destsize)
		{
			MemoryUtil.MemCopy((uint)(src.data + srcoffset), (uint)(dest.data + destoffset), destsize);
		}

		static public char GetChar(ByteBuffer buf, uint offset)
		{
			return (char)(buf.data[offset]);
		}

		static public void SetChar(ByteBuffer buf, uint offset, char value)
		{
			buf.data[offset] = (byte)value;
		}

		static public void SetChars(ByteBuffer buf, uint offset, char[] value)
		{
			for (int index = 0; index < value.Length; index++)
				buf.data[offset + index] = (byte)value[index];
		}

		static public void SetString(ByteBuffer buf, uint offset, string value)
		{
			for (int index = 0; index < value.Length; index++)
				buf.data[offset + index] = (byte)value[index];
		}

		static public uint GetUInt(ByteBuffer buf, uint offset)
		{
			uint value = buf.data[offset++];
			value += (uint)(buf.data[offset++] << 8);
			value += (uint)(buf.data[offset++] << 16);
			value += (uint)(buf.data[offset++] << 24);

			return value;
		}

		static public void SetUInt(ByteBuffer buf, uint offset, uint value)
		{
			buf.data[offset++] = (byte)(value & 0xFF);
			buf.data[offset++] = (byte)((value >> 8) & 0xFF);
			buf.data[offset++] = (byte)((value >> 16) & 0xFF);
			buf.data[offset++] = (byte)((value >> 24) & 0xFF);
		}

		static public ushort GetUShort(ByteBuffer buf, uint offset)
		{
			ushort value = buf.data[offset++];
			value += (ushort)(buf.data[offset++] << 8);

			return value;
		}

		static public void SetUShort(ByteBuffer buf, uint offset, ushort value)
		{
			buf.data[offset++] = (byte)(value & 0xFF);
			buf.data[offset++] = (byte)((value >> 8) & 0xFF);
		}

		static public int GetInt(ByteBuffer buf, uint offset)
		{
			return (int)GetUInt(buf, offset);
		}

		static public void SetInt(ByteBuffer buf, uint offset, int value)
		{
			SetUInt(buf, offset, (uint)value);
		}

		static public byte GetByte(ByteBuffer buf, uint offset)
		{
			return buf.data[offset];
		}

		static public void SetByte(ByteBuffer buf, uint offset, byte value)
		{
			buf.data[offset] = value;
		}

		static public byte* GetByteArray(ByteBuffer buf, uint count, uint offset)
		{
			byte* result = (byte*)MemoryManager.Allocate(count);

			for (int i = 0; i < count; ++i)
				result[i] = buf.data[offset + count];

			return result;
		}

		static public void SetByteArray(ByteBuffer buf, uint count, uint offset, byte* values)
		{
			for (int i = 0; i < count; i++)
				buf.data[offset + i] = values[i];
		}

		static public uint* GetUIntArray(ByteBuffer buf, uint count, uint offset)
		{
			uint* result = (uint*)MemoryManager.Allocate(sizeof(uint) * count);

			for (int i = 0; i < count; i++) {
				uint value = buf.data[offset++];
				value += (uint)(buf.data[offset++] << 8);
				value += (uint)(buf.data[offset++] << 16);
				value += (uint)(buf.data[offset++] << 24);

				result[i] = value;
			}

			return result;
		}

		static public void SetUIntArray(ByteBuffer buf, uint count, uint offset, uint* values)
		{
			for (int i = 0; i < count; i++) {
				uint value = values[i];

				buf.data[offset++] = (byte)(value & 0xFF);
				buf.data[offset++] = (byte)((value >> 8) & 0xFF);
				buf.data[offset++] = (byte)((value >> 16) & 0xFF);
				buf.data[offset++] = (byte)((value >> 24) & 0xFF);
			}
		}
	}
}