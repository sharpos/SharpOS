// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//  Phil Garcia (aka tgiphil) <phil@thinkedge.com>
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
using SharpOS.Kernel.ADC;	// for spin lock
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.DeviceSystem;
using SharpOS.Kernel.HAL;
using SharpOS.Kernel.FileSystem;

namespace SharpOS.Kernel.DeviceSystem.DiskController
{
	public class FloppyDiskDriver : HardwareDevice, IDiskControllerDevice
	{

		#region Definitions

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
			internal const uint driveSelectMask = 0x03;
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
			public uint drive;
			public byte track;
			public byte head;
		}

		protected unsafe struct TrackCache
		{
			public byte[] buffer;
			public bool valid;
			public byte track;
			public byte head;
		}

		protected SpinLock spinLock;
		protected bool enchancedController = false;

		public const uint DrivesPerController = 2; // the maximum supported

		protected FloppyDriveInfo[] floppyDrives;
		protected FloppyMediaInfo[] floppyMedia;

		protected TrackCache[] trackCache;
		protected LastSeek[] lastSeek;

		protected IReadWriteIOPort ControllerCommands;
		protected IReadWriteIOPort DataPort;
		protected IReadWriteIOPort ConfigPort;
		protected IReadWriteIOPort StatusPort;

		protected IReadWriteIOPort CMOSComand;
		protected IReadWriteIOPort CMOSResponse;

		protected IDMAChannel floppyDMA;
		protected IRQHandler floppyIRQ;
		protected bool verbose;

		protected ushort ioBase;

		public const ushort PrimaryIOBase = 0x03F2;
		public const ushort SecondaryIOBase = 0x0372;

		public FloppyDiskDriver ()
			: this (PrimaryIOBase)
		{
		}

		public FloppyDiskDriver (ushort ioBase)
		{
			base.name = "FDC_0x" + ioBase.ToString ("X");
			base.parent = null; // no parent
			base.deviceStatus = DeviceStatus.Initializing;

			this.ioBase = ioBase;

			floppyDrives = new FloppyDriveInfo[DrivesPerController];
			floppyMedia = new FloppyMediaInfo[DrivesPerController];

			trackCache = new TrackCache[DrivesPerController];
			lastSeek = new LastSeek[DrivesPerController];

			for (int drive = 0; drive < DrivesPerController; drive++) 
				trackCache[drive].buffer = new byte[FDC.MaxBytesPerTrack];

			ControllerCommands = base.CreateIOPort (ioBase);
			StatusPort = base.CreateIOPort (ioBase, 2);
			DataPort = base.CreateIOPort (ioBase, 3);
			ConfigPort = base.CreateIOPort (ioBase, 5);

			CMOSComand = base.CreateIOPort (0x70);
			CMOSResponse = base.CreateIOPort (0x71);

			floppyDMA = base.CreateDMAChannel (2);
			floppyIRQ = base.CreateIRQHandler (6);

			this.Initialize ();
		}

		public void Initialize ()
		{
			try {
				spinLock.Enter ();

				for (int drive = 0; drive < DrivesPerController; drive++) {
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

				ResetController ();

				SendByte (FIFOCommand.Version);

				if (GetByte () == 0x80)
					enchancedController = false;
				else
					enchancedController = true;

				TextMode.Write (base.name);
				TextMode.Write (": ");

				if (enchancedController)
					TextMode.WriteLine ("Enhanced floppy disk controller found at 0x" + ((uint)ioBase).ToString ("X"));
				else
					TextMode.WriteLine ("Non-enhanced floppy disk controller found at 0x" + ((uint)ioBase).ToString ("X"));

				DetectDrives ();

				for (uint drive = 0; drive < DrivesPerController; drive++) {
					if (floppyDrives[drive].Type != FloppyDriveType.None) {
						Open (drive);

						TextMode.Write (base.name);
						TextMode.Write (": Disk #");
						TextMode.Write ((int)drive);
						TextMode.Write (" - ", (int)floppyDrives[drive].KiloByteSize);
						TextMode.WriteLine ("KB, media sector/track=", (int)floppyMedia[drive].SectorsPerTrack);

						DeviceManager.Add (new DiskDevice (this, drive, false));

						//Timer.Delay (1000 * 5);
						//DeviceResource resource = new DeviceResource (driveNames[drive], DeviceResourceType.FloppyDisk, new GenericBlockDeviceAdapter (this, drive), DeviceResourceStatus.Online, 0, drive);
						//DeviceResourceManager.Add (resource);
					}
				}

				base.deviceStatus = DeviceStatus.Online;
			}
			finally {
				spinLock.Exit ();
			}
		}

		public bool Open (uint driveNbr)
		{
			// clear it
			floppyMedia[driveNbr].TotalTracks = 0;
			floppyMedia[driveNbr].SectorsPerTrack = 0;

			byte[] temp = new byte[FDC.BytesPerSector];
			spinLock.Enter ();
			verbose = false;	// surpress failures messages

			try {
				//TODO: check drive type first

				// attempt to read 2.88MB/2880KB
				floppyMedia[driveNbr].SectorsPerTrack = 36;
				floppyMedia[driveNbr].TotalTracks = 80;
				floppyMedia[driveNbr].Gap1Length = 0x1B;
				floppyMedia[driveNbr].Gap2Length = 0x54;

				if (ReadBlock (driveNbr, CHSToLBA (driveNbr, floppyMedia[driveNbr].TotalTracks - 1, 0, floppyMedia[driveNbr].SectorsPerTrack - 1), 1, temp))
					return true;

				// attempt to read 1.64MB/1680KB (DMF)
				floppyMedia[driveNbr].SectorsPerTrack = 21;
				floppyMedia[driveNbr].TotalTracks = 80;
				floppyMedia[driveNbr].Gap1Length = 0x0C;
				floppyMedia[driveNbr].Gap2Length = 0x1C;

				if (ReadBlock (driveNbr, CHSToLBA (driveNbr, floppyMedia[driveNbr].TotalTracks - 1, 0, floppyMedia[driveNbr].SectorsPerTrack - 1), 1, temp))
					return true;

				// attempt to read 1.44MB
				floppyMedia[driveNbr].TotalTracks = 80;
				floppyMedia[driveNbr].SectorsPerTrack = 18;
				floppyMedia[driveNbr].Gap1Length = 0x1B;
				floppyMedia[driveNbr].Gap2Length = 0x54;

				if (ReadBlock (driveNbr, CHSToLBA (driveNbr, floppyMedia[driveNbr].TotalTracks - 1, 0, floppyMedia[driveNbr].SectorsPerTrack - 1), 1, temp))
					return true;

				// attempt to read 720Kb
				floppyMedia[driveNbr].TotalTracks = 80;
				floppyMedia[driveNbr].SectorsPerTrack = 9;
				floppyMedia[driveNbr].Gap1Length = 0x1B;
				floppyMedia[driveNbr].Gap2Length = 0x54;

				if (ReadBlock (driveNbr, CHSToLBA (driveNbr, floppyMedia[driveNbr].TotalTracks - 1, 0, floppyMedia[driveNbr].SectorsPerTrack - 1), 1, temp))
					return true;

				// unable to read floppy media 
				floppyMedia[driveNbr].TotalTracks = 0;
				floppyMedia[driveNbr].SectorsPerTrack = 0;

				return false;
			}
			finally {
				spinLock.Exit ();
				verbose = true;
			}

		}

		public bool Release (uint driveNbr)
		{
			return true;
		}

		public uint GetSectorSize (uint driveNbr)
		{
			return 512;
		}

		public uint GetTotalSectors (uint driveNbr)
		{
			return floppyMedia[driveNbr].SectorsPerTrack * floppyMedia[driveNbr].TotalTracks * 2;
		}

		public bool CanWrite (uint driveNbr)
		{
			if (floppyMedia[driveNbr].SectorsPerTrack == 0)
				return false;

			return true;
		}

		protected bool WaitForReqisterReady ()
		{
			// wait for RQM data register to be ready
			while (true) {
				uint status = StatusPort.Read8 ();

				if ((status & 0x80) == 0x80)
					return true;

				//TODO: add timeout check
			}

			//return false;
		}

		protected void SendByte (byte command)
		{
			WaitForReqisterReady ();
			DataPort.Write8 (command);
		}

		protected byte GetByte ()
		{
			WaitForReqisterReady ();
			return DataPort.Read8 ();
		}

		protected void ResetController ()
		{
			floppyIRQ.ClearInterrupt ();

			ControllerCommands.Write8 (DORFlags.ResetController);

			Timer.Delay (200);	// 20 msec

			ControllerCommands.Write8 (DORFlags.EnableController);

			floppyIRQ.WaitForInterrupt (3000);

			ControllerCommands.Write8 (DORFlags.EnableController | DORFlags.EnableDMA);

			SendByte (FIFOCommand.SenseInterrupt);
			GetByte ();
			GetByte ();

			ConfigPort.Write8 (0x00); // 500 Kb/s (MFM)			

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
			CMOSComand.Write8 (0x10);
			byte types = CMOSResponse.Read8 ();

			floppyDrives[0] = DetermineByType ((byte)(types >> 4));
			floppyDrives[1] = DetermineByType ((byte)(types & 0xF));
		}

		protected void TurnOffMotor (uint drive)
		{
			//TextMode.WriteLine("..Motor Off");
			ControllerCommands.Write8 ((byte)(DORFlags.EnableDMA | DORFlags.EnableController | drive));
		}

		protected void TurnOnMotor (uint drive)
		{
			byte reg = ControllerCommands.Read8 ();
			byte bits = (byte)(DORFlags.MotorShift << (byte)drive | DORFlags.EnableDMA | DORFlags.EnableController | (byte)drive);

			if (reg != bits) {
				///TextMode.WriteLine("..Motor On");
				ControllerCommands.Write8 (bits);
				Timer.Delay (500);	// 500 msec
			}
		}

		protected bool Recalibrate (uint drive)
		{
			//TextMode.WriteLine("..Recalibrating");

			lastSeek[drive].calibrated = false;

			for (int i = 0; i < 5; i++) {
				TurnOnMotor (drive);

				floppyIRQ.ClearInterrupt ();

				SendByte (FIFOCommand.Recalibrate);
				SendByte ((byte)drive);

				floppyIRQ.WaitForInterrupt (3000);

				SendByte (FIFOCommand.SenseInterrupt);
				byte sr0 = GetByte ();
				byte fdc_track = GetByte ();

				if (((sr0 & 0xC0) == 0x00) && (fdc_track == 0)) {
					lastSeek[drive].calibrated = true;
					lastSeek[drive].track = 0;
					lastSeek[drive].head = 2;	// invalid head (required)
					return true;	// Note: motor is left on				
				}

				//TextMode.WriteLine("...Retrying");
			}

			if (verbose) TextMode.WriteLine ("Recalibrating failed");
			TurnOffMotor (drive);

			return false;
		}

		protected bool Seek (uint drive, byte track, byte head)
		{
			TurnOnMotor (drive);

			if (!lastSeek[drive].calibrated)
				if (!Recalibrate (drive))
					return false;

			if ((lastSeek[drive].calibrated) && (lastSeek[drive].track == track) && (lastSeek[drive].head == head))
				return true;

			//TextMode.WriteLine("..Seeking #", track);

			for (int i = 0; i < 5; i++) {
				floppyIRQ.ClearInterrupt ();

				lastSeek[drive].calibrated = false;

				SendByte (FIFOCommand.Seek);
				SendByte ((byte)(((byte)drive | (head << 2))));
				SendByte (track);

				if (!floppyIRQ.WaitForInterrupt (3000))
					return false;

				Timer.Delay (20);

				SendByte (FIFOCommand.SenseInterrupt);
				byte sr0 = GetByte ();
				byte trk = GetByte ();

				if ((sr0 == (0x20 + ((byte)drive | (head << 2)))) && (trk == track)) {
					lastSeek[drive].calibrated = true;
					lastSeek[drive].track = track;
					lastSeek[drive].head = head;
					return true;
				}

				//TextMode.WriteLine("...Retrying");
			}

			if (verbose) TextMode.WriteLine ("Seek failed");
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

		public bool ReadBlock (uint driveNbr, uint block, uint count, byte[] data)
		{
			if (data.Length < count * 512)
				return false;

			try {
				spinLock.Enter ();
				for (uint index = 0; index < count; index++) {
					if (!ReadBlock2 (driveNbr, block + index, data, index * FDC.BytesPerSector))
						return false;
				}
				return true;
			}
			finally {
				TurnOffMotor ((byte)driveNbr);	//TODO: create timer to turn off drive motors after 1 sec.
				spinLock.Exit ();
			}
		}

		public bool WriteBlock (uint driveNbr, uint block, uint count, byte[] data)
		{
			if (data.Length < count * 512)
				return false;

			try {
				spinLock.Enter ();
				for (uint index = 0; index < count; index++) {
					if (!WriteBlock2 (driveNbr, block + index, 1, data, index * FDC.BytesPerSector))
						return false;
				}
				return true;
			}
			finally {
				TurnOffMotor ((byte)driveNbr);	//TODO: create timer to turn off drive motors after 1 sec.
				spinLock.Exit ();
			}
		}

		protected bool ReadBlock2 (uint drive, uint lba, byte[] data, uint offset)
		{
			byte track = LBAToTrack (drive, lba);
			byte head = LBAToHead (drive, lba);
			byte sector = LBAToSector (drive, lba);

			if (!((trackCache[drive].valid) && (trackCache[drive].track == track) && (trackCache[drive].head == head))) {
				trackCache[drive].valid = false;

				if (!PerformIO (SectorOperation.Read, drive, 0, track, head, floppyMedia[drive].SectorsPerTrack, trackCache[drive].buffer, 0))
					return false;

				trackCache[drive].valid = true;
				trackCache[drive].head = head;
				trackCache[drive].track = track;
			}

			for (uint i = 0; i < FDC.BytesPerSector; i++)
				data[offset + i] = trackCache[drive].buffer[(sector * FDC.BytesPerSector) + i];

			return true;
		}

		protected bool WriteBlock2 (uint drive, uint lba, uint count, byte[] data, uint offset)
		{
			byte track = LBAToTrack (drive, lba);
			byte head = LBAToHead (drive, lba);
			byte sector = LBAToSector (drive, lba);

			if (sector + count > floppyMedia[drive].SectorsPerTrack)
				return false;

			if ((trackCache[drive].track == track) && (trackCache[drive].head == head)) {
				// updated track cache
				for (uint i = 0; i < count * FDC.BytesPerSector; i++)
					trackCache[drive].buffer[(sector * FDC.BytesPerSector) + i] = data[offset + i];
			}

			if (!PerformIO (SectorOperation.Write, drive, sector, track, head, count, data, offset))
				return false;

			return true;
		}

		protected enum SectorOperation
		{
			Read,
			Write
		}

		protected bool PerformIO (SectorOperation operation, uint drive, byte sector, byte track, byte head, uint count, byte[] data, uint offset)
		{
			//TextMode.WriteLine("Sector: ", (int)sector);
			//TextMode.WriteLine("Track: ", (int)track);
			//TextMode.WriteLine("Head: ", (int)head);

			for (int i = 0; i < 5; i++) {
				int error = 0;

				TurnOnMotor (drive);

				//TODO: Check for disk change

				if (Seek (drive, track, head)) {
					if (operation == SectorOperation.Write) {
						floppyDMA.TransferIn (count * FDC.BytesPerSector, data, offset);
						floppyDMA.SetupChannel (DMAMode.ReadFromMemory, DMATransferType.Single, false, count * FDC.BytesPerSector);
					}
					else
						floppyDMA.SetupChannel (DMAMode.WriteToMemory, DMATransferType.Single, false, count * FDC.BytesPerSector);

					floppyIRQ.ClearInterrupt ();

					if (operation == SectorOperation.Write)
						SendByte (FIFOCommand.WriteSector | FIFOCommand.MFMModeMask);
					else
						SendByte (FIFOCommand.ReadSector | FIFOCommand.MFMModeMask);

					SendByte ((byte)((byte)drive | (head << 2)));	// 0:0:0:0:0:HD:US1:US0 = head and drive
					SendByte (track);// C: 
					SendByte (head);	// H: first head (should match with above)
					SendByte ((byte)(sector + 1));	// R: first sector, strangely counts from 1
					SendByte (2);	// N: bytes/sector, 128*2^x (x=2 -> 512) 
					SendByte ((byte)(sector + count)); // EOT
					SendByte (floppyMedia[drive].Gap1Length);	// GPL: GAP3 length, 27 is default for 3.5" 
					SendByte (0xFF);	// DTL: (bytes to transfer) = unused

					if (!floppyIRQ.WaitForInterrupt (3000))
						error = 3;

					byte st0 = GetByte ();
					byte st1 = GetByte ();
					byte st2 = GetByte ();

					byte trk = GetByte ();	// track (cylinder)
					byte rhe = GetByte ();	// head
					byte sec = GetByte ();	// sector number
					byte bps = GetByte ();	// bytes per sector

					//TextMode.WriteLine("ST0: ", (int)st0);
					//TextMode.WriteLine("ST1: ", (int)st1);
					//TextMode.WriteLine("ST2: ", (int)st2);
					//TextMode.WriteLine("TRK: ", (int)trk);
					//TextMode.WriteLine("RHE: ", (int)rhe);
					//TextMode.WriteLine("SEC: ", (int)sec);
					//TextMode.WriteLine("BPS: ", (int)bps);

					if ((st0 & 0xC0) != 0x0) {
						if (verbose) TextMode.WriteLine ("FloppyDiskController: error");
						error = 1;
					}
					if (trk != track + 1) {
						if (verbose) TextMode.WriteLine ("FloppyDiskController: wrong track: ", trk - 1);
						error = 1;
					}
					if (rhe != head) {
						if (verbose) TextMode.WriteLine ("FloppyDiskController: wrong track: ", trk - 1);
						error = 1;
					}
					if ((st1 & 0x80) != 0x0) {
						if (verbose) TextMode.WriteLine ("FloppyDiskController: end of cylinder");
						error = 1;
					}
					if ((st0 & 0x08) != 0x0) {
						if (verbose) TextMode.WriteLine ("FloppyDiskController: drive not ready");
						error = 1;
					}
					if ((st1 & 0x20) != 0x0) {
						if (verbose) TextMode.WriteLine ("FloppyDiskController: CRC error");
						error = 1;
					}
					if ((st1 & 0x10) != 0x0) {
						if (verbose) TextMode.WriteLine ("FloppyDiskController: controller timeout");
						error = 1;
					}
					if ((st1 & 0x04) != 0x0) {
						if (verbose) TextMode.WriteLine ("FloppyDiskController: no data found");
						error = 1;
					}
					if (((st1 | st2) & 0x01) != 0x0) {
						if (verbose) TextMode.WriteLine ("FloppyDiskController: no address mark found");
						error = 1;
					}
					if ((st2 & 0x40) != 0x0) {
						if (verbose) TextMode.WriteLine ("FloppyDiskController: deleted address mark");
						error = 1;
					}
					if ((st2 & 0x20) != 0x0) {
						if (verbose) TextMode.WriteLine ("FloppyDiskController: CRC error in data");
						error = 1;
					}
					if ((st2 & 0x10) != 0x0) {
						if (verbose) TextMode.WriteLine ("FloppyDiskController: wrong cylinder");
						error = 1;
					}
					if ((st2 & 0x04) != 0x0) {
						if (verbose) TextMode.WriteLine ("FloppyDiskController: sector not found");
						error = 1;
					}
					if ((st2 & 0x02) != 0x0) {
						if (verbose) TextMode.WriteLine ("FloppyDiskController: bad cylinder");
						error = 1;
					}
					if (bps != 0x02) {
						if (verbose) TextMode.WriteLine ("FloppyDiskController: wanted 512B/sector, got something else: ", (int)bps);
						error = 1;
					}
					if ((st1 & 0x02) != 0x0) {
						if (verbose) TextMode.WriteLine ("FloppyDiskController: not writable");
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
						if (floppyDMA.TransferOut (count * FDC.BytesPerSector, data, offset))
							return true;

					return false;
				}

				lastSeek[drive].calibrated = false;	// will force recalibration

				if (error > 1) {
					if (verbose) TextMode.WriteLine ("FloppyDiskController: not retrying..");
					TurnOffMotor (drive);
					break;
				}

			}

			return false;
		}

	}
}
