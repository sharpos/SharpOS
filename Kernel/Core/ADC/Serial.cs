//
// (C) 2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Ásgeir Halldórsson <asgeir@ec.is>
//
// Licensed under the terms of the GNU GPL License version 2
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