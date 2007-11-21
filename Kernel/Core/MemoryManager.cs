// 
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//  Ásgeir Halldórsson <asgeir.halldorsson@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT;
using SharpOS.ADC;

namespace SharpOS.Memory {
	public unsafe class MemoryManager {

		#region Global state
		
		static byte *firstHeaderPage = null;
		static byte *nextHeaderPtr = null;
		static byte *currentHeaderPage = null;

		#endregion
		#region Nested types

		public unsafe struct AllocHeader {
			public void *Buffer;
			public uint Size;
			public byte Priority;
			public uint Flags;
		}
		
		#endregion
		#region Setup

		public static unsafe void Setup ()
		{
			firstHeaderPage = currentHeaderPage = (byte*) PageAllocator.Alloc ();
		}

		#endregion
		#region Alloc () family
		
		public static unsafe void *Alloc (int bytes, byte *reason)
		{
			if (currentHeaderPage == null || nextHeaderPtr + sizeof (AllocHeader) >=
					currentHeaderPage + Pager.AtomicPageSize)
				GrowControlData ();

			/* XXX: TODO
			
			AllocHeader *hdr = (AllocHeader *)nextHeaderPtr;
			nextHeaderPtr += sizeof (AllocHeader);

			*/
			
			return null;
		}

		public static unsafe void Dealloc (void *ptr, byte *reason)
		{

		}

		#endregion
		#region Internal

		static unsafe void GrowControlData ()
		{
			if (firstHeaderPage == null) {
				firstHeaderPage = currentHeaderPage = nextHeaderPtr =
					(byte *) PageAllocator.RangeAlloc (32);
				((uint*)firstHeaderPage)[0] = 0;
				nextHeaderPtr += 4;
			} else {
				byte *newpg = (byte*)PageAllocator.Alloc ();

				// link the pages together
				((uint*)currentHeaderPage)[Pager.AtomicPageSize/4-1] = (uint)newpg;
				((uint*)newpg)[0] = (uint)currentHeaderPage;

				nextHeaderPtr = newpg+1;
				currentHeaderPage = newpg;
			}
		}
		
		#endregion
	}
}