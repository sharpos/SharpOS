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
// http://www.t13.org/Documents/UploadedDocuments/docs2004/d1572r3-EDD3.pdf
// http://www.osdever.net/tutorials/lba.php
// http://www.nondot.org/sabre/os/files/Disk/IDE-tech.html

using System;
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.DriverSystem;

namespace SharpOS.Kernel.DriverSystem.Drivers.Block
{
	public class IDEDiskDriver : GenericDriver, IBlockDeviceController
	{

		#region Definations

		internal struct IDECommand
		{
			internal const byte ReadSectorsWithRetry = 0x20;
			internal const byte WriteSectorsWithRetry = 0x30;
			internal const byte IdentifyDrive = 0xEC;
		};


		internal struct IdentifyDrive
		{
			internal const uint GeneralConfig = 0x00;
			internal const uint LogicalCylinders = 0x02;
			internal const uint LogicalHeads = 0x08;
			internal const uint LogicalSectors = 0x06 * 2;
			internal const uint SerialNbr = 0x14;
			internal const uint ControllerType = 0x28;
			internal const uint BufferSize = 0x15 * 2;
			internal const uint FirmwareRevision = 0x17 * 2;
			internal const uint ModelNumber = 0x1B * 2;
			internal const uint SupportDoubleWord = 0x30 * 2;

			internal const uint CommandSetSupported83 = 83 * 2; // 1 word
			internal const uint MaxLBA28 = 60 * 2; // 2 words
			internal const uint MaxLBA48 = 100 * 2;	// 3 words
		}

		#endregion

		protected SpinLock spinLock;

		public const uint DrivesPerConroller = 2; // the maximum supported

		protected IOPortStream DataPort;
		protected IOPortStream FeaturePort;
		protected IOPortStream ErrorPort;
		protected IOPortStream SectorCountPort;
		protected IOPortStream LBALowPort;
		protected IOPortStream LBAMidPort;
		protected IOPortStream LBAHighPort;
		protected IOPortStream DeviceHeadPort;
		protected IOPortStream StatusPort;
		protected IOPortStream CommandPort;

		protected IRQHandler IdeIRQ;
		protected bool verbose;

		public enum LBAType
		{
			LBA28,
			LBA48
		}

		protected struct DriveInfo
		{
			public bool Present;
			public uint MaxLBA;

			public LBAType LBAType;

			// legacy info
			public uint Cylinders;
			public uint Heads;
			public uint SectorsPerTrack;
		}

		protected static DriveInfo[] driveInfo;
		protected static String[] driveNames;
		protected string deviceName;

		protected const ushort IOBase = 0x1F0; // Secondary is at 0x170

		public override bool Initialize (IDriverContext context)
		{
			spinLock.Enter ();

			deviceName = "ide1";

			context.Initialize ();

			DataPort = context.CreateIOPortStream (IOBase);
			ErrorPort = context.CreateIOPortStream (IOBase, 1);
			FeaturePort = context.CreateIOPortStream (IOBase, 1);
			SectorCountPort = context.CreateIOPortStream (IOBase, 2);

			LBALowPort = context.CreateIOPortStream (IOBase, 3);
			LBAMidPort = context.CreateIOPortStream (IOBase, 4);
			LBAHighPort = context.CreateIOPortStream (IOBase, 5);

			DeviceHeadPort = context.CreateIOPortStream (IOBase, 6);
			CommandPort = context.CreateIOPortStream (IOBase, 7);
			StatusPort = context.CreateIOPortStream (IOBase, 7);

			//IdeIRQ = context.CreateIRQHandler (14);

			driveInfo = new DriveInfo[DrivesPerConroller];
			driveNames = new string[DrivesPerConroller];
			driveNames[0] = "hd1";
			driveNames[1] = "hd2";

			for (int drive = 0; drive < DrivesPerConroller; drive++) {
				driveInfo[0].Present = false;
				driveInfo[0].MaxLBA = 0;
			}

			verbose = true;

			LBALowPort.Write8 (0x88);

			TextMode.Write (deviceName);
			TextMode.Write (": ");

			if (LBALowPort.Read8 () != 0x88) {
				TextMode.WriteLine ("controller not found");
				return false;
			}

			TextMode.WriteLine ("controller found at 0x1F0");

			DeviceHeadPort.Write8 (0xA0);

			Timer.Delay (1000 / 250); // wait 1/250th of a second

			if ((StatusPort.Read16 () & 0x40) == 0x40)
				driveInfo[0].Present = true;

			DeviceHeadPort.Write8 (0xB0);

			Timer.Delay (1000 / 250); // wait 1/250th of a second

			if ((StatusPort.Read16 () & 0x40) == 0x40)
				driveInfo[1].Present = true;

			DeviceController deviceController = new DeviceController (deviceName, 0);

			for (uint drive = 0; drive < DrivesPerConroller; drive++) {
				if (driveInfo[drive].Present) {
					Open (drive);

					//TextMode.Write (driveNames[drive]);
					TextMode.Write ("Disk #");
					TextMode.Write ((int)drive);
					TextMode.Write (" - ", (int)(driveInfo[drive].MaxLBA / 1024 / 2));
					TextMode.Write ("MB, lba=", (int)driveInfo[drive].MaxLBA);

					TextMode.WriteLine ("");
					//TextMode.WriteLine ("    Cylinders: ", (int)driveInfo[drive].Cylinders);
					//TextMode.WriteLine ("    Heads: ", (int)driveInfo[drive].Heads);
					//TextMode.WriteLine ("    Sectors Per Track: ", (int)driveInfo[drive].SectorsPerTrack);

					//Timer.Delay (1000 * 5);

					//DeviceResource resource = new DeviceResource (driveNames[drive], DeviceResourceType.HardDrive, new GenericBlockDeviceAdapter (this, drive), DeviceResourceStatus.Online, 0, drive);
					//DeviceResourceManager.Add (resource);

					deviceController.AddDisk (new GenericBlockDeviceAdapter (this, drive));

					// test
					GenericBlockDeviceAdapter lGenericDisk = new GenericBlockDeviceAdapter (this, drive);
					SharpOS.Kernel.FileSystem.GenericDisk lDisk = new SharpOS.Kernel.FileSystem.GenericDisk (lGenericDisk);
					lDisk.ReadMasterBootBlock ();
				}
			}

			DeviceControllers.Add (deviceController);

			isInitialized = true;

			return true;
		}

		protected bool WaitForReqisterReady ()
		{
			while (true) {
				uint status = StatusPort.Read8 ();

				if ((status & 0x08) == 0x08)
					return true;

				//TODO: add timeout check
			}

			//return false;
		}

		protected enum SectorOperation
		{
			Read,
			Write
		}

		protected bool ReadLBA28 (SectorOperation operation, uint drive, uint lba, MemoryBlock memory)
		{
			FeaturePort.Write8 (0);
			SectorCountPort.Write8 (1);

			LBALowPort.Write8 ((byte)(lba & 0xFF));
			LBAMidPort.Write8 ((byte)((lba >> 8) & 0xFF));
			LBAHighPort.Write8 ((byte)((lba >> 16) & 0xFF));

			DeviceHeadPort.Write8 ((byte)(0xE0 | (drive << 4) | ((lba >> 24) & 0x0F)));

			if (operation == SectorOperation.Write)
				CommandPort.Write8 (IDECommand.WriteSectorsWithRetry);
			else
				CommandPort.Write8 (IDECommand.ReadSectorsWithRetry);

			if (!WaitForReqisterReady ())
				return false;

			//TODO: Don't use PIO
			if (operation == SectorOperation.Read) {
				for (uint index = 0; index < 256; index++)
					memory.SetUShort (index * 2, DataPort.Read16 ());
			}
			else {
				for (uint index = 0; index < 256; index++)
					DataPort.Write16 (memory.GetUShort (index * 2));
			}

			return true;
		}

		// doesn't work since uint should be uint64
		//protected bool ReadLBA48 (SectorOperation operation, uint drive, uint lba, MemoryBlock memory)
		//{
		//    FeaturePort.Write8 (0);
		//    FeaturePort.Write8 (0);

		//    SectorCountPort.Write8 (0);
		//    SectorCountPort.Write8 (1);

		//    LBALowPort.Write8 ((byte)((lba >> 24) & 0xFF));
		//    LBALowPort.Write8 ((byte)(lba & 0xFF));

		//    LBAMidPort.Write8 ((byte)((lba >> 32) & 0xFF));
		//    LBAMidPort.Write8 ((byte)((lba >> 8) & 0xFF));

		//    LBAHighPort.Write8 ((byte)((lba >> 40) & 0xFF));
		//    LBAHighPort.Write8 ((byte)((lba >> 16) & 0xFF));

		//    DeviceHeadPort.Write8 ((byte)(0x40 | (drive << 4)));

		//    if (operation == SectorOperation.Write)
		//        CommandPort.Write8 (0x34);
		//    else
		//        CommandPort.Write8 (0x24);

		//    if (!WaitForReqisterReady ())
		//        return false;

		//    //TODO: Don't use PIO
		//    if (operation == SectorOperation.Read) {
		//        for (uint index = 0; index < 256; index++)
		//            memory.SetUShort (index * 2, DataPort.Read16 ());
		//    }
		//    else {
		//        for (uint index = 0; index < 256; index++)
		//            DataPort.WriteUShort (memory.GetUShort (index * 2));
		//    }

		//    return true;
		//}

		public int Open (uint drive)
		{
			if (drive == 0)
				DeviceHeadPort.Write8 (0xA0);
			else
				if (drive == 1)
					DeviceHeadPort.Write8 (0xB0);
				else
					return -1;

			CommandPort.Write8 (IDECommand.IdentifyDrive);

			if (!WaitForReqisterReady ())
				return -1;

			MemoryBlock info = new MemoryBlock (512);

			for (uint index = 0; index < 256; index++)
				info.SetUShort (index * 2, DataPort.Read16 ());

			driveInfo[drive].MaxLBA = info.GetUInt (IdentifyDrive.MaxLBA28);

			// legacy info
			driveInfo[drive].Heads = info.GetUShort (IdentifyDrive.LogicalHeads);
			driveInfo[drive].Cylinders = info.GetUShort (IdentifyDrive.LogicalCylinders);
			driveInfo[drive].SectorsPerTrack = info.GetUShort (IdentifyDrive.LogicalSectors);

			info.Release ();

			return 0;
		}

		public int Release (uint drive)
		{
			return 0;
		}

		public uint GetSectorSize (uint drive)
		{
			return 512;
		}

		public uint GetTotalSectors (uint drive)
		{
			return driveInfo[drive].MaxLBA;
		}

		public bool CanWrite (uint drive)
		{

			return false;

		}

		public IDevice GetDeviceDriver ()
		{
			return (IDevice)this;
		}

		public bool ReadBlock (uint drive, uint lba, uint count, MemoryBlock memory)
		{
			try {
				spinLock.Enter ();
				for (uint index = 0; index < count; index++) {
					if (!ReadLBA28 (SectorOperation.Read, drive, lba, memory.Offset (index * 512)))
						return false;
				}
				return true;
			}
			finally {
				spinLock.Exit ();
			}
		}

		public bool WriteBlock (uint drive, uint lba, uint count, MemoryBlock memory)
		{
			try {
				spinLock.Enter ();
				for (uint index = 0; index < count; index++) {
					if (!ReadLBA28 (SectorOperation.Write, drive, lba, memory.Offset (index * 512)))
						return false;
				}
				return true;
			}
			finally {
				spinLock.Exit ();
			}
		}


	}
}
