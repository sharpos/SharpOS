//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.ADC
{
	public static unsafe class Scheduler
	{
		[AOTAttr.ADCStub]
		public static unsafe void Setup()
		{
			Kernel.Error("Unimplemented - Setup");
		}

		private static int				position				= -1;
		private static void**			ThreadScheduled			= (void**)Kernel.StaticAlloc((uint)(4 * Kernel.MaxThreads));

		public static void DumpThreads()
		{
			Architecture.DisableInterrupts();
			for (int i = 0; i < Kernel.MaxThreads; i++)
			{
				if (ThreadScheduled[i] == null)
					continue;

				TextMode.Write("Thread");
				TextMode.Write(i);
				TextMode.WriteLine();
			}
			TextMode.WriteLine();
			Architecture.EnableInterrupts();
		}

		public static bool ScheduleThread(void* newThread)
		{
			Architecture.DisableInterrupts();
			for (int i = 0; i < Kernel.MaxThreads; i++)
			{
				if (ThreadScheduled[i] != null)
					continue;
				
				ThreadScheduled[i] = newThread;

				Architecture.EnableInterrupts();
				return true;
			}
			Architecture.EnableInterrupts();
			return false;
		}

		public static void* GetNextThread(void* currentThread)
		{
			// do scheduling here...

			if (ThreadScheduled[0] != null)
			{
				// for now, just return the current thread ...
				if (position != -1)
				{
					ThreadScheduled[position] = currentThread;
				}

				position++;
				if (position >= Kernel.MaxThreads || 
					ThreadScheduled[position] == null)
					position = 0;

				TextMode.Write(position);

				currentThread = ThreadScheduled[position];
			}

			return currentThread;
		}

		[AOTAttr.ADCStub]
		public static unsafe void* CreateThread(uint address)
		{
			Kernel.Error("Unimplemented - CreateThread");
			return null;
		}
	}
}
