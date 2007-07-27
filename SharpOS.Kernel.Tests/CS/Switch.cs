//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
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
	}
}
