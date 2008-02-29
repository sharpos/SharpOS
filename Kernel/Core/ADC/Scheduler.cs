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
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.ADC {
	public static unsafe class Scheduler {
		[AOTAttr.ADCStub]
		public static unsafe void Setup ()
		{
			Diagnostics.Error ("Unimplemented - Setup");
		}

		[AOTAttr.ADCStub]
		public static unsafe void* CreateThread (uint address)
		{
			Diagnostics.Error ("Unimplemented - CreateThread");
			return null;
		}

		private static bool enabled = false;
		public static bool Enabled { get { return enabled; } set { enabled = value; } }

		private static void** ThreadScheduled = (void**) Stubs.StaticAlloc ((uint) (4 * EntryModule.MaxThreads));

		public static void DumpThreads ()
		{
			Barrier.Enter();
			try
			{
				for (int i = 0; i < EntryModule.MaxThreads; i++)
				{
					if (ThreadScheduled[i] == null)
						continue;

					TextMode.Write("Thread");
					TextMode.Write(i);
					TextMode.WriteLine();
				}
				TextMode.WriteLine();
			}
			finally
			{
				Barrier.Exit();
			}
		}

		public static bool ScheduleThread (void* newThread)
		{
			Barrier.Enter();
			try
			{
				for (int i = 0; i < EntryModule.MaxThreads; i++)
				{
					if (ThreadScheduled[i] != null)
						continue;

					ThreadScheduled[i] = newThread;
					return true;
				}
			}
			finally
			{
				Barrier.Exit();
			}
			return false;
		}

		private static int position = -1;
		public static void* GetNextThread (void* currentThread)
		{
			// do scheduling here...

			if (enabled && ThreadScheduled [0] != null) {
				// for now, just return the current thread ...
				if (position != -1) {
					UpdateThread(ThreadScheduled [position], currentThread);
				}

				position++;
				if (position >= EntryModule.MaxThreads ||
					ThreadScheduled [position] == null)
					position = 0;
				
				currentThread = ThreadScheduled [position];
			}

			return currentThread;
		}
		
		[AOTAttr.ADCStub]
		private static unsafe void UpdateThread(void* thread, void* currentThread)
		{
			Diagnostics.Error ("Unimplemented - CreateThread");
		}
	}
}
