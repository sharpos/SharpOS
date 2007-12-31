//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.Kernel;
using SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.Tests.CS {
	public unsafe class ByteString {

		/// <summary>
		/// </summary>
		public static uint CMP0 ()
		{
			byte* str = String ("XYZ");
			uint result = 1;

			if (str [0] != 'X' || str [1] != 'Y' || str [2] != 'Z')
				result = 0;

			return result;
		}

		[String]
		public static byte* String (string str)
		{
			return null;
		}
	}
}
