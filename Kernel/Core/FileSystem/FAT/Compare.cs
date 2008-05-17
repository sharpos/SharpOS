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
using SharpOS.Kernel.DriverSystem;
using SharpOS.Kernel.Vfs;

namespace SharpOS.Kernel.FileSystem.Fat
{
	public interface ICompare
	{
		bool Compare (MemoryBlock entry, FatType type);
	}

	public class FatMatchClusterComparer : ICompare
	{
		protected uint cluster;

		public FatMatchClusterComparer (uint cluster)
		{
			this.cluster = cluster;
		}

		public bool Compare (MemoryBlock entry, FatType type)
		{
			byte first = entry.GetByte (Entry.DOSName);

			if (first == FileNameAttribute.LastEntry)
				return false;

			if ((first == FileNameAttribute.Deleted) | (first == FileNameAttribute.Dot))
				return false;

			if (first == FileNameAttribute.Escape)
				return false;

			uint startcluster = FileSystem.GetClusterEntry (entry, type);

			if (startcluster == cluster)
				return true;

			return false;
		}
	}

	public class FatAnyExistComparer : ICompare
	{
		protected uint cluster;

		public FatAnyExistComparer ()
		{
		}

		public bool Compare (MemoryBlock entry, FatType type)
		{
			byte first = entry.GetByte (Entry.DOSName);

			if (first == FileNameAttribute.LastEntry)
				return false;

			if ((first == FileNameAttribute.Deleted) | (first == FileNameAttribute.Dot))
				return false;

			if (first == FileNameAttribute.Escape)
				return false;

			return true;
		}
	}

	public class FatEntityComparer : ICompare
	{
		protected string name;

		public FatEntityComparer (string name)
		{
			this.name = name;
		}

		public bool Compare (MemoryBlock entry, FatType type)
		{
			byte first = entry.GetByte (Entry.DOSName);

			if (first == FileNameAttribute.LastEntry)
				return false;

			if ((first == FileNameAttribute.Deleted) | (first == FileNameAttribute.Dot))
				return false;

			if (first == FileNameAttribute.Escape)
				return false;

			string entryname = FileSystem.ExtractFileName (entry);

			if (entryname == name)
				return true;

			return false;
		}
	}
}