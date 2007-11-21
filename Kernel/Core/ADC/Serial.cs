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

namespace SharpOS.ADC
{
    public class Serial
    {
        [AOTAttr.ADCStub]
        public static void Setup()
        {
            Kernel.Warning("Serial.Setup - not implemented!");
        }

        [AOTAttr.ADCStub]
        public static void PutChar(byte ch)
        {
            Kernel.Warning("Serial.PutChar - not implemented!");
        }
    }
}