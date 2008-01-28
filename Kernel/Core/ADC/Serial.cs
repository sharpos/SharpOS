//
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	�sgeir Halld�rsson <asgeir.halldorsson@gmail.com>
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using AOTAttr = SharpOS.AOT.Attributes;
using SharpOS.Kernel.Foundation;

namespace SharpOS.Kernel.ADC {
	public class Serial {
		[AOTAttr.ADCStub]
		public static void Setup ()
		{
			Diagnostics.Warning ("Serial.Setup - not implemented!");
		}

		[AOTAttr.ADCStub]
		public static void WriteChar (byte ch)
		{
			Diagnostics.Warning ("Serial.WriteChar(byte) - not implemented!");
		}

		[AOTAttr.ADCStub]
		public static void WriteLine ()
		{
			Diagnostics.Warning ("Serial.WriteLine() - not implemented!");
		}

		[AOTAttr.ADCStub]
		public static void Write (string str)
		{
			Diagnostics.Warning ("Serial.Write(string) - not implemented!");
		}

		[AOTAttr.ADCStub]
		public static void WriteLine (string str)
		{
			Diagnostics.Warning ("Serial.WriteLine(string) - not implemented!");
		}

		[AOTAttr.ADCStub]
		public unsafe static void Write (byte* str)
		{
			Diagnostics.Warning ("Serial.Write(byte*) - not implemented!");
		}

		public unsafe static void Write (CString8* str)
		{
			Write ((byte*) str);
		}

		public unsafe static void WriteNumber (int number, bool hex)
		{
			CString8* str = Foundation.Convert.ToString (number, hex);
			Write (str);
			ADC.MemoryManager.Free (str);
		}

		public unsafe static void Write (int number)
		{
			WriteNumber (number, false);
		}

		public unsafe static void Write (int number, bool hex)
		{
			WriteNumber (number, hex);
		}
	}
}