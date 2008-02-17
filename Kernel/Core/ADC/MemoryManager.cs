//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	�sgeir Halld�rsson <asgeir.halldorsson@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
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
			Kernel.Diagnostics.Assert (firstNode != null, "MemoryManager.Allocate(uint): Unable to allocate because the MemoryManager has not been initialized");

			Header* currentNode = lastFreeNode;
			
			// Allign the block to 4? or 8?
			if (allocate_size % 4 != 0)
				allocate_size = ((allocate_size / 4) + 1) * 4;

			// FIXME: Use a free list as this is VERY slow but works, asgeirh 2007-11-16
			while (currentNode != null) {
				if (currentNode->Free == 1 && currentNode->Size >= allocate_size) {
					break;
				}

				currentNode = currentNode->Next;
				if (currentNode == lastFreeNode) {
					currentNode = null;
					break;
				}
			}

			Diagnostics.Assert (currentNode != null, "MemoryManager.Allocate(uint): Unable to allocate memory; no sufficiently sized nodes available");
			if (currentNode == null)
				return null;

			uint memoryLeft = currentNode->Size - allocate_size;

			currentNode->Free = 0;

			if (memoryLeft > (uint) sizeof (Header)) {
				//If there is enough room to squeeze in a new node after this one, then do it

				currentNode->Size = allocate_size;
				void *nextPtr = (byte*)currentNode + currentNode->Size + sizeof (Header);

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
			}

			// Check to see if the next node is free, larger in size than lastFreeNode, and is also
			// lower in range than lastFreeNode.  If all these are true, set lastFreeNode to the next
			// node.

			if (currentNode->Next->Free == 1 && currentNode->Next->Size > lastFreeNode->Size &&
			    currentNode->Next < lastFreeNode)
				lastFreeNode = currentNode->Next;

			uint retPtr = (uint) currentNode + (uint) sizeof (Header);
			allocated += (ulong) currentNode->Size;

			Diagnostics.Assert (retPtr != 0, "MemoryManager.Allocate(uint): Allocation failed");

			// TODO X86.MemoryUtil.MemSet should not be called directly
			SharpOS.Kernel.ADC.X86.MemoryUtil.MemSet (0, retPtr, allocate_size);

			return (void*) retPtr;
		}

		//TODO: maybe use a hash table to find a memory block faster?
		public static unsafe void Free (void* memory)
		{
			uint memoryHeaderPointer = (uint) memory - (uint) sizeof (Header);
			Header* freeHeader = (Header*) memoryHeaderPointer;

			freeHeader->Free = 1;

			Header* currentNode = freeHeader;

			//Scan forward for the last consecutive free node
			while (currentNode->Next != firstNode && currentNode->Next->Free == 1)
				currentNode = currentNode->Next;

			//Now scan backwards and consolidate free nodes
			while (currentNode->Previous != null && currentNode->Previous->Free == 1) {
				Header* previous = currentNode->Previous;
				previous->Size = currentNode->Size + (uint) sizeof (Header);
				if (currentNode->Next != null) {
					previous->Next = currentNode->Next;
					currentNode->Next->Previous = previous;
				} else {
					previous->Next = null;
				}

				currentNode = currentNode->Previous;
			}

			if (currentNode->Next->Free == 1 && currentNode->Next->Size > lastFreeNode->Size &&
			    currentNode->Next < lastFreeNode)
				lastFreeNode = currentNode;
		}

		private static unsafe void DumpNode (string msg, Header* node)
		{
			ADC.TextMode.Write (msg);
			ADC.TextMode.Write (", Current node: ");
			ADC.TextMode.Write ((int) node);
			ADC.TextMode.Write (", Next node: ");
			ADC.TextMode.Write ((int) node->Next);
			ADC.TextMode.Write (", Size: ");
			ADC.TextMode.Write ((int) node->Size);
			ADC.TextMode.Write (", IsFree: ");
			ADC.TextMode.Write ((int) node->Free, false);
			ADC.TextMode.Write (", TotalAlloc: ");
			ADC.TextMode.Write ((int) allocated);
			ADC.TextMode.WriteLine ();
		}

		private static unsafe void DumpNodeSerial (string msg, Header* node)
		{
			Serial.COM1.Write (msg);
			Serial.COM1.Write (", Current node: ");
			Serial.COM1.Write ((int) node);
			Serial.COM1.Write (", Next node: ");
			Serial.COM1.Write ((int) node->Next);
			Serial.COM1.Write (", Size: ");
			Serial.COM1.Write ((int) node->Size);
			Serial.COM1.Write (", IsFree: ");
			Serial.COM1.Write ((node->Free == 1 ? "true" : "false"));
			Serial.COM1.Write (", TotalAlloc: ");
			Serial.COM1.Write ((int) allocated);
			Serial.COM1.WriteLine ();
		}

		public static void Dump ()
		{
			ADC.TextMode.WriteLine ("Memory dump: ");
			Header* currentNode = firstNode;

			// FIXME: Use a free list as this is VERY slow but works, asgeirh 2007-11-16
			do {
				DumpNode ("MemoryManager", currentNode);

				currentNode = currentNode->Next;
			} while (currentNode != firstNode);

			ADC.TextMode.WriteLine ();
		}

		public static void DumpSerial ()
		{
			Serial.COM1.WriteLine ("Memory dump: ");
			Header* currentNode = firstNode;

			// FIXME: Use a free list as this is VERY slow but works, asgeirh 2007-11-16
			while (currentNode != null) {
				DumpNodeSerial ("MemoryManager", currentNode);

				currentNode = currentNode->Next;
			}

			Serial.COM1.WriteLine ();
		}

		public static void __RunTests ()
		{
			__ExclusivityTest ();
			//__StressTest ();
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
