// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	Ásgeir Halldórsson <asgeir.halldorsson@gmail.com>
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
			public Header* Next;
			public Header* Previous;
			public uint Size;
			public bool Free;
		}

		private static Header* firstNode = null;

		public static unsafe void Setup ()
		{
			memoryStart = (uint) Pager.PageAlign ((void*) startAddress, 0);
			memoryEnd = UInt32.MaxValue;

			firstNode = (Header*) memoryStart;
			firstNode->Previous = null;
			firstNode->Next = null;
			firstNode->Size = memoryEnd - memoryStart - (uint) sizeof (Header);
			firstNode->Free = true;
		}

		public static unsafe void* Allocate (uint allocate_size)
		{
			Header* currentNode = firstNode;

			// FIXME: Use a free list as this is VERY slow but works, asgeirh 2007-11-16
			while (currentNode != null) {
				if (currentNode->Free && currentNode->Size >= allocate_size) {
					break;
				}

				currentNode = currentNode->Next;
			}

			Diagnostics.Assert (currentNode != null, "MemoryManager.Allocate(uint): Unable to allocate memory; no sufficiently sized nodes available");
			if (currentNode == null)
				return null;

			uint memoryLeft = currentNode->Size - allocate_size;

			currentNode->Free = false;

			if (memoryLeft > (uint) sizeof (Header)) {//If there is enough room to squeeze in a new node after this one, then do it
				currentNode->Size = allocate_size;
				uint nextPtr = (uint) currentNode + currentNode->Size + (uint) sizeof (Header);

				Header* nextNode = (Header*) nextPtr;
				nextNode->Free = true;
				nextNode->Size = memoryLeft - (uint) sizeof (Header);
				nextNode->Previous = currentNode;
				nextNode->Next = null;

				if (currentNode->Next != null) {// There are more nodes in list so injection is required					
					Header* tmpNext = currentNode->Next;

					nextNode->Next = tmpNext;
					tmpNext->Previous = nextNode;
				}

				currentNode->Next = (Header*) nextPtr;
			}

			uint retPtr = (uint) currentNode + (uint) sizeof (Header);
			allocated += (ulong) currentNode->Size;

			Diagnostics.Assert (retPtr != 0, "MemoryManager.Allocate(uint): Allocation failed");

			return (void*) retPtr;
		}

		//TODO: maybe use a hash table to find a memory block faster?
		public static unsafe void Free (void* memory)
		{
			uint memoryHeaderPointer = (uint) memory - (uint) sizeof (Header);
			Header* freeHeader = (Header*) memoryHeaderPointer;

			freeHeader->Free = true;

			Header* currentNode = freeHeader;
			//Scan forward for the last consecutive free node
			while (currentNode->Next != null && currentNode->Next->Free)
				currentNode = currentNode->Next;
			//Now scan backwards and consolidate free nodes
			while (currentNode->Previous != null && currentNode->Previous->Free) {
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
			ADC.TextMode.Write (node->Free);
			ADC.TextMode.Write (", TotalAlloc: ");
			ADC.TextMode.Write ((int) allocated);
			ADC.TextMode.WriteLine ();
		}

		public static void Dump ()
		{
			ADC.TextMode.WriteLine ("Memory dump: ");
			Header* currentNode = firstNode;

			// FIXME: Use a free list as this is VERY slow but works, asgeirh 2007-11-16
			while (currentNode != null) {
				DumpNode ("MemoryManager", currentNode);

				currentNode = currentNode->Next;
			}

			ADC.TextMode.WriteLine ();
		}
	}
}
