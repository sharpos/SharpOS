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
    class EightPtThreeDirectoryEntry : BaseDirectoryEntry
    {
        #region Entry Structure
        private byte[] dOSFileName = new byte[8];
        private byte[] dOSFileExtension = new byte[3];
        //private byte fileAttributes;                 /* Inherited from BaseDirectoryEntry */
        //private byte nTReserved;                     /* Inherited from BaseDirectoryEntry */
        private byte createTimeFine;                   //fine resolution, 10ms units
        private byte[] createTimeCoarse = new byte[2]; //bits 15-11 for hours, 10-5 for minutes, and 4-0 for seconds/2 (two second units)
        private byte[] createDate = new byte[2];       //bits 15-9 for year (1980-2107), 8-5 for month (1=jan, 12=dec), and 4-0 for day
        private byte[] lastAccessDate = new byte[2];   //same format as createDate
        private byte[] eAIndex = new byte[2];
        private byte[] lastModifiedTime = new byte[2]; //same format as createTime
        private byte[] lastModifiedDate = new byte[2]; //same format as createDate
        //private Int16 firstCluster;                  /* Inherited from BaseDirectoryEntry */
        private Int32 fileSize;
        #endregion Entry Structure

        #region Internal Variables
        private ASCIIEncoding aSCIIEncoder = new ASCIIEncoding();
        private Int16 createTimeCoarseHours;
        private Int16 createTimeCoarseMinutes;
        private Int16 createTimeCoarseSeconds;
        private Int16 createDateYear;
        private Int16 createDateMonth;
        private Int16 createDateDay;
        private DateTime createDateTime = new DateTime();
        private Int16 lastAccessDateYear;
        private Int16 lastAccessDateMonth;
        private Int16 lastAccessDateDay;
        private Int16 lastModifiedTimeHours;
        private Int16 lastModifiedTimeMinutes;
        private Int16 lastModifiedTimeSeconds;
        private Int16 lastModifiedDateYear;
        private Int16 lastModifiedDateMonth;
        private Int16 lastModifiedDateDay;
        private DateTime lastModifiedDateTime = new DateTime();
        #endregion Internal Variables

        #region Properties
        public string DOSFileName
        {
            get { return this.aSCIIEncoder.GetString(this.dOSFileName); }
            set
            {
                byte[] temp = this.aSCIIEncoder.GetBytes(value.ToCharArray());
                for (int i = 0; i < this.dOSFileName.Length; i++)
                {
                    this.dOSFileName[i] = temp[i];
                }
            }
        }

        public string DOSFileExtension
        {
            get { return this.aSCIIEncoder.GetString(this.dOSFileExtension); }
            set
            {
                byte[] temp = this.aSCIIEncoder.GetBytes(value.ToCharArray());
                for (int i = 0; i < this.dOSFileExtension.Length; i++)
                {
                    this.dOSFileExtension[i] = temp[i];
                }
            }
        }
        #endregion Properties

        public EightPtThreeDirectoryEntry(byte[] sectorData, Int16 sectorDataIndex) :this(new BinaryReader(new MemoryStream(sectorData,sectorDataIndex,32)))
        {}

        public EightPtThreeDirectoryEntry(BinaryReader fileStream)
        {
            this.dOSFileName = fileStream.ReadBytes(8);
            this.dOSFileExtension = fileStream.ReadBytes(3);
            this.fileAttributes = fileStream.ReadByte();
            this.nTReserved = fileStream.ReadByte();
            this.createTimeFine = fileStream.ReadByte();
            this.createTimeCoarse = fileStream.ReadBytes(2);
            this.createDate = fileStream.ReadBytes(2);
            this.createDateTime = GetDateTime(this.createDate, out this.createDateMonth, out this.createDateDay, out this.createDateYear, this.createTimeCoarse,
                        out this.createTimeCoarseHours, out this.createTimeCoarseMinutes, out this.createTimeCoarseSeconds);
            this.lastAccessDate = fileStream.ReadBytes(2);
            GetDate(this.lastAccessDate, out this.lastAccessDateMonth, out this.lastAccessDateDay, out this.lastAccessDateYear);
            this.eAIndex = fileStream.ReadBytes(2);
            this.lastModifiedTime = fileStream.ReadBytes(2);
            this.lastModifiedDate = fileStream.ReadBytes(2);
            this.lastModifiedDateTime = GetDateTime(this.lastModifiedDate, out this.lastModifiedDateMonth, out this.lastModifiedDateDay, out this.lastModifiedDateYear, this.lastModifiedTime,
                        out this.lastModifiedTimeHours, out this.lastModifiedTimeMinutes, out this.lastModifiedTimeSeconds);
            this.firstCluster = fileStream.ReadInt16();
            this.fileSize = fileStream.ReadInt32();
        }


        public override string ToString()
        {
            StringBuilder returnValue = null;
            switch ((FileAttribute)this.fileAttributes)
            {
                case BaseDirectoryEntry.FileAttribute.VolumeLabel:
                    returnValue = new StringBuilder("Fat12VolumeLabelDirectoryEntry");
                    returnValue.AppendLine();
                    returnValue.AppendLine("==========================");
                    returnValue.AppendLine(String.Format("Volume Label = {0}{1}", this.aSCIIEncoder.GetString(this.dOSFileName), this.aSCIIEncoder.GetString(this.dOSFileExtension)));
                    break;
                default:
                    returnValue = new StringBuilder("EightPtThreeDirectoryEntry");
                    returnValue.AppendLine();
                    returnValue.AppendLine("==========================");
                    returnValue.AppendLine(String.Format("DOSFileName = {0}", this.aSCIIEncoder.GetString(this.dOSFileName)));
                    returnValue.AppendLine(String.Format("DOSFileExtension = {0}", this.aSCIIEncoder.GetString(this.dOSFileExtension)));
                    break;
            }
            returnValue.Append((BaseDirectoryEntry.FileAttribute.Archive      & (FileAttribute)this.fileAttributes) != 0 ? "Archive, "      : "");
            returnValue.Append((BaseDirectoryEntry.FileAttribute.Device       & (FileAttribute)this.fileAttributes) != 0 ? "Device, "       : "");
            returnValue.Append((BaseDirectoryEntry.FileAttribute.Hidden       & (FileAttribute)this.fileAttributes) != 0 ? "Hidden, "       : "");
            returnValue.Append((BaseDirectoryEntry.FileAttribute.ReadOnly     & (FileAttribute)this.fileAttributes) != 0 ? "ReadOnly, "     : "");
            returnValue.Append((BaseDirectoryEntry.FileAttribute.Subdirectory & (FileAttribute)this.fileAttributes) != 0 ? "Subdirectory, " : "");
            returnValue.Append((BaseDirectoryEntry.FileAttribute.System       & (FileAttribute)this.fileAttributes) != 0 ? "System, "       : "");
            returnValue.Append((BaseDirectoryEntry.FileAttribute.Unused       & (FileAttribute)this.fileAttributes) != 0 ? "Unused, "       : "");
            returnValue.Append((BaseDirectoryEntry.FileAttribute.VolumeLabel  & (FileAttribute)this.fileAttributes) != 0 ? "VolumeLabel"    : "");
            returnValue.AppendLine();
//            returnValue.AppendLine(String.Format("Created {0:d2}:{1:d2}{2} {3:d2}/{4:d2}/{5:d4}", this.createTimeCoarseHours % 12,
//                this.createTimeCoarseMinutes, this.createTimeCoarseHours / 12 > 1 ? " PM" : " AM", this.createDateMonth, this.createDateDay, this.createDateYear));
//            returnValue.AppendLine(String.Format("LastModified {0:d2}:{1:d2}{2} {3:d2}/{4:d2}/{5:d4}", this.lastModifiedTimeHours % 12,
//                this.lastModifiedTimeMinutes, this.lastModifiedTimeHours / 12 > 1 ? " PM" : " AM", this.lastModifiedDateMonth, this.lastModifiedDateDay, this.lastModifiedDateYear));
            returnValue.AppendLine(String.Format("Created {0}", this.createDateTime));
            returnValue.AppendLine(String.Format("LastModified {0}", this.lastModifiedDateTime));
            returnValue.AppendLine(String.Format("FirstCluster {0}", this.firstCluster));
            returnValue.AppendLine(String.Format("DOS Filename Checksum: {0}", LongFileNameDirectoryEntry.GetDOSFileNameCheckSum(this.DOSFileName+this.DOSFileExtension)));
            return returnValue.ToString();
        }

        private static DateTime GetDateTime(byte[] date, out short dateMonth, out short dateDay, out short dateYear, byte[] timeCoarse, out short timeCoarseHours, out short timeCoarseMinutes, out short timeCoarseSeconds)
        {
            GetDate(date, out dateMonth, out dateDay, out dateYear);
            timeCoarseSeconds = (Int16) ((timeCoarse[0] & 0x1F)*2);
            timeCoarseMinutes = (Int16)((timeCoarse[0] >> 5) | ((timeCoarse[1] & 0x07) << 3));
            timeCoarseHours = (Int16) (timeCoarse[1] >> 3);

            DateTime dateTime;
            if (dateMonth != 0)
            {
                dateTime =
                    new DateTime(dateYear, dateMonth, dateDay, timeCoarseHours, timeCoarseMinutes, timeCoarseSeconds);
            } else
            {
                dateTime = DateTime.MinValue;
            }

            return dateTime;
        }

        private static void GetDate(byte[] date, out short dateMonth, out short dateDay, out short dateYear)
        {
            dateDay = (Int16)(date[0] & 0x1F);
            dateMonth = (Int16) ((date[0] >> 5) | ((date[1] & 0x01)<<3));
            dateYear = (Int16)((date[1]>>1) + 1980);
        }
    }
}
