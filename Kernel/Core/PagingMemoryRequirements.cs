// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using SharpOS;
using SharpOS.AOT;
using SharpOS.Memory;

namespace SharpOS.ADC {
	public unsafe struct PagingMemoryRequirements {
		public PagingMemoryRequirements (uint atomicPages, void *start)
		{
			AtomicPages = atomicPages;
			Start = start;
			Error = PageAllocator.Errors.Success;
		}
		
		public uint AtomicPages;
		public void *Start;
		public PageAllocator.Errors Error;
	}
}
