// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using SharpOS.AOT;
using SharpOS.ADC;

namespace SharpOS.Memory {
	
	/// <summary>
	/// The PageAllocator class handles physical memory page allocation and provides
	/// the OS an interface for memory paging/mapping. The PageAllocator class is
	/// portable, making use of the SharpOS.ADC.Pager class to implement platform-
	/// specific paging mechanisms.
	/// </summary>
	public unsafe class PageAllocator {
		
		#region Global state
		
		static byte *kernelStartPage;	// physical pointer to the first page occupied by the kernel
		static uint kernelSize;		// kernel image size (pages)
		static uint totalPages;		// amount of pages with RAM
		
		static uint *fpStack;		// stack containing free page addresses
		static uint fpStackSize;	// size of the free page stack (KB)
		static uint fpStackPointer;
		
		static uint *rpStack;		// stack containing reserved page addresses
		static uint rpStackSize;	// size of the reserved page stack (KB)
		static uint rpStackPointer;
		
		static byte *pagingData;	// data used for paging
		static uint pagingDataSize;	// size of pagingData

		#endregion
		#region Nested types
		
		public enum Errors: uint {
			Success = 0,
			Unknown,
			NotImplemented,

			/// <summary>
			/// The given granularity is not available on this platform, or
			/// cannot be specified for the current operation.
			/// </summary>
			UnsupportedGranularity,

			/// <summary>
			/// Attributes are not supported for pages of the given granularity.
			/// </summary>
			NoAttributesForGranularity,

			/// <summary>
			/// The buffer provided by the kernel is insufficiently sized or
			/// placed to be used as the mapping control data buffer.
			/// </summary>
			UnusablePageControlBuffer,
		}

		#endregion
		#region Setup
		
		/// <summary>
		/// Initializes page management and paging. Using <see cref="Alloc" />
		/// and related management functions before calling this function results
		/// in a kernel panic. After this function is called, the
		/// <see cref="ReservePage"/> and <see cref="ReservePageRange" />
		/// functions can be used to reserve memory that should not be allocated.
		/// You should ensure that no memory allocations have happened between
		/// calling this function and reserving memory.
		/// </summary>
		public static void Setup (byte *kernelOffset, uint _kernelSize, uint totalMem)
		{
			PagingMemoryRequirements *pagingMemory = stackalloc PagingMemoryRequirements [1];
			byte *start = null;
			
			// Determine the pages used by the kernel and the total pages in the system
			kernelStartPage = (byte*)PtrToPage(kernelOffset);
			kernelSize = (_kernelSize / Pager.AtomicPageSize) + 1;
			totalPages = totalMem / (Pager.AtomicPageSize / 1024);

			start = (byte*)((uint)kernelStartPage + (kernelSize * Pager.AtomicPageSize));
			
			// Allocate the free page stack
			fpStack = (uint*)start;
			fpStackPointer = 0;
			fpStackSize = (totalPages * 4) / Pager.AtomicPageSize;
			
			start += fpStackSize * Pager.AtomicPageSize;
			
			// Allocate the reserved page stack
			rpStack = (uint*)start;
			rpStackPointer = 0;
			rpStackSize = 1;
			
			// Allocate paging information
			pagingMemory->Start = (byte*)PtrToPage(start + (rpStackSize * 1024) + (Pager.AtomicPageSize - 1));
			Pager.GetMemoryRequirements (totalMem, pagingMemory);
			pagingData = (byte*)pagingMemory->Start;
			pagingDataSize = pagingMemory->AtomicPages;
			
			// Reserve the memory ranges we're using.
			ReservePageRange (kernelStartPage, kernelSize, "kernel");
			ReservePageRange (fpStack, fpStackSize, "fpstack");
			ReservePageRange (rpStack, rpStackSize, "rpstack");
			ReservePageRange (pagingData, pagingDataSize, "paging");
			
			byte* page = (byte*)0;
			for (int i = 0; i < totalPages; ++i, page += Pager.AtomicPageSize)
			{
				//if (!PageAllocator.IsPageReserved(page))	//..ugh
					PushFreePage(page);
			}

			bool paging = true;//CommandLine.ContainsOption ("-paging");
			if (paging)
			{
				// FIXME: the value we get back from Pager.Setup is not the same value that 
				//		  we 'return' from inside the method itself!!!!
				Errors error = Errors.Unknown;
				Pager.Setup(totalMem, pagingData, pagingDataSize, &error);
				
				if (error != Errors.Success)
				{
					PrintError(error);
					return;
				}
			} else
				Kernel.Warning("Paging not set in commandline!");
			
			if (paging)
			{
				Errors error = Errors.Unknown;
				Pager.Enable(&error);
				if (error != Errors.Success)
					PrintError(error);
			}
		}

		private static void PrintError(Errors error)
		{
			ADC.TextMode.Write("(");
			ADC.TextMode.Write((int)error);
			ADC.TextMode.Write(") ");

			switch (error)
			{
				case Errors.NoAttributesForGranularity:	Kernel.Error("NoAttributesForGranularity"); return;
				case Errors.NotImplemented:				Kernel.Error("NotImplemented"); return;
				case Errors.Unknown:					Kernel.Error("Unknown"); return;
				case Errors.UnsupportedGranularity:		Kernel.Error("UnsupportedGranularity"); return;
				case Errors.UnusablePageControlBuffer:	Kernel.Error("UnusablePageControlBuffer"); return;
				case Errors.Success:					Kernel.Error("Success"); return;
				default:								Kernel.Error("Garbage"); return;
			}
		}
		
		#endregion
		#region IsPageFree() and IsPageReserved()
		
		/// <summary>
		/// Checks if a given page is allocated. If the page is
		/// allocated, the method returns true. This function
		/// starts from the current stack pointer and goes
		/// backward.
		/// </summary>
		public static bool IsPageFree (void *page)
		{
			uint fsp = fpStackPointer;
			
			while (true) {
				if (fpStack[fsp] == (uint)page)
					return false;
			
				if (fsp == 0)
					break;
					
				--fsp;
			}
			
			return true;
		}
		
		/// <summary>
		/// Returns true if the given page is reserved (that is, not available for allocation).
		/// </summary>
		public static bool IsPageReserved (void *page)
		{
			uint sp = 0;
			uint *ptr = rpStack;
			
			while (sp < rpStackPointer) {
				if (*ptr == (uint)page)
					return true;
				++sp;
				++ptr;
			}
			
			return false;
		}
		
		#endregion
		#region Alloc / Dealloc
		
		/// <summary>
		/// Allocates a page of memory. This function returns null when out of memory.
		/// </summary>
		public static void *Alloc ()
		{
			void *page = null;
			
			if (fpStackPointer == 0)
				return null;
			
			do {
				page = PopFreePage ();
			} while ((uint)page != 0x1U || IsPageReserved (page));
			
			return page;
		}
		
		/// <summary>
		/// Allocates a contiguous range of pages. This function returns null on failure.
		/// </summary>
		public static void *RangeAlloc (uint count)
		{
			uint fsp = fpStackPointer;
			
			// iterate through free pages
			
			while (true) {
				uint fsp2 = fpStackPointer;
				uint *stackLoc = stackalloc uint[(int)count];
				uint found = 0;
				uint start = fpStack[fsp];
				
				// skip invalidated stack entries
				
				if (start == 0x1U)
					goto next;
					
				stackLoc[0] = fsp;
				
				// check if this page starts a big enough contiguous range,
				// and put the stack locations of the free pages into stackLoc
				
				while (true) {
					if (fsp2 == fsp && fsp2 > 1)
						--fsp2;
					
					for (int x = 1; x < count; ++x) {
						if (fpStack[fsp2] != 0x1 &&
								fpStack[fsp2] == (uint)((byte*)start + x * Pager.AtomicPageSize)) {
								
							stackLoc[x] = fsp2;
							++found;
							
							break;
						}
					}
					
					if (found == count-1)
						break;
						
					if (fsp2 == 0)
						return null;
						
					--fsp2;
				}
				
				if (found == count-1) {
					// found our memory, invalidate the free page stack entries 
					// related to this allocation.
					
					for (int x = 0; x < count; ++x)
						fpStack[stackLoc[x]] = 0x1U;
					
					/*
					// use stackLoc to remove the gaps in the free page stack created by this allocation.
					
					if (fpStackPointer - maxUsedFsp >= count) {
						// the last (count) pages on the free page stack are not used by this
						// allocation. We can afford to merely move the last (count) pages into
						// stack locations which were previously occupied by pages involved in
						// this allocation.
						
						uint replaced = 0;
						uint nextUnusedFsp = 0;
						while (replaced < count) {
							bool foundUnusedFsp = false;
							
							// get the next unused free page in the stack
							
							while (true) {
								bool used = false;
								for (int x = 0; x < count; ++x) {
									if (stackLoc[x] != 0xFFFFFFFF && 
											stackLoc[x] == nextUnusedFsp) {
											
										used = true;
										break;
									}
								}
								
								if (!used)
									break;
								
								--nextUnusedFsp;
							}
							
							if (!foundUnusedFsp) {
								// TODO	
							} else {
								uint lowestIndex = 0;
								lowestUsedFsp = 0xFFFFFFFF;
								
								for (int x = 0; x < count; ++x) {
									if (stackLoc[x] != 0xFFFFFFFF) {
										if (stackLoc[x] < lowestUsedFsp) {
											lowestUsedFsp = stackLoc[x];
											lowestIndex = x;
										}
									}
								}
								
								fpStack[nextUnusedFsp] = fpStack[lowestUsedFsp];
								stackLoc[lowestIndex] = 0xFFFFFFFF;
								++replaced;
							}
						}
						
						fpStackPointer -= count;
					} else {
						// one or more free pages involved in this allocation are within the last 
						// (count) entries on the free page stack. We must remove the gaps made 
						// by our allocation.
						
						uint curGap = 1;
						uint *ptr = (fpStack + minUsedFsp + 1);
						uint curFsp = minUsedFsp + 1;
						
						while (curFsp < fpStackPointer) {
							bool isUsed = false;
							
							for (int x = 0; x < count; ++x) {
								if (curFsp == stackLoc[x]) {
									isUsed = true;
									break;
								}
							}
							
							if (isUsed) {
								++curGap;
							} else {
								*(ptr - curGap) = *ptr;
							}
							
							++ptr;
							++curFsp;
						}
					}*/
					
					return (void*)start;
				}
				
				next:
				
				if (fsp == 0)
					break;
					
				--fsp;
			}
			
			return null;
		}
		
		/// <summary>
		/// Deallocates the memory page pointed to by <paramref name="page" />.
		/// </summary>
		/// <param name="page">
		/// A pointer which is aligned along the platform's native page boundaries.
		/// </param>
		/// <remarks>
		/// Do not deallocate contiguous page ranges using this function, use
		/// Dealloc(void *pages, uint count) instead. If this
		/// function is used to deallocate a page range, only the first page
		/// will be deallocated and the rest of the pages in the range will
		/// remain allocated. While it is possible to properly deallocate a
		/// range using this function, Dealloc(void *page, uint count)
		/// does the work for you.
		/// </remarks>
		public static void Dealloc (void *page)
		{
			PushFreePage (page);
		}
		
		/// <summary>
		/// Deallocates a range of memory pages where <paramref name="pages" /> points
		/// to the first page of the range, and <paramref name="count" /> represents the
		/// amount of pages to deallocate.
		/// </summary>
		/// <param name="pages">
		/// A pointer which is aligned along the platform's native page boundaries.
		/// </param>
		/// <param name="count">
		/// The amount of pages to deallocate. This value must be equal to the 'count'
		/// parameter passed to RangeAlloc().
		/// </param>
		public static void Dealloc (void *pages, uint count)
		{
			byte *ptr = (byte*)pages;
			
			// TODO: error handling
			
			for (int x = 0; x < count; ++x) {
				PushFreePage(ptr);
				ptr += Pager.AtomicPageSize;
			}
		}
		
		#endregion 
		#region ReservePage () family
		
		/// <summary>
		/// Reserves a memory page so that it cannot be allocated using
		/// <see cref="M:Alloc()" /> or <see cref="M:RangeAlloc(uint count)" />.
		/// </summary>
		/// <param name="page">
		/// A pointer which is aligned along the platform's native page boundaries.
		/// </param>
		public static bool ReservePage(void *page)
		{
			//if (!IsPageFree(page, &fsp))
			//	return false;
			
			PushReservedPage(page);
			
			return true;
		}
		
		/// <summary>
		/// Reserves a range of memory pages so that they cannot be allocated using
		/// <see cref="M:Alloc()" /> or <see cref="M:RangeAlloc(uint count)" />.
		/// </summary>
		/// <param name="pageStart">
		/// A pointer which is aligned along the platform's native page boundaries.
		/// </param>
		/// <param name="pages">
		/// The amount of pages to reserve.
		/// </param>
		public static bool ReservePageRange(void *firstPage, uint pages, string name)
		{
			byte*	page = (byte*)firstPage;
			for (int i = 0; i < pages; i++)
			{
				//if (!IsPageReserved(page))	// ugh...
					PushReservedPage(page);
				page += Pager.AtomicPageSize;
			}
			return false;
		}
		
		#endregion
		#region Page Mapping
		
		/// <summary>
		/// Modifies the virtual memory mapping of the given super-page to point to
		/// the given physical super page. Both pointers must be aligned along the
		/// platform's native super-page boundaries.
		/// </summary>
		/// <remarks>
		/// This function is only available on platforms which can provide super-page
		/// mapping. The Intel 386 and later processors support this using the 'page
		/// directory', which maps large 4MB sections of memory at a time (via
		/// redirection to page tables which contain 4KB blocks).
		/// </remarks>
		/// <param name="superPage">
		/// A pointer aligned along the platform's native super-page boundaries which
		/// represents the virtual address to map.
		/// </param>
		/// <param name="phys_superPage">
		/// A pointer aligned along the platform's native super-page boundaries which
		/// represents the physical address to point to.
		/// </param>
		/// <param name="attr">
		/// The attributes to apply to the paging table entry for this super-page. If
		/// one or more flags given in this parameter are not supported by the current
		/// platform, the call will fail, returning false.
		/// </param>
		/// <returns>
		/// Returns true if the map operation completed successfully. Returns false
		/// if the platform does not support super-page mapping or one of the flags
		/// supplied in <paramref name="attr"/>, or an error occurred.
		/// </returns>
		public static PageAllocator.Errors MapPage (void *page, void *physPage, 
					    uint granularity, PageAttributes attr)
		{
			return Pager.MapPage (page, physPage, granularity, attr);
		}
		
		#endregion
		#region Get/SetAttributes() family
		
		public static Errors SetPageAttributes(void *page, uint granularity, PageAttributes attr)
		{
			return Pager.SetPageAttributes (page, granularity, attr);
		}
		
		public static PageAttributes GetPageAttributes(void *page, uint granularity,
							       PageAllocator.Errors *ret_err)
		{
			return (PageAttributes)Pager.GetPageAttributes (page, granularity, ret_err);
		}
		
		#endregion
		#region Implementation Details
		
		/// <summary>
		/// Gets the page address of a given pointer.
		/// </summary>
		private static void *PtrToPage(void *ptr)
		{
			uint up = (uint)ptr;
			uint pageSize = Pager.AtomicPageSize - 1;
			
			return (void*) ( up - (up & pageSize ) );
		}
		
		private static void PushFreePage(void *page)
		{
			fpStack[fpStackPointer++] = (uint)page;
		}
		
		private static void *PopFreePage()
		{
			return (void*)fpStack[fpStackPointer--];
		}
		
		private static void PushReservedPage(void *page)
		{
			rpStack[rpStackPointer++] = (uint)page;
		}
		
		#endregion
	}
}
