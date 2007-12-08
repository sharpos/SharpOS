//
// (C) 2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Ásgeir Halldórsson <asgeir.halldorsson@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using AOTAttr = SharpOS.AOT.Attributes;
using SharpOS.Foundation;

namespace SharpOS.ADC
{
    public class Serial
    {
        [AOTAttr.ADCStub]
        public static void Setup()
        {
            Diagnostics.Warning("Serial.Setup - not implemented!");
        }

        [AOTAttr.ADCStub]
        public static void WriteChar(byte ch)
        {
            Diagnostics.Warning("Serial.WriteChar(byte) - not implemented!");
        }

        [AOTAttr.ADCStub]
        public static void Write(string str)
        {
            Diagnostics.Warning("Serial.Write(string) - not implemented!");
        }

        [AOTAttr.ADCStub]
        public unsafe static void Write(byte* str)
        {
            Diagnostics.Warning("Serial.Write(byte*) - not implemented!");
        }

        public unsafe static void Write(CString8* str)
        {
            Write((byte*)str);
        }

        public unsafe static void WriteNumber(int number, bool hex)
        {
            CString8* str = Foundation.Convert.ToString(number, hex);
            Write(str);
            ADC.MemoryManager.Free(str);
        }
    }
}