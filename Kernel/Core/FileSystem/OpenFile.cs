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

namespace SharpOS.Kernel.FileSystem
{
	public enum FileType : byte
	{
		Directory = 0,
		File = 1,
		Root = 2
	}

	public class OpenFile
	{
		//public IFileSystem FileSystem; // reference to the given file system

		public char[] Name;
		public FileType Type;
		public FileDateTime LastAccessTime;
		public FileDateTime LastModifiedTime;
		public FileDateTime CreateTime;
		public uint Size;

		public OpenFile Directory;

		public bool ReadOnly;
		public bool Hidden = false;	// only used on FAT
		public bool Archive = true;	// only used on FAT
		public bool System = false;	// only used on FAT

		public uint _position;	// position into file or index into directory
		public uint _positionondisk;	// location on disk at position
		public uint _startdisklocation;// location on disk
		public uint _count;		// number of opens on this file or directory
	}

	public struct FileDateTime	// used until DateTime is available
	{
		public ushort Year;
		public ushort Month;
		public ushort Day;
		public ushort Hour;
		public ushort Minute;
		public ushort Second;
		public ushort Milliseconds;
	}

	public enum FileAccess
	{
		Read,
		Write,
		ReadWrite
	}

	public enum FileMode
	{
		Append,
		Create,
		CreateNew,
		Open,
		OpenOrCreate,
		Truncate
	}

	public enum SeekOrigin
	{
		Begin,
		Current,
		End
	}

}
