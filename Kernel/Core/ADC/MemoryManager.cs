//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	�sgeir Halld�rsson <asgeir.halldorsson@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
//	Ziliang Guo <drakekaizer666@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace SharpOS.Kernel.ADC {
	// temp. memory manager, this will be used to test paging
	public static unsafe class MemoryManager {
		private static uint memoryEnd = 0;
		private static uint memoryStart = 0;
		private static uint startAddress = 0x40000000; // 1gb Mark
		private static ulong allocated = 0;

		[StructLayout (LayoutKind.Sequential)]
		private struct Header {
			public Header* Previous;
			public Header* Next;
			public uint Size;
			public uint Free;
		}

		private static Header* firstNode = null;
		private static Header* lastFreeNode = null;

		public static unsafe void Setup ()
		{
			memoryStart = (uint) Pager.PageAlign ((void*) startAddress, 0);
			memoryEnd = UInt32.MaxValue;

			firstNode = (Header*) memoryStart;
			firstNode->Previous = null;
			firstNode->Next = firstNode;
			firstNode->Size = memoryEnd - memoryStart - (uint) sizeof (Header);
			firstNode->Free = 1;

			lastFreeNode = firstNode;
		}

		public static unsafe void* Allocate (uint allocate_size)
		{
			Kernel.Diagnostics.Assert (firstNode != null, "MemoryManager.Allocate(uint): Unable to allocate because the MemoryManager has not been initialized. ");
			if (firstNode == null)
				return null;

			Header* currentNode = lastFreeNode;
			
			// Allign the block to 4? or 8?
			allocate_size += (8 - (allocate_size & 7));

			// FIXME: Use a free list as this is VERY slow but works, asgeirh 2007-11-16
			while (currentNode != null) {
				if (currentNode->Free == 1 && 
					currentNode->Size >= allocate_size) {
					break;
				}

				currentNode = currentNode->Next;
				if (currentNode == lastFreeNode) {
					currentNode = null;
					break;
				}
			}

			if (currentNode == null)
			{
				DumpSerial();
				Diagnostics.Assert (false, "MemoryManager.Allocate(uint): Unable to allocate memory; no sufficiently sized nodes available.");
				// ... how lucky do you feel?
				throw new System.OutOfMemoryException("MemoryManager.Allocate(uint): Unable to allocate memory; no sufficiently sized nodes available.");
				//return null;
			}

			uint memoryLeft = currentNode->Size - allocate_size;

			currentNode->Free = 0;

			if (memoryLeft > (uint) sizeof (Header)) {
				//If there is enough room to squeeze in a new node after this one, then do it

				currentNode->Size = allocate_size;
				void *nextPtr = (byte*)currentNode + currentNode->Size + (uint)sizeof (Header);

				Header* nextNode = (Header*) nextPtr;
				nextNode->Free = 1;
				nextNode->Size = memoryLeft - (uint) sizeof (Header);
				nextNode->Previous = currentNode;
				nextNode->Next = null;

				// Do not link firstNode's Previous link to the end of the list

				if (currentNode->Next != firstNode)
					currentNode->Next->Previous = nextNode;

				nextNode->Next = currentNode->Next;
				currentNode->Next = nextNode;
			} else
				allocate_size = currentNode->Size;

			// Check to see if the next node is free, larger in size than lastFreeNode, and is also
			// lower in range than lastFreeNode.  If all these are true, set lastFreeNode to the next
			// node.

			if (currentNode->Next->Free == 1 && 
				currentNode->Next->Size > lastFreeNode->Size &&
			    currentNode->Next < lastFreeNode)
				lastFreeNode = currentNode->Next;

			uint retPtr = (uint) currentNode + (uint) sizeof (Header);
			allocated += (ulong) currentNode->Size;

			Diagnostics.Assert (retPtr != 0, "MemoryManager.Allocate(uint): Allocation failed. ");
			if (retPtr == 0)
			{
				// ... how lucky do you feel?
				throw new System.OutOfMemoryException("MemoryManager.Allocate(uint): Could not allocate memory. ");
				//return null;
			}

			SharpOS.Kernel.ADC.MemoryUtil.MemSet(0, retPtr, allocate_size);

			
			if (currentNode->Next == null ||
				currentNode->Next->Previous != currentNode ||
				(currentNode->Previous != null &&
				currentNode->Previous->Next != currentNode))
			{
				Diagnostics.Assert(false, "Allocation corrupted linked list. ");
			}

			return (void*) retPtr;
		}

        public static unsafe void* Reallocate(void* stptr, uint allocate_size)
		{
			uint memoryHeaderPointer = (uint)stptr - (uint)sizeof(Header);
			Header* oldBlock = (Header*)memoryHeaderPointer;
			// neededSize is the amount of additional space needed to enlarge the original block
			uint neededSize = allocate_size - oldBlock->Size;

			Header* retPtr = null;
			void* nextPtr = null;

			/*
			 * If the node after our current block is free and is large enough
			 * to accommodate the new allocated size, we will simply extend the
			 * current node into its space.  In this case, we return the same
			 * pointer that was given to use, since the location of the block
			 * does not change.
			 */
			if (oldBlock->Next->Free == 1 && oldBlock->Next->Previous != null && oldBlock->Next->Size >= neededSize) {
				Header* nextBlock = oldBlock->Next;
				oldBlock->Size = allocate_size;
				oldBlock->Next = nextBlock->Next;
				uint memoryLeft = oldBlock->Next->Size - allocate_size;

				/*
				 * Squeeze in another node if there's space.
				 */ 
				if (memoryLeft > (uint)sizeof(Header)) {
					nextPtr = (byte*)oldBlock + oldBlock->Size + (uint)sizeof(Header);
					Header* nextNode = (Header*)nextPtr;
					nextNode->Size = memoryLeft - (uint)sizeof(Header);
					nextNode->Next = oldBlock->Next;
					nextNode->Free = 1;
					if (nextNode->Next->Previous != null)
						nextNode->Next->Previous = nextNode;
					nextNode->Previous = oldBlock;
					oldBlock->Next = nextNode;
				}
				else if (oldBlock->Next->Previous != null)
					oldBlock->Next->Previous = oldBlock;

				return stptr;
			}

			/*
			 * If the block before our current position is free, then some
			 * specialized handling needs to be done.  We need to check
			 * whether we need to extend into the current block, which requires
			 * care in not overwriting the Header information too early and
			 * creatinng and possibly consolidating a new free node.  Or there
			 * is sufficient space in the previous node to move everything
			 * over, in which case we need to check if any space left over
			 * needs to be consolidated with the newly freed current node.
			 */
			if (oldBlock->Previous != null && oldBlock->Previous->Free == 1) {
				if (oldBlock->Previous->Size >= allocate_size) {
					Header* prevBlock = oldBlock->Previous;
					nextPtr = (byte*)prevBlock + (uint)sizeof(Header);
					prevBlock->Free = 0;
					SharpOS.Kernel.ADC.MemoryUtil.MemCopy((uint)stptr, (uint)nextPtr, oldBlock->Size);
					uint over = prevBlock->Size - allocate_size;
					if (over > 0) {
						uint hold = (uint)nextPtr + prevBlock->Size;
						Header* newFree = (Header*)hold;
						SharpOS.Kernel.ADC.MemoryUtil.MemCopy((uint)oldBlock, hold, (uint)sizeof(Header));
						newFree->Size += over;
						Free((void*)((byte*)newFree + (uint)sizeof(Header)));
						prevBlock->Next = newFree;
					}
					else
						Free(stptr);
					
					return nextPtr;
				}
				if (oldBlock->Previous->Size >= neededSize) {
					Header* prevBlock = oldBlock->Previous;
					prevBlock->Free = 0;
					prevBlock->Next = oldBlock->Next;
					prevBlock->Size = allocate_size;
					uint over = oldBlock->Size + prevBlock->Size - allocate_size;

					SharpOS.Kernel.ADC.MemoryUtil.MemCopy((uint)stptr, ((uint)prevBlock + (uint)sizeof(Header)), oldBlock->Size);

					prevBlock->Size = allocate_size;
					if (over > (uint)sizeof(Header)) {
						nextPtr = (byte*)prevBlock + prevBlock->Size + (uint)sizeof(Header);
						Header* nextNode = (Header*)nextPtr;
						nextNode->Free = 1;
						nextNode->Size = over - (uint)sizeof(Header);
						nextNode->Previous = prevBlock;
						nextNode->Next = prevBlock->Next;
						if (nextNode->Next->Previous != null)
							nextNode->Next->Previous = nextNode;
						prevBlock->Next = nextNode;

						Free(nextNode + 1);
					}
					else if (prevBlock->Next->Previous != null) {
						prevBlock->Next->Previous = prevBlock;
					}

					return (void*)((byte*)prevBlock + (uint)sizeof(Header));
				}
			}

			nextPtr = Allocate(allocate_size);
			SharpOS.Kernel.ADC.MemoryUtil.MemCopy((uint)stptr, (uint)nextPtr, oldBlock->Size);
			Free(stptr);

            Diagnostics.Assert(nextPtr != null, "MemoryManager.Allocate(uint): Allocation failed.");
			if (nextPtr == null)
			{
				throw new OutOfMemoryException("MemoryManager.Allocate(uint): Allocation failed.");
				return null;
			}

			return nextPtr;
		}

		public static unsafe void Free (void* memory)
		{
			uint memoryHeaderPointer = (uint) memory - (uint) sizeof (Header);
			Header* freeHeader = (Header*) memoryHeaderPointer;

			if (freeHeader->Next->Previous != freeHeader ||
				freeHeader->Previous->Next != freeHeader)
			{
				Diagnostics.Assert(false, "Trying to free invalid pointer. ");
				throw new System.InvalidOperationException("Trying to free invalid pointer. ");
			}
			
			allocated -= (ulong) freeHeader->Size;

			freeHeader->Free = 1;

			Header* currentNode = freeHeader;


			// I fixed this piece of code, 
			//	but i'm not sure how usefull it realy is in the first place... 
			//	can someone explain? (logicalerror)
			uint startOfBlock	= (uint)memory;
			uint endOfBlock		= startOfBlock + currentNode->Size;
			if (currentNode->Next != firstNode && 
				endOfBlock < (uint)currentNode->Next)
				currentNode->Size = (uint)currentNode->Next - startOfBlock;
			else 
			if (currentNode->Next == firstNode && 
				endOfBlock < memoryEnd)
				currentNode->Size = memoryEnd - startOfBlock;


			//Scan forward for the last consecutive free node
			if (currentNode->Next > currentNode && currentNode->Next->Free == 1)
			{
				currentNode->Size = 
					((uint)currentNode->Next - (uint)currentNode)/* - (uint)sizeof(Header)*/ +
					currentNode->Next->Size/* + (uint)sizeof(Header)*/;
				currentNode->Next = currentNode->Next->Next;
				currentNode->Next->Previous = currentNode;
			}

			//Now scan backwards and consolidate free nodes
            if (currentNode->Previous != null && currentNode->Previous->Free == 1) {
				Header* previous = currentNode->Previous;
				previous->Size = 
					((uint)currentNode->Next - (uint)currentNode)/* - (uint)sizeof(Header)*/ +
					currentNode->Size/* + (uint)sizeof(Header)*/;
				previous->Next = currentNode->Next;
				currentNode->Next->Previous = previous;

				currentNode = currentNode->Previous;
			}

			if (currentNode->Size > lastFreeNode->Size && 
				currentNode < lastFreeNode)
				lastFreeNode = currentNode;
			
			if (currentNode->Next == null ||
				currentNode->Next->Previous != currentNode ||
				(currentNode->Previous != null &&
				currentNode->Previous->Next != currentNode))
			{
				Diagnostics.Assert(false, "Free corrupted linked list. ");
			}
		}

		private static unsafe void DumpNode (string msg, Header* node)
		{
			ADC.TextMode.Write (msg);
			ADC.TextMode.Write (", Current node: ");
			ADC.TextMode.Write ((int) node);
			ADC.TextMode.Write (", Next node: ");
			ADC.TextMode.Write ((int) node->Next);
			ADC.TextMode.Write (", Prev node: ");
			ADC.TextMode.Write ((int) node->Previous);
			ADC.TextMode.Write (", Size: ");
			ADC.TextMode.Write ((int) node->Size);
			ADC.TextMode.Write (", IsFree: ");
			if (node->Free == 1)
				ADC.TextMode.Write ("true");
			else
				ADC.TextMode.Write ("false");
			ADC.TextMode.Write (", TotalAlloc: ");
			ADC.TextMode.Write ((int) allocated);
			ADC.TextMode.WriteLine ();
		}

		private static unsafe void DumpNodeSerial (Header* node)
		{
			if (!Serial.Initialized)
				return;

			Serial.COM1.Write ("Current node: ");
			Serial.COM1.Write ((uint) node);
			Serial.COM1.Write (", Next node: ");
			Serial.COM1.Write ((uint) node->Next);
			Serial.COM1.Write (", Prev node: ");
			Serial.COM1.Write ((uint) node->Previous);
			Serial.COM1.Write (", Size: ");
			Serial.COM1.Write ((uint) node->Size);
			Serial.COM1.Write (", IsFree: ");
			if (node->Free == 1)
				Serial.COM1.Write ("true");
			else
				Serial.COM1.Write ("false");
			Serial.COM1.WriteLine ();
		}

		public static void Dump ()
		{
			ADC.TextMode.WriteLine ("Memory dump: ");
			Header* currentNode = firstNode;

			do {
				DumpNode ("MemoryManager", currentNode);

				currentNode = currentNode->Next;
			} while (currentNode != firstNode);

			ADC.TextMode.WriteLine ();
		}

		public static void DumpSerial ()
		{
			if (!Serial.Initialized)
				return;
						
			Serial.COM1.WriteLine ("-------------");
			Serial.COM1.WriteLine ("Memory dump: ");
			
			Header* currentNode = firstNode;
			
			if (currentNode == null)
				Serial.COM1.WriteLine ("null");

			while (currentNode != null) {
				DumpNodeSerial (currentNode);

				currentNode = currentNode->Next;
				if (currentNode == firstNode)
					break;
			}

			Serial.COM1.WriteLine ();
		}

		public static void __RunTests ()
		{
			__ExclusivityTest ();
			//__StressTest ();
			//DumpSerial();
			//TextMode.WriteLine ("Memory tests completed.");
		}

		public const int ExclusivityTestLimit = 50;
		public const int ExclusivityTestBlockSize = 4096;
		public const int MaxStressAllocSize = 256;
		public const int StressAllocLimit = 1000;

		public static void __StressTest ()
		{
			int num = 1;
			byte** ptrs = stackalloc byte*[StressAllocLimit];
			int failures = 0;
			int exclusiveFailures = 0;
			byte *kstart, kend;

			{
				void *ks, ke;
				EntryModule.GetKernelLocation (out ks, out ke);
				kstart = (byte*)ks;
				kend = (byte*)ke;
			}

			Serial.COM1.WriteLine ("Memory Management: Stress Test:");

			Serial.COM1.Write (" - Allocating ", StressAllocLimit);
			Serial.COM1.Write (" buffers");

			for (int x = 0; x < StressAllocLimit; ++x) {
				ptrs [x] = (byte*)MemoryManager.Allocate ((uint)num);

				for (int y = 0; y < num; ++y)
					ptrs [x][y] = (byte)(num - 1);

				if (num + 1 >= 256)
					num = 1;
				else
					++num;
			}

			Serial.COM1.WriteLine (" - Checking allocations...");
			// Check the allocations for the correct number.
			num = 1;

			for (int x = 0; x < StressAllocLimit; ++x) {
				byte *alloc = ptrs [x];
				int oldFail = exclusiveFailures;

				for (int y = 0; y < num; ++y) {
					if (alloc[y] != (byte)(num - 1)) {
						++failures;
						break;
					}
				}

				if (alloc <= kend) {
					Serial.COM1.Write (" - Allocation #", x);
					Serial.COM1.Write (" is not exclusive: ");
					Serial.COM1.Write (" overlaps with kernel!");
					Serial.COM1.WriteLine ();
					++exclusiveFailures;
					continue;
				}

				// Test that the entry is also mutually exclusive from the others.

				for (int y = 0; y < StressAllocLimit; ++y) {
					byte *xa, ya;
					bool fail = false;

					if (x == y)
						continue;

					xa = alloc;
					ya = ptrs [y];

					if (ya > xa && ya < xa + num) {
						Serial.COM1.Write (" - Allocation #", x);
						Serial.COM1.Write (" is not exclusive: ");
						Serial.COM1.Write ("Overlap with alloc #", y);
						Serial.COM1.WriteLine ();
						fail = true;
					} else if (xa == ya) {
						Serial.COM1.Write (" - Allocation #", x);
						Serial.COM1.Write (" is not exclusive: ");
						Serial.COM1.Write ("Same as alloc #", y);
						Serial.COM1.WriteLine ();
						fail  =  true;
					}

					/*
					if (ya > xa && ya <= xa + ExclusivityTestBlockSize)
						fail = true;
					else if (xa > ya && xa <= ya + ExclusivityTestBlockSize)
						fail = true;
					else if (xa == ya)
						fail  =  true;
					else if (xa <= kend)
						fail = true;
					*/

					if (fail) {
						exclusiveFailures++;
						break;
					}
				}

				if (oldFail == exclusiveFailures) {
					Serial.COM1.Write (" - Allocation #", x);
					Serial.COM1.WriteLine (" is correct");
				}

				if (num + 1 >= 256)
					num = 1;
				else
					++num;
			}

			Serial.COM1.Write (" - Failures: ", failures * 100 / StressAllocLimit);
			Serial.COM1.WriteLine ("%");
			Serial.COM1.Write (" - Exclusivity Failures: ",
				exclusiveFailures * 100 / StressAllocLimit);
			Serial.COM1.WriteLine ("%");

			// Deallocate

			for (int x = 0; x < StressAllocLimit; ++x) {
				MemoryManager.Free (ptrs [x]);
			}
		}

		public static void __ExclusivityTest ()
		{
			byte** ptrs = stackalloc byte*[ExclusivityTestLimit];
			int exclusiveFailures = 0;
			byte *kstart, kend;

			{
				void *ks, ke;
				EntryModule.GetKernelLocation (out ks, out ke);
				kstart = (byte*)ks;
				kend = (byte*)ke;
			}

			Serial.COM1.WriteLine ("Memory Management: Exclusivity Test:");
			// Make our allocations

			for (int x = 0; x < ExclusivityTestLimit; ++x) {
				ptrs [x] = (byte*)MemoryManager.Allocate (ExclusivityTestBlockSize);
			}

			// Test that each entry is mutually exclusive

			for (int x = 0; x < ExclusivityTestLimit; ++x) {
				bool fail = false;

				if (ptrs [x] <= kend) {
					Serial.COM1.Write ("Allocation #", x);
					Serial.COM1.Write (" is not exclusive: ");
					Serial.COM1.Write (" overlaps with kernel!");
					fail = true;
				}

				for (int y = 0; !fail && y < ExclusivityTestLimit; ++y) {
					byte *xa, ya;

					if (x == y)
						continue;

					xa = ptrs [x];
					ya = ptrs [y];

					if (ya > xa && ya <= xa + ExclusivityTestBlockSize) {
						Serial.COM1.Write (" - Allocation #", x);
						Serial.COM1.Write (" is not exclusive: ");
						Serial.COM1.Write ("Overlap with alloc #", y);
						fail = true;
					} else if (xa > ya && xa <= ya + ExclusivityTestBlockSize) {
						Serial.COM1.Write (" - (inv) Allocation #", x);
						Serial.COM1.Write (" is not exclusive: ");
						Serial.COM1.Write ("Overlap with alloc #", y);
						fail = true;
					} else if (xa == ya) {
						Serial.COM1.Write ("Allocation #", x);
						Serial.COM1.Write (" is not exclusive: ");
						Serial.COM1.Write ("Same as alloc #", y);
						fail  =  true;
					}

					/*
					if (xa > ya && xa + ExclusivityTestBlockSize < ya)
						fail = true;
					else if (ya > xa && ya + ExclusivityTestBlockSize < xa)
						fail = true;
					else if (xa == ya)
						fail  =  true;
					*/
				}

				if (fail)
					exclusiveFailures++;
			}

			Serial.COM1.Write (" - Failures: ", exclusiveFailures * 100 / ExclusivityTestLimit);
			Serial.COM1.WriteLine ("%");

			// Deallocate

			for (int x = 0; x < ExclusivityTestLimit; ++x) {
				MemoryManager.Free (ptrs [x]);
			}
		}
	}
}
