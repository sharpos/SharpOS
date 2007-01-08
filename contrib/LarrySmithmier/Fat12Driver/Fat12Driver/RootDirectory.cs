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
    class RootDirectory
    {
        List<BaseDirectoryEntry> directoryEntries = new List<BaseDirectoryEntry>();
        Dictionary<byte, Dictionary<byte, string>> longFileNames = new Dictionary<byte, Dictionary<byte, string>>();
        public RootDirectory(BinaryReader fileReader, short sectorLength, Int32 maximumNumberOfRootDirectoryEntries)
        {
            Int32 numberOfSectors = (Int32)Math.Ceiling((maximumNumberOfRootDirectoryEntries * 32.0) / 512.0);
            byte[] rootDirectory = Fat12Utility.ReadFullSectors(fileReader, numberOfSectors, sectorLength);
            for (Int16 i = 0; i < rootDirectory.Length; i+=32)
            {
                if(i+32 > rootDirectory.Length )
                {
                    break;
                }
                BaseDirectoryEntry fde = new BaseDirectoryEntry(rootDirectory, i);
                switch(fde.FileAttributes){
                    case (byte)(BaseDirectoryEntry.FileAttribute.VolumeLabel | BaseDirectoryEntry.FileAttribute.System | BaseDirectoryEntry.FileAttribute.Hidden | BaseDirectoryEntry.FileAttribute.ReadOnly):
                        fde = new LongFileNameDirectoryEntry(rootDirectory, i);
                        if(!longFileNames.ContainsKey(((LongFileNameDirectoryEntry)fde).DOSFileNameCheckSum))
                        {
                            longFileNames.Add(((LongFileNameDirectoryEntry)fde).DOSFileNameCheckSum,new Dictionary<byte, string>());
                            longFileNames[(((LongFileNameDirectoryEntry)fde).DOSFileNameCheckSum)].Add((((LongFileNameDirectoryEntry)fde).SequenceNumber), ((LongFileNameDirectoryEntry)fde).LongFileName);
                        } else
                        {
                            longFileNames[(((LongFileNameDirectoryEntry)fde).DOSFileNameCheckSum)].Add((((LongFileNameDirectoryEntry)fde).SequenceNumber),((LongFileNameDirectoryEntry)fde).LongFileName);
                        }
//                        directoryEntries.Add(fde);
                        break;
                    default:
                        EightPtThreeDirectoryEntry frde = new EightPtThreeDirectoryEntry(rootDirectory, i);
                        if (!frde.DOSFileName.Equals("\0\0\0\0\0\0\0\0") & !frde.DOSFileName.Equals("\0\0\0\0\0\0\0\0"))
                        {
                            directoryEntries.Add(frde);
                        }
                        break;
                }

            }
        }


        public override string ToString()
        {
            StringBuilder returnValue = new StringBuilder("RootDirectory");
            returnValue.AppendLine();
            returnValue.AppendLine("------------------");
            foreach (BaseDirectoryEntry entry in directoryEntries)
            {
                returnValue.AppendLine(entry.ToString());
            }
            foreach (KeyValuePair<byte, Dictionary<byte, string>> longFileNameDictionary in longFileNames)
            {
                returnValue.AppendFormat("DOS Filename Checksum {0,4}: ", longFileNameDictionary.Key);
                foreach (KeyValuePair<byte, string> pair in longFileNameDictionary.Value)
                {
                    returnValue.Append(String.Format("{0} ({1})", pair.Value, pair.Key));
                }
                returnValue.AppendLine();
            }

            return returnValue.ToString();
        }
    }
}
