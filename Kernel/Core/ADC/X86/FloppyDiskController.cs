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
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using ADC = SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.ADC.X86
{
    public unsafe class FloppyDiskController
    {
        const string FLOPPYDISKCONTROLLER_HANDLER = "FLOPPYDISKCONTROLLER_HANDLER";

        static bool fddInterrupt = false;

        const int BYTES_PER_SECTOR = 512;
        const int SECTORS_PER_TRACK = 18;

        public static void Setup()
        {
            IDT.RegisterIRQ(IDT.Interrupt.FloppyDiskController, Stubs.GetFunctionPointer(FLOPPYDISKCONTROLLER_HANDLER));

            TurnOffMotor();
        }

        [SharpOS.AOT.Attributes.Label(FLOPPYDISKCONTROLLER_HANDLER)]
        static unsafe void FloppyDiskControllerHandler(IDT.ISRData data)
        {
            SetInterruptOccurred(true);
            IO.Out8(IO.Port.Master_PIC_CommandPort, 0x20);
        }

        public static bool IsInterruptOccurred()
        {
            return fddInterrupt;
        }

        public static void SetInterruptOccurred(bool interruptOccurred)
        {
            fddInterrupt = interruptOccurred;
        }

        public static void TurnOffMotor()
        {
            IO.Out8(IO.Port.FDC_DORPort, (byte)0x00);
        }

        public static void TurnOnMotor()
        {
            IO.Out8(IO.Port.FDC_DORPort, (byte)0x1c);
        }

        public unsafe static void SendCommandToFDC(byte data)
        {
            byte status = 0;

            do
            {
                status = IO.In8(IO.Port.FDC_StatusPort);
            }
            while ((status & 0xC0) != 0x80);

            IO.Out8(IO.Port.FDC_DataPort, data);
        }

        public unsafe static void SetupDMA()
        {
            System.UInt16 count = BYTES_PER_SECTOR * SECTORS_PER_TRACK * 7 - 1;

            IO.Out8(IO.Port.DMA_ModeRegister, 0x46);

            // Set Address
            IO.Out8(IO.Port.DMA_AddressRegister, 0x00);
            IO.Out8(IO.Port.DMA_AddressRegister, 0x00);
            IO.Out8(IO.Port.DMA_TempRegister, 0x00);

            // Set Count
            IO.Out8(IO.Port.DMA_CountRegister, (byte)count);
            IO.Out8(IO.Port.DMA_CountRegister, (byte)(count >> 8));

            // Enable DMA Controller
            IO.Out8(IO.Port.DMA_ChannelMaskRegister, 0x02);
        }

        public unsafe static void ReadData()
        {
            SetInterruptOccurred(false);

            {
                TurnOnMotor();
            }
            while (IsInterruptOccurred() == false) ;

            SetInterruptOccurred(false);

            {
                SendCommandToFDC(0x07);
                SendCommandToFDC(0x00);
            }
            while (IsInterruptOccurred() == false) ;

            SetInterruptOccurred(false);

            {
                SendCommandToFDC(0x0f);
                SendCommandToFDC((0x00 << 2) + 0);
                SendCommandToFDC(0);
            }
            while (IsInterruptOccurred() == false) ;

            SetupDMA();

            SetInterruptOccurred(false);

            {
                SendCommandToFDC(0xe6);
                SendCommandToFDC((0x00 << 2) + 0);
                SendCommandToFDC(0);
                SendCommandToFDC(0);
                SendCommandToFDC(1);
                SendCommandToFDC(2);
                SendCommandToFDC(18);
                SendCommandToFDC(27);
                SendCommandToFDC(0xff);
            }
            while (IsInterruptOccurred() == false) ;
        }
    }
}
