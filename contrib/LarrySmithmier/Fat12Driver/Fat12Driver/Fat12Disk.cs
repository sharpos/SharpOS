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
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
using System.IO;

namespace Fat12Driver
{
    // not valid parent class? bjordan
    //public class Fat12Disk : FileSystemInfo
    public class Fat12Disk
    {
        BootSector BootSector;
        ReservedSector[] ReservedSector;
        FileAllocationTable[] FileAllocationTable;
        RootDirectory RootDirectory;

        /// <summary>
        /// Initiates a Fat12Disk object using the given image file.
        /// </summary>
        /// <param name="fileName">The path to the image file to read.</param>
        public Fat12Disk(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentException("File does not exist.", "fileName");
            }
            else
            {
                FileStream fileStream = null;
                BinaryReader binaryReader = null;
                try
                {
                    FileIOPermission fileStreamPermission;
                    fileStreamPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, fileName);
                    fileStream = new FileStream(fileName, FileMode.Open);
                    binaryReader = new BinaryReader(fileStream);
                    BootSector = ReadBootSector(binaryReader);
                    Int32 TotalNumberOfSectors;
                    if (BootSector.TotalSectors16 == 0)
                    {
                        TotalNumberOfSectors = BootSector.TotalSectors32;
                    }
                    else
                    {
                        TotalNumberOfSectors = BootSector.TotalSectors16;
                    }
                    ReservedSector = new ReservedSector[BootSector.Reserved];
                    for (int i = 0; i < BootSector.Reserved; i++)
                    {
                        ReservedSector[i] = ReadReservedSectors(binaryReader,BootSector.BytesPerSector);
                    }
                    FileAllocationTable = new FileAllocationTable[BootSector.NumberOfFileAllocationTables];
                    for (int i = 0; i < BootSector.NumberOfFileAllocationTables; i++)
                    {
                        FileAllocationTable[i] = ReadFileAllocationTable(binaryReader, TotalNumberOfSectors / BootSector.SectorsPerCluster, BootSector.BytesPerSector, BootSector.SectorsPerFileAllocationTable);
                    }
                    RootDirectory = ReadRootDirectory(binaryReader, BootSector.BytesPerSector, BootSector.MaximumNumberOfRootDirectoryEntries);
                }
                finally
                {
                    if (fileStream != null)
                    {
                        fileStream.Close();
                    }
                    if (binaryReader != null)
                    {
                        binaryReader.Close();
                    }
                }
            }
        }

        private ReservedSector ReadReservedSectors(BinaryReader binaryReader, short sectorLength)
        {
            return new ReservedSector(binaryReader, sectorLength);
        }


        private RootDirectory ReadRootDirectory(BinaryReader binaryReader, short sectorLength, Int32 maximumNumberOfRootDirectoryEntries)
        {
            return new RootDirectory(binaryReader, sectorLength, maximumNumberOfRootDirectoryEntries);
        }

        private FileAllocationTable ReadFileAllocationTable(BinaryReader binaryReader, Int32 numberOfClusters, Int16 bytesPerSector, Int32 sectorsPerFileAllocationTable)
        {
            return new FileAllocationTable(binaryReader, numberOfClusters, bytesPerSector, sectorsPerFileAllocationTable);
        }

        private BootSector ReadBootSector(BinaryReader binaryReader)
        {
            return new BootSector(binaryReader);
        }

        // no longer used without FileSystemInfo parent - bjordan
        //public override void Delete()
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public override bool Exists
        //{
        //    get { throw new Exception("The method or operation is not implemented."); }
        //}

        //public override string Name
        public string Name
        {
            get { return BootSector.VolumeLabel.ToString(); }
        }


        public override string ToString()
        {
            StringBuilder returnString = new StringBuilder();
            returnString.AppendLine("Fat12Disk");
            returnString.AppendLine("=========");
            returnString.AppendLine(BootSector.ToString());
            for (int i = 0; i < BootSector.NumberOfFileAllocationTables; i++)
            {
                returnString.AppendLine(FileAllocationTable[i].ToString());
            }
            returnString.AppendLine(RootDirectory.ToString());
            return returnString.ToString();
        }
    }
}
