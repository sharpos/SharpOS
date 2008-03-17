//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.Korlib.Runtime;
using SharpOS.Kernel.DriverSystem;
using SharpOS.Kernel.DriverSystem.Drivers.Block;
using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.FileSystem.FAT
{

	#region Boot Sector

	internal struct BootSector
	{
		internal const uint JumpInstruction = 0x00; // 3 
		internal const uint EOMName = 0x03;	// 8 - "IBM  3.3", "MSDOS5.0", "MSWIN4.1", "FreeDOS"
		internal const uint BytesPerSector = 0x0B;	// 2 - common value 512
		internal const uint SectorsPerCluster = 0x0D;	// 1 - valid 1 to 128
		internal const uint ReservedSectors = 0x0D; // 2 - 1 for FAT12/FAT16, usually 32 for FAT32
		internal const uint FatAllocationTables = 0x10;	// 1 - always 2
		internal const uint MaxRootDirEntries = 0x11; // 2
		internal const uint TotalSectors = 0x13;	// 2
		internal const uint MediaDescriptor = 0x15; // 1
		internal const uint SectorsPerFAT = 0x16; // 2
		internal const uint SectorsPerTrack = 0x18;	// 2
		internal const uint NumberOfHeads = 0x1A;	// 2
		internal const uint HiddenSectors = 0x1C; // 4
		internal const uint FAT32_TotalSectors = 0x20; // 4

		// Extended BIOS Paremeter Block

		internal const uint PhysicalDriveNbr = 0x24; // 1
		internal const uint ReservedCurrentHead = 0x25; // 1
		internal const uint ExtendedBootSignature = 0x26; // 1 // value: 0x29 or 0x28
		internal const uint IDSerialNumber = 0x25; // 4
		internal const uint VolumeLabel = 0x2B; // 11
		internal const uint FATType = 0x36; // 8 - padded with blanks (0x20) "FAT12"; "FAT16"
		internal const uint OSBootCode = 0x3E; // 448 - Operating system boot code
		internal const uint BootSectorSignature = 0x1FE; // 2 - value: 0x55 0xaa

		// Fat32

		internal const uint FAT32_SectorPerFAT = 0x24; // 4
		internal const uint FAT32_Flags = 0x28; // 2
		internal const uint FAT32_Version = 0x2A; // 2
		internal const uint FAT32_ClusterNumberOfRoot = 0x2C; // 2
		internal const uint FAT32_SectorFSInformation = 0x30; // 2
		internal const uint FAT32_SecondBootSector = 0x32; // 2
		internal const uint FAT32_Reserved1 = 0x34; // 12
		internal const uint FAT32_PhysicalDriveNbr = 0x40; // 1
		internal const uint FAT32_Reserved2 = 0x40; // 1
		internal const uint FAT32_ExtendedBootSignature = 0x42; // 1
		internal const uint FAT32_IDSerialNumber = 0x43; // 4
		internal const uint FAT32_VolumeLabel = 0x47; // 2
		internal const uint FAT32_FATType = 0x52; // 2
		internal const uint FAT32_OSBootCode = 0x5A; // 2
	}

	#endregion

	internal struct FSInfo
	{
		internal const uint FSI_LeadSignature = 0x00; // 4 - always 0x41615252
		internal const uint FSI_Reserved1 = 0x04; // 480 - always 0
		internal const uint FSI_StructureSigature = 484; // 4 - always 0x61417272
		internal const uint FSI_FreeCount = 488; // 4
		internal const uint FSI_NextFree = 492; // 4
		internal const uint FSI_Reserved2 = 496; // 4 - always 0
		internal const uint FSI_TrailSignature = 508; // 4 - always 0xAA550000
		internal const uint FSI_TrailSignature2 = 510; // 4 - always 0xAA55
	}

	internal struct Entry
	{
		internal const uint DOSName = 0x00; // 8
		internal const uint DOSExtension = 0x08;	// 3
		internal const uint FileAttributes = 0x0B;	// 1
		internal const uint Reserved = 0x0C;	// 1
		internal const uint CreationTimeFine = 0x0D; // 1
		internal const uint CreationTime = 0x0E; // 2
		internal const uint CreationDate = 0x10; // 2
		internal const uint LastAccessDate = 0x12; // 2
		internal const uint EAIndex = 0x14; // 2
		internal const uint LastModifiedTime = 0x16; // 2
		internal const uint LastModifiedDate = 0x18; // 2
		internal const uint FirstCluster = 0x1A; // 2
		internal const uint FileSize = 0x1C; // 4
	}

	[Flags]
	public enum FileAttributes : byte
	{
		ReadOnly = 0x01,
		Hidden = 0x02,
		System = 0x04,
		VolumeLabel = 0x08,
		SubDirectory = 0x10,
		Archive = 0x20,
		Device = 0x40,
		Unused = 0x80,
		LongFileName = 0x0F
	}

	internal struct FileNameAttribute
	{
		internal const uint LastEntry = 0x00;
		internal const uint Escape = 0x05;	// special msdos hack where 0x05 really means 0xE5 (since 0xE5 was already used for delete
		internal const uint Dot = 0x2E;
		internal const uint Deleted = 0xE5;
	}

	public enum FatType : byte
	{
		FAT12 = 12,
		FAT16 = 16,
		FAT32 = 32
	}

	public class FAT
	{
		// limitations: fat32 and vfat (long files) are not supported
		// plus almost all testing has been against fat12 (not fat16)

		protected uint last;
		protected uint bad;
		protected uint reserved;
		protected FatType type;
		protected uint fatmask;

		protected IBlockDevice device;
		protected uint blocksize;

		public FAT (IBlockDevice blockdevice)
		{
			device = blockdevice;
			blocksize = device.GetBlockSize();
		}

		protected bool validfat = false;
		protected uint bytespersector;
		protected byte sectorspercluster;
		protected byte reservedsectors;
		protected byte nbrfats;
		protected uint rootentries;
		protected uint totalclusters;

		protected uint rootdirsectors;
		protected uint firstdatasector;
		protected uint totalsectors;

		protected uint datasectors;
		protected uint dataareastart;
		protected uint entriespersector;
		protected uint firstrootdirectorysector;

		public bool ReadBootSector ()
		{
			validfat = false;

			if (blocksize != 512)	// only going to work with 512 sector sizes (for now)
				return false;

			MemoryBlock bootsector = new MemoryBlock(512);

			device.ReadBlock(0, 1, bootsector);

			byte bootsignature = bootsector.GetByte(BootSector.ExtendedBootSignature);

			if ((bootsignature != 0x29) && (bootsignature != 0x28))
				return false;

			TextMode.Write("EOM NAME: ");

			for (uint i = 0; i < 8; i++)
				TextMode.WriteChar(bootsector.GetByte(BootSector.EOMName + i));

			TextMode.WriteLine();

			bytespersector = bootsector.GetUShort(BootSector.BytesPerSector);
			sectorspercluster = bootsector.GetByte(BootSector.SectorsPerCluster);
			reservedsectors = bootsector.GetByte(BootSector.ReservedSectors);
			nbrfats = bootsector.GetByte(BootSector.FatAllocationTables);
			rootentries = bootsector.GetUShort(BootSector.MaxRootDirEntries);

			uint sectorsperfat16 = bootsector.GetUShort(BootSector.SectorsPerFAT);
			uint sectorsperfat32 = bootsector.GetUInt(BootSector.FAT32_SectorPerFAT);
			uint totalsectors16 = bootsector.GetUShort(BootSector.TotalSectors);
			uint totalsectors32 = bootsector.GetUInt(BootSector.FAT32_TotalSectors);
			uint sectorsperfat = (sectorsperfat16 != 0) ? sectorsperfat16 : sectorsperfat32;
			uint fatsectors = nbrfats * sectorsperfat;

			rootdirsectors = (((rootentries * 32) + (bytespersector - 1)) / bytespersector);
			firstdatasector = reservedsectors + (nbrfats * sectorsperfat) + rootdirsectors;

			if (totalsectors16 != 0)
				totalsectors = totalsectors16;
			else
				totalsectors = totalsectors32;

			datasectors = totalsectors - (reservedsectors + (nbrfats * sectorsperfat) + rootdirsectors);
			totalclusters = datasectors / sectorspercluster;
			entriespersector = (bytespersector / 32);
			dataareastart = reservedsectors + fatsectors + rootdirsectors;
			firstrootdirectorysector = reservedsectors + fatsectors;

			if (totalclusters < 4085)
				type = FatType.FAT12;
			else if (totalclusters < 65525)
				type = FatType.FAT16;
			else
				type = FatType.FAT32;

			bootsector.Release();

			if (type == FatType.FAT12) {
				reserved = 0xFF0;
				last = 0x0FF8;
				bad = 0x0FF7;
				fatmask = 0xFFFFFFFF;
			}
			else if (type == FatType.FAT16) {
				reserved = 0xFFF0;
				last = 0xFFF8;
				bad = 0xFFF7;
				fatmask = 0xFFFFFFFF;
			}
			else { //  if (type == FatType.FAT32) {
				reserved = 0xFFF0;
				last = 0x0FFFFFF8;
				bad = 0x0FFFFFF7;
				fatmask = 0x0FFFFFFF;
			}

			// some basic checks 

			if ((nbrfats == 0) || (nbrfats > 2))
				validfat = false;
			else if (totalsectors == 0)
				validfat = false;
			else if (sectorsperfat == 0)
				validfat = false;
			else if (!((type == FatType.FAT12) || (type == FatType.FAT16))) // no support for Fat32
				validfat = false;
			else
				validfat = true;

			return validfat;
		}

		public bool FormatBootSector (FatType type, string volumename)
		{
			if (!device.CanWrite())
				return false;

			this.type = type;
			bytespersector = 512;
			totalsectors = device.GetTotalBlocks();
			sectorspercluster = GetSectorsPerClusterByTotalSectors(type, totalsectors);
			nbrfats = 2;

			if (type == FatType.FAT32) {
				reservedsectors = 32;
				rootentries = 0;
			}
			else {
				reservedsectors = 1;
				rootentries = 512;
			}

			rootdirsectors = (((rootentries * 32) + (bytespersector - 1)) / bytespersector);

			uint val1 = totalsectors - (reservedsectors + rootdirsectors);
			uint val2 = (uint)((256 * sectorspercluster) + nbrfats);

			if (type == FatType.FAT32)
				val2 = val2 / 2;

			uint sectorsperfat = (val1 + (val2 - 1)) / val2;

			MemoryBlock bootsector = new MemoryBlock(512);

			bootsector.SetUInt(BootSector.JumpInstruction, 0);
			bootsector.SetString(BootSector.EOMName, "MSWIN4.1");
			bootsector.SetUShort(BootSector.BytesPerSector, (ushort)bytespersector);
			bootsector.SetByte(BootSector.SectorsPerCluster, (byte)sectorspercluster);
			bootsector.SetUShort(BootSector.ReservedSectors, (ushort)reservedsectors);
			bootsector.SetByte(BootSector.FatAllocationTables, nbrfats);
			bootsector.SetUShort(BootSector.MaxRootDirEntries, (ushort)rootentries);

			if (totalsectors > 0xFFFF) {
				bootsector.SetUShort(BootSector.TotalSectors, 0);
				bootsector.SetUInt(BootSector.FAT32_TotalSectors, totalclusters);
			}
			else {
				bootsector.SetUShort(BootSector.TotalSectors, (ushort)totalsectors);
				bootsector.SetUInt(BootSector.FAT32_TotalSectors, 0);
			}

			bootsector.SetByte(BootSector.MediaDescriptor, 0xF8);

			if (type == FatType.FAT32)
				bootsector.SetUShort(BootSector.SectorsPerFAT, 0);
			else
				bootsector.SetUShort(BootSector.SectorsPerFAT, (ushort)sectorsperfat);

			bootsector.SetUShort(BootSector.SectorsPerTrack, 0); ////
			bootsector.SetUInt(BootSector.HiddenSectors, 0);
			bootsector.SetByte(BootSector.PhysicalDriveNbr, 0x80);
			bootsector.SetByte(BootSector.ReservedCurrentHead, 0);
			bootsector.SetByte(BootSector.ExtendedBootSignature, 0x29);
			bootsector.SetUInt(BootSector.IDSerialNumber, 0); ////
			bootsector.SetString(BootSector.VolumeLabel, "            ");  // 12 blank spaces
			bootsector.SetString(BootSector.VolumeLabel, volumename);  // 12 blank spaces

			if (type == FatType.FAT12)
				bootsector.SetString(BootSector.FATType, "FAT12   ");
			else if (type == FatType.FAT16)
				bootsector.SetString(BootSector.FATType, "FAT16   ");
			else // if (type == FatType.FAT32)
				bootsector.SetString(BootSector.FATType, "FAT32   ");

			//BootSector.OSBootCode
			bootsector.SetUShort(BootSector.BootSectorSignature, 0x55AA);

			if (type == FatType.FAT32) {
				bootsector.SetUInt(BootSector.FAT32_SectorPerFAT, sectorsperfat);
				bootsector.SetByte(BootSector.FAT32_Flags, 0);
				bootsector.SetUShort(BootSector.FAT32_Version, 0);
				bootsector.SetUInt(BootSector.FAT32_ClusterNumberOfRoot, 2);
				bootsector.SetUShort(BootSector.FAT32_SectorFSInformation, 1);
				bootsector.SetUShort(BootSector.FAT32_SecondBootSector, 6);
				//FAT32_Reserved1
				bootsector.SetByte(BootSector.FAT32_PhysicalDriveNbr, 0x80);
				bootsector.SetByte(BootSector.FAT32_Reserved2, 0);
				bootsector.SetByte(BootSector.FAT32_ExtendedBootSignature, 0x29);
				bootsector.SetUInt(BootSector.FAT32_IDSerialNumber, 0); ////
				bootsector.SetString(BootSector.FAT32_VolumeLabel, "            ");  // 12 blank spaces
				bootsector.SetString(BootSector.FAT32_VolumeLabel, volumename);  // 12 blank spaces
				bootsector.SetString(BootSector.FAT32_FATType, "FAT32   ");
			}

			// Write Boot Sector
			device.WriteBlock(0, 1, bootsector);

			// Write backup Boot Sector
			if (type == FatType.FAT32) {
				device.WriteBlock(0, 1, bootsector);
			}

			// create FSInfo Structure
			if (type == FatType.FAT32) {
				MemoryBlock fsinfosector = new MemoryBlock(512);

				fsinfosector.SetUInt(FSInfo.FSI_LeadSignature, 0x41615252);
				//FSInfo.FSI_Reserved1
				fsinfosector.SetUInt(FSInfo.FSI_StructureSigature, 0x61417272);
				fsinfosector.SetUInt(FSInfo.FSI_FreeCount, 0xFFFFFFFF);
				fsinfosector.SetUInt(FSInfo.FSI_NextFree, 0xFFFFFFFF);
				//FSInfo.FSI_Reserved2
				bootsector.SetUInt(FSInfo.FSI_TrailSignature, 0xAA550000);

				device.WriteBlock(1, 1, fsinfosector);
				device.WriteBlock(7, 1, fsinfosector);

				// create 2nd sector
				MemoryBlock secondsector = new MemoryBlock(512);

				secondsector.SetUInt((ushort)FSInfo.FSI_TrailSignature2, 0xAA55);

				device.WriteBlock(2, 1, fsinfosector);
				device.WriteBlock(8, 1, fsinfosector);
			}

			// create fats
			//TODO

			return true;
		}

		protected bool IsClusterFree (uint cluster)
		{
			return ((cluster & fatmask) == 0x00);
		}

		protected bool IsClusterReserved (uint cluster)
		{
			return (((cluster & fatmask) == 0x00) || ((cluster & fatmask) >= reserved) && ((cluster & fatmask) < bad));
		}

		protected bool IsClusterBad (uint cluster)
		{
			return ((cluster & fatmask) == bad);
		}

		protected bool IsClusterLast (uint cluster)
		{
			return ((cluster & fatmask) >= last);
		}

		protected bool IsUsed (uint cluster)
		{
			return !((IsClusterLast(cluster)) || (IsClusterBad(cluster)) || (IsClusterFree(cluster)) || (IsClusterReserved(cluster)));
		}

		protected uint GetClusterBySector (uint sector)
		{
			if (sector < dataareastart)
				return 0;

			return (sector - dataareastart) / sectorspercluster;
		}

		protected uint ClusterToFirstSector (uint cluster)
		{
			return ((cluster - 2) * sectorspercluster) + firstdatasector;
		}

		protected uint GetClusterEntryValue (uint cluster)
		{
			uint fatoffset = 0;

			if (type == FatType.FAT12)
				fatoffset = (cluster + (cluster / 2));
			else if (type == FatType.FAT16)
				fatoffset = cluster * 2;
			else //if (type == FatType.FAT32)
				fatoffset = cluster * 4;

			uint sector = reservedsectors + (fatoffset / bytespersector);
			uint sectoroffset = fatoffset % bytespersector;
			uint nbrsectors = 1;

			if ((type == FatType.FAT12) && (sectoroffset == bytespersector - 1))
				nbrsectors = 2;

			MemoryBlock fat = new MemoryBlock(512);

			device.ReadBlock(sector, nbrsectors, fat);			

			uint clustervalue;

			if (type == FatType.FAT12) {
				clustervalue = fat.GetUShort(sectoroffset);
				if (cluster % 2 == 1)
					clustervalue = clustervalue >> 4;
				else
					clustervalue = clustervalue & 0x0fff;
			}
			else if (type == FatType.FAT16)
				clustervalue = fat.GetUShort(sectoroffset);
			else //if (type == FatType.FAT32)
				clustervalue = fat.GetUInt(sectoroffset) & 0x0fffffff;

			fat.Release();

			return clustervalue;
		}

		public static byte GetSectorsPerClusterByTotalSectors (FatType type, uint sectors)
		{
			switch (type) {
				case FatType.FAT12: {
						if (sectors < 512) return 1;
						else if (sectors == 720) return 2;
						else if (sectors == 1440) return 2;
						else if (sectors <= 2880) return 1;
						else if (sectors <= 5760) return 2;
						else if (sectors <= 16384) return 4;
						else if (sectors <= 32768) return 8;
						else return 0;
					}
				case FatType.FAT16: {
						if (sectors < 8400) return 0;
						else if (sectors < 32680) return 2;
						else if (sectors < 262144) return 4;
						else if (sectors < 524288) return 8;
						else if (sectors < 1048576) return 16;
						else if (sectors < 2097152) return 32;
						else if (sectors < 4194304) return 64;
						else return 0;
					}
				case FatType.FAT32: {
						if (sectors < 66600) return 0;
						else if (sectors < 532480) return 1;
						else if (sectors < 16777216) return 8;
						else if (sectors < 33554432) return 16;
						else if (sectors < 67108864) return 32;
						else return 64;
					}
				default: return 0;
			}
		}

		protected char[] ExtractFileName (MemoryBlock directory, uint index)
		{
			uint offset = index * 32;
			char[] name = new char[12];

			for (uint i = 0; i < 8; i++)
				name[i] = (char)directory.GetByte(i + offset + Entry.DOSName);

			int len = 8;

			for (int i = 7; i > 0; i--)
				if (name[i] == ' ')
					len--;
				else
					break;

			// special case where real character is same as the delete
			if ((len >= 1) && (name[0] == (char)FileNameAttribute.Escape))
				name[0] = (char)FileNameAttribute.Deleted;

			name[len] = '.';

			len++;

			for (uint i = 0; i < 3; i++)
				name[len + i] = (char)directory.GetByte(i + offset + Entry.DOSExtension);

			len = len + 3;

			int spaces = 0;
			for (int i = len - 1; i >= 0; i--)
				if (name[i] == ' ')
					spaces++;
				else
					break;

			if (spaces == 3)
				spaces = 4;

			len = len - spaces;

			// put into a exact size char[] (since string is not imlemented yet)
			char[] fullname = new char[len];

			for (int i = 0; i < len; i++)
				fullname[i] = name[i];

			Runtime.Free(name);

			return fullname;
		}

		protected OpenFile ExtractFileInformation (MemoryBlock directory, uint index, OpenFile parent)
		{
			uint offset = index * 32;

			byte first = directory.GetByte(offset + Entry.DOSName);

			if ((first == FileNameAttribute.LastEntry) || (first == FileNameAttribute.Deleted))
				return null;

			FileAttributes attribute = (FileAttributes)directory.GetByte(offset + Entry.FileAttributes);

			if (attribute == FileAttributes.LongFileName)
				return null;	// long file names are not supported

			byte second = directory.GetByte(offset + Entry.DOSName);

			if ((first == FileNameAttribute.Dot) && (first == FileNameAttribute.Dot))
				return null;

			OpenFile file = new OpenFile();

			if ((attribute & FileAttributes.SubDirectory) == FileAttributes.SubDirectory)
				file.Type = FileType.Directory;
			else
				file.Type = FileType.File;

			file.ReadOnly = ((attribute & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);
			file.Hidden = ((attribute & FileAttributes.Hidden) == FileAttributes.Hidden);
			file.Archive = ((attribute & FileAttributes.Archive) == FileAttributes.Archive);
			file.System = ((attribute & FileAttributes.System) == FileAttributes.System);
			file.Size = directory.GetUInt(offset + Entry.FileSize);

			//TODO: build file name name.Trim()+'.'+ext.Trim()
			//string name = ByteBuffer.GetString(directory, 8, offset + Entry.DOSName);
			//string ext = ByteBuffer.GetString(directory, 3, offset + Entry.DOSExtension);

			file.Name = ExtractFileName(directory, index);
			ushort cdate = directory.GetUShort(offset + Entry.CreationDate);
			ushort ctime = directory.GetUShort(offset + Entry.CreationTime);
			ushort mtime = directory.GetUShort(offset + Entry.LastModifiedTime);
			ushort mdate = directory.GetUShort(offset + Entry.LastModifiedDate);
			ushort adate = directory.GetUShort(offset + Entry.LastAccessDate);
			ushort msec = (ushort)(directory.GetByte(offset + Entry.CreationTimeFine) * 10);

			file.CreateTime.Year = (ushort)((cdate >> 9) + 1980);
			file.CreateTime.Month = (ushort)(((cdate >> 5) - 1) & 0x0F);
			file.CreateTime.Day = (ushort)(cdate & 0x1F);
			file.CreateTime.Hour = (ushort)(ctime >> 11);
			file.CreateTime.Month = (ushort)((ctime >> 5) & 0x0F);
			file.CreateTime.Second = (ushort)(((ctime & 0x1F) * 2) + (msec / 100));
			file.CreateTime.Milliseconds = (ushort)(msec / 20);

			file.LastModifiedTime.Year = (ushort)((mdate >> 9) + 1980);
			file.LastModifiedTime.Month = (ushort)((mdate >> 5) & 0x0F);
			file.LastModifiedTime.Day = (ushort)(mdate & 0x1F);
			file.LastModifiedTime.Hour = (ushort)(mtime >> 11);
			file.LastModifiedTime.Minute = (ushort)((mtime >> 5) & 0x3F);
			file.LastModifiedTime.Second = (ushort)((mtime & 0x1F) * 2);
			file.LastModifiedTime.Milliseconds = 0;

			file.LastAccessTime.Year = (ushort)((adate >> 9) + 1980);
			file.LastAccessTime.Month = (ushort)((adate >> 5) & 0x0F);
			file.LastAccessTime.Day = (ushort)(adate & 0x1F);

			file.Directory = parent;
			file._startdisklocation = directory.GetUShort(offset + Entry.FirstCluster);

			if (file.Type == FileType.Directory)
				file._startdisklocation = dataareastart + ((file._startdisklocation - 2) * sectorspercluster);

			file._position = 0;
			file._count = 0;

			return file;
		}

		protected OpenFile GetRootDirectory ()
		{
			OpenFile file = new OpenFile();

			file.Type = FileType.Root;
			file.ReadOnly = true;
			file.Hidden = false;
			file.Archive = false;
			file.System = true;
			file.Size = 0;

			file.Name = null;
			file.CreateTime.Year = 0;
			file.CreateTime.Month = 0;
			file.CreateTime.Day = 0;
			file.CreateTime.Hour = 0;
			file.CreateTime.Month = 0;
			file.CreateTime.Second = 0;
			file.CreateTime.Milliseconds = 0;

			file.LastModifiedTime.Year = 0;
			file.LastModifiedTime.Month = 0;
			file.LastModifiedTime.Day = 0;
			file.LastModifiedTime.Hour = 0;
			file.LastModifiedTime.Minute = 0;
			file.LastModifiedTime.Second = 0;
			file.LastModifiedTime.Milliseconds = 0;

			file.LastAccessTime.Year = 0;
			file.LastAccessTime.Month = 0;
			file.LastAccessTime.Day = 0;

			file.Directory = null;

			file._startdisklocation = 0; // rootstartingsector;
			file._position = 0;
			file._count = 0;

			return file;
		}

		protected bool Compare (char[] a, char[] b)
		{
			if ((a == null) || (b == null))
				return false;

			if (a.Length != b.Length)
				return false;

			for (int i = 0; i < a.Length; i++)
				if (a[i] != b[i])
					return false;

			return true;
		}

		protected OpenFile FindFile (OpenFile parent, char[] filename)
		{
			uint activesector = parent._startdisklocation;

			if (activesector == 0)
				activesector = firstrootdirectorysector;

			uint increment = 0;

			for (; ; ) {
				MemoryBlock directory = new MemoryBlock(512);

				device.ReadBlock(activesector, 1, directory);

				for (uint index = 0; index < entriespersector; index++) {
					byte first = directory.GetByte(index * 32 + Entry.DOSName);

					if (first == FileNameAttribute.LastEntry)
						return null;

					OpenFile file = ExtractFileInformation(directory, index, parent);

					if (file != null)
						if (Compare(file.Name, filename))
							return file;

					if (file != null)
						Runtime.Free(file);

				}

				directory.Release();

				++increment;

				if (parent._startdisklocation == 0) {
					// root directory
					if (increment >= rootdirsectors)
						return null;

					activesector = parent._startdisklocation + increment;
					continue;
				}
				else {
					// subdirectory
					if (increment < sectorspercluster) {
						// still within cluster
						activesector = parent._startdisklocation + increment;
						continue;
					}
					// exiting cluster

					// goto next cluster (if any)
					uint cluster = GetClusterBySector(parent._startdisklocation);

					if (cluster == 0)
						return null;

					uint nextcluster = GetClusterEntryValue(cluster);

					if ((IsClusterLast(nextcluster)) || (IsClusterBad(nextcluster)) || (IsClusterFree(nextcluster)) || (IsClusterReserved(nextcluster)))
						return null;

					activesector = (uint)(dataareastart + (nextcluster - 1 * sectorspercluster));

					continue;
				}

			}
		}

		protected static int FindNextChar (string str, int index, char c)
		{
			for (int i = index; i < str.Length; i++)
				if (str[i] == c)
					return i;

			return -1;
		}

		protected static int FindNextPathSeperator (string path, int index)
		{
			for (int i = index; i < path.Length; i++)
				if ((path[i] == '\\') || (path[i] == '/'))
					return i;

			return -1;
		}

		protected static int FindAfterDriveLetter (string file)
		{
			int col = FindNextChar(file, 0, ':');

			if (col < 0)
				return 0;

			int sep = FindNextPathSeperator(file, col);

			if (sep < 0)
				return col + 1;

			return sep + 1;
		}

		protected static int FindEndPath (int index, string file)
		{
			int next = FindNextPathSeperator(file, index);

			if (next < 0)
				return file.Length;

			return next;
		}

		public OpenFile FindFile (string file)
		{
			OpenFile cur = GetRootDirectory();

			int len = file.Length;
			int at = FindAfterDriveLetter(file);

			for (; at < len; ) {
				int next = FindEndPath(at, file);

				char[] name = new char[next - at];

				for (int i = 0; i < next - at; i++)
					name[i] = file[at + i];

				OpenFile found = FindFile(cur, name);

				if (found == null)
					return null;

				if (cur != null)
					Runtime.Free(cur);

				cur = found;
				at = next + 1;

				Runtime.Free(name);
			}

			return cur;
		}

		public MemoryBlock LoadFile (OpenFile file)
		{
			MemoryBlock data = new MemoryBlock(file.Size);
			MemoryBlock sectorbuf = new MemoryBlock(bytespersector);

			uint cluster = file._startdisklocation;
			uint total = file.Size;
			uint at = 0;

			while (at < total) {
				if (!IsUsed(cluster))
					return data;		//TODO: Raise error, something bad happened!

				uint sector = dataareastart + ((cluster - 2) * sectorspercluster);

				for (int i = 0; i < sectorspercluster; i++) {
					device.ReadBlock(sector, 1, sectorbuf);

					sector++;

					if (at + bytespersector < total)
						sectorbuf.CopyTo(data.Offset(at), bytespersector);
					else
						sectorbuf.CopyTo(data.Offset(at), total - at);

					at = at + bytespersector;
				}

				cluster = this.GetClusterEntryValue(cluster);
			}

			return data;
		}

	}
}
