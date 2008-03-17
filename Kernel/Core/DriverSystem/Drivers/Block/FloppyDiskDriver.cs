// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Phil Garcia <phil@thinkedge.com>
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
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.DriverSystem;

namespace SharpOS.Kernel.DriverSystem.Drivers.Block
{
	public class FloppyDiskDriver : GenericDriver, IBlockDeviceController
	{

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

		internal struct FDC
		{
			internal const uint BytesPerSector = 512;
			internal const uint MaxSectorsPerTracks = 36; // 2.88 = 36, DMF formatted disks can have up to 21 sectors
			internal const uint MaxBytesPerTrack = MaxSectorsPerTracks * BytesPerSector;
		};

		#endregion

		public enum FloppyDriveType : byte
		{
			None,
			Floppy_5_25,
			Floppy_3_5,
			Unknown
		}

		public struct FloppyDriveInfo
		{
			// drive information
			public FloppyDriveType Type;
			public uint KiloByteSize;
		}

		public struct FloppyMediaInfo
		{
			// media information
			public uint SectorsPerTrack;
			public uint TotalTracks;
			public byte Gap1Length;
			public byte Gap2Length;
		}

		protected struct LastSeek
		{
			public bool calibrated;
			public byte drive;
			public byte track;
			public byte head;
		}

		protected unsafe struct TrackCache
		{
			public MemoryBlock buffer;
			public bool valid;
			public byte track;
			public byte head;
		}

		protected SpinLock spinLock;
		protected bool enchancedController = false;

		public const uint DrivesPerConroller = 2; // the maximun that is supported

		protected FloppyDriveInfo[] floppyDrives;
		protected FloppyMediaInfo[] floppyMedia;

		private TrackCache[] trackCache;
		private LastSeek[] lastSeek;

		private IOPortStream ControllerCommands;
		private IOPortStream DataPort;
		private IOPortStream ConfigPort;
		private IOPortStream StatusPort;
		private IOPortStream CMOSComand;
		private IOPortStream CMOSResponse;
		private DMAChannel FloppyDMA;
		private IRQHandler FloppyIRQ;
		private bool verbose;

		private static String[] driveNames;

		public override bool Initialize (IDriverContext context)
		{
			spinLock.Enter ();

			Diagnostics.Message ("Floppy Disk Controller");

			context.Initialize (DriverFlags.IOStream8Bit);

			ushort iobase = 0x03F2; // Secondary is at 0x0372

			ControllerCommands = context.CreateIOPortStream ((ushort)iobase);
			StatusPort = context.CreateIOPortStream ((ushort)(iobase + 2));
			DataPort = context.CreateIOPortStream ((ushort)(iobase + 3));
			ConfigPort = context.CreateIOPortStream ((ushort)(iobase + 5));

			CMOSComand = context.CreateIOPortStream ((ushort)(0x70));
			CMOSResponse = context.CreateIOPortStream ((ushort)(0x71));

			FloppyDMA = context.CreateDMAChannel (2);
			FloppyIRQ = context.CreateIRQHandler (6);

			driveNames = new string[DrivesPerConroller];
			driveNames[0] = "floppy1";
			driveNames[1] = "floppy2";

			floppyDrives = new FloppyDriveInfo[DrivesPerConroller];
			floppyMedia = new FloppyMediaInfo[DrivesPerConroller];

			trackCache = new TrackCache[DrivesPerConroller];
			lastSeek = new LastSeek[DrivesPerConroller];

			for (int drive = 0; drive < DrivesPerConroller; drive++) {
				trackCache[drive].buffer.Allocate (FDC.MaxBytesPerTrack);
				trackCache[drive].valid = false;

				lastSeek[drive].calibrated = false;

				// default
				floppyMedia[drive].SectorsPerTrack = 18;
				floppyMedia[drive].TotalTracks = 80;
				//TODO: for 5.25, Gap1 = 0x2A and Gap2 = 0x50
				floppyMedia[drive].Gap1Length = 0x1B;	// 27
				floppyMedia[drive].Gap2Length = 0x54;
			}

			verbose = true;

			FloppyIRQ.ClearInterrupt ();

			ResetController ();

			SendByte (FIFOCommand.Version);

			if (GetByte () == 0x80)
				enchancedController = false;
			else
				enchancedController = true;

			if (enchancedController)
				Diagnostics.Message ("->Enhanced controller detected");
			else
				Diagnostics.Message ("->Non-enhanced controller detected");

			DetectDrives ();

			for (uint drive = 0; drive < DrivesPerConroller; drive++) {
				if (floppyDrives[drive].Type != FloppyDriveType.None) {
					Open (drive);

					Diagnostics.Message ("-->Drive Found: ", (int)drive);
					Diagnostics.Message ("--->Max. Drive Size in KB: ", (int)floppyDrives[drive].KiloByteSize);
					Diagnostics.Message ("---->Current Media - Sectors Per Track: ", (int)floppyMedia[drive].SectorsPerTrack);

					//Timer.Delay (1000 * 5);
					DeviceResource resource = new DeviceResource (driveNames[drive], DeviceResourceType.FloppyDisk, new GenericBlockDeviceAdapter (this, drive), DeviceResourceStatus.Online, 0, drive);
					DeviceResourceManager.Add (resource);
				}
			}

			//MemoryBlock block = new MemoryBlock();

			//block.Allocate(FDC.BYTES_PER_TRACK);

			//for (uint i = 0; i < FDC.TRACKS_PER_DISK * FDC.SECTORS_PER_TRACK * 2; i = i + 18) {
			//    ReadBlock(0, i, 18, block);
			//    Diagnostics.Message("Sector #", (int)i);
			//    Diagnostics.Message("->", (int)((uint)*((uint*)block.address)));
			//    MemoryUtil.MemSet(0, (uint)block.address, 32);
			//}


			return (isInitialized = false);
		}

		public int Open (uint drive)
		{
			// clear it
			floppyMedia[drive].TotalTracks = 0;
			floppyMedia[drive].SectorsPerTrack = 0;

			MemoryBlock temp = new MemoryBlock (FDC.BytesPerSector);
			spinLock.Enter ();
			verbose = false;	// surpress failures messages

			try {
				//TODO: check drive type first

				// attempt to read 2.88MB/2880KB
				floppyMedia[drive].SectorsPerTrack = 36;
				floppyMedia[drive].TotalTracks = 80;
				floppyMedia[drive].Gap1Length = 0x1B;
				floppyMedia[drive].Gap2Length = 0x54;

				if (ReadBlock (drive, CHSToLBA (drive, floppyMedia[drive].TotalTracks - 1, 0, floppyMedia[drive].SectorsPerTrack - 1), 1, temp))
					return 0;

				// attempt to read 1.64MB/1680KB (DMF)
				floppyMedia[drive].SectorsPerTrack = 21;
				floppyMedia[drive].TotalTracks = 80;
				floppyMedia[drive].Gap1Length = 0x0C;
				floppyMedia[drive].Gap2Length = 0x1C;

				if (ReadBlock (drive, CHSToLBA (drive, floppyMedia[drive].TotalTracks - 1, 0, floppyMedia[drive].SectorsPerTrack - 1), 1, temp))
					return 0;

				// attempt to read 1.44MB
				floppyMedia[drive].TotalTracks = 80;
				floppyMedia[drive].SectorsPerTrack = 18;
				floppyMedia[drive].Gap1Length = 0x1B;
				floppyMedia[drive].Gap2Length = 0x54;

				if (ReadBlock (drive, CHSToLBA (drive, floppyMedia[drive].TotalTracks - 1, 0, floppyMedia[drive].SectorsPerTrack - 1), 1, temp))
					return 0;

				// attempt to read 720Kb
				floppyMedia[drive].TotalTracks = 80;
				floppyMedia[drive].SectorsPerTrack = 9;
				floppyMedia[drive].Gap1Length = 0x1B;
				floppyMedia[drive].Gap2Length = 0x54;

				if (ReadBlock (drive, CHSToLBA (drive, floppyMedia[drive].TotalTracks - 1, 0, floppyMedia[drive].SectorsPerTrack - 1), 1, temp))
					return 0;

				// unable to read floppy media 
				floppyMedia[drive].TotalTracks = 0;
				floppyMedia[drive].SectorsPerTrack = 0;

				return -1;
			}
			finally {
				spinLock.Exit ();
				temp.Release ();
				verbose = true;
			}

			return -1;

		}

		public int Release (uint drive)
		{
			return 0;
		}

		public uint GetBlockSize (uint drive)
		{
			return 512;
		}

		public uint GetTotalBlocks (uint drive)
		{
			return floppyMedia[drive].SectorsPerTrack * floppyMedia[drive].TotalTracks * 2;
		}

		public bool CanWrite (uint drive)
		{
			if (floppyMedia[drive].SectorsPerTrack == 0)
				return false;

			return true;
		}

		public IDevice GetDeviceDriver ()
		{
			return null;
			//return (IDevice)this;
		}

		protected bool WaitForReqisterReady ()
		{
			// wait for RQM data register to be ready
			while (true) {
				uint status = StatusPort.ReadByte ();

				if ((status & 0x80) == 0x80)
					return true;

				//TODO: add timeout check
			}

			return false;
		}

		protected void SendByte (byte command)
		{
			WaitForReqisterReady ();
			DataPort.Write (command);
		}

		protected byte GetByte ()
		{
			WaitForReqisterReady ();
			return DataPort.ReadByte ();
		}

		protected void ResetController ()
		{
			FloppyIRQ.ClearInterrupt ();

			ControllerCommands.WriteByte (DORFlags.ResetController);

			Timer.Delay (200);	// 20 msec

			ControllerCommands.WriteByte (DORFlags.EnableController);

			FloppyIRQ.WaitForInterrupt (3000);

			ControllerCommands.WriteByte (DORFlags.EnableController | DORFlags.EnableDMA);

			SendByte (FIFOCommand.SenseInterrupt);
			GetByte ();
			GetByte ();

			ConfigPort.WriteByte (0x00); // 500 Kb/s (MFM)			

			SendByte (FIFOCommand.Specify);
			SendByte ((((16 - (3)) << 4) | ((240 / 16))));	// set step rate to 3ms & head unload time to 240ms 
			SendByte (0x02); // set head load time to 2ms
		}

		protected static FloppyDriveInfo DetermineByType (byte type)
		{
			FloppyDriveInfo floppy = new FloppyDriveInfo ();

			switch (type) {
				case 0: { floppy.Type = FloppyDriveType.None; floppy.KiloByteSize = 0; break; }
				case 1: { floppy.Type = FloppyDriveType.Floppy_5_25; floppy.KiloByteSize = 360; break; }
				case 2: { floppy.Type = FloppyDriveType.Floppy_5_25; floppy.KiloByteSize = 1200; break; }
				case 3: { floppy.Type = FloppyDriveType.Floppy_3_5; floppy.KiloByteSize = 720; break; }
				case 4: { floppy.Type = FloppyDriveType.Floppy_3_5; floppy.KiloByteSize = 1440; break; }
				case 5: { floppy.Type = FloppyDriveType.Floppy_3_5; floppy.KiloByteSize = 2880; break; }
				default: { floppy.Type = FloppyDriveType.Unknown; floppy.KiloByteSize = 0; break; }
			}

			return floppy;
		}

		protected void DetectDrives ()
		{
			CMOSComand.WriteByte (0x10);
			byte types = CMOSResponse.ReadByte ();

			floppyDrives[0] = DetermineByType ((byte)(types >> 4));
			floppyDrives[1] = DetermineByType ((byte)(types & 0xF));
		}

		protected void TurnOffMotor (byte drive)
		{
			//Diagnostics.Message("..Motor Off");
			ControllerCommands.Write ((byte)(DORFlags.EnableDMA | DORFlags.EnableController | drive));
		}

		protected void TurnOnMotor (byte drive)
		{
			byte reg = ControllerCommands.ReadByte ();
			byte bits = (byte)(DORFlags.MotorShift << drive | DORFlags.EnableDMA | DORFlags.EnableController | drive);

			if (reg != bits) {
				///Diagnostics.Message("..Motor On");
				ControllerCommands.Write (bits);
				Timer.Delay (500);	// 500 msec
			}
		}

		protected bool Recalibrate (byte drive)
		{
			//Diagnostics.Message("..Recalibrating");

			lastSeek[drive].calibrated = false;

			for (int i = 0; i < 5; i++) {
				TurnOnMotor (drive);

				FloppyIRQ.ClearInterrupt ();

				SendByte (FIFOCommand.Recalibrate);
				SendByte (drive);

				FloppyIRQ.WaitForInterrupt (3000);

				SendByte (FIFOCommand.SenseInterrupt);
				byte sr0 = GetByte ();
				byte fdc_track = GetByte ();

				if (((sr0 & 0xC0) == 0x00) && (fdc_track == 0)) {
					lastSeek[drive].calibrated = true;
					lastSeek[drive].track = 0;
					lastSeek[drive].head = 2;	// invalid head (required)
					return true;	// Note: motor is left on				
				}

				//Diagnostics.Message("...Retrying");
			}

			if (verbose) Diagnostics.Message ("Recalibrating failed");
			TurnOffMotor (drive);

			return false;
		}

		protected bool Seek (byte drive, byte track, byte head)
		{
			TurnOnMotor (drive);

			if (!lastSeek[drive].calibrated)
				if (!Recalibrate (drive))
					return false;

			if ((lastSeek[drive].calibrated) && (lastSeek[drive].track == track) && (lastSeek[drive].head == head))
				return true;

			//Diagnostics.Message("..Seeking #", track);

			for (int i = 0; i < 5; i++) {
				FloppyIRQ.ClearInterrupt ();

				lastSeek[drive].calibrated = false;

				SendByte (FIFOCommand.Seek);
				SendByte ((byte)((drive | (head << 2))));
				SendByte (track);

				if (!FloppyIRQ.WaitForInterrupt (3000))
					return false;

				Timer.Delay (20);

				SendByte (FIFOCommand.SenseInterrupt);
				byte sr0 = GetByte ();
				byte trk = GetByte ();

				if ((sr0 == (0x20 + (drive | (head << 2)))) && (trk == track)) {
					lastSeek[drive].calibrated = true;
					lastSeek[drive].track = track;
					lastSeek[drive].head = head;
					return true;
				}

				//Diagnostics.Message("...Retrying");
			}

			if (verbose) Diagnostics.Message ("Seek failed");
			return false;
		}

		protected byte LBAToTrack (uint drive, uint lba)
		{
			return (byte)(lba / (floppyMedia[drive].SectorsPerTrack * 2));
		}

		protected byte LBAToHead (uint drive, uint lba)
		{
			return (byte)((lba % (floppyMedia[drive].SectorsPerTrack * 2)) / floppyMedia[drive].SectorsPerTrack);
		}

		protected byte LBAToSector (uint drive, uint lba)
		{
			return (byte)((lba % (floppyMedia[drive].SectorsPerTrack * 2)) % floppyMedia[drive].SectorsPerTrack);
		}

		protected uint CHSToLBA (uint drive, uint cylinder, uint head, uint sector)
		{
			return ((cylinder * 2 + head) * floppyMedia[drive].SectorsPerTrack) + sector - 1;
		}

		public bool ReadBlock (uint drive, uint lba, uint count, MemoryBlock memory)
		{
			try {
				spinLock.Enter ();
				for (uint index = 0; index < count; index++) {
					if (!ReadBlock2 ((byte)drive, lba + index, memory.Offset (index * FDC.BytesPerSector)))
						return false;
				}
				return true;
			}
			finally {
				TurnOffMotor ((byte)drive);	//TODO: create timer to turn off drive motors after 1 sec.
				spinLock.Exit ();
			}
		}

		public bool WriteBlock (uint drive, uint lba, uint count, MemoryBlock memory)
		{
			try {
				spinLock.Enter ();
				for (uint index = 0; index < count; index++) {
					if (!WriteBlock2 ((byte)drive, lba + index, 1, memory.Offset (index * FDC.BytesPerSector)))
						return false;
				}
				return true;
			}
			finally {
				TurnOffMotor ((byte)drive);	//TODO: create timer to turn off drive motors after 1 sec.
				spinLock.Exit ();
			}
		}

		protected bool ReadBlock2 (byte drive, uint lba, MemoryBlock memory)
		{
			byte track = LBAToTrack (drive, lba);
			byte head = LBAToHead (drive, lba);
			byte sector = LBAToSector (drive, lba);

			if (!((trackCache[drive].valid) && (trackCache[drive].track == track) && (trackCache[drive].head == head))) {
				trackCache[drive].valid = false;

				if (!SectorIO (SectorOperation.Read, drive, 0, track, head, floppyMedia[drive].SectorsPerTrack, trackCache[drive].buffer))
					return false;

				trackCache[drive].valid = true;
				trackCache[drive].head = head;
				trackCache[drive].track = track;
			}

			trackCache[drive].buffer.Offset (sector * FDC.BytesPerSector).CopyTo (memory, FDC.BytesPerSector);

			return true;
		}

		protected bool WriteBlock2 (byte drive, uint lba, uint count, MemoryBlock memory)
		{
			byte track = LBAToTrack (drive, lba);
			byte head = LBAToHead (drive, lba);
			byte sector = LBAToSector (drive, lba);

			if (sector + count > floppyMedia[drive].SectorsPerTrack)
				return false;

			if ((trackCache[drive].track == track) && (trackCache[drive].head == head)) {
				// updated track cache
				trackCache[drive].buffer.Offset (sector * FDC.BytesPerSector).CopyFrom (memory, count * FDC.BytesPerSector);

			}

			if (!SectorIO (SectorOperation.Write, drive, sector, track, head, count, memory))
				return false;

			return true;
		}

		protected enum SectorOperation
		{
			Read,
			Write
		}

		protected bool SectorIO (SectorOperation operation, byte drive, byte sector, byte track, byte head, uint count, MemoryBlock memory)
		{
			//Diagnostics.Message("Sector: ", (int)sector);
			//Diagnostics.Message("Track: ", (int)track);
			//Diagnostics.Message("Head: ", (int)head);

			for (int i = 0; i < 5; i++) {
				int error = 0;

				TurnOnMotor (drive);

				//TODO: Check for disk change

				if (Seek (drive, track, head)) {
					//Diagnostics.Message(".Setup DMA Channel");

					if (operation == SectorOperation.Write) {
						FloppyDMA.TransferIn (memory, count * FDC.BytesPerSector);
						FloppyDMA.SetupChannel (DMAMode.ReadFromMemory, DMATransferType.Single, false, count * FDC.BytesPerSector);
					}
					else
						FloppyDMA.SetupChannel (DMAMode.WriteToMemory, DMATransferType.Single, false, count * FDC.BytesPerSector);

					FloppyIRQ.ClearInterrupt ();

					if (operation == SectorOperation.Write)
						SendByte (FIFOCommand.WriteSector | FIFOCommand.MFMModeMask);
					else
						SendByte (FIFOCommand.ReadSector | FIFOCommand.MFMModeMask);

					SendByte ((byte)((drive) | (head << 2)));	// 0:0:0:0:0:HD:US1:US0 = head and drive
					SendByte (track);// C: 
					SendByte (head);	// H: first head (should match with above)
					SendByte ((byte)(sector + 1));	// R: first sector, strangely counts from 1
					SendByte (2);	// N: bytes/sector, 128*2^x (x=2 -> 512) 
					SendByte ((byte)(sector + count)); // EOT
					SendByte (floppyMedia[drive].Gap1Length);	// GPL: GAP3 length, 27 is default for 3.5" 
					SendByte (0xFF);	// DTL: (bytes to transfer) = unused

					if (!FloppyIRQ.WaitForInterrupt (3000))
						error = 3;

					byte st0 = GetByte ();
					byte st1 = GetByte ();
					byte st2 = GetByte ();

					byte trk = GetByte ();	// track (cylinder)
					byte rhe = GetByte ();	// head
					byte sec = GetByte ();	// sector number
					byte bps = GetByte ();	// bytes per sector

					//Diagnostics.Message("ST0: ", (int)st0);
					//Diagnostics.Message("ST1: ", (int)st1);
					//Diagnostics.Message("ST2: ", (int)st2);
					//Diagnostics.Message("TRK: ", (int)trk);
					//Diagnostics.Message("RHE: ", (int)rhe);
					//Diagnostics.Message("SEC: ", (int)sec);
					//Diagnostics.Message("BPS: ", (int)bps);

					if ((st0 & 0xC0) != 0x0) {
						if (verbose) Diagnostics.Message ("FloppyDiskController: error");
						error = 1;
					}
					if (trk != track + 1) {
						if (verbose) Diagnostics.Message ("FloppyDiskController: wrong track: ", trk - 1);
						error = 1;
					}
					if (rhe != head) {
						if (verbose) Diagnostics.Message ("FloppyDiskController: wrong track: ", trk - 1);
						error = 1;
					}
					if ((st1 & 0x80) != 0x0) {
						if (verbose) Diagnostics.Message ("FloppyDiskController: end of cylinder");
						error = 1;
					}
					if ((st0 & 0x08) != 0x0) {
						if (verbose) Diagnostics.Message ("FloppyDiskController: drive not ready");
						error = 1;
					}
					if ((st1 & 0x20) != 0x0) {
						if (verbose) Diagnostics.Message ("FloppyDiskController: CRC error");
						error = 1;
					}
					if ((st1 & 0x10) != 0x0) {
						if (verbose) Diagnostics.Message ("FloppyDiskController: controller timeout");
						error = 1;
					}
					if ((st1 & 0x04) != 0x0) {
						if (verbose) Diagnostics.Message ("FloppyDiskController: no data found");
						error = 1;
					}
					if (((st1 | st2) & 0x01) != 0x0) {
						if (verbose) Diagnostics.Message ("FloppyDiskController: no address mark found");
						error = 1;
					}
					if ((st2 & 0x40) != 0x0) {
						if (verbose) Diagnostics.Message ("FloppyDiskController: deleted address mark");
						error = 1;
					}
					if ((st2 & 0x20) != 0x0) {
						if (verbose) Diagnostics.Message ("FloppyDiskController: CRC error in data");
						error = 1;
					}
					if ((st2 & 0x10) != 0x0) {
						if (verbose) Diagnostics.Message ("FloppyDiskController: wrong cylinder");
						error = 1;
					}
					if ((st2 & 0x04) != 0x0) {
						if (verbose) Diagnostics.Message ("FloppyDiskController: sector not found");
						error = 1;
					}
					if ((st2 & 0x02) != 0x0) {
						if (verbose) Diagnostics.Message ("FloppyDiskController: bad cylinder");
						error = 1;
					}
					if (bps != 0x02) {
						if (verbose) Diagnostics.Message ("FloppyDiskController: wanted 512B/sector, got something else: ", (int)bps);
						error = 1;
					}
					if ((st1 & 0x02) != 0x0) {
						if (verbose) Diagnostics.Message ("FloppyDiskController: not writable");
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
						if (FloppyDMA.TransferOut (memory, count * FDC.BytesPerSector))
							return true;

					return false;
				}

				lastSeek[drive].calibrated = false;	// will force recalibration

				if (error > 1) {
					if (verbose) Diagnostics.Message ("FloppyDiskController: not retrying..");
					TurnOffMotor (drive);
					break;
				}

			}

			return false;
		}

	}
}
