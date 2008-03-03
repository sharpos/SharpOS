//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Phil Garcia <phil@thinkedge.com>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using SharpOS.AOT;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.ADC
{
	public struct SpinLock
	{
		private enum LockState : uint
		{
			Free	= 0,
			Owned	= 1
		}
		private int	lockState;

		public void Enter() 
		{
			Thread.BeginCriticalRegion();
			while (true) 
			{
				// If resource available, set it to in-use and return
				if (Interlocked.Exchange(ref lockState, (int)LockState.Owned) == (int)LockState.Free) 
					return;

				while (Thread.VolatileRead(ref lockState) == (int)LockState.Owned) 
					Thread.Yield();
			}
		}

		public void Exit() 
		{ 
			Interlocked.Exchange(ref lockState, (int)LockState.Free);
			Thread.EndCriticalRegion();
		}

	}
}
