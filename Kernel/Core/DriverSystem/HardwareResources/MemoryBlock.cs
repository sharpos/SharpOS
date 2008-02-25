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

	// TODO: ...eventually use generics?
	// TODO: use stubs for fill/move functions
	// TODO: do bound checks on fill/move functions
	public unsafe struct MemoryBlock {

		internal MemoryBlock(uint _address, uint _length)
		{
			length = _length;
			address = (byte*)_address;
		}

		internal uint	length;
		internal byte*	address;
		
		public uint Length
		{
			get
			{
				return this.length;
			}
		}

		public byte this [int index]
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

		public void Fill(uint pattern)
		{
			ADC.X86.MemoryUtil.MemSet(pattern, (uint)address, length);
		}

		internal void Fill(uint pattern, uint index, uint count)
		{
			ADC.X86.MemoryUtil.MemSet(pattern, (uint)address + index, length);
		}

		internal void Move(uint source, uint dest, uint count)
		{
			ADC.X86.MemoryUtil.MemCopy(source, dest, count);
		}
	}
}
