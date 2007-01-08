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
    class Fat12Utility
    {
        public static byte[] ReadSectorData(BinaryReader fileReader, Int32 sectorsToRead, Int16 bytesPerSector)
        {
            byte[] returnValue = new byte[sectorsToRead * (bytesPerSector - 2)];
            byte[] tempValue = null;
            for (int i = 0; i < sectorsToRead; i++)
            {
                tempValue = fileReader.ReadBytes(bytesPerSector - 2);
                tempValue.CopyTo(returnValue, i * (bytesPerSector - 2));
                //read end of sector marker and toss it away
                byte[] endOfSector = fileReader.ReadBytes(2);
            }
            return returnValue;
        }

        public static byte[] ReadFullSectors(BinaryReader fileReader, Int32 sectorsToRead, Int16 bytesPerSector)
        {
            byte[] returnValue;
            returnValue = fileReader.ReadBytes(sectorsToRead * bytesPerSector);
            return returnValue;
        }
    }
}
