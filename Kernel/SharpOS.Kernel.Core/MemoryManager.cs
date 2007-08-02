/*
 * SharpOS.Memory/MemoryManager.cs
 * N:SharpOS.Memory
 *
 * (C) 2007 William Lahti. This software is licensed under the terms of the GNU General
 * Public License.
 *
 * TODO: set segment base properly to allow kernel to appear in higher half
 *
 */

using SharpOS.AOT;
using SharpOS.ADC;

namespace SharpOS.Memory {
	public unsafe class MemoryManager {
			private static byte *FirstHeaderPage = null;
			private static byte *NextHeaderPtr = null;
			private static byte *CurrentHeaderPage = null;
			
			public static unsafe void Setup ()
			{
				FirstHeaderPage = CurrentHeaderPage = (byte*) PageAllocator.Alloc();
			}
			
			public unsafe struct AllocHeader {
				public void *Buffer;
				public uint Size;
				public byte Priority;
				public uint Flags;
			}
			
			public static unsafe void *Alloc (int bytes, byte *reason)
			{
				if (CurrentHeaderPage == null || NextHeaderPtr + sizeof(AllocHeader) >= 
						CurrentHeaderPage + Pager.AtomicPageSize)
					GrowControlData();
				
				AllocHeader *hdr = (AllocHeader *)NextHeaderPtr;
				NextHeaderPtr += sizeof(AllocHeader);
				
				return null; // TODO
			}
			
			public static unsafe void Dealloc(void *ptr, byte *reason)
			{
				
			}
			
			private static unsafe void GrowControlData()
			{
				if (FirstHeaderPage == null) {
					FirstHeaderPage = CurrentHeaderPage = NextHeaderPtr = 
						(byte *) PageAllocator.RangeAlloc(32);
					((uint*)FirstHeaderPage)[0] = 0;
					NextHeaderPtr += 4;
				} else {
					byte *newpg = (byte*)PageAllocator.Alloc();
					
					// link the pages together
					((uint*)CurrentHeaderPage)[Pager.AtomicPageSize/4-1] = (uint)newpg;
					((uint*)newpg)[0] = (uint)CurrentHeaderPage;
					
					NextHeaderPtr = newpg+1;
					CurrentHeaderPage = newpg;
				}
			}
	}
}