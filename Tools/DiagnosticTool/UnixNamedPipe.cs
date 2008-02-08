//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Cédric Rousseau <cedrou@gmail.com>
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//
//#define LOG_IO

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Generic;
using System.IO;

namespace SharpOS.Tools.DiagnosticTool {
	public class UnixNamedPipe : INamedPipe {
		public UnixNamedPipe (string name)
		{
      path = name;
		}

		string path;
		bool opened = false;
		private uint rbytes;
		private uint wbytes;
		private Stream stream;

		#region INamedPipe implementations

		public uint BytesWritten { get { return wbytes; } }
		public uint BytesRead { get { return rbytes; } }

		public bool Opened {
			get { return opened; }
		}

		public string FileName { get { return this.path; } }

		public Client.Status Open ()
		{
			int desc = 0;

			if (File.Exists (path))
				File.Delete (path);

			if (Mono.Unix.Native.Syscall.mkfifo (path, (Mono.Unix.Native.FilePermissions) 438) != 0) {
				Console.WriteLine ("error : {0}",
					(Mono.Unix.Native.Errno) Mono.Unix.Native.Stdlib.GetLastError ());
				return Client.Status.Error;
			}

			desc = Mono.Unix.Native.Syscall.open (path,
				Mono.Unix.Native.OpenFlags.O_RDWR | Mono.Unix.Native.OpenFlags.O_NONBLOCK);
			stream = new Mono.Unix.UnixStream (desc, true);

			Console.WriteLine ("Created UNIX FIFO successfully");
			return Client.Status.Success;
		}

		public void Close ()
		{
			this.stream.Close ();
		}

		public Client.Status Read (byte [] buffer, ref uint count)
		{
			count = (uint) this.stream.Read (buffer, 0, (int) count);

			return Client.Status.Success;
		}

		public Client.Status Write (byte [] buffer)
		{
			this.stream.Write (buffer, 0, buffer.Length);
			Thread.Sleep (50);

			return Client.Status.Success;
		}

		#endregion
	}
}
