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

namespace SharpOS.Tools.DiagnosticTool {
	public class WindowsNamedPipe : INamedPipe {
		public WindowsNamedPipe (string name)
		{
			path = name;
		}

		private IntPtr pipeHandle;
		string path;
		bool opened = false;
		private uint rbytes;
		private uint wbytes;

		#region Named pipes related native declarations

		private struct COMMTIMEOUTS
		{
			public UInt32 ReadIntervalTimeout;
			public UInt32 ReadTotalTimeoutMultiplier;
			public UInt32 ReadTotalTimeoutConstant;
			public UInt32 WriteTotalTimeoutMultiplier;
			public UInt32 WriteTotalTimeoutConstant;
		}

		[DllImport ("kernel32.dll")]
		private static extern bool GetCommTimeouts (IntPtr hFile, ref COMMTIMEOUTS lpCommTimeouts);

		[DllImport ("kernel32.dll")]
		private static extern bool SetCommTimeouts (IntPtr hFile, ref COMMTIMEOUTS lpCommTimeouts);

		[DllImport ("kernel32.dll", SetLastError = true)]
		private static extern IntPtr CreateFile (
			string lpFileName,						  // file name
			uint dwDesiredAccess,					  // access mode
			uint dwShareMode,								// share mode
			IntPtr attr,				// SD
			uint dwCreationDisposition,			// how to create
			uint dwFlagsAndAttributes,			// file attributes
			IntPtr hTemplateFile);					  // handle to template file

		[DllImport ("kernel32.dll", SetLastError = true)]
		private static extern bool WaitNamedPipe (string name, int timeout);

		[DllImport ("kernel32.dll", SetLastError = true)]
		private static extern bool CloseHandle (IntPtr hHandle);

		[DllImport ("kernel32.dll", SetLastError = true)]
		private static extern bool ReadFile (
			IntPtr hHandle,											// handle to file
			byte [] lpBuffer,								// data buffer
			uint nNumberOfBytesToRead,			// number of bytes to read
			ref uint lpNumberOfBytesRead,			// number of bytes read
			uint lpOverlapped								// overlapped buffer
			);

		[DllImport ("kernel32.dll", SetLastError = true)]
		private static extern bool WriteFile (
			IntPtr hHandle,											// handle to file
			byte [] lpBuffer,							  // data buffer
			uint nNumberOfBytesToWrite,			// number of bytes to write
			ref uint lpNumberOfBytesWritten,	// number of bytes written
			uint lpOverlapped								// overlapped buffer
			);

		#endregion

		#region INamedPipe implementations

		public uint BytesWritten { get { return wbytes; } }
		public uint BytesRead { get { return rbytes; } }
		public string FileName { get { return path; } }

		public bool Opened {
			get { return opened; }
		}

		public Client.Status Open ()
		{
			string name = this.path;

			// Try to open a named pipe; wait for it, if necessary.
			while (true)
			{
				pipeHandle = CreateFile (name, 0xC0000000/*GENERIC_READ|GENERIC_WRITE*/, 0, IntPtr.Zero, 3/*OPEN_EXISTING(3) OPEN_ALWAYS(4)*/, 0, IntPtr.Zero);
				if (pipeHandle.ToInt32 () != -1/*INVALID_HANDLE_VALUE*/)
				{
					break;
				}

				// Exit if an error other than ERROR_PIPE_BUSY occurs.
				if (Marshal.GetLastWin32Error () != 231/*ERROR_PIPE_BUSY*/)
				{
					return Client.Status.Error;
				}

				// All pipe instances are busy, so wait for 4 seconds.
				if (!WaitNamedPipe (name, 4000))
				{
					return Client.Status.Error;
				}
			}

			return Client.Status.Success;
		}

		public void Close ()
		{
			CloseHandle (pipeHandle);
		}

		public Client.Status Read (byte [] buffer, ref uint count)
		{
			bool bResult = ReadFile (pipeHandle, buffer, (uint)buffer.Length, ref count, (uint)0);
			if (!bResult)
			{
				return Client.Status.Error;
			}
			rbytes += count;
#if LOG_IO
			StringBuilder sb = new StringBuilder ("-> ");
			foreach (byte b in buffer) sb.AppendFormat (" {0:X2}", b);
			MainWindow.InvokeLog (sb.ToString ());
#endif
			return Client.Status.Success;
		}

		public Client.Status Write (byte [] buffer)
		{
			uint bytesWritten = 0;
#if LOG_IO
			StringBuilder sb = new StringBuilder("<- ");
			foreach(byte b in buffer) sb.AppendFormat(" {0:X2}", b);
			MainWindow.InvokeLog (sb.ToString());
#endif
			bool bResult = WriteFile (pipeHandle, buffer, (uint)buffer.Length, ref bytesWritten, 0);
			if (!bResult)
			{
				MainWindow.InvokeLog ("Communicator.Send() failed");
				return Client.Status.Error;
			}
			wbytes += bytesWritten;

			Thread.Sleep (50);

			return Client.Status.Success;
		}

		#endregion
	}
}
