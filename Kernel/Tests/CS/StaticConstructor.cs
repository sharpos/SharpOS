//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

namespace SharpOS.Kernel.Tests.CS {
	public unsafe class StaticConstructor {
		static StaticConstructor ()
		{
			result = 1;
		}

		static uint result = 0;

		/// <summary>
		/// tests the AOT's handling of a static constructor
		/// </summary>
		public static uint CMPStaticConstructor ()
		{
			return result;
		}
	}
}
