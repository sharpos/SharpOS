//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//  Phil Garcia (aka tgiphil) <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;

namespace SharpOS.Kernel.FileSystem.FATFileSystem
{
	public class FATSettings : SettingsBase
	{
		public FATType FatType;
		public string VolumeLabel;
		public byte[] SerialID;

		public FATSettings ()
		{
			this.FatType = FATType.FAT16;	// default
			this.VolumeLabel = string.Empty;
			this.SerialID = new byte[0];
		}
	}
}