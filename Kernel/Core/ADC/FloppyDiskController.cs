// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Jae Hyun Roh <wonbear@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Text;
using System.Runtime.InteropServices;
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.ADC
{
    public unsafe class FloppyDiskController
    {
        [AOTAttr.ADCStub]
        public static void Setup()
        {
        }

        [AOTAttr.ADCStub]
        public static bool IsInterruptOccurred()
        {
            return false;
        }

        [AOTAttr.ADCStub]
        public static void SetInterruptOccurred(bool interruptOccurred)
        {
        }

        [AOTAttr.ADCStub]
        public static void TurnOnMotor()
        {
        }

        [AOTAttr.ADCStub]
        public static void TurnOffMotor()
        {
        }

        [AOTAttr.ADCStub]
        public static void SendCommandToFDC(byte data)
        {
        }

        [AOTAttr.ADCStub]
        public static void SetupDMA()
        {
        }

		[AOTAttr.ADCStub]
		public unsafe static void Read(byte* buffer, uint offset, uint length)
		{
		}

		[AOTAttr.ADCStub]
		public unsafe static void ReadData(byte head, byte track, byte sector)
		{
		}
	}
}
