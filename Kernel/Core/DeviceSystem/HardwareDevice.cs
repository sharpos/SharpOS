using System;
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.ADC.X86;
using SharpOS.Kernel.HAL;

namespace SharpOS.Kernel.DeviceSystem
{
	public abstract class HardwareDevice : Device
	{
		protected HardwareResources hardware;

		protected HardwareDevice ()
		{
			this.hardware = new HardwareResources ();
		}

		protected IOPort CreateIOPort (uint port)
		{
			IOPort IOPort = new IOPort (port);
			hardware.Add (IOPort);
			return IOPort;
		}

		protected IOPort CreateIOPort (uint port, uint offset)
		{
			return CreateIOPort (port + offset);
		}

		protected IDMAChannel CreateDMAChannel (byte channel)
		{
			IDMAChannel dmaChannel = new DMAChannel (channel);
			hardware.Add (dmaChannel);
			return dmaChannel;
		}

		protected IRQHandler CreateIRQHandler (byte irq)
		{
			IRQHandler irqHandler = new IRQHandler16bit (irq);
			hardware.Add (irqHandler);
			return irqHandler;
		}
	}
}
