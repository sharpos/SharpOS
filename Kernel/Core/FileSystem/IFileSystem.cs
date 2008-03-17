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
using SharpOS.Kernel.DriverSystem.Drivers.Block;

namespace SharpOS.Kernel.FileSystem
{
	public interface IFileSystem
	{
		// creates a file and returns a OpenFile handle
		OpenFile Create(string name, FileMode mode);

		// creates a file and returns a OpenFile handle	
		// must specify full path including drive letter
		OpenFile Open(string filename, FileAccess access);

		// creates a file and returns a OpenFile handle	
		// directory is the directory to start from
		OpenFile Open(OpenFile directory, string filename, FileAccess access);

		// reads data from the file		
		int Read(OpenFile file, byte[] bytes, uint size);

		// writes data to the file (or directory)
		int Write(OpenFile file, byte[] bytes, uint size);

		// seeks within the file 
		void Seek(OpenFile file, ulong location, SeekOrigin origin);

		// closes the file (or directory)
		void Close(OpenFile file);

		// deletes the file (or directory)
		void Delete(string name);

		// renames a file (or directory)
		void Rename(string src, string dst);

		// returns true if the file system supports modification
		bool CanWrite();

		// returns true this file system is case sensitive
		bool IsCaseSensitve();

		// returns true if file system is valid on this device (read-only operation)
		bool IsMountable(IBlockDevice device);

		// returns true if file system can be formatted 
		// bool Format(IBlockDevice device);
	}

}
