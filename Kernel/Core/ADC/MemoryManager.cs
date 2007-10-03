using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SharpOS.Memory;

namespace SharpOS.ADC
{
	// temp. memory manager, this will be used to test paging
	public static unsafe class MemoryManager
	{
		private static uint		memoryEnd			= 0;
		private static uint		memoryStart			= 0;
		private static uint		numberOfPages		= 1;
		private static uint		minimumBlockSize	= (uint)(sizeof(Header) + sizeof(uint));

		//TODO: remove prevnode dependency
		[StructLayout(LayoutKind.Sequential)]
		private struct Header
		{
			public Header*		nextNode;
			public Header*		prevNode;
			public uint			nodeSize;
		}

		private static Header*	firstUsedNode		= null;
		private static Header*	firstEmptyNode		= null;

		public static unsafe void Setup()
		{
			memoryEnd = memoryStart =
				(uint)PageAllocator.RangeAlloc(numberOfPages);

			if (memoryStart == 0)
			{
				Kernel.Error("Failed to initialize memory manager");
				return;
			}

			memoryEnd += (numberOfPages * Pager.AtomicPageSize);

			TextMode.Write("Memory: ");
			TextMode.Write((int)memoryStart);
			TextMode.Write(" - ");
			TextMode.Write((int)memoryEnd);
			TextMode.WriteLine();


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

		public static unsafe void* Allocate(uint allocate_size)
		{
			// Align size to 32bit..
			if ((allocate_size & 3) != 0)
			{
				allocate_size -= allocate_size & 3;
				allocate_size += 4;
			}

			if (firstEmptyNode == null)
				return null;
						
			Header* currentNode		= firstEmptyNode;
			// Try to find a node with the exact same size...
			while (currentNode != null)
			{
				if (currentNode->nodeSize == allocate_size)
					break;
				currentNode = currentNode->nextNode;
			}
			if (currentNode == null)
				currentNode = firstEmptyNode;

			if (allocate_size > currentNode->nodeSize)
				// couldn't find a large enough block..
				return null;
			
			// calculate the position of the block to return
			void*	currentBlock	= (void*)(((byte*)currentNode) + sizeof(Header));
			

			//
			// Remove the node from the empty list (current node)
			//
			if (currentNode->prevNode != null)
				currentNode->prevNode->nextNode = currentNode->nextNode;
			else
				firstEmptyNode = currentNode->nextNode;

			if (currentNode->nextNode != null)
				currentNode->nextNode->prevNode = currentNode->prevNode;
			currentNode->prevNode = null;
			
			//
			// Add new node to the start of the used node list (new nodes are more likely to live short than long)
			//
			currentNode->nextNode = firstUsedNode;
			if (firstUsedNode != null)
			{
				firstUsedNode->prevNode = currentNode;
			}
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

		//TODO: maybe use a hash table to find a memory block faster?
		public static unsafe void Free(void* memory)
		{
			uint address = (uint)memory;
			if (address == 0)
			{
				Kernel.Error("Memory handle not initialized");
				return;
			}
			if (address < memoryStart && address > memoryEnd)
			{
				Kernel.Error("Memory handle not within limits of used memory");
				return;
			}

			Header* currentNode = (Header*)(address - sizeof(Header));

			//
			// Some sanity checking ..
			//
			if (((uint)memory & 3) != 0)
			{
				Kernel.Error("((uint)memory & 3) != 0");
				return;
			} else
				if ((currentNode->nodeSize & 3) != 0)
				{
					Kernel.Error("(currentNode->nodeSize & 3) != 0");
					return;
				} else
				{
					if (((uint)memoryStart + currentNode->nodeSize) > (uint)memoryEnd)
					{
						Kernel.Error("((uint)memoryStart + currentNode->nodeSize) > (uint)memoryEnd");
						return;
					}

					if (currentNode->prevNode == null)
					{
						if (firstUsedNode != currentNode)
						{
							Kernel.Error("firstUsedNode != currentNode");
							return;
						}
					} else
					{
						if (currentNode->prevNode->nextNode != currentNode)
						{
							Kernel.Error("currentNode->prevNode->nextNode != currentNode");
							return;
						}
					}
					if (currentNode->nextNode != null)
					{
						if (currentNode->nextNode->prevNode != currentNode)
						{
							Kernel.Error("currentNode->nextNode->prevNode != currentNode");
							return;
						}
					}
				}

			//
			// Find allocated node ...
			//
			Header* used_iterator = firstUsedNode;
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

		private static unsafe void AddToEmpty(Header* currentNode)
		{
			//
			// Look for adjacent memory blocks and merge them
			//
			Header* iterator = firstEmptyNode;
			Header* nextNode = (Header*)(((byte*)currentNode) + currentNode->nodeSize + sizeof(Header));
			Header* nextIteratorNode;
			while (iterator != null)
			{
				nextIteratorNode = (Header*)(((byte*)iterator) + iterator->nodeSize + sizeof(Header));
				if (nextIteratorNode == currentNode) // iterator lies in front of currentNode
				{
					//
					// Remove node from list
					//
					if (iterator->prevNode != null)
						iterator->prevNode->nextNode = iterator->nextNode;
					else
						firstEmptyNode = iterator->nextNode;

					if (iterator->nextNode != null)
						iterator->nextNode->prevNode = iterator->prevNode;

					//
					// Merge nodes
					//
					uint offset = (uint)(currentNode->nodeSize + sizeof(Header));
					iterator->nodeSize += offset;
					nextNode = (Header*)(((byte*)iterator) + iterator->nodeSize + sizeof(Header));
					currentNode = iterator;

					// Probably not the most efficient choice...
					iterator = firstEmptyNode;
				} else
					if (iterator == nextNode)	// currentNode lies in front of iterator
					{
						//
						// Remove node from list
						//
						if (iterator->prevNode != null)
							iterator->prevNode->nextNode = iterator->nextNode;
						else
							firstEmptyNode = iterator->nextNode;

						if (iterator->nextNode != null)
							iterator->nextNode->prevNode = iterator->prevNode;

						//
						// Merge nodes
						//
						uint offset = (uint)(iterator->nodeSize + sizeof(Header));
						currentNode->nodeSize += offset;
						nextNode += offset;

						// Probably not the most efficient choice...
						iterator = firstEmptyNode;
					} else
						iterator = iterator->nextNode;
			}

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
					firstEmptyNode->prevNode = currentNode;
					currentNode->nextNode = firstEmptyNode;
					currentNode->prevNode = null;
					firstEmptyNode = currentNode;
					return;
				}

				// find the position to place the of the new empty node
				Header* insert_iterator = firstEmptyNode;
				while (insert_iterator->nextNode != null)
				{
					insert_iterator = insert_iterator->nextNode;
					if (insert_iterator->nodeSize < currentNode->nodeSize)
					{
						currentNode->nextNode = insert_iterator;
						currentNode->prevNode = insert_iterator->prevNode;
						insert_iterator->prevNode = currentNode;
						return;
					}
				}

				// add node to end of list
				insert_iterator->nextNode = currentNode;
				currentNode->prevNode = insert_iterator;
				currentNode->nextNode = null;
			}
		}

		public static unsafe void Dump()
		{
			if (memoryEnd < memoryStart + minimumBlockSize)
			{
				Kernel.Error("Memory manager not initialized");
				return;
			}
			ADC.TextMode.SaveAttributes();

			ADC.TextMode.SetAttributes(TextColor.Cyan, TextColor.Black);

			Header* iterator = firstEmptyNode;
			ADC.TextMode.WriteLine("Empty:");
			while (iterator != null)
			{
				DumpNode(iterator);
				iterator = iterator->nextNode;
			}

			iterator = firstUsedNode;
			ADC.TextMode.WriteLine("Used:");
			while (iterator != null)
			{
				DumpNode(iterator);
				iterator = iterator->nextNode;
			}

			ADC.TextMode.WriteLine();
			ADC.TextMode.RestoreAttributes();
		}

		private static unsafe void DumpNode(Header* currentNode)
		{
			if (currentNode == null)
			{
				ADC.TextMode.Write("null ");
			} else
			{
				ADC.TextMode.Write("Pointer: ");
				ADC.TextMode.Write((int)currentNode);

				ADC.TextMode.Write(" Prev: ");
				ADC.TextMode.Write((int)currentNode->prevNode);

				ADC.TextMode.Write(" Next: ");
				ADC.TextMode.Write((int)currentNode->nextNode);

				ADC.TextMode.Write(" Size: ");
				ADC.TextMode.Write((int)currentNode->nodeSize);
			}

			ADC.TextMode.WriteLine();
		}
	}
}
