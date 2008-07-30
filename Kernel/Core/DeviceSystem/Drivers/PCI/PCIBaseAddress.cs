// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//  Phil Garcia (aka tgiphil) <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries

using System;
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using SharpOS.Kernel.ADC;	// for spin lock
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.DeviceSystem;
using SharpOS.Kernel.HAL;

namespace SharpOS.Kernel.DeviceSystem.PCI
{
	public enum AddressRegion : byte
	{
		IO,
		Memory,
	}
	public class PCIBaseAddress
	{
		protected uint address;
		protected uint size;
		protected AddressRegion region;
		protected bool prefetchable;

		public PCIBaseAddress (uint address, uint size, AddressRegion region, bool prefetchable)
		{
			this.address = address;
			this.size = size;
			this.region = region;
			this.prefetchable = prefetchable;
		}

		public uint Address
		{
			get
			{
				return address;
			}
		}

		public uint Size
		{
			get
			{
				return size;
			}
		}

		public AddressRegion Region
		{
			get
			{
				return region;
			}
		}

		public bool Prefetchable
		{
			get
			{
				return prefetchable;
			}
		}

		public override string ToString ()
		{
			if (region == AddressRegion.IO)
				return "I/O Port at 0x" + address.ToString ("X") + " [size=" + size.ToString () + "]";
			
			if (prefetchable)
				return "Memory at 0x" + address.ToString ("X") + " [size=" + size.ToString () + "] (prefetchable)";

			return "Memory at 0x" + address.ToString ("X") + " [size=" + size.ToString () + "] (non-prefetchable)";
		}

	}
}