//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

namespace SharpOS.Kernel.Tests.CS {
	public class Switch {
		/// <summary>
		/// 'int' switch, value = 0, explicit case
		/// </summary>
		public static uint CMP0 ()
		{
			int value = 0;

			switch (value) {
			case 0:
				return 1;
			case 1:
				return 0;
			default:
				return 2;
			}
		}

		/// <summary>
		/// 'int' switch, value = 5, explicit case
		/// </summary>
		public static uint CMP1 ()
		{
			int value = 5;

			switch (value) {
			case 0:
				return 0;
			case 5:
				return 1;
			default:
				return 2;
			}
		}

		/// <summary>
		/// 'int' switch, value = 5, default
		/// </summary>
		public static uint CMP2 ()
		{
			int value = 5;

			switch (value) {
			case 0:
				return 0;
			case 3:
				return 2;
			default:
				return 1;
			}
		}

		/// <summary>
		/// 'int' switch, value = 0x51, explicit case
		/// </summary>
		public static uint CMP3 ()
		{
			int value = 0x51;

			// FIXME!
			//	This doesn't work.
			//	Change the values or remove one of the cases and it'll work again.
			//	A & C won't work, but B will.
			//  Modifying the order of or Adding new cases has no effect..
			switch (value) {
			case 0x4D:	//A
				{
					return 0;
				}
			case 0x50:	//B
				{
					return 0;
				}
			case 0x51:	//C
				{
					return 1;
				}
			default:
				return 2;
			}
		}

		public unsafe static uint Misc2 (uint granularity)
		{
			switch (granularity) {
			case 0:
				return 4096;
			case 1:
				return 131072;
			default:
				return 0xFFFFFFFF;
			}
		}

		public unsafe static uint CMPMisc2a ()
		{
			if (Misc2 (0) == 4096)
				return 1;

			return 0;
		}

		public unsafe static uint CMPMisc2b ()
		{
			if (Misc2 (1) == 131072)
				return 1;

			return 0;
		}

		public unsafe static uint CMPMisc2c ()
		{
			if (Misc2 (5) == 0xFFFFFFFF)
				return 1;

			return 0;
		}
	}
}
