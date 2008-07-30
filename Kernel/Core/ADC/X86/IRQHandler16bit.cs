// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//  Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.Kernel.HAL;

namespace SharpOS.Kernel.ADC.X86
{

	public class IRQHandler16bit : IRQHandler
	{

		internal struct IRQCallBack
		{
			public IIRQCallBack callback;
		}

		internal static SpinLock spinLock;
		internal static bool[] irqs;
		internal static IRQCallBack[] callBacks;	//TODO: create list per IRQ (for shared IRQs)

		#region Constructor
		internal IRQHandler16bit (byte irq)
		{
			spinLock.Enter ();

			if (irqs == null) {
				irqs = new bool[16];
				callBacks = new IRQCallBack[16];
			}

			this.irq = irq;

			IDT.Interrupt interrupt = (IDT.Interrupt)(irq + 0x20);

			IDT.RegisterIRQ (interrupt, Stubs.GetFunctionPointer (INTERRUPT16BIT_HANDLER));

			spinLock.Exit ();
		}

		#endregion

		protected byte irq;

		const string INTERRUPT16BIT_HANDLER = "INTERRUPT16BIT_HANDLER";

		[SharpOS.AOT.Attributes.Label (INTERRUPT16BIT_HANDLER)]
		static unsafe void IRQHandler (IDT.ISRData data)
		{
			uint index = (uint)data.Stack->IrqIndex - 0x20;

			if ((index < 0) || (index > 0x0F))
				return;

			irqs[index] = true;

			if (callBacks[index].callback != null) {
				// call the callback methods
				// note this is a temporary solution until threads are implemented

				bool mine = callBacks[index].callback.OnInterrupt (index);

				//if (mine)
				//	irqs[index] = false;
			}
		}

		#region Methods
		public void ClearInterrupt ()
		{
			irqs[irq] = false;
		}

		public bool WaitForInterrupt (uint timeout)
		{
			while (!irqs[irq])
				;

			return true;
		}

		public void AssignCallBack (IIRQCallBack callback)
		{
			callBacks[irq].callback = callback;
		}

		#endregion
	}
}
