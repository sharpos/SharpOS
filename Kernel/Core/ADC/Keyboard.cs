// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.ADC {
	public unsafe class Keyboard {
		[AOTAttr.ADCStub]
		public static void Setup ()
		{
		}

		[AOTAttr.ADCStub]
		public static EventRegisterStatus RegisterKeyUpEvent (uint address)
		{
			return EventRegisterStatus.NotSupported;
		}

		[AOTAttr.ADCStub]
		public static EventRegisterStatus RegisterKeyDownEvent (uint address)
		{
			return EventRegisterStatus.NotSupported;
		}

		[AOTAttr.ADCStub]
		public static bool LeftShift ()
		{
			return false;
		}

		[AOTAttr.ADCStub]
		public static bool RightShift ()
		{
			return false;
		}

		[AOTAttr.ADCStub]
		public static bool LeftAlt ()
		{
			return false;
		}

		[AOTAttr.ADCStub]
		public static bool RightAlt ()
		{
			return false;
		}

		[AOTAttr.ADCStub]
		public static bool LeftControl ()
		{
			return false;
		}

		[AOTAttr.ADCStub]
		public static bool RightControl ()
		{
			return false;
		}

		[AOTAttr.ADCStub]
		public static bool ScrollLock ()
		{
			return false;
		}

		[AOTAttr.ADCStub]
		public static bool CapsLock ()
		{
			return false;
		}

		[AOTAttr.ADCStub]
		public static bool NumLock ()
		{
			return false;
		}

		[AOTAttr.ADCStub]
		public static byte* GetCurrentDefaultTable (int* ret_len)
		{
			return null;
		}

		[AOTAttr.ADCStub]
		public static byte* GetCurrentShiftedTable (int* ret_len)
		{
			return null;
		}

		[AOTAttr.ADCStub]
		public unsafe static void SetKeyMap (byte* defMap, int defLen, byte* shiftMap,
					      int shiftLen)
		{
			Diagnostics.Assert (true, "Keyboard.SetKeyMap not implemented!");
		}


		[AOTAttr.ADCStub]
		public static byte Translate (uint scancode, bool shifted)
		{
			return 0;
		}
	}
}

