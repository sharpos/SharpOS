//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	William Lahti <xfurious@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections.Generic;
using System.Text;

namespace SharpOS.Kernel {
	internal static class Stubs {
		#region Stubs

		/// <summary>
		/// Used to statically allocate and initialize a CString8* string.
		/// </summary>
		[SharpOS.AOT.Attributes.String]
		public unsafe static byte* CString (string value)
		{
			return null;
		}

		/// <summary>
		/// Statically allocates a range of bytes.
		/// </summary>
		[SharpOS.AOT.Attributes.Alloc]
		public unsafe static byte* StaticAlloc (uint value)
		{
			return null;
		}

		/// <summary>
		/// Statically allocates a range of bytes and gives it
		/// the specified label.
		/// </summary>
		[SharpOS.AOT.Attributes.LabelledAlloc]
		public unsafe static byte* LabelledAlloc (string label, uint value)
		{
			return null;
		}

		/// <summary>
		/// Gets the function pointer of the given label. This
		/// is a synonym for GetLabelAddress().
		/// </summary>
		[SharpOS.AOT.Attributes.LabelAddress]
		public unsafe static uint GetFunctionPointer (string label)
		{
			return 0;
		}

		/// <summary>
		/// Gets the pointer associated with the given label.
		/// </summary>
		[SharpOS.AOT.Attributes.LabelAddress]
		public unsafe static uint GetLabelAddress (string label)
		{
			return 0;
		}

		[SharpOS.AOT.Attributes.PointerToObject]
		public unsafe static InternalSystem.Object GetObjectFromPointer (void* pointer)
		{
			return null;
		}

		[SharpOS.AOT.Attributes.ObjectToPointer]
		public unsafe static void *GetPointerFromObject (object obj)
		{
			return null;
		}

		#endregion
	}
}
