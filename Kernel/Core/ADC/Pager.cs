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
	public unsafe struct PagingMemoryRequirements {
		public PagingMemoryRequirements (uint atomicPages, void *start)
		{
			AtomicPages = atomicPages;
			Start = start;
			Error = PageAllocator.Errors.Success;
		}
		
		public uint AtomicPages;
		public void *Start;
		public PageAllocator.Errors Error;
	}
	
	/// <summary>
	/// Provides the architecture-specific implementation of
	/// memory paging.
	/// </summary>
	public unsafe class Pager {
	
		#region Convenience

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
					TextMode.WriteNumber ((int)err, false);
					Kernel.Assert (false, _("Failed to get pager granularity!"));
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
					_("Specified invalid granularity level"));

			PageAllocator.Errors err = PageAllocator.Errors.Success;
			uint size = GetGranularitySize (granularity, &err);
			ulong ptri = (ulong)ptr;
			Kernel.AssertSuccess ((uint)err, _("Failed to get granularity size"));
			
			return (void*) (ptri - (ptri & (size-1)));
		}


		public static uint AtomicPageSize {
			get {
				PageAllocator.Errors err = PageAllocator.Errors.Success;
				
				uint s = GetGranularitySize (0, &err);

				Kernel.Assert (err == PageAllocator.Errors.Success,
					_("ADC.Pager::get_AtomicPageSize (): layer does not support the atomic paging granularity (0)"));

				return s;
			}
		}
		
		#endregion
		#region AOT Stubs
		
		[AOTAttr.String]
		private static byte *_ (string s)
		{
			return null;
		}

		#endregion
		#region ADC Stubs
		
		/// <summary>
		/// Returns the native size of a page at the <paramref name="granularity" /> 
		/// granularity where 0 is the `atomic' granularity used by the memory
		/// manager to represent the smallest allocatable block.
		/// </summary>
		[AOTAttr.ADCStub]
		public static uint GetGranularitySize (uint granularity, PageAllocator.Errors *ret_err)
		{
			*ret_err = PageAllocator.Errors.NotImplemented;
			return 0;
		}
		
		/// <summary>
		/// Gets the largest page granularity provided by the ADC layer.
		/// </summary>
		[AOTAttr.ADCStub]
		public static uint GetBigGranularity ()
		{
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
		}
		
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
		public static PageAllocator.Errors Init (uint totalMem, byte *pagemap, uint pagemapLen)
		{
			return PageAllocator.Errors.NotImplemented;
		}
		
		/// <summary>
		/// Causes paging to be enabled and active after this call.
		/// </summary>
		[AOTAttr.ADCStub]
		public static PageAllocator.Errors Enable ()
		{
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
			return PageAllocator.Errors.NotImplemented;
		}
		
		[AOTAttr.ADCStub]
		public static PageAllocator.Errors SetPageAttributes (void *page, uint granularity,
						       PageAttributes attr)
		{
			return PageAllocator.Errors.NotImplemented;
		}
		
		[AOTAttr.ADCStub]
		public static PageAttributes GetPageAttributes (void *page, uint granularity,
								     PageAllocator.Errors *ret_err)
		{
			*ret_err = PageAllocator.Errors.NotImplemented;
			return 0;
		}

		#endregion
	}
}
