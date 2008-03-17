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

	// TODO: ...eventually use generics?
	// TODO: use stubs for fill/move functions
	// TODO: do bound checks on fill/move functions
	public unsafe struct MemoryBlock
	{
		internal MemoryBlock (uint address, uint length)
		{
			this.length = length;
			this.address = (byte*)address;
		}

		public MemoryBlock (uint length)
		{
			address = (byte*)MemoryManager.Allocate((uint)length);
			this.length = length;
		}

		internal uint length;
		internal byte* address;

		public uint Length
		{
			get
			{
				return this.length;
			}
		}

		public byte this[int index]
		{
			get
			{
				return address[index];
			}
			set
			{
				address[index] = value;
			}
		}

		public MemoryBlock Offset (uint offset)
		{
			return new MemoryBlock((uint)address + offset, length - offset);
		}

		public void Allocate (uint count)
		{
			address = (byte*)MemoryManager.Allocate((uint)count);
			length = count;
		}

		public void Release ()
		{
			MemoryManager.Free(address);
			address = null;
			length = 0;
		}

		public void Fill (uint pattern)
		{
			ADC.X86.MemoryUtil.MemSet(pattern, (uint)address, length);
		}

		internal void Fill (uint pattern, uint index, uint count)
		{
			ADC.X86.MemoryUtil.MemSet(pattern, (uint)address + index, length);
		}

		internal void Move (uint source, uint dest, uint count)
		{
			ADC.X86.MemoryUtil.MemCopy(source, dest, count);
		}

		internal void CopyTo (MemoryBlock destination, uint count)
		{
			ADC.X86.MemoryUtil.MemCopy((uint)address, (uint)destination.address, count);
		}

		internal void CopyFrom (MemoryBlock source, uint count)
		{
			ADC.X86.MemoryUtil.MemCopy((uint)source.address, (uint)address, count);
		}

		public char GetChar (uint offset)
		{
			return (char)(address[offset]);
		}

		public void SetChar (uint offset, char value)
		{
			address[offset] = (byte)value;
		}

		public void SetChars (uint offset, char[] value)
		{
			for (int index = 0; index < value.Length; index++)
				address[offset + index] = (byte)value[index];
		}

		public void SetString (uint offset, string value)
		{
			for (int index = 0; index < value.Length; index++)
				address[offset + index] = (byte)value[index];
		}

		public uint GetUInt (uint offset)
		{
			uint value = address[offset++];
			value += (uint)(address[offset++] << 8);
			value += (uint)(address[offset++] << 16);
			value += (uint)(address[offset++] << 24);

			return value;
		}

		public void SetUInt (uint offset, uint value)
		{
			address[offset++] = (byte)(value & 0xFF);
			address[offset++] = (byte)((value >> 8) & 0xFF);
			address[offset++] = (byte)((value >> 16) & 0xFF);
			address[offset++] = (byte)((value >> 24) & 0xFF);
		}

		public ushort GetUShort (uint offset)
		{
			ushort value = address[offset++];
			value += (ushort)(address[offset++] << 8);

			return value;
		}

		public void SetUShort (uint offset, ushort value)
		{
			address[offset++] = (byte)(value & 0xFF);
			address[offset++] = (byte)((value >> 8) & 0xFF);
		}

		public int GetInt (uint offset)
		{
			return (int)GetUInt(offset);
		}

		public void SetInt (uint offset, int value)
		{
			SetUInt(offset, (uint)value);
		}

		public byte GetByte (uint offset)
		{
			return address[offset];
		}

		public void SetByte (uint offset, byte value)
		{
			address[offset] = value;
		}

	}
}
