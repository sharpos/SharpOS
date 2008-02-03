// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Cédric Rousseau <cedrou@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.IO;

namespace DiskImageUpdater
{
	class MsVhd
	{
		private enum Offsets
		{
			Cookie = 0, // 8
			Features = 8, // 4
			FileFormatVersion = 12, // 4
			DataOffset = 16, // 8
			TimeStamp = 24, // 4
			CreatorApplication = 28, // 4
			CreatorVersion = 32, // 4
			CreatorHostOS = 36, // 4
			OriginalSize = 40, // 8
			CurrentSize = 48, // 8
			DiskGeometry = 56, // 4
			DiskType = 60, // 4
			Checksum = 64, // 4
			UniqueId = 68, // 16
			SavedState = 84, // 1
			Reserved = 85 // 427
		}

		public static void CreateFromImage (string imagePath, string vhdPath)
		{
			File.Copy (imagePath, vhdPath, true);
			FileInfo fi = new FileInfo (vhdPath);
			long imgSize = fi.Length;
			long alignedSize = (imgSize + 511) & ~((long)511);

			byte [] footer = new byte [512];

			int off;

			off = (int)Offsets.Cookie;
			footer[off++] = (byte)'c';
			footer[off++] = (byte)'o';
			footer[off++] = (byte)'n';
			footer[off++] = (byte)'e';
			footer[off++] = (byte)'c';
			footer[off++] = (byte)'t';
			footer[off++] = (byte)'i';
			footer[off++] = (byte)'x';

			off = (int)Offsets.Features;
			footer [off++] = 0;
			footer [off++] = 0;
			footer [off++] = 0;
			footer [off++] = 2;

			off = (int)Offsets.FileFormatVersion;
			footer [off++] = 0;
			footer [off++] = 1;
			footer [off++] = 0;
			footer [off++] = 0;

			off = (int)Offsets.DataOffset;
			footer [off++] = 0xFF;
			footer [off++] = 0xFF;
			footer [off++] = 0xFF;
			footer [off++] = 0xFF;
			footer [off++] = 0xFF;
			footer [off++] = 0xFF;
			footer [off++] = 0xFF;
			footer [off++] = 0xFF;

			off = (int)Offsets.TimeStamp;
			DateTime now = DateTime.Now;
			DateTime org = new DateTime (2000, 1, 1, 0, 0, 0);
			int stamp = (now - org).Seconds;
			footer [off++] = (byte)((stamp >> 24) & 0xFF);
			footer [off++] = (byte)((stamp >> 16) & 0xFF);
			footer [off++] = (byte)((stamp >> 8) & 0xFF);
			footer [off++] = (byte)(stamp & 0xFF);

			off = (int)Offsets.CreatorApplication;
			footer [off++] = (byte)'#';
			footer [off++] = (byte)'O';
			footer [off++] = (byte)'S';
			footer [off++] = (byte)' ';

			off = (int)Offsets.CreatorVersion;
			footer [off++] = 0;
			footer [off++] = 1;
			footer [off++] = 0;
			footer [off++] = 0;

			off = (int)Offsets.CreatorHostOS;
			footer [off++] = (byte)'W';
			footer [off++] = (byte)'i';
			footer [off++] = (byte)'2';
			footer [off++] = (byte)'k';

			off = (int)Offsets.OriginalSize;
			footer [off++] = (byte)((alignedSize >> 56) & 0xFF);
			footer [off++] = (byte)((alignedSize >> 48) & 0xFF);
			footer [off++] = (byte)((alignedSize >> 40) & 0xFF);
			footer [off++] = (byte)((alignedSize >> 32) & 0xFF);
			footer [off++] = (byte)((alignedSize >> 24) & 0xFF);
			footer [off++] = (byte)((alignedSize >> 16) & 0xFF);
			footer [off++] = (byte)((alignedSize >> 8) & 0xFF);
			footer [off++] = (byte)(alignedSize & 0xFF);

			off = (int)Offsets.CurrentSize;
			footer [off++] = (byte)((alignedSize >> 56) & 0xFF);
			footer [off++] = (byte)((alignedSize >> 48) & 0xFF);
			footer [off++] = (byte)((alignedSize >> 40) & 0xFF);
			footer [off++] = (byte)((alignedSize >> 32) & 0xFF);
			footer [off++] = (byte)((alignedSize >> 24) & 0xFF);
			footer [off++] = (byte)((alignedSize >> 16) & 0xFF);
			footer [off++] = (byte)((alignedSize >> 8) & 0xFF);
			footer [off++] = (byte)(alignedSize & 0xFF);

			off = (int)Offsets.DiskGeometry;
			int geom = ComputeCHS (alignedSize >> 9);
			footer [off++] = (byte)((geom >> 24) & 0xFF);
			footer [off++] = (byte)((geom >> 16) & 0xFF);
			footer [off++] = (byte)((geom >> 8) & 0xFF);
			footer [off++] = (byte)(geom & 0xFF);

			off = (int)Offsets.DiskType;
			footer [off++] = 0;
			footer [off++] = 0;
			footer [off++] = 0;
			footer [off++] = 2;

			off = (int)Offsets.Checksum;
			footer [off++] = 0;
			footer [off++] = 0;
			footer [off++] = 0;
			footer [off++] = 0;

			off = (int)Offsets.UniqueId;
			byte[] uuid = (new Guid ()).ToByteArray();
			Array.Copy (uuid, 0, footer, off, 16);

			off = (int)Offsets.SavedState;
			footer [off++] = 0;
			footer [off++] = 0;
			footer [off++] = 0;
			footer [off++] = 0;

			// Compute the checksum
			int chksum = 0;
			for (off = 0; off < footer.Length; off++)
				chksum += footer [off];
			chksum = (~chksum);

			off = (int)Offsets.Checksum;
			footer [off++] = (byte)((chksum >> 24) & 0xFF);
			footer [off++] = (byte)((chksum >> 16) & 0xFF);
			footer [off++] = (byte)((chksum >> 8) & 0xFF);
			footer [off++] = (byte)(chksum & 0xFF);


			FileStream bw = new FileStream (vhdPath, FileMode.Append, FileAccess.Write);
			for (off = 0; off <= alignedSize - imgSize; off++)
				bw.WriteByte (0);
			bw.Write (footer, 0, footer.Length);
			bw.Close ();
		}

		// From Microsoft's VHD spec
		private static int ComputeCHS (long totalSectors)
		{
			int sectorsPerTrack, heads, cylinderTimesHeads, cylinders;

			if (totalSectors > 65535 * 16 * 255)
				totalSectors = 65535 * 16 * 255;

			if (totalSectors >= 65535 * 16 * 63)
			{
				sectorsPerTrack = 255;
				heads = 16;
				cylinderTimesHeads = (int)totalSectors / sectorsPerTrack;
			}
			else
			{
				sectorsPerTrack = 17;
				cylinderTimesHeads = (int)totalSectors / sectorsPerTrack;

				heads = (cylinderTimesHeads + 1023) / 1024;

				if (heads < 4)
					heads = 4;

				if (cylinderTimesHeads >= (heads * 1024) || heads > 16)
				{
					sectorsPerTrack = 31;
					heads = 16;
					cylinderTimesHeads = (int)totalSectors / sectorsPerTrack;
				}

				if (cylinderTimesHeads >= (heads * 1024))
				{
					sectorsPerTrack = 63;
					heads = 16;
					cylinderTimesHeads = (int)totalSectors / sectorsPerTrack;
				}
			}
			cylinders = cylinderTimesHeads / heads;

			return (sectorsPerTrack | (heads<<8) | (cylinders<<16));
		}
	}
}
