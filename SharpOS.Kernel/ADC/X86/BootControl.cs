/*
 * SharpOS.ADC.X86/ADC.cs
 * N:SharpOS.ADC
 *
 * (C) 2007 William Lahti. This software is licensed under the terms of the
 * GNU General Public License.
 *
 * Author: William Lahti <xfurious@gmail.com>
 *
 */

using SharpOS.AOT;

namespace SharpOS.ADC.X86 {
	public unsafe class BootControl {
		/**
			<summary>
				Checks for compatibility with the current system, using 
				the most well-supported method possible. 
			</summary>
		*/
		public static void Halt()
			{ }
		
		public static void Reboot()
			{ }
	}
}
