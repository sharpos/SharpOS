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
	public interface IScheduler	{
		int Count { get; }
		Thread GetNextThread();
		void Dump();
		bool Schedule(Thread thread);
		bool Unschedule(Thread thread);
	}
}
