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
	public class PCIController : HardwareDevice, IPCIController
	{

		#region Definitions

		private static readonly uint BASE_VALUE = 0x80000000;

		#endregion

		protected SpinLock spinLock;

		protected IReadWriteIOPort ConfigAddress;
		protected IReadWriteIOPort ConfigData;

		public const ushort PCIConfigAddress = 0x0CF8;
		public const ushort PCIConfigData = 0x0CFC;
		private static readonly uint BaseValue = 0x80000000;

		public PCIController ()
		{
			base.name = "PCI_0x" + PCIConfigAddress.ToString ("X");
			base.parent = null; // no parent
			base.deviceStatus = DeviceStatus.Initializing;

			ConfigAddress = base.CreateIOPort (PCIConfigAddress);
			ConfigData = base.CreateIOPort (PCIConfigData);

			this.Initialize ();
		}

		public void Initialize ()
		{
			spinLock.Enter ();

			TextMode.Write (base.name);
			TextMode.Write (": ");

			ConfigAddress.Write32 (BaseValue);

			if (ConfigAddress.Read32 () != BaseValue) {
				base.deviceStatus = DeviceStatus.NotFound;

				TextMode.WriteLine ("PCI controller not found");
				return;
			}

			TextMode.WriteLine ("PCI controller found at 0x" + ((uint)PCIConfigAddress).ToString ("X"));

			Setup ();

			base.deviceStatus = DeviceStatus.Online;
		}

		public uint ReadConfig (uint bus, uint slot, uint function, uint register)
		{
			uint address = BaseValue
					   | ((bus & 0xFF) << 16)
					   | ((slot & 0x0F) << 11)
					   | ((function & 0x07) << 8)
					   | (register & 0xFC);

			ConfigAddress.Write32 (address);
			return ConfigData.Read32 ();
		}

		public void WriteConfig (uint bus, uint slot, uint function, uint register, uint value)
		{
			uint address = BaseValue
					   | ((bus & 0xFF) << 16)
					   | ((slot & 0x0F) << 11)
					   | ((function & 0x07) << 8)
					   | (register & 0xFC);

			ConfigAddress.Write32 (address);
			ConfigData.Write32 (value);
		}

		// check for the presence of a device at the specific PCI address
		public bool ProbeDevice (uint bus, uint slot, uint fun)
		{
			uint data = ReadConfig (bus, slot, fun, 0);
			return (data != 0xFFFFFFFF);
		}

		protected void Setup ()
		{
			uint deviceCount = 0;
			// iterate over bus/device/function to list all devices
			for (uint bus = 0; bus < 256; bus++)
				for (uint slot = 0; slot < 16; slot++)
					for (uint fun = 0; fun < 7; fun++)
						if (ProbeDevice (bus, slot, fun)) {
							DeviceManager.Add (new PCIDevice (this, bus, slot, fun));
							deviceCount++;
						}
		}

	}
}
