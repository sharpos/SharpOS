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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fat12Driver
{
    class FileAllocationTable
    {
        private SortedDictionary<Int16, Int16> allClusterAllocationList = new SortedDictionary<Int16, Int16>();
        private Dictionary<Int16, Int16> usedClusterAllocationList = new Dictionary<Int16, Int16>();
        private SortedDictionary<Int16, Int16> freeClusterAllocationList = new SortedDictionary<Int16, Int16>();
        private SortedDictionary<Int16, Int16> badClusterAllocationList = new SortedDictionary<Int16, Int16>();
        private SortedDictionary<Int16, Int16> reservedClusterAllocationList = new SortedDictionary<Int16, Int16>();
        private SortedDictionary<Int16, Int16> errorClusterAllocationList = new SortedDictionary<Int16, Int16>();
        private Int32 numberOfClusters;
        private byte FirstByte;
        private byte SecondByte;
        private byte ThirdByte;

        private byte[] leftOverBytes;

        public FileAllocationTable(BinaryReader fileReader, Int32 numberOfClusters, Int16 bytesPerSector, Int32 sectorsPerFileAllocationTable)
        {
            this.numberOfClusters = numberOfClusters;
            byte[] data = Fat12Utility.ReadFullSectors(fileReader, sectorsPerFileAllocationTable, bytesPerSector);
            int i = 0;
            Int16 clusterNumber=0;
            //This loop counts the number of nibbles used.  Each cluster entry uses 3 nibbles.
            FirstByte = data[i];
            SecondByte = data[i + 1];
            ThirdByte = data[i + 2];
            i += 3;
            for (clusterNumber = 2; clusterNumber < numberOfClusters; i += 3)
            {
                Int16 clusterValue;
                //if these bytes are uv,wx,yz then the entries are xuv and yzw
                // from http://www.win.tue.nl/~aeb/linux/fs/fat/fat-1.html
                clusterValue = ReadEvenEntry(data[i], data[i + 1]);
                allClusterAllocationList.Add(clusterNumber,clusterValue);
                StoreClusterValue(clusterNumber++, clusterValue);
                clusterValue = ReadOddEntry(data[i + 1], data[i + 2]);
                allClusterAllocationList.Add(clusterNumber, clusterValue);
                StoreClusterValue(clusterNumber++, clusterValue);
            }
            //take care of the extra bit of sector left over
            // each cluster is 3 nibbles, bytesPerSector * 2 = nibblesPerSector
            Int32 remainingBytes = ((numberOfClusters * 3) % (bytesPerSector*2))/2;
            leftOverBytes = new byte[remainingBytes];
            for (int j = 0; j < remainingBytes; j++)
            {
                leftOverBytes[j] = data[i++];               
            }
        }

        private void StoreClusterValue(short clusterNumber, short clusterValue)
        {
            switch (clusterValue)
            {
                case 0:
                    freeClusterAllocationList.Add(clusterNumber, clusterValue);
                    break;
                case 1:
                    reservedClusterAllocationList.Add(clusterNumber, clusterValue);
                    break;
                case 0xFF0:
                case 0xFF1:
                case 0xFF2:
                case 0xFF3:
                case 0xFF4:
                case 0xFF5:
                case 0xFF6:
                    errorClusterAllocationList.Add(clusterNumber, clusterValue);
                    break;
                case 0xFF7:
                    badClusterAllocationList.Add(clusterNumber, clusterValue);
                    break;
                default:
                    // 0x002-0xFEF are pointing to the next cluster and
                    // 0xFF8-0xFFF are signifying the end of the file
                    usedClusterAllocationList.Add(clusterNumber, clusterValue);
                    break;
            }
        }

        public bool WriteFat12FileAllocationTable()
        {
            bool returnValue = true;

            return returnValue;
        }

        public override string ToString()
        {
            StringBuilder returnValue = new StringBuilder("FileAllocationTable");
            returnValue.AppendLine();
            returnValue.AppendLine("------------------------");
            returnValue.AppendLine(String.Format("usedClusterAllocationList has {0} values.", usedClusterAllocationList.Count));
            returnValue.AppendLine(String.Format("freeClusterAllocationList has {0} values.", freeClusterAllocationList.Count));
            returnValue.AppendLine(String.Format("badClusterAllocationList has {0} values.", badClusterAllocationList.Count));
            returnValue.AppendLine(String.Format("reservedClusterAllocationList has {0} values.", reservedClusterAllocationList.Count));
//            returnValue.AppendLine(String.Format("errorClusterAllocationList has {0} values.", errorClusterAllocationList.Count));
//            foreach (KeyValuePair<short, short> pair in errorClusterAllocationList)
//            {
//                returnValue.AppendLine(String.Format("errorClusterAllocationList value: <{0}, {1}>", pair.Key, pair.Value));
//            }
            returnValue.AppendLine("allClusterAllocationList value: ");
            //returnValue.Append("usedClusterAllocationList value: ");
            //foreach (KeyValuePair<short, short> pair in usedClusterAllocationList)
            int endLine = 0;
            foreach (KeyValuePair<short, short> pair in allClusterAllocationList)
            {
                if(endLine++%8==0)
                {
                    returnValue.AppendLine();
                }
                if (pair.Value >= 0xFF8)
                {
                    returnValue.Append(String.Format("|{0,4}, END", pair.Key, pair.Value));
                    returnValue.AppendLine();
                    returnValue.AppendLine();
                }
                else
                {
                    returnValue.Append(String.Format("|{0,4},{1,4}", pair.Key, pair.Value));
                }
            }
            returnValue.AppendLine();
            returnValue.AppendLine(String.Format("plus cluster 0 and 1 containing bytes: 0x{0}h 0x{1}h 0x{2}h", FirstByte.ToString("X"), SecondByte.ToString("X"), ThirdByte.ToString("X")));
            return returnValue.ToString();
        }

        private Int16 ReadEvenEntry(byte firstPart, byte secondPart)
        {
            Int16 returnValue = (Int16)((((UInt16)(secondPart & 0x0F)) << 8) | firstPart);
            return returnValue;
        }

        private Int16 ReadOddEntry(byte firstPart, byte secondPart)
        {
            // the inner cast is used to make this a logical shift rather than an arithmetic one
            Int16 returnValue = (Int16)((((UInt16)secondPart) << 4) | (((UInt16)firstPart) >> 4));
            return returnValue;
        }

    }
}
