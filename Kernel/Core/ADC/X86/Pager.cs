// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	Ásgeir Halldórsson <asgeir.halldorsson@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.Kernel.Memory;
using ADC = SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.ADC.X86
{
	/// <summary>
	/// Hardware specific paging layer
	/// </summary>
	/// <todo>
	/// - Add support for PAE (Physical Address Extensions) so we can have 64 gb of virtual memory
	/// - Add support for the paging exceptions that can occur
	/// - Initialisation should be done in 'Architecture'
	/// </todo>
	public unsafe class Pager
	{

		#region Global State

		private static uint* PageDirectory;	// page directory
		private static uint* PageTables;	// page tables

		#endregion
		#region Enumerations

		[System.Flags]
		private enum PageAttr : uint
		{
			Present = (1 << 0),
			ReadWrite = (1 << 1),
			User = (1 << 2),
			Accessed = (1 << 5),
			Dirty = (1 << 6),

			A1 = (1 << 9),
			A2 = (1 << 10),
			A3 = (1 << 11),

			FrameMask = 0xFFFFF000,
			AttributeMask = 0x00000FFF
		}

		#endregion
		#region Implementation Details

		private static PageAttr GetNativePMA (PageAttributes attr)
		{
			if ((uint)attr == 0xFFFFFFFF)
				return (PageAttr)attr;

			PageAttr attrMask = 0;

			if ((attr & PageAttributes.ReadWrite) != 0)
				attrMask |= PageAttr.ReadWrite;

			if ((attr & PageAttributes.User) != 0)
				attrMask |= PageAttr.User;

			if ((attr & PageAttributes.Present) != 0)
				attrMask |= PageAttr.Present;

			return attrMask;
		}

		private static PageAttributes GetAbstractPMA (PageAttr attr)
		{
			if ((uint)attr == 0xFFFFFFFF)
				return (PageAttributes)attr;

			PageAttributes ret = PageAttributes.None;

			if ((attr & PageAttr.ReadWrite) != 0)
				ret |= PageAttributes.ReadWrite;

			if ((attr & PageAttr.User) != 0)
				ret |= PageAttributes.User;

			if ((attr & PageAttr.Present) != 0)
				ret |= PageAttributes.Present;

			return ret;
		}

		/*
		private static uint ReadCR0()
		{
			uint val = 0;
			
			Asm.PUSH (R32.EAX);
			Asm.MOV (R32.EAX, CR.CR0);
			Asm.MOV (&val, R32.EAX);
			Asm.POP (R32.EAX);
			
			return val;
		}
		
		private static void *ReadCR3()
		{
			uint val = 0;
			
			Asm.PUSH (R32.EAX);
			Asm.MOV (R32.EAX, CR.CR3);
			Asm.MOV (&val, R32.EAX);
			Asm.POP (R32.EAX);
			
			return (void*)val;
		}
		
		private unsafe static void WriteCR0 (uint value)
		{
			Asm.PUSH (R32.EAX);
			Asm.MOV (R32.EAX, &value);
			Asm.MOV (CR.CR0, R32.EAX);
			Asm.POP (R32.EAX);
		}
		
		private static void WriteCR3 (uint ptr)
		{
			Asm.PUSH (R32.EAX);
			Asm.MOV (R32.EAX, &ptr);
			Asm.MOV (CR.CR3, R32.EAX);
			Asm.POP (R32.EAX);
		}
		*/

		private static void SetDirectory(uint page)
		{
			Asm.CLI();

			Asm.PUSH(R32.EAX);
			Asm.PUSH(R32.ECX);

			Asm.MOV(R32.EAX, &page);

			Asm.MOV(R32.ECX, CR.CR0);
			Asm.OR(R32.ECX, (uint)CR0Flags.PG);

			Asm.MOV(CR.CR3, R32.EAX);
			Asm.MOV(CR.CR0, R32.ECX);

			Asm.POP(R32.ECX);
			Asm.POP(R32.EAX);

			Asm.STI();
		}

		private static void PagePtrToTables(void* page, uint* ret_pde, uint* ret_pte)
		{
			*ret_pde = (uint)page / 4194304;
			*ret_pte = ((uint)page / 4096) - (*ret_pde * 1024);
		}

		private static uint ComputeControlReq(uint totalMem)
		{
			PageAllocator.Errors err;

			return (totalMem / (GetGranularitySize(0, &err) / 1024) / 1024 + 1);
		}

		#endregion
		#region ADC Stub Implementations

		public static uint GetGranularitySize(uint granularity, PageAllocator.Errors* ret_err)
		{
			if (granularity < 0 || granularity > 1) {
				*ret_err = PageAllocator.Errors.UnsupportedGranularity;
				return 0;
			}

			*ret_err = PageAllocator.Errors.Success;

			switch (granularity) {
				case 0:
					return 4096;
				case 1:
					return 131072;
				default:
					*ret_err = PageAllocator.Errors.UnsupportedGranularity;
					return 0xFFFFFFFF;
			}
		}

		public static uint GetBigGranularity()
		{
			return 1;
		}

		public static void GetMemoryRequirements(uint totalMem, PagingMemoryRequirements* req)
		{
			req->AtomicPages = ComputeControlReq(totalMem);
			// this can't be right??
			//req->Start = null;	
			req->Error = PageAllocator.Errors.Success;
		}

		public static PageAllocator.Errors Setup(uint totalMem, byte* pagemap, uint pagemapLen, PageAllocator.Errors* error)
		{
			if (pagemap == null ||
				pagemapLen < ComputeControlReq(totalMem)) {
				*error = PageAllocator.Errors.UnusablePageControlBuffer;
				return *error;
			}

			uint totalBytes = totalMem * 1024;	// more intuitive to think in bytes than in kibibytes

			PageDirectory = (uint*)pagemap;
			PageTables = (uint*)(((byte*)PageDirectory) + 4096);

			uint addr = 0;
			uint* table = (uint*)PageTables;

			// Page directory needs to span all 4 GBs
			// FIXME: What about PAE support might different implementation
			// uint totalPages = UInt32.MaxValue / 4096; // Each page spans of memory 4MB
			uint totalTables = 1024; // 1024 * 4MB = 4GB

			MemoryUtil.MemSet32(0, (uint)PageDirectory, 1024);

			for (int x = 0; x < totalTables; ++x) {
				bool needsDirectoryPresent = false;

				for (int i = 0; i < 1024; ++i) {
					uint val = (addr & (uint)PageAttr.FrameMask) |
						(uint)(PageAttr.ReadWrite);

					if (addr <= totalBytes) {
						val |= (uint)PageAttr.Present;
						needsDirectoryPresent = true;
					}

					table[i] = val;
					addr += 4096;
				}

				// top-level page directory (level-1)
				uint pageAddress = (uint)table & (uint)PageAttr.FrameMask;

				// Make direcory read/write enabled
				pageAddress |= (uint)PageAttr.ReadWrite;

				if (needsDirectoryPresent) {
					// Make directory present if its point to a physical memory already
					pageAddress |= (uint)PageAttr.Present;
				}

				PageDirectory[x] = pageAddress;

				table += 1024;	// 1024 x sizeof(int) = 4k
			}

			DMA.Setup((byte*)((uint)pagemap) + pagemapLen);

			*error = PageAllocator.Errors.Success;
			return *error;
		}

		public static PageAllocator.Errors Enable(PageAllocator.Errors* error)
		{
			if (PageDirectory == null) {
				*error = PageAllocator.Errors.UnusablePageControlBuffer;
				return *error;
			}

			SetDirectory((uint)PageDirectory);

			*error = PageAllocator.Errors.Success;
			return *error;
		}

		/**
			<summary>
				Changes the mapping of an individual page.
			</summary>
		*/
		public static PageAllocator.Errors MapPage(void* page, void* phys_page, uint granularity,
							PageAttributes attr)
		{
			uint nativeAttr = 0, pde = 0, pte = 0;
			uint* table = null;

			// validity checks

			Diagnostics.Assert(ADC.Pager.GetPointerGranularity(page) == granularity,
				"X86.Pager::MapPage(): bad alignment on virtual page pointer");

			Diagnostics.Assert(ADC.Pager.GetPointerGranularity(phys_page) == granularity,
				"X86.Pager::MapPage(): bad alignment on physical page pointer");

			Diagnostics.Assert(page != PageTables,
				"X86.Pager::MapPage(): tried to change mapping of the page table!");

			Diagnostics.Assert(page != PageDirectory,
				"X86.Pager::MapPage(): tried to change mapping of the page directory!");

			Diagnostics.Assert(!PageAllocator.IsPageReserved(page),
				"X86.Pager::MapPage(): tried to change mapping on a reserved page.");

			// perform mapping

			nativeAttr = (uint) GetNativePMA(attr);

			PagePtrToTables(page, &pde, &pte);

			// Make sure the directory is present and read write
			PageDirectory[pde] |= (uint)PageAttr.Present;
			PageDirectory[pde] |= (uint)PageAttr.ReadWrite;

			if (granularity == 0) {
				uint tablePointer = PageDirectory[pde] & (uint)PageAttr.FrameMask;
				table = (uint*)tablePointer;
			}

			if (nativeAttr == 0xFFFFFFFF)
				nativeAttr = table[pte] & (uint)PageAttr.AttributeMask;

			// set our table entry to it's new target

			table[pte] = (uint)phys_page | nativeAttr;

			return PageAllocator.Errors.Success;
		}

		public static PageAllocator.Errors SetPageAttributes (void* page, uint granularity,
								      PageAttributes attr)
		{
			uint pde = 0, pte = 0;
			uint* table = null;
			uint nativeAttr = (uint)GetNativePMA(attr);

			if (granularity < 0 || granularity > 1)
				return PageAllocator.Errors.UnsupportedGranularity;

			Diagnostics.Assert(nativeAttr != 0xFFFFFFFF,
				"X86.Pager::SetPageAttributes(): bad page map attributes");

			Diagnostics.Assert(ADC.Pager.GetPointerGranularity(page) == granularity,
				"X86.Pager::SetPageAttributes(): bad alignment on page pointer");

			PagePtrToTables(page, &pde, &pte);

			table = PageDirectory;

			if (granularity == 0)
				table = (uint*)(PageDirectory[pde] & (uint)PageAttr.FrameMask);

			table[pte] = (table[pte] & (uint)PageAttr.FrameMask) | nativeAttr;

			return PageAllocator.Errors.Success;
		}

		public static PageAttributes GetPageAttributes (void* page, uint granularity,
								      PageAllocator.Errors* ret_err)
		{
			uint pde = 0, pte = 0;
			uint* table = null;

			if (granularity < 0 || granularity > 1) {
				*ret_err = PageAllocator.Errors.UnsupportedGranularity;
				return PageAttributes.None;
			}

			Diagnostics.Assert(ADC.Pager.GetPointerGranularity(page) == granularity,
				"X86.Pager.GetPageAttributes(): bad page pointer alignment!");

			PagePtrToTables(page, &pde, &pte);
			table = PageDirectory;

			if (granularity == 0)
				table = (uint*)(table[pde] & (uint)PageAttr.FrameMask);

			*ret_err = PageAllocator.Errors.Success;
			return GetAbstractPMA((PageAttr)(table[pde] &
					(uint)PageAttr.AttributeMask));
		}

		#endregion
	}
}
