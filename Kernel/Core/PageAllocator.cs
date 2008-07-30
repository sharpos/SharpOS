// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//	Ásgeir Halldórsson <asgeir.halldorsson@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT;
using SharpOS.Kernel.ADC;
using System.Runtime.InteropServices;

namespace SharpOS.Kernel.Memory
{

	/// <summary>
	/// The PageAllocator class handles physical memory page allocation and provides
	/// the OS an interface for memory paging/mapping. The PageAllocator class is
	/// portable, making use of the SharpOS.ADC.Pager class to implement platform-
	/// specific paging mechanisms.
	/// </summary>
	public unsafe class PageAllocator
	{
		#region Global state

		static byte* kernelStartPage;	// physical pointer to the first page occupied by the kernel
		static uint kernelSize;		// kernel image size (pages)
		static uint totalPages;		// amount of pages with RAM
		static uint totalMem;	// total amount of memory in bytes

		static uint currentPage;
		static uint* fpStack;		// stack containing free page addresses
		static uint fpStackSize;	// size of the free page stack in pages
		static uint fpStackPointer;

		static ReservedPages* rpStack;		// stack containing reserved page addresses
		static uint rpStackSize;	// size of the reserved page stack in pages
		static uint rpStackPointer;

		static byte* pagingData;	// data used for paging
		static uint pagingDataSize;	// size of pagingData

		#endregion
		#region Nested types

		/// <summary>
		/// Reserverd pages is used to reserve pages for system applications
		/// like kernel, stack and memory mananger
		/// </summary>
		[StructLayout (LayoutKind.Sequential)]
		private struct ReservedPages
		{
			/// <summary>
			/// Address of the reserved block
			/// </summary>
			public uint Address;
			/// <summary>
			/// Number of pages that are reserved
			/// </summary>
			public uint Size;
		}

		public enum Errors : uint
		{
			Success = 0,
			Unknown = 1,
			NotImplemented = 2,

			/// <summary>
			/// The given granularity is not available on this platform, or
			/// cannot be specified for the current operation.
			/// </summary>
			UnsupportedGranularity = 3,

			/// <summary>
			/// Attributes are not supported for pages of the given granularity.
			/// </summary>
			NoAttributesForGranularity = 4,

			/// <summary>
			/// The buffer provided by the kernel is insufficiently sized or
			/// placed to be used as the mapping control data buffer.
			/// </summary>
			UnusablePageControlBuffer = 5,
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
		public static void Setup (byte* kernelOffset, uint _kernelSize, uint totalKbMem)
		{
			PagingMemoryRequirements* pagingMemory = stackalloc PagingMemoryRequirements[1];

			// The total memory in bytes
			totalMem = totalKbMem * 1024;
			// Total pages 
			totalPages = totalMem / Pager.AtomicPageSize;

			// First page of the kernel
			kernelStartPage = (byte*)PtrToPage (kernelOffset);
			// Kernel size in pages
			kernelSize = (_kernelSize / Pager.AtomicPageSize) + 1;

			// Allocate the free page stack immediately after end of the kernel
			fpStack = (uint*)(kernelStartPage + (kernelSize * Pager.AtomicPageSize));
			// Initalize the stack pointer 
			fpStackPointer = 0;
			// Free page stack size in pages
			fpStackSize = (totalPages * (uint)sizeof (uint*) / Pager.AtomicPageSize) + 1;

			// Allocate the reserved page stack immediately after free page stack
			rpStack = (ReservedPages*)((uint)fpStack + (fpStackSize * Pager.AtomicPageSize));
			// Initalize the reserve stack pointer
			rpStackPointer = 0;
			// Reserve stack size in pages
			rpStackSize = 1;	// fixed - should be enough

			// Allocate paging information
			pagingData = (byte*)(rpStack + (rpStackSize * Pager.AtomicPageSize));
			pagingMemory->Start = (void*)pagingData;

			// Reserve 4 mega bytes of memory for Virtual memory manager
			// FIXME: 
			pagingDataSize = (4 * 1024 * 1024) / 4096;

			// Reserve the memory ranges we're using.
			ReservePageRange (kernelStartPage, kernelSize, "kernel");
			ReservePageRange (fpStack, fpStackSize, "fpstack");
			ReservePageRange (rpStack, rpStackSize, "rpstack");
			ReservePageRange (pagingData, pagingDataSize, "paging");

			// Reserve memory below 0x100000 (1MB) for the BIOS/video memory
			ReservePageRange ((void*)0, (0x100000 / ADC.Pager.AtomicPageSize), "Reserve memory below 0x100000 (1MB) for the BIOS/video memory");

			// FIXME: the value we get back from Pager.Setup is not the same value that 
			//		  we 'return' from inside the method itself!!!!
			Errors error = Errors.Unknown;
			Pager.Setup (totalKbMem, pagingData, pagingDataSize, &error);

			if (error != Errors.Success) {
				PrintError (error);
				return;
			}

			// NOTE: 0x0000 page is reserved
			currentPage = 1;

			error = Errors.Unknown;
			Pager.Enable (&error);
			if (error != Errors.Success)
				PrintError (error);
		}

		private static void PrintError (Errors error)
		{
			ADC.TextMode.Write ("(");
			ADC.TextMode.Write ((int)error);
			ADC.TextMode.Write (") ");

			switch (error) {
				case Errors.NoAttributesForGranularity:
					Diagnostics.Error ("NoAttributesForGranularity");
					return;
				case Errors.NotImplemented:
					Diagnostics.Error ("NotImplemented");
					return;
				case Errors.Unknown:
					Diagnostics.Error ("Unknown");
					return;
				case Errors.UnsupportedGranularity:
					Diagnostics.Error ("UnsupportedGranularity");
					return;
				case Errors.UnusablePageControlBuffer:
					Diagnostics.Error ("UnusablePageControlBuffer");
					return;
				case Errors.Success:
					Diagnostics.Error ("Success");
					return;
				default:
					Diagnostics.Error ("Garbage");
					return;
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
		public static bool IsPageFree (void* page)
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
		public static bool IsPageReserved (void* page)
		{
			uint sp = 0;
			ReservedPages* ptr = rpStack;
			uint pageAddr = (uint)page;

			while (sp < rpStackPointer) {
				if (pageAddr >= ptr[sp].Address && pageAddr < (ptr[sp].Address + (ptr[sp].Size * Pager.AtomicPageSize)))
					return true;

				sp++;
			}

			return false;
		}

		#endregion
		#region Alloc / Dealloc

		/// <summary>
		/// Allocates a page of memory. This function returns null when out of memory.
		/// </summary>
		public static void* Alloc ()
		{
			return PopFreePage ();
		}

		/// <summary>
		/// Deallocates the memory page pointed to by <paramref name="page" />.
		/// </summary>
		/// <param name="page">
		/// A pointer which is aligned along the platform's native page boundaries.
		/// </param>
		/// <remarks>
		/// </remarks>
		public static void Dealloc (void* page)
		{
			PushFreePage (page);
		}

		#endregion
		#region ReservePage () family
		private static ReservedPages* GetReservedPage ()
		{
			return &rpStack[rpStackPointer++];
		}

		/// <summary>
		/// Reserves a memory page so that it cannot be allocated using
		/// <see cref="M:Alloc()" /> or <see cref="M:RangeAlloc(uint count)" />.
		/// </summary>
		/// <param name="page">
		/// A pointer which is aligned along the platform's native page boundaries.
		/// </param>
		public static bool ReservePage (void* page)
		{
			if (page == null)
				return false;

			// we should be doing this.. but it's so slow..
			if (IsPageReserved (page))	// ugh...
				return true;

			//if (!IsPageFree(page, &fsp))
			//	return false;

			ReservedPages* pages = GetReservedPage ();
			pages->Address = (uint)page;
			pages->Size = 1;

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
		public static bool ReservePageRange (void* firstPage, uint pages, string name)
		{
			ReservedPages* reservePages = GetReservedPage ();
			reservePages->Address = (uint)firstPage;
			reservePages->Size = pages;

			return true;
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
		public static PageAllocator.Errors MapPage (void* page, void* physPage,
						uint granularity, PageAttributes attr)
		{
			return Pager.MapPage (page, physPage, granularity, attr);
		}

		#endregion
		#region Get/SetAttributes() family

		public static Errors SetPageAttributes (void* page, uint granularity, PageAttributes attr)
		{
			return Pager.SetPageAttributes (page, granularity, attr);
		}

		public static PageAttributes GetPageAttributes (void* page, uint granularity,
								   PageAllocator.Errors* ret_err)
		{
			return Pager.GetPageAttributes (page, granularity, ret_err);
		}

		#endregion
		#region Implementation Details

		/// <summary>
		/// Gets the page address of a given pointer.
		/// </summary>
		/// <param name="ptr">The pointer</param>
		/// <returns></returns>
		private static void* PtrToPage (void* ptr)
		{
			uint up = (uint)ptr;
			uint pageSize = Pager.AtomicPageSize - 1;

			return (void*)(up - (up & pageSize));
		}

		/// <summary>
		/// Pushes the free page to the free page stack
		/// </summary>
		/// <param name="page">The page.</param>
		private static void PushFreePage (void* page)
		{
			fpStack[fpStackPointer++] = (uint)page;
		}

		/// <summary>
		/// Pops the free page from the free page stack
		/// </summary>
		/// <returns>Pointer to free page.</returns>
		private static void* PopFreePage ()
		{
			if (fpStackPointer == 0 && currentPage < (totalPages - 1)) {
				while (currentPage < (totalPages - 1)) {
					uint* page = (uint*)(currentPage * Pager.AtomicPageSize);
					currentPage++;

					if (IsPageReserved (page)) {
						continue;
					}

					return (void*)page;
				}
			}
			else if (fpStackPointer == 0)
				return null;

			return (void*)fpStack[--fpStackPointer];
		}

		#endregion
		#region Debug

		public static void DumpStack (uint* stack, uint stackptr, int count)
		{
			for (int i = (int)stackptr - 1; i >= 0 && i >= stackptr - count; i--) {
				ADC.TextMode.Write (i);
				ADC.TextMode.Write (":");
				ADC.TextMode.Write ((int)stack[i]);
				ADC.TextMode.WriteLine ();
			}
		}

		private static void DumpReservedStack (ReservedPages* pageStack, uint stackPtr, int count)
		{
			for (int i = (int)stackPtr - 1; i >= 0 && i >= stackPtr - count; i--) {
				ADC.TextMode.Write (i);
				ADC.TextMode.Write (": Address: ");
				ADC.TextMode.Write ((int)pageStack->Address);
				ADC.TextMode.Write (", Size: ");
				ADC.TextMode.Write ((int)pageStack->Size);
				pageStack++;
				ADC.TextMode.WriteLine ();
			}
		}

		public static void Dump (int count)
		{
			ADC.TextMode.WriteLine ("Free");
			DumpStack (fpStack, fpStackPointer, count);
			ADC.TextMode.WriteLine ("Reserved");
			DumpReservedStack (rpStack, rpStackPointer, count);
		}

		public static void DumpInfo ()
		{
			ADC.TextMode.Write ("Total Memory: 0x");
			ADC.TextMode.WriteLine (((uint)totalMem).ToString (""));
			ADC.TextMode.Write ("Kernel Start Page: 0x");
			ADC.TextMode.WriteLine (((uint)kernelStartPage).ToString (""));
			ADC.TextMode.Write ("Size in Pages: 0x");
			ADC.TextMode.WriteLine (((uint)kernelSize).ToString (""));
			ADC.TextMode.Write ("Total RAM in Pages: 0x");
			ADC.TextMode.WriteLine (((uint)totalPages).ToString (""));
			ADC.TextMode.Write ("Free Page Stack Start: 0x");
			ADC.TextMode.WriteLine (((uint)fpStack).ToString (""));
			ADC.TextMode.Write ("Size of Free Page Stack: 0x");
			ADC.TextMode.WriteLine (((uint)fpStackSize).ToString (""));
			ADC.TextMode.Write ("Start of Reserve Page Stack: 0x");
			ADC.TextMode.WriteLine (((uint)rpStack).ToString (""));
			ADC.TextMode.Write ("Size of Reserve Page Stack: 0x");
			ADC.TextMode.WriteLine (((uint)rpStackSize).ToString (""));
		}

		#endregion
	}
}
