// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using SharpOS;
using SharpOS.AOT;
using SharpOS.Memory;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.ADC {

	/// <summary>
	/// Provides the architecture-specific implementation of
	/// memory paging.
	/// </summary>
	public unsafe class Pager {

		#region ADC Interface
		
		/// <summary>
		/// Initializes paging, placing paging control data at <paramref name="pagemap" />.
		/// </summary>
		/// <returns>
		/// 0 on success or PageAllocator.Errors.UnusablePageControlBuffer if
		/// <paramref name="pagemap" /> or <paramref name="pagemapLen" /> are outside
		/// of the constraints provided to the memory manager by
		/// <see cref="M:GetMemoryRequirements" />.
		/// </returns>
		[AOTAttr.ADCStub]
		public static PageAllocator.Errors Setup (uint totalMem, byte *pagemap, uint pagemapLen, PageAllocator.Errors* error)
		{
			Kernel.Warning("Pager.Setup - not implemented!");
			return PageAllocator.Errors.NotImplemented;
		}
		
		/// <summary>
		/// Returns the native size of a page at the <paramref name="granularity" /> 
		/// granularity where 0 is the `atomic' granularity used by the memory
		/// manager to represent the smallest allocatable block.
		/// </summary>
		[AOTAttr.ADCStub]
		public static uint GetGranularitySize (uint granularity, PageAllocator.Errors *ret_err)
		{
			Kernel.Warning("Pager.GetGranularitySize - not implemented!");
			*ret_err = PageAllocator.Errors.NotImplemented;
			return 0;
		}
		
		/// <summary>
		/// Gets the largest page granularity provided by the ADC layer.
		/// </summary>
		[AOTAttr.ADCStub]
		public static uint GetBigGranularity ()
		{
			Kernel.Warning("Pager.GetBigGranularity - not implemented!");
			return 0;
		}
		
		/// <summary>
		/// Used by the memory manager to determine what memory must be 
		/// reserved for paging control data.
		/// </summary>
		/// <returns>
		/// A <see cref="PagingMemoryRequirements" /> structure containing 
		/// the amount of atomic memory pages required and an optional 
		/// pointer specifying the buffer that must be used for paging
		/// control data (Start). If the processor supports dynamic placement of
		/// paging structures, Start must be a null pointer.
		/// </returns>
		[AOTAttr.ADCStub]
		public static void GetMemoryRequirements (uint totalMem, PagingMemoryRequirements *req)
		{
			Kernel.Warning("Pager.GetMemoryRequirements - not implemented!");
		}
		
		/// <summary>
		/// Causes paging to be enabled and active after this call.
		/// </summary>
		[AOTAttr.ADCStub]
		public static PageAllocator.Errors Enable (PageAllocator.Errors* error)
		{
			Kernel.Warning("Pager.Enable - not implemented!");
			return PageAllocator.Errors.NotImplemented;
		}
		
		/// <summary>
		/// Changes the mapping of the virtual page <paramref name="page" /> to the
		/// physical page <paramref name="phys_page" /> at the specified 
		/// <paramref name="granularity" />. 
		/// </summary>
		[AOTAttr.ADCStub]
		public static PageAllocator.Errors MapPage (void *page, void *phys_page, uint granularity,
					     PageAttributes attr)
		{
			Kernel.Warning("Pager.MapPage - not implemented!");
			return PageAllocator.Errors.NotImplemented;
		}
		
		[AOTAttr.ADCStub]
		public static PageAllocator.Errors SetPageAttributes (void *page, uint granularity,
						       PageAttributes attr)
		{
			Kernel.Warning("Pager.SetPageAttributes - not implemented!");
			return PageAllocator.Errors.NotImplemented;
		}
		
		[AOTAttr.ADCStub]
		public static PageAttributes GetPageAttributes (void *page, uint granularity,
								     PageAllocator.Errors *ret_err)
		{
			*ret_err = PageAllocator.Errors.NotImplemented;
			Kernel.Warning("Pager.GetPageAttributes - not implemented!");
			return 0;
		}

		#endregion
		#region AOT Stubs
		
		[AOTAttr.String]
		static byte *_ (string s)
		{
			return null;
		}

		#endregion
		#region Common

		/// <summary>
		/// Returns the paging granularity of the pointer <paramref name="page" />.
		/// </summary>
		/// <returns>
		/// The granularity level or -1 if the pointer is not page-aligned.
		/// </returns>
		public static uint GetPointerGranularity (void *page)
		{
			ulong pagei = (ulong) page;
			uint levels = GetBigGranularity () + 1;
			
			for (uint x = 0; x < levels; ++x) {
				PageAllocator.Errors err = PageAllocator.Errors.Success;
				
				uint size = GetGranularitySize (x, &err);

				if (err != PageAllocator.Errors.Success) {
					TextMode.Write ("Error: ");
					TextMode.Write ((int)err);
					Kernel.Assert (false, "Failed to get pager granularity!");
				}
				
				if ((pagei & (ulong)(size - 1)) == 0)
					return x;
			}
			
			return 0xFFFFFFFF;
		}

		/// <summary>
		/// Aligns the pointer <paramref name="ptr" /> to the page granularity
		/// specified by <paramref name="granularity" />. This method asserts 
		/// that the granularity is within the allowed range for the ADC layer
		/// in use.
		/// <returns>
		/// A pointer which is aligned to the page boundaries of the given
		/// granularity.
		/// </returns>
		/// </summary>
		public static void *PageAlign (void *ptr, uint granularity)
		{
			Kernel.Assert (granularity >= 0 && granularity <= GetBigGranularity (),
				"Specified invalid granularity level");

			PageAllocator.Errors err = PageAllocator.Errors.Success;
			uint size = GetGranularitySize (granularity, &err);
			ulong ptri = (ulong)ptr;
			Kernel.AssertZero ((uint)err, "Failed to get granularity size");
			
			return (void*) (ptri - (ptri & (size-1)));
		}

		public static uint AtomicPageSize {
			get {
				PageAllocator.Errors err = PageAllocator.Errors.Success;
				
				uint s = GetGranularitySize (0, &err);

				Kernel.Assert (err == PageAllocator.Errors.Success,
					"ADC.Pager::get_AtomicPageSize (): layer does not support the atomic paging granularity (0)");

				return s;
			}
		}
		
		#endregion
	}
}
