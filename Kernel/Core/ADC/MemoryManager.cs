using System;
using System.Collections.Generic;
using System.Text;

namespace SharpOS.ADC
{
	// TODO: add paging support to memory manager to ensure that memory seems to be continous even tough areas of memory
	//			are used by, for example, hardware (0xb0000 / 0xb8000 textmode for example)
	public static unsafe class MemoryManager
	{
		private static uint		memoryEnd			= 0;
		private static uint		memoryStart			= 0;
		private static uint		minimumBlockSize	= 0;

		private struct Header
		{
			public Header*		nextNode;
			public Header*		prevNode;	//TODO: remove dependency on this one..
			public uint			nodeSize;
		}

		private static Header*	firstUsedNode		= null;
		private static Header*	firstEmptyNode		= null;

		public static unsafe void Setup(void* start, void* end)
		{
			minimumBlockSize	= (uint)(sizeof(Header) + 4);
			memoryStart			= (uint)start;
			memoryEnd			= (uint)end;

			// align to 32bit
			if ((((uint)memoryStart) & 3) != 0)
				memoryStart = ((memoryStart - ((uint)memoryStart) & 3) + 4);
			// align to 32bit
			memoryEnd -= ((uint)memoryEnd) & 3;


			firstEmptyNode = (Header*)memoryStart;
			firstEmptyNode->nextNode = null;
			firstEmptyNode->prevNode = null;
			firstEmptyNode->nodeSize = (uint)((memoryEnd - memoryStart) - sizeof(Header));
		}

		// TODO: Don't just use the largest block, but try to find a block which is exactly the right size first
		public static unsafe void* Allocate(uint allocate_size)
		{
			// Align size to 32bit..
			if ((allocate_size & 3) != 0)
			{
				allocate_size -= allocate_size & 3;
				allocate_size += 4;
			}

			Header* currentNode		= firstEmptyNode;
			if (allocate_size > currentNode->nodeSize)
				// couldn't find a large enough block..
				return null;
			
			// calculate the position of the block to return
			void*	currentBlock	= (void*)(((byte*)currentNode) + sizeof(Header));
			
			//
			// Remove the first node from the empty list (current node)
			//
			firstEmptyNode			= currentNode->nextNode;
			currentNode->prevNode	= null;


			//
			// Add new node to the start of the used node list (new nodes are more likely to live short than long)
			//
			currentNode->nextNode = firstUsedNode;
			if (firstUsedNode != null)
				firstUsedNode->prevNode = currentNode;
			firstUsedNode = currentNode;
			
			
			//
			// Check if new allocation size is (almost) equal to size of node
			//
			if (allocate_size >= (currentNode->nodeSize - minimumBlockSize))
				return currentBlock;	// our work is done...
			
			//
			// Create a new node with the remainder of the block
			//
			uint	offset	= (uint)(sizeof(Header) + allocate_size);
			Header* newNode = (Header*)(((byte*)currentNode) + offset);
			newNode->nodeSize = currentNode->nodeSize - offset;

			//
			// Set the current node to the allocated size
			//
			currentNode->nodeSize = allocate_size;

			//
			// Add node to empty list
			//
			AddToEmpty(newNode);

			return currentBlock;
		}

		// TODO: Look for adjacent memory blocks and merge them
		private static unsafe void AddToEmpty(Header* currentNode)
		{
			if (firstEmptyNode == null)
			{
				// set the start of the linked list to the new node
				firstEmptyNode = currentNode;		
				firstEmptyNode->nextNode = null;
				firstEmptyNode->prevNode = null;
			} else
			{
				// check if the new node needs to be added to the start
				if (firstEmptyNode->nodeSize < currentNode->nodeSize)
				{
					currentNode->nextNode = firstEmptyNode->nextNode;
					currentNode->prevNode = null;
					firstEmptyNode = currentNode;
					return;
				}

				// find the position to place the of the new empty node
				Header* iterator = firstEmptyNode;
				while (iterator->nextNode != null)
				{
					iterator = iterator->nextNode;
					if (iterator->nodeSize < currentNode->nodeSize)
					{
						currentNode->prevNode	= iterator->prevNode;
						iterator->prevNode		= currentNode;
						currentNode->nextNode	= iterator;
						return;
					}
				}

				// add node to end of list
				iterator->nextNode		= currentNode;
				currentNode->prevNode	= iterator;
				currentNode->nextNode	= null;
			}
		}
		
		//TODO: maybe use a hash table to find a memory block faster?
		public static unsafe void Release(void* memory)
		{
			uint address = (uint)memory;
			if (address < memoryStart && address > memoryEnd)
			{
				Kernel.Error("Memory handle not within limits of used memory");
				return;
			}

			Header* currentNode = (Header*)(address - sizeof(Header));

			//
			// Some sanity checking ..
			//
			bool valid = true;
			if (((uint)memory & 3) != 0 || (currentNode->nodeSize & 3) != 0)
			{
				valid = false;
			} else
			{
				if (((uint)memoryStart + currentNode->nodeSize) > (uint)memoryEnd)
					valid = false;

				if (currentNode->prevNode == null)
				{
					if (firstUsedNode != currentNode) 
						valid = false;
				} else
				{
					if (currentNode->prevNode->nextNode != currentNode)
						valid = false;
				}
				if (currentNode->nextNode != null)
				{
					if (currentNode->nextNode->prevNode != currentNode)
						valid = false;
				}
			}
			if (!valid)
			{
				Kernel.Error("Invalid or corrupted memory handle");
				return;
			}

			
			//
			// Find allocated node ...
			//
			Header* used_iterator		= firstUsedNode;
			while (used_iterator != null)
			{
				if (used_iterator == currentNode)
				{
					//
					// Remove node from used list
					//
					if (used_iterator->prevNode != null)
						used_iterator->prevNode->nextNode = used_iterator->nextNode;
					else
						firstUsedNode = used_iterator->nextNode;
					
					if (used_iterator->nextNode != null)
						used_iterator->nextNode->prevNode = used_iterator->prevNode;

					//
					// Add node to empty list
					//
					AddToEmpty(used_iterator);
					
					// our work here is done..
					return;
				}
				used_iterator = used_iterator->nextNode;
			}
			
			//
			// Could not find node!
			//
			Kernel.Error("Invalid memory handle (handle not found / memory already freed)");
		}
	}
}
