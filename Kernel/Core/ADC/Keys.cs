// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

namespace SharpOS.Kernel.ADC {
	public enum Keys : uint {
		Escape = 0x0001,
		Backspace = 0x000E,
		Tab = 0x000F,
		Enter = 0x001C,
		LeftControl = 0x001D,
		RightControl = 0xe01D,
		LeftShift = 0x002A,
		RightShift = 0x0036,
		LeftAlt = 0x0038,
		RightAlt = 0xe038,
		CapsLock = 0x003A,
		NumLock = 0x0045,
		ScrollLock = 0x0046,

		F1 = 0x003B,
		F2 = 0x003C,
		F3 = 0x003D,
		F4 = 0x003E,
		F5 = 0x003F,
		F6 = 0x0040,
		F7 = 0x0041,
		F8 = 0x0042,
		F9 = 0x0043,
		F10 = 0x0044,
		F11 = 0x0057,
		F12 = 0x0058,

		Home = 0x0047,
		UpArrow = 0x0048,
		PageUp = 0x0049,
		LeftArrow = 0x004B,
		RightArrow = 0x004D,
		End = 0x004F,
		DownArrow = 0x0050,
		PageDown = 0x0051,
		Insert = 0x0052,
		Delete = 0x0053
	}
}
