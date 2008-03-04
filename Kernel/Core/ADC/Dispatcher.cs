//
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using AOTAttr = SharpOS.AOT.Attributes;
using SharpOS.Kernel.Foundation;

namespace SharpOS.Kernel.ADC {
	public static class Dispatcher {
		private static IScheduler scheduler = null;
		private static bool enabled = false;
		private static Thread currentThread = null;

		public static void Setup(IScheduler _scheduler)
		{
			scheduler = _scheduler;
		}

		public static void Start()
		{
			currentThread = null;
			enabled = (scheduler != null && scheduler.Count != 0);
		}
				
		public static unsafe Thread Dispatch(void* currentStack) 
		{
			if (!enabled)
				return null;

			if (currentThread != null)
				currentThread.StackPointer = currentStack;
						
			// run scheduler here..
			currentThread = scheduler.GetNextThread();
			
			return currentThread;
		}

		public static void Yield()
		{
			Barrier.Enter();
			try
			{
				//Schedule(void* currentStack)  // .. somehow
			}
			finally
			{
				Barrier.Exit();
			}
		}

		public static bool Schedule(Thread thread)
		{
			if (scheduler == null)
				return false;

			Barrier.Enter();
			try
			{
				return scheduler.Schedule(thread);
			}
			finally
			{
				Barrier.Exit();
			}
		}
		
		public static bool Unschedule(Thread thread)
		{
			if (scheduler == null)
				return false;

			Barrier.Enter();
			try
			{
				return scheduler.Unschedule(thread);
			}
			finally
			{
				Barrier.Exit();
			}
		}
	}
}
