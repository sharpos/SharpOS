/*
 * SharpGC -- A conservative, generational, compacting garbage collector written in C#
 *
 * (C) 2007 William Lahti. This software is licensed under the terms of the GNU Lesser
 * General Public License.
 *
 * ManagedHeap.cs -- A managed heap implementation. Controlled by the GC.
 *
 */
 
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace SharpGC {
	/**
		<summary>
			This structure represents the header information affixed to each
			allocated object on the managed heap. The Size field specifies how
			many bytes beyond the end of the header the allocated memory for the
			object stretches. The Handle field specifies the GC handle for the 
			given object. If Handle is null, the header specifies a gap. A gap is
			an area in the heap which was once allocated for an object but has since
			been freed.
		</summary>
	*/
	public unsafe struct ManagedEntryHeader {
		public byte Magic;
		public int Size;
		public GCHandle *Handle;
	}
	
	public unsafe class ManagedHeap {
		private ManagedHeap()
		{ }
		
		// Private State
		
		static int HeapSize;			// the total size of the heap
		static int Allocated;			// the amount of memory allocated in the heap
		static int ObjectCount;			// the amount of objects currently allocated
		static int ObjectSpaceAllocated;	// the memory allocated on the heap for object storage
							// (that is, not including 
		static int GapCount;
		static int GapSize;
		static byte *Heap;
		static int EmergencyExpandStep;
		static int EmergencyCompactBuffer;
		
		static byte *NextObjPtr;
		
		public static void Initialize(int heapSize)
		{
			Initialize(heapSize, 1024 * 32, 256); 
		}
		
		public static void Initialize(int heapSize, int expandStep, int emCompactBuffer)
		{
			HeapSize = heapSize;
			Heap = (byte*)GCMem.Alloc(heapSize, "ManagedHeap");
			Allocated = 0;
			GapCount = 0;
			GapSize = 0;
			NextObjPtr = Heap;
			
			// The managed heap will perform an emergency expansion
			// when an allocation is requested that cannot be held
			// by the currently allocated heap.
			EmergencyExpandStep = expandStep;
			
			// The managed heap will perform emergency compacting 
			// when there is less bytes than this left unallocated.
			EmergencyCompactBuffer = emCompactBuffer;
		}
			
		public static ManagedEntryHeader *GetManagedHeader(void *ptr)
		{
			byte *bptr = (byte*)ptr;
			string straddr = "0x" + ((int)ptr).ToString("x");
			
			if (bptr < Heap || bptr > Heap + HeapSize)
				throw new ArgumentException("Pointer [" + straddr + "] lies outside the managed heap");
			if (bptr >= NextObjPtr)
				throw new ArgumentException("Pointer [" + straddr + 
							"] points to unallocated memory on the heap");
				
			ManagedEntryHeader *hdr = (ManagedEntryHeader*)(bptr - sizeof(ManagedEntryHeader));
			
			if ((byte*)hdr < Heap) 
				throw new ArgumentException("Managed header for pointer [" + straddr + 
							"] lies outside of the managed heap");
			if (hdr->Magic != 233)
				throw new ArgumentException("Tried to get managed header for non-heap pointer [" +
							straddr + "]", "ptr");
				
			return hdr;
		}
		
		public static int GetAllocatedMemory()
		{
			return ObjectSpaceAllocated;
		}
		
		public static int GetObjectsAllocated()
		{
			return ObjectCount;
		}
		
		public static int GetGapUsage()
		{
			return GapSize;
		}
		
		public static int GetGapCount()
		{
			return GapCount;
		}
		
		public static double GetFragmentation()
		{
			return GapSize / (double)HeapSize;
		}
		
		public static int GetTotalUsage()
		{
			return Allocated + GapSize;
		}
		
		public static void SetEmergencyExpandStep(int size)
		{
			EmergencyExpandStep = size;
		}
		
		public static void PrintStats()
		{
			Console.WriteLine("Managed Heap");
			Console.WriteLine("- Size: {0}", HeapSize);
			Console.WriteLine("- Address: {0}", ((int)Heap).ToString("x"));
			Console.WriteLine("- NextObjPtr: {0}", ((int)NextObjPtr).ToString("x"));
			Console.WriteLine("Heap Allocation Status");
			Console.WriteLine("- Allocated: {0}", Allocated);
			Console.WriteLine("- ObjectSpaceAllocated: {0}", ObjectSpaceAllocated);
			Console.WriteLine("- ObjectCount: {0}", ObjectCount);
			Console.WriteLine("- Gaps: {0}", GapCount);
			Console.WriteLine("- Total size of gaps: {0}", GapSize);
			Console.WriteLine("- Fragmentation: {0}%", GetFragmentation() * 100);
		}
		
		public static void *Allocate(int objSize, GCHandle *hndl)
		{
			// Emergency Compact: if running low on space, try compacting 
				
			if (HeapSize - (Allocated + GapSize) <= EmergencyCompactBuffer) {
				ManagedHeap.Compact();	
			}
			
			// Emergency Expand: if we don't have enough space to allocate our object,
			// expand the heap.
			
			if (Allocated + GapSize + objSize >= HeapSize) {
				int expandSize = EmergencyExpandStep;
				
				while (HeapSize - (Allocated + GapSize) + expandSize < objSize)
					expandSize += EmergencyExpandStep;
				
				ManagedHeap.Expand(expandSize);
			}
			
			// Some checks
			
			if (NextObjPtr < Heap || NextObjPtr > (Heap + HeapSize))
				throw new Exception("Error: the NextObjPtr pointer doesn't fall inside the managed heap");
				
			if (NextObjPtr >= (Heap + HeapSize)) 
				throw new Exception("Error: the NextObjPtr is beyond the end of the managed heap");
			
			// Allocate our memory from NextObjPtr
			
			ManagedEntryHeader *meh = (ManagedEntryHeader*)NextObjPtr;
			void *mem = ((byte*)meh) + sizeof(ManagedEntryHeader);
			NextObjPtr += sizeof(ManagedEntryHeader) + objSize;
			
			// Our magic number ensures we don't get header locations misaligned
			
			meh->Magic = 233;
			meh->Size = objSize;
			meh->Handle = hndl;
			
			// Update some tracking buffers
			
			Allocated += objSize + sizeof(ManagedEntryHeader);
			ObjectSpaceAllocated += objSize;
			ObjectCount++;
			
			return mem;
		}
		
		public static void Deallocate(void *mem)
		{
			ManagedEntryHeader *meh = null;
			
			if (mem == null)
				throw new ArgumentNullException("mem");
				
			meh = GetManagedHeader(mem);
			
			if (meh->Magic != 233) 
				throw new ArgumentException("The pointer is outside of the managed heap", "mem");
			
			// Invalidate the entry by nulling the Handle pointer in
			// the managed header
			
			meh->Handle = null;
			
			// Update our tracking buffers
			
			Allocated -= meh->Size + sizeof(ManagedEntryHeader);
			ObjectSpaceAllocated -= meh->Size;
			ObjectCount--;
			GapCount++;
			GapSize += meh->Size + sizeof(ManagedEntryHeader);
			
			// If all memory has been deallocated, save time compacting
			// by moving NextObjPtr manually.
			
			if (Allocated == 0) {
				NextObjPtr = Heap;
				GapSize = 0;
				GapCount = 0;
			}
		}
		
		public static void Compact()
		{
			byte *heap_ptr = Heap;
			int heap_offset = 0, gap = 0;
			
			if (GapSize == 0)
				return;
			
			// Walk the heap linearly, finding gaps and shifting 
			// allocated memory back to fill them. After shifting
			// each allocation, update GCData pointers for the runtime.
			
			while (heap_offset < (Allocated+GapSize)) {
				ManagedEntryHeader *meh = (ManagedEntryHeader*)heap_ptr;
				
				if (meh->Magic != 233)
					throw new Exception("Compaction process became misaligned, bad magic byte in header");
				if (meh->Handle == null) {
					// Append gap size to total
					gap += sizeof(ManagedEntryHeader) + meh->Size;
				} else {
					GCHandle *hndl = meh->Handle;
					
					// Ensure that this object is not memory locked
					if ((hndl->Data->Flags & GCFlags.MemoryLock) != 0) {
						gap = 0;
					} else {
						// Slide allocation back to fill gap
						for (int x = 0; x < (sizeof(ManagedEntryHeader) + meh->Size); ++x) {
							byte *new_loc, old_loc;
							
							new_loc = heap_ptr + x - gap;
							old_loc = heap_ptr + x;
							
							*new_loc = *old_loc;
						}
						
						// Fixup data pointer
						hndl->Data->Memory = (byte*)hndl->Data->Memory - gap;
					}
				}
				
				// Move heap_ptr along the heap as normal
				heap_ptr += sizeof(ManagedEntryHeader) + meh->Size;
				heap_offset += sizeof(ManagedEntryHeader) + meh->Size;
			}
			
			Console.WriteLine("** HEAP: compaction saved {0} bytes (gap was {2} bytes). NextObjPtr = [0x{1}]", 
						gap, ((int)NextObjPtr).ToString("x"), GapSize);
			
			// Adjust NextObjPtr and reset GapCount/GapSize
			
			NextObjPtr -= gap;
			GapSize = 0;
			GapCount = 0;
		}
		
		public static void Expand(int moreBytes)
		{
			byte *new_heap = (byte*)GCMem.Alloc(HeapSize + moreBytes, "ManagedHeap"),
				nh_ptr = new_heap,
				oh_ptr = Heap;
			
			int diff = (int) (NextObjPtr - Heap);
			
			// Copy old heap to new heap.
			for (int x = 0; x < HeapSize; ++x) {
				*nh_ptr = *oh_ptr;
				++nh_ptr;
				++oh_ptr;
			}
			
			// Deallocate the old memory
			
			GCMem.Dealloc(Heap, "ManagedHeap"); Heap = null;
			
			// Assign the new heap
			
			Heap = new_heap;
			HeapSize += moreBytes;
			NextObjPtr = Heap + diff;
			
			// Checks
			
			if (NextObjPtr < Heap || NextObjPtr >= (Heap + HeapSize))
				throw new Exception("Failed to reposition nextobjptr");
				
			Console.WriteLine("** HEAP :: expanded by {0} bytes", moreBytes);
		}
		
		public static void Contract(int lessBytes)
		{
			if (Allocated + GapSize >= HeapSize - lessBytes) {
				if (GapSize > 0)
					throw new Exception("Cannot contract heap: allocated space too large " +
								"(gaps exist; did you do ManagedHeap.Compact() first?)");
				else
					throw new Exception("Cannot contract heap: allocated space too large");
			}
			
			byte *new_heap = (byte*)GCMem.Alloc(HeapSize - lessBytes, "ManagedHeap"),
				nh_ptr = new_heap,
				oh_ptr = Heap;
			
			for (int x = 0; x < (HeapSize - lessBytes); ++x) {
				*nh_ptr = *oh_ptr;
				++nh_ptr;
				++oh_ptr;
			}
			
			GCMem.Dealloc(Heap, "ManagedHeap");
			
			Heap = new_heap;
			HeapSize -= lessBytes;
		}
	}
}