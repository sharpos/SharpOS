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
using SharpOS.Kernel.FileSystem;

namespace SharpOS.Kernel.DeviceSystem.PCI
{
	public class PCIDevice : HardwareDevice
	{
		private uint bus;
		private uint slot;
		private uint function;
		private ushort vendorID;
		private ushort deviceID;
		private byte revisionID;
		private ushort classCode;
		private byte progIF;
		private byte baseClass;
		private byte subClass;
		private byte irq;

		PCIBaseAddress[] addresses;

		protected IPCIController pciController;

		/// <summary>
		/// Create a new PCIDevice instance at the selected PCI address
		/// </summary>
		public PCIDevice (IPCIController pciController, uint bus, uint slot, uint fun)
		{
			base.parent = pciController as Device;
			base.name = base.parent.Name + "/" + bus.ToString () + "/" + slot.ToString () + "/" + fun.ToString ();
			base.deviceStatus = DeviceStatus.Offline;

			this.pciController = pciController;
			this.bus = bus;
			this.slot = slot;
			this.function = fun;

			this.addresses = new PCIBaseAddress[6];

			uint data = pciController.ReadConfig (bus, slot, fun, 0);
			this.vendorID = (ushort)(data & 0xFFFF);
			this.deviceID = (ushort)((data >> 16) & 0xFFFF);

			data = pciController.ReadConfig (bus, slot, fun, 0x08);
			this.revisionID = (byte)(data & 0xFF);
			this.progIF = (byte)((data >> 8) & 0xFF);
			this.classCode = (ushort)((data >> 16) & 0xFFFF);
			this.subClass = (byte)((data >> 16) & 0xFF);
			this.baseClass = (byte)((data >> 24) & 0xFF);

			data = pciController.ReadConfig (bus, slot, fun, 0x3c);
			
			if ((data & 0xFF00) != 0)
				this.irq = (byte)(data & 0xFF);	

			for (uint i = 0; i < 6; i++) {
				uint cur = pciController.ReadConfig (bus, slot, fun, 16 + (i * 4));
				///TODO: disable interrupts
				pciController.WriteConfig (bus, slot, fun, 16 + (i * 4), 0xFFFFFFFF);
				uint mask = pciController.ReadConfig (bus, slot, fun, 16 + (i * 4));
				pciController.WriteConfig (bus, slot, fun, 16 + (i * 4), cur);
				///TODO: enable interrupts
				if (cur % 2 == 1)
					addresses[i] = new PCIBaseAddress (cur & 0x0000FFF8, (~(mask & 0xFFF8) + 1) & 0xFFFF, AddressRegion.IO, false);
				else
					addresses[i] = new PCIBaseAddress (cur & 0xFFFFFFF0, ~(mask & 0xFFFFFFF0) + 1, AddressRegion.Memory, ((cur & 0x08) == 1));
			}
		}

		public PCIBaseAddress[] BaseAddresses
		{
			get
			{
				return addresses;
			}
		}

		public uint Bus
		{
			get
			{
				return bus;
			}
		}

		public uint Slot
		{
			get
			{
				return slot;
			}
		}

		public uint Function
		{
			get
			{
				return function;
			}
		}
		public ushort VendorID
		{
			get
			{
				return vendorID;
			}
		}

		public ushort DeviceID
		{
			get
			{
				return deviceID;
			}
		}

		public byte RevisionID
		{
			get
			{
				return revisionID;
			}
		}

		public ushort ClassCode
		{
			get
			{
				return classCode;
			}
		}

		public byte ProgIF
		{
			get
			{
				return progIF;
			}
		}

		public byte SubClass
		{
			get
			{
				return subClass;
			}
		}

		public byte IRQ
		{
			get
			{
				return irq;
			}
		}
	}
}