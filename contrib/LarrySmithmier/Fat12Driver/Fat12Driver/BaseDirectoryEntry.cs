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
    class BaseDirectoryEntry
    {
        #region Entry Structure
        private byte[] baseEntryPieceOne = new byte[11];
        /* exposed to children of BaseDirectoryEntry */
        protected byte fileAttributes;
        /* exposed to children of BaseDirectoryEntry */
        protected byte nTReserved;
        private byte[] baseEntryPieceTwo = new byte[13];
        /* exposed to children of BaseDirectoryEntry */
        protected Int16 firstCluster;
        private byte[] baseEntryPieceThree = new byte[4];
        #endregion Entry Structure

        #region Properties

        public byte[] BaseEntryPieceOne
        {
            get { return baseEntryPieceOne; }
            set { baseEntryPieceOne = value; }
        }

        public byte[] BaseEntryPieceTwo
        {
            get { return baseEntryPieceTwo; }
            set { baseEntryPieceTwo = value; }
        }

        public byte[] BaseEntryPieceThree
        {
            get { return baseEntryPieceThree; }
            set { baseEntryPieceThree = value; }
        }

        public byte FileAttributes
        {
            get { return fileAttributes; }
            set { fileAttributes = value; }
        }

        public byte NTReserved
        {
            get { return nTReserved; }
            set { nTReserved = value; }
        }

        public short FirstCluster
        {
            get { return firstCluster; }
            set { firstCluster = value; }
        }
        #endregion Properties

        public BaseDirectoryEntry(byte[] sectorData, Int16 sectorDataIndex): this(new BinaryReader(new MemoryStream(sectorData,sectorDataIndex,32)))
        {}

        public BaseDirectoryEntry(BinaryReader fileStream)
        {
            baseEntryPieceOne = fileStream.ReadBytes(11);
            fileAttributes = fileStream.ReadByte();
            nTReserved = fileStream.ReadByte();
            baseEntryPieceTwo = fileStream.ReadBytes(13);
            firstCluster = fileStream.ReadInt16();
            baseEntryPieceThree = fileStream.ReadBytes(4);
        }

        public BaseDirectoryEntry(){ }

        [Flags]
        public enum FileAttribute
        {
            ReadOnly = 0x01,
            Hidden = 0x02,
            System = 0x04,
            VolumeLabel = 0x08,   //an attribute of 0x0F is used to designate a long filename
            Subdirectory = 0x10,
            Archive = 0x20,
            Device = 0x40,
            Unused = 0x80
        }

    }
}
