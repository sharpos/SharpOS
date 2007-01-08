/* Fat12 Driver Library
 * Copyright (C) 2006,2007 Larry Smithmier
 * email: larry@smithmier.com
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"), to deal in 
 * the Software without restriction, including without limitation the rights to 
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies 
 * of the Software, and to permit persons to whom the Software is furnished to do 
 * so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all 
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS 
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER 
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
 * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 */
using System;
using System.IO;
using System.Text;

namespace Fat12Driver
{
    class BootSector
    {
        private byte[] jumpLocation = new byte[3];
        private char[] oEMLabel = new char[8];
        private Int16 bytesPerSector;
        private byte sectorsPerCluster;
        private Int16 reservedSectorCount;
        private byte numberOfFileAllocationTables;
        private Int16 maximumNumberOfRootDirectoryEntries;
        private Int16 totalSectors16;
        private byte mediaDescriptor;
        private Int16 sectorsPerFileAllocationTable;
        private Int16 sectorsPerTrack;
        private Int16 numberOfHeads;
        private Int32 hiddenSectors;
        private Int32 totalSectors32;
        private byte physicalDriveNumber;
        private byte reserved;
        private byte signature;
        private Int32 serialNumber;
        private char[] volumeLabel = new char[11];
        private char[] fATFileSystemType = new char[8];
        private byte[] oSBootCode = new byte[448];
        private byte[] endOfSectorMarker = new byte[2];


        public byte[] JumpLocation
        {
            get { return jumpLocation; }
            set { jumpLocation = value; }
        }

        public char[] OEMLabel
        {
            get { return oEMLabel; }
            set { oEMLabel = value; }
        }

        public string OEMLabelString
        {
            get
            {
                StringBuilder returnString = new StringBuilder(oEMLabel.Length);
                returnString.Append(oEMLabel);
                return returnString.ToString();
            }
        }

        public short BytesPerSector
        {
            get { return bytesPerSector; }
            set { bytesPerSector = value; }
        }

        public byte SectorsPerCluster
        {
            get { return sectorsPerCluster; }
            set { sectorsPerCluster = value; }
        }

        public short ReservedSectorCount
        {
            get { return reservedSectorCount; }
            set { reservedSectorCount = value; }
        }

        public byte NumberOfFileAllocationTables
        {
            get { return numberOfFileAllocationTables; }
            set { numberOfFileAllocationTables = value; }
        }

        public short MaximumNumberOfRootDirectoryEntries
        {
            get { return maximumNumberOfRootDirectoryEntries; }
            set { maximumNumberOfRootDirectoryEntries = value; }
        }

        public short TotalSectors16
        {
            get { return totalSectors16; }
            set { totalSectors16 = value; }
        }

        public byte MediaDescriptor
        {
            get { return mediaDescriptor; }
            set { mediaDescriptor = value; }
        }

        public short SectorsPerFileAllocationTable
        {
            get { return sectorsPerFileAllocationTable; }
            set { sectorsPerFileAllocationTable = value; }
        }

        public short SectorsPerTrack
        {
            get { return sectorsPerTrack; }
            set { sectorsPerTrack = value; }
        }

        public short NumberOfHeads
        {
            get { return numberOfHeads; }
            set { numberOfHeads = value; }
        }

        public int HiddenSectors
        {
            get { return hiddenSectors; }
            set { hiddenSectors = value; }
        }

        public int TotalSectors32
        {
            get { return totalSectors32; }
            set { totalSectors32 = value; }
        }

        public byte PhysicalDriveNumber
        {
            get { return physicalDriveNumber; }
            set { physicalDriveNumber = value; }
        }

        public byte Reserved
        {
            get { return reserved; }
            set { reserved = value; }
        }

        public byte Signature
        {
            get { return signature; }
            set { signature = value; }
        }

        public int SerialNumber
        {
            get { return serialNumber; }
            set { serialNumber = value; }
        }

        public char[] VolumeLabel
        {
            get { return volumeLabel; }
            set { volumeLabel = value; }
        }

        public string VolumeLabelString
        {
            get
            {
                StringBuilder returnString = new StringBuilder(volumeLabel.Length);
                returnString.Append(volumeLabel);
                return returnString.ToString();
            }
        }

        public char[] FATFileSystemType
        {
            get { return fATFileSystemType; }
            set { fATFileSystemType = value; }
        }

        public string FATFileSystemTypeString
        {
            get
            {
                StringBuilder returnString = new StringBuilder(FATFileSystemType.Length);
                returnString.Append(FATFileSystemType);
                return returnString.ToString();
            }
        }

        public byte[] OSBootCode
        {
            get { return oSBootCode; }
            set { oSBootCode = value; }
        }

        public byte[] EndOfSectorMarker
        {
            get { return endOfSectorMarker; }
            set { endOfSectorMarker = value; }
        }

/*
        enum Media_Descriptor
        {
            SingleSided_80_9 = 0xF8,
            DoubleSided_80_9 = 0xF9,
            SingleSided_80_8 = 0xFA,
            DoubleSided_80_8 = 0xFB,
            SingleSided_40_9 = 0xFC,
            DoubleSided_40_9 = 0xFD,
            SingleSided_40_8 = 0xFE,
            DoubleSided_40_8 = 0xFF
        }
*/

        public BootSector(byte[] jumpLocation, char[] oEMLabel, short bytesPerSector, byte sectorsPerCluster, short reservedSectorCount, byte numberOfFileAllocationTables, short maximumNumberOfRootDirectoryEntries, short totalSectors16, byte mediaDescriptor, short sectorsPerFileAllocationTable, short sectorsPerTrack, short numberOfHeads, int hiddenSectors, int totalSectors32, byte physicalDriveNumber, byte reserved, byte signature, int serialNumber, char[] volumeLabel, char[] fATFileSystemType, byte[] oSBootCode, byte[] endOfSectorMarker)
        {
            this.jumpLocation = jumpLocation;
            this.oEMLabel = oEMLabel;
            this.bytesPerSector = bytesPerSector;
            this.sectorsPerCluster = sectorsPerCluster;
            this.reservedSectorCount = reservedSectorCount;
            this.numberOfFileAllocationTables = numberOfFileAllocationTables;
            this.maximumNumberOfRootDirectoryEntries = maximumNumberOfRootDirectoryEntries;
            this.totalSectors16 = totalSectors16;
            this.mediaDescriptor = mediaDescriptor;
            this.sectorsPerFileAllocationTable = sectorsPerFileAllocationTable;
            this.sectorsPerTrack = sectorsPerTrack;
            this.numberOfHeads = numberOfHeads;
            this.hiddenSectors = hiddenSectors;
            this.totalSectors32 = totalSectors32;
            this.physicalDriveNumber = physicalDriveNumber;
            this.reserved = reserved;
            this.signature = signature;
            this.serialNumber = serialNumber;
            this.volumeLabel = volumeLabel;
            this.fATFileSystemType = fATFileSystemType;
            this.oSBootCode = oSBootCode;
            this.endOfSectorMarker = endOfSectorMarker;
        }

        public BootSector(BinaryReader fileStream)
        {
            this.jumpLocation = fileStream.ReadBytes(3);
            this.oEMLabel = fileStream.ReadChars(8);
            this.bytesPerSector = fileStream.ReadInt16();
            this.sectorsPerCluster = fileStream.ReadByte();
            this.reservedSectorCount = fileStream.ReadInt16();
            this.numberOfFileAllocationTables = fileStream.ReadByte();
            this.maximumNumberOfRootDirectoryEntries = fileStream.ReadInt16();
            this.totalSectors16 = fileStream.ReadInt16();
            this.mediaDescriptor = fileStream.ReadByte();
            this.sectorsPerFileAllocationTable = fileStream.ReadInt16();
            this.sectorsPerTrack = fileStream.ReadInt16();
            this.numberOfHeads = fileStream.ReadInt16();
            this.hiddenSectors = fileStream.ReadInt32();
            this.totalSectors32 = fileStream.ReadInt32();
            this.physicalDriveNumber = fileStream.ReadByte();
            this.reserved = fileStream.ReadByte();
            this.signature = fileStream.ReadByte();
            this.serialNumber = fileStream.ReadInt32();
            this.volumeLabel = fileStream.ReadChars(11);
            this.fATFileSystemType = fileStream.ReadChars(8);
            // normal sector size is 512 which makes the normal boot code length 448
            this.oSBootCode = fileStream.ReadBytes(this.bytesPerSector-64);
            this.endOfSectorMarker = fileStream.ReadBytes(2);
        }

        public bool WriteFat12BootSector(BinaryWriter fileStream)
        {
            bool successfulWrite = true;
            fileStream.Write(this.jumpLocation);
            fileStream.Write(this.oEMLabel);
            fileStream.Write(this.bytesPerSector);
            fileStream.Write(this.sectorsPerCluster);
            fileStream.Write(this.reservedSectorCount);
            fileStream.Write(this.numberOfFileAllocationTables);
            fileStream.Write(this.maximumNumberOfRootDirectoryEntries);
            fileStream.Write(this.totalSectors16);
            fileStream.Write(this.mediaDescriptor);
            fileStream.Write(this.sectorsPerFileAllocationTable);
            fileStream.Write(this.sectorsPerTrack);
            fileStream.Write(this.numberOfHeads);
            fileStream.Write(this.hiddenSectors);
            fileStream.Write(this.totalSectors32);
            fileStream.Write(this.physicalDriveNumber);
            fileStream.Write(this.reserved);
            fileStream.Write(this.signature);
            fileStream.Write(this.serialNumber);
            fileStream.Write(this.volumeLabel);
            fileStream.Write(this.fATFileSystemType);
            fileStream.Write(this.oSBootCode);
            fileStream.Write(this.endOfSectorMarker);
            return successfulWrite;
        }

        public override string ToString()
        {
            StringBuilder returnString = new StringBuilder();
            returnString.AppendLine("BootSector");
            returnString.AppendLine("---------------");
            returnString.AppendLine(string.Format("JumpLocation = {0} {1} {2}", jumpLocation[0].ToString("X"), jumpLocation[1].ToString("X"), jumpLocation[2].ToString("X")));
            returnString.AppendLine(string.Format("OEMLabel = {0}", OEMLabelString));
            returnString.AppendLine(string.Format("BytesPerSector = {0}", bytesPerSector));
            returnString.AppendLine(string.Format("SectorsPerCluster = {0}", sectorsPerCluster));
            returnString.AppendLine(string.Format("ReservedSectorCount = {0}", reservedSectorCount));
            returnString.AppendLine(string.Format("NumberOfFileAllocationTables = {0}", numberOfFileAllocationTables));
            returnString.AppendLine(string.Format("MaximumNumberOfRootDirectoryEntries = {0}", maximumNumberOfRootDirectoryEntries));
            returnString.AppendLine(string.Format("TotalSectors16 = {0}", totalSectors16));
            returnString.AppendLine(string.Format("MediaDescriptor = {0} ({1})", mediaDescriptor, mediaDescriptor.ToString("X")));
            returnString.AppendLine(string.Format("SectorsPerFileAllocationTable = {0}", sectorsPerFileAllocationTable));
            returnString.AppendLine(string.Format("SectorsPerTrack = {0}", sectorsPerTrack));
            returnString.AppendLine(string.Format("NumberOfHeads = {0}", numberOfHeads));
            returnString.AppendLine(string.Format("HiddenSectors = {0}", hiddenSectors));
            returnString.AppendLine(string.Format("TotalSectors32 = {0}", totalSectors32));
            returnString.AppendLine(string.Format("PhysicalDriveNumber = {0}", physicalDriveNumber));
            returnString.AppendLine(string.Format("Reserved = {0}", reserved));
            returnString.AppendLine(string.Format("Signature = {0}", signature));
            returnString.AppendLine(string.Format("SerialNumber = {0}", serialNumber));
            returnString.AppendLine(string.Format("VolumeLabel = {0}", VolumeLabelString));
            returnString.AppendLine(string.Format("FATFileSystemType = {0}", FATFileSystemTypeString));
            returnString.AppendLine(string.Format("OSBootCode = {0}", oSBootCode));
            returnString.AppendLine(string.Format("EndOfSectorMarker = {0} {1}", endOfSectorMarker[0].ToString("X"), endOfSectorMarker[1].ToString("X")));
            return returnString.ToString();
        }
    }
}


