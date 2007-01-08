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
using System.IO;
using System.Text;

namespace Fat12Driver
{
    class LongFileNameDirectoryEntry : BaseDirectoryEntry
    {
        #region Entry Structure
        private byte sequenceNumber;
        private byte[] nameCharacters1 = new byte[10]; 
        //private byte fileAttributes; //always 0x0Fh  /* Inherited from BaseDirectoryEntry */
        //private byte nTReserved; //always 0x00h      /* Inherited from BaseDirectoryEntry */
        private byte dOSFileNameCheckSum;
        private byte[] nameCharacters2 = new byte[12]; 
        //private Int16 firstCluster; //always 0x0000h /* Inherited from BaseDirectoryEntry */
        private byte[] nameCharacters3 = new byte[4];
        private byte[] nameCharactersBytes = new byte[26];
        #endregion Entry Structure

        UnicodeEncoding unicodeEncoder = new UnicodeEncoding();

        #region Properties
        public string LongFileName
        {
            get
            {
                return unicodeEncoder.GetString(nameCharactersBytes);
            }
        }

        public byte SequenceNumber
        {
            get { return sequenceNumber; }
            set { sequenceNumber = value; }
        }

        public byte DOSFileNameCheckSum
        {
            get { return dOSFileNameCheckSum; }
            set { dOSFileNameCheckSum = value; }
        }

        #endregion Properties

        public LongFileNameDirectoryEntry(byte[] sectorData, Int16 sectorDataIndex): this(new BinaryReader(new MemoryStream(sectorData,sectorDataIndex,32)))
        {}

        public LongFileNameDirectoryEntry(BinaryReader fileStream)
        {
            sequenceNumber = fileStream.ReadByte();
            nameCharacters1 = fileStream.ReadBytes(10);
            fileAttributes = fileStream.ReadByte();
            nTReserved = fileStream.ReadByte();
            dOSFileNameCheckSum = fileStream.ReadByte();
            nameCharacters2 = fileStream.ReadBytes(12);
            firstCluster = fileStream.ReadInt16();
            nameCharacters3 = fileStream.ReadBytes(4);
            nameCharacters1.CopyTo(nameCharactersBytes,0);
            nameCharacters2.CopyTo(nameCharactersBytes,10);
            nameCharacters3.CopyTo(nameCharactersBytes,22);
        }

        public static byte GetDOSFileNameCheckSum(char[] fileNameAndExtension)
        {
            byte returnValue = 0x00;
            for (int i = 11; i != 0; i--)
                returnValue = (byte)((((returnValue & 0x01) != 0) ? 0x80 : 0) + (returnValue >> 1) + fileNameAndExtension[11-i]);
            return returnValue;
        }

        public static byte GetDOSFileNameCheckSum(string fileNameAndExtension)
        {
            byte returnValue = 0x00;
            for (int i = 11; i != 0; i--)
                returnValue = (byte)((((returnValue & 0x01) != 0) ? 0x80 : 0) + (returnValue >> 1) + fileNameAndExtension[11 - i]);
            return returnValue;
        }

        public override string ToString()
        {
            StringBuilder returnValue;
            returnValue = new StringBuilder("LongFileNameDirectoryEntry");
            returnValue.AppendLine();
            returnValue.AppendLine("==========================");
            returnValue.AppendLine(String.Format("LongFileName = {0}", unicodeEncoder.GetString(nameCharactersBytes)));
            returnValue.Append((BaseDirectoryEntry.FileAttribute.Archive & (FileAttribute)fileAttributes) != 0 ? "Archive, " : "");
            returnValue.Append((BaseDirectoryEntry.FileAttribute.Device & (FileAttribute)fileAttributes) != 0 ? "Device, " : "");
            returnValue.Append((BaseDirectoryEntry.FileAttribute.Hidden & (FileAttribute)fileAttributes) != 0 ? "Hidden, " : "");
            returnValue.Append((BaseDirectoryEntry.FileAttribute.ReadOnly & (FileAttribute)fileAttributes) != 0 ? "ReadOnly, " : "");
            returnValue.Append((BaseDirectoryEntry.FileAttribute.Subdirectory & (FileAttribute)fileAttributes) != 0 ? "Subdirectory, " : "");
            returnValue.Append((BaseDirectoryEntry.FileAttribute.System & (FileAttribute)fileAttributes) != 0 ? "System, " : "");
            returnValue.Append((BaseDirectoryEntry.FileAttribute.Unused & (FileAttribute)fileAttributes) != 0 ? "Unused, " : "");
            returnValue.Append((BaseDirectoryEntry.FileAttribute.VolumeLabel & (FileAttribute)fileAttributes) != 0 ? "VolumeLabel" : "");
            returnValue.AppendLine();
            returnValue.AppendFormat("Sequence Number= {0:X}", sequenceNumber);
            returnValue.AppendLine();
            returnValue.AppendFormat("DOSFileNameCheckSum= {0:X}", dOSFileNameCheckSum);
            returnValue.AppendLine();
            return returnValue.ToString();
        }
    }
}
