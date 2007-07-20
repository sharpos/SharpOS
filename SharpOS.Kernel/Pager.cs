/*
 * SharpOS.Memory/Pager.cs
 * N:SharpOS.Memory
 *
 * (C) 2007 William Lahti. This software is licensed under the terms of the GNU General
 * Public License.
 *
 * TODO
 *  - set segment base (working with AOT) to allow kernel to be mapped in upper half.
 *
 */

using SharpOS.AOT;
using SharpOS.ADC;

namespace SharpOS.Memory {
	[System.Flags]
	public enum PageAttributes: uint {
		None = 0,
		
		ReadWrite	= 1,
		User		= 1<<1,
		Present		= 1<<2,
	}
	
	/**
		<summary>
			The PageAllocator class handles physical memory page allocation and provides
			the OS an interface for memory paging/mapping. The PageAllocator class is 
			portable, making use of the SharpOS.ADC.Pager class to implement platform-
			specific paging mechanisms.
		</summary>
	*/
	public unsafe class PageAllocator {
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


		// Global State
		
		private static byte *KernelStartPage;	// physical pointer to the first page occupied by the kernel
		private static uint KernelSize;		// kernel image size (pages)
		private static uint TotalPages;		// amount of pages with RAM
		
		private static uint *FPStack;		// stack containing free page addresses
		private static uint FPStackSize;	// size of the free page stack (KB)
		private static uint FPStackPointer;
		
		private static uint *RPStack;		// stack containing reserved page addresses
		private static uint RPStackSize;	// size of the reserved page stack (KB)
		private static uint RPStackPointer;
		
		private static byte *PagingData;	// data used for paging
		private static uint PagingDataSize;	// size of PagingData
		
		#region Initialization
		
		/**
			<summary>
				Initializes page management and paging. Using <see cref="Alloc" /> 
				and related management functions before calling this function results
				in a kernel panic. After this function is called, the 
				<see cref="ReservePage"/> and <see cref="ReservePageRange" /> 
				functions can be used to reserve memory that should not be allocated.
				You should ensure that no memory allocations have happened between
				calling this function and reserving memory.
			</summary>
		*/
		public unsafe static void Setup (byte *kernelOffset, uint kernelSize, uint totalMem)
		{
			PagingMemoryRequirements *pagingMemory = stackalloc PagingMemoryRequirements [1];
			byte *start = null;
			
			// Determine the pages used by the kernel and the total pages in the system
			KernelStartPage = (byte*)PtrToPage(kernelOffset);
			KernelSize = kernelSize * 1024 / Pager.AtomicPageSize + 1;
			TotalPages = totalMem / (Pager.AtomicPageSize / 1024);
			
			start = KernelStartPage + KernelSize * Pager.AtomicPageSize;
			
			// Allocate the free page stack
			FPStack = (uint*)start;
			FPStackPointer = 0;
			FPStackSize = TotalPages * 4 / Pager.AtomicPageSize;
			
			start += FPStackSize * Pager.AtomicPageSize;
			
			// Allocate the reserved page stack
			RPStack = (uint*)start;
			RPStackPointer = 0;
			RPStackSize = 1;
			
			// Allocate paging information
			pagingMemory->Start = start;
			Pager.GetMemoryRequirements (totalMem, pagingMemory);
			PagingData = (byte*)pagingMemory->Start;
			PagingDataSize = pagingMemory->AtomicPages;
			
			// Reserve the memory ranges we're using.
			ReservePageRange (KernelStartPage, KernelSize);
			ReservePageRange (FPStack, FPStackSize);
			ReservePageRange (RPStack, RPStackSize);
			ReservePageRange (PagingData, PagingDataSize);
			
			Pager.Init (totalMem, PagingData, PagingDataSize);
			Pager.Enable ();
		
			for (int page = 0; page < TotalPages; ++page)
				PushFreePage ((void*)(page * Pager.AtomicPageSize));
		}
		
		#endregion
		#region IsPageFree () and IsPageReserved ()
		
		/**
			<summary>
				Checks if a given page is allocated. If the page is
				allocated, the method returns true. This function 
				starts from the current stack pointer and goes 
				backward.
			</summary>
		*/
		public static bool IsPageFree (void *page)
		{
			uint fsp = FPStackPointer;
			
			while (true) {
				if (FPStack[fsp] == (uint)page)
					return false;
			
				if (fsp == 0)
					break;
					
				--fsp;
			}
			
			return true;
		}
		
		/**
			<summary>
				Returns true if the given page is reserved (that is, not available for allocation).
			</summary>
		*/
		public static bool IsPageReserved (void *page)
		{
			uint sp = 0;
			uint *ptr = RPStack;
			
			while (sp < RPStackPointer) {
				if (*ptr == (uint)page)
					return true;
				++sp;
				++ptr;
			}
			
			return false;
		}
		
		#endregion
		#region Alloc / Dealloc
		
		/**
			<summary>
				Allocates a page of memory. This function returns null when out of memory.
			</summary>
		*/
		public static void *Alloc ()
		{
			void *page = null;
			
			if (FPStackPointer == 0)
				return null;
			
			do {
				page = PopFreePage ();
			} while ((uint)page != 0x1U || IsPageReserved (page));
			
			return page;
		}
		
		/**
			<summary>
				Allocates a contiguous range of pages. This function returns null on failure.
			</summary>
		*/
		public static void *RangeAlloc (uint count)
		{
			uint fsp = FPStackPointer;
			
			// iterate through free pages
			
			while (true) {
				uint fsp2 = FPStackPointer;
				uint *stackLoc = stackalloc uint[(int)count];
				uint found = 0;
				uint start = FPStack[fsp];
				
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
						if (FPStack[fsp2] != 0x1 && 
								FPStack[fsp2] == (uint)((byte*)start + x * Pager.AtomicPageSize)) {
								
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
						FPStack[stackLoc[x]] = 0x1U;
					
					/*
					// use stackLoc to remove the gaps in the free page stack created by this allocation.
					
					if (FPStackPointer - maxUsedFsp >= count) {
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
								
								FPStack[nextUnusedFsp] = FPStack[lowestUsedFsp];
								stackLoc[lowestIndex] = 0xFFFFFFFF;
								++replaced;
							}
						}
						
						FPStackPointer -= count;
					} else {
						// one or more free pages involved in this allocation are within the last 
						// (count) entries on the free page stack. We must remove the gaps made 
						// by our allocation.
						
						uint curGap = 1;
						uint *ptr = (FPStack + minUsedFsp + 1);
						uint curFsp = minUsedFsp + 1;
						
						while (curFsp < FPStackPointer) {
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
		
		/**
			<summary>
				Deallocates the memory page pointed to by <paramref name="page" />.
			</summary>
			<param name="page">
				A pointer which is aligned along the platform's native page boundaries.
			</param>
			<remarks>
				Do not deallocate contiguous page ranges using this function, use
				Dealloc(void *pages, uint count) instead. If this
				function is used to deallocate a page range, only the first page 
				will be deallocated and the rest of the pages in the range will
				remain allocated. While it is possible to properly deallocate a 
				range using this function, Dealloc(void *page, uint count)
				does the work for you.
			</remarks>
		*/
		public static void Dealloc (void *page)
		{
			PushFreePage (page);
		}
		
		/**
			<summary>
				Deallocates a range of memory pages where <paramref name="pages" /> points
				to the first page of the range, and <paramref name="count" /> represents the
				amount of pages to deallocate.
			</summary>
			<param name="pages">
				A pointer which is aligned along the platform's native page boundaries.
			</param>
			<param name="count">
				The amount of pages to deallocate. This value must be equal to the 'count'
				parameter passed to RangeAlloc().
			</param>
		*/
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
		
		/**
			<summary>
				Reserves a memory page so that it cannot be allocated using 
				<see cref="M:Alloc()" /> or <see cref="M:RangeAlloc(uint count)" />.
			</summary>
			<param name="page">
				A pointer which is aligned along the platform's native page boundaries.
			</param>
		*/
		public static bool ReservePage(void *page)
		{
			//if (!IsPageFree(page, &fsp))
			//	return false;
			
			PushReservedPage(page);
			
			return true;
		}
		
		/**
			<summary>
				Reserves a range of memory pages so that they cannot be allocated using
				<see cref="M:Alloc()" /> or <see cref="M:RangeAlloc(uint count)" />.
			</summary>
			<param name="pageStart">
				A pointer which is aligned along the platform's native page boundaries.
			</param>
			<param name="pages">
				The amount of pages to reserve.
			</param>
		*/
		public static bool ReservePageRange(void *firstPage, uint pages)
		{
			// TODO
			return false;
		}
		
		#endregion
		#region Page Mapping
		
		/**
			<summary>
				Modifies the virtual memory mapping of the given super-page to point to 
				the given physical super page. Both pointers must be aligned along the 
				platform's native super-page boundaries.
			</summary>
			<remarks>
				This function is only available on platforms which can provide super-page
				mapping. The Intel 386 and later processors support this using the 'page 
				directory', which maps large 4MB sections of memory at a time (via 
				redirection to page tables which contain 4KB blocks).
			</remarks>
			<param name="superPage">
				A pointer aligned along the platform's native super-page boundaries which 
				represents the virtual address to map.
			</param>
			<param name="phys_superPage">
				A pointer aligned along the platform's native super-page boundaries which
				represents the physical address to point to.
			</param>
			<param name="attr">
				The attributes to apply to the paging table entry for this super-page. If 
				one or more flags given in this parameter are not supported by the current
				platform, the call will fail, returning false.
			</param>
			<returns>
				Returns true if the map operation completed successfully. Returns false 
				if the platform does not support super-page mapping or one of the flags 
				supplied in <paramref name="attr"/>, or an error occurred.
			</returns>
		*/
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
		
		/**
			<summary>
				Gets the page address of a given pointer.
			</summary>
		*/
		private static void *PtrToPage(void *ptr)
		{
			uint up = (uint)ptr;
			uint pageSize = Pager.AtomicPageSize - 1;
			
			return (void*) ( up - (up & pageSize ) );
		}
		
		private static void PushFreePage(void *page)
		{
			FPStack[FPStackPointer++] = (uint)page;
		}
		
		private static void *PopFreePage()
		{
			return (void*)FPStack[FPStackPointer--];
		}
		
		private static void PushReservedPage(void *page)
		{
			RPStack[RPStackPointer++] = (uint)page;
		}
		
		#endregion
	}
}