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
		#region Constants
		
		const string	FLOPPYDISKCONTROLLER_HANDLER = "FLOPPYDISKCONTROLLER_HANDLER";

		const int BYTES_PER_SECTOR = 512;
		const int SECTORS_PER_TRACK = 18;
		
		#endregion
		#region Enumerations
		private enum FIFOCommand
		{
			ReadTrack			= 0x02,
			Specify				= 0x03,
			SenseDriveStatus 	= 0x04,
			WriteData			= 0x05,
			ReadData			= 0x06,
			Recalibrate			= 0x07,
			SenseInterrupt		= 0x08,
			WriteDeletedData	= 0x09,
			ReadID				= 0x0A,

			ReadDeletedData		= 0x0C,
			FormatTrack			= 0x0D,

			Seek				= 0x0F,
			Version				= 0x10,
			ScanEqual			= 0x11,
			PerpendicularMode	= 0x12,
			Configure			= 0x13,

			Verify				= 0x16,

			ScanLowOrEqual		= 0x19,

			ScanHighOrEqual		= 0x1D,

			Write				= 0xC5,
			Read				= 0xE6,
			Format				= 0x4D
		};

		[Flags]
		private enum DORFlags : byte
		{
			MotorEnableShift	= 0x04,
			MotorEnableMask		= 0x0F,

			EnableDMA			= 0x08,
			EnableController	= 0x04,
			EnableAllMotors		= 0x10,
			DisableAll			= 0x00,

			DriveSelectShift	= 0x00,
			DriveSelectMask		= 0x03,
		};
		#endregion

		static bool fddInterrupt = false;
		public static void Setup ()
		{
			IDT.RegisterIRQ (IDT.Interrupt.FloppyDiskController, Stubs.GetFunctionPointer (FLOPPYDISKCONTROLLER_HANDLER));
			TurnOffMotor ();
		}

		[SharpOS.AOT.Attributes.Label (FLOPPYDISKCONTROLLER_HANDLER)]
		static unsafe void FloppyDiskControllerHandler (IDT.ISRData data)
		{
			SetInterruptOccurred (true);
			
			// This is not necesarry, already done in code wrapped around this:
			//IO.Out8(IO.Port.Master_PIC_CommandPort, 0x20);
		}
		
		public static bool WaitForInterrupt()
		{
			int counter = 0;
			while (HasInterruptOccurred () == false && counter < 10000)
				counter++;
			return (counter < 10000);
		}

		public static bool HasInterruptOccurred ()
		{
			return fddInterrupt;
		}

		public static void SetInterruptOccurred (bool interruptOccurred)
		{
			fddInterrupt = interruptOccurred;
		}

		public static void TurnOffMotor ()
		{
			IO.Write8 (IO.Port.FDC_DORPort,
				(byte) DORFlags.DisableAll);
		}

		public static void TurnOnMotor ()
		{
			IO.Write8 (IO.Port.FDC_DORPort,
						(byte)(DORFlags.EnableDMA | DORFlags.EnableController | DORFlags.EnableAllMotors));
		}

		private unsafe static void SendCommandToFDC (byte command)
		{
			Barrier.Enter();
			byte status = 0;

			do {
				status = IO.Read8 (IO.Port.FDC_StatusPort);
			}
			while ((status & 0xC0) != 0x80); //TODO: implement timeout

			IO.Write8 (IO.Port.FDC_DataPort, command);
			Barrier.Exit();
		}

		private unsafe static void SendDataToFDC (byte data)
		{
			Barrier.Enter();
			byte status = 0;

			do {
				status = IO.Read8 (IO.Port.FDC_StatusPort);
			}
			while ((status & 0xC0) != 0x80); //TODO: implement timeout

			IO.Write8 (IO.Port.FDC_DataPort, data);
			Barrier.Exit();
		}

		//TODO: replace integer values with enums or describe in comments
		//		..should we create a DMA.cs for all kernel DMA handling?
		public unsafe static void SetupDMA ()
		{
			Barrier.Enter();
			System.UInt16 count = BYTES_PER_SECTOR * SECTORS_PER_TRACK - 1;

			byte DMA_Channel = 0x02;

			// Disable DMA Controller
			IO.Write8 (IO.Port.DMA_ChannelMaskRegister, (byte)(DMA_Channel | 4));

			// Set DMA_Channel to write
			IO.Write8 (IO.Port.DMA_ModeRegister, (byte)(0x48 | DMA_Channel));

			// Set Address
			IO.Write8 (IO.Port.DMA_Channel2AddressByte2,   (byte) (((uint)diskBuffer) >> 16));
			IO.Write8 (IO.Port.DMA_Channel2AddressByte0_1, (byte) (((uint)diskBuffer) >> 0));
			IO.Write8 (IO.Port.DMA_Channel2AddressByte0_1, (byte) (((uint)diskBuffer) >> 8));

			// Set Count
			IO.Write8 (IO.Port.DMA_Channel2CountByte0_1, (byte) count);
			IO.Write8 (IO.Port.DMA_Channel2CountByte0_1, (byte) (count >> 8));
			
			// Enable DMA Controller
			IO.Write8 (IO.Port.DMA_ChannelMaskRegister, (byte)(DMA_Channel));
			Barrier.Exit();
		}

		internal static byte* diskBuffer = (byte*)0x00000000;

		public unsafe static void Read(byte* buffer, uint offset, uint length)
		{
			uint readBlockCount = (length + (512 * 18 - 1)) / (512 * 18);

			for (uint b = 0; b < readBlockCount; ++b)
			{
				byte sector = (byte)(offset / BYTES_PER_SECTOR);

				byte head = (byte)((sector % (SECTORS_PER_TRACK * 2)) / SECTORS_PER_TRACK);
				byte track = (byte)(sector / (SECTORS_PER_TRACK * 2));

				sector = (byte)((sector % SECTORS_PER_TRACK) + 1);

				MemoryUtil.MemSet(0, (uint)(diskBuffer), (uint)(BYTES_PER_SECTOR * SECTORS_PER_TRACK));
				ReadData(head, track, sector);

				for (uint index = offset, index2 = 0; index < offset + 512 * 18; ++index, ++index2)
					buffer[index] = diskBuffer[index2];

				offset += 512 * 18;
			}
		}

		//TODO: replace integer values with enums or describe in comments
		public unsafe static void ReadData(byte head, byte track, byte sector)
		{
			// ...this section should be skipped if motor is already running
			Barrier.Enter();
			{
				SetInterruptOccurred (false);
				TurnOffMotor ();
				TurnOnMotor ();
			}
			Barrier.Exit();

			if (!WaitForInterrupt())
				return;
					
			// ...do we need to recalibrate every time?
			Barrier.Enter();
			{
				SetInterruptOccurred (false);
				SendCommandToFDC((byte)FIFOCommand.Recalibrate);
				SendDataToFDC (0x00);
			}
			Barrier.Exit();

			if (!WaitForInterrupt())
				return;
						
			Barrier.Enter();
			{
				SetInterruptOccurred (false);
				SendCommandToFDC((byte)FIFOCommand.Seek);
				SendCommandToFDC((byte)((head << 2) + 0));
				SendCommandToFDC(track);
			}
			Barrier.Exit();

			if (!WaitForInterrupt())
				return;
									
			SetupDMA ();
				
			Barrier.Enter();
			{
				SetInterruptOccurred (false);
				
				SendCommandToFDC((byte)FIFOCommand.Read);
				SendDataToFDC ((byte)((head << 2) + 0));
				SendDataToFDC (track);
				SendDataToFDC (head);
				SendDataToFDC (sector);
				SendDataToFDC (2);		// 512 bytes per sector
				SendDataToFDC (SECTORS_PER_TRACK);
				SendDataToFDC (27);
				SendDataToFDC (0xff);	// DTL (bytes to transfer) = unused
			}
			Barrier.Exit();

			if (!WaitForInterrupt())
				return;
						
			Barrier.Enter();
			{
				TurnOffMotor();
			}
			Barrier.Exit();
		}
	}
}
