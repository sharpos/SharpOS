// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//  Phil Garcia <phil@thinkedge.com>
//	Jae Hyun Roh <wonbear@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//
// References
// http://en.wikipedia.org/wiki/Floppy_disk_controller
// http://www.osdev.org/wiki/Floppy_Disk_Controller
// ftp://download.intel.com/design/archives/periphrl/docs/29047403.pdf
// http://www.osdever.net/documents/82077AA_FloppyControllerDatasheet.pdf?the_id=41
// http://www.isdaman.com/alsos/hardware/fdc/floppy.htm
// http://www.osdev.org/phpBB2/viewtopic.php?t=13538

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;
using ADC = SharpOS.Kernel.ADC;

//TODO: Port into new Driver framework

namespace SharpOS.Kernel.ADC.X86
{
	public unsafe class FloppyDiskController
	{
		#region Constants

		const string FLOPPYDISKCONTROLLER_HANDLER = "FLOPPYDISKCONTROLLER_HANDLER";

		const int BYTES_PER_SECTOR = 512;
		const int SECTORS_PER_TRACK = 18;
		const int BYTES_PER_TRACK = BYTES_PER_SECTOR * SECTORS_PER_TRACK;
		const int TRACKS_PER_DISK = 80;	// Assuming 1.44Mb 

		#endregion

		#region Definations

		internal struct FIFOCommand
		{
			internal const byte ReadTrack = 0x02;
			internal const byte Specify = 0x03;
			internal const byte SenseDriveStatus = 0x04;
			internal const byte WriteSector = 0x05;
			internal const byte ReadSector = 0x06;
			internal const byte Recalibrate = 0x07;
			internal const byte SenseInterrupt = 0x08;
			internal const byte WriteDeletedSector = 0x09;
			internal const byte ReadID = 0x0A;
			internal const byte ReadDeletedSector = 0x0C;
			internal const byte FormatTrack = 0x0D;
			internal const byte Seek = 0x0F;
			internal const byte Version = 0x10;
			internal const byte ScanEqual = 0x11;
			internal const byte PerpendicularMode = 0x12;
			internal const byte Configure = 0x13;
			internal const byte Verify = 0x16;
			internal const byte ScanLowOrEqual = 0x19;
			internal const byte ScanHighOrEqual = 0x1D;
			internal const byte MFMModeMask = 0x40;
		};

		internal struct DORFlags
		{
			internal const byte MotorEnableShift = 0x04;
			internal const byte MotorEnableMask = 0x0F;
			internal const byte ResetController = 0x00;
			internal const byte EnableDMA = 0x08;
			internal const byte EnableController = 0x04;
			internal const byte MotorShift = 0x10;
			internal const byte DisableAll = 0x00;
			internal const byte DriveSelectMask = 0x03;
		};

		#endregion

		static SpinLock spinLock;
		static bool fddInterrupt = false;
		static bool enchancedController = false;

		protected struct LastSeek
		{
			public bool calibrated;
			public byte drive;
			public byte track;
			public byte head;
		}

		protected struct TrackCache
		{
			public byte* buffer;
			public bool valid;
			public byte track;
			public byte head;
			public byte drive;
		}

		static TrackCache trackCache;
		static LastSeek lastSeek;

		/// <summary>
		/// Setups this instance.
		/// </summary>
		public static void Setup()
		{
			spinLock.Enter();

			trackCache.buffer = (byte*)MemoryManager.Allocate((uint)BYTES_PER_TRACK);
			trackCache.valid = false;

			IDT.RegisterIRQ(IDT.Interrupt.FloppyDiskController, Stubs.GetFunctionPointer(FLOPPYDISKCONTROLLER_HANDLER));

			lastSeek.calibrated = false;

			SetInterruptOccurred(false);

			ResetController();

			SendByte(FIFOCommand.Version);

			if (GetByte() == 0x80)
				enchancedController = false;
			else
				enchancedController = true;

			if (enchancedController)
				Diagnostics.Message("Enhanced controller detected");
			else
				Diagnostics.Message("Non-enhanced controller detected");

			// Recalibrate(0);

			//byte* block = (byte*)MemoryManager.Allocate((uint)BYTES_PER_TRACK);

			//for (uint i = 0; i < TRACKS_PER_DISK * SECTORS_PER_TRACK * 2; i = i + 18) {
			//    ReadBlock(0, i, 18, block);
			//    Diagnostics.Message("Sector #", (int)i);
			//    Diagnostics.Message("->", (int)((uint)*((uint*)block)));
			//    MemoryUtil.MemSet(0, (uint)block, 32);
			//}

			//ReadBlock(0, 100, 1, block);
			//Diagnostics.Message("->", (int)((uint)*((uint*)block)));
			//ReadBlock(0, 0, 1, block);
			//Diagnostics.Message("->", (int)((uint)*((uint*)block)));
			//WriteBlock(0, 100, 1, block);
			//ReadBlock(0, 200, 1, block);
			//ReadBlock(0, 100, 1, block);

			//Diagnostics.Message("->", (int)((uint)*((uint*)block)));

			spinLock.Exit();
		}

		[SharpOS.AOT.Attributes.Label(FLOPPYDISKCONTROLLER_HANDLER)]
		static unsafe void FloppyDiskControllerHandler(IDT.ISRData data)
		{
			SetInterruptOccurred(true);
		}

		protected static bool WaitForInterrupt()
		{
			//TODO: add timeout check
			while (true)
				if (HasInterruptOccurred()) {
					SetInterruptOccurred(false);
					return true;
				}
		}

		protected static bool HasInterruptOccurred()
		{
			return fddInterrupt;
		}

		protected static void SetInterruptOccurred(bool interruptOccurred)
		{
			fddInterrupt = interruptOccurred;
		}

		protected static bool WaitForReqisterReady()
		{
			// wait for RQM data register to be ready
			while (true) {
				uint status = IO.ReadByte(IO.Port.FDC_StatusPort);

				if ((status & 0x80) == 0x80)
					return true;

				//TODO: add timeout check
			}

			return false;
		}

		protected static void SendByte(byte command)
		{
			WaitForReqisterReady();
			IO.WriteByte(IO.Port.FDC_DataPort, command);
		}

		protected static byte GetByte()
		{
			WaitForReqisterReady();
			return IO.ReadByte(IO.Port.FDC_DataPort);
		}

		protected static void ResetController()
		{
			SetInterruptOccurred(false);	// clear interrupt

			IO.WriteByte(IO.Port.FDC_DORPort, DORFlags.ResetController);

			Timer.Delay(200);	// 20 msec

			IO.WriteByte(IO.Port.FDC_DORPort, DORFlags.EnableController);

			WaitForInterrupt(); //TODO: check for timeout

			IO.WriteByte(IO.Port.FDC_DORPort, DORFlags.EnableController | DORFlags.EnableDMA);

			SendByte(FIFOCommand.SenseInterrupt);
			GetByte();
			GetByte();

			IO.WriteByte(IO.Port.FDC_Config, 0x00); // 500 Kb/s (MFM)			

			SendByte(FIFOCommand.Specify);
			SendByte((((16 - (3)) << 4) | ((240 / 16))));	// set step rate to 3ms & head unload time to 240ms 
			SendByte(0x02); // set head load time to 2ms
		}

		protected static void TurnOffMotor(byte drive)
		{
			//Diagnostics.Message("..Motor Off");
			IO.WriteByte(IO.Port.FDC_DORPort, (byte)(DORFlags.EnableDMA | DORFlags.EnableController | drive));
		}

		protected static void TurnOnMotor(byte drive)
		{
			byte reg = IO.ReadByte(IO.Port.FDC_DORPort);
			byte bits = (byte)(DORFlags.MotorShift << drive | DORFlags.EnableDMA | DORFlags.EnableController | drive);

			if (reg != bits) {
				///Diagnostics.Message("..Motor On");
				IO.WriteByte(IO.Port.FDC_DORPort, bits);
				Timer.Delay(500);	// 500 msec
			}
		}

		protected static bool Recalibrate(byte drive)
		{
			//Diagnostics.Message("..Recalibrating");

			lastSeek.calibrated = false;

			for (int i = 0; i < 5; i++) {
				TurnOnMotor(drive);

				SetInterruptOccurred(false);

				SendByte(FIFOCommand.Recalibrate);
				SendByte(drive);

				WaitForInterrupt();

				SendByte(FIFOCommand.SenseInterrupt);
				byte sr0 = GetByte();
				byte fdc_track = GetByte();

				if (((sr0 & 0xC0) == 0x00) && (fdc_track == 0)) {
					lastSeek.calibrated = true;
					lastSeek.drive = drive;
					lastSeek.track = 0;
					lastSeek.head = 2;	// invalid head (required)
					return true;	// Note: motor is left on				
				}

				//Diagnostics.Message("...Retrying");
			}

			Diagnostics.Message("Recalibrating failed");
			TurnOffMotor(drive);

			return false;
		}

		protected static bool Seek(byte drive, byte track, byte head)
		{
			TurnOnMotor(drive);

			if (!lastSeek.calibrated)
				if (!Recalibrate(drive))
					return false;

			if ((lastSeek.calibrated) && (lastSeek.drive == drive) && (lastSeek.track == track) && (lastSeek.head == head))
				return true;

			//Diagnostics.Message("..Seeking #", track);

			for (int i = 0; i < 5; i++) {
				SetInterruptOccurred(false);

				lastSeek.calibrated = false;

				SendByte(FIFOCommand.Seek);
				SendByte((byte)((drive | (head << 2))));
				SendByte(track);

				if (!WaitForInterrupt())
					return false;

				Timer.Delay(20);

				SendByte(FIFOCommand.SenseInterrupt);
				byte sr0 = GetByte();
				byte trk = GetByte();

				if ((sr0 == (0x20 + (drive | (head << 2)))) && (trk == track)) {
					lastSeek.calibrated = true;
					lastSeek.drive = drive;
					lastSeek.track = track;
					lastSeek.head = head;
					return true;
				}

				//Diagnostics.Message("...Retrying");
			}

			Diagnostics.Message("Seek failed");
			return false;
		}

		protected static byte LBAToTrack(uint lba)
		{
			return (byte)(lba / (SECTORS_PER_TRACK * 2));
		}

		public static byte LBAToHead(uint lba)
		{
			return (byte)((lba % (SECTORS_PER_TRACK * 2)) / SECTORS_PER_TRACK);
		}

		public static byte LBAToSector(uint lba)
		{
			return (byte)((lba % (SECTORS_PER_TRACK * 2)) % SECTORS_PER_TRACK);
		}

		public static uint CHSToLBA(uint cylinder, uint head, uint sector)
		{
			return ((cylinder * 2 + head) * SECTORS_PER_TRACK) + sector - 1;
		}

		public static bool ReadBlock(uint drive, uint lba, uint count, byte* memory)
		{
			try {
				spinLock.Enter();
				for (uint index = 0; index < count; index++) {
					if (!ReadBlock2((byte)drive, lba + index, (byte*)((uint)memory + (index * BYTES_PER_SECTOR))))
						return false;
				}
				return true;
			}
			finally {
				TurnOffMotor((byte)drive);	//TODO: create timer to turn off drive motors after 1 sec.
				spinLock.Exit();
			}
		}

		public static bool WriteBlock(uint drive, uint lba, uint count, byte* memory)
		{
			try {
				spinLock.Enter();
				for (uint index = 0; index < count; index++) {
					if (!WriteBlock2((byte)drive, lba + index, 1, (byte*)((uint)memory + (index * BYTES_PER_SECTOR))))
						return false;
				}
				return true;
			}
			finally {
				TurnOffMotor((byte)drive);	//TODO: create timer to turn off drive motors after 1 sec.
				spinLock.Exit();
			}
		}

		protected static bool ReadBlock2(byte drive, uint lba, byte* memory)
		{
			byte track = LBAToTrack(lba);
			byte head = LBAToHead(lba);
			byte sector = LBAToSector(lba);

			if (!((trackCache.valid) && (trackCache.track == track) && (trackCache.drive == drive) && (trackCache.head == head))) {
				trackCache.valid = false;

				if (!SectorIO(SectorOperation.Read, drive, 0, track, head, SECTORS_PER_TRACK, trackCache.buffer))
					return false;

				trackCache.valid = true;
				trackCache.drive = drive;
				trackCache.head = head;
				trackCache.track = track;
			}

			MemoryUtil.MemCopy((uint)trackCache.buffer + (uint)(sector * BYTES_PER_SECTOR), (uint)memory, BYTES_PER_SECTOR);

			return true;
		}

		protected static bool WriteBlock2(byte drive, uint lba, uint count, byte* memory)
		{
			byte track = LBAToTrack(lba);
			byte head = LBAToHead(lba);
			byte sector = LBAToSector(lba);

			if (sector + count > SECTORS_PER_TRACK)
				return false;

			if ((trackCache.track == track) && (trackCache.drive == drive) && (trackCache.head == head)) {
				// updated track cache
				MemoryUtil.MemCopy((uint)memory, (uint)trackCache.buffer + (uint)(sector * BYTES_PER_SECTOR), count * BYTES_PER_SECTOR);
			}

			if (!SectorIO(SectorOperation.Write, drive, sector, track, head, count, memory))
				return false;

			return true;
		}

		protected enum SectorOperation
		{
			Read,
			Write
		}

		protected static bool SectorIO(SectorOperation operation, byte drive, byte sector, byte track, byte head, uint count, byte* memory)
		{
			//Diagnostics.Message("Sector: ", (int)sector);
			//Diagnostics.Message("Track: ", (int)track);
			//Diagnostics.Message("Head: ", (int)head);

			for (int i = 0; i < 5; i++) {
				int error = 0;

				TurnOnMotor(drive);

				//TODO: Check for disk change

				if (Seek(drive, track, head)) {
					//Diagnostics.Message(".Setup DMA Channel");

					if (operation == SectorOperation.Write) {
						DMA.TransferIn(DMAChannel.Channel2, memory, count * BYTES_PER_SECTOR);
						DMA.SetupChannel(DMAChannel.Channel2, count * BYTES_PER_SECTOR, DMAMode.ReadFromMemory, DMATransferType.Single, DMAAuto.NoAuto);
					}
					else
						DMA.SetupChannel(DMAChannel.Channel2, count * BYTES_PER_SECTOR, DMAMode.WriteToMemory, DMATransferType.Single, DMAAuto.NoAuto);

					SetInterruptOccurred(false);

					if (operation == SectorOperation.Write)
						SendByte(FIFOCommand.WriteSector | FIFOCommand.MFMModeMask);
					else
						SendByte(FIFOCommand.ReadSector | FIFOCommand.MFMModeMask);

					SendByte((byte)((drive) | (head << 2)));	// 0:0:0:0:0:HD:US1:US0 = head and drive
					SendByte(track);// C: 
					SendByte(head);	// H: first head (should match with above)
					SendByte((byte)(sector + 1));	// R: first sector, strangely counts from 1
					SendByte(2);	// N: bytes/sector, 128*2^x (x=2 -> 512) 
					SendByte((byte)(sector + count)); // EOT
					SendByte(0x1B);	// GPL: GAP3 length, 27 is default for 3.5" 
					SendByte(0xFF);	// DTL: (bytes to transfer) = unused

					if (!WaitForInterrupt())
						error = 3;

					byte st0 = GetByte();
					byte st1 = GetByte();
					byte st2 = GetByte();

					byte trk = GetByte();	// track (cylinder)
					byte rhe = GetByte();	// head
					byte sec = GetByte();	// sector number
					byte bps = GetByte();	// bytes per sector

					//Diagnostics.Message("ST0: ", (int)st0);
					//Diagnostics.Message("ST1: ", (int)st1);
					//Diagnostics.Message("ST2: ", (int)st2);
					//Diagnostics.Message("TRK: ", (int)trk);
					//Diagnostics.Message("RHE: ", (int)rhe);
					//Diagnostics.Message("SEC: ", (int)sec);
					//Diagnostics.Message("BPS: ", (int)bps);

					if ((st0 & 0xC0) != 0x0) {
						Diagnostics.Message("FloppyDiskController: error");
						error = 1;
					}
					if (trk != track + 1) {
						Diagnostics.Message("FloppyDiskController: wrong track: ", trk - 1);
						error = 1;
					}
					if (rhe != head) {
						Diagnostics.Message("FloppyDiskController: wrong track: ", trk - 1);
						error = 1;
					}
					if ((st1 & 0x80) != 0x0) {
						Diagnostics.Message("FloppyDiskController: end of cylinder");
						error = 1;
					}
					if ((st0 & 0x08) != 0x0) {
						Diagnostics.Message("FloppyDiskController: drive not ready");
						error = 1;
					}
					if ((st1 & 0x20) != 0x0) {
						Diagnostics.Message("FloppyDiskController: CRC error");
						error = 1;
					}
					if ((st1 & 0x10) != 0x0) {
						Diagnostics.Message("FloppyDiskController: controller timeout");
						error = 1;
					}
					if ((st1 & 0x04) != 0x0) {
						Diagnostics.Message("FloppyDiskController: no data found");
						error = 1;
					}
					if (((st1 | st2) & 0x01) != 0x0) {
						Diagnostics.Message("FloppyDiskController: no address mark found");
						error = 1;
					}
					if ((st2 & 0x40) != 0x0) {
						Diagnostics.Message("FloppyDiskController: deleted address mark");
						error = 1;
					}
					if ((st2 & 0x20) != 0x0) {
						Diagnostics.Message("FloppyDiskController: CRC error in data");
						error = 1;
					}
					if ((st2 & 0x10) != 0x0) {
						Diagnostics.Message("FloppyDiskController: wrong cylinder");
						error = 1;
					}
					if ((st2 & 0x04) != 0x0) {
						Diagnostics.Message("FloppyDiskController: uPD765 sector not found");
						error = 1;
					}
					if ((st2 & 0x02) != 0x0) {
						Diagnostics.Message("FloppyDiskController: bad cylinder");
						error = 1;
					}
					if (bps != 0x02) {
						Diagnostics.Message("FloppyDiskController: wanted 512B/sector, got something else: ", (int)bps);
						error = 1;
					}
					if ((st1 & 0x02) != 0x0) {
						Diagnostics.Message("FloppyDiskController: not writable");
						error = 2;
					}
				}
				else {
					error = 1; // seek failed
				}

				if (error == 0) {
					if (operation == SectorOperation.Write)
						return true;
					else
						if (DMA.TransferOut(DMAChannel.Channel2, memory, count * BYTES_PER_SECTOR))
							return true;

					return false;
				}

				lastSeek.calibrated = false;	// will force recalibration

				if (error > 1) {
					Diagnostics.Message("FloppyDiskController: not retrying..");
					TurnOffMotor(drive);
					break;
				}

			}

			return false;
		}

	}
}
