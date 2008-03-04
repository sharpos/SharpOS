//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT;
using AOTAttr = SharpOS.AOT.Attributes;
using SharpOS.Kernel.Foundation;
using System;

namespace SharpOS.Kernel.ADC {
	public unsafe sealed class Thread {
        [AOTAttr.ADCStub]
		public static void		Yield() 
		{
		}
		
        [AOTAttr.ADCStub]
		public static void		SpinWait(int iterations) 
		{
		}
		
        [AOTAttr.ADCStub]
		public static Int32		VolatileRead(ref Int32 address) 
		{
			return address;
		}
		
        [AOTAttr.ADCStub]
		public static void		BeginCriticalRegion()
		{
		}
		
        [AOTAttr.ADCStub]
		public static void		EndCriticalRegion()
		{
		}

		private uint stackAddress;
		internal uint StackAddress { get { return stackAddress; } set { stackAddress = value; } }
		
		private void* stackPointer;
		internal void* StackPointer { get { return stackPointer; } set { stackPointer = value; } }
		
		private uint functionAddress;
		internal uint FunctionAddress { get { return functionAddress; } set { functionAddress = value; } }

		internal void Dump()
		{
			TextMode.Write("Thread stack: ");
			TextMode.Write((int)stackAddress, true);
			TextMode.Write(" function: ");
			TextMode.Write((int)functionAddress, true);
			TextMode.Write(" position: ");
			TextMode.Write((int)stackPointer, true);
			TextMode.WriteLine();
		}
	}
}
