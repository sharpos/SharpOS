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
using System.Collections;

namespace SharpOS.Kernel.ADC {
	public sealed class RoundRobinScheduler : IScheduler
	{
		public int Count 
		{
			get
			{
				return schedule.Count;
			}
		}
		
		private int			position		= -1;
		private ArrayList	schedule		= new ArrayList();

		public Thread GetNextThread()
		{
			// TODO: make this thread safe!
			int count = schedule.Count;
			if (count == 0)
				return null;

			position++;
			position %= count;
			
			return (Thread) schedule [position];
		}

		public unsafe void Dump()
		{
			// TODO: make this thread safe!
			TextMode.WriteLine("--------------");
			TextMode.Write("Count: ");
			TextMode.Write(Count);
			TextMode.WriteLine();
			for(int i=0;i<Count;i++)
			{
				Thread thread = schedule[i] as Thread;
				thread.Dump();
			}
			TextMode.WriteLine("--------------");
		}

		public bool Schedule(Thread thread)
		{
			// TODO: make this thread safe!
			if (schedule.Contains(thread))
				return false;

			schedule.Add(thread);
			return true;
		}
		
		public bool Unschedule(Thread thread)
		{
			// TODO: make this thread safe!
			if (!schedule.Contains(thread))
				return false;

			// TODO: check if thread is currently being run!
			// schedule.Remove(thread); // not implemented yet
			return true;
		}
	}
}
