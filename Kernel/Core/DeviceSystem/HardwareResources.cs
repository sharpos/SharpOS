using System;
using SharpOS.Kernel.HAL;

namespace SharpOS.Kernel.DeviceSystem
{
	public class HardwareResources
	{
		protected static uint MaxDMAChannels = 4;
		protected static uint MaxIOPorts = 100;
		protected static uint MaxIRQHandlers = 20;

		protected IDMAChannel[] dmaChannels;
		protected IOPort[] ioPorts;
		protected IRQHandler[] irqHandlers;

		protected uint dmaChannelCount;
		protected uint IOPortCount;
		protected uint irqHandlerCount;

		public HardwareResources ()
		{
			dmaChannels = new IDMAChannel[MaxDMAChannels];
			ioPorts = new IOPort[MaxIOPorts];
			irqHandlers = new IRQHandler[MaxIRQHandlers];

			dmaChannelCount = 0;
			IOPortCount = 0;
			irqHandlerCount = 0;
		}

		public void Add (IDMAChannel dmaChannel)
		{
			if (dmaChannelCount >= MaxDMAChannels)
				return;

			dmaChannels[dmaChannelCount++] = dmaChannel;
		}

		public void Add (IOPort ioPort)
		{
			if (IOPortCount >= MaxIOPorts)
				return;

			ioPorts[IOPortCount++] = ioPort;
		}

		public void Add (IRQHandler irqHandler)
		{
			if (irqHandlerCount >= MaxIRQHandlers)
				return;

			irqHandlers[irqHandlerCount++] = irqHandler;
		}

		public IDMAChannel[] DMAChannels
		{
			get
			{
				IDMAChannel[] list = new IDMAChannel[dmaChannelCount];

				for (int i = 0; i < dmaChannelCount; i++)
					list[i] = dmaChannels[i];

				return list;
			}
		}

		public IOPort[] IOPorts
		{
			get
			{
				IOPort[] list = new IOPort[IOPortCount];

				for (int i = 0; i < IOPortCount; i++)
					list[i] = ioPorts[i];

				return list;
			}
		}

		public IRQHandler[] IRQHandlers
		{
			get
			{
				IRQHandler[] list = new IRQHandler[irqHandlerCount];

				for (int i = 0; i < irqHandlerCount; i++)
					list[i] = irqHandlers[i];

				return list;
			}
		}
	}
}
