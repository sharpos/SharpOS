//
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;

namespace SharpOS.Kernel {

	public class ASCII {
		public static byte ToLower (byte ch)
		{
			if (ch >= (byte) 'A' && ch <= (byte) 'Z')
				return (byte) (ch - ((byte) 'A' - (byte) 'a'));
			else
				return ch;
		}

		public static byte ToUpper (byte ch)
		{
			if (ch >= (byte) 'a' && ch <= (byte) 'z')
				return (byte) (ch - ((byte) 'a' - (byte) 'A'));
			else
				return ch;
		}

		public static bool IsLowerAlpha (byte ch)
		{
			if (ch >= (byte) 'a' && ch <= (byte) 'z')
				return true;
			else
				return false;
		}

		public static bool IsUpperAlpha (byte ch)
		{
			if (ch >= (byte) 'A' && ch <= (byte) 'Z')
				return true;
			else
				return false;
		}

		public static bool IsAlpha (byte ch)
		{
			return IsLowerAlpha (ch) || IsUpperAlpha (ch);
		}

		public static bool IsBackspace (byte ch)
		{
			if (ch == 26)
				return true;

			return false;
		}

		public static bool IsNumeric (byte ch)
		{
			if (ch >= (byte) '0' && ch <= (byte) '9')
				return true;
			else
				return false;
		}

		public static bool IsWhiteSpace (byte ch)
		{
			switch (ch) {
			case (byte) ' ':
				return true;
			case (byte) '\n':
				return true;
			case (byte) '\r':
				return true;
			case (byte) '\v':
				return true;
			case (byte) '\f':
				return true;
			case (byte) '\t':
				return true;
			default:
				return false;
			}
		}
	}
}
