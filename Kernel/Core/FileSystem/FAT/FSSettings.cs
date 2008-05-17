//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.Kernel.Vfs;

namespace SharpOS.Kernel.FileSystem.Fat
{
	public class FSSettings : FSSettingsBase
	{
		private FatType fatType;

		public FatType FatType
		{
			get
			{
				return fatType;
			}
			set
			{
				fatType = value;
			}
		}

		public FSSettings ()
		{
			fatType = FatType.FAT16;	// default
		}
	}
}